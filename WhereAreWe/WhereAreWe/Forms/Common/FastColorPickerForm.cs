using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace WhereAreWe
{
    public partial class FastColorPickerForm : Form
    {
        private Bitmap m_bmp;
        private int m_iItemHeight = 16;
        private int m_iMaxIndex = 9;
        private int m_iSelected = 0;
        private bool m_bRectangles = false;
        private List<DrawColor> m_items;
        private DrawColor m_dcDefault = new DrawColor();

        public bool AnchorBottom { get; set; }
        public Point AnchorPoint { get; set; }
        public ToolBarCapture PickerType { get; set; }

        public event EventHandler ColorSelected;

        public FastColorPickerForm()
        {
            InitializeComponent();
            AnchorBottom = false;
            PickerType = ToolBarCapture.None;
        }

        public void SetAnchor(Point pt, bool bottom = false)
        {
            AnchorPoint = pt;
            AnchorBottom = bottom;

            if (AnchorBottom)
                Location = new Point(pt.X, pt.Y - Height);
            else
                Location = new Point(pt.X, pt.Y);
        }

        public void SetDrawColors(List<DrawColor> items, BlockMode mode)
        {
            m_items = items;
            m_iMaxIndex = items.Count-1;
            if (m_bmp == null || m_bmp.Height != (items.Count + 1) * m_iItemHeight)
            {
                if (m_bmp != null)
                    m_bmp.Dispose();
                m_bmp = new Bitmap(pbColors.Width, (items.Count + 1) * m_iItemHeight);
            }

            m_bRectangles = (mode != BlockMode.Line);
            Height = (Height - ClientRectangle.Height) + m_bmp.Height - (m_bRectangles ? (m_iItemHeight * 3 / 4) : 0);

            using(Graphics g = Graphics.FromImage(m_bmp))
            {
                g.FillRectangle(new SolidBrush(Properties.Settings.Default.DefaultGridBackground), pbColors.ClientRectangle);
                for (int i = 0; i < items.Count; i++)
                {
                    if (m_bRectangles)
                    {
                        Brush brush = new SolidBrush(items[i].color);
                        if (items[i].styleBlock != HatchStyle.Percent90)
                            brush = new HatchBrush(items[i].styleBlock, items[i].color, Properties.Settings.Default.DefaultGridBackground);
                        int iVert = (i + 1) * m_iItemHeight - (m_iItemHeight * 3 / 4);
                        g.FillRectangle(brush, 4, iVert, pbColors.Width - 8, m_iItemHeight * 3 / 4);
                    }
                    else
                    {
                        Pen pen = new Pen(items[i].color, items[i].width);
                        pen.DashStyle = items[i].style;

                        int iVert = (m_iItemHeight / 2) + (i * m_iItemHeight) + (m_iItemHeight / 2) - (items[i].width / 2);
                        g.DrawLine(pen, 2, iVert, pbColors.Width - 2, iVert);
                    }
                }
            }

            pbColors.Image = m_bmp;

            switch (mode)
            {
                case BlockMode.Notes:
                    Text = "Note Color";
                    break;
                case BlockMode.Line:
                    Text = "Line Style";
                    break;
                default:
                    Text = "Block Style";
                    break;
            }

            if (AnchorBottom)
                Location = new Point(AnchorPoint.X, AnchorPoint.Y - Height);
            else
                Location = new Point(AnchorPoint.X, AnchorPoint.Y);
        }

        public DrawColor DefaultColor
        {
            get { return m_dcDefault; }
            set { m_dcDefault = new DrawColor(value); }
        }

        public DrawColor SelectedColor
        {
            get
            {
                return (m_iSelected >= 0 && m_iSelected <= m_iMaxIndex ? new DrawColor(m_items[m_iSelected]) : m_dcDefault);
            }

            set
            {
                m_dcDefault = value;
            }
        }

        private void pbColors_MouseMove(object sender, MouseEventArgs e)
        {
            SelectColor(pbColors.PointToScreen(e.Location));
        }

        public void SelectColor(Point ptCursor)
        {
            ptCursor = pbColors.PointToClient(ptCursor);
            if (m_bmp == null)
                return;

            Pen penDraw = new Pen(Color.Black, 2);
            Pen penErase = new Pen(Properties.Settings.Default.DefaultGridBackground, 2);

            if (m_bRectangles)
                m_iSelected = ptCursor.Y / m_iItemHeight;
            else
                m_iSelected = (ptCursor.Y - m_iItemHeight / 2) / m_iItemHeight;

            if (ptCursor.Y < 0 || ptCursor.X < 0 || ptCursor.X > pbColors.Width)
                m_iSelected = -1;

            int iVert = 0;

            using (Graphics g = Graphics.FromImage(m_bmp))
            {
                if (m_bRectangles)
                {
                    for (int i = 0; i < pbColors.Height / m_iItemHeight; i++)
                    {
                        iVert = (i + 1) * m_iItemHeight - (m_iItemHeight * 3 / 4);
                        g.DrawRectangle(penErase, 1, iVert - 2, pbColors.Width - 2, m_iItemHeight);
                    }
                    if (m_iSelected >= 0 && m_iSelected <= m_iMaxIndex)
                    {
                        iVert = (m_iSelected + 1) * m_iItemHeight - (m_iItemHeight * 3 / 4);
                        g.DrawRectangle(penDraw, 1, iVert - 2, pbColors.Width - 2, m_iItemHeight);
                    }
                }
                else
                {
                    for (int i = 0; i < pbColors.Height / m_iItemHeight; i++)
                    {
                        iVert = (i + 1) * m_iItemHeight - (m_iItemHeight / 2);
                        g.DrawRectangle(penErase, 1, iVert - 2, pbColors.Width - 2, m_iItemHeight);
                    }
                    if (m_iSelected >= 0 && m_iSelected <= m_iMaxIndex)
                    {
                        iVert = (m_iSelected + 1) * m_iItemHeight - (m_iItemHeight / 2);
                        g.DrawRectangle(penDraw, 1, iVert - 2, pbColors.Width - 2, m_iItemHeight);
                    }
                }
            }

            pbColors.Image = m_bmp;
        }

        private void pbColors_Click(object sender, EventArgs e)
        {
            if (ColorSelected != null)
                ColorSelected(sender, e);
        }

        private void FastColorPickerForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    Hide();
                    break;
                default:
                    break;
            }
        }
    }
}
