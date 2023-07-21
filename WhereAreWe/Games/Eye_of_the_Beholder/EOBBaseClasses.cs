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
    public abstract class EOBMemory
    {
        public abstract byte[] MainSearch { get; }
        public abstract MemoryGuess[] Guesses { get; }

        public abstract int MainBlockSVN { get; }
        public abstract int MainBlockOldSVN { get; }
        public virtual int MainBlockNonSVN { get { return MainBlockSVN; } }

        public abstract int Map { get; }
        public abstract int LocationXY { get; }
        public abstract int Facing { get; }
        public abstract int PartyInfo { get; }
        public abstract int ItemList { get; }
        public abstract int ItemBasicList { get; }
        public abstract int ScriptBits { get; }
        public abstract int ScriptBits2 { get; }
        public abstract int MonsterHP { get; }
        public abstract int MonsterList { get; }
        public abstract int InspectingChar { get; }
        public abstract int MainMapIndex { get; }
        public abstract int GameTimeSeconds { get; }
        public abstract int PartyNames { get; }
        public abstract int CreationStats { get; }
        public abstract int CreationRace { get; }
        public abstract int MapSquareStrings { get; }
        public abstract int MapSpecials { get; }
        public abstract int AskCastSpell { get; }
        public abstract int AskWhichSpell { get; }
        public abstract int ScreenText { get; }
        public abstract int CampInspectingChar { get; }
        public abstract int MarchingOrder { get; }
    }

    public class EOBSpell : Spell
    {
        public SpellDuration Duration;

        public override string MultiLineDescription
        {
            get
            {
                return String.Format("Name: {0}\r\nType: {1}\r\nLevel: {2}\r\nWhen: {3}\r\nTarget: {4}\r\nDuration: {5}\r\nShort: {6}\r\n{7}",
                    Name, TypeString(Type), Level, WhenString, TargetStringFull, Spell.GetDurationString(Duration), ShortDescription, Description);
            }
        }

        public override string DurationString { get { return Spell.GetDurationString(Duration); } }

        public override string ExtendedName
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Abbreviation))
                    return Name;
                return String.Format("{0} ({1})", Name, Abbreviation);
            }
        }
    }
    
    public abstract class EOBEncounterInfo : EncounterInfo
    {
        protected Dictionary<int, Monster> m_monsters;

        public override Dictionary<int, Monster> Monsters { get { return m_monsters; } set { m_monsters = value; } }

        public override bool InCombat => m_monsters == null ? false : m_monsters.Values.Any(v => !v.Killed);
        public override int NumLivingMonsters => m_monsters == null ? 0 : m_monsters.Values.Count(m => m.IsAlive);

        public override TurnOrderCalculator GetTurnOrder(CharCombatLabel[] labelChars, GameInfo gameInfo)
        {
            if (Party == null || Party.CharacterSize < 1)
                return null;

            TurnOrderCalculator toc = new TurnOrderCalculator(0, 0);

            EOBCharacter[] characters = new EOBCharacter[Party.Bytes.Length / Party.CharacterSize];
            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
            {
                characters[iIndex] = EOBCharacter.Create(gameInfo.Game, null, Party.MarchingIndex(iIndex), Party.Bytes, Party.MarchingIndex(iIndex) * Games.CharacterSize(gameInfo.Game));
                labelChars[iIndex].Melee = iIndex < 2;
                labelChars[iIndex].Condition = characters[iIndex].BasicCondition;
                labelChars[iIndex].CharName = String.Format("{0})  {1}", iIndex + 1, characters[iIndex].CharName);
                labelChars[iIndex].HP = characters[iIndex].HitPoints.Temporary.ToString();
                labelChars[iIndex].SP = "";
            }
            for (byte iIndex = Party.NumChars; iIndex < 8; iIndex++)
                labelChars[iIndex].Clear();

            return toc;
        }
    }

    public abstract class EOBBaseCharacter : BaseCharacter
    {
        public virtual StatAndModifier BasicStrength { get { return null; } }
        public virtual StatAndModifier BasicStrength18 { get { return null; } }
        public virtual StatAndModifier BasicIntelligence { get { return null; } }
        public virtual StatAndModifier BasicWisdom { get { return null; } }
        public virtual StatAndModifier BasicDexterity { get { return null; } }
        public virtual StatAndModifier BasicConstitution { get { return null; } }
        public virtual StatAndModifier BasicCharisma { get { return null; } }
        public virtual ResistanceValue SaveVsParalyzePoisonDeath { get { return null; } }
        public virtual ResistanceValue SaveVsWand { get { return null; } }
        public virtual ResistanceValue SaveVsPetfifyPolymorph { get { return null; } }
        public virtual ResistanceValue SaveVsBreath { get { return null; } }
        public virtual ResistanceValue SaveVsSpell { get { return null; } }
        public override bool PartyGems { get { return true; } }

        public override StatAndModifier[] PrimaryStats
        {
            get { return new StatAndModifier[] { BasicStrength, BasicStrength18, BasicIntelligence, BasicWisdom, BasicDexterity, BasicConstitution, BasicCharisma }; }
        }

        public override StatAndModifier Stat(string str)
        {
            if (str == null || str.Length < 1)
                return StatAndModifier.Zero;
            switch (Char.ToLower(str[0]))
            {
                case 's': return BasicStrength;
                case '1': return BasicStrength18;
                case 'i': return BasicIntelligence;
                case 'w': return BasicWisdom;
                case 'd': return BasicDexterity;
                case 'c': return BasicConstitution;
                case 'h': return BasicCharisma;
                default: return StatAndModifier.Zero;
            }
        }

        public virtual int CurrentStrength { get { return BasicStrength.Temporary + Modifiers.Strength; } }
        public virtual int CurrentStrength18 { get { return BasicStrength18.Temporary + Modifiers.Strength18; } }
        public virtual int CurrentIntelligence { get { return BasicIntelligence.Temporary + Modifiers.Intelligence; } }
        public virtual int CurrentWisdom{ get { return BasicWisdom.Temporary + Modifiers.Wisdom; } }
        public virtual int CurrentDexterity { get { return BasicDexterity.Temporary + Modifiers.Dexterity; } }
        public virtual int CurrentConstitution { get { return BasicConstitution.Temporary + Modifiers.Constitution; } }
        public virtual int CurrentCharisma { get { return BasicCharisma.Temporary + Modifiers.Charisma; } }

        public override string AttributeTip(ModAttr attrib, MemoryHacker hacker)
        {
            string strBase = base.AttributeTip(attrib, hacker);
            if (!String.IsNullOrWhiteSpace(strBase))
                return strBase;

            switch (attrib)
            {
                case ModAttr.Strength: return StrengthTip();
                case ModAttr.Intelligence: return IntelligenceTip();
                case ModAttr.Wisdom: return WisdomTip();
                case ModAttr.Dexterity: return DexterityTip();
                case ModAttr.Constitution: return GetModifier(CurrentConstitution, PrimaryStat.Constitution).TipString("{0} Constitution: {1} HP per level ({2})");
                case ModAttr.Charisma: return GetModifier(CurrentCharisma, PrimaryStat.Charisma).TipString("{0} Charisma: No practical effects", CurrentCharisma);
                case ModAttr.SaveVsPoison: return SaveTipString(GenericResistanceFlags.SaveVsPoison, SaveVsParalyzePoisonDeath);
                case ModAttr.SaveVsWand: return SaveTipString(GenericResistanceFlags.SaveVsWand, SaveVsWand);
                case ModAttr.SaveVsPetrify: return SaveTipString(GenericResistanceFlags.SaveVsPetrify, SaveVsPetfifyPolymorph);
                case ModAttr.SaveVsBreath: return SaveTipString(GenericResistanceFlags.SaveVsBreath, SaveVsBreath);
                case ModAttr.SaveVsSpell: return SaveTipString(GenericResistanceFlags.SaveVsSpell, SaveVsSpell);
                case ModAttr.MeleeDamage: return BasicInventory.MeleeWeaponName;
                case ModAttr.SpellPoints: return SpellTip();
                case ModAttr.MeleeToHit: return THAC0Tip();
                case ModAttr.Experience: return ExperienceTip();
                case ModAttr.Spellbook: return SpellbookTip();
                default: return String.Empty;
            }
        }

        public virtual int[] GetSpells(GenericClass gc) { return new int[0]; }

        public static bool UsesStrength18(GenericClass gc) { return IsWarrior(gc); }

        public static bool IsWarrior(GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Fighter:
                case GenericClass.Ranger:
                case GenericClass.Paladin:
                case GenericClass.FighterCleric:
                case GenericClass.FighterClericMage:
                case GenericClass.FighterMage:
                case GenericClass.FighterMageThief:
                case GenericClass.FighterThief:
                case GenericClass.RangerCleric:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsMage(GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Mage:
                case GenericClass.FighterClericMage:
                case GenericClass.FighterMage:
                case GenericClass.FighterMageThief:
                case GenericClass.ClericMage:
                case GenericClass.ThiefMage:
                    return true;
                default:
                    return false;
            }
        }

        public static bool IsCleric(GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Cleric:
                case GenericClass.FighterClericMage:
                case GenericClass.ClericThief:
                case GenericClass.RangerCleric:
                case GenericClass.ClericMage:
                case GenericClass.FighterCleric:
                    return true;
                default:
                    return false;
            }
        }

        public static int MaxMageSpellLevel(int iInt) { return iInt < 9 ? 0 : Math.Min(9, iInt / 2); }

        public static int MageLearnChance(int iInt)
        {
            if (iInt > 23) return 100;
            else if (iInt == 18) return 85;
            else if (iInt > 18) return 76 + iInt;
            else if (iInt > 8) return 5 * (iInt - 2);
            return 0;
        }

        public static int MageSpellsLevelLimit(int iInt)
        {
            if (iInt > 18) return -1;
            if (iInt > 17) return 18;
            if (iInt > 16) return 14;
            if (iInt > 14) return 11;
            if (iInt > 12) return 9;
            if (iInt > 9) return 7;
            if (iInt > 8) return 6;
            return 0;
        }

        public string IntelligenceTip()
        {
            if (!IsMage(BasicClass))
                return String.Format("{0} Intelligence (no effect for non-Mage classes)", CurrentIntelligence);
            StringBuilder sb = new StringBuilder();
            int iSpellLevelLimit = MageSpellsLevelLimit(CurrentIntelligence);
            sb.AppendFormat("{0} Intelligence:\r\n", CurrentIntelligence);
            sb.AppendFormat("Max spell level: {0}\r\n", MaxMageSpellLevel(CurrentIntelligence));
            sb.AppendFormat("Chance to learn spells: {0}% (not used)\r\n", MageLearnChance(CurrentIntelligence));
            sb.AppendFormat("Max spells per level: {0}\r\n", iSpellLevelLimit == -1 ? "All" : iSpellLevelLimit.ToString());
            return sb.ToString();
        }

        public static int PriestSpellFailChance(int iWisdom)
        {
            if (iWisdom < 10) return 20;
            if (iWisdom < 11) return 15;
            if (iWisdom < 12) return 10;
            if (iWisdom < 13) return 5;
            return 0;
        }

        public static string BonusSpellString(int[] levels)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i < levels.Length; i++)
            {
                if (levels[i] == 0)
                    break;
                sb.AppendFormat("{0}/", levels[i]);
            }
            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public string WisdomTip()
        {
            if (!IsCleric(BasicClass))
                return String.Format("{0} Wisdom (no effect for non-Cleric classes)", CurrentWisdom);
            StringBuilder sb = new StringBuilder();
            int[] bonus = EOB1Character.GetClericBonusSpells(CurrentWisdom);
            sb.AppendFormat("{0} Wisdom:\r\n", CurrentWisdom);
            sb.AppendFormat("Bonus spells: {0}\r\n", BonusSpellString(bonus));
            sb.AppendFormat("Spell failure: {0}%\r\n", PriestSpellFailChance(CurrentWisdom));
            return sb.ToString();
        }

        public string THAC0Tip()
        {
            bool bUse18 = CurrentStrength == 18 && UsesStrength18(BasicClass);
            StrengthWith18 s18 = new StrengthWith18(BasicStrength.Temporary, bUse18 ? BasicStrength18.Temporary : 0);
            StatModifier mod = EOBCharacter.GetMeleeHitModifier(s18);
            GenericClass bestClass = BasicClass;
            int iLevel = 1;
            int iTHAC0 = 20;
            if (this is EOB1Character)
                iTHAC0 = ((EOB1Character)this).GetClassTHAC0(out bestClass, out iLevel);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}\tBase THAC0 (Level {1} {2})", EOBCharacter.GetBaseTHAC0(bestClass, iLevel), iLevel, ClassString(bestClass));
            if (mod.Value != 0)
            {
                sb.AppendFormat("\r\n{0}\tStrength modifier", mod.PlusValue);
                sb.AppendFormat("\r\nNote: Positive THAC0 bonuses reduce total THAC0 by that amount");
            }
            return sb.ToString();
        }

        public string DexterityTip()
        {
            StatModifier modAC = EOBCharacter.GetACModifier(CurrentDexterity);
            StatModifier modMissile = EOBCharacter.GetMissileHitModifier(CurrentDexterity);
            return String.Format("{0} Dexterity:\r\n{1} AC ({2})\r\n{3} to-hit missile ({4})",
                CurrentDexterity, modAC.PlusValue, StatModifier.NextBonus(modAC.Next), modMissile.PlusValue, StatModifier.NextBonus(modMissile.Next));
        }

        public string StrengthTip()
        {
            bool bUse18 = CurrentStrength == 18 && CurrentStrength18 > 0 && UsesStrength18(BasicClass);
            StrengthWith18 s18 = new StrengthWith18(BasicStrength.Temporary, bUse18 ? BasicStrength18.Temporary : 0);
            StatModifier modMelee = EOBCharacter.GetMeleeHitModifier(s18);
            StatModifier modDamage = EOBCharacter.GetMeleeDamageModifier(s18);
            string strNextHit = "maximum bonus";
            string strNextDamage = "maximum bonus";
            if (bUse18)
            {
                int iNextHit = 51;
                if (CurrentStrength18 > 50)
                    iNextHit = 100;
                int iNextDamage = 1;
                if (CurrentStrength18 > 90)
                    iNextDamage = 100;
                else if (CurrentStrength18 > 75)
                    iNextDamage = 91;
                else if (CurrentStrength18 > 0)
                    iNextDamage = 76;
                if (iNextHit != -1)
                    strNextHit = String.Format("next bonus at {0}", CurrentStrength18 == 100 ? "19" : String.Format("18/{0:D2}", iNextHit > 99 ? 0 : iNextHit));
                if (iNextDamage != -1)
                    strNextDamage = String.Format("next bonus at {0}", CurrentStrength18 == 100 ? "19" : String.Format("18/{0:D2}", iNextDamage > 99 ? 0 : iNextDamage));
            }
            else
            {
                strNextHit = StatModifier.NextBonus(modMelee.Next);
                strNextDamage = StatModifier.NextBonus(modDamage.Next);
            }
            return String.Format("{0} Strength:\r\n{1} THAC0 ({2})\r\n{3} damage ({4})" +
                "\r\nNote: Positive THAC0 bonuses reduce total THAC0 by that amount",
                s18.ToString(), modMelee.PlusValue, strNextHit, modDamage.PlusValue, strNextDamage);
        }

        public string SpellTip()
        {
            StringBuilder sb = new StringBuilder();
            int[] cleric = GetSpells(GenericClass.Cleric);
            int[] clericBonus = EOBCharacter.GetClericBonusSpells(BasicWisdom.Permanent);
            int[] mage = GetSpells(GenericClass.Mage);
            for (int i = 1; i < cleric.Length; i++)
            {
                if (cleric[i] > 0)
                {
                    sb.AppendFormat("{0} Cleric level {1}", cleric[i], i);
                    if (cleric[i] > 0 && clericBonus[i] > 0)
                        sb.AppendFormat(" ({0} + {1} for Wisdom)", cleric[i] - clericBonus[i], clericBonus[i]);
                    sb.AppendFormat("\r\n");
                }
            }
            for (int i = 1; i < mage.Length; i++)
            {
                if (mage[i] > 0)
                    sb.AppendFormat("{0} Mage level {1}\r\n", mage[i], i);
            }
            return sb.ToString().Trim();
        }

        public string SpellbookTip()
        {
            StringBuilder sb = new StringBuilder("Memorized Spells:");
            bool bAny = false;
            EOBCharacter eobChar = this as EOBCharacter;
            Dictionary<EOBSpell, int> spellCounts = new Dictionary<EOBSpell, int>();
            foreach (EOBSpell spell in eobChar.Spells.Memorized)
            {
                if (!spellCounts.ContainsKey(spell))
                    spellCounts.Add(spell, 0);
                spellCounts[spell] += 1;
            }
            foreach(EOBSpell spell in spellCounts.Keys)
            {
                bAny = true;
                sb.AppendFormat("\r\n{0}", spell.Name);
                if (spellCounts[spell] > 1)
                    sb.AppendFormat(" ({0})", spellCounts[spell]);
            }
            if (!bAny)
                sb.AppendFormat(" (none)");
            return sb.ToString();
        }

        public string ExperienceTip()
        {
            EOBCharacter eobChar = this as EOBCharacter;
            if (eobChar == null)
                return "";
            GenericClass[] classes = Global.SeparateClasses(BasicClass);
            StringBuilder sb = new StringBuilder("Experience for class:");
            for (int i = 0; i < classes.Length; i++)
            {
                sb.AppendFormat("\r\n{0}: {1}/{2}",
                    ClassString(classes[i]), 
                    eobChar.Experience[i],
                    eobChar.Level[i] >= eobChar.GetMaxLevel(classes[i]) ? "Max" : eobChar.GetXPForNextLevel(eobChar.Experience[i], EOBCharacter.ClassForGeneric(classes[i])).ToString());
            }
            return sb.ToString();
        }

        public static string SaveString(GenericResistanceFlags save)
        {
            switch (save)
            {
                case GenericResistanceFlags.SaveVsPoison:
                case GenericResistanceFlags.SaveVsParalyze:
                    return "Save vs. Paralyze/Poison/Death";
                case GenericResistanceFlags.SaveVsWand:
                    return "Save vs. Wand";
                case GenericResistanceFlags.SaveVsPetrify:
                    return "Save vs. Petrification/Polymorph";
                case GenericResistanceFlags.SaveVsBreath:
                    return "Save vs. Breath Weapon";
                case GenericResistanceFlags.SaveVsSpell:
                    return "Save vs. Spell";
                default: return "Unknown saving throw";
            }
        }

        public static string SaveTipString(GenericResistanceFlags save, ResistanceValue value)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}% chance to avoid this type of effect\r\n", SaveString(save), 5 * (21 - value.Total));
            sb.AppendFormat("{0}\tBase value for a {1}", value.BaseValue, value.ReasonBase);
            return sb.ToString();
        }

        public static GenericClass[] EOBClasses
        {
            get
            {
                return new GenericClass[] {
                    GenericClass.Fighter,
                    GenericClass.Paladin,
                    GenericClass.Ranger,
                    GenericClass.Mage,
                    GenericClass.Cleric,
                    GenericClass.Thief,
                    GenericClass.FighterCleric,
                    GenericClass.FighterThief,
                    GenericClass.FighterMage,
                    GenericClass.FighterMageThief,
                    GenericClass.ThiefMage,
                    GenericClass.FighterClericMage,
                    GenericClass.RangerCleric,
                    GenericClass.ClericMage};
            }
        }
    }

    public class DnDKnownSpells : KnownSpells
    {
        public DnDKnownSpells()
        {
        }

        public void SetBytes(byte[] bytes) { RawBytes = bytes; }

        public override KnownSpells CreateNew(GenericClass charClass, KnownSpells original = null) { return FromBytes(original); }

        public static DnDKnownSpells FromBytes(KnownSpells original = null)
        {
            DnDKnownSpells spells = new DnDKnownSpells();
            byte[] bytes = Global.NullBytes(64);
            if (original != null)
                Buffer.BlockCopy(original.GetBytes(), 0, bytes, 0, Math.Min(bytes.Length, original.GetBytes().Length));
            spells.SetBytes(bytes);
            return spells;
        }

        public virtual List<Spell> Memorized { get { return new List<Spell>(); } }
        public virtual List<Spell> Selected { get { return new List<Spell>(); } }
        public virtual int[] Add(SpellTag spell) { return new int[0]; }

        public virtual bool InBook(int index) { return false; }
        public virtual void SetBook(int index, bool bLearned = true) { }

        // Virtual so that a spellbook can provide a more optimized function than "get every spell as an object and check them"
        public virtual int NumSelected(int index) { return Selected.Count(s => s.BasicIndex == index); }
        public virtual int NumMemorized(int index) { return Memorized.Count(s => s.BasicIndex == index); }
        public virtual int NumSelected(SpellType type) { return Selected.Count(s => s.Type == type); }
        public virtual int NumMemorized(SpellType type) { return Memorized.Count(s => s.Type == type); }

        public virtual int Maximum(SpellType type) { return 30; }
        public virtual int Available(SpellType type) { return Maximum(type); }
    }
}
