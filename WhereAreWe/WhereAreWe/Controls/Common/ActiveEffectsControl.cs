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
    public partial class ActiveEffectsControl
        : HackerBasedUserControl
    {
        private bool m_bUpdatingSize = false;
        private MMActiveEffects m_lastEffects = null;
        private bool m_bShowValue = true;

        public ActiveEffectsControl()
        {
            InitializeComponent();
        }

        public bool ValueEnabled
        {
            get { return m_bShowValue; }
            set 
            {
                m_bShowValue = value;
                ResizeColumns();
            }
        }

        public ActiveEffectsControl(IMain main)
        {
            InitializeComponent();
            SetMain(main);
        }

        public void UpdateUI(MMActiveEffects effects)
        {
            if (m_lastEffects == null || !Global.Compare(effects.Bytes, m_lastEffects.Bytes))
            {
                m_lastEffects = effects;

                Dictionary<MMEffects, MMEffectTag> tags = MMEffectTag.GetEffects(effects, Hacker.Game);

                lvActiveEffects.BeginUpdate();
                lvActiveEffects.Items.Clear();
                if (tags != null)
                {
                    foreach (MMEffectTag tag in tags.Values)
                        AddEffect(tag);
                }
                ResizeColumns();
                lvActiveEffects.EndUpdate();
            }
        }

        private void ResizeColumns()
        {
            Global.SizeHeadersAndContent(lvActiveEffects);
            if (!m_bShowValue)
                lvActiveEffects.Columns[1].Width = 0;
        }

        private void AddEffect(MMEffectTag effect)
        {
            if (effect.Value == 0 && !effect.Enabled)
                return;
            ListViewItem lvi = new ListViewItem(effect.EffectText);
            if (effect.Value != 0)
                lvi.SubItems.Add(String.Format("{0}{1}", effect.Effect == MMEffects.Cursed ? "-" : "", effect.Value));
            effect.Enabled = true;
            lvi.Tag = effect;
            lvActiveEffects.Items.Add(lvi);
            effect.Index = lvi.Index;
        }

        private void miActiveEdit_Click(object sender, EventArgs e)
        {
            if (lvActiveEffects.FocusedItem == null)
                return;

            MMEffectTag tag = lvActiveEffects.FocusedItem.Tag as MMEffectTag;

            AttributeEditForm formAttr = new AttributeEditForm();

            switch (tag.BytesPerValue)
            {
                case 2:
                    formAttr.Attribute = new EditableAttribute((ushort)tag.Value);
                    if (formAttr.ShowDialog() == DialogResult.Cancel)
                        return;
                    tag.Value = formAttr.Attribute.UShorts[0];
                    break;
                default:
                    formAttr.Attribute = new EditableAttribute((byte)tag.Value);
                    if (formAttr.ShowDialog() == DialogResult.Cancel)
                        return;
                    tag.Value = formAttr.Attribute.Bytes[0];
                    break;
            }

            Hacker.SetActiveEffect(tag);
        }

        private void miActiveAdd_Click(object sender, EventArgs e)
        {
            MMEffectsEditForm form = new MMEffectsEditForm();
            form.SetMain(m_main, WindowType.Effects);
            Dictionary<MMEffects, MMEffectTag> tags = new Dictionary<MMEffects, MMEffectTag>(18);
            foreach (ListViewItem lvi in lvActiveEffects.Items)
            {
                MMEffectTag effect = lvi.Tag as MMEffectTag;
                tags.Add(effect.Effect, effect);
            }
            form.Effects = tags;
            if (form.ShowDialog() == DialogResult.OK)
            {
                tags = form.Effects;
                foreach (MMEffectTag effect in tags.Values)
                    Hacker.SetActiveEffect(effect);
            }
        }

        private void miActiveRemove_Click(object sender, EventArgs e)
        {
            if (lvActiveEffects.FocusedItem == null)
                return;

            MMEffectTag tag = lvActiveEffects.FocusedItem.Tag as MMEffectTag;
            tag.Enabled = false;
            tag.Value = 0;
            Hacker.SetActiveEffect(tag);
        }

        private void cmActiveEffects_Opening(object sender, CancelEventArgs e)
        {
            if (!Global.Cheats)
            {
                e.Cancel = true;
                return;
            }

            miActiveAdd.Enabled = true;
            if (lvActiveEffects.SelectedItems.Count > 0)
            {
                miActiveEdit.Enabled = ((MMEffectTag)lvActiveEffects.SelectedItems[0].Tag).Value != 0;
                miActiveRemove.Enabled = true;
            }
            else
            {
                miActiveEdit.Enabled = false;
                miActiveRemove.Enabled = false;
            }
        }

        private void lvActiveEffects_SizeChanged(object sender, EventArgs e)
        {
            if (m_bUpdatingSize)
                return;

            m_bUpdatingSize = true;
            lvActiveEffects.BeginUpdate();
            ResizeColumns();
            lvActiveEffects.EndUpdate();
            m_bUpdatingSize = false;
        }
    }
}
