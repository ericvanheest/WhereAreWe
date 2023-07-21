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
    public enum Wiz1Race
    {
        None = 0,
        Human = 1,
        Elf = 2,
        Dwarf = 3,
        Gnome = 4,
        Hobbit = 5,
        Last
    }

    public enum Wiz1Class
    {
        None = -1,
        Fighter = 0,
        Mage = 1,
        Priest = 2,
        Thief = 3,
        Bishop = 4,
        Samurai = 5,
        Lord = 6,
        Ninja = 7,
        Last
    }

    public enum Wiz1Condition
    {
        Good = 0,
        Afraid = 1,
        Asleep = 2,
        Paralyzed = 3,
        Petrified = 4,
        Dead = 5,
        Ashes = 6,
        Lost = 7
    }

    public partial class Wiz1CharacterInfoControl : CharacterInfoControl
    {
        private Control m_ctrlView = null;

        public Wiz1CharacterInfoControl(IMain main) : base(main)
        {
            InitializeComponent();
            m_char = new Wiz1Character();

            FindEditableAttributes();
        }

        protected override void Stat_MouseLeave(object sender, EventArgs e)
        {
            base.Stat_MouseLeave(sender, e);
        }

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
        private void labelIDTrapsHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Identify, labelIDTrapsHeader); }
        private void labelIDTraps_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Identify, labelIDTraps); }
        private void labelDisarmHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Thievery, labelDisarmHeader); }
        private void labelDisarm_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Thievery, labelDisarm); }


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
                m_cheatType = AttributeType.WizSpellPoints;
                menuFlags = CheatMenuFlags.Edit;
                m_cheatOffsets = Global.IntRange(m_char.Offsets.CurrentSP, 28, 2);
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
                if (iIndex != -1)
                {
                    m_cheatOffsets = Global.IntRange(m_char.Offsets.Inventory + 1 + (8 * iIndex), 9);
                    m_cheatOffsets[0] = m_char.Offsets.Inventory;   // May need to change the number of items
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

        public override void SetInfo(PartyInfo info, int iIndex, GameInfo gameInfo)
        {
            if (info != null && info.Bytes.Length < (info.Addresses[iIndex] + 1) * CharacterSize)
                return;

            if (info is Wiz1PartyInfo)
            {
                Wiz1PartyInfo wiz1Info = info as Wiz1PartyInfo;

                m_bytes = new byte[info.CharacterSize + 2];
                if (wiz1Info.Silenced != null && wiz1Info.Silenced.Length > iIndex)
                    Global.SetInt16(m_bytes, info.CharacterSize, wiz1Info.Silenced[iIndex]);
                Buffer.BlockCopy(info.Bytes, info.Addresses[iIndex] * info.CharacterSize, m_bytes, 0, info.CharacterSize);
                m_char.SetFromBytes(m_bytes, gameInfo);
                m_iCharacterIndex = iIndex;
                m_iCharacterAddress = info.Addresses[iIndex];
                m_iCharacterPosition = info.Positions[iIndex];
                ((Wiz1Character)m_char).Address = m_iCharacterAddress;
            }
            else
            {
                m_iCharacterAddress = -1;
                m_iCharacterIndex = -1;
                m_iCharacterPosition = -1;
            }

            UpdateUI();
        }

        public override void UpdateUI()
        {
            Wiz1Character wiz1Char = m_char as Wiz1Character;

            if (String.IsNullOrWhiteSpace(wiz1Char.Name))
                return;

            m_commonCtrls.labelLevel.Text = wiz1Char.BasicInfoString;
            m_commonCtrls.labelAC.Text = String.Format("{0}", wiz1Char.ArmorClass - wiz1Char.ACBonus);

            m_commonCtrls.lvEquipped.BeginUpdate();
            m_commonCtrls.lvBackpack.BeginUpdate();
            m_commonCtrls.lvEquipped.Items.Clear();
            m_commonCtrls.lvBackpack.Items.Clear();
            for (int i = 0; i < wiz1Char.Inventory.Items.Count; i++)
            {
                if (wiz1Char.Inventory.Items[i].IsEquipped)
                    SetEquippedLVI(wiz1Char.Inventory.Items[i], wiz1Char);
                else
                    SetBackpackLVI(wiz1Char.Inventory.Items[i], wiz1Char);
            }
            Global.FitSingleColumn(m_commonCtrls.lvBackpack);
            Global.FitSingleColumn(m_commonCtrls.lvEquipped);

            UpdateHeaders();

            m_commonCtrls.lvEquipped.EndUpdate();
            m_commonCtrls.lvBackpack.EndUpdate();

            SetResistances(wiz1Char.GetResistances());
            m_commonCtrls.labelCondition.Text = Wiz1Character.ConditionString(wiz1Char.Condition, wiz1Char.GetExtraConditions(), true);
            m_tipCondition.SetToolTip(m_commonCtrls.labelCondition, Wiz1Character.ConditionDescription(wiz1Char.Condition, wiz1Char));
            m_tipCondition.ShowAlways = true;
            m_tipCondition.AutoPopDelay = 32000;

            m_commonCtrls.labelExp.Text = wiz1Char.ExperienceString;
            labelKnownSpells.Text = wiz1Char.SpellBook.ToString();
            StringBuilder sbSpells = new StringBuilder();
            if (wiz1Char.IsMage || wiz1Char.IsPriest)
            {
                sbSpells.Append("(Can learn level ");
                int iMaxMage = wiz1Char.MaxMageSpell(wiz1Char.Level);
                int iMaxPriest = wiz1Char.MaxPriestSpell(wiz1Char.Level);
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
            labelGold.Text = String.Format("{0}", wiz1Char.Gold);
            m_commonCtrls.labelHP.Text = wiz1Char.HitPoints.ToString(); 
            m_commonCtrls.labelSP.Text = wiz1Char.SpellPoints.ToString();
            m_commonCtrls.labelMelee.Text = wiz1Char.MeleeDamageString;

            m_commonCtrls.llCureAll.Visible = wiz1Char.SpellBook.KnowsAnyHealing && Properties.Settings.Default.EnableMemoryWrite;

            labelStrength.Text = String.Format("{0}", wiz1Char.Strength);
            labelIQ.Text = String.Format("{0}", wiz1Char.IQ);
            labelPiety.Text = String.Format("{0}", wiz1Char.Piety);
            labelVitality.Text = String.Format("{0}", wiz1Char.Vitality);
            labelAgility.Text = String.Format("{0}", wiz1Char.Agility);
            labelLuck.Text = String.Format("{0}", wiz1Char.Luck);

            int iMod = wiz1Char.Class == Wiz1Class.Thief ? 6 : wiz1Char.Class == Wiz1Class.Ninja ? 4 : 1;
            labelIDTraps.Text = String.Format("{0}%", Math.Min(95, iMod * wiz1Char.Agility));
            int iMap = (Hacker == null ? 0 : Hacker.GetCurrentMapIndex());
            if (iMap < 1 || iMap > 10)
                iMap = 0;
            int iDisarm = Math.Min(100, ((iMod == 1 ? 0 : 50) + wiz1Char.Level - iMap) * 100 / 70);
            labelDisarm.Text = String.Format("{0}%", iDisarm);

            m_commonCtrls.lvResistances.BeginUpdate();
            m_commonCtrls.lvResistances.Columns[0].Text = "Save vs.";
            m_commonCtrls.lvResistances.Items.Clear();
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsPoison, "Poison", wiz1Char.SaveVsPoison));
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsPetrify, "Petrify", wiz1Char.SaveVsPetrify));
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsWand, "Wand", wiz1Char.SaveVsWand));
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsBreath, "Breath", wiz1Char.SaveVsBreath));
            m_commonCtrls.lvResistances.Items.Add(GetSavingLVI(GenericResistanceFlags.SaveVsSpell, "Spell", wiz1Char.SaveVsSpell));
            Global.SizeHeadersAndContent(m_commonCtrls.lvResistances);
            m_commonCtrls.lvResistances.EndUpdate();

            labelRegen.Text = Global.AddPlus(wiz1Char.Regeneration);
            labelPoison.Text = wiz1Char.LocationX == 0 ? "None" : string.Format("{0}", wiz1Char.LocationX);

            MoveControls(labelMoveAC, labelGold.Location.X, m_commonCtrls.labelACHeader, m_commonCtrls.labelAC);
            MoveControls(labelMoveHP, labelGold.Location.X, m_commonCtrls.labelHPHeader, m_commonCtrls.labelHP);
            MoveControls(labelMoveSP, labelGold.Location.X, m_commonCtrls.labelSPHeader, m_commonCtrls.labelSP);
            //MoveControls(m_commonCtrls.labelMeleeHeader, labelGold.Location.X, m_commonCtrls.labelMeleeHeader, m_commonCtrls.labelMelee);
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
                formSpells.SetSpells(((Wiz1Character)m_char).SpellBook, m_char.BasicClass);
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

    public class Wiz1Inventory : Inventory
    {
        private List<Item> m_items;

        public override List<Item> Items { get { return m_items; } set { m_items = value; } }

        public override int NumBackpackItems { get { return Items.Count(i => !i.IsEquipped); } }

        public Wiz1Inventory(byte[] bytes, int offset = 0)
        {
            // A Wizardry 1 inventory is an Int16 followed by up to eight 8-byte items
            int iNumItems = BitConverter.ToInt16(bytes, offset);

            m_items = new List<Item>(iNumItems);
            for (int i = 0; i < iNumItems; i++)
            {
                if (i * 8 + 2 > bytes.Length - 8 - offset)
                    break;  // Not enough bytes for the item count

                Wiz1Item item = Wiz1Item.FromInventoryBytes(bytes, offset + 2 + (i * 8));
                if (item != null)
                {
                    item.MemoryIndex = i;
                    m_items.Add(item);
                }
            }
        }

        public Wiz1Inventory(List<Item> items)
        {
            m_items = items;
        }

        public Wiz1Inventory()
        {
            m_items = new List<Item>();
        }

        public bool HasItem(Wiz1ItemIndex itemWanted)
        {
            return Items.Any(i => i.Index == (int)itemWanted);
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[66];

            for(int i = 0; i < 66; i++)
                bytes[i] = 0;

            Global.SetInt16(bytes, 0, m_items.Count);

            int iIndexItem = 0;
            foreach(Wiz1Item item in m_items)
            {
                bytes[2 + (iIndexItem * 8)] = (byte)(item.IsEquipped ? 1 : 0);
                bytes[2 + (iIndexItem * 8) + 2] = (byte)(item.InvCursed ? 1 : 0);
                bytes[2 + (iIndexItem * 8) + 4] = (byte)(item.Identified ? 1 : 0);
                byte[] bytesInt = BitConverter.GetBytes((Int16)item.Index);
                Buffer.BlockCopy(bytesInt, 0, bytes, 2 + (iIndexItem * 8) + 6, bytesInt.Length);
                iIndexItem++;
            }

            return bytes;
        }
    }

    public class Wiz1CharacterOffsets : CharacterOffsets
    {
        public override int NameLength       { get { return 0; } }
        public override int Name             { get { return 1; } }
        public override int PasswordLength   { get { return 16; } }
        public override int Password         { get { return 17; } }
        public override int Out              { get { return 32; } }
        public override int Race             { get { return 34; } }
        public override int Class            { get { return 36; } }
        public override int Age              { get { return 38; } }
        public override int Condition        { get { return 40; } }
        public override int Alignment        { get { return 42; } }
        public override int ConditionLength  { get { return 2; } }
        public override int Stats            { get { return 44; } }
        public override int SavingThrows     { get { return 48; } }
        public override int Gold             { get { return 52; } }
        public override int GoldLength       { get { return 6; } }
        public override int Inventory        { get { return 58; } }
        public override int InventoryLength  { get { return 66; } }
        public override int Experience       { get { return 124; } }
        public override int ExperienceLength { get { return 6; } }
        public override int LevelMod         { get { return 130; } }
        public override int Level            { get { return 132; } }
        public override int CurrentHP        { get { return 134; } }
        public override int MaxHP            { get { return 136; } }
        public override int CurrentSP        { get { return 146; } }
        public override int LastArmorClass   { get { return 174; } }
        public override int ArmorClass       { get { return 176; } }
        public override int Regeneration     { get { return 178; } }
        public override int Critical         { get { return 180; } }
        public override int Swings           { get { return 182; } }
        public override int MeleeDamage      { get { return 184; } }
        public override int WeaponEffects    { get { return 190; } }
        public override int LocationX        { get { return 200; } }
        public override int LocationY        { get { return 202; } }
        public override int LocationZ        { get { return 204; } }
        public override int Awards           { get { return 206; } }
        public override int AwardsLength     { get { return 2; } }
        public override int Spells           { get { return 138; } }
        public override int SpellsLength     { get { return 8; } }
    }

    [Serializable]
    public class Wiz1Character : WizardryCharacter
    {
        public string CharName;
        public string Password;
        public int Silenced;
        public bool Out;
        public Wiz1Alignment Alignment;
        public Wiz1Race Race;
        public Wiz1Class Class;
        public int Strength;
        public int IQ;
        public int Piety;
        public int Vitality;
        public int Agility;
        public int Luck;
        public int Level;
        public int LevelMod;
        public int Age;
        public long Experience;
        public Wiz1SpellPoints SpellPoints;
        public MMHitPoints HitPoints;
        public long Gold;
        public int ArmorClass;
        public int ACBonus;
        public int LastArmorClass;
        public Wiz1Condition Condition;
        public Wiz1Inventory Inventory;
        public Wiz1KnownSpells SpellBook;
        public int Critical;
        public BasicDamage MeleeDamage;
        public byte[] WeaponEffects;  // 10 bytes
        public int LocationX; // Also used for poison counters
        public int LocationY;
        public int LocationZ;
        public int Honors;

        public int Address = -1;
        public const int SizeInBytes = 208;

        public Wiz1Character()
        {
            Address = -1;
        }

        public string GetExtraConditionDesc()
        {
            StringBuilder sb = new StringBuilder();
            if (Inventory.Items.Count > 7)
                sb.AppendFormat("Backpack Full: Monsters may not drop items\r\n");
            if (PoisonCounter != 0)
                sb.AppendFormat("Poisoned: 25% chance per step of losing 10% HP\r\n");
            if (Silenced != 0)
                sb.AppendFormat("Silenced: Character cannot cast spells for {0}\r\n", Global.Plural(Silenced, "round"));
            return sb.ToString().Trim();
        }

        public string GetExtraConditions()
        {
            StringBuilder sb = new StringBuilder();
            if (Inventory.Items.Count > 7)
                sb.AppendFormat("Backpack Full, ");
            if (PoisonCounter != 0)
                sb.AppendFormat("Poisoned, ");
            if (Silenced > 0 && Silenced < 100)
                sb.AppendFormat("Silenced ({0}), ", Silenced);
            return Global.Trim(sb).ToString();
        }

        public override CharacterOffsets Offsets { get { return Wiz1.Offsets; } }
        public override int BasicAddress { get { return Address; } }

        public override int CharacterSize { get { return SizeInBytes; } }
        public override Inventory BasicInventory { get { return Inventory as Inventory; } }

        public Wiz1Character(byte[] bytes, int iIndex = 0, bool bFromRosterFile = false)
        {
            Address = -1;
            if (bytes == null || bytes.Length < iIndex + SizeInBytes - 1)
                return;
            SetCharFromStream(new MemoryStream(bytes, iIndex, bytes.Length - iIndex), null, bFromRosterFile);
        }

        public override void Serialize(Stream stream)
        {
            byte[] bytes = new byte[CharacterSize];

            bytes[Offsets.NameLength] = (byte) CharName.Length;
            for (int i = Offsets.Name; i <= 15; i++)
                bytes[i] = 0x00;
            Buffer.BlockCopy(new ASCIIEncoding().GetBytes(CharName), 0, bytes, Offsets.Name, CharName.Length);
            bytes[Offsets.PasswordLength] = (byte)Password.Length;
            for (int i = Offsets.Password; i <= 15; i++)
                bytes[i] = 0x00;
            Buffer.BlockCopy(new ASCIIEncoding().GetBytes(Password), 0, bytes, Offsets.Password, CharName.Length);
            Global.SetInt16(bytes, Offsets.Alignment, (int)Alignment);
            Global.SetInt16(bytes, Offsets.Race, (int)Race);
            Global.SetInt16(bytes, Offsets.Class, (int)Class);
            Global.SetInt16(bytes, Offsets.Age, Age);
            Global.SetInt16(bytes, Offsets.LevelMod, LevelMod);
            Global.SetInt16(bytes, Offsets.Level, Level);
            Global.SetInt16(bytes, Offsets.CurrentHP, HitPoints.Current);
            Global.SetInt16(bytes, Offsets.MaxHP, HitPoints.Maximum);
            Global.SetInt16(bytes, Offsets.LastArmorClass, LastArmorClass);
            Global.SetInt16(bytes, Offsets.ArmorClass, ArmorClass);

            Global.SetInt16(bytes, Offsets.Regeneration, Regeneration);
            Global.SetInt16(bytes, Offsets.Critical, Critical);
            Global.SetInt16(bytes, Offsets.Swings, MeleeDamage.NumAttacks);
            Global.SetInt16(bytes, Offsets.MeleeDamage, MeleeDamage.Dice.Quantity);
            Global.SetInt16(bytes, Offsets.MeleeDamage + 2, MeleeDamage.Dice.Faces);
            Global.SetInt16(bytes, Offsets.MeleeDamage + 4, MeleeDamage.Dice.Bonus);
            Global.SetInt16(bytes, Offsets.LocationX, LocationX);
            Global.SetInt16(bytes, Offsets.LocationY, LocationY);    
            Global.SetInt16(bytes, Offsets.LocationZ, LocationZ);
            Global.SetInt16(bytes, Offsets.Awards, Honors);

            Global.SetInt16(bytes, Offsets.Out, Out ? 1 : 0);
            Global.SetInt16(bytes, Offsets.Condition, (int)Condition);
            PackedFiveBitValues bytesP5 = new PackedFiveBitValues(Strength, IQ, Piety, Vitality, Agility, Luck);
            byte[] bytesTemp = bytesP5.Bytes;
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.Stats, bytesTemp.Length);

            bytesP5 = new PackedFiveBitValues(SaveVsPoison, SaveVsPetrify, SaveVsWand, SaveVsBreath, SaveVsSpell);
            bytesTemp = bytesP5.Bytes;
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.SavingThrows, bytesTemp.Length);

            bytesTemp = WizardryLong.GetBytes(Gold);
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.Gold, bytesTemp.Length);
            bytesTemp = WizardryLong.GetBytes(Experience);
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.Experience, bytesTemp.Length);
            bytesTemp = Inventory.GetBytes();
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.Inventory, bytesTemp.Length);
            bytesTemp = SpellBook.GetBytes();
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.Spells, bytesTemp.Length);
            bytesTemp = SpellPoints.GetBytes();
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.CurrentSP, bytesTemp.Length);

            Buffer.BlockCopy(WeaponEffects, 0, bytes, Offsets.WeaponEffects, WeaponEffects.Length);

            stream.Write(bytes, 0, CharacterSize);
        }

        public override void SetCharFromStream(Stream stream, GameInfo info, bool bFromRosterFile = false)
        {
            if (stream.Length < CharacterSize)
                return;

            RawBytes = new byte[CharacterSize];
            stream.Read(RawBytes, 0, CharacterSize);

            if (stream.CanRead && !bFromRosterFile)
                Silenced = Global.ReadInt16(stream);
            else
                Silenced = 0;

            CharName = new ASCIIEncoding().GetString(RawBytes, Offsets.Name, RawBytes[Offsets.NameLength]);
            Password = new ASCIIEncoding().GetString(RawBytes, Offsets.Password, RawBytes[Offsets.PasswordLength]);
            Alignment = (Wiz1Alignment)BitConverter.ToInt16(RawBytes, Offsets.Alignment);
            Race = (Wiz1Race)BitConverter.ToInt16(RawBytes, Offsets.Race);
            Class = (Wiz1Class)BitConverter.ToInt16(RawBytes, Offsets.Class);
            Condition = (Wiz1Condition)BitConverter.ToInt16(RawBytes, Offsets.Condition);
            Age = BitConverter.ToInt16(RawBytes, Offsets.Age);
            LevelMod = BitConverter.ToInt16(RawBytes, Offsets.LevelMod);
            Level = BitConverter.ToInt16(RawBytes, Offsets.Level);
            HitPoints = new MMHitPoints(BitConverter.ToInt16(RawBytes, Offsets.CurrentHP), BitConverter.ToInt16(RawBytes, Offsets.MaxHP));
            LastArmorClass = BitConverter.ToInt16(RawBytes, Offsets.LastArmorClass);
            ArmorClass = BitConverter.ToInt16(RawBytes, Offsets.ArmorClass);
            ACBonus = info is Wiz1GameInfo ? ((Wiz1GameInfo) info).ACBonus : 0;
            Out = BitConverter.ToInt16(RawBytes, Offsets.Out) == 1;
            PackedFiveBitValues stats = new PackedFiveBitValues(RawBytes, Offsets.Stats);
            Strength = stats.Values[0];
            IQ = stats.Values[1];
            Piety = stats.Values[2];
            Vitality = stats.Values[3];
            Agility = stats.Values[4];
            Luck = stats.Values[5];
            PackedFiveBitValues saving = new PackedFiveBitValues(RawBytes, Offsets.SavingThrows);
            SaveVsPoison = saving.Values[0];
            SaveVsPetrify = saving.Values[1];
            SaveVsWand = saving.Values[2];
            SaveVsBreath = saving.Values[3];
            SaveVsSpell = saving.Values[4];

            Gold = new WizardryLong(RawBytes, Offsets.Gold).Number;
            Experience = new WizardryLong(RawBytes, Offsets.Experience).Number;
            Inventory = new Wiz1Inventory(RawBytes, Offsets.Inventory);
            SpellBook = new Wiz1KnownSpells(RawBytes, Offsets.Spells);
            SpellPoints = new Wiz1SpellPoints(RawBytes, Offsets.CurrentSP);

            Regeneration = BitConverter.ToInt16(RawBytes, Offsets.Regeneration);
            Critical = BitConverter.ToInt16(RawBytes, Offsets.Critical);
            int swings = BitConverter.ToInt16(RawBytes, Offsets.Swings);
            int quantity = BitConverter.ToInt16(RawBytes, Offsets.MeleeDamage);
            int faces = BitConverter.ToInt16(RawBytes, Offsets.MeleeDamage + 2);
            int bonus = BitConverter.ToInt16(RawBytes, Offsets.MeleeDamage + 4);
            MeleeDamage = new BasicDamage(swings, new DamageDice(faces, quantity, bonus));

            LocationX = BitConverter.ToInt16(RawBytes, Offsets.LocationX);
            LocationY = BitConverter.ToInt16(RawBytes, Offsets.LocationY);
            LocationZ = BitConverter.ToInt16(RawBytes, Offsets.LocationZ);
            PoisonCounter = (info != null && info.Location.MapIndex == 0) ? 0 : LocationX;
            Honors = BitConverter.ToInt16(RawBytes, Offsets.Awards);

            WeaponEffects = new byte[10];
            Buffer.BlockCopy(RawBytes, Offsets.WeaponEffects, WeaponEffects, 0, WeaponEffects.Length);
        }

        public override int MaxBackpackSize { get { return 8 - Inventory.SelectEquippedItems.Count; } }

        public override Modifiers InternalModifiers
        {
            get
            {
                Modifiers mod = Wiz1.Modifiers.For(BasicRace).Clone();
                mod.Adjust(Wiz1.Modifiers.For(BasicClass).Clone());

                foreach(ModAttr attr in new ModAttr[] { ModAttr.SaveVsPoison, ModAttr.SaveVsPetrify, ModAttr.SaveVsWand, ModAttr.SaveVsBreath, ModAttr.SaveVsSpell })
                {
                    mod.Adjust(attr, GetStatModifier(BasicLuck.Temporary, PrimaryStat.Luck).Value, "Luck modifier");
                    mod.Adjust(attr, - Level / 5, "Level modifier");
                }

                foreach (Wiz1Item item in Inventory.SelectEquippedItems)
                {
                    if (item.CanEquip && item.AC > 0)
                        mod.Adjust(ModAttr.ArmorClass, -item.AC, item.DescriptionString);
                }

                return mod;
            }
        }

        public override StatAndModifier BasicStrength { get { return new StatAndModifier(Strength, 0); } }
        public override StatAndModifier BasicIQ { get { return new StatAndModifier(IQ, 0); } }
        public override StatAndModifier BasicPiety { get { return new StatAndModifier(Piety, 0); } }
        public override StatAndModifier BasicVitality { get { return new StatAndModifier(Vitality, 0); } }
        public override StatAndModifier BasicAgility { get { return new StatAndModifier(Agility, 0); } }
        public override StatAndModifier BasicLuck { get { return new StatAndModifier(Luck, 0); } }

        public override string Name { get { return CharName; } }
        public override SpellPoints QuickRefSpellPoints { get { return SpellPoints; } }
        public override List<Item> BackpackItems { get { return Inventory.SelectUnequippedItems; } }

        public override string GetACFormula(int iBless = 0)
        {
            if (ACBonus < 1)
                return String.Empty;
            return String.Format("-{0}\tSpell bonus", ACBonus);
        }

        public override bool KnowsSpell(Spell spell)
        {
            if (SpellBook == null)
                return false;
            return SpellBook.IsKnown((Wiz1SpellIndex) spell.BasicIndex);
        }

        public bool IsMage { get { return Class == Wiz1Class.Mage || Class == Wiz1Class.Bishop || Class == Wiz1Class.Samurai; } }
        public bool IsPriest { get { return Class == Wiz1Class.Priest || Class == Wiz1Class.Bishop || Class == Wiz1Class.Lord; } }

        public int MaxMageSpell(int iLevel)
        {
            switch (Class)
            {
                case Wiz1Class.Mage: return Math.Min(7, (iLevel + 1) / 2);
                case Wiz1Class.Bishop: return Math.Min(7, (iLevel + 3) / 4);
                case Wiz1Class.Samurai: return Math.Min(7, (iLevel - 1) / 3);
                default: return 0;
            }
        }

        public int MaxPriestSpell(int iLevel)
        {
            switch (Class)
            {
                case Wiz1Class.Priest: return Math.Min(7, (iLevel + 1) / 2);
                case Wiz1Class.Bishop: return Math.Min(7, iLevel / 4);
                case Wiz1Class.Lord: return Math.Min(7, (iLevel - 1) / 3);
                default: return 0;
            }
        }

        public override BasicDamage BasicMeleeDamage { get { return new BasicDamage(MeleeDamage.NumAttacks + NumAttacks, MeleeDamage.Dice); } }

        public override string MeleeDamageString
        {
            get
            {
                StringBuilder sb = new StringBuilder(BasicMeleeDamage.ToString());
                // TODO: Add weapon effects
                return sb.ToString();
            }
        }

        public override string CombatInfo
        {
            get
            {
                return String.Format("{0}{1} {2}", Condition == Wiz1Condition.Good ? "" : "*", CharName, HitPoints.ToString());
            }
        }

        public override long NeedsXP
        {
            get
            {
                return XPForNextLevel - Experience;
            }
        }

        public override long XPForNextLevel { get { return XPForLevel(Class, Level + 1); } }

        public override long BasicExperience { get { return Experience; } }

        public override long XPForLevel(GenericClass gClass, int iLevel)
        {
            return XPForLevel(ClassForGeneric(gClass), iLevel);
        }

        public static long[] LevelArray(Wiz1Class wiz1Class)
        {
            switch (wiz1Class)
            {
                case Wiz1Class.Fighter: return new long[] { 0, 0, 1000, 1724, 2972, 5124,  8834, 15231, 26260, 45275,  78060, 134586, 232044, 400075, 289709 };
                case Wiz1Class.Mage:    return new long[] { 0, 0, 1100, 1896, 3268, 5634,  9713, 16746, 28872, 49779,  85825, 147974, 255127, 439874, 318529 };
                case Wiz1Class.Priest:  return new long[] { 0, 0, 1050, 1810, 3120, 5379,  9274, 15989, 27567, 47529,  81946, 141286, 243596, 419993, 304132 };
                case Wiz1Class.Thief:   return new long[] { 0, 0,  900, 1551, 2674, 4610,  7948, 13703, 23625, 40732,  70187, 121081, 208750, 359931, 260639 };
                case Wiz1Class.Bishop:  return new long[] { 0, 0, 1200, 2105, 3692, 6477, 11363, 19935, 34973, 61136, 107642, 188845, 331370, 581240, 438479 };
                case Wiz1Class.Samurai: return new long[] { 0, 0, 1250, 2192, 3845, 6745, 11833, 20759, 36419, 63892, 112091, 196650, 345000, 605263, 456601 };
                case Wiz1Class.Lord:    return new long[] { 0, 0, 1300, 2280, 4000, 7017, 12310, 21596, 37887, 66468, 116610, 204578, 358908, 629663, 475008 };
                case Wiz1Class.Ninja:   return new long[] { 0, 0, 1450, 2543, 4461, 7826, 13729, 24085, 42254, 74129, 130050, 228157, 400275, 702236, 529756 };
                default:                return new long[] { 0, 0, 1000, 1724, 2972, 5124,  8834, 15231, 26260, 45275,  78060, 134586, 232044, 400075, 289709 };
            }
        }

        public static long XPForLevel(Wiz1Class wiz1Class, int iLevel)
        {
            long[] levels = LevelArray(wiz1Class);

            if (iLevel < 2)
                return 0;
            if (iLevel < 14)
                return levels[iLevel];
            else
                return levels[13] + (levels[14] * (iLevel - 13));
        }

        public static string AlignmentString(Wiz1Alignment align) { return Wiz1Item.GetAlignmentString(align); }

        public static string RaceString(Wiz1Race race)
        {
            switch (race)
            {
                case Wiz1Race.None: return "None";
                case Wiz1Race.Dwarf: return "Dwarf";
                case Wiz1Race.Elf: return "Elf";
                case Wiz1Race.Gnome: return "Gnome";
                case Wiz1Race.Hobbit: return "Hobbit";
                case Wiz1Race.Human: return "Human";
                default: return String.Format("Unknown({0})", (int)race);
            }
        }
        public static string ClassString(Wiz1Class classenum)
        {
            switch (classenum)
            {
                case Wiz1Class.Fighter: return "Fighter";
                case Wiz1Class.Mage: return "Mage";
                case Wiz1Class.Priest: return "Priest";
                case Wiz1Class.Thief: return "Thief";
                case Wiz1Class.Bishop: return "Bishop";
                case Wiz1Class.Samurai: return "Samurai";
                case Wiz1Class.Lord: return "Lord";
                case Wiz1Class.Ninja: return "Ninja";
                default: return String.Format("Unknown({0})", (int)classenum);
            }
        }

        public static string ConditionString(Wiz1Condition cond) { return ConditionString(cond, String.Empty, true); }

        public static string ConditionString(Wiz1Condition cond, string strExtra, bool bIncludeGood)
        {
            if (cond == Wiz1Condition.Good)
            {
                if (!String.IsNullOrWhiteSpace(strExtra))
                    return strExtra;
                return bIncludeGood ? "Good" : "";
            }

            if (!String.IsNullOrWhiteSpace(strExtra))
                strExtra = ", " + strExtra;

            switch (cond)
            {
                case Wiz1Condition.Good: return "Good" + strExtra;
                case Wiz1Condition.Afraid: return "Afraid" + strExtra;
                case Wiz1Condition.Asleep: return "Asleep" + strExtra;
                case Wiz1Condition.Paralyzed: return "Paralyzed" + strExtra;
                case Wiz1Condition.Petrified: return "Petrified" + strExtra;
                case Wiz1Condition.Dead: return "Dead" + strExtra;
                case Wiz1Condition.Ashes: return "Ashes" + strExtra;
                case Wiz1Condition.Lost: return "Lost" + strExtra;
                default: return String.Format("Unknown({0})" + strExtra, (int)cond);
            }
        }

        public static StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            switch (stat)
            {
                case PrimaryStat.Strength:
                    return StatModifier.FromTable(value, stat, 4, -3, 5, -2, 6, -1, 16, 0, 17, 1, 18, 2, 3);
                case PrimaryStat.IQ:
                case PrimaryStat.Piety:
                    return StatModifier.FromTable(value, stat, 1, 0, 2, 3, 3, 7, 4, 10, 5, 13, 6, 17, 7, 20, 8, 23, 9, 27,
                        10, 30, 11, 33, 12, 37, 13, 40, 14, 43, 15, 47, 16, 50, 17, 53, 18, 57, 60);
                case PrimaryStat.Agility:
                    return StatModifier.FromTable(value, stat, 4, 3, 6, 2, 8, 1, 15, 0, 16, -1, 17, -2, 18, -3, -4);
                case PrimaryStat.Vitality:
                    return StatModifier.FromTable(value, stat, 4, -2, 6, -1, 16, 0, 17, 1, 18, 2, 3);
                case PrimaryStat.Luck:
                    return StatModifier.FromTable(value, stat, 6, 0, 12, -1, 18, -2, -3);
                default:
                    return StatModifier.Zero;
            }
        }

        public int NumAttacks
        {
            get
            {
                int iNumAttacks = 1;
                switch (Class)
                {
                    case Wiz1Class.Fighter:
                    case Wiz1Class.Priest:
                    case Wiz1Class.Samurai:
                    case Wiz1Class.Lord:
                        iNumAttacks = Level / 5 + 1;
                        break;
                    case Wiz1Class.Ninja:
                        iNumAttacks = Level / 5 + 2;
                        break;
                }
                return iNumAttacks;
            }
        }

        public static string ConditionDescription(Wiz1Condition cond, Wiz1Character wizChar = null)
        {
            string strExtra = wizChar == null ? String.Empty : wizChar.GetExtraConditionDesc();
            if (cond == Wiz1Condition.Good)
            {
                if (String.IsNullOrWhiteSpace(strExtra))
                    return "Good: Character is healthy";
                return strExtra;
            }

            strExtra = "\r\n" + strExtra;
            // Wizardry 1 conditions are not flags; you may only have one at a time (except Poison, which is stored elsewhere)
            switch (cond)
            {
                case Wiz1Condition.Good: 
                case Wiz1Condition.Afraid: 
                    if (wizChar == null)
                        return "Afraid: No known effect"+ strExtra;
                    return String.Format("Afraid: No known effect, recovery chance {0}%/round", Math.Min(50, 5 * wizChar.Level)) + strExtra;
                case Wiz1Condition.Asleep:
                    if (wizChar == null)
                        return "Asleep: Cannot perform actions until attacked";
                    return String.Format("Asleep: Cannot perform actions until attacked, recovery chance {0}%/round", Math.Min(50, 10 * wizChar.Level)) + strExtra;
                case Wiz1Condition.Dead: return "Dead: Cannot perform actions and gains no XP" + strExtra;
                case Wiz1Condition.Lost: return "Lost: Character cannot be recovered without cheating" + strExtra;
                case Wiz1Condition.Paralyzed: return "Paralyzed: Cannot perform actions" + strExtra;
                case Wiz1Condition.Petrified: return "Stone: Cannot perform actions and gains no XP" + strExtra;
                case Wiz1Condition.Ashes: return "Ashes: Cannot perform actions and gains no XP" + strExtra;
                default: return String.Format("Unknown Condition({0})" + strExtra, (int)cond);
            }
        }

        public override string AttributesString
        {
            get
            {
                return String.Format("Str:{0}, IQ:{1}, Pie:{2}, Vit:{3}, Agi:{4}, Lck:{5}",
                    Strength.ToString(),
                    IQ.ToString(),
                    Piety.ToString(),
                    Vitality.ToString(),
                    Agility.ToString(),
                    Luck.ToString());
            }
        }

        public override string ExperienceString
        {
            get
            {
                if (Level >= MaxLevel)
                    return String.Format("{0} (Max Level)", Experience);
                return String.Format("{0}{1}", Experience, ReadyToTrain ? " (Train!)" : ("/" + XPForNextLevel.ToString()));
            }
        }

        public override int TrainableLevel
        {
            get
            {
                int iLevel = Level;
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

        public static int MaxHPPerLevel(Wiz1Class wiz1Class)
        {
            switch (wiz1Class)
            {
                case Wiz1Class.Fighter: return 10;
                case Wiz1Class.Mage: return 4;
                case Wiz1Class.Priest: return 8;
                case Wiz1Class.Thief: return 6;
                case Wiz1Class.Bishop: return 6;
                case Wiz1Class.Samurai: return 8;
                case Wiz1Class.Lord: return 10;
                case Wiz1Class.Ninja: return 6;
                default:
                    return 0;
            }
        }

        public string HPLevelString
        {
            get
            {
                int iBase = MaxHPPerLevel(Class);
                int iBonus = Wiz1Character.GetStatModifier(Vitality, PrimaryStat.Vitality).Value;

                return String.Format("{0} - {1}", iBonus + 1, iBonus + iBase);
            }
        }

        public string EquippedString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach(Wiz1Item item in Inventory.SelectEquippedItems)
                    sb.AppendFormat("{0}, ", (item.IsIdentified || !Properties.Settings.Default.HideUnidentifiedItems) ? item.Name : String.Format("Unidentified {0}", item.ItemNoun));
                Global.Trim(sb);
                if (sb.Length == 0)
                    return "(nothing)";
                return sb.ToString();
            }
        }

        public string BackpackString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (Wiz1Item item in Inventory.SelectUnequippedItems)
                    sb.AppendFormat("{0}, ", (item.IsIdentified || !Properties.Settings.Default.HideUnidentifiedItems) ? item.Name : String.Format("Unidentified {0}", item.ItemNoun));
                Global.Trim(sb);
                if (sb.Length == 0)
                    return "(empty)";
                return sb.ToString();
            }
        }

        public static string AgeString(int age)
        {
            // Age is in weeks
            return String.Format("{0}, {1}", Global.Plural(age / 52, "year"), Global.Plural(age % 52, "week"));
        }

        public override string BasicInfoString
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Name))
                    return "<Invalid Character Record>";
                return String.Format("Level {0} {1} {2} {3}, {4} old",
                 Level.ToString(),
                 Wiz1Character.AlignmentString(Alignment),
                 Wiz1Character.RaceString(Race),
                 Wiz1Character.ClassString(Class),
                 Wiz1Character.AgeString(Age));
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
                sb.AppendFormat("Condition: {0}\n", Wiz1Character.ConditionString(Condition, GetExtraConditions(), true));
                sb.AppendFormat("HP: {0}\n", HitPoints.ToString());
                sb.AppendFormat("SP: {0}\n", SpellPoints.ToString());
                sb.AppendFormat("AC: {0}\n", ArmorClass);
                sb.AppendFormat("Equipped: {0}\n", EquippedString);
                sb.AppendFormat("Backpack: {0}\n", BackpackString);
                sb.AppendFormat("Gold: {0}\n", Gold);
                return sb.ToString();
            }
        }

        public override StatAndModifier BasicLevel { get { return new StatAndModifier(Level, LevelMod - Level); } }
        public override StatAndModifier BasicAC { get { return new StatAndModifier(LastArmorClass, ArmorClass - LastArmorClass); } }

        public override GenericClass BasicClass
        {
            get
            {
                switch (Class)
                {
                    case Wiz1Class.Bishop: return GenericClass.Bishop;
                    case Wiz1Class.Fighter: return GenericClass.Fighter;
                    case Wiz1Class.Lord: return GenericClass.Lord;
                    case Wiz1Class.Mage: return GenericClass.Mage;
                    case Wiz1Class.Ninja: return GenericClass.Ninja;
                    case Wiz1Class.Priest: return GenericClass.Priest;
                    case Wiz1Class.Samurai: return GenericClass.Samurai;
                    case Wiz1Class.Thief: return GenericClass.Thief;
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
                    case Wiz1Race.Human: return GenericRace.Human;
                    case Wiz1Race.Elf: return GenericRace.Elf;
                    case Wiz1Race.Gnome: return GenericRace.Gnome;
                    case Wiz1Race.Dwarf: return GenericRace.Dwarf;
                    case Wiz1Race.Hobbit: return GenericRace.Hobbit;
                    default: return GenericRace.None;
                }
            }
        }

        public override GenericAge BasicAge { get { return new GenericAge(Age / 52, (Age % 52) * 7); } }

        public override GenericAlignment BasicAlignment
        {
            get
            {
                return new GenericAlignment(BasicAlignmentValue(true), BasicAlignmentValue(true)); 
            }
        }

        public static GenericAlignmentValue GetGenericAlignment(Wiz1Alignment alignment)
        {
            switch (alignment)
            {
                case Wiz1Alignment.Good: return GenericAlignmentValue.Good;
                case Wiz1Alignment.Neutral: return GenericAlignmentValue.Neutral;
                case Wiz1Alignment.Evil: return GenericAlignmentValue.Evil;
                default: return GenericAlignmentValue.None;
            }
        }

        public GenericAlignmentValue BasicAlignmentValue(bool bTemporary)
        {
            return GetGenericAlignment(Alignment);
        }

        public override long QuickRefExperience { get { return Experience; } }
        public override MMHitPoints QuickRefHitPoints { get { return HitPoints; } }
        public override string QuickRefCondition { get { return Wiz1Character.ConditionString(Condition, GetExtraConditions(), false); } }
        public override bool IsHealer { get { return true; } }   // Any Wizard character may know spells from a prior class

        public override BasicConditionFlags BasicCondition { get { return GetBasicCondition(Condition); } }

        public static BasicConditionFlags GetBasicCondition(Wiz1Condition wiz1Condition)
        {
            BasicConditionFlags cond = BasicConditionFlags.Good;

            if (wiz1Condition.HasFlag(Wiz1Condition.Lost))
                cond |= BasicConditionFlags.Lost;
            if (wiz1Condition.HasFlag(Wiz1Condition.Ashes))
                cond |= BasicConditionFlags.Eradicated;
            if (wiz1Condition.HasFlag(Wiz1Condition.Dead))
                cond |= BasicConditionFlags.Dead;
            if (wiz1Condition.HasFlag(Wiz1Condition.Petrified))
                cond |= BasicConditionFlags.Stone;
            if (wiz1Condition.HasFlag(Wiz1Condition.Paralyzed))
                cond |= BasicConditionFlags.Paralyzed;
            if (wiz1Condition.HasFlag(Wiz1Condition.Asleep))
                cond |= BasicConditionFlags.Asleep;
            if (wiz1Condition.HasFlag(Wiz1Condition.Afraid))
                cond |= BasicConditionFlags.Afraid;

            return cond;
        }

        public override byte ConditionValue(BasicConditionFlags condition)
        {
            if (condition.HasFlag(BasicConditionFlags.Lost))
                return (byte)Wiz1Condition.Lost;
            if (condition.HasFlag(BasicConditionFlags.Eradicated))
                return (byte)Wiz1Condition.Ashes;
            if (condition.HasFlag(BasicConditionFlags.Dead))
                return (byte)Wiz1Condition.Dead;
            if (condition.HasFlag(BasicConditionFlags.Stone))
                return (byte)Wiz1Condition.Petrified;
            if (condition.HasFlag(BasicConditionFlags.Paralyzed))
                return (byte)Wiz1Condition.Paralyzed;
            if (condition.HasFlag(BasicConditionFlags.Asleep))
                return (byte)Wiz1Condition.Asleep;
            if (condition.HasFlag(BasicConditionFlags.Afraid))
                return (byte)Wiz1Condition.Afraid;

            return (byte)Wiz1Condition.Good;
        }

        public override byte AlignmentValue(GenericAlignmentValue align)
        {
            switch (align)
            {
                case GenericAlignmentValue.Evil: return (byte)Wiz1Alignment.Evil;
                case GenericAlignmentValue.Neutral: return (byte)Wiz1Alignment.Neutral;
                case GenericAlignmentValue.Good: return (byte)Wiz1Alignment.Good;
                default: return (byte)Wiz1Alignment.None;
            }
        }

        public override byte RaceValue(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return (byte)Wiz1Race.Human;
                case GenericRace.Elf: return (byte)Wiz1Race.Elf;
                case GenericRace.Dwarf: return (byte)Wiz1Race.Dwarf;
                case GenericRace.Gnome: return (byte)Wiz1Race.Gnome;
                case GenericRace.Hobbit: return (byte)Wiz1Race.Hobbit;
                default: return (byte)Wiz1Race.None;
            }
        }

        public static Wiz1Class ClassForGeneric(GenericClass gClass)
        {
            switch (gClass)
            {
                case GenericClass.Fighter: return Wiz1Class.Fighter;
                case GenericClass.Mage: return Wiz1Class.Mage;
                case GenericClass.Priest: return Wiz1Class.Priest;
                case GenericClass.Thief: return Wiz1Class.Thief;
                case GenericClass.Bishop: return Wiz1Class.Bishop;
                case GenericClass.Samurai: return Wiz1Class.Samurai;
                case GenericClass.Lord: return Wiz1Class.Lord;
                case GenericClass.Ninja: return Wiz1Class.Ninja;
                default: return Wiz1Class.None;
            }
        }

        public override byte ClassValue(GenericClass classVal)
        {
            return (byte)ClassForGeneric(classVal);
        }

        public override Item GetItem(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset< 8)
                return null;

            if (bytes[offset + 6] >= Wiz1.Items.Count)
                return null;

            Wiz1Item item = Wiz1.Items[bytes[offset + 6]].Clone() as Wiz1Item;
            item.Equipped = bytes[offset] != 0;
            item.InvCursed = bytes[offset + 2] != 0;
            item.Identified = bytes[offset + 4] != 0;

            return item;
        }

        public override GameNames Game { get { return GameNames.Wizardry1; } }

        public override int FirstEmptyBackpackIndex
        {
            get
            {
                // Wizardry items are always stored in order
                if (Inventory == null)
                    return -1;
                if (Inventory.Items.Count > 8)
                    return -1;
                return Inventory.Items.Count;
            }
        }

        public override bool BackpackFull
        {
            get
            {
                return (FirstEmptyBackpackIndex == -1);
            }
        }

        public override int MaxLevel { get { return Int16.MaxValue; } }
    }

    public class CheatTag
    {
        public long Maximum;
        public long Minimum;

        public byte ByteMax { get { return (byte)Maximum; } }
        public Int16 Int16Max { get { return (Int16)Maximum; } }
        public UInt16 UInt16Max { get { return (UInt16)Maximum; } }
        public Int32 Int32Max { get { return (Int32)Maximum; } }
        public UInt32 UInt32Max { get { return (UInt32)Maximum; } }

        public byte ByteMin { get { return (byte)Minimum; } }
        public Int16 Int16Min { get { return (Int16)Minimum; } }
        public UInt16 UInt16Min { get { return (UInt16)Minimum; } }
        public Int32 Int32Min { get { return (Int32)Minimum; } }
        public UInt32 UInt32Min { get { return (UInt32)Minimum; } }

        public CheatTag(long max = 0, long min = 0)
        {
            Maximum = max;
            Minimum = min;
        }
    }

    public class WizardryCheatTag : CheatTag
    {
        public PackedStat Stat;

        public bool IsWizLong { get { return Stat == PackedStat.Gold || Stat == PackedStat.Experience; } }
        public bool IsFiveBitStat
        {
            get
            {
                switch (Stat)
                {
                    case PackedStat.Strength:
                    case PackedStat.IQ:
                    case PackedStat.Piety:
                    case PackedStat.Vitality:
                    case PackedStat.Agility:
                    case PackedStat.Luck:
                    case PackedStat.SaveVsPoison:
                    case PackedStat.SaveVsPetrify:
                    case PackedStat.SaveVsWand:
                    case PackedStat.SaveVsBreath:
                    case PackedStat.SaveVsSpell:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public int StatOffset
        {
            get
            {
                switch (Stat)
                {
                    case PackedStat.Strength:
                    case PackedStat.IQ:
                    case PackedStat.Piety:
                    case PackedStat.Vitality:
                    case PackedStat.Agility:
                    case PackedStat.Luck:
                        return Stat - PackedStat.Strength;
                    case PackedStat.SaveVsPoison:
                    case PackedStat.SaveVsPetrify:
                    case PackedStat.SaveVsWand:
                    case PackedStat.SaveVsBreath:
                    case PackedStat.SaveVsSpell:
                        return Stat - PackedStat.SaveVsPoison;
                    default:
                        return 0;
                }
            }
        }

        public WizardryCheatTag(PackedStat stat, long max = 0, long min = 0)
        {
            Stat = stat;
            Maximum = max;
            Minimum = min;
        }
    }
}
