using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum BTMonsterSpecial
    {
        None = 0,
        Poison,
        Insanity,
        DrainLevel,
        Wither,
        Possess,
        Critical,
        Stone
    }

    public abstract class BTMonster : Monster
    {
        public DamageDice HPDice;
        public DamageDice DamDice;
        public BTMonsterSpecial Special;
        public virtual string TouchString { get { return GetMonsterSpecial(Special); } }

        public const int Size = 4;

        public virtual string GetName(int index) { return "Unknown"; }
        public abstract void SetBytes(int index, byte[] bytes, int offset = 0);

        public static string GetMonsterSpecial(BTMonsterSpecial index)
        {
            switch (index)
            {
                case BTMonsterSpecial.None: return String.Empty;
                case BTMonsterSpecial.Critical: return "Critical";
                case BTMonsterSpecial.DrainLevel: return "Drain Level";
                case BTMonsterSpecial.Wither: return "Wither";
                case BTMonsterSpecial.Poison: return "Poison";
                case BTMonsterSpecial.Possess: return "Possess";
                case BTMonsterSpecial.Insanity: return "Insane";
                case BTMonsterSpecial.Stone: return "Stone";

                default:
                    return String.Format("Unknown({0})", (int)index);
            }
        }

        public virtual string GetAttackString(int iAttack, bool bAbbrev) { return "N/A"; }

        public BTMonster()
        {
            Name = "Invalid";
            NumAttacks = 1;
        }

        public override string OneLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ", ProperName);
                return Global.Trim(sb).ToString();
            }
        }

        public static class Offsets
        {
        }

        public virtual byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            return ms.ToArray();
        }

        public override string HPString(bool bPreEncounter) { return bPreEncounter ? (HPDice == null ? "" : HPDice.ToString()) : CurrentHP.ToString(); }
        public override string DamageString { get { return new BasicDamage(NumAttacks, DamDice).ToString(); } }

        public override string AllPowersString
        {
            get
            {
                switch (Special)
                {
                    case BTMonsterSpecial.Critical: return "Critical";
                    case BTMonsterSpecial.DrainLevel: return "Drain Level";
                    case BTMonsterSpecial.Insanity: return "Insanity";
                    case BTMonsterSpecial.Poison: return "Poison";
                    case BTMonsterSpecial.Possess: return "Possess";
                    case BTMonsterSpecial.Stone: return "Stone";
                    case BTMonsterSpecial.Wither: return "Wither";
                    default: return String.Empty;
                }
            }
        }

        public override int InternalIndex { get { return Index; } }
        public override string ProperName { get { return Name; } }
        public override string TreasureStringShort { get { return GroupSize.ToString(); } }
        public override string GetMultiLineDescription(bool bActive) { return MultiLineDescription; }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("#{0}, {1}\r\n", Index, Name);
                if (HPDice == null)
                {
                    sb.AppendLine("(Invalid Monster Record)");
                    return sb.ToString();
                }
                sb.AppendFormat("HP: {0}, Group Size: {1}\r\n", HPString(true), GroupSize);
                sb.AppendFormat("AC: {0}\r\n", AC);
                sb.AppendFormat("Damage: {0}\r\n", DamageString);
                sb.AppendFormat("Experience: {0}\r\n", Experience);
                if (Special != BTMonsterSpecial.None)
                {
                    sb.AppendFormat("Special: {0}\r\n", AllPowersString);
                }
                return sb.ToString();
            }
        }

        public override double AverageHP { get { return HPDice.Average; } }
    }

    public class BTTrapInfo : TrapInfo
    {
        public enum BT12Trap
        {
            TraplessChest = 0,
            PoisonNeedle = 1,
            Blades = 2,
            Darts = 3,
            GasCloud = 4,
            Shocker = 5,
            CrazyCloud = 6,
            MindTrap = 7,

            Last
        }

        public static string GetBT12TrapName(BT12Trap trap)
        {
            switch (trap)
            {
                case BT12Trap.PoisonNeedle: return "Poison Needle";
                case BT12Trap.Blades: return "Blades";
                case BT12Trap.Darts: return "Darts";
                case BT12Trap.GasCloud: return "Gas Cloud";
                case BT12Trap.Shocker: return "Shocker";
                case BT12Trap.CrazyCloud: return "Crazy Cloud";
                case BT12Trap.MindTrap: return "Mind Trap";
                case BT12Trap.TraplessChest: return "Untrapped";
                default: return "Unknown";
            }
        }

        public BTTrapInfo()
        {
        }

        public BTTrapInfo(int trap)
        {
            Trap = trap;
        }

        public override string GetTrapName(int iTrap) { return GetBT12TrapName((BT12Trap) iTrap); }
        public override int TrapCount { get { return (int) BT12Trap.Last; } }
    }

    public abstract class BT123MonsterList : InternExternList
    {
        protected List<BTMonster> m_monsters;
        public List<BTMonster> InternalMonsters;
        public bool UsingInternalList = false;
        public int MonsterLength = 4;

        public virtual BTMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return null; }

        public List<BTMonster> Monsters
        {
            get
            {
                if (m_monsters == null)
                    InitInternalList();
                return m_monsters;
            }
        }

        public bool Reinitialize(BTMemoryHacker hacker, bool bOverrideSanityCheck)
        {
            if (InitExternalList(hacker, bOverrideSanityCheck))
                return true;

            InitInternalList();
            return false;
        }

        public abstract List<BTMonster> SetFromBytes(byte[] bytes);

        public override bool InitExternalList(MemoryHacker hacker, bool bOverrideSanityCheck = false)
        {
            if (!hacker.IsValid)
                return false;

            byte[] bytes = GetExternalBytes(hacker);
            if (bytes == null)
                return false;

            m_monsters = SetFromBytes(bytes);

            UsingInternalList = false;
            return true;
        }

        public override bool InitInternalList()
        {
            InternalMonsters = SetFromBytes(GetInternalBytes());
            m_monsters = InternalMonsters;
            UsingInternalList = true;
            return true;
        }
    }
}
