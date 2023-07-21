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
    public partial class EditDndBasicCharInfoForm : Form
    {
        private DnDCharBasicInfo m_charBasicInfo;

        public EditDndBasicCharInfoForm()
        {
            InitializeComponent();
        }

        public DnDCharBasicInfo BasicInfo
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
            comboAlignment.Items.Clear();
            foreach (GenericAlignmentValue ga in Games.Alignments(m_charBasicInfo.Game))
                comboAlignment.Items.Add(new AlignmentTag(ga));
            comboClass.Items.Clear();
            foreach (GenericClass gc in Games.Classes(m_charBasicInfo.Game))
                comboClass.Items.Add(new GenericClassTag(gc));
            BasicInfoStyle style = Games.GetInfoType(m_charBasicInfo.Game);
            m_charBasicInfo.SetLevels(nudLevel1, nudLevel2, nudLevel3);
            tbName.Text = m_charBasicInfo.CharName;
            SelectClass(m_charBasicInfo.CharClass);
            comboSex.SelectedIndex = m_charBasicInfo.CharSex == MMSex.Male ? 0 : 1;
            comboRace.SelectedIndex = RaceIndex(m_charBasicInfo.CharRace);
            SelectAlignment(m_charBasicInfo.CharAlignment);
        }

        private void SelectAlignment(GenericAlignmentValue align)
        {
            for (int i = 0; i < comboAlignment.Items.Count; i++)
            {
                if ((comboAlignment.Items[i] as AlignmentTag).Alignment == align)
                {
                    comboAlignment.SelectedIndex = i;
                    return;
                }
            }
            comboAlignment.SelectedIndex = -1;
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
            m_charBasicInfo.Levels = GetLevels();
            m_charBasicInfo.CharSex = comboSex.SelectedIndex == 0 ? MMSex.Male : MMSex.Female;
            m_charBasicInfo.CharRace = RaceFromIndex(comboRace.SelectedIndex);
            m_charBasicInfo.CharClass = ((GenericClassTag)comboClass.SelectedItem).Class;
            m_charBasicInfo.CharAlignment = ((AlignmentTag)comboAlignment.SelectedItem).Alignment;
        }

        private int[] GetLevels()
        {
            switch(Global.SeparateClasses((comboClass.SelectedItem as GenericClassTag).Class).Length)
            {
                case 2: return new int[] { (int)nudLevel1.Value, (int)nudLevel2.Value, 0 };
                case 3: return new int[] { (int)nudLevel1.Value, (int)nudLevel2.Value, (int) nudLevel3.Value };
                default: return new int[] { (int)nudLevel1.Value, 0, 0 };
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

        private void comboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenericClass gc = (comboClass.SelectedItem as GenericClassTag).Class;
            GenericClass[] classes = Global.SeparateClasses(gc);
            int iNumClasses = classes.Length;
            nudLevel3.Visible = iNumClasses > 2;
            labelLevel3.Visible = iNumClasses > 2;
            labelClass3.Visible = iNumClasses > 2;
            nudLevel3.Visible = iNumClasses > 2;
            nudLevel2.Visible = iNumClasses > 1;
            labelLevel2.Visible = iNumClasses > 1;
            nudLevel2.Visible = iNumClasses > 1;
            labelClass2.Visible = iNumClasses > 1;
            if (iNumClasses > 2)
                labelClass3.Text = BaseCharacter.ClassString(classes[2]);
            if (iNumClasses > 1)
                labelClass2.Text = BaseCharacter.ClassString(classes[1]);
            labelClass1.Text = BaseCharacter.ClassString(classes[0]);
        }
    }

    public class AlignmentTag
    {
        public GenericAlignmentValue Alignment;

        public AlignmentTag(GenericAlignmentValue alignment)
        {
            Alignment = alignment;
        }

        public override string ToString()
        {
            return EOBBaseCharacter.AlignmentString(Alignment);
        }
    }

    public class DnDCharBasicInfo
    {
        public GenericClass CharClass;
        public GenericRace CharRace;
        public MMSex CharSex;
        public int[] Levels;
        public GenericAlignmentValue CharAlignment;
        public string CharName;
        public GameNames Game;
        public GenericAge Age;

        public DnDCharBasicInfo(GameNames game, string name, int[] levels, MMSex sex, GenericAlignmentValue align, GenericRace race, GenericClass gClass)
        {
            Game = game;
            CharClass = gClass;
            CharRace = race;
            CharSex = sex;
            Levels = levels;
            CharAlignment = align;
            CharName = name;
        }

        public void SetLevels(params NumericUpDown[] nuds)
        {
            for (int i = 0; i < nuds.Length; i++)
            {
                if (Levels.Length > i)
                    Global.SetNud(nuds[i], Levels[i]);
            }
        }

        public byte[] GetNameBytes() { return Games.GetNameBytes(Game, CharName); }
    }
}
