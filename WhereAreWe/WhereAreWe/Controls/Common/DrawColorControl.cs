using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WhereAreWe
{
    public partial class DrawColorControl : UserControl
    {
        public DrawColorControl()
        {
            InitializeComponent();
        }

        private int m_iKey = 1;

        private BlockMode m_mode = BlockMode.Block;

        public BlockMode Mode
        {
            get { return m_mode; }
            set
            {
                m_mode = value;
                SetModeUI();
            }
        }

        private void SetModeUI()
        {
            switch (m_mode)
            {
                case BlockMode.Block:
                    nudWidth.Hide();
                    cbStyle.Items.Clear();
                    cbStyle.Items.AddRange(new string[] {"Solid", "75%", "50%", "25%"});
                    cbStyle.SelectedIndex = 0;
                    break;
                default:
                    nudWidth.Show();
                    nudWidth.Maximum = Properties.Settings.Default.MaxLineWidth;
                    cbStyle.Items.Clear();
                    cbStyle.Items.AddRange(new string[] {"Solid", "Dot", "Dash"});
                    cbStyle.SelectedIndex = 0;
                    break;
            }
        }

        public int Key
        {
            get
            {
                return m_iKey;
            }

            set
            {
                m_iKey = value;
                labelKey.Text = String.Format("{0}", m_iKey);
            }
        }

        public Color Color
        {
            get
            {
                return pbSelect.BackColor;
            }

            set
            {
                pbSelect.BackColor = value;
            }
        }

        public byte LineWidth
        {
            get
            {
                return (byte) nudWidth.Value;
            }

            set
            {
                nudWidth.Value = value;
            }
        }

        public DashStyle StyleLines
        {
            get
            {
                switch (cbStyle.SelectedIndex)
                {
                    case 1: return DashStyle.Dot;
                    case 2: return DashStyle.Dash;
                    default: return DashStyle.Solid;
                }
            }

            set
            {
                cbStyle.SelectedIndex = Global.IndexOfStyle(value);
            }
        }

        public HatchStyle StyleBlocks
        {
            get
            {
                switch (cbStyle.SelectedIndex)
                {
                    case 1: return HatchStyle.Percent75;
                    case 2: return HatchStyle.Percent50;
                    case 3: return HatchStyle.Percent25;
                    default: return HatchStyle.Percent90;
                }
            }

            set
            {
                cbStyle.SelectedIndex = Global.IndexOfBlockStyle(value);
            }
        }

        public DrawColor DrawColor
        {
            get
            {
                if (Mode == BlockMode.Block)
                    return new DrawColor(Color, StyleBlocks);
                return new DrawColor(Color, StyleLines, LineWidth);
            }

            set
            {
                Color = value.color;
                if (Mode == BlockMode.Block)
                    StyleBlocks = value.styleBlock;
                else
                    StyleLines = value.style;
                LineWidth = value.width;
            }
        }

        private void pbSelect_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = pbSelect.BackColor;
            if (colorDialog1.ShowDialog(Parent) == DialogResult.OK)
            {
                pbSelect.BackColor = colorDialog1.Color;
            }
        }
    }

    public class TitledColorDialog : ColorDialog
    {
        private string m_strTitle = string.Empty;

        public TitledColorDialog(string title)
        {
            m_strTitle = title;
        }

        protected override IntPtr HookProc(IntPtr hWnd, int msg, IntPtr wparam, IntPtr lparam)
        {
            if (msg == NativeMethods.WM_INITDIALOG && !String.IsNullOrWhiteSpace(m_strTitle))
                NativeMethods.SetWindowText(hWnd, m_strTitle);

            return base.HookProc(hWnd, msg, wparam, lparam);
        }
    }
}
