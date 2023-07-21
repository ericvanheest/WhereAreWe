using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class NamedColorSelectorForm : Form
    {
        public UIElementOption Element;

        public NamedColorSelectorForm()
        {
            InitializeComponent();
        }

        public NamedColorSelectorForm(UIElementOption element)
        {
            InitializeComponent();
            Element = element.Clone();
        }

        private void NamedColorSelectorForm_Load(object sender, EventArgs e)
        {
            AddColors(lvForeground, true);
            AddColors(lvBackground, false);

            if (Element != null)
                SelectColors(Element.ForeColor, Element.BackColor);
        }

        private void AddColors(ListView lv, bool bForeground)
        {
            MethodInfo[] knownColors = typeof(Color).GetMethods(BindingFlags.Public | BindingFlags.Static);
            MethodInfo[] systemColors = typeof(SystemColors).GetMethods(BindingFlags.Public | BindingFlags.Static);

            lv.BeginUpdate();
            lv.Items.Clear();
            foreach (MethodInfo info in systemColors)
                AddColor(lv, bForeground, info, "System: ");
            foreach (MethodInfo info in knownColors)
                AddColor(lv, bForeground, info);
            lv.EndUpdate();
        }

        private void AddColor(ListView lv, bool bForeground, MethodInfo info, string strPrefix = "")
        {
            if (info.ReturnType == typeof(Color) && info.GetParameters().Length == 0)
            {
                Color color = (Color)info.Invoke(null, null);
                ListViewItem lvi = lv.Items.Add(strPrefix + color.Name);
                if (bForeground)
                    lvi.ForeColor = color;
                else
                    lvi.BackColor = color;
                lvi.Tag = new ColorInfoTag(color);
            }
        }

        private void SelectColor(ListView lv, Color c)
        {
            ColorInfoTag tag;
            if (c.IsNamedColor)
            {
                foreach (ListViewItem lvi in lv.Items)
                {
                    tag = lvi.Tag as ColorInfoTag;
                    if (tag.Selected.IsNamedColor && tag.Selected.Name == c.Name)
                    {
                        lvi.Selected = true;
                        lv.EnsureVisible(lvi.Index);
                        return;
                    }
                }
            }
            int iARGB = c.ToArgb();
            foreach (ListViewItem lvi in lv.Items)
            {
                tag = lvi.Tag as ColorInfoTag;
                if (tag.Selected.ToArgb() == iARGB)
                {
                    lvi.Selected = true;
                    lv.EnsureVisible(lvi.Index);
                    return;
                }
            }
        }

        private void SelectColors(Color fore, Color back)
        {
            SelectColor(lvForeground, fore);
            SelectColor(lvBackground, back);
        }

        private void lvForeground_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSample();
        }

        private void lvBackground_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSample();
        }

        private void UpdateSample()
        {
            if (lvBackground.SelectedItems.Count < 1 || lvForeground.SelectedItems.Count < 1)
            {
                labelSample.Text = String.Format("Invalid selection");
                labelSample.ForeColor = SystemColors.ControlText;
                labelSample.BackColor = SystemColors.Control;
                return;
            }
            ColorInfoTag tagFore = lvForeground.SelectedItems[0].Tag as ColorInfoTag;
            ColorInfoTag tagBack = lvBackground.SelectedItems[0].Tag as ColorInfoTag;

            labelSample.Text = String.Format("{0} on {1}", tagFore.Selected.Name, tagBack.Selected.Name);
            labelSample.ForeColor = tagFore.Selected;
            labelSample.BackColor = tagBack.Selected;
            Element.ForeColor = tagFore.Selected;
            Element.BackColor = tagBack.Selected;
        }

        private void lvForeground_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvForeground.SelectedItems.Count < 1)
                return;

            ColorInfoTag tag = lvForeground.SelectedItems[0].Tag as ColorInfoTag;

            lvBackground.BeginUpdate();
            foreach (ListViewItem lvi in lvBackground.Items)
                lvi.ForeColor = tag.Selected;
            lvBackground.EndUpdate();
        }

        private void lvBackground_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lvBackground.SelectedItems.Count < 1)
                return;

            ColorInfoTag tag = lvBackground.SelectedItems[0].Tag as ColorInfoTag;

            lvForeground.BeginUpdate();
            foreach (ListViewItem lvi in lvForeground.Items)
                lvi.BackColor = tag.Selected;
            lvForeground.EndUpdate();
        }
    }

    public class ColorInfoTag
    {
        public Color Selected;

        public ColorInfoTag(Color color)
        {
            Selected = color;
        }
    }
}
