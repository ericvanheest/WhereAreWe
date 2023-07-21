using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO.Compression;
using System.IO;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Globalization;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Specialized;
using System.Collections;

namespace WhereAreWe
{
    // When adding menu items to the Action enum, be sure to add them in the correct position in m_menuItems during MainForm()
    public enum Action
    {
        None = -1,

        // Menu items

        FirstMenuItem = 0,
        FileNew = 0,
        FileOpen,
        FileSave,
        FileSaveAs,
        FileExportPng,
        FileExportZip,
        FileExit,
        EditUndo,
        EditRedo,
        EditCut,
        EditCopy,
        EditPaste,
        EditDelete,
        EditFind,
        EditCrop,
        EditRotateLeft,
        EditRotateRight,
        EditRotate180,
        EditFlipHoriz,
        EditFlipVert,
        EditConvertHalf,
        EditFillBlocks,
        EditOutline,
        EditLiveSquares,
        ViewOptions,
        ViewColors,
        ViewInformation,
        ViewToolbar,
        ViewNoteTemplates,
        ViewTriggers,
        ViewZOrder,
        View100Percent,
        View150Percent,
        View200Percent,
        View300Percent,
        ViewFitWidth,
        ViewFitHeight,
        ViewFitInPanel,
        ViewFitWindow,
        ViewBringDOSBoxToForeground,
        ViewAutoArrange,
        ModePlay,
        ModeBlock,
        ModeLine,
        ModeHybrid,
        ModeNotes,
        ModeKeyboard,
        ModeEdit,
        ModeFill,
        SheetAdd,
        SheetClone,
        SheetRemove,
        SheetExpand,
        SheetPrevious,
        SheetNext,
        SheetGoto,
        SheetOrganize,
        SheetLabels,
        SheetClearVisitedSquares,
        GameLaunchCurrentGame,
        GameViewParty,
        GameShowSpells,
        GameShowMonsters,
        GameShowItems,
        GameShowGameInformation,
        GameShowQuests,
        GameShowShopInventories,
        GameShowScripts,
        GameShowQuickReference,
        GameShowEncountersWhenInCombat,
        GameEditRoster,
        GameCharacterCreationAssistant,
        GameTrainingAssistant,
        GameReAcquireGameProcess,
        GameRemoveAllMonstersFromMap,
        GameResetMonstersOnMap,
        GameEditInGameCartographyData, 
        HelpRunWizard,
        LastMenuItem,

        // Miscellaneous functions
        DecreaseSquareSize,
        IncreaseSquareSize,
        DecreaseSquareSizeFixed,
        IncreaseSquareSizeFixed,
        ScrollSheet,
        Draw,
        DrawScroll,
        MoveLabels,
        DrawBlocks,
        DrawLines,
        DrawHybrid,
        DrawFill,
        DrawEdit,
        MoveCursorNW,
        MoveCursorN,
        MoveCursorNE,
        MoveCursorW,
        MoveCursorE,
        MoveCursorSW,
        MoveCursorS,
        MoveCursorSE,
        ToggleLineNW,
        ToggleLineN,
        ToggleLineNE,
        ToggleLineW,
        ToggleLineE,
        ToggleLineSW,
        ToggleLineS,
        ToggleLineSE,
        ToggleLineAll,
        ToggleDoubleLineNW,
        ToggleDoubleLineN,
        ToggleDoubleLineNE,
        ToggleDoubleLineW,
        ToggleDoubleLineE,
        ToggleDoubleLineSW,
        ToggleDoubleLineS,
        ToggleDoubleLineSE,
        ToggleDoubleLineAll,
        ToggleBackground,
        ExpandSheetNW,
        ExpandSheetN,
        ExpandSheetNE,
        ExpandSheetW,
        ExpandSheetE,
        ExpandSheetSW,
        ExpandSheetS,
        ExpandSheetSE,
        ExpandSheetAll,
        MoveBitmapNW,
        MoveBitmapN,
        MoveBitmapNE,
        MoveBitmapW,
        MoveBitmapE,
        MoveBitmapSW,
        MoveBitmapS,
        MoveBitmapSE,
        MoveBitmap10NW,
        MoveBitmap10N,
        MoveBitmap10NE,
        MoveBitmap10W,
        MoveBitmap10E,
        MoveBitmap10SW,
        MoveBitmap10S,
        MoveBitmap10SE,
        QuickDoor,
        LoadRecent1,
        LoadRecent2,
        LoadRecent3,
        LoadRecent4,
        CureAllSilent,
        CureAll1,
        CureAll2,
        CureAll3,
        CureAll4,
        CureAll5,
        CureAll6,
        CureAll7,
        CureAll8,
        ShowLegend,
        ResetHacker,
        CloseEncounterWindow,
        TradeBackpack1,
        TradeBackpack2,
        TradeBackpack3,
        TradeBackpack4,
        TradeBackpack5,
        TradeBackpack6,
        TradeBackpack7,
        TradeBackpack8,
        TeleportToCursor,
        SpellHotkey1,
        SpellHotkey2,
        SpellHotkey3,
        SpellHotkey4,
        SpellHotkey5,
        SpellHotkey6,
        SpellHotkey7,
        SpellHotkey8,
        SpellHotkey9,
        SpellHotkey10,
        SetMapCartography,
        ClearMapCartography,
        CopyLocation,
        SelectCharacter1,
        SelectCharacter2,
        SelectCharacter3,
        SelectCharacter4,
        SelectCharacter5,
        SelectCharacter6,
        SelectCharacter7,
        SelectCharacter8,
        MoveDOSBox,
        RotateIconCW,
        RotateIconCCW,
        ToggleMapScrollbars,
        Refresh,
        SelectBlock1,
        SelectBlock2,
        SelectBlock3,
        SelectBlock4,
        SelectBlock5,
        SelectBlock6,
        SelectBlock7,
        SelectBlock8,
        SelectBlock9,
        SelectBlock10,
        SelectLine1,
        SelectLine2,
        SelectLine3,
        SelectLine4,
        SelectLine5,
        SelectLine6,
        SelectLine7,
        SelectLine8,
        SelectLine9,
        SelectLine10,
        NoteTemplate1,
        NoteTemplate2,
        NoteTemplate3,
        NoteTemplate4,
        BlockColorDialog,
        LineColorDialog,
        ToggleLastBlock,
        ToggleLastLine,
        PrevBlockStyle,
        NextBlockStyle,
        PrevLineStyle,
        NextLineStyle,
        PrevCharacter,
        NextCharacter,
        OptionsMaps,
        OptionsMisc,
        OptionsKeyboard,
        OptionsMouse,
        OptionsPlay,
        OptionsWindows,
        OptionsSpells,
        OptionsEncounter,
        OptionsQuests,
        OptionsDOSBox,
        WizardMinimal,
        WizardFaq,
        WizardFull,
        SkipIntroductions,
        AutoCombat,
        DisarmTrap,
        CopyMapText,

        // Options window checkboxes

        ToggleHideSquares,
        ToggleShowNotesUnvisited,
        ToggleRevealEdgeSquares,
        ToggleRevealInaccessible,
        ToggleUseInGameCartography,
        ToggleHideUnvisitedDots,
        ToggleShowActiveScripts,
        ToggleShowActiveEncountersOnly,
        ToggleShowGridLines,
        ToggleShowLabels,
        ToggleRevealSeenSquares,
        ToggleReadOnlyMaps,
        ToggleReadOnlyNotes,
        CycleYouAreHereOpacity,
        CycleMonsterIconOpacity,
        CycleTreasureWindowOpacity,
        CycleUnvisitedSquareOpacity,
        CycleSeenSquareOpacity,
        CycleItemFormats,
        CycleWizardModes,
        PrevIcon,
        NextIcon,
        PrevBlockIndex,
        NextBlockIndex,
        PrevLineIndex,
        NextLineIndex,
        ToggleKeyboardHook,
        ToggleMemoryWrite,
        ToggleUpdateInGameCartography,
        ToggleRestoreHPWithCureall,
        ToggleCheat,
        ToggleAutoCharacterSwitch,
        ToggleAutoMapSwitch,
        ToggleAutoShowNotes,
        ToggleShowSpellsWhenCasting,
        ToggleForceDOSBoxLocation,
        ToggleForceDOSBoxSize,
        ToggleWindowSnap,
        ToggleShowEncountersInCombat,
        ToggleShowTreasureWindow,
        ToggleShowDeadMonsters,
        ToggleShowMonstersOnMap,
        ToggleShowOnlyNearbyMonsters,
        ToggleShowActiveMonsterIcon,
        ToggleShowUnexploredMonsters,
        ToggleHideScriptMonsters,
        ToggleNearbyAndFlaggedBold,
        ToggleQuestGiver,
        ToggleQuestRewards,
        ToggleHideInvalidQuests, 
        ToggleEditCopyBackgrounds,
        ToggleEditCopyInnerLines,
        ToggleEditCopyOuterLines,
        ToggleEditCopyIcons,
        ToggleEditCopyNotes,

        Last
    }

    public enum WheelAction
    {
        None,
        Zoom,
        FixedZoom,
        Icon,
        IconOrientation,
        BlockStyle,
        LineStyle,
        BlockIndex,
        LineIndex,
        Sheet,
        Character,

        Last,
    }

    public enum BoolHandled
    {
        False,
        True,
        None
    }

    public enum DOSBoxVersion
    {
        Classic074,     // DOSBox 0.74 with no modifications
        SVNDaum,        // YKHwong's SVN build (toolbar and save states)
        Unknown,
        NotRunning,
    }

    public enum SelectionActions
    {
        None,
        Create,
        Move,
        Copy
    }

    public enum ShowNextType
    {
        None,
        Message,
        CreationAssistant,
        TrainingAssistant,
        PartyWindow,
        SpellWindow,
        EncounterWindow,
        QuestWindow,
        GameInfoWindow,
        ShopInventoryWindow,
        ScriptsWindow,
        QuickRef
    }

    public class ShowNext
    {
        public string Caption;
        public string Message;
        public Point Position;
        public ShowNextType Type;
        public MessageBoxIcon Icon;

        public ShowNext(ShowNextType type)
        {
            Caption = String.Empty;
            Message = String.Empty;
            Type = type;
            Position = Global.NullPoint;
        }

        public ShowNext(string strMessage, string strCaption, MessageBoxIcon icon = MessageBoxIcon.Warning)
        {
            Type = ShowNextType.Message;
            Caption = strCaption;
            Message = strMessage;
            Position = Global.NullPoint;
            Icon = icon;
        }
    }

    public enum DoorType
    {
        StraddleWall,
        FullSquare
    }

    public enum Direction
    {
        None,
        Up,
        Right,
        Down,
        Left,
        UpLeft,
        UpRight,
        DownLeft,
        DownRight,
        All
    }

    [Flags]
    public enum DirectionFlags
    {
        None = 0x0000,
        Up = 0x0001,
        Right = 0x0002,
        Down = 0x0004,
        Left = 0x0008,
        UpLeft = Up | Left,
        UpRight = Up | Right,
        DownLeft = Down | Left,
        DownRight = Down | Right,
        North = Up,
        East = Right,
        South = Down,
        West = Left,
        NorthEast = North | East,
        NorthWest = North | West,
        SouthEast = South | East,
        SouthWest = South | West,
        All = Up | Down | Left | Right
    }

    public enum RectangleEdge
    {
        None,
        Left,
        Top,
        Bottom,
        Right
    }

    public static class Directions
    {
        public static Direction[] UpLeft    { get { return new Direction[] { Direction.Up, Direction.Left    }; } }
        public static Direction[] UpRight   { get { return new Direction[] { Direction.Up, Direction.Right   }; } }
        public static Direction[] DownLeft  { get { return new Direction[] { Direction.Down, Direction.Left  }; } }
        public static Direction[] DownRight { get { return new Direction[] { Direction.Down, Direction.Right }; } }
        public static Direction[] All       { get { return new Direction[] { Direction.Up, Direction.Left, Direction.Down, Direction.Right }; } }
    }

    public enum ScrollStyle
    {
        LockScroll,
        Drag
    }

    public enum CaptureMode
    {
        None,
        Scroll,
        Draw
    }

    public enum DrawMode
    {
        None,
        Fill,
        Erase
    }

    public enum Orient
    {
        None,
        Horiz,
        Vert
    }

    public enum BlockMode
    {
        None,
        Play,
        Block,
        Line,
        Hybrid,
        Notes,
        Keyboard,
        Fill,
        Edit,
        Live,

        Last
    }

    public enum Origin
    {
        UpperLeft,
        LowerLeft,
        UpperRight,
        LowerRight,
        Center
    }

    public enum IconName
    {
        None,
        ArrowFull,
        ArrowHalf,
        DoorFull,
        DoorHalf,
        GrateFull,
        GrateHalf,
        FragileHalf,
        Spinner,
        StairsUp,
        StairsDown,
        Exit,
        Safe,
        RotateCW,
        RotateCCW,
        Pentagram,
        LockedHalf,
        LockedFull
    }

    public enum MeleeType
    {
        None,
        FrontRowClose,
        FrontRowShort,
        FrontRowMedium,
        FrontRowLong,
        BackRowClose,
        BackRowShort,
        BackRowMedium,
        BackRowLong
    }

    public enum MathOperation
    {
        None,
        Equal,
        LessThanOrEqual,
        GreaterThanOrEqual,
        NotEqual,
        LessThan,
        GreaterThan,
        IsZero,
        IsNotZero,
        IsTrue,
        IsFalse
    }

    public enum AxisIncreaseX
    {
        LeftToRight,
        RightToLeft
    }

    public enum AxisIncreaseY
    {
        TopToBottom,
        BottomToTop
    }

    public class ResistanceValue : IComparable<ResistanceValue>
    {
        public GenericResistanceFlags Resistance;
        public int BaseValue;
        public int Bonus;
        public int Modifier;

        public int CompareTo(ResistanceValue rv)
        {
            if (BaseValue == rv.BaseValue && Bonus == rv.Bonus && Modifier == rv.Modifier)
                return 0;
            int iDelta = rv.Total - Total;
            if (iDelta == 0)
                return 1;   // Don't falsely equate just because the totals are the same
            return iDelta;
        }

        public ResistanceValue(GenericResistanceFlags resist, int val, int bonus, int modifier)
        {
            Resistance = resist;
            BaseValue = val;
            Bonus = bonus;
            Modifier = modifier;
        }

        public ResistanceValue(GenericResistanceFlags resist, int val, int bonus)
        {
            Resistance = resist;
            BaseValue = val;
            Bonus = bonus;
            Modifier = 0;
        }

        public ResistanceValue(GenericResistanceFlags resist, int val)
        {
            Resistance = resist;
            BaseValue = val;
            Modifier = 0;
            Bonus = 0;
        }

        public int Total
        {
            get
            {
                return BaseValue + Bonus + Modifier;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}", BaseValue);
            if (Bonus != 0)
                sb.Append(Global.AddPlus(Bonus));
            if (Modifier != 0)
                sb.Append(Global.AddPlus(Modifier));
            return sb.ToString();
        }
    }

    public class ActiveEncounterInfo
    {
        public MapXY Location;
        public bool Active;
        public bool Monster;

        public ActiveEncounterInfo(MapXY map, bool active, bool monster)
        {
            Location = map;
            Active = active;
            Monster = monster;
        }

        public override string ToString()
        {
            return String.Format("{0}:{1}{2}", Location.ToString(), Active ? "a" : "", Monster ? "m" : "");
        }
    }

    public class EncounterData
    {
        protected Dictionary<MapXY, ActiveEncounterInfo> Data;

        protected void SetBytes(byte[] bytes)
        {
            Data = new Dictionary<MapXY, ActiveEncounterInfo>();
            int iIndex = 0;

            while (iIndex < bytes.Length - 2)
            {
                int iMap = bytes[iIndex++];
                int iLength = bytes[iIndex++];

                for (int i = 0; i < iLength; i++)
                {
                    MapXY map = new MapXY(GameNames.None, iMap, bytes[iIndex] & 0xf, bytes[iIndex] >> 4);
                    Data.Add(map, new ActiveEncounterInfo(map, true, true));
                    iIndex++;
                }
            }
        }

        public EncounterData()
        {
            Data = null;
        }

        public virtual bool IsMonsterEncounter(int iMapIndex, Point pt)
        {
            if (Data == null)
                return false;

            MapXY map = new MapXY(GameNames.None, iMapIndex, pt.X, pt.Y);
            if (!Data.ContainsKey(map))
                return false;
            return Data[map].Monster;
        }
    }

    public class MM1EncounterData : EncounterData
    {
        public MM1EncounterData()
        {
            SetBytes(Properties.Resources.MM1MonsterDefaults);
        }
    }

    public class MM2EncounterData : EncounterData
    {
        public MM2EncounterData()
        {
            SetBytes(Properties.Resources.MM2MonsterDefaults);
        }
    }

    public class MapBytes
    {
        public byte[] Bytes;
        public Size Size;

        public MapBytes(byte[] bytes, int width, int height)
        {
            Bytes = bytes;
            Size = new Size(width, height);
        }
    }

    public class MemoryBytes
    {
        public byte[] Bytes;
        public long Offset;
        public bool ValidOffset;

        public MemoryBytes(byte[] bytes, long offset)
        {
            Bytes = bytes;
            Offset = offset;
            ValidOffset = true;
        }

        public MemoryBytes(byte[] bytes)
        {
            Bytes = bytes;
            Offset = 0;
            ValidOffset = false;
        }

        public int Length { get { return Bytes == null ? 0 : Bytes.Length; } }

        public static implicit operator byte[](MemoryBytes mb)
        {
            return (mb == null ? null : mb.Bytes);
        }

        public byte this[int key]
        {
            get { return Bytes[key]; }
            set { Bytes[key] = value; }
        }

        public MemoryBytes GetRange(ScriptBlock block)
        {
            return GetRange(block.Start, block.Length);
        }

        public MemoryBytes GetRange(int iStart)
        {
            if (Bytes == null)
                return null;
            return GetRange(iStart, Bytes.Length - iStart);
        }

        public MemoryBytes GetRange(int iStart, int iLength)
        {
            if (iStart < 0 || iLength < 1 || Bytes == null)
                return null;
            if (iStart + iLength > Bytes.Length)
                return GetRange(iStart);

            byte[] bytes = new byte[iLength];
            Buffer.BlockCopy(Bytes, iStart, bytes, 0, iLength);

            if (ValidOffset)
                return new MemoryBytes(bytes, Offset + iStart);

            return new MemoryBytes(bytes);
        }

        public static MemoryBytes[] Array(byte[] bytes, long offset)
        {
            return new MemoryBytes[] { new MemoryBytes(bytes, offset) };
        }

        public static MemoryBytes[] Array(byte[][] bytes, OffsetList offsets)
        {
            MemoryBytes[] mbOut = new MemoryBytes[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                if (offsets.Length > i)
                    mbOut[i] = new MemoryBytes(bytes[i], offsets.Offsets[i]);
                else
                    mbOut[i] = new MemoryBytes(bytes[i]);
            }
            return mbOut;
        }

        public static MemoryBytes[] Array(ushort[] shorts, OffsetList offsets)
        {
            MemoryBytes[] mbOut = new MemoryBytes[shorts.Length];
            for (int i = 0; i < shorts.Length; i++)
            {
                if (offsets.Length > i)
                    mbOut[i] = new MemoryBytes(BitConverter.GetBytes(shorts[i]), offsets.Offsets[i]);
                else
                    mbOut[i] = new MemoryBytes(BitConverter.GetBytes(shorts[i]));
            }
            return mbOut;
        }

        public void CopyTo(byte[] bytes, int offset, int count = -1)
        {
            if (count == -1)
                count = Bytes.Length;
            Buffer.BlockCopy(Bytes, 0, bytes, offset, count);
        }
    }

    public class RLEByte
    {
        public byte Byte;
        public int Count;

        public RLEByte(byte b, int count)
        {
            Byte = b;
            Count = count;
        }

        public static implicit operator byte(RLEByte rle)
        {
            return rle.Byte;
        }

        public static implicit operator byte[](RLEByte rle)
        {
            byte[] result = new byte[rle.Count];
            for(int i = 0; i < result.Length; i++)
                result[i] = rle.Byte;
            return result;
        }
    }

    public class MonsterCount
    {
        public string Name;
        public int Count;

        public MonsterCount(string name, int count)
        {
            Name = name;
            Count = count;
        }

        public static string MonsterList(Dictionary<string, MonsterCount> dict)
        {
            StringBuilder sb = new StringBuilder();
            foreach (MonsterCount mc in dict.Values)
            {
                sb.AppendFormat("{0}{1}, ", mc.Name, mc.Count != 1 ? String.Format(" ({0})", mc.Count) : "");
            }
            Global.Trim(sb);
            if (sb.Length == 0)
                return "Unknown";

            return sb.ToString();
        }

        public static string MonsterListUnique(Dictionary<string, MonsterCount> dict)
        {
            StringBuilder sb = new StringBuilder();
            foreach (MonsterCount mc in dict.Values)
            {
                sb.AppendFormat("{0}, ", mc.Name);
            }
            Global.Trim(sb);
            if (sb.Length == 0)
                return "Unknown";

            return sb.ToString();
        }
    }

    public enum InventoryStyle
    {
        None = -1,
        Full = 0,
        NameOnly = 1,
        InfoOnly = 2
    }

    public abstract class RaceClassModifiers
    {
        public Modifiers Human;
        public Modifiers Elf;
        public Modifiers Dwarf;
        public Modifiers Gnome;
        public Modifiers Halforc;
        public Modifiers Hobbit;

        public Modifiers Fighter;
        public Modifiers Mage;
        public Modifiers Priest;
        public Modifiers Thief;
        public Modifiers Bishop;
        public Modifiers Samurai;
        public Modifiers Lord;
        public Modifiers Ninja;

        public Modifiers For(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return Human;
                case GenericRace.Elf: return Elf;
                case GenericRace.Dwarf: return Dwarf;
                case GenericRace.Gnome: return Gnome;
                case GenericRace.HalfOrc: return Halforc;
                case GenericRace.Hobbit: return Hobbit;
                default: return null;
            }
        }

        public Modifiers For(GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Fighter: return Fighter;
                case GenericClass.Mage: return Mage;
                case GenericClass.Priest: return Priest;
                case GenericClass.Thief: return Thief;
                case GenericClass.Bishop: return Bishop;
                case GenericClass.Samurai: return Samurai;
                case GenericClass.Lord: return Lord;
                case GenericClass.Ninja: return Ninja;
                default: return null;
            }
        }
    }

    public enum MouseCursor
    {
        Default,
        Fill,
        FillLines,
        Pencil,
        Block,
        Hybrid,
        Note,
        NoteCopy,
        Copy,
        Anchor,
        Live
    }

    public static class Global
    {
        public static Wiz1Globals Wiz1 = new Wiz1Globals();
        public static Wiz2Globals Wiz2 = new Wiz2Globals();
        public static Wiz3Globals Wiz3 = new Wiz3Globals();
        public static Wiz4Globals Wiz4 = new Wiz4Globals();
        public static Wiz5Globals Wiz5 = new Wiz5Globals();

        public static BT1Globals BT1 = new BT1Globals();
        public static BT2Globals BT2 = new BT2Globals();
        public static BT3Globals BT3 = new BT3Globals();

        public static MM1Globals MM1 = new MM1Globals();
        public static MM2Globals MM2 = new MM2Globals();
        public static MM3Globals MM3 = new MM3Globals();
        public static MM45Globals MM45 = new MM45Globals();

        private static Cursor CursorFill = NativeMethods.CreateCursor(Properties.Resources.iconCursorFill, 13, 15);
        private static Cursor CursorFillLines = NativeMethods.CreateCursor(Properties.Resources.iconCursorFillLines, 13, 15);
        private static Cursor CursorPencil = NativeMethods.CreateCursor(Properties.Resources.iconCursorPencil, 0, 15);
        private static Cursor CursorBlock = NativeMethods.CreateCursor(Properties.Resources.iconCursorBlock, 0, 0);
        private static Cursor CursorHybrid = NativeMethods.CreateCursor(Properties.Resources.iconCursorHybrid, 0, 0);
        private static Cursor CursorNote = NativeMethods.CreateCursor(Properties.Resources.iconCursorNote, 0, 0);
        private static Cursor CursorNoteCopy = NativeMethods.CreateCursor(Properties.Resources.iconCursorNoteCopy, 0, 0);
        private static Cursor CursorCopy = NativeMethods.CreateCursor(Properties.Resources.iconCursorCopy, 0, 0);
        private static Cursor CursorAnchor = NativeMethods.CreateCursor(Properties.Resources.iconCursorAnchor, 0, 0);
        private static Cursor CursorLive = NativeMethods.CreateCursor(Properties.Resources.iconCursorLive, 0, 0);
        private static Cursor CursorFillDouble = NativeMethods.CreateCursor(Properties.Resources.iconCursorFillDouble, 27, 30);
        private static Cursor CursorFillLinesDouble = NativeMethods.CreateCursor(Properties.Resources.iconCursorFillLinesDouble, 27, 30);
        private static Cursor CursorPencilDouble = NativeMethods.CreateCursor(Properties.Resources.iconCursorPencilDouble, 0, 31);
        private static Cursor CursorBlockDouble = NativeMethods.CreateCursor(Properties.Resources.iconCursorBlockDouble, 0, 0);
        private static Cursor CursorHybridDouble = NativeMethods.CreateCursor(Properties.Resources.iconCursorHybridDouble, 0, 0);
        private static Cursor CursorNoteDouble = NativeMethods.CreateCursor(Properties.Resources.iconCursorNoteDouble, 0, 0);
        private static Cursor CursorNoteCopyDouble = NativeMethods.CreateCursor(Properties.Resources.iconCursorNoteCopyDouble, 0, 0);
        private static Cursor CursorCopyDouble = NativeMethods.CreateCursor(Properties.Resources.iconCursorCopyDouble, 0, 0);
        private static Cursor CursorAnchorDouble = NativeMethods.CreateCursor(Properties.Resources.iconCursorAnchorDouble, 0, 0);
        private static Cursor CursorLiveDouble = NativeMethods.CreateCursor(Properties.Resources.iconCursorLiveDouble, 0, 0);

        public static Cursor GetCursor(MouseCursor cursor)
        {
            bool bDoubleSize = Cursors.Default.Size.Width > 32;

            switch (cursor)
            {
                case MouseCursor.Fill: return bDoubleSize ? CursorFillDouble : CursorFill;
                case MouseCursor.FillLines: return bDoubleSize ? CursorFillLinesDouble : CursorFillLines;
                case MouseCursor.Pencil: return bDoubleSize ? CursorPencilDouble : CursorPencil;
                case MouseCursor.Block: return bDoubleSize ? CursorBlockDouble : CursorBlock;
                case MouseCursor.Hybrid: return bDoubleSize ? CursorHybridDouble : CursorHybrid;
                case MouseCursor.Note: return bDoubleSize ? CursorNoteDouble : CursorNote;
                case MouseCursor.NoteCopy: return bDoubleSize ? CursorNoteCopyDouble : CursorNoteCopy;
                case MouseCursor.Copy: return bDoubleSize ? CursorCopyDouble : CursorCopy;
                case MouseCursor.Anchor: return bDoubleSize ? CursorAnchorDouble : CursorAnchor;
                case MouseCursor.Live: return bDoubleSize ? CursorLiveDouble : CursorLive;
                default: return Cursors.Default;
            }
        }

        private static TextInfo g_TextInfo = new CultureInfo("en-US", false).TextInfo;

        public static Random Rand = new Random();

        public static SquareStyleList SquareStyles = SquareStyleList.Default;

        public static byte[] MaxInt16 = BitConverter.GetBytes(Int16.MaxValue);
        public static byte[] MaxUInt16 = BitConverter.GetBytes(UInt16.MaxValue);
        public static byte[] MaxInt32 = BitConverter.GetBytes(Int32.MaxValue);
        public static byte[] MaxUInt32 = BitConverter.GetBytes(UInt32.MaxValue);
        public static byte[] MaxUInt24 = new byte[] { 0xff, 0xff, 0xff };
        public static PerformanceCounter CpuCounter = null;
        public static WindowInfoList Windows = null;
        public static MemoryGuesses MemoryGuesses = null;
        public static LineOfSight LOS = new LineOfSight();

        public static BitmapSet BmpOneMonster = BitmapsFromIcon(Properties.Resources.iconOneMonster);
        public static BitmapSet BmpTwoMonsters = BitmapsFromIcon(Properties.Resources.iconTwoMonsters);
        public static BitmapSet BmpThreeMonsters = BitmapsFromIcon(Properties.Resources.iconThreeMonsters);
        public static BitmapSet BmpOneMonsterSel = BitmapsFromIcon(Properties.Resources.iconOneMonsterHighlighted);
        public static BitmapSet BmpTwoMonstersSel = BitmapsFromIcon(Properties.Resources.iconTwoMonstersHighlighted);
        public static BitmapSet BmpThreeMonstersSel = BitmapsFromIcon(Properties.Resources.iconThreeMonstersHighlighted);
        public static BitmapSet BmpActiveMonster = BitmapsFromIcon(Properties.Resources.iconActiveMonster);
        public static BitmapSet BmpYouAreHere = BitmapsFromIcon(Properties.Resources.iconYouAreHere);
        public static BitmapSet BmpActiveSquare = BitmapsFromIcon(Properties.Resources.iconActiveSquare);
        public static BitmapSet BmpCursor = BitmapsFromIcon(Properties.Resources.iconCursor, 26);
        public static BitmapSet BmpOneNPC = BitmapsFromIcon(Properties.Resources.iconOneNPC);
        public static BitmapSet BmpOneNPCSel = BitmapsFromIcon(Properties.Resources.iconOneNPCHighlighted);
        public static BitmapSet BmpOneMonsterAndNPC = BitmapsFromIcon(Properties.Resources.iconMonsterAndNPC);
        public static BitmapSet BmpOneMonsterAndNPCSel = BitmapsFromIcon(Properties.Resources.iconMonsterAndNPCHighlighted);

        public static BitmapSet GetBitmapSet(int iTotal, int iNPCs, bool bSelected)
        {
            if (iTotal > 2)
                return bSelected ? Global.BmpThreeMonstersSel : Global.BmpThreeMonsters;
            if (iTotal == 2 && iNPCs > 0)
                return bSelected ? Global.BmpOneMonsterAndNPCSel : Global.BmpOneMonsterAndNPC;
            if (iTotal == 2 && iNPCs == 0)
                return bSelected ? Global.BmpTwoMonstersSel : Global.BmpTwoMonsters;
            if (iNPCs > 0)
                return bSelected ? Global.BmpOneNPCSel : Global.BmpOneNPC;
            return bSelected ? Global.BmpOneMonsterSel : Global.BmpOneMonster;
        }

        public static NativeMethods.INPUT[] ModifierKeysUp = NativeMethods.CreateKeyInputs(false, Keys.LControlKey, Keys.RControlKey, Keys.LShiftKey, Keys.RShiftKey, Keys.LMenu, Keys.RMenu);

        public const string RepeatableQuest = "Repeatable";
        public const string InternalMapString = "(Internal Game Map)";
        public static Regex TeleportRegex = new Regex(@"^\(Teleport|\d+% chance: \(Teleport");
        public const string MemoryScanFail = "The memory scanner could not locate the correct addresses inside the game process.\r\n\r\n" +
            "Please check that your shortcuts are correct and verify the settings under the \"DOSBox\" tab of the Options, then select \"Game->Re-acquire game process\" to try again.\r\n\r\n" +
            "If you are running DOSBox as an Administrator, then this program requires Administrator rights as well.";
        public const string UnidentifiedItemTip = "This item has not yet been identified.\r\n\r\nYou may trade it to another character by pressing that character's number.";

        public static ImageCache BitmapCache = new ImageCache();
        public static IconCache IconCache = new IconCache();

        private static Stopwatch m_swLogTime = new Stopwatch();

        public static string[] ItemFormats = new string[] {
            "$[Cursed] $[Broken] $[Name] ($[Why]), $[Type] $[DamAC], $[Equip], $[Use] [$[Charges]], $[Value]",
            "$[Cursed] $[Broken] $[Name] ($[AlignShort]:$[UsableBy]), $[Type] $[DamAC], $[Equip], $[Use] [$[Charges]], $[Value]",
            "$[Cursed] $[Broken] $[BasicName] ($[Why]), $[Type] $[DamAC], $[Equip], $[Use] [$[Charges]], $[Value]",
            "$[Cursed] $[Broken] $[Name]",
            "$[Cursed] $[Broken] $[BasicName]"
        };

        public static string[] NESW = new string[] { "north", "east", "south", "west" };

#if DEBUG
        public static bool Debug = true;
#else
        public static bool Debug = false;
#endif

        public static void LogTimeStart()
        {
            m_swLogTime.Reset();
            m_swLogTime.Start();
        }

        public static void LogTimeStop(string strFormat = "Time: {0} ms")
        {
            m_swLogTime.Stop();
            Log(strFormat, m_swLogTime.ElapsedMilliseconds);
        }

        public static MapBook CreateNewMapBook()
        {
            BitmapCache.Clear();
            return new MapBook();
        }

        public static MapBook CreateNewMapBook(List<MapSheet> maps, string strFile = null)
        {
            BitmapCache.Clear();
            MapBook book = new MapBook(maps);
            book.LastFile = strFile;
            return book;
        }

        public static BitmapSet BitmapsFromIcon(Icon icon, int iFirst = 16)
        {
            return new BitmapSet(
                new Icon(icon, iFirst, iFirst).ToBitmap(),
                new Icon(icon, iFirst * 3 / 2, iFirst * 3 / 2).ToBitmap(),
                new Icon(icon, iFirst * 2, iFirst * 2).ToBitmap(),
                new Icon(icon, iFirst * 3, iFirst * 3).ToBitmap()
            );
        }

        public static DirectionFlags[] CardinalDirections
        {
            get
            {
                return new DirectionFlags[] {
                    DirectionFlags.North,
                    DirectionFlags.East,
                    DirectionFlags.South,
                    DirectionFlags.West };
            }
        }

        public static bool ShowOnlyDetectableMonsters
        {
            get
            {
                return Properties.Settings.Default.ShowMonstersOnMaps && Properties.Settings.Default.ShowOnlyDetectableMonsters;
            }
        }

        public static string TipTextBreak(string str)
        {
            // Break up a long string so that it looks better as a tool tip.  Mainly for sentences.

            StringBuilder sb = new StringBuilder();
            int iLen = 0;
            bool bSkipSpace = true;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if (bSkipSpace && Char.IsWhiteSpace(c))
                    continue;
                bSkipSpace = false;
                sb.Append(c);
                switch (c)
                {
                    case '\r':
                    case '\n':
                        iLen = 0;
                        break;
                    default:
                        break;
                }
                if (iLen++ > 40)
                {
                    switch (c)
                    {
                        case '.':
                            if (str.Length > i + 1 && Char.IsWhiteSpace(str[i + 1]))
                            {
                                sb.Append("\r\n");
                                bSkipSpace = true;
                                iLen = 0;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return sb.ToString();
        }

        public static bool Cheats { get { return Properties.Settings.Default.EnableMemoryWrite && Properties.Settings.Default.EnableCheats; } }

        public static Shortcuts CommonShortcuts
        {
            get
            {
                Shortcuts sc = new Shortcuts();
                sc.Add(Action.FileNew, Keys.LControlKey, Keys.N);
                sc.Add(Action.FileOpen, Keys.LControlKey, Keys.O);
                sc.Add(Action.FileSave, Keys.LControlKey, Keys.S);
                sc.Add(Action.EditUndo, Keys.LControlKey, Keys.Z);
                sc.Add(Action.EditRedo, Keys.LControlKey, Keys.Y);
                sc.Add(Action.EditCut, Keys.LControlKey, Keys.X);
                sc.Add(Action.EditCopy, Keys.LControlKey, Keys.C);
                sc.Add(Action.EditPaste, Keys.LControlKey, Keys.V);
                sc.Add(Action.EditDelete, Keys.Delete);
                sc.Add(Action.EditFind, Keys.LControlKey, Keys.F);
                sc.Add(Action.ViewOptions, Keys.LControlKey, Keys.LShiftKey, Keys.O);
                sc.Add(Action.ViewColors, Keys.LControlKey, Keys.LShiftKey, Keys.C);
                sc.Add(Action.ViewInformation, Keys.F1);
                sc.Add(Action.ViewFitWindow, Keys.LControlKey, Keys.LShiftKey, Keys.F);
                sc.Add(Action.ViewBringDOSBoxToForeground, Keys.LControlKey, Keys.LShiftKey, Keys.D);
                sc.Add(Action.ModeBlock, Keys.F2);
                sc.Add(Action.ModeLine, Keys.F3);
                sc.Add(Action.ModeHybrid, Keys.F4);
                sc.Add(Action.ModeNotes, Keys.F6);
                sc.Add(Action.ModePlay, Keys.F7);
                sc.Add(Action.ModeKeyboard, Keys.F8);
                sc.Add(Action.ModeFill, Keys.F9);
                sc.Add(Action.ModeEdit, Keys.LControlKey, Keys.E);
                sc.Add(Action.Refresh, Keys.F5);
                sc.Add(Action.SheetPrevious, Keys.LControlKey, Keys.PageUp);
                sc.Add(Action.SheetNext, Keys.LControlKey, Keys.PageDown);
                sc.Add(Action.GameLaunchCurrentGame, Keys.LControlKey, Keys.LShiftKey, Keys.G);
                sc.Add(Action.GameViewParty, Keys.LControlKey, Keys.P);
                sc.Add(Action.GameShowSpells, Keys.LControlKey, Keys.L);
                sc.Add(Action.GameShowMonsters, Keys.LControlKey, Keys.M);
                sc.Add(Action.GameShowGameInformation, Keys.LControlKey, Keys.G);
                sc.Add(Action.GameShowQuests, Keys.LControlKey, Keys.Q);
                sc.Add(Action.GameShowShopInventories, Keys.LControlKey, Keys.I);
                sc.Add(Action.GameTrainingAssistant, Keys.LControlKey, Keys.T);
                sc.Add(Action.GameShowQuickReference, Keys.LControlKey, Keys.R);
                sc.Add(Action.SelectBlock1, Keys.LControlKey, Keys.D1);
                sc.Add(Action.SelectBlock2, Keys.LControlKey, Keys.D2);
                sc.Add(Action.SelectBlock3, Keys.LControlKey, Keys.D3);
                sc.Add(Action.SelectBlock4, Keys.LControlKey, Keys.D4);
                sc.Add(Action.SelectBlock5, Keys.LControlKey, Keys.D5);
                sc.Add(Action.SelectBlock6, Keys.LControlKey, Keys.D6);
                sc.Add(Action.SelectBlock7, Keys.LControlKey, Keys.D7);
                sc.Add(Action.SelectBlock8, Keys.LControlKey, Keys.D8);
                sc.Add(Action.SelectBlock9, Keys.LControlKey, Keys.D9);
                sc.Add(Action.SelectBlock10, Keys.LControlKey, Keys.D0);
                sc.Add(Action.SelectLine1, Keys.D1);
                sc.Add(Action.SelectLine2, Keys.D2);
                sc.Add(Action.SelectLine3, Keys.D3);
                sc.Add(Action.SelectLine4, Keys.D4);
                sc.Add(Action.SelectLine5, Keys.D5);
                sc.Add(Action.SelectLine6, Keys.D6);
                sc.Add(Action.SelectLine7, Keys.D7);
                sc.Add(Action.SelectLine8, Keys.D8);
                sc.Add(Action.SelectLine9, Keys.D9);
                sc.Add(Action.SelectLine10, Keys.D0);
                return sc;
            }
        }

        public static Shortcuts DebugShortcuts
        {
            get
            {
                Shortcuts sc = CommonShortcuts;

                sc.Add(Action.MoveBitmapSW, Keys.NumPad1);
                sc.Add(Action.MoveBitmapS, Keys.NumPad2);
                sc.Add(Action.MoveBitmapSE, Keys.NumPad3);
                sc.Add(Action.MoveBitmapW, Keys.NumPad4);
                sc.Add(Action.MoveBitmapE, Keys.NumPad6);
                sc.Add(Action.MoveBitmapNW, Keys.NumPad7);
                sc.Add(Action.MoveBitmapN, Keys.NumPad8);
                sc.Add(Action.MoveBitmapNE, Keys.NumPad9);
                sc.Add(Action.MoveBitmap10SW, Keys.LControlKey, Keys.NumPad1);
                sc.Add(Action.MoveBitmap10S, Keys.LControlKey, Keys.NumPad2);
                sc.Add(Action.MoveBitmap10SE, Keys.LControlKey, Keys.NumPad3);
                sc.Add(Action.MoveBitmap10W, Keys.LControlKey, Keys.NumPad4);
                sc.Add(Action.MoveBitmap10E, Keys.LControlKey, Keys.NumPad6);
                sc.Add(Action.MoveBitmap10NW, Keys.LControlKey, Keys.NumPad7);
                sc.Add(Action.MoveBitmap10N, Keys.LControlKey, Keys.NumPad8);
                sc.Add(Action.MoveBitmap10NE, Keys.LControlKey, Keys.NumPad9);
                return sc;
            }
        }

        public static Shortcuts DefaultShortcuts
        {
            get
            {
                Shortcuts sc = CommonShortcuts;

                sc.Add(Action.MoveCursorSW, Keys.NumPad1);
                sc.Add(Action.MoveCursorS, Keys.NumPad2);
                sc.Add(Action.MoveCursorSE, Keys.NumPad3);
                sc.Add(Action.MoveCursorW, Keys.NumPad4);
                sc.Add(Action.ToggleBackground, Keys.NumPad5);
                sc.Add(Action.MoveCursorE, Keys.NumPad6);
                sc.Add(Action.MoveCursorNW, Keys.NumPad7);
                sc.Add(Action.MoveCursorN, Keys.NumPad8);
                sc.Add(Action.MoveCursorNE, Keys.NumPad9);
                sc.Add(Action.ToggleLineSW, Keys.LControlKey, Keys.NumPad1);
                sc.Add(Action.ToggleLineS, Keys.LControlKey, Keys.NumPad2);
                sc.Add(Action.ToggleLineSE, Keys.LControlKey, Keys.NumPad3);
                sc.Add(Action.ToggleLineW, Keys.LControlKey, Keys.NumPad4);
                sc.Add(Action.ToggleLineAll, Keys.LControlKey, Keys.NumPad5);
                sc.Add(Action.ToggleLineE, Keys.LControlKey, Keys.NumPad6);
                sc.Add(Action.ToggleLineNW, Keys.LControlKey, Keys.NumPad7);
                sc.Add(Action.ToggleLineN, Keys.LControlKey, Keys.NumPad8);
                sc.Add(Action.ToggleLineNE, Keys.LControlKey, Keys.NumPad9);
                return sc;
            }
        }

        public static int AlignedFirstIndexOf(byte[] bytes, byte b1, byte b2, int iStart, int iEnd)
        {
            for (int i = iStart; i < iEnd; i += 2)
                if (bytes[i] == b1 && bytes[i + 1] == b2)
                    return i;
            return -1;
        }

        public static int[] FindBytes(byte[] searchIn, byte[] searchFor, int iStart = 0)
        {
            if (searchIn == null || searchFor == null || searchIn.Length < 1 || searchFor.Length < 1)
                return new int[0];

            List<int> found = new List<int>();

            for (int i = iStart; i < searchIn.Length - searchFor.Length; i++)
            {
                if (Global.CompareBytes(searchIn, searchFor, i, 0, searchFor.Length))
                {
                    found.Add(i);
                    i += (searchFor.Length - 1);
                }
            }

            return found.ToArray();
        }

        public static string Flatten(List<string> list)
        {
            if (list == null || list.Count == 0)
                return String.Empty;

            StringBuilder sb = new StringBuilder();
            bool bFirst = true;
            foreach (string str in list)
            {
                if (!bFirst)
                    sb.AppendLine();
                bFirst = false;
                sb.Append(str);
            }

            return sb.ToString();
        }

        public static bool AnySubItemContains(ListViewItem lvi, string str)
        {
            string strFind = str.ToLower();
            foreach (ListViewItem.ListViewSubItem si in lvi.SubItems)
            {
                if (si.Text.ToLower().Contains(strFind))
                    return true;
            }
            return false;
        }

        public static Point PointFromByte(byte b)
        {
            return new Point(b & 0xf, b >> 4);
        }

        public static void WritePoints(Stream stream, params Point[] points)
        {
            foreach (Point pt in points)
            {
                byte[] bytes = BitConverter.GetBytes(pt.X);
                stream.Write(bytes, 0, bytes.Length);
                bytes = BitConverter.GetBytes(pt.Y);
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public static bool NullOrEmpty(Array array)
        {
            return (array == null || array.Length == 0);
        }

        public static void WriteInt32(Stream ms, Int32 i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            ms.Write(bytes, 0, bytes.Length);
        }

        public static void WriteFloat(Stream ms, float f)
        {
            byte[] bytes = BitConverter.GetBytes(f);
            ms.Write(bytes, 0, bytes.Length);
        }

        public static void WriteUInt16(Stream ms, UInt16 i)
        {
            byte[] bytes = BitConverter.GetBytes(i);
            ms.Write(bytes, 0, bytes.Length);
        }

        public static void WriteBytes(Stream ms, byte[] bytes, int start = 0, int length = -1)
        {
            if (bytes != null)
                ms.Write(bytes, start, length == -1 ? bytes.Length - start : length);
        }

        public static void WriteInt16(Stream ms, int i)
        {
            byte[] bytes = BitConverter.GetBytes((Int16)i);
            ms.Write(bytes, 0, bytes.Length);
        }

        public static void WriteBool(Stream ms, bool b)
        {
            ms.WriteByte((byte)(b ? 1 : 0));
        }

        public static UInt16 ReadUInt16(Stream ms)
        {
            byte[] bytes = new byte[2];
            ms.Read(bytes, 0, 2);
            return BitConverter.ToUInt16(bytes, 0);
        }

        public static Int32 ReadInt32(Stream ms)
        {
            byte[] bytes = new byte[4];
            ms.Read(bytes, 0, 4);
            return BitConverter.ToInt32(bytes, 0);
        }

        public static Int16 ReadInt16(Stream ms)
        {
            byte[] bytes = new byte[2];
            ms.Read(bytes, 0, 2);
            return BitConverter.ToInt16(bytes, 0);
        }

        public static float ReadFloat(Stream ms)
        {
            byte[] bytes = new byte[4];
            ms.Read(bytes, 0, 4);
            return BitConverter.ToSingle(bytes, 0);
        }

        public static byte[] Combine(params byte[][] bytes)
        {
            byte[] bytesOut = new byte[bytes.Sum<byte[]>(f => f.Length)];
            int iOffset = 0;
            foreach (byte[] array in bytes)
            {
                Buffer.BlockCopy(array, 0, bytesOut, iOffset, array.Length);
                iOffset += array.Length;
            }
            return bytesOut;
        }

        public static MemoryBytes Combine(params MemoryBytes[] bytes)
        {
            byte[] bytesOut = new byte[bytes.Sum<MemoryBytes>(f => f.Length)];
            int iOffset = 0;
            foreach (MemoryBytes mb in bytes)
            {
                Buffer.BlockCopy(mb, 0, bytesOut, iOffset, mb.Length);
                iOffset += mb.Length;
            }
            return new MemoryBytes(bytesOut);
        }

        public static Keys MenuKeys(Keys[] keys)
        {
            // Convert an array of non-modifier keys into a menu-shortcut-style set of keys and modifiers, if possible
            // A proper menu-style Keys value may only have a maximum of one of each of Control, Shift, and/or Menu and
            // a single non-modifier key
            if (keys == null || keys.Length > 4 || keys.Length < 1)
                return Keys.None;

            Keys keyShift = Keys.None;
            Keys keyControl = Keys.None;
            Keys keyMenu = Keys.None;
            Keys keyMain = Keys.None;

            foreach (Keys key in keys)
            {
                switch (key)
                {
                    case Keys.Control:
                    case Keys.ControlKey:
                    case Keys.LControlKey:
                    case Keys.RControlKey:
                        keyControl = Keys.Control;
                        break;
                    case Keys.Shift:
                    case Keys.ShiftKey:
                    case Keys.LShiftKey:
                    case Keys.RShiftKey:
                        keyShift = Keys.Shift;
                        break;
                    case Keys.Menu:
                    case Keys.Alt:
                    case Keys.LMenu:
                    case Keys.RMenu:
                        keyMenu = Keys.Alt;
                        break;
                    default:
                        keyMain = key;
                        break;
                }
            }

            if (keyMain == Keys.None)
                return Keys.None;

            // A menu shortcut cannot be just Shift+Key; windows does not allow it
            if (keyShift != Keys.None && keyControl == Keys.None && keyMenu == Keys.None)
                return Keys.None;

            return keyControl | keyShift | keyMenu | keyMain;
        }

        public static Direction Opposite(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return Direction.Down;
                case Direction.Down: return Direction.Up;
                case Direction.Right: return Direction.Left;
                case Direction.Left: return Direction.Right;
                case Direction.UpRight: return Direction.DownLeft;
                case Direction.UpLeft: return Direction.DownRight;
                case Direction.DownRight: return Direction.UpLeft;
                case Direction.DownLeft: return Direction.UpRight;
                case Direction.None: return Direction.All;
                default: return Direction.None;
            }
        }

        public static Point OffsetPoint(Point pt, Direction dir, int iCount = 1)
        {
            switch (dir)
            {
                case Direction.Up: return new Point(pt.X, pt.Y - iCount);
                case Direction.Down: return new Point(pt.X, pt.Y + iCount);
                case Direction.Right: return new Point(pt.X + iCount, pt.Y);
                case Direction.Left: return new Point(pt.X - iCount, pt.Y);
                case Direction.UpRight: return new Point(pt.X + iCount, pt.Y - iCount);
                case Direction.UpLeft: return new Point(pt.X - iCount, pt.Y - iCount);
                case Direction.DownRight: return new Point(pt.X + iCount, pt.Y + iCount);
                case Direction.DownLeft: return new Point(pt.X - iCount, pt.Y + iCount);
                default: return pt;
            }
        }

        public static DirectionFlags Opposite(DirectionFlags dir)
        {
            switch (dir)
            {
                case DirectionFlags.North: return DirectionFlags.South;
                case DirectionFlags.South: return DirectionFlags.North;
                case DirectionFlags.East: return DirectionFlags.West;
                case DirectionFlags.West: return DirectionFlags.East;
                case DirectionFlags.NorthEast: return DirectionFlags.SouthWest;
                case DirectionFlags.NorthWest: return DirectionFlags.SouthEast;
                case DirectionFlags.SouthEast: return DirectionFlags.NorthWest;
                case DirectionFlags.SouthWest: return DirectionFlags.NorthEast;
                case DirectionFlags.None: return DirectionFlags.All;
                case DirectionFlags.West | DirectionFlags.East: return DirectionFlags.North | DirectionFlags.South;
                case DirectionFlags.North | DirectionFlags.South: return DirectionFlags.West | DirectionFlags.East;
                default: return DirectionFlags.None;
            }
        }

        public static int FindNext(ComboBox comboBox, string strFind, bool bNext = true)
        {
            strFind = strFind.ToLower();

            if (comboBox.Items.Count < 1)
                return -1;

            int iStart = comboBox.SelectedIndex;

            int iDelta = bNext ? 1 : -1;
            int iIndex = iStart + iDelta;

            while (iIndex != iStart)
            {
                if (iIndex >= comboBox.Items.Count)
                    iIndex = 0;
                else if (iIndex < 0)
                    iIndex = comboBox.Items.Count - 1;

                if (comboBox.Items[iIndex].ToString().ToLower().Contains(strFind))
                {
                    comboBox.SelectedIndex = iIndex;
                    return iIndex;
                }
                iIndex += iDelta;
            }

            return -1;
        }

        public static bool FormVisible(Form form)
        {
            if (form == null)
                return false;
            if (form.IsDisposed)
                return false;
            if (!form.Visible)
                return false;

            return true;
        }

        public static int LevelFromString(string str)
        {
            string strLower = str.ToLower();
            Match match = Regex.Match(str, @"level (\d+)", RegexOptions.IgnoreCase);
            if (match.Success)
            {
                int iLevel = Convert.ToInt32(match.Groups[1].Value) - 1;
                bool bLevelsGoDown = false;
                if (strLower.Contains("dungeon") ||
                    strLower.Contains("temple")
                   )
                    bLevelsGoDown = true;
                return bLevelsGoDown ? -iLevel : iLevel;
            }
            if (strLower.Contains("surface"))
                return 0;
            if (strLower.Contains("clouds"))
                return 6;
            if (strLower.Contains("dungeon"))
                return -1;
            if (strLower.Contains("troll holes"))
                return -1;
            if (strLower.Contains("sewer"))
                return -1;
            if (strLower.Contains("skyroad"))
                return 6;
            return 0;
        }

        public static string GetHWndListString(IEnumerable<IntPtr> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (IntPtr p in list)
            {
                sb.AppendFormat("{0:X8}, ", (uint)p);
            }

            Global.Trim(sb);
            if (sb.Length == 0)
                return "<none>";

            return sb.ToString();
        }

        public static bool AreAdjacent(Point ptCenter, Point pt1, Point pt2)
        {
            if (pt1.X == pt2.X && ptCenter.X == pt1.X && pt1.Y == ptCenter.Y - 1 && pt2.Y == ptCenter.Y + 1)
                return true;
            if (pt1.X == pt2.X && ptCenter.X == pt1.X && pt1.Y == ptCenter.Y + 1 && pt2.Y == ptCenter.Y - 1)
                return true;
            if (pt1.Y == pt2.Y && ptCenter.Y == pt1.Y && pt1.X == ptCenter.X - 1 && pt2.X == ptCenter.X + 1)
                return true;
            if (pt1.Y == pt2.Y && ptCenter.Y == pt1.Y && pt1.X == ptCenter.X + 1 && pt2.X == ptCenter.X - 1)
                return true;
            return false;
        }

        public static bool IsSouthwest(Point ptTest, Point pt1, Point pt2)
        {
            if (pt1.X == ptTest.X + 1 && pt1.Y == ptTest.Y && pt2.X == ptTest.X && pt2.Y == ptTest.Y + 1)
                return true;
            if (pt2.X == ptTest.X + 1 && pt2.Y == ptTest.Y && pt1.X == ptTest.X && pt1.Y == ptTest.Y + 1)
                return true;
            return false;
        }

        public static bool IsSoutheast(Point ptTest, Point pt1, Point pt2)
        {
            if (pt1.X == ptTest.X - 1 && pt1.Y == ptTest.Y && pt2.X == ptTest.X && pt2.Y == ptTest.Y + 1)
                return true;
            if (pt2.X == ptTest.X - 1 && pt2.Y == ptTest.Y && pt1.X == ptTest.X && pt1.Y == ptTest.Y + 1)
                return true;
            return false;
        }

        public static bool IsNorthwest(Point ptTest, Point pt1, Point pt2)
        {
            if (pt1.X == ptTest.X + 1 && pt1.Y == ptTest.Y && pt2.X == ptTest.X && pt2.Y == ptTest.Y - 1)
                return true;
            if (pt2.X == ptTest.X + 1 && pt2.Y == ptTest.Y && pt1.X == ptTest.X && pt1.Y == ptTest.Y - 1)
                return true;
            return false;
        }

        public static bool IsNortheast(Point ptTest, Point pt1, Point pt2)
        {
            if (pt1.X == ptTest.X - 1 && pt1.Y == ptTest.Y && pt2.X == ptTest.X && pt2.Y == ptTest.Y - 1)
                return true;
            if (pt2.X == ptTest.X - 1 && pt2.Y == ptTest.Y && pt1.X == ptTest.X && pt1.Y == ptTest.Y - 1)
                return true;
            return false;
        }

        public static void RestartTimer(System.Windows.Forms.Timer timer)
        {
            timer.Stop();
            timer.Start();
        }

        public static byte[] ByteArray(int size, byte init)
        {
            byte[] result = new byte[size];
            for (int i = 0; i < size; i++)
                result[i] = init;
            return result;
        }

        public static int[] IntArray(int size, int init)
        {
            int[] result = new int[size];
            for (int i = 0; i < size; i++)
                result[i] = init;
            return result;
        }

        public static void SetBytes(byte[] array, int offset, int length, byte val)
        {
            for (int i = offset; i - offset < length; i++)
                array[i] = val;
        }

        public static void SetBytes(byte[] target, int offsetTarget, byte[] source, int offsetSource = 0)
        {
            Buffer.BlockCopy(source, offsetSource, target, offsetTarget, source.Length);
        }

        public static MathOperation ReverseOperation(MathOperation op)
        {
            switch (op)
            {
                case MathOperation.Equal: return MathOperation.NotEqual;
                case MathOperation.NotEqual: return MathOperation.Equal;
                case MathOperation.IsTrue: return MathOperation.IsFalse;
                case MathOperation.IsFalse: return MathOperation.IsTrue;
                case MathOperation.GreaterThan: return MathOperation.LessThanOrEqual;
                case MathOperation.GreaterThanOrEqual: return MathOperation.LessThan;
                case MathOperation.LessThan: return MathOperation.GreaterThanOrEqual;
                case MathOperation.LessThanOrEqual: return MathOperation.GreaterThan;
                case MathOperation.IsZero: return MathOperation.IsNotZero;
                case MathOperation.IsNotZero: return MathOperation.IsZero;
                default: return op;
            }
        }

        public static string CreateSymbol(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return "?";
            str = str.Replace("\"", "");
            if (str.Length < 3)
                return str;
            return str.Substring(0, 2).Trim();
        }

        public static void InternalError(string str)
        {
            MessageBox.Show(str, "Internal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static string GetTimeString(int minutes)
        {
            return String.Format("{0}:{1:D2} {2}", minutes / 60 % 12 == 0 ? 12 : minutes / 60 % 12, minutes % 60, minutes >= 720 ? "PM" : "AM");
        }

        public static Color Lighten(Color c, int amount)
        {
            return Color.FromArgb(c.A, (byte)Math.Min(c.R + amount, 255), (byte)Math.Min(c.G + amount, 255), (byte)Math.Min(c.B + amount, 255));
        }

        public static Color Darken(Color c, int amount)
        {
            return Color.FromArgb(c.A, (byte)Math.Max(c.R - amount, 0), (byte)Math.Max(c.G - amount, 0), (byte)Math.Max(c.B - amount, 0));
        }

        public static Color Highlight(Color c, int amount)
        {
            if (c.R + c.G + c.B > 256 * 3 / 2)
                return Darken(c, amount);
            return Lighten(c, amount);
        }

        public static byte[,] NullBytes(int size1, int size2)
        {
            byte[,] bytes = new byte[size1, size2];

            for (int i = 0; i < size1; i++)
            {
                for (int j = 0; j < size2; j++)
                    bytes[i, j] = 0;
            }
            return bytes;
        }

        public static byte[] NullBytes(int size)
        {
            byte[] bytes = new byte[size];
            for (int i = 0; i < bytes.Length; i++)
                bytes[i] = 0;
            return bytes;
        }

        public static bool CheckRangeAndSet(byte[] bytes, int offset, byte val, object tag = null)
        {
            if (offset < 0 || offset >= bytes.Length)
                return false;

            WizardryCheatTag ct = tag as WizardryCheatTag;
            if (ct != null && ct.IsFiveBitStat)
            {
                PackedFiveBitValues p5b = new PackedFiveBitValues(bytes, offset);
                p5b.Values[ct.StatOffset] = val;
                byte[] bytesP5 = p5b.Bytes;
                Buffer.BlockCopy(bytesP5, 0, bytes, offset, bytesP5.Length);
            }
            else
                bytes[offset] = val;

            return true;
        }

        public static string IntString(params int[] ints)
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in ints)
                sb.AppendFormat("{0:D2}, ", i);
            return Global.Trim(sb).ToString();
        }

        public static int[] IntRange(int start, int count, int step = 1)
        {
            if (count < 1)
                return new int[0];

            int[] result = new int[count / step];
            for (int i = 0; i < count; i += step)
                result[i / step] = start + i;

            return result;
        }

        public static DirectionFlags ConvertDirection(Direction dir)
        {
            switch (dir)
            {
                case Direction.Up: return DirectionFlags.Up;
                case Direction.Right: return DirectionFlags.Right;
                case Direction.Down: return DirectionFlags.Down;
                case Direction.Left: return DirectionFlags.Left;
                case Direction.UpLeft: return DirectionFlags.UpLeft;
                case Direction.UpRight: return DirectionFlags.UpRight;
                case Direction.DownLeft: return DirectionFlags.DownLeft;
                case Direction.DownRight: return DirectionFlags.DownRight;
                case Direction.All: return DirectionFlags.All;
                default: return DirectionFlags.None;
            }
        }

        public static bool AllNull(byte[] bytes)
        {
            foreach (byte b in bytes)
                if (b != 0)
                    return false;

            return true;
        }

        public static bool AllNull(byte[] bytes, int iStart, int iCount)
        {
            if (bytes == null)
                return true;

            if (iStart < 0 || iCount < 0 || iStart + iCount > bytes.Length)
                return true;

            for (int i = iStart; i < iStart + iCount; i++)
                if (bytes[i] != 0)
                    return false;

            return true;
        }

        public static void SelectAll(Control ctrl)
        {
            if (ctrl is SplitContainer)
                SelectAll(((SplitContainer)ctrl).ActiveControl);
            else if (ctrl is TextBox)
                (ctrl as TextBox).SelectAll();
            else if (ctrl is ListView)
            {
                ListView lv = ctrl as ListView;
                if (!lv.MultiSelect)
                    return;
                lv.BeginUpdate();
                foreach (ListViewItem lvi in lv.Items)
                    lvi.Selected = true;
                lv.EndUpdate();
            }
            return;
        }

        public static bool ContainsByte(byte[] bytes, byte byteSearch)
        {
            foreach (byte b in bytes)
                if (byteSearch == b)
                    return true;

            return false;
        }

        public static void SetSplitterDistance(SplitContainer sc, int iPos)
        {
            // SplitterDistance must be between Panel1MinSize and Width - Panel2MinSize (if vertical)
            if (iPos < sc.Panel1MinSize ||
                (sc.Orientation == Orientation.Vertical && iPos >= (sc.Width - sc.Panel2MinSize)) ||
                (sc.Orientation == Orientation.Horizontal && iPos >= (sc.Height - sc.Panel2MinSize)))
                return;     // Otherwise the attempt will throw an exception

            sc.SplitterDistance = iPos;
        }

        public static bool Compare(EncounterInfo info1, byte[] info2)
        {
            if (info1 == null && info2 == null)
                return true;

            if (info1 == null || info2 == null)
                return false;

            return Global.Compare(info1.AllBytes, info2);
        }

        public static bool Compare(PrimaryStat[] ps1, PrimaryStat[] ps2)
        {
            if (ps1 == null || ps2 == null)
                return false;

            if (ps1.Length != ps2.Length)
                return false;

            for (int i = 0; i < ps1.Length; i++)
                if (ps1[i] != ps2[i])
                    return false;

            return true;
        }

        public static bool Compare(StatAndModifier[] sm1, StatAndModifier[] sm2)
        {
            if (sm1 == null || sm2 == null)
                return false;

            if (sm1.Length != sm2.Length)
                return false;

            for (int i = 0; i < sm1.Length; i++)
                if (!sm1[i].Equals(sm2[i]))
                    return false;

            return true;
        }

        public static bool Compare(ResistanceValue[] rv1, ResistanceValue[] rv2)
        {
            if (rv1 == null || rv2 == null)
                return false;

            if (rv1.Length != rv2.Length)
                return false;

            for (int i = 0; i < rv1.Length; i++)
                if (rv1[i].CompareTo(rv2[i]) != 0)
                    return false;

            return true;
        }

        public static int MM2SlayerExp(int index)
        {
            switch (index >> 4)
            {
                case 0:
                case 1:
                case 2: return 2000;
                case 3: return 4000;
                case 4: return 5000;
                case 5: return 7000;
                case 6: return 10000;
                case 7: return 15000;
                case 8: return 25000;
                case 9: return 50000;
                case 10: return 100000;
                default: return 250000;
            }
        }

        public static string FacingString(Direction dir, bool bNSEW = true)
        {
            switch (dir)
            {
                case Direction.None: return "None";
                case Direction.Up: return bNSEW ? "North" : "Up";
                case Direction.UpRight: return bNSEW ? "Northeast" : "Up/Right";
                case Direction.Right: return bNSEW ? "East" : "Right";
                case Direction.DownRight: return bNSEW ? "Southeast" : "Down/Right";
                case Direction.Down: return bNSEW ? "South" : "Down";
                case Direction.DownLeft: return bNSEW ? "Southwest" : "Down/Left";
                case Direction.Left: return bNSEW ? "West" : "Left";
                case Direction.UpLeft: return bNSEW ? "Northwest" : "Up/Left";
                case Direction.All: return "All";
                default: return "Unknown";
            }
        }

        public static string DirectionString(DirectionFlags dir, bool bAbbreviated = false, bool bNSEW = true)
        {
            StringBuilder sb = new StringBuilder();
            string strUp = bAbbreviated ? "U" : "Up";
            string strNorth = bAbbreviated ? "N" : "North";
            string strRight = bAbbreviated ? "R" : "Right";
            string strEast = bAbbreviated ? "E" : "East";
            string strDown = bAbbreviated ? "D" : "Down";
            string strSouth = bAbbreviated ? "S" : "South";
            string strLeft = bAbbreviated ? "L" : "Left";
            string strWest = bAbbreviated ? "W" : "West";

            if (dir.HasFlag(DirectionFlags.Up))
                sb.AppendFormat("{0}{1}", bNSEW ? strNorth : strUp, bAbbreviated ? "" : "/");
            if (dir.HasFlag(DirectionFlags.Down))
                sb.AppendFormat("{0}{1}", bNSEW ? strSouth : strDown, bAbbreviated ? "" : "/");
            if (dir.HasFlag(DirectionFlags.Right))
                sb.AppendFormat("{0}{1}", bNSEW ? strEast : strRight, bAbbreviated ? "" : "/");
            if (dir.HasFlag(DirectionFlags.Left))
                sb.AppendFormat("{0}{1}", bNSEW ? strWest : strLeft, bAbbreviated ? "" : "/");

            if (sb.Length > 1 && !bAbbreviated)
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string An(string str, bool bUpper = false)
        {
            if (String.IsNullOrWhiteSpace(str))
                return "a";
            switch (str[0])
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'u':
                case 'A':
                case 'E':
                case 'I':
                case 'O':
                case 'U':
                    return (bUpper ? "An " : "an ") + str;
                default:
                    return (bUpper ? "A " : "a ") + str;
            }
        }

        public static string Abbreviate(string str, int iMax = 30)
        {
            if (str.Length <= iMax)
                return str;

            int iSpace = str.LastIndexOfAny(new char[] { ' ', '\t', '\r', '\n' }, iMax);

            if (iSpace == -1 || iSpace > iMax)
                return str.Substring(0, iMax) + "...";

            return str.Substring(0, iSpace) + "...";
        }

        public static int GetBit(byte[] bytes, int iBit, bool bReverse = false)
        {
            if (bytes == null)
                return -1;
            int iByte = iBit / 8;
            if (iByte >= bytes.Length)
                return -1;

            if (bReverse)
                return (bytes[iByte] >> (iBit % 8)) & 1;
            else
                return (bytes[iByte] >> (7 - (iBit % 8))) & 1;
        }

        public static void SetBit(byte[] bytes, int iBit, int iValue, bool bReverse = false)
        {
            int iByte = iBit / 8;
            if (iByte >= bytes.Length)
                return;

            int iBitPos = iBit % 8;
            if (!bReverse)
                iBitPos = 7 - iBitPos;

            if (iValue == 0)
                bytes[iByte] &= (byte)~(1 << iBitPos);
            else
                bytes[iByte] |= (byte)(1 << iBitPos);
        }

        public static List<int> GetBitsSet(byte[] bytes, bool bReverse = false)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < bytes.Length * 8; i++)
            {
                if (Global.GetBit(bytes, i, bReverse) != 0)
                    list.Add(i);
            }
            return list;
        }

        public static bool IsBitSet(byte[] bytes, MM3Bits.Party bit)
        {
            return (GetBit(bytes, (int)bit) == 1);
        }

        public static bool IsBitSet(byte b, int bit, bool reverse = false)
        {
            if (reverse)
                return (b & (0x80 >> bit)) > 0;
            return (b & (1 << bit)) > 0;
        }

        public static bool IsMapByteFlagSet(byte[][] maps, Point pt, byte bFlag, int iWidth = 16)
        {
            int iOffset = -1;
            int iMap = 0;
            if (pt.X < iWidth && pt.Y < iWidth)
            {
                iMap = 0;
                iOffset = pt.Y * iWidth + pt.X;
            }
            else if (pt.Y < iWidth)
            {
                iMap = 1;
                iOffset = pt.Y * iWidth + (pt.X - iWidth);
            }
            else if (pt.X < iWidth)
            {
                iMap = 2;
                iOffset = (pt.Y - iWidth) * iWidth + pt.X;
            }
            else
            {
                iMap = 3;
                iOffset = (pt.Y - iWidth) * iWidth + (pt.X - iWidth);
            }

            if (iOffset >= maps[iMap].Length)
                return false;

            return (maps[iMap][iOffset] & bFlag) != 0;
        }

        public static int NumBitsSet(object obj)
        {
            if (obj == null)
                return 0;

            if (obj is byte[])
                return NumBitsSet((byte[])obj);
            if (obj is UInt32)
                return NumBitsSet((UInt32)obj, 32);
            if (obj is UInt16)
                return NumBitsSet((UInt32)obj, 16);
            if (obj is byte)
                return NumBitsSet((UInt32)obj, 8);

            return 0;
        }

        public static int NumBitsSet(long iVal, int width)
        {
            int iBitsSet = 0;
            for (int i = 0; i < width; i++)
            {
                if ((iVal & 1) > 0)
                    iBitsSet++;
                iVal >>= 1;

                if (iVal == 0)
                    break;
            }

            return iBitsSet;
        }

        public static int NumBitsSet(byte[] bytes)
        {
            if (bytes == null)
                return 0;

            int iBitsSet = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                byte b = bytes[i];
                while (b > 0)
                {
                    iBitsSet += (b & 1);
                    b >>= 1;
                }
            }

            return iBitsSet;
        }

        public static string TrainString(int iCurrentLevel, int iTrainableLevel)
        {
            if (iCurrentLevel >= iTrainableLevel)
                return "Can't Train";
            int iDelta = iTrainableLevel - iCurrentLevel;
            return String.Format("Train{0}!", iDelta > 1 ? (" " + iDelta.ToString()) : "");
        }

        public static string Plural(int iNumber, string sObj, string sPlural = null)
        {
            if (iNumber == 1)
                return String.Format("{0} {1}", iNumber, sObj);

            if (sPlural == null)
                return String.Format("{0} {1}s", iNumber, sObj);

            return String.Format("{0} {1}", iNumber, sPlural);
        }

        public static string Plural(string strNumber, string sObj, string sPlural = null)
        {
            int iNumber;
            if (Int32.TryParse(strNumber, out iNumber))
                return Plural(iNumber, sObj, sPlural);

            return String.Format("{0} {1}", strNumber, sObj);
        }

        public static string AddPlus(long i, bool bShowZero = true)
        {
            if (i == 0 && !bShowZero)
                return String.Empty;

            if (i < 0)
                return String.Format("{0}", i);
            return String.Format("+{0}", i);
        }

        public static string AddPlus(double d, bool bShowZero = true)
        {
            if (d == 0 && !bShowZero)
                return String.Empty;

            if (d < 0)
                return String.Format("{0}", d);
            return String.Format("+{0}", d);
        }

        public static string SpaceParen(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return str;
            return String.Format(" ({0})", str);
        }

        public static string CommaSpace(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return str;
            return String.Format(", {0}", str);
        }

        public static string AddCommaSpace(string str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return str;
            return String.Format("{0}, ", str);
        }

        public static string PointString(Point pt)
        {
            // For comparisons
            return String.Format("{0:D3}{1:D3}", pt.X, pt.Y);
        }

        public static byte StringToByte(string s)
        {
            byte b = 0;
            Byte.TryParse(s, System.Globalization.NumberStyles.AllowHexSpecifier, null as IFormatProvider, out b);
            return b;
        }

        public static byte[] StringToBytes(string s)
        {
            MemoryStream stream = new MemoryStream(s.Length / 2);
            for (int i = 0; i < s.Length - 1; i += 2)
                stream.WriteByte(StringToByte(s.Substring(i, 2)));
            return stream.ToArray();
        }

        public static string Title(string str)
        {
            StringBuilder sbTitle = new StringBuilder(g_TextInfo.ToTitleCase(str));
            sbTitle.Replace(" Of ", " of ");
            sbTitle.Replace(" And ", " and ");
            if (sbTitle.Length >= 3)
                sbTitle.Replace("Id ", "ID ", 0, 3);
            return sbTitle.ToString();
        }

        public static string SingleResistance(GenericResistanceFlags resist)
        {
            switch (resist)
            {
                case GenericResistanceFlags.Fire: return "Fire";
                case GenericResistanceFlags.Electricity: return "Electric";
                case GenericResistanceFlags.Acid: return "Acid";
                case GenericResistanceFlags.Cold: return "Cold";
                case GenericResistanceFlags.Paralyze: return "Paralyze";
                case GenericResistanceFlags.Sleep: return "Sleep";
                case GenericResistanceFlags.Male: return "Male";
                case GenericResistanceFlags.Female: return "Female";
                case GenericResistanceFlags.Weapons: return "Weapons";
                case GenericResistanceFlags.Poison: return "Poison";
                case GenericResistanceFlags.Fear: return "Fear";
                case GenericResistanceFlags.Energy: return "Energy";
                case GenericResistanceFlags.Mental: return "Mental";
                case GenericResistanceFlags.Magic: return "Magic";
                case GenericResistanceFlags.Blessed: return "Blessed";
                case GenericResistanceFlags.PowerShield: return "P.Shield";
                case GenericResistanceFlags.HolyBonus: return "H.Bonus";
                case GenericResistanceFlags.Heroism: return "Heroism";
                default: return "Unknown";
            }
        }

        public static string GetNullTerminatedString(byte[] bytes, int index, int max)
        {
            int iLength = 0;
            while (bytes[index + iLength] != 0)
            {
                iLength++;
                if (iLength >= max)
                    break;
            }

            return Encoding.ASCII.GetString(bytes, index, iLength).Trim();
        }

        public static byte[] Uncompress(byte[] bytesCompressed)
        {
            MemoryStream ms = new MemoryStream(bytesCompressed);
            GZipStream gz = new GZipStream(ms, CompressionMode.Decompress);
            MemoryStream msOut = new MemoryStream();
            gz.CopyTo(msOut);
            return msOut.ToArray();
        }

        public static SpellType GetSpellType(GenericClass mmClass)
        {
            switch (mmClass)
            {
                case GenericClass.Archer:
                case GenericClass.Sorcerer:
                    return SpellType.Sorcerer;
                case GenericClass.Paladin:
                case GenericClass.Cleric:
                    return SpellType.Cleric;
                case GenericClass.Ranger:
                case GenericClass.Druid:
                    return SpellType.Druid;
                default:
                    return SpellType.Unknown;
            }
        }

        public static string StatString(PrimaryStat stat)
        {
            return GenericAttributeString(GetAttribute(stat));
        }

        public static GenericAttribute GetAttribute(PrimaryStat stat)
        {
            switch (stat)
            {
                case PrimaryStat.Might: return GenericAttribute.Might;
                case PrimaryStat.Intellect: return GenericAttribute.Intellect;
                case PrimaryStat.Personality: return GenericAttribute.Personality;
                case PrimaryStat.Endurance: return GenericAttribute.Endurance;
                case PrimaryStat.Speed: return GenericAttribute.Speed;
                case PrimaryStat.Accuracy: return GenericAttribute.Accuracy;
                case PrimaryStat.Luck: return GenericAttribute.Luck;
                case PrimaryStat.Strength: return GenericAttribute.Strength;
                case PrimaryStat.IQ: return GenericAttribute.IQ;
                case PrimaryStat.Piety: return GenericAttribute.Piety;
                case PrimaryStat.Vitality: return GenericAttribute.Vitality;
                case PrimaryStat.Agility: return GenericAttribute.Agility;
                case PrimaryStat.Dexterity: return GenericAttribute.Dexterity;
                case PrimaryStat.Constitution: return GenericAttribute.Constitution;
                default: return GenericAttribute.None;
            }
        }

        public static string GenericAttributeString(GenericAttribute attrib)
        {
            switch (attrib)
            {
                case GenericAttribute.Might: return "Might";
                case GenericAttribute.Intellect: return "Intellect";
                case GenericAttribute.Personality: return "Personality";
                case GenericAttribute.Endurance: return "Endurance";
                case GenericAttribute.Speed: return "Speed";
                case GenericAttribute.Accuracy: return "Accuracy";
                case GenericAttribute.Luck: return "Luck";
                case GenericAttribute.Strength: return "Strength";
                case GenericAttribute.IQ: return "I.Q.";
                case GenericAttribute.Piety: return "Piety";
                case GenericAttribute.Vitality: return "Vitality";
                case GenericAttribute.Agility: return "Agility";
                case GenericAttribute.HP: return "HP";
                case GenericAttribute.SP: return "SP";
                case GenericAttribute.AC: return "AC";
                case GenericAttribute.Thievery: return "Thievery";
                case GenericAttribute.Sex: return "Sex";
                case GenericAttribute.Race: return "Race";
                case GenericAttribute.Level: return "Level";
                case GenericAttribute.Age: return "Age";
                case GenericAttribute.SpellLevel: return "Spell Level";
                case GenericAttribute.Food: return "Food";
                case GenericAttribute.MagicRes: return "Magic Res";
                case GenericAttribute.FireRes: return "Fire Res";
                case GenericAttribute.ColdRes: return "Cold Res";
                case GenericAttribute.ElecRes: return "Electric Res";
                case GenericAttribute.AcidRes: return "Acid Res";
                case GenericAttribute.PoisonRes: return "Poison Res";
                case GenericAttribute.SleepRes: return "Sleep Res";
                case GenericAttribute.FearRes: return "Fear Res";
                case GenericAttribute.EnergyRes: return "Energy Res";
                case GenericAttribute.Cursed: return "Cursed";
                case GenericAttribute.Gems: return "Gems";
                case GenericAttribute.Gold: return "Gold";
                case GenericAttribute.Dexterity: return "Dexterity";
                case GenericAttribute.Constitution: return "Constitution";
                default: return "None";
            }
        }

        public static string GetBits(byte b)
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                (b & 0x80) > 0 ? "1" : ".",
                (b & 0x40) > 0 ? "1" : ".",
                (b & 0x20) > 0 ? "1" : ".",
                (b & 0x10) > 0 ? "1" : ".",
                (b & 0x08) > 0 ? "1" : ".",
                (b & 0x04) > 0 ? "1" : ".",
                (b & 0x02) > 0 ? "1" : ".",
                (b & 0x01) > 0 ? "1" : ".");
        }

        public static bool ValidWindowSize(Rectangle rc)
        {
            if (rc.Location == NullPoint)
                return false;
            if (rc.Location == NullPoint32000)
                return false;
            if (rc == Rectangle.Empty)
                return false;

            return true;
        }

        public static string EraString(int era)
        {
            switch (era)
            {
                case -1: return "";
                case 0: return "1st Century (Era 0)";
                case 1: return "2st Century (Era 1)";
                case 2: return "3rd Century (Era 2)";
                default: return String.Format("{0}th Century (Era {1})", era + 1, era);
            }
        }

        public static string GetBits(byte b, string on, string off)
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                (b & 0x80) > 0 ? on[0] : off[0],
                (b & 0x40) > 0 ? on[1] : off[1],
                (b & 0x20) > 0 ? on[2] : off[2],
                (b & 0x10) > 0 ? on[3] : off[3],
                (b & 0x08) > 0 ? on[4] : off[4],
                (b & 0x04) > 0 ? on[5] : off[5],
                (b & 0x02) > 0 ? on[6] : off[6],
                (b & 0x01) > 0 ? on[7] : off[7]);
        }

        public static List<RLEByte> GetBytesRLE(byte[] bytes, int iStart = 0, int iLength = -1)
        {
            List<RLEByte> list = new List<RLEByte>();

            if (bytes == null)
                return list;

            if (iLength == -1)
                iLength = bytes.Length;

            if (iLength < 1 || iLength > bytes.Length)
                return list;

            int i = iStart;
            int iCount = 1;
            while (i < iStart + iLength)
            {
                if (i < iStart + iLength - 1 && bytes[i] == bytes[i + 1])
                    iCount++;
                else
                {
                    list.Add(new RLEByte(bytes[i], iCount));
                    iCount = 1;
                }
                i++;
            }

            if (iCount > 1)
                list.Add(new RLEByte(bytes[iLength - 1], iCount));

            return list;
        }

        public static string ByteStringCombined(byte[] bytes)
        {
            if (bytes == null)
                return "<null>";

            List<RLEByte> bytesRLE = GetBytesRLE(bytes);

            StringBuilder sb = new StringBuilder();
            foreach (RLEByte rle in bytesRLE)
            {
                if (rle.Count < 4)  // don't bother to abbreviate strings of fewer than 4 bytes
                    sb.AppendFormat("{0} ", ByteString(rle));
                else
                    sb.AppendFormat("[{0:X2}*{1}] ", rle.Byte, rle.Count);
            }

            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);

            return sb.ToString();
        }

        public static string ByteString(byte[] bytes, bool bSpaces = true)
        {
            if (bytes == null)
                return "<null>";

            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            string strFormat = "{0:X2}" + (bSpaces ? " " : "");
            foreach (byte b in bytes)
                sb.AppendFormat(strFormat, b);
            if (sb.Length > 0 && bSpaces)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static bool IsHexChar(char c)
        {
            c = Char.ToUpper(c);

            if (Char.IsDigit(c))
                return true;

            return (c >= 'A' && c <= 'F');
        }

        public static long ConvertBytesToLong(byte[] bytes, int iIndex = 0)
        {
            if (bytes.Length - iIndex >= 8)
                return BitConverter.ToInt64(bytes, iIndex);
            if (bytes.Length - iIndex >= 4)
                return BitConverter.ToInt32(bytes, iIndex);
            if (bytes.Length - iIndex >= 2)
                return BitConverter.ToInt16(bytes, iIndex);
            if (bytes.Length - iIndex >= 1)
                return bytes[iIndex];
            throw new ArgumentException("Array must contain at least one byte", "bytes");
        }

        public static byte[] BytesFromRelaxedString(string str, int iForceLength = -1)
        {
            MemoryStream ms = new MemoryStream(str.Length / 2);

            int iCurrent = -1;
            for (int i = 0; i < str.Length; i++)
            {
                if (iForceLength > -1 && ms.Length == iForceLength)
                    break;

                if (!IsHexChar(str[i]))
                {
                    if (iCurrent != -1)
                    {
                        ms.WriteByte((byte)iCurrent);
                        iCurrent = -1;
                    }

                    if (str[i] == '.')
                    {
                        i++;
                        int iStartDecimal = i;
                        while (i < str.Length && Char.IsDigit(str[i]))
                            i++;
                        if (iStartDecimal == i)
                            ms.WriteByte(0);
                        else
                        {
                            long lTemp;
                            if (Int64.TryParse(str.Substring(iStartDecimal, i - iStartDecimal), out lTemp))
                            {
                                byte[] pbTemp = BitConverter.GetBytes(lTemp);
                                if (lTemp < 0x100)
                                    ms.WriteByte((byte)lTemp);
                                else if (lTemp < 0x10000)
                                    ms.Write(pbTemp, 0, 2);
                                else if (lTemp < 0x100000000)
                                    ms.Write(pbTemp, 0, 4);
                                else
                                    ms.Write(pbTemp, 0, 8);
                            }
                            else
                                ms.WriteByte(0);
                        }
                    }
                    continue;
                }

                if (iCurrent == -1)
                    iCurrent = Convert.ToByte(str.Substring(i, 1), 16);
                else
                {
                    iCurrent *= 16;
                    iCurrent += Convert.ToByte(str.Substring(i, 1), 16);
                    ms.WriteByte((byte)iCurrent);
                    iCurrent = -1;
                }
            }

            if (iCurrent > -1 && (iForceLength == -1 || ms.Length < iForceLength))
                ms.WriteByte((byte)iCurrent);

            if (iForceLength > -1)
            {
                while (ms.Length < iForceLength)
                    ms.WriteByte(0);
            }

            return ms.ToArray();
        }

        public static string GetHRString(long num)
        {
            if (num < 10000)
                return num.ToString();
            if (num < 1000000)
                return String.Format("{0}K", num / 1000);
            if (num < 1000000000)
                return String.Format("{0}M", num / 1000000);
            return String.Format("{0}G", num / 1000000000);
        }

        public static void UpdateBonusTable(ListView lvBonusTable, MemoryHacker hacker, PrimaryStat stat, int iMaxStat = -1)
        {
            lvBonusTable.BeginUpdate();
            lvBonusTable.Items.Clear();
            ListViewItem lvi;

            StatModifier mod = StatModifier.Zero;
            int i = 0;
            int iCount = 0;
            do
            {
                mod = hacker.GetStatModifier(i, stat);
                if (mod.Next == -1)
                    lvi = new ListViewItem(String.Format("{0}+", i));
                else if (mod.Next - 1 > i)
                    lvi = new ListViewItem(String.Format("{0} - {1}", i, mod.Next - 1));
                else
                    lvi = new ListViewItem(String.Format("{0}", i));
                lvi.SubItems.Add(Global.AddPlus(mod.Value));
                lvBonusTable.Items.Add(lvi);
                i = mod.Next;
            } while (mod.Next != -1 && iCount++ < 50 && (iMaxStat == -1 || i <= iMaxStat));

            if (lvBonusTable.Parent != null)
                lvBonusTable.BackColor = lvBonusTable.Parent.BackColor;

            lvBonusTable.EndUpdate();
        }

        public static byte UpdateFlag(byte original, byte update, bool bValue)
        {
            if (bValue)
                return (byte)(original | update);
            else
                return (byte)(original & ~update);
        }

        public static string GetStrings(int iMinLength, byte[] bytes, bool bNewlinesToSpaces = false)
        {
            StringBuilder sb = new StringBuilder();
            ASCIIEncoding ascii = new ASCIIEncoding();
            int i = 0;
            int iCurrentLen = 0;
            while (i < bytes.Length)
            {
                if ((bytes[i] < 127 && bytes[i] > 31) || (bytes[i] == 10 || bytes[i] == 13 || bytes[i] == 9))
                {
                    iCurrentLen++;
                }
                else
                {
                    if (iCurrentLen >= iMinLength)
                    {
                        string str = new String(ascii.GetChars(bytes, i - iCurrentLen, iCurrentLen));
                        if (bNewlinesToSpaces)
                            sb.AppendLine(str.Replace('\n', ' '));
                        else
                            sb.AppendLine(str);
                    }
                    iCurrentLen = 0;
                }
                i++;
            }
            return sb.ToString();
        }

        public static string HexString(byte[] bytes, int offset = 0, int length = -1)
        {
            if (length == -1)
                length = bytes.Length - offset;
            StringBuilder sb = new StringBuilder(bytes.Length * 3);

            foreach (byte b in bytes)
                sb.AppendFormat("{0:X2} ", b);
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string HexString(int i) { return HexString(BitConverter.GetBytes(i)); }

        public static DirectionFlags DirectionFromWMSZ(IntPtr wmsz)
        {
            switch ((int)wmsz)
            {
                case NativeMethods.WMSZ_BOTTOM: return DirectionFlags.Down;
                case NativeMethods.WMSZ_BOTTOMLEFT: return DirectionFlags.DownLeft;
                case NativeMethods.WMSZ_BOTTOMRIGHT: return DirectionFlags.DownRight;
                case NativeMethods.WMSZ_LEFT: return DirectionFlags.Left;
                case NativeMethods.WMSZ_RIGHT: return DirectionFlags.Right;
                case NativeMethods.WMSZ_TOP: return DirectionFlags.Up;
                case NativeMethods.WMSZ_TOPLEFT: return DirectionFlags.UpLeft;
                case NativeMethods.WMSZ_TOPRIGHT: return DirectionFlags.UpRight;
                default: return DirectionFlags.None;
            }
        }

        public static void SnapToWindows(int Msg, DirectionFlags sizing, SnapWindows snapWindows, ref NativeMethods.RECT rc)
        {
            if (snapWindows == null)
                return;

            if (snapWindows.FormMoving.IsDisposed)
                return;

            Rectangle rcProper = Rectangle.Empty;

            if (Msg == NativeMethods.WM_MOVING || !(sizing.HasFlag(DirectionFlags.Right) || sizing.HasFlag(DirectionFlags.Down)))
                rcProper = new Rectangle(Cursor.Position.X - snapWindows.SnapOffsetPositive.X, Cursor.Position.Y - snapWindows.SnapOffsetPositive.Y, rc.Right - rc.Left, rc.Bottom - rc.Top);
            else
                rcProper = new Rectangle(rc.Left, rc.Top, Cursor.Position.X - snapWindows.SnapOffsetNegative.X - rc.Left, Cursor.Position.Y - snapWindows.SnapOffsetNegative.Y - rc.Top);

            Rectangle rcCompare = rcProper;
            //if (!snapWindows.Margins.IsEmpty)
            //{
            //    rcCompare.Offset(snapWindows.Margins.Left, snapWindows.Margins.Top);
            //    rcCompare.Inflate(-snapWindows.Margins.Left - snapWindows.Margins.Right, -snapWindows.Margins.Top - snapWindows.Margins.Bottom);
            //}

            Rectangle rcPrev = new Rectangle(rc.Left, rc.Top, rc.Right - rc.Left, rc.Bottom - rc.Top);
            int iRange = snapWindows.SnapRange;

            bool bSnappedHoriz = false;
            bool bSnappedVert = false;
            bool bExtended = Properties.Settings.Default.UseExtendedWindowRect;
            int iExtendedLeft = 0;
            int iExtendedRight = 0;
            int iExtendedTop = 0;
            int iExtendedBottom = 0;
            if (bExtended)
            {
                Rectangle rcExtended = NativeMethods.GetExtendedWindowRect(snapWindows.FormMoving.Handle);
                if (!rcExtended.IsEmpty)
                {
                    iExtendedLeft = rcExtended.Left - snapWindows.FormMoving.Bounds.Left;
                    iExtendedRight = snapWindows.FormMoving.Bounds.Right - rcExtended.Right;
                    iExtendedTop = rcExtended.Top - snapWindows.FormMoving.Bounds.Top;
                    iExtendedBottom = snapWindows.FormMoving.Bounds.Bottom - rcExtended.Bottom;
                }
            }

            foreach (EdgeRect rcSnap in snapWindows.EdgeRects)
            {
                Global.Log("rcSnap: {0},{1} {2}x{3}", rcSnap.Left, rcSnap.Top, rcSnap.Width, rcSnap.Height);
                bool bHoriz = rcSnap.Width > rcSnap.Height;

                if (bHoriz)
                {
                    int iSameBottom = (rcSnap.Dir == Direction.Down ? snapWindows.Margins.HeightDelta : 0);
                    int iSameTop = (rcSnap.Dir == Direction.Up ? snapWindows.Margins.HeightDelta : 0);
                    if (rcCompare.Right > rcSnap.Left && rcCompare.Left < rcSnap.Right && rcCompare.Top + iSameTop > rcSnap.Top && rcCompare.Top + iSameTop < rcSnap.Bottom)
                    {
                        // If any part of the top edge of the window is in one of the horizontal snap rectangles' heights
                        if (!bSnappedVert && sizing.HasFlag(DirectionFlags.Up))
                        {
                            bSnappedVert = true;
                            rc.Top = rcSnap.Top + iRange - iSameTop - (bExtended ? iExtendedTop : 0);
                        }
                    }
                    else if (rcCompare.Right > rcSnap.Left && rcCompare.Left < rcSnap.Right && rcCompare.Bottom - iSameBottom > rcSnap.Top && rcCompare.Bottom - iSameBottom < rcSnap.Bottom)
                    {
                        // If any part of the bottom edge of the window is in one of the horizontal snap rectangles' heights
                        if (!bSnappedVert && sizing.HasFlag(DirectionFlags.Down))
                        {
                            bSnappedVert = true;
                            rc.Bottom = rcSnap.Top + iRange + iSameBottom + (bExtended ? iExtendedBottom : 0);
                        }
                    }
                }
                else
                {
                    bool bSameLeft = rcSnap.Dir == Direction.Left;
                    int iSameRight = (rcSnap.Dir == Direction.Right ? snapWindows.Margins.WidthDelta : 0);
                    int iSameLeft = (rcSnap.Dir == Direction.Left ? snapWindows.Margins.WidthDelta : 0);
                    if (rcCompare.Bottom > rcSnap.Top && rcCompare.Top < rcSnap.Bottom && rcCompare.Left + iSameLeft > rcSnap.Left && rcCompare.Left + iSameLeft < rcSnap.Right)
                    {
                        // If any part of the left edge of the window is in one of the vertical snap rectangles' widths
                        if (!bSnappedHoriz && sizing.HasFlag(DirectionFlags.Left))
                        {
                            bSnappedHoriz = true;
                            rc.Left = rcSnap.Left + iRange - iSameLeft - (bExtended ? iExtendedLeft : 0);
                        }
                    }
                    else if (rcCompare.Bottom > rcSnap.Top && rcCompare.Top < rcSnap.Bottom && rcCompare.Right - iSameRight > rcSnap.Left && rcCompare.Right - iSameRight < rcSnap.Right)
                    {
                        // If any part of the right edge of the window is in one of the vertical snap rectangles' widths
                        if (!bSnappedHoriz && sizing.HasFlag(DirectionFlags.Right))
                        {
                            bSnappedHoriz = true;
                            rc.Right = rcSnap.Left + iRange + iSameRight + (bExtended ? iExtendedRight : 0);
                        }
                    }
                }
            }

            if (!bSnappedHoriz && !bSnappedVert)
            {
                // If we didn't snap to any windows at all, make sure the original rectangle is correct
                if (sizing.HasFlag(DirectionFlags.Left))
                    rc.Left = rcProper.Left;
                if (sizing.HasFlag(DirectionFlags.Up))
                    rc.Top = rcProper.Top;
            }
            else if (Msg == NativeMethods.WM_MOVING)
            {
                if (rc.Right != rcPrev.Right)
                    rc.Left = rc.Right - rcProper.Width;
                if (rc.Bottom != rcPrev.Bottom)
                    rc.Top = rc.Bottom - rcProper.Height;
            }
            if (Msg == NativeMethods.WM_MOVING)
            {
                rc.Right = rc.Left + rcProper.Width;
                rc.Bottom = rc.Top + rcProper.Height;
            }
            else
            {
                if (rc.Right - rc.Left < snapWindows.MinimumSize.Width)
                {
                    if (sizing.HasFlag(DirectionFlags.Right))
                        rc.Right = rc.Left + snapWindows.MinimumSize.Width;
                    else
                        rc.Left = rc.Right - snapWindows.MinimumSize.Width;
                }
                if (rc.Bottom - rc.Top < snapWindows.MinimumSize.Height)
                {
                    if (sizing.HasFlag(DirectionFlags.Down))
                        rc.Bottom = rc.Top + snapWindows.MinimumSize.Height;
                    else
                        rc.Top = rc.Bottom - snapWindows.MinimumSize.Height;
                }
            }
        }

        public static Size MinFormSize(Size szMin)
        {
            if (szMin.Width < 112)
                szMin.Width = 112;
            if (szMin.Height < 87)
                szMin.Height = 27;
            return szMin;
        }

        public static void LogError(string format, params object[] args)
        {
            if (args == null)
                Debugger.Log(0, "", format + "\r\n");
            else
                Debugger.Log(0, "", String.Format(format, args) + "\r\n");
        }

        public static void LogWarning(string warning)
        {
            Debugger.Log(0, "", warning + "\r\n");
        }

        public static void Log(string format, params object[] args)
        {
            if (args == null)
                Debugger.Log(0, "", format + "\r\n");
            else
                Debugger.Log(0, "", String.Format(format, args) + "\r\n");
        }

        public static int IndexOfStyle(DashStyle style)
        {
            switch (style)
            {
                case DashStyle.Dash: return 2;
                case DashStyle.Dot: return 1;
                default: return 0;
            }
        }

        public static void UpdateText(Control ctrl, string str)
        {
            // Avoids flicker
            if (ctrl.Text != str)
                ctrl.Text = str;
        }

        public static void UpdateText(ListViewItem lvi, string str)
        {
            // Avoids flicker
            if (lvi.Text != str)
                lvi.Text = str;
        }

        public static void UpdateText(ListViewItem.ListViewSubItem lvsi, string str)
        {
            // Avoids flicker
            if (lvsi.Text != str)
                lvsi.Text = str;
        }

        public static Rectangle HeaderRect(ListView lv)
        {
            Rectangle rc = lv.ClientRectangle;

            if (lv.Items.Count < 1)
                return lv.RectangleToClient(NativeMethods.GetListViewHeaderRect(lv));

            rc.Height = lv.GetItemRect(lv.TopItem.Index).Top;
            return rc;
        }

        public static void SizeHeadersAndContent(ListView lv, int iHeaderMargin = 0, bool bTrimLast = true, int[] maximums = null)
        {
            if (lv.Columns.Count < 1)
                return;

            bool[] zeroWidth = new bool[lv.Columns.Count];
            for (int i = 0; i < lv.Columns.Count; i++)
                zeroWidth[i] = lv.Columns[i].Width == 0;

            int[] minimums = new int[lv.Columns.Count];
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            if (iHeaderMargin > 0 && lv.Columns.Count > 0)
                lv.Columns[lv.Columns.Count - 1].Width -= iHeaderMargin;
            for (int i = 0; i < minimums.Length; i++)
                minimums[i] = lv.Columns[i].Width;
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            for (int i = 0; i < minimums.Length; i++)
            {
                if (zeroWidth[i])
                    lv.Columns[i].Width = 0;
                else if (minimums[i] > lv.Columns[i].Width)
                    lv.Columns[i].Width = minimums[i];
            }

            if (maximums != null)
            {
                for (int i = 0; i < lv.Columns.Count; i++)
                    if (maximums.Length > i && maximums[i] > -1 && lv.Columns[i].Width > maximums[i])
                        lv.Columns[i].Width = maximums[i];
            }

            if (bTrimLast && !zeroWidth[lv.Columns.Count - 1])
                lv.Columns[lv.Columns.Count - 1].Width -= 2;    // Prevent pointless horizontal scroll bar
        }

        public static void FitHeaders(ListView lv, int iHeaderMargin = 0)
        {
            if (lv.Columns.Count < 1)
                return;
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            if (iHeaderMargin > 0)
                lv.Columns[lv.Columns.Count - 1].Width -= iHeaderMargin;
            int iCount = 0;
            foreach (ColumnHeader ch in lv.Columns)
                iCount += ch.Width;
            if (iCount > lv.Width)
                lv.Columns[lv.Columns.Count - 1].Width -= (iCount - lv.Width + 4);
        }

        public static byte[] Subset(byte[] bytes, int iStart, int iLength = -1)
        {
            if (iStart > bytes.Length || iStart < 0)
                return null;
            if (iLength == -1)
                iLength = bytes.Length - iStart;

            if (iLength + iStart > bytes.Length)
                return null;

            byte[] subset = new byte[iLength];
            Buffer.BlockCopy(bytes, iStart, subset, 0, iLength);
            return subset;
        }

        public static void StripHighBits(byte[] bytes, int iLength = -1)
        {
            if (iLength == -1)
                iLength = bytes.Length;

            for (int i = 0; i < bytes.Length; i++)
            {
                if (i >= iLength)
                    break;
                bytes[i] = (byte)(bytes[i] & 0x7f);
                if (bytes[i] == 0x7f)
                    bytes[i] = 0;
            }
        }

        public static byte[] GetHighAsciiBytes(string str, int iMinLength, byte bPadding)
        {
            int iLength = Math.Max(iMinLength, str.Length);
            byte[] bytes = Global.ByteArray(iLength, bPadding);
            for (int i = 0; i < str.Length; i++)
                bytes[i] = (byte)(str[i] | 0x80);
            return bytes;
        }

        public static string GetLowAsciiString(byte[] bytes, int offset = 0, int iLength = -1)
        {
            if (bytes == null || bytes.Length == 0)
                return "";

            if (iLength == -1)
                iLength = bytes.Length - offset;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iLength; i++)
            {
                if (bytes[offset + i] != 0xff)
                    sb.AppendFormat("{0}", (char)(bytes[offset + i] & 0x7f));
            }

            return sb.ToString();
        }

        public static void FitSingleColumn(ListView lv)
        {
            if (lv.Columns.Count < 1)
                return;
            lv.Columns[0].Width = lv.Width - SystemInformation.VerticalScrollBarWidth - 4;
        }

        public static bool Compare(int[] ints1, int[] ints2)
        {
            if (ints1 == ints2)
                return true;
            if (ints1 == null || ints2 == null)
                return false;
            if (ints1.Length != ints2.Length)
                return false;
            for (int i = 0; i < ints1.Length; i++)
                if (ints1[i] != ints2[i])
                    return false;
            return true;
        }

        public static bool Compare(byte[] bytes1, byte[] bytes2)
        {
            if (bytes1 == bytes2)
                return true;
            if (bytes1 == null || bytes2 == null || bytes1.Length != bytes2.Length)
                return false;
            return NativeMethods.memcmp(bytes1, bytes2, bytes1.Length) == 0;
        }

        public static bool CompareBytes(byte[] bytes1, byte[] bytes2, int index1 = 0, int index2 = 0, int length = -1)
        {
            if (bytes1 == null || bytes2 == null)
                return false;

            if (length == -1)
            {
                length = Math.Min(bytes1.Length - index1, bytes2.Length - index2);
            }

            if (bytes1.Length - index1 < length || bytes2.Length - index2 < length)
                return false;

            for (int i = 0; i < length; i++)
            {
                if (i >= bytes1.Length || i >= bytes2.Length)
                    return false;
                if (bytes1[i + index1] != bytes2[i + index2])
                    return false;
            }

            return true;
        }

        public static void ShowAllColumns(ListView lv, ColumnHeaderList colWidths, ContextMenuStrip cmColumns)
        {
            lv.BeginUpdate();
            for (int i = 0; i < lv.Columns.Count; i++)
            {
                Global.ShowColumn(lv, colWidths, i);
                lv.Columns[i].DisplayIndex = i;
                ((ToolStripMenuItem)cmColumns.Items[i]).Checked = true;
            }
            Global.SizeHeadersAndContent(lv);
            lv.EndUpdate();
        }

        public static void RestoreColumnOrder(ListView lv, ColumnHeaderList colWidths, ContextMenuStrip cmColumns)
        {
            if (colWidths == null)
                return;

            // Restore column order and hidden (0 width) or not, but not the full size
            for (int i = 0; i < lv.Columns.Count; i++)
            {
                if (colWidths.Widths.Count > i)
                {
                    if (colWidths.Widths[i] == 0)
                        lv.Columns[i].Width = 0;
                    if (colWidths.Order.Count > i && colWidths.Order[i] < lv.Columns.Count)
                        lv.Columns[i].DisplayIndex = colWidths.Order[i];
                    ((ToolStripMenuItem)cmColumns.Items[i]).Checked = (colWidths.Widths[i] != 0);
                }
                else
                    colWidths.Widths.Add(lv.Columns[i].Width);
            }
        }

        public static bool ToggleColumnVisible(ListView lv, ColumnHeaderList colWidths, int iCol)
        {
            if (lv == null || colWidths == null)
                return false;

            if (lv.Columns[iCol].Width == 0)
            {
                return ShowColumn(lv, colWidths, iCol);
            }
            else
            {
                colWidths.Widths[iCol] = lv.Columns[iCol].Width;
                lv.Columns[iCol].Width = 0;
                return false;
            }
        }

        public static bool ShowColumn(ListView lv, ColumnHeaderList colWidths, int iCol)
        {
            if (lv == null || colWidths == null)
                return false;

            if (colWidths.Widths[iCol] == 0) // Can happen if hidden columns were saved
            {
                lv.Columns[iCol].AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
                colWidths.Widths[iCol] = lv.Columns[iCol].Width;
            }
            else
                lv.Columns[iCol].Width = colWidths.Widths[iCol];
            return true;
        }

        public static int IndexOfBlockStyle(HatchStyle style)
        {
            switch (style)
            {
                case HatchStyle.Percent25: return 3;
                case HatchStyle.Percent50: return 2;
                case HatchStyle.Percent75: return 1;
                default: return 0;
            }
        }

        public static int CompareSheetIndex(MapSheet a, MapSheet b)
        {
            return Math.Sign(a.SortIndex - b.SortIndex);
        }

        public static ImageAttributes ReplaceColor(Color colorOld, Color colorNew)
        {
            ImageAttributes ia = new ImageAttributes();
            ColorMap map = new ColorMap();
            map.OldColor = colorOld;
            map.NewColor = colorNew;
            ia.SetRemapTable(new ColorMap[] { map });
            return ia;
        }

        public static ImageAttributes GetOpacityAttributes(int iOpacityPercent)
        {
            ImageAttributes imageAttributes;
            float[][] matrixItems ={
                                new float[] {1, 0, 0, 0, 0},
                                new float[] {0, 1, 0, 0, 0},
                                new float[] {0, 0, 1, 0, 0},
                                new float[] {0, 0, 0, iOpacityPercent / 100.0f, 0},
                                new float[] {0, 0, 0, 0, 1}};
            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);
            imageAttributes = new ImageAttributes();
            imageAttributes.SetColorMatrix(
                colorMatrix,
                ColorMatrixFlag.Default,
                ColorAdjustType.Bitmap);
            return imageAttributes;
        }

        public static DashStyle StyleFromIndex(int index)
        {
            switch (index)
            {
                case 2: return DashStyle.Dash;
                case 1: return DashStyle.Dot;
                default: return DashStyle.Solid;
            }
        }

        public static HatchStyle BlockStyleFromIndex(int index)
        {
            switch (index)
            {
                case 3: return HatchStyle.Percent25;
                case 2: return HatchStyle.Percent50;
                case 1: return HatchStyle.Percent75;
                default: return HatchStyle.Percent90;   // Translated to 100% later
            }
        }

        public static string[] KeyNames =
        {
            "None", "LButton", "RButton", "Cancel", "MButton", "XButton1", "XButton2", "Bell", "Back", "Tab", "LineFeed", "K11", "Clear", "Enter", "K14", "K15",
            "Shift", "Ctrl", "Alt", "Pause", "CapsLock", "KanaMode", "K22", "JunjaMode", "FinalMode", "HanjaMode", "K26", "Escape", "IMEConvert", "IMENonconvert", "IMEAceept", "IMEModeChange",
            "Space", "PageUp", "PageDown", "End", "Home", "Left", "Up", "Right", "Down", "Select", "Print", "Execute", "PrintScr", "Insert", "Delete", "Help",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "K58", "K59", "K60", "K61", "K62", "K63",
            "K64", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O",
            "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "LWin", "RWin", "Apps", "K94", "Sleep",
            "Num0", "Num1", "Num2", "Num3", "Num4", "Num5", "Num6", "Num7", "Num8", "Num9", "Multiply", "Add", "Separator", "Subtract", "Decimal", "Divide",
            "F1", "F2", "F3", "F4", "F5", "F6", "F7", "F8", "F9", "F10", "F11", "F12", "F13", "F14", "F15", "F16",
            "F17", "F18", "F19", "F20", "F21", "F22", "F23", "F24", "K136", "K137", "K138", "K139", "K140", "K141", "K142", "K143",
            "NumLock", "Scroll", "K146", "K147", "K148", "K149", "K150", "K151", "K152", "K153", "K154", "K155", "K156", "K157", "K158", "K159",
            "LShift", "RShift", "LCtrl", "RCtrl", "LAlt", "RAlt", "Back", "Forward", "Refresh", "Stop", "Search", "Favorites", "Home", "Mute", "Down", "Up",
            "NextTrack", "PreviousTrack", "Stop", "PlayPause", "LaunchMail", "SelectMedia", "LaunchApp1", "LaunchApp2", "K184", "K185", ";", "=", ",", "-", ".",
            "/", "`", "K193", "K194", "K195", "K196", "K197", "K198", "K199", "K200", "K201", "K202", "K203", "K204", "K205", "K206",
            "K207", "K208", "K209", "K210", "K211", "K212", "K213", "K214", "K215", "K216", "K217", "K218", "[", "\\", "]", "'",
            "Oem8", "K224", "K225", "\\", "K227", "K228", "ProcessKey", "K230", "Packet", "K232", "K233", "K234", "K235", "K236", "K237", "K238",
            "K239", "K240", "K241", "K242", "K243", "K244", "K245", "Attn", "Crsel", "Exsel", "EraseEof", "Play", "Zoom", "NoName", "Pa1", "OemClear", "K255"
        };

        public static int[] KeyPrintOrder =
        {
            17, 162, 163, 18, 164, 165, 16, 160, 161, 91, 92, 0, 1, 2, 3, 4,
            5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 19, 20, 21, 22, 23,
            24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39,
            40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55,
            56, 57, 58, 59, 60, 61, 62, 63, 64, 65, 66, 67, 68, 69, 70, 71,
            72, 73, 74, 75, 76, 77, 78, 79, 80, 81, 82, 83, 84, 85, 86, 87,
            88, 89, 90, 93, 94, 95, 96, 97, 98, 99, 100, 101, 102, 103, 104, 105,
            106, 107, 108, 109, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121,
            122, 123, 124, 125, 126, 127, 128, 129, 130, 131, 132, 133, 134, 135, 136, 137,
            138, 139, 140, 141, 142, 143, 144, 145, 146, 147, 148, 149, 150, 151, 152, 153,
            154, 155, 156, 157, 158, 159, 166, 167, 168, 169, 170, 171, 172, 173, 174, 175,
            176, 177, 178, 179, 180, 181, 182, 183, 184, 185, 186, 187, 188, 189, 190, 191,
            192, 193, 194, 195, 196, 197, 198, 199, 200, 201, 202, 203, 204, 205, 206, 207,
            208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 220, 221, 222, 223,
            224, 225, 226, 227, 228, 229, 230, 231, 232, 233, 234, 235, 236, 237, 238, 239,
            240, 241, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255
        };

        public static Keys[] AllKeys
        {
            get
            {
                Keys[] keys = new Keys[256];
                for (int i = 0; i < 256; i++)
                    keys[i] = (Keys)i;
                return keys;
            }
        }

        public static string WheelActionString(WheelAction action)
        {
            switch (action)
            {
                case WheelAction.None: return "No Action";
                case WheelAction.Zoom: return "Zoom in or out";
                case WheelAction.FixedZoom: return "Zoom in or out (50%, 100%, 150%, 200%, 300%)";
                case WheelAction.Icon: return "Select the next or previous icon";
                case WheelAction.IconOrientation: return "Rotate the currently selected icon";
                case WheelAction.BlockStyle: return "Change the block pattern (25%, 50%, 75%, 100%)";
                case WheelAction.LineStyle: return "Change the line pattern (Solid, Dot, Dash)";
                case WheelAction.BlockIndex: return "Change the selected custom block style";
                case WheelAction.LineIndex: return "Change the selected custom line style";
                case WheelAction.Sheet: return "Select the next or previous map sheet";
                case WheelAction.Character: return "Select the next or previous character";
                default: return "Unknown";
            }
        }

        public static string ActionString(Action action)
        {
            switch (action)
            {
                case Action.None: return "No Action";
                case Action.FileNew: return "File: New";
                case Action.FileOpen: return "File: Open";
                case Action.FileSave: return "File: Save";
                case Action.FileSaveAs: return "File: Save As";
                case Action.FileExportPng: return "File: Export as PNG";
                case Action.FileExportZip: return "File: Export as ZIP";
                case Action.FileExit: return "File: Exit";
                case Action.EditUndo: return "Edit: Undo";
                case Action.EditRedo: return "Edit: Redo";
                case Action.EditCut: return "Edit: Cut";
                case Action.EditCopy: return "Edit: Copy";
                case Action.EditPaste: return "Edit: Paste";
                case Action.EditDelete: return "Edit: Delete";
                case Action.EditFind: return "Edit: Find";
                case Action.EditCrop: return "Edit: Crop";
                case Action.EditRotateLeft: return "Edit: Rotate Left 90°";
                case Action.EditRotateRight: return "Edit: Rotate Right 90°";
                case Action.EditRotate180: return "Edit: Rotate 180°";
                case Action.EditFlipHoriz: return "Edit: Flip Horizontally";
                case Action.EditFlipVert: return "Edit: Flip Vertically";
                case Action.EditConvertHalf: return "Edit: Convert half-lines to full";
                case Action.EditFillBlocks: return "Edit: Fill with blocks";
                case Action.EditOutline: return "Edit: Outline";
                case Action.EditLiveSquares: return "Edit: Live squares";
                case Action.ViewOptions: return "View: Options";
                case Action.ViewColors: return "View: Colors";
                case Action.ViewInformation: return "View: Information";
                case Action.ViewToolbar: return "View: Toolbar";
                case Action.ViewNoteTemplates: return "View: Note Templates";
                case Action.ViewTriggers: return "View: Triggers";
                case Action.ViewZOrder: return "View: Z-Order";
                case Action.View100Percent: return "View: 100%";
                case Action.View150Percent: return "View: 150%";
                case Action.View200Percent: return "View: 200%";
                case Action.View300Percent: return "View: 300%";
                case Action.ViewFitWidth: return "View: Fit Width";
                case Action.ViewFitHeight: return "View: Fit Height";
                case Action.ViewFitInPanel: return "View: Fit in Panel";
                case Action.ViewFitWindow: return "View: Fit Window";
                case Action.ViewAutoArrange: return "View: Auto Arrange";
                case Action.ModePlay: return "Mode: Play";
                case Action.ModeBlock: return "Mode: Block";
                case Action.ModeLine: return "Mode: Line";
                case Action.ModeHybrid: return "Mode: Hybrid";
                case Action.ModeNotes: return "Mode: Notes";
                case Action.ModeKeyboard: return "Mode: Keyboard";
                case Action.ModeEdit: return "Mode: Edit";
                case Action.ModeFill: return "Mode: Fill";
                case Action.SheetAdd: return "Sheet: Add";
                case Action.SheetClone: return "Sheet: Clone";
                case Action.SheetRemove: return "Sheet: Remove";
                case Action.SheetExpand: return "Sheet: Expand";
                case Action.SheetPrevious: return "Sheet: Previous";
                case Action.SheetNext: return "Sheet: Next";
                case Action.SheetGoto: return "Sheet: Go to";
                case Action.SheetOrganize: return "Sheet: Organize";
                case Action.SheetLabels: return "Sheet: Labels";
                case Action.SheetClearVisitedSquares: return "Sheet: Clear Visited Squares";
                case Action.GameViewParty: return "Game: View Party";
                case Action.GameShowSpells: return "Game: Show Full Spell List";
                case Action.GameShowMonsters: return "Game: Show Full Monster List";
                case Action.GameShowItems: return "Game: Show Full Item List";
                case Action.GameShowGameInformation: return "Game: Show Game Information";
                case Action.GameShowQuests: return "Game: Show Quests";
                case Action.GameShowShopInventories: return "Game: Show Shop Inventories";
                case Action.GameShowScripts: return "Game: Show Scripts";
                case Action.GameShowQuickReference: return "Game: Show Quick Reference";
                case Action.GameShowEncountersWhenInCombat: return "Game: Show Encounters When In Combat";
                case Action.GameEditRoster: return "Game: Edit Roster";
                case Action.GameCharacterCreationAssistant: return "Game: Character Creation Assistant";
                case Action.GameTrainingAssistant: return "Game: Training Assistant";
                case Action.GameReAcquireGameProcess: return "Game: Re-acquire Game Process";
                case Action.GameRemoveAllMonstersFromMap: return "Game: Remove All Monsters From Map";
                case Action.GameResetMonstersOnMap: return "Game: Reset Monsters On Map";
                case Action.GameEditInGameCartographyData: return "Game: Edit In-game Cartography Data";
                case Action.HelpRunWizard: return "Help: Run the setup wizard";
                case Action.DecreaseSquareSize: return "Zoom Out";
                case Action.IncreaseSquareSize: return "Zoom In";
                case Action.DecreaseSquareSizeFixed: return "Zoom Out (fixed increments)";
                case Action.IncreaseSquareSizeFixed: return "Zoom In (fixed increments)";
                case Action.ScrollSheet: return "Move Sheet";
                case Action.DrawScroll: return "Draw or Scroll depending on mode";
                case Action.Draw: return "Draw";
                case Action.MoveCursorNW: return "Move Cursor NW";
                case Action.MoveCursorN: return "Move Cursor N";
                case Action.MoveCursorNE: return "Move Cursor NE";
                case Action.MoveCursorW: return "Move Cursor W";
                case Action.MoveCursorE: return "Move Cursor E";
                case Action.MoveCursorSW: return "Move Cursor SW";
                case Action.MoveCursorS: return "Move Cursor S";
                case Action.MoveCursorSE: return "Move Cursor SE";
                case Action.ToggleLineNW: return "Toggle Line NW";
                case Action.ToggleLineN: return "Toggle Line N";
                case Action.ToggleLineNE: return "Toggle Line NE";
                case Action.ToggleLineW: return "Toggle Line W";
                case Action.ToggleLineE: return "Toggle Line E";
                case Action.ToggleLineSW: return "Toggle Line SW";
                case Action.ToggleLineS: return "Toggle Line S";
                case Action.ToggleLineSE: return "Toggle Line SE";
                case Action.ToggleLineAll: return "Toggle Line All";
                case Action.ToggleDoubleLineNW: return "Toggle Double Line NW";
                case Action.ToggleDoubleLineN: return "Toggle Double Line N";
                case Action.ToggleDoubleLineNE: return "Toggle Double Line NE";
                case Action.ToggleDoubleLineW: return "Toggle Double Line W";
                case Action.ToggleDoubleLineE: return "Toggle Double Line E";
                case Action.ToggleDoubleLineSW: return "Toggle Double Line SW";
                case Action.ToggleDoubleLineS: return "Toggle Double Line S";
                case Action.ToggleDoubleLineSE: return "Toggle Double Line SE";
                case Action.ToggleDoubleLineAll: return "Toggle Double Line All";
                case Action.ToggleBackground: return "Toggle Background";
                case Action.ExpandSheetNW: return "Expand Sheet NW";
                case Action.ExpandSheetN: return "Expand Sheet N";
                case Action.ExpandSheetNE: return "Expand Sheet NE";
                case Action.ExpandSheetW: return "Expand Sheet W";
                case Action.ExpandSheetE: return "Expand Sheet E";
                case Action.ExpandSheetSW: return "Expand Sheet SW";
                case Action.ExpandSheetS: return "Expand Sheet S";
                case Action.ExpandSheetSE: return "Expand Sheet SE";
                case Action.ExpandSheetAll: return "Expand Sheet All";
                case Action.MoveBitmapNW: return "Move background image crop area up-left";
                case Action.MoveBitmapN: return "Move background image crop area up";
                case Action.MoveBitmapNE: return "Move background image crop area up-right";
                case Action.MoveBitmapW: return "Move background image crop area left";
                case Action.MoveBitmapE: return "Move background image crop area right";
                case Action.MoveBitmapSW: return "Move background image crop area down-left";
                case Action.MoveBitmapS: return "Move background image crop area down";
                case Action.MoveBitmapSE: return "Move background image crop area down-right";
                case Action.MoveBitmap10NW: return "Move background image crop area up-left 10";
                case Action.MoveBitmap10N: return "Move background image crop area up 10";
                case Action.MoveBitmap10NE: return "Move background image crop area up-right 10";
                case Action.MoveBitmap10W: return "Move background image crop area left 10";
                case Action.MoveBitmap10E: return "Move background image crop area right 10";
                case Action.MoveBitmap10SW: return "Move background image crop area down-left 10";
                case Action.MoveBitmap10S: return "Move background image crop area down 10";
                case Action.MoveBitmap10SE: return "Move background image crop area down-right 10";
                case Action.QuickDoor: return "Cycle through the door icons";
                case Action.LoadRecent1: return "Load Recent Map 1";
                case Action.LoadRecent2: return "Load Recent Map 2";
                case Action.LoadRecent3: return "Load Recent Map 3";
                case Action.LoadRecent4: return "Load Recent Map 4";
                case Action.CureAllSilent: return "Cure All (current char)";
                case Action.CureAll1: return "Cure All with Char 1";
                case Action.CureAll2: return "Cure All with Char 2";
                case Action.CureAll3: return "Cure All with Char 3";
                case Action.CureAll4: return "Cure All with Char 4";
                case Action.CureAll5: return "Cure All with Char 5";
                case Action.CureAll6: return "Cure All with Char 6";
                case Action.CureAll7: return "Cure All with Char 7";
                case Action.CureAll8: return "Cure All with Char 8";
                case Action.ShowLegend: return "Show/Hide Map Legend";
                case Action.GameLaunchCurrentGame: return "Game: Launch Current Game";
                case Action.ViewBringDOSBoxToForeground: return "View: Bring DOSBox to Foreground";
                case Action.ResetHacker: return "Reset Memory Scanner";
                case Action.CloseEncounterWindow: return "Encounter Window";
                case Action.TradeBackpack1: return "Trade Backpack with Char 1";
                case Action.TradeBackpack2: return "Trade Backpack with Char 2";
                case Action.TradeBackpack3: return "Trade Backpack with Char 3";
                case Action.TradeBackpack4: return "Trade Backpack with Char 4";
                case Action.TradeBackpack5: return "Trade Backpack with Char 5";
                case Action.TradeBackpack6: return "Trade Backpack with Char 6";
                case Action.TradeBackpack7: return "Trade Backpack with Char 7";
                case Action.TradeBackpack8: return "Trade Backpack with Char 8";
                case Action.TeleportToCursor: return "Teleport to Cursor";
                case Action.SpellHotkey1: return "Spell Hotkey 1";
                case Action.SpellHotkey2: return "Spell Hotkey 2";
                case Action.SpellHotkey3: return "Spell Hotkey 3";
                case Action.SpellHotkey4: return "Spell Hotkey 4";
                case Action.SpellHotkey5: return "Spell Hotkey 5";
                case Action.SpellHotkey6: return "Spell Hotkey 6";
                case Action.SpellHotkey7: return "Spell Hotkey 7";
                case Action.SpellHotkey8: return "Spell Hotkey 8";
                case Action.SpellHotkey9: return "Spell Hotkey 9";
                case Action.SpellHotkey10: return "Spell Hotkey 10";
                case Action.SetMapCartography: return "Set In-Game Cartography for Current Map to Visited";
                case Action.ClearMapCartography: return "Set In-Game Cartography for Current Map to Unvisited";
                case Action.CopyLocation: return "Copy Current Location to Clipboard";
                case Action.SelectCharacter1: return "Select character #1 in the party information window";
                case Action.SelectCharacter2: return "Select character #2 in the party information window";
                case Action.SelectCharacter3: return "Select character #3 in the party information window";
                case Action.SelectCharacter4: return "Select character #4 in the party information window";
                case Action.SelectCharacter5: return "Select character #5 in the party information window";
                case Action.SelectCharacter6: return "Select character #6 in the party information window";
                case Action.SelectCharacter7: return "Select character #7 in the party information window";
                case Action.SelectCharacter8: return "Select character #8 in the party information window";
                case Action.MoveDOSBox: return "Move the DOSBox window to the preset location and size";
                case Action.RotateIconCW: return "Rotate the currently selected icon clockwise";
                case Action.RotateIconCCW: return "Rotate the currently selected icon counterclockwise";
                case Action.Refresh: return "Redraw the map display";
                case Action.SelectBlock1: return "Select block color/pattern 1";
                case Action.SelectBlock2: return "Select block color/pattern 2";
                case Action.SelectBlock3: return "Select block color/pattern 3";
                case Action.SelectBlock4: return "Select block color/pattern 4";
                case Action.SelectBlock5: return "Select block color/pattern 5";
                case Action.SelectBlock6: return "Select block color/pattern 6";
                case Action.SelectBlock7: return "Select block color/pattern 7";
                case Action.SelectBlock8: return "Select block color/pattern 8";
                case Action.SelectBlock9: return "Select block color/pattern 9";
                case Action.SelectBlock10: return "Select block color/pattern 10";
                case Action.SelectLine1: return "Select line color/pattern 1";
                case Action.SelectLine2: return "Select line color/pattern 2";
                case Action.SelectLine3: return "Select line color/pattern 3";
                case Action.SelectLine4: return "Select line color/pattern 4";
                case Action.SelectLine5: return "Select line color/pattern 5";
                case Action.SelectLine6: return "Select line color/pattern 6";
                case Action.SelectLine7: return "Select line color/pattern 7";
                case Action.SelectLine8: return "Select line color/pattern 8";
                case Action.SelectLine9: return "Select line color/pattern 9";
                case Action.SelectLine10: return "Select line color/pattern 10";
                case Action.NoteTemplate1: return "Use note template 1";
                case Action.NoteTemplate2: return "Use note template 2";
                case Action.NoteTemplate3: return "Use note template 3";
                case Action.NoteTemplate4: return "Use note template 4";
                case Action.BlockColorDialog: return "Change the current block style";
                case Action.LineColorDialog: return "Change the current line style";
                case Action.PrevBlockStyle: return "Select the previous common block style";
                case Action.NextBlockStyle: return "Select the next common block style";
                case Action.PrevLineStyle: return "Select the previous common line style";
                case Action.NextLineStyle: return "Select the next common line style";
                case Action.NextIcon: return "Select the next icon";
                case Action.PrevIcon: return "Select the previous icon";
                case Action.PrevCharacter: return "Select the previous character in the party window";
                case Action.NextCharacter: return "Select the next character in the party window";
                case Action.SkipIntroductions: return "Skip the game introductions and start the default party";
                case Action.AutoCombat: return "Attack/Shoot/Defend the first monster with the current character";
                case Action.DisarmTrap: return "Attempt to disarm a trap (as via the Treasure dialog)";
                case Action.CopyMapText: return "Copy the map text from memory to the clipboard";
                case Action.OptionsMaps: return "Open the \"Maps\" tab of the Options dialog";
                case Action.OptionsMisc: return "Open the \"Misc\" tab of the Options dialog";
                case Action.OptionsKeyboard: return "Open the \"Keyboard\" tab of the Options dialog";
                case Action.OptionsMouse: return "Open the \"Mouse\" tab of the Options dialog";
                case Action.OptionsPlay: return "Open the \"Play\" tab of the Options dialog";
                case Action.OptionsWindows: return "Open the \"Windows\" tab of the Options dialog";
                case Action.OptionsSpells: return "Open the \"Spells\" tab of the Options dialog";
                case Action.OptionsEncounter: return "Open the \"Encounter\" tab of the Options dialog";
                case Action.OptionsQuests: return "Open the \"Quests\" tab of the Options dialog";
                case Action.OptionsDOSBox: return "Open the \"DOSBox\" tab of the Options dialog";
                case Action.WizardMinimal: return "Switch to the \"Minimal\" wizard play style";
                case Action.WizardFaq: return "Switch to the \"FAQ-Style\" wizard play style";
                case Action.WizardFull: return "Switch to the \"Full Information\" wizard play style";
                case Action.PrevBlockIndex: return "Select the previous custom block style";
                case Action.NextBlockIndex: return "Select the next custom block style";
                case Action.PrevLineIndex: return "Select the previous custom line style";
                case Action.NextLineIndex: return "Select the next custom line style";
                case Action.ToggleLastBlock: return "Switch between the most recent block styles";
                case Action.ToggleLastLine: return "Switch between the most recent line styles";
                case Action.ToggleMapScrollbars: return "Toggle showing scrollbars in Play mode";
                case Action.ToggleHideSquares: return "Toggle hiding unvisited squares";
                case Action.ToggleRevealSeenSquares: return "Toggle hiding seen but unvisited squares";
                case Action.ToggleShowNotesUnvisited: return "Toggle showing notes on unvisited squares";
                case Action.ToggleRevealEdgeSquares: return "Toggle hiding edge squares";
                case Action.ToggleRevealInaccessible: return "Toggle hiding of inaccessible squares";
                case Action.ToggleUseInGameCartography: return "Toggle using of in-game cartography";
                case Action.ToggleHideUnvisitedDots: return "Toggle hiding of non-solid lines opposite unvisited squares";
                case Action.ToggleShowActiveScripts: return "Toggle showing of active scripts";
                case Action.ToggleShowActiveEncountersOnly: return "Toggle showing active encounter scripts only";
                case Action.ToggleShowGridLines: return "Toggle showing grid lines";
                case Action.ToggleShowLabels: return "Toggle showing labels on maps";
                case Action.ToggleReadOnlyMaps: return "Toggle read-only maps";
                case Action.ToggleReadOnlyNotes: return "Toggle read-only notes";
                case Action.CycleYouAreHereOpacity: return "Cycle opacity of \"You are Here\" icon by 10%";
                case Action.CycleMonsterIconOpacity: return "Cycle opacity of monster icons by 10%";
                case Action.CycleTreasureWindowOpacity: return "Cycle opacity of treasure window by 10%";
                case Action.CycleUnvisitedSquareOpacity: return "Cycle opacity of unvisited squares by 10%";
                case Action.CycleSeenSquareOpacity: return "Cycle opacity of seen but unvisited squares by 10%";
                case Action.CycleItemFormats: return "Cycle through the default item description format strings";
                case Action.CycleWizardModes: return "Cycle through the Wizard modes: Minimal, FAQ-Style, Full Visibility";
                case Action.ToggleKeyboardHook: return "Toggle the keyboard hook for global shortcuts";
                case Action.ToggleMemoryWrite: return "Toggle allowing writing to game memory";
                case Action.ToggleUpdateInGameCartography: return "Toggle updating of in-game cartography";
                case Action.ToggleRestoreHPWithCureall: return "Toggle restoring of hit points with \"Cure-All\"";
                case Action.ToggleCheat: return "Toggle allowing cheats";
                case Action.ToggleAutoCharacterSwitch: return "Toggle automatic switching of character tabs";
                case Action.ToggleAutoMapSwitch: return "Toggle automatic switching of maps";
                case Action.ToggleAutoShowNotes: return "Toggle automatic showing of notes at party location";
                case Action.ToggleShowSpellsWhenCasting: return "Toggle showing spell list while casting";
                case Action.ToggleForceDOSBoxLocation: return "Toggle forcing DOSBox to a specific location";
                case Action.ToggleForceDOSBoxSize: return "Toggle forcing DOSBox to a specific size";
                case Action.ToggleWindowSnap: return "Toggle window snapping";
                case Action.ToggleShowEncountersInCombat: return "Toggle showing encounter window during combat";
                case Action.ToggleShowTreasureWindow: return "Toggle showing treasure window when available";
                case Action.ToggleShowDeadMonsters: return "Toggle showing dead monsters on the encounter window";
                case Action.ToggleShowMonstersOnMap: return "Toggle showing monster icons on map";
                case Action.ToggleShowOnlyNearbyMonsters: return "Toggle showing only nearby monsters";
                case Action.ToggleShowActiveMonsterIcon: return "Toggle diplay of icons on active monsters";
                case Action.ToggleShowUnexploredMonsters: return "Toggle showing monsters in unexplored areas";
                case Action.ToggleHideScriptMonsters: return "Toggle hiding monsters that are serving as script bits";
                case Action.ToggleNearbyAndFlaggedBold: return "Toggle showing quest items on the current map in bold";
                case Action.ToggleQuestGiver: return "Toggle showing quest giver in the quest tree";
                case Action.ToggleQuestRewards: return "Toggle showing quest rewards in the quest tree";
                case Action.ToggleHideInvalidQuests: return "Toggle hiding of invalid quests";
                case Action.ToggleEditCopyBackgrounds: return "Toggle whether moving/copying a selection includes backgrounds";
                case Action.ToggleEditCopyInnerLines: return "Toggle whether moving/copying a selection includes inner lines";
                case Action.ToggleEditCopyOuterLines: return "Toggle whether moving/copying a selection includes outer lines";
                case Action.ToggleEditCopyIcons: return "Toggle whether moving/copying a selection includes icons";
                case Action.ToggleEditCopyNotes: return "Toggle whether moving/copying a selection includes notes";
            }
            return "Unknown";
        }

        public static string KeyString(Keys[] keys)
        {
            StringBuilder sb = new StringBuilder();
            string strPlus = "";
            for (int i = 0; i < 255; i++)
            {
                foreach (Keys key in keys)
                {
                    if ((int)key == KeyPrintOrder[i])
                    {
                        sb.AppendFormat("{0}{1}", strPlus, KeyNames[(int)key]);
                        strPlus = "+";
                    }
                }
            }
            return sb.ToString();
        }

        public static string InputString(IEnumerable<NativeMethods.INPUT> inputs)
        {
            StringBuilder sb = new StringBuilder();
            foreach (NativeMethods.INPUT input in inputs)
            {
                sb.AppendFormat("{0:X4}{1},", input.U.ki.wVk, (input.U.ki.dwFlags & NativeMethods.KEYEVENTF_KEYUP) > 0 ? "u" : "d");
            }
            Trim(sb);
            return sb.ToString();
        }

        public static void FixRange(ref int i, int iMin, int iMax)
        {
            if (i < iMin)
                i = iMin;
            else if (i > iMax)
                i = iMax;
        }

        public static void FixRange(ref byte b, byte bMin, byte bMax)
        {
            if (b < bMin)
                b = bMin;
            else if (b > bMax)
                b = bMax;
        }

        public static void FixRange(ref double val, double min, double max)
        {
            if (val < min)
                val = min;
            else if (val > max)
                val = max;
        }

        public static void FixRange(ref Rectangle rc, int iMinLeft, int iMinTop, int iMaxRight, int iMaxBottom)
        {
            if (rc.X < iMinLeft)
            {
                rc.Width = rc.Width - (iMinLeft - rc.X);
                rc.X = iMinLeft;
            }
            if (rc.Y < iMinTop)
            {
                rc.Height = rc.Height - (iMinTop - rc.Y);
                rc.Y = iMinTop;
            }
            if (rc.Width + rc.X >= iMaxRight)
                rc.Width = iMaxRight - rc.X;
            if (rc.Height + rc.Y >= iMaxBottom)
                rc.Height = iMaxBottom - rc.Y;
        }

        public static void FixRange(ref Size sz, int iMinH, int iMaxH, int iMinV, int iMaxV)
        {
            if (sz.Width < iMinH)
                sz.Width = iMinH;
            else if (sz.Width > iMaxH)
                sz.Width = iMaxH;
            if (sz.Height < iMinV)
                sz.Height = iMinV;
            else if (sz.Height > iMaxV)
                sz.Height = iMaxV;
        }

        public static void FixRange(ref Point pt, int iMinH, int iMaxH, int iMinV, int iMaxV)
        {
            if (pt.X < iMinH)
                pt.X = iMinH;
            else if (pt.X > iMaxH)
                pt.X = iMaxH;
            if (pt.Y < iMinV)
                pt.Y = iMinV;
            else if (pt.Y > iMaxV)
                pt.Y = iMaxV;
        }

        public static string GenericColorName(Color c)
        {
            if (c.R > 240 && c.G > 240 && c.B > 240)
                return "White";
            if (c.R < 20 && c.G < 20 && c.B < 20)
                return "Black";
            if (c.R > (c.G + c.B + 64))
                return "Red";
            if (c.G > (c.R + c.B + 64))
                return "Green";
            if (c.B > (c.R + c.G + 64))
                return "Blue";
            return "Grey";
        }

        public static bool CenterControl(Control ctrl)
        {
            if (ctrl == null)
                return false;

            if (ctrl.Parent == null)
                return false;

            int iX = ctrl.Location.X;
            int iY = ctrl.Location.Y;

            if (ctrl.Width < ctrl.Parent.Width)
                iX = (ctrl.Parent.Width - ctrl.Width) / 2;
            else if (iX > 0)
                iX = 0;
            if (ctrl.Height < ctrl.Parent.Height)
                iY = (ctrl.Parent.Height - ctrl.Height) / 2;
            else if (iY > 0)
                iY = 0;

            Point pt = new Point(iX, iY);
            if (ctrl.Location != pt)
            {
                ctrl.Location = new Point(iX, iY);
                return true;
            }
            return false;
        }

        public static Point NullPoint = new Point(-32767, -32767);
        public static PointF NullPointF = new PointF(-32767f, -32767f);
        public static Point NullPoint32000 = new Point(-32000, -32000);

        public static byte[] CompressBytes(byte[] bytes)
        {
            MemoryStream ms = new MemoryStream(bytes.Length);
            GZipStream gzs = new GZipStream(ms, CompressionMode.Compress);
            gzs.Write(bytes, 0, bytes.Length);
            gzs.Close();
            return ms.ToArray();
        }

        public static Rectangle NormalizeRect(Rectangle rc)
        {
            Rectangle rcResult = new Rectangle(rc.Location, rc.Size);
            if (rc.Width < 0)
            {
                rcResult.X = rc.Right;
                rcResult.Width = -rc.Width;
            }

            if (rc.Height < 0)
            {
                rcResult.Y = rc.Bottom;
                rcResult.Height = -rc.Height;
            }

            return rcResult;
        }

        public static Bitmap GetCrop(Image imgSource, Rectangle rcCrop)
        {
            Bitmap bmpOut = new Bitmap(rcCrop.Width, rcCrop.Height);
            using (Graphics g = Graphics.FromImage(bmpOut))
            {
                g.DrawImage(imgSource, new Rectangle(0, 0, rcCrop.Width, rcCrop.Height), rcCrop, GraphicsUnit.Pixel);
            }
            return bmpOut;
        }

        public static byte[] DecompressBytes(byte[] bytes)
        {
            MemoryStream msOut = new MemoryStream(bytes.Length);
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                GZipStream gzs = new GZipStream(ms, CompressionMode.Decompress);
                BinaryReader reader = new BinaryReader(gzs);
                byte[] buffer = new byte[1024];
                int iCount = 0;
                do
                {
                    iCount = gzs.Read(buffer, 0, buffer.Length);
                    if (iCount > 0)
                        msOut.Write(buffer, 0, iCount);
                } while (iCount == 1024);
                return msOut.ToArray();
            }
        }

        public static void InsertText(TextBox tb, string str)
        {
            int iStart = tb.SelectionStart;

            if (tb.SelectionStart == -1)
            {
                tb.Text += str;
                return;
            }

            tb.Text = tb.Text.Substring(0, tb.SelectionStart) + str + tb.Text.Substring(tb.SelectionStart + tb.SelectionLength);
            tb.SelectionStart = iStart + str.Length;
            tb.ScrollToCaret();
        }

        public static int SubItemAtPoint(ListView lv, Point pt, out ListViewItem lvi)
        {
            Rectangle rc = lv.GetItemRect(0, ItemBoundsPortion.Entire);
            lvi = lv.GetItemAt(1, pt.Y);
            int iTotal = rc.Left;
            for (int i = 0; i < lv.Columns.Count; i++)
            {
                iTotal += lv.Columns[i].Width;
                if (pt.X < iTotal)
                    return i;
            }
            return lv.Columns.Count;
        }

        public static Keys[] NormalizeKeys(Keys[] keys)
        {
            List<Keys> keysOut = new List<Keys>();
            foreach (Keys key in keys)
            {
                switch (key)
                {
                    case Keys.LControlKey:
                    case Keys.RControlKey:
                    case Keys.ControlKey:
                        if (!keysOut.Contains(Keys.ControlKey))
                            keysOut.Add(Keys.ControlKey);
                        break;
                    case Keys.LShiftKey:
                    case Keys.RShiftKey:
                    case Keys.ShiftKey:
                        if (!keysOut.Contains(Keys.ShiftKey))
                            keysOut.Add(Keys.ShiftKey);
                        break;
                    case Keys.LMenu:
                    case Keys.RMenu:
                    case Keys.Menu:
                        if (!keysOut.Contains(Keys.Menu))
                            keysOut.Add(Keys.Menu);
                        break;
                    default:
                        keysOut.Add(key);
                        break;
                }
            }
            return keysOut.ToArray();
        }

        public static int CompareQuests(BasicQuest quest1, BasicQuest quest2)
        {
            if (String.IsNullOrWhiteSpace(quest1.Path) && !String.IsNullOrWhiteSpace(quest2.Path))
                return 1;
            if (!String.IsNullOrWhiteSpace(quest1.Path) && String.IsNullOrWhiteSpace(quest2.Path))
                return -1;
            if (quest1.Path == quest2.Path)
            {
                if (quest1.SortOrder != quest2.SortOrder)
                    return quest1.SortOrder - quest2.SortOrder;
                return String.Compare(quest1.Primary.Description, quest2.Primary.Description);
            }
            return String.Compare(quest1.Path, quest2.Path);
        }

        public static void SetNud(NumericUpDown nud, int value)
        {
            if (value < nud.Minimum)
                nud.Value = nud.Minimum;
            else if (value > nud.Maximum)
                nud.Value = nud.Maximum;
            else
                nud.Value = value;
        }

        public static void SetNud(NumericUpDown nud, float value)
        {
            if (value < (float)nud.Minimum)
                nud.Value = nud.Minimum;
            else if (value > (float)nud.Maximum)
                nud.Value = nud.Maximum;
            else
                nud.Value = (decimal)value;
        }

        public static void SetIndex(ComboBox cb, int value)
        {
            if (value < 0 || value >= cb.Items.Count)
                return;
            cb.SelectedIndex = value;
        }

        public static void SetWindowInfo(Form form, WindowInfo info)
        {
            form.Location = info.NormalSize.Location;
            form.Size = info.NormalSize.Size;
            if (info.Maximized)
                form.WindowState = FormWindowState.Maximized;
            if (!Global.AllowableWindowLocations.Contains(form.Location))
                form.Location = SystemInformation.VirtualScreen.Location;
        }

        public static void MeasureTime(System.Action action)
        {
            MeasureTime("Time", action);
        }

        public static void MeasureTime(string str, System.Action action)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            action.Invoke();
            sw.Stop();
            Global.Log("{0}: {1} ms", str, sw.ElapsedMilliseconds);
        }

        public static bool UpdateCartography
        {
            get
            {
                return Properties.Settings.Default.EnableMemoryWrite &&
                    Properties.Settings.Default.UpdateCartWhenInaccessibleRevealed &&
                    Properties.Settings.Default.UseInGameCartography;
            }
        }

        public static Direction Rotate(Direction dir, bool clockwise)
        {
            switch (dir)
            {
                case Direction.Up: return clockwise ? Direction.Right : Direction.Left;
                case Direction.Right: return clockwise ? Direction.Down : Direction.Up;
                case Direction.Down: return clockwise ? Direction.Left : Direction.Right;
                case Direction.Left: return clockwise ? Direction.Up : Direction.Down;
                case Direction.UpRight: return clockwise ? Direction.DownRight : Direction.UpLeft;
                case Direction.DownRight: return clockwise ? Direction.DownLeft : Direction.UpRight;
                case Direction.DownLeft: return clockwise ? Direction.UpLeft : Direction.DownRight;
                case Direction.UpLeft: return clockwise ? Direction.UpRight : Direction.DownLeft;
                default: return dir;
            }
        }

        public static void Write(Stream stream, params UInt16[] shorts)
        {
            foreach (UInt16 i in shorts)
            {
                byte[] bytes = BitConverter.GetBytes(i);
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public static void Write(Stream stream, params int[] ints)
        {
            foreach (int i in ints)
            {
                byte[] bytes = BitConverter.GetBytes(i);
                stream.Write(bytes, 0, bytes.Length);
            }
        }

        public static void Write(Stream stream, params bool[] bools)
        {
            foreach (bool b in bools)
                stream.WriteByte((byte)(b ? 1 : 0));
        }

        public static MapLineInfo DefaultGridLineInfo
        {
            get
            {
                return new MapLineInfo(
                    Properties.Settings.Default.DefaultGridLines,
                    (DashStyle)Properties.Settings.Default.DefaultGridLineStyle,
                    Properties.Settings.Default.DefaultGridLineWidth);
            }
            set
            {
                Properties.Settings.Default.DefaultGridLines = value.Color;
                Properties.Settings.Default.DefaultGridLineStyle = (int)value.Pattern;
                Properties.Settings.Default.DefaultGridLineWidth = (byte)value.Width;
            }
        }

        public static ColorPattern DefaultGridBackground
        {
            get
            {
                return new ColorPattern(
                    Properties.Settings.Default.DefaultGridBackground,
                    Properties.Settings.Default.DefaultGridBackgroundStyle);
            }
            set
            {
                Properties.Settings.Default.DefaultGridBackground = value.Color;
                Properties.Settings.Default.DefaultGridBackgroundStyle = value.Pattern;
            }
        }

        public static Bitmap GetFillBitmap(ColorPattern cp, Size sz)
        {
            Bitmap bmp = new Bitmap(sz.Width, sz.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                Brush brush = null;
                if (cp.Pattern == HatchStyle.Percent90)
                    brush = new SolidBrush(cp.Color);
                else
                    brush = new HatchBrush(cp.Pattern, cp.Color, cp.BackColor);

                g.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
            }
            return bmp;
        }

        public static Bitmap GetFillBitmap(DrawColor dc, Size sz, bool bDrawBorder = false)
        {
            Bitmap bmp = new Bitmap(sz.Width, sz.Height);
            Graphics g = Graphics.FromImage(bmp);
            Brush brush = new SolidBrush(dc.color);
            if (dc.styleBlock != HatchStyle.Percent90)
                brush = new HatchBrush(dc.styleBlock, dc.color, Color.White);
            g.FillRectangle(brush, 0, 0, bmp.Width, bmp.Height);
            if (bDrawBorder)
            {
                Pen pen = new Pen(Color.Black, 1.0f);
                g.DrawRectangle(pen, 0, 0, sz.Width - 1, sz.Height - 1);
            }
            g.Dispose();
            return bmp;
        }

        public static Bitmap GetLineBitmap(DrawColor dc, Size sz, bool bBorder = false)
        {
            Bitmap bmp = new Bitmap(sz.Width, sz.Height);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            Pen pen = new Pen(dc.color, dc.width);
            pen.DashStyle = dc.style;
            g.DrawLine(pen, 0, sz.Height / 2, sz.Width, sz.Height / 2);
            if (bBorder)
            {
                Pen penBorder = new Pen(Color.Black, 1.0f);
                g.DrawRectangle(penBorder, 0, 0, sz.Width - 1, sz.Height - 1);
            }
            g.Dispose();
            return bmp;
        }

        public static string Version { get { return System.Diagnostics.Process.GetCurrentProcess().MainModule.FileVersionInfo.ProductVersion; } }

        public static Bitmap Overlay(Bitmap bmpSource, Color color, int percent)
        {
            Bitmap bmp = new Bitmap(bmpSource);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImageUnscaled(bmpSource, 0, 0);
            Brush brush = new SolidBrush(Color.FromArgb(Math.Min(percent * 255 / 100, 255), color));
            g.FillRectangle(brush, new Rectangle(0, 0, bmp.Width, bmp.Height));
            g.Dispose();
            return bmp;
        }

        public static Color MaskColor { get { return Color.FromArgb(180, Color.White); } }
        public static Color PreMaskColor { get { return Color.White; } }

        public static int PercentToAlpha(int iPercent) { return Math.Min(255, Math.Max(0, iPercent * 255 / 100)); }
        public static int AlphaToPercent(int iAlpha) { return Math.Min(100, Math.Max(0, iAlpha * 100 / 255)); }

        public static Bitmap Mask(Bitmap bmpSource, Bitmap bmpMask)
        {
            Bitmap bmp = new Bitmap(bmpSource);
            Graphics g = Graphics.FromImage(bmp);
            g.DrawImageUnscaled(bmpSource, 0, 0);
            g.DrawImage(bmpMask,
                new Rectangle(0, 0, bmp.Width, bmp.Height),
                0, 0, bmpMask.Width, bmpMask.Height,
                GraphicsUnit.Pixel, Global.ReplaceColor(Global.PreMaskColor, Global.MaskColor));
            g.Dispose();
            return bmp;
        }

        public static EditFlags EditSettings
        {
            get
            {
                return new EditFlags(
                    Properties.Settings.Default.EditCopyInnerLines,
                    Properties.Settings.Default.EditCopyOuterLines,
                    Properties.Settings.Default.EditCopyBackgrounds,
                    Properties.Settings.Default.EditCopyNotes,
                    Properties.Settings.Default.EditCopyIcons);
            }

            set
            {
                Properties.Settings.Default.EditCopyInnerLines = value.Inner;
                Properties.Settings.Default.EditCopyOuterLines = value.Outer;
                Properties.Settings.Default.EditCopyBackgrounds = value.Back;
                Properties.Settings.Default.EditCopyNotes = value.Notes;
                Properties.Settings.Default.EditCopyIcons = value.Icons;
            }
        }

        public static string DisableWhileOptions(string strForm)
        {
            return String.Format("The {0} is temporarily disabled while the Options window is open.", strForm);
        }

        public static string DisableWhileNoScanner()
        {
            return "The memory scanner is not running.  Please check the selected game in the Options dialog.";
        }

        public static string SanitizeFilename(string str)
        {
            Regex replace = new Regex(string.Format("[{0}]", Regex.Escape(new string(Path.GetInvalidFileNameChars()))));
            return replace.Replace(str, "");
        }

        public static void CenterForm(Form form, Rectangle rcParent)
        {
            Rectangle rc = GetCenterRect(form, rcParent);
            form.Location = rc.Location;
        }

        public static Rectangle GetCenterRect(Form form, Rectangle rcParent)
        {
            return new Rectangle(new Point(
                rcParent.X + (rcParent.Width - form.Size.Width) / 2,
                rcParent.Y + (rcParent.Height - form.Size.Height) / 2),
                form.Size);
        }

        public static Rectangle ScaleRect(Rectangle rc, Size szNew, Size szCurrent)
        {
            double scaleW = szNew.Width / (double)szCurrent.Width;
            double scaleH = szNew.Height / (double)szCurrent.Height;
            return new Rectangle((int)(rc.Left * scaleW + 0.5), (int)(rc.Top * scaleH + 0.5), (int)(rc.Width * scaleW + 0.5), (int)(rc.Height * scaleH + 0.5));
        }

        public static Point ConstrainMove(Point ptNew, Point ptOriginal)
        {
            // Ensures that ptNew and ptOriginal form a horizonal or vertical line
            if (Math.Abs(ptNew.X - ptOriginal.X) > Math.Abs(ptNew.Y - ptOriginal.Y))
                ptNew.Y = ptOriginal.Y;    // Line should be horizontal
            else
                ptNew.X = ptOriginal.X;    // Line should be vertical
            return ptNew;
        }

        public static Comparer<Point> PointComparer = Comparer<Point>.Create((p1, p2) => (p1.Y << 16 + p1.X).CompareTo(p2.Y << 16 + p2.X));

        public static string ExePath { get { return Path.GetDirectoryName(System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName); } }

        public static Point[] PointsAround(Point pt)
        {
            return new Point[] { new Point(pt.X, pt.Y - 1), new Point(pt.X, pt.Y + 1), new Point(pt.X - 1, pt.Y), new Point(pt.X + 1, pt.Y) };
        }

        public static bool IsValidShortcut(Keys keys)
        {
            if (keys == Keys.None)
                return false;

            if (keys >= Keys.D0 && keys <= Keys.D9)
                return false;

            return true;
        }

        public static bool IsNear(Point pt1, Point pt2, int iMax = 1)
        {
            return Math.Abs(pt1.X - pt2.X) <= iMax && Math.Abs(pt1.Y - pt2.Y) <= iMax;
        }

        public static Rectangle[] OffsetRects(Rectangle[] rects, int x, int y)
        {
            if (rects == null)
                return null;
            Rectangle[] rcNew = new Rectangle[rects.Length];
            for (int i = 0; i < rects.Length; i++)
                rcNew[i] = new Rectangle(rects[i].X + x, rects[i].Y + y, rects[i].Width, rects[i].Height);
            return rcNew;
        }

        public static bool PointInRects(Rectangle[] rects, int x, int y)
        {
            if (rects == null)
                return false;

            Point pt = new Point(x, y);
            return (rects.Any(r => r.Contains(pt)));
        }

        public static bool PointInRects(Point pt, params Rectangle[] rects)
        {
            if (rects == null)
                return false;

            return (rects.Any(r => r.Contains(pt)));
        }

        public static bool PointInRects(Point pt, params int[] rects)
        {
            if (rects == null || rects.Length % 4 != 0)
                return false;

            for (int i = 0; i < rects.Length / 4; i++)
            {
                if (new Rectangle(rects[i * 4], rects[i * 4 + 1], rects[i * 4 + 2], rects[i * 4 + 3]).Contains(pt))
                    return true;
            }
            return false;
        }

        public static char MatchingBrace(char c)
        {
            switch (c)
            {
                case '(': return ')';
                case '[': return ']';
                case '{': return '}';
                case '<': return '>';
                case ')': return '(';
                case ']': return '[';
                case '}': return '{';
                case '>': return '<';
                default: return c;
            }
        }

        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = Rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static bool Compare(List<int> list1, List<int> list2)
        {
            if (list1 == null && list2 == null)
                return true;
            if (list1 == null || list2 == null)
                return false;
            if (list1.Count != list2.Count)
                return false;
            for (int i = 0; i < list1.Count; i++)
                if (list1[i] != list2[i])
                    return false;
            return true;
        }

        public static void SetInt16(byte[] bytes, int offset, int val)
        {
            if (bytes.Length - offset < 2)
                return;
            byte[] bVal = BitConverter.GetBytes((Int16)val);
            Buffer.BlockCopy(bVal, 0, bytes, offset, bVal.Length);
        }

        public static void SetUInt16(byte[] bytes, int offset, int val)
        {
            if (bytes.Length - offset < 2)
                return;
            byte[] bVal = BitConverter.GetBytes((UInt16)val);
            Buffer.BlockCopy(bVal, 0, bytes, offset, bVal.Length);
        }

        public static void SetUInt24(byte[] bytes, int offset, int val)
        {
            if (bytes.Length - offset < 3)
                return;
            byte[] bVal = BitConverter.GetBytes((UInt32)val);
            Buffer.BlockCopy(bVal, 0, bytes, offset, 3);  // assumes little-endian
        }

        public static void SetInt32(byte[] bytes, int offset, long val)
        {
            if (bytes.Length - offset < 4)
                return;
            byte[] bVal = BitConverter.GetBytes((Int32)val);
            Buffer.BlockCopy(bVal, 0, bytes, offset, bVal.Length);
        }

        public static string Symbol(string sOld, string sNew) { return sOld == "" || sOld == sNew ? sNew : "?"; }

        public static StringBuilder Trim(StringBuilder sb)
        {
            if (sb.Length > 1 && sb[sb.Length - 2] == ',')
                sb.Remove(sb.Length - 2, 2);
            else if (sb.Length > 1 && sb[sb.Length - 2] == ';')
                sb.Remove(sb.Length - 2, 2);
            else if (sb.Length > 0 && (sb[sb.Length - 1] == ',' || sb[sb.Length - 1] == '\t'))
                sb.Remove(sb.Length - 1, 1);
            if (sb.Length > 1 && sb[sb.Length - 1] == '\n')
                sb.Remove(sb.Length - 1, 1);
            if (sb.Length > 1 && sb[sb.Length - 1] == '\r')
                sb.Remove(sb.Length - 1, 1);
            return sb;
        }

        public static Point FixWindowLocation(Point pt)
        {
            // Returns a point that is on the virtual screen as near to the requested point as possible
            Point ptOut = pt;
            if (pt.X < SystemInformation.VirtualScreen.X)
                ptOut.X = SystemInformation.VirtualScreen.X;
            if (pt.Y < SystemInformation.VirtualScreen.Y)
                ptOut.Y = SystemInformation.VirtualScreen.Y;
            if (pt.X > SystemInformation.VirtualScreen.Right)
                ptOut.X = SystemInformation.VirtualScreen.Right - 50;
            if (pt.Y > SystemInformation.VirtualScreen.Bottom)
                ptOut.Y = SystemInformation.VirtualScreen.Bottom - 50;
            return ptOut;
        }

        public static void SetPackedBools(bool[,] bools, byte bSource, int iFirst, int iSecondStart, int iSecondCount)
        {
            for (int i = 0; i < iSecondCount; i++)
            {
                if (iFirst < bools.GetLength(0) && iSecondStart + i < bools.GetLength(1) && iFirst >= 0 && iSecondStart >= 0)
                    bools[iFirst, iSecondStart + i] = (((bSource >> i) & 1) != 0);
            }
        }

        public static void SetPackedBools(bool[] bools, byte bSource, int iStart, int iCount)
        {
            for (int i = 0; i < iCount; i++)
                bools[iStart + i] = (((bSource >> i) & 1) != 0);
        }

        public static string EnsureCR(string str)
        {
            StringBuilder sb = new StringBuilder(str);
            int i = 0;
            while (i < sb.Length)
            {
                if (sb[i] == '\n' && (i < 1 || sb[i - 1] != '\r'))
                {
                    sb.Insert(i, '\r');
                    i++;
                }
                i++;
            }
            return sb.ToString();
        }

        public static void FormatSentences(StringBuilder sb)
        {
            // Make everything lower case except characters following a period.
            bool bNextCap = true;
            for (int i = 0; i < sb.Length; i++)
            {
                if (Char.IsLetter(sb[i]))
                {
                    if (bNextCap)
                        sb[i] = Char.ToUpper(sb[i]);
                    else
                        sb[i] = Char.ToLower(sb[i]);

                    bNextCap = false;
                }
                else
                {
                    switch (sb[i])
                    {
                        case '.':
                        case '?':
                        case '!':
                            bNextCap = true;
                            break;
                    }
                }
            }
            sb.Replace(" i ", " I ");
            sb.Replace(" i'", " I'");
            sb.Replace("g.P.", "G.P.");
        }

        public static int NumDifferences(byte[] b1, byte[] b2, int iCount)
        {
            if (iCount > b1.Length)
                iCount = b1.Length;
            if (iCount > b2.Length)
                iCount = b2.Length;

            int iNumDiff = 0;
            for (int i = 0; i < iCount; i++)
            {
                if (b1[i] != b2[i])
                    iNumDiff++;
            }
            return iNumDiff;
        }

        public static string PascalString(byte[] bytes, int offset = 0, int iMax = 256)
        {
            int iLength = Math.Min((int)bytes[0], iMax);
            return Encoding.ASCII.GetString(bytes, offset + 1, iLength);
        }

        public static void DeselectAll(params ListView[] views)
        {
            foreach (ListView lv in views)
            {
                lv.BeginUpdate();
                int[] indices = new int[lv.SelectedIndices.Count];
                lv.SelectedIndices.CopyTo(indices, 0);
                foreach (int i in indices)
                    lv.Items[i].Selected = false;
                lv.EndUpdate();
            }
        }

        public static string CombineRoster(GameNames game)
        {
            return Properties.Settings.Default.RosterPaths.Combine(game, Games.RosterPath(game), Games.RosterFile(game));
        }

        public static string SingleConditionString(BasicConditionFlags condition, GameNames game = GameNames.None)
        {
            switch (condition)
            {
                case BasicConditionFlags.Good: return "Good";
                case BasicConditionFlags.Asleep: return "Asleep";
                case BasicConditionFlags.Blinded: return "Blinded";
                case BasicConditionFlags.Silenced: return "Silenced";
                case BasicConditionFlags.Diseased: return "Diseased";
                case BasicConditionFlags.Poisoned: return "Poisoned";
                case BasicConditionFlags.Paralyzed: return "Paralyzed";
                case BasicConditionFlags.Unconscious: return "Unconscious";
                case BasicConditionFlags.Cursed: return "Cursed";
                case BasicConditionFlags.Stone: return "Stone";
                case BasicConditionFlags.Dead: return "Dead";
                case BasicConditionFlags.Eradicated: return "Eradicated";
                case BasicConditionFlags.EncasedAir: return "Encased in Air";
                case BasicConditionFlags.EncasedWater: return "Encased in Water";
                case BasicConditionFlags.EncasedEarth: return "Encased in Earth";
                case BasicConditionFlags.EncasedFire: return "Encased in Fire";
                case BasicConditionFlags.Mindless: return "Mindless";
                case BasicConditionFlags.Afraid: return "Afraid";
                case BasicConditionFlags.Held: return "Held";
                case BasicConditionFlags.Webbed: return "Webbed";
                case BasicConditionFlags.Hurt: return "Hurt";
                case BasicConditionFlags.Weak: return "Weak";
                case BasicConditionFlags.HeartBroken: return "HeartBroken";
                case BasicConditionFlags.Insane: return Games.IsBardsTale(game) ? "Nuts" : "Insane";
                case BasicConditionFlags.InLove: return "In Love";
                case BasicConditionFlags.Drunk: return "Drunk";
                case BasicConditionFlags.Confused: return Games.IsBardsTale(game) ? "Possessed" : "Confused";
                case BasicConditionFlags.Depressed: return "Depressed";
                case BasicConditionFlags.BrokenItem: return "Broken Item";
                case BasicConditionFlags.CursedItem: return "Cursed Item";
                case BasicConditionFlags.Hypnotized: return "Hypnotized";
                case BasicConditionFlags.Lost: return "Lost";
                case BasicConditionFlags.Old: return "Old";
                default: return "Multiple Conditions";
            }
        }

        public static bool AnyEnabled(ListView lv)
        {
            foreach (ListViewItem lvi in lv.Items)
                if (lvi.ForeColor != SystemColors.GrayText)
                    return true;
            return false;
        }

        public static string States(params int[] states)
        {
            if (states == null)
                return "<null>";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < states.Length; i++)
                sb.AppendFormat("S{0}: {1:X4}, ", i + 1, states[i]);
            return Trim(sb).ToString();
        }

        public static string[] GetFilesNumeric(string strDir, string strWildcard)
        {
            if (!Directory.Exists(strDir))
                return null;

            // Return files in order by number (e.g. "5.TPW" is returned before "10.TPW")
            try
            {
                List<string> files = new List<string>(Directory.GetFiles(strDir, strWildcard));
                files.Sort(new NumericFileComparer());
                return files.ToArray();
            }
            catch (Exception)
            {
                return new string[0];
            }
        }

        public static int Between(int i, int iMin, int iMax) { return i < iMin ? iMin : i > iMax ? iMax : i; }

        public static string UnixToDos(string str)
        {
            if (str.IndexOf('\n') == -1)
                return str;

            StringBuilder sb = new StringBuilder(str);
            int iIndex = 0;
            while (iIndex < sb.Length - 1)
            {
                if (sb[iIndex] != '\r' && sb[iIndex + 1] == '\n')
                {
                    sb.Insert(iIndex + 1, '\r');
                    iIndex++;
                }
                iIndex++;
            }
            return sb.ToString();
        }

        public static long GetLong(string strValue)
        {
            long lVal;
            if (Int64.TryParse(strValue, out lVal))
                return lVal;

            if (!strValue.StartsWith("0x") && !strValue.StartsWith("-0x"))
                return 0;

            int iStart = 2;
            int iMultiply = 1;

            if (strValue[0] == '-')
            {
                iStart = 3;
                iMultiply = -1;
            }

            try
            {
                return Convert.ToInt64(strValue.Substring(iStart), 16) * iMultiply;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static int NumRightZeros(int val)
        {
            int iCount = 0;
            while ((val & 1) == 0)
            {
                val >>= 1;
                iCount++;
            }
            return iCount;
        }

        public static int[] UInt16Array(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 2)
                return new int[0];

            int[] array = new int[bytes.Length / 2];
            for (int i = 0; i < bytes.Length - 1; i += 2)
                array[i / 2] = BitConverter.ToUInt16(bytes, i);

            return array;
        }

        public static string GetText(ListViewItem lvi)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem.ListViewSubItem si in lvi.SubItems)
                sb.AppendFormat("{0}\t", si.Text);
            if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public static string GetText(ListView lv, bool bSelected)
        {
            StringBuilder sb = new StringBuilder();
            if (bSelected)
            {
                foreach (ListViewItem lvi in lv.SelectedItems)
                    sb.AppendLine(GetText(lvi));
            }
            else
            {
                foreach (ListViewItem lvi in lv.Items)
                    sb.AppendLine(GetText(lvi));
            }
            return sb.ToString();
        }

        public static void InsertColumnWidths(ListView lv, params int[] shiftIn)
        {
            for (int i = lv.Columns.Count - 1; i > shiftIn.Length; i--)
            {
                lv.Columns[i].Width = lv.Columns[i - shiftIn.Length].Width;
            }
            for (int i = 0; i < shiftIn.Length; i++)
                lv.Columns[i].Width = shiftIn[i];
        }

        public static void RemoveColumnWidths(ListView lv, int count)
        {
            for (int i = 0; i < lv.Columns.Count - count - 1; i++)
                lv.Columns[i].Width = lv.Columns[i + count].Width;
        }

        public static string GetListViewItemText(ListViewItem lvi)
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListViewItem.ListViewSubItem lvsi in lvi.SubItems)
                sb.AppendFormat("{0}\t", lvsi.Text);
            return Trim(sb).ToString();
        }

        public static void CopyListViewItems(ListView lv, bool bSelected = true)
        {
            if (bSelected && lv.SelectedItems.Count < 1)
                bSelected = false;

            StringBuilder sb = new StringBuilder();
            if (bSelected)
            {
                foreach (ListViewItem lvi in lv.SelectedItems)
                    sb.AppendFormat("{0}\r\n", GetListViewItemText(lvi));
            }
            else
            {
                foreach (ListViewItem lvi in lv.Items)
                    sb.AppendFormat("{0}\r\n", GetListViewItemText(lvi));
            }
            Clipboard.SetText(sb.ToString());
        }

        public static int GetIntAttrib(XmlElement e, string strName, int iDefault = 0)
        {
            if (!e.HasAttribute(strName))
                return iDefault;
            string strVal = e.GetAttribute(strName);
            int iValue = iDefault;
            if (Int32.TryParse(strVal, out iValue))
                return iValue;
            return iDefault;
        }

        public static string GetStrAttrib(XmlElement e, string strName, string strDefault = "") { return e.HasAttribute(strName) ? e.GetAttribute(strName) : strDefault; }

        public static void SetListViewSubItemFont(ListView lv, int index, int subindex, FontStyle style)
        {
            if (lv == null || index < 0 || index >= lv.Items.Count || subindex < 0 || subindex >= lv.Items[index].SubItems.Count)
                return;

            lv.Items[index].UseItemStyleForSubItems = false;
            lv.Items[index].SubItems[subindex].Font = new Font(lv.Items[index].Font, style);
        }

        public static void SetListViewSubItemColor(ListView lv, int index, int subindex, Color fore, Color back)
        {
            if (lv != null && index == -1)
            {
                lv.BackColor = back;
                lv.ForeColor = fore;
                return;
            }
            if (lv == null || index < 0 || index >= lv.Items.Count || subindex < 0 || subindex >= lv.Items[index].SubItems.Count)
                return;

            lv.Items[index].UseItemStyleForSubItems = false;
            lv.Items[index].SubItems[subindex].ForeColor = fore;
            lv.Items[index].SubItems[subindex].BackColor = back;
        }

        public static void SetListViewSubItem(ListView lv, int index, int subindex, ColoredUIElements elements)
        {
            if (!Properties.Settings.Default.UIElementOptions.Elements.ContainsKey(elements))
                return;
            UIElementOption option = Properties.Settings.Default.UIElementOptions.Elements[elements];
            if (option == null)
                return;
            if (lv != null && index == -1)
            {
                lv.BackColor = option.BackColor;
                lv.ForeColor = option.ForeColor;
                lv.Font = new Font(lv.Font, FontStyle.Regular);
                return;
            }
            if (lv == null || index < 0 || index >= lv.Items.Count || subindex < 0 || subindex >= lv.Items[index].SubItems.Count)
                return;
            lv.Items[index].UseItemStyleForSubItems = false;
            lv.Items[index].SubItems[subindex].Font = new Font(lv.Items[index].Font, FontStyle.Regular);
            lv.Items[index].SubItems[subindex].ForeColor = option.ForeColor;
            lv.Items[index].SubItems[subindex].BackColor = option.BackColor;
        }

        public static string ArrayString(IEnumerable array)
        {
            StringBuilder sb = new StringBuilder();
            foreach (object o in array)
                sb.AppendFormat("{0}, ", o);
            return Trim(sb).ToString();
        }

        public static Rectangle AllowableWindowLocations
        {
            get
            {
                Rectangle rc = SystemInformation.VirtualScreen;
                if (Properties.Settings.Default.UseExtendedWindowRect)
                    rc.Inflate(16, 16);
                return rc;
            }
        }

        public static void SetColor(ListViewItem lvi, ColoredUIElements element)
        {
            UIElementOptions options = Properties.Settings.Default.UIElementOptions;
            if (options == null || options.Elements == null)
                return;
            if (!options.Elements.ContainsKey(element))
                return;
            UIElementOption opt = options.Elements[element];
            lvi.ForeColor = opt.ForeColor;
            lvi.BackColor = opt.BackColor;
        }

        public static void DeleteSelectedItems(ListView lv, MainForm.VoidDelegate fnWorking)
        {
            if (lv.Items.Count == lv.SelectedItems.Count)
            {
                lv.Items.Clear();
                return;
            }

            lv.BeginUpdate();

            if (lv.SelectedItems.Count * 10 / lv.Items.Count >= 8) // > 80% of items are selected
            {
                // Delete all and put back unselected items; much faster than deleting individually
                ListViewItem[] unselected = new ListViewItem[lv.Items.Count - lv.SelectedItems.Count];
                int iCount = 0;
                foreach (ListViewItem lvi in lv.Items)
                {
                    if (lvi.Selected)
                        continue;
                    unselected[iCount++] = lvi;
                }
                lv.Items.Clear();
                lv.Items.AddRange(unselected);
            }
            else
            {
                foreach (ListViewItem lvi in lv.SelectedItems)
                {
                    lvi.Remove();
                    if (fnWorking != null)
                        fnWorking();
                }
            }
            lv.EndUpdate();
        }

        public static void DeleteUnselectedItems(ListView lv, MainForm.VoidDelegate fnWorking)
        {
            if (lv.Items.Count == 0)
            {
                lv.Items.Clear();
                return;
            }

            lv.BeginUpdate();

            if (lv.SelectedItems.Count * 10 / lv.Items.Count <= 2) // < 20% of items are selected
            {
                // Delete all and put back selected items; much faster than deleting individually
                ListViewItem[] selected = new ListViewItem[lv.SelectedItems.Count];
                for (int i = 0; i < lv.SelectedItems.Count; i++)
                    selected[i] = lv.SelectedItems[i];
                lv.Items.Clear();
                lv.Items.AddRange(selected);
            }
            else
            {
                foreach (ListViewItem lvi in lv.Items)
                {
                    if (!lvi.Selected)
                        lvi.Remove();
                    if (fnWorking != null)
                        fnWorking();
                }
            }
            lv.EndUpdate();
        }

        public static UIElementOption GetUIElement(ColoredUIElements elements)
        {
            if (!Properties.Settings.Default.UIElementOptions.Elements.ContainsKey(elements))
                return UIElementOption.Default(elements);
            return Properties.Settings.Default.UIElementOptions.Elements[elements];
        }
    }

    class NumericFileComparer : IComparer<string>
    {
        public int Compare(string x, string y)
        {
            string str1 = Path.GetFileNameWithoutExtension(x);
            string str2 = Path.GetFileNameWithoutExtension(y);
            int i1 = 0;
            int i2 = 0;
            if (Int32.TryParse(str1, out i1) && Int32.TryParse(str2, out i2))
                return Math.Sign(i1 - i2);
            return String.Compare(str1, str2);
        }
    }

    public class EditFlags
    {
        public bool Inner;
        public bool Outer;
        public bool Back;
        public bool Notes;
        public bool Labels;
        public bool Icons;
        public bool Mask;
        public bool Grid;
        public bool AlwaysUseAlpha;
        public bool HideNorth;
        public bool HideSouth;
        public bool HideEast;
        public bool HideWest;
        public bool HideImmaterial;
        public int OuterDepth;

        public EditFlags(bool inner, bool outer, bool back, bool notes, bool icons)
        {
            Inner = inner;
            Outer = outer;
            Back = back;
            Notes = notes;
            Labels = notes;
            Icons = icons;
            Mask = false;
            Grid = true;
            HideNorth = false;
            HideSouth = false;
            HideWest = false;
            HideEast = false;
            HideImmaterial = false;
            OuterDepth = 0;
            AlwaysUseAlpha = false;
        }

        public static EditFlags All { get { return new EditFlags(true, true, true, true, true); } }

        public bool DrawAll { get { return Inner && Outer && Back && Icons && Notes && Labels; } }
    }

    public class Margins
    {
        public int Top;
        public int Bottom;
        public int Left;
        public int Right;

        public Margins(int top, int bottom, int left, int right)
        {
            Top = top;
            Bottom = bottom;
            Left = left;
            Right = right;
        }
    }

    public struct EdgeRect
    {
        public Rectangle Rect;
        public Direction Dir;

        public int Left { get { return Rect.Left; } }
        public int Top { get { return Rect.Top; } }
        public int Bottom { get { return Rect.Bottom; } }
        public int Right { get { return Rect.Right; } }
        public int Width { get { return Rect.Width; } }
        public int Height { get { return Rect.Height; } }

        public EdgeRect(Rectangle rc, Direction dir)
        {
            Rect = rc;
            Dir = dir;
        }

        public EdgeRect(int x, int y, int width, int height, Direction dir)
        {
            Rect = new Rectangle(x, y, width, height);
            Dir = dir;
        }
    }

    public class SnapWindows
    {
        public Point SnapOffsetPositive;
        public Point SnapOffsetNegative;
        private Rectangle[] m_windowRects;
        private List<EdgeRect> m_edgeRects;
        public int SnapRange;
        public Size MinimumSize;
        public ExpandSizes Margins;
        public Form FormMoving;

        public Rectangle[] WindowRects
        {
            set
            {
                m_windowRects = value;
                m_edgeRects = new List<EdgeRect>(m_windowRects.Length * 4);

                foreach (Rectangle rc in m_windowRects)
                {
                    m_edgeRects.Add(new EdgeRect(rc.Left + Margins.WidthDelta - SnapRange, rc.Top - SnapRange + Margins.HeightDelta, rc.Width - Margins.WidthDelta * 2 + SnapRange * 2, SnapRange * 2, Direction.Up));
                    m_edgeRects.Add(new EdgeRect(rc.Left - SnapRange + Margins.WidthDelta, rc.Top + Margins.HeightDelta - SnapRange, SnapRange * 2, rc.Height - Margins.HeightDelta * 2 + SnapRange * 2, Direction.Left));
                    m_edgeRects.Add(new EdgeRect(rc.Right - SnapRange - Margins.WidthDelta, rc.Top + Margins.HeightDelta - SnapRange, SnapRange * 2, rc.Height - Margins.HeightDelta * 2 + SnapRange * 2, Direction.Right));
                    m_edgeRects.Add(new EdgeRect(rc.Left + Margins.WidthDelta - SnapRange, rc.Bottom - SnapRange - Margins.HeightDelta, rc.Width - Margins.WidthDelta * 2 + SnapRange * 2, SnapRange * 2, Direction.Down));
                }
            }

            get { return m_windowRects; }
        }

        public Rectangle[] MarginWindows(Rectangle[] rects)
        {
            Rectangle[] rcOut = new Rectangle[rects.Length];
            for (int i = 0; i < rects.Length; i++)
                rcOut[i] = new Rectangle(rects[i].Left + Margins.Left, rects[i].Top + Margins.Top, rects[i].Width - Margins.WidthDelta, rects[i].Height - Margins.HeightDelta);
            return rcOut;
        }

        public List<EdgeRect> EdgeRects
        {
            get { return m_edgeRects; }
        }

        public SnapWindows(int snapRange, Form formMoving, Rectangle[] rects, Size rcMinimum, ExpandSizes margins)
        {
            FormMoving = formMoving;
            Margins = margins;
            SnapRange = snapRange;
            m_edgeRects = null;
            WindowRects = rects;    // This also sets the m_edgeRects
            Rectangle rcMoving = formMoving.Bounds;
            if (Properties.Settings.Default.UseExtendedWindowRect)
                rcMoving = NativeMethods.GetExtendedWindowRect(formMoving.Handle);
            SnapOffsetPositive = new Point(Cursor.Position.X - rcMoving.Left, Cursor.Position.Y - rcMoving.Top);
            SnapOffsetNegative = new Point(Cursor.Position.X - rcMoving.Right, Cursor.Position.Y - rcMoving.Bottom);
            MinimumSize = rcMinimum;
        }
    }

    public class DrawCommand : IComparable
    {
        public Pen m_pen;
        public Color BackgroundColor;
        public int x1, y1, x2, y2;
        public Direction Edge;
        public DrawCommand(Pen pen, Color bg, int X1, int Y1, int X2, int Y2)
        {
            BackgroundColor = bg;
            m_pen = pen;
            x1 = X1;
            y1 = Y1;
            x2 = X2;
            y2 = Y2;
        }

        public int Length
        {
            get
            {
                return (int) Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
            }
        }

        public override string ToString()
        {
            if (m_pen == null)
                return "<null pen>";
            return String.Format("{0}, {1}, {2}, {3}", (m_pen.Color.ToArgb() == BackgroundColor.ToArgb()) ? "Background" : m_pen.Color.ToString(), Length, m_pen.Width, Edge);
        }

        public static Pen GetPen(MapSquare square, Direction dir, Color bg, float fScaleBG, float fScaleFG)
        {
            Pen pen = new Pen(Color.Transparent);
            switch (dir)
            {
                case Direction.Down:
                    pen = new Pen(square.Colors.Bottom, square.Lines.BottomWidth);
                    pen.DashStyle = (DashStyle)square.Lines.BottomPattern;
                    break;
                case Direction.Left:
                    pen = new Pen(square.Colors.Left, square.Lines.LeftWidth);
                    pen.DashStyle = (DashStyle)square.Lines.LeftPattern;
                    break;
                case Direction.Right:
                    pen = new Pen(square.Colors.Right, square.Lines.RightWidth);
                    pen.DashStyle = (DashStyle)square.Lines.RightPattern;
                    break;
                case Direction.Up:
                    pen = new Pen(square.Colors.Top, square.Lines.TopWidth);
                    pen.DashStyle = (DashStyle)square.Lines.TopPattern;
                    break;
                default:
                    break;
            }
            pen.Width *= (pen.Color.ToArgb() == bg.ToArgb() ? fScaleBG : fScaleFG);
            return pen;
        }

        public static void Consolidate(List<DrawCommand> commands)
        {
            // Turn parallel half-width drawing commands into single full-width commands
            // This function assumes that the DrawCommands are non-diagonal
            // and are all drawing from up/left to down/right

            // Note:  This function doesn't work very well; segments become off-by-one
            // and won't combine with others

            bool bAnyChange = false;
            int iDiff = 0;
            float fNewWidth = 0f;

            do
            {
                bAnyChange = false;
                int iIndex = 0;
                while (iIndex < commands.Count)
                {
                    for (int iCompare = iIndex + 1; iCompare < commands.Count; iCompare++)
                    {
                        // Line segments are consolidatable if they are parallel, the same color and style, and either:
                        // 1.  The congruent and have overlapping widths, or
                        // 2.  Are the same width and have overlapping lengths

                        DrawCommand cmd1 = commands[iIndex];
                        DrawCommand cmd2 = commands[iCompare];

                        if (cmd1.m_pen.Color.ToArgb() != cmd2.m_pen.Color.ToArgb())
                            continue;    // Not the same color -> No consolidation possible

                        if (cmd1.m_pen.DashStyle != cmd1.m_pen.DashStyle)
                            continue;    // Not the same style -> No consolidation possible

                        if (cmd2.y1 == cmd2.y2 && cmd1.y1 == cmd1.y2)
                        {
                            // parallel horizontally ==
                            if (cmd1.x2 - cmd1.x1 == cmd2.x2 - cmd1.x1)
                            {
                                // Same length
                                iDiff = cmd2.y1 - cmd1.y1;
                                if (cmd1.m_pen.Width + cmd2.m_pen.Width + 0.5 >= Math.Abs(iDiff))
                                {
                                    // Widths overlap; combine these commands
                                    fNewWidth = cmd1.m_pen.Width + cmd2.m_pen.Width;
                                    fNewWidth -= (cmd2.m_pen.Width - Math.Abs(iDiff));
                                    cmd1.y1 += (int) (fNewWidth / 2.0);
                                    cmd1.y2 = cmd1.y1;
                                    cmd1.m_pen.Width = fNewWidth;
                                    bAnyChange = true;
                                    commands.Remove(cmd2);
                                    break;
                                }
                            }
                            else if (cmd1.m_pen.Width == cmd2.m_pen.Width)
                            {
                                // Same widths
                                if (Math.Max(cmd2.x2, cmd1.x2) - Math.Min(cmd2.x1, cmd1.x1) < (cmd2.x2 - cmd2.x1 + cmd1.x2 - cmd1.x1))
                                {
                                    // Line segments overlap; combine them
                                    cmd1.x1 = Math.Min(cmd1.x1, cmd2.x1);
                                    cmd1.x2 = Math.Max(cmd1.x2, cmd2.x2);
                                    bAnyChange = true;
                                    commands.Remove(cmd2);
                                }
                            }
                        }
                        else if (cmd2.x1 == cmd2.x2 && cmd1.x1 == cmd1.x2)
                        {
                            // parallel vertically   ||
                            if (cmd1.y2 - cmd1.y1 == cmd2.y2 - cmd1.y1)
                            {
                                // Same length
                                if (cmd1.m_pen.Width + cmd2.m_pen.Width >= Math.Abs(cmd2.y1 - cmd1.y1))
                                {
                                    iDiff = cmd2.x1 - cmd1.x1;
                                    if (cmd1.m_pen.Width + cmd2.m_pen.Width + 0.5 >= Math.Abs(iDiff))
                                    {
                                        // Widths overlap; combine these commands
                                        fNewWidth = cmd1.m_pen.Width + cmd2.m_pen.Width;
                                        fNewWidth -= (cmd2.m_pen.Width - Math.Abs(iDiff));
                                        cmd1.x1 += (int)(fNewWidth / 2.0);
                                        cmd1.x2 = cmd1.x1;
                                        cmd1.m_pen.Width = fNewWidth;
                                        bAnyChange = true;
                                        commands.Remove(cmd2);
                                        break;
                                    }
                                }
                            }
                            else if (cmd1.m_pen.Width == cmd2.m_pen.Width)
                            {
                                // Same width
                                if (Math.Max(cmd2.y2, cmd1.y2) - Math.Min(cmd2.y1, cmd1.y1) < (cmd2.y2 - cmd2.y1 + cmd1.y2 - cmd1.y1))
                                {
                                    // Line segments overlap; combine them
                                    cmd1.y1 = Math.Min(cmd1.y1, cmd2.y1);
                                    cmd1.y2 = Math.Max(cmd1.y2, cmd2.y2);
                                    bAnyChange = true;
                                    commands.Remove(cmd2);
                                }
                            }
                        }
                    }
                    if (bAnyChange)
                        break;
                    iIndex++;
                }
            } while (bAnyChange);
        }

        public void DoublePen(Direction dir)
        {
            int iMoveMajor = (int)(m_pen.Width + 0.5);
            int iMoveMinor = (int)m_pen.Width;
            m_pen.Width *= 2;

            switch (dir)
            {
                case Direction.Up:
                    y1 -= iMoveMinor;
                    y2 = y1;
                    break;
                case Direction.Left:
                    x1 -= iMoveMinor;
                    x2 = x1;
                    break;
                case Direction.Right:
                    x1 += iMoveMajor;
                    x2 = x1;
                    break;
                case Direction.Down:
                    y1 += iMoveMajor;
                    y2 = y1;
                    break;
                default:
                    break;
            }
        }

        public static void Combine(ref DrawCommand cmd1, ref DrawCommand cmd2, Direction dir)
        {
            if (cmd1 == null || cmd2 == null)
                return;
            // Combines cmd2 into cmd1 if possible
            if (cmd1.m_pen.Width != cmd2.m_pen.Width)
                return;
            if (cmd1.m_pen.DashStyle != cmd2.m_pen.DashStyle)
                return;
            if (cmd1.m_pen.Color.ToArgb() != cmd2.m_pen.Color.ToArgb())
                return;

            // Pens are the same; combine into a single command
            switch (dir)
            {
                case Direction.Right:
                    cmd1.x2 = cmd2.x2;
                    break;
                case Direction.Down:
                    cmd1.y2 = cmd2.y2;
                    break;
                case Direction.Up:
                    cmd1.y1 = cmd2.y1;
                    break;
                case Direction.Left:
                    cmd1.x1 = cmd2.x1;
                    break;
                default:
                    break;
            }

            cmd2 = null;
        }

        public DrawCommand(MapSquare square, Direction dir, Color bg, float fScaleBG, float fScaleFG, int X1, int Y1, int X2, int Y2)
        {
            Edge = dir;
            BackgroundColor = bg;
            m_pen = GetPen(square, dir, bg, fScaleBG, fScaleFG);

            // Draw lines the same direction to save trouble during consolidation
            x1 = Math.Min(X1, X2);
            y1 = Math.Min(Y1, Y2);
            x2 = Math.Max(X1, X2);
            y2 = Math.Max(Y1, Y2);
        }

        public void Draw(Graphics g)
        {
            int offsetMajor = ((int)m_pen.Width + 1) / 2;
            int offsetMinor = (int)m_pen.Width / 2;

            switch (Edge)
            {
                case Direction.Down:    // y1 == y2, bottom edge of square
                    g.DrawLine(m_pen, x1, y1 - offsetMajor, x2, y2 - offsetMajor);
                    break;
                case Direction.Right:   // x1 == x2, right edge of square
                    g.DrawLine(m_pen, x1 - offsetMajor, y1, x2 - offsetMajor, y2);
                    break;
                case Direction.Up:      // y1 == y2, top edge of square
                    g.DrawLine(m_pen, x1, y1 + offsetMinor, x2, y2 + offsetMinor);
                    break;
                case Direction.Left:    // x1 == x2, left edge of square
                    g.DrawLine(m_pen, x1 + offsetMinor, y1, x2 + offsetMinor, y2);
                    break;
                default:
                    break;
            }
        }

        public int CompareTo(object o)
        {
            if (!(o is DrawCommand))
                return 0;

            DrawCommand cmd = (DrawCommand)o;

            // Background color is always lowest

            if (m_pen.Color.ToArgb() == BackgroundColor.ToArgb() && cmd.m_pen.Color.ToArgb() != BackgroundColor.ToArgb())
                return -1;

            if (m_pen.Color.ToArgb() != BackgroundColor.ToArgb() && cmd.m_pen.Color.ToArgb() == BackgroundColor.ToArgb())
                return 1;

            return m_pen.Width.CompareTo(cmd.m_pen.Width);
        }
    }


    public class Notification
    {
        public enum AlertType { None, Text, Audio, Both }

        public string AudioFile;
        public string Message;
        public AlertType Type;

        public Notification()
        {
            Type = AlertType.None;
            Message = String.Empty;
            AudioFile = String.Empty;
        }

        public Notification(string strMessage)
        {
            Type = AlertType.Text;
            Message = strMessage;
            AudioFile = String.Empty;
        }

        public Notification(AlertType type, string strMessage, string strAudioFile = "")
        {
            Type = type;
            Message = strMessage;
            AudioFile = strAudioFile;
        }

        public void PlayAudio(bool bShowErrors = false, string strFile = "")
        {
            try
            {
                if (String.IsNullOrWhiteSpace(strFile))
                    strFile = AudioFile;
                strFile = Environment.ExpandEnvironmentVariables(strFile);
                if (!File.Exists(strFile))
                    strFile = Path.Combine(Global.ExePath, Path.GetFileName(strFile));
                if (!File.Exists(strFile))
                {
                    if (bShowErrors)
                        MessageBox.Show(String.Format("The file \"{0}\" could not be located.", strFile), "Invalid audio file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(strFile);
                player.Play();
            }
            catch (Exception ex)
            {
                if (bShowErrors)
                    MessageBox.Show(String.Format("Could not play audio file.\r\n\r\nException: {0}", ex.Message), "Invalid audio file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public bool Any { get { return Type != AlertType.None; } }
        public bool AudioType { get { return Type == AlertType.Audio || Type == AlertType.Both; } }
        public bool TextType { get { return Type == AlertType.Text || Type == AlertType.Both; } }

        public void ShowNotification()
        {
            switch (Type)
            {
                case AlertType.Audio:
                    break;
                case AlertType.Text:
                    break;
                default:
                    // No notification
                    break;
            }
        }
    }

    public class LocationSettings
    {
        public AxisIncreaseX IncreaseX;
        public AxisIncreaseY IncreaseY;
        public int OffsetX;
        public int OffsetY;

        public LocationSettings()
        {
            IncreaseX = AxisIncreaseX.LeftToRight;
            IncreaseY = AxisIncreaseY.TopToBottom;
            OffsetX = 0;
            OffsetY = 0;
        }
    }

    public class MapSheetPathInfo
    {
        public MapSheet Sheet;
        public string Path;
        public int Index;
        public int Zoom;

        public MapSheetPathInfo(MapSheet sheet, string path, int index, int zoom)
        {
            Sheet = sheet;
            Path = path;
            Index = index;
            Zoom = zoom;
        }

        public override int GetHashCode()
        {
            return Sheet.GetHashCode() ^ Path.GetHashCode() ^ Index ^ Zoom;
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", Sheet == null || Sheet.Title == null ? "<null>" : Sheet.Title, Path == null ? "<null>" : Path);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MapSheetPathInfo))
                return false;

            MapSheetPathInfo pair = (MapSheetPathInfo)obj;
            if (pair.Sheet != Sheet)
                return false;

            if (pair.Path != Path)
                return false;

            if (pair.Index != Index)
                return false;

            if (pair.Zoom != Zoom)
                return false;

            return true;
        }
    }

    public class TOCombatant : IComparable<TOCombatant>
    {
        public string Name;
        public int Speed;
        public int Position;

        public TOCombatant(string name, int speed, int position)
        {
            Name = name;
            Speed = speed;
            Position = position;
        }

        public int CompareTo(TOCombatant combatant)
        {
            int iDiff = combatant.Speed - Speed;
            if (iDiff == 0)
                return Math.Sign(Position - combatant.Position);
            return Math.Sign(iDiff);
        }
    }

    public class TurnOrderCalculator
    {
        private int m_iPartySpeedBonus = 0;
        private List<TOCombatant> m_list;

        public TurnOrderCalculator(int target, int value)
        {
            m_list = new List<TOCombatant>();
            SetHandicap(target, value);
        }

        public void SetHandicap(int target, int value)
        {
            m_iPartySpeedBonus = (target == 1 ? value : target == 2 ? -value : 0);
        }

        public void AddPlayerCharacter(string name, int speed, int position)
        {
            m_list.Add(new TOCombatant(name, speed + m_iPartySpeedBonus, position));
        }

        public void AddMonster(string name, int speed, int position)
        {
            m_list.Add(new TOCombatant(name, speed, position));
        }

        public string GetTurnOrder()
        {
            if (m_list.Count == 0)
                return "";

            m_list.Sort();
            StringBuilder sb = new StringBuilder();
            int iDuplicate = 0;
            for (int i = 1; i < m_list.Count; i++)
            {
                if (m_list[i].Name == m_list[i - 1].Name)
                    iDuplicate++;
                else
                {
                    if (iDuplicate > 0)
                        sb.AppendFormat("{0} ({1}), ", m_list[i - 1].Name, iDuplicate + 1);
                    else
                        sb.AppendFormat("{0}, ", m_list[i-1].Name);
                    iDuplicate = 0;
                }
            }
            if (iDuplicate > 0)
                sb.AppendFormat("{0} ({1}), ", m_list[m_list.Count-1].Name, iDuplicate + 1);
            else
                sb.AppendFormat("{0}, ", m_list[m_list.Count-1].Name);
            return Global.Trim(sb).ToString();
        }
    }

    public class WindowFinder
    {
        public string m_strRegexCaption = "";
        public string m_strRegexClass = "";
        public List<IntPtr> m_regexMatchingCaptions = null;
        public bool m_bIgnoreInvisible = true;

        public IntPtr[] FindWindowByRexeg(string strClass, string strCaption, bool bIgnoreInvisible = true)
        {
            m_bIgnoreInvisible = bIgnoreInvisible;
            m_strRegexCaption = strCaption;
            m_regexMatchingCaptions = new List<IntPtr>();
            NativeMethods.EnumWindows(RegexEnumWindowsProc, IntPtr.Zero);
            return m_regexMatchingCaptions.ToArray();
        }

        public bool RegexEnumWindowsProc(IntPtr hWnd, IntPtr lParam)
        {
            if (m_bIgnoreInvisible && !NativeMethods.IsWindowVisible(hWnd))
                return true;
            if (Regex.IsMatch(NativeMethods.GetWindowText(hWnd), m_strRegexCaption))
                if (Regex.IsMatch(NativeMethods.GetClassName(hWnd), m_strRegexClass))
                    if (NativeMethods.IsWindowVisible(hWnd))
                        m_regexMatchingCaptions.Add(hWnd);

            return true;
        }
    }

    public class LaunchGameInfo
    {
        public Process Proc;
        private IntPtr WindowHandle;
        public IMain Main;

        public LaunchGameInfo(Process proc, IMain main)
        {
            Proc = proc;
            WindowHandle = IntPtr.Zero;
            Main = main;
        }
    }

    [Flags]
    public enum BasicConditionFlags : long
    {
        Good = 0x00000000,
        Asleep = 0x00000001,
        Blinded = 0x00000002,
        Silenced = 0x00000004,
        Diseased = 0x00000008,
        Poisoned = 0x00000010,
        Paralyzed = 0x00000020,
        Unconscious = 0x00000040,
        Cursed = 0x00000080,
        Stone = 0x00000100,
        Dead = 0x00000200,
        Eradicated = 0x00000400,
        EncasedAir = 0x00000800,
        EncasedWater = 0x00001000,
        EncasedEarth = 0x00002000,
        EncasedFire = 0x00004000,
        Mindless = 0x00008000,
        Afraid = 0x00010000,
        Held = 0x00020000,
        Webbed = 0x00040000,
        Hurt = 0x00080000,
        Weak = 0x00100000,
        HeartBroken = 0x00200000,
        Insane = 0x00400000,
        InLove = 0x00800000,
        Drunk = 0x01000000,
        Confused = 0x02000000,
        Depressed = 0x04000000,
        BrokenItem = 0x08000000,
        CursedItem = 0x10000000,
        Hypnotized = 0x20000000,
        Lost = 0x40000000,
        Old = 0x80000000,
        Last = 0x100000000,
        UnableToCast = Asleep | Silenced | Paralyzed | Unconscious | Dead | Stone | Eradicated | Depressed
    }

    public enum AttributeType
    {
        Unknown,
        UInt8,
        Int16,
        UInt16,
        UInt24,
        Int32,
        UInt32,
        TwoUInt8,
        TwoUInt16,
        ThreeUInt16,
        Item,
        LevSexAlignRaceClass,
        KnownSpells,
        SecondarySkills,
        Condition,
        ReadySpell,
        MapAndPosition,
        StatMax18,
        StatMax31,
        Int64,
        SixByteLong,
        TwoInt16,
        WizCondition,
        WizSpellPoints,
        Wiz5SpellPoints
    }

    public class EditableAttribute
    {
        public byte[] Bytes;
        public UInt16[] UShorts;
        public Int16[] Shorts;
        public UInt24[] Int24s;
        public Int32[] Ints;
        public UInt32[] UInts;
        public long[] Longs;
        public string[] Strings;
        public MMItem[] Items;
        public long Minimum;
        public long Maximum;

        public EditableAttribute(byte[] bytes, UInt16[] ushorts, Int16[] shorts, UInt24[] int24s, UInt32[] uints, Int32[] ints, long[] longs, string[] strings, MMItem[] items, long min = 0, long max = 0)
        {
            Bytes = bytes;
            UShorts = ushorts;
            Shorts = shorts;
            Int24s = int24s;
            Ints = ints;
            UInts = uints;
            Longs = longs;
            Strings = strings;
            Items = items;
            Minimum = min;
            Maximum = max;
        }

        public EditableAttribute(byte b)
        {
            Bytes = new byte[] { b } ;
            Shorts = null;
            UShorts = null;
            Int24s = null;
            Ints = null;
            UInts = null;
            Strings = null;
            Items = null;
        }

        public EditableAttribute(byte[] bytes)
        {
            Bytes = bytes;
            Shorts = null;
            UShorts = null;
            Int24s = null;
            Ints = null;
            UInts = null;
            Strings = null;
            Items = null;
        }

        public EditableAttribute(UInt16 i)
        {
            Bytes = null;
            Shorts = null;
            UShorts = new UInt16[] { i };
            Int24s = null;
            Ints = null;
            UInts = null;
            Strings = null;
            Items = null;
        }

        public EditableAttribute(Int16 i)
        {
            Bytes = null;
            Shorts = new Int16[] { i };
            UShorts = null;
            Int24s = null;
            Ints = null;
            UInts = null;
            Strings = null;
            Items = null;
        }

        public EditableAttribute(UInt32 i)
        {
            Bytes = null;
            Shorts = null;
            UShorts = null;
            Int24s = null;
            Ints = null;
            UInts = new UInt32[] { i };
            Strings = null;
            Items = null;
        }

        public EditableAttribute(Int32 i)
        {
            Bytes = null;
            Shorts = null;
            UShorts = null;
            Int24s = null;
            Ints = new Int32[] { i };
            UInts = null;
            Strings = null;
            Items = null;
        }
    }

    public class UInt24
    {
        private int m_value;

        public Int32 Value
        { 
            get { return m_value & 0x00ffffff; }
            set { m_value = value & 0x00ffffff; }
        }

        public UInt24(int val)
        {
            Value = val;
        }

        public UInt24(byte[] bytes, int offset)
        {
            if (offset + 2 >= bytes.Length)
                Value = 0;
            Value = bytes[offset] + (bytes[offset + 1] << 8) + (bytes[offset + 2] << 16);
        }

        public override string ToString()
        {
            return m_value.ToString();
        }

        public byte[] GetBytes()
        {
            return BitConverter.GetBytes(Value).Take(3).ToArray();
        }

        public static implicit operator int(UInt24 v) { return v.Value; }
    }

    [Flags]
    public enum CheatMenuFlags
    {
        None = 0x0000,
        Add1 = 0x0001,
        Subtract1 = 0x0002,
        Minimum = 0x0004,
        Maximum = 0x0008,
        NextLevel = 0x0010,
        Edit = 0x0020,
        AddNew = 0x0040,
        SuperChar = 0x0080,
        AllNonlevel = Add1 | Subtract1 | Minimum | Maximum | Edit
    }

    public enum InventoryCharAction
    {
        None,
        FindExisting,
        FindPotential,
        FindOrCreate
    }

    public class NoteTemplateTag
    {
        public string OriginalText;
        public string FinalText;
        public string Symbol;

        public NoteTemplateTag(string str)
        {
            int iLength = 1;
            int iIndex = str.IndexOf('\t');
            if (iIndex == -1)
            {
                Symbol = "?";
                OriginalText = str;
                FinalText = str;
            }
            else
            {
                Symbol = str.Substring(0, iIndex);
                OriginalText = str.Substring(iIndex+iLength);
                FinalText = OriginalText;
            }
        }

        public NoteTemplateTag(string symbol, string original, string final)
        {
            Symbol = symbol;
            OriginalText = original;
            FinalText = final;
        }
    }

    public class AutoloadMapItem
    {
        public string Path;
        public string Display;

        public AutoloadMapItem(string path, string display)
        {
            Path = path;
            Display = display;
        }

        public override string ToString()
        {
            return Display;
        }
    }

    public class NoteSearchItem
    {
        public MapSheet Sheet;
        public MapNote Note;

        public NoteSearchItem(MapSheet sheet, MapNote note)
        {
            Sheet = sheet;
            Note = note;
        }
    }

    public class QuestBits
    {
        public object[] Bits;

        public QuestBits(params object[] bits)
        {
            Bits = bits;
        }

        public bool IsEmpty { get { return Bits == null || Bits.Length < 1 || Bits[0] == null; } }
    }

    public class DamageDice : IComparable<DamageDice>
    {
        public int Faces;
        public int Quantity;
        public int Bonus;

        public static DamageDice Zero { get { return new DamageDice(1, 0, 0); } }

        public int CompareTo(DamageDice dice)
        {
            // Compare the average results
            double avg1 = (Max + Min) / 2.0;
            double avg2 = (dice.Max + dice.Min) / 2.0;

            return Math.Sign(avg1 - avg2);
        }

        public DamageDice(int faces, int quantity, int bonus)
        {
            Faces = faces;
            Quantity = quantity;
            Bonus = bonus;
        }

        public DamageDice(DamageDice dice, int bonus)
        {
            Faces = dice.Faces;
            Quantity = dice.Quantity;
            Bonus = bonus;
        }

        public int Min { get { return Bonus + Quantity; } }
        public int Max { get { return Faces * Quantity + Bonus; } }
        public bool BonusOnly { get { return Bonus != 0 && Quantity == 0; } }

        public override string ToString()
        {
            if (Quantity == 0)
                return Bonus == 0 ? "0" : Global.AddPlus(Bonus);
            return String.Format("{0}d{1}{2}", Quantity, Faces, Bonus == 0 ? "" : Global.AddPlus(Bonus));
        }

        public string StringWithAverage
        {
            get
            {
                if (Quantity == 0)
                    return Bonus == 0 ? "0" : Global.AddPlus(Bonus);
                return String.Format("{0}d{1}{2} ({3:F1} av)", Quantity, Faces, Bonus == 0 ? "" : Global.AddPlus(Bonus), Average);
            }
        }

        public double Average { get { return Faces == 0 || Quantity == 0 ? 0 : ((Faces + 1) / 2.0) * Quantity + Bonus; } }

        public override int GetHashCode()
        {
            return (Faces << 16) | (Quantity << 8) | Bonus;
        }

        public override bool Equals(object obj)
        {
            DamageDice dd = obj as DamageDice;
            if (dd == null)
                return false;
            return dd.Faces == Faces && dd.Quantity == Quantity && dd.Bonus == Bonus;
        }
    }

    public class BasicDamage
    {
        public int NumAttacks;
        public List<DamageDice> Dice;
        public int Modifier;

        public BasicDamage(int iNumAttacks, DamageDice dice)
        {
            NumAttacks = iNumAttacks;
            Dice = new List<DamageDice>(1);
            Dice.Add(dice);
            Modifier = 0;
        }

        public BasicDamage(int iNumAttacks, List<DamageDice> dice, int iModifier = 0)
        {
            NumAttacks = iNumAttacks;
            Dice = dice;
            Modifier = iModifier;
        }

        public static BasicDamage Zero { get { return new BasicDamage(0, DamageDice.Zero); } }

        public int Min { get { return NumAttacks * Dice.Sum(d => d.Min); } }
        public int Max { get { return NumAttacks * Dice.Sum(d => d.Max); } }

        public double Average { get { return NumAttacks * Dice.Sum(d => d.Average); } }

        public int Bonus { get { return Dice.Sum(d => d.Bonus) + Modifier; } }
        public int Quantity { get { return Dice.Sum(d => d.Quantity); } }
        public int Faces { get { return (int) Dice.Average(d => d.Faces); } }

        public string StringWithAverage
        {
            get
            {
                if (Dice == null || Dice.Any(d => d == null) || NumAttacks == 0 || Quantity == 0)
                    return "0";
                if (NumAttacks == 1)
                    return String.Format("{0} ({1:F1} av)", DiceString(Dice), Dice.Sum(d => d.Average));
                return String.Format("{0}x {1} ({2:F1} av)", NumAttacks, DiceString(Dice), NumAttacks * Dice.Sum(d => d.Average));
            }
        }

        public override string ToString()
        {
            if (Dice == null || Dice.Any(d => d == null) || NumAttacks == 0 || Quantity == 0)
                return "0";
            if (NumAttacks == 1)
                return DiceString(Dice);
            return String.Format("{0}x {1}", NumAttacks, DiceString(Dice));
        }

        public void AddBytes(Stream stream)
        {
            foreach(int i in new int[] { NumAttacks, Faces, Quantity, Bonus })
                Global.WriteInt32(stream, i);
        }

        public void Add(DamageDice dice)
        {
            if (Dice == null)
                Dice = new List<DamageDice>();
            if (Dice.Count == 1 && Dice[0].Max == 0)
                Dice[0] = dice;
            else
                Dice.Add(dice);
        }

        public static string DiceString(List<DamageDice> dice)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DamageDice die in dice)
                if (die.Max > 0)
                    sb.AppendFormat("{0}, ", die.ToString());
            return Global.Trim(sb).ToString();
        }

        public static string UniqueDiceString(List<DamageDice> dice)
        {
            // Convert a collection of arbitrary numbers of dice into a condensed string
            // e.g. 4d10 + 3d6 + 2d10 + 8d6 => 6d10 + 11d6
            Dictionary<int, int> uniqueDice = new Dictionary<int, int>();
            int iTotalBonus = 0;
            foreach (DamageDice die in dice)
            {
                if (die.Quantity == 0)
                    continue;
                if (!uniqueDice.ContainsKey(die.Faces))
                    uniqueDice.Add(die.Faces, die.Quantity);
                else
                    uniqueDice[die.Faces] += die.Quantity;
                iTotalBonus += die.Bonus;
            }
            StringBuilder sb = new StringBuilder();
            foreach (int iFace in uniqueDice.Keys)
                sb.AppendFormat("{0}d{1}+", uniqueDice[iFace], iFace);
            if (iTotalBonus > 0)
                sb.AppendFormat("{0}", iTotalBonus);
            else if (sb.Length > 0)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }

    public class HitDamageAC
    {
        public int Hit;
        public int Damage;
        public int AC;

        public HitDamageAC(int hit, int damage, int ac)
        {
            Hit = hit;
            Damage = damage;
            AC = ac;
        }

        public static HitDamageAC Empty { get { return new HitDamageAC(0, 0, 0); } }

        public override string ToString()
        {
            return String.Format("To Hit: {0}, Damage: {1}, AC: {2}", Global.AddPlus(Hit), Global.AddPlus(Damage), Global.AddPlus(AC));
        }

        public string AddHitString
        {
            get
            {
                if (Hit == 0)
                    return String.Empty;
                return String.Format(", Hit{0}", Global.AddPlus(Hit));
            }
        }

        public string ToString(ItemType type)
        {
            switch (type)
            {
                case ItemType.OneHandMelee:
                case ItemType.TwoHandMelee:
                case ItemType.Weapon:
                case ItemType.Missile:
                    return String.Format("{0} to Hit, {1} Damage", Global.AddPlus(Hit), Global.AddPlus(Damage));
                case ItemType.Miscellaneous:
                case ItemType.Quest:
                    return String.Empty;
                default:
                    if (AC != 0)
                        return String.Format("AC {0}", Global.AddPlus(AC));
                    else
                        return String.Empty;
            }
        }
    }

    public class ElementDamageResistance
    {
        public GenericResistanceFlags DamageElement;
        public int DamageValue;
        public GenericResistanceFlags ResistElement;
        public int ResistValue;

        public ElementDamageResistance(GenericResistanceFlags element, int damage, int resist)
        {
            DamageElement = element;
            ResistElement = element;
            DamageValue = damage;
            ResistValue = resist;
        }

        public static ElementDamageResistance Empty { get { return new ElementDamageResistance(GenericResistanceFlags.None, 0, 0); } }

        public override string ToString()
        {
            //if (DamageElement == ResistElement)
            //    return String.Format("{0}, Resist {1}, Damage {2}",
            //        Global.SingleResistance(DamageElement),
            //        Global.AddPlus(ResistValue),
            //        Global.AddPlus(DamageValue));
            //else
                return String.Format("Resist {0} {1}, {2} Damage {3}",
                    Global.SingleResistance(ResistElement),
                    Global.AddPlus(ResistValue),
                    Global.SingleResistance(DamageElement),
                    Global.AddPlus(DamageValue));
        }

        public string ToString(bool bWeapon)
        {
            if (bWeapon)
                return String.Format("{0} Damage {1}",
                    Global.SingleResistance(DamageElement),
                    Global.AddPlus(DamageValue));
            else
                return String.Format("Resist {0} {1}",
                    Global.SingleResistance(ResistElement),
                    Global.AddPlus(ResistValue));
        }
    }

    public class AttributeModifier
    {
        public GenericAttribute Attribute;
        public int Modifier;

        public AttributeModifier(GenericAttribute attrib, int mod)
        {
            Attribute = attrib;
            Modifier = mod;
        }

        public static AttributeModifier Empty { get { return new AttributeModifier(GenericAttribute.None, 0); } }

        public override string ToString()
        {
            return String.Format("{0} {1}", Global.GenericAttributeString(Attribute), Global.AddPlus(Modifier));
        }
    }

    public enum GenericAttribute
    {
        None,
        Might,
        Intellect,
        Personality,
        Endurance,
        Speed,
        Accuracy,
        Strength,
        IQ,
        Piety,
        Vitality,
        Agility,
        Luck,
        HP,
        SP,
        AC,
        Thievery,
        Sex,
        Race,
        Level,
        Age,
        SpellLevel,
        Food,
        MagicRes,
        FireRes,
        ColdRes,
        ElecRes,
        AcidRes,
        PoisonRes,
        SleepRes,
        FearRes,
        EnergyRes,
        Cursed,
        Gems,
        Gold,
        IdentifyTrap,
        Critical,
        IdentifyItem,
        Dispel,
        Swimming,
        Marks,
        RIP,
        Regeneration,
        Dexterity,
        Constitution
    }

    public class InventoryItemTag
    {
        public ShopItem ShopItem;
        public int MemoryIndex;
        public string DisplayIndex;
        public string ListViewString;

        public Item Item { get { return ShopItem.Item; } }

        public InventoryItemTag(ShopItem item, int memory, string display, string strListView)
        {
            ShopItem = item;
            MemoryIndex = memory;
            DisplayIndex = display;
            ListViewString = strListView;
        }

        public InventoryItemTag(Item item, int memory, string display, string strListView)
        {
            ShopItem = new ShopItem(item, -1, -1);
            MemoryIndex = memory;
            DisplayIndex = display;
            ListViewString = strListView;
        }
    }

    public class MapXY
    {
        public GameNames Game;
        public int Map;
        public int Era;
        public Point Location;

        public static MapXY Empty { get { return new MapXY(GameNames.None, -1, -1, -1); } }
        public bool IsEmpty { get { return Game == GameNames.None; } }

        public int X { get { return Location.X; } set { Location.X = value; } }
        public int Y { get { return Location.Y; } set { Location.Y = value; } }

        public MapXY(GameNames game, int map, int x, int y)
        {
            Game = game;
            Map = map;
            Era = -1;
            Location = new Point(x, y);
        }

        public MapXY(GameNames game, int map, int x, int y, int era)
        {
            Game = game;
            Map = map;
            Era = era;
            Location = new Point(x, y);
        }

        public override bool Equals(object obj)
        {
            MapXY compare = obj as MapXY;
            if (compare == null)
                return false;
            return (Game == compare.Game && Map == compare.Map && Location == compare.Location);
        }

        public override int GetHashCode()
        {
            return Era ^ ((int)Game << 24) ^ Map << 16 ^ Location.X << 8 ^ Location.Y;
        }

        public MapXY Clone()
        {
            return new MapXY(Game, Map, Location.X, Location.Y);
        }

        public MapXY(Wiz1Map map, int x, int y) : this(GameNames.Wizardry1, (int)map, x, y) { }
        public MapXY(Wiz2Map map, int x, int y) : this(GameNames.Wizardry2, (int)map, x, y) { }
        public MapXY(Wiz3Map map, int x, int y) : this(GameNames.Wizardry3, (int)map, x, y) { }
        public MapXY(Wiz4Map map, int x, int y) : this(GameNames.Wizardry4, (int)map, x, y) { }
        public MapXY(Wiz5Map map, int x, int y) : this(GameNames.Wizardry5, (int)map, x, y) { }
        public MapXY(MM1Map map, int x, int y) : this(GameNames.MightAndMagic1, (int)map, x, y) { }
        public MapXY(MM2Map map, int x, int y) : this(GameNames.MightAndMagic2, (int)map, x, y) { }
        public MapXY(MM2Map map, int x, int y, int era) : this(GameNames.MightAndMagic2, (int)map, x, y, era) { }
        public MapXY(MM3Map map, int x, int y) : this(GameNames.MightAndMagic3, (int)map, x, y) { }
        public MapXY(MM4Map map, int x, int y) : this(GameNames.MightAndMagic45, (int)map, x, y) { }
        public MapXY(MM5Map map, int x, int y) : this(GameNames.MightAndMagic45, (int)map + 256, x, y) { }
        public MapXY(BT1Map map, int x, int y) : this(GameNames.BardsTale1, (int)map, x, y) { }
        public MapXY(BT2Map map, int x, int y) : this(GameNames.BardsTale2, (int)map, x, y) { }
        public MapXY(BT3Map map, int x, int y) : this(GameNames.BardsTale3, (int)map, x, y) { }

        public static MapXY[] Combine(params MapXY[][] arrays)
        {
            List<MapXY> locations = new List<MapXY>();
            foreach (MapXY[] array in arrays)
                locations.AddRange(array);
            return locations.ToArray();
        }

        public static MapXY[] Array(GameNames game, int map, params int[] coordinates)
        {
            MapXY[] locations = new MapXY[coordinates.Length / 2];
            for (int i = 0; i < coordinates.Length; i += 2)
                locations[i / 2] = new MapXY(game, map, coordinates[i], coordinates[i + 1]);
            return locations;
        }

        public static MapXY[] Array(MM1Map map, params int[] coordinates) { return Array(GameNames.MightAndMagic1, (int) map, coordinates); }
        public static MapXY[] Array(MM2Map map, params int[] coordinates) { return Array(GameNames.MightAndMagic2, (int)map, coordinates); }
        public static MapXY[] Array(MM3Map map, params int[] coordinates) { return Array(GameNames.MightAndMagic3, (int)map, coordinates); }
        public static MapXY[] Array(MM4Map map, params int[] coordinates) { return Array(GameNames.MightAndMagic45, (int)map, coordinates); }
        public static MapXY[] Array(MM5Map map, params int[] coordinates) { return Array(GameNames.MightAndMagic45, (int)map + 256, coordinates); }

        public static MapXY[] Array(GameNames game, int map, params Point[] coordinates)
        {
            MapXY[] locations = new MapXY[coordinates.Length];
            for (int i = 0; i < coordinates.Length; i++)
                locations[i] = new MapXY(game, map, coordinates[i].X, coordinates[i].Y);
            return locations;
        }

        public static MapXY[] Array(MM1Map map, params Point[] coordinates) { return Array(GameNames.MightAndMagic1, (int)map, coordinates); }
        public static MapXY[] Array(MM2Map map, params Point[] coordinates) { return Array(GameNames.MightAndMagic2, (int)map, coordinates); }
        public static MapXY[] Array(MM3Map map, params Point[] coordinates) { return Array(GameNames.MightAndMagic3, (int)map, coordinates); }
        public static MapXY[] Array(MM4Map map, params Point[] coordinates) { return Array(GameNames.MightAndMagic45, (int)map, coordinates); }
        public static MapXY[] Array(MM5Map map, params Point[] coordinates) { return Array(GameNames.MightAndMagic45, (int)map + 256, coordinates); }

        public override string ToString() { return String.Format("{0}: {1},{2}", Games.GetMapTitleFunction(Game)(Map), Location.X, Location.Y); }

        public static MapXY Match(int x, int y, int iMap, params MapXY[] spots)
        {
            foreach (MapXY spot in spots)
            {
                if (spot.X == x && spot.Y == y && spot.Map == iMap)
                    return spot;
            }
            return null;
        }
    }

    public enum ModAttr
    {
        Invalid = -1,
        Might = 0,
        Intellect,
        Personality,
        Endurance,
        Speed,
        Accuracy,
        Luck,
        Strength,
        IQ,
        Piety,
        Vitality,
        Agility,
        Level,
        Thievery,
        ArmorClass,
        HitPoints,
        SpellPoints,
        Magic,
        Cold,
        Fire,
        Electricity,
        Acid,
        Poison,
        Sleep,
        Fear,
        Paralyze,
        Energy,
        MeleeToHit,
        RangedToHit,
        MeleeDamage,
        RangedDamage,
        SaveVsPoison,
        SaveVsPetrify,
        SaveVsWand,
        SaveVsBreath,
        SaveVsSpell,
        Regen,
        PoisonCount,
        IdentifyTrap,
        Critical,
        SaveVsSleep,
        SaveVsParalyze,
        IdentifyItem,
        Dispel,
        Swimming,
        Marks,
        RIP,
        Dexterity,
        Constitution,

        Last
    }

    public class Modifiers
    {
        public static ModAttr GetAttrib(GenericAttribute attrib)
        {
            switch (attrib)
            {
                case GenericAttribute.Might: return ModAttr.Might;
                case GenericAttribute.Intellect: return ModAttr.Intellect;
                case GenericAttribute.Personality: return ModAttr.Personality;
                case GenericAttribute.Endurance: return ModAttr.Endurance;
                case GenericAttribute.Speed: return ModAttr.Speed;
                case GenericAttribute.Accuracy: return ModAttr.Accuracy;
                case GenericAttribute.Luck: return ModAttr.Luck;
                case GenericAttribute.Strength: return ModAttr.Strength;
                case GenericAttribute.IQ: return ModAttr.IQ;
                case GenericAttribute.Piety: return ModAttr.Piety;
                case GenericAttribute.Vitality: return ModAttr.Vitality;
                case GenericAttribute.Agility: return ModAttr.Agility;
                case GenericAttribute.Level: return ModAttr.Level;
                case GenericAttribute.Thievery: return ModAttr.Thievery;
                case GenericAttribute.AC: return ModAttr.ArmorClass;
                case GenericAttribute.HP: return ModAttr.HitPoints;
                case GenericAttribute.SP: return ModAttr.SpellPoints;
                case GenericAttribute.MagicRes: return ModAttr.Magic;
                case GenericAttribute.ColdRes: return ModAttr.Cold;
                case GenericAttribute.FireRes: return ModAttr.Fire;
                case GenericAttribute.ElecRes: return ModAttr.Electricity;
                case GenericAttribute.AcidRes: return ModAttr.Acid;
                case GenericAttribute.PoisonRes: return ModAttr.Poison;
                case GenericAttribute.SleepRes: return ModAttr.Sleep;
                case GenericAttribute.FearRes: return ModAttr.Fear;
                case GenericAttribute.EnergyRes: return ModAttr.Energy;
                case GenericAttribute.IdentifyTrap: return ModAttr.IdentifyTrap;
                case GenericAttribute.Critical: return ModAttr.Critical;
                case GenericAttribute.IdentifyItem: return ModAttr.IdentifyItem;
                case GenericAttribute.Dispel: return ModAttr.Dispel;
                case GenericAttribute.Swimming: return ModAttr.Swimming;
                case GenericAttribute.Marks: return ModAttr.Marks;
                case GenericAttribute.RIP: return ModAttr.RIP;
                case GenericAttribute.Regeneration: return ModAttr.Regen;
                default: return ModAttr.Invalid;
            }
        }

        public static ModAttr GetAttrib(GenericResistanceFlags resist)
        {
            switch (resist)
            {
                case GenericResistanceFlags.Acid: return ModAttr.Acid;
                case GenericResistanceFlags.Magic: return ModAttr.Magic;
                case GenericResistanceFlags.Cold: return ModAttr.Cold;
                case GenericResistanceFlags.Fire: return ModAttr.Fire;
                case GenericResistanceFlags.Electricity: return ModAttr.Electricity;
                case GenericResistanceFlags.Poison: return ModAttr.Poison;
                case GenericResistanceFlags.Sleep: return ModAttr.Sleep;
                case GenericResistanceFlags.Energy: return ModAttr.Energy;
                case GenericResistanceFlags.Fear: return ModAttr.Fear;
                case GenericResistanceFlags.SaveVsPoison: return ModAttr.SaveVsPoison;
                case GenericResistanceFlags.SaveVsPetrify: return ModAttr.SaveVsPetrify;
                case GenericResistanceFlags.SaveVsWand: return ModAttr.SaveVsWand;
                case GenericResistanceFlags.SaveVsBreath: return ModAttr.SaveVsBreath;
                case GenericResistanceFlags.SaveVsSpell: return ModAttr.SaveVsSpell;
                case GenericResistanceFlags.SaveVsSleep: return ModAttr.SaveVsSleep;
                case GenericResistanceFlags.SaveVsParalyze: return ModAttr.SaveVsParalyze;
                default: return ModAttr.Invalid;
            }
        }

        public int Might { get { return Value(ModAttr.Might); } }
        public int Intellect { get { return Value(ModAttr.Intellect); } }
        public int Personality { get { return Value(ModAttr.Personality); } }
        public int Endurance { get { return Value(ModAttr.Endurance); } }
        public int Speed { get { return Value(ModAttr.Speed); } }
        public int Accuracy { get { return Value(ModAttr.Accuracy); } }
        public int Strength { get { return Value(ModAttr.Strength); } }
        public int IQ { get { return Value(ModAttr.IQ); } }
        public int Piety { get { return Value(ModAttr.Piety); } }
        public int Vitality { get { return Value(ModAttr.Vitality); } }
        public int Agility { get { return Value(ModAttr.Agility); } }
        public int Dexterity { get { return Value(ModAttr.Dexterity); } }
        public int Constitution { get { return Value(ModAttr.Constitution); } }
        public int Luck { get { return Value(ModAttr.Luck); } }
        public int Level { get { return Value(ModAttr.Level); } }
        public int Thievery { get { return Value(ModAttr.Thievery); } }
        public int ArmorClass { get { return Value(ModAttr.ArmorClass); } }
        public int HitPoints { get { return Value(ModAttr.HitPoints); } }
        public int SpellPoints { get { return Value(ModAttr.SpellPoints); } }
        public int Magic { get { return Value(ModAttr.Magic); } }
        public int Cold { get { return Value(ModAttr.Cold); } }
        public int Fire { get { return Value(ModAttr.Fire); } }
        public int Electricity { get { return Value(ModAttr.Electricity); } }
        public int Acid { get { return Value(ModAttr.Acid); } }
        public int Poison { get { return Value(ModAttr.Poison); } }
        public int Sleep { get { return Value(ModAttr.Sleep); } }
        public int Paralyze { get { return Value(ModAttr.Paralyze); } }
        public int Fear { get { return Value(ModAttr.Fear); } }
        public int Energy { get { return Value(ModAttr.Energy); } }
        public int MeleeToHit { get { return Value(ModAttr.MeleeToHit); } }
        public int RangedToHit { get { return Value(ModAttr.RangedToHit); } }
        public int MeleeDamage { get { return Value(ModAttr.MeleeDamage); } }
        public int RangedDamage { get { return Value(ModAttr.RangedDamage); } }
        public int SaveVsPoison { get { return Value(ModAttr.SaveVsPoison); } }
        public int SaveVsPetrify { get { return Value(ModAttr.SaveVsPetrify); } }
        public int SaveVsWand { get { return Value(ModAttr.SaveVsWand); } }
        public int SaveVsBreath { get { return Value(ModAttr.SaveVsBreath); } }
        public int SaveVsSpell { get { return Value(ModAttr.SaveVsSpell); } }
        public int SaveVsSleep { get { return Value(ModAttr.SaveVsSleep); } }
        public int SaveVsParalyze { get { return Value(ModAttr.SaveVsParalyze); } }
        public int IdentifyTrap { get { return Value(ModAttr.IdentifyTrap); } }
        public int Critical { get { return Value(ModAttr.Critical); } }
        public int IdentifyItem { get { return Value(ModAttr.IdentifyItem); } }
        public int Dispel { get { return Value(ModAttr.Dispel); } }
        public int Swimming { get { return Value(ModAttr.Swimming); } }
        public int Marks { get { return Value(ModAttr.Marks); } }
        public int RIP { get { return Value(ModAttr.RIP); } }
        public int Regen { get { return Value(ModAttr.Regen); } }

        public int[] AttributeArray = new int[ModAttr.Last - ModAttr.Might];
        public List<string>[] ReasonArray = new List<string>[ModAttr.Last - ModAttr.Might];

        public Modifiers()
        {
            Clear();
        }

        public void Clear()
        {
            for (int i = 0; i < AttributeArray.Length; i++)
            {
                AttributeArray[i] = 0;
                ReasonArray[i] = new List<string>(0);
            }
        }

        public Modifiers Clone()
        {
            Modifiers mod = new Modifiers();

            for (int i = 0; i < AttributeArray.Length; i++)
            {
                mod.AttributeArray[i] = AttributeArray[i];
                mod.ReasonArray[i] = new List<string>(ReasonArray[i]);
            }

            return mod;
        }

        public static string ModString(int iOriginal, int iModifier)
        {
            if (iModifier == 0)
                return iOriginal.ToString();
            return String.Format("{0}{1}", iOriginal, Global.AddPlus(iModifier));
        }

        public void Adjust(Modifiers mod)
        {
            for(ModAttr attr = ModAttr.Might; attr < ModAttr.Last; attr++)
                Adjust(attr, mod.Value(attr), mod.Reasons(attr));
        }

        public int Value(ModAttr attr) { return AttributeArray[(int) attr]; }
        public List<string> Reasons(ModAttr attr) { return ReasonArray[(int) attr]; }

        public List<string> Reasons(GenericResistanceFlags flags)
        {
            switch (flags)
            {
                case GenericResistanceFlags.Acid: return Reasons(ModAttr.Acid);
                case GenericResistanceFlags.Cold: return Reasons(ModAttr.Cold);
                case GenericResistanceFlags.Electricity: return Reasons(ModAttr.Electricity);
                case GenericResistanceFlags.Energy: return Reasons(ModAttr.Energy);
                case GenericResistanceFlags.Fire: return Reasons(ModAttr.Fire);
                case GenericResistanceFlags.Magic: return Reasons(ModAttr.Magic);
                case GenericResistanceFlags.Poison: return Reasons(ModAttr.Poison);
                case GenericResistanceFlags.Sleep: return Reasons(ModAttr.Sleep);
                case GenericResistanceFlags.Paralyze: return Reasons(ModAttr.Paralyze);
                case GenericResistanceFlags.SaveVsPoison: return Reasons(ModAttr.SaveVsPoison);
                case GenericResistanceFlags.SaveVsPetrify: return Reasons(ModAttr.SaveVsPetrify);
                case GenericResistanceFlags.SaveVsWand: return Reasons(ModAttr.SaveVsWand);
                case GenericResistanceFlags.SaveVsBreath: return Reasons(ModAttr.SaveVsBreath);
                case GenericResistanceFlags.SaveVsSpell: return Reasons(ModAttr.SaveVsSpell);
                default: return new List<string>(0);
            }
        }

        public void Adjust(ModAttr attr, int modVal, List<string> modReason)
        {
            if (modVal == 0)
                return;

            AttributeArray[(int) attr] += modVal;
            ReasonArray[(int) attr].AddRange(modReason);
        }

        public void Adjust(ModAttr attr, int modVal, string modReason)
        {
            if (modVal == 0)
                return;

            AttributeArray[(int)attr] += modVal;
            ReasonArray[(int)attr].Add(FormatReason(modVal, modReason));
        }

        private string FormatReason(int iMod, string strReason)
        {
            return String.Format("{0}\t{1}", Global.AddPlus(iMod), strReason);
        }

        public void Adjust(HitDamageAC hda, ItemType type, string reason)
        {
            switch(type)
            {
                case ItemType.OneHandMelee:
                case ItemType.TwoHandMelee:
                case ItemType.Weapon:
                    Adjust(ModAttr.MeleeToHit, hda.Hit, reason);
                    Adjust(ModAttr.MeleeDamage, hda.Damage, reason);
                    break;
                case ItemType.Missile:
                    Adjust(ModAttr.RangedToHit, hda.Hit, reason);
                    Adjust(ModAttr.RangedDamage, hda.Damage, reason);
                    break;
                case ItemType.Armor:
                case ItemType.Accessory:
                    Adjust(ModAttr.ArmorClass, hda.AC, reason);
                    break;
                default:
                    break;
            }
        }

        public void Adjust(AttributeModifier mod, string reason)
        {
            switch (mod.Attribute)
            {
                case GenericAttribute.AC:
                    Adjust(ModAttr.ArmorClass, mod.Modifier, reason);
                    break;
                case GenericAttribute.HP:
                    Adjust(ModAttr.HitPoints, mod.Modifier, reason);
                    break;
                case GenericAttribute.SP:
                    Adjust(ModAttr.SpellPoints, mod.Modifier, reason);
                    break;
                case GenericAttribute.Thievery:
                    Adjust(ModAttr.Thievery, mod.Modifier, reason);
                    break;
                case GenericAttribute.Might:
                    Adjust(ModAttr.Might, mod.Modifier, reason);
                    break;
                case GenericAttribute.Intellect:
                    Adjust(ModAttr.Intellect, mod.Modifier, reason);
                    break;
                case GenericAttribute.Personality:
                    Adjust(ModAttr.Personality, mod.Modifier, reason);
                    break;
                case GenericAttribute.Endurance:
                    Adjust(ModAttr.Endurance, mod.Modifier, reason);
                    break;
                case GenericAttribute.Speed:
                    Adjust(ModAttr.Speed, mod.Modifier, reason);
                    break;
                case GenericAttribute.Accuracy:
                    Adjust(ModAttr.Accuracy, mod.Modifier, reason);
                    break;
                case GenericAttribute.Strength:
                    Adjust(ModAttr.Strength, mod.Modifier, reason);
                    break;
                case GenericAttribute.IQ:
                    Adjust(ModAttr.IQ, mod.Modifier, reason);
                    break;
                case GenericAttribute.Piety:
                    Adjust(ModAttr.Piety, mod.Modifier, reason);
                    break;
                case GenericAttribute.Vitality:
                    Adjust(ModAttr.Vitality, mod.Modifier, reason);
                    break;
                case GenericAttribute.Agility:
                    Adjust(ModAttr.Agility, mod.Modifier, reason);
                    break;
                case GenericAttribute.Luck:
                    Adjust(ModAttr.Luck, mod.Modifier, reason);
                    break;
                default:
                    break;
            }
        }

        public void Adjust(ElementDamageResistance dam, ItemType type, string reason, bool bResist)
        {
            switch (type)
            {
                case ItemType.OneHandMelee:
                case ItemType.TwoHandMelee:
                    Adjust(ModAttr.MeleeDamage, dam.DamageValue, reason);
                    break;
                case ItemType.Missile:
                    Adjust(ModAttr.RangedDamage, dam.DamageValue, reason);
                    break;
                default:
                    break;
            }

            // Weapons give the elemental resistance as well as the damage type (in MM3, not in MM4/5)
            if (bResist)
            {
                switch (dam.ResistElement)
                {
                    case GenericResistanceFlags.Fire:
                        Adjust(ModAttr.Fire, dam.ResistValue, reason);
                        break;
                    case GenericResistanceFlags.Magic:
                        Adjust(ModAttr.Magic, dam.ResistValue, reason);
                        break;
                    case GenericResistanceFlags.Cold:
                        Adjust(ModAttr.Cold, dam.ResistValue, reason);
                        break;
                    case GenericResistanceFlags.Electricity:
                        Adjust(ModAttr.Electricity, dam.ResistValue, reason);
                        break;
                    case GenericResistanceFlags.Acid:
                        Adjust(ModAttr.Acid, dam.ResistValue, reason);
                        break;
                    case GenericResistanceFlags.Poison:
                        Adjust(ModAttr.Poison, dam.ResistValue, reason);
                        break;
                    case GenericResistanceFlags.Sleep:
                        Adjust(ModAttr.Sleep, dam.ResistValue, reason);
                        break;
                    case GenericResistanceFlags.Paralyze:
                        Adjust(ModAttr.Paralyze, dam.ResistValue, reason);
                        break;
                    case GenericResistanceFlags.Energy:
                        Adjust(ModAttr.Energy, dam.ResistValue, reason);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public static class ByteSaver
    {
        public static Dictionary<int, byte[]> data = new Dictionary<int, byte[]>(128);

        public static void AddMap(int iIndex, byte[] bytes)
        {
            if (data.ContainsKey(iIndex))
                return;

            data.Add(iIndex, bytes);
        }

        public static byte[] GetBytes()
        {
            byte[] zero = new byte[] {0,0};

            MemoryStream ms = new MemoryStream();
            MemoryStream msOffsets = new MemoryStream();

            for (int i = 0; i < 128; i++)
            {
                if (data.ContainsKey(i))
                {
                    UInt16 offset = (UInt16) (256 + ms.Length);
                    msOffsets.Write(BitConverter.GetBytes(offset), 0, 2);
                    ms.Write(data[i], 0, data[i].Length);
                }
                else
                    msOffsets.Write(zero, 0, 2);
            }

            ms.WriteTo(msOffsets);
            return msOffsets.ToArray();
        }

        public static string GetString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, byte[]> kvp in data)
                sb.AppendFormat("{0}: {1}\r\n", kvp.Key, Global.ByteString(kvp.Value));

            return sb.ToString();
        }
    }

    public class WatchedFile : IDisposable
    {
        private FileSystemWatcher m_watcher;

        private string m_strFilePath = String.Empty;
        private FileStream m_file = null;
        private byte[] m_bytes = null;
        private bool m_bFileChanged = true;

        public bool UserCanceled { get; set; }

        public string FileName { get { return m_strFilePath; } }

        public bool IsValid
        {
            get
            {
                return (m_file != null && m_file.CanRead);
            }
        }

        public long Length { get { return m_file == null ? -1 : new FileInfo(m_strFilePath).Length; } }

        public void ForceRead()
        {
            m_bFileChanged = true;
        }

        public byte[] GetBytes()
        {
            if (m_file == null)
                return new byte[0];

            if (!m_file.CanRead)
            {
                Close();
                return new byte[0];
            }

            if (!m_bFileChanged && m_bytes != null)
                return m_bytes;

            m_bFileChanged = false;

            m_file.Seek(0, SeekOrigin.Begin);
            if (m_bytes == null || m_bytes.Length != m_file.Length)
                m_bytes = new byte[m_file.Length];
            m_file.Read(m_bytes, 0, m_bytes.Length);

            return m_bytes;
        }

        public void Flush()
        {
            if (m_file == null)
                return;
            m_file.Flush(true);
        }

        public bool WriteBytes(uint offset, byte[] bytes)
        {
            if (m_file == null)
                return false;

            if (!m_file.CanWrite)
                return false;

            if (m_file.Length < (offset + bytes.Length))
                return false;

            m_file.Seek(offset, SeekOrigin.Begin);
            m_file.Write(bytes, 0, bytes.Length);
            return true;
        }

        private void StopWatcher()
        {
            if (m_watcher != null)
                m_watcher = null;
        }

        private void StartWatcher()
        {
            m_bFileChanged = true;
            m_watcher = new FileSystemWatcher(Path.GetDirectoryName(m_strFilePath), Path.GetFileName(m_strFilePath));
            m_watcher.EnableRaisingEvents = true;
            m_watcher.Changed += OnFileChanged;
        }

        void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            m_bFileChanged = true;
        }

        public bool CanWrite { get { return m_file == null ? false : m_file.CanWrite; } }

        public WatchedFile(string strFile, bool bWatch = true, bool bWrite = false)
        {
            OpenFile(strFile, bWatch, bWrite);
        }

        private bool OpenFile(string strFile, bool bWatch, bool bWrite)
        {
            if (File.Exists(strFile))
            {
                try
                {
                    m_file = File.Open(strFile, FileMode.Open, bWrite ? FileAccess.ReadWrite : FileAccess.Read, FileShare.ReadWrite);
                    m_strFilePath = strFile;
                    if (bWatch)
                        StartWatcher();
                    return true;
                }
                catch (Exception)
                {
                }
            }
            return false;
        }

        public WatchedFile(string strFile, string strPrompt, bool bWatch = true, bool bWrite = false)
        {
            if (OpenFile(strFile, bWatch, bWrite))
                return;

            UserCanceled = false;
            bool bRetry = false;
            if (String.IsNullOrWhiteSpace(strPrompt))
                strPrompt = "Please select a file.";

            do
            {
                if (bRetry || m_file == null)
                {
                    OpenFileDialog ofd = new OpenFileDialog();
                    ofd.FileName = strFile;
                    if (ofd.ShowDialog() != DialogResult.OK)
                    {
                        UserCanceled = true;
                        return;
                    }
                    strFile = ofd.FileName;
                }
                try
                {
                    m_file = File.Open(strFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    bRetry = false;
                    if (bWatch)
                        StartWatcher();
                }
                catch (Exception ex)
                {
                    if (UserCanceled)
                        return;
                    MessageBox.Show(String.Format("Could not open file \"{0}\" for reading: {1}.\r\n\r\nPlease select another file.", strFile, ex.Message), "File Open Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            } while (bRetry);
        }

        public void Close()
        {
            StopWatcher();
            if (m_file != null)
            {
                m_file.Close();
                m_file = null;
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
                if (m_watcher != null)
                    m_watcher.Dispose();
                Close();
            }
        }
    }

    public class CheatOffsets
    {
        public int[] Values;
        public object Tag;

        public CheatOffsets(int[] values)
        {
            Values = values;
        }

        public CheatOffsets(int[] values, object tag)
        {
            Values = values;
            Tag = tag;
        }

        public int Length { get { return Values.Length; } }

        public CheatOffsets Subset(int iStart, int iLength)
        {
            int[] newValues = new int[iLength];
            Array.Copy(Values, iStart, newValues, 0, iLength);
            return newValues;
        }

        public static implicit operator int[](CheatOffsets co) { return co.Values; }
        public static implicit operator CheatOffsets(int[] ints) { return new CheatOffsets(ints); }

        public int this[int key]
        {
            get { return Values[key]; }
            set { Values[key] = value; }
        }
    }

    public class OffsetFinder : IDisposable
    {
        private MemoryHacker m_hacker;
        private byte[] m_bytes;
        private System.Threading.Timer m_timer;

        private byte[] m_lastBytesSearched = null;

        public void Pause()
        {
            m_timer.Change(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
        }

        public void Go()
        {
            m_timer.Change(500, 500);
        }

        public void TimerCallback(object state)
        {
            if (m_hacker == null || !m_hacker.IsValid)
                return;

            Pause();

            byte[] bytes = m_hacker.OffsetSearchBytes;
            if (Global.Compare(m_lastBytesSearched, bytes))
            {
                Go();
                return;
            }

            m_lastBytesSearched = bytes;

            int[] offsets = Global.FindBytes(m_bytes, bytes);
            if (offsets.Length == 0)
            {
                Go();
                return;
            }

            StringBuilder sb = new StringBuilder();
            foreach(long offset in offsets)
                sb.AppendFormat("0x{0:X5}, ", offset);
            Global.Trim(sb);

            Global.Log("{0} // Map {1}", sb.ToString(), m_hacker.GetMapTitle(m_hacker.GetCurrentMapIndex()).ToString());

            Go();
        }

        public OffsetFinder(MemoryHacker hacker, byte[] bytes)
        {
            m_hacker = hacker;
            m_bytes = bytes;
            m_timer = new System.Threading.Timer(TimerCallback, null, 500, 500);
        }

        public void Stop()
        {
            Pause();
            m_timer.Dispose();
            m_timer = null;
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
                if (m_timer != null)
                    m_timer.Dispose();
            }
        }
    }

    public class QuestTotals
    {
        public int All;
        public int Completed;

        public QuestTotals(int all, int completed)
        {
            All = all;
            Completed = completed;
        }
    }

    public interface ISplitters
    {
        int[] Splitters { get; set; }
    }

    public delegate void VoidHandler(object sender, EventArgs e);

    public interface IMain
    {
        MemoryHacker Hacker { get; }
        MainForm Main { get; }
        bool ShowShopInventories { get; set;  }
        bool ShowingCurrentMap { get; }
        ViewPartyForm PartyForm { get; }
        MapSheet CurrentSheet { get; }
        MapBook CurrentBook { get; }
        QuickRefForm QuickRefForm { get; }
        bool InOptions { get; }
        GameNames Game { get; }
        bool ManualSpellWindowClose { get; set; }
        bool SetFlaggedQuests(string[] quests);
        bool ShuttingDown { get; }
        event VoidHandler OptionsChanged;

        bool CureAll(int iAddress, bool bSilent);
        void ProcessMessage(ref Message m, Form form);
        void SetCursor(Point pt);
        void SetUnsaved(bool bUnsaved, bool bCheckLabels);
        void UpdateEncounterMenu();
        void InvalidateMonsters();
        void SetDirty();
        void SetDirty(Point ptGame);
        void SetGridDirty(Point ptGrid);
        void ShowQuickRefNext();
        bool SelectCharacter(int iChar);
        void AddNoteToMap(Point pt, string strSymbol, string strText);
        void UpdateBookExtraData();
        void ShowQuests(BaseCharacter character);
        void CopyLocationText(Point pt, MapSheet sheet);
        string GetLocationText(Point pt, int index, string strTitle);
        string GetMapName(int iIndex);
        int GetSelectedCharacterAddress();
        BaseCharacter GetSelectedCharacter();
        Rectangle TranslateToGameMap(Rectangle rc, MapSheet sheet);
        Point TranslateToGameMap(Point pt, MapSheet sheet);
        Rectangle TranslateToInternalMap(Rectangle rc, MapSheet sheet);
        Point TranslateToInternalMap(Point pt, MapSheet sheet);
        Rectangle[] TranslateToInternalMap(Rectangle[] rects, MapSheet sheet);
        PointF TranslateToGameMap(PointF pt, MapSheet sheet);
        PointF TranslateToInternalMap(PointF pt, MapSheet sheet);
        bool GotoSheet(MapSheet sheet);
        bool GotoSheet(int iMapIndex);
        bool SquareIsVisible(Point ptLocation);
        bool SquareIsVisible(Point ptLocation, Point ptFrom, int iVisibleRange);
        bool HideMonsters(List<Monster> monsters);
        void SelectSheetByPartialName(string strName, bool bNextIfCurrent, bool bReverse);
        void SuspendActivation();
        void ResumeActivation();
        void SetCurrentSheetLabels(MapLabels labels);
        void SetCurrentSheetSelectedLabels(HashSet<PointF> labels);
        Point GetSquareLocationAtMouse(bool bFixRange);
        void CancelSelection();
        bool RunMenuCommand(string strMenu, int iNumber);
    }

    public enum Toggle
    {
        Reset,
        Set,
        Toggle
    }

    public class Proximity
    {
        const int Maximum = 64;
        public int Horiz;
        public int Vert;
        public bool UseSimple = false;

        public Proximity(int iHoriz, int iVert, bool bUseSimple = false)
        {
            Horiz = iHoriz;
            Vert = iVert;
            UseSimple = bUseSimple;
        }

        public static int SimpleDistance(Point pt1, Point pt2)
        {
            return Math.Max(Math.Abs(pt1.X - pt2.X), Math.Abs(pt1.Y - pt2.Y));
        }

        public Proximity(Point pt1, Point pt2)
        {
            Horiz = Math.Abs(pt1.X - pt2.X);
            Vert = Math.Abs(pt1.Y - pt2.Y);
        }

        public Proximity(int i)
        {
            Horiz = i;
            Vert = i;
            UseSimple = true;
        }

        public int Simple { get { return Math.Max(Horiz, Vert); } }
        public double Full 
        {
            get 
            {
                if (Horiz > Maximum || Vert > Maximum)
                    return Maximum;
                return Math.Sqrt(Horiz * Horiz + Vert * Vert);
            }
        }
    }

    public class FindBox
    {
        public delegate void FindDelegate(string strSearch, bool bForward, bool bIncludeCurrent, object param, Control ctrlFocus);

        private SplitContainer m_splitContainer;
        private TextBox m_textBox;
        private FindDelegate m_findFunction;
        private object m_param;
        private TabControl m_tabControl = null;
        private TabPage m_tabPage = null;

        public FindBox(SplitContainer sc, TextBox tb, FindDelegate fnFind, object param, TabControl tabControl = null, TabPage tabPage = null)
        {
            m_splitContainer = sc;
            m_textBox = tb;
            tb.KeyDown += tb_KeyDown;
            tb.TextChanged += tb_TextChanged;
            m_findFunction = fnFind;
            m_param = param;
            m_tabControl = tabControl;
            m_tabPage = tabPage;
        }

        public bool Focused { get { return m_textBox.Visible && m_textBox.Focused; } }

        void tb_TextChanged(object sender, EventArgs e)
        {
            Next(sender, new BoolHandlerEventArgs(true));
        }

        public bool Visible { get { return !m_splitContainer.Panel2Collapsed; } }

        public void ShowFindBox()
        {
            if (m_splitContainer.Panel2Collapsed)
                m_textBox.Text = "";
            m_splitContainer.Panel2Collapsed = false;
            m_splitContainer.SplitterDistance = m_splitContainer.Height - m_splitContainer.Panel2MinSize;
            m_textBox.Focus();
            m_textBox.SelectAll();
        }

        public void HideFindBox()
        {
            m_splitContainer.Panel2Collapsed = true;
        }

        void tb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers != Keys.None)
                return;

            switch (e.KeyCode)
            {
                case Keys.Escape:
                    HideFindBox();
                    e.Handled = true;
                    break;
                case Keys.Enter:
                    Next(sender, new BoolHandlerEventArgs(false));
                    e.Handled = true;
                    break;
                default:
                    break;
            }
        }

        public void Find(object sender, EventArgs e)
        {
            if (m_tabControl != null && m_tabControl.SelectedTab != m_tabPage)
                return; // Don't show the find box if we aren't on the right tab

            ShowFindBox();
        }

        public void Previous(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(m_textBox.Text))
                return;

            m_findFunction(m_textBox.Text, false, false, m_param, m_textBox.Focused ? m_textBox : null);
        }

        public void Next(object sender, BoolHandlerEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(m_textBox.Text))
                return;

            m_findFunction(m_textBox.Text, true, e.BoolValue, m_param, m_textBox.Focused ? m_textBox : null);
        }

        public static void ListViewFindFunction(string strSearch, bool bForward, bool bIncludeCurrent, object param, Control ctrlFocus)
        {
            ListView lv = param as ListView;
            if (lv == null)
                return;

            int iSelected = lv.SelectedItems.Count > 0 ? lv.SelectedItems[0].Index : -1;
            int iDelta = bForward ? 1 : -1;

            int iCurrent = iSelected + (bIncludeCurrent ? 0 : iDelta);
            if (iCurrent == -1 && iDelta == 1)
                iCurrent = 0;

            bool bWrap = (iSelected != -1);

            while (iCurrent >= 0 && iCurrent < lv.Items.Count || bWrap)
            {
                if ((iCurrent < 0 || iCurrent >= lv.Items.Count) && bWrap)
                {
                    bWrap = false;
                    iCurrent = bForward ? 0 : lv.Items.Count - 1;
                }

                if (iCurrent >= 0 && lv.Items.Count > iCurrent && Global.AnySubItemContains(lv.Items[iCurrent], strSearch))
                {
                    Global.DeselectAll(lv);
                    lv.Items[iCurrent].Selected = true;
                    lv.EnsureVisible(iCurrent);
                    return;
                }
                iCurrent += iDelta;
            }
        }

        public static void MultiSpellRefFindFunction(string strSearch, bool bForward, bool bIncludeCurrent, object param, Control ctrlFocus)
        {
            SpellRefPanel[] srpList = param as SpellRefPanel[];
            if (srpList == null || srpList.Length == 0)
                return;

            ListView[] views = new ListView[srpList.Length];
            for(int i = 0; i < srpList.Length; i++)
                views[i] = srpList[i].SpellListView;

            MultiListViewFindFunction(strSearch, bForward, bIncludeCurrent, views, ctrlFocus);

            foreach (SpellRefPanel srp in srpList)
            {
                if (srp.SpellListView.SelectedItems.Count > 0 && srp.ParentTab != null)
                    srp.ParentTabControl.SelectedTab = srp.ParentTab;
            }

            if (ctrlFocus != null)
                ctrlFocus.Focus();
        }

        public static void MultiListViewFindFunction(string strSearch, bool bForward, bool bIncludeCurrent, object param, Control ctrlFocus)
        {
            ListView[] views = param as ListView[];
            if (views == null || views.Length == 0)
                return;

            int iCurrentView = 0;
            for(int i = 0; i < views.Length; i++)
            {
                if (views[i].Visible)
                {
                    iCurrentView = i;
                    break;
                }
            }

            int iFirstView = iCurrentView;

            int iSelected = views[iCurrentView].SelectedItems.Count > 0 ? views[iCurrentView].SelectedItems[0].Index : -1;
            int iDelta = bForward ? 1 : -1;

            int iCurrent = iSelected + (bIncludeCurrent ? 0 : iDelta);
            if (iCurrent == -1 && iDelta == 1)
                iCurrent = 0;

            bool bWrap = (iSelected != -1);

            while (iCurrent >= 0 && iCurrent < views[iCurrentView].Items.Count || bWrap)
            {
                if ((iCurrent < 0 || iCurrent >= views[iCurrentView].Items.Count) && bWrap)
                {
                    iCurrentView = iCurrentView + (bForward ? 1 : -1);
                    if (iCurrentView >= views.Length)
                        iCurrentView = 0;
                    if (iCurrentView < 0)
                        iCurrentView = views.Length - 1;

                    if (iCurrentView == iFirstView)
                        bWrap = false;

                    iCurrent = bForward ? 0 : views[iCurrentView].Items.Count - 1;
                }

                if (iCurrent >= 0 && views[iCurrentView].Items.Count > iCurrent && Global.AnySubItemContains(views[iCurrentView].Items[iCurrent], strSearch))
                {
                    foreach(ListView view in views)
                        Global.DeselectAll(view);
                    views[iCurrentView].Items[iCurrent].Selected = true;
                    views[iCurrentView].EnsureVisible(iCurrent);
                    return;
                }
                iCurrent += iDelta;
            }
        }

        private static void Find(TreeView tv, string str, TreeNode start = null, bool bForward = true, bool bIncludeCurrent = false)
        {
            string strFind = str.ToLower();
            if (bIncludeCurrent && start != null && start.Text.ToLower().Contains(strFind))
            {
                tv.SelectedNode = start;
                return;
            }

            int iSelected = -1;
            List<TreeNode> nodes = GetFlatTree(tv, start, out iSelected);
            int iDelta = bForward ? 1 : -1;

            int iCurrent = iSelected + iDelta;
            bool bWrap = (start != null);

            while (iCurrent >= 0 && iCurrent < nodes.Count || bWrap)
            {
                if ((iCurrent < 0 || iCurrent >= nodes.Count) && bWrap)
                {
                    bWrap = false;
                    iCurrent = bForward ? 0 : nodes.Count - 1;
                }

                if (nodes[iCurrent].Text.ToLower().Contains(strFind))
                {
                    tv.SelectedNode = nodes[iCurrent];
                    return;
                }
                iCurrent += iDelta;
            }
        }

        public static List<TreeNode> GetFlatTree(TreeView tv, TreeNode tnSelected, out int iSelected)
        {
            iSelected = -1;
            List<TreeNode> nodes = new List<TreeNode>();
            GetFlatTree(tv.Nodes, nodes, tnSelected, ref iSelected);
            return nodes;
        }

        public static void GetFlatTree(TreeNodeCollection tnc, List<TreeNode> nodes, TreeNode tnSelected, ref int iSelected)
        {
            if (tnc == null)
                return;

            foreach (TreeNode node in tnc)
            {
                if (node == tnSelected)
                    iSelected = nodes.Count;
                nodes.Add(node);
                GetFlatTree(node.Nodes, nodes, tnSelected, ref iSelected);
            }
        }

        public static void TreeViewFindFunction(string strSearch, bool bForward, bool bIncludeCurrent, object param, Control ctrlFocus)
        {
            TreeView tv = param as TreeView;
            if (tv == null)
                return;

            Find(tv, strSearch, tv.SelectedNode, bForward, bIncludeCurrent);
        }
    }

    public class StatModifier
    {
        public int Stat;
        public int Value;
        public int Next;

        public StatModifier(int stat, int val, int next)
        {
            Stat = stat;
            Value = val;
            Next = next;
        }

        public static StatModifier Zero { get { return new StatModifier(0, 0, -1); } }

        public static StatModifier FromTable(int val, PrimaryStat stat, params int[] bonusTable)
        {
            for (int i = 0; i < bonusTable.Length - 1; i += 2)
            {
                if (val < bonusTable[i])
                    return new StatModifier(val, bonusTable[i + 1], bonusTable[i]);
            }
            return new StatModifier(val, bonusTable[bonusTable.Length - 1], -1);
        }

        public static StatModifier FromTablePlus(int val, PrimaryStat stat, int iFinalStep, int iFinalBonus, params int[] bonusTable)
        {
            // Creates a StatModifier that offers a constant bonus for a constant increment past the end
            // of the table.  For example, if iFinalStep is 2 and iFinalBonus is 5, the StatModifier will gain 5 for each 2 values
            // after the last specific one in the table.
            for (int i = 0; i < bonusTable.Length - 1; i += 2)
            {
                if (val < bonusTable[i])
                    return new StatModifier(val, bonusTable[i + 1], bonusTable[i]);
            }
            int iSteps = val - (bonusTable[bonusTable.Length - 2] / iFinalStep) + 1;
            int iTotalBonus = bonusTable[bonusTable.Length - 1] + (iSteps * iFinalBonus);
            return new StatModifier(val, iTotalBonus, bonusTable[bonusTable.Length - 2] + (iSteps * iFinalStep));
        }

        public string TipString(string strFormat, bool bPlus = true)
        {
            return String.Format(strFormat, Stat, bPlus ? Global.AddPlus(Value) : Value.ToString(), NextString);
        }

        public string TipString(string strFormat, double newValue)
        {
            return String.Format(strFormat, Stat, Global.AddPlus(newValue), NextString);
        }

        public string NextString
        {
            get
            {
                if (Next == -1)
                    return "maximum bonus";
                return String.Format("next bonus at {0}", Next);
            }
        }
    }

    public class Ranges
    {
        public int[] Values;

        public Ranges(params int[] values)
        {
            Values = values;
        }

        public string LevelString
        {
            get
            {
                if (Values == null || Values.Length < 1)
                    return String.Empty;
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Level {0}", Values[0]);
                for (int i = 1; i < Values.Length; i++)
                {
                    if (Values[i] == Values[i - 1] + 1)
                    {
                        if (sb[sb.Length - 1] != '-')
                            sb.Append("-");
                        continue;
                    }
                    else if (sb[sb.Length - 1] == '-')
                        sb.AppendFormat("{0}", Values[i - 1]);
                    sb.AppendFormat(",{0}", Values[i]);
                }
                if (sb[sb.Length - 1] == '-')
                    sb.AppendFormat("{0}", Values[Values.Length - 1]);
                return sb.ToString();
            }
        }

        public static Ranges Empty { get { return new Ranges(); } }
    }

    public class BitmapSet
    {
        public Bitmap[] Bitmaps;

        public BitmapSet(params Bitmap[] bitmaps)
        {
            Bitmaps = bitmaps;
        }

        public Bitmap NearestSize(int iWidth)
        {
            if (Bitmaps == null || Bitmaps.Length < 1)
                return null;

            Bitmap bmpClosest = null;
            foreach (Bitmap bmp in Bitmaps)
            {
                if (bmpClosest == null || (bmpClosest.Width < bmp.Width && iWidth >= bmp.Width))
                    bmpClosest = bmp;
            }
            return bmpClosest;
        }
    }

    public class MapLineInfo
    {
        public Color Color;
        public DashStyle Pattern;
        public int Width;
        public int AltWidth;

        public static MapLineInfo BlackLine2 = new MapLineInfo(Color.Black, DashStyle.Solid, 2);
        public static MapLineInfo RedLine2 = new MapLineInfo(Color.Red, DashStyle.Solid, 2);
        public static MapLineInfo BlueLine2 = new MapLineInfo(Color.Blue, DashStyle.Solid, 2);
        public static MapLineInfo BlackDot2 = new MapLineInfo(Color.Black, DashStyle.Dot, 2);
        public static MapLineInfo BlueDot2 = new MapLineInfo(Color.Blue, DashStyle.Dot, 2);

        public MapLineInfo(Color color, DashStyle pattern, int width)
        {
            Color = color;
            Pattern = pattern;
            Width = width;
            AltWidth = 0;
        }

        public bool SameColorAndPattern(MapLineInfo infoCompare)
        {
            if (infoCompare == null)
                return false;
            return Color.ToArgb() == infoCompare.Color.ToArgb() && Pattern == infoCompare.Pattern;
        }

        public override int GetHashCode()
        {
 	         return Color.ToArgb() | (int) Pattern | (Width << 8) | (AltWidth << 16);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MapLineInfo))
                return false;
            MapLineInfo compare = (MapLineInfo) obj;
            return Color.ToArgb() == compare.Color.ToArgb() && Pattern == compare.Pattern && Width == compare.Width && AltWidth == compare.AltWidth;
        }

        public bool Check(bool bIsMatch, int argb, int width, DashStyle style)
        {
            return (Color.ToArgb() == argb && Pattern == style && Width == width) ? bIsMatch : !bIsMatch;
        }

        public override string ToString()
        {
            string strPattern = Pattern == DashStyle.Dot ? "Dotted" : Pattern == DashStyle.Solid ? "Solid" : "Other";
            int iColor = Color.ToArgb();
            string strColor = iColor == Color.Black.ToArgb() ? "Black" : iColor == Global.DefaultGridLineInfo.Color.ToArgb() ? "Grid" : String.Format("{0:X8}", iColor);
            return String.Format("{0} {1}, width {2}", strPattern, strColor, Width);
        }

        public void CopyFrom(MapLineInfo copy)
        {
            Color = copy.Color;
            Pattern = copy.Pattern;
            Width = copy.Width;
            AltWidth = copy.AltWidth;
        }
    }

    public class ColorPattern
    {
        public Color Color;
        public Color BackColor;
        public HatchStyle Pattern;

        public ColorPattern(Color color, HatchStyle pattern)
        {
            Color = color;
            BackColor = Color.White;
            Pattern = pattern;
        }

        public ColorPattern(Color color, HatchStyle pattern, Color backColor)
        {
            Color = color;
            BackColor = backColor;
            Pattern = pattern;
        }

        public static ColorPattern Empty { get { return new ColorPattern(Color.White, HatchStyle.Percent90); } }

        public Brush GetBrush(int iAlpha)
        {
            if (Pattern == HatchStyle.Percent90)
                return new SolidBrush(Color.FromArgb(iAlpha, Color));
            return new HatchBrush(Pattern, Color.FromArgb(iAlpha, Color), Color.FromArgb(iAlpha, BackColor));
        }

        public override int GetHashCode()
        {
            return Color.ToArgb() ^ BackColor.ToArgb() | ((int)Pattern << 24);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ColorPattern))
                return false;
            ColorPattern compare = (ColorPattern)obj;
            return Color.ToArgb() == compare.Color.ToArgb() && BackColor.ToArgb() == compare.BackColor.ToArgb() && Pattern == compare.Pattern;
        }
    }

    public class RepeatableTip : IDisposable
    {
        public const string SelectBlocks = "The currently selected block color and pattern.\r\nLeft-click to change to any style, right-click and hold to change to a particular quick-select style.";
        public const string SelectLines = "The currently selected line color and pattern.\r\nLeft-click to change to any style, right-click and hold to change to a particular quick-select style.";
        public const string NoteSymbol = "This text (maximum 2 characters) is displayed on the map directly in the grid square.";
        public const string NoteColor = "The color that will be used when displaying the note symbol on the map.";
        public const string Unicode = "Select this to bring up a dialog of common Unicode drawing characters.\r\nClick on any of them to use it as the note symbol.";
        public const string Icon = "Right-click and hold to select an icon for use on the map.";
        public const string Title = "Changes the title of this map on the Maps menu.";
        public const string Party = "Location of the party in the currently-running game, if any.";
        public const string Cursor = "Coordinates of the square under the mouse cursor.";
        public const string Selection = "Upper-left corner of the selection rectangle (and width, height)";
        public const string Mode = "The currently selected edit mode";

        public Control Ctrl;
        public ToolTip Tip;
        public string Text;
        public int TipDelay = 1200;
        public int TipDuration = 10000;

        public RepeatableTip(Control ctrl, string str)
        {
            Ctrl = ctrl;
            Tip = new ToolTip();
            Text = str;

            SetTip();
        }

        public void SetTip()
        {
            Tip.Hide(Ctrl);
            Tip.InitialDelay = TipDelay;
            Tip.AutoPopDelay = TipDuration;
            Tip.SetToolTip(Ctrl, Text);
            Ctrl.MouseEnter += ctrl_MouseEnter;
        }

        public void ResetTip()
        {
            Tip.Hide(Ctrl);
            Tip.InitialDelay = TipDelay;
            Tip.AutoPopDelay = TipDuration;
            Tip.SetToolTip(Ctrl, Text);
        }

        void ctrl_MouseEnter(object sender, EventArgs e)
        {
            ResetTip();
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
                if (Tip != null)
                    Tip.Dispose();
            }
        }
    }

    public enum NoScanReason
    {
        None,
        Unknown,
        NotRunning,
        NoParty,
        OptionsOpen
    }

    public enum ToolBarCapture
    {
        None,
        Block,
        Line,
        Icon,
        Note
    }

    public class BlockModeItem
    {
        public BlockMode Mode;

        public static string Name(BlockMode mode)
        {
            switch (mode)
            {
                case BlockMode.Block: return "Block";
                case BlockMode.Edit: return "Edit";
                case BlockMode.Hybrid: return "Hybrid";
                case BlockMode.Keyboard: return "Key";
                case BlockMode.Line: return "Line";
                case BlockMode.Notes: return "Notes";
                case BlockMode.Play: return "Play";
                case BlockMode.Fill: return "Fill";
                default: return "None";
            }
        }

        public BlockModeItem(BlockMode mode)
        {
            Mode = mode;
        }

        public override string ToString() { return Name(Mode); }
    }

    public class ImageCrop
    {
        public String File;
        public Rectangle Crop;

        public ImageCrop(String strFile, Rectangle rcCrop)
        {
            File = strFile;
            Crop = rcCrop;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ImageCrop))
                return false;
            ImageCrop ic = (ImageCrop)obj;
            return ic.File == File && ic.Crop == Crop;
        }

        public override int GetHashCode()
        {
 	         return File.GetHashCode() ^ Crop.GetHashCode();
        }
    }

    public class DatedBitmap : IComparable<DatedBitmap>, IDisposable
    {
        public DateTime Date;
        public Bitmap Bitmap;
        public object Tag;

        public DatedBitmap(Bitmap bmp)
        {
            Date = DateTime.Now;
            Bitmap = bmp;
            Tag = null;
        }

        public DatedBitmap(Bitmap bmp, object tag)
        {
            Date = DateTime.Now;
            Bitmap = bmp;
            Tag = tag;
        }

        // Size is in pixels, not bytes
        public long Size { get { return Bitmap.Width * Bitmap.Height; } }

        public int CompareTo(DatedBitmap db)
        {
            return Date.CompareTo(db.Date);
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
                if (Bitmap != null)
                    Bitmap.Dispose();
            }
        }
    }
    
    public class IconCacheInfo
    {
        public IconName Name;
        public Direction Orientation;
        public Size Size;

        public IconCacheInfo(MapIcon icon, Size size)
        {
            Name = icon.Name;
            Orientation = icon.Orientation;
            Size = size;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IconCacheInfo))
                return false;
            IconCacheInfo test = (IconCacheInfo)obj;
            return (test.Name == Name && test.Orientation == Orientation && test.Size == Size);
        }

        public override int GetHashCode()
        {
            return (int)Name ^ ((int)Orientation << 8) ^ Size.GetHashCode();
        }
    }

    public class IconCache
    {
        private const int MaxSize = 1000;
        private Dictionary<IconCacheInfo, Bitmap> m_dictBitmaps = null;

        public IconCache()
        {
            Clear();
        }

        public void Clear()
        {
            if (m_dictBitmaps != null)
            {
                foreach(Bitmap bmp in m_dictBitmaps.Values)
                    bmp.Dispose();
            }
            m_dictBitmaps = new Dictionary<IconCacheInfo,Bitmap>();
        }

        private void CheckSize()
        {
            if (m_dictBitmaps.Count > MaxSize)
                Clear();    // Don't let the cache get too crazy;
        }

        public Bitmap GetIconBitmap(MapIcon icon, Size sz)
        {
            IconCacheInfo info = new IconCacheInfo(icon, sz);
            if (m_dictBitmaps.ContainsKey(info))
                return m_dictBitmaps[info];
            CheckSize();
            Bitmap bmpIcon = icon.GetSizedIcon(sz).ToBitmap();
            bmpIcon.RotateFlip(icon.RotateCommand);
            m_dictBitmaps.Add(info, bmpIcon);
            return bmpIcon;
        }
    }

    public class ImageCache
    {
        private Dictionary<string, DatedBitmap> m_dictFull = null;
        private Dictionary<ImageCrop, DatedBitmap> m_dictCrops = null;
        private long m_iFullSize = 0;
        private long m_iCropSize = 0;

        public long FullSize { get { return m_iFullSize; } }
        public long CropSize { get { return m_iCropSize; } }

        public ImageCache()
        {
            Clear();
        }

        public void Clear()
        {
            ClearFull();
            ClearCrops();
        }

        public void ClearFull()
        {
            if (m_dictFull != null)
            {
                foreach (DatedBitmap bmp in m_dictFull.Values)
                    bmp.Dispose();
            }
            m_dictFull = new Dictionary<string, DatedBitmap>();
            m_iFullSize = 0;
        }

        public void ClearCrops()
        {
            if (m_dictCrops != null)
            {
                foreach (DatedBitmap bmp in m_dictCrops.Values)
                    bmp.Dispose();
            }
            m_dictCrops = new Dictionary<ImageCrop, DatedBitmap>();
            m_iCropSize = 0;
        }

        public void CheckSize()
        {
            List<DatedBitmap> list = null;
            if (m_iFullSize > Properties.Settings.Default.BitmapCacheFull && m_dictFull.Count > 1)  // Don't erase the only image
            {
                list = new List<DatedBitmap>();
                foreach (KeyValuePair<string, DatedBitmap> pair in m_dictFull)
                    list.Add(pair.Value);
                list.Sort();
                int iRemoveUntil = 0;
                while (m_iFullSize > (Properties.Settings.Default.BitmapCacheFull * 4 / 5))
                {
                    if (iRemoveUntil >= list.Count)
                    {
                        // Something's wrong; reset the size
                        ClearFull();
                        break;
                    }
                    m_iFullSize -= list[iRemoveUntil++].Size;
                }
                for (int i = 0; i < iRemoveUntil; i++)
                {
                    m_dictFull.Remove(list[i].Tag as string);
                    list[i].Dispose();
                }
                list.RemoveRange(0, iRemoveUntil);
            }

            if (m_iCropSize > Properties.Settings.Default.BitmapCacheCrop && m_dictCrops.Count > 1)  // Don't erase the only image
            {
                list = new List<DatedBitmap>();
                foreach (KeyValuePair<ImageCrop, DatedBitmap> pair in m_dictCrops)
                    list.Add(pair.Value);
                list.Sort();
                int iRemoveUntil = 0;
                while (m_iCropSize > (Properties.Settings.Default.BitmapCacheCrop * 4 / 5))
                {
                    if (iRemoveUntil >= list.Count)
                    {
                        // Something's wrong; reset the size
                        ClearCrops();
                        break;
                    }
                    m_iCropSize -= list[iRemoveUntil++].Size;
                }
                for(int i = 0; i < iRemoveUntil; i++)
                {
                    m_dictCrops.Remove(list[i].Tag as ImageCrop);
                    list[i].Dispose();
                }
                list.RemoveRange(0, iRemoveUntil);
            }
        }

        private void AddFullBitmap(string strFile, Bitmap bmp)
        {
            CheckSize();
            m_dictFull.Add(strFile, new DatedBitmap(bmp, strFile));
            m_iFullSize += bmp.Width * bmp.Height;
        }

        private void AddCropBitmap(ImageCrop ic, Bitmap bmpCrop)
        {
            CheckSize();
            m_dictCrops.Add(ic, new DatedBitmap(bmpCrop, ic));
            m_iCropSize += bmpCrop.Width * bmpCrop.Height;
        }

        public Bitmap GetFullImage(string strFile, string strDefaultDirectory = null)
        {
            if (!File.Exists(strFile) && Directory.Exists(strDefaultDirectory))
                strFile = Path.Combine(strDefaultDirectory, strFile);

            if (!m_dictFull.ContainsKey(strFile))
            {
                if (File.Exists(strFile))
                {
                    using (FileStream fs = new FileStream(strFile, FileMode.Open, FileAccess.Read))
                    {
                        AddFullBitmap(strFile, Bitmap.FromStream(fs) as Bitmap);
                    }
                }
                else
                    return null;
            }

            return m_dictFull[strFile].Bitmap;
        }

        public Bitmap GetCrop(string strFile, Rectangle rcCrop, string strDefaultDirectory = null)
        {
            ImageCrop ic = new ImageCrop(strFile, rcCrop);
            if (m_dictCrops.ContainsKey(ic))
                return m_dictCrops[ic].Bitmap;
            Bitmap bmp = GetFullImage(strFile, strDefaultDirectory);
            if (bmp == null || rcCrop.Left < 0 || rcCrop.Top < 0 || rcCrop.Right >= bmp.Width || rcCrop.Bottom >= bmp.Height)
                return null;
            Bitmap bmpCrop = new Bitmap(rcCrop.Width, rcCrop.Height);
            using (Graphics g = Graphics.FromImage(bmpCrop))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.DrawImage(bmp, new Rectangle(Point.Empty, bmpCrop.Size), rcCrop, GraphicsUnit.Pixel);
            }
            AddCropBitmap(ic, bmpCrop);
            return bmpCrop;
        }

        public Bitmap GetSubCrop(string strFile, Rectangle rcCrop, Rectangle rcSubCrop, string strDefaultDirectory = null)
        {
            ImageCrop ic = new ImageCrop(strFile, new Rectangle(rcCrop.X + rcSubCrop.X, rcCrop.Y + rcSubCrop.Y, rcSubCrop.Width, rcSubCrop.Height));
            if (m_dictCrops.ContainsKey(ic))
                return m_dictCrops[ic].Bitmap;
            Bitmap bmpCrop = GetCrop(strFile, rcCrop, strDefaultDirectory);
            if (bmpCrop == null)
                return null;
            Bitmap bmpSubCrop = new Bitmap(rcSubCrop.Width, rcSubCrop.Height);
            using (Graphics g = Graphics.FromImage(bmpSubCrop))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.DrawImage(bmpCrop, new Rectangle(Point.Empty, bmpSubCrop.Size), rcSubCrop, GraphicsUnit.Pixel);
            }
            AddCropBitmap(ic, bmpSubCrop);
            return bmpSubCrop;
        }
    }

    public class MapLabels
    {
        private Dictionary<PointF, MapLabel> m_labelsByLocation;
        private Dictionary<Point, List<MapLabel>> m_labelsByAnchor;

        public MapLabels()
        {
            Clear();
        }

        public void Clear()
        {
            m_labelsByLocation = new Dictionary<PointF, MapLabel>();
            m_labelsByAnchor = new Dictionary<Point, List<MapLabel>>();
        }

        public void AddRange(IEnumerable<MapLabel> labels)
        {
            foreach (MapLabel label in labels)
                Add(label);
        }

        public bool Add(MapLabel label)
        {
            return Add(m_labelsByLocation, m_labelsByAnchor, label);
        }

        private bool Add(Dictionary<PointF, MapLabel> locations, Dictionary<Point, List<MapLabel>> anchors, MapLabel label)
        {
            if (label == null)
                return false;
            if (locations.ContainsKey(label.Location))
                return false;
            locations.Add(label.Location, label);

            foreach (Rectangle rc in label.Anchors)
            {
                for (int y = rc.Top; y < rc.Bottom; y++)
                {
                    for (int x = rc.Left; x < rc.Right; x++)
                    {
                        Point pt = new Point(x, y);
                        if (!anchors.ContainsKey(pt))
                            anchors.Add(pt, new List<MapLabel>(1));
                        if (!anchors[pt].Contains(label))
                            anchors[pt].Add(label);
                    }
                }
            }
            return true;
        }

        public bool Remove(MapLabel label)
        {
            if (!m_labelsByLocation.ContainsKey(label.Location))
                return false;

            m_labelsByLocation.Remove(label.Location);

            bool bRemovedAnchors = false;
            foreach (Rectangle rc in label.Anchors)
            {
                for (int y = rc.Top; y < rc.Bottom; y++)
                {
                    for (int x = rc.Left; x < rc.Right; x++)
                    {
                        Point pt = new Point(x, y);
                        if (m_labelsByAnchor.ContainsKey(pt))
                        {
                            m_labelsByAnchor[pt].Remove(label);
                            bRemovedAnchors = true;
                        }
                    }
                }
            }

            return bRemovedAnchors;
        }

        public MapLabel Move(MapLabel label, float x, float y)
        {
            // Moves a label by removing it from the dictionaries and adding a new label with the updated coordinates
            if (!m_labelsByLocation.ContainsKey(label.Location))
                return label;   // Not in the dictionary; can't move it

            MapLabel labelNew = new MapLabel(new PointF(label.Location.X + x, label.Location.Y + y), label.Anchors, label.Text);
            labelNew.CopyFrom(label);
            Remove(label);
            Add(labelNew);
            return labelNew;
        }

        public bool RemoveAllAtAnchor(int col, int row) { return RemoveAllAtAnchor(new Point(col, row)); }

        public bool RemoveAllAtAnchor(Point pt)
        {
            if (!m_labelsByAnchor.ContainsKey(pt))
                return false;

            foreach (MapLabel label in m_labelsByAnchor[pt])
                m_labelsByLocation.Remove(label.Location);

            m_labelsByAnchor.Remove(pt);
            return true;
        }

        public int Count { get { return m_labelsByLocation.Count; } }

        public Dictionary<PointF, MapLabel>.KeyCollection Keys { get { return m_labelsByLocation.Keys; } }
        public Dictionary<PointF, MapLabel>.ValueCollection Values { get { return m_labelsByLocation.Values; } }

        public void Offset(int x, int y)
        {
            if (x == 0 && y == 0)
                return; // Don't waste time re-copying the dictionary if it hasn't changed

            // Since the dictionary keys are the locations and anchors, the dictionaries must be re-created with the new values
            Dictionary<PointF, MapLabel> newLocations = new Dictionary<PointF,MapLabel>(m_labelsByLocation.Count);
            Dictionary<Point, List<MapLabel>> newAnchors = new Dictionary<Point,List<MapLabel>>(m_labelsByAnchor.Count);

            foreach (MapLabel label in m_labelsByLocation.Values)
                Add(newLocations, newAnchors, label.Offset(x, y));

            m_labelsByLocation = newLocations;
            m_labelsByAnchor = newAnchors;
        }

        public void Offset(Point ptAnchor, int x, int y)
        {
            if (x == 0 && y == 0)
                return; // Don't waste time re-copying the dictionary if it hasn't changed

            List<MapLabel> anchored = LabelsAtAnchor(ptAnchor);
            if (anchored == null || anchored.Count < 1)
                return; // Nothing to change

            // All of the anchored labels must be removed from the dictionaries and re-positioned

            m_labelsByAnchor.Remove(ptAnchor);
            foreach (MapLabel label in anchored)
            {
                m_labelsByLocation.Remove(label.Location);
                Add(label.Offset(x, y));
            }
        }

        public bool ContainsKey(PointF pt)
        {
            return m_labelsByLocation != null && m_labelsByLocation.ContainsKey(pt);
        }

        public bool ContainsKey(Point pt)
        {
            return m_labelsByAnchor != null && m_labelsByAnchor.ContainsKey(pt);
        }

        public bool AnyAnchorsAt(int col, int row) { return AnyAnchorsAt(new Point(col, row)); }

        public bool AnyAnchorsAt(Point pt)
        {
            if (m_labelsByAnchor == null)
                return false;
            if (!m_labelsByAnchor.ContainsKey(pt))
                return false;
            return m_labelsByAnchor[pt].Count > 0;
        }

        public List<MapLabel> LabelsAtAnchor(int col, int row) { return LabelsAtAnchor(new Point(col, row)); }

        public List<MapLabel> LabelsAtAnchor(Point pt)
        {
            if (!m_labelsByAnchor.ContainsKey(pt))
                return null;

            return m_labelsByAnchor[pt];
        }

        public MapLabel this[PointF pt]
        {
            get
            {
                return m_labelsByLocation[pt];
            }
        }
    }

    public class MapLabel
    {
        public String Text;
        public Color ForeColor;
        public Color BackColor;
        public Color BorderColor;
        public int Size;                // Percent of a relative 12-point size
        private PointF m_ptLocation;    // Can be in fractions of a square
        private Rectangle[] m_rcAnchors;   // The square(s) that determines if this label is shown or not
        private HashSet<Point> m_anchorPoints;
        public bool Selected;           // used to give visual feedback of the selected notes in the edit form

        public static Color DefaultForeColor = Color.Black;
        public static Color DefaultBackColor = Color.FromArgb(128, Color.Gainsboro);
        public static Color DefaultBorderColor = Color.Transparent;

        public MapLabel Clone()
        {
            return new MapLabel(this);
        }

        public void Serialize(Stream stream)
        {
            Global.WriteFloat(stream, m_ptLocation.X);
            Global.WriteFloat(stream, m_ptLocation.Y);
            Global.WriteInt32(stream, m_rcAnchors.Length);
            foreach (Rectangle rc in m_rcAnchors)
            {
                Global.WriteInt32(stream, rc.X);
                Global.WriteInt32(stream, rc.Y);
                Global.WriteInt32(stream, rc.Width);
                Global.WriteInt32(stream, rc.Height);
            }
            UTF8Encoding encoding = new UTF8Encoding();
            byte[] bytes = encoding.GetBytes(Text);
            Global.WriteInt32(stream, bytes.Length);
            stream.Write(bytes, 0, bytes.Length);

            Global.WriteInt32(stream, ForeColor.ToArgb());
            Global.WriteInt32(stream, BackColor.ToArgb());
            Global.WriteInt32(stream, BorderColor.ToArgb());
            Global.WriteInt32(stream, Size);
        }

        public static MapLabel Deserialize(Stream stream)
        {
            if (stream.Length - stream.Position < 36)
                return null;

            PointF ptLocation = new PointF(Global.ReadFloat(stream), Global.ReadFloat(stream));
            int iNumAnchors = Global.ReadInt32(stream);
            Rectangle[] anchors = new Rectangle[iNumAnchors];
            for (int i = 0; i < iNumAnchors; i++)
                anchors[i] = new Rectangle(Global.ReadInt32(stream), Global.ReadInt32(stream), Global.ReadInt32(stream), Global.ReadInt32(stream));
            int iTextLength = Global.ReadInt32(stream);
            byte[] bytes = new byte[iTextLength];
            stream.Read(bytes, 0, iTextLength);
            MapLabel label = new MapLabel(ptLocation, anchors, new UTF8Encoding().GetString(bytes, 0, bytes.Length));

            label.ForeColor = Color.FromArgb(Global.ReadInt32(stream));
            label.BackColor = Color.FromArgb(Global.ReadInt32(stream));
            label.BorderColor = Color.FromArgb(Global.ReadInt32(stream));
            label.Size = Global.ReadInt32(stream);

            return label;
        }

        public MapLabel(MapLabel copy)
        {
            // Location and Anchor may only be changed in the constructor
            m_ptLocation = copy.Location;
            m_rcAnchors = copy.Anchors;
            UpdateAnchorHashSet();

            CopyFrom(copy);
        }

        public void CopyFrom(MapLabel copy)
        {
            Text = copy.Text;
            ForeColor = copy.ForeColor;
            BackColor = copy.BackColor;
            BorderColor = copy.BorderColor;
            Size = copy.Size;
            Selected = copy.Selected;
        }

        public MapLabel Offset(int x, int y)
        {
            MapLabel newLabel = new MapLabel(new PointF(Location.X + x, Location.Y + y), Global.OffsetRects(Anchors, x, y), Text);
            newLabel.CopyFrom(this);
            return newLabel;
        }

        public MapLabel(PointF pt, string str)
        {
            SetDefaults();
            m_ptLocation = pt;
            m_rcAnchors = new Rectangle[1];
            m_rcAnchors[0] = new Rectangle((int)pt.X, (int)pt.Y, 1, 1);
            UpdateAnchorHashSet();
            Text = str;
        }

        public MapLabel(PointF pt, Rectangle[] anchors, string str)
        {
            SetDefaults();
            m_ptLocation = pt;
            m_rcAnchors = new Rectangle[anchors.Length];
            anchors.CopyTo(m_rcAnchors, 0);
            UpdateAnchorHashSet();
            Text = str;
        }

        private void UpdateAnchorHashSet()
        {
            m_anchorPoints = new HashSet<Point>();
            if (m_rcAnchors == null)
                return;
            foreach (Rectangle rc in m_rcAnchors)
            {
                for (int y = rc.Top; y < rc.Bottom; y++)
                {
                    for (int x = rc.Left; x < rc.Right; x++)
                    {
                        Point pt = new Point(x, y);
                        if (!m_anchorPoints.Contains(pt))
                            m_anchorPoints.Add(pt);
                    }
                }
            }
        }

        public int NumAnchorPoints { get { return m_anchorPoints.Count; } }
        public PointF Location { get { return m_ptLocation; } }
        public Rectangle[] Anchors { get { return m_rcAnchors; } }
        public bool IsAnchored(Point pt) { return m_anchorPoints.Contains(pt); }

        public void SetDefaults()
        {
            Text = String.Empty;
            ForeColor = DefaultForeColor;
            BackColor = DefaultBackColor;
            BorderColor = DefaultBorderColor;
            Size = 100;
            Selected = false;
        }

        public int GetScaledTextSize(Size szSquare)
        {
            return Math.Min(szSquare.Height, szSquare.Width) * 3 / 5;
        }

        public Font GetFont(Size szSquare)
        {
            return new Font(FontFamily.GenericSansSerif, GetScaledTextSize(szSquare) * Size / 100, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        public Font GetFont(int iSize)
        {
            return new Font(FontFamily.GenericSansSerif, iSize * Size / 100, FontStyle.Regular, GraphicsUnit.Pixel);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is MapLabel))
                return base.Equals(obj);

            MapLabel test = (MapLabel) obj;
            return (Location == test.Location &&
                Text == test.Text &&
                ForeColor == test.ForeColor &&
                BackColor == test.BackColor &&
                BorderColor == test.BorderColor &&
                RectsEqual(Anchors, test.Anchors) &&
                Size == test.Size);
        }

        public static bool RectsEqual(Rectangle[] rc1, Rectangle[] rc2)
        {
            if (rc1 == null && rc2 == null)
                return true;
            if (rc1 == null || rc2 == null)
                return false;
            if (rc1.Length != rc2.Length)
                return false;
            for (int i = 0; i < rc1.Length; i++)
                if (!rc1[i].Equals(rc2[i]))
                    return false;
            return true;
        }

        public static int HashRects(Rectangle[] rects)
        {
            if (rects == null)
                return 0;
            int hash = 0;
            foreach (Rectangle rc in rects)
                hash ^= rc.GetHashCode();
            return hash;
        }

        public override int GetHashCode()
        {
            return Text.GetHashCode() ^ Location.GetHashCode() ^ ForeColor.ToArgb() ^ BackColor.ToArgb() ^ BorderColor.ToArgb() ^ Size ^ HashRects(Anchors);
        }

        public string AnchorsString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (Rectangle rc in Anchors)
                {
                    sb.AppendFormat("{0},{1}");
                    if (rc.Width != 1 || rc.Height != 1)
                        sb.AppendFormat(",{0},{1}", rc.Width, rc.Height);
                    sb.Append(";");
                }
                if (sb.Length > 1)
                    sb.Remove(sb.Length-1,1);
                return sb.ToString();
            }
        }

        public override string ToString()
        {
            return String.Format("Text=\"{0}\", Location={1:F2},{2:F2}, Anchors:{3}", Text, Location.X, Location.Y, AnchorsString);
        }

        public bool AnyAnchorHasFlag(MapSquareFlags[,] flags, MapSquareFlags flag)
        {
            int Width = flags.GetLength(0);
            int Height = flags.GetLength(1);
            foreach(Point pt in m_anchorPoints)
                if (pt.X < Width && pt.Y < Height && pt.X >= 0 && pt.Y >= 0 && flags[pt.X, pt.Y].HasFlag(flag))
                    return true;
            return false;
        }

        public bool AllAnchorsHaveFlag(MapSquareFlags[,] flags, MapSquareFlags flag)
        {
            int Width = flags.GetLength(0);
            int Height = flags.GetLength(1);
            foreach (Point pt in m_anchorPoints)
                if (pt.X < Width && pt.Y < Height && pt.X >= 0 && pt.Y >= 0 && !flags[pt.X, pt.Y].HasFlag(flag))
                    return false;
            return true;
        }
    }

    [Flags]
    public enum UpdateType
    {
        None =                  0x0000,
        Dirty =                 0x0001,
        Unsaved =               0x0002,
        Refresh =               0x0004,
        RefreshBook =           0x0008,
        Encounters =            0x0010,
        Quests =                0x0020,
        EncountersRefresh = Encounters | Refresh,
        DirtyUnsaved = Dirty | Unsaved,
        RefreshUnsaved = Refresh | Unsaved,
        RefreshBookDirty = RefreshBook | Dirty,
    }

    public class LineOfSight
    {
        class LinePair
        {
            public PointDir Front;
            public PointDir Back;

            public LinePair(PointDir front, PointDir back)
            {
                Front = front;
                Back = back;
            }

            public LinePair Rotate180(Size szBounds)
            {
                return new LinePair(Front.Rotate180(szBounds), Back.Rotate180(szBounds));
            }

            public LinePair Rotate90(Size szBounds)
            {
                return new LinePair(Front.Rotate90(szBounds), Back.Rotate90(szBounds));
            }

            public override string ToString()
            {
                return String.Format("{0} <=> {1}", Front == null ? "null" : Front.ToString(), Back == null ? "null" : Back.ToString());
            }
        }

        class LineSet
        {
            public LinePair[] Pairs;

            public LineSet(params LinePair[] pairs)
            {
                Pairs = pairs;
            }

            public static LineSet Create(int x, int y, Direction dir)
            {
                Point ptOpposite = Global.OffsetPoint(new Point(x, y), dir);
                LineSet set = new LineSet(new LinePair(new PointDir(x, y, dir), new PointDir(ptOpposite.X, ptOpposite.Y, Global.Opposite(dir))));
                return set;
            }

            public static LineSet Create(int x1, int y1, Direction dir1, int x2, int y2, Direction dir2)
            {
                Point ptOpposite1 = Global.OffsetPoint(new Point(x1, y1), dir1);
                Point ptOpposite2 = Global.OffsetPoint(new Point(x2, y2), dir2);
                LineSet set = new LineSet(
                    new LinePair(new PointDir(x1, y1, dir1), new PointDir(ptOpposite1.X, ptOpposite1.Y, Global.Opposite(dir1))),
                    new LinePair(new PointDir(x2, y2, dir2), new PointDir(ptOpposite2.X, ptOpposite2.Y, Global.Opposite(dir2)))
                    );
                return set;
            }

            public LineSet Rotate180(Size szBounds)
            {
                LinePair[] pairs = new LinePair[Pairs.Length];
                for (int i = 0; i < Pairs.Length; i++)
                    pairs[i] = Pairs[i].Rotate180(szBounds);
                return new LineSet(pairs);
            }

            public LineSet Rotate90(Size szBounds)
            {
                LinePair[] pairs = new LinePair[Pairs.Length];
                for (int i = 0; i < Pairs.Length; i++)
                    pairs[i] = Pairs[i].Rotate90(szBounds);
                return new LineSet(pairs);
            }

            public override string ToString()
            {
                return String.Format("Pairs: {0}", Pairs == null ? "null" : Pairs.Length.ToString());
            }
        }

        class PDSet
        {
            // Represents the logic:  ((a or b) and (c or d)) or ((e or f) and (g or h)) or ((i or j) and (k or l))
            // where the letters are all PointDir objects
            public List<LineSet> Sets;

            public PDSet()
            {
                Sets = new List<LineSet>();
            }

            public PDSet(int x, int y, Direction dir)
            {
                Sets = new List<LineSet>(1);
                Sets.Add(LineSet.Create(x, y, dir));
            }

            public PDSet(int x1, int y1, Direction dir1, int x2, int y2, Direction dir2, int x3, int y3, Direction dir3,
                int x4, int y4, Direction dir4, int x5, int y5, Direction dir5, int x6, int y6, Direction dir6)
            {
                Sets = new List<LineSet>(3);
                Sets.Add(LineSet.Create(x1, y1, dir1, x2, y2, dir2));
                Sets.Add(LineSet.Create(x3, y3, dir3, x4, y4, dir4));
                Sets.Add(LineSet.Create(x5, y5, dir5, x6, y6, dir6));
            }

            public PDSet(int x1, int y1, Direction dir1, int x2, int y2, Direction dir2)
            {
                Sets = new List<LineSet>(2);
                Sets.Add(LineSet.Create(x1, y1, dir1));
                Sets.Add(LineSet.Create(x2, y2, dir2));
            }

            public PDSet(int x1, int y1, Direction dir1, int x2, int y2, Direction dir2, int x3, int y3, Direction dir3)
            {
                Sets = new List<LineSet>(3);
                Sets.Add(LineSet.Create(x1, y1, dir1));
                Sets.Add(LineSet.Create(x2, y2, dir2));
                Sets.Add(LineSet.Create(x3, y3, dir3));
            }

            public bool Test(GameNames game, MapSheet sheet, int x, int y)
            {
                // x and y are the grid coordinates of where (0,0) is in the local line sets
                foreach (LineSet set in Sets)
                {
                    bool bAllSegments = true;
                    foreach (LinePair pair in set.Pairs)
                    {
                        MapSquare squareFront = sheet.GetSquareAtGridPoint(pair.Front.Point, x, y);
                        MapSquare squareBack = sheet.GetSquareAtGridPoint(pair.Back.Point, x, y);
                        bool bFront = squareFront != null && squareFront.HasSightBlockingLine(game, sheet, pair.Front.Dir, sheet.GridLines);
                        bool bBack = squareBack != null && squareBack.HasSightBlockingLine(game, sheet, pair.Back.Dir, sheet.GridLines);
                        bool bEitherSide = bFront || bBack;
                        if (bBack && squareBack.HasIcon(IconName.ArrowHalf, Global.Opposite(pair.Front.Dir)) && squareBack.Left.Color == Color.Black)
                            bEitherSide = false;    // A half-arrow by a wall means that it can be seen and stepped through from one side, which means "not sight blocking"
                        if (!bEitherSide)
                        {
                            // If neither side has a line, the line set is incomplete
                            bAllSegments = false;
                            break;
                        }
                    }
                    if (bAllSegments)
                        return false;   // If any line set is complete, the tested location cannot be seen
                }

                return true;    // No line set was complete, so the tested location is visible
            }

            public PDSet Rotate180(Size szBounds)
            {
                PDSet pdSet = new PDSet();
                foreach (LineSet set in Sets)
                    pdSet.Sets.Add(set.Rotate180(szBounds));
                return pdSet;
            }

            public PDSet Rotate90(Size szBounds)
            {
                PDSet pdSet = new PDSet();
                foreach (LineSet set in Sets)
                    pdSet.Sets.Add(set.Rotate90(szBounds));
                return pdSet;
            }
        }

        private PDSet[,] NorthView;
        private PDSet[,] SouthView;
        private PDSet[,] EastView;
        private PDSet[,] WestView;

        public LineOfSight()
        {
            Size szBounds = new Size(3, 5);
            NorthView = new PDSet[3, 5];
            NorthView[0, 0] = new PDSet(0, 0, Direction.Right, 0, 0, Direction.Down, 0, 0, Direction.Right, 0, 1, Direction.Right, 0, 0, Direction.Down, 1, 0, Direction.Down);
            NorthView[1, 0] = new PDSet(1, 0, Direction.Down, 1, 1, Direction.Down, 1, 2, Direction.Down);
            NorthView[2, 0] = new PDSet(2, 0, Direction.Left, 2, 0, Direction.Down, 2, 0, Direction.Left, 2, 1, Direction.Left, 2, 0, Direction.Down, 1, 0, Direction.Down);
            NorthView[0, 1] = new PDSet(0, 1, Direction.Right, 0, 1, Direction.Down, 0, 1, Direction.Right, 0, 2, Direction.Right, 0, 1, Direction.Down, 1, 1, Direction.Down);
            NorthView[1, 1] = new PDSet(1, 1, Direction.Down, 1, 2, Direction.Down, 1, 3, Direction.Down);
            NorthView[2, 1] = new PDSet(2, 1, Direction.Left, 2, 1, Direction.Down, 2, 1, Direction.Left, 2, 2, Direction.Left, 2, 1, Direction.Down, 1, 1, Direction.Down);
            NorthView[0, 2] = new PDSet(0, 2, Direction.Right, 0, 2, Direction.Down, 0, 2, Direction.Right, 0, 3, Direction.Right, 0, 2, Direction.Down, 1, 2, Direction.Down);
            NorthView[1, 2] = new PDSet(1, 2, Direction.Down, 1, 3, Direction.Down);
            NorthView[2, 2] = new PDSet(2, 2, Direction.Left, 2, 2, Direction.Down, 2, 2, Direction.Left, 2, 3, Direction.Left, 2, 2, Direction.Down, 1, 2, Direction.Down);
            NorthView[0, 3] = new PDSet(0, 3, Direction.Right, 0, 3, Direction.Down, 0, 3, Direction.Right, 0, 4, Direction.Right, 0, 3, Direction.Down, 1, 3, Direction.Down);
            NorthView[1, 3] = new PDSet(1, 3, Direction.Down);
            NorthView[2, 3] = new PDSet(2, 3, Direction.Left, 2, 3, Direction.Down, 2, 3, Direction.Left, 2, 4, Direction.Left, 2, 3, Direction.Down, 1, 3, Direction.Down);
            NorthView[0, 4] = new PDSet(0, 4, Direction.Right);
            NorthView[1, 4] = new PDSet();
            NorthView[2, 4] = new PDSet(2, 4, Direction.Left);

            NorthView[0, 0].Sets.Add(LineSet.Create(1, 1, Direction.Left, 1, 1, Direction.Up));
            NorthView[0, 0].Sets.Add(LineSet.Create(1, 2, Direction.Left, 1, 2, Direction.Up));
            NorthView[0, 1].Sets.Add(LineSet.Create(1, 2, Direction.Left, 1, 2, Direction.Up));
            NorthView[0, 2].Sets.Add(LineSet.Create(1, 3, Direction.Left, 1, 3, Direction.Up));
            NorthView[2, 0].Sets.Add(LineSet.Create(1, 1, Direction.Right, 1, 1, Direction.Up));
            NorthView[2, 0].Sets.Add(LineSet.Create(1, 2, Direction.Right, 1, 2, Direction.Up));
            NorthView[2, 1].Sets.Add(LineSet.Create(1, 2, Direction.Right, 1, 2, Direction.Up));
            NorthView[2, 2].Sets.Add(LineSet.Create(1, 3, Direction.Right, 1, 3, Direction.Up));

            NorthView[0, 2].Sets.Add(LineSet.Create(1, 3, Direction.Left, 1, 3, Direction.Up));
            NorthView[2, 2].Sets.Add(LineSet.Create(1, 3, Direction.Right, 1, 3, Direction.Up));
            NorthView[0, 3].Sets.Add(LineSet.Create(1, 4, Direction.Left, 1, 4, Direction.Up));
            NorthView[2, 3].Sets.Add(LineSet.Create(1, 4, Direction.Right, 1, 4, Direction.Up));

            NorthView[0, 0].Sets.Add(LineSet.Create(1, 2, Direction.Down));
            NorthView[0, 0].Sets.Add(LineSet.Create(0, 1, Direction.Down, 1, 1, Direction.Down));
            NorthView[0, 0].Sets.Add(LineSet.Create(0, 0, Direction.Down, 1, 1, Direction.Down));
            NorthView[1, 0].Sets.Add(LineSet.Create(1, 2, Direction.Down));
            NorthView[2, 0].Sets.Add(LineSet.Create(1, 2, Direction.Down));
            NorthView[2, 0].Sets.Add(LineSet.Create(1, 1, Direction.Down, 2, 1, Direction.Down));
            NorthView[2, 0].Sets.Add(LineSet.Create(1, 1, Direction.Down, 2, 0, Direction.Down));
            NorthView[0, 0].Sets.Add(LineSet.Create(1, 3, Direction.Down));
            NorthView[1, 0].Sets.Add(LineSet.Create(1, 3, Direction.Down));
            NorthView[2, 0].Sets.Add(LineSet.Create(1, 3, Direction.Down));
            NorthView[0, 2].Sets.Add(LineSet.Create(1, 3, Direction.Down));
            NorthView[2, 2].Sets.Add(LineSet.Create(1, 3, Direction.Down));
            NorthView[0, 1].Sets.Add(LineSet.Create(1, 2, Direction.Down));
            NorthView[0, 1].Sets.Add(LineSet.Create(1, 3, Direction.Down));
            NorthView[2, 1].Sets.Add(LineSet.Create(1, 2, Direction.Down));
            NorthView[2, 1].Sets.Add(LineSet.Create(1, 3, Direction.Down));

            SouthView = new PDSet[3, 5];
            for(int i = 0; i < szBounds.Height; i++)
                for(int j = 0; j < szBounds.Width; j++)
                    SouthView[j, i] = NorthView[szBounds.Width - 1 - j, szBounds.Height - 1 - i].Rotate180(szBounds);

            szBounds = new Size(5, 3);
            EastView = new PDSet[5, 3];
            for (int i = 0; i < szBounds.Height; i++)
                for (int j = 0; j < szBounds.Width; j++)
                    EastView[j, i] = NorthView[i, szBounds.Width - 1 - j].Rotate90(szBounds);

            WestView = new PDSet[5, 3];
            for (int i = 0; i < szBounds.Height; i++)
                for (int j = 0; j < szBounds.Width; j++)
                    WestView[j, i] = EastView[szBounds.Width - 1 - j, szBounds.Height - 1 - i].Rotate180(szBounds);
        }

        public bool IsVisible(GameNames game, MapSheet sheet, Point ptOrigin, Direction facing, Point ptTest, int iLightDistance)
        {
            // Determines if the point at "ptTest" is visible from "ptOrigin" when looking in the direction "facing"
            // A light distance of 0 is total darkness; 1 indicates only being able to see the current square (and left/right adjacent)
            Point ptView = Point.Empty;
            Point ptOffsetOrigin = Point.Empty;
            PDSet pdSet = null;
            bool bIgnoreDark = !Games.IsWizardry(game);
            int iMaxDepth = Math.Min(iLightDistance - 1, 4);

            if (!bIgnoreDark && sheet.PointInGrid(ptTest) && SquareStyleList.IsDark(sheet.Grid[ptTest.X, ptTest.Y].Colors.BackColorPattern))
                return false;

            switch (facing)
            {
                case Direction.Up:
                    ptView = new Point(1 - (ptOrigin.X - ptTest.X), 4 - (ptOrigin.Y - ptTest.Y));
                    if (ptView.X < 0 || ptView.X > 2 || ptView.Y < 0 || ptView.Y > 4)
                        return false;
                    if (4 - ptView.Y > iMaxDepth)
                        return false;
                    pdSet = NorthView[ptView.X, ptView.Y];
                    ptOffsetOrigin = new Point(ptOrigin.X - 1, ptOrigin.Y - 4);
                    break;
                case Direction.Down:
                    ptView = new Point(1 - (ptOrigin.X - ptTest.X), ptTest.Y - ptOrigin.Y);
                    if (ptView.X < 0 || ptView.X > 2 || ptView.Y < 0 || ptView.Y > 4)
                        return false;
                    if (ptView.Y > iMaxDepth)
                        return false;
                    pdSet = SouthView[ptView.X, ptView.Y];
                    ptOffsetOrigin = new Point(ptOrigin.X - 1, ptOrigin.Y);
                    break;
                case Direction.Left:
                    ptView = new Point(4 - (ptOrigin.X - ptTest.X), 1 - (ptOrigin.Y - ptTest.Y));
                    if (ptView.X < 0 || ptView.X > 4 || ptView.Y < 0 || ptView.Y > 2)
                        return false;
                    if (4 - ptView.X > iMaxDepth)
                        return false;
                    pdSet = WestView[ptView.X, ptView.Y];
                    ptOffsetOrigin = new Point(ptOrigin.X - 4, ptOrigin.Y - 1);
                    break;
                case Direction.Right:
                    ptView = new Point(ptTest.X - ptOrigin.X, 1 - (ptOrigin.Y - ptTest.Y));
                    if (ptView.X < 0 || ptView.X > 4 || ptView.Y < 0 || ptView.Y > 2)
                        return false;
                    if (ptView.X > iMaxDepth)
                        return false;
                    pdSet = EastView[ptView.X, ptView.Y];
                    ptOffsetOrigin = new Point(ptOrigin.X, ptOrigin.Y - 1);
                    break;
                default:
                    return false;
            }

            return pdSet.Test(game, sheet, ptOffsetOrigin.X, ptOffsetOrigin.Y);
        }
    }

    public class PointDir
    {
        public Point Point;
        public Direction Dir;

        public PointDir(int x, int y, Direction dir)
        {
            Point = new Point(x, y);
            Dir = dir;
        }

        public PointDir Rotate180(Size szBounds)
        {
            return new PointDir(szBounds.Width - 1 - Point.X, szBounds.Height - 1 - Point.Y, Global.Opposite(Dir));
        }

        public PointDir Rotate90(Size szBounds)
        {
            return new PointDir(szBounds.Width - 1 - Point.Y, Point.X, Global.Rotate(Dir, true));
        }

        public override string ToString()
        {
            return String.Format("{0},{1},{2}", Point.X, Point.Y, Global.FacingString(Dir));
        }
    }

    public class IntDeck
    {
        private List<int> m_list;

        public List<int> Cards { get { return m_list; } }

        public IntDeck(int iStart, int iCount)
        {
            m_list = new List<int>(iCount);
            for(int i = iStart; i < iCount; i++)
                m_list.Add(i);
        }

        public void Shuffle() { m_list.Shuffle(); }
    }

    public class DirectionsTo
    {
        public class SingleDirectionTo
        {
            public int North;
            public int East;
            public int Up;
            public bool Possible;

            public SingleDirectionTo(int north, int east, int up = 0, bool possible = true)
            {
                North = north;
                East = east;
                Up = up;
                Possible = possible;
            }

            public static SingleDirectionTo Impossible { get { return new SingleDirectionTo(0, 0, 0, false); } }

            public void Add(SingleDirectionTo dir)
            {
                if (!dir.Possible)
                    Possible = false;
                North += dir.North;
                East += dir.East;
                Up += dir.Up;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                if (North != 0)
                    sb.AppendFormat("{0} {1}, ", North > 0 ? "North" : "South", Math.Abs(North));
                if (East != 0)
                    sb.AppendFormat("{0} {1}, ", East > 0 ? "East" : "West", Math.Abs(East));
                if (Up != 0)
                    sb.AppendFormat("{0} {1}, ", Up > 0 ? "Up" : "Down", Math.Abs(Up));
                return Global.Trim(sb).ToString();
            }
        }

        public List<SingleDirectionTo> All;

        public DirectionsTo()
        {
            All = new List<SingleDirectionTo>();
        }

        public DirectionsTo(int north, int east)
        {
            All = new List<SingleDirectionTo>(1);
            All.Add(new SingleDirectionTo(north, east));
        }

        public DirectionsTo(int north, int east, int up)
        {
            All = new List<SingleDirectionTo>(1);
            All.Add(new SingleDirectionTo(north, east, up));
        }

        public static DirectionsTo Impossible { get { return new DirectionsTo(); } }

        public bool Possible { get { return All.Count < 1 ? false : All.All(d => d.Possible); } }

        public SingleDirectionTo Total
        {
            get
            {
                if (All == null || All.Count < 1)
                    return null;

                SingleDirectionTo total = new SingleDirectionTo(0, 0);
                foreach (SingleDirectionTo dir in All)
                    total.Add(dir);

                return total;
            }
        }
    }

    public class ListViewSelectionSaver
    {
        private HashSet<int> m_selected;
        private ListView m_listView;

        public ListViewSelectionSaver(ListView lv)
        {
            m_listView = lv;
            m_selected = new HashSet<int>();
            foreach (int i in lv.SelectedIndices)
                m_selected.Add(i);
        }

        public void Restore(ListView lv = null, bool bUseUpdate = false)
        {
            if (lv == null)
                lv = m_listView;
            if (bUseUpdate)
                lv.BeginUpdate();
            for (int i = 0; i < lv.Items.Count; i++)
                lv.Items[i].Selected = m_selected.Contains(i);
            if (bUseUpdate)
                lv.EndUpdate();
        }
    }

    public class DrawParams
    {
        public bool ChangesOnly;
        public bool DrawCursor;
        public bool IgnoreHidden;
        public ColorPattern Unvisited;
        public bool IgnoreInaccessible;
        public bool MapChanged;
        public bool ShowLive;

        public DrawParams(bool bChangesOnly, bool bDrawCursor, bool bIgnoreHidden, ColorPattern cpUnvisited, bool bIgnoreInaccessible, bool bShowLive)
        {
            // in
            ChangesOnly = bChangesOnly;
            DrawCursor = bDrawCursor;
            IgnoreHidden = bIgnoreHidden;
            Unvisited = cpUnvisited;
            IgnoreInaccessible = bIgnoreInaccessible;
            ShowLive = bShowLive;

            // out
            MapChanged = false;
        }
    }

    public class SquareDirInfo
    {
        public MapLineInfo Line;
        public IconName Icon;

        public SquareDirInfo(MapLineInfo line, IconName icon)
        {
            Line = line;
            Icon = icon;
        }
    }

    public class SquareDirParams
    {
        public MapLineInfo Info;
        public Point GridPoint;
        public MapSquare Square;
        public int Tile;
        public int OppositeTile;
        public MMAreaStyle Style;
        public int Visible;
        public bool Solid;
        public int OppositeVisible;
        public bool OppositeSolid;
        public bool OppositeExitable;
        public Direction Dir;
        public bool Unimportant;

        public SquareDirParams()
        {
        }

        public void Set(MapLineInfo info, int oppositeTile, int visible, bool solid, int oppositeVisible, bool oppositeSolid, bool oppositeExitable, Direction dir)
        {
            Info = info;
            OppositeTile = oppositeTile;
            Visible = visible;
            Solid = solid;
            OppositeVisible = oppositeVisible;
            OppositeSolid = oppositeSolid;
            OppositeExitable = oppositeExitable;
            Dir = dir;
        }

        public bool IsDoor
        {
            get
            {
                switch (Style)
                {
                    case MMAreaStyle.Castle:
                    case MMAreaStyle.Town:
                        return Visible == 2;
                    case MMAreaStyle.Dungeon:
                        return Visible == 2 || Visible == 3;
                    case MMAreaStyle.MM2Dungeon:
                        return Visible == 2;
                    case MMAreaStyle.Mixed:
                        return Visible == 3;
                    default:
                        return Visible == 2;
                }
            }
        }

        public bool IsEdge(Rectangle rcBounds)
        {
            switch (Dir)
            {
                case Direction.Up: return GridPoint.Y == rcBounds.Top;
                case Direction.Left: return GridPoint.X == rcBounds.Left;
                case Direction.Right: return GridPoint.X == rcBounds.Right - 1;
                case Direction.Down: return GridPoint.Y == rcBounds.Bottom - 1;
                default: return false;
            }
        }
    }

    public class SpellList
    {
        public virtual Spell GetSpell(int index) { return null; }

        public virtual bool HasTarget(int index)
        {
            Spell spell = GetSpell(index);
            if (spell == null)
                return false;

            switch (spell.Target)
            {
                case SpellTarget.Caster:
                case SpellTarget.Monster:
                case SpellTarget.Character:
                case SpellTarget.CharacterSameAlign:
                case SpellTarget.NonUndeadMonster:
                case SpellTarget.ThreeMonsters:
                case SpellTarget.FiveMonsters:
                case SpellTarget.FiveRangedMonsters:
                case SpellTarget.ThreeRangedMonsters:
                case SpellTarget.TenMonsters:
                case SpellTarget.FourMonstersPlusOnePerLevel:
                case SpellTarget.FourMonsters:
                case SpellTarget.FiveStepsPerLevel:
                case SpellTarget.CharacterOnce:
                case SpellTarget.TwoMonsters:
                case SpellTarget.ThreeNonUndeadMonsters:
                case SpellTarget.SixMonsters:
                case SpellTarget.OneRangedMonster:
                case SpellTarget.FourRangedMonstersPlusOnePerLevel:
                case SpellTarget.SixRangedMonsters:
                case SpellTarget.MonsterGroup:
                case SpellTarget.UndeadMonsterGroup:
                case SpellTarget.AnimalGroup:
                case SpellTarget.Dragon:
                case SpellTarget.Item:
                case SpellTarget.Golem:
                case SpellTarget.Monster10Feet:
                case SpellTarget.MonsterGroup20Feet:
                case SpellTarget.MonsterGroup30Feet:
                case SpellTarget.MonsterGroup40FeetDiminish:
                case SpellTarget.Monster70Feet:
                case SpellTarget.Monster40FeetDiminish:
                case SpellTarget.MonsterGroup10Feet:
                case SpellTarget.Monster30Feet:
                case SpellTarget.MonsterGroup50Feet:
                case SpellTarget.MonsterGroup60Feet:
                case SpellTarget.MonsterGroup80Feet:
                case SpellTarget.MonsterGroup40Feet:
                case SpellTarget.Monster60Feet:
                case SpellTarget.Monster90Feet:
                    return true;
                default:
                    return false;
            }
        }
    }

    public class MonsterName
    {
        public string Singular;
        public string Plural;

        public MonsterName(string sSingular, string sPlural = null)
        {
            Singular = sSingular;
            if (String.IsNullOrWhiteSpace(sPlural))
                Plural = Singular + "s";
            else
                Plural = sPlural;
        }

        public override string ToString()
        {
            return Singular == Plural ? Singular : String.Format("{0}/{1}", Singular, Plural);
        }
    }

    public class BitStream
    {
        public byte[] Bytes;
        public long BitPosition;
        public bool Reverse = false;
        public bool EndOfFile { get { return BitPosition >= Bytes.Length * 8; } }

        private int m_offset = 0;

        public BitStream(byte[] bytes, int offset = 0)
        {
            Bytes = bytes;
            m_offset = offset;
            BitPosition = 0;
        }

        public int GetNextBits(int iCount)
        {
            int iResult = 0;
            for (int i = 0; i < iCount; i++)
                iResult = (iResult << 1) | GetNextBit();
            return iResult;
        }

        public int GetNextBit()
        {
            long iByte = m_offset + (BitPosition / 8);
            int iBit = (int)(BitPosition % 8);
            if (!Reverse)
                iBit = 7 - iBit;

            BitPosition++;
            if (iByte >= Bytes.Length || iByte < 0)
                return 0;
            return (Bytes[iByte] >> iBit) & 1;
        }
    }
}
