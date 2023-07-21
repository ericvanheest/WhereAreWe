using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum BT3Opcode
    {
        Zero                = 0x00,
        StairsUp            = 0x00,
        StairsDown          = 0x01,
        RunFunction         = 0x02,
        Teleport            = 0x03,
        Encounter           = 0x04,
        Message             = 0x05,
        ReplaceExit         = 0x06,
        Animation           = 0x07,
        ViewportText        = 0x08,
        WaitKeypress        = 0x09,
        ClearText           = 0x0A,
        ExitIfNotSet        = 0x0B,
        ExitIfSet           = 0x0C,
        AlterMap            = 0x0D,
        SetScriptBit        = 0x0E,
        ClearScriptBit      = 0x0F,
        CheckAction         = 0x10,
        CancelAction        = 0x11,
        ContMessage         = 0x12,
        Unknown13           = 0x13,
        CheckContainer      = 0x14,
        ReceiveItem         = 0x15,
        IfHaveItem          = 0x16,
        EndIfHaveItem       = 0x17,
        DelayUntilKey       = 0x18,
        AskYesNo            = 0x19,
        Jump                = 0x1A,
        CondEncounter       = 0x1B,
        AllowStairs         = 0x1C,
        BackUp              = 0x1D,
        RemoveItem          = 0x1E,
        DecreaseCounter     = 0x20,
        CheckCounter        = 0x21,
        PartyDamage         = 0x23,
        CheckLocation       = 0x24,
        SetContents         = 0x25,
        AddItemCharges      = 0x26,
        SetItemCharges      = 0x27,
        AddCounter          = 0x28,
        RotateParty         = 0x2A,
        ReadLine            = 0x2B,
        CompareLine         = 0x2C,
        StoreCounter        = 0x2D,
        AskCharacter        = 0x2E,
        CompareCounterGold  = 0x2F,
        CompareCounterLess  = 0x31,
        CompareCounterEqual = 0x32,
        LearnSpell          = 0x34,
        SetCounter          = 0x35,
        CheckItem           = 0x36,
        Unknown37           = 0x37,
        AddNPC              = 0x38,
        CheckNPC            = 0x39,
        MessageIndirect     = 0x3A,
        Unknown3B           = 0x3B,
        RemoveHawkslayer    = 0x3C,
        Unknown3F           = 0x3F,
        Unknown42           = 0x42,
        RemoveTree          = 0x43,
        CheckClass          = 0x44,
        MessageIndirect2    = 0x45,
        TeleportGray        = 0x46,
        Continue            = 0x80,
        Invalid             = 0xd1,
        ExitScript          = 0x7f,
        BranchIfSetHigh     = 0x8B,
        BranchIfNotSetHigh  = 0x8C,
        DelayUntilKeyHigh   = 0x98,
        AskYesNoHigh        = 0x99,
        JumpHigh            = 0x9A,
        AskCharacterHigh    = 0xAE,
        ExitScriptHigh      = 0xff,
    }

    public class BT3ScriptInfo : ScriptInfo
    {
        public IBT3MapLevel Level;
        public BT3Monster[] Monsters;
        public bool StairsReversed = false;

        public BT3ScriptInfo(IBT3MapLevel level, MapTitleInfo map, BT3Scripts scripts, BT3Monster[] monsters, byte[] mapFlags)
        {
            Level = level;
            Map = map;
            Scripts = scripts;
            Monsters = monsters;
            StairsReversed = (mapFlags != null && mapFlags.Length > 2 && (mapFlags[2] & 0x10) > 0);
        }
    }

    public class BT3ScriptLine : ScriptLine
    {
        public static string OpcodeDescription(BT3Opcode opcode)
        {
            switch (opcode)
            {
                case BT3Opcode.StairsUp: return "Stairs outward";
                case BT3Opcode.StairsDown: return "Stairs inward";
                case BT3Opcode.Teleport: return "Teleport";
                case BT3Opcode.Encounter: return "Encounter";
                case BT3Opcode.CondEncounter: return "Encounter";
                case BT3Opcode.Message: return "Display message";
                case BT3Opcode.ContMessage: return "Continue message";
                case BT3Opcode.ReplaceExit: return "Set exit command";
                case BT3Opcode.Animation: return "Display animation";
                case BT3Opcode.RunFunction: return "Run function";
                case BT3Opcode.ViewportText: return "Set viewport text";
                case BT3Opcode.CheckNPC: return "Check NPC";
                case BT3Opcode.WaitKeypress: return "Wait for keypress";
                case BT3Opcode.ClearText: return "Clear text display";
                case BT3Opcode.Unknown13: return "Unknown13";
                case BT3Opcode.MessageIndirect:
                case BT3Opcode.MessageIndirect2: return "Display message at";
                case BT3Opcode.CheckContainer: return "Check container";
                case BT3Opcode.CheckAction: return "Check action";
                case BT3Opcode.AddNPC: return "Add NPC";
                case BT3Opcode.CheckClass: return "Check class";
                case BT3Opcode.RemoveHawkslayer: return "Remove NPC \"Hawkslayer\"";
                case BT3Opcode.CheckItem: return "Check item";
                case BT3Opcode.CancelAction: return "Cancel action";
                case BT3Opcode.AllowStairs: return "Permit stairs (reverses opcode 0x18)";
                case BT3Opcode.BackUp: return "Back up";
                case BT3Opcode.SetScriptBit: return "Set script bit";
                case BT3Opcode.ClearScriptBit: return "Clear script bit";
                case BT3Opcode.StoreCounter: return "Store in counter";
                case BT3Opcode.AlterMap: return "Alter map";
                case BT3Opcode.DelayUntilKey: return "Execute script";
                case BT3Opcode.Unknown37: return "Unknown37";
                case BT3Opcode.Unknown42: return "Unknown42";
                case BT3Opcode.RemoveTree: return "Remove tree";
                case BT3Opcode.DecreaseCounter: return "Decrease counter";
                case BT3Opcode.LearnSpell: return "Learn spell";
                case BT3Opcode.RemoveItem: return "Remove item";
                case BT3Opcode.CheckCounter: return "Check counter";
                case BT3Opcode.CompareCounterGold: return "Check gold vs. counter";
                case BT3Opcode.CompareCounterLess: return "Check if counter";
                case BT3Opcode.CompareCounterEqual: return "Check if counter";
                case BT3Opcode.PartyDamage: return "Party damage";
                case BT3Opcode.SetCounter: return "Set counter";
                case BT3Opcode.AddCounter: return "Add to counter";
                case BT3Opcode.CheckLocation: return "Check location";
                case BT3Opcode.SetContents: return "Set container contents";
                case BT3Opcode.AddItemCharges: return "Add item charges";
                case BT3Opcode.SetItemCharges: return "Set item charges";
                case BT3Opcode.RotateParty: return "Rotate party";
                case BT3Opcode.Unknown3B: return "Unknown3B";
                case BT3Opcode.Unknown3F: return "Unknown3F";
                case BT3Opcode.AskYesNo: return "Ask Yes/No";
                case BT3Opcode.AskCharacter: return "Ask which character";
                case BT3Opcode.ReadLine: return "Read line";
                case BT3Opcode.CompareLine: return "Compare line";
                case BT3Opcode.TeleportGray: return "Teleport with gray screen";
                case BT3Opcode.Continue: return "Continue";
                case BT3Opcode.Invalid: return "Invalid";
                case BT3Opcode.ExitScript:
                case BT3Opcode.ExitScriptHigh: return "Exit script";
                case BT3Opcode.ExitIfSet: return "Check script bit";
                case BT3Opcode.ExitIfNotSet: return "Check script bit";
                case BT3Opcode.Jump: return "Jump";
                case BT3Opcode.IfHaveItem: return "If party has item";
                case BT3Opcode.EndIfHaveItem: return "If party has item";
                case BT3Opcode.ReceiveItem: return "Receive item";
                default: return String.Format("Invalid Opcode: {0:X2}", (int) opcode);
            }
        }

        public BT3Opcode Opcode { get { return Bytes == null ? BT3Opcode.Invalid : (BT3Opcode)Bytes.Bytes[0] & (~BT3Opcode.Continue); } }
        public BT3Opcode FullOpcode { get { return Bytes == null ? BT3Opcode.Invalid : (BT3Opcode)Bytes.Bytes[0]; } }
        public bool HighOpcode { get { return (FullOpcode & BT3Opcode.Continue) == BT3Opcode.Continue; } }
        public bool IsEndOfScript
        {
            get
            {
                switch (FullOpcode)
                {
                    case BT3Opcode.AskYesNo:        // Only exits the script if "no" is selected
                    case BT3Opcode.AskCharacter:    // Only exits the script if "escape" is pressed
                    case BT3Opcode.CheckAction:
                    case BT3Opcode.AddNPC:
                    case BT3Opcode.CheckClass:
                    case BT3Opcode.CheckItem:
                    case BT3Opcode.CheckContainer:
                    case BT3Opcode.DelayUntilKey:
                    case BT3Opcode.Unknown42:
                    case BT3Opcode.RotateParty:
                    case BT3Opcode.Unknown3B:
                    case BT3Opcode.Unknown3F:
                    case BT3Opcode.CheckLocation:
                    case BT3Opcode.ExitIfNotSet:
                    case BT3Opcode.ExitIfSet:
                    case BT3Opcode.EndIfHaveItem:
                    case BT3Opcode.ReceiveItem:
                        return false;
                    case BT3Opcode.Invalid:
                    case BT3Opcode.ExitScript:
                    case BT3Opcode.ExitScriptHigh:
                    case BT3Opcode.Jump:
                    case BT3Opcode.JumpHigh:    // Why is the high bit ever set on this??
                        return true;
                }
                if (HighOpcode)
                    return false;
                return true;
            }
        }

        public override MemoryBytes HeaderBytes { get { return new MemoryBytes(BitConverter.GetBytes((short)Address)); } }

        public string Text;
        public string Text2;
        public int Jump;
        public int Target;
        public BT3ItemIndex Item;

        public override Point TeleportLocation { get { return IsTeleportCommand ? new Point(CommandBytes[2], CommandBytes[1]) : base.TeleportLocation; } }
        public override bool IsTeleportCommand { get { return Opcode == BT3Opcode.Teleport || Opcode == BT3Opcode.TeleportGray; } }
        public override bool IsEncounterCommand { get { return Opcode == BT3Opcode.Encounter || Opcode == BT3Opcode.CondEncounter; } }
        public override int TeleportMapIndex { get { return IsTeleportCommand ? CommandBytes[3] : base.TeleportMapIndex; } }

        public BT3ScriptLine(int iMemOffset, int iScriptOffset, Point pt, byte[] command)
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

        public static string MonsterName(int index, BT3Monster[] monsters = null)
        {
            if (monsters == null || monsters.Length <= index)
                return String.Format("#{0}", index);

            return monsters[index].ProperName;
        }

        public static string EncounterString(byte[] bytes, int offset, BT3Monster[] monsters = null)
        {
            if (bytes == null || offset >= bytes.Length)
                return "<invalid>";
            int iGroups = bytes[offset];
            if (iGroups < 1 || iGroups > 4 || bytes.Length - offset - 1 < iGroups * 2)
                return "<invalid>";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < iGroups; i++)
            {
                int iMonster = bytes[i * 2 + offset + 1];
                int iCount = bytes[i * 2 + offset + 2];
                sb.AppendFormat("{0} ({1}), ", MonsterName(iMonster, monsters), iCount);
            }
            Global.Trim(sb);
            return sb.ToString();
        }

        private string BasicWallType(int iType)
        {
            switch (iType)
            {
                case 0: return "Open";
                case 5:
                case 13: return "Barrier";
                case 1:
                case 6:
                case 8:
                case 9:
                case 11:
                case 14: return "Wall";
                case 2:
                case 3:
                case 4:
                case 7:
                case 10:
                case 12:
                case 15: return "Door";
                default: return "Unknown";
            }
        }

        private string AlterMapCommand(byte b, bool bAbbrev = false)
        {
            int iDirection = (b & 0x30) >> 4;
            int iCommand = b & 0x0f;
            return String.Format("{0}={1}", BT3MemoryHacker.FacingString(iDirection, bAbbrev), BasicWallType(iCommand));
        }

        private string GetAction(bool bForNote, byte b, int iJump)
        {
            if (b < 0x80)
            {
                if (BT3.Spells.Count > b)
                {
                    if (iJump != 0)
                    {
                        if (bForNote)
                            return String.Format("Cast \"{0}\"", BT3.Spells[b].Name);
                        else
                            return String.Format("Cast {0}? Goto {1:X4}", BT3.Spells[b].Abbreviation, iJump);
                    }
                    return String.Format("Cast \"{0}\"", bForNote ? BT3.Spells[b].Name : BT3.Spells[b].Abbreviation);
                }
                return "Cast <invalid>";
            }
            switch (b)
            {
                case 0x80: return bForNote ? "use an Acorn" : "Use Acorn";
                case 0x81: return bForNote ? "use a container" : "Use container";
                case 0x82: return bForNote ? "use an item" : "Use item";
            }
            return "<unknown>";
        }

        private string GetActionDescription(byte b, int iJump)
        {
            if (b < 0x80)
            {
                if (BT3.Spells.Count > b)
                {
                    string strJump = iJump == 0 ? "; End script if not" : String.Format("; jump to 0x{0:X4} if true", iJump);
                    return String.Format("Cast \"{0}\"{1}", BT3.Spells[b].Name, strJump);
                }
            }

            return GetAction(false, b, iJump);
        }

        private string ContainerContents(byte b)
        {
            switch (b)
            {
                case 0: return "water";
                case 1: return "spirits";
                case 2: return "water of life";
                case 3: return "dragon blood";
                case 4: return "molten tar";
                default: return String.Format("unknown({0})", b);
            }
        }

        private string GetGameFunction(int iFunction)
        {
            switch (iFunction)
            {
                case 3: return "Change class to Geomancer";
                case 4: return "Scry Site";
                case 5: return "End Game";
                default: return "Unknown";
            }
        }

        private int ToMap(int iMapCurrent, int iMapTo)
        {
            // If iMapTo has the high bit set, it indicates a map from the other set (surface/dungeon)
            if ((iMapCurrent ^ iMapTo) < 0x80)
                return iMapTo & 0x7F;
            return iMapTo | 0x80;
        }

        public string ClassString(byte bClass)
        {
            if (bClass >= (int)BT3Class.Last)
                return String.Format("Unknown({0})", bClass);
            return BTCharacter.ClassString((BT3Class)bClass);
        }

        public string SpellString(byte bSpell, bool bAbbrev)
        {
            if (bSpell >= (int)BT3SpellIndex.Last)
                return String.Format("Unknown({0})", bSpell);
            return bAbbrev ? BT3.Spells[bSpell].Abbreviation : BT3.Spells[bSpell].Name;
        }

        public override string Description(ScriptInfo info, string strLinePrefix)
        {
            // This is the text that is displayed in lower pane of the script editor
            StringBuilder sb = new StringBuilder(OpcodeDescription(Opcode));
            BT3ScriptInfo bt3Info = info as BT3ScriptInfo;
            bool bHigh = HighOpcode;
            switch (Opcode)
            {
                case BT3Opcode.StairsDown:
                    sb.Append(bt3Info.StairsReversed ? " (up)" : " (down)");
                    break;
                case BT3Opcode.StairsUp:
                    sb.Append(bt3Info.StairsReversed ? " (down)" : " (up)");
                    break;
                case BT3Opcode.BackUp:
                    sb.Append(" one square and turn around");
                    break;
                case BT3Opcode.RemoveTree:
                    sb.Append(" in front of the party, if any");
                    break;
                case BT3Opcode.RotateParty:
                    sb.AppendFormat(" to face {0}", BT3MemoryHacker.FacingString(Bytes.Bytes[1]).ToLower());
                    break;
                case BT3Opcode.Unknown3B:
                    sb.AppendFormat("; jump to 0x{0:X4} if ??", Jump);
                    break;
                case BT3Opcode.DelayUntilKey:
                    if (bHigh)
                        sb.AppendFormat(" at 0x{0:X4}", Jump);
                    else
                        sb.Append(" at next line");
                    sb.Append(" after keypress (and prevent stairs from appearing until opcode 0x1C)");
                    break;
                case BT3Opcode.MessageIndirect:
                case BT3Opcode.MessageIndirect2:
                    sb.AppendFormat(" 0x{0:X4}: {1}", Target, Text2);
                    break;
                case BT3Opcode.CheckContainer:
                    sb.AppendFormat(" for \"{0}\"; jump to 0x{1:X4} if true", ContainerContents(Bytes.Bytes[1]), Jump);
                    break;
                case BT3Opcode.SetContents:
                    sb.AppendFormat(" to \"{0}\"", ContainerContents(Bytes.Bytes[1]));
                    break;
                case BT3Opcode.SetItemCharges:
                    sb.AppendFormat(" to {0}", Bytes.Bytes[1]);
                    break;
                case BT3Opcode.SetCounter:
                    sb.AppendFormat(" #{0} to {1}", Bytes.Bytes[1], Target);
                    break;
                case BT3Opcode.AddCounter:
                    sb.AppendFormat(" #{0}: {1}", Bytes.Bytes[1], Target);
                    break;
                case BT3Opcode.DecreaseCounter:
                    sb.AppendFormat(" #{0}", Bytes.Bytes[1]);
                    break;
                case BT3Opcode.LearnSpell:
                    sb.AppendFormat(" \"{0}\"", SpellString(Bytes.Bytes[1], false));
                    break;
                case BT3Opcode.RemoveItem:
                    sb.AppendFormat(" \"{0}\"", BT3.ItemName(Item));
                    break;
                case BT3Opcode.PartyDamage:
                    sb.AppendFormat(": {0} HP", Target);
                    break;
                case BT3Opcode.CheckLocation:
                    sb.AppendFormat(" is between {0},{1} and {2},{3}; End script if not", Bytes.Bytes[2], Bytes.Bytes[1], Bytes.Bytes[4], Bytes.Bytes[3]);
                    break;
                case BT3Opcode.AddItemCharges:
                    sb.AppendFormat(": {0}", Bytes.Bytes[1]);
                    break;
                case BT3Opcode.CheckCounter:
                    sb.AppendFormat(" #{0}; jump to 0x{1:X4} if zero", Bytes.Bytes[1], Jump);
                    break;
                case BT3Opcode.CompareCounterGold:
                    sb.AppendFormat(" #{0}; jump to 0x{1:X4} if sufficient", Bytes.Bytes[1], Jump);
                    break;
                case BT3Opcode.CompareCounterLess:
                    sb.AppendFormat(" #{0} is less than {1}; jump to 0x{2:X4} if true", Bytes.Bytes[1], Target, Jump);
                    break;
                case BT3Opcode.CompareCounterEqual:
                    sb.AppendFormat(" #{0} is equal to {1}; jump to 0x{2:X4} if true", Bytes.Bytes[1], Target, Jump);
                    break;
                case BT3Opcode.Teleport:
                case BT3Opcode.TeleportGray:
                    sb.AppendFormat(" to {0},{1}", Bytes.Bytes[2], Bytes.Bytes[1]);
                    int iTo = ToMap(info.Map.Map, Bytes.Bytes[3]);
                    if (info.Map.Map != iTo)
                        sb.AppendFormat(" in {0}", BT3MemoryHacker.GetMapTitlePair(iTo).Title);
                    break;
                case BT3Opcode.IfHaveItem:
                    sb.AppendFormat(" \"{0}\" then jump to 0x{1:X4}", BT3.ItemName(Item), Jump);
                    break;
                case BT3Opcode.CheckAction:
                    sb.AppendFormat(": {0}", GetActionDescription(Bytes.Bytes[1], Jump));
                    break;
                case BT3Opcode.AddNPC:
                    sb.AppendFormat(" \"{0}\"; jump to 0x{1:X4} if successful", MonsterName(Bytes.Bytes[1], bt3Info.Monsters), Jump);
                    break;
                case BT3Opcode.CheckClass:
                    sb.AppendFormat(" is \"{0}\"; jump to 0x{1:X4} if true", ClassString(Bytes.Bytes[1]), Jump);
                    break;
                case BT3Opcode.CheckItem:
                    sb.AppendFormat(" is \"{0}\"", BT3.ItemName((BT3ItemIndex) Bytes.Bytes[1]));
                    if (!bHigh)
                        sb.Append("; End script if not");
                    else
                        sb.AppendFormat("; jump to 0x{0:X4} if true", Jump);
                    break;
                case BT3Opcode.EndIfHaveItem:
                    sb.AppendFormat(" \"{0}\" then end script", BT3.ItemName(Item));
                    break;
                case BT3Opcode.ReceiveItem:
                    sb.AppendFormat(" \"{0}\"", BT3.ItemName(Item));
                    if (Bytes.Bytes[3] != 255)
                        sb.AppendFormat(" ({0} charges)", Bytes.Bytes[3]);
                    break;
                case BT3Opcode.ExitIfSet:
                    if (bHigh)
                        sb.AppendFormat(" {0}; jump to 0x{1:X4} if not set", Bytes.Bytes[1], Jump);
                    else
                        sb.AppendFormat(" {0}; End script if set", Bytes.Bytes[1], Jump);
                    break;
                case BT3Opcode.Jump:
                    sb.AppendFormat(" to 0x{0:X4}", Jump);
                    break;
                case BT3Opcode.ExitIfNotSet:
                    if (bHigh)
                        sb.AppendFormat(" {0}; jump to 0x{1:X4} if set", Bytes.Bytes[1], Jump);
                    else
                        sb.AppendFormat(" {0}; End script if not set", Bytes.Bytes[1], Jump);
                    break;
                case BT3Opcode.SetScriptBit:
                case BT3Opcode.ClearScriptBit:
                    sb.AppendFormat(" {0}", Bytes.Bytes[1]);
                    break;
                case BT3Opcode.StoreCounter:
                    sb.AppendFormat(" #{0} the value read", Bytes.Bytes[1]);
                    break;
                case BT3Opcode.AlterMap:
                    sb.AppendFormat(" at {0},{1}: {2}", Bytes.Bytes[2], Bytes.Bytes[1], AlterMapCommand(Bytes.Bytes[3]));
                    break;
                case BT3Opcode.Encounter:
                    if (bHigh)
                        sb.AppendFormat(" with {0}; jump to 0x{1:X4} if successful", EncounterString(Bytes.Bytes, 1, bt3Info.Monsters), Jump);
                    else
                        sb.AppendFormat(" with {0}", EncounterString(Bytes.Bytes, 1, bt3Info.Monsters));
                    break;
                case BT3Opcode.CondEncounter:
                    sb.AppendFormat(" with {0}; jump to 0x{1:X4} if successful", EncounterString(Bytes.Bytes, 1, bt3Info.Monsters), Jump);
                    break;
                case BT3Opcode.Message:
                case BT3Opcode.ContMessage:
                case BT3Opcode.ViewportText:
                    sb.AppendFormat(": {0}", Text);
                    break;
                case BT3Opcode.CheckNPC:
                    sb.AppendFormat(" \"{0}\" is in party; jump to 0x{1:X4} if not", Text, Jump);
                    break;
                case BT3Opcode.CompareLine:
                    sb.AppendFormat(" with \"{0}\" and jump to 0x{1:X4} if successful", Text, Jump);
                    break;
                case BT3Opcode.ReplaceExit:
                    sb.AppendFormat(" at address 0x{0:X4}", Target);
                    break;
                case BT3Opcode.Animation:
                    sb.AppendFormat(" #{0}", CommandBytes[1]);
                    break;
                case BT3Opcode.RunFunction:
                    sb.AppendFormat(" #{0} ({1})", CommandBytes[1], GetGameFunction(CommandBytes[1]));
                    break;
                case BT3Opcode.AskYesNo:
                    if (FullOpcode == BT3Opcode.AskYesNoHigh)
                        sb.AppendFormat("; jump to {0:X4} if \"Yes\"", Jump);
                    else
                        sb.Append("; End script if \"No\"");
                    break;
                case BT3Opcode.AskCharacter:
                    if (FullOpcode == BT3Opcode.AskCharacterHigh)
                        sb.AppendFormat("; jump to {0:X4} if not \"Escape\"", Jump);
                    else
                        sb.Append("; End script if \"Escape\"");
                    break;
            }
            if (IsEndOfScript)
            {
                switch (Opcode)
                {
                    case BT3Opcode.ExitScript:
                    case BT3Opcode.Jump:
                        break;
                    default:
                        sb.Append("; End script");
                        break;
                }
            }

            return sb.ToString();
        }

        private ScriptSummary StairsSummary(DirectionFlags dir, MapXY dest, bool bForNote)
        {
            string strDir = dir == DirectionFlags.Up ? "Up" : "Down";
            string strDescription = strDir;
            if (bForNote)
            {
                strDescription = String.Format("There are stairs here, going {0}.  Do you with to take them? (Yes/No)\r\nY: {{map:{1}}} ({2},{3})\r\n",
                    strDir.ToLower(), BT3MemoryHacker.GetMapTitlePair(dest.Map).Title, dest.X, dest.Y);
            }
            ScriptSummary summary = new ScriptSummary(strDescription);
            summary.Icon = dir == DirectionFlags.Down ? IconName.StairsDown : IconName.StairsUp;
            summary.Command = ScriptCommand.Stairs;
            summary.Direction = dir;
            summary.Destination = dest;
            summary.Symbol = ".";
            return summary;
        }

        private ScriptSummary AskCharacterSummary(bool bForNote)
        {
            ScriptSummary summary = new ScriptSummary(Jump == 0 ? "AskChar" : String.Format("AskChar or goto {0:X4}", Jump), ScriptCommand.Input);
            if (!bForNote)
                return summary;

            summary.Description = "If you do not select a character: ";
            summary.NoNewline = true;
            return summary;
        }

        private ScriptSummary ReadLineSummary(bool bForNote)
        {
            ScriptSummary summary = new ScriptSummary("ReadLine");
            if (!bForNote)
                return summary;

            summary.Command = ScriptCommand.Input;
            summary.Description = "If you do not answer ";
            summary.NoNewline = true;
            return summary;
        }

        private ScriptSummary CompareLineSummary(bool bForNote)
        {
            ScriptSummary summary = null;
            if (bForNote)
                summary = new ScriptSummary(String.Format("\"{0}\": ", Text));
            else
                summary = new ScriptSummary(String.Format("\"{0}\"? Goto {1:X4}", Text, Jump));
            summary.Command = ScriptCommand.Conditional;
            summary.Symbol = "Ri";
            summary.NoNewline = true;
            return summary;
        }

        private ScriptSummary TeleportSummary(bool bForNote, bool bGray, int x, int y, string strMap)
        {
            string strGray = bGray ? (bForNote ? " with a gray screen" : "/gray") : "";
            ScriptSummary summary = null;
            if (bForNote)
            {
                if (String.IsNullOrWhiteSpace(strMap))
                    summary = new ScriptSummary(String.Format("Teleport{0} to ({1},{2})", strGray, x, y));
                else
                    summary = new ScriptSummary(String.Format("{{map:{0}}} ({1},{2})", strMap, x, y));
            }
            else
                summary = new ScriptSummary(String.Format("Teleport{0} {1},{2}{3}", strGray, x, y, strMap));
            summary.Command = ScriptCommand.Teleport;
            summary.Symbol = "T";
            return summary;
        }

        private ScriptSummary EncounterSummary(bool bForNote, byte[] args, bool bConditional, BT3Monster[] monsters)
        {
            int iGroups = args[0];
            if (args[0] < 1 || args[0] > 4 || args[0] > args.Length * 2 + 1)
                return new ScriptSummary(bForNote ? "(Invalid encounter script)" : "<invalid>");

            StringBuilder sb = new StringBuilder(bForNote ? "Forced Encounter with " : "Enc ");
            for (int i = 0; i < iGroups; i++)
            {
                if (bForNote)
                    sb.AppendFormat("{0}, ", EncounterString(args, 0, monsters));
                else
                {
                    int iCount = args[i * 2 + 2];
                    int iMonster = args[i * 2 + 1];
                    sb.AppendFormat("#{0}:{1}, ", iMonster, iCount);
                }
            }
            Global.Trim(sb);
            if (bConditional)
                sb.Append(bForNote ? "\r\nVictory: " : String.Format("? Goto {0:X4}", Jump));

            return new ScriptSummary(sb.ToString(), ScriptCommand.Encounter, "E");
        }

        private string TeleportMap(BT3ScriptInfo info, bool bForNote, int iMap)
        {
            if (info.Map.Map == iMap)
                return "";
            if (bForNote)
                return String.Format("{0}", BT3MemoryHacker.GetMapTitlePair(iMap).Title);
            return String.Format(",Map#{0}", iMap);
        }

        private ScriptSummary IfHaveItemSummary(bool bForNote, BT3ItemIndex item, int iJump)
        {
            ScriptSummary summary = null;
            if (bForNote)
            {
                summary = new ScriptSummary(String.Format("If you do not have \"{0}\": ", BT3.ItemName(item)));
                summary.NoNewline = true;
                return summary;
            }
            summary = new ScriptSummary(String.Format("\"{0}\"? Goto {1:X4}", BT3.ItemName(item), iJump));
            return summary;
        }

        private ScriptSummary EndIfHaveItemSummary(bool bForNote, BT3ItemIndex item, int iJump)
        {
            ScriptSummary summary = null;
            if (bForNote)
            {
                summary = new ScriptSummary(String.Format("If you have \"{0}\": ", BT3.ItemName(item)));
                summary.NoNewline = true;
                return summary;
            }
            if (iJump == 0)
                summary = new ScriptSummary(String.Format("!\"{0}\"? Goto {1:X4}", BT3.ItemName(item), iJump));
            else
                summary = new ScriptSummary(String.Format("!\"{0}\"? Exit", BT3.ItemName(item)));
            return summary;
        }

        private ScriptSummary CheckScriptBitSummary(bool bForNote, BT3Opcode opcode, int iBit, int iJump)
        {
            if (bForNote)
            {
                switch (opcode)
                {
                    case BT3Opcode.ExitIfNotSet:
                    case BT3Opcode.BranchIfNotSetHigh:
                        return new ScriptSummary(String.Format("If script bit {0} is set:", iBit));
                    default:
                        return new ScriptSummary(String.Format("If script bit {0} is not set:", iBit));
                }
            }

            switch (opcode)
            {
                case BT3Opcode.ExitIfNotSet:
                    return new ScriptSummary(String.Format("!S{0}? Exit", iBit));
                case BT3Opcode.BranchIfNotSetHigh:
                    return new ScriptSummary(String.Format("!S{0}? Goto {1:X4}", iBit, iJump));
                case BT3Opcode.ExitIfSet:
                    return new ScriptSummary(String.Format("S{0}? Exit", iBit));
                case BT3Opcode.BranchIfSetHigh:
                    return new ScriptSummary(String.Format("S{0}? Goto {1:X4}", iBit, iJump));
                default: return new ScriptSummary("(Invalid opcode)");
            }
        }

        private ScriptSummary AlterMapSummary(bool bForNote, int x, int y, byte command)
        {
            StringBuilder sb = new StringBuilder();
            if (bForNote)
                sb.AppendFormat("Alter map at {0},{1}: Set {2}", x, y, AlterMapCommand(command));
            else
                sb.AppendFormat("{0} at {1},{2}", AlterMapCommand(command), x, y);

            return new ScriptSummary(sb.ToString(), ScriptCommand.AlterMap);
        }

        private ScriptSummary CheckLocationSummary(bool bForNote, byte[] args, int iJump)
        {
            StringBuilder sb = new StringBuilder();
            if (bForNote)
                sb.AppendFormat("If the party is between ({0},{1}) and ({2},{3}):", args[1], args[0], args[3], args[2]);
            else
            {
                sb.AppendFormat("NotIn {0},{1}-{2},{3}? ", args[1], args[0], args[3], args[2]);
                if (iJump != 0)
                    sb.AppendFormat("Goto {0:X4}", iJump);
                else
                    sb.AppendFormat("Exit");
            }

            return new ScriptSummary(sb.ToString(), ScriptCommand.CheckLocation);
        }

        private ScriptSummary ReceiveItemSummary(bool bForNote, BT3ItemIndex item, int iCharges)
        {
            StringBuilder sb = new StringBuilder();
            if (bForNote)
            {
                sb.AppendFormat("+Item \"{0}\"", BT3.ItemName(item));
                if (iCharges != 255)
                    sb.AppendFormat(" ({0} charges)", iCharges);
            }
            else
                sb.AppendFormat("+\"{0}\"", BT3.ItemName(item));
            ScriptSummary summary = new ScriptSummary(sb.ToString());
            summary.Command = ScriptCommand.Rewards;
            summary.Symbol = "L";
            return summary;
        }

        private string ExitJump(int iJump) { return iJump == 0 ? "Exit" : String.Format("Goto {0:X4}", iJump); }
        private string TrimText(string str) { return str == null ? "<null>" : str.Trim(); }

        private ScriptSummary SummaryString(BT3ScriptInfo info, bool bForNote)
        {
            byte[] args = Bytes.GetRange(1);

            int iExpectedArgs = BT3Script.NumArgs(FullOpcode);
            int iHaveArgs = (args == null ? 0 : args.Length);

            if (iHaveArgs < iExpectedArgs)
                return new ScriptSummary("BadArgs");

            switch (Opcode)
            {
                case BT3Opcode.StairsDown: return StairsSummary(info.StairsReversed ? DirectionFlags.Up : DirectionFlags.Down, info.Level.NextLevel(true, Location), bForNote);
                case BT3Opcode.StairsUp: return StairsSummary(info.StairsReversed ? DirectionFlags.Down : DirectionFlags.Up, info.Level.NextLevel(false, Location), bForNote);
                case BT3Opcode.Message:
                case BT3Opcode.ContMessage: return new ScriptSummary(TrimText(Text), ScriptCommand.Text, "s");
                case BT3Opcode.Teleport: return TeleportSummary(bForNote, false, args[1], args[0], TeleportMap(info, bForNote, ToMap(info.Map.Map, args[2])));
                case BT3Opcode.ReplaceExit: return new ScriptSummary(bForNote ? "" : String.Format("SetExit {0:X4}", Target));
                case BT3Opcode.Animation: return new ScriptSummary(String.Format("Anim #{0}", args[0]));
                case BT3Opcode.RunFunction: return new ScriptSummary(String.Format("Func #{0}", args[0]));
                case BT3Opcode.ViewportText: return new ScriptSummary(String.Format("View: {0}", Text));
                case BT3Opcode.CheckNPC: return new ScriptSummary(String.Format(bForNote ? "If \"{0}\" is in party: " :"NPC \"{0}\"? Goto {1:X4}", Text, Jump));
                case BT3Opcode.WaitKeypress: return new ScriptSummary(bForNote ? "" : "WaitKey");
                case BT3Opcode.AskYesNo: return new ScriptSummary(bForNote ? " (Y/N)?\r\nYes: " : FullOpcode == BT3Opcode.AskYesNoHigh ? String.Format("YesNo? Goto {0:X4}", Jump) : "YesNo");
                case BT3Opcode.AskCharacter: return AskCharacterSummary(bForNote);
                case BT3Opcode.ReadLine: return ReadLineSummary(bForNote);
                case BT3Opcode.CompareLine: return CompareLineSummary(bForNote);
                case BT3Opcode.TeleportGray: return TeleportSummary(bForNote, true, args[1], args[0], TeleportMap(info, bForNote, ToMap(info.Map.Map, args[2])));
                case BT3Opcode.ExitScript:
                case BT3Opcode.ExitScriptHigh: return new ScriptSummary(bForNote ? "" : "Exit");
                case BT3Opcode.ClearText: return new ScriptSummary(bForNote ? "" : "ClearText");
                case BT3Opcode.CheckAction: return new ScriptSummary(String.Format(bForNote ? "If you {0}: " : "{0}", GetAction(bForNote, args[0], Jump)));
                case BT3Opcode.AddNPC: return new ScriptSummary(String.Format(bForNote ? "If NPC \"{0}\" can't be added: " : "+NPC \"{0}\"? Goto {1:X4}", MonsterName(args[0], info.Monsters), Jump));
                case BT3Opcode.CheckClass: return new ScriptSummary(String.Format(bForNote ? "If character is a \"{0}\": " : "Is{0}? Goto {1:X4}", ClassString(args[0]), Jump));
                case BT3Opcode.RemoveHawkslayer: return new ScriptSummary(bForNote ? "(Remove Hawkslayer from the party)" : "-NPC \"Hawkslayer\"");
                case BT3Opcode.CheckItem: return new ScriptSummary(String.Format(bForNote ? "If you use \"{0}\": " : "Use \"{0}\"? {1}", BT3.ItemName((BT3ItemIndex) args[0]), ExitJump(Jump)));
                case BT3Opcode.CancelAction: return new ScriptSummary(bForNote ? "" : "CancelAction");
                case BT3Opcode.Unknown13: return new ScriptSummary(bForNote ? "" : "U13");
                case BT3Opcode.MessageIndirect:
                case BT3Opcode.MessageIndirect2: return new ScriptSummary(TrimText(Text2), ScriptCommand.Text, "s");
                case BT3Opcode.CheckContainer: return new ScriptSummary(bForNote ? 
                    String.Format("If container is not holding \"{0}\": ", ContainerContents(args[0])) :
                    String.Format("Contains \"{0}\"? Goto {1:X4}", ContainerContents(args[0]), Jump));
                case BT3Opcode.DelayUntilKey: return new ScriptSummary(bForNote ? "" : "DelayKey");
                case BT3Opcode.Unknown42: return new ScriptSummary(bForNote ? "" : "U42");
                case BT3Opcode.Unknown37: return new ScriptSummary(bForNote ? "" : "U37");
                case BT3Opcode.RemoveTree: return new ScriptSummary(bForNote ? "(Remove tree party is facing)" : "KillTree");
                case BT3Opcode.CheckCounter: return new ScriptSummary(String.Format(bForNote ? "If counter #{0} is not zero: " : "C{0}=0? Goto {1:X4}", args[0], Jump));
                case BT3Opcode.CompareCounterGold: return new ScriptSummary(String.Format(bForNote ? "If gold <= counter #{0}: " : "C{0}>Gold? Goto {1:X4}", args[0], Jump));
                case BT3Opcode.CompareCounterLess: return new ScriptSummary(String.Format(bForNote ? "If counter #{0} >= {1}: " : "C{0}>{1}? Goto {2:X4}", args[0], Target, Jump));
                case BT3Opcode.CompareCounterEqual: return new ScriptSummary(String.Format(bForNote ? "If counter #{0} is {1}: " : "C{0}={1}? Goto {2:X4}", args[0], Target, Jump));
                case BT3Opcode.PartyDamage: return new ScriptSummary(String.Format(bForNote ? "Party damage: {0}" : "-{0} HP", Target));
                case BT3Opcode.SetCounter: return new ScriptSummary(String.Format(bForNote ? "Set counter #{0} to {1}" : "C{0}={1}", args[0], Target));
                case BT3Opcode.AddCounter: return new ScriptSummary(String.Format(bForNote ? "Add {1} to counter #{0}" : "C{0}+={1}", args[0], Target));
                case BT3Opcode.DecreaseCounter: return new ScriptSummary(String.Format(bForNote ? "Decrease counter #{0}" : "C{0}--", args[0]));
                case BT3Opcode.LearnSpell: return new ScriptSummary((bForNote ? "Learn spell " : "Learn ") + SpellString(args[0], !bForNote));
                case BT3Opcode.RemoveItem: return new ScriptSummary(String.Format(bForNote ? "Lose item \"{0}\"" : "-Item \"{0}\"", BT3.ItemName((BT3ItemIndex)args[0])));
                case BT3Opcode.CheckLocation: return CheckLocationSummary(bForNote, args, Jump);
                case BT3Opcode.SetContents: return new ScriptSummary(String.Format(bForNote ? "Set the container's contents to \"{0}\"" : "SetContents \"{0}\"", ContainerContents(args[0])));
                case BT3Opcode.AddItemCharges: return new ScriptSummary(String.Format(bForNote ? "Add {0} charges to item" : "AddCharges {0}", args[0]));
                case BT3Opcode.SetItemCharges: return new ScriptSummary(String.Format(bForNote ? "Set item's charges to {0}" : "SetCharges {0}", args[0]));
                case BT3Opcode.RotateParty: return new ScriptSummary(String.Format(bForNote ? "(Rotate party to face {0})" : "Face{0}", BT3MemoryHacker.FacingString(args[0])));
                case BT3Opcode.Unknown3B: return new ScriptSummary(bForNote ? "" : "U3B");
                case BT3Opcode.Unknown3F: return new ScriptSummary(bForNote ? "" : "U3F");
                case BT3Opcode.AllowStairs: return new ScriptSummary(bForNote ? "" : "AllowStairs");
                case BT3Opcode.BackUp: return new ScriptSummary(bForNote ? "(Teleport back one square and turn around)" : "BackUp");
                case BT3Opcode.ExitIfSet:
                case BT3Opcode.ExitIfNotSet: return CheckScriptBitSummary(bForNote, FullOpcode, args[0], Jump);
                case BT3Opcode.SetScriptBit: return new ScriptSummary(bForNote ? 
                    String.Format("Set script bit {0}", args[0]) :
                    String.Format("+S{0}", args[0]));
                case BT3Opcode.ClearScriptBit: return new ScriptSummary(bForNote ? 
                    String.Format("Clear script bit {0}", args[0]) :
                    String.Format("-S{0}", args[0]));
                case BT3Opcode.StoreCounter: return new ScriptSummary(bForNote ? "" : String.Format("ToCounter{0}", args[0]));
                case BT3Opcode.AlterMap: return AlterMapSummary(bForNote, args[1], args[0], args[2]);
                case BT3Opcode.Encounter:
                case BT3Opcode.CondEncounter: return EncounterSummary(bForNote, args, Opcode == BT3Opcode.CondEncounter || HighOpcode, info.Monsters);
                case BT3Opcode.Jump: return new ScriptSummary(bForNote ? "" : String.Format("Goto {0:X4}", Jump));
                case BT3Opcode.IfHaveItem: return IfHaveItemSummary(bForNote, Item, Jump);
                case BT3Opcode.EndIfHaveItem: return EndIfHaveItemSummary(bForNote, Item, Jump);
                case BT3Opcode.ReceiveItem: return ReceiveItemSummary(bForNote, Item, args[2]);
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

            ScriptSummary summary = SummaryString(info as BT3ScriptInfo, bForNote);
            summary.IsExitCommand = IsEndOfScript;

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

    public class BT3Script : GameScript
    {
        private List<ScriptLine> m_lines;

        public override List<ScriptLine> Lines { get { return m_lines; } }
        public override bool HasHeaderBytes { get { return true; } }

        public int Offset;
        public byte[] Commands;
        public int MemoryOffset;

        public BT3Script(byte[] bytes, int iSquare, int iAddressOffset = 0, int iMemoryOffset = -1)
        {
            MemoryOffset = iMemoryOffset;

            Location = new Point(bytes[iSquare + 1] & 0x7F, bytes[iSquare] & 0x7F);
            AutoExecute = (bytes[iSquare] & 0x80) == 0;
            Offset = BitConverter.ToInt16(bytes, iSquare + 2);

            int iStart = Offset - iAddressOffset;
            BT3ScriptLine line = null;
            m_lines = new List<ScriptLine>();
            int iLineNumber = 0;
            int iCurrentByte = iStart;
            do
            {
                if (iCurrentByte < 0 || iCurrentByte > bytes.Length - 1)
                    break;
                line = ReadNextLine(bytes, iAddressOffset, ref iCurrentByte);
                if (line != null)
                {
                    line.Number = iLineNumber++;
                    m_lines.Add(line);
                    if (line.IsEndOfScript)
                    {
                        if (!m_lines.Any(l => ((BT3ScriptLine)l).Jump > line.Address))
                            break;
                        // Skip to the next jump address
                        HashSet<int> jumps = GetJumps(m_lines);
                        while (!jumps.Contains(iCurrentByte + iAddressOffset))
                        {
                            iCurrentByte++;
                            if (jumps.All(j => j < iCurrentByte + iAddressOffset))
                                break;  // Didn't find the exact value; give up
                        }
                    }
                }
            } while (line != null);

            Bytes = new MemoryBytes(Global.Subset(bytes, iStart, iCurrentByte - iStart), iMemoryOffset + iStart);
        }

        private HashSet<int> GetJumps(List<ScriptLine> lines)
        {
            HashSet<int> jumps = new HashSet<int>();
            foreach (BT3ScriptLine line in lines)
            {
                if (line.Jump != 0 && !jumps.Contains(line.Jump))
                    jumps.Add(line.Jump);
            }
            return jumps;
        }

        public static int NumArgs(BT3Opcode opcode)
        {
            switch (opcode & ~BT3Opcode.Continue)
            {
                case BT3Opcode.Animation:
                case BT3Opcode.RunFunction:
                case BT3Opcode.EndIfHaveItem:
                case BT3Opcode.DecreaseCounter:
                case BT3Opcode.LearnSpell:
                case BT3Opcode.RemoveItem:
                case BT3Opcode.SetContents:
                case BT3Opcode.AddItemCharges:
                case BT3Opcode.SetItemCharges:
                case BT3Opcode.CheckItem:
                case BT3Opcode.Unknown42:
                case BT3Opcode.Unknown3F:
                    return 1;
                case BT3Opcode.Unknown3B:
                case BT3Opcode.Jump:
                case BT3Opcode.PartyDamage:
                case BT3Opcode.ReplaceExit:
                case BT3Opcode.MessageIndirect:
                case BT3Opcode.MessageIndirect2:
                    return 2;
                case BT3Opcode.Teleport:
                case BT3Opcode.TeleportGray:
                case BT3Opcode.AlterMap:
                case BT3Opcode.IfHaveItem:
                case BT3Opcode.ReceiveItem:
                case BT3Opcode.CheckCounter:
                case BT3Opcode.CompareCounterGold:
                case BT3Opcode.SetCounter:
                case BT3Opcode.AddCounter:
                    return 3;
                case BT3Opcode.CheckLocation:
                    return 4;
                case BT3Opcode.CompareCounterLess:
                case BT3Opcode.CompareCounterEqual:
                    return 5;
                case BT3Opcode.Message:
                case BT3Opcode.ContMessage:
                case BT3Opcode.ViewportText:
                case BT3Opcode.CheckNPC:
                case BT3Opcode.Encounter:
                case BT3Opcode.CondEncounter:
                    return 1;   // Variable length
                case BT3Opcode.CompareLine:
                    return 3;   // 1 Variable length, 2 address bytes
                default:
                    switch (opcode)
                    {
                        // High-bit codes that include jump addresses
                        case BT3Opcode.DelayUntilKeyHigh:
                        case BT3Opcode.AskYesNoHigh:
                        case BT3Opcode.AskCharacterHigh:
                            return 2;
                        case BT3Opcode.ExitIfNotSet:
                        case BT3Opcode.ExitIfSet:
                            return 1;
                        case BT3Opcode.BranchIfNotSetHigh:
                        case BT3Opcode.BranchIfSetHigh:
                            return 3;
                        default:
                            return 0;
                    }
            }
        }

        public override void Summary(int iDepth, List<ScriptSummary> listSummary, ScriptInfo info, int iStartLine, bool bSkipSubscripts, bool bForNote)
        {
            foreach (BT3ScriptLine line in m_lines)
            {
                if (line.Number < iStartLine)
                    continue;

                listSummary.AddRange(line.Summary(info as ScriptInfo, bForNote));
            }
        }

        public static MapIcon BestSingleIcon(List<IconName> names)
        {
            if (names == null || names.Count < 1)
                return null;

            return new MapIcon(names[0]);
        }

        public static MapIcon BestSingleIcon(List<NoteInfo> notes)
        {
            if (notes == null || notes.Count < 1)
                return null;

            List<IconName> icons = new List<IconName>(notes.Count);
            foreach (NoteInfo note in notes)
                if (note.Icon != null)
                    icons.Add(note.Icon.Name);

            return BestSingleIcon(icons);
        }

        public static string BestSingleSymbol(List<string> symbols, List<string> skip = null)
        {
            if (symbols == null || symbols.Count < 1)
                return "?";

            string[] order = new string[] { ".", "?", "n", "e", "e!", "x", "t", "s", "T", "E", "L"};
            int iBest = 0;
            foreach (string strSymbol in symbols)
            {
                if (!order.Contains(strSymbol))
                    return strSymbol;   // Unique symbols take precedence over common ones
                for (int i = 0; i < order.Length; i++)
                {
                    if (order[i] == strSymbol && (skip == null || !skip.Contains(order[i])))
                    {
                        if (iBest < i)
                            iBest = i;
                    }
                }
            }

            return order[iBest];
        }

        public static string BestSingleSymbol(List<NoteInfo> notes, string strOriginal = "", List<string> skip = null)
        {
            if (notes == null || notes.Count < 1)
                return strOriginal;

            string strDigit = "";
            if (strOriginal.Length > 0 && Char.IsDigit(strOriginal[0]))
                strDigit = strOriginal.Substring(0,1);

            List<string> symbols = new List<string>(notes.Count);
            foreach (NoteInfo note in notes)
                symbols.Add(note.Symbol);

            if (!String.IsNullOrWhiteSpace(strOriginal) && strOriginal != strDigit)
                symbols.Add(strOriginal);

            string strBest = BestSingleSymbol(symbols, skip);

            if (strDigit != "" && strBest != strDigit && strBest.Length < 2)
                strBest += strDigit;

            return strBest;
        }

        private string Parenthesize(string str)
        {
            if (!str.Contains("("))
                return String.Format("({0})", str);
            StringBuilder sb = new StringBuilder(str);
            sb.Replace("(", "");
            sb.Replace(")", "");
            sb.Insert(0, '(');
            sb.Append(")");
            return sb.ToString();
        }

        public override NoteInfo Summary(ScriptInfo info, bool bSkipSubscripts, bool bForNote = false)
        {
            List<IconName> icons = new List<IconName>();
            List<string> symbols = new List<string>();

            List<ScriptSummary> listSummary = new List<ScriptSummary>();
            Summary(0, listSummary, info, 0, bSkipSubscripts, bForNote);

            StringBuilder sb = new StringBuilder();
            bool bConditional = false;
            bool bLastText = false;
            for(int i = 0; i < listSummary.Count; i++)
            {
                ScriptSummary summary = listSummary[i];
                if (summary.Command == ScriptCommand.Conditional)
                    bConditional = true;
                bool bLast = i == listSummary.Count - 1;
                if (bForNote)
                {
                    if (!String.IsNullOrWhiteSpace(summary.Description))
                    {
                        if (summary.Command == ScriptCommand.Teleport && bLastText && sb.Length > 2 && sb[sb.Length - 2] == '\r')
                        {
                            sb.Remove(sb.Length - 2, 2);
                            sb.AppendFormat(" {0}", Parenthesize(summary.Description));
                        }
                        else
                            sb.Append(summary.Description);
                        if (!summary.NoNewline)
                            sb.Append("\r\n");
                        if (summary.IsExitCommand && bConditional)
                        {
                            bConditional = false;
                            sb.Append("Otherwise: ");
                        }
                    }
                }
                else
                {
                    sb.AppendFormat("{0}; ", bLast ? summary.Description : Global.Abbreviate(summary.Description));
                }
                if (summary.Icon != IconName.None)
                    icons.Add(summary.Icon);
                if (!String.IsNullOrWhiteSpace(summary.Symbol))
                    symbols.Add(summary.Symbol);
                if (!String.IsNullOrWhiteSpace(summary.Description))
                    bLastText = summary.Command == ScriptCommand.Text;
            }
            if (!bForNote)
                Global.Trim(sb);

            return new NoteInfo(sb.ToString().Trim(), BestSingleSymbol(symbols), Color.Black, BestSingleIcon(icons));
        }

        private BT3ScriptLine ReadNextLine(byte[] bytes, int iAddressOffset, ref int iCurrentByte)
        {
            if (bytes == null || bytes.Length < 1)
                return null;

            int iScriptOffset = iCurrentByte;
            BT3Opcode opcode = (BT3Opcode) bytes[iCurrentByte++];
            byte[] command = new byte[] { (byte) opcode };
            string strText = null;
            string strText2 = null;
            int iJump = 0;
            int iEnd = 0;
            int iTarget = 0;
            int iGroups = 0;
            int iItem = 0;
            BT3TextDecoder decoder;
            bool bHigh = (opcode & BT3Opcode.Continue) == BT3Opcode.Continue;
            switch (opcode & ~BT3Opcode.Continue)
            {
                // No-Argument opcodes
                case BT3Opcode.StairsUp:
                case BT3Opcode.StairsDown:
                case BT3Opcode.ReadLine:
                case BT3Opcode.WaitKeypress:
                case BT3Opcode.ClearText:
                case BT3Opcode.Unknown13:
                case BT3Opcode.AllowStairs:
                case BT3Opcode.RemoveHawkslayer:
                case BT3Opcode.BackUp:
                    break;
                // Single-Argument opcodes
                case BT3Opcode.RotateParty:
                case BT3Opcode.Unknown42:
                case BT3Opcode.Unknown3F:
                case BT3Opcode.DecreaseCounter:
                case BT3Opcode.LearnSpell:
                case BT3Opcode.SetScriptBit:
                case BT3Opcode.ClearScriptBit:
                case BT3Opcode.StoreCounter:
                case BT3Opcode.Animation:
                case BT3Opcode.RunFunction:
                case BT3Opcode.SetContents:
                case BT3Opcode.AddItemCharges:
                case BT3Opcode.SetItemCharges:
                    command = Global.Subset(bytes, iCurrentByte - 1, 2);
                    iCurrentByte++;
                    break;
                case BT3Opcode.ExitIfSet:
                case BT3Opcode.ExitIfNotSet:
                    if (bHigh)
                    {
                        command = Global.Subset(bytes, iCurrentByte - 1, 4);
                        iCurrentByte += 3;
                        iJump = BitConverter.ToInt16(bytes, iCurrentByte - 2);
                    }
                    else
                    {
                        command = Global.Subset(bytes, iCurrentByte - 1, 2);
                        iCurrentByte++;
                    }
                    break;
                case BT3Opcode.EndIfHaveItem:
                case BT3Opcode.RemoveItem:
                    iItem = bytes[iCurrentByte];
                    command = Global.Subset(bytes, iCurrentByte - 1, 2);
                    iCurrentByte++;
                    break;
                // Two-Argument opcodes
                case BT3Opcode.Unknown3B:
                case BT3Opcode.Jump:
                    command = Global.Subset(bytes, iCurrentByte - 1, 3);
                    iCurrentByte += 2;
                    iJump = BitConverter.ToInt16(bytes, iCurrentByte - 2);
                    break;
                case BT3Opcode.MessageIndirect:
                case BT3Opcode.MessageIndirect2:
                    iTarget = BitConverter.ToInt16(bytes, iCurrentByte);
                    decoder = new BT3TextDecoder(bytes, iTarget - iAddressOffset);
                    strText2 = decoder.DecodedString;
                    command = Global.Subset(bytes, iCurrentByte - 1, 3);
                    iCurrentByte += command.Length - 1;
                    break;
                case BT3Opcode.PartyDamage:
                case BT3Opcode.ReplaceExit:
                    command = Global.Subset(bytes, iCurrentByte - 1, 3);
                    iTarget = BitConverter.ToInt16(bytes, iCurrentByte);
                    iCurrentByte += 2;
                    break;
                case BT3Opcode.DelayUntilKey:
                case BT3Opcode.AskYesNo:
                case BT3Opcode.AskCharacter:
                    if (bHigh)
                    {
                        command = Global.Subset(bytes, iCurrentByte - 1, 3);
                        iCurrentByte += 2;
                        iJump = BitConverter.ToInt16(bytes, iCurrentByte - 2);
                    }
                    break;
                // Three-Argument opcodes
                case BT3Opcode.CheckAction:
                case BT3Opcode.AddNPC:
                case BT3Opcode.CheckClass:
                    if (!bHigh)
                    {
                        command = Global.Subset(bytes, iCurrentByte - 1, 2);
                        iCurrentByte++;
                    }
                    else
                    {
                        command = Global.Subset(bytes, iCurrentByte - 1, 4);
                        iJump = (command[3] << 8) | command[2];
                        iCurrentByte += 3;
                    }
                    break;
                case BT3Opcode.SetCounter:
                case BT3Opcode.AddCounter:
                    command = Global.Subset(bytes, iCurrentByte - 1, 5);
                    iTarget = BitConverter.ToInt16(bytes, iCurrentByte + 1);
                    iCurrentByte += 3;
                    break;
                case BT3Opcode.Teleport:
                case BT3Opcode.TeleportGray:
                case BT3Opcode.AlterMap:
                    command = Global.Subset(bytes, iCurrentByte - 1, 4);
                    iCurrentByte += 3;
                    break;
                case BT3Opcode.CheckItem:
                    iItem = bytes[iCurrentByte];
                    if (bHigh)
                    {
                        command = Global.Subset(bytes, iCurrentByte - 1, 4);
                        iCurrentByte += 3;
                        iJump = BitConverter.ToInt16(bytes, iCurrentByte - 2);
                    }
                    else
                    {
                        command = Global.Subset(bytes, iCurrentByte - 1, 2);
                        iCurrentByte++;
                    }
                    break;
                case BT3Opcode.CheckContainer:
                case BT3Opcode.IfHaveItem:
                case BT3Opcode.CheckCounter:
                case BT3Opcode.CompareCounterGold:
                    iItem = bytes[iCurrentByte];
                    command = Global.Subset(bytes, iCurrentByte - 1, 4);
                    iCurrentByte += 3;
                    iJump = BitConverter.ToInt16(bytes, iCurrentByte - 2);
                    break;
                case BT3Opcode.CompareCounterLess:
                case BT3Opcode.CompareCounterEqual:
                    command = Global.Subset(bytes, iCurrentByte - 1, 5);
                    iCurrentByte += 5;
                    iTarget = BitConverter.ToInt16(bytes, iCurrentByte - 4);
                    iJump = BitConverter.ToInt16(bytes, iCurrentByte - 2);
                    break;
                case BT3Opcode.ReceiveItem:
                    iItem = bytes[iCurrentByte + 1];
                    command = Global.Subset(bytes, iCurrentByte - 1, 4);
                    iCurrentByte += 3;
                    break;
                // Four-Argument opcodes
                case BT3Opcode.CheckLocation:
                    command = Global.Subset(bytes, iCurrentByte - 1, 5);
                    iCurrentByte += 4;
                    break;
                // Variable-length commands
                case BT3Opcode.Encounter:
                    if (bHigh)
                        goto case BT3Opcode.CondEncounter;
                    iGroups = bytes[iCurrentByte++];
                    command = Global.Subset(bytes, iCurrentByte - 2, iGroups * 2 + 2);
                    iCurrentByte += iGroups * 2;
                    break;
                case BT3Opcode.CondEncounter:
                    iGroups = bytes[iCurrentByte++];
                    command = Global.Subset(bytes, iCurrentByte - 2, iGroups * 2 + 4);
                    iCurrentByte += iGroups * 2 + 2;
                    iJump = BitConverter.ToInt16(bytes, iCurrentByte - 2);
                    break;
                case BT3Opcode.Message:
                case BT3Opcode.ContMessage:
                    decoder = new BT3TextDecoder(bytes, iCurrentByte);
                    strText = decoder.DecodedString;
                    command = Global.Subset(bytes, iCurrentByte - 1, 1 + decoder.GetByteLength());
                    iCurrentByte += command.Length - 1;
                    break;
                case BT3Opcode.CompareLine:
                case BT3Opcode.CheckNPC:
                    iEnd = iCurrentByte;
                    while (iEnd < bytes.Length && bytes[iEnd] != 0xff)
                        iEnd++;
                    command = Global.Subset(bytes, iCurrentByte - 1, iEnd - iCurrentByte + 4);
                    strText = Global.GetLowAsciiString(bytes, iCurrentByte, iEnd - iCurrentByte);
                    iCurrentByte = iEnd + 3;
                    iJump = BitConverter.ToInt16(bytes, iCurrentByte - 2);
                    break;
                case BT3Opcode.ViewportText:
                    iEnd = iCurrentByte;
                    while (iEnd < bytes.Length && bytes[iEnd] != 0xff)
                        iEnd++;
                    command = Global.Subset(bytes, iCurrentByte - 1, iEnd - iCurrentByte + 2);
                    strText = Global.GetLowAsciiString(bytes, iCurrentByte, iEnd - iCurrentByte);
                    iCurrentByte = iEnd + 1;
                    break;
                default:
                    break;
            }

            BT3ScriptLine line = new BT3ScriptLine(MemoryOffset, iScriptOffset + iAddressOffset, Location, command);
            line.Text = strText;
            line.Text2 = strText2;
            line.Jump = iJump;
            line.Target = iTarget;
            line.Item = (BT3ItemIndex)iItem;
            return line;
        }
    }

    public class BT3Scripts : GameScripts
    {
        private int OriginalOffset = 0;
        private byte[] RawBytes;

        public override bool HasHeaderBytes { get { return true; } }

        public BT3Scripts(byte[] bytes, int iOffset = 0, int iOriginalOffset = 0, int iMemoryOffset = -1)
        {
            if (bytes == null || iOffset >= bytes.Length)
                return;
            OriginalOffset = iOriginalOffset;
            RawBytes = bytes;
            Scripts = new Dictionary<Point, List<GameScript>>();

            int iCount = bytes[iOffset];
            int iScriptCount = 0;

            for (int i = 0; i < iCount; i++)
            {
                int iSquare = i * 4 + iOffset + 1;
                if (iSquare >= bytes.Length - 3)
                    break;
                BT3Script script = new BT3Script(bytes, iSquare, OriginalOffset, iMemoryOffset);
                script.Index = iScriptCount++;
                if (!Scripts.ContainsKey(script.Location))
                    Scripts.Add(script.Location, new List<GameScript>(1));
                Scripts[script.Location].Add(script);
            }
        }
    }
}
