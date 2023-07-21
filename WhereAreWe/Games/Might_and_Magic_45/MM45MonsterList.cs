using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum MM45Side
    {
        Clouds = 0,
        Darkside = 1,
        Unknown = -1
    }

    public enum MM45RaceClass
    {
        Unknown = -1,
        First = 0,
        Knight = 0,
        Paladin = 1,
        Archer = 2,
        Cleric = 3,
        Sorcerer = 4,
        Robber = 5,
        Barbarian = 6,
        Ninja = 7,
        Druid = 8,
        Ranger = 9,
        Human = 10,
        Elf = 11,
        Dwarf = 12,
        Gnome = 13,
        HalfOrc = 14,
        All = 15,
        Any = 16,
        Last = 17,
    }

    public enum MM45SpecialAttack
    {
        Unknown = -1,
        First = 0,
        None = 0,
        Poison = 5,
        Disease = 7,
        Insane = 8,
        Sleep = 9,
        CurseItem = 10,
        None11 = 11,
        DrainSP = 12,
        Curse = 13,
        Paralyze = 14,
        Knockout = 15,
        Confuse = 16,
        BreakWeapon = 17,
        Weakness = 18,
        Eradicate = 19,
        Age = 20,
        Kill = 21,
        Stone = 22,
        Last = 23
    }

    public enum MM45MonsterType
    {
        Unknown = -1,
        First = 0,
        Unique = 0,
        Animal = 1,
        Insect = 2,
        Humanoid = 3,
        Undead = 4,
        Golem = 5,
        Dragon = 6,
        Last = 7
    }

    public class MM45Monster : MM345Monster
    {
        public MM45Side Side;
        public MM45RaceClass HatesClass;
        public MM45SpecialAttack SpecialAttack;
        public MM45MonsterType MonsterType;
        public int ImageNumber;
        public bool LoopAnimation;
        public int AnimationEffect;
        public int IdleSound;
        public string AttackVoc;

        public MM45Monster(int index, byte[] bytes, int offset, MM45Side side)
        {
            if (bytes == null || bytes.Length - offset < 60)
                return;

            MM345Condition = MM345MonsterCondition.Good;

            RawBytes = new byte[60];
            Buffer.BlockCopy(bytes, offset, RawBytes, 0, 60);

            ASCIIEncoding ascii = new ASCIIEncoding();
            int iEnd = offset;
            while (iEnd < offset + 15 && bytes[iEnd] != 0)
                iEnd++;

            Index = index;
            Name = ascii.GetString(bytes, offset, iEnd - offset);
            Experience = BitConverter.ToInt32(bytes, offset+16);
            HP = BitConverter.ToUInt16(bytes, offset + 20);
            AC = bytes[offset + 22];
            Speed = bytes[offset + 23];
            NumAttacks = bytes[offset + 24];
            HatesClass = (MM45RaceClass)bytes[offset + 25];
            DamageNumDice = BitConverter.ToUInt16(bytes, offset + 26);
            DamageDieMax = bytes[offset + 28];
            Damage = DamageNumDice * DamageDieMax;
            DamageMin = 1;
            DamageType = (DamageType)bytes[offset + 29];
            SpecialAttack = (MM45SpecialAttack)bytes[offset + 30];
            Accuracy = bytes[offset + 31];
            Missile = bytes[offset + 32] != 0;
            MonsterType = (MM45MonsterType)bytes[offset + 33];
            Resistances = new Resistances(
                bytes[offset + 40], bytes[offset + 34], bytes[offset + 36],
                bytes[offset + 35], bytes[offset + 37], bytes[offset + 38], bytes[offset + 39]);
            MagicResist = bytes[offset + 39];
            Gold = BitConverter.ToUInt16(bytes, offset + 42);
            Gems = bytes[offset + 44];
            Items = new int[] { bytes[offset + 45] };
            Flying = bytes[offset + 46] != 0;
            ImageNumber = bytes[offset + 47];
            LoopAnimation = bytes[offset + 48] != 0;
            AnimationEffect = bytes[offset + 49];
            IdleSound = bytes[offset + 50];
            Undead = (MonsterType == MM45MonsterType.Undead);
            Side = side;

            iEnd = offset + 51;
            while (iEnd < offset + 60 && bytes[iEnd] != 0)
                iEnd++;
            AttackVoc = ascii.GetString(bytes, offset + 51, iEnd - offset - 51);
        }

        public static MM45Monster Dummy
        {
            get
            {
                byte[] bytes = Global.ByteArray(60, 0);
                MM45Monster monster = new MM45Monster(0, bytes, 0, MM45Side.Darkside);
                monster.Name = "<invalid>";
                return monster;
            }
        }

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream(60);
            ASCIIEncoding ascii = new ASCIIEncoding();
            byte[] bytesName = ascii.GetBytes(Name.Length < 15 ? Name : Name.Substring(0, 14));
            byte[] bytesPadding = Global.NullBytes(16 - bytesName.Length);
            ms.Write(bytesName, 0, bytesName.Length);
            ms.Write(bytesPadding, 0, bytesPadding.Length);
            ms.Write(BitConverter.GetBytes((UInt32) Experience), 0, 4);
            ms.Write(BitConverter.GetBytes((UInt16)HP), 0, 2);
            ms.WriteByte((byte)AC);
            ms.WriteByte((byte)Speed);
            ms.WriteByte((byte)NumAttacks);
            ms.WriteByte((byte)HatesClass);
            ms.Write(BitConverter.GetBytes((UInt16)DamageNumDice), 0, 2);
            ms.WriteByte((byte)DamageDieMax);
            ms.WriteByte((byte)DamageType);
            ms.WriteByte((byte)SpecialAttack);
            ms.WriteByte((byte)Accuracy);
            ms.WriteByte((byte)(Missile ? 1 : 0));
            ms.WriteByte((byte)MonsterType);
            ms.WriteByte((byte)Resistances.Fire);
            ms.WriteByte((byte)Resistances.Electricity);
            ms.WriteByte((byte)Resistances.Cold);
            ms.WriteByte((byte)Resistances.Poison);
            ms.WriteByte((byte)Resistances.Energy);
            ms.WriteByte((byte)Resistances.Magic);
            ms.WriteByte((byte)Resistances.Physical);
            ms.WriteByte(0);
            ms.Write(BitConverter.GetBytes((UInt16)Gold), 0, 2);
            ms.WriteByte((byte)Gems);
            ms.WriteByte((byte)Items[0]);
            ms.WriteByte((byte)(Flying ? 1 : 0));
            ms.WriteByte((byte)ImageNumber);
            ms.WriteByte((byte)(LoopAnimation ? 1 : 0));
            ms.WriteByte((byte)AnimationEffect);
            ms.WriteByte((byte)IdleSound);
            bytesName = ascii.GetBytes(AttackVoc.Length < 9 ? AttackVoc : AttackVoc.Substring(0, 8));
            bytesPadding = Global.NullBytes(9 - bytesName.Length);
            ms.Write(bytesName, 0, bytesName.Length);
            ms.Write(bytesPadding, 0, bytesPadding.Length);

            return ms.ToArray();
        }

        public override string IndexString { get { return String.Format("{0}-{1}", Side == MM45Side.Clouds ? "C" : "D", InternalIndex); } }

        public static string GetMonsterTypeString(MM45MonsterType mType)
        {
            switch (mType)
            {
                case MM45MonsterType.Animal: return "Animal";
                case MM45MonsterType.Dragon: return "Dragon";
                case MM45MonsterType.Golem: return "Golem";
                case MM45MonsterType.Humanoid: return "Humanoid";
                case MM45MonsterType.Insect: return "Insect";
                case MM45MonsterType.Undead: return "Undead";
                case MM45MonsterType.Unique: return "Unique";
                default: return "Unknown Type";
            }
        }

        public static string GetMonsterTypeShort(MM45MonsterType mType)
        {
            switch (mType)
            {
                case MM45MonsterType.Animal: return " (A)";
                case MM45MonsterType.Dragon: return " (D)";
                case MM45MonsterType.Golem: return " (G)";
                case MM45MonsterType.Humanoid: return " (H)";
                case MM45MonsterType.Insect: return " (I)";
                case MM45MonsterType.Undead: return " (U)";
                default: return String.Empty;
            }
        }

        public string HatesString { get { return GetHatesString(HatesClass); } }

        public static string GetHatesString(MM45RaceClass target)
        {
            switch (target)
            {
                case MM45RaceClass.All: return "All";
                case MM45RaceClass.Knight: return "Knight";
                case MM45RaceClass.Paladin: return "Paladin";
                case MM45RaceClass.Archer: return "Archer";
                case MM45RaceClass.Cleric: return "Cleric";
                case MM45RaceClass.Sorcerer: return "Sorcerer";
                case MM45RaceClass.Robber: return "Robber";
                case MM45RaceClass.Barbarian: return "Barbarian";
                case MM45RaceClass.Ninja: return "Ninja";
                case MM45RaceClass.Druid: return "Druid";
                case MM45RaceClass.Ranger: return "Ranger";
                case MM45RaceClass.Human: return "Human";
                case MM45RaceClass.Elf: return "Elf";
                case MM45RaceClass.Dwarf: return "Dwarf";
                case MM45RaceClass.Gnome: return "Gnome";
                case MM45RaceClass.HalfOrc: return "HalfOrc";
                default: return "";
            }
        }

        public string HatesStringShort
        {
            get
            {
                switch (HatesClass)
                {
                    case MM45RaceClass.All: return "All";
                    case MM45RaceClass.Knight: return "Kn";
                    case MM45RaceClass.Paladin: return "Pa";
                    case MM45RaceClass.Archer: return "Ar";
                    case MM45RaceClass.Cleric: return "Cl";
                    case MM45RaceClass.Sorcerer: return "So";
                    case MM45RaceClass.Robber: return "Ro";
                    case MM45RaceClass.Barbarian: return "Ba";
                    case MM45RaceClass.Ninja: return "Ni";
                    case MM45RaceClass.Druid: return "Dr";
                    case MM45RaceClass.Ranger: return "Ra";
                    case MM45RaceClass.Human: return "Hu";
                    case MM45RaceClass.Elf: return "El";
                    case MM45RaceClass.Dwarf: return "Dw";
                    case MM45RaceClass.Gnome: return "Gn";
                    case MM45RaceClass.HalfOrc: return "HO";
                    default: return "";
                }
            }
        }

        public override string DamageString
        {
            get
            {
                string sType = DamageType == DamageType.Physical ? "" : String.Format(", {0}", DamageTypeStringShort);
                StringBuilder sb = new StringBuilder();
                if (NumAttacks > 1)
                    sb.AppendFormat("{0}x ", NumAttacks);
                sb.AppendFormat("{0}d{1}", DamageNumDice, DamageDieMax);
                sb.Append(sType);
                if (HatesClass != MM45RaceClass.Any)
                    sb.AppendFormat(" T:{0}", HatesStringShort);
                return sb.ToString();
            }
        }

        public string DamageStringFull
        {
            get
            {
                string sType = DamageType == DamageType.Physical ? "" : String.Format(", {0}", DamageTypeString);
                StringBuilder sb = new StringBuilder();
                if (NumAttacks > 1)
                    sb.AppendFormat("{0}x ", NumAttacks);
                sb.AppendFormat("{0}d{1}", DamageNumDice, DamageDieMax);
                sb.Append(sType);
                if (HatesClass != MM45RaceClass.Any)
                    sb.AppendFormat(" ({0})", HatesString);
                return sb.ToString();
            }
        }

        public override string OneLineDescription
        {
            get
            {
                return String.Format("{0}{1}{2}, {3}/{4}, AC{5}, {6}, Speed:{7}, Accuracy:{8}, Exp:{9}{10}{11}",
                    Name,
                    GetMonsterTypeShort(MonsterType),
                    MM345Condition == MM345MonsterCondition.Good ? "" : String.Format(" ({0})", ConditionString(MM345Condition)),
                    CurrentHP,
                    HP,
                    AC,
                    DamageString,
                    Speed,
                    Accuracy,
                    Experience,
                    HasSpecialAttack ? ", " : "",
                    GetSpecialAttackString(SpecialAttack, false)
                    );
            }
        }

        public bool HasSpecialAttack { get { return (SpecialAttack != MM45SpecialAttack.None && SpecialAttack != MM45SpecialAttack.None11); } }

        public override Monster Clone()
        {
            return new MM45Monster(Index, GetBytes(), 0, Side);
        }

        public override string ResistancesStringShort { get { return MMMonster.GetResistancesStringShort(Resistances); } }
        public string DamageTypeString { get { return GetDamageTypeString(DamageType); } }
        public string DamageTypeStringShort { get { return MMMonster.GetDamageTypeStringShort(DamageType); } }

        public static string GetSpecialAttackString(MM45SpecialAttack attack, bool bShowNone)
        {
            switch (attack)
            {
                case MM45SpecialAttack.None:
                case MM45SpecialAttack.None11: return bShowNone ? "None" : "";
                case MM45SpecialAttack.Poison: return "Poison";
                case MM45SpecialAttack.Disease: return "Disease";
                case MM45SpecialAttack.Insane: return "Insane";
                case MM45SpecialAttack.Sleep: return "Sleep";
                case MM45SpecialAttack.DrainSP: return "DrainSP";
                case MM45SpecialAttack.Curse: return "Curse";
                case MM45SpecialAttack.Paralyze: return "Paralyze";
                case MM45SpecialAttack.Knockout: return "Knockout";
                case MM45SpecialAttack.Confuse: return "Confuse";
                case MM45SpecialAttack.BreakWeapon: return "BreakWeapon";
                case MM45SpecialAttack.Weakness: return "Weakness";
                case MM45SpecialAttack.Eradicate: return "Eradicate";
                case MM45SpecialAttack.Age: return "Age";
                case MM45SpecialAttack.Kill: return "Kill";
                case MM45SpecialAttack.Stone: return "Stone";
                case MM45SpecialAttack.CurseItem: return "CurseItem";
                default: return "Unknown";
            }
        }

        public string SpecialAttackString
        {
            get { return GetSpecialAttackString(SpecialAttack, true); }
        }

        public override int AverageResistance { get { return MMMonster.GetAverageResistance(Resistances); } }
        public override int TreasureStrength { get { return MMMonster.GetTreasureStrength(Items, Gems, Gold); } }
        public override string TreasureStringShort { get { return MMMonster.GetTreasureStringShort(Items, Gems, Gold); } }

        public override double AverageDamage
        {
            get
            {
                return (NumAttacks * DamageNumDice * (Damage - 1)) / 2.0;
            }
        }

        public override string AllPowersString { get { return GetSpecialAttackString(SpecialAttack, false); } }
        public override string TargetString { get { return HatesString; } }

        public override string GetMultiLineDescription(bool bActive)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} ({1})\r\n", Name, GetMonsterTypeString(MonsterType));
            if (bActive)
            {
                sb.AppendFormat("HP: {0}/{1}\r\n", CurrentHP, HP);
                sb.AppendFormat("Condition: {0}\r\n", ConditionString(MM345Condition));
            }
            else
                sb.AppendFormat("MaxHP: {0}\r\n", HP);
            sb.AppendFormat("AC: {0}\r\n", AC);
            if (Resistances != null)
            {
                sb.AppendFormat("Physical Resistance: {0}\r\n", Resistances.Physical);
                sb.AppendFormat("Resist Fire:{0}, Cold:{1}, Electricity:{2}\r\n", Resistances.Fire, Resistances.Cold, Resistances.Electricity);
                sb.AppendFormat("Resist Poison:{0}, Energy:{1}, Magic:{2}\r\n", Resistances.Poison, Resistances.Energy, Resistances.Magic);
            }
            sb.AppendFormat("Damage: {0}{1}\r\n", DamageStringFull, Missile ? ", Ranged" : "");
            sb.AppendFormat("Speed: {0}{1}\r\n", Speed, Flying ? ", Flying" : "");
            sb.AppendFormat("Accuracy: {0}\r\n", Accuracy);
            sb.AppendFormat("Experience: {0}\r\n", Global.GetHRString(Experience));
            sb.AppendFormat("Touch: {0}\r\n", SpecialAttackString);
            if (HatesClass != MM45RaceClass.Any)
                sb.AppendFormat("Target: {0}\r\n", TargetString);
            sb.AppendFormat("Treasure: " + TreasureString);
            if (Global.Debug)
                sb.AppendFormat("\nEncounter Index: " + EncounterIndex);
            return sb.ToString();
        }

        public string TreasureString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Gold > 0)
                    sb.AppendFormat("{0} Gold, ", Gold);
                if (Gems > 0)
                    sb.AppendFormat("{0} Gems, ", Gems);
                if (Items[0] > 0 && Items[0] < 7)
                    sb.AppendFormat("Level {0} Item, ", Items);
                else if (Items[0] == 7)
                    sb.Append("[L7 item; does not drop], ");

                if (Global.Trim(sb).Length == 0)
                    return "None";

                return sb.ToString();
            }
        }

        public override string ToString()
        {
            if (String.IsNullOrWhiteSpace(Name))
                return base.ToString();
            return Name;
        }
    }

    public class MM4MonsterList
    {
        private bool m_bValid = false;
        private string m_strError = string.Empty;
        private List<MMMonster> m_monsters = new List<MMMonster>();
        public bool UsingInternalList = true;

        public List<MMMonster> Monsters
        {
            get { return m_monsters; }
        }

        public bool Valid
        {
            get { return m_bValid; }
        }

        public string LastError
        {
            get { return m_strError; }
        }

        public MM4MonsterList(byte[] bytes)
        {
            m_bValid = false;
            SetFromBytes(bytes);
            UsingInternalList = false;
        }

        public void SetFromBytes(byte[] bytes)
        {
            m_monsters = new List<MMMonster>(bytes.Length / 60);
            for(int iIndex = 0; iIndex < bytes.Length / 60; iIndex++)
            {
                MM45Monster monster = new MM45Monster(iIndex, bytes, iIndex * 60, MM45Side.Clouds);
                m_monsters.Add(monster);
            }
            m_bValid = true;
        }

        public bool Reinitialize(MM45MemoryHacker hacker, bool bOverrideSanityCheck)
        {
            if (InitExternalList(hacker, bOverrideSanityCheck))
            {
                UsingInternalList = false;
                return true;
            }

            InitInternalList();
            return false;
        }

        public bool InitExternalList(MM45MemoryHacker hacker, bool bOverrideSanityCheck)
        {
            if (!hacker.IsValid)
                return false;

            byte[] bytes = new byte[90 * 60];

            if (!hacker.ReadOffset(hacker.Memory.MM4MonsterData, bytes))
                return false;

            if (!bOverrideSanityCheck)
            {
                // The only reason we might want to skip this check is if the user edited some of the monster's values, which would
                // make these values incorrect
                byte[] byteCompare = new byte[] { 0x53, 0x6C, 0x69, 0x6D, 0x65, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                if (!Global.CompareBytes(bytes, byteCompare, 0, 0, byteCompare.Length))
                    return false;       // Probably trying to read the memory before it's ready
            }

            SetFromBytes(bytes);

            return m_bValid;
        }

        public MM4MonsterList()
        {
            InitInternalList();
        }

        public void InitInternalList()
        {
            // Load the standard MM4/5 monster list
            m_bValid = false;
            byte[] bytes = Global.Uncompress(Properties.Resources.MM4Monsters);
            SetFromBytes(bytes);
            UsingInternalList = true;
        }
    }

    public class MM5MonsterList
    {
        private bool m_bValid = false;
        private string m_strError = string.Empty;
        private List<MMMonster> m_monsters = new List<MMMonster>();
        public bool UsingInternalList = true;

        public List<MMMonster> Monsters
        {
            get { return m_monsters; }
        }

        public bool Valid
        {
            get { return m_bValid; }
        }

        public string LastError
        {
            get { return m_strError; }
        }

        public MM5MonsterList(byte[] bytes)
        {
            m_bValid = false;
            SetFromBytes(bytes);
            UsingInternalList = false;
        }

        public void SetFromBytes(byte[] bytes)
        {
            m_monsters = new List<MMMonster>(bytes.Length / 60);
            // For some reason MM5 monster number 0 is trash
            m_monsters.Add(MM45Monster.Dummy);
            for (int iIndex = 1; iIndex < bytes.Length / 60; iIndex++)
            {
                MM45Monster monster = new MM45Monster(iIndex, bytes, iIndex * 60, MM45Side.Darkside);
                m_monsters.Add(monster);
            }
            m_bValid = true;
        }

        public bool Reinitialize(MM45MemoryHacker hacker, bool bOverrideSanityCheck)
        {
            if (InitExternalList(hacker, bOverrideSanityCheck))
            {
                UsingInternalList = false;
                return true;
            }

            InitInternalList();
            return false;
        }

        public bool InitExternalList(MM45MemoryHacker hacker, bool bOverrideSanityCheck)
        {
            if (!hacker.IsValid)
                return false;

            byte[] bytes = new byte[95 * 60];

            if (!hacker.ReadOffset(hacker.Memory.MM5MonsterData, bytes))
                return false;

            if (!bOverrideSanityCheck)
            {
                // The only reason we might want to skip this check is if the user edited some of the monster's values, which would
                // make these values incorrect
                byte[] byteCompare = new byte[] { 0x57, 0x68, 0x69, 0x72, 0x6C, 0x77, 0x69, 0x6E, 0x64, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };
                if (!Global.CompareBytes(bytes, byteCompare, 60, 0, byteCompare.Length))
                    return false;       // Probably trying to read the memory before it's ready
            }

            SetFromBytes(bytes);

            return m_bValid;
        }

        public MM5MonsterList()
        {
            InitInternalList();
        }

        public void InitInternalList()
        {
            // Load the standard MM4/5 monster list
            m_bValid = false;
            byte[] bytes = Global.Uncompress(Properties.Resources.MM5Monsters);
            SetFromBytes(bytes);
            UsingInternalList = true;
        }
    }
}
