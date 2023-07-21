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
    public partial class Wiz123CharacterInfoControl : CharacterInfoControl
    {
        protected Control m_ctrlView = null;
        protected List<Item> m_lastBackpack = null;

        public Wiz123CharacterInfoControl()
        {
            InitializeComponent();
        }

        public Wiz123CharacterInfoControl(IMain main) : base(main)
        {
            InitializeComponent();
            m_char = new WizCharacter();

            FindEditableAttributes();

            SetCommonControls();
        }

        protected override void Stat_MouseLeave(object sender, EventArgs e) { base.Stat_MouseLeave(sender, e); }

        private void labelStrength_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Strength, labelStrength); }
        private void labelIQ_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.IQ, labelIQ); }
        private void labelPiety_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Piety, labelPiety); }
        private void labelVitality_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Vitality, labelVitality); }
        private void labelAgility_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Agility, labelAgility); }
        private void labelLuck_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Luck, labelLuck); }
        private void labelStrengthHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Strength, labelStrengthHeader); }
        private void labelIQHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.IQ, labelIQHeader); }
        private void labelPietyHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Piety, labelPietyHeader); }
        private void labelVitalityHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Vitality, labelVitalityHeader); }
        private void labelAgilityHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Agility, labelAgilityHeader); }
        private void labelLuckHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Luck, labelLuckHeader); }
        private void labelPoison_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.PoisonCount, labelPoison); }
        private void labelPoisonHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.PoisonCount, labelPoisonHeader); }
        private void labelRegen_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Regen, labelRegen); }
        private void labelRegenHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Regen, labelRegenHeader); }
        private void labelIDTrapsHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.IdentifyTrap, labelIDTrapsHeader); }
        private void labelIDTraps_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.IdentifyTrap, labelIDTraps); }
        private void labelDisarmHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Thievery, labelDisarmHeader); }
        private void labelDisarm_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Thievery, labelDisarm); }
        private void labelCriticalHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Critical, labelCriticalHeader); }
        private void labelCritical_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Critical, labelCritical); }
        private void labelIDItemHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.IdentifyItem, labelIdentifyItemHeader); }
        private void labelIDItem_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.IdentifyItem, labelIdentifyItem); }
        private void labelDispelHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Dispel, labelDispelHeader); }
        private void labelDispel_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Dispel, labelDispel); }

        private PackedStat GetStatForLabel(Control label)
        {
            if (label == labelStrength)
                return PackedStat.Strength;
            if (label == labelIQ)
                return PackedStat.IQ;
            if (label == labelPiety)
                return PackedStat.Piety;
            if (label == labelVitality)
                return PackedStat.Vitality;
            if (label == labelAgility)
                return PackedStat.Agility;
            if (label == labelLuck)
                return PackedStat.Luck;
            return PackedStat.None;
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
                        case 2: return new TriggerControl(labelPiety);
                        case 3: return new TriggerControl(labelVitality);
                        case 4: return new TriggerControl(labelAgility);
                        case 5: return new TriggerControl(labelLuck);
                        default: return base.GetTriggerControl(entity, strVal);
                    }
                case TriggerEntity.StatNamed:
                    if (strVal == null || strVal.Length < 1)
                        return base.GetTriggerControl(entity, strVal);
                    switch (Char.ToLower(strVal[0]))
                    {
                        case 's': return new TriggerControl(labelStrength);
                        case 'i': return new TriggerControl(labelIQ);
                        case 'p': return new TriggerControl(labelPiety);
                        case 'v': return new TriggerControl(labelVitality);
                        case 'a': return new TriggerControl(labelAgility);
                        case 'l': return new TriggerControl(labelLuck);
                        default: return base.GetTriggerControl(entity, strVal);
                    }
                case TriggerEntity.Disarm:
                case TriggerEntity.Thievery: return new TriggerControl(labelDisarm);
                case TriggerEntity.Regen: return new TriggerControl(labelRegen);
                case TriggerEntity.Poison: return new TriggerControl(labelPoison);
                case TriggerEntity.IDTrap: return new TriggerControl(labelIDTraps);
                case TriggerEntity.Dispel: return new TriggerControl(labelDispel);
                case TriggerEntity.Critical: return new TriggerControl(labelCritical);
                default: return base.GetTriggerControl(entity, strVal);
            }
        }

        protected override CheatMenuFlags PrepareCheatMenu(Control label, CheatMenuFlags flags = CheatMenuFlags.None)
        {
            if (!(label is EditableAttributeLabel || label is MMItemLabel || label is ListView))
                return CheatMenuFlags.None;

            CheatMenuFlags menuFlags = CheatMenuFlags.AllNonlevel;

            m_cheatOffsets = null;

            m_cheatType = AttributeType.StatMax18;

            PackedStat stat = GetStatForLabel(label);
            if (stat != PackedStat.None)
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Stats }, new WizardryCheatTag(stat, 18));
            else if (label == labelGold)
            {
                m_cheatType = AttributeType.SixByteLong;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Gold }, new WizardryCheatTag(PackedStat.Gold, 999999999999));
            }
            else if (label == m_commonCtrls.labelExp)
            {
                menuFlags |= CheatMenuFlags.NextLevel;
                m_cheatType = AttributeType.SixByteLong;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Experience }, new WizardryCheatTag(PackedStat.Experience, 999999999999));
            }
            else if (label == m_commonCtrls.labelHP)
            {
                m_cheatType = AttributeType.TwoInt16;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.CurrentHP, m_char.Offsets.MaxHP});
            }
            else if (label == m_commonCtrls.labelCondition)
            {
                m_cheatType = AttributeType.WizCondition;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Condition }, new WizardryCheatTag(PackedStat.Condition, 7));
            }
            else if (label == m_commonCtrls.labelLevel)
            {
                m_cheatType = AttributeType.LevSexAlignRaceClass;
                menuFlags |= CheatMenuFlags.SuperChar;
                m_cheatOffsets = new CheatOffsets(new int[] {
                    m_char.Offsets.Level, m_char.Offsets.LevelMod, 0,
                    m_char.Offsets.Sex,
                    m_char.Offsets.AlignmentMod, m_char.Offsets.Alignment,
                    m_char.Offsets.Race,
                    m_char.Offsets.Class,
                    m_char.Offsets.BirthYear,
                    m_char.Offsets.BirthDay,
                    m_char.Offsets.AgeModifier,
                    m_char.Offsets.Age,
                    m_char.Offsets.AgeDays,
                    m_char.Offsets.SpellCaster
                }, new WizardryCheatTag(PackedStat.None, 999, 1));
            }
            else if (label == labelKnownSpells)
            {
                m_cheatType = AttributeType.KnownSpells;
                menuFlags = CheatMenuFlags.Edit;
                m_cheatOffsets = Global.IntRange(m_char.Offsets.Spells, m_char.Offsets.SpellsLength);
            }
            else if (label == m_commonCtrls.labelSP)
            {
                menuFlags = CheatMenuFlags.Edit;
                if (m_char is Wiz5Character)
                {
                    m_cheatType = AttributeType.Wiz5SpellPoints;
                    m_cheatOffsets = Global.IntRange(m_char.Offsets.CurrentSP, 16, 1);
                }
                else
                {
                    m_cheatType = AttributeType.WizSpellPoints;
                    m_cheatOffsets = Global.IntRange(m_char.Offsets.CurrentSP, 28, 2);
                }
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
                if (iIndex >= 0 && iIndex < 8)
                {
                    if (this is Wiz5CharacterInfoControl)
                    {
                        m_cheatOffsets = Global.IntRange(m_char.Offsets.Inventory + 1 + (4 * iIndex), 5);
                        m_cheatOffsets[0] = m_char.Offsets.Inventory;   // May need to change the number of items
                    }
                    else
                    {
                        m_cheatOffsets = Global.IntRange(m_char.Offsets.Inventory + 1 + (8 * iIndex), 9);
                        m_cheatOffsets[0] = m_char.Offsets.Inventory;   // May need to change the number of items
                    }
                }
                else if (iIndex > 7 && iIndex < 27)
                {
                    // This items aren't stored in the character record; they're elsewhere in memory
                    m_cheatOffsets = new CheatOffsets(new int[] { iIndex - 8 }, new WizardryCheatTag(PackedStat.BlackBox));
                }
            }
            else if (label == m_commonCtrls.lvResistances && m_commonCtrls.lvResistances.FocusedItem != null)
            {
                ResistanceValue rv = m_commonCtrls.lvResistances.FocusedItem.Tag as ResistanceValue;
                m_cheatType = AttributeType.StatMax31;
                menuFlags = CheatMenuFlags.AllNonlevel;

                switch (rv.Resistance)
                {
                    case GenericResistanceFlags.SaveVsPoison:
                        m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.SavingThrows }, new WizardryCheatTag(PackedStat.SaveVsPoison, 31));
                        break;
                    case GenericResistanceFlags.SaveVsPetrify:
                        m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.SavingThrows }, new WizardryCheatTag(PackedStat.SaveVsPetrify, 31));
                        break;
                    case GenericResistanceFlags.SaveVsWand:
                        m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.SavingThrows }, new WizardryCheatTag(PackedStat.SaveVsWand, 31));
                        break;
                    case GenericResistanceFlags.SaveVsBreath:
                        m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.SavingThrows }, new WizardryCheatTag(PackedStat.SaveVsBreath, 31));
                        break;
                    case GenericResistanceFlags.SaveVsSpell:
                        m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.SavingThrows }, new WizardryCheatTag(PackedStat.SaveVsSpell, 31));
                        break;
                    default:
                        m_cheatOffsets = null;
                        break;
                }
            }
            else
            {
                m_cheatType = AttributeType.Int16;
                if (label == m_commonCtrls.labelAC)
                    m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.ArmorClass });
                else if (label == labelRegen)
                    m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Regeneration });
                else if (label == labelPoison)
                    m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.LocationX });
            }

            if (m_cheatOffsets == null)
                return CheatMenuFlags.None;

            if (m_cheatOffsets.Length < 1 || m_cheatOffsets[0] < 0)        // Offset refers to an item that doesn't exist in this game's character record
                return CheatMenuFlags.None;

            return menuFlags;
        }

        public override void SetInfo(PartyInfo info, int iIndex, GameInfo gameInfo, EncounterInfo encounterInfo = null)
        {
            if (info != null && info.Bytes.Length < (info.Addresses[iIndex] + 1) * CharacterSize)
                return;

            if (info is WizPartyInfo)
            {
                WizPartyInfo wiz1Info = info as WizPartyInfo;

                m_bytes = new byte[info.CharacterSize];
                Buffer.BlockCopy(info.Bytes, info.Addresses[iIndex] * info.CharacterSize, m_bytes, 0, info.CharacterSize);
                if (gameInfo != null && gameInfo.Game == GameNames.Wizardry4 && !(m_char is Wiz4Character))
                    m_char = Wiz4Character.Create(iIndex, m_bytes, 0, gameInfo as Wiz4GameInfo, encounterInfo as WizEncounterInfo, wiz1Info.BlackBox);
                else if (m_char is Wiz4Character)
                    ((Wiz4Character) m_char).SetFromBytes(iIndex, m_bytes, 0, gameInfo as Wiz4GameInfo, encounterInfo as WizEncounterInfo, wiz1Info.BlackBox);
                else
                    m_char.SetFromBytes(iIndex, m_bytes, gameInfo, encounterInfo);
                m_iCharacterIndex = iIndex;
                m_iCharacterAddress = info.Addresses[iIndex];
                m_iCharacterPosition = info.Positions[iIndex];
                ((WizCharacter)m_char).Address = m_iCharacterAddress;
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

            m_commonCtrls.labelPoison = labelPoison;
            m_commonCtrls.labelPoisonHeader = labelPoisonHeader;
        }

        public override void UpdateUI()
        {
            WizCharacter wizChar = m_char as WizCharacter;
            bool bWiz5 = (m_char is Wiz5Character);

            if (m_bDebugMonitorBackpack)
            {
                if (m_lastBackpack == null)
                    m_lastBackpack = new List<Item>();
                for (int i = 0; i < Math.Max(m_lastBackpack.Count, wizChar.Inventory.Items.Count); i++)
                {
                    if (i >= m_lastBackpack.Count)
                        Global.Log("Char{0} +Item: {1}", wizChar.Address, wizChar.Inventory.Items[i].DebugString);
                    else if (i >= wizChar.Inventory.Items.Count)
                        Global.Log("Char{0} -Item: {1}", wizChar.Address, m_lastBackpack[i].DebugString);
                    else if (m_lastBackpack[i].DebugString != wizChar.Inventory.Items[i].DebugString)
                        Global.Log("Char{0} xItem: {1} <=> {2}", wizChar.Address, m_lastBackpack[i].DebugString, wizChar.Inventory.Items[i].DebugString);
                }
                m_lastBackpack = wizChar.Inventory.Items;
            }

            if (String.IsNullOrWhiteSpace(wizChar.Name))
                return;

            m_commonCtrls.labelLevel.Text = wizChar.BasicInfoString;
            m_commonCtrls.labelAC.Text = String.Format("{0}", wizChar.ArmorClass - wizChar.ACBonus);

            ListViewSelectionSaver savePack = new ListViewSelectionSaver(m_commonCtrls.lvBackpack);
            ListViewSelectionSaver saveEquip = new ListViewSelectionSaver(m_commonCtrls.lvEquipped);

            m_commonCtrls.lvEquipped.BeginUpdate();
            m_commonCtrls.lvBackpack.BeginUpdate();
            m_commonCtrls.lvEquipped.Items.Clear();
            m_commonCtrls.lvBackpack.Items.Clear();
            for (int i = 0; i < wizChar.Inventory.Items.Count; i++)
            {
                if (wizChar.Inventory.Items[i].IsEquipped)
                    SetEquippedLVI(wizChar.Inventory.Items[i], wizChar);
                else
                    SetBackpackLVI(wizChar.Inventory.Items[i], wizChar);
            }
            Global.FitSingleColumn(m_commonCtrls.lvBackpack);
            Global.FitSingleColumn(m_commonCtrls.lvEquipped);

            UpdateHeaders();

            savePack.Restore();
            saveEquip.Restore();

            m_commonCtrls.lvEquipped.EndUpdate();
            m_commonCtrls.lvBackpack.EndUpdate();

            SetResistances(wizChar.GetResistances());
            m_commonCtrls.labelCondition.Text = WizCharacter.ConditionString(wizChar.Condition, wizChar.GetExtraConditions(), true);
            m_tipCondition.SetToolTip(m_commonCtrls.labelCondition, WizCharacter.ConditionDescription(wizChar.Condition, wizChar));
            m_tipCondition.ShowAlways = true;
            m_tipCondition.AutoPopDelay = 32000;

            m_commonCtrls.labelExp.Text = wizChar.ExperienceString;
            labelKnownSpells.Text = wizChar.SpellBook.ToString();
            StringBuilder sbSpells = new StringBuilder();
            if (!(m_char is Wiz4Character) && (wizChar.IsMage || wizChar.IsPriest))
            {
                sbSpells.Append("(Can learn level ");
                int iMaxMage = wizChar.MaxMageSpell(wizChar.Level);
                int iMaxPriest = wizChar.MaxPriestSpell(wizChar.Level);
                if (iMaxMage > 0)
                {
                    if (iMaxMage > 1)
                        sbSpells.AppendFormat("1-{0} Mage", iMaxMage);
                    else if (iMaxMage == 1)
                        sbSpells.Append("1 Mage");
                    if (iMaxPriest > 0)
                        sbSpells.Append(" and level ");
                }
                if (iMaxPriest > 0)
                {
                    if (iMaxPriest > 1)
                        sbSpells.AppendFormat("1-{0} Priest", iMaxPriest);
                    else if (iMaxPriest == 1)
                        sbSpells.Append("1 Priest");
                }
                sbSpells.Append(" spells)");
            }
            labelSpellLevel.Text = sbSpells.ToString();
            labelGold.Text = String.Format("{0}", wizChar.Gold);
            m_commonCtrls.labelHP.Text = wizChar.HitPoints.ToString(); 
            m_commonCtrls.labelSP.Text = wizChar.SpellPoints.ToString();
            m_commonCtrls.labelMelee.Text = wizChar.MeleeDamageString;

            m_commonCtrls.llCureAll.Visible = wizChar.SpellBook.KnowsAnyHealing && Properties.Settings.Default.EnableMemoryWrite;

            labelStrength.Text = String.Format("{0}", wizChar.Strength);
            labelIQ.Text = String.Format("{0}", wizChar.IQ);
            labelPiety.Text = String.Format("{0}", wizChar.Piety);
            labelVitality.Text = String.Format("{0}", wizChar.Vitality);
            labelAgility.Text = String.Format("{0}", wizChar.Agility);
            labelLuck.Text = String.Format("{0}", wizChar.Luck);

            int iMod = wizChar.Class == WizClass.Thief ? 6 : wizChar.Class == WizClass.Ninja ? 4 : 1;
            labelIDTraps.Text = String.Format("{0}%", wizChar.IDTraps);
            labelIdentifyItem.Text = String.Format("{0}%", wizChar.IDItem);

            labelIdentifyItem.Visible = !bWiz5;
            labelIdentifyItemHeader.Visible = !bWiz5;

            labelDisarm.Text = String.Format("{0}%", Disarm);
            labelDispel.Text = String.Format("{0}%", wizChar.Dispel);
            labelCritical.Text = String.Format("{0}%", wizChar.CriticalChance);

            m_commonCtrls.lvResistances.BeginUpdate();
            m_commonCtrls.lvResistances.Columns[0].Text = "Save vs.";
            m_commonCtrls.lvResistances.Items.Clear();
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsPoison, "Poison", wizChar.SaveVsPoison));
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsPetrify, "Petrify", wizChar.SaveVsPetrify));
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsWand, "Wand", wizChar.SaveVsWand));
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsBreath, "Breath", wizChar.SaveVsBreath));
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsSpell, "Spell", wizChar.SaveVsSpell));
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsSleep, "Sleep", Math.Max(1, 20 - (4 * wizChar.Level))));
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsParalyze, "Paralyze", Math.Max(1, 10 - (2 * wizChar.Level))));

            Global.SizeHeadersAndContent(m_commonCtrls.lvResistances);
            m_commonCtrls.lvResistances.EndUpdate();

            labelRegen.Text = Global.AddPlus(wizChar.Regeneration);

            labelPoison.Text = wizChar.PoisonString;

            MoveControls(labelMoveAC, labelGold.Location.X, m_commonCtrls.labelACHeader, m_commonCtrls.labelAC);
            MoveControls(labelMoveHP, labelGold.Location.X, m_commonCtrls.labelHPHeader, m_commonCtrls.labelHP);
            MoveControls(labelMoveSP, labelGold.Location.X, m_commonCtrls.labelSPHeader, m_commonCtrls.labelSP);
            //MoveControls(m_commonCtrls.labelMeleeHeader, labelGold.Location.X, m_commonCtrls.labelMeleeHeader, m_commonCtrls.labelMelee);

            // Wizardry 4 doesn't have as many stats
            if (m_char is Wiz4Character)
            {
                foreach (Control ctrl in new Control[] { labelCritical, labelCriticalHeader, labelDisarm, labelDisarmHeader, labelDispel, labelDispelHeader,
                    labelIdentifyItem, labelIdentifyItemHeader, labelIDTraps, labelIDTrapsHeader, labelPoison, labelPoisonHeader})
                    ctrl.Hide();
                m_commonCtrls.labelExpHeader.Text = "Keys";
            }
        }

        public int Disarm
        {
            get
            {
                WizCharacter wizChar = m_char as WizCharacter;
                int iMod = wizChar.Class == WizClass.Thief ? 6 : wizChar.Class == WizClass.Ninja ? 4 : 1;
                int iMap = (Hacker == null ? 0 : Hacker.GetCurrentMapIndex());
                if (iMap < 1 || iMap > 10)
                    iMap = 0;
                return Math.Min(100, ((iMod == 1 ? 0 : 50) + wizChar.Level - iMap) * 100 / 70);
            }
        }

        private ListViewItem GetSavingLVI(GenericResistanceFlags res, string str, int val)
        {
            ListViewItem lvi = new ListViewItem(str);
            int iPercent = 5 * (20 - val);
            if (iPercent < 0)
                iPercent = 0;
            if (iPercent > 100)
                iPercent = 100;
            lvi.SubItems.Add(String.Format("{0} ({1}%)", val, iPercent));
            lvi.Tag = new ResistanceValue(res, val);
            return lvi;
        }

        private void miViewView_Click(object sender, EventArgs e)
        {
            if (m_ctrlView == labelKnownSpells)
            {
                KnownSpellsEditForm formSpells = new KnownSpellsEditForm();
                formSpells.SetSpells(((WizCharacter)m_char).SpellBook, m_char.BasicClass);
                formSpells.ReadOnly = true;
                formSpells.ShowDialog();
            }
        }

        private void labelKnownSpells_MouseUp(object sender, MouseEventArgs e)
        {
            if (Global.Cheats)
                return;     // In cheat mode you can just edit the spells

            m_ctrlView = labelKnownSpells;

            cmView.Show(Cursor.Position);
        }
    }
}
