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
    public partial class MM3CharacterInfoControl : MMCharacterInfoControl
    {
        private List<Item> m_lastBackpack = null;

        public MM3CharacterInfoControl(IMain main)
            : base(main)
        {
            InitializeComponent();
            m_char = new MM3Character();

            FindEditableAttributes();
        }

        public override int CharacterSize { get { return MM3Character.SizeInBytes; } }

        public override void SetInfo(PartyInfo info, int iIndex, GameInfo gameInfo, EncounterInfo encounterInfo = null)
        {
            int iBytesPerChar = CharacterSize;
            if (info != null && info.Bytes.Length < (info.Addresses[iIndex] + 1) * iBytesPerChar)
                return;

            if (info is MM3PartyInfo)
            {
                m_bytes = new byte[iBytesPerChar];
                Buffer.BlockCopy(info.Bytes, info.Addresses[iIndex] * iBytesPerChar, m_bytes, 0, iBytesPerChar);
                m_char.SetFromBytes(0, m_bytes, gameInfo, encounterInfo);
                m_iCharacterIndex = iIndex;
                m_iCharacterAddress = info.Addresses[iIndex];
                m_iCharacterPosition = info.Positions[iIndex];
                ((MM3Character) m_char).Address = m_iCharacterAddress;
            }
            else
            {
                m_iCharacterAddress = -1;
                m_iCharacterIndex = -1;
                m_iCharacterPosition = -1;
            }

            UpdateUI();
        }

        public override bool UpdateSubscreen(Subscreen screen, bool bForce = false)
        {
            switch (screen)
            {
                case Subscreen.Inventory1:
                case Subscreen.Inventory2:
                case Subscreen.Weapons:     // During combat these show up
                    break;
                default:
                    screen = Subscreen.Unknown;     // Highlighting weapon/armor/misc in MM3 doesn't make much visual sense
                    break;
            }

            if (screen == m_oldScreen && !bForce)
                return false;

            m_oldScreen = screen;

            return HighlightInventory(screen);
        }

        public override void UpdateUI()
        {
            MM3Character mm3Char = m_char as MM3Character;

            if (m_bDebugMonitorBackpack)
            {
                if (m_lastBackpack == null)
                    m_lastBackpack = new List<Item>();
                for (int i = 0; i < Math.Max(m_lastBackpack.Count, mm3Char.Inventory.Items.Count); i++)
                {
                    if (i >= m_lastBackpack.Count)
                        Global.Log("Char{0} +Item: {1}", mm3Char.Address, mm3Char.Inventory.Items[i].DebugString);
                    else if (i >= mm3Char.Inventory.Items.Count)
                        Global.Log("Char{0} -Item: {1}", mm3Char.Address, m_lastBackpack[i].DebugString);
                    else if (m_lastBackpack[i].DebugString != mm3Char.Inventory.Items[i].DebugString)
                        Global.Log("Char{0} xItem: {1} <=> {2}", mm3Char.Address, m_lastBackpack[i].DebugString, mm3Char.Inventory.Items[i].DebugString);
                }
                //if (mm3Char.Inventory.Items.Count >= 9)
                //    Hacker.SetBackpack(mm3Char.BasicAddress, new List<Item>());
                m_lastBackpack = mm3Char.Inventory.Items;
            }

            bool bHireling = false;

            if (String.IsNullOrWhiteSpace(mm3Char.CharName))
                return;

            m_commonCtrls.labelLevel.Text = mm3Char.BasicInfoString + (bHireling ? " (hireling)" : "");
            m_commonCtrls.labelAC.Text = mm3Char.GetACString();
            MMCommonControls.labelAccuracy.Text = mm3Char.Accuracy.AdjustedString(mm3Char.Modifiers.Accuracy);
            ListViewSelectionSaver savePack = new ListViewSelectionSaver(m_commonCtrls.lvBackpack);
            ListViewSelectionSaver saveEquip = new ListViewSelectionSaver(m_commonCtrls.lvEquipped);

            m_commonCtrls.lvBackpack.BeginUpdate();
            m_commonCtrls.lvEquipped.BeginUpdate();
            ClearInventoryLists();
            for (int i = 0; i < 18; i++)
            {
                if (mm3Char.Inventory.Items.Count > i)
                {
                    if (mm3Char.Inventory.Items[i].WhereEquipped == EquipLocation.None)
                        SetBackpackLVI(mm3Char.Inventory.Items[i], mm3Char);
                    else
                        SetEquippedLVI(mm3Char.Inventory.Items[i], mm3Char);
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

            SetResistances(mm3Char.GetResistances());
            m_commonCtrls.labelCondition.Text = MM345BaseCharacter.ConditionString(mm3Char.Inventory, mm3Char.Condition, true);
            string strTip = mm3Char.Condition.Description;
            if (mm3Char.Inventory.BrokenEquipped)
                strTip = strTip.TrimEnd() + "\r\nBroken Item: One or more equipped items need repair";
            if (mm3Char.Inventory.CursedEquipped)
                strTip = strTip.TrimEnd() + "\r\nCursed Item: One or more equipped items need to be uncursed";
            m_tipCondition.SetToolTip(m_commonCtrls.labelCondition, strTip);
            m_tipCondition.ShowAlways = true;
            m_tipCondition.AutoPopDelay = 32000;

            MMCommonControls.labelEndurance.Text = mm3Char.Endurance.AdjustedString(mm3Char.Modifiers.Endurance);
            m_commonCtrls.labelExp.Text = mm3Char.ExperienceString;
            if (bHireling)
            {
                labelCost.Visible = true;
                labelCost.Text = String.Format("{0}/day", mm3Char.HirelingCost);
                labelCostHeader.Visible = true;
            }
            else
            {
                labelCost.Visible = false;
                labelCostHeader.Visible = false;
            }
            m_commonCtrls.labelHP.Text = String.Format("{0}/{1}", mm3Char.CurrentHP, Modifiers.ModString(mm3Char.MaxHPWithoutItems, mm3Char.Modifiers.HitPoints));
            MMCommonControls.labelIntellect.Text = mm3Char.Intellect.AdjustedString(mm3Char.Modifiers.Intellect);
            MMCommonControls.labelLuck.Text = mm3Char.Luck.AdjustedString(mm3Char.Modifiers.Luck);
            MMCommonControls.labelMight.Text = mm3Char.Might.AdjustedString(mm3Char.Modifiers.Might);
            MMCommonControls.labelPersonality.Text = mm3Char.Personality.AdjustedString(mm3Char.Modifiers.Personality);
            m_commonCtrls.labelSP.Text = String.Format("{0}/{1}", mm3Char.CurrentSP, Modifiers.ModString(mm3Char.MaxSPWithoutItems, mm3Char.Modifiers.SpellPoints));
            MMCommonControls.labelSpeed.Text = mm3Char.Speed.AdjustedString(mm3Char.Modifiers.Speed);
            m_commonCtrls.labelMelee.Text = mm3Char.MeleeDamageString;
            MMCommonControls.labelRanged.Text = mm3Char.RangedDamageString;
            MMCommonControls.labelThievery.Text = Modifiers.ModString(mm3Char.Thievery, mm3Char.Modifiers.Thievery);
            labelKnownSpells.Text = mm3Char.Spells.KnownString(mm3Char.BasicClass);
            m_tipSkills.SetToolTip(labelSecondarySkills, mm3Char.Skills.MultiLineDescription);
            m_tipSkills.ShowAlways = true;
            m_tipSkills.AutoPopDelay = 32000;
            labelSecondarySkills.Text = mm3Char.Skills.ToString();
            labelReadySpell.Text = mm3Char.ReadySpellString;

            labelBeacon.Text = mm3Char.Beacon.ToString(Hacker as MM3MemoryHacker);

            m_commonCtrls.llCureAll.Visible = mm3Char.IsHealer && Properties.Settings.Default.EnableMemoryWrite;
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

            if (label == labelReadySpell)
            {
                menuFlags = CheatMenuFlags.Edit;
                m_cheatType = AttributeType.ReadySpell;
                m_cheatOffsets = new int[] { m_char.Offsets.ReadySpell };
            }
            else if (label == labelSecondarySkills)
            {
                menuFlags = CheatMenuFlags.Edit;
                m_cheatType = AttributeType.SecondarySkills;
                m_cheatOffsets = Global.IntRange(m_char.Offsets.Skills, m_char.Offsets.SkillsLength);
            }
            else if (label == labelKnownSpells)
            {
                m_cheatType = AttributeType.KnownSpells;
                menuFlags = CheatMenuFlags.Edit;
                m_cheatOffsets = Global.IntRange(m_char.Offsets.Spells, m_char.Offsets.SpellsLength);
            }
            else if (label == m_commonCtrls.labelCondition)
            {
                m_cheatType = AttributeType.Condition;
                menuFlags = CheatMenuFlags.Edit;
                m_cheatOffsets = Global.IntRange(m_char.Offsets.Condition, m_char.Offsets.ConditionLength);
            }
            else if (label == labelBeacon)
            {
                m_cheatType = AttributeType.MapAndPosition;
                menuFlags = CheatMenuFlags.Edit | CheatMenuFlags.Add1 | CheatMenuFlags.Subtract1;
                m_cheatOffsets = Global.IntRange(m_char.Offsets.Beacon, 3);
            }
            else if (m_cheatOffsets == null)
            {
                m_cheatType = AttributeType.Item;
                menuFlags = CheatMenuFlags.Edit;
                int i = -1;

                if (label == m_commonCtrls.lvBackpack)
                {
                    if (m_commonCtrls.lvBackpack.SelectedItems.Count > 0 && flags != CheatMenuFlags.AddNew)
                    {
                        InventoryItemTag tag = m_commonCtrls.lvBackpack.SelectedItems[0].Tag as InventoryItemTag;
                        i = tag.MemoryIndex;
                    }
                    else
                        i = m_char.FirstEmptyBackpackIndex;

                    m_cheatOffsets = new int[] { 
                        i + m_char.Offsets.InvEquipLoc,
                        i + m_char.Offsets.InvCharges,
                        i + m_char.Offsets.InvElements,
                        i + m_char.Offsets.InvMaterials,
                        i + m_char.Offsets.InvAttributes,
                        i + m_char.Offsets.InvBases,
                        i + m_char.Offsets.InvProperties
                    };

                    if (i == -1)
                        m_cheatOffsets = null;
                }
                else if (label == m_commonCtrls.lvEquipped)
                {
                    if (m_commonCtrls.lvEquipped.SelectedItems.Count > 0)
                    {
                        InventoryItemTag tag = m_commonCtrls.lvEquipped.SelectedItems[0].Tag as InventoryItemTag;
                        i = tag.MemoryIndex;

                        m_cheatOffsets = new int[] { 
                            i + m_char.Offsets.InvEquipLoc,
                            i + m_char.Offsets.InvCharges,
                            i + m_char.Offsets.InvElements,
                            i + m_char.Offsets.InvMaterials,
                            i + m_char.Offsets.InvAttributes,
                            i + m_char.Offsets.InvBases,
                            i + m_char.Offsets.InvProperties
                        };
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
                MM3Character mm3Char = m_char as MM3Character;
                MMSpellSelectForm formSpellSelect = new MMSpellSelectForm(mm3Char);
                formSpellSelect.SpellIndex = new MMInternalSpellIndex((MM3InternalSpellIndex)mm3Char.ReadySpell);
                if (formSpellSelect.ShowDialog() == DialogResult.OK)
                {
                    ((MM3MemoryHacker)Hacker).SetReadySpell(mm3Char.BasicAddress, formSpellSelect.SpellIndex.CorrectedIndex);
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
                MM3Character mm3Char = m_char as MM3Character;
                MMSkillEditForm formSkills = new MMSkillEditForm(Hacker.Game);
                formSkills.ReadOnly = true;
                formSkills.Skills = mm3Char.Skills;
                formSkills.ShowDialog();
            }
        }

        private void llAwards_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            byte[] bytes = MM3Char.Awards.GetBytes();
            EditBytesForm form = new EditBytesForm(TopLevelControl);
            form.ReadOnly = !Global.Cheats;
            form.ForceLength = true;
            form.SetDescriptionFunction(MM3Bits.AwardDescription);
            form.Bytes = bytes;
            if (form.ShowDialog() == DialogResult.OK)
            {
                MM3Awards awards = MM3Awards.Create(form.Bytes);
                (Hacker as MM3MemoryHacker).SetAwards(MM3Char.Address, awards.GetBytes());
            }
        }

        public MM3Character MM3Char { get { return m_char as MM3Character; } }
    }

    public class MM3CharacterOffsets : CharacterOffsets
    {
        public override int Name             { get { return 0; } }
        public override int NameTerminator   { get { return 10; } }
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
        public override int AwardsLength     { get { return 26; } }
        public override int Spells           { get { return 83; } }
        public override int SpellsLength     { get { return 36; } }
        public override int Beacon           { get { return 119; } }
        public override int SpellCaster      { get { return 122; } }
        public override int ReadySpell       { get { return 123; } }
        public override int Inventory        { get { return 125; } }
        public override int InventoryLength  { get { return 133; } }
        public override int FireResist       { get { return 263; } }
        public override int ColdResist       { get { return 265; } }
        public override int ElecResist       { get { return 267; } }
        public override int PoisonResist     { get { return 269; } }
        public override int EnergyResist     { get { return 271; } }
        public override int MagicResist      { get { return 273; } }
        public override int Condition        { get { return 275; } }
        public override int ConditionLength  { get { return 16; } }
        public override int Town             { get { return 291; } }
        public override int CurrentHP        { get { return 293; } }
        public override int CurrentSP        { get { return 295; } }
        public override int BirthYear        { get { return 297; } }
        public override int Experience       { get { return 299; } }
        public override int Blessed          { get { return 258; } }
        public override int PowerShield      { get { return 259; } }
        public override int HolyBonus        { get { return 260; } }
        public override int Heroism          { get { return 261; } }
        public override int Donations        { get { return 262; } }
        public override int InventoryBases   { get { return 220; } }
        public override int Protection       { get { return 258; } }
        public override int InvEquipLoc      { get { return 125; } }
        public override int InvCharges       { get { return 144; } }
        public override int InvElements      { get { return 163; } }
        public override int InvMaterials     { get { return 182; } }
        public override int InvAttributes    { get { return 201; } }
        public override int InvBases         { get { return 220; } }
        public override int InvProperties    { get { return 239; } }
    }

    public class MM3Character : MM345BaseCharacter
    {
        public MM3Race Race;
        public MM3Awards Awards;
        public MM3DonationFlags Donations;
        public byte[] Unknowns1;
        public byte Unknown2;
        public byte Unknown3;
        public byte Unknown4;

        public const int SizeInBytes = 303;

        public MM3Character()
        {
            Address = -1;
        }

        public static MM3Character Create(byte[] bytes, int iIndex, GameInfo info, bool bRosterFile = false)
        {
            if (bytes == null || bytes.Length < iIndex + SizeInBytes)
                return null;
            MM3Character character = new MM3Character();
            character.SetCharFromStream(0, new MemoryStream(bytes, iIndex, bytes.Length - iIndex), info, null, bRosterFile);
            return character;
        }

        public override CharacterOffsets Offsets { get { return MM3.Offsets; } }
        public override int CharacterSize { get { return SizeInBytes; } }
        public override GameNames Game { get { return GameNames.MightAndMagic3; } }
        public override Inventory BasicInventory { get { return Inventory; } }

        public override ResistanceValue[] GetResistances()
        {
            return new ResistanceValue[] {
                new ResistanceValue(GenericResistanceFlags.Fire, FireResist.Permanent, FireResist.Modifier, Modifiers.Fire),
                new ResistanceValue(GenericResistanceFlags.Cold, ColdResist.Permanent, ColdResist.Modifier, Modifiers.Cold),
                new ResistanceValue(GenericResistanceFlags.Electricity, ElecResist.Permanent, ElecResist.Modifier, Modifiers.Electricity),
                new ResistanceValue(GenericResistanceFlags.Poison, PoisonResist.Permanent, PoisonResist.Modifier, Modifiers.Poison),
                new ResistanceValue(GenericResistanceFlags.Energy, EnergyResist.Permanent, EnergyResist.Modifier, Modifiers.Energy),
                new ResistanceValue(GenericResistanceFlags.Magic, MagicResist.Permanent, MagicResist.Modifier, Modifiers.Magic),
                new ResistanceValue(GenericResistanceFlags.Blessed, Protection.Blessed),
                new ResistanceValue(GenericResistanceFlags.PowerShield, Protection.PowerShield),
                new ResistanceValue(GenericResistanceFlags.HolyBonus, Protection.HolyBonus),
                new ResistanceValue(GenericResistanceFlags.Heroism, Protection.Heroism)
            };
        }

        public override void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info, EncounterInfo encounterInfo = null, bool bFromRosterFile = false)
        {
            if (stream.Length < SizeInBytes)
                return;

            RawBytes = new byte[SizeInBytes];
            stream.Read(RawBytes, 0, SizeInBytes);

            CharName = Global.GetNullTerminatedString(RawBytes, Offsets.Name, 10);
            NameTerminator = RawBytes[Offsets.NameTerminator];
            Unknowns1 = new byte[15 - 11 + 1];
            Buffer.BlockCopy(RawBytes, 11, Unknowns1, 0, 15 - 11 + 1);
            Sex = SexFromByte(RawBytes[Offsets.Sex]);
            Race = MM3RaceFromByte(RawBytes[Offsets.Race]);
            Alignment = (MM345AlignmentValue)RawBytes[Offsets.Alignment];
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
            Skills = new MM3SecondarySkills(RawBytes, Offsets.Skills);
            Awards = MM3Awards.Create(RawBytes, Offsets.Awards);
            Spells = new MM3KnownSpells(RawBytes, Offsets.Spells, BasicClass);
            Beacon = new MMBeacon(RawBytes, Offsets.Beacon);
            Unknown3 = RawBytes[122];
            ReadySpell = RawBytes[Offsets.ReadySpell];
            Unknown2 = RawBytes[124];
            Inventory = MM3Inventory.Create(RawBytes, 0);
            Modifiers = Inventory.GetModifiers();
            Protection = new MMProtections(RawBytes, Offsets.Protection);
            Donations = (MM3DonationFlags)RawBytes[Offsets.Donations];
            FireResist = new OneByteStatModifier(RawBytes, Offsets.FireResist);
            ColdResist = new OneByteStatModifier(RawBytes, Offsets.ColdResist);
            ElecResist = new OneByteStatModifier(RawBytes, Offsets.ElecResist);
            PoisonResist = new OneByteStatModifier(RawBytes, Offsets.PoisonResist);
            EnergyResist = new OneByteStatModifier(RawBytes, Offsets.EnergyResist);
            MagicResist = new OneByteStatModifier(RawBytes, Offsets.MagicResist);
            Condition = new MM3Condition(RawBytes, Offsets.Condition);
            Modifiers.Adjust(Condition.GetModifiers());
            Town = RawBytes[Offsets.Town];
            Unknown4 = RawBytes[292];
            CurrentHP = BitConverter.ToInt16(RawBytes, Offsets.CurrentHP);
            CurrentSP = BitConverter.ToUInt16(RawBytes, Offsets.CurrentSP);
            BirthYear = BitConverter.ToUInt16(RawBytes, Offsets.BirthYear);
            Experience = (uint)(BitConverter.ToUInt32(RawBytes, Offsets.Experience) + XPForLevel(Class, Level.Permanent));
            if (info is MM3GameInfo)
                Modifiers.Adjust(GetModifier(BasicAge, (info as MM3GameInfo).Party.Year));
        }

        public bool AnyTempStat(int iMin)
        {
            return (Might.Modifier > iMin ||
                Intellect.Modifier > iMin ||
                Personality.Modifier > iMin ||
                Endurance.Modifier > iMin ||
                Speed.Modifier > iMin ||
                Accuracy.Modifier > iMin ||
                Luck.Modifier > iMin);
        }

        public bool AnyPermStat(int iMin)
        {
            return (Might.Permanent > iMin ||
                Intellect.Permanent > iMin ||
                Personality.Permanent > iMin ||
                Endurance.Permanent > iMin ||
                Speed.Permanent > iMin ||
                Accuracy.Permanent > iMin ||
                Luck.Permanent > iMin);
        }

        public bool AllPermStats(int iMin)
        {
            return (Might.Permanent > iMin &&
                Intellect.Permanent > iMin &&
                Personality.Permanent > iMin &&
                Endurance.Permanent > iMin &&
                Speed.Permanent > iMin &&
                Accuracy.Permanent > iMin &&
                Luck.Permanent > iMin);
        }

        public bool AllTempStats(int iMin)
        {
            return (Might.Modifier > iMin &&
                Intellect.Modifier > iMin &&
                Personality.Modifier > iMin &&
                Endurance.Modifier > iMin &&
                Speed.Modifier > iMin &&
                Accuracy.Modifier > iMin &&
                Luck.Modifier > iMin);
        }

        public bool AllTempRes(int iMin)
        {
            return (FireResist.Modifier > iMin &&
                ColdResist.Modifier > iMin &&
                ElecResist.Modifier > iMin &&
                PoisonResist.Modifier > iMin &&
                EnergyResist.Modifier > iMin &&
                MagicResist.Modifier > iMin);
        }

        public override string EquippedString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (MM3Item item in Inventory.Items)
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
                foreach (MM3Item item in Inventory.Items)
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

        public override GenericRace BasicRace
        {
            get
            {
                switch (Race)
                {
                    case MM3Race.Human: return GenericRace.Human;
                    case MM3Race.Elf: return GenericRace.Elf;
                    case MM3Race.Gnome: return GenericRace.Gnome;
                    case MM3Race.Dwarf: return GenericRace.Dwarf;
                    case MM3Race.HalfOrc: return GenericRace.HalfOrc;
                    default: return GenericRace.None;
                }
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
                sb.AppendFormat("Condition: {0}\n", MM3Character.ConditionString(Inventory, Condition, true));
                sb.AppendFormat("HP: {0}\n", QuickRefHitPoints.ToString());
                sb.AppendFormat("SP: {0}\n", QuickRefSpellPoints.ToString());
                sb.AppendFormat("AC: {0}\n", GetACString());
                sb.AppendFormat("Resist: {0}\n", ResistancesString);
                sb.AppendFormat("Damage: {0} melee, {1} ranged\n", MeleeDamageString, RangedDamageString);
                sb.AppendFormat("Equipped: {0}\n", EquippedString);
                sb.AppendFormat("Backpack: {0}\n", BackpackString);
                return sb.ToString();
            }
        }

        public override bool KnowsSpell(Spell spell)
        {
            if (!(spell is MM3Spell && Spells is MM3KnownSpells))
                return false;

            if (Spells == null)
                return false;

            return ((MM3KnownSpells)Spells).IsKnown(spell as MM3Spell);
        }

        public override int MaxBackpackSize { get { return 18 - Inventory.Items.Count(i => ((MM3Item)i).WhereEquipped != EquipLocation.None); } }

        public override List<Item> BackpackItems
        {
            get
            {
                List<Item> items = new List<Item>();
                foreach (Item item in Inventory.Items)
                {
                    MM3Item mm3Item = item as MM3Item;
                    if (mm3Item.WhereEquipped == EquipLocation.None)
                        items.Add(item);
                }
                return items;
            }
        }

        public override Item GetItem(byte[] bytes, int offset = 0)
        {
            // equip, charges, prefix1, prefix2, prefix3, item, suffix
            if (bytes.Length - offset < 7)
                return null;

            if (bytes[offset] >= (int)EquipLocation.Invalid)
                return null;
            if (bytes[offset + 2] >= (int)MM3ItemElementalIndex.Invalid)
                return null;
            if (bytes[offset + 3] >= (int)MM3ItemMaterialIndex.Invalid)
                return null;
            if (bytes[offset + 4] >= (int)MM3ItemAttributeIndex.Invalid)
                return null;
            if (bytes[offset + 5] >= (int)MM3ItemIndex.Invalid)
                return null;
            if (bytes[offset + 6] >= (int)MM3ItemPropertyIndex.Invalid)
                return null;

            MM3Item item = MM3Item.Create(bytes[offset], bytes[offset + 1], bytes[offset + 2], bytes[offset + 3], bytes[offset + 4], bytes[offset + 5], bytes[offset + 6]);
            return item;
        }

        public override int FirstEmptyBackpackIndex
        {
            get
            {
                if (Inventory == null)
                    return -1;
                if (Inventory.Items.Count > 17)
                    return -1;
                int[] used = new int[18] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int i = 0; i < Inventory.Items.Count; i++)
                    if (Inventory.Items[i].MemoryIndex > -1 && Inventory.Items[i].MemoryIndex < 18)
                        used[Inventory.Items[i].MemoryIndex] = 1;
                for (int i = 0; i < 18; i++)
                    if (used[i] == 0)
                        return i;
                return -1;
            }
        }

        public static byte[] GetConditionProtectionBytes(MMCondition condition, MMProtections protection)
        {
            return new byte[] {
                condition.Cursed,
                condition.HeartBroken,
                condition.Weak,
                condition.Poisoned,
                condition.Diseased,
                condition.Insane,
                condition.InLove,
                condition.Drunk,
                condition.Asleep,
                condition.Depressed,
                condition.Confused,
                condition.Paralyzed,
                condition.Unconscious,
                condition.Dead,
                condition.Stone,
                condition.Eradicated,
                protection.Blessed,
                protection.PowerShield,
                protection.HolyBonus,
                protection.Heroism,
            };
        }

        public void SetConditionProtectionFromBytes(byte[] bytes)
        {
            Condition = new MM3Condition();
            Protection = new MMProtections();
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
            Protection.Blessed = bytes[16];
            Protection.PowerShield = bytes[17];
            Protection.HolyBonus = bytes[18];
            Protection.Heroism = bytes[19];
        }

        public override string ReadySpellString { get { return MM3SpellList.GetSpellName((MM3InternalSpellIndex)ReadySpell); } }

        public override bool BackpackFull
        {
            get
            {
                return (Inventory.NumBackpackItems >= MaxBackpackSize);
            }
        }

        public static string AwardString(MM3AwardIndex award)
        {
            switch (award)
            {
                case MM3AwardIndex.RavensGuildMember: return "Ravens Guild Member";
                case MM3AwardIndex.AlbatrossGuildMember: return "Albatross Guild Member";
                case MM3AwardIndex.FalconsGuildMember: return "Falcons Guild Member";
                case MM3AwardIndex.BuzzardsGuildMember: return "Buzzards Guild Member";
                case MM3AwardIndex.EaglesGuildMember: return "Eagles Guild Member";
                case MM3AwardIndex.SavedFountainHead: return "Saved Fountain Head";
                case MM3AwardIndex.BlessedByTheForces: return "Blessed By The Forces";
                case MM3AwardIndex.OrbsGivenToZealot: return "Orbs Given to Zealot";
                case MM3AwardIndex.OrbsGivenToMalefactor: return "Orbs Given to Malefactor";
                case MM3AwardIndex.OrbsGivenToTumult: return "Orbs Given To Tumult";
                case MM3AwardIndex.ChampionOfAlignment: return "Champion of Good/Neutrality/Evil";
                case MM3AwardIndex.GoodArtifactsRecovered: return "Good Artifacts Recovered";
                case MM3AwardIndex.EvilArtifactsRecovered: return "Evil Artifacts Recovered";
                case MM3AwardIndex.NeutralArtifactsRecovered: return "Neutral Artifacts Recovered";
                case MM3AwardIndex.ShellsGivenToAthea: return "Shells Given to Athea";
                case MM3AwardIndex.GreekBrothersVisited: return "Greek Brothers Visited";
                case MM3AwardIndex.GreywindReleased645: return "Greywind Released 645";
                case MM3AwardIndex.BlackwindReleased231: return "Blackwind Released 231";
                case MM3AwardIndex.SkullsGivenToKranion: return "Skulls Given to Kranion";
                case MM3AwardIndex.IcarusResurrected: return "Icarus Resurrected";
                case MM3AwardIndex.FreedPrincessTrueberry: return "Freed Princess Trueberry";
                case MM3AwardIndex.ArenaWins: return "Arena Wins";
                case MM3AwardIndex.PearlsToPirateQueen: return "Pearls to Pirate Queen";
                case MM3AwardIndex.UltimateAdventurer: return "Ultimate Adventurer";
                default: return String.Format("Unknown({0})", (int)award);
            }
        }

        public override string GetACFormula(int iBless = 0)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}\tSpeed modifier", Global.AddPlus(GetStatModifier(Speed.Temporary, PrimaryStat.Speed).Value));
            if (Protection.Blessed != 0)
                sb.AppendFormat("\r\n{0}\tBlessed", Global.AddPlus(Protection.Blessed));
            return sb.ToString();
        }

        public override Modifiers InternalModifiers { get { return MM3.Modifiers.For(BasicRace); } }
    }

    [Flags]
    public enum MM3DonationFlags
    {
        None = 0,
        BlisteringHeights = 0x08,
        SwampTown = 0x10,
        Wildabar = 0x20,
        Baywatch = 0x40,
        FountainHead = 0x80
    }

    public class MMProtections
    {
        public byte Blessed;
        public byte PowerShield;
        public byte HolyBonus;
        public byte Heroism;

        public MMProtections(byte[] bytes, int index = 0)
        {
            SetFromBytes(bytes, index);
        }

        public MMProtections()
        {
            Blessed = 0;
            PowerShield = 0;
            HolyBonus = 0;
            Heroism = 0;
        }

        private void SetFromBytes(byte[] bytes, int index)
        {
            if (bytes.Length < 4)
                return;

            Blessed = bytes[index + 0];
            PowerShield = bytes[index + 1];
            HolyBonus = bytes[index + 2];
            Heroism = bytes[index + 3];
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[4];
            bytes[0] = Blessed;
            bytes[1] = PowerShield;
            bytes[2] = HolyBonus;
            bytes[3] = Heroism;
            return bytes;
        }
    }

    public class MM3Condition : MMCondition
    {
        public MM3Condition(byte[] bytes, int index = 0)
        {
            SetFromBytes(bytes, index);
        }

        public MM3Condition()
        {
            SetZero();
        }

        public static byte[] GetConditionProtectionBytes(MMCondition condition, MMProtections protection)
        {
            return new byte[] {
                condition.Cursed,
                condition.HeartBroken,
                condition.Weak,
                condition.Poisoned,
                condition.Diseased,
                condition.Insane,
                condition.InLove,
                condition.Drunk,
                condition.Asleep,
                condition.Depressed,
                condition.Confused,
                condition.Paralyzed,
                condition.Unconscious,
                condition.Dead,
                condition.Stone,
                condition.Eradicated,
                protection.Blessed,
                protection.PowerShield,
                protection.HolyBonus,
                protection.Heroism,
            };
        }
    }

    public class MM3SecondarySkills : MM345SecondarySkills
    {
        public MM3SecondarySkills(byte b)
        {
            SetAll(b);
        }

        public MM3SecondarySkills(byte[] bytes, int index = 0)
        {
            SetFromBytes(bytes, index);
        }

        public MM3SecondarySkills(MM3QuestStates.Skills skills)
        {
            SetFromSkills(skills);
        }

        public override string WhereLearned(MMSecondarySkillIndex skill)
        {
            switch (skill)
            {
                case MMSecondarySkillIndex.ArmsMaster: return "Wildabar (1,1), 500 gold";
                case MMSecondarySkillIndex.Astrologer: return "B1 (2,4), 5000 gold";
                case MMSecondarySkillIndex.BodyBuilder: return "Wildabar (1,2), 200 gold";
                case MMSecondarySkillIndex.Cartographer: return "Fountain Head (6,14), 25 gold";
                case MMSecondarySkillIndex.Crusader: return "Ancient Temple of Moo (29,15), free";
                case MMSecondarySkillIndex.DangerSense: return "Fountain Head Cavern (0,5), 500 gold";
                case MMSecondarySkillIndex.DirectionSense: return "Fountain Head Cavern (2,5), 100 gold";
                case MMSecondarySkillIndex.Linguist: return "Arachnoid Cavern (22,17), 50 gems";
                case MMSecondarySkillIndex.Merchant: return "A1 (14,8), 5000 gold";
                case MMSecondarySkillIndex.Mountaineer: return "Baywatch (11,11), 5000 gold";
                case MMSecondarySkillIndex.Navigator: return "Wildabar (1,4), 3000 gold";
                case MMSecondarySkillIndex.PathFinder: return "Baywatch (11,14), 2500 gold";
                case MMSecondarySkillIndex.PrayerMaster: return "Arachnoid Cavern (23,13), 500 gems";
                case MMSecondarySkillIndex.Prestidigitator: return "Arachnoid Cavern (23,11), 500 gems";
                case MMSecondarySkillIndex.SpotSecretDoors: return "Fountain Head Cavern (13,15), 250 gold";
                case MMSecondarySkillIndex.Swimmer: return "Fountain Head Cavern (15,8), 200 gold";
                case MMSecondarySkillIndex.Thievery: return "Halls of Insanity (4,29), 100000 gold";
                case MMSecondarySkillIndex.Tracker: return "Halls of Insanity (4,29), 100000 gold";
                default: return "Unknown";
            }
        }
    }

    public enum MM3AwardIndex
    {
        RavensGuildMember = 0,
        AlbatrossGuildMember = 1,
        FalconsGuildMember = 2,
        BuzzardsGuildMember = 3,
        EaglesGuildMember = 4,
        SavedFountainHead = 5,
        BlessedByTheForces = 6,
        OrbsGivenToZealot = 7,
        OrbsGivenToMalefactor = 8,
        OrbsGivenToTumult = 9,
        ChampionOfAlignment = 10,
        GoodArtifactsRecovered = 11,
        EvilArtifactsRecovered = 12,
        NeutralArtifactsRecovered = 13,
        ShellsGivenToAthea = 14,
        GreekBrothersVisited = 15,
        GreywindReleased645 = 16,
        BlackwindReleased231 = 17,
        SkullsGivenToKranion = 18,
        IcarusResurrected = 19,
        FreedPrincessTrueberry = 20,
        ArenaWins = 21,
        PearlsToPirateQueen = 22,
        UltimateAdventurer = 23,
        Last = 24,
        Unknown2 = 24,
        Unknown3 = 25
    }

    public class MM3Awards : MM345Awards
    {
        public byte RavensGuildMember;
        public byte AlbatrossGuildMember;
        public byte FalconsGuildMember;
        public byte BuzzardsGuildMember;
        public byte EaglesGuildMember;
        public byte SavedFountainHead;
        public byte BlessedByTheForces;
        public byte OrbsGivenToZealot;
        public byte OrbsGivenToMalefactor;
        public byte OrbsGivenToTumult;
        public byte ChampionOfAlignment;
        public byte GoodArtifactsRecovered;
        public byte EvilArtifactsRecovered;
        public byte NeutralArtifactsRecovered;
        public byte ShellsGivenToAthea;
        public byte GreekBrothersVisited;
        public byte GreywindReleased645;
        public byte BlackwindReleased231;
        public byte SkullsGivenToKranion;
        public byte IcarusResurrected;
        public byte FreedPrincessTrueberry;
        public byte ArenaWins;
        public byte PearlsToPirateQueen;
        public byte UltimateAdventurer;
        public byte Unknown2;
        public byte Unknown3;

        public MM3Awards()
        {
        }

        public static MM3Awards Create(byte[] bytes, int index = 0)
        {
            MM3Awards awards = new MM3Awards();
            awards.SetFromBytes(bytes, index);
            return awards;
        }

        public override void Clear()
        {
            RavensGuildMember = 0;
            AlbatrossGuildMember = 0;
            FalconsGuildMember = 0;
            BuzzardsGuildMember = 0;
            EaglesGuildMember = 0;
            SavedFountainHead = 0;
            BlessedByTheForces = 0;
            OrbsGivenToZealot = 0;
            OrbsGivenToMalefactor = 0;
            OrbsGivenToTumult = 0;
            ChampionOfAlignment = 0;
            GoodArtifactsRecovered = 0;
            EvilArtifactsRecovered = 0;
            NeutralArtifactsRecovered = 0;
            ShellsGivenToAthea = 0;
            GreekBrothersVisited = 0;
            GreywindReleased645 = 0;
            BlackwindReleased231 = 0;
            SkullsGivenToKranion = 0;
            IcarusResurrected = 0;
            FreedPrincessTrueberry = 0;
            ArenaWins = 0;
            PearlsToPirateQueen = 0;
            UltimateAdventurer = 0;
            Unknown2 = 0;
            Unknown3 = 0;
        }

        protected override void SetFromBytes(byte[] bytes, int index)
        {
            if (bytes.Length < 26)
                return;

            RavensGuildMember = bytes[index + (int) MM3AwardIndex.RavensGuildMember];
            AlbatrossGuildMember = bytes[index + (int) MM3AwardIndex.AlbatrossGuildMember];
            FalconsGuildMember = bytes[index + (int) MM3AwardIndex.FalconsGuildMember];
            BuzzardsGuildMember = bytes[index + (int) MM3AwardIndex.BuzzardsGuildMember];
            EaglesGuildMember = bytes[index + (int) MM3AwardIndex.EaglesGuildMember];
            SavedFountainHead = bytes[index + (int) MM3AwardIndex.SavedFountainHead];
            BlessedByTheForces = bytes[index + (int) MM3AwardIndex.BlessedByTheForces];
            OrbsGivenToZealot = bytes[index + (int) MM3AwardIndex.OrbsGivenToZealot];
            OrbsGivenToMalefactor = bytes[index + (int) MM3AwardIndex.OrbsGivenToMalefactor];
            OrbsGivenToTumult = bytes[index + (int) MM3AwardIndex.OrbsGivenToTumult];
            ChampionOfAlignment = bytes[index + (int) MM3AwardIndex.ChampionOfAlignment];
            GoodArtifactsRecovered = bytes[index + (int) MM3AwardIndex.GoodArtifactsRecovered];
            EvilArtifactsRecovered = bytes[index + (int) MM3AwardIndex.EvilArtifactsRecovered];
            NeutralArtifactsRecovered = bytes[index + (int) MM3AwardIndex.NeutralArtifactsRecovered];
            ShellsGivenToAthea = bytes[index + (int) MM3AwardIndex.ShellsGivenToAthea];
            GreekBrothersVisited = bytes[index + (int) MM3AwardIndex.GreekBrothersVisited];
            GreywindReleased645 = bytes[index + (int) MM3AwardIndex.GreywindReleased645];
            BlackwindReleased231 = bytes[index + (int) MM3AwardIndex.BlackwindReleased231];
            SkullsGivenToKranion = bytes[index + (int) MM3AwardIndex.SkullsGivenToKranion];
            IcarusResurrected = bytes[index + (int) MM3AwardIndex.IcarusResurrected];
            FreedPrincessTrueberry = bytes[index + (int) MM3AwardIndex.FreedPrincessTrueberry];
            ArenaWins = bytes[index + (int) MM3AwardIndex.ArenaWins];
            PearlsToPirateQueen = bytes[index + (int) MM3AwardIndex.PearlsToPirateQueen];
            UltimateAdventurer = bytes[index + (int) MM3AwardIndex.UltimateAdventurer];
            Unknown2 = bytes[index + (int) MM3AwardIndex.Unknown2];
            Unknown3 = bytes[index + (int) MM3AwardIndex.Unknown3];
        }

        public override byte[] GetBytes()
        {
            byte[] bytes = new byte[26];
            bytes[(int)MM3AwardIndex.RavensGuildMember] = RavensGuildMember;
            bytes[(int)MM3AwardIndex.AlbatrossGuildMember] = AlbatrossGuildMember;
            bytes[(int)MM3AwardIndex.FalconsGuildMember] = FalconsGuildMember;
            bytes[(int)MM3AwardIndex.BuzzardsGuildMember] = BuzzardsGuildMember;
            bytes[(int)MM3AwardIndex.EaglesGuildMember] = EaglesGuildMember;
            bytes[(int)MM3AwardIndex.SavedFountainHead] = SavedFountainHead;
            bytes[(int)MM3AwardIndex.BlessedByTheForces] = BlessedByTheForces;
            bytes[(int)MM3AwardIndex.OrbsGivenToZealot] = OrbsGivenToZealot;
            bytes[(int)MM3AwardIndex.OrbsGivenToMalefactor] = OrbsGivenToMalefactor;
            bytes[(int)MM3AwardIndex.OrbsGivenToTumult] = OrbsGivenToTumult;
            bytes[(int)MM3AwardIndex.ChampionOfAlignment] = ChampionOfAlignment;
            bytes[(int)MM3AwardIndex.GoodArtifactsRecovered] = GoodArtifactsRecovered;
            bytes[(int)MM3AwardIndex.EvilArtifactsRecovered] = EvilArtifactsRecovered;
            bytes[(int)MM3AwardIndex.NeutralArtifactsRecovered] = NeutralArtifactsRecovered;
            bytes[(int)MM3AwardIndex.ShellsGivenToAthea] = ShellsGivenToAthea;
            bytes[(int)MM3AwardIndex.GreekBrothersVisited] = GreekBrothersVisited;
            bytes[(int)MM3AwardIndex.GreywindReleased645] = GreywindReleased645;
            bytes[(int)MM3AwardIndex.BlackwindReleased231] = BlackwindReleased231;
            bytes[(int)MM3AwardIndex.SkullsGivenToKranion] = SkullsGivenToKranion;
            bytes[(int)MM3AwardIndex.IcarusResurrected] = IcarusResurrected;
            bytes[(int)MM3AwardIndex.FreedPrincessTrueberry] = FreedPrincessTrueberry;
            bytes[(int)MM3AwardIndex.ArenaWins] = ArenaWins;
            bytes[(int)MM3AwardIndex.PearlsToPirateQueen] = PearlsToPirateQueen;
            bytes[(int)MM3AwardIndex.UltimateAdventurer] = UltimateAdventurer;
            bytes[(int)MM3AwardIndex.Unknown2] = Unknown2;
            bytes[(int)MM3AwardIndex.Unknown3] = Unknown3; 
            return bytes;
        }
    }

    public class MM3Inventory : Inventory
    {
        private List<Item> m_items;

        public override List<Item> Items
        {
            get { return m_items; }
            set { m_items = value; }
        }

        public MM3Inventory()
        {
        }

        public static MM3Inventory Create(byte[] bytes, int index)
        {
            MM3Inventory inventory = new WhereAreWe.MM3Inventory();
            inventory.SetFromBytes(bytes, index);
            return inventory;
        }

        public void SetFromBytes(byte[] bytes, int index)
        {
            Items = new List<Item>(18);
            for (int i = 0; i < 18; i++)
            {
                MM3Item item = MM3Item.Create(
                    bytes[index + i + MM3.Offsets.InvEquipLoc],
                    bytes[index + i + MM3.Offsets.InvCharges],
                    bytes[index + i + MM3.Offsets.InvElements],
                    bytes[index + i + MM3.Offsets.InvMaterials],
                    bytes[index + i + MM3.Offsets.InvAttributes],
                    bytes[index + i + MM3.Offsets.InvBases],
                    bytes[index + i + MM3.Offsets.InvProperties]
                    );
                item.MemoryIndex = i;
                item.DisplayIndex = String.Format("{0}", i % 9 + 1);
                if (item != null && item.Base != MM3ItemIndex.None)
                    Items.Add(item);
            }
        }

        public override int NumBackpackItems
        {
            get
            {
                int iCount = 0;
                foreach (MM3Item item in Items)
                    if (item.WhereEquipped == EquipLocation.None)
                        iCount++;
                return iCount;
            }
        }

        public void SetBytes(byte[] bytes, int index)
        {
            // Fill the given array of bytes with data representing this inventory
        }

        public bool HasItem(MM3ItemIndex itemWanted)
        {
            foreach (MM3Item item in Items)
                if (item.Index == (int)itemWanted)
                    return true;
            return false;
        }

        public override bool BrokenEquipped
        {
            get
            {
                foreach (MM3Item item in Items)
                    if (item.WhereEquipped != EquipLocation.None && item.Broken)
                        return true;
                return false;
            }
        }

        public override bool CursedEquipped
        {
            get
            {
                foreach (MM3Item item in Items)
                    if (item.WhereEquipped != EquipLocation.None && item.Cursed)
                        return true;
                return false;
            }
        }

        private void AddModifier(ref Modifiers mod, MM3Item item)
        {
            mod.Adjust(ModAttr.ArmorClass, MM3Item.GetArmorClass(item.Base), item.DescriptionString); ;
            HitDamageAC hda = MM3Item.MaterialEffect(item.Material);
            mod.Adjust(hda,  item.Type == ItemType.None ? ItemType.Accessory : item.Type, item.DescriptionString);
            AttributeModifier attrib = MM3Item.AttributeEffect(item.Attribute);
            mod.Adjust(attrib, item.DescriptionString);
            ElementDamageResistance resist = MM3Item.ElementalEffect(item.Element);
            mod.Adjust(resist, item.Type, item.DescriptionString, true);
        }

        public override Modifiers GetModifiers()
        {
            Modifiers mod = new Modifiers();

            foreach (MM3Item item in Items)
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
                foreach(MM3Item item in Items)
                {
                    if (item.WhereEquipped != EquipLocation.None)
                    {
                        switch (item.Type)
                        {
                            case ItemType.OneHandMelee:
                            case ItemType.TwoHandMelee:
                                return new BasicDamage(1, MM3Item.GetItemDamage(item.Base));
                            default:
                                break;
                        }
                    }
                }
                return BasicDamage.Zero;
            }
        }

        public override BasicDamage  RangedWeaponDamage
        {
            get
            {
                foreach (MM3Item item in Items)
                {
                    if (item.WhereEquipped != EquipLocation.None)
                    {
                        switch (item.Type)
                        {
                            case ItemType.Missile:
                                return new BasicDamage(1, MM3Item.GetItemDamage(item.Base));
                            default:
                                break;
                        }
                    }
                }
                return BasicDamage.Zero;
            }
        }
    }

    public class MM3KnownSpells : MM345KnownSpells
    {
        public MM3KnownSpells(GenericClass charClass)
        {
            RawBytes = new byte[36];
            for (int i = 0; i < 36; i++)
                RawBytes[i] = 0;
            m_iNumKnown = 0;
            Type = Global.GetSpellType(charClass);
        }

        public MM3KnownSpells(byte[] bytes, int iIndex, GenericClass charClass)
        {
            RawBytes = new byte[36];
            Buffer.BlockCopy(bytes, iIndex, RawBytes, 0, 36);
            m_iNumKnown = -1;
            Type = Global.GetSpellType(charClass);
        }

        public override KnownSpells CreateNew(GenericClass charClass, KnownSpells original = null) { return new MM3KnownSpells(charClass); }
        public override void SetKnown(Spell spell, bool bKnown) { SetKnown(spell as MM3Spell, bKnown); }

        public static MM3InternalSpellIndex GetInternalIndex(MM3SpellIndex idxInternal)
        {
            int iSpell = (int)idxInternal;
            if (iSpell < 0 || iSpell >= MM3.Spells.Count)
                return MM3InternalSpellIndex.None;

            return MM3.Spells[(MM3SpellIndex)iSpell].InternalIndex;
        }

        public override string KnownString(GenericClass charClass)
        {
            int iMax = 0;
            switch (Global.GetSpellType(charClass))
            {
                case SpellType.Cleric:
                case SpellType.Sorcerer:
                    iMax = 36;
                    break;
                case SpellType.Druid:
                    iMax = 29;
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

        public void SetKnown(MM3Spell spell, bool bKnown)
        {
            if (spell == null || spell.InternalIndex == MM3InternalSpellIndex.None)
                return;

            int iRawIndex = RawByteIndex(spell);
            if (iRawIndex < 0 || iRawIndex >= RawBytes.Length)
                return;

            RawBytes[iRawIndex] = (byte) (bKnown ? 1 : 0);

            m_iNumKnown = -1;
        }

        public void SetKnown(MM3InternalSpellIndex index, SpellType type, bool bKnown)
        {
            if (index == MM3InternalSpellIndex.None)
                return;

            int iRawIndex = RawByteIndex(index, type);
            if (iRawIndex < 0 || iRawIndex >= RawBytes.Length)
                return;

            RawBytes[iRawIndex] = (byte)(bKnown ? 1 : 0);

            m_iNumKnown = -1;
        }

        public int RawByteIndex(MM3InternalSpellIndex index, SpellType type)
        {
            switch (type)
            {
                case SpellType.Cleric:
                    switch (index)
                    {
                        case MM3InternalSpellIndex.Light: return 0;
                        case MM3InternalSpellIndex.Awaken: return 1;
                        case MM3InternalSpellIndex.FirstAid: return 2;
                        case MM3InternalSpellIndex.FlyingFist: return 3;
                        case MM3InternalSpellIndex.Revitalize: return 4;
                        case MM3InternalSpellIndex.CureWounds: return 5;
                        case MM3InternalSpellIndex.Sparks: return 6;
                        case MM3InternalSpellIndex.ProtFromElements: return 7;
                        case MM3InternalSpellIndex.Pain: return 8;
                        case MM3InternalSpellIndex.SuppressPoison: return 9;
                        case MM3InternalSpellIndex.SuppressDisease: return 10;
                        case MM3InternalSpellIndex.TurnUndead: return 11;
                        case MM3InternalSpellIndex.Silence: return 12;
                        case MM3InternalSpellIndex.Blessed: return 13;
                        case MM3InternalSpellIndex.HolyBonus: return 14;
                        case MM3InternalSpellIndex.PowerCure: return 15;
                        case MM3InternalSpellIndex.Heroism: return 16;
                        case MM3InternalSpellIndex.Immobilize: return 17;
                        case MM3InternalSpellIndex.ColdRay: return 18;
                        case MM3InternalSpellIndex.CurePoison: return 19;
                        case MM3InternalSpellIndex.AcidSpray: return 20;
                        case MM3InternalSpellIndex.CureDisease: return 21;
                        case MM3InternalSpellIndex.CureParalysis: return 22;
                        case MM3InternalSpellIndex.Paralyze: return 23;
                        case MM3InternalSpellIndex.CreateFood: return 24;
                        case MM3InternalSpellIndex.FieryFlail: return 25;
                        case MM3InternalSpellIndex.TownPortal: return 26;
                        case MM3InternalSpellIndex.StoneToFlesh: return 27;
                        case MM3InternalSpellIndex.HalfForMe: return 28;
                        case MM3InternalSpellIndex.RaiseDead: return 29;
                        case MM3InternalSpellIndex.MoonRay: return 30;
                        case MM3InternalSpellIndex.MassDistortion: return 31;
                        case MM3InternalSpellIndex.HolyWord: return 32;
                        case MM3InternalSpellIndex.Resurrect: return 33;
                        case MM3InternalSpellIndex.SunRay: return 34;
                        case MM3InternalSpellIndex.DivineIntervention: return 35;
                        default: return -1;
                    }
                case SpellType.Sorcerer:
                    switch (index)
                    {
                        case MM3InternalSpellIndex.Light: return 0;
                        case MM3InternalSpellIndex.Awaken: return 1;
                        case MM3InternalSpellIndex.DetectMagic: return 2;
                        case MM3InternalSpellIndex.ElementalArrow: return 3;
                        case MM3InternalSpellIndex.EnergyBlast: return 4;
                        case MM3InternalSpellIndex.Sleep: return 5;
                        case MM3InternalSpellIndex.CreateRope: return 6;
                        case MM3InternalSpellIndex.ToxicCloud: return 7;
                        case MM3InternalSpellIndex.Jump: return 8;
                        case MM3InternalSpellIndex.AcidStream: return 9;
                        case MM3InternalSpellIndex.Levitate: return 10;
                        case MM3InternalSpellIndex.WizardEye: return 11;
                        case MM3InternalSpellIndex.IdentifyMonster: return 12;
                        case MM3InternalSpellIndex.LightningBolt: return 13;
                        case MM3InternalSpellIndex.LloydsBeacon: return 14;
                        case MM3InternalSpellIndex.PowerShield: return 15;
                        case MM3InternalSpellIndex.DetectMonster: return 16;
                        case MM3InternalSpellIndex.Fireball: return 17;
                        case MM3InternalSpellIndex.TimeDistortion: return 18;
                        case MM3InternalSpellIndex.FeebleMind: return 19;
                        case MM3InternalSpellIndex.Teleport: return 20;
                        case MM3InternalSpellIndex.FingerOfDeath: return 21;
                        case MM3InternalSpellIndex.SuperShelter: return 22;
                        case MM3InternalSpellIndex.DragonBreath: return 23;
                        case MM3InternalSpellIndex.RechargeItem: return 24;
                        case MM3InternalSpellIndex.FantasticFreeze: return 25;
                        case MM3InternalSpellIndex.Duplication: return 26;
                        case MM3InternalSpellIndex.Disintegrate: return 27;
                        case MM3InternalSpellIndex.Etherealize: return 28;
                        case MM3InternalSpellIndex.DancingSword: return 29;
                        case MM3InternalSpellIndex.EnchantItem: return 30;
                        case MM3InternalSpellIndex.Incinerate: return 31;
                        case MM3InternalSpellIndex.MegaVolts: return 32;
                        case MM3InternalSpellIndex.Inferno: return 33;
                        case MM3InternalSpellIndex.Implosion: return 34;
                        case MM3InternalSpellIndex.StarBurst: return 35;
                        default: return -1;
                    }
                case SpellType.Druid:
                    switch (index)
                    {
                        case MM3InternalSpellIndex.Light: return 0;
                        case MM3InternalSpellIndex.Awaken: return 1;
                        case MM3InternalSpellIndex.FirstAid: return 2;
                        case MM3InternalSpellIndex.DetectMagic: return 3;
                        case MM3InternalSpellIndex.ElementalArrow: return 4;
                        case MM3InternalSpellIndex.Revitalize: return 5;
                        case MM3InternalSpellIndex.Sleep: return 6;
                        case MM3InternalSpellIndex.CreateRope: return 7;
                        case MM3InternalSpellIndex.SuppressPoison: return 8;
                        case MM3InternalSpellIndex.ProtFromElements: return 9;
                        case MM3InternalSpellIndex.SuppressDisease: return 10;
                        case MM3InternalSpellIndex.IdentifyMonster: return 11;
                        case MM3InternalSpellIndex.NaturesCure: return 12;
                        case MM3InternalSpellIndex.Immobilize: return 13;
                        case MM3InternalSpellIndex.WalkOnWater: return 14;
                        case MM3InternalSpellIndex.FrostBite: return 15;
                        case MM3InternalSpellIndex.LightningBolt: return 16;
                        case MM3InternalSpellIndex.AcidSpray: return 17;
                        case MM3InternalSpellIndex.ColdRay: return 18;
                        case MM3InternalSpellIndex.NaturesGate: return 19;
                        case MM3InternalSpellIndex.Fireball: return 20;
                        case MM3InternalSpellIndex.DeadlySwarm: return 21;
                        case MM3InternalSpellIndex.CureParalysis: return 22;
                        case MM3InternalSpellIndex.Paralyze: return 23;
                        case MM3InternalSpellIndex.CreateFood: return 24;
                        case MM3InternalSpellIndex.StoneToFlesh: return 25;
                        case MM3InternalSpellIndex.RaiseDead: return 26;
                        case MM3InternalSpellIndex.PrismaticLight: return 27;
                        case MM3InternalSpellIndex.ElementalStorm: return 28;
                        default: return -1;
                    }
                default:
                    return -1;
            }
        }

        public static int RawByteIndex(MM3Spell spell)
        {
            return RawByteIndex(spell.Index);
        }

        public static int RawByteIndex(MM3SpellIndex index)
        {
            switch (index)
            {
                case MM3SpellIndex.LightCleric:
                case MM3SpellIndex.LightDruid:
                case MM3SpellIndex.LightSorcerer: return 0;
                case MM3SpellIndex.AwakenCleric:
                case MM3SpellIndex.AwakenDruid:
                case MM3SpellIndex.AwakenSorcerer: return 1;
                case MM3SpellIndex.DetectMagic:
                case MM3SpellIndex.FirstAid:
                case MM3SpellIndex.FirstAidDruid: return 2;
                case MM3SpellIndex.DetectMagicDruid:
                case MM3SpellIndex.ElementalArrow:
                case MM3SpellIndex.FlyingFist: return 3;
                case MM3SpellIndex.ElementalArrowDruid:
                case MM3SpellIndex.EnergyBlast:
                case MM3SpellIndex.Revitalize: return 4;
                case MM3SpellIndex.CureWounds:
                case MM3SpellIndex.RevitalizeDruid:
                case MM3SpellIndex.Sleep: return 5;
                case MM3SpellIndex.CreateRope:
                case MM3SpellIndex.SleepDruid:
                case MM3SpellIndex.Sparks: return 6;
                case MM3SpellIndex.CreateRopeDruid:
                case MM3SpellIndex.ProtectionFromElements:
                case MM3SpellIndex.ToxicCloud: return 7;
                case MM3SpellIndex.Jump:
                case MM3SpellIndex.Pain:
                case MM3SpellIndex.SuppressPoisonDruid: return 8;
                case MM3SpellIndex.AcidStream:
                case MM3SpellIndex.ProtectionFromElementsDruid:
                case MM3SpellIndex.SuppressPoison: return 9;
                case MM3SpellIndex.Levitate:
                case MM3SpellIndex.SuppressDisease:
                case MM3SpellIndex.SuppressDiseaseDruid: return 10;
                case MM3SpellIndex.IdentifyMonsterDruid:
                case MM3SpellIndex.TurnUndead:
                case MM3SpellIndex.WizardEye: return 11;
                case MM3SpellIndex.IdentifyMonster:
                case MM3SpellIndex.NaturesCure:
                case MM3SpellIndex.Silence: return 12;
                case MM3SpellIndex.Blessed:
                case MM3SpellIndex.ImmobilizeDruid:
                case MM3SpellIndex.LightningBolt: return 13;
                case MM3SpellIndex.HolyBonus:
                case MM3SpellIndex.LloydsBeacon:
                case MM3SpellIndex.WalkOnWater: return 14;
                case MM3SpellIndex.FrostBite:
                case MM3SpellIndex.PowerCure:
                case MM3SpellIndex.PowerShield: return 15;
                case MM3SpellIndex.DetectMonster:
                case MM3SpellIndex.Heroism:
                case MM3SpellIndex.LightningBoltDruid: return 16;
                case MM3SpellIndex.AcidSprayDruid:
                case MM3SpellIndex.Fireball:
                case MM3SpellIndex.Immobilize: return 17;
                case MM3SpellIndex.ColdRay:
                case MM3SpellIndex.ColdRayDruid:
                case MM3SpellIndex.TimeDistortion: return 18;
                case MM3SpellIndex.CurePoison:
                case MM3SpellIndex.FeebleMind:
                case MM3SpellIndex.NaturesGate: return 19;
                case MM3SpellIndex.AcidSpray:
                case MM3SpellIndex.FireballDruid:
                case MM3SpellIndex.Teleport: return 20;
                case MM3SpellIndex.CureDisease:
                case MM3SpellIndex.DeadlySwarm:
                case MM3SpellIndex.FingerOfDeath: return 21;
                case MM3SpellIndex.CureParalysis:
                case MM3SpellIndex.CureParalysisDruid:
                case MM3SpellIndex.SuperShelter: return 22;
                case MM3SpellIndex.DragonBreath:
                case MM3SpellIndex.Paralyze:
                case MM3SpellIndex.ParalyzeDruid: return 23;
                case MM3SpellIndex.CreateFood:
                case MM3SpellIndex.CreateFoodDruid:
                case MM3SpellIndex.RechargeItem: return 24;
                case MM3SpellIndex.FantasticFreeze:
                case MM3SpellIndex.FieryFlail:
                case MM3SpellIndex.StoneToFleshDruid: return 25;
                case MM3SpellIndex.Duplication:
                case MM3SpellIndex.RaiseDeadDruid:
                case MM3SpellIndex.TownPortal: return 26;
                case MM3SpellIndex.Disintegration:
                case MM3SpellIndex.PrismaticLight:
                case MM3SpellIndex.StoneToFlesh: return 27;
                case MM3SpellIndex.ElementalStorm:
                case MM3SpellIndex.Etherealize:
                case MM3SpellIndex.HalfForMe: return 28;
                case MM3SpellIndex.DancingSword:
                case MM3SpellIndex.RaiseDead: return 29;
                case MM3SpellIndex.EnchantItem:
                case MM3SpellIndex.MoonRay: return 30;
                case MM3SpellIndex.Incinerate:
                case MM3SpellIndex.MassDistortion: return 31;
                case MM3SpellIndex.HolyWord:
                case MM3SpellIndex.MegaVolts: return 32;
                case MM3SpellIndex.Inferno:
                case MM3SpellIndex.Resurrection: return 33;
                case MM3SpellIndex.Implosion:
                case MM3SpellIndex.SunRay: return 34;
                case MM3SpellIndex.DivineIntervention:
                case MM3SpellIndex.StarBurst: return 35;
                default: return -1;
            }
        }

        public override bool IsKnown(int internalIndex, GenericClass mmClass) { return IsKnown((MM3InternalSpellIndex) internalIndex, mmClass); }
        public override bool IsKnown(int index, SpellType type) { return IsKnown((MM3SpellIndex) index, type); }

        public bool IsKnown(MM3InternalSpellIndex spell, GenericClass mmClass)
        {
            int iIndex = RawByteIndex(spell, Global.GetSpellType(mmClass));
            if (iIndex < 0 || iIndex >= RawBytes.Length)
                return false;

            return (RawBytes[iIndex] != 0);
        }

        public bool IsKnown(MM3SpellIndex index, SpellType type)
        {
            if (type != Type)
                return false;

            int iIndex = RawByteIndex(index);
            if (iIndex < 0 || iIndex >= RawBytes.Length)
                return false;

            return (RawBytes[iIndex] != 0);
        }

        public bool IsKnown(MM3Spell spell)
        {
            return IsKnown(spell.Index, spell.Type);
        }
    }
}
