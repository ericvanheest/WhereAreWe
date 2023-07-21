using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using System.Xml;

namespace WhereAreWe
{
    public partial class ColorsForm : CommonKeyForm
    {
        private PictureBox[] Blocks;
        private DrawColorControl[] Lines;
        private PictureBox[] Notes;
        private UIElementOptions UIElements = null;

        public ColorsForm()
        {
            InitializeComponent();
            Blocks = new PictureBox[] { pbBlock1, pbBlock2, pbBlock3, pbBlock4, pbBlock5, pbBlock6, pbBlock7, pbBlock8, pbBlock9, pbBlock10,
                pbBlock11, pbBlock12, pbBlock13, pbBlock14, pbBlock15, pbBlock16, pbBlock17, pbBlock18, pbBlock19, pbBlock20 };
            Lines = new DrawColorControl[] { dccLine1, dccLine2, dccLine3, dccLine4, dccLine5, dccLine6, dccLine7, dccLine8, dccLine9, dccLine10,
                dccLine11, dccLine12, dccLine13, dccLine14, dccLine15, dccLine16, dccLine17, dccLine18, dccLine19, dccLine20 };
            Notes = new PictureBox[] { pbNote1, pbNote2, pbNote3, pbNote4, pbNote5, pbNote6, pbNote7, pbNote8, pbNote9, pbNote10,
                pbNote11, pbNote12, pbNote13, pbNote14, pbNote15, pbNote16, pbNote17, pbNote18, pbNote19, pbNote20 };
            UIElements = new UIElementOptions();

            CommonKeySelectAll += ColorsForm_CommonKeySelectAll;
        }

        private void ColorsForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            if (tcColors.SelectedTab == tpUI)
                Global.SelectAll(lvElements);
        }

        public UIElementOptions Elements
        {
            get
            {
                UIElementOptions options = new UIElementOptions(false);
                foreach (ListViewItem lvi in lvElements.Items)
                {
                    UIElementOption tag = lvi.Tag as UIElementOption;
                    options.Elements.Add(tag.Element, tag);
                }
                return options;
            }

            set
            {
                UIElements = value;
                UpdateUIElements();
            }
        }

        public DrawColors Colors
        {
            get
            {
                DrawColors colors = new DrawColors();
                foreach (PictureBox pb in Blocks)
                    colors.Blocks.Add(pb.Tag as DrawColor);
                foreach (DrawColorControl dcc in Lines)
                    colors.Lines.Add(dcc.DrawColor);
                foreach (PictureBox pb in Notes)
                    colors.Notes.Add(pb.Tag as DrawColor);
                return colors;
            }
            set
            {
                for (int i = 0; i < Blocks.Length; i++)
                {
                    if (value.Blocks.Count > i)
                    {
                        if (Blocks[i].Image != null)
                            Blocks[i].Image.Dispose();
                        Blocks[i].Image = Global.GetFillBitmap(value.Blocks[i], Blocks[i].Size);
                        Blocks[i].Tag = value.Blocks[i];
                    }
                }
                for (int i = 0; i < Lines.Length; i++)
                {
                    if (value.Lines.Count > i)
                        Lines[i].DrawColor = value.Lines[i];
                }
                for (int i = 0; i < Notes.Length; i++)
                {
                    if (value.Notes.Count > i)
                    {
                        Notes[i].BackColor = value.Notes[i].color;
                        Notes[i].Tag = value.Notes[i];
                    }
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
            Properties.Settings.Default.DrawColors = Colors;
            Properties.Settings.Default.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnDefaults_Click(object sender, EventArgs e)
        {
            if (tcColors.SelectedTab == tpUI)
            {
                if (MessageBox.Show("Reset all UI elements to their default colors?", "Reset Defaults", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return;

                UIElements.SetDefaults();

                UpdateUIElements();
            }
            else
            {
                if (MessageBox.Show("Reset all block, line and note colors and styles to the defaults?", "Reset Defaults", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) != DialogResult.OK)
                    return;

                Colors = DrawColors.Default;
            }
        }

        private void UpdateUIElements()
        {
            lvElements.BeginUpdate();
            foreach (ListViewItem lvi in lvElements.Items)
            {
                lvi.Tag = UIElementOption.Default((lvi.Tag as UIElementOption).Element);
                UpdateElement(lvi);
            }
            lvElements.EndUpdate();
        }

        private DrawColor SelectNoteColor(DrawColor dc, PictureBox pb)
        {
            if (dc == null)
                dc = new DrawColor(Color.Black);
            TitledColorDialog cd = new TitledColorDialog("Select the note color");
            cd.Color = dc.color;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                pb.BackColor = cd.Color;
                return new DrawColor(cd.Color);
            }
            return dc;
        }

        private DrawColor SelectBlockColor(DrawColor dc, PictureBox pb)
        {
            ColorPatternSelectForm form = new ColorPatternSelectForm("Select block color and pattern");
            form.BlockColor = dc;
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (pb.Image != null)
                    pb.Image.Dispose();
                pb.Image = Global.GetFillBitmap(form.BlockColor, pb.Size);
                return form.BlockColor;
            }
            return dc;
        }

        private void pbBlock1_Click(object sender, EventArgs e)
        {
            Blocks[0].Tag = SelectBlockColor(Blocks[0].Tag as DrawColor, Blocks[0]);
        }

        private void pbBlock2_Click(object sender, EventArgs e)
        {
            Blocks[1].Tag = SelectBlockColor(Blocks[1].Tag as DrawColor, Blocks[1]);
        }

        private void pbBlock3_Click(object sender, EventArgs e)
        {
            Blocks[2].Tag = SelectBlockColor(Blocks[2].Tag as DrawColor, Blocks[2]);
        }

        private void pbBlock4_Click(object sender, EventArgs e)
        {
            Blocks[3].Tag = SelectBlockColor(Blocks[3].Tag as DrawColor, Blocks[3]);
        }

        private void pbBlock5_Click(object sender, EventArgs e)
        {
            Blocks[4].Tag = SelectBlockColor(Blocks[4].Tag as DrawColor, Blocks[4]);
        }

        private void pbBlock6_Click(object sender, EventArgs e)
        {
            Blocks[5].Tag = SelectBlockColor(Blocks[5].Tag as DrawColor, Blocks[5]);
        }

        private void pbBlock7_Click(object sender, EventArgs e)
        {
            Blocks[6].Tag = SelectBlockColor(Blocks[6].Tag as DrawColor, Blocks[6]);
        }

        private void pbBlock8_Click(object sender, EventArgs e)
        {
            Blocks[7].Tag = SelectBlockColor(Blocks[7].Tag as DrawColor, Blocks[7]);
        }

        private void pbBlock9_Click(object sender, EventArgs e)
        {
            Blocks[8].Tag = SelectBlockColor(Blocks[8].Tag as DrawColor, Blocks[8]);
        }

        private void pbBlock10_Click(object sender, EventArgs e)
        {
            Blocks[9].Tag = SelectBlockColor(Blocks[9].Tag as DrawColor, Blocks[9]);
        }

        private void pbBlock11_Click(object sender, EventArgs e)
        {
            Blocks[10].Tag = SelectBlockColor(Blocks[10].Tag as DrawColor, Blocks[10]);
        }

        private void pbBlock12_Click(object sender, EventArgs e)
        {
            Blocks[11].Tag = SelectBlockColor(Blocks[11].Tag as DrawColor, Blocks[11]);
        }

        private void pbBlock13_Click(object sender, EventArgs e)
        {
            Blocks[12].Tag = SelectBlockColor(Blocks[12].Tag as DrawColor, Blocks[12]);
        }

        private void pbBlock14_Click(object sender, EventArgs e)
        {
            Blocks[13].Tag = SelectBlockColor(Blocks[13].Tag as DrawColor, Blocks[13]);
        }

        private void pbBlock15_Click(object sender, EventArgs e)
        {
            Blocks[14].Tag = SelectBlockColor(Blocks[14].Tag as DrawColor, Blocks[14]);
        }

        private void pbBlock16_Click(object sender, EventArgs e)
        {
            Blocks[15].Tag = SelectBlockColor(Blocks[15].Tag as DrawColor, Blocks[15]);
        }

        private void pbBlock17_Click(object sender, EventArgs e)
        {
            Blocks[16].Tag = SelectBlockColor(Blocks[16].Tag as DrawColor, Blocks[16]);
        }

        private void pbBlock18_Click(object sender, EventArgs e)
        {
            Blocks[17].Tag = SelectBlockColor(Blocks[17].Tag as DrawColor, Blocks[17]);
        }

        private void pbBlock19_Click(object sender, EventArgs e)
        {
            Blocks[18].Tag = SelectBlockColor(Blocks[18].Tag as DrawColor, Blocks[18]);
        }

        private void pbBlock20_Click(object sender, EventArgs e)
        {
            Blocks[19].Tag = SelectBlockColor(Blocks[19].Tag as DrawColor, Blocks[19]);
        }

        private void pbNote1_Click(object sender, EventArgs e)
        {
            Notes[0].Tag = SelectNoteColor(Notes[0].Tag as DrawColor, Notes[0]);
        }

        private void pbNote2_Click(object sender, EventArgs e)
        {
            Notes[1].Tag = SelectNoteColor(Notes[1].Tag as DrawColor, Notes[1]);
        }

        private void pbNote3_Click(object sender, EventArgs e)
        {
            Notes[2].Tag = SelectNoteColor(Notes[2].Tag as DrawColor, Notes[2]);
        }

        private void pbNote4_Click(object sender, EventArgs e)
        {
            Notes[3].Tag = SelectNoteColor(Notes[3].Tag as DrawColor, Notes[3]);
        }

        private void pbNote5_Click(object sender, EventArgs e)
        {
            Notes[4].Tag = SelectNoteColor(Notes[4].Tag as DrawColor, Notes[4]);
        }

        private void pbNote6_Click(object sender, EventArgs e)
        {
            Notes[5].Tag = SelectNoteColor(Notes[5].Tag as DrawColor, Notes[5]);
        }

        private void pbNote7_Click(object sender, EventArgs e)
        {
            Notes[6].Tag = SelectNoteColor(Notes[6].Tag as DrawColor, Notes[6]);
        }

        private void pbNote8_Click(object sender, EventArgs e)
        {
            Notes[7].Tag = SelectNoteColor(Notes[7].Tag as DrawColor, Notes[7]);
        }

        private void pbNote9_Click(object sender, EventArgs e)
        {
            Notes[8].Tag = SelectNoteColor(Notes[8].Tag as DrawColor, Notes[8]);
        }

        private void pbNote10_Click(object sender, EventArgs e)
        {
            Notes[9].Tag = SelectNoteColor(Notes[9].Tag as DrawColor, Notes[9]);
        }

        private void pbNote11_Click(object sender, EventArgs e)
        {
            Notes[10].Tag = SelectNoteColor(Notes[10].Tag as DrawColor, Notes[10]);
        }

        private void pbNote12_Click(object sender, EventArgs e)
        {
            Notes[11].Tag = SelectNoteColor(Notes[11].Tag as DrawColor, Notes[11]);
        }

        private void pbNote13_Click(object sender, EventArgs e)
        {
            Notes[12].Tag = SelectNoteColor(Notes[12].Tag as DrawColor, Notes[12]);
        }

        private void pbNote14_Click(object sender, EventArgs e)
        {
            Notes[13].Tag = SelectNoteColor(Notes[13].Tag as DrawColor, Notes[13]);
        }

        private void pbNote15_Click(object sender, EventArgs e)
        {
            Notes[14].Tag = SelectNoteColor(Notes[14].Tag as DrawColor, Notes[14]);
        }

        private void pbNote16_Click(object sender, EventArgs e)
        {
            Notes[15].Tag = SelectNoteColor(Notes[15].Tag as DrawColor, Notes[15]);
        }

        private void pbNote17_Click(object sender, EventArgs e)
        {
            Notes[16].Tag = SelectNoteColor(Notes[16].Tag as DrawColor, Notes[16]);
        }

        private void pbNote18_Click(object sender, EventArgs e)
        {
            Notes[17].Tag = SelectNoteColor(Notes[17].Tag as DrawColor, Notes[17]);
        }

        private void pbNote19_Click(object sender, EventArgs e)
        {
            Notes[18].Tag = SelectNoteColor(Notes[18].Tag as DrawColor, Notes[18]);
        }

        private void pbNote20_Click(object sender, EventArgs e)
        {
            Notes[19].Tag = SelectNoteColor(Notes[19].Tag as DrawColor, Notes[19]);
        }

        private Color SelectColor(Color colorDefault)
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = colorDefault;
            if (dialog.ShowDialog() == DialogResult.OK)
                return dialog.Color;
            return colorDefault;
        }

        private void ColorsForm_Load(object sender, EventArgs e)
        {
            lvElements.BeginUpdate();
            foreach (KeyValuePair<ColoredUIElements, UIElementOption> element in UIElements.Elements)
            {
                switch (element.Key)
                {
                    case ColoredUIElements.None:
                    case ColoredUIElements.TriggerItem:
                        continue;
                }
                ListViewItem lvi = new ListViewItem(UIElementOption.ElementName(element.Key));
                lvi.SubItems.Add("");
                lvi.Tag = element.Value.Clone();
                UpdateElement(lvi);
                lvElements.Items.Add(lvi);
            }
            lvElements.EndUpdate();
        }

        private void llDefaultUI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectDefaultColor();
        }

        private void SelectDefaultColor()
        {
            if (lvElements.SelectedItems.Count < 1)
                return;

            lvElements.BeginUpdate();
            foreach (ListViewItem lvi in lvElements.SelectedItems)
            {
                UIElementOption tag = lvi.Tag as UIElementOption;
                lvi.Tag = UIElementOption.Default(tag.Element);
                UpdateElement(lvi);
            }
            lvElements.EndUpdate();
        }

        private void llForegroundUI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectForegroundColor();
        }

        private void SelectForegroundColor()
        {
            if (lvElements.SelectedItems.Count < 1)
                return;

            Color colorNew = SelectColor((lvElements.SelectedItems[0].Tag as UIElementOption).ForeColor);

            lvElements.BeginUpdate();
            foreach (ListViewItem lvi in lvElements.SelectedItems)
            {
                UIElementOption tag = lvi.Tag as UIElementOption;
                lvi.Tag = new UIElementOption(tag.Element, colorNew, tag.BackColor);
                UpdateElement(lvi);
            }
            lvElements.EndUpdate();
        }

        private void llBackgroundUI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectBackgroundColor();
        }

        private void SelectBackgroundColor()
        {
            if (lvElements.SelectedItems.Count < 1)
                return;

            Color colorNew = SelectColor((lvElements.SelectedItems[0].Tag as UIElementOption).BackColor);

            lvElements.BeginUpdate();
            foreach (ListViewItem lvi in lvElements.SelectedItems)
            {
                UIElementOption tag = lvi.Tag as UIElementOption;
                lvi.Tag = new UIElementOption(tag.Element, tag.ForeColor, colorNew);
                UpdateElement(lvi);
            }
            lvElements.EndUpdate();
        }

        private void UpdateElement(ListViewItem lvi)
        {
            UIElementOption tag = lvi.Tag as UIElementOption;
            lvi.SubItems[1].Text = String.Format("{0}, {1}", UIElementOption.ColorName(tag.ForeColor), UIElementOption.ColorName(tag.BackColor));
            lvi.ForeColor = tag.ForeColor;
            lvi.BackColor = tag.BackColor;
        }

        private void lvElements_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool bAnySelected = lvElements.SelectedItems.Count > 0;
            llDefaultUI.Enabled = bAnySelected;
            llBackgroundUI.Enabled = bAnySelected;
            llForegroundUI.Enabled = bAnySelected;
        }

        private void lvElements_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            SelectColors();
        }

        private void miUIForeground_Click(object sender, EventArgs e)
        {
            SelectForegroundColor();
        }

        private void miUIBackground_Click(object sender, EventArgs e)
        {
            SelectBackgroundColor();
        }

        private void miUIDefault_Click(object sender, EventArgs e)
        {
            SelectDefaultColor();
        }

        private void cmUIElements_Opening(object sender, CancelEventArgs e)
        {
            bool bAnySelected = lvElements.SelectedItems.Count > 0;
            miUIBackground.Enabled = bAnySelected;
            miUIForeground.Enabled = bAnySelected;
            miUIDefault.Enabled = bAnySelected;
        }

        private void miUISelectColors_Click(object sender, EventArgs e)
        {
            SelectColors();
        }

        private void SelectColors()
        {
            if (lvElements.SelectedItems.Count < 1)
                return;

            NamedColorSelectorForm form = new NamedColorSelectorForm(lvElements.SelectedItems[0].Tag as UIElementOption);
            if (form.ShowDialog() != DialogResult.OK)
                return;

            lvElements.BeginUpdate();
            foreach (ListViewItem lvi in lvElements.SelectedItems)
            {
                (lvi.Tag as UIElementOption).CopyColorsFrom(form.Element);
                UpdateElement(lvi);
            }
            lvElements.EndUpdate();
        }
    }

    public enum ColoredUIElements
    {
        None,
        ActiveQuest,
        ActiveQuestTask,
        CompletedQuestTask,
        ManuallyCompletedQuest,
        ActiveManualQuestTask,
        ManuallyCompletedQuestTask,
        InvalidQuestTask,
        UnachievableQuestTask,
        TriggerItem,
        PartyFormItem,
        DisabledSpell,
        EnabledSpell,
        DisabledShopItem,
        EnabledShopItem,

        Last
    }

    public class UIElementOption
    {
        public ColoredUIElements Element;
        public Color ForeColor;
        public Color BackColor;

        public UIElementOption(ColoredUIElements element, Color fore, Color back)
        {
            Element = element;
            ForeColor = fore;
            BackColor = back;
        }

        public UIElementOption Clone()
        {
            return new UIElementOption(Element, ForeColor, BackColor);
        }

        public void CopyColorsFrom(UIElementOption copy)
        {
            ForeColor = copy.ForeColor;
            BackColor = copy.BackColor;
        }

        public static UIElementOption Default(ColoredUIElements element)
        {
            switch (element)
            {
                case ColoredUIElements.ActiveQuest: return new UIElementOption(element, SystemColors.ControlText, SystemColors.Window);
                case ColoredUIElements.CompletedQuestTask: return new UIElementOption(element, SystemColors.GrayText, SystemColors.Window);
                case ColoredUIElements.ActiveManualQuestTask: return new UIElementOption(element, Color.Blue, SystemColors.Window);
                case ColoredUIElements.ManuallyCompletedQuest: return new UIElementOption(element, Color.Blue, SystemColors.Window);
                case ColoredUIElements.ActiveQuestTask: return new UIElementOption(element, SystemColors.ControlText, SystemColors.Window);
                case ColoredUIElements.ManuallyCompletedQuestTask: return new UIElementOption(element, Color.Blue, SystemColors.Window);
                case ColoredUIElements.InvalidQuestTask: return new UIElementOption(element, Color.DarkGoldenrod, SystemColors.Window);
                case ColoredUIElements.UnachievableQuestTask: return new UIElementOption(element, Color.DarkRed, SystemColors.Window);
                case ColoredUIElements.PartyFormItem: return new UIElementOption(element, SystemColors.ControlText, SystemColors.Window);
                case ColoredUIElements.DisabledSpell: return new UIElementOption(element, SystemColors.GrayText, SystemColors.Window);
                case ColoredUIElements.EnabledSpell: return new UIElementOption(element, SystemColors.ControlText, SystemColors.Window);
                case ColoredUIElements.DisabledShopItem: return new UIElementOption(element, SystemColors.GrayText, SystemColors.Window);
                case ColoredUIElements.EnabledShopItem: return new UIElementOption(element, SystemColors.ControlText, SystemColors.Window);
                default: return new UIElementOption(element, SystemColors.ControlText, SystemColors.Window);
            }
        }

        public static string ColorName(Color color)
        {
            if (color.IsNamedColor)
                return color.Name;
            return String.Format("#{0:X6}", color.ToArgb());
        }

        public static string ElementName(ColoredUIElements element)
        {
            switch (element)
            {
                case ColoredUIElements.None: return "None";
                case ColoredUIElements.ActiveQuest: return "Quest";
                case ColoredUIElements.CompletedQuestTask: return "Completed Quest Task";
                case ColoredUIElements.ManuallyCompletedQuest: return "Manually Completed Quest";
                case ColoredUIElements.ActiveQuestTask: return "Active Quest Task";
                case ColoredUIElements.ActiveManualQuestTask: return "Active Manual Quest Task";
                case ColoredUIElements.ManuallyCompletedQuestTask: return "Manually Completed Quest Task";
                case ColoredUIElements.InvalidQuestTask: return "Invalid Quest Task";
                case ColoredUIElements.UnachievableQuestTask: return "Unachievable Quest Task";
                case ColoredUIElements.TriggerItem: return "Trigger Item";
                case ColoredUIElements.PartyFormItem: return "Party Form Item";
                case ColoredUIElements.DisabledSpell: return "Disabled (uncastable) Spell";
                case ColoredUIElements.EnabledSpell: return "Enabled (castable) Spell";
                case ColoredUIElements.DisabledShopItem: return "Disabled (unusable) Shop Item";
                case ColoredUIElements.EnabledShopItem: return "Enabled (usable) Shop Item";
                default: return String.Format("Unknown({0})", (int)element);
            }
        }
    }

    public class UIElementOptionsTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                UIElementOptions options = new UIElementOptions(value as string);
                return options;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return ((UIElementOptions)value).GetXml();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    [TypeConverter(typeof(UIElementOptionsTypeConverter))]
    public class UIElementOptions
    {
        public Dictionary<ColoredUIElements, UIElementOption> Elements;

        public UIElementOptions()
        {
            Elements = new Dictionary<ColoredUIElements, UIElementOption>();
            SetDefaults();
        }

        public UIElementOptions(bool bSetDefaults)
        {
            Elements = new Dictionary<ColoredUIElements, UIElementOption>();
            if (bSetDefaults)
                SetDefaults();
        }

        public Color ForeColor(ColoredUIElements element)
        {
            if (Elements.ContainsKey(element))
                return Elements[element].ForeColor;
            return UIElementOption.Default(element).ForeColor;
        }

        public Color BackColor(ColoredUIElements element)
        {
            if (Elements.ContainsKey(element))
                return Elements[element].BackColor;
            return UIElementOption.Default(element).BackColor;
        }

        public void SetElement(ListViewItem lvi, ColoredUIElements element)
        {
            UIElementOption option = Elements.ContainsKey(element) ? Elements[element] : UIElementOption.Default(element);
            lvi.ForeColor = option.ForeColor;
            lvi.BackColor = option.BackColor;
        }

        public void SetElement(Control ctrl, ColoredUIElements element)
        {
            UIElementOption option = Elements.ContainsKey(element) ? Elements[element] : UIElementOption.Default(element);
            ctrl.BackColor = option.BackColor;
            ctrl.ForeColor = option.ForeColor;
            ctrl.Font = new Font(ctrl.Font, FontStyle.Regular);
        }

        public void SetElement(TreeNode tn, ColoredUIElements element)
        {
            UIElementOption option = Elements.ContainsKey(element) ? Elements[element] : UIElementOption.Default(element);
            tn.ForeColor = option.ForeColor;
            tn.BackColor = option.BackColor;
        }

        public UIElementOptions(string strXml)
        {
            Elements = new Dictionary<ColoredUIElements, UIElementOption>();
            if (String.IsNullOrWhiteSpace(strXml))
            {
                SetDefaults();
                return;
            }
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(strXml);
                XmlNodeList list = doc.SelectNodes("/UIElementOptions/Elements/Element");
                foreach (XmlElement node in list)
                {
                    try
                    {
                        Color fore = ColorFromString(node.Attributes["fore"].Value);
                        Color back = ColorFromString(node.Attributes["back"].Value);
                        ColoredUIElements element = (ColoredUIElements)Convert.ToInt32(node.Attributes["index"].Value);
                        Elements.Add(element, new UIElementOption(element, fore, back));
                    }
                    catch (Exception)
                    {
                        Global.LogError("Invalid element in UIElementOptions XML; ignoring");
                    }
                }
            }
            catch (Exception)
            {
                Global.LogError("Invalid UIElementOptions XML; using defaults");
                SetDefaults();
            }
        }

        public void SetDefaults()
        {
            Elements.Clear();
            for (ColoredUIElements element = ColoredUIElements.ActiveQuest; element < ColoredUIElements.Last; element++)
                Elements.Add(element, UIElementOption.Default(element));
        }

        public static Color ColorFromString(string str)
        {
            try
            {
                if (str.StartsWith("#"))
                    return Color.FromArgb(Convert.ToInt32(str.Substring(1), 16));
                else return Color.FromName(str);
            }
            catch (Exception)
            {
                return Color.Black;
            }
        }

        public static string ColorString(Color c)
        {
            if (c.IsNamedColor)
                return c.Name;
            return String.Format("#{0:X8}", c.ToArgb());
        }

        public static string ColorString(Color fore, Color back)
        {
            return String.Format("{0} on {1}", ColorString(fore), ColorString(back));
        }

        public string GetXml()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<UIElementOptions version=\"1.0\"/>");
            XmlNode root = doc.SelectSingleNode("/UIElementOptions");
            XmlNode nodeElements = root.AppendChild(doc.CreateElement("Elements"));
            foreach (KeyValuePair<ColoredUIElements, UIElementOption> element in Elements)
            {
                XmlElement nodeElement = nodeElements.AppendChild(doc.CreateElement("Element")) as XmlElement;
                nodeElement.Attributes.Append(doc.CreateAttribute("index")).Value = String.Format("{0}", (int)element.Key);
                nodeElement.Attributes.Append(doc.CreateAttribute("fore")).Value = ColorString(element.Value.ForeColor);
                nodeElement.Attributes.Append(doc.CreateAttribute("back")).Value = ColorString(element.Value.BackColor);
            }
            return doc.OuterXml;
        }

        public override string ToString() { return GetXml(); }
    }
}
