using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class DebugConsole : HackerBasedForm
    {
        private List<string> m_history = new List<string>();
        private int m_iHistoryCursor = -1;
        private MemoryBytes m_bytesMonitoring = null;
        private MemoryBytes m_bytesReset = null;
        private Timer m_timerMonitor;
        private bool m_bMonitorHex;
        private string m_strPartialNextCommand = "";
        private Queue<string> m_nextCommands = null;
        private bool m_bLogTime = false;
        private bool m_bEcho = true;
        private long m_iMonitorMin = Int64.MaxValue;
        private long m_iMonitorMax = Int64.MinValue;
        private long m_iMonitorMinDiff = Int64.MaxValue;
        private long m_iMonitorMaxDiff = Int64.MinValue;


        public DebugConsole()
        {
            InitializeComponent();
            m_timerMonitor = new Timer();
            m_timerMonitor.Interval = 10;
            m_timerMonitor.Tick += timerMonitor_Tick;
        }

        private void InitCommands()
        {
            if (m_commands != null)
                return;

            m_commands = new Dictionary<string, ConsoleCommandInfo>();

            AddCommand("Close", CloseCommand, "Hide the console window");
            AddCommand("Lines", Lines, "Print console line count");
            AddCommand("Exit", CloseCommand, "Hide the console window");
            AddCommand("GridDirty", GridDirty, "Mark a square on the main grid as dirty", "x y");
            AddCommand("Dirty", Dirty, "Set the general UI dirty flag");
            AddCommand("RefreshActive", RefreshActive, "Force a refresh of the active squares");
            AddCommand("Help", Help, "List console commands");
            AddCommand("GameDirty", GameDirty, "Mark a square as dirty based on in-game coordinates", "x y");
            AddCommand("GiveExp", GiveExp, "Give a character (#) an amount of experience points (exp)", "# exp");
            AddCommand("SetAllStats", SetAllStats, "Set all of a character's (#) stats to the same number (val)", "# val");
            AddCommand("SetLevel", SetLevel, "Set character's (#) level to (val)", "# val");
            AddCommand("SetAge", SetAge, "Set character's (#) age to (val) years", "# val");
            AddCommand("SetFullHP", SetFullHP, "Set character's (#) HP and MaxHP to (val)", "# val");
            AddCommand("Monitor", Monitor, "Display (count) bytes at (address) whenever they change.  Address is relative to DOSBox main memory starting location.", "address count");
            AddCommand("MonitorInt", MonitorInt, "Display (count) bytes at (address) as a single integer whenever they change, and optionally set the value back to [reset].  Address is relative to DOSBox main memory starting location.", "address count [reset]");
            AddCommand("MinMax", MinMax, "Show the monitoring min/max values");
            AddCommand("ClearMinMax", ClearMinMax, "Reset the monitoring min/max values");
            AddCommand("SetBytes", SetBytes, "Set bytes at (address) to (value).  Address is relative to the MemoryHacker's Main Search Offset", "address value");
            AddCommand("Clear", Clear, "Clear the debug log window");
            AddCommand("Sleep", Sleep, "Wait for (delay) milliseconds", "delay");
            AddCommand("Send", SendKeys, "Send a key sequence to the DOSBox window", "keys");
            AddCommand("Run", RunFile, "Run commands from a file", "path");
            AddCommand("Menu", DoMenu, "Select menu item (number) from the main map window menu (name)", "name number");
            AddCommand("Message", MessageWindow, "Show (message) and wait for OK", "messsage");
            AddCommand("Pause", Pause, "Wait until key is pressed");
            AddCommand("ResetHacker", ResetHacker, "Reinitialize the current MemoryHacker object");
            AddCommand("ResetMonsters", ResetMonsters, "Reinitialize the current Monster list");
            AddCommand("ResetItems", ResetItems, "Reinitialize the current Item list");
            AddCommand("LogTime", LogTime, "Show the duration of each command or not", "on/off");
            AddCommand("Echo", Echo, "Echo commands to the output window or not", "on/off");
            AddCommand("LoadItem", LoadItem, "Load an instance of an item and display its MultiLineDescription", "game index");
            AddCommand("SetWall", SetWall, "Set a wall (x,y,dir) to a value (#)", "x y dir #");
            AddCommand("GetWall", GetWall, "Get the wall bytes at (x,y)", "x y");
            AddCommand("To5", To5, "Convert a an (x,y) coordinate to packed 5-bit hex", "x y");
            AddCommand("From5", From5, "Convert a packed 5-bit hex value (#) to x,y coordinates to hex", "#");
            //AddCommand("DumpMM45Offsets", DumpMM45Offsets, "Convert MM4/5 map offsets to full-voice");
            //AddCommand("E1Items", E1Items, "Display the Eye of the Beholder 1 item table");
            //AddCommand("a", DumpMM45ScriptOffsets, "Convert MM4/5 script offsets to full-voice");
            //AddCommand("b", FindMM45ScriptStart, "Convert MM4/5 script start values to full-voice");
        }

        private void timerMonitor_Tick(object sender, EventArgs e)
        {
            if (IsDisposed)
                m_timerMonitor.Stop();

            if (m_bytesMonitoring == null)
                return;

            MemoryBytes mbNew = m_main.Hacker.ReadDirect(m_bytesMonitoring.Offset, m_bytesMonitoring.Length);
            if (Global.Compare(m_bytesMonitoring.Bytes, mbNew.Bytes))
                return;

            long oldVal = Global.ConvertBytesToLong(m_bytesMonitoring.Bytes);
            long newVal = Global.ConvertBytesToLong(mbNew.Bytes);
            long diff = newVal - oldVal;
            if (newVal < m_iMonitorMin)
                m_iMonitorMin = newVal;
            if (newVal > m_iMonitorMax)
                m_iMonitorMax = newVal;
            if (diff < m_iMonitorMinDiff)
                m_iMonitorMinDiff = diff;
            if (diff > m_iMonitorMaxDiff)
                m_iMonitorMaxDiff = diff;

            m_bytesMonitoring = mbNew;
            long monitorVal = Global.ConvertBytesToLong(m_bytesMonitoring.Bytes);
            if (m_bytesReset == null)
                Output("Monitor: {0} ({1})", m_bMonitorHex ? Global.HexString(m_bytesMonitoring.Bytes) : monitorVal.ToString(), diff.ToString());
            else
            {
                long resetVal = Global.ConvertBytesToLong(m_bytesReset.Bytes);
                m_main.Hacker.WriteDirect(m_bytesReset.Offset, m_bytesReset.Bytes);
                Output("Monitor: {0} ({1}) [{2}]", m_bMonitorHex ? Global.HexString(m_bytesMonitoring.Bytes) : monitorVal.ToString(), diff.ToString(), resetVal.ToString());
                m_bytesMonitoring = m_main.Hacker.ReadDirect(m_bytesMonitoring.Offset, m_bytesMonitoring.Length);
            }
        }

        private void SetCursor(int iPosition)
        {
            if (iPosition > tbCommand.TextLength)
                iPosition = tbCommand.TextLength;
            tbCommand.SelectionStart = iPosition;
            tbCommand.SelectionLength = 0;
        }

        private bool KeyPressed(Keys key)
        {
            switch (key)
            {
                case Keys.Up:
                    if (m_iHistoryCursor == -1)
                        m_iHistoryCursor = m_history.Count - 1;
                    else
                        m_iHistoryCursor--;

                    if (m_iHistoryCursor < 0)
                        m_iHistoryCursor = 0;

                    if (m_history.Count > m_iHistoryCursor)
                        tbCommand.Text = m_history[m_iHistoryCursor];
                    else
                        m_iHistoryCursor = 0;

                    SetCursor(tbCommand.TextLength);
                    break;
                case Keys.Down:
                    if (m_iHistoryCursor == -1)
                        break;

                    m_iHistoryCursor++;

                    if (m_history.Count > m_iHistoryCursor)
                        tbCommand.Text = m_history[m_iHistoryCursor];
                    else
                        m_iHistoryCursor = m_history.Count - 1;

                    SetCursor(tbCommand.TextLength);
                    break;
                case Keys.Enter:
                    if (m_history.Count < 1 || (m_history[m_history.Count - 1] != tbCommand.Text))
                    {
                        if (!String.IsNullOrWhiteSpace(tbCommand.Text))
                            m_history.Add(tbCommand.Text);
                    }
                    m_iHistoryCursor = -1;
                    ProcessCommand(tbCommand.Text);
                    tbCommand.Text = m_strPartialNextCommand;
                    if (m_nextCommands != null && m_nextCommands.Count > 0)
                    {
                        ProcessQueue();
                        tbCommand.Text = m_strPartialNextCommand;
                    }
                    m_strPartialNextCommand = String.Empty;
                    break;
                case Keys.Control | Keys.U:
                    tbCommand.Text = String.Empty;
                    break;
                case Keys.Escape:
                    Hide();
                    break;
                case Keys.Tab:
                    AutoComplete();
                    break;
                default:
                    return false;
            }

            return true;
        }

        private void ProcessQueue()
        {
            tbCommand.Text = String.Empty;
            int iQueueLength = m_nextCommands.Count;
            while (m_nextCommands.Count > 0)
            {
                string strCommand = m_nextCommands.Dequeue();
                Text = String.Format("Debug Console: Running {0}/{1} (Control+Shift+C to abort)", iQueueLength - m_nextCommands.Count, iQueueLength);
                if (String.IsNullOrWhiteSpace(strCommand))
                    continue;
                ProcessCommand(strCommand);
                Application.DoEvents();
                if ( (tbCommand.TextLength > 0 && !String.IsNullOrWhiteSpace(tbCommand.Text)) ||
                    (NativeMethods.IsShiftDown() && NativeMethods.IsControlDown() && NativeMethods.IsKeyDown(Keys.C)) )
                {
                    Output("RunFile aborted after running {0}/{1} commands", iQueueLength - m_nextCommands.Count, iQueueLength);
                    m_nextCommands.Clear();
                    Text = "Debug Console";
                    return;
                }
            }
            Output("Executed {0} from file", Global.Plural(iQueueLength, "command"));
            Text = "Debug Console";
            m_nextCommands.Clear();
        }

        private void tbCommand_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = KeyPressed(e.KeyData);
        }

        private List<string[]> GetCommands(string[] tokens)
        {
            List<string[]> commands = new List<string[]>();
            int iStart = 0;
            for (int i = 0; i < tokens.Length; i++)
            {
                if (tokens[i] == "&")
                {
                    commands.Add(tokens.Skip(iStart).Take(i - iStart).ToArray());
                    iStart = i + 1;
                }
            }
            if (iStart < tokens.Length)
                commands.Add(tokens.Skip(iStart).ToArray());
            return commands;
        }

        enum State { Normal, Quote, SingleQuote };

        private string[] GetTokens(string str)
        {
            List<string> tokens = new List<string>();
            int iNext = 0;
            StringBuilder sbNextToken = new StringBuilder();
            State state = State.Normal;
            bool bBackslash = false;

            while (iNext < str.Length)
            {
                if (bBackslash)
                {
                    // Only quotes are escapable from the main command line 
                    // (otherwise things like file paths need to be quoted for no particular reason)
                    switch (str[iNext])
                    {
                        case '"':
                        case '\'':
                            break;
                        default:
                            sbNextToken.Append("\\");
                            break;
                    }
                    sbNextToken.Append(str[iNext]);
                    bBackslash = false;
                }
                else
                {
                    switch (str[iNext])
                    {
                        case '"':
                            if (state == State.SingleQuote)
                                break;
                            state = (state == State.Normal ? State.Quote : State.Normal);
                            break;
                        case '\'':
                            if (state == State.Quote)
                                break;
                            state = (state == State.Normal ? State.SingleQuote : State.Normal);
                            break;
                        case ';':
                            if (state == State.Quote)
                                sbNextToken.Append(str[iNext]);
                            else
                            {
                                tokens.Add(sbNextToken.ToString());
                                tokens.Add(";");
                            }
                            break;
                        case ' ':
                        case '\t':
                        case '\r':
                        case '\n':
                            if (state == State.Normal)
                            {
                                if (sbNextToken.Length > 0)
                                    tokens.Add(sbNextToken.ToString());
                                sbNextToken.Clear();
                            }
                            else
                                sbNextToken.Append(str[iNext]);
                            break;
                        case '\\':
                            if (state == State.SingleQuote)
                                sbNextToken.Append('\\');
                            else
                                bBackslash = true;
                            break;
                        default:
                            sbNextToken.Append(str[iNext]);
                            break;
                    }
                }
                iNext++;
            }
            if (sbNextToken.Length > 0)
                tokens.Add(sbNextToken.ToString());

            return tokens.ToArray();
        }

        private void Output(string str, params object[] format)
        {
            tbOutput.SelectionStart = tbOutput.TextLength;
            tbOutput.SelectedText = String.Format(str, format) + (str.EndsWith("\r\n") ? "" : "\r\n");
            tbOutput.SelectionStart = tbOutput.TextLength;
            tbOutput.SelectionLength = 0;
            tbOutput.ScrollToCaret();
        }

        private Dictionary<string, ConsoleCommandInfo> m_commands = null;

        private void AddCommand(string text, ConsoleCommand command, string help, string args = "")
        {
            m_commands.Add(text.ToLower(), new ConsoleCommandInfo(text, args, command, help));
        }

        // Sample commands:
        // send '\a\{F9}\A' & sleep 500 & setbytes -79863 3 1 1 1 & send ' ' & sleep 500 & menu debug 2 & pause

        private void ProcessCommand(string strMain)
        {
            if (m_bEcho)
                Output("> {0}", strMain);

            if (strMain.StartsWith("#"))
                return; // comment; ignore

            DateTime dtStart = DateTime.Now;

            string[] tokens = GetTokens(strMain);
            List<string[]> commands = GetCommands(tokens);

            foreach (string[] command in commands)
            {
                for (int i = 0; i < command.Length; i++)
                {
                    i++;
                    string strCommand = command[i - 1].ToLower();
                    if (m_commands.ContainsKey(strCommand))
                    {
                        m_commands[strCommand].Function(command, ref i);
                        if (i < command.Length && command[i] != ";")
                        {
                            Output("(Ignoring extra arguments to command \"{0}\")", strCommand);
                            break;
                        }
                        else
                            i++;
                    }
                    else
                        Output("Unknown command: \"{0}\"", command[i - 1]);
                }
            }

            if (m_bLogTime)
                Output("Time: {0:F1} ms", (DateTime.Now - dtStart).TotalMilliseconds);
        }

        private void AutoComplete()
        {
            string strPartial = tbCommand.Text.ToLower();
            foreach (string strCommand in m_commands.Keys)
            {
                if (strCommand.StartsWith(strPartial))
                {
                    tbCommand.Text = m_commands[strCommand].Text + " ";
                    SetCursor(tbCommand.TextLength);
                    break;
                }
            }
        }

        private void CloseCommand(string[] tokens, ref int iToken)
        {
            Hide();
        }

        private void Lines(string[] tokens, ref int iToken)
        {
            Output(String.Format("Lines: {0}\r\n", tbOutput.Lines.Length));
        }

        private void ClearMinMax(string[] tokens, ref int iToken)
        {
            ClearMinMaxQuiet();
            Output("Min/Max cleared\r\n");
        }

        private void ClearMinMaxQuiet()
        {
            m_iMonitorMin = Int64.MaxValue;
            m_iMonitorMax = Int64.MinValue;
            m_iMonitorMinDiff = Int64.MaxValue;
            m_iMonitorMaxDiff = Int64.MinValue;
        }

        private void MinMax(string[] tokens, ref int iToken)
        {
            Output(String.Format("Min: {0}, Max: {1}, MinDiff: {2}, MaxDiff: {3}\r\n", m_iMonitorMin, m_iMonitorMax, m_iMonitorMinDiff, m_iMonitorMaxDiff));
        }

        private void Help(string[] tokens, ref int iToken)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Commands:\r\n");
            foreach (ConsoleCommandInfo info in m_commands.Values)
                sb.AppendFormat("{0}{1}:  {2}\r\n", info.Text, String.IsNullOrWhiteSpace(info.Args) ? "" : (" " + info.Args), info.Help);
            Output(sb.ToString());
        }

        private int GetInt(string[] tokens, ref int iToken) { return (int)GetLong(tokens, ref iToken); }

        private long GetLong(string[] tokens, ref int iToken)
        {
            if (iToken >= tokens.Length)
                return 0;

            long iOut = Global.GetLong(tokens[iToken]);
            iToken++;
            return iOut;
        }

        private void RefreshActive(string[] tokens, ref int iToken)
        {
            if (!CheckHackerActive())
                return;

            ActiveSquares squares = m_main.Hacker.GetActiveSquares(m_main.Main, true);
            m_main.CurrentSheet.SetActiveSquares(squares, m_main.CurrentBook, true);

            Output("Active squares reset");
        }

        private void GridDirty(string[] tokens, ref int iToken)
        {
            // Marks a particular square on the grid as "dirty"
            // Example:  dirty 10 12

            int iX = GetInt(tokens, ref iToken);
            int iY = GetInt(tokens, ref iToken);

            if (CheckMainNull())
                return;

            m_main.SetGridDirty(new Point(iX, iY));
            Output("Grid square ({0},{1}) marked as dirty", iX, iY);
        }

        public static void GiveExperience(IMain main, int iChar, BaseCharacter baseChar, long lAmount)
        {
            long exp = baseChar.BasicExperience;
            exp += lAmount;

            byte[] bytes = baseChar.RawBytes;

            switch (baseChar.Offsets.ExperienceLength)
            {
                case 6: // Wizardry-style
                    WizardryLong wlExp = new WizardryLong(exp);
                    Buffer.BlockCopy(wlExp.Bytes, 0, bytes, baseChar.Offsets.Experience, 6);
                    break;
                default:
                    Buffer.BlockCopy(BitConverter.GetBytes((int)exp), 0, bytes, baseChar.Offsets.Experience, baseChar.Offsets.ExperienceLength);
                    break;
            }

            main.Hacker.SetCharacterBytes(iChar, bytes);
        }

        public static void SetAllStats(IMain main, int iChar, BaseCharacter baseChar, int iVal)
        {
            byte[] bytes = baseChar.RawBytes;

            if (baseChar is WizCharacter)
            {
                // Use packed stats
                PackedFiveBitValues p5b = new PackedFiveBitValues(Global.IntArray(6, iVal));
                Buffer.BlockCopy(p5b.Bytes, 0, bytes, baseChar.Offsets.Stats, 4);
            }
            else if (baseChar is MM345BaseCharacter)
            {
                // two-byte stats
                byte[] stats = new byte[14];
                for (int i = 0; i < 7; i++)
                {
                    stats[i * 2] = (byte)iVal;
                    stats[i * 2 + 1] = 0;
                }
                Buffer.BlockCopy(stats, 0, bytes, baseChar.Offsets.Stats, 14);
            }
            else if (baseChar is EOBBaseCharacter)
            {
                // two-byte stats
                byte[] stats = new byte[14];
                for (int i = 0; i < 7; i++)
                {
                    stats[i * 2] = (byte)iVal;
                    stats[i * 2 + 1] = (byte)iVal;
                }
                // 18/xx
                stats[2] = 0;
                stats[3] = 0;
                Buffer.BlockCopy(stats, 0, bytes, baseChar.Offsets.Stats, 14);
            }
            else
            {
                // Use one-byte unpacked stats
                Buffer.BlockCopy(Global.ByteArray(7, (byte)iVal), 0, bytes, baseChar.Offsets.Stats, 7);
            }

            main.Hacker.SetCharacterBytes(iChar, bytes);
        }

        public static void SetLevel(IMain main, int iChar, BaseCharacter baseChar, int iVal)
        {
            byte[] bytes = baseChar.RawBytes;

            if (baseChar is WizCharacter)
            {
                Global.SetInt16(bytes, baseChar.Offsets.Level, iVal);
                Global.SetInt16(bytes, baseChar.Offsets.LevelMod, iVal);
            }
            else
            {
                bytes[baseChar.Offsets.Level] = (byte)iVal;
                if (baseChar.Offsets.LevelMod != -1)
                    bytes[baseChar.Offsets.LevelMod] = 0;
            }

            main.Hacker.SetCharacterBytes(iChar, bytes);
        }

        public static void SetFullHP(IMain main, int iChar, BaseCharacter baseChar, int iVal)
        {
            byte[] bytes = baseChar.RawBytes;

            Global.SetInt16(bytes, baseChar.Offsets.CurrentHP, iVal);
            Global.SetInt16(bytes, baseChar.Offsets.MaxHP, iVal);
            if (baseChar.Offsets.MaxHPMod != -1)
                Global.SetInt16(bytes, baseChar.Offsets.MaxHPMod, iVal);

            main.Hacker.SetCharacterBytes(iChar, bytes);
        }

        private void Clear(string[] tokens, ref int iToken)
        {
            tbOutput.Clear();
        }

        private void Monitor(string[] tokens, ref int iToken)
        {
            Monitor(tokens, ref iToken, true);
        }

        private void Monitor(string[] tokens, ref int iToken, bool bHex)
        {
            long iMemory = GetLong(tokens, ref iToken);
            int iCount = GetInt(tokens, ref iToken);

            if (iCount < 1)
            {
                if (m_bytesMonitoring != null)
                {
                    Output("Monitor of memory at 0x{0} stopped", m_bytesMonitoring.Offset);
                    m_bytesMonitoring = null;
                    m_bytesReset = null;
                    m_timerMonitor.Stop();
                    return;
                }
                Output("Must monitor at least 1 byte");
                return;
            }

            if (tokens.Length > 3)
            {
                long iReset = GetLong(tokens, ref iToken);
                m_bytesReset = new MemoryBytes(Global.BytesFor(iReset, iCount), iMemory);
            }

            ClearMinMaxQuiet();

            m_bMonitorHex = bHex;
            m_bytesMonitoring = m_main.Hacker.ReadDirect(iMemory, iCount);
            Output("Now monitoring {0} at 0x{1}", Global.Plural(m_bytesMonitoring.Bytes.Length, "byte"), m_bytesMonitoring.Offset);
            Output("Monitor: {0} (0)", bHex ? Global.HexString(m_bytesMonitoring.Bytes) : Global.ConvertBytesToLong(m_bytesMonitoring.Bytes).ToString());
            m_timerMonitor.Start();
        }

        private void MonitorInt(string[] tokens, ref int iToken)
        {
            Monitor(tokens, ref iToken, false);
        }

        private void SetBytes(string[] tokens, ref int iToken)
        {
            long iMemory = GetLong(tokens, ref iToken);
            List<byte> bytes = new List<byte>();
            while (iToken < tokens.Length)
                bytes.Add((byte) GetInt(tokens, ref iToken));

            byte[] byteArray = bytes.ToArray();
            if (Hacker.WriteOffset(iMemory, byteArray))
                Output("Set {0}:{1}", iMemory, Global.ByteString(byteArray));
            else
                Output("Error setting bytes");
        }

        private void SetFullHP(string[] tokens, ref int iToken)
        {
            int iChar = GetInt(tokens, ref iToken);
            int iVal = GetInt(tokens, ref iToken);

            BaseCharacter baseChar = GetCharacter(iChar);
            if (baseChar == null)
                return;

            SetFullHP(m_main, iChar, baseChar, iVal);

            Output("Set HP and MaxHP for character #{0} ({1}) to {2}.", iChar, baseChar.Name, iVal);
        }

        public static void SetAge(IMain main, int iChar, BaseCharacter baseChar, int iVal)
        {
            byte[] bytes = baseChar.RawBytes;

            if (baseChar is WizCharacter)
                Global.SetInt16(bytes, baseChar.Offsets.Age, iVal * 52);    // weeks
            else
                bytes[baseChar.Offsets.Age] = (byte)iVal;

            main.Hacker.SetCharacterBytes(iChar, bytes);
        }

        private void SetAge(string[] tokens, ref int iToken)
        {
            int iChar = GetInt(tokens, ref iToken);
            int iVal = GetInt(tokens, ref iToken);

            BaseCharacter baseChar = GetCharacter(iChar);
            if (baseChar == null)
                return;

            SetAge(m_main, iChar, baseChar, iVal);

            Output("Set Age for character #{0} ({1}) to {2}.", iChar, baseChar.Name, iVal);
        }

        private BaseCharacter GetCharacter(int iChar)
        {
            if (!CheckHackerActive())
                return null;

            List<BaseCharacter> chars = m_main.Hacker.GetCharacters();
            if (chars == null || iChar >= chars.Count)
            {
                Output("Invalid character ({0}); there are currently only {1}.", iChar, Global.Plural(chars.Count, "character"));
                return null;
            }

            return chars[iChar];
        }

        private void SetLevel(string[] tokens, ref int iToken)
        {
            int iChar = GetInt(tokens, ref iToken);
            int iVal = GetInt(tokens, ref iToken);

            BaseCharacter baseChar = GetCharacter(iChar);
            if (baseChar == null)
                return;

            SetLevel(m_main, iChar, baseChar, iVal);

            Output("Set level for character #{0} ({1}) to {2}.", iChar, baseChar.Name, iVal);
        }

        private void SetAllStats(string[] tokens, ref int iToken)
        {
            int iChar = GetInt(tokens, ref iToken);
            int iVal = GetInt(tokens, ref iToken);

            BaseCharacter baseChar = GetCharacter(iChar);
            if (baseChar == null)
                return;

            SetAllStats(m_main, iChar, baseChar, iVal);

            Output("Set all stats for character #{0} ({1}) to {2}.", iChar, baseChar.Name, iVal);
        }

        private void GiveExp(string[] tokens, ref int iToken)
        {
            int iChar = GetInt(tokens, ref iToken);
            int iExp = GetInt(tokens, ref iToken);

            if (!CheckHackerActive())
                return;

            List<BaseCharacter> chars = m_main.Hacker.GetCharacters();
            if (chars == null || iChar >= chars.Count)
            {
                Output("Invalid character ({0}); there are currently only {1}.", iChar, Global.Plural(chars.Count, "character"));
                return;
            }

            GiveExperience(m_main, iChar, chars[iChar], iExp);

            Output("Gave {0} Experience Points to character #{1} ({2}).", iExp, iChar, chars[iChar].Name);
        }

        private void SendKeys(string[] tokens, ref int iToken)
        {
            if (!CheckHackerActive())
                return;

            while (iToken < tokens.Length)
                m_main.Hacker.SendInputsToDOSBox(NativeMethods.InputsForSendString(tokens[iToken++]));
        }

        private void To5(string[] tokens, ref int iToken)
        {
            if (iToken > tokens.Length - 1)
            {
                Output("X and Y are required.");
                return;
            }

            int iX = GetInt(tokens, ref iToken);
            int iY = GetInt(tokens, ref iToken);

            if (iX < 0 || iX > 31) { Output("Invalid X"); return; }
            if (iY < 0 || iY > 31) { Output("Invalid Y"); return; }

            Output("{0},{1} = 0x{2:X4}", iX, iY, EOBMemoryHacker.PackedFiveFromPoint(new Point(iX, iY)));
        }

        private void From5(string[] tokens, ref int iToken)
        {
            if (iToken > tokens.Length)
            {
                Output("Value is required.");
                return;
            }

            int iVal = GetInt(tokens, ref iToken);

            Point pt = EOBMemoryHacker.PointFromPackedFive((ushort) iVal);
            Output("0x{0:X4} = {1},{2}", iVal, pt.X, pt.Y);
        }

        private void GetWall(string[] tokens, ref int iToken)
        {
            if (!CheckHackerActive())
                return;

            EOBMemoryHacker eobHacker = Hacker as EOBMemoryHacker;
            if (eobHacker == null)
                return;

            if (iToken > tokens.Length - 1)
            {
                Output("X and Y are required.");
                return;
            }

            int iX = GetInt(tokens, ref iToken);
            int iY = GetInt(tokens, ref iToken);

            if (iX < 0 || iX > 31) { Output("Invalid X"); return; }
            if (iY < 0 || iY > 31) { Output("Invalid Y"); return; }

            byte[] bytes = eobHacker.GetMapSquare(iX, iY);
            Output("Wall at {0},{1}: {2}", iX, iY, Global.ByteString(bytes));
        }

        private void SetWall(string[] tokens, ref int iToken)
        {
            if (!CheckHackerActive())
                return;

            EOBMemoryHacker eobHacker = Hacker as EOBMemoryHacker;
            if (eobHacker == null)
                return;

            int iMap = eobHacker.GetCurrentMapIndex();

            if (iToken > tokens.Length - 3)
            {
                Output("X, Y, direction and value are required.");
                return;
            }

            int iX = GetInt(tokens, ref iToken);
            int iY = GetInt(tokens, ref iToken);
            int iDir = GetInt(tokens, ref iToken);
            int iVal = GetInt(tokens, ref iToken);

            if (iX < 0 || iX > 31) { Output("Invalid X"); return; }
            if (iY < 0 || iY > 31) { Output("Invalid Y"); return; }
            if (iDir < -1 || iDir > 3) { Output("Invalid Direction"); return; }
            if (iVal < 0 || iVal > 255) { Output("Invalid Value"); return; }

            bool bResult = eobHacker.SetMapSquare(iX, iY, iDir, iVal);
            Output("Setting {0},{1} ({2}) to {3} {4}", iX, iY, iDir == -1 ? "all" : EOBMemoryHacker.FacingString(iDir), EOB1ScriptLine.WallDesc(iMap, iVal),  bResult ? "succeeded" : "failed");
        }

        private void LoadItem(string[] tokens, ref int iToken)
        {
            if (!CheckHackerActive())
                return;

            if (iToken > tokens.Length - 1)
            {
                Output("Game and Index are required.");
                return;
            }

            string strGame = tokens[iToken++];
            string strIndex = tokens[iToken++];

            int iGame = 0;
            if (!Int32.TryParse(strGame, out iGame))
            {
                Output("Invalid game index");
                return;
            }
            GameNames game = (GameNames)iGame;
            int iIndex = -1;
            if (!Int32.TryParse(strIndex, out iIndex))
            {
                Output("Invalid item index");
                return;
            }

            GameGlobals globals = Games.GetGlobals(game);
            if (globals == null)
            {
                Output("Could not load game globals for {0}", Games.ShortName(game));
                return;
            }

            Item item = globals.GetItem(iIndex);
            if (item == null)
            {
                Output("Item[{0}] is null", iIndex);
                return;
            }

            Output("Direct item:\r\n" + item.MultiLineDescription);
            Output("Cloned item:\r\n" + item.Clone().MultiLineDescription);
        }

        private void Sleep(string[] tokens, ref int iToken)
        {
            if (iToken < tokens.Length)
            {
                int iSleep = 0;
                if (Int32.TryParse(tokens[iToken++], out iSleep))
                {
                    if (iSleep < 0)
                        return;

                    Output("Waiting for {0} milliseconds", iSleep);
                    if (iSleep < 20)
                        System.Threading.Thread.Sleep(iSleep);
                    else
                    {
                        DateTime dtStart = DateTime.Now;
                        tbCommand.Clear();
                        while ((DateTime.Now - dtStart).TotalMilliseconds < iSleep)
                        {
                            Application.DoEvents();
                            if (tbCommand.TextLength > 0)
                            {
                                Output("Wait canceled after {0:F0} ms", (DateTime.Now - dtStart).TotalMilliseconds);
                                m_strPartialNextCommand = tbCommand.Text;
                                return;
                            }
                            System.Threading.Thread.Sleep(iSleep > 1000 ? 100 : 10);
                        }
                    }
                }
            }
        }

        private void RunFile(string[] tokens, ref int iToken)
        {
            string strFile = "";
            try
            {
                strFile = tokens[iToken++];
                m_nextCommands = new Queue<string>(File.ReadAllLines(strFile));
                Output("Loaded {0} from file: {1}", Global.Plural(m_nextCommands.Count, "command"), strFile);
            }
            catch (Exception ex)
            {
                Output("Could not load command file \"{0}\": {1}", strFile, ex.Message);
            }
        }

        private void Pause(string[] tokens, ref int iToken)
        {
            tbCommand.Text = String.Empty;
            Output("Paused; press space to continue");
            while (NativeMethods.GetAsyncKeyState(Keys.Space) >= 0)
            {
                System.Threading.Thread.Sleep(50);
                Application.DoEvents();
                if (tbCommand.TextLength > 0)
                {
                    m_strPartialNextCommand = tbCommand.Text;
                    break;
                }
            }
            Output("Resumed");
        }

        private void ResetHacker(string[] tokens, ref int iToken)
        {
            if (!CheckHackerActive())
                return;

            Hacker.NeedsReinitialize = true;
            Output("Hacker for \"{0}\" reinitialize requested", Games.Name(Hacker.Game));
        }

        private void ResetMonsters(string[] tokens, ref int iToken)
        {
            if (!CheckHackerActive())
                return;

            GameGlobals globals = Games.GetGlobals(Hacker.Game);
            if (globals == null)
            {
                Output("GameGlobals object for \"{0}\" is null", Games.Name(Hacker.Game));
                return;
            }
            globals.ResetMonsterList();

            Output("Reset monster list for \"{0}\"", Games.Name(Hacker.Game));
        }

        private void ResetItems(string[] tokens, ref int iToken)
        {
            if (!CheckHackerActive())
                return;

            GameGlobals globals = Games.GetGlobals(Hacker.Game);
            if (globals == null)
            {
                Output("GameGlobals object for \"{0}\" is null", Games.Name(Hacker.Game));
                return;
            }
            globals.ResetItemList();

            Output("Reset item list for \"{0}\"", Games.Name(Hacker.Game));
        }

        private void LogTime(string[] tokens, ref int iToken)
        {
            if (iToken >= tokens.Length)
            {
                Output("Time logging is {0}", m_bLogTime ? "on" : "off");
                iToken = tokens.Length;
                return;
            }

            string strArg = tokens[iToken++];
            m_bLogTime = strArg.ToLower() == "on";
            Output("Time logging {0}", m_bLogTime ? "on" : "off");
        }

        private void FindMM45ScriptStart(string[] tokens, ref int iToken)
        {
            byte[] bytesOldMM4 = File.ReadAllBytes("F:\\Fresh-Novoice-XEEN.SAV");
            byte[] bytesOldMM5 = File.ReadAllBytes("F:\\Fresh-Novoice-DARK.SAV");
            byte[] bytesNewMM4 = File.ReadAllBytes("F:\\Fresh-Voice-XEEN.WOX");
            byte[] bytesNewMM5 = File.ReadAllBytes("F:\\Fresh-Voice-DARK.WOX");
            MM4FileOffsets mm4 = new MM4FileOffsets();
            MM5FileOffsets mm5 = new MM5FileOffsets();
            List<Tuple<int, List<int>>> mm4Scripts = new List<Tuple<int, List<int>>>();
            foreach (uint iOffset in mm4.Scripts)
            {
                int iFind = 16;
                int[] matches = null;
                while (matches == null || matches.Length == 0)
                {
                    byte[] bytesOriginal = Global.Subset(bytesOldMM4, (int)iOffset, iFind);
                    matches = Global.FindBytes(bytesNewMM4, bytesOriginal);
                    iFind--;
                    if (iFind < 4)
                        break;
                }
                mm4Scripts.Add(new Tuple<int, List<int>>((int)iOffset, new List<int>(matches)));
            }
            Output("MM4.Scripts:");
            foreach (Tuple<int, List<int>> list in mm4Scripts)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("0x{0:X8}\t", list.Item1);
                foreach (int i in list.Item2)
                    sb.AppendFormat("0x{0:X8}, ", i);
                Global.Trim(sb);
                sb.Append("\t");
                foreach (int i in list.Item2)
                    sb.AppendFormat("0x{0:X8}, ", i - list.Item1);
                Global.Trim(sb);
                Output(sb.ToString());
            }
        }

        private void E1Items(string[] tokens, ref int iToken)
        {
            EOB1MemoryHacker eob1 = Hacker as EOB1MemoryHacker;
            if (eob1 == null)
            {
                Output("Hacker is not an EOB1MemoryHacker");
                return;
            }


            byte[] bytesItemTable = eob1.GetItemTable();
            for (int i = 0; i <= bytesItemTable.Length - 14; i += 14)
            {
                EOB1Item item = new EOB1Item(bytesItemTable, i);
                Output(item.DebugDescription + String.Format("|{0}\r\n", Global.ByteString(item.GetBytes())));
            }
        }

        private void DumpMM45Offsets(string[] tokens, ref int iToken)
        {
            byte[] bytesOldMM4 = File.ReadAllBytes("F:\\Fresh-Novoice-XEEN.SAV");
            byte[] bytesOldMM5 = File.ReadAllBytes("F:\\Fresh-Novoice-DARK.SAV");
            byte[] bytesNewMM4 = File.ReadAllBytes("F:\\Fresh-Voice-XEEN.WOX");
            byte[] bytesNewMM5 = File.ReadAllBytes("F:\\Fresh-Voice-DARK.WOX");
            MM4FileOffsets mm4 = new MM4FileOffsets();
            MM5FileOffsets mm5 = new MM5FileOffsets();
            List<Tuple<int, List<int>>> mm4Maps = new List<Tuple<int, List<int>>>();
            List<Tuple<int, List<int>>> mm5Maps = new List<Tuple<int, List<int>>>();

            foreach (int iOffset in MM4FileOffsets.StaticMaps)
            {
                byte[] bytesOriginal = Global.Subset(bytesOldMM4, iOffset, 512);
                int[] matches = Global.FindBytes(bytesNewMM4, bytesOriginal);
                mm4Maps.Add(new Tuple<int, List<int>>(iOffset, new List<int>(matches)));
            }
            Output("MM4.Maps:");
            foreach(Tuple<int, List<int>> list in mm4Maps)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("0x{0:X8}: ", list.Item1);
                foreach (int i in list.Item2)
                    sb.AppendFormat("0x{0:X8}, ", i);
                sb.Append("|");
                foreach (int i in list.Item2)
                    sb.AppendFormat("0x{0:X8}, ", i - list.Item1);
                Global.Trim(sb);
                Output(sb.ToString());
            }
            foreach (int iOffset in MM5FileOffsets.StaticMaps)
            {
                byte[] bytesOriginal = Global.Subset(bytesOldMM5, iOffset, 512);
                int[] matches = Global.FindBytes(bytesNewMM5, bytesOriginal);
                mm5Maps.Add(new Tuple<int, List<int>>(iOffset, new List<int>(matches)));
            }
            Output("MM5.Maps:");
            foreach (Tuple<int, List<int>> list in mm5Maps)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("0x{0:X8}: ", list.Item1);
                foreach (int i in list.Item2)
                    sb.AppendFormat("0x{0:X8}, ", i);
                sb.Append("|");
                foreach (int i in list.Item2)
                    sb.AppendFormat("0x{0:X8}, ", i - list.Item1);
                Global.Trim(sb);
                Output(sb.ToString());
            }
        }

        private void ConvertScriptOffset(MM4Map map, uint offset, string strEnum, byte[] bytesOld, byte[] bytesNew)
        {
            uint iOldScripts = MM45.FileOffsetsMM4.Scripts[(int)map];
            uint iNewScripts = MM45.FileOffsetsMM4.AlternateGameScriptStart((int)map);

            ConvertScriptOffset("MM4Map." + map.ToString(), strEnum, iOldScripts, iNewScripts, offset, bytesOld, bytesNew);
        }

        private void ConvertScriptOffset(string strMap, string strEnum, uint iOldScripts, uint iNewScripts, uint offset, byte[] bytesOld, byte[] bytesNew)
        {
            int iFind = 16;
            int[] found = null;

            while (found == null || found.Length == 0)
            {
                byte[] bytesOldSubset = Global.Subset(bytesOld, (int)(iOldScripts + offset - 4), iFind);
                found = Global.FindBytes(bytesNew, bytesOldSubset, (int)(iNewScripts + offset - 4));
                iFind--;
                if (iFind < 5)
                    break;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}\t{1}\t{2}\t", strMap, strEnum, iOldScripts);
            foreach (int i in found)
                sb.AppendFormat("{0}, ", i);
            Global.Trim(sb);
            sb.Append("\t");
            foreach (int i in found)
                sb.AppendFormat("{0}, ", i - iNewScripts);
            Global.Trim(sb);
            Output(sb.ToString());
        }

        private void ConvertScriptOffset(MM5Map map, uint offset, string strEnum, byte[] bytesOld, byte[] bytesNew)
        {
            uint iOldScripts = MM45.FileOffsetsMM5.Scripts[(int)map];
            uint iNewScripts = MM45.FileOffsetsMM5.AlternateGameScriptStart((int)map);

            ConvertScriptOffset("MM5Map." + map.ToString(), strEnum, iOldScripts, iNewScripts, offset, bytesOld, bytesNew);
        }

        private void DumpMM45ScriptOffsets(string[] tokens, ref int iToken)
        {
            byte[] bytesOldMM4 = File.ReadAllBytes("F:\\Fresh-Novoice-XEEN.SAV");
            byte[] bytesOldMM5 = File.ReadAllBytes("F:\\Fresh-Novoice-DARK.SAV");
            byte[] bytesNewMM4 = File.ReadAllBytes("F:\\Fresh-Voice-XEEN.WOX");
            byte[] bytesNewMM5 = File.ReadAllBytes("F:\\Fresh-Voice-DARK.WOX");

            ConvertScriptOffset(MM4Map.F3Surface, MM45Bytes.PhirnaF3CS0802, "MM45Bytes.PhirnaF3CS0802", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS1214, "MM45Bytes.PhirnaF4CS1214", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS0512, "MM45Bytes.PhirnaF4CS0512", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS0712, "MM45Bytes.PhirnaF4CS0712", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS1312, "MM45Bytes.PhirnaF4CS1312", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS0607, "MM45Bytes.PhirnaF4CS0607", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS0807, "MM45Bytes.PhirnaF4CS0807", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS1207, "MM45Bytes.PhirnaF4CS1207", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS1204, "MM45Bytes.PhirnaF4CS1204", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4Surface, MM45Bytes.PhirnaF4CS0702, "MM45Bytes.PhirnaF4CS0702", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4WitchTowerLevel4, MM45Bytes.AlacornF4WT40704, "MM45Bytes.AlacornF4WT40704", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E4Surface, MM45Bytes.WhistleE4CS0514, "MM45Bytes.WhistleE4CS0514", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E4Surface, MM45Bytes.ReturnedAlacornF4CS0903, "MM45Bytes.ReturnedAlacornF4CS0903", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3Surface, MM45Bytes.ReturnedSkullD3CS1208, "MM45Bytes.ReturnedSkullD3CS1208", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2CastleBurlockLevel3, MM45Bytes.ReturnedTiaraD2CBL30211, "MM45Bytes.ReturnedTiaraD2CBL30211", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C2Surface, MM45Bytes.ReturnedScarabC2CS1006, "MM45Bytes.ReturnedScarabC2CS1006", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C2Surface, MM45Bytes.ReturnedCrystalsC2CS0811, "MM45Bytes.ReturnedCrystalsC2CS0811", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D4Surface, MM45Bytes.DeliveredElixirD4CS1203, "MM45Bytes.DeliveredElixirD4CS1203", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Surface, MM45Bytes.DeliveredBookC3CS0308, "MM45Bytes.DeliveredBookC3CS0308", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B3Surface, MM45Bytes.DeliveredRockB3CS0906, "MM45Bytes.DeliveredRockB3CS0906", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A1Surface, MM45Bytes.DeliveredScrollA1CS1105, "MM45Bytes.DeliveredScrollA1CS1105", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A3Surface, MM45Bytes.TurnInCyclopsA3CS1000, "MM45Bytes.TurnInCyclopsA3CS1000", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.A3WesternTowerLevel4, MM45Bytes.TurnInDreyfusA3WTL40410, "MM45Bytes.TurnInDreyfusA3WTL40410", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.B3Surface, MM45Bytes.TurnInTrollLairB3CS0603, "MM45Bytes.TurnInTrollLairB3CS0603", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2CastleBurlockLevel1, MM45Bytes.TurnInMirrorD2CBL10801, "MM45Bytes.TurnInMirrorD2CBL10801", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4NewcastleDungeon, MM45Bytes.FoundXeenSlayerSwordC4ND0704, "MM45Bytes.FoundXeenSlayerSwordC4ND0704", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.A4Surface, MM45Bytes.TurnInLunaA4DS1315, "MM45Bytes.TurnInLunaA4DS1315", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B1Surface, MM45Bytes.TalkedToAmbroseB1DS1205, "MM45Bytes.TalkedToAmbroseB1DS1205", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel2, MM45Bytes.TurnInSongbirdA4CKL21115, "MM45Bytes.TurnInSongbirdA4CKL21115", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B3Surface, MM45Bytes.TurnInOgresB3DS1104, "MM45Bytes.TurnInOgresB3DS1104", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B3Surface, MM45Bytes.TurnInVesparB3DS0701, "MM45Bytes.TurnInVesparB3DS0701", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B4Surface, MM45Bytes.BringMelon1B4DS0312, "MM45Bytes.BringMelon1B4DS0312", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B4Surface, MM45Bytes.BringMelon2B4DS0312, "MM45Bytes.BringMelon2B4DS0312", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Surface, MM45Bytes.FoundMelon1A4DS0804, "MM45Bytes.FoundMelon1A4DS0804", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Surface, MM45Bytes.FoundMelon2A4DS1403, "MM45Bytes.FoundMelon2A4DS1403", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Surface, MM45Bytes.FoundMelon3A4DS0301, "MM45Bytes.FoundMelon3A4DS0301", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B4Surface, MM45Bytes.FoundMelon4B4DS0104, "MM45Bytes.FoundMelon4B4DS0104", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4Surface, MM45Bytes.TurnInSheewanaC4DS0107, "MM45Bytes.TurnInSheewanaC4DS0107", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1Surface, MM45Bytes.TurnInChaliceD1DS0108, "MM45Bytes.TurnInChaliceD1DS0108", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E2Surface, MM45Bytes.TurnInEctorE2DS0312, "MM45Bytes.TurnInEctorE2DS0312", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Surface, MM45Bytes.TurnInCalebE3DS1313, "MM45Bytes.TurnInCalebE3DS1313", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F4Surface, MM45Bytes.TurnInJewelF4DS0607, "MM45Bytes.TurnInJewelF4DS0607", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TurnInGettleA4C2327, "MM45Bytes.TurnInGettleA4C2327", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TurnInJethroA4C2224, "MM45Bytes.TurnInJethroA4C2224", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.TurnInAstraE3S2014, "MM45Bytes.TurnInAstraE3S2014", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A3WesternTowerLevel1, MM45Bytes.EnergyDiskA3WTL10608, "MM45Bytes.EnergyDiskA3WTL10608", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A3WesternTowerLevel1, MM45Bytes.EnergyDiskA3WTL10808, "MM45Bytes.EnergyDiskA3WTL10808", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1NorthernTowerLevel4, MM45Bytes.EnergyDiskD1NTL40308, "MM45Bytes.EnergyDiskD1NTL40308", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1NorthernTowerLevel4, MM45Bytes.EnergyDiskD1NTL41108, "MM45Bytes.EnergyDiskD1NTL41108", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D4SouthernTowerLevel3, MM45Bytes.EnergyDiskD4STL30505, "MM45Bytes.EnergyDiskD4STL30505", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D4SouthernTowerLevel3, MM45Bytes.EnergyDiskD4STL30905, "MM45Bytes.EnergyDiskD4STL30905", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F3EasternTowerLevel3, MM45Bytes.EnergyDiskF3ETL31108, "MM45Bytes.EnergyDiskF3ETL31108", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F3EasternTowerLevel3, MM45Bytes.EnergyDiskF3ETL30704, "MM45Bytes.EnergyDiskF3ETL30704", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20015, "MM45Bytes.EnergyDiskA4CKL20015", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20215, "MM45Bytes.EnergyDiskA4CKL20215", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20415, "MM45Bytes.EnergyDiskA4CKL20415", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20815, "MM45Bytes.EnergyDiskA4CKL20815", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20012, "MM45Bytes.EnergyDiskA4CKL20012", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel2, MM45Bytes.EnergyDiskA4CKL20010, "MM45Bytes.EnergyDiskA4CKL20010", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.EnergyDiskC4TBL50018, "MM45Bytes.EnergyDiskC4TBL50018", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.EnergyDiskC4TBL53117, "MM45Bytes.EnergyDiskC4TBL53117", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4Surface, MM45Bytes.EnergyDiskC4DS0107, "MM45Bytes.EnergyDiskC4DS0107", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1Surface, MM45Bytes.EnergyDiskD1DS1005, "MM45Bytes.EnergyDiskD1DS1005", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D3Surface, MM45Bytes.EnergyDiskD3DS1105, "MM45Bytes.EnergyDiskD3DS1105", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.EnergyDiskA4C2913, "MM45Bytes.EnergyDiskA4C2913", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4EllingersTowerLevel4, MM45Bytes.TurnInDisksA4ETL40408, "MM45Bytes.TurnInDisksA4ETL40408", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel1, MM45Bytes.TurnInEggD1DTL10605, "MM45Bytes.TurnInEggD1DTL10605", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel1, MM45Bytes.FireScroll1A1CBL10313, "MM45Bytes.FireScroll1A1CBL10313", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel1, MM45Bytes.FireScroll2A1CBL10613, "MM45Bytes.FireScroll2A1CBL10613", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3TowerofHighMagicLevel1, MM45Bytes.FireBrew1C3THML10505, "MM45Bytes.FireBrew1C3THML10505", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3TowerofHighMagicLevel2, MM45Bytes.FireBrew2C3THML20505, "MM45Bytes.FireBrew2C3THML20505", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel1, MM45Bytes.ElectricityScroll1A1CBL10309, "MM45Bytes.ElectricityScroll1A1CBL10309", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel1, MM45Bytes.ElectricityScroll2A1CBL10609, "MM45Bytes.ElectricityScroll2A1CBL10609", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3TowerofHighMagicLevel1, MM45Bytes.ElectricBrew1C3THML10511, "MM45Bytes.ElectricBrew1C3THML10511", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3TowerofHighMagicLevel2, MM45Bytes.ElectricBrew2C3THML20406, "MM45Bytes.ElectricBrew2C3THML20406", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3TowerofHighMagicLevel1, MM45Bytes.ColdBrew1C3THML10911, "MM45Bytes.ColdBrew1C3THML10911", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3TowerofHighMagicLevel2, MM45Bytes.ColdBrew2C3THML20410, "MM45Bytes.ColdBrew2C3THML20410", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3TowerofHighMagicLevel2, MM45Bytes.PoisonBrew1C3THML20511, "MM45Bytes.PoisonBrew1C3THML20511", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3TowerofHighMagicLevel3, MM45Bytes.PoisonBrew2C3THML30410, "MM45Bytes.PoisonBrew2C3THML30410", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3TowerofHighMagicLevel3, MM45Bytes.PoisonBrew3C3THML31010, "MM45Bytes.PoisonBrew3C3THML31010", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel3, MM45Bytes.EnergyScroll1A1CBL31102, "MM45Bytes.EnergyScroll1A1CBL31102", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel3, MM45Bytes.EnergyScroll2A1CBL31201, "MM45Bytes.EnergyScroll2A1CBL31201", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3TowerofHighMagicLevel3, MM45Bytes.EnergyBrew1C3THML30406, "MM45Bytes.EnergyBrew1C3THML30406", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.MagicScroll1A1CBL21104, "MM45Bytes.MagicScroll1A1CBL21104", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel3, MM45Bytes.MagicScroll2A1CBL31404, "MM45Bytes.MagicScroll2A1CBL31404", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3TowerofHighMagicLevel3, MM45Bytes.MagicBrew1C3THML31006, "MM45Bytes.MagicBrew1C3THML31006", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2BurlockDungeon, MM45Bytes.RedLiquid1D2BD1103, "MM45Bytes.RedLiquid1D2BD1103", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.MightSkull1B4CIL11113, "MM45Bytes.MightSkull1B4CIL11113", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.MightSkull2B4CIL11201, "MM45Bytes.MightSkull2B4CIL11201", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel2, MM45Bytes.MightSkull3B4CIL20314, "MM45Bytes.MightSkull3B4CIL20314", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.MightSkull4B4CIL30114, "MM45Bytes.MightSkull4B4CIL30114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel4, MM45Bytes.MightSkull5B4CIL40114, "MM45Bytes.MightSkull5B4CIL40114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.MightJuice1C4TTT0930, "MM45Bytes.MightJuice1C4TTT0930", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.MightJuice2C4TTT1330, "MM45Bytes.MightJuice2C4TTT1330", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.MightJuice3C4TTT0126, "MM45Bytes.MightJuice3C4TTT0126", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.MightJuice4C4TTT0726, "MM45Bytes.MightJuice4C4TTT0726", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.MightLiquid2F3DM10507, "MM45Bytes.MightLiquid2F3DM10507", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.MightLiquid3F3DM10304, "MM45Bytes.MightLiquid3F3DM10304", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.MightLiquid4F3DM21026, "MM45Bytes.MightLiquid4F3DM21026", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.MightLiquid5E2DM30201, "MM45Bytes.MightLiquid5E2DM30201", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.MightLiquid6E2DM30301, "MM45Bytes.MightLiquid6E2DM30301", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.MightLiquid7E2DM30501, "MM45Bytes.MightLiquid7E2DM30501", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.MightLiquid8E2DM30601, "MM45Bytes.MightLiquid8E2DM30601", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.MightBookD3DTL21006, "MM45Bytes.MightBookD3DTL21006", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel3, MM45Bytes.MightBrew1A4CKL30802, "MM45Bytes.MightBrew1A4CKL30802", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel3, MM45Bytes.MightBrew2A4CKL30902, "MM45Bytes.MightBrew2A4CKL30902", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel3, MM45Bytes.MightBrew3A4CKL30801, "MM45Bytes.MightBrew3A4CKL30801", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel3, MM45Bytes.MightBrew4A4CKL30901, "MM45Bytes.MightBrew4A4CKL30901", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel3, MM45Bytes.MightBrew5A4CKL30900, "MM45Bytes.MightBrew5A4CKL30900", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.MightPotion1aC4TBL10215, "MM45Bytes.MightPotion1aC4TBL10215", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.MightPotion1bC4TBL10215, "MM45Bytes.MightPotion1bC4TBL10215", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.MightPotion1cC4TBL10215, "MM45Bytes.MightPotion1cC4TBL10215", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.MightPotion2aC4TBL21106, "MM45Bytes.MightPotion2aC4TBL21106", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.MightPotion2bC4TBL21106, "MM45Bytes.MightPotion2bC4TBL21106", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.MightPotion2cC4TBL21106, "MM45Bytes.MightPotion2cC4TBL21106", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.MightMagpie1F2LSDL53125, "MM45Bytes.MightMagpie1F2LSDL53125", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.MightMagpie2F2LSDL52917, "MM45Bytes.MightMagpie2F2LSDL52917", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4Surface, MM45Bytes.MightAppleE4DS1313, "MM45Bytes.MightAppleE4DS1313", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.MightLiquid9A4CS0226, "MM45Bytes.MightLiquid9A4CS0226", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.MightLiquid10A4CS0426, "MM45Bytes.MightLiquid10A4CS0426", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.MightLiquid11A4CS0225, "MM45Bytes.MightLiquid11A4CS0225", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.MightLiquid12A4CS0425, "MM45Bytes.MightLiquid12A4CS0425", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6aE3SS3004, "MM45Bytes.MightBrew6aE3SS3004", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6bE3SS3004, "MM45Bytes.MightBrew6bE3SS3004", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6cE3SS3004, "MM45Bytes.MightBrew6cE3SS3004", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6dE3SS3004, "MM45Bytes.MightBrew6dE3SS3004", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6eE3SS3004, "MM45Bytes.MightBrew6eE3SS3004", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.MightBrew6fE3SS3004, "MM45Bytes.MightBrew6fE3SS3004", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.IntellectScroll1A1CBL20508, "MM45Bytes.IntellectScroll1A1CBL20508", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.IntellectScroll2A1CBL20708, "MM45Bytes.IntellectScroll2A1CBL20708", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.IntellectSkull1B4CIL11413, "MM45Bytes.IntellectSkull1B4CIL11413", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.IntellectSkull2B4CIL10400, "MM45Bytes.IntellectSkull2B4CIL10400", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel2, MM45Bytes.IntellectSkull3B4CIL20613, "MM45Bytes.IntellectSkull3B4CIL20613", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.IntellectSkull4B4CIL30407, "MM45Bytes.IntellectSkull4B4CIL30407", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.IntellectSkull5B4CIL31003, "MM45Bytes.IntellectSkull5B4CIL31003", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.IntellectJuice1C4TTT1219, "MM45Bytes.IntellectJuice1C4TTT1219", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.IntellectJuice2C4TTT1513, "MM45Bytes.IntellectJuice2C4TTT1513", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.IntellectLiquid1F3DM10823, "MM45Bytes.IntellectLiquid1F3DM10823", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.IntellectLiquid2F3DM10722, "MM45Bytes.IntellectLiquid2F3DM10722", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.IntellectLiquid3F3DM21123, "MM45Bytes.IntellectLiquid3F3DM21123", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.IntellectBook1D3DTL20905, "MM45Bytes.IntellectBook1D3DTL20905", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion1aC4TBL21015, "MM45Bytes.IntellectPotion1aC4TBL21015", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion1bC4TBL21015, "MM45Bytes.IntellectPotion1bC4TBL21015", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion1cC4TBL21015, "MM45Bytes.IntellectPotion1cC4TBL21015", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion2aC4TBL21404, "MM45Bytes.IntellectPotion2aC4TBL21404", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion2bC4TBL21404, "MM45Bytes.IntellectPotion2bC4TBL21404", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.IntellectPotion2cC4TBL21404, "MM45Bytes.IntellectPotion2cC4TBL21404", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4Surface, MM45Bytes.IntellectOrangeC4DS0614, "MM45Bytes.IntellectOrangeC4DS0614", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion3aE3S2310, "MM45Bytes.IntellectPotion3aE3S2310", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion3bE3S2310, "MM45Bytes.IntellectPotion3bE3S2310", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion3cE3S2310, "MM45Bytes.IntellectPotion3cE3S2310", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion4aE3S1305, "MM45Bytes.IntellectPotion4aE3S1305", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion4bE3S1305, "MM45Bytes.IntellectPotion4bE3S1305", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion4cE3S1305, "MM45Bytes.IntellectPotion4cE3S1305", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion5aE3S1303, "MM45Bytes.IntellectPotion5aE3S1303", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion5bE3S1303, "MM45Bytes.IntellectPotion5bE3S1303", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.IntellectPotion5cE3S1303, "MM45Bytes.IntellectPotion5cE3S1303", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.PersonalityScroll1A1CBL20514, "MM45Bytes.PersonalityScroll1A1CBL20514", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.PersonalityScroll2A1CBL20714, "MM45Bytes.PersonalityScroll2A1CBL20714", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.PersonalitySkull1B4CIL11506, "MM45Bytes.PersonalitySkull1B4CIL11506", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.PersonalitySkull2B4CIL10101, "MM45Bytes.PersonalitySkull2B4CIL10101", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel2, MM45Bytes.PersonalitySkull3B4CIL20109, "MM45Bytes.PersonalitySkull3B4CIL20109", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.PersonalitySkull4B4CIL30103, "MM45Bytes.PersonalitySkull4B4CIL30103", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.PersonalitySkull5B4CIL31000, "MM45Bytes.PersonalitySkull5B4CIL31000", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.PersonalityJuice1C4TTT1105, "MM45Bytes.PersonalityJuice1C4TTT1105", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.PersonalityJuice2C4TTT1505, "MM45Bytes.PersonalityJuice2C4TTT1505", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.PersonalityLiquid1F3DM10621, "MM45Bytes.PersonalityLiquid1F3DM10621", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.PersonalityLiquid2F3DM10620, "MM45Bytes.PersonalityLiquid2F3DM10620", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.PersonalityLiquid3F3DM21125, "MM45Bytes.PersonalityLiquid3F3DM21125", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.PersonalityBookD3DTL20505, "MM45Bytes.PersonalityBookD3DTL20505", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel3, MM45Bytes.PersonalityBrew1A4CKL30815, "MM45Bytes.PersonalityBrew1A4CKL30815", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel3, MM45Bytes.PersonalityBrew2A4CKL30915, "MM45Bytes.PersonalityBrew2A4CKL30915", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleKalindraLevel3, MM45Bytes.PersonalityBrew3A4CKL30914, "MM45Bytes.PersonalityBrew3A4CKL30914", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.PersonalityPotion1aC4TBL10211, "MM45Bytes.PersonalityPotion1aC4TBL10211", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.PersonalityPotion1bC4TBL10211, "MM45Bytes.PersonalityPotion1bC4TBL10211", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel1, MM45Bytes.PersonalityPotion1cC4TBL10211, "MM45Bytes.PersonalityPotion1cC4TBL10211", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.PersonalityPotion2aC4TBL21204, "MM45Bytes.PersonalityPotion2aC4TBL21204", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.PersonalityPotion2bC4TBL21204, "MM45Bytes.PersonalityPotion2bC4TBL21204", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.PersonalityPotion2cC4TBL21204, "MM45Bytes.PersonalityPotion2cC4TBL21204", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.PersonalityParakeet1F2LSDL52024, "MM45Bytes.PersonalityParakeet1F2LSDL52024", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.PersonalityParakeet2F2LSDL52011, "MM45Bytes.PersonalityParakeet2F2LSDL52011", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D4Surface, MM45Bytes.PersonalityBlueberriesD4DS0612, "MM45Bytes.PersonalityBlueberriesD4DS0612", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2Lakeside, MM45Bytes.PersonalityCauldronF2L1401, "MM45Bytes.PersonalityCauldronF2L1401", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.EnduranceSkull1B4CIL10306, "MM45Bytes.EnduranceSkull1B4CIL10306", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.EnduranceSkull2B4CIL10805, "MM45Bytes.EnduranceSkull2B4CIL10805", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.EnduranceSkull3B4CIL30202, "MM45Bytes.EnduranceSkull3B4CIL30202", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel4, MM45Bytes.EnduranceSkull4B4CIL41410, "MM45Bytes.EnduranceSkull4B4CIL41410", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel4, MM45Bytes.EnduranceSkull5B4CIL41103, "MM45Bytes.EnduranceSkull5B4CIL41103", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.EnduranceJuice1C4TTT0122, "MM45Bytes.EnduranceJuice1C4TTT0122", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.EnduranceJuice2C4TTT0722, "MM45Bytes.EnduranceJuice2C4TTT0722", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.EnduranceJuice3C4TTT0817, "MM45Bytes.EnduranceJuice3C4TTT0817", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.EnduranceLiquid1F3DM10817, "MM45Bytes.EnduranceLiquid1F3DM10817", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.EnduranceLiquid2F3DM10917, "MM45Bytes.EnduranceLiquid2F3DM10917", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.EnduranceLiquid3F3DM11017, "MM45Bytes.EnduranceLiquid3F3DM11017", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.EnduranceLiquid4F3DM21022, "MM45Bytes.EnduranceLiquid4F3DM21022", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion1aD1DTL30511, "MM45Bytes.EndurancePotion1aD1DTL30511", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion1bD1DTL30511, "MM45Bytes.EndurancePotion1bD1DTL30511", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion1cD1DTL30511, "MM45Bytes.EndurancePotion1cD1DTL30511", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion1dD1DTL30511, "MM45Bytes.EndurancePotion1dD1DTL30511", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion2aD1DTL30911, "MM45Bytes.EndurancePotion2aD1DTL30911", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion2bD1DTL30911, "MM45Bytes.EndurancePotion2bD1DTL30911", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion2cD1DTL30911, "MM45Bytes.EndurancePotion2cD1DTL30911", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel3, MM45Bytes.EndurancePotion2dD1DTL30911, "MM45Bytes.EndurancePotion2dD1DTL30911", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.EnduranceBookD3DTL20406, "MM45Bytes.EnduranceBookD3DTL20406", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion3aC4TBL21515, "MM45Bytes.EndurancePotion3aC4TBL21515", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion3bC4TBL21515, "MM45Bytes.EndurancePotion3bC4TBL21515", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion3cC4TBL21515, "MM45Bytes.EndurancePotion3cC4TBL21515", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion4aC4TBL21503, "MM45Bytes.EndurancePotion4aC4TBL21503", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion4bC4TBL21503, "MM45Bytes.EndurancePotion4bC4TBL21503", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.EndurancePotion4cC4TBL21503, "MM45Bytes.EndurancePotion4cC4TBL21503", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.EnduranceEagle1F2LSDL52731, "MM45Bytes.EnduranceEagle1F2LSDL52731", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.EnduranceEagle2F2LSDL52615, "MM45Bytes.EnduranceEagle2F2LSDL52615", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.EnduranceEagle3F2LSDL53115, "MM45Bytes.EnduranceEagle3F2LSDL53115", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4Surface, MM45Bytes.EndurancePearC4DS1304, "MM45Bytes.EndurancePearC4DS1304", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid5A4CS2008, "MM45Bytes.EnduranceLiquid5A4CS2008", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid6A4CS2208, "MM45Bytes.EnduranceLiquid6A4CS2208", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid7A4CS2007, "MM45Bytes.EnduranceLiquid7A4CS2007", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid8A4CS2207, "MM45Bytes.EnduranceLiquid8A4CS2207", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid9A4CS2006, "MM45Bytes.EnduranceLiquid9A4CS2006", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.EnduranceLiquid10A4CS2206, "MM45Bytes.EnduranceLiquid10A4CS2206", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBrewaE3SS2903, "MM45Bytes.EnduranceBrewaE3SS2903", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBrewbE3SS2903, "MM45Bytes.EnduranceBrewbE3SS2903", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBrewcE3SS2903, "MM45Bytes.EnduranceBrewcE3SS2903", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBrewdE3SS2903, "MM45Bytes.EnduranceBrewdE3SS2903", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBreweE3SS2903, "MM45Bytes.EnduranceBreweE3SS2903", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3SandcasterSewer, MM45Bytes.EnduranceBrewfE3SS2903, "MM45Bytes.EnduranceBrewfE3SS2903", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2Lakeside, MM45Bytes.EnduranceCauldronF2L0905, "MM45Bytes.EnduranceCauldronF2L0905", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.SpeedScroll1A1CBL20712, "MM45Bytes.SpeedScroll1A1CBL20712", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A1CastleBasenjiLevel2, MM45Bytes.SpeedScroll2A1CBL20710, "MM45Bytes.SpeedScroll2A1CBL20710", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.SpeedSkull1B4CIL10908, "MM45Bytes.SpeedSkull1B4CIL10908", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.SpeedSkull2B4CIL10506, "MM45Bytes.SpeedSkull2B4CIL10506", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel2, MM45Bytes.SpeedSkull3B4CIL20600, "MM45Bytes.SpeedSkull3B4CIL20600", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.SpeedSkull4B4CIL30500, "MM45Bytes.SpeedSkull4B4CIL30500", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.SpeedJuice1C4TTT1619, "MM45Bytes.SpeedJuice1C4TTT1619", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.SpeedJuice2C4TTT1819, "MM45Bytes.SpeedJuice2C4TTT1819", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.SpeedLiquid1F3DM10921, "MM45Bytes.SpeedLiquid1F3DM10921", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.SpeedLiquid2F3DM10920, "MM45Bytes.SpeedLiquid2F3DM10920", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.SpeedLiquid3F3DM20403, "MM45Bytes.SpeedLiquid3F3DM20403", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.SpeedLiquid4F3DM20402, "MM45Bytes.SpeedLiquid4F3DM20402", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.SpeedLiquid5E2DM30200, "MM45Bytes.SpeedLiquid5E2DM30200", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.SpeedLiquid6E2DM30300, "MM45Bytes.SpeedLiquid6E2DM30300", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.SpeedLiquid7D2DM50807, "MM45Bytes.SpeedLiquid7D2DM50807", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.SpeedLiquid8D2DM50503, "MM45Bytes.SpeedLiquid8D2DM50503", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.SpeedBookD3DTL20410, "MM45Bytes.SpeedBookD3DTL20410", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion1aC4TBL21407, "MM45Bytes.SpeedPotion1aC4TBL21407", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion1bC4TBL21407, "MM45Bytes.SpeedPotion1bC4TBL21407", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion1cC4TBL21407, "MM45Bytes.SpeedPotion1cC4TBL21407", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion2aC4TBL21201, "MM45Bytes.SpeedPotion2aC4TBL21201", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion2bC4TBL21201, "MM45Bytes.SpeedPotion2bC4TBL21201", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.SpeedPotion2cC4TBL21201, "MM45Bytes.SpeedPotion2cC4TBL21201", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.SpeedSparrow1F2LSDL52431, "MM45Bytes.SpeedSparrow1F2LSDL52431", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.SpeedSparrow2F2LSDL53110, "MM45Bytes.SpeedSparrow2F2LSDL53110", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4Surface, MM45Bytes.SpeedPlumC4DS0612, "MM45Bytes.SpeedPlumC4DS0612", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion3aE3S0110, "MM45Bytes.SpeedPotion3aE3S0110", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion3bE3S0110, "MM45Bytes.SpeedPotion3bE3S0110", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion3cE3S0110, "MM45Bytes.SpeedPotion3cE3S0110", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion4aE3S2510, "MM45Bytes.SpeedPotion4aE3S2510", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion4bE3S2510, "MM45Bytes.SpeedPotion4bE3S2510", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion4cE3S2510, "MM45Bytes.SpeedPotion4cE3S2510", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion5aE3S0801, "MM45Bytes.SpeedPotion5aE3S0801", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion5bE3S0801, "MM45Bytes.SpeedPotion5bE3S0801", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3Sandcaster, MM45Bytes.SpeedPotion5cE3S0801, "MM45Bytes.SpeedPotion5cE3S0801", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2Lakeside, MM45Bytes.SpeedCauldron1F2L0612, "MM45Bytes.SpeedCauldron1F2L0612", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2Lakeside, MM45Bytes.SpeedCauldron2F2L1404, "MM45Bytes.SpeedCauldron2F2L1404", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.AccuracySkull1B4CIL10011, "MM45Bytes.AccuracySkull1B4CIL10011", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.AccuracySkull2B4CIL11004, "MM45Bytes.AccuracySkull2B4CIL11004", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel2, MM45Bytes.AccuracySkull3B4CIL20305, "MM45Bytes.AccuracySkull3B4CIL20305", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.AccuracySkull4B4CIL30401, "MM45Bytes.AccuracySkull4B4CIL30401", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel4, MM45Bytes.AccuracySkull5B4CIL40110, "MM45Bytes.AccuracySkull5B4CIL40110", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel4, MM45Bytes.AccuracySkull6B4CIL41303, "MM45Bytes.AccuracySkull6B4CIL41303", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.AccuracyJuice1C4TTT0124, "MM45Bytes.AccuracyJuice1C4TTT0124", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.AccuracyJuice2C4TTT0724, "MM45Bytes.AccuracyJuice2C4TTT0724", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.AccuracyJuice3C4TTT0811, "MM45Bytes.AccuracyJuice3C4TTT0811", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.AccuracyJuice4C4TTT0807, "MM45Bytes.AccuracyJuice4C4TTT0807", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.AccuracyLiquid1F3DM11023, "MM45Bytes.AccuracyLiquid1F3DM11023", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.AccuracyLiquid2F3DM11120, "MM45Bytes.AccuracyLiquid2F3DM11120", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.AccuracyLiquid3F3DM20401, "MM45Bytes.AccuracyLiquid3F3DM20401", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.AccuracyLiquid4F3DM20501, "MM45Bytes.AccuracyLiquid4F3DM20501", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.AccuracyLiquid5D2DM50602, "MM45Bytes.AccuracyLiquid5D2DM50602", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.AccuracyLiquid6D2DM50702, "MM45Bytes.AccuracyLiquid6D2DM50702", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3DarzogsTowerLevel2, MM45Bytes.AccuracyBookD3DTL20511, "MM45Bytes.AccuracyBookD3DTL20511", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion1aC4TBL21409, "MM45Bytes.AccuracyPotion1aC4TBL21409", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion1bC4TBL21409, "MM45Bytes.AccuracyPotion1bC4TBL21409", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion1cC4TBL21409, "MM45Bytes.AccuracyPotion1cC4TBL21409", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion2aC4TBL21401, "MM45Bytes.AccuracyPotion2aC4TBL21401", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion2bC4TBL21401, "MM45Bytes.AccuracyPotion2bC4TBL21401", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.AccuracyPotion2cC4TBL21401, "MM45Bytes.AccuracyPotion2cC4TBL21401", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.AccuracyAlbatross1F2LSDL52630, "MM45Bytes.AccuracyAlbatross1F2LSDL52630", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.AccuracyAlbatross2F2LSDL52919, "MM45Bytes.AccuracyAlbatross2F2LSDL52919", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4Surface, MM45Bytes.AccuracyBananaE4DS0504, "MM45Bytes.AccuracyBananaE4DS0504", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.LuckSkull1B4CIL10014, "MM45Bytes.LuckSkull1B4CIL10014", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel1, MM45Bytes.LuckSkull2B4CIL11304, "MM45Bytes.LuckSkull2B4CIL11304", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.LuckSkull3B4CIL31511, "MM45Bytes.LuckSkull3B4CIL31511", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.LuckSkull4B4CIL31405, "MM45Bytes.LuckSkull4B4CIL31405", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.LuckSkull5B4CIL31503, "MM45Bytes.LuckSkull5B4CIL31503", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4CaveOfIllusionLevel3, MM45Bytes.LuckSkull6B4CIL31202, "MM45Bytes.LuckSkull6B4CIL31202", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.LuckJuice1C4TTT2408, "MM45Bytes.LuckJuice1C4TTT2408", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4TombOfAThousandTerrors, MM45Bytes.LuckJuice2C4TTT2608, "MM45Bytes.LuckJuice2C4TTT2608", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.LuckLiquid1F3DM10207, "MM45Bytes.LuckLiquid1F3DM10207", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.LuckLiquid2F3DM10206, "MM45Bytes.LuckLiquid2F3DM10206", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.LuckLiquid3F3DM20606, "MM45Bytes.LuckLiquid3F3DM20606", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine4, MM45Bytes.LuckLiquid4E2DM40805, "MM45Bytes.LuckLiquid4E2DM40805", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine4, MM45Bytes.LuckLiquid5E2DM40804, "MM45Bytes.LuckLiquid5E2DM40804", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.LuckPotionaC4TBL21306, "MM45Bytes.LuckPotionaC4TBL21306", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.LuckPotionbC4TBL21306, "MM45Bytes.LuckPotionbC4TBL21306", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.LuckPotioncC4TBL21306, "MM45Bytes.LuckPotioncC4TBL21306", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.LuckLark1F2LSDL52628, "MM45Bytes.LuckLark1F2LSDL52628", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel5, MM45Bytes.LuckLark2F2LSDL52915, "MM45Bytes.LuckLark2F2LSDL52915", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F4Surface, MM45Bytes.LuckCoconutF4DS0215, "MM45Bytes.LuckCoconutF4DS0215", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal1D1DC1327, "MM45Bytes.LevelCrystal1D1DC1327", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal2D1DC1325, "MM45Bytes.LevelCrystal2D1DC1325", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal3D1DC1323, "MM45Bytes.LevelCrystal3D1DC1323", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal4D1DC2423, "MM45Bytes.LevelCrystal4D1DC2423", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal5D1DC0622, "MM45Bytes.LevelCrystal5D1DC0622", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal6D1DC1321, "MM45Bytes.LevelCrystal6D1DC1321", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal7D1DC1721, "MM45Bytes.LevelCrystal7D1DC1721", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal8D1DC0217, "MM45Bytes.LevelCrystal8D1DC0217", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal9D1DC0817, "MM45Bytes.LevelCrystal9D1DC0817", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal10D1DC2217, "MM45Bytes.LevelCrystal10D1DC2217", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal11D1DC2817, "MM45Bytes.LevelCrystal11D1DC2817", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal12D1DC0216, "MM45Bytes.LevelCrystal12D1DC0216", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal13D1DC2816, "MM45Bytes.LevelCrystal13D1DC2816", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal14D1DC0215, "MM45Bytes.LevelCrystal14D1DC0215", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal15D1DC2815, "MM45Bytes.LevelCrystal15D1DC2815", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal16D1DC0214, "MM45Bytes.LevelCrystal16D1DC0214", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal17D1DC2814, "MM45Bytes.LevelCrystal17D1DC2814", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal18D1DC0213, "MM45Bytes.LevelCrystal18D1DC0213", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal19D1DC2813, "MM45Bytes.LevelCrystal19D1DC2813", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal20D1DC2410, "MM45Bytes.LevelCrystal20D1DC2410", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonClouds, MM45Bytes.LevelCrystal21D1DC0709, "MM45Bytes.LevelCrystal21D1DC0709", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer1A2SSL30015, "MM45Bytes.LevelEmbalmer1A2SSL30015", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer2A2SSL31315, "MM45Bytes.LevelEmbalmer2A2SSL31315", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer3A2SSL31515, "MM45Bytes.LevelEmbalmer3A2SSL31515", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer4A2SSL30013, "MM45Bytes.LevelEmbalmer4A2SSL30013", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer5A2SSL31210, "MM45Bytes.LevelEmbalmer5A2SSL31210", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer6A2SSL31405, "MM45Bytes.LevelEmbalmer6A2SSL31405", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer7A2SSL31502, "MM45Bytes.LevelEmbalmer7A2SSL31502", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer8A2SSL30201, "MM45Bytes.LevelEmbalmer8A2SSL30201", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer9A2SSL30000, "MM45Bytes.LevelEmbalmer9A2SSL30000", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.LevelEmbalmer10A2SSL31500, "MM45Bytes.LevelEmbalmer10A2SSL31500", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice1aA1CAD1015, "MM45Bytes.LevelJuice1aA1CAD1015", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice1bA1CAD1015, "MM45Bytes.LevelJuice1bA1CAD1015", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice2aA1CAD1113, "MM45Bytes.LevelJuice2aA1CAD1113", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice2bA1CAD1113, "MM45Bytes.LevelJuice2bA1CAD1113", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice2cA1CAD1113, "MM45Bytes.LevelJuice2cA1CAD1113", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice3aA1CAD1513, "MM45Bytes.LevelJuice3aA1CAD1513", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice3bA1CAD1513, "MM45Bytes.LevelJuice3bA1CAD1513", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice3cA1CAD1513, "MM45Bytes.LevelJuice3cA1CAD1513", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice4aA1CAD1011, "MM45Bytes.LevelJuice4aA1CAD1011", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice4bA1CAD1011, "MM45Bytes.LevelJuice4bA1CAD1011", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice5aA1CAD1007, "MM45Bytes.LevelJuice5aA1CAD1007", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarDungeon, MM45Bytes.LevelJuice5bA1CAD1007, "MM45Bytes.LevelJuice5bA1CAD1007", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarLevel3, MM45Bytes.LevelJuice6aA1CAL30410, "MM45Bytes.LevelJuice6aA1CAL30410", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A1CastleAlamarLevel3, MM45Bytes.LevelJuice6bA1CAL30410, "MM45Bytes.LevelJuice6bA1CAL30410", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood1B2NS1414, "MM45Bytes.LevelFood1B2NS1414", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood2B2NS0911, "MM45Bytes.LevelFood2B2NS0911", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood3B2NS1011, "MM45Bytes.LevelFood3B2NS1011", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood4B2NS0910, "MM45Bytes.LevelFood4B2NS0910", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood5B2NS1102, "MM45Bytes.LevelFood5B2NS1102", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood6B2NS0201, "MM45Bytes.LevelFood6B2NS0201", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2NecropolisSewer, MM45Bytes.LevelFood7B2NS0801, "MM45Bytes.LevelFood7B2NS0801", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.StatsSludge1C4TBL20114, "MM45Bytes.StatsSludge1C4TBL20114", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.StatsSludge2C4TBL20214, "MM45Bytes.StatsSludge2C4TBL20214", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.StatsSludge3C4TBL20113, "MM45Bytes.StatsSludge3C4TBL20113", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel2, MM45Bytes.StatsSludge4C4TBL20213, "MM45Bytes.StatsSludge4C4TBL20213", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice1E4TH2031, "MM45Bytes.StatsJuice1E4TH2031", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice2E4TH2131, "MM45Bytes.StatsJuice2E4TH2131", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice3E4TH2431, "MM45Bytes.StatsJuice3E4TH2431", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice4E4TH2531, "MM45Bytes.StatsJuice4E4TH2531", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice5E4TH2030, "MM45Bytes.StatsJuice5E4TH2030", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice6E4TH2530, "MM45Bytes.StatsJuice6E4TH2530", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice7E4TH0722, "MM45Bytes.StatsJuice7E4TH0722", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice8E4TH0822, "MM45Bytes.StatsJuice8E4TH0822", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice9E4TH1418, "MM45Bytes.StatsJuice9E4TH1418", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice10E4TH1417, "MM45Bytes.StatsJuice10E4TH1417", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice11E4TH2511, "MM45Bytes.StatsJuice11E4TH2511", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice12E4TH0110, "MM45Bytes.StatsJuice12E4TH0110", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice13E4TH0210, "MM45Bytes.StatsJuice13E4TH0210", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice14E4TH0910, "MM45Bytes.StatsJuice14E4TH0910", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice15E4TH1010, "MM45Bytes.StatsJuice15E4TH1010", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice16E4TH2510, "MM45Bytes.StatsJuice16E4TH2510", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice17E4TH1509, "MM45Bytes.StatsJuice17E4TH1509", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice18E4TH1609, "MM45Bytes.StatsJuice18E4TH1609", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.StatsJuice19E4TH1709, "MM45Bytes.StatsJuice19E4TH1709", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.TalkValioA4CS3126, "MM45Bytes.TalkValioA4CS3126", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4CastleviewSewer, MM45Bytes.ReturnValioA4CS3126, "MM45Bytes.ReturnValioA4CS3126", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C2Surface, MM45Bytes.PaladinC2DS1105, "MM45Bytes.PaladinC2DS1105", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C2Surface, MM45Bytes.PaladinC2DS1100, "MM45Bytes.PaladinC2DS1100", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D2Surface, MM45Bytes.PaladinD2DS0010, "MM45Bytes.PaladinD2DS0010", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D2Surface, MM45Bytes.PaladinD2DS0000, "MM45Bytes.PaladinD2DS0000", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D2Surface, MM45Bytes.PaladinD2DS0510, "MM45Bytes.PaladinD2DS0510", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D2Surface, MM45Bytes.PaladinD2DS0505, "MM45Bytes.PaladinD2DS0505", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D2Surface, MM45Bytes.PaladinD2DS0500, "MM45Bytes.PaladinD2DS0500", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.F3Vertigo, MM45Bytes.Case1F3V1003, "MM45Bytes.Case1F3V1003", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3Vertigo, MM45Bytes.Case2F3V1005, "MM45Bytes.Case2F3V1005", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3Vertigo, MM45Bytes.Case3F3V1103, "MM45Bytes.Case3F3V1103", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3Vertigo, MM45Bytes.Case4F3V1105, "MM45Bytes.Case4F3V1105", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3Vertigo, MM45Bytes.Case5F3V1212, "MM45Bytes.Case5F3V1212", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3Vertigo, MM45Bytes.Case6F3V0812, "MM45Bytes.Case6F3V0812", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3Vertigo, MM45Bytes.Case7F3V0903, "MM45Bytes.Case7F3V0903", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3Vertigo, MM45Bytes.Case8F3V0905, "MM45Bytes.Case8F3V0905", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel1, MM45Bytes.WordMasterE3DOD12000, "MM45Bytes.WordMasterE3DOD12000", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel3, MM45Bytes.TTLeverE3DOD31703, "MM45Bytes.TTLeverE3DOD31703", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.GoldF3DM10230, "MM45Bytes.GoldF3DM10230", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.GoldF3DM10930, "MM45Bytes.GoldF3DM10930", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.GoldF3DM10525, "MM45Bytes.GoldF3DM10525", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.GoldF3DM11430, "MM45Bytes.GoldF3DM11430", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.Gold2F3DM11430, "MM45Bytes.Gold2F3DM11430", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.GoldF3DM10529, "MM45Bytes.GoldF3DM10529", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine1, MM45Bytes.Gold2F3DM10529, "MM45Bytes.Gold2F3DM10529", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.GoldF3DM20112, "MM45Bytes.GoldF3DM20112", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.GoldF3DM21217, "MM45Bytes.GoldF3DM21217", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.Gold2F3DM21217, "MM45Bytes.Gold2F3DM21217", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.GoldF3DM20103, "MM45Bytes.GoldF3DM20103", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.Gold2F3DM20103, "MM45Bytes.Gold2F3DM20103", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.GoldF3DM21301, "MM45Bytes.GoldF3DM21301", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.Gold2F3DM21301, "MM45Bytes.Gold2F3DM21301", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.GoldF3DM21414, "MM45Bytes.GoldF3DM21414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.Gold2F3DM21414, "MM45Bytes.Gold2F3DM21414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3DwarfMine2, MM45Bytes.Gold3F3DM21414, "MM45Bytes.Gold3F3DM21414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31114, "MM45Bytes.GoldE2DM31114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31714, "MM45Bytes.GoldE2DM31714", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM32010, "MM45Bytes.GoldE2DM32010", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31206, "MM45Bytes.GoldE2DM31206", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31202, "MM45Bytes.GoldE2DM31202", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31414, "MM45Bytes.GoldE2DM31414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM31414, "MM45Bytes.Gold2E2DM31414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM33014, "MM45Bytes.GoldE2DM33014", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM33014, "MM45Bytes.Gold2E2DM33014", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold3E2DM33014, "MM45Bytes.Gold3E2DM33014", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM31807, "MM45Bytes.GoldE2DM31807", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM31807, "MM45Bytes.Gold2E2DM31807", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold3E2DM31807, "MM45Bytes.Gold3E2DM31807", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM32907, "MM45Bytes.GoldE2DM32907", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM32907, "MM45Bytes.Gold2E2DM32907", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold3E2DM32907, "MM45Bytes.Gold3E2DM32907", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM32902, "MM45Bytes.GoldE2DM32902", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM32902, "MM45Bytes.Gold2E2DM32902", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold3E2DM32902, "MM45Bytes.Gold3E2DM32902", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.GoldE2DM33009, "MM45Bytes.GoldE2DM33009", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold2E2DM33009, "MM45Bytes.Gold2E2DM33009", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold3E2DM33009, "MM45Bytes.Gold3E2DM33009", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine3, MM45Bytes.Gold4E2DM33009, "MM45Bytes.Gold4E2DM33009", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine4, MM45Bytes.GoldE2DM40414, "MM45Bytes.GoldE2DM40414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine4, MM45Bytes.Gold2E2DM40414, "MM45Bytes.Gold2E2DM40414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine4, MM45Bytes.Gold3E2DM40414, "MM45Bytes.Gold3E2DM40414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine4, MM45Bytes.GoldE2DM40510, "MM45Bytes.GoldE2DM40510", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine4, MM45Bytes.Gold2E2DM40510, "MM45Bytes.Gold2E2DM40510", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine4, MM45Bytes.Gold3E2DM40510, "MM45Bytes.Gold3E2DM40510", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2DwarfMine4, MM45Bytes.Gold4E2DM40510, "MM45Bytes.Gold4E2DM40510", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.GoldD2DM50114, "MM45Bytes.GoldD2DM50114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.Gold2D2DM50114, "MM45Bytes.Gold2D2DM50114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.Gold3D2DM50114, "MM45Bytes.Gold3D2DM50114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.GoldD2DM51002, "MM45Bytes.GoldD2DM51002", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.Gold2D2DM51002, "MM45Bytes.Gold2D2DM51002", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.Gold3D2DM51002, "MM45Bytes.Gold3D2DM51002", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.GoldD2DM51010, "MM45Bytes.GoldD2DM51010", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.Gold2D2DM51010, "MM45Bytes.Gold2D2DM51010", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.Gold3D2DM51010, "MM45Bytes.Gold3D2DM51010", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D2DwarfMine5, MM45Bytes.Gold4D2DM51010, "MM45Bytes.Gold4D2DM51010", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA3129, "MM45Bytes.GoldDMA3129", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA3129, "MM45Bytes.Gold2DMA3129", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA1521, "MM45Bytes.GoldDMA1521", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA1521, "MM45Bytes.Gold2DMA1521", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA2915, "MM45Bytes.GoldDMA2915", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA2915, "MM45Bytes.Gold2DMA2915", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA2906, "MM45Bytes.GoldDMA2906", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA2906, "MM45Bytes.Gold2DMA2906", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0202, "MM45Bytes.GoldDMA0202", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0202, "MM45Bytes.Gold2DMA0202", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA1202, "MM45Bytes.GoldDMA1202", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA1202, "MM45Bytes.Gold2DMA1202", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA1931, "MM45Bytes.GoldDMA1931", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA1931, "MM45Bytes.Gold2DMA1931", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA1931, "MM45Bytes.Gold3DMA1931", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0130, "MM45Bytes.GoldDMA0130", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0130, "MM45Bytes.Gold2DMA0130", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA0130, "MM45Bytes.Gold3DMA0130", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA1530, "MM45Bytes.GoldDMA1530", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA1530, "MM45Bytes.Gold2DMA1530", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA1530, "MM45Bytes.Gold3DMA1530", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA3020, "MM45Bytes.GoldDMA3020", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA3020, "MM45Bytes.Gold2DMA3020", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA3020, "MM45Bytes.Gold3DMA3020", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA2217, "MM45Bytes.GoldDMA2217", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA2217, "MM45Bytes.Gold2DMA2217", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA2217, "MM45Bytes.Gold3DMA2217", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0108, "MM45Bytes.GoldDMA0108", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0108, "MM45Bytes.Gold2DMA0108", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA0108, "MM45Bytes.Gold3DMA0108", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA3003, "MM45Bytes.GoldDMA3003", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA3003, "MM45Bytes.Gold2DMA3003", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA3003, "MM45Bytes.Gold3DMA3003", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0125, "MM45Bytes.GoldDMA0125", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0125, "MM45Bytes.Gold2DMA0125", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA0125, "MM45Bytes.Gold3DMA0125", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold4DMA0125, "MM45Bytes.Gold4DMA0125", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0523, "MM45Bytes.GoldDMA0523", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0523, "MM45Bytes.Gold2DMA0523", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA0523, "MM45Bytes.Gold3DMA0523", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold4DMA0523, "MM45Bytes.Gold4DMA0523", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.GoldDMA0411, "MM45Bytes.GoldDMA0411", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold2DMA0411, "MM45Bytes.Gold2DMA0411", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold3DMA0411, "MM45Bytes.Gold3DMA0411", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineAlpha, MM45Bytes.Gold4DMA0411, "MM45Bytes.Gold4DMA0411", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK1531, "MM45Bytes.GoldDMK1531", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK1531, "MM45Bytes.Gold2DMK1531", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK1531, "MM45Bytes.Gold3DMK1531", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK1623, "MM45Bytes.GoldDMK1623", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK1623, "MM45Bytes.Gold2DMK1623", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK1623, "MM45Bytes.Gold3DMK1623", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK0813, "MM45Bytes.GoldDMK0813", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK0813, "MM45Bytes.Gold2DMK0813", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK0813, "MM45Bytes.Gold3DMK0813", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK2712, "MM45Bytes.GoldDMK2712", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK2712, "MM45Bytes.Gold2DMK2712", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK2712, "MM45Bytes.Gold3DMK2712", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK1526, "MM45Bytes.GoldDMK1526", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK1526, "MM45Bytes.Gold2DMK1526", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK1526, "MM45Bytes.Gold3DMK1526", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold4DMK1526, "MM45Bytes.Gold4DMK1526", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK3026, "MM45Bytes.GoldDMK3026", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK3026, "MM45Bytes.Gold2DMK3026", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK3026, "MM45Bytes.Gold3DMK3026", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold4DMK3026, "MM45Bytes.Gold4DMK3026", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK2819, "MM45Bytes.GoldDMK2819", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK2819, "MM45Bytes.Gold2DMK2819", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK2819, "MM45Bytes.Gold3DMK2819", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold4DMK2819, "MM45Bytes.Gold4DMK2819", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK0414, "MM45Bytes.GoldDMK0414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK0414, "MM45Bytes.Gold2DMK0414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK0414, "MM45Bytes.Gold3DMK0414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold4DMK0414, "MM45Bytes.Gold4DMK0414", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.GoldDMK0231, "MM45Bytes.GoldDMK0231", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold2DMK0231, "MM45Bytes.Gold2DMK0231", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold3DMK0231, "MM45Bytes.Gold3DMK0231", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold4DMK0231, "MM45Bytes.Gold4DMK0231", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineKappa, MM45Bytes.Gold5DMK0231, "MM45Bytes.Gold5DMK0231", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineTheta, MM45Bytes.GoldDMT3107, "MM45Bytes.GoldDMT3107", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineTheta, MM45Bytes.Gold2DMT3107, "MM45Bytes.Gold2DMT3107", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineTheta, MM45Bytes.Gold3DMT3107, "MM45Bytes.Gold3DMT3107", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineTheta, MM45Bytes.Gold4DMT3107, "MM45Bytes.Gold4DMT3107", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineTheta, MM45Bytes.Gold5DMT3107, "MM45Bytes.Gold5DMT3107", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.GoldDMO3024, "MM45Bytes.GoldDMO3024", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold2DMO3024, "MM45Bytes.Gold2DMO3024", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold3DMO3024, "MM45Bytes.Gold3DMO3024", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold4DMO3024, "MM45Bytes.Gold4DMO3024", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold5DMO3024, "MM45Bytes.Gold5DMO3024", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.GoldDMO0515, "MM45Bytes.GoldDMO0515", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold2DMO0515, "MM45Bytes.Gold2DMO0515", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold3DMO0515, "MM45Bytes.Gold3DMO0515", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold4DMO0515, "MM45Bytes.Gold4DMO0515", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold5DMO0515, "MM45Bytes.Gold5DMO0515", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.GoldDMO2114, "MM45Bytes.GoldDMO2114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold2DMO2114, "MM45Bytes.Gold2DMO2114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold3DMO2114, "MM45Bytes.Gold3DMO2114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold4DMO2114, "MM45Bytes.Gold4DMO2114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold5DMO2114, "MM45Bytes.Gold5DMO2114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.GoldDMO0506, "MM45Bytes.GoldDMO0506", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold2DMO0506, "MM45Bytes.Gold2DMO0506", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold3DMO0506, "MM45Bytes.Gold3DMO0506", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold4DMO0506, "MM45Bytes.Gold4DMO0506", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.DeepMineOmega, MM45Bytes.Gold5DMO0506, "MM45Bytes.Gold5DMO0506", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.A2Surface, MM45Bytes.DestroyA2DS0702, "MM45Bytes.DestroyA2DS0702", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.A3Surface, MM45Bytes.DestroyA3CS0814, "MM45Bytes.DestroyA3CS0814", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.A4Surface, MM45Bytes.DestroyA4CS1008, "MM45Bytes.DestroyA4CS1008", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.B2Surface, MM45Bytes.DestroyB2DS0002, "MM45Bytes.DestroyB2DS0002", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B3Surface, MM45Bytes.DestroyB3DS1310, "MM45Bytes.DestroyB3DS1310", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.B4Surface, MM45Bytes.DestroyB4CS0207, "MM45Bytes.DestroyB4CS0207", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.B4Surface, MM45Bytes.DestroyB4CS1012, "MM45Bytes.DestroyB4CS1012", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.C1Surface, MM45Bytes.DestroyC1DS0911, "MM45Bytes.DestroyC1DS0911", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.C2Surface, MM45Bytes.DestroyC2CS0108, "MM45Bytes.DestroyC2CS0108", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C2Surface, MM45Bytes.DestroyC2CS0500, "MM45Bytes.DestroyC2CS0500", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C4Surface, MM45Bytes.DestroyC4CS0111, "MM45Bytes.DestroyC4CS0111", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.D1Surface, MM45Bytes.DestroyD1DS0012, "MM45Bytes.DestroyD1DS0012", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX0505, "MM45Bytes.DestroyD3CX0505", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX2505, "MM45Bytes.DestroyD3CX2505", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX2729, "MM45Bytes.DestroyD3CX2729", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX2827, "MM45Bytes.DestroyD3CX2827", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX2830, "MM45Bytes.DestroyD3CX2830", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX2928, "MM45Bytes.DestroyD3CX2928", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D3CloudsOfXeen, MM45Bytes.DestroyD3CX3030, "MM45Bytes.DestroyD3CX3030", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.D3Surface, MM45Bytes.DestroyD3DS0908, "MM45Bytes.DestroyD3DS0908", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D3Surface, MM45Bytes.DestroyD3DS0307, "MM45Bytes.DestroyD3DS0307", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.E1VolcanoCaveLevel1, MM45Bytes.DestroyE1VCL10015, "MM45Bytes.DestroyE1VCL10015", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E1VolcanoCaveLevel1, MM45Bytes.DestroyE1VCL10909, "MM45Bytes.DestroyE1VCL10909", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E2Surface, MM45Bytes.DestroyE2CS0902, "MM45Bytes.DestroyE2CS0902", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E3Surface, MM45Bytes.DestroyE3CS1413, "MM45Bytes.DestroyE3CS1413", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.DestroyE3DDL20420, "MM45Bytes.DestroyE3DDL20420", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.DestroyE3DDL20620, "MM45Bytes.DestroyE3DDL20620", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.DestroyE3DDL20820, "MM45Bytes.DestroyE3DDL20820", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel4, MM45Bytes.DestroyE3DDL40203, "MM45Bytes.DestroyE3DDL40203", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel4, MM45Bytes.DestroyE3DDL40329, "MM45Bytes.DestroyE3DDL40329", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel4, MM45Bytes.DestroyE3DDL42703, "MM45Bytes.DestroyE3DDL42703", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel4, MM45Bytes.DestroyE3DDL42829, "MM45Bytes.DestroyE3DDL42829", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.E4AncientTempleOfYak, MM45Bytes.DestroyE4ATY0206, "MM45Bytes.DestroyE4ATY0206", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E4AncientTempleOfYak, MM45Bytes.DestroyE4ATY0217, "MM45Bytes.DestroyE4ATY0217", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E4AncientTempleOfYak, MM45Bytes.DestroyE4ATY2507, "MM45Bytes.DestroyE4ATY2507", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E4AncientTempleOfYak, MM45Bytes.DestroyE4ATY2725, "MM45Bytes.DestroyE4ATY2725", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.E4Surface, MM45Bytes.DestroyE4CS0914, "MM45Bytes.DestroyE4CS0914", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.F1Surface, MM45Bytes.DestroyF1DS1000, "MM45Bytes.DestroyF1DS1000", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.F2Surface, MM45Bytes.DestroyF2CS1205, "MM45Bytes.DestroyF2CS1205", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F2Surface, MM45Bytes.DestroyF2CS1303, "MM45Bytes.DestroyF2CS1303", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F3Surface, MM45Bytes.DestroyF3CS1214, "MM45Bytes.DestroyF3CS1214", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4WitchClouds, MM45Bytes.DestroyF4WC0419, "MM45Bytes.DestroyF4WC0419", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4WitchClouds, MM45Bytes.DestroyF4WC0427, "MM45Bytes.DestroyF4WC0427", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4WitchClouds, MM45Bytes.DestroyF4WC0807, "MM45Bytes.DestroyF4WC0807", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4WitchClouds, MM45Bytes.DestroyF4WC2228, "MM45Bytes.DestroyF4WC2228", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.F4WitchClouds, MM45Bytes.DestroyF4WC2720, "MM45Bytes.DestroyF4WC2720", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.A2SkyroadA2, MM45Bytes.LampA2SA21214, "MM45Bytes.LampA2SA21214", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B1SkyroadB1, MM45Bytes.LampB1SB10507, "MM45Bytes.LampB1SB10507", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2SkyroadB2, MM45Bytes.LampB2SB20514, "MM45Bytes.LampB2SB20514", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C3SkyroadC3, MM45Bytes.LampC3SC30700, "MM45Bytes.LampC3SC30700", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E1SkyroadE1, MM45Bytes.LampE1SE11201, "MM45Bytes.LampE1SE11201", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E2SkyroadE2, MM45Bytes.LampE2SE20812, "MM45Bytes.LampE2SE20812", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E2SkyroadE2, MM45Bytes.LampE2SE20308, "MM45Bytes.LampE2SE20308", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E2SkyroadE2, MM45Bytes.LampE2SE21208, "MM45Bytes.LampE2SE21208", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E2SkyroadE2, MM45Bytes.LampE2SE20803, "MM45Bytes.LampE2SE20803", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F1SkyroadF1, MM45Bytes.LampF1SF10303, "MM45Bytes.LampF1SF10303", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2SkyroadF2, MM45Bytes.LampF2SF20112, "MM45Bytes.LampF2SF20112", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2Surface, MM45Bytes.LampB2DS1309, "MM45Bytes.LampB2DS1309", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2Surface, MM45Bytes.LampB2DS1202, "MM45Bytes.LampB2DS1202", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C2Surface, MM45Bytes.LampC2DS0315, "MM45Bytes.LampC2DS0315", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C2Surface, MM45Bytes.LampC2DS0611, "MM45Bytes.LampC2DS0611", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C2Surface, MM45Bytes.LampC2DS0206, "MM45Bytes.LampC2DS0206", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D2Surface, MM45Bytes.LampD2DS0015, "MM45Bytes.LampD2DS0015", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D3Surface, MM45Bytes.LampD3DS0712, "MM45Bytes.LampD3DS0712", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.E1DragonCave, MM45Bytes.L7ItemE1DC3100, "MM45Bytes.L7ItemE1DC3100", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL10518, "MM45Bytes.L7ItemA2SSL10518", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL10516, "MM45Bytes.L7ItemA2SSL10516", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL10514, "MM45Bytes.L7ItemA2SSL10514", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL10201, "MM45Bytes.L7ItemA2SSL10201", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL10401, "MM45Bytes.L7ItemA2SSL10401", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL11001, "MM45Bytes.L7ItemA2SSL11001", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel1, MM45Bytes.L7ItemA2SSL11201, "MM45Bytes.L7ItemA2SSL11201", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A2SouthernSphinxLevel3, MM45Bytes.L7ItemA2SSL30615, "MM45Bytes.L7ItemA2SSL30615", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL22613, "MM45Bytes.L7ItemE3DDL22613", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL23013, "MM45Bytes.L7ItemE3DDL23013", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL20704, "MM45Bytes.L7ItemE3DDL20704", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL20904, "MM45Bytes.L7ItemE3DDL20904", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL21104, "MM45Bytes.L7ItemE3DDL21104", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL21304, "MM45Bytes.L7ItemE3DDL21304", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E3DungeonOfDeathLevel2, MM45Bytes.L7ItemE3DDL21504, "MM45Bytes.L7ItemE3DDL21504", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel4, MM45Bytes.L7ItemD1DTL40509, "MM45Bytes.L7ItemD1DTL40509", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel4, MM45Bytes.L7ItemD1DTL40909, "MM45Bytes.L7ItemD1DTL40909", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel4, MM45Bytes.L7ItemD1DTL40308, "MM45Bytes.L7ItemD1DTL40308", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel4, MM45Bytes.L7ItemD1DTL41108, "MM45Bytes.L7ItemD1DTL41108", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D1DragonTowerLevel4, MM45Bytes.L7ItemD1DTL40706, "MM45Bytes.L7ItemD1DTL40706", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.L7ItemC4TBL51611, "MM45Bytes.L7ItemC4TBL51611", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D2TheGreatPyramidLevel1, MM45Bytes.L7ItemD2TGPL12315, "MM45Bytes.L7ItemD2TGPL12315", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.E4TrollHoles, MM45Bytes.L7ItemE4TH0724, "MM45Bytes.L7ItemE4TH0724", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N0105, "MM45Bytes.L7ItemB2N0105", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N0205, "MM45Bytes.L7ItemB2N0205", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1204, "MM45Bytes.L7ItemB2N1204", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1404, "MM45Bytes.L7ItemB2N1404", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1203, "MM45Bytes.L7ItemB2N1203", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1403, "MM45Bytes.L7ItemB2N1403", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1002, "MM45Bytes.L7ItemB2N1002", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1402, "MM45Bytes.L7ItemB2N1402", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2Necropolis, MM45Bytes.L7ItemB2N1401, "MM45Bytes.L7ItemB2N1401", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2NecropolisSewer, MM45Bytes.L7ItemB2NS0610, "MM45Bytes.L7ItemB2NS0610", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2NecropolisSewer, MM45Bytes.L7ItemB2NS0106, "MM45Bytes.L7ItemB2NS0106", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2NecropolisSewer, MM45Bytes.L7ItemB2NS0102, "MM45Bytes.L7ItemB2NS0102", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.B2NecropolisSewer, MM45Bytes.L7ItemB2NS1001, "MM45Bytes.L7ItemB2NS1001", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.D4Nightshadow, MM45Bytes.OpenCoffinD4N0114, "MM45Bytes.OpenCoffinD4N0114", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.D2TheGreatPyramidLevel1, MM45Bytes.LeverD2TGPL12520, "MM45Bytes.LeverD2TGPL12520", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.D2TheGreatPyramidLevel1, MM45Bytes.TreasureD2TGPL12315, "MM45Bytes.TreasureD2TGPL12315", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N0111, "MM45Bytes.TreeD4N0111", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N0210, "MM45Bytes.TreeD4N0210", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N1008, "MM45Bytes.TreeD4N1008", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N0107, "MM45Bytes.TreeD4N0107", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N0902, "MM45Bytes.TreeD4N0902", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.D4Nightshadow, MM45Bytes.TreeD4N1102, "MM45Bytes.TreeD4N1102", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C1328, "MM45Bytes.TreeA4C1328", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C1728, "MM45Bytes.TreeA4C1728", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C1326, "MM45Bytes.TreeA4C1326", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C1726, "MM45Bytes.TreeA4C1726", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C0625, "MM45Bytes.TreeA4C0625", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C1319, "MM45Bytes.TreeA4C1319", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C1617, "MM45Bytes.TreeA4C1617", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C0814, "MM45Bytes.TreeA4C0814", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C0914, "MM45Bytes.TreeA4C0914", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C1014, "MM45Bytes.TreeA4C1014", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C1408, "MM45Bytes.TreeA4C1408", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.A4Castleview, MM45Bytes.TreeA4C1608, "MM45Bytes.TreeA4C1608", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R3028, "MM45Bytes.TreeC3R3028", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R0126, "MM45Bytes.TreeC3R0126", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R0326, "MM45Bytes.TreeC3R0326", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R3023, "MM45Bytes.TreeC3R3023", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R2720, "MM45Bytes.TreeC3R2720", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R0908, "MM45Bytes.TreeC3R0908", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R1206, "MM45Bytes.TreeC3R1206", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R0905, "MM45Bytes.TreeC3R0905", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R1205, "MM45Bytes.TreeC3R1205", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R2105, "MM45Bytes.TreeC3R2105", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R1303, "MM45Bytes.TreeC3R1303", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R1403, "MM45Bytes.TreeC3R1403", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM4Map.C3Rivercity, MM45Bytes.TreeC3R0902, "MM45Bytes.TreeC3R0902", bytesOldMM4, bytesNewMM4);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL50000, "MM45Bytes.Feed1C4TBL50000", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL50000, "MM45Bytes.Feed2C4TBL50000", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL50000, "MM45Bytes.Feed3C4TBL50000", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL50000, "MM45Bytes.Feed4C4TBL50000", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL50700, "MM45Bytes.Feed1C4TBL50700", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL50700, "MM45Bytes.Feed2C4TBL50700", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL50700, "MM45Bytes.Feed3C4TBL50700", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL50700, "MM45Bytes.Feed4C4TBL50700", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL52200, "MM45Bytes.Feed1C4TBL52200", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL52200, "MM45Bytes.Feed2C4TBL52200", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL52200, "MM45Bytes.Feed3C4TBL52200", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL52200, "MM45Bytes.Feed4C4TBL52200", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL53100, "MM45Bytes.Feed1C4TBL53100", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL53100, "MM45Bytes.Feed2C4TBL53100", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL53100, "MM45Bytes.Feed3C4TBL53100", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL53100, "MM45Bytes.Feed4C4TBL53100", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL50028, "MM45Bytes.Feed1C4TBL50028", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL50028, "MM45Bytes.Feed2C4TBL50028", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL50028, "MM45Bytes.Feed3C4TBL50028", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL50028, "MM45Bytes.Feed4C4TBL50028", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL50121, "MM45Bytes.Feed1C4TBL50121", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL50121, "MM45Bytes.Feed2C4TBL50121", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL50121, "MM45Bytes.Feed3C4TBL50121", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL50121, "MM45Bytes.Feed4C4TBL50121", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL53130, "MM45Bytes.Feed1C4TBL53130", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL53130, "MM45Bytes.Feed2C4TBL53130", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL53130, "MM45Bytes.Feed3C4TBL53130", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL53130, "MM45Bytes.Feed4C4TBL53130", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed1C4TBL53121, "MM45Bytes.Feed1C4TBL53121", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed2C4TBL53121, "MM45Bytes.Feed2C4TBL53121", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed3C4TBL53121, "MM45Bytes.Feed3C4TBL53121", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.Feed4C4TBL53121, "MM45Bytes.Feed4C4TBL53121", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.TreasureC4TBL51611, "MM45Bytes.TreasureC4TBL51611", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.TreasureC4TBL50124, "MM45Bytes.TreasureC4TBL50124", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.C4TempleOfBarkLevel5, MM45Bytes.TreasureC4TBL53025, "MM45Bytes.TreasureC4TBL53025", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM5Map.F2LostSoulsDungeonLevel4, MM45Bytes.PayGoldF2LSDL41402, "MM45Bytes.PayGoldF2LSDL41402", bytesOldMM5, bytesNewMM5);
            ConvertScriptOffset(MM4Map.F3Surface, MM45Bytes.HelpedDerekF3CS0405, "MM45Bytes.HelpedDerekF3CS0405", bytesOldMM4, bytesNewMM4);
        }

        private void Echo(string[] tokens, ref int iToken)
        {
            if (iToken >= tokens.Length)
            {
                Output("Echo is {0}", m_bEcho ? "on" : "off");
                iToken = tokens.Length;
                return;
            }

            string strArg = tokens[iToken++];
            m_bEcho = strArg.ToLower() == "on";
            Output("Echo is {0}", m_bEcho ? "on" : "off");
        }

        private void MessageWindow(string[] tokens, ref int iToken)
        {
            if (iToken >= tokens.Length)
            {
                Output("Message box text required");
                iToken = tokens.Length;
                return;
            }

            MessageBox.Show(tokens[iToken++], "WhereAreWe Debug Console");
        }

        private void DoMenu(string[] tokens, ref int iToken)
        {
            if (iToken > tokens.Length - 2)
            {
                Output("2 arguments required (menu title, item number)");
                iToken = tokens.Length;
                return;
            }

            string strMenu = tokens[iToken++];
            int iNumber = 0;
            if (!Int32.TryParse(tokens[iToken++], out iNumber))
            {
                Output("Invalid item number");
                return;
            }

            if (m_main.RunMenuCommand(strMenu, iNumber))
                Output("Executed menu item {0} from \"{1}\"", iNumber, strMenu);
            else
                Output("Error executing menu item {0} from \"{1}\"", iNumber, strMenu);
        }

        private bool CheckHackerActive()
        {
            if (CheckMainNull())
                return false;

            if (m_main.Hacker != null && m_main.Hacker.IsValid)
                return true;

            Output("Error: Hacker is not active.");
            return false;
        }

        private bool CheckMainNull()
        {
            if (m_main != null)
                return false;

            Output("Error: m_main not set");
            return true;
        }

        private void Dirty(string[] tokens, ref int iToken)
        {
            if (CheckMainNull())
                return;

            m_main.SetDirty();
            Output("Main dirty flag set.");
        }

        private void GameDirty(string[] tokens, ref int iToken)
        {
            // Marks a particular square on the game map as "dirty"
            // Example:  dirty 10 12

            int iX = GetInt(tokens, ref iToken);
            int iY = GetInt(tokens, ref iToken);

            if (CheckMainNull())
                return;

            m_main.SetDirty(new Point(iX, iY));
            Output("Game square ({0},{1}) marked as dirty.", iX, iY);
        }

        private void DumpTokens(string[] tokens)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < tokens.Length; i++)
            {
                sb.AppendFormat("{0}: {1}", i, tokens[i]);
                sb.AppendLine();
            }

            Output(sb.ToString());
        }

        private void DebugConsole_Load(object sender, EventArgs e)
        {
            InitCommands();
            tbCommand.Focus();
            tbCommand.Select();
        }

        private void tbOutput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
                e.Handled = true;
            }
        }

        private void DebugConsole_CommonKeySelectAll(object sender, EventArgs e)
        {
            Global.SelectAll(ActiveControl);
        }
    }

    public delegate void ConsoleCommand(string[] tokens, ref int iToken);

    public class ConsoleCommandInfo
    {
        public string Text;
        public string Args;
        public ConsoleCommand Function;
        public string Help;

        public ConsoleCommandInfo(string text, string args, ConsoleCommand command, string help)
        {
            Text = text;
            Args = args;
            Function = command;
            Help = help;
        }
    }
}
