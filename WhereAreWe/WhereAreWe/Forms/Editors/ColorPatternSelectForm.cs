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
    public partial class ColorPatternSelectForm : Form
    {
        public ColorPatternSelectForm()
        {
            InitializeComponent();
            SetDefaults();
        }

        private void SetDefaults()
        {
            Color = Color.Black;
            Pattern = HatchStyle.Percent90;
            BackgroundColor = Color.White;
            DualColor = false;
        }

        public ColorPatternSelectForm(string strTitle)
        {
            InitializeComponent();
            Text = strTitle;
            SetDefaults();
        }

        public Color Color;
        public Color BackgroundColor;
        public HatchStyle Pattern;

        public bool DualColor { get; set; }

        public DrawColor BlockColor { get { return new DrawColor(Color, Pattern); } set { Color = value.color; Pattern = value.styleBlock; } }
        public ColorPattern ColorPattern 
        {
            get { return new ColorPattern(Color, Pattern, BackgroundColor); }
            set
            {
                Color = value.Color;
                BackgroundColor = value.BackColor;
                Pattern = value.Pattern;
            }
        }

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

        private void SelectBackColor()
        {
            ColorDialog dialog = new ColorDialog();
            dialog.Color = BackgroundColor;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                BackgroundColor = dialog.Color;
                UpdateUI();
            }
        }

        private void UpdateUI()
        {
            if (pbSample.Image != null)
                pbSample.Image.Dispose();
            if (DualColor)
                pbSample.Image = Global.GetFillBitmap(ColorPattern, pbSample.Size);
            else
                pbSample.Image = Global.GetFillBitmap(BlockColor, pbSample.Size);
        }

        private void ColorPatternSelectForm_Load(object sender, EventArgs e)
        {
            if (DualColor)
            {
                llBackground.Visible = true;
                llColor.Text = "Foreground color";
            }
            else
            {
                llBackground.Visible = false;
                llColor.Text = "Select color";
            }

            comboPattern.Items.Clear();
            for (HatchStyle style = HatchStyle.Horizontal; style <= HatchStyle.SolidDiamond; style++)
            {
                comboPattern.Items.Add(new HatchStyleItem(style));
                if (Pattern == style)
                    comboPattern.SelectedIndex = comboPattern.Items.Count - 1;
            }
            UpdateUI();
        }

        private void comboPattern_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pattern = (comboPattern.SelectedItem as HatchStyleItem).Style;
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

        private void llBackground_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SelectBackColor();
        }
    }

    public class HatchStyleItem
    {
        public HatchStyle Style;

        public HatchStyleItem(HatchStyle style)
        {
            Style = style;
        }

        public override string ToString()
        {
            switch (Style)
            {
                case HatchStyle.Horizontal: return "Horizontal";
                case HatchStyle.Vertical: return "Vertical";
                case HatchStyle.ForwardDiagonal: return "Forward Diagonal";
                case HatchStyle.BackwardDiagonal: return "Backward Diagonal";
                case HatchStyle.Cross: return "Cross";
                case HatchStyle.DiagonalCross: return "Diagonal Cross";
                case HatchStyle.Percent05: return "5%";
                case HatchStyle.Percent10: return "10%";
                case HatchStyle.Percent20: return "20%";
                case HatchStyle.Percent25: return "25%";
                case HatchStyle.Percent30: return "30%";
                case HatchStyle.Percent40: return "40%";
                case HatchStyle.Percent50: return "50%";
                case HatchStyle.Percent60: return "60%";
                case HatchStyle.Percent70: return "70%";
                case HatchStyle.Percent75: return "75%";
                case HatchStyle.Percent80: return "80%";
                case HatchStyle.Percent90: return "Full Color";
                case HatchStyle.LightDownwardDiagonal: return "Light Downward Diagonal";
                case HatchStyle.LightUpwardDiagonal: return "Light Upward Diagonal";
                case HatchStyle.DarkDownwardDiagonal: return "Dark Downward Diagonal";
                case HatchStyle.DarkUpwardDiagonal: return "Dark Upward Diagonal";
                case HatchStyle.WideDownwardDiagonal: return "Wide Downward Diagonal";
                case HatchStyle.WideUpwardDiagonal: return "Wide Upward Diagonal";
                case HatchStyle.LightVertical: return "Light Vertical";
                case HatchStyle.LightHorizontal: return "Light Horizontal";
                case HatchStyle.NarrowVertical: return "Narrow Vertical";
                case HatchStyle.NarrowHorizontal: return "Narrow Horizontal";
                case HatchStyle.DarkVertical: return "Dark Vertical";
                case HatchStyle.DarkHorizontal: return "Dark Horizontal";
                case HatchStyle.DashedDownwardDiagonal: return "Dashed Downward Diagonal";
                case HatchStyle.DashedUpwardDiagonal: return "Dashed Upward Diagonal";
                case HatchStyle.DashedHorizontal: return "Dashed Horizontal";
                case HatchStyle.DashedVertical: return "Dashed Vertical";
                case HatchStyle.SmallConfetti: return "Small Confetti";
                case HatchStyle.LargeConfetti: return "Large Confetti";
                case HatchStyle.ZigZag: return "Zig Zag";
                case HatchStyle.Wave: return "Wave";
                case HatchStyle.DiagonalBrick: return "Diagonal Brick";
                case HatchStyle.HorizontalBrick: return "Horizontal Brick";
                case HatchStyle.Weave: return "Weave";
                case HatchStyle.Plaid: return "Plaid";
                case HatchStyle.Divot: return "Divot";
                case HatchStyle.DottedGrid: return "Dotted Grid";
                case HatchStyle.DottedDiamond: return "Dotted Diamond";
                case HatchStyle.Shingle: return "Shingle";
                case HatchStyle.Trellis: return "Trellis";
                case HatchStyle.Sphere: return "Sphere";
                case HatchStyle.SmallGrid: return "Small Grid";
                case HatchStyle.SmallCheckerBoard: return "Small Checker Board";
                case HatchStyle.LargeCheckerBoard: return "Large Checker Board";
                case HatchStyle.OutlinedDiamond: return "Outlined Diamond";
                case HatchStyle.SolidDiamond: return "Solid Diamond";
                default: return String.Format("Unknown ({0})", (int) Style);
            }
        }
    }
}
