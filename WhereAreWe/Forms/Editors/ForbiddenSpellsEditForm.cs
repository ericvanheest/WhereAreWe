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
    public partial class ForbiddenSpellsEditForm : Form
    {
        private MapAttributeFlags m_flags;
        private bool m_bUpdatingLinkedItems = false;

        public ForbiddenSpellsEditForm()
        {
            InitializeComponent();
        }

        public MapAttributeFlags MapAttributes
        {
            get { return m_flags; }
            set { m_flags = value; UpdateUI(); }
        }

        private void AddSpellLVI(string str, MapAttributeFlags flags)
        {
            ListViewItem item = new ListViewItem(str);
            item.Tag = new ForbiddenSpellTag(flags, str);
            item.Checked = !m_flags.HasFlag(flags);
            lvSpells.Items.Add(item);
        }

        public void UpdateUI()
        {
            lvSpells.BeginUpdate();
            lvSpells.Items.Clear();
            switch (Properties.Settings.Default.Game)
            {
                case GameNames.MightAndMagic1:
                    AddSpellLVI("Fly", MapAttributeFlags.AllowFly);
                    //AddSpellLVI("Etherealize", MapAttributeFlags.AllowEtherealize);
                    AddSpellLVI("Surface", MapAttributeFlags.AllowSurface);
                    AddSpellLVI("Teleport", MapAttributeFlags.AllowTeleport);
                    AddSpellLVI("Town Portal", MapAttributeFlags.AllowTownPortal);
                    break;
                case GameNames.MightAndMagic2:
                    AddSpellLVI("Etherealize", MapAttributeFlags.AllowEtherealize);
                    AddSpellLVI("Teleport", MapAttributeFlags.AllowTeleport);
                    AddSpellLVI("Town Portal", MapAttributeFlags.AllowTownPortal);
                    AddSpellLVI("Surface", MapAttributeFlags.AllowSurface);
                    AddSpellLVI("Lloyd's Beacon", MapAttributeFlags.AllowLloydsBeacon);
                    break;
                case GameNames.MightAndMagic3:
                    AddSpellLVI("Etherealize", MapAttributeFlags.AllowEtherealize);
                    AddSpellLVI("Teleport", MapAttributeFlags.AllowTeleport);
                    AddSpellLVI("Town Portal", MapAttributeFlags.AllowTownPortal);
                    AddSpellLVI("Nature's Gate", MapAttributeFlags.AllowNaturesGate);
                    AddSpellLVI("Time Distortion", MapAttributeFlags.AllowTimeDistortion);
                    AddSpellLVI("Super Shelter", MapAttributeFlags.AllowSuperShelter);
                    AddSpellLVI("Lloyd's Beacon", MapAttributeFlags.AllowLloydsBeacon);
                    break;
                default:
                    break;
            }
            Global.SizeHeadersAndContent(lvSpells);
            lvSpells.EndUpdate();
        }

        private MapAttributeFlags UpdateFlag(MapAttributeFlags flag, bool bValue)
        {
            if (bValue)
                return m_flags | flag;
            else
                return m_flags & ~flag;
        }

        public void UpdateFromUI()
        {
            m_flags = MapAttributeFlags.None;
            foreach (ListViewItem lvi in lvSpells.Items)
            {
                if (!lvi.Checked)
                {
                    ForbiddenSpellTag tag = lvi.Tag as ForbiddenSpellTag;
                    m_flags |= tag.Flags;
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
            foreach (ListViewItem lvi in lvSpells.Items)
                lvi.Checked = bCheck;
        }

        private void lvSpells_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (m_bUpdatingLinkedItems || Properties.Settings.Default.Game != GameNames.MightAndMagic2)
                return;

            m_bUpdatingLinkedItems = true;

            ForbiddenSpellTag tag = e.Item.Tag as ForbiddenSpellTag;
            if (tag.Flags == MapAttributeFlags.AllowTownPortal || tag.Flags == MapAttributeFlags.AllowSurface || tag.Flags == MapAttributeFlags.AllowLloydsBeacon)
            {
                foreach (ListViewItem lvi in lvSpells.Items)
                {
                    if (lvi == null)        // No idea how this happens
                        continue;
                    ForbiddenSpellTag tag1 = lvi.Tag as ForbiddenSpellTag;
                    if (tag1.Flags == MapAttributeFlags.AllowTownPortal || tag1.Flags == MapAttributeFlags.AllowSurface || tag1.Flags == MapAttributeFlags.AllowLloydsBeacon)
                        lvi.Checked = e.Item.Checked;
                }
            }

            m_bUpdatingLinkedItems = false;
        }
    }

    public class ForbiddenSpellTag
    {
        public MapAttributeFlags Flags;
        public string Text;

        public ForbiddenSpellTag(MapAttributeFlags flags, string text)
        {
            Flags = flags;
            Text = text;
        }
    }
}
