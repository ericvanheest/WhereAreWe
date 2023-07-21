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
    public partial class SpellRefPanel : UserControl
    {
        public delegate void SpellActivatedEventHandler(object sender, SpellActivatedEventArgs e);
        private BaseCharacter m_character;
        private bool m_bShowDuration = false;
        public TabPage ParentTab { get; set; }
        public TabControl ParentTabControl { get; set; }
        public event SpellActivatedEventHandler SpellActivated;

        public SpellRefPanel()
        {
            InitializeComponent();
            NativeMethods.SetTooltipDelay(lvSpells, 32000);
            lvSpells.MouseDoubleClick += lvSpells_MouseDoubleClick;
            SpellListView.EnableDragging = false;
        }

        void lvSpells_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (SpellActivated == null)
                return;

            if (lvSpells.SelectedItems.Count < 1)
                return;

            SpellActivated(sender, new SpellActivatedEventArgs(lvSpells.SelectedItems[0].Tag as SpellTag));
        }

        public string Title
        {
            get { return labelSpellType.Text; }
            set { labelSpellType.Text = value; }
        }

        public void SetCharacter(BaseCharacter character)
        {
            m_character = character;
        }

        public DraggableListView SpellListView { get { return lvSpells; } }
        public bool ShowSpellType
        {
            get { return lvSpells.Columns[0].Text == "Type"; }
            set
            {
                if (value == ShowSpellType)
                    return;
                if (value)
                    SpellListView.Columns.Insert(0, "Type");
                else
                    SpellListView.Columns.RemoveAt(0);
            }
        }

        public bool SpellsUseLevelOnly { set { lvSpells.Columns[ShowSpellType ? 1 : 0].Text = value ? "Lev" : "Num"; } }

        public void SizeHeadersAndContent()
        {
            if (m_bShowDuration && chDuration.Width == 0)
                chDuration.Width = 40;
            Global.SizeHeadersAndContent(lvSpells);
            if (!m_bShowDuration)
                chDuration.Width = 0;
        }

        public bool ShowDuration
        {
            get { return m_bShowDuration; }
            set 
            {
                m_bShowDuration = value;
                if (!m_bShowDuration)
                    chDuration.Width = 0;
            }
        }

        public ListViewItem GetLVIForSpell(Spell spell)
        {
            string strNum = null;
            if (spell.UsesLevelOnly)
                strNum = spell.Level.ToString();
            else
                strNum = String.Format("{0}-{1}", spell.Level, spell.Number);
            ListViewItem lvi = new ListViewItem(strNum);
            lvi.SubItems.Add(spell.ExtendedName);
            lvi.SubItems.Add(spell.Cost.ToString());
            lvi.SubItems.Add(spell.WhenString);
            lvi.SubItems.Add(spell.TargetString);
            lvi.SubItems.Add(spell.DurationString);
            lvi.SubItems.Add(spell.ShortDescription);
            bool bCastable = true;
            if (m_character != null && !m_character.KnowsSpell(spell))
            {
                Global.SetColor(lvi, ColoredUIElements.DisabledSpell);
                bCastable = false;
            }
            else
                Global.SetColor(lvi, ColoredUIElements.EnabledSpell);
            lvi.Tag = new SpellTag(spell, bCastable);
            string strDescription = Global.TipTextBreak(spell.Description);
            if (String.IsNullOrWhiteSpace(spell.Learned))
                lvi.ToolTipText = strDescription;
            else
                lvi.ToolTipText = String.Format("Learned: {0}\n{1}", spell.Learned, strDescription);
            return lvi;
        }
    }

    public class SpellActivatedEventArgs : EventArgs
    {
        public SpellTag SelectedSpell;

        public SpellActivatedEventArgs(SpellTag tag)
        {
            SelectedSpell = tag;
        }
    }
}
