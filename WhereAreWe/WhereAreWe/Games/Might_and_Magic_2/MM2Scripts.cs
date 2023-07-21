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
    public enum MM2ScriptCommand
    {
        ExitScript00 = 0,               // 0 args
        DisplayHeader = 1,              // 1 arg (local string index)
        DisplayMain = 2,                // 1 arg (local string index)
        DisplayEntire = 3,              // 1 arg (local string index)
        DisplayTopWall = 4,             // 1 arg (local string index)
        DisplayWall = 5,                // 1 arg (local string index)
        DisplaySign = 6,                // 1 arg (local string index)
        SpaceToContinue = 7,            // 0 args
        SpaceToContinueAnim = 8,        // 0 args
        ReadKey = 9,                    // 0 args
        ReadKeyAnimation = 10,          // 0 args
        ShowAnimation = 11,             // 2 args
        Teleport = 12,                  // 2 args (map, packed location)
        PlaySound = 13,                 // 1 arg
        RunExternalScript = 14,         // 1 arg
        ExitScriptRemoveAnim = 15,      // 0 args
        JumpIfSucceeded = 16,           // 1 arg (number of lines to skip)
        JumpIfFailed = 17,              // 1 arg (number of lines to skip)
        EncounterFull = 18,             // 12 args
        Encounter = 19,                 // 10 args
        ClearScriptActive = 20,         // 0 args
        GetCharDataBits = 21,           // 3 args (character, index, mask)
        CheckPartyItem = 22,            // 2 args (?, item index)
        CheckPartyByte = 23,            // 2 args
        WriteCharDataBits = 24,         // 4 args (character, index, mask, write value)
        AddItem = 25,                   // 4 args
        SetPartyByte = 26,              // 2 args
        TestGreaterOrEqual = 27,        // 1 arg
        GetRandom = 28,                 // 1 arg
        Unknown29 = 29,                 // 1 arg
        Pause = 30,                     // 1 arg
        AddCharDataValue = 31,          // 6 args
        SubCharDataValue = 32,          // 6 args
        SetMapSquare = 33,              // 3 args
        CheckEra = 34,                  // 2 args
        CheckDay = 35,                  // 2 args
        SubtractTotalGold = 36,         // 2 args
        SubtractTotalGems = 37,         // 2 args
        ReadKeySelectChar = 38,         // 0 args
        ReadKeySelectCharAnim = 39,     // 0 args
        RemoveItem = 40,                // 2 args
        ExitScript29 = 41,              // 0 args
        Treasure = 42,                  // 14 args
        SetEncounterLine = 43,          // 1 arg
        AddCurrentYear = 44,            // 1 arg
        CheckRaceClass = 45,            // 2 args
        SetAllCharSingleBit = 46,       // 2 args
        ReadString = 47,                // 2 args
        CompareText = 48,               // 10 args
        ElectricalDamage = 49,          // 3 args
        CheckAnyCharHasSkill = 50,      // 1 arg
        EndOfScript = 255,              // 0 args
        Invalid = -1
    }

    public enum MM2ScriptCharData
    {
        Invalid = -1,
        RosterPosition = 0,
        HirelingFlag = 1,
        Name_0 = 2,             // Character[0]
        Name_1 = 3,             // Character[1]
        Name_2 = 4,             // Character[2]
        Name_3 = 5,             // Character[3]
        Name_4 = 6,             // Character[4]
        Name_5 = 7,             // Character[5]
        Name_6 = 8,             // Character[6]
        Name_7 = 9,             // Character[7]
        Name_8 = 10,            // Character[8]
        Name_9 = 11,            // Character[9]
        Sex = 12,               // Character[12]
        AlignmentPerm = 13,     // Character[13]
        Race = 14,              // Character[14]
        Class = 15,             // Character[15]
        MightPerm = 16,         // Character[16]
        IntellectPerm = 17,     // Character[17]
        PersonalityPerm = 18,   // Character[18]
        SpeedPerm = 19,         // Character[19]
        AccuracyPerm = 20,      // Character[20]
        LuckPerm = 21,          // Character[21]
        MagicResist = 22,       // Character[22]
        FireResist = 23,        // Character[23]
        ElecResist = 24,        // Character[24]
        ColdResist = 25,        // Character[25]
        EnergyResist = 26,      // Character[26]
        SleepResist = 27,       // Character[27]
        PoisonResist = 28,      // Character[28]
        AcidResist = 29,        // Character[29]
        Thievery = 30,          // Character[30]
        ACPerm = 31,            // Character[31]
        HPTempMax_0 = 32,       // Character[116]
        HPTempMax_1 = 33,       // Character[117]
        MightTemp = 34,         // Character[107]
        SpeedTemp = 35,         // Character[110]
        AccuracyTemp = 36,      // Character[111]
        AlignmentTemp = 37,     // Character[106]
        LevelTemp = 38,         // Character[113]
        SpellLevelTemp = 39,    // Character[114]
        SPCurrent_0 = 40,       // Character[88]
        SPCurrent_1 = 41,       // Character[89]
        EnduranceTemp = 42,     // Character[115]
        IntellectTemp = 43,     // Character[108]
        PersonalityTemp = 44,   // Character[109]
        LuckTemp = 45,          // Character[112]
        LuckPerm46 = 46,        // Character[21]
        Age = 47,               // Character[33]
        AgeDays = 48,           // Character[34]
        Experience_0 = 49,      // Character[98]
        Experience_1 = 50,      // Character[99]
        Experience_2 = 51,      // Character[100]
        Experience_3 = 52,      // Character[101]
        SPMax_0 = 53,           // Character[91]
        SPMax_1 = 54,           // Character[90]
        SpellLevelPerm = 55,    // Character[35]
        Gems_0 = 56,            // Character[92]
        Gems_1 = 57,            // Character[93]
        HPCurrent_0 = 58,       // Character[94]
        HPCurrent_1 = 59,       // Character[95]
        HPMax_0 = 60,           // Character[96]
        HPMax_1 = 61,           // Character[97]
        Gold_0 = 62,            // Character[102]
        Gold_1 = 63,            // Character[103]
        Gold_2 = 64,            // Character[104]
        ACTotal = 65,           // Character[36]
        Food = 66,              // Character[37]
        Condition = 67,         // Character[38]
        EndurancePerm = 68,     // Character[39]
        EquipItem1 = 69,        // Character[40]
        EquipItem2 = 70,        // Character[41]
        EquipItem3 = 71,        // Character[42]
        EquipItem4 = 72,        // Character[43]
        EquipItem5 = 73,        // Character[44]
        EquipItem6 = 74,        // Character[45]
        BackpackItem1 = 75,     // Character[58]
        BackpackItem2 = 76,     // Character[59]
        BackpackItem3 = 77,     // Character[60]
        BackpackItem4 = 78,     // Character[61]
        BackpackItem5 = 79,     // Character[62]
        BackpackItem6 = 80,     // Character[63]
        EquipCharges1 = 81,     // Character[46]
        EquipCharges2 = 82,     // Character[47]
        EquipCharges3 = 83,     // Character[48]
        EquipCharges4 = 84,     // Character[49]
        EquipCharges5 = 85,     // Character[50]
        EquipCharges6 = 86,     // Character[51]
        BackpackCharges1 = 87,  // Character[64]
        BackpackCharges2 = 88,  // Character[65]
        BackpackCharges3 = 89,  // Character[66]
        BackpackCharges4 = 90,  // Character[67]
        BackpackCharges5 = 91,  // Character[68]
        BackpackCharges6 = 92,  // Character[69]
        EquipBonus1 = 93,       // Character[52]
        EquipBonus2 = 94,       // Character[53]
        EquipBonus3 = 95,       // Character[54]
        EquipBonus4 = 96,       // Character[55]
        EquipBonus5 = 97,       // Character[56]
        EquipBonus6 = 98,       // Character[57]
        BackpackBonus1 = 99,    // Character[70]
        BackpackBonus2 = 100,   // Character[71]
        BackpackBonus3 = 101,   // Character[72]
        BackpackBonus4 = 102,   // Character[73]
        BackpackBonus5 = 103,   // Character[65]
        BackpackBonus6 = 104,   // Character[75]
        MeleeOrdinary = 105,    // Character[76]
        MeleeMagical = 106,     // Character[77]
        RangedOrdinary = 107,   // Character[78]
        RangedMagical = 108,    // Character[79]
        SecondarySkills = 109,  // Character[80]
        KnownSpells_0 = 110,    // Character[81]
        KnownSpells_1 = 111,    // Character[82]
        KnownSpells_2 = 112,    // Character[83]
        KnownSpells_3 = 113,    // Character[84]
        KnownSpells_4 = 114,    // Character[85]
        KnownSpells_5 = 115,    // Character[86]
        GuildFlags = 116,       // Character[121]
        AdvancementFlags = 117, // Character[122]
        QuestFlags1_2 = 118,    // Character[123]
        MealsEaten_1 = 119,     // Character[118]
        MealsEaten_0 = 120,     // Character[119]
        QuestObject = 121,      // Character[120]
        QuestFlags1_1 = 122,    // Character[124]
        QuestFlags1_0 = 123,    // Character[125]
        ArenaFlags_1 = 124,     // Character[126]
        ArenaFlags_0 = 125,     // Character[127]
        QuestFlags2_1 = 126,    // Character[128]
        QuestFlags2_0 = 127,    // Character[129]
        Last = 255,
        FlagsMask = 0x7f
    }

    public enum MM2ExtScript
    {
        Invalid = -1,
        None = 0,
        Inn = 1,
        Training = 2,
        Tavern = 3,
        Temple = 4,
        MageGuild = 5,
        Blacksmith = 6,
        Arena = 8,
    }

    public class MM2Scripts : GameScripts
    {
        public List<MemoryBytes> Headers;
        public bool ScriptsOnly;

        public MM2Scripts(MemoryBytes bytes)
        {
            int iHeader = 0;
            Headers = new List<MemoryBytes>();

            byte[] bytesNulls = new byte[] { 0, 0, 0 };

            int[] knownLengths = new int[]  { 0xff, 0x1ef, 0x1b8, 0x1a6, 0xe3, 0x50, 0x128, 0x12b, 0xf8, 0x21d, 0x158 };
            int[] scriptIndices = new int[] { 9, 17, 56, 76, 86, 92, 101, 106, 151, 227, 144 };

            byte[] bytesFFs = new byte[] { 0xff, 0xff };

            bool bCustomScripts = false;
            int iCustomIndex = 0;
            if (bytes != null && bytes.Length > 3 && bytes[2] == 0xff)
            {
                int iLength = BitConverter.ToUInt16(bytes, 0);
                if (iLength > 0xe00)    // Custom-made concatenated script file
                {
                    bCustomScripts = true;
                    iCustomIndex = 9;
                }
                else
                {
                    for (int i = 0; i < knownLengths.Length; i++)
                    {
                        if (iLength == knownLengths[i])
                        {
                            bCustomScripts = true;
                            iCustomIndex = scriptIndices[i];
                            break;
                        }
                    }
                }
            }

            if (bytes == null)
                return;

            if (!bCustomScripts)
            {
                ScriptsOnly = false;
                while (!Global.CompareBytes(bytes, bytesNulls, iHeader, 0, bytesNulls.Length))
                {
                    Headers.Add(bytes.GetRange(iHeader, 3));
                    iHeader += 3;
                    if (iHeader > bytes.Length - 2)
                        return;
                }

                MM2ScriptBytes scriptBytes = new MM2ScriptBytes(bytes, iHeader+3);

                foreach(MemoryBytes header in Headers)
                {
                    MM2Script script = new MM2Script(header, scriptBytes);

                    if (!Scripts.ContainsKey(script.Location))
                        Scripts.Add(script.Location, new List<GameScript>(1));
                    Scripts[script.Location].Add(script);
                }
            }
            else
            {
                // No headers, just an array of scripts
                ScriptsOnly = true;
                MM2ScriptBytes scriptBytes = new MM2ScriptBytes(bytes, 3);

                for (int iIndex = 0; iIndex < scriptBytes.Offsets.Count; iIndex++)
                {
                    MemoryBytes mbHeader = new MemoryBytes(new byte[] { 0, (byte)iIndex, 0 }, -1);
                    if (mbHeader != null)
                    {
                        MM2Script script = new MM2Script(mbHeader, scriptBytes, iCustomIndex);

                        if (!Scripts.ContainsKey(script.Location))
                            Scripts.Add(script.Location, new List<GameScript>(1));
                        Scripts[script.Location].Add(script);
                    }
                }
            }
        }

        public override bool IsMainList { get { return !ScriptsOnly; } }

    }

    public class MM2Script : MMScript
    {
        public MemoryBytes Header;
        private List<ScriptLine> m_lines;

        public MM2Script(MemoryBytes header, MM2ScriptBytes scriptBytes, int iIndexOffset = 0)
        {
            m_lines = null;
            Header = header;
            Bytes = null;
            Index = -1;

            if (header == null || header.Length < 3)
                return;

            if (header[1] > scriptBytes.Offsets.Count-1)
                return;

            Index = header[1] + iIndexOffset;

            if (header[1] >= scriptBytes.Offsets.Count)
                return;

            Bytes = scriptBytes.Bytes.GetRange(scriptBytes.Offsets[header[1]]);

            Location = new Point(header[0] & 0xf, header[0] >> 4);
            Facing = FacingFromByte(header[2]);
        }

        public static DirectionFlags FacingFromByte(byte b)
        {
            DirectionFlags dir = DirectionFlags.None;

            if ((b & 0x10) > 0)
                dir |= DirectionFlags.West;
            if ((b & 0x20) > 0)
                dir |= DirectionFlags.South;
            if ((b & 0x40) > 0)
                dir |= DirectionFlags.East;
            if ((b & 0x80) > 0)
                dir |= DirectionFlags.North;

            return dir;
        }

        public static int NumArgs(MM2ScriptCommand cmd)
        {
            switch (cmd)
            {
                case MM2ScriptCommand.ExitScript00:
                case MM2ScriptCommand.SpaceToContinue:
                case MM2ScriptCommand.SpaceToContinueAnim:
                case MM2ScriptCommand.ReadKey:
                case MM2ScriptCommand.ReadKeyAnimation:
                case MM2ScriptCommand.ExitScriptRemoveAnim:
                case MM2ScriptCommand.ReadKeySelectChar:
                case MM2ScriptCommand.ReadKeySelectCharAnim:
                case MM2ScriptCommand.ExitScript29:
                case MM2ScriptCommand.EndOfScript:
                case MM2ScriptCommand.ClearScriptActive:
                case MM2ScriptCommand.ReadString:
                    return 0;
                case MM2ScriptCommand.DisplayHeader:
                case MM2ScriptCommand.DisplayMain:
                case MM2ScriptCommand.DisplayTopWall:
                case MM2ScriptCommand.DisplayWall:
                case MM2ScriptCommand.DisplaySign:
                case MM2ScriptCommand.DisplayEntire:
                case MM2ScriptCommand.RunExternalScript:
                case MM2ScriptCommand.JumpIfSucceeded:
                case MM2ScriptCommand.JumpIfFailed:
                case MM2ScriptCommand.SetEncounterLine:
                case MM2ScriptCommand.AddCurrentYear:
                case MM2ScriptCommand.PlaySound:
                case MM2ScriptCommand.CheckAnyCharHasSkill:
                case MM2ScriptCommand.TestGreaterOrEqual:
                case MM2ScriptCommand.GetRandom:
                case MM2ScriptCommand.Unknown29:
                case MM2ScriptCommand.Pause:
                    return 1;
                case MM2ScriptCommand.Teleport:
                case MM2ScriptCommand.SubtractTotalGems:
                case MM2ScriptCommand.SubtractTotalGold:
                case MM2ScriptCommand.SetPartyByte:
                case MM2ScriptCommand.CheckPartyByte:
                case MM2ScriptCommand.RemoveItem:
                case MM2ScriptCommand.SetAllCharSingleBit:
                case MM2ScriptCommand.ShowAnimation:
                case MM2ScriptCommand.CheckRaceClass:
                case MM2ScriptCommand.CheckPartyItem:
                case MM2ScriptCommand.CheckEra:
                case MM2ScriptCommand.CheckDay:
                    return 2;
                case MM2ScriptCommand.ElectricalDamage:
                case MM2ScriptCommand.GetCharDataBits:
                case MM2ScriptCommand.SetMapSquare:
                    return 3;
                case MM2ScriptCommand.WriteCharDataBits:
                case MM2ScriptCommand.AddItem:
                    return 4;
                case MM2ScriptCommand.AddCharDataValue:
                case MM2ScriptCommand.SubCharDataValue:
                    return 6;
                case MM2ScriptCommand.CompareText:
                case MM2ScriptCommand.Encounter:
                    return 10;
                case MM2ScriptCommand.EncounterFull:
                    return 12;
                case MM2ScriptCommand.Treasure:
                    return 14;
                default:
                    return 0;
            }
        }

        public override List<ScriptLine> Lines
        {
            get
            {
                if (m_lines != null)
                    return m_lines;

                InitLines();

                return m_lines;
            }
        }

        private void InitLines()
        {
            m_lines = new List<ScriptLine>();

            if (Bytes == null)
                return;

            int i = 0;
            int iNumber = 0;
            while (i < Bytes.Length)
            {
                int iLength = NumArgs((MM2ScriptCommand)Bytes.Bytes[i]) + 1;
                if (iLength + i <= Bytes.Length)
                {
                    m_lines.Add(new MM2ScriptLine(Bytes, i, iLength, iNumber++));
                    i += iLength;
                }
                else
                    break;
            }
        }

        public override void Summary(int iDepth, List<ScriptSummary> listSummary, ScriptInfo info, int iStartLine, bool bSkipSubscripts, bool bForNote)
        {
            foreach (MM2ScriptLine line in m_lines)
            {
                if (line.Number < iStartLine)
                    continue;

                listSummary.AddRange(line.Summary(info as MMScriptInfo, bForNote));
            }
        }
    }

    public struct ScriptBlock
    {
        public int Start;
        public int Length;

        public ScriptBlock(int start, int len)
        {
            Start = start;
            Length = len;
        }

        public int End { get { return Start + Length; } }
    }

    public class MM2ScriptBytes
    {
        public MemoryBytes Bytes;
        public List<ScriptBlock> Offsets;

        public MM2ScriptBytes(MemoryBytes bytes, int offset)
        {
            Bytes = bytes;

            Offsets = new List<ScriptBlock>();

            int i = offset;
            int iStart = offset;
            while (i < bytes.Length-1)
            {
                switch (bytes[i])
                {
                    // 0xff doesn't appear to be permitted inside scripts as a number, so this simplistic approach should work
                    // Places that would logically use "set value to 0xff" instead use "set value to 0xfe with bitmask 0xfe"
                    // followed by "set value to 0x01 with bitmask 0x01"
                    case 0xff:
                        Offsets.Add(new ScriptBlock(iStart, i - iStart));
                        if (bytes[i+1] == 0xff)
                            return;
                        iStart = i+1;
                        break;
                    default:
                        break;
                }
                i++;
            }
        }
    }

    public class MM2ScriptLine : MMScriptLine
    {
        public MM2ScriptCommand Command
        {
            get
            {
                if (Bytes == null || Bytes.Length < 1)
                    return MM2ScriptCommand.Invalid;
                return (MM2ScriptCommand)Bytes[0];
            }
        }

        public MM2ScriptLine(MemoryBytes bytes, int offset, int length, int lineNumber)
        {
            Number = lineNumber;
            Bytes = bytes.GetRange(offset, length);
        }

        public override bool IsTeleportCommand
        {
            get
            {
                switch (Command)
                {
                    case MM2ScriptCommand.Teleport:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override bool IsEncounterCommand
        {
            get
            {
                switch (Command)
                {
                    case MM2ScriptCommand.Encounter:
                    case MM2ScriptCommand.EncounterFull:
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
                    case MM2ScriptCommand.DisplayHeader:
                    case MM2ScriptCommand.DisplayMain:
                    case MM2ScriptCommand.DisplaySign:
                    case MM2ScriptCommand.DisplayTopWall:
                    case MM2ScriptCommand.DisplayWall:
                    case MM2ScriptCommand.DisplayEntire:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override Point TeleportLocation
        {
            get
            {
                if (Command != MM2ScriptCommand.Teleport)
                    return Global.NullPoint;

                return new Point(Bytes[2] & 0xf, Bytes[2] >> 4);
            }
        }

        public override int TeleportMapIndex
        {
            get
            {
                if (Command != MM2ScriptCommand.Teleport)
                    return -1;

                return Bytes[1];
            }
        }

        private ScriptSummary SummaryString(List<ScriptString> strings)
        {
            byte[] args = Bytes.GetRange(1);

            int iExpectedArgs = MM2Script.NumArgs(Command);
            int iHaveArgs = (args == null ? 0 : args.Length);

            if (iHaveArgs < iExpectedArgs)
                return new ScriptSummary("BadArgs");

            switch (Command)
            {
                case MM2ScriptCommand.DisplayMain:
                case MM2ScriptCommand.DisplaySign:
                case MM2ScriptCommand.DisplayTopWall:
                case MM2ScriptCommand.DisplayWall:
                case MM2ScriptCommand.DisplayEntire:
                case MM2ScriptCommand.DisplayHeader:
                    return new ScriptSummary(MapString(args[0] - 1, strings));
                case MM2ScriptCommand.EndOfScript: return new ScriptSummary("End");
                case MM2ScriptCommand.ExitScriptRemoveAnim:
                case MM2ScriptCommand.ExitScript29:
                case MM2ScriptCommand.ExitScript00: return new ScriptSummary("Exit");
                case MM2ScriptCommand.Invalid: return new ScriptSummary("Invalid");
                case MM2ScriptCommand.JumpIfFailed:
                case MM2ScriptCommand.JumpIfSucceeded: return new ScriptSummary(String.Format("Jump +{0}", args[0]));
                case MM2ScriptCommand.GetCharDataBits: return new ScriptSummary(String.Format("Check {0}", MM2CmdDesc.CharRecordStringAbbr((MM2ScriptCharData)args[1])));
                case MM2ScriptCommand.CheckPartyItem: return new ScriptSummary(String.Format("Item {0}?", args[1]));
                case MM2ScriptCommand.WriteCharDataBits: return new ScriptSummary(String.Format("Set {0}={1}", MM2CmdDesc.CharRecordStringAbbr((MM2ScriptCharData)args[1]), args[3]));
                case MM2ScriptCommand.SetPartyByte: return new ScriptSummary(String.Format("PByte{0}={1}", args[0], args[1]));
                case MM2ScriptCommand.CheckPartyByte: return new ScriptSummary(String.Format("Check PByte{0}={1}", args[0], args[1]));
                case MM2ScriptCommand.ReadKey:
                case MM2ScriptCommand.ReadKeyAnimation: return new ScriptSummary("ReadYN");
                case MM2ScriptCommand.ReadKeySelectCharAnim:
                case MM2ScriptCommand.ReadKeySelectChar: return new ScriptSummary("SelectChar");
                case MM2ScriptCommand.SpaceToContinue: return new ScriptSummary("PressSpace");
                case MM2ScriptCommand.SpaceToContinueAnim: return new ScriptSummary("PressSpace");
                case MM2ScriptCommand.RunExternalScript: return new ScriptSummary(String.Format("Script {0}", args[0]));
                case MM2ScriptCommand.Teleport: return new ScriptSummary(MM2CmdDesc.Teleport(args, true));
                case MM2ScriptCommand.SubtractTotalGold: return new ScriptSummary(String.Format("-{0} Gold", BitConverter.ToUInt16(args, 0)));
                case MM2ScriptCommand.SubtractTotalGems: return new ScriptSummary(String.Format("-{0} Gems", BitConverter.ToUInt16(args, 0)));
                case MM2ScriptCommand.RemoveItem: return new ScriptSummary(String.Format("-Item {0}", args[1]));
                case MM2ScriptCommand.AddItem: return new ScriptSummary(String.Format("+Item {0}", args[1]));
                case MM2ScriptCommand.Treasure: return new ScriptSummary(MM2CmdDesc.TreasureAbbr(args));
                case MM2ScriptCommand.SetAllCharSingleBit: return new ScriptSummary(String.Format("SetBit {0}", (MM2ScriptCharData)args[0]));
                case MM2ScriptCommand.AddCharDataValue: return new ScriptSummary(String.Format("{0}+{1}", MM2CmdDesc.CharRecordStringAbbr((MM2ScriptCharData)args[1]), new UInt24(args, 3)));
                case MM2ScriptCommand.SubCharDataValue: return new ScriptSummary(String.Format("{0}-{1}", MM2CmdDesc.CharRecordStringAbbr((MM2ScriptCharData)args[1]), new UInt24(args, 3)));
                case MM2ScriptCommand.ShowAnimation: return new ScriptSummary(String.Format("Anim {0}", args[0]));
                case MM2ScriptCommand.Encounter: return new ScriptSummary(Global.AllNull(args) ? "RandEncounter" : "FixedEncounter");
                case MM2ScriptCommand.EncounterFull: return new ScriptSummary(Global.AllNull(args) ? "RandEncounter" : "FixedEncounter");
                case MM2ScriptCommand.ClearScriptActive: return new ScriptSummary("ClearScript");
                case MM2ScriptCommand.AddCurrentYear: return new ScriptSummary(String.Format("+{0} Year", args[0]));
                case MM2ScriptCommand.TestGreaterOrEqual: return new ScriptSummary(String.Format("Test >= {0}", args[0]));
                case MM2ScriptCommand.GetRandom: return new ScriptSummary(String.Format("Random({0})", args[0]));
                case MM2ScriptCommand.Pause: return new ScriptSummary(String.Format("Pause({0})", args[0]));
                case MM2ScriptCommand.ReadString: return new ScriptSummary("ReadString");
                case MM2ScriptCommand.CompareText: return new ScriptSummary(String.Format("Comp({0})", MM2CmdDesc.CompareString(args)));
                case MM2ScriptCommand.ElectricalDamage: return new ScriptSummary(String.Format("-{0} Electric", args[0] > 0x7f ? "Prev" : BitConverter.ToUInt16(args, 1).ToString()));
                case MM2ScriptCommand.CheckAnyCharHasSkill: return new ScriptSummary(String.Format("{0}?", MM2Character.SecondarySkillName((MM2SecondarySkill)args[0])));
                case MM2ScriptCommand.CheckRaceClass: return new ScriptSummary(MM2CmdDesc.CheckRaceClass(args, true));
                case MM2ScriptCommand.SetMapSquare: return new ScriptSummary(String.Format("SetMap({0},{1})", args[0] & 0xf, args[0] >> 4));
                case MM2ScriptCommand.CheckEra: return new ScriptSummary(String.Format("Era({0}-{1})?", args[0], args[1]));
                case MM2ScriptCommand.CheckDay: return new ScriptSummary(String.Format("Day({0}-{1})?", args[0], args[1]));
                case MM2ScriptCommand.SetEncounterLine: return new ScriptSummary(String.Format("EncLine(+{0})", args[0]));
                case MM2ScriptCommand.PlaySound: return new ScriptSummary(String.Format("Sound({0})", args[0]));
                default: return new ScriptSummary("?");
            }
        }

        public override List<ScriptSummary> Summary(ScriptInfo info, bool bForNote)
        {
            List<ScriptSummary> list = new List<ScriptSummary>();

            if (info.Strings == null || Bytes == null)
                return list;
            if (Bytes.Length < 1)
                return list;

            list.Add(SummaryString(info.Strings));

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

        public override string Description(ScriptInfo info, string strLinePrefix)
        {
            if (Bytes == null || Bytes.Length < 1)
                return "<null>";

            byte bCommand = Bytes[0];
            MemoryBytes args = Bytes.GetRange(1);

            string strUnknown = String.Format("Unknown({0})", Global.ByteString(Bytes));

            switch ((MM2ScriptCommand)bCommand)
            {
                case MM2ScriptCommand.DisplayHeader: return MM2CmdDesc.DisplayLocal("Display, Header", info.Strings, args);
                case MM2ScriptCommand.DisplayMain: return MM2CmdDesc.DisplayLocal("Display, Main", info.Strings, args);
                case MM2ScriptCommand.DisplaySign: return MM2CmdDesc.DisplayLocal("Display, Sign", info.Strings, args);
                case MM2ScriptCommand.DisplayTopWall: return MM2CmdDesc.DisplayLocal("Display, TopWall", info.Strings, args);
                case MM2ScriptCommand.DisplayWall: return MM2CmdDesc.DisplayLocal("Display, Wall", info.Strings, args);
                case MM2ScriptCommand.DisplayEntire: return MM2CmdDesc.DisplayLocal("Display, Full", info.Strings, args);
                case MM2ScriptCommand.EndOfScript: return "End of Script";
                case MM2ScriptCommand.ExitScriptRemoveAnim: return "Exit Script (remove animation)";
                case MM2ScriptCommand.ExitScript29:
                case MM2ScriptCommand.ExitScript00: return "Exit Script";
                case MM2ScriptCommand.Invalid: return "Invalid";
                case MM2ScriptCommand.JumpIfFailed: return MM2CmdDesc.Jump("Jump if Failed", Number, args);
                case MM2ScriptCommand.JumpIfSucceeded: return MM2CmdDesc.Jump("Jump if Succeeded", Number, args);
                case MM2ScriptCommand.GetCharDataBits: return MM2CmdDesc.GetCharDataBits(args);
                case MM2ScriptCommand.WriteCharDataBits: return MM2CmdDesc.WriteCharDataBits(args);
                case MM2ScriptCommand.SetPartyByte: return MM2CmdDesc.SetPartyByte(args);
                case MM2ScriptCommand.ReadKey: return "Read Y or N";
                case MM2ScriptCommand.ReadKeyAnimation: return "Read Y or N (allow animation)";
                case MM2ScriptCommand.ReadKeySelectChar: return "Select Character, 1-8";
                case MM2ScriptCommand.ReadKeySelectCharAnim: return "Select Character, 1-8 (allow animation)";
                case MM2ScriptCommand.SpaceToContinue: return "Space to Continue";
                case MM2ScriptCommand.SpaceToContinueAnim: return "Space to Continue (allow animation)";
                case MM2ScriptCommand.RunExternalScript: return MM2CmdDesc.RunExternalScript(args);
                case MM2ScriptCommand.Teleport: return MM2CmdDesc.Teleport(args);
                case MM2ScriptCommand.SubtractTotalGold: return MM2CmdDesc.ModifyTotal("Subtract", "Gold", args);
                case MM2ScriptCommand.SubtractTotalGems: return MM2CmdDesc.ModifyTotal("Subtract", "Gems", args);
                case MM2ScriptCommand.RemoveItem: return MM2CmdDesc.ItemRemove(args);
                case MM2ScriptCommand.AddItem: return MM2CmdDesc.ItemAdd(args);      // Note: MM2 places the item in the "search" treasure if backpacks are full
                case MM2ScriptCommand.Treasure: return MM2CmdDesc.Treasure(args);
                case MM2ScriptCommand.SetAllCharSingleBit: return MM2CmdDesc.SetAllCharSingleBit(args);
                case MM2ScriptCommand.AddCharDataValue: return MM2CmdDesc.AddCharDataValue(args);
                case MM2ScriptCommand.SubCharDataValue: return MM2CmdDesc.SubCharDataValue(args);
                case MM2ScriptCommand.ShowAnimation: return MM2CmdDesc.ShowAnimation(args);
                case MM2ScriptCommand.EncounterFull: return MM2CmdDesc.Encounter(args);
                case MM2ScriptCommand.Encounter: return MM2CmdDesc.Encounter(args);
                case MM2ScriptCommand.ClearScriptActive: return "Clear Script Active Bit";
                case MM2ScriptCommand.CheckPartyByte: return MM2CmdDesc.CheckPartyByte(args);
                case MM2ScriptCommand.AddCurrentYear: return MM2CmdDesc.AddCurrentYear(args);
                case MM2ScriptCommand.TestGreaterOrEqual: return MM2CmdDesc.TestGreaterOrEqual(args);
                case MM2ScriptCommand.GetRandom: return MM2CmdDesc.GetRandom(args);
                case MM2ScriptCommand.Pause: return MM2CmdDesc.Pause(args);
                case MM2ScriptCommand.ReadString: return "ReadString";
                case MM2ScriptCommand.CompareText: return MM2CmdDesc.CompareText(args);
                case MM2ScriptCommand.CheckAnyCharHasSkill: return MM2CmdDesc.CheckAnyCharHasSkill(args);
                case MM2ScriptCommand.ElectricalDamage: return MM2CmdDesc.Damage("Electrical", args);
                case MM2ScriptCommand.CheckRaceClass: return MM2CmdDesc.CheckRaceClass(args);
                case MM2ScriptCommand.CheckPartyItem: return MM2CmdDesc.CheckPartyItem(args);
                case MM2ScriptCommand.SetMapSquare: return MM2CmdDesc.SetMapSquare(args);
                case MM2ScriptCommand.CheckEra: return MM2CmdDesc.CheckBetween("Current Era", args);
                case MM2ScriptCommand.CheckDay: return MM2CmdDesc.CheckBetween("Current Day", args);
                case MM2ScriptCommand.SetEncounterLine: return MM2CmdDesc.EncounterLine(Number, args);
                case MM2ScriptCommand.PlaySound: return MM2CmdDesc.PlaySound(args);
                default: return strUnknown;
            }
        }
    }

    public static class MM2CmdDesc
    {
        public static string EncounterLine(int iCurrentLine, byte[] args)
        {
            if (args == null || args.Length < 1)
                return "SetEncounterLine(invalid args)";

            return String.Format("Set Encounter Line to CurrentLine+{0} ({1})", args[0], iCurrentLine + args[0]);
        }

        public static string CharRecordString(MM2ScriptCharData index)
        {
            switch (index & MM2ScriptCharData.FlagsMask)
            {
                case MM2ScriptCharData.Invalid: return "Invalid";
                case MM2ScriptCharData.HirelingFlag: return "Hireling Flag";
                case MM2ScriptCharData.RosterPosition: return "Roster Position";
                case MM2ScriptCharData.Name_0: return "Name";
                case MM2ScriptCharData.Name_1: return "Name[1]";
                case MM2ScriptCharData.Name_2: return "Name[2]";
                case MM2ScriptCharData.Name_3: return "Name[3]";
                case MM2ScriptCharData.Name_4: return "Name[4]";
                case MM2ScriptCharData.Name_5: return "Name[5]";
                case MM2ScriptCharData.Name_6: return "Name[6]";
                case MM2ScriptCharData.Name_7: return "Name[7]";
                case MM2ScriptCharData.Name_8: return "Name[8]";
                case MM2ScriptCharData.Name_9: return "Name[9]";
                case MM2ScriptCharData.Sex: return "Sex";
                case MM2ScriptCharData.AlignmentPerm: return "Alignment Permanent";
                case MM2ScriptCharData.Race: return "Race";
                case MM2ScriptCharData.Class: return "Class";
                case MM2ScriptCharData.MightPerm: return "Might Permanent";
                case MM2ScriptCharData.IntellectPerm: return "Intellect Permanent";
                case MM2ScriptCharData.PersonalityPerm: return "Personality Permanent";
                case MM2ScriptCharData.SpeedPerm: return "Speed Permanent";
                case MM2ScriptCharData.AccuracyPerm: return "Accuracy Permanent";
                case MM2ScriptCharData.LuckPerm: return "Luck Permanent";
                case MM2ScriptCharData.MagicResist: return "Magic Resistance";
                case MM2ScriptCharData.FireResist: return "Fire Resistance";
                case MM2ScriptCharData.ElecResist: return "Electrical Resistance";
                case MM2ScriptCharData.ColdResist: return "Cold Resistance";
                case MM2ScriptCharData.EnergyResist: return "Energy Resistance";
                case MM2ScriptCharData.SleepResist: return "Sleep Resistance";
                case MM2ScriptCharData.PoisonResist: return "Poison Resistance";
                case MM2ScriptCharData.AcidResist: return "Acid Resistance";
                case MM2ScriptCharData.Thievery: return "Thievery";
                case MM2ScriptCharData.ACPerm: return "AC Permanent";
                case MM2ScriptCharData.HPTempMax_0: return "Maximum HP Temporary";
                case MM2ScriptCharData.HPTempMax_1: return "Maximum HP Temporary[1]";
                case MM2ScriptCharData.MightTemp: return "Might Temporary";
                case MM2ScriptCharData.SpeedTemp: return "Speed Temporary";
                case MM2ScriptCharData.AccuracyTemp: return "Accuracy Temporary";
                case MM2ScriptCharData.AlignmentTemp: return "Alignment Temporary";
                case MM2ScriptCharData.LevelTemp: return "Level Temporary";
                case MM2ScriptCharData.SpellLevelTemp: return "SpellLevel Temporary";
                case MM2ScriptCharData.SPCurrent_0: return "Current SP";
                case MM2ScriptCharData.SPCurrent_1: return "Current SP[1]";
                case MM2ScriptCharData.EnduranceTemp: return "Endurance Temporary";
                case MM2ScriptCharData.IntellectTemp: return "Intellect Temporary";
                case MM2ScriptCharData.PersonalityTemp: return "Personality Temporary";
                case MM2ScriptCharData.LuckTemp: return "Luck Temporary";
                case MM2ScriptCharData.LuckPerm46: return "Luck Permanent";
                case MM2ScriptCharData.Age: return "Age";
                case MM2ScriptCharData.AgeDays: return "AgeDays";
                case MM2ScriptCharData.Experience_0: return "Experience";
                case MM2ScriptCharData.Experience_1: return "Experience[1]";
                case MM2ScriptCharData.Experience_2: return "Experience[2]";
                case MM2ScriptCharData.Experience_3: return "Experience[3]";
                case MM2ScriptCharData.SPMax_0: return "Maximum SP";
                case MM2ScriptCharData.SPMax_1: return "Maximum SP[1]";
                case MM2ScriptCharData.SpellLevelPerm: return "SpellLevel Permanent";
                case MM2ScriptCharData.Gems_0: return "Gems";
                case MM2ScriptCharData.Gems_1: return "Gems[1]";
                case MM2ScriptCharData.HPCurrent_0: return "Current HP";
                case MM2ScriptCharData.HPCurrent_1: return "Current HP[1]";
                case MM2ScriptCharData.HPMax_0: return "Maximum HP";
                case MM2ScriptCharData.HPMax_1: return "Maximum HP[1]";
                case MM2ScriptCharData.Gold_0: return "Gold";
                case MM2ScriptCharData.Gold_1: return "Gold[1]";
                case MM2ScriptCharData.Gold_2: return "Gold[2]";
                case MM2ScriptCharData.ACTotal: return "Total AC";
                case MM2ScriptCharData.Food: return "Food";
                case MM2ScriptCharData.Condition: return "Condition";
                case MM2ScriptCharData.EndurancePerm: return "Endurance Permanent";
                case MM2ScriptCharData.EquipItem1: return "Equipped Item #1";
                case MM2ScriptCharData.EquipItem2: return "Equipped Item #2";
                case MM2ScriptCharData.EquipItem3: return "Equipped Item #3";
                case MM2ScriptCharData.EquipItem4: return "Equipped Item #4";
                case MM2ScriptCharData.EquipItem5: return "Equipped Item #5";
                case MM2ScriptCharData.EquipItem6: return "Equipped Item #6";
                case MM2ScriptCharData.BackpackItem1: return "Backpack Item #1";
                case MM2ScriptCharData.BackpackItem2: return "Backpack Item #2";
                case MM2ScriptCharData.BackpackItem3: return "Backpack Item #3";
                case MM2ScriptCharData.BackpackItem4: return "Backpack Item #4";
                case MM2ScriptCharData.BackpackItem5: return "Backpack Item #5";
                case MM2ScriptCharData.BackpackItem6: return "Backpack Item #6";
                case MM2ScriptCharData.EquipCharges1: return "Equipped Item #1 Charges";
                case MM2ScriptCharData.EquipCharges2: return "Equipped Item #2 Charges";
                case MM2ScriptCharData.EquipCharges3: return "Equipped Item #3 Charges";
                case MM2ScriptCharData.EquipCharges4: return "Equipped Item #4 Charges";
                case MM2ScriptCharData.EquipCharges5: return "Equipped Item #5 Charges";
                case MM2ScriptCharData.EquipCharges6: return "Equipped Item #6 Charges";
                case MM2ScriptCharData.BackpackCharges1: return "Backpack Item #1 Charges";
                case MM2ScriptCharData.BackpackCharges2: return "Backpack Item #2 Charges";
                case MM2ScriptCharData.BackpackCharges3: return "Backpack Item #3 Charges";
                case MM2ScriptCharData.BackpackCharges4: return "Backpack Item #4 Charges";
                case MM2ScriptCharData.BackpackCharges5: return "Backpack Item #5 Charges";
                case MM2ScriptCharData.BackpackCharges6: return "Backpack Item #6 Charges";
                case MM2ScriptCharData.EquipBonus1: return "Equipped Item #1 Bonus";
                case MM2ScriptCharData.EquipBonus2: return "Equipped Item #2 Bonus";
                case MM2ScriptCharData.EquipBonus3: return "Equipped Item #3 Bonus";
                case MM2ScriptCharData.EquipBonus4: return "Equipped Item #4 Bonus";
                case MM2ScriptCharData.EquipBonus5: return "Equipped Item #5 Bonus";
                case MM2ScriptCharData.EquipBonus6: return "Equipped Item #6 Bonus";
                case MM2ScriptCharData.BackpackBonus1: return "Backpack Item #1 Bonus";
                case MM2ScriptCharData.BackpackBonus2: return "Backpack Item #2 Bonus";
                case MM2ScriptCharData.BackpackBonus3: return "Backpack Item #3 Bonus";
                case MM2ScriptCharData.BackpackBonus4: return "Backpack Item #4 Bonus";
                case MM2ScriptCharData.BackpackBonus5: return "Backpack Item #5 Bonus";
                case MM2ScriptCharData.BackpackBonus6: return "Backpack Item #6 Bonus";
                case MM2ScriptCharData.MeleeOrdinary: return "Ordinary Melee Damage";
                case MM2ScriptCharData.MeleeMagical: return "Magical Melee Damage";
                case MM2ScriptCharData.RangedOrdinary: return "Ordinary Ranged Damage";
                case MM2ScriptCharData.RangedMagical: return "Magical Ranged Damage";
                case MM2ScriptCharData.SecondarySkills: return "Secondary Skills";
                case MM2ScriptCharData.KnownSpells_0: return "KnownSpells[0]";
                case MM2ScriptCharData.KnownSpells_1: return "KnownSpells[1]";
                case MM2ScriptCharData.KnownSpells_2: return "KnownSpells[2]";
                case MM2ScriptCharData.KnownSpells_3: return "KnownSpells[3]";
                case MM2ScriptCharData.KnownSpells_4: return "KnownSpells[4]";
                case MM2ScriptCharData.KnownSpells_5: return "KnownSpells[5]";
                case MM2ScriptCharData.GuildFlags: return "GuildFlags";
                case MM2ScriptCharData.AdvancementFlags: return "AdvancementFlags";
                case MM2ScriptCharData.QuestFlags1_2: return "QuestFlags1[2]";
                case MM2ScriptCharData.MealsEaten_1: return "MealsEaten[1]";
                case MM2ScriptCharData.MealsEaten_0: return "MealsEaten[0]";
                case MM2ScriptCharData.QuestObject: return "QuestObject";
                case MM2ScriptCharData.QuestFlags1_1: return "QuestFlags1[1]";
                case MM2ScriptCharData.QuestFlags1_0: return "QuestFlags1[0]";
                case MM2ScriptCharData.ArenaFlags_1: return "ArenaFlags[1]";
                case MM2ScriptCharData.ArenaFlags_0: return "ArenaFlags[0]";
                case MM2ScriptCharData.QuestFlags2_1: return "QuestFlags2[1]";
                case MM2ScriptCharData.QuestFlags2_0: return "QuestFlags2[0]";
                default: return "Unknown";
            }
        }

        public static string CharRecordStringAbbr(MM2ScriptCharData index)
        {
            switch (index & MM2ScriptCharData.FlagsMask)
            {
                case MM2ScriptCharData.Invalid: return "Invalid";
                case MM2ScriptCharData.HirelingFlag: return "HireF";
                case MM2ScriptCharData.RosterPosition: return "Roster#";
                case MM2ScriptCharData.Name_0:
                case MM2ScriptCharData.Name_1:
                case MM2ScriptCharData.Name_2:
                case MM2ScriptCharData.Name_3:
                case MM2ScriptCharData.Name_4:
                case MM2ScriptCharData.Name_5:
                case MM2ScriptCharData.Name_6:
                case MM2ScriptCharData.Name_7:
                case MM2ScriptCharData.Name_8:
                case MM2ScriptCharData.Name_9: return "Name";
                case MM2ScriptCharData.Sex: return "Sex";
                case MM2ScriptCharData.AlignmentPerm: return "AlignP";
                case MM2ScriptCharData.Race: return "Race";
                case MM2ScriptCharData.Class: return "Class";
                case MM2ScriptCharData.MightPerm: return "MgtP";
                case MM2ScriptCharData.IntellectPerm: return "IntP";
                case MM2ScriptCharData.PersonalityPerm: return "PerP";
                case MM2ScriptCharData.SpeedPerm: return "SpdP";
                case MM2ScriptCharData.AccuracyPerm: return "AcyP";
                case MM2ScriptCharData.LuckPerm: return "LckP";
                case MM2ScriptCharData.MagicResist: return "MagRes";
                case MM2ScriptCharData.FireResist: return "FiRes";
                case MM2ScriptCharData.ElecResist: return "ElRes";
                case MM2ScriptCharData.ColdResist: return "CoRes";
                case MM2ScriptCharData.EnergyResist: return "EnRes";
                case MM2ScriptCharData.SleepResist: return "SlRes";
                case MM2ScriptCharData.PoisonResist: return "PoRes";
                case MM2ScriptCharData.AcidResist: return "AcRes";
                case MM2ScriptCharData.Thievery: return "Thievery";
                case MM2ScriptCharData.ACPerm: return "ACP";
                case MM2ScriptCharData.HPTempMax_0:
                case MM2ScriptCharData.HPTempMax_1: return "HPMaxT";
                case MM2ScriptCharData.MightTemp: return "MgtT";
                case MM2ScriptCharData.SpeedTemp: return "SpdT";
                case MM2ScriptCharData.AccuracyTemp: return "AcyT";
                case MM2ScriptCharData.AlignmentTemp: return "AlignT";
                case MM2ScriptCharData.LevelTemp: return "LevelT";
                case MM2ScriptCharData.SpellLevelTemp: return "SpLevT";
                case MM2ScriptCharData.SPCurrent_0:
                case MM2ScriptCharData.SPCurrent_1: return "SP";
                case MM2ScriptCharData.EnduranceTemp: return "EndT";
                case MM2ScriptCharData.IntellectTemp: return "IntT";
                case MM2ScriptCharData.PersonalityTemp: return "PerT";
                case MM2ScriptCharData.LuckTemp: return "LckT";
                case MM2ScriptCharData.LuckPerm46: return "LckP";
                case MM2ScriptCharData.Age: return "Age";
                case MM2ScriptCharData.AgeDays: return "AgeDay";
                case MM2ScriptCharData.Experience_0:
                case MM2ScriptCharData.Experience_1:
                case MM2ScriptCharData.Experience_2:
                case MM2ScriptCharData.Experience_3: return "Exp";
                case MM2ScriptCharData.SPMax_0:
                case MM2ScriptCharData.SPMax_1: return "SPMax";
                case MM2ScriptCharData.SpellLevelPerm: return "SpLevP";
                case MM2ScriptCharData.Gems_0:
                case MM2ScriptCharData.Gems_1: return "Gems";
                case MM2ScriptCharData.HPCurrent_0:
                case MM2ScriptCharData.HPCurrent_1: return "HP";
                case MM2ScriptCharData.HPMax_0:
                case MM2ScriptCharData.HPMax_1: return "HPMax";
                case MM2ScriptCharData.Gold_0:
                case MM2ScriptCharData.Gold_1:
                case MM2ScriptCharData.Gold_2: return "Gold";
                case MM2ScriptCharData.ACTotal: return "ACTotal";
                case MM2ScriptCharData.Food: return "Food";
                case MM2ScriptCharData.Condition: return "Cond";
                case MM2ScriptCharData.EndurancePerm: return "EndP";
                case MM2ScriptCharData.EquipItem1: return "Equip1";
                case MM2ScriptCharData.EquipItem2: return "Equip2";
                case MM2ScriptCharData.EquipItem3: return "Equip3";
                case MM2ScriptCharData.EquipItem4: return "Equip4";
                case MM2ScriptCharData.EquipItem5: return "Equip5";
                case MM2ScriptCharData.EquipItem6: return "Equip6";
                case MM2ScriptCharData.BackpackItem1: return "Pack1";
                case MM2ScriptCharData.BackpackItem2: return "Pack2";
                case MM2ScriptCharData.BackpackItem3: return "Pack3";
                case MM2ScriptCharData.BackpackItem4: return "Pack4";
                case MM2ScriptCharData.BackpackItem5: return "Pack5";
                case MM2ScriptCharData.BackpackItem6: return "Pack6";
                case MM2ScriptCharData.EquipCharges1: return "EquipChg1";
                case MM2ScriptCharData.EquipCharges2: return "EquipChg2";
                case MM2ScriptCharData.EquipCharges3: return "EquipChg3";
                case MM2ScriptCharData.EquipCharges4: return "EquipChg4";
                case MM2ScriptCharData.EquipCharges5: return "EquipChg5";
                case MM2ScriptCharData.EquipCharges6: return "EquipChg6";
                case MM2ScriptCharData.BackpackCharges1: return "PackChg1";
                case MM2ScriptCharData.BackpackCharges2: return "PackChg2";
                case MM2ScriptCharData.BackpackCharges3: return "PackChg3";
                case MM2ScriptCharData.BackpackCharges4: return "PackChg4";
                case MM2ScriptCharData.BackpackCharges5: return "PackChg5";
                case MM2ScriptCharData.BackpackCharges6: return "PackChg6";
                case MM2ScriptCharData.EquipBonus1: return "EquipBon1";
                case MM2ScriptCharData.EquipBonus2: return "EquipBon2";
                case MM2ScriptCharData.EquipBonus3: return "EquipBon3";
                case MM2ScriptCharData.EquipBonus4: return "EquipBon4";
                case MM2ScriptCharData.EquipBonus5: return "EquipBon5";
                case MM2ScriptCharData.EquipBonus6: return "EquipBon6";
                case MM2ScriptCharData.BackpackBonus1: return "PackBon1";
                case MM2ScriptCharData.BackpackBonus2: return "PackBon2";
                case MM2ScriptCharData.BackpackBonus3: return "PackBon3";
                case MM2ScriptCharData.BackpackBonus4: return "PackBon4";
                case MM2ScriptCharData.BackpackBonus5: return "PackBon5";
                case MM2ScriptCharData.BackpackBonus6: return "PackBon6";
                case MM2ScriptCharData.MeleeOrdinary: return "MeleeOrg";
                case MM2ScriptCharData.MeleeMagical: return "MeleeMag";
                case MM2ScriptCharData.RangedOrdinary: return "RangeOrd";
                case MM2ScriptCharData.RangedMagical: return "RangeMag";
                case MM2ScriptCharData.SecondarySkills: return "Skills";
                case MM2ScriptCharData.KnownSpells_0: return "Spells0";
                case MM2ScriptCharData.KnownSpells_1: return "Spells1";
                case MM2ScriptCharData.KnownSpells_2: return "Spells2";
                case MM2ScriptCharData.KnownSpells_3: return "Spells3";
                case MM2ScriptCharData.KnownSpells_4: return "Spells4";
                case MM2ScriptCharData.KnownSpells_5: return "Spells5";
                case MM2ScriptCharData.GuildFlags: return "GuildF";
                case MM2ScriptCharData.AdvancementFlags: return "AdvanceF";
                case MM2ScriptCharData.QuestFlags1_2: return "QuestF1.2";
                case MM2ScriptCharData.MealsEaten_1: return "Meals1";
                case MM2ScriptCharData.MealsEaten_0: return "Meals0";
                case MM2ScriptCharData.QuestObject: return "QuestObj";
                case MM2ScriptCharData.QuestFlags1_1: return "QuestF1.1";
                case MM2ScriptCharData.QuestFlags1_0: return "QuestF1.0";
                case MM2ScriptCharData.ArenaFlags_1: return "ArenaF1";
                case MM2ScriptCharData.ArenaFlags_0: return "ArenaF0";
                case MM2ScriptCharData.QuestFlags2_1: return "QuestF2.1";
                case MM2ScriptCharData.QuestFlags2_0: return "QuestF2.0";
                default: return "Unknown";
            }
        }

        public static string DisplayLocal(string strCommand, List<ScriptString> strings, byte[] args)
        {
            if (args == null || args.Length < 1 || args[0] == 0 || args[0] > strings.Count)
                return String.Format("{0} [invalid args]", strCommand);
            return String.Format("{0}: {1}", strCommand, MMScriptLine.MapString(args[0] - 1, strings));  // Strings in MM2 are 1-based
        }

        public static string GenericCommand(string strCommand, byte[] args)
        {
            if (args == null || args.Length < 1)
                return strCommand;
            return String.Format("{0} [{1}]", strCommand, Global.ByteString(args));
        }

        public static string Jump(string strCommand, int iCurrentLine, byte[] args)
        {
            if (args == null || args.Length < 1)
                return String.Format("{0}(invalid args)", strCommand);
            return String.Format("{0}: +{1} ({2})", strCommand, Global.Plural(args[0], "line"), iCurrentLine + 1 + args[0]);
        }

        public static string GetCharDataBits(byte[] args)
        {
            if (args == null || args.Length < 3)
                return "GetCharBits(invalid args)";
            return String.Format("GetBits: {0} {1}{2}",
                CharacterNumber(args[0]),
                CharRecordString((MM2ScriptCharData)args[1]),
                args[2] == 0 ? "" : String.Format(", bitmask {0}", GetBits(args[2])));
        }

        public static string GetAnimationString(byte b)
        {
            return String.Format("#{0}", b);
        }

        public static string PartyByteDescription(byte b)
        {
            switch (b)
            {
                case 0: return "Hireling(Sir Hyron)";
                case 1: return "Hireling(Drog)";
                case 2: return "Hireling(HKPhooey)";
                case 3: return "Hireling(Thund R)";
                case 4: return "Hireling(Aeriel)";
                case 5: return "Hireling(Big Bootay)";
                case 6: return "Hireling(Cleogotcha)";
                case 7: return "Hireling(Harry Kari)";
                case 8: return "Hireling(No Name)";
                case 9: return "Hireling(Gertrude)";
                case 10: return "Hireling(Rat Fink)";
                case 11: return "Hireling(Friar Fly)";
                case 12: return "Hireling(Dark Mage)";
                case 13: return "Hireling(Red Duke)";
                case 14: return "Hireling(Dead Eye)";
                case 15: return "Hireling(Nakazawa)";
                case 16: return "Hireling(Sherman)";
                case 17: return "Hireling(Flailer)";
                case 18: return "Hireling(Fumbler)";
                case 19: return "Hireling(Sir Kill)";
                case 20: return "Hireling(Jed I)";
                case 21: return "Hireling(Holy Moley)";
                case 22: return "Hireling(Slick Pick)";
                case 23: return "Hireling(Mr Wizard)";
                case 35: return "Spell(Levitation)";
                case 39: return "Spell(Water Transmutation)";
                case 40: return "Spell(Air Transmutation)";
                case 41: return "Spell(Fire Transmutation)";
                case 42: return "Spell(Earth Transmutation)";
                case 43: return "Spell(Eagle Eye)";
                case 44: return "Spell(Wizard Eye)";
                case 50: return "NamedPegasus";
                case 51: return "PulledRedLever";
                case 59: return "GwyndonGone";
                case 60: return "StoleMurrayTreasure";
                case 61: return "Benefits(1)";
                case 62: return "Benefits(2)";
                case 132: return "CurrentEra";
                default: return String.Format("{0}", b);
            }
        }

        public static string SetPartyByte(byte[] args)
        {
            if (args == null || args.Length < 2)
                return "SetPartyByte(invalid args)";
            return String.Format("Set Party Byte: {0}, {1}", PartyByteDescription(args[0]), args[1]);
        }

        public static string CheckPartyByte(byte[] args)
        {
            if (args == null || args.Length < 2)
                return "CheckPartyByte(invalid args)";
            return String.Format("Test Party Byte: {0}, {1}", PartyByteDescription(args[0]), args[1]);
        }

        public static string ItemString(byte[] bytes, int offset, byte modifier = 0)
        {
            if (offset >= bytes.Length)
                return "(invalid args)";

            StringBuilder sb = new StringBuilder();
            if (modifier == 0x81)
                sb.Append("(PreviousValue)");
            else
                sb.Append(MM2.Items[bytes[offset]].Name);
            if (bytes.Length > offset + 2 && bytes[offset + 2] != 0)
                sb.AppendFormat(" +{0}", bytes[offset + 2]);
            if (bytes.Length > offset + 1 && bytes[offset + 1] != 0)
                sb.AppendFormat(", {0}", Global.Plural(bytes[offset + 1], "charge"));
            return sb.ToString();
        }

        public static string ItemAdd(byte[] args)
        {
            if (args == null || args.Length < 2)
                return "RemoveItem(invalid args)";
            return String.Format("Add Item: {0}", ItemString(args, 1, args[0]));
        }

        public static string ItemRemove(byte[] args)
        {
            if (args == null || args.Length < 2)
                return "RemoveItem(invalid args)";
            return String.Format("Remove Item: {0}, {1}", CharacterNumber(args[0], false), ItemString(args, 1, args[0]));
        }

        public static string GetBits(byte b)
        {
            return Global.GetBits(b, "11111111", "00000000");
        }

        public static string WriteCharDataBits(byte[] args)
        {
            if (args == null || args.Length < 4)
                return "WriteCharData(invalid args)";
            return String.Format("SetBits: {0} {1} to {2}{3}",
                CharacterNumber(args[0]),
                CharRecordString((MM2ScriptCharData)args[1]),
                (args[0] & 0x80) > 0 ? "LastValue" : args[2] == 0 ? args[3].ToString() : GetBits(args[3]),
                args[2] == 0 ? "" : String.Format(", bitmask {0}", Global.GetBits(args[2], "00000000", "11111111")));
        }

        public static string RunExternalScript(byte[] args)
        {
            if (args == null || args.Length < 1)
                return "RunExternalScript(invalid args)";
            return String.Format("RunScript: {0}", ExternalScriptString(args[0]));
        }

        public static string Teleport(byte[] args, bool bAbbr = false)
        {
            if (args == null || args.Length < 2)
                return "Teleport(invalid args)";

            if ((args[0] & 0x80) > 0)
                return bAbbr ? "RandTeleport" : "Teleport: Random location on this map";
            if (args[0] == 0x40)
                return bAbbr ? "RandOutdoorTelep" : "Teleport: Random outdoor location (map numbers 5 to 16, or 33 to 42)";

            string sFormat = bAbbr ? "Telep {0}:{1},{2}" : "Teleport: {0} ({1},{2})";

            return String.Format(sFormat, bAbbr ? args[0].ToString() : MM2MemoryHacker.GetMapName((MM2Map)args[0]), args[1] & 0xf, args[1] >> 4);
        }

        public static string ShowAnimation(byte[] args)
        {
            if (args == null || args.Length < 2)
                return "ShowAnimation(invalid args)";
            return String.Format("Animation {0}", GetAnimationString(args[0]));
        }

        public static string Encounter(byte[] args)
        {
            if (args == null || args.Length < 10)
                return "Encounter(invalid args)";

            if (Global.AllNull(args))
                return "Random Encounter";

            StringBuilder sb = new StringBuilder();
            List<RLEByte> rleBytes = Global.GetBytesRLE(args, 0, 10);

            if (args.Length == 12 && rleBytes.Count > 0)
            {
                // Special case if the last monster (which can be up to 255 more) is the same as the next-to-last monster
                if (rleBytes[rleBytes.Count - 1].Byte == args[10])
                    rleBytes[rleBytes.Count - 1].Count += args[11];
                else
                    rleBytes.Add(new RLEByte(args[10], args[11]));
            }

            foreach (RLEByte rle in rleBytes)
            {
                if (rle.Byte == 0)
                    continue;   // Fixed encounters don't make use of monster #0 (Creepy Crawler), instead using it as a "none" indicator

                if (rle.Count == 0)
                    continue;

                string strMonster = (rle.Byte < MM2.Monsters.Count ? MM2.Monsters[rle.Byte].ProperName : "Unknown");
                sb.AppendFormat("{0}{1}", strMonster, rle.Count != 1 ? String.Format(" ({0})", rle.Count) : "");
                sb.Append(", ");
            }

            Global.Trim(sb);

            sb.Insert(0, "Encounter: ");
            return sb.ToString();
        }

        public static string ModifyTotal(string strMod, string strAttr, byte[] args)
        {
            if (args == null || args.Length < 2)
                return String.Format("{0} (invalid args) from total {1}", strMod, strAttr);
            return String.Format("{0} {1} from total {2}", strMod, BitConverter.ToUInt16(args, 0), strAttr);
        }

        public static string SetAllCharSingleBit(byte[] args)
        {
            if (args == null || args.Length < 2)
                return "SetAllCharSingleBit(invalid args)";
            return String.Format("Set All Characters' {0}, bit {1}", CharRecordString((MM2ScriptCharData)args[0]), GetBits(args[1]));
        }

        public static string AddCharDataValue(byte[] args)
        {
            if (args == null || args.Length < 6)
                return "AddCharDataValue(invalid args)";

            int iAdd = new UInt24(args, 3);
            return String.Format("{0} to {1} {2}",
                args[0] > 127 ? "Add LastValue" : String.Format("+{0}", iAdd),
                CharacterNumber(args[0]),
                CharRecordString((MM2ScriptCharData)args[1]));
        }

        public static string SubCharDataValue(byte[] args)
        {
            if (args == null || args.Length < 6)
                return "SubCharDataValue(invalid args)";

            int iSub = new UInt24(args, 3);
            return String.Format("{0} {1} {2}",
                args[0] > 127 ? "Subtract LastValue from" : String.Format("-{0} to", iSub),
                CharacterNumber(args[0]),
                CharRecordString((MM2ScriptCharData)args[1]));
        }

        public static string Treasure(byte[] args)
        {
            if (args == null || args.Length < 14)
                return "Treasure(invalid args)";

            int iGold = new UInt24(args, 0);
            int iGems = BitConverter.ToUInt16(args, 3);

            StringBuilder sb = new StringBuilder();
            if (iGold > 0)
                sb.AppendFormat("{0} Gold, ", iGold);
            if (iGems > 0)
                sb.AppendFormat("{0} Gems, ", iGems);
            if (args[5] != 0)
                sb.AppendFormat("{0}, ", ItemString(args, 5));
            if (args[8] != 0)
                sb.AppendFormat("{0}, ", ItemString(args, 8));
            if (args[11] != 0)
                sb.AppendFormat("{0}, ", ItemString(args, 11));

            if (sb.Length == 0)
                return "Treasure(none)";

            Global.Trim(sb);
            return "Treasure: " + sb.ToString();
        }

        public static string TreasureAbbr(byte[] args)
        {
            if (args == null || args.Length < 14)
                return "BadTreasure";

            int iGold = new UInt24(args, 0);
            int iGems = BitConverter.ToUInt16(args, 3);

            StringBuilder sb = new StringBuilder();
            if (iGold > 0)
                sb.AppendFormat("+{0} Gold, ", iGold);
            if (iGems > 0)
                sb.AppendFormat("+{0} Gems, ", iGems);
            if (args[5] != 0)
                sb.AppendFormat("+Item {0}, ", args[5]);
            if (args[8] != 0)
                sb.AppendFormat("+Item {0}, ", args[8]);
            if (args[11] != 0)
                sb.AppendFormat("+Item {0}, ", args[11]);

            if (sb.Length == 0)
                return "EmptyTreasure";

            return Global.Trim(sb).ToString();
        }

        public static char CompareChar(byte b)
        {
            switch (b)
            {
                case 0xE9: return '1';
                case 0xE8: return '2';
                case 0xE7: return '3';
                case 0xE6: return '4';
                case 0xE5: return '5';
                case 0xE4: return '6';
                case 0xE3: return '7';
                case 0xE2: return '8';
                case 0xE1: return '9';
                case 0xD9: return 'a';
                case 0xD8: return 'b';
                case 0xD7: return 'c';
                case 0xD6: return 'd';
                case 0xD5: return 'e';
                case 0xD4: return 'f';
                case 0xD3: return 'g';
                case 0xD2: return 'h';
                case 0xD1: return 'i';
                case 0xD0: return 'j';
                case 0xCF: return 'k';
                case 0xCE: return 'l';
                case 0xCD: return 'm';
                case 0xCC: return 'n';
                case 0xCB: return 'o';
                case 0xCA: return 'p';
                case 0xC9: return 'q';
                case 0xC8: return 'r';
                case 0xC7: return 's';
                case 0xC6: return 't';
                case 0xC5: return 'u';
                case 0xC4: return 'v';
                case 0xC3: return 'w';
                case 0xC2: return 'x';
                case 0xC1: return 'y';
                case 0xC0: return 'z';
                default: return '?';
            }
        }

        public static string CompareString(byte[] args)
        {
            // MM2 stores comparison strings in an odd format
            StringBuilder sb = new StringBuilder(args.Length);
            foreach (byte b in args)
            {
                if (b == 0xFA || b == 0x7A)
                    break;
                else
                    sb.Append(CompareChar(b));
            }

            if (sb.Length < 1)
                return "<null>";

            return sb.ToString();
        }

        public static string DamageType(byte b)
        {
            switch (b)
            {
                case 0: return "Electrical";
                default: return "Unknown";
            }
        }

        public static string AddCurrentYear(byte[] args)
        {
            if (args.Length < 1)
                return "AddCurrentYear(invalid args)";
            return String.Format("Add: Current Year +{0}", args[0]);
        }

        public static string TestGreaterOrEqual(byte[] args)
        {
            if (args.Length < 1)
                return "TestGreaterOrEqual(invalid args)";
            return String.Format("Test: Value >= {0}", args[0]);
        }

        public static string GetRandom(byte[] args)
        {
            if (args.Length < 1)
                return "GetRandom(invalid args)";
            return String.Format("Get a Random Number from 1 to {0}", args[0]);
        }

        public static string CompareText(byte[] args)
        {
            if (args.Length < 1)
                return "CompareText(invalid args)";
            return String.Format("Compare String: {0}", CompareString(args));
        }

        public static string CheckAnyCharHasSkill(byte[] args)
        {
            if (args.Length < 1)
                return "CheckAnyCharHasSkil(invalid args)";
            return String.Format("Test Any Character for Skill: {0}", MM2Character.SecondarySkillName((MM2SecondarySkill)args[0]));
        }

        public static string CharacterNumber(int iNum, bool bPossessive = true)
        {
            switch (iNum)
            {
                case 128:
                case 0: return "All Characters" + (bPossessive ? "'" : "");
                case 137:
                case 9: return "Selected Character" + (bPossessive ? "'s" : "");
                default: return String.Format("Character {0}" + (bPossessive ? "'s" : ""), iNum & 0x7f);
            }
        }

        public static string Damage(string strType, byte[] args)
        {
            if (args.Length < 3)
                return String.Format("{0}Damage(invalid args)", strType);
            string strDamage = (args[0] & 0x80) > 0 ? "(PreviousValue)" : BitConverter.ToUInt16(args, 1).ToString();
            return String.Format("-{0} {1} Damage to {2}", strDamage, strType, CharacterNumber(args[0], false));
        }

        public static string ExternalScriptString(byte b)
        {
            switch ((MM2ExtScript)b)
            {
                case MM2ExtScript.Invalid: return "Invalid";
                case MM2ExtScript.None: return "None";
                case MM2ExtScript.Inn: return "Inn";
                case MM2ExtScript.Training: return "Training";
                case MM2ExtScript.Tavern: return "Tavern";
                case MM2ExtScript.Temple: return "Temple";
                case MM2ExtScript.MageGuild: return "Guild";
                case MM2ExtScript.Blacksmith: return "Blacksmith";
                case MM2ExtScript.Arena: return "Arena";
                default: return String.Format("Internal#{0}", b);
            }
        }

        public static string RaceClassString(int i)
        {
            string strNegate = ((i & 0x20) == 0 ? "Non-" : "");
            switch (i & ~0x20)
            {
                case 0x00: return strNegate + "Knights";
                case 0x01: return strNegate + "Paladins";
                case 0x02: return strNegate + "Archers";
                case 0x03: return strNegate + "Clerics";
                case 0x04: return strNegate + "Sorcerers";
                case 0x05: return strNegate + "Robbers";
                case 0x06: return strNegate + "Ninjas";
                case 0x07: return strNegate + "Barbarians";
                case 0x40: return strNegate + "Males";
                case 0x41: return strNegate + "Females";
                case 0x80: return strNegate + "Humans";
                case 0x81: return strNegate + "Elves";
                case 0x82: return strNegate + "Dwarves";
                case 0x83: return strNegate + "Gnomes";
                case 0x84: return strNegate + "Half-Orcs";
                default: return "[Unknown]";
            }
        }

        public static string CheckPartyItem(byte[] args)
        {
            if (args.Length < 2)
                return "CheckPartyItem(invalid args)";

            return String.Format("Test if party inventory contains: {0}", ItemString(args, 1));
        }

        public static string CheckBetween(string strCheck, byte[] args)
        {
            if (args.Length < 2)
                return String.Format("Check {0} (invalid args)", strCheck);

            return String.Format("Test if {0} is from {1} to {2}", strCheck, args[0], args[1]);
        }

        public static string SetMapSquare(byte[] args)
        {
            if (args.Length < 3)
                return "SetMapSquare(invalid args)";

            return String.Format("Set Map Square({0},{1}), Appearance: {2}, Attributes: {3}",
                args[0] & 0xf, args[0] >> 4, GetBits(args[1]), GetBits(args[2]));
        }

        public static string CheckRaceClass(byte[] args, bool bAbbr = false)
        {
            if (args.Length < 2)
                return "CheckRaceClass(invalid args)";

            bool bNeg1 = (args[0] & 0x20) == 0;
            bool bNeg2 = (args[1] & 0x20) == 0;

            // The first argument being 0 means "non-knights," but the second argument being 0 is just a null argument

            if (bAbbr && args[1] != 0)
                return String.Format("{0}/{1}?", RaceClassString(args[0]), RaceClassString(args[1]));

            if (bAbbr)
                return String.Format("{0}?", RaceClassString(args[0]));

            if (args[1] == 0)
                return String.Format("Test if any {0} are in the party.", RaceClassString(args[0] == 0 ? args[1] : args[0]));

            if (bNeg1 && bNeg2)
                return String.Format("Test if only {0} and {1} are in the party.", RaceClassString(args[0] | 0x20), RaceClassString(args[1] | 0x20));
            return String.Format("Test if any {0} or {1} are in the party.", RaceClassString(args[0]), RaceClassString(args[1]));
        }

        public static string Pause(byte[] args)
        {
            if (args.Length < 1)
                return "Pause(invalid args)";

            return String.Format("Pause for {0} cycles", args[0]);
        }

        public static string PlaySound(byte[] args)
        {
            if (args.Length < 1)
                return "PlaySound(invalid args)";

            return String.Format("Play Sound #{0}", args[0]);
        }
    }
}
