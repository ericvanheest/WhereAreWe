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
    public partial class MMEffectsEditForm : HackerBasedForm
    {
        private Dictionary<MMEffects, MMEffectTag> m_effects;

        public MMEffectsEditForm()
        {
            InitializeComponent();
        }

        public Dictionary<MMEffects, MMEffectTag> Effects
        {
            get { return m_effects; }
            set { m_effects = value; UpdateUI(); }
        }

        public void UpdateUI()
        {
            Dictionary<MMEffects, MMEffectTag> allEffects = null;

            allEffects = MMEffectTag.GetEffects(null, Hacker.Game);

            lvEffects.BeginUpdate();
            lvEffects.Items.Clear();
            foreach (MMEffectTag tag in allEffects.Values)
            {
                ListViewItem lvi = new ListViewItem(tag.EffectText);
                lvi.Checked = m_effects.ContainsKey(tag.Effect);
                lvi.Tag = tag;
                lvEffects.Items.Add(lvi);
            }
            Global.SizeHeadersAndContent(lvEffects);
            lvEffects.EndUpdate();
        }

        public void UpdateFromUI()
        {
            MMEffectTag.UpdateEffects(m_effects);

            foreach (ListViewItem lvi in lvEffects.Items)
            {
                MMEffectTag tag = lvi.Tag as MMEffectTag;
                if (m_effects.ContainsKey(tag.Effect))
                {
                    MMEffectTag effect = m_effects[tag.Effect];
                    effect.Enabled = lvi.Checked;
                    if (lvi.Checked && effect.Value == 0)
                        effect.Value = 1;
                }
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

        private void btnAll_Click(object sender, EventArgs e)
        {
            CheckAll(this, true);
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            CheckAll(this, false);
        }

        private void CheckAll(Control ctrl, bool bCheck)
        {
            foreach (ListViewItem lvi in lvEffects.Items)
                lvi.Checked = bCheck;
        }
    }
}
