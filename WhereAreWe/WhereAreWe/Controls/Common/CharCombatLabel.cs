using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class CharCombatLabel : UserControl
    {
        public CharCombatLabel()
        {
            InitializeComponent();
        }

        public BasicConditionFlags Condition
        {
            set { labelCondition.Text = BaseCharacter.WorstConditionString(value); }
        }

        public bool Melee
        {
            set
            {
                pbMelee.Visible = value;
                if (value)
                    pbMelee.Image = Properties.Resources.pngMeleeCheck;
            }
        }

        public bool Empty
        {
            get
            {
                return String.IsNullOrWhiteSpace(labelCondition.Text) &&
                       !pbMelee.Visible &&
                       String.IsNullOrWhiteSpace(labelName.Text) &&
                       String.IsNullOrWhiteSpace(labelHP.Text) &&
                       String.IsNullOrWhiteSpace(labelSP.Text);
            }
        }

        public string ToolTip
        {
            get { return tipCombatInfo.GetToolTip(this); }
            set
            {
                tipCombatInfo.SetToolTip(this, value);
                foreach(Control ctrl in Controls)
                    tipCombatInfo.SetToolTip(ctrl, value);
                tipCombatInfo.AutoPopDelay = 10000;
            }
        }

        public string CharName
        {
            get { return labelName.Text; }
            set { labelName.Text = value; }
        }

        public string HP
        {
            get { return labelHP.Text; }
            set { labelHP.Text = value; }
        }

        public string SP
        {
            get { return labelSP.Text; }
            set { labelSP.Text = value; }
        }

        public int NameLength
        {
            get { return labelName.Width; }
        }

        public void SetHPOffset(int i)
        {
            labelHP.Left = labelName.Left + i;
            labelSP.Left = labelHP.Left + 37;
        }

        public void Clear()
        {
            labelCondition.Text = "";
            pbMelee.Visible = false;
            labelName.Text = "";
            labelHP.Text = "";
            labelSP.Text = "";
        }
    }
}
