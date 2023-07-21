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
    public partial class ConditionEditForm : Form
    {
        private BasicConditionFlags m_basicCondition;
        private GameNames m_game;
        private bool m_bUpdating = false;

        private BasicConditionFlags m_validConditions;

        private const BasicConditionFlags DefaultConditions =
            BasicConditionFlags.Asleep | BasicConditionFlags.Blinded | BasicConditionFlags.Cursed | BasicConditionFlags.Diseased | BasicConditionFlags.Poisoned |
            BasicConditionFlags.Silenced | BasicConditionFlags.Paralyzed | BasicConditionFlags.Unconscious |
            BasicConditionFlags.Stone | BasicConditionFlags.Dead | BasicConditionFlags.Eradicated;

        public ConditionEditForm()
        {
            InitializeComponent();
            m_validConditions = DefaultConditions;
        }

        public ConditionEditForm(GameNames game)
        {
            InitializeComponent();

            m_game = game;

            switch (game)
            {
                case GameNames.MightAndMagic1:
                    m_validConditions = DefaultConditions & ~BasicConditionFlags.Cursed;
                    break;
                case GameNames.MightAndMagic2:
                    m_validConditions = DefaultConditions & ~BasicConditionFlags.Blinded;
                    break;
                case GameNames.BardsTale1:
                case GameNames.BardsTale2:
                case GameNames.BardsTale3:
                    m_validConditions = BasicConditionFlags.Dead | BasicConditionFlags.Old | BasicConditionFlags.Poisoned | BasicConditionFlags.Stone | 
                        BasicConditionFlags.Paralyzed | BasicConditionFlags.Confused | BasicConditionFlags.Insane;
                    break;
                case GameNames.EyeOfTheBeholder1:
                case GameNames.EyeOfTheBeholder2:
                case GameNames.EyeOfTheBeholder3:
                    m_validConditions = BasicConditionFlags.Poisoned | BasicConditionFlags.Paralyzed;
                    break;
                default:
                    m_validConditions = DefaultConditions;
                    break;
            }
            m_bUpdating = false;
        }

        public void InitConditions(BasicConditionFlags conditions)
        {
            lvConditions.BeginUpdate();
            lvConditions.Items.Clear();
            long iCondition = 1;
            while (iCondition < (long) BasicConditionFlags.Last)
            {
                BasicConditionFlags flag = (BasicConditionFlags)iCondition;
                if (conditions.HasFlag(flag))
                {
                    ListViewItem lvi = new ListViewItem(Global.SingleConditionString(flag, m_game));
                    lvi.Tag = flag;
                    lvConditions.Items.Add(lvi);
                }
                iCondition <<= 1;
            }
            lvConditions.EndUpdate();
        }

        public BasicConditionFlags BasicCondition
        {
            get
            {
                UpdateFromUI();
                return m_basicCondition;
            }

            set
            {
                m_basicCondition = value;
                UpdateUI();
            }
        }

        public void UpdateUI()
        {
            m_bUpdating = true;
            foreach (ListViewItem lvi in lvConditions.Items)
                lvi.Checked = m_basicCondition.HasFlag((BasicConditionFlags)lvi.Tag);
            m_bUpdating = false;
        }

        public void UpdateFromUI()
        {
            m_basicCondition = BasicConditionFlags.Good;

            foreach (ListViewItem lvi in lvConditions.Items)
                if (lvi.Checked)
                    m_basicCondition |= (BasicConditionFlags)lvi.Tag;
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

        private void btnNone_Click(object sender, EventArgs e)
        {
            lvConditions.BeginUpdate();
            foreach (ListViewItem lvi in lvConditions.Items)
                lvi.Checked = false;
            lvConditions.EndUpdate();
        }

        private void Enable(ListViewItem lvi, bool bEnable = true)
        {
            m_bUpdating = true;
            lvi.ForeColor = bEnable ? SystemColors.ControlText : SystemColors.GrayText;
            if (!bEnable)
                lvi.Checked = false;
            m_bUpdating = false;
        }

        private void UpdateCheckboxes(int iLastChange = -1)
        {
            if (m_bUpdating)
                return;

            const BasicConditionFlags SevereConditions = BasicConditionFlags.Stone | BasicConditionFlags.Dead | BasicConditionFlags.Eradicated;

            switch (m_game)
            {
                case GameNames.MightAndMagic1:
                case GameNames.MightAndMagic2:
                    bool bSevere = false;
                    bool bEradicated = false;
                    bool bStone = false;
                    bool bDead = false;

                    foreach (ListViewItem lvi in lvConditions.CheckedItems)
                    {
                        BasicConditionFlags flag = (BasicConditionFlags)lvi.Tag;
                        if (SevereConditions.HasFlag(flag))
                            bSevere = true;
                        bEradicated = flag == BasicConditionFlags.Eradicated;
                        bStone = flag == BasicConditionFlags.Stone;
                        bDead = flag == BasicConditionFlags.Dead;
                    }

                    foreach (ListViewItem lvi in lvConditions.Items)
                    {
                        BasicConditionFlags flag = (BasicConditionFlags)lvi.Tag;
                        if (SevereConditions.HasFlag(flag))
                        {
                            Enable(lvi, !bEradicated || flag == BasicConditionFlags.Eradicated);
                            if (m_game == GameNames.MightAndMagic2)
                            {
                                if (flag == BasicConditionFlags.Stone)
                                    Enable(lvi, !bDead);
                                else if (flag == BasicConditionFlags.Dead)
                                    Enable(lvi, !bStone);
                            }
                        }
                        else
                            Enable(lvi, !bSevere);
                    }
                    break;
                case GameNames.BardsTale1:
                case GameNames.BardsTale2:
                case GameNames.BardsTale3:
                    // Any combination of conditions is possible in these games (though the UI may only show one)
                    break;
                default:
                    // Other games only allow one condition at a time
                    if (iLastChange == -1)
                        break;
                    m_bUpdating = true;
                    lvConditions.BeginUpdate();
                    foreach (ListViewItem lvi in lvConditions.CheckedItems)
                        if (lvi.Index != iLastChange)
                            lvi.Checked = false;
                    lvConditions.EndUpdate();
                    m_bUpdating = false;
                    break;
            }

        }

        private void lvConditions_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (m_bUpdating)
                return;
            if (lvConditions.Items[e.Index].ForeColor == SystemColors.GrayText)
                e.NewValue = e.CurrentValue;
        }

        private void lvConditions_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            UpdateCheckboxes(e.Item.Index);
        }

        private void ConditionEditForm_Load(object sender, EventArgs e)
        {
            InitConditions(m_validConditions);
            UpdateUI();
            UpdateCheckboxes();
        }
    }
}
