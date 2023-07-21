using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class BasicCharInfoEditForm : Form
    {
        private CharBasicInfo m_charBasicInfo;

        public BasicCharInfoEditForm()
        {
            InitializeComponent();
        }

        public CharBasicInfo BasicInfo
        {
            get
            {
                UpdateFromUI();
                return m_charBasicInfo;
            }

            set
            {
                m_charBasicInfo = value;
                UpdateUI();
            }
        }

        public void SetClasses(GenericClass start, GenericClass end)
        {
            comboClass.Items.Clear();
            for (GenericClass c = start; c <= end; c++)
            {
                comboClass.Items.Add(new GenericClassTag(c));
            }
        }

        public void SetSingleClasses(params GenericClass[] classes)
        {
            comboClass.Items.Clear();
            foreach (GenericClass c in classes)
                comboClass.Items.Add(new GenericClassTag(c));
        }

        public void UpdateUI()
        {
            tbName.MaxLength = Games.MaxNameLength(m_charBasicInfo.Game);
            comboRace.Items.Clear();
            comboRace.Items.AddRange(Games.Races(m_charBasicInfo.Game));
            bool bAlign = true;
            BasicInfoStyle style = Games.GetInfoType(m_charBasicInfo.Game);
            switch (style)
            {
                case BasicInfoStyle.PermanentWithTemp:  // MM1/2
                    SetClasses(GenericClass.Knight, m_charBasicInfo.Game == GameNames.MightAndMagic1 ? GenericClass.Robber : GenericClass.Ninja);
                    nudLevelTemp.Value = m_charBasicInfo.Level.Temporary;
                    Global.SetNud(nudAgeDays, m_charBasicInfo.Age.Days);
                    break;
                case BasicInfoStyle.PermanentWithMod:  // MM3
                case BasicInfoStyle.PermanentWithModNoAlign:  // MM4-5
                    SetClasses(GenericClass.Knight, GenericClass.Ranger);
                    labelLevelCurrent.Text = "(modifier)";
                    labelAlignCurrent.Visible = false;
                    comboAlignTemp.Visible = false;
                    bAlign = style == BasicInfoStyle.PermanentWithMod;
                    labelAlignPerm.Visible = bAlign;
                    comboAlignPermanent.Visible = bAlign;
                    labelAlignment.Visible = bAlign;
                    labelAge.Text = "Born";
                    labelBirthDay.Text = "day";
                    labelBirthYear.Text = "year";
                    nudAgeYears.Maximum = 65535;
                    Global.SetNud(nudLevelTemp, m_charBasicInfo.Level.Modifier);
                    labelAgeModifier.Visible = true;
                    nudAgeModifier.Visible = true;
                    Global.SetNud(nudAgeDays, m_charBasicInfo.Age.Days);
                    break;
                case BasicInfoStyle.AgeInWeeks:  // Wiz1-5
                    SetClasses(GenericClass.Fighter, GenericClass.Lord);
                    comboClass.Items.Add(new GenericClassTag(GenericClass.Ninja));
                    labelAlignCurrent.Visible = false;
                    labelAlignPerm.Visible = false;
                    labelSex.Visible = false;
                    comboSex.Visible = false;
                    comboAlignTemp.Visible = false;
                    labelBirthDay.Text = "weeks";
                    Global.SetNud(nudAgeDays, m_charBasicInfo.Age.Days / 7);
                    nudLevelPermanent.Maximum = Int16.MaxValue;
                    nudLevelTemp.Maximum = Int16.MaxValue;
                    Global.SetNud(nudLevelTemp, m_charBasicInfo.Level.Temporary);
                    nudAgeDays.Maximum = 51;
                    nudAgeYears.Maximum = Int16.MaxValue / 52 - 1;
                    break;
                case BasicInfoStyle.NoAgeOrAlign:  // BT1-2
                    SetSingleClasses(BTBaseCharacter.BTClasses);
                    nudLevelTemp.Value = m_charBasicInfo.Level.Temporary;
                    labelAlignCurrent.Visible = false;
                    labelAlignPerm.Visible = false;
                    labelSex.Visible = false;
                    comboSex.Visible = false;
                    comboAlignTemp.Visible = false;
                    labelAlignment.Visible = false;
                    labelAlignCurrent.Visible = false;
                    labelAlignPerm.Visible = false;
                    comboAlignPermanent.Visible = false;
                    nudAgeDays.Visible = false;
                    nudAgeModifier.Visible = false;
                    nudAgeYears.Visible = false;
                    labelAge.Visible = false;
                    labelAgeModifier.Visible = false;
                    labelBirthDay.Visible = false;
                    labelBirthYear.Visible = false;
                    break;
                case BasicInfoStyle.SexNoAgeAlign:  // BT3
                    nudLevelTemp.Maximum = 32767;
                    nudLevelPermanent.Maximum = 32767;
                    SetSingleClasses(BT3Character.BT3Classes);
                    nudLevelTemp.Value = m_charBasicInfo.Level.Temporary;
                    labelAlignCurrent.Visible = false;
                    labelAlignPerm.Visible = false;
                    labelSex.Visible = true;
                    comboSex.Visible = true;
                    comboAlignTemp.Visible = false;
                    labelAlignment.Visible = false;
                    labelAlignCurrent.Visible = false;
                    labelAlignPerm.Visible = false;
                    comboAlignPermanent.Visible = false;
                    nudAgeDays.Visible = false;
                    nudAgeModifier.Visible = false;
                    nudAgeYears.Visible = false;
                    labelAge.Visible = false;
                    labelAgeModifier.Visible = false;
                    labelBirthDay.Visible = false;
                    labelBirthYear.Visible = false;
                    break;
                case BasicInfoStyle.NoLevel:  // Ultima1
                    nudLevelTemp.Visible = false;
                    nudLevelPermanent.Visible = false;
                    labelLevelCurrent.Visible = false;
                    labelLevelPerm.Visible = false;
                    labelLevel.Visible = false;
                    SetSingleClasses(UltimaBaseCharacter.UltimaClasses);
                    labelAlignCurrent.Visible = false;
                    labelAlignPerm.Visible = false;
                    labelSex.Visible = true;
                    comboSex.Visible = true;
                    comboAlignTemp.Visible = false;
                    labelAlignment.Visible = false;
                    labelAlignCurrent.Visible = false;
                    labelAlignPerm.Visible = false;
                    comboAlignPermanent.Visible = false;
                    nudAgeDays.Visible = false;
                    nudAgeModifier.Visible = false;
                    nudAgeYears.Visible = false;
                    labelAge.Visible = false;
                    labelAgeModifier.Visible = false;
                    labelBirthDay.Visible = false;
                    labelBirthYear.Visible = false;
                    break;
                default:
                    break;
            }

            tbName.Text = m_charBasicInfo.CharName;
            Global.SetNud(nudLevelPermanent, m_charBasicInfo.Level.Permanent);
            SelectClass(m_charBasicInfo.CharClass);
            comboSex.SelectedIndex = (int)m_charBasicInfo.CharSex;
            comboRace.SelectedIndex = RaceIndex(m_charBasicInfo.CharRace);
            if (m_charBasicInfo.CharAlignment != null)
            {
                comboAlignTemp.SelectedIndex = (int)m_charBasicInfo.CharAlignment.Temporary;
                comboAlignPermanent.SelectedIndex = (int)m_charBasicInfo.CharAlignment.Permanent;
            }
            if (m_charBasicInfo.Age != null)
            {
                Global.SetNud(nudAgeYears, m_charBasicInfo.Age.Years);
                Global.SetNud(nudAgeModifier, m_charBasicInfo.Age.Modifier);
            }
        }

        private int RaceIndex(GenericRace race)
        {
            for(int i = 0; i < comboRace.Items.Count; i++)
                if (((Race)comboRace.Items[i]).Generic == race)
                    return i;
            return 0;
        }

        private GenericRace RaceFromIndex(int index)
        {
            return ((Race)comboRace.Items[index]).Generic;
        }
           
        private void SelectClass(GenericClass gc)
        {
            for (int i = 0; i < comboClass.Items.Count; i++)
            {
                if (((GenericClassTag)comboClass.Items[i]).Class == gc)
                {
                    comboClass.SelectedIndex = i;
                    return;
                }
            }
            comboClass.SelectedIndex = -1;
        }

        public void UpdateFromUI()
        {
            m_charBasicInfo.CharName = tbName.Text;
            switch (Games.GetInfoType(m_charBasicInfo.Game))
            {
                case BasicInfoStyle.PermanentWithTemp:  // MM1/2
                    m_charBasicInfo.Level = new StatAndModifier(new OneByteStat((byte) nudLevelTemp.Value, (byte) nudLevelPermanent.Value));
                    m_charBasicInfo.Age.Days = (int) nudAgeDays.Value;
                    break;
                case BasicInfoStyle.PermanentWithMod:  // MM3
                case BasicInfoStyle.PermanentWithModNoAlign:  // MM4-5
                    m_charBasicInfo.Age.Days = (int) nudAgeDays.Value;
                    m_charBasicInfo.Level = new StatAndModifier((int)nudLevelPermanent.Value, (int)nudLevelTemp.Value);
                    break;
                case BasicInfoStyle.AgeInWeeks:  // Wiz1-5
                    m_charBasicInfo.Level = new StatAndModifier((int)nudLevelPermanent.Value, (int)(nudLevelTemp.Value - nudLevelPermanent.Value));
                    m_charBasicInfo.Age.Days = (int) nudAgeDays.Value * 7;
                    break;
                case BasicInfoStyle.SexNoAgeAlign:  // BT3
                    m_charBasicInfo.Level = new StatAndModifier((int) nudLevelPermanent.Value, (int)(nudLevelTemp.Value - nudLevelPermanent.Value));
                    break;
                case BasicInfoStyle.NoLevel:  // Ultima1
                    break;
                default:
                    m_charBasicInfo.Level = new StatAndModifier(new OneByteStat((byte) nudLevelTemp.Value, (byte) nudLevelPermanent.Value));
                    break;
            }
            m_charBasicInfo.CharSex = (MMSex) comboSex.SelectedIndex;
            m_charBasicInfo.CharRace = RaceFromIndex(comboRace.SelectedIndex);
            m_charBasicInfo.CharClass = ((GenericClassTag)comboClass.SelectedItem).Class;
            m_charBasicInfo.CharAlignment = new GenericAlignment((GenericAlignmentValue)comboAlignTemp.SelectedIndex, (GenericAlignmentValue)comboAlignPermanent.SelectedIndex);
            if (m_charBasicInfo.Age != null)
            {
                m_charBasicInfo.Age.Years = (int)nudAgeYears.Value;
                m_charBasicInfo.Age.Modifier = (int)nudAgeModifier.Value;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            UpdateFromUI();
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public class CharBasicInfo
    {
        public GenericClass CharClass;
        public GenericRace CharRace;
        public MMSex CharSex;
        public StatAndModifier Level;
        public GenericAlignment CharAlignment;
        public string CharName;
        public GameNames Game;
        public GenericAge Age;

        public CharBasicInfo(GameNames game, string name, StatAndModifier level, MMSex sex, GenericAlignment align, GenericRace race, GenericClass gClass, GenericAge age)
        {
            Game = game;
            CharClass = gClass;
            CharRace = race;
            CharSex = sex;
            Level = level;
            CharAlignment = align;
            CharName = name;
            Age = age;
        }

        public byte[] GetNameBytes() { return Games.GetNameBytes(Game, CharName); }
    }

    public class GenericAge
    {
        public int Years;
        public int Days;
        public int Modifier;

        public GenericAge(int years, int days)
        {
            Years = years;
            Days = days;
            Modifier = 0;
        }

        public GenericAge(int years, int days, int mod)
        {
            Years = years;
            Days = days;
            Modifier = mod;
        }

        public override string ToString()
        {
            return String.Format("{0}{1}y, {2}d", Years, Modifier > 0 ? Global.AddPlus(Modifier) : "", Days);
        }
    }

    public class GenericClassTag
    {
        public GenericClass Class;

        public GenericClassTag(GenericClass gc)
        {
            Class = gc;
        }

        public override string ToString()
        {
            return BaseCharacter.ClassString(Class);
        }
    }
}
