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
    public partial class EOB123CharacterInfoControl : CharacterInfoControl
    {
        protected Control m_ctrlView = null;
        public override int CharacterSize { get { return Games.CharacterSize(Game); } }

        public EOB123CharacterInfoControl()
        {
            InitializeComponent();
        }

        public EOB123CharacterInfoControl(IMain main) : base(main)
        {
            InitializeComponent();
            byte[] bytesItemTable = null;
            if (main.Hacker is EOBMemoryHacker)
                bytesItemTable = ((EOBMemoryHacker)main.Hacker).GetItemTable();
            m_char = EOBCharacter.Create(main.Game, bytesItemTable);

            FindEditableAttributes();

            SetCommonControls();

            Global.MoveControls(labelFoodHeader.Location.X - m_commonCtrls.labelHPHeader.Location.X,
                m_commonCtrls.labelSPHeader.Location.Y - m_commonCtrls.labelHPHeader.Location.Y,
                m_commonCtrls.labelHPHeader,
                m_commonCtrls.labelSPHeader,
                m_commonCtrls.labelACHeader);
            Global.MoveControls(labelFood.Location.X - m_commonCtrls.labelHP.Location.X,
                m_commonCtrls.labelSP.Location.Y - m_commonCtrls.labelHP.Location.Y,
                m_commonCtrls.labelHP,
                m_commonCtrls.labelSP,
                m_commonCtrls.labelAC);
        }

        public GameNames Game { get { return m_main == null ? GameNames.None : m_main.Game; } }

        protected override void Stat_MouseLeave(object sender, EventArgs e) { base.Stat_MouseLeave(sender, e); }

        private void labelStrength_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Strength, labelStrength); }
        private void labelIntelligence_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Intelligence, labelIntelligence); }
        private void labelWisdom_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Wisdom, labelWisdom); }
        private void labelDexterity_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Dexterity, labelDexterity); }
        private void labelConstitution_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Constitution, labelConstitution); }
        private void labelCharisma_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Charisma, labelCharisma); }
        private void labelStrengthHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Strength, labelStrengthHeader); }
        private void labelIntelligenceHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Intelligence, labelIntelligenceHeader); }
        private void labelWisdomHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Wisdom, labelWisdomHeader); }
        private void labelDexterityHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Piety, labelDexterityHeader); }
        private void labelConstitutionHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Vitality, labelConstitutionHeader); }
        private void labelCharismaHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Charisma, labelCharismaHeader); }

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
                case TriggerEntity.MaxFood:
                case TriggerEntity.Food: return new TriggerControl(labelFood);
                case TriggerEntity.StatIndex:
                    switch (iIndex)
                    {
                        case 0: return new TriggerControl(labelStrength);
                        case 1: return new TriggerControl(labelIntelligence);
                        case 2: return new TriggerControl(labelWisdom);
                        case 3: return new TriggerControl(labelDexterity);
                        case 4: return new TriggerControl(labelConstitution);
                        case 5: return new TriggerControl(labelCharisma);
                        default: return base.GetTriggerControl(entity, strVal);
                    }
                case TriggerEntity.StatNamed:
                    if (strVal == null || strVal.Length < 1)
                        return base.GetTriggerControl(entity, strVal);
                    switch (Char.ToLower(strVal[0]))
                    {
                        case 's': return new TriggerControl(labelStrength);
                        case 'i': return new TriggerControl(labelIntelligence);
                        case 'w': return new TriggerControl(labelWisdom);
                        case 'd': return new TriggerControl(labelDexterity);
                        case 'c': return new TriggerControl(labelConstitution);
                        case 'l': return new TriggerControl(labelCharisma);
                        default: return base.GetTriggerControl(entity, strVal);
                    }
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

            if (info is EOBPartyInfo)
            {
                EOBPartyInfo eobInfo = info as EOBPartyInfo;

                m_bytes = new byte[info.CharacterSize];
                Buffer.BlockCopy(info.Bytes, iAddress * info.CharacterSize, m_bytes, 0, info.CharacterSize);
                m_char.SetFromBytes(iIndex, m_bytes, gameInfo, encounterInfo, eobInfo.ItemTable);
                m_iCharacterIndex = iIndex;
                m_iCharacterAddress = iAddress;
                m_iCharacterPosition = info.Positions[iIndex];
                ((EOBCharacter)m_char).Address = m_iCharacterAddress;
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

            m_commonCtrls.lvResistances.Columns[0].Text = "Save vs.";
        }

        public override void UpdateUI()
        {
            EOBCharacter eobChar = m_char as EOBCharacter;

            CheckMonitorBackpack();

            if (String.IsNullOrWhiteSpace(eobChar.Name))
                return;

            m_commonCtrls.labelLevel.Text = eobChar.BasicInfoString;
            m_commonCtrls.labelAC.Text = eobChar.ArmorClass.ToString();

            ListViewSelectionSaver savePack = new ListViewSelectionSaver(m_commonCtrls.lvBackpack);
            ListViewSelectionSaver saveEquip = new ListViewSelectionSaver(m_commonCtrls.lvEquipped);

            m_commonCtrls.lvEquipped.BeginUpdate();
            m_commonCtrls.lvBackpack.BeginUpdate();
            m_commonCtrls.lvEquipped.Items.Clear();
            m_commonCtrls.lvBackpack.Items.Clear();
            for (int i = 0; i < eobChar.Inventory.Items.Count; i++)
            {
                if (eobChar.Inventory.Items[i].IsEquipped)
                    SetEquippedLVI(eobChar.Inventory.Items[i], eobChar);
                else
                    SetBackpackLVI(eobChar.Inventory.Items[i], eobChar);
            }
            Global.FitSingleColumn(m_commonCtrls.lvBackpack);
            Global.FitSingleColumn(m_commonCtrls.lvEquipped);

            UpdateHeaders();

            savePack.Restore();
            saveEquip.Restore();

            m_commonCtrls.lvEquipped.EndUpdate();
            m_commonCtrls.lvBackpack.EndUpdate();
            labelFood.Text = String.Format("{0}/100", eobChar.Food);

            SetResistances(eobChar.GetResistances());
            m_commonCtrls.labelCondition.Text = EOBCharacter.ConditionString(eobChar.Condition, eobChar.GetExtraConditions(), true);
            m_tipCondition.SetToolTip(m_commonCtrls.labelCondition, EOBCharacter.ConditionDescription(eobChar.Condition, eobChar));
            m_tipCondition.ShowAlways = true;
            m_tipCondition.AutoPopDelay = 32000;

            m_commonCtrls.labelExp.Text = eobChar.ExperienceString;
            if (!m_char.IsCaster)
                labelKnownSpells.Text = "(not a spellcaster)";
            else
                labelKnownSpells.Text = String.Format("{0} scribed, {1} selected, {2} memorized", 
                    eobChar.Spells.NumKnown,
                    eobChar.Spells.NumSelected(SpellType.Any),
                    eobChar.Spells.NumMemorized(SpellType.Any));

            m_commonCtrls.labelHP.Text = eobChar.HitPoints.SlashString;
            m_commonCtrls.labelMelee.Text = eobChar.MeleeDamageString;
            labelRanged.Text = eobChar.RangedDamageString;
            labelTHAC0.Text = String.Format("{0}", eobChar.THAC0);

            m_commonCtrls.llCureAll.Visible = false;    // Not really an option for D&D-style games; healing is random and spells are limited

            labelStrength.Text = eobChar.Stats.StrengthString;
            labelIntelligence.Text = String.Format("{0}", eobChar.Stats.Intelligence);
            labelWisdom.Text = String.Format("{0}", eobChar.Stats.Wisdom);
            labelDexterity.Text = String.Format("{0}", eobChar.Stats.Dexterity);
            labelConstitution.Text = String.Format("{0}", eobChar.Stats.Constitution);
            labelCharisma.Text = String.Format("{0}", eobChar.Stats.Charisma);

            Global.SizeHeadersAndContent(m_commonCtrls.lvResistances);
            m_commonCtrls.lvResistances.EndUpdate();

            labelQuest.Text = eobChar.GetCurrentQuest(Hacker);
            m_commonCtrls.labelSP.Text = eobChar.GetAvailableSpellString();
        }

        protected override CheatMenuFlags PrepareCheatMenu(Control label, CheatMenuFlags flags = CheatMenuFlags.None)
        {
            if (!(label is EditableAttributeLabel || label is MMItemLabel || label is ListView))
                return CheatMenuFlags.None;

            CheatMenuFlags menuFlags = CheatMenuFlags.AllNonlevel;
            EOBCharacter eobChar = m_char as EOBCharacter;
            if (eobChar == null)
                return CheatMenuFlags.None;

            m_cheatOffsets = null;

            m_cheatType = AttributeType.TwoUInt8;
            if (label == labelIntelligence)
                m_cheatOffsets = new CheatOffsets(new int[] { eobChar.Offsets.Stats + eobChar.Stats.IntelligenceOffsetPerm, eobChar.Offsets.Stats + eobChar.Stats.IntelligenceOffsetTemp });
            else if (label == labelWisdom)
                m_cheatOffsets = new CheatOffsets(new int[] { eobChar.Offsets.Stats + eobChar.Stats.WisdomOffsetPerm, eobChar.Offsets.Stats + eobChar.Stats.WisdomOffsetTemp });
            else if (label == labelDexterity)
                m_cheatOffsets = new CheatOffsets(new int[] { eobChar.Offsets.Stats + eobChar.Stats.DexterityOffsetPerm, eobChar.Offsets.Stats + eobChar.Stats.DexterityOffsetTemp });
            else if (label == labelConstitution)
                m_cheatOffsets = new CheatOffsets(new int[] { eobChar.Offsets.Stats + eobChar.Stats.ConstitutionOffsetPerm, eobChar.Offsets.Stats + eobChar.Stats.ConstitutionOffsetTemp });
            else if (label == labelCharisma)
                m_cheatOffsets = new CheatOffsets(new int[] { eobChar.Offsets.Stats + eobChar.Stats.CharismaOffsetPerm, eobChar.Offsets.Stats + eobChar.Stats.CharismaOffsetTemp });
            if (label == labelStrength)
            {
                m_cheatType = AttributeType.Strength18;
                m_cheatOffsets = new CheatOffsets(new int[] {
                    eobChar.Offsets.Stats + eobChar.Stats.StrengthOffsetTemp, eobChar.Offsets.Stats + eobChar.Stats.StrengthOffsetPerm,
                    eobChar.Offsets.Stats + eobChar.Stats.Strength18OffsetTemp, eobChar.Offsets.Stats + eobChar.Stats.Strength18OffsetPerm,
                });
            }
            else if (label == labelFood)
            {
                m_cheatType = AttributeType.UInt8;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Food });
                m_cheatOffsets.Tag = new CheatTag(100, 0);
            }
            else if (label == m_commonCtrls.labelExp)
            {
                menuFlags |= CheatMenuFlags.NextLevel;
                m_cheatType = AttributeType.ThreeInt32;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Experience, m_char.Offsets.Experience + 4, m_char.Offsets.Experience + 8 });
            }
            else if (label == m_commonCtrls.labelHP)
            {
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.CurrentHP, m_char.Offsets.MaxHP });
                if (eobChar is EOB1Character)
                {
                    m_cheatType = AttributeType.TwoInt8;
                    m_cheatOffsets.Tag = new CheatTag(sbyte.MaxValue, sbyte.MinValue);
                }
                else
                {
                    m_cheatType = AttributeType.TwoInt16;
                    m_cheatOffsets.Tag = new CheatTag(632, -632);
                }
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
                    m_char.Offsets.Level,
                    -1, // levelmod
                    m_char.Offsets.Name,
                    -1, // sex
                    -1, // alignmentmod
                    m_char.Offsets.Alignment,
                    m_char.Offsets.Race,
                    m_char.Offsets.Class,
                    -1, -1, -1, -1, -1, -1
                });
            }
            else if (label == labelKnownSpells)
            {
                m_cheatType = AttributeType.KnownSpells;
                menuFlags = CheatMenuFlags.Edit;
                m_cheatOffsets = Global.IntRange(m_char.Offsets.Spells, m_char.Offsets.SpellsLength);
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
                m_cheatOffsets = ((EOBCharacter)m_char).GetInventoryCheatOffsets(iIndex);
            }

            if (m_cheatOffsets == null)
                return CheatMenuFlags.None;

            if (m_cheatOffsets.Length < 1 || m_cheatOffsets[0] < 0)        // Offset refers to an item that doesn't exist in this game's character record
                return CheatMenuFlags.None;

            return menuFlags;
        }

        private void labelRangedHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.RangedDamage, labelRangedHeader); }
        private void labelRanged_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.RangedDamage, labelRanged); }
        private void labelTHAC0Header_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.MeleeToHit, labelTHAC0Header); }
        private void labelTHAC0_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.MeleeToHit, labelTHAC0); }
        private void labelKnownSpells_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Spellbook, labelKnownSpells); }
        private void labelSpellbookHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Spellbook, labelSpellbookHeader); }

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
                EditDnDSpellsForm form = new EditDnDSpellsForm();
                form.ReadOnly = true;
                form.SetSpells((m_char as EOBCharacter).Spells, m_char);
                form.ShowDialog();
            }
        }
    }
}
