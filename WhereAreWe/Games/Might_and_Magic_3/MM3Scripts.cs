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
    public enum MM345ScriptAttribute
    {
        Invalid = -1,
        Nothing = 0,
        Sex = 3,
        Race = 4,
        Class = 5,
        Alignment = 6,
        CurrentHP = 8,
        CurrentSP = 9,
        TempArmorClass = 10,
        TempLevel = 11,
        TempAge = 12,
        Skill = 13,
        Award = 15,
        Experience = 16,
        PoisonRes = 17,
        Condition = 18,
        Spell = 19,
        PartyBit = 20,
        Item = 21,
        Hireling = 23,
        Minutes = 25,
        Gold = 34,
        Gems = 35,
        TempMight = 37,
        TempIntellect = 38,
        TempPersonality = 39,
        TempEndurance = 40,
        TempSpeed = 41,
        TempAccuracy = 42,
        TempLuck = 43,
        AskYesNo = 44,
        PermMight = 45,
        PermIntellect = 46,
        PermPersonality = 47,
        PermEndurance = 48,
        PermSpeed = 49,
        PermAccuracy = 50,
        PermLuck = 51,
        PermFireRes = 52,
        PermElectricityRes = 53,
        PermColdRes = 54,
        PermPoisonRes = 55,
        PermEnergyRes = 56,
        PermMagicRes = 57,
        TempFireRes = 58,
        TempElectricityRes = 59,
        TempColdRes = 60,
        TempPoisonRes = 61,
        TempEnergyRes = 62,
        TempMagicRes = 63,
        PermLevel = 64,
        Food = 65,
        RandomItem = 66,
        Scroll = 67,
        Levitate = 69,
        Light = 70,
        FireRes = 71,
        ElecRes = 72,
        ColdRes = 73,
        Protection = 74,
        Days = 76,
        TempArmorClass2 = 77,
        HPOverMaxHP = 78,
        WizardEye = 79,
        SPOverMaxSP = 81,
        PhysicalDamage = 82,
        DamageType = 83,
        Facing = 84,
        GameYear = 85,
        TotalMight = 86,
        TotalIntellect = 87,
        TotalPersonality = 88,
        TotalEndurance = 89,
        TotalSpeed = 90,
        TotalAccuracy = 91,
        TotalLuck = 92,
        DayOfWeek = 93,
        WalkOnWater = 94,
        SkullsToKranion = 95,
        OrbsToZealot = 96,
        OrbsToTumult = 97,
        OrbsToMalefactor = 98,
        PartySkill = 99,
        RandomGold = 100,
        RandomGems = 101,
        Thievery = 102,
        WorldBit = 103,
        QuestBit = 104,
        MegaCredits = 105,
        RandomFood = 106,
        CharBit = 107,
    }

    public class MM3ScriptLine : MM345ScriptLine
    {
        public MM3ScriptLine(MM3SpecialSquare square)
        {
            Bytes = new MemoryBytes(square.Action, square.Offset);
            Location = new Point(square.X, square.Y);
            Facing = square.Facing;
            Number = square.SubIndex;
            Parent = null;
        }

        public override MM345Command CreateCommand(MMScriptInfo info, DirectionFlags facing, Point location) { return new MM3Command(info, facing, location); }
    }

    public class MM3Script : MM345Script
    {
        public MM3Script()
        {
            m_lines = new List<ScriptLine>();
        }

        public MM3Script(int index, List<MM345SpecialSquare> squares)
        {
            m_lines = new List<ScriptLine>(squares.Count);
            Index = index;

            foreach (MM3SpecialSquare square in squares)
            {
                if (square.AutoExecute)
                    AutoExecute = true;
                m_lines.Add(new MM3ScriptLine(square));
            }

            if (m_lines.Count > 0)
            {
                Location = m_lines[0].Location;
                Facing = m_lines[0].Facing;
            }
        }
    }

    public class MM3Scripts : MM345Scripts
    {
        public MM3Scripts(Dictionary<Point, List<MM345SpecialSquare>> squares)
        {
            Scripts = new Dictionary<Point, List<GameScript>>(squares.Count);
            int iIndex = 0;
            foreach (Point pt in squares.Keys)
            {
                Scripts.Add(pt, new List<GameScript>(1));
                    Scripts[pt].Add(new MM3Script(iIndex++, squares[pt]));
            }
        }
    }

    public class MM3Command : MM345Command
    {
        public override MapTitleInfo GetMapTitlePair(int index) { return MM3MemoryHacker.GetMapTitlePair(index); }

        public MM3Command(MMScriptInfo info, DirectionFlags facing, Point location) : base(info, facing, location) { }
        protected override List<MMMonster> Monsters { get { return MM3.Monsters; } }
        protected override string AwardString(byte b) { return MM3Character.AwardString((MM3AwardIndex)b); }
        protected override string ItemString(byte b) { return MM3Item.GetItemName((MM3ItemIndex)b); }
        protected override string SpellString(byte b) { return MM3SpellList.GetSpellName((MM3InternalSpellIndex)b); }
    }
}
