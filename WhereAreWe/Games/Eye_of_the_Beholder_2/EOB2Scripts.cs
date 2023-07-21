﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum EOB2Opcode
    {
        Invalid = 0x00,
        WindowPictures = 0xE2,
        TextMenu = 0xE3,
        UpdateScreen = 0xE4,
        Wait = 0xE5,
        SpecialEncounter = 0xE6,
        IdentifyAllItems = 0xE7,
        Turn = 0xE8,
        Launcher = 0xE9,
        NewItem = 0xEA,
        GiveExperience = 0xEB,
        ChangeLevel = 0xEC,
        ItemConsume = 0xED,
        Conditions = 0xEE,
        Call = 0xEF,
        Return = 0xF0,
        EndCode = 0xF1,
        Jump = 0xF2,
        Damage = 0xF3,
        Heal = 0xF4,
        ClearFlag = 0xF5,
        Sound = 0xF6,
        SetFlag = 0xF7,
        Message = 0xF8,
        StealSmallItem = 0xF9,
        Teleport = 0xFA,
        CreateMonster = 0xFB,
        CloseDoor = 0xFC,
        OpenDoor = 0xFD,
        ChangeWall = 0xFE,
        SetWall = 0xFF,

        First = WindowPictures,
        Last = SetWall
    }

    public class EOB2ScriptInfo : ScriptInfo
    {
        public EOB2ScriptInfo(MapTitleInfo map, EOB2Scripts scripts)
        {
            Map = map;
            Scripts = scripts;
        }
    }

    public class EOB2ScriptLine : ScriptLine
    {
        public static string OpcodeDescription(EOB2Opcode opcode)
        {
            switch (opcode)
            {
                case EOB2Opcode.WindowPictures: return "Window pictures";
                case EOB2Opcode.TextMenu: return "Text menu";
                case EOB2Opcode.UpdateScreen: return "Update screen";
                case EOB2Opcode.Wait: return "Wait";
                case EOB2Opcode.SpecialEncounter: return "Special encounter";
                case EOB2Opcode.IdentifyAllItems: return "Identify all items";
                case EOB2Opcode.Turn: return "Turn";
                case EOB2Opcode.Launcher: return "Launch";
                case EOB2Opcode.NewItem: return "New item";
                case EOB2Opcode.GiveExperience: return "Give experience";
                case EOB2Opcode.ChangeLevel: return "Change level";
                case EOB2Opcode.ItemConsume: return "Item consume";
                case EOB2Opcode.Conditions: return "Conditions";
                case EOB2Opcode.Call: return "Call";
                case EOB2Opcode.Return: return "Return";
                case EOB2Opcode.EndCode: return "End";
                case EOB2Opcode.Jump: return "Jump";
                case EOB2Opcode.Damage: return "Damage";
                case EOB2Opcode.Heal: return "Heal";
                case EOB2Opcode.ClearFlag: return "Clear flag";
                case EOB2Opcode.Sound: return "Sound";
                case EOB2Opcode.SetFlag: return "Set flag";
                case EOB2Opcode.Message: return "Message";
                case EOB2Opcode.StealSmallItem: return "Steal small item";
                case EOB2Opcode.Teleport: return "Teleport";
                case EOB2Opcode.CreateMonster: return "Create monster";
                case EOB2Opcode.CloseDoor: return "Close door";
                case EOB2Opcode.OpenDoor: return "Open door";
                case EOB2Opcode.ChangeWall: return "Change";
                case EOB2Opcode.SetWall: return "Set wall";
                default: return String.Format("Invalid Opcode: {0:X2}", (int)opcode);
            }
        }

        public EOB2Opcode Opcode { get { return Bytes == null ? EOB2Opcode.Invalid : (EOB2Opcode)Bytes.Bytes[0]; } }

        public EOB2ScriptLine(int iMemOffset, int iScriptOffset, Point pt, byte[] command)
        {
            Location = pt;
            Address = iScriptOffset;
            Bytes = new MemoryBytes(command, iMemOffset + Address);
        }

        public override string ToString()
        {
            if (Bytes == null || Bytes.Bytes == null || Bytes.Bytes.Length == 0)
                return "<null>";
            return String.Format("{0}: {1}", Opcode.ToString(), Global.ByteString(Bytes.Bytes));
        }

        public string Text, Text2, Text3;
        public Point TargetPoint;
        public Point SourcePoint;
        public int TargetValue;
        public int SourceValue;
        public int Direction;
        public DamageDice Dice;
        public int Jump;

        public override Point TeleportLocation { get { return IsTeleportCommand ? EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(Bytes, 2)) : base.TeleportLocation; } }
        public override bool IsTeleportCommand { get { return Opcode == EOB2Opcode.Teleport; } }
        public override bool IsEncounterCommand { get { return Opcode == EOB2Opcode.CreateMonster; } }
        public override int TeleportMapIndex { get { return IsTeleportCommand ? CommandBytes[1] : base.TeleportMapIndex; } }

        public static string BoolOper(int iNot)
        {
            switch (iNot)
            {
                case 0xFB: return "<=";
                case 0xFE: return "=";
                case 0xFF: return "!=";
                default: return "?=";
            }
        }

        public static string ScriptClassDesc(EOBScriptClass sc)
        {
            StringBuilder sb = new StringBuilder();
            if (sc.HasFlag(EOBScriptClass.Fighter))
                sb.Append("fighter,");
            if (sc.HasFlag(EOBScriptClass.Thief))
                sb.Append("thief,");
            if (sc.HasFlag(EOBScriptClass.Cleric))
                sb.Append("cleric,");
            if (sc.HasFlag(EOBScriptClass.Mage))
                sb.Append("mage,");
            return Global.Trim(sb).ToString();
        }

        public static string ScriptRaceDesc(EOBScriptRace sr)
        {
            switch (sr)
            {
                case EOBScriptRace.Dwarf: return "dwarf";
                case EOBScriptRace.Elf: return "elf";
                case EOBScriptRace.Gnome: return "gnome";
                case EOBScriptRace.HalfElf: return "half-elf";
                case EOBScriptRace.Halfling: return "halfling";
                case EOBScriptRace.Human: return "human";
                default: return String.Format("unknown({0})", (int)sr);
            }
        }

        public static string ActivationReason(int iVal)
        {
            EOBActivationReason reason = (EOBActivationReason)iVal;
            StringBuilder sb = new StringBuilder();
            if (reason.HasFlag(EOBActivationReason.PartyEnter))
                sb.Append("Party, ");
            if (reason.HasFlag(EOBActivationReason.PartyLeave))
                sb.Append("NoParty, ");
            if (reason.HasFlag(EOBActivationReason.ItemEnter))
                sb.Append("Item, ");
            if (reason.HasFlag(EOBActivationReason.ItemLeave))
                sb.Append("NoItem, ");
            if (reason.HasFlag(EOBActivationReason.MissileEnter))
                sb.Append("Missile, ");
            if (sb.Length < 2)
                return String.Format("Unknown({0})", iVal);
            return Global.Trim(sb).ToString();
        }

        public void AddNoteCond(List<EOBConditional> list, String strFormat, int iBoolOperation, params object[] args)
        {
            string str = String.Format(strFormat, args).Replace("{isnot}", iBoolOperation == 0xFF ? "is" : "is not"); // reversed because typically a successful condition skips the next line
            EOBConditional conditional = new EOBConditional(str);
            list.Add(conditional);
        }

        public void AddCond(List<EOBConditional> list, string strFormat, string strFormatAbbrev, int iBoolOperation, params object[] args)
        {
            string strBool = BoolOper(iBoolOperation);
            string str1 = String.Format(strFormat, args).Replace("{bool}", strBool);
            string str2 = String.Format(strFormatAbbrev, args).Replace("{bool}", strBool).Replace("{not}", iBoolOperation == 0xFF ? "!" : "");
            EOBConditional conditional = new EOBConditional(str1);
            conditional.Abbreviation = str2;
            list.Add(conditional);
        }

        public static string ItemType(int iVal)
        {
            if (iVal == 255)
                return "Any";
            return EOBItem.GetName((EOBItemIndex)iVal);
        }

        public static string ListItem(int iIndex)
        {
            return String.Format("#{0}", iIndex);
        }

        public static string Relative(int iVal)
        {
            switch (iVal)
            {
                case 0xFF: return ">";
                default: return "?";
            }
        }

        public static int ConditionLength(byte b)
        {
            switch (b)
            {
                case 0xED: return 2;  // Party direction (dir, op)
                case 0xF7: return 4;  // Walls at (pt, dir, op)
                case 0xE7: return 3;  // Item (type/mod/index, val, op)
                case 0xEF: return 5;  // Level flag (index, val, op, ?, ?)
                case 0xDD: return 3;  // Active spell (index, val, op)
                case 0xF0: return 3;  // Global flag (index, val, op)
                case 0xDA: return 2;  // End game (?, ?)
                case 0xDB: return 5;  // Roll (count, faces, bonus, val, op)
                case 0xDC: return 3;  // Party includes (class, val, op)
                case 0xE9: return 5;  // Wall is (dir, pt, val, op)
                case 0xF1: return 4;  // Party location (pt, dir, op)
                case 0xF5: return 5;  // Item (type, pt, val, op)
                case 0xF9: return 0;  // Or
                case 0xF8: return 0;  // Nor
                case 0x00:
                case 0x01:
                case 0x02:
                case 0x03: return 2;  // Party direction (dir, op)
                case 0xE0: return 4;  // Activation (reason, op, ?, ?)
                default: return 1;
            }
        }

        public static int ConditionEnd(byte[] bytes, int iStart)
        {
            // Returns the index of the 0xEE value that represents the end of this conditional expression
            while (iStart < bytes.Length && bytes[iStart] != 0xEE)
            {
                iStart += ConditionLength(bytes[iStart]);
                iStart++;
            }
            return iStart;
        }

        public string ConditionsString(bool bAbbrev = false, bool bForNote = false, EOB2ScriptInfo info = null)
        {
            StringBuilder sb = new StringBuilder();
            int iStart = 1; // Skip the first EE
            Point pt;
            int iFlag = 0;
            int iDir = 0;
            int iValue = 0;
            int iFaces = 0;
            int iCount = 0;
            int iBonus = 0;
            int iNot = 0;
            int iSubCode = 0;
            string strShort = "";
            List<EOBConditional> conditions = new List<EOBConditional>();

            while (iStart < CommandBytes.Length)
            {
                switch (CommandBytes[iStart++])
                {
                    case 0xEE:
                        // End of conditions
                        iStart = CommandBytes.Length;
                        break;
                    case 0xED:
                        iDir = CommandBytes[iStart++];
                        iNot = CommandBytes[iStart++];
                        AddCond(conditions, "party direction {{bool}} {0}", "{{not}}{0}?", iNot, EOBMemoryHacker.FacingString(iDir));
                        break;
                    case 0xF7:
                        pt = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(CommandBytes, iStart));
                        iStart += 2;
                        if (iStart < CommandBytes.Length)
                            iValue = CommandBytes[iStart++];
                        if (iStart < CommandBytes.Length)
                            iNot = CommandBytes[iStart++];
                        AddCond(conditions, "the walls at {0} {{bool}} {1}", "Wall{{bool}}{2}?", iNot, PointDesc(pt), WallDesc(info, iValue), iValue);
                        break;
                    case 0xE7:
                        iSubCode = CommandBytes[iStart++];
                        iValue = CommandBytes[iStart++];
                        iNot = CommandBytes[iStart++];
                        switch (iSubCode)
                        {
                            case 0xE1:
                                AddCond(conditions, "item type {{bool}} {0} ({1})", "{{not}}{1}?", iNot, iValue, ItemType(iValue));
                                break;
                            case 0xF6:
                                AddCond(conditions, "item modifier {{bool}} {0}", "mod{{bool}}{0}?", iNot, iValue);
                                break;
                            case 0xF5:
                                AddCond(conditions, "item index {{bool}} {0}", "idx{{bool}}{0}?", iNot, iValue);
                                break;
                            default:
                                AddCond(conditions, "item ({0:X2}?) {{bool}} {1}", "({0:X2}?){{bool}}{1}?", iNot, iSubCode, iValue);
                                break;
                        }
                        break;
                    case 0xEF:
                        iFlag = CommandBytes[iStart++];
                        iValue = CommandBytes[iStart++];
                        iNot = CommandBytes[iStart++];
                        if (info?.SummaryInfo != null)
                            info.SummaryInfo.LastFlagTest = String.Format("L{0}", iFlag);
                        AddCond(conditions, "Flag Level.{0} {{bool}} {1}", "#L{0}{{bool}}{1}?", iNot, iFlag, iValue);
                        iStart += 2;
                        break;
                    case 0xDA:
                        iValue = CommandBytes[iStart++];
                        iNot = CommandBytes[iStart++];
                        AddCond(conditions, "GameFinished {{bool}} {0}", "{{not}}GameFinished:{0}?", iNot, iValue);
                        break;
                    case 0xDD:
                        iFlag = CommandBytes[iStart++];
                        iValue = CommandBytes[iStart++];
                        iNot = CommandBytes[iStart++];
                        if (bForNote)
                            AddNoteCond(conditions, "there {{isnot}} a {0} in the party: ", iNot, ScriptRaceDesc((EOBScriptRace)iFlag));
                        else
                            AddCond(conditions, "(party includes {0}) {{bool}} {1}", "{{not}}{0}:{1}?", iNot, ScriptRaceDesc((EOBScriptRace)iFlag), iValue);
                        break;
                    case 0xF0:
                        iFlag = CommandBytes[iStart++];
                        iValue = CommandBytes[iStart++];
                        iNot = CommandBytes[iStart++];
                        if (info?.SummaryInfo != null)
                            info.SummaryInfo.LastFlagTest = String.Format("G{0}", iFlag);
                        AddCond(conditions, "Flag Global.{0} {{bool}} {1}", "#G{0}{{bool}}{1}?", iNot, iFlag, iValue);
                        break;
                    case 0xDB:
                        iCount = CommandBytes[iStart++];
                        iFaces = CommandBytes[iStart++];
                        iBonus = CommandBytes[iStart++];
                        iValue = CommandBytes[iStart++];
                        iNot = CommandBytes[iStart++];
                        AddCond(conditions, "{0} {{bool}} {1}", "{0}{{bool}}{1}?", iNot, new DamageDice(iFaces, iCount, iBonus).ToString(), iValue);
                        break;
                    case 0xDC:
                        iFlag = CommandBytes[iStart++];
                        iValue = CommandBytes[iStart++];
                        iNot = CommandBytes[iStart++];
                        AddCond(conditions, "(party includes {0}) {{bool}} {1}", "{{not}}{0}?", iNot, ScriptClassDesc((EOBScriptClass)iFlag), iValue);
                        break;
                    case 0xE9:
                        iDir = CommandBytes[iStart++];
                        pt = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(CommandBytes, iStart));
                        iStart += 2;
                        if (iStart < CommandBytes.Length)
                            iValue = CommandBytes[iStart++];
                        if (iStart < CommandBytes.Length)
                            iNot = CommandBytes[iStart++];
                        AddCond(conditions, "the {0} wall at {1} {{bool}} {2}", "{0}{{bool}}{3}?", iNot, EOBMemoryHacker.FacingString(iDir), PointDesc(pt), WallDesc(info, iValue), iValue);
                        break;
                    case 0xF1:
                        if (iStart >= CommandBytes.Length - 1)
                        {
                            AddCond(conditions, "[invalid condition length]", "[invalid]", 0);
                            break;
                        }
                        pt = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(CommandBytes, iStart));
                        iStart += 2;
                        if (CommandBytes.Length > iStart + 1)
                        {
                            iDir = CommandBytes[iStart++];
                            iNot = CommandBytes[iStart++];
                        }
                        AddCond(conditions, "party location {{bool}} {0}", "{{not}}@{0}?", iNot, PointDesc(pt), EOBMemoryHacker.FacingString(iDir));
                        break;
                    case 0xF5:
                        iFlag = CommandBytes[iStart++];
                        pt = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(CommandBytes, iStart));
                        iStart += 2;
                        if (iStart < CommandBytes.Length)
                            iValue = CommandBytes[iStart++];
                        if (iStart < CommandBytes.Length)
                            iNot = CommandBytes[iStart++];
                        strShort = "Items[" + iFlag.ToString() + "]@{0}{{bool}}{1}?";
                        AddCond(conditions, "item type \"" + ItemType(iFlag) + "\" at {0} {{bool}} {1}", strShort, iNot, PointDesc(pt), iValue);
                        break;
                    case 0xF9:
                        if (conditions.Count > 0)
                            conditions[conditions.Count - 1].Prefix = "or";
                        break;
                    case 0xF8:
                        if (conditions.Count > 0)
                            conditions[conditions.Count - 1].Prefix = "nor";
                        break;
                    case 0x00:
                    case 0x01:
                    case 0x02:
                    case 0x03:
                        if (CommandBytes.Length > iStart + 1)
                        {
                            iDir = CommandBytes[iStart++];
                            iNot = CommandBytes[iStart++];
                        }
                        AddCond(conditions, "party direction {{bool}} {0}", "{{not}}{0}?", iNot, EOBMemoryHacker.FacingString(iDir));
                        break;
                    case 0xE0:
                        iValue = CommandBytes[iStart++];
                        iNot = CommandBytes[iStart++];
                        AddCond(conditions, "Activation reason {{bool}} {0}", "Active{{bool}}{0}?", iNot, ActivationReason(iValue));
                        iStart += 2;
                        break;
                    default:
                        break;
                }
            }

            for (int i = 0; i < conditions.Count; i++)
            {
                if (i != 0)
                {
                    if (bAbbrev)
                        sb.Append(conditions[i].Prefix.EndsWith("or") ? "/" : "&");
                    else
                        sb.AppendFormat(", {0} if ", conditions[i].Prefix);
                }
                else
                {
                    if (!bAbbrev)
                        sb.Append("If ");
                }
                if (i < conditions.Count - 1 && conditions[i + 1].Prefix == "nor" && !bAbbrev)
                    sb.Append("neither ");
                if (bAbbrev && conditions[i].Abbreviation != null)
                {
                    sb.Append(conditions[i].Abbreviation);
                }
                else
                    sb.Append(conditions[i].Description);
            }
            return sb.ToString();
        }

        public static string WallDesc(int iMap, int iValue) { return String.Format("{0} ({1})", iValue, Global.EOB2.GetWall(iMap, iValue).Description); }
        public static string WallDesc(ScriptInfo info, int iValue) { return String.Format("{0} ({1})", iValue, Global.EOB2.GetWall(info == null || info.Map == null ? 1 : info.Map.Map, iValue).Description); }
        public static string PointDesc(Point pt) { return String.Format("{0},{1}", pt.X, pt.Y); }
        public static string TeleportType(int i)
        {
            switch (i)
            {
                case 0xE8: return "party";
                case 0xF3: return "monsters";
                case 0xF5: return "items";
                default: return String.Format("unknown({0})", i);
            }
        }

        public static string SpecialEncounterDescription(int iEncounter)
        {
            switch (iEncounter)
            {
                case 0: return "Injured Dwarf";
                case 1: return "Armun";
                case 2: return "Dwarven Cleric";
                case 3: return "Drow Patrol";
                case 4: return "Shindia";
                case 5: return "Beholder";
                case 6: return "Dark-Robed Figure";
                case 7: return "Prince Keirgar";
                case 8: return "Gateway";
                case 9: return "End Game";
                case 10: return "Copy Protection";
                default: return String.Format("Unknown({0})", iEncounter);
            }
        }

        public override string Description(ScriptInfo info, string strLinePrefix)
        {
            // This is the text that is displayed in lower pane of the script editor
            StringBuilder sb = new StringBuilder();
            bool bUseOpcodeDescription = true;
            EOB2ScriptInfo EOB2Info = info as EOB2ScriptInfo;
            string strFacing = "All";
            switch (Opcode)
            {
                case EOB2Opcode.ItemConsume:
                    if (CommandBytes[1] != 0xFF)
                        sb.AppendFormat(" at {0}", PointDesc(TargetPoint));
                    break;
                case EOB2Opcode.StealSmallItem:
                    sb.AppendFormat(" ({0}) at {1}, {2}", ItemType(TargetValue), PointDesc(TargetPoint), SourceValue);
                    break;
                case EOB2Opcode.OpenDoor:
                case EOB2Opcode.CloseDoor:
                    sb.AppendFormat(" at {0}", PointDesc(TargetPoint));
                    break;
                case EOB2Opcode.SpecialEncounter:
                    sb.AppendFormat(" #{0} ({1})", TargetValue, SpecialEncounterDescription(TargetValue));
                    break;
                case EOB2Opcode.Message:
                    sb.AppendFormat(": {0}", TrimText(Text));
                    break;
                case EOB2Opcode.ChangeLevel:
                    sb.AppendFormat(" to {0} at {1}, {2}", TargetValue, PointDesc(TargetPoint), EOBMemoryHacker.FacingString(Direction));
                    break;
                case EOB2Opcode.IdentifyAllItems:
                    sb.AppendFormat(" at {0}", PointDesc(TargetPoint));
                    break;
                case EOB2Opcode.CreateMonster:
                    sb.AppendFormat(" {0}", new EOBScriptMonster(CommandBytes, 1).ToString());
                    break;
                case EOB2Opcode.Sound:
                    sb.AppendFormat(" #{0} at {1}", Bytes[1], PointDesc(TargetPoint));
                    break;
                case EOB2Opcode.Conditions:
                    sb.AppendFormat("{0} then goto {1}", ConditionsString(), Jump);
                    bUseOpcodeDescription = false;
                    break;
                case EOB2Opcode.Jump:
                case EOB2Opcode.Call:
                    sb.AppendFormat(" {0}", Jump);
                    break;
                case EOB2Opcode.Turn:
                    switch (Direction)
                    {
                        case 0xF1:
                            sb.AppendFormat(" party right");
                            break;
                        case 0xF5:
                            sb.AppendFormat(" missile right");
                            break;
                        default:
                            sb.AppendFormat(" ? right");
                            break;
                    }
                    sb.AppendFormat(" {0}", TargetValue);
                    break;
                case EOB2Opcode.Teleport:
                    if (SourcePoint.X == 0 && SourcePoint.Y == 0)
                        sb.AppendFormat(" {0} to {1}", TeleportType(CommandBytes[1]), PointDesc(TargetPoint));
                    else
                        sb.AppendFormat(" {0} from {1} to {2}", TeleportType(CommandBytes[1]), PointDesc(SourcePoint), PointDesc(TargetPoint));
                    break;
                case EOB2Opcode.ChangeWall:
                    if (TargetValue == -1)
                        sb.AppendFormat(" the state of the door at {0}", PointDesc(TargetPoint));
                    else if (SourceValue == -1)
                    {
                        if (Direction != -1)
                            strFacing = EOBMemoryHacker.FacingString(Direction);
                        sb.AppendFormat(" the wall at {0} ({1}) to {2}", PointDesc(TargetPoint), strFacing, TargetValue);
                    }
                    else
                    {
                        sb.AppendFormat(" the walls at {0} between {1} and {2}", PointDesc(TargetPoint), WallDesc(info, SourceValue), WallDesc(info, TargetValue));
                    }
                    break;
                case EOB2Opcode.SetWall:
                    if (CommandBytes[1] == 0xED)
                    {
                        bUseOpcodeDescription = false;
                        sb.AppendFormat("Turn party {0}", EOBMemoryHacker.FacingString(Direction));
                    }
                    else
                    {
                        if (Direction != -1)
                            strFacing = EOBMemoryHacker.FacingString(Direction);
                        sb.AppendFormat(" at {0} ({1}) to {2}", PointDesc(TargetPoint), strFacing, WallDesc(info, TargetValue));
                    }
                    break;
                case EOB2Opcode.GiveExperience:
                    sb.AppendFormat(" {0}", TargetValue);
                    break;
                case EOB2Opcode.Launcher:
                    if (CommandBytes[1] == 0xDF)
                        sb.AppendFormat(" Spell[{0}]", TargetValue);
                    else
                        sb.AppendFormat(" Item[{0}]", TargetValue);
                    sb.AppendFormat(" {1} from {2} at {3}", TargetValue, EOBMemoryHacker.FacingString(Direction), PointDesc(TargetPoint), SourceValue);
                    break;
                case EOB2Opcode.SetFlag:
                case EOB2Opcode.ClearFlag:
                    sb.AppendFormat(" {0}.{1}", FlagSource(SourceValue, false), TargetValue);
                    break;
                case EOB2Opcode.Damage:
                    sb.AppendFormat(" {0}", Dice.ToString());
                    break;
                case EOB2Opcode.NewItem:
                    sb.AppendFormat(": Copy of \"{0}\" at {1}, {2}", ListItem(TargetValue), PointDesc(TargetPoint), Direction);
                    break;
                default:
                    break;
            }
            if (bUseOpcodeDescription)
                sb.Insert(0, OpcodeDescription(Opcode));
            return sb.ToString();
        }

        private string TrimText(string str) { return str == null ? "<null>" : str.Trim(); }

        private string FlagSource(int iVal, bool bAbbrev = false)
        {
            switch (iVal)
            {
                case 0xEF: return bAbbrev ? "L" : "Level";
                case 0xF0: return bAbbrev ? "G" : "Global";
                case 0xF3: return bAbbrev ? "M" : "Monster";
                default: return String.Format("{0:X2}", iVal);
            }
        }

        private ScriptSummary ChangeWallSummaryString(int iMap, int iValue, Point pt)
        {
            if (TargetValue == 0)
                return new ScriptSummary(String.Format("(Remove the wall at {0})", PointDesc(pt)), ScriptCommand.AlterMap);
            if (TargetValue == 1)
                return new ScriptSummary(String.Format("(Add a wall at {0})", PointDesc(pt)), ScriptCommand.AlterMap);
            return new ScriptSummary(String.Format("(Change the wall at {0} to \"{1}\")", PointDesc(pt), WallDesc(iMap, iValue)), ScriptCommand.AlterMap);
        }

        private ScriptSummary NoteSummaryString(EOB2ScriptInfo info)
        {
            if (info.SummaryInfo != null)
            {
                switch (Opcode)
                {
                    case EOB2Opcode.SetFlag:
                        info.SummaryInfo.LastFlagSet = String.Format("{0}{1}", FlagSource(SourceValue, true), TargetValue);
                        break;
                    case EOB2Opcode.Jump:
                        info.SummaryInfo.LastJump = Jump;
                        break;
                    case EOB2Opcode.Conditions:
                        info.SummaryInfo.LastConditionalJump = Jump;
                        break;
                }
            }

            switch (Opcode)
            {
                case EOB2Opcode.StealSmallItem: return new ScriptSummary(String.Format("(Steal {0})", ItemType(TargetValue)), ScriptCommand.None);
                case EOB2Opcode.OpenDoor: return new ScriptSummary(String.Format("(Open door at {0})", PointDesc(TargetPoint)), ScriptCommand.None);
                case EOB2Opcode.CloseDoor: return new ScriptSummary(String.Format("(Close door at {0})", PointDesc(TargetPoint)), ScriptCommand.None);
                case EOB2Opcode.SpecialEncounter: return new ScriptSummary(String.Format("(Special Encounter: {0})", SpecialEncounterDescription(TargetValue)));
                case EOB2Opcode.ChangeLevel: return new ScriptSummary(String.Format("{{map:{0}}} ({1})", EOB2MemoryHacker.GetMapTitlePair(TargetValue).Title, PointDesc(TargetPoint)), ScriptCommand.None, "?");
                case EOB2Opcode.Message: return new ScriptSummary(Global.Sentence(TrimText(Text)), ScriptCommand.Text, "s");
                case EOB2Opcode.SetFlag: return new ScriptSummary(String.Format("(Set flag {0}.{1})", FlagSource(SourceValue), TargetValue), ScriptCommand.None);
                case EOB2Opcode.GiveExperience: return new ScriptSummary(String.Format("(+{0} XP, flag {1})", TargetValue, info?.SummaryInfo == null ? "?" : info.SummaryInfo.LastFlagTest), ScriptCommand.None);
                case EOB2Opcode.Launcher: return new ScriptSummary(String.Format("(Launch a [#{0}] from {1})", TargetValue, PointDesc(TargetPoint)), ScriptCommand.None);
                case EOB2Opcode.ItemConsume: return new ScriptSummary(String.Format("(Remove Items at {0})", PointDesc(TargetPoint)), ScriptCommand.None);
                case EOB2Opcode.NewItem: return new ScriptSummary(String.Format("(+Item [#{0}] at {1})", TargetValue, PointDesc(TargetPoint)), ScriptCommand.None);
                case EOB2Opcode.Call: return new ScriptSummary(String.Format("(Call {0})", Jump), ScriptCommand.Jump);
                case EOB2Opcode.Conditions: return new ScriptSummary(ConditionsString(false, true, info), ScriptCommand.Conditional);
                case EOB2Opcode.SetWall:
                    if (Bytes[1] == 0xED)
                        return new ScriptSummary(String.Format("Turn {0}", EOBMemoryHacker.FacingString(Direction)), ScriptCommand.None);
                    return ChangeWallSummaryString(info.Map.Map, TargetValue, TargetPoint);
                case EOB2Opcode.Teleport: return new ScriptSummary(String.Format("(Teleport to {0})", PointDesc(TargetPoint)), ScriptCommand.Teleport);
                case EOB2Opcode.Turn: return new ScriptSummary(String.Format("(Turn {0})", TargetValue == 2 ? "around" : TargetValue.ToString()), ScriptCommand.None);
                case EOB2Opcode.Damage: return new ScriptSummary(String.Format("(-{0} damage)", Dice.ToString()), ScriptCommand.Damage);
                case EOB2Opcode.ChangeWall:
                    if (TargetValue == -1)
                        return new ScriptSummary(String.Format("(Open or close the door at {0})", PointDesc(TargetPoint)), ScriptCommand.AlterMap);
                    else if (SourceValue == -1)
                        return ChangeWallSummaryString(info.Map.Map, TargetValue, TargetPoint);
                    else if ((SourceValue == 0 && TargetValue == 1) || (SourceValue == 1 && TargetValue == 0))
                        return new ScriptSummary(String.Format("(Add or remove the walls at {0})", PointDesc(TargetPoint)), ScriptCommand.AlterMap);
                    return new ScriptSummary(String.Format("(Change the walls at {0} between \"{1}\" and \"{2}\")",
                        PointDesc(TargetPoint), WallDesc(info, SourceValue), WallDesc(info, TargetValue)), ScriptCommand.AlterMap);

                default: return new ScriptSummary(OpcodeDescription(Opcode));
            }
        }

        private ScriptSummary SummaryString(EOB2ScriptInfo info, bool bForNote)
        {
            byte[] args = Bytes.GetRange(1);

            int iExpectedArgs = EOB2Script.NumArgs(Opcode);
            int iHaveArgs = (args == null ? 0 : args.Length);

            if (iHaveArgs < iExpectedArgs)
                return new ScriptSummary("BadArgs");

            if (bForNote)
                return NoteSummaryString(info);

            switch (Opcode)
            {
                case EOB2Opcode.Message: return new ScriptSummary(TrimText(Text), ScriptCommand.Text, "s");
                case EOB2Opcode.SetFlag: return new ScriptSummary(String.Format("Set #{0}{1}", FlagSource(SourceValue, true), TargetValue), ScriptCommand.None);
                case EOB2Opcode.ClearFlag: return new ScriptSummary(String.Format("Clear #{0}{1}", FlagSource(SourceValue, true), TargetValue), ScriptCommand.None);
                case EOB2Opcode.GiveExperience: return new ScriptSummary(String.Format("+{0} XP", TargetValue), ScriptCommand.None);
                case EOB2Opcode.Launcher: return new ScriptSummary(String.Format("LaunchItem[{0}]", TargetValue), ScriptCommand.None);
                case EOB2Opcode.ItemConsume: return new ScriptSummary(String.Format("-Item"), ScriptCommand.None);
                case EOB2Opcode.NewItem: return new ScriptSummary(String.Format("+Item[{0}]@{1}", TargetValue, PointDesc(TargetPoint)), ScriptCommand.None);
                case EOB2Opcode.Call: return new ScriptSummary(String.Format("Call {0}", Jump), ScriptCommand.Jump);
                case EOB2Opcode.Conditions: return new ScriptSummary(ConditionsString(false, true, info), ScriptCommand.Conditional);
                case EOB2Opcode.SetWall:
                    if (Bytes[1] == 0xED)
                        return new ScriptSummary(String.Format("Turn {0}", EOBMemoryHacker.FacingString(Direction)), ScriptCommand.None);
                    return new ScriptSummary(String.Format("Wall@{0}={1}", PointDesc(TargetPoint), TargetValue), ScriptCommand.AlterMap);
                case EOB2Opcode.Teleport: return new ScriptSummary(String.Format("Teleport {0}", PointDesc(TargetPoint)), ScriptCommand.Teleport);
                case EOB2Opcode.Turn: return new ScriptSummary(String.Format("Turn {0}", TargetValue), ScriptCommand.None);
                case EOB2Opcode.Damage: return new ScriptSummary(String.Format("Dmg {0}", Dice.ToString()), ScriptCommand.Damage);
                case EOB2Opcode.ChangeWall:
                    if (TargetValue == -1)
                        return new ScriptSummary(String.Format("Door@{0}", PointDesc(TargetPoint)), ScriptCommand.AlterMap);
                    else if (SourceValue == -1)
                        return new ScriptSummary(String.Format("Wall@{0}={1}", PointDesc(TargetPoint), TargetValue), ScriptCommand.AlterMap);
                    else
                        return new ScriptSummary(String.Format("Wall@{0} {1}<=>{2}", PointDesc(TargetPoint), SourceValue, TargetValue), ScriptCommand.AlterMap);

                default: return new ScriptSummary(OpcodeDescription(Opcode));
            }
        }

        public override List<ScriptSummary> Summary(ScriptInfo info, bool bForNote)
        {
            List<ScriptSummary> list = new List<ScriptSummary>();

            if (Bytes == null)
                return list;
            if (Bytes.Length < 1)
                return list;

            ScriptSummary summary = SummaryString(info as EOB2ScriptInfo, bForNote);
            summary.IsExitCommand = Opcode == EOB2Opcode.EndCode;

            bool bOtherwise = (info?.SummaryInfo != null && Address == info.SummaryInfo.LastConditionalJump && Opcode != EOB2Opcode.EndCode);
            if (bOtherwise)
                summary.Description = "Otherwise: " + summary.Description;

            if (String.IsNullOrWhiteSpace(summary.Symbol))
                summary.Symbol = "?";
            list.Add(summary);

            return list;
        }

        public override string GetTabSeparatedString(ScriptInfo info, string strLinePrefix)
        {
            if (Bytes == null || Bytes.Length < 1)
                return String.Format("{0}{1}\t<null>\t{2}", strLinePrefix, Number, Description(info, String.Empty));

            return String.Format("{0}{1}\t{2}\t{3}",
                strLinePrefix, Number, Global.ByteString(Bytes),
                Description(info, String.Empty));
        }
    }

    public class EOB2Script : GameScript
    {
        private List<ScriptLine> m_lines;
        private string[] m_messages;

        public override List<ScriptLine> Lines { get { return m_lines; } }
        public override bool HasHeaderBytes { get { return true; } }

        public int Offset;
        public byte[] Commands;
        public int MemoryOffset;

        public EOB2Script(IScriptAddressLocator locator, byte[] bytes, int offset = 0, int iOrigin = 0, string[] messages = null)
        {
            MemoryOffset = -1;
            Offset = offset;
            m_messages = messages;

            int iStart = Offset;
            EOB2ScriptLine line = null;
            m_lines = new List<ScriptLine>();
            int iLineNumber = 0;
            int iCurrentByte = iStart;
            Location = locator.GetLocation(iStart - iOrigin);
            do
            {
                if (iCurrentByte < 0 || iCurrentByte > bytes.Length - 1)
                    break;
                line = ReadNextLine(bytes, ref iCurrentByte, iOrigin);
                if (line != null)
                {
                    line.Number = iLineNumber++;
                    m_lines.Add(line);
                    if (line.Opcode == EOB2Opcode.EndCode)
                        break;
                }
            } while (line != null);

            Bytes = new MemoryBytes(Global.Subset(bytes, iStart, iCurrentByte - iStart));
        }

        public static int NumArgs(EOB2Opcode opcode)
        {
            switch (opcode)
            {
                case EOB2Opcode.Call:
                case EOB2Opcode.Jump:
                case EOB2Opcode.Turn:
                    return 2;
                case EOB2Opcode.WindowPictures:
                case EOB2Opcode.TextMenu:
                case EOB2Opcode.UpdateScreen:
                case EOB2Opcode.Wait:
                case EOB2Opcode.SpecialEncounter:
                case EOB2Opcode.IdentifyAllItems:
                case EOB2Opcode.NewItem:
                case EOB2Opcode.GiveExperience:
                case EOB2Opcode.ChangeLevel:
                case EOB2Opcode.ItemConsume:
                case EOB2Opcode.Conditions:
                case EOB2Opcode.Damage:
                case EOB2Opcode.Heal:
                case EOB2Opcode.ClearFlag:
                case EOB2Opcode.Sound:
                case EOB2Opcode.SetFlag:
                case EOB2Opcode.Message:
                case EOB2Opcode.Teleport:
                case EOB2Opcode.CreateMonster:
                case EOB2Opcode.CloseDoor:
                case EOB2Opcode.OpenDoor:
                case EOB2Opcode.ChangeWall:
                case EOB2Opcode.SetWall:
                    return 1;
                case EOB2Opcode.StealSmallItem:
                    return 4;
                case EOB2Opcode.Launcher:
                    return 7;
                case EOB2Opcode.Return:
                case EOB2Opcode.EndCode:
                    return 0;
                default:
                    return 0;
            }
        }

        public override void Summary(int iDepth, List<ScriptSummary> listSummary, ScriptInfo info, int iStartLine, bool bSkipSubscripts, bool bForNote)
        {
            ScriptSummaryInfo ssi = new ScriptSummaryInfo();
            info.SummaryInfo = ssi;

            foreach (EOB2ScriptLine line in m_lines)
            {
                if (line.Number < iStartLine)
                    continue;

                listSummary.AddRange(line.Summary(info, bForNote));
            }
        }

        public override NoteInfo Summary(ScriptInfo info, bool bSkipSubscripts, bool bForNote = false)
        {
            ScriptSummaryInfo ssi = new ScriptSummaryInfo();
            info.SummaryInfo = ssi;

            List<string> list = new List<string>();
            foreach (ScriptLine line in Lines)
            {
                foreach (ScriptSummary summary in line.Summary(info, bForNote))
                {
                    if (summary.IsEmpty || line.CommandBytes == null || line.CommandBytes.Length < 1)
                        continue;
                    // some things aren't worth bothering to list in the summary
                    bool bSkip = false;
                    switch ((EOB2Opcode)line.CommandBytes[0])
                    {
                        case EOB2Opcode.Return:
                        case EOB2Opcode.EndCode:
                        case EOB2Opcode.Sound:
                        case EOB2Opcode.Jump:
                            bSkip = true;
                            break;
                    }
                    if (bSkip)
                        continue;
                    list.Add(summary.Description);
                }
            }

            string strSummary = "";
            if (bForNote)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string str in list)
                {
                    if (str.Trim().EndsWith(":"))
                        sb.Append(str);
                    else
                        sb.AppendFormat("{0}\r\n", str);
                }
                strSummary = sb.ToString().Trim();
            }
            else
            {
                // Collapse sequential duplicate sequences
                strSummary = Global.CollapseSequences(list);
            }
            return new NoteInfo(strSummary, "?", Color.Black);
        }

        private string NextText(byte[] bytes, ref int iCurrent)
        {
            int iStart = iCurrent;
            string str = null;
            while (iCurrent < bytes.Length && bytes[iCurrent] != 0)
                iCurrent++;
            str = Encoding.ASCII.GetString(bytes, iStart, iCurrent - iStart);
            while (iCurrent < bytes.Length && bytes[iCurrent] == 0)
                iCurrent++;
            return str;
        }

        private EOB2ScriptLine ReadNextLine(byte[] bytes, ref int iCurrentByte, int iStartAddress)
        {
            if (bytes == null || bytes.Length < 1 || iCurrentByte >= bytes.Length || bytes[iCurrentByte] < (int)EOB2Opcode.First)
                return null;

            int iScriptOffset = iCurrentByte;
            EOB2Opcode opcode = (EOB2Opcode)bytes[iCurrentByte++];
            byte[] command = new byte[] { (byte)opcode };
            string strText1 = null;
            string strText2 = null;
            string strText3 = null;
            Point ptTarget = Global.NullPoint;
            Point ptSource = Global.NullPoint;
            int jump = -1;
            int iTarget = -1;
            int iSource = -1;
            int iFacing = -1;
            int iSubCode = -1;
            DamageDice dice = null;
            switch (opcode)
            {
                case EOB2Opcode.Message:
                    iTarget = BitConverter.ToUInt16(bytes, iCurrentByte);
                    iCurrentByte += 4;
                    strText1 = "(unknown)";
                    if (iTarget < m_messages.Length)
                        strText1 = m_messages[iTarget];
                    break;
                case EOB2Opcode.ChangeLevel:
                    iTarget = BitConverter.ToUInt16(bytes, iCurrentByte + 1);
                    ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte + 3));
                    iFacing = bytes[iCurrentByte + 5];
                    iCurrentByte += 6;
                    break;
                case EOB2Opcode.CreateMonster:
                    iCurrentByte += 14;
                    break;
                case EOB2Opcode.TextMenu:
                    switch (bytes[iCurrentByte++])
                    {
                        case 0xD3:
                            iCurrentByte += 20;
                            break;
                        case 0xD8:
                            iCurrentByte += 8;
                            break;
                        case 0xF8:
                            iCurrentByte += 4;
                            break;
                    }
                    break;
                case EOB2Opcode.WindowPictures:
                case EOB2Opcode.UpdateScreen:
                    break;
                case EOB2Opcode.Wait:
                    switch (bytes[iCurrentByte])
                    {
                        case 1:
                        case 5:
                            ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte + 1));
                            iCurrentByte += 3;
                            break;
                        default:
                            iTarget = BitConverter.ToUInt16(bytes, iCurrentByte);
                            iCurrentByte += 2;
                            break;
                    }
                    break;
                case EOB2Opcode.SpecialEncounter:
                    iTarget = bytes[iCurrentByte++];
                    break;
                case EOB2Opcode.IdentifyAllItems:
                    ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte));
                    iCurrentByte += 2;
                    break;
                case EOB2Opcode.Turn:
                    iFacing = bytes[iCurrentByte++];
                    iTarget = bytes[iCurrentByte++];
                    break;
                case EOB2Opcode.Launcher:
                    iSubCode = bytes[iCurrentByte++];
                    iTarget = BitConverter.ToUInt16(bytes, iCurrentByte);
                    ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte + 2));
                    iCurrentByte += 4;
                    iFacing = bytes[iCurrentByte++];
                    iSource = bytes[iCurrentByte++];
                    break;
                case EOB2Opcode.NewItem:
                    iTarget = BitConverter.ToInt16(bytes, iCurrentByte);
                    int test = BitConverter.ToInt16(bytes, iCurrentByte + 2);
                    if (test == -2)
                    {
                        ptTarget = new Point(0, 0);
                        iFacing = bytes[iCurrentByte + 5];
                        iCurrentByte += 7;
                    }
                    else
                    {
                        ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte + 2));
                        iFacing = bytes[iCurrentByte + 4];
                        iCurrentByte += 6;
                    }
                    break;
                case EOB2Opcode.GiveExperience:
                    iTarget = BitConverter.ToInt16(bytes, iCurrentByte + 1);
                    iCurrentByte += 3;
                    break;
                case EOB2Opcode.ItemConsume:
                    if (bytes[iCurrentByte] == 0xFF)
                        iCurrentByte += 1;
                    else
                    {
                        ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte + 1));
                        iCurrentByte += 3;
                    }
                    break;
                case EOB2Opcode.Conditions:
                    iCurrentByte = EOB2ScriptLine.ConditionEnd(bytes, iCurrentByte);
                    if (iCurrentByte < bytes.Length - 1)
                        jump = BitConverter.ToInt16(bytes, iCurrentByte + 1);
                    iCurrentByte += 3;
                    break;
                case EOB2Opcode.Call:
                case EOB2Opcode.Jump:
                    jump = BitConverter.ToInt16(bytes, iCurrentByte);
                    iCurrentByte += 2;
                    break;
                case EOB2Opcode.Return:
                case EOB2Opcode.EndCode:
                    break;
                case EOB2Opcode.Damage:
                    dice = new DamageDice(bytes[iCurrentByte + 2], bytes[iCurrentByte + 1], bytes[iCurrentByte + 3]);
                    iCurrentByte += 4;
                    break;
                case EOB2Opcode.Heal:
                    break;
                case EOB2Opcode.Sound:
                    ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte + 1));
                    iCurrentByte += 3;
                    break;
                case EOB2Opcode.SetFlag:
                case EOB2Opcode.ClearFlag:
                    iSubCode = bytes[iCurrentByte++];
                    iTarget = bytes[iCurrentByte++];
                    if (iSubCode == 0xF3)
                        iCurrentByte++;
                    iSource = iSubCode;
                    break;
                case EOB2Opcode.StealSmallItem:
                    iTarget = bytes[iCurrentByte++];
                    ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte));
                    iCurrentByte += 2;
                    iSource = bytes[iCurrentByte++];
                    break;
                case EOB2Opcode.Teleport:
                    ptSource = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte + 1));
                    ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte + 3));
                    iCurrentByte += 5;
                    break;
                case EOB2Opcode.CloseDoor:
                case EOB2Opcode.OpenDoor:
                    ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte));
                    iCurrentByte += 2;
                    break;
                case EOB2Opcode.ChangeWall:
                    ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte + 1));
                    switch (bytes[iCurrentByte])
                    {
                        case 0xF7:
                            iSource = bytes[iCurrentByte + 3];
                            iTarget = bytes[iCurrentByte + 4];
                            iCurrentByte += 5;
                            break;
                        case 0xE9:
                            iFacing = bytes[iCurrentByte + 3];
                            iTarget = bytes[iCurrentByte + 4];
                            iCurrentByte += 6;
                            break;
                        default:
                            iCurrentByte += 3;
                            break;
                    }
                    break;
                case EOB2Opcode.SetWall:
                    iSubCode = bytes[iCurrentByte++];
                    if (iSubCode == 0xED)
                    {
                        iFacing = bytes[iCurrentByte++];
                    }
                    else
                    {
                        ptTarget = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, iCurrentByte));
                        if (iSubCode == 0xF7)
                            iCurrentByte += 2;
                        else
                        {
                            iFacing = bytes[iCurrentByte + 2];
                            iCurrentByte += 3;
                        }
                        iTarget = bytes[iCurrentByte++];
                    }
                    break;
                default:
                    // For now, read until the next possible opcode (this will need to be changed since opcodes aren't reserved values)
                    while (iCurrentByte < bytes.Length && bytes[iCurrentByte] < (int)EOB2Opcode.First)
                        iCurrentByte++;
                    break;
            }

            command = Global.Subset(bytes, iScriptOffset, iCurrentByte - iScriptOffset);
            Bytes = new MemoryBytes(command, iScriptOffset);

            EOB2ScriptLine line = new EOB2ScriptLine(MemoryOffset, iScriptOffset - iStartAddress, Location, command);
            line.Text = strText1;
            line.Text2 = strText2;
            line.Text3 = strText3;
            line.SourcePoint = ptSource;
            line.TargetPoint = ptTarget;
            line.Jump = jump;
            line.SourceValue = iSource;
            line.TargetValue = iTarget;
            line.Direction = iFacing;
            line.Dice = dice;
            return line;
        }
    }

    public class EOB2Scripts : GameScripts, IScriptAddressLocator
    {
        private int OriginalOffset = 0;
        private byte[] RawBytes;

        public override bool HasHeaderBytes { get { return true; } }

        public Dictionary<int, List<Point>> Addresses;

        private void InitAddresses(MapBytes mb)
        {
            Addresses = new Dictionary<int, List<Point>>();
            if (mb == null || mb.Bytes == null)
                return;

            for (int i = 0; i < mb.Bytes.Length - (EOB2MapData.BytesPerSquare - 1); i += EOB2MapData.BytesPerSquare)
            {
                int iAddress = BitConverter.ToUInt16(mb.Bytes, i + 8);
                if (iAddress != 0)
                {
                    Point pt = new Point((i / EOB2MapData.BytesPerSquare) % 32, i / (EOB2MapData.BytesPerSquare * 32));
                    if (!Addresses.ContainsKey(iAddress))
                        Addresses.Add(iAddress, new List<Point>());
                    Addresses[iAddress].Add(pt);
                }
            }
        }

        public Point GetLocation(int iAddress)
        {
            if (Addresses.ContainsKey(iAddress))
                return Addresses[iAddress][0];
            return Point.Empty;
        }

        public EOB2Scripts(MapBytes mapBytes, byte[] bytes, int iOffset = 0, int iOriginalOffset = 0, int iMemoryOffset = -1)
        {
            InitAddresses(mapBytes);
            if (bytes == null || iOffset >= bytes.Length)
                return;
            OriginalOffset = iOriginalOffset;
            RawBytes = bytes;
            Scripts = new Dictionary<Point, List<GameScript>>();

            // Skip past everything until "FF FF FF FF"
            int iStart = Global.FindFirstBytes(bytes, new byte[] { 0xff, 0xff, 0xff, 0xff });

            if (iStart == -1)
                return;     // Couldn't find the end of the map/monster items

            iStart += (4 + 8 + (30 * 14));   // The "FF FF FF FF" ending, 8 misc values, and the 30 initial monster positions
            int iScriptLength = BitConverter.ToUInt16(bytes, iStart);
            iStart += 2;
            int iScriptStart = iStart;
            int iScriptCount = 0;

            List<string> messages = new List<string>();
            string sMsg = null;
            int iMsg = iStart + iScriptLength - 2;
            do
            {
                if (bytes[iMsg] < ' ' || bytes[iMsg] > 127)
                    break;
                sMsg = Global.GetNullTerminatedString(bytes, iMsg, bytes.Length - iMsg, false);
                if (sMsg != null)
                {
                    messages.Add(sMsg);
                    iMsg += (sMsg.Length + 1);
                }
                else
                    break;
            } while (sMsg != null);
            string[] msgArray = messages.ToArray();
            int iOrigin = OriginalOffset + iStart - 2;      // The beginning of the entire script block, used by "jump" and related commands

            while (iStart < bytes.Length - 1 && iStart < (iScriptStart + iScriptLength) && bytes[iStart] != 0)
            {
                EOB2Script script = new EOB2Script(this, bytes, OriginalOffset + iStart, iOrigin, msgArray);
                if (script == null || script.Bytes == null || script.Bytes.Length == 0)
                    break;
                script.Index = iScriptCount++;
                if (!Scripts.ContainsKey(script.Location))
                    Scripts.Add(script.Location, new List<GameScript>(1));
                Scripts[script.Location].Add(script);
                iStart += script.Bytes.Length;
            }
        }
    }
}
