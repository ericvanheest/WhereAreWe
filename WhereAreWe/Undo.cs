using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;

namespace WhereAreWe
{
    public enum UndoAction
    {
        None,
        MapSizeChange,
        MapDataChange,
        NotesChange,
        IconsChange,
        NoteChange,
        IconChange,
        LabelChange,
        LabelsChange,
        VisitedChange
    }

    public class UndoMapSquare
    {
        public MapSquare Square;
        public Point Location;

        public UndoMapSquare(MapSquare square, Point location)
        {
            Square = square.Clone();
            Location = location;
        }
    }

    public class UndoList
    {
        public Dictionary<Point, UndoMapSquare> Squares;
        public List<UndoItem> Other;

        public UndoList()
        {
            Squares = new Dictionary<Point, UndoMapSquare>();
            Other = new List<UndoItem>();
        }

        public UndoList(UndoMapSquare[] squares)
        {
            Other = new List<UndoItem>();
            Squares = new Dictionary<Point, UndoMapSquare>(squares.Length);
            foreach (UndoMapSquare square in squares)
                if (square != null)
                    Add(square);
        }

        public UndoList(UndoList list)
        {
            Other = new List<UndoItem>(list.Other);
            Squares = new Dictionary<Point, UndoMapSquare>(list.Squares.Count);
            foreach (UndoMapSquare square in list.Squares.Values)
                if (square != null)
                    Add(square);
        }

        public bool Add(UndoMapSquare square)
        {
            if (Squares.ContainsKey(square.Location))
                return false;
            Squares.Add(square.Location, square);
            return true;
        }

        public int Count { get { return Squares.Count; } }

        public bool Contains(Point pt)
        {
            return Squares.ContainsKey(pt);
        }
    }

    public class UndoItem : IDisposable
    {
        public UndoAction Action;

        public MemoryStream MapSheetStream = null;
        public MapNote[] Notes;
        public MapIcon[] Icons;
        public MapNote Note;
        public MapIcon Icon;
        public MapLabel[] Labels;
        public MapLabel Label;
        public BasicLocation YouAreHere;
        public UndoList Squares;
        public VisitedArray Visited;

        public Size MapSheetSize;

        public UndoItem(MapSheet sheet)
        {
            Action = UndoAction.MapSizeChange;
            MapSheetSize = new Size(sheet.GridWidth, sheet.GridHeight);
            MapSheetStream = new MemoryStream();
            sheet.Serialize(MapSheetStream);
            MapSheetStream.Seek(0, SeekOrigin.Begin);
            Notes = sheet.GetAllNotesCopy();
            Labels = sheet.GetAllLabelsCopy();
            Icons = sheet.GetAllIconsCopy();
            YouAreHere = sheet.YouAreHere;
        }

        public UndoItem(UndoMapSquare[] squares)
        {
            Action = UndoAction.MapDataChange;
            Squares = new UndoList(squares);
        }

        public UndoItem(UndoList list)
        {
            Action = UndoAction.MapDataChange;
            Squares = new UndoList(list);
        }

        public UndoItem(MapNote[] notes)
        {
            Action = UndoAction.NotesChange;
            Notes = notes;
        }

        public UndoItem(MapLabel[] labels)
        {
            Action = UndoAction.LabelsChange;
            Labels = labels;
        }

        public UndoItem(MapLabel label)
        {
            Action = UndoAction.LabelsChange;
            Label = label;
        }

        public UndoItem(MapNote note)
        {
            Action = UndoAction.NoteChange;
            Note = note;
        }

        public UndoItem(MapIcon icon)
        {
            Action = UndoAction.IconChange;
            Icon = icon;
        }

        public UndoItem(MapIcon[] icons)
        {
            Action = UndoAction.IconsChange;
            Icons = icons;
        }

        public UndoItem(VisitedArray visited)
        {
            Action = UndoAction.VisitedChange;
            Visited = visited;
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
                if (MapSheetStream != null)
                    MapSheetStream.Dispose();
            }
        }

        public string DebugString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                switch (Action)
                {
                    case UndoAction.IconsChange:
                        sb.AppendFormat("Icons({0})", Icons.Length);
                        break;
                    case UndoAction.IconChange:
                        sb.AppendFormat("Icon({0},{1},{2})", Icon.Location.X, Icon.Location.Y, Icon.Name.ToString());
                        break;
                    case UndoAction.NoteChange:
                        sb.AppendFormat("Note({0},{1},{2})", Note.Location.X, Note.Location.Y, Note.Symbol);
                        break;
                    case UndoAction.MapDataChange:
                        sb.AppendFormat("MapData({0}: ", Squares.Count);
                        string strSep = "";
                        foreach (UndoMapSquare square in Squares.Squares.Values)
                        {
                            sb.Append(strSep);
                            sb.AppendFormat("{0},{1},{2}", square.Location.X, square.Location.Y, Global.GenericColorName(square.Square.Colors.Background));
                            strSep = " ";
                        }
                        sb.Append(")");
                        break;
                    case UndoAction.MapSizeChange:
                        sb.AppendFormat("MapSizeChange({0},{1})", MapSheetSize.Width, MapSheetSize.Height);
                        break;
                    case UndoAction.None:
                        break;
                    case UndoAction.NotesChange:
                        sb.AppendFormat("Notes({0})", Notes.Length);
                        break;
                    case UndoAction.LabelsChange:
                        sb.AppendFormat("Labels({0})", Labels.Length);
                        break;
                    case UndoAction.VisitedChange:
                        sb.AppendFormat("Visited({0},{1})", Visited.Flags.GetLength(1), Visited.Flags.GetLength(0));
                        break;
                    default:
                        sb.Append("<unknown>");
                        break;
                }
                return sb.ToString();
            }
        }
    }

    public class Undo
    {
        public List<UndoItem> Actions;
        private int m_iMaxUndo;
        private int m_iRedoIndex;

        public Undo(int iMaxUndo)
        {
            Actions = new List<UndoItem>(iMaxUndo);
            m_iMaxUndo = iMaxUndo;
            m_iRedoIndex = Actions.Count;
        }

        public UndoItem AddEmptySquareItem()
        {
            return AddItem(new UndoMapSquare[0]);
        }

        public void ClearRedo()
        {
            if (RedoAvailable > 0)
            {
                Actions.RemoveRange(m_iRedoIndex, Actions.Count - m_iRedoIndex);
                m_iRedoIndex = Actions.Count;
            }
        }

        public UndoItem AddItem(object o)
        {
            // Can't redo any more after adding a new item
            UndoItem item = SpecificItem(o);
            if (item == null)
                return null;

            if (m_iRedoIndex < Actions.Count)
                Actions[m_iRedoIndex] = item;
            else
            {
                Actions.Add(item);
                m_iRedoIndex = Actions.Count;
            }

            if (Actions.Count > m_iMaxUndo)
            {
                Actions.RemoveAt(0);
                m_iRedoIndex = Actions.Count;
            }

            return item;
        }

        private UndoItem SpecificItem(object o)
        {
            if (o is MapSheet)
                return new UndoItem(o as MapSheet);
            else if (o is MapIcon)
                return new UndoItem(o as MapIcon);
            else if (o is MapNote)
                return new UndoItem(o as MapNote);
            else if (o is UndoMapSquare[])
                return new UndoItem(o as UndoMapSquare[]);
            else if (o is MapNote[])
                return new UndoItem(o as MapNote[]);
            else if (o is MapLabel[])
                return new UndoItem(o as MapLabel[]);
            else if (o is MapLabel)
                return new UndoItem(o as MapLabel);
            else if (o is MapIcon[])
                return new UndoItem(o as MapIcon[]);
            else if (o is UndoList)
                return new UndoItem(o as UndoList);
            else if (o is VisitedArray)
                return new UndoItem(o as VisitedArray);
            return null;
        }

        public void AddRedoItem(object o)
        {
            UndoItem item = SpecificItem(o);

            if (item == null)
                return;

            // If the m_iRedoIndex is in the middle of the list, then replace
            // the current item
            if (m_iRedoIndex < Actions.Count)
                Actions[m_iRedoIndex] = item;

            // Otherwise something is wrong; do nothing
        }

        public int UndoAvailable
        {
            get { return m_iRedoIndex; }
        }

        public string UndoActionName(int iLevel)
        {
            if (iLevel < 0 || iLevel > UndoAvailable)
                return "<Error>";

            return NameForAction(Actions[UndoAvailable - iLevel - 1].Action);
        }

        public string RedoActionName(int iLevel)
        {
            if (iLevel < 0 || iLevel > RedoAvailable)
                return "<Error>";

            return NameForAction(Actions[Actions.Count - RedoAvailable + iLevel].Action);
        }

        public UndoItem UndoOneAction()
        {
            if (Actions.Count < 1)
                return null;

            if (UndoAvailable < 1)
                return null;

            return Actions[--m_iRedoIndex];
        }

        public int RedoAvailable
        {
            get { return Actions.Count - m_iRedoIndex; }
        }

        public UndoItem GetRedoAction()
        {
            if (m_iRedoIndex < 0)
                return null;

            if (m_iRedoIndex >= Actions.Count)
            {
                m_iRedoIndex = Actions.Count;
                return null;
            }

            return Actions[m_iRedoIndex];
        }

        public void IncrementRedoCounter()
        {
            m_iRedoIndex++;
            if (m_iRedoIndex > Actions.Count)
                m_iRedoIndex = Actions.Count;
        }

        public void Clear()
        {
            Actions.Clear();
        }

        public string NameForAction(UndoAction action)
        {
            switch (action)
            {
                case UndoAction.None:        return "<none>";
                case UndoAction.MapSizeChange:   return "Map Size Change";
                case UndoAction.MapDataChange: return "Map Change";
                case UndoAction.NotesChange: return "Note Change";
                case UndoAction.IconsChange: return "Icon Change";
                case UndoAction.IconChange: return "Icon Change";
                case UndoAction.NoteChange: return "Note Change";
                case UndoAction.LabelChange: return "Label Change";
                case UndoAction.LabelsChange: return "Label Change";
                case UndoAction.VisitedChange: return "Visited Squares Change";
                default: return "<unknown>";
            }
        }

        public string DebugString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Undo[{0},{1}]: ", Actions.Count, m_iRedoIndex);
                string strComma = "";
                for (int i = 0; i < Actions.Count; i++)
                {
                    sb.Append(strComma);
                    if (i == m_iRedoIndex)
                        sb.Append("**");
                    sb.Append(Actions[i].DebugString);
                    if (i == m_iRedoIndex)
                        sb.Append("**");
                    strComma = ", ";
                }
                sb.Append("\r\n");
                return sb.ToString();
            }
        }
    }
}
