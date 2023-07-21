using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;

namespace WhereAreWe
{
    public enum MM345ScriptCommand
    {
        DoNothing = 0,
        Display = 1,
        SignWall = 2,
        DisplayWall = 3,
        OutdoorSign = 4,
        NPC = 5,
        PlayAudioEffect = 6,
        TeleportEndScript = 7,
        JumpIfGreaterOrEqual = 8,
        Condition = 9,
        JumpIfLessOrEqual = 10,
        SelectGraphic = 11,
        Trade = 12,
        NoAction = 13,
        ZeroAll = 14,
        SetAffected = 15,
        Summon = 16,
        Business = 17,
        ExitScript = 18,
        MapCommand = 19,
        Receive = 20,
        Question = 21,
        Damage = 22,
        Random = 23,
        SetCommand = 24,
        SubScript = 25,
        Return = 26,
        SetAttribute = 27,
        SetAttributeIfEqual = 28,
        AddIfEqualAndLessThanOrEqual = 29,
        EndingSequence = 30,
        Teleport = 31,
        PrintF = 32,
        AddIfLessOrEqual = 33,
        MoveGraphic = 34,
        SetMapAttribute = 35,
        AlterHED = 36,
        DisplaySeat = 39,
        PlayAudio = 40,
        DisplayBottom = 41,
        JumpIfMonster = 42,
        RandomChar = 43,
        ReceiveItem = 44,
        SetItemType = 45,
        ZeroAll2 = 46,
        CopyProtection = 47,
        NumericChoice = 48,
        DisplayPair = 49,
        DisplayLong = 50,
        SwapGraphics = 51,
        Fall = 52,
        DisplayMain = 53,
        MultiAnswer = 55,
        JumpRandom = 56,
        EndingSequence2 = 57,
        EndingSequence3 = 58,
        SetWorldSide = 59,
        Invalid = -1
    }

    [Flags]
    public enum PermStatReward
    {
        None = 0,
        Might = 0x0001,
        Intellect = 0x0002,
        Personality = 0x0004,
        Endurance = 0x0008,
        Speed = 0x0010,
        Accuracy = 0x0020,
        Luck = 0x0040,
        FireRes = 0x0080,
        ColdRes = 0x0100,
        ElecRes = 0x0200,
        PoisRes = 0x0400,
        EnergyRes = 0x0800,
        MagicRes = 0x1000,
        Level = 0x2000
    }

    public abstract class MMString : ScriptString
    {
        public MMString()
        {
            Init();
        }
    }

    public abstract class MMScriptLine : ScriptLine
    {
        public MMScriptLine()
        {
            Number = -1;
            Location = Global.NullPoint;
            Facing = DirectionFlags.None;
            Bytes = null;
        }

        public static string MapString(int index, List<ScriptString> strings, bool bAbbrev = false)
        {
            if (strings == null || index < 0 || index >= strings.Count)
                return String.Format("MapString[{0}]", index);

            if (bAbbrev)
                return strings[index].Abbreviated;

            return strings[index].Basic;
        }

        public override string ToString() { return Description(new MMScriptInfo(null, new List<ScriptString>(0), null, new MapTitlePair(-1, "")), String.Empty); }
    }

    public abstract class MMScript : GameScript
    {
        public MMScript()
        {
            Init();
        }

        protected void Init()
        {
            Index = -1;
            Location = Global.NullPoint;
            Facing = DirectionFlags.None;
            AutoExecute = false;
        }

        public MMScript(Point pt)
        {
            Init();
            Location = pt;
        }

        private void CollapseRedundantConditionals(List<ScriptSummary> list)
        {
            for (int i = 0; i < list.Count - 3; i++)
            {
                if (list[i].Command == ScriptCommand.Conditional &&
                    list[i + 1].Command == ScriptCommand.Text &&
                    list[i + 2].Command == ScriptCommand.WhoWill &&
                    list[i + 3].Command == ScriptCommand.Text &&
                    list[i + 1].Description == list[i + 3].Description)
                {
                    list.RemoveRange(i, 2);
                }
                if (list[i].Command == ScriptCommand.Text &&
                    list[i + 1].Command == ScriptCommand.ExitScript &&
                    list[i + 2].Command == ScriptCommand.WhoWill &&
                    list[i + 3].Command == ScriptCommand.Text &&
                    list[i].Description == list[i + 3].Description)
                {
                    list.RemoveRange(i, 2);
                }
            }

            for (int i = 0; i < list.Count - 2; i++)
            {
                if (list[i].Command == ScriptCommand.Text &&
                    list[i + 1].Command == ScriptCommand.WhoWill &&
                    list[i + 2].Command == ScriptCommand.Text &&
                    list[i].Description == list[i + 2].Description)
                {
                    list.RemoveRange(i, 1);
                }
            }

            /* This is is causing problems
            for (int i = 0; i < list.Count - 2; i++)
            {
                if (list[i].Command == ScriptCommand.Conditional &&
                    list[i + 1].Command == ScriptCommand.Jump)
                {
                    list.RemoveAt(i+1);
                }
            }
             */
        }

        private enum NPCNoteState { None, NPCOnly, MoreThanNPC };

        public string PermStatSymbol(PermStatReward reward)
        {
            // Only returns a symbol for a reward containing exactly one stat
            switch(reward)
            {
                case PermStatReward.Might: return "Mi";
                case PermStatReward.Intellect: return "In";
                case PermStatReward.Personality: return "Pe";
                case PermStatReward.Endurance: return "En";
                case PermStatReward.Speed: return "Sp";
                case PermStatReward.Accuracy: return "Ac";
                case PermStatReward.Luck: return "Lu";
                case PermStatReward.FireRes: return "Fi";
                case PermStatReward.ColdRes: return "Co";
                case PermStatReward.ElecRes: return "El";
                case PermStatReward.PoisRes: return "Po";
                case PermStatReward.EnergyRes: return "Eg";
                case PermStatReward.MagicRes: return "Ma";
                case PermStatReward.Level: return "Lv";
                default: return "L";
            }
        }

        public override NoteInfo Summary(ScriptInfo info, bool bSkipSubscripts, bool bForNote = false)
        {
            NPCNoteState npcState = NPCNoteState.None;
            MapIcon icon = null;

            string strSymbol = "?";

            List<ScriptSummary> listSummary = new List<ScriptSummary>();
            Summary(0, listSummary, info as MMScriptInfo, 0, bSkipSubscripts, bForNote);

            CollapseRedundantConditionals(listSummary);

            CollapsibleInfo ci = new CollapsibleInfo(Location, RewardContent.Empty, 0, new StringBuilder(), 
                String.Empty, bForNote, bForNote ? " ({0} times)" : " x{0}", false, listSummary.Count, 1);
            List<CollapsibleScriptItem> listCollapsible = new List<CollapsibleScriptItem>();

            bool bAnyReward = false;
            bool bAnyText = false;
            bool bAnySummon = false;
            bool bWhoWill = false;
            bool bAnySign = false;
            bool bAnyDamage = false;
            bool bAnyTeleport = false;
            bool bExternalTeleport = false;
            bool bPortal = false;
            bool bAnyAlter = false;
            PermStatReward statPerm = PermStatReward.None;
            List<Point> listTargets = new List<Point>();
            string strVerb = String.Empty;
            string strNPC = String.Empty;
            string strBusiness = String.Empty;
            string strSuggestion = String.Empty;
            string strFirstText = String.Empty;
            string strItemType = String.Empty;
            string strExitDirection = "Ex";
            string strLower = String.Empty;
            SummaryListType listType = SummaryListType.None;
            int iMaxTextLength = 0;

            string strWhoWillAbbr = String.Empty;

            for (ci.Index = 0; ci.Index <= listSummary.Count; ci.Index++)
            {
                if (ci.Index < listSummary.Count && listSummary[ci.Index].Command != ScriptCommand.NPC)
                    npcState = NPCNoteState.MoreThanNPC;

                if (ci.Index < listSummary.Count && strSymbol == "?")
                {
                    strLower = listSummary[ci.Index].Description.ToLower();
                    ScriptCommand cmd = listSummary[ci.Index].Command;
                    switch (cmd)
                    {
                        case ScriptCommand.Text:
                            bAnyText = true;
                            iMaxTextLength = Math.Max(iMaxTextLength, listSummary[ci.Index].Description.Length);
                            if (String.IsNullOrWhiteSpace(strFirstText))
                                strFirstText = listSummary[ci.Index].Description;
                            if (strLower.Contains("descend") || strLower.Contains(" down"))
                                strExitDirection = "D";
                            else if (strLower.Contains(" up"))
                                strExitDirection = "U";
                            break;
                        case ScriptCommand.AlterMap:
                            bAnyAlter = true;
                            break;
                        case ScriptCommand.Rewards:
                            bool bHarmful = MM345Reward.IsHarmful(listSummary[ci.Index].Rewards);
                            statPerm |= MM345Reward.PermanentStats(listSummary[ci.Index].Rewards);
                            bAnyReward = bAnyReward || !bHarmful;
                            if (bHarmful)
                                bAnyDamage = true;
                            break;
                        case ScriptCommand.Damage:
                            bAnyDamage = true;
                            break;
                        case ScriptCommand.Summon:
                            bAnySummon = true;
                            break;
                        case ScriptCommand.WhoWill:
                            bWhoWill = true;
                            strVerb = listSummary[ci.Index].Verb;
                            strWhoWillAbbr = listSummary[ci.Index].Description.Substring(0, 2).TrimEnd();
                            break;
                        case ScriptCommand.Encounter:
                            bAnySummon = true;
                            break;
                        case ScriptCommand.Sign:
                            bAnySign = true;
                            break;
                        case ScriptCommand.Teleport:
                            bAnyTeleport = true;
                            if (listSummary[ci.Index].Description.Contains("{map"))
                            {
                                int iThisLevel = Global.LevelFromString(info.Map.Title);
                                int iToLevel = Global.LevelFromString(strLower);
                                if (iThisLevel > iToLevel)
                                    strExitDirection = "D";
                                else if (iThisLevel < iToLevel)
                                    strExitDirection = "U";
                                bExternalTeleport = true;
                            }
                            listTargets.Add(listSummary[ci.Index].Target);
                            break;
                        case ScriptCommand.Portal:
                            bPortal = true;
                            break;
                        case ScriptCommand.SetItemType:
                            strItemType = MM345Command.ItemTypeString((byte)listSummary[ci.Index].Value);
                            if (bForNote)
                                continue;
                            break;
                        case ScriptCommand.NPC:
                            if (npcState == NPCNoteState.None)
                                npcState = NPCNoteState.NPCOnly;
                            else
                                npcState = NPCNoteState.MoreThanNPC;
                            strNPC = listSummary[ci.Index].Symbol;
                            break;
                        default:
                            if (!String.IsNullOrWhiteSpace(listSummary[ci.Index].Symbol))
                                strSuggestion = listSummary[ci.Index].Symbol;
                            else
                                strBusiness = ScriptSummary.SymbolFromBusinessCommand(listSummary[ci.Index].Command);
                            break;
                    }
                }

                string strLine = String.Empty;

                int iCollapsibleCount = listCollapsible.Count;

                if (ci.Index < listSummary.Count)
                {
                    if (listSummary[ci.Index].Rewards != null)
                        listCollapsible.AddRange(listSummary[ci.Index].Rewards);
                    else if (listSummary[ci.Index].Summons != null)
                        listCollapsible.AddRange(listSummary[ci.Index].Summons);
                    else if (listSummary[ci.Index].Command == ScriptCommand.Conditional)
                        listCollapsible.Add(new ConditionalInfo(listSummary[ci.Index]));
                    else if (listSummary[ci.Index].Command == ScriptCommand.SetMapAttribute)
                        listCollapsible.Add(new SetMapAttributeInfo(listSummary[ci.Index].Target, (byte)listSummary[ci.Index].Value));
                    else if (listSummary[ci.Index].Command == ScriptCommand.AlterMap)
                        listCollapsible.Add(new SetMapAppearanceInfo(listSummary[ci.Index]));

                    if (iCollapsibleCount != listCollapsible.Count)
                        continue;
                }

                if (listCollapsible.Count > 0)
                {
                    CollapsibleScriptItem.Collapse(listCollapsible, ci);
                    listCollapsible.Clear();
                }

                if (String.IsNullOrWhiteSpace(strLine) && listType != SummaryListType.None)
                    continue;

                if (ci.Index < listSummary.Count && String.IsNullOrWhiteSpace(strLine))
                    strLine = listSummary[ci.Index].Description;

                if (!ci.AddSingleSummaryItem(strLine))
                    break;
            }

            if (npcState == NPCNoteState.NPCOnly)
                strSymbol = "f";    // Flavor text
            else if (!String.IsNullOrWhiteSpace(strBusiness) && strBusiness != "?")
                strSymbol = strBusiness;
            else if (bPortal)
                strSymbol = "Po";   // Mirror/Portal
            else if (bExternalTeleport)
            {
                switch (strExitDirection)
                {
                    case "U":
                        icon = new MapIcon(IconName.StairsUp, Direction.Up, Location);
                        break;
                    case "D":
                        icon = new MapIcon(IconName.StairsDown, Direction.Up, Location);
                        break;
                    default:
                        icon = new MapIcon(IconName.Exit, Direction.Up, Location);
                        break;
                }
                strSymbol = strExitDirection;   // Exit
            }
            else if (bAnySummon)
                strSymbol = "E";    // Encounter
            else if (bAnyDamage && !bAnyReward)
                strSymbol = "t";    // Trap
            else if (bAnySign && !bAnyReward)
                strSymbol = "s";    // Sign
            else if (bWhoWill && !bAnyReward && !bAnyAlter)
            {
                if (iMaxTextLength < 50)    // "There is nothing here" etc.
                    strSymbol = "n";    // Nothing of interest
                else
                    strSymbol = strWhoWillAbbr;
            }
            else if (ci.Content.OnlyGoldGemsFoodItems)
                strSymbol = "L";    // Loot
            else if (Global.NumBitsSet((UInt32)statPerm) == 1)
                strSymbol = PermStatSymbol(statPerm);
            else if (bWhoWill && bAnyReward)
                strSymbol = strWhoWillAbbr;
            else if (!String.IsNullOrWhiteSpace(strNPC))
                strSymbol = strNPC;
            else if (!String.IsNullOrWhiteSpace(strSuggestion))
                strSymbol = strSuggestion;
            else if (bAnyTeleport)
            {
                strSymbol = "T";    // Teleport
                if (!bExternalTeleport && listTargets.Count == 1)
                {
                    if (listTargets[0].X == Location.X)
                    {
                        if (listTargets[0].Y == Location.Y + 1)
                            strSymbol = "↑";
                        else if (listTargets[0].Y == Location.Y - 1)
                            strSymbol = "↓";
                    }
                    else if (listTargets[0].Y == Location.Y)
                    {
                        if (listTargets[0].X == Location.X + 1)
                            strSymbol = "→";
                        else if (listTargets[0].X == Location.X - 1)
                            strSymbol = "←";
                    }
                }
                else if (!bExternalTeleport && listTargets.Count == 2)
                {
                    if (Global.AreAdjacent(Location, listTargets[0], listTargets[1]))
                    {
                        if (listTargets[0].X == Location.X)
                            strSymbol = "┆";
                        else
                            strSymbol = "┄";
                    }
                    else if (Global.IsNorthwest(Location, listTargets[0], listTargets[1]))
                        strSymbol = "╭";
                    else if (Global.IsNortheast(Location, listTargets[0], listTargets[1]))
                        strSymbol = "╮";
                    else if (Global.IsSouthwest(Location, listTargets[0], listTargets[1]))
                        strSymbol = "╰";
                    else if (Global.IsSoutheast(Location, listTargets[0], listTargets[1]))
                        strSymbol = "╯";
                }
            }
            else if (bAnyAlter)
                strSymbol = "W";    // Wall alterations
            else if (bAnyText && !bAnyReward)
                strSymbol = "s";    // Nothing but text?  Essentially a sign.
            else if (!String.IsNullOrWhiteSpace(strFirstText))
                strSymbol = Global.CreateSymbol(strFirstText);

            string strTest = ci.Builder.ToString().ToLower();

            if (strTest.Contains("if you have already <verb>:") && !strTest.Contains("otherwise:"))
                ci.Builder.Replace("If you have already <verb>: ", "");
            ci.Builder.Replace("If you have already <verb>: Otherwise: ", "");
            ci.Builder.Replace("If you have already <verb>: [If ", "[If ");
            ci.Builder.Replace("If you have already <verb>: If ", "If ");
            ci.Builder.Replace("Otherwise: [If ", "[If ");
            ci.Builder.Replace("Otherwise: If ", "If ");
            ci.Builder.Replace(": and If ", " and ");
            if (!String.IsNullOrWhiteSpace(strItemType))
            {
                ci.Builder.Replace("Item)", strItemType + ")");
                ci.Builder.Replace("Items)", "Items, " + strItemType + ")");
            }

            if (!String.IsNullOrWhiteSpace(strVerb))
                ci.Builder.Replace("<verb>", MM45Command.PastPerfect(strVerb));

            ci.Builder.Replace("If you have already <verb>:", "If ?:");

            ci.Builder.Replace("N: Otherwise: ", "Y: ");

            if (ci.Builder.Length > 1 && ci.Builder[0] == ';' && !bForNote)
                ci.Builder.Remove(0, 2);

            return new NoteInfo(ci.Builder.ToString(), strSymbol, Color.Black, icon);
        }
    }

    public class NoteInfo
    {
        public string Text;
        public string Symbol;
        public Color Color;
        public MapIcon Icon;

        public static NoteInfo Empty { get { return new NoteInfo(String.Empty, String.Empty, Color.Black); } }
        public bool IsEmpty { get { return String.IsNullOrWhiteSpace(Text); } }

        public NoteInfo(string text, string symbol, Color color, MapIcon icon = null)
        {
            Text = text;
            Symbol = symbol;
            Color = color;
            Icon = icon;
        }
    }

    public class SetMapAppearanceInfo : CollapsibleScriptItem
    {
        internal class CommandInfo
        {
            public MM345MapCommand Command;
            public DirectionFlags Direction;
            public string Description;
            public bool Used;

            public CommandInfo(long cmd, DirectionFlags dir, string desc)
            {
                Command = (MM345MapCommand) cmd;
                Direction = dir;
                Description = desc;
                Used = false;
            }

            public static void SetUsed(params List<CommandInfo>[] lists)
            {
                foreach (List<CommandInfo> list in lists)
                    foreach (CommandInfo info in list)
                        info.Used = true;
            }
        }

        public override SummaryListType ItemType { get { return SummaryListType.SetMapAppearance; } }

        public ScriptSummary Summary;
        public MM345MapCommand Command;

        public SetMapAppearanceInfo(ScriptSummary summary)
        {
            Summary = summary;
            Command = (MM345MapCommand)summary.Value;
        }

        internal class CommandArgs
        {
            public List<CommandInfo> Used;
            public Dictionary<Point, List<CommandInfo>> Points;
            public Point Center;
            public MM345MapCommand Command;

            public CommandArgs(List<CommandInfo> listUsed, Dictionary<Point, List<CommandInfo>> dict, Point pt, MM345MapCommand mapCommand)
            {
                Used = listUsed;
                Points = dict;
                Center = pt;
                Command = mapCommand;
            }
        }

        internal class WallInfo
        {
            public DirectionFlags Dir;
            public Point Adjacent;

            public WallInfo(DirectionFlags dir, Point square)
            {
                Dir = dir;
                Adjacent = square;
            }

            public static bool GetCommand(List<CommandInfo> used, List<CommandInfo> list, MM345MapCommand cmd, DirectionFlags dir)
            {
                foreach (CommandInfo cmdInfo in list)
                {
                    switch (cmd)
                    {
                        case MM345MapCommand.RemoveWall:
                            if ((cmdInfo.Command == MM345MapCommand.RemoveWall0 ||
                                cmdInfo.Command == MM345MapCommand.RemoveWall7) &&
                                cmdInfo.Direction == dir &&
                                !cmdInfo.Used)
                            {
                                used.Add(cmdInfo);
                                return true;
                            }
                            break;
                        case MM345MapCommand.CreateWall:
                            if (cmdInfo.Command == MM345MapCommand.CreateWall &&
                                cmdInfo.Direction == dir &&
                                !cmdInfo.Used)
                            {
                                used.Add(cmdInfo);
                                return true;
                            }
                            break;
                        default:
                            break;
                    }
                }
                return false;
            }

            public static bool GetCommands(CommandArgs args, params WallInfo[] walls)
            {
                args.Used.Clear();

                foreach(WallInfo info in walls)
                    if (!args.Points.ContainsKey(info.Adjacent))
                        return false;

                foreach(WallInfo info in walls)
                {
                    GetCommand(args.Used, args.Points[args.Center], args.Command, info.Dir);
                    GetCommand(args.Used, args.Points[info.Adjacent], args.Command, Global.Opposite(info.Dir));
                }

                if (args.Used.Count != walls.Length * 2)
                    return false;

                CommandInfo.SetUsed(args.Used);
                return true;
            }
        }

        public override string Condense(List<CollapsibleScriptItem> list, CollapsibleInfo ci)
        {
            if (!ci.ForNote)
            {
                if (list.Count == 1)
                    return Summary.Description;
                return String.Format("{0} x{1}", Summary.Description, list.Count);
            }

            if (list.Count == 1)
                return Summary.Description;

            CommandArgs args = new CommandArgs(new List<CommandInfo>(8), new Dictionary<Point, List<CommandInfo>>(), Global.NullPoint, MM345MapCommand.RemoveWall);

            foreach (SetMapAppearanceInfo info in list)
            {
                if (!args.Points.ContainsKey(info.Summary.Target))
                    args.Points.Add(info.Summary.Target, new List<CommandInfo>(1));
                args.Points[info.Summary.Target].Add(new CommandInfo(info.Summary.Value, info.Summary.Direction, info.Summary.Description));
            }

            StringBuilder sb = new StringBuilder();

            foreach (Point pt in args.Points.Keys)
            {
                args.Center = pt;

                WallInfo infoNorth = new WallInfo(DirectionFlags.North, new Point(pt.X, pt.Y + 1));
                WallInfo infoSouth = new WallInfo(DirectionFlags.South, new Point(pt.X, pt.Y - 1));
                WallInfo infoEast = new WallInfo(DirectionFlags.East, new Point(pt.X + 1, pt.Y));
                WallInfo infoWest = new WallInfo(DirectionFlags.West, new Point(pt.X - 1, pt.Y));

                // Check for doing something to all walls around an square

                foreach (MM345MapCommand mapCommand in new MM345MapCommand[] { MM345MapCommand.RemoveWall, MM345MapCommand.CreateWall })
                {
                    args.Command = mapCommand;
                    string strVerb = (mapCommand == MM345MapCommand.RemoveWall ? "Remove the" : "Add the");
                    args.Used.Clear();

                    if (WallInfo.GetCommands(args, infoNorth, infoSouth, infoEast, infoWest))
                        sb.AppendFormat("{0} walls around ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoNorth, infoSouth, infoEast))
                        sb.AppendFormat("{0} North, East and South walls at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoNorth, infoSouth, infoWest))
                        sb.AppendFormat("{0} North, South and West walls at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoNorth, infoEast, infoWest))
                        sb.AppendFormat("{0} North, East and West walls at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoSouth, infoEast, infoWest))
                        sb.AppendFormat("{0} East, West and South walls at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoNorth, infoSouth))
                        sb.AppendFormat("{0} North and South walls at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoEast, infoWest))
                        sb.AppendFormat("{0} East and West walls at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoNorth, infoEast))
                        sb.AppendFormat("{0} North and East walls at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoNorth, infoWest))
                        sb.AppendFormat("{0} North and West walls at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoSouth, infoEast))
                        sb.AppendFormat("{0} South and East walls at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoSouth, infoWest))
                        sb.AppendFormat("{0} South and West walls at ({1},{2}), ", strVerb, pt.X, pt.Y);
                }
            }

            foreach (Point pt in args.Points.Keys)
            {
                args.Center = pt;

                WallInfo infoNorth = new WallInfo(DirectionFlags.North, new Point(pt.X, pt.Y + 1));
                WallInfo infoSouth = new WallInfo(DirectionFlags.South, new Point(pt.X, pt.Y - 1));
                WallInfo infoEast = new WallInfo(DirectionFlags.East, new Point(pt.X + 1, pt.Y));
                WallInfo infoWest = new WallInfo(DirectionFlags.West, new Point(pt.X - 1, pt.Y));

                // Check for doing something to all walls around an square

                foreach (MM345MapCommand mapCommand in new MM345MapCommand[] { MM345MapCommand.RemoveWall, MM345MapCommand.CreateWall })
                {
                    args.Command = mapCommand;
                    string strVerb = (mapCommand == MM345MapCommand.RemoveWall ? "Remove the" : "Add the");
                    args.Used.Clear();

                    if (WallInfo.GetCommands(args, infoNorth))
                        sb.AppendFormat("{0} North wall at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoSouth))
                        sb.AppendFormat("{0} South wall at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoEast))
                        sb.AppendFormat("{0} East wall at ({1},{2}), ", strVerb, pt.X, pt.Y);
                    else if (WallInfo.GetCommands(args, infoWest))
                        sb.AppendFormat("{0} West wall at ({1},{2}), ", strVerb, pt.X, pt.Y);
                }
            }

            foreach (Point pt in args.Points.Keys)
            {
                foreach (CommandInfo cmdInfo in args.Points[pt])
                {
                    if (!cmdInfo.Used)
                        sb.AppendFormat("{0}, ", cmdInfo.Description);
                }
            }

            return Global.Trim(sb).ToString();
        }
    }

    public class SetMapAttributeInfo : CollapsibleScriptItem
    {
        public override SummaryListType ItemType { get { return SummaryListType.SetMapAttribute; } }

        public Point Location;
        public byte Attribute;

        public SetMapAttributeInfo(Point pt, byte attr)
        {
            Location = pt;
            Attribute = attr;
        }

        public override string Condense(List<CollapsibleScriptItem> list, CollapsibleInfo ci)
        {
            if (!ci.ForNote)
            {
                if (list.Count == 1)
                    return String.Format("SetMapAttr");
                return String.Format("SetMapAttr x{0}", list.Count);
            }

            if (list.Count == 1)
            {
                SetMapAttributeInfo info = list[0] as SetMapAttributeInfo;
                return String.Format("Set map attribute at ({0}, {1}) to 0x{2:X2}", info.Location.X, info.Location.Y, info.Attribute);
            }

            Dictionary<byte, List<Point>> dict = new Dictionary<byte, List<Point>>();

            foreach (SetMapAttributeInfo info in list)
            {
                if (!dict.ContainsKey(info.Attribute))
                    dict.Add(info.Attribute, new List<Point>(1));
                dict[info.Attribute].Add(info.Location);
            }

            // Special case for setting every square on the map to the same thing
            if (dict.Values.Count == 1 && list.Count == 256)
                return String.Format("Set the map attribute for every square on this map to 0x{0:X2}", ((SetMapAttributeInfo) list[0]).Attribute);

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<byte, List<Point>> pair in dict)
            {
                if (sb.Length > 0)
                    sb.AppendLine();
                sb.AppendFormat("Set these map attributes to 0x{0:X2}: ", pair.Key);
                foreach (Point pt in pair.Value)
                    sb.AppendFormat("({0},{1}),", pt.X, pt.Y);
                Global.Trim(sb);
            }

            return sb.ToString();
        }
    }

    public class ConditionalInfo : CollapsibleScriptItem
    {
        public override SummaryListType ItemType { get { return SummaryListType.Conditional; } }

        ScriptSummary Summary;

        public ConditionalInfo(ScriptSummary summary)
        {
            Summary = summary;
        }

        public override string Condense(List<CollapsibleScriptItem> list, CollapsibleInfo ci)
        {
            StringBuilder sb = new StringBuilder();
            MM345ScriptAttribute prevAttr = MM345ScriptAttribute.Nothing;
            bool bCombined = false;

            if (!ci.ForNote)
            {
                foreach (ConditionalInfo info in list)
                {
                    if (info.Summary.Attribute == prevAttr)
                    {
                        if (sb.Length > 0 && sb[sb.Length - 1] == '?')
                            sb.Remove(sb.Length - 1, 1);
                        sb.AppendFormat("/{0}", info.Summary.ValueString);
                        bCombined = true;
                    }
                    else
                        sb.AppendFormat("&{0}", info.Summary.Description);
                    prevAttr = info.Summary.Attribute;
                }

                if (bCombined)
                    sb.Append("?");
                if (sb.Length > 0)
                    sb.Remove(0, 1);
                return sb.ToString();
            }

            if (list.Count == 1)
                return (list[0] as ConditionalInfo).Summary.Description;

            foreach (ConditionalInfo info in list)
            {
                if (info.Summary.Attribute == prevAttr)
                {
                    if (sb.Length > 0 && sb[sb.Length - 1] == ':')
                        sb.Remove(sb.Length - 1, 1);
                    sb.AppendFormat(" and {0}", info.Summary.ValuePhrase);
                    bCombined = true;
                }
                else
                    sb.AppendFormat("{0}{1}", sb.Length > 0 ? " or " : "", info.Summary.Description);

                prevAttr = info.Summary.Attribute;
            }
            if (bCombined)
                sb.Append(":");
            sb.Replace("::", ":");
            return sb.ToString();
        }
    }

    public class SummonInfo : CollapsibleScriptItem
    {
        public override SummaryListType ItemType { get { return SummaryListType.Summon; } }

        public string Name;
        public Point Location;

        public SummonInfo(string name, Point pt)
        {
            Name = name;
            Location = pt;
        }

        public override string ToString() { return String.Format("{0}:{1},{2}", Name, Location.X, Location.Y); }

        public override string Condense(List<CollapsibleScriptItem> summons, CollapsibleInfo ci)
        {
            if (summons == null || summons.Count < 1)
                return String.Empty;

            if (!ci.ForNote)
                return String.Format("Summon({0})", summons.Count);

            Dictionary<string, Dictionary<Point, int>> positions = new Dictionary<string, Dictionary<Point, int>>();
            foreach (SummonInfo si in summons)
            {
                if (!positions.ContainsKey(si.Name))
                    positions.Add(si.Name, new Dictionary<Point, int>());
                if (!positions[si.Name].ContainsKey(si.Location))
                    positions[si.Name].Add(si.Location, 0);
                positions[si.Name][si.Location]++;
            }

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, Dictionary<Point, int>> monster in positions)
            {
                bool bNewLine = true;
                bool bAnd = false;
                foreach (KeyValuePair<Point, int> location in positions[monster.Key])
                {
                    if (bNewLine)
                    {
                        if (sb.Length > 0)
                            sb.AppendLine();
                        sb.AppendFormat("Summon {0}", monster.Key);
                        if (ci.Location != location.Key)
                            sb.Append(" to");
                        bNewLine = false;
                    }

                    if (ci.Location != location.Key)
                    {
                        if (bAnd)
                            sb.Append(",");
                        sb.AppendFormat(" {0},{1}", location.Key.X, location.Key.Y);
                        bAnd = true;
                    }

                    if (location.Value > 1)
                        sb.AppendFormat(" ({0})", location.Value);
                }
            }

            return sb.ToString();
        }
    }

    public abstract class MMScripts : GameScripts
    {
    }

    public abstract class MM345ScriptLine : MMScriptLine
    {
        public MM345ScriptLine Parent;

        public override MemoryBytes HeaderBytes { get { return Bytes.GetRange(0, 4); } }
        public override MemoryBytes CommandBytes { get { return Bytes.GetRange(4); } }

        public abstract MM345Command CreateCommand(MMScriptInfo info, DirectionFlags facing, Point location);

        public override bool IsTeleportCommand
        {
            get
            {
                switch (Command)
                {
                    case MM345ScriptCommand.TeleportEndScript:
                    case MM345ScriptCommand.Teleport:
                    case MM345ScriptCommand.Fall:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override bool IsDisplayCommand
        {
            get
            {
                switch (Command)
                {
                    case MM345ScriptCommand.SignWall:
                    case MM345ScriptCommand.DisplayWall:
                    case MM345ScriptCommand.DisplayPair:
                    case MM345ScriptCommand.DisplayBottom:
                    case MM345ScriptCommand.DisplaySeat:
                    case MM345ScriptCommand.OutdoorSign:
                    case MM345ScriptCommand.Display:
                    case MM345ScriptCommand.DisplayLong:
                    case MM345ScriptCommand.DisplayMain:
                    case MM345ScriptCommand.Question:
                    case MM345ScriptCommand.PrintF:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override bool IsReturnCommand
        {
            get
            {
                switch (Command)
                {
                    case MM345ScriptCommand.Return:
                    case MM345ScriptCommand.ExitScript:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override bool IsSubscriptReturnCommand
        {
            get
            {
                switch (Command)
                {
                    case MM345ScriptCommand.Return:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override bool IsSubscriptCommand
        {
            get
            {
                switch (Command)
                {
                    case MM345ScriptCommand.SubScript:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public MM345ScriptCommand Command
        {
            get
            {
                if (Bytes == null || Bytes.Length < 5)
                    return MM345ScriptCommand.Invalid;
                return (MM345ScriptCommand) Bytes[4];
            }
        }

        public override Point InsertScriptLocation
        {
            get
            {
                if (Command != MM345ScriptCommand.SubScript || Bytes.Length < 7)
                    return Global.NullPoint;

                return new Point(Bytes[5], Bytes[6]);
            }
        }

        public override int InsertScriptLine
        {
            get
            {
                if (Command != MM345ScriptCommand.SubScript || Bytes.Length < 8)
                    return 0;

                return Bytes[7];
            }
        }

        public override Point TeleportLocation
        {
            get
            {
                switch (Command)
                {
                    case MM345ScriptCommand.Teleport:
                    case MM345ScriptCommand.TeleportEndScript:
                    case MM345ScriptCommand.Fall:
                        return new Point(Bytes[6], Bytes[7]);
                    default: return Global.NullPoint;
                }
            }
        }

        public override int TeleportMapIndex
        {
            get
            {
                switch (Command)
                {
                    case MM345ScriptCommand.Teleport:
                    case MM345ScriptCommand.TeleportEndScript:
                    case MM345ScriptCommand.Fall:
                        return Bytes[5];
                    default: return -1;
                }
            }
        }

        public override List<ScriptSummary> Summary(ScriptInfo info, bool bForNote)
        {
            List<ScriptSummary> list = new List<ScriptSummary>();

            if (info.Strings == null || Bytes == null)
                return list;
            if (Bytes.Length < 5)
                return list;

            ScriptSummary ss;
            switch (Command)
            {
                case MM345ScriptCommand.Receive:
                case MM345ScriptCommand.ReceiveItem:
                    ss = CreateCommand(info as MMScriptInfo, Facing, Location).Summary(Command, Bytes.GetRange(5), String.Empty, bForNote);
                    break;
                default:
                    ss = SummaryString(info as MMScriptInfo, bForNote);
                    break;
            }

            if (!ss.IsEmpty)
                list.Add(ss);

            return list;
        }

        public ScriptSummary SummaryString(MMScriptInfo info, bool bForNote)
        {
            MM345Command cmd = CreateCommand(info, Facing, Location);
            return cmd.Summary(Command, Bytes.GetRange(5), String.Empty, bForNote);
        }

        private int[] IntsFromBytes(byte[] bytes)
        {
            int[] ints = new int[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
                ints[i] = bytes[i];
            return ints;
        }

        public override int[] GotoLines
        {
            get
            {
                switch (Command)
                {
                    case MM345ScriptCommand.JumpIfLessOrEqual:
                        if (Bytes.Length < 8)
                            return new int[0];
                        return new int[] { Bytes[7] };
                    case MM345ScriptCommand.JumpIfMonster:
                        if (Bytes.Length < 7)
                            return new int[0];
                        return new int[] { Bytes[6] };
                    case MM345ScriptCommand.JumpIfGreaterOrEqual:
                        if (Bytes.Length < 8)
                            return new int[0];
                        return new int[] { Bytes[7] };
                    case MM345ScriptCommand.Random:
                        if (Bytes.Length < 8)
                            return new int[0];
                        return new int[] { Bytes[7] };
                    case MM345ScriptCommand.Condition:
                        if (Bytes.Length < 8)
                            return new int[0];
                        return new int[] { Bytes[7] };
                    case MM345ScriptCommand.Question:
                        if (Bytes.Length < 7)
                            return new int[0];
                        return new int[] { Bytes[6] };
                    case MM345ScriptCommand.NumericChoice:
                        if (Bytes.Length < 7)
                            return new int[0];
                        return IntsFromBytes(Bytes.GetRange(5));
                    case MM345ScriptCommand.MultiAnswer:
                        if (Bytes.Length < 9)
                            return new int[0];
                        return new int[] { Bytes[6], Bytes[8] };
                    case MM345ScriptCommand.JumpRandom:
                        if (Bytes.Length < 7)
                            return new int[0];
                        return IntsFromBytes(Bytes.GetRange(5));
                    case MM345ScriptCommand.NPC:
                        if (Bytes.Length < 10)
                            return new int[0];
                        return new int[] { Bytes[9] };
                    default:
                        return new int[0];
                }
            }
        }

        public string Description(MMScriptInfo info)
        {
            return Description(info, String.Empty);
        }

        public override string Description(ScriptInfo info, string strLinePrefix)
        {
            if (Bytes == null)
                return "<null>";

            // [length] [x] [y] [direction] [command] [arguments]
            if (Bytes.Length < 5)
                return String.Format("[{0}]", Global.ByteString(Bytes));

            byte bX = Bytes[0];
            byte bY = Bytes[1];
            byte bDir = Bytes[2];
            byte bBytes = Bytes[3];
            byte bCommand = Bytes[4];

            MM345Command cmd = CreateCommand(info as MMScriptInfo, Facing, Location);
                
            return cmd.Description((MM345ScriptCommand)bCommand, Bytes.GetRange(5), strLinePrefix);
        }

        public override string GetTabSeparatedString(ScriptInfo info, string strLinePrefix)
        {
            if (Bytes.Length < 5)
                return String.Format("{0}{1}\t[{2}] {3}\t{4}", strLinePrefix, Number, Bytes.Offset+1, Global.ByteString(Bytes), Description(info as MMScriptInfo));

            return String.Format("{0}{1}\t[{2}] {3}\t[{4}] {5}\t{6}",
                strLinePrefix, Number, 
                Bytes.Offset + 1, Global.ByteString(Bytes.GetRange(0, 4)), 
                Bytes.Offset + 5, Global.ByteString(Bytes.GetRange(4)), 
                Description(info as MMScriptInfo));
        }
    }

    public abstract class MM345Script : MMScript
    {
        public List<ScriptLine> m_lines;

        public override List<ScriptLine> Lines { get { return m_lines; } }

        public override bool HasHeaderBytes { get { return true; } }

        public override int NumBytes
        {
            get 
            {
                int iCount = 0;
                foreach (MMScriptLine line in m_lines)
                    iCount += line.Bytes.Length;
                return iCount;
            }
        }

        public static int NumArgs(MM345ScriptCommand cmd)
        {
            // These are the minimum number of bytes required for the command
            // Some commands have optional further bytes
            switch (cmd)
            {
                case MM345ScriptCommand.Display:
                case MM345ScriptCommand.DisplayLong:
                case MM345ScriptCommand.SignWall:
                case MM345ScriptCommand.DisplayWall:
                case MM345ScriptCommand.DisplayBottom:
                case MM345ScriptCommand.OutdoorSign:
                case MM345ScriptCommand.PlayAudioEffect:
                case MM345ScriptCommand.PlayAudio:
                case MM345ScriptCommand.DisplaySeat:
                case MM345ScriptCommand.PrintF:
                case MM345ScriptCommand.Business:
                case MM345ScriptCommand.SetAffected:
                case MM345ScriptCommand.SetItemType:
                case MM345ScriptCommand.NumericChoice:
                case MM345ScriptCommand.SetWorldSide:
                    return 1;
                case MM345ScriptCommand.SetCommand:
                case MM345ScriptCommand.SetAttribute:
                case MM345ScriptCommand.AlterHED:
                case MM345ScriptCommand.Receive:
                case MM345ScriptCommand.Trade:
                case MM345ScriptCommand.JumpIfMonster:
                case MM345ScriptCommand.DisplayPair:
                case MM345ScriptCommand.SwapGraphics:
                case MM345ScriptCommand.JumpRandom:
                    return 2;
                case MM345ScriptCommand.Teleport:
                case MM345ScriptCommand.Fall:
                case MM345ScriptCommand.JumpIfGreaterOrEqual:
                case MM345ScriptCommand.TeleportEndScript:
                case MM345ScriptCommand.Condition:
                case MM345ScriptCommand.JumpIfLessOrEqual:
                case MM345ScriptCommand.Random:
                case MM345ScriptCommand.SubScript:
                case MM345ScriptCommand.Damage:
                case MM345ScriptCommand.SelectGraphic:
                case MM345ScriptCommand.MoveGraphic:
                case MM345ScriptCommand.AddIfLessOrEqual:   // 4 in MM3, 3 in MM4/5 (because it's RandomDamage instead of AddIfLessOrEqual)
                case MM345ScriptCommand.SetMapAttribute:
                    return 3;
                case MM345ScriptCommand.NPC:
                case MM345ScriptCommand.Question:
                case MM345ScriptCommand.Summon:
                case MM345ScriptCommand.SetAttributeIfEqual:
                case MM345ScriptCommand.MapCommand:
                case MM345ScriptCommand.ReceiveItem:
                case MM345ScriptCommand.MultiAnswer:
                    return 4;
                case MM345ScriptCommand.AddIfEqualAndLessThanOrEqual:
                    return 6;
                case MM345ScriptCommand.DoNothing:
                case MM345ScriptCommand.ZeroAll:
                case MM345ScriptCommand.ZeroAll2:
                case MM345ScriptCommand.ExitScript:
                case MM345ScriptCommand.Return:
                case MM345ScriptCommand.EndingSequence:
                case MM345ScriptCommand.EndingSequence2:
                case MM345ScriptCommand.EndingSequence3:
                case MM345ScriptCommand.CopyProtection:
                case MM345ScriptCommand.RandomChar:
                default:
                    return 0;
            }
        }

        public static DirectionFlags FacingFromInt(int i)
        {
            switch (i)
            {
                case 0: return DirectionFlags.North;
                case 1: return DirectionFlags.South;
                case 2: return DirectionFlags.East;
                case 3: return DirectionFlags.West;
                case 4: return DirectionFlags.All;
                default: return DirectionFlags.None;
            }
        }

        public override bool DoesNothing
        {
            get
            {
                foreach (MM345ScriptLine line in m_lines)
                {
                    if (line.Command != MM345ScriptCommand.DoNothing)
                        return false;
                }
                return true;
            }
        }

        public override void Summary(int iDepth, List<ScriptSummary> listSummary, ScriptInfo info, int iStartLine, bool bSkipSubscripts, bool bForNote)
        {
            if (iDepth > 5)
            {
                listSummary.Add(new ScriptSummary("[depth exceeded]"));
                return;
            }

            int iMaxGoto = -1;
            foreach (MM345ScriptLine line in m_lines)
            {
                if (line.Number < iStartLine)
                    continue;
                
                foreach(int iGoto in line.GotoLines)
                    iMaxGoto = Math.Max(iMaxGoto, iGoto);

                // Stop adding lines after an Exit or Return that can't be jumped past.
                // This prevents some infinite recursions in the list that can't actually happen in-game
                if (line.IsReturnCommand && iMaxGoto <= line.Number)
                    break;

                if (line.Command == MM345ScriptCommand.SubScript && info.Scripts != null && info.Scripts.Scripts.ContainsKey(line.InsertScriptLocation) && !bSkipSubscripts)
                {
                    info.Scripts.Scripts[line.InsertScriptLocation][0].Summary(iDepth + 1, listSummary, info, line.InsertScriptLine, bSkipSubscripts, bForNote);
                    continue;
                }

                listSummary.AddRange(line.Summary(info as MMScriptInfo, bForNote));
            }
        }
    }

    public class RewardContent
    {
        public bool Gold;
        public bool Gems;
        public bool Food;
        public bool Items;
        public bool Condition;
        public bool Other;
        public bool Payment;

        public static RewardContent Empty { get { return new RewardContent(); } }
        public bool OnlyGoldGemsFoodItems { get { return (Gold || Gems || Food || Items) && !Condition && !Other && !Payment; } }

        public RewardContent()
        {
            Gold = false;
            Gems = false;
            Food = false;
            Items = false;
            Condition = false;
            Other = false;
            Payment = false;
        }
    }

    public enum SummaryListType
    {
        None,
        Reward,
        Summon,
        SetMapAttribute,
        Conditional,
        SetMapAppearance,
    }

    public class CollapsibleInfo
    {
        public Point Location;
        public RewardContent Content;
        public int Index;
        public StringBuilder Builder;
        public string PreviousLine;
        public bool ForNote;
        public string DupFormat;
        public bool SkipNewline;
        public int SummaryCount;
        public int Duplicate;

        public CollapsibleInfo(Point pt, RewardContent content, int iIndex, StringBuilder sb, string strPrevLine, bool bForNote,
            string strDupFormat, bool bSkipNewline, int iSummaryCount, int iDuplicate)
        {
            Location = pt;
            Content = content;
            Index = iIndex;
            Builder = sb;
            PreviousLine = strPrevLine;
            ForNote = bForNote;
            DupFormat = strDupFormat;
            SkipNewline = bSkipNewline;
            SummaryCount = iSummaryCount;
            Duplicate = iDuplicate;
        }

        public bool AddSingleSummaryItem(string strLine)
        {
            if (Index > 0 && strLine == PreviousLine)
                Duplicate++;
            else
            {
                if (Builder.Length > 150 && !ForNote)
                {
                    if (!String.IsNullOrWhiteSpace(strLine))
                        Builder.Append("; [...]");
                    return false;
                }

                if (Duplicate > 1)
                {
                    if (Builder.Length > 1 && Builder[Builder.Length - 2] == ':')
                    {
                        Builder.Remove(Builder.Length - 2, 2);
                        Builder.AppendFormat(DupFormat, Duplicate);
                        Builder.Append(": ");
                    }
                    else
                        Builder.AppendFormat(DupFormat, Duplicate);
                }

                if (!String.IsNullOrWhiteSpace(strLine))
                {
                    if (ForNote)
                    {
                        if (Builder.Length > 0 && !SkipNewline)
                            Builder.AppendLine();
                        SkipNewline = false;
                        Builder.Append(strLine);
                        if (strLine.EndsWith(":"))
                        {
                            Builder.Append(" ");
                            SkipNewline = true;
                        }
                    }
                    else if (Builder.Length > 50 || (SummaryCount > 3 && Index < SummaryCount - 2))
                        Builder.AppendFormat("; {0}", Global.Abbreviate(strLine));
                    else
                        Builder.AppendFormat("; {0}", strLine);
                }

                Duplicate = 1;

                PreviousLine = strLine;
            }
            return true;
        }

    }

    public abstract class CollapsibleScriptItem : IComparable<CollapsibleScriptItem>
    {
        public int CompareTo(CollapsibleScriptItem item)
        {
            if (item is MM345Reward && this is MM345Reward)
                return ((MM345Reward) this).CompareTo((MM345Reward)item);
            return (item == this ? 0 : 1);
        }

        public abstract SummaryListType ItemType { get; }
        public abstract string Condense(List<CollapsibleScriptItem> items, CollapsibleInfo ci);

        public static bool Collapse(List<CollapsibleScriptItem> items, CollapsibleInfo ci)
        {
            // Only condense adjacent items that are of the same derived type
            List<CollapsibleScriptItem> listTemp = new List<CollapsibleScriptItem>();
            SummaryListType typePrev = SummaryListType.None;
            foreach (CollapsibleScriptItem item in items)
            {
                if (item.ItemType == typePrev || typePrev == SummaryListType.None)
                    listTemp.Add(item);
                else if (listTemp.Count > 0)
                {
                    if (!ci.AddSingleSummaryItem(listTemp[0].Condense(listTemp, ci)))
                        return false;
                    listTemp.Clear();
                    listTemp.Add(item);
                }
                typePrev = item.ItemType;
            }
            if (listTemp.Count > 0)
                return ci.AddSingleSummaryItem(listTemp[0].Condense(listTemp, ci));

            return true;
        }
    }

    public class MM345Reward : CollapsibleScriptItem, IComparable<MM345Reward>
    {
        public override SummaryListType ItemType { get { return SummaryListType.Reward; } }

        public enum Verb
        {
            None,
            SetZero,
            ReceiveLose,
            ReceivePay,
            ReceiveRemove,
            AdvanceGoBack,
            CastDispel,
            AddSubtract,
            PlusMinus,
            SetDirectly,
            LearnForget,
        }

        public MM345ScriptAttribute Attribute;
        public string OverrideValue;
        public string OverrideAll;
        public string NoteFormat;
        public string ScriptFormat;
        public long Value;
        public Verb Action;
        public bool Abbreviate;
        public bool IncludeReceiveString;
        public bool ForPaying;
        public bool ZeroIsVariable;
        public bool Singular;

        public MM345Reward()
        {
            Attribute = MM345ScriptAttribute.Nothing;
            OverrideValue = String.Empty;
            Value = 0;
            Action = Verb.None;
            Abbreviate = false;
            IncludeReceiveString = true;
            ForPaying = false;
            ZeroIsVariable = false;
            Singular = false;
            NoteFormat = "{0}{1} {2}";
            ScriptFormat = "{0}{1}({2})";
        }

        public bool IsEmpty { get { return Action == Verb.None; } }

        public int CompareTo(MM345Reward reward)
        {
            if ((int)Attribute != (int)reward.Attribute)
                return SortOrder(Attribute) - SortOrder(reward.Attribute);
            switch (Attribute)
            {
                case MM345ScriptAttribute.PartyBit:
                case MM345ScriptAttribute.WorldBit:
                case MM345ScriptAttribute.QuestBit:
                case MM345ScriptAttribute.CharBit:
                    if (reward.ForPaying != ForPaying)
                        return ForPaying ? -1 : 1;
                    goto default;
                default:
                    return (int)(Value - reward.Value);
            }
        }

        public static int SortOrder(MM345ScriptAttribute attr)
        {
            switch (attr)
            {
                case MM345ScriptAttribute.Gold: return 0;
                case MM345ScriptAttribute.RandomGold: return 1;
                case MM345ScriptAttribute.Gems: return 2;
                case MM345ScriptAttribute.RandomGems: return 3;
                case MM345ScriptAttribute.Food: return 4;
                case MM345ScriptAttribute.RandomFood: return 5;
                default: return (int)attr + 6;
            }
        }

        public string ActionString
        {
            get
            {
                switch (Action)
                {
                    case Verb.SetZero: return SetZero;
                    case Verb.ReceiveLose: return RecvLose;
                    case Verb.ReceivePay: return RecvPay;
                    case Verb.ReceiveRemove: return RecvRemove;
                    case Verb.AdvanceGoBack: return AdvanceGoBack;
                    case Verb.CastDispel: return CastDispel;
                    case Verb.LearnForget: return LearnForget;
                    case Verb.SetDirectly: return "Set ";
                    case Verb.AddSubtract:
                    case Verb.PlusMinus:
                    default:
                        return AddSubtract;
                }
            }
        }

        public string Receive { get { return IncludeReceiveString ? (Abbreviate ? "+" : "Receive ") : String.Empty; } }
        public string SetZero { get { return ForPaying ? (Abbreviate ? "-" : "Zero ") : (IncludeReceiveString ? (Abbreviate ? "+" : "Set ") : ""); } }
        public string RecvLose { get { return ForPaying ? (Abbreviate ? "-" : "Lose ") : Receive; } }
        public string RecvPay { get { return ForPaying ? (Abbreviate ? "-" : "Pay ") : Receive; } }
        public string RecvRemove { get { return ForPaying ? (Abbreviate ? "-" : "Remove ") : Receive; } }
        public string LearnForget { get { return ForPaying ? (Abbreviate ? "-" : "Forget ") : (IncludeReceiveString ? (Abbreviate ? "+" : "Learn ") : ""); } }
        public string AdvanceGoBack { get { return ForPaying ? (Abbreviate ? "-" : "Go Back ") : (IncludeReceiveString ? (Abbreviate ? "+" : "Advance ") : ""); } }
        public string CastDispel { get { return ForPaying ? (Abbreviate ? "-" : "Dispel ") : (IncludeReceiveString ? (Abbreviate ? "+" : "Cast ") : ""); } }
        public string AddSubtract { get { return ForPaying ? "-" : IncludeReceiveString ? "+" : ""; } }

        public bool IsCurrency { get { return (Attribute == MM345ScriptAttribute.Gold || Attribute == MM345ScriptAttribute.Gems); } }

        public bool IsPermanentPrimaryStat
        {
            get
            {
                switch (Attribute)
                {
                    case MM345ScriptAttribute.PermMight:
                    case MM345ScriptAttribute.PermIntellect:
                    case MM345ScriptAttribute.PermPersonality:
                    case MM345ScriptAttribute.PermEndurance:
                    case MM345ScriptAttribute.PermSpeed:
                    case MM345ScriptAttribute.PermAccuracy:
                    case MM345ScriptAttribute.PermLuck:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public string AttributeName
        {
            get
            {
                if (Abbreviate)
                    return MM345Command.ShortAttribute((byte)Attribute);
                else
                {
                    switch (Attribute)
                    {
                        case MM345ScriptAttribute.CharBit: return "CharBit";
                        case MM345ScriptAttribute.PartyBit: return "PartyBit";
                        case MM345ScriptAttribute.QuestBit: return "QuestBit";
                        case MM345ScriptAttribute.WorldBit: return "WorldBit";
                        case MM345ScriptAttribute.Days: return "Days";
                        case MM345ScriptAttribute.GameYear: return "Years";
                        default:
                            return MM345Command.Attribute((byte)Attribute);
                    }
                }
            }
        }

        public string ScriptString
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(OverrideAll))
                    return OverrideAll;
                if (Action == Verb.AddSubtract && Abbreviate)
                    return String.Format("{0}{1}{2}", AttributeName, ActionString, Value);
                if (Action == Verb.AddSubtract)
                    return String.Format("{0}{1} to ({2})", ActionString, Value, AttributeName);
                if (Action == Verb.SetDirectly && Abbreviate)
                    return String.Format("{0}={1}", AttributeName, Value);
                if (Action == Verb.SetDirectly)
                    return String.Format("Set ({0}) to ({1})", AttributeName, Value);
                return String.Format(ScriptFormat, 
                    ActionString,
                    AttributeName, 
                    String.IsNullOrWhiteSpace(OverrideValue) ? Value.ToString() : OverrideValue);
            }
        }

        public string NoteString
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(OverrideAll))
                    return OverrideAll;

                if (Action == Verb.AddSubtract && ForPaying)
                    return String.Format("-{0} from {1}", Value, AttributeName);
                if (Action == Verb.AddSubtract)
                    return String.Format("+{0} to {1}", Value, AttributeName);
                if (Action == Verb.SetZero)
                    return String.Format("{0}{1}{2}", AddSubtract, AttributeName, Value);
                if (Action == Verb.SetDirectly)
                    return String.Format("{0}{1} to {2}",
                        ActionString,
                        AttributeName,
                        String.IsNullOrWhiteSpace(OverrideValue) ? Value.ToString() : OverrideValue);
                return String.Format(NoteFormat,
                    AddSubtract,
                    String.IsNullOrWhiteSpace(OverrideValue) ? Value.ToString() : OverrideValue,
                    AttributeName);
            }
        }

        public override string ToString() { return ScriptString; }

        public bool IsBit
        {
            get
            {
                return Attribute == MM345ScriptAttribute.PartyBit ||
                       Attribute == MM345ScriptAttribute.QuestBit ||
                       Attribute == MM345ScriptAttribute.CharBit ||
                       Attribute == MM345ScriptAttribute.WorldBit;
            }
        }

        public static void UpdateContent(RewardContent content, MM345Reward reward)
        {
            if (content == null)
                return;

            if (reward.ForPaying)
                content.Payment = true;
            else
            {
                switch (reward.Attribute)
                {
                    case MM345ScriptAttribute.Gold:
                    case MM345ScriptAttribute.RandomGold:
                        content.Gold = true;
                        break;
                    case MM345ScriptAttribute.Gems:
                    case MM345ScriptAttribute.RandomGems:
                        content.Gems = true;
                        break;
                    case MM345ScriptAttribute.Food:
                    case MM345ScriptAttribute.RandomFood:
                        content.Food = true;
                        break;
                    case MM345ScriptAttribute.Item:
                    case MM345ScriptAttribute.RandomItem:
                        content.Items = true;
                        break;
                    case MM345ScriptAttribute.Condition:
                        content.Condition = true;
                        break;
                    default:
                        content.Other = true;
                        break;
                }
            }
        }

        public override string Condense(List<CollapsibleScriptItem> rewards, CollapsibleInfo ci)
        {
            if (rewards == null || rewards.Count < 1)
                return String.Empty;

            rewards.Sort();

            StringBuilder sb = new StringBuilder();
            StringBuilder sbBits = new StringBuilder();     // Bits are distinct lines from other types of rewards

            int i = 0;
            for (i = 0; i < rewards.Count - 1; i++)
            {
                MM345Reward reward = rewards[i] as MM345Reward;
                MM345Reward rewardNext = rewards[i+1] as MM345Reward;

                UpdateContent(ci.Content, reward);

                if ((reward.Attribute == MM345ScriptAttribute.Gold && rewardNext.Attribute == MM345ScriptAttribute.RandomGold) ||
                    (reward.Attribute == MM345ScriptAttribute.Food && rewardNext.Attribute == MM345ScriptAttribute.RandomFood) ||
                    (reward.Attribute == MM345ScriptAttribute.Gems && rewardNext.Attribute == MM345ScriptAttribute.RandomGems))
                {
                    // X Item + RandomItem(Y) => "X+1 to X+Y Item"
                    if (ci.ForNote)
                        sb.AppendFormat("+{0}-{1} {2}, ", reward.Value + 1, rewardNext.Value + reward.Value, reward.AttributeName);
                    else
                        sb.AppendFormat("{0}{1}-{2} {3}, ", reward.ActionString, reward.Value + 1, rewardNext.Value + reward.Value, reward.AttributeName);
                    i++;
                }
                else if (reward.Attribute == MM345ScriptAttribute.Condition && rewardNext.Attribute == MM345ScriptAttribute.Condition)
                {
                    // +Condition "X", +Condition "x" => +Condition "X:2"
                    int iCount = 1;
                    while (reward.Attribute == rewardNext.Attribute && reward.Value == rewardNext.Value)
                    {
                        iCount++;
                        i++;
                        if (i >= rewards.Count - 1)
                            break;
                        rewardNext = rewards[i + 1] as MM345Reward;
                    }
                    if (iCount > 1)
                    {
                        if (ci.ForNote)
                            sb.AppendFormat("Character becomes {0}:{1}, ", reward.OverrideValue, iCount);
                        else
                            sb.AppendFormat("{0}Cond({1}:{2}), ", reward.ActionString, reward.OverrideValue, iCount);
                    }
                }
                else if (reward.Attribute == MM345ScriptAttribute.RandomItem && rewardNext.Attribute == MM345ScriptAttribute.RandomItem &&
                    reward.Value == rewardNext.Value)
                {
                    // 1 Random Item(..), 1 Random Item(..) => 2 Random Items(..)
                    long iLevel = reward.Value;
                    int iCount = 1;
                    while (reward.Attribute == rewardNext.Attribute && reward.Value == rewardNext.Value)
                    {
                        iCount++;
                        i++;
                        if (i >= rewards.Count - 1)
                            break;
                        rewardNext = rewards[i + 1] as MM345Reward;
                    }
                    if (iCount > 1)
                    {
                        if (ci.ForNote)
                            sb.AppendFormat("+{0} {1} Items, ", iCount, reward.OverrideValue);
                        else
                            sb.AppendFormat("{0}{1}({2}) x{3}, ", reward.ActionString, reward.AttributeName, reward.OverrideValue, iCount);
                    }
                }
                else if (reward.IsPermanentPrimaryStat && i < rewards.Count - 6)
                {
                    // +1 to each stat => +1 to all stats
                    long iFirst = reward.Value;
                    Dictionary<MM345ScriptAttribute, long> stats = new Dictionary<MM345ScriptAttribute, long>();
                    for (int iStatTest = i; iStatTest < i + 7; iStatTest++)
                    {
                        MM345Reward rewardTest = rewards[iStatTest] as MM345Reward;
                        if (rewardTest.IsPermanentPrimaryStat && !stats.ContainsKey(rewardTest.Attribute))
                            stats.Add(rewardTest.Attribute, rewardTest.Value);
                    }
                    if (stats.ContainsKey(MM345ScriptAttribute.PermMight) &&
                        stats.ContainsKey(MM345ScriptAttribute.PermIntellect) &&
                        stats.ContainsKey(MM345ScriptAttribute.PermPersonality) &&
                        stats.ContainsKey(MM345ScriptAttribute.PermEndurance) &&
                        stats.ContainsKey(MM345ScriptAttribute.PermSpeed) &&
                        stats.ContainsKey(MM345ScriptAttribute.PermAccuracy) &&
                        stats.ContainsKey(MM345ScriptAttribute.PermLuck) &&
                        stats.Values.All(v => v == iFirst)
                       )
                    {
                        if (ci.ForNote)
                            sb.AppendFormat("{0} to Permanent Primary Stats, ", Global.AddPlus(iFirst));
                        else
                            sb.AppendFormat("{0}{1}(PermPrimaryStats), ", reward.ActionString, iFirst);
                        i += 6;
                        if (i < rewards.Count - 1)
                            rewardNext = rewards[i + 1] as MM345Reward;
                    }
                    else
                        sb.AppendFormat("{0}, ", ci.ForNote ? reward.NoteString : reward.ScriptString);
                }
                else if ((reward.Attribute == MM345ScriptAttribute.CurrentHP ||
                          rewardNext.Attribute == MM345ScriptAttribute.CurrentSP)
                        && reward.Attribute == rewardNext.Attribute)
                {
                    // +X HP, +Y HP => +(X+Y) HP
                    int iTotal = (int)reward.Value;
                    while (reward.Attribute == rewardNext.Attribute)
                    {
                        iTotal += (int)rewardNext.Value;
                        i++;
                        if (i >= rewards.Count - 1)
                            break;
                        rewardNext = rewards[i + 1] as MM345Reward;
                    }
                    if (iTotal > 0)
                    {
                        if (ci.ForNote)
                            sb.AppendFormat("{0} to {1}, ", Global.AddPlus(iTotal), reward.AttributeName);
                        else
                            sb.AppendFormat("{0}{1}{2}, ", reward.ActionString, iTotal, reward.AttributeName);
                    }
                }
                else if (reward.Attribute == MM345ScriptAttribute.Item && rewardNext.Attribute == MM345ScriptAttribute.Item &&
                    reward.Value == rewardNext.Value)
                {
                    // +Item "..", +Item ".." => +2 Items ".."
                    long iLevel = reward.Value;
                    int iCount = 1;
                    while (reward.Attribute == rewardNext.Attribute && reward.Value == rewardNext.Value)
                    {
                        iCount++;
                        i++;
                        if (i >= rewards.Count - 1)
                            break;
                        rewardNext = rewards[i + 1] as MM345Reward;
                    }
                    if (iCount > 1)
                    {
                        if (ci.ForNote)
                            sb.AppendFormat("+{0} Items \"{1}\", ", iCount, reward.OverrideValue);
                        else
                            sb.AppendFormat("{0}{1}({2}) x{3}, ", reward.ActionString, reward.AttributeName, reward.OverrideValue, iCount);
                    }
                }
                else if (reward.IsBit && (rewardNext.Attribute == reward.Attribute) && (reward.ForPaying == rewardNext.ForPaying))
                {
                    // Bit X, Bit Y => Bits X,Y
                    MM345ScriptAttribute bit = reward.Attribute;
                    bool bZero = reward.ForPaying;
                    sbBits.AppendFormat("{0}{1}s {2},", bZero ? "-" : "+", reward.AttributeName, reward.Value);
                    while (rewardNext.Attribute == bit && rewardNext.ForPaying == bZero)
                    {
                        sbBits.AppendFormat("{0},", rewardNext.Value);
                        i++;
                        if (i >= rewards.Count - 1)
                            break;
                        rewardNext = rewards[i + 1] as MM345Reward;
                    }
                    sbBits.Append(" ");
                }
                else
                {
                    if (reward.IsBit)
                        sbBits.AppendFormat("{0}, ", ci.ForNote ? reward.NoteString : reward.ScriptString);
                    else
                        sb.AppendFormat("{0}, ", ci.ForNote ? reward.NoteString : reward.ScriptString);
                }
            }

            if (i < rewards.Count)
            {
                MM345Reward reward = rewards[i] as MM345Reward;
                UpdateContent(ci.Content, reward);
                if (reward.IsBit)
                    sbBits.Append(ci.ForNote ? reward.NoteString : reward.ScriptString);
                else
                    sb.Append(ci.ForNote ? reward.NoteString : reward.ScriptString);
            }

            Global.Trim(sb);
            Global.Trim(sbBits);

            if (ci.ForNote)
            {
                if (sb.Length > 0)
                {
                    sb.Insert(0, "(");
                    sb.Append(")");
                }
                if (sbBits.Length > 0)
                {
                    sbBits.Insert(0, "(");
                    sbBits.Append(")");
                }
                return sb.ToString() + ((sb.Length > 0 && sbBits.Length > 0) ? "\r\n" : "") + sbBits.ToString();
            }
            return sb.ToString() + ((sb.Length > 0 && sbBits.Length > 0) ? ", " : "") + sbBits.ToString();
        }

        public static PermStatReward PermanentStats(List<MM345Reward> rewards)
        {
            PermStatReward stats = PermStatReward.None;
            foreach (MM345Reward reward in rewards)
            {
                switch (reward.Attribute)
                {
                    case MM345ScriptAttribute.PermMight:
                        stats |= PermStatReward.Might;
                        break;
                    case MM345ScriptAttribute.PermIntellect:
                        stats |= PermStatReward.Intellect;
                        break;
                    case MM345ScriptAttribute.PermPersonality:
                        stats |= PermStatReward.Personality;
                        break;
                    case MM345ScriptAttribute.PermEndurance:
                        stats |= PermStatReward.Endurance;
                        break;
                    case MM345ScriptAttribute.PermSpeed:
                        stats |= PermStatReward.Speed;
                        break;
                    case MM345ScriptAttribute.PermAccuracy:
                        stats |= PermStatReward.Accuracy;
                        break;
                    case MM345ScriptAttribute.PermLuck:
                        stats |= PermStatReward.Luck;
                        break;
                    case MM345ScriptAttribute.PermFireRes:
                        stats |= PermStatReward.FireRes;
                        break;
                    case MM345ScriptAttribute.PermColdRes:
                        stats |= PermStatReward.ColdRes;
                        break;
                    case MM345ScriptAttribute.PermElectricityRes:
                        stats |= PermStatReward.ElecRes;
                        break;
                    case MM345ScriptAttribute.PermPoisonRes:
                        stats |= PermStatReward.PoisRes;
                        break;
                    case MM345ScriptAttribute.PermEnergyRes:
                        stats |= PermStatReward.EnergyRes;
                        break;
                    case MM345ScriptAttribute.PermMagicRes:
                        stats |= PermStatReward.MagicRes;
                        break;
                    case MM345ScriptAttribute.PermLevel:
                        stats |= PermStatReward.Level;
                        break;
                    default:
                        break;
                }
            }
            return stats;
        }

        public static bool IsHarmful(List<MM345Reward> rewards)
        {
            foreach (MM345Reward reward in rewards)
            {
                switch (reward.Attribute)
                {
                    case MM345ScriptAttribute.Condition:
                        if (reward.Value != 16)     // cure all
                            return true;
                        break;
                    case MM345ScriptAttribute.DamageType:
                    case MM345ScriptAttribute.PhysicalDamage:
                    case MM345ScriptAttribute.GameYear:
                    case MM345ScriptAttribute.TempAge:
                        return true;
                    case MM345ScriptAttribute.CurrentHP:
                    case MM345ScriptAttribute.CurrentSP:
                    case MM345ScriptAttribute.Gold:
                    case MM345ScriptAttribute.Gems:
                        if (reward.ForPaying)
                            return true;
                        break;
                    default:
                        break;
                }
            }
            return false;
        }
    }

    public enum ScriptCommand
    {
        None = 0,
        Business,
        Bank,
        Store,
        Guild,
        Inn,
        Tavern,
        Temple,
        Training,
        Arena,
        Joke,
        Rewards,
        Encounter,
        NPC,
        Text,
        Sign,
        Jump,
        Summon,
        WhoWill,
        ExitScript,
        Teleport,
        Portal,
        Damage,
        AlterMap,
        SetItemType,
        SetMapAttribute,
        Conditional,
        SetCommand,
        WarZone,
        Pyramid,
        Tower,
        Input,
        SetSide,
        Stairs,
        CheckLocation
    }

    public class ScriptSummary
    {
        public List<MM345Reward> Rewards;
        public List<SummonInfo> Summons;
        public string Description;
        public string Symbol;
        public IconName Icon;
        public ScriptCommand Command;
        public string Verb;
        public Point Target;
        public long Value;
        public string ValueOverride;
        public string ValuePhraseOverride;
        public DirectionFlags Direction;
        public MapXY Destination;
        public bool NoNewline = false;
        public bool IsExitCommand;

        public string ValueString { get { return String.IsNullOrWhiteSpace(ValueOverride) ? String.Format("{0}", Value) : ValueOverride; } }
        public string ValuePhrase { get { return String.IsNullOrWhiteSpace(ValuePhraseOverride) ? ValueString : ValuePhraseOverride; } }

        public MM345ScriptAttribute Attribute;

        public ScriptSummary(string description)
        {
            SetDefaults();
            Description = description;
        }

        public void SetDefaults()
        {
            Command = ScriptCommand.None;
            Rewards = null;
            Description = String.Empty;
            Symbol = String.Empty;
            Verb = String.Empty;
            Summons = null;
            Target = Global.NullPoint;
            Value = 0;
            ValueOverride = String.Empty;
            Attribute = MM345ScriptAttribute.Nothing;
            Direction = DirectionFlags.None;
            NoNewline = false;
            Destination = null;
        }

        public ScriptSummary(List<MM345Reward> rewards, string description)
        {
            SetDefaults();
            Rewards = rewards;
            Command = ScriptCommand.Rewards;
            Description = description;
        }

        public ScriptSummary(SummonInfo summon, string description)
        {
            SetDefaults();
            Description = description;
            Command = ScriptCommand.Summon;
            Summons = new List<SummonInfo>(1);
            Summons.Add(summon);
        }

        public static string SymbolFromBusinessCommand(ScriptCommand cmd)
        {
            switch (cmd)
            {
                case ScriptCommand.Arena: return "Ar";
                case ScriptCommand.Bank: return "Ba";
                case ScriptCommand.Store: return "St";
                case ScriptCommand.Guild: return "Gu";
                case ScriptCommand.Inn: return "In";
                case ScriptCommand.Tavern: return "Ta";
                case ScriptCommand.Temple: return "Te";
                case ScriptCommand.Training: return "Tr";
                case ScriptCommand.Joke: return "Jo";
                case ScriptCommand.WarZone: return "Wa";
                case ScriptCommand.Pyramid: return "Py";
                case ScriptCommand.Tower: return "Tw";
                default: return "?";
            }
        }

        public ScriptSummary NewDescription(string strNewDescription)
        {
            Description = strNewDescription;
            return this;
        }

        public ScriptSummary(string description, ScriptCommand cmd, string symbol = "", string verb = "", long val = 0)
        {
            Rewards = null;
            Description = description;
            Command = cmd;
            Symbol = symbol;
            Verb = verb;

            if (cmd == ScriptCommand.Business)
            {
                if (Description.Contains("Inn"))
                    Command = ScriptCommand.Inn;
                else if (Description.Contains("Bank"))
                    Command = ScriptCommand.Bank;
                else if (Description.Contains("Store"))
                    Command = ScriptCommand.Store;
                else if (Description.Contains("Guild"))
                    Command = ScriptCommand.Guild;
                else if (Description.Contains("Tavern"))
                    Command = ScriptCommand.Tavern;
                else if (Description.Contains("Temple"))
                    Command = ScriptCommand.Temple;
                else if (Description.Contains("Training"))
                    Command = ScriptCommand.Training;
                else if (Description.Contains("Arena"))
                    Command = ScriptCommand.Arena;
                else if (Description.Contains("Joke"))
                    Command = ScriptCommand.Joke;
                else if (Description.Contains("WarZone"))
                    Command = ScriptCommand.WarZone;
                else if (Description.Contains("Pyramid"))
                    Command = ScriptCommand.Pyramid;
                else if (Description.Contains("Tower"))
                    Command = ScriptCommand.Tower;
            }
        }

        public bool IsEmpty { get { return String.IsNullOrWhiteSpace(Description); } }
        public override string ToString() { return Description; }
    }

    public enum MM345MapCommand
    {
        RemoveWall0 = 0,
        SurroundMountains = 5,
        OpenDoor = 6,
        RemoveWall7 = 7,
        CreateWall = 8,
        CloseDoor = 9,
        LockDoor = 10,
        AddTorch = 12,
        RemoveWall = 256
    }

    public abstract class MM345Command
    {
        protected MMScriptInfo m_info;
        protected MemoryBytes m_args;
        protected MM345ScriptCommand m_cmd;
        protected DirectionFlags m_facing;
        protected Point m_location;
        protected string m_strLinePrefix;
        protected bool m_bAbbrev;
        protected bool m_bForNote;

        public MM345Command(MMScriptInfo info, DirectionFlags facing, Point location)
        {
            m_info = info;
            m_args = null;
            m_cmd = MM345ScriptCommand.NoAction;
            m_strLinePrefix = String.Empty;
            m_bAbbrev = false;
            m_facing = facing;
            m_location = location;
        }

        public virtual MapTitlePair GetMapTitlePair(int index) { return new MapTitlePair(index, "Unknown"); }
        protected virtual bool IsGems(byte b) { return (b == (byte)MM345ScriptAttribute.Gems); }
        protected virtual bool IsGold(byte b) { return (b == (byte)MM345ScriptAttribute.Gold); }
        protected virtual bool IsFacing(byte b) { return (b == (byte)MM345ScriptAttribute.Facing); }
        protected virtual string AwardString(byte b) { return "Unknown"; }
        protected virtual string ItemString(byte b) { return "Unknown"; }
        protected virtual string SpellString(byte b) { return "Unknown"; }
        protected virtual List<MMMonster> Monsters { get { return null; } }

        protected MM345Reward RewardItem(byte[] bytes)
        {
            MM345Reward reward = new MM345Reward();
            if (bytes.Length < 4)
            {
                reward.OverrideValue = "Unknown";
                return reward;
            }

            reward.Attribute = MM345ScriptAttribute.Item;
            reward.Value = BitConverter.ToUInt32(bytes, 0);
            reward.Action = MM345Reward.Verb.ReceiveLose;
            MM45Item item = MM45Item.FromScriptBytes(bytes);
            reward.NoteFormat = "{0}Item \"{1}\"";
            reward.OverrideValue = item.ScriptString;
            return reward;
        }

        protected MM345Reward SingleReward(byte[] bytes, ref int index, bool bZeroIsVariable = false, bool bForPaying = false, bool bAbbrev = false, bool bIncludeReceiveString = true)
        {
            MM345Reward reward = new MM345Reward();

            if (bytes.Length - index < 2)
            {
                index++;
                reward.OverrideValue = "Unknown";
                return reward;
            }

            reward.IncludeReceiveString = bIncludeReceiveString;
            reward.Abbreviate = bAbbrev;
            reward.ForPaying = bForPaying;
            reward.Attribute = (MM345ScriptAttribute) bytes[index];
            reward.Value = bytes[index+1];
            reward.ZeroIsVariable = bZeroIsVariable;
            reward.Action = MM345Reward.Verb.ReceiveLose;

            byte b1 = bytes[index];
            byte b2 = bytes[index+1];
            MM345ScriptAttribute attr = (MM345ScriptAttribute) b1;

            reward.Attribute = attr;
            index += 2;     // Assume two bytes; add more on a per-attribute basis

            switch (attr)
            {
                case MM345ScriptAttribute.Nothing:
                    reward.Action = MM345Reward.Verb.None;
                    break;
                case MM345ScriptAttribute.Award:
                    reward.NoteFormat = "{0}Award \"{1}\"";
                    reward.OverrideValue = AwardString(b2);
                    break;
                case MM345ScriptAttribute.PartySkill:
                case MM345ScriptAttribute.Skill:
                    reward.NoteFormat = "{0}Skill \"{1}\"";
                    reward.OverrideValue = MMSecondarySkills.Name((MMSecondarySkillIndex)b2);
                    break;
                case MM345ScriptAttribute.Experience:
                    index += 3;
                    reward.Value = SafeUInt32(bytes, index - 4);
                    reward.OverrideValue = SafeUInt32(bytes, index - 4, bZeroIsVariable);
                    break;
                case MM345ScriptAttribute.Hireling:
                    reward.Singular = true;
                    break;
                case MM345ScriptAttribute.Condition:
                    if (b2 == 16)    // Special value for "cure all"
                        reward.OverrideAll = bAbbrev ? "CureAll" : "Cure of all conditions";
                    else
                    {
                        reward.NoteFormat = "{0}Condition \"{1}\"";
                        reward.OverrideValue = MMCondition.ConditionString((ConditionIndex)b2);
                    }
                    break;
                case MM345ScriptAttribute.Item:
                    reward.NoteFormat = "{0}Item \"{1}\"";
                    reward.OverrideValue = ItemString(b2);
                    break;
                case MM345ScriptAttribute.Spell:
                    reward.Action = MM345Reward.Verb.LearnForget;
                    reward.NoteFormat = "{0}Spell \"{1}\"";
                    reward.OverrideValue = SpellString(b2);
                    break;
                case MM345ScriptAttribute.PartyBit:
                case MM345ScriptAttribute.WorldBit:
                case MM345ScriptAttribute.CharBit:
                    reward.Action = MM345Reward.Verb.SetZero;
                    break;
                case MM345ScriptAttribute.QuestBit:
                    reward.Value += m_info.QuestBitOffset;
                    reward.Action = MM345Reward.Verb.SetZero;
                    break;
                case MM345ScriptAttribute.Gold:
                    reward.Action = MM345Reward.Verb.ReceivePay;
                    index += 3;
                    reward.Value = SafeUInt32(bytes, index - 4);
                    reward.OverrideValue = SafeUInt32(bytes, index - 4, bZeroIsVariable);
                    break;
                case MM345ScriptAttribute.Food:
                    reward.Action = MM345Reward.Verb.ReceivePay;
                    reward.Value = b2;
                    break;
                case MM345ScriptAttribute.RandomFood:
                    reward.Action = MM345Reward.Verb.ReceivePay;
                    index += 1;
                    reward.Value = SafeUInt16(bytes, index - 2);
                    reward.OverrideValue = SafeUInt16(bytes, index - 2, bZeroIsVariable);
                    reward.NoteFormat = "{0}1-{1} Food";
                    break;
                case MM345ScriptAttribute.RandomGold:
                    reward.Action = MM345Reward.Verb.ReceivePay;
                    index += 3;
                    reward.Value = SafeUInt32(bytes, index - 4);
                    reward.OverrideValue = SafeUInt32(bytes, index - 4, bZeroIsVariable);
                    reward.NoteFormat = "{0}1-{1} Gold";
                    break;
                case MM345ScriptAttribute.RandomGems:
                    reward.Action = MM345Reward.Verb.ReceivePay;
                    index += 1;
                    reward.Value = SafeUInt16(bytes, index - 2);
                    reward.OverrideValue = SafeUInt16(bytes, index - 2, bZeroIsVariable);
                    reward.NoteFormat = "{0}1-{1} Gems";
                    break;
                case MM345ScriptAttribute.Gems:
                    reward.Action = MM345Reward.Verb.ReceivePay;
                    index += 1;
                    reward.Value = SafeUInt16(bytes, index - 2);
                    reward.OverrideValue = SafeUInt16(bytes, index - 2, bZeroIsVariable);
                    break;
                case MM345ScriptAttribute.RandomItem:
                    if (bAbbrev)
                    {
                        reward.OverrideValue = String.Format("L{0}", b2);
                        reward.NoteFormat = "{0}Item({1})";
                    }
                    else
                    {
                        reward.OverrideValue = String.Format("Level {0}", b2);
                        reward.NoteFormat = "{0}1 {1} Item";
                    }
                    break;
                case MM345ScriptAttribute.Scroll:
                    reward.OverrideValue = SpellString(b2);
                    break;
                case MM345ScriptAttribute.Levitate:
                    reward.Action = MM345Reward.Verb.CastDispel;
                    reward.OverrideValue = "Levitation";
                    break;
                case MM345ScriptAttribute.Days:
                    reward.Action = MM345Reward.Verb.AdvanceGoBack;
                    break;
                case MM345ScriptAttribute.Alignment:
                    reward.Action = MM345Reward.Verb.SetDirectly;
                    reward.OverrideValue = MM3Character.AlignmentString((MM345AlignmentValue)b2);
                    break;
                case MM345ScriptAttribute.HPOverMaxHP:
                    if (b2 > 1)
                        goto default;
                    reward.OverrideAll = "Set HP to MaxHP";
                    break;
                case MM345ScriptAttribute.SPOverMaxSP:
                    if (b2 > 1)
                        goto default;
                    reward.OverrideAll = "Set SP to MaxSP";
                    break;
                default:
                    reward.Action = MM345Reward.Verb.AddSubtract;
                    if (String.IsNullOrWhiteSpace(Attribute(b1)))
                        reward.OverrideValue = String.Format("Unknown({0})", Global.ByteString(bytes.Skip(index - 2).Take(2).ToArray()));
                    break;
            }
            return reward;
        }

        public string MapString(int index)
        {
            if (m_info.Strings == null || index < 0 || index >= m_info.Strings.Count)
                return String.Format("MapString[{0}]", index);

            return m_info.Strings[index].Basic.Trim();
        }

        public static string Name(MM345ScriptCommand cmd)
        {
            switch (cmd)
            {
                case MM345ScriptCommand.DoNothing: return "DoNothing";
                case MM345ScriptCommand.Display: return "Display";
                case MM345ScriptCommand.DisplayPair: return "DisplayPair";
                case MM345ScriptCommand.DisplayMain: return "DisplayMain";
                case MM345ScriptCommand.DisplayWall: return "DisplayWall";
                case MM345ScriptCommand.DisplayBottom: return "DisplayBottom";
                case MM345ScriptCommand.DisplaySeat: return "DisplaySeat";
                case MM345ScriptCommand.OutdoorSign: return "Sign";
                case MM345ScriptCommand.SignWall: return "SignWall";
                case MM345ScriptCommand.PrintF: return "Print";
                case MM345ScriptCommand.NPC: return "NPC";
                case MM345ScriptCommand.JumpIfGreaterOrEqual: return "JumpIfGreaterOrEqual";
                case MM345ScriptCommand.MoveGraphic: return "MoveGraphic";
                case MM345ScriptCommand.SwapGraphics: return "SwapGraphics";
                case MM345ScriptCommand.TeleportEndScript: return "TeleportExit";
                case MM345ScriptCommand.Fall: return "Fall";
                case MM345ScriptCommand.Teleport: return "Teleport";
                case MM345ScriptCommand.Condition: return "Conditional";
                case MM345ScriptCommand.JumpIfLessOrEqual: return "JumpIfLessOrEqual";
                case MM345ScriptCommand.PlayAudioEffect: return "PlaySound";
                case MM345ScriptCommand.PlayAudio: return "PlayAudio";
                case MM345ScriptCommand.Random: return "Random";
                case MM345ScriptCommand.Trade: return "Trade";
                case MM345ScriptCommand.NoAction: return "NoAction";
                case MM345ScriptCommand.SetAffected: return "SetAffectedChar";
                case MM345ScriptCommand.Summon: return "Summon";
                case MM345ScriptCommand.ZeroAll:
                case MM345ScriptCommand.ZeroAll2: return "ZeroAllCommands";
                case MM345ScriptCommand.Business: return "TownBusiness";
                case MM345ScriptCommand.ExitScript: return "ExitScript";
                case MM345ScriptCommand.MapCommand: return "MapCommand";
                case MM345ScriptCommand.SetMapAttribute: return "SetMapAttribute";
                case MM345ScriptCommand.SubScript: return "SubScript";
                case MM345ScriptCommand.Return: return "Return";
                case MM345ScriptCommand.Receive: return "Receive";
                case MM345ScriptCommand.ReceiveItem: return "ReceiveItem";
                case MM345ScriptCommand.SetItemType: return "SetItemType";
                case MM345ScriptCommand.Question: return "Question";
                case MM345ScriptCommand.MultiAnswer: return "MultiAnswer";
                case MM345ScriptCommand.NumericChoice: return "ChooseNum";
                case MM345ScriptCommand.JumpRandom: return "JumpRandom";
                case MM345ScriptCommand.Damage: return "Damage";
                case MM345ScriptCommand.SetCommand: return "SetCommand";
                case MM345ScriptCommand.EndingSequence: return "EndGame";
                case MM345ScriptCommand.EndingSequence2: return "EndGame2";
                case MM345ScriptCommand.EndingSequence3: return "EndGame3";
                case MM345ScriptCommand.SetAttribute: return "SetAttribute";
                case MM345ScriptCommand.SetAttributeIfEqual: return "SetAttributeIfEqual";
                case MM345ScriptCommand.SelectGraphic: return "SelectGraphic";
                case MM345ScriptCommand.AlterHED: return "AlterHED";
                case MM345ScriptCommand.AddIfLessOrEqual: return "AddIfLessOrEqual";
                case MM345ScriptCommand.AddIfEqualAndLessThanOrEqual: return "ConditionalAdd";
                case MM345ScriptCommand.CopyProtection: return "CopyProtection";
                case MM345ScriptCommand.RandomChar: return "RandomChar";
                case MM345ScriptCommand.JumpIfMonster: return "JumpIfMonster";
                case MM345ScriptCommand.SetWorldSide: return "SetWorldSide";
                default: return String.Format("Unknown ({0})", (int) cmd);
            }
        }

        public string Description(MM345ScriptCommand cmd, MemoryBytes args, string strLinePrefix)
        {
            int iExpectedLength = MM345Script.NumArgs(cmd);
            int iArgsLength = (args == null ? 0 : args.Length);
            if (iArgsLength < iExpectedLength)
                return String.Format("{0} (invalid, expected {1} args, got {2})", Name(cmd), iExpectedLength, args == null ? "<null>" : args.Length.ToString());

            m_cmd = cmd;
            m_args = args;
            m_strLinePrefix = strLinePrefix;

            switch (cmd)
            {
                case MM345ScriptCommand.DoNothing: return "Do nothing";
                case MM345ScriptCommand.DisplayWall:
                case MM345ScriptCommand.DisplayBottom:
                case MM345ScriptCommand.DisplaySeat:
                case MM345ScriptCommand.SignWall:
                case MM345ScriptCommand.OutdoorSign:
                case MM345ScriptCommand.DisplayMain:
                case MM345ScriptCommand.DisplayPair:
                case MM345ScriptCommand.Display: return DisplayCmd();
                case MM345ScriptCommand.DisplayLong: return DisplayLong();
                case MM345ScriptCommand.PrintF: return PrintF().Description;
                case MM345ScriptCommand.NPC: return NPC();
                case MM345ScriptCommand.JumpIfGreaterOrEqual: return JumpGE();
                case MM345ScriptCommand.Fall:
                case MM345ScriptCommand.TeleportEndScript:
                case MM345ScriptCommand.Teleport: return Teleport().Description;
                case MM345ScriptCommand.Condition: return Conditional().Description;
                case MM345ScriptCommand.JumpIfLessOrEqual: return JumpLE();
                case MM345ScriptCommand.PlayAudioEffect:
                case MM345ScriptCommand.PlayAudio: return PlayAudio();
                case MM345ScriptCommand.Random: return Random();
                case MM345ScriptCommand.Trade: return TradeString(Trade());
                case MM345ScriptCommand.NoAction: return "No Action";
                case MM345ScriptCommand.SetAffected: return SetCharacter();
                case MM345ScriptCommand.Summon: return Summon().Description;
                case MM345ScriptCommand.ZeroAll:
                case MM345ScriptCommand.ZeroAll2: return "Set all Actions in this square to DoNothing";
                case MM345ScriptCommand.Business: return Business();
                case MM345ScriptCommand.ExitScript: return "Exit the script";
                case MM345ScriptCommand.MapCommand: return MapCommand().Description;
                case MM345ScriptCommand.SetMapAttribute: return SetMapAttribute().Description;
                case MM345ScriptCommand.SubScript: return SubScript();
                case MM345ScriptCommand.Return: return "Return from subscript";
                case MM345ScriptCommand.Receive: return "Receive: " + RewardString(args, false, false);
                case MM345ScriptCommand.ReceiveItem: return RewardItem(args).ScriptString;
                case MM345ScriptCommand.SetItemType: return SetItemType().Description;
                case MM345ScriptCommand.Question: return Question();
                case MM345ScriptCommand.MultiAnswer: return MultiAnswer();
                case MM345ScriptCommand.NumericChoice: return NumericChoice();
                case MM345ScriptCommand.JumpRandom: return JumpRandom();
                case MM345ScriptCommand.Damage: return Damage();
                case MM345ScriptCommand.SetCommand: return SetCommand();
                case MM345ScriptCommand.EndingSequence: return "Initiate End-Game Sequence";
                case MM345ScriptCommand.EndingSequence2: return "Initiate End-Game Sequence 2";
                case MM345ScriptCommand.EndingSequence3: return "Initiate End-Game Sequence 3";
                case MM345ScriptCommand.SetAttribute: return SetAttribute();
                case MM345ScriptCommand.SetAttributeIfEqual: return SetAttributeIfEqual();
                case MM345ScriptCommand.SelectGraphic: return SelectGraphic();
                case MM345ScriptCommand.MoveGraphic: return MoveGraphic();
                case MM345ScriptCommand.SwapGraphics: return SwapGraphics();
                case MM345ScriptCommand.AlterHED: return AlterHED();
                case MM345ScriptCommand.AddIfLessOrEqual: return AddLE().Description;
                case MM345ScriptCommand.AddIfEqualAndLessThanOrEqual: return AddEqualLE();
                case MM345ScriptCommand.RandomChar: return "SetCharacter: Random";
                case MM345ScriptCommand.CopyProtection: return "Invoke the Copy Protection Scheme";
                case MM345ScriptCommand.JumpIfMonster: return JumpIfMonster();
                case MM345ScriptCommand.SetWorldSide: return SetWorldSide();
                default: return String.Format("Unknown: {0}", Global.ByteString(args));
            }
        }

        public ScriptSummary Summary(MM345ScriptCommand cmd, MemoryBytes args, string strLinePrefix, bool bForNote)
        {
            int iExpectedLength = MM345Script.NumArgs(cmd);
            int iArgsLength = (args == null ? 0 : args.Length);
            if (iArgsLength < iExpectedLength)
                return new ScriptSummary("<Invalid>");

            m_cmd = cmd;
            m_args = args;
            m_strLinePrefix = strLinePrefix;
            m_bForNote = bForNote;
            m_bAbbrev = !bForNote;

            List<MM345Reward> rewards;

            if (bForNote)
            {
                switch (m_cmd)
                {
                    case MM345ScriptCommand.Business: return new ScriptSummary(BusinessString(m_args[0]), ScriptCommand.Business);
                    case MM345ScriptCommand.Question: return new ScriptSummary(Question(), ScriptCommand.Text);
                    case MM345ScriptCommand.MultiAnswer: return new ScriptSummary(MultiAnswer(), ScriptCommand.Input);
                    case MM345ScriptCommand.DisplayLong: return new ScriptSummary(DisplayLong(), ScriptCommand.Text);
                    case MM345ScriptCommand.NumericChoice: return new ScriptSummary(NumericChoice(), ScriptCommand.Input);
                    case MM345ScriptCommand.JumpRandom: return new ScriptSummary(JumpRandom(), ScriptCommand.Text);
                    case MM345ScriptCommand.Display:
                    case MM345ScriptCommand.DisplayMain:
                    case MM345ScriptCommand.DisplayBottom:
                        return new ScriptSummary(MapString(m_args[0]), ScriptCommand.Text);
                    case MM345ScriptCommand.DisplayPair:
                        return new ScriptSummary(String.Format("{0} {1}", MapString(m_args[0]), MapString(m_args[1])), ScriptCommand.Text);
                    case MM345ScriptCommand.DisplayWall:
                    case MM345ScriptCommand.OutdoorSign:
                    case MM345ScriptCommand.DisplaySeat:
                    case MM345ScriptCommand.SignWall:
                        if (m_facing == DirectionFlags.All)
                            return new ScriptSummary(MapString(m_args[0]), ScriptCommand.Sign);
                        return new ScriptSummary(String.Format("Sign {0}: {1}", Global.DirectionString(m_facing), MapString(m_args[0])), ScriptCommand.Sign);
                    case MM345ScriptCommand.PrintF: return PrintF();
                    case MM345ScriptCommand.NPC: return new ScriptSummary(String.Format("{0}: {1}",
                        MapString(m_args[0]), MapString(m_args[1])), ScriptCommand.NPC, Global.CreateSymbol(MapString(m_args[0])));
                    case MM345ScriptCommand.Trade:
                        rewards = Trade();
                        return new ScriptSummary(rewards, TradeString(rewards));
                    case MM345ScriptCommand.Summon: return Summon();
                    case MM345ScriptCommand.Receive:
                        rewards = RewardArray(m_args, m_bAbbrev);
                        return new ScriptSummary(rewards, RewardString(rewards));
                    case MM345ScriptCommand.ReceiveItem:
                        rewards = new List<MM345Reward>(1);
                        rewards.Add(RewardItem(m_args));
                        return new ScriptSummary(rewards, RewardString(rewards));
                    case MM345ScriptCommand.Damage: return new ScriptSummary(Damage(), ScriptCommand.Damage);
                    case MM345ScriptCommand.SetItemType: return SetItemType();
                    case MM345ScriptCommand.SetAttribute: return new ScriptSummary(SetAttribute());
                    case MM345ScriptCommand.AddIfLessOrEqual: return AddLE();
                    case MM345ScriptCommand.AddIfEqualAndLessThanOrEqual: return new ScriptSummary(AddEqualLE());
                    case MM345ScriptCommand.TeleportEndScript:
                    case MM345ScriptCommand.Fall:
                    case MM345ScriptCommand.Teleport: return Teleport();
                    case MM345ScriptCommand.SetAttributeIfEqual: return new ScriptSummary(SetAttributeIfEqual());
                    case MM345ScriptCommand.EndingSequence: return new ScriptSummary("End-Game Sequence");
                    case MM345ScriptCommand.EndingSequence2: return new ScriptSummary("End-Game Sequence 2");
                    case MM345ScriptCommand.EndingSequence3: return new ScriptSummary("End-Game Sequence 3");
                    case MM345ScriptCommand.Random: return new ScriptSummary(Random());
                    case MM345ScriptCommand.ExitScript: return new ScriptSummary("Otherwise:", ScriptCommand.ExitScript);        // Typically used before a jump target
                    case MM345ScriptCommand.MapCommand: return MapCommand();
                    case MM345ScriptCommand.SetMapAttribute: return SetMapAttribute();
                    case MM345ScriptCommand.JumpIfGreaterOrEqual: return new ScriptSummary(JumpGE(), ScriptCommand.Jump);
                    case MM345ScriptCommand.RandomChar: return new ScriptSummary("Random character:");
                    case MM345ScriptCommand.Condition: return Conditional();
                    case MM345ScriptCommand.JumpIfLessOrEqual: return new ScriptSummary(JumpLE(), ScriptCommand.Jump);
                    case MM345ScriptCommand.JumpIfMonster: return new ScriptSummary(JumpIfMonster(), ScriptCommand.Jump);
                    case MM345ScriptCommand.SetCommand: return new ScriptSummary(String.Empty, ScriptCommand.SetCommand);
                    case MM345ScriptCommand.SetWorldSide: return new ScriptSummary(SetWorldSide(), ScriptCommand.SetSide);
                    default: return new ScriptSummary(String.Empty);
                }
            }
            else
            {
                switch (m_cmd)
                {
                    case MM345ScriptCommand.Business: return new ScriptSummary(BusinessString(m_args[0]), ScriptCommand.Business);
                    case MM345ScriptCommand.Question: return new ScriptSummary(MapString(m_args[3]), ScriptCommand.Text);
                    case MM345ScriptCommand.MultiAnswer: return new ScriptSummary(String.Format("Answer {0}/{1}", MapString(m_args[1]), MapString(m_args[3])), ScriptCommand.Input);
                    case MM345ScriptCommand.NumericChoice: return new ScriptSummary(String.Format("Choose 1-{0}", m_args[0]), ScriptCommand.Input);
                    case MM345ScriptCommand.JumpRandom: return new ScriptSummary(String.Format("JumpRnd[{0}]", m_args[0]), ScriptCommand.Text);
                    case MM345ScriptCommand.DisplayWall:
                    case MM345ScriptCommand.DisplayMain:
                    case MM345ScriptCommand.DisplayBottom:
                    case MM345ScriptCommand.OutdoorSign:
                    case MM345ScriptCommand.Display:
                    case MM345ScriptCommand.DisplaySeat:
                    case MM345ScriptCommand.SignWall: return new ScriptSummary(MapString(m_args[0]), ScriptCommand.Text);
                    case MM345ScriptCommand.DisplayLong: return new ScriptSummary(String.Format("TextFile({0})", m_args[0]));
                    case MM345ScriptCommand.DisplayPair:
                        return new ScriptSummary(String.Format("{0}; {1}", MapString(m_args[0]), MapString(m_args[1])), ScriptCommand.Text);
                    case MM345ScriptCommand.PrintF: return PrintF();
                    case MM345ScriptCommand.NPC: return new ScriptSummary(String.Format("{0}; {1}", MapString(m_args[0]), MapString(m_args[1])), ScriptCommand.NPC);
                    case MM345ScriptCommand.PlayAudioEffect: return new ScriptSummary(String.Format("SFX {0}", m_args[0]));
                    case MM345ScriptCommand.PlayAudio: return new ScriptSummary(String.Format("Audio {0}", m_args[0]));
                    case MM345ScriptCommand.Trade:
                        rewards = Trade();
                        return new ScriptSummary(rewards, TradeString(rewards));
                    case MM345ScriptCommand.Summon: return new ScriptSummary(String.Format("Summon"), ScriptCommand.Summon);
                    case MM345ScriptCommand.Receive:
                        rewards = RewardArray(m_args, m_bAbbrev);
                        return new ScriptSummary(rewards, RewardString(rewards));
                    case MM345ScriptCommand.ReceiveItem:
                        rewards = new List<MM345Reward>(1);
                        rewards.Add(RewardItem(m_args));
                        return new ScriptSummary(rewards, RewardString(rewards));
                    case MM345ScriptCommand.Damage: return new ScriptSummary(Damage(), ScriptCommand.Damage);
                    case MM345ScriptCommand.SetAttribute:
                        return new ScriptSummary(IsFacing(m_args[0]) ?
                            String.Format("Face {0}", FacingString(m_args[1], m_bAbbrev)) :
                            SetAttributeToString(m_args[0], ValueForAttribute(m_args[0], m_args.GetRange(1)), m_bAbbrev));
                    case MM345ScriptCommand.AddIfLessOrEqual: return AddLE();
                    case MM345ScriptCommand.AddIfEqualAndLessThanOrEqual:
                        return new ScriptSummary(String.Format("{0}+{1}", ShortAttribute(m_args[4]), m_args.GetRange(4)));
                    case MM345ScriptCommand.TeleportEndScript:
                    case MM345ScriptCommand.Teleport: return new ScriptSummary(Global.AllNull(m_args) ? "Portal" :
                        String.Format("Teleport {0}:{1},{2}", m_args[0], m_args[1], m_args[2]), ScriptCommand.Teleport);
                    case MM345ScriptCommand.Fall:
                        if (m_args.Length < 4)
                            return new ScriptSummary(String.Format("Fall {0}:{1},{2}", m_args[0], m_args[1], m_args[2]), ScriptCommand.Teleport);
                        return new ScriptSummary(String.Format("Fall {0}:{1},{2} ({3} damage)", m_args[0], m_args[1], m_args[2], m_args[3]), ScriptCommand.Teleport);
                    case MM345ScriptCommand.SetAttributeIfEqual: return new ScriptSummary(SetAttributeIfEqual());
                    case MM345ScriptCommand.EndingSequence: return new ScriptSummary("End-Game Sequence");
                    case MM345ScriptCommand.EndingSequence2: return new ScriptSummary("End-Game Sequence 2");
                    case MM345ScriptCommand.EndingSequence3: return new ScriptSummary("End-Game Sequence 3");
                    case MM345ScriptCommand.SubScript: return new ScriptSummary(String.Format("Subscript({0},{1},{2})", m_args[0], m_args[1], m_args[2]));
                    case MM345ScriptCommand.ZeroAll:
                    case MM345ScriptCommand.ZeroAll2: return new ScriptSummary("ZeroAll");
                    case MM345ScriptCommand.Random: return new ScriptSummary("Random");
                    case MM345ScriptCommand.MapCommand: return new ScriptSummary("AlterMap", ScriptCommand.AlterMap);
                    case MM345ScriptCommand.SetMapAttribute: return new ScriptSummary("SetMapAttr", ScriptCommand.SetMapAttribute);
                    case MM345ScriptCommand.CopyProtection: return new ScriptSummary("CopyProtection");
                    case MM345ScriptCommand.ExitScript: return new ScriptSummary("Exit", ScriptCommand.ExitScript);
                    case MM345ScriptCommand.RandomChar: return new ScriptSummary("RandChar");
                    case MM345ScriptCommand.SetCommand: return new ScriptSummary(String.Empty, ScriptCommand.SetCommand);
                    case MM345ScriptCommand.Condition: return Conditional();
                    case MM345ScriptCommand.SetWorldSide: return new ScriptSummary(String.Format("SetSide({0})", m_args[0]), ScriptCommand.SetSide);

                    case MM345ScriptCommand.JumpIfGreaterOrEqual:
                    case MM345ScriptCommand.JumpIfLessOrEqual:
                    case MM345ScriptCommand.JumpIfMonster:
                        return new ScriptSummary(String.Empty, ScriptCommand.Jump);
                    case MM345ScriptCommand.NoAction:
                    case MM345ScriptCommand.SetAffected:
                    case MM345ScriptCommand.Return:
                    case MM345ScriptCommand.SelectGraphic:
                    case MM345ScriptCommand.SwapGraphics:
                    case MM345ScriptCommand.MoveGraphic:
                    case MM345ScriptCommand.AlterHED:
                    case MM345ScriptCommand.SetItemType:
                        // Not worth a summary string
                        return new ScriptSummary(String.Empty);

                    default: return new ScriptSummary(String.Empty);
                }
            }
        }

        private string AlterHED()
        {
            return String.Format("Set HED bytes at this location to 0x{0:X4}", BitConverter.ToUInt16(m_args, 0));
        }

        private string DisplayCmd()
        {
            if (m_cmd == MM345ScriptCommand.DisplayPair)
                return String.Format("{0}: {1}; {2}", Name(m_cmd), MapString(m_args[0]), MapString(m_args[1]));
            return String.Format("{0}: {1}", Name(m_cmd), MapString(m_args[0]));
        }

        private string DisplayLong()
        {
            if (m_bForNote)
                return String.Format("(Displays text from file #{0})", m_args[0]);
            return String.Format("Display Text from File: {0}", m_args[0]);
        }

        protected virtual ScriptSummary PrintF()
        {
            if (m_bAbbrev)
                return new ScriptSummary(MapString(m_args[0]).Replace("%s", "<Name>"), ScriptCommand.Text);
            return new ScriptSummary(String.Format("Print: {0}", MapString(m_args[0]).Replace("%s", "<CharacterName>")), ScriptCommand.Text);
        }

        private string NPC()
        {
            string strAsk = String.Format("NPC {0} ({1}): {2}", m_args[2], MapString(m_args[0]), MapString(m_args[1]));
            if (m_args.Length > 4)
            {
                if (m_args[3] != 1)
                    return String.Format("{0}, Ask(Yes/No), if (Yes) goto {1}{2}", strAsk, m_strLinePrefix, m_args[4]);
                else
                    return String.Format("{0}, Pause, goto {1}{2}", strAsk, m_strLinePrefix, m_args[4]);
            }
            return strAsk;
        }

        private byte LastArg
        {
            get
            {
                if (m_args.Length < 1)
                    return 0;
                return m_args[m_args.Length - 1];
            }
        }

        private string JumpGE()
        {
            // Special always-true case used for "goto" (if character's spell points are >= 0)
            if (m_args[0] == 9 && m_args[1] == 0)
                return m_bForNote ? "If you have already <verb>:" :  String.Format("Goto {0}{1}", m_strLinePrefix, m_args[2]);

            if (m_bForNote) // Typically the "jump" skips the command after this one
                return String.Format("If {0} < {1}:", Attribute(m_args[0], true), ValueForAttribute(m_args[0], m_args.GetRange(1)));

            return String.Format("For (Character), if ({0}) >= {1} goto {2}{3}",
                Attribute(m_args[0], true), ValueForAttribute(m_args[0], m_args.GetRange(1)), m_strLinePrefix, LastArg);
        }

        private string JumpLE()
        {
            if (m_bForNote) // Typically the "jump" skips the command after this one
                return String.Format("If {0} > {1}:", Attribute(m_args[0], true), ValueForAttribute(m_args[0], m_args.GetRange(1)));

            return String.Format("For (Character), if ({0}) <= {1} goto {2}{3}",
                Attribute(m_args[0], true), ValueForAttribute(m_args[0], m_args.GetRange(1)), m_strLinePrefix, LastArg);
        }

        private string JumpIfMonster()
        {
            if (m_bForNote)
            {
                switch (m_args[0])
                {
                    case 255: return "If any monsters are alive: ";
                    default: return String.Format("If Monster[{0}] ({1}) is alive: ", m_args[0], MonsterName(m_args[0]));
                }
            }

            switch (m_args[0])
            {
                case 255: return String.Format("If no monsters are alive on this map, goto {0}{1}", m_strLinePrefix, m_args[1]);
                default: return String.Format("If Monster[{0}] ({1}) is dead, goto {2}{3}", m_args[0], MonsterName(m_args[0]), m_strLinePrefix, m_args[1]);
            }
        }

        protected virtual ScriptSummary AddLE()
        {
            byte attr1 = m_args[0];
            byte attr2 = m_args[m_args.Length - 2];

            if (m_bAbbrev)
            {
                if (attr1 == attr2)
                    return new ScriptSummary(String.Format("{0}+{1} if <={2}",
                        ShortAttribute(attr1), LastArg, ValueForAttribute(m_args[0], m_args.GetRange(1))));
                return new ScriptSummary(String.Format("{0}<={1}? {2}+{3}",
                    ShortAttribute(attr1), ValueForAttribute(m_args[0], m_args.GetRange(1)), ShortAttribute(attr2), LastArg));
            }

            return new ScriptSummary(String.Format("For (Character), if ({0}) <= {1}, add {2} to ({3})",
                Attribute(attr1, true), ValueForAttribute(m_args[0], m_args.GetRange(1)), LastArg, Attribute(attr2)));
        }

        private string AddEqualLE()
        {
            return String.Format("For (Character), if {0} and ({1}) <= {2}, add {3} to ({4})",
                AttributeIsEqualString(m_args[0], m_args[1]),
                Attribute(m_args[2], true),
                ValueForAttribute(m_args[2], m_args.GetRange(3)),
                LastArg,
                Attribute(m_args[m_args.Length - 2]));
        }

        private string SetWorldSide()
        {
            MMScriptInfo.Flip side = m_args[0] == 0 ? MMScriptInfo.Flip.ToCloudside : MMScriptInfo.Flip.ToDarkside;
            if (m_info.WorldChanges.ContainsKey(m_location))
                m_info.WorldChanges[m_location] = side;
            else
                m_info.WorldChanges.Add(m_location, side);

            if (m_bForNote)
                return String.Format("Switch to {0}", m_args[0] == 0 ? "Cloudside" : "Darkside");
            return String.Format("Change World Side: {0}", m_args[0] == 0 ? "Cloudside" : "Darkside");
        }

        private ScriptSummary Teleport()
        {
            bool bFall = (m_cmd == MM345ScriptCommand.Fall);
            int iDamage = (bFall && m_args.Length > 3 ? m_args[3] : 7);
            string strDamage = bFall && iDamage > 0 ? String.Format(", -{0} Damage", iDamage) : "";

            string strTeleport = bFall ? "Fall" : "Teleport";

            int iMap = m_args[0];
            if (m_info.Map.Map > 255)
                iMap += 256; // Can only teleport to maps on the same side unless there is a world change command

            if (m_info.WorldChanges.ContainsKey(m_location))
            switch (m_info.WorldChanges[m_location])
            {
                case MMScriptInfo.Flip.ToCloudside:
                    iMap = iMap % 256;
                    break;
                case MMScriptInfo.Flip.ToDarkside:
                    iMap = iMap | 256;
                    break;
                default:
                    break;
            }

            if (m_args[0] == 0 && m_args[1] == 0 && m_args[2] == 0)
                return new ScriptSummary(strTeleport + " to (portal destination)", ScriptCommand.Portal);
            ScriptSummary ss;
            if (m_bForNote)
            {
                if (iMap == m_info.Map.Map)
                    ss = new ScriptSummary(String.Format("{0} to ({1},{2}{3})", strTeleport, m_args[1], m_args[2], strDamage), ScriptCommand.Teleport);
                else
                    ss = new ScriptSummary(String.Format("{0}{{map:{1}}} ({2},{3}){4}", bFall ? "Fall to " : "", GetMapTitlePair(iMap).Title, m_args[1], m_args[2], strDamage), ScriptCommand.Teleport);
            }
            else
                ss = new ScriptSummary(String.Format("{0} to map \"{1}\" ({2},{3}){4}{5}", strTeleport, GetMapTitlePair(iMap), m_args[1], m_args[2], strDamage,
                    m_cmd == MM345ScriptCommand.TeleportEndScript ? ", Exit Script" : ""), ScriptCommand.Teleport);
            ss.Target = new Point(m_args[1], m_args[2]);

            return ss;
        }

        private string SetAttribute()
        {
            if (m_bForNote)
                return SetAttributeToString(m_args[0], ValueForAttribute(m_args[0], m_args.GetRange(1)));
            return String.Format("For (Character), {0}", SetAttributeToString(m_args[0], ValueForAttribute(m_args[0], m_args.GetRange(1))));
        }

        private string PlayAudio()
        {
            switch (m_cmd)
            {
                case MM345ScriptCommand.PlayAudioEffect: return String.Format("Play Sound Effect: {0}", m_args[0]);
                default: return String.Format("Play Audio: {0}", m_args[0]);
            }
        }

        private string SetAttributeIfEqual()
        {
            if (m_bAbbrev)
                return SetAttributeToString(m_args[2], ValueForAttribute(m_args[2], m_args.GetRange(3)), m_bAbbrev);
            return String.Format("For (Character), if {0}, {1}",
                AttributeIsEqualString(m_args[0], ValueForAttribute(m_args[0], m_args.GetRange(1))),
                SetAttributeToString(m_args[2], ValueForAttribute(m_args[2], m_args.GetRange(3))));
        }

        private ScriptSummary SetMapAttribute()
        {
            ScriptSummary ss;
            if (m_bForNote)
                ss = new ScriptSummary(String.Format("Set the map attributes at ({0},{1}) to 0x{2:X2}", m_args[0], m_args[1], m_args[2]), ScriptCommand.SetMapAttribute);
            else
                ss = new ScriptSummary(String.Format("Set Map Attribute: ({0},{1}) = 0x{2:X2}", m_args[0], m_args[1], m_args[2]), ScriptCommand.SetMapAttribute);
            ss.Value = m_args[2];
            ss.Target = new Point(m_args[0], m_args[1]);
            return ss;
        }

        private ScriptSummary MapCommand()
        {
            ScriptSummary ss = new ScriptSummary(String.Empty);
            ss.Target = new Point(m_args[0], m_args[1]);
            ss.Direction = FacingDirection(m_args[2]);
            ss.Value = m_args[3];
            ss.Command = ScriptCommand.AlterMap;

            if (m_bForNote)
            {
                switch ((MM345MapCommand) m_args[3])
                {
                    case MM345MapCommand.OpenDoor:
                        return ss.NewDescription(String.Format("Open the {0} door at ({1},{2})", FacingString(m_args[2]), m_args[0], m_args[1]));
                    case MM345MapCommand.RemoveWall0:
                    case MM345MapCommand.RemoveWall7:
                        return ss.NewDescription(String.Format("Remove the {0} wall at ({1},{2})", FacingString(m_args[2]), m_args[0], m_args[1]));
                    case MM345MapCommand.SurroundMountains:
                        return ss.NewDescription(String.Format("Surround the square at ({0},{1}) with mountains", m_args[0], m_args[1]));
                    case MM345MapCommand.CreateWall:
                        return ss.NewDescription(String.Format("Add the {0} wall at ({1},{2})", FacingString(m_args[2]), m_args[0], m_args[1]));
                    case MM345MapCommand.CloseDoor:
                        return ss.NewDescription(String.Format("Close the {0} door at ({1},{2})", FacingString(m_args[2]), m_args[0], m_args[1]));
                    case MM345MapCommand.LockDoor:
                        return ss.NewDescription(String.Format("Lock the {0} door at ({1},{2})", FacingString(m_args[2]), m_args[0], m_args[1]));
                    case MM345MapCommand.AddTorch:
                        return ss.NewDescription(String.Format("Add the {0} torch at ({1},{2})", FacingString(m_args[2]), m_args[0], m_args[1]));
                    default: return ss.NewDescription(String.Format("Unknown MapCommand({0})", Global.ByteString(m_args)));
                }
            }

            return ss.NewDescription(String.Format("AlterMap({0},{1},{2}):{3}", m_args[0], m_args[1], FacingString(m_args[2]), m_args[3]));
        }

        protected ScriptSummary Conditional()
        {
            if (m_bForNote)
                return NoteConditional();

            ScriptSummary ss = new ScriptSummary("Unknown");
            ss.Attribute = (MM345ScriptAttribute)m_args[0];
            ss.Command = ScriptCommand.Conditional;
            ss.Value = m_args[1];

            if (m_bAbbrev)
            {
                switch ((MM345ScriptAttribute)m_args[0])
                {
                    // Some things aren't worth a summary
                    case MM345ScriptAttribute.AskYesNo:
                        return ss.NewDescription(String.Empty);

                    case MM345ScriptAttribute.PartyBit: return ss.NewDescription(String.Format("PBit{0}?", m_args[1]));
                    case MM345ScriptAttribute.CharBit: return ss.NewDescription(String.Format("CBit{0}?", m_args[1]));
                    case MM345ScriptAttribute.WorldBit: return ss.NewDescription(String.Format("WBit{0}?", m_args[1]));
                    case MM345ScriptAttribute.QuestBit: return ss.NewDescription(String.Format("QBit{0}?", m_args[1] + m_info.QuestBitOffset));

                    default:
                        break;
                }
                return ss.NewDescription(String.Format("{0}?", ShortAttribute(m_args)));
            }

            string strUnknown = String.Format("if (Unknown) has Unknown({0}) = {1} goto {2}", m_args[0], m_args[1], m_args[2]);

            switch ((MM345ScriptAttribute)m_args[0])
            {
                case MM345ScriptAttribute.Award: return ss.NewDescription(String.Format("If (Character) has Award({0}) goto {1}{2}",
                    MM45Character.AwardString((MM45AwardIndex)m_args[1]), m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.PartyBit: return ss.NewDescription(String.Format("If PartyBit({0}) is 1 goto {1}{2}",
                    m_args[1], m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.CharBit: return ss.NewDescription(String.Format("If CharBit({0}) is 1 goto {1}{2}",
                    m_args[1], m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.WorldBit: return ss.NewDescription(String.Format("If WorldBit({0}) is 1 goto {1}{2}",
                    m_args[1], m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.QuestBit: return ss.NewDescription(String.Format("If QuestBit({0}) is 1 goto {1}{2}",
                    m_args[1] + m_info.QuestBitOffset, m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.Item: return ss.NewDescription(String.Format("If (Character) has Item({0}) goto {1}{2}",
                    ItemString(m_args[1]), m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.Race: return ss.NewDescription(String.Format("If (Character) is {0} goto {1}{2}",
                    Global.An(MM45Character.RaceString(MM345BaseCharacter.MM45RaceFromByte(m_args[1]))), m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.Class: return ss.NewDescription(String.Format("If (Character) is {0} goto {1}{2}",
                    Global.An(MM45Character.ClassString(MM345BaseCharacter.ClassFromByte(m_args[1]))), m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.Skill: return ss.NewDescription(String.Format("If (Character) has Skill({0}) goto {1}{2}",
                    MMSecondarySkills.Name((MMSecondarySkillIndex)m_args[1]), m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.PartySkill: return ss.NewDescription(String.Format("If (Entire Party) has Skill({0}) goto {1}{2}",
                    MMSecondarySkills.Name((MMSecondarySkillIndex)m_args[1]), m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.Facing: return ss.NewDescription(String.Format("If (Party Direction) is ({0}) goto {1}{2}", FacingString(m_args[1]), m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.Levitate:
                case MM345ScriptAttribute.WalkOnWater:
                case MM345ScriptAttribute.Light:
                case MM345ScriptAttribute.WizardEye:
                    return ss.NewDescription(String.Format("If {0} is {1} goto {2}{3}",
                        Attribute(m_args[0]),
                        m_args[1] == 0 ? "not active" : "active", m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.Condition: return ss.NewDescription(String.Format("For (Character), if Condition({0}) > 0 goto {1}{2}",
                    MMCondition.ConditionString((ConditionIndex)m_args[1]), m_strLinePrefix, m_args[2]));
                case MM345ScriptAttribute.AskYesNo:
                    if (m_args[1] == 0)
                        return ss.NewDescription(String.Format("Ask(Yes/No), if (Yes) goto {0}{1}", m_strLinePrefix, m_args[2]));
                    return ss.NewDescription(String.Format("Pause, goto {0}{1}", m_strLinePrefix, m_args[2]));
                default:
                    string strAttribute = Attribute(m_args[0]);
                    if (String.IsNullOrWhiteSpace(strAttribute))
                        return ss.NewDescription(strUnknown);
                    return ss.NewDescription(String.Format("For (Character), if {0}, goto {1}{2}", Attribute(m_args, true), m_strLinePrefix, m_args[2]));
            }
        }

        protected ScriptSummary NoteConditional()
        {
            ScriptSummary ss = new ScriptSummary("Unknown");
            ss.Attribute = (MM345ScriptAttribute)m_args[0];
            ss.Command = ScriptCommand.Conditional;
            ss.Value = m_args[1];

            string strUnknown = String.Empty;

            switch ((MM345ScriptAttribute)m_args[0])
            {
                case MM345ScriptAttribute.Award: 
                    ss.ValueOverride = String.Format("\"{0}\"", MM45Character.AwardString((MM45AwardIndex)m_args[1]));
                    ss.ValuePhraseOverride = String.Format("the {0} award", ss.ValueOverride);
                    return ss.NewDescription(String.Format("If you do not have {0}:", ss.ValuePhraseOverride));
                case MM345ScriptAttribute.PartyBit: return ss.NewDescription(String.Format("[If !PartyBit{0}]", m_args[1]));
                case MM345ScriptAttribute.CharBit: return ss.NewDescription(String.Format("[If !CharBit{0}]", m_args[1]));
                case MM345ScriptAttribute.WorldBit: return ss.NewDescription(String.Format("[If !WorldBit{0}]", m_args[1]));
                case MM345ScriptAttribute.QuestBit: return ss.NewDescription(String.Format("[If !QuestBit{0}]", m_args[1] + m_info.QuestBitOffset));
                case MM345ScriptAttribute.Item:
                    ss.ValueOverride = String.Format("\"{0}\"", ItemString(m_args[1]));
                    return ss.NewDescription(String.Format("If you do not have {0}:", ss.ValueOverride));
                case MM345ScriptAttribute.Race:
                    ss.ValueOverride = Global.An(MM45Character.RaceString(MM345BaseCharacter.MM45RaceFromByte(m_args[1])));
                    return ss.NewDescription(String.Format("If you are not {0}:", ss.ValueOverride));
                case MM345ScriptAttribute.Class:
                    ss.ValueOverride = Global.An(MM45Character.ClassString(MM345BaseCharacter.ClassFromByte(m_args[1])));
                    return ss.NewDescription(String.Format("If you are not {0}:", ss.ValueOverride));
                case MM345ScriptAttribute.Skill:
                    ss.ValueOverride = String.Format("\"{0}\"", MMSecondarySkills.Name((MMSecondarySkillIndex) m_args[1]));
                    ss.ValuePhraseOverride = String.Format("the {0} skill", ss.ValueOverride);
                    return ss.NewDescription(String.Format("If you do not have {0}:", ss.ValuePhraseOverride));
                case MM345ScriptAttribute.PartySkill:
                    ss.ValueOverride = String.Format("\"{0}\"", MMSecondarySkills.Name((MMSecondarySkillIndex) m_args[1]));
                    ss.ValuePhraseOverride = String.Format("the {0} skill", ss.ValueOverride);
                    return ss.NewDescription(String.Format("If any character does not have {0}:", ss.ValuePhraseOverride));
                case MM345ScriptAttribute.Facing:
                    ss.ValueOverride = FacingString(m_args[1]);
                    return ss.NewDescription(String.Format("If party is facing {0}:", ss.ValueString));
                case MM345ScriptAttribute.Levitate: return ss.NewDescription(String.Format("If Levitate is {0}active:", m_args[1] == 0 ? "" : "not "));
                case MM345ScriptAttribute.WalkOnWater: return ss.NewDescription(String.Format("If Walk on Water is {0}active:", m_args[1] == 0 ? "" : "not "));
                case MM345ScriptAttribute.Light: return ss.NewDescription(String.Format("If Light is {0}active:", m_args[1] == 0 ? "" : "not "));
                case MM345ScriptAttribute.WizardEye: return ss.NewDescription(String.Format("If Wizard Eye is {0}active:", m_args[1] == 0 ? "" : "not "));
                case MM345ScriptAttribute.Condition: return ss.NewDescription(String.Format("If you are not \"{0}\":",
                    MMCondition.ConditionString((ConditionIndex)m_args[1])));
                case MM345ScriptAttribute.AskYesNo:
                    if (m_args[1] == 0)
                        return ss.NewDescription(("N:"));
                    return ss.NewDescription(String.Empty);
                default:
                    return ss.NewDescription(String.Format("If {0}:", NoteAttribute(m_args)));
            }
        }

        public string SelectGraphic()
        {
            return String.Format("Select Graphic[{0}] at ({1},{2})", m_args[0], m_args[1], m_args[2]);
        }

        public string MoveGraphic()
        {
            return String.Format("Move Graphic[{0}] to ({1},{2})", m_args[0], m_args[1], m_args[2]);
        }

        public string SwapGraphics()
        {
            return String.Format("Swap Graphic[{0}] with Graphic[{1}]", m_args[0], m_args[1]);
        }

        protected static string SafeUInt32(byte[] bytes, int index, bool bZeroIsVar = false)
        {
            if (bytes.Length - index < 4)
                return "invalid bytes";
            return CheckZero(BitConverter.ToUInt32(bytes, index), bZeroIsVar);
        }

        protected UInt32 SafeUInt32(byte[] bytes, int index)
        {
            if (bytes.Length - index < 4)
                return 0;
            return BitConverter.ToUInt32(bytes, index);
        }

        protected UInt16 SafeUInt16(byte[] bytes, int index)
        {
            if (bytes.Length - index < 2)
                return 0;
            return BitConverter.ToUInt16(bytes, index);
        }

        protected static string SafeUInt16(byte[] bytes, int index, bool bZeroIsVar)
        {
            if (bytes.Length - index < 2)
                return "invalid bytes";
            return CheckZero(BitConverter.ToUInt16(bytes, index), bZeroIsVar);
        }

        private static string CheckZero(long i, bool bVar)
        {
            if (i == 0 && bVar)
                return "X";
            return String.Format("{0}", i);
        }

        public List<MM345Reward> RewardArray(byte[] bytes, bool bAbbrev = false, bool bIncludeReceiveString = true)
        {
            List<MM345Reward> list = new List<MM345Reward>();
            int iCurrent = 0;
            while (iCurrent < bytes.Length)
            {
                MM345Reward reward = SingleReward(bytes, ref iCurrent, false, false, bAbbrev, bIncludeReceiveString);
                if (!reward.IsEmpty)
                    list.Add(reward);
            }

            return list;
        }

        public string RewardString(byte[] bytes, bool bAbbrev = false, bool bIncludeReceiveString = true)
        {
            return RewardString(RewardArray(bytes, bAbbrev, bIncludeReceiveString));
        }

        public string RewardString(List<MM345Reward> rewards)
        {
            StringBuilder sb = new StringBuilder();

            foreach (MM345Reward reward in rewards)
                sb.AppendFormat("{0}, ", m_bForNote ? reward.NoteString : reward.ScriptString);

            return Global.Trim(sb).ToString();
        }

        public static long ValueForAttribute(byte attr, byte[] bytesValue)
        {
            if (bytesValue == null || bytesValue.Length < 1)
                return 0;

            switch ((MM345ScriptAttribute)attr)
            {
                case MM345ScriptAttribute.Gold:
                case MM345ScriptAttribute.Experience:
                    if (bytesValue.Length < 4)
                        return 0;
                    return BitConverter.ToUInt32(bytesValue, 0);
                case MM345ScriptAttribute.Gems:
                case MM345ScriptAttribute.Minutes:
                case MM345ScriptAttribute.GameYear:
                    if (bytesValue.Length < 2)
                        return 0;
                    return BitConverter.ToUInt16(bytesValue, 0);
                default: return bytesValue[0];
            }
        }

        public static string Attribute(byte attr, long compare, bool bAbbrev = false, bool bUnknownString = false, bool bReverseOperation = false, bool bParens = true)
        {
            MathOperation op;
            string strAttribute = Attribute(attr, out op, bAbbrev, bUnknownString);
            if (bReverseOperation)
                op = Global.ReverseOperation(op);
            string strOpen = bParens ? "(" : "";
            string strClose = bParens ? ")" : "";

            switch (op)
            {
                case MathOperation.Equal: return String.Format("{0}{1}{2} = {3}", strOpen, strAttribute, strClose, compare);
                case MathOperation.NotEqual: return String.Format("{0}{1}{2} != {3}", strOpen, strAttribute, strClose, compare);
                case MathOperation.IsTrue: return String.Format("{0}{1}{2}{3}", (compare == 0 ? "not " : ""), strOpen, strAttribute, strClose);
                case MathOperation.IsFalse: return String.Format("{0}{1}{2}{3}", (compare != 0 ? "not " : ""), strOpen, strAttribute, strClose);
                case MathOperation.GreaterThan: return String.Format("{0}{1} > {2}{3}", strOpen, strAttribute, compare, strClose);
                case MathOperation.GreaterThanOrEqual: return String.Format("{0}{1} >= {2}{3}", strOpen, strAttribute, compare, strClose);
                case MathOperation.LessThan: return String.Format("{0}{1} < {2}{3}", strOpen, strAttribute, compare, strClose);
                case MathOperation.LessThanOrEqual: return String.Format("{0}{1} <= {2}{3}", strOpen, strAttribute, compare, strClose);
                default: return String.Format("{0}{1} = {2}{3}", strOpen, strAttribute, compare, strClose);
            }
        }

        public static string NoteAttribute(byte[] bytesAttrAndValue)
        {
            // Notes typically reverse the condition, because instead of "if X, goto Y" we want "if not X, don't goto Y"
            if (bytesAttrAndValue == null || bytesAttrAndValue.Length < 2)
                return String.Empty;
            long lVal = ValueForAttribute(bytesAttrAndValue[0], bytesAttrAndValue.Skip(1).ToArray());
            StringBuilder sb = new StringBuilder(Attribute(bytesAttrAndValue[0], lVal, false, false, true, false));

            // Fix some clumsy English
            sb.Replace("not HP Over MaxHP", "HP < MaxHP");
            sb.Replace("not SP Over MaxSP", "SP < MaxSP");

            return sb.ToString();
        }

        public static string Attribute(byte[] bytesAttrAndValue)
        {
            return Attribute(bytesAttrAndValue, false);
        }

        public static string ShortAttribute(byte[] bytesAttrAndValue)
        {
            if (bytesAttrAndValue == null || bytesAttrAndValue.Length < 2)
                return "?=?";
            long lVal = ValueForAttribute(bytesAttrAndValue[0], bytesAttrAndValue.Skip(1).ToArray());
            return String.Format("{0}={1}", ShortAttribute(bytesAttrAndValue[0]), lVal);
        }

        public static string Attribute(byte[] bytesAttrAndValue, bool bUnknownString)
        {
            if (bytesAttrAndValue == null || bytesAttrAndValue.Length < 2)
                return "Unknown";
            long lVal = ValueForAttribute(bytesAttrAndValue[0], bytesAttrAndValue.Skip(1).ToArray());
            return Attribute(bytesAttrAndValue[0], lVal, false, bUnknownString);
        }

        public static string Attribute(byte attr)
        {
            MathOperation op;
            return Attribute(attr, out op, false);
        }

        public static string Attribute(byte attr, bool bUnknownString)
        {
            MathOperation op;
            return Attribute(attr, out op, false, bUnknownString);
        }

        public static string Attribute(byte attr, out MathOperation op, bool bAbbrev = false, bool bUnknownString = false)
        {
            op = MathOperation.None;
            if (bAbbrev)
                return ShortAttribute(attr);

            switch ((MM345ScriptAttribute)attr)
            {
                case MM345ScriptAttribute.Condition: return "Condition";
                case MM345ScriptAttribute.Sex: return "Sex";
                case MM345ScriptAttribute.Race: return "Race";
                case MM345ScriptAttribute.Alignment: return "Alignment";
                case MM345ScriptAttribute.Class: return "Class";
                case MM345ScriptAttribute.CurrentHP: return "Hit Points";
                case MM345ScriptAttribute.CurrentSP: return "Spell Points";
                case MM345ScriptAttribute.TempLevel: return "Temporary Level";
                case MM345ScriptAttribute.TempArmorClass: return "Temporary Armor Class";
                case MM345ScriptAttribute.TempAge: return "Temporary Age";
                case MM345ScriptAttribute.Skill: return "Skill";
                case MM345ScriptAttribute.TempMight: return "Temporary Might";
                case MM345ScriptAttribute.TempIntellect: return "Temporary Intellect";
                case MM345ScriptAttribute.TempPersonality: return "Temporary Personality";
                case MM345ScriptAttribute.TempEndurance: return "Temporary Endurance";
                case MM345ScriptAttribute.TempSpeed: return "Temporary Speed";
                case MM345ScriptAttribute.TempAccuracy: return "Temporary Accuracy";
                case MM345ScriptAttribute.TempLuck: return "Temporary Luck";
                case MM345ScriptAttribute.PermMight: return "Permanent Might";
                case MM345ScriptAttribute.PermIntellect: return "Permanent Intellect";
                case MM345ScriptAttribute.PermPersonality: return "Permanent Personality";
                case MM345ScriptAttribute.PermEndurance: return "Permanent Endurance";
                case MM345ScriptAttribute.PermSpeed: return "Permanent Speed";
                case MM345ScriptAttribute.PermAccuracy: return "Permanent Accuracy";
                case MM345ScriptAttribute.PermLuck: return "Permanent Luck";
                case MM345ScriptAttribute.TotalMight: return "Total Might";
                case MM345ScriptAttribute.TotalIntellect: return "Total Intellect";
                case MM345ScriptAttribute.TotalPersonality: return "Total Personality";
                case MM345ScriptAttribute.TotalEndurance: return "Total Endurance";
                case MM345ScriptAttribute.TotalSpeed: return "Total Speed";
                case MM345ScriptAttribute.TotalAccuracy: return "Total Accuracy";
                case MM345ScriptAttribute.TotalLuck: return "Total Luck";
                case MM345ScriptAttribute.PermFireRes: return "Permanent Fire Resistance";
                case MM345ScriptAttribute.PermElectricityRes: return "Permanent Electricity Resistance";
                case MM345ScriptAttribute.PermColdRes: return "Permanent Cold Resistance";
                case MM345ScriptAttribute.PermPoisonRes: return "Permanent Poison Resistance";
                case MM345ScriptAttribute.PermEnergyRes: return "Permanent Energy Resistance";
                case MM345ScriptAttribute.PermMagicRes: return "Permanent Magic Resistance";
                case MM345ScriptAttribute.TempFireRes: return "Temporary Fire Resistance";
                case MM345ScriptAttribute.TempElectricityRes: return "Temporary Electricity Resistance";
                case MM345ScriptAttribute.TempColdRes: return "Temporary Cold Resistance";
                case MM345ScriptAttribute.TempPoisonRes: return "Temporary Poison Resistance";
                case MM345ScriptAttribute.TempEnergyRes: return "Temporary Energy Resistance";
                case MM345ScriptAttribute.TempMagicRes: return "Temporary Magic Resistance";
                case MM345ScriptAttribute.PermLevel: return "Permanent Level";
                case MM345ScriptAttribute.Experience: return "Experience";
                case MM345ScriptAttribute.PoisonRes: return "Party Poison Resistance";
                case MM345ScriptAttribute.FireRes: return "Party Fire Resistance";
                case MM345ScriptAttribute.ElecRes: return "Party Electrical Resistance";
                case MM345ScriptAttribute.ColdRes: return "Party Cold Resistance";
                case MM345ScriptAttribute.Gold: return "Gold";
                case MM345ScriptAttribute.Food: return "Food";
                case MM345ScriptAttribute.Hireling: return "Hireling";
                case MM345ScriptAttribute.Minutes: return "Minutes";
                case MM345ScriptAttribute.Gems: return "Gems";
                case MM345ScriptAttribute.Spell: return "Spell";
                case MM345ScriptAttribute.Scroll: return "Spell Scroll";
                case MM345ScriptAttribute.Levitate: return "LevitationCast";
                case MM345ScriptAttribute.Light: return "LightCast";
                case MM345ScriptAttribute.Protection: return "Protective Spells";
                case MM345ScriptAttribute.GameYear: return "Game Year";
                case MM345ScriptAttribute.TempArmorClass2: return "Temporary Armor Class";
                case MM345ScriptAttribute.HPOverMaxHP:
                    op = MathOperation.IsFalse;
                    return "HP Over MaxHP";
                case MM345ScriptAttribute.SPOverMaxSP:
                    op = MathOperation.IsFalse;
                    return "SP Over MaxSP";
                case MM345ScriptAttribute.Days: return "Day of Year";
                case MM345ScriptAttribute.PhysicalDamage: return "Physical Damage";
                case MM345ScriptAttribute.DamageType: return "Damage Type";
                case MM345ScriptAttribute.DayOfWeek: return "Day of Week";
                case MM345ScriptAttribute.WizardEye: return "Wizard Eye";
                case MM345ScriptAttribute.WalkOnWater: return "Walk on Water";
                case MM345ScriptAttribute.SkullsToKranion: return "Skulls Given to Kranion";
                case MM345ScriptAttribute.OrbsToZealot: return "Orbs Given to Zealot";
                case MM345ScriptAttribute.OrbsToTumult: return "Orbs Given to Tumult";
                case MM345ScriptAttribute.OrbsToMalefactor: return "Orbs Given to Malefactor";
                case MM345ScriptAttribute.Facing: return "Party Orientation";
                case MM345ScriptAttribute.Thievery: return "Thievery";
                case MM345ScriptAttribute.PartyBit: return "Party Bit Set";
                case MM345ScriptAttribute.WorldBit: return "World Bit Set";
                case MM345ScriptAttribute.QuestBit: return "Quest Bit Set";
                case MM345ScriptAttribute.MegaCredits: return "MegaCredits";
                case MM345ScriptAttribute.Award: return "Award";
                case MM345ScriptAttribute.RandomItem: return "Random Item";
                case MM345ScriptAttribute.RandomGold: return "Random Gold";
                case MM345ScriptAttribute.RandomGems: return "Random Gems";
                case MM345ScriptAttribute.RandomFood: return "Random Food";
                case MM345ScriptAttribute.CharBit: return "Character Bit Set";
                case MM345ScriptAttribute.Item: return "Item";
                default: return bUnknownString ? String.Format("Unknown:{0}", attr) : String.Empty;
            }
        }

        public string AttributeValueString(byte attr, long value)
        {
            switch ((MM345ScriptAttribute)attr)
            {
                case MM345ScriptAttribute.Facing: return FacingString((int)value, false);
                case MM345ScriptAttribute.Sex: return MM345BaseCharacter.SexString(MM3Character.SexFromByte((byte)value));
                case MM345ScriptAttribute.Race: return MM345BaseCharacter.RaceString(MM3Character.MM45RaceFromByte((byte)value));
                case MM345ScriptAttribute.Class: return MM345BaseCharacter.ClassString(MM3Character.ClassFromByte((byte)value));
                case MM345ScriptAttribute.Alignment: return MM345BaseCharacter.AlignmentString((MM345AlignmentValue)value);
                case MM345ScriptAttribute.DamageType: return MMMonster.GetDamageTypeString((DamageType)value);
                default: return String.Format("{0}", value);
            }
        }

        public string AttributeIsEqualString(byte attr, long value, bool bAbbrev = false)
        {
            switch ((MM345ScriptAttribute)attr)
            {
                case MM345ScriptAttribute.Condition: return String.Format("{0}({1}) > 0",
                    bAbbrev ? ShortAttribute(attr) : Attribute(attr),
                    MMCondition.ConditionString((ConditionIndex)value));
                default: return String.Format("({0}) is {1}", bAbbrev ? ShortAttribute(attr) : Attribute(attr), AttributeValueString(attr, value));
            }

        }

        public string SetAttributeToString(byte attr, long value, bool bAbbrev = false)
        {
            string strValue = String.Format("{0}", value);
            switch ((MM345ScriptAttribute)attr)
            {
                case MM345ScriptAttribute.Condition:
                    if (value == 16)    // Special value for "cure all"
                        return m_bForNote ? "Character is cured of all conditions" : "(Cure of All Conditions)";
                    if (m_bForNote)
                        return String.Format("Character becomes {0}:{1}", bAbbrev ? ShortAttribute(attr) : Attribute(attr), MMCondition.ConditionString((ConditionIndex)value));
                    return String.Format("set {0}({1})", bAbbrev ? ShortAttribute(attr) : Attribute(attr), MMCondition.ConditionString((ConditionIndex)value));
                case MM345ScriptAttribute.Experience:
                case MM345ScriptAttribute.Gold:
                case MM345ScriptAttribute.Gems:
                    break;
                default:
                    strValue = AttributeValueString(attr, value);
                    break;
            }
            if (bAbbrev)
                return String.Format("{0}={1}", ShortAttribute(attr), strValue);
            if (m_bForNote)
            {
                switch ((MM345ScriptAttribute)attr)
                {
                    case MM345ScriptAttribute.Facing:
                        return String.Empty;
                    default:
                        return String.Format("Set {0} to {1}", Attribute(attr), strValue);
                }
            }
            return String.Format("set ({0}) to {1}", Attribute(attr), strValue);
        }

        public static string ShortAttribute(byte attr)
        {
            switch ((MM345ScriptAttribute)attr)
            {
                case MM345ScriptAttribute.Condition: return "Cond";
                case MM345ScriptAttribute.Sex: return "Sex";
                case MM345ScriptAttribute.Race: return "Race";
                case MM345ScriptAttribute.Alignment: return "Align";
                case MM345ScriptAttribute.Class: return "Class";
                case MM345ScriptAttribute.Skill: return "Skill";
                case MM345ScriptAttribute.CurrentHP: return "HP";
                case MM345ScriptAttribute.CurrentSP: return "SP";
                case MM345ScriptAttribute.TempLevel: return "TempLev";
                case MM345ScriptAttribute.TempArmorClass: return "TempAC";
                case MM345ScriptAttribute.TempAge: return "TempAge";
                case MM345ScriptAttribute.TempMight: return "TempMgt";
                case MM345ScriptAttribute.TempIntellect: return "TempInt";
                case MM345ScriptAttribute.TempPersonality: return "TempPer";
                case MM345ScriptAttribute.TempEndurance: return "TempEnd";
                case MM345ScriptAttribute.TempSpeed: return "TempSpd";
                case MM345ScriptAttribute.TempAccuracy: return "TempAcy";
                case MM345ScriptAttribute.TempLuck: return "TempLck";
                case MM345ScriptAttribute.PermMight: return "Mgt";
                case MM345ScriptAttribute.PermIntellect: return "Int";
                case MM345ScriptAttribute.PermPersonality: return "Per";
                case MM345ScriptAttribute.PermEndurance: return "End";
                case MM345ScriptAttribute.PermSpeed: return "Spd";
                case MM345ScriptAttribute.PermAccuracy: return "Acy";
                case MM345ScriptAttribute.PermLuck: return "Lck";
                case MM345ScriptAttribute.TotalMight: return "TotMgt";
                case MM345ScriptAttribute.TotalIntellect: return "TotInt";
                case MM345ScriptAttribute.TotalPersonality: return "TotPer";
                case MM345ScriptAttribute.TotalEndurance: return "TotEnd";
                case MM345ScriptAttribute.TotalSpeed: return "TotSpd";
                case MM345ScriptAttribute.TotalAccuracy: return "TotAcy";
                case MM345ScriptAttribute.TotalLuck: return "TotLck";
                case MM345ScriptAttribute.PermFireRes: return "FireRes";
                case MM345ScriptAttribute.PermElectricityRes: return "ElecRes";
                case MM345ScriptAttribute.PermColdRes: return "ColdRes";
                case MM345ScriptAttribute.PermPoisonRes: return "PoisRes";
                case MM345ScriptAttribute.PermEnergyRes: return "EnerRes";
                case MM345ScriptAttribute.PermMagicRes: return "MagRes";
                case MM345ScriptAttribute.TempFireRes: return "TempFireRes";
                case MM345ScriptAttribute.TempElectricityRes: return "TempElecRes";
                case MM345ScriptAttribute.TempColdRes: return "TempColdRes";
                case MM345ScriptAttribute.TempPoisonRes: return "TempPoisRes";
                case MM345ScriptAttribute.TempEnergyRes: return "TempEnerRes";
                case MM345ScriptAttribute.TempMagicRes: return "TempMagRes";
                case MM345ScriptAttribute.PermLevel: return "Level";
                case MM345ScriptAttribute.Experience: return "Exp";
                case MM345ScriptAttribute.PoisonRes: return "MagPoisRes";
                case MM345ScriptAttribute.FireRes: return "MagFireRes";
                case MM345ScriptAttribute.ElecRes: return "MagElecRes";
                case MM345ScriptAttribute.ColdRes: return "MagColdRes";
                case MM345ScriptAttribute.Gold: return "Gold";
                case MM345ScriptAttribute.Food: return "Food";
                case MM345ScriptAttribute.Hireling: return "Hireling";
                case MM345ScriptAttribute.Minutes: return "Minutes";
                case MM345ScriptAttribute.Gems: return "Gems";
                case MM345ScriptAttribute.Spell: return "Spell";
                case MM345ScriptAttribute.Scroll: return "Scroll";
                case MM345ScriptAttribute.Levitate: return "Levitate";
                case MM345ScriptAttribute.Light: return "Light";
                case MM345ScriptAttribute.Protection: return "Protect";
                case MM345ScriptAttribute.TempArmorClass2: return "TempAC";
                case MM345ScriptAttribute.HPOverMaxHP: return "ExtraHP";
                case MM345ScriptAttribute.SPOverMaxSP: return "ExtraSP";
                case MM345ScriptAttribute.PhysicalDamage: return "Damage";
                case MM345ScriptAttribute.DamageType: return "DamageType";
                case MM345ScriptAttribute.Days: return "Day";
                case MM345ScriptAttribute.DayOfWeek: return "Weekday";
                case MM345ScriptAttribute.GameYear: return "GameYear";
                case MM345ScriptAttribute.WizardEye: return "WizardEye";
                case MM345ScriptAttribute.WalkOnWater: return "WaterWalk";
                case MM345ScriptAttribute.SkullsToKranion: return "KranionSkulls";
                case MM345ScriptAttribute.OrbsToZealot: return "ZealotOrbs";
                case MM345ScriptAttribute.OrbsToTumult: return "TumultOrbs";
                case MM345ScriptAttribute.OrbsToMalefactor: return "MalefactorOrbs";
                case MM345ScriptAttribute.Facing: return "Facing";
                case MM345ScriptAttribute.Thievery: return "Thievery";
                case MM345ScriptAttribute.PartyBit: return "PBit";
                case MM345ScriptAttribute.WorldBit: return "WBit";
                case MM345ScriptAttribute.QuestBit: return "QBit";
                case MM345ScriptAttribute.MegaCredits: return "MCredits";
                case MM345ScriptAttribute.Award: return "Award";
                case MM345ScriptAttribute.RandomItem: return "Item";
                case MM345ScriptAttribute.RandomGold: return "RandGold";
                case MM345ScriptAttribute.RandomGems: return "RandGems";
                case MM345ScriptAttribute.RandomFood: return "RandFood";
                case MM345ScriptAttribute.CharBit: return "CBit";
                case MM345ScriptAttribute.Item: return "Item";
                default: return "?";
            }
        }

        public string Random()
        {
            if (m_bForNote)
                return String.Format("{0}% chance: ", 100 / m_args[0]);
                 
            return String.Format("If Random(1..{0}) is {1}, goto {2}{3}", m_args[0], m_args[1], m_strLinePrefix, m_args[2]);
        }

        public string TradeString(List<MM345Reward> rewards)
        {
            StringBuilder sb = new StringBuilder();

            bool bPay = false;
            bool bGoldOrGems = false;
            int iItemCount = 0;
            string strThen = m_bAbbrev ? ", " : " then ";
            string strAnd = m_bAbbrev ? ", " : " and ";

            foreach (MM345Reward reward in rewards)
            {
                if (reward.IsEmpty)
                    continue;

                switch (iItemCount)
                {
                    case 0:
                        if (reward.ForPaying)
                        {
                            bGoldOrGems = reward.IsCurrency;
                            bPay = true;
                        }
                        break;
                    case 1:
                        if (bPay)
                            sb.Append(strThen);
                        break;
                    default:
                        sb.Append(strAnd);
                        break;
                }
                sb.Append(m_bForNote ? reward.NoteString : reward.ScriptString);
                iItemCount++;
            }

            if (bPay && bGoldOrGems && !m_bAbbrev)
                sb.Append(", or exit the script if the party cannot pay.");

            return sb.ToString();
        }

        public List<MM345Reward> Trade()
        {
            List<MM345Reward> rewards = new List<MM345Reward>();
            int iItemCount = 0;
            int iCurrent = 0;
            while (iCurrent < m_args.Length)
            {
                MM345Reward reward = SingleReward(m_args, ref iCurrent, true, iItemCount == 0, m_bAbbrev);
                if (!reward.IsEmpty)
                    rewards.Add(reward);
                iItemCount++;
            }

            return rewards;
        }

        public string SetCharacter()
        {
            switch (m_args[0])
            {
                case 0: return "SetCharacter: Any/All";
                case 7: return "SetCharacter: Random";
                case 9: return "SetCharacter: UserSelected";
                default: return String.Format("SetCharacter: First {0}", m_args[0]);
            }
        }

        public string Question()
        {
            string strExpect = "<null>";
            if (m_args[0] != 0 && m_args[2] != 0 && (m_args[0] != m_args[2]))
                strExpect = String.Format("\"{0}\" or \"{1}\"", MapString(m_args[0]), MapString(m_args[2]));
            else if (m_args[0] != 0)
                strExpect = String.Format("\"{0}\"", MapString(m_args[0]));
            else if (m_args[2] != 0)
                strExpect = String.Format("\"{0}\"", MapString(m_args[2]));
            else
                strExpect = "valid portal destination";

            if (m_bForNote)
                return String.Format("{0}\r\nIf not {1}:", MapString(m_args[3]), strExpect);
            return String.Format("Ask({0}), if ({1}) goto {2}{3}", MapString(m_args[3]), strExpect, m_strLinePrefix, m_args[1]);
        }

        public string MultiAnswer()
        {
            if (m_bForNote)
                return String.Format("If neither \"{0}\" nor \"{1}\":", MapString(m_args[0]), MapString(m_args[2]));
            return String.Format("ReadString, if \"{0}\" goto {1}{2}, if \"{3}\" goto {4}{5}",
                MapString(m_args[0]), m_strLinePrefix, m_args[1], MapString(m_args[2]), m_strLinePrefix, m_args[3]);
        }

        public string NumericChoice()
        {
            if (m_bForNote)
                return String.Format("Choose from 1 to {0}\r\nIf {1}:", m_args[0], m_args[0]);

            StringBuilder sb = new StringBuilder();
            int iCount = 0;
            foreach (byte b in m_args.GetRange(1).Bytes)
            {
                iCount++;
                sb.AppendFormat("{0}{1}, ", m_strLinePrefix, b);
            }

            if (Global.Trim(sb).Length == 0)
                return String.Format("Choose(1-{0})", m_args[0]);

            if (iCount < m_args[0])
                sb.Append(", <next>");

            return String.Format("Choose from 1-{0} and goto: {1}", m_args[0], sb.ToString());
        }

        public string JumpRandom()
        {
            if (m_bForNote)
                return String.Format("Select one of {0} randomly:", m_args[0]);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in m_args.GetRange(1,m_args[0]).Bytes)
                sb.AppendFormat("{0}{1}, ", m_strLinePrefix, b);

            if (Global.Trim(sb).Length == 0)
                return String.Format("JumpRandom({0})", m_args[0]);
            return String.Format("Goto random line: {0}", sb.ToString());
        }

        public string SubScript()
        {
            return String.Format("Run script from square ({0},{1}) beginning at line {2}", m_args[0], m_args[1], m_args[2]);
        }

        public string Damage()
        {
            if (m_args[2] == 7)
                return "Damage: Death";
            if (m_bAbbrev)
                return String.Format("{0}Dmg({1})", MMMonster.GetDamageTypeStringShort((DamageType)m_args[2]), SafeUInt16(m_args, 0));
            if (m_bForNote)
                return String.Format("({0} damage: {1})", MMMonster.GetDamageTypeString((DamageType)m_args[2]), SafeUInt16(m_args, 0));
            return String.Format("{0} Damage: {1}", MMMonster.GetDamageTypeString((DamageType)m_args[2]), SafeUInt16(m_args, 0));
        }

        public virtual string BusinessString(byte b)
        {
            switch (b)
            {
                case 0: return "Bank";
                case 1: return "Store";
                case 2: return "Guild";
                case 3: return "Inn";
                case 4: return "Tavern";
                case 5: return "Temple";
                case 6: return "Training";
                case 7: return "Arena";
                case 8: return "Joke";
                default: return String.Format("Unknown({0})", b);
            }
        }

        public string Business()
        {
            return String.Format("Run External Code: {0}", BusinessString(m_args[0]));
        }

        public string MonsterName(int iIndex)
        {
            if (m_info.Monsters != null && m_info.Monsters.Count > iIndex && Monsters.Count > iIndex)
            {
                int iMonsterIndex = m_info.Monsters[iIndex];
                if (Monsters.Count > iMonsterIndex)
                    return Monsters[m_info.Monsters[iIndex]].ProperName;
                return String.Format("UnknownMonster({0})", iMonsterIndex);
            }
            return "<null>";
        }

        public ScriptSummary Summon()
        {
            string strMonster = MonsterName(m_args[0]);
            SummonInfo summonInfo = new SummonInfo(strMonster, new Point(m_args[1], m_args[2]));
            if (m_bForNote)
                return new ScriptSummary(summonInfo, String.Format("Summon {0} ({1},{2})", strMonster, m_args[1], m_args[2]));
            return new ScriptSummary(summonInfo, String.Format("Summon Monster[{0}] ({1}) to ({2},{3})",
                m_args[0], strMonster, m_args[1], m_args[2]));
        }

        public string SetCommand()
        {
            return String.Format("Set the command byte of line {0} to {1}", m_args[0], m_args[1]);
        }

        public static string ItemTypeString(byte b)
        {
            return MM45Item.FromScriptBytes(new byte[] { b, 0, 0, 0 }).ScriptString;
        }

        public ScriptSummary SetItemType()
        {
            ScriptSummary ss = new ScriptSummary(String.Format("Set Item Type: {0}", ItemTypeString(m_args[0])), ScriptCommand.SetItemType);
            ss.Value = m_args[0];
            return ss;
        }

        public virtual string FacingString(int i, bool bAbbrev = false) { return MM3MemoryHacker.FacingString(i, bAbbrev); }
        public virtual DirectionFlags FacingDirection(int i) { return MM3MemoryHacker.FacingDirection(i); }
    }

    public abstract class MM345Scripts : GameScripts
    {
        public override bool HasHeaderBytes { get { return true; } }
    }
}
