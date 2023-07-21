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
    public partial class MM45CharacterInfoControl : MMCharacterInfoControl
    {
        public MM45CharacterInfoControl(IMain main)
            : base(main)
        {
            InitializeComponent();
            m_char = new MM45Character();

            FindEditableAttributes();
        }

        public override int CharacterSize { get { return MM45Character.SizeInBytes; } }

        public override void SetInfo(PartyInfo info, int iIndex, GameInfo gameInfo, EncounterInfo encounterInfo = null)
        {
            int iBytesPerChar = CharacterSize;
            if (info != null && info.Bytes.Length < (info.Addresses[iIndex] + 1) * iBytesPerChar)
                return;

            if (info is MM45PartyInfo)
            {
                m_bytes = new byte[iBytesPerChar];
                Buffer.BlockCopy(info.Bytes, info.Addresses[iIndex] * iBytesPerChar, m_bytes, 0, iBytesPerChar);
                m_char.SetFromBytes(0, m_bytes, gameInfo, encounterInfo);
                m_iCharacterIndex = iIndex;
                m_iCharacterAddress = info.Addresses[iIndex];
                m_iCharacterPosition = info.Positions[iIndex];
                ((MM45Character) m_char).Address = m_iCharacterAddress;
            }
            else
            {
                m_iCharacterAddress = -1;
                m_iCharacterIndex = -1;
                m_iCharacterPosition = -1;
            }

            UpdateUI();
            m_formParty.OnCharacterInfoSet(m_char, iIndex);
        }

        public override void UpdateUI()
        {
            MM45Character mm4Char = m_char as MM45Character;

            CheckMonitorBackpack();

            bool bHireling = false;

            if (String.IsNullOrWhiteSpace(mm4Char.CharName))
                return;

            m_commonCtrls.labelLevel.Text = mm4Char.BasicInfoString + (bHireling ? " (hireling)" : "");
            m_commonCtrls.labelAC.Text = mm4Char.GetACString(Hacker.GetBlessValue());
            MMCommonControls.labelAccuracy.Text = mm4Char.Accuracy.AdjustedString(mm4Char.Modifiers.Accuracy);
            ListViewSelectionSaver savePack = new ListViewSelectionSaver(m_commonCtrls.lvBackpack);
            ListViewSelectionSaver saveEquip = new ListViewSelectionSaver(m_commonCtrls.lvEquipped);

            m_commonCtrls.lvBackpack.BeginUpdate();
            m_commonCtrls.lvEquipped.BeginUpdate();
            ClearInventoryLists();
            for (int i = 0; i < 36; i++)
            {
                if (mm4Char.Inventory.Items.Count > i)
                {
                    if (mm4Char.Inventory.Items[i].WhereEquipped == EquipLocation.None)
                        SetBackpackLVI(mm4Char.Inventory.Items[i], mm4Char);
                    else
                        SetEquippedLVI(mm4Char.Inventory.Items[i], mm4Char);
                }
            }
            Global.FitSingleColumn(m_commonCtrls.lvBackpack);
            Global.FitSingleColumn(m_commonCtrls.lvEquipped);
            UpdateHeaders();
            HighlightInventory(m_oldScreen, false);
            savePack.Restore();
            saveEquip.Restore();
            m_commonCtrls.lvBackpack.EndUpdate();
            m_commonCtrls.lvEquipped.EndUpdate();

            SetResistances(mm4Char.GetResistances());
            m_commonCtrls.labelCondition.Text = MM45Character.ConditionString(mm4Char.Inventory, mm4Char.Condition, true);
            string strTip = mm4Char.Condition.Description;
            if (mm4Char.Inventory.BrokenEquipped)
                strTip = strTip.TrimEnd() + "\r\nBroken Item: One or more items need repair";
            if (mm4Char.Inventory.CursedEquipped)
                strTip = strTip.TrimEnd() + "\r\nCursed Item: One or more equipped items need to be uncursed";
            m_tipCondition.SetToolTip(m_commonCtrls.labelCondition, strTip);
            m_tipCondition.ShowAlways = true;
            m_tipCondition.AutoPopDelay = 32000;

            MMCommonControls.labelEndurance.Text = mm4Char.Endurance.AdjustedString(mm4Char.Modifiers.Endurance);
            m_commonCtrls.labelExp.Text = mm4Char.ExperienceString;
            if (bHireling)
            {
                labelCost.Visible = true;
                labelCost.Text = String.Format("{0}/day", mm4Char.HirelingCost);
                labelCostHeader.Visible = true;
            }
            else
            {
                labelCost.Visible = false;
                labelCostHeader.Visible = false;
            }
            m_commonCtrls.labelHP.Text = String.Format("{0}/{1}", mm4Char.CurrentHP, Modifiers.ModString(mm4Char.MaxHPWithoutItems, mm4Char.Modifiers.HitPoints));
            MMCommonControls.labelIntellect.Text = mm4Char.Intellect.AdjustedString(mm4Char.Modifiers.Intellect);
            MMCommonControls.labelLuck.Text = mm4Char.Luck.AdjustedString(mm4Char.Modifiers.Luck);
            MMCommonControls.labelMight.Text = mm4Char.Might.AdjustedString(mm4Char.Modifiers.Might);
            MMCommonControls.labelPersonality.Text = mm4Char.Personality.AdjustedString(mm4Char.Modifiers.Personality);
            m_commonCtrls.labelSP.Text = String.Format("{0}/{1}", mm4Char.CurrentSP, Modifiers.ModString(mm4Char.MaxSPWithoutItems, mm4Char.Modifiers.SpellPoints));
            MMCommonControls.labelSpeed.Text = mm4Char.Speed.AdjustedString(mm4Char.Modifiers.Speed);
            m_commonCtrls.labelMelee.Text = mm4Char.MeleeDamageString;
            MMCommonControls.labelRanged.Text = mm4Char.RangedDamageString;
            MMCommonControls.labelThievery.Text = Modifiers.ModString(mm4Char.Thievery, mm4Char.Modifiers.Thievery);
            labelKnownSpells.Text = mm4Char.Spells.KnownString(mm4Char.BasicClass);
            m_tipSkills.SetToolTip(labelSecondarySkills, mm4Char.Skills.MultiLineDescription);
            m_tipSkills.ShowAlways = true;
            m_tipSkills.AutoPopDelay = 32000;
            labelSecondarySkills.Text = mm4Char.Skills.ToString();
            labelReadySpell.Text = mm4Char.ReadySpellString;

            labelBeacon.Text = mm4Char.Beacon.ToString(Hacker as MM45MemoryHacker);

            m_commonCtrls.llCureAll.Visible = mm4Char.IsHealer && Properties.Settings.Default.EnableMemoryWrite;
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

            if (label == labelReadySpell && m_char.IsCaster)
            {
                menuFlags = CheatMenuFlags.Edit;
                m_cheatType = AttributeType.ReadySpell;
                m_cheatOffsets = new int[] { MM45.Offsets.ReadySpell };
            }
            else if (label == labelSecondarySkills)
            {
                menuFlags = CheatMenuFlags.Edit;
                m_cheatType = AttributeType.SecondarySkills;
                m_cheatOffsets = Global.IntRange(MM45.Offsets.Skills, MM45.Offsets.SkillsLength);
            }
            else if (label == labelKnownSpells)
            {
                m_cheatType = AttributeType.KnownSpells;
                menuFlags = CheatMenuFlags.Edit;
                m_cheatOffsets = Global.IntRange(MM45.Offsets.Spells, MM45.Offsets.SpellsLength);
            }
            else if (label == m_commonCtrls.labelCondition)
            {
                m_cheatType = AttributeType.Condition;
                menuFlags = CheatMenuFlags.Edit;
                m_cheatOffsets = Global.IntRange(MM45.Offsets.Condition, MM45.Offsets.ConditionLength);
            }
            else if (label == labelBeacon)
            {
                m_cheatType = AttributeType.MapAndPosition;
                menuFlags = CheatMenuFlags.Edit | CheatMenuFlags.Add1 | CheatMenuFlags.Subtract1;
                m_cheatOffsets = new int[] { MM45.Offsets.Beacon, MM45.Offsets.Beacon+1, MM45.Offsets.Beacon+2, MM45.Offsets.BeaconSide };
            }
            else if (m_cheatOffsets == null)
            {
                m_cheatType = AttributeType.Item;
                menuFlags = CheatMenuFlags.Edit;
                int i = -1;

                InventoryItemCounts info = ((MM45Character)m_char).InventoryInfo;

                if (label == m_commonCtrls.lvBackpack)
                {
                    if (m_commonCtrls.lvBackpack.SelectedItems.Count > 0)
                    {
                        InventoryItemTag tag = m_commonCtrls.lvBackpack.SelectedItems[0].Tag as InventoryItemTag;
                        info.ItemType = ((MM45Item)tag.Item).Base.Type;
                        i = tag.MemoryIndex * 4;
                        m_cheatOffsets = new CheatOffsets(Global.IntRange(MM45.Offsets.Inventory + i, 4), info);
                    }
                    else
                    {
                        i = 0;
                        m_cheatOffsets = new CheatOffsets(
                            Global.IntRange(MM45.Offsets.InvWeapons + info.Weapons * 4, 4).Concat(
                            Global.IntRange(MM45.Offsets.InvArmor + info.Armor * 4, 4)).Concat(
                            Global.IntRange(MM45.Offsets.InvAccessories + info.Accessories * 4, 4)).Concat(
                            Global.IntRange(MM45.Offsets.InvMisc + info.Miscellaneous * 4, 4)).ToArray(),
                            info);
                    }


                    if (i == -1)
                        m_cheatOffsets = null;
                }
                else if (label == m_commonCtrls.lvEquipped)
                {
                    if (m_commonCtrls.lvEquipped.SelectedItems.Count > 0)
                    {
                        InventoryItemTag tag = m_commonCtrls.lvEquipped.SelectedItems[0].Tag as InventoryItemTag;
                        info.ItemType = ((MM45Item)tag.Item).Base.Type;
                        i = tag.MemoryIndex * 4;

                        m_cheatOffsets = new CheatOffsets(Global.IntRange(MM45.Offsets.Inventory + i, 4), info);
                    }

                    if (i == -1)
                        m_cheatOffsets = null;
                }
            }

            if (m_cheatOffsets == null)
                return CheatMenuFlags.None;

            return menuFlags;
        }

        private void miViewEdit_Click(object sender, EventArgs e)
        {
            if (m_ctrlEdit == labelReadySpell)
            {
                MM45Character mm4Char = m_char as MM45Character;
                if (!Global.Cheats && mm4Char.NumKnownSpells < 1)
                {
                    MessageBox.Show("This character does not know any spells!", "No spells known", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                MMSpellSelectForm formSpellSelect = new MMSpellSelectForm(mm4Char);
                formSpellSelect.SpellIndex = new MMInternalSpellIndex(mm4Char.ReadySpell, mm4Char.CasterType);
                if (formSpellSelect.ShowDialog() == DialogResult.OK)
                {
                    Hacker.SetReadySpell(mm4Char.BasicAddress, formSpellSelect.SpellIndex.CorrectedIndex);
                }
            }
        }

        private void labelReadySpell_MouseUp(object sender, MouseEventArgs e)
        {
            if (Global.Cheats)
                return;     // In cheat mode you can just edit the spells

            if (!Properties.Settings.Default.EnableMemoryWrite)
                return;

            m_ctrlEdit = labelReadySpell;

            cmEdit.Show(Cursor.Position);
        }

        private void labelSecondarySkills_MouseUp(object sender, MouseEventArgs e)
        {
            if (Global.Cheats)
                return;     // In cheat mode you can just edit the skills

            m_ctrlEdit = labelSecondarySkills;

            cmView.Show(Cursor.Position);
        }

        private void miViewView_Click(object sender, EventArgs e)
        {
            if (m_ctrlEdit == labelSecondarySkills)
            {
                MM45Character mm4Char = m_char as MM45Character;
                MMSkillEditForm formSkills = new MMSkillEditForm(Hacker.Game);
                formSkills.ReadOnly = true;
                formSkills.Skills = mm4Char.Skills;
                formSkills.ShowDialog();
            }
        }

        private void llAwards_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            byte[] bytes = MM45Char.Awards.GetFlatBytes();
            EditBytesForm form = new EditBytesForm(TopLevelControl);
            form.ReadOnly = !Global.Cheats;
            form.ForceLength = true;
            form.SetDescriptionFunction(MM45Bits.AwardDescription);
            form.Bytes = bytes;
            if (form.ShowDialog() == DialogResult.OK)
            {
                MM45Awards awards = MM45Awards.FromFlatBytes(form.Bytes);
                (Hacker as MM45MemoryHacker).SetAwards(MM45Char.Address, awards.GetBytes());
            }
        }

        public MM45Character MM45Char { get { return m_char as MM45Character; } }
    }

    public class MM45CharacterOffsets : CharacterOffsets
    {
        public override int Name             { get { return 0; } }
        public override int NameTerminator   { get { return 15; } }
        public override int Sex              { get { return 16; } }
        public override int Race             { get { return 17; } }
        public override int Alignment        { get { return 18; } }
        public override int AlignmentMod     { get { return 18; } }
        public override int Class            { get { return 19; } }
        public override int Stats            { get { return 20; } }
        public override int Might            { get { return 20; } }
        public override int Intellect        { get { return 22; } }
        public override int Personality      { get { return 24; } }
        public override int Endurance        { get { return 26; } }
        public override int Speed            { get { return 28; } }
        public override int Accuracy         { get { return 30; } }
        public override int Luck             { get { return 32; } }
        public override int ArmorClassMod    { get { return 34; } }
        public override int Level            { get { return 35; } }
        public override int BirthDay         { get { return 37; } }
        public override int AgeModifier      { get { return 38; } }
        public override int Skills           { get { return 39; } }
        public override int SkillsLength     { get { return 18; } }
        public override int Awards           { get { return 57; } }
        public override int AwardsLength     { get { return 64; } }
        public override int Spells           { get { return 121; } }
        public override int SpellsLength     { get { return 39; } }
        public override int Beacon           { get { return 160; } }
        public override int SpellCaster      { get { return 163; } }
        public override int ReadySpell       { get { return 164; } }
        public override int Inventory        { get { return 166; } }
        public override int InventoryLength  { get { return 144; } }
        public override int BeaconSide       { get { return 310; } }
        public override int FireResist       { get { return 311; } }
        public override int ColdResist       { get { return 313; } }
        public override int ElecResist       { get { return 315; } }
        public override int PoisonResist     { get { return 317; } }
        public override int EnergyResist     { get { return 319; } }
        public override int MagicResist      { get { return 321; } }
        public override int Condition        { get { return 323; } }
        public override int ConditionLength  { get { return 16; } }
        public override int Town             { get { return 340; } }
        public override int CurrentHP        { get { return 342; } }
        public override int CurrentSP        { get { return 344; } }
        public override int BirthYear        { get { return 346; } }
        public override int Experience       { get { return 348; } }
        public override int InvWeapons       { get { return 166; } }
        public override int InvArmor         { get { return 202; } }
        public override int InvAccessories   { get { return 238; } }
        public override int InvMisc          { get { return 274; } }
    }

    public class MM45Character : MM345BaseCharacter
    {
        public byte[] Unknowns1;
        public MM45Awards Awards;
        public MM45Race Race;
        public byte Unknown4;
        public byte Unknown3;
        public byte Unknown2;
            
        public const int SizeInBytes = 354;
        public const byte MaxReadySpell = 39;

        public override CharacterOffsets Offsets { get { return MM45.Offsets; } }
        public override GameNames Game { get { return GameNames.MightAndMagic45; } }
        public override Inventory BasicInventory { get { return Inventory; } }

        public MM45Character()
        {
            Address = -1;
        }

        public override int CharacterSize { get { return SizeInBytes; } }

        public static MM45Character Create(byte[] bytes, int iIndex, GameInfo info, bool bRosterFile = false)
        {
            if (bytes == null || bytes.Length < iIndex + SizeInBytes)
                return null;
            MM45Character character = new MM45Character();
            character.SetCharFromStream(0, new MemoryStream(bytes, iIndex, bytes.Length - iIndex), info, null, bRosterFile);
            return character;
        }

        public static byte[] GetInventoryCharBytes()
        {
            return null;
        }

        public override ResistanceValue[] GetResistances()
        {
            return new ResistanceValue[] {
                new ResistanceValue(GenericResistanceFlags.Fire, FireResist.Permanent, FireResist.Modifier, Modifiers.Fire),
                new ResistanceValue(GenericResistanceFlags.Cold, ColdResist.Permanent, ColdResist.Modifier, Modifiers.Cold),
                new ResistanceValue(GenericResistanceFlags.Electricity, ElecResist.Permanent, ElecResist.Modifier, Modifiers.Electricity),
                new ResistanceValue(GenericResistanceFlags.Poison, PoisonResist.Permanent, PoisonResist.Modifier, Modifiers.Poison),
                new ResistanceValue(GenericResistanceFlags.Energy, EnergyResist.Permanent, EnergyResist.Modifier, Modifiers.Energy),
                new ResistanceValue(GenericResistanceFlags.Magic, MagicResist.Permanent, MagicResist.Modifier, Modifiers.Magic),
            };
        }

        public override int BasicAddress { get { return Address; } }
        public override int NumKnownSpells { get { return (Spells == null ? 0 : Spells.NumKnown); } }

        public override GenericRace BasicRace
        {
            get
            {
                switch (Race)
                {
                    case MM45Race.Human: return GenericRace.Human;
                    case MM45Race.Elf: return GenericRace.Elf;
                    case MM45Race.Gnome: return GenericRace.Gnome;
                    case MM45Race.Dwarf: return GenericRace.Dwarf;
                    case MM45Race.HalfOrc: return GenericRace.HalfOrc;
                    default: return GenericRace.None;
                }
            }
        }

        public MM45SpellRange SpellRange
        {
            get
            {
                switch (Class)
                {
                    case MM345Class.Sorcerer:
                    case MM345Class.Archer:
                        return new MM45SpellRange(MM45SpellIndex.FirstArcane, MM45SpellIndex.LastArcane, SpellType.Sorcerer);
                    case MM345Class.Cleric:
                    case MM345Class.Paladin:
                        return new MM45SpellRange(MM45SpellIndex.FirstCleric, MM45SpellIndex.LastCleric, SpellType.Cleric);
                    case MM345Class.Druid:
                    case MM345Class.Ranger:
                        return new MM45SpellRange(MM45SpellIndex.FirstDruid, MM45SpellIndex.LastDruid, SpellType.Druid);
                    default:
                        return new MM45SpellRange(MM45SpellIndex.None, MM45SpellIndex.None, SpellType.Unknown);
                }
            }
        }

        public override void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info, EncounterInfo encounterInfo = null, bool bFromRosterFile = false, byte[] itemList = null)
        {
            if (stream.Length < SizeInBytes)
                return;

            RawBytes = new byte[SizeInBytes];
            stream.Read(RawBytes, 0, SizeInBytes);

            CharName = Global.GetNullTerminatedString(RawBytes, Offsets.Name, 14);
            NameTerminator = RawBytes[Offsets.NameTerminator];
            Sex = SexFromByte(RawBytes[Offsets.Sex]);
            Race = MM45RaceFromByte(RawBytes[Offsets.Race]);
            Alignment = MM345AlignmentValue.None;
            SaveSide = RawBytes[18];
            Class = ClassFromByte(RawBytes[Offsets.Class]);
            Might = new OneByteStatModifier(RawBytes, Offsets.Might);
            Intellect = new OneByteStatModifier(RawBytes, Offsets.Intellect);
            Personality = new OneByteStatModifier(RawBytes, Offsets.Personality);
            Endurance = new OneByteStatModifier(RawBytes, Offsets.Endurance);
            Speed = new OneByteStatModifier(RawBytes, Offsets.Speed);
            Accuracy = new OneByteStatModifier(RawBytes, Offsets.Accuracy);
            Luck = new OneByteStatModifier(RawBytes, Offsets.Luck);
            ACModifier = RawBytes[Offsets.ArmorClassMod];
            Level = new OneByteStatModifier(RawBytes, Offsets.Level);
            BirthDay = RawBytes[Offsets.BirthDay];
            AgeModifier = RawBytes[Offsets.AgeModifier];
            Skills = new MM45SecondarySkills(RawBytes, Offsets.Skills);
            Awards = new MM45Awards(RawBytes, Offsets.Awards);
            Spells = new MM45KnownSpells(RawBytes, Offsets.Spells, BasicClass);
            Beacon = new MMBeacon(RawBytes, Offsets.Beacon, Offsets.BeaconSide);
            Unknown3 = RawBytes[122];
            ReadySpell = RawBytes[Offsets.ReadySpell];
            Unknown2 = RawBytes[124];
            Inventory = MM45Inventory.Create(RawBytes, 0);
            Modifiers = Inventory.GetModifiers();
            FireResist = new OneByteStatModifier(RawBytes, Offsets.FireResist);
            ColdResist = new OneByteStatModifier(RawBytes, Offsets.ColdResist);
            ElecResist = new OneByteStatModifier(RawBytes, Offsets.ElecResist);
            PoisonResist = new OneByteStatModifier(RawBytes, Offsets.PoisonResist);
            EnergyResist = new OneByteStatModifier(RawBytes, Offsets.EnergyResist);
            MagicResist = new OneByteStatModifier(RawBytes, Offsets.MagicResist);
            Condition = new MM45Condition(RawBytes, Offsets.Condition);
            Modifiers.Adjust(Condition.GetModifiers());
            Town = RawBytes[Offsets.Town];
            Unknown4 = RawBytes[292];
            CurrentHP = BitConverter.ToInt16(RawBytes, Offsets.CurrentHP);
            CurrentSP = BitConverter.ToUInt16(RawBytes, Offsets.CurrentSP);
            BirthYear = BitConverter.ToUInt16(RawBytes, Offsets.BirthYear);
            Experience = (uint) (BitConverter.ToUInt32(RawBytes, Offsets.Experience) + XPForLevel(Class, Level.Permanent));
            if (info is MM45GameInfo)
                Modifiers.Adjust(GetModifier(BasicAge, (info as MM45GameInfo).Party.Year));
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

        public override string EquippedString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (MM45Item item in Inventory.Items)
                {
                    if (item.WhereEquipped != EquipLocation.None)
                        sb.AppendFormat("{0}, ", item.DescriptionString);
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
                foreach (MM45Item item in Inventory.Items)
                {
                    if (item.WhereEquipped == EquipLocation.None)
                        sb.AppendFormat("{0}, ", item.DescriptionString);
                }
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
                string strMagicalAging = AgeModifier > 0 ? String.Format(" (+{0} years)", AgeModifier) : "";
                return String.Format("Level {0} {1} {2} {3}, Born {4}/{5}{6}",
                 Level.ToString(),
                 MM45Character.SexString(Sex),
                 MM45Character.RaceString(Race),
                 MM45Character.ClassString(Class),
                 BirthDay,
                 BirthYear,
                 strMagicalAging);
            }
        }

        protected override UInt16 MaxSPForClass(MM345Class mmClass, int iMod)
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
                case MM345Class.Paladin: return (UInt16)(Level.Temporary * (Math.Max(3 + iModPer, 1) + iMod + (Skills.PrayerMaster > 0 ? 2 : 0)) / 2);
                case MM345Class.Ranger: return (UInt16)(Level.Temporary * (Math.Max(3 + iModBoth, 1) + iMod + (Skills.Astrologer > 0 ? 2 : 0)) / 2);
                case MM345Class.Archer: return (UInt16)(Level.Temporary * (Math.Max(3 + iModInt, 1) + iMod + (Skills.Prestidigitator > 0 ? 2 : 0)) / 2);
                case MM345Class.Cleric: return (UInt16)(Level.Temporary * (Math.Max(3 + iModPer, 1) + iMod + (Skills.PrayerMaster > 0 ? 2 : 0)));
                case MM345Class.Druid: return (UInt16)(Level.Temporary * (Math.Max(3 + iModBoth, 1) + iMod + (Skills.Astrologer > 0 ? 2 : 0)));
                case MM345Class.Sorcerer: return (UInt16)(Level.Temporary * (Math.Max(3 + iModInt, 1) + iMod + (Skills.Prestidigitator > 0 ? 2 : 0)));
                default: return 0;
            }
        }

        public override bool KnowsSpell(Spell spell)
        {
            if (!(spell is MM45Spell && Spells is MM45KnownSpells))
                return false;

            if (Spells == null)
                return false;

            return ((MM45KnownSpells)Spells).IsKnown(spell as MM45Spell);
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
                sb.AppendFormat("Condition: {0}\n", MM45Character.ConditionString(Inventory, Condition, true));
                sb.AppendFormat("HP: {0}\n", QuickRefHitPoints.ToString());
                sb.AppendFormat("SP: {0}\n", QuickRefSpellPoints.ToString());
                sb.AppendFormat("AC: {0}\n", GetACString(0));
                sb.AppendFormat("Resist: {0}\n", ResistancesString);
                sb.AppendFormat("Damage: {0} melee, {1} ranged\n", MeleeDamageString, RangedDamageString);
                sb.AppendFormat("Equipped: {0}\n", EquippedString);
                sb.AppendFormat("Backpack: {0}\n", BackpackString);
                return sb.ToString();
            }
        }

        public override int MaxBackpackSize { get { return 36 - Inventory.Items.Count(i => i.IsEquipped); } }
        public override int MaxBackpackWeapons { get { return 9 - Inventory.Items.Count(i => i.IsWeapon && i.IsEquipped); } }
        public override int MaxBackpackArmor { get { return 9 - Inventory.Items.Count(i => i.IsArmor && i.IsEquipped); } }
        public override int MaxBackpackAccessories { get { return 9 - Inventory.Items.Count(i => i.IsAccessory && i.IsEquipped); } }
        public override int MaxBackpackMisc { get { return 9 - Inventory.Items.Count(i => i.IsMiscellaneous && i.IsEquipped); } }

        public override List<Item> BackpackItems
        {
            get
            {
                List<Item> items = new List<Item>();
                foreach(Item item in Inventory.Items)
                {
                    MM45Item mm45Item = item as MM45Item;
                    if (mm45Item.WhereEquipped == EquipLocation.None)
                        items.Add(item);
                }
                return items;
            }
        }

        public override Item GetItem(byte[] bytes, int offset = 0)
        {
            return MM45Item.FromBytes(bytes, offset);
        }

        public override int FirstEmptyBackpackIndex
        {
            get
            {
                if (Inventory == null)
                    return -1;
                if (Inventory.Items.Count > 36)
                    return -1;
                int[] used = new int[36];
                for(int i = 0; i < used.Length; i++)
                    used[i] = 0;
                for (int i = 0; i < Inventory.Items.Count; i++)
                    if (Inventory.Items[i].MemoryIndex > -1 && Inventory.Items[i].MemoryIndex < 36)
                        used[Inventory.Items[i].MemoryIndex] = 1;
                for (int i = 0; i < 36; i++)
                    if (used[i] == 0)
                        return i;
                return -1;
            }
        }

        public InventoryItemCounts InventoryInfo
        {
            get
            {
                MM45Inventory inv = Inventory as MM45Inventory;
                if (inv == null)
                    return null;

                return new InventoryItemCounts(inv.Weapons.Count, inv.Armor.Count, inv.Accessories.Count, inv.Misc.Count);
            }
        }

        public void SetConditionFromBytes(byte[] bytes)
        {
            Condition = new MM45Condition();
            Condition.Cursed = bytes[0];
            Condition.HeartBroken = bytes[1];
            Condition.Weak = bytes[2];
            Condition.Poisoned = bytes[3];
            Condition.Diseased = bytes[4];
            Condition.Insane = bytes[5];
            Condition.InLove = bytes[6];
            Condition.Drunk = bytes[7];
            Condition.Asleep = bytes[8];
            Condition.Depressed = bytes[9];
            Condition.Confused = bytes[10];
            Condition.Paralyzed = bytes[11];
            Condition.Unconscious = bytes[12];
            Condition.Dead = bytes[13];
            Condition.Stone = bytes[14];
            Condition.Eradicated = bytes[15];
        }

        public override string ReadySpellString { get { return MM45SpellList.GetSpellName((byte)ReadySpell, CasterType); } }

        public static string AwardString(MM45AwardIndex award)
        {
            switch(award)
            {
                case MM45AwardIndex.VertigoGuildMember: return "Vertigo Guild Member";
                case MM45AwardIndex.NightshadowGuildMember: return "Nightshadow Guild Member";
                case MM45AwardIndex.RivercityGuildMember: return "Rivercity Guild Member";
                case MM45AwardIndex.AspGuildMember: return "Asp Guild Member";
                case MM45AwardIndex.WinterkillGuildMember: return "Winterkill Guild Member";
                case MM45AwardIndex.ShangriLaGuildMember: return "Shangri-La Guild Member";
                case MM45AwardIndex.OutstandingCitizen: return "Outstanding Citizen";
                case MM45AwardIndex.RedDwarfBadgeOfCourage: return "Red Dwarf Badge of Courage";
                case MM45AwardIndex.ConvictedThief: return "Convicted Thief";
                case MM45AwardIndex.WarzoneWins: return "Warzone Wins";
                case MM45AwardIndex.SuperExplorer: return "Super Explorer";
                case MM45AwardIndex.MasterOfGolems: return "Master of Golems";
                case MM45AwardIndex.TaxmanEmeritus: return "Taxman Emeritus";
                case MM45AwardIndex.RescuedCrodo: return "Rescued Crodo";
                case MM45AwardIndex.FoundShangriLa: return "Found Shangri-La";
                case MM45AwardIndex.PrinceOfThieves: return "Prince of Thieves";
                case MM45AwardIndex.GhostslayerExtraordinaire: return "Ghostslayer Extraordinaire";
                case MM45AwardIndex.Legendary: return "Legendary";
                case MM45AwardIndex.RescuedCelia: return "Rescued Celia";
                case MM45AwardIndex.HelpedOrothin: return "Helped Orothin";
                case MM45AwardIndex.RestoredFalista: return "Restored Falista";
                case MM45AwardIndex.TurnedSeasons: return "Turned Seasons";
                case MM45AwardIndex.HelpedCarlawna: return "Helped Carlawna";
                case MM45AwardIndex.HelpedFalagar: return "Helped Falagar";
                case MM45AwardIndex.RestoredMirabeth: return "Restored Mirabeth";
                case MM45AwardIndex.HelpedDanulf: return "Helped Danulf";
                case MM45AwardIndex.SavedElves: return "Saved Elves";
                case MM45AwardIndex.CivilizedOne: return "Civilized One";
                case MM45AwardIndex.HelpedCaptainNystor: return "Helped Captain Nystor";
                case MM45AwardIndex.LiberatedPagoda: return "Liberated Pagoda";
                case MM45AwardIndex.FishermansFriend: return "Fisherman's Friend";
                case MM45AwardIndex.HelpedArie: return "Helped Arie";
                case MM45AwardIndex.FreedLigono: return "Freed Ligono";
                case MM45AwardIndex.HelpedGlom: return "Helped Glom";
                case MM45AwardIndex.HelpedHalon: return "Helped Halon";
                case MM45AwardIndex.PrincessFavorite: return "Princess' Favorite";
                case MM45AwardIndex.AppeasedBarok: return "Appeased Barok";
                case MM45AwardIndex.LoremasterOfWorms: return "Loremaster of Worms";
                case MM45AwardIndex.LoremasterOfLizards: return "Loremaster of Lizards";
                case MM45AwardIndex.LoremasterOfSerpents: return "Loremaster of Serpents";
                case MM45AwardIndex.LoremasterOfDrakes: return "Loremaster of Drakes";
                case MM45AwardIndex.LoremasterOfDragons: return "Loremaster of Dragons";
                case MM45AwardIndex.DefeatedLordXeen: return "DEFEATED LORD XEEN";
                case MM45AwardIndex.MasterOfWords: return "Master of Words";
                case MM45AwardIndex.MemberDrawkcabBrotherhood: return "Member Drawkcab Brotherhood";
                case MM45AwardIndex.ChosenOne: return "Chosen one";
                case MM45AwardIndex.DiscipleOfBark: return "Disciple of Bark";
                case MM45AwardIndex.CartographersChallenge: return "Cartographer's Challenge";
                case MM45AwardIndex.MerchantsChallenge: return "Merchant's Challenge";
                case MM45AwardIndex.SuperiorIntellect: return "Superior Intellect";
                case MM45AwardIndex.HelpedDreyfus: return "Helped Dreyfus";
                case MM45AwardIndex.ReturnedStatuettes: return "Returned Statuettes";
                case MM45AwardIndex.DrawkcabExtraordinaire: return "Drawkcab Extraordinaire";
                case MM45AwardIndex.EnchantedBridle: return "Enchanted Bridle";
                case MM45AwardIndex.HelpedKramer: return "Helped Kramer";
                case MM45AwardIndex.HelpedVespar: return "Helped Vespar";
                case MM45AwardIndex.FedNibbler: return "Fed Nibbler";
                case MM45AwardIndex.RescuedSprite: return "Rescued Sprite";
                case MM45AwardIndex.TasteTesterRoyale: return "Taste Tester Royale";
                case MM45AwardIndex.HelpedEctor: return "Helped Ector";
                case MM45AwardIndex.RestoredFountainOfYouth: return "Restored Fountain of Youth";
                case MM45AwardIndex.AwakenedFireSleeper: return "Awakened Fire Sleeper";
                case MM45AwardIndex.AwakenedAirSleeper: return "Awakened Air Sleeper";
                case MM45AwardIndex.AwakenedEarthSleeper: return "Awakened Earth Sleeper";
                case MM45AwardIndex.AwakenedWaterSleeper: return "Awakened Water Sleeper";
                case MM45AwardIndex.CleanedUpCastleview: return "Cleaned up Castleview";
                case MM45AwardIndex.RescuedJasper: return "Rescued Jasper";
                case MM45AwardIndex.HelpedNadia: return "Helped Nadia";
                case MM45AwardIndex.ExterminatedQueenRat: return "Exterminated Queen Rat";
                case MM45AwardIndex.DefeatedXenocAndMorgana: return "Defeated Xenoc and Morgana";
                case MM45AwardIndex.FreedSandro: return "Freed Sandro";
                case MM45AwardIndex.CheeredDimitri: return "Cheered Dimitri";
                case MM45AwardIndex.HelpedMegan: return "Helped Megan";
                case MM45AwardIndex.RescuedRoland: return "Rescued Roland";
                case MM45AwardIndex.RestoredCastleKalindra: return "Restored Castle Kalindra";
                case MM45AwardIndex.DefeatedSheltem: return "DEFEATED SHELTEM";
                case MM45AwardIndex.Goober: return "Goober";
                case MM45AwardIndex.SuperGoober: return "Super Goober";
                case MM45AwardIndex.HelpedCaleb: return "Helped Caleb";
                case MM45AwardIndex.SavedtheQueen: return "Saved the Queen";
                case MM45AwardIndex.FreedCorak: return "Freed Corak";
                case MM45AwardIndex.ReturnedOrb: return "Returned Orb";
                case MM45AwardIndex.PaladinsFriend: return "Paladin's Friend";
                case MM45AwardIndex.CastleviewGuildMember: return "Castleview Guild Member";
                case MM45AwardIndex.SandcasterGuildMember: return "Sandcaster Guild Member";
                case MM45AwardIndex.LakesideGuildMember: return "Lakeside Guild Member";
                case MM45AwardIndex.NecropolisGuildMember: return "Necropolis Guild Member";
                case MM45AwardIndex.OlympusGuildMember: return "Olympus Guild Member";
                default: return String.Format("Unknown({0})", (int) award);
            }
        }

        public bool CanCast(MM45InternalSpellIndex spell)
        {
            bool bCleric = (Class == MM345Class.Paladin || Class == MM345Class.Cleric);
            bool bArcane = (Class == MM345Class.Archer || Class == MM345Class.Sorcerer);
            bool bDruid = (Class == MM345Class.Ranger || Class == MM345Class.Druid);

            switch (spell)
            {
                case MM45InternalSpellIndex.AcidSpray: return bCleric || bDruid;
                case MM45InternalSpellIndex.Awaken: return bCleric || bArcane || bDruid;
                case MM45InternalSpellIndex.BeastMaster: return bCleric || bDruid;
                case MM45InternalSpellIndex.Bless: return bCleric || bDruid;
                case MM45InternalSpellIndex.Clairvoyance: return bArcane || bDruid;
                case MM45InternalSpellIndex.ColdRay: return bCleric || bDruid;
                case MM45InternalSpellIndex.CreateFood: return bCleric;
                case MM45InternalSpellIndex.CureDisease: return bCleric || bDruid;
                case MM45InternalSpellIndex.CureParalysis: return bCleric;
                case MM45InternalSpellIndex.CurePoison: return bCleric || bDruid;
                case MM45InternalSpellIndex.CureWounds: return bCleric || bDruid;
                case MM45InternalSpellIndex.DancingSword: return bArcane;
                case MM45InternalSpellIndex.DayOfProtection: return bCleric;
                case MM45InternalSpellIndex.DayOfSorcery: return bArcane;
                case MM45InternalSpellIndex.DeadlySwarm: return bCleric;
                case MM45InternalSpellIndex.DetectMonster: return bArcane;
                case MM45InternalSpellIndex.DivineIntervention: return bCleric;
                case MM45InternalSpellIndex.DragonSleep: return bArcane;
                case MM45InternalSpellIndex.ElementalStorm: return bArcane;
                case MM45InternalSpellIndex.EnchantItem: return bArcane;
                case MM45InternalSpellIndex.EnergyBlast: return bArcane || bDruid;
                case MM45InternalSpellIndex.Etherealize: return bArcane;
                case MM45InternalSpellIndex.FantasticFreeze: return bArcane;
                case MM45InternalSpellIndex.FieryFlail: return bCleric;
                case MM45InternalSpellIndex.FingerOfDeath: return bArcane;
                case MM45InternalSpellIndex.FireBall: return bArcane || bDruid;
                case MM45InternalSpellIndex.FirstAid: return bCleric || bDruid;
                case MM45InternalSpellIndex.FlyingFist: return bCleric || bDruid;
                case MM45InternalSpellIndex.FrostBite: return bCleric || bDruid;
                case MM45InternalSpellIndex.GolemStopper: return bArcane;
                case MM45InternalSpellIndex.Heroism: return bCleric || bDruid;
                case MM45InternalSpellIndex.HolyBonus: return bCleric || bDruid;
                case MM45InternalSpellIndex.HolyWord: return bCleric;
                case MM45InternalSpellIndex.Hypnotize: return bCleric;
                case MM45InternalSpellIndex.IdentifyMonster: return bArcane || bDruid;
                case MM45InternalSpellIndex.Implosion: return bArcane;
                case MM45InternalSpellIndex.Incinerate: return bArcane;
                case MM45InternalSpellIndex.Inferno: return bArcane;
                case MM45InternalSpellIndex.InsectSpray: return bArcane || bDruid;
                case MM45InternalSpellIndex.ItemToGold: return bArcane;
                case MM45InternalSpellIndex.Jump: return bArcane || bDruid;
                case MM45InternalSpellIndex.Levitate: return bArcane || bDruid;
                case MM45InternalSpellIndex.Light: return bCleric || bArcane || bDruid;
                case MM45InternalSpellIndex.LightningBolt: return bArcane || bDruid;
                case MM45InternalSpellIndex.LloydsBeacon: return bArcane || bDruid;
                case MM45InternalSpellIndex.MagicArrow: return bArcane || bDruid;
                case MM45InternalSpellIndex.MassDistortion: return bCleric;
                case MM45InternalSpellIndex.MegaVolts: return bArcane;
                case MM45InternalSpellIndex.MoonRay: return bCleric;
                case MM45InternalSpellIndex.NaturesCure: return bCleric || bDruid;
                case MM45InternalSpellIndex.Pain: return bCleric || bDruid;
                case MM45InternalSpellIndex.PoisonVolley: return bArcane;
                case MM45InternalSpellIndex.PowerCure: return bCleric || bDruid;
                case MM45InternalSpellIndex.PowerShield: return bArcane || bDruid;
                case MM45InternalSpellIndex.PrismaticLight: return bArcane;
                case MM45InternalSpellIndex.ProtFromElements: return bCleric || bDruid;
                case MM45InternalSpellIndex.RaiseDead: return bCleric;
                case MM45InternalSpellIndex.RechargeItem: return bArcane;
                case MM45InternalSpellIndex.Resurrect: return bCleric;
                case MM45InternalSpellIndex.Revitalize: return bCleric || bDruid;
                case MM45InternalSpellIndex.Shrapmetal: return bArcane || bDruid;
                case MM45InternalSpellIndex.Sleep: return bArcane || bDruid;
                case MM45InternalSpellIndex.Sparks: return bCleric || bDruid;
                case MM45InternalSpellIndex.StarBurst: return bArcane;
                case MM45InternalSpellIndex.StoneToFlesh: return bCleric;
                case MM45InternalSpellIndex.SunRay: return bCleric;
                case MM45InternalSpellIndex.SuperShelter: return bArcane;
                case MM45InternalSpellIndex.SuppressDisease: return bCleric || bDruid;
                case MM45InternalSpellIndex.SuppressPoison: return bCleric || bDruid;
                case MM45InternalSpellIndex.Teleport: return bArcane;
                case MM45InternalSpellIndex.TimeDistortion: return bArcane;
                case MM45InternalSpellIndex.TownPortal: return bCleric;
                case MM45InternalSpellIndex.ToxicCloud: return bArcane || bDruid;
                case MM45InternalSpellIndex.TurnUndead: return bCleric || bDruid;
                case MM45InternalSpellIndex.WalkOnWater: return bCleric || bDruid;
                case MM45InternalSpellIndex.WizardEye: return bArcane || bDruid;
                default: return false;
            }
        }

        public override int GetRaceModThievery(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return 0;
                case GenericRace.Elf: return 10;
                case GenericRace.Gnome: return 10;
                case GenericRace.Dwarf: return 5;
                case GenericRace.HalfOrc: return -10;
                default: return 0;
            }
        }

        public override string GetACFormula(int iBless = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}\tSpeed modifier", GetStatModifier(Speed.Temporary, PrimaryStat.Speed).PlusValue);
            if (iBless != 0)
                sb.AppendFormat("\r\n{0}\tBlessed", Global.AddPlus(iBless));
            return sb.ToString();
        }

        public override Modifiers InternalModifiers { get { return MM45.Modifiers.For(BasicRace); } }

        public override string GetACString(int iBless = 0)
        {
            return String.Format("{0}{1}{2}",
                Math.Max(0, BasicAC.Permanent),
                ACModifier != 0 ? Global.AddPlus(ACModifier) : "",
                iBless != 0 ? Global.AddPlus(iBless) : "");
        }

        public override byte RaceValue(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return (byte)MM45Race.Human;
                case GenericRace.Elf: return (byte)MM45Race.Elf;
                case GenericRace.Dwarf: return (byte)MM45Race.Dwarf;
                case GenericRace.Gnome: return (byte)MM45Race.Gnome;
                case GenericRace.HalfOrc: return (byte)MM45Race.HalfOrc;
                default: return (byte)MM45Race.None;
            }
        }
    }

    public class MM45SpellRange
    {
        public MM45SpellIndex First;
        public MM45SpellIndex Last;
        public SpellType Type;

        public MM45SpellRange(MM45SpellIndex first, MM45SpellIndex last, SpellType type)
        {
            First = first;
            Last = last;
            Type = type;
        }
    }

    public class MM45Condition : MMCondition
    {
        public MM45Condition(byte[] bytes, int index = 0)
        {
            SetFromBytes(bytes, index);
        }

        public MM45Condition()
        {
            SetZero();
        }
    }

    public class MM45SecondarySkills : MM345SecondarySkills
    {
        public MM45SecondarySkills(byte[] bytes, int index = 0)
        {
            SetFromBytes(bytes, index);
        }

        public override string WhereLearned(MMSecondarySkillIndex skill)
        {
            switch (skill)
            {
                case MMSecondarySkillIndex.ArmsMaster: return "C-3, Rivercity (30,3), 300 gold; A-4, Castle Kalindra Level 2 (3,6); E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.Astrologer: return "D-2, Castle Burlock Level 1, (10,5); D-2, Castle Burlock Level 1 (10,5); E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.BodyBuilder: return "C-3, Rivercity (30,1), 1000 gold; E-3, Sandcaster Sewer (27,3), 50000 gold; E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.Cartographer: return "F-3, Vertigo (8,16), 100 gold; A-4, Castleview (30,23), 10 gold; E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.Crusader: return "F-4, Cloudside Surface (9,3); E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.DangerSense: return "F-3, Dwarf Mine 3 (5,11); A-4, Castle Kalindra Level 1 (8,8); E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.DirectionSense: return "F-3, Dwarf Mine 5 (8,5); A-4, Castleview Sewer (3,25), 1000 gold; E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.Linguist: return "D-2, Castle Burlock Level 1, (6,5); E-3, Sandcaster (19,30), 25000 gold; E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.Merchant: return "D-2, Cloudside Surface (14,2), 6000 gold; E-3, Sandcaster (19,27), 5000 gold; E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.Mountaineer: return "C-3, Rivercity (30,30), 5000 gold; B-2, Darkside Surface (13,14), 5000 gold; E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.Navigator: return "C-3, Rivercity (22,30), 2000 gold; E-3, Sandcaster (19,5), 10000 gold; E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.PathFinder: return "F-3, Vertigo (25,26), 2000 gold; A-4, Castleview (29,27), 1500 gold; E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.PrayerMaster: return "A-2, Surface (6,13), 10000 gold; F-2, Lakeside Sewer (6,4), 10000 gold; E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.Prestidigitator: return "C-3, Tower of High Magic Level 4 (6,8); E-3, Sandcaster (19,1), 1000 gems; E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.SpotSecretDoors: return "E-3, Cloudside Surface (11,12), 500 gold; E-4, Darkside Surface (5,12), 500 gold, E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.Swimmer: return "C-3, Rivercity (19,23), 100 gold; A-4, Castleview (7,19), 250 gold; E-1, Shangri-La (0,3), 100000 gold";
                case MMSecondarySkillIndex.Thievery: return "Robbers and Ninjas start with this skill, and it cannot be learned any other way.";
                case MMSecondarySkillIndex.Tracker: return "This skill is not used in this game.";
                default: return "Unknown";
            }
        }
    }

    public enum MM45AwardIndex
    {
        VertigoGuildMember = 0,         // Offset  57, bit 0 : Vertigo Guild Member
        NightshadowGuildMember,         // Offset  58, bit 0 : Nightshadow Guild Member
        RivercityGuildMember,           // Offset  59, bit 0 : Rivercity Guild Member
        AspGuildMember,                 // Offset  60, bit 0 : Asp Guild Member
        WinterkillGuildMember,          // Offset  61, bit 0 : Winterkill Guild Member
        ShangriLaGuildMember,           // Offset  62, bit 0 : Shangri-La Guild Member
        OutstandingCitizen,             // Offset  63, bit 0 : Outstanding Citizen
        RedDwarfBadgeOfCourage,         // Offset  64, bit 0 : Red Dwarf Badge of Courage
        ConvictedThief,                 // Offset  65, bit 0 : Convicted Thief
        WarzoneWins,                    // Offset  66, bit 0 : %u Warzone Wins
        SuperExplorer,                  // Offset  67, bit 0 : Super Explorer
        MasterOfGolems,                 // Offset  68, bit 0 : Master of Golems
        TaxmanEmeritus,                 // Offset  69, bit 0 : Taxman Emeritus
        RescuedCrodo,                   // Offset  70, bit 0 : Rescued Crodo
        FoundShangriLa,                 // Offset  71, bit 0 : Found Shangri-La
        PrinceOfThieves,                // Offset  72, bit 0 : Prince of Thieves
        GhostslayerExtraordinaire,      // Offset  73, bit 0 : Ghostslayer Extraordinaire
        Legendary,                      // Offset  74, bit 0 : Legendary
        RescuedCelia,                   // Offset  75, bit 0 : Rescued Celia
        HelpedOrothin,                  // Offset  76, bit 0 : Helped Orothin
        RestoredFalista,                // Offset  77, bit 0 : Restored Falista
        TurnedSeasons,                  // Offset  78, bit 0 : Turned Seasons
        HelpedCarlawna,                 // Offset  79, bit 0 : Helped Carlawna
        HelpedFalagar,                  // Offset  80, bit 0 : Helped Falagar
        RestoredMirabeth,               // Offset  81, bit 0 : Restored Mirabeth
        HelpedDanulf,                   // Offset  82, bit 0 : Helped Danulf
        SavedElves,                     // Offset  83, bit 0 : Saved Elves
        CivilizedOne,                   // Offset  84, bit 0 : Civilized One
        HelpedCaptainNystor,            // Offset  85, bit 0 : Helped Captain Nystor
        LiberatedPagoda,                // Offset  86, bit 0 : Liberated Pagoda
        FishermansFriend,               // Offset  87, bit 0 : Fisherman's Friend
        HelpedArie,                     // Offset  88, bit 0 : Helped Arie
        FreedLigono,                    // Offset  89, bit 0 : Freed Ligono
        HelpedGlom,                     // Offset  90, bit 0 : Helped Glom
        HelpedHalon,                    // Offset  91, bit 0 : Helped Halon
        PrincessFavorite,               // Offset  92, bit 0 : Princess' Favorite
        AppeasedBarok,                  // Offset  93, bit 0 : Appeased Barok
        LoremasterOfWorms,              // Offset  94, bit 0 : Loremaster of Worms
        LoremasterOfLizards,            // Offset  95, bit 0 : Loremaster of Lizards
        LoremasterOfSerpents,           // Offset  96, bit 0 : Loremaster of Serpents
        LoremasterOfDrakes,             // Offset  97, bit 0 : Loremaster of Drakes
        LoremasterOfDragons,            // Offset  98, bit 0 : Loremaster of Dragons
        DefeatedLordXeen,               // Offset  99, bit 0 : DEFEATED LORD XEEN
        MasterOfWords,                  // Offset 100, bit 0 : Master of Words
        MemberDrawkcabBrotherhood,      // Offset 101, bit 0 : Member Drawkcab Brotherhood
        ChosenOne,                      // Offset 102, bit 0 : Chosen one
        DiscipleOfBark,                 // Offset 103, bit 0 : Disciple of Bark
        CartographersChallenge,         // Offset 104, bit 0 : Cartographer's Challenge
        MerchantsChallenge,             // Offset 105, bit 0 : Merchant's Challenge
        SuperiorIntellect,              // Offset 106, bit 0 : Superior Intellect
        HelpedDreyfus,                  // Offset 107, bit 0 : Helped Dreyfus
        ReturnedStatuettes,             // Offset 108, bit 0 : Returned Statuettes
        DrawkcabExtraordinaire,         // Offset 109, bit 0 : Drawkcab Extraordinaire
        EnchantedBridle,                // Offset 110, bit 0 : Enchanted Bridle
        HelpedKramer,                   // Offset 111, bit 0 : Helped Kramer
        HelpedVespar,                   // Offset 112, bit 0 : Helped Vespar
        FedNibbler,                     // Offset 113, bit 0 : Fed Nibbler
        RescuedSprite,                  // Offset 114, bit 0 : Rescued Sprite
        TasteTesterRoyale,              // Offset 115, bit 0 : Taste Tester Royale
        HelpedEctor,                    // Offset 116, bit 0 : Helped Ector
        RestoredFountainOfYouth,        // Offset 117, bit 0 : Restored Fountain of Youth
        AwakenedFireSleeper,            // Offset 118, bit 0 : Awakened Fire Sleeper
        AwakenedAirSleeper,             // Offset 119, bit 0 : Awakened Air Sleeper
        AwakenedEarthSleeper,           // Offset 120, bit 0 : Awakened Earth Sleeper
        AwakenedWaterSleeper,           // Offset  57, bit 4 : Awakened Water Sleeper
        CleanedUpCastleview,            // Offset  58, bit 4 : Cleaned up Castleview
        RescuedJasper,                  // Offset  59, bit 4 : Rescued Jasper
        HelpedNadia,                    // Offset  60, bit 4 : Helped Nadia
        ExterminatedQueenRat,           // Offset  61, bit 4 : Exterminated Queen Rat
        DefeatedXenocAndMorgana,        // Offset  62, bit 4 : Defeated Xenoc and Morgana
        FreedSandro,                    // Offset  63, bit 4 : Freed Sandro
        CheeredDimitri,                 // Offset  64, bit 4 : Cheered Dimitri
        HelpedMegan,                    // Offset  65, bit 4 : Helped Megan
        RescuedRoland,                  // Offset 119, bit 4 : Rescued Roland
        RestoredCastleKalindra,         // Offset  67, bit 4 : Restored Castle Kalindra
        DefeatedSheltem,                // Offset  68, bit 4 : DEFEATED SHELTEM
        Goober,                         // Offset  69, bit 4 : Goober
        SuperGoober,                    // Offset  70, bit 4 : Super Goober
        HelpedCaleb,                    // Offset  71, bit 4 : Helped Caleb
        SavedtheQueen,                  // Offset  72, bit 4 : Saved the Queen
        FreedCorak,                     // Offset  73, bit 4 : Freed Corak
        ReturnedOrb,                    // Offset 120, bit 4 : Returned Orb
        PaladinsFriend,                 // Offset  74, bit 4 : Paladin's Friend
        CastleviewGuildMember,          // Offset  75, bit 4 : Castleview Guild Member
        SandcasterGuildMember,          // Offset  76, bit 4 : Sandcaster Guild Member
        LakesideGuildMember,            // Offset  77, bit 4 : Lakeside Guild Member
        NecropolisGuildMember,          // Offset  78, bit 4 : Necropolis Guild Member
        OlympusGuildMember,             // Offset  79, bit 4 : Olympus Guild Member
        Last
    }

    public class MM45Awards
    {
        public byte VertigoGuildMember;
        public byte NightshadowGuildMember;
        public byte RivercityGuildMember;
        public byte AspGuildMember;
        public byte WinterkillGuildMember;
        public byte ShangriLaGuildMember;
        public byte OutstandingCitizen;
        public byte RedDwarfBadgeOfCourage;
        public byte ConvictedThief;
        public byte WarzoneWins;
        public byte SuperExplorer;
        public byte MasterOfGolems;
        public byte TaxmanEmeritus;
        public byte RescuedCrodo;
        public byte FoundShangriLa;
        public byte PrinceOfThieves;
        public byte GhostslayerExtraordinaire;
        public byte Legendary;
        public byte RescuedCelia;
        public byte HelpedOrothin;
        public byte RestoredFalista;
        public byte TurnedSeasons;
        public byte HelpedCarlawna;
        public byte HelpedFalagar;
        public byte RestoredMirabeth;
        public byte HelpedDanulf;
        public byte SavedElves;
        public byte CivilizedOne;
        public byte HelpedCaptainNystor;
        public byte LiberatedPagoda;
        public byte FishermansFriend;
        public byte HelpedArie;
        public byte FreedLigono;
        public byte HelpedGlom;
        public byte HelpedHalon;
        public byte PrincessFavorite;
        public byte AppeasedBarok;
        public byte LoremasterOfWorms;
        public byte LoremasterOfLizards;
        public byte LoremasterOfSerpents;
        public byte LoremasterOfDrakes;
        public byte LoremasterOfDragons;
        public byte DefeatedLordXeen;
        public byte MasterOfWords;
        public byte MemberDrawkcabBrotherhood;
        public byte ChosenOne;
        public byte DiscipleOfBark;
        public byte CartographersChallenge;
        public byte MerchantsChallenge;
        public byte SuperiorIntellect;
        public byte HelpedDreyfus;
        public byte ReturnedStatuettes;
        public byte DrawkcabExtraordinaire;
        public byte EnchantedBridle;
        public byte HelpedKramer;
        public byte HelpedVespar;
        public byte FedNibbler;
        public byte RescuedSprite;
        public byte TasteTesterRoyale;
        public byte HelpedEctor;
        public byte RestoredFountainOfYouth;
        public byte AwakenedFireSleeper;
        public byte AwakenedAirSleeper;
        public byte AwakenedEarthSleeper;

        public MM45Awards(byte[] bytes, int index = 0)
        {
            SetFromBytes(bytes, index);
        }

        public static bool IsSetFromByte(byte[] bytes, int offset, MM45AwardIndex index)
        {
            // Checks the value quickly without initializing the entire object (for QuestInfo purposes)
            if (index == MM45AwardIndex.WarzoneWins)
                return bytes[offset + (int)index] > 0;
            if (index < MM45AwardIndex.AwakenedWaterSleeper)
                return (bytes[offset + (int)index] & 0x01) == 0x01;
            return (bytes[offset + (index - MM45AwardIndex.AwakenedWaterSleeper)] & 0x10) == 0x10;
        }

        public bool IsSet(MM45AwardIndex index)
        {
            switch (index)
            {
                // Bit 0
                case MM45AwardIndex.VertigoGuildMember: return (VertigoGuildMember & 0x01) == 1;
                case MM45AwardIndex.NightshadowGuildMember: return (NightshadowGuildMember & 0x01) == 1;
                case MM45AwardIndex.RivercityGuildMember: return (RivercityGuildMember & 0x01) == 1;
                case MM45AwardIndex.AspGuildMember: return (AspGuildMember & 0x01) == 1;
                case MM45AwardIndex.WinterkillGuildMember: return (WinterkillGuildMember & 0x01) == 1;
                case MM45AwardIndex.ShangriLaGuildMember: return (ShangriLaGuildMember & 0x01) == 1;
                case MM45AwardIndex.OutstandingCitizen: return (OutstandingCitizen & 0x01) == 1;
                case MM45AwardIndex.RedDwarfBadgeOfCourage: return (RedDwarfBadgeOfCourage & 0x01) == 1;
                case MM45AwardIndex.ConvictedThief: return (ConvictedThief & 0x01) == 1;
                case MM45AwardIndex.SuperExplorer: return (SuperExplorer & 0x01) == 1;
                case MM45AwardIndex.MasterOfGolems: return (MasterOfGolems & 0x01) == 1;
                case MM45AwardIndex.TaxmanEmeritus: return (TaxmanEmeritus & 0x01) == 1;
                case MM45AwardIndex.RescuedCrodo: return (RescuedCrodo & 0x01) == 1;
                case MM45AwardIndex.FoundShangriLa: return (FoundShangriLa & 0x01) == 1;
                case MM45AwardIndex.PrinceOfThieves: return (PrinceOfThieves & 0x01) == 1;
                case MM45AwardIndex.GhostslayerExtraordinaire: return (GhostslayerExtraordinaire & 0x01) == 1;
                case MM45AwardIndex.Legendary: return (Legendary & 0x01) == 1;
                case MM45AwardIndex.RescuedCelia: return (RescuedCelia & 0x01) == 1;
                case MM45AwardIndex.HelpedOrothin: return (HelpedOrothin & 0x01) == 1;
                case MM45AwardIndex.RestoredFalista: return (RestoredFalista & 0x01) == 1;
                case MM45AwardIndex.TurnedSeasons: return (TurnedSeasons & 0x01) == 1;
                case MM45AwardIndex.HelpedCarlawna: return (HelpedCarlawna & 0x01) == 1;
                case MM45AwardIndex.HelpedFalagar: return (HelpedFalagar & 0x01) == 1;
                case MM45AwardIndex.RestoredMirabeth: return (RestoredMirabeth & 0x01) == 1;
                case MM45AwardIndex.HelpedDanulf: return (HelpedDanulf & 0x01) == 1;
                case MM45AwardIndex.SavedElves: return (SavedElves & 0x01) == 1;
                case MM45AwardIndex.CivilizedOne: return (CivilizedOne & 0x01) == 1;
                case MM45AwardIndex.HelpedCaptainNystor: return (HelpedCaptainNystor & 0x01) == 1;
                case MM45AwardIndex.LiberatedPagoda: return (LiberatedPagoda & 0x01) == 1;
                case MM45AwardIndex.FishermansFriend: return (FishermansFriend & 0x01) == 1;
                case MM45AwardIndex.HelpedArie: return (HelpedArie & 0x01) == 1;
                case MM45AwardIndex.FreedLigono: return (FreedLigono & 0x01) == 1;
                case MM45AwardIndex.HelpedGlom: return (HelpedGlom & 0x01) == 1;
                case MM45AwardIndex.HelpedHalon: return (HelpedHalon & 0x01) == 1;
                case MM45AwardIndex.PrincessFavorite: return (PrincessFavorite & 0x01) == 1;
                case MM45AwardIndex.AppeasedBarok: return (AppeasedBarok & 0x01) == 1;
                case MM45AwardIndex.LoremasterOfWorms: return (LoremasterOfWorms & 0x01) == 1;
                case MM45AwardIndex.LoremasterOfLizards: return (LoremasterOfLizards & 0x01) == 1;
                case MM45AwardIndex.LoremasterOfSerpents: return (LoremasterOfSerpents & 0x01) == 1;
                case MM45AwardIndex.LoremasterOfDrakes: return (LoremasterOfDrakes & 0x01) == 1;
                case MM45AwardIndex.LoremasterOfDragons: return (LoremasterOfDragons & 0x01) == 1;
                case MM45AwardIndex.DefeatedLordXeen: return (DefeatedLordXeen & 0x01) == 1;
                case MM45AwardIndex.MasterOfWords: return (MasterOfWords & 0x01) == 1;
                case MM45AwardIndex.MemberDrawkcabBrotherhood: return (MemberDrawkcabBrotherhood & 0x01) == 1;
                case MM45AwardIndex.ChosenOne: return (ChosenOne & 0x01) == 1;
                case MM45AwardIndex.DiscipleOfBark: return (DiscipleOfBark & 0x01) == 1;
                case MM45AwardIndex.CartographersChallenge: return (CartographersChallenge & 0x01) == 1;
                case MM45AwardIndex.MerchantsChallenge: return (MerchantsChallenge & 0x01) == 1;
                case MM45AwardIndex.SuperiorIntellect: return (SuperiorIntellect & 0x01) == 1;
                case MM45AwardIndex.HelpedDreyfus: return (HelpedDreyfus & 0x01) == 1;
                case MM45AwardIndex.ReturnedStatuettes: return (ReturnedStatuettes & 0x01) == 1;
                case MM45AwardIndex.DrawkcabExtraordinaire: return (DrawkcabExtraordinaire & 0x01) == 1;
                case MM45AwardIndex.EnchantedBridle: return (EnchantedBridle & 0x01) == 1;
                case MM45AwardIndex.HelpedKramer: return (HelpedKramer & 0x01) == 1;
                case MM45AwardIndex.HelpedVespar: return (HelpedVespar & 0x01) == 1;
                case MM45AwardIndex.FedNibbler: return (FedNibbler & 0x01) == 1;
                case MM45AwardIndex.RescuedSprite: return (RescuedSprite & 0x01) == 1;
                case MM45AwardIndex.TasteTesterRoyale: return (TasteTesterRoyale & 0x01) == 1;
                case MM45AwardIndex.HelpedEctor: return (HelpedEctor & 0x01) == 1;
                case MM45AwardIndex.RestoredFountainOfYouth: return (RestoredFountainOfYouth & 0x01) == 1;
                case MM45AwardIndex.AwakenedFireSleeper: return (AwakenedFireSleeper & 0x01) == 1;
                case MM45AwardIndex.AwakenedAirSleeper: return (AwakenedAirSleeper & 0x01) == 1;
                case MM45AwardIndex.AwakenedEarthSleeper: return (AwakenedEarthSleeper & 0x01) == 1;

                // Bit 4
                case MM45AwardIndex.AwakenedWaterSleeper: return (VertigoGuildMember & 0x10) == 0x10;
                case MM45AwardIndex.CleanedUpCastleview: return (NightshadowGuildMember & 0x10) == 0x10;
                case MM45AwardIndex.RescuedJasper: return (RivercityGuildMember & 0x10) == 0x10;
                case MM45AwardIndex.HelpedNadia: return (AspGuildMember & 0x10) == 0x10;
                case MM45AwardIndex.ExterminatedQueenRat: return (WinterkillGuildMember & 0x10) == 0x10;
                case MM45AwardIndex.DefeatedXenocAndMorgana: return (ShangriLaGuildMember & 0x10) == 0x10;
                case MM45AwardIndex.FreedSandro: return (OutstandingCitizen & 0x10) == 0x10;
                case MM45AwardIndex.CheeredDimitri: return (RedDwarfBadgeOfCourage & 0x10) == 0x10;
                case MM45AwardIndex.HelpedMegan: return (ConvictedThief & 0x10) == 0x10;
                case MM45AwardIndex.RestoredCastleKalindra: return (SuperExplorer & 0x10) == 0x10;
                case MM45AwardIndex.DefeatedSheltem: return (MasterOfGolems & 0x10) == 0x10;
                case MM45AwardIndex.Goober: return (TaxmanEmeritus & 0x10) == 0x10;
                case MM45AwardIndex.SuperGoober: return (RescuedCrodo & 0x10) == 0x10;
                case MM45AwardIndex.HelpedCaleb: return (FoundShangriLa & 0x10) == 0x10;
                case MM45AwardIndex.SavedtheQueen: return (PrinceOfThieves & 0x10) == 0x10;
                case MM45AwardIndex.FreedCorak: return (GhostslayerExtraordinaire & 0x10) == 0x10;
                case MM45AwardIndex.PaladinsFriend: return (RescuedCelia & 0x10) == 0x10;
                case MM45AwardIndex.CastleviewGuildMember: return (HelpedOrothin & 0x10) == 0x10;
                case MM45AwardIndex.SandcasterGuildMember: return (RestoredFalista & 0x10) == 0x10;
                case MM45AwardIndex.LakesideGuildMember: return (TurnedSeasons & 0x10) == 0x10;
                case MM45AwardIndex.NecropolisGuildMember: return (HelpedCarlawna & 0x10) == 0x10;
                case MM45AwardIndex.OlympusGuildMember: return (HelpedFalagar & 0x10) == 0x10;
                case MM45AwardIndex.RescuedRoland: return (AwakenedAirSleeper & 0x10) == 0x10;
                case MM45AwardIndex.ReturnedOrb: return (AwakenedEarthSleeper & 0x10) == 0x10;

                // Full byte
                case MM45AwardIndex.WarzoneWins: return (WarzoneWins > 0);

                default: return false;
            }
        }

        public byte[] GetFlatBytes()
        {
            List<byte> bytes = new List<byte>((int) MM45AwardIndex.Last);
            for (MM45AwardIndex index = MM45AwardIndex.VertigoGuildMember; index < MM45AwardIndex.Last; index++)
            {
                if (index == MM45AwardIndex.WarzoneWins)
                    bytes.Add(WarzoneWins);
                else
                    bytes.Add((byte)(IsSet(index) ? 1 : 0));
            }
            return bytes.ToArray();
        }

        public static MM45Awards FromFlatBytes(byte[] bytes)
        {
            if (bytes.Length < 88)
                return null;

            byte[] bytesCombined = new byte[MM45.Offsets.AwardsLength];
            for (MM45AwardIndex index = MM45AwardIndex.VertigoGuildMember; index <= MM45AwardIndex.AwakenedEarthSleeper; index++)
                bytesCombined[(int)index] = bytes[(int)index];
            bytesCombined[0] |= (byte) (bytes[64] == 0 ? 0 : 0x10);
            bytesCombined[1] |= (byte) (bytes[65] == 0 ? 0 : 0x10);
            bytesCombined[2] |= (byte) (bytes[66] == 0 ? 0 : 0x10);
            bytesCombined[3] |= (byte) (bytes[67] == 0 ? 0 : 0x10);
            bytesCombined[4] |= (byte) (bytes[68] == 0 ? 0 : 0x10);
            bytesCombined[5] |= (byte) (bytes[69] == 0 ? 0 : 0x10);
            bytesCombined[6] |= (byte) (bytes[70] == 0 ? 0 : 0x10);
            bytesCombined[7] |= (byte) (bytes[71] == 0 ? 0 : 0x10);
            bytesCombined[8] |= (byte) (bytes[72] == 0 ? 0 : 0x10);
            // byte #9 is WarzoneWins
            bytesCombined[62] |= (byte)(bytes[73] == 0 ? 0 : 0x10);
            bytesCombined[10] |= (byte)(bytes[74] == 0 ? 0 : 0x10);
            bytesCombined[11] |= (byte) (bytes[75] == 0 ? 0 : 0x10);
            bytesCombined[12] |= (byte) (bytes[76] == 0 ? 0 : 0x10);
            bytesCombined[13] |= (byte) (bytes[77] == 0 ? 0 : 0x10);
            bytesCombined[14] |= (byte) (bytes[78] == 0 ? 0 : 0x10);
            bytesCombined[15] |= (byte) (bytes[79] == 0 ? 0 : 0x10);
            bytesCombined[16] |= (byte) (bytes[80] == 0 ? 0 : 0x10);
            bytesCombined[63] |= (byte) (bytes[81] == 0 ? 0 : 0x10);
            bytesCombined[18] |= (byte) (bytes[82] == 0 ? 0 : 0x10);
            bytesCombined[19] |= (byte) (bytes[83] == 0 ? 0 : 0x10);
            bytesCombined[20] |= (byte) (bytes[84] == 0 ? 0 : 0x10);
            bytesCombined[21] |= (byte) (bytes[85] == 0 ? 0 : 0x10);
            bytesCombined[22] |= (byte) (bytes[86] == 0 ? 0 : 0x10);
            bytesCombined[23] |= (byte) (bytes[87] == 0 ? 0 : 0x10);
            return new MM45Awards(bytesCombined);
        }

        public void Clear()
        {
            VertigoGuildMember = 0;
            NightshadowGuildMember = 0;
            RivercityGuildMember = 0;
            AspGuildMember = 0;
            WinterkillGuildMember = 0;
            ShangriLaGuildMember = 0;
            OutstandingCitizen = 0;
            RedDwarfBadgeOfCourage = 0;
            ConvictedThief = 0;
            WarzoneWins = 0;
            SuperExplorer = 0;
            MasterOfGolems = 0;
            TaxmanEmeritus = 0;
            RescuedCrodo = 0;
            FoundShangriLa = 0;
            PrinceOfThieves = 0;
            GhostslayerExtraordinaire = 0;
            Legendary = 0;
            RescuedCelia = 0;
            HelpedOrothin = 0;
            RestoredFalista = 0;
            TurnedSeasons = 0;
            HelpedCarlawna = 0;
            HelpedFalagar = 0;
            RestoredMirabeth = 0;
            HelpedDanulf = 0;
            SavedElves = 0;
            CivilizedOne = 0;
            HelpedCaptainNystor = 0;
            LiberatedPagoda = 0;
            FishermansFriend = 0;
            HelpedArie = 0;
            FreedLigono = 0;
            HelpedGlom = 0;
            HelpedHalon = 0;
            PrincessFavorite = 0;
            AppeasedBarok = 0;
            LoremasterOfWorms = 0;
            LoremasterOfLizards = 0;
            LoremasterOfSerpents = 0;
            LoremasterOfDrakes = 0;
            LoremasterOfDragons = 0;
            DefeatedLordXeen = 0;
            MasterOfWords = 0;
            MemberDrawkcabBrotherhood = 0;
            ChosenOne = 0;
            DiscipleOfBark = 0;
            CartographersChallenge = 0;
            MerchantsChallenge = 0;
            SuperiorIntellect = 0;
            HelpedDreyfus = 0;
            ReturnedStatuettes = 0;
            DrawkcabExtraordinaire = 0;
            EnchantedBridle = 0;
            HelpedKramer = 0;
            HelpedVespar = 0;
            FedNibbler = 0;
            RescuedSprite = 0;
            TasteTesterRoyale = 0;
            HelpedEctor = 0;
            RestoredFountainOfYouth = 0;
            AwakenedFireSleeper = 0;
            AwakenedAirSleeper = 0;
            AwakenedEarthSleeper = 0;
        }

        private void SetFromBytes(byte[] bytes, int index)
        {
            if (bytes.Length < 64)
                return;

            VertigoGuildMember = bytes[index + (int)MM45AwardIndex.VertigoGuildMember];
            NightshadowGuildMember = bytes[index + (int)MM45AwardIndex.NightshadowGuildMember];
            RivercityGuildMember = bytes[index + (int)MM45AwardIndex.RivercityGuildMember];
            AspGuildMember = bytes[index + (int)MM45AwardIndex.AspGuildMember];
            WinterkillGuildMember = bytes[index + (int)MM45AwardIndex.WinterkillGuildMember];
            ShangriLaGuildMember = bytes[index + (int)MM45AwardIndex.ShangriLaGuildMember];
            OutstandingCitizen = bytes[index + (int)MM45AwardIndex.OutstandingCitizen];
            RedDwarfBadgeOfCourage = bytes[index + (int)MM45AwardIndex.RedDwarfBadgeOfCourage];
            ConvictedThief = bytes[index + (int)MM45AwardIndex.ConvictedThief];
            WarzoneWins = bytes[index + (int)MM45AwardIndex.WarzoneWins];
            SuperExplorer = bytes[index + (int)MM45AwardIndex.SuperExplorer];
            MasterOfGolems = bytes[index + (int)MM45AwardIndex.MasterOfGolems];
            TaxmanEmeritus = bytes[index + (int)MM45AwardIndex.TaxmanEmeritus];
            RescuedCrodo = bytes[index + (int)MM45AwardIndex.RescuedCrodo];
            FoundShangriLa = bytes[index + (int)MM45AwardIndex.FoundShangriLa];
            PrinceOfThieves = bytes[index + (int)MM45AwardIndex.PrinceOfThieves];
            GhostslayerExtraordinaire = bytes[index + (int)MM45AwardIndex.GhostslayerExtraordinaire];
            Legendary = bytes[index + (int)MM45AwardIndex.Legendary];
            RescuedCelia = bytes[index + (int)MM45AwardIndex.RescuedCelia];
            HelpedOrothin = bytes[index + (int)MM45AwardIndex.HelpedOrothin];
            RestoredFalista = bytes[index + (int)MM45AwardIndex.RestoredFalista];
            TurnedSeasons = bytes[index + (int)MM45AwardIndex.TurnedSeasons];
            HelpedCarlawna = bytes[index + (int)MM45AwardIndex.HelpedCarlawna];
            HelpedFalagar = bytes[index + (int)MM45AwardIndex.HelpedFalagar];
            RestoredMirabeth = bytes[index + (int)MM45AwardIndex.RestoredMirabeth];
            HelpedDanulf = bytes[index + (int)MM45AwardIndex.HelpedDanulf];
            SavedElves = bytes[index + (int)MM45AwardIndex.SavedElves];
            CivilizedOne = bytes[index + (int)MM45AwardIndex.CivilizedOne];
            HelpedCaptainNystor = bytes[index + (int)MM45AwardIndex.HelpedCaptainNystor];
            LiberatedPagoda = bytes[index + (int)MM45AwardIndex.LiberatedPagoda];
            FishermansFriend = bytes[index + (int)MM45AwardIndex.FishermansFriend];
            HelpedArie = bytes[index + (int)MM45AwardIndex.HelpedArie];
            FreedLigono = bytes[index + (int)MM45AwardIndex.FreedLigono];
            HelpedGlom = bytes[index + (int)MM45AwardIndex.HelpedGlom];
            HelpedHalon = bytes[index + (int)MM45AwardIndex.HelpedHalon];
            PrincessFavorite = bytes[index + (int)MM45AwardIndex.PrincessFavorite];
            AppeasedBarok = bytes[index + (int)MM45AwardIndex.AppeasedBarok];
            LoremasterOfWorms = bytes[index + (int)MM45AwardIndex.LoremasterOfWorms];
            LoremasterOfLizards = bytes[index + (int)MM45AwardIndex.LoremasterOfLizards];
            LoremasterOfSerpents = bytes[index + (int)MM45AwardIndex.LoremasterOfSerpents];
            LoremasterOfDrakes = bytes[index + (int)MM45AwardIndex.LoremasterOfDrakes];
            LoremasterOfDragons = bytes[index + (int)MM45AwardIndex.LoremasterOfDragons];
            DefeatedLordXeen = bytes[index + (int)MM45AwardIndex.DefeatedLordXeen];
            MasterOfWords = bytes[index + (int)MM45AwardIndex.MasterOfWords];
            MemberDrawkcabBrotherhood = bytes[index + (int)MM45AwardIndex.MemberDrawkcabBrotherhood];
            ChosenOne = bytes[index + (int)MM45AwardIndex.ChosenOne];
            DiscipleOfBark = bytes[index + (int)MM45AwardIndex.DiscipleOfBark];
            CartographersChallenge = bytes[index + (int)MM45AwardIndex.CartographersChallenge];
            MerchantsChallenge = bytes[index + (int)MM45AwardIndex.MerchantsChallenge];
            SuperiorIntellect = bytes[index + (int)MM45AwardIndex.SuperiorIntellect];
            HelpedDreyfus = bytes[index + (int)MM45AwardIndex.HelpedDreyfus];
            ReturnedStatuettes = bytes[index + (int)MM45AwardIndex.ReturnedStatuettes];
            DrawkcabExtraordinaire = bytes[index + (int)MM45AwardIndex.DrawkcabExtraordinaire];
            EnchantedBridle = bytes[index + (int)MM45AwardIndex.EnchantedBridle];
            HelpedKramer = bytes[index + (int)MM45AwardIndex.HelpedKramer];
            HelpedVespar = bytes[index + (int)MM45AwardIndex.HelpedVespar];
            FedNibbler = bytes[index + (int)MM45AwardIndex.FedNibbler];
            RescuedSprite = bytes[index + (int)MM45AwardIndex.RescuedSprite];
            TasteTesterRoyale = bytes[index + (int)MM45AwardIndex.TasteTesterRoyale];
            HelpedEctor = bytes[index + (int)MM45AwardIndex.HelpedEctor];
            RestoredFountainOfYouth = bytes[index + (int)MM45AwardIndex.RestoredFountainOfYouth];
            AwakenedFireSleeper = bytes[index + (int)MM45AwardIndex.AwakenedFireSleeper];
            AwakenedAirSleeper = bytes[index + (int)MM45AwardIndex.AwakenedAirSleeper];
            AwakenedEarthSleeper = bytes[index + (int)MM45AwardIndex.AwakenedEarthSleeper]; 
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[64];
            bytes[(int)MM45AwardIndex.VertigoGuildMember] = VertigoGuildMember;
            bytes[(int)MM45AwardIndex.NightshadowGuildMember] = NightshadowGuildMember;
            bytes[(int)MM45AwardIndex.RivercityGuildMember] = RivercityGuildMember;
            bytes[(int)MM45AwardIndex.AspGuildMember] = AspGuildMember;
            bytes[(int)MM45AwardIndex.WinterkillGuildMember] = WinterkillGuildMember;
            bytes[(int)MM45AwardIndex.ShangriLaGuildMember] = ShangriLaGuildMember;
            bytes[(int)MM45AwardIndex.OutstandingCitizen] = OutstandingCitizen;
            bytes[(int)MM45AwardIndex.RedDwarfBadgeOfCourage] = RedDwarfBadgeOfCourage;
            bytes[(int)MM45AwardIndex.ConvictedThief] = ConvictedThief;
            bytes[(int)MM45AwardIndex.WarzoneWins] = WarzoneWins;
            bytes[(int)MM45AwardIndex.SuperExplorer] = SuperExplorer;
            bytes[(int)MM45AwardIndex.MasterOfGolems] = MasterOfGolems;
            bytes[(int)MM45AwardIndex.TaxmanEmeritus] = TaxmanEmeritus;
            bytes[(int)MM45AwardIndex.RescuedCrodo] = RescuedCrodo;
            bytes[(int)MM45AwardIndex.FoundShangriLa] = FoundShangriLa;
            bytes[(int)MM45AwardIndex.PrinceOfThieves] = PrinceOfThieves;
            bytes[(int)MM45AwardIndex.GhostslayerExtraordinaire] = GhostslayerExtraordinaire;
            bytes[(int)MM45AwardIndex.Legendary] = Legendary;
            bytes[(int)MM45AwardIndex.RescuedCelia] = RescuedCelia;
            bytes[(int)MM45AwardIndex.HelpedOrothin] = HelpedOrothin;
            bytes[(int)MM45AwardIndex.RestoredFalista] = RestoredFalista;
            bytes[(int)MM45AwardIndex.TurnedSeasons] = TurnedSeasons;
            bytes[(int)MM45AwardIndex.HelpedCarlawna] = HelpedCarlawna;
            bytes[(int)MM45AwardIndex.HelpedFalagar] = HelpedFalagar;
            bytes[(int)MM45AwardIndex.RestoredMirabeth] = RestoredMirabeth;
            bytes[(int)MM45AwardIndex.HelpedDanulf] = HelpedDanulf;
            bytes[(int)MM45AwardIndex.SavedElves] = SavedElves;
            bytes[(int)MM45AwardIndex.CivilizedOne] = CivilizedOne;
            bytes[(int)MM45AwardIndex.HelpedCaptainNystor] = HelpedCaptainNystor;
            bytes[(int)MM45AwardIndex.LiberatedPagoda] = LiberatedPagoda;
            bytes[(int)MM45AwardIndex.FishermansFriend] = FishermansFriend;
            bytes[(int)MM45AwardIndex.HelpedArie] = HelpedArie;
            bytes[(int)MM45AwardIndex.FreedLigono] = FreedLigono;
            bytes[(int)MM45AwardIndex.HelpedGlom] = HelpedGlom;
            bytes[(int)MM45AwardIndex.HelpedHalon] = HelpedHalon;
            bytes[(int)MM45AwardIndex.PrincessFavorite] = PrincessFavorite;
            bytes[(int)MM45AwardIndex.AppeasedBarok] = AppeasedBarok;
            bytes[(int)MM45AwardIndex.LoremasterOfWorms] = LoremasterOfWorms;
            bytes[(int)MM45AwardIndex.LoremasterOfLizards] = LoremasterOfLizards;
            bytes[(int)MM45AwardIndex.LoremasterOfSerpents] = LoremasterOfSerpents;
            bytes[(int)MM45AwardIndex.LoremasterOfDrakes] = LoremasterOfDrakes;
            bytes[(int)MM45AwardIndex.LoremasterOfDragons] = LoremasterOfDragons;
            bytes[(int)MM45AwardIndex.DefeatedLordXeen] = DefeatedLordXeen;
            bytes[(int)MM45AwardIndex.MasterOfWords] = MasterOfWords;
            bytes[(int)MM45AwardIndex.MemberDrawkcabBrotherhood] = MemberDrawkcabBrotherhood;
            bytes[(int)MM45AwardIndex.ChosenOne] = ChosenOne;
            bytes[(int)MM45AwardIndex.DiscipleOfBark] = DiscipleOfBark;
            bytes[(int)MM45AwardIndex.CartographersChallenge] = CartographersChallenge;
            bytes[(int)MM45AwardIndex.MerchantsChallenge] = MerchantsChallenge;
            bytes[(int)MM45AwardIndex.SuperiorIntellect] = SuperiorIntellect;
            bytes[(int)MM45AwardIndex.HelpedDreyfus] = HelpedDreyfus;
            bytes[(int)MM45AwardIndex.ReturnedStatuettes] = ReturnedStatuettes;
            bytes[(int)MM45AwardIndex.DrawkcabExtraordinaire] = DrawkcabExtraordinaire;
            bytes[(int)MM45AwardIndex.EnchantedBridle] = EnchantedBridle;
            bytes[(int)MM45AwardIndex.HelpedKramer] = HelpedKramer;
            bytes[(int)MM45AwardIndex.HelpedVespar] = HelpedVespar;
            bytes[(int)MM45AwardIndex.FedNibbler] = FedNibbler;
            bytes[(int)MM45AwardIndex.RescuedSprite] = RescuedSprite;
            bytes[(int)MM45AwardIndex.TasteTesterRoyale] = TasteTesterRoyale;
            bytes[(int)MM45AwardIndex.HelpedEctor] = HelpedEctor;
            bytes[(int)MM45AwardIndex.RestoredFountainOfYouth] = RestoredFountainOfYouth;
            bytes[(int)MM45AwardIndex.AwakenedFireSleeper] = AwakenedFireSleeper;
            bytes[(int)MM45AwardIndex.AwakenedAirSleeper] = AwakenedAirSleeper;
            bytes[(int)MM45AwardIndex.AwakenedEarthSleeper] = AwakenedEarthSleeper; 
            return bytes;
        }
    }

    public class MM45Inventory : Inventory
    {
        private List<Item> m_items;

        public override List<Item> Items
        {
            get { return m_items; }
            set { m_items = value; }
        }

        public List<Item> Weapons;
        public List<Item> Armor;
        public List<Item> Accessories;
        public List<Item> Misc;

        public MM45Inventory()
        {
        }

        public static MM45Inventory Create(byte[] bytes, int index)
        {
            MM45Inventory inventory = new MM45Inventory();
            inventory.SetFromBytes(bytes, index);
            return inventory;
        }

        public void SetFromBytes(byte[] bytes, int index)
        {
            Items = new List<Item>(36);
            Weapons = new List<Item>(9);
            Armor = new List<Item>(9);
            Accessories = new List<Item>(9);
            Misc = new List<Item>(9);

            int iDisplay = 1;
            for (int i = 0; i < 36; i += 4)
            {
                MM45Item item = MM45Item.Create(bytes, ItemType.Weapon, index + i + MM45Memory.CharItemsWeapons);
                item.MemoryIndex = i / 4;
                item.DisplayIndex = String.Format("{0}.", iDisplay++);
                if (item != null && item.Base.Index != 0)
                    Weapons.Add(item);
            }
            iDisplay = 1;
            for (int i = 0; i < 36; i += 4)
            {
                MM45Item item = MM45Item.Create(bytes, ItemType.Armor, index + i + MM45Memory.CharItemsArmor);
                item.MemoryIndex = i / 4 + 9;
                item.DisplayIndex = String.Format("{0}.", iDisplay++);
                if (item != null && item.Base.Index != 0)
                    Armor.Add(item);
            }
            iDisplay = 1;
            for (int i = 0; i < 36; i += 4)
            {
                MM45Item item = MM45Item.Create(bytes, ItemType.Accessory, index + i + MM45Memory.CharItemsAccessories);
                item.MemoryIndex = i / 4 + 18;
                item.DisplayIndex = String.Format("{0}.", iDisplay++);
                if (item != null && item.Base.Index != 0)
                    Accessories.Add(item);
            }
            iDisplay = 1;
            for (int i = 0; i < 36; i += 4)
            {
                MM45Item item = MM45Item.Create(bytes, ItemType.Miscellaneous, index + i + MM45Memory.CharItemsMisc);
                item.MemoryIndex = i / 4 + 27;
                item.DisplayIndex = String.Format("{0}.", iDisplay++);
                if (item != null && item.Base.Index != 0)
                    Misc.Add(item);
            }

            Items.AddRange(Weapons);
            Items.AddRange(Armor);
            Items.AddRange(Accessories);
            Items.AddRange(Misc);
        }

        public override int NumBackpackItems
        {
            get
            {
                int iCount = 0;
                foreach (MM45Item item in Items)
                    if (item.WhereEquipped == EquipLocation.None)
                        iCount++;
                return iCount;
            }
        }

        public void SetBytes(byte[] bytes, int index)
        {
            // Fill the given array of bytes with data representing this inventory
        }

        public bool HasItem(MM45ItemBase itemWanted)
        {
            foreach (MM45Item item in Items)
                if (item.EqualsBase(itemWanted))
                    return true;
            return false;
        }

        public override bool BrokenEquipped
        {
            get
            {
                foreach (MM45Item item in Items)
                    if (item.Broken)    // MM4/5 unequips broken items automatically, so we can't really return the proper value
                        return true;
                return false;
            }
        }

        public override bool CursedEquipped
        {
            get
            {
                foreach (MM45Item item in Items)
                    if (item.WhereEquipped != EquipLocation.None && item.Cursed)
                        return true;
                return false;
            }
        }

        private void AddModifier(ref Modifiers mod, MM45Item item)
        {
            AttributeModifier attribute = MM45Item.AttributeEffect(item.Prefix);
            HitDamageAC hdac = MM45Item.PrefixHDAC(item.Prefix);
            ElementDamageResistance elemental = MM45Item.ElementalEffect(item.Prefix);
            mod.Adjust(attribute, item.DescriptionString);
            if (item.IsWeapon || item.IsArmor)
                mod.Adjust(hdac, item.Type, item.DescriptionString);
            mod.Adjust(elemental, item.Type, item.DescriptionString, !item.IsWeapon);
            mod.Adjust(ModAttr.ArmorClass, MM45Item.GetArmorClass(item.Base), item.DescriptionString);
        }

        public override Modifiers GetModifiers()
        {
            Modifiers mod = new Modifiers();

            foreach (MM45Item item in Items)
            {
                if (item.WhereEquipped != EquipLocation.None)
                    AddModifier(ref mod, item);
            }

            return mod;
        }

        public override BasicDamage MeleeWeaponDamage
        {
            get
            {
                foreach(MM45Item item in Items)
                {
                    if (item.WhereEquipped != EquipLocation.None)
                    {
                        switch (item.Type)
                        {
                            case ItemType.OneHandMelee:
                            case ItemType.TwoHandMelee:
                            case ItemType.Weapon:
                                return new BasicDamage(1, MM45Item.GetItemDamage(item.Base));
                            default:
                                break;
                        }
                    }
                }
                return BasicDamage.Zero;
            }
        }

        public override BasicDamage RangedWeaponDamage
        {
            get
            {
                foreach (MM45Item item in Items)
                {
                    if (item.WhereEquipped != EquipLocation.None)
                    {
                        switch (item.Type)
                        {
                            case ItemType.Missile:
                                return new BasicDamage(1, MM45Item.GetItemDamage(item.Base));
                            default:
                                break;
                        }
                    }
                }
                return BasicDamage.Zero;
            }
        }
    }

    public class MM45KnownSpells : MM345KnownSpells
    {
        public MM45KnownSpells(GenericClass charClass)
        {
            RawBytes = new byte[39];
            for (int i = 0; i < 39; i++)
                RawBytes[i] = 0;
            m_iNumKnown = 0;
            Type = Global.GetSpellType(charClass);
        }

        public MM45KnownSpells(byte[] bytes, int iIndex, GenericClass charClass)
        {
            RawBytes = new byte[39];
            Buffer.BlockCopy(bytes, iIndex, RawBytes, 0, 39);
            m_iNumKnown = -1;
            Type = Global.GetSpellType(charClass);
        }

        public override KnownSpells CreateNew(GenericClass charClass, KnownSpells original = null) { return new MM45KnownSpells(charClass); }
        public override void SetKnown(Spell spell, bool bKnown) { SetKnown(spell as MM45Spell, bKnown); }

        public override string KnownString(GenericClass charClass)
        {
            int iMax = 39;
            switch (Global.GetSpellType(charClass))
            {
                case SpellType.Cleric:
                case SpellType.Sorcerer:
                case SpellType.Druid:
                    break;
                default:
                    iMax = 0;
                    break;
            }
            return String.Format("{0}/{1}", NumKnown, iMax);
        }

        public override int NumKnown
        {
            get
            {
                if (m_iNumKnown != -1)
                    return m_iNumKnown;

                int iCount = 0;
                for (int i = 0; i < RawBytes.Length; i++)
                    if (RawBytes[i] != 0)
                        iCount++;

                m_iNumKnown = iCount;
                return m_iNumKnown;
            }
        }

        public void SetKnown(MM45Spell spell, bool bKnown)
        {
            if (spell == null || spell.InternalIndex == MM45InternalSpellIndex.None)
                return;

            int iRawIndex = RawByteIndex(spell);
            if (iRawIndex < 0 || iRawIndex >= RawBytes.Length)
                return;

            RawBytes[iRawIndex] = (byte) (bKnown ? 1 : 0);

            m_iNumKnown = -1;
        }

        public void SetKnown(MM45InternalSpellIndex index, SpellType type, bool bKnown)
        {
            if (index == MM45InternalSpellIndex.None)
                return;

            int iRawIndex = RawByteIndex(index, type);
            if (iRawIndex < 0 || iRawIndex >= RawBytes.Length)
                return;

            RawBytes[iRawIndex] = (byte)(bKnown ? 1 : 0);

            m_iNumKnown = -1;
        }

        public int RawByteIndex(MM45InternalSpellIndex index, SpellType type)
        {
            switch (type)
            {
                case SpellType.Cleric:
                    switch (index)
                    {
                        case MM45InternalSpellIndex.AcidSpray: return 0;
                        case MM45InternalSpellIndex.Awaken: return 1;
                        case MM45InternalSpellIndex.BeastMaster: return 2;
                        case MM45InternalSpellIndex.Bless: return 3;
                        case MM45InternalSpellIndex.ColdRay: return 4;
                        case MM45InternalSpellIndex.CreateFood: return 5;
                        case MM45InternalSpellIndex.CureDisease: return 6;
                        case MM45InternalSpellIndex.CureParalysis: return 7;
                        case MM45InternalSpellIndex.CurePoison: return 8;
                        case MM45InternalSpellIndex.CureWounds: return 9;
                        case MM45InternalSpellIndex.DayOfProtection: return 10;
                        case MM45InternalSpellIndex.DeadlySwarm: return 11;
                        case MM45InternalSpellIndex.DivineIntervention: return 12;
                        case MM45InternalSpellIndex.FieryFlail: return 13;
                        case MM45InternalSpellIndex.FirstAid: return 14;
                        case MM45InternalSpellIndex.FlyingFist: return 15;
                        case MM45InternalSpellIndex.FrostBite: return 16;
                        case MM45InternalSpellIndex.Heroism: return 17;
                        case MM45InternalSpellIndex.HolyBonus: return 18;
                        case MM45InternalSpellIndex.HolyWord: return 19;
                        case MM45InternalSpellIndex.Hypnotize: return 20;
                        case MM45InternalSpellIndex.Light: return 21;
                        case MM45InternalSpellIndex.MassDistortion: return 22;
                        case MM45InternalSpellIndex.MoonRay: return 23;
                        case MM45InternalSpellIndex.NaturesCure: return 24;
                        case MM45InternalSpellIndex.Pain: return 25;
                        case MM45InternalSpellIndex.PowerCure: return 26;
                        case MM45InternalSpellIndex.ProtFromElements: return 27;
                        case MM45InternalSpellIndex.RaiseDead: return 28;
                        case MM45InternalSpellIndex.Resurrect: return 29;
                        case MM45InternalSpellIndex.Revitalize: return 30;
                        case MM45InternalSpellIndex.Sparks: return 31;
                        case MM45InternalSpellIndex.StoneToFlesh: return 32;
                        case MM45InternalSpellIndex.SunRay: return 33;
                        case MM45InternalSpellIndex.SuppressDisease: return 34;
                        case MM45InternalSpellIndex.SuppressPoison: return 35;
                        case MM45InternalSpellIndex.TownPortal: return 36;
                        case MM45InternalSpellIndex.TurnUndead: return 37;
                        case MM45InternalSpellIndex.WalkOnWater: return 38;
                        default: return -1;
                    }
                case SpellType.Sorcerer:
                    switch (index)
                    {
                        case MM45InternalSpellIndex.Awaken: return 0;
                        case MM45InternalSpellIndex.Clairvoyance: return 1;
                        case MM45InternalSpellIndex.DancingSword: return 2;
                        case MM45InternalSpellIndex.DayOfSorcery: return 3;
                        case MM45InternalSpellIndex.DetectMonster: return 4;
                        case MM45InternalSpellIndex.DragonSleep: return 5;
                        case MM45InternalSpellIndex.ElementalStorm: return 6;
                        case MM45InternalSpellIndex.EnchantItem: return 7;
                        case MM45InternalSpellIndex.EnergyBlast: return 8;
                        case MM45InternalSpellIndex.Etherealize: return 9;
                        case MM45InternalSpellIndex.FantasticFreeze: return 10;
                        case MM45InternalSpellIndex.FingerOfDeath: return 11;
                        case MM45InternalSpellIndex.FireBall: return 12;
                        case MM45InternalSpellIndex.GolemStopper: return 13;
                        case MM45InternalSpellIndex.IdentifyMonster: return 14;
                        case MM45InternalSpellIndex.Implosion: return 15;
                        case MM45InternalSpellIndex.Incinerate: return 16;
                        case MM45InternalSpellIndex.Inferno: return 17;
                        case MM45InternalSpellIndex.InsectSpray: return 18;
                        case MM45InternalSpellIndex.ItemToGold: return 19;
                        case MM45InternalSpellIndex.Jump: return 20;
                        case MM45InternalSpellIndex.Levitate: return 21;
                        case MM45InternalSpellIndex.Light: return 22;
                        case MM45InternalSpellIndex.LightningBolt: return 23;
                        case MM45InternalSpellIndex.LloydsBeacon: return 24;
                        case MM45InternalSpellIndex.MagicArrow: return 25;
                        case MM45InternalSpellIndex.MegaVolts: return 26;
                        case MM45InternalSpellIndex.PoisonVolley: return 27;
                        case MM45InternalSpellIndex.PowerShield: return 28;
                        case MM45InternalSpellIndex.PrismaticLight: return 29;
                        case MM45InternalSpellIndex.RechargeItem: return 30;
                        case MM45InternalSpellIndex.Shrapmetal: return 31;
                        case MM45InternalSpellIndex.Sleep: return 32;
                        case MM45InternalSpellIndex.StarBurst: return 33;
                        case MM45InternalSpellIndex.SuperShelter: return 34;
                        case MM45InternalSpellIndex.Teleport: return 35;
                        case MM45InternalSpellIndex.TimeDistortion: return 36;
                        case MM45InternalSpellIndex.ToxicCloud: return 37;
                        case MM45InternalSpellIndex.WizardEye: return 38;
                        default: return -1;
                    }
                case SpellType.Druid:
                    switch (index)
                    {
                        case MM45InternalSpellIndex.AcidSpray: return 0;
                        case MM45InternalSpellIndex.Awaken: return 1;
                        case MM45InternalSpellIndex.BeastMaster: return 2;
                        case MM45InternalSpellIndex.Bless: return 3;
                        case MM45InternalSpellIndex.Clairvoyance: return 4;
                        case MM45InternalSpellIndex.ColdRay: return 5;
                        case MM45InternalSpellIndex.CureDisease: return 6;
                        case MM45InternalSpellIndex.CurePoison: return 7;
                        case MM45InternalSpellIndex.CureWounds: return 8;
                        case MM45InternalSpellIndex.EnergyBlast: return 9;
                        case MM45InternalSpellIndex.FireBall: return 10;
                        case MM45InternalSpellIndex.FirstAid: return 11;
                        case MM45InternalSpellIndex.FlyingFist: return 12;
                        case MM45InternalSpellIndex.FrostBite: return 13;
                        case MM45InternalSpellIndex.Heroism: return 14;
                        case MM45InternalSpellIndex.HolyBonus: return 15;
                        case MM45InternalSpellIndex.IdentifyMonster: return 16;
                        case MM45InternalSpellIndex.InsectSpray: return 17;
                        case MM45InternalSpellIndex.Jump: return 18;
                        case MM45InternalSpellIndex.Levitate: return 19;
                        case MM45InternalSpellIndex.Light: return 20;
                        case MM45InternalSpellIndex.LightningBolt: return 21;
                        case MM45InternalSpellIndex.LloydsBeacon: return 22;
                        case MM45InternalSpellIndex.MagicArrow: return 23;
                        case MM45InternalSpellIndex.NaturesCure: return 24;
                        case MM45InternalSpellIndex.Pain: return 25;
                        case MM45InternalSpellIndex.PowerCure: return 26;
                        case MM45InternalSpellIndex.PowerShield: return 27;
                        case MM45InternalSpellIndex.ProtFromElements: return 28;
                        case MM45InternalSpellIndex.Revitalize: return 29;
                        case MM45InternalSpellIndex.Shrapmetal: return 30;
                        case MM45InternalSpellIndex.Sleep: return 31;
                        case MM45InternalSpellIndex.Sparks: return 32;
                        case MM45InternalSpellIndex.SuppressDisease: return 33;
                        case MM45InternalSpellIndex.SuppressPoison: return 34;
                        case MM45InternalSpellIndex.ToxicCloud: return 35;
                        case MM45InternalSpellIndex.TurnUndead: return 36;
                        case MM45InternalSpellIndex.WalkOnWater: return 37;
                        case MM45InternalSpellIndex.WizardEye: return 38;
                        default: return -1;
                    }
                default:
                    return -1;
            }
        }

        public static int RawByteIndex(MM45Spell spell)
        {
            return RawByteIndex(spell.Index);
        }

        public static int RawByteIndex(MM45SpellIndex index)
        {
            switch (index)
            {
                case MM45SpellIndex.AcidSpray:
                case MM45SpellIndex.AwakenArcane:
                case MM45SpellIndex.AcidSprayDruid:
                    return 0;
                case MM45SpellIndex.Awaken:
                case MM45SpellIndex.Clairvoyance:
                case MM45SpellIndex.AwakenDruid:
                    return 1;
                case MM45SpellIndex.BeastMaster:
                case MM45SpellIndex.DancingSword:
                case MM45SpellIndex.BeastMasterDruid:
                    return 2;
                case MM45SpellIndex.Bless:
                case MM45SpellIndex.DayOfSorcery:
                case MM45SpellIndex.BlessDruid:
                    return 3;
                case MM45SpellIndex.ColdRay:
                case MM45SpellIndex.DetectMonster:
                case MM45SpellIndex.ClairvoyanceDruid:
                    return 4;
                case MM45SpellIndex.CreateFood:
                case MM45SpellIndex.DragonSleep:
                case MM45SpellIndex.ColdRayDruid:
                    return 5;
                case MM45SpellIndex.CureDisease:
                case MM45SpellIndex.ElementalStorm:
                case MM45SpellIndex.CureDiseaseDruid:
                    return 6;
                case MM45SpellIndex.CureParalysis:
                case MM45SpellIndex.EnchantItem:
                case MM45SpellIndex.CurePoisonDruid:
                    return 7;
                case MM45SpellIndex.CurePoison:
                case MM45SpellIndex.EnergyBlast:
                case MM45SpellIndex.CureWoundsDruid:
                    return 8;
                case MM45SpellIndex.CureWounds:
                case MM45SpellIndex.Etherealize:
                case MM45SpellIndex.EnergyBlastDruid:
                    return 9;
                case MM45SpellIndex.DayOfProtection:
                case MM45SpellIndex.FantasticFreeze:
                case MM45SpellIndex.FireballDruid:
                    return 10;
                case MM45SpellIndex.DeadlySwarm:
                case MM45SpellIndex.FingerOfDeath:
                case MM45SpellIndex.FirstAidDruid:
                    return 11;
                case MM45SpellIndex.DivineIntervention:
                case MM45SpellIndex.Fireball:
                case MM45SpellIndex.FlyingFistDruid:
                    return 12;
                case MM45SpellIndex.FieryFlail:
                case MM45SpellIndex.GolemStopper:
                case MM45SpellIndex.FrostBiteDruid:
                    return 13;
                case MM45SpellIndex.FirstAid:
                case MM45SpellIndex.IdentifyMonster:
                case MM45SpellIndex.HeroismDruid:
                    return 14;
                case MM45SpellIndex.FlyingFist:
                case MM45SpellIndex.Implosion:
                case MM45SpellIndex.HolyBonusDruid:
                    return 15;
                case MM45SpellIndex.FrostBite:
                case MM45SpellIndex.Incinerate:
                case MM45SpellIndex.IdentifyMonsterDruid:
                    return 16;
                case MM45SpellIndex.Heroism:
                case MM45SpellIndex.Inferno:
                case MM45SpellIndex.InsectSprayDruid:
                    return 17;
                case MM45SpellIndex.HolyBonus:
                case MM45SpellIndex.InsectSpray:
                case MM45SpellIndex.JumpDruid:
                    return 18;
                case MM45SpellIndex.HolyWord:
                case MM45SpellIndex.ItemToGold:
                case MM45SpellIndex.LevitateDruid:
                    return 19;
                case MM45SpellIndex.Hypnotize:
                case MM45SpellIndex.Jump:
                case MM45SpellIndex.LightDruid:
                    return 20;
                case MM45SpellIndex.Light:
                case MM45SpellIndex.Levitate:
                case MM45SpellIndex.LightningBoltDruid:
                    return 21;
                case MM45SpellIndex.MassDistortion:
                case MM45SpellIndex.LightArcane:
                case MM45SpellIndex.LloydsBeaconDruid:
                    return 22;
                case MM45SpellIndex.MoonRay:
                case MM45SpellIndex.LightningBolt:
                case MM45SpellIndex.MagicArrowDruid:
                    return 23;
                case MM45SpellIndex.NaturesCure:
                case MM45SpellIndex.LloydsBeacon:
                case MM45SpellIndex.NaturesCureDruid:
                    return 24;
                case MM45SpellIndex.Pain:
                case MM45SpellIndex.MagicArrow:
                case MM45SpellIndex.PainDruid:
                    return 25;
                case MM45SpellIndex.PowerCure:
                case MM45SpellIndex.MegaVolts:
                case MM45SpellIndex.PowerCureDruid:
                    return 26;
                case MM45SpellIndex.ProtFromElements:
                case MM45SpellIndex.PoisonVolley:
                case MM45SpellIndex.PowerShieldDruid:
                    return 27;
                case MM45SpellIndex.RaiseDead:
                case MM45SpellIndex.PowerShield:
                case MM45SpellIndex.ProtFromElementsDruid:
                    return 28;
                case MM45SpellIndex.Resurrect:
                case MM45SpellIndex.PrismaticLight:
                case MM45SpellIndex.RevitalizeDruid:
                    return 29;
                case MM45SpellIndex.Revitalize:
                case MM45SpellIndex.RechargeItem:
                case MM45SpellIndex.ShrapmetalDruid:
                    return 30;
                case MM45SpellIndex.Sparks:
                case MM45SpellIndex.Shrapmetal:
                case MM45SpellIndex.SleepDruid:
                    return 31;
                case MM45SpellIndex.StoneToFlesh:
                case MM45SpellIndex.Sleep:
                case MM45SpellIndex.SparksDruid:
                    return 32;
                case MM45SpellIndex.SunRay:
                case MM45SpellIndex.StarBurst:
                case MM45SpellIndex.SuppressDiseaseDruid:
                    return 33;
                case MM45SpellIndex.SuppressDisease:
                case MM45SpellIndex.SuperShelter:
                case MM45SpellIndex.SuppressPoisonDruid:
                    return 34;
                case MM45SpellIndex.SuppressPoison:
                case MM45SpellIndex.Teleport:
                case MM45SpellIndex.ToxicCloudDruid:
                    return 35;
                case MM45SpellIndex.TownPortal:
                case MM45SpellIndex.TimeDistortion:
                case MM45SpellIndex.TurnUndeadDruid:
                    return 36;
                case MM45SpellIndex.TurnUndead:
                case MM45SpellIndex.ToxicCloud:
                case MM45SpellIndex.WalkOnWaterDruid:
                    return 37;
                case MM45SpellIndex.WalkOnWater:
                case MM45SpellIndex.WizardEye:
                case MM45SpellIndex.WizardEyeDruid:
                    return 38;
                default: return -1;
            }
        }

        public bool IsKnown(MM45InternalSpellIndex spell, GenericClass mmClass)
        {
            int iIndex = RawByteIndex(spell, Global.GetSpellType(mmClass));
            if (iIndex < 0 || iIndex >= RawBytes.Length)
                return false;

            return (RawBytes[iIndex] != 0);
        }

        public override bool IsKnown(int index, SpellType type)
        {
            return IsKnown((MM45SpellIndex)index, type);
        }

        public override bool IsKnown(int internalIndex, GenericClass mmClass)
        {
            return IsKnown((MM45InternalSpellIndex)internalIndex, mmClass);
        }

        public bool IsKnown(MM45SpellIndex index, SpellType type)
        {
            if (type != Type)
                return false;

            int iIndex = RawByteIndex(index);
            if (iIndex < 0 || iIndex >= RawBytes.Length)
                return false;

            return (RawBytes[iIndex] != 0);
        }

        public bool IsKnown(MM45Spell spell)
        {
            return IsKnown(spell.Index, spell.Type);
        }
    }
}
