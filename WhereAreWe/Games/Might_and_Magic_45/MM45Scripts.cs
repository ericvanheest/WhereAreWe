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
    public class MM45ScriptLine : MM345ScriptLine
    {
        public MM45ScriptLine(MM345SpecialSquare square)
        {
            Bytes = new MemoryBytes(square.Action, square.Offset);
            Location = new Point(square.X, square.Y);
            Facing = square.Facing;
            Number = square.SubIndex;
            Parent = null;
            Address = square.Offset;
        }

        public override MM345Command CreateCommand(MMScriptInfo info, DirectionFlags facing, Point location) { return new MM45Command(info, facing, location); }
    }

    public class MM45Script : MM345Script
    {
        public MM45Script()
        {
            m_lines = new List<ScriptLine>();
        }

        public MM45Script(int index, List<MM345SpecialSquare> squares)
        {
            m_lines = new List<ScriptLine>(squares.Count);
            Index = index;

            foreach (MM345SpecialSquare square in squares)
            {
                if (square.AutoExecute)
                    AutoExecute = true;
                m_lines.Add(new MM45ScriptLine(square));
            }

            if (m_lines.Count > 0)
            {
                Location = m_lines[0].Location;
                Facing = m_lines[0].Facing;
            }
        }
    }

    public class MM45Scripts : MM345Scripts
    {
        public MM45Scripts(Dictionary<Point, List<MM345SpecialSquare>> squares)
        {
            if (squares == null)
                return;
            Scripts = new Dictionary<Point, List<GameScript>>(squares.Count);
            int iIndex = 0;
            foreach (Point pt in squares.Keys)
            {
                Scripts.Add(pt, new List<GameScript>(1));
                Scripts[pt].Add(new MM45Script(iIndex++, squares[pt]));
            }
        }
    }

    public class MM45Command : MM345Command
    {
        public override MapTitleInfo GetMapTitlePair(int index) { return MM45MemoryHacker.GetMapTitlePair(index % 256, index / 256); }

        public MM45Command(MMScriptInfo info, DirectionFlags facing, Point location) : base(info, facing, location) { }
        protected override List<MMMonster> Monsters { get { return m_info.MonsterListIndex == 1 ? MM45.MM5Monsters : MM45.MM4Monsters; } }
        protected override string AwardString(byte b) { return MM45Character.AwardString((MM45AwardIndex)b); }
        protected override string SpellString(byte b) { return MM45SpellList.GetSpellName((MM45InternalSpellIndex)b); }

        protected override string ItemString(byte b)
        {
            return MM45Item.FromScriptBytes(new byte[] { b, 0, 0, 0 }).DescriptionString;
        }

        public override string BusinessString(byte b)
        {
            switch (b)
            {
                case 0: return "Bank";
                case 1: return "Store";
                case 2: return "Guild";
                case 3: return "Inn";
                case 4: return "Temple";
                case 5: return "Training";
                case 6: return "WarZone";
                case 8: return "Tower";
                case 9: return "Dungeon";
                case 10: return "Red Dwarf Range";
                case 11: return "Sphinx";
                case 12: return "Pyramid";
                case 13: return "Town";
                default: return String.Format("Unknown({0})", b);
            }
        }

        public static string PastPerfect(string strVerb)
        {
            switch (strVerb.ToLower())
            {
                case "drink": return "drunk";
                case "read": return "read";
                case "take": return "taken";
                case "steal": return "stolen";
                case "sit": return "sat";
                case "try": return "tried";
                case "toss a coin": return "tossed a coin";
                case "rub": return "rubbed";
                case "eat": return "eaten";
                default:
                    if (strVerb.EndsWith("e"))
                        return strVerb + "d";
                    return strVerb + "ed";
            }
        }

        private string WhoWillAction(byte b)
        {
            switch (b)
            {
                case 0: return "search";
                case 1: return "open";
                case 2: return "drink";
                case 3: return "mine";
                case 4: return "touch";
                case 5: return "read";
                case 6: return "learn";
                case 7: return "take";
                case 8: return "bang";
                case 9: return "steal";
                case 10: return "bribe";
                case 11: return "pay";
                case 12: return "sit";
                case 13: return "try";
                case 14: return "turn";
                case 15: return "bathe";
                case 16: return "destroy";
                case 17: return "pull";
                case 18: return "descend";
                case 19: return "toss a coin";
                case 20: return "pray";
                case 21: return "join";
                case 22: return "act";
                case 23: return "play";
                case 24: return "push";
                case 25: return "rub";
                case 26: return "pick";
                case 27: return "eat";
                case 28: return "sign";
                case 29: return "close";
                case 30: return "look";
                default: return "<unknown>";
            }
        }
        
        protected override ScriptSummary PrintF()
        {
            if (m_args.Length < 2)
                return new ScriptSummary("WhoWill(invalid args)", ScriptCommand.WhoWill);

            string strAction = WhoWillAction(m_args[0]);

            if (m_bAbbrev)
                return new ScriptSummary(Global.Title(strAction + "?"), ScriptCommand.WhoWill);
            if (m_bForNote)
                return new ScriptSummary(String.Format("{0}, who will {1}?", MapString(m_args[1]), strAction), ScriptCommand.WhoWill, String.Empty, strAction);
            return new ScriptSummary(String.Format("Ask and Set Character: {0}, who will {1}?", MapString(m_args[1]), strAction), ScriptCommand.WhoWill, String.Empty, strAction);
        }

        public override string FacingString(int i, bool bAbbrev = false) { return MM45MemoryHacker.FacingString(i, bAbbrev); }
        public override DirectionFlags FacingDirection(int i) { return MM45MemoryHacker.FacingDirection(i); }

        protected override ScriptSummary AddLE()
        {
            // In MM4/5 this command has been replaced with "RandomDamage"
            ScriptSummary ss = new ScriptSummary(String.Empty);
            ss.Command = ScriptCommand.Damage;

            if (m_bAbbrev)
                return ss.NewDescription(String.Format("RDmg({0})", SafeUInt16(m_args, 1)));

            if (m_args[0] == 7)
                return ss.NewDescription("Damage: Death");
            return ss.NewDescription(String.Format("{0} Damage: 0-{1}", MMMonster.GetDamageTypeString((DamageType)m_args[0]), SafeUInt16(m_args, 1)));
        }
    }
}
