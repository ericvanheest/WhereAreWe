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
    public abstract class BTMemory
    {
        public abstract byte[] MainSearch { get; }
        public abstract MemoryGuess[] Guesses { get; }

        public abstract int MainBlockSVN { get; }
        public abstract int MainBlockOldSVN { get; }
        public virtual int MainBlockNonSVN { get { return MainBlockSVN; } }

        public abstract int FacingTown { get; }
        public abstract int Facing { get; }
        public abstract int LocationNorth { get; }
        public abstract int LocationEast { get; }
        public abstract int LocationNorthTown { get; }
        public abstract int LocationEastTown { get; }
        public abstract int NumChars { get; }
        public abstract int Map { get; }
        public abstract int MapSpecials { get; }
        public abstract int ItemList { get; }
        public abstract int PartyInfo { get; }
        public abstract int PartyNames { get; }
        public abstract int EncounterInfo { get; }
        public abstract int MarchingOrder { get; }
        public abstract int InspectingChar { get; }
        public abstract int CampInspectingChar { get; }
        public abstract int CastingChar { get; }
        public abstract int CombatActingChar1 { get; }
        public abstract int CombatActingChar2 { get; }
        public abstract int CombatActingChar3 { get; }
        public abstract int ShoppingChar { get; }
        public abstract int ShopInventory { get; }
        public abstract int ShopInspectingChar { get; }
        public abstract int CreationStats { get; }
        public abstract int CreationRace { get; }
        public abstract int MainMapIndex { get; }
        public abstract int SubMapIndex { get; }
        public abstract int MapStrings { get; }
        public abstract int MapSquareStrings { get; }
        public abstract int MonsterHP { get; }
        public abstract int MonsterIndices { get; }
        public abstract int MonsterNumAlive { get; }
        public abstract int AskCastSpell { get; }
        public abstract int AskWhichSpell { get; }
        public abstract int AskWhichSpellCombat { get; }
        public abstract int AskWhichSpellCombat2 { get; }
        public abstract int AskWhichSong { get; }
        public abstract int GameTimeHours { get; }
        public abstract int GameTimeSeconds { get; }
        public abstract int SpellIcon1 { get; }
        public abstract int SpellIcon2 { get; }
        public abstract int SpellIcon3 { get; }
        public abstract int SpellIcon4 { get; }
        public abstract int SpellIcon5 { get; }
        public abstract int LightDistance { get; }
        public abstract int LevitationDuration { get; }
        public abstract int ShieldDuration { get; }
        public abstract int LightDuration { get; }
        public abstract int DetectionDuration { get; }
        public abstract int CompassDuration { get; }
        public abstract int AdventuringSong { get; }
        public abstract int CombatSong { get; }
        public abstract int SongDuration { get; }
        public abstract int CharCombatACBonus { get; }
        public abstract int CharCombatDamageBonus { get; }
        public abstract int EnemyDamageBonus { get; }
        public abstract int EnemyACBonus { get; }
        public abstract int EnemyLoseTurn { get; }
        public abstract int PartyCombatACBonus { get; }
        public abstract int PartyCombatMagicResist { get; }
        public abstract int PartyCombatOptions { get; }
        public abstract int PartyCombatSubOptions1 { get; }
        public abstract int PartyCombatSubOptions2 { get; }
        public abstract int PartyCombatSelectedSpells { get; }
        public abstract int SummonedCreature { get; }
        public abstract int TreasureState { get; }
        public abstract int TrapType { get; }
        public abstract int TrapExamined { get; }
        public abstract int ItemACBonus { get; }
        public abstract int ItemTypes { get; }
        public abstract int ItemUsableBy { get; }
        public abstract int ItemEffects { get; }
        public abstract int ItemDamage { get; }
        public abstract int ItemValues { get; }
        public abstract int ItemCharges { get; }
        public abstract int ItemEquipEffect { get; }
        public abstract int MonsterGroup { get; }
        public abstract int MonsterAC { get; }
        public abstract int MonsterDamage { get; }
        public abstract int MonsterExp { get; }
        public abstract int ScreenText { get; }
        public abstract int TreasureRanges { get; }
        public abstract int TreasureMinimums { get; }
        public abstract int MapTreasureIndex { get; }
        public abstract int EncounterMonstersKilled { get; }
        public abstract int MapGoldMax { get; }
        public abstract int SwapWallsDoors { get; }
        public abstract int ForcedEncounters { get; }
        public abstract int TownMap { get; }
        public abstract int ImageCaption { get; }
        public abstract int NumItemsInShop { get; }
        public abstract int AdvPartyACBonus { get; }
        public abstract int MonsterDistances { get; }
        public abstract int CharCombatDamageBonus2 { get; }
        public abstract int PartyPerishSeconds { get ; }
        public abstract int Counter1 { get; }
        public abstract int PartyBonusAttacks { get; }
        public abstract int SurfaceMapIndex { get; }

        public abstract int State1 { get; }
        public abstract int State2 { get; }
        public abstract int State3 { get; }
        public abstract int State4 { get; }
        public abstract int Stack { get; }
        public virtual int StackSize { get { return 512; } }
        public abstract int StackAddressIndicator { get; }
        public abstract int MapCustomSquares { get; }
        public abstract int MapFixedSquares { get; }
        public abstract int MapTeleport { get; }
        public abstract int MapSpecials2 { get; }
    }

    public class BTSpell : Spell
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

    public abstract class BTCombatEffects
    {
        public byte[] CharAC;
        public byte[] CharDamage;
        public byte[] CharDamage2;
        public byte[] MonsterAC;
        public byte[] MonsterDamage;
        public byte[] MonsterLoseTurn;
        public byte[] PartyOptions;
        public byte[] PartySubOptions1;
        public byte[] PartySubOptions2;
        public byte[] PartySelectedSpells;
        public byte PartyAC;
        public byte PartyMagicRes;
        public byte PartyAttacks;
        public byte TreasureState;
        public byte Trap;
        public BTGameState GameState;
        public byte ActingChar;
        public string[] Names;

        public virtual void InitSpecific(BTMemoryHacker hacker) { }

        public void Init(BTMemoryHacker hacker, int iChars, int iMonsters)
        {
            GameState = hacker.GetGameState() as BTGameState;
            CharDamage = new byte[iChars];
            CharDamage2 = new byte[iChars];
            MonsterAC = new byte[iMonsters];
            MonsterDamage = new byte[iMonsters];
            MonsterLoseTurn = new byte[iMonsters];
            PartyOptions = new byte[iChars];
            PartySubOptions1 = new byte[iChars];
            PartySelectedSpells = new byte[iChars];

            hacker.ReadOffset(hacker.Memory.CharCombatDamageBonus, CharDamage);
            hacker.ReadOffset(hacker.Memory.CharCombatDamageBonus2, CharDamage2);
            hacker.ReadOffset(hacker.Memory.EnemyACBonus, MonsterAC);
            hacker.ReadOffset(hacker.Memory.EnemyDamageBonus, MonsterDamage);
            hacker.ReadOffset(hacker.Memory.EnemyLoseTurn, MonsterLoseTurn);
            hacker.ReadOffset(hacker.Memory.PartyCombatOptions, PartyOptions);
            hacker.ReadOffset(hacker.Memory.PartyCombatSubOptions1, PartySubOptions1);
            hacker.ReadOffset(hacker.Memory.PartyCombatSelectedSpells, PartySelectedSpells);
            Names = hacker.GetCharacterNames();
            PartyAC = hacker.ReadByte(hacker.Memory.PartyCombatACBonus);
            PartyAttacks = hacker.ReadByte(hacker.Memory.PartyBonusAttacks);
            Trap = hacker.ReadByte(hacker.Memory.TrapType);
            PartyMagicRes = hacker.ReadByte(hacker.Memory.PartyCombatMagicResist);
            if (GameState.Main == MainState.CombatConfirmRound)
                ActingChar = (byte) (iChars + 1);
            else
                ActingChar = (byte)GameState.ActingChar;

            TreasureState = hacker.ReadByte(hacker.Memory.TreasureState);

            InitSpecific(hacker);
        }

        public string GetCharTarget(int iChar) { return GetTarget(PartySubOptions1[iChar]); }

        public abstract string GetTarget(int iTarget);
        public abstract string GetItem(int iChar);
        public abstract string SpellName(byte b);
        public abstract bool HasTarget(byte b);

        public string GetAction(int iChar)
        {
            if (iChar < 0 || iChar > PartyOptions.Length)
                return String.Empty;

            string strName = Names[iChar];

            switch (PartyOptions[iChar])
            {
                case 1: return String.Format("{0}: Attack {1}, ", strName, GetCharTarget(iChar));
                case 2: return String.Format("{0}: Defend, ", strName);
                case 3: return String.Format("{0}: Cast {1}{2}, ", strName, SpellName(PartySelectedSpells[iChar]), 
                    HasTarget(PartySelectedSpells[iChar]) ? String.Format(" on {0}", GetCharTarget(iChar)) : "");
                case 4: return String.Format("{0}: Use {1}, ", strName, GetItem(iChar));
                case 5: return String.Format("{0}: Hide, ", strName);
                case 6: return String.Format("{0}: Sing #{1}, ", strName, PartySubOptions1[iChar]);
                case 7: return String.Format("{0}: Attack {1}, ", strName, GetCharTarget(iChar));
            }

            return String.Empty;
        }

        public byte[] Bytes { get { return Global.Combine(CharAC, CharDamage, CharDamage2, MonsterAC, MonsterDamage, MonsterLoseTurn, PartyOptions,
            PartySubOptions1, PartySubOptions2, PartySelectedSpells, new byte[] { PartyAC, PartyMagicRes, ActingChar, TreasureState, Trap, PartyAttacks }); } }

        public bool AllZero
        {
            get
            {
                if (PartyAC != 0 || PartyAttacks != 0 || PartyMagicRes != 0 || ActingChar != 0)
                    return false;
                foreach (byte[] bytes in new byte[][] { CharAC, CharDamage, MonsterAC, MonsterDamage, MonsterLoseTurn,
                    PartyOptions, PartySubOptions1, PartySubOptions2, PartySelectedSpells })
                    if (!Global.AllNull(bytes))
                        return false;
                return true;
            }
        }
    }

    public class BTEncounterInfo : EncounterInfo
    {
        public byte[] MonsterIndices;
        public byte[] MonsterHP;
        public byte[] Living;
        public BTCombatEffects Effects = null;
        protected Dictionary<int, Monster> m_monsters;

        public override Dictionary<int, Monster> Monsters { get { return m_monsters; } set { m_monsters = value; } }

        public BTEncounterInfo()
        {
        }

        public BTEncounterInfo(byte[] indices, byte[] hp, byte[] living, BTCombatEffects effects)
        {
            MonsterIndices = indices;
            MonsterHP = hp;
            Living = living;
            Effects = effects;
            AllBytes = Global.Combine(indices, hp, effects.Bytes);
        }

        public override bool HasTreasure { get { return Effects != null && Effects.TreasureState == 2; } }

        public virtual void UpdateLivingMonstersBytes()
        {
            if (MonsterHP == null)
                Living = Global.NullBytes(4);
            Living = new byte[] { 100, 100, 100, 100 };
            for (byte i = 0; i < 100; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (Living[j] == 100 && MonsterHP[j * 100 + i] == 0)
                    {
                        Living[j] = i;
                        if (Living.All(b => b != 100))
                            return;
                    }
                }
            }
        }

        public override bool InCombat
        {
            get
            {
                if (MonsterIndices == null)
                    return false;
                               return MonsterIndices.Any(m => m != 0);
            }
        }

        public int NumAlive(int iGroup)
        {
            int iCount = 0;
            for (int i = iGroup * 100; i < (iGroup + 1) * 100; i++)
                if (MonsterHP[i] > 0)
                    iCount++;
            return iCount;
        }

        public override TurnOrderCalculator GetTurnOrder(CharCombatLabel[] labelChars, GameInfo gameInfo)
        {
            TurnOrderCalculator toc = new TurnOrderCalculator(0, 0);

            BTCharacter[] characters = new BTCharacter[Party.Bytes.Length / Party.CharacterSize];
            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
            {
                characters[iIndex] = BTCharacter.Create(gameInfo.Game, Party.MarchingIndex(iIndex), Party.Bytes, Party.MarchingIndex(iIndex) * Games.CharacterSize(gameInfo.Game));
                labelChars[iIndex].Melee = iIndex < (gameInfo.Game == GameNames.BardsTale3 ? 4 : 3);
                labelChars[iIndex].Condition = characters[iIndex].BasicCondition;
                labelChars[iIndex].CharName = String.Format("{0})  {1}", iIndex + 1, characters[iIndex].CharName);
                labelChars[iIndex].HP = characters[iIndex].HitPoints.Temporary.ToString();
                labelChars[iIndex].SP = characters[iIndex].SpellPoints.Temporary.ToString();
            }
            for (byte iIndex = Party.NumChars; iIndex < 8; iIndex++)
                labelChars[iIndex].Clear();

            return toc;
        }

        public override string ExtraText
        {
            get
            {
                if (Effects.AllZero || !Effects.GameState.InCombat)
                    return String.Empty;
                StringBuilder sb = new StringBuilder();
                for(int i = 0; i < 7; i++)
                {
                    if (Party.Addresses.Length <= i)
                        break;
                    if (Party.Addresses[i] == Effects.GameState.ActingChar && Effects.GameState.Main != MainState.CombatConfirmRound)
                        break;
                    sb.Append(Effects.GetAction(i));
                }

                Global.Trim(sb);
                if (sb.Length > 0)
                    sb.AppendFormat("\r\n");
                if (Effects.PartyAC != 0 || Effects.PartyMagicRes != 0 || Effects.PartyAttacks != 0)
                {
                    if (Effects.PartyAC != 0)
                        sb.AppendFormat("Party AC Bonus: {0}, ", Effects.PartyAC);
                    if (Effects.PartyMagicRes != 0)
                        sb.AppendFormat("Party Magic Resist: {0}, ", Effects.PartyMagicRes);
                    if (Effects.PartyAttacks != 0)
                        sb.AppendFormat("Party Bonus Attacks: {0}, ", Effects.PartyAttacks);
                    Global.Trim(sb).Append("\r\n");
                }

                bool bACBonus = false;
                for (int i = 0; i < Effects.CharAC.Length; i++)
                {
                    if (Effects.CharAC[i] != 0 && Effects.Names.Length > i)
                    {
                        if (!bACBonus)
                        {
                            bACBonus = true;
                            sb.Append("AC Bonuses: ");
                        }
                        sb.AppendFormat("{0} ({1}), ", Effects.Names[i], (sbyte) Effects.CharAC[i]);
                    }
                }
                if (bACBonus)
                {
                    Global.Trim(sb);
                    sb.AppendFormat("\r\n");
                }

                bool bToHitBonus = false;
                for (int i = 0; i < Effects.CharDamage.Length; i++)
                {
                    if (Effects.CharDamage[i] != 0 && Effects.Names.Length > i)
                    {
                        if (!bToHitBonus)
                        {
                            bToHitBonus = true;
                            sb.Append("To-Hit Bonuses: ");
                        }
                        sb.AppendFormat("{0} ({1}), ", Effects.Names[i], (sbyte) Effects.CharDamage[i]);
                    }
                }
                if (bToHitBonus)
                {
                    Global.Trim(sb);
                    sb.AppendFormat("\r\n");
                }

                bool bDamageBonus = false;
                for (int i = 0; i < Effects.CharDamage2.Length; i++)
                {
                    if (Effects.CharDamage2[i] != 0 && Effects.Names.Length > i)
                    {
                        if (!bDamageBonus)
                        {
                            bDamageBonus = true;
                            sb.Append("Damage Bonuses: ");
                        }
                        int iDamage = Effects.CharDamage2[i] * (Effects.GameState.Game == GameNames.BardsTale3 ? 1 : 2);
                        sb.AppendFormat("{0} ({1}d4), ", Effects.Names[i], (sbyte) iDamage);
                    }
                }
                if (bDamageBonus)
                {
                    Global.Trim(sb);
                    sb.AppendFormat("\r\n");
                }

                bool bACPenalties = false;
                for (int i = 0; i < Effects.MonsterAC.Length; i++)
                {
                    if (Effects.MonsterAC[i] != 0 && MonsterIndices[i] != 0)
                    {
                        if (!bACPenalties)
                        {
                            bACPenalties = true;
                            sb.Append("AC Penalties: ");
                        }
                        sb.AppendFormat("{0} ({1}), ", GetMonsterName(MonsterIndices, Living, i), (sbyte) Effects.MonsterAC[i]);
                    }
                }
                if (bACPenalties)
                {
                    Global.Trim(sb);
                    sb.AppendFormat("\r\n");
                }

                bool bDamPenalties = false;
                for (int i = 0; i < Effects.MonsterDamage.Length; i++)
                {
                    if (Effects.MonsterDamage[i] != 0 && MonsterIndices[i] != 0)
                    {
                        if (!bDamPenalties)
                        {
                            bDamPenalties = true;
                            sb.Append("Damage Penalties: ");
                        }
                        sb.AppendFormat("{0} ({1}), ", GetMonsterName(MonsterIndices, Living, i), (sbyte) Effects.MonsterDamage[i]);
                    }
                }
                if (bDamPenalties)
                {
                    Global.Trim(sb);
                    sb.AppendFormat("\r\n");
                }

                return sb.ToString();
            }
        }

        public string GetMonsterName(byte[] indices, byte[] living, int index)
        {
            if (indices == null || indices.Length <= index)
                return "Unknown";

            if (indices.Length < 5)
                return BardsTale1.MonsterName(indices[index]);

            int iOffset = index * (indices.Length / 4);
            if (indices[iOffset] == 0)
                return "Unknown";

            MonsterName name = BT3MemoryHacker.ExtractMonsterNames(indices, iOffset);
            if (living == null || living.Length <= index || living[index] == 1)
                return name.Singular;
            return name.Plural;
        }
    }

    public abstract class BTBaseCharacter : BaseCharacter
    {
        public virtual StatAndModifier BasicStrength { get { return null; } }
        public virtual StatAndModifier BasicIQ { get { return null; } }
        public virtual StatAndModifier BasicDexterity { get { return null; } }
        public virtual StatAndModifier BasicConstitution { get { return null; } }
        public virtual StatAndModifier BasicLuck { get { return null; } }
        public override bool PartyGems { get { return true; } }

        public override StatAndModifier[] PrimaryStats
        {
            get { return new StatAndModifier[] { BasicStrength, BasicIQ, BasicDexterity, BasicConstitution, BasicLuck }; }
        }

        public virtual int CurrentStrength { get { return BasicStrength.Temporary + Modifiers.Strength; } }
        public virtual int CurrentIQ { get { return BasicIQ.Temporary + Modifiers.IQ; } }
        public virtual int CurrentDexterity { get { return BasicDexterity.Temporary + Modifiers.Dexterity; } }
        public virtual int CurrentConstitution { get { return BasicConstitution.Temporary + Modifiers.Constitution; } }
        public virtual int CurrentLuck { get { return BasicLuck.Temporary + Modifiers.Luck; } }

        public override string AttributeTip(ModAttr attrib, MemoryHacker hacker)
        {
            string strBase = base.AttributeTip(attrib, hacker);
            if (!String.IsNullOrWhiteSpace(strBase))
                return strBase;

            switch (attrib)
            {
                case ModAttr.Strength: return GetModifier(CurrentStrength, PrimaryStat.Strength).TipString("{0} Strength: {1} to melee damage ({2})");
                case ModAttr.IQ: return GetModifier(CurrentIQ, PrimaryStat.IQ).TipString("{0} IQ: {1} SP per level ({2})");
                case ModAttr.Dexterity: return GetModifier(CurrentDexterity, PrimaryStat.Dexterity).TipString("{0} Dexterity: {1} to Armor Class ({2})");
                case ModAttr.Constitution: return GetModifier(CurrentConstitution, PrimaryStat.Constitution).TipString("{0} Constitution: {1} HP per level ({2})");
                //case ModAttr.Luck: return GetModifier(CurrentLuck, PrimaryStat.Luck).TipString("{0} Luck: {1} to ? ({2})");
                case ModAttr.MeleeDamage: return BasicInventory.MeleeWeaponName;
                default: return String.Empty;
            }
        }

        public static GenericClass[] BTClasses
        {
            get
            {
                return new GenericClass[] {
                    GenericClass.Warrior,
                    GenericClass.Paladin,
                    GenericClass.Rogue,
                    GenericClass.Bard,
                    GenericClass.Hunter,
                    GenericClass.Monk,
                    GenericClass.Conjurer,
                    GenericClass.Magician,
                    GenericClass.Sorcerer,
                    GenericClass.Wizard,
                    GenericClass.Archmage,
                    GenericClass.Monster};
            }
        }
    }

    public class BTStackInfo
    {
        public int Offset;
        public HashSet<int> Values;
        public MainState State;
        public bool Address;

        public BTStackInfo(int offset, MainState state, params int[] values)
        {
            Offset = offset;
            Values = new HashSet<int>(values);
            State = state;
            Address = false;
        }

        public static void AddSet(List<BTStackInfo> list, int offsetStart, MainState state, params int[] values)
        {
            list.Add(new BTStackInfo(offsetStart, state, values));
            list.Add(new BTStackInfo(offsetStart + 30, state, values));
            list.Add(new BTStackInfo(offsetStart + 52, state, values));
            list.Add(new BTStackInfo(offsetStart + 66, state, values));
            list.Add(new BTStackInfo(offsetStart + 74, state, values));
            list.Add(new BTStackInfo(offsetStart + 78, state, values));
        }

        public static BTStackInfo NewAddress(int offset, MainState state, params int[] values)
        {
            BTStackInfo si = new BTStackInfo(offset, state, values);
            si.Address = true;
            return si;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in Values)
                sb.AppendFormat("{0:X4},", i);
            Global.Trim(sb);
            if (Address)
                return String.Format("{0}: [{1}],{2}", Offset, sb.ToString(), State.ToString());
            return String.Format("{0}: {1},{2}", Offset, sb.ToString(), State.ToString());
        }
    }

    public class BTTrainingInfo : TrainingInfo
    {
        public BTGameState State;
        public override bool InTraining { get { return (State.Main == MainState.Training); } }
    }

    public class StackGuess
    {
        public int Location;
        public byte[] Value;
        public int CharacterOffset;
        public MainState State;

        public StackGuess(int iLocation, byte b1, byte b2, byte b3, byte b4, MainState state, int offset = 0)
        {
            Location = iLocation;
            Value = new byte[] { b1, b2, b3, b4 };
            State = state;
            CharacterOffset = offset;
        }
    }
}
