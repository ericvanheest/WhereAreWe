using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum MM3Touch
    {
        First = 0,
        None = 0,
        Poison = 1,
        Sleep = 2,
        Insane = 3,
        Disease = 4,
        Confuse = 5,
        Curse = 6,
        BreakArmor = 7,
        Weak = 8,
        DrainSP = 9,
        Paralyze = 10,
        Aging = 11,
        Unconscious = 12,
        Eradicate = 13,
        Stone = 14,
        Death = 15,
        CurseItem = 16,
        BreakWeapon = 17,
        Last
    }

    public enum MM3Target
    {
        First = 1,
        None0 = 0,
        None1 = 1,
        Cleric = 2,
        Sorcerer = 3,
        Druid = 4,
        Paladin = 5,
        Dwarf = 6,
        Last
    }

    public enum MM345MonsterCondition
    {
        Good = 0,
        Asleep = 7,
        Immobilized = 8,
        FeebleMind = 9,
        Hypnotized = 15,
        Silenced = 16
    }

    public class MM3MonsterList
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

        public bool Reinitialize(MM3MemoryHacker hacker, bool bOverrideSanityCheck)
        {
            if (InitExternalList(hacker, bOverrideSanityCheck))
                return true;

            InitInternalList();
            return false;
        }

        public bool InitExternalList(MM3MemoryHacker hacker, bool bOverrideSanityCheck)
        {
            if (!hacker.IsValid)
                return false;

            long pRead;

            byte[] bytesNames = new byte[924];

            hacker.ReadOffset(MM3Memory.MonsterNames, bytesNames, 924, out pRead);
            if (pRead != 924)
                return false;

            byte[] bytesStats = hacker.GetMonsterListBytes();

            if (!bOverrideSanityCheck)
            {
                // The only reason we might want to skip this check is if the user edited some of the monster's MaxHP values, which would
                // make these values incorrect
                byte[] byteCompare = new byte[] { 0x05, 0x00, 0x0F, 0x00, 0x0A, 0x00, 0x19, 0x00, 0x14, 0x00, 0x0A, 0x00, 0x28, 0x00, 0x28, 0x00 };
                if (!Global.CompareBytes(bytesStats, byteCompare, MM3Memory.MonFileHPMax, 0, byteCompare.Length))
                    return false;       // Probably trying to read the memory before it's ready
            }

            return InitUsingBytes(bytesNames, bytesStats);
        }

        public MM3MonsterList(byte[] bytesNames, byte[] stats)
        {
            InitUsingBytes(bytesNames, stats);
        }

        public bool InitUsingBytes(byte[] bytesNames, byte[] stats)
        {
            string[] names = Encoding.ASCII.GetString(bytesNames).Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
            // names[69] is supposed to be "Wizard" but seems to be missing from memory for some reason
            List<string> namesFix = new List<string>(names);
            namesFix.Insert(69, "Wizard");

            m_monsters = new List<MMMonster>(namesFix.Count);
            for (int iIndex = 0; iIndex < namesFix.Count; iIndex++)
            {
                MM3Monster monster = new MM3Monster();
                monster.Name = namesFix[iIndex];
                monster.Index = iIndex;
                monster.Resistances = new Resistances(
                    stats[iIndex + MM3Memory.MonFilePhysicalResist],
                    stats[iIndex + MM3Memory.MonFileFireResist],
                    stats[iIndex + MM3Memory.MonFileColdResist],
                    stats[iIndex + MM3Memory.MonFileElecResist],
                    stats[iIndex + MM3Memory.MonFileAcidResist],
                    stats[iIndex + MM3Memory.MonFileEnergyResist],
                    stats[iIndex + MM3Memory.MonFileMagicResist]);
                monster.Gems = BitConverter.ToUInt16(stats, iIndex * 2 + MM3Memory.MonFileGems);
                monster.Gold = BitConverter.ToInt32(stats, iIndex * 4 + MM3Memory.MonFileGold);
                monster.Items = new int[] { stats[iIndex + MM3Memory.MonFileItems] };
                monster.Accuracy = stats[iIndex + MM3Memory.MonFileAccuracy];
                monster.Missile = stats[iIndex + MM3Memory.MonFileRanged] == 1;
                monster.Touch = (MM3Touch)stats[iIndex + MM3Memory.MonFileSpecialPower];
                monster.Target = (MM3Target)stats[iIndex + MM3Memory.MonFileTarget];
                monster.DamageType = (DamageType)stats[iIndex + MM3Memory.MonFileDamageType];
                monster.Experience = BitConverter.ToInt32(stats, iIndex * 4 + MM3Memory.MonFileExperience);
                monster.NumAttacks = PartyAttack(iIndex) ? -1 : stats[iIndex + MM3Memory.MonFileNumAttacks];
                monster.Speed = stats[iIndex + MM3Memory.MonFileSpeed];
                monster.AC = stats[iIndex + MM3Memory.MonFileAC];
                monster.HP = BitConverter.ToUInt16(stats, iIndex * 2 + MM3Memory.MonFileHPMax);
                monster.DamageNumDice = stats[iIndex + MM3Memory.MonFileDamageNumDice];
                if (monster.DamageNumDice == 232)
                    monster.DamageNumDice = 1000;
                monster.DamageDieMax = stats[iIndex + MM3Memory.MonFileDamageDieMax];
                monster.DamageMin = monster.DamageNumDice;
                monster.Damage = monster.DamageNumDice * monster.DamageDieMax;
                monster.Undead = IsUndead(iIndex);
                m_monsters.Add(monster);
            }
            UsingInternalList = false;
            return true;
        }

        private static bool IsUndead(int iIndex)
        {
            // "undead" doesn't seem to be in memory in an obvious way
            switch (iIndex)
            {
                case 4:  // Skeleton
                case 9:  // Zombie
                case 27:  // Ghoul
                case 29:  // Phantom
                case 44:  // Ghost
                case 51:  // Reaper
                case 53:  // Lich
                case 61:  // Mummy
                case 71:  // Vampire
                case 82:  // Mummy King
                case 85:  // Vampire King
                    return true;
                default:
                    return false;
            }
        }

        private static bool PartyAttack(int iIndex)
        {
            // "party attack" doesn't seem to be in memory in an obvious way
            switch (iIndex)
            {
                case 5: // Screamer
                case 8: // Wild Fungus
                case 21: // Scorpia
                case 22: // Cryo Spore
                case 24: // Mini Dragon
                case 34: // Wicked Witch
                case 38: // Mystic Cloud
                case 40: // Cleric of Moo
                case 52: // Sorcerer
                case 53: // Lich
                case 62: // Priest of Moo
                case 64: // Dragon Worm
                case 67: // Green Dragon
                case 76: // Kudo Crab
                case 77: // Medusa
                case 80: // Dragon Lord
                case 86: // Moo Master
                    return true;
                default:
                    return false;
            }
        }

        public MM3MonsterList()
        {
            InitInternalList();
        }

        public void InitInternalList()
        {
            // Load the standard MM3 monster list
            InitUsingBytes(Global.Uncompress(Properties.Resources.MM3MonstersNames), Global.Uncompress(Properties.Resources.MM3Monsters));

            UsingInternalList = true;
        }
    }

    public class MM3Monster : MM345Monster
    {
        public MM3Touch Touch;
        public MM3Target Target;

        public MM3Monster()
        {
        }

        public MM3Monster(int index, string name, int hp, bool undead, int ac, int speed, int accuracy, int attacks, int damageMin, int damageMax,
            DamageType damageType, bool ranged, MM3Touch touch, MM3Target target, int resistPhysical, int resistFire, int resistCold,
            int resistElectricity, int resistPoison, int resistEnergy, int resistMagic, int exp, int gold, int gems, int[] items)
        {
            MM345Condition = MM345MonsterCondition.Good;
            Index = index;
            Name = name;
            HP = hp;
            CurrentHP = hp;
            Undead = undead;
            AC = ac;
            Speed = speed;
            Accuracy = accuracy;
            NumAttacks = attacks;
            Damage = damageMax;
            DamageMin = damageMin;
            DamageType = damageType;
            Missile = ranged;
            Touch = touch;
            Target = target;
            Resistances = new Resistances(resistPhysical, resistFire, resistCold, resistElectricity, resistPoison, resistEnergy, resistMagic);
            Experience = exp;
            Gold = gold;
            Gems = gems;
            Items = items;
        }

        public MM3Monster(int index, string name, int hp, bool undead, int ac, int speed, int accuracy, int attacks, int damageMin, int damageMax,
            int damageNumDice, int damageDieMax,
            DamageType damageType, bool ranged, MM3Touch touch, MM3Target target, int resistPhysical, int resistFire, int resistCold,
            int resistElectricity, int resistPoison, int resistEnergy, int resistMagic, long exp, int gold, int gems, int[] items, MM345MonsterCondition condition)
        {
            MM345Condition = condition;
            Index = index;
            Name = name;
            HP = hp;
            CurrentHP = hp;
            Undead = undead;
            AC = ac;
            Speed = speed;
            Accuracy = accuracy;
            NumAttacks = attacks;
            Damage = damageMax;
            DamageMin = damageMin;
            DamageType = damageType;
            DamageNumDice = damageNumDice;
            DamageDieMax = damageDieMax;
            Missile = ranged;
            Touch = touch;
            Target = target;
            Resistances = new Resistances(resistPhysical, resistFire, resistCold, resistElectricity, resistPoison, resistEnergy, resistMagic);
            Experience = exp;
            Gold = gold;
            Gems = gems;
            Items = items;
        }

        public override string DamageString
        {
            get
            {
                string sType = DamageType == DamageType.Physical ? "" : String.Format(", {0}", DamageTypeStringShort);
                StringBuilder sb = new StringBuilder();
                if (DamageMin == Damage)
                    sb.AppendFormat("{0}", Damage);
                else if (DamageNumDice > 0)
                    sb.AppendFormat("{0}d{1}", DamageNumDice, DamageDieMax);
                else
                    sb.AppendFormat("{0}-{1}", DamageMin, Damage);
                sb.Append(sType);
                if (NumAttacks == 1)
                    return sb.ToString();
                return String.Format("{0} {1}", NumAttacks == -1 ? "Ax" : (NumAttacks.ToString() + "x"), sb.ToString());
            }
        }

        public string DamageStringFull
        {
            get
            {
                string sType = String.Format(" ({0})", DamageTypeString);
                StringBuilder sb = new StringBuilder();
                if (DamageMin == Damage)
                    sb.AppendFormat("{0}", Damage);
                else if (DamageNumDice > 0)
                    sb.AppendFormat("{0}d{1}", DamageNumDice, DamageDieMax);
                else
                    sb.AppendFormat("{0}-{1}", DamageMin, Damage);
                sb.Append(sType);
                if (NumAttacks == 1)
                    return sb.ToString();
                return String.Format("{0} {1}", NumAttacks == -1 ? "All x" : (NumAttacks.ToString() + "x"), sb.ToString());
            }
        }

        public override string OneLineDescription
        {
            get
            {
                return String.Format("{0}{1}{2}, {3}/{4}, AC{5}, {6}, Speed:{7}, Accuracy:{8}, Exp:{9}{10}{11}",
                    Name,   
                    Undead ? "(u)" : "",
                    MM345Condition == MM345MonsterCondition.Good ? "" : String.Format(" ({0})", ConditionString(MM345Condition)),
                    CurrentHP,
                    HP,
                    AC,
                    DamageString,
                    Speed,
                    Accuracy,
                    Experience,
                    Touch == MM3Touch.None ? "" : ", ",
                    TouchString
                    );
            }
        }

        public override Monster Clone()
        {
            return new MM3Monster(
                Index,
                Name,
                HP,
                Undead,
                AC,
                Speed,
                Accuracy,
                NumAttacks,
                DamageMin,
                Damage,
                DamageNumDice,
                DamageDieMax,
                DamageType,
                Missile,
                Touch,
                Target,
                Resistances.Physical,
                Resistances.Fire,
                Resistances.Cold,
                Resistances.Electricity,
                Resistances.Poison,
                Resistances.Energy,
                Resistances.Magic,
                Experience,
                Gold,
                Gems,
                Items,
                MM345Condition
                );
        }

        public override string ResistancesStringShort { get { return MMMonster.GetResistancesStringShort(Resistances); } }
        public string DamageTypeString { get { return GetDamageTypeString(DamageType); } }
        public string DamageTypeStringShort { get { return MMMonster.GetDamageTypeStringShort(DamageType); } }

        public static string GetTouchString(MM3Touch touch)
        {
            switch (touch)
            {
                case MM3Touch.Aging: return "Aging";
                case MM3Touch.BreakArmor: return "Break Armor";
                case MM3Touch.BreakWeapon: return "Break Weapon";
                case MM3Touch.Confuse: return "Confuse";
                case MM3Touch.CurseItem: return "Curse Item";
                case MM3Touch.Curse: return "Curse";
                case MM3Touch.Death: return "Death";
                case MM3Touch.Disease: return "Disease";
                case MM3Touch.Eradicate: return "Eradicate";
                case MM3Touch.Insane: return "Insane";
                case MM3Touch.Paralyze: return "Paralyze";
                case MM3Touch.Poison: return "Poison";
                case MM3Touch.Sleep: return "Sleep";
                case MM3Touch.DrainSP: return "Drain SP";
                case MM3Touch.Stone: return "Stone";
                case MM3Touch.Unconscious: return "Unconscious";
                case MM3Touch.Weak: return "Weak";
                default: return "";
            }
        }

        public static string GetTargetString(MM3Target target)
        {
            switch (target)
            {
                case MM3Target.Paladin: return "Paladin";
                case MM3Target.Cleric: return "Cleric";
                case MM3Target.Sorcerer: return "Sorcerer";
                case MM3Target.Druid: return "Druid";
                case MM3Target.Dwarf: return "Dwarf";
                default: return "";
            }
        }

        public string TouchString { get { return GetTouchString(Touch); } }

        public override int AverageResistance { get { return MMMonster.GetAverageResistance(Resistances); } }
        public override int TreasureStrength { get { return MMMonster.GetTreasureStrength(Items, Gems, Gold); } }
        public override string TreasureStringShort { get { return MMMonster.GetTreasureStringShort(Items, Gems, Gold); } }

        public override double AverageDamage
        {
            get
            {
                double average = (Damage - DamageMin) / 2.0 + DamageMin;
                if (NumAttacks == -1)
                    return average;
                return NumAttacks * average;
            }
        }

        public override string AllPowersString { get { return (Touch == MM3Touch.None ? String.Empty : TouchString); } }
        public override string TargetString { get { return GetTargetString(Target); } }

        public override string GetMultiLineDescription(bool bActive)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Name + (Undead ? " (Undead)" : ""));
            if (bActive)
            {
                sb.AppendFormat("HP: {0}/{1}\r\n", CurrentHP, HP);
                sb.AppendFormat("Condition: {0}\r\n", ConditionString(MM345Condition));
            }
            else
                sb.AppendFormat("MaxHP: {0}\r\n", HP);
            sb.AppendFormat("AC: {0}\r\n", AC);
            sb.AppendFormat("Physical Resistance: {0}\r\n", Resistances.Physical);
            sb.AppendFormat("Resist Fire:{0}, Cold:{1}, Electricity:{2}\r\n", Resistances.Fire, Resistances.Cold, Resistances.Electricity);
            sb.AppendFormat("Resist Poison:{0}, Energy:{1}, Magic:{2}\r\n", Resistances.Poison, Resistances.Energy, Resistances.Magic);
            sb.AppendFormat("Damage: {0}{1}\r\n", DamageStringFull, Missile ? ", Ranged" : "");
            sb.AppendFormat("Speed: {0}\r\n", Speed);
            sb.AppendFormat("Accuracy: {0}\r\n", Accuracy);
            sb.AppendFormat("Experience: {0}\r\n", Global.GetHRString(Experience));
            sb.AppendFormat("Touch: {0}\r\n", TouchString);
            sb.AppendFormat("Target: {0}\r\n", TargetString);
            sb.AppendFormat("Gold: {0}\r\n", Gold);
            sb.AppendFormat("Gems: {0}\r\n", Gems);
            sb.AppendFormat("Item: {0}\r\n", Items[0] > 0 ? ("Level " + Items[0].ToString()) : "None");
            return sb.ToString();
        }

    }
}
