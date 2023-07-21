using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class LineStyleSelectForm : Form
    {
        private bool m_bUpdating = false;

        public LineStyleSelectForm()
        {
            InitializeComponent();
        }

        public LineStyleSelectForm(string strTitle)
        {
            InitializeComponent();
            Text = strTitle;
        }

        public Color Color;
        public DashStyle Pattern;
        public int LineWidth;

        public DrawColor LineColor { get { return new DrawColor(Color, Pattern, (byte)LineWidth); } set { Color = value.color; Pattern = value.style; LineWidth = value.width; } }

        private void llColor_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectColor();
        }

        private void SelectColor()
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = Color;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                Color = dialog.Color;
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            if (pbSample.Image != null)
                pbSample.Image.Dispose();
            pbSample.Image = Global.GetLineBitmap(LineColor, pbSample.Size);
        }

        private void ColorPatternSelectForm_Load(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            m_bUpdating = true;
            comboPattern.Items.Clear();
            for (DashStyle style = DashStyle.Solid; style <= DashStyle.DashDotDot; style++)
            {
                comboPattern.Items.Add(new DashStyleItem(style));
                if (Pattern == style)
                    comboPattern.SelectedIndex = comboPattern.Items.Count - 1;
            }
            nudWidth.Maximum = Properties.Settings.Default.MaxLineWidth;
            Global.SetNud(nudWidth, LineWidth);
            m_bUpdating = false;
            UpdateUI();
        }

        private void comboPattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pattern = (comboPattern.SelectedItem as DashStyleItem).Style;
            UpdateUI();
        }

        private void pbSample_SizeChanged(object sender, EventArgs e)
        {
            UpdateUI();
        }

        private void pbSample_Click(object sender, EventArgs e)
        {
            SelectColor();
        }

        private void nudWidth_ValueChanged(object sender, EventArgs e)
        {
            LineWidth = (int) nudWidth.Value;
            UpdateUI();
        }
    }

    public class DashStyleItem
    {
        public DashStyle Style;

        public DashStyleItem(DashStyle style)
        {
            Style = style;
        }

        public override string ToString()
        {
            switch (Style)
            {
                case DashStyle.Solid: return "Solid";
                case DashStyle.Dot: return "Dot";
                case DashStyle.Dash: return "Dash";
                case DashStyle.DashDot: return "Dash Dot";
                case DashStyle.DashDotDot: return "Dash Dot Dot";
                default: return String.Format("Unknown ({0})", (int)Style);
            }
        }
    }
}
