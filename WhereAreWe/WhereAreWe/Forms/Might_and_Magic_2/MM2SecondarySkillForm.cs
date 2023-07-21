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
    public partial class MM2SecondarySkillForm : Form
    {
        private MM2SecondarySkill m_skill1;
        private MM2SecondarySkill m_skill2;
        private bool m_bReadOnly = false;

        public bool ReadOnly { get { return m_bReadOnly; } set { m_bReadOnly = true; UpdateUI(); } }

        public MM2SecondarySkillForm()
        {
            InitializeComponent();
            m_skill1 = MM2SecondarySkill.None;
            m_skill2 = MM2SecondarySkill.None;

            comboSkill1.Items.Clear();
            comboSkill2.Items.Clear();

            for (int i = 0; i < 16; i++)
            {
                comboSkill1.Items.Add(MM2Character.SecondarySkillName((MM2SecondarySkill) i));
                comboSkill2.Items.Add(MM2Character.SecondarySkillName((MM2SecondarySkill) i));
            }
        }

        public byte SkillByte
        {
            get { return (byte)(((int)m_skill2 << 4) | (int)m_skill1); }
            set
            {
                m_skill2 = (MM2SecondarySkill)(value >> 4);
                m_skill1 = (MM2SecondarySkill)(value & 0x0f);
                UpdateUI();
            }
        }

        public void UpdateUI()
        {
            comboSkill1.SelectedIndex = (int)m_skill1;
            comboSkill2.SelectedIndex = (int)m_skill2;

            comboSkill1.Enabled = !m_bReadOnly;
            comboSkill2.Enabled = !m_bReadOnly;

            if (m_bReadOnly)
            {
                btnOK.Visible = false;
                btnCancel.Text = "&Close";
                Text = "View Secondary Skills";
            }
            else
            {
                btnOK.Visible = true;
                btnCancel.Text = "&Cancel";
                Text = "Change Secondary Skills";
            }
        }

        public void UpdateFromUI()
        {
            m_skill1 = (MM2SecondarySkill)comboSkill1.SelectedIndex;
            m_skill2 = (MM2SecondarySkill)comboSkill2.SelectedIndex;
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

        private void MM2SecondarySkillForm_Load(object sender, EventArgs e)
        {
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            for (int i = 1; i < 16; i++)
            {
                sb1.AppendFormat("{0}\r\n", MM2Character.SecondarySkillName((MM2SecondarySkill)i));
                sb2.AppendFormat("{0}\r\n", MM2Character.SecondarySkillDescription((MM2SecondarySkill)i));
            }

            labelSkills1.Text = sb1.ToString();
            labelSkills2.Text = sb2.ToString();
        }
    }
}
