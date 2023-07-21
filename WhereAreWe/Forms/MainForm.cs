using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.IO.Compression;
using System.Security.Principal;

namespace WhereAreWe
{
    public partial class MainForm : Form, IKeyboardHookCallback, IMain
    {
        private Action m_actionDraw = Action.None;
        private ActiveSquares m_lastActiveSquares = null;
        private BasicLocation m_lastLocation;
        private Bitmap m_bmpCopyBlocksGhost = null;
        private Bitmap m_bmpCopyMoveBlocks = null;
        private Bitmap m_bmpMoveBlocksGhost = null;
        private Bitmap m_bmpUnderSelection = null;
        private BlockMode m_modeEditToggle = BlockMode.None;
        private bool m_bAbortExport = false;
        private bool m_bBringDOSBoxToFront = false;
        private bool m_bCancelNoteMove = false;
        private bool m_bCenterKeyboard = true;
        private bool m_bCheckBook = false;
        private bool m_bConstrainMove = false;
        private bool m_bDirty;
        private bool m_bDOSBoxAboveWAW = false;
        private bool m_bDosBoxPosSet = true;
        private bool m_bDrawCursor = false;
        private bool m_bDrawStraightLine = false;
        private bool m_bEditingNote = false;
        private bool m_bExportingMaps = false;
        private bool m_bFirstRefresh = true;
        private bool m_bFocusingWindows = false;
        private bool m_bInDirtyTimer = false;
        private bool m_bLabelMoved = false;
        private bool m_bLaunchFinished = false;
        private bool m_bMapMenuDirty = true;
        private bool m_bNeedCartUpdate = false;
        private bool m_bNeedCentering = false;
        private bool m_bOverrideAutoSwitch = false;
        private bool m_bRecaptureSelection = false;
        private bool m_bSettingMode = false;
        private bool m_bShowingOptions = false;
        private bool m_bShowShopInventories = false;
        private bool m_bShuttingDown = false;
        private bool m_bSkipNextCenter = false;
        private bool m_bUnsaved = false;
        private bool m_bMonstersChanged = false;
        private bool m_bShowingCustomBackgroundError = false;
        private bool m_bAskingCorrectBook = false;
        private bool m_bEditingLiveSquares = false;
        private bool m_bSuspendMapUpdates = false;
        private bool m_bForceActiveSquareUpdate = false;
        private bool m_bRedrawAfterNextActive = false;
        private CaptureMode m_captureMode;
        private CLOptions m_clOptions = null;
        private Control m_ctrlLastFocus = null;
        private DateTime m_dtLastAutosave = DateTime.Now;
        private DateTime m_dtLastDebugCaptionUpdate = DateTime.MinValue;
        private DateTime m_dtLastDOSBoxCheck = DateTime.Now;
        private DateTime m_dtMonsterUpdate;
        private DrawColor m_dcCurrentBlock = new DrawColor(Color.Gray, HatchStyle.Percent90);
        private DrawColor m_dcCurrentErase = new DrawColor(Properties.Settings.Default.DefaultGridBackground, DashStyle.Solid);
        private DrawColor m_dcCurrentEraseLine = new DrawColor(Global.DefaultGridLineInfo);
        private DrawColor m_dcCurrentLine = new DrawColor(Color.Black, DashStyle.Solid, Properties.Settings.Default.DefaultLineWidth);
        private DrawColor m_dcLastBlock = new DrawColor(Color.Black, HatchStyle.Percent90);
        private DrawColor m_dcLastLine = new DrawColor(Color.Black, DashStyle.Solid);
        private DrawMode m_drawMode;
        private int m_iCurrentSheet;
        private int m_iLegendHoldIndex = -1;
        private int m_iSpellHideCount = 0;
        private int m_iSpellShowCount = 0;
        private int m_iMapLoadGraceCounter = 0;
        private int m_iCartographyGraceCounter = 0;
        private int m_iMovementGraceCounter = 0;
        private int m_iIgnoreDOSBoxWindowState = 0;
        private int m_iWatchdogInterval = 500;
        private int m_iPostLaunchGraceCounter = 0;
        private long m_lastGameTime = -1;
        private int m_iForceDBSizeGraceCounter = 0;
        private ManualResetEvent m_evtShutdown = new ManualResetEvent(false);
        private MapBook m_mapBook;
        private MapCartography m_lastCartography = null;
        private MapLabel m_labelAtCursor = null;
        private MapLabel m_labelBeforeMove = null;
        private MapNote m_noteCanceled = null;
        private MapNote m_noteCurrent;
        private MapSheet m_sheetDummy = new MapSheet(Global.DefaultGridLineInfo);
        private MemoryHacker m_hacker = null;
        private MonsterLocations m_lastMonsterLocations;
        private ItemLocations m_lastItemLocations;
        private MonsterPosition m_lastMonsterTip;
        private ItemPosition m_lastItemTip;
        private MouseButtons m_btnCaptured;
        private MouseEventArgs m_lastMouseMoveArgs = null;
        private Orient m_orStraightLine = Orient.None;
        private Point m_ptCaptureMove;
        private Point m_ptCaptureScrollbars;
        private Point m_ptContextOpened = Point.Empty;
        private Point m_ptCurrentNote = Global.NullPoint;
        private Point m_ptCursorAtContext = Global.NullPoint;
        private Point m_ptCursorAtMouseDown = Global.NullPoint;
        private Point m_ptFocusSquare = Global.NullPoint;
        private Point m_ptLastMonsterDetectionCenter = Global.NullPoint;
        private Point m_ptLastScroll = Global.NullPoint;
        private Point m_ptLastSquare = Global.NullPoint;
        private Point m_ptLastVertex = Global.NullPoint;
        private Point m_ptNextSquare = Global.NullPoint;
        private Point m_ptOriginalCapture;
        private Point m_ptUnderSelection = Global.NullPoint;
        private Rectangle m_rcSelection = Rectangle.Empty;
        private RepeatableTip m_tipCursor1;
        private RepeatableTip m_tipCursor2;
        private RepeatableTip m_tipCursor3;
        private RepeatableTip m_tipCursor4;
        private RepeatableTip m_tipNoteColor;
        private RepeatableTip m_tipNoteSymbol;
        private RepeatableTip m_tipParty1;
        private RepeatableTip m_tipParty2;
        private RepeatableTip m_tipParty3;
        private RepeatableTip m_tipParty4;
        private RepeatableTip m_tipSelection1;
        private RepeatableTip m_tipSelection2;
        private RepeatableTip m_tipUnicode;
        private SelectionActions m_selectionAction = SelectionActions.None;
        private Serializer m_serializer = new Serializer();
        private Shortcuts m_shortcuts = Properties.Settings.Default.Shortcuts;
        private ShowNext m_showNext = null;
        private SnapWindows m_snapWindows = null;
        private string m_strCurrentFile = null;
        private string m_strLastTitle = String.Empty;
        private System.Windows.Forms.Timer m_timerDelayFocus = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer m_timerDirty = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer m_timerFocusAll = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer m_timerHideTips = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer m_timerMouseMove = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer m_timerRedrawMap = new System.Windows.Forms.Timer();
        private System.Windows.Forms.Timer m_timerWatchdog = new System.Windows.Forms.Timer();
        private Thread m_threadExport = null;
        private Thread m_threadLaunch = null;
        private Thread threadLoadFile = null;
        private Thread m_threadSkipIntros = null;
        private ToolBarCapture m_captureToolBar = ToolBarCapture.None;
        private ToolTip m_tipGrid = null;
        private ToolTip m_tipReadOnly = new ToolTip();
        private ToolTip m_tipZoom = new ToolTip();
        private UndoList m_undoBlocks = null;
        private WindowInfo m_infoSelectionForm = null;
        private string m_strItemFormatHold = String.Empty;
        private List<Point> m_liveMap = new List<Point>();

        public delegate void ErrorDelegate(string strMessage, string strCaption);
        public delegate void VoidDelegate();
        public event VoidHandler OptionsChanged;

        private FastColorPickerForm m_fastColor = new FastColorPickerForm();
        private IconsForm m_iconsForm = null;
        private CreationAssistantForm m_formCreationAssistant = null;
        private EditLabelsForm m_formEditLabels = null;
        private EncounterForm m_formEncounters = null;
        private Form m_formDelayFocus = null;
        private GameInformationForm m_formInfo = null;
        private ItemsForm m_formItems = null;
        private List<IntPtr> m_formsNewOrder = null;
        private MonstersForm m_formMonsters = null;
        private QuestsForm m_formQuests = null;
        private QuickRefForm m_formQuickRef = null;
        private ScriptsForm m_formScripts = null;
        private SearchForm m_formSearch = null;
        private SheetSelectorForm m_formSelectSheet = null;
        private ShopInventoryForm m_formShopInventory = null;
        private SpellReferenceForm m_formSpells = null;
        private StringsViewForm m_formStrings = null;
        private TrainingAssistantForm m_formTrainingAssistant = null;
        private ViewPartyForm m_formParty = null;
        private WaitForm m_formWait = null;
        private NotificationForm m_formNotification = null;
        private DebugConsole m_formDebugConsole = null;

        public static ToolStripMenuItem[] m_menuItems;

        public void SetDirty()
        {
            // This is for the display
            m_bDirty = true;
        }

        public void SetDirty(Point ptGame)
        {
            // This is for the current sheet
            if (CurrentSheet == null)
                return;
            Point pt = TranslateToInternalMap(ptGame);
            CurrentSheet.SetDirty(pt);
        }

        public void SetGridDirty(Point pt)
        {
            // This is for the current sheet
            if (CurrentSheet == null)
                return;
            CurrentSheet.SetDirty(pt);
        }

        public void SetDirtyUnsaved(bool bCheckLabels = true)
        {
            SetDirty();
            SetUnsaved(true, bCheckLabels);
        }

        public GameNames Game
        {
            get
            {
                if (m_hacker == null)
                    return GameNames.None;
                return m_hacker.Game;
            }
        }

        public MapSheet CurrentSheet
        {
            get
            {
                if (m_iCurrentSheet < m_mapBook.Sheets.Count)
                    return m_mapBook.Sheets[m_iCurrentSheet];
                return m_sheetDummy;
            }
        }

        public MapBook CurrentBook { get { return m_mapBook; } }

        public void SetUnsaved() { SetUnsaved(true, true); }

        public void SetUnsaved(bool bUnsaved, bool bCheckLabels)
        {
            // This is for the file
            m_bUnsaved = bUnsaved;
            if (bCheckLabels)
                CheckLabelWindow();
            SetCaption();
        }

        public MainForm()
        {
            InitializeComponent();

            // These items must match the order of the first entries in the Action enum
            m_menuItems = new ToolStripMenuItem[] { miFileNew, miFileOpen, miFileSave, miFileSaveAs, miFileExportPNG, miFileExportZIP, miFileExit, 
                miEditUndo, miEditRedo, miEditCut, miEditCopy, miEditPaste, miEditDelete, miEditFind, miEditCrop, 
                    miEditRotateLeft, miEditRotateRight, miEditRotate180, miEditFlipHorizontally, miEditFlipVertically, miEditConvertHalf,
                    miEditFillBlocks, miEditOutline, miEditLiveSquares,
                miViewOptions, miViewColors, miViewInfo, miViewToolbar, miViewNoteTemplates, miViewTriggers, miViewZOrder, miView100pc, miView150pc, miView200pc, miView300pc, 
                    miViewFitWidth, miViewFitHeight, miViewFitInPanel, miViewFitWindow, miViewBringDOSBoxForeground, miViewAutoArrange,
                miModePlay, miModeBlock, miModeLine, miModeHybrid, miModeNotes, miModeKeyboard, miModeEdit, miModeFill,
                miSheetAdd, miSheetClone, miSheetRemove, miSheetExpand, miSheetPrevious, miSheetNext, miSheetGoto, miSheetOrganize, miSheetLabels, miSheetClearVisited, 
                miGameLaunchCurrent, miGameViewParty, miGameShowSpells, miGameShowMonsters, miGameShowItems, miGameShowInfo, miGameShowQuests, miGameShowShops,
                    miGameScripts, miGameQuickRef, miGameShowEncounters, miGameEditRoster, 
                    miGameCreationAssistant, miGameTrainingAssistant, miGameResetHacker, miGameRemoveMonsters, miGameResetMonsters,
                    miGameEditCartography,
                miHelpRunWizard};

            if (m_menuItems.Length != Action.LastMenuItem - Action.FirstMenuItem)
                Global.InternalError(String.Format("The number of menu items in m_menuItems ({0}) does not match the number of menu items in the Action enum ({1}).\r\n\r\n" +
                    "This will cause the menu shortcuts to be incorrect.", m_menuItems.Length, Action.LastMenuItem - Action.FirstMenuItem));

            KeyboardHook.Initialize();
            m_mapBook = Global.CreateNewMapBook();
            m_iCurrentSheet = 0;
            m_ptCaptureMove = Point.Empty;
            m_ptCaptureScrollbars = Point.Empty;
            m_btnCaptured = System.Windows.Forms.MouseButtons.None;
            m_tipGrid = new ToolTip();
            m_timerFocusAll.Interval = 1000;
            m_timerFocusAll.Tick += m_timerFocusAll_Tick;
            m_timerRedrawMap.Interval = 1500;
            m_timerRedrawMap.Tick += m_timerCartography_Tick;
            m_timerHideTips.Interval = 4000;
            m_timerHideTips.Tick += m_timerHideTips_Tick;
            m_timerDirty.Interval = 40;
            m_timerDirty.Tick += new System.EventHandler(this.timerDirty_Tick);
            m_timerDelayFocus.Interval = 100;
            m_timerDelayFocus.Tick += m_timerDelayFocus_Tick;
            m_iWatchdogInterval = Properties.Settings.Default.WatchdogTimer;
            m_timerWatchdog.Interval = m_iWatchdogInterval;
            m_timerWatchdog.Tick += m_timerWatchdog_Tick;
            m_bInDirtyTimer = true;     // Set to false after MainForm_Load is complete

            m_tipNoteSymbol = new RepeatableTip(tbSymbol, RepeatableTip.NoteSymbol);
            m_tipNoteColor = new RepeatableTip(pbSelectColor, RepeatableTip.NoteColor);
            m_tipUnicode = new RepeatableTip(btnUnicode, RepeatableTip.Unicode);
            m_tipParty1 = new RepeatableTip(labelParty, RepeatableTip.Party);
            m_tipParty2 = new RepeatableTip(tbPartyLocation, RepeatableTip.Party);
            m_tipParty3 = new RepeatableTip(labelParty, RepeatableTip.Party);
            m_tipParty4 = new RepeatableTip(tbPartyLocation, RepeatableTip.Party);
            m_tipCursor1 = new RepeatableTip(labelCursor, RepeatableTip.Cursor);
            m_tipCursor2 = new RepeatableTip(tbLocation, RepeatableTip.Cursor);
            m_tipCursor3 = new RepeatableTip(labelCursor, RepeatableTip.Cursor);
            m_tipCursor4 = new RepeatableTip(tbLocation, RepeatableTip.Cursor);
            m_tipSelection1 = new RepeatableTip(labelSelection, RepeatableTip.Selection);
            m_tipSelection2 = new RepeatableTip(tbSelection, RepeatableTip.Selection);
            tbNote.ContextMenuStrip = new RichTextBoxContextMenu(tbNote);

            for (BlockMode mode = BlockMode.Play; mode < BlockMode.Last; mode++)
                tscMode.Items.Add(new BlockModeItem(mode));

            m_fastColor.ColorSelected += OnFastColorSelected;

            if (Global.Debug)
                m_debugMonitor = new DebugMonitor(this);
        }
        private DebugMonitor m_debugMonitor;

        void m_timerWatchdog_Tick(object sender, EventArgs e)
        {
            if (m_bAskingCorrectBook)
                return;

            if ((m_hacker != null && m_hacker.IsValid) || Properties.Settings.Default.Game == GameNames.None)
                return;

            m_timerWatchdog.Stop();
            GameNames game = MemoryHacker.FindKnownGame();
            if (m_hacker != null && m_hacker.Game != game && game != GameNames.None)
            {
                m_hacker.Stop();
                m_hacker = null;

                if (CheckCorrectBookForGame())
                {
                    Properties.Settings.Default.Game = game;
                    CheckGameMemory();
                }
            }
            else
                CheckGameMemory();

            m_timerWatchdog.Interval = m_iWatchdogInterval;
            m_timerWatchdog.Start();
        }

        void m_timerDelayFocus_Tick(object sender, EventArgs e)
        {
            if (m_formDelayFocus != null)
                NativeMethods.SetForegroundWindow(m_formDelayFocus.Handle);
            m_formDelayFocus = null;
            m_timerDelayFocus.Stop();
        }

        private bool CheckLastShortcut(Action action)
        {
            if (m_lastActionRun == action)
            {
                m_lastActionRun = Action.None;
                return true;
            }
            CheckNotifications(action);
            return false;
        }

        private void miFileNew_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.FileNew)) { MenuFileNew(); } }
        private void miFileOpen_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.FileOpen)) { MenuFileOpen(); } }
        private void miFileSave_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.FileSave)) { MenuFileSave(); } }
        private void miFileSaveAs_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.FileSaveAs)) { MenuFileSaveAs(); } }
        private void miFileExportPNG_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.FileSaveAs)) { MenuFileExportPng(); } }
        private void miFileExportZIP_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.FileSaveAs)) { MenuFileExportZip(); } }
        private void miFileExit_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.FileExit)) { MenuFileExit(); } }
        private void miEditUndo_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditUndo)) { MenuEditUndo(); } }
        private void miEditRedo_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditRedo)) { MenuEditRedo(); } }
        private void miEditCut_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditCut)) { MenuEditCut(); } }
        private void miEditCopy_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditCopy)) { MenuEditCopy(); } }
        private void miEditPaste_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditPaste)) { MenuEditPaste(); } }
        private void miEditDelete_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditDelete)) { MenuEditDelete(); } }
        private void miEditFind_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditFind)) { MenuEditFind(); } }
        private void miEditCrop_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditCrop)) { MenuEditCrop(); } }
        private void miEditRotateLeft_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditRotateLeft)) { MenuEditRotateLeft(); } }
        private void miEditRotateRight_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditRotateRight)) { MenuEditRotateRight(); } }
        private void miEditRotate180_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditRotate180)) { MenuEditRotate180(); } }
        private void miEditFlipHorizontally_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditFlipHoriz)) { MenuEditFlipHoriz(); } }
        private void miEditFlipVertically_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditFlipVert)) { MenuEditFlipVert(); } }
        private void miEditConvertHalf_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditConvertHalf)) { MenuEditConvertHalf(); } }
        private void miEditFillBlocks_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditFillBlocks)) { MenuEditFillBlocks(); } }
        private void miEditOutline_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditOutline)) { MenuEditOutline(); } }
        private void miEditLiveSquares_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.EditLiveSquares)) { MenuEditLiveSquares(); } }
        private void miViewOptions_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ViewOptions)) { MenuViewOptions(); } }
        private void miViewColors_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ViewColors)) { MenuViewColors(); } }
        private void miViewInfo_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ViewInformation)) { MenuViewInfo(); } }
        private void miViewToolbar_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ViewToolbar)) { MenuViewToolbar(); } }
        private void miViewNoteTemplates_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ViewNoteTemplates)) { MenuViewNoteTemplates(); } }
        private void miViewTriggers_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ViewTriggers)) { MenuViewTriggers(); } }
        private void miViewZOrder_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ViewZOrder)) { MenuViewZOrder(); } }
        private void miView100pc_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.View100Percent)) { MenuView100pc(); } }
        private void miView200pc_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.View200Percent)) { MenuView200pc(); } }
        private void miView150pc_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.View100Percent)) { MenuView150pc(); } }
        private void miView300pc_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.View200Percent)) { MenuView300pc(); } }
        private void miViewFitWidth_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ViewFitWidth)) { MenuViewFitWidth(); } }
        private void miViewFitHeight_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ViewFitHeight)) { MenuViewFitHeight(); } }
        private void miViewFitInPanel_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ViewFitInPanel)) { MenuViewFitInPanel(); } }
        private void miViewFitWindow_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ViewFitWindow)) { MenuViewFitWindow(); } }
        private void miModePlay_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ModePlay)) { MenuModePlay(); } }
        private void miModeBlock_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ModeBlock)) { MenuModeBlock(); } }
        private void miModeFill_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ModeFill)) { MenuModeFill(); } }
        private void miModeLine_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ModeLine)) { MenuModeLine(); } }
        private void miModeHybrid_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ModeHybrid)) { MenuModeHybrid(); } }
        private void miModeNotes_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ModeNotes)) { MenuModeNotes(); } }
        private void miModeKeyboard_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ModeKeyboard)) { MenuModeKeyboard(); } }
        private void miModeEdit_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ModeEdit)) { MenuModeEdit(); } }
        private void miSheetAdd_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.SheetAdd)) { MenuSheetAdd(); } }
        private void miSheetClone_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.SheetClone)) { MenuSheetClone(); } }
        private void miSheetRemove_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.SheetRemove)) { MenuSheetRemove(); } }
        private void miSheetExpand_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.SheetExpand)) { MenuSheetExpand(); } }
        private void miSheetGoto_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.SheetGoto)) { MenuSheetGoto(); } }
        private void miSheetOrganize_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.SheetOrganize)) { MenuSheetOrganize(); } }
        private void miSheetLabels_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.SheetLabels)) { MenuSheetLabels(); } }
        private void miSheetClearVisited_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.SheetClearVisitedSquares)) { MenuSheetClearVisited(); } }
        private void miGameViewParty_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameViewParty)) { MenuGameViewParty(); } }
        private void miGameShowSpells_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameShowSpells)) { MenuGameShowSpells(); } }
        private void miGameShowMonsters_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameShowMonsters)) { MenuGameShowMonsters(); } }
        private void miGameShowItems_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameShowItems)) { MenuGameShowItems(); } }
        private void miGameShowInfo_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameShowGameInformation)) { MenuGameShowInfo(); } }
        private void miGameShowQuests_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameShowQuests)) { MenuGameShowQuests(); } }
        private void miGameScripts_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameShowScripts)) { MenuGameScripts(); } }
        private void miGameQuickRef_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameShowQuickReference)) { MenuGameQuickRef(); } }
        private void miGameTrainingAssistant_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameTrainingAssistant)) { MenuGameTrainingAssistant(); } }
        private void miGameCreationAssistant_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameCharacterCreationAssistant)) { MenuGameCreationAssistant(); } }
        private void miGameShowShops_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameShowShopInventories)) { MenuGameShowShops(); } }
        private void miGameShowEncounters_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameShowEncountersWhenInCombat)) { MenuGameShowEncounters(); } }
        private void miGameEditRoster_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameEditRoster)) { MenuGameEditRoster(); } }
        private void miGameResetHacker_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.ResetHacker)) { MenuGameResetHacker(); } }
        private void miGameEditCartography_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.GameEditInGameCartographyData)) { MenuGameEditCartography(); } }
        private void miHelpRunWizard_Click(object sender, EventArgs e) { if (!CheckLastShortcut(Action.HelpRunWizard)) { MenuHelpRunWizard(); } }

        void m_iconsForm_OnIconSelected(object sender, EventArgs e)
        {
            SetIconMode();
        }

        public void SetIconMode()
        {
            switch (Mode)
            {
                case BlockMode.Block:
                case BlockMode.Hybrid:
                    break;
                default:
                    // Set block mode, otherwise the user selects an icon and can't use it
                    SetMode(BlockMode.Block, false);
                    break;

            }
            CheckCurrentIcon();
        }

        void m_timerHideTips_Tick(object sender, EventArgs e)
        {
            m_tipZoom.RemoveAll();
            m_tipReadOnly.RemoveAll();
            m_timerHideTips.Stop();
        }

        void m_timerFocusAll_Tick(object sender, EventArgs e)
        {
            m_timerFocusAll.Stop();
            m_bFocusingWindows = false;
        }

        public bool ManualSpellWindowClose { get; set; }

        private Form[] GetForms()
        {
            return new Form[] { m_formCreationAssistant, m_formInfo, m_formMonsters, m_formItems, m_formParty, m_formSearch,
                m_formStrings, m_formTrainingAssistant, m_formEncounters, m_formShopInventory, m_formSpells, m_formQuests,
                m_formScripts, m_formQuickRef, m_formEditLabels };
        }

        public Dictionary<WindowType, Form> GetFormsByType()
        {
            Dictionary<WindowType, Form> forms = new Dictionary<WindowType, Form>();
            foreach (Form form in GetForms())
                if (Global.FormVisible(form) && form is HackerBasedForm)
                    forms.Add(((HackerBasedForm)form).WindowType, form);
            forms.Add(WindowType.Main, this);
            return forms;
        }

        public Dictionary<IntPtr, Form> Forms
        {
            get
            {
                Dictionary<IntPtr, Form> forms = new Dictionary<IntPtr, Form>();
                foreach (Form form in GetForms())
                    if (Global.FormVisible(form) && form is HackerBasedForm)
                        forms.Add(form.Handle, form);
                forms.Add(Handle, this);
                return forms;
            }
        }

        public BlockMode Mode
        {
            get
            {
                if (miModeBlock.Checked)
                    return BlockMode.Block;
                if (miModeLine.Checked)
                    return BlockMode.Line;
                if (miModeHybrid.Checked)
                    return BlockMode.Hybrid;
                if (miModeNotes.Checked)
                    return BlockMode.Notes;
                if (miModeKeyboard.Checked)
                    return BlockMode.Keyboard;
                if (miModeEdit.Checked)
                    return BlockMode.Edit;
                if (miModePlay.Checked)
                    return BlockMode.Play;
                if (miModeFill.Checked)
                    return BlockMode.Fill;
                return BlockMode.None;
            }
        }

        private bool IgnoreInaccessible { get { return Game == GameNames.MightAndMagic1 || Game == GameNames.MightAndMagic2; } }

        private bool Redraw(bool bChangesOnly = true)
        {
            Bitmap bmp = null;
            try
            {
                DrawParams dp = new DrawParams(
                    bChangesOnly,
                    m_bDrawCursor,
                    Mode == BlockMode.Edit || Mode == BlockMode.Fill || m_bEditingLiveSquares,
                    m_mapBook.UnvisitedPattern,
                    IgnoreInaccessible,
                    m_bEditingLiveSquares);
                bmp = CurrentSheet.CreateImage(this, dp);
                CurrentSheet.SetActiveSquaresDrawn();
                if (dp.MapChanged)
                {
                    SetUnsaved();
                    if (CurrentSheet.YouAreHere != null && Properties.Settings.Default.HideUnvisitedDottedLines)
                    {
                        foreach (Point pt in Global.PointsAround(CurrentSheet.YouAreHere.PrimaryCoordinates))
                            CurrentSheet.SetDirty(pt);
                        SetDirty();
                    }
                }
            }
            catch (Exception ex)
            {
                int iNewZoom = CurrentSheet.CurrentZoom / 2;
                if (iNewZoom < 18)
                    iNewZoom = 25;
                m_showNext = new ShowNext(String.Format("Could not create a bitmap for a {0}x{1} grid at zoom level {2}.  The zoom level will be set to {3}.\r\nException: {4}",
                    CurrentSheet.GridWidth, CurrentSheet.GridHeight, CurrentSheet.CurrentZoom, iNewZoom, ex.Message), "Error creating bitmap");
                CurrentSheet.SetZoom(iNewZoom);
                return false;
            }

            if (bmp.Size != pbMain.Size)
            {
                m_bNeedCentering = true;
                pbMain.Size = bmp.Size;
                Global.CenterControl(pbMain);

                m_tipZoom.AutoPopDelay = 1500;
                m_tipZoom.SetToolTip(panelMain, String.Format("Zoom: {0}% ({1}x{2})", CurrentSheet.CurrentZoom, CurrentSheet.SquareSize.Width, CurrentSheet.SquareSize.Height));
                m_tipZoom.SetToolTip(pbMain, String.Format("Zoom: {0}% ({1}x{2})", CurrentSheet.CurrentZoom, CurrentSheet.SquareSize.Width, CurrentSheet.SquareSize.Height));
                m_timerHideTips.Stop();
                m_timerHideTips.Interval = 1500;
                m_timerHideTips.Start();

                return true;
            }

            if (m_bNeedCentering)
                Global.CenterControl(pbMain);

            pbMain.Image = bmp;
            m_bNeedCentering = false;

            CenterMap(m_ptFocusSquare);
            m_ptFocusSquare = Global.NullPoint;

            CheckShowScrollbars();
            return true;
        }

        private void ForceRefreshOnDisplay()
        {
            if (CurrentSheet == null)
                return;

            m_ptLastSquare = Global.NullPoint;
            CurrentSheet.ForceRefreshOnDisplay = true;
            UpdateLiveMap();
            SetDirty();
        }

        private void LoadFile(string strFile)
        {
            if (!CheckMapsUnlocked())
                return;

            if (m_bUnsaved && MessageBox.Show("Discard changes to current file?", "Open New File", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != System.Windows.Forms.DialogResult.OK)
                return;

            LoadFileWithoutConfirmation(strFile);
        }

        private bool CheckMapsUnlocked(bool bSilent = false)
        {
            if (m_bExportingMaps)
            {
                MessageBox.Show("Cannot change mapbook or launch games during export; please wait for the export process to finish (or cancel it)", "Exporting maps", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
            }

            return true;
        }

        public void OnSelectAll()
        {
            if (!InputControlFocused && CurrentSheet != null)
            {
                SetSelection(new Rectangle(0, 0, CurrentSheet.GridWidth, CurrentSheet.GridHeight));
                m_bRecaptureSelection = true;
            }
        }

        public void SetSelection(Rectangle rc)
        {
            m_rcSelection = rc;
            m_ptLastSquare = Point.Empty;
            SetDirty(rc, true);
        }

        public void OnEscapePressed()
        {
            if (m_rcSelection != Rectangle.Empty)
            {
                if (pbMain.Capture)
                    pbMain.Capture = false;
                CancelSelection();
            }
            if (pbMain.Capture && m_labelAtCursor != null && m_labelBeforeMove != null)
            {
                CurrentSheet.SetLabelSquaresDirty(m_labelBeforeMove);
                CurrentSheet.SetLabelSquaresDirty(m_labelAtCursor);
                CurrentSheet.Labels.Remove(m_labelAtCursor);
                CurrentSheet.Labels.Add(m_labelBeforeMove);
                m_labelAtCursor = null;
                m_labelBeforeMove = null;
                SetDirty();
            }
            else if (Mode == BlockMode.Notes && pbMain.Capture)
                m_bCancelNoteMove = true;
            if (pbMain.Capture)
                ForceMouseUp(m_btnCaptured);
            if (EditingNote)
                CancelNote();
        }

        private bool CheckCustomBackgrounds()
        {
            if (!Properties.Settings.Default.WarnIfMissingCustomBackground)
                return false;

            List<string> listFiles = new List<string>();
            foreach (MapSheet sheet in m_mapBook.Sheets)
            {
                if (sheet.UseUnvisitedBitmap && !File.Exists(sheet.UnvisitedBitmapFile))
                {
                    bool bAdd = false;
                    if (m_strCurrentFile.StartsWith(":"))
                        bAdd = true;
                    else
                    {
                        try
                        {
                            if (!File.Exists(Path.Combine(Path.GetDirectoryName(m_strCurrentFile), Path.GetFileName(sheet.UnvisitedBitmapFile))))
                                bAdd = true;
                        }
                        catch (Exception)
                        {
                            bAdd = true;
                        }
                    }
                    if (bAdd && !listFiles.Contains(sheet.UnvisitedBitmapFile))
                        listFiles.Add(sheet.UnvisitedBitmapFile);
                }
            }
            if (listFiles.Count > 0 && !m_bShowingCustomBackgroundError)
            {
                m_bShowingCustomBackgroundError = true;
                StringBuilder sb = new StringBuilder();
                foreach(string str in listFiles)
                {
                    sb.AppendFormat("{0}", str);
                    sb.AppendLine();
                }
                string strPlural = listFiles.Count > 1 ? "s" : "";
                DoNotShowAgainForm form = new DoNotShowAgainForm(String.Format("Missing image file{0}", strPlural),
                    String.Format("This map book is trying to use the following file{0}, which could not be found:\r\n\r\n{1}", strPlural, sb.ToString()));
                form.Owner = this;
                DelayFocus(form);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    if (form.DoNotShowAgain)
                        Properties.Settings.Default.WarnIfMissingCustomBackground = false;
                }
                form = null;
                m_bShowingCustomBackgroundError = false;
                return true;
            }
            return false;
        }

        private void DelayFocus(Form form)
        {
            m_formDelayFocus = form;
            m_timerDelayFocus.Start();
        }

        public void LoadFileFinished()
        {
            LoadBookExtraData();
            SelectSheet(0);
            m_lastActiveSquares = null;
            m_lastCartography = null;
            m_lastMonsterLocations = null;
            SetUnsaved(false, false);
            SetCaption();
            AddMRU(m_strCurrentFile);
            m_bOverrideAutoSwitch = true;
            CheckCorrectBookForGame();
            if (Global.FormVisible(m_formSearch))
                m_formSearch.SetMapBook(m_mapBook, null, false);
            m_bCheckBook = true;
            menuMaps.Enabled = false;
            ThreadStart ts = new ThreadStart(GenerateMapMenu);
            new Thread(ts).Start();
            UpdateLiveMap();
        }

        private void LoadFileThread(object file)
        {
            try
            {
                string strFile = (string)file;
                m_strCurrentFile = strFile;
                m_mapBook = m_serializer.Load(strFile);
                if (m_evtShutdown.WaitOne(0))
                    return;
                BeginInvoke(new VoidDelegate(LoadFileFinished));
            }
            catch (Exception ex)
            {
                BeginInvoke(new ErrorDelegate(CaughtThreadException), String.Format("The file \"{0}\" could not be opened.\r\nException: {1}", (string) file, ex.Message), "Error opening file");
            }
        }

        public void CaughtThreadException(string strMessage, string strCaption)
        {
            MessageBox.Show(strMessage, strCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void LoadFileWithoutConfirmation(string strFile, bool bAsync = true)
        {
            if (!CheckMapsUnlocked())
                return;

            if (threadLoadFile != null && threadLoadFile.IsAlive)
                return;

            if (String.IsNullOrWhiteSpace(strFile) || strFile == ":None")
                return;

            bool bRetry = true;
            while (bRetry)
            {
                try
                {
                    if (bAsync)
                    {
                        ParameterizedThreadStart ts = new ParameterizedThreadStart(LoadFileThread);
                        threadLoadFile = new Thread(ts);
                        threadLoadFile.Start(strFile);
                        return;
                    }
                    else
                    {
                        m_strCurrentFile = strFile;
                        m_mapBook = m_serializer.Load(strFile);
                        LoadFileFinished();
                        bRetry = false;
                    }
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("The file \"" + strFile + "\" could not be opened.\r\n\r\nException:  " + ex.Message, "Error opening file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Retry)
                    {
                        bRetry = false;
                        RemoveMRU(strFile);
                    }
                }
            }
        }

        private void AddMRU(string strFile)
        {
            if (strFile == Global.InternalMapString || strFile.StartsWith(":"))
                return;   // Don't add the internal maps to the MRU list

            if (Properties.Settings.Default.MRUList == null)
                Properties.Settings.Default.MRUList = new MRUFileList();

            if (!Properties.Settings.Default.MRUList.Paths.Contains(strFile))
            {
                Properties.Settings.Default.MRUList.Paths.Insert(0, strFile);
            }
            while (Properties.Settings.Default.MRUList.Paths.Count > 10)
                Properties.Settings.Default.MRUList.Paths.RemoveAt(Properties.Settings.Default.MRUList.Paths.Count - 1);

        }

        private void RemoveMRU(string strFile)
        {
            if (Properties.Settings.Default.MRUList.Paths.Contains(strFile))
            {
                Properties.Settings.Default.MRUList.Paths.Remove(strFile);
            }
        }

        public bool SetFlaggedQuests(string[] quests)
        {
            if (m_mapBook.SetFlaggedQuests(quests))
            {
                SetUnsaved();
                return true;
            }
            return false;
        }

        public bool SetManualCompletions(string[] quests, string[] tasks)
        {
            if (m_mapBook.SetManualCompletions(quests, tasks))
            {
                SetUnsaved();
                return true;
            }
            return false;
        }

        public void UpdateBookExtraData()
        {
            if (m_formQuests != null)
            {
                SetFlaggedQuests(m_formQuests.GetFlaggedQuests());
                SetManualCompletions(m_formQuests.GetManualCompletedQuests(), m_formQuests.GetManualCompletedTasks());
            }
        }

        public void LoadBookExtraData()
        {
            if (m_formQuests != null)
            {
                m_formQuests.SetFlaggedQuests(m_mapBook.FlaggedQuests);
                m_formQuests.SetManualCompletedItems(m_mapBook.ManualCompletedQuests, m_mapBook.ManualCompletedTasks);
            }
        }

        private bool SaveBook(MapBook book, string strFile)
        {
            bool bRetry = true;
            while (bRetry)
            {
                try
                {
                    m_serializer.Save(m_mapBook, m_strCurrentFile);
                    m_mapBook.LastFile = m_strCurrentFile;
                    bRetry = false;
                }
                catch (Exception ex)
                {
                    if (MessageBox.Show("The file \"" + strFile + "\" could not be written.\r\n\r\nException:  " + ex.Message, "Error writing to file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Retry)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool SaveCurrentFile()
        {
            if (!String.IsNullOrWhiteSpace(m_strCurrentFile) && !m_strCurrentFile.StartsWith(":") && m_strCurrentFile != Global.InternalMapString)
            {
                UpdateBookExtraData();
                if (SaveBook(m_mapBook, m_strCurrentFile))
                {
                    SetUnsaved(false, false);
                    AddMRU(m_strCurrentFile);
                    return true;
                }
                return false;
            }
            return MenuFileSaveAs();
        }

        private bool MenuFileSaveAs()
        {
            saveFileDialog.FileName = m_strCurrentFile;
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                UpdateBookExtraData();
                m_strCurrentFile = saveFileDialog.FileName;
                if (SaveBook(m_mapBook, m_strCurrentFile))
                {
                    SetUnsaved(false, false);
                    SetCaption();
                    AddMRU(m_strCurrentFile);
                    if (m_hacker != null && m_hacker.Running && m_hacker.Game == Properties.Settings.Default.Game)
                        AutoLoadFile =  m_strCurrentFile;
                    return true;
                }
            }
            return false;
        }

        private void SetCaption()
        {
            string strTitle = "Where Are We? - " + DisplayFileName() + (m_bUnsaved && !String.IsNullOrEmpty(m_strCurrentFile) ? "*" : "");
            if (Global.Debug)
                strTitle = GetDebugTitle(strTitle);

            if (m_strLastTitle == strTitle)
                return;

            if (InvokeRequired)
                Invoke((MethodInvoker)(() => Text = strTitle));
            else
                Text = strTitle;

            m_strLastTitle = strTitle;
        }

        public bool PerformAction(Action action)
        {
            return PerformAction(action, null);
        }

        public bool SquareIsVisible(Point ptLocation)
        {
            return SquareIsVisible(ptLocation, Global.NullPoint, 0);
        }

        public bool SquareIsVisible(Point ptLocation, Point ptFrom, int iVisibleRange)
        {
            if (CurrentSheet.IsLegend)
                return true;

            MapSquare square = CurrentSheet.GetSquareAtGridPoint(ptLocation);
            if (square == null)
                return false;
            if (square.Visited)
                return true;

            if (ptFrom != Global.NullPoint && square.Seen && Proximity.SimpleDistance(ptLocation, ptFrom) <= iVisibleRange)
                return true;

            if (!Properties.Settings.Default.HideUnvisitedSquares)
                return true;

            if (ptLocation.X == 0 || ptLocation.X == CurrentSheet.GridWidth - 1 || ptLocation.Y == 0 || ptLocation.Y == CurrentSheet.GridHeight - 1)
            {
                if (Properties.Settings.Default.AlwaysRevealEdges)
                    return true;
            }

            if (Properties.Settings.Default.RevealAdjacentInaccessible && square.IsUnimportant && !IgnoreInaccessible)
                return true;

            if (Mode == BlockMode.Edit || Mode == BlockMode.Fill)
                return true;

            return false;
        }

        public BoolHandled Draw(MouseButtons info, Action action = Action.Draw)
        {
            m_actionDraw = action;
            Point pt = GetSquareLocationAtMouse();
            if (m_lastMonsterLocations != null && Global.FormVisible(m_formEncounters) && VirtualMode == BlockMode.Play)
            {
                Point ptSquare = TranslateToGameMap(pt);
                if (m_lastMonsterLocations.MonsterPositions.ContainsKey(ptSquare))
                {
                    MonsterPosition pos = m_lastMonsterLocations.MonsterPositions[ptSquare];
                    if (pos.Monsters.Any(m => m.EncounterIndex != -1))
                    {
                        m_formEncounters.SelectMonsters(pos, NativeMethods.IsControlDown());
                        m_bSkipNextCenter = true;
                    }
                }
            }

            if (Properties.Settings.Default.ReadOnlyMaps)
                ShowReadOnlyMapMessage();
            else
            {
                if (VirtualMode == BlockMode.Live)
                {
                    MapSquare square = GetSquareAtMouse();
                    if (square != null)
                    {
                        square.SetLive(!square.Live);
                        UpdateLiveMap();
                        CurrentSheet.SetDirty(pt);
                        SetDirtyUnsaved();
                    }
                    return BoolHandled.None;
                }

                if (VirtualMode == BlockMode.Notes)
                {
                    m_bCancelNoteMove = false;
                    m_bLabelMoved = false;
                    m_ptOriginalCapture = pt; 
                    m_ptCursorAtContext = pbMain.PointToClient(Cursor.Position);
                    m_ptCursorAtMouseDown = m_ptCursorAtContext;
                    m_labelAtCursor = CurrentSheet.LabelInPoint(m_ptCursorAtContext);
                    if (m_labelAtCursor != null)
                    {
                        CurrentSheet.ClearRedo();
                        CurrentSheet.AddUndoLabels();
                        m_labelBeforeMove = m_labelAtCursor.Clone();    // In case we want to copy instead of move
                    }
                    m_selectionAction = NativeMethods.IsControlDown() ? SelectionActions.Copy : SelectionActions.Move;
                    m_captureMode = CaptureMode.Draw;
                    pbMain.Capture = true;
                    m_btnCaptured = info;
                    panelMain.Focus();
                    return BoolHandled.None;
                }

                m_labelAtCursor = null;

                if (EditingNote)
                    FinishNote();
                SetCursor(pt);
                if (VirtualMode != BlockMode.Edit && !SquareIsVisible(CurrentSheet.Cursor))
                    return BoolHandled.None;
                if (VirtualMode == BlockMode.Play)
                    return BoolHandled.None;  // "Play" mode means no editing
                if (m_iconsForm.SelectedIcon != null)
                {
                    // If an icon is selected, that takes precedence regardless of the mode
                    ToggleSquareIcon(m_iconsForm.SelectedIcon);
                    return BoolHandled.True;
                }
                if (VirtualMode == BlockMode.Keyboard)
                {
                    // In Keyboard mode, clicking a square just moves the cursor to that location
                    m_bCenterKeyboard = false;      // centering after clicking is confusing
                    SetDirty();
                    return BoolHandled.None;
                }
                else if (VirtualMode == BlockMode.Fill)
                {
                    CurrentSheet.ClearRedo();
                    m_undoBlocks = CurrentSheet.StartUndoBlock().Squares;
                    if (!NativeMethods.IsControlDown())
                    {
                        // Replace one color of blocks with another
                        Dictionary<Point, MapSquare> squares = CurrentSheet.GetFillList(pt.X, pt.Y, m_undoBlocks);
                        foreach(Point ptSquare in squares.Keys)
                        {
                            if (SquareIsVisible(pt))
                            {
                                squares[ptSquare].Colors.BackColorPattern = m_dcCurrentBlock.BlockStyle;
                                squares[ptSquare].SetDirty(DirtyType.Back);
                            }
                        }
                    }
                    else
                    {
                        // Draw lines around the fill area
                        CurrentSheet.SurroundLines(pt.X, pt.Y, m_dcCurrentLine.LineStyle, m_undoBlocks);
                    }
                    SetDirtyUnsaved();
                    return BoolHandled.None;
                }

                m_captureMode = CaptureMode.Draw;
                pbMain.Capture = true;
                m_bDrawStraightLine = NativeMethods.IsShiftDown();
                m_orStraightLine = Orient.None;
                m_btnCaptured = info;
                panelMain.Focus();

                if (VirtualMode == BlockMode.Edit)
                {
                    m_ptNextSquare = m_ptOriginalCapture = pt;

                    // If the cursor is inside a selection, move it instead of creating a new selection
                    if (m_rcSelection.Contains(pt))
                    {
                        if (NativeMethods.IsControlDown())
                            m_selectionAction = SelectionActions.Copy;
                        else
                            m_selectionAction = SelectionActions.Move;
                        pbMain.Cursor = m_selectionAction == SelectionActions.Copy ? Global.GetCursor(MouseCursor.Copy) : Global.GetCursor(MouseCursor.Default);
                        m_ptLastSquare = Global.NullPoint;
                    }
                    else
                    {
                        m_ptLastSquare = m_ptNextSquare;
                        CancelSelection();
                        m_selectionAction = SelectionActions.Create;
                    }

                    SetDirty();
                    return BoolHandled.None;
                }

                CurrentSheet.ClearRedo();
                m_undoBlocks = CurrentSheet.StartUndoBlock().Squares;
                switch (VirtualMode)
                {
                    case BlockMode.Block:
                        m_ptNextSquare = m_ptOriginalCapture = m_ptLastSquare = pt;
                        m_drawMode = CurrentSheet.Grid[m_ptLastSquare.X, m_ptLastSquare.Y].IsEmpty ? DrawMode.Fill : DrawMode.Erase;
                        DrawActionCurrentSquare(DrawMode.None);
                        break;
                    case BlockMode.Line:
                        m_ptNextSquare = m_ptOriginalCapture = m_ptLastVertex = GetVertexAtMouse();
                        m_drawMode = (NativeMethods.IsControlDown() ? DrawMode.Erase : DrawMode.Fill);
                        break;
                    case BlockMode.Hybrid:
                        m_ptNextSquare = m_ptOriginalCapture = m_ptLastSquare = pt;
                        m_drawMode = CurrentSheet.Grid[m_ptLastSquare.X, m_ptLastSquare.Y].IsEmpty ? DrawMode.Fill : DrawMode.Erase;
                        DrawActionCurrentSquare(DrawMode.Fill);
                        break;
                    default:
                        break;
                }
            }
            return BoolHandled.None;
        }

        public void ScrollSheet(MouseButtons info)
        {
            m_actionDraw = Action.ScrollSheet;
            m_captureMode = CaptureMode.Scroll;
            m_ptCaptureMove = Cursor.Position;
            m_ptCaptureScrollbars = new Point(-panelMain.AutoScrollPosition.X, -panelMain.AutoScrollPosition.Y);
            pbMain.Capture = true;
            panelMain.BlockNextContextMenu = true;
            m_btnCaptured = info;
        }

        private void AfterChangeSquareSize()
        {
            if (CurrentSheet.GridWidth * CurrentSheet.SquareSize.Width * CurrentSheet.GridHeight * CurrentSheet.SquareSize.Height > 50000000)
            {
                m_showNext = new ShowNext(String.Format("The zoom level ({0}) is too high for the sheet size ({1}x{2}) and will be reduced to {3}.",
                    CurrentSheet.CurrentZoom, CurrentSheet.GridWidth, CurrentSheet.GridHeight, CurrentSheet.CurrentZoom / 2), "Zoom level too high");
                CurrentSheet.SetZoom(CurrentSheet.CurrentZoom / 2);
            }
            if (pbMain.Capture)
                CancelSelection();
            else
                m_bRecaptureSelection = true;
            ForceRefreshOnDisplay();
        }

        public string CharacterName(int index = -1, bool raw = false)
        {
            string prefix = raw ? "" : "↺";
            string strName = "no character";
            if (m_hacker == null || !m_hacker.IsValid)
                return strName;
            if (index == -1)
                index = GetSelectedCharacterAddress();
            if (index == -1)
                return String.Empty;
            // The ↺ symbol at the beginning of the name directs the "Replace Variables" function to use the name verbatim without title-izing it
            if (Global.FormVisible(m_formParty))
            {
                BaseCharacter baseChar = m_formParty.GetCharacterByAddress(index);
                if (baseChar == null || String.IsNullOrWhiteSpace(baseChar.Name))
                    return prefix + strName;
                return prefix + baseChar.Name;
            }
            List<BaseCharacter> list = m_hacker.GetCharacters();
            if (list == null || index >= list.Count)
                return prefix + strName;

            return prefix + list[index].Name;
        }

        public SpellHotkeyList SpellHotkeys { get { return Games.GetSpellHotkeys(Game); } }

        public string GetActionChar(Action action, bool raw = false)
        {
            string strChar = "no character";
            if (action >= Action.SpellHotkey1 && action <= Action.SpellHotkey10)
            {
                SpellHotkeyList list = SpellHotkeys;
                if (list == null)
                    return strChar;
                SpellHotkey.HKCharacter hkChar = list.Hotkeys[action - Action.SpellHotkey1].Character;
                switch (hkChar)
                {
                    case SpellHotkey.HKCharacter.AllArcane: return "all arcane";
                    case SpellHotkey.HKCharacter.AllCharacters: return "all characters";
                    case SpellHotkey.HKCharacter.AllCleric: return "all cleric";
                    case SpellHotkey.HKCharacter.AllDruid: return "all druid";
                    case SpellHotkey.HKCharacter.CurrentCharacter: return CharacterName(Hacker.GetActingCharacterAddress());
                    default: return CharacterName(hkChar - SpellHotkey.HKCharacter.Character1, raw);
                }
            }
            else if (action >= Action.CureAll1 && action <= Action.CureAll8)
                return CharacterName(action - Action.CureAll1);
            else if (action >= Action.TradeBackpack1 && action <= Action.TradeBackpack8)
                return CharacterName(action - Action.TradeBackpack1);
            return strChar;
        }

        public string GetReadySpell(Action action)
        {
            string strSpell = "no spell";
            if (action >= Action.SpellHotkey1 && action <= Action.SpellHotkey10)
            {
                SpellHotkeyList list = SpellHotkeys;
                if (list == null)
                    return strSpell;

                int iSpell = list.Hotkeys[action - Action.SpellHotkey1].SpellIndex;
                if (iSpell == -1)
                    return strSpell;
                int iResolvedSpell = CheckFavoriteSpells(iSpell, Hacker.Game, GetActionChar(action, true));
                return Games.GetSpellName(Game, iResolvedSpell, iSpell);
            }
            return strSpell;
        }

        public string GetSpellAction(Action action)
        {
            string strAction = "ready spell set to";
            if (action >= Action.SpellHotkey1 && action <= Action.SpellHotkey10)
            {
                SpellHotkeyList list = SpellHotkeys;
                if (list == null)
                    return strAction;
                SpellHotkey.HKCharacter hkChar = list.Hotkeys[action - Action.SpellHotkey1].Character;
                switch (hkChar)
                {
                    case SpellHotkey.HKCharacter.CurrentCharacter: return "casting spell";
                    default: return strAction;
                }
            }
            return strAction;
        }

        public BaseCharacter GetCharacterByPosition(int iChar = -1)
        {
            if (iChar == -1)
                iChar = GetSelectedCharacterPosition();
            if (iChar == -1)
                return null;

            BaseCharacter baseChar = null;
            if (Global.FormVisible(m_formParty))
                baseChar = m_formParty.GetCharacterByPosition(iChar);
            else
            {
                List<BaseCharacter> list = m_hacker.GetCharacters();
                if (list == null || iChar >= list.Count)
                    return null;
                return list[iChar];
            }
            return baseChar;
        }

        public BaseCharacter GetCharacterByAddress(int iCharAddress)
        {
            if (iCharAddress == -1)
                iCharAddress = GetSelectedCharacterAddress();
            if (iCharAddress == -1)
                return null;

            if (Global.FormVisible(m_formParty))
                return m_formParty.GetCharacterByAddress(iCharAddress);
            else
            {
                List<BaseCharacter> list = m_hacker.GetCharacters();
                if (list == null || iCharAddress >= list.Count)
                    return null;
                return list.FirstOrDefault(c => c.BasicAddress == iCharAddress);
            }
        }

        public string GetReadySpell(int iChar = -1)
        {
            string strSpell = "no spell";
            if (m_hacker == null || !m_hacker.IsValid)
                return strSpell;
            BaseCharacter baseChar = GetCharacterByPosition(iChar);
            if (baseChar == null)
                return strSpell;
            return baseChar.ReadySpellString;
        }

        public void ReplaceVariable(StringBuilder sb, string strSearch, string strReplace)
        {
            if (sb == null || sb.Length < 2 || String.IsNullOrWhiteSpace(strSearch) || strSearch.Length < 2 || String.IsNullOrWhiteSpace(strReplace))
                return;

            string strUpper = String.Format("${0}{1}", Char.ToUpper(strSearch[0]), strSearch.Substring(1));
            string strLower = String.Format("${0}", strSearch);

            bool bVerbatim = false;
            if (strReplace.StartsWith("↺"))
            {
                bVerbatim = true;
                strReplace = strReplace.Substring(1);
            }
            sb.Replace(strLower, strReplace);
            sb.Replace(strUpper, bVerbatim ? strReplace : Global.Title(strReplace));
        }

        public string CoordString(Point pt) { return String.Format("{0},{1}", pt.X, pt.Y); }
        public string SizeString(Size sz) { return String.Format("{0}x{1}", sz.Width, sz.Height); }
        public string RectString(Rectangle rc) { return String.Format("{0},{1} ({2}x{3})", rc.X, rc.Y, rc.Width, rc.Height); }

        public string ReplaceVariables(Action action, string str, bool bSuccess, bool bEnabled)
        {
            if (!str.Contains('$'))
                return str.Replace(@"\n", "\n");

            StringBuilder sb = new StringBuilder(str);
            sb.Replace(@"\n", "\n");
            try
            {
                ReplaceVariable(sb, "actionChar", GetActionChar(action));
                ReplaceVariable(sb, "curChar", CharacterName());
                ReplaceVariable(sb, "spellName", GetReadySpell(action));
                ReplaceVariable(sb, "spellAction", GetSpellAction(action));
                ReplaceVariable(sb, "curGame", Games.Name(Game));
                ReplaceVariable(sb, "curMap", CurrentSheet.Title);
                ReplaceVariable(sb, "curCoord", CoordString(m_lastLocation.PrimaryCoordinates));
                ReplaceVariable(sb, "curLocation", String.Format("{0}: {1}", CurrentSheet.Title, CoordString(m_lastLocation.PrimaryCoordinates)));
                ReplaceVariable(sb, "curMouse", CoordString(GetSquareLocationAtMouse()));
                ReplaceVariable(sb, "curCursor", CoordString(CurrentSheet.Cursor));
                ReplaceVariable(sb, "curZoom", String.Format("{0}", CurrentSheet.CurrentZoom));
                ReplaceVariable(sb, "curSquareSize", SizeString(CurrentSheet.SquareSize));
                ReplaceVariable(sb, "curSheetSize", String.Format("{0}x{1}", CurrentSheet.GridWidth, CurrentSheet.GridHeight));
                ReplaceVariable(sb, "curSheets", String.Format("{0}", m_mapBook.Sheets.Count));
                ReplaceVariable(sb, "bookTitle", m_mapBook.Title);
                ReplaceVariable(sb, "enabledState", bEnabled ? "enabled" : "disabled");
                ReplaceVariable(sb, "successState", bSuccess ? "succeeded" : "failed");
                ReplaceVariable(sb, "mode", BlockModeItem.Name(Mode));
                ReplaceVariable(sb, "fileName", m_strCurrentFile);
                ReplaceVariable(sb, "curSelection", RectString(m_rcSelection));
                ReplaceVariable(sb, "character1", CharacterName(0));
                ReplaceVariable(sb, "character2", CharacterName(1));
                ReplaceVariable(sb, "character3", CharacterName(2));
                ReplaceVariable(sb, "character4", CharacterName(3));
                ReplaceVariable(sb, "character5", CharacterName(4));
                ReplaceVariable(sb, "character6", CharacterName(5));
                ReplaceVariable(sb, "character7", CharacterName(6));
                ReplaceVariable(sb, "character8", CharacterName(7));
                ReplaceVariable(sb, "readySpell", GetReadySpell(-1));
                ReplaceVariable(sb, "ready1", GetReadySpell(0));
                ReplaceVariable(sb, "ready2", GetReadySpell(1));
                ReplaceVariable(sb, "ready3", GetReadySpell(2));
                ReplaceVariable(sb, "ready4", GetReadySpell(3));
                ReplaceVariable(sb, "ready5", GetReadySpell(4));
                ReplaceVariable(sb, "ready6", GetReadySpell(5));
                ReplaceVariable(sb, "ready7", GetReadySpell(6));
                ReplaceVariable(sb, "ready8", GetReadySpell(7));
            }
            catch(Exception)
            {
                return sb.ToString();
            }
            return sb.ToString();
        }

        private void CheckNotifications(Action action, bool bSuccessState = true, bool bEnabledState = true)
        {
            if (!Properties.Settings.Default.Notifications.Enabled)
                return;

            if (!Properties.Settings.Default.Notifications.Contains(action))
                return;

            Notification alert = Properties.Settings.Default.Notifications.GetAlert(action);
            if (alert == null || !alert.Any)
                return;

            if (alert.AudioType)
            {
                string strFile = ReplaceVariables(action, alert.AudioFile, bSuccessState, bEnabledState);
                alert.PlayAudio(false, strFile);
            }

            if (alert.TextType)
            {
                string strAlert = ReplaceVariables(action, alert.Message, bSuccessState, bEnabledState);
                if (m_formNotification == null || m_formNotification.IsDisposed)
                    m_formNotification = new NotificationForm(strAlert);
                else
                    m_formNotification.Restart(strAlert);
                m_formNotification.Show();
            }
        }

        private bool ToggleSetting(bool bCurrent, out bool bNew, UpdateType update = UpdateType.Refresh)
        {
            bNew = !bCurrent;
            if (update.HasFlag(UpdateType.Dirty))
                SetDirty();
            if (update.HasFlag(UpdateType.Encounters))
                RefreshEncounterWindow();
            if (update.HasFlag(UpdateType.Quests))
                RefreshQuestWindow();
            if (update.HasFlag(UpdateType.Refresh))
                ForceRefreshOnDisplay();
            if (update.HasFlag(UpdateType.RefreshBook))
            {
                foreach (MapSheet sheet in m_mapBook.Sheets)
                    sheet.ForceRefreshOnDisplay = true;
            }
            if (update.HasFlag(UpdateType.Unsaved))
                SetUnsaved();
            return !bCurrent; 
        }

        public bool PerformAction(Action action, object info)
        {
            bool bSuccess = true;
            bool bEnabled = true;
            Properties.Settings settings = Properties.Settings.Default;

            try
            {
                // Almost any action will make the pop-up menu irrelevant
                cmGrid.Hide();
                switch (action)
                {
                    case Action.DecreaseSquareSize:
                        m_ptFocusSquare = GetSquareLocationAtMouse();
                        if (CurrentSheet.ChangeSize(-1, false))
                            AfterChangeSquareSize();
                        break;
                    case Action.IncreaseSquareSize:
                        m_ptFocusSquare = GetSquareLocationAtMouse();
                        if (CurrentSheet.ChangeSize(1, false))
                            AfterChangeSquareSize();
                        break;
                    case Action.DecreaseSquareSizeFixed:
                        m_ptFocusSquare = GetSquareLocationAtMouse();
                        if (CurrentSheet.ChangeSize(-1, true))
                            AfterChangeSquareSize();
                        break;
                    case Action.IncreaseSquareSizeFixed:
                        m_ptFocusSquare = GetSquareLocationAtMouse();
                        if (CurrentSheet.ChangeSize(1, true))
                            AfterChangeSquareSize();
                        break;
                    case Action.ScrollSheet:
                        ScrollSheet((MouseButtons)info);
                        break;
                    case Action.DrawScroll:
                        if (Mode != BlockMode.Play)
                        {
                            if (Draw((MouseButtons)info) == BoolHandled.True)
                                return true;
                        }
                        else
                            ScrollSheet((MouseButtons)info);
                        break;
                    case Action.MoveLabels:
                    case Action.Draw:
                    case Action.DrawBlocks:
                    case Action.DrawLines:
                    case Action.DrawHybrid:
                    case Action.DrawFill:
                        if (Draw((MouseButtons)info, action) == BoolHandled.True)
                            return true;
                        break;
                    case Action.SheetPrevious:
                        ProcessSheetPrevious();
                        break;
                    case Action.SheetNext:
                        ProcessSheetNext();
                        break;
                    case Action.SheetGoto:
                        MenuSheetGoto();
                        break;
                    case Action.MoveCursorNW:
                    case Action.MoveCursorN:
                    case Action.MoveCursorNE:
                    case Action.MoveCursorW:
                    case Action.MoveCursorE:
                    case Action.MoveCursorSW:
                    case Action.MoveCursorS:
                    case Action.MoveCursorSE:
                        ProcessMoveCursor(action);
                        break;
                    case Action.ToggleLineNW:
                    case Action.ToggleLineN:
                    case Action.ToggleLineNE:
                    case Action.ToggleLineW:
                    case Action.ToggleLineE:
                    case Action.ToggleLineSW:
                    case Action.ToggleLineS:
                    case Action.ToggleLineSE:
                    case Action.ToggleLineAll:
                    case Action.ToggleDoubleLineNW:
                    case Action.ToggleDoubleLineN:
                    case Action.ToggleDoubleLineNE:
                    case Action.ToggleDoubleLineW:
                    case Action.ToggleDoubleLineE:
                    case Action.ToggleDoubleLineSW:
                    case Action.ToggleDoubleLineS:
                    case Action.ToggleDoubleLineSE:
                    case Action.ToggleDoubleLineAll:
                        if (Mode == BlockMode.Keyboard)
                        {
                            if (settings.ReadOnlyMaps)
                                ShowReadOnlyMapMessage();
                            else
                                ProcessLineToggle(action);
                        }
                        break;

                    case Action.MoveBitmapNW:
                    case Action.MoveBitmapN:
                    case Action.MoveBitmapNE:
                    case Action.MoveBitmapW:
                    case Action.MoveBitmapE:
                    case Action.MoveBitmapSW:
                    case Action.MoveBitmapS:
                    case Action.MoveBitmapSE:
                        ProcessMoveBitmap(action);
                        break;

                    case Action.MoveBitmap10NW:
                    case Action.MoveBitmap10N:
                    case Action.MoveBitmap10NE:
                    case Action.MoveBitmap10W:
                    case Action.MoveBitmap10E:
                    case Action.MoveBitmap10SW:
                    case Action.MoveBitmap10S:
                    case Action.MoveBitmap10SE:
                        ProcessMoveBitmap(action, 10);
                        break;

                    case Action.ToggleBackground:
                        if (Mode == BlockMode.Keyboard)
                        {
                            if (settings.ReadOnlyMaps)
                                ShowReadOnlyMapMessage();
                            else
                            {
                                CurrentSheet.ClearRedo();
                                CurrentSheet.AddUndoCursorBlock();
                                if (CurrentSheet.CursorSquare.IsEmpty)
                                    CurrentSheet.CursorSquare.Fill(m_dcCurrentBlock);
                                else
                                    CurrentSheet.CursorSquare.Empty();
                                SetDirtyUnsaved();
                            }
                        }
                        break;
                    case Action.ExpandSheetNW:
                    case Action.ExpandSheetN:
                    case Action.ExpandSheetNE:
                    case Action.ExpandSheetW:
                    case Action.ExpandSheetE:
                    case Action.ExpandSheetSW:
                    case Action.ExpandSheetS:
                    case Action.ExpandSheetSE:
                    case Action.ExpandSheetAll:
                        if (settings.ReadOnlyMaps)
                            ShowReadOnlyMapMessage();
                        else
                            ProcessExpandSheet(action);
                        break;

                    case Action.SelectBlock1:
                    case Action.SelectBlock2:
                    case Action.SelectBlock3:
                    case Action.SelectBlock4:
                    case Action.SelectBlock5:
                    case Action.SelectBlock6:
                    case Action.SelectBlock7:
                    case Action.SelectBlock8:
                    case Action.SelectBlock9:
                    case Action.SelectBlock10:
                        SelectBlock(action - Action.SelectBlock1);
                        break;

                    case Action.SelectLine1:
                    case Action.SelectLine2:
                    case Action.SelectLine3:
                    case Action.SelectLine4:
                    case Action.SelectLine5:
                    case Action.SelectLine6:
                    case Action.SelectLine7:
                    case Action.SelectLine8:
                    case Action.SelectLine9:
                    case Action.SelectLine10:
                        SelectLine(action - Action.SelectLine1);
                        break;

                    case Action.NoteTemplate1:
                    case Action.NoteTemplate2:
                    case Action.NoteTemplate3:
                    case Action.NoteTemplate4:
                        UseNoteTemplate(action - Action.NoteTemplate1);
                        break;

                    case Action.ToggleLastBlock:
                        SelectBlockColor(m_dcLastBlock);
                        break;

                    case Action.ToggleLastLine:
                        SelectLineColor(m_dcLastLine);
                        break;

                    case Action.NextLineStyle:
                        CycleLineStyle(true);
                        break;

                    case Action.PrevLineStyle:
                        CycleLineStyle(false);
                        break;

                    case Action.NextCharacter:
                        SelectNextCharacter(1);
                        break;

                    case Action.PrevCharacter:
                        SelectNextCharacter(-1);
                        break;

                    case Action.OptionsMaps:
                    case Action.OptionsMisc:
                    case Action.OptionsKeyboard:
                    case Action.OptionsMouse:
                    case Action.OptionsPlay:
                    case Action.OptionsWindows:
                    case Action.OptionsSpells:
                    case Action.OptionsEncounter:
                    case Action.OptionsQuests:
                    case Action.OptionsDOSBox:
                        MenuViewOptions(action - Action.OptionsMaps);
                        break;

                    case Action.NextBlockStyle:
                        CycleBlockStyle(true);
                        break;

                    case Action.PrevBlockStyle:
                        CycleBlockStyle(false);
                        break;

                    case Action.NextIcon:
                        SelectNextIcon(1);
                        break;

                    case Action.PrevIcon:
                        SelectNextIcon(-1);
                        break;

                    case Action.NextBlockIndex:
                        SelectNextBlockIndex(1);
                        break;

                    case Action.PrevBlockIndex:
                        SelectNextBlockIndex(-1);
                        break;

                    case Action.NextLineIndex:
                        SelectNextLineIndex(1);
                        break;

                    case Action.PrevLineIndex:
                        SelectNextLineIndex(-1);
                        break;

                    case Action.QuickDoor:
                        switch (Mode)
                        {
                            case BlockMode.Block:
                            case BlockMode.Line:
                            case BlockMode.Hybrid:
                                if (ActiveForm != this)
                                    return false;
                                CycleDoorIcon();
                                break;
                            default:
                                return false;
                        }
                        break;

                    case Action.RotateIconCW:
                        RotateCurrentIcon(1);
                        break;

                    case Action.RotateIconCCW:
                        RotateCurrentIcon(-1);
                        break;

                    case Action.Refresh:
                        ForceRefreshAll();
                        break;

                    case Action.SkipIntroductions:
                        SkipIntroductions();
                        break;

                    case Action.AutoCombat:
                        AutoCombat();
                        break;

                    case Action.DisarmTrap:
                        DisarmTrap();
                        break;

                    case Action.CopyMapText:
                        Clipboard.SetText(m_hacker.GetMapStrings());
                        break;

                    case Action.TeleportPartyNW:
                    case Action.TeleportPartyN:
                    case Action.TeleportPartyNE:
                    case Action.TeleportPartyW:
                    case Action.TeleportPartyE:
                    case Action.TeleportPartySW:
                    case Action.TeleportPartyS:
                    case Action.TeleportPartySE:
                        if (Global.Cheats)
                            TeleportPartyDirection(Global.DirectionForAction(action));
                        break;

                    case Action.TeleportPartyForward:
                    case Action.TeleportPartyBackward:
                    case Action.TeleportPartyLeft:
                    case Action.TeleportPartyRight:
                    case Action.TeleportPartyForwardLeft:
                    case Action.TeleportPartyForwardRight:
                    case Action.TeleportPartyBackwardLeft:
                    case Action.TeleportPartyBackwardRight:
                        if (Global.Cheats)
                            TeleportPartyDirection(Global.DirectionForAction(action, m_hacker));
                        break;

                    case Action.ToggleMapScrollbars:
                        settings.HideScrollbarsInPlayMode = ToggleSetting(settings.HideScrollbarsInPlayMode, out bEnabled, UpdateType.None);
                        CheckShowScrollbars();
                        SetDirty();
                        break;

                    case Action.ToggleShowGridLines:
                        settings.ShowGridLines = ToggleSetting(settings.ShowGridLines, out bEnabled);
                        break;

                    case Action.ToggleShowLabels:
                        settings.ShowMapLabels = ToggleSetting(settings.ShowMapLabels, out bEnabled);
                        break;

                    case Action.SelectCharacter1:
                    case Action.SelectCharacter2:
                    case Action.SelectCharacter3:
                    case Action.SelectCharacter4:
                    case Action.SelectCharacter5:
                    case Action.SelectCharacter6:
                    case Action.SelectCharacter7:
                    case Action.SelectCharacter8:
                        bSuccess = SelectCharacter(action - Action.SelectCharacter1);
                        break;

                    case Action.FileSave:
                        SaveCurrentFile();
                        break;

                    case Action.ResetHacker:
                        bSuccess = MenuGameResetHacker();
                        break;

                    case Action.GameCharacterCreationAssistant:
                        m_showNext = new ShowNext(ShowNextType.CreationAssistant);
                        break;

                    case Action.GameTrainingAssistant:
                        m_showNext = new ShowNext(ShowNextType.TrainingAssistant);
                        break;

                    case Action.GameLaunchCurrentGame:
                        LaunchCurrentGame();
                        break;

                    case Action.ViewBringDOSBoxToForeground:
                        BringDOSBoxToFront();
                        break;

                    case Action.LoadRecent1:
                    case Action.LoadRecent2:
                    case Action.LoadRecent3:
                    case Action.LoadRecent4:
                        LoadRecentMap(action - Action.LoadRecent1);
                        break;

                    case Action.CureAllSilent:
                    case Action.CureAll1:
                    case Action.CureAll2:
                    case Action.CureAll3:
                    case Action.CureAll4:
                    case Action.CureAll5:
                    case Action.CureAll6:
                    case Action.CureAll7:
                    case Action.CureAll8:
                        bSuccess = ProcessCureAll(action);
                        break;

                    case Action.GameViewParty:
                        m_showNext = new ShowNext(ShowNextType.PartyWindow);
                        break;

                    case Action.GameShowSpells:
                        m_showNext = new ShowNext(ShowNextType.SpellWindow);
                        break;

                    case Action.CloseEncounterWindow:
                        m_showNext = new ShowNext(ShowNextType.EncounterWindow);
                        break;

                    case Action.ShowLegend:
                        ToggleLegend();
                        break;

                    case Action.TradeBackpack1:
                    case Action.TradeBackpack2:
                    case Action.TradeBackpack3:
                    case Action.TradeBackpack4:
                    case Action.TradeBackpack5:
                    case Action.TradeBackpack6:
                    case Action.TradeBackpack7:
                    case Action.TradeBackpack8:
                        bSuccess = m_formParty.TradeBackpacks((int)(action - Action.TradeBackpack1));
                        break;

                    case Action.TeleportToCursor:
                        if (CurrentSheet != null && Global.Cheats)
                            TeleportTo(TranslateToGameMap(CurrentSheet.Cursor));
                        break;

                    case Action.SpellHotkey1:
                    case Action.SpellHotkey2:
                    case Action.SpellHotkey3:
                    case Action.SpellHotkey4:
                    case Action.SpellHotkey5:
                    case Action.SpellHotkey6:
                    case Action.SpellHotkey7:
                    case Action.SpellHotkey8:
                    case Action.SpellHotkey9:
                    case Action.SpellHotkey10:
                        bSuccess = ProcessSpellHotkey(action);
                        break;

                    case Action.GameResetMonstersOnMap:
                        ResetMonsters();
                        break;

                    case Action.GameRemoveAllMonstersFromMap:
                        RemoveMonsters();
                        break;

                    case Action.SetMapCartography:
                        if (Global.Cheats)
                            m_hacker.EditMapCartography(MapCartography.EditAction.FillSingle);
                        break;

                    case Action.ClearMapCartography:
                        if (Global.Cheats)
                            m_hacker.EditMapCartography(MapCartography.EditAction.ClearSingle);
                        break;

                    case Action.CopyLocation:
                        if (CurrentSheet != null && CurrentSheet.YouAreHere != null)
                            CopyLocationText(TranslateToGameMap(CurrentSheet.YouAreHere.PrimaryCoordinates));
                        break;

                    case Action.FileNew: MenuFileNew(); break;
                    case Action.FileOpen: MenuFileOpen(); break;
                    case Action.FileSaveAs: MenuFileSaveAs(); break;
                    case Action.FileExportPng: MenuFileExportPng(); break;
                    case Action.FileExportZip: MenuFileExportZip(); break;
                    case Action.FileExit: MenuFileExit(); break;
                    case Action.EditUndo: MenuEditUndo(); break;
                    case Action.EditRedo: MenuEditRedo(); break;
                    case Action.EditCut: MenuEditCut(); break;
                    case Action.EditCopy: MenuEditCopy(); break;
                    case Action.EditPaste: MenuEditPaste(); break;
                    case Action.EditDelete: MenuEditDelete(); break;
                    case Action.EditFind: MenuEditFind(); break;
                    case Action.EditCrop: MenuEditCrop(); break;
                    case Action.EditRotateLeft: MenuEditRotateLeft(); break;
                    case Action.EditRotateRight: MenuEditRotateRight(); break;
                    case Action.EditRotate180: MenuEditRotate180(); break;
                    case Action.EditFlipHoriz: MenuEditFlipHoriz(); break;
                    case Action.EditFlipVert: MenuEditFlipVert(); break;
                    case Action.EditConvertHalf: MenuEditConvertHalf(); break;
                    case Action.ViewOptions: MenuViewOptions(); break;
                    case Action.ViewColors: MenuViewColors(); break;
                    case Action.ViewInformation: MenuViewInfo(); break;
                    case Action.ViewToolbar: MenuViewToolbar(); break;
                    case Action.ViewNoteTemplates: MenuViewNoteTemplates(); break;
                    case Action.ViewTriggers: MenuViewTriggers(); break;
                    case Action.ViewZOrder: MenuViewZOrder(); break;
                    case Action.View100Percent: MenuView100pc(); break;
                    case Action.View200Percent: MenuView200pc(); break;
                    case Action.ViewFitWidth: MenuViewFitWidth(); break;
                    case Action.ViewFitHeight: MenuViewFitHeight(); break;
                    case Action.ViewFitInPanel: MenuViewFitInPanel(); break;
                    case Action.ViewFitWindow: MenuViewFitWindow(); break;
                    case Action.ModePlay: MenuModePlay(); break;
                    case Action.ModeBlock: MenuModeBlock(); break;
                    case Action.ModeLine: MenuModeLine(); break;
                    case Action.ModeHybrid: MenuModeHybrid(); break;
                    case Action.ModeNotes: MenuModeNotes(); break;
                    case Action.ModeKeyboard: MenuModeKeyboard(); break;
                    case Action.ModeEdit: MenuModeEdit(); break;
                    case Action.ModeFill: MenuModeFill(); break;
                    case Action.SheetAdd: MenuSheetAdd(); break;
                    case Action.SheetClone: MenuSheetClone(); break;
                    case Action.SheetRemove: MenuSheetRemove(); break;
                    case Action.SheetExpand: MenuSheetExpand(); break;
                    case Action.SheetOrganize: MenuSheetOrganize(); break;
                    case Action.SheetLabels: MenuSheetLabels(); break;
                    case Action.SheetClearVisitedSquares: MenuSheetClearVisited(); break;
                    case Action.GameShowMonsters: MenuGameShowMonsters(); break;
                    case Action.GameShowItems: MenuGameShowItems(); break;
                    case Action.GameShowGameInformation: MenuGameShowInfo(); break;
                    case Action.GameShowQuests: MenuGameShowQuests(); break;
                    case Action.GameShowShopInventories: MenuGameShowShops(); break;
                    case Action.GameShowScripts: MenuGameScripts(); break;
                    case Action.GameShowQuickReference: MenuGameQuickRef(); break;
                    case Action.GameShowEncountersWhenInCombat: MenuGameShowEncounters(); break;
                    case Action.GameEditRoster: MenuGameEditRoster(); break;
                    case Action.GameReAcquireGameProcess: MenuGameResetHacker(); break;
                    case Action.GameEditInGameCartographyData: MenuGameEditCartography(); break;
                    case Action.HelpRunWizard: MenuHelpRunWizard(); break;

                    case Action.BlockColorDialog:
                        SelectBlockColor();
                        break;

                    case Action.LineColorDialog:
                        SelectLineColor();
                        break;

                    case Action.MoveDOSBox:
                        if (settings.DosBoxWindowRect != Rectangle.Empty)
                            Hacker.SetDOSBoxPosition(settings.DosBoxWindowRect);
                        break;

                    case Action.WizardMinimal:
                        WizardForm.SetMinimal();
                        ForceRefreshOnDisplay();
                        RefreshEncounterWindow();
                        break;

                    case Action.WizardFaq:
                        WizardForm.SetFaqStyle();
                        ForceRefreshOnDisplay();
                        RefreshEncounterWindow();
                        break;

                    case Action.WizardFull:
                        WizardForm.SetFullInformation();
                        ForceRefreshOnDisplay();
                        RefreshEncounterWindow();
                        break;

                    case Action.CycleWizardModes:
                        WizardForm.CycleStyles();
                        ForceRefreshOnDisplay();
                        RefreshEncounterWindow();
                        break;
                    case Action.ToggleHideSquares:
                        settings.HideUnvisitedSquares = ToggleSetting(settings.HideUnvisitedSquares, out bEnabled, UpdateType.RefreshBookDirty);
                        break;
                    case Action.ToggleRevealSeenSquares:
                        settings.RevealSeenSquares = ToggleSetting(settings.RevealSeenSquares, out bEnabled, UpdateType.RefreshBookDirty);
                        break;
                    case Action.ToggleShowNotesUnvisited:
                        settings.ShowUnvisitedNotes = ToggleSetting(settings.ShowUnvisitedNotes, out bEnabled);
                        break;
                    case Action.ToggleRevealEdgeSquares:
                        settings.AlwaysRevealEdges = ToggleSetting(settings.AlwaysRevealEdges, out bEnabled);
                        break;
                    case Action.ToggleRevealInaccessible:
                        settings.RevealAdjacentInaccessible = ToggleSetting(settings.RevealAdjacentInaccessible, out bEnabled);
                        break;
                    case Action.ToggleHideUnvisitedDots:
                        settings.HideUnvisitedDottedLines = ToggleSetting(settings.HideUnvisitedDottedLines, out bEnabled);
                        break;
                    case Action.ToggleUseInGameCartography:
                        settings.UseInGameCartography = ToggleSetting(settings.UseInGameCartography, out bEnabled);
                        break;
                    case Action.ToggleShowActiveScripts:
                        settings.ShowActiveSquares = ToggleSetting(settings.ShowActiveSquares, out bEnabled);
                        break;
                    case Action.ToggleShowActiveEncountersOnly:
                        settings.ShowActiveEncountersOnly = ToggleSetting(settings.ShowActiveEncountersOnly, out bEnabled);
                        break;
                    case Action.ToggleReadOnlyMaps:
                        settings.ReadOnlyMaps = ToggleSetting(settings.ReadOnlyMaps, out bEnabled, UpdateType.None);
                        tseTitle.ReadOnly = settings.ReadOnlyMaps;
                        break;
                    case Action.ToggleReadOnlyNotes:
                        settings.ReadOnlyNotes = ToggleSetting(settings.ReadOnlyNotes, out bEnabled, UpdateType.None);
                        break;
                    case Action.CycleYouAreHereOpacity:
                        settings.YouAreHereOpacity += 10;
                        while (settings.YouAreHereOpacity > 100)
                            settings.YouAreHereOpacity = 30;
                        ForceRefreshOnDisplay();
                        break;
                    case Action.CycleMonsterIconOpacity:
                        settings.MonsterOpacity += 10;
                        while (settings.MonsterOpacity > 100)
                            settings.MonsterOpacity = 30;
                        ForceRefreshOnDisplay();
                        break;
                    case Action.CycleTreasureWindowOpacity:
                        settings.TreasureOpacity += 10;
                        while (settings.TreasureOpacity > 100)
                            settings.TreasureOpacity = 30;
                        ForceRefreshOnDisplay();
                        break;
                    case Action.CycleUnvisitedSquareOpacity:
                        settings.UnvisitedSquareOpacity += 10;
                        while (settings.UnvisitedSquareOpacity > 100)
                            settings.UnvisitedSquareOpacity = 30;
                        ForceRefreshOnDisplay();
                        break;
                    case Action.CycleSeenSquareOpacity:
                        settings.SeenSquareOpacity += 10;
                        while (settings.SeenSquareOpacity > 100)
                            settings.SeenSquareOpacity = 30;
                        ForceRefreshOnDisplay();
                        break;
                    case Action.CycleItemFormats:
                        CycleItemFormat();
                        break;
                    case Action.ToggleKeyboardHook:
                        settings.EnableGlobalShortcuts = ToggleSetting(settings.EnableGlobalShortcuts, out bEnabled, UpdateType.None);
                        if (settings.EnableGlobalShortcuts)
                            KeyboardHook.Start(this, m_shortcuts.KeysWanted);
                        else
                            KeyboardHook.Stop();
                        break;
                    case Action.ToggleMemoryWrite:
                        settings.EnableMemoryWrite = ToggleSetting(settings.EnableMemoryWrite, out bEnabled, UpdateType.None);
                        if (!settings.EnableMemoryWrite)
                            settings.EnableCheats = false;
                        if (Global.FormVisible(m_formParty))
                            m_formParty.UpdateUI();
                        break;
                    case Action.ToggleUpdateInGameCartography:
                        settings.UpdateCartWhenInaccessibleRevealed = ToggleSetting(settings.UpdateCartWhenInaccessibleRevealed, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleRestoreHPWithCureall:
                        settings.CureAllHPWithConditions = ToggleSetting(settings.CureAllHPWithConditions, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleCheat:
                        settings.EnableCheats = ToggleSetting(settings.EnableCheats, out bEnabled, UpdateType.None);
                        if (Global.Cheats)
                            settings.EnableMemoryWrite = true;
                        if (Global.FormVisible(m_formParty))
                            m_formParty.UpdateTitle();
                        break;
                    case Action.ToggleAutoCharacterSwitch:
                        settings.FollowCharWindows = ToggleSetting(settings.FollowCharWindows, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleAutoMapSwitch:
                        settings.AutoSwitchSheets = ToggleSetting(settings.AutoSwitchSheets, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleAutoShowNotes:
                        settings.AutoShowNoteAtLocation = ToggleSetting(settings.AutoShowNoteAtLocation, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleShowSpellsWhenCasting:
                        settings.ShowSpells = ToggleSetting(settings.ShowSpells, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleForceDOSBoxLocation:
                        settings.ForceDosBoxLocation = ToggleSetting(settings.ForceDosBoxLocation, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleForceDOSBoxSize:
                        settings.ForceDosBoxSize = ToggleSetting(settings.ForceDosBoxSize, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleWindowSnap:
                        settings.SnapWindowsToDOSBox = ToggleSetting(settings.SnapWindowsToDOSBox, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleShowEncountersInCombat:
                        MenuGameShowEncounters();
                        break;
                    case Action.ToggleShowTreasureWindow:
                        settings.ShowTreasureWindow = ToggleSetting(settings.ShowTreasureWindow, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleShowDeadMonsters:
                        settings.ShowDeadMonsters = ToggleSetting(settings.ShowDeadMonsters, out bEnabled, UpdateType.Encounters);
                        break;
                    case Action.ToggleShowMonstersOnMap:
                        settings.ShowMonstersOnMaps = ToggleSetting(settings.ShowMonstersOnMaps, out bEnabled, UpdateType.EncountersRefresh);
                        break;
                    case Action.ToggleShowOnlyNearbyMonsters:
                        settings.ShowOnlyDetectableMonsters = ToggleSetting(settings.ShowOnlyDetectableMonsters, out bEnabled, UpdateType.EncountersRefresh);
                        break;
                    case Action.ToggleShowActiveMonsterIcon:
                        settings.ShowActivatedMonstersIcon = ToggleSetting(settings.ShowActivatedMonstersIcon, out bEnabled);
                        break;
                    case Action.ToggleShowUnexploredMonsters:
                        settings.ShowListMonstersUnexplored = ToggleSetting(settings.ShowListMonstersUnexplored, out bEnabled, UpdateType.Encounters);
                        break;
                    case Action.ToggleHideScriptMonsters:
                        settings.HideScriptMonsters = ToggleSetting(settings.HideScriptMonsters, out bEnabled, UpdateType.EncountersRefresh);
                        break;
                    case Action.ToggleNearbyAndFlaggedBold:
                        settings.QuestBoldNearby = ToggleSetting(settings.QuestBoldNearby, out bEnabled, UpdateType.Quests);
                        break;
                    case Action.ToggleQuestGiver:
                        settings.QuestShowGiver = ToggleSetting(settings.QuestShowGiver, out bEnabled, UpdateType.Quests);
                        break;
                    case Action.ToggleQuestRewards:
                        settings.QuestShowReward = ToggleSetting(settings.QuestShowReward, out bEnabled, UpdateType.Quests);
                        break;
                    case Action.ToggleHideInvalidQuests:
                        settings.HideInvalidQuestGoals = ToggleSetting(settings.HideInvalidQuestGoals, out bEnabled, UpdateType.Quests);
                        RefreshQuestWindow();
                        break;
                    case Action.ToggleEditCopyBackgrounds:
                        settings.EditCopyBackgrounds = ToggleSetting(settings.EditCopyBackgrounds, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleEditCopyInnerLines:
                        settings.EditCopyInnerLines = ToggleSetting(settings.EditCopyInnerLines, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleEditCopyOuterLines:
                        settings.EditCopyOuterLines = ToggleSetting(settings.EditCopyOuterLines, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleEditCopyIcons:
                        settings.EditCopyIcons = ToggleSetting(settings.EditCopyIcons, out bEnabled, UpdateType.None);
                        break;
                    case Action.ToggleEditCopyNotes:
                        settings.EditCopyNotes = ToggleSetting(settings.EditCopyNotes, out bEnabled, UpdateType.None);
                        break;

                    default:
                        return false;
                }
            }
            finally
            {
                CheckNotifications(action, bSuccess, bEnabled);
                m_lastActionRun = Action.None;
            }

            return true;
        }

        public DrawColor GetLineColor(int iIndex)
        {
            if (iIndex < Properties.Settings.Default.DrawColors.Lines.Count)
                return Properties.Settings.Default.DrawColors.Lines[iIndex];
            return new DrawColor();
        }

        public DrawColor GetBlockColor(int iIndex)
        {
            if (iIndex < Properties.Settings.Default.DrawColors.Blocks.Count)
                return Properties.Settings.Default.DrawColors.Blocks[iIndex];
            return new DrawColor();
        }

        public void SelectNextIcon(int iCount)
        {
            if (Mode == BlockMode.Play)
                return;     // Don't select icons in play mode unless directly selected from the toolbar
            if (m_iconsForm != null)
            {
                m_iconsForm.SelectNextIcon(iCount);
                SetIconMode();
            }
        }

        public void CycleItemFormat()
        {
            try
            {
                for (int i = 0; i < Global.ItemFormats.Length; i++)
                {
                    if (Properties.Settings.Default.ItemFormat == Global.ItemFormats[i])
                    {
                        i++;
                        if (i >= Global.ItemFormats.Length)
                        {
                            if (String.IsNullOrWhiteSpace(m_strItemFormatHold))
                                i = 0;
                            else
                            {
                                Properties.Settings.Default.ItemFormat = m_strItemFormatHold;
                                return;
                            }
                        }
                        Properties.Settings.Default.ItemFormat = Global.ItemFormats[i];
                        return;
                    }
                }
                m_strItemFormatHold = Properties.Settings.Default.ItemFormat;
                Properties.Settings.Default.ItemFormat = Global.ItemFormats[0];
            }
            finally
            {
                if (Global.FormVisible(m_formParty))
                    m_formParty.UpdateUI();
            }
        }

        public void SelectNextBlockIndex(int iCount)
        {
            DrawColors dc = Properties.Settings.Default.DrawColors;
            int iCurrent = -1;
            for (int i = 0; i < dc.Blocks.Count; i++)
            {
                if (m_dcCurrentBlock.color == dc.Blocks[i].color && m_dcCurrentBlock.styleBlock == dc.Blocks[i].styleBlock)
                {
                    iCurrent = i;
                    break;
                }
            }
            iCurrent += iCount;
            if (iCurrent < 0)
                iCurrent = dc.Blocks.Count + iCurrent;
            else if (iCurrent >= dc.Blocks.Count)
                iCurrent -= dc.Blocks.Count;
            SelectBlockColor(dc.Blocks[iCurrent]);
        }

        public void ForceRefreshAll()
        {
            // Clean up some things that may have gotten out of hand
            Global.IconCache.Clear();
            m_lastMonsterLocations = null;
            m_lastActiveSquares = null;
            m_bForceActiveSquareUpdate = true;
            if (pbMain.Capture)
                ForceMouseUp(m_btnCaptured);
            if (CurrentSheet != null)
            {
                foreach (MapSquare square in CurrentSheet.Grid)
                    if (square.Note != null)
                        square.Note.Moving = false;
            }
            CurrentSheet.SetCurrentNote(null, Point.Empty);
            CurrentSheet.SetCurrentIcon(null, Point.Empty);
            UpdateLiveMap();
            ForceRefreshOnDisplay();
        }

        public void SelectNextLineIndex(int iCount)
        {
            DrawColors dc = Properties.Settings.Default.DrawColors;
            int iCurrent = -1;
            for (int i = 0; i < dc.Lines.Count; i++)
            {
                if (m_dcCurrentLine.color == dc.Lines[i].color && m_dcCurrentLine.style == dc.Lines[i].style && m_dcCurrentLine.width == dc.Lines[i].width)
                {
                    iCurrent = i;
                    break;
                }
            }
            iCurrent += iCount;
            if (iCurrent < 0)
                iCurrent = dc.Lines.Count + iCurrent;
            else if (iCurrent >= dc.Lines.Count)
                iCurrent -= dc.Lines.Count;
            SelectLineColor(dc.Lines[iCurrent]);
        }

        private void SelectBlock(int iIndex)
        {
            SelectBlockColor(GetBlockColor(iIndex));
        }

        private void SelectLine(int iIndex)
        {
            SelectLineColor(GetLineColor(iIndex));
        }

        private void RefreshEncounterWindow()
        {
            if (Global.FormVisible(m_formEncounters))
                m_formEncounters.UpdateUI();
        }

        private void CycleLineStyle(bool bNext)
        {
            switch (m_dcCurrentLine.style)
            {
                case DashStyle.Solid:
                    SelectLineColor(new DrawColor(m_dcCurrentLine.color, bNext ? DashStyle.Dot : DashStyle.Dash, m_dcCurrentLine.width));
                    break;
                case DashStyle.Dot:
                    SelectLineColor(new DrawColor(m_dcCurrentLine.color, bNext ? DashStyle.Dash : DashStyle.Solid, m_dcCurrentLine.width));
                    break;
                default:
                    SelectLineColor(new DrawColor(m_dcCurrentLine.color, bNext ? DashStyle.Solid : DashStyle.Dot, m_dcCurrentLine.width));
                    break;
            }
        }

        private void CycleBlockStyle(bool bNext)
        {
            switch (m_dcCurrentBlock.styleBlock)
            {
                case HatchStyle.Percent90:
                    SelectBlockColor(new DrawColor(m_dcCurrentBlock.color, bNext ? HatchStyle.Percent25 : HatchStyle.Percent75));
                    break;
                case HatchStyle.Percent25:
                    SelectBlockColor(new DrawColor(m_dcCurrentBlock.color, bNext ? HatchStyle.Percent50 : HatchStyle.Percent90));
                    break;
                case HatchStyle.Percent50:
                    SelectBlockColor(new DrawColor(m_dcCurrentBlock.color, bNext ? HatchStyle.Percent75 : HatchStyle.Percent25));
                    break;
                default:
                    SelectBlockColor(new DrawColor(m_dcCurrentBlock.color, bNext ? HatchStyle.Percent90 : HatchStyle.Percent50));
                    break;
            }
        }

        private void RefreshQuestWindow()
        {
            if (Global.FormVisible(m_formQuests))
                m_formQuests.ForceRefresh();
        }

        private void CheckShowScrollbars()
        {
            bool bShow = false;

            if (Mode != BlockMode.Play)
                bShow = true;
            else
                bShow = !Properties.Settings.Default.HideScrollbarsInPlayMode;

            if (!bShow)
            {
                // Toggling the AutoScroll forces an update that keeps the picturebox in the correct location
                Point ptSave = new Point(-panelMain.AutoScrollPosition.X, -panelMain.AutoScrollPosition.Y);
                panelMain.AutoScroll = true;
                panelMain.AutoScroll = false;
                panelMain.AutoScrollPosition = ptSave;
            }

            if (panelMain.HorizontalScroll.Visible == bShow)
                return;

            ShowScrollbars(bShow);
        }

        private void ShowScrollbars(bool bShow)
        {
            UpdateScrollSizes(panelMain);
            Point ptSave = new Point(-panelMain.AutoScrollPosition.X, -panelMain.AutoScrollPosition.Y);
            panelMain.AutoScroll = bShow;
            panelMain.HorizontalScroll.Visible = bShow;
            panelMain.VerticalScroll.Visible = bShow;
            panelMain.AutoScrollPosition = ptSave;
        }

        private void SetMenuShortcut(ToolStripMenuItem mi, Keys keys)
        {
            if (!Global.IsValidShortcut(keys))
            {
                mi.ShortcutKeys = Keys.None;
                mi.ShowShortcutKeys = false;
            }
            else
            {
                switch (keys)
                {
                    case Keys.End:
                        // Some keys that Windows doesn't like as menu shortcuts, to avoid throwing exceptions
                        return;
                    default:
                        break;
                }
                try
                {
                    mi.ShortcutKeys = keys;
                    mi.ShowShortcutKeys = true;
                }
                catch (Exception)
                {
                    // Windows doesn't like this keyboard shortcut, so skip it
                }
            }
        }

        private void SetMenuShortcuts()
        {
            foreach (ToolStripMenuItem mi in m_menuItems)
                mi.ShortcutKeys = Keys.None;

            foreach (KeyValuePair<ShortcutKeys, InputOption> pair in m_shortcuts.ShortcutDict)
            {
                if (pair.Value.Input == null || pair.Value.Input.Length < 1)
                    continue;

                if ((int)pair.Value.Action >= m_menuItems.Length)
                    continue;

                SetMenuShortcut(m_menuItems[(int)pair.Value.Action], Global.MenuKeys(pair.Value.Input[0].Keys));
            }
        }

        public static int CheckFavoriteSpells(int iSpellIndex, GameNames game, string strCharName)
        {
            if (iSpellIndex >= -1)
                return iSpellIndex;
            List<int> favorites = Properties.Settings.Default.FavoriteSpells.GetFavorites(game, strCharName);
            iSpellIndex += 999;
            if (iSpellIndex < 0 || iSpellIndex >= favorites.Count)
                return -1;
            return favorites[iSpellIndex];
        }

        private bool ProcessSpellHotkey(Action action)
        {
            if (!m_hacker.Running)
                return false;

            int iKey = action - Action.SpellHotkey1;

            SpellHotkeyList hotkeys = Games.GetSpellHotkeys(Hacker.Game);
            if (hotkeys != null)
            {
                SpellHotkey hk = hotkeys.GetHotkey(iKey);
                switch (hk.Character)
                {
                    case SpellHotkey.HKCharacter.CurrentCharacter:
                        int iSpellIndex = hk.SpellIndex;
                        BaseCharacter baseChar = GetCharacterByAddress(Hacker.GetActingCharacterAddress());
                        if (baseChar == null)
                            return false;
                        iSpellIndex = CheckFavoriteSpells(iSpellIndex, Hacker.Game, baseChar.Name);
                        Spell spell = Games.GetSpellByBasicIndex(Hacker.Game, iSpellIndex);
                        if (!baseChar.KnowsSpell(spell))
                            return false;
                        if (spell != null)
                        {
                            Keys[] keys = spell.GetKeys(baseChar);
                            if (keys == null || keys.Length == 0)
                                return false;
                            return m_hacker.SendKeysToDOSBox(Hacker.DelayBetweenSpellKeys, keys, true);
                        }
                        break;
                    default:
                        return m_hacker.SetReadySpell(hotkeys.GetHotkey(iKey));
                }
            }
            return false;
        }

        private bool ProcessCureAll(Action action)
        {
            if (!Properties.Settings.Default.EnableMemoryWrite)
                return false;

            switch (action)
            {
                case Action.CureAllSilent:
                    if (m_formParty == null)
                        return false;   // No party form => no selected character
                    return m_formParty.CureAll(true);
                case Action.CureAll1: return CureAll(0, true);
                case Action.CureAll2: return CureAll(1, true);
                case Action.CureAll3: return CureAll(2, true);
                case Action.CureAll4: return CureAll(3, true);
                case Action.CureAll5: return CureAll(4, true);
                case Action.CureAll6: return CureAll(5, true);
                case Action.CureAll7: return CureAll(6, true);
                case Action.CureAll8: return CureAll(7, true);
                default: return false;
            }
        }

        private void ProcessMoveCursor(Action action)
        {
            if (Mode != BlockMode.Keyboard)
                return;

            m_bCenterKeyboard = true;

            switch (action)
            {
                case Action.MoveCursorNW:
                    MoveCursor(-1, -1);
                    break;
                case Action.MoveCursorN:
                    MoveCursor(0, -1);
                    break;
                case Action.MoveCursorNE:
                    MoveCursor(1, -1);
                    break;
                case Action.MoveCursorW:
                    MoveCursor(-1, 0);
                    break;
                case Action.MoveCursorE:
                    MoveCursor(1, 0);
                    break;
                case Action.MoveCursorSW:
                    MoveCursor(-1, 1);
                    break;
                case Action.MoveCursorS:
                    MoveCursor(0, 1);
                    break;
                case Action.MoveCursorSE:
                    MoveCursor(1, 1);
                    break;
                default:
                    break;
            }
        }

        private void ProcessExpandSheet(Action action)
        {
            ExpandSizes sizes = new ExpandSizes();
            switch (action)
            {
                case Action.ExpandSheetSW:
                    sizes = new ExpandSizes(0, 1, 1, 0);
                    break;
                case Action.ExpandSheetS:
                    sizes = new ExpandSizes(0, 1, 0, 0);
                    break;
                case Action.ExpandSheetSE:
                    sizes = new ExpandSizes(0, 1, 0, 1);
                    break;
                case Action.ExpandSheetW:
                    sizes = new ExpandSizes(0, 0, 1, 0);
                    break;
                case Action.ExpandSheetAll:
                    sizes = new ExpandSizes(1, 1, 1, 1);
                    break;
                case Action.ExpandSheetE:
                    sizes = new ExpandSizes(0, 0, 0, 1);
                    break;
                case Action.ExpandSheetNW:
                    sizes = new ExpandSizes(1, 0, 1, 0);
                    break;
                case Action.ExpandSheetN:
                    sizes = new ExpandSizes(1, 0, 0, 0);
                    break;
                case Action.ExpandSheetNE:
                    sizes = new ExpandSizes(1, 0, 0, 1);
                    break;
                default:
                    break;
            }
            if (!sizes.IsEmpty)
            {
                CurrentSheet.Expand(sizes);
                SetDirtyUnsaved();
            }
        }

        private void ProcessMoveBitmap(Action action, int iOffset = 1)
        {
            if (CurrentSheet == null || !CurrentSheet.UseUnvisitedBitmap)
                return;

            switch (action)
            {
                case Action.MoveBitmapNW:
                case Action.MoveBitmap10NW:
                    CurrentSheet.UnvisitedCrop.Offset(-iOffset, -iOffset);
                    break;
                case Action.MoveBitmapN:
                case Action.MoveBitmap10N:
                    CurrentSheet.UnvisitedCrop.Offset(0, -iOffset);
                    break;
                case Action.MoveBitmapNE:
                case Action.MoveBitmap10NE:
                    CurrentSheet.UnvisitedCrop.Offset(iOffset, -iOffset);
                    break;
                case Action.MoveBitmapW:
                case Action.MoveBitmap10W:
                    CurrentSheet.UnvisitedCrop.Offset(-iOffset, 0);
                    break;
                case Action.MoveBitmapE:
                case Action.MoveBitmap10E:
                    CurrentSheet.UnvisitedCrop.Offset(iOffset, 0);
                    break;
                case Action.MoveBitmapSW:
                case Action.MoveBitmap10SW:
                    CurrentSheet.UnvisitedCrop.Offset(-iOffset, iOffset);
                    break;
                case Action.MoveBitmapS:
                case Action.MoveBitmap10S:
                    CurrentSheet.UnvisitedCrop.Offset(0, iOffset);
                    break;
                case Action.MoveBitmapSE:
                case Action.MoveBitmap10SE:
                    CurrentSheet.UnvisitedCrop.Offset(iOffset, iOffset);
                    break;
                default:
                    break;
            }
            SetDirtyUnsaved();
            ForceRefreshOnDisplay();
        }

        private void ProcessLineToggle(Action action)
        {
            CurrentSheet.ClearRedo();
            CurrentSheet.AddUndoCursorBlock(); 
            switch (action)
            {
                case Action.ToggleLineNW:
                    CurrentSheet.ToggleLinesCurrent(Directions.UpLeft, m_dcCurrentLine);
                    break;
                case Action.ToggleLineN:
                    CurrentSheet.ToggleLineCurrent(Direction.Up, m_dcCurrentLine);
                    break;
                case Action.ToggleLineNE:
                    CurrentSheet.ToggleLinesCurrent(Directions.UpRight, m_dcCurrentLine);
                    break;
                case Action.ToggleLineW:
                    CurrentSheet.ToggleLineCurrent(Direction.Left, m_dcCurrentLine);
                    break;
                case Action.ToggleLineE:
                    CurrentSheet.ToggleLineCurrent(Direction.Right, m_dcCurrentLine);
                    break;
                case Action.ToggleLineSW:
                    CurrentSheet.ToggleLinesCurrent(Directions.DownLeft, m_dcCurrentLine);
                    break;
                case Action.ToggleLineS:
                    CurrentSheet.ToggleLineCurrent(Direction.Down, m_dcCurrentLine);
                    break;
                case Action.ToggleLineSE:
                    CurrentSheet.ToggleLinesCurrent(Directions.DownRight, m_dcCurrentLine);
                    break;
                case Action.ToggleLineAll:
                    CurrentSheet.ToggleLinesCurrent(Directions.All, m_dcCurrentLine);
                    break;
                case Action.ToggleDoubleLineNW:
                    CurrentSheet.ToggleDoubleLinesCurrent(Directions.UpLeft, m_dcCurrentLine);
                    break;
                case Action.ToggleDoubleLineN:
                    CurrentSheet.ToggleDoubleLineCurrent(Direction.Up, m_dcCurrentLine);
                    break;
                case Action.ToggleDoubleLineNE:
                    CurrentSheet.ToggleDoubleLinesCurrent(Directions.UpRight, m_dcCurrentLine);
                    break;
                case Action.ToggleDoubleLineW:
                    CurrentSheet.ToggleDoubleLineCurrent(Direction.Left, m_dcCurrentLine);
                    break;
                case Action.ToggleDoubleLineE:
                    CurrentSheet.ToggleDoubleLineCurrent(Direction.Right, m_dcCurrentLine);
                    break;
                case Action.ToggleDoubleLineSW:
                    CurrentSheet.ToggleDoubleLinesCurrent(Directions.DownLeft, m_dcCurrentLine);
                    break;
                case Action.ToggleDoubleLineS:
                    CurrentSheet.ToggleDoubleLineCurrent(Direction.Down, m_dcCurrentLine);
                    break;
                case Action.ToggleDoubleLineSE:
                    CurrentSheet.ToggleDoubleLinesCurrent(Directions.DownRight, m_dcCurrentLine);
                    break;
                case Action.ToggleDoubleLineAll:
                    CurrentSheet.ToggleDoubleLinesCurrent(Directions.All, m_dcCurrentLine);
                    break;
                default:
                    break;
            }
            SetDirtyUnsaved();
        }

        void m_timerCartography_Tick(object sender, EventArgs e)
        {
            m_timerRedrawMap.Stop();
            ForceRefreshOnDisplay();
        }

        private bool CartographyTimer()
        {
            if (m_hacker == null)
                return false;
            if (!m_hacker.IsValid)
                return false;

            try
            {
                m_bSuspendMapUpdates = true;
                if (ShowingCurrentMap && ((m_liveMap != null && m_liveMap.Count > 0) || CurrentSheet.Live))
                {
                    if (m_hacker.UpdateLiveSquares(m_mapBook, CurrentSheet, m_liveMap))
                    {
                        if (!CurrentSheet.Live)
                        {
                            foreach (Point pt in m_liveMap)
                                CurrentSheet.SetDirty(pt);
                        }
                        SetDirty();
                        CurrentSheet.RefreshYouAreHere(this, IgnoreInaccessible);
                    }
                }

                if (Properties.Settings.Default.UseInGameCartography && m_iMapLoadGraceCounter == 0)
                {
                    MapCartography cart = m_hacker.GetCartography();
                    if (cart == null)
                        return false;

                    if (m_lastCartography == null || cart.MapIndex != m_lastCartography.MapIndex || !Global.Compare(cart.GetBytes(), m_lastCartography.GetBytes()))
                    {
                        if (m_iCartographyGraceCounter == 0)
                        {
                            m_iCartographyGraceCounter = 1;
                            return false;
                        }

                        long time = Hacker.GetGameTimeLong();
                        if (m_lastGameTime > time)  // Game was probably reloaded
                            m_bNeedCartUpdate = true;

                        m_lastGameTime = time;

                        m_lastCartography = cart;
                        if (m_mapBook.UpdateVisited(CurrentSheet, m_lastCartography, Hacker.CartographyCanUnvisitSquares))
                            SetDirtyUnsaved();
                        if (m_bNeedCartUpdate)
                            ForceRefreshOnDisplay();
                        else if (Hacker.PointInMap(Hacker.GetPartyPosition()))  // If the current location is off the map, don't bother
                            SetDirty();

                        m_iCartographyGraceCounter = 0;
                        return true;
                    }
                }

                return false;
            }
            finally
            {
                m_bSuspendMapUpdates = false;
            }
        }

        private bool CheckActiveSquares()
        {
            ActiveSquares squares = m_hacker.GetActiveSquares(this, m_lastActiveSquares == null);
            if (squares == null)
                return false;

            if (squares.BytesEqual(m_lastActiveSquares) && m_lastActiveSquares != null && m_lastActiveSquares.Drawn)
            {
                m_lastActiveSquares.Changed = false;
                return false;
            }

            m_lastActiveSquares = squares;
            m_lastActiveSquares.Changed = true;

            return true;
        }

        private bool MonsterItemTimer()
        {
            if (m_hacker == null)
                return false;
            if (!m_hacker.IsValid)
                return false;

            CheckActiveSquares();

            MonsterLocations monsters = m_hacker.GetMonsterLocations();
            ItemLocations items = m_hacker.GetItemLocations(CurrentSheet == null ? -1 : CurrentSheet.GameMapIndex);

            bool bMonsters = MonsterTimer(monsters);
            bool bItems = ItemTimer(items);
            return (bMonsters || bItems);
        }

        private bool ItemTimer(ItemLocations items)
        {
            bool bForceUpdate = false;
            if (items == null)
                return false;

            if (items == null || items.RawBytes == null)
            {
                m_lastItemLocations = null;
                if (m_lastItemLocations != null)
                {
                    SetDirty(); // Erase the old item positions
                    return true;
                }
                return false;
            }

            if (bForceUpdate ||
                m_lastItemLocations == null ||
                m_lastItemLocations.RawBytes == null ||
                !Global.Compare(items.RawBytes, m_lastMonsterLocations.RawBytes)
                )
            {
                m_lastItemLocations = items;
                m_lastItemLocations.Drawn = false;
                SetDirty();
                if (ShowingCurrentMap)
                {
                    ItemLocations itemsTranslated = m_mapBook.TranslateLocationToMap(m_lastItemLocations, CurrentSheet);
                    UpdateItemLocations(CurrentSheet, itemsTranslated);
                }
                return true;
            }
            m_lastItemLocations = items;
            return false;
        }

        private bool MonsterTimer(MonsterLocations monsters)
        {
            bool bForceUpdate = false;
            if (monsters == null)
                return false;

            if (!monsters.AlwaysShow && Global.ShowOnlyDetectableMonsters && CurrentSheet != null && CurrentSheet.YouAreHere != null)
            {
                Point ptParty = TranslateToGameMap(CurrentSheet.YouAreHere.PrimaryCoordinates, null);
                if (m_ptLastMonsterDetectionCenter != ptParty)
                {
                    foreach (Point pt in monsters.MonsterPositions.Keys)
                    {
                        if (Proximity.SimpleDistance(ptParty, pt) < 4 ||
                            Proximity.SimpleDistance(m_ptLastMonsterDetectionCenter, pt) < 4)
                            SetDirty(pt);
                    }
                    m_ptLastMonsterDetectionCenter = ptParty;
                    bForceUpdate = true;
                }
            }

            if (Global.FormVisible(m_formEncounters))
            {
                if (monsters == m_lastMonsterLocations)
                {
                    byte[] bytesSelected = m_formEncounters.GetSelectedBytes();
                    if (Global.Compare(bytesSelected, monsters.HighlightedBytes) && !bForceUpdate)
                        return false;
                    else
                    {
                        m_formEncounters.SetSelectedMonsters(monsters);
                        bForceUpdate = true;
                    }
                }
                else
                {
                    m_formEncounters.SetSelectedMonsters(monsters);
                }
            }
            else
                monsters.HighlightedBytes = new byte[0];

            if (monsters == null || monsters.RawBytes == null)
            {
                m_lastMonsterLocations = null;
                if (m_lastMonsterLocations != null)
                {
                    SetDirty(); // Erase the old monster positions
                    return true;
                }
                return false;
            }

            if (bForceUpdate ||
                m_lastMonsterLocations == null || 
                m_lastMonsterLocations.RawBytes == null || 
                !Global.Compare(monsters.RawBytes, m_lastMonsterLocations.RawBytes) ||
                !Global.Compare(monsters.HighlightedBytes, m_lastMonsterLocations.HighlightedBytes)
                )
            {
                m_lastMonsterLocations = monsters;
                m_lastMonsterLocations.Drawn = false;
                SetDirty();
                if (ShowingCurrentMap)
                {
                    MonsterLocations monstersTranslated = m_mapBook.TranslateLocationToMap(m_lastMonsterLocations, CurrentSheet);
                    //Global.Log("UpdateMonsterLocations()");
                    UpdateMonsterLocations(CurrentSheet, monstersTranslated);
                }
                return true;
            }
            m_lastMonsterLocations = monsters;
            return false;
        }

        public void InvalidateMonsters()
        {
            m_dtMonsterUpdate = DateTime.MinValue;
        }

        private void timerDirty_Tick(object sender, EventArgs e)
        {
            if (m_bInDirtyTimer || m_bSuspendMapUpdates)
                return;

            if ((pbMain.Location.X < 0 || pbMain.Location.Y < 0 || pbMain.Right > panelMain.ClientRectangle.Right || pbMain.Bottom > panelMain.ClientRectangle.Bottom) &&
                panelMain.ClientRectangle.Contains(pbMain.ClientRectangle))
            {
                // Somehow the picturebox holding the map image, although it can fit in the panel, is partially outside of the panel rectangle
                panelMain.AutoScrollPosition = new Point(0, 0);
                Global.CenterControl(pbMain);
            }

            if (m_bCheckBook)
            {
                m_bCheckBook = false;
                if (CheckCustomBackgrounds())
                    return;
            }

            if (Timer_BringDOSBoxToFront())
                return;

            if (m_lastMouseMoveArgs != null)
            {
                OnTimedMouseMove(m_lastMouseMoveArgs);
                m_lastMouseMoveArgs = null;
            }

            if (!m_bFocusingWindows && m_hacker != null && m_hacker.DOSBoxWindow != IntPtr.Zero)
            {
                if (NativeMethods.GetForegroundWindow() == m_hacker.DOSBoxWindow)
                    m_bDOSBoxAboveWAW = !NativeMethods.IsAltDown();  // Prepares for Alt+Tab
            }

            if (m_formsNewOrder != null)
            {
                foreach (IntPtr hWnd in m_formsNewOrder)
                    NativeMethods.ResumeDrawing(hWnd);
                m_formsNewOrder = null;
            }

            if (m_bMapMenuDirty)
                GenerateMapMenu();

            CheckModifierKeys();

            if (Global.Debug)
                SetCaption();

            if (m_hacker != null && m_hacker.GameFound != m_hacker.Game && m_hacker.GameFound != GameNames.None)
            {
                CreateHacker(m_hacker.GameFound);
                return;
            }

            if (m_dtMonsterUpdate == DateTime.MinValue || (DateTime.Now - m_dtMonsterUpdate).TotalMilliseconds > Properties.Settings.Default.MonsterPositionTimer)
            {
                CartographyTimer();
                m_dtMonsterUpdate = DateTime.Now;
                m_bMonstersChanged = MonsterItemTimer();
                if (m_bMonstersChanged)
                    return;
            }

            m_bInDirtyTimer = true;

            try
            {
                bool bDOSBoxMinimized = m_hacker != null && NativeMethods.IsMinimized(m_hacker.DOSBoxWindow);
                if (m_hacker != null && Properties.Settings.Default.LinkedMinimizeAndRestore)
                {
                    if (m_iIgnoreDOSBoxWindowState-- <= 0)
                    {
                        m_iIgnoreDOSBoxWindowState = 0;
                        if (bDOSBoxMinimized && WindowState != FormWindowState.Minimized)
                            WindowState = FormWindowState.Minimized;
                        else if (!bDOSBoxMinimized && WindowState == FormWindowState.Minimized)
                            NativeMethods.RestoreWindow(Handle);
                    }
                }

                if (m_hacker != null && m_bDosBoxPosSet && (Properties.Settings.Default.ForceDosBoxLocation || Properties.Settings.Default.ForceDosBoxSize) &&
                    !bDOSBoxMinimized && NativeMethods.IsWindowVisible(m_hacker.DOSBoxWindow) && (DateTime.Now - m_dtLastDOSBoxCheck).TotalMilliseconds > 500)
                {
                    m_dtLastDOSBoxCheck = DateTime.Now;
                    Rectangle rc = m_hacker.GetDOSBoxRect();
                    Rectangle rcFix = Properties.Settings.Default.DosBoxWindowRect;
                    if (rc != rcFix && Properties.Settings.Default.ForceDosBoxLocation && Properties.Settings.Default.ForceDosBoxSize)
                        NativeMethods.SetWindowPosAsync(m_hacker.DOSBoxWindow, IntPtr.Zero, rcFix.X, rcFix.Y, rcFix.Width, rcFix.Height, NativeMethods.SetWindowPosFlags.NOZORDER | NativeMethods.SetWindowPosFlags.NOACTIVATE);
                    else if (rc.Location != rcFix.Location && Properties.Settings.Default.ForceDosBoxLocation)
                        NativeMethods.SetWindowPosAsync(m_hacker.DOSBoxWindow, IntPtr.Zero, rcFix.X, rcFix.Y, 0, 0, NativeMethods.SetWindowPosFlags.NOZORDER | NativeMethods.SetWindowPosFlags.NOSIZE | NativeMethods.SetWindowPosFlags.NOACTIVATE);
                    else if (rc.Location != rcFix.Location && Properties.Settings.Default.ForceDosBoxLocation)
                        NativeMethods.SetWindowPosAsync(m_hacker.DOSBoxWindow, IntPtr.Zero, 0, 0, rcFix.Width, rcFix.Height, NativeMethods.SetWindowPosFlags.NOZORDER | NativeMethods.SetWindowPosFlags.NOREPOSITION | NativeMethods.SetWindowPosFlags.NOACTIVATE);
                    NativeMethods.JoinSWPThread();
                    Rectangle rcTest = m_hacker.GetDOSBoxRect();
                    if (rcTest.Size != rcFix.Size && Properties.Settings.Default.ForceDosBoxSize)
                    {
                        if (m_iForceDBSizeGraceCounter++ > 50)
                        {
                            if (MessageBox.Show(String.Format("The DOSBox window could not be forced to size {0}x{1}\r\n\r\n" +
                                "(Current size: {2}x{3}).  Would you like to disable this option?",
                                rcFix.Width, rcFix.Height, rcTest.Width, rcTest.Height), "Error forcing DOSBox size",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                                Properties.Settings.Default.ForceDosBoxSize = false;
                        }
                    }
                    else
                        m_iForceDBSizeGraceCounter = 0;
                }

                if (Properties.Settings.Default.AutoSave)
                {
                    // Don't go nuts if the setting somehow ended up as zero or similar
                    if (Properties.Settings.Default.AutosaveSeconds < 10)
                        Properties.Settings.Default.AutosaveSeconds = 10;

                    if ((DateTime.Now - m_dtLastAutosave).TotalSeconds > Properties.Settings.Default.AutosaveSeconds)
                    {
                        try
                        {
                            UpdateBookExtraData();
                            string strFile = Environment.ExpandEnvironmentVariables(Properties.Settings.Default.AutoSaveFile);
                            m_serializer.Save(m_mapBook, strFile);
                            Global.LogWarning(String.Format("Autosaved to {0} on {1} at {2}", strFile, DateTime.Now.ToShortDateString(), DateTime.Now.ToShortTimeString()));
                            m_dtLastAutosave = DateTime.Now;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(String.Format("Could not autosave to {0}.  Autosave has been disabled.\r\n\r\nException: {1}", Properties.Settings.Default.AutoSaveFile, ex.Message),
                                "Error autosaving", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Properties.Settings.Default.AutoSave = false;
                        }
                    }
                }

                if (m_bLaunchFinished)
                {
                    m_formWait.Close();
                    if (m_iPostLaunchGraceCounter == 0)
                        m_iPostLaunchGraceCounter = 12;  // 12 loops through the dirty timer is a half-second or so
                    if (!CheckGameMemory())
                    {
                        bool bIgnoreLaunchCounter = false;
                        if (
                            NativeMethods.GetWindowText(m_hacker.DOSBoxWindow).EndsWith("IMGMOUNT") ||
                            NativeMethods.GetWindowText(m_hacker.DOSBoxWindow).EndsWith("DOSBOX")
                            )
                            bIgnoreLaunchCounter = true;
                        if (Game == GameNames.EyeOfTheBeholder1 && m_hacker.FindAnyBlock(13856, EOB1MemoryHacker.BatchBytes))
                        {
                            bIgnoreLaunchCounter = true;
                            if (Properties.Settings.Default.SkipIntroductions)
                                SkipIntroductions(false);
                        }

                        if (!bIgnoreLaunchCounter)
                            m_iPostLaunchGraceCounter--;

                        if (m_iPostLaunchGraceCounter <= 0)
                        {
                            m_showNext = new ShowNext(Global.MemoryScanFail, "Game Launch Failed");
                            m_bLaunchFinished = false;
                        }
                    }
                    else
                    {
                        m_iPostLaunchGraceCounter = 0;
                        m_threadLaunch = null;
                        if (m_hacker != null && Properties.Settings.Default.DOSBoxPosition != Global.NullPoint && Properties.Settings.Default.SaveDOSBoxPosition)
                            m_hacker.SetDOSBoxPosition(Properties.Settings.Default.DOSBoxPosition);
                        m_bDosBoxPosSet = true;
                        m_bNeedCartUpdate = true;
                        SetLaunchFinished(false);
                        if (Global.FormVisible(m_formSpells))
                            m_formSpells.SetSpellInfo(null);
                        if (m_hacker != null)
                            m_hacker.Start();
                        CheckAutoShowWindows();
                        m_bInDirtyTimer = false;
                        if (Properties.Settings.Default.SkipIntroductions)
                            SkipIntroductions();
                    }
                    return;
                }

                if (m_showNext != null)
                {
                    // Show dialogs here in the timer of the main thread, instead of launching them from the keyboard hook.
                    switch (m_showNext.Type)
                    {
                        case ShowNextType.CreationAssistant:
                            m_showNext.Type = ShowNextType.None;
                            if (Global.FormVisible(m_formCreationAssistant))
                                m_formCreationAssistant.Close();
                            else
                                ShowCreationAssistant();
                            break;
                        case ShowNextType.QuickRef:
                            m_showNext.Type = ShowNextType.None;
                            if (Global.FormVisible(m_formQuickRef))
                                m_formQuickRef.Close();
                            else
                                ShowQuickRef();
                            break;
                        case ShowNextType.TrainingAssistant:
                            m_showNext.Type = ShowNextType.None;
                            if (Global.FormVisible(m_formTrainingAssistant))
                                m_formTrainingAssistant.Close();
                            else
                                ShowTrainingAssistant();
                            break;
                        case ShowNextType.PartyWindow:
                            m_showNext.Type = ShowNextType.None;
                            if (Global.FormVisible(m_formParty))
                                m_formParty.Close();
                            else
                            {
                                CheckGameMemory();
                                ShowPartyWindow();
                            }
                            break;
                        case ShowNextType.SpellWindow:
                            m_showNext.Type = ShowNextType.None;
                            if (Global.FormVisible(m_formSpells))
                                m_formSpells.Hide();
                            else
                                ShowSpellWindow(null);
                            break;
                        case ShowNextType.EncounterWindow:
                            m_showNext.Type = ShowNextType.None;
                            if (Global.FormVisible(m_formEncounters))
                                m_formEncounters.Close();
                            else
                                CheckEncounters();
                            break;
                        case ShowNextType.ShopInventoryWindow:
                            m_showNext.Type = ShowNextType.None;
                            if (Global.FormVisible(m_formShopInventory))
                                m_formShopInventory.Close();
                            else
                                CheckShops();
                            break;
                        case ShowNextType.GameInfoWindow:
                            m_showNext.Type = ShowNextType.None;
                            if (Global.FormVisible(m_formInfo))
                                m_formInfo.Close();
                            else
                                ShowGameInfo();
                            break;
                        case ShowNextType.QuestWindow:
                            m_showNext.Type = ShowNextType.None;
                            if (Global.FormVisible(m_formQuests))
                                m_formQuests.Close();
                            else
                                ShowQuestWindow();
                            break;
                        case ShowNextType.ScriptsWindow:
                            m_showNext.Type = ShowNextType.None;
                            if (Global.FormVisible(m_formScripts) && m_showNext.Position == Global.NullPoint)
                                m_formScripts.Close();
                            else
                                ShowScriptsWindow(m_showNext.Position);
                            break;
                        case ShowNextType.Message:
                            if (Visible && CanFocus)
                            {
                                // Only show the dialog if there is not a modal window open already
                                m_showNext.Type = ShowNextType.None;
                                MessageBox.Show(m_showNext.Message, m_showNext.Caption, MessageBoxButtons.OK, m_showNext.Icon);
                            }
                            break;
                        default:
                            break;
                    }
                    m_showNext = null;
                    return;
                }

                if (m_hacker != null && m_hacker.Running)
                {
                    UpdateLocationFromGame();
                    if (Properties.Settings.Default.ShowEncounters && !Global.FormVisible(m_formEncounters))
                        CheckEncounters();
                    if (Properties.Settings.Default.ShowShopInventories && !Global.FormVisible(m_formShopInventory))
                        CheckShops();
                    CheckSpells();
                }
                else
                {
                    if (CurrentSheet != null)
                        CurrentSheet.CurrentMap = false;    // Never the current map if no game is being played
                }

                // Change the mouse cursors only if the main window can accept the mouse click that the cursor represents
                if (Visible && CanFocus && pbMain.ClientRectangle.Contains(pbMain.PointToClient(Cursor.Position)) && NativeMethods.WindowAtPoint(Cursor.Position) == Handle)
                {
                    bool bConstrainMove = NativeMethods.IsShiftDown();
                    if (m_bConstrainMove != bConstrainMove)
                    {
                        m_bConstrainMove = bConstrainMove;
                        OnTimedMouseMove(m_lastMouseMoveArgs);
                    }
                    switch (VirtualMode)
                    {
                        case BlockMode.Live:
                            if (pbMain.Cursor != Global.GetCursor(MouseCursor.Live))
                                pbMain.Cursor = Global.GetCursor(MouseCursor.Live);
                            break;
                        case BlockMode.Notes:
                            if (NativeMethods.IsControlDown())
                            {
                                if (pbMain.Cursor != Global.GetCursor(MouseCursor.NoteCopy))
                                    pbMain.Cursor = Global.GetCursor(MouseCursor.NoteCopy);
                                if (m_selectionAction == SelectionActions.Move)
                                {
                                    m_selectionAction = SelectionActions.Copy;
                                    CheckLabelCopyFeedback(pbMain.PointToClient(Cursor.Position));
                                }
                            }
                            else
                            {
                                if (pbMain.Cursor != Global.GetCursor(MouseCursor.Note))
                                    pbMain.Cursor = Global.GetCursor(MouseCursor.Note);
                                if (m_selectionAction == SelectionActions.Copy)
                                {
                                    m_selectionAction = SelectionActions.Move;
                                    CheckLabelCopyFeedback(pbMain.PointToClient(Cursor.Position));
                                }
                            }
                            break;
                        case BlockMode.Edit:
                            if (m_selectionAction == SelectionActions.Move && NativeMethods.IsControlDown())
                            {
                                pbMain.Cursor = Global.GetCursor(MouseCursor.Copy);
                                m_selectionAction = SelectionActions.Copy;
                                m_ptLastSquare = Global.NullPoint;
                                m_bDirty = true;
                            }
                            else if (m_selectionAction == SelectionActions.Copy && !NativeMethods.IsControlDown())
                            {
                                pbMain.Cursor = Cursors.Default;
                                m_selectionAction = SelectionActions.Move;
                                m_ptLastSquare = Global.NullPoint;
                                m_bDirty = true;
                            }
                            break;
                        case BlockMode.Fill:
                            if (pbMain.Cursor == Global.GetCursor(MouseCursor.Fill) && NativeMethods.IsControlDown())
                                pbMain.Cursor = Global.GetCursor(MouseCursor.FillLines);
                            else if (pbMain.Cursor == Global.GetCursor(MouseCursor.FillLines) && !NativeMethods.IsControlDown())
                                pbMain.Cursor = Global.GetCursor(MouseCursor.Fill);
                            break;
                        default:
                            if (pbMain.Cursor == Global.GetCursor(MouseCursor.Live))
                                pbMain.Cursor = Global.GetCursor(MouseCursor.Default);
                            break;
                    }
                }

                if (m_iMapLoadGraceCounter > 0)
                {
                    m_iMapLoadGraceCounter--;
                    return;
                }

                if (!m_bDirty)
                    return;

                m_bDirty = false;

                if (CurrentSheet == null)
                    return;

                if (CurrentSheet.NeverDisplayed)
                {
                    CurrentSheet.NeverDisplayed = false;
                    int iZoom = Properties.Settings.Default.DefaultZoom;
                    if (CurrentSheet.DefaultZoom > 0)
                        iZoom = CurrentSheet.DefaultZoom;
                    CurrentSheet.SquareSize = new Size(16 * iZoom / 100, 16 * iZoom / 100);
                }

                bool bAbortDraw = false;

                NativeMethods.SuspendDrawing(scMainNote.Panel1);

                if (CurrentSheet.ForceRefreshOnDisplay)
                {
                    CurrentSheet.ForceRefreshOnDisplay = false;
                    bAbortDraw = !Redraw(false);
                }
                else
                    bAbortDraw = !Redraw();

                if (bAbortDraw)
                {
                    ForceRefreshOnDisplay();
                    return;
                }

                if (m_bNeedCentering)
                {
                    CheckShowScrollbars();
                    SetDirty();
                    return;
                }

                if (m_rcSelection != Rectangle.Empty)
                    Global.FixRange(ref m_rcSelection, 0, 0, CurrentSheet.GridWidth, CurrentSheet.GridHeight);
                else
                    HideSelectText();

                if (m_bRecaptureSelection)
                {
                    m_bRecaptureSelection = false;
                    m_bmpUnderSelection = null;
                    RecaptureSelection();
                }

                DrawSelectionMarquee();

                if (m_bFirstRefresh)
                {
                    m_bFirstRefresh = false;
                    NativeMethods.SetForegroundWindow(Handle);
                }

                if (Properties.Settings.Default.NeedAutoArrange && Hacker != null && Hacker.IsValid)
                    AutoArrangeWindows(true);

                CheckShowScrollbars();

                if (!m_bMonstersChanged)
                {
                    if (Mode == BlockMode.Keyboard && m_bCenterKeyboard)
                        CenterMap(CurrentSheet.Cursor);
                    else
                        CheckCenterParty();
                }
                else
                    m_bMonstersChanged = false;

                labelLoadingMap.Visible = false;

                NativeMethods.ResumeDrawing(scMainNote.Panel1);
            }
            finally
            {
                m_bInDirtyTimer = false;
            }
        }

        private void CheckCenterParty()
        {
            if (m_bSkipNextCenter)
            {
                m_bSkipNextCenter = false;
                return;
            }

            if (!Properties.Settings.Default.CenterPartyInMap || Mode != BlockMode.Play || !ShowingCurrentMap)
                return;

            Point pt = TranslateToInternalMap(Hacker.GetPartyPosition());

            CenterMap(pt);
        }

        private void CenterMap(Point pt)
        {
            if (pt == Global.NullPoint)
                return;

            if (pbMain.Width <= panelMain.Width)
                return; // If the entire map fits in the panel, there is no point to the centering logic

            Point ptCenter = CurrentSheet.GetPixelForSquareCenter(pt);

            int iHoriz = ptCenter.X - (panelMain.Width / 2);
            int iVert = ptCenter.Y - (panelMain.Height / 2);

            Global.FixRange(ref iHoriz, panelMain.HorizontalScroll.Minimum, panelMain.HorizontalScroll.Maximum);
            Global.FixRange(ref iVert, panelMain.VerticalScroll.Minimum, panelMain.VerticalScroll.Maximum);

            panelMain.AutoScrollPosition = new Point(iHoriz, iVert);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (m_hacker == null)
                return;

            if (WindowState == FormWindowState.Minimized && Properties.Settings.Default.LinkedMinimizeAndRestore)
            {
                m_iIgnoreDOSBoxWindowState = 10;
                NativeMethods.MinimizeWindow(Hacker.DOSBoxWindow);
            }
            else if (WindowState != FormWindowState.Minimized && Properties.Settings.Default.LinkedMinimizeAndRestore && NativeMethods.IsMinimized(Hacker.DOSBoxWindow))
            {
                m_iIgnoreDOSBoxWindowState = 10;
                NativeMethods.RestoreWindow(Hacker.DOSBoxWindow);
            }
        }

        private void BeginEditNote(Point pt)
        {
            BeginEditNote(pt, tbNote);
        }

        private void ShowNotesPanel(bool bShow = true)
        {
            miViewNotesPanel.Checked = bShow;
            scMainNote.Panel2Collapsed = !miViewNotesPanel.Checked;
        }

        private void BeginEditNote(Point pt, Control sender)
        {
            bool bReadOnly = Properties.Settings.Default.ReadOnlyNotes || Mode == BlockMode.Play;
            ShowNotesPanel();

            m_ptCurrentNote = pt;
            UpdateLocation(pt);
            m_noteCurrent = CurrentSheet.NoteAtPoint(pt);
            if (m_noteCurrent == null)
            {
                if (bReadOnly)
                    return;
                m_noteCurrent = new MapNote();
            }

            tbNote.ReadOnly = bReadOnly;
            tbNote.BackColor = bReadOnly ? SystemColors.Control : SystemColors.Window;
            m_bEditingNote = true;
            tbNote.Rtf = "";        // Clear any formatting before editing
            tbNote.Text = m_noteCurrent.Text;
            pbSelectColor.BackColor = m_noteCurrent.Color;
            tbSymbol.Text = m_noteCurrent.Symbol;
            tbSymbol.ForeColor = m_noteCurrent.Color;
            if (bReadOnly)
            {
                btnClearNote.Visible = false;
                tbNote.SelectionStart = 0;
                tbNote.SelectionLength = 0;
            }
            else
            {
                btnClearNote.Visible = true;
                SetDirty();
                tbSymbol.ReadOnly = false;
                if (tbSymbol.Text == "")
                {
                    tbSymbol.Text = "N";
                    tbSymbol.ForeColor = Color.Black;
                }
                tbNote.Width = btnFinishNote.Left - 10 - tbNote.Left;
            }
            tbNote.ScrollBars = RichTextBoxScrollBars.Vertical;
            sender.Focus();
            btnClearNote.TabStop = true;
            btnFinishNote.TabStop = true;
        }

        private Point GetVertexAtMouse()
        {
            Point pt = pbMain.PointToScreen(Point.Empty);
            Point ptCursor = Cursor.Position;
            return CurrentSheet.GetVertexAtPoint(new Point(ptCursor.X - pt.X, ptCursor.Y - pt.Y));
        }

        private MapSquare GetSquareAtMouse()
        {
            Point pt = pbMain.PointToScreen(Point.Empty);
            Point ptCursor = Cursor.Position;
            return CurrentSheet.GetSquareAtPoint(new Point(ptCursor.X - pt.X, ptCursor.Y - pt.Y));
        }

        public Point GetSquareLocationAtMouse() { return GetSquareLocationAtMouse(true); }

        public Point GetSquareLocationAtMouse(bool bFixRange)
        {
            if (pbMain.IsDisposed)
                return Global.NullPoint;
            Point pt = pbMain.PointToScreen(Point.Empty);
            Point ptCursor = Cursor.Position;
            return CurrentSheet.GetSquareLocationAtPoint(new Point(ptCursor.X - pt.X, ptCursor.Y - pt.Y), bFixRange);
        }

        private Point GetSquareSelectionAtMouse()
        {
            Point pt = pbMain.PointToScreen(Point.Empty);
            Point ptCursor = Cursor.Position;
            return CurrentSheet.GetSquareLocationAtPoint(new Point(ptCursor.X - pt.X + CurrentSheet.SquareSize.Width / 2, ptCursor.Y - pt.Y + CurrentSheet.SquareSize.Height / 2));
        }

        private void ToggleSquareIcon(MapIcon icon)
        {
            Point pt = GetSquareLocationAtMouse();
            CurrentSheet.ToggleIcon(pt, icon);
            SetDirtyUnsaved();
        }

        private void DrawActionCurrentSquare(DrawMode drawLines)
        {
            Point pt = GetSquareLocationAtMouse();

            MapSquare square = CurrentSheet.Grid[pt.X, pt.Y];
            MapSheet.AddUndoBlock(m_undoBlocks, square, pt);
            if (m_drawMode == DrawMode.Fill)
            {
                if (square.Fill(m_dcCurrentBlock))
                    SetDirtyUnsaved();
                if (drawLines != DrawMode.None)
                {
                    if (CurrentSheet.HybridLines(m_undoBlocks, pt.X, pt.Y, m_dcCurrentLine, DrawMode.Fill))
                        SetDirtyUnsaved();
                }
            }
            else
            {
                if (square.Empty())
                    SetDirtyUnsaved();
                if (drawLines != DrawMode.None)
                {
                    if (CurrentSheet.HybridLines(m_undoBlocks, pt.X, pt.Y, m_dcCurrentLine, DrawMode.Erase))
                        SetDirtyUnsaved();
                }
            }
        }

        public bool SelectCharacter(int iChar)
        {
            if (!Global.FormVisible(m_formParty))
                return false;

            return m_formParty.SetCharacter(iChar);
        }

        public bool SelectNextCharacter(int iCount)
        {
            if (!Global.FormVisible(m_formParty))
                return false;

            return m_formParty.SelectNextCharacter(iCount);
        }

        public int GetSelectedCharacterAddress()
        {
            if (!Global.FormVisible(m_formParty))
                return Hacker.GetGameState().ActingCharAddress;

            return m_formParty.GetSelectedCharacterAddress();
        }

        public int GetSelectedCharacterPosition()
        {
            if (!Global.FormVisible(m_formParty))
                return Hacker.GetGameState().ActingPosition;

            return m_formParty.GetSelectedCharacterPosition();
        }

        public BaseCharacter GetSelectedCharacter()
        {
            if (!Global.FormVisible(m_formParty))
                return null;

            return m_formParty.GetCharacterByAddress(GetSelectedCharacterAddress());
        }

        public BaseCharacter GetActiveCharacter()
        {
            return GetCharacterByPosition(Hacker.GetGameState().ActingCaster);
        }

        public Rectangle[] TranslateToInternalMap(Rectangle[] rects, MapSheet sheet)
        {
            if (rects == null)
                return null;
            Rectangle[] rectsNew = new Rectangle[rects.Length];
            for (int i = 0; i < rects.Length; i++)
                rectsNew[i] = m_mapBook.TranslateLocationToMap(rects[i], sheet == null ? CurrentSheet : sheet);
            return rectsNew;
        }

        public Rectangle TranslateToInternalMap(Rectangle rc, MapSheet sheet = null)
        {
            return m_mapBook.TranslateLocationToMap(rc, sheet == null ? CurrentSheet : sheet);
        }

        public Rectangle TranslateToGameMap(Rectangle rc, MapSheet sheet = null)
        {
            return m_mapBook.TranslateLocationFromMap(rc, sheet == null ? CurrentSheet : sheet);
        }

        public Point TranslateToInternalMap(Point pt, MapSheet sheet = null)
        {
            return m_mapBook.TranslateLocationToMap(pt, sheet == null ? CurrentSheet : sheet);
        }

        public Point TranslateToGameMap(Point pt, MapSheet sheet = null)
        {
            return m_mapBook.TranslateLocationFromMap(pt, sheet == null ? CurrentSheet : sheet);
        }

        public PointF TranslateToInternalMap(PointF pt, MapSheet sheet = null)
        {
            return m_mapBook.TranslateLocationToMap(pt, sheet == null ? CurrentSheet : sheet);
        }

        public PointF TranslateToGameMap(PointF pt, MapSheet sheet = null)
        {
            return m_mapBook.TranslateLocationFromMap(pt, sheet == null ? CurrentSheet : sheet);
        }

        public void HideSelectText()
        {
            tbSelection.Visible = false;
            labelSelection.Visible = false;
        }

        public Point[] GetExpectedNextSquares(Point pt, Direction dir, bool bForwards)
        {
            int iMoveUp = m_mapBook.Location.IncreaseY == AxisIncreaseY.BottomToTop ? -1 : 1;
            int iMoveRight = m_mapBook.Location.IncreaseX == AxisIncreaseX.RightToLeft ? -1 : 1;

            if (!bForwards)
            {
                iMoveUp = - iMoveUp;
                iMoveRight = - iMoveRight;
            }

            // Return the square immediately ahead/behind, but also return the 
            // squares to the sides (if a turn-and-move is fast enough, we don't want to
            // erroneously report that as a teleport.

            switch(dir)
            {
                case Direction.Up: return new Point[] { new Point(pt.X, pt.Y + iMoveUp), new Point(pt.X+1, pt.Y), new Point(pt.X-1, pt.Y) };
                case Direction.Down: return new Point[] { new Point(pt.X, pt.Y - iMoveUp), new Point(pt.X+1, pt.Y), new Point(pt.X-1, pt.Y) };
                case Direction.Left: return new Point[] { new Point(pt.X - iMoveRight, pt.Y), new Point(pt.X, pt.Y+1), new Point(pt.X, pt.Y-1) };
                case Direction.Right: return new Point[] { new Point(pt.X + iMoveRight, pt.Y), new Point(pt.X, pt.Y + 1), new Point(pt.X - 1, pt.Y - 1) };
                default: return new Point[] { pt };
            }
        }

        private void UpdateLocation(Point pt)
        {
            pt = TranslateToGameMap(pt);
            Global.UpdateText(tbLocation, String.Format("{0},{1}", pt.X, pt.Y));
        }

        private void DrawSelectionMarquee()
        {
            if (Mode != BlockMode.Edit)
                return;

            if (m_ptLastSquare == m_ptNextSquare && m_selectionAction != SelectionActions.Create)
                return;

            if (m_rcSelection.Width < 0 || m_rcSelection.Height < 0)
            {
                m_rcSelection = Rectangle.Empty;
                return;
            }

            Rectangle rcSelection;

            switch (m_selectionAction)
            {
                case SelectionActions.Create:
                    m_rcSelection = Global.NormalizeRect(new Rectangle(m_ptOriginalCapture.X,
                        m_ptOriginalCapture.Y,
                        m_ptNextSquare.X - m_ptOriginalCapture.X,
                        m_ptNextSquare.Y - m_ptOriginalCapture.Y));
                    m_rcSelection.Width++;
                    m_rcSelection.Height++;
                    rcSelection = m_rcSelection;
                    break;
                case SelectionActions.Copy:
                case SelectionActions.Move:
                    rcSelection = m_rcSelection;
                    rcSelection.Offset(m_ptNextSquare.X - m_ptOriginalCapture.X, m_ptNextSquare.Y - m_ptOriginalCapture.Y);
                    break;
                default:
                    rcSelection = m_rcSelection;
                    break;
            }

            m_ptLastSquare = m_ptNextSquare;

            if (m_rcSelection.IsEmpty)
            {
                HideSelectText();
                return;
            }

            Global.UpdateText(tbSelection, String.Format("{0},{1} ({2},{3})", m_rcSelection.X, m_rcSelection.Y, m_rcSelection.Width, m_rcSelection.Height));
            tbSelection.Visible = true;
            labelSelection.Visible = true;

            Point ptOrigin = CurrentSheet.GetPixelForSquareOrigin(m_rcSelection.Location);
            Point ptOriginMargin = ptOrigin;
            Point pt1 = CurrentSheet.GetPixelForSquareOrigin(rcSelection.Location);
            Point pt2 = CurrentSheet.GetPixelForSquareOrigin(new Point(rcSelection.Right, rcSelection.Bottom));

            Size szMargin = CurrentSheet.SelectionMargin;

            ptOriginMargin.Offset(-szMargin.Width, -szMargin.Height);
            pt1.Offset(-szMargin.Width, -szMargin.Height);
            pt2.Offset(szMargin.Width, szMargin.Height);   // Include edge of next square

            Rectangle rcCrop = new Rectangle(pt1.X, pt1.Y, pt2.X - pt1.X, pt2.Y - pt1.Y);

            using (Graphics g = Graphics.FromImage(pbMain.Image))
            {
                if (m_bmpUnderSelection != null)
                {
                    g.DrawImage(m_bmpUnderSelection, m_ptUnderSelection);
                    pbMain.Invalidate(new Rectangle(m_ptUnderSelection, m_bmpUnderSelection.Size));
                }

                if (m_bmpUnderSelection != null)
                    m_bmpUnderSelection.Dispose();
                m_bmpUnderSelection = Global.GetCrop(pbMain.Image, rcCrop);
                m_ptUnderSelection = rcCrop.Location;

                if (m_bmpMoveBlocksGhost != null && m_selectionAction == SelectionActions.Move)
                {
                    g.DrawImage(m_bmpMoveBlocksGhost, ptOriginMargin);
                    pbMain.Invalidate(new Rectangle(ptOriginMargin, m_bmpMoveBlocksGhost.Size));
                }
                else if (m_bmpCopyBlocksGhost != null && m_selectionAction == SelectionActions.Copy)
                {
                    g.DrawImage(m_bmpCopyBlocksGhost, ptOriginMargin);
                    pbMain.Invalidate(new Rectangle(ptOriginMargin, m_bmpCopyBlocksGhost.Size));
                }

                if (m_bmpCopyMoveBlocks != null)
                {
                    g.DrawImage(m_bmpCopyMoveBlocks, m_ptUnderSelection);
                    pbMain.Invalidate(new Rectangle(m_ptUnderSelection, m_bmpCopyMoveBlocks.Size));
                }

                Color c = Color.FromArgb(48, Color.LightPink);
                g.FillRectangle(new SolidBrush(c), rcCrop);
                g.DrawRectangle(Pens.HotPink, rcCrop.X, rcCrop.Y, rcCrop.Width - 1, rcCrop.Height - 1);
                pbMain.Invalidate(rcCrop);
            }
        }

        private void DrawBlocksToMouse(UndoList undoBlocks, DrawMode drawLines)
        {
            Point pt = GetSquareLocationAtMouse();
            if (pt == m_ptLastSquare)
                return;

            if (m_bDrawStraightLine && m_orStraightLine == Orient.None)
            {
                if (Math.Abs(pt.X - m_ptOriginalCapture.X) > Math.Abs(pt.Y - m_ptOriginalCapture.Y))
                    m_orStraightLine = Orient.Horiz;
                else
                    m_orStraightLine = Orient.Vert;
            }

            switch(m_orStraightLine)
            {
                case Orient.Horiz:
                    pt.Y = m_ptOriginalCapture.Y;
                    break;
                case Orient.Vert:
                    pt.X = m_ptOriginalCapture.X;
                    break;
                default:
                    break;
            }

            switch (m_drawMode)
            {
                case DrawMode.Fill:
                    CurrentSheet.LineOfBlocks(undoBlocks, m_ptLastSquare, pt, m_dcCurrentBlock, drawLines == DrawMode.None ? drawLines : DrawMode.Fill, m_dcCurrentLine);
                    break;
                case DrawMode.Erase:
                    CurrentSheet.LineOfBlocks(undoBlocks, m_ptLastSquare, pt, m_dcCurrentErase, drawLines == DrawMode.None ? drawLines : DrawMode.Erase, m_dcCurrentLine);
                    break;
                case DrawMode.None:
                    CurrentSheet.LineOfBlocks(undoBlocks, m_ptLastSquare, pt, m_dcCurrentBlock, DrawMode.None, m_dcCurrentLine);
                    break;
                default:
                    break;
            }

            m_ptLastSquare = pt;
            SetDirtyUnsaved();
        }

        private void DrawEdgeToMouse(UndoList undoBlocks)
        {
            Point pt = GetVertexAtMouse();
            if (pt == m_ptLastVertex)
                return;

            if (m_bDrawStraightLine && m_orStraightLine == Orient.None)
            {
                if (Math.Abs(pt.X - m_ptOriginalCapture.X) > Math.Abs(pt.Y - m_ptOriginalCapture.Y))
                    m_orStraightLine = Orient.Horiz;
                else
                    m_orStraightLine = Orient.Vert;
            }

            switch (m_orStraightLine)
            {
                case Orient.Horiz:
                    pt.Y = m_ptOriginalCapture.Y;
                    break;
                case Orient.Vert:
                    pt.X = m_ptOriginalCapture.X;
                    break;
                default:
                    break;
            }

            CurrentSheet.DrawEdge(undoBlocks, m_ptLastVertex, pt, m_drawMode == DrawMode.Fill ? m_dcCurrentLine : m_dcCurrentEraseLine);

            m_ptLastVertex = pt;
            SetDirtyUnsaved();
        }

        private void panelMain_SizeChanged(object sender, EventArgs e)
        {
            if (m_bInDirtyTimer)
                return;
            m_bNeedCentering = true;
            SetDirty();
        }

        private string DisplayFileName()
        {
            if (String.IsNullOrWhiteSpace(m_strCurrentFile))
                return "Untitled";

            if (m_strCurrentFile == Global.InternalMapString)
                return Games.Name(Properties.Settings.Default.Game);

            return Path.GetFileNameWithoutExtension(m_strCurrentFile);
        }

        private void SaveAndDestroy(HackerBasedForm form, WindowType type, bool bAutoShowIfVisible = false)
        {
            if (form == null)
                return;
            WindowInfo wi = new WindowInfo(form, bAutoShowIfVisible && Global.FormVisible(form));
            form.Destroy(); // This sets a non-auto-show WindowInfo in the form's FormClosing override
            if (!Global.Windows.Info.ContainsKey(type))
                Global.Windows.Info.Add(type, wi);
            else
                Global.Windows.Info[type] = wi;
        }

        public bool ShuttingDown { get { return m_bShuttingDown; } }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (m_bUnsaved)
                {
                    switch (MessageBox.Show("Do you want to save changes to " + DisplayFileName() + "?", "Exit Program", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
                    {
                        case DialogResult.Yes:
                            if (!SaveCurrentFile())
                            {
                                e.Cancel = true;
                                return;
                            }
                            break;
                        case DialogResult.No:
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                        default:
                            break;
                    }
                }

                m_bShuttingDown = true;
                m_evtShutdown.Set();
                Application.DoEvents();

                m_timerDirty.Stop();
                m_timerFocusAll.Stop();
                m_timerHideTips.Stop();
                m_timerRedrawMap.Stop();
                m_timerWatchdog.Stop();

                if (Global.Windows != null)
                    Global.Windows.Set(WindowType.Main, new WindowInfo(this, true, scMainNote.SplitterDistance));
                bool bShowEncounters = Properties.Settings.Default.ShowEncounters || Global.FormVisible(m_formEncounters);
                SaveAndDestroy(m_formEncounters, WindowType.Encounter);
                if (Global.Windows != null)
                    Global.Windows.Info[WindowType.Encounter].AutoShow = bShowEncounters;
                Properties.Settings.Default.ShowEncounters = bShowEncounters;

                SaveAndDestroy(m_formParty, WindowType.Party, true);
                SaveAndDestroy(m_formShopInventory, WindowType.ShopInventory);
                SaveAndDestroy(m_formSpells, WindowType.SpellReference);
                SaveAndDestroy(m_formMonsters, WindowType.Monsters);
                SaveAndDestroy(m_formItems, WindowType.Items);
                SaveAndDestroy(m_formCreationAssistant, WindowType.CreationAssistant);
                SaveAndDestroy(m_formSearch, WindowType.Search);
                SaveAndDestroy(m_formInfo, WindowType.GameInfo, true);
                SaveAndDestroy(m_formScripts, WindowType.Scripts);
                SaveAndDestroy(m_formTrainingAssistant, WindowType.TrainingAssistant);
                SaveAndDestroy(m_formQuickRef, WindowType.QuickRef, true);
                SaveAndDestroy(m_formQuests, WindowType.Quests, true);
                if (Properties.Settings.Default.SaveDOSBoxPosition)
                    Properties.Settings.Default.DOSBoxPosition = m_hacker.GetDOSBoxRect().Location;
                SaveAndDestroy(m_formSelectSheet, WindowType.SheetSelector, false);
                if (Global.FormVisible(m_formEditLabels))
                    m_formEditLabels.HideRectangles();
                SaveAndDestroy(m_formEditLabels, WindowType.EditLabels, false);
                Properties.Settings.Default.ShowNotesPanel = miViewNotesPanel.Checked;

                KeyboardHook.Stop();

                if (m_hacker != null)
                    m_hacker.Stop();
            }
            catch (Exception)
            {
                // Ignore anything on shutdown
            }

            if (Global.Windows != null)
                Properties.Settings.Default.Windows = Global.Windows;
            Properties.Settings.Default.MemoryGuesses = Global.MemoryGuesses;
            Properties.Settings.Default.GracefulShutdown = true;
            Properties.Settings.Default.Save();
        }

        private void UpdateEditMenu()
        {
            miEditUnclear.Visible = (m_noteCanceled != null);

            if (CurrentSheet.UndoContainer.UndoAvailable > 0)
            {
                miEditUndo.Enabled = true;
                miEditUndo.Text = "&Undo " + CurrentSheet.UndoContainer.UndoActionName(0);
            }
            else
            {
                miEditUndo.Enabled = false;
                miEditUndo.Text = "&Undo";
            }

            if (CurrentSheet.UndoContainer.RedoAvailable > 0)
            {
                miEditRedo.Enabled = true;
                miEditRedo.Text = "&Redo " + CurrentSheet.UndoContainer.RedoActionName(0);
            }
            else
            {
                miEditRedo.Enabled = false;
                miEditRedo.Text = "&Redo";
            }

            bool bSelection = (m_rcSelection != Rectangle.Empty);

            miEditCut.Enabled = bSelection;
            miEditCopy.Enabled = bSelection;
            miEditDelete.Enabled = bSelection;

            miEditCrop.Enabled = bSelection;

            miEditLiveSquares.Checked = m_bEditingLiveSquares;

            UpdatePasteMenu(miEditPaste);
        }

        private bool EditingNote
        {
            get { return m_bEditingNote; }
        }

        private void miSheetPrevious_Click(object sender, EventArgs e)
        {
            ProcessSheetPrevious();
        }

        private void ProcessSheetPrevious()
        {
            if (EditingNote)
                FinishNote();

            int iSheet = m_iCurrentSheet - 1;
            if (iSheet < 0)
            {
                if (Properties.Settings.Default.SheetWraparound)
                    iSheet = m_mapBook.Sheets.Count - 1;
                else
                    return;
            }

            SelectSheet(iSheet);
        }

        private bool GotoGameMapIndexSheet(int iIndex)
        {
            if (iIndex == -1)
                return false;

            if (EditingNote)
                FinishNote();

            for(int index = 0; index < m_mapBook.Sheets.Count; index++)
            {
                if (m_mapBook.Sheets[index].GameMapIndex == iIndex)
                {
                    SelectSheet(index);
                    if (Properties.Settings.Default.RevealAdjacentInaccessible && !IgnoreInaccessible &&
                        (!Properties.Settings.Default.UpdateCartWhenInaccessibleRevealed || !Properties.Settings.Default.EnableMemoryWrite || m_bNeedCartUpdate))
                    {
                        m_bNeedCartUpdate = false;
                        m_timerRedrawMap.Start();
                    }
                    return true;
                }
            }
            return false;
        }

        public bool GotoSheet(MapSheet sheet)
        {
            if (EditingNote)
                FinishNote();

            for(int index = 0; index < m_mapBook.Sheets.Count; index++)
            {
                if (m_mapBook.Sheets[index] == sheet)
                {
                    SelectSheet(index);
                    return true;
                }
            }
            return false;
        }

        public bool GotoSheet(int iMapIndex)
        {
            if (EditingNote)
                FinishNote();

            for (int index = 0; index < m_mapBook.Sheets.Count; index++)
            {
                if (m_mapBook.Sheets[index].GameMapIndex == iMapIndex)
                {
                    SelectSheet(index);
                    return true;
                }
            }
            return false;
        }

        private void miSheetNext_Click(object sender, EventArgs e)
        {
            ProcessSheetNext();
        }

        private void ProcessSheetNext()
        {
            int iSheet = m_iCurrentSheet + 1;
            if (iSheet >= m_mapBook.Sheets.Count)
            {
                if (Properties.Settings.Default.SheetWraparound)
                    iSheet = 0;
                else
                    return;
            }

            if (EditingNote)
                FinishNote();

            SelectSheet(iSheet);
        }

        private void SelectSheet(int iSheet)
        {
            m_iCurrentSheet = iSheet;
            CancelSelection();
            UpdateTitle();
            ForceRefreshOnDisplay();
            UpdateSheetMenu();
            CheckLabelWindow();
        }

        private void CheckLabelWindow()
        {
            if (CurrentSheet != null && Global.FormVisible(m_formEditLabels))
                m_formEditLabels.SetLabels(CurrentSheet.Labels);
        }

        private void UpdateSheetMenu()
        {
            miSheetAdd.Enabled = !m_bExportingMaps;
            miSheetPrevious.Enabled = (m_iCurrentSheet > 0 || (m_mapBook.Sheets.Count > 1 && Properties.Settings.Default.SheetWraparound));
            miSheetNext.Enabled = (m_iCurrentSheet < m_mapBook.Sheets.Count - 1 || (m_mapBook.Sheets.Count > 1 && Properties.Settings.Default.SheetWraparound));
            miSheetRemove.Enabled = !m_bExportingMaps && (m_mapBook.Sheets.Count > 1);
        }

        private void HideSpoilers(RichTextBox rtb, string strNote)
        {
            if (strNote == null || strNote.IndexOfAny(Global.SpoilerChars) == -1 || !Properties.Settings.Default.HideSpoilers)
            {
                // Don't mess with the RTF codes in the simple case that there are no spoiler tags.
                if (rtb.Text == strNote)
                    return;
                rtb.Rtf = "";
                rtb.Text = strNote;
                return;
            }
            const string strNoSpoiler = @"\cf0\highlight0 ";
            const string strSpoiler = @"\cf1\highlight1 ";
            StringBuilder sbRtf = new StringBuilder(@"{\rtf1{\colortbl ;");
            sbRtf.Append(Global.RtfColorString(Properties.Settings.Default.SpoilerColor));
            sbRtf.Append(@";}");
            sbRtf.Append(strNoSpoiler);
            sbRtf.Append(strNote.Replace("\r", "").Replace("\n", "\\par\n"));
            sbRtf.Append(@"}");
            sbRtf.Replace(Global.SpoilerStart.ToString(), strSpoiler);
            sbRtf.Replace(Global.SpoilerEnd.ToString(), strNoSpoiler);
            rtb.Rtf = sbRtf.ToString();
        }

        private void SetNoteText(Point pt, bool bOverrideVisited = false)
        {
            if (!SquareIsVisible(pt) && !bOverrideVisited && !Properties.Settings.Default.ShowUnvisitedNotes)
                return;

            MapNote note = CurrentSheet.NoteAtPoint(pt);
            if (note == null)
            {
                note = new MapNote();
                note.Text = Properties.Resources.ClickToAddNote;
            }

            HideSpoilers(tbNote, note.Text);

            if (pbSelectColor.BackColor != note.Color)
                pbSelectColor.BackColor = note.Color;

            if (tbSymbol.Text != note.Text)
                tbSymbol.Text = note.Symbol;

            if (tbSymbol.ForeColor != note.Color)
                tbSymbol.ForeColor = note.Color;
        }

        private void ClearIconSelection()
        {
            if (m_iconsForm != null && !m_iconsForm.IsDisposed)
                m_iconsForm.SelectedIcon = null;
        }

        private void SetMode(BlockMode mode, bool bClearIcon = true)
        {
            if (m_bSettingMode)
                return;

            m_bSettingMode = true;

            if (pbMain.Capture)
                ForceMouseUp(m_btnCaptured); // Don't switch modes in the middle of drawing

            if (EditingNote)
                FinishNote();

            if (bClearIcon)
                ClearIconSelection();

            if (mode == BlockMode.Edit)
            {
                if (miModeEdit.Checked)
                {
                    CancelSelection();
                    mode = m_modeEditToggle;
                }
                else
                    m_modeEditToggle = Mode;
            }
            else if (miModeEdit.Checked)
                CancelSelection();

            bool bWasKeyboard = miModeKeyboard.Checked;

            miModeBlock.Checked = (mode == BlockMode.Block);
            miModeLine.Checked = (mode == BlockMode.Line);
            miModeHybrid.Checked = (mode == BlockMode.Hybrid);
            miModeNotes.Checked = (mode == BlockMode.Notes);
            miModeKeyboard.Checked = (mode == BlockMode.Keyboard);
            miModeEdit.Checked = (mode == BlockMode.Edit);
            miModePlay.Checked = (mode == BlockMode.Play);
            miModeFill.Checked = (mode == BlockMode.Fill);

            scNoteColors.Panel1Collapsed = (mode == BlockMode.Play);
            tbNote.Left = (mode == BlockMode.Play ? labelCursor2.Right + 12 : 0);
            tbNote.Width = scNoteColors.Panel2.Width - tbNote.Left;

            foreach(BlockModeItem item in tscMode.Items)
            {
                if (item.Mode == Mode)
                {
                    tscMode.SelectedItem = item;
                    break;
                }
            }

            if (mode == BlockMode.Notes)
            {
                SetNoteText(GetSquareLocationAtMouse());
            }
            else
            {
                if (EditingNote)
                    FinishNote();
                tbNote.Text = Properties.Resources.ClickToAddNote;
            }


            if (mode == BlockMode.Keyboard)
            {
                m_bDrawCursor = true;
                UpdateLocation(CurrentSheet.Cursor);
                SetNoteText(CurrentSheet.Cursor);
            }
            else
            {
                m_bDrawCursor = false;
            }

            if (!miModeEdit.Checked)
                HideSelectText();

            CheckCurrentIcon();
            SetDirty();

            m_bEditingLiveSquares = false;
            ForceRefreshOnDisplay();

            pbMain.Cursor = CursorForMode(Mode);
            m_bSettingMode = false;
        }

        private Cursor CursorForMode(BlockMode mode)
        {
            switch (Mode)
            {
                case BlockMode.Fill: return Global.GetCursor(MouseCursor.Fill);
                case BlockMode.Line: return Global.GetCursor(MouseCursor.Pencil);
                case BlockMode.Block: return Global.GetCursor(MouseCursor.Block);
                case BlockMode.Hybrid: return Global.GetCursor(MouseCursor.Hybrid);
                case BlockMode.Notes: return Global.GetCursor(MouseCursor.Note);
                default: return Cursors.Default;
            }
        }

        public void SetCursor(Point pt)
        {
            CurrentSheet.Cursor = pt;
            UpdateLocation(CurrentSheet.Cursor);
            m_ptCurrentNote = CurrentSheet.Cursor;
            SetNoteText(CurrentSheet.Cursor);
        }

        public string AutoLoadFile
        {
            get
            {
                if (Games.IsImplemented(Properties.Settings.Default.Game))
                    return Properties.Settings.Default.AutoLoadFiles.Get(Properties.Settings.Default.Game);
                return Properties.Settings.Default.AutoLoadFiles.Get(GameNames.MightAndMagic1);
            }

            set
            {
                if (Games.IsImplemented(Properties.Settings.Default.Game))
                    Properties.Settings.Default.AutoLoadFiles.Set(Properties.Settings.Default.Game, value);
                else
                    Properties.Settings.Default.AutoLoadFiles.Set(GameNames.MightAndMagic1, value);
            }
        }

        private void SetInternalMapMenu()
        {
            miFileLoadInternalMap.DropDownItems.Clear();

            foreach (GameNames game in Games.ImplementedGames)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(Games.Name(game));
                item.Tag = game;
                item.Click += new EventHandler(LoadInternalMap_click);
                miFileLoadInternalMap.DropDownItems.Add(item);
            }
        }

        void LoadInternalMap_click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (sender == null)
                return;

            LoadFile(Games.MapForGame((GameNames)item.Tag));
        }

        private void CheckShortcuts()
        {
            if (Properties.Settings.Default.Shortcuts == null || Properties.Settings.Default.Shortcuts.IsEmpty)
            {
                m_shortcuts = Global.DefaultShortcuts;
                Properties.Settings.Default.Shortcuts = m_shortcuts;
            }

            try
            {
                SetMenuShortcuts();
            }
            catch (Exception)
            {
                m_shortcuts = Global.DefaultShortcuts;
                SetMenuShortcuts();
            }
        }

        private void ResetWindowSizes()
        {
            Global.Windows = new WindowInfoList();
            Global.Windows.Set(WindowType.Party, new WindowInfo(Rectangle.Empty, false, true));
            Global.Windows.Set(WindowType.GameInfo, new WindowInfo(Rectangle.Empty, false, true));
        }

        private void AddGuess(GameNames game, params MemoryGuess[] guesses)
        {
            Dictionary<GameNames, MemoryGuess[]> dict = Global.MemoryGuesses.Guesses;

            MemoryGuess[] mgOld = null;

            if (dict.ContainsKey(game))
                mgOld = dict[game];
            else
            {
                mgOld = new MemoryGuess[0];
                dict.Add(game, mgOld);
            }

            List<MemoryGuess> mgNew = new List<MemoryGuess>();
            foreach (MemoryGuess guess in guesses)
            {
                if (!mgOld.Contains(guess))
                    mgNew.Add(guess);
            }
            if (mgNew.Count == 0)
                return;

            List<MemoryGuess> mgCombine = new List<MemoryGuess>(mgOld);
            mgCombine.AddRange(mgNew);
            dict[game] = mgCombine.ToArray();
        }

        private void Upgrade()
        {
            if (Properties.Settings.Default.DOSBoxCaption == "^DOSBox [S0]")
                Properties.Settings.Default.DOSBoxCaption = "^DOSBox [SE0]";
            if (Properties.Settings.Default.UIElementOptions == null || !Properties.Settings.Default.UIElementOptions.Elements.ContainsKey(ColoredUIElements.DisabledSpell))
                Properties.Settings.Default.UIElementOptions = new UIElementOptions(true);
            if (Properties.Settings.Default.MM3SpellKeys.SelectedGame != GameNames.None ||
                Properties.Settings.Default.MM45SpellKeys.SelectedGame != GameNames.None)
            {
                SpellHotkeyCollection shkc = new SpellHotkeyCollection();
                shkc.Hotkeys.Add(GameNames.MightAndMagic3, Properties.Settings.Default.MM3SpellKeys);
                shkc.Hotkeys.Add(GameNames.MightAndMagic45, Properties.Settings.Default.MM45SpellKeys);
                Properties.Settings.Default.MM3SpellKeys = new SpellHotkeyList();
                Properties.Settings.Default.MM45SpellKeys = new SpellHotkeyList();
            }

            Notifications notifications = Properties.Settings.Default.Notifications;
            if (notifications != null && notifications.Alerts.ContainsKey(Action.SpellHotkey1) &&
                notifications.Alerts[Action.SpellHotkey1].Message.Contains("Ready spell set to"))
            {
                foreach (Action action in notifications.Alerts.Keys)
                {
                    if (notifications.Alerts[action].Message.Contains("Ready spell set to"))
                        notifications.Alerts[action].Message = notifications.Alerts[action].Message.Replace("Ready spell set to", "$SpellAction") + " $successState";
                }
                Properties.Settings.Default.Notifications = notifications;
            }

            AddGuess(GameNames.MightAndMagic45, new MemoryGuess(1000000, 8320));
            AddGuess(GameNames.MightAndMagic45, new MemoryGuess(1000000, 8344));
            foreach (GameNames game in Games.WizardryGames)
            {
                if (!Global.MemoryGuesses.Guesses.ContainsKey(game))
                    AddGuess(game, new MemoryGuess(1000000, 8066), new MemoryGuess(1000000, 7514));
                if (!Properties.Settings.Default.AutoLaunchShortcuts.ContainsKey(game))
                {
                    Properties.Settings.Default.AutoLaunchShortcuts.Set(game, String.Format(@"C:\Games\Wizardry\Wizardry-0{0}\Wizardry {0}.lnk", (int)(game - GameNames.Wizardry1)));
                }
            }
            AddGuess(GameNames.MightAndMagic2, new MemoryGuess(4000000, 133744));
            if (!Global.MemoryGuesses.Guesses.ContainsKey(GameNames.BardsTale1))
                AddGuess(GameNames.BardsTale1, new MemoryGuess(1000000, 154334), new MemoryGuess(1000000, 153966));
            if (!Properties.Settings.Default.AutoLoadFiles.ContainsKey(GameNames.BardsTale1))
                Properties.Settings.Default.AutoLoadFiles.Set(GameNames.BardsTale1, @"C:\Games\Bard's_Tale\Bard's_Tale-01\Bard's Tale 1.lnk");
            if (!Global.MemoryGuesses.Guesses.ContainsKey(GameNames.BardsTale2))
                AddGuess(GameNames.BardsTale2, new MemoryGuess(1000000, 243687), new MemoryGuess(1000000, 244055));
            if (!Properties.Settings.Default.AutoLoadFiles.ContainsKey(GameNames.BardsTale2))
                Properties.Settings.Default.AutoLoadFiles.Set(GameNames.BardsTale2, @"C:\Games\Bard's_Tale\Bard's_Tale-02\Bard's Tale 2.lnk");
            if (!Global.MemoryGuesses.Guesses.ContainsKey(GameNames.BardsTale3))
                AddGuess(GameNames.BardsTale3, new MemoryGuess(1000000, 213457), new MemoryGuess(1000000, 213089));
            if (!Properties.Settings.Default.AutoLoadFiles.ContainsKey(GameNames.BardsTale3))
                Properties.Settings.Default.AutoLoadFiles.Set(GameNames.BardsTale3, @"C:\Games\Bard's_Tale\Bard's_Tale-03\Bard's Tale 3.lnk");
            if (!Global.MemoryGuesses.Guesses.ContainsKey(GameNames.EyeOfTheBeholder1))
                AddGuess(GameNames.EyeOfTheBeholder1, new MemoryGuess(1000000, 6626), new MemoryGuess(1000000, 6384));
            if (!Global.MemoryGuesses.Guesses.ContainsKey(GameNames.EyeOfTheBeholder2))
                AddGuess(GameNames.EyeOfTheBeholder2, new MemoryGuess(1000000, 6626), new MemoryGuess(1000000, 6384));
            Global.MemoryGuesses.Guesses.Remove(GameNames.Ultima1);
            if (!Global.MemoryGuesses.Guesses.ContainsKey(GameNames.Ultima1))
                AddGuess(GameNames.Ultima1, new MemoryGuess(1001000, 6386));
            foreach (GameNames game in new GameNames[] { GameNames.EyeOfTheBeholder1, GameNames.EyeOfTheBeholder2, GameNames.EyeOfTheBeholder3, GameNames.Ultima1 })
            {
                if (!Properties.Settings.Default.AutoLaunchShortcuts.ContainsKey(game))
                {
                    GamePathTag tag = WizardForm.FindGamePath(game, Games.GetRegistries(game), Games.GetDefaultLink(game));
                    Properties.Settings.Default.AutoLaunchShortcuts.Set(game, tag.Path);
                }
            }

            foreach (GameNames game in Games.ImplementedGames)
            {
                if (!Properties.Settings.Default.AutoLoadFiles.ContainsKey(game))
                    Properties.Settings.Default.AutoLoadFiles.Set(game, Global.InternalMapString);
            }
            if (Properties.Settings.Default.UIElementOptions == null)
                Properties.Settings.Default.UIElementOptions = new UIElementOptions();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            m_clOptions = new CLOptions(Environment.GetCommandLineArgs(), true);
            if (m_clOptions.ForceDebugOff)
                Global.Debug = false;
            else if (m_clOptions.ForceDebugOn)
                Global.Debug = true;

            if (m_clOptions.Help || m_clOptions.Usage)
            {
                MessageBox.Show(CLOptions.GetUsage(), "Where Are We");
                Environment.Exit(0);
            }

            if (!String.IsNullOrWhiteSpace(m_clOptions.SettingsFile))
                OptionsForm.ImportSettingsFile(m_clOptions.SettingsFile);
            else if (m_clOptions.ResetSettings)
                OptionsForm.ResetSettings();

            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            if (identity != null &&
                !(new WindowsPrincipal(identity).IsInRole(WindowsBuiltInRole.Administrator)) &&
                Properties.Settings.Default.WarnNonAdmin)
            {
                if (MessageBox.Show("Although \"Where Are We\" will run in normal (non-administrator) mode, it will not be able to access " +
                    "the memory space of a DOSBox game that is being run as an administrator (which is often necessary in order for the game to write to files in the " +
                    "installation directory).\r\n\r\nContinue as a non-administrator (you will only be warned once)?", "Non-administrator mode detected",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    Environment.Exit(0);
                Properties.Settings.Default.WarnNonAdmin = false;
            }

            Global.Windows = Properties.Settings.Default.Windows;
            if (Global.Windows == null)
                ResetWindowSizes();

            Global.MemoryGuesses = Properties.Settings.Default.MemoryGuesses;
            if (Global.MemoryGuesses == null)
                Global.MemoryGuesses = new MemoryGuesses();

            Upgrade();

            AdjustTitleBox();
            ShowToolbar(Properties.Settings.Default.ShowToolbar);
            CheckShortcuts();

            bool bLaunchedGame = false;
            if (!Properties.Settings.Default.WizardRun)
                bLaunchedGame = RunWizard();

            SetInternalMapMenu();

            if (m_clOptions.Game != GameNames.None)
            {
                Properties.Settings.Default.Game = m_clOptions.Game;
                if (String.IsNullOrEmpty(m_clOptions.Map))
                    m_clOptions.Map = Games.MapForGame(m_clOptions.Game);
            }

            menuDebug.Visible = Global.Debug;
            m_lastLocation = new BasicLocation();
            m_bOverrideAutoSwitch = true;

            ManualSpellWindowClose = false;
            tseTitle.ReadOnly = Properties.Settings.Default.ReadOnlyMaps;
            UpdateEncounterMenu();
            UpdateGameMenu();

            FinishNote();
            WindowInfo info = Global.Windows.Get(WindowType.Main);
            if (!info.IsEmpty)
            {
                if (Global.AllowableWindowLocations.Contains(info.NormalSize.Location))
                    Location = info.NormalSize.Location;
                else
                    Location = Global.FixWindowLocation(info.NormalSize.Location);

                Width = info.NormalSize.Width;
                Height = info.NormalSize.Height;
                if (info.SplitPositions != null && info.SplitPositions.Length > 0)
                    Global.SetSplitterDistance(scMainNote, info.SplitPositions[0]);
            }

            m_iconsForm = new IconsForm(tsbIcon);
            m_iconsForm.SetIcons();
            m_iconsForm.SelectedIcon = null;
            m_iconsForm.SelectedColor = Color.Black;
            m_iconsForm.OnIconSelected += m_iconsForm_OnIconSelected;
            SelectBlock(0);
            SelectLine(0);

            bool bTest = Properties.Settings.Default.GracefulShutdown;

            if (!Properties.Settings.Default.GracefulShutdown &&
                Properties.Settings.Default.AutoSave && 
                File.Exists(Environment.ExpandEnvironmentVariables(Properties.Settings.Default.AutoSaveFile)) &&
                MessageBox.Show("\"Where Are We?\" did not shut down properly the last time it was run; would you like to open the autosave file?",
                    "Previous crash detected", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                LoadFileWithoutConfirmation(Environment.ExpandEnvironmentVariables(Properties.Settings.Default.AutoSaveFile), true);
                m_strCurrentFile = String.Empty;    // don't save back to the autosave by pressing Ctrl+S
            }
            else if (!String.IsNullOrWhiteSpace(m_clOptions.Map))
                LoadFileWithoutConfirmation(m_clOptions.Map, true);
            else if (AutoLoadFile == Global.InternalMapString || File.Exists(AutoLoadFile))
                LoadFileWithoutConfirmation(AutoLoadFile, true);

            SetMode(Properties.Settings.Default.DefaultMode);

            if (!bLaunchedGame)
                CreateHacker(Properties.Settings.Default.Game, false);

            SetNoteTemplateMenu();

            if ((m_hacker == null || !m_hacker.Running) && Properties.Settings.Default.AutoLaunchGame)
            {
                LaunchGame(Properties.Settings.Default.Game);
            }
            else if (m_hacker != null)
            {
                if (NativeMethods.IsMinimized(Hacker.DOSBoxWindow))
                    NativeMethods.RestoreWindow(Hacker.DOSBoxWindow);
                if (Properties.Settings.Default.DOSBoxPosition != Global.NullPoint && Properties.Settings.Default.SaveDOSBoxPosition)
                    m_hacker.SetDOSBoxPosition(Properties.Settings.Default.DOSBoxPosition);
                Hacker.FocusDOSBox();
            }

            if (Properties.Settings.Default.EnableGlobalShortcuts)
                KeyboardHook.Start(this, m_shortcuts.KeysWanted);

            ShowNotesPanel(Properties.Settings.Default.ShowNotesPanel);
            SetShowInTaskbar();

            if (!System.Diagnostics.Debugger.IsAttached)    // Don't warn about the autosave every time we debug the program
            {
                Properties.Settings.Default.GracefulShutdown = false;   // Set to true when the program exits, to detect crashes
                Properties.Settings.Default.Save();
            }

            m_bInDirtyTimer = false;
            SetDirty();
            m_timerDirty.Start();
            m_timerWatchdog.Start();
        }

        private void CreateHacker(GameNames game, bool bShowErrors = true, bool bIgnoreAutoshow = false)
        {
            m_hacker = Games.CreateHacker(game);
            if (!CheckGameMemory() && bShowErrors)
                m_showNext = new ShowNext(Global.MemoryScanFail, "Memory scanner creation failed");
            if (!bIgnoreAutoshow)
                CheckAutoShowWindows();
        }

        public MemoryHacker Hacker { get { return m_hacker; } }
        public MainForm Main { get { return this; } }

        private void SetNoteTemplateMenu()
        {
            miGridAddNote.DropDownItems.Clear();
            foreach (string str in Properties.Settings.Default.NoteTemplates)
            {
                ToolStripMenuItem item = (ToolStripMenuItem)miGridAddNote.DropDownItems.Add(
                    String.Format("{0}: {1}", miGridAddNote.DropDownItems.Count + 1, str.Replace('\t',':')));
                item.Click += new EventHandler(miGridAddNoteTemplate_Click);
                item.Tag = new NoteTemplateTag(str);
            }
        }

        private void LaunchGameThread(object o)
        {
            try
            {
                LaunchGameInfo info = (LaunchGameInfo)o;

                DateTime dtStart = DateTime.Now;
                while (info.Proc.MainWindowHandle == IntPtr.Zero)
                {
                    if ((DateTime.Now - dtStart).TotalSeconds > 3)
                        break;

                    if (m_evtShutdown.WaitOne(100))
                        return;
                }

                int iMaxDelay = Properties.Settings.Default.GameStartDelay;
                while (!m_evtShutdown.WaitOne(100))
                {
                    GameReadyState ready = GameReadyState.NotReady;
                    if (info.Main.Hacker != null)
                        ready = info.Main.Hacker.GameReady;

                    switch (ready)
                    {
                        case GameReadyState.Ready:
                            SetLaunchFinished(true);
                            return;
                        case GameReadyState.NeedDelay:
                            Thread.Sleep(2000);
                            SetLaunchFinished(true);
                            return;
                        default:
                            break;
                    }

                    iMaxDelay -= 100;
                    if (iMaxDelay <= 0)
                        break;
                }

                SetLaunchFinished(true);
            }
            catch (ThreadAbortException)
            {
                // User clicked the Abort button; do not set m_bLaunchFinished, as that tells the MemoryHacker we are ready for it
            }
        }

        private void LaunchGame(GameNames game)
        {
            try
            {
                string str = Properties.Settings.Default.AutoLaunchShortcuts.Get(game, String.Empty);
                m_hacker.Stop();

                if (m_threadLaunch != null && m_threadLaunch.IsAlive)
                    m_threadLaunch.Abort();

                if (Global.FormVisible(m_formWait))
                    m_formWait.Close();

                Process proc = new Process();
                proc.StartInfo.FileName = str;
                if (!File.Exists(proc.StartInfo.FileName))
                {   
                    m_showNext = new ShowNext(String.Format("The shortcut for game \"{0}\" does not exist ({1})\r\nPlease check the path in the options dialog.",
                        Games.Name(game), proc.StartInfo.FileName), "Invalid Shortcut Path");
                    return;
                }
                if (!proc.Start())
                {
                    m_showNext = new ShowNext(String.Format("Could not launch shortcut for game \"{0}\"\r\nPlease check the path in the options dialog.",
                        Games.Name(game)), "Invalid Shortcut Path");
                    return;
                }

                m_bLaunchFinished = false;  // Calling SetLaunchFinished() would restart the watchtimer with the wrong interval
                m_bDosBoxPosSet = false;

                m_iWatchdogInterval = 500; // Check frequently while launching

                ParameterizedThreadStart ts = new ParameterizedThreadStart(LaunchGameThread);
                m_threadLaunch = new Thread(ts);
                m_threadLaunch.Start(new LaunchGameInfo(proc, this));

                m_formWait = new WaitForm();
                m_formWait.SetAbort(m_formWait_OnAbortLaunch);
                m_formWait.SetWaitText("Waiting for game to start...");
                m_formWait.Show();
                m_formWait.Select();
            }
            catch(Exception ex)
            {
                m_showNext = new ShowNext(String.Format("Could not launch shortcut for game \"{0}\"\r\nException: {1}",
                    Games.Name(game), ex.Message), "Invalid Shortcut Path");
            }
        }

        void m_formWait_OnAbortLaunch(object sender, EventArgs e)
        {
            m_formWait.Close();
            if (m_threadLaunch != null)
                m_threadLaunch.Abort();

            SetLaunchFinished(false);
            m_threadLaunch = null;
        }

        private void SetLaunchFinished(bool bFinished)
        {
            m_iWatchdogInterval = Properties.Settings.Default.WatchdogTimer;
            m_bLaunchFinished = bFinished;  // "true" starts the MemoryHacker
        }

        private void EndEditNote()
        {
            tbNote.ScrollBars = RichTextBoxScrollBars.None;
            m_bEditingNote = false;
            tbNote.ReadOnly = true;
            tbNote.BackColor = SystemColors.Control;
            tbSymbol.ReadOnly = true;
            tbNote.Width = btnFinishNote.Right - tbNote.Left;
            tbSymbol.SelectionStart = 0;
            tbSymbol.SelectionLength = 0;
            btnClearNote.TabStop = false;
            btnFinishNote.TabStop = false;
            SetNoteText(CurrentNotePoint);
        }

        private void btnClearNote_Click(object sender, EventArgs e)
        {
            if (m_noteCurrent == null)
                return;

            SaveUncancelInfo();
            ClearNote();

            EndEditNote();
        }

        private void SaveUncancelInfo()
        {
            if (!String.IsNullOrWhiteSpace(tbNote.Text))
                m_noteCanceled = new MapNote(tbNote.Text, pbSelectColor.BackColor, tbSymbol.Text, m_noteCurrent.Location);
            else
                m_noteCanceled = null;
        }

        private void DeleteNote(MapNote note)
        {
            if (Properties.Settings.Default.ReadOnlyNotes)
                return;

            if (!String.IsNullOrEmpty(tbNote.Text))
            {
                CurrentSheet.ClearRedo();
                CurrentSheet.AddUndoNote(note.Location);
            }

            note.Symbol = "";
            CurrentSheet.SetNote(note);

            ClearNote();
            SetDirtyUnsaved();
        }

        private void ClearNote()
        {
            SetNoteText(CurrentNotePoint);
        }

        private void btnFinishNote_Click(object sender, EventArgs e)
        {
            FinishNote();
        }

        private void CancelNote()
        {
            SaveUncancelInfo();
            EndEditNote();
            SetDirty();
        }

        private void FinishNote()
        {
            if (!Properties.Settings.Default.ReadOnlyNotes && Mode != BlockMode.Play)
            {
                if (m_noteCurrent != null)
                {
                    m_noteCanceled = null;
                    CurrentSheet.ClearRedo();
                    CurrentSheet.AddUndoNote(m_noteCurrent.Location);
                    m_noteCurrent.Text = tbNote.Text;
                    m_noteCurrent.Symbol = tbSymbol.Text;
                    m_noteCurrent.Color = pbSelectColor.BackColor;
                    CurrentSheet.SetNote(m_noteCurrent);
                    SetDirtyUnsaved();
                }
            }
            EndEditNote();
        }

        private void MoveCursor(int iX, int iY)
        {
            Point pt = CurrentSheet.Cursor;
            iX = pt.X + iX;
            iY = pt.Y + iY;
            Global.FixRange(ref iX, 0, CurrentSheet.GridWidth-1);
            Global.FixRange(ref iY, 0, CurrentSheet.GridHeight-1);
            if (pt.X == iX && pt.Y == iY)
                return; // no change -> no redraw

            pt = new Point(iX, iY);
            UpdateLocation(pt);
            SetNoteText(pt);
            CurrentSheet.Cursor = pt;
            CenterMap(pt);
            SetDirty();
        }

        private void UpdateTitle(string strDefault = "")
        {
            if (CurrentSheet == null)
                tseTitle.Text = strDefault;
            else
            {
                tseTitle.Text = CurrentSheet.Title;
                UpdateMapMenu(m_iCurrentSheet, CurrentSheet.Title);
            }
        }

        private void MenuViewFitWindow()
        {
            FitWindow();
        }

        private void FitWindow(int iWidthSquares = -1, int iHeightSquares = -1, int iZoom = -1)
        {
            int iWidth = (iZoom < 1 ? CurrentSheet.SquareSize.Width : 16 * iZoom / 100);
            int iHeight = (iZoom < 1 ? CurrentSheet.SquareSize.Height : 16 * iZoom / 100);

            Size szRequired = CurrentSheet.GridRectangle.Size;
            if (iWidthSquares > 0)
                szRequired.Width = iWidthSquares;
            if (iHeightSquares > 0)
                szRequired.Height = iHeightSquares;
            szRequired.Width *= iWidth;
            szRequired.Height *= iHeight;
            Size szExtra = new Size(Width - panelMain.Width + SystemInformation.VerticalScrollBarWidth/2, Height - panelMain.Height + SystemInformation.HorizontalScrollBarHeight/2);
            Height = szExtra.Height + szRequired.Height;
            Width = szExtra.Width + szRequired.Width;
        }

        private void MenuFileNew()
        {
            if (!CheckMapsUnlocked())
                return;

            if (m_bUnsaved)
            {
                switch (MessageBox.Show("Do you want to save changes to " + DisplayFileName() + "?", "New Map", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
                {
                    case DialogResult.Yes:
                        if (!SaveCurrentFile())
                            return;
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                        return;
                    default:
                        break;
                }
            }

            m_strCurrentFile = "";
            m_mapBook = Global.CreateNewMapBook();
            SelectSheet(0);
            m_bMapMenuDirty = true;
            SetUnsaved(false, false);
        }

        private void LoadRecentMap(int index)
        {
            if (Properties.Settings.Default.MRUList == null || Properties.Settings.Default.MRUList.Paths == null)
                return;

            if (index < Properties.Settings.Default.MRUList.Paths.Count)
            {
                LoadFile(Properties.Settings.Default.MRUList.Paths[index]);
            }
        }

        private void MenuFileOpen()
        {
            if (!CheckMapsUnlocked())
                return;

            if (m_bUnsaved && MessageBox.Show("Discard changes to current file?", "Open New File", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != System.Windows.Forms.DialogResult.OK)
                return;

            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadFileWithoutConfirmation(openFileDialog.FileName);
            }
        }

        private void MenuFileSave()
        {
            SaveCurrentFile();
        }

        private void MenuFileExit()
        {
            Close();
        }

        private void miFileAbout_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialog();
        }

        private void SetShowInTaskbar()
        {
            foreach (Form window in Forms.Values)
            {
                if (this != window)
                    SetTaskbarWindow(window);
            }
        }

        public bool InOptions { get { return m_bShowingOptions; } }

        private bool RunWizard(int iStartPage = 0)
        {
            bool bLaunched = false;
            WizardForm form = new WizardForm(iStartPage);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.LaunchGame != GameNames.None)
                {
                    Properties.Settings.Default.Game = form.LaunchGame;
                    AskLoadGameMap();
                    bLaunched = LaunchCurrentGame(true);
                }
                if (Global.FormVisible(m_formEncounters))
                    m_formEncounters.ForceNextUpdate();
                ForceRefreshOnDisplay();
            }
            Properties.Settings.Default.WizardRun = true;
            return bLaunched;
        }

        private void AskLoadGameMap()
        {
            SelectSheet(0);
            if (!m_bUnsaved || MessageBox.Show(String.Format(
                "Would you like to discard the changes to the current file and load the default map for \"{0}\"?",
                Games.Name(Properties.Settings.Default.Game)), "Discard unsaved changes?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                LoadFileWithoutConfirmation(AutoLoadFile);
        }


        private void MenuViewOptions(int iTab = 4)
        {
            m_bShowingOptions = true;
            m_timerWatchdog.Stop();

            if (m_hacker != null)
            {
                m_hacker.Stop();
                m_hacker.Dispose();
                m_hacker = null;
            }

            m_bFocusingWindows = true;

            BlockMode modeSave = Mode;

            OptionsForm form = new OptionsForm();
            form.StartPage = iTab;
            form.SetMain(this, WindowType.Options);

            bool bOldHide = Properties.Settings.Default.HideUnvisitedSquares;
            bool bOldShowUnvisitedNotes = Properties.Settings.Default.ShowUnvisitedNotes;
            bool bOldMargins = Properties.Settings.Default.AlwaysRevealEdges;
            bool bOldInaccessible = Properties.Settings.Default.RevealAdjacentInaccessible;
            bool bOldMonsters = Properties.Settings.Default.ShowMonstersOnMaps;
            bool bOldNearbyMonsters = Properties.Settings.Default.ShowOnlyDetectableMonsters;
            bool bOldCartography = Properties.Settings.Default.UseInGameCartography;
            bool bOldShowListMonstersUnexplored = Properties.Settings.Default.ShowListMonstersUnexplored;
            bool bOldShowActive = Properties.Settings.Default.ShowActiveSquares;
            bool bOldShowActiveMonstersOnly = Properties.Settings.Default.ShowActiveEncountersOnly;
            bool bOldShowGridLines = Properties.Settings.Default.ShowGridLines;
            bool bOldHideDots = Properties.Settings.Default.HideUnvisitedDottedLines;
            bool bOldShowLabels = Properties.Settings.Default.ShowMapLabels;
            bool bOldSeen = Properties.Settings.Default.RevealSeenSquares;
            int iOldSeenOpacity = Properties.Settings.Default.SeenSquareOpacity;
            int iOldShowGridLinesAboveZoom = Properties.Settings.Default.ShowGridLinesAboveZoom;
            int iOldUnvisitedOpacity = Properties.Settings.Default.UnvisitedSquareOpacity;
            int iOldMonsterOpacity = Properties.Settings.Default.MonsterOpacity;
            int iOldUndo = Properties.Settings.Default.MaxUndoActions;
            GameNames oldGame = Properties.Settings.Default.Game;

            if (KeyboardHook.IsActive())
                KeyboardHook.Stop();

            if (form.ShowDialog() == DialogResult.OK)
            {
                OptionsChanged?.Invoke(this, new EventArgs());
            }

            m_shortcuts = Properties.Settings.Default.Shortcuts;

            if (!Properties.Settings.Default.WizardRun)
            {
                CheckShortcuts();
                RunWizard();
            }

            m_bFocusingWindows = false;
            SetShowInTaskbar();

            if (form.LoadNewMap || oldGame != Properties.Settings.Default.Game)
                AskLoadGameMap();

            CreateHacker(Properties.Settings.Default.Game, false, true);

            if (Properties.Settings.Default.EnableGlobalShortcuts)
                KeyboardHook.Start(this, m_shortcuts.KeysWanted);

            tseTitle.ReadOnly = Properties.Settings.Default.ReadOnlyMaps;

            if (form.SetKeys)
                SetMenuShortcuts();

            if (iOldUndo != Properties.Settings.Default.MaxUndoActions)
            {
                foreach (MapSheet sheet in m_mapBook.Sheets)
                    sheet.ResetUndo();
            }

            if (form.ResetAllVisited)
            {
                foreach (MapSheet sheet in m_mapBook.Sheets)
                    sheet.ResetVisited();
                SetDirtyUnsaved();
            }
            else if (
                bOldHide != Properties.Settings.Default.HideUnvisitedSquares ||
                bOldShowUnvisitedNotes != Properties.Settings.Default.ShowUnvisitedNotes ||
                bOldMargins != Properties.Settings.Default.AlwaysRevealEdges ||
                bOldInaccessible != Properties.Settings.Default.RevealAdjacentInaccessible ||
                bOldMonsters != Properties.Settings.Default.ShowMonstersOnMaps ||
                bOldNearbyMonsters != Properties.Settings.Default.ShowOnlyDetectableMonsters ||
                iOldUnvisitedOpacity != Properties.Settings.Default.UnvisitedSquareOpacity ||
                bOldCartography != Properties.Settings.Default.UseInGameCartography ||
                bOldShowActive != Properties.Settings.Default.ShowActiveSquares ||
                bOldShowActiveMonstersOnly != Properties.Settings.Default.ShowActiveEncountersOnly ||
                bOldShowListMonstersUnexplored != Properties.Settings.Default.ShowListMonstersUnexplored ||
                iOldMonsterOpacity != Properties.Settings.Default.MonsterOpacity ||
                bOldShowGridLines != Properties.Settings.Default.ShowGridLines ||
                iOldShowGridLinesAboveZoom != Properties.Settings.Default.ShowGridLinesAboveZoom ||
                bOldHideDots != Properties.Settings.Default.HideUnvisitedDottedLines ||
                bOldShowLabels != Properties.Settings.Default.ShowMapLabels ||
                bOldSeen != Properties.Settings.Default.RevealSeenSquares ||
                iOldSeenOpacity != Properties.Settings.Default.SeenSquareOpacity ||
                form.SquareStylesChanged
                )
            {
                foreach (MapSheet sheet in m_mapBook.Sheets)
                    sheet.ForceRefreshOnDisplay = true;
                if (Global.FormVisible(m_formEncounters))
                    m_formEncounters.UpdateUI();
                m_bNeedCartUpdate = true;
            }

            if (Global.FormVisible(m_formParty))
            {
                m_formParty.UpdateUI();
            }

            ForceRefreshOnDisplay();

            if (Properties.Settings.Default.BitmapCacheCrop < Global.BitmapCache.CropSize)
                Global.BitmapCache.CheckSize();

            if (form.ResetWindows)
                ResetWindowSizes();

            UpdateGameMenu();

            CheckShowScrollbars();

            CurrentSheet.SetYouAreHereDirty();
            SetDirty();

            if (bOldCartography != Properties.Settings.Default.UseInGameCartography && !bOldCartography)
                m_mapBook.UpdateVisited(CurrentSheet, m_hacker.GetCartography(), Hacker.CartographyCanUnvisitSquares);

            m_bShowingOptions = false;
            m_timerWatchdog.Interval = Properties.Settings.Default.WatchdogTimer;
            m_timerWatchdog.Start();

            SetMode(modeSave);
        }

        private bool CheckCorrectBookForGame()
        {
            if (m_bAskingCorrectBook)
                return true;

            if (!Properties.Settings.Default.OfferToSwitchBooks)
                return true;

            if (m_strCurrentFile != null && !m_strCurrentFile.StartsWith(":") && m_strCurrentFile != Global.InternalMapString)
                return true; // User loaded a specific file; don't offer to load an internal map instead

            GameNames gameCurrentlyRunning = MemoryHacker.FindKnownGame();

            if (gameCurrentlyRunning != Properties.Settings.Default.Game && gameCurrentlyRunning != GameNames.None)
            {
                m_bAskingCorrectBook = true;
                if (MessageBox.Show(String.Format("The currently loaded map book is for \"{0}\", but \"{1}\" seems to be currently running.  Would you like to load the internal map book for that game instead?",
                    Games.Name(Properties.Settings.Default.Game), Games.Name(gameCurrentlyRunning)),
                    "Different game running", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Properties.Settings.Default.Game = gameCurrentlyRunning;
                    LoadFileWithoutConfirmation(Games.MapForGame(gameCurrentlyRunning));
                    CreateHacker(Properties.Settings.Default.Game, false);
                    m_bAskingCorrectBook = false;
                    return false;
                }
                m_bAskingCorrectBook = false;
            }

            return true;
        }

        private void MenuEditCut()
        {
            MenuEditCopy();
            MenuEditDelete();
        }

        private void MenuEditCopy()
        {
            if (m_rcSelection == Rectangle.Empty)
                return;
            CurrentSheet.CopyToClipboard(m_rcSelection);
        }

        private void MenuEditPaste()
        {
            if (m_rcSelection != Rectangle.Empty)
                m_ptContextOpened = m_rcSelection.Location;
            else
                m_ptContextOpened = Point.Empty;

            Paste();
        }

        private void Paste()
        {
            if (Clipboard.ContainsData(typeof(MapNote).FullName))
                PasteNote();
            else if (Clipboard.ContainsData(typeof(MapSquareArray).FullName))
                PasteBlocks();
        }

        private void MenuEditDelete()
        {
            if (m_rcSelection != Rectangle.Empty)
            {
                CurrentSheet.ClearRedo();
                m_undoBlocks = CurrentSheet.StartUndoBlock().Squares;
                CurrentSheet.DeleteBlocks(m_undoBlocks, m_rcSelection);
                SetDirty(m_rcSelection, true);
                CancelSelection();
                SetDirtyUnsaved();
                return;
            }

            if (Global.FormVisible(m_formEditLabels))
                m_formEditLabels.DeleteSelectedItems();
        }

        private void MenuModeBlock()
        {
            SetMode(BlockMode.Block);
        }

        private void MenuModeFill()
        {
            SetMode(BlockMode.Fill);
        }

        private void MenuModeLine()
        {
            SetMode(BlockMode.Line);
        }

        private void MenuModeHybrid()
        {
            SetMode(BlockMode.Hybrid);
        }

        private void MenuModeEdit()
        {
            SetMode(BlockMode.Edit);
        }

        private void editToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            UpdateEditMenu();
        }

        private void miSheetSwitchTo_DropDownOpening(object sender, EventArgs e)
        {
        }

        public void AddNoteToMap(Point pt, string strSymbol, string strText)
        {
            CurrentSheet.ClearRedo();
            CurrentSheet.AddUndoNote(pt);

            MapNote note = CurrentSheet.NoteAtPoint(pt);
            if (note == null)
            {
                note = new MapNote();
                note.Location = pt;
            }
            note.Symbol = strSymbol;
            note.Text = strText;
            note.Color = pbSelectColor.BackColor;
            CurrentSheet.SetNote(note);
            SetDirtyUnsaved();
        }

        void miGridAddNoteTemplate_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripItem))
                return;
            NoteTemplateTag tag = ((ToolStripItem)sender).Tag as NoteTemplateTag;
            CurrentSheet.Cursor = m_ptContextOpened;
            UpdateLocation(m_ptContextOpened);

            AddNoteToMap(m_ptContextOpened, tag.Symbol, Global.EnsureCR(tag.FinalText));
        }

        void menuMapsItem_Click(object sender, EventArgs e)
        {
            if (!(sender is ToolStripItem))
                return;
            int iIndex = ((SheetTag)((ToolStripItem)sender).Tag).Index;
            if (iIndex < 0 || iIndex >= m_mapBook.Sheets.Count)
                return;
            if (m_mapBook.Sheets[iIndex].IsLegend)
                ToggleLegend(iIndex);
            else
                SelectSheet(iIndex);
        }

        void ToggleLegend(int iIndex = -1)
        {
            if (iIndex == -1)
            {
                for(int i = 0; i < m_mapBook.Sheets.Count; i++)
                    if (m_mapBook.Sheets[i].IsLegend)
                        iIndex = i;
            }

            if (iIndex == -1)
                return;

            if (CurrentSheet.IsLegend && m_iLegendHoldIndex != -1)
                iIndex = m_iLegendHoldIndex;
            else
                m_iLegendHoldIndex = m_iCurrentSheet;
            SelectSheet(iIndex);
        }

        private void ShowReadOnlyMapMessage()
        {
            m_tipReadOnly.SetToolTip(pbMain, "Maps are currently set to read-only.  This can be changed in the Options dialog.");
            m_tipReadOnly.InitialDelay = 0;
            m_tipReadOnly.ShowAlways = true;
            m_tipReadOnly.Show(m_tipReadOnly.ToolTipTitle, this);
            m_timerHideTips.Stop();
            m_timerHideTips.Interval = 4000;
            m_timerHideTips.Start();
        }

        private void ShowReadOnlyNotesMessage()
        {
            if (NativeMethods.ApplicationIsActivated())
                MessageBox.Show("Notes are currently set to read-only.  This can be changed in the Options dialog.", "Read-only!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MenuSheetAdd()
        {
            AddNewSheet();
        }

        private void MenuSheetRemove()
        {
            if (!CheckMapsUnlocked())
                return;

            if (Properties.Settings.Default.ReadOnlyMaps)
            {
                ShowReadOnlyMapMessage();
                return;
            }

            if (m_mapBook.Sheets.Count < 2)
            {
                MessageBox.Show("Cannot remove the only sheet from the map book.", "Delete Sheet", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            switch (MessageBox.Show("Remove this sheet from the map book?", "Delete Sheet", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation))
            {
                case DialogResult.Yes:
                    m_mapBook.Sheets.Remove(CurrentSheet);
                    if (m_iCurrentSheet >= m_mapBook.Sheets.Count)
                        m_iCurrentSheet--;
                    UpdateTitle();
                    SetDirtyUnsaved();
                    m_bMapMenuDirty = true;
                    break;
                default:
                    break;
            }
        }

        private void menuSheet_DropDownOpening(object sender, EventArgs e)
        {
            UpdateSheetMenu();
        }

        private void MenuViewColors()
        {
            ColorsForm form = new ColorsForm();
            form.Colors = Properties.Settings.Default.DrawColors;
            form.Elements = Properties.Settings.Default.UIElementOptions;
            if (form.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.UIElementOptions = form.Elements;
                if (Global.FormVisible(m_formQuests))
                    m_formQuests.ForceRefresh();
                if (Global.FormVisible(m_formParty))
                    m_formParty.ForceUpdate();
            }
        }

        private void MenuModeNotes()
        {
            SetMode(BlockMode.Notes);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);

            switch (NativeMethods.GetModifiers())
            {
                case Keys.None:
                    if (e.Delta > 0)
                        PerformAction(Properties.Settings.Default.ActionWheelUp);
                    else if (e.Delta < 0)
                        PerformAction(Properties.Settings.Default.ActionWheelDown);
                    break;
                case Keys.Control:
                    if (e.Delta > 0)
                        PerformAction(Properties.Settings.Default.ActionCtrlWheelUp);
                    else if (e.Delta < 0)
                        PerformAction(Properties.Settings.Default.ActionCtrlWheelDown);
                    break;
                case Keys.Control | Keys.Shift:
                    if (e.Delta > 0)
                        PerformAction(Properties.Settings.Default.ActionCtrlShiftWheelUp);
                    else if (e.Delta < 0)
                        PerformAction(Properties.Settings.Default.ActionCtrlShiftWheelDown);
                    break;
                case Keys.Control | Keys.Alt:
                    if (e.Delta > 0)
                        PerformAction(Properties.Settings.Default.ActionCtrlAltWheelUp);
                    else if (e.Delta < 0)
                        PerformAction(Properties.Settings.Default.ActionCtrlAltWheelDown);
                    break;
                case Keys.Shift | Keys.Alt:
                    if (e.Delta > 0)
                        PerformAction(Properties.Settings.Default.ActionShiftAltWheelUp);
                    else if (e.Delta < 0)
                        PerformAction(Properties.Settings.Default.ActionShiftAltWheelDown);
                    break;
                case Keys.Alt:
                    if (e.Delta > 0)
                        PerformAction(Properties.Settings.Default.ActionAltWheelUp);
                    else if (e.Delta < 0)
                        PerformAction(Properties.Settings.Default.ActionAltWheelDown);
                    break;
                case Keys.Shift:
                    if (e.Delta > 0)
                        PerformAction(Properties.Settings.Default.ActionShiftWheelUp);
                    else if (e.Delta < 0)
                        PerformAction(Properties.Settings.Default.ActionShiftWheelDown);
                    break;
                default:
                    break;
            }

        }

        private void pbMain_MouseDown(object sender, MouseEventArgs e)
        {
            m_ctrlLastFocus = FocusedTextBox?.Box;
            panelMain.Focus();
            switch (e.Button)
            {
                case System.Windows.Forms.MouseButtons.Left:
                    PerformAction(Properties.Settings.Default.ActionLeftMouse, e.Button);
                    break;
                case System.Windows.Forms.MouseButtons.Right:
                    PerformAction(Properties.Settings.Default.ActionRightMouse, e.Button);
                    break;
                case System.Windows.Forms.MouseButtons.Middle:
                    PerformAction(Properties.Settings.Default.ActionMiddleMouse, e.Button);
                    break;
                case System.Windows.Forms.MouseButtons.XButton1:
                    PerformAction(Properties.Settings.Default.ActionX1Mouse, e.Button);
                    break;
                case System.Windows.Forms.MouseButtons.XButton2:
                    PerformAction(Properties.Settings.Default.ActionX2Mouse, e.Button);
                    break;
            }
        }

        private void CheckCurrentIcon() { CheckCurrentIcon(GetSquareLocationAtMouse()); }

        private void CheckCurrentIcon(Point ptLocation)
        {
            if (m_iconsForm != null && !m_iconsForm.IsDisposed)
                if (CurrentSheet.SetCurrentIcon(m_iconsForm.SelectedIcon, ptLocation))
                    SetDirty();
        }

        private void pbMain_MouseMove(object sender, MouseEventArgs e)
        {
            // Don't process these too often; CPU usage can get a little high
            m_lastMouseMoveArgs = e;
        }

        public BlockMode VirtualMode
        {
            get
            {
                if (m_bEditingLiveSquares)
                    return BlockMode.Live;

                if (Mode == BlockMode.Edit)
                    return Mode;    // Drawing things while in edit mode does not work exactly as expected
                switch (m_actionDraw)
                {
                    case Action.MoveLabels: return BlockMode.Notes;
                    case Action.DrawBlocks: return BlockMode.Block;
                    case Action.DrawLines: return BlockMode.Line;
                    case Action.DrawHybrid: return BlockMode.Hybrid;
                    case Action.DrawFill: return BlockMode.Fill;
                    case Action.DrawEdit: return BlockMode.Edit;
                    default: return Mode;
                }
            }
        }

        private void OnTimedMouseMove(MouseEventArgs e)
        {
            Point ptLocation = GetSquareLocationAtMouse();
            StringBuilder sbSquareTip = new StringBuilder();
            if (m_lastMonsterLocations != null)
            {
                Point ptSquare = TranslateToGameMap(ptLocation);
                bool bShowTip = (m_lastMonsterLocations.MonsterPositions.ContainsKey(ptSquare) && ShowingCurrentMap) &&
                    Properties.Settings.Default.ShowMonstersOnMaps &&
                    (!Properties.Settings.Default.ShowOnlyDetectableMonsters || new Proximity(ptSquare, TranslateToGameMap(m_lastLocation.PrimaryCoordinates)).Simple < 4) &&
                    (!Properties.Settings.Default.HideUnvisitedSquares || SquareIsVisible(ptLocation));
                if (bShowTip)
                {
                    if (!Global.ShowOnlyDetectableMonsters || new Proximity(ptSquare, TranslateToGameMap(m_lastLocation.PrimaryCoordinates)).Simple < 4)
                    {
                        MonsterPosition pos = m_lastMonsterLocations.MonsterPositions[ptSquare];
                        if (HideMonsters(pos.Monsters) || (!Properties.Settings.Default.ShowDeadMonsters && pos.Monsters.All(m => m.Killed)))
                            bShowTip = false;
                        else if (m_lastMonsterTip != pos)
                            m_lastMonsterTip = pos;
                        sbSquareTip.Append(pos.TipString);
                    }
                    else
                        bShowTip = false;
                }
                if (!bShowTip)
                    m_lastMonsterTip = null;
            }

            if (m_lastItemLocations != null)
            {
                Point ptSquare = TranslateToGameMap(ptLocation);
                bool bShowTip = (m_lastItemLocations.ItemPositions.ContainsKey(ptSquare) && ShowingCurrentMap) &&
                    Properties.Settings.Default.ShowItemIcons &&
                    (!Properties.Settings.Default.HideUnvisitedSquares || SquareIsVisible(ptLocation));
                if (bShowTip)
                {
                    ItemPosition pos = m_lastItemLocations.ItemPositions[ptSquare];
                    if (m_lastItemTip != pos)
                        m_lastItemTip = pos;
                    sbSquareTip.Append(pos.TipString);
                }
                if (!bShowTip)
                    m_lastItemTip = null;
            }
            if (sbSquareTip.Length > 0)
            {
                if (m_tipGrid.GetToolTip(pbMain) != sbSquareTip.ToString())
                {
                    m_tipGrid.AutoPopDelay = 32000;
                    m_tipGrid.InitialDelay = 500;
                    m_tipGrid.ShowAlways = true;
                    m_tipGrid.SetToolTip(pbMain, sbSquareTip.ToString());
                }
            }
            else
                m_tipGrid.RemoveAll();

            if (EditingNote)
            {
                // Currently editing a note; don't do anything until user is finished with that
                // unless we are in Notes Mode, in which case cancel the edit and enter note-moving mode instead
                if (!pbMain.Capture || VirtualMode != BlockMode.Notes)
                    return;

                if (ptLocation == m_noteCurrent.Location)
                    return;

                FinishNote();
            }

            if (Mode != BlockMode.Keyboard)
            {
                // In keyboard mode, the cursor takes precedence over the cursor location
                SetNoteText(ptLocation, Mode == BlockMode.Edit);
                UpdateLocation(ptLocation);
            }

            if (!pbMain.Capture)
            {
                CheckCurrentIcon(ptLocation);
                return;
            }

            // This is to keep the panel from scrolling back to (0,0) for some unknown reason
            if (panelMain.LastManualScroll != Global.NullPoint)
            {
                panelMain.AutoScrollPosition = panelMain.LastManualScroll;
                panelMain.LastManualScroll = Global.NullPoint;
            }

            switch (m_captureMode)
            {
                case CaptureMode.Scroll:
                    if (m_ptCaptureMove != Point.Empty)
                    {
                        int iHoriz = 0;
                        int iVert = 0;

                        if (Properties.Settings.Default.ScrollStyle == ScrollStyle.LockScroll)
                        {
                            double fScaleH = (double)(panelMain.HorizontalScroll.Maximum - panelMain.HorizontalScroll.Minimum) / (double)panelMain.Width;
                            double fScaleV = (double)(panelMain.VerticalScroll.Maximum - panelMain.VerticalScroll.Minimum) / (double)panelMain.Height;

                            iHoriz = m_ptCaptureScrollbars.X + (int)((Cursor.Position.X - m_ptCaptureMove.X) * fScaleH);
                            iVert = m_ptCaptureScrollbars.Y + (int)((Cursor.Position.Y - m_ptCaptureMove.Y) * fScaleV);
                        }
                        else
                        {
                            iHoriz = m_ptCaptureScrollbars.X - (Cursor.Position.X - m_ptCaptureMove.X);
                            iVert = m_ptCaptureScrollbars.Y - (Cursor.Position.Y - m_ptCaptureMove.Y);
                        }

                        Global.FixRange(ref iHoriz, panelMain.HorizontalScroll.Minimum, panelMain.HorizontalScroll.Maximum);
                        Global.FixRange(ref iVert, panelMain.VerticalScroll.Minimum, panelMain.VerticalScroll.Maximum);

                        panelMain.LastManualScroll = new Point(iHoriz, iVert);
                        panelMain.AutoScrollPosition = panelMain.LastManualScroll;
                    }
                    break;
                case CaptureMode.Draw:
                    switch (VirtualMode)
                    {
                        case BlockMode.Block:
                            DrawBlocksToMouse(m_undoBlocks, DrawMode.None);
                            break;
                        case BlockMode.Line:
                            DrawEdgeToMouse(m_undoBlocks);
                            break;
                        case BlockMode.Hybrid:
                            DrawBlocksToMouse(m_undoBlocks, DrawMode.Fill);
                            break;
                        case BlockMode.Notes:
                            // Move labels around the map
                            Point ptNew = pbMain.PointToClient(Cursor.Position);
                            if (m_bConstrainMove)
                                ptNew = Global.ConstrainMove(ptNew, m_ptCursorAtMouseDown);
                            if (ptNew != m_ptCursorAtContext)
                                CheckLabelCopyFeedback(ptNew);
                            break;
                        case BlockMode.Edit:
                            switch (m_selectionAction)
                            {
                                case SelectionActions.Create:
                                case SelectionActions.Copy:
                                case SelectionActions.Move:
                                    m_ptNextSquare = GetSquareLocationAtMouse();
                                    if (m_bConstrainMove)
                                        m_ptNextSquare = Global.ConstrainMove(m_ptNextSquare, m_ptOriginalCapture);
                                    if (m_ptLastSquare != m_ptNextSquare)
                                        SetDirty();
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
            }
        }

        private void CheckLabelCopyFeedback(Point ptNew)
        {
            if (m_labelAtCursor == null)
            {
                if (CurrentSheet.HasNote(m_ptOriginalCapture))
                {
                    MapNote note = CurrentSheet.Grid[m_ptOriginalCapture.X, m_ptOriginalCapture.Y].Note;
                    bool bMoving = note.Moving;
                    if ((m_selectionAction == SelectionActions.Move && !bMoving) || (m_selectionAction == SelectionActions.Copy && bMoving))
                    {
                        note.Moving = !bMoving;
                        CurrentSheet.SetDirty(m_ptOriginalCapture.X, m_ptOriginalCapture.Y);
                        SetDirty();
                    }
                    Point ptNewGrid = new Point(ptNew.X / CurrentSheet.SquareSize.Width, ptNew.Y / CurrentSheet.SquareSize.Height);
                    MapNote noteCopy = note.Clone(ptNewGrid);
                    noteCopy.Moving = false;
                    if (CurrentSheet.SetCurrentNote(noteCopy, ptNewGrid))
                        SetDirty();
                }
                return;
            }
            CurrentSheet.SetLabelSquaresDirty(m_labelAtCursor);
            m_labelAtCursor = CurrentSheet.Labels.Move(m_labelAtCursor,
                (ptNew.X - m_ptCursorAtContext.X) / (float)CurrentSheet.SquareSize.Width,
                (ptNew.Y - m_ptCursorAtContext.Y) / (float)CurrentSheet.SquareSize.Height);
            CurrentSheet.SetLabelSquaresDirty(m_labelAtCursor);

            if (CurrentSheet == null || m_labelBeforeMove == null)
                return;
            if (m_selectionAction == SelectionActions.Copy && !CurrentSheet.Labels.ContainsKey(m_labelBeforeMove.Location))
            {
                CurrentSheet.Labels.Add(m_labelBeforeMove);
                CurrentSheet.SetLabelSquaresDirty(m_labelBeforeMove);
            }
            else if (m_selectionAction == SelectionActions.Move && CurrentSheet.Labels.ContainsKey(m_labelBeforeMove.Location))
            {
                if (CurrentSheet.Labels[m_labelBeforeMove.Location] == m_labelBeforeMove)
                {
                    CurrentSheet.Labels.Remove(m_labelBeforeMove);
                    CurrentSheet.SetLabelSquaresDirty(m_labelBeforeMove);
                }
            }

            m_ptCursorAtContext = ptNew;
            m_bLabelMoved = true;
            SetDirtyUnsaved();
        }

        private Bitmap GetSelectionBitmap(GameNames game, Rectangle rcSelection, EditFlags flags)
        {
            rcSelection.Inflate(1,1);
            flags.OuterDepth = 1;
            Bitmap bmp = CurrentSheet.GetBlocksBitmap(game, rcSelection, flags);
            if (bmp == null)
                return null;

            Size szMargin = CurrentSheet.SelectionMargin;
            int iCropWidth = CurrentSheet.SquareSize.Width - szMargin.Width;
            int iCropHeight = CurrentSheet.SquareSize.Height - szMargin.Height;
            int iBuffer = CurrentSheet.CurrentZoom / 100;
            Bitmap bmpNew = new Bitmap(Math.Max(1, bmp.Width - (iCropWidth*2) - iBuffer), Math.Max(1, bmp.Height - (iCropHeight*2) - iBuffer));
            using (Graphics g = Graphics.FromImage(bmpNew))
            {
                g.DrawImage(bmp, new Rectangle(0, 0, bmpNew.Width, bmpNew.Height), new Rectangle(iCropWidth, iCropHeight, bmpNew.Width, bmpNew.Height), GraphicsUnit.Pixel);
            }
            return bmpNew;
        }

        private void RecaptureSelection()
        {
            if (!HasSelection)
                return;
            if (m_bmpCopyBlocksGhost != null)
                m_bmpCopyBlocksGhost.Dispose();
            m_bmpCopyBlocksGhost = GetSelectionBitmap(Game, m_rcSelection, EditFlags.All);
            EditFlags flags = Global.EditSettings;
            flags.Grid = false;
            flags.AlwaysUseAlpha = true;
            if (m_bmpCopyMoveBlocks != null)
                m_bmpCopyMoveBlocks.Dispose();
            m_bmpCopyMoveBlocks = GetSelectionBitmap(Game, m_rcSelection, flags);
            flags.Mask = true;
            flags.Grid = true;
            if (m_bmpMoveBlocksGhost != null)
                m_bmpMoveBlocksGhost.Dispose();
            if (m_bmpCopyBlocksGhost != null)
            {
                using (Bitmap bmpTemp = GetSelectionBitmap(Game, m_rcSelection, flags))
                {
                    if (bmpTemp != null)
                        m_bmpMoveBlocksGhost = Global.Mask(m_bmpCopyBlocksGhost, bmpTemp);
                }
            }
        }

        private void pbMain_MouseUp(object sender, MouseEventArgs e)
        {
            ForceMouseUp(e.Button);
        }

        private void ForceMouseUp(MouseButtons buttons)
        {
            if (CurrentSheet.HasNote(m_ptOriginalCapture))
                CurrentSheet.Grid[m_ptOriginalCapture.X, m_ptOriginalCapture.Y].Note.Moving = false;
            CurrentSheet.SetCurrentNote(null, Point.Empty);

            Point ptDest = GetSquareLocationAtMouse();
            if (m_bConstrainMove)
                ptDest = Global.ConstrainMove(ptDest, m_ptOriginalCapture);

            m_undoBlocks = null;

            bool bMovedSignificantly = false;
            try
            {
                if (buttons == m_btnCaptured)
                {
                    Capture = false;
                    m_captureMode = CaptureMode.None;
                    m_btnCaptured = System.Windows.Forms.MouseButtons.None;

                    switch (VirtualMode)
                    {
                        case BlockMode.Notes:
                            if (m_bLabelMoved)
                            {
                                if (m_selectionAction == SelectionActions.Copy)
                                {
                                    CurrentSheet.Labels.Add(m_labelBeforeMove);
                                    CurrentSheet.SetLabelSquaresDirty(m_labelBeforeMove);
                                    SetDirtyUnsaved();
                                }
                                if (m_labelAtCursor != null && Global.FormVisible(m_formEditLabels))
                                    m_formEditLabels.SetCurrentlySelectedLabel(m_labelAtCursor);
                            }
                            else if (buttons != MouseButtons.Right)    // Don't edit notes and show the context menu at the same time
                            {
                                if (m_labelAtCursor != null && Global.FormVisible(m_formEditLabels))
                                    m_formEditLabels.SetCurrentlySelectedLabel(m_labelAtCursor);
                                // Notes are just "click to edit" rather than "hold down the mouse to draw"
                                else if (EditingNote)
                                {
                                    m_selectionAction = SelectionActions.None;
                                    if (m_noteCurrent.Location == ptDest)
                                    {
                                        if (m_ctrlLastFocus == tbNote)
                                        {
                                            tbSymbol.Select();
                                            tbSymbol.SelectAll();
                                            return;
                                        }
                                        else
                                        {
                                            tbNote.Select();
                                            return;
                                        }
                                    }
                                    else
                                    {
                                        FinishNote();
                                        BeginEditNote(ptDest);
                                    }
                                }
                                else if (m_ptOriginalCapture == ptDest)
                                    BeginEditNote(ptDest);
                                else if (CurrentSheet.HasNote(m_ptOriginalCapture) && CurrentSheet.PointInGrid(ptDest))
                                {
                                    if (m_bCancelNoteMove)
                                    {
                                        m_bCancelNoteMove = false;
                                        CurrentSheet.SetCurrentNote(null, Point.Empty);
                                        CurrentSheet.SetDirty(m_ptOriginalCapture.X, m_ptOriginalCapture.Y);
                                        SetDirty();
                                    }
                                    else
                                    {
                                        CurrentSheet.ClearRedo();
                                        if (m_selectionAction == SelectionActions.Copy)
                                        {
                                            CurrentSheet.AddUndoNote(ptDest);
                                            CurrentSheet.Grid[ptDest.X, ptDest.Y].CopyNoteFrom(CurrentSheet.Grid[m_ptOriginalCapture.X, m_ptOriginalCapture.Y], ptDest, true);
                                        }
                                        else
                                        {
                                            CurrentSheet.AddUndoNotes();
                                            CurrentSheet.Grid[ptDest.X, ptDest.Y].Note = CurrentSheet.Grid[m_ptOriginalCapture.X, m_ptOriginalCapture.Y].Note;
                                            CurrentSheet.Grid[m_ptOriginalCapture.X, m_ptOriginalCapture.Y].Note = null;
                                            CurrentSheet.SetDirty(m_ptOriginalCapture, DirtyType.Back);
                                        }
                                        SetDirtyUnsaved();
                                    }
                                }
                                else
                                    BeginEditNote(ptDest);
                            }
                            m_selectionAction = SelectionActions.None;
                            break;
                        case BlockMode.Edit:
                            int iDeltaX = ptDest.X - m_ptOriginalCapture.X;
                            int iDeltaY = ptDest.Y - m_ptOriginalCapture.Y;

                            ptDest = new Point(m_rcSelection.Left + iDeltaX, m_rcSelection.Top + iDeltaY);

                            switch (m_selectionAction)
                            {
                                case SelectionActions.Move:
                                case SelectionActions.Copy:
                                    bool bMove = m_selectionAction == SelectionActions.Move;
                                    CurrentSheet.ClearRedo();
                                    m_undoBlocks = CurrentSheet.StartUndoBlock().Squares;
                                    if (bMove)
                                        CurrentSheet.MoveBlocks(m_undoBlocks, m_rcSelection, ptDest, Global.EditSettings);
                                    else
                                        CurrentSheet.CopyBlocks(m_undoBlocks, m_rcSelection, ptDest, Global.EditSettings);
                                    SetDirty(m_rcSelection, true);
                                    SetBorderDirty(ptDest, m_rcSelection.Size);
                                    if (bMove && !(m_rcSelection.Location == ptDest && !(m_rcSelection.Width == 1 && m_rcSelection.Height == 1)))
                                        CancelSelection();
                                    else
                                    {
                                        m_rcSelection = new Rectangle(ptDest, m_rcSelection.Size);
                                        m_ptLastSquare = Global.NullPoint;
                                        m_bRecaptureSelection = true;
                                    }
                                    SetDirtyUnsaved();
                                    break;
                                default:
                                    RecaptureSelection();
                                    break;
                            }
                            m_selectionAction = SelectionActions.None;
                            break;
                    }

                    if (Math.Abs(m_ptCaptureMove.X - Cursor.Position.X) > 3 || Math.Abs(m_ptCaptureMove.Y - Cursor.Position.Y) > 3)
                        bMovedSignificantly = true;
                    m_ptCaptureMove = Point.Empty;
                    pbMain.Cursor = CursorForMode(Mode);
                }

                if (buttons == m_btnCaptured || Mode == BlockMode.Keyboard)
                {
                    CurrentSheet.Cursor = GetSquareLocationAtMouse();
                    SetNoteText(CurrentSheet.Cursor);
                }
                if (buttons == MouseButtons.Right && !bMovedSignificantly)
                {
                    if (pbMain.RectangleToScreen(pbMain.ClientRectangle).Contains(Cursor.Position))
                    {
                        m_ptCursorAtContext = pbMain.PointToClient(Cursor.Position);
                        m_ptContextOpened = GetSquareLocationAtMouse();
                        cmGrid.Show(Cursor.Position);
                    }
                }
            }
            finally
            {
                m_actionDraw = Action.None;
            }
        }

        private void SetDirty(Rectangle rc, bool bIncludeBorder = false)
        {
            if (rc == Rectangle.Empty)
            {
                ForceRefreshOnDisplay();
                return;
            }
            int iExtra = bIncludeBorder ? 1 : 0;
            for (int row = rc.Top - iExtra; row < rc.Bottom + iExtra; row++)
                for (int col = rc.Left - iExtra; col < rc.Right + iExtra; col++)
                    CurrentSheet.SetDirty(col, row);
            SetDirty();
        }

        private void SetBorderDirty(Point pt, Size sz)
        {
            Rectangle rc = new Rectangle(pt, sz);
            for (int row = rc.Top - 1; row <= rc.Bottom ; row++)
            {
                CurrentSheet.SetDirty(rc.Left - 1, row);
                CurrentSheet.SetDirty(rc.Right, row);
            }
            for (int col = rc.Left - 1; col <= rc.Right; col++)
            {
                CurrentSheet.SetDirty(col, rc.Top - 1);
                CurrentSheet.SetDirty(col, rc.Bottom);
            }
        }

        private void MenuModeKeyboard()
        {
            SetMode(BlockMode.Keyboard);
        }

        public Point CurrentNotePoint
        {
            get
            {
                if (m_ptCurrentNote == Global.NullPoint)
                    return CurrentSheet.Cursor;
                return m_ptCurrentNote;
            }
        }

        private void tbNote_Click(object sender, EventArgs e)
        {
            if (EditingNote)
                return;
            BeginEditNote(CurrentNotePoint);
        }

        private void miGridNote_Click(object sender, EventArgs e)
        {
            if (EditingNote)
                FinishNote();

            CurrentSheet.Cursor = m_ptContextOpened;
            BeginEditNote(m_ptContextOpened, tbNote);
        }

        private void tbSymbol_Click(object sender, EventArgs e)
        {
            if (EditingNote)
                return;
            BeginEditNote(CurrentSheet.Cursor, tbSymbol);
        }

        private void miGridDeleteNote_Click(object sender, EventArgs e)
        {
            if (EditingNote)
                FinishNote();
            CurrentSheet.Cursor = m_ptContextOpened;
            DeleteNote(CurrentSheet.NoteAtCursor);
        }

        private void cmGrid_Opening(object sender, CancelEventArgs e)
        {
            if (m_rcSelection != Rectangle.Empty)
            {
                e.Cancel = true;
                cmSelection.Show(Cursor.Position);
                return;
            }

            bool bHackerRunning = m_hacker != null && m_hacker.Running;
            bool bBeacon = bHackerRunning && m_hacker.HasBeacon;
            bool bSurface = bHackerRunning && m_hacker.HasSurfaceLocation;
            bool bScripts = bHackerRunning && m_hacker.HasScripts;
            bool bCart = bHackerRunning && m_hacker.HasCartography;

            bool bReadOnlyMaps = Properties.Settings.Default.ReadOnlyMaps;
            bool bReadOnlyNotes = Properties.Settings.Default.ReadOnlyNotes;
            bool bPlay = (Mode == BlockMode.Play);
            bool bCheat = Global.Cheats;

            bool bVisited = SquareIsVisible(m_ptContextOpened);
            bool bHasNote = CurrentSheet.HasNote(m_ptContextOpened);
            bool bCanEditNote = (bVisited || Properties.Settings.Default.ShowUnvisitedNotes) && !bReadOnlyNotes && !bPlay;
            bool bViewNoteOnly = bReadOnlyNotes || bPlay;
            miGridYouAreHere.Visible = false;   // This doesn't really serve much purpose any more
            miGridDeleteNote.Enabled = bHasNote && bCanEditNote && !bReadOnlyNotes && !bPlay;
            miGridDeleteNote.Visible = !bPlay;
            miGridSetBlockColor.Enabled = !bPlay;
            miGridSetBlockColor.Visible = miGridSetBlockColor.Enabled;
            miGridRemoveIcons.Enabled = CurrentSheet.HasIcons(m_ptContextOpened) && !bReadOnlyMaps && !bPlay;
            miGridRemoveIcons.Visible = !bPlay;
            miGridGoToLinkedSheet.Enabled = bHasNote && (bCanEditNote || bViewNoteOnly) && CurrentSheet.NoteAtPoint(m_ptContextOpened).Text.Contains("{map:");
            miGridCopyNote.Enabled = bHasNote && bCanEditNote && !bPlay;
            miGridCopyNote.Visible = miGridCopyNote.Enabled;
            miGridHackTeleport.Visible = bCheat;
            miGridHackTeleport.Enabled = bCheat && bHackerRunning;
            miGridAddNote.Enabled = !bReadOnlyNotes && !bPlay;
            miGridAddNote.Visible = miGridAddNote.Enabled;
            miGridNote.Text = bViewNoteOnly ? "Vi&ew note" : "&Edit note";
            miGridNote.Enabled = bCanEditNote || bViewNoteOnly;
            miGridNote.Visible = bCanEditNote || bHasNote;
            miGridHackBeacon.Visible = bCheat && m_hacker != null && m_hacker.HasBeacon;
            miGridHackBeacon.Enabled = bCheat && bBeacon;
            miGridHackSurface.Visible = bCheat && m_hacker != null && m_hacker.HasSurfaceLocation;
            miGridHackSurface.Enabled = bCheat && bSurface;
            miGridViewScripts.Visible = ShowingCurrentMap && bScripts && (!bPlay || bCheat);
            miGridToggleVisited.Enabled = bCheat || !bHackerRunning || !Properties.Settings.Default.UpdateCartWhenInaccessibleRevealed;
            miGridToggleVisited.Visible = miGridToggleVisited.Enabled;

            miGridDebugReinterpret.Visible = Global.Debug;
            miGridDebugCartography.Visible = Global.Debug && bCart;
            miGridDebugToggleLive.Visible = Global.Debug;
            if (Global.Debug)
            {
                MapSquare square = GetSquareAtMouse();
                if (square == null)
                    miGridDebugToggleLive.Visible = false;
                else if (square.Live)
                    miGridDebugToggleLive.Text = "Debug: Make passive s&quare";
                else
                    miGridDebugToggleLive.Text = "Debug: Make live s&quare";
            }

            m_labelAtCursor = CurrentSheet.LabelInPoint(pbMain.PointToClient(Cursor.Position));
            if (CurrentSheet.Labels != null && CurrentSheet.Labels.Count > 0 && m_labelAtCursor != null)
                miGridAddLabel.Text = "Edit label";
            else
                miGridAddLabel.Text = "Add label";
            miGridAddLabel.Enabled = !bPlay;
            miGridAddLabel.Visible = miGridAddLabel.Enabled;

            UpdatePasteMenu(miGridPaste);
        }

        private void UpdatePasteMenu(ToolStripMenuItem paste)
        {
            if (Mode == BlockMode.Play)
            {
                paste.Visible = false;
                paste.Enabled = false;
                return;
            }

            paste.Visible = true;
            if (Clipboard.ContainsData(typeof(MapNote).FullName))
            {
                paste.Enabled = !Properties.Settings.Default.ReadOnlyNotes;
                paste.Text = "&Paste note";
            }
            else if (Clipboard.ContainsData(typeof(MapSquareArray).FullName))
            {
                paste.Enabled = !Properties.Settings.Default.ReadOnlyMaps;
                paste.Text = "&Paste blocks/labels";
            }
            else
            {
                paste.Enabled = false;
                paste.Text = "&Paste";
            }

        }

        private void MenuSheetExpand()
        {
            if (Properties.Settings.Default.ReadOnlyMaps)
            {
                ShowReadOnlyMapMessage();
                return;
            }

            SheetExpandForm expandForm = new SheetExpandForm();
            if (expandForm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CurrentSheet.Expand(expandForm.Sizes);
                SetDirtyUnsaved();
            }
        }

        private void panelMain_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                m_ptContextOpened = Cursor.Position;
                cmPanel.Show(Cursor.Position);
            }
        }

        private void cmPanel_Opening(object sender, CancelEventArgs e)
        {
            Rectangle rcPB = pbMain.RectangleToScreen(pbMain.ClientRectangle);
            miPanelAddColumn.Visible = (m_ptContextOpened.X > rcPB.Right) || (m_ptContextOpened.X < rcPB.Left);
            miPanelAddRow.Visible = (m_ptContextOpened.Y > rcPB.Bottom) || (m_ptContextOpened.Y < rcPB.Top);
            miPanelAddRow.Enabled = !Properties.Settings.Default.ReadOnlyMaps;
            miPanelAddColumn.Enabled = !Properties.Settings.Default.ReadOnlyMaps;
        }

        private void miPanelAddColumn_Click(object sender, EventArgs e)
        {
            if (m_ptContextOpened.X > pbMain.RectangleToScreen(pbMain.ClientRectangle).Right)
                CurrentSheet.Expand(new ExpandSizes(0, 0, 0, 1));
            else
                CurrentSheet.Expand(new ExpandSizes(0, 0, 1, 0));

            SetDirty();
        }

        private void miPanelAddRow_Click(object sender, EventArgs e)
        {
            if (m_ptContextOpened.Y > pbMain.RectangleToScreen(pbMain.ClientRectangle).Bottom)
                CurrentSheet.Expand(new ExpandSizes(0, 1, 0, 0));
            else
                CurrentSheet.Expand(new ExpandSizes(1, 0, 0, 0));

            SetDirty();
        }

        private void MenuViewToolbar()
        {
            ShowToolbar(!miViewToolbar.Checked);
        }

        private void ShowToolbar(bool bShow)
        {
            miViewToolbar.Checked = bShow;
            tsEdit.Visible = bShow;
            Properties.Settings.Default.ShowToolbar = bShow;
        }

        private void SelectBlockColor()
        {
            ColorPatternSelectForm form = new ColorPatternSelectForm("Select block color and pattern");
            form.BlockColor = m_dcCurrentBlock;
            if (form.ShowDialog() == DialogResult.OK)
                SelectBlockColor(form.BlockColor);
        }

        private void SelectBlockColor(DrawColor dc)
        {
            m_dcLastBlock = m_dcCurrentBlock;
            m_dcCurrentBlock = dc;
            Bitmap bmpFill = Global.GetFillBitmap(m_dcCurrentBlock, tsbBlock.ContentRectangle.Size, true);
            if (tsbBlock.Image != null)
                tsbBlock.Image.Dispose();
            tsbBlock.Image = bmpFill;
            if (Mode == BlockMode.Line || Mode == BlockMode.Notes || Mode == BlockMode.Edit || Mode == BlockMode.Play)
                SetMode(BlockMode.Block);
            ClearIconSelection();
        }

        private void pbSelectColor_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (!EditingNote)
                    BeginEditNote(CurrentSheet.Cursor, tbSymbol);
                m_fastColor.SetAnchor(pbSelectColor.Parent.PointToScreen(new Point(pbSelectColor.Left, pbSelectColor.Bottom)), true);
                m_fastColor.DefaultColor = m_dcCurrentLine;
                m_fastColor.SetDrawColors(Properties.Settings.Default.DrawColors.Notes, BlockMode.Notes);
                m_fastColor.PickerType = ToolBarCapture.Note;
                m_fastColor.Show();
                pbSelectColor.Capture = true;
            }
        }

        void OnFastColorSelected(object sender, EventArgs e)
        {
            switch (m_fastColor.PickerType)
            {
                case ToolBarCapture.Block:
                    SelectBlockColor(m_fastColor.SelectedColor);
                    break;
                case ToolBarCapture.Line:
                    SelectLineColor(m_fastColor.SelectedColor);
                    break;
                case ToolBarCapture.Note:
                    SelectNoteColor(m_fastColor.SelectedColor);
                    break;
                default:
                    break;
            }
            m_fastColor.Hide();
        }

        private void pbSelectColor_MouseMove(object sender, MouseEventArgs e)
        {
            if (!pbSelectColor.Capture)
                return;

            m_fastColor.SelectColor(pbSelectColor.PointToScreen(e.Location));
        }

        private void pbSelectColor_MouseUp(object sender, MouseEventArgs e)
        {
            pbSelectColor.Capture = false;
            if (m_fastColor.Visible)
            {
                SelectNoteColor(m_fastColor.SelectedColor);
                m_fastColor.Hide();
            }
            else if (e.Button == MouseButtons.Left)
                OnNoteColorClick();
        }

        private void SelectNoteColor(DrawColor dc)
        {
            pbSelectColor.BackColor = dc.color;
            tbSymbol.ForeColor = dc.color;
        }

        private void OnNoteColorClick()
        {
            if (!EditingNote)
                BeginEditNote(CurrentSheet.Cursor, tbSymbol);

            if (CurrentSheet.NoteAtCursor == null)
                return;

            if (!EditingNote && String.IsNullOrEmpty(CurrentSheet.NoteAtCursor.Text))
                return;

            int iStart = tbNote.SelectionStart;
            int iLength = tbNote.SelectionLength;

            TitledColorDialog colorDialog = new TitledColorDialog("Select note color");
            colorDialog.Color = pbSelectColor.BackColor;
            if (colorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                SelectNoteColor(new DrawColor(colorDialog.Color));

            if (!EditingNote)
            {
                // Change note color immediately
                CurrentSheet.NoteAtCursor.Color = pbSelectColor.BackColor;
                SetDirty();
            }
            else
            {
                tbNote.SelectionLength = iLength;
                tbNote.SelectionStart = iStart;
            }
        }

        private void SelectLineColor()
        {
            LineStyleSelectForm form = new LineStyleSelectForm("Select line color and style");
            form.LineColor = m_dcCurrentLine;
            if (form.ShowDialog() == DialogResult.OK)
                SelectLineColor(form.LineColor);
        }

        private void SelectLineColor(DrawColor dc)
        {
            m_dcLastLine = m_dcCurrentLine;
            m_dcCurrentLine = dc;
            Bitmap bmpLine = Global.GetLineBitmap(m_dcCurrentLine, tsbLine.ContentRectangle.Size, true);
            if (tsbLine.Image != null)
                tsbLine.Image.Dispose();
            tsbLine.Image = bmpLine;
            if (Mode == BlockMode.Block || Mode == BlockMode.Notes || Mode == BlockMode.Edit || Mode == BlockMode.Play)
                SetMode(BlockMode.Line);
            ClearIconSelection();
        }

        private void tscMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            BlockModeItem item = tscMode.SelectedItem as BlockModeItem;
            if (item == null)
                return;
            SetMode(item.Mode);
        }

        private void tseTitle_TextChanged(object sender, EventArgs e)
        {
            if (CurrentSheet == null)
                return;

            if (CurrentSheet.Title != tseTitle.Text)
            {
                CurrentSheet.Title = tseTitle.Text;
                UpdateMapMenu(m_iCurrentSheet, CurrentSheet.Title);
                SetUnsaved();
            }
        }

        private void tsbIcon_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_iconsForm.Location = tsEdit.PointToScreen(tsbIcon.Bounds.Location);
                m_iconsForm.DeselectOnDeactivate = false;
                m_iconsForm.Show();
                m_captureToolBar = ToolBarCapture.Icon;
                tsEdit.Capture = true;
            }
        }

        private void tsbLine_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_fastColor.SetAnchor(tsEdit.PointToScreen(tsbLine.Bounds.Location));
                m_fastColor.DefaultColor = m_dcCurrentLine;
                m_fastColor.SetDrawColors(Properties.Settings.Default.DrawColors.Lines, BlockMode.Line);
                m_fastColor.Show();
                m_fastColor.PickerType = ToolBarCapture.Line;
                m_captureToolBar = ToolBarCapture.Line;
                tsEdit.Capture = true;
            }
        }

        private void tsEdit_MouseUp(object sender, MouseEventArgs e)
        {
            tsEdit.Capture = false;
            if (m_fastColor.Visible)
            {
                switch(m_captureToolBar)
                {
                    case ToolBarCapture.Block:
                        SelectBlockColor(m_fastColor.SelectedColor);
                        break;
                    case ToolBarCapture.Line:
                        SelectLineColor(m_fastColor.SelectedColor);
                        break;
                }
            }
            else if (m_captureToolBar == ToolBarCapture.Icon)
            {
                if (m_iconsForm.Visible)
                {
                    if (m_iconsForm.PointOverColor(tsEdit.PointToScreen(e.Location)))
                    {
                        if (m_iconsForm.SelectColor())
                        {
                            if (m_iconsForm.SelectedIcon != null)
                                SetIconMode();
                        }
                    }
                    else
                        m_iconsForm.SetHighlightedAsSelected();
                }
            }
            m_iconsForm.Hide();
            m_fastColor.Hide();
        }

        private void tsEdit_MouseMove(object sender, MouseEventArgs e)
        {
            if (!tsEdit.Capture)
                return;

            switch (m_captureToolBar)
            {
                case ToolBarCapture.Icon:
                    m_iconsForm.HighlightIcon(tsEdit.PointToScreen(e.Location), true);
                    break;
                case ToolBarCapture.Block:
                case ToolBarCapture.Line:
                    m_fastColor.SelectColor(tsEdit.PointToScreen(e.Location));
                    break;
                default:
                    break;
            }
        }

        private void tsbBlock_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                m_fastColor.SetAnchor(tsEdit.PointToScreen(tsbBlock.ContentRectangle.Location));
                m_fastColor.Location = tsbBlock.ContentRectangle.Location;
                m_fastColor.DefaultColor = m_dcCurrentBlock;
                m_fastColor.SetDrawColors(Properties.Settings.Default.DrawColors.Blocks, BlockMode.Block);
                m_fastColor.PickerType = ToolBarCapture.Block;
                m_captureToolBar = ToolBarCapture.Block;
                m_fastColor.Show();
                tsEdit.Capture = true;
            }
        }

        private void tsbBlock_Click(object sender, EventArgs e)
        {
            SelectBlockColor();
        }

        private void tsbLine_Click(object sender, EventArgs e)
        {
            SelectLineColor();
        }

        private void tsbIcon_Click(object sender, EventArgs e)
        {
            m_iconsForm.Location = tsEdit.PointToScreen(tsbIcon.Bounds.Location);
            m_iconsForm.DeselectOnDeactivate = true;
            m_iconsForm.Show();
        }

        private void MenuView100pc()
        {
            if (CurrentSheet.IsZoom(100))
                return;
            CurrentSheet.SquareSize = new Size(16, 16);
            AfterChangeSquareSize();
            SetDirty();
        }

        private void MenuView200pc()
        {
            if (CurrentSheet.IsZoom(200))
                return;
            CurrentSheet.SquareSize = new Size(32, 32);
            AfterChangeSquareSize();
            SetDirty();
        }

        private void MenuView150pc()
        {
            if (CurrentSheet.IsZoom(150))
                return;
            CurrentSheet.SquareSize = new Size(24, 24);
            AfterChangeSquareSize();
            SetDirty();
        }

        private void MenuView300pc()
        {
            if (CurrentSheet.IsZoom(300))
                return;
            CurrentSheet.SquareSize = new Size(48, 48);
            AfterChangeSquareSize();
            SetDirty();
        }

        private void menuView_DropDownOpening(object sender, EventArgs e)
        {
        }

        private void miGridRemoveIcons_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.ReadOnlyMaps)
            {
                ShowReadOnlyMapMessage();
                return;
            }

            CurrentSheet.RemoveIcons(m_ptContextOpened);
            SetDirtyUnsaved();
        }

        private void MenuViewFitWidth()
        {
            int iWidth = (panelMain.Width - 2) / CurrentSheet.GridWidth;
            int iHeight = CurrentSheet.GridHeight * iWidth;
            if (iHeight > panelMain.Height)
                iWidth--;
            CurrentSheet.SetSquareSize(new Size(iWidth, iWidth));
            ForceRefreshOnDisplay();
        }

        private void MenuViewFitHeight()
        {
            int iHeight = (panelMain.Height - 2) / CurrentSheet.GridHeight;
            int iWidth = CurrentSheet.GridWidth * iHeight;
            if (iWidth > panelMain.Width)
                iHeight--;
            CurrentSheet.SetSquareSize(new Size(iHeight, iHeight));
            ForceRefreshOnDisplay();
        }

        private void MenuViewFitInPanel()
        {
            int iWidth = (panelMain.Width - 2) / CurrentSheet.GridWidth;
            int iHeight = (panelMain.Height - 2) / CurrentSheet.GridHeight;
            int iMin = Math.Min(iWidth, iHeight);
            CurrentSheet.SetSquareSize(new Size(iMin, iMin));
            ForceRefreshOnDisplay();
        }

        private void MenuEditCrop()
        {
            if (Properties.Settings.Default.ReadOnlyMaps)
            {
                ShowReadOnlyMapMessage();
                return;
            }

            if (m_rcSelection != Rectangle.Empty)
                CurrentSheet.CropRectangle(m_rcSelection);
            else
                CurrentSheet.CropUnusedSquares();
            SetDirtyUnsaved();
        }

        private void MenuViewInfo()
        {
            MapLineInfo infoGrid = m_mapBook.GridLines;
            int iOldZoom = CurrentSheet == null ? 100 : CurrentSheet.DefaultZoom;
            InfoForm infoForm = new InfoForm();
            infoForm.SelectionFormInfo = m_infoSelectionForm;
            infoForm.SetUIFromBook(m_mapBook, m_iCurrentSheet);
            if (infoForm.ShowDialog() == DialogResult.OK)
            {
                infoForm.SetBookFromUI(m_mapBook, m_iCurrentSheet);
                if (!m_mapBook.GridLines.Equals(infoGrid))
                {
                    m_mapBook.ReplaceLines(infoGrid, m_mapBook.GridLines, true);
                    m_mapBook.RefreshAllSheets();
                }
                UpdateTitle();
                if (iOldZoom == infoForm.DefaultZoom)
                    CurrentSheet.NeverDisplayed = false;
                ForceRefreshOnDisplay();
                SetUnsaved();
            }
            m_infoSelectionForm = infoForm.SelectionFormInfo;
        }

        private void miGridYouAreHere_Click(object sender, EventArgs e)
        {
            CurrentSheet.SetYouAreHere(new LocationInformation(m_ptContextOpened), this, IgnoreInaccessible);
            SetDirty();
        }

        private void miGridCancelSelection_Click(object sender, EventArgs e)
        {
            CancelSelection();
        }

        public void CancelSelection()
        {
            if (m_rcSelection != Rectangle.Empty)
            {
                m_ptNextSquare = m_ptOriginalCapture = m_ptLastSquare = GetSquareLocationAtMouse();
                m_bmpCopyMoveBlocks = null;
                m_bmpUnderSelection = null;
                m_rcSelection = Rectangle.Empty;
                ForceRefreshOnDisplay();
            }
        }

        private void PopulateRecentFilesMenu()
        {
            miFileRecentFiles.DropDownItems.Clear();
            if (Properties.Settings.Default.MRUList == null)
            {
                miFileRecentFiles.Enabled = false;
                return;
            }

            int iIndex = 1;
            foreach (string strPath in Properties.Settings.Default.MRUList.Paths)
            {
                ToolStripItem tsi = miFileRecentFiles.DropDownItems.Add(String.Format("&{0} {1}", iIndex++, strPath));
                tsi.Tag = strPath;
                tsi.Click += new EventHandler(tsi_Click);
            }
        }

        void tsi_Click(object sender, EventArgs e)
        {
            if (sender is ToolStripItem)
            {
                LoadFile(((ToolStripItem)sender).Tag as string);
            }
        }

        private void menuFile_DropDownOpening(object sender, EventArgs e)
        {
            miFileNew.Enabled = !m_bExportingMaps;
            miFileOpen.Enabled = !m_bExportingMaps;
            miFileRecentFiles.Enabled = !m_bExportingMaps;
            miFileLoadInternalMap.Enabled = !m_bExportingMaps;

            miFileRecentFiles.Enabled = (Properties.Settings.Default.MRUList != null);
            PopulateRecentFilesMenu();
        }

        private void miIconDoor_Click(object sender, EventArgs e)
        {
            CycleDoorIcon();
        }

        private void RotateCurrentIcon(int iCount)
        {
            if (Mode == BlockMode.Play)
                return; // Don't rotate icons in play mode
            MapIcon icon = m_iconsForm.SelectedIcon;
            if (icon == null)
                return;

            for(int i = 0; i < Math.Abs(iCount); i++)
                icon.Orientation = Global.Rotate(icon.Orientation, Math.Sign(iCount) > 0);
            m_iconsForm.SelectedIcon = icon;
            CheckCurrentIcon();
        }

        private void CycleDoorIcon()
        {
            Point ptIcon = m_iconsForm.GetSelectedIcon();

            if (m_mapBook.QuickDoor == DoorType.FullSquare)
            {
                if (ptIcon.Y != 2)
                    ptIcon = new Point(0,2);
                else
                {
                    ptIcon.X++;
                    if (ptIcon.X > 1)
                        ptIcon.X = 0;
                }
            }
            else
            {
                if (ptIcon.Y != 3)
                    ptIcon = new Point(0,3);
                else
                {
                    ptIcon.X++;
                    if (ptIcon.X > 3)
                        ptIcon.X = 0;
                }
            }
            m_iconsForm.SetSelectedIcon(ptIcon);
        }

        private void MenuSheetOrganize()
        {
            int iCurrentIndex = (CurrentSheet == null ? -1 : CurrentSheet.GameMapIndex);

            if (Properties.Settings.Default.ReadOnlyMaps)
            {
                ShowReadOnlyMapMessage();
                return;
            }

            Comparison<MapSheet> SheetIndexComparison = new Comparison<MapSheet>(Global.CompareSheetIndex);

            SheetOrganizerForm formOrganizer = new SheetOrganizerForm();
            formOrganizer.SetMain(this, WindowType.SheetOrganizer);
            formOrganizer.SetPaths(m_mapBook.GetMenuPaths());
            if (formOrganizer.ShowDialog() == DialogResult.OK)
            {
                if (formOrganizer.PathsChanged)
                {
                    m_mapBook.SetMenuPaths(formOrganizer.GetPaths());
                    m_mapBook.RefreshAllSheets();
                    ForceRefreshOnDisplay();
                    SetUnsaved();
                }
                MapSheet[] presort = new MapSheet[m_mapBook.Sheets.Count];
                m_mapBook.Sheets.Sort(SheetIndexComparison);
                if (iCurrentIndex != -1)
                    GotoGameMapIndexSheet(iCurrentIndex);
                m_bMapMenuDirty = true;
            }
        }

        private void miGridGoToLinkedSheet_Click(object sender, EventArgs e)
        {
            if (EditingNote)
                FinishNote();

            MapNote note = CurrentSheet.NoteAtPoint(m_ptContextOpened);
            if (note == null)
                return;

            // Assumes a note of the format "{map:Title} X,Y"
            int iIndex = note.Text.IndexOf("{map:");
            if (iIndex == -1)
                return;
            iIndex += 5;
            int iEnd = note.Text.IndexOf('}', iIndex);
            if (iEnd == -1)
                return;
            string strMap = note.Text.Substring(iIndex, iEnd - iIndex);

            for (int i = 0; i < m_mapBook.Sheets.Count; i++)
            {
                if (m_mapBook.Sheets[i].Title == strMap)
                {
                    SelectSheet(i);
                    // Also set the cursor to the coordinates after the {map:} command, if any
                    Match match = Regex.Match(note.Text.Substring(iEnd), @"\s*(\d+)\s*,\s*(\d+)");
                    if (match.Success && match.Groups.Count > 2)
                    {
                        int iX = 0;
                        int iY = 0;
                        if (Int32.TryParse(match.Groups[1].Value, out iX) && Int32.TryParse(match.Groups[2].Value, out iY))
                        {
                            SetCursor(TranslateToInternalMap(new Point(iX, iY)));
                        }
                    }
                    return;
                }
            }

            MessageBox.Show(String.Format("Could not find linked map \"{0}\"", strMap), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnUnicode_Click(object sender, EventArgs e)
        {
            if (!EditingNote)
                BeginEditNote(CurrentSheet.Cursor, tbSymbol);

            UnicodeSelectionForm form = new UnicodeSelectionForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                tbSymbol.Text = form.SelectedSymbol;
            }
        }

        private bool CheckGameMemory()
        {
            if (m_hacker != null)
                m_hacker.Stop();

            if (Properties.Settings.Default.Game != GameNames.None)
            {
                if (m_hacker == null)
                    m_hacker = Games.CreateHacker(Properties.Settings.Default.Game);

                if (m_hacker == null)
                    return false;

                if (!m_hacker.HasReinitializedHandler)
                    m_hacker.Reinitialized += m_hacker_Reinitialized;

                if (!m_hacker.Init())
                {
                    // Return false only if there is a DOSBox window running, but the initialization process failed
                    return m_hacker.DOSBoxWindow == IntPtr.Zero;
                }
            }

            if (m_hacker != null)
                m_hacker.Start();

            return true;
        }

        private void SetFormHackers()
        {
            foreach (Form form in Forms.Values)
            {
                HackerBasedForm hackerForm = form as HackerBasedForm;
                if (hackerForm != null)
                    hackerForm.SetMain(this);
            }
        }

        void m_hacker_Reinitialized(object sender, EventArgs e)
        {
            if (IsDisposed)
                return;

            if (InvokeRequired)
                Invoke((MethodInvoker)(() => SetFormHackers()));
            else
                SetFormHackers();

            if (Global.Debug)
                SetCaption();

            if (Properties.Settings.Default.AutoSwitchToPlayMode)
            {
                if (InvokeRequired)
                    Invoke((MethodInvoker)(() => SetPlayMode()));
                else
                    SetPlayMode();
            }
        }

        private void SetPlayMode() { SetMode(BlockMode.Play); }

        private float[] m_cpuTimes = new float[20] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        private int m_cpuIndex = 0;

        private string GetDebugTitle(string strTitle)
        {
            double fTime = (DateTime.Now - m_dtLastDebugCaptionUpdate).TotalMilliseconds;
            if (fTime < 100.0)
                return m_strLastTitle;  // Don't spike CPU trying to get CPU time

            try
            {
                string strHook = KeyboardHook.IsActive() ? " (hook)" : "";

                if (Properties.Settings.Default.ShowCPU)
                {
                    if (Global.CpuCounter == null)
                        Global.CpuCounter = new PerformanceCounter("Process", "% Processor Time", Process.GetCurrentProcess().ProcessName);
                    m_cpuTimes[m_cpuIndex++] = Global.CpuCounter.NextValue();
                    if (m_cpuIndex >= m_cpuTimes.Length)
                        m_cpuIndex = 0;
                    if (fTime > 1000)
                    {
                        m_dtLastDebugCaptionUpdate = DateTime.Now;
                        if (m_hacker != null && !IsDisposed)
                            return String.Format("{0:X8}{1} ({2:F1}%): {3}", m_hacker.GetBaseAddress(), strHook, m_cpuTimes.Average(), strTitle);
                    }
                    else
                        return m_strLastTitle;
                }
                else if (m_hacker != null && !IsDisposed)
                    return String.Format("{0:X8}{1}: {2}", m_hacker.GetBaseAddress(), strHook, strTitle);
            }
            catch (Exception)
            {
                // Can crash on exit
            }
            return strTitle;
        }

        public bool ShowingCurrentMap
        {
            get
            {
                if (m_hacker == null || !m_hacker.IsValid)
                    return false;

                if (CurrentSheet == null)
                    return false;

                return (CurrentSheet.GameMapIndex == m_hacker.GetCurrentMapIndex());
            }
        }

        private DateTime m_dtTest = DateTime.MinValue;

        private void VisitSquare(Point pt)
        {
            if (CurrentSheet.SetVisited(pt, true))
            {
                CurrentSheet.GetSquareAtGridPoint(pt)?.SetDirty(DirtyType.Back);
                if (Global.UpdateCartography)
                    Hacker.ToggleCartography(TranslateToGameMap(pt), Toggle.Set);
                SetDirtyUnsaved();
            }
        }

        private void UpdateLocationFromGame()
        {
            if (m_hacker == null || !m_hacker.Running)
                return;

            GameState state = m_hacker.GetGameState();

            if (state == null || (!m_hacker.HasParty && !state.CharCreation))
            {
                tbPartyLocation.Text = "";
                return;
            }

            LocationInformation info = state.Location;

            if (info == null || (info.NumChars < 1 && !state.CharCreation))
            {
                tbPartyLocation.Text = "";
                return;     // Can't "be" anywhere with no characters
            }

            // Don't immediately move the party marker; some games are still using the wrong position for a few cycles
            if (state.Main == MainState.LoadingMap)
                m_iMapLoadGraceCounter = 8;

            bool bLoadingMap = m_iMapLoadGraceCounter > 0;

            if (CurrentSheet != null)
            {
                if (m_hacker.IsGameFocused || m_bOverrideAutoSwitch)
                {
                    if (Properties.Settings.Default.AutoSwitchSheets &&
                        info.MapIndex != CurrentSheet.GameMapIndex &&
                        (!CurrentSheet.IsLegend || m_iLegendHoldIndex == -1) &&
                        !bLoadingMap)
                    {
                        m_bOverrideAutoSwitch = false;
                        GotoGameMapIndexSheet(info.MapIndex);
                        m_lastMonsterLocations = null;
                        m_lastActiveSquares = null;
                    }
                }

                if (state.Main == MainState.SignIn || bLoadingMap)
                {
                    CurrentSheet.ClearYouAreHere();
                    return;
                }

                BasicLocation location = new BasicLocation(info);
                MapSheet sheetTest = CurrentSheet;
                if (CurrentSheet.IsLegend && m_iLegendHoldIndex != -1 && m_iLegendHoldIndex < m_mapBook.Sheets.Count)
                    sheetTest = m_mapBook.Sheets[m_iLegendHoldIndex];

                if (info.MapIndex == -1)
                    sheetTest.ClearYouAreHere();

                if (info.MapIndex == sheetTest.GameMapIndex)
                    sheetTest.CurrentMap = true;
                else
                    sheetTest.CurrentMap = false;

                Global.UpdateText(tbPartyLocation, String.Format("{0},{1}", location.PrimaryCoordinates.X, location.PrimaryCoordinates.Y));

                if (CurrentSheet.IsLegend || state.NotPlaying)
                    return;

                if (m_lastActiveSquares != null && m_lastActiveSquares.Changed)
                {
                    UpdateActiveSquareLocations(sheetTest, m_lastActiveSquares, m_bForceActiveSquareUpdate);
                    m_bForceActiveSquareUpdate = false;
                }

                location.PrimaryCoordinates = m_mapBook.TranslateLocationToMap(location.PrimaryCoordinates, CurrentSheet);

                if (sheetTest.YouAreHere != null && 
                    (location.PrimaryCoordinates != sheetTest.YouAreHere.PrimaryCoordinates ||
                     location.Facing != sheetTest.YouAreHere.Facing ||
                     location.LightDistance != sheetTest.YouAreHere.LightDistance
                    )
                   )
                {
                    if (Hacker.NeedsMovementDelay)
                    {
                        if (!Global.IsNear(location.PrimaryCoordinates, sheetTest.YouAreHere.PrimaryCoordinates))
                        {
                            if (m_iMovementGraceCounter == 0)
                                m_iMovementGraceCounter = 4;
                            if (m_iMovementGraceCounter > 0)
                            {
                                m_iMovementGraceCounter--;
                                if (m_iMovementGraceCounter > 0)
                                    return;
                            }
                        }
                    }

                    if (Hacker.VisitFacingSquare(location, CurrentSheet))
                    {
                        VisitSquare(Global.OffsetPoint(location.PrimaryCoordinates, location.Facing));
                    }
                    if (!state.InCombat && Properties.Settings.Default.RevealTeleports)
                    {
                        Point ptNext = Global.OffsetPoint(location.PrimaryCoordinates, location.Facing);
                        MapSquare squareNext = CurrentSheet.GetSquareAtGridPoint(ptNext);
                        if (squareNext != null && squareNext.Note != null && Global.TeleportRegex.IsMatch(squareNext.Note.Text))
                        {
                            VisitSquare(ptNext);
                        }
                        else if (info.LastKeypress == Keys.Up ||
                                 info.LastKeypress == Keys.Down ||
                                 info.LastKeypress == Keys.B ||
                                 (info.LastKeypress == Keys.Enter && info.LastSpellNonCombat == MMGenericSpell.Etherealize)
                                 )
                        {
                            if (location.PrimaryCoordinates != m_lastLocation.PrimaryCoordinates && info.MapIndex == m_lastLocation.MapIndex)
                            {
                                // Check for teleporters
                                Point[] ptsExpected = GetExpectedNextSquares(m_lastLocation.PrimaryCoordinates, m_lastLocation.Facing, info.LastKeypress != Keys.Down);
                                bool bInExpected = false;
                                foreach (Point pt in ptsExpected)
                                {
                                    if (location.PrimaryCoordinates == pt)
                                    {
                                        bInExpected = true;
                                        break;
                                    }
                                }
                                if (!bInExpected)
                                {
                                    if (ptsExpected[0] != Global.NullPoint)
                                    {
                                        MapSquare square = sheetTest.GetSquareAtGridPoint(ptsExpected[0]);
                                        if (square != null)
                                        {
                                            if (sheetTest.SetVisited(ptsExpected[0], true))
                                                SetUnsaved();
                                            if (Global.UpdateCartography)
                                                Hacker.ToggleCartography(TranslateToGameMap(ptsExpected[0]), Toggle.Set);
                                            SetDirty();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (ShowingCurrentMap)
                    {
                        m_lastLocation = location;
                        m_lastCartography = null;  // Force in-game cartography to take precedence, if available
                        CartographyTimer();
                        UpdateYouAreHere(sheetTest, location);
                    }
                }
                else if (CurrentSheet.YouAreHere == null || !CurrentSheet.YouAreHere.Drawn)
                    UpdateYouAreHere(sheetTest, location);

                // To avoid situations such as a fly spell revealing the previous party location on the new map,
                // we update the "visited" property only if we have been on the square for more than one polling interval.
                if (m_lastLocation != location && m_lastLocation.PrimaryCoordinates == info.PrimaryCoordinates)
                    sheetTest.SetYouAreHereVisited();
            }
        }

        private void UpdateYouAreHere(MapSheet sheet, BasicLocation info)
        {
            if (!ShowingCurrentMap)
                return;

            if (Properties.Settings.Default.AutoShowNoteAtLocation &&
                CurrentSheet.HasNote(info.PrimaryCoordinates) &&
                !EditingNote && 
                info.MapIndex == CurrentSheet.GameMapIndex)
            {
                m_ptCurrentNote = info.PrimaryCoordinates;
                SetNoteText(info.PrimaryCoordinates, true);
            }

            if (sheet.YouAreHere != null && 
                info.PrimaryCoordinates == sheet.YouAreHere.PrimaryCoordinates &&
                info.Facing == sheet.YouAreHere.Facing &&
                sheet.YouAreHere.Drawn &&
                info.LightDistance == sheet.YouAreHere.LightDistance
                )
                return;

            bool bChanges = sheet.SetYouAreHere(info, this, IgnoreInaccessible);
            if (bChanges || sheet.HasChanges)
            {
                sheet.HasChanges = false;
                SetUnsaved();
            }
            if (sheet == CurrentSheet)
                SetDirty();
        }

        private void UpdateMonsterLocations(MapSheet sheet, MonsterLocations monsters)
        {
            sheet.Monsters = monsters;
            if (sheet.HasChanges)
            {
                sheet.HasChanges = false;
                SetUnsaved();
            }
            if (sheet == CurrentSheet)
                SetDirty();
        }

        private void UpdateItemLocations(MapSheet sheet, ItemLocations items)
        {
            sheet.Items = items;
            if (sheet.HasChanges)
            {
                sheet.HasChanges = false;
                SetUnsaved();
            }
            if (sheet == CurrentSheet)
                SetDirty();
        }

        private void UpdateActiveSquareLocations(MapSheet sheet, ActiveSquares active, bool bForceUpdate = false)
        {
            if (active.AllInactive)
                m_bRedrawAfterNextActive = true;
            else if (m_bRedrawAfterNextActive)
            {
                ForceRefreshOnDisplay();
                m_bRedrawAfterNextActive = false;
            }

            if (sheet.SetActiveSquares(active, m_mapBook, bForceUpdate))
                SetDirty();

            if (sheet.HasChanges)
            {
                sheet.HasChanges = false;
                SetUnsaved();
            }
        }

        private void UpdateYouAreHere(MapSheet sheet, LocationInformation info, MonsterLocations monsters)
        {
            if (CurrentSheet.HasNote(info.PrimaryCoordinates) && !EditingNote && info.MapIndex == CurrentSheet.GameMapIndex)
                SetNoteText(info.PrimaryCoordinates, true);

            sheet.SetYouAreHere(info, this, IgnoreInaccessible);
            sheet.Monsters = monsters;
            if (sheet.HasChanges)
            {
                sheet.HasChanges = false;
                SetUnsaved();
            }
            if (sheet == CurrentSheet)
                SetDirty();
        }

        private void miGridCopyNote_Click(object sender, EventArgs e)
        {
            MapNote note = CurrentSheet.NoteAtPoint(m_ptContextOpened);
            if (note != null)
                note.CopyToClipboard();
        }

        private void miGridPaste_Click(object sender, EventArgs e)
        {
            Paste();
        }

        private void PasteNote()
        {
            if (Properties.Settings.Default.ReadOnlyNotes)
                return;

            IDataObject dataObj = Clipboard.GetDataObject();
            string format = typeof(MapNote).FullName;

            if (dataObj.GetDataPresent(format))
            {
                CurrentSheet.ClearRedo();
                CurrentSheet.AddUndoNote(m_ptContextOpened == Global.NullPoint ? Point.Empty : m_ptContextOpened);

                MapNote note = dataObj.GetData(format) as MapNote;
                note.Location = m_ptContextOpened == Global.NullPoint ? Point.Empty : m_ptContextOpened;
                CurrentSheet.SetNote(note);
                SetDirtyUnsaved();
            }
        }

        private void PasteBlocks()
        {
            if (Properties.Settings.Default.ReadOnlyMaps)
            {
                ShowReadOnlyMapMessage();
                return;
            }

            EditFlags flags = Global.EditSettings;
            IDataObject dataObj = Clipboard.GetDataObject();
            string format = typeof(MapSquareArray).FullName;

            if (dataObj.GetDataPresent(format))
            {
                CancelSelection();
                MapSquareArray array = dataObj.GetData(format) as MapSquareArray;
                CurrentSheet.ClearRedo();
                m_undoBlocks = CurrentSheet.StartUndoBlock().Squares;
                Rectangle rc = array.Bounds;
                if (rc != Rectangle.Empty)
                {
                    rc.Offset(-rc.Location.X, -rc.Location.Y);
                    rc.Inflate(-1, -1);
                    CurrentSheet.CopyBlocks(m_undoBlocks, rc, m_ptContextOpened == Global.NullPoint ? Point.Empty : m_ptContextOpened, flags, array);
                }
                else if (array != null && flags.Labels)
                {
                    // A labels-only paste uses the entire grid
                    CurrentSheet.ManipulateLabels(CurrentSheet.GridRectangle, Point.Empty, true, array.GetLabels());
                }
                ForceRefreshOnDisplay();
                SetDirtyUnsaved();
            }
        }

        private void miGridHackTeleport_Click(object sender, EventArgs e)
        {
            TeleportTo(TranslateToGameMap(m_ptContextOpened));
        }

        public void TeleportTo(Point pt)
        {
            m_hacker.SetLocation(pt);
        }

        public void TeleportPartyDirection(Direction dir)
        {
            GameState state = m_hacker.GetGameState();

            if (state == null || (!m_hacker.HasParty && !state.CharCreation))
                return;

            TeleportTo(Global.OffsetPoint(state.Location.PrimaryCoordinates, dir));
        }

        private void UpdateMapMenu(int iSheetIndex, string strText)
        {
            UpdateMapMenu(menuMaps.DropDownItems, iSheetIndex, strText);
        }

        private void UpdateMapMenu(ToolStripItemCollection menus, int iSheetIndex, string strText)
        {
            if (menus == null)
                return;

            foreach (ToolStripMenuItem item in menus)
            {
                if (item.Tag is SheetTag && ((SheetTag) item.Tag).Index == iSheetIndex)
                {
                    item.Text = strText;
                    return;
                }
                UpdateMapMenu(item.DropDownItems, iSheetIndex, strText);
            }
        }

        private void CheckSheetMenu(ToolStripItemCollection menus, int iCheckSheet)
        {
            if (menus == null)
                return;

            foreach (ToolStripMenuItem item in menus)
            {
                if (item.Tag != null)
                    item.Checked = ((SheetTag) item.Tag).Index == iCheckSheet;
                CheckSheetMenu(item.DropDownItems, iCheckSheet);
            }
        }

        private void menuMaps_DropDownOpening(object sender, EventArgs e)
        {
            CheckSheetMenu(menuMaps.DropDownItems, m_iCurrentSheet);
        }

        private void GenerateMapMenu()
        {
            ToolStripMenuItem root = new ToolStripMenuItem("&Maps");
            root.Name = "menuMaps";
            for (int i = 0; i < m_mapBook.Sheets.Count; i++)
            {
                string strPath = m_mapBook.Sheets[i].MenuPath;
                if (String.IsNullOrWhiteSpace(strPath))
                    strPath = "\\";
                string[] paths = strPath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
                ToolStripMenuItem menuCurrent = root;
                foreach (string strSubPath in paths)
                {
                    ToolStripItem[] items = menuCurrent.DropDownItems.Find(strSubPath, false);
                    if (items.Length < 1)
                    {
                        menuCurrent = (ToolStripMenuItem)menuCurrent.DropDownItems.Add(strSubPath);
                        menuCurrent.Name = strSubPath;
                    }
                    else
                        menuCurrent = (ToolStripMenuItem)items[0];
                }

                ToolStripMenuItem item = (ToolStripMenuItem)menuCurrent.DropDownItems.Add(String.IsNullOrWhiteSpace(m_mapBook.Sheets[i].Title) ? "<untitled>" : m_mapBook.Sheets[i].Title);
                    //String.Format("{0}: {1}", menuCurrent.DropDownItems.Count + 1, String.IsNullOrWhiteSpace(m_mapBook.Sheets[i].Title) ? "<untitled>" : m_mapBook.Sheets[i].Title));
                item.Click += new EventHandler(menuMapsItem_Click);
                item.Tag = new SheetTag(i);
                if (i == m_iCurrentSheet)
                    item.Checked = true;
                if (m_evtShutdown.WaitOne(0))
                    return;
            }

            m_bMapMenuDirty = false;
            for(int i = 0; i < menuStrip1.Items.Count; i++)
            {
                if (menuStrip1.Items[i].Name == "menuMaps")
                {
                    if (menuStrip1.InvokeRequired)
                        menuStrip1.Invoke((MethodInvoker)delegate 
                        {
                            menuStrip1.Items.RemoveAt(i);
                            menuStrip1.Items.Insert(i, root);
                            menuStrip1.Items[i].Enabled = true;
                        });
                    else
                    {
                        menuStrip1.Items.RemoveAt(i);
                        menuStrip1.Items.Insert(i, root);
                        menuStrip1.Items[i].Enabled = true;
                    }
                }
            }
            menuMaps = root;
            menuMaps.DropDownOpening += new System.EventHandler(this.menuMaps_DropDownOpening);
        }

        private void MenuSheetClearVisited()
        {
            if (MessageBox.Show("This will reset the \"visited\" status for this entire map sheet.  Are you sure you wish to do this?",
                "Reset this sheet?", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
            {
                CurrentSheet.ClearRedo();
                CurrentSheet.AddUndoVisited();
                CurrentSheet.ResetVisited();
                SetUnsaved();
            }
            ForceRefreshOnDisplay();
        }

        private void MenuGameViewParty()
        {
            m_showNext = new ShowNext(ShowNextType.PartyWindow);
        }

        private bool CheckHackerRunning(bool bHideErrors = false)
        {
            if (m_hacker == null || !m_hacker.Running)
            {
                if (!bHideErrors)
                    MessageBox.Show("The memory scanner is not running; please open the Options dialog and check your settings (particularly on the DOSBox tab).", "Could not scan memory", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void ShowPartyWindow(bool bHideErrors = false)
        {
            CreateOrShowWindow<ViewPartyForm>(ref m_formParty, WindowType.Party);
        }

        public bool ShowShopInventories
        {
            get { return m_bShowShopInventories; }

            set 
            {
                if (CheckHackerRunning(true))
                {
                    Shops shops = m_hacker.GetShopInfo();
                    if (shops != null && shops.InShop)
                    {
                        // User asked to close the window while in a shop -> change auto-show settings
                        Properties.Settings.Default.ShowShopInventories = false;
                    }
                }
                m_bShowShopInventories = value; 
            }
        }

        private void CheckShops()
        {
            if (!CheckHackerRunning(true))
                return;

            if (!Properties.Settings.Default.ShowShopInventories && !m_bShowShopInventories)
                return;

            Shops shops = m_hacker.GetShopInfo();

            if (shops == null)
                return;

            if (!shops.InShop && !m_bShowShopInventories)
                return;

            ShowShopInventoryWindow(shops, false);
        }

        private void ShowShopInventoryWindow(Shops shops = null, bool bActivate = true)
        {
            if (shops == null)
                shops = m_hacker.GetShopInfo();

            if (shops == null)
                return;

            if (Global.Windows.IsEmpty(WindowType.ShopInventory) && Global.FormVisible(m_formShopInventory))
            {
                Rectangle rc = RectangleToScreen(ClientRectangle);
                m_formShopInventory.Location = new Point(rc.Left, rc.Top + MainMenuStrip.Height);
                m_formShopInventory.Width = ClientRectangle.Right;
                m_formShopInventory.Height = ClientRectangle.Height - MainMenuStrip.Height;
            }
            if (bActivate)
                ActivateWindow<ShopInventoryForm>(ref m_formShopInventory, WindowType.ShopInventory);
            else
                CreateWindow<ShopInventoryForm>(ref m_formShopInventory, WindowType.ShopInventory);

            m_formShopInventory.SetInventories(shops, Hacker == null ? null : Hacker.ShopWindowTitle);
        }

        private void CheckEncounters()
        {
            if (!m_hacker.Running)
                return;

            if (!Properties.Settings.Default.ShowEncounters)
                return;

            if (!m_hacker.HasParty)
                return;

            EncounterInfo info = m_hacker.GetEncounterInfo();

            if (info == null)
                return;

            // Wizardry IV is odd in that you are the monsters, so we always want the "combat" window open
            if (Game != GameNames.Wizardry4)
            {
                if (info != null && !info.InCombat && !info.HasTreasure)
                    return; // No encounter and no treasure

                if (!Properties.Settings.Default.ShowTreasureWindow && !info.InCombat)
                    return;
            }

            if (Global.Windows.IsEmpty(WindowType.Encounter) && Global.FormVisible(m_formEncounters))
            {
                Rectangle rc = RectangleToScreen(ClientRectangle);
                m_formEncounters.Location = new Point(rc.Left, rc.Top + MainMenuStrip.Height);
                m_formEncounters.Width = ClientRectangle.Right;
                m_formEncounters.Height = ClientRectangle.Height - MainMenuStrip.Height;
            }

            CreateWindowNoShow<EncounterForm>(ref m_formEncounters, WindowType.Encounter);

            if (!m_formEncounters.SetEncounterInfo(info))
                return;
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                    LoadFile(files[0]);
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void MenuGameShowEncounters()
        {
            Properties.Settings.Default.ShowEncounters = !Properties.Settings.Default.ShowEncounters;
            if (!Properties.Settings.Default.ShowEncounters && Global.FormVisible(m_formEncounters))
                m_formEncounters.Close();
            UpdateEncounterMenu();
        }

        public void UpdateEncounterMenu()
        {
            miGameShowEncounters.Checked = Properties.Settings.Default.ShowEncounters;
        }

        private void MenuGameShowSpells()
        {
            if (CheckLastShortcut(Action.GameShowSpells))
                return;

            if (m_formSpells != null)
                ManualSpellWindowClose = false;

            if (Global.FormVisible(m_formSpells))
            {
                m_formSpells.Hide();
                return;
            }

            ShowSpellWindow(null);
        }

        private void ShowSpellWindow(SpellInfo info)
        {
            CreateOrShowWindow<SpellReferenceForm>(ref m_formSpells, WindowType.SpellReference);

            m_bFocusingWindows = true;
            if (m_formSpells.SetSpellInfo(info))
            {
                m_formSpells.Show();
                if (info == null)
                    m_formSpells.Activate();
                else
                    EnforceFormZOrder(m_formSpells, WindowType.SpellReference);
            }
            m_bFocusingWindows = false;
        }

        public void UpdateGameMenu()
        {
            miGameShowEncounters.Checked = Properties.Settings.Default.ShowEncounters;
        }

        public void CheckSpells()
        {
            if (!m_hacker.Running)
                return;

            if (!Properties.Settings.Default.ShowSpells)
            {
                if (m_formSpells != null && ManualSpellWindowClose)
                    m_formSpells.Hide();
                return;
            }

            SpellInfo info = m_hacker.GetSpellInfo();

            if (info == null)
                return;

            if (!(info.Game.Casting && info.Game.ActingIsCaster))
            {
                if (m_iSpellHideCount++ > 1)
                {
                    if (Global.FormVisible(m_formSpells) && !m_formSpells.OpenedManually)
                        m_formSpells.Hide();
                    m_iSpellShowCount = 0;
                    if (m_formSpells != null)
                        ManualSpellWindowClose = false;
                }

                return; // Not trying to cast a spell
            }

            if (m_formSpells != null && ManualSpellWindowClose)
            {
                // User requested that the spell window not be open
                m_formSpells.Hide();
                return;
            }

            if (Global.FormVisible(m_formSpells))
                return;

            if (m_iSpellShowCount++ > 1)
            {
                try
                {
                    ShowSpellWindow(info);
                    m_iSpellHideCount = 0;
                }
                catch (Exception ex)
                {
                    Global.LogWarning("Could not show spell window: " + ex.Message);
                }
            }
        }

        private void MenuGameShowMonsters()
        {
            if (!CheckHackerRunning(false))
                return;

            if (Global.FormVisible(m_formMonsters))
            {
                m_formMonsters.Close();
                return;
            }
            ActivateWindow<MonstersForm>(ref m_formMonsters, WindowType.Monsters);
        }

        private void MenuModePlay()
        {
            SetMode(BlockMode.Play);
        }

        private void MenuGameEditRoster()
        {
            if (Game == GameNames.Wizardry4)
            {
                MessageBox.Show("Wizardry 4 has no roster of characters and as such no roster editor is available.",
                    "No Roster in Wizardry 4", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (m_hacker != null && m_hacker.Running)
            {
                if (MessageBox.Show("Editing the roster file while the game is running may have adverse effects.  Continue?",
                    "Edit roster while game is running?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;
            }
            EditRosterForm form = new EditRosterForm();
            string strPath = String.Empty;
            string strFile = String.Empty;

            try
            {
                if (m_hacker == null)
                {
                    if (Properties.Settings.Default.AutoLaunchShortcuts.ContainsKey(Game))
                    {
                        strPath = Path.GetDirectoryName(Properties.Settings.Default.AutoLaunchShortcuts.Get(Game));
                        strFile = Games.RosterFile(Game);
                    }
                    else
                    {
                        MessageBox.Show("There is no game set; please check the \"Currently Playing\" setting in the Options.",
                            "Error opening roster editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                else
                {
                    strPath = Hacker.RosterPath;
                    strFile = Hacker.RosterFile;
                }
                form.RosterFilePath = Path.Combine(strPath, strFile);
                form.ShowDialog();
                if (form.RosterValid && Games.CorrectRoster(form.RosterGame, Hacker.Game))
                    Games.SetRosterFileAndPath(form.RosterGame, form.RosterFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Could not obtain the roster path information for the current game (\"{0}\").\r\n\r\nException: {1}",
                    Games.Name(Game), ex.Message), "Error opening roster editor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MenuGameCreationAssistant()
        {
            switch (Game)
            {
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                    MessageBox.Show("The versions of Wizardry 2 and 3 supported by \"Where Are We\" require that characters be imported from previous scenarios, " +
                    "so the Character Creation Assistant is unavailable", "No Character Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                case GameNames.Wizardry4:
                    MessageBox.Show("Wizardry 4 only has one pre-made character (Werdna), so the Character Creation Assistant is unavailable",
                        "No Character Creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                default:
                    break;
            }

            if (CheckLastShortcut(Action.GameCharacterCreationAssistant))
                return;
            if (!CheckHackerRunning(false))
                return;
            m_showNext = new ShowNext(ShowNextType.CreationAssistant);
        }

        public void ShowQuickRefNext()
        {
            m_showNext = new ShowNext(ShowNextType.QuickRef);
        }

        private void ShowCreationAssistant()
        {
            CheckGameMemory();
            if (!Properties.Settings.Default.EnableMemoryWrite)
            {
                MessageBox.Show("The Character Creation Assistant can only be used if \"Enable writing to game memory\" is checked under Options->Game.", "Memory Write Disabled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ActivateWindow<CreationAssistantForm>(ref m_formCreationAssistant, WindowType.CreationAssistant);
            m_formCreationAssistant.SetAssistant(Hacker.CreateCreationAssistantControl(this));
        }

        public ViewPartyForm PartyForm { get { return m_formParty; } }
        public QuickRefForm QuickRefForm { get { return m_formQuickRef; } }

        private void ShowQuickRef()
        {
            if (!CheckHackerRunning(true))
                return;
            CreateOrShowWindow<QuickRefForm>(ref m_formQuickRef, WindowType.QuickRef);
        }

        private void MenuGameTrainingAssistant()
        {
            if (CheckLastShortcut(Action.GameTrainingAssistant))
                return;
            if (!CheckHackerRunning(false))
                return;

            if (!m_hacker.UsesTrainingAssistant)
            {
                MessageBox.Show("The Training Assistant serves no purpose in " + Games.Name(m_hacker.Game),
                    "No Assistant Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
                m_showNext = new ShowNext(ShowNextType.TrainingAssistant);
        }

        private void ShowTrainingAssistant()
        {
            CheckGameMemory();
            if (!Properties.Settings.Default.EnableMemoryWrite)
            {
                MessageBox.Show("The Training Assistant can only be used if \"Enable writing to game memory\" is checked under Options->Game.", "Memory Write Disabled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            ActivateWindow<TrainingAssistantForm>(ref m_formTrainingAssistant, WindowType.TrainingAssistant);
            m_formTrainingAssistant.SetAssistant(Hacker.CreateTrainingAssistantControl(this));
        }

        private bool MenuGameResetHacker()
        {
            if (m_hacker != null && m_hacker.DOSBoxWindow == IntPtr.Zero)
            {
                m_showNext = new ShowNext("DOSBox is not running; please check your shortcuts on the Play tab of the Options", "No game detected");
                return false;
            }
            m_bOverrideAutoSwitch = true;
            if (!CheckGameMemory())
            {
                m_showNext = new ShowNext(Global.MemoryScanFail, "Memory scanner reset failed");
                return false;
            }
            CheckAutoShowWindows();
            return true;
        }

        private void CheckAutoShowWindows()
        {
            if (Properties.Settings.Default.Game == GameNames.None)
                return;     // Nothing to show if we aren't playing a game
            if (Global.Windows.AutoShow(WindowType.Party))
                ShowPartyWindow(true);
            if (Global.Windows.AutoShow(WindowType.GameInfo))
                ShowGameInfo();
            if (Global.Windows.AutoShow(WindowType.QuickRef))
                ShowQuickRef();
            if (Global.Windows.AutoShow(WindowType.Quests))
                ShowQuestWindow();
            Properties.Settings.Default.ShowEncounters = Global.Windows.AutoShow(WindowType.Encounter);
        }

        public SnapWindows FindSnapWindows(Form formMoving)
        {
            if (!Properties.Settings.Default.SnapWindowsToDOSBox)
                return null;

            Dictionary<IntPtr, Form> forms = Forms;
            List<Rectangle> snapWindows = new List<Rectangle>(forms.Count);
            bool bExtended = Properties.Settings.Default.UseExtendedWindowRect;
            foreach (Form form in forms.Values)
            {
                if (Global.FormVisible(form) && form != formMoving)
                    snapWindows.Add(bExtended ? NativeMethods.GetExtendedWindowRect(form.Handle) : form.Bounds);
            }
            if (m_hacker != null && m_hacker.DOSBoxWindow != IntPtr.Zero)
                snapWindows.Add(bExtended ? NativeMethods.GetExtendedWindowRect(m_hacker.DOSBoxWindow) : m_hacker.GetDOSBoxRect());

            SnapWindows snap = new SnapWindows(20, formMoving, snapWindows.ToArray(), Global.MinFormSize(formMoving.MinimumSize), Properties.Settings.Default.WindowSnapMargins);
            return snap;
        }

        public void ProcessMessage(ref Message m, Form form)
        {
            if (m_bFocusingWindows || m_bShuttingDown)
                return;
            switch (m.Msg)
            {
                case NativeMethods.WM_MOUSEACTIVATE:
                    m_bDOSBoxAboveWAW = false;
                    break;
                case NativeMethods.WM_PARENTNOTIFY:
                    m_bDOSBoxAboveWAW = false;
                    break;
                case NativeMethods.WM_ACTIVATE:
                    switch ((uint)m.WParam)
                    {
                        case NativeMethods.WA_ACTIVE:
                            if (m_hacker == null || !m_hacker.IsGameFocused)
                            {
                                if (Properties.Settings.Default.FocusDOSBoxWhenSelected)
                                    BringDOSBoxToFront();
                            }
                            break;
                        case NativeMethods.WA_CLICKACTIVE:
                            m_bDOSBoxAboveWAW = false;
                            if (m_hacker == null || !m_hacker.IsGameFocused)
                            {
                                if (Properties.Settings.Default.FocusDOSBoxWhenSelected)
                                    BringDOSBoxToFront();
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case NativeMethods.WM_ENTERSIZEMOVE:
                    m_bDOSBoxAboveWAW = false;
                    m_snapWindows = FindSnapWindows(form);
                    break;
                case NativeMethods.WM_MOVING:
                case NativeMethods.WM_SIZING:
                    if (m_snapWindows != null && !NativeMethods.IsAltDown())
                    {
                        NativeMethods.RECT rc = (NativeMethods.RECT)Marshal.PtrToStructure(m.LParam, typeof(NativeMethods.RECT));

                        Global.SnapToWindows(m.Msg, m.Msg == NativeMethods.WM_SIZING ? Global.DirectionFromWMSZ(m.WParam) : DirectionFlags.All, m_snapWindows, ref rc);
                        
                        Marshal.StructureToPtr(rc, m.LParam, false);
                    }
                    break;
            }
        }

        protected override void WndProc(ref Message m)
        {
            ProcessMessage(ref m, this);
            base.WndProc(ref m);
        }

        public ItemBag BagOfHolding
        {
            get
            {
                if (m_mapBook == null)
                    return null;

                if (m_mapBook.BagOfHolding.Game == GameNames.None)
                    m_mapBook.BagOfHolding.Game = Properties.Settings.Default.Game;

                if (m_mapBook.BagOfHolding.Game != Properties.Settings.Default.Game)
                    return null;

                return m_mapBook.BagOfHolding;
            }

            set
            {
                if (m_mapBook == null)
                    return;

                if (m_mapBook.BagOfHolding.Game != Properties.Settings.Default.Game)
                    return;

                m_mapBook.BagOfHolding = value;
                SetUnsaved();
            }
        }

        private void miDebugShowMapStrings_Click(object sender, EventArgs e)
        {
            string str = m_hacker.GetMapStrings();
            if (String.IsNullOrWhiteSpace(str))
                str = "<no strings found; game process may not be running>";

            Clipboard.SetText(str);

            if (m_formStrings == null || m_formStrings.IsDisposed)
                m_formStrings = new StringsViewForm();

            m_formStrings.ClearSearch();
            m_formStrings.Strings = str;
            m_formStrings.Show();
            EnforceFormZOrder(m_formStrings, WindowType.StringsView);
        }

        private void ReadMapQuadFromMemory(MapReplaceMode mode)
        {
            if (!m_hacker.Running)
                return;

            MMMapData data1 = m_hacker.GetMapData(true, 0) as MMMapData;
            MMMapData data2 = m_hacker.GetMapData(true, 1) as MMMapData;
            MMMapData data3 = m_hacker.GetMapData(true, 2) as MMMapData;
            MMMapData data4 = m_hacker.GetMapData(true, 3) as MMMapData;

            if (data1 != null && data2 != null && data3 != null && data4 != null)
            {
                MapSheet sheet = null;
                if (data1 is MM3MapData)
                    sheet = new MapSheet(m_mapBook, MMMapData.CreateFromQuad<MM3MapData>(data1, data2, data3, data4));
                else if (data1 is MM45MapData)
                    sheet = new MapSheet(m_mapBook, MMMapData.CreateFromQuad<MM45MapData>(data1, data2, data3, data4));
                else
                    sheet = new MapSheet(m_mapBook, MMMapData.CreateFromQuad<MMMapData>(data1, data2, data3, data4));
                sheet.GameMapIndex = data1.Index;
                sheet.Title = data1.Title.Title;
                sheet.MenuPath = data1.Title.Path;
                sheet.DefaultZoom = 100;
                if (mode == MapReplaceMode.GridOnly)
                    CurrentSheet.CopyGridDataFrom(sheet, mode);
                else
                {
                    m_mapBook.Sheets.Add(sheet);
                    SelectSheet(m_mapBook.Sheets.Count - 1);
                }
                m_lastMonsterLocations = null;
                SetDirtyUnsaved();
                m_bMapMenuDirty = true;
            }
        }

        private void ReadMapFromMemory(int iMap, MapReplaceMode mode)
        {
            if (!m_hacker.Running)
                return;

            MapData data = m_hacker.GetMapData(true, iMap);
            if (data != null)
            {
                MapSheet sheet = new MapSheet(m_mapBook, data);
                sheet.GameMapIndex = data.Index;
                sheet.Title = data.Title.Title;
                sheet.MenuPath = data.Title.Path;
                sheet.DefaultZoom = data.DefaultZoom;
                sheet.Live = data.AlwaysLive;
                sheet.Directionless = data.Directionless;
                if (mode != MapReplaceMode.Full)
                    CurrentSheet.CopyGridDataFrom(sheet, mode);
                else
                {
                    m_mapBook.Sheets.Add(sheet);
                    SelectSheet(m_mapBook.Sheets.Count - 1);
                }
                m_lastMonsterLocations = null;
                SetDirtyUnsaved();
                m_bMapMenuDirty = true;
            }
        }

        private void miDebugMemoryInfo_Click(object sender, EventArgs e)
        {
            if (m_hacker == null)
                return;
            StringsViewForm form = new StringsViewForm();
            form.Strings = m_hacker.GetDebugMemoryInfo();
            form.ShowDialog();
        }

        private void miGridMarkAsUnvisited_Click(object sender, EventArgs e)
        {
            CurrentSheet.ClearRedo();
            CurrentSheet.AddUndoVisited();
            bool bSet = CurrentSheet.ToggleVisited(m_ptContextOpened);
            if (Hacker != null && Properties.Settings.Default.UpdateCartWhenInaccessibleRevealed)
            {
                Hacker.ToggleCartography(TranslateToGameMap(m_ptContextOpened), bSet ? Toggle.Set : Toggle.Reset);
                if (Global.FormVisible(m_formEncounters))
                    m_formEncounters.UpdateUI();
            }
            SetDirtyUnsaved();
        }

        private void miGridAddNote_DropDownOpening(object sender, EventArgs e)
        {
            int iIndex = 1;
            foreach (ToolStripMenuItem item in miGridAddNote.DropDownItems)
            {
                NoteTemplateTag tag = item.Tag as NoteTemplateTag;
                if (tag.OriginalText.Contains('$'))
                {
                    StringBuilder strReplace = new StringBuilder(m_hacker == null ? tag.OriginalText : m_hacker.ReplaceNoteStrings(tag.OriginalText));
                    strReplace.Replace("$currentSheetTitle", CurrentSheet.Title);
                    item.Text = String.Format("{0}: {1}", iIndex, strReplace);
                    ((NoteTemplateTag)item.Tag).FinalText = strReplace.ToString();
                }
                iIndex++;
            }
        }

        private void ShowSearch()
        {
            bool bEditing = EditingNote;
            if (bEditing)
                FinishNote();
            if (!Global.FormVisible(m_formSearch))
            {
                ActivateWindow<SearchForm>(ref m_formSearch, WindowType.Search);
                m_formSearch.SetMapBook(m_mapBook, m_noteCurrent, bEditing);
            }
            else
            {
                m_formSearch.RefreshMapData(m_noteCurrent, bEditing);
            }


            m_formSearch.ClearSearch();
            m_formSearch.Show();
            EnforceFormZOrder(m_formSearch, WindowType.Search);
            m_formSearch.Activate();
        }

        private void ShowGameInfo()
        {
            CreateOrShowWindow<GameInformationForm>(ref m_formInfo, WindowType.GameInfo);
        }

        private void ShowQuestWindow()
        {
            ActivateWindow<QuestsForm>(ref m_formQuests, WindowType.Quests,
                new QuestWindowParams(m_mapBook.FlaggedQuests));
        }

        private void ShowScriptsWindow()
        {
            ShowScriptsWindow(Global.NullPoint);
        }

        private void ShowScriptsWindow(Point position)
        {
            if (!Hacker.HasScripts)
            {
                MessageBox.Show(Games.Name(Game) + " does not have a particular scripting language; all of its special squares are written in machine code.  " + 
                    "Unfortunately, this program does not have a disassembler in it to view that code.",
                    "No scripts for this game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            ActivateWindow<ScriptsForm>(ref m_formScripts, WindowType.Scripts, position);
            m_formScripts.BringToFront();
            m_formScripts.Activate();
        }

        public void ShowQuests(BaseCharacter character)
        {
            m_showNext = new ShowNext(ShowNextType.QuestWindow);
            // For now we track the active character instead of using the passed-in one
        }

        private void CreateWindow<T>(ref T form, WindowType type, bool bIgnoreAutoshow = false, object param = null) where T : HackerBasedForm, new()
        {
            bool bSetSplitters = false;
            WindowInfo info = Global.Windows.Get(type);
            if (form == null || form.IsDisposed)
            {
                form = new T();
                SetTaskbarWindow(form);
                form.StartPosition = FormStartPosition.Manual;
                if (info.NormalSize == Rectangle.Empty)
                    info.NormalSize = Global.GetCenterRect(form, Bounds);
                if (Properties.Settings.Default.PreventOffscreenWindows)
                {
                    if (!Global.AllowableWindowLocations.Contains(info.NormalSize.Location))
                        form.Location = Global.FixWindowLocation(info.NormalSize.Location);
                    else
                        form.Location = info.NormalSize.Location;
                }
                else
                    form.Location = info.NormalSize.Location;
                form.Size = info.NormalSize.Size;
                bSetSplitters = true;
            }

            if (bSetSplitters)
                form.Splitters = info.SplitPositions;
            form.SetParameter(param);
            form.SetMain(this, type);
            if (Properties.Settings.Default.PreventOffscreenWindows)
            {
                // If the window has already managed to show up off-screen and is simply being shown (not created), attempt to fix that
                if (!Global.AllowableWindowLocations.Contains(info.NormalSize.Location))
                    form.Location = Global.FixWindowLocation(info.NormalSize.Location);
            }
            if (info.Maximized)
                form.WindowState = FormWindowState.Maximized;
            SetTaskbarWindow(form);
            form.Owner = this;
            if (info.AutoShow && !bIgnoreAutoshow)
                form.Show();

            if (Global.FormVisible(form))
                EnforceFormZOrder(form, type);
        }

        private void CreateWindowNoShow<T>(ref T form, WindowType type, object param = null) where T : HackerBasedForm, new()
        {
            CreateWindow<T>(ref form, type, true, param);
        }

        private void ActivateWindow<T>(ref T form, WindowType type, object param = null) where T : HackerBasedForm, new()
        {
            m_bFocusingWindows = true;
            CreateOrShowWindow<T>(ref form, type, param);
            form.Activate();
            m_bFocusingWindows = false;
        }

        private void CreateOrShowWindow<T>(ref T form, WindowType type, object param = null) where T : HackerBasedForm, new()
        {
            CreateWindow<T>(ref form, type, false, param);

            form.Show();
            EnforceFormZOrder(form, type);
        }

        private void SetTaskbarWindow(Form form)
        {
            if (Properties.Settings.Default.SingleTaskbarWindow)
            {
                if (form.ShowInTaskbar)
                {
                    form.ShowInTaskbar = false;
                    form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
                }
            }
            else
            {
                if (!form.ShowInTaskbar)
                {
                    form.ShowInTaskbar = true;
                    form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                }
            }
        }

        private void EnforceFormZOrder(Form form, WindowType type)
        {
            if (Properties.Settings.Default.IgnoreZOrderList)
            {
                form.BringToFront();
                // Send all other forms below this one instead of only relying on BringToFront() as this is not always successful
                foreach (Form otherForm in Forms.Values)
                {
                    if (otherForm != form)
                        NativeMethods.SetWindowPos(otherForm.Handle, form.Handle, 0, 0, 0, 0, NativeMethods.SetWindowPosFlags.ZOnly);
                }
                return;
            }

            Dictionary<WindowType, Form> forms = GetFormsByType();
            
            // Insert this form after the nearest visible form above it in the Z-Order list
            IntPtr hwndAbove = IntPtr.Zero;
            WindowType[] types = Properties.Settings.Default.ZOrder.Types;
            if (types == null || types.Length < 2)
                types = EditZOrderForm.DefaultOrder;

            bool bFound = false;
            foreach (WindowType winType in types)
            {
                if (winType == type)
                {
                    bFound = true;
                    break;
                }
                if (forms.ContainsKey(winType))
                    hwndAbove = forms[winType].Handle;
            }
            if (hwndAbove == IntPtr.Zero || !bFound)
                form.BringToFront();
            else
                NativeMethods.SetWindowPos(form.Handle, hwndAbove, 0, 0, 0, 0, NativeMethods.SetWindowPosFlags.ZOnly);
        }

        private void MenuEditFind()
        {
            ShowSearch();
        }

        private void miDebugProgramInfo_Click(object sender, EventArgs e)
        {
            StringsViewForm form = new StringsViewForm();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("Config file (setup): {0}\r\n", AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            sb.AppendFormat("Config file (ConfigurationUserLevel.None): {0}\r\n", ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath);
            sb.AppendFormat("Config file (ConfigurationUserLevel.PerUserRoaming): {0}\r\n", ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoaming).FilePath);
            sb.AppendFormat("Config file (ConfigurationUserLevel.PerUserRoamingAndLocal): {0}\r\n", ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath);
            
            form.Strings = sb.ToString();
            form.ShowDialog();
        }

        public string GetMapName(int iIndex)
        {
            foreach (MapSheet sheet in m_mapBook.Sheets)
                if (sheet.GameMapIndex == iIndex)
                    return sheet.Title;
            return String.Empty;
        }

        private void MenuViewNoteTemplates()
        {
            NoteTemplatesForm form = new NoteTemplatesForm(Properties.Settings.Default.NoteTemplates);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.NoteTemplates = form.GetNoteTemplates();
                SetNoteTemplateMenu();
            }
        }

        private void MenuViewTriggers()
        {
            EditTriggerListForm form = new EditTriggerListForm();
            form.SetTriggerList(Properties.Settings.Default.Triggers);
            if (form.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.Triggers = form.GetTriggerList();
                m_formParty?.RevertTriggeredItems();
                m_formParty?.ForceUpdate();
                m_formQuickRef?.ForceUpdate();
                Properties.Settings.Default.Save();
            }
        }

        private void MenuViewZOrder()
        {
            EditZOrderForm form = new EditZOrderForm();
            form.WindowOrder = Properties.Settings.Default.ZOrder.Types;
            form.IgnoreList = Properties.Settings.Default.IgnoreZOrderList;
            if (form.ShowDialog() == DialogResult.OK)
            {
                Properties.Settings.Default.ZOrder = new WindowTypeList(form.WindowOrder);
                Properties.Settings.Default.IgnoreZOrderList = form.IgnoreList;
            }
        }

        private void MenuGameShowInfo()
        {
            if (!CheckHackerRunning(false))
                return;

            m_showNext = new ShowNext(ShowNextType.GameInfoWindow);
        }

        private void miGridHackBeacon_Click(object sender, EventArgs e)
        {
            if (m_hacker == null)
                return;
            m_hacker.SetBeacon(TranslateToGameMap(m_ptContextOpened), CurrentSheet.GameMapIndex);
        }

        private void miViewBringDOSBoxForeground_Click(object sender, EventArgs e)
        {
            if (CheckLastShortcut(Action.ViewBringDOSBoxToForeground))
                return;
            NativeMethods.SetForegroundWindow(m_hacker.DOSBoxWindow);
        }

        public void BringDOSBoxToFront()
        {
            m_bBringDOSBoxToFront = true;
        }

        private bool Timer_BringDOSBoxToFront()
        {
            if (!m_bBringDOSBoxToFront)
                return false;

            m_bBringDOSBoxToFront = false;

            if (m_hacker == null || m_hacker.DOSBoxWindow == IntPtr.Zero || !Properties.Settings.Default.FocusDOSBoxWhenSelected)
                return false;

            m_bFocusingWindows = true;

            m_timerFocusAll.Start();

            if (m_bDOSBoxAboveWAW)
                NativeMethods.SetForegroundWindow(m_hacker.DOSBoxWindow);
            else
                MakeSecondWindow(m_hacker.DOSBoxWindow);

            m_bDOSBoxAboveWAW = false;
            return true;
        }

        private void BringWindowToForeground(Form form)
        {
            if (!Global.FormVisible(form))
                return;
            if (form.Handle == IntPtr.Zero)
                return;

            NativeMethods.SetForegroundWindow(form.Handle);
        }

        private void MakeSecondWindow(Form form) { MakeSecondWindow(form.Handle); }

        private void MakeSecondWindow(IntPtr handle)
        {
            NativeMethods.SetWindowPos(handle, NativeMethods.GetForegroundWindow(), 0, 0, 0, 0, NativeMethods.SetWindowPosFlags.ZOnly);
        }

        private void MenuGameShowQuests()
        {
            if (!CheckHackerRunning(false))
                return;
            m_showNext = new ShowNext(ShowNextType.QuestWindow);
        }

        private void miGridHackSurface_Click(object sender, EventArgs e)
        {
            if (m_hacker == null)
                return;
            m_hacker.SetExit(new MMExit(MMExitDirection.Surface, CurrentSheet.GameMapIndex, TranslateToGameMap(m_ptContextOpened)));
        }

        private void miDebugReadMap1_Click(object sender, EventArgs e)
        {
            ReadMap16x16(MapReplaceMode.Full);
        }

        private void ReadMap32x32(MapReplaceMode mode)
        {
            if (m_hacker == null)
                return;
            ReadMapFromMemory(m_hacker.GetCurrentMapQuad(), mode);
        }

        private void ReadMap16x16(MapReplaceMode mode)
        {
            if (m_hacker == null)
                return;
            ReadMapFromMemory(m_hacker.GetCurrentMapQuad(), mode);
        }

        private void ReadMap20x20(MapReplaceMode mode)
        {
            if (m_hacker == null)
                return;
            ReadMapFromMemory(m_hacker.GetCurrentMapQuad(), mode);
        }

        private void ReadMapWiz5(MapReplaceMode mode)
        {
            if (m_hacker == null)
                return;
            ReadMapFromMemory(0, mode);
        }

        private void miDebugReadMapQuad_Click(object sender, EventArgs e)
        {
            ReadMapQuadFromMemory(MapReplaceMode.Full);
        }

        private void MenuGameShowShops()
        {
            if (!CheckHackerRunning(false))
                return;
            if (Global.FormVisible(m_formShopInventory))
            {
                m_formShopInventory.Close();
            }
            else
            {
                m_bShowShopInventories = true;
                Shops shops = m_hacker.GetShopInfo();
                if (shops == null)
                {
                    MessageBox.Show("There are no available shop inventories at the moment.  Please try again when you are purchasing items.", 
                        "No Shop Inventories", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (shops.InShop)
                {
                    // User asked to open the window while in a shop -> change auto-show settings
                    Properties.Settings.Default.ShowShopInventories = true;
                }

                ShowShopInventoryWindow();
            }
        }

        private void miGridSetBlockColor_Click(object sender, EventArgs e)
        {
            MapSquare square = CurrentSheet.GetSquareAtGridPoint(m_ptContextOpened);
            if (square == null)
                return;
            SelectBlockColor(new DrawColor(square.Colors.Background, square.Colors.BackgroundStyle));
        }

        private void miGameResetMonsters_Click(object sender, EventArgs e)
        {
            if (CheckLastShortcut(Action.GameResetMonstersOnMap))
                return;
            if (!Global.Cheats)
            {
                MessageBox.Show("Monsters can only be reset if cheating is enabled in the Options.", "Cheats Disabled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult result = DialogResult.Cancel;

            if (Games.IsWizardry(Game))
                result = MessageBox.Show("Reset all of the encounter squares on this map to \"Active\"?\r\n\r\n" + 
                    "(Note that this differs from reloading a level in-game, which only activates a random selection)", "Reset all monsters?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            else
                result = MessageBox.Show("Reset all of the monsters on this map to their default locations and stats?", "Reset all monsters?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result != DialogResult.OK)
                return;

            ResetMonsters();
        }

        private void ResetMonsters()
        {
            if (Global.Cheats)
                m_hacker.ResetMonsters();
        }

        private void miGridViewScripts_Click(object sender, EventArgs e)
        {
            m_showNext = new ShowNext(ShowNextType.ScriptsWindow);
            m_showNext.Position = TranslateToGameMap(m_ptContextOpened);
        }

        private void MenuGameScripts()
        {
            if (!CheckHackerRunning(false))
                return;
            m_showNext = new ShowNext(ShowNextType.ScriptsWindow);
            m_showNext.Position = TranslateToGameMap(m_lastLocation.PrimaryCoordinates);
        }

        private void MenuGameEditCartography()
        {
            if (!CheckHackerRunning(false))
                return;

            string strNoMap = null;
            if (!m_hacker.HasCartography)
                strNoMap = String.Format("\"{0}\" has no internal automap and thus no cartography data.", Games.Name(m_hacker.Game));
            else if (!m_hacker.HasCartographyOnMap())
                strNoMap = String.Format("\"{0}\" has no internal automap for the current area and thus no cartography data.", Games.Name(m_hacker.Game));

            if (!String.IsNullOrWhiteSpace(strNoMap))
            {
                MessageBox.Show(strNoMap + "  However, this program will keep track of the squares you have visited, and that data is saved with your .WAW file.  " +
                    "It can be cleared via the Options dialog, under the Maps tab.",
                    "No Cartography Data", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (!Global.Cheats)
            {
                MessageBox.Show("Cartography data can only be edited if cheating is enabled in the Options.", "Cheats Disabled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            EditCartographyForm form = new EditCartographyForm();
            form.SetMain(this, WindowType.EditCartography);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (Properties.Settings.Default.UseInGameCartography &&
                    Properties.Settings.Default.HideUnvisitedSquares)
                {
                    CurrentSheet.ResetVisited();
                    ForceRefreshOnDisplay();
                    if (Global.FormVisible(m_formEncounters))
                        m_formEncounters.UpdateUI();
                }
            }
        }

        private void miGameRemoveMonsters_Click(object sender, EventArgs e)
        {
            if (CheckLastShortcut(Action.GameRemoveAllMonstersFromMap))
                return;
            if (!Global.Cheats)
            {
                MessageBox.Show("Monsters can only be removed if cheating is enabled in the Options.", "Cheats Disabled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MessageBox.Show("Remove all of the monsters on this map?  This is different than killing the monsters normally and may have some unexpected effects.", "Remove All Monsters?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            RemoveMonsters();
        }

        private void RemoveMonsters()
        {
            if (Global.Cheats)
                m_hacker.KillAllMonsters();
        }

        private void miGridDebugReinterpret_Click(object sender, EventArgs e)
        {
            MapData data = null;

            if (Game == GameNames.Wizardry5)
                data = Hacker.GetMapData(false, 0);
            else if (Games.IsBardsTale(Game) || Games.IsEyeOfTheBeholder(Game) || Games.IsUltima(Game))
            {
                data = Hacker.GetMapData(false, 0);
            }
            else
            {
                MMMapData data1 = m_hacker.GetMapData(true, 0) as MMMapData;
                MMMapData data2 = m_hacker.GetMapData(true, 1) as MMMapData;
                MMMapData data3 = m_hacker.GetMapData(true, 2) as MMMapData;
                MMMapData data4 = m_hacker.GetMapData(true, 3) as MMMapData;

                if (data1 != null && data2 != null && data3 != null && data4 != null)
                {
                    if (data1 is MM3MapData)
                        data = MMMapData.CreateFromQuad<MM3MapData>(data1, data2, data3, data4);
                    else if (data1 is MM45MapData)
                        data = MMMapData.CreateFromQuad<MM45MapData>(data1, data2, data3, data4);
                    else
                        data = data1;   // MM1/2
                }
            }

            CurrentSheet.ReinterpretSquare(m_ptContextOpened, TranslateToGameMap(m_ptContextOpened), data, m_mapBook);
            SetDirty();
        }

        private void miDebugReadMap16x32_Click(object sender, EventArgs e)
        {
            ReadMap16x32(MapReplaceMode.Full);
        }

        private void ReadMap16x32(MapReplaceMode mode)
        {
            if (!m_hacker.Running)
                return;

            MMMapData data1 = m_hacker.GetMapData(true, 0) as MMMapData;
            MMMapData data2 = m_hacker.GetMapData(true, 2) as MMMapData;

            if (data1 != null && data2 != null)
            {
                MapSheet sheet = null;
                if (data1 is MM3MapData)
                    sheet = new MapSheet(m_mapBook, MMMapData.CreateFromPair<MM3MapData>(data1, data2, Orientation.Vertical));
                else if (data1 is MM45MapData)
                    sheet = new MapSheet(m_mapBook, MMMapData.CreateFromPair<MM45MapData>(data1, data2, Orientation.Vertical));
                else
                    sheet = new MapSheet(m_mapBook, MMMapData.CreateFromPair<MMMapData>(data1, data2, Orientation.Vertical));
                sheet.GameMapIndex = data1.Index;
                sheet.Title = data1.Title.Title;
                sheet.MenuPath = data1.Title.Path;
                sheet.DefaultZoom = 100;
                if (mode == MapReplaceMode.GridOnly)
                    CurrentSheet.CopyGridDataFrom(sheet, mode);
                else
                {
                    m_mapBook.Sheets.Add(sheet);
                    SelectSheet(m_mapBook.Sheets.Count - 1);
                }
                m_lastMonsterLocations = null;
                SetDirtyUnsaved();
            }
        }

        private void miDebugReadMap32x16_Click(object sender, EventArgs e)
        {
            ReadMap32x16(MapReplaceMode.Full);
        }

        private void ReadMap32x16(MapReplaceMode mode)
        {
            if (!m_hacker.Running)
                return;

            MMMapData data1 = m_hacker.GetMapData(true, 0) as MMMapData;
            MMMapData data2 = m_hacker.GetMapData(true, 1) as MMMapData;

            if (data1 != null && data2 != null)
            {
                MapSheet sheet = null;
                if (data1 is MM3MapData)
                    sheet = new MapSheet(m_mapBook, MMMapData.CreateFromPair<MM3MapData>(data1, data2, Orientation.Horizontal));
                else if (data1 is MM45MapData)
                    sheet = new MapSheet(m_mapBook, MMMapData.CreateFromPair<MM45MapData>(data1, data2, Orientation.Horizontal));
                else
                    sheet = new MapSheet(m_mapBook, MMMapData.CreateFromPair<MMMapData>(data1, data2, Orientation.Horizontal));
                sheet.GameMapIndex = data1.Index;
                sheet.Title = data1.Title.Title;
                sheet.MenuPath = data1.Title.Path;
                sheet.DefaultZoom = 100;
                if (mode == MapReplaceMode.GridOnly)
                    CurrentSheet.CopyGridDataFrom(sheet, mode);
                else
                {
                    m_mapBook.Sheets.Add(sheet);
                    SelectSheet(m_mapBook.Sheets.Count - 1);
                }
                m_lastMonsterLocations = null;
                SetDirtyUnsaved();
            }
        }

        private void miDebugReadMapAuto_Click(object sender, EventArgs e)
        {
            ReadAutosizeMap(sender, e, MapReplaceMode.Full);
        }

        private void ReadAutosizeMap(object sender, EventArgs e, MapReplaceMode mode)
        {
            if (!m_hacker.Running)
                return;

            Size sz = m_hacker.GetCurrentMapDimensions();

            if (m_hacker is EOBMemoryHacker)
                ReadMap32x32(mode);
            if (m_hacker is Wiz5MemoryHacker)
                ReadMapWiz5(mode);  // Wiz5 has variable-size maps
            else if (sz.Width == 32 && sz.Height == 32)
                ReadMapQuadFromMemory(mode);
            else if (sz.Width == 32 && sz.Height == 16)
                ReadMap32x16(mode);
            else if (sz.Width == 16 && sz.Height == 32)
                ReadMap16x32(mode);
            else if (sz.Width == 20 && sz.Height == 20)
                ReadMap20x20(mode);
            else
                ReadMap16x16(mode);
            m_lastActiveSquares = null;
        }

        private void miGridCopyLocation_Click(object sender, EventArgs e)
        {
            CopyLocationText(TranslateToGameMap(m_ptContextOpened));
        }

        public void CopyLocationText(Point pt, MapSheet sheet = null)
        {
            if (sheet == null)
                sheet = CurrentSheet;
            Clipboard.SetText(GetLocationText(pt, sheet.GameMapIndex, sheet.Title));
        }

        public string GetLocationText(Point pt, int index, string strTitle)
        {
            StringBuilder sb = new StringBuilder(Properties.Settings.Default.CopyLocationFormat);
            sb.Replace("{Title}", strTitle);
            sb.Replace("{MapIndex}", String.Format("{0}", index));
            sb.Replace("{Abbr}", Regex.Replace(strTitle, @"[\-, 'a-z]", ""));
            if (m_hacker != null)
                sb.Replace("{Enum}", m_hacker.GetMapEnum(index));
            sb.Replace("{x}", String.Format("{0}", pt.X));
            sb.Replace("{y}", String.Format("{0}", pt.Y));
            sb.Replace("{xx}", String.Format("{0:D2}", pt.X));
            sb.Replace("{yy}", String.Format("{0:D2}", pt.Y));
            return sb.ToString();
        }

        private void miDebugDumpScripts_Click(object sender, EventArgs e)
        {
            ScriptInfo info = m_hacker.GetScriptInfo();
            if (info == null || info.Scripts == null)
            {
                MessageBox.Show("No scripts were returned from the memory hacker", "No Scripts to Dump");
                return;
            }

            Dictionary<int, int> CDCommands = new Dictionary<int, int>();

            StringBuilder sb = new StringBuilder();
            foreach (Point pt in info.Scripts.Scripts.Keys)
            {
                List<GameScript> list = info.Scripts.Scripts[pt];
                foreach (GameScript script in list)
                {
                    foreach(ScriptLine line in script.Lines)
                    {
                        if (line.CommandBytes != null && line.CommandBytes.Length > 0 && line.CommandBytes[0] == 0x3C && !CDCommands.ContainsKey(line.Address))
                            CDCommands.Add(line.Address, line.Bytes.Length);
                        sb.AppendLine(String.Format("{0}\t{1}\t{2}\t{3}\t{4}", m_hacker.GetMapTitle(m_hacker.GetCurrentMapIndex()),
                            line.Location.X, line.Location.Y, line.Number, line.Description(info, "")));
                    }
                }
            }

            StringBuilder sbFileName = new StringBuilder(Environment.ExpandEnvironmentVariables(Properties.Settings.Default.ScriptsDumpFile));
            sbFileName.Replace("{Enum}", m_hacker.GetMapEnum(m_hacker.GetCurrentMapIndex()));

            File.AppendAllText(sbFileName.ToString(), sb.ToString());

            if (CDCommands.Count > 0)
            {
                sb.Clear();
                sb.Append("\r\n\r\nCD Commands:\r\n");
                foreach (int address in CDCommands.Keys)
                {
                    sb.AppendFormat("{0}: {1}\r\n", address, CDCommands[address]);
                }
                File.AppendAllText(sbFileName.ToString(), sb.ToString());
            }
        }

        private void MenuGameQuickRef()
        {
            if (!CheckHackerRunning(false))
                return;
            ShowQuickRefNext();
        }

        private void AddUndoEntireGrid()
        {
            m_undoBlocks = CurrentSheet.StartUndoBlock().Squares;
            for (int x = 0; x < CurrentSheet.GridWidth; x++)
            {
                for (int y = 0; y < CurrentSheet.GridHeight; y++)
                    m_undoBlocks.Add(CurrentSheet.UndoSquare(x, y));
            }
        }

        private void miDebugReplaceGrid_Click(object sender, EventArgs e)
        {
            if (CurrentSheet == null)
                return;
            CurrentSheet.ClearRedo();
            AddUndoEntireGrid();

            ReadAutosizeMap(sender, e, MapReplaceMode.GridOnly);
        }

        private void miDebugReplaceIcons_Click(object sender, EventArgs e)
        {
            if (CurrentSheet == null)
                return;
            CurrentSheet.ClearRedo();
            m_undoBlocks = CurrentSheet.StartUndoBlock().Squares;
            AddUndoEntireGrid();
            CurrentSheet.AddUndoIcons();
            ReadAutosizeMap(sender, e, MapReplaceMode.WallIcons | MapReplaceMode.Lines);
        }

        private bool LaunchCurrentGame(bool bSkipAutoshowWindows = false)
        {
            if (!CheckMapsUnlocked())
                return false;

            if (Properties.Settings.Default.Game == GameNames.None)
            {
                MessageBox.Show("There is no game selected to launch!\r\nPlease check the settings on the Play tab of the Options dialog and try again.",
                    "No game selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            m_bOverrideAutoSwitch = true;
            m_iLegendHoldIndex = -1;

            if (m_hacker == null)
                CreateHacker(Properties.Settings.Default.Game, true, bSkipAutoshowWindows);

            if (m_hacker == null)
            {
                MessageBox.Show(String.Format("Could not create the memory scanner for game: {0}.\r\nPlease check the settings on the Play tab of the Options dialog and try again.",
                    Games.Name(Properties.Settings.Default.Game)),
                    "Error launching game", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!m_hacker.Running)
                LaunchGame(Properties.Settings.Default.Game);
            else
            {
                m_showNext = new ShowNext(String.Format("DOSBox appears to already be running \"{0}\", so it will not be launched again.\r\n\r\n" +
                "If this is not correct, please check the titles under the \"DOSBox\" tab of the Options.",
                    Games.Name(Properties.Settings.Default.Game)), "Already Running", MessageBoxIcon.Information);
                if (Hacker == null || !Hacker.IsValid)
                    SetLaunchFinished(true);   // Will restart the memory hacker
                else
                    CheckAutoShowWindows();
            }
            return true;
        }

        private void miGameLaunchCurrent_Click(object sender, EventArgs e)
        {
            if (CheckLastShortcut(Action.GameLaunchCurrentGame))
                return;
            LaunchCurrentGame();
        }

        private void MenuGameShowItems()
        {
            if (!CheckHackerRunning(false))
                return;

            if (Global.FormVisible(m_formItems))
            {
                m_formItems.Close();
                return;
            }
            ActivateWindow<ItemsForm>(ref m_formItems, WindowType.Items);
            m_formItems.SetMain(this, WindowType.Items);
            m_formItems.Show();
        }

        private void miGridDebugCartography_Click(object sender, EventArgs e)
        {
            Hacker.ToggleCartography(TranslateToGameMap(m_ptContextOpened), Toggle.Toggle);
            if (Global.FormVisible(m_formEncounters))
                m_formEncounters.UpdateUI();
        }

        public bool HideMonsters(List<Monster> monsters)
        {
            if (!Properties.Settings.Default.HideScriptMonsters)
                return false;
            return monsters.All(m => Hacker.IsScriptBitMonster(m.EncounterIndex));
        }

        private void miViewAutoArrange_Click(object sender, EventArgs e)
        {
            if (CheckLastShortcut(Action.ViewAutoArrange))
                return;
            AutoArrangeWindows();
        }

        private void AutoArrangeWindows(bool bSilent = false)
        {
            if (Hacker == null || !Hacker.IsValid)
            {
                if (!bSilent)
                    MessageBox.Show("The auto-arrange function requires that a known DOSBox game is running (check the \"Play\" tab of the Options window)",
                        "Memory scanner not running", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Arrange the primary windows on the primary display using the following conventions:

            // DOSBox window: Upper-left corner of the desktop
            // MainForm: Directly to the right of the DOSBox
            // Party window: Directly below DOSBox, set to the width of the DOSBox, and height as large as the rest of the desktop height
            // Game Info window: To the right of the Main Form, if available, and width as large as the rest of the desktop width
            // Encounter window: Below and the same width as the Main Form, and height as large as the rest of the desktop height

            // Other windows are not auto-arranged, but the positions and sizes are saved as part of the user preferences (as are the ones listed above)
            bool bExtended = Properties.Settings.Default.UseExtendedWindowRect;

            Rectangle rcPrimary = Screen.PrimaryScreen.WorkingArea;
            Rectangle rcDOS = Hacker.GetDOSBoxRect();
            Rectangle rcExtDOS = bExtended ? NativeMethods.GetExtendedWindowRect(Hacker.DOSBoxWindow) : rcDOS;
            Rectangle rcExtMain = bExtended ? NativeMethods.GetExtendedWindowRect(Handle) : Bounds;
            ExpandSizes es = Properties.Settings.Default.WindowSnapMargins;

            if (rcDOS.Width < 100 || rcDOS.Height < 100)
            {
                if (!bSilent)
                    MessageBox.Show(String.Format("A DOSBox window was located but its size is invalid ({0}x{1}).\r\n\r\n" +
                        "Please be sure that the game is running properly.", rcDOS.Width, rcDOS.Height),
                        "DOSBox Window Size Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rcDOS == Screen.PrimaryScreen.Bounds)
            {
                if (!bSilent)
                    MessageBox.Show(String.Format("DOSBox appears to be full-screen, so the auto-arrange function is not available."),
                        "DOSBox Window Size Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (rcExtDOS.Location != rcPrimary.Location)
            {
                Hacker.SetDOSBoxPosition(new Point(rcPrimary.Left - (rcExtDOS.Left - rcDOS.Left), rcPrimary.Top - (rcExtDOS.Top - rcDOS.Top)));
                Thread.Sleep(300);   // Let DOSBox finish moving before we put our window to the right of it
            }
            Location = new Point(rcExtDOS.Right - (rcExtMain.Left - Bounds.Left) - es.Left, rcExtDOS.Top - es.Top);
            FitWindow(18,18,200);   // Appropriate for 16x16 maps with a 1-square border (also 32x32 maps at half-zoom)
            Application.DoEvents();
            rcExtMain = bExtended ? NativeMethods.GetExtendedWindowRect(Handle) : Bounds;
            if (m_formInfo != null && !m_formInfo.IsDisposed)
            {
                Rectangle rcInfo = m_formInfo.Bounds;
                Rectangle rcExtendedInfo = bExtended ? NativeMethods.GetExtendedWindowRect(m_formInfo.Handle) : rcInfo;
                m_formInfo.Location = new Point(rcExtMain.Right - (rcExtendedInfo.Left - rcInfo.Left) - es.Right, rcExtMain.Top - (rcExtendedInfo.Top - rcInfo.Top) - es.Bottom);
                m_formInfo.Width = Math.Min(rcPrimary.Right - Bounds.Right, 350);
                m_formInfo.Height = Height;
                Application.DoEvents();
            }
            if (m_formEncounters != null && !m_formEncounters.IsDisposed)
            {
                Rectangle rcEnc = m_formEncounters.Bounds;
                Rectangle rcExtEnc = bExtended ? NativeMethods.GetExtendedWindowRect(m_formEncounters.Handle) : rcEnc;
                m_formEncounters.Location = new Point(rcExtMain.Left - (rcExtEnc.Left - rcEnc.Left) - es.Right, rcExtMain.Bottom - (rcExtEnc.Top - rcEnc.Top) - es.Bottom);
                m_formEncounters.Width = Width;
                m_formEncounters.Height = rcPrimary.Bottom - Bottom;
                Application.DoEvents();
            }
            if (m_formParty != null && !m_formParty.IsDisposed)
            {
                Rectangle rcParty = m_formParty.Bounds;
                Rectangle rcExtParty = bExtended ? NativeMethods.GetExtendedWindowRect(m_formParty.Handle) : rcParty;
                m_formParty.Location = new Point(rcExtDOS.Left - (rcExtParty.Left - rcParty.Left) - es.Right, rcExtDOS.Bottom - (rcExtParty.Top - rcParty.Top) - es.Bottom);
                m_formParty.Width = rcExtDOS.Width + (rcParty.Width - rcExtParty.Width);
                m_formParty.Height = rcPrimary.Bottom - rcDOS.Bottom;
                Application.DoEvents();
            }
            Properties.Settings.Default.NeedAutoArrange = false;
        }

        private void MenuHelpRunWizard()
        {
            RunWizard(1);
        }

        private void UpdateScrollSizes(ScrollableControl ctrl)
        {
            ctrl.AutoScroll = true;
        }

        private void miSelectionRemove_Click(object sender, EventArgs e)
        {
            CancelSelection();
        }

        private void miSelectionCut_Click(object sender, EventArgs e)
        {
            MenuEditCut();
        }

        private void miSelectionCopy_Click(object sender, EventArgs e)
        {
            MenuEditCopy();
        }

        private void miSelectionDelete_Click(object sender, EventArgs e)
        {
            MenuEditDelete();
        }

        private void miCtxModePlay_Click(object sender, EventArgs e)
        {
            MenuModePlay();
        }

        private void miCtxModeBlock_Click(object sender, EventArgs e)
        {
            MenuModeBlock();
        }

        private void miCtxModeLine_Click(object sender, EventArgs e)
        {
            MenuModeLine();
        }

        private void miCtxModeHybrid_Click(object sender, EventArgs e)
        {
            MenuModeHybrid();
        }

        private void miCtxModeNotes_Click(object sender, EventArgs e)
        {
            MenuModeNotes();
        }

        private void miCtxModeKeyboard_Click(object sender, EventArgs e)
        {
            MenuModeKeyboard();
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            MenuModeEdit();
        }

        private void miSelectionPaste_Click(object sender, EventArgs e)
        {
            MenuEditPaste();
        }

        private void cmSelection_Opening(object sender, CancelEventArgs e)
        {
            UpdatePasteMenu(miSelectionPaste);
        }

        private void miEditUnclear_Click(object sender, EventArgs e)
        {
            if (m_noteCanceled == null)
                return;

            BeginEditNote(m_noteCanceled.Location);
            tbSymbol.Text = m_noteCanceled.Symbol;
            tbSymbol.ForeColor = m_noteCanceled.Color;
            tbNote.Text = m_noteCanceled.Text;
            pbSelectColor.BackColor = m_noteCanceled.Color;

            m_noteCanceled = null;
        }

        private void Rotate(RotateFlipType rotate)
        {
            CurrentSheet.ClearRedo();
            m_undoBlocks = CurrentSheet.StartUndoBlock().Squares;
            CurrentSheet.RotateFlipBlocks(m_undoBlocks, rotate, m_rcSelection, Global.EditSettings);
            SetDirty(m_rcSelection, true);
            switch (rotate)
            {
                case RotateFlipType.Rotate90FlipNone:
                case RotateFlipType.Rotate270FlipNone:
                    m_rcSelection = new Rectangle(m_rcSelection.X, m_rcSelection.Y, m_rcSelection.Height, m_rcSelection.Width);
                    break;
                default:
                    break;
            }
            m_ptLastSquare = Global.NullPoint;
            m_bRecaptureSelection = true;
            SetDirtyUnsaved();
        }

        private void SetSelectionRecapture()
        {
            SetDirty(m_rcSelection, true);
            m_ptLastSquare = Global.NullPoint;
            m_bRecaptureSelection = true;
        }

        private void MenuEditRotateLeft()
        {
            Rotate(RotateFlipType.Rotate270FlipNone);
        }

        private void MenuEditRotateRight()
        {
            Rotate(RotateFlipType.Rotate90FlipNone);
        }

        private void MenuEditRotate180()
        {
            Rotate(RotateFlipType.Rotate180FlipNone);
        }

        private void MenuEditFlipHoriz()
        {
            Rotate(RotateFlipType.RotateNoneFlipX);
        }

        private void MenuEditFlipVert()
        {
            Rotate(RotateFlipType.RotateNoneFlipY);
        }

        private void MenuFileExportPng()
        {
            if (threadLoadFile != null && threadLoadFile.IsAlive)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Portable Network Graphic|*.png|All Files|*.*";
            sfd.FileName = Global.SanitizeFilename(CurrentSheet.Title + ".png");
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                bool bRetry = true;
                while (bRetry)
                {
                    try
                    {
                        pbMain.Image.Save(sfd.FileName, ImageFormat.Png);
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("The file \"" + sfd.FileName + "\" could not be saved.\r\n\r\nException:  " + ex.Message, "Error saving file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Retry)
                            bRetry = false;
                    }
                }
            }
        }

        private void MenuFileExportZip()
        {
            if (threadLoadFile != null && threadLoadFile.IsAlive)
                return;

            if (m_bExportingMaps)
                return;

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ZIP Archive|*.zip|All Files|*.*";
            sfd.FileName = Global.SanitizeFilename(m_mapBook.Title + ".zip");
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                bool bRetry = true;
                while (bRetry)
                {
                    try
                    {
                        ExportBook(sfd.FileName);
                        break;
                    }
                    catch (Exception ex)
                    {
                        if (MessageBox.Show("The file \"" + sfd.FileName + "\" could not be saved.\r\n\r\nException:  " + ex.Message, "Error saving file", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) != DialogResult.Retry)
                            bRetry = false;
                    }
                }
            }
        }

        private void ExportFinished()
        {
            if (m_formWait != null && !m_formWait.IsDisposed)
                m_formWait.Close();
            m_bExportingMaps = false;
            m_bAbortExport = false;
            labelLoadingMap.Visible = false;
            ForceRefreshOnDisplay();
        }

        void m_formWait_OnAbortExport(object sender, EventArgs e)
        {
            m_bAbortExport = true;
            if (m_threadExport != null)
            {
                m_threadExport.Join(300);
                if (m_threadExport.IsAlive)
                    m_threadExport.Abort();
            }
            else
                ExportFinished();
        }

        private void ExportBookThread(object oFile)
        {
            string strZIP = (string)oFile;
            bool bDelete = false;
            try
            {
                FileStream fs = new FileStream(strZIP, FileMode.Create);
                using (ZipArchive archive = new ZipArchive(fs, ZipArchiveMode.Create))
                {
                    int iCount = 0;
                    foreach (MapSheet sheet in m_mapBook.Sheets)
                    {
                        string strPath = sheet.MenuPath.Replace('/', '.');
                        ZipArchiveEntry entry = archive.CreateEntry(Global.SanitizeFilename(String.Format(
                            "{0}{1}{2}.png", strPath, String.IsNullOrWhiteSpace(strPath) ? "" : ".", sheet.Title)));
                        using (Stream stream = entry.Open())
                        {
                            Size sz = sheet.SquareSize;
                            sheet.SetZoom(sheet.DefaultZoom);
                            DrawParams dp = new DrawParams(false, false, false, m_mapBook.UnvisitedPattern, false, false);
                            Bitmap bmp = sheet.CreateImage(this, dp);
                            sheet.SquareSize = sz;
                            bmp.Save(stream, ImageFormat.Png);
                        }
                        if (m_evtShutdown.WaitOne(0) || m_bAbortExport)
                        {
                            bDelete = true;
                            break;
                        }
                        if (m_formWait != null && !m_formWait.IsDisposed)
                            Invoke((MethodInvoker)(() => m_formWait.SetWaitText(String.Format("Exporting maps: {0}/{1}", iCount++, m_mapBook.Sheets.Count))));
                    }
                }
            }
            catch (ThreadAbortException)
            {
                bDelete = true;
            }
            catch (Exception ex)
            {
                BeginInvoke(new ErrorDelegate(CaughtThreadException), String.Format("The file \"{0}\" could not be written.\r\nException: {1}", (string)strZIP, ex.Message), "Error writing file");
            }
            finally
            {
                if (bDelete)
                    File.Delete(strZIP);
                BeginInvoke(new VoidDelegate(ExportFinished));
            }
        }

        private void ExportBook(string strZIP)
        {
            pbMain.Image = null; // Prevent OnPaint from having problems due to the thread accessing the sheet images
            labelLoadingMap.Text = "Exporting maps, please wait...";
            labelLoadingMap.Visible = true;
            labelLoadingMap.BringToFront();
            m_bExportingMaps = true;
            m_formWait = new WaitForm();
            m_formWait.SetAbort(new EventHandler(m_formWait_OnAbortExport));
            m_formWait.SetWaitText(String.Format("Exporting maps: 0/{0}", m_mapBook.Sheets.Count));
            ParameterizedThreadStart ts = new ParameterizedThreadStart(ExportBookThread);
            m_threadExport = new Thread(ts);
            m_threadExport.Start(strZIP);
            m_formWait.ShowDialog();
        }

        private void menuGame_DropDownOpening(object sender, EventArgs e)
        {
            miGameLaunchCurrent.Enabled = !m_bExportingMaps;
            miGameShowEncounters.Checked = Properties.Settings.Default.ShowEncounters;
            string strShort = Games.ShortName(Game);
            if (String.IsNullOrWhiteSpace(strShort))
                miGameLaunchCurrent.Text = "&Launch game (not set)";
            else
                miGameLaunchCurrent.Text = String.Format("&Launch \"{0}\"", Games.ShortName(Game));
        }

        private void miCtxModeFill_Click(object sender, EventArgs e)
        {
            MenuModeFill();
        }

        public bool HasSelection { get { return m_rcSelection != Rectangle.Empty; } }

        private void MenuEditConvertHalf()
        {
            CurrentSheet.ClearRedo();
            m_undoBlocks = CurrentSheet.StartUndoBlock().Squares;
            Rectangle rc = m_rcSelection == Rectangle.Empty ? CurrentSheet.GridRectangle : m_rcSelection;
            CurrentSheet.ConvertHalfLines(m_undoBlocks, rc);
            SetSelectionRecapture();
            SetDirtyUnsaved();
        }

        public void MenuSheetClone()
        {
            AddNewSheet(CurrentSheet);
        }

        public MapSheet AddNewSheet(MapSheet sheetCopy = null)
        {
            if (!CheckMapsUnlocked())
                return null;

            if (Properties.Settings.Default.ReadOnlyMaps)
            {
                ShowReadOnlyMapMessage();
                return null;
            }

            if (EditingNote)
                FinishNote();

            if (sheetCopy == null)
                m_mapBook.Sheets.Add(new MapSheet(m_mapBook.GridLines));
            else
                m_mapBook.Sheets.Add(new MapSheet(sheetCopy));
            SelectSheet(m_mapBook.Sheets.Count - 1);
            SetDirtyUnsaved();
            m_bMapMenuDirty = true;

            return CurrentSheet;
        }

        public void MenuEditFillBlocks()
        {
            CurrentSheet.ClearRedo();
            m_undoBlocks = CurrentSheet.StartUndoBlock().Squares;
            Rectangle rc = m_rcSelection == Rectangle.Empty ? CurrentSheet.GridRectangle : m_rcSelection;
            CurrentSheet.FillBlocks(m_undoBlocks, rc, m_dcCurrentBlock.BlockStyle);
            SetSelectionRecapture();
            SetDirtyUnsaved();
        }

        public void MenuEditOutline()
        {
            CurrentSheet.ClearRedo();
            m_undoBlocks = CurrentSheet.StartUndoBlock().Squares;
            Rectangle rc = m_rcSelection == Rectangle.Empty ? CurrentSheet.GridRectangle : m_rcSelection;
            CurrentSheet.Outline(m_undoBlocks, rc, m_dcCurrentLine.LineStyle);
            SetSelectionRecapture();
            SetDirtyUnsaved();
        }

        public void MenuEditLiveSquares()
        {
            m_bEditingLiveSquares = !m_bEditingLiveSquares;
            ForceRefreshOnDisplay();
            SetDirty();
        }

        public void MenuSheetGoto()
        {
            Global.Windows.Set(WindowType.SheetSelector, new WindowInfo(new Rectangle(Location.X, Location.Y + Height, Width, SheetSelectorForm.NormalHeight)));
            ActivateWindow<SheetSelectorForm>(ref m_formSelectSheet, WindowType.SheetSelector);
        }

        public void SelectSheetByPartialName(string strName, bool bNextIfCurrent, bool bReverse)
        {
            int iDelta = bReverse ? -1 : 1;
            string strLower = strName.ToLower();
            int iStart = m_iCurrentSheet;
            if (CurrentSheet.Title.ToLower().Contains(strLower) && bNextIfCurrent)
                iStart += iDelta;

            if (iStart >= m_mapBook.Sheets.Count)
                iStart = 0;
            if (iStart < 0)
                iStart = m_mapBook.Sheets.Count - 1;

            int i = iStart;
            do
            {
                if (m_mapBook.Sheets[i].Title.ToLower().Contains(strLower))
                {
                    SelectSheet(i);
                    break;
                }
                i += iDelta;
                if (i >= m_mapBook.Sheets.Count)
                    i = 0;
                if (i < 0)
                    i = m_mapBook.Sheets.Count - 1;
            } while (i != iStart);
        }

        public void SuspendActivation() { m_bFocusingWindows = true; }
        public void ResumeActivation() { m_bFocusingWindows = false; }

        public void UseNoteTemplate(int iIndex)
        {
            if (CurrentSheet == null || !CurrentSheet.PointInGrid(CurrentSheet.Cursor))
                return;

            if (iIndex >= Properties.Settings.Default.NoteTemplates.Count)
                return;

            NoteTemplateTag tag = new NoteTemplateTag(Properties.Settings.Default.NoteTemplates[iIndex]);

            AddNoteToMap(CurrentSheet.Cursor, tag.Symbol, tag.FinalText);
            SetNoteText(CurrentSheet.Cursor);
        }

        private void tsEdit_ClientSizeChanged(object sender, EventArgs e)
        {
            AdjustTitleBox();
        }

        private void AdjustTitleBox()
        {
            if (tsEdit.Orientation == Orientation.Vertical)
                return;

            Int32 width = tsEdit.DisplayRectangle.Width;

            if (tsEdit.OverflowButton.Visible)
                width = width - tsEdit.OverflowButton.Width - tsEdit.OverflowButton.Margin.Horizontal;

            foreach (ToolStripItem item in tsEdit.Items)
            {
                if (item.IsOnOverflow) continue;

                if (item == tseTitle)
                    width -= item.Margin.Horizontal;
                else
                    width = width - item.Width - item.Margin.Horizontal;
            }

            if (width < 100)
                width = 100;

            tseTitle.Width = width;
        }

        private void MenuSheetLabels()
        {
            ShowLabelsWindow();
        }

        private void ShowLabelsWindow(PointF ptSelect)
        {
            ActivateWindow<EditLabelsForm>(ref m_formEditLabels, WindowType.EditLabels);
            m_formEditLabels.SetNextSelectedLabel(CurrentSheet.Labels.ContainsKey(ptSelect) ? CurrentSheet.Labels[ptSelect] : null);
            m_formEditLabels.SetLabels(CurrentSheet.Labels);
        }

        private void ShowLabelsWindow(MapLabel label)
        {
            ActivateWindow<EditLabelsForm>(ref m_formEditLabels, WindowType.EditLabels);
            m_formEditLabels.SetNextSelectedLabel(label);
            m_formEditLabels.SetLabels(CurrentSheet.Labels);
        }

        private void ShowLabelsWindow()
        {
            ShowLabelsWindow(Global.NullPoint);
        }

        public void SetCurrentSheetLabels(MapLabels labels)
        {
            if (CurrentSheet == null)
                return;

            if (CurrentSheet.ChangeLabels(labels))
                SetDirtyUnsaved(false);
        }

        private void miGridAddLabel_Click(object sender, EventArgs e)
        {
            if (CurrentSheet == null)
                return;

            if (m_labelAtCursor != null)
            {
                ShowLabelsWindow(m_labelAtCursor);
            }
            else
            {
                PointF pt = new PointF(m_ptCursorAtContext.X / (float)CurrentSheet.SquareSize.Width, m_ptCursorAtContext.Y / (float)CurrentSheet.SquareSize.Height);
                pt.X = (float)Math.Round(pt.X, 2);
                pt.Y = (float)Math.Round(pt.Y, 2);
                CurrentSheet.ClearRedo();
                CurrentSheet.AddUndoLabels();
                if (CurrentSheet.AddLabel(pt, "New Label"))
                {
                    ShowLabelsWindow(pt);
                    SetDirtyUnsaved();
                }
            }
        }

        public void SetCurrentSheetSelectedLabels(HashSet<PointF> labels)
        {
            if (CurrentSheet == null)
                return;

            if (CurrentSheet.SetSelectedLabels(labels))
                SetDirty();
        }

        private void pbMain_DoubleClick(object sender, EventArgs e)
        {
            if (m_labelAtCursor != null)
            {
                if (EditingNote)
                    FinishNote();
                ShowLabelsWindow(m_labelAtCursor);
            }
            else if (Mode != BlockMode.Edit)
            {
                Point ptNote = GetSquareLocationAtMouse();
                if (SquareIsVisible(ptNote) || Properties.Settings.Default.ShowUnvisitedNotes)
                    BeginEditNote(ptNote);
            }
        }

        private void tbNote_MouseEnter(object sender, EventArgs e)
        {
            if (!EditingNote)
                SetNoteText(CurrentNotePoint);
        }

        private void tbNote_MouseMove(object sender, MouseEventArgs e)
        {
            if (!EditingNote)
                SetNoteText(CurrentNotePoint);
        }

        private void tbPartyLocation_TextChanged(object sender, EventArgs e)
        {
            tbParty2.Text = tbPartyLocation.Text;
        }

        private void tbLocation_TextChanged(object sender, EventArgs e)
        {
            tbCursor2.Text = tbLocation.Text;
        }

        private void miViewNotesPanel_Click(object sender, EventArgs e)
        {
            ShowNotesPanel(!miViewNotesPanel.Checked);
        }

        public bool CureAll(int iAddress, bool bSilent)
        {
            if (Hacker == null)
                return false;

            if (!Properties.Settings.Default.EnableMemoryWrite)
            {
                if (!bSilent)
                    MessageBox.Show("The Cure-All function cannot be used unless \"Enable writing to game memory\" is set in the options.",
                        "Memory write disabled", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            // Use the spell points of the caster to remove conditions from the characters (except Eradicated), if possible

            /*
            -  Must be paladin/cleric/druid/ranger
            -  Must not be in combat
            -  Must not be in an anti-magic zone
            -  Caster must not be dead, unconscious, paralyzed, stone, or eradicated
            -  Must have sufficient spell level, spell points and gems:
                6+4G and spell level 6 for each dead character
                6+4G and spell level 6 for each stoned character
                4 for each poisoned character
                4 for each diseased character
                3 for each blinded character
                3 for each paralyzed character
                1 to cure everyone of sleep
            */

            int[] partyAddresses = Hacker.GetPartyInfo().Addresses;

            CureAllInfo info = Hacker.GetCureAllInfo(iAddress, partyAddresses);

            if (info == null || !info.Valid)
            {
                if (!bSilent)
                    MessageBox.Show("Could not read the game memory", "Memory Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (info.Combat)
            {
                if (!bSilent)
                    MessageBox.Show(String.Format("Can't cure all in {0}!", info.CombatString), String.Format("In {0}!", info.CombatString), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!info.IsHealer)
            {
                if (!bSilent)
                    MessageBox.Show("Must be able to cast Cleric spells!", "Wrong Class!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!info.MagicPermitted)
            {
                if (!bSilent)
                    MessageBox.Show(this, "Magic doesn't work here!", "Anti-Magic Zone!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (info.IsIncapacitated)
            {
                if (!bSilent)
                    MessageBox.Show("Caster cannot cast spells; check condition!", "Can't cast!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            CureAllResult result = Hacker.CureAll(info);

            string strFail = "Cure-All did not succeed.\r\n\r\n";

            switch (result)
            {
                case CureAllResult.NoGems:
                    if (!bSilent)
                        MessageBox.Show(strFail + "Caster doesn't have enough gems to fix all of the conditions!", "Insufficient Gems", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case CureAllResult.NoSpellLevel:
                    if (!bSilent)
                        MessageBox.Show(strFail + "Caster doesn't have a high enough spell level to fix all of the conditions!", "Insufficient Spell Level", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case CureAllResult.NoSpellPoints:
                    if (!bSilent)
                        MessageBox.Show(strFail + "Caster doesn't have enough spell points to fix all of the conditions!", "Insufficient SP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case CureAllResult.SpellNotKnown:
                    if (!bSilent)
                        MessageBox.Show(strFail + "Caster doesn't know all of the spells necessary to fix all of the conditions!", "Unknown Spells", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case CureAllResult.MonstersNearby:
                    if (!bSilent)
                        MessageBox.Show(strFail + "There are active monsters too close to the party!", "Monsters Nearby", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    break;
            }

            if (result != CureAllResult.MonstersNearby)
            {
                Hacker.SetCureAllInfo(info, iAddress, partyAddresses);
                if (Properties.Settings.Default.RefreshDOSBox)
                    Hacker.RefreshConditions();
            }

            return result == CureAllResult.Success;
        }

        void m_formWait_OnAbortIntro(object sender, EventArgs e)
        {
            if (m_threadSkipIntros != null)
                m_threadSkipIntros.Abort();
            m_threadSkipIntros = null;
            BeginInvoke(new VoidDelegate(SkipIntrosFinished));
        }

        public void SkipIntrosThread()
        {
            try
            {
                int iIntroTimeout = Hacker.IntroTimeout;
                Hacker.SkipIntroductions(iIntroTimeout + ((iIntroTimeout * 10) / Properties.Settings.Default.SkipIntroTimingTweak));
            }
            catch (ThreadAbortException)
            {
            }
            if (!m_bShuttingDown)
                BeginInvoke(new VoidDelegate(SkipIntrosFinished));
        }

        public void SkipIntrosFinished()
        {
            if (Global.FormVisible(m_formWait))
                m_formWait.Close();
        }

        public void SkipIntroductions(bool bBackground = true)
        {
            if (Hacker != null)
            {
                if (bBackground)
                {
                    if (Global.FormVisible(m_formWait))
                        m_formWait.Close();

                    m_formWait = new WaitForm();
                    m_formWait.SetAbort(m_formWait_OnAbortIntro);
                    m_formWait.SetWaitText("Skipping introductions...");
                    m_formWait.Show();
                    m_formWait.Select();

                    if (m_threadSkipIntros != null && m_threadSkipIntros.IsAlive)
                        m_threadSkipIntros.Abort();

                    ThreadStart ts = new ThreadStart(SkipIntrosThread);
                    m_threadSkipIntros = new Thread(ts);
                    m_threadSkipIntros.Start();
                }
                else
                {
                    int iIntroTimeout = Hacker.IntroTimeout;
                    Hacker.SkipIntroductions(iIntroTimeout + ((iIntroTimeout * 10) / Properties.Settings.Default.SkipIntroTimingTweak), true);
                }
            }
        }

        public void DisarmTrap()
        {
            bool bDisarm = Global.FormVisible(m_formEncounters);
            if (bDisarm)
            {
                if (m_formEncounters.InvokeRequired)
                    m_formEncounters.Invoke((MethodInvoker)delegate { bDisarm = m_formEncounters.DisarmTrap(); });
                else
                    bDisarm = m_formEncounters.DisarmTrap();
            }

            if (!bDisarm)
            {
                MessageBox.Show("There does not appear to be a treasure with a trap to disarm.  This action only applies when the \"Treasure\" dialog is visible.",
                    "No Treasure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        public void AutoCombat()
        {
            if (Hacker == null || !Hacker.IsValid)
                return;

            Hacker.AutoCombat();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
                if (m_tipCursor1 != null)
                    m_tipCursor1.Dispose();
                if (m_tipCursor2 != null)
                    m_tipCursor2.Dispose();
                if (m_tipCursor3 != null)
                    m_tipCursor3.Dispose();
                if (m_tipCursor4 != null)
                    m_tipCursor4.Dispose();
                if (m_tipParty1 != null)
                    m_tipParty1.Dispose();
                if (m_tipParty2 != null)
                    m_tipParty2.Dispose();
                if (m_tipParty3 != null)
                    m_tipParty3.Dispose();
                if (m_tipParty4 != null)
                    m_tipParty4.Dispose();
                if (m_tipGrid != null)
                    m_tipGrid.Dispose();
                if (m_tipNoteColor != null)
                    m_tipNoteColor.Dispose();
                if (m_tipNoteSymbol != null)
                    m_tipNoteSymbol.Dispose();
                if (m_tipReadOnly != null)
                    m_tipReadOnly.Dispose();
                if (m_tipSelection1 != null)
                    m_tipSelection1.Dispose();
                if (m_tipSelection2 != null)
                    m_tipSelection2.Dispose();
                if (m_tipUnicode != null)
                    m_tipUnicode.Dispose();
                if (m_tipZoom != null)
                    m_tipZoom.Dispose();
                if (m_sheetDummy != null)
                    m_sheetDummy.Dispose();
                if (m_evtShutdown != null)
                    m_evtShutdown.Dispose();
                if (m_debugMonitor != null)
                    m_debugMonitor.Dispose();
            }
            base.Dispose(disposing);
        }

        private void miGridDebugToggleLive_Click(object sender, EventArgs e)
        {
            MapSquare square = CurrentSheet.GetSquareAtGridPoint(m_ptContextOpened);
            if (square == null)
                return;

            square.SetLive(!square.Live);
            UpdateLiveMap();
            if (m_bEditingLiveSquares)
            {
                CurrentSheet.SetDirty(m_ptContextOpened);
                SetDirty();
            }
            SetUnsaved();
        }

        private void UpdateLiveMap()
        {
            m_liveMap.Clear();
            for(int row = 0; row < CurrentSheet.GridHeight; row++)
            {
                for(int col = 0; col < CurrentSheet.GridWidth; col++)
                {
                    Point pt = new Point(col, row);
                    MapSquare square = CurrentSheet.GetSquareAtGridPoint(pt);
                    if (square != null && square.Live)
                        m_liveMap.Add(pt);
                }
            }
            if (Hacker != null && Hacker.Running)
                Hacker.ClearLiveData();
        }

        private void miDebugConsole_Click(object sender, EventArgs e)
        {
            CreateOrShowWindow<DebugConsole>(ref m_formDebugConsole, WindowType.DebugConsole);
        }

        public bool RunMenuCommand(string strMenu, int iNumber)
        {
            ToolStripMenuItem miFound = null;

            foreach (ToolStripMenuItem mi in menuStrip1.Items)
            {
                if (mi.Text.ToLower().Replace("&", "") == strMenu)
                {
                    miFound = mi;
                    break;
                }
            }

            if (miFound == null)
                return false;

            if (miFound.DropDownItems.Count <= iNumber)
                return false;

            miFound.DropDownItems[iNumber].PerformClick();
            return true;
        }

        private void menuWindow_DropDownOpening(object sender, EventArgs e)
        {
            miView100pc.Checked = (CurrentSheet.SquareSize.Equals(new Size(16, 16)));
            miView150pc.Checked = (CurrentSheet.SquareSize.Equals(new Size(24, 24)));
            miView200pc.Checked = (CurrentSheet.SquareSize.Equals(new Size(32, 32)));
            miView300pc.Checked = (CurrentSheet.SquareSize.Equals(new Size(48, 48)));
        }

        private void miGridDebugRemoveAllScripts_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove all scripts from this map?", "Confirm script removal", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                Hacker.RemoveAllScripts();
        }

        private void miDebugViewProperties_Click(object sender, EventArgs e)
        {
            StringsViewForm form = new StringsViewForm();
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("RosterFiles:\r\n{0}\r\n", Global.FormatXml(Properties.Settings.Default.RosterFiles.ToString()));
            sb.AppendFormat("RosterPaths:\r\n{0}\r\n", Global.FormatXml(Properties.Settings.Default.RosterPaths.ToString()));

            form.Strings = sb.ToString();
            form.ShowDialog();
        }
    }

    public class BoolHandlerEventArgs : EventArgs
    {
        public bool BoolValue;

        public BoolHandlerEventArgs(bool bValue)
        {
            BoolValue = bValue;
        }
    }

    public class CommonKeyForm : Form
    {
        public delegate void BoolHandler(object sender, BoolHandlerEventArgs e);

        public event BoolHandler CommonKeyNext;
        public event VoidHandler CommonKeyPrevious;
        public event VoidHandler CommonKeyRefresh;
        public event VoidHandler CommonKeyFind;
        public event VoidHandler CommonKeyBeacon;
        public event VoidHandler CommonKeyTeleport;
        public event VoidHandler CommonKeySelectAll;
        public event VoidHandler CommonKeyClearText;
        public event VoidHandler CommonKeyEscape;
        public event VoidHandler CommonKeyUndo;

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            KeyPreview = true;
        }

        private bool Handled(VoidHandler handler)
        {
            if (handler != null)
            {
                handler(this, new EventArgs());
                return true;
            }
            return false;
        }

        private bool Handled(BoolHandler handler, bool bParam)
        {
            if (handler != null)
            {
                handler(this, new BoolHandlerEventArgs(bParam));
                return true;
            }
            return false;
        }

        protected virtual bool OnCommonKeyNext(bool bIncludeCurrent) { return Handled(CommonKeyNext, bIncludeCurrent); }
        protected virtual bool OnCommonKeyPrevious() { return Handled(CommonKeyPrevious); }
        protected virtual bool OnCommonKeyRefresh() { return Handled(CommonKeyRefresh); }
        protected virtual bool OnCommonKeyFind() { return Handled(CommonKeyFind); }
        protected virtual bool OnCommonKeyBeacon() { return Handled(CommonKeyBeacon); }
        protected virtual bool OnCommonKeyTeleport() { return Handled(CommonKeyTeleport); }
        protected virtual bool OnCommonKeySelectAll() { return Handled(CommonKeySelectAll); }
        protected virtual bool OnCommonKeyClearText() { return Handled(CommonKeyClearText); }
        protected virtual bool OnCommonKeyEscape() { return Handled(CommonKeyEscape); }
        protected virtual bool OnCommonKeyUndo() { return Handled(CommonKeyUndo); }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            e.Handled = HandleKeyDown(e);
            if (!e.Handled)
                base.OnKeyDown(e);
        }

        private bool HandleKeyDown(KeyEventArgs e)
        {
            bool bControl = (e.Modifiers == Keys.Control);
            bool bShift = (e.Modifiers == Keys.Shift);
            bool bNone = (e.Modifiers == Keys.None);

            switch (e.KeyCode)
            {
                case Keys.F3: return bNone ? OnCommonKeyNext(false) : bShift ? OnCommonKeyPrevious() : false;
                case Keys.F5: return bNone ? OnCommonKeyRefresh() : false;
                case Keys.Escape: return bNone ? OnCommonKeyEscape() : false;
                case Keys.F: return bControl ? OnCommonKeyFind() : false;
                case Keys.B: return bControl ? OnCommonKeyBeacon() : false;
                case Keys.T: return bControl ? OnCommonKeyTeleport() : false;
                case Keys.U: return bControl ? OnCommonKeyClearText() : false;
                case Keys.A: return bControl ? OnCommonKeySelectAll() : false;
                case Keys.Z: return bControl ? OnCommonKeyUndo() : false;
                default: return false;
            }
        }
    }

    public class HackerBasedForm : CommonKeyForm, ISplitters
    {
        protected IMain m_main = null;
        protected bool m_bDestroy = false;
        protected WindowType m_windowType = WindowType.None;

        public WindowType WindowType { get { return m_windowType; } }

        protected MemoryHacker Hacker { get { return m_main == null ? null : m_main.Hacker; } }

        public virtual void Destroy()
        {
            m_bDestroy = true;
            Close();
        }

        public HackerBasedForm(IMain main, WindowType type) : base()
        {
            SetMain(main, type);
            m_windowType = type;
        }

        public HackerBasedForm()
            : base()
        {
        }

        public void SetMain(IMain main, WindowType type = WindowType.None)
        {
            if (type != WindowType.None)
                m_windowType = type;
            if (m_main != main)
            {
                m_main = main;
                OnMainSet();
            }
            else
                OnMainSetAgain();
        }

        protected virtual void BeforeSetSize() { }
        protected virtual void OnMainSet() { }
        protected virtual void OnMainSetAgain() { OnMainSet(); }
        public virtual int[] Splitters { get { return null; } set {} }

        public virtual void SetParameter(object param) { }

        protected override void OnClosing(CancelEventArgs e)
        {
            BeforeSetSize();
            if (m_windowType != WindowType.None)
                Global.Windows.Set(m_windowType, this);
        }

        protected override void WndProc(ref Message m)
        {
            if (m_main != null)
                m_main.ProcessMessage(ref m, this);
            base.WndProc(ref m);
        }
    }

    public class HackerBasedUserControl : UserControl
    {
        protected IMain m_main;
        protected MemoryHacker Hacker { get { return m_main == null ? null : m_main.Hacker; } }
        protected virtual void OnMainSet() { }

        public void SetMain(IMain main)
        {
            m_main = main;
            OnMainSet();
        }
    }
}
