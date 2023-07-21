using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class SelectImageRectangleForm : Form
    {
        private Image m_image = null;
        private Bitmap m_bmpSave = null;
        private Point m_ptCapture = Global.NullPoint;
        private Point m_ptCaptureScaled = Global.NullPoint;
        private Rectangle m_rcSelected = Rectangle.Empty;
        private Rectangle m_rcOriginalSelection = Rectangle.Empty;
        private Rectangle[] m_rgnSelected = null;
        private Timer m_timerUpdate = new Timer();
        private bool m_bChangingPictureBox = false;
        private Point m_ptCaptureMove = Global.NullPoint;
        private Point m_ptCaptureScrollbars = Global.NullPoint;
        private bool m_bDirty = false;
        private bool m_bMoveSelection = false;

        public SelectImageRectangleForm()
        {
            InitializeComponent();
            m_timerUpdate.Interval = 30;
            m_timerUpdate.Tick += m_timerUpdate_Tick;
            m_timerUpdate.Start();
        }

        public void SetImage(Image img)
        {
            m_image = img;
            SetImages(pbImage.Width, pbImage.Height);
            SelectedRectangle = new Rectangle(Point.Empty, img.Size);
        }

        public Rectangle SelectedRectangle
        {
            get { return m_rcSelected; }
            set { m_rcSelected = value; }
        }

        public bool FullSize
        {
            get { return cbFullSize.Checked; }
            set { cbFullSize.Checked = value; }
        }

        private void pbImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_image == null)
                return;

            if (m_ptCaptureMove != Global.NullPoint)
            {
                int iHoriz = 0;
                int iVert = 0;

                if (Properties.Settings.Default.ScrollStyle == ScrollStyle.LockScroll)
                {
                    double fScaleH = (double)(panelImageContainer.HorizontalScroll.Maximum - panelImageContainer.HorizontalScroll.Minimum) / (double)panelImageContainer.Width;
                    double fScaleV = (double)(panelImageContainer.VerticalScroll.Maximum - panelImageContainer.VerticalScroll.Minimum) / (double)panelImageContainer.Height;

                    iHoriz = m_ptCaptureScrollbars.X + (int)((Cursor.Position.X - m_ptCaptureMove.X) * fScaleH);
                    iVert = m_ptCaptureScrollbars.Y + (int)((Cursor.Position.Y - m_ptCaptureMove.Y) * fScaleV);
                }
                else
                {
                    iHoriz = m_ptCaptureScrollbars.X - (Cursor.Position.X - m_ptCaptureMove.X);
                    iVert = m_ptCaptureScrollbars.Y - (Cursor.Position.Y - m_ptCaptureMove.Y);
                }

                Global.FixRange(ref iHoriz, panelImageContainer.HorizontalScroll.Minimum, panelImageContainer.HorizontalScroll.Maximum);
                Global.FixRange(ref iVert, panelImageContainer.VerticalScroll.Minimum, panelImageContainer.VerticalScroll.Maximum);

                panelImageContainer.LastManualScroll = new Point(iHoriz, iVert);
                panelImageContainer.AutoScrollPosition = panelImageContainer.LastManualScroll;

                if (Math.Abs(m_ptCaptureMove.X - e.X) > 4 || Math.Abs(m_ptCaptureMove.Y - e.Y) > 4)
                    pbImage.BlockNextContextMenu = true;
                return;
            }

            Point pt = new Point(e.X, e.Y);

            Point ptScaled = new Point(pt.X * m_image.Width / pbImage.Width, pt.Y * m_image.Height / pbImage.Height);
            labelCursor.Text = String.Format("{0}, {1}", ptScaled.X, ptScaled.Y);

            if (e.Button == System.Windows.Forms.MouseButtons.Left && pbImage.Capture)
            {
                if (m_bMoveSelection)
                {
                    if (NativeMethods.IsShiftDown())
                        ptScaled = Global.ConstrainMove(ptScaled, m_ptCaptureScaled);

                    Point ptNew = new Point(m_rcOriginalSelection.X + (ptScaled.X - m_ptCaptureScaled.X),
                        m_rcOriginalSelection.Y + (ptScaled.Y - m_ptCaptureScaled.Y));

                    if (ptNew.X + m_rcSelected.Width > m_image.Width)
                        ptNew.X = m_image.Width - m_rcSelected.Width - 1;
                    if (ptNew.Y + m_rcSelected.Height > m_image.Height)
                        ptNew.Y = m_image.Height - m_rcSelected.Height - 1;
                    if (ptNew.X < 0)
                        ptNew.X = 0;
                    if (ptNew.Y < 0)
                        ptNew.Y = 0;

                    m_rcSelected.Location = ptNew;
                }
                else
                {
                    Global.FixRange(ref pt, 0, pbImage.Width - 1, 0, pbImage.Height - 1);
                    SetSelectionRect(pt);
                }
                UpdateSelection();
            }
        }

        private void SetSelectionRect(Point ptDest)
        {
            Rectangle rc = new Rectangle(m_ptCapture.X, m_ptCapture.Y, ptDest.X - m_ptCapture.X, ptDest.Y - m_ptCapture.Y);
            rc = Global.ScaleRect(rc, m_image.Size, pbImage.Size);
            if (NativeMethods.IsShiftDown())
            {
                if (Math.Abs(rc.Width) < Math.Abs(rc.Height))
                    rc.Height = Math.Sign(rc.Height) * Math.Abs(rc.Width);
                else
                    rc.Width = Math.Sign(rc.Width) * Math.Abs(rc.Height);
            }
            m_rcSelected = Global.NormalizeRect(rc);
            Global.FixRange(ref m_rcSelected, 0, 0, m_image.Width - 1, m_image.Height - 1);
        }

        private void UpdateLabels()
        {
            labelSelected.Text = String.Format("({0}, {1}) - ({2}, {3})  {4}x{5}",
                m_rcSelected.Left, m_rcSelected.Top, m_rcSelected.Right, m_rcSelected.Bottom, m_rcSelected.Width, m_rcSelected.Height);
        }

        void m_timerUpdate_Tick(object sender, EventArgs e)
        {
            if (!m_bDirty)
                return;

            if (m_rgnSelected != null)
            {
                // Copy the source data back from the previously-used region
                using (Graphics g = Graphics.FromImage(pbImage.Image))
                {
                    g.CompositingMode = CompositingMode.SourceCopy;
                    g.InterpolationMode = InterpolationMode.NearestNeighbor;
                    foreach (Rectangle rcRgn in m_rgnSelected)
                    {
                        g.DrawImage(m_bmpSave, rcRgn, rcRgn, GraphicsUnit.Pixel);
                        pbImage.Invalidate(rcRgn);
                    }
                }
            }

            Rectangle rc = Global.ScaleRect(m_rcSelected, pbImage.Size, m_image.Size);
            if (rc.Width < 1)
                rc.Width = 1;
            if (rc.Height < 1)
                rc.Height = 1;

            int iLine = 10;
            m_rgnSelected = new Rectangle[] {
                new Rectangle(rc.Location.X - iLine, rc.Location.Y - iLine, rc.Width + 2 * iLine, iLine),
                new Rectangle(rc.Location.X - iLine, rc.Bottom, rc.Width + 2 * iLine, iLine),
                new Rectangle(rc.Location.X - iLine, rc.Location.Y - iLine, iLine, rc.Height + 2 * iLine),
                new Rectangle(rc.Right, rc.Location.Y - iLine, iLine, rc.Height + 2 * iLine) };

            using (Graphics g = Graphics.FromImage(pbImage.Image))
            {
                HatchBrush brush = new HatchBrush(HatchStyle.Percent50, Color.Black, Color.White);
                foreach (Rectangle rcRgn in m_rgnSelected)
                {
                    g.FillRectangle(brush, rcRgn);
                    pbImage.Invalidate(rcRgn);
                }
            }
            m_bDirty = false;
        }

        private void UpdateSelection()
        {
            UpdateLabels();
            if (m_rcSelected.Width == 0 || m_rcSelected.Height == 0)
                return;

            Global.FixRange(ref m_rcSelected, 0, 0, m_image.Width - 1, m_image.Height - 1);
            m_rcSelected = Global.NormalizeRect(m_rcSelected);
            m_bDirty = true;
        }

        private void pbImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                m_bMoveSelection = NativeMethods.IsControlDown();
                m_ptCapture = new Point(e.X, e.Y);
                m_ptCaptureScaled = new Point(m_ptCapture.X * m_image.Width / pbImage.Width, m_ptCapture.Y * m_image.Height / pbImage.Height);

                if (m_bMoveSelection)
                    m_rcOriginalSelection = m_rcSelected;
                else
                {
                    Point ptScale = new Point(e.X * m_image.Width / pbImage.Width, e.Y * m_image.Height / pbImage.Height);
                    m_rcSelected = new Rectangle(ptScale, new Size(1, 1));
                    UpdateSelection();
                }
                pbImage.Capture = true;
            }
            else if (pbImage.Width > panelImageContainer.Width || pbImage.Height > panelImageContainer.Height)
            {
                m_ptCaptureMove = Cursor.Position;
                m_ptCaptureScrollbars = new Point(-panelImageContainer.AutoScrollPosition.X, -panelImageContainer.AutoScrollPosition.Y);
                pbImage.Capture = true;
            }
        }

        private void pbImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && pbImage.Capture)
            {
                pbImage.Capture = false;
                if (!m_bMoveSelection)
                    m_rcSelected = Global.ScaleRect(Global.NormalizeRect(new Rectangle(m_ptCapture.X, m_ptCapture.Y, e.X - m_ptCapture.X, e.Y - m_ptCapture.Y)), m_image.Size, pbImage.Size);
                UpdateLabels();
            }
            m_ptCaptureMove = Global.NullPoint;
            pbImage.BlockNextContextMenu = false;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (msg.Msg == NativeMethods.WM_KEYDOWN)
            {
                switch (keyData & ~(Keys.Shift | Keys.Control))
                {
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Left:
                    case Keys.Right:
                        OnArrowKeyPressed(keyData);
                        return true; // do not let the dialog process the key
                    default:
                        break;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void OnArrowKeyPressed(Keys keyData)
        {
            if (m_rcSelected == Rectangle.Empty || m_image == null)
                return;

            switch (keyData)
            {
                case Keys.Up:
                    if (m_rcSelected.Top > 0)
                        m_rcSelected.Offset(0, -1);
                    break;
                case Keys.Down:
                    if (m_rcSelected.Bottom < m_image.Height - 1)
                        m_rcSelected.Offset(0, 1);
                    break;
                case Keys.Left:
                    if (m_rcSelected.Left > 0)
                        m_rcSelected.Offset(-1, 0);
                    break;
                case Keys.Right:
                    if (m_rcSelected.Right < m_image.Width - 1)
                        m_rcSelected.Offset(1, 0);
                    break;
                case Keys.Up | Keys.Shift:
                    if (m_rcSelected.Top > 0)
                    {
                        m_rcSelected.Offset(0, -1);
                        m_rcSelected.Height += 1;
                    }
                    break;
                case Keys.Down | Keys.Shift:
                    if (m_rcSelected.Bottom < m_image.Height - 1)
                        m_rcSelected.Height += 1;
                    break;
                case Keys.Left | Keys.Shift:
                    if (m_rcSelected.Left > 0)
                    {
                        m_rcSelected.Offset(-1, 0);
                        m_rcSelected.Width += 1;
                    }
                    break;
                case Keys.Right | Keys.Shift:
                    if (m_rcSelected.Right < m_image.Width - 1)
                        m_rcSelected.Width += 1;
                    break;
                case Keys.Up | Keys.Control:
                    if (m_rcSelected.Height > 1)
                        m_rcSelected.Height -= 1;
                    break;
                case Keys.Down | Keys.Control:
                    if (m_rcSelected.Height > 1)
                    {
                        m_rcSelected.Offset(0, 1);
                        m_rcSelected.Height -= 1;
                    }
                    break;
                case Keys.Left | Keys.Control:
                    if (m_rcSelected.Width > 1)
                        m_rcSelected.Width -= 1;
                    break;
                case Keys.Right | Keys.Control:
                    if (m_rcSelected.Width > 1)
                    {
                        m_rcSelected.Offset(1, 0);
                        m_rcSelected.Width -= 1;
                    }
                    break;
                case Keys.Up | Keys.Control | Keys.Shift:
                    if (m_rcSelected.Y >= m_rcSelected.Height)
                        m_rcSelected.Y -= m_rcSelected.Height;
                    break;
                case Keys.Down | Keys.Control | Keys.Shift:
                    if (m_rcSelected.Y + (2 * m_rcSelected.Height) < m_image.Height - 1)
                        m_rcSelected.Y += m_rcSelected.Height;
                    break;
                case Keys.Left | Keys.Control | Keys.Shift:
                    if (m_rcSelected.X >= m_rcSelected.Width)
                        m_rcSelected.X -= m_rcSelected.Width;
                    break;
                case Keys.Right | Keys.Control | Keys.Shift:
                    if (m_rcSelected.X + (2 * m_rcSelected.Width) < m_image.Width - 1)
                        m_rcSelected.X += m_rcSelected.Width;
                    break;
                default:
                    break;
            }

            UpdateSelection();
        }

        private void panelImageContainer_SizeChanged(object sender, EventArgs e)
        {
            if (!m_bChangingPictureBox)
                CheckSizes();
        }

        private void pbImage_SizeChanged(object sender, EventArgs e)
        {
            if (!m_bChangingPictureBox)
                CheckSizes();
        }

        private void SetImages(int iWidth, int iHeight)
        {
            if (iWidth > m_image.Width)
                iWidth = m_image.Width;
            if (iHeight > m_image.Height)
                iHeight = m_image.Height;
            if (m_bmpSave != null && pbImage.Image != null && m_bmpSave.Size == pbImage.Image.Size && m_bmpSave.Width == iWidth && m_bmpSave.Height == iHeight)
            {
                CheckPBSize();
                return;
            }
            if (m_bmpSave != null)
                m_bmpSave.Dispose();
            m_bmpSave = new Bitmap(m_image, new Size(iWidth, iHeight));
            if (pbImage.Image != null)
                pbImage.Image.Dispose();
            pbImage.Image = new Bitmap(m_bmpSave);
            CheckPBSize();
            m_rgnSelected = null;
        }

        private void CheckPBSize()
        {
            if (pbImage.SizeMode == PictureBoxSizeMode.AutoSize)
                return;
            m_bChangingPictureBox = true;
            if (pbImage.Image.Width < pbImage.Width + SystemInformation.BorderSize.Width * 2)
                pbImage.Width = pbImage.Image.Width + SystemInformation.BorderSize.Width * 2;
            if (pbImage.Image.Height < pbImage.Height + SystemInformation.BorderSize.Width * 2)
                pbImage.Height = pbImage.Image.Height + SystemInformation.BorderSize.Width * 2;
            m_bChangingPictureBox = false;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void SelectImageRectangleForm_Load(object sender, EventArgs e)
        {
            UpdateSelection();
        }

        private void miCtxSelectAll_Click(object sender, EventArgs e)
        {
            if (m_image == null)
                return;
            m_rcSelected = new Rectangle(Point.Empty, m_image.Size);
            UpdateSelection();
        }

        private void cbFullSize_CheckedChanged(object sender, EventArgs e)
        {
            CheckSizes();
        }

        private void CheckSizes()
        {
            m_bChangingPictureBox = true;
            if (cbFullSize.Checked)
            {
                panelImageContainer.AutoScroll = true;
                pbImage.Dock = DockStyle.None;
                pbImage.SizeMode = PictureBoxSizeMode.AutoSize;
                SetImages(m_image.Width, m_image.Height);
                UpdateSelection();
            }
            else
            {
                panelImageContainer.AutoScroll = false;
                pbImage.Dock = DockStyle.None;
                pbImage.SizeMode = PictureBoxSizeMode.Normal;
                pbImage.Width = panelImageContainer.Width;
                pbImage.Height = panelImageContainer.Height;
                SetImages(pbImage.Width, pbImage.Height);
                UpdateSelection();
            }
            m_bChangingPictureBox = false;
        }

        private void SelectImageRectangleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_timerUpdate.Stop();
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
                if (m_bmpSave != null)
                    m_bmpSave.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
