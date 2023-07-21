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
    public partial class MMCharacterInfoControl : CharacterInfoControl
    {
        protected MMCharCommonControls MMCommonControls { get { return m_commonCtrls as MMCharCommonControls; } }

        public MMCharacterInfoControl()
        {
            InitializeComponent();

            SetCommonControls();
        }

        public MMCharacterInfoControl(IMain main) : base(main)
        {
            InitializeComponent();

            SetCommonControls();

            SetMain(main);
        }

        protected override void SetCommonControls()
        {
            if (m_commonCtrls == null)
                base.SetCommonControls();

            MMCharCommonControls mmcc = new MMCharCommonControls();
            mmcc.CopyFrom(m_commonCtrls);
            mmcc.groupPrimaryStats = groupPrimaryStats;
            mmcc.labelIntellect = labelIntellect;
            mmcc.labelMight = labelMight;
            mmcc.labelPersonality = labelPersonality;
            mmcc.labelEndurance = labelEndurance;
            mmcc.labelSpeed = labelSpeed;
            mmcc.labelAccuracy = labelAccuracy;
            mmcc.labelLuck = labelLuck;
            mmcc.labelRanged = labelRanged;
            mmcc.labelThievery = labelThievery;

            SetInventorySize(true);
            m_commonCtrls = mmcc;
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
                        case 0: return new TriggerControl(labelMight);
                        case 1: return new TriggerControl(labelIntellect);
                        case 2: return new TriggerControl(labelPersonality);
                        case 3: return new TriggerControl(labelEndurance);
                        case 4: return new TriggerControl(labelSpeed);
                        case 5: return new TriggerControl(labelAccuracy);
                        case 6: return new TriggerControl(labelLuck);
                        default: return base.GetTriggerControl(entity, strVal);
                    }
                case TriggerEntity.StatNamed:
                    if (strVal == null || strVal.Length < 1)
                        return base.GetTriggerControl(entity, strVal);
                    switch (Char.ToLower(strVal[0]))
                    {
                        case 'm': return new TriggerControl(labelMight);
                        case 'i': return new TriggerControl(labelIntellect);
                        case 'p': return new TriggerControl(labelPersonality);
                        case 'e': return new TriggerControl(labelEndurance);
                        case 's': return new TriggerControl(labelSpeed);
                        case 'a': return new TriggerControl(labelAccuracy);
                        case 'l': return new TriggerControl(labelLuck);
                        default: return base.GetTriggerControl(entity, strVal);
                    }
                case TriggerEntity.Thievery: return new TriggerControl(labelThievery);
                case TriggerEntity.RangedDamageAverage: return new TriggerControl(labelRanged);
                default: return base.GetTriggerControl(entity, strVal);
            }
        }

        protected override CheatMenuFlags PrepareCheatMenu(Control label, CheatMenuFlags flags = CheatMenuFlags.None)
        {
            if (!(label is EditableAttributeLabel || label is MMItemLabel || label is ListView))
                return CheatMenuFlags.None;

            bool bMM12 = (m_char.Game == GameNames.MightAndMagic1 || m_char.Game == GameNames.MightAndMagic2);

            CheatMenuFlags menuFlags = CheatMenuFlags.AllNonlevel;

            m_cheatOffsets = null;

            m_cheatType = AttributeType.TwoUInt8;

            if (label == MMCommonControls.labelIntellect)
                m_cheatOffsets = new int[] { m_char.Offsets.Intellect, m_char.Offsets.IntellectMod };
            else if (label == MMCommonControls.labelMight)
                m_cheatOffsets = new int[] { m_char.Offsets.Might, m_char.Offsets.MightMod };
            else if (label == MMCommonControls.labelPersonality)
                m_cheatOffsets = new int[] { m_char.Offsets.Personality, m_char.Offsets.PersonalityMod };
            else if (label == MMCommonControls.labelEndurance)
                m_cheatOffsets = new int[] { m_char.Offsets.Endurance, m_char.Offsets.EnduranceMod };
            else if (label == MMCommonControls.labelSpeed)
                m_cheatOffsets = new int[] { m_char.Offsets.Speed, m_char.Offsets.SpeedMod };
            else if (label == MMCommonControls.labelAccuracy)
                m_cheatOffsets = new int[] { m_char.Offsets.Accuracy, m_char.Offsets.AccuracyMod };
            else if (label == MMCommonControls.labelLuck)
                m_cheatOffsets = new int[] { m_char.Offsets.Luck, m_char.Offsets.LuckMod };

            if (m_cheatOffsets == null)
            {
                m_cheatType = AttributeType.UInt8;

                if (label == MMCommonControls.labelThievery && bMM12)
                    m_cheatOffsets = new int[] { m_char.Offsets.Thievery };
            }
            if (label == m_commonCtrls.labelExp)
            {
                menuFlags |= CheatMenuFlags.NextLevel;
                m_cheatType = AttributeType.UInt32;
                m_cheatOffsets = new int[] { m_char.Offsets.Experience };
            }
            else if (label == m_commonCtrls.labelAC)
            {
                if (bMM12)
                {
                    m_cheatType = AttributeType.TwoUInt8;
                    m_cheatOffsets = new int[] { m_char.Offsets.ArmorClass, m_char.Offsets.ArmorClassMod };
                }
                else
                {
                    m_cheatType = AttributeType.UInt8;
                    m_cheatOffsets = new int[] { m_char.Offsets.ArmorClassMod };
                }
            }
            else if (label == m_commonCtrls.labelSP)
            {
                if (bMM12)
                {
                    m_cheatType = AttributeType.TwoUInt16;
                    m_cheatOffsets = new int[] { m_char.Offsets.CurrentSP, m_char.Offsets.MaxSP };
                }
                else
                {
                    m_cheatType = AttributeType.Int16;
                    m_cheatOffsets = new int[] { m_char.Offsets.CurrentSP };
                }
            }
            else if (label == m_commonCtrls.labelHP)
            {
                if (bMM12)
                {
                    m_cheatType = AttributeType.ThreeUInt16;
                    m_cheatOffsets = new int[] { m_char.Offsets.CurrentHP, m_char.Offsets.MaxHP, m_char.Offsets.MaxHPMod };
                }
                else
                {
                    m_cheatType = AttributeType.Int16;
                    m_cheatOffsets = new int[] { m_char.Offsets.CurrentHP };
                }
            }
            else if (label == m_commonCtrls.labelLevel)
            {
                m_cheatType = AttributeType.LevSexAlignRaceClass;
                menuFlags |= CheatMenuFlags.SuperChar;
                m_cheatOffsets = new int[] {
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
                };
            }
            else if (label == m_commonCtrls.labelCondition)
            {
                m_cheatType = AttributeType.Condition;
                menuFlags = CheatMenuFlags.Edit;
                m_cheatOffsets = Global.IntRange(m_char.Offsets.Condition, m_char.Offsets.ConditionLength);
            }
            else if (m_cheatOffsets == null)
            {
                m_cheatType = AttributeType.Item;
                menuFlags = CheatMenuFlags.Edit;

                if (label == m_commonCtrls.lvResistances && m_commonCtrls.lvResistances.FocusedItem != null)
                {
                    ResistanceValue rv = m_commonCtrls.lvResistances.FocusedItem.Tag as ResistanceValue;
                    m_cheatType = AttributeType.TwoUInt8;
                    menuFlags = CheatMenuFlags.AllNonlevel;

                    switch (rv.Resistance)
                    {
                        case GenericResistanceFlags.Cold:
                            m_cheatOffsets = new int[] { m_char.Offsets.ColdResist, m_char.Offsets.ColdResistMod };
                            break;
                        case GenericResistanceFlags.Electricity:
                            m_cheatOffsets = new int[] { m_char.Offsets.ElecResist, m_char.Offsets.ElecResistMod };
                            break;
                        case GenericResistanceFlags.Fire:
                            m_cheatOffsets = new int[] { m_char.Offsets.FireResist, m_char.Offsets.FireResistMod };
                            break;
                        case GenericResistanceFlags.Magic:
                            m_cheatOffsets = new int[] { m_char.Offsets.MagicResist, m_char.Offsets.MagicResistMod };
                            break;
                        case GenericResistanceFlags.Poison:
                            m_cheatOffsets = new int[] { m_char.Offsets.PoisonResist, m_char.Offsets.PoisonResistMod };
                            break;
                        case GenericResistanceFlags.Energy:
                            m_cheatOffsets = new int[] { m_char.Offsets.EnergyResist, m_char.Offsets.EnergyResistMod };
                            break;
                        case GenericResistanceFlags.Fear:
                            m_cheatOffsets = new int[] { m_char.Offsets.FearResist, m_char.Offsets.FearResistMod };
                            break;
                        case GenericResistanceFlags.Sleep:
                            m_cheatOffsets = new int[] { m_char.Offsets.SleepResist, m_char.Offsets.SleepResistMod };
                            break;
                        case GenericResistanceFlags.Acid:
                            m_cheatOffsets = new int[] { m_char.Offsets.AcidResist, m_char.Offsets.AcidResistMod };
                            break;
                        case GenericResistanceFlags.Blessed:
                            m_cheatType = AttributeType.UInt8;
                            m_cheatOffsets = new int[] { m_char.Offsets.Blessed };
                            break;
                        case GenericResistanceFlags.HolyBonus:
                            m_cheatType = AttributeType.UInt8;
                            m_cheatOffsets = new int[] { m_char.Offsets.HolyBonus };
                            break;
                        case GenericResistanceFlags.PowerShield:
                            m_cheatType = AttributeType.UInt8;
                            m_cheatOffsets = new int[] { m_char.Offsets.PowerShield };
                            break;
                        case GenericResistanceFlags.Heroism:
                            m_cheatType = AttributeType.UInt8;
                            m_cheatOffsets = new int[] { m_char.Offsets.Heroism };
                            break;
                        default:
                            m_cheatOffsets = null;
                            break;
                    }
                }
            }

            if (m_cheatOffsets == null)
                return CheatMenuFlags.None;

            if (m_cheatOffsets[0] < 0)        // Offset refers to an item that doesn't exist in this game's character record
                return CheatMenuFlags.None;

            return menuFlags;
        }

        public IEnumerable<int> StatPositionY()
        {
            yield return labelStatPos0.Location.Y;
            yield return labelStatPos1.Location.Y;
            yield return labelStatPos2.Location.Y;
            yield return labelStatPos3.Location.Y;
            yield return labelStatPos4.Location.Y;
            yield return labelStatPos5.Location.Y;
            yield return labelStatPos6.Location.Y;
        }

        public override void SetStatOrder(PrimaryStat[] stats)
        {
            if (Global.Compare(stats, m_lastStatOrder))
                return; // no changes

            m_lastStatOrder = stats;

            IEnumerator<int> yPositions = StatPositionY().GetEnumerator();
            yPositions.MoveNext();

            int iHeaderX = labelMightHeader.Location.X;
            int iValueX = labelMight.Location.X;

            foreach (PrimaryStat stat in stats)
            {
                switch (stat)
                {
                    case PrimaryStat.Might:
                        labelMightHeader.Location = new Point(iHeaderX, yPositions.Current);
                        labelMight.Location = new Point(iValueX, yPositions.Current);
                        break;
                    case PrimaryStat.Intellect:
                        labelIntellectHeader.Location = new Point(iHeaderX, yPositions.Current);
                        labelIntellect.Location = new Point(iValueX, yPositions.Current);
                        break;
                    case PrimaryStat.Personality:
                        labelPersonalityHeader.Location = new Point(iHeaderX, yPositions.Current);
                        labelPersonality.Location = new Point(iValueX, yPositions.Current);
                        break;
                    case PrimaryStat.Endurance:
                        labelEnduranceHeader.Location = new Point(iHeaderX, yPositions.Current);
                        labelEndurance.Location = new Point(iValueX, yPositions.Current);
                        break;
                    case PrimaryStat.Speed:
                        labelSpeedHeader.Location = new Point(iHeaderX, yPositions.Current);
                        labelSpeed.Location = new Point(iValueX, yPositions.Current);
                        break;
                    case PrimaryStat.Accuracy:
                        labelAccuracyHeader.Location = new Point(iHeaderX, yPositions.Current);
                        labelAccuracy.Location = new Point(iValueX, yPositions.Current);
                        break;
                    case PrimaryStat.Luck:
                        labelLuckHeader.Location = new Point(iHeaderX, yPositions.Current);
                        labelLuck.Location = new Point(iValueX, yPositions.Current);
                        break;
                }
                yPositions.MoveNext();
            }
        }

        protected override void Stat_MouseLeave(object sender, EventArgs e)
        {
            base.Stat_MouseLeave(sender, e);
        }

        private void labelMight_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Might, MMCommonControls.labelMight); }
        private void labelIntellect_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Intellect, MMCommonControls.labelIntellect); }
        private void labelPersonality_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Personality, MMCommonControls.labelPersonality); }
        private void labelEndurance_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Endurance, MMCommonControls.labelEndurance); }
        private void labelSpeed_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Speed, MMCommonControls.labelSpeed); }
        private void labelAccuracy_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Accuracy, MMCommonControls.labelAccuracy); }
        private void labelLuck_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Luck, MMCommonControls.labelLuck); }
        private void labelAC_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.ArmorClass, m_commonCtrls.labelAC); }
        private void labelThievery_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Thievery, MMCommonControls.labelThievery); }
        private void labelMelee_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.MeleeDamage, m_commonCtrls.labelMelee); }
        private void labelRanged_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.RangedDamage, MMCommonControls.labelRanged); }
        private void labelHP_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.HitPoints, MMCommonControls.labelHP); }
        private void labelSP_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.SpellPoints, MMCommonControls.labelSP); }

        private void labelIntellectHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Intellect, labelIntellectHeader); }
        private void labelMightHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Might, labelMightHeader); }
        private void labelPersonalityHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Personality, labelPersonalityHeader); }
        private void labelEnduranceHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Endurance, labelEnduranceHeader); }
        private void labelSpeedHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Speed, labelSpeedHeader); }
        private void labelAccuracyHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Accuracy, labelAccuracyHeader); }
        private void labelLuckHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Luck, labelLuckHeader); }
        private void labelThieveryHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Thievery, labelThieveryHeader); }
        private void labelRangedHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.RangedDamage, labelRangedHeader); }
    }

    public abstract class MMBaseCharacter : BaseCharacter
    {
        public override bool IsCaster { get { return IsCasterClass(BasicClass); } }

        public static bool IsCasterClass(GenericClass gc)
        {
            switch(gc)
            {
                case GenericClass.Barbarian:
                case GenericClass.Knight:
                case GenericClass.Robber:
                case GenericClass.Ninja:
                case GenericClass.None:
                    return false;
                default:
                    return true;
            }
        }

        public virtual string IntellectEffect() { return String.Empty; }
        public virtual string IntellectEffect(StatModifier mod, double perLev) { return mod.TipString("{0} Intellect: {1} SP per level ({2})", perLev); }

        public virtual string PersonalityEffect() { return String.Empty; }
        public virtual string PersonalityEffect(StatModifier mod, double perLev) { return mod.TipString("{0} Personality: {1} SP per level ({2})", perLev); }

        public virtual int CurrentMight { get { return BasicMight.Temporary + Modifiers.Might; } }
        public virtual int CurrentIntellect { get { return BasicIntellect.Temporary + Modifiers.Intellect; } }
        public virtual int CurrentPersonality { get { return BasicPersonality.Temporary + Modifiers.Personality; } }
        public virtual int CurrentEndurance { get { return BasicEndurance.Temporary + Modifiers.Endurance; } }
        public virtual int CurrentSpeed { get { return BasicSpeed.Temporary + Modifiers.Speed; } }
        public virtual int CurrentAccuracy { get { return BasicAccuracy.Temporary + Modifiers.Accuracy; } }
        public virtual int CurrentLuck { get { return BasicLuck.Temporary + Modifiers.Luck; } }

        public override StatAndModifier[] PrimaryStats
        {
            get { return new StatAndModifier[] { BasicMight, BasicIntellect, BasicPersonality, BasicEndurance, BasicSpeed, BasicAccuracy, BasicLuck }; }
        }

        public virtual StatAndModifier BasicIntellect { get { return null; } }
        public virtual StatAndModifier BasicMight { get { return null; } }
        public virtual StatAndModifier BasicPersonality { get { return null; } }
        public virtual StatAndModifier BasicEndurance { get { return null; } }
        public virtual StatAndModifier BasicSpeed { get { return null; } }
        public virtual StatAndModifier BasicAccuracy { get { return null; } }
        public virtual StatAndModifier BasicLuck { get { return null; } }

        public override StatAndModifier Stat(string str)
        {
            if (str == null || str.Length < 1)
                return StatAndModifier.Zero;
            switch (Char.ToLower(str[0]))
            {
                case 'm': return BasicMight;
                case 'i': return BasicIntellect;
                case 'p': return BasicPersonality;
                case 'e': return BasicEndurance;
                case 's': return BasicSpeed;
                case 'a': return BasicAccuracy;
                case 'l': return BasicLuck;
                default: return StatAndModifier.Zero;
            }
        }

        public override string AttributeTip(ModAttr attrib, MemoryHacker hacker)
        {
            string strBase = base.AttributeTip(attrib, hacker);
            if (!String.IsNullOrWhiteSpace(strBase))
                return strBase;

            switch (attrib)
            {
                case ModAttr.Might: return GetModifier(CurrentMight, PrimaryStat.Might).TipString("{0} Might: {1} to melee combat damage ({2})");
                case ModAttr.Intellect: return IntellectEffect();
                case ModAttr.Personality: return PersonalityEffect();
                case ModAttr.Endurance: return GetModifier(CurrentEndurance, PrimaryStat.Endurance).TipString("{0} Endurance: {1} HP per level ({2})");
                case ModAttr.Speed: return GetModifier(CurrentSpeed, PrimaryStat.Speed).TipString("{0} Speed: {1} to Armor Class ({2})");
                case ModAttr.Accuracy: return GetModifier(CurrentAccuracy, PrimaryStat.Accuracy).TipString("{0} Accuracy: {1} To-Hit ({2})");
                case ModAttr.Luck: return String.Empty;
                default: return String.Empty;
            }
        }
    }

    public class MMCharCommonControls : CharCommonControls
    {
        public GroupBox groupPrimaryStats;
        public Label labelIntellect;
        public Label labelMight;
        public Label labelPersonality;
        public Label labelEndurance;
        public Label labelSpeed;
        public Label labelAccuracy;
        public Label labelLuck;
        public Label labelRanged;
        public Label labelThievery;
    }
}