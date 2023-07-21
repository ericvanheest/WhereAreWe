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
    public partial class IconsForm : Form
    {
        private Size m_szIcon = new Size(16, 16);
        private int m_iMargin = 2;
        private int m_iIconsAcross = 4;
        private Bitmap m_bmp;
        private MapIcon m_selectedIcon = null;
        private Point m_ptSelectedIcon = new Point(-1,-1);
        private ToolStripButton m_tsbSelected = null;
        private MapIcon[] m_icons;
        private Point m_ptLastHighlighted = new Point(-1, -1);
        private bool m_bSelectingColor = false;

        public event EventHandler OnIconSelected;

        public bool DeselectOnDeactivate { get; set; }

        public IconName[] IconNames = new IconName[] {
                IconName.ArrowFull, IconName.ArrowHalf, IconName.DoorFull, IconName.StairsDown, IconName.StairsUp, IconName.DoorHalf,
                IconName.FragileHalf, IconName.GrateFull, IconName.Exit, IconName.Safe, IconName.GrateHalf, IconName.Spinner,
                IconName.RotateCCW, IconName.RotateCW, IconName.Pentagram, IconName.LockedHalf, IconName.LockedFull,
                IconName.Slash, IconName.Corner, IconName.RotCorner, IconName.CurveLeft, IconName.CurveRight, IconName.Bar,
                IconName.X, IconName.Cross, IconName.Y, IconName.ArrowCap, IconName.ArrowCapDiag};

        public IconsForm()
        {
            InitializeComponent();
            pbColor.BackColor = Color.Black;
        }

        public IconsForm(ToolStripButton tsbSelected)
        {
            InitializeComponent();
            m_tsbSelected = tsbSelected;
        }

        public Color SelectedColor
        {
            get { return pbColor.BackColor; }
            set
            {
                pbColor.BackColor = value;
                SetIcons();
                if (m_selectedIcon != null)
                    m_selectedIcon.Color = pbColor.BackColor;
                DrawIcon();
            }
        }

        public void SetIcons(MapIcon[] icons)
        {
            m_icons = icons;
            int iWidth = (m_szIcon.Width + m_iMargin * 2) * m_iIconsAcross + (m_iMargin * 2);
            int iHeight = (m_szIcon.Height + m_iMargin * 2) * ((icons.Length + m_iIconsAcross - 1) / m_iIconsAcross) + (m_iMargin * 2);

            Width = (Width - ClientRectangle.Width) + iWidth;
            Height = (Height - ClientRectangle.Height + pbIcons.Top) + iHeight;

            if (m_bmp != null)
                m_bmp.Dispose();
            m_bmp = new Bitmap(iWidth, iHeight);

            int iX = 0, iY = 0;

            using (Graphics g = Graphics.FromImage(m_bmp))
            {
                foreach (MapIcon icon in icons)
                {
                    Rectangle rc = new Rectangle(m_iMargin + (iX * (m_szIcon.Width + 2 * m_iMargin)), m_iMargin + (iY * (m_szIcon.Height + 2 * m_iMargin)), m_szIcon.Width, m_szIcon.Height);
                    
                    Bitmap bmpImage = new Icon(icon.Image, m_szIcon).ToBitmap();
                    bmpImage.RotateFlip(icon.RotateCommand);

                    g.DrawImage(bmpImage, rc, 0, 0, bmpImage.Width, bmpImage.Height, GraphicsUnit.Pixel, Global.ReplaceColor(Color.Black, icon.Color));
                    iX++;
                    if (iX >= m_iIconsAcross)
                    {
                        iX = 0;
                        iY++;
                    }
                }
            }

            pbIcons.Image = m_bmp;
        }

        public void SelectNextIcon(int iCount)
        {
            if (m_selectedIcon == null)
            {
                SelectedIcon = new MapIcon(IconName.ArrowFull, Direction.Up, pbColor.BackColor);
                return;
            }

            for (int i = 0; i < IconNames.Length; i++)
            {
                if (m_selectedIcon.Name == IconNames[i])
                {
                    i += iCount;
                    if (i < 0)
                        i = IconNames.Length + i;
                    else if (i >= IconNames.Length)
                        i -= IconNames.Length;
                    SelectedIcon = new MapIcon(IconNames[i], SelectedIcon.Orientation, pbColor.BackColor);
                    return;
                }
            }
            SelectedIcon = new MapIcon(IconName.ArrowFull, Direction.Up, pbColor.BackColor);
        }

        public void SetIcons()
        {
            List<MapIcon> icons = new List<MapIcon>();
            foreach (IconName name in IconNames)
            {
                if (name == IconName.None)
                    continue;

                switch (name)
                {
                    // Icons with two default rotation options (0 and 90 degrees)
                    case IconName.DoorFull:
                    case IconName.GrateFull:
                    case IconName.LockedFull:
                    case IconName.Slash:
                    case IconName.Bar:
                        icons.Add(new MapIcon(name, Direction.Up, pbColor.BackColor));
                        icons.Add(new MapIcon(name, Direction.Right, pbColor.BackColor));
                        break;
                    // Icons with two default rotation options (0 and 180 degrees)
                    case IconName.Pentagram:
                        icons.Add(new MapIcon(name, Direction.Up, pbColor.BackColor));
                        icons.Add(new MapIcon(name, Direction.Down, pbColor.BackColor));
                        break;
                    // Icons with only one default rotation option
                    case IconName.Exit:
                    case IconName.Safe:
                    case IconName.StairsUp:
                    case IconName.StairsDown:
                    case IconName.RotateCCW:
                    case IconName.RotateCW:
                    case IconName.X:
                    case IconName.Cross:
                        icons.Add(new MapIcon(name, Direction.Up, pbColor.BackColor));
                        break;
                    // Icons with all four rotation options as the defaults
                    default:
                        icons.Add(new MapIcon(name, Direction.Up, pbColor.BackColor));
                        icons.Add(new MapIcon(name, Direction.Right, pbColor.BackColor));
                        icons.Add(new MapIcon(name, Direction.Down, pbColor.BackColor));
                        icons.Add(new MapIcon(name, Direction.Left, pbColor.BackColor));
                        break;
                }
            }

            SetIcons(icons.ToArray());
        }

        public MapIcon SelectedIcon
        {
            get
            {
                return m_selectedIcon;
            }

            set
            {
                m_selectedIcon = value;
                    DrawIcon();
            }
        }

        public void DrawIcon()
        {
            if (m_tsbSelected == null)
                return;

            Bitmap bmp = new Bitmap(m_szIcon.Width, m_szIcon.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(Brushes.White, 0, 0, bmp.Width, bmp.Height);

                if (m_selectedIcon != null)
                {
                    Rectangle rc = new Rectangle((bmp.Width - m_szIcon.Width) / 2, (bmp.Height - m_szIcon.Height) / 2, m_szIcon.Width, m_szIcon.Height);
                    Bitmap bmpIcon = new Icon(m_selectedIcon.Image, m_szIcon).ToBitmap();
                    g.DrawImage(bmpIcon, rc, 0, 0, bmpIcon.Width, bmpIcon.Height, GraphicsUnit.Pixel, Global.ReplaceColor(Color.Black, m_selectedIcon.Color));
                    bmp.RotateFlip(m_selectedIcon.RotateCommand);
                }
                Pen pen = new Pen(Color.Black, 1.0f);
                g.DrawRectangle(pen, 0, 0, bmp.Width - 1, bmp.Height - 1);
            }
            if (m_tsbSelected.Image != null)
                m_tsbSelected.Image.Dispose();

            m_tsbSelected.Image = bmp;
        }

        private void pbIcons_Click(object sender, EventArgs e)
        {
            Hide();
            if (m_icons == null)
            {
                SelectedIcon = null;
                return;
            }

            Point pt = SelectedPoint(Cursor.Position, true);

            SetSelectedIcon(pt);
        }

        public void SetSelectedIcon(Point pt)
        {
            m_ptSelectedIcon = new Point(-1, -1);

            if (pt.X >= m_iIconsAcross || pt.X < 0)
            {
                SelectedIcon = null;
                if (OnIconSelected != null)
                    OnIconSelected(this, new EventArgs());
                return;
            }

            int iIndex = pt.Y * m_iIconsAcross + pt.X;
            if (iIndex < 0 || iIndex >= m_icons.Length)
                SelectedIcon = null;
            else
            {
                SelectedIcon = m_icons[iIndex];
                m_ptSelectedIcon = pt;
            }
            if (OnIconSelected != null)
                OnIconSelected(this, new EventArgs());
        }

        public Point GetSelectedIcon()
        {
            return m_ptSelectedIcon;
        }

        private Point SelectedPoint(Point ptMouse, bool bScreen)
        {
            if (bScreen)
                ptMouse = pbIcons.PointToClient(ptMouse);
            if (ptMouse.Y < 0)
                return new Point(-1, -1);
            int iX = ptMouse.X / (m_szIcon.Width + 2 * m_iMargin);
            int iY = ptMouse.Y / (m_szIcon.Height + 2 * m_iMargin);
            return new Point(iX, iY);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void IconsForm_Deactivate(object sender, EventArgs e)
        {
            if (m_bSelectingColor)
                return;
            if (DeselectOnDeactivate)
                SelectedIcon = null;
            Hide();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
        }

        private Rectangle IconBorderRect(Point pt, int iExpand)
        {
            Rectangle rc = new Rectangle(m_iMargin + (pt.X * (m_szIcon.Width + 2 * m_iMargin)), m_iMargin + (pt.Y * (m_szIcon.Height + 2 * m_iMargin)), m_szIcon.Width, m_szIcon.Height);
            rc.Inflate(iExpand, iExpand);
            return rc;
        }

        private void pbIcons_MouseMove(object sender, MouseEventArgs e)
        {
            HighlightIcon(e.Location, false);
        }

        public void SetHighlightedAsSelected()
        {
            SetSelectedIcon(m_ptLastHighlighted); 
        }

        public void HighlightIcon(Point pt, bool bScreen)
        {
            if (m_bmp == null)
                return;

            if (m_icons == null)
                return;

            Point ptIcon = SelectedPoint(pt, bScreen);

            if (ptIcon == m_ptLastHighlighted)
                return;

            using (Graphics g = Graphics.FromImage(m_bmp))
            {
                g.DrawRectangle(new Pen(pbIcons.BackColor), IconBorderRect(m_ptLastHighlighted, 1));
                if ((ptIcon.Y * m_iIconsAcross + ptIcon.X) < m_icons.Length)
                    g.DrawRectangle(Pens.Black, IconBorderRect(ptIcon, 1));

                pbIcons.Image = m_bmp;
            }

            m_ptLastHighlighted = ptIcon;
        }

        private void pbColor_Click(object sender, EventArgs e)
        {
            if (SelectColor())
            {
                DeselectOnDeactivate = false;
                Hide();
            }
        }

        public bool PointOverColor(Point ptScreen)
        {
            Point ptClient = PointToClient(ptScreen);
            Rectangle rcLabel = labelColor.ClientRectangle;
            Rectangle rcColor = pbColor.ClientRectangle;
            rcLabel.Offset(labelColor.Location);
            rcColor.Offset(pbColor.Location);
            return (rcLabel.Contains(ptClient) || rcColor.Contains(ptClient));
        }

        public bool SelectColor()
        {
            m_bSelectingColor = true;
            TitledColorDialog cd = new TitledColorDialog("Select the icon color");
            cd.Color = SelectedColor;
            if (cd.ShowDialog() == DialogResult.OK)
            {
                SelectedColor = cd.Color;
                m_bSelectingColor = false;
                return true;
            }
            m_bSelectingColor = false;
            return false;
        }

        private void labelColor_Click(object sender, EventArgs e)
        {
            SelectColor();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                    components.Dispose();
                if (m_bmp != null)
                    m_bmp.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
