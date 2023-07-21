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
    public partial class Ultima123CharacterInfoControl : CharacterInfoControl
    {
        protected Control m_ctrlView = null;
        public override int CharacterSize { get { return Games.CharacterSize(Game); } }

        public Ultima123CharacterInfoControl()
        {
            InitializeComponent();
        }

        public Ultima123CharacterInfoControl(IMain main) : base(main)
        {
            InitializeComponent();
            m_char = UltimaCharacter.Create(main.Game);

            FindEditableAttributes();

            SetCommonControls();
        }

        public GameNames Game { get { return m_main == null ? GameNames.None : m_main.Game; } }

        protected override void Stat_MouseLeave(object sender, EventArgs e) { base.Stat_MouseLeave(sender, e); }

        private void labelStrength_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Strength, labelStrength); }
        private void labelIntelligence_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Intelligence, labelIntelligence); }
        private void labelWisdom_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Wisdom, labelWisdom); }
        private void labelAgility_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Agility, labelAgility); }
        private void labelStamina_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Stamina, labelStamina); }
        private void labelCharisma_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Charisma, labelCharisma); }
        private void labelStrengthHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Strength, labelStrengthHeader); }
        private void labelIntelligenceHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Intelligence, labelIntelligenceHeader); }
        private void labelWisdomHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Wisdom, labelWisdomHeader); }
        private void labelAgilityHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Piety, labelAgilityHeader); }
        private void labelStaminaHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Vitality, labelStaminaHeader); }
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
                        case 1: return new TriggerControl(labelAgility);
                        case 2: return new TriggerControl(labelStamina);
                        case 3: return new TriggerControl(labelCharisma);
                        case 4: return new TriggerControl(labelWisdom);
                        case 5: return new TriggerControl(labelIntelligence);
                        default: return base.GetTriggerControl(entity, strVal);
                    }
                case TriggerEntity.StatNamed:
                    if (strVal == null || strVal.Length < 1)
                        return base.GetTriggerControl(entity, strVal);
                    switch (Char.ToLower(strVal[0]))
                    {
                        case 's': return new TriggerControl(labelStrength);
                        case 'a': return new TriggerControl(labelAgility);
                        case 't': return new TriggerControl(labelStamina);
                        case 'c': return new TriggerControl(labelCharisma);
                        case 'w': return new TriggerControl(labelWisdom);
                        case 'i': return new TriggerControl(labelIntelligence);
                        default: return base.GetTriggerControl(entity, strVal);
                    }
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

            if (info is UltimaPartyInfo)
            {
                UltimaPartyInfo UltimaInfo = info as UltimaPartyInfo;

                m_bytes = new byte[info.CharacterSize];
                Buffer.BlockCopy(info.Bytes, iAddress * info.CharacterSize, m_bytes, 0, info.CharacterSize);
                m_char.SetFromBytes(iIndex, m_bytes, gameInfo, encounterInfo);
                m_iCharacterIndex = iIndex;
                m_iCharacterAddress = iAddress;
                m_iCharacterPosition = info.Positions[iIndex];
                ((UltimaCharacter)m_char).Address = m_iCharacterAddress;
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
            UltimaCharacter ultimaChar = m_char as UltimaCharacter;

            CheckMonitorBackpack();

            if (String.IsNullOrWhiteSpace(ultimaChar.Name))
                return;

            m_commonCtrls.labelLevel.Text = ultimaChar.BasicInfoString;

            ListViewSelectionSaver savePack = new ListViewSelectionSaver(m_commonCtrls.lvBackpack);
            ListViewSelectionSaver saveEquip = new ListViewSelectionSaver(m_commonCtrls.lvEquipped);

            m_commonCtrls.lvEquipped.BeginUpdate();
            m_commonCtrls.lvBackpack.BeginUpdate();
            m_commonCtrls.lvEquipped.Items.Clear();
            m_commonCtrls.lvBackpack.Items.Clear();
            for (int i = 0; i < ultimaChar.Inventory.Items.Count; i++)
            {
                if (ultimaChar.Inventory.Items[i].IsEquipped)
                    SetEquippedLVI(ultimaChar.Inventory.Items[i], ultimaChar);
                else
                    SetBackpackLVI(ultimaChar.Inventory.Items[i], ultimaChar);
            }
            Global.FitSingleColumn(m_commonCtrls.lvBackpack);
            Global.FitSingleColumn(m_commonCtrls.lvEquipped);

            UpdateHeaders();

            savePack.Restore();
            saveEquip.Restore();

            m_commonCtrls.lvEquipped.EndUpdate();
            m_commonCtrls.lvBackpack.EndUpdate();
            labelFood.Text = String.Format("{0}", ultimaChar.Food);
            labelCoin.Text = String.Format("{0}", ultimaChar.Coin);

            m_commonCtrls.labelCondition.Location = new Point(labelQuest.Location.X, m_commonCtrls.labelCondition.Location.Y);
            Ultima1Character u1Char = ultimaChar as Ultima1Character;
            if (u1Char != null)
            {
                m_commonCtrls.labelConditionHeader.Text = "Last Visited";
                m_commonCtrls.labelCondition.Text = Ultima1Character.LastUsed(u1Char.LastSignUsed);
            }

            SetResistances(ultimaChar.GetResistances());
            m_tipCondition.ShowAlways = true;
            m_tipCondition.AutoPopDelay = 32000;

            m_commonCtrls.labelExp.Text = ultimaChar.ExperienceString;

            m_commonCtrls.labelHP.Text = ultimaChar.Hits.ToString();
            m_commonCtrls.labelMelee.Text = ultimaChar.MeleeDamageString;

            m_commonCtrls.llCureAll.Visible = false;

            labelStrength.Text = String.Format("{0}", ultimaChar.Stats.Strength);
            labelIntelligence.Text = String.Format("{0}", ultimaChar.Stats.Intelligence);
            labelWisdom.Text = String.Format("{0}", ultimaChar.Stats.Wisdom);
            labelAgility.Text = String.Format("{0}", ultimaChar.Stats.Agility);
            labelStamina.Text = String.Format("{0}", ultimaChar.Stats.Stamina);
            labelCharisma.Text = String.Format("{0}", ultimaChar.Stats.Charisma);

            Global.SizeHeadersAndContent(m_commonCtrls.lvResistances);
            m_commonCtrls.lvResistances.EndUpdate();
        }

        protected override CheatMenuFlags PrepareCheatMenu(Control label, CheatMenuFlags flags = CheatMenuFlags.None)
        {
            if (!(label is EditableAttributeLabel || label is MMItemLabel || label is ListView))
                return CheatMenuFlags.None;

            CheatMenuFlags menuFlags = CheatMenuFlags.AllNonlevel;
            UltimaCharacter UltimaChar = m_char as UltimaCharacter;
            if (UltimaChar == null)
                return CheatMenuFlags.None;

            m_cheatOffsets = null;

            m_cheatType = AttributeType.Int16;
            if (label == labelIntelligence)
                m_cheatOffsets = new CheatOffsets(new int[] { UltimaChar.Offsets.Stats + UltimaChar.Stats.IntelligenceOffset });
            else if (label == labelWisdom)
                m_cheatOffsets = new CheatOffsets(new int[] { UltimaChar.Offsets.Stats + UltimaChar.Stats.WisdomOffset });
            else if (label == labelAgility)
                m_cheatOffsets = new CheatOffsets(new int[] { UltimaChar.Offsets.Stats + UltimaChar.Stats.AgilityOffset });
            else if (label == labelStamina)
                m_cheatOffsets = new CheatOffsets(new int[] { UltimaChar.Offsets.Stats + UltimaChar.Stats.StaminaOffset });
            else if (label == labelCharisma)
                m_cheatOffsets = new CheatOffsets(new int[] { UltimaChar.Offsets.Stats + UltimaChar.Stats.CharismaOffset });
            else if (label == labelStrength)
                m_cheatOffsets = new CheatOffsets(new int[] { UltimaChar.Offsets.Stats + UltimaChar.Stats.StrengthOffset });
            else if (label == labelFood)
            {
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Food });
            }
            else if (label == m_commonCtrls.labelExp)
            {
                menuFlags |= CheatMenuFlags.NextLevel;
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Experience });
            }
            else if (label == m_commonCtrls.labelHP)
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.CurrentHP });
            else if (label == labelCoin)
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Gold });
            else if (label == m_commonCtrls.labelLevel)
            {
                m_cheatType = AttributeType.LevSexAlignRaceClass;
                menuFlags =  CheatMenuFlags.Edit | CheatMenuFlags.SuperChar;
                m_cheatOffsets = new CheatOffsets(new int[] {
                    -1, // level
                    -1, // levelmod
                    m_char.Offsets.Name,
                    m_char.Offsets.Sex,
                    -1, // alignmentmod
                    -1, // alignment
                    m_char.Offsets.Race,
                    m_char.Offsets.Class,
                    -1, -1, -1, -1, -1, -1
                });
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
                m_cheatOffsets = ((UltimaCharacter)m_char).GetInventoryCheatOffsets(iIndex);
            }

            if (m_cheatOffsets == null)
                return CheatMenuFlags.None;

            if (m_cheatOffsets.Length < 1 || (m_cheatType != AttributeType.LevSexAlignRaceClass && m_cheatOffsets[0] < 0))        // Offset refers to an item that doesn't exist in this game's character record
                return CheatMenuFlags.None;

            return menuFlags;
        }
    }
}
