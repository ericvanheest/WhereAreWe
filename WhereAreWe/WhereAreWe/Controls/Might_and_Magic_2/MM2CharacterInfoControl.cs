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
    public partial class MM2CharacterInfoControl : MMCharacterInfoControl
    {
        private Control m_ctrlView = null;

        public MM2CharacterInfoControl(IMain main) : base(main)
        {
            InitializeComponent();
            m_char = new MM2Character();

            FindEditableAttributes();
        }

        public override int CharacterSize { get { return MM2Character.SizeInBytes; } }

        public override void SetInfo(PartyInfo info, int iIndex, GameInfo gameInfo, EncounterInfo encounterInfo = null)
        {
            if (info != null && info.Bytes.Length < (info.Addresses[iIndex] + 1) * CharacterSize)
                return;

            if (info is MM2PartyInfo)
            {
                m_bytes = new byte[info.CharacterSize];
                Buffer.BlockCopy(info.Bytes, info.Addresses[iIndex] * info.CharacterSize, m_bytes, 0, info.CharacterSize);
                m_char.SetFromBytes(0, m_bytes, gameInfo, encounterInfo);
                m_iCharacterIndex = iIndex;
                m_iCharacterAddress = info.Addresses[iIndex];
                m_iCharacterPosition = iIndex;
                ((MM2Character) m_char).Address = m_iCharacterAddress;
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
            MM2Character mm2Char = m_char as MM2Character;

            bool bHireling = (CharacterAddress > 23);

            if (String.IsNullOrWhiteSpace(mm2Char.CharName))
                return;

            m_commonCtrls.labelLevel.Text = mm2Char.BasicInfoString + (bHireling ? " (hireling)" : "");
            m_commonCtrls.labelAC.Text = mm2Char.ArmorClass.ToString();
            MMCommonControls.labelAccuracy.Text = mm2Char.Accuracy.ToString();
            ListViewSelectionSaver savePack = new ListViewSelectionSaver(m_commonCtrls.lvBackpack);
            ListViewSelectionSaver saveEquip = new ListViewSelectionSaver(m_commonCtrls.lvEquipped);

            m_commonCtrls.lvEquipped.BeginUpdate();
            m_commonCtrls.lvBackpack.BeginUpdate();
            for (int i = 0; i < 6; i++)
            {
                if (mm2Char.Inventory.BackpackItems.Count > i)
                    SetBackpackLVI(i, mm2Char.Inventory.BackpackItems[i], mm2Char);
                else
                    SetBackpackLVI(i, null, mm2Char);

                if (mm2Char.Inventory.EquippedItems.Count > i)
                    SetEquippedLVI(i, mm2Char.Inventory.EquippedItems[i], mm2Char);
                else
                    SetEquippedLVI(i, null, mm2Char);
            }
            Global.FitSingleColumn(m_commonCtrls.lvBackpack);
            Global.FitSingleColumn(m_commonCtrls.lvEquipped);
            UpdateHeaders();
            m_commonCtrls.lvEquipped.EndUpdate();
            m_commonCtrls.lvBackpack.EndUpdate();

            SetResistances(mm2Char.GetResistances());
            m_commonCtrls.labelCondition.Text = MM2Character.ConditionString(mm2Char.Condition, true);
            m_tipCondition.SetToolTip(m_commonCtrls.labelCondition, MM2Character.ConditionDescription(mm2Char.Condition));
            m_tipCondition.ShowAlways = true;
            m_tipCondition.AutoPopDelay = 32000;

            MMCommonControls.labelEndurance.Text = mm2Char.Endurance.ToString();
            m_commonCtrls.labelExp.Text = mm2Char.ExperienceString;
            labelFood.Text = String.Format("{0}", mm2Char.Food);
            labelGems.Text = String.Format("{0}", mm2Char.Gems);
            if (bHireling)
            {
                labelGold.Text = String.Format("{0}/day", mm2Char.Gold);
                labelGoldHeader.Text = "Cost";
            }
            else
            {
                labelGold.Text = String.Format("{0}", mm2Char.Gold);
                labelGoldHeader.Text = "Gold";
            }
            m_commonCtrls.labelHP.Text = mm2Char.HitPoints.ToString();
            MMCommonControls.labelIntellect.Text = mm2Char.Intellect.ToString();
            MMCommonControls.labelLuck.Text = mm2Char.Luck.ToString();
            MMCommonControls.labelMight.Text = mm2Char.Might.ToString();
            MMCommonControls.labelPersonality.Text = mm2Char.Personality.ToString();
            m_commonCtrls.labelSP.Text = mm2Char.SpellPoints.ToString();
            MMCommonControls.labelSpeed.Text = mm2Char.Speed.ToString();
            labelSpellLevel.Text = mm2Char.SpellLevel.ToString();
            m_commonCtrls.labelMelee.Text = mm2Char.MeleeDamageString;
            MMCommonControls.labelRanged.Text = mm2Char.RangedDamageString;
//            labelSideQuest.Text = MM2Character.QuestString(mm2Char.Quest, mm2Char.CastleQuestStatus);
            MMCommonControls.labelThievery.Text = String.Format("{0}", mm2Char.Thievery);
            labelKnownSpells.Text = String.Format("{0}/48", mm2Char.KnownSpells.Total);
            m_tipSpells.SetToolTip(labelKnownSpells, mm2Char.KnownSpells.ToString());
            m_tipSpells.ShowAlways = true;
            m_tipSpells.AutoPopDelay = 32000;
            m_tipSkills.SetToolTip(labelSecondarySkills, mm2Char.SecondarySkillsDescription);
            m_tipSkills.ShowAlways = true;
            m_tipSkills.AutoPopDelay = 32000;
            labelSecondarySkills.Text = mm2Char.SkillString;

            m_commonCtrls.llCureAll.Visible = (mm2Char.Class == MM2Class.Cleric || mm2Char.Class == MM2Class.Paladin) && Properties.Settings.Default.EnableMemoryWrite;
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
            else if (label == labelFood)
            {
                m_cheatType = AttributeType.UInt8;
                m_cheatOffsets = new int[] { m_char.Offsets.Food };
            }
            else if (label == labelGold)
            {
                m_cheatType = AttributeType.Int32;
                m_cheatOffsets = new int[] { m_char.Offsets.Gold };
            }
            else if (label == labelGems)
            {
                m_cheatType = AttributeType.UInt16;
                m_cheatOffsets = new int[] { m_char.Offsets.Gems };
            }
            else if (label == labelKnownSpells)
            {
                menuFlags = CheatMenuFlags.Edit;
                m_cheatType = AttributeType.KnownSpells;
                m_cheatOffsets = Global.IntRange(m_char.Offsets.Spells, m_char.Offsets.SpellsLength);
            }
            else if (label == labelSecondarySkills)
            {
                menuFlags = CheatMenuFlags.Edit;
                m_cheatType = AttributeType.SecondarySkills;
                m_cheatOffsets = Global.IntRange(m_char.Offsets.Skills, m_char.Offsets.SkillsLength);
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
                    m_cheatOffsets = new int[] {
                        m_char.Offsets.BackpackBases + iIndex,
                        m_char.Offsets.BackpackCharges + iIndex,
                        m_char.Offsets.BackpackBonus + iIndex };
                }
                else if (label == m_commonCtrls.lvEquipped && m_commonCtrls.lvEquipped.FocusedItem != null)
                {
                    int iIndex = m_commonCtrls.lvEquipped.FocusedItem.Index;
                    m_cheatOffsets = new int[] {
                        m_char.Offsets.EquippedBases + iIndex,
                        m_char.Offsets.EquippedCharges + iIndex,
                        m_char.Offsets.EquippedBonus + iIndex };
                }
            }

            if (m_cheatOffsets == null)
                return CheatMenuFlags.None;

            return menuFlags;
        }

        private void labelKnownSpells_MouseUp(object sender, MouseEventArgs e)
        {
            if (Global.Cheats)
                return;     // In cheat mode you can just edit the spells

            m_ctrlView = labelKnownSpells;

            cmView.Show(Cursor.Position);
        }

        private void miViewView_Click(object sender, EventArgs e)
        {
            if (m_ctrlView == labelKnownSpells)
            {
                MM2KnownSpellsEditForm form = new MM2KnownSpellsEditForm();
                form.Sorcerer = !m_char.IsHealer;
                form.ReadOnly = true;
                form.Attribute = new EditableAttribute(((MM2Character)m_char).KnownSpells.GetBytes());
                form.ShowDialog();
            }
            else if (m_ctrlView == labelSecondarySkills)
            {
                MM2SecondarySkillForm form = new MM2SecondarySkillForm();
                form.ReadOnly = true;
                form.SkillByte = ((MM2Character)m_char).SkillByte;
                form.ShowDialog();
            }
        }

        private void labelSecondarySkills_MouseUp(object sender, MouseEventArgs e)
        {
            if (Global.Cheats)
                return;     // In cheat mode you can just edit the skills

            m_ctrlView = labelSecondarySkills;

            cmView.Show(Cursor.Position);
        }
    }

    public class MM2CharacterOffsets : CharacterOffsets
    {
        public override int Name             { get { return 0; } }
        public override int NameTerminator   { get { return 10; } }
        public override int Town             { get { return 11; } }
        public override int Sex              { get { return 12; } }
        public override int Race             { get { return 14; } }
        public override int Alignment        { get { return 13; } }
        public override int AlignmentMod     { get { return 106; } }
        public override int Class            { get { return 15; } }
        public override int Stats            { get { return 16; } }
        public override int Might            { get { return 16; } }
        public override int Intellect        { get { return 17; } }
        public override int Personality      { get { return 18; } }
        public override int Endurance        { get { return 39; } }
        public override int Speed            { get { return 19; } }
        public override int Accuracy         { get { return 20; } }
        public override int Luck             { get { return 21; } }
        public override int MightMod         { get { return 107; } }
        public override int IntellectMod     { get { return 108; } }
        public override int PersonalityMod   { get { return 109; } }
        public override int EnduranceMod     { get { return 115; } }
        public override int SpeedMod         { get { return 110; } }
        public override int AccuracyMod      { get { return 111; } }
        public override int LuckMod          { get { return 112; } }
        public override int ArmorClass       { get { return 31; } }
        public override int ArmorClassMod    { get { return 36; } }
        public override int Level            { get { return 32; } }
        public override int LevelMod         { get { return 113; } }
        public override int Age              { get { return 33; } }
        public override int AgeDays          { get { return 34; } }
        public override int Awards           { get { return 118; } }
        public override int AwardsLength     { get { return 12; } }
        public override int Skills           { get { return 80; } }
        public override int SkillsLength     { get { return 1; } }
        public override int Spells           { get { return 81; } }
        public override int SpellsLength     { get { return 6; } }
        public override int Inventory        { get { return 40; } }
        public override int InventoryLength  { get { return 36; } }
        public override int MagicResist      { get { return 22; } }
        public override int FireResist       { get { return 23; } }
        public override int ElecResist       { get { return 24; } }
        public override int ColdResist       { get { return 25; } }
        public override int EnergyResist     { get { return 26; } }
        public override int SleepResist      { get { return 27; } }
        public override int PoisonResist     { get { return 28; } }
        public override int AcidResist       { get { return 29; } }
        public override int MagicResistMod   { get { return -1; } }
        public override int FireResistMod    { get { return -1; } }
        public override int ElecResistMod    { get { return -1; } }
        public override int ColdResistMod    { get { return -1; } }
        public override int EnergyResistMod  { get { return -1; } }
        public override int SleepResistMod   { get { return -1; } }
        public override int PoisonResistMod  { get { return -1; } }
        public override int AcidResistMod    { get { return -1; } }
        public override int Condition        { get { return 38; } }
        public override int ConditionLength  { get { return 1; } }
        public override int CurrentHP        { get { return 94; } }
        public override int MaxHP            { get { return 96; } }
        public override int MaxHPMod         { get { return 116; } }
        public override int CurrentSP        { get { return 88; } }
        public override int MaxSP            { get { return 90; } }
        public override int Gems             { get { return 92; } }
        public override int Food             { get { return 37; } }
        public override int Gold             { get { return 102; } }
        public override int Experience       { get { return 98; } }
        public override int SpellLevel       { get { return 35; } }
        public override int SpellLevelMod    { get { return 114; } }
        public override int BackpackBases    { get { return 58; } }
        public override int BackpackCharges  { get { return 64; } }
        public override int BackpackBonus    { get { return 70; } }
        public override int EquippedBases    { get { return 40; } }
        public override int EquippedCharges  { get { return 46; } }
        public override int EquippedBonus    { get { return 52; } }
        public override int MeleeDamage      { get { return 76; } }
        public override int RangedDamage     { get { return 78; } }
        public override int Thievery         { get { return 30; } }
    }

    public class MM2Character : MMBaseCharacter
    {
        public string CharName;                     // 0-9, space-padded
        public byte NameTerminator;                 // 10
        public byte Town;                           // 11
        public MM2Sex Sex;                          // 12
        public MM2Race Race;                        // 14
        public MM2Class Class;                      // 15
        public OneByteStat Might;                   // 16, 107
        public OneByteStat Intellect;               // 17, 108
        public OneByteStat Personality;             // 18, 109
        public OneByteStat Speed;                   // 19, 110
        public OneByteStat Accuracy;                // 20, 111
        public OneByteStat Luck;                    // 21, 112
        public byte MagicResist;                    // 22
        public byte FireResist;                     // 23
        public byte ElecResist;                     // 24
        public byte ColdResist;                     // 25
        public byte EnergyResist;                   // 26
        public byte SleepResist;                    // 27
        public byte PoisonResist;                   // 28
        public byte AcidResist;                     // 29
        public byte Thievery;                       // 30
        public OneByteStat Level;                   // 32, 113
        public byte Age;                            // 33
        public byte AgeDays;                        // 34
        public MMArmorClass ArmorClass;             // 31, 36
        public byte Food;                           // 37
        public MM2Condition Condition;              // 38
        public OneByteStat Endurance;               // 39, 115
        public MM2Inventory Inventory;              // 40-75
        public MMDamage MeleeDamage;                // 76-77
        public MMDamage RangedDamage;               // 78-79
        public MM2SecondarySkill Skill1;            // 80 (low 4 bits)
        public MM2SecondarySkill Skill2;            // 80 (high 4 bits)
        public MM2KnownSpells KnownSpells;          // 81-86
        public byte Unknown087;
        public MMSpellPoints SpellPoints;           // 88-91
        public UInt16 Gems;                         // 92-93
        public MMHitPoints HitPoints;               // 94-97, 116-117
        public UInt32 Experience;                   // 98-101
        public UInt32 Gold;                         // 102-105
        public MM2Alignment Alignment;              // 106, 13
        public OneByteStat SpellLevel;              // 114, 35
        public MM2MealsEaten Meals;                 // 118, 119
        public byte QuestObject;                    // 120 - Index of the item for Lord Hoardall or the monster for Lord Slayer
        public MM2GuildFlags Guilds;                // 121
        public MM2AdvancementFlags Advancement;     // 122
        public MM2QuestFlags1 Quests1;              // 123-125
        public MM2ArenaFlags Arena;                 // 126-127
        public MM2QuestFlags2 Quests2;              // 128-129

        public int Address = -1;
        public const int SizeInBytes = 130;

        public MM2Character()
        {
            Address = -1;
        }

        public override int BasicSP { get { return SpellPoints == null ? 0 : SpellPoints.CurrentSP; } }
        public override int BasicMaxSP { get { return SpellPoints == null ? 0 : SpellPoints.MaximumSP; } }
        public override int BasicHP { get { return HitPoints == null ? 0 : HitPoints.Current; } }
        public override int BasicMaxHP { get { return HitPoints == null ? 0 : HitPoints.TemporaryMaximum; } }
        public override long BasicMoney { get { return Gold; } }
        public override int BasicFood { get { return Food; } }
        public override int BasicThievery { get { return Thievery; } }

        public override CharacterOffsets Offsets { get { return MM2.Offsets; } }

        public override int CharacterSize { get { return SizeInBytes; } }

        public static MM2Character Create(byte[] bytes, int iIndex = 0, bool bRosterFile = false)
        {
            if (bytes == null || bytes.Length < iIndex + SizeInBytes)
                return null;
            MM2Character character = new MM2Character();
            character.SetFromBytes(bytes, iIndex, bRosterFile);
            return character;
        }

        public static byte[] GetInventoryCharBytes()
        {
            // The bytes for a Level 1 Neutral Human Knight named "Inventory" with no items
            return new byte[] {
                0x49, 0x6E, 0x76, 0x65, 0x6E, 0x74, 0x6F, 0x72, 0x79, 0x20, 0x00, 0x01, 0x00, 0x01, 0x00, 0x00, 0x0F, 0x0F, 0x0F, 0x0F,
                0x0F, 0x0F, 0x00, 0x05, 0x05, 0x05, 0x00, 0x3C, 0x3C, 0x00, 0x00, 0x00, 0x01, 0x12, 0x00, 0x00, 0x01, 0x0A, 0x00, 0x0F,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA1, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x0D, 0x00, 0x0D, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x0F, 0x01, 0x00, 0x0F, 0x0D, 0x00, 0x00, 0x00,
                0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00
            };
        }

        public override int BasicAddress { get { return Address; } }
        public override Inventory BasicInventory { get { return Inventory as Inventory; } }

        public override void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info = null, EncounterInfo encounterInfo = null, bool bFromRosterFile = false)
        {
            int iSize = SizeInBytes;
            if (stream.Length < iSize)
                return;

            RawBytes = new byte[iSize];
            stream.Read(RawBytes, 0, iSize);

            CharName = Encoding.ASCII.GetString(RawBytes, Offsets.Name, Offsets.NameTerminator - Offsets.Name).Trim();
            NameTerminator = RawBytes[Offsets.NameTerminator];
            Town = RawBytes[Offsets.Town];
            Sex = SexFromByte(RawBytes[Offsets.Sex]);
            Race = RaceFromByte(RawBytes[Offsets.Race]);
            Class = ClassFromByte(RawBytes[Offsets.Class]);
            Might = new OneByteStat(RawBytes[Offsets.MightMod], RawBytes[Offsets.Might]);
            Intellect = new OneByteStat(RawBytes[Offsets.IntellectMod], RawBytes[Offsets.Intellect]);
            Personality = new OneByteStat(RawBytes[Offsets.PersonalityMod], RawBytes[Offsets.Personality]);
            Endurance = new OneByteStat(RawBytes[Offsets.EnduranceMod], RawBytes[Offsets.Endurance]);
            Speed = new OneByteStat(RawBytes[Offsets.SpeedMod], RawBytes[Offsets.Speed]);
            Accuracy = new OneByteStat(RawBytes[Offsets.AccuracyMod], RawBytes[Offsets.Accuracy]);
            Luck = new OneByteStat(RawBytes[Offsets.LuckMod], RawBytes[Offsets.Luck]); 
            MagicResist = RawBytes[Offsets.MagicResist];
            FireResist = RawBytes[Offsets.FireResist];
            ElecResist = RawBytes[Offsets.ElecResist];
            ColdResist = RawBytes[Offsets.ColdResist];
            EnergyResist = RawBytes[Offsets.EnergyResist];
            SleepResist = RawBytes[Offsets.SleepResist];
            PoisonResist = RawBytes[Offsets.PoisonResist];
            AcidResist = RawBytes[Offsets.AcidResist];
            Thievery = RawBytes[Offsets.Thievery];
            Age = RawBytes[Offsets.Age];
            AgeDays = RawBytes[Offsets.AgeDays];
            ArmorClass = new MMArmorClass(RawBytes[Offsets.ArmorClass], RawBytes[Offsets.ArmorClassMod]);
            Food = RawBytes[Offsets.Food];
            Condition = (MM2Condition)RawBytes[Offsets.Condition];
            Inventory = new MM2Inventory(RawBytes, Offsets.Inventory);
            MeleeDamage = new MMDamage(RawBytes, Offsets.MeleeDamage);
            RangedDamage = new MMDamage(RawBytes, Offsets.RangedDamage);
            Skill1 = (MM2SecondarySkill)(RawBytes[Offsets.Skills] & 0x0f);
            Skill2 = (MM2SecondarySkill)((RawBytes[Offsets.Skills] & 0xf0) >> 4);
            Unknown087 = RawBytes[87];
            KnownSpells = new MM2KnownSpells(RawBytes, Offsets.Spells);
            SpellPoints = new MMSpellPoints(RawBytes, Offsets.CurrentSP);
            Gems = BitConverter.ToUInt16(RawBytes, Offsets.Gems);
            HitPoints = new MMHitPoints(RawBytes, Offsets.CurrentHP, Offsets.MaxHP, Offsets.MaxHPMod);
            Experience = BitConverter.ToUInt32(RawBytes, Offsets.Experience);
            Gold = BitConverter.ToUInt32(RawBytes, Offsets.Gold);
            Alignment = new MM2Alignment(RawBytes[Offsets.Alignment], RawBytes[Offsets.AlignmentMod]);
            Level = new OneByteStat(RawBytes[Offsets.LevelMod], RawBytes[Offsets.Level]);
            SpellLevel = new OneByteStat(RawBytes[Offsets.SpellLevelMod], RawBytes[Offsets.SpellLevel]);
            Meals = (MM2MealsEaten)((RawBytes[Offsets.Awards] << 8) | RawBytes[Offsets.Awards+1]);
            QuestObject = RawBytes[Offsets.Awards+2];
            Guilds = (MM2GuildFlags)RawBytes[Offsets.Awards + 3];
            Advancement = (MM2AdvancementFlags)RawBytes[Offsets.Awards + 4];
            Quests1 = (MM2QuestFlags1)((RawBytes[Offsets.Awards + 5] << 16) | (RawBytes[Offsets.Awards + 6] << 8) | RawBytes[Offsets.Awards + 7]);
            Arena = (MM2ArenaFlags)((RawBytes[Offsets.Awards + 8] << 8) | RawBytes[Offsets.Awards + 9]);
            Quests2 = (MM2QuestFlags2)((RawBytes[Offsets.Awards + 10] << 8) | RawBytes[Offsets.Awards + 11]);
        }

        public byte SkillByte { get { return (byte) ((((int) Skill2) << 4) | ((int) Skill1)); } }

        public static StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            switch (stat)
            {
                case PrimaryStat.Endurance:
                case PrimaryStat.Speed:
                case PrimaryStat.Intellect:
                case PrimaryStat.Personality:
                    return StatModifier.FromTable(value, stat, 14, 0, 16, 1, 18, 2, 20, 3, 23, 4, 27, 5, 31, 6, 46, 7, 61, 8,
                        76, 9, 91, 10, 106, 11, 121, 12, 136, 13, 151, 14, 176, 15, 201, 16, 226, 17, 251, 18, 19);
                default:
                    return StatModifier.FromTable(value, stat, 5, -3, 7, -2, 10, -1, 14, 0, 16, 1, 18, 2, 20, 3, 23, 4, 27, 5, 31, 6, 46, 7, 61, 8,
                        76, 9, 91, 10, 106, 11, 121, 12, 136, 13, 151, 14, 176, 15, 201, 16, 226, 17, 251, 18, 19);
            }
        }

        public override ResistanceValue[] GetResistances()
        {
            return new ResistanceValue[] {
                new ResistanceValue(GenericResistanceFlags.Magic, MagicResist),
                new ResistanceValue(GenericResistanceFlags.Fire, FireResist),
                new ResistanceValue(GenericResistanceFlags.Electricity, ElecResist),
                new ResistanceValue(GenericResistanceFlags.Cold, ColdResist),
                new ResistanceValue(GenericResistanceFlags.Energy, EnergyResist),
                new ResistanceValue(GenericResistanceFlags.Sleep, SleepResist),
                new ResistanceValue(GenericResistanceFlags.Poison, PoisonResist),
                new ResistanceValue(GenericResistanceFlags.Acid, AcidResist)
            };
        }

        public override string Name { get { return CharName; } }

        public static MM2Sex SexFromByte(byte b)
        {
            if (b >= (byte)MM2Sex.Male && b <= (byte)MM2Sex.Female)
                return (MM2Sex)b;
            return MM2Sex.None;
        }

        public static MM2Race RaceFromByte(byte b)
        {
            if (b >= (byte)MM2Race.Human && b <= (byte)MM2Race.HalfOrc)
                return (MM2Race)b;
            return MM2Race.None;
        }

        public static MM2Class ClassFromByte(byte b)
        {
            if (b >= (byte)MM2Class.Knight && b <= (byte)MM2Class.Barbarian)
                return (MM2Class)b;
            return MM2Class.None;
        }
        public static string SexString(MM2Sex sex)
        {
            switch (sex)
            {
                case MM2Sex.Male: return "Male";
                case MM2Sex.Female: return "Female";
                default: return "None";
            }
        }

        public bool AllStatsTemp(int iVal)
        {
            return (Might.Temporary == iVal &&
                Intellect.Temporary == iVal &&
                Personality.Temporary == iVal &&
                Endurance.Temporary == iVal &&
                Speed.Temporary == iVal &&
                Accuracy.Temporary == iVal &&
                Luck.Temporary == iVal);
        }

        public static string AlignmentString(MM2AlignmentValue align)
        {
            switch (align)
            {
                case MM2AlignmentValue.Good: return "Good";
                case MM2AlignmentValue.Neutral: return "Neutral";
                case MM2AlignmentValue.Evil: return "Evil";
                default: return "None";
            }
        }

        public static string RaceString(MM2Race race)
        {
            switch (race)
            {
                case MM2Race.Dwarf: return "Dwarf";
                case MM2Race.Elf: return "Elf";
                case MM2Race.Gnome: return "Gnome";
                case MM2Race.HalfOrc: return "Half-Orc";
                case MM2Race.Human: return "Human";
                default: return "None";
            }
        }
        public static string ClassString(MM2Class classenum)
        {
            switch (classenum)
            {
                case MM2Class.Archer: return "Archer";
                case MM2Class.Cleric: return "Cleric";
                case MM2Class.Knight: return "Knight";
                case MM2Class.Paladin: return "Paladin";
                case MM2Class.Robber: return "Robber";
                case MM2Class.Sorcerer: return "Sorcerer";
                case MM2Class.Ninja: return "Ninja";
                case MM2Class.Barbarian: return "Barbarian";
                default: return "None";
            }
        }

        public static string ConditionString(MM2Condition cond, bool bIncludeGood)
        {
            if (cond == MM2Condition.Good)
                return bIncludeGood ? "Good" : "";

            StringBuilder sb = new StringBuilder();

            if (cond.HasFlag(MM2Condition.SevereFlag))
            {
                if (cond == MM2Condition.Stone)
                    sb.Append("Stone");
                else if (cond == MM2Condition.Dead)
                    sb.Append("Dead");
                else
                    sb.Append("Eradicated");
            }
            else
            {
                if (cond.HasFlag(MM2Condition.Unconscious))
                    sb.Append("Unconscious, ");
                if (cond.HasFlag(MM2Condition.Paralyzed))
                    sb.Append("Paralyzed, ");
                if (cond.HasFlag(MM2Condition.Poisoned))
                    sb.Append("Poisoned, ");
                if (cond.HasFlag(MM2Condition.Diseased))
                    sb.Append("Diseased, ");
                if (cond.HasFlag(MM2Condition.Silenced))
                    sb.Append("Silenced, ");
                if (cond.HasFlag(MM2Condition.Cursed))
                    sb.Append("Cursed, ");
                if (cond.HasFlag(MM2Condition.Asleep))
                    sb.Append("Asleep, ");
            }

            return Global.Trim(sb).ToString();
        }

        public int NumAttacks
        {
            get
            {
                switch (Class)
                {
                    case MM2Class.Knight:
                    case MM2Class.Paladin:
                    case MM2Class.Barbarian:
                        return Level.Temporary / 4 + 1;
                    case MM2Class.Archer:
                    case MM2Class.Robber:
                    case MM2Class.Ninja:
                        return Level.Temporary / 5 + 1;
                    case MM2Class.Cleric:
                        return Level.Temporary / 7 + 1;
                    case MM2Class.Sorcerer:
                        return Level.Temporary / 10 + 1;
                    default:
                        return 1;
                }
            }
        }

        public static string SecondarySkillName(MM2SecondarySkill skill)
        {
            switch(skill)
            {
                case MM2SecondarySkill.ArmsMaster: return "Arms Master";
                case MM2SecondarySkill.Athlete: return "Athlete";
                case MM2SecondarySkill.Cartographer: return "Cartographer";
                case MM2SecondarySkill.Crusader: return "Crusader";
                case MM2SecondarySkill.Diplomat: return "Diplomat";
                case MM2SecondarySkill.Gambler: return "Gambler";
                case MM2SecondarySkill.Gladiator: return "Gladiator";
                case MM2SecondarySkill.HeroHeroine: return "Hero/Heroine";
                case MM2SecondarySkill.Linguist: return "Linguist";
                case MM2SecondarySkill.Merchant: return "Merchant";
                case MM2SecondarySkill.Mountaineer: return "Mountaineer";
                case MM2SecondarySkill.Navigator: return "Navigator";
                case MM2SecondarySkill.Pathfinder: return "Pathfinder";
                case MM2SecondarySkill.PickPocket: return "PickPocket";
                case MM2SecondarySkill.Soldier: return "Soldier";
                default: return "None";
            }
        }

        public static string SecondarySkillDescription(MM2SecondarySkill skill)
        {
            switch (skill)
            {
                case MM2SecondarySkill.ArmsMaster: return "+5 Accuracy";
                case MM2SecondarySkill.Athlete: return "+5 Speed";
                case MM2SecondarySkill.Cartographer: return "Enable the internal automap";
                case MM2SecondarySkill.Crusader: return "Allows the party to be bestowed quests";
                case MM2SecondarySkill.Diplomat: return "+5 Personality";
                case MM2SecondarySkill.Gambler: return "+5 Luck";
                case MM2SecondarySkill.Gladiator: return "+5 Might";
                case MM2SecondarySkill.HeroHeroine: return "+1 to all primary statistics";
                case MM2SecondarySkill.Linguist: return "+5 Intellect";
                case MM2SecondarySkill.Merchant: return "Purchase at 50% and sell for double the non-merchant price";
                case MM2SecondarySkill.Mountaineer: return "Allow passage through mountains if two characters have this";
                case MM2SecondarySkill.Navigator: return "Prevents the party from getting lost in open areas";
                case MM2SecondarySkill.Pathfinder: return "Allow passage through dense forest if two characters have this";
                case MM2SecondarySkill.PickPocket: return "+5 Thievery";
                case MM2SecondarySkill.Soldier: return "+5 Endurance";
                default: return "None";
            }
        }

        public string SkillString
        {
            get
            {
                if (Skill1 == MM2SecondarySkill.None && Skill2 == MM2SecondarySkill.None)
                    return "None";
                if (Skill1 == MM2SecondarySkill.None)
                    return SecondarySkillName(Skill2);
                if (Skill2 == MM2SecondarySkill.None)
                    return SecondarySkillName(Skill1);
                return String.Format("{0}, {1}", SecondarySkillName(Skill1), SecondarySkillName(Skill2));
            }
        }

        public override string MeleeDamageString
        {
            get
            {
                int iNumAttacks = NumAttacks;
                return String.Format("{0}{1}", iNumAttacks > 1 ? iNumAttacks.ToString() + "x" : "", MeleeDamage.ToString());
            }
        }

        public override string RangedDamageString
        {
            get
            {
                int iNumAttacks = NumAttacks;
                return String.Format("{0}{1}", iNumAttacks > 1 ? iNumAttacks.ToString() + "x" : "", RangedDamage.ToString());
            }
        }

        public static string ConditionDescription(MM2Condition cond)
        {
            if (cond == MM2Condition.Good)
                return "Good: Character is healthy\n";
            if (cond == MM2Condition.Eradicated)
                return "Eradicated: Cannot perform actions and gains no XP\n";

            StringBuilder sb = new StringBuilder();
            if (cond.HasFlag(MM2Condition.Asleep))
                sb.AppendLine("Asleep: Cannot perform actions until attacked");
            if (cond.HasFlag(MM2Condition.Silenced))
                sb.AppendLine("Silenced: Cannot cast spells");
            if (cond.HasFlag(MM2Condition.Diseased))
                sb.AppendLine("Diseased: Gain no HP when resting");
            if (cond.HasFlag(MM2Condition.Poisoned))
                sb.AppendLine("Poisoned: Max HP halved when resting");
            else if (cond.HasFlag(MM2Condition.Paralyzed))
                sb.AppendLine("Paralyzed: Cannot perform actions");
            else if (cond.HasFlag(MM2Condition.Unconscious))
                sb.AppendLine("Unconscious: Cannot perform actions and dies if attacked");

            return sb.ToString();
        }

        public string SecondarySkillsDescription
        {
            get
            {
                if (Skill1 == MM2SecondarySkill.None && Skill2 == MM2SecondarySkill.None)
                    return "No secondary skills have been learned.";

                StringBuilder sb = new StringBuilder();
                if (Skill1 != MM2SecondarySkill.None)
                    sb.AppendFormat("{0}: {1}\n", MM2Character.SecondarySkillName(Skill1), MM2Character.SecondarySkillDescription(Skill1));
                if (Skill2 != MM2SecondarySkill.None)
                    sb.AppendFormat("{0}: {1}\n", MM2Character.SecondarySkillName(Skill2), MM2Character.SecondarySkillDescription(Skill2));

                return sb.ToString().Trim();
            }
        }

        public override long QuickRefExperience { get { return Experience; } }
        public override MMHitPoints QuickRefHitPoints { get { return HitPoints; } }
        public override SpellPoints QuickRefSpellPoints { get { return SpellPoints; } }
        public override OneByteStat QuickRefSpeed { get { return Speed; } }
        public override OneByteStat QuickRefSpellLevel { get { return SpellLevel; } }
        public override int QuickRefGems { get { return Gems; } }
        public override string QuickRefCondition { get { return MM2Character.ConditionString(Condition, false); } }
        public override bool IsHealer { get { return (Class == MM2Class.Paladin || Class == MM2Class.Cleric); } }


        public override string ResistancesString
        {
            get
            {
                return String.Format("Magic: {0}, Fire: {1}, Energy: {2}, Cold: {3}, Poison: {4}, Elec: {5}, Sleep: {6}, Acid: {7}",
                    MagicResist,
                    FireResist,
                    EnergyResist,
                    ColdResist,
                    PoisonResist,
                    ElecResist,
                    SleepResist,
                    AcidResist
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
                return String.Format("{0}{1}", Experience, ReadyToTrain ? " (Train!)" : ("/" + XPForNextLevel.ToString()));
            }
        }

        public override int MaxLevel { get { return 256; } }    // MM2 lets you train to level 255 repeatedly

        public override bool ReadyToTrain
        {
            get
            {
                return NeedsXP < 1;
            }
        }

        public static long XPForLevel(MM2Class mm2Class, int iLevel)
        {
            long iBase = 1500;
            switch (mm2Class)
            {
                case MM2Class.Archer:
                case MM2Class.Paladin:
                case MM2Class.Sorcerer:
                case MM2Class.Ninja:
                    iBase = 2000;
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
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                        iBase *= 2;
                        break;
                    case 11:
                        iBase = 192000;
                        break;
                    case 14:
                        iBase = 384000;
                        break;
                    case 16:
                        iBase = 768000;
                        break;
                    case 21:
                        iBase = 1536000;
                        break;
                    case 31:
                        iBase = 3072000;
                        break;
                    case 51:
                        iBase = 1638400;
                        break;
                    case 76:
                        iBase = 6144000;
                        break;
                    default:
                        break;
                }
                iRequired += iBase;
            }
            return iRequired;
        }

        public override long XPForNextLevel { get { return Level == null ? 0 : XPForLevel(Class, Level.Permanent+1); } }

        public override long NeedsXP
        {
            get
            {
                return XPForNextLevel - Experience;
            }
        }

        public override long BasicExperience { get { return Experience; } }

        public override long XPForLevel(GenericClass mmClass, int iLevel)
        {
            return XPForLevel(ClassForGeneric(mmClass), iLevel);
        }

        public static int BaseHPForClass(MM2Class mm2Class)
        {
            switch (mm2Class)
            {
                case MM2Class.Sorcerer:
                    return 6;
                case MM2Class.Cleric:
                case MM2Class.Robber:
                case MM2Class.Ninja:
                    return 8;
                case MM2Class.Archer:
                case MM2Class.Paladin:
                    return 10;
                case MM2Class.Knight:
                    return 12;
                case MM2Class.Barbarian:
                    return 15;
                default:
                    return 0;
            }
        }

        public static int LevelUpHPForClass(MM2Class mm2Class, int iEndurance, MM2Map town)
        {
            int iEndBonus = MM2Character.GetStatModifier(iEndurance, PrimaryStat.Endurance).Value;
            int[] townBonuses = null;

            switch (town)
            {
                case MM2Map.C2Middlegate:
                    townBonuses = new int[] { 0, 1, 2, 3, 5};
                    break;
                case MM2Map.E4Sandsobar:
                case MM2Map.A1Tundara:
                    townBonuses = new int[] { 1, 2, 3, 5, 7};
                    break;
                case MM2Map.E1Vulcania:
                    townBonuses = new int[] { 2, 3, 5, 6, 9 };
                    break;
                case MM2Map.A4Atlantium:
                    townBonuses = new int[] { 3, 5, 7, 9, 12 };
                    break;
                default:
                    townBonuses = new int[] { 0, 0, 0, 0, 0 };
                    break;
            }

            switch (mm2Class)
            {
                case MM2Class.Sorcerer:
                    return 3 + iEndBonus + townBonuses[0];
                case MM2Class.Cleric:
                case MM2Class.Robber:
                case MM2Class.Ninja:
                    return 3 + iEndBonus + townBonuses[1];
                case MM2Class.Archer:
                case MM2Class.Paladin:
                    return 3 + iEndBonus + townBonuses[2];
                case MM2Class.Knight:
                    return 3 + iEndBonus + townBonuses[3];
                case MM2Class.Barbarian:
                    return 3 + iEndBonus + townBonuses[4];
                default:
                    return 0;
            }
        }

        public string HPLevelString(MM2Map town)
        {
            return String.Format("{0}", LevelUpHPForClass(Class, Endurance.Permanent, town));
        }

        public override string EquippedString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (MM2Item item in Inventory.EquippedItems)
                {
                    if (item.Index != 0)
                        sb.AppendFormat("{0}, ", item.Name);
                }
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
                foreach (MM2Item item in Inventory.BackpackItems)
                    if (item.Index != 0)
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
                if (Level == null)
                    return "<Invalid Character Record>";
                return String.Format("Level {0} {1} {2} {3} {4}, {5} old",
                 Level.ToString(),
                 MM2Character.SexString(Sex),
                 Alignment.ToString(),
                 MM2Character.RaceString(Race),
                 MM2Character.ClassString(Class),
                 MM2Character.AgeString(Age, AgeDays));
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
                sb.AppendFormat("Condition: {0}\n", MM2Character.ConditionString(Condition, true));
                sb.AppendFormat("HP: {0}\n", HitPoints.ToString());
                sb.AppendFormat("SP: {0} ({1})\n", SpellPoints.ToString(), SpellLevel);
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
        public override MMSex BasicSex
        {
            get
            {
                switch (Sex)
                {
                    case MM2Sex.Male: return MMSex.Male;
                    case MM2Sex.Female: return MMSex.Female;
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
                    case MM2Class.Knight: return GenericClass.Knight;
                    case MM2Class.Paladin: return GenericClass.Paladin;
                    case MM2Class.Archer: return GenericClass.Archer;
                    case MM2Class.Cleric: return GenericClass.Cleric;
                    case MM2Class.Sorcerer: return GenericClass.Sorcerer;
                    case MM2Class.Robber: return GenericClass.Robber;
                    case MM2Class.Ninja: return GenericClass.Ninja;
                    case MM2Class.Barbarian: return GenericClass.Barbarian;
                    default: return GenericClass.None;
                }
            }
        }

        public override GenericAge BasicAge { get { return new GenericAge(Age, AgeDays); } }

        public override GenericRace BasicRace
        {
            get
            {
                switch (Race)
                {
                    case MM2Race.Human: return GenericRace.Human;
                    case MM2Race.Elf: return GenericRace.Elf;
                    case MM2Race.Gnome: return GenericRace.Gnome;
                    case MM2Race.Dwarf: return GenericRace.Dwarf;
                    case MM2Race.HalfOrc: return GenericRace.HalfOrc;
                    default: return GenericRace.None;
                }
            }
        }

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
                case MM2AlignmentValue.Good: return GenericAlignmentValue.Good;
                case MM2AlignmentValue.Neutral: return GenericAlignmentValue.Neutral;
                case MM2AlignmentValue.Evil: return GenericAlignmentValue.Evil;
                default: return GenericAlignmentValue.None;
            }
        }

        public override bool KnowsSpell(Spell spell)
        {
            if (!(spell is MM2Spell))
                return false;

            MM2Spell mm2Spell = spell as MM2Spell;

            if (Class == MM2Class.Paladin || Class == MM2Class.Cleric)
            {
                if (spell.Type != SpellType.Cleric)
                    return false;
            }
            else if (Class == MM2Class.Archer || Class == MM2Class.Sorcerer)
            {
                if (spell.Type != SpellType.Sorcerer)
                    return false;
            }
            else
                return false;

            return KnownSpells.Spells[spell.Level, spell.Number];
        }

        public override bool KnowsSpell(SpellType type, int level, int number)
        {
            if (Class == MM2Class.Paladin || Class == MM2Class.Cleric)
            {
                if (type != SpellType.Cleric)
                    return false;
            }
            else if (Class == MM2Class.Archer || Class == MM2Class.Sorcerer)
            {
                if (type != SpellType.Sorcerer)
                    return false;
            }
            else
                return false;

            return KnownSpells.Spells[level, number];
        }

        public override int NumKnownSpells
        {
            get
            {
                int iCount = 0;
                for (int i = 0; i < KnownSpells.Spells.GetLength(0); i++)
                    for (int j = 0; j < KnownSpells.Spells.GetLength(1); j++)
                        if (KnownSpells.Spells[i, j])
                            iCount++;
                return iCount;
            }
        }

        public override BasicConditionFlags BasicCondition
        {
            get
            {
                BasicConditionFlags cond = BasicConditionFlags.Good;

                if (Condition.HasFlag(MM2Condition.SevereFlag))
                {
                    if (Condition == MM2Condition.Dead)
                        return BasicConditionFlags.Dead;
                    if (Condition == MM2Condition.Stone)
                        return BasicConditionFlags.Stone;
                    return BasicConditionFlags.Eradicated;
                }

                if (Condition.HasFlag(MM2Condition.Asleep))
                    cond |= BasicConditionFlags.Asleep;
                if (Condition.HasFlag(MM2Condition.Cursed))
                    cond |= BasicConditionFlags.Cursed;
                if (Condition.HasFlag(MM2Condition.Diseased))
                    cond |= BasicConditionFlags.Diseased;
                if (Condition.HasFlag(MM2Condition.Poisoned))
                    cond |= BasicConditionFlags.Poisoned;
                if (Condition.HasFlag(MM2Condition.Silenced))
                    cond |= BasicConditionFlags.Silenced;
                if (Condition.HasFlag(MM2Condition.Paralyzed))
                    cond |= BasicConditionFlags.Paralyzed;
                if (Condition.HasFlag(MM2Condition.Unconscious))
                    cond |= BasicConditionFlags.Unconscious;

                return cond;
            }
        }

        public override byte ConditionValue(BasicConditionFlags condition)
        {
            byte bResult = 0;

            if (condition.HasFlag(BasicConditionFlags.Eradicated))
                return 0xff;    // Eradication is special; 0x80 will show as eradicated in the UI but in-game it is set (and tested) as 0xff

            if (condition.HasFlag(BasicConditionFlags.Dead))
                return (byte)MM2Condition.Dead;
            if (condition.HasFlag(BasicConditionFlags.Stone))
                return (byte)MM2Condition.Stone;

            if (condition.HasFlag(BasicConditionFlags.Asleep))
                bResult |= (byte)MM2Condition.Asleep;
            if (condition.HasFlag(BasicConditionFlags.Cursed))
                bResult |= (byte)MM2Condition.Cursed;
            if (condition.HasFlag(BasicConditionFlags.Diseased))
                bResult |= (byte)MM2Condition.Diseased;
            if (condition.HasFlag(BasicConditionFlags.Poisoned))
                bResult |= (byte)MM2Condition.Poisoned;
            if (condition.HasFlag(BasicConditionFlags.Silenced))
                bResult |= (byte)MM2Condition.Silenced;
            if (condition.HasFlag(BasicConditionFlags.Paralyzed))
                bResult |= (byte)MM2Condition.Paralyzed;
            if (condition.HasFlag(BasicConditionFlags.Unconscious))
                bResult |= (byte)MM2Condition.Unconscious;

            return bResult;
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

        public override List<Item> BackpackItems
        {
            get
            {
                return new List<Item>(Inventory.BackpackItems);
            }
        }

        public override byte SexValue(MMSex sex)
        {
            switch (sex)
            {
                case MMSex.Male: return (byte)MM2Sex.Male;
                case MMSex.Female: return (byte)MM2Sex.Female;
                default: return (byte)MM2Sex.None;
            }
        }

        public override byte AlignmentValue(GenericAlignmentValue align)
        {
            switch (align)
            {
                case GenericAlignmentValue.Evil: return (byte)MM2AlignmentValue.Evil;
                case GenericAlignmentValue.Neutral: return (byte)MM2AlignmentValue.Neutral;
                case GenericAlignmentValue.Good: return (byte)MM2AlignmentValue.Good;
                default: return (byte)MM2AlignmentValue.None;
            }
        }

        public override byte RaceValue(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return (byte)MM2Race.Human;
                case GenericRace.Elf: return (byte)MM2Race.Elf;
                case GenericRace.Dwarf: return (byte)MM2Race.Dwarf;
                case GenericRace.Gnome: return (byte)MM2Race.Gnome;
                case GenericRace.HalfOrc: return (byte)MM2Race.HalfOrc;
                default: return (byte)MM2Race.None;
            }
        }

        public static MM2Class ClassForGeneric(GenericClass mmClass)
        {
            switch (mmClass)
            {
                case GenericClass.Archer: return MM2Class.Archer;
                case GenericClass.Cleric: return MM2Class.Cleric;
                case GenericClass.Knight: return MM2Class.Knight;
                case GenericClass.Paladin: return MM2Class.Paladin;
                case GenericClass.Robber: return MM2Class.Robber;
                case GenericClass.Sorcerer: return MM2Class.Sorcerer;
                case GenericClass.Ninja: return MM2Class.Ninja;
                case GenericClass.Barbarian: return MM2Class.Barbarian;
                default: return MM2Class.None;
            }
        }

        public override byte ClassValue(GenericClass classVal)
        {
            return (byte)ClassForGeneric(classVal);
        }

        public override Item GetItem(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < 3)
                return null;

            if (bytes[offset] >= MM2.Items.Count)
                return null;

            MM2Item item = MM2.Items[bytes[offset]].Clone() as MM2Item;
            item.m_iChargesCurrent = bytes[offset + 1];
            item.BonusCurrent = (MM2BonusFlags)bytes[offset + 2];

            return item;
        }

        public override GameNames Game { get { return GameNames.MightAndMagic2; } }

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

        public bool HasSkill(MM2SecondarySkill skill)
        {
            return (Skill1 == skill || Skill2 == skill);
        }

        public override string GetACFormula(int iBless = 0)
        {
            return String.Format("{0}\tSpeed modifier", Global.AddPlus(GetStatModifier(Speed.Temporary, PrimaryStat.Speed).Value));
        }

        public override Modifiers InternalModifiers
        {
            get
            {
                Modifiers mod = MM2.Modifiers.For(BasicRace).Clone();

                // Age doesn't actually seem to have any effect on stats in MM2

                foreach (MM2Item item in Inventory.EquippedItems)
                {
                    if (item.IsArmor)
                        mod.Adjust(ModAttr.ArmorClass, item.DamageByte + item.BonusValue, item.DescriptionString);
                    ModAttr attrib = Modifiers.GetAttrib(MM2Item.GetEquipAttribute(item.Equip >> 4));
                    if (attrib != ModAttr.Invalid && item.Equip != 0xf0 && item.Equip != 0)
                        mod.Adjust(attrib, item.BonusValue + (item.Equip & 0xf), item.DescriptionString);
                }
                return mod;
            }
        }

        public override string GetMaxSPFormula()
        {
            int iBonus = 0;
            int iTotal = 0;

            string strNote = "\r\nNOTE:\tThe maximum spell points are calculated during resting.\r\n\tOnly the permanent stat value is used.";

            switch (Class)
            {
                case MM2Class.Archer:
                case MM2Class.Sorcerer:
                    iBonus = GetStatModifier(Intellect.Permanent, PrimaryStat.Intellect).Value;
                    iTotal = Level.Temporary * (3 + iBonus);
                    return String.Format("Level * (3 + IntellectBonus)\r\n{0} * (3 + {1}) = {2}{3}", Level.Temporary, iBonus, iTotal, iTotal == SpellPoints.MaximumSP ? "" : strNote);
                case MM2Class.Cleric:
                case MM2Class.Paladin:
                    iBonus = GetStatModifier(Personality.Temporary, PrimaryStat.Personality).Value;
                    iTotal = Level.Temporary * (3 + iBonus);
                    return String.Format("Level * (3 + PersonalityBonus)\r\n{0} * (3 + {1}) = {2}{3}", Level.Temporary, iBonus, iTotal, iTotal == SpellPoints.MaximumSP ? "" : strNote);
                default: return String.Empty;
            }
        }

        public override string IntellectEffect()
        {
            StatModifier mod = GetStatModifier(Intellect.Permanent, PrimaryStat.Intellect);
            switch (Class)
            {
                case MM2Class.Sorcerer:
                case MM2Class.Archer: return IntellectEffect(mod, mod.Value);
                default: return String.Empty;
            }
        }

        public override string PersonalityEffect()
        {
            StatModifier mod = GetStatModifier(Personality.Permanent, PrimaryStat.Personality);
            switch (Class)
            {
                case MM2Class.Cleric: return PersonalityEffect(mod, mod.Value);
                case MM2Class.Paladin:
                default: return String.Empty;
            }
        }

        public override string GetThieveryFormula()
        {
            StringBuilder sb = new StringBuilder();
            switch (Class)
            {
                case MM2Class.Robber:
                    sb.Append("Level + 29 (Robber)");
                    break;
                case MM2Class.Ninja:
                    sb.Append("Level + 9 (Ninja)");
                    break;
                default:
                    break;
            }

            foreach (MM2Item item in Inventory.EquippedItems)
            {
                if ((item.Equip & 0xf0) == 0xe0) // Bonus to thievery
                {
                    if (sb.Length > 0)
                        sb.AppendLine();
                    sb.AppendFormat("{0}\t{1}", Global.AddPlus((item.Equip & 0xf) + item.BonusValue), item.DescriptionString);
                }
            }

            return sb.ToString();
        }
    }

    public enum MM2Sex
    {
        Male = 0,
        Female = 1,
        None = 2,
    }

    public enum MM2AlignmentValue
    {
        Good = 0,
        Neutral = 1,
        Evil = 2,
        None = 3
    }

    public enum MM2Race
    {
        Human = 0,
        Elf = 1,
        Dwarf = 2,
        Gnome = 3,
        HalfOrc = 4,
        None = 0
    }

    public enum MM2Class
    {
        Knight = 0,
        Paladin = 1,
        Archer = 2,
        Cleric = 3,
        Sorcerer = 4,
        Robber = 5,
        Ninja = 6,
        Barbarian = 7,
        None = 0,
    }

    [Flags]
    public enum MM2Condition
    {
        Good = 0x00,
        Cursed = 0x01,
        Silenced = 0x02,
        Diseased = 0x04,
        Poisoned = 0x08,
        Asleep = 0x10,
        Paralyzed = 0x20,
        Unconscious = 0x40,
        SevereFlag = 0x80,
        Eradicated = 0x80,
        Dead = 0x81,
        Stone = 0x82,
        UnableToCast = Asleep | Silenced | Paralyzed | Unconscious | Eradicated
    }

    public enum MM2SecondarySkill
    {
        None = 0,
        ArmsMaster = 1,
        Athlete = 2,
        Cartographer = 3,
        Crusader = 4,
        Diplomat = 5,
        Gambler = 6,
        Gladiator = 7,
        HeroHeroine = 8,
        Linguist = 9,
        Merchant = 10,
        Mountaineer = 11,
        Navigator = 12,
        Pathfinder = 13,
        PickPocket = 14,
        Soldier = 15
    }

    public class MM2KnownSpells
    {
        public bool[,] Spells;

        public int Total;

        public MM2KnownSpells(byte[] bytes, int index)
        {
            Spells = new bool[10, 8];
            for (int i = 0; i < 10; i++)
                for (int j = 0; j < 8; j++)
                    Spells[i, j] = false;

            Spells[1, 1] = (bytes[index] & 0x01) > 0;
            Spells[1, 2] = (bytes[index] & 0x02) > 0;
            Spells[1, 3] = (bytes[index] & 0x04) > 0;
            Spells[1, 4] = (bytes[index] & 0x08) > 0;
            Spells[1, 5] = (bytes[index] & 0x10) > 0;
            Spells[1, 6] = (bytes[index] & 0x20) > 0;
            Spells[1, 7] = (bytes[index] & 0x40) > 0;
            Spells[2, 1] = (bytes[index] & 0x80) > 0;

            Spells[2, 2] = (bytes[index + 1] & 0x01) > 0;
            Spells[2, 3] = (bytes[index + 1] & 0x02) > 0;
            Spells[2, 4] = (bytes[index + 1] & 0x04) > 0;
            Spells[2, 5] = (bytes[index + 1] & 0x08) > 0;
            Spells[2, 6] = (bytes[index + 1] & 0x10) > 0;
            Spells[2, 7] = (bytes[index + 1] & 0x20) > 0;
            Spells[3, 1] = (bytes[index + 1] & 0x40) > 0;
            Spells[3, 2] = (bytes[index + 1] & 0x80) > 0;

            Spells[3, 3] = (bytes[index + 2] & 0x01) > 0;
            Spells[3, 4] = (bytes[index + 2] & 0x02) > 0;
            Spells[3, 5] = (bytes[index + 2] & 0x04) > 0;
            Spells[3, 6] = (bytes[index + 2] & 0x08) > 0;
            Spells[4, 1] = (bytes[index + 2] & 0x10) > 0;
            Spells[4, 2] = (bytes[index + 2] & 0x20) > 0;
            Spells[4, 3] = (bytes[index + 2] & 0x40) > 0;
            Spells[4, 4] = (bytes[index + 2] & 0x80) > 0;

            Spells[4, 5] = (bytes[index + 3] & 0x01) > 0;
            Spells[4, 6] = (bytes[index + 3] & 0x02) > 0;
            Spells[5, 1] = (bytes[index + 3] & 0x04) > 0;
            Spells[5, 2] = (bytes[index + 3] & 0x08) > 0;
            Spells[5, 3] = (bytes[index + 3] & 0x10) > 0;
            Spells[5, 4] = (bytes[index + 3] & 0x20) > 0;
            Spells[5, 5] = (bytes[index + 3] & 0x40) > 0;
            Spells[6, 1] = (bytes[index + 3] & 0x80) > 0;

            Spells[6, 2] = (bytes[index + 4] & 0x01) > 0;
            Spells[6, 3] = (bytes[index + 4] & 0x02) > 0;
            Spells[6, 4] = (bytes[index + 4] & 0x04) > 0;
            Spells[6, 5] = (bytes[index + 4] & 0x08) > 0;
            Spells[7, 1] = (bytes[index + 4] & 0x10) > 0;
            Spells[7, 2] = (bytes[index + 4] & 0x20) > 0;
            Spells[7, 3] = (bytes[index + 4] & 0x40) > 0;
            Spells[7, 4] = (bytes[index + 4] & 0x80) > 0;

            Spells[8, 1] = (bytes[index + 5] & 0x01) > 0;
            Spells[8, 2] = (bytes[index + 5] & 0x02) > 0;
            Spells[8, 3] = (bytes[index + 5] & 0x04) > 0;
            Spells[8, 4] = (bytes[index + 5] & 0x08) > 0;
            Spells[9, 1] = (bytes[index + 5] & 0x10) > 0;
            Spells[9, 2] = (bytes[index + 5] & 0x20) > 0;
            Spells[9, 3] = (bytes[index + 5] & 0x40) > 0;
            Spells[9, 4] = (bytes[index + 5] & 0x80) > 0;

            Total = 0;
            for (int i = 0; i < 6; i++)
            {
                byte b = bytes[index+i];
                for (int j = 0; j < 8; j++)
                {
                    if ((b & 0x01) == 1)
                        Total++;
                    b >>= 1;
                }
            }
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[6] { 0,0,0,0,0,0 };

            bytes[0] |= Spells[1, 1] ? (byte)0x01 : (byte)0;
            bytes[0] |= Spells[1, 2] ? (byte)0x02 : (byte)0;
            bytes[0] |= Spells[1, 3] ? (byte)0x04 : (byte)0;
            bytes[0] |= Spells[1, 4] ? (byte)0x08 : (byte)0;
            bytes[0] |= Spells[1, 5] ? (byte)0x10 : (byte)0;
            bytes[0] |= Spells[1, 6] ? (byte)0x20 : (byte)0;
            bytes[0] |= Spells[1, 7] ? (byte)0x40 : (byte)0;
            bytes[0] |= Spells[2, 1] ? (byte)0x80 : (byte)0;

            bytes[1] |= Spells[2, 2] ? (byte)0x01 : (byte)0;
            bytes[1] |= Spells[2, 3] ? (byte)0x02 : (byte)0;
            bytes[1] |= Spells[2, 4] ? (byte)0x04 : (byte)0;
            bytes[1] |= Spells[2, 5] ? (byte)0x08 : (byte)0;
            bytes[1] |= Spells[2, 6] ? (byte)0x10 : (byte)0;
            bytes[1] |= Spells[2, 7] ? (byte)0x20 : (byte)0;
            bytes[1] |= Spells[3, 1] ? (byte)0x40 : (byte)0;
            bytes[1] |= Spells[3, 2] ? (byte)0x80 : (byte)0;

            bytes[2] |= Spells[3, 3] ? (byte)0x01 : (byte)0;
            bytes[2] |= Spells[3, 4] ? (byte)0x02 : (byte)0;
            bytes[2] |= Spells[3, 5] ? (byte)0x04 : (byte)0;
            bytes[2] |= Spells[3, 6] ? (byte)0x08 : (byte)0;
            bytes[2] |= Spells[4, 1] ? (byte)0x10 : (byte)0;
            bytes[2] |= Spells[4, 2] ? (byte)0x20 : (byte)0;
            bytes[2] |= Spells[4, 3] ? (byte)0x40 : (byte)0;
            bytes[2] |= Spells[4, 4] ? (byte)0x80 : (byte)0;

            bytes[3] |= Spells[4, 5] ? (byte)0x01 : (byte)0;
            bytes[3] |= Spells[4, 6] ? (byte)0x02 : (byte)0;
            bytes[3] |= Spells[5, 1] ? (byte)0x04 : (byte)0;
            bytes[3] |= Spells[5, 2] ? (byte)0x08 : (byte)0;
            bytes[3] |= Spells[5, 3] ? (byte)0x10 : (byte)0;
            bytes[3] |= Spells[5, 4] ? (byte)0x20 : (byte)0;
            bytes[3] |= Spells[5, 5] ? (byte)0x40 : (byte)0;
            bytes[3] |= Spells[6, 1] ? (byte)0x80 : (byte)0;

            bytes[4] |= Spells[6, 2] ? (byte)0x01 : (byte)0;
            bytes[4] |= Spells[6, 3] ? (byte)0x02 : (byte)0;
            bytes[4] |= Spells[6, 4] ? (byte)0x04 : (byte)0;
            bytes[4] |= Spells[6, 5] ? (byte)0x08 : (byte)0;
            bytes[4] |= Spells[7, 1] ? (byte)0x10 : (byte)0;
            bytes[4] |= Spells[7, 2] ? (byte)0x20 : (byte)0;
            bytes[4] |= Spells[7, 3] ? (byte)0x40 : (byte)0;
            bytes[4] |= Spells[7, 4] ? (byte)0x80 : (byte)0;

            bytes[5] |= Spells[8, 1] ? (byte)0x01 : (byte)0;
            bytes[5] |= Spells[8, 2] ? (byte)0x02 : (byte)0;
            bytes[5] |= Spells[8, 3] ? (byte)0x04 : (byte)0;
            bytes[5] |= Spells[8, 4] ? (byte)0x08 : (byte)0;
            bytes[5] |= Spells[9, 1] ? (byte)0x10 : (byte)0;
            bytes[5] |= Spells[9, 2] ? (byte)0x20 : (byte)0;
            bytes[5] |= Spells[9, 3] ? (byte)0x40 : (byte)0;
            bytes[5] |= Spells[9, 4] ? (byte)0x80 : (byte)0;

            return bytes;
        }

        public string Level1String
        {
            get
            {
                return String.Format("{0}{1}{2}{3}{4}{5}{6}", Spells[1, 1] ? "1" : ".", Spells[1, 2] ? "2" : ".", Spells[1, 3] ? "3" : ".",
                    Spells[1, 4] ? "4" : ".", Spells[1, 5] ? "5" : ".", Spells[1, 6] ? "6" : ".", Spells[1, 7] ? "7" : ".");
            }
        }

        public string Level2String
        {
            get
            {
                return String.Format("{0}{1}{2}{3}{4}{5}{6}", Spells[2, 1] ? "1" : ".", Spells[2, 2] ? "1" : ".", Spells[2, 3] ? "3" : ".",
                    Spells[2, 4] ? "4" : ".", Spells[2, 5] ? "5" : ".", Spells[2, 6] ? "6" : ".", Spells[2, 7] ? "7" : ".");
            }
        }

        public string Level3String
        {
            get
            {
                return String.Format("{0}{1}{2}{3}{4}{5}", Spells[3, 1] ? "1" : ".", Spells[3, 2] ? "2" : ".", Spells[3, 3] ? "3" : ".",
                    Spells[3, 4] ? "4" : ".", Spells[3, 5] ? "5" : ".", Spells[3, 6] ? "6" : ".");
            }
        }

        public string Level4String
        {
            get
            {
                return String.Format("{0}{1}{2}{3}{4}{5}", Spells[4, 1] ? "1" : ".", Spells[4, 2] ? "2" : ".", Spells[4, 3] ? "3" : ".",
                    Spells[4, 4] ? "4" : ".", Spells[4, 5] ? "5" : ".", Spells[4, 6] ? "6" : ".");
            }
        }

        public string Level5String
        {
            get
            {
                return String.Format("{0}{1}{2}{3}{4}", Spells[5, 1] ? "1" : ".", Spells[5, 2] ? "2" : ".",
                    Spells[5, 3] ? "3" : ".", Spells[5, 4] ? "4" : ".", Spells[5, 5] ? "5" : ".");
            }
        }

        public string Level6String
        {
            get
            {
                return String.Format("{0}{1}{2}{3}{4}", Spells[5, 1] ? "1" : ".", Spells[5, 2] ? "2" : ".",
                    Spells[5, 3] ? "3" : ".", Spells[5, 4] ? "4" : ".", Spells[5, 5] ? "5" : ".");
            }
        }

        public string Level7String
        {
            get { return String.Format("{0}{1}{2}{3}", Spells[6, 1] ? "1" : ".", Spells[6, 2] ? "2" : ".", Spells[6, 3] ? "3" : ".", Spells[6, 4] ? "4" : "."); }
        }

        public string Level8String
        {
            get { return String.Format("{0}{1}{2}{3}", Spells[7, 1] ? "1" : ".", Spells[7, 2] ? "2" : ".", Spells[7, 3] ? "3" : ".", Spells[7, 4] ? "4" : "."); }
        }

        public string Level9String
        {
            get { return String.Format("{0}{1}{2}{3}", Spells[8, 1] ? "1" : ".", Spells[8, 2] ? "2" : ".", Spells[8, 3] ? "3" : ".", Spells[8, 4] ? "4" : "."); }
        }

        public override string ToString()
        {
            return String.Format("L1:{0}  L2:{1}  L3:{2}  L4:{3}  L5:{4}  L6:{5}  L7:{6}  L8:{7}  L9:{8}",
                Level1String,
                Level2String,
                Level3String,
                Level4String,
                Level5String,
                Level6String,
                Level7String,
                Level8String,
                Level9String
                );
        }
    }

    public class MM2Alignment
    {
        public MM2AlignmentValue Permanent;
        public MM2AlignmentValue Temporary;

        public MM2Alignment(byte[] bytes, int index)
        {
            Permanent = AlignmentFromByte(bytes[index]);
            Temporary = AlignmentFromByte(bytes[index + 1]);
        }

        public MM2Alignment(byte temp, byte perm)
        {
            Permanent = AlignmentFromByte(temp);
            Temporary = AlignmentFromByte(perm);
        }

        public void SetBytes(byte[] bytes, int index)
        {
            bytes[index] = (byte)Permanent;
            bytes[index + 1] = (byte)Temporary;
        }

        public static MM2AlignmentValue AlignmentFromByte(byte b)
        {
            if (b >= (byte)MM2AlignmentValue.Good && b <= (byte)MM2AlignmentValue.Evil)
                return (MM2AlignmentValue)b;
            return MM2AlignmentValue.None;
        }

        public override string ToString()
        {
            if (Permanent == Temporary)
                return MM2Character.AlignmentString(Permanent);
            return String.Format("{0} ({1})", MM2Character.AlignmentString(Temporary), MM2Character.AlignmentString(Permanent));
        }
    }

    public class MM2Inventory : Inventory
    {
        public List<MM2Item> EquippedItems;
        public List<MM2Item> BackpackItems;

        public override List<Item> Items
        {
            get
            {
                List<Item> items = new List<Item>(EquippedItems.Count + BackpackItems.Count);
                for (int i = 0; i < EquippedItems.Count; i++)
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
                List<MM2Item> equip = new List<MM2Item>(6);
                List<MM2Item> backpack = new List<MM2Item>(6);
                foreach (Item item in value)
                {
                    if (!(item is MM2Item))
                        continue;
                    if (item.WhereEquipped == EquipLocation.None && equip.Count < 6)
                        backpack.Add(item as MM2Item);
                    else if (backpack.Count < 6)
                        equip.Add(item as MM2Item);
                }

                EquippedItems = equip;
                BackpackItems = backpack;
            }
        }

        public override int NumBackpackItems { get { return BackpackItems.Count; } }

        public MM2Inventory(byte[] bytes, int index)
        {
            EquippedItems = new List<MM2Item>(6);
            BackpackItems = new List<MM2Item>(6);
            for (int i = 0; i < 6; i++)
            {
                MM2Item item = MM2.ItemList.Value.GetItem(bytes[index + i], i);
                if (item != null && item.Index != (int) MM2ItemIndex.Blank)
                {
                    item.m_iChargesCurrent = bytes[index + i + 6];
                    item.BonusCurrent = (MM2BonusFlags)bytes[index + i + 12];
                    item.WhereEquipped = EquipLocation.Slot1 + i;
                    EquippedItems.Add(item);
                }
            }
            for (int i = 18; i < 24; i++)
            {
                MM2Item item = MM2.ItemList.Value.GetItem(bytes[index + i], i-18);
                if (item != null && item.Index != (int)MM2ItemIndex.Blank)
                {
                    item.m_iChargesCurrent = bytes[index + i + 6];
                    item.BonusCurrent = (MM2BonusFlags)bytes[index + i + 12];
                    BackpackItems.Add(item);
                }
            }
        }

        public void SetBytes(byte[] bytes, int index)
        {
            for (int i = 0; i < 24; i++)
                bytes[i] = 0;

            int iIndexItem = 0;
            foreach (MM2Item item in EquippedItems)
            {
                bytes[index + iIndexItem] = (byte)item.Index;
                bytes[index + iIndexItem + 6] = item.m_iChargesCurrent;
                bytes[index + iIndexItem + 12] = (byte)item.BonusCurrent;
                iIndexItem++;
            }
            iIndexItem = 18;
            foreach (MM2Item item in BackpackItems)
            {
                bytes[index + iIndexItem] = (byte)item.Index;
                bytes[index + iIndexItem + 6] = item.m_iChargesCurrent;
                bytes[index + iIndexItem + 12] = (byte)item.BonusCurrent;
                iIndexItem++;
            }
        }

        public bool HasItem(MM2ItemIndex itemWanted)
        {
            foreach (MM2Item item in EquippedItems)
                if (item.Index == (int)itemWanted)
                    return true;
            foreach (MM2Item item in BackpackItems)
                if (item.Index == (int)itemWanted)
                    return true;
            return false;
        }
    }

    [Flags]
    public enum MM2MealsEaten
    {
        None = 0x0000,
        HorrorsDoeuvres = 0x0100,
        SoupDeGhoulWGarlicToast = 0x0200,
        DragonSteakTartar = 0x0400,
        GourmetDinnerBWyrmChopSuey = 0x0800,
        RoastPeasantUnderGlass = 0x1000,
        PhantomPuddingVeryLowCal = 0x2000,
        SizzlingSwineSoup = 0x4000,
        Reserved = 0x0080,
        RoastLegOfWyvern = 0x0001,
        PickledPixieBrains = 0x0002,
        DeepFriedTrollLiver = 0x0004,
        CreamOfKoboldSoup = 0x0008,
        LightlySaltedTongueOfToad = 0x0010,
        PureeOfGnome = 0x0020,
        DevilsFoodBrownie = 0x0040,
        RedHotWolfNippleChips = 0x8000,
        All = 0xff7f
    }

    [Flags]
    public enum MM2GuildFlags
    {
        None = 0x00,
        VisitedSpirit = 0x01,
        MiddlegateMageGuild = 0x02,
        AtlantiumMageGuild = 0x04,
        TundaranMageGuild = 0x08,
        VulcanianMageGuild = 0x10,
        SandsobarMageGuild = 0x20,
        VisitedPegasus = 0x40,
        JoinedAllGuilds = MiddlegateMageGuild | AtlantiumMageGuild | TundaranMageGuild | VulcanianMageGuild | SandsobarMageGuild,
        Advancement = 0x80
    }

    [Flags]
    public enum MM2AdvancementFlags
    {
        None = 0x00,
        AccomplishedMainGoal = 0x01,
        ClericReturnedCorakSoul = 0x01,
        ArcherDefeatedBaronWilfrey = 0x01,
        KnightDefeatedDreadKnight = 0x01,
        PaladinDefeatedFrostDragon = 0x01,
        NinjaAssassinatedDawn = 0x01,
        BarbarianDefeatedBrutalBruno = 0x01,
        RobberAccompaniedOtherClass = 0x01,
        SorcererFreedWizards = 0x03,
        SorcererCodeEvilRight = 0x04,
        SorcererCodeEvilLeft = 0x08,
        SorcererCodeGoodLeft = 0x10,
        SorcererCodeGoodRight = 0x20,
        SorcererFreedEvil = 0x80,
        SorcererFreedGood = 0x40,
        All = 0xff,
    }

    [Flags]
    public enum MM2QuestFlags1
    {
        None                            = 0x000000,
        AcceptedNordonsQuest            = 0x010000,
        FinishedNordonsQuest            = 0x020000,
        AcceptedNordonnasQuest          = 0x040000,
        RescuedDrogAndSirHyron          = 0x080000,
        FinishedNordonnasQuest          = 0x100000,
        Reserved1                       = 0x200000,
        Reserved2                       = 0x400000,
        DefeatedSpazTwit                = 0x800000,
        AcceptedSlayerQuest             = 0x000100,
        DefeatedSlayersMonster          = 0x000200,
        AcceptedSlayerOrHoardallQuest   = 0x000400,
        FinishedHoardallLordsQuest      = 0x000800,
        FinishedSlayerLordsQuest        = 0x001000,
        DefeatedSerpentKing             = 0x002000,
        DefeatedQueenBeetle             = 0x004000,
        DefeatedDragonLord              = 0x008000,
        UsedCupieDoll                   = 0x000001,
        UsedInnerLimitsPool             = 0x000002,
        BoughtMurraysTicket             = 0x000004,
        OnMurraysFerry                  = 0x000008,
        UsedMurraysFerry                = 0x000010,
        DefeatedDawn                    = 0x000020,
        BrokeDragonsDominionGlass       = 0x000040,
        Reserved3                       = 0x000080    
    }

    [Flags]
    public enum MM2ArenaFlags
    {
        RedMonsterBowl = 0x0001,
        BlackArena = 0x0002,
        BlackColosseum = 0x0004,
        BlackMonsterBowl = 0x0008,
        Reserved1 = 0x0010,
        RedArena = 0x0020,
        RedColosseum = 0x0040,
        Reserved2 = 0x0080,
        GreenArena = 0x0100,
        GreenColosseum = 0x0200,
        GreenMonsterBowl = 0x0400,
        YellowArena = 0x0800,
        YellowColosseum = 0x1000,
        YellowMonsterBowl = 0x2000,
        Reserved3 = 0x4000,
        Reserved4 = 0x8000,
        BlackAll = BlackArena | BlackColosseum | BlackMonsterBowl,
        GreenAll = GreenArena | GreenColosseum | GreenMonsterBowl,
        RedAll = RedArena | RedColosseum | RedMonsterBowl,
        YellowAll = YellowArena | YellowColosseum | YellowMonsterBowl
    }

    [Flags]
    public enum MM2QuestFlags2
    {
        AcceptedLamandasQuest1 = 0x0001,
        HelpedKalohn = 0x0002,
        Reserved1 = 0x0004,
        FinishedKalohnsQuest = 0x0008,
        AcceptedLamandasQuest2 = 0x0010,
        FinishedLamandasQuest = 0x0020,
        AcceptedMurraysQuest = 0x0040,
        FinishedMurraysQuest = 0x0080,
        AcceptedPeabodysQuest = 0x0100,
        FinishedPeabodysQuest = 0x0200,
        DefeatedHorvath = 0x0400,
        AcceptedElderDruidsQuest = 0x0800,
        AcceptedHaartsQuest = 0x1000,
        FreedBlackBishop = 0x2000,
        Reserved2 = 0x4000,
        Reserved3 = 0x8000,
        AcceptedLamandasQuests = AcceptedLamandasQuest1 | AcceptedLamandasQuest2,
    }


}
