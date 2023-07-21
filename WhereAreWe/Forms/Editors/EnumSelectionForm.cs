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
    public partial class EnumSelectionForm : Form
    {
        private int m_enumOrigValue;
        private int m_enumValue;
        private string[] m_strings;

        public EnumSelectionForm()
        {
            InitializeComponent();
        }

        public void SetValue(int val, string[] strings, string strTitle = "")
        {
            m_enumOrigValue = val;
            m_enumValue = val;
            m_strings = strings;
            if (!String.IsNullOrWhiteSpace(strTitle))
                Text = strTitle;
            UpdateUI();
        }

        public int GetValue() { return m_enumValue; }

        public void UpdateUI()
        {
            comboValue.Items.Clear();
            foreach(string str in m_strings)
                comboValue.Items.Add(str);

            if (m_enumValue < comboValue.Items.Count)
                comboValue.SelectedIndex = m_enumValue;

            if (m_enumOrigValue < m_strings.Length)
                labelCurrent.Text = m_strings[m_enumOrigValue];
            else
                labelCurrent.Text = string.Format("Unknown({0})", m_enumOrigValue);

            comboValue.Focus();
            comboValue.Select();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            m_enumValue = comboValue.SelectedIndex;
            Close();
        }

        private void comboValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_enumValue = comboValue.SelectedIndex;
        }
    }
}
