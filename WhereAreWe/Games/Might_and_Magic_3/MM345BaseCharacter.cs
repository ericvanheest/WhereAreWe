using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WhereAreWe
{
    public abstract class MM345BaseCharacter : MMBaseCharacter
    {
        public string CharName;
        public byte NameTerminator;
        public MM345Sex Sex;
        public MM345AlignmentValue Alignment;
        public MM345Class Class;
        public OneByteStatModifier Might;
        public OneByteStatModifier Intellect;
        public OneByteStatModifier Personality;
        public OneByteStatModifier Endurance;
        public OneByteStatModifier Speed;
        public OneByteStatModifier Accuracy;
        public OneByteStatModifier Luck;
        public byte ACModifier;
        public OneByteStatModifier Level;
        public byte BirthDay;
        public byte AgeModifier;
        public MM345KnownSpells Spells;
        public Inventory Inventory;
        public MMCondition Condition;
        public MMProtections Protection;
        public MMBeacon Beacon;
        public MM345SecondarySkills Skills;
        public byte ReadySpell;
        public OneByteStatModifier FireResist;
        public OneByteStatModifier ColdResist;
        public OneByteStatModifier ElecResist;
        public OneByteStatModifier PoisonResist;
        public OneByteStatModifier EnergyResist;
        public OneByteStatModifier MagicResist;
        public byte Town;
        public byte SaveSide;
        public Int16 CurrentHP;
        public UInt16 CurrentSP;
        public UInt16 BirthYear;
        public UInt32 Experience;

        public int Address = -1;

        public override int BasicSP { get { return CurrentSP; } }
        public override int BasicMaxSP { get { return MaxSP; } }
        public override int BasicHP { get { return CurrentHP; } }
        public override int BasicMaxHP { get { return MaxHP; } }
        public override int BasicThievery { get { return Thievery; } }

        public static int ConditionOffset(BasicConditionFlags cond)
        {
            switch (cond)
            {
                case BasicConditionFlags.Cursed: return MM3.Offsets.Condition + 0;
                case BasicConditionFlags.HeartBroken: return MM3.Offsets.Condition + 1;
                case BasicConditionFlags.Weak: return MM3.Offsets.Condition + 2;
                case BasicConditionFlags.Poisoned: return MM3.Offsets.Condition + 3;
                case BasicConditionFlags.Diseased: return MM3.Offsets.Condition + 4;
                case BasicConditionFlags.Insane: return MM3.Offsets.Condition + 5;
                case BasicConditionFlags.InLove: return MM3.Offsets.Condition + 6;
                case BasicConditionFlags.Drunk: return MM3.Offsets.Condition + 7;
                case BasicConditionFlags.Asleep: return MM3.Offsets.Condition + 8;
                case BasicConditionFlags.Depressed: return MM3.Offsets.Condition + 9;
                case BasicConditionFlags.Confused: return MM3.Offsets.Condition + 10;
                case BasicConditionFlags.Paralyzed: return MM3.Offsets.Condition + 11;
                case BasicConditionFlags.Unconscious: return MM3.Offsets.Condition + 12;
                case BasicConditionFlags.Dead: return MM3.Offsets.Condition + 13;
                case BasicConditionFlags.Stone: return MM3.Offsets.Condition + 14;
                case BasicConditionFlags.Eradicated: return MM3.Offsets.Condition + 15;
                default: return 0;
            }
        }

        public override int BasicAddress { get { return Address; } }
        public override int NumKnownSpells { get { return (Spells == null ? 0 : Spells.NumKnown); } }

        public Modifiers GetModifier(GenericAge age, int iCurrentYear)
        {
            int iYears = iCurrentYear - age.Years + age.Modifier;
            Modifiers mod = new Modifiers();

            int iMod = 0;
            if (iYears < 36)
                iMod = 0;
            else if (iYears < 51)
                iMod = 2;
            else if (iYears < 76)
                iMod = 5;
            else if (iYears < 101)
                iMod = 10;
            else if (iYears < 201)
                iMod = 20;
            else
                iMod = 50;

            string strAge = String.Format("Character age ({0})", iYears);

            mod.Adjust(ModAttr.Might, -iMod, strAge);
            mod.Adjust(ModAttr.Endurance, -iMod, strAge);
            mod.Adjust(ModAttr.Speed, -iMod, strAge);
            mod.Adjust(ModAttr.Accuracy, -iMod, strAge);
            mod.Adjust(ModAttr.Intellect, iMod, strAge);
            mod.Adjust(ModAttr.Personality, iMod, strAge);

            return mod;
        }

        public override string GetACString(int iBless = 0)
        {
            return String.Format("{0}{1}{2}",
                Math.Max(0, BasicAC.Permanent), 
                ACModifier != 0 ? Global.AddPlus(ACModifier) : "",
                Protection.Blessed != 0 ? Global.AddPlus(Protection.Blessed) : "");
        }

        public static StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            return StatModifier.FromTable(value, stat,
                  3, -5,   5, -4,   7, -3,   9, -2,  11, -1,  13,  0,
                 15,  1,  17,  2,  19,  3,  21,  4,  25,  5,  30,  6,  35,  7,  40,  8,  50,  9,  75,  10,
                100, 11, 125, 12, 150, 13, 175, 14, 200, 15, 225, 16, 250, 17,  20);
        }

        public static string GetStatModifierName(int value)
        {
            if (value < 3) return "Nonexistant";      
            if (value < 5) return "Very Poor";        
            if (value < 7) return "Poor";             
            if (value < 9) return "Very Low";         
			if (value < 11) return "Low";              
			if (value < 13) return "Average";          
			if (value < 15) return "Good";             
			if (value < 17) return "Very Good";        
			if (value < 19) return "High";             
			if (value < 21) return "Very High";        
			if (value < 25) return "Great";            
			if (value < 30) return "Super";            
			if (value < 35) return "Amazing";          
			if (value < 40) return "Incredible";       
			if (value < 50) return "Gigantic";         
			if (value < 75) return "Fantastic";        
            if (value < 100) return "Astounding";       
            if (value < 125) return "Astonishing";      
            if (value < 150) return "Monumental";       
            if (value < 175) return "Tremendous";       
            if (value < 200) return "Collosal";         // sic
            if (value < 225) return "Awesome";          
            if (value < 250) return "Awe Inspiring";
            return "Ultimate";
        }

        public override string Name { get { return CharName; } }

        public static MM345Sex SexFromByte(byte b)
        {
            if (b >= (byte)MM345Sex.Male && b <= (byte)MM345Sex.Female)
                return (MM345Sex)b;
            return MM345Sex.None;
        }

        public static MM45Race MM45RaceFromByte(byte b)
        {
            if (b >= (byte)MM45Race.Human && b <= (byte)MM45Race.HalfOrc)
                return (MM45Race)b;
            return MM45Race.None;
        }

        public static MM3Race MM3RaceFromByte(byte b)
        {
            if (b >= (byte)MM3Race.Human && b <= (byte)MM3Race.HalfOrc)
                return (MM3Race)b;
            return MM3Race.None;
        }

        public static MM345Class ClassFromByte(byte b)
        {
            if (b >= (byte)MM345Class.Knight && b <= (byte)MM345Class.Ranger)
                return (MM345Class)b;
            return MM345Class.None;
        }

        public static string SexString(MM345Sex sex)
        {
            switch (sex)
            {
                case MM345Sex.Male: return "Male";
                case MM345Sex.Female: return "Female";
                default: return "None";
            }
        }

        public static string AlignmentString(MM345AlignmentValue align)
        {
            switch (align)
            {
                case MM345AlignmentValue.Good: return "Good";
                case MM345AlignmentValue.Neutral: return "Neutral";
                case MM345AlignmentValue.Evil: return "Evil";
                default: return "None";
            }
        }

        public static string RaceString(MM45Race race)
        {
            switch (race)
            {
                case MM45Race.Dwarf: return "Dwarf";
                case MM45Race.Elf: return "Elf";
                case MM45Race.Gnome: return "Gnome";
                case MM45Race.HalfOrc: return "Half-Orc";
                case MM45Race.Human: return "Human";
                default: return "None";
            }
        }
        public static string ClassString(MM345Class classenum)
        {
            switch (classenum)
            {
                case MM345Class.Archer: return "Archer";
                case MM345Class.Cleric: return "Cleric";
                case MM345Class.Knight: return "Knight";
                case MM345Class.Paladin: return "Paladin";
                case MM345Class.Robber: return "Robber";
                case MM345Class.Sorcerer: return "Sorcerer";
                case MM345Class.Ninja: return "Ninja";
                case MM345Class.Barbarian: return "Barbarian";
                case MM345Class.Druid: return "Druid";
                case MM345Class.Ranger: return "Ranger";
                default: return "None";
            }
        }

        public static string ConditionString(Inventory inventory, MMCondition cond, bool bIncludeGood)
        {
            bool bCursedItem = inventory.CursedEquipped;
            bool bBrokenItem = inventory.BrokenEquipped;

            if (cond.Good && !bCursedItem && !bBrokenItem)
                return bIncludeGood ? "Good" : "";

            StringBuilder sb = new StringBuilder();
            if (bCursedItem)
                sb.Append("Cursed Item, ");
            if (bBrokenItem)
                sb.Append("Broken Item, ");
            Global.Trim(sb);
            string strCond = cond.ToString();
            if (strCond.Length > 0 && sb.Length > 0)
                sb.Append(", ");
            sb.Append(strCond);
            return sb.ToString();
        }

        public int NumAttacks
        {
            get
            {
                switch (Class)
                {
                    case MM345Class.Ninja:
                    case MM345Class.Knight: return Level.Temporary / 5 + 1;
                    case MM345Class.Paladin:
                    case MM345Class.Archer:
                    case MM345Class.Robber:
                    case MM345Class.Ranger: return Level.Temporary / 6 + 1;
                    case MM345Class.Barbarian: return Level.Temporary / 4 + 1;
                    case MM345Class.Druid:
                    case MM345Class.Cleric: return Level.Temporary / 7 + 1;
                    case MM345Class.Sorcerer: return Level.Temporary / 8 + 1;
                    default:
                        return 1;
                }
            }
        }

        public override BasicDamage BasicMeleeDamage { get { return new BasicDamage(NumAttacks, Inventory.MeleeWeaponDamage.Dice, Modifiers.MeleeDamage); } }
        public override BasicDamage BasicRangedDamage { get { return new BasicDamage(1, Inventory.RangedWeaponDamage.Dice, Modifiers.RangedDamage); } }

        public override string MeleeDamageString { get { return BasicMeleeDamage.ToString(); } }
        public override string RangedDamageString { get { return BasicRangedDamage.ToString(); } }

        public override long QuickRefExperience { get { return Experience; } }
        public override MMHitPoints QuickRefHitPoints { get { return new MMHitPoints(CurrentHP, MaxHP, MaxHP); } }
        public override SpellPoints QuickRefSpellPoints { get { return new MMSpellPoints(CurrentSP, MaxSP); } }
        public override OneByteStat QuickRefSpeed { get { return new OneByteStat(Speed.Temporary + Modifiers.Speed, Speed.Permanent); } }
        public override OneByteStat QuickRefSpellLevel { get { return new OneByteStat(99, 99); } }
        public override int QuickRefGems { get { return 0; } }
        public override string QuickRefCondition { get { return MM3Character.ConditionString(Inventory, Condition, false); } }
        public override bool IsHealer { get { return (Class == MM345Class.Paladin || Class == MM345Class.Cleric || Class == MM345Class.Druid || Class == MM345Class.Ranger); } }
        public override bool PartyGems { get { return true; } }
        public override bool HasSpellLevel { get { return false; } }

        public static bool IsSpellcaster(MM345Class mm3Class)
        {
            switch (mm3Class)
            {
                case MM345Class.Archer:
                case MM345Class.Cleric:
                case MM345Class.Druid:
                case MM345Class.Paladin:
                case MM345Class.Ranger:
                case MM345Class.Sorcerer:
                    return true;
                default:
                    return false;
            }
        }

        public override string ResistancesString
        {
            get
            {
                return String.Format("Magic: {0}, Fire: {1}, Energy: {2}, Cold: {3}, Poison: {4}, Electric: {5}",
                    MagicResist.AdjustedString(Modifiers.Magic),
                    FireResist.AdjustedString(Modifiers.Fire),
                    EnergyResist.AdjustedString(Modifiers.Energy),
                    ColdResist.AdjustedString(Modifiers.Cold),
                    PoisonResist.AdjustedString(Modifiers.Poison),
                    ElecResist.AdjustedString(Modifiers.Electricity)
                    );
            }
        }

        public override string AttributesString
        {
            get
            {
                return String.Format("Mgt:{0}, Int:{1}, Per:{2}, End:{3}, Spd:{4}, Acy:{5}, Luc:{6}",
                    Might.ToString(),
                    Intellect.ToString(),
                    Personality.ToString(),
                    Endurance.ToString(),
                    Speed.ToString(),
                    Accuracy.ToString(),
                    Luck.ToString());
            }
        }

        public override string ExperienceString
        {
            get
            {
                if (Level.Permanent >= MaxLevel)
                    return String.Format("{0} (Max Level)", Experience);
                if (!ReadyToTrain)
                    return String.Format("{0}/{1}", Experience, XPForNextLevel);
                return String.Format("{0} ({1})", Experience, Global.TrainString(Level.Permanent, TrainableLevel));
            }
        }

        public override long BasicExperience { get { return Experience; } }

        public override long XPForLevel(GenericClass mmClass, int iLevel)
        {
            return XPForLevel(ClassForGeneric(mmClass), iLevel);
        }

        public override bool ReadyToTrain
        {
            get
            {
                return NeedsXP < 1;
            }
        }

        public static long XPForLevel(MM345Class mm3Class, int iLevel)
        {
            long iBase = 1500;
            switch (mm3Class)
            {
                case MM345Class.Archer:
                case MM345Class.Paladin:
                case MM345Class.Sorcerer:
                case MM345Class.Ranger:
                    iBase = 2000;
                    break;
                case MM345Class.Robber:
                    iBase = 1000;
                    break;
                default:
                    break;
            }

            long iRequired = 0;

            for (int i = 2; i < 256; i++)
            {
                if (i > iLevel)
                    break;

                switch (i)
                {
                    case 1:
                    case 2:
                    case 3:
                        break;
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        iBase *= 2;
                        break;
                    default:
                        iBase = 1024000;
                        break;
                }
                iRequired += iBase;
            }
            return iRequired;
        }

        public override long XPForNextLevel { get { return XPForLevel(Class, Level.Permanent+1); } }

        public override long XPForCurrentLevel { get { return XPForLevel(Class, Level.Permanent); } }

        public override long NeedsXP
        {
            get
            {
                return XPForNextLevel - Experience;
            }
        }

        public static int BaseHPForClass(MM345Class mm3Class)
        {
            switch (mm3Class)
            {
                case MM345Class.Sorcerer: return 4;
                case MM345Class.Cleric: return 5;
                case MM345Class.Robber: return 8;
                case MM345Class.Ninja: return 7;
                case MM345Class.Archer: return 7;
                case MM345Class.Paladin: return 8;
                case MM345Class.Knight: return 10;
                case MM345Class.Barbarian: return 12;
                case MM345Class.Druid: return 6;
                case MM345Class.Ranger: return 9;
                default: return 0;
            }
        }

        public override string BasicInfoString
        {
            get
            {
                if (Level == null)
                    return "<Invalid Character Record>";
                string strMagicalAging = AgeModifier > 0 ? String.Format(" (+{0} years)", AgeModifier) : "";
                return String.Format("Level {0} {1} {2} {3} {4}, Born {5}/{6}{7}",
                 Level.ToString(),
                 MM3Character.SexString(Sex),
                 Alignment.ToString(),
                 Race.RaceString(BasicRace),
                 MM3Character.ClassString(Class),
                 BirthDay,
                 BirthYear,
                 strMagicalAging);
            }
        }

        public override StatAndModifier BasicLevel { get { return new StatAndModifier(Level); } }
        public override StatAndModifier BasicAC 
        {
            get { return new StatAndModifier(MM3Character.GetStatModifier(Speed.Temporary + Modifiers.Speed, PrimaryStat.Speed).Value + Modifiers.ArmorClass, ACModifier); } 
        }

        public override MMSex BasicSex
        {
            get
            {
                switch (Sex)
                {
                    case MM345Sex.Male: return MMSex.Male;
                    case MM345Sex.Female: return MMSex.Female;
                    default: return MMSex.None;
                }
            }
        }

        public override StatAndModifier BasicIntellect { get { return new StatAndModifier(Intellect); } }
        public override StatAndModifier BasicMight { get { return new StatAndModifier(Might); } }
        public override StatAndModifier BasicPersonality { get { return new StatAndModifier(Personality); } }
        public override StatAndModifier BasicEndurance { get { return new StatAndModifier(Endurance); } }
        public override StatAndModifier BasicSpeed { get { return new StatAndModifier(Speed); } }
        public override StatAndModifier BasicAccuracy { get { return new StatAndModifier(Accuracy); } }
        public override StatAndModifier BasicLuck { get { return new StatAndModifier(Luck); } }

        public override GenericClass BasicClass
        {
            get
            {
                switch (Class)
                {
                    case MM345Class.Knight: return GenericClass.Knight;
                    case MM345Class.Paladin: return GenericClass.Paladin;
                    case MM345Class.Archer: return GenericClass.Archer;
                    case MM345Class.Cleric: return GenericClass.Cleric;
                    case MM345Class.Sorcerer: return GenericClass.Sorcerer;
                    case MM345Class.Robber: return GenericClass.Robber;
                    case MM345Class.Ninja: return GenericClass.Ninja;
                    case MM345Class.Barbarian: return GenericClass.Barbarian;
                    case MM345Class.Druid: return GenericClass.Druid;
                    case MM345Class.Ranger: return GenericClass.Ranger;
                    default: return GenericClass.None;
                }
            }
        }

        public override GenericAge BasicAge { get { return new GenericAge(BirthYear, BirthDay, AgeModifier); } }

        public override GenericAlignment BasicAlignment
        {
            get
            {
                return new GenericAlignment(BasicAlignmentValue(true), BasicAlignmentValue(false));
            }
        }

        public GenericAlignmentValue BasicAlignmentValue(bool bTemporary)
        {
            switch (Alignment)
            {
                case MM345AlignmentValue.Good: return GenericAlignmentValue.Good;
                case MM345AlignmentValue.Neutral: return GenericAlignmentValue.Neutral;
                case MM345AlignmentValue.Evil: return GenericAlignmentValue.Evil;
                default: return GenericAlignmentValue.None;
            }
        }

        public override BasicConditionFlags BasicCondition
        {
            get
            {
                BasicConditionFlags cond = BasicConditionFlags.Good;

                bool bBrokenItemEquipped = Inventory.BrokenEquipped;
                bool bCursedItemEquipped = Inventory.CursedEquipped;

                if (Condition.Good && !bBrokenItemEquipped && !bCursedItemEquipped)
                    return cond;

                if (bBrokenItemEquipped)
                    cond |= BasicConditionFlags.BrokenItem;
                if (bCursedItemEquipped)
                    cond |= BasicConditionFlags.CursedItem;
                if (Condition.Cursed > 0)
                    cond |= BasicConditionFlags.Cursed;
                if (Condition.HeartBroken > 0)
                    cond |= BasicConditionFlags.HeartBroken;
                if (Condition.Weak > 0)
                    cond |= BasicConditionFlags.Weak;
                if (Condition.Poisoned > 0)
                    cond |= BasicConditionFlags.Poisoned;
                if (Condition.Diseased > 0)
                    cond |= BasicConditionFlags.Diseased;
                if (Condition.Insane > 0)
                    cond |= BasicConditionFlags.Insane;
                if (Condition.InLove > 0)
                    cond |= BasicConditionFlags.InLove;
                if (Condition.Drunk > 0)
                    cond |= BasicConditionFlags.Drunk;
                if (Condition.Asleep > 0)
                    cond |= BasicConditionFlags.Asleep;
                if (Condition.Depressed > 0)
                    cond |= BasicConditionFlags.Depressed;
                if (Condition.Confused > 0)
                    cond |= BasicConditionFlags.Confused;
                if (Condition.Paralyzed > 0)
                    cond |= BasicConditionFlags.Paralyzed;
                if (Condition.Unconscious > 0)
                    cond |= BasicConditionFlags.Unconscious;
                if (Condition.Dead > 0)
                    cond |= BasicConditionFlags.Dead;
                if (Condition.Stone > 0)
                    cond |= BasicConditionFlags.Stone;
                if (Condition.Eradicated > 0)
                    cond |= BasicConditionFlags.Eradicated;

                return cond;
            }
        }

        public override byte SexValue(MMSex sex)
        {
            switch (sex)
            {
                case MMSex.Male: return (byte)MM345Sex.Male;
                case MMSex.Female: return (byte)MM345Sex.Female;
                default: return (byte)MM345Sex.None;
            }
        }

        public override byte AlignmentValue(GenericAlignmentValue align)
        {
            switch (align)
            {
                case GenericAlignmentValue.Evil: return (byte)MM345AlignmentValue.Evil;
                case GenericAlignmentValue.Neutral: return (byte)MM345AlignmentValue.Neutral;
                case GenericAlignmentValue.Good: return (byte)MM345AlignmentValue.Good;
                default: return (byte)MM345AlignmentValue.None;
            }
        }

        public override byte RaceValue(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return (byte)MM3Race.Human;
                case GenericRace.Elf: return (byte)MM3Race.Elf;
                case GenericRace.Dwarf: return (byte)MM3Race.Dwarf;
                case GenericRace.Gnome: return (byte)MM3Race.Gnome;
                case GenericRace.HalfOrc: return (byte)MM3Race.HalfOrc;
                default: return (byte)MM3Race.None;
            }
        }

        public static MM345Class ClassForGeneric(GenericClass mmClass)
        {
            switch (mmClass)
            {
                case GenericClass.Archer: return MM345Class.Archer;
                case GenericClass.Cleric: return MM345Class.Cleric;
                case GenericClass.Knight: return MM345Class.Knight;
                case GenericClass.Paladin: return MM345Class.Paladin;
                case GenericClass.Robber: return MM345Class.Robber;
                case GenericClass.Sorcerer: return MM345Class.Sorcerer;
                case GenericClass.Ninja: return MM345Class.Ninja;
                case GenericClass.Barbarian: return MM345Class.Barbarian;
                case GenericClass.Druid: return MM345Class.Druid;
                case GenericClass.Ranger: return MM345Class.Ranger;
                default: return MM345Class.None;
            }
        }

        public override byte ClassValue(GenericClass classVal)
        {
            return (byte)ClassForGeneric(classVal);
        }

        public UInt32 HirelingCost
        {
            get
            {
                UInt32 result = (UInt32) (Level.Permanent * Level.Permanent * 20);
                if (Level.Permanent < 182)
                    return result;
                return 4293656576 + result;
            }
        }

        public static int GetRaceModHP(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return 0;
                case GenericRace.Elf: return -2;
                case GenericRace.Gnome: return -1;
                case GenericRace.Dwarf: return 1;
                case GenericRace.HalfOrc: return 2;
                default: return 0;
            }
        }

        public static int GetRaceModSP(GenericRace race, MM345Class cls)
        {
            switch (race)
            {
                case GenericRace.Human: return 0;
                case GenericRace.Elf: return (cls == MM345Class.Sorcerer || cls == MM345Class.Archer ? 2 : cls == MM345Class.Ranger ? 1 : 0);
                case GenericRace.Gnome: return +1;
                case GenericRace.Dwarf: return -1;
                case GenericRace.HalfOrc: return -2;
                default: return 0;
            }
        }

        private int HPPerLevel(MM345Class mm3Class)
        {
            switch (Class)
            {
                case MM345Class.Sorcerer: return 4;
                case MM345Class.Cleric: return 5;
                case MM345Class.Druid: return 6;
                case MM345Class.Archer:
                case MM345Class.Ninja: return 7;
                case MM345Class.Robber:
                case MM345Class.Paladin: return 8;
                case MM345Class.Ranger: return 9;
                case MM345Class.Knight: return 10;
                case MM345Class.Barbarian: return 12;
                default: return 10;
            }
        }

        private UInt16 MaxHPForClass(MM345Class mm3Class, int iMod)
        {
            return (UInt16)(Level.Temporary * Math.Max(HPPerLevel(mm3Class) + iMod, 1));
        }

        public int MaxHP { get { return MaxHPWithoutItems + Modifiers.HitPoints; } }

        public int MaxHPWithoutItems
        {
            get
            {
                if (Endurance == null)
                    return 0;

                int iMod = GetStatModifier(Endurance.Temporary + Modifiers.Endurance, PrimaryStat.Endurance).Value;
                iMod += GetRaceModHP(BasicRace);

                if (Skills.BodyBuilder > 0)
                    iMod += 1;  // per level
                return MaxHPForClass(Class, iMod);
            }
        }

        public override string GetMaxHPFormula()
        {
            int iRaceMod = GetRaceModHP(BasicRace);
            int iEndMod = GetStatModifier(Endurance.Temporary + Modifiers.Endurance, PrimaryStat.Endurance).Value;
            return String.Format("Level * (HP/lev for {0}/{1} + Endurance + BodyBuilder)\r\n" +
                "{2} * ({3} + {4} + {5} + {6}) = {7}",
                ClassString(Class), Race.RaceString(BasicRace), Level.Temporary, HPPerLevel(Class), GetRaceModHP(BasicRace),
                iEndMod, Skills.BodyBuilder > 0 ? "1" : "0", MaxHPWithoutItems);
        }

        public virtual int GetRaceModThievery(GenericRace race)
        {
            switch(race)
            {
                case GenericRace.Human: return 0;
                case GenericRace.Elf: return 10;
                case GenericRace.Gnome: return 5;
                case GenericRace.Dwarf: return 10;
                case GenericRace.HalfOrc: return -10;
                default: return 0;
            }
        }

        public static int GetClassModThievery(GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Robber: return 30;
                case GenericClass.Ninja: return 15;
                default: return 0;
            }
        }

        public override string GetThieveryFormula()
        {
            StringBuilder sb = new StringBuilder();

            if (Thievery > 0)
            {
                sb.AppendFormat("(Level * 2) = {0}", 2 * Level.Temporary);
                int iRace = GetRaceModThievery(BasicRace);
                if (iRace != 0)
                    sb.AppendFormat("\r\n{0}\t{1}", Global.AddPlus(iRace), Race.RaceString(BasicRace));
                int iClass = GetClassModThievery(BasicClass);
                if (iClass != 0)
                    sb.AppendFormat("\r\n{0}\t{1}", Global.AddPlus(iClass), BaseCharacter.ClassString(BasicClass));
                return sb.ToString();
            }

            return String.Empty;
        }

        public UInt16 Thievery
        {
            get
            {
                if (Skills.Thievery == 0)
                    return 0;
                int iBase = 0;
                iBase += GetRaceModThievery(BasicRace);
                iBase += GetClassModThievery(BasicClass);
                return (UInt16)(Level.Temporary * 2 + iBase);
            }
        }

        private double GetSPMod(MM345Class mmClass)
        {
            if (Intellect == null || Personality == null)
                return 0;

            int iModInt = GetStatModifier(Intellect.Temporary + Modifiers.Intellect, PrimaryStat.Intellect).Value;
            int iModPer = GetStatModifier(Personality.Temporary + Modifiers.Personality, PrimaryStat.Personality).Value;
            double iModBoth = (iModInt + iModPer) / 2.0;
            switch (Class)
            {
                case MM345Class.Knight:
                case MM345Class.Robber:
                case MM345Class.Ninja:
                case MM345Class.Barbarian: return 0;
                case MM345Class.Paladin: return iModPer;
                case MM345Class.Ranger: return iModBoth;
                case MM345Class.Archer: return iModInt;
                case MM345Class.Cleric: return iModPer;
                case MM345Class.Druid: return iModBoth;
                case MM345Class.Sorcerer: return iModInt;
                default: return 0;
            }
        }

        private double GetSPDivider(MM345Class mmClass)
        {
            switch (Class)
            {
                case MM345Class.Paladin:
                case MM345Class.Ranger:
                case MM345Class.Archer: return 2.0;
                default: return 1.0;
            }
        }

        private int SPSkillBonus
        {
            get
            {
                switch (Class)
                {
                    case MM345Class.Paladin:
                    case MM345Class.Cleric: return Skills.PrayerMaster > 0 ? 2 : 0;
                    case MM345Class.Ranger:
                    case MM345Class.Druid: return Skills.Astrologer > 0 ? 2 : 0;
                    case MM345Class.Archer:
                    case MM345Class.Sorcerer: return Skills.Prestidigitator > 0 ? 2 : 0;
                    default: return 0;
                }
            }
        }

        protected virtual UInt16 MaxSPForClass(MM345Class mmClass, int iMod)
        {
            double iModClass = GetSPMod(mmClass);
            double iClassDivider = GetSPDivider(mmClass);

            switch (Class)
            {
                case MM345Class.Archer: return (UInt16)(Level.Temporary * (Math.Max(3 + iModClass + iMod + (Skills.Prestidigitator > 0 ? 2 : 0), 1)) / iClassDivider);
                case MM345Class.Sorcerer: return (UInt16)(Level.Temporary * (Math.Max(3 + iModClass + iMod + (Skills.Prestidigitator > 0 ? 2 : 0), 1)) / iClassDivider);
                case MM345Class.Paladin: return (UInt16)(Level.Temporary * (Math.Max(3 + iModClass + iMod + (Skills.PrayerMaster > 0 ? 2 : 0), 1)) / iClassDivider);
                case MM345Class.Cleric: return (UInt16)(Level.Temporary * (Math.Max(3 + iModClass + iMod + (Skills.PrayerMaster > 0 ? 2 : 0), 1)) / iClassDivider);
                case MM345Class.Ranger: return (UInt16)(Level.Temporary * (Math.Max(3 + iModClass + iMod + (Skills.Astrologer > 0 ? 2 : 0), 1)) / iClassDivider);
                case MM345Class.Druid: return (UInt16)(Level.Temporary * (Math.Max(3 + iModClass + iMod + (Skills.Astrologer > 0 ? 2 : 0), 1)) / iClassDivider);
                default: return 0;
            }
        }

        public int MaxSP { get { return MaxSPWithoutItems + Modifiers.SpellPoints; } }

        public int MaxSPWithoutItems
        {
            get
            {
                int iMod = GetRaceModSP(BasicRace, Class);
                return MaxSPForClass(Class, iMod);
            }
        }

        public override string GetMaxSPFormula()
        {
            double modClass = GetSPMod(Class);
            int modRace = GetRaceModSP(BasicRace, Class);
            int modSkill = SPSkillBonus;

            switch (Class)
            {
                case MM345Class.Archer: return String.Format("Level * (3 + SP/lev for {0} + Intellect + Prestidigitator) / 2\r\n{1} * (3 + {2} + {3} + {4}) / 2 = {5}",
                    Race.RaceString(BasicRace), Level.Temporary, modRace, modClass, modSkill, MaxSPWithoutItems);
                case MM345Class.Sorcerer: return String.Format("Level * (3 + SP/lev for {0} + Intellect + Prestidigitator)\r\n{1} * (3 + {2} + {3} + {4}) = {5}",
                    Race.RaceString(BasicRace), Level.Temporary, modRace, modClass, modSkill, MaxSPWithoutItems);
                case MM345Class.Paladin: return String.Format("Level * (3 + SP/lev for {0} + Personality + PrayerMaster) / 2\r\n{1} * (3 + {2} + {3} + {4}) / 2 = {5}",
                    Race.RaceString(BasicRace), Level.Temporary, modRace, modClass, modSkill, MaxSPWithoutItems);
                case MM345Class.Cleric: return String.Format("Level * (3 + SP/lev for {0} + Personality + PrayerMaster)\r\n{1} * (3 + {2} + {3} + {4}) = {5}",
                    Race.RaceString(BasicRace), Level.Temporary, modRace, modClass, modSkill, MaxSPWithoutItems);
                case MM345Class.Ranger: return String.Format("Level * (3 + SP/lev for {0} + (Int+Per)/2 + Astrologer) / 2\r\n{1} * (3 + {2} + {3} + {4}) / 2 = {5}",
                    Race.RaceString(BasicRace), Level.Temporary, modRace, modClass, modSkill, MaxSPWithoutItems);
                case MM345Class.Druid: return String.Format("Level * (3 + SP/lev for {0} + (Int+Per)/2 + Astrologer)\r\n{1} * (3 + {2} + {3} + {4}) = {5}",
                    Race.RaceString(BasicRace), Level.Temporary, modRace, modClass, modSkill, MaxSPWithoutItems);
                default: return String.Empty;
            }
        }

        public SpellType CasterType
        {
            get
            {
                switch (Class)
                {
                    case MM345Class.Archer:
                    case MM345Class.Sorcerer:
                        return SpellType.Sorcerer;
                    case MM345Class.Cleric:
                    case MM345Class.Paladin:
                        return SpellType.Cleric;
                    case MM345Class.Druid:
                    case MM345Class.Ranger:
                        return SpellType.Druid;
                    default:
                        return SpellType.Unknown;
                }
            }
        }

        public override string IntellectEffect()
        {
            StatModifier mod = GetStatModifier(CurrentIntellect, PrimaryStat.Intellect);
            switch (Class)
            {
                case MM345Class.Sorcerer: return IntellectEffect(mod, mod.Value);
                case MM345Class.Archer:
                case MM345Class.Druid: return IntellectEffect(mod, mod.Value / 2.0);
                case MM345Class.Ranger: return IntellectEffect(mod, mod.Value / 4.0);
                default: return String.Empty;
            }
        }

        public override string PersonalityEffect()
        {
            StatModifier mod = GetStatModifier(CurrentPersonality, PrimaryStat.Personality);
            switch (Class)
            {
                case MM345Class.Cleric: return PersonalityEffect(mod, mod.Value);
                case MM345Class.Paladin:
                case MM345Class.Druid: return PersonalityEffect(mod, mod.Value / 2.0);
                case MM345Class.Ranger: return PersonalityEffect(mod, mod.Value / 4.0);
                default: return String.Empty;
            }
        }
    }

    public enum MM345Sex
    {
        Male = 0,
        Female = 1,
        None = 2,
    }

    public enum MM345AlignmentValue
    {
        Good = 0,
        Neutral = 1,
        Evil = 2,
        None = 3
    }

    public enum MM3Race
    {
        Human = 0,
        Elf = 1,
        Gnome = 2,
        Dwarf = 3,
        HalfOrc = 4,
        None = 0
    }

    public enum MM45Race
    {
        Human = 0,
        Elf = 1,
        Dwarf = 2,
        Gnome = 3,
        HalfOrc = 4,
        None = 0
    }

    public enum MM345Class
    {
        Knight = 0,
        Paladin = 1,
        Archer = 2,
        Cleric = 3,
        Sorcerer = 4,
        Robber = 5,
        Ninja = 6,
        Barbarian = 7,
        Druid = 8,
        Ranger = 9,
        None = 0,
    }

    public class MM345SecondarySkills : MMSecondarySkills
    {
        public MM345SecondarySkills()
        {
        }

        public MM345SecondarySkills(byte b)
        {
            SetAll(b);
        }

        public MM345SecondarySkills(byte[] bytes, int index = 0)
        {
            SetFromBytes(bytes, index);
        }

        public MM345SecondarySkills(MM3QuestStates.Skills skills)
        {
            SetFromSkills(skills);
        }
    }

    public abstract class MM345Awards
    {
        public abstract void Clear();
        protected abstract void SetFromBytes(byte[] bytes, int index);
        public abstract byte[] GetBytes();
    }

    public abstract class KnownSpells
    {
        protected byte[] RawBytes;
        public SpellType Type;

        public virtual bool UsesSpellType { get { return true; } }
        public virtual byte[] GetBytes() { return RawBytes; }
        public virtual int NumKnown { get { return 0; } }
        public virtual bool IsKnown(int internalIndex, GenericClass mmClass) { return false; }
        public virtual bool IsKnown(int index, SpellType type) { return false; }
        public virtual string KnownString(GenericClass charClass) { return "?/?"; }
        public virtual KnownSpells CreateNew(GenericClass charClass, KnownSpells original = null) { return null; }
        public virtual void SetKnown(Spell spell, bool bKnown) { }
    }

    public abstract class MM345KnownSpells : KnownSpells
    {
        protected int m_iNumKnown = -1;
        public override int NumKnown { get { return m_iNumKnown; } }
    }

    public class MMBeacon
    {
        public int Map;
        public Point Position;

        public int Side { get { return Map / 256; } }

        public MMBeacon(byte[] bytes, int iOffset, int iOffsetSide = -1)
        {
            Position = new Point(bytes[iOffset + 1], bytes[iOffset + 2]);
            if (iOffsetSide != -1)
                Map = bytes[iOffsetSide] * 256 + bytes[iOffset];
            else
                Map = bytes[iOffset];
        }

        public MMBeacon(int map, Point position)
        {
            Map = map;
            Position = position;
        }

        public MMBeacon(MM3Map map, Point position)
        {
            Map = (int) map;
            Position = position;
        }

        public MMBeacon(MM4Map map, Point position)
        {
            Map = (int) map;
            Position = position;
        }

        public MMBeacon(MM5Map map, Point position)
        {
            Map = (int)map + 256;
            Position = position;
        }

        public byte[] GetBytes()
        {
            return new byte[] { (byte)(Map%256), (byte)Position.X, (byte)Position.Y };
        }

        public string ToString(MemoryHacker hacker)
        {
            if (hacker == null)
                return "None";

            switch (hacker.Game)
            {
                case GameNames.MightAndMagic3:
                case GameNames.MightAndMagic45:
                    if (Map == -1 || Map == 0)
                        return "None";
                    break;
                default:
                    if (Map == -1)
                        return "None";
                    break;
            }
            return String.Format("{0} ({1},{2})", hacker.GetMapTitle(Map).Title, Position.X, Position.Y);
        }
    }

    [Flags]
    public enum MM345UsableFlags
    {
        None = 0x0000,
        Barbarian = 0x0001,
        Ninja = 0x0002,
        Robber = 0x0004,
        Sorcerer = 0x0008,
        Cleric = 0x0010,
        Archer = 0x0020,
        Paladin = 0x0040,
        Knight = 0x0080,
        Ranger = 0x0100,
        Druid = 0x0200,
        KP = Knight | Paladin,
        KPACTNBDR = Knight | Paladin | Archer | Cleric | Robber | Ninja | Barbarian | Druid | Ranger,
        KPACTNBR = Knight | Paladin | Archer | Cleric | Robber | Ninja | Barbarian | Ranger,
        KPACTNR = Knight | Paladin | Archer | Cleric | Robber | Ninja | Ranger,
        KPACTR = Knight | Paladin | Archer | Cleric | Robber | Ranger,
        KPAR = Knight | Paladin | Archer | Ranger,
        KPASTNBDR = Knight | Paladin | Archer | Robber | Sorcerer | Ninja | Barbarian | Druid | Ranger,
        KPATBR = Knight | Paladin | Archer | Robber | Barbarian | Ranger,
        KPATNBDR = Knight | Paladin | Archer | Robber | Ninja | Barbarian | Druid | Ranger,
        KPATNBR = Knight | Paladin | Archer | Robber | Ninja | Barbarian | Ranger,
        KPATNR = Knight | Paladin | Archer | Robber | Ninja | Ranger,
        KPATR = Knight | Paladin | Archer | Robber | Ranger,
        KPCR = Knight | Paladin | Cleric | Ranger,
        KPCTBR = Knight | Paladin | Cleric | Robber | Barbarian | Ranger,
        KPN = Knight | Paladin | Ninja,
        AnyClass = 0x03ff,
    }
}
