using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class ViewInfoForm : CommonKeyForm
    {
        private Timer m_timerHideSelection = new Timer();
        private string m_strItemText;
        private bool m_bInitialized = false;
        public int MaxHeight = -1;
        public int MaxWidth = 800;
        private bool m_bChangingSize = false;
        private Control m_ctrlCenterTo = null;

        public ViewInfoForm()
        {
            InitializeComponent();
            m_timerHideSelection.Tick += m_timerHideSelection_Tick;
            CommonKeySelectAll += ViewInfoForm_CommonKeySelectAll;
            CommonKeyRefresh += ViewInfoForm_CommonKeyRefresh;
        }

        void ViewInfoForm_CommonKeyRefresh(object sender, EventArgs e)
        {
            SizeToText();
        }

        void ViewInfoForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            tbInfo.SelectionStart = 0;
            tbInfo.SelectionLength = tbInfo.Text.Length;
        }

        public Control CenterTo 
        {
            get { return m_ctrlCenterTo; }
            set
            { 
                m_ctrlCenterTo = value;
                if (m_bInitialized)
                    Center();
            }
        }

        void m_timerHideSelection_Tick(object sender, EventArgs e)
        {
            m_timerHideSelection.Stop();
            tbInfo.SelectionStart = 0;
            tbInfo.SelectionLength = 0;
            tbInfo.Focus();
            tbInfo.Select();
            m_bInitialized = true;
            SizeToText();
            if (m_ctrlCenterTo != null)
                Center();
        }

        private void Center()
        {
            if (m_ctrlCenterTo is Form)
                Global.CenterForm(this, m_ctrlCenterTo.Bounds);
            else
                Global.CenterForm(this, m_ctrlCenterTo.RectangleToScreen(m_ctrlCenterTo.Bounds));
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public string ItemText
        {
            get { return tbInfo.Text; }

            set
            {
                m_strItemText = value;
                tbInfo.Text = value;
                if (m_bInitialized)
                    SizeToText();
            }
        }

        private void SizeToText(bool bChangeSize = true)
        {
            int iMaxWidth = bChangeSize ? MaxWidth : Width;
            iMaxWidth -= 27;

            m_bChangingSize = true;
            int iHeightTotal = 0;
            int iWidthTotal = 0;
            foreach (String str in tbInfo.Lines)
            {
                Size sz = TextRenderer.MeasureText(str, tbInfo.Font);
                if (sz.Height < tbInfo.PreferredHeight)
                    sz.Height = tbInfo.PreferredHeight;
                if (sz.Width > iMaxWidth)
                {
                    sz.Height = ((sz.Width / iMaxWidth + 1) * tbInfo.PreferredHeight);
                    sz.Width = iMaxWidth;
                }
                iHeightTotal += sz.Height;
                iWidthTotal = Math.Max(iWidthTotal, sz.Width);
            }

            int iMarginHoriz = Width - tbInfo.Width;
            int iMarginVert = Height - tbInfo.Height;
            if (bChangeSize)
                Width = iMarginHoriz + iWidthTotal + 10;
            int iNeedHeight = iMarginVert + iHeightTotal + 4;

            if (iNeedHeight <= Height)
                tbInfo.ScrollBars = ScrollBars.None;

            if (bChangeSize && (MaxHeight == -1 || iNeedHeight < MaxHeight))
                Height = iNeedHeight;
            else
            {
                if (bChangeSize)
                {
                    Height = Math.Min(MaxHeight, iMarginVert + iHeightTotal + 4);
                    Width += SystemInformation.VerticalScrollBarWidth;
                }
                if (iNeedHeight > Height)
                    tbInfo.ScrollBars = ScrollBars.Vertical;
            }
            m_bChangingSize = false;
        }

        private void ViewInfoForm_Load(object sender, EventArgs e)
        {
            m_timerHideSelection.Interval = 10;
            m_timerHideSelection.Start();
        }

        public static void Show(string strText, string strCaption, int iMaxHeight = -1)
        {
            ViewInfoForm form = new ViewInfoForm();
            form.MaxHeight = iMaxHeight;
            form.ItemText = strText;
            form.Text = strCaption;
            form.ShowDialog();
        }

        public static void ShowCentered(Control ctrlParent, string strText, string strCaption, int iMaxHeight = -1)
        {
            ViewInfoForm form = new ViewInfoForm();
            form.MaxHeight = iMaxHeight;
            form.ItemText = strText;
            form.Text = strCaption;
            form.CenterTo = ctrlParent;
            form.ShowDialog();
        }

        private void ViewInfoForm_SizeChanged(object sender, EventArgs e)
        {
            if (m_bChangingSize)
                return;
            SizeToText(false);
        }
    }
}
