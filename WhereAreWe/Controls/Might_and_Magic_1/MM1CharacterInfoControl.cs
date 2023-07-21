using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WhereAreWe
{
    public partial class MM1CharacterInfoControl : MMCharacterInfoControl
    {
        public MM1CharacterInfoControl(IMain main) : base(main)
        {
            InitializeComponent();
            m_char = new MM1Character();

            FindEditableAttributes();
        }

        public override void SetInfo(PartyInfo info, int iIndex, GameInfo gameInfo, EncounterInfo encounterInfo = null)
        {
            if (info != null && info.Bytes.Length < (info.Addresses[iIndex] + 1) * CharacterSize)
                return;

            if (info is MM1PartyInfo)
            {
                m_bytes = new byte[info.CharacterSize];
                Buffer.BlockCopy(info.Bytes, info.Addresses[iIndex] * info.CharacterSize, m_bytes, 0, info.CharacterSize);
                m_char.SetFromBytes(0, m_bytes, gameInfo, encounterInfo);
                m_iCharacterIndex = iIndex;
                m_iCharacterAddress = info.Addresses[iIndex];
                m_iCharacterPosition = iIndex;
                ((MM1Character)m_char).Address = m_iCharacterAddress;
            }
            else
            {
                m_iCharacterAddress = -1;
                m_iCharacterIndex = -1;
                m_iCharacterPosition = -1;
            }

            UpdateUI();
        }

        public override TriggerControl GetTriggerControl(TriggerEntity entity, string strVal)
        {
            switch (entity)
            {
                case TriggerEntity.Gems: return new TriggerControl(labelGems);
                case TriggerEntity.Gold: return new TriggerControl(labelGold);
                case TriggerEntity.Food: return new TriggerControl(labelFood);
                case TriggerEntity.SpellLevel: return new TriggerControl(labelSpellLevel);
                default: return base.GetTriggerControl(entity, strVal);
            }
        }

        public override void UpdateUI()
        {
            MM1Character mm1Char = m_char as MM1Character;

            if (mm1Char.Level == null)
                return;

            m_commonCtrls.labelLevel.Text = mm1Char.BasicInfoString;
            m_commonCtrls.labelAC.Text = mm1Char.ArmorClass.ToString();
            MMCommonControls.labelAccuracy.Text = mm1Char.Accuracy.ToString();
            ListViewSelectionSaver savePack = new ListViewSelectionSaver(m_commonCtrls.lvBackpack);
            ListViewSelectionSaver saveEquip = new ListViewSelectionSaver(m_commonCtrls.lvEquipped);

            m_commonCtrls.lvEquipped.BeginUpdate();
            m_commonCtrls.lvBackpack.BeginUpdate();
            for (int i = 0; i < 6; i++)
            {
                if (mm1Char.Inventory.BackpackItems.Count > i)
                    SetBackpackLVI(i, mm1Char.Inventory.BackpackItems[i], mm1Char);
                else
                    SetBackpackLVI(i, null, mm1Char);

                if (mm1Char.Inventory.EquippedItems.Count > i)
                    SetEquippedLVI(i, mm1Char.Inventory.EquippedItems[i], mm1Char);
                else
                    SetEquippedLVI(i, null, mm1Char);
            }
            Global.FitSingleColumn(m_commonCtrls.lvBackpack);
            Global.FitSingleColumn(m_commonCtrls.lvEquipped);

            UpdateHeaders();
            savePack.Restore();
            saveEquip.Restore();

            m_commonCtrls.lvEquipped.EndUpdate();
            m_commonCtrls.lvBackpack.EndUpdate();

            SetResistances(mm1Char.GetResistances());
            m_commonCtrls.labelCondition.Text = MM1Character.ConditionString(mm1Char.Condition, true);
            m_tipCondition.SetToolTip(m_commonCtrls.labelCondition, MM1Character.ConditionDescription(mm1Char.Condition));
            m_tipCondition.ShowAlways = true;
            m_tipCondition.AutoPopDelay = 32000;

            MMCommonControls.labelEndurance.Text = mm1Char.Endurance.ToString();
            m_commonCtrls.labelExp.Text = mm1Char.ExperienceString;
            labelFood.Text = String.Format("{0}", mm1Char.Food);
            labelGems.Text = String.Format("{0}", mm1Char.Gems);
            labelGold.Text = String.Format("{0}", mm1Char.Gold);
            m_commonCtrls.labelHP.Text = mm1Char.HitPoints.ToString();
            MMCommonControls.labelIntellect.Text = mm1Char.Intellect.ToString();
            MMCommonControls.labelLuck.Text = mm1Char.Luck.ToString();
            MMCommonControls.labelMight.Text = mm1Char.Might.ToString();
            MMCommonControls.labelPersonality.Text = mm1Char.Personality.ToString();
            m_commonCtrls.labelSP.Text = mm1Char.SpellPoints.ToString();
            MMCommonControls.labelSpeed.Text = mm1Char.Speed.ToString();
            labelSpellLevel.Text = mm1Char.SpellLevel.ToString();
            m_commonCtrls.labelMelee.Text = mm1Char.MeleeDamageString;
            MMCommonControls.labelRanged.Text = mm1Char.RangedDamageString;
            labelPrimaryQuest.Text = MM1Character.QuestString(mm1Char.MainQuest, mm1Char.AstralQuest);
            labelSideQuest.Text = MM1Character.QuestString(mm1Char.Quest, mm1Char.CastleQuestStatus);
            MMCommonControls.labelThievery.Text = String.Format("{0}", mm1Char.Thievery);
            labelSign.Text = MM1Character.SignString(mm1Char.Sign);
            labelWorthy.Text = mm1Char.WorthyString;

            m_commonCtrls.llCureAll.Visible = (mm1Char.Class == MM1Class.Cleric || mm1Char.Class == MM1Class.Paladin) && Properties.Settings.Default.EnableMemoryWrite;
        }

        protected override CheatMenuFlags PrepareCheatMenu(Control label, CheatMenuFlags flags = CheatMenuFlags.None)
        {
            CheatMenuFlags ret = base.PrepareCheatMenu(label, flags);
            if (ret != CheatMenuFlags.None)
                return ret;   // common control handled by base

            if (!(label is EditableAttributeLabel || label is MMItemLabel || label is ListView))
                return CheatMenuFlags.None;

            CheatMenuFlags menuFlags = CheatMenuFlags.AllNonlevel;

            m_cheatOffsets = null;

            if (label == labelSpellLevel)
            {
                m_cheatType = AttributeType.TwoUInt8;
                m_cheatOffsets = new int[] { m_char.Offsets.SpellLevel, m_char.Offsets.SpellLevelMod };
            }
            if (label == labelWorthy)
            {
                m_cheatType = AttributeType.MM1Worthy;
                m_cheatOffsets = new int[] { m_char.Offsets.Awards + 14 };
                menuFlags = CheatMenuFlags.Edit;
            }
            if (label == labelSideQuest)
            {
                m_cheatType = AttributeType.MM1Castle;
                m_cheatOffsets = new int[6];
                for(int iCastle = 0; iCastle < 6; iCastle++)
                    m_cheatOffsets[iCastle] = m_char.Offsets.Awards + 8 + iCastle;
                menuFlags = CheatMenuFlags.Edit;
            }
            if (label == labelPrimaryQuest)
            {
                m_cheatType = AttributeType.MM1Main;
                m_cheatOffsets = new int[] { m_char.Offsets.Awards + 3, m_char.Offsets.Awards + 16 };
                menuFlags = CheatMenuFlags.Edit;
            }
            else if (label == labelFood)
            {
                m_cheatType = AttributeType.UInt8;
                m_cheatOffsets = new int[] { m_char.Offsets.Food };
            }
            else if (label == labelGold)
            {
                m_cheatType = AttributeType.UInt24;
                m_cheatOffsets = new int[] { m_char.Offsets.Gold };
            }
            else if (label == labelGems)
            {
                m_cheatType = AttributeType.UInt16;
                m_cheatOffsets = new int[] { m_char.Offsets.Gems };
            }
            else if (m_cheatOffsets == null)
            {
                m_cheatType = AttributeType.Item;
                menuFlags = CheatMenuFlags.Edit;

                if (label == m_commonCtrls.lvBackpack && m_commonCtrls.lvBackpack.FocusedItem != null)
                {
                    int iIndex = m_commonCtrls.lvBackpack.FocusedItem.Index;
                    if (flags == CheatMenuFlags.AddNew)
                        iIndex = m_char.FirstEmptyBackpackIndex;
                    else
                    {
                        InventoryItemTag tag = m_commonCtrls.lvBackpack.FocusedItem.Tag as InventoryItemTag;
                        if (tag != null)
                            iIndex = tag.MemoryIndex;
                        else
                            iIndex = m_char.FirstEmptyBackpackIndex;
                    }
                    m_cheatOffsets = new int[] { m_char.Offsets.BackpackBases + iIndex, m_char.Offsets.BackpackCharges + iIndex };
                }
                else if (label == m_commonCtrls.lvEquipped && m_commonCtrls.lvEquipped.FocusedItem != null)
                {
                    int iIndex = m_commonCtrls.lvEquipped.FocusedItem.Index;
                    InventoryItemTag tag = m_commonCtrls.lvEquipped.FocusedItem.Tag as InventoryItemTag;
                    if (tag != null)
                        iIndex = tag.MemoryIndex;
                    m_cheatOffsets = new int[] { m_char.Offsets.EquippedBases + iIndex, m_char.Offsets.EquippedCharges + iIndex };
                }
            }
            if (m_cheatOffsets == null)
                return CheatMenuFlags.None;

            return menuFlags;
        }
    }

    public class MM1Inventory : Inventory
    {
        public List<MM1Item> EquippedItems;
        public List<MM1Item> BackpackItems;

        public override List<Item> Items
        {
            get
            {
                List<Item> items = new List<Item>(EquippedItems.Count + BackpackItems.Count);
                for(int i = 0; i < EquippedItems.Count; i++)
                {
                    EquippedItems[i].WhereEquipped = EquipLocation.Slot1 + i;
                    items.Add(EquippedItems[i]);
                }
                for (int i = 0; i < BackpackItems.Count; i++)
                {
                    BackpackItems[i].WhereEquipped = EquipLocation.None;
                    items.Add(BackpackItems[i]);
                }
                return items;
            }
            set
            {
                List<MM1Item> equip = new List<MM1Item>(6);
                List<MM1Item> backpack = new List<MM1Item>(6);
                foreach (Item item in value)
                {
                    if (!(item is MM1Item))
                        continue;
                    if (item.WhereEquipped == EquipLocation.None && equip.Count < 6)
                        backpack.Add(item as MM1Item);
                    else if (backpack.Count < 6)
                        equip.Add(item as MM1Item);
                }

                EquippedItems = equip;
                BackpackItems = backpack;
            }
        }

        public override int NumBackpackItems { get { return BackpackItems.Count; } }

        public MM1Inventory(byte[] bytes, int index)
        {
            EquippedItems = new List<MM1Item>(6);
            BackpackItems = new List<MM1Item>(6);
            for (int i = 0; i < 6; i++)
            {
                MM1Item item = MM1.ItemList.Value.GetItem(bytes[index+i], i);
                if (item != null && item.Index != (int) MM1ItemIndex.Empty)
                {
                    item.m_iChargesCurrent = bytes[index + i + 12];
                    EquippedItems.Add(item);
                }
            }
            for (int i = 6; i < 12; i++)
            {
                MM1Item item = MM1.ItemList.Value.GetItem(bytes[index + i], i - 6);
                if (item != null && item.Index != (int)MM1ItemIndex.Empty)
                {
                    item.m_iChargesCurrent = bytes[index + i + 12];
                    BackpackItems.Add(item);
                }
            }
        }

        public bool HasItem(MM1ItemIndex itemWanted)
        {
            foreach (MM1Item item in EquippedItems)
                if (item.Index == (int)itemWanted)
                    return true;
            foreach (MM1Item item in BackpackItems)
                if (item.Index == (int)itemWanted)
                    return true;
            return false;
        }

        public void SetBytes(byte[] bytes, int index)
        {
            for(int i = 0; i < 24; i++)
                bytes[i] = 0;

            int iIndexItem = 0;
            foreach(MM1Item item in EquippedItems)
            {
                bytes[index + iIndexItem] = (byte) item.Index;
                bytes[index + iIndexItem + 12] = item.m_iChargesCurrent;
                iIndexItem++;
            }
            iIndexItem = 6;
            foreach(MM1Item item in BackpackItems)
            {
                bytes[index + iIndexItem] = (byte) item.Index;
                bytes[index + iIndexItem + 12] = item.m_iChargesCurrent;
                iIndexItem++;
            }
        }
    }

    public enum MMSex
    {
        None = 0,
        Male = 1,
        Female = 2,
    }

    public enum MM1AlignmentValue
    {
        None = 0,
        Good = 1,
        Neutral = 2,
        Evil = 3
    }

    public enum MM1Race
    {
        None = 0,
        Human = 1,
        Elf = 2,
        Dwarf = 3,
        Gnome = 4,
        HalfOrc = 5
    }

    public enum MM1Class
    {
        None = 0,
        Knight = 1,
        Paladin = 2,
        Archer = 3,
        Cleric = 4,
        Sorcerer = 5,
        Robber = 6
    }

    [Flags]
    public enum MM1Condition
    {
        Good = 0x00,
        Asleep = 0x01,
        Blinded = 0x02,
        Silenced = 0x04,
        Diseased = 0x08,
        Poisoned = 0x10,
        Paralyzed = 0x20,
        Unconscious = 0x40,
        Stone = 0xA0,
        Dead = 0xC0,
        Eradicated = 0xFF,
        SevereFlag = 0x80,
        UnableToCast = Asleep | Silenced | Paralyzed | Unconscious | SevereFlag
    }

    public class MM1Alignment
    {
        public MM1AlignmentValue Permanent;
        public MM1AlignmentValue Temporary;

        public MM1Alignment(byte[] bytes, int index)
        {
            Permanent = AlignmentFromByte(bytes[index]);
            Temporary = AlignmentFromByte(bytes[index + 1]);
        }

        public void SetBytes(byte[] bytes, int index)
        {
            bytes[index] = (byte)Permanent;
            bytes[index + 1] = (byte)Temporary;
        }

        public static MM1AlignmentValue AlignmentFromByte(byte b)
        {
            if (b >= (byte)MM1AlignmentValue.Good && b <= (byte)MM1AlignmentValue.Evil)
                return (MM1AlignmentValue)b;
            return MM1AlignmentValue.None;
        }

        public override string ToString()
        {
            if (Permanent == Temporary)
                return MM1Character.AlignmentString(Permanent);
            return String.Format("{0} ({1})", MM1Character.AlignmentString(Temporary), MM1Character.AlignmentString(Permanent));
        }
    }

    public class MM1CastleQuests
    {
        public MM1CastleQuestFlags InspectronCompleted; // 117  Inspectron Quest completed bits 0-6, bit 7 - all quests completed at least once
        public MM1CastleQuestFlags HackerCompleted;     // 118  Hacker Quest completed bits 0-6
        public MM1CastleQuestFlags IronfistCompleted;   // 119  Ironfist Quest completed bits 0-6, bit 7 - all quests completed at least once
        public MM1CastleQuestFlags InspectronRewarded;  // 120  Inspectron Quest rewarded bits 0-6, bit 7 - all quests rewarded at least once
        public MM1CastleQuestFlags HackerRewarded;      // 121  Hacker Quest rewarded bits 0-6, bit 7 - Have been sent into the pit of peril at least once  
        public MM1CastleQuestFlags IronfistRewarded;    // 122  Ironfist Quest rewarded bits 0-6, bit 7 - all quests rewarded at least once

        public MM1CastleQuests(byte[] bytes, int index)
        {
            InspectronCompleted = (MM1CastleQuestFlags) bytes[index];
            HackerCompleted = (MM1CastleQuestFlags) bytes[index+1];
            IronfistCompleted = (MM1CastleQuestFlags) bytes[index+2];
            InspectronRewarded = (MM1CastleQuestFlags) bytes[index+3];
            HackerRewarded = (MM1CastleQuestFlags) bytes[index+4];
            IronfistRewarded = (MM1CastleQuestFlags) bytes[index+5];
        }

        public void SetBytes(byte[] bytes, int index)
        {
            bytes[index] = (byte) InspectronCompleted;
            bytes[index+1] = (byte) HackerCompleted;
            bytes[index+2] = (byte) IronfistCompleted;
            bytes[index+3] = (byte) InspectronRewarded;
            bytes[index+4] = (byte) HackerRewarded;
            bytes[index+5] = (byte) IronfistRewarded;
        }
    }

    public enum MM1QuestIndex
    {
        None = 0,
        Ironfist1 = 1,
        Ironfist2 = 2,
        Ironfist3 = 3,
        Ironfist4 = 4,
        Ironfist5 = 5,
        Ironfist6 = 6,
        Ironfist7 = 7,
        Inspectron1 = 8,
        Inspectron2 = 9,
        Inspectron3 = 10,
        Inspectron4 = 11,
        Inspectron5 = 12,
        Inspectron6 = 13,
        Inspectron7 = 14,
        Hacker1 = 15,
        Hacker2 = 16,
        Hacker3 = 17,
        Hacker4 = 18,
        Hacker5 = 19,
        Hacker6 = 20,
        Hacker7 = 21,
        Unknown = 254,
        FakeAlamar = 255
    }

    [Flags]
    public enum MM1MainQuestFlags
    {
        NotStarted = 0x00,
        ReceivedScroll = 0x01,
        DeliveredToAgar = 0x02,
        DeliveredToTelgoran = 0x04,
        FoundZom = 0x08,
        FoundZam = 0x10,
        FoundRubyWhistle = 0x20,
        FoundStronghold = 0x40,
        FoundCanine = 0x80,
        All = 0xff
    }

    [Flags]
    public enum MM1Sign
    {
        AnsweredQuestion = 0x80,
        RedThorac = 0x00,
        BlueOgram = 0x01,
        GreenBagar = 0x02,
        YellowLimra = 0x03,
        PurpleSagran = 0x04,
        OrangeOolak = 0x05,
        BlackDresidion = 0x06,
        WhiteDilithium = 0x07,
        SignMask = 0x07
    }

    [Flags]
    public enum MM1PrisonersFlags
    {
        None = 0x00,
        QuestStarted = 0x01,
        Dragadune = 0x02,
        Doom = 0x04,
        Alamar = 0x08,
        WhiteWolf = 0x10,
        BlackridgeNorth = 0x20,
        BlackridgeSouth = 0x40,
        QuestCompleted = 0x80,
        AllPrisoners = Dragadune | Doom | Alamar | WhiteWolf | BlackridgeNorth | BlackridgeSouth,
        All = 0xff,
    }

    [Flags]
    public enum MM1GreatBeastsFlags
    {
        None = 0x00,
        GreatSeaBeast = 0x01,
        Scorpion = 0x02,
        WingedBeast = 0x04,
        DarkRider = 0x08,
        All = GreatSeaBeast | Scorpion | WingedBeast | DarkRider
    }

    [Flags]
    public enum MM1CastleQuestFlags
    {
        None = 0x00,
        Quest1 = 0x01,
        Quest2 = 0x02,
        Quest3 = 0x04,
        Quest4 = 0x08,
        Quest5 = 0x10,
        Quest6 = 0x20,
        Quest7 = 0x40,
        All = 0x80,
    }

    [Flags]
    public enum MM1StatIncreaserFlags
    {
        None = 0x00,
        Endurance = 0x01,
        Personality = 0x02,
        Intellect = 0x04,
        Might = 0x08,
        Accuracy = 0x10,
        Speed = 0x20,
        Luck = 0x40,
        Worthy = 0x80,
        AllStats = 0x7f,
    }

    [Flags]
    public enum MM1AstralQuestFlags
    {
        None = 0x00,
        Projector1Visited = 0x01,
        Projector2Visited = 0x02,
        Projector3Visited = 0x04,
        Projector4Visited = 0x08,
        Projector5Visited = 0x10,
        AllProjectorsVisited = 0x1f,
        Unknown = 0x20,
        EnteredSheltem = 0x40,
        FinishedGame = 0x80,
        All = 0xff
    }

    public class MM1CharacterOffsets : CharacterOffsets
    {
        public override int Name             { get { return 0; } }
        public override int NameTerminator   { get { return 15; } }
        public override int Sex              { get { return 16; } }
        public override int Race             { get { return 19; } }
        public override int Alignment        { get { return 17; } }
        public override int Class            { get { return 20; } }
        public override int Stats            { get { return 21; } }
        public override int Intellect        { get { return 21; } }
        public override int Might            { get { return 23; } }
        public override int Personality      { get { return 25; } }
        public override int Endurance        { get { return 27; } }
        public override int Speed            { get { return 29; } }
        public override int Accuracy         { get { return 31; } }
        public override int Luck             { get { return 33; } }
        public override int ArmorClass       { get { return 60; } }
        public override int ArmorClassMod    { get { return 61; } }
        public override int Level            { get { return 35; } }
        public override int Age              { get { return 37; } }
        public override int AgeDays          { get { return 38; } }
        public override int Awards           { get { return 109; } }
        public override int AwardsLength     { get { return 17; } }
        public override int Inventory        { get { return 64; } }
        public override int InventoryLength  { get { return 24; } }
        public override int MagicResist      { get { return 88; } }
        public override int FireResist       { get { return 90; } }
        public override int ColdResist       { get { return 92; } }
        public override int ElecResist       { get { return 94; } }
        public override int AcidResist       { get { return 96; } }
        public override int FearResist       { get { return 98; } }
        public override int PoisonResist     { get { return 100; } }
        public override int SleepResist      { get { return 102; } }
        public override int Condition        { get { return 63; } }
        public override int ConditionLength  { get { return 1; } }
        public override int CurrentHP        { get { return 51; } }
        public override int MaxHP            { get { return 53; } }
        public override int MaxHPMod         { get { return 55; } }
        public override int CurrentSP        { get { return 43; } }
        public override int MaxSP            { get { return 45; } }
        public override int Food             { get { return 62; } }
        public override int Gems             { get { return 49; } }
        public override int Gold             { get { return 57; } }
        public override int Experience       { get { return 39; } }
        public override int SpellLevel       { get { return 47; } }
        public override int GoldLength       { get { return 3; } }
        public override int RosterPos        { get { return 126; } }
        public override int BackpackBases    { get { return 70; } }
        public override int BackpackCharges  { get { return 82; } }
        public override int EquippedBases    { get { return 64; } }
        public override int EquippedCharges  { get { return 76; } }
        public override int MeleeDamage      { get { return 104; } }
        public override int RangedDamage     { get { return 106; } }
        public override int Thievery         { get { return 108; } }
    }

    public class MM1Character : MMBaseCharacter
    {
        public string CharName;                     // 0-15, null terminated
        public MMSex Sex;                           // 16
        public MM1Alignment Alignment;              // 17-18
        public MM1Race Race;                        // 19
        public MM1Class Class;                      // 20
        public OneByteStat Intellect;               // 21-22
        public OneByteStat Might;                   // 23-24
        public OneByteStat Personality;             // 25-26
        public OneByteStat Endurance;               // 27-28
        public OneByteStat Speed;                   // 29-30
        public OneByteStat Accuracy;                // 31-32
        public OneByteStat Luck;                    // 33-34
        public OneByteStat Level;                   // 35-36
        public byte Age;                            // 37
        public byte RestCounter;                    // 38 (256 rests ages character one year)
        public uint Experience;                     // 39-42
        public MMSpellPoints SpellPoints;           // 43-46
        public OneByteStat SpellLevel;              // 47-48
        public UInt16 Gems;                         // 49-50
        public MMHitPoints HitPoints;               // 51-56
        public uint Gold;                           // 57-59
        public MMArmorClass ArmorClass;             // 60-61
        public byte Food;                           // 62
        public MM1Condition Condition;              // 63
        public MM1Inventory Inventory;              // 64-87
        public OneByteStat MagicResist;             // 88-89
        public OneByteStat FireResist;              // 90-91
        public OneByteStat ColdResist;              // 92-93
        public OneByteStat ElecResist;              // 94-95
        public OneByteStat AcidResist;              // 96-97
        public OneByteStat FearResist;              // 98-99
        public OneByteStat PoisonResist;            // 100-101
        public OneByteStat SleepResist;             // 102-103
        public MMDamage MeleeDamage;                // 104-105
        public MMDamage RangedDamage;               // 106-107
        public byte Thievery;                       // 108  Thievery
        public MM1QuestIndex Quest;                 // 109
        public byte PrisonersXP;                    // 110  Prisoners freed "properly * 32
        public byte Unknown111;                     // 111
        public MM1MainQuestFlags MainQuest;         // 112  Main quest progress
        public MM1PrisonersFlags Prisoners;         // 113  Bitfield for freed prisoners: Bits 0 1 2 3 4 5 6 7 = <set=quest possible> Dragadune Doom Alamar North South WW <set=quest completable>
        public MM1GreatBeastsFlags Beasts;          // 114  0x00 = killed none 0x01 = killed GREAT SEA BEAST 0x02 = SCORPION  0x08 = DARK RIDER 0x04 = WINGED BEAST
        public byte Unknown115;                     // 115
        public MM1Sign Sign;                        // 116  Character's sign
        public MM1CastleQuests CastleQuestStatus;   // 117-122
        public MM1StatIncreaserFlags IncreasersUsed;// 123  Permanent stat increasers used: bits 0-6:  END PER INT MGT ACY AGL LUC.  Bit 7 = deemed worthy
        public byte Unknown124;                     // 124
        public MM1AstralQuestFlags AstralQuest;     // 125  Bits: 0-4 astral projectors visited, bit 0x40 - entered SHELTEM in soul maze, bit 0x80 = finished game
        public byte PositionInRoster;               // 126  Position in roster
        public byte Unknown127;                     // 127

        public int Address = -1;
        public const int SizeInBytes = 128;

        public MM1Character()
        {
            Address = -1;
        }

        public override int BasicSP { get { return SpellPoints.CurrentSP; } }
        public override int BasicMaxSP { get { return SpellPoints.MaximumSP; } }
        public override int BasicHP { get { return HitPoints.Current; } }
        public override int BasicMaxHP { get { return HitPoints.TemporaryMaximum; } }
        public override long BasicMoney { get { return Gold; } }
        public override int BasicFood { get { return Food; } }
        public override int BasicMaxFood => 40;
        public override int BasicThievery { get { return Thievery; } }

        public override bool KnowsSpell(Spell spell)
        {
            if (spell == null)
                return false;
            return KnowsSpell(spell.Type, spell.Level, spell.Number);
        }

        public override bool KnowsSpell(SpellType type, int level, int number)
        {
            if (!IsCaster)
                return false;
            switch (Class)
            {
                case MM1Class.Archer:
                case MM1Class.Sorcerer:
                    if (type != SpellType.Sorcerer)
                        return false;
                    break;
                case MM1Class.Paladin:
                case MM1Class.Cleric:
                    if (type != SpellType.Cleric)
                        return false;
                    break;
            }
            return (level <= SpellLevel.Temporary);
        }

        public static MM1Character Create(byte[] bytes, int iIndex = 0, bool bRosterFile = false)
        {
            if (bytes == null || bytes.Length < iIndex + (SizeInBytes - (bRosterFile ? 1 : 0)))
                return null;
            MM1Character character = new MM1Character();
            character.SetFromBytes(bytes, iIndex, bRosterFile);
            return character;
        }

        public override CharacterOffsets Offsets { get { return MM1.Offsets; } }

        public override ResistanceValue[] GetResistances()
        {
            return new ResistanceValue[] {
                new ResistanceValue(GenericResistanceFlags.Magic, MagicResist.Permanent, MagicResist.Bonus),
                new ResistanceValue(GenericResistanceFlags.Fire, FireResist.Permanent, FireResist.Bonus),
                new ResistanceValue(GenericResistanceFlags.Cold, ColdResist.Permanent, ColdResist.Bonus),
                new ResistanceValue(GenericResistanceFlags.Electricity, ElecResist.Permanent, ElecResist.Bonus),
                new ResistanceValue(GenericResistanceFlags.Acid, AcidResist.Permanent, AcidResist.Bonus),
                new ResistanceValue(GenericResistanceFlags.Fear, FearResist.Permanent, FearResist.Bonus),
                new ResistanceValue(GenericResistanceFlags.Poison, PoisonResist.Permanent, PoisonResist.Bonus),
                new ResistanceValue(GenericResistanceFlags.Sleep, SleepResist.Permanent, SleepResist.Bonus),
            };
        }

        public override int CharacterSize { get { return SizeInBytes; } }
        public override Inventory BasicInventory { get { return Inventory as Inventory; } }

        public static byte[] GetInventoryCharBytes()
        {
            // The bytes for a Level 1 Good Human Knight named "Inventory" with no items
            return new byte[] {
                0x49, 0x4E, 0x56, 0x45, 0x4E, 0x54, 0x4F, 0x52, 0x59, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x01, 0x01, 0x01, 0x01, 0x01, 0x0D, 0x0D, 0x10, 0x10, 0x0B, 0x0B, 0x09, 0x09, 0x0A, 0x0A, 0x0F,
                0x0F, 0x0B, 0x0B, 0x01, 0x01, 0x12, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x0C, 0x00, 0x0C, 0x00, 0x0C, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0A, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x46, 0x46, 0x00, 0x00, 0x19, 0x19, 0x00, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
        }

        public override int BasicAddress { get { return Address; } }

        public override void Serialize(Stream stream)
        {
            byte[] bytes = new byte[CharacterSize];

            for (int i = 0; i <= Offsets.NameTerminator; i++)
                bytes[i] = 0x00;
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(CharName), 0, bytes, 0, CharName.Length);
            bytes[Offsets.Sex] = (byte)Sex;
            Alignment.SetBytes(bytes, Offsets.Alignment);
            Alignment = new MM1Alignment(bytes, Offsets.Alignment);
            bytes[Offsets.Race] = (byte)Race;
            bytes[Offsets.Class] = (byte)Class;
            Intellect.SetBytes(bytes, Offsets.Intellect);
            Might.SetBytes(bytes, Offsets.Might);
            Personality.SetBytes(bytes, Offsets.Personality);
            Endurance.SetBytes(bytes, Offsets.Endurance);
            Speed.SetBytes(bytes, Offsets.Speed);
            Accuracy.SetBytes(bytes, Offsets.Accuracy);
            Luck.SetBytes(bytes, Offsets.Luck);
            Level.SetBytes(bytes, Offsets.Level);
            bytes[Offsets.Age] = Age;
            bytes[Offsets.AgeDays] = RestCounter;
            Buffer.BlockCopy(BitConverter.GetBytes(Experience), 0, bytes, Offsets.Experience, 4);
            SpellPoints.SetBytes(bytes, Offsets.CurrentSP);
            SpellLevel.SetBytes(bytes, Offsets.SpellLevel);
            Buffer.BlockCopy(BitConverter.GetBytes(Gems), 0, bytes, Offsets.Gems, 2);
            HitPoints.SetBytes(bytes, Offsets.CurrentHP);
            Buffer.BlockCopy(BitConverter.GetBytes(Gold), 0, bytes, Offsets.Gold, Offsets.GoldLength); // only uses 3 of the 4 bytes
            ArmorClass.SetBytes(bytes, Offsets.ArmorClass);
            bytes[Offsets.Food] = Food;
            bytes[Offsets.Condition] = (byte)Condition;
            Inventory.SetBytes(bytes, Offsets.Inventory);
            MagicResist.SetBytes(bytes, Offsets.MagicResist);
            FireResist.SetBytes(bytes, Offsets.FireResist);
            ColdResist.SetBytes(bytes, Offsets.ColdResist);
            ElecResist.SetBytes(bytes, Offsets.ElecResist);
            AcidResist.SetBytes(bytes, Offsets.AcidResist);
            FearResist.SetBytes(bytes, Offsets.FearResist);
            PoisonResist.SetBytes(bytes, Offsets.PoisonResist);
            SleepResist.SetBytes(bytes, Offsets.SleepResist);
            MeleeDamage.SetBytes(bytes, Offsets.MeleeDamage);
            RangedDamage.SetBytes(bytes, Offsets.RangedDamage);
            bytes[Offsets.Thievery] = Thievery;
            bytes[Offsets.Awards] = (byte)Quest;
            bytes[Offsets.Awards+1] = PrisonersXP;
            bytes[Offsets.Awards+2] = Unknown111;
            bytes[Offsets.Awards+3] = (byte)MainQuest;
            bytes[Offsets.Awards+4] = (byte)Prisoners;
            bytes[Offsets.Awards+5] = (byte)Beasts;
            bytes[Offsets.Awards+6] = Unknown115;
            bytes[Offsets.Awards+7] = (byte)Sign;
            CastleQuestStatus.SetBytes(bytes, Offsets.Awards + 8);
            bytes[Offsets.Awards+14] = (byte)IncreasersUsed;
            bytes[Offsets.Awards+15] = Unknown124;
            bytes[Offsets.Awards+16] = (byte)AstralQuest;
            bytes[Offsets.Awards+17] = PositionInRoster;
            bytes[Offsets.Awards+18] = Unknown127;

            stream.Write(bytes, 0, CharacterSize);
        }

        public override void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info, EncounterInfo encounterInfo = null, bool bFromRosterFile = false, byte[] itemTable = null)
        {
            int iSize = bFromRosterFile ? CharacterSize-1 : CharacterSize;
            if (stream.Length < iSize)
                return;

            RawBytes = new byte[iSize];
            stream.Read(RawBytes, 0, iSize);

            int iEnd = Offsets.Name;
            while (RawBytes[iEnd] != 0x00)
            {
                iEnd++;
                if (iEnd >= Offsets.NameTerminator)
                    break;
            }
            CharName = Encoding.ASCII.GetString(RawBytes, Offsets.Name, iEnd);
            Sex = SexFromByte(RawBytes[Offsets.Sex]);
            Alignment = new MM1Alignment(RawBytes, Offsets.Alignment);
            Race = RaceFromByte(RawBytes[Offsets.Race]);
            Class = ClassFromByte(RawBytes[Offsets.Class]);
            Intellect = new OneByteStat(RawBytes, Offsets.Intellect);
            Might = new OneByteStat(RawBytes, Offsets.Might);
            Personality = new OneByteStat(RawBytes, Offsets.Personality);
            Endurance = new OneByteStat(RawBytes, Offsets.Endurance);
            Speed = new OneByteStat(RawBytes, Offsets.Speed);
            Accuracy = new OneByteStat(RawBytes, Offsets.Accuracy);
            Luck = new OneByteStat(RawBytes, Offsets.Luck);
            Level = new OneByteStat(RawBytes, Offsets.Level);
            Age = RawBytes[Offsets.Age];
            RestCounter = RawBytes[Offsets.AgeDays];
            Experience = BitConverter.ToUInt32(RawBytes, Offsets.Experience);
            SpellPoints = new MMSpellPoints(RawBytes, Offsets.CurrentSP);
            SpellLevel = new OneByteStat(RawBytes, Offsets.SpellLevel);
            Gems = BitConverter.ToUInt16(RawBytes, Offsets.Gems);
            HitPoints = new MMHitPoints(RawBytes, Offsets.CurrentHP);
            Gold = BitConverter.ToUInt32(new byte[] { RawBytes[Offsets.Gold], RawBytes[Offsets.Gold + 1], RawBytes[Offsets.Gold + 2], 0 }, 0); // only 3 bytes, so pad the fourth
            ArmorClass = new MMArmorClass(RawBytes, Offsets.ArmorClass);
            Food = RawBytes[Offsets.Food];
            Condition = (MM1Condition)RawBytes[Offsets.Condition];
            Inventory = new MM1Inventory(RawBytes, Offsets.Inventory);
            MagicResist = new OneByteStat(RawBytes, Offsets.MagicResist);
            FireResist = new OneByteStat(RawBytes, Offsets.FireResist);
            ColdResist = new OneByteStat(RawBytes, Offsets.ColdResist);
            ElecResist = new OneByteStat(RawBytes, Offsets.ElecResist);
            AcidResist = new OneByteStat(RawBytes, Offsets.AcidResist);
            FearResist = new OneByteStat(RawBytes, Offsets.FearResist);
            PoisonResist = new OneByteStat(RawBytes, Offsets.PoisonResist);
            SleepResist = new OneByteStat(RawBytes, Offsets.SleepResist);
            MeleeDamage = new MMDamage(RawBytes, Offsets.MeleeDamage);
            RangedDamage = new MMDamage(RawBytes, Offsets.RangedDamage);
            Thievery = RawBytes[Offsets.Thievery];
            Quest = QuestFromByte(RawBytes[Offsets.Awards]);
            PrisonersXP = RawBytes[Offsets.Awards + 1];
            Unknown111 = RawBytes[Offsets.Awards + 2];
            MainQuest = (MM1MainQuestFlags)RawBytes[Offsets.Awards + 3];
            Prisoners = (MM1PrisonersFlags)RawBytes[Offsets.Awards + 4];
            Beasts = (MM1GreatBeastsFlags)RawBytes[Offsets.Awards + 5];
            Unknown115 = RawBytes[Offsets.Awards + 6];
            Sign = (MM1Sign)RawBytes[Offsets.Awards + 7];
            CastleQuestStatus = new MM1CastleQuests(RawBytes, Offsets.Awards + 8);
            IncreasersUsed = (MM1StatIncreaserFlags)RawBytes[Offsets.Awards + 14];
            Unknown124 = RawBytes[Offsets.Awards + 15];
            AstralQuest = (MM1AstralQuestFlags)RawBytes[Offsets.Awards + 16];
            PositionInRoster = RawBytes[Offsets.Awards + 17];
            if (iSize > 127)
                Unknown127 = RawBytes[Offsets.Awards + 18];
        }

        public override string Name { get { return CharName; } }

        public override string CombatInfo
        {
            get
            {
                return String.Format("{0}{1} {2}/{3}",
                    Condition == MM1Condition.Good ? "" : "*",
                    CharName,
                    HitPoints.Current,
                    HitPoints.TemporaryMaximum);
            }
        }

        public override long NeedsXP { get { return XPForNextLevel - Experience; } }
        public override long XPForNextLevel { get { return XPForLevel(Class, Level.Permanent+1); } }
        public override long BasicExperience { get { return Experience; } }

        public override long XPForLevel(GenericClass mmClass, int iLevel)
        {
            return XPForLevel(ClassForGeneric(mmClass), iLevel);
        }

        public static long XPForLevel(MM1Class mm1Class, int iLevel)
        {
            if (iLevel < 2)
                return 0;   // No XP required to be level 1

            int iStart = 1500;
            switch (mm1Class)
            {
                case MM1Class.Archer:
                case MM1Class.Paladin:
                case MM1Class.Sorcerer:
                    iStart = 2000;
                    break;
                default:
                    break;
            }

            long iExpForNext = 0;
            if (iLevel < 2)
                return iStart;
            if (iLevel < 9)
                iExpForNext = (long)(iStart * (1 << (iLevel-2)));
            else
                iExpForNext = (iStart * 128) + (iLevel - 9) * (iStart * 100);

            iExpForNext++;

            if (iExpForNext < 0)
                return 0;

            return iExpForNext;
        }

        public static MMSex SexFromByte(byte b)
        {
            if (b >= (byte)MMSex.Male && b <= (byte)MMSex.Female)
                return (MMSex)b;
            return MMSex.None;
        }

        public static MM1Race RaceFromByte(byte b)
        {
            if (b >= (byte)MM1Race.Human && b <= (byte)MM1Race.HalfOrc)
                return (MM1Race)b;
            return MM1Race.None;
        }

        public static MM1Class ClassFromByte(byte b)
        {
            if (b >= (byte)MM1Class.Knight && b <= (byte)MM1Class.Robber)
                return (MM1Class)b;
            return MM1Class.None;
        }

        public static MM1QuestIndex QuestFromByte(byte b)
        {
            if ((b >= (byte) MM1QuestIndex.None && b <= (byte) MM1QuestIndex.Hacker7) || (b == (byte) MM1QuestIndex.FakeAlamar))
                return (MM1QuestIndex) b;
            return MM1QuestIndex.Unknown;
        }

        public static string SexString(MMSex sex)
        {
            switch (sex)
            {
                case MMSex.Male: return "Male";
                case MMSex.Female: return "Female";
                default: return "None";
            }
        }

        public static string AlignmentString(MM1AlignmentValue align)
        {
            switch (align)
            {
                case MM1AlignmentValue.Good: return "Good";
                case MM1AlignmentValue.Neutral: return "Neutral";
                case MM1AlignmentValue.Evil: return "Evil";
                default: return "None";
            }
        }

        public static string RaceString(MM1Race race)
        {
            switch (race)
            {
                case MM1Race.Dwarf: return "Dwarf";
                case MM1Race.Elf: return "Elf";
                case MM1Race.Gnome: return "Gnome";
                case MM1Race.HalfOrc: return "Half-Orc";
                case MM1Race.Human: return "Human";
                default: return "None";
            }
        }
        public static string ClassString(MM1Class classenum)
        {
            switch (classenum)
            {
                case MM1Class.Archer: return "Archer";
                case MM1Class.Cleric: return "Cleric";
                case MM1Class.Knight: return "Knight";
                case MM1Class.Paladin: return "Paladin";
                case MM1Class.Robber: return "Robber";
                case MM1Class.Sorcerer: return "Sorcerer";
                default: return "None";
            }
        }

        public static string ConditionString(MM1Condition cond, bool bIncludeGood)
        {
            if (cond == MM1Condition.Good)
                return bIncludeGood ? "Good" : "";

            if (cond == MM1Condition.Eradicated)
                return "Eradicated";

            StringBuilder sb = new StringBuilder();

            if (cond.HasFlag(MM1Condition.SevereFlag))
            {
                if (cond.HasFlag(MM1Condition.Dead))
                    sb.Append("Dead, ");
                if (cond.HasFlag(MM1Condition.Stone))
                    sb.Append("Stone, ");
            }
            else
            {
                if (cond.HasFlag(MM1Condition.Unconscious))
                    sb.Append("Unconscious, ");
                if (cond.HasFlag(MM1Condition.Paralyzed))
                    sb.Append("Paralyzed, ");
                if (cond.HasFlag(MM1Condition.Poisoned))
                    sb.Append("Poisoned, ");
                if (cond.HasFlag(MM1Condition.Diseased))
                    sb.Append("Diseased, ");
                if (cond.HasFlag(MM1Condition.Silenced))
                    sb.Append("Silenced, ");
                if (cond.HasFlag(MM1Condition.Blinded))
                    sb.Append("Blinded, ");
                if (cond.HasFlag(MM1Condition.Asleep))
                    sb.Append("Asleep, ");
            }

            return Global.Trim(sb).ToString();
        }

        public static string QuestString(MM1QuestIndex quest, MM1CastleQuests status)
        {
            switch (quest)
            {
                case MM1QuestIndex.None: return "None";
                case MM1QuestIndex.Ironfist1: return "Ironfist-Find the Stronghold in Raven's Wood" + (status.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest1) ? " (turn in!)" : "");
                case MM1QuestIndex.Ironfist2: return "Ironfist-Find Lord Kilburn" + (status.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest2) ? " (turn in!)" : "");
                case MM1QuestIndex.Ironfist3: return "Ironfist-Discover the Secret of Portsmith" + (status.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest3) ? " (turn in!)" : "");
                case MM1QuestIndex.Ironfist4: return "Ironfist-Find the Pirates Secret Cove" + (status.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest4) ? " (turn in!)" : "");
                case MM1QuestIndex.Ironfist5: return "Ironfist-Find the shipwreck of the Jolly Raven" + (status.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest5) ? " (turn in!)" : "");
                case MM1QuestIndex.Ironfist6: return "Ironfist-Defeat the Pirate Ghost Ship" + (status.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest6) ? " (turn in!)" : "");
                case MM1QuestIndex.Ironfist7: return "Ironfist-Defeat the Stronghold in Ravens Wood" + (status.IronfistCompleted.HasFlag(MM1CastleQuestFlags.Quest7) ? " (turn in!)" : "");
                case MM1QuestIndex.Inspectron1: return "Inspectron-Find the Ruins in the Quivering Forest" + (status.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest1) ? " (turn in!)" : "");
                case MM1QuestIndex.Inspectron2: return "Inspectron-Visit Blithes Peak and report" + (status.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest2) ? " (turn in!)" : "");
                case MM1QuestIndex.Inspectron3: return "Inspectron-Get Cactus Nectar" + (status.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest3) ? " (turn in!)" : "");
                case MM1QuestIndex.Inspectron4: return "Inspectron-Find the Shrine of Okzar below Dusk" + (status.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest4) ? " (turn in!)" : "");
                case MM1QuestIndex.Inspectron5: return "Inspectron-Find the Fabled Fountain of Dragadune" + (status.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest5) ? " (turn in!)" : "");
                case MM1QuestIndex.Inspectron6: return "Inspectron-Solve the Riddle of the Ruby" + (status.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest6) ? " (turn in!)" : "");
                case MM1QuestIndex.Inspectron7: return "Inspectron-Defeat the Stronghold in the Enchanted Forest" + (status.InspectronCompleted.HasFlag(MM1CastleQuestFlags.Quest7) ? " (turn in!)" : "");
                case MM1QuestIndex.Hacker1: return "Bring Lord Hacker Garlic" + (status.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest1) ? " (turn in!)" : "");
                case MM1QuestIndex.Hacker2: return "Bring Lord Hacker Wolfsbane" + (status.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest2) ? " (turn in!)" : "");
                case MM1QuestIndex.Hacker3: return "Bring Lord Hacker Belladonna" + (status.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest3) ? " (turn in!)" : "");
                case MM1QuestIndex.Hacker4: return "Bring Lord Hacker the Head of a Medusa" + (status.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest4) ? " (turn in!)" : "");
                case MM1QuestIndex.Hacker5: return "Bring Lord Hacker the Eye of a Wyvern" + (status.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest5) ? " (turn in!)" : "");
                case MM1QuestIndex.Hacker6: return "Bring Lord Hacker a Dragon's Tooth" + (status.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest6) ? " (turn in!)" : "");
                case MM1QuestIndex.Hacker7: return "Bring Lord Hacker the Ring of Okrim" + (status.HackerCompleted.HasFlag(MM1CastleQuestFlags.Quest7) ? " (turn in!)" : "");
                default: return "Unknown";
                case MM1QuestIndex.FakeAlamar: return "Find the Crypt of Carmenca for Lord Alamar";
            }
        }
        public static string SignString(MM1Sign sign)
        {
            if ((int)sign == 0)
                return "Not Found";

            switch (sign & MM1Sign.SignMask)
            {
                case MM1Sign.BlackDresidion: return "Black Dresidon";
                case MM1Sign.BlueOgram: return "Blue Ogram";
                case MM1Sign.GreenBagar: return "Green Bagar";
                case MM1Sign.OrangeOolak: return "Orange Oolak";
                case MM1Sign.PurpleSagran: return "Purple Sagran";
                case MM1Sign.RedThorac: return "Red Thorac";
                case MM1Sign.WhiteDilithium: return "White Dilithium";
                case MM1Sign.YellowLimra: return "Yellow Limra";
                default: return "Not Found";
            }
        }

        public static string QuestString(MM1MainQuestFlags main, MM1AstralQuestFlags astral)
        {
            if (main == MM1MainQuestFlags.NotStarted)
                return "Find a man in need of courier service beneath Sorpigal";
            if (main.HasFlag(MM1MainQuestFlags.FoundCanine))
            {
                if (!astral.HasFlag(MM1AstralQuestFlags.EnteredSheltem))
                    return "Expose the impostor in Castle Alamar";
                if (!astral.HasFlag(MM1AstralQuestFlags.AllProjectorsVisited))
                {
                    StringBuilder sb = new StringBuilder();
                    if (!astral.HasFlag(MM1AstralQuestFlags.Projector1Visited))
                        sb.Append("1, ");
                    if (!astral.HasFlag(MM1AstralQuestFlags.Projector2Visited))
                        sb.Append("2, ");
                    if (!astral.HasFlag(MM1AstralQuestFlags.Projector3Visited))
                        sb.Append("3, ");
                    if (!astral.HasFlag(MM1AstralQuestFlags.Projector4Visited))
                        sb.Append("4, ");
                    if (!astral.HasFlag(MM1AstralQuestFlags.Projector5Visited))
                        sb.Append("5, ");
                    Global.Trim(sb);
                    return "Locate Astral Projectors on the Astral Plane: " + sb.ToString();
                }
                if (!astral.HasFlag(MM1AstralQuestFlags.FinishedGame))
                {
                    return "Find a way to enter the INNER SANCTUM!";
                }
                return "Find the Gates to Another World!";
            }
            if (main.HasFlag(MM1MainQuestFlags.FoundStronghold))
                return "Discover the secret of the Stronghold in the Enchanted Forest";
            if (main.HasFlag(MM1MainQuestFlags.FoundRubyWhistle))
                return "Enter the Stronghold in the Enchanted Forest";
            if (main.HasFlag(MM1MainQuestFlags.DeliveredToTelgoran))
            {
                if (main.HasFlag(MM1MainQuestFlags.FoundZam))
                {
                    if (main.HasFlag(MM1MainQuestFlags.FoundZom))
                        return "Use Zam and Zom's clues to locate a hidden treasure";
                    return "Find Zom in Algary";
                }
                if (main.HasFlag(MM1MainQuestFlags.FoundZom))
                    return "Find Zam in Portsmith";
                return "Find the brothers Zam and Zom in Portsmith and Algary";
            }
            if (main.HasFlag(MM1MainQuestFlags.DeliveredToAgar) && !main.HasFlag(MM1MainQuestFlags.DeliveredToTelgoran))
                return "Deliver the VELLUM SCROLL to Telgoran in Dusk";
            if (main.HasFlag(MM1MainQuestFlags.ReceivedScroll) && !main.HasFlag(MM1MainQuestFlags.DeliveredToAgar))
                return "Deliver the VELLUM SCROLL to Agar in Erliquin";
            return "Unknown";
        }

        public static StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            switch (stat)
            {
                case PrimaryStat.Endurance:
                    return StatModifier.FromTable(value, stat, 13, 0, 5, -3, 7, -2, 9, -1, 13, 0, 15, 1, 17, 2, 19, 3, 21, 4, 24, 5, 27, 6, 30, 7, 40, 8, 9);
                case PrimaryStat.Might:
                    return StatModifier.FromTable(value, stat, 12, 0, 15, 1, 16, 2, 17, 3, 18, 4, 19, 5, 21, 6, 23, 7, 25, 8, 27, 9, 29, 10, 35, 11, 40, 12, 13);
                case PrimaryStat.Intellect:
                case PrimaryStat.Personality:
                    return StatModifier.FromTable(value, stat, 5, -3, 7, -2, 9, -1, 13, 0, 15, 1, 17, 2, 19, 3, 21, 4, 24, 5, 27, 6, 30, 7, 35, 8, 40, 9, 10);
                default:
                    return StatModifier.FromTable(value, stat, 5, -3, 7, -2, 9, -1, 13, 0, 15, 1, 17, 2, 19, 3, 21, 4, 25, 5, 30, 6, 35, 7, 40, 8, 9);
            }
        }

        public string WorthyString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (!IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Intellect))
                    sb.Append("I");
                if (!IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Might))
                    sb.Append("M");
                if (!IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Personality))
                    sb.Append("P");
                if (!IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Endurance))
                    sb.Append("E");
                if (!IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Speed))
                    sb.Append("S");
                if (!IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Accuracy))
                    sb.Append("A");
                if (!IncreasersUsed.HasFlag(MM1StatIncreaserFlags.Luck))
                    sb.Append("L");
                if (sb.Length == 0)
                    sb.Append("Visit Clerics!");
                return sb.ToString();
            }
        }

        public int NumAttacks
        {
            get
            {
                int iNumAttacks = 1;
                switch (Class)
                {
                    case MM1Class.Knight:
                    case MM1Class.Paladin:
                    case MM1Class.Archer:
                        iNumAttacks = Level.Temporary / 8 + 1;
                        break;
                }
                return iNumAttacks;
            }
        }

        public override string MeleeDamageString
        {
            get
            {
                return String.Format("{0}{1}", NumAttacks > 1 ? NumAttacks.ToString() + "x " : "", MeleeDamage.ToString());
            }
        }

        public override string RangedDamageString
        {
            get
            {
                return String.Format("{0}{1}", NumAttacks > 1 ? NumAttacks.ToString() + "x " : "", RangedDamage.ToString());
            }
        }

        public static string ConditionDescription(MM1Condition cond)
        {
            if (cond == MM1Condition.Good)
                return "Good: Character is healthy\n";
            if (cond == MM1Condition.Eradicated)
                return "Eradicated: Cannot perform actions and gains no XP\n";

            StringBuilder sb = new StringBuilder();
            if (cond.HasFlag(MM1Condition.Asleep))
                sb.AppendLine("Asleep: Cannot perform actions until attacked");
            if (cond.HasFlag(MM1Condition.Blinded))
                sb.AppendLine("Blinded: Reduced accuracy"); 
            if (cond.HasFlag(MM1Condition.Silenced))
                sb.AppendLine("Silenced: Cannot cast spells"); 
            if (cond.HasFlag(MM1Condition.Diseased))
                sb.AppendLine("Diseased: Gain no HP when resting"); 
            if (cond.HasFlag(MM1Condition.Poisoned))
                sb.AppendLine("Poisoned: Max HP halved when resting");
            if (cond.HasFlag(MM1Condition.Paralyzed) && cond.HasFlag(MM1Condition.SevereFlag))
                sb.AppendLine("Stone: Cannot perform actions and gains no XP");
            else if (cond.HasFlag(MM1Condition.Paralyzed))
                sb.AppendLine("Paralyzed: Cannot perform actions");
            if (cond.HasFlag(MM1Condition.Unconscious) && cond.HasFlag(MM1Condition.SevereFlag))
                sb.AppendLine("Dead: Cannot perform actions and gains no XP");
            else if (cond.HasFlag(MM1Condition.Unconscious))
                sb.AppendLine("Unconscious: Cannot perform actions and dies if attacked");

            return sb.ToString();
        }

        public override string ResistancesString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (MagicResist.Permanent > 0)
                    sb.AppendFormat("Magic:{0}, ", MagicResist.ToString());
                if (FireResist.Permanent > 0)
                    sb.AppendFormat("Fire:{0}, ", FireResist.ToString());
                if (ColdResist.Permanent > 0)
                    sb.AppendFormat("Cold:{0}, ", ColdResist.ToString());
                if (ElecResist.Permanent > 0)
                    sb.AppendFormat("Elec:{0}, ", ElecResist.ToString());
                if (AcidResist.Permanent > 0)
                    sb.AppendFormat("Acid:{0}, ", AcidResist.ToString());
                if (FearResist.Permanent > 0)
                    sb.AppendFormat("Fear:{0}, ", FearResist.ToString());
                if (PoisonResist.Permanent > 0)
                    sb.AppendFormat("Poison:{0}, ", PoisonResist.ToString());
                if (SleepResist.Permanent > 0)
                    sb.AppendFormat("Sleep:{0}, ", SleepResist.ToString());
                Global.Trim(sb);
                if (sb.Length == 0)
                    return "None";
                return sb.ToString();
            }
        }

        public override string AttributesString
        {
            get
            {
                return String.Format("Int:{0}, Mgt:{1}, Per:{2}, End:{3}, Spd:{4}, Acy:{5}, Luc:{6}",
                    Intellect.ToString(),
                    Might.ToString(),
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
                return String.Format("{0}{1}", Experience, ReadyToTrain ? " (Train!)" : ("/" + XPForNextLevel.ToString()));
            }
        }

        public override int TrainableLevel
        {
            get
            {
                int iLevel = Level.Permanent;
                while (XPForLevel(Class, iLevel + 1) <= Experience && iLevel < 255)
                    iLevel++;
                return iLevel;
            }
        }

        public override bool ReadyToTrain
        {
            get
            {
                return NeedsXP < 1;
            }
        }

        public static int BaseHPForClass(MM1Class mm1Class)
        {
            switch (mm1Class)
            {
                case MM1Class.Sorcerer:
                    return 6;
                case MM1Class.Cleric:
                case MM1Class.Robber:
                    return 8;
                case MM1Class.Archer:
                case MM1Class.Paladin:
                    return 10;
                case MM1Class.Knight:
                    return 12;
                default:
                    return 0;
            }
        }

        public string HPLevelString
        {
            get
            {
                int iBase = BaseHPForClass(Class);
                int iBonus = MM1Character.GetStatModifier(Endurance.Permanent, PrimaryStat.Endurance).Value;

                return String.Format("{0} - {1}", iBonus + 1, iBonus + iBase);
            }
        }

        public override string EquippedString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach(MM1Item item in Inventory.EquippedItems)
                    sb.AppendFormat("{0}, ", item.Name);
                Global.Trim(sb);
                if (sb.Length == 0)
                    return "(nothing)";
                return sb.ToString();
            }
        }

        public override string BackpackString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (MM1Item item in Inventory.BackpackItems)
                    sb.AppendFormat("{0}, ", item.Name);
                Global.Trim(sb);
                if (sb.Length == 0)
                    return "(empty)";
                return sb.ToString();
            }
        }

        public override string BasicInfoString
        {
            get
            {
                if (Level == null || Alignment == null)
                    return "<Invalid Character Record>";
                return String.Format("Level {0} {1} {2} {3} {4}, {5} old",
                 Level.ToString(),
                 MM1Character.SexString(Sex),
                 Alignment.ToString(),
                 MM1Character.RaceString(Race),
                 MM1Character.ClassString(Class),
                 MM1Character.AgeString(Age, RestCounter));
            }
        }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(CharName);
                sb.AppendLine(BasicInfoString);
                sb.AppendLine(AttributesString);
                sb.AppendFormat("Experience: {0}\n", ExperienceString);
                sb.AppendFormat("Condition: {0}\n", MM1Character.ConditionString(Condition, true));
                sb.AppendFormat("HP: {0}\n", HitPoints.ToString());
                sb.AppendFormat("SP: {0} ({1})\n", SpellPoints.ToString(), SpellLevel.Permanent);
                sb.AppendFormat("AC: {0}\n", ArmorClass.ToString());
                sb.AppendFormat("Resist: {0}\n", ResistancesString);
                sb.AppendFormat("Damage: {0} melee, {1} ranged\n", MeleeDamageString, RangedDamageString);
                sb.AppendFormat("Equipped: {0}\n", EquippedString);
                sb.AppendFormat("Backpack: {0}\n", BackpackString);
                sb.AppendFormat("Food: {0}\n", Food);
                sb.AppendFormat("Gems: {0}\n", Gems);
                sb.AppendFormat("Gold: {0}\n", Gold);
                sb.AppendFormat("Thievery: {0}\n", Thievery);
                return sb.ToString();
            }
        }

        public override bool UsesSpellLevel { get { return true; } }
        public override BasicDamage BasicMeleeDamage { get { return new BasicDamage(NumAttacks, new DamageDice(MeleeDamage.Ordinary, 1, MeleeDamage.Magical)); } }
        public override BasicDamage BasicRangedDamage { get { return new BasicDamage(NumAttacks, new DamageDice(RangedDamage.Ordinary, 1, RangedDamage.Magical)); } }

        public override StatAndModifier BasicLevel { get { return new StatAndModifier(Level); } }
        public override StatAndModifier BasicAC { get { return new StatAndModifier(ArmorClass.ArmorOnly, ArmorClass.Total - ArmorClass.ArmorOnly); } }
        public override MMSex BasicSex { get { return Sex; } }

        public override StatAndModifier BasicIntellect { get { return new StatAndModifier(Intellect); } }
        public override StatAndModifier BasicMight { get { return new StatAndModifier(Might); } }
        public override StatAndModifier BasicPersonality { get { return new StatAndModifier(Personality); } }
        public override StatAndModifier BasicEndurance { get { return new StatAndModifier(Endurance); } }
        public override StatAndModifier BasicSpeed { get { return new StatAndModifier(Speed); } }
        public override StatAndModifier BasicAccuracy { get { return new StatAndModifier(Accuracy); } }
        public override StatAndModifier BasicLuck { get { return new StatAndModifier(Luck); } }

        public override List<Item> BackpackItems
        {
            get
            {
                return new List<Item>(Inventory.BackpackItems);
            }
        }

        public override GenericClass BasicClass
        {
            get
            {
                switch (Class)
                {
                    case MM1Class.Knight: return GenericClass.Knight;
                    case MM1Class.Paladin: return GenericClass.Paladin;
                    case MM1Class.Archer: return GenericClass.Archer;
                    case MM1Class.Cleric: return GenericClass.Cleric;
                    case MM1Class.Sorcerer: return GenericClass.Sorcerer;
                    case MM1Class.Robber: return GenericClass.Robber;
                    default: return GenericClass.None;
                }
            }
        }

        public override GenericRace BasicRace
        {
            get
            {
                switch (Race)
                {
                    case MM1Race.Human: return GenericRace.Human;
                    case MM1Race.Elf: return GenericRace.Elf;
                    case MM1Race.Gnome: return GenericRace.Gnome;
                    case MM1Race.Dwarf: return GenericRace.Dwarf;
                    case MM1Race.HalfOrc: return GenericRace.HalfOrc;
                    default: return GenericRace.None;
                }
            }
        }

        public override GenericAge BasicAge { get { return new GenericAge(Age, RestCounter); } }

        public override GenericAlignment BasicAlignment
        {
            get
            {
                return new GenericAlignment(BasicAlignmentValue(true), BasicAlignmentValue(false)); 
            }
        }

        public GenericAlignmentValue BasicAlignmentValue(bool bTemporary)
        {
            if (Alignment == null)
                return GenericAlignmentValue.None;
            switch (bTemporary ? Alignment.Temporary : Alignment.Permanent)
            {
                case MM1AlignmentValue.Good: return GenericAlignmentValue.Good;
                case MM1AlignmentValue.Neutral: return GenericAlignmentValue.Neutral;
                case MM1AlignmentValue.Evil: return GenericAlignmentValue.Evil;
                default: return GenericAlignmentValue.None;
            }
        }

        public override long QuickRefExperience { get { return Experience; } }
        public override MMHitPoints QuickRefHitPoints { get { return HitPoints; } }
        public override SpellPoints QuickRefSpellPoints { get { return SpellPoints; } }
        public override OneByteStat QuickRefSpeed { get { return Speed; } }
        public override OneByteStat QuickRefSpellLevel { get { return SpellLevel; } }
        public override int QuickRefGems { get { return Gems; } }
        public override string QuickRefCondition { get { return MM1Character.ConditionString(Condition, false); } }
        public override bool IsHealer { get { return (Class == MM1Class.Paladin || Class == MM1Class.Cleric); } }

        public override BasicConditionFlags BasicCondition
        {
            get
            {
                BasicConditionFlags cond = BasicConditionFlags.Good;

                if (Condition == MM1Condition.Eradicated)
                    return BasicConditionFlags.Eradicated;

                if (Condition.HasFlag(MM1Condition.Asleep))
                    cond |= BasicConditionFlags.Asleep;
                if (Condition.HasFlag(MM1Condition.Blinded))
                    cond |= BasicConditionFlags.Blinded;
                if (Condition.HasFlag(MM1Condition.Diseased))
                    cond |= BasicConditionFlags.Diseased;
                if (Condition.HasFlag(MM1Condition.Poisoned))
                    cond |= BasicConditionFlags.Poisoned;
                if (Condition.HasFlag(MM1Condition.Silenced))
                    cond |= BasicConditionFlags.Silenced;
                if (Condition.HasFlag(MM1Condition.Paralyzed))
                    cond |= (Condition.HasFlag(MM1Condition.SevereFlag) ? BasicConditionFlags.Stone : BasicConditionFlags.Paralyzed);
                if (Condition.HasFlag(MM1Condition.Unconscious))
                    cond |= (Condition.HasFlag(MM1Condition.SevereFlag) ? BasicConditionFlags.Dead : BasicConditionFlags.Unconscious);

                return cond;
            }
        }

        public override byte ConditionValue(BasicConditionFlags condition)
        {
            byte bResult = 0;

            if (condition.HasFlag(BasicConditionFlags.Eradicated))
                return (byte) MM1Condition.Eradicated;

            if (condition.HasFlag(BasicConditionFlags.Dead))
                bResult |= (byte) MM1Condition.Dead;
            if (condition.HasFlag(BasicConditionFlags.Stone))
                bResult |= (byte) MM1Condition.Stone;
            if (condition.HasFlag(BasicConditionFlags.Asleep))
                bResult |= (byte)MM1Condition.Asleep;
            if (condition.HasFlag(BasicConditionFlags.Blinded))
                bResult |= (byte)MM1Condition.Blinded;
            if (condition.HasFlag(BasicConditionFlags.Diseased))
                bResult |= (byte)MM1Condition.Diseased;
            if (condition.HasFlag(BasicConditionFlags.Poisoned))
                bResult |= (byte)MM1Condition.Poisoned;
            if (condition.HasFlag(BasicConditionFlags.Silenced))
                bResult |= (byte)MM1Condition.Silenced;
            if (condition.HasFlag(BasicConditionFlags.Paralyzed))
                bResult |= (byte)MM1Condition.Paralyzed;
            if (condition.HasFlag(BasicConditionFlags.Unconscious))
                bResult |= (byte)MM1Condition.Unconscious;

            return bResult;
        }

        public override byte SexValue(MMSex sex)
        {
            return (byte)sex;
        }

        public override byte AlignmentValue(GenericAlignmentValue align)
        {
            switch (align)
            {
                case GenericAlignmentValue.Evil: return (byte)MM1AlignmentValue.Evil;
                case GenericAlignmentValue.Neutral: return (byte)MM1AlignmentValue.Neutral;
                case GenericAlignmentValue.Good: return (byte)MM1AlignmentValue.Good;
                default: return (byte)MM1AlignmentValue.None;
            }
        }

        public override byte RaceValue(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return (byte)MM1Race.Human;
                case GenericRace.Elf: return (byte)MM1Race.Elf;
                case GenericRace.Dwarf: return (byte)MM1Race.Dwarf;
                case GenericRace.Gnome: return (byte)MM1Race.Gnome;
                case GenericRace.HalfOrc: return (byte)MM1Race.HalfOrc;
                default: return (byte)MM1Race.None;
            }
        }

        public static MM1Class ClassForGeneric(GenericClass mmClass)
        {
            switch (mmClass)
            {
                case GenericClass.Archer: return MM1Class.Archer;
                case GenericClass.Cleric: return MM1Class.Cleric;
                case GenericClass.Knight: return MM1Class.Knight;
                case GenericClass.Paladin: return MM1Class.Paladin;
                case GenericClass.Robber: return MM1Class.Robber;
                case GenericClass.Sorcerer: return MM1Class.Sorcerer;
                default: return MM1Class.None;
            }
        }

        public override byte ClassValue(GenericClass classVal)
        {
            return (byte)ClassForGeneric(classVal);
        }

        public override Item GetItem(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < 2)
                return null;

            if (bytes[offset] >= MM1.Items.Count)
                return null;

            MM1Item item = MM1.Items[bytes[offset]].Clone() as MM1Item;
            item.m_iChargesCurrent = bytes[offset + 1];

            return item;
        }

        public override GameNames Game { get { return GameNames.MightAndMagic1; } }

        public override int FirstEmptyBackpackIndex
        {
            get
            {
                if (Inventory == null)
                    return -1;
                if (Inventory.BackpackItems.Count > 5)
                    return -1;
                int[] used = new int[6] { 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < Inventory.BackpackItems.Count; i++)
                    if (Inventory.BackpackItems[i].MemoryIndex > -1 && Inventory.BackpackItems[i].MemoryIndex < 6)
                        used[Inventory.BackpackItems[i].MemoryIndex] = 1;
                for (int i = 0; i < 6; i++)
                    if (used[i] == 0)
                        return i;
                return -1;
            }
        }

        public override bool BackpackFull
        {
            get
            {
                return (FirstEmptyBackpackIndex == -1);
            }
        }

        public override int MaxLevel { get { return 200; } }

        public override string GetACFormula(int iBless = 0)
        {
            return String.Format("{0}\tSpeed modifier", GetStatModifier(Speed.Temporary, PrimaryStat.Speed).PlusValue);
        }

        public override Modifiers InternalModifiers
        {
            get
            {
                Modifiers mod = MM1.Modifiers.For(BasicRace).Clone();

                if (Age > 59)
                {
                    int iMight = Age > 79 ? -4 : Age > 69 ? -2 : -1;
                    int iOther = Age > 69 ? -2 : -1;
                    mod.Adjust(ModAttr.Might, iMight, "Age penalty");
                    mod.Adjust(ModAttr.Endurance, iOther, "Age penalty");
                    mod.Adjust(ModAttr.Speed, iOther, "Age penalty");
                }


                foreach (MM1Item item in Inventory.EquippedItems)
                {
                    if (item.IsArmor)
                        mod.Adjust(ModAttr.ArmorClass, item.Extra, item.DescriptionString);
                    ModAttr attrib = Modifiers.GetAttrib(MM1Item.GetEquipAttribute(item.EquipAttribute));
                    if (attrib != WhereAreWe.ModAttr.Invalid)
                        mod.Adjust(attrib, item.EquipAttributeBonus, item.DescriptionString);
                }
                return mod;
            }
        }

        public override string GetThieveryFormula()
        {
            if (Class == MM1Class.Robber)
                return "(Level * 2) + 48 (Robber)";
            return "(Level * 2) - 1";
        }

        public override string GetMaxSPFormula()
        {
            // Only gets calculated in-game when resting, so temporary effects are not used
            int iLev = Level.Permanent;
            int iLev6 = Math.Max(iLev-6, 0);
            int iInt = GetStatModifier(Intellect.Permanent, PrimaryStat.Intellect).Value;
            int iPer = GetStatModifier(Personality.Permanent, PrimaryStat.Personality).Value;

            string strNote = "\r\nNOTE:\tThe maximum spell points are calculated during resting.\r\n\tOnly the permanent stat value is used.";

            switch (Class)
            {
                case MM1Class.Archer: return String.Format("(Level - 6) * (3 + IntellectBonus)\r\n({0} - 6) * (3 + {1}) = {2}{3}",
                    iLev, iInt, Math.Max(iLev6 * (3 + iInt), 0), strNote);
                case MM1Class.Sorcerer: return String.Format("Level * (3 + IntellectBonus)\r\n{0} * (3 + {1}) = {2}{3}",
                    iLev, iInt, Math.Max(iLev * (3 + iInt), 0), strNote);
                case MM1Class.Paladin: return String.Format("(Level - 6) * (3 + PersonalityBonus)\r\n({0} - 6) * (3 + {1}) = {2}{3}",
                   iLev, iPer, Math.Max(iLev6 * (3 + iPer), 0), strNote);
                case MM1Class.Cleric: return String.Format("Level * (3 + PersonalityBonus)\r\n{0} * (3 + {1}) = {2}{3}",
                   iLev, iPer, Math.Max(iLev * (3 + iPer), 0), strNote);
                default: return String.Empty;
            }
        }

        public override string IntellectEffect()
        {
            StatModifier mod = GetStatModifier(Intellect.Permanent, PrimaryStat.Intellect);
            switch (Class)
            {
                case MM1Class.Sorcerer:
                case MM1Class.Archer:
                    return IntellectEffect(mod, mod.Value);
                default: return String.Empty;
            }
        }

        public override string PersonalityEffect()
        {
            StatModifier mod = GetStatModifier(Personality.Permanent, PrimaryStat.Personality);
            switch (Class)
            {
                case MM1Class.Cleric:
                case MM1Class.Paladin:
                    return PersonalityEffect(mod, mod.Value);
                default: return String.Empty;
            }
        }
    }
}
