using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO.Compression;
using System.IO;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace WhereAreWe
{
    public class MapSheet : IDisposable, IComparable<MapSheet>
    {
        public MapSquare[,] Grid;
        public Size SquareSize;
        public Color FilledBlock = Properties.Settings.Default.DefaultFilledSquare;
        public Color EmptyBlock = Properties.Settings.Default.DefaultGridBackground;

        public Origin MapOrigin = Properties.Settings.Default.DefaultOrigin;
        public Point MapOffset = Properties.Settings.Default.DefaultGridOffset;

        private Undo m_undo = new Undo(Properties.Settings.Default.MaxUndoActions);
        private Point m_ptCursor;
        private bool m_bNeverDisplayed = true;

        private Bitmap m_bmpGrid = null;
        private Bitmap m_bmpSquare = null;

        private BasicLocation m_liYouAreHere = new BasicLocation();

        public string MenuPath = null;
        public int SortIndex = -1;
        public bool HasChanges = false;
        public bool ForceRefreshOnDisplay = false;
        public bool CurrentMap = false;
        public int GameMapIndex = -1;
        public string SheetNote;
        public MapSection[] Sections = null;
        private string m_strUnvisitedBitmapFile = String.Empty;
        public bool UseUnvisitedBitmap = false;
        public Rectangle UnvisitedCrop = Rectangle.Empty;
        public Rectangle UnvisitedGrid = Rectangle.Empty;
        public bool Live = false;       // True means every single square needs to be read from memory regularly
        public bool Directionless = false;  // True uses the "directionless" You-Are-Here icon instead of the standard arrow

        private MapIcon m_iconCurrent = null;
        private MapNote m_noteCurrent = null;
        private Rectangle[] m_anchorCursors = new Rectangle[0];
        private MonsterLocations m_monsters = null;
        private ItemLocations m_items = null;
        private ActiveSquares m_activeSquares = null;
        private Dictionary<Point, ActiveSquareInfo> m_changedActiveSquares = null;
        public MapLabels Labels = new MapLabels();

        private bool m_bGridStreamChanged = true;
        private string m_strGridStreamCache = null;
        private MapLineInfo m_gridLines = Global.DefaultGridLineInfo;

        private WizMaps m_mapsWizardry;
        private BTMaps m_mapsBardsTale;
        private MMMaps m_mapsMightAndMagic;
        private EOBMaps m_mapsEOB;
        private UltimaMaps m_mapsUltima;

        public MapLineInfo GridLines { get { return m_gridLines; } set { m_gridLines = value; } }
        public string GridStreamCache { get { return m_strGridStreamCache; } set { m_strGridStreamCache = value; } }

        public void SetZoom(int iZoom) { SquareSize = new Size(iZoom * 16 / 100, iZoom * 16 / 100); }

        public string DefaultDirectory = String.Empty;
        public string UnvisitedBitmapFile
        {
            get { return m_strUnvisitedBitmapFile; }
            set { m_strUnvisitedBitmapFile = value; }
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Title == null ? "<null>" : Title, MenuPath == null ? "<null>" : MenuPath);
        }

        public Bitmap GetUnvisitedBitmap()
        {
            return Global.BitmapCache.GetCrop(UnvisitedBitmapFile, UnvisitedCrop, DefaultDirectory);
        }

        public Bitmap GetUnvisitedBitmapSection(int x, int y, int width, int height)
        {
            return Global.BitmapCache.GetSubCrop(UnvisitedBitmapFile, UnvisitedCrop, new Rectangle(x, y, width, height), DefaultDirectory);
        }

        public bool GridStreamChanged
        {
            get
            {
                if (m_bGridStreamChanged)
                    return true;
                foreach (MapSquare square in Grid)
                    if (square.ChangedSinceSave)
                    {
                        m_bGridStreamChanged = true;
                        return true;
                    }
                return false;
            }

            set
            {
                m_bGridStreamChanged = value;
                if (!value)
                    foreach (MapSquare square in Grid)
                        square.ChangedSinceSave = false;
            }
        }

        public static void AddUndoBlock(UndoList undoBlocks, MapSquare square, Point pt)
        {
            if (undoBlocks == null)
                return;

            undoBlocks.Add(new UndoMapSquare(square, pt));
        }

        public int CompareTo(MapSheet sheet)
        {
            return String.Compare(MenuPath + "\\" + Title, sheet.MenuPath + "\\" + sheet.Title);
        }

        public bool SetAnchorSelection(Rectangle[] anchors)
        {
            if (m_anchorCursors == anchors)
                return false;

            SetDirty(m_anchorCursors);
            m_anchorCursors = anchors;
            SetDirty(m_anchorCursors);
            return true;
        }

        public bool SetCurrentIcon(MapIcon icon, Point ptLocation)
        {
            if (icon == null && m_iconCurrent == null)
                return false; // no change; don't mark anything as dirty;

            if (icon != null && 
                m_iconCurrent != null && 
                icon.Name == m_iconCurrent.Name && 
                icon.Orientation == m_iconCurrent.Orientation && 
                ptLocation == m_iconCurrent.Location)
                return false; // no change; don't mark anything as dirty;

            if (m_iconCurrent != null)
                SetDirty(m_iconCurrent.Location, DirtyType.Back);

            if (icon == null)
            {
                m_iconCurrent = null;
                return true;
            }

            m_iconCurrent = icon.Clone(ptLocation);
            SetDirty(ptLocation, DirtyType.Back);
            return true;
        }

        public bool SetCurrentNote(MapNote note, Point ptLocation)
        {
            if (note == null && m_noteCurrent == null)
                return false; // no change; don't mark anything as dirty;

            if (note != null &&
                m_noteCurrent != null &&
                note.Symbol == m_noteCurrent.Symbol &&
                note.Color.ToArgb() == m_noteCurrent.Color.ToArgb() &&
                ptLocation == m_noteCurrent.Location)
                return false; // no change; don't mark anything as dirty;

            if (!PointInGrid(ptLocation))
                note = null;

            if (m_noteCurrent != null)
                SetDirty(m_noteCurrent.Location, DirtyType.Back);

            if (note == null)
            {
                bool bResult = (m_noteCurrent != null);
                m_noteCurrent = null;
                return bResult;
            }

            m_noteCurrent = note.Clone(ptLocation);
            SetDirty(ptLocation, DirtyType.Back);
            return true;
        }

        public Undo UndoContainer
        {
            get { return m_undo; }
            set { m_undo = value; }
        }

        public bool NeverDisplayed
        {
            get { return m_bNeverDisplayed; }
            set { m_bNeverDisplayed = value; }
        }

        public Size SquareMargin
        {
            get { return new Size(SquareSize.Width * 2, SquareSize.Height * 2); }
        }

        public UndoItem StartUndoBlock()
        {
            return m_undo.AddEmptySquareItem();
        }

        public void AddUndoSheet()
        {
            m_undo.AddItem(this);
        }

        public void AddRedoSheet()
        {
            m_undo.AddRedoItem(this);
        }

        public void ClearRedo()
        {
            m_undo.ClearRedo();
        }

        public void ResetUndo()
        {
            m_undo = new Undo(Properties.Settings.Default.MaxUndoActions);
        }

        public void AddRedoBlocks(UndoList squares)
        {
            // Add the squares in this sheet that would be overwritten if the given squares are undone
            UndoMapSquare[] squaresNew = new UndoMapSquare[squares.Count];
            int i = 0;
            foreach(UndoMapSquare square in squares.Squares.Values)
            {
                squaresNew[i++] = UndoSquare(square.Location);
            }
            UndoList list = new UndoList(squaresNew);
            foreach (UndoItem item in squares.Other)
            {
                if (item.Action == UndoAction.IconsChange)
                    list.Other.Add(new UndoItem(GetAllIconsCopy()));
                else if (item.Action == UndoAction.NotesChange)
                    list.Other.Add(new UndoItem(GetAllNotesCopy()));
                else if (item.Action == UndoAction.LabelsChange)
                    list.Other.Add(new UndoItem(GetAllLabelsCopy()));
            }
            m_undo.AddRedoItem(list);
        }

        public void AddUndoBlocks(UndoList squares)
        {
            // Add the squares in this sheet that would be overwritten if the given squares are undone
            UndoMapSquare[] squaresNew = new UndoMapSquare[squares.Count];
            int i = 0;
            foreach (UndoMapSquare square in squares.Squares.Values)
            {
                squaresNew[i++] = UndoSquare(square.Location);
            }
            UndoList list = new UndoList(squaresNew);
            foreach (UndoItem item in squares.Other)
            {
                if (item.Action == UndoAction.IconsChange)
                    list.Other.Add(new UndoItem(GetAllIconsCopy()));
                else if (item.Action == UndoAction.NotesChange)
                    list.Other.Add(new UndoItem(GetAllNotesCopy()));
                else if (item.Action == UndoAction.LabelsChange)
                    list.Other.Add(new UndoItem(GetAllLabelsCopy()));
            }
            m_undo.AddItem(list);
        }

        public void AddUndoCursorBlock()
        {
            UndoItem item = m_undo.AddEmptySquareItem();
            item.Squares.Add(UndoSquare(Cursor));
        }

        public void SetSquaresFromUndo(UndoList squares)
        {
            foreach (UndoMapSquare square in squares.Squares.Values)
            {
                Grid[square.Location.X, square.Location.Y] = square.Square.Clone(Grid[square.Location.X, square.Location.Y]);
                SetDirty(square.Location);
            }
        }

        public void AddUndoVisited()
        {
            m_undo.AddItem(new VisitedArray(this));
        }

        public void AddUndoNotes()
        {
            m_undo.AddItem(GetAllNotesCopy());
        }

        public void AddUndoLabels()
        {
            m_undo.AddItem(GetAllLabelsCopy());
        }

        public void AddUndoNote(Point pt)
        {
            if (!PointInGrid(pt))
                return;
            MapNote note = new MapNote(Grid[pt.X, pt.Y].Note);
            note.Location = pt;
            m_undo.AddItem(note);
        }

        public void AddUndoLabel(MapLabel label)
        {
            MapLabel labelCopy = label.Clone();
            m_undo.AddItem(labelCopy);
        }

        public void AddRedoNote(Point pt)
        {
            if (!PointInGrid(pt))
                return;
            MapNote note = new MapNote(Grid[pt.X, pt.Y].Note);
            note.Location = pt;
            m_undo.AddRedoItem(note);
        }

        public void AddRedoNotes()
        {
            m_undo.AddRedoItem(GetAllNotesCopy());
        }

        public void AddRedoVisited()
        {
            m_undo.AddRedoItem(new VisitedArray(this));
        }

        public void AddUndoIcons()
        {
            m_undo.AddItem(GetAllIconsCopy());
        }

        public void AddRedoIcons()
        {
            m_undo.AddRedoItem(GetAllIconsCopy());
        }

        public void AddRedoLabels()
        {
            m_undo.AddRedoItem(GetAllLabelsCopy());
        }

        public void SetYouAreHereDirty()
        {
            if (m_liYouAreHere == null)
                return;
            if (GridRectangle.Contains(m_liYouAreHere.PrimaryCoordinates))
                SetDirty(m_liYouAreHere.PrimaryCoordinates, DirtyType.Back);
        }

        public void SetMonsterLocationsDirty(MonsterLocations monstersNew)
        {
            if (m_monsters == null)
            {
                // Mark all new positions as dirty
                foreach (MonsterPosition pos in monstersNew.MonsterPositions.Values)
                    SetDirty(pos.Position);
                return;
            }
            else if (monstersNew == null)
            {
                // Mark all old positions as dirty
                foreach (MonsterPosition pos in m_monsters.MonsterPositions.Values)
                    SetDirty(pos.Position);
                return;
            }

            // Mark only the monsters that have changed as dirty
            // (monster type doesn't matter -- only location, highlight, and quantity)

            foreach (MonsterPosition pos in m_monsters.MonsterPositions.Values)
            {
                // Squares that used to have a monster that now do not
                if (!monstersNew.MonsterPositions.ContainsKey(pos.Position))
                    SetDirty(pos.Position, DirtyType.Back);
                else 
                {
                    // Squares in which the number of monsters or highlight state changed
                    MonsterPosition posNew = monstersNew.MonsterPositions[pos.Position];
                    if (posNew.Highlighted != pos.Highlighted || 
                        posNew.Monsters.Count != pos.Monsters.Count ||
                        posNew.Monsters.Any(m => m.Active) != m_monsters.MonsterPositions[pos.Position].Monsters.Any(m => m.Active))
                        SetDirty(pos.Position);
                }
            }

            // Squares that did not have a monster that now do
            foreach (MonsterPosition monster in monstersNew.MonsterPositions.Values)
            {
                if (!m_monsters.MonsterPositions.ContainsKey(monster.Position))
                    SetDirty(monster.Position, DirtyType.Back);
            }
        }

        public MonsterLocations Monsters
        {
            get { return m_monsters; }
            set
            {
                if (value != null)
                    SetMonsterLocationsDirty(value);
                m_monsters = value;
                m_monsters.Drawn = false;
            }
        }

        public void SetItemLocationsDirty(ItemLocations itemsNew)
        {
            if (m_items == null)
            {
                // Mark all new positions as dirty
                foreach (ItemPosition pos in itemsNew.ItemPositions.Values)
                    SetDirty(pos.Position);
                return;
            }
            else if (itemsNew == null)
            {
                // Mark all old positions as dirty
                foreach (ItemPosition pos in m_items.ItemPositions.Values)
                    SetDirty(pos.Position);
                return;
            }

            // Mark only the items that have changed as dirty
            // (item type doesn't matter -- only location and quantity)

            foreach (ItemPosition pos in m_items.ItemPositions.Values)
            {
                // Squares that used to have a item that now do not
                if (!itemsNew.ItemPositions.ContainsKey(pos.Position))
                    SetDirty(pos.Position, DirtyType.Back);
                else
                {
                    // Squares in which the number of items changed
                    ItemPosition posNew = itemsNew.ItemPositions[pos.Position];
                    if (posNew.Items.Count != pos.Items.Count)
                        SetDirty(pos.Position);
                }
            }

            // Squares that did not have a item that now do
            foreach (ItemPosition item in itemsNew.ItemPositions.Values)
            {
                if (!m_items.ItemPositions.ContainsKey(item.Position))
                    SetDirty(item.Position, DirtyType.Back);
            }
        }

        public ItemLocations Items
        {
            get { return m_items; }
            set
            {
                if (value != null)
                    SetItemLocationsDirty(value);
                m_items = value;
                m_items.Drawn = false;
            }
        }

        public bool SetActiveSquares(ActiveSquares squares, MapBook book, bool bForceUpdate = false)
        {
            if (squares == null)
                return false;

            if (bForceUpdate || (m_activeSquares != null && squares.MapIndex != m_activeSquares.MapIndex))
                m_activeSquares = null;

            if (m_activeSquares == null)
                m_changedActiveSquares = squares.GetInternalMapDelta(null);
            else
            {
                m_changedActiveSquares = m_activeSquares.GetInternalMapDelta(squares);
                if (m_changedActiveSquares == null)
                    return false; // Not a valid changelist; check again later

                m_activeSquares.Drawn = true;   // For any objects holding on to this reference
            }

            if (m_changedActiveSquares == null)
                return false; // Not a valid changelist; check again later

            if (m_changedActiveSquares.Count == 0)
            {
                m_activeSquares = squares;
                return false;
            }

            foreach (Point pt in m_changedActiveSquares.Keys)
            {
                if (GridRectangle.Contains(pt))
                    SetDirty(pt, DirtyType.Back);
            }

            m_activeSquares = squares;
            m_activeSquares.Drawn = false;
            return true;
        }

        public BasicLocation YouAreHere
        {
            get { return m_liYouAreHere; }
        }

        public bool SetSeen(Point pt, Direction dir, int iLightDistance, GameNames game)
        {
            // Typically a 3xN block of squares in front of the party is visible, unless there are walls blocking vision
            // or there isn't enough light to see that far
            Rectangle rcVisible = Rectangle.Empty;
            switch (dir)
            {
                case Direction.Up:
                    rcVisible = new Rectangle(pt.X - 1, pt.Y - 4, 3, 5);
                    break;
                case Direction.Down:
                    rcVisible = new Rectangle(pt.X - 1, pt.Y, 3, 5);
                    break;
                case Direction.Left:
                    rcVisible = new Rectangle(pt.X - 4, pt.Y - 1, 5, 3);
                    break;
                case Direction.Right:
                    rcVisible = new Rectangle(pt.X, pt.Y - 1, 5, 3);
                    break;
                default:
                    // Invalid direction; do nothing
                    return false;
            }

            bool bAnySet = false;
            for (int row = rcVisible.Y; row < rcVisible.Bottom; row++)
            {
                for (int col = rcVisible.X; col < rcVisible.Right; col++)
                {
                    Point ptTest = new Point(col, row);
                    if (Global.LOS.IsVisible(game, this, pt, dir, ptTest, iLightDistance))
                        bAnySet |= SetSeen(ptTest, true);
                }
            }

            return bAnySet;
        }

        public void ClearYouAreHere()
        {
            if (m_liYouAreHere != null)
                SetDirty(m_liYouAreHere.PrimaryCoordinates, DirtyType.Back);
            m_liYouAreHere = null;
        }

        public bool SetYouAreHere(BasicLocation location, IMain main, bool bIgnoreInaccessible)
        {
            SetYouAreHereDirty();
            m_liYouAreHere = location;
            Global.FixRange(ref m_liYouAreHere.PrimaryCoordinates, 0, GridWidth - 1, 0, GridHeight - 1);
            SetDirty(m_liYouAreHere.PrimaryCoordinates, DirtyType.Back);
            bool bChanges = false;
            if (!main.Hacker.CartographySupportsSeen)
                bChanges = SetSeen(m_liYouAreHere.PrimaryCoordinates, m_liYouAreHere.Facing, location.LightDistance, main.Game);
            m_liYouAreHere.Drawn = false;
            if (Properties.Settings.Default.RevealAdjacentInaccessible && !bIgnoreInaccessible)
                CheckInaccessible(m_liYouAreHere.PrimaryCoordinates,
                    (Properties.Settings.Default.UpdateCartWhenInaccessibleRevealed && Properties.Settings.Default.EnableMemoryWrite) ? main : null);
            return bChanges;
        }

        public bool RefreshYouAreHere(IMain main, bool bIgnoreInaccessible)
        {
            if (m_liYouAreHere == null)
                return false;
            return SetYouAreHere(m_liYouAreHere, main, bIgnoreInaccessible);
        }

        public void SetYouAreHere(LocationInformation li, IMain main, bool bIgnoreInaccessible)
        {
            SetYouAreHere(new BasicLocation(li), main, bIgnoreInaccessible);
        }

        public void SetYouAreHereVisited()
        {
            if (m_liYouAreHere == null || !PointInGrid(m_liYouAreHere.PrimaryCoordinates))
                return;
            MapSquare square = Grid[m_liYouAreHere.PrimaryCoordinates.X, m_liYouAreHere.PrimaryCoordinates.Y];
            if (!square.Visited)
            {
                square.SetDirty(DirtyType.Back);
                if (CurrentMap)
                {
                    if (Properties.Settings.Default.HideUnvisitedSquares)
                    {
                        SetVisited(m_liYouAreHere.PrimaryCoordinates, true);
                        HasChanges = true;
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_bmpGrid != null)
                    m_bmpGrid.Dispose();
                if (m_bmpSquare != null)
                    m_bmpSquare.Dispose();
            }
        }

        private void InitSheet()
        {
            m_mapsWizardry = new WizMaps(this);
            m_mapsBardsTale = new BTMaps(this);
            m_mapsMightAndMagic = new MMMaps(this);
            m_mapsEOB = new EOBMaps(this);
            m_mapsUltima = new UltimaMaps(this);
            Cursor = Point.Empty;
        }

        public MapSheet(MapSheet sheetCopy)
        {
            InitSheet();
            using (MemoryStream ms = new MemoryStream())
            {
                sheetCopy.Serialize(ms);
                ms.Seek(0, SeekOrigin.Begin);
                Cursor = Point.Empty;
                SetGridData(sheetCopy.GridWidth, sheetCopy.GridHeight, ms);
                DefaultZoom = sheetCopy.DefaultZoom;
                SetAllNotes(sheetCopy.GetAllNotesCopy());
                SetAllIcons(sheetCopy.GetAllIconsCopy());
                SetAllLabels(sheetCopy.GetAllLabelsCopy());
                Sections = sheetCopy.GetSectionsCopy();
            }
        }

        public MapSheet(MapLineInfo gridLines)
        {
            InitSheet();
            GridLines = gridLines;
            Grid = new MapSquare[Properties.Settings.Default.DefaultMapSize.Width, Properties.Settings.Default.DefaultMapSize.Height];
            SquareSize = new Size(Properties.Settings.Default.DefaultSquareSize.Width, Properties.Settings.Default.DefaultSquareSize.Height);
            CreateGrid();
            GridStreamChanged = true;
            DefaultZoom = 200;
        }

        public MapSheet(MapLineInfo gridLines, int width, int height, Stream stream)
        {
            InitSheet();
            SetGridData(width, height, stream);
        }

        public void SetGridData(int width, int height, Stream stream)
        {
            Grid = new MapSquare[width, height];
            SquareSize = new Size(Properties.Settings.Default.DefaultSquareSize.Width, Properties.Settings.Default.DefaultSquareSize.Height);
            for (int row = 0; row < GridHeight; row++)
            {
                for (int col = 0; col < GridWidth; col++)
                {
                    Grid[col, row] = new MapSquare(stream);
                    SetDirty(col, row);
                }
            }
        }

        public Point Cursor
        {
            get { return m_ptCursor; }
            set { m_ptCursor = value; }
        }

        public MapSquare CursorSquare
        {
            get { return Grid[m_ptCursor.X, m_ptCursor.Y]; }
        }

        public MapNote NoteAtPoint(Point pt)
        {
            if (!PointInGrid(pt))
                return null;

            if (Grid[pt.X, pt.Y] == null)
                return null;

            MapNote note = Grid[pt.X, pt.Y].Note;
            if (note == null)
                note = new MapNote();

            note.Location = pt;
            return note;
        }

        public MapNote NoteAtCursor
        {
            get { return NoteAtPoint(Cursor); }
        }

        public bool HasNote(Point pt)
        {
            if (!PointInGrid(pt))
                return false;

            return (Grid[pt.X, pt.Y].Note != null);
        }

        public bool HasIcons(Point pt)
        {
            return (Grid[pt.X, pt.Y].Icons != null && Grid[pt.X, pt.Y].Icons.Count > 0);
        }

        public void SetNote(MapNote note)
        {
            if (!GridRectangle.Contains(note.Location))
                return;

            Grid[note.Location.X, note.Location.Y].Note = (note.Symbol == "" || note.Text == "" ? null : note);
            SetDirty(note.Location, DirtyType.Back);
        }

        public void SetIcon(MapIcon icon)
        {
            if (!GridRectangle.Contains(icon.Location))
                return;

            MapSquare square = Grid[icon.Location.X, icon.Location.Y];
            if (square.Icons == null)
                square.Icons = new List<MapIcon>();

            if (icon.Name != IconName.None)
            {
                square.Icons.Add(icon);
                square.SetDirty(DirtyType.Back);
            }
        }

        public void SetAllNotes(MapNote[] notes)
        {
            ClearNotes();

            foreach (MapNote note in notes)
                SetNote(note);
        }

        public void SetAllIcons(MapIcon[] icons)
        {
            ClearIcons();

            foreach (MapIcon icon in icons)
                SetIcon(icon);
        }

        public void ClearNotes()
        {
            for (int row = 0; row < GridHeight; row++)
            {
                for (int col = 0; col < GridWidth; col++)
                {
                    if (Grid[col, row].Note != null)
                    {
                        Grid[col, row].Note = null;
                        SetDirty(col, row, DirtyType.Back);
                    }
                }
            }
        }

        public void ClearIcons()
        {
            for (int row = 0; row < GridHeight; row++)
            {
                for (int col = 0; col < GridWidth; col++)
                {
                    if (Grid[col, row].Icons != null && Grid[col, row].Icons.Count > 0)
                    {
                        Grid[col, row].Icons = new List<MapIcon>();
                        SetDirty(col, row, DirtyType.Back);
                    }
                }
            }
        }

        public MapNote[] GetAllNotes()
        {
            List<MapNote> notes = new List<MapNote>();
            for (int row = 0; row < GridHeight; row++ )
            {
                for (int col = 0; col < GridWidth; col++)
                {
                    if (Grid[col, row].Note != null)
                        notes.Add(Grid[col, row].Note);
                }
            }

            return notes.ToArray();
        }

        public MapNote[] GetAllNotesCopy()
        {
            List<MapNote> notes = new List<MapNote>();
            for (int row = 0; row < GridHeight; row++)
            {
                for (int col = 0; col < GridWidth; col++)
                {
                    if (Grid[col, row].Note != null)
                        notes.Add(new MapNote(Grid[col, row].Note));
                }
            }

            return notes.ToArray();
        }

        public MapSection[] GetSectionsCopy()
        {
            if (Sections == null)
                return new MapSection[0];
            MapSection[] sections = new MapSection[Sections.Length];
            for (int i = 0; i < Sections.Length; i++)
                sections[i] = Sections[i].Clone();
            return sections;
        }

        public MapLabel[] GetAllLabelsCopy()
        {
            MapLabel[] labels = new MapLabel[Labels.Count];
            int i = 0;
            foreach(PointF pt in Labels.Keys)
                labels[i++] = Labels[pt].Clone();
            return labels;
        }

        public MapIcon[] GetAllIcons()
        {
            List<MapIcon> icons = new List<MapIcon>();
            for (int row = 0; row < GridHeight; row++)
            {
                for (int col = 0; col < GridWidth; col++)
                {
                    if (Grid[col, row].Icons != null)
                        icons.AddRange(Grid[col, row].Icons);
                }
            }

            return icons.ToArray();
        }

        public MapIcon[] GetAllIconsCopy()
        {
            List<MapIcon> icons = new List<MapIcon>();
            for (int row = 0; row < GridHeight; row++)
            {
                for (int col = 0; col < GridWidth; col++)
                {
                    if (Grid[col, row].Icons != null)
                    {
                        foreach(MapIcon icon in Grid[col, row].Icons)
                            icons.Add(new MapIcon(icon));
                    }
                }
            }

            return icons.ToArray();
        }

        public Point TranslateLocation(Point pt)
        {
            switch (MapOrigin)
            {
                case Origin.Center:
                    pt.X -= (GridWidth / 2);
                    pt.Y -= (GridHeight / 2);
                    break;
                case Origin.LowerLeft:
                    pt.Y = GridHeight - 1 - pt.Y;
                    break;
                case Origin.LowerRight:
                    pt.X = GridWidth - 1 - pt.X;
                    pt.Y = GridHeight - 1 - pt.Y;
                    break;
                case Origin.UpperLeft:
                    break;
                case Origin.UpperRight:
                    pt.X = GridWidth - 1 - pt.X;
                    break;
                default:
                    break;
            }

            pt.X += MapOffset.X;
            pt.Y += MapOffset.Y;

            return pt;
        }

        public PointF TranslateLocation(PointF pt)
        {
            switch (MapOrigin)
            {
                case Origin.Center:
                    pt.X -= (GridWidth / 2);
                    pt.Y -= (GridHeight / 2);
                    break;
                case Origin.LowerLeft:
                    pt.Y = GridHeight - 1 - pt.Y;
                    break;
                case Origin.LowerRight:
                    pt.X = GridWidth - 1 - pt.X;
                    pt.Y = GridHeight - 1 - pt.Y;
                    break;
                case Origin.UpperLeft:
                    break;
                case Origin.UpperRight:
                    pt.X = GridWidth - 1 - pt.X;
                    break;
                default:
                    break;
            }

            pt.X += MapOffset.X;
            pt.Y += MapOffset.Y;

            return pt;
        }

        public int GridWidth
        {
            get { return Grid.GetLength(0); }
        }

        public int GridHeight
        {
            get { return Grid.GetLength(1); }
        }

        public Rectangle GridRectangle
        {
            get { return new Rectangle(0, 0, GridWidth, GridHeight); }
        }

        public string Title { get; set; }

        public int DefaultZoom { get; set; }
        public int CurrentZoom { get { return SquareSize.Width * 100 / 16; } }

        public bool IsLegend
        {
            get { return (Title == "Legend"); }
        }

        public void CreateGrid()
        {
            for (int row = 0; row < GridHeight; row++)
                for (int col = 0; col < GridWidth; col++)
                    Grid[col, row] = new MapSquare(GridLines);
        }

        private SurroundingSquares GetSurrounding(int col, int row)
        {
            return new SurroundingSquares(new Point(col, row),
                Grid[col, row],
                row > 0 ? Grid[col, row - 1] : null,
                col < GridWidth - 1 ? Grid[col + 1, row] : null,
                row < GridHeight - 1 ? Grid[col, row + 1] : null,
                col > 0 ? Grid[col - 1, row] : null,
                row > 0 && col > 0 ? Grid[col - 1, row - 1] : null,
                row > 0 && col < GridWidth - 1 ? Grid[col + 1, row - 1] : null,
                row < GridHeight - 1 && col > 0 ? Grid[col - 1, row + 1] : null,
                row < GridHeight - 1 && col < GridWidth - 1 ? Grid[col + 1, row + 1] : null
                );

        }

        public void CheckInaccessible(Point ptGrid, IMain main)
        {
            // Checks whether to mark inaccessible squares around a particular square as visited
            HideInaccessible(main, ptGrid.X, ptGrid.Y - 1, DirectionFlags.North, true);
            HideInaccessible(main, ptGrid.X + 1, ptGrid.Y, DirectionFlags.East, true);
            HideInaccessible(main, ptGrid.X, ptGrid.Y + 1, DirectionFlags.South, true);
            HideInaccessible(main, ptGrid.X - 1, ptGrid.Y, DirectionFlags.West, true);
        }

        public bool HideInaccessible(IMain main, int col, int row, DirectionFlags dir = DirectionFlags.All, bool bMarkVisited = false)
        {
            // Returns true if (col,row) is an unvisited normal square, an off-grid square, or
            // an unimportant square completely bordered by at least one unvisited normal square
            if (col < 0 || row < 0 || col >= GridWidth || row >= GridHeight)
                return false;

            MapSquare square = Grid[col, row];

            if (!square.IsUnimportant)
                return !square.Visited;

            if (square.Visited)
                return false;       // This inaccessible square has been visited somehow, so override the tests

            // If this is an unimportant square, it is to be hidden unless all normal squares
            // around it are visited (in which case, mark it as visited).

            bool bHide = false;
            switch (dir)
            {
                case DirectionFlags.All:
                    // Show the square if north/south OR east/west adjacent squares are revealed
                    bool bHideEastWest =
                        HideInaccessible(main, col - 1, row, DirectionFlags.West, bMarkVisited) ||
                        HideInaccessible(main, col + 1, row, DirectionFlags.East, bMarkVisited);
                    bool bHideNorthSouth =
                        HideInaccessible(main, col, row - 1, DirectionFlags.North, bMarkVisited) ||
                        HideInaccessible(main, col, row + 1, DirectionFlags.South, bMarkVisited);
                    bHide = bHideEastWest && bHideNorthSouth;
                    break;
                case DirectionFlags.West:
                    bHide = HideInaccessible(main, col - 1, row, DirectionFlags.West, bMarkVisited);
                    break;
                case DirectionFlags.East:
                    bHide = HideInaccessible(main, col + 1, row, DirectionFlags.East, bMarkVisited);
                    break;
                case DirectionFlags.North:
                    bHide = HideInaccessible(main, col, row - 1, DirectionFlags.North, bMarkVisited);
                    break;
                case DirectionFlags.South:
                    bHide = HideInaccessible(main, col, row + 1, DirectionFlags.South, bMarkVisited);
                    break;
                default:
                    // Something is wrong; don't hide the square;
                    bHide = false;
                    break;
            }
            if (bMarkVisited && !bHide)
            {
                SetVisited(new Point(col, row), true);
                if (main != null)
                    main.Hacker.ToggleCartography(main.TranslateToGameMap(new Point(col, row), this), Toggle.Set);
            }
            return bHide;
        }

        private void DrawLineHoriz(bool bMask, Graphics g, Size szExtra, MapLineInfo line, float scaleWidth, float x1, float x2, float y, float scaleY, bool bForceSolid = false)
        {
            x1 += szExtra.Width;
            x2 += szExtra.Width;
            y += szExtra.Height;

            Pen pen = new Pen(bMask ? Global.PreMaskColor : line.Color, (line.Width + line.AltWidth) * scaleWidth);
            pen.DashStyle = bForceSolid ? DashStyle.Solid : line.Pattern;
            if (line.AltWidth == 0)
                g.DrawLine(pen, x1 + scaleWidth, y + scaleWidth / 2, x2 - pen.Width, y + scaleWidth / 2);
            else
                g.DrawLine(pen, x1, y, x2, y);
        }

        private void DrawLineVert(bool bMask, Graphics g, Size szExtra, MapLineInfo line, float scaleWidth, float x, float y1, float y2, float scaleX, bool bForceSolid = false)
        {
            x += szExtra.Width;
            y1 += szExtra.Height;
            y2 += szExtra.Height;

            Pen pen = new Pen(bMask ? Global.PreMaskColor : line.Color, (line.Width + line.AltWidth) * scaleWidth);
            pen.DashStyle = bForceSolid ? DashStyle.Solid : line.Pattern;
            if (line.AltWidth == 0)
                g.DrawLine(pen, x + scaleWidth / 2, y1 + pen.Width, x + scaleWidth / 2, y2 - scaleWidth);
            else
                g.DrawLine(pen, x, y1, x, y2);
        }

        public Bitmap GetBlocksBitmap(GameNames game, Rectangle rc, EditFlags flags)
        {
            if (rc.Width < 0 || rc.Height < 0)
                return null;

            Size szExtra = DrawGridMargins;
            int iBitmapWidth = rc.Width * SquareSize.Width + szExtra.Width * 2 + 1;
            int iBitmapHeight = rc.Height * SquareSize.Height + szExtra.Height * 2 + 1;
            Bitmap bmp = new Bitmap(iBitmapWidth, iBitmapHeight);

            Graphics g = Graphics.FromImage(bmp);
            DrawAllSquares(game, g, szExtra, null, null, null, rc.Left, rc.Top, rc.Width, rc.Height, false, flags);
            g.Dispose();
            return bmp;
        }

        private bool HideHints(bool bHide, MapSquareFlags[,] HideGrid, int col, int row)
        {
            if (!bHide || HideGrid == null)
                return false;

            return col >= 0 && row >= 0 && col < HideGrid.GetLength(0) && row < HideGrid.GetLength(1) && HideGrid[col, row].HasFlag(MapSquareFlags.Visited);
        }

        private bool HideHints(bool bHide, MapSquareFlags[,] HideGrid, MapLineInfo info, int col, int row, int colCurrent, int rowCurrent)
        {
            if (!bHide || HideGrid == null || info == null)
                return false;

            int iWidth = HideGrid.GetLength(0);
            int iHeight = HideGrid.GetLength(1);

            if (col >= 0 && row >= 0 && col < iWidth && row < iHeight && HideGrid[col,row].HasFlag(MapSquareFlags.Visited))
                return true;

            return colCurrent >= 0 && rowCurrent >= 0 && colCurrent < iWidth && rowCurrent < iHeight && HideGrid[colCurrent, rowCurrent].HasFlag(MapSquareFlags.Visited);
        }

        public bool SeenNotVisited(params int[] points)
        {
            for (int i = 0; i < points.Length - 1; i += 2)
            {
                if (points[i] < 0 || points[i + 1] < 0 || points[i] >= GridWidth || points[i + 1] >= GridHeight)
                    return false;
                if (!(Grid[points[i], points[i + 1]].Seen && !Grid[points[i], points[i + 1]].Visited))
                    return false;
            }
            return true;
        }

        private void DrawAllSquares(GameNames game, Graphics gGrid, Size szExtra, MapSquareFlags[,] HideGrid, Brush brushUnvisited, Brush brushSeen,
            int iLeft = 0, int iTop = 0, int iWidth = 0, int iHeight = 0, bool bCenterOnly = false, EditFlags flags = null)
        {
            Bitmap bmpUnvisited = UseUnvisitedBitmap ? GetUnvisitedBitmap() : null;

            if (flags == null)
                flags = EditFlags.All;

            ImageAttributes opacity = null;

            // Draw an entire map in one pass, instead of asking each square to draw itself and its neighbors
            int colorClear = Properties.Settings.Default.DefaultGridBackground.ToArgb();
            int colorGrid = GridLines.Color.ToArgb();
            int iWidthGrid = GridLines.Width;
            int iPartialDrawAlpha = 80;
            DashStyle styleGrid = GridLines.Pattern;
            ColorPattern cpMask = new ColorPattern(Global.PreMaskColor, HatchStyle.Percent90);
            bool bShowGrid = (flags.Mask || flags.Grid) && Properties.Settings.Default.ShowGridLines && SquareSize.Width * 100 / 16 > Properties.Settings.Default.ShowGridLinesAboveZoom;
            bool bHideHints = Properties.Settings.Default.HideUnvisitedDottedLines;
            bool[] arrayDrawGrid = bShowGrid ? new bool[] { true, false } : new bool[] { false };

            if (flags.DrawAll && !flags.AlwaysUseAlpha)
                gGrid.Clear(Properties.Settings.Default.DefaultGridBackground);
            else
                gGrid.Clear(Color.FromArgb(0, Color.Black));
            Dictionary<ColorPattern, List<Rectangle>> backgrounds = new Dictionary<ColorPattern, List<Rectangle>>();
            float fWidth = SquareSize.Width;
            float fHeight = SquareSize.Height;
            float fScaleFG = (fWidth + fHeight) / (float)Properties.Settings.Default.ZoomLineScale / 2.0f;
            int col, row;
            if (iWidth == 0)
                iWidth = GridWidth;
            if (iHeight == 0)
                iHeight = GridHeight;
            int iRight = iLeft + iWidth;
            int iBottom = iTop + iHeight;
            int iX = iLeft;
            int iY = iTop;

            if (flags.Back)
            {
                if (bCenterOnly)
                {
                    iX = iLeft + iWidth / 2;
                    iY = iTop + iHeight / 2;
                    if (HideGrid == null || !HideGrid[iX, iY].HasFlag(MapSquareFlags.Visited))
                    {
                        ColorPattern cp = flags.Mask ? cpMask : Grid[iX, iY].Colors.BackColorPattern;
                        backgrounds.Add(cp, new List<Rectangle>(new Rectangle[] { new Rectangle(
                            (iX - iLeft) * SquareSize.Width, (iY - iTop) * SquareSize.Height, SquareSize.Width, SquareSize.Height) }));
                    }
                }
                else
                {
                    // Draw all of the square backgrounds first
                    for (row = iTop; row < iBottom; row++)
                    {
                        for (col = iLeft; col < iRight; col++)
                        {
                            if (row < 0 || col < 0 || col >= GridWidth || row >= GridHeight)
                                continue;
                            if (HideGrid == null || !HideGrid[col, row].HasFlag(MapSquareFlags.Visited)) // Background colors and patterns hidden by another pattern don't look very good
                            {
                                MapSquare square = Grid[col, row];
                                if (!flags.Mask || !square.Colors.BackColorPattern.Equals(Global.DefaultGridBackground))
                                {
                                    ColorPattern cp = flags.Mask ? cpMask : square.Colors.BackColorPattern;
                                    if (!backgrounds.ContainsKey(cp))
                                        backgrounds.Add(cp, new List<Rectangle>());
                                    backgrounds[cp].Add(new Rectangle(
                                        (col - iLeft) * SquareSize.Width, (row - iTop) * SquareSize.Height, SquareSize.Width, SquareSize.Height));
                                }
                            }
                        }
                    }
                }

                foreach (ColorPattern cp in backgrounds.Keys)
                {
                    if (flags.Mask)
                        gGrid.FillRectangles(new SolidBrush(cpMask.Color), backgrounds[cpMask].ToArray());
                    else
                    {
                        Color c = (flags.DrawAll && !flags.AlwaysUseAlpha) ? cp.Color : Color.FromArgb(iPartialDrawAlpha, cp.Color);
                        if (cp.Pattern == HatchStyle.Percent90)
                            gGrid.FillRectangles(new SolidBrush(c), backgrounds[cp].ToArray());
                        else
                            gGrid.FillRectangles(new HatchBrush(cp.Pattern, c, Properties.Settings.Default.DefaultGridBackground), backgrounds[cp].ToArray());
                    }
                }
            }

            if (flags.Inner || flags.Outer)
            {
                // Draw all of the horizontal lines
                foreach (bool bGrid in arrayDrawGrid)
                {
                    for (row = iTop; row <= iBottom; row++)
                    {
                        if (row < 0 || row > GridHeight)
                            continue;
                        int iColStart = 0;
                        MapLineInfo info = null;
                        MapLineInfo infoCurrent = null;
                        MapLineInfo infoLastSingle = null;
                        bool bSkipInner = (row > iTop + flags.OuterDepth && row < iBottom - flags.OuterDepth && !flags.Inner);
                        for (col = iLeft; col < iRight; col++)
                        {
                            if (col < 0 || col >= GridWidth)
                                continue;

                            infoCurrent = row < GridHeight ? Grid[col, row].Top : null;
                            if (row > 0)
                            {
                                bool bHidden = HideGrid != null && col < GridWidth && row < GridHeight && HideGrid[col, row].HasFlag(MapSquareFlags.Visited);
                                bool bHiddenUp = HideGrid != null && col < GridWidth && row < GridHeight && HideGrid[col, row - 1].HasFlag(MapSquareFlags.Visited);
                                bool bSeenOnly = HideGrid != null && Properties.Settings.Default.RevealSeenSquares && bHidden && 
                                    (!HideGrid[col, row].HasFlag(MapSquareFlags.Seen) || !HideGrid[col, row-1].HasFlag(MapSquareFlags.Seen));

                                MapLineInfo infoBottom = Grid[col, row - 1].Bottom;
                                if (HideHints(bHideHints, HideGrid, infoCurrent, col, row - 1, col, row))
                                {
                                    //if (Grid[col, row].HasIcon(IconName.ArrowHalf, Direction.Up) && (Grid[col, row].Visited || (SeenNotVisited(col, row, col, row - 1))))
                                    if (Grid[col, row].HasIcon(IconName.ArrowHalf, Direction.Up) && Grid[col, row].Seen && !Grid[col, row - 1].Visited)
                                    {
                                        infoCurrent.CopyFrom(m_gridLines);
                                        infoBottom.CopyFrom(m_gridLines);
                                    }
                                    //else if (Grid[col, row - 1].HasIcon(IconName.ArrowHalf, Direction.Down) && (Grid[col, row - 1].Visited || (SeenNotVisited(col, row, col, row - 1))))
                                    else if (Grid[col, row - 1].HasIcon(IconName.ArrowHalf, Direction.Down) && Grid[col, row - 1].Seen && !Grid[col, row].Visited)
                                    {
                                        infoCurrent.CopyFrom(m_gridLines);
                                        infoBottom.CopyFrom(m_gridLines);
                                    }
                                    else
                                    {
                                        infoCurrent.Pattern = DashStyle.Solid;
                                        infoBottom.Pattern = DashStyle.Solid;
                                    }
                                }

                                if (bSeenOnly && bHiddenUp)
                                {
                                    if (!Games.IsSightBlocking(game, this, GridLines, infoBottom))
                                        infoBottom.CopyFrom(GridLines);
                                    if (!Games.IsSightBlocking(game, this, GridLines, infoCurrent))
                                        infoCurrent.CopyFrom(GridLines);
                                }

                                if (infoBottom.SameColorAndPattern(infoCurrent))
                                {
                                    infoCurrent.AltWidth = infoBottom.Width;
                                    infoLastSingle = null;
                                }
                                else if (infoBottom.Color.ToArgb() != colorClear)
                                {
                                    if (infoCurrent == null)
                                        infoBottom.AltWidth = 1;   // Add one to bottom edge squares
                                    // Have to draw this segment separately, because it's a different color or pattern from the adjacent square
                                    if (!bSkipInner && infoBottom.Check(bGrid, colorGrid, iWidthGrid, styleGrid))
                                    {
                                        int iFromCol = infoBottom.Equals(infoLastSingle) ? col - 1 : col;   // Extend the previous line if it's the same
                                        DrawLineHoriz(flags.Mask && !bGrid, gGrid, szExtra, infoBottom, fScaleFG,
                                            (iFromCol - iLeft) * fWidth, (col - iLeft + 1) * fWidth, (row - iTop) * fHeight - fScaleFG, -2.0f);
                                    }
                                    infoLastSingle = infoBottom;
                                }
                            }
                            else if (infoCurrent != null)
                                infoCurrent.AltWidth = 1;   // Add one to top edge squares
                            if (info == null)
                                info = infoCurrent;
                            else if (info != null && !info.Equals(infoCurrent))
                            {
                                // Draw the previous segment
                                if (!bSkipInner && info.Check(bGrid, colorGrid, iWidthGrid, styleGrid))
                                    DrawLineHoriz(flags.Mask && !bGrid, gGrid, szExtra, info, fScaleFG, 
                                        (iColStart - iLeft) * fWidth, (col - iLeft) * fWidth, (row - iTop) * fHeight, 2.0f);
                                info = infoCurrent;
                                iColStart = col;
                            }
                        }
                        if (info != null && info.Equals(infoCurrent))
                        {
                            if (!bSkipInner && info.Check(bGrid, colorGrid, iWidthGrid, styleGrid))
                                DrawLineHoriz(flags.Mask && !bGrid, gGrid, szExtra, info, fScaleFG, 
                                    (iColStart - iLeft) * fWidth, (col - iLeft) * fWidth, (row - iTop) * fHeight, 2.0f);
                        }
                    }
                    // Draw all of the vertical lines
                    for (col = iLeft; col <= iRight; col++)
                    {
                        if (col < 0 || col > GridWidth)
                            continue;
                        int iRowStart = 0;
                        MapLineInfo info = null;
                        MapLineInfo infoCurrent = null;
                        MapLineInfo infoLastSingle = null;
                        bool bSkipInner = (col > iLeft + flags.OuterDepth && col < iRight - flags.OuterDepth && !flags.Inner);
                        for (row = iTop; row < iBottom; row++)
                        {
                            if (row < 0 || row >= GridHeight)
                                continue;
                            infoCurrent = col < GridWidth ? Grid[col, row].Left : null;
                            if (col > 0)
                            {
                                bool bHidden = HideGrid != null && col < GridWidth && row < GridHeight && HideGrid[col, row].HasFlag(MapSquareFlags.Visited);
                                bool bHiddenLeft = HideGrid != null && col < GridWidth && row < GridHeight && HideGrid[col - 1, row].HasFlag(MapSquareFlags.Visited);
                                bool bSeenOnly = HideGrid != null && Properties.Settings.Default.RevealSeenSquares && bHidden &&
                                    (!HideGrid[col, row].HasFlag(MapSquareFlags.Seen) || !HideGrid[col - 1, row].HasFlag(MapSquareFlags.Seen));

                                MapLineInfo infoRight = Grid[col - 1, row].Right;
                                if (HideHints(bHideHints, HideGrid, infoCurrent, col - 1, row, col, row))
                                {
                                    //if (Grid[col, row].HasIcon(IconName.ArrowHalf, Direction.Left) && (Grid[col, row].Visited || (SeenNotVisited(col, row, col - 1, row))))
                                    if (Grid[col, row].HasIcon(IconName.ArrowHalf, Direction.Left) && Grid[col, row].Seen && !Grid[col - 1, row].Visited)
                                    {
                                        infoCurrent.CopyFrom(m_gridLines);
                                        infoRight.CopyFrom(m_gridLines);
                                    }
                                    //else if (Grid[col - 1, row].HasIcon(IconName.ArrowHalf, Direction.Right) && (Grid[col - 1, row].Visited || (SeenNotVisited(col, row, col - 1, row))))
                                    else if (Grid[col - 1, row].HasIcon(IconName.ArrowHalf, Direction.Right) && Grid[col - 1, row].Seen && !Grid[col, row].Visited)
                                    {
                                        infoCurrent.CopyFrom(m_gridLines);
                                        infoRight.CopyFrom(m_gridLines);
                                    }
                                    else
                                    {
                                        infoCurrent.Pattern = DashStyle.Solid;
                                        infoRight.Pattern = DashStyle.Solid;
                                    }
                                }

                                if (bSeenOnly && bHiddenLeft)
                                {
                                    if (!Games.IsSightBlocking(game, this, GridLines, infoRight))
                                        infoRight.CopyFrom(GridLines);
                                    if (!Games.IsSightBlocking(game, this, GridLines, infoCurrent))
                                        infoCurrent.CopyFrom(GridLines);
                                }

                                if (infoRight.SameColorAndPattern(infoCurrent))
                                {
                                    infoCurrent.AltWidth = infoRight.Width;
                                    infoLastSingle = null;
                                }
                                else if (infoRight.Color.ToArgb() != colorClear)
                                {
                                    if (infoCurrent == null)
                                        infoRight.AltWidth = 1;   // Add one to right edge squares
                                    // Have to draw this segment separately, because it's a different color or pattern from the adjacent square
                                    if (!bSkipInner && infoRight.Check(bGrid, colorGrid, iWidthGrid, styleGrid))
                                    {
                                        int iFromRow = infoRight.Equals(infoLastSingle) ? row - 1 : row;   // Extend the previous line if it's the same
                                        DrawLineVert(flags.Mask && !bGrid, gGrid, szExtra, infoRight, fScaleFG,
                                            (col - iLeft) * fWidth - fScaleFG, (iFromRow - iTop) * fHeight, (row - iTop + 1) * fHeight, -2.0f);
                                    }
                                    infoLastSingle = infoRight;
                                }
                            }
                            else if (infoCurrent != null)   // Add one to left edge squares
                                infoCurrent.AltWidth = 1;
                            if (info == null)
                                info = infoCurrent;
                            else if (info != null && !info.Equals(infoCurrent))
                            {
                                // Draw the previous segment
                                if (!bSkipInner && info.Check(bGrid, colorGrid, iWidthGrid, styleGrid))
                                    DrawLineVert(flags.Mask && !bGrid, gGrid, szExtra, info, fScaleFG, 
                                        (col - iLeft) * fWidth, (iRowStart - iTop) * fHeight, (row - iTop) * fHeight, 2.0f);
                                info = infoCurrent;
                                iRowStart = row;
                            }
                        }
                        if (info != null && info.Equals(infoCurrent))
                        {
                            if (!bSkipInner && info.Check(bGrid, colorGrid, iWidthGrid, styleGrid))
                                DrawLineVert(flags.Mask && !bGrid, gGrid, szExtra, info, fScaleFG, 
                                    (col - iLeft) * fWidth, (iRowStart - iTop) * fHeight, (row - iTop) * fHeight, 2.0f);
                        }
                    }
                }
            }

            bool bShowUnvisitedNotes = Properties.Settings.Default.ShowUnvisitedNotes;

            opacity = Global.GetOpacityAttributes(Properties.Settings.Default.UnvisitedSquareOpacity);
            ImageAttributes opacitySeen = Global.GetOpacityAttributes(Properties.Settings.Default.SeenSquareOpacity);

            for (row = iTop; row < iBottom; row++)
            {
                for (col = iLeft; col < iRight; col++)
                {
                    if (row < 0 || col < 0 || col >= GridWidth || row >= GridHeight)
                        continue;

                    bool bVisited = HideGrid == null || !HideGrid[col, row].HasFlag(MapSquareFlags.Visited);
                    bool bSeenOnly = Properties.Settings.Default.RevealSeenSquares && (HideGrid == null || (!bVisited && !HideGrid[col, row].HasFlag(MapSquareFlags.Seen)));
                    if (HideGrid == null)
                        bSeenOnly = false;  // A null HideGrid means "show everything"

                    if (bVisited || bShowUnvisitedNotes || bSeenOnly)
                    {
                        flags.HideNorth = HideHints(bHideHints, HideGrid, col, row - 1);
                        flags.HideSouth = HideHints(bHideHints, HideGrid, col, row + 1);
                        flags.HideEast = HideHints(bHideHints, HideGrid, col + 1, row);
                        flags.HideWest = HideHints(bHideHints, HideGrid, col - 1, row);
                        flags.HideImmaterial = bSeenOnly && !bShowUnvisitedNotes;
                        Grid[col, row].DrawIconsAndNotes(flags.Mask, gGrid,
                            new Rectangle((col - iLeft) * SquareSize.Width, (row - iTop) * SquareSize.Height, SquareSize.Width, SquareSize.Height),
                            flags);
                    }

                    ImageAttributes opacityUse = bSeenOnly ? opacitySeen : opacity;
                    if (!bVisited)
                    {
                        if (bmpUnvisited == null || !UnvisitedGrid.Contains(col, row))
                            gGrid.FillRectangle(bSeenOnly ? brushSeen : brushUnvisited,
                                (col - iLeft) * fWidth + szExtra.Width, (row - iTop) * fHeight + szExtra.Height, SquareSize.Width, SquareSize.Height);
                        else
                        {
                            int iSrcWidth = UnvisitedCrop.Width / UnvisitedGrid.Width;
                            int iSrcHeight = UnvisitedCrop.Height / UnvisitedGrid.Height;

                            Bitmap bmpSquare = GetUnvisitedBitmapSection(
                                (col - UnvisitedGrid.Left) * iSrcWidth - 1,
                                (row - UnvisitedGrid.Top) * iSrcHeight - 1,
                                iSrcWidth + 2,
                                iSrcHeight + 2);   // do not dispose; comes from ImageCache

                            Rectangle rcDest = new Rectangle(
                                (col - iLeft) * SquareSize.Width + szExtra.Width,
                                (row - iTop) * SquareSize.Height + szExtra.Height,
                                SquareSize.Width, SquareSize.Height);

                            gGrid.DrawImage(bmpSquare, rcDest, 1, 1, iSrcWidth, iSrcHeight, GraphicsUnit.Pixel, opacityUse);
                        }
                    }
                }
            }

            // Show labels above the grid (including the unvisited squares)

            Rectangle rcPixels = new Rectangle(iLeft * SquareSize.Width, iTop * SquareSize.Height, (iRight - iLeft) * SquareSize.Width, (iBottom - iTop) * SquareSize.Height);
            if (Properties.Settings.Default.ShowMapLabels && Labels != null && Labels.Count > 0)
            {
                foreach (MapLabel label in Labels.Values)
                {
                    if (String.IsNullOrWhiteSpace(label.Text))
                        continue;
                    Font font = label.GetFont(SquareSize);
                    SizeF szf = gGrid.MeasureString(label.Text, font);
                    RectangleF rc = GetLabelRect(szf, label);
                    if (rc.Right < rcPixels.Left || rc.Top > rcPixels.Bottom || rc.Bottom < rcPixels.Top || rc.Left > rcPixels.Right)
                        continue;
                    if (HideGrid != null && label.AllAnchorsHaveFlag(HideGrid, MapSquareFlags.Visited) && !bShowUnvisitedNotes)
                        continue;
                    rc.Offset(-iLeft * SquareSize.Width,-iTop * SquareSize.Height);
                    if (label.BackColor.A > 0)
                        gGrid.FillRectangle(new SolidBrush(label.BackColor), rc);
                    if (label.Selected)
                        gGrid.DrawRectangle(new Pen(new HatchBrush(HatchStyle.Percent50, Color.White, Color.Black), 2.0f), rc.X, rc.Y, rc.Width, rc.Height);
                    else if (label.BorderColor.A > 0)
                        gGrid.DrawRectangle(new Pen(label.BorderColor), rc.X, rc.Y, rc.Width, rc.Height);
                    gGrid.DrawString(label.Text, font, new SolidBrush(label.ForeColor), rc.X + 2, rc.Y + 1);
                }
            }
        }

        public bool AnyDirty(Rectangle[] rects)
        {
            if (rects == null)
                return false;
            foreach (Rectangle rc in rects)
                for (int y = rc.Top; y < rc.Bottom; y++)
                    for (int x = rc.Left; x < rc.Right; x++)
                        if (AnyDirty(x, y))
                            return true;
            return false;
        }

        public bool AnyDirty(Point pt) { return AnyDirty(pt.X, pt.Y); }

        public bool AnyDirty(int col, int row)
        {
            if (!PointInGrid(col, row))
                return false;
            return Grid[col, row].AnyDirty;
        }

        public Size DrawGridMargins { get { return new Size((SquareSize.Width - 1) / 32, (SquareSize.Height - 1)/ 32); } }

        public string DirtySquaresString()
        {
            StringBuilder sb = new StringBuilder();
            for (int row = 0; row < GridHeight; row++)
                for (int col = 0; col < GridHeight; col++)
                    if (Grid[col, row].AnyDirty)
                        sb.AppendFormat("{0},{1};", col, row);
            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public Bitmap CreateImage(IMain main, DrawParams dp)
        {
            dp.MapChanged = false;
            Size szExtra = DrawGridMargins;
            int iBitmapWidth = GridWidth * SquareSize.Width + szExtra.Width * 2;
            int iBitmapHeight = GridHeight * SquareSize.Height + szExtra.Height * 2;
            int iMarginLeft = (SquareMargin.Width - SquareSize.Width) / 2;
            int iMarginTop = (SquareMargin.Height - SquareSize.Height) / 2;
            int col = 0;
            int row = 0;
            int iUnvisitedAlpha = Properties.Settings.Default.UnvisitedSquareOpacity * 255 / 100;
            Brush brushUnvisited = dp.Unvisited.GetBrush(iUnvisitedAlpha);
            int iSeenAlpha = Properties.Settings.Default.SeenSquareOpacity * 255 / 100;
            Brush brushSeen = dp.Unvisited.GetBrush(iSeenAlpha);
            bool bLegend = IsLegend;
            int iHideMargin = Properties.Settings.Default.AlwaysRevealEdges ? 1 : 0;
            bool bShowInaccessible = Properties.Settings.Default.RevealAdjacentInaccessible && !dp.IgnoreInaccessible;
            bool bHideUnvisited = Properties.Settings.Default.HideUnvisitedSquares;
            Rectangle rcHideUnvisited = new Rectangle(iHideMargin, iHideMargin, GridWidth - (2 * iHideMargin), GridHeight - (2 * iHideMargin));
            Bitmap bmpIcon = null;

            if (m_bmpGrid == null || m_bmpGrid.Width != iBitmapWidth || m_bmpGrid.Height != iBitmapHeight)
            {
                if (m_bmpGrid != null)
                    m_bmpGrid.Dispose();
                m_bmpGrid = new Bitmap(iBitmapWidth, iBitmapHeight);
                dp.ChangesOnly = false;
            }

            MapSquare square;

            MapSquareFlags[,] HideGrid = null;
            if (!dp.IgnoreHidden)
            {
                HideGrid = new MapSquareFlags[GridWidth, GridHeight];
                for (row = 0; row < GridHeight; row++)
                {
                    for (col = 0; col < GridWidth; col++)
                    {
                        bool bHideBecauseUnvisited = 
                            bHideUnvisited &&
                            !bLegend &&
                            rcHideUnvisited.Contains(col, row) &&
                            !Grid[col, row].Visited &&
                            (bShowInaccessible ? HideInaccessible(main, col, row) : true) &&
                            !(m_liYouAreHere != null && m_liYouAreHere.PrimaryCoordinates.X == col && m_liYouAreHere.PrimaryCoordinates.Y == row && CurrentMap);
                        bool bHideBecauseUnseen = !Grid[col, row].Seen;
                        HideGrid[col, row] = bHideBecauseUnvisited ? MapSquareFlags.Visited : MapSquareFlags.None;
                        if (bHideBecauseUnseen)
                            HideGrid[col, row] |= MapSquareFlags.Seen;
                    }
                }
            }

            Graphics gGrid = Graphics.FromImage(m_bmpGrid);
            Rectangle rcMinDirty = GridRectangle;
            if (!dp.ChangesOnly)
            {
                // If we are drawing the entire map, we don't need to spend the time calculating individual line segments for every square
                DrawAllSquares(main.Game, gGrid, szExtra, HideGrid, brushUnvisited, brushSeen);
            }
            else
            {
                // Draw the smallest rectangle that covers all of the dirty squares

                bool bFound = false;
                for (row = 0; row < GridHeight; row++)
                {
                    for (col = 0; col < GridWidth; col++)
                    {
                        if (AnyDirty(col, row))
                        {
                            rcMinDirty.Y = row;
                            bFound = true;
                            break;
                        }
                    }
                    if (bFound)
                        break;
                }
                if (!bFound)
                    return m_bmpGrid; // No squares were dirty; nothing to do at all

                bFound = false;
                for (row = GridHeight - 1; row >= 0; row--)
                {
                    for (col = 0; col < GridWidth; col++)
                    {
                        if (AnyDirty(col, row))
                        {
                            rcMinDirty.Height = row - rcMinDirty.Y + 1;
                            bFound = true;
                            break;
                        }
                    }
                    if (bFound)
                        break;
                }
                bFound = false;
                for (col = 0; col < GridWidth; col++)
                {
                    for (row = 0; row < GridHeight; row++)
                    {
                        if (AnyDirty(col, row))
                        {
                            rcMinDirty.X = col;
                            bFound = true;
                            break;
                        }
                    }
                    if (bFound)
                        break;
                }
                bFound = false;
                for (col = GridWidth - 1; col >= 0; col--)
                {
                    for (row = 0; row < GridHeight; row++)
                    {
                        if (AnyDirty(col, row))
                        {
                            rcMinDirty.Width = col - rcMinDirty.X + 1;
                            bFound = true;
                            break;
                        }
                    }
                    if (bFound)
                        break;
                }

                // If there are any MapLabel items anchored to a dirty square, the minimum dirty rectangle
                // must include the size of the entire label (if it does not already).

                foreach (MapLabel label in Labels.Values)
                {
                    if (AnyDirty(label.Anchors))
                    {
                        Font font = label.GetFont(SquareSize);
                        SizeF szf = gGrid.MeasureString(label.Text, font);
                        RectangleF rc = GetLabelRect(szf, label);
                        float left = rc.Left / SquareSize.Width;
                        float top = rc.Top / SquareSize.Height;
                        Rectangle rcGrid = new Rectangle((int)left, (int)top,
                            (int)Math.Round(rc.Width / SquareSize.Width + 2), (int)Math.Round(rc.Height / SquareSize.Height + 2));
                        if (rcMinDirty.X > rcGrid.Left)
                        {
                            rcMinDirty.Width += (rcMinDirty.X - rcGrid.Left);
                            rcMinDirty.Offset(rcGrid.Left - rcMinDirty.X, 0);
                        }
                        if (rcMinDirty.Y > rcGrid.Top)
                        {
                            rcMinDirty.Height += (rcMinDirty.Y - rcGrid.Top);
                            rcMinDirty.Offset(0, rcGrid.Top - rcMinDirty.Y);
                        }
                        if (rcMinDirty.Right < rcGrid.Right)
                            rcMinDirty.Width += rcGrid.Right - rcMinDirty.Right;
                        if (rcMinDirty.Bottom < rcGrid.Bottom)
                            rcMinDirty.Height += rcGrid.Bottom - rcMinDirty.Bottom;
                    }
                }

                Global.FixRange(ref rcMinDirty, 0, 0, GridWidth, GridHeight);

                Rectangle rcInflate = rcMinDirty;
                rcInflate.Inflate(1, 1);

                Size sz = new Size(SquareSize.Width * rcInflate.Width, SquareSize.Height * rcInflate.Height);
                if (m_bmpSquare == null || m_bmpSquare.Size != sz)
                    m_bmpSquare = new Bitmap(sz.Width, sz.Height);
                Graphics gSquare = dp.ChangesOnly ? Graphics.FromImage(m_bmpSquare) : null;

                DrawAllSquares(main.Game, gSquare, szExtra, HideGrid, brushUnvisited, brushSeen, rcInflate.X, rcInflate.Y, rcInflate.Width, rcInflate.Height);
                // Copy only the center squares of the bitmap to the grid
                int iRightEdge = (rcMinDirty.Right == GridWidth ? szExtra.Width * 2 + 1 : 1);
                int iBottomEdge = (rcMinDirty.Bottom == GridHeight ? szExtra.Height * 2 + 1 : 1);
                gGrid.DrawImage(m_bmpSquare,
                    new Rectangle(
                        rcMinDirty.X * SquareSize.Width,
                        rcMinDirty.Y * SquareSize.Height,
                        SquareSize.Width * rcMinDirty.Width + iRightEdge,
                        SquareSize.Height * rcMinDirty.Height + iBottomEdge),
                    new Rectangle(
                        SquareSize.Width, 
                        SquareSize.Height, 
                        SquareSize.Width * rcMinDirty.Width + iRightEdge, 
                        SquareSize.Height * rcMinDirty.Height + iBottomEdge),
                    GraphicsUnit.Pixel);

                if (gSquare != null)
                    gSquare.Dispose();
            }

            if (Global.Debug && Properties.Settings.Default.DebugShowDirtySquares && NativeMethods.IsShiftDown())
            {
                for (row = rcMinDirty.Top; row < rcMinDirty.Bottom; row++)
                {
                    for (col = rcMinDirty.Left; col < rcMinDirty.Right; col++)
                    {
                        int iX = col * SquareSize.Width;
                        int iY = row * SquareSize.Height;
                        if (AnyDirty(col, row))
                            gGrid.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Red)), iX, iY, SquareSize.Width, SquareSize.Height);
                    }
                }
                gGrid.FillRectangle(new SolidBrush(Color.FromArgb(40, Color.Red)), rcMinDirty.X * SquareSize.Width, rcMinDirty.Y * SquareSize.Height,
                    rcMinDirty.Width * SquareSize.Width, rcMinDirty.Height * SquareSize.Height);
            }

            for (row = rcMinDirty.Top; row < rcMinDirty.Bottom; row++)
            {
                for (col = rcMinDirty.Left; col < rcMinDirty.Right; col++)
                {
                    square = Grid[col, row];
                    int iX = col * SquareSize.Width;
                    int iY = row * SquareSize.Height;
                    Rectangle rcSquare = new Rectangle(iX, iY, SquareSize.Width, SquareSize.Height);

                    if (m_iconCurrent != null && m_iconCurrent.Location.X == col && m_iconCurrent.Location.Y == row)
                        MapSquare.DrawIcon(gGrid, m_iconCurrent, rcSquare, EditFlags.All, Color.FromArgb(75, m_iconCurrent.Color));

                    if (m_noteCurrent != null && m_noteCurrent.Location.X == col && m_noteCurrent.Location.Y == row)
                        MapSquare.DrawNote(gGrid, m_noteCurrent, rcSquare, EditFlags.All, Color.FromArgb(200, m_noteCurrent.Color));

                    if (square.Live && dp.ShowLive)
                    {
                        MapSquare.DrawLive(gGrid, rcSquare);
                    }

                    if (Global.PointInRects(m_anchorCursors, col, row))
                    {
                        HatchBrush brush = new HatchBrush(HatchStyle.Percent50, Color.FromArgb(128, Color.White), Color.FromArgb(128, Color.Black));
                        gGrid.FillRectangle(brush, rcSquare);
                    }

                    if (!bLegend && CurrentMap)
                    {
                        if (m_liYouAreHere != null && m_liYouAreHere.PrimaryCoordinates.X == col && m_liYouAreHere.PrimaryCoordinates.Y == row)
                        {
                            if (SetVisited(m_liYouAreHere.PrimaryCoordinates, true)) // We have obviously visited wherever we are current located
                                dp.MapChanged = true;
                            if (Directionless)
                                bmpIcon = Global.BmpYouAreHereDirectionless.NearestSize(SquareSize.Width).Clone() as Bitmap;
                            else
                            {
                                bmpIcon = Global.BmpYouAreHere.NearestSize(SquareSize.Width).Clone() as Bitmap;
                                switch (m_liYouAreHere.Facing)
                                {
                                    case Direction.Up:
                                        break;
                                    case Direction.Right:
                                        bmpIcon.RotateFlip(RotateFlipType.Rotate90FlipNone);
                                        break;
                                    case Direction.Left:
                                        bmpIcon.RotateFlip(RotateFlipType.Rotate270FlipNone);
                                        break;
                                    case Direction.Down:
                                        bmpIcon.RotateFlip(RotateFlipType.Rotate180FlipNone);
                                        break;
                                }
                            }
                            gGrid.DrawImage(bmpIcon, rcSquare, 0.0f, 0.0f, bmpIcon.Width, bmpIcon.Height, GraphicsUnit.Pixel,
                                Global.GetOpacityAttributes(Properties.Settings.Default.YouAreHereOpacity));
                            m_liYouAreHere.Drawn = true;
                        }
                        bool bShowDead = Properties.Settings.Default.ShowDeadMonsters;
                        if (m_monsters != null && Properties.Settings.Default.ShowMonstersOnMaps)
                        {
                            Point ptGrid = new Point(col, row);
                            if (m_monsters.MonsterPositions.ContainsKey(ptGrid) && (HideGrid == null || !HideGrid[col, row].HasFlag(MapSquareFlags.VisitedSeen)))
                            {
                                Proximity prox = m_liYouAreHere == null ? null : new Proximity(ptGrid, m_liYouAreHere.PrimaryCoordinates);
                                if ((prox != null && prox.Simple < 4) ||
                                    (!Properties.Settings.Default.ShowOnlyDetectableMonsters &&
                                     (HideGrid == null || !HideGrid[col, row].HasFlag(MapSquareFlags.Visited)))
                                    )
                                {
                                    MonsterPosition monster = m_monsters.MonsterPositions[ptGrid];
                                    bool bSelected = monster.Highlighted && monster.Monsters.Any(m => m.EncounterIndex != -1);
                                    if (!main.HideMonsters(monster.Monsters) && (bShowDead || !(monster.Monsters.All(m => m.Killed))))
                                    {
                                        bmpIcon = Global.GetBitmapSet(monster.Monsters.Count, monster.Monsters.Any(m => m.NPC) ? 1 : 0, bSelected).NearestSize(SquareSize.Width);
                                        if (Properties.Settings.Default.ShowActivatedMonstersIcon && monster.Monsters.Any(m => m.Active))
                                        {
                                            bmpIcon = bmpIcon.Clone() as Bitmap;
                                            Graphics g = Graphics.FromImage(bmpIcon);
                                            g.DrawImage(Global.BmpActiveMonster.NearestSize(SquareSize.Width), new Point(0, 0));
                                            g.Dispose();
                                        }
                                        gGrid.DrawImage(bmpIcon, rcSquare, 0.0f, 0.0f, bmpIcon.Width, bmpIcon.Height, GraphicsUnit.Pixel,
                                            Global.GetOpacityAttributes(Properties.Settings.Default.MonsterOpacity));
                                    }
                                }
                            }
                            m_monsters.Drawn = true;
                        }
                        if (m_items != null && Properties.Settings.Default.ShowItemIcons)
                        {
                            Point ptGrid = new Point(col, row);
                            if (m_items.ItemPositions.ContainsKey(ptGrid) && (HideGrid == null || !HideGrid[col, row].HasFlag(MapSquareFlags.VisitedSeen)))
                            {
                                ItemPosition item = m_items.ItemPositions[ptGrid];
                                bmpIcon = Global.GetItemBitmapSet(item.Items.Count).NearestSize(SquareSize.Width);
                                gGrid.DrawImage(bmpIcon, rcSquare, 0.0f, 0.0f, bmpIcon.Width, bmpIcon.Height, GraphicsUnit.Pixel,
                                    Global.GetOpacityAttributes(Properties.Settings.Default.MonsterOpacity));
                            }
                            m_items.Drawn = true;
                        }

                        if (m_activeSquares != null && Properties.Settings.Default.ShowActiveSquares)
                        {
                            if ( m_activeSquares.IsActiveInternal(col, row, Properties.Settings.Default.ShowActiveEncountersOnly) &&
                                (!Properties.Settings.Default.HideUnvisitedSquares || Grid[col, row].Visited) )
                            {
                                bmpIcon = Global.BmpActiveSquare.NearestSize(SquareSize.Width);
                                gGrid.DrawImage(bmpIcon, rcSquare, 0.0f, 0.0f, bmpIcon.Width, bmpIcon.Height, GraphicsUnit.Pixel);
                            }
                        }
                    }
                    SetDirty(col, row, DirtyType.All, false);
                }
            }

            if (dp.DrawCursor)
            {
                Size szMargin = new Size(SquareSize.Width / 3, SquareSize.Height / 3);
                Rectangle rc = new Rectangle(
                    m_ptCursor.X * SquareSize.Width - szMargin.Width + 1,
                    m_ptCursor.Y * SquareSize.Height - szMargin.Height + 1,
                    SquareSize.Width + szMargin.Width * 2,
                    SquareSize.Height + szMargin.Height * 2);
                gGrid.DrawIcon(new Icon(Properties.Resources.iconCursor, rc.Width, rc.Height), rc);

                for (int i = Cursor.X - 1; i <= Cursor.X + 1; i++)
                {
                    for (int j = Cursor.Y - 1; j <= Cursor.Y + 1; j++)
                    {
                        if (!PointInGrid(i, j))
                            continue;
                        if (i == Cursor.X && j == Cursor.Y)
                            continue;   // The cursor doesn't actually touch the indicated square
                        SetDirty(i, j);
                    }
                }
            }

            gGrid.Dispose();

            return m_bmpGrid;
        }

        public ActiveSquares GetActiveSquares() { return m_activeSquares; }

        public void SetActiveSquaresDrawn(bool bDrawn = true)
        {
            if (m_activeSquares != null)
                m_activeSquares.Drawn = bDrawn;
        }

        public RectangleF GetLabelRect(SizeF szf, MapLabel label)
        {
            RectangleF rc = new RectangleF(label.Location.X * SquareSize.Width, label.Location.Y * SquareSize.Height, szf.Width, szf.Height);
            rc.Inflate(1, 1);
            return rc;
        }

        public void SetAdjacentDirty(Point pt)
        {
            if (PointInGrid(pt.X - 1, pt.Y))
                SetDirty(pt.X - 1, pt.Y, DirtyType.Right);
            if (PointInGrid(pt.X + 1, pt.Y))
                SetDirty(pt.X + 1, pt.Y, DirtyType.Left);
            if (PointInGrid(pt.X, pt.Y - 1))
                SetDirty(pt.X, pt.Y - 1, DirtyType.Down);
            if (PointInGrid(pt.X, pt.Y + 1))
                SetDirty(pt.X, pt.Y + 1, DirtyType.Up);
        }

        public bool SetVisited(Point pt, bool bVisited)
        {
            if (!PointInGrid(pt))
                return false;

            if (Grid[pt.X, pt.Y].SetVisited(bVisited))
            {
                SetDirty(pt);
                if (Properties.Settings.Default.HideUnvisitedDottedLines)
                    SetAdjacentDirty(pt);
                return true;
            }
            return false;
        }

        public bool SetSeen(Point pt, bool bSeen)
        {
            if (!PointInGrid(pt))
                return false;

            if (Grid[pt.X, pt.Y].SetSeen(bSeen))
            {
                SetDirty(pt);
                // Changing whether a square has been seen can change whether a line is visible on adjacent squares
                if (Properties.Settings.Default.HideUnvisitedDottedLines)
                    SetAdjacentDirty(pt);
                return true;
            }

            return false;
        }

        public Dictionary<Point, MapSquare> GetFillList(int iX, int iY, UndoList undo = null, bool bObeySolidLines = true)
        {
            // Return a list of contiguous squares that are the same color as the given point
            Dictionary<Point, MapSquare> dict = new Dictionary<Point, MapSquare>();
            if (!PointInGrid(iX, iY))
                return dict;
            AddToFillList(dict, iX, iY, Grid[iX, iY].Colors.BackColorPattern, undo, bObeySolidLines);
            return dict;
        }

        private void AddLineIfDifferentBacks(Point pt1, Direction dir, MapLineInfo info, UndoList undo = null)
        {
            if (!PointInGrid(pt1))
                return;
            Point pt2 = Global.OffsetPoint(pt1, dir);
            if (!PointInGrid(pt2))
            {
                if (undo != null)
                    undo.Add(UndoSquare(pt1));
                Grid[pt1.X, pt1.Y].Line(dir, info);
            }
            else if (!Grid[pt1.X, pt1.Y].Colors.BackColorPattern.Equals(Grid[pt2.X, pt2.Y].Colors.BackColorPattern))
            {
                if (undo != null)
                {
                    undo.Add(UndoSquare(pt1));
                    undo.Add(UndoSquare(pt2));
                }
                Grid[pt1.X, pt1.Y].Line(dir, info);
                Grid[pt2.X, pt2.Y].Line(Global.Opposite(dir), info);
            }
        }

        public void SurroundLines(int iX, int iY, MapLineInfo info, UndoList undo = null, bool bObeySolidLines = true)
        {
            // Return a list of contiguous squares that are the same color as the given point
            if (!PointInGrid(iX, iY))
                return;
            Dictionary<Point, MapSquare> dict = new Dictionary<Point, MapSquare>();
            AddToFillList(dict, iX, iY, Grid[iX, iY].Colors.BackColorPattern, null, bObeySolidLines);
            foreach (Point pt in dict.Keys)
            {
                AddLineIfDifferentBacks(pt, Direction.Up, info, undo);
                AddLineIfDifferentBacks(pt, Direction.Down, info, undo);
                AddLineIfDifferentBacks(pt, Direction.Left, info, undo);
                AddLineIfDifferentBacks(pt, Direction.Right, info, undo);
            }
        }

        private void AddToFillList(Dictionary<Point, MapSquare> dict, int iX, int iY, ColorPattern cp, UndoList undo = null, bool bObeySolidLines = true)
        {
            if (!PointInGrid(iX, iY))
                return;
            Point pt = new Point(iX, iY);
            if (!Grid[iX, iY].Colors.BackColorPattern.Equals(cp))
                return;
            if (dict.ContainsKey(new Point(iX, iY)))
                return;

            if (undo != null)
                undo.Add(UndoSquare(pt));
            dict.Add(pt, Grid[iX, iY]);

            if (!bObeySolidLines || Grid[iX, iY].Top.Equals(GridLines) || Grid[iX, iY].Top.Pattern != DashStyle.Solid)
                AddToFillList(dict, iX, iY - 1, cp, undo);
            if (!bObeySolidLines || Grid[iX, iY].Bottom.Equals(GridLines) || Grid[iX, iY].Bottom.Pattern != DashStyle.Solid)
                AddToFillList(dict, iX, iY + 1, cp, undo);
            if (!bObeySolidLines || Grid[iX, iY].Left.Equals(GridLines) || Grid[iX, iY].Left.Pattern != DashStyle.Solid)
                AddToFillList(dict, iX - 1, iY, cp, undo);
            if (!bObeySolidLines || Grid[iX, iY].Right.Equals(GridLines) || Grid[iX, iY].Right.Pattern != DashStyle.Solid)
                AddToFillList(dict, iX + 1, iY, cp, undo);
        }

        public bool ChangeSize(int iChange, bool bFixedSizesOnly = false)
        {
            int iOriginalWidth = SquareSize.Width;
            if (bFixedSizesOnly)
            {
                int iNewSize = iOriginalWidth;
                // Change only between 50/100/150/200/300 percent sizes
                if (iChange > 0)
                {
                    if (iOriginalWidth < 8)
                        iNewSize = 8;
                    else if (iOriginalWidth < 16)
                        iNewSize = 16;
                    else if (iOriginalWidth < 24)
                        iNewSize = 24;
                    else if (iOriginalWidth < 32)
                        iNewSize = 32;
                    else if (iOriginalWidth < 48)
                        iNewSize = 48;
                }
                else
                {
                    if (iOriginalWidth > 48)
                        iNewSize = 48;
                    else if (iOriginalWidth > 32)
                        iNewSize = 32;
                    else if (iOriginalWidth > 24)
                        iNewSize = 24;
                    else if (iOriginalWidth > 16)
                        iNewSize = 16;
                    else if (iOriginalWidth > 8)
                        iNewSize = 8;
                }

                SquareSize.Width = iNewSize;
                if (SquareSize.Height != SquareSize.Width)
                {
                    SquareSize.Height = SquareSize.Width;
                    return true;
                }

                return iNewSize != iOriginalWidth;
            }

            int iModifier = SquareSize.Width / Properties.Settings.Default.ZoomScale + 1;

            SquareSize.Width += (iChange * iModifier);
            SquareSize.Height += (iChange * iModifier);

            Global.FixRange(ref SquareSize, Properties.Settings.Default.MinSquareSize.Width, Properties.Settings.Default.MaxSquareSize.Width,
                                            Properties.Settings.Default.MinSquareSize.Height, Properties.Settings.Default.MaxSquareSize.Height);

            // Change square sizes that are almost 16/24/32/48 to be exactly those value (the icons display better at those precise sizes)
            if (SquareSize.Width >= 15 && SquareSize.Width <= 17)
                SquareSize.Width = 16;
            if (SquareSize.Width >= 23 && SquareSize.Width <= 25)
                SquareSize.Width = 24;
            if (SquareSize.Width >= 30 && SquareSize.Width <= 34)
                SquareSize.Width = 32;
            if (SquareSize.Width >= 45 && SquareSize.Width <= 51)
                SquareSize.Width = 48;

            if (SquareSize.Height != SquareSize.Width)
                SquareSize.Height = SquareSize.Width;

            return SquareSize.Width != iOriginalWidth;
        }

        public void SetSquareSize(Size sz)
        {
            SquareSize = sz;

            Global.FixRange(ref SquareSize, Properties.Settings.Default.MinSquareSize.Width, Properties.Settings.Default.MaxSquareSize.Width,
                                            Properties.Settings.Default.MinSquareSize.Height, Properties.Settings.Default.MaxSquareSize.Height);

            if (SquareSize.Height != SquareSize.Width)
                SquareSize.Height = SquareSize.Width;
        }

        public bool PointVisited(Point pt)
        {
            Global.FixRange(ref pt, 0, GridWidth - 1, 0, GridHeight - 1);
            return Grid[pt.X, pt.Y].Visited;
        }

        public MapSquare GetSquareAtPoint(Point pt)
        {
            Point ptSquare = GetSquareLocationAtPoint(pt);
            if (!PointInGrid(ptSquare))
                return null;
            return Grid[ptSquare.X, ptSquare.Y];
        }

        public MapSquare GetSquareAtGridPoint(Point ptSquare)
        {
            if (!PointInGrid(ptSquare))
                return null;
            return Grid[ptSquare.X, ptSquare.Y];
        }

        public MapSquare GetSquareAtGridPoint(Point ptSquare, int iOffsetX, int iOffsetY)
        {
            ptSquare.Offset(iOffsetX, iOffsetY);
            if (!PointInGrid(ptSquare))
                return null;
            return Grid[ptSquare.X, ptSquare.Y];
        }

        public Point GetSquareLocationAtPoint(Point pt, bool bFixRange = true)
        {
            int iX = pt.X / SquareSize.Width;
            int iY = pt.Y / SquareSize.Height;
            if (bFixRange)
            {
                Global.FixRange(ref iX, 0, GridWidth - 1);
                Global.FixRange(ref iY, 0, GridHeight - 1);
            }
            return new Point(iX, iY);
        }

        public Point GetVertexAtPoint(Point pt)
        {
            int iX = (pt.X + (SquareSize.Width / 2)) / SquareSize.Width;
            int iY = (pt.Y + (SquareSize.Height / 2)) / SquareSize.Height;
            Global.FixRange(ref iX, 0, GridWidth);
            Global.FixRange(ref iY, 0, GridHeight);
            return new Point(iX, iY);
        }

        public Point GetPixelForSquareCenter(Point pt)
        {
            return new Point(pt.X * SquareSize.Width + (SquareSize.Width / 2), pt.Y * SquareSize.Height + (SquareSize.Height / 2));
        }

        public Point GetPixelForSquareOrigin(Point pt)
        {
            return new Point(pt.X * SquareSize.Width, pt.Y * SquareSize.Height);
        }

        public void Serialize(Stream stream)
        {
            for (int row = 0; row < GridHeight; row++)
                for (int col = 0; col < GridWidth; col++)
                    Grid[col, row].Serialize(stream);
        }

        public void ToggleLineCurrent(Direction dir, DrawColor dc)
        {
            ToggleLinesCurrent(new Direction[] { dir }, dc, dc.width, false);
        }

        public void ToggleDoubleLineCurrent(Direction dir, DrawColor dc)
        {
            ToggleLinesCurrent(new Direction[] { dir }, dc, dc.width, true);
        }

        public void ToggleLinesCurrent(Direction[] dirs, DrawColor dc)
        {
            ToggleLinesCurrent(dirs, dc, dc.width, false);
        }

        public void ToggleDoubleLinesCurrent(Direction[] dirs, DrawColor dc)
        {
            ToggleLinesCurrent(dirs, dc, dc.width, true);
        }

        public void ToggleLinesCurrent(Direction[] dirs, DrawColor dc, byte iWidth, bool bDouble)
        {
            // Draws lines if any of the specified directions is missing one, otherwise removes them all
            bool bErase = true;
            foreach (Direction dir in dirs)
            {
                if (!Grid[Cursor.X, Cursor.Y].HasLine(dir, GridLines))
                {
                    bErase = false;
                    break;
                }
                if (bDouble)
                {
                    Point ptOpposite = Global.OffsetPoint(Cursor, dir);
                    if (PointInGrid(ptOpposite) && !Grid[ptOpposite.X, ptOpposite.Y].HasLine(Global.Opposite(dir), GridLines))
                    {
                        bErase = false;
                        break;
                    }
                }
            }

            foreach (Direction dir in dirs)
            {
                if (bErase)
                    Grid[Cursor.X, Cursor.Y].Line(dir, GridLines);
                else
                    Grid[Cursor.X, Cursor.Y].Line(dir, dc.LineStyle, iWidth);

                if (bDouble)
                {
                    Point ptOpposite = Global.OffsetPoint(Cursor, dir);
                    if (PointInGrid(ptOpposite))
                    {
                        if (bErase)
                            Grid[ptOpposite.X, ptOpposite.Y].Line(Global.Opposite(dir), GridLines);
                        else
                            Grid[ptOpposite.X, ptOpposite.Y].Line(Global.Opposite(dir), dc.LineStyle, iWidth);
                    }
                }
            }
        }

        public void BlockPair(bool bDraw, UndoList undoBlocks, SurroundingSquares surr, DrawColor dc, Direction dir)
        {
            if (bDraw)
            {
                BlockLine(undoBlocks, surr.LocationOf(dir), dc, Global.Opposite(dir),
                    !BlockLine(undoBlocks, surr.Location, dc, dir));
            }
            else
            {
                BlockRemoveLine(undoBlocks, surr.LocationOf(dir), Global.Opposite(dir),
                    !BlockRemoveLine(undoBlocks, surr.Location, dir));
            }
        }

        public bool BlockLine(UndoList undoBlocks, int iX, int iY, DrawColor dc, Direction dir, bool bObeyUnvisited = true)
        {
            return BlockLine(undoBlocks, new Point(iX, iY), dc, dir, bObeyUnvisited);
        }

        public bool BlockLine(UndoList undoBlocks, Point pt, DrawColor dc, Direction dir, bool bObeyUnvisited = true)
        {
            if (!PointInGrid(pt))
                return false;

            MapSquare square = Grid[pt.X, pt.Y];

            if (bObeyUnvisited)
            {
                if (Properties.Settings.Default.HideUnvisitedSquares && !IsLegend)
                    if (!square.Visited)
                        return false;
            }

            AddUndoBlock(undoBlocks, square, pt);
            square.Line(dir, dc.LineStyle);
            return true;
        }

        public bool BlockRemoveLine(UndoList undoBlocks, int iX, int iY, Direction dir, bool bObeyUnvisited = true)
        {
            return BlockRemoveLine(undoBlocks, new Point(iX, iY), dir, bObeyUnvisited);
        }

        public bool BlockRemoveLine(UndoList undoBlocks, Point pt, Direction dir, bool bObeyUnvisited = true)
        {
            if (!PointInGrid(pt))
                return false;
            MapSquare square = Grid[pt.X, pt.Y];

            if (bObeyUnvisited)
            {
                if (Properties.Settings.Default.HideUnvisitedSquares && !IsLegend)
                    if (!square.Visited)
                        return false;
            }

            AddUndoBlock(undoBlocks, square, pt);
            square.Line(dir, GridLines);
            return true;
        }

        public void DrawEdge(UndoList undoBlocks, Point pt1, Point pt2, DrawColor dc)
        {
            int iX = pt1.X;
            int iY = pt1.Y;
            bool bDrawn = false;

            while (iX < pt2.X)
            {
                bDrawn = false;
                if (iY < GridHeight)
                    bDrawn = BlockLine(undoBlocks, iX, iY, dc, Direction.Up);
                if (iY > 0)
                    if (BlockLine(undoBlocks, iX, iY - 1,dc, Direction.Down, !bDrawn) && !bDrawn)
                        BlockLine(undoBlocks, iX, iY, dc, Direction.Up, false); // Force the other side to be drawn even if unvisited
                iX++;
            }

            while (iX > pt2.X)
            {
                if (iX > 0)
                {
                    bDrawn = false;
                    if (iY < GridHeight)
                        bDrawn = BlockLine(undoBlocks, iX - 1, iY,dc, Direction.Up);
                    if (iY > 0)
                        if (BlockLine(undoBlocks, iX - 1, iY - 1,dc, Direction.Down, !bDrawn) && !bDrawn)
                            BlockLine(undoBlocks, iX - 1, iY, dc, Direction.Up, false); // Force the other side to be drawn even if unvisited
                }
                iX--;
            }

            while (iY < pt2.Y)
            {
                bDrawn = false;
                if (iX < GridWidth)
                    bDrawn = BlockLine(undoBlocks, iX, iY, dc, Direction.Left);
                if (iX > 0)
                    if (BlockLine(undoBlocks, iX - 1, iY,dc, Direction.Right, !bDrawn) && !bDrawn)
                        BlockLine(undoBlocks, iX, iY, dc, Direction.Left, false); // Force the other side to be drawn even if unvisited
                iY++;
            }

            while (iY > pt2.Y)
            {
                if (iY > 0)
                {
                    bDrawn = false;
                    if (iX < GridWidth)
                        bDrawn = BlockLine(undoBlocks, iX, iY - 1, dc, Direction.Left);
                    if (iX > 0)
                        if (BlockLine(undoBlocks, iX - 1, iY - 1,dc, Direction.Right, !bDrawn) && !bDrawn)
                            BlockLine(undoBlocks, iX, iY - 1, dc, Direction.Left, false); // Force the other side to be drawn even if unvisited
                }
                iY--;
            }
        }

        public bool HybridLines(UndoList undoBlocks, int iX, int iY, DrawColor dc, DrawMode mode)
        {
            if (mode == DrawMode.None)
                return false;

            // Add or remove lines around the area that includes this block
            // Since this is just one block, we only need to check the immediate surroundings
            // (If hybrid mode was not enabled for other parts of the area,
            // this will not retro-fit lines to them)

            bool bUnvisited = false;
            if (Properties.Settings.Default.HideUnvisitedSquares && !IsLegend)
            {
                if (!Grid[iX, iY].Visited)
                    bUnvisited = true;
                else if (iY > 0 && !Grid[iX, iY - 1].Visited)
                    bUnvisited = true;
                else if (iY < GridHeight-1 && !Grid[iX, iY + 1].Visited)
                    bUnvisited = true;
                if (iX > 0 && !Grid[iX-1, iY].Visited)
                    bUnvisited = true;
                else if (iX < GridWidth - 1 && !Grid[iX+1, iY].Visited)
                    bUnvisited = true;
            }

            if (bUnvisited)
                return false;

            bool bErase = mode == DrawMode.Erase;
            SurroundingSquares surr = GetSurrounding(iX, iY);
            BlockPair(bErase ^ surr.UpEmpty, undoBlocks, surr, dc, Direction.Up);
            BlockPair(bErase ^ surr.LeftEmpty, undoBlocks, surr, dc, Direction.Left);
            BlockPair(bErase ^ surr.RightEmpty, undoBlocks, surr, dc, Direction.Right);
            BlockPair(bErase ^ surr.DownEmpty, undoBlocks, surr, dc, Direction.Down);

            return true;
        }

        public void LineOfBlocks(UndoList undoBlocks, Point pt1, Point pt2, DrawColor dc, DrawMode drawLines, DrawColor dcLines)
        {
            int iX = pt1.X;
            int iY = pt1.Y;
            int iDirX = Math.Sign(pt2.X - iX);
            int iDirY = Math.Sign(pt2.Y - iY);

            while (iX != pt2.X || iY != pt2.Y)
            {
                if (iX != pt2.X)
                {
                    iX += iDirX;
                    if (PointInGrid(iX, iY) && (Grid[iX, iY].Visited || !Properties.Settings.Default.HideUnvisitedSquares || IsLegend))
                    {
                        AddUndoBlock(undoBlocks, Grid[iX, iY], new Point(iX, iY));
                        Grid[iX, iY].Fill(dc);
                        HybridLines(undoBlocks, iX, iY, dcLines, drawLines);
                    }
                }
                if (iY != pt2.Y)
                {
                    iY += iDirY;
                    if (PointInGrid(iX, iY) && (Grid[iX, iY].Visited || !Properties.Settings.Default.HideUnvisitedSquares || IsLegend))
                    {
                        AddUndoBlock(undoBlocks, Grid[iX, iY], new Point(iX, iY));
                        Grid[iX, iY].Fill(dc);
                        HybridLines(undoBlocks, iX, iY, dcLines, drawLines);
                    }
                }
            }
        }

        public void Expand(ExpandSizes sizes)
        {
            ClearRedo();
            AddUndoSheet();
            // Add columns and/or rows to the grid while leaving objects in place
            int iNewWidth = GridWidth + sizes.WidthDelta;
            int iNewHeight = GridHeight + sizes.HeightDelta;

            MapSquare[,] mapNew = new MapSquare[iNewWidth, iNewHeight];

            for (int row = Math.Max(0, -sizes.Top); row < GridHeight + Math.Min(0, sizes.Bottom); row++)
            {
                for (int col = Math.Max(0, -sizes.Left); col < GridWidth + Math.Min(0, sizes.Right); col++)
                {
                    mapNew[col + sizes.Left, row + sizes.Top] = Grid[col, row];
                    mapNew[col + sizes.Left, row + sizes.Top].Offset(sizes.Left, sizes.Top);
                }
            }

            Labels.Offset(sizes.Left, sizes.Top);

            Grid = mapNew;
            CreateEmptySquares();

            // Double any lines that used to be edges and are now in the main map area
            for (int iCol = sizes.Left; iCol < GridWidth - sizes.Right; iCol++)
            {
                if (sizes.Top > 0)
                {
                    MapLineInfo info = Grid[iCol, sizes.Top].Top;
                    if (!info.Equals(GridLines))
                        Grid[iCol, sizes.Top - 1].Line(Direction.Down, info);
                }
                if (sizes.Bottom > 0)
                {
                    MapLineInfo info = Grid[iCol, GridHeight - sizes.Bottom - 1].Bottom;
                    if (!info.Equals(GridLines))
                        Grid[iCol, GridHeight - sizes.Bottom].Line(Direction.Up, info);
                }
            }
            for (int iRow = sizes.Top; iRow < GridHeight - sizes.Bottom; iRow++)
            {
                if (sizes.Left > 0)
                {
                    MapLineInfo info = Grid[sizes.Left, iRow].Left;
                    if (!info.Equals(GridLines))
                        Grid[sizes.Left - 1, iRow].Line(Direction.Right, info);
                }
                if (sizes.Right > 0)
                {
                    MapLineInfo info = Grid[GridWidth - sizes.Right - 1, iRow].Right;
                    if (!info.Equals(GridLines))
                        Grid[GridWidth - sizes.Right, iRow].Line(Direction.Left, info);
                }
            }

            if (m_liYouAreHere != null && m_liYouAreHere.PrimaryCoordinates != Global.NullPoint)
                m_liYouAreHere.PrimaryCoordinates = new Point(m_liYouAreHere.PrimaryCoordinates.X + sizes.Left, m_liYouAreHere.PrimaryCoordinates.Y + sizes.Top);

        }

        private void CreateEmptySquares()
        {
            for (int row = 0; row < GridHeight; row++)
                for (int col = 0; col < GridWidth; col++)
                    if (Grid[col, row] == null)
                        Grid[col, row] = new MapSquare(GridLines);
        }

        public bool ToggleIcon(Point pt, MapIcon icon)
        {
            ClearRedo();
            AddUndoIcons();

            MapSquare square = Grid[pt.X, pt.Y];
            square.SetDirty(DirtyType.Back);

            foreach(MapIcon di in square.Icons)
            {
                if (di.Equals(icon, true))
                {
                    if (di.Color.ToArgb() != icon.Color.ToArgb())
                        di.Color = icon.Color;
                    else
                        square.Icons.Remove(di);
                    return false;
                }
            }

            MapIcon iconNew = new MapIcon(icon);
            iconNew.Location = pt;
            square.Icons.Add(iconNew);
            return false;
        }

        public void RemoveIcons(Point pt)
        {
            MapSquare square = Grid[pt.X, pt.Y];

            if (square.Icons == null)
                return;

            if (square.Icons.Count == 0)
                return;

            ClearRedo();
            AddUndoIcons();

            square.Icons = new List<MapIcon>();
            square.SetDirty(DirtyType.Back);
        }

        public bool ToggleIcon(MapIcon icon)
        {
            return ToggleIcon(Cursor, icon);
        }

        public ExpandSizes CropRectangle(Rectangle rc)
        {
            return CropSpecific(rc.Left, rc.Top, rc.Right-1, rc.Bottom-1);
        }

        public ExpandSizes CropSpecific(int iCropLeft, int iCropTop, int iCropRight, int iCropBottom)
        {
            ClearRedo();
            AddUndoSheet();

            ExpandSizes sizes = new ExpandSizes(-iCropTop, iCropBottom - GridHeight + 1, -iCropLeft, iCropRight - GridWidth + 1);

            // Remove columns and/or rows from the grid while leaving objects in place
            int iNewWidth = GridWidth + sizes.WidthDelta;
            int iNewHeight = GridHeight + sizes.HeightDelta;

            if (iNewWidth < 1)
                iNewWidth = 1;

            if (iNewHeight < 1)
                iNewHeight = 1;

            MapSquare[,] mapNew = new MapSquare[iNewWidth, iNewHeight];

            for (int row = Math.Max(0, -sizes.Top); row < GridHeight + Math.Min(0, sizes.Bottom); row++)
            {
                for (int col = Math.Max(0, -sizes.Left); col < GridWidth + Math.Min(0, sizes.Right); col++)
                {
                    mapNew[col + sizes.Left, row + sizes.Top] = Grid[col, row];
                    mapNew[col + sizes.Left, row + sizes.Top].Offset(sizes.Left, sizes.Top);
                }
            }

            Labels.Offset(sizes.Left, sizes.Top);

            Grid = mapNew;

            GridStreamChanged = true;

            return sizes;
        }

        public ExpandSizes CropUnusedSquares()
        {
            // Returns the values that would be necessary to undo the crop
            int iCropTop = -1;

            for (int row = 0; row < GridHeight; row++)
            {
                for (int col = 0; col < GridWidth; col++)
                {
                    if (!Grid[col, row].IsUnused(GridLines))
                    {
                        iCropTop = row;
                        break;
                    }
                }
                if (iCropTop != -1)
                    break;
            }

            int iCropLeft = -1;

            for (int col = 0; col < GridWidth; col++)
            {
                for (int row = 0; row < GridHeight; row++)
                {
                    if (!Grid[col, row].IsUnused(GridLines))
                    {
                        iCropLeft = col;
                        break;
                    }
                }
                if (iCropLeft != -1)
                    break;
            }

            int iCropRight = -1;

            for (int col = GridWidth-1; col >= 0; col--)
            {
                for (int row = 0; row < GridHeight; row++)
                {
                    if (!Grid[col, row].IsUnused(GridLines))
                    {
                        iCropRight = col;
                        break;
                    }
                }
                if (iCropRight != -1)
                    break;
            }

            int iCropBottom = -1;

            for (int row = GridHeight - 1; row >= 0; row--)
            {
                for (int col = 0; col < GridWidth; col++)
                {
                    if (!Grid[col, row].IsUnused(GridLines))
                    {
                        iCropBottom = row;
                        break;
                    }
                }
                if (iCropBottom != -1)
                    break;
            }

            if (iCropTop == -1 && iCropBottom == -1 && iCropLeft == -1 && iCropRight == -1)
                return new ExpandSizes(0,0,0,0);     // Don't crop to nothing

            return CropSpecific(iCropLeft, iCropTop, iCropRight, iCropBottom);
        }

        public bool RectInGrid(Rectangle rc)
        {
            if (rc.Left < 0 || rc.Top < 0 || rc.Right >= Grid.GetLength(1) || rc.Bottom >= Grid.GetLength(0))
                return false;

            return true;
        }

        public void DeleteBlocks(UndoList undoBlocks, Rectangle rcBlock, bool bAddOtherUndo = true)
        {
            if (undoBlocks != null && bAddOtherUndo)
            {
                undoBlocks.Other.Add(new UndoItem(GetAllNotesCopy()));
                undoBlocks.Other.Add(new UndoItem(GetAllIconsCopy()));
                undoBlocks.Other.Add(new UndoItem(GetAllLabelsCopy()));
            }
            EditFlags flags = Global.EditSettings;
            if (flags.Outer)
            {
                for (int row = rcBlock.Top; row < rcBlock.Bottom; row++)
                {
                    if (PointInGrid(rcBlock.Left-1, row))
                    {
                        AddUndoBlock(undoBlocks, Grid[rcBlock.Left-1, row], new Point(rcBlock.Left-1, row));
                        Grid[rcBlock.Left-1, row].Right = GridLines;
                        SetDirty(rcBlock.Left-1, row, DirtyType.Right);
                    }
                    if (PointInGrid(rcBlock.Right, row))
                    {
                        AddUndoBlock(undoBlocks, Grid[rcBlock.Right, row], new Point(rcBlock.Right, row));
                        Grid[rcBlock.Right, row].Left = GridLines;
                        SetDirty(rcBlock.Right, row, DirtyType.Left);
                    }
                }
                for (int col = rcBlock.Left; col < rcBlock.Right; col++)
                {
                    if (PointInGrid(col, rcBlock.Top-1))
                    {
                        AddUndoBlock(undoBlocks, Grid[col, rcBlock.Top-1], new Point(col, rcBlock.Top-1));
                        Grid[col, rcBlock.Top-1].Bottom = GridLines;
                        SetDirty(col, rcBlock.Top - 1, DirtyType.Down);
                    }
                    if (PointInGrid(col, rcBlock.Bottom))
                    {
                        AddUndoBlock(undoBlocks, Grid[col, rcBlock.Bottom], new Point(col, rcBlock.Bottom));
                        Grid[col, rcBlock.Bottom].Top = GridLines;
                        SetDirty(col, rcBlock.Bottom, DirtyType.Up);
                    }
                }
            }
            for (int row = rcBlock.Top; row < rcBlock.Bottom; row++)
            {
                for (int col = rcBlock.Left; col < rcBlock.Right; col++)
                {
                    if (!PointInGrid(col, row))
                        continue;

                    if (flags.Labels && Labels.AnyAnchorsAt(col, row))
                    {
                        foreach (MapLabel label in Labels.LabelsAtAnchor(col, row))
                            SetLabelSquaresDirty(label);
                        Labels.RemoveAllAtAnchor(col, row);
                    }

                    MapSquare square = Grid[col, row];
                    AddUndoBlock(undoBlocks, square, new Point(col, row));
                    if (flags.Back)
                    {
                        square.Colors.BackColorPattern = Global.DefaultGridBackground;
                        square.SetDirty(DirtyType.Back);
                    }
                    if (flags.Inner)
                    {
                        square.Top = GridLines;
                        square.Left = GridLines;
                        square.Right = GridLines;
                        square.Bottom = GridLines;
                        square.SetDirty(DirtyType.Lines);
                    }
                    if (flags.Notes)
                    {
                        square.Note = null;
                        square.SetDirty(DirtyType.Back);
                    }
                    if (flags.Icons)
                    {
                        square.Icons = new List<MapIcon>();
                        square.SetDirty(DirtyType.Back);
                    }
                }
            }
        }

        private void ManipulateBlocks(UndoList undoBlocks, Rectangle rcSource, Point ptDest, bool bDelete, EditFlags flags, MapSquare[,] grid = null, bool bAddOtherUndo = true)
        {
            if (rcSource.Location == ptDest && grid == null)
            {
                // Don't trash the map if the user tries to copy a selection to itself, but we do need to mark the area as dirty
                // for display purposes;
                for (int row = rcSource.Y - 1; row <= rcSource.Bottom; row++)
                    for (int col = rcSource.X - 1; col <= rcSource.Right; col++)
                        if (PointInGrid(col, row))
                            SetDirty(col, row);
                return;
            }

            // Add undo blocks for the union of the source and target rectangles (including a one-square border around each)
            if (grid == null)
            {
                for (int row = rcSource.Y - 1; row <= rcSource.Bottom; row++)
                    for (int col = rcSource.X - 1; col <= rcSource.Right; col++)
                        if (PointInGrid(col, row))
                            AddUndoBlock(undoBlocks, Grid[col, row], new Point(col, row));
            }

            for (int row = ptDest.Y - 1; row <= ptDest.Y + rcSource.Height; row++)
                for (int col = ptDest.X - 1; col <= ptDest.X + rcSource.Width; col++)
                    if (PointInGrid(col, row) && !undoBlocks.Contains(new Point(col, row)))
                        AddUndoBlock(undoBlocks, Grid[col, row], new Point(col, row));

            MapLineInfo infoCopy = bDelete ? GridLines : null;

            // Due to the nature of lines existing on both sides of two adjacent MapSquare objects, we need to process an extra
            // column and row on each side of the desired operation.

            // The order of the transfer of squares depends on the direction of the copy/move operation, so that we don't overwrite
            // an area that needs to be a later source.
            int iDeltaX = (ptDest.X < rcSource.Location.X ? 1 : -1);
            int iDeltaY = (ptDest.Y < rcSource.Location.Y ? 1 : -1);
            int iRowStart = (iDeltaY == 1 ? rcSource.Top - 1 : rcSource.Bottom);
            int iColStart = (iDeltaX == 1 ? rcSource.Left - 1 : rcSource.Right);
            int iRowEnd = (iDeltaY == 1 ? rcSource.Bottom + 1 : rcSource.Top - 2);
            int iColEnd = (iDeltaX == 1 ? rcSource.Right + 1 : rcSource.Left - 2);

            Point ptOffset = new Point(ptDest.X - rcSource.Left, ptDest.Y - rcSource.Top);

            if (bAddOtherUndo)
            {
                undoBlocks.Other.Add(new UndoItem(GetAllNotesCopy()));
                undoBlocks.Other.Add(new UndoItem(GetAllIconsCopy()));
                undoBlocks.Other.Add(new UndoItem(GetAllLabelsCopy()));
            }

            MapSquare[,] gridSource = grid == null ? Grid : grid;

            int rowFrom = iRowStart;
            int colFrom = iColStart;
            while (rowFrom != iRowEnd)
            {
                while (colFrom != iColEnd)
                {
                    bool bSkipMainCopy = false;

                    Point ptTo = new Point(colFrom + ptOffset.X, rowFrom + ptOffset.Y);

                    if (PointInGrid(gridSource, colFrom, rowFrom))
                    {
                        bool bTo = PointInGrid(ptTo);
                        // Copy the relevant information from one square to the other
                        MapSquare squareFrom = gridSource[colFrom, rowFrom];
                        MapSquare squareTo = bTo ? Grid[ptTo.X, ptTo.Y] : null;

                        if (rowFrom < rcSource.Top || rowFrom >= rcSource.Bottom || colFrom < rcSource.Left || colFrom >= rcSource.Right)
                            bSkipMainCopy = true;

                        if (flags.Outer)
                        {
                            if (rowFrom < rcSource.Top && colFrom >= rcSource.Left && colFrom < rcSource.Right)
                            {
                                if (bTo)
                                    squareTo.CopyLineFrom(Direction.Down, squareFrom, infoCopy, GridLines);
                                else if (infoCopy != null)
                                    squareFrom.Bottom = GridLines;
                                if (grid == null)
                                    SetDirty(colFrom, rowFrom + 1, DirtyType.Up);
                            }
                            if (rowFrom >= rcSource.Bottom && colFrom >= rcSource.Left && colFrom < rcSource.Right)
                            {
                                if (bTo)
                                    squareTo.CopyLineFrom(Direction.Up, squareFrom, infoCopy, GridLines);
                                else if (infoCopy != null)
                                    squareFrom.Top = GridLines;
                                if (grid == null)
                                    SetDirty(colFrom, rowFrom - 1, DirtyType.Down);
                            }
                            if (colFrom < rcSource.Left && rowFrom >= rcSource.Top && rowFrom < rcSource.Bottom)
                            {
                                if (bTo)
                                    squareTo.CopyLineFrom(Direction.Right, squareFrom, infoCopy, GridLines);
                                else if (infoCopy != null)
                                    squareFrom.Right = GridLines;
                                if (grid == null)
                                    SetDirty(colFrom + 1, rowFrom, DirtyType.Left);
                            }
                            if (colFrom >= rcSource.Right && rowFrom >= rcSource.Top && rowFrom < rcSource.Bottom)
                            {
                                if (bTo)
                                    squareTo.CopyLineFrom(Direction.Left, squareFrom, infoCopy, GridLines);
                                else if (infoCopy != null)
                                    squareFrom.Left = GridLines;
                                if (grid == null)
                                    SetDirty(colFrom - 1, rowFrom, DirtyType.Right);
                            }
                        }

                        if (!bSkipMainCopy)
                        {
                            if (flags.Inner)
                            {
                                if (bTo)
                                    squareTo.CopyLinesFrom(squareFrom, infoCopy, GridLines);
                                else if (grid == null && infoCopy != null)
                                    squareFrom.AllLines = infoCopy;
                            }

                            if (flags.Back)
                            {
                                if (bTo)
                                    squareTo.CopyBackFrom(squareFrom, bDelete, true);
                                else if (bDelete && grid == null)
                                    squareFrom.Colors.BackColorPattern = Global.DefaultGridBackground;
                            }

                            if (flags.Notes)
                            {
                                if (bTo)
                                    squareTo.CopyNoteFrom(squareFrom, ptTo, true);
                                if (bDelete && grid == null)
                                {
                                    squareFrom.Note = null;
                                    squareFrom.SetDirty(DirtyType.Back);
                                }
                            }

                            if (flags.Icons)
                            {
                                if (bTo)
                                    squareTo.CopyIconsFrom(squareFrom, ptTo, true, MapReplaceMode.AllIcons);
                                if (bDelete && grid == null)
                                {
                                    squareFrom.Icons = new List<MapIcon>();
                                    squareFrom.SetDirty(DirtyType.Back);
                                }
                            }

                            if (bTo)
                                squareTo.SetDirty(DirtyType.All);
                        }
                    }

                    colFrom += iDeltaX;
                }
                rowFrom += iDeltaY;
                colFrom = iColStart;
            }
        }

        public void ManipulateLabels(Rectangle rcFrom, Point ptTo, bool bCopy, MapLabels labels = null)
        {
            if (labels == null)
                labels = Labels;
            // Move or copy a set of labels that fall inside rcFrom to the same rectangle located at ptTo
            Point ptOffset = new Point(ptTo.X - rcFrom.X, ptTo.Y - rcFrom.Y);

            // Process the grid in an order that will not move anchors into squares that have not yet been processed
            int iDeltaX = ptOffset.X > 0 ? -1 : 1;
            int iDeltaY = ptOffset.Y > 0 ? -1 : 1;
            int iStartRow = iDeltaY == 1 ? rcFrom.Top : rcFrom.Bottom - 1;
            int iEndRow = iDeltaY == 1 ? rcFrom.Bottom : rcFrom.Top - 1;
            int iStartCol = iDeltaX == 1 ? rcFrom.Left : rcFrom.Right - 1;
            int iEndCol = iDeltaX == 1 ? rcFrom.Right : rcFrom.Left - 1;

            for (int row = iStartRow; row != iEndRow; row += iDeltaY)
            {
                for (int col = iStartCol; col != iEndCol; col += iDeltaX)
                {
                    if (labels.AnyAnchorsAt(col, row))
                    {
                        if (bCopy)
                        {
                            foreach (MapLabel label in labels.LabelsAtAnchor(col, row))
                                Labels.Add(label.Offset(ptOffset.X, ptOffset.Y));
                        }
                        else
                        {
                            foreach (MapLabel label in Labels.LabelsAtAnchor(col, row))
                                SetLabelSquaresDirty(label);
                            Labels.Offset(new Point(col, row), ptOffset.X, ptOffset.Y);
                            foreach (MapLabel label in Labels.LabelsAtAnchor(ptOffset.X + col, ptOffset.Y + row))
                                SetLabelSquaresDirty(label);
                        }
                    }
                }
            }
        }

        public void MoveBlocks(UndoList undoBlocks, Rectangle rcBlock, Point ptMoveTo, EditFlags flags)
        {
            ManipulateBlocks(undoBlocks, rcBlock, ptMoveTo, true, flags);
            if (flags.Labels)
                ManipulateLabels(rcBlock, ptMoveTo, false);
        }

        public void CopyBlocks(UndoList undoBlocks, Rectangle rcBlock, Point ptCopyTo, EditFlags flags, MapSquareArray array = null)
        {
            ManipulateBlocks(undoBlocks, rcBlock, ptCopyTo, false, flags, array == null ? null : array.GetGrid());
            if (flags.Labels)
                ManipulateLabels(rcBlock, ptCopyTo, true, array == null ? null : array.GetLabels());
        }

        public void RotateFlipBlocks(UndoList undoBlocks, RotateFlipType rotate, Rectangle rc, EditFlags flags)
        {
            if (rc == Rectangle.Empty)
                rc = new Rectangle(0, 0, GridWidth, GridHeight);

            undoBlocks.Other.Add(new UndoItem(GetAllIconsCopy()));
            undoBlocks.Other.Add(new UndoItem(GetAllNotesCopy()));
            undoBlocks.Other.Add(new UndoItem(GetAllLabelsCopy()));

            Rectangle rcBorder = new Rectangle(0, 0, rc.Width+2, rc.Height+2);

            Rectangle rcNew = new Rectangle(0, 0, rcBorder.Width, rcBorder.Height);

            switch (rotate)
            {
                case RotateFlipType.Rotate90FlipNone:
                case RotateFlipType.Rotate270FlipNone:
                    rcNew = new Rectangle(0, 0, rcBorder.Height, rcBorder.Width);
                    break;
                default:
                    break;
            }

            MapSquare[,] blockNew = new MapSquare[rcNew.Width, rcNew.Height];
            Point ptFrom;

            List<MapLabel> newLabels = new List<MapLabel>();

            for (int row = 0; row < rcNew.Height; row++)
            {
                for (int col = 0; col < rcNew.Width; col++)
                {
                    // A "no-change" point: new Point(rc.X + col - 1, rc.Y + row - 1);
                    switch (rotate)
                    {
                        case RotateFlipType.Rotate90FlipNone:
                            ptFrom = new Point(row + rc.X - 1, (rcBorder.Height - col) + rc.Y - 2);
                            break;
                        case RotateFlipType.Rotate270FlipNone:
                            ptFrom = new Point((rcBorder.Width - row) + rc.X - 2, col + rc.Y - 1);
                            break;
                        case RotateFlipType.Rotate180FlipNone:
                            ptFrom = new Point((rcBorder.Width - 2 - col) + rc.X, (rcBorder.Height - 2 - row) + rc.Y);
                            break;
                        case RotateFlipType.RotateNoneFlipX:
                            ptFrom = new Point((rcBorder.Width - 2 - col) + rc.X, rc.Y + row - 1);
                            break;
                        case RotateFlipType.RotateNoneFlipY:
                            ptFrom = new Point(rc.X + col - 1, (rcBorder.Height - 2 - row) + rc.Y);
                            break;
                        default:
                            // Not a valid operation; bail out
                            return;
                    }
                    if (PointInGrid(ptFrom))
                    {
                        blockNew[col, row] = CloneSquare(ptFrom.X, ptFrom.Y, col, row);
                        blockNew[col, row].Rotate(rotate);
                        if (flags.Labels && rc.Contains(ptFrom) && Labels.AnyAnchorsAt(ptFrom))
                        {
                            foreach (MapLabel label in Labels.LabelsAtAnchor(ptFrom))
                                newLabels.Add(label.Offset(col - ptFrom.X, row - ptFrom.Y));
                        }
                    }
                    else
                        blockNew[col, row] = new MapSquare(GridLines);
                }
            }

            DeleteBlocks(undoBlocks, rc, false);
            ManipulateBlocks(undoBlocks, new Rectangle(1, 1, rcNew.Width - 2, rcNew.Height - 2), rc.Location, false, flags, blockNew, false);
            foreach (MapLabel label in newLabels)
                SetLabelSquaresDirty(label);
            Labels.AddRange(newLabels);
        }

        public MapSquare CloneSquare(int colFrom, int rowFrom, int colTo, int rowTo)
        {
            if (!PointInGrid(colFrom, rowFrom))
                return null;
            MapSquare squareFrom = Grid[colFrom, rowFrom];
            MapSquare squareTo = squareFrom.Clone();
            if (squareFrom.Note != null)
                squareTo.Note = squareFrom.Note.Clone(new Point(colTo, rowTo));
            if (squareFrom.Icons != null)
                foreach (MapIcon icon in squareFrom.Icons)
                    squareTo.Icons.Add(icon.Clone(new Point(colTo, rowTo)));
            return squareTo;
        }

        public List<MapSquare> GetSquares(Rectangle rc)
        {
            List<MapSquare> list = new List<MapSquare>(rc.Width * rc.Height);
            for(int row = rc.Top; row < rc.Bottom; row++)
                for(int col = rc.Left; col < rc.Right; col++)
                    list.Add(Grid[col, row]);

            return list;
        }

        public void SetDirty(Rectangle[] rects, DirtyType type = DirtyType.All, bool bValue = true)
        {
            if (rects == null)
                return;
            foreach (Rectangle rc in rects)
                for (int y = rc.Top; y < rc.Bottom; y++)
                    for (int x = rc.Left; x < rc.Right; x++)
                        SetDirty(x, y, type, bValue);
        }

        public void SetDirty(int col, int row, DirtyType type = DirtyType.All, bool bValue = true)
        {
            if (!PointInGrid(col, row))
                return;
            Grid[col, row].SetDirty(type, bValue);
        }

        public void SetGameSquaresDirty(DirtyType type = DirtyType.All, bool bValue = true)
        {
            // TODO: Figure out which squares belong to the game (i.e. not borders) and skip others
            for (int i = 0; i < GridHeight; i++)
                for (int j = 0; j < GridWidth; j++)
                    Grid[j, i].SetDirty(type, bValue);
        }

        public void SetDirty(Point pt, DirtyType type = DirtyType.All, bool bValue = true)
        {
            if (!PointInGrid(pt))
                return;
            Grid[pt.X, pt.Y].SetDirty(type, bValue);
        }

        public void ResetVisited()
        {
            foreach(MapSquare square in Grid)
                square.SetVisitedAndSeen(false);
            ForceRefreshOnDisplay = true;
        }

        public void ResetVisited(Point pt)
        {
            if (!PointInGrid(pt))
                return;

            SetVisited(pt, false);
        }

        public bool ToggleVisited(Point pt, bool bIncludeSeen = true)
        {
            if (!PointInGrid(pt))
                return false;

            bool bSet = !Grid[pt.X, pt.Y].Visited;
            if (bIncludeSeen)
                SetSeen(pt, bSet);
            SetVisited(pt, bSet);
            return bSet;
        }

        public bool PointInGrid(MapSquare[,] grid, int x, int y)
        {
            if (x < 0)
                return false;
            if (y < 0)
                return false;
            if (x >= grid.GetLength(0))
                return false;
            if (y >= grid.GetLength(1))
                return false;
            return true;
        }

        public bool PointInGrid(Point pt) { return PointInGrid(pt.X, pt.Y); }

        public bool PointInGrid(int x, int y)
        {
            if (x < 0)
                return false;
            if (y < 0)
                return false;
            if (x >= GridWidth)
                return false;
            if (y >= GridHeight)
                return false;
            return true;
        }

        public void ReinterpretSquare(Point ptGrid, Point ptMap, MapData data, MapBook book)
        {
            int iEast = (book.Location.IncreaseX == AxisIncreaseX.RightToLeft ? -1 : 1);
            int iSouth = (book.Location.IncreaseY == AxisIncreaseY.BottomToTop ? -1 : 1);
            SetSquareFromGameBytes(book, ptGrid, data, ptMap.X, ptMap.Y, iEast, iSouth);
            SetDirty(ptGrid);
        }

        public MapSheet(MapBook book, MapData data)
        {
            InitSheet();
            Grid = new MapSquare[data.Width + 2, data.Height + 2];
            List<MapSection> sections = new List<MapSection>();
            int iFullGridSection = -1;

            if (data.Sections != null || data.Bounds.Location != Point.Empty)
            {
                if (data.Sections != null)
                    foreach (MapSection section in data.Sections)
                        sections.Add(section);

                // The "entire grid" section needs to be last or it will override any of the other sections
                if (data.Bounds.Location != Point.Empty)
                {
                    int iNewX = book.Location.IncreaseX == AxisIncreaseX.RightToLeft ? data.Bounds.Width : -1;
                    int iNewY = book.Location.IncreaseY == AxisIncreaseY.BottomToTop ? data.Bounds.Height : -1;
                    sections.Add(new MapSection(new Rectangle(0, 0, GridWidth, GridHeight),
                        new Point(iNewX + data.Bounds.Location.X, iNewY + data.Bounds.Location.Y)));
                    iFullGridSection = sections.Count - 1;
                }
            }
            Sections = sections.ToArray();

            SquareSize = new Size(Properties.Settings.Default.DefaultSquareSize.Width, Properties.Settings.Default.DefaultSquareSize.Height);
            Cursor = Point.Empty;
            CreateGrid();

            int iEast = (book.Location.IncreaseX == AxisIncreaseX.RightToLeft ? -1 : 1);
            int iSouth = (book.Location.IncreaseY == AxisIncreaseY.BottomToTop ? -1 : 1);

            List<MapSquareData> squareData = new List<MapSquareData>();

            for (int i = data.Bounds.Left; i < data.Bounds.Right; i++)
            {
                for (int j = data.Bounds.Top; j < data.Bounds.Bottom; j++)
                {
                    Point ptGrid = book.TranslateLocationToMap(new Point(i, j), this);
                    MapSquareData sd = SetSquareFromGameBytes(book, ptGrid, data, i, j, iEast, iSouth);
                    if (sd != null)
                        squareData.Add(sd);
                }
            }

            foreach (MapSquareData sd in squareData)
            {
                Point ptGrid = book.TranslateLocationToMap(sd.Location, this);
                MapSquare square = GetSquareAtGridPoint(ptGrid);
                if (square == null)
                    continue;

                square.AddNoteLine(sd.Note, sd.Symbol, ptGrid);
            }

            // Unimportant squares technically have walls, but the maps look cleaner if they are hidden
            for (int i = data.Bounds.Left; i < data.Bounds.Right; i++)
                for (int j = data.Bounds.Top; j < data.Bounds.Bottom; j++)
                    CheckUnimportantWalls(book, book.TranslateLocationToMap(new Point(i, j), this));

            // Add the border square colors and lines
            Grid[0, 0].Colors.Background = Color.DarkGray;
            Grid[0, data.Height + 1].Colors.Background = Color.DarkGray;
            Grid[data.Width + 1, 0].Colors.Background = Color.DarkGray;
            Grid[data.Width + 1, data.Height + 1].Colors.Background = Color.DarkGray;

            for (int i = 1; i < data.Width + 1; i++)
            {
                Grid[i, 0].Colors.Background = Color.DarkGray;
                Grid[i, 0].Bottom = EdgeLineInfo(i, 1, Direction.Up);
                Grid[i, data.Height + 1].Colors.Background = Color.DarkGray;
                Grid[i, data.Height + 1].Top = EdgeLineInfo(i, data.Height, Direction.Down);
            }
            for (int j = 1; j < data.Height + 1; j++)
            {
                Grid[0, j].Colors.Background = Color.DarkGray;
                Grid[0, j].Right = EdgeLineInfo(1, j, Direction.Left);
                Grid[data.Width + 1, j].Colors.Background = Color.DarkGray;
                Grid[data.Width + 1, j].Left = EdgeLineInfo(data.Width, j, Direction.Right);
            }

            m_mapsMightAndMagic.CheckConnectingMaps(data);

            if (Sections.Length > 0)
            {
                // The raw sections from the map data need to be translated to the main grid
                for (int i = 0; i < sections.Count; i++)
                {
                    Sections[i].External = true;

                    if (i == iFullGridSection)
                        continue;  // already translated

                    if (book.Location.IncreaseX == AxisIncreaseX.RightToLeft)
                    {
                        Sections[i].Source.X = (data.Bounds.Width + data.Bounds.Left) - Sections[i].Source.X - Sections[i].Source.Width;
                        Sections[i].Target.X += Sections[i].Source.Width + book.Location.OffsetX;
                    }
                    Sections[i].Source.X -= book.Location.OffsetX;
                    if (book.Location.IncreaseY == AxisIncreaseY.BottomToTop)
                    {
                        Sections[i].Source.Y = (data.Bounds.Height + data.Bounds.Top) - Sections[i].Source.Y - Sections[i].Source.Height;
                        Sections[i].Target.Y += Sections[i].Source.Height + book.Location.OffsetY;
                    }
                    Sections[i].Source.Y -= book.Location.OffsetY;
                }
                if (data.LocationOffset != Point.Empty)
                {
                    // The map is now at the correct place on the grid, but where (0,0) is located
                    // is something the map bytes need to tell us
                    foreach (MapSection section in Sections)
                    {
                        section.Target.X += data.LocationOffset.X;
                        section.Target.Y += data.LocationOffset.Y;
                    }
                }
            }
        }

        private MapLineInfo EdgeLineInfo(int x, int y, Direction dir)
        {
            if (!PointInGrid(new Point(x, y)))
                return new MapLineInfo(Color.Black, DashStyle.Solid, 2);
            return Grid[x, y].LineInfo(dir);
        }

        public void CheckUnimportantWalls(MapBook book, Point ptGrid)
        {
            if (!PointInGrid(ptGrid))
                return;

            MapSquare square = Grid[ptGrid.X, ptGrid.Y];
            if (!square.IsUnimportant)
                return;

            if (UnimportantAndIdentical(ptGrid, ptGrid.X - 1, ptGrid.Y))
                square.Line(Direction.Left, GridLines);
            if (UnimportantAndIdentical(ptGrid, ptGrid.X, ptGrid.Y - 1))
                square.Line(Direction.Up, GridLines);
            if (UnimportantAndIdentical(ptGrid, ptGrid.X + 1, ptGrid.Y))
                square.Line(Direction.Right, GridLines);
            if (UnimportantAndIdentical(ptGrid, ptGrid.X, ptGrid.Y + 1))
                square.Line(Direction.Down, GridLines);
        }

        private bool UnimportantAndIdentical(Point pt, int x, int y)
        {
            if (x >= GridWidth || y >= GridHeight || x < 0 || y < 0)
                return false;
            if (pt.X >= GridWidth || pt.Y >= GridHeight || pt.X < 0 || pt.Y < 0)
                return false;
            if (!Grid[pt.X, pt.Y].IsUnimportant)
                return false;
            if (!Grid[x, y].IsUnimportant)
                return false;
            if (Grid[pt.X, pt.Y].Colors.Background.ToArgb() != Grid[x, y].Colors.Background.ToArgb())
                return false;
            if (Grid[pt.X, pt.Y].Colors.BackgroundStyle != Grid[x, y].Colors.BackgroundStyle)
                return false;
            return true;
        }

        private MapSquareData SetSquareFromGameBytes(MapBook book, Point ptGrid, MapData mapData, int horiz, int vert, int iDeltaEast, int iDeltaSouth)
        {
            if (mapData is WizardryMapData)
                m_mapsWizardry.SetWizSquareFromGameBytes(book, ptGrid, mapData as WizardryMapData, horiz, vert, iDeltaEast, iDeltaSouth);
            else if (mapData is MMMapData)
                m_mapsMightAndMagic.SetMMSquareFromGameBytes(ptGrid, mapData as MMMapData, horiz, vert, iDeltaEast, iDeltaSouth);
            else if (mapData is BTMapData)
                m_mapsBardsTale.SetBTSquareFromGameBytes(ptGrid, mapData as BTMapData, horiz, vert, iDeltaEast, iDeltaSouth);
            else if (mapData is EOBMapData)
                m_mapsEOB.SetEOBSquareFromGameBytes(ptGrid, mapData as EOBMapData, horiz, vert, iDeltaEast, iDeltaSouth);
            else if (mapData is UltimaMapData)
                m_mapsUltima.SetUltimaSquareFromGameBytes(ptGrid, mapData as UltimaMapData, horiz, vert, iDeltaEast, iDeltaSouth);

            return null;
        }

        public void RemoveLiveData(MapSquare square)
        {
            square.Left = GridLines;
            square.Right = GridLines;
            square.Top = GridLines;
            square.Bottom = GridLines;
            for (int i = square.Icons.Count - 1; i >= 0; i--)
            {
                if (square.Icons[i].IsWallIcon)
                    square.Icons.RemoveAt(i);
            }
        }

        public MapSquare SquareForNote(MapNote note)
        {
            if (!PointInGrid(note.Location))
                return null;

            return GetSquareAtGridPoint(note.Location);
        }

        public void CopyGridDataFrom(MapSheet copy, MapReplaceMode mode)
        {
            // Copies the square colors and lines (not the notes or icons)
            for (int y = 0; y < copy.GridHeight; y++)
            {
                for (int x = 0; x < copy.GridWidth; x++)
                {
                    if (!PointInGrid(new Point(x,y)))
                        continue;
                    Grid[x, y].CopyGridDataFrom(copy.Grid[x, y], mode);
                    if (mode.HasFlag(MapReplaceMode.WallIcons) || mode.HasFlag(MapReplaceMode.SquareIcons))
                        Grid[x, y].CopyIconsFrom(copy.Grid[x, y], new Point(x, y), false, mode);
                }
            }
        }

        public void ReplaceLines(MapLineInfo infoOld, MapLineInfo infoNew)
        {
            for (int row = 0; row < GridHeight; row++)
                for (int col = 0; col < GridWidth; col++)
                    Grid[col, row].ReplaceLines(infoOld, infoNew);
        }

        public Size SelectionMargin
        {
            get
            {
                return new Size(SquareSize.Width / 8, SquareSize.Height / 8);
            }
        }

        public bool IsZoom(int iTest)
        {
            if (SquareSize.Width != SquareSize.Height)
                return false;
            return (iTest * 16 / 100 == SquareSize.Width);
        }

        public void CopyToClipboard(Rectangle rc)
        {
            rc.Inflate(1, 1);
            MapSquareArray array = new MapSquareArray(Grid, rc, GridLines, Labels);
            DataFormats.Format format = DataFormats.GetFormat(typeof(MapSquareArray).FullName);
            IDataObject dataObj = new DataObject();
            dataObj.SetData(format.Name, false, array);
            Clipboard.SetDataObject(dataObj, false);
        }

        public MapSquare GetNextSquare(Point pt, Direction facing)
        {
            Point ptOther = Point.Empty;
            return GetNextSquare(pt, facing, out ptOther);
        }

        public MapSquare GetNextSquare(Point pt, Direction facing, out Point ptOther)
        {
            ptOther = Global.OffsetPoint(pt, facing);
            if (!PointInGrid(ptOther))
                return null;
            return Grid[ptOther.X, ptOther.Y];
        }

        private void CopyLineInfo(UndoList undo, MapSquare dest, Point ptDest, Direction dirDest, MapSquare source, Point ptSource, Direction dirSource)
        {
            if (undo != null)
                undo.Add(new UndoMapSquare(dest, ptDest));
            dest.Line(dirDest, source.LineInfo(dirSource));
            dest.SetDirty(dirDest);
        }

        private void ConvertHalfLines(UndoList undo, int col, int row, Direction dir)
        {
            Point pt = new Point(col, row);
            Point ptOther = Point.Empty;
            if (!PointInGrid(col, row))
                return;
            MapSquare square = Grid[col, row];
            MapSquare squareOther = GetNextSquare(pt, dir, out ptOther);
            Direction opposite = Global.Opposite(dir);
            if (squareOther == null)
                return;

            if (squareOther.LineInfo(opposite).Equals(GridLines) && !square.LineInfo(dir).Equals(GridLines))
                CopyLineInfo(undo, squareOther, ptOther, opposite, square, pt, dir);
            else if (!squareOther.LineInfo(opposite).Equals(GridLines) && square.LineInfo(dir).Equals(GridLines))
                CopyLineInfo(undo, square, pt, dir, squareOther, ptOther, opposite);
        }

        public void ConvertHalfLines(UndoList undo, Rectangle rc)
        {
            for (int row = rc.Top; row < rc.Bottom; row++)
            {
                for (int col = rc.Left; col < rc.Right; col++)
                {
                    ConvertHalfLines(undo, col, row, Direction.Left);
                    ConvertHalfLines(undo, col, row, Direction.Up);
                    ConvertHalfLines(undo, col, row, Direction.Right);
                    ConvertHalfLines(undo, col, row, Direction.Down);
                }
            }
        }

        public void FillBlocks(UndoList undo, Rectangle rc, ColorPattern cp)
        {
            for (int row = rc.Top; row < rc.Bottom; row++)
            {
                for (int col = rc.Left; col < rc.Right; col++)
                {
                    if (!PointInGrid(col, row))
                        continue;
                    if (undo != null)
                        undo.Add(UndoSquare(col, row));
                    Grid[col, row].Colors.BackColorPattern = cp;
                    SetDirty(col, row, DirtyType.Back);
                }
            }
        }

        public UndoMapSquare UndoSquare(Point pt) { return UndoSquare(pt.X, pt.Y); }

        public UndoMapSquare UndoSquare(int col, int row)
        {
            if (!PointInGrid(col, row))
                return null;
            return new UndoMapSquare(Grid[col, row], new Point(col, row));
        }

        public void SetLines(UndoList undo, int iX, int iY, Direction dir, MapLineInfo info)
        {
            if (!PointInGrid(iX, iY))
                return;
            if (undo != null)
                undo.Add(UndoSquare(iX, iY));
            Grid[iX, iY].Line(dir, info);
            Point ptOpposite = Global.OffsetPoint(new Point(iX, iY), dir);
            if (!PointInGrid(ptOpposite))
                return;
            if (undo != null)
                undo.Add(UndoSquare(ptOpposite));
            Grid[ptOpposite.X, ptOpposite.Y].Line(Global.Opposite(dir), info);
        }

        public void Outline(UndoList undo, Rectangle rc, MapLineInfo info)
        {
            for (int row = rc.Top; row < rc.Bottom; row++)
            {
                SetLines(undo, rc.Left, row, Direction.Left, info);
                SetLines(undo, rc.Right - 1, row, Direction.Right, info);
            }
            for (int col = rc.Left; col < rc.Right; col++)
            {
                SetLines(undo, col, rc.Top, Direction.Up, info);
                SetLines(undo, col, rc.Bottom - 1, Direction.Down, info);
            }
        }

        public void SetLabelSquaresDirty(MapLabel label)
        {
            if (m_bmpGrid == null)
                return;

            // Map labels cover multiple squares, so they all need to be marked as dirty
            using (Graphics g = Graphics.FromImage(m_bmpGrid))
            {
                SizeF sz = g.MeasureString(label.Text, label.GetFont(SquareSize));
                RectangleF rc = GetLabelRect(sz, label);
                Rectangle rcGrid = new Rectangle((int)label.Location.X, (int)label.Location.Y,
                    (int)(rc.Width / SquareSize.Width) + 1, (int)(rc.Height / SquareSize.Height) + 1);
                for (int row = rcGrid.Y - 1; row <= rcGrid.Bottom; row++)
                    for (int col = rcGrid.X - 1; col <= rcGrid.Right; col++)
                        SetDirty(col, row);
            }
        }

        public bool AnyHidden(RectangleF rc, bool[,] hide)
        {
            if (hide == null)
                return false;

            Rectangle rcGrid = new Rectangle((int)(rc.Left / SquareSize.Width), (int)(rc.Top / SquareSize.Height), 
                (int) (rc.Width / SquareSize.Width + 1), (int) (rc.Height / SquareSize.Height + 1));
            for (int row = (int)rcGrid.Top; row < rcGrid.Bottom; row++)
                for (int col = (int)rcGrid.Left; col < rcGrid.Right; col++)
                    if (hide[col, row])
                        return true;

            return false;
        }

        public MapLabel LabelInPoint(Point pt)
        {
            if (m_bmpSquare == null)
                m_bmpSquare = new Bitmap(1, 1);
            using (Graphics g = Graphics.FromImage(m_bmpSquare))
            {
                foreach (MapLabel label in Labels.Values)
                {
                    SizeF sz = g.MeasureString(label.Text, label.GetFont(SquareSize));
                    RectangleF rc = GetLabelRect(sz, label);
                    if (rc.Contains(pt.X, pt.Y))
                        return label;
                }
            }
            return null;
        }

        public void SetAllLabels(MapLabel[] labels)
        {
            foreach (MapLabel label in Labels.Values)
                SetLabelSquaresDirty(label);
            Labels.Clear();
            foreach (MapLabel label in labels)
            {
                Labels.Add(label);
                SetLabelSquaresDirty(label);
            }
        }

        public bool ChangeLabels(MapLabels newLabels)
        {
            bool bAnyChanges = false;
            if (Labels != null)
            {
                foreach (PointF point in Labels.Keys)
                {
                    // Set any squares under a label that is about to be deleted as dirty
                    if (!newLabels.ContainsKey(point))
                    {
                        bAnyChanges = true;
                        SetLabelSquaresDirty(Labels[point]);
                    }
                    // Set any squares under a label that is being changed as dirty
                    else if (!newLabels[point].Equals(Labels[point]))
                    {
                        SetLabelSquaresDirty(Labels[point]);
                        bAnyChanges = true;
                    }
                }
            }

            // Set any squares under a new label as dirty
            foreach (PointF point in newLabels.Keys)
            {
                if (Labels == null || !Labels.ContainsKey(point))
                {
                    SetLabelSquaresDirty(newLabels[point]);
                    bAnyChanges = true;
                }
            }

            if (bAnyChanges)
                Labels = newLabels;

            return bAnyChanges;
        }

        public bool AddLabel(PointF pt, string strText)
        {
            if (Labels == null)
                Labels = new MapLabels();
            if (Labels.ContainsKey(pt))
                return false;
            Labels.Add(new MapLabel(pt, strText));
            SetLabelSquaresDirty(Labels[pt]);
            return true;
        }

        public bool SetSelectedLabels(HashSet<PointF> labels)
        {
            bool bAnyDirty = false;
            foreach (MapLabel label in Labels.Values)
            {
                bool bSelected = labels.Contains(label.Location);
                if (label.Selected != bSelected)
                {
                    SetDirty(label.Anchors);
                    label.Selected = bSelected;
                    bAnyDirty = true;
                }
            }
            return bAnyDirty;
        }

        public MapSection PointInSectionTarget(LocationSettings settings, Point pt)
        {
            if (Sections == null || Sections.Length < 1)
                return null;

            // Order is important; if a section intersects (or encompasses) another, the first one will be returned
            foreach (MapSection section in Sections)
                if (section.GetTargetRect(settings).Contains(pt))
                    return section;

            return null;
        }

        public MapSection PointInSection(Point pt)
        {
            if (Sections == null || Sections.Length < 1)
                return null;

            // Order is important; if a section intersects (or encompasses) another, the first one will be returned
            foreach (MapSection section in Sections)
                if (section.Source.Contains(pt))
                    return section;

            return null;
        }
    }

    [Flags]
    public enum MapReplaceMode
    {
        None =          0x0000,
        Lines =         0x0001,
        Back =          0x0002,
        WallIcons =     0x0004,
        SquareIcons =   0x0008,
        Labels =        0x0010,
        GridOnly = Lines | Back,
        AllIcons = WallIcons | SquareIcons,
        Full = Lines | Back | WallIcons | SquareIcons | Labels
    }

    public class MapSquareData
    {
        public Point Location;
        public string Symbol;
        public string Note;

        public MapSquareData(Point ptMap, string sSymbol, string sNote)
        {
            Location = ptMap;
            Symbol = sSymbol;
            Note = sNote;
        }
    }

    [Flags]
    public enum CheckBarrierResult
    {
        None =         0x0000,
        Lock =         0x0001,
        Barrier =      0x0002,
        RemoveDoor =   0x0004
    }

    [Serializable]
    public class MapSquareArray
    {
        public Rectangle Bounds;
        public byte[] BytesGrid;
        public byte[] BytesLabels;

        public MapSquareArray(MapLabels labels) : this(new MapSquare[0,0], Rectangle.Empty, null, labels) { }

        public MapSquareArray(MapSquare[,] grid, Rectangle rc, MapLineInfo gridLines, MapLabels labels)
        {
            int iGridWidth = grid.GetLength(0);
            int iGridHeight = grid.GetLength(1);
            Bounds = rc;
            MemoryStream ms = new MemoryStream(rc.Width * rc.Height * 29);
            for (int row = rc.Top; row < rc.Bottom; row++)
            {
                for (int col = rc.Left; col < rc.Right; col++)
                {
                    if (row >= 0 && row < iGridHeight && col >= 0 && col < iGridWidth)
                        grid[col, row].Serialize(ms, true);
                    else
                        new MapSquare(gridLines).Serialize(ms, true);
                }
            }
            BytesGrid = ms.ToArray();
            if (labels != null)
            {
                ms = new MemoryStream();
                foreach (MapLabel label in labels.Values)
                    label.Offset(-rc.Location.X, -rc.Location.Y).Serialize(ms);
                BytesLabels = ms.ToArray();
            }
            else
                BytesLabels = null;
        }

        public MapSquare[,] GetGrid()
        {
            MapSquare[,] grid = new MapSquare[Bounds.Width, Bounds.Height];
            MemoryStream ms = new MemoryStream(BytesGrid);
            for (int row = 0; row < Bounds.Height; row++)
                for (int col = 0; col < Bounds.Width; col++)
                    grid[col, row] = new MapSquare(ms, new Point(col, row));
            return grid;
        }

        public MapLabels GetLabels()
        {
            if (BytesLabels == null)
                return null;
            MemoryStream ms = new MemoryStream(BytesLabels);
            MapLabels labels = new MapLabels();
            MapLabel label = null;
            do
            {
                label = MapLabel.Deserialize(ms);
                if (label != null)
                    labels.Add(label);
            } while (label != null);
            return labels;
        }
    }

    public class VisitedArray
    {
        public MapSquareFlags[,] Flags;

        public VisitedArray(MapSheet sheet)
        {
            Flags = new MapSquareFlags[sheet.GridWidth, sheet.GridHeight];
            for (int row = 0; row < sheet.GridHeight; row++)
                for (int col = 0; col < sheet.GridWidth; col++)
                    Flags[col, row] = sheet.Grid[col, row].Flags;
        }

        public void SetVisited(MapSheet sheet)
        {
            for (int row = 0; row < sheet.GridHeight; row++)
                for (int col = 0; col < sheet.GridWidth; col++)
                    if (row < Flags.GetLength(1) && col < Flags.GetLength(0))
                        if (sheet.Grid[col, row].SetFlags(Flags[col, row]))
                            sheet.Grid[col, row].SetDirty(DirtyType.All);
        }
    }
}
