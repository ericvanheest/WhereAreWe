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
    public partial class BT123CharacterInfoControl : CharacterInfoControl
    {
        protected ToolTip m_tipWon = new ToolTip();
        protected ToolTip m_tipNumAttacks = new ToolTip();
        protected ToolTip m_tipHide = new ToolTip();
        protected ToolTip m_tipIdentify = new ToolTip();
        protected ToolTip m_tipDisarm = new ToolTip();
        protected ToolTip m_tipCritical = new ToolTip();
        protected Control m_ctrlView = null;
        public override int CharacterSize { get { return Games.CharacterSize(Game); } }

        public BT123CharacterInfoControl()
        {
            InitializeComponent();
        }

        public BT123CharacterInfoControl(IMain main) : base(main)
        {
            InitializeComponent();
            m_char = BTCharacter.Create(main.Game);

            FindEditableAttributes();

            SetCommonControls();
        }

        public GameNames Game { get { return m_main == null ? GameNames.None : m_main.Game; } }

        protected override void Stat_MouseLeave(object sender, EventArgs e) { base.Stat_MouseLeave(sender, e); }

        private void labelStrength_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Strength, labelStrength); }
        private void labelIQ_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.IQ, labelIQ); }
        private void labelDexterity_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Dexterity, labelDexterity); }
        private void labelConstitution_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Constitution, labelConstitution); }
        private void labelLuck_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Luck, labelLuck); }
        private void labelStrengthHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Strength, labelStrengthHeader); }
        private void labelIQHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.IQ, labelIQHeader); }
        private void labelDexterityHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Piety, labelDexterityHeader); }
        private void labelConstitutionHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Vitality, labelConstitutionHeader); }
        private void labelLuckHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Luck, labelLuckHeader); }

        private void SetTipLong(ToolTip tip, Control ctrl, int iLabelValue, string strTipText)
        {
            SetTipLong(tip, ctrl, String.Format("{0}", iLabelValue), strTipText);
        }

        private void SetTipLong(ToolTip tip, Control ctrl, string strLabelText, string strTipText)
        {
            ctrl.Text = strLabelText;
            tip.SetToolTip(ctrl, strTipText);
            tip.ShowAlways = true;
            tip.AutoPopDelay = 32000;
        }

        public override TriggerControl GetTriggerControl(TriggerEntity entity, string strVal)
        {
            int iIndex = 0;
            Int32.TryParse(strVal, out iIndex);
            switch (entity)
            {
                case TriggerEntity.StatIndex:
                    switch (iIndex)
                    {
                        case 0: return new TriggerControl(labelStrength);
                        case 1: return new TriggerControl(labelIQ);
                        case 2: return new TriggerControl(labelDexterity);
                        case 3: return new TriggerControl(labelConstitution);
                        case 4: return new TriggerControl(labelLuck);
                        default: return base.GetTriggerControl(entity, strVal);
                    }
                case TriggerEntity.StatNamed:
                    if (strVal == null || strVal.Length < 1)
                        return base.GetTriggerControl(entity, strVal);
                    switch (Char.ToLower(strVal[0]))
                    {
                        case 's': return new TriggerControl(labelStrength);
                        case 'i': return new TriggerControl(labelIQ);
                        case 'd': return new TriggerControl(labelDexterity);
                        case 'c': return new TriggerControl(labelConstitution);
                        case 'l': return new TriggerControl(labelLuck);
                        default: return base.GetTriggerControl(entity, strVal);
                    }
                case TriggerEntity.IDItem: return new TriggerControl(labelIdentify);
                case TriggerEntity.Disarm: return new TriggerControl(labelDisarm);
                case TriggerEntity.Critical: return new TriggerControl(labelCritical);
                case TriggerEntity.Hide: return new TriggerControl(labelHide);
                case TriggerEntity.Attacks: return new TriggerControl(labelNumAttacks);
                case TriggerEntity.Won: return new TriggerControl(labelWon);
                case TriggerEntity.Songs: return new TriggerControl(labelSongs);
                case TriggerEntity.RangedDamageAverage: return new TriggerControl(labelRanged);
                default: return base.GetTriggerControl(entity, strVal);
            }
        }

        public override void SetInfo(PartyInfo info, int iIndex, GameInfo gameInfo, EncounterInfo encounterInfo = null)
        {
            if (info == null || iIndex >= 7 || m_char == null)
                return;

            int iAddress = Math.Max(0, info.Addresses[iIndex]);
            if (info != null && info.Bytes.Length < (iAddress + 1) * CharacterSize)
                return;

            if (info is BTPartyInfo)
            {
                BTPartyInfo btInfo = info as BTPartyInfo;

                m_bytes = new byte[info.CharacterSize + (Game == GameNames.BardsTale3 ? 0 : 15)];       // BT3 has the character name in the bytes; others don't
                Buffer.BlockCopy(info.Bytes, iAddress * info.CharacterSize, m_bytes, 0, info.CharacterSize);
                m_char.SetFromBytes(iIndex, m_bytes, gameInfo, encounterInfo);
                m_iCharacterIndex = iIndex;
                m_iCharacterAddress = iAddress;
                m_iCharacterPosition = info.Positions[iIndex];
                ((BTCharacter)m_char).Address = m_iCharacterAddress;
            }
            else
            {
                m_iCharacterAddress = -1;
                m_iCharacterIndex = -1;
                m_iCharacterPosition = -1;
            }

            UpdateUI();
        }

        protected override void SetCommonControls()
        {
            if (m_commonCtrls == null)
                base.SetCommonControls();

            m_commonCtrls.lvResistances.Visible = false;
        }

        public override void UpdateUI()
        {
            BTCharacter btChar = m_char as BTCharacter;
            bool bt3 = Game == GameNames.BardsTale3;

            CheckMonitorBackpack();

            if (String.IsNullOrWhiteSpace(btChar.Name))
                return;

            m_commonCtrls.labelLevel.Text = btChar.BasicInfoString;
            m_commonCtrls.labelAC.Text = btChar.ArmorClass.ToString();

            ListViewSelectionSaver savePack = new ListViewSelectionSaver(m_commonCtrls.lvBackpack);
            ListViewSelectionSaver saveEquip = new ListViewSelectionSaver(m_commonCtrls.lvEquipped);

            m_commonCtrls.lvEquipped.BeginUpdate();
            m_commonCtrls.lvBackpack.BeginUpdate();
            m_commonCtrls.lvEquipped.Items.Clear();
            m_commonCtrls.lvBackpack.Items.Clear();
            for (int i = 0; i < btChar.Inventory.Items.Count; i++)
            {
                if (btChar.Inventory.Items[i].IsEquipped)
                    SetEquippedLVI(btChar.Inventory.Items[i], btChar);
                else
                    SetBackpackLVI(btChar.Inventory.Items[i], btChar);
            }
            Global.FitSingleColumn(m_commonCtrls.lvBackpack);
            Global.FitSingleColumn(m_commonCtrls.lvEquipped);

            UpdateHeaders();

            savePack.Restore();
            saveEquip.Restore();

            m_commonCtrls.lvEquipped.EndUpdate();
            m_commonCtrls.lvBackpack.EndUpdate();

            SetResistances(btChar.GetResistances());
            m_commonCtrls.labelCondition.Text = BTCharacter.ConditionString(btChar.Condition, btChar.GetExtraConditions(), true);
            m_tipCondition.SetToolTip(m_commonCtrls.labelCondition, BTCharacter.ConditionDescription(btChar.Condition, btChar));
            m_tipCondition.ShowAlways = true;
            m_tipCondition.AutoPopDelay = 32000;

            m_commonCtrls.labelExp.Text = btChar.ExperienceString;
            if (!m_char.IsCaster && m_char.BasicClass != GenericClass.Bard)
                labelKnownSpells.Text = "(not a spellcaster)";
            else if (btChar.Spells == null)
                labelKnownSpells.Text = btChar.SpellLevel == null ? "<null>" : btChar.SpellLevel.ToFullString();
            else
                labelKnownSpells.Text = btChar.Spells.KnownString(btChar.BasicClass);
            labelGold.Text = String.Format("{0}", btChar.Gold);
            labelSongs.Text = String.Format("{0}", btChar.Songs);

            labelDisarm.Text = String.Format("{0}", btChar.Disarm);
            labelIdentify.Text = String.Format("{0}", btChar.Identify);

            SetTipLong(m_tipHide, labelHide, btChar.HideChance, "The chance for a Rogue to hide in the shadows (max 255)\r\n(In-game this value is shown as a percentage)");
            SetTipLong(m_tipIdentify, labelIdentify, btChar.Identify, "The chance for a Rogue to identify traps or items (max 255)\r\n(In-game this value is shown as a percentage)");
            SetTipLong(m_tipDisarm, labelDisarm, btChar.Disarm, "The chance for a Rogue to disarm traps (max 255)\r\n(In-game this value is shown as a percentage)");
            SetTipLong(m_tipCritical, labelCritical, btChar.CriticalChance, "The chance for a Ranger or Rogue to critically hit (max 255)\r\n(In-game this value is shown as a percentage)");
            SetTipLong(m_tipNumAttacks, labelNumAttacks, btChar.NumAttacks, "The number of extra melee attacks per round");
            SetTipLong(m_tipWon, labelWon, btChar.BattlesWon, "The number of battles won also affects your character's attack priority in combat");

            m_commonCtrls.labelHP.Text = btChar.HitPoints.SlashString;
            m_commonCtrls.labelSP.Text = btChar.SpellPoints.SlashString;
            m_commonCtrls.labelMelee.Text = btChar.MeleeDamageString;
            labelRanged.Text = btChar.RangedDamageString;

            if (bt3)
            {
                labelRanged.Visible = false;
                labelRangedHeader.Visible = false;
            }

            m_commonCtrls.llCureAll.Visible = false;    // Not really an option for Bard's Tale; healing is random and conditions are limited

            labelStrength.Text = String.Format("{0}", btChar.Stats.Strength);
            labelIQ.Text = String.Format("{0}", btChar.Stats.IQ);
            labelDexterity.Text = String.Format("{0}", btChar.Stats.Dexterity);
            labelConstitution.Text = String.Format("{0}", btChar.Stats.Constitution);
            labelLuck.Text = String.Format("{0}", btChar.Stats.Luck);

            Global.SizeHeadersAndContent(m_commonCtrls.lvResistances);
            m_commonCtrls.lvResistances.EndUpdate();

            bool bRogue = m_char.BasicClass == GenericClass.Rogue;
            labelDisarm.Visible = bRogue && bt3;
            labelDisarmHeader.Visible = bRogue && bt3;
            labelIdentify.Visible = bRogue && bt3;
            labelIdentifyHeader.Visible = bRogue && bt3;
            labelHide.Visible = bRogue;
            labelHideHeader.Visible = bRogue;
            labelSongs.Visible = m_char.BasicClass == GenericClass.Bard;
            labelSongsHeader.Visible = m_char.BasicClass == GenericClass.Bard;
            labelCritical.Visible = bRogue || m_char.BasicClass == GenericClass.Hunter;
            labelCriticalHeader.Visible = bRogue || m_char.BasicClass == GenericClass.Hunter;
            labelWon.Visible = Game != GameNames.BardsTale3;
            labelWonHeader.Visible = Game != GameNames.BardsTale3;
            llAwards.Visible = Game == GameNames.BardsTale3;
            labelQuest.Text = btChar.GetCurrentQuest(Hacker);
        }

        protected override CheatMenuFlags PrepareCheatMenu(Control label, CheatMenuFlags flags = CheatMenuFlags.None)
        {
            if (!(label is EditableAttributeLabel || label is MMItemLabel || label is ListView))
                return CheatMenuFlags.None;

            CheatMenuFlags menuFlags = CheatMenuFlags.AllNonlevel;
            BTCharacter btChar = m_char as BTCharacter;
            if (btChar == null)
                return CheatMenuFlags.None;

            m_cheatOffsets = null;

            if (Game == GameNames.BardsTale3)
            {
                m_cheatType = AttributeType.UInt8;
                if (label == labelStrength)
                    m_cheatOffsets = new CheatOffsets(new int[] { btChar.Offsets.Stats + btChar.Stats.StrengthOffsetPerm });
                else if (label == labelIQ)
                    m_cheatOffsets = new CheatOffsets(new int[] { btChar.Offsets.Stats + btChar.Stats.IQOffsetPerm });
                else if (label == labelDexterity)
                    m_cheatOffsets = new CheatOffsets(new int[] { btChar.Offsets.Stats + btChar.Stats.DexOffsetPerm });
                else if (label == labelConstitution)
                    m_cheatOffsets = new CheatOffsets(new int[] { btChar.Offsets.Stats + btChar.Stats.ConOffsetPerm });
                else if (label == labelLuck)
                    m_cheatOffsets = new CheatOffsets(new int[] { btChar.Offsets.Stats + btChar.Stats.LuckOffsetPerm });
            }
            else
            {
                if (btChar.Stats.StatSize == 1)
                    m_cheatType = AttributeType.TwoUInt8;
                else
                    m_cheatType = AttributeType.TwoInt16;

                if (label == labelStrength)
                    m_cheatOffsets = new CheatOffsets(new int[] { btChar.Offsets.Stats + btChar.Stats.StrengthOffsetPerm, btChar.Offsets.Stats + btChar.Stats.StrengthOffsetTemp });
                else if (label == labelIQ)
                    m_cheatOffsets = new CheatOffsets(new int[] { btChar.Offsets.Stats + btChar.Stats.IQOffsetPerm, btChar.Offsets.Stats + btChar.Stats.IQOffsetTemp });
                else if (label == labelDexterity)
                    m_cheatOffsets = new CheatOffsets(new int[] { btChar.Offsets.Stats + btChar.Stats.DexOffsetPerm, btChar.Offsets.Stats + btChar.Stats.DexOffsetTemp });
                else if (label == labelConstitution)
                    m_cheatOffsets = new CheatOffsets(new int[] { btChar.Offsets.Stats + btChar.Stats.ConOffsetPerm, btChar.Offsets.Stats + btChar.Stats.ConOffsetTemp });
                else if (label == labelLuck)
                    m_cheatOffsets = new CheatOffsets(new int[] { btChar.Offsets.Stats + btChar.Stats.LuckOffsetPerm, btChar.Offsets.Stats + btChar.Stats.LuckOffsetTemp });
            }
            if (label == labelGold)
            {
                m_cheatType = AttributeType.Int32;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Gold });
            }
            else if (label == labelSongs)
            {
                m_cheatType = AttributeType.UInt8;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.BardSongs });
            }
            else if (label == labelWon)
            {
                m_cheatType = AttributeType.Int16;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.BattlesWon });
            }
            else if (label == labelCritical)
            {
                m_cheatType = AttributeType.UInt8;
                if (m_char.BasicClass == GenericClass.Rogue)
                    m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.HideChance });
                else
                    m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Critical });
            }
            else if (label == labelIdentify)
            {
                m_cheatType = AttributeType.UInt8;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Identify });
            }
            else if (label == labelDisarm)
            {
                m_cheatType = AttributeType.UInt8;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Thievery });
            }
            else if (label == labelHide)
            {
                m_cheatType = AttributeType.UInt8;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.HideChance });
            }
            else if (label == labelNumAttacks)
            {
                m_cheatType = Game == GameNames.BardsTale3 ? AttributeType.UInt8 : AttributeType.Int16;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Swings });
            }
            else if (label == m_commonCtrls.labelExp)
            {
                menuFlags |= CheatMenuFlags.NextLevel;
                m_cheatType = AttributeType.UInt32;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Experience });
            }
            else if (label == m_commonCtrls.labelHP)
            {
                m_cheatType = AttributeType.TwoInt16;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.CurrentHP, m_char.Offsets.MaxHP });
            }
            else if (label == m_commonCtrls.labelCondition)
            {
                m_cheatType = AttributeType.Condition;
                menuFlags = CheatMenuFlags.Edit;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Condition }, m_char.Offsets.ConditionLength);
            }
            else if (label == m_commonCtrls.labelLevel)
            {
                m_cheatType = AttributeType.LevSexAlignRaceClass;
                menuFlags |= CheatMenuFlags.SuperChar;
                m_cheatOffsets = new CheatOffsets(new int[] {
                    m_char.Offsets.Level, m_char.Offsets.LevelMod,
                    m_char is BT3Character ? m_char.Offsets.Name : -1,
                    m_char is BT3Character ? m_char.Offsets.Sex : -1,
                    -1, -1,
                    m_char.Offsets.Race,
                    m_char.Offsets.Class, -1, -1, -1, -1, -1, -1
                });
            }
            else if (label == labelKnownSpells)
            {
                m_cheatType = AttributeType.KnownSpells;
                menuFlags = CheatMenuFlags.Edit;
                if (btChar is BT3Character)
                    m_cheatOffsets = Global.IntRange(m_char.Offsets.Spells, m_char.Offsets.SpellsLength);
                else
                    m_cheatOffsets = Global.IntRange(m_char.Offsets.SpellLevel, m_char.Offsets.SpellLevelLength);
            }
            else if (label == m_commonCtrls.labelSP)
            {
                m_cheatType = AttributeType.TwoInt16;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.CurrentSP, m_char.Offsets.MaxSP });
            }
            else if (label == m_commonCtrls.lvBackpack || label == m_commonCtrls.lvEquipped)
            {
                m_cheatType = AttributeType.Item;
                menuFlags = CheatMenuFlags.Edit;

                int iIndex = -1;
                if (label == m_commonCtrls.lvBackpack)
                {
                    if (flags == CheatMenuFlags.AddNew)
                        iIndex = m_char.FirstEmptyBackpackIndex;
                    else if (m_commonCtrls.lvBackpack.FocusedItem != null)
                    {
                        iIndex = m_commonCtrls.lvBackpack.FocusedItem.Index;
                        InventoryItemTag tag = m_commonCtrls.lvBackpack.FocusedItem.Tag as InventoryItemTag;
                        if (tag != null)
                            iIndex = tag.MemoryIndex;
                        else
                            iIndex = m_char.FirstEmptyBackpackIndex;
                    }
                }
                else if (label == m_commonCtrls.lvEquipped && m_commonCtrls.lvEquipped.FocusedItem != null)
                {
                    iIndex = m_commonCtrls.lvEquipped.FocusedItem.Index;
                    InventoryItemTag tag = m_commonCtrls.lvEquipped.FocusedItem.Tag as InventoryItemTag;
                    if (tag != null)
                        iIndex = tag.MemoryIndex;
                }
                m_cheatOffsets = ((BTCharacter) m_char).GetInventoryCheatOffsets(iIndex);
            }

            if (m_cheatOffsets == null)
                return CheatMenuFlags.None;

            if (m_cheatOffsets.Length < 1 || m_cheatOffsets[0] < 0)        // Offset refers to an item that doesn't exist in this game's character record
                return CheatMenuFlags.None;

            return menuFlags;
        }

        private void llAwards_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BT3Character bt3Char = m_char as BT3Character;
            if (m_char == null)
                return;

            EditBitsForm form = new EditBitsForm(BT3Bits.AwardsDescription, this);
            form.Bytes = bt3Char.Awards;
            form.ReadOnly = !Global.Cheats;
            if (form.ShowDialog() == DialogResult.OK)
            {
                (Hacker as BT3MemoryHacker).SetAwards(bt3Char.Address, form.Bytes);
            }
        }

        private void labelRangedHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.RangedDamage, labelRangedHeader); }
        private void labelRanged_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.RangedDamage, labelRanged); 

        }
    }
}
