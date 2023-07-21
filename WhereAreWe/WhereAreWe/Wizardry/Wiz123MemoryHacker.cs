using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace WhereAreWe
{
    public class Wiz123CharCreationInfo : CharCreationInfo
    {
        public int SelectedStat = -1;
        public int SelectedRace = -1;
        public int Gold = 0;
        public int Age = 0;

        public Wiz123CharCreationInfo()
        {
            AttributesOriginal = Global.NullBytes(7);
        }

        public Wiz123CharCreationInfo(Wiz123Race race, int bonus, int selectedStat, int gold)
        {
            SelectedRace = (int) race;
            SelectedStat = selectedStat;
            Gold = gold;
            switch (race)
            {
                case Wiz123Race.Human:
                    AttributesOriginal = new byte[] { 8, 8, 5, 8, 8, 9, (byte)bonus };
                    break;
                case Wiz123Race.Elf:
                    AttributesOriginal = new byte[] { 7, 10, 10, 6, 9, 6, (byte)bonus };
                    break;
                case Wiz123Race.Dwarf:
                    AttributesOriginal = new byte[] { 10, 7, 10, 10, 5, 6, (byte)bonus };
                    break;
                case Wiz123Race.Gnome:
                    AttributesOriginal = new byte[] { 7, 7, 10, 8, 10, 7, (byte)bonus };
                    break;
                case Wiz123Race.Hobbit:
                    AttributesOriginal = new byte[] { 5, 7, 7, 6, 10, 15, (byte)bonus };
                    break;
                default:
                    AttributesOriginal = Global.NullBytes(7);
                    break;
            }
        }

        public Wiz123CharCreationInfo(int str, int iq, int pie, int vit, int agi, int luc, int bonus, int selectedStat, int selectedRace, int gold = 199)
        {
            SelectedStat = selectedStat;
            SelectedRace = selectedRace;
            Gold = gold;
            AttributesOriginal = new byte[] { (byte)str, (byte)iq, (byte)pie, (byte)vit, (byte)agi, (byte)luc, (byte)bonus };
        }

        public override bool ValidValues
        {
            get
            {
                if (AttributesOriginal == null || AttributesOriginal.Length < 7)
                    return false;

                for (int i = 0; i < 6; i++)
                    if (AttributesOriginal[i] < 3 || AttributesOriginal[i] > 18)
                        return false;

                if (AttributesOriginal[6] < 0 || AttributesOriginal[6] > 29)
                    return false;

                return true;
            }
        }

        public override bool OnCharCreation
        {
            get
            {
                return (
                    State.Main == MainState.CreateSelectClass ||
                    State.Main == MainState.CreateSelectAlignment ||
                    State.Main == MainState.CreateSelectRace ||
                    State.Main == MainState.CreateSelectSex ||
                    State.Main == MainState.CreateSelectName ||
                    State.Main == MainState.Rolling ||
                    State.Main == MainState.CreateExchangeStat ||
                    State.Main == MainState.SaveCharacter);
            }
        }
    }

    public enum Wiz123States
    {
        Done,
        Training,
        Castle,
        Gilgamesh,
        Inspect,
        Boltac,
        Cant,
        Runner,
        Combat,
        NewMaze,
        CheckForWin,
        Reward,
        Inspect2,
        Equip6,
        EquipDsp,
        ReOrder,
        Cemetery,
        Inspect3,
        BackToCamp,
        BackToRoll,
        Camp2Equip6,
        Unused,
        Reward2,
        ScanMessage,
        CampStuff,
        EdgeOfTown,
        InsArea,
        BackToCamp2
    }

    public class Wiz123TrainingInfo : TrainingInfo
    {
        public Wiz123GameState State;

        public override bool InTraining
        {
            get
            {
                switch (State.Main)
                {
                    case MainState.Training:
                    case MainState.TrainingInspectChangeClass:
                    case MainState.TrainingInspectCharSelected:
                    case MainState.TrainingInspecting:
                    case MainState.TrainingInspectRead:
                    case MainState.TrainingInspectSelectChar:
                        return true;
                    default:
                        return false;
                }
            }
        }
    }

    public class Wiz123KnownSpells : KnownSpells
    {
        private bool[] KnownSpells;

        public override int NumKnown { get { return KnownSpells.Count(s => s); } }
        public override bool IsKnown(int internalIndex, GenericClass mmClass) { return IsKnown((Wiz123SpellIndex) internalIndex); }
        public override bool IsKnown(int index, SpellType type) { return IsKnown((Wiz123SpellIndex)(index + (type == SpellType.Priest ? 21 : 0))); }

        public override KnownSpells CreateNew(GenericClass charClass) { return new Wiz123KnownSpells(new byte[] {0, 0, 0, 0, 0, 0, 0, 0}); }
        public override void SetKnown(Spell spell, bool bKnown) { SetKnown(spell as Wiz123Spell, bKnown); }

        public void SetKnown(Wiz123Spell spell, bool bKnown)
        {
            KnownSpells[(int)spell.Index] = bKnown;
        }

        public override string KnownString(GenericClass charClass)
        {
            int iMage = KnownMage;
            int iPriest = KnownPriest;
            if (iMage == 0 && iPriest == 0)
                return "None";
            if (iPriest == 0)
                return String.Format("{0} Mage", iMage);
            if (iMage == 0)
                return String.Format("{0} Priest", iPriest);
            return String.Format("{0} Mage, {1} Priest", iMage, iPriest);
        }

        public bool IsKnown(Wiz123SpellIndex spell) { return KnownSpells != null && KnownSpells.Length > (int)spell && KnownSpells[(int)spell]; }

        public Wiz123SpellPoints GetMaxSpellPoints()
        {
            Wiz123SpellPoints sp = new Wiz123SpellPoints();
            foreach (Wiz123Spell spell in Wiz123.Spells)
            {
                switch(spell.Type)
                {
                    case SpellType.Mage:
                        if (IsKnown(spell.Index))
                            sp.Mage[spell.Level]++;
                        break;
                    case SpellType.Priest:
                        if (IsKnown(spell.Index))
                            sp.Priest[spell.Level]++;
                        break;
                }
            }
            return sp;
        }

        public bool KnowsAnyHealing
        {
            get
            {
                return IsKnown(Wiz123SpellIndex.Dios) ||
                    IsKnown(Wiz123SpellIndex.Dialko) ||
                    IsKnown(Wiz123SpellIndex.Dial) ||
                    IsKnown(Wiz123SpellIndex.Latumofis) ||
                    IsKnown(Wiz123SpellIndex.Dialma) ||
                    IsKnown(Wiz123SpellIndex.Di) ||
                    IsKnown(Wiz123SpellIndex.Madi) ||
                    IsKnown(Wiz123SpellIndex.Kadorto);
            }
        }

        public Wiz123KnownSpells(byte[] bytes, int offset = 0)
        {
            RawBytes = new byte[8];
            Buffer.BlockCopy(bytes, offset, RawBytes, 0, 8);
            KnownSpells = new bool[64];
            KnownSpells[0] = false;
            for (int i = 0; i < 64; i++)
                KnownSpells[i] = Global.GetBit(RawBytes, i, true) != 0;
        }

        private byte ByteFromBits(bool[] bits, int offset)
        {
            int iResult = 0;
            for (int i = 0; i < 8; i++)
            {
                iResult |= ((bits[offset + i] ? 1 : 0) << i);
            }
            return (byte)iResult;
        }

        public override byte[] GetBytes()
        {
            byte[] bytes = new byte[8];
            for(int i = 0; i < 8; i++)
                bytes[i] = ByteFromBits(KnownSpells, i * 8);
            return bytes;
        }

        public int KnownMage { get { return Wiz123SpellList.Mage.Count(s => KnownSpells[(int)s]); } }
        public int KnownPriest { get { return Wiz123SpellList.Priest.Count(s => KnownSpells[(int)s]); } }

        public Wiz123SpellIndex NextMage
        {
            get
            {
                for (Wiz123SpellIndex spell = Wiz123SpellIndex.Halito; spell < Wiz123SpellIndex.Kalki; spell++)
                {
                    if (KnownSpells[(int)spell])
                        continue;
                    return spell;
                }
                return Wiz123SpellIndex.None;
            }
        }

        public Wiz123SpellIndex NextPriest
        {
            get
            {
                for (Wiz123SpellIndex spell = Wiz123SpellIndex.Kalki; spell < Wiz123SpellIndex.Last; spell++)
                {
                    if (KnownSpells[(int)spell])
                        continue;
                    return spell;
                }
                return Wiz123SpellIndex.None;
            }
        }

        public override string ToString()
        {
            return KnownString(GenericClass.None);
        }
    }

    public class Wiz123SpellPoints : SpellPoints
    {
        public override string Current { get { return ToString(); } }
        public override bool HasAnyCurrent { get { return Mage.Any(m => m > 0) || Priest.Any(p => p > 0); } }

        public int[] Mage;
        public int[] Priest;

        public static Wiz123SpellPoints Zero { get { return new Wiz123SpellPoints(Global.NullBytes(28), 0); } }

        public Wiz123SpellPoints()
        {
            Mage = new int[7];
            Priest = new int[7];
            for (int i = 0; i < 7; i++)
            {
                Mage[i] = 0;
                Priest[i] = 0;
            }
        }

        public Wiz123SpellPoints(byte[] bytes, int offset)
        {
            if (bytes.Length - offset < 14)
                return;

            Mage = new int[7];
            Priest = new int[7];

            for (int i = 0; i < 7; i++)
            {
                Mage[i] = BitConverter.ToInt16(bytes, offset + (i * 2));
                Priest[i] = BitConverter.ToInt16(bytes, offset + (i * 2) + 14);
            }
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[28];
            for (int i = 0; i < 7; i++)
            {
                Global.SetInt16(bytes, i * 2, Mage[i]);
                Global.SetInt16(bytes, 14 + (i * 2), Priest[i]);
            }
            return bytes;
        }

        public override string ToString()
        {
            int iMage = Mage.Sum();
            int iPriest = Priest.Sum();

            if (iMage == 0 && iPriest == 0)
                return "0";

            StringBuilder sb = new StringBuilder();
            if (iMage != 0)
            {
                sb.Append("M");
                foreach (int i in Mage)
                    sb.AppendFormat("{0}", i);
            }
            if (iPriest != 0)
            {
                if (sb.Length > 0)
                    sb.Append(" ");
                sb.Append("P");
                foreach (int i in Priest)
                    sb.AppendFormat("{0}", i);
            }
            return sb.ToString();
        }
    }

    public enum PackedStat
    {
        None = -1,
        Strength = 0,
        IQ,
        Piety,
        Vitality,
        Agility,
        Luck,
        SaveVsPoison,
        SaveVsPetrify,
        SaveVsWand,
        SaveVsBreath,
        SaveVsSpell,
        Experience,
        Gold,
        Condition
    }

    public class PackedFiveBitValues
    {
        public int[] Values;

        private static int[] BitOrder = new int[] { 24, 25, 26, 27, 28, 29, 30, 31, 16, 17, 18, 19, 20, 21, 22, 8, 9, 10, 11, 12, 13, 14, 15, 0, 1, 2, 3, 4, 5, 6 };

        public PackedFiveBitValues(byte[] stats, int offset = 0)
        {
            long statInt = (stats[offset] << 24) | (stats[offset + 1] << 16) | (stats[offset + 2] << 8) | stats[offset + 3];
            Values = new int[6];
            for (int i = 0; i < Values.Length; i++)
                Values[i] = IntFromBits(statInt, BitOrder[5 * i], BitOrder[5 * i + 1], BitOrder[5 * i + 2], BitOrder[5 * i + 3], BitOrder[5 * i + 4]);
        }

        public PackedFiveBitValues(params int[] values)
        {
            Values = values;
        }

        public byte[] Bytes { get { return GetBytes(Values); } }

        public static byte[] GetBytes(params int[] values)
        {
            long bits = 0;
            for(int i = 0; i < values.Length; i++)
                bits = SetBits(bits, values[i], BitOrder[5 * i], BitOrder[5 * i + 1], BitOrder[5 * i + 2], BitOrder[5 * i + 3], BitOrder[5 * i + 4]);
            return new byte[] { (byte)((bits >> 24) & 0xff), (byte)((bits >> 16) & 0xff), (byte)((bits >> 8) & 0xff), (byte)(bits & 0xff) };
        }

        private static long SetBits(long bytes, int iVal, params int[] bits)
        {
            for (int i = 0; i < bits.Length; i++)
            {
                if (((iVal >> i) & 1) == 1)
                    bytes |= ((uint)1 << bits[i]);
            }
            return bytes;
        }

        private int IntFromBits(long stats, params int[] bits)
        {
            long lOut = 0;
            for (int i = 0; i < bits.Length; i++)
            {
                lOut += (((stats >> bits[i]) & 1) * (1 << i));
            }
            return (int)lOut;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (int i in Values)
                sb.AppendFormat("{0}/", i);
            if (sb.Length > 1)
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }
    }

    public class WizardryLong
    {
        public long Number;

        public WizardryLong(byte[] bytes, int offset = 0)
        {
            Number = 0;
            if (bytes.Length - offset < 6)
                return;
            Number = BitConverter.ToUInt16(bytes, offset) + (10000 * (long)BitConverter.ToUInt16(bytes, offset + 2)) + (100000000 * (long)BitConverter.ToUInt16(bytes, offset + 4));
        }

        public WizardryLong(long lVal)
        {
            Number = lVal;
        }

        public byte[] Bytes { get { return GetBytes(Number); } }

        public static byte[] GetBytes(long lVal)
        {
            byte[] bytes1 = BitConverter.GetBytes((Int16)(lVal % 10000));
            byte[] bytes2 = BitConverter.GetBytes((Int16)((lVal / 10000) % 10000));
            byte[] bytes3 = BitConverter.GetBytes((Int16)((lVal / 100000000) % 10000));
            return new byte[] { bytes1[0], bytes1[1], bytes2[0], bytes2[1], bytes3[0], bytes3[1] };
        }

        public override string ToString()
        {
            return String.Format("{0}", Number);
        }

        public static implicit operator long(WizardryLong val) { return val.Number; }
    }

    public enum Wiz123Wall
    {
        Open =         0,
        SolidWall =    1,
        Door      =    2,
        HiddenDoor =   3,
        OffMap =      -1
    }

    public enum Wiz123Square
    {
        Normal = 0,
        Stairs = 1,
        Pit = 2,
        Chute = 3,
        Spinner = 4,
        Dark = 5,
        Transfer = 6,
        Ouchy = 7,
        Elevator = 8,
        SolidRock = 9,
        Fizzle = 10,
        ScenarioMessage = 11,
        Encounter = 12
    }

    public class Wiz123GameState : GameState
    {
        public GameNames WizGame = GameNames.Wizardry1;

        public override GameNames Game { get { return WizGame; } }

        public bool CastingState = false;
        public override bool Casting { get { return CastingState; } }
        public override bool ActingIsCaster { get { return true; } }
        public override Subscreen Subscreen { get { return Subscreen.InventoryMain; } set { } }

        public override bool NoActingChar
        {
            get
            {
                if (InCombat)
                    return false;

                switch (Main)
                {
                    case MainState.Adventuring:
                    case MainState.QuickRef:
                    case MainState.Question:
                    case MainState.PreCombat:
                    case MainState.Treasure:
                    case MainState.TreasureEnterTrapType:
                    case MainState.TreasureWhoWillDisarm:
                        return true;
                    default:
                        return false;
                }
            }
        }
    }

    public class Wiz123GameInfoItem : GameInfoItem
    {
        public override MapTitlePair GetMapTitlePair(int iMap) { return Wiz1MemoryHacker.GetMapTitlePair(iMap); }

        public Wiz123GameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offsets, type, mask, style, fn)
        {
        }

        public Wiz123GameInfoItem(string desc, object val, int offset, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, new OffsetList(offset), type, mask, style, fn)
        {
        }

        public Wiz123GameInfoItem(string desc, object val, OffsetList offsets, BitDescriptionDelegate fn)
            : base(desc, val, offsets, DataType.Bits, 0, ShowStyle.Editable, fn)
        {
        }
    }

    public class Wiz123BackpackBytes
    {
        public byte[,] Items;  // 8 sets of 8 bytes

        public Wiz123BackpackBytes()
        {
            Items = new byte[8,8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    Items[i,j] = 0;
        }
    }

    public class Wiz123PartyInfo : PartyInfo
    {
        public GameState State;
        public int[] Silenced = null;

        public Wiz123PartyInfo(byte[] bytes, byte numchars)
        {
            Bytes = bytes;
            NumChars = numchars;
            Positions = new byte[numchars];
            Addresses = new int[numchars];
            for (int i = 0; i < numchars; i++)
            {
                Positions[i] = (byte)i;
                Addresses[i] = i;
            }
        }

        public byte[] QuestBytes
        {
            get
            {
                if (Bytes == null)
                    return null;
                MemoryStream stream = new MemoryStream();
                for (int i = 0; i < NumChars; i++)
                {
                    stream.Write(Bytes, i * CharacterSize + Wiz123.Offsets.Awards, Wiz123.Offsets.AwardsLength);
                    stream.Write(Bytes, i * CharacterSize + Wiz123.Offsets.Inventory, Wiz123.Offsets.InventoryLength);
                }
                stream.WriteByte(ActingChar);
                return stream.ToArray();
            }
        }

        public bool CharacterHasItem(GameNames game, int iCharAddress, int item, bool bEquippedOnly = false)
        {
            if (Bytes.Length < (iCharAddress + 1) * CharacterSize)
                return false;

            Wiz123Inventory inventory = new Wiz123Inventory(game, Bytes, iCharAddress * CharacterSize + Wiz123.Offsets.Inventory);
            return inventory.HasItem(game, item, bEquippedOnly);
        }

        public bool CharacterHasItem(int iCharAddress, Wiz1ItemIndex item) { return CharacterHasItem(GameNames.Wizardry1, iCharAddress, (int)item); }
        public bool CharacterHasItem(int iCharAddress, Wiz2ItemIndex item) { return CharacterHasItem(GameNames.Wizardry2, iCharAddress, (int)item); }
        public bool CharacterHasItem(int iCharAddress, Wiz3ItemIndex item) { return CharacterHasItem(GameNames.Wizardry3, iCharAddress, (int)item); }

        public bool CurrentPartyHasItem(GameNames game, int item, bool bEquippedOnly = false)
        {
            for (int i = 0; i < Addresses.Length; i++)
            {
                if (CharacterHasItem(game, Addresses[i], item, bEquippedOnly))
                    return true;
            }
            return false;
        }

        public bool CurrentPartyHasItem(Wiz1ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry1, (int)item); }
        public bool CurrentPartyHasItem(Wiz2ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry2, (int)item); }
        public bool CurrentPartyHasItem(Wiz3ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry3, (int)item); }

        public bool CurrentPartyHasEquipped(Wiz1ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry1, (int)item, true); }
        public bool CurrentPartyHasEquipped(Wiz2ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry2, (int)item, true); }
        public bool CurrentPartyHasEquipped(Wiz3ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry3, (int)item, true); }

        public override int CharacterSize { get { return Wiz123Character.SizeInBytes; } }
    }

    public class Wiz123SpellInfo : SpellInfo
    {
        public Wiz123Spell Spell;
        public Wiz123PartyInfo Party;
        public override bool ClassLimited { get { return false; } }

        public Wiz123SpellInfo()
        {
            Spell = null;
            Party = null;
            Game = new Wiz123GameState();
            Game.Main = MainState.Unknown;
            Game.InCombat = false;
            Game.Location = LocationInformation.Empty;
            Game.ActingChar = -1;
        }
    }

    public class Wiz123MapData : WizardryMapData
    {
        public Wiz123MapData(GameNames game, int index, byte[] bytes, int offset = 0, bool bGameInfoOnly = false)
        {
            SetFromBytes(game, index, bytes, offset, bGameInfoOnly);
        }
    }

    public class Wiz123CureAllInfo : CureAllInfo
    {
        public Wiz123Condition[] Conditions;   // 6 bytes; one per character
        public Wiz123Condition CasterCondition;
        public Int16[] HitPoints;
        public Int16[] HitPointsMax;
        public Wiz123KnownSpells CasterSpells;
        public Wiz123SpellPoints CasterSpellPoints;
        public bool InCastle;

        public override bool Valid { get { return Conditions != null && Conditions.Length > 0; } }
        public override bool IsHealer { get { return CasterSpells.KnowsAnyHealing; } }
        public override bool IsIncapacitated { get { return CasterCondition >= Wiz123Condition.Asleep; } }
        public override bool MagicPermitted { get { return true; } }
        public override bool Combat { get { return !InCastle; } }
        public override string CombatString { get { return "the Maze"; } }

        public Wiz123CureAllInfo()
        {
        }
    }

    public abstract class Wiz123SearchResults : SearchResults
    {
        public int RewardIndex;

        public abstract List<Wiz123Treasure> Treasures { get; }

        public override string HeaderString { get { return "The party has found a chest!"; } }

        public override string ContentsString
        {
            get
            {
                if (IsEmpty)
                    return String.Empty;
                return Treasures[RewardIndex].MultilineDescription;
            }
        }
        public override bool IsEmpty { get { return RewardIndex < 0 || RewardIndex >= Treasures.Count; } }

        public override int CompareTo(SearchResults results)
        {
            if (!(results is Wiz123SearchResults))
                return 1;

            if (((Wiz123SearchResults)results).RewardIndex != RewardIndex)
                return 1;

            return 0;
        }
    }

    public class Wiz123MapAttributes : MapAttributes
    {
    }

    public abstract class Wiz123EncounterInfo : EncounterInfo
    {
        Dictionary<int, Monster> m_monsters;
        public List<Group> Groups;
        public Group Characters;
        public const int Size = 4 * Group.Size;
        private string m_sExtraTitleText = String.Empty;

        public override string ExtraTitleText { get { return m_sExtraTitleText; } }

        public abstract Wiz123Monster CreateMonster(int index, byte[] bytes, int offset);
        public abstract void CreateSearchResults(int iRewardIndex);

        public class Group
        {
            public enum Property { Index, Identified, Alive, Total }

            public const int Size = 8 + (Record.Size * 9) + (Wiz123Monster.Size);

            public bool Identified;
            public int NumAlive;
            public int NumEnemies;
            public int Index;
            public List<Record> Records;
            public Wiz123Monster Monster;
            public bool MonsterChanged = false;

            public Monster GetMonster(int index)
            {
                if (index >= Records.Count)
                    return null;

                Wiz123Monster monster = Monster.Clone() as Wiz123Monster;
                monster.CurrentHP = Records[index].CurrentHP;
                monster.Speed = Records[index].Initiative;
                if (monster.Speed < 0 || monster.Speed > 10)
                    monster.Speed = 0;
                monster.Accuracy = Records[index].ArmorClass;
                monster.ACModifier = Records[index].ArmorClass;
                monster.WizCondition = Records[index].Condition;
                monster.Silenced = Records[index].Silenced;
                monster.Target = Records[index].Victim;

                return monster;
            }

            public Group()
            {
                Identified = false;
                NumAlive = 0;
                NumEnemies = 0;
                Index = -1;
                Records = new List<Record>(9);
                Monster = null;
                MonsterChanged = false;
            }

            public Group(byte[] bytes, int offset = 0)
            {
                Identified = BitConverter.ToInt16(bytes, offset) != 0;
                NumAlive = BitConverter.ToInt16(bytes, offset + 2);
                NumEnemies = BitConverter.ToInt16(bytes, offset + 4);
                Index = BitConverter.ToInt16(bytes, offset + 6);
                Records = new List<Record>(9);
                for (int i = 0; i < 9; i++)
                {
                    if (i < NumAlive)
                        Records.Add(new Record(bytes, offset + 8 + (16 * i)));
                    else
                        Records.Add(Record.Default);
                }
                MonsterChanged = false;
            }

            public bool ChangeValue(int iIndex, int val)
            {
                switch ((Property)iIndex)
                {
                    case Property.Index:
                        if (Index != val)
                        {
                            Index = val;
                            return true;
                        }
                        break;
                    case Property.Identified: Identified = val != 0; break;
                    case Property.Alive: NumAlive = val; break;
                    case Property.Total: NumEnemies = val; break;
                    default: break;
                }
                return false;
            }
        }

        public class Record
        {
            public enum Property { HP, AC, Condition, Initiative, Victim, Silenced, Unknown, SpellHash }

            public const int Size = 16;

            public int Victim;
            public int SpellHash;
            public int Initiative;
            public int CurrentHP;
            public int ArmorClass;
            public int Silenced;
            public Wiz123Condition Condition;
            public int Unknown1;

            public void ChangeValue(int iIndex, int val)
            {
                switch ((Property)iIndex)
                {
                    case Property.HP: CurrentHP = val; break;
                    case Property.AC: ArmorClass = val; break;
                    case Property.Condition: Condition = (Wiz123Condition)val; break;
                    case Property.Initiative: Initiative = val; break;
                    case Property.Victim: Victim = val; break;
                    case Property.Unknown: Unknown1 = val; break;
                    case Property.Silenced: Silenced = val; break;
                    case Property.SpellHash: SpellHash = val; break;
                    default: break;
                }
            }

            public Record()
            {
                Victim = 0;
                SpellHash = -1;
                Initiative = 0;
                CurrentHP = 0;
                ArmorClass = 0;
                Silenced = 0;
                Condition = Wiz123Condition.Good;
                Unknown1 = 0;
            }

            public static Record Default
            {
                get
                {
                    Record record = new Record();
                    record.ArmorClass = 0;
                    record.Condition = Wiz123Condition.Good;
                    record.CurrentHP = 1;
                    record.Initiative = 0;
                    record.Silenced = 0;
                    record.SpellHash = -1;
                    record.Unknown1 = 1;
                    record.Victim = 1;
                    return record;
                }
            }

            public Record(byte[] bytes, int offset = 0)
            {
                if (bytes.Length - offset < 16)
                    return;

                Victim = BitConverter.ToInt16(bytes, offset);
                SpellHash = BitConverter.ToInt16(bytes, offset + 2);
                Initiative = BitConverter.ToInt16(bytes, offset + 4);
                CurrentHP = BitConverter.ToInt16(bytes, offset + 6);
                ArmorClass = -BitConverter.ToInt16(bytes, offset + 8);
                Silenced = BitConverter.ToInt16(bytes, offset + 10);
                Unknown1 = BitConverter.ToInt16(bytes, offset + 12);
                Condition = (Wiz123Condition)BitConverter.ToInt16(bytes, offset + 14);
            }
        }

        public void SetBytes(Wiz123GameState state, byte[] bytes, byte[] bytesPartyCombat, Point ptParty, int iRewardModifier, int offset = 0)
        {
            if (bytes.Length - offset < (4 * (8 + (16 * 9))))
                return;

            m_monsters = new Dictionary<int, Monster>();

            Groups = new List<Group>(4);
            int iCount = 0;
            if (!state.IsTreasure)
            {
                for (int i = 0; i < 4; i++)
                {
                    Group group = new Group(bytes, offset + (Group.Size * i));
                    group.Monster = CreateMonster(group.Index, bytes, offset + 8 + (16 * 9));
                    Groups.Add(group);

                    for (int iMonster = 0; iMonster < Groups[i].NumEnemies; iMonster++)
                    {
                        Monster monster = Groups[i].GetMonster(iMonster);
                        if (monster != null)
                        {
                            monster.Position = ptParty;
                            monster.MonsterGroup = i;
                            monster.MonsterSubGroup = iMonster;
                            monster.EncounterIndex = iCount;
                            monster.RewardModifier = iRewardModifier;
                            if (iMonster >= Groups[i].NumAlive || monster.CurrentHP == 0 || monster.Condition.HasFlag(BasicConditionFlags.Dead))
                                monster.Killed = true;
                            Groups[i].Monster = monster as Wiz123Monster;
                            m_monsters.Add(iCount, monster);
                            iCount++;
                        }
                    }
                }
            }

            Characters = new Group();
            for (int i = 0; i < 6; i++)
            {
                if (i * 16 + 15 > bytesPartyCombat.Length)
                    break;
                Characters.Records.Add(new Record(bytesPartyCombat, i * 16));
            }
            Characters.NumAlive = Characters.Records.Count;
            Characters.NumEnemies = Characters.Records.Count;
            Characters.Identified = true;

            NumTotalMonsters = iCount;

            switch (iRewardModifier)
            {
                case 1:
                    m_sExtraTitleText = "(double gold!)";
                    break;
                case 2:
                    m_sExtraTitleText = "(extra treasure!)";
                    break;
            }
        }

        public byte[] GetCharBytes()
        {
            MemoryStream ms = new MemoryStream();
            foreach (Record record in Characters.Records)
            {
                Global.WriteInt16(ms, record.Victim);
                Global.WriteInt16(ms, record.SpellHash);
                Global.WriteInt16(ms, record.Initiative);
                Global.WriteInt16(ms, record.CurrentHP);
                Global.WriteInt16(ms, -record.ArmorClass);
                Global.WriteInt16(ms, record.Silenced);
                Global.WriteInt16(ms, record.Unknown1);
                Global.WriteInt16(ms, (int)record.Condition);
            }
            return ms.ToArray();
        }

        public byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            foreach (Group group in Groups)
            {
                Global.WriteInt16(ms, group.Identified ? 1 : 0);
                Global.WriteInt16(ms, group.NumAlive);
                Global.WriteInt16(ms, group.NumEnemies);
                Global.WriteInt16(ms, (int) group.Index);

                foreach (Record record in group.Records)
                {
                    Global.WriteInt16(ms, record.Victim);
                    Global.WriteInt16(ms, record.SpellHash);
                    Global.WriteInt16(ms, record.Initiative);
                    Global.WriteInt16(ms, record.CurrentHP);
                    Global.WriteInt16(ms, -record.ArmorClass);
                    Global.WriteInt16(ms, record.Silenced);
                    Global.WriteInt16(ms, record.Unknown1);
                    Global.WriteInt16(ms, (int)record.Condition);
                }

                byte[] bytesMonster = group.Monster.GetBytes();
                ms.Write(bytesMonster, 0, bytesMonster.Length);
            }
            return ms.ToArray();
        }

        public override Dictionary<int, Monster> Monsters { get { return m_monsters; } set { m_monsters = value; } }
        public override bool HasTreasure { get { return SearchResults != null && !SearchResults.IsEmpty; } }
        public override bool InCombat { get { return NumTotalMonsters > 0; } }

        public override TurnOrderCalculator GetTurnOrder(CharCombatLabel[] labelChars, GameInfo gameInfo)
        {
            // Wizardry determines initiative randomly after commands are committed,
            // so we can't reliably determine who will be in which order beforehand
            TurnOrderCalculator toc = new TurnOrderCalculator(0, 0);
            int iNameMax = 0;

            Wiz123Character[] characters = new Wiz123Character[Party.Bytes.Length / Party.CharacterSize];
            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
            {
                characters[iIndex] = Wiz123Character.Create(gameInfo.Game, 0, Party.Bytes, iIndex * Wiz123Character.SizeInBytes, null);
                labelChars[iIndex].Melee = iIndex < 3;
                labelChars[iIndex].Condition = characters[iIndex].BasicCondition;
                labelChars[iIndex].CharName = String.Format("{0})  {1}", iIndex + 1, characters[iIndex].CharName);
                labelChars[iIndex].HP = characters[iIndex].HitPoints.Current.ToString();
                labelChars[iIndex].SP = characters[iIndex].SpellPoints.Current.ToString();

                iNameMax = Math.Max(iNameMax, labelChars[iIndex].NameLength);
            }
            for (byte iIndex = Party.NumChars; iIndex < 8; iIndex++)
                labelChars[iIndex].Clear();

            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
                labelChars[iIndex].SetHPOffset(iNameMax + 2);

            return toc;
        }
    }

    public class Wiz123ActiveSquares : ActiveSquares
    {
        public Wiz123ActiveSquares(MainForm main, int mapIndex, byte[] bytesFightMap)
        {
            Main = main;
            m_iMapIndex = mapIndex;
            RawBytes = bytesFightMap;
            m_bInitialized = false;
        }

        public override bool IsActive(int x, int y, bool bEncountersOnly)
        {
            if (x < 0 || x > 20 || y < 0 || y > 20)
                return false;
            int offset = x * 4 + y / 8;
            if (offset < 0 || offset >= RawBytes.Length)
                return false;
            bool bActive = Global.IsBitSet(RawBytes[offset], y % 8);
            if (!bActive)
                return false;
            if (!bEncountersOnly)
                return true;

            return true;
            //MMEncounterData ed = GlobalEncounterData;
            //if (ed == null)
            //    return true;

            //return ed.IsMonsterEncounter(m_iMapIndex, new Point(x, y));
        }

        protected override void Initialize()
        {
            m_activeSquares = new Dictionary<Point, ActiveSquareInfo>();

            if (RawBytes == null || RawBytes.Length < 80)
                return;

            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 20; x++)
                {
                    Point pt = new Point(x, y);
                    m_activeSquares.Add(pt, new ActiveSquareInfo(pt, Global.IsBitSet(RawBytes[x * 4 + y / 8], y % 8)));
                }
            }

            m_bInitialized = true;
        }
   }

    public abstract class Wiz123MemoryHacker : WizardryMemoryHacker
    {
        protected abstract WizMemory Memory { get; }

        public override byte[] MainSearch { get { return Memory.MainSearch; } }
        public override MemoryGuess[] Guesses { get { return Memory.Guesses; } }

        protected Wiz123RosterFile m_wiz123Roster = null;

        protected Wiz123EncounterInfo m_lastEncounterInfo = null;

        public override Size GetCurrentMapDimensions() { return new Size(20, 20); }

        public override string BagOfHoldingRequirement { get { return "in the Castle"; } }

        public override int MaxInventoryChar { get { return 20; } }
        public override int MaxBackpackSize { get { return 8; } }

        public override CreationAssistantControl CreateCreationAssistantControl(IMain main) { return new Wiz123CreationAssistantControl(main); }
        public override TrainingAssistantControl CreateTrainingAssistantControl(IMain main) { return new Wiz123TrainingAssistantControl(main); }

        public override string GetRaceDescription(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return "46 stat points, 5% resistance to Poison and Paralysis";
                case GenericRace.Elf: return "48 stat points, 10% resistance to Rod/Staff/Wand (unused)";
                case GenericRace.Dwarf: return "48 stat points, 20% resistance to Gas/Breath Weapons";
                case GenericRace.Gnome: return "49 stat points, 10% resistance to Petrification";
                case GenericRace.Hobbit: return "50 stat points, 15% resistance to Spells";
                default: return "Unknown";
            }
        }

        public override string GetClassDescription(GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Fighter: return "1d10 HP/lev, 15% resistance to Poison/Paralysis";
                case GenericClass.Mage: return "1d4 HP/lev, 15% resistance to Spells";
                case GenericClass.Priest: return "1d8 HP/lev, 15% resistance to Petrification";
                case GenericClass.Thief: return "1d6 HP/lev, 15% resistance to Gas/Breath Weapons";
                case GenericClass.Bishop: return "1d6 HP/lev, 10% resistance to Petrify, Gas/Breath, and Spells";
                case GenericClass.Samurai: return "1d8 HP/lev, 10% resistance to Poison/Paralysis and Spells";
                case GenericClass.Lord: return "1d10 HP/lev, 10% resistance to Poison/Paralysis and Petrify";
                case GenericClass.Ninja: return "1d6 HP/lev, 10% Petrify/Spells, 15% Poison/Par/Gas/Breath"; // Also 20% to Rod/Staff/Wand but that seems to be unused in the game
                default: return "Unknown";
            }
        }

        public override StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            return Wiz123Character.GetStatModifier(value, stat);
        }

        public override string StatToolTip(int iIndex, int iValue)
        {
            switch (iIndex)
            {
                case 0: return String.Format("Strength gives a bonus chance to hit ({0}: {1}%) and damage per swing ({0}: {2}).\r\n11\tMinimum for Fighter\r\n15\tMinimum for Samurai, Lord\r\n17\tMinimum for Ninja", iValue,
                    Global.AddPlus(GetStatModifier(iValue, PrimaryStat.Strength).Value * 5), GetStatModString(iValue, PrimaryStat.Strength));
                case 1: return String.Format("I.Q. determines the chance ({0}: {1}%) to learn Mage spells when leveling\r\n11\tMinimum for Mage, Samurai\r\n12\tMinimum for Bishop, Lord\r\n17\tMinimum for Ninja", iValue, GetStatModifier(iValue, PrimaryStat.IQ).Value);
                case 2: return String.Format("Piety determines the chance ({0}: {1}%) to learn Priest spells when leveling\r\n11\tMinimum for Priest\r\n12\tMinimum for Bishop, Lord\r\n17\tMinimum for Ninja", iValue, GetStatModifier(iValue, PrimaryStat.Piety).Value);
                case 3: return String.Format("Vitality gives a bonus ({0}: {1}) to the number of hit points gained per level\r\n14\tMinimum for Samurai\r\n15\tMinimum for Lord\r\n17\tMinimum for Ninja", iValue, GetStatModString(iValue, PrimaryStat.Vitality));
                case 4: return String.Format("Agility gives a bonus ({0}: {1}) to combat initiative and to the chance of identifying a trap (Thieves multiply this by 6 and Ninjas by 4)\r\n10\tMinimum for Samurai\r\n11\tMinimum for Thief\r\n14\tMinimum for Lord\r\n17\tMinimum for Ninja", iValue, GetStatModString(iValue, PrimaryStat.Agility));
                case 5: return String.Format("Luck gives a bonus ({0}: {1}%) to all resistances\r\n15\tMinimum for Lord\r\n17\tMinimum for Ninja", iValue, Global.AddPlus(GetStatModifier(iValue, PrimaryStat.Luck).Value * -5));
            }
            return "";
        }

        public override RosterFile CurrentRoster { get { return m_wiz123Roster; } }

        public override bool ValidateRosterFile()
        {
            // Always reload the roster file, in case the player deleted characters or otherwise putzed with the file
            m_wiz123Roster = CreateRoster(true) as Wiz123RosterFile;
            if (!m_wiz123Roster.Valid)
                BrowseRosterFile();

            return m_wiz123Roster.Valid;
        }

        protected override int FindNextInventoryChar(int iStart, InventoryCharAction action)
        {
            if (!ValidateRosterFile())
                return -1;

            byte[] bytes = new byte[Wiz123Character.SizeInBytes];
            while (iStart >= 0)
            {
                Wiz123Character wizChar = null;
                if (iStart < m_wiz123Roster.Chars.Count)
                    wizChar = Wiz123Character.Create(Game, 0, m_wiz123Roster.Chars[iStart].Bytes, 0, null);

                switch (action)
                {
                    case InventoryCharAction.FindExisting:
                        if (wizChar != null && wizChar.Name == "INVENTORY" && wizChar.Condition != Wiz123Condition.Lost && wizChar.Inventory.Items.Count > 0)
                        {
                            m_wiz123Roster.SetGoodCondition(iStart);
                            return iStart;
                        }
                        break;
                    case InventoryCharAction.FindOrCreate:
                        if (wizChar != null && wizChar.Name == "INVENTORY")
                        {
                            m_wiz123Roster.SetGoodCondition(iStart);
                            return iStart;
                        }
                        if (wizChar == null || String.IsNullOrWhiteSpace(wizChar.Name) || wizChar.Condition == Wiz123Condition.Lost)
                        {
                            // No character in the roster at this position; make a new one;
                            m_wiz123Roster.Chars[iStart].Bytes = Properties.Resources.Wizardry_1_Inventory_Char;
                            m_wiz123Roster.SaveCharBytes(iStart);
                            return iStart;
                        }
                        break;
                    case InventoryCharAction.FindPotential:
                        if (wizChar == null || String.IsNullOrWhiteSpace(wizChar.Name))
                            return iStart;
                        if (wizChar.Condition == Wiz123Condition.Lost || wizChar.Name == "INVENTORY")
                            return iStart;
                        break;
                    default:
                        break;
                }
                iStart--;
            }

            return -1;
        }

        public override SetBackpackResult SetBackpackInRoster(int iRosterPosition, List<Item> items)
        {
            // Completely overwrites a backpack, including equipped items
            // Also removes the "equipped" status of all items

            if (iRosterPosition < 0 || iRosterPosition > 19)
                return SetBackpackResult.InvalidPosition;

            if (!ValidateRosterFile())
                return SetBackpackResult.InvalidFile;

            if (iRosterPosition >= m_wiz123Roster.Chars.Count)
                return SetBackpackResult.InvalidPosition;

            Wiz123Inventory inv = new Wiz123Inventory(items);
            foreach (Wiz123Item item in inv.Items)
            {
                if (item == null)
                    continue;
                item.Equipped = false;
                item.Cursed = false;
            }

            byte[] bytesChar = m_wiz123Roster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return SetBackpackResult.LoadCharFailure;

            Buffer.BlockCopy(inv.GetBytes(), 0, bytesChar, Wiz123.Offsets.Inventory, Wiz123.Offsets.InventoryLength);
            m_wiz123Roster.SaveCharBytes(iRosterPosition, 0, bytesChar);

            return SetBackpackResult.Success;
        }

        public override List<Item> GetBackpackFromRoster(int iRosterPosition)
        {
            if (iRosterPosition < 0 || iRosterPosition > 19)
                return null;

            if (!ValidateRosterFile())
                return null;

            if (iRosterPosition >= m_wiz123Roster.Chars.Count)
                return null;

            byte[] bytesChar = m_wiz123Roster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return null;

            Wiz123Inventory inv = new Wiz123Inventory(Game, bytesChar, Wiz123.Offsets.Inventory);

            return inv.SelectUnequippedItems;
        }

        public override CureAllResult CureAll(CureAllInfo cureAllInfo)
        {
            bool bUnknownSpells = false;

            if (!(cureAllInfo is Wiz123CureAllInfo))
                return CureAllResult.Error;

            Wiz123CureAllInfo info = cureAllInfo as Wiz123CureAllInfo;

            // Okay, let's start curing!  Since Wizardry 1 cure-all requires being in the Castle,
            // we don't need to check spell points, just whether the spell is known (entering the Maze restores
            // all of your SP anyway, at least on the DOS version).
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                if (info.Conditions[i] >= Wiz123Condition.Dead)
                    continue;   // Don't deal with death and eradication; make the player do that manually

                if (info.Conditions[i] == Wiz123Condition.Petrified)
                {
                    if (info.CasterSpells.IsKnown(Wiz123SpellIndex.Madi))
                        info.Conditions[i] = Wiz123Condition.Good;
                    else
                        bUnknownSpells = true;
                }
                if (info.Conditions[i] == Wiz123Condition.Paralyzed || info.Conditions[i] == Wiz123Condition.Asleep)
                {
                    if (info.CasterSpells.IsKnown(Wiz123SpellIndex.Madi) || info.CasterSpells.IsKnown(Wiz123SpellIndex.Dialko))
                        info.Conditions[i] = Wiz123Condition.Good;
                    else
                        bUnknownSpells = true;
                }
            }

            // Restore all HP if the caster knows any HP restoring spells at all
            if (Properties.Settings.Default.CureAllHPWithConditions)
            {
                for (int i = 0; i < info.HitPoints.Length; i++)
                {
                    if (info.HitPoints[i] < info.HitPointsMax[i])
                    {
                        if (info.CasterSpells.IsKnown(Wiz123SpellIndex.Dios) ||
                            info.CasterSpells.IsKnown(Wiz123SpellIndex.Dial) ||
                            info.CasterSpells.IsKnown(Wiz123SpellIndex.Dialma) ||
                            info.CasterSpells.IsKnown(Wiz123SpellIndex.Madi)
                            )
                        {
                            info.HitPoints[i] = info.HitPointsMax[i];
                        }
                        else
                            bUnknownSpells = true;
                    }
                }
            }

            if (bUnknownSpells)
                return CureAllResult.SpellNotKnown;
            return CureAllResult.Success;
        }

        public override TrapsControl GetTrapsControl(IMain main) { return new Wiz123TrapsControl(main); }

        public override DirectionsTo GetDirections(int iMap, Point ptLocation, bool bNorthIncreases = true, bool bEastIncreases = true)
        {
            LocationInformation li = GetLocation();
            int iEast = ptLocation.X - li.PrimaryCoordinates.X;
            int iNorth = ptLocation.Y - li.PrimaryCoordinates.Y;

            if (!bNorthIncreases)
                iNorth = -iNorth;
            if (!bEastIncreases)
                iEast = -iEast;

            if (li.MapIndex == iMap)
            {
                // Same map -> simple calculation
                return new DirectionsTo(iNorth, iEast);
            }

            if (li.MapIndex >= 1 && li.MapIndex <= 10)
                return new DirectionsTo(iNorth, iEast, li.MapIndex - iMap);

            return DirectionsTo.Impossible;
        }

        protected virtual MainState GetMainState(int state1, int state2, int state3, int state4, int state5) { return MainState.Unknown; }

        public override GameState GetGameState() { return ReadWiz123GameState(); }

        private Wiz123GameState ReadWiz123GameState()
        {
            if (m_gsCurrent != null && m_gsCurrent.ReadTime.AddMilliseconds(50) > DateTime.Now)
                return m_gsCurrent as Wiz123GameState;     // Don't spam the game state from different windows

            Wiz123GameState state = new Wiz123GameState();

            state.WizGame = Game;
            state.Location = GetLocationForce();

            state.Main = GetMainState(ReadUInt16(Memory.State1),
                ReadUInt16(Memory.State2),
                ReadUInt16(Memory.State3),
                ReadUInt16(Memory.State4),
                ReadUInt16(Memory.State5));
            int iActing = GetInspectingChar(state.Main);
            state.ActingChar = -1;
            state.ActingCaster = -1;
            state.ActingCombatChar = ReadByte(Memory.CombatOptionActiveChar);
            state.CastingState = false;
            state.InShop = false;
            state.InCombat = false;
            state.Inspecting = false;

            switch (state.Main)
            {
                case MainState.CampInspecting:
                case MainState.TavernInspect:
                case MainState.TavernInspectRead:
                case MainState.CampInspectingCastDropUse:
                case MainState.CampInspectingRead:
                case MainState.TavernInspectTrade:
                case MainState.TrainingInspectChangeClass:
                case MainState.TrainingInspectCharSelected:
                case MainState.TrainingInspecting:
                case MainState.TrainingInspectRead:
                case MainState.TrainingInspectSelectChar:
                case MainState.Inn:
                    state.ActingChar = iActing;
                    state.Inspecting = true;
                    break;
                case MainState.CastSelectSpell:
                    state.ActingChar = iActing;
                    state.ActingCaster = iActing;
                    state.Inspecting = true;
                    state.CastingState = true;
                    break;
                case MainState.Combat:
                case MainState.CombatOptions:
                case MainState.CombatSelectFightTarget:
                case MainState.CombatConfirmRound:
                case MainState.CombatFriendly:
                    state.InCombat = true;
                    break;
                case MainState.CombatSelectSpell:
                    state.ActingChar = state.ActingCombatChar;
                    state.ActingCaster = state.ActingCombatChar;
                    state.InCombat = true;
                    state.CastingState = true;
                    break;
            }

            if (state.IsTreasure)
                state.InCombat = true;  // To leave the encounter window open

            m_gsCurrent = state;
            return state;
        }

        protected int GetInspectingChar(MainState state = MainState.Camp)
        {
            switch (state)
            {
                case MainState.Inn:
                    return ReadByte(Memory.TrainingChar);
                case MainState.Shop:
                    return ReadByte(Memory.ShoppingChar);
                case MainState.Castle:
                    return -1;
                case MainState.CombatOptions:
                    return ReadByte(Memory.CombatOptionActiveChar);
                default:
                    int i1 = ReadByte(Memory.InspectingChar);
                    int i2 = ReadByte(Memory.InspectingChar2);
                    int numChars = ReadByte(Memory.NumChars);
                    if (i2 > numChars)
                        return i1;
                    else if (i1 > numChars)
                        return i2;
                    return i2;
            }
        }

        private Wiz123PartyInfo ReadWiz123PartyInfo()
        {
            byte numChars = ReadByte(Memory.NumChars);
            if (numChars > 6)
                numChars = 6;
            if (m_block == null)
                return null;
            if (numChars == 0)
                return new Wiz123PartyInfo(new byte[0], 0);

            MemoryBytes bytes = ReadOffset(Memory.PartyInfo, Wiz123Character.SizeInBytes * numChars);
            Wiz123PartyInfo info = new Wiz123PartyInfo(bytes, numChars);

            info.State = GetGameState() as Wiz123GameState;
            info.ActingChar = (byte)GetInspectingChar(info.State.Main);
            info.ActingCaster = info.ActingChar;

            return info;
        }

        public override PartyInfo GetPartyInfo()
        {
            if (!IsValid)
                return null;

            return ReadWiz123PartyInfo();
        }

        public override Point GetPartyPosition()
        {
            if (!IsValid)
                return Global.NullPoint;

            MemoryBytes pos = ReadOffset(Memory.LocationNorth, 4);
            if (pos == null)
                return Global.NullPoint;
            return new Point(pos.Bytes[2], pos.Bytes[0]);
        }

        public static bool IsCastle(int iMap) { return iMap == 0 || iMap == 255; }

        protected LocationInformation GetLocationForce()
        {
            if (!IsValid)
                return LocationInformation.Empty;

            LocationInformation info = new LocationInformation(GetPartyPosition());
            switch (ReadByte(Memory.Facing))
            {
                case 0:
                    info.Facing = Direction.Up;
                    break;
                case 1:
                    info.Facing = Direction.Right;
                    break;
                case 2:
                    info.Facing = Direction.Down;
                    break;
                case 3:
                    info.Facing = Direction.Left;
                    break;
                default:
                    break;
            }
            info.MapIndex = GetCurrentMapIndex();

            if (info.MapIndex < 1 || info.MapIndex > 10)
                info.PrimaryCoordinates = new Point(0, 0);

            info.CanUseBag = IsCastle(info.MapIndex) || Global.Cheats;

            info.NumChars = ReadByte(Memory.NumChars);
            return info;
        }

        public override bool SetLocation(Point ptLocation)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[4] { (byte)ptLocation.Y, 0, (byte)ptLocation.X, 0 };
            return WriteOffset(Memory.LocationNorth, bytes);
        }

        public override int GetCurrentMapIndex()
        {
            return ReadByte(Memory.LocationDown);
        }

        public override MapData GetMapData(bool bIncludeStrings, int iMapIndex)
        {
            MemoryBytes bytes = ReadOffset(Memory.Map, 1024);
            if (bytes == null)
                return null;

            return new Wiz123MapData(Game, GetCurrentMapIndex(), bytes.Bytes);
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            return WriteOffset(Memory.PartyInfo + (iAddress * Wiz123Character.SizeInBytes), bytes, Math.Min(Wiz123Character.SizeInBytes, bytes.Length));
        }

        public override SpellInfo GetSpellInfo()
        {
            if (!IsValid)
                return null;

            Wiz123SpellInfo info = new Wiz123SpellInfo();
            IntPtr pRead = IntPtr.Zero;
            info.Game = GetGameState() as Wiz123GameState;

            // set info.Spell somehow
            if (!info.Game.Casting)
                return info;

            info.Party = ReadWiz123PartyInfo();

            if (info.Game.ActingChar == -1)
                info.Game.ActingChar = info.Party.ActingChar;

            if (info.Game.ActingCaster == -1)
            {
                if (info.Game.InCombat)
                    info.Game.ActingCaster = info.Party.ActingCombatChar;
                else
                    info.Game.ActingCaster = info.Party.ActingCaster;
            }
            return info;
        }

        public override SetBackpackResult SetBackpack(int iCharAddress, List<Item> items, bool bRemoveEquipped = false)
        {
            if (!IsValid)
                return SetBackpackResult.InvalidHacker;

            if (iCharAddress < 0)
                return SetBackpackResult.InvalidPosition;

            if (items == null || (items.Count > 0 && !(items[0] is Wiz123Item)))
                return SetBackpackResult.InvalidItems;

            Wiz123Inventory inv = new Wiz123Inventory(Game, ReadOffset(Memory.PartyInfo + (iCharAddress * Wiz123Character.SizeInBytes) + 58, 66).Bytes);

            List<Item> listNew = bRemoveEquipped ? new List<Item>() : inv.SelectEquippedItems;
            foreach (Item item in items)
            {
                if (listNew.Count < 8)
                    listNew.Add(item);
                else
                    return SetBackpackResult.InsufficientSpace;
            }

            for (int i = 0; i < listNew.Count; i++)
                listNew[0].MemoryIndex = i;

            byte[] bytes = new Wiz123Inventory(listNew).GetBytes();
            WriteOffset(Memory.PartyInfo + (iCharAddress * Wiz123Character.SizeInBytes) + 58, bytes);

            return SetBackpackResult.Success;
        }

        public override List<Item> GetBackpack(int iCharAddress)
        {
            List<Item> list = new List<Item>();

            if (!IsValid || iCharAddress < 0)
                return list;

            byte[] bytes = ReadOffset(Memory.PartyInfo + (iCharAddress * Wiz123Character.SizeInBytes) + 58, 66).Bytes;
            Wiz123Inventory inv = new Wiz123Inventory(Game, bytes);

            return inv.SelectUnequippedItems;
        }

        public abstract List<Wiz123Item> WizItems { get; }

        public override void RandomizeBackpack(BaseCharacter baseChar, ItemType type, bool bUsableOnly, bool bSingleModifierOnly)
        {
            Wiz123Character wizChar = baseChar as Wiz123Character;
            if (!IsValid || wizChar == null)
                return;

            // Wizardry stores equipped/unequipped in the same list, so we can't just completely randomize the inventory
            List<Item> equipped = wizChar.Inventory.SelectEquippedItems;

            // Add random items to fill up or replace the unequipped spaces in the backpack
            List<Item> items = new List<Item>(MaxBackpackSize - equipped.Count);

            int iMemIndex = 0;
            while (items.Count + equipped.Count < MaxBackpackSize)
            {
                while (equipped.Any(i => i.MemoryIndex == iMemIndex))
                    iMemIndex++;    // Skip the equipped items
                Wiz123Item item = Wiz123Item.CreateRandom(WizItems, type, bUsableOnly ? wizChar : null);
                item.Identified = true;
                items.Add(item);
            }

            SetBackpack(baseChar.BasicAddress, items);
        }

        protected virtual List<Item> GetSuperItems(Wiz123Class wizClass, Wiz123Alignment alignment) { return new List<Item>(0); }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            int offset = iAddress * Wiz123Character.SizeInBytes;
            CharacterOffsets offsets = Wiz123.Offsets;

            PartyInfo info = GetPartyInfo();
            if (offset + Wiz123Character.SizeInBytes > info.Bytes.Length + 1)
                return false;

            Wiz123Class wizClass = (Wiz123Class)info.Bytes[offset + offsets.Class];

            byte[] bytes = new PackedFiveBitValues(18, 18, 18, 18, 18, 18).Bytes;   // Stats
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Stats, bytes.Length);
            bytes = new PackedFiveBitValues(0, 0, 0, 0, 0).Bytes;   // Saving throws
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.SavingThrows, bytes.Length);
            info.Bytes[offset + offsets.Condition] = (byte)Wiz123Condition.Good;
            Global.SetInt16(info.Bytes, offset + offsets.Age, 14 * 52);
            Global.SetInt16(info.Bytes, offset + offsets.Level, 99);
            Global.SetInt16(info.Bytes, offset + offsets.LevelMod, 99);
            Global.SetInt16(info.Bytes, offset + offsets.CurrentHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.MaxHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.ArmorClass, -10);
            Global.SetInt16(info.Bytes, offset + offsets.LastArmorClass, -10);
            bytes = WizardryLong.GetBytes(99999999999);
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Gold, bytes.Length);
            bytes = WizardryLong.GetBytes(Wiz123Character.XPForLevel(wizClass, 99));
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Experience, bytes.Length);
            bytes = new byte[] { 0xFE, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x07, 0x00 };
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Spells, bytes.Length);
            bytes = new byte[28];
            for (int i = 0; i < 28; i += 2)
            {
                bytes[i] = 9;
                bytes[i + 1] = 0;
            }
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.CurrentSP, bytes.Length);

            WriteOffset(Memory.PartyInfo, info.Bytes);

            List<Item> items = GetSuperItems(wizClass, (Wiz123Alignment)info.Bytes[offset + offsets.Alignment]);

            foreach (Wiz123Item item in items)
                item.Identified = true;

            SetBackpack(iAddress, items, true);

            return true;
        }

        public override Shops GetShopInfo()
        {
            if (!IsValid)
                return null;

            Wiz123GameState state = GetGameState() as Wiz123GameState;
            Shops shops = new Shops();
            shops.InShop = state.Main == MainState.Shop;

            // There is only one shop in Wizardry 1, and the stock is constant
            shops.RawBytes = new byte[0];
            shops.Inventories = new List<ShopInventory>(1);

            Wiz1ShopInventory inv = new Wiz1ShopInventory();
            inv.Town = "Boltac's Trading Post";
            shops.CurrentDisplay = null;
            shops.Inventories.Add(inv);

            return shops;
        }

        protected abstract Wiz123EncounterInfo CreateEncounterInfo(Wiz123GameState state, byte[] bytesEncounter, byte[] bytesPartyCombat,
            Point ptPartyPosition, int iRewardModifier, int offset = 0);

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            Wiz123GameState state = GetGameState() as Wiz123GameState;
            if (!state.InCombat)
                return null;

            byte[] bytesEncounter = ReadOffset(Memory.EncounterInfo, Wiz123EncounterInfo.Size).Bytes;
            byte[] bytesPartyCombat = ReadOffset(Memory.CombatCharInfo, 16 * ReadByte(Memory.NumChars)).Bytes;
            byte[] bytesMod = ReadOffset(Memory.EncounterRewardModifier, 2).Bytes;
            Wiz123PartyInfo party = ReadWiz123PartyInfo();
            byte[] bytesReward = ReadOffset(Memory.RewardIndex, 1).Bytes;
            byte[] bytes = Global.Combine(bytesEncounter, party.Bytes, bytesPartyCombat, bytesMod, bytesReward);
            if (!bForceNew && m_lastEncounterInfo != null && Global.CompareBytes(bytes, m_lastEncounterInfo.AllBytes))
                return m_lastEncounterInfo;

            m_lastEncounterInfo = CreateEncounterInfo(state, bytesEncounter, bytesPartyCombat, GetPartyPosition(), bytesMod[0]);
            m_lastEncounterInfo.Party = party;
            m_lastEncounterInfo.AllBytes = bytes;
            m_lastEncounterInfo.CreateSearchResults(bytesReward[0]);
            return m_lastEncounterInfo;
        }

        public override bool SetEncounterInfo(EncounterInfo info)
        {
            if (!(info is Wiz123EncounterInfo) || !IsValid)
                return false;

            Wiz123EncounterInfo wi = info as Wiz123EncounterInfo;

            WriteOffset(Memory.CombatCharInfo, wi.GetCharBytes());
            return WriteOffset(Memory.EncounterInfo, wi.GetBytes());
        }

        public override bool SetMonster(Monster monster)
        {
            return SetMonsterInfo(monster as Wiz123Monster);
        }

        public virtual bool InitExternalMonsterList() { return false;  }

        public bool SetMonsterInfo(Wiz123Monster monster)
        {
            if (!IsValid || monster == null)
                return false;

            // The wizardry 1 items are stored in 22-item sections, padded out to 1024 bytes each
            int iOffset = Memory.MonsterListDisk + ((monster.Index / 22) * 1024) + ((monster.Index % 22) * Wiz123Monster.Size);

            if (WriteOffset(iOffset, monster.GetBytes()))
            {
                InitExternalMonsterList();
                return true;
            }

            return false;
        }

        public override ActiveSquares GetActiveSquares(MainForm form)
        {
            if (!IsValid)
                return null;

            MemoryBytes fights = ReadOffset(Memory.FightMap, 80);
            if (fights == null)
                return null;

            return new Wiz123ActiveSquares(form, GetCurrentMapIndex(), fights);
        }

        public override bool KillAllMonsters()
        {
            if (!IsValid)
                return false;

            // Remove all of the Fight bits from this map
            return WriteOffset(Memory.FightMap, Global.NullBytes(80));
        }

        public virtual List<bool[,]> GetFights() { return new List<bool[,]>(); }

        public override bool ResetMonsters()
        {
            List<bool[,]> listFights = GetFights();

            if (!IsValid)
                return false;

            // Set all of the appropriate Fight bits for this map
            int iMap = GetCurrentMapIndex();
            if (iMap < 1 || iMap > listFights.Count)
                return false;


            bool[,] fights = listFights[iMap - 1];
            byte[] bytesFights = new byte[80];
            int width = fights.GetLength(0);
            int height = fights.GetLength(1);
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y += 8)
                {
                    int b = 0;
                    for (int bit = 0; bit < 8; bit++)
                    {
                        if (y + bit >= height)
                            continue;
                        b |= fights[x, y + bit] ? (1 << bit) : 0;
                    }
                    bytesFights[x * 4 + y / 8] = (byte)b;
                }
            }

            return WriteOffset(Memory.FightMap, bytesFights);
        }

        protected virtual Wiz123GameInfo CreateGameInfo() { return null; }

        public override GameInfo GetGameInfo()
        {
            if (!IsValid)
                return null;

            Wiz123GameInfo info = CreateGameInfo();
            Wiz123GameState state = GetGameState() as Wiz123GameState;

            info.Location = GetLocation();

            MemoryStream stream = new MemoryStream();

            info.InCombat = state.InCombat;

            MemoryBytes mbMap = ReadOffset(Memory.Map, 1024);

            if (mbMap == null)
                return null;

            stream.Write(mbMap.Bytes, 0, mbMap.Length);

            info.Location = GetLocation();
            if (info.Location == null)
                return null;
            byte[] locationBytes = info.Location.GetBytes();
            stream.Write(locationBytes, 0, locationBytes.Length);
            stream.WriteByte((byte)(info.InCombat ? 1 : 0));

            info.TrapType = ReadInt16(Memory.TrapType);
            info.TrapType2 = ReadInt16(Memory.TrapType2);
            info.ACBonus = ReadInt16(Memory.ACBonus);
            info.Light = ReadInt16(Memory.Light);
            info.TimeDelay = ReadInt16(Memory.TimeDelay);
            info.Identify = ReadInt16(Memory.Identify);
            Global.WriteInt16(stream, info.TrapType);
            Global.WriteInt16(stream, info.TrapType2);
            Global.WriteInt16(stream, info.ACBonus);
            Global.WriteInt16(stream, info.Light);
            Global.WriteInt16(stream, info.TimeDelay);
            Global.WriteInt16(stream, info.Identify);

            info.Bytes = stream.ToArray();

            return info;
        }

        public override GameInfo GetGameInfo(GameInfo infoOld)
        {
            if (!(infoOld is Wiz123GameInfo))
                return GetGameInfo();

            Wiz123GameInfo wizOld = infoOld as Wiz123GameInfo;
            Wiz123GameInfo wizNew = GetGameInfo() as Wiz123GameInfo;

            if (wizNew == null)
                return null;

            if (Global.CompareBytes(wizOld.Bytes, wizNew.Bytes))
                return infoOld; // All the bytes are the same; return the old object

            return wizNew;
        }

        public override bool SetCharCreationInfo(CharCreationInfo info)
        {
            if (!(info is Wiz123CharCreationInfo) || !IsValid)
                return false;

            Wiz123CharCreationInfo wizInfo = info as Wiz123CharCreationInfo;

            byte[] bytes = Global.NullBytes(12);
            for (int i = 0; i < 6; i++)
                bytes[i * 2] = info.AttributesOriginal[i];
            WriteOffset(Memory.CreateAttributes, bytes);
            WriteByte(Memory.CreateBonus, info.AttributesOriginal[6]);
            WriteUInt16(Memory.CreateGold, (ushort)wizInfo.Gold);
            if (wizInfo.SelectedStat != -1)
                WriteByte(Memory.CreationSelectedStat, (byte)wizInfo.SelectedStat);

            return true;
        }

        public override CharCreationInfo GetCharCreationInfo()
        {
            if (!IsValid)
                return null;

            Wiz123CharCreationInfo info = new Wiz123CharCreationInfo();
            info.State = GetGameState() as Wiz123GameState;

            byte[] bytes = ReadOffset(Memory.CreateAttributes, 16).Bytes;
            info.AttributesOriginal = new byte[] { bytes[0], bytes[2], bytes[4], bytes[6], bytes[8], bytes[10], ReadByte(Memory.CreateBonus) };
            info.SelectedStat = ReadByte(Memory.CreationSelectedStat);
            info.SelectedRace = ReadByte(Memory.CreationSelectedRace);
            info.Gold = ReadUInt16(Memory.CreateGold);

            return info;
        }

        public bool SetCharCreationName(string str)
        {
            if (str.Length < 1 || str.Length > 15)
                return false;

            return WriteOffset(Memory.CreateName, new ASCIIEncoding().GetBytes(str.ToUpper()));
        }

        public override bool SkipIntroductions(int iTimeout = 5000)
        {
            DateTime dtStart = DateTime.Now;
            bool bOut = false;
            while ((DateTime.Now - dtStart).TotalMilliseconds < iTimeout)
            {
                Wiz123GameState state = GetGameState() as Wiz123GameState;
                if (state != null)
                {
                    switch (state.Main)
                    {
                        case MainState.Opening:
                            SendKeysToDOSBox(new Keys[] { Keys.S }, true);  // Start the game
                            Thread.Sleep(200);
                            break;
                        case MainState.InsertDisk:
                            SendKeysToDOSBox(new Keys[] { Keys.Enter }, true);  // Skip this screen
                            Thread.Sleep(200);
                            break;
                        case MainState.Castle:
                            try
                            {
                                Wiz123RosterFile roster = CreateRoster(true) as Wiz123RosterFile;
                                Wiz123Character wizChar = Wiz123Character.Create(Game, 0, roster.LoadCharBytes(0), 0, null, true);
                                if (wizChar == null)
                                {
                                    Thread.Sleep(500);
                                    break;
                                }
                                if (wizChar.LocationZ > 0)
                                {
                                    bOut = true;
                                    SendKeysToDOSBox(new Keys[] { Keys.E }, true);  // Go to the Edge of Town to restart an out party
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                            }
                            SendKeysToDOSBox(new Keys[] { Keys.G }, true);  // go the the Tavern to add characters
                            Thread.Sleep(200);
                            break;
                        case MainState.Tavern:
                            SendKeysToDOSBox(new Keys[] { Keys.A }, true);  // add characters to the party
                            Thread.Sleep(200);
                            break;
                        case MainState.TavernAddChar:
                            SendKeysToDOSBox(new Keys[] { Keys.A, Keys.B, Keys.C, Keys.D, Keys.E, Keys.F }, true);  // Add first six characters
                            Thread.Sleep(200);
                            SendKeysToDOSBox(new Keys[] { Keys.Enter, Keys.Enter }, true);  // Stop adding characters (if there were fewer than six) and exit the tavern
                            Thread.Sleep(100);
                            SendKeysToDOSBox(new Keys[] { Keys.E }, true);  // Go to the Edge of Town
                            break;
                        case MainState.EdgeOfTown:
                            if (bOut)
                                SendKeysToDOSBox(new Keys[] { Keys.U }, true);  // Utilities (to restart an out party)
                            else
                                SendKeysToDOSBox(new Keys[] { Keys.M }, true);  // Maze
                            Thread.Sleep(200);
                            break;
                        case MainState.Utilities:
                            SendKeysToDOSBox(new Keys[] { Keys.R }, true);  // Restart an out party
                            Thread.Sleep(200);
                            break;
                        case MainState.MoveSelectChars:
                            SendKeysToDOSBox(new Keys[] { Keys.A }, true);  // Add characters to the party
                            Thread.Sleep(200);
                            break;
                        case MainState.Camp:
                            SendKeysToDOSBox(new Keys[] { Keys.Enter }, true);  // Stop camping
                            Thread.Sleep(200);
                            break;
                        case MainState.Adventuring:  // Done!
                            return true;
                        default:
                            break;
                    }
                }
                Thread.Sleep(10);
            }
            return false;
        }

        public override TrainingInfo GetTrainingInfo()
        {
            if (!IsValid)
                return null;

            Wiz123TrainingInfo info = new Wiz123TrainingInfo();
            info.State = GetGameState() as Wiz123GameState;
            info.Party = ReadWiz123PartyInfo();
            info.MapIndex = GetCurrentMapIndex();
            info.MapName = GetMapTitle(info.MapIndex).Title;
            return info;
        }

        public override List<BaseCharacter> GetCharacters()
        {
            PartyInfo pi = GetPartyInfo();
            if (pi == null)
                return null;

            Wiz123EncounterInfo encounterInfo = null;
            if (GetGameState().InCombat)
                encounterInfo = GetEncounterInfo() as Wiz123EncounterInfo;

            List<BaseCharacter> chars = new List<BaseCharacter>(pi.NumChars);
            for (int i = 0; i < pi.NumChars; i++)
            {
                Wiz123Character wizChar = Wiz123Character.Create(Game, i, pi.Bytes, Wiz123Character.SizeInBytes * i, encounterInfo);
                wizChar.Address = i;
                chars.Add(wizChar);
            }

            return chars;
        }

        public override bool SetTrainingInfo(TrainingInfo info)
        {
            if (!IsValid)
                return false;

            if (info is Wiz123TrainingInfo)
            {
                Wiz123TrainingInfo wiz1Info = info as Wiz123TrainingInfo;
                byte[] bytes = new byte[4];
                Buffer.BlockCopy(info.Party.Bytes, wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz123.Offsets.Stats, bytes, 0, 4);
                WriteOffset(Memory.PartyInfo + (wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz123.Offsets.Stats), bytes);

                UInt16 iCurrentHP = BitConverter.ToUInt16(info.Party.Bytes, wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz123.Offsets.CurrentHP);
                UInt16 iMaxHP = BitConverter.ToUInt16(info.Party.Bytes, wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz123.Offsets.MaxHP);

                WriteUInt16(Memory.PartyInfo + (wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz123.Offsets.CurrentHP), iCurrentHP);
                return WriteUInt16(Memory.PartyInfo + (wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz123.Offsets.MaxHP), iMaxHP);
            }

            return false;
        }

        public override string PartyExitsWhat { get { return "castle"; } }

        public override CureAllInfo GetCureAllInfo(int iCasterIndex, int[] partyAddresses)
        {
            if (!IsValid)
                return null;

            if (iCasterIndex >= partyAddresses.Length)
                return null;

            Wiz123CureAllInfo info = new Wiz123CureAllInfo();
            Wiz123PartyInfo party = ReadWiz123PartyInfo();

            info.Conditions = new Wiz123Condition[party.NumChars];
            info.HitPoints = new Int16[party.NumChars];
            info.HitPointsMax = new Int16[party.NumChars];
            for (int i = 0; i < partyAddresses.Length; i++)
            {
                info.Conditions[i] = (Wiz123Condition)party.Bytes[partyAddresses[i] * party.CharacterSize + Wiz123.Offsets.Condition];
                info.HitPoints[i] = BitConverter.ToInt16(party.Bytes, partyAddresses[i] * party.CharacterSize + Wiz123.Offsets.CurrentHP);
                info.HitPointsMax[i] = BitConverter.ToInt16(party.Bytes, partyAddresses[i] * party.CharacterSize + Wiz123.Offsets.MaxHP);
            }

            int iCasterAddress = partyAddresses[iCasterIndex];
            info.CasterCondition = info.Conditions[iCasterAddress];
            info.CasterSpellPoints = new Wiz123SpellPoints(party.Bytes, iCasterAddress * party.CharacterSize + Wiz123.Offsets.CurrentSP);
            info.CasterSpells = new Wiz123KnownSpells(party.Bytes, iCasterAddress * party.CharacterSize + Wiz123.Offsets.Spells);
            int map = GetCurrentMapIndex();
            info.InCastle = IsCastle(map);
            return info;
        }

        public override void SetCureAllInfo(CureAllInfo cureAll, int iCasterIndex, int[] partyAddresses)
        {
            if (!IsValid)
                return;

            if (iCasterIndex >= partyAddresses.Length)
                return;

            Wiz123CureAllInfo info = cureAll as Wiz123CureAllInfo;
            int iCasterAddress = partyAddresses[iCasterIndex];
            byte[] bytesSP = info.CasterSpellPoints.GetBytes();
            WriteOffset(Memory.PartyInfo + (iCasterAddress * Wiz123Character.SizeInBytes) + Wiz123.Offsets.CurrentSP, bytesSP);
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                WriteByte(Memory.PartyInfo + (partyAddresses[i] * Wiz123Character.SizeInBytes + Wiz123.Offsets.Condition), (byte)info.Conditions[i]);
                WriteInt16(Memory.PartyInfo + (partyAddresses[i] * Wiz123Character.SizeInBytes + Wiz123.Offsets.CurrentHP), info.HitPoints[i]);
            }
        }

        public override bool RefreshConditions()
        {
            Wiz123GameState state = GetGameState() as Wiz123GameState;
            switch (state.Main)
            {
                case MainState.Castle:
                    SendKeysToDOSBox(new Keys[] { Keys.E, Keys.C }, true);
                    break;
                case MainState.EdgeOfTown:
                    SendKeysToDOSBox(new Keys[] { Keys.C, Keys.E }, true);
                    break;
                case MainState.Tavern:
                case MainState.Inn:
                case MainState.Temple:
                case MainState.Shop:
                    SendKeysToDOSBox(new Keys[] { Keys.D1, Keys.Enter, Keys.Enter }, true);
                    break;
                default:
                    break;
            }
            return true;
        }

        protected virtual Wiz123QuestInfo CreateQuestInfo() { return null; }

        public override QuestInfo GetQuestInfo(QuestInfo lastInfo = null, int iOverrideCharAddress = -1, bool bAllowSelectionDialog = false)
        {
            if (!IsValid)
                return null;

            Wiz123QuestInfo info = CreateQuestInfo();

            Wiz123PartyInfo party = ReadWiz123PartyInfo();
            if (party == null)
                return null;

            Wiz123MapData map = GetMapData(false, 0) as Wiz123MapData;

            MemoryStream stream = new MemoryStream();
            byte[] questBytes = party.QuestBytes;
            if (questBytes == null)
                return null;
            stream.Write(questBytes, 0, questBytes.Length);
            MemoryBytes mbFights = ReadOffset(Memory.FightMap, 80);
            if (mbFights == null)
                return null;
            stream.Write(mbFights.Bytes, 0, mbFights.Length);
            QuestData data = GetQuestData();
            if (data != null)
                Global.WriteBytes(stream, data.GetBytes());
            stream.WriteByte((byte)iOverrideCharAddress);
            LocationInformation location = GetLocation();
            info.MapIndex = location.MapIndex;
            stream.WriteByte((byte)info.MapIndex);
            stream.WriteByte((byte)location.PrimaryCoordinates.X);
            stream.WriteByte((byte)location.PrimaryCoordinates.Y);

            byte[] newBytes = stream.ToArray();

            if (lastInfo != null && Global.CompareBytes(lastInfo.Bytes, newBytes))
                return lastInfo;    // Don't bother going through the lengthy SetQuests routine if nothing has changed

            info.SetQuests(party, location, mbFights.Bytes, data, GetGameState() as Wiz123GameState, iOverrideCharAddress);
            info.Bytes = newBytes;

            return info;
        }

        public override IEnumerable<Monster> GetMonsterList() { return Wiz1.Monsters; }

        public override bool AutoCombat()
        {
            Wiz123GameState state = ReadWiz123GameState();

            switch (state.Main)
            {
                case MainState.ReceiveExp:
                case MainState.PreCombat:
                    SendKeysToDOSBox(new Keys[] { Keys.LMenu }, true);
                    return true;
                case MainState.CombatFriendly:
                    // Auto-attack -> always fight
                    SendKeysToDOSBox(new Keys[] { Keys.F }, true);
                    return true;
                case MainState.CombatSelectFightTarget:
                    SendKeysToDOSBox(new Keys[] { Keys.D1 }, true);
                    return true;
                case MainState.CombatConfirmRound:
                    SendKeysToDOSBox(new Keys[] { Keys.Enter }, true);
                    return true;
                case MainState.CombatOptions:
                    bool bCanAttack = state.ActingCombatChar < 3;

                    if (!bCanAttack)
                        SendKeysToDOSBox(new Keys[] { Keys.P }, true);
                    else
                    {
                        Wiz123EncounterInfo info = GetEncounterInfo() as Wiz123EncounterInfo;
                        if (info == null)
                            SendKeysToDOSBox(new Keys[] { Keys.Enter }, true);
                        else
                        {
                            int iNumAttackableGroups = info.Groups.Count(g => g.NumAlive > 0);
                            if (iNumAttackableGroups < 2)
                                SendKeysToDOSBox(new Keys[] { Keys.Enter }, true);
                            else
                                SendKeysToDOSBox(new Keys[] { Keys.Enter, Keys.D1 }, true);
                        }
                    }
                    return true;
                default:
                    if (state.InCombat)
                    {
                        SendKeysToDOSBox(new Keys[] { Keys.LMenu }, true);  // Speeds up combat
                        return true;
                    }
                    return false;
            }
        }
    }
}
