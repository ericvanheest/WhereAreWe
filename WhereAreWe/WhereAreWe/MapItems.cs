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
using System.Collections;

namespace WhereAreWe
{
    public class MapSquareColors
    {
        private Color m_Background;
        private HatchStyle m_BackgroundStyle;
        private Color m_Top;
        private Color m_Left;
        private Color m_Bottom;
        private Color m_Right;

        public bool Changed { get; set; }
        public ColorPattern BackColorPattern {
            get { return new ColorPattern(m_Background, m_BackgroundStyle); }
            set { Changed = true;  m_Background = value.Color; m_BackgroundStyle = value.Pattern; } 
        }

        public Color Background { get { return m_Background; } set { Changed = true; m_Background = value; } }
        public HatchStyle BackgroundStyle { get { return m_BackgroundStyle; } set { Changed = true; m_BackgroundStyle = value; } }
        public Color Top { get { return m_Top; } set { Changed = true; m_Top = value; } }
        public Color Left { get { return m_Left; } set { Changed = true; m_Left = value; } }
        public Color Bottom { get { return m_Bottom; } set { Changed = true; m_Bottom = value; } }
        public Color Right { get { return m_Right; } set { Changed = true; m_Right = value; } }

        public MapSquareColors(MapLineInfo gridLines)
        {
            Background = Properties.Settings.Default.DefaultGridBackground;
            BackgroundStyle = HatchStyle.Percent90;
            Top = Left = Bottom = Right = gridLines.Color;
            Changed = false;
        }

        private bool SetColor(Stream stream, ref Color c)
        {
            byte[] buffer = new byte[sizeof(Int32)];
            int iLen = stream.Read(buffer, 0, buffer.Length);
            if (iLen < buffer.Length)
                return false;
            c = Color.FromArgb(BitConverter.ToInt32(buffer, 0));
            return true;
        }

        private void PutColor(Stream stream, Color c)
        {
            byte[] bytes = BitConverter.GetBytes(c.ToArgb());
            stream.Write(bytes, 0, bytes.Length);
        }

        public MapSquareColors(Stream stream)
        {
            Changed = true;
            SetColor(stream, ref m_Background);
            BackgroundStyle = (HatchStyle)stream.ReadByte();
            SetColor(stream, ref m_Top);
            SetColor(stream, ref m_Left);
            SetColor(stream, ref m_Bottom);
            SetColor(stream, ref m_Right);
        }

        public void Serialize(Stream stream)
        {
            PutColor(stream, Background);
            stream.WriteByte((byte)BackgroundStyle);
            PutColor(stream, Top);
            PutColor(stream, Left);
            PutColor(stream, Bottom);
            PutColor(stream, Right);
        }

        public void CopyFrom(MapSquareColors copy, MapReplaceMode mode = MapReplaceMode.Full)
        {
            if (mode.HasFlag(MapReplaceMode.Back))
            {
                m_Background = copy.Background;
                m_BackgroundStyle = copy.BackgroundStyle;
            }
            if (mode.HasFlag(MapReplaceMode.Lines))
            {
                m_Top = copy.Top;
                m_Bottom = copy.Bottom;
                m_Left = copy.Left;
                m_Right = copy.Right;
            }
            if (mode.HasFlag(MapReplaceMode.Back) || mode.HasFlag(MapReplaceMode.Lines))
                Changed = true;
        }
    }

    public class MapSquareLines
    {
        public bool Changed { get; set; }

        private byte m_TopWidth;
        private byte m_LeftWidth;
        private byte m_BottomWidth;
        private byte m_RightWidth;
        private byte m_TopPattern;
        private byte m_LeftPattern;
        private byte m_BottomPattern;
        private byte m_RightPattern;

        public byte TopWidth { get { return m_TopWidth; } set { Changed = true; m_TopWidth = value; } }
        public byte LeftWidth { get { return m_LeftWidth; } set { Changed = true; m_LeftWidth = value; } }
        public byte BottomWidth { get { return m_BottomWidth; } set { Changed = true; m_BottomWidth = value; } }
        public byte RightWidth { get { return m_RightWidth; } set { Changed = true; m_RightWidth = value; } }
        public byte TopPattern { get { return m_TopPattern; } set { Changed = true; m_TopPattern = value; } }
        public byte LeftPattern { get { return m_LeftPattern; } set { Changed = true; m_LeftPattern = value; } }
        public byte BottomPattern { get { return m_BottomPattern; } set { Changed = true; m_BottomPattern = value; } }
        public byte RightPattern { get { return m_RightPattern; } set { Changed = true; m_RightPattern = value; } }

        public MapSquareLines(MapLineInfo gridLines)
        {
            TopWidth = LeftWidth = BottomWidth = RightWidth = (byte) gridLines.Width;
            TopPattern = LeftPattern = RightPattern = BottomPattern = (byte) gridLines.Pattern;
            Changed = false;
        }

        private bool SetByte(Stream stream, ref byte b)
        {
            int i = stream.ReadByte();
            if (i == -1)
                return false;
            b = (byte)i;
            return true;
        }

        public MapSquareLines(Stream stream)
        {
            Changed = true;
            SetByte(stream, ref m_TopWidth);
            SetByte(stream, ref m_LeftWidth);
            SetByte(stream, ref m_BottomWidth);
            SetByte(stream, ref m_RightWidth);
            SetByte(stream, ref m_TopPattern);
            SetByte(stream, ref m_LeftPattern);
            SetByte(stream, ref m_BottomPattern);
            SetByte(stream, ref m_RightPattern);
        }

        public void Serialize(Stream stream)
        {
            stream.Write(new byte[] { m_TopWidth, m_LeftWidth, m_BottomWidth, m_RightWidth, m_TopPattern, m_LeftPattern, m_BottomPattern, m_RightPattern }, 0, 8);
        }

        public void CopyFrom(MapSquareLines copy)
        {
            m_TopWidth = copy.TopWidth;
            m_LeftWidth = copy.LeftWidth;
            m_BottomWidth = copy.BottomWidth;
            m_RightWidth = copy.RightWidth;
            m_TopPattern = copy.TopPattern;
            m_LeftPattern = copy.LeftPattern;
            m_BottomPattern = copy.BottomPattern;
            m_RightPattern = copy.RightPattern;
            Changed = true;
        }
    }

    public struct MapSquareDirty
    {
        public bool Up;
        public bool Down;
        public bool Left;
        public bool Right;
        public bool Back;

        public MapSquareDirty(bool all)
        {
            Up = Down = Left = Right = Back = all;
        }

        public bool Lines
        {
            get { return Up && Down && Left && Right; }
            set { Up = value; Down = value; Left = value; Right = value; }
        }

        public bool All
        {
            set
            {
                Up = Down = Left = Right = Back = value;
            }
        }

        public bool Any
        {
            get
            {
                return Up || Down || Left || Right || Back;
            }
        }
    }

    [Flags]
    public enum MapSquareFlags
    {
        None =    0x00,
        Visited = 0x01,
        Seen =    0x02,
        Live =    0x04,     // Always update this square from game memory when it changes
        VisitedSeen = Visited | Seen
    }

    public enum DirtyType
    {
        None,
        Up,
        Down,
        Left,
        Right,
        Back,
        All,
        Lines
    }

    public class MapSquare
    {
        private MapSquareColors m_colors;
        private MapSquareLines m_lines;
        public const int SizeHint = 24;
        public MapNote Note;
        public List<MapIcon> Icons;
        private MapSquareDirty m_dirty;
        public MapSquareFlags Flags;
        public bool m_bVisitedChanged = false;
        public bool m_bSeenChanged = false;
        public bool m_bLiveChanged = false;

        public MapSquareColors Colors { get { return m_colors; } }
        public MapSquareLines Lines { get { return m_lines; } }

        public bool BackDirty { get { return m_dirty.Back; } }
        public bool UpDirty { get { return m_dirty.Up; } }
        public bool LeftDirty { get { return m_dirty.Left; } }
        public bool RightDirty { get { return m_dirty.Right; } }
        public bool DownDirty { get { return m_dirty.Down; } }
        public bool AnyDirty { get { return m_dirty.Any; } }

        public void SetDirty(DirtyType type, bool bValue = true)
        {
            switch (type)
            {
                case DirtyType.Up:
                    m_dirty.Up = bValue;
                    break;
                case DirtyType.Down:
                    m_dirty.Down = bValue;
                    break;
                case DirtyType.Left:
                    m_dirty.Left = bValue;
                    break;
                case DirtyType.Right:
                    m_dirty.Right = bValue;
                    break;
                case DirtyType.Back:
                    m_dirty.Back = bValue;
                    break;
                case DirtyType.All:
                    m_dirty.All = bValue;
                    break;
                case DirtyType.Lines:
                    m_dirty.Up = bValue;
                    m_dirty.Down = bValue;
                    m_dirty.Left = bValue;
                    m_dirty.Right = bValue;
                    break;
                default:
                    break;
            }
        }

        public MapLineInfo Top 
        {
            get { return new MapLineInfo(Colors.Top, (DashStyle)Lines.TopPattern, Lines.TopWidth); }
            set { Colors.Top = value.Color; Lines.TopPattern = (byte)value.Pattern; Lines.TopWidth = (byte)value.Width; }
        }

        public MapLineInfo Bottom 
        { 
            get { return new MapLineInfo(Colors.Bottom, (DashStyle)Lines.BottomPattern, Lines.BottomWidth); }
            set { Colors.Bottom = value.Color; Lines.BottomPattern = (byte)value.Pattern; Lines.BottomWidth = (byte)value.Width; }
        }

        public MapLineInfo Left 
        {
            get { return new MapLineInfo(Colors.Left, (DashStyle)Lines.LeftPattern, Lines.LeftWidth); }
            set { Colors.Left = value.Color; Lines.LeftPattern = (byte)value.Pattern; Lines.LeftWidth = (byte)value.Width; }
        }

        public MapLineInfo Right
        {
            get { return new MapLineInfo(Colors.Right, (DashStyle)Lines.RightPattern, Lines.RightWidth); }
            set { Colors.Right = value.Color; Lines.RightPattern = (byte)value.Pattern; Lines.RightWidth = (byte)value.Width; }
        }

        public MapLineInfo LineInfo(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return Top;
                case Direction.Down: return Bottom;
                case Direction.Left: return Left;
                case Direction.Right: return Right;
                default: return null;
            }
        }

        public void Line(Direction dir, MapLineInfo info, byte iWidth, bool bSetDirty = true) { Line(dir, new MapLineInfo(info.Color, info.Pattern, iWidth), bSetDirty); }

        public void Line(Direction dir, MapLineInfo info, bool bSetDirty = true)
        {
            switch (dir)
            {
                case Direction.Up: 
                    Top = info;
                    if (bSetDirty)
                        SetDirty(DirtyType.Up);
                    break;
                case Direction.Down:
                    Bottom = info;
                    if (bSetDirty)
                        SetDirty(DirtyType.Down);
                    break;
                case Direction.Left:
                    Left = info;
                    if (bSetDirty)
                        SetDirty(DirtyType.Left);
                    break;
                case Direction.Right:
                    Right = info;
                    if (bSetDirty)
                        SetDirty(DirtyType.Right);
                    break;
                default:
                    break;
            }
        }

        public void SetDirty(Direction dir, bool bDirty = true)
        {
            switch (dir)
            {
                case Direction.Up:
                    SetDirty(DirtyType.Up, bDirty);
                    break;
                case Direction.Down:
                    SetDirty(DirtyType.Down, bDirty);
                    break;
                case Direction.Left:
                    SetDirty(DirtyType.Left, bDirty);
                    break;
                case Direction.Right:
                    SetDirty(DirtyType.Right, bDirty);
                    break;
                default:
                    break;
            }
        }

        public MapLineInfo AllLines
        {
            set { Top = value; Bottom = value; Left = value; Right = value; }
        }

        public bool ChangedSinceSave
        {
            get
            {
                return m_colors.Changed || m_lines.Changed || m_bVisitedChanged || m_bSeenChanged || m_bLiveChanged;
            }

            set
            {
                m_colors.Changed = value;
                m_lines.Changed = value;
            }
        }

        public MapSquare Clone()
        {
            MemoryStream stream = new MemoryStream();
            Serialize(stream);
            stream.Seek(0, SeekOrigin.Begin);
            return new MapSquare(stream);
        }

        public MapSquare Clone(MapSquare squareDeleting)
        {
            MemoryStream stream = new MemoryStream();
            Serialize(stream);
            stream.Seek(0, SeekOrigin.Begin);
            MapSquare square = new MapSquare(stream);
            square.Note = squareDeleting.Note;
            square.Icons = squareDeleting.Icons;
            return square;
        }

        public MapSquare(MapLineInfo gridLines)
        {
            Clear(gridLines);
        }

        public bool Visited { get { return (Flags & MapSquareFlags.Visited) > 0; } }
        public bool Seen { get { return (Flags & MapSquareFlags.Seen) > 0; } }
        public bool Live { get { return (Flags & MapSquareFlags.Live) > 0; } }

        public bool SetFlags(MapSquareFlags flags)
        {
            return SetSeen(flags.HasFlag(MapSquareFlags.Seen)) || SetVisited(flags.HasFlag(MapSquareFlags.Visited));
        }

        public bool SetVisitedAndSeen(bool bSet)
        {
            MapSquareFlags flagsOld = Flags;
            if (bSet)
                Flags |= MapSquareFlags.VisitedSeen;
            else
                Flags &= ~MapSquareFlags.VisitedSeen;
            bool bChanged = (flagsOld != Flags);
            m_bVisitedChanged |= bChanged;
            return bChanged;
        }

        public bool SetVisited(bool bVisited)
        {
            bool bOldValue = Visited;
            if (bVisited)
            {
                SetSeen(true);  // Visited squared are by definition also seen
                Flags |= MapSquareFlags.Visited;
            }
            else
                Flags &= ~MapSquareFlags.Visited;
            bool bChanged = (bOldValue != bVisited);
            m_bVisitedChanged |= bChanged;
            return bChanged;
        }

        public bool SetLive(bool bLive)
        {
            bool bOldValue = Live;
            if (bLive)
                Flags |= MapSquareFlags.Live;
            else
                Flags &= ~MapSquareFlags.Live;
            bool bChanged = (bOldValue != bLive);
            m_bLiveChanged |= bChanged;
            return bChanged;
        }

        public bool SetSeen(bool bSeen)
        {
            bool bOldValue = Seen;
            if (bSeen)
                Flags |= MapSquareFlags.Seen;
            else
                Flags &= ~MapSquareFlags.Seen;
            bool bChanged = (bOldValue != bSeen);
            m_bSeenChanged |= bChanged;
            return bChanged;
        }

        public MapSquare(Stream stream) : this(stream, Global.NullPoint) { }

        public MapSquare(Stream stream, Point ptNoteIcons)
        {
            if (Icons == null)
                Icons = new List<MapIcon>();
            m_colors = new MapSquareColors(stream);
            m_lines= new MapSquareLines(stream);
            Flags = (MapSquareFlags) stream.ReadByte();
            if (ptNoteIcons != Global.NullPoint)
            {
                bool bNote = stream.ReadByte() == 1;
                if (bNote)
                {
                    Note = new MapNote(stream);
                    Note.Location = ptNoteIcons;
                }
                byte count = (byte) stream.ReadByte();
                Icons = new List<MapIcon>(count);
                for(int i = 0; i < count; i++)
                {
                    MapIcon icon = new MapIcon(stream);
                    icon.Location = ptNoteIcons;
                    Icons.Add(icon);
                }
            }
            m_dirty = new MapSquareDirty();
            ChangedSinceSave = true;
        }

        public void Serialize(Stream stream, bool bIncludeIconsNotes = false)
        {
            Colors.Serialize(stream);
            Lines.Serialize(stream);
            stream.WriteByte((byte)Flags);
            if (bIncludeIconsNotes)
            {
                stream.WriteByte((byte) (Note == null ? 0 : 1));
                if (Note != null)
                    Note.Serialize(stream);
                byte count = (byte) Math.Min(Icons.Count, byte.MaxValue);
                stream.WriteByte(count);
                for (int i = 0; i < count; i++)
                    Icons[i].Serialize(stream);
            }
        }

        public void Offset(int iHoriz, int iVert)
        {
            if (Note != null && Note.Location != Global.NullPoint)
            {
                Note.Location.X += iHoriz;
                Note.Location.Y += iVert;
            }

            if (Icons != null)
            {
                foreach (MapIcon icon in Icons)
                {
                    if (icon.Location != Global.NullPoint)
                    {
                        icon.Location.X += iHoriz;
                        icon.Location.Y += iVert;
                    }
                }
            }
        }

        public bool HasIcon(IconName name, Direction dir)
        {
            return Icons != null && Icons.Any(i => i.Name == name && i.Orientation == dir);
        }

        public void AddUniqueIcon(MapIcon icon)
        {
            if (HasIcon(icon.Name, icon.Orientation))
                return;

            if (Icons == null)
                Icons = new List<MapIcon>(1);
            Icons.Add(icon);
        }

        public void RemoveIcons(IconName name, Direction dir = Direction.All)
        {
            List<int> removeIcons = new List<int>();
            for (int i = Icons.Count - 1; i >= 0; i--)
            {
                if (Icons[i].Name == name && (dir == Direction.All || dir == Icons[i].Orientation))
                    removeIcons.Add(i);
            }

            foreach (int i in removeIcons)
                Icons.RemoveAt(i);
        }

        public static bool CompareEdges(MapSquare square1, MapSquare square2, Direction dir)
        {
            // Returns true if the edges are the same

            if (square1 == null || square2 == null)
                return false;

            switch (dir)
            {
                case Direction.Up:
                    return square1.Colors.Top.ToArgb() == square2.Colors.Bottom.ToArgb() && 
                        square1.Lines.TopPattern == square2.Lines.BottomPattern && 
                        square1.Lines.TopWidth == square2.Lines.BottomWidth;
                case Direction.Right:
                    return square1.Colors.Right.ToArgb() == square2.Colors.Left.ToArgb() &&
                        square1.Lines.RightPattern == square2.Lines.LeftPattern &&
                        square1.Lines.RightWidth == square2.Lines.LeftWidth;
                case Direction.Down:
                    return square1.Colors.Bottom.ToArgb() == square2.Colors.Top.ToArgb() &&
                        square1.Lines.BottomPattern == square2.Lines.TopPattern &&
                        square1.Lines.BottomWidth == square2.Lines.TopWidth;
                case Direction.Left:
                    return square1.Colors.Left.ToArgb() == square2.Colors.Right.ToArgb() &&
                        square1.Lines.LeftPattern == square2.Lines.RightPattern &&
                        square1.Lines.LeftWidth == square2.Lines.RightWidth;
                default:
                    return false;
            }
        }

        public bool IsUnimportant { get { return Global.SquareStyles.IsMatch(this); } }

        public void DrawIconsAndNotes(bool bMask, Graphics g, Rectangle rcFull, EditFlags flags)
        {
            DrawIconsAndNotes(bMask, g, rcFull, rcFull.Left, rcFull.Top, flags);
        }

        public static void DrawIcon(Graphics g, MapIcon icon, Rectangle rcFull, EditFlags flags, bool bMask)
        {
            if (icon.HintNorth && flags.HideNorth)
                return;
            if (icon.HintSouth && flags.HideSouth)
                return;
            if (icon.HintEast && flags.HideEast)
                return;
            if (icon.HintWest && flags.HideWest)
                return;

            if (icon.Immaterial && flags.HideImmaterial)
                return;

            if (bMask)
                DrawIcon(g, icon, rcFull, flags, Global.MaskColor);
            else if (icon.Color.ToArgb() != Color.Black.ToArgb())
                DrawIcon(g, icon, rcFull, flags, icon.Color);
            else
                DrawIcon(g, icon, rcFull, flags);
        }

        public static void DrawIcon(Graphics g, MapIcon icon, Rectangle rcFull, EditFlags flags)
        {
            if (icon.Name == IconName.None)
                return;

            Bitmap bmp = GetDrawIconBitmap(g, icon, ref rcFull, flags);
            g.DrawImage(bmp, rcFull);
        }

        public static void DrawIcon(Graphics g, MapIcon icon, Rectangle rcFull, EditFlags flags, Color colorReplaceBlack)
        {
            if (icon.Name == IconName.None)
                return;

            Bitmap bmp = GetDrawIconBitmap(g, icon, ref rcFull, flags);
            g.DrawImage(bmp, rcFull, 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel, Global.ReplaceColor(Color.Black, colorReplaceBlack));
        }

        public static void DrawNote(Graphics g, MapNote note, Rectangle rcFull, EditFlags flags, Color color)
        {
            if (note == null || note.Symbol == "." || note.Symbol == "")
                return;

            int iSize = Math.Min(rcFull.Height, rcFull.Width) * 3 / 5;
            Font font = new Font(FontFamily.GenericSansSerif, iSize, FontStyle.Regular, GraphicsUnit.Pixel);
            SizeF szf = g.MeasureString(note.Symbol, font);
            Size szLines = new Size((rcFull.Width - 1) / (Properties.Settings.Default.ZoomLineScale / 2) - 1,
                (rcFull.Height - 1) / (Properties.Settings.Default.ZoomLineScale / 2) - 1);
            if (note.Symbol != ".") // Not a "hidden" symbol
                g.DrawString(note.Symbol, font, new SolidBrush(color),
                    rcFull.Left + (rcFull.Width - szf.Width) / 2 + szLines.Width,
                    rcFull.Top + (rcFull.Height - szf.Height) / 2 + szLines.Height);
        }

        public static void DrawLive(Graphics g, Rectangle rcFull)
        {
            Brush brush = new HatchBrush(HatchStyle.SmallCheckerBoard, Color.Green, Color.LightBlue);
            int iPart = 7;
            int iMargin = Math.Max(1, rcFull.Width / 8 - 1);
            int iMargin2 = Math.Max(1, iMargin * 2 - 2);

            g.FillRectangle(brush, new Rectangle(rcFull.Left + iMargin, rcFull.Top + iMargin, rcFull.Width - iMargin2, rcFull.Height / iPart + iMargin));
            g.FillRectangle(brush, new Rectangle(rcFull.Left + iMargin, rcFull.Top + iMargin, rcFull.Width / iPart + iMargin, rcFull.Height - iMargin2));
            g.FillRectangle(brush, new Rectangle(rcFull.Right - (rcFull.Width / iPart + iMargin), rcFull.Top + iMargin, rcFull.Width / iPart + iMargin, rcFull.Height - iMargin2));
            g.FillRectangle(brush, new Rectangle(rcFull.Left + iMargin, rcFull.Bottom - (rcFull.Height / iPart + iMargin), rcFull.Width - iMargin2, rcFull.Height / iPart + iMargin));
        }

        public static Bitmap GetDrawIconBitmap(Graphics g, MapIcon icon, ref Rectangle rc, EditFlags flags)
        {
            Bitmap bmpIcon = Global.IconCache.GetIconBitmap(icon, rc.Size);

            // Some orientations look better with the 24x24 or 48x48 icon size if they are pushed one more pixel outward
            if ((rc.Width >= 24 && rc.Width < 32) || rc.Width > 32)
            {
                if (icon.Orientation == Direction.Right)
                    rc.Offset(rc.Width / 32 + 1, 0);
                else if (icon.Orientation == Direction.Down)
                    rc.Offset(0, rc.Height / 32 + 1);
            }
            return bmpIcon;
        }

        public void DrawIconsAndNotes(bool bMask, Graphics g, Rectangle rcFull, int iLeft, int iTop, EditFlags flags)
        {
            if (flags.Icons && Icons != null)
            {
                // Show icons and notes only if the square is unhidden or we specifically set the ShowUnvisitedNotes option
                foreach (MapIcon icon in Icons)
                    DrawIcon(g, icon, rcFull, flags, bMask);
            }

            if (flags.Notes && Note != null && !flags.HideImmaterial)
                DrawNote(g, Note, rcFull, flags, bMask ? Global.MaskColor : Note.Moving ? Color.FromArgb(128, Note.Color) : Note.Color);
        }

        public void Clear(MapLineInfo gridLines, bool bNotes = false, bool bIcons = false)
        {
            Icons = new List<MapIcon>();
            m_colors = new MapSquareColors(gridLines);
            m_lines = new MapSquareLines(gridLines);
            SetVisited(true);
            SetDirty(DirtyType.All);
            if (bNotes)
                Note = null;
            if (bIcons)
                Icons = new List<MapIcon>();
            ChangedSinceSave = true;
        }

        public bool Fill(DrawColor dc)
        {
            if (Colors.Background.ToArgb() == dc.color.ToArgb() && Colors.BackgroundStyle == dc.styleBlock)
                return false;
            Colors.Background = dc.color;
            Colors.BackgroundStyle = dc.styleBlock;
            SetDirty(DirtyType.Back);
            return true;
        }

        public bool HasLine(Direction dir, MapLineInfo gridLines)
        {
            switch (dir)
            {
                case Direction.Down: return !Bottom.Equals(gridLines);
                case Direction.Left: return !Left.Equals(gridLines);
                case Direction.Right: return !Right.Equals(gridLines);
                case Direction.Up: return !Top.Equals(gridLines);
                default:
                    break;
            }
            return false;
        }

        public bool HasSightBlockingLine(GameNames game, MapSheet sheet, Direction dir, MapLineInfo gridLines)
        {
            switch (dir)
            {
                case Direction.Down: return !(Bottom.Equals(gridLines) || !Games.IsSightBlocking(game, sheet, Bottom, Icons, dir));
                case Direction.Left: return !(Left.Equals(gridLines) || !Games.IsSightBlocking(game, sheet, Left, Icons, dir));
                case Direction.Right: return !(Right.Equals(gridLines) || !Games.IsSightBlocking(game, sheet, Right, Icons, dir));
                case Direction.Up: return !(Top.Equals(gridLines) || !Games.IsSightBlocking(game, sheet, Top, Icons, dir));
                default: break;
            }
            return false;
        }

        public bool Empty()
        {
            if (Colors.Background == Properties.Settings.Default.DefaultGridBackground)
                return false;
            Colors.Background = Properties.Settings.Default.DefaultGridBackground;
            Colors.BackgroundStyle = Properties.Settings.Default.DefaultGridBackgroundStyle;
            SetDirty(DirtyType.Back);
            return true;
        }

        public bool IsUnused(MapLineInfo gridLines)
        {
            if (!IsEmpty)
                return false;
            int iColor = gridLines.Color.ToArgb();
            if (Colors.Bottom.ToArgb() != iColor)
                return false;
            if (Colors.Top.ToArgb() != iColor)
                return false;
            if (Colors.Left.ToArgb() != iColor)
                return false;
            if (Colors.Right.ToArgb() != iColor)
                return false;
            byte bStyle = (byte)gridLines.Pattern;
            if (Lines.BottomPattern != bStyle)
                return false;
            if (Lines.TopPattern != bStyle)
                return false;
            if (Lines.LeftPattern != bStyle)
                return false;
            if (Lines.RightPattern != bStyle)
                return false;
            byte bWidth = (byte) gridLines.Width;
            if (Lines.BottomWidth != bWidth)
                return false;
            if (Lines.TopWidth != bWidth)
                return false;
            if (Lines.LeftWidth != bWidth)
                return false;
            if (Lines.RightWidth != bWidth)
                return false;
            if (Note != null && !String.IsNullOrEmpty(Note.Symbol))
                return false;
            if (Icons.Count > 0)
                return false;
            return true;
        }

        public bool IsEmpty
        {
            get
            {
                return (Colors.Background.ToArgb() == Properties.Settings.Default.DefaultGridBackground.ToArgb());
            }
        }

        public void CopyGridDataFrom(MapSquare copy, MapReplaceMode mode)
        {
            Colors.CopyFrom(copy.Colors, mode);
            if (mode.HasFlag(MapReplaceMode.Lines))
                Lines.CopyFrom(copy.Lines);
            SetDirty(DirtyType.All);
        }

        public void CopyLinesFrom(MapSquare copy, MapLineInfo infoReplace = null, MapLineInfo infoSkip = null)
        {
            if (!copy.Top.Equals(infoSkip))
            {
                Top = copy.Top;
                SetDirty(DirtyType.Up);
                if (infoReplace != null)
                {
                    copy.Top = infoReplace;
                    copy.SetDirty(DirtyType.Up);
                }
            }

            if (!copy.Left.Equals(infoSkip))
            {
                Left = copy.Left;
                SetDirty(DirtyType.Left);
                if (infoReplace != null)
                {
                    copy.Left = infoReplace;
                    copy.SetDirty(DirtyType.Left);
                }
            }

            if (!copy.Right.Equals(infoSkip))
            {
                Right = copy.Right;
                SetDirty(DirtyType.Right);
                if (infoReplace != null)
                {
                    copy.Right = infoReplace;
                    copy.SetDirty(DirtyType.Right);
                }
            }

            if (!copy.Bottom.Equals(infoSkip))
            {
                Bottom = copy.Bottom;
                SetDirty(DirtyType.Down);
                if (infoReplace != null)
                {
                    copy.Bottom = infoReplace;
                    copy.SetDirty(DirtyType.Down);
                }
            }
        }

        public void CopyLineFrom(Direction dir, MapSquare copy, MapLineInfo infoReplace = null, MapLineInfo infoSkip = null)
        {
            switch (dir)
            {
                case Direction.Up:
                    if (copy.Top.Equals(infoSkip))
                        return;
                    Top = copy.Top;
                    SetDirty(DirtyType.Up);
                    if (infoReplace != null)
                        copy.Top = infoReplace;
                        copy.SetDirty(DirtyType.Up);
                    break;
                case Direction.Left:
                    if (copy.Left.Equals(infoSkip))
                        return;
                    Left = copy.Left;
                    SetDirty(DirtyType.Left);
                    if (infoReplace != null)
                        copy.Left = infoReplace;
                        copy.SetDirty(DirtyType.Left);
                    break;
                case Direction.Right:
                    if (copy.Right.Equals(infoSkip))
                        return;
                    Right = copy.Right;
                    SetDirty(DirtyType.Right);
                    if (infoReplace != null)
                        copy.Right = infoReplace;
                        copy.SetDirty(DirtyType.Right);
                    break;
                case Direction.Down:
                    if (copy.Bottom.Equals(infoSkip))
                        return;
                    Bottom = copy.Bottom;
                    SetDirty(DirtyType.Down);
                    if (infoReplace != null)
                        copy.Bottom = infoReplace;
                        copy.SetDirty(DirtyType.Down);
                    break;
                default:
                    break;
            }
        }

        public void CopyBackFrom(MapSquare copy, bool bDeleteSource = false, bool bKeepIfEmpty = false)
        {
            if (!bKeepIfEmpty || !copy.Colors.BackColorPattern.Equals(Global.DefaultGridBackground))
            {
                Colors.Background = copy.Colors.Background;
                Colors.BackgroundStyle = copy.Colors.BackgroundStyle;
                SetDirty(DirtyType.Back);
            }
            if (bDeleteSource)
            {
                copy.Colors.Background = Properties.Settings.Default.DefaultGridBackground;
                copy.Colors.BackgroundStyle = Properties.Settings.Default.DefaultGridBackgroundStyle;
                copy.SetDirty(DirtyType.Back);
            }
        }

        public void CopyNoteFrom(MapSquare copy, Point ptNew, bool bKeepIfEmpty)
        {
            if (copy.Note == null)
            {
                if (!bKeepIfEmpty)
                    Note = null;
                return;
            }

            Note = copy.Note.Clone(ptNew);
            Note.Location = ptNew;
            SetDirty(DirtyType.Back);
        }

        public void CopyIconsFrom(MapSquare copy, Point ptNew, bool bKeepIfEmpty, MapReplaceMode mode)
        {
            if (copy.Icons == null && !bKeepIfEmpty)
            {
                Icons = null;
                return;
            }

            if (mode.HasFlag(MapReplaceMode.AllIcons) || Icons == null)
            {
                if (!bKeepIfEmpty || copy.Icons.Count > 0 || Icons == null)
                    Icons = new List<MapIcon>(copy.Icons.Count);
            }
            else
            {
                for (int i = Icons.Count - 1; i >= 0; i--)
                {
                    if (mode.HasFlag(MapReplaceMode.WallIcons) && Icons[i].IsWallIcon)
                        Icons.RemoveAt(i);
                    else if (mode.HasFlag(MapReplaceMode.SquareIcons) && !Icons[i].IsWallIcon)
                        Icons.RemoveAt(i);
                }
            }
            foreach (MapIcon icon in copy.Icons)
                Icons.Add(icon.Clone(ptNew));
            SetDirty(DirtyType.Back);
        }

        public void ReplaceLines(MapLineInfo infoOld, MapLineInfo infoNew)
        {
            if (Top.Equals(infoOld))
            {
                Colors.Top = infoNew.Color;
                Lines.TopPattern = (byte)infoNew.Pattern;
                Lines.TopWidth = (byte) infoNew.Width;
            }
            if (Left.Equals(infoOld))
            {
                Colors.Left = infoNew.Color;
                Lines.LeftPattern = (byte)infoNew.Pattern;
                Lines.LeftWidth = (byte)infoNew.Width;
            }
            if (Right.Equals(infoOld))
            {
                Colors.Right = infoNew.Color;
                Lines.RightPattern = (byte)infoNew.Pattern;
                Lines.RightWidth = (byte)infoNew.Width;
            }
            if (Bottom.Equals(infoOld))
            {
                Colors.Bottom = infoNew.Color;
                Lines.BottomPattern = (byte)infoNew.Pattern;
                Lines.BottomWidth = (byte)infoNew.Width;
            }
        }

        public void Rotate(RotateFlipType rotate)
        {
            MapLineInfo saveTop = Top;
            MapLineInfo saveLeft = Left;
            switch (rotate)
            {
                case RotateFlipType.Rotate90FlipNone:
                    Top = Left;
                    Left = Bottom;
                    Bottom = Right;
                    Right = saveTop;
                    if (Icons != null)
                        foreach (MapIcon icon in Icons)
                            icon.Orientation = Global.Rotate(icon.Orientation, true);
                    break;
                case RotateFlipType.Rotate270FlipNone:
                    Top = Right;
                    Right = Bottom;
                    Bottom = Left;
                    Left = saveTop;
                    if (Icons != null)
                        foreach (MapIcon icon in Icons)
                            icon.Orientation = Global.Rotate(icon.Orientation, false);
                    break;
                case RotateFlipType.Rotate180FlipNone:
                    Top = Bottom;
                    Bottom = saveTop;
                    Left = Right;
                    Right = saveLeft;
                    if (Icons != null)
                        foreach (MapIcon icon in Icons)
                            icon.Orientation = Global.Opposite(icon.Orientation);
                    break;
                case RotateFlipType.RotateNoneFlipX:
                    Left = Right;
                    Right = saveLeft;
                    if (Icons != null)
                        foreach (MapIcon icon in Icons)
                        {
                            switch (icon.Orientation)
                            {
                                case Direction.Left:
                                case Direction.Right:
                                    icon.Orientation = Global.Opposite(icon.Orientation);
                                    break;
                                default:
                                    break;
                            }
                        }
                    break;
                case RotateFlipType.RotateNoneFlipY:
                    Top = Bottom;
                    Bottom = saveTop;
                    if (Icons != null)
                        foreach (MapIcon icon in Icons)
                        {
                            switch (icon.Orientation)
                            {
                                case Direction.Up:
                                case Direction.Down:
                                    icon.Orientation = Global.Opposite(icon.Orientation);
                                    break;
                                default:
                                    break;
                            }
                        }
                    break;
                default:
                    break;
            }
        }

        public void AddNoteLine(string strNote, string strSymbol, Point pt)
        {
            if (Note == null || String.IsNullOrWhiteSpace(Note.Text))
                Note = new MapNote(strNote, Color.Black, strSymbol, pt);
            else
            {
                Note.Text += String.Format("{0}{1}", Note.Text.EndsWith("\n") ? "" : "\r\n", strNote);
                if (Note.Symbol == "?")
                    Note.Symbol = strSymbol;
            }
        }
    }

    public struct SheetTag
    {
        public int Index;

        public SheetTag(int index)
        {
            Index = index;
        }
    }

    public class ItemBag
    {
        public List<Item> Items;
        public GameNames Game;

        public override string ToString() { return String.Format("Items: {0}", Items == null ? 0 : Items.Count); }

        public string SerializeToString()
        {
            if (Items == null || Items.Count == 0)
                return String.Empty;

            StringBuilder sb = new StringBuilder(Items.Count * 6 + 2);
            sb.AppendFormat("{0:X2}", (byte) Game);
            sb.AppendFormat("{0:X2}", (byte) Items[0].Serialize().Length);    // Number of bytes per item
            foreach (Item item in Items)
            {
                sb.AppendFormat(Global.ByteString(item.Serialize(), false));
            }
            return sb.ToString();
        }

        public ItemBag()
        {
            Items = new List<Item>();
            Game = GameNames.None;
        }

        public ItemBag(string strItems)
        {
            if (strItems.Length < 6)
                return;

            Game = (GameNames)Global.StringToByte(strItems.Substring(0,2));
            int iSize = Global.StringToByte(strItems.Substring(2, 2)) * 2;      // Size in characters (2 per byte)
            Items = new List<Item>((strItems.Length - 4) / iSize);

            for (int i = 4; i < strItems.Length; i += iSize)
            {
                byte[] bytes = Global.StringToBytes(strItems.Substring(i, iSize));
                switch (Game)
                {
                    case GameNames.MightAndMagic1:
                        Items.Add(MM1Item.FromBagBytes(bytes));
                        break;
                    case GameNames.MightAndMagic2:
                        Items.Add(MM2Item.FromBagBytes(bytes));
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class MapBook
    {
        public String Title;
        public List<MapSheet> Sheets;
        public LocationSettings Location;
        public DoorType QuickDoor;
        public ItemBag BagOfHolding;
        public string[] FlaggedQuests;
        public string[] ManualCompletedQuests;
        public string[] ManualCompletedTasks;
        public MapLineInfo GridLines;
        public string BookNote;
        public ColorPattern UnvisitedPattern;
        private string m_strLastFile = String.Empty;

        public string LastFile
        {
            get { return m_strLastFile; }
            set
            {
                m_strLastFile = value;
                try
                {
                    string strDir = value;
                    if (value.StartsWith(":"))
                        strDir = System.Reflection.Assembly.GetEntryAssembly().GetName().CodeBase;
                    strDir = Path.GetDirectoryName(strDir);
                    foreach (MapSheet sheet in Sheets)
                        sheet.DefaultDirectory = strDir;
                }
                catch (Exception)
                {
                    // Don't throw exceptions in property setter
                }
            }
        }

        public void SetDefaults()
        {
            Location = new LocationSettings();
            QuickDoor = DoorType.StraddleWall;
            BagOfHolding = new ItemBag();
            GridLines = Global.DefaultGridLineInfo;
            UnvisitedPattern = DefaultUnvisitedPattern;
        }

        public static ColorPattern DefaultUnvisitedPattern { get { return new ColorPattern(Color.LightGray, HatchStyle.LargeConfetti, Color.DarkGray); } }

        public MapBook()
        {
            SetDefaults();
            Sheets = new List<MapSheet>(1);
            Sheets.Add(new MapSheet(GridLines));
        }

        public bool SetIfAnyDifferent(ref string[] original, string[] current)
        {
            if (Global.NullOrEmpty(original) && Global.NullOrEmpty(current))
                return false;
            if (Global.NullOrEmpty(current))
            {
                original = null;
                return true;
            }
            if (Global.NullOrEmpty(original) || current.Length != original.Length)
            {
                original = current;
                return true;
            }
            for (int i = 0; i < current.Length; i++)
            {
                if (original[i] != current[i])
                {
                    original = current;
                    return true;
                }
            }
            return false;
        }

        public bool SetFlaggedQuests(string[] quests)
        {
            return SetIfAnyDifferent(ref FlaggedQuests, quests);
        }

        public bool SetManualCompletions(string[] quests, string[] tasks)
        {
            bool bAnyDifferent = SetIfAnyDifferent(ref ManualCompletedQuests, quests);
            bAnyDifferent = bAnyDifferent || SetIfAnyDifferent(ref ManualCompletedTasks, tasks);
            return bAnyDifferent;
        }

        public MapBook(List<MapSheet> sheets)
        {
            SetDefaults();
            Sheets = sheets;
        }

        public bool UpdateVisited(MapSheet sheet, MapCartography cart, bool bPermitUnvisiting)
        {
            // Set visited/unvisited based on cartography bits from the game
            if (sheet == null || cart == null)
                return false;

            if (sheet.GameMapIndex != cart.MapIndex)
                return false; // Not the right cartography data; don't use it

            bool bAnyChanged = false;

            for (int y = 0; y < cart.MapSize.Height; y++)
            {
                for (int x = 0; x < cart.MapSize.Width; x++)
                {
                    Point pt = TranslateLocationToMap(new Point(x, y), sheet);
                    MapSquare square = sheet.GetSquareAtGridPoint(pt);
                    if (square != null)
                    {
                        bool bVisited = cart.IsVisited(x, y) || (!bPermitUnvisiting && square.Visited);
                        if (sheet.SetVisited(pt, bVisited))
                            bAnyChanged = true;
                        if (cart.SupportsSeen)
                        {
                            bool bSeen = cart.IsSeen(x, y);
                            if (bVisited)
                                bSeen = true;   // Can't not-see someplace that's visited
                            if (sheet.SetSeen(pt, bSeen))
                                bAnyChanged = true;
                        }
                    }
                }
            }

            return bAnyChanged;
        }

        public MonsterLocations TranslateLocationToMap(MonsterLocations monsters, MapSheet sheet)
        {
            MonsterLocations monstersNew = new MonsterLocations();

            foreach (MonsterPosition monster in monsters.MonsterPositions.Values)
            {
                MonsterPosition pos = new MonsterPosition(monster);
                pos.Position = TranslateLocationToMap(pos.Position, sheet);
                monstersNew.MonsterPositions.Add(pos.Position, pos);
            }

            return monstersNew;
        }

        public Rectangle TranslateLocationToMap(Rectangle rc, MapSheet sheet)
        {
            rc.Location = TranslateLocationToMap(rc.Location, sheet);
            return rc;
        }

        public Rectangle TranslateLocationFromMap(Rectangle rc, MapSheet sheet)
        {
            rc.Location = TranslateLocationFromMap(rc.Location, sheet);
            return rc;
        }

        public Point TranslateLocationToMap(Point pt, MapSheet sheet)
        {
            MapSection section = sheet.PointInSectionTarget(Location, pt);
            if (section != null)
            {
                // Use this section's explicit translation instead of the general offsets
                int iDeltaX = (pt.X - section.Target.X) * (Location.IncreaseX == AxisIncreaseX.RightToLeft ? -1 : 1);
                int iDeltaY = (pt.Y - section.Target.Y) * (Location.IncreaseY == AxisIncreaseY.BottomToTop ? -1 : 1);

                pt = new Point(section.Source.X + iDeltaX, section.Source.Y + iDeltaY);
            }
            else
            {
                pt.X -= Location.OffsetX;
                pt.Y -= Location.OffsetY;
                if (Location.IncreaseX == AxisIncreaseX.RightToLeft)
                    pt.X = (sheet.GridWidth - 1 - pt.X);
                if (Location.IncreaseY == AxisIncreaseY.BottomToTop)
                    pt.Y = (sheet.GridHeight - 1 - pt.Y);
                pt = sheet.TranslateLocation(pt);
            }
            return pt;
        }

        public Point TranslateLocationFromMap(Point pt, MapSheet sheet)
        {
            MapSection section = sheet.PointInSection(pt);
            if (section != null)
            {
                // Use this section's explicit translation instead of the general offsets
                int iDeltaX = (pt.X - section.Source.X) * (Location.IncreaseX == AxisIncreaseX.RightToLeft ? -1 : 1);
                int iDeltaY = (pt.Y - section.Source.Y) * (Location.IncreaseY == AxisIncreaseY.BottomToTop ? -1 : 1);

                pt = new Point(section.Target.X + iDeltaX, section.Target.Y + iDeltaY);
            }
            else
            {
                pt = sheet.TranslateLocation(pt);

                if (Location.IncreaseX == AxisIncreaseX.RightToLeft)
                    pt.X = (sheet.GridWidth - 1 - pt.X);
                if (Location.IncreaseY == AxisIncreaseY.BottomToTop)
                    pt.Y = (sheet.GridHeight - 1 - pt.Y);

                pt.X += Location.OffsetX;
                pt.Y += Location.OffsetY;
            }

            return pt;
        }

        public PointF TranslateLocationToMap(PointF pt, MapSheet sheet)
        {
            pt.X -= Location.OffsetX;
            pt.Y -= Location.OffsetY;
            if (Location.IncreaseX == AxisIncreaseX.RightToLeft)
                pt.X = (sheet.GridWidth - 1 - pt.X);
            if (Location.IncreaseY == AxisIncreaseY.BottomToTop)
                pt.Y = (sheet.GridHeight - 1 - pt.Y);
            pt = sheet.TranslateLocation(pt);
            return pt;
        }

        public PointF TranslateLocationFromMap(PointF pt, MapSheet sheet)
        {
            pt = sheet.TranslateLocation(pt);
            if (Location.IncreaseX == AxisIncreaseX.RightToLeft)
                pt.X = (sheet.GridWidth - 1 - pt.X);
            if (Location.IncreaseY == AxisIncreaseY.BottomToTop)
                pt.Y = (sheet.GridHeight - 1 - pt.Y);
            pt.X += Location.OffsetX;
            pt.Y += Location.OffsetY;
            return pt;
        }

        public MapSheetPathInfo[] GetMenuPaths()
        {
            List<MapSheetPathInfo> paths = new List<MapSheetPathInfo>(Sheets.Count);

            foreach (MapSheet sheet in Sheets)
            {
                paths.Add(new MapSheetPathInfo(sheet, sheet.MenuPath == null ? "\\" : sheet.MenuPath, sheet.SortIndex, sheet.DefaultZoom));
            }

            return paths.ToArray();
        }

        public void SetMenuPaths(MapSheetPathInfo[] paths)
        {
            foreach(MapSheetPathInfo info in paths)
            {
                info.Sheet.MenuPath = info.Path;
                info.Sheet.SortIndex = info.Index;
                info.Sheet.DefaultZoom = info.Zoom;
            }
            //Sheets.Sort(new MapSheetComparer(paths));
        }

        public void ReplaceLines(MapLineInfo infoOld, MapLineInfo infoNew, bool bGrid = false)
        {
            foreach (MapSheet sheet in Sheets)
            {
                sheet.ReplaceLines(infoOld, infoNew);
                if (bGrid)
                    sheet.GridLines = GridLines;
            }
        }

        public void RefreshAllSheets()
        {
            foreach (MapSheet sheet in Sheets)
                sheet.NeverDisplayed = true;
        }
    }

    class MapSheetComparer : IComparer<MapSheet>
    {
        MapSheetPathInfo[] m_paths;

        public MapSheetComparer(MapSheetPathInfo[] paths)
        {
            m_paths = paths;
        }

        public int Compare(MapSheet x, MapSheet y)
        {
            if (x == y || x.MenuPath == y.MenuPath)
                return 0;
            // Whichever sheet is earlier in m_paths should be sorted earlier; no other criteria
            foreach (MapSheetPathInfo info in m_paths)
            {
                if (info.Sheet == x)
                    return -1;
            }
            return 1;
        }
    }

    [Serializable]
    public class MapNote
    {
        public string Text;
        public Color Color;
        public string Symbol;
        public Point Location;
        public bool Moving;     // Used for feedback on the main grid

        public MapNote()
        {
            Text = null;
            Color = Color.Black;
            Symbol = "N";
            Location = Point.Empty;
            Moving = false;
        }

        public MapNote(string text, Color color, string symbol)
        {
            Text = text;
            Color = color;
            Symbol = symbol;
            Location = Point.Empty;
            Moving = false;
        }

        public MapNote(string text, Color color, string symbol, Point pt)
        {
            Text = text;
            Color = color;
            Symbol = symbol;
            Location = pt;
            Moving = false;
        }

        public MapNote(NoteInfo info, Point pt)
        {
            Text = info.Text;
            Color = info.Color;
            Symbol = info.Symbol;
            Location = pt;
            Moving = false;
        }

        public MapNote(MapNote copy)
        {
            if (copy == null)
            {
                Text = "";
                Color = Color.Transparent;
                Symbol = "";
                Location = Global.NullPoint;
                Moving = false;
            }
            else
            {
                Text = copy.Text;
                Color = copy.Color;
                Symbol = copy.Symbol;
                Location = copy.Location;
                Moving = copy.Moving;
            }
        }

        public MapNote Clone(Point ptNew)
        {
            MapNote note = new MapNote(this);
            note.Location = ptNew;
            return note;
        }

        public MapNote(Stream stream)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            UInt16 count = Global.ReadUInt16(stream);
            byte[] bytes = new byte[count];
            stream.Read(bytes, 0, count);
            Text = utf8.GetString(bytes, 0, count);
            byte bCount = (byte) stream.ReadByte();
            stream.Read(bytes, 0, bCount);
            Symbol = utf8.GetString(bytes, 0, bCount);
            Color = Color.FromArgb(Global.ReadInt32(stream));
            Moving = false;
        }

        public void Serialize(Stream stream)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            byte[] bytes = utf8.GetBytes(Text);
            UInt16 count = (UInt16) Math.Min(bytes.Length, UInt16.MaxValue);
            Global.WriteUInt16(stream, count);
            stream.Write(bytes, 0, count);

            bytes = utf8.GetBytes(Symbol);
            byte bCount = (byte)Math.Min(bytes.Length, byte.MaxValue);
            stream.WriteByte(bCount);
            stream.Write(bytes, 0, bCount);

            Global.WriteInt32(stream, Color.ToArgb());
        }

        public void CopyToClipboard()
        {
            DataFormats.Format format = DataFormats.GetFormat(typeof(MapNote).FullName);
            IDataObject dataObj = new DataObject();
            dataObj.SetData(format.Name, false, this);
            Clipboard.SetDataObject(dataObj, false);
        }

        public override string ToString()
        {
            return String.Format("{0}:{1}", Symbol, Text);
        }
    }

    public class SurroundingSquares
    {
        public MapSquare Main;
        public MapSquare Up;
        public MapSquare Right;
        public MapSquare Down;
        public MapSquare Left;
        public MapSquare UpLeft;
        public MapSquare UpRight;
        public MapSquare DownLeft;
        public MapSquare DownRight;
        public Point Location;

        public bool UpEmpty { get { return Up == null || Up.IsEmpty; } }
        public bool RightEmpty { get { return Right == null || Right.IsEmpty; } }
        public bool DownEmpty { get { return Down == null || Down.IsEmpty; } }
        public bool LeftEmpty { get { return Left == null || Left.IsEmpty; } }
        public bool UpLeftEmpty { get { return UpLeft == null || UpLeft.IsEmpty; } }
        public bool UpRightEmpty { get { return UpRight == null || UpRight.IsEmpty; } }
        public bool DownLeftEmpty { get { return DownLeft == null || DownLeft.IsEmpty; } }
        public bool DownRightEmpty { get { return DownRight == null || DownRight.IsEmpty; } }

        public Point LocationOf(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return new Point(Location.X, Location.Y-1);
                case Direction.Left: return new Point(Location.X-1, Location.Y);
                case Direction.Right: return new Point(Location.X+1, Location.Y);
                case Direction.Down: return new Point(Location.X, Location.Y+1);
                case Direction.UpLeft: return new Point(Location.X-1, Location.Y-1);
                case Direction.UpRight: return new Point(Location.X+1, Location.Y-1);
                case Direction.DownLeft: return new Point(Location.X-1, Location.Y+1);
                case Direction.DownRight: return new Point(Location.X+1, Location.Y+1);
                default: return Location;
            }
        }

        public SurroundingSquares(Point pt, MapSquare main, MapSquare up, MapSquare right, MapSquare down, MapSquare left, MapSquare upleft, MapSquare upright, MapSquare downleft, MapSquare downright)
        {
            Location = pt;
            Main = main;
            Up = up;
            Right = right;
            Down = down;
            Left = left;
            UpLeft = upleft;
            UpRight = upright;
            DownLeft = downleft;
            DownRight = downright;
        }
    }

    public class MapIcon
    {
        public IconName Name;
        public Direction Orientation;
        public Point Location;
        public Color Color;

        public MapIcon()
        {
            Name = IconName.None;
            Orientation = Direction.None;
            Color = Color.Black;
        }

        public MapIcon(IconName icon, Direction orientation)
        {
            Name = icon;
            Orientation = orientation;
            Location = Point.Empty;
            Color = Color.Black;
        }

        public MapIcon(IconName icon, Direction orientation, Point pt)
        {
            Name = icon;
            Orientation = orientation;
            Location = pt;
            Color = Color.Black;
        }

        public MapIcon(IconName icon, Direction orientation, Color color)
        {
            Name = icon;
            Orientation = orientation;
            Location = Point.Empty;
            Color = color;
        }

        public MapIcon(IconName icon)
        {
            Name = icon;
            Orientation = Direction.Up;
            Color = Color.Black;
        }

        public MapIcon(MapIcon copy)
        {
            Name = copy.Name;
            Orientation = copy.Orientation;
            Location = copy.Location;
            Color = copy.Color;
        }

        public MapIcon Clone(Point ptNew)
        {
            MapIcon icon = new MapIcon(this);
            icon.Location = ptNew;
            return icon;
        }

        public void Serialize(Stream stream)
        {
            Global.WriteUInt16(stream, (UInt16)Name);
            stream.WriteByte((byte)Orientation);
            Global.WriteInt32(stream, Color.ToArgb());
        }

        public MapIcon(Stream stream)
        {
            Name = (IconName)Global.ReadUInt16(stream);
            Orientation = (Direction)stream.ReadByte();
            Color = Color.FromArgb(Global.ReadInt32(stream));
        }

        public RotateFlipType RotateCommand
        {
            get
            {
                switch (Orientation)
                {
                    case Direction.Up: return RotateFlipType.RotateNoneFlipNone;
                    case Direction.Right: return RotateFlipType.Rotate90FlipNone;
                    case Direction.Down: return RotateFlipType.Rotate180FlipNone;
                    case Direction.Left: return RotateFlipType.Rotate270FlipNone;
                    default: return RotateFlipType.RotateNoneFlipNone;
                }
            }
        }

        public Icon GetSizedIcon(Size sz)
        {
            if (sz.Width < 17)
                return new Icon(Image, new Size(16, 16));
            return new Icon(Image, sz);
        }

        public string Text
        {
            get
            {
                switch (Name)
                {
                    case IconName.ArrowFull: return "Arrow (full)";
                    case IconName.ArrowHalf: return "Arrow (half)";
                    case IconName.DoorFull: return "Door (full)";
                    case IconName.StairsDown: return "Stairs (down)";
                    case IconName.StairsUp: return "Stairs (up)";
                    case IconName.DoorHalf: return "Door (half)";
                    case IconName.GrateFull: return "Grate (full)";
                    case IconName.GrateHalf: return "Grate (half)";
                    case IconName.FragileHalf: return "Fragile (half)";
                    case IconName.Spinner: return "Spinner";
                    case IconName.Exit: return "Exit";
                    case IconName.Pentagram: return "Pentagram";
                    case IconName.Safe: return "Safe";
                    case IconName.RotateCCW: return "Rotate (CCW)";
                    case IconName.RotateCW: return "Rotate (CW)";
                    case IconName.LockedFull: return "Locked (full)";
                    case IconName.LockedHalf: return "Locked (half)";
                    default: return "<none>";
                }
            }
        }

        public bool HintName { get { return Name == IconName.ArrowHalf || Name == IconName.FragileHalf; } }
        public bool HintNorth { get { return Orientation == Direction.Up && HintName; } }
        public bool HintSouth { get { return Orientation == Direction.Down && HintName; } }
        public bool HintEast { get { return Orientation == Direction.Right && HintName; } }
        public bool HintWest { get { return Orientation == Direction.Left && HintName; } }

        public bool Immaterial
        {
            get
            {
                switch (Name)
                {
                    case IconName.DoorFull:
                    case IconName.DoorHalf:
                    case IconName.GrateFull:
                    case IconName.GrateHalf:
                        return false;
                    default:
                        return true;
                }
            }
        }

        public Icon Image
        {
            get
            {
                switch (Name)
                {
                    case IconName.ArrowFull: return Properties.Resources.iconArrowFullUp;
                    case IconName.ArrowHalf: return Properties.Resources.iconArrowHalfUp;
                    case IconName.DoorFull: return Properties.Resources.iconDoorFullHoriz;
                    case IconName.DoorHalf: return Properties.Resources.iconDoorHalfTop;
                    case IconName.GrateFull: return Properties.Resources.iconGrateFullHoriz;
                    case IconName.GrateHalf: return Properties.Resources.iconGrateHalfTop;
                    case IconName.FragileHalf: return Properties.Resources.iconFragileHalfUp;
                    case IconName.Spinner: return Properties.Resources.iconSpinner;
                    case IconName.StairsDown: return Properties.Resources.iconStairsDown;
                    case IconName.StairsUp: return Properties.Resources.iconStairsUp;
                    case IconName.Exit: return Properties.Resources.iconExit;
                    case IconName.Pentagram: return Properties.Resources.iconPentagram;
                    case IconName.Safe: return Properties.Resources.iconSafe;
                    case IconName.RotateCCW: return Properties.Resources.iconRotateCCW;
                    case IconName.RotateCW: return Properties.Resources.iconRotateCW;
                    case IconName.LockedFull: return Properties.Resources.iconLockedFullHoriz;
                    case IconName.LockedHalf: return Properties.Resources.iconLockedHalfTop;
                    default: return Properties.Resources.iconNone;
                }
            }
        }

        public override bool Equals(object obj)
        {
            return Equals(obj, false);
        }

        public bool Equals(object obj, bool bIgnoreColor)
        {
            if (!(obj is MapIcon))
                return false;

            MapIcon di = (MapIcon)obj;

            if (di.Name != Name)
                return false;

            if (di.Orientation != Orientation)
                return false;

            if (!bIgnoreColor && di.Color.ToArgb() != Color.ToArgb())
                return false;

            return true;
        }

        public override int GetHashCode()
        {
            return ((byte)Name << 8) | (byte)Orientation | Color.ToArgb();
        }

        public override string ToString()
        {
            return Text;
        }

        public bool IsWallIcon
        {
            get
            {
                switch (Name)
                {
                    case IconName.ArrowHalf:
                    case IconName.DoorHalf:
                    case IconName.FragileHalf:
                    case IconName.GrateHalf:
                    case IconName.LockedHalf:
                        return true;
                    default:
                        return false;
                }
            }
        }
    }
}
