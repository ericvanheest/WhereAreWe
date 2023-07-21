using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum EOBMonsterSpecial
    {
        None =          0x00,
        Unknown01 =     0x01,
        ResFire =       0x02,
        ResSlash =      0x04,
        ResIce =        0x08,
        Poison =        0x10,
        Paralyze =      0x20,
        ResLightning =  0x40,
        Rust =          0x80,

        All = Poison | Paralyze | Rust,
        NotAll = ~All & 0xFF,
    }

    public enum EOBMonsterSize
    {
        Small = 0,
        Medium = 1,
        Large = 2,
    }

    public class EOBMonster : MMMonster
    {
        public const int Size = 28;
        public EOBItem PocketItem = null;
        public EOBItem Weapon = null;
        public DamageDice Attack1 = null;
        public DamageDice Attack2 = null;
        public DamageDice Attack3 = null;
        public EOBMonsterSpecial Special;
        public int UndeadPower;
        public byte[] SpecificBytes;
        public EOBMonsterSize MonsterSize;
        public int HitDice;

        public EOBMonster()
        {
            Name = "Invalid";
            NumAttacks = 1;
        }

        public override string OneLineDescription
        {
            get
            {
                return String.Format("{0}, {1}/{2}, AC{3}, {4}, {5} XP{6}{7}",
                    Name,
                    CurrentHP,
                    HP,
                    AC,
                    DamageString,
                    Experience,
                    Weapon == null ? "" : String.Format(", {0}", Weapon.Name),
                    PocketItem == null ? "" : String.Format(", {0}", PocketItem.Name)
                    );
            }
        }

        public static class Offsets
        {
        }

        public virtual byte[] GetBytes()
        {
            byte[] bytes = new byte[28];
            SetBytes(bytes);
            return bytes;
        }

        public virtual void SetBytes(byte[] bytes) { }

        public override string HPString(bool bPreEncounter) { return String.Format("{0}/{1}", CurrentHP, HP); }
        public override string ProperName { get { return Name; } }
        public override string GetMultiLineDescription(bool bActive) { return MultiLineDescription; }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("#{0}, {1} ({2})\r\n", Index, Name, MonsterSizeString);
                sb.AppendFormat("HP: {0}\r\n", HPString(true));
                sb.AppendFormat("AC: {0}\r\n", AC);
                sb.AppendFormat("Damage: {0}\r\n", DamageString);
                if (UndeadPower > -1)
                    sb.AppendFormat("Undead Level: {0}\r\n", UndeadPower);
                sb.AppendFormat("Experience: {0}\r\n", Experience);
                if (Weapon != null)
                    sb.AppendFormat("Weapon: {0}\r\n", Weapon.Name);
                if (PocketItem != null)
                    sb.AppendFormat("Pocket: {0}\r\n", PocketItem.Name);
                return sb.ToString();
            }
        }

        public string MonsterSizeString
        {
            get
            {
                switch (MonsterSize)
                {
                    case EOBMonsterSize.Small: return "Small";
                    case EOBMonsterSize.Medium: return "Medium";
                    case EOBMonsterSize.Large: return "Large";
                    default: return String.Format("Unknown({0})", (int)MonsterSize);
                }
            }
        }

        public override string AllPowersString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Special.HasFlag(EOBMonsterSpecial.Poison))
                    sb.Append("Poison, ");
                if (Special.HasFlag(EOBMonsterSpecial.Paralyze))
                    sb.Append("Paralyze, ");
                if (Special.HasFlag(EOBMonsterSpecial.Rust))
                    sb.Append("Rust, ");
                if (Special.HasFlag(EOBMonsterSpecial.Unknown01))
                    sb.Append("U01, ");
                if (UndeadPower > -1)
                    sb.AppendFormat("Undead:{0}, ", UndeadPower);
                return Global.Trim(sb).ToString();
            }
        }

        public override string ResistancesStringShort
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Special.HasFlag(EOBMonsterSpecial.Unknown01))
                    sb.Append("U01, ");
                if (Special.HasFlag(EOBMonsterSpecial.ResFire))
                    sb.Append("Fire, ");
                if (Special.HasFlag(EOBMonsterSpecial.ResSlash))
                    sb.Append("Slash, ");
                if (Special.HasFlag(EOBMonsterSpecial.ResIce))
                    sb.Append("Ice, ");
                if (Special.HasFlag(EOBMonsterSpecial.ResLightning))
                    sb.Append("Lightning, ");
                return Global.Trim(sb).ToString();
            }
        }

        public override string AccuracyString => MonsterSizeString;
    }

    public abstract class EOB123MonsterList : InternExternList
    {
        protected List<EOBMonster> m_monsters;
        public List<EOBMonster> InternalMonsters;
        public bool UsingInternalList = false;
        public int MonsterLength = 4;

        public virtual EOBMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return null; }

        public List<EOBMonster> Monsters
        {
            get
            {
                if (m_monsters == null)
                    InitInternalList();
                return m_monsters;
            }
        }

        public bool Reinitialize(EOBMemoryHacker hacker, bool bOverrideSanityCheck)
        {
            if (InitExternalList(hacker, bOverrideSanityCheck))
                return true;

            InitInternalList();
            return false;
        }

        public abstract List<EOBMonster> SetFromBytes(byte[] bytes);

        public override bool InitExternalList(MemoryHacker hacker, bool bOverrideSanityCheck = false)
        {
            if (!hacker.IsValid)
                return false;

            byte[] bytes = GetExternalBytes(hacker);
            if (bytes == null)
                return false;

            if (!bOverrideSanityCheck)
            {
                if (!Global.CompareBytes(bytes, GetInternalBytes(), 0, 0, hacker.MonsterListEntrySize))
                    return false;
            }

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
