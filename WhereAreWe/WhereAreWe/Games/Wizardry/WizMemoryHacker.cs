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
    public class WizQuestData : QuestData
    {
        public byte[] Fights;

        public WizQuestData(WizPartyInfo party, LocationInformation location, byte[] fights, WizGameState state)
        {
            Party = party;
            Location = location;
            Fights = fights;
            State = state;
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
            if (Fights != null)
                Global.WriteBytes(stream, Fights);
        }
    }

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

        public Wiz123CharCreationInfo(WizRace race, int bonus, int selectedStat, int gold)
        {
            SelectedRace = (int) race;
            SelectedStat = selectedStat;
            Gold = gold;
            switch (race)
            {
                case WizRace.Human:
                    AttributesOriginal = new byte[] { 8, 8, 5, 8, 8, 9, (byte)bonus };
                    break;
                case WizRace.Elf:
                    AttributesOriginal = new byte[] { 7, 10, 10, 6, 9, 6, (byte)bonus };
                    break;
                case WizRace.Dwarf:
                    AttributesOriginal = new byte[] { 10, 7, 10, 10, 5, 6, (byte)bonus };
                    break;
                case WizRace.Gnome:
                    AttributesOriginal = new byte[] { 7, 7, 10, 8, 10, 7, (byte)bonus };
                    break;
                case WizRace.Hobbit:
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

                if (AttributesOriginal[6] < 0 || AttributesOriginal[6] > 30)
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
        public WizGameState State;

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

    public abstract class WizKnownSpells : KnownSpells
    {
        protected bool[] KnownSpells;
        public override int NumKnown { get { return KnownSpells.Count(s => s); } }
        public abstract IEnumerable<WizardrySpell> Spells { get; }
        public abstract IEnumerable<WizardrySpell> HealingSpells { get; }
        public abstract bool KnowsAnyHealing { get; }

        public override bool UsesSpellType { get { return false; } }
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

        public WizSpellPoints GetMaxSpellPoints()
        {
            WizSpellPoints sp = new WizSpellPoints();
            foreach (WizardrySpell spell in Spells)
            {
                switch (spell.Type)
                {
                    case SpellType.Mage:
                        if (IsKnown(spell.BasicIndex, spell.Type))
                            sp.Mage[spell.Level]++;
                        break;
                    case SpellType.Priest:
                        if (IsKnown(spell.BasicIndex, spell.Type))
                            sp.Priest[spell.Level]++;
                        break;
                }
            }
            return sp;
        }

        protected void InitEmpty(byte[] bytes, int offset = 0)
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
            for (int i = 0; i < 8; i++)
                bytes[i] = ByteFromBits(KnownSpells, i * 8);
            return bytes;
        }

        public abstract int KnownMage { get; }
        public abstract int KnownPriest { get; }
        public abstract int NextMage { get; }
        public abstract int NextPriest { get; }

        public override string ToString()
        {
            return KnownString(GenericClass.None);
        }
    }

    public class Wiz5KnownSpells : WizKnownSpells
    {
        public override bool IsKnown(int internalIndex, GenericClass mmClass) { return IsKnown((Wiz5SpellIndex)internalIndex); }
        public override KnownSpells CreateNew(GenericClass charClass, KnownSpells original = null) { return new Wiz5KnownSpells(new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 }); }
        public override void SetKnown(Spell spell, bool bKnown) { SetKnown(spell as Wiz5Spell, bKnown); }
        public bool IsKnown(Wiz5SpellIndex spell) { return KnownSpells != null && KnownSpells.Length > (int)spell && KnownSpells[(int)spell]; }
        public void SetKnown(Wiz5Spell spell, bool bKnown) { KnownSpells[(int)spell.Index] = bKnown; }
        public override IEnumerable<WizardrySpell> Spells { get { return Wiz5.Spells; } }
        public override IEnumerable<WizardrySpell> HealingSpells { get { return Wiz5SpellList.HealingSpells; } }
        public override bool KnowsAnyHealing { get { return HealingSpells.Any(s => IsKnown((Wiz5SpellIndex)s.BasicIndex)); } }
        public Wiz5KnownSpells(byte[] bytes, int offset = 0) { InitEmpty(bytes, offset); }
        public override int KnownMage { get { return Wiz5SpellList.Mage.Count(s => KnownSpells[(int)s]); } }
        public override int KnownPriest { get { return Wiz5SpellList.Priest.Count(s => KnownSpells[(int)s]); } }

        public override bool IsKnown(int index, SpellType type)
        {
            if (type == SpellType.Priest && index < 32)
                index += 31;
            return IsKnown((Wiz5SpellIndex)index);
        }

        public override int NextMage { get { return (int) Wiz5SpellList.Mage.FirstOrDefault(m => !KnownSpells[(int)m]); } }
        public override int NextPriest { get { return (int) Wiz5SpellList.Priest.FirstOrDefault(m => !KnownSpells[(int)m]); } }
    }

    public class Wiz123KnownSpells : WizKnownSpells
    {
        public override bool IsKnown(int internalIndex, GenericClass mmClass) { return IsKnown((Wiz1234SpellIndex) internalIndex); }
        public override bool IsKnown(int index, SpellType type) { return IsKnown((Wiz1234SpellIndex)(index/* + (type == SpellType.Priest ? 21 : 0)*/)); }
        public override KnownSpells CreateNew(GenericClass charClass, KnownSpells original = null) { return new Wiz123KnownSpells(new byte[] {0, 0, 0, 0, 0, 0, 0, 0}); }
        public override void SetKnown(Spell spell, bool bKnown) { SetKnown(spell as Wiz1234Spell, bKnown); }
        public bool IsKnown(Wiz1234SpellIndex spell) { return KnownSpells != null && KnownSpells.Length > (int)spell && KnownSpells[(int)spell]; }
        public void SetKnown(Wiz1234Spell spell, bool bKnown) { KnownSpells[(int)spell.Index] = bKnown; }
        public override IEnumerable<WizardrySpell> Spells { get { return Wiz123.Spells; } }
        public override IEnumerable<WizardrySpell> HealingSpells { get { return Wiz1234SpellList.HealingSpells; } }
        public override bool KnowsAnyHealing { get { return HealingSpells.Any(s => IsKnown((Wiz1234SpellIndex) s.BasicIndex)); } }
        public Wiz123KnownSpells(byte[] bytes, int offset = 0) { InitEmpty(bytes, offset); }
        public override int KnownMage { get { return Wiz1234SpellList.Mage.Count(s => KnownSpells[(int)s]); } }
        public override int KnownPriest { get { return Wiz1234SpellList.Priest.Count(s => KnownSpells[(int)s]); } }

        public override int NextMage { get { return (int) Wiz1234SpellList.Mage.FirstOrDefault(m => !KnownSpells[(int) m]); } }
        public override int NextPriest { get { return (int) Wiz1234SpellList.Priest.FirstOrDefault(m => !KnownSpells[(int) m]); } }
    }

    public class WizSpellPoints : SpellPoints
    {
        public override string Current { get { return ToString(); } }
        public override bool HasAnyCurrent { get { return Mage.Any(m => m > 0) || Priest.Any(p => p > 0); } }

        public int[] Mage;
        public int[] Priest;

        public static WizSpellPoints Zero { get { return new WizSpellPoints(Global.NullBytes(28), 0); } }

        public WizSpellPoints()
        {
            Mage = new int[7];
            Priest = new int[7];
            for (int i = 0; i < 7; i++)
            {
                Mage[i] = 0;
                Priest[i] = 0;
            }
        }

        public WizSpellPoints(byte[] bytes, int offset)
        {
            if (bytes.Length - offset < 28)
                return;

            Mage = new int[7];
            Priest = new int[7];

            for (int i = 0; i < 7; i++)
            {
                Mage[i] = BitConverter.ToInt16(bytes, offset + (i * 2));
                Priest[i] = BitConverter.ToInt16(bytes, offset + (i * 2) + 14);
            }
        }

        public virtual byte[] GetBytes()
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
        Condition,
        BlackBox
    }

    public class PackedThreeBitValues
    {
        public int[] Values;

        public PackedThreeBitValues(params int[] values)
        {
            Values = values;
        }

        public PackedThreeBitValues(byte[] bytes, int offset = 0, int count = -1)
        {
            if (count == -1)
                count = bytes.Length - offset;

            // Five values are stored in every two bytes, as follows:
            // Bits:   76543210 FEDCBA98     or as a 16-bit int: FEDCBA9876543210
            // Values: 22111000 x4443332                         x444333222111000

            List<int> values = new List<int>(count / 2 * 5);
            for (int i = 0; i < count - 1; i += 2)
            {
                int i16 = BitConverter.ToUInt16(bytes, offset + i);
                values.Add(i16 & 0x0007);            // 0000000000000111
                values.Add((i16 & 0x0038) >> 3);     // 0000000000111000
                values.Add((i16 & 0x01C0) >> 6);     // 0000000111000000
                values.Add((i16 & 0x0E00) >> 9);     // 0000111000000000
                values.Add((i16 & 0x7000) >> 12);    // 0111000000000000
            }

            Values = values.ToArray();
        }

        public byte[] Bytes { get { return GetBytes(Values); } }

        public static byte[] GetBytes(params int[] values)
        {
            if (values == null)
                return new byte[0];

            List<byte> bytes = new List<byte>(values.Length / 5 + 1);
            for (int i = 0; i < values.Length; i++)
            {
                int i16 = values.Length > i ? values[i] : 0;
                i16 |= ((values.Length > i + 1 ? values[i + 1] : 0) << 3);
                i16 |= ((values.Length > i + 2 ? values[i + 2] : 0) << 6);
                i16 |= ((values.Length > i + 3 ? values[i + 3] : 0) << 9);
                i16 |= ((values.Length > i + 4 ? values[i + 4] : 0) << 12);
                bytes.AddRange(BitConverter.GetBytes((UInt16)i16));
            }

            return bytes.ToArray();
        }
    }

    public class PackedSixBitValues
    {
        public int[] Values;

        public PackedSixBitValues(params int[] values)
        {
            Values = values;
        }

        public PackedSixBitValues(byte[] bytes, int offset = 0, int count = -1)
        {
            if (count == -1)
                count = bytes.Length - offset;

            // Two values are stored in every two bytes, as follows:
            // Bits:   76543210 FEDCBA98     or as a 16-bit int: FEDCBA9876543210
            // Values: 11222222 xxxx1111                         xxxx111111222222

            Values = new int[count];
            for (int i = 0; i < count - 1; i += 2)
            {
                int i16 = BitConverter.ToUInt16(bytes, offset + i);
                Values[i] = i16 & 0x003F;            // 0000000000111111
                Values[i+1] = (i16 & 0x0FC0) >> 6;   // 0000111111000000
            }
        }

        public byte[] Bytes { get { return GetBytes(Values); } }

        public static byte[] GetBytes(params int[] values)
        {
            if (values == null)
                return new byte[0];

            byte[] bytes = new byte[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                int i16 = values.Length > i ? values[i] : 0;
                i16 |= ((values.Length > i + 1 ? values[i + 1] : 0) << 6);
                Global.SetInt16(bytes, i / 2, i16);
            }

            return bytes.ToArray();
        }
    }

    public class PackedFourBitValues
    {
        public int[] Values;

        public PackedFourBitValues(params int[] values)
        {
            Values = values;
        }

        public PackedFourBitValues(byte[] bytes, int offset = 0, int count = -1)
        {
            if (count == -1)
                count = bytes.Length - offset;

            Values = new int[count * 2];
            for (int i = 0; i < count; i++)
            {
                Values[i * 2] = bytes[offset + i] & 0x0f;
                Values[i * 2 + 1] = (bytes[offset + i] & 0xf0) >> 4;
            }
        }

        public byte[] Bytes { get { return GetBytes(Values); } }

        public static byte[] GetBytes(params int[] values)
        {
            if (values == null)
                return new byte[0];

            byte[] bytes = new byte[values.Length / 2];
            for (int i = 0; i < values.Length; i++)
            {
                bytes[i / 2] = (byte) values[i];
                bytes[i / 2] |= (byte) ((values.Length > i + 1 ? values[i + 1] : 0) << 4);
            }

            return bytes;
        }
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
            if (values == null)
                return new byte[0];
            long bits = 0;
            for (int i = 0; i < values.Length; i++)
            {
                bits = SetBits(bits, values[i], BitOrder[5 * i], BitOrder[5 * i + 1], BitOrder[5 * i + 2], BitOrder[5 * i + 3], BitOrder[5 * i + 4]);
            }
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

    public enum WizWall
    {
        Open =         0,
        SolidWall =    1,
        Door      =    2,
        HiddenDoor =   3,
        OffMap =      -1,
        SparseRock =  -2,
        Dependent =   -3,
    }

    public enum WizSquare
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

    public class WizGameState : GameState
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

    public class WizGameInfoItem : GameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return Wiz1MemoryHacker.GetMapTitlePair(iMap); }

        public WizGameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offsets, type, mask, style, fn)
        {
        }

        public WizGameInfoItem(string desc, object val, int offset, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, new OffsetList(offset), type, mask, style, fn)
        {
        }

        public WizGameInfoItem(string desc, object val, OffsetList offsets, BitDescriptionDelegate fn)
            : base(desc, val, offsets, DataType.Bits, 0, ShowStyle.Editable, fn)
        {
        }
    }

    public class WizBackpackBytes
    {
        public byte[,] Items;  // 8 sets of 8 bytes

        public WizBackpackBytes()
        {
            Items = new byte[8,8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    Items[i,j] = 0;
        }
    }

    public class WizPartyInfo : PartyInfo
    {
        public int[] Silenced = null;
        public List<Item> BlackBox = null;

        public WizPartyInfo(byte[] bytes, byte numchars)
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

        public override byte[] QuestBytes
        {
            get
            {
                if (Bytes == null)
                    return null;
                MemoryStream stream = new MemoryStream();
                for (int i = 0; i < NumChars; i++)
                {
                    stream.WriteByte(Bytes[i * CharacterSize + Wiz123.Offsets.Level]);
                    stream.WriteByte(Bytes[i * CharacterSize + Wiz123.Offsets.Alignment]);
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

            WizInventory inventory = null;
            if (game == GameNames.Wizardry5)
                inventory = new Wiz5Inventory(game, Bytes, iCharAddress * CharacterSize + Wiz5.Offsets.Inventory);
            else
                inventory = new WizInventory(game, Bytes, iCharAddress * CharacterSize + Wiz123.Offsets.Inventory);

            return inventory.HasItem(game, item, bEquippedOnly);
        }

        public bool CharacterHasItem(int iCharAddress, Wiz1ItemIndex item) { return CharacterHasItem(GameNames.Wizardry1, iCharAddress, (int)item); }
        public bool CharacterHasItem(int iCharAddress, Wiz2ItemIndex item) { return CharacterHasItem(GameNames.Wizardry2, iCharAddress, (int)item); }
        public bool CharacterHasItem(int iCharAddress, Wiz3ItemIndex item) { return CharacterHasItem(GameNames.Wizardry3, iCharAddress, (int)item % 1000); }
        public bool CharacterHasItem(int iCharAddress, Wiz4ItemIndex item) { return CharacterHasItem(GameNames.Wizardry4, iCharAddress, (int)item); }
        public bool CharacterHasItem(int iCharAddress, Wiz5ItemIndex item) { return CharacterHasItem(GameNames.Wizardry5, iCharAddress, (int)item); }

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
        public bool CurrentPartyHasItem(Wiz3ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry3, (int)item % 1000); }
        public bool CurrentPartyHasItem(Wiz4ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry4, (int)item); }
        public bool CurrentPartyHasItem(Wiz5ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry5, (int)item); }

        public bool CurrentPartyHasEquipped(Wiz1ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry1, (int)item, true); }
        public bool CurrentPartyHasEquipped(Wiz2ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry2, (int)item, true); }
        public bool CurrentPartyHasEquipped(Wiz3ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry3, (int)item % 1000, true); }
        public bool CurrentPartyHasEquipped(Wiz4ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry4, (int)item, true); }
        public bool CurrentPartyHasEquipped(Wiz5ItemIndex item) { return CurrentPartyHasItem(GameNames.Wizardry5, (int)item, true); }

        public int AlignmentCount(WizAlignment alignment)
        {
            int iCount = 0;
            for (int i = 0; i < NumChars; i++)
            {
                if (Bytes[i * CharacterSize + Wiz123.Offsets.Alignment] == (int)alignment)
                    iCount++;
            }
            return iCount;
        }

        public override int CharacterSize { get { return WizCharacter.SizeInBytes; } }
    }

    public class WizSpellInfo : SpellInfo
    {
        public WizPartyInfo Party;
        public override bool ClassLimited { get { return false; } }

        public WizSpellInfo()
        {
            Party = null;
            Game = new WizGameState();
            Game.Main = MainState.Unknown;
            Game.InCombat = false;
            Game.Location = LocationInformation.Empty;
            Game.ActingCharAddress = -1;
        }
    }

    public class Wiz1234SpellInfo : WizSpellInfo
    {
        public Wiz1234Spell Spell;

        public Wiz1234SpellInfo() : base() { Spell = null;  }
    }

    public class WizMapData : WizardryMapData
    {
        public WizMapData(GameNames game, int index, byte[] bytes, int offset = 0, bool bSkipMainInfo = false, bool bSkipNonWallInfo = false)
        {
            SetFromBytes(game, index, bytes, offset, bSkipMainInfo, bSkipNonWallInfo);
        }
    }

    public class WizCureAllInfo : CureAllInfo
    {
        public WizCondition[] Conditions;   // 6 bytes; one per character
        public WizCondition CasterCondition;
        public Int16[] HitPoints;
        public Int16[] HitPointsMax;
        public Wiz123KnownSpells CasterSpells;
        public WizSpellPoints CasterSpellPoints;
        public bool InCastle;

        public override bool Valid { get { return Conditions != null && Conditions.Length > 0; } }
        public override bool IsHealer { get { return CasterSpells.KnowsAnyHealing; } }
        public override bool IsIncapacitated { get { return CasterCondition >= WizCondition.Asleep; } }
        public override bool MagicPermitted { get { return true; } }
        public override bool Combat { get { return !InCastle; } }
        public override string CombatString { get { return "the Maze"; } }

        public WizCureAllInfo()
        {
        }
    }

    public abstract class Wiz123SearchResults : SearchResults
    {
        public int RewardIndex;

        public abstract List<WizTreasure> Treasures { get; }
        public override bool HasTraps { get { return true; } }

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

    public class WizEncounterGroup
    {
        public enum Property { Index, Identified, Alive, Total, MagicScreen, FizzleField }

        public const int Size = 8 + (WizEncounterRecord.Size * 9) + (WizMonster.Size);

        public bool Identified;
        public int NumAlive;
        public int NumEnemies;
        public int Index;
        public int MagicScreen;
        public int FizzleField;
        public List<WizEncounterRecord> Records;
        public WizMonster Monster;
        public bool MonsterChanged = false;

        public Monster GetMonster(int index)
        {
            if (index >= Records.Count)
                return null;

            if (Monster == null)
                return null;

            WizMonster monster = Monster.Clone() as WizMonster;
            monster.Summoned = Monster.Summoned;
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

        public void AddMonsters(int iGroupIndex, ref int iEncounterIndex, int iRewardModifier, Point ptParty, Dictionary<int, Monster> monsters)
        {
            for (int iMonster = 0; iMonster < NumEnemies; iMonster++)
            {
                Monster monster = GetMonster(iMonster);
                if (monster != null)
                {
                    monster.Position = ptParty;
                    monster.MonsterGroup = iGroupIndex;
                    monster.MonsterSubGroup = iMonster;
                    monster.EncounterIndex = iEncounterIndex;
                    monster.RewardModifier = iRewardModifier;
                    if (iMonster >= NumAlive || monster.Index < 0 || monster.CurrentHP == 0 || monster.Condition.HasFlag(BasicConditionFlags.Dead))
                        monster.Killed = true;
                    Monster = monster as WizMonster;
                    monsters.Add(iEncounterIndex, monster);
                    iEncounterIndex++;
                }
            }
        }

        public WizEncounterGroup()
        {
            SetDefaults();
        }

        public void SetDefaults()
        {
            Identified = false;
            NumAlive = 0;
            NumEnemies = 0;
            Index = -1;
            Records = new List<WizEncounterRecord>(9);
            Monster = null;
            MonsterChanged = false;
            MagicScreen = 0;
            FizzleField = 0;
        }

        public WizEncounterGroup(byte[] bytes, int offset = 0)
        {
            if (bytes == null)
            {
                SetDefaults();
                return;
            }

            Identified = BitConverter.ToInt16(bytes, offset) != 0;
            NumAlive = BitConverter.ToInt16(bytes, offset + 2);
            NumEnemies = BitConverter.ToInt16(bytes, offset + 4);
            Index = BitConverter.ToInt16(bytes, offset + 6);
            MagicScreen = 0;
            FizzleField = 0;
            Records = new List<WizEncounterRecord>(9);
            for (int i = 0; i < 9; i++)
            {
                if (i < NumAlive)
                    Records.Add(new WizEncounterRecord(bytes, offset + 8 + (16 * i)));
                else
                    Records.Add(WizEncounterRecord.Default);
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
                case Property.MagicScreen: MagicScreen = val; break;
                case Property.FizzleField: FizzleField= val; break;
                default: break;
            }
            return false;
        }
    }

    public class WizEncounterRecord
    {
        public enum Property { HP, AC, Condition, Initiative, Victim, Silenced, Unknown, SpellHash, Unknown2, Level }

        public const int Size = 16;

        public int Victim;
        public int SpellHash;
        public int Initiative;
        public int CurrentHP;
        public int ArmorClass;
        public int Silenced;
        public WizCondition Condition;
        public int Unknown1;
        public int Unknown2;
        public int Level;

        public void ChangeValue(int iIndex, int val)
        {
            switch ((Property)iIndex)
            {
                case Property.HP: CurrentHP = val; break;
                case Property.AC: ArmorClass = val; break;
                case Property.Condition: Condition = (WizCondition)val; break;
                case Property.Initiative: Initiative = val; break;
                case Property.Victim: Victim = val; break;
                case Property.Unknown: Unknown1 = val; break;
                case Property.Silenced: Silenced = val; break;
                case Property.SpellHash: SpellHash = val; break;
                case Property.Unknown2: Unknown2 = val; break;
                case Property.Level: Level = val; break;
                default: break;
            }
        }

        public WizEncounterRecord()
        {
            Victim = 0;
            SpellHash = -1;
            Initiative = 0;
            CurrentHP = 0;
            ArmorClass = 0;
            Silenced = 0;
            Condition = WizCondition.Good;
            Unknown1 = 0;
            Unknown2 = 0;
            Level = 0;
        }

        public static WizEncounterRecord Default
        {
            get
            {
                WizEncounterRecord record = new WizEncounterRecord();
                record.ArmorClass = 0;
                record.Condition = WizCondition.Good;
                record.CurrentHP = 1;
                record.Initiative = 0;
                record.Silenced = 0;
                record.SpellHash = -1;
                record.Unknown1 = 1;
                record.Victim = 1;
                record.Level = 1;
                record.Unknown2 = 0;
                return record;
            }
        }

        public WizEncounterRecord(byte[] bytes, int offset = 0)
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
            Condition = (WizCondition)BitConverter.ToInt16(bytes, offset + 14);
        }
    }

    public abstract class WizEncounterInfo : EncounterInfo
    {
        protected Dictionary<int, Monster> m_monsters;
        public List<WizEncounterGroup> Groups;
        public WizEncounterGroup Characters { get { return Groups == null || Groups.Count < 1 ? null : Groups[0]; } }
        public const int Size = 5 * (WizEncounterGroup.Size + WizMonster.Size);
        private string m_sExtraTitleText = String.Empty;

        public override string ExtraTitleText { get { return m_sExtraTitleText; } }

        public abstract WizMonster CreateMonster(int index, byte[] bytes, int offset);
        public abstract void CreateSearchResults(int iRewardIndex);
        public virtual int GetGroupSize() { return WizEncounterGroup.Size; }
        public virtual int GetRecordSize() { return WizEncounterRecord.Size; }
        public virtual WizEncounterGroup CreateEncounterGroup(byte[] bytes = null, int offset = 0) { return new WizEncounterGroup(bytes, offset); }
        public virtual int GetMonsterOffset() { return 16 * 9 + 8; }
        public virtual int DoubleGoldModifier { get { return 1; } }
        public virtual int ExtraTreasureModifier { get { return 2; } }
        public virtual int MaxEncounterGroups { get { return 6; } }

        public void SetBytes(WizGameState state, byte[] bytes, Point ptParty, int iRewardModifier, int offset = 0)
        {
            PartyLocation = ptParty;
            int iMonsterOffset = GetMonsterOffset();
            if (bytes.Length - offset < (4 * iMonsterOffset))
                return;

            m_monsters = new Dictionary<int, Monster>();

            Groups = new List<WizEncounterGroup>(6);

            int iCount = 0;
            int iSummonedMonsters = 0;
            int iPlayers = 0;

            int iRecordSize = GetRecordSize();
            if (!state.IsTreasure)
            {
                int iGroupSize = GetGroupSize();

                for (int i = 0; i < MaxEncounterGroups; i++)
                {
                    if (bytes.Length - offset < iGroupSize * i)
                        continue;

                    WizEncounterGroup group = CreateEncounterGroup(bytes, offset + (iGroupSize * i));
                    if (group == null)
                        continue;

                    group.Monster = CreateMonster(group.Index, bytes, offset + (i * iGroupSize) + iMonsterOffset);

                    switch (i)
                    {
                        case 0:
                            group.Identified = true;   // Player-characters
                            iPlayers += group.NumAlive;
                            break;
                        case 5:
                            group.Monster.Summoned = true;
                            iSummonedMonsters += group.NumAlive;
                            break;
                    }

                    Groups.Add(group);
                    Groups[i].AddMonsters(i, ref iCount, iRewardModifier, ptParty, m_monsters);
                }
            }

            NumTotalMonsters = iCount - iSummonedMonsters - iPlayers;

            if (NumTotalMonsters < 0 || NumTotalMonsters > (9 * 6))
                NumTotalMonsters = 0;

            if (iRewardModifier == DoubleGoldModifier)
                m_sExtraTitleText = "(double gold!)";
            else if (iRewardModifier == ExtraTreasureModifier)
                m_sExtraTitleText = "(extra treasure!)";
        }

        public virtual WizEncounterRecord CreateEncounterRecord(byte[] bytes, int offset = 0)
        {
            return new WizEncounterRecord(bytes, offset);
        }

        public virtual byte[] GetCharBytes()
        {
            MemoryStream ms = new MemoryStream();
            foreach (WizEncounterRecord record in Characters.Records)
                WriteRecord(ms, record);
            return ms.ToArray();
        }

        private void WriteRecord(Stream stream, WizEncounterRecord record)
        {
            Global.WriteInt16(stream, record.Victim);
            Global.WriteInt16(stream, record.SpellHash);
            Global.WriteInt16(stream, record.Initiative);
            Global.WriteInt16(stream, record.CurrentHP);
            Global.WriteInt16(stream, -record.ArmorClass);
            Global.WriteInt16(stream, record.Silenced);
            Global.WriteInt16(stream, record.Unknown1);
            Global.WriteInt16(stream, (int)record.Condition);
        }

        public virtual byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            foreach (WizEncounterGroup group in Groups)
            {
                Global.WriteInt16(ms, group.Identified ? 1 : 0);
                Global.WriteInt16(ms, group.NumAlive);
                Global.WriteInt16(ms, group.NumEnemies);
                Global.WriteInt16(ms, (int) group.Index);

                foreach (WizEncounterRecord record in group.Records)
                    WriteRecord(ms, record);

                if (group.Monster == null)
                    return ms.ToArray();

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

            WizCharacter[] characters = new WizCharacter[Party.Bytes.Length / Party.CharacterSize];
            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
            {
                characters[iIndex] = WizCharacter.Create(gameInfo.Game, 0, Party.Bytes, iIndex * WizCharacter.SizeInBytes, null);
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
        protected int m_iCurrentMapAdventuringCount = 0;

        protected MainState m_stateLastNonTransitional = MainState.Unknown;
        protected WizEncounterInfo m_lastEncounterInfo = null;

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
            return WizCharacter.GetStatModifier(value, stat);
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

        public override int CharacterSize { get { return WizCharacter.SizeInBytes; } }
        public override BaseCharacter CreateCharFromBytes(byte[] bytes) { return WizCharacter.Create(Game, 0, bytes, 0, null); }
        public override byte[] GetInventoryCharBytes() { return Properties.Resources.Wizardry_1_Inventory_Char; }

        protected override int FindNextInventoryChar(int iStart, InventoryCharAction action)
        {
            if (!ValidateRosterFile())
                return -1;

            byte[] bytes = new byte[CharacterSize];
            WizRosterFile wizRoster = m_roster as WizRosterFile;

            while (iStart >= 0)
            {
                WizCharacter wizChar = null;
                if (iStart < wizRoster.Chars.Count)
                    wizChar = CreateCharFromBytes(wizRoster.Chars[iStart].Bytes) as WizCharacter;

                switch (action)
                {
                    case InventoryCharAction.FindExisting:
                        if (wizChar != null && wizChar.Name == "INVENTORY" && wizChar.Condition != WizCondition.Lost && wizChar.Inventory.Items.Count > 0)
                        {
                            wizRoster.SetGoodCondition(iStart);
                            return iStart;
                        }
                        break;
                    case InventoryCharAction.FindOrCreate:
                        if (wizChar != null && wizChar.Name == "INVENTORY")
                        {
                            wizRoster.SetGoodCondition(iStart);
                            return iStart;
                        }
                        if (wizChar == null || String.IsNullOrWhiteSpace(wizChar.Name) || wizChar.Condition == WizCondition.Lost)
                        {
                            // No character in the roster at this position; make a new one;
                            wizRoster.Chars[iStart].Bytes = GetInventoryCharBytes();
                            wizRoster.SaveCharBytes(iStart);
                            return iStart;
                        }
                        break;
                    case InventoryCharAction.FindPotential:
                        if (wizChar == null || String.IsNullOrWhiteSpace(wizChar.Name))
                            return iStart;
                        if (wizChar.Condition == WizCondition.Lost || wizChar.Name == "INVENTORY")
                            return iStart;
                        break;
                    default:
                        break;
                }
                iStart--;
            }

            return -1;
        }

        protected virtual int InventoryOffset { get { return Wiz123.Offsets.Inventory; } }
        protected virtual int InventoryLength { get { return Wiz123.Offsets.InventoryLength; } }
        protected virtual WizInventory CreateInventory(List<Item> items) { return new WizInventory(items); }
        protected virtual WizInventory CreateInventory(byte[] bytesChar) { return new WizInventory(Game, bytesChar, InventoryOffset); }

        public override SetBackpackResult SetBackpackInRoster(int iRosterPosition, List<Item> items)
        {
            // Completely overwrites a backpack, including equipped items
            // Also removes the "equipped" status of all items

            if (iRosterPosition < 0 || iRosterPosition > 19)
                return SetBackpackResult.InvalidPosition;

            if (!ValidateRosterFile())
                return SetBackpackResult.InvalidFile;

            if (iRosterPosition >= m_roster.Chars.Count)
                return SetBackpackResult.InvalidPosition;

            WizInventory inv = CreateInventory(items);
            foreach (WizItem item in inv.Items)
            {
                if (item == null)
                    continue;
                item.Equipped = false;
                item.Cursed = false;
            }

            byte[] bytesChar = m_roster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return SetBackpackResult.LoadCharFailure;

            Buffer.BlockCopy(inv.GetBytes(), 0, bytesChar, InventoryOffset, InventoryLength);
            m_roster.SaveCharBytes(iRosterPosition, 0, bytesChar);

            return SetBackpackResult.Success;
        }

        public override List<Item> GetBackpackFromRoster(int iRosterPosition)
        {
            if (iRosterPosition < 0 || iRosterPosition > 19)
                return null;

            if (!ValidateRosterFile())
                return null;

            if (iRosterPosition >= m_roster.Chars.Count)
                return null;

            byte[] bytesChar = m_roster.LoadCharBytes(iRosterPosition);
            if (bytesChar == null)
                return null;

            WizInventory inv = CreateInventory(bytesChar);

            return inv.SelectUnequippedItems;
        }

        public override CureAllResult CureAll(CureAllInfo cureAllInfo)
        {
            bool bUnknownSpells = false;

            if (!(cureAllInfo is WizCureAllInfo))
                return CureAllResult.Error;

            WizCureAllInfo info = cureAllInfo as WizCureAllInfo;

            // Okay, let's start curing!  Since Wizardry 1 cure-all requires being in the Castle,
            // we don't need to check spell points, just whether the spell is known (entering the Maze restores
            // all of your SP anyway, at least on the DOS version).
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                if (info.Conditions[i] >= WizCondition.Dead)
                    continue;   // Don't deal with death and eradication; make the player do that manually

                if (info.Conditions[i] == WizCondition.Petrified)
                {
                    if (info.CasterSpells.IsKnown(Wiz1234SpellIndex.Madi))
                        info.Conditions[i] = WizCondition.Good;
                    else
                        bUnknownSpells = true;
                }
                if (info.Conditions[i] == WizCondition.Paralyzed || info.Conditions[i] == WizCondition.Asleep)
                {
                    if (info.CasterSpells.IsKnown(Wiz1234SpellIndex.Madi) || info.CasterSpells.IsKnown(Wiz1234SpellIndex.Dialko))
                        info.Conditions[i] = WizCondition.Good;
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
                        if (info.CasterSpells.IsKnown(Wiz1234SpellIndex.Dios) ||
                            info.CasterSpells.IsKnown(Wiz1234SpellIndex.Dial) ||
                            info.CasterSpells.IsKnown(Wiz1234SpellIndex.Dialma) ||
                            info.CasterSpells.IsKnown(Wiz1234SpellIndex.Madi)
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

        protected virtual int GetActingCombatChar() { return ReadByte(Memory.CombatOptionActiveChar); }
        protected virtual MainState GetMainState(int state1, int state2, int state3, int state4, int state5) { return MainState.Unknown; }

        public override GameState GetGameState() { return ReadWiz123GameState(); }

        private WizGameState ReadWiz123GameState()
        {
            if (m_gsCurrent != null && m_gsCurrent.ReadTime.AddMilliseconds(50) > DateTime.Now)
                return m_gsCurrent as WizGameState;     // Don't spam the game state from different windows

            WizGameState state = new WizGameState();

            state.WizGame = Game;
            state.Location = GetLocationForce();

            state.Main = GetMainState(ReadUInt16(Memory.State1),
                ReadUInt16(Memory.State2),
                ReadUInt16(Memory.State3),
                ReadUInt16(Memory.State4),
                ReadUInt16(Memory.State5));

            if (state.Main == MainState.Transitional)
                state.Main = m_stateLastNonTransitional;
            else
            {
                if (state.Main != MainState.Unknown)
                    m_stateLastNonTransitional = state.Main;
            }

            int iActing = GetInspectingChar(state.Main);
            state.ActingCharAddress = -1;
            state.ActingCaster = -1;
            state.ActingCombatChar = GetActingCombatChar();
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
                    state.ActingCharAddress = iActing;
                    state.Inspecting = true;
                    break;
                case MainState.SelectSpell:
                    state.ActingCharAddress = iActing;
                    state.ActingCaster = iActing;
                    state.Inspecting = true;
                    state.CastingState = true;
                    break;
                case MainState.Combat:
                case MainState.CombatOptions:
                case MainState.CombatSelectFightTarget:
                case MainState.CombatConfirmRound:
                case MainState.CombatFriendly:
                case MainState.CombatUseItem:
                    state.InCombat = true;
                    break;
                case MainState.CombatSelectSpell:
                    state.ActingCharAddress = state.ActingCombatChar;
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

        protected virtual int GetInspectingChar(MainState state = MainState.Camp)
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
                    return GetActingCombatChar();
                default:
                    int i1 = ReadByte(Memory.InspectingChar);
                    int i2 = ReadByte(Memory.InspectingChar2);
                    int numChars = GetNumChars();
                    if (i2 > numChars)
                        return i1;
                    else if (i1 > numChars)
                        return i2;
                    return i2;
            }
        }

        private WizPartyInfo ReadWiz123PartyInfo()
        {
            byte numChars = (byte) GetNumChars();
            if (numChars > 6)
                numChars = 6;
            if (m_block == null)
                return null;
            if (numChars == 0)
                return new WizPartyInfo(new byte[0], 0);

            MemoryBytes bytes = ReadOffset(Memory.PartyInfo, WizCharacter.SizeInBytes * numChars);
            WizPartyInfo info = new WizPartyInfo(bytes, numChars);

            info.State = GetGameState() as WizGameState;
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

        public override int GetLightDistance(Point ptLocation)
        {
            byte extras = ReadByte(Memory.Map + WizardryMapData.Offsets.Extras + (ptLocation.X * 10) + (ptLocation.Y / 2));
            int iExtra = (ptLocation.Y % 2 == 0 ? extras & 0xf : extras >> 4);
            byte types = ReadByte(Memory.Map + WizardryMapData.Offsets.Types + (iExtra / 2));
            int iType = (iExtra % 2 == 0 ? types & 0xf : types >> 4);
            short aux2 = ReadInt16(Memory.Map + WizardryMapData.Offsets.Aux2 + (iExtra * 2));

            if (iType == (int)WizSquare.Dark && aux2 != 1)
                return 0;

            if (ReadInt16(Memory.Light) > 0)
                return 4;
            return 2;
        }

        protected virtual LocationInformation GetLocationForce()
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

            if (info.MapIndex < 1 || info.MapIndex > 250)
                info.PrimaryCoordinates = new Point(0, 0);

            info.CanUseBag = IsCastle(info.MapIndex) || Global.Cheats;

            info.NumChars = (byte) GetNumChars();
            info.LightDistance = GetLightDistance(info.PrimaryCoordinates);
            return info;
        }

        public virtual int GetNumChars() { return ReadByte(Memory.NumChars); }

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

            return new WizMapData(Game, GetCurrentMapIndex(), bytes.Bytes);
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            return WriteOffset(Memory.PartyInfo + (iAddress * WizCharacter.SizeInBytes), bytes, Math.Min(WizCharacter.SizeInBytes, bytes.Length));
        }

        public virtual WizSpellInfo CreateSpellInfo() { return new Wiz1234SpellInfo(); }

        public override SpellInfo GetSpellInfo()
        {
            if (!IsValid)
                return null;

            WizSpellInfo info = CreateSpellInfo();
            IntPtr pRead = IntPtr.Zero;
            info.Game = GetGameState() as WizGameState;

            // set info.Spell somehow
            if (!info.Game.Casting)
                return info;

            info.Party = GetPartyInfo() as WizPartyInfo;

            if (info.Game.ActingCharAddress == -1)
                info.Game.ActingCharAddress = info.Party.ActingChar;

            if (info.Game.ActingCaster == -1)
            {
                if (info.Game.InCombat)
                    info.Game.ActingCaster = info.Party.ActingCombatChar;
                else
                    info.Game.ActingCaster = info.Party.ActingCaster;
            }
            return info;
        }

        public virtual byte[] GetBackpackBytes(int iCharAddress)
        {
            return ReadOffset(Memory.PartyInfo + (iCharAddress * WizCharacter.SizeInBytes) + Wiz123.Offsets.Inventory, Wiz123.Offsets.InventoryLength).Bytes;
        }

        public virtual bool SetBackpackBytes(int iCharAddress, byte[] bytes)
        {
            return WriteOffset(Memory.PartyInfo + (iCharAddress * WizCharacter.SizeInBytes) + Wiz123.Offsets.Inventory, bytes);
        }

        public override SetBackpackResult SetBackpack(int iCharAddress, List<Item> items, bool bRemoveEquipped = false)
        {
            if (!IsValid)
                return SetBackpackResult.InvalidHacker;

            if (iCharAddress < 0)
                return SetBackpackResult.InvalidPosition;

            if (items == null || (items.Count > 0 && !(items[0] is WizItem)))
                return SetBackpackResult.InvalidItems;

            if (this is Wiz4MemoryHacker)
            {
                // Remove any Black Box items; those are stored in a completely different part of memory
                int index = items.Count - 1;
                while (index >= 0)
                {
                    if (items[index].MemoryIndex > 7)
                        items.RemoveAt(index);
                    index--;
                }
            }

            WizInventory inv = null;
            if (this is Wiz5MemoryHacker)
                inv = new Wiz5Inventory(Game, GetBackpackBytes(iCharAddress));
            else
                inv = new WizInventory(Game, GetBackpackBytes(iCharAddress));

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

            byte[] bytes = null;
            if (this is Wiz5MemoryHacker)
                bytes = new Wiz5Inventory(listNew).GetBytes();
            else
                bytes = new WizInventory(listNew).GetBytes();
            
            SetBackpackBytes(iCharAddress, bytes);

            return SetBackpackResult.Success;
        }

        public override List<Item> GetBackpack(int iCharAddress)
        {
            List<Item> list = new List<Item>();

            if (!IsValid || iCharAddress < 0)
                return list;

            byte[] bytes = GetBackpackBytes(iCharAddress);
            WizInventory inv = new WizInventory(Game, bytes);

            return inv.SelectUnequippedItems;
        }

        public abstract List<WizItem> WizItems { get; }

        public override void RandomizeBackpack(BaseCharacter baseChar, ItemType type, bool bUsableOnly, bool bSingleModifierOnly)
        {
            WizCharacter wizChar = baseChar as WizCharacter;
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
                WizItem item = WizItem.CreateRandom(WizItems, type, bUsableOnly ? wizChar : null);
                item.Identified = true;
                items.Add(item);
            }

            SetBackpack(baseChar.BasicAddress, items);
        }

        protected virtual List<Item> GetSuperItems(WizClass wizClass, WizAlignment alignment) { return new List<Item>(0); }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            int offset = iAddress * WizCharacter.SizeInBytes;
            CharacterOffsets offsets = Wiz123.Offsets;

            PartyInfo info = GetPartyInfo();
            if (offset + WizCharacter.SizeInBytes > info.Bytes.Length + 1)
                return false;

            WizClass wizClass = (WizClass)info.Bytes[offset + offsets.Class];

            byte[] bytes = new PackedFiveBitValues(18, 18, 18, 18, 18, 18).Bytes;   // Stats
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Stats, bytes.Length);
            bytes = new PackedFiveBitValues(0, 0, 0, 0, 0).Bytes;   // Saving throws
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.SavingThrows, bytes.Length);
            info.Bytes[offset + offsets.Condition] = (byte)WizCondition.Good;
            Global.SetInt16(info.Bytes, offset + offsets.Age, 14 * 52);
            Global.SetInt16(info.Bytes, offset + offsets.Level, 99);
            Global.SetInt16(info.Bytes, offset + offsets.LevelMod, 99);
            Global.SetInt16(info.Bytes, offset + offsets.CurrentHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.MaxHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.ArmorClass, -10);
            Global.SetInt16(info.Bytes, offset + offsets.LastArmorClass, -10);
            bytes = WizardryLong.GetBytes(99999999999);
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Gold, bytes.Length);
            bytes = WizardryLong.GetBytes(new WizCharacter().XPForLevel(wizClass, 99));
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

            List<Item> items = GetSuperItems(wizClass, (WizAlignment)info.Bytes[offset + offsets.Alignment]);

            foreach (WizItem item in items)
                item.Identified = true;

            SetBackpack(iAddress, items, true);

            return true;
        }

        public override Shops GetShopInfo()
        {
            if (!IsValid)
                return null;

            WizGameState state = GetGameState() as WizGameState;
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

        protected abstract WizEncounterInfo CreateEncounterInfo(WizGameState state, byte[] bytesEncounter, Point ptPartyPosition, int iRewardModifier, int offset = 0);

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            WizGameState state = GetGameState() as WizGameState;

            switch (state.Main)
            {
                case MainState.Opening:
                case MainState.Opening2:
                case MainState.MainMenu:
                case MainState.SelectSave:
                    return null;
                default:
                    break;
            }

            if (!state.InCombat && Game != GameNames.Wizardry4)
                return null;

            bool bWiz4 = Game == GameNames.Wizardry4;

            MemoryBytes mbEncounter = ReadOffset(Memory.EncounterInfo, WizEncounterInfo.Size);
            if (mbEncounter == null)
                return null;

            byte[] bytesEncounter = mbEncounter.Bytes;
            byte[] bytesPartyCombat = ReadOffset(Memory.CombatCharInfo, 16 * ReadByte(Memory.NumChars)).Bytes;
            byte[] bytesMod = bWiz4 ? ReadOffset(Wiz4.Memory.Group1Count, 12).Bytes : ReadOffset(Memory.EncounterRewardModifier, 2).Bytes;
            WizPartyInfo party = GetPartyInfo() as WizPartyInfo;
            byte[] bytesReward = bWiz4 ? ReadOffset(Wiz4.Memory.EncounterTreasureList, 16).Bytes : ReadOffset(Memory.RewardIndex, 1).Bytes;
            byte[] bytes = Global.Combine(bytesEncounter, party.Bytes, bytesPartyCombat, bytesMod, bytesReward);
            if (m_lastEncounterInfo != null && m_lastEncounterInfo.HasTreasure != (state.Main == MainState.Treasure))
                bForceNew = true;
            if (!bForceNew && m_lastEncounterInfo != null && Global.Compare(bytes, m_lastEncounterInfo.AllBytes))
                return m_lastEncounterInfo;

            m_lastEncounterInfo = CreateEncounterInfo(state, bytesEncounter, GetPartyPosition(), bytesMod[0]);
            m_lastEncounterInfo.Party = party;
            m_lastEncounterInfo.AllBytes = bytes;
            if (m_lastEncounterInfo is Wiz4EncounterInfo && state.Main == MainState.Treasure)
                ((Wiz4EncounterInfo) m_lastEncounterInfo).CreateSearchResults(ReadOffset(Wiz4.Memory.EncounterTreasureList, 16).Bytes);
            else
                m_lastEncounterInfo.CreateSearchResults(bytesReward[0]);
            return m_lastEncounterInfo;
        }

        public override bool SetEncounterInfo(EncounterInfo info)
        {
            if (!(info is WizEncounterInfo) || !IsValid)
                return false;

            WizEncounterInfo wi = info as WizEncounterInfo;

            WriteOffset(Memory.CombatCharInfo, wi.GetCharBytes());
            return WriteOffset(Memory.EncounterInfo, wi.GetBytes());
        }

        public override bool SetMonster(Monster monster)
        {
            return SetMonsterInfo(monster as WizMonster);
        }

        public virtual bool InitExternalMonsterList() { return false;  }

        public bool SetMonsterInfo(WizMonster monster)
        {
            if (!IsValid || monster == null)
                return false;

            // The wizardry 1 items are stored in 22-item sections, padded out to 1024 bytes each
            int iOffset = Memory.MonsterListDisk + ((monster.Index / 22) * 1024) + ((monster.Index % 22) * WizMonster.Size);

            if (WriteOffset(iOffset, monster.GetBytes()))
            {
                InitExternalMonsterList();
                return true;
            }

            return false;
        }

        public override ActiveSquares GetActiveSquares(MainForm form, bool bForce = false)
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

        protected virtual Wiz1234GameInfo CreateGameInfo() { return null; }

        public override GameInfo GetGameInfo()
        {
            if (!IsValid)
                return null;

            Wiz1234GameInfo info = CreateGameInfo();
            WizGameState state = GetGameState() as WizGameState;

            switch(state.Main)
            {
                case MainState.Opening:
                    return null;
                default:
                    break;
            }

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
            if (!(infoOld is Wiz1234GameInfo))
                return GetGameInfo();

            Wiz1234GameInfo wizOld = infoOld as Wiz1234GameInfo;
            Wiz1234GameInfo wizNew = GetGameInfo() as Wiz1234GameInfo;

            if (wizNew == null)
                return null;

            if (Global.Compare(wizOld.Bytes, wizNew.Bytes))
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
            info.State = GetGameState() as WizGameState;

            MemoryBytes mbCA = ReadOffset(Memory.CreateAttributes, 16);
            if (mbCA == null || mbCA.Bytes == null)
                return null;

            byte[] bytes = mbCA.Bytes;
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

            return WriteOffset(Memory.CreateName, Encoding.ASCII.GetBytes(str.ToUpper()));
        }

        public override bool SkipIntroductions(int iTimeout = 5000)
        {
            bool bWiz5 = (Game == GameNames.Wizardry5);
            DateTime dtStart = DateTime.Now;
            bool bOut = false;
            int iAdvCount = bWiz5 ? 10 : 1;
            if (bWiz5)
                iTimeout += (iTimeout / 2);
            while ((DateTime.Now - dtStart).TotalMilliseconds < iTimeout)
            {
                WizGameState state = GetGameState() as WizGameState;
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
                            if (!bWiz5)  // Wiz5 doesn't seem to leave characters in the maze
                            {
                                try
                                {
                                    WizRosterFile roster = CreateRoster(true) as WizRosterFile;
                                    WizCharacter wizChar = WizCharacter.Create(Game, 0, roster.LoadCharBytes(0), 0, null, true);
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
                            if (iAdvCount-- < 1)
                                return true;
                            break;
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
            info.State = GetGameState() as WizGameState;
            info.Party = GetPartyInfo() as WizPartyInfo;;
            info.MapIndex = GetCurrentMapIndex();
            info.MapName = GetMapTitle(info.MapIndex).Title;
            return info;
        }

        public override List<BaseCharacter> GetCharacters()
        {
            WizEncounterInfo encounterInfo = null;
            if (GetGameState().InCombat)
                encounterInfo = GetEncounterInfo() as WizEncounterInfo;

            return GetCharacters(encounterInfo);
        }

        public List<BaseCharacter> GetCharacters(WizEncounterInfo encounterInfo)
        {
            PartyInfo pi = GetPartyInfo();
            if (pi == null)
                return null;

            List<BaseCharacter> chars = new List<BaseCharacter>(pi.NumChars);
            for (int i = 0; i < pi.NumChars; i++)
            {
                WizCharacter wizChar = WizCharacter.Create(Game, i, pi.Bytes, WizCharacter.SizeInBytes * i, encounterInfo);
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

        public override string PleaseFormPartyString { get { return "Please form a party and exit the inn."; } }

        public override CureAllInfo GetCureAllInfo(int iCasterIndex, int[] partyAddresses)
        {
            if (!IsValid)
                return null;

            if (iCasterIndex >= partyAddresses.Length)
                return null;

            WizCureAllInfo info = new WizCureAllInfo();
            WizPartyInfo party = GetPartyInfo() as WizPartyInfo;

            info.Conditions = new WizCondition[party.NumChars];
            info.HitPoints = new Int16[party.NumChars];
            info.HitPointsMax = new Int16[party.NumChars];
            for (int i = 0; i < partyAddresses.Length; i++)
            {
                info.Conditions[i] = (WizCondition)party.Bytes[partyAddresses[i] * party.CharacterSize + Wiz123.Offsets.Condition];
                info.HitPoints[i] = BitConverter.ToInt16(party.Bytes, partyAddresses[i] * party.CharacterSize + Wiz123.Offsets.CurrentHP);
                info.HitPointsMax[i] = BitConverter.ToInt16(party.Bytes, partyAddresses[i] * party.CharacterSize + Wiz123.Offsets.MaxHP);
            }

            int iCasterAddress = partyAddresses[iCasterIndex];
            info.CasterCondition = info.Conditions[iCasterAddress];
            info.CasterSpellPoints = new WizSpellPoints(party.Bytes, iCasterAddress * party.CharacterSize + Wiz123.Offsets.CurrentSP);
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

            WizCureAllInfo info = cureAll as WizCureAllInfo;
            int iCasterAddress = partyAddresses[iCasterIndex];
            byte[] bytesSP = info.CasterSpellPoints.GetBytes();
            WriteOffset(Memory.PartyInfo + (iCasterAddress * WizCharacter.SizeInBytes) + Wiz123.Offsets.CurrentSP, bytesSP);
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                WriteByte(Memory.PartyInfo + (partyAddresses[i] * WizCharacter.SizeInBytes + Wiz123.Offsets.Condition), (byte)info.Conditions[i]);
                WriteInt16(Memory.PartyInfo + (partyAddresses[i] * WizCharacter.SizeInBytes + Wiz123.Offsets.CurrentHP), info.HitPoints[i]);
            }
        }

        public override bool RefreshConditions()
        {
            WizGameState state = GetGameState() as WizGameState;
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

        protected virtual QuestInfo CreateQuestInfo() { return null; }

        protected virtual WizQuestData CreateQuestData() 
        {
            WizPartyInfo party = GetPartyInfo() as WizPartyInfo;
            if (party == null)
                return null;
            return new WizQuestData(party, GetLocation(), ReadOffset(Memory.FightMap, 80), GetGameState() as WizGameState);
        }

        public override QuestInfo GetQuestInfo(QuestInfo lastInfo = null, int iOverrideCharAddress = -1, bool bAllowSelectionDialog = false)
        {
            if (!IsValid)
                return null;

            QuestInfo info = CreateQuestInfo();
            QuestData data = CreateQuestData();

            if (info == null || data == null)
                return null;

            info.MapIndex = data.Location.MapIndex;
            MemoryStream ms = new MemoryStream();
            data.AddBytes(ms);
            ms.WriteByte((byte)iOverrideCharAddress);
            byte[] newBytes = ms.ToArray();

            if (lastInfo != null && Global.Compare(lastInfo.Bytes, newBytes))
                return lastInfo;    // Don't bother going through the lengthy SetQuests routine if nothing has changed

            info.SetQuests(data, iOverrideCharAddress);
            info.Bytes = newBytes;

            return info;
        }

        public override IEnumerable<Monster> GetMonsterList() { return Wiz1.Monsters; }

        public override bool AutoCombat()
        {
            WizGameState state = ReadWiz123GameState();

            switch (state.Main)
            {
                case MainState.ReceiveExp:
                case MainState.ReceiveGold:
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
                    bool bCanAttack = state.ActingCombatChar < 3 || Game == GameNames.Wizardry5;

                    if (!bCanAttack)
                        SendKeysToDOSBox(new Keys[] { Keys.P }, true);
                    else
                    {
                        WizEncounterInfo info = GetEncounterInfo() as WizEncounterInfo;
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

        public override string GetMapStrings(bool bRaw = false)
        {
            if (!IsValid)
                return String.Empty;

            // Wizardry strings are typically 16 bits per character
            MemoryBytes bytes = ReadOffset(Memory.Text, 10240);
            StringBuilder sb = new StringBuilder();

            if (bRaw)
            {
                int iCount = 0;
                // Add up to 200 printable characters
                for (int i = 0; i < bytes.Length; i += 2)
                {
                    if (bytes.Bytes[i] >= 32 && bytes.Bytes[i] < 128)
                    {
                        if (sb.Length > 0 && sb[sb.Length - 1] == ' ' && bytes.Bytes[i] == (int)' ')
                            continue;

                        sb.Append((char)bytes.Bytes[i]);
                        iCount++;
                        if (iCount > 200)
                            break;
                    }
                }
                sb.Replace("f` ", "");
                sb.Replace("C)AMP S)TATUS I)NSPECT P)ICK U)SE O)FF", "");
                if (sb.Length > 2 && sb[0] == 'V' && sb[1] == ' ')
                    sb.Remove(0, 2);
                return sb.ToString();
            }

            bool bNewline = false;
            for (int i = 0; i < bytes.Bytes.Length; i++)
            {
                if (bytes.Bytes[i] == 0)
                {
                    if (bNewline && (sb.Length == 0 || sb[sb.Length-1] != '\n'))
                        sb.Append("\r\n");
                    if (i < bytes.Bytes.Length - 1 && bytes.Bytes[i + 1] == 0)
                        bNewline = true;
                    continue;
                }

                if (bytes.Bytes[i] >= ' ' && bytes.Bytes[i] <= 128)
                {
                    sb.Append((char)bytes.Bytes[i]);
                    bNewline = false;
                }
            }

            // Wizardry full-justifies most of the text, which is not necessary for viewing
            while(sb.ToString().Contains("  "))
                sb.Replace("  ", " ");

            Global.FormatSentences(sb);

            sb.Replace(". ", ".  ");
            sb.Replace("! ", "!  ");
            sb.Replace("? ", "?  ");
            sb.Replace("f`d ", "f`d \r\n");
            sb.Replace("(y/n) ?", "(Y/N) ?\r\nY: \r\n");
            sb.Replace("  \r\n", "\r\n");

            string strText = sb.ToString().Trim();

            return String.Format("{0}\r\n\r\n{1}", strText, Global.Title(strText));
        }

        public override MapBytes GetCurrentMapBytes()
        {
            if (GetGameState().Main != MainState.Adventuring)
            {
                m_iCurrentMapAdventuringCount = 2;
                return null;    // The in-memory map only exists during the "Adventuring" state
            }

            if (m_iCurrentMapAdventuringCount > 0)
            {
                // Don't try to read the map the instant it becomes available in adventuring mode; it might be in a transition state
                m_iCurrentMapAdventuringCount--;
                return null;
            }

            MemoryBytes bytes = ReadOffset(Memory.Map, 1024);
            if (bytes == null)
                return null;

            // Wizardry often has trash in the map data space during special events; this tries to avoid
            // returning the trash as valid data at least some of the time.
            if (m_bytesLastLiveMap != null && Global.NumDifferences(bytes.Bytes, m_bytesLastLiveMap, 64) > 32)
                return new MapBytes(m_bytesLastLiveMap, 20, 20);

            return new MapBytes(bytes, 20, 20); 
        }

        public override MapData CreateLiveMapData(MapBytes mb)
        {
            if (mb == null || mb.Bytes == null)
                return null;
            WizMapData data = new WizMapData(Game, 0, mb.Bytes, 0, false, true);
            data.LiveOnly = true;
            return data;
        }

        public override string ReplaceNoteStrings(string str)
        {
            if (!IsValid)
                return str;

            StringBuilder sbResult = new StringBuilder(str);

            if (str.Contains("EncounterMonsters"))
            {
                WizEncounterInfo encounter = GetEncounterInfo() as WizEncounterInfo;
                if (encounter == null)
                    return str;
                StringBuilder sb = new StringBuilder();
                if (str.Contains("$uniqueEncounterMonsters") || str.Contains("$allEncounterMonsters"))
                {
                    Dictionary<string, MonsterCount> dict = new Dictionary<string, MonsterCount>();
                    foreach (WizEncounterGroup group in encounter.Groups)
                    {
                        if (group == null || group.Monster == null || group.Monster.ProperName == null || group.NumAlive < 1)
                            continue;
                        if (!dict.ContainsKey(group.Monster.ProperName))
                            dict.Add(group.Monster.ProperName, new MonsterCount(group.Monster.ProperName, group.NumAlive));
                        else
                            dict[group.Monster.ProperName].Count += group.NumAlive;
                    }
                    sbResult.Replace("$allEncounterMonsters", MonsterCount.MonsterList(dict));
                    sbResult.Replace("$uniqueEncounterMonsters", MonsterCount.MonsterListUnique(dict));
                }
            }
            return sbResult.ToString();
        }

        public override TrapInfo CreateTrapInfo(int iTrap)
        {
            WizTrapInfo.WizTrap trap = (WizTrapInfo.WizTrap)iTrap;
            if (trap == WizTrapInfo.WizTrap.MiscTrap)
                return null;    // Not a real trap type

            return new WizTrapInfo(trap);
        }
    }
}
