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
    public partial class MM3ConditionEditForm : Form
    {
        private MMCondition m_condition;
        private MMProtections m_protections;
        private int m_iOriginalWidth = -1;

        private GameNames m_game;

        public MM3ConditionEditForm()
        {
            InitializeComponent();
            InitSizes();
        }

        public MM3ConditionEditForm(GameNames game)
        {
            InitializeComponent();
            InitSizes();

            m_game = game;
        }

        private void InitSizes()
        {
            m_iOriginalWidth = Width;
        }

        public MMCondition Condition
        {
            get
            {
                UpdateFromUI();
                return m_condition;
            }

            set
            {
                m_condition = value;
                UpdateUI();
            }
        }

        public MMProtections Protection
        {
            get
            {
                UpdateFromUI();
                return m_protections;
            }

            set
            {
                m_protections = value;
                UpdateUI();
            }
        }

        public void UpdateUI()
        {
            if (m_condition != null)
            {
                nudCursed.Value = m_condition.Cursed;
                nudHeartBroken.Value = m_condition.HeartBroken;
                nudWeak.Value = m_condition.Weak;
                nudPoisoned.Value = m_condition.Poisoned;
                nudDiseased.Value = m_condition.Diseased;
                nudInLove.Value = m_condition.InLove;
                nudInsane.Value = m_condition.Insane;
                nudDrunk.Value = m_condition.Drunk;
                nudAsleep.Value = m_condition.Asleep;
                nudUnconscious.Value = m_condition.Unconscious;
                nudConfused.Value = m_condition.Confused;
                nudStone.Value = m_condition.Stone;
                nudDepressed.Value = m_condition.Depressed;
                nudDead.Value = m_condition.Dead;
                nudParalyzed.Value = m_condition.Paralyzed;
                nudEradicated.Value = m_condition.Eradicated;
                cbCursed.Checked = (nudCursed.Value > 0);
                cbHeartBroken.Checked = (nudHeartBroken.Value > 0);
                cbWeak.Checked = (nudWeak.Value > 0);
                cbPoisoned.Checked = (nudPoisoned.Value > 0);
                cbDiseased.Checked = (nudDiseased.Value > 0);
                cbInLove.Checked = (nudInLove.Value > 0);
                cbInsane.Checked = (nudInsane.Value > 0);
                cbDrunk.Checked = (nudDrunk.Value > 0);
                cbAsleep.Checked = (nudAsleep.Value > 0);
                cbUnconscious.Checked = (nudUnconscious.Value > 0);
                cbConfused.Checked = (nudConfused.Value > 0);
                cbStone.Checked = (nudStone.Value > 0);
                cbDepressed.Checked = (nudDepressed.Value > 0);
                cbDead.Checked = (nudDead.Value > 0);
                cbParalyzed.Checked = (nudParalyzed.Value > 0);
                cbEradicated.Checked = (nudEradicated.Value > 0);
            }
            if (m_protections != null)
            {
                nudBlessed.Value = m_protections.Blessed;
                nudHolyBonus.Value = m_protections.HolyBonus;
                nudPowerShield.Value = m_protections.PowerShield;
                nudHeroism.Value = m_protections.Heroism;
                cbBlessed.Checked = (nudBlessed.Value > 0);
                cbHolyBonus.Checked = (nudHolyBonus.Value > 0);
                cbPowerShield.Checked = (nudPowerShield.Value > 0);
                cbHeroism.Checked = (nudHeroism.Value > 0);
                if (m_iOriginalWidth > 0)
                    Width = m_iOriginalWidth;
            }
            else
                Width = m_iOriginalWidth - panelBonuses.Right + nudAsleep.Right;

            panelBonuses.Visible = (m_protections != null);


            CheckEnabled();
        }

        public void UpdateFromUI()
        {
            m_condition = new MM3Condition();
            m_protections = new MMProtections();

            m_condition.Cursed = (byte)(cbCursed.Checked ? nudCursed.Value : 0);
            m_condition.HeartBroken = (byte)(cbHeartBroken.Checked ? nudHeartBroken.Value : 0);
            m_condition.Weak = (byte)(cbWeak.Checked ? nudWeak.Value : 0);
            m_condition.Poisoned = (byte)(cbPoisoned.Checked ? nudPoisoned.Value : 0);
            m_condition.Diseased = (byte)(cbDiseased.Checked ? nudDiseased.Value : 0);
            m_condition.InLove = (byte)(cbInLove.Checked ? nudInLove.Value : 0);
            m_condition.Insane = (byte)(cbInsane.Checked ? nudInsane.Value : 0);
            m_condition.Drunk = (byte)(cbDrunk.Checked ? nudDrunk.Value : 0);
            m_condition.Asleep = (byte)(cbAsleep.Checked ? nudAsleep.Value : 0);
            m_condition.Unconscious = (byte)(cbUnconscious.Checked ? nudUnconscious.Value : 0);
            m_condition.Confused = (byte)(cbConfused.Checked ? nudConfused.Value : 0);
            m_condition.Stone = (byte)(cbStone.Checked ? nudStone.Value : 0);
            m_condition.Depressed = (byte)(cbDepressed.Checked ? nudDepressed.Value : 0);
            m_condition.Dead = (byte)(cbDead.Checked ? nudDead.Value : 0);
            m_condition.Paralyzed = (byte)(cbParalyzed.Checked ? nudParalyzed.Value : 0);
            m_condition.Eradicated = (byte)(cbEradicated.Checked ? nudEradicated.Value : 0);
            m_protections.Blessed = (byte)(cbBlessed.Checked ? nudBlessed.Value : 0);
            m_protections.HolyBonus = (byte)(cbHolyBonus.Checked ? nudHolyBonus.Value : 0);
            m_protections.PowerShield = (byte)(cbPowerShield.Checked ? nudPowerShield.Value : 0);
            m_protections.Heroism = (byte)(cbHeroism.Checked ? nudHeroism.Value : 0);
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
            foreach (Control ctrl in Controls)
            {
                if (ctrl is CheckBox)
                    ((CheckBox)ctrl).Checked = false;
                if (ctrl is NumericUpDown)
                    ((NumericUpDown)ctrl).Value = 0;
            }
        }

        private void CheckEnabled()
        {
            nudCursed.Enabled = cbCursed.Checked;
            nudHeartBroken.Enabled = cbHeartBroken.Checked;
            nudWeak.Enabled = cbWeak.Checked;
            nudPoisoned.Enabled = cbPoisoned.Checked;
            nudDiseased.Enabled = cbDiseased.Checked;
            nudInLove.Enabled = cbInLove.Checked;
            nudInsane.Enabled = cbInsane.Checked;
            nudDrunk.Enabled = cbDrunk.Checked;
            nudAsleep.Enabled = cbAsleep.Checked;
            nudUnconscious.Enabled = cbUnconscious.Checked;
            nudConfused.Enabled = cbConfused.Checked;
            nudStone.Enabled = cbStone.Checked;
            nudDepressed.Enabled = cbDepressed.Checked;
            nudDead.Enabled = cbDead.Checked;
            nudParalyzed.Enabled = cbParalyzed.Checked;
            nudEradicated.Enabled = cbEradicated.Checked;
            nudBlessed.Enabled = cbBlessed.Checked;
            nudHolyBonus.Enabled = cbHolyBonus.Checked;
            nudPowerShield.Enabled = cbPowerShield.Checked;
            nudHeroism.Enabled = cbHeroism.Checked;

            if (cbCursed.Checked && nudCursed.Value == 0)
                nudCursed.Value = 1;
            if (cbHeartBroken.Checked && nudHeartBroken.Value == 0)
                nudHeartBroken.Value = 1;
            if (cbWeak.Checked && nudWeak.Value == 0)
                nudWeak.Value = 1;
            if (cbPoisoned.Checked && nudPoisoned.Value == 0)
                nudPoisoned.Value = 1;
            if (cbDiseased.Checked && nudDiseased.Value == 0)
                nudDiseased.Value = 1;
            if (cbInLove.Checked && nudInLove.Value == 0)
                nudInLove.Value = 1;
            if (cbInsane.Checked && nudInsane.Value == 0)
                nudInsane.Value = 1;
            if (cbDrunk.Checked && nudDrunk.Value == 0)
                nudDrunk.Value = 1;
            if (cbAsleep.Checked && nudAsleep.Value == 0)
                nudAsleep.Value = 1;
            if (cbUnconscious.Checked && nudUnconscious.Value == 0)
                nudUnconscious.Value = 1;
            if (cbConfused.Checked && nudConfused.Value == 0)
                nudConfused.Value = 1;
            if (cbStone.Checked && nudStone.Value == 0)
                nudStone.Value = 1;
            if (cbDepressed.Checked && nudDepressed.Value == 0)
                nudDepressed.Value = 1;
            if (cbDead.Checked && nudDead.Value == 0)
                nudDead.Value = 1;
            if (cbParalyzed.Checked && nudParalyzed.Value == 0)
                nudParalyzed.Value = 1;
            if (cbEradicated.Checked && nudEradicated.Value == 0)
                nudEradicated.Value = 1;
            if (cbBlessed.Checked && nudBlessed.Value == 0)
                nudBlessed.Value = 1;
            if (cbHolyBonus.Checked && nudHolyBonus.Value == 0)
                nudHolyBonus.Value = 1;
            if (cbPowerShield.Checked && nudPowerShield.Value == 0)
                nudPowerShield.Value = 1;
            if (cbHeroism.Checked && nudHeroism.Value == 0)
                nudHeroism.Value = 1;
        }

        private void CheckedChanged(object sender, EventArgs e)
        {
            CheckEnabled();
        }
    }
}
