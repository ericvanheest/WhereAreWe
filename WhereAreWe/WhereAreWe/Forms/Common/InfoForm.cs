using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class InfoForm : CommonKeyForm
    {
        private bool m_bUpdating = false;
        private MapBook m_book = null;
        private int m_iSheet = -1;
        private Image m_imgCustom = null;
        private ColorPattern m_cpUnvisited = MapBook.DefaultUnvisitedPattern;
        public WindowInfo SelectionFormInfo { get; set; }

        public InfoForm()
        {
            InitializeComponent();
        }

        public void SetUIFromBook(MapBook book, int iSheet)
        {
            m_book = book;
            m_iSheet = iSheet;
            m_bUpdating = true;
            tbBookTitle.Text = book.Title;
            tbBookNotes.Text = book.BookNote;
            if (iSheet < book.Sheets.Count)
            {
                MapSheet sheet = book.Sheets[iSheet];
                tbSheetTitle.Text = sheet.Title;
                nudGameMapIndex.Value = sheet.GameMapIndex;
                nudDefaultZoom.Value = (sheet.DefaultZoom > 0 ? sheet.DefaultZoom : 100);
                labelGridSize.Text = String.Format("Grid Size: {0} x {1}", sheet.GridWidth, sheet.GridHeight);
                tbSheetNotes.Text = sheet.SheetNote;
                cbUseCustomImage.Checked = !String.IsNullOrWhiteSpace(sheet.UnvisitedBitmapFile) && sheet.UseUnvisitedBitmap;
                tbCustomImage.Text = sheet.UnvisitedBitmapFile;
                Global.SetNud(nudCustomBottom, sheet.UnvisitedCrop.Bottom);
                Global.SetNud(nudCustomTop, sheet.UnvisitedCrop.Top);
                Global.SetNud(nudCustomLeft, sheet.UnvisitedCrop.Left);
                Global.SetNud(nudCustomRight, sheet.UnvisitedCrop.Right);
                Global.SetNud(nudGridBottom, sheet.UnvisitedGrid.Bottom-1);
                Global.SetNud(nudGridTop, sheet.UnvisitedGrid.Top);
                Global.SetNud(nudGridLeft, sheet.UnvisitedGrid.Left);
                Global.SetNud(nudGridRight, sheet.UnvisitedGrid.Right-1);
                SetSections(sheet.Sections);
            }
            labelNumberOfSheets.Text = String.Format("Number of sheets: {0}", book.Sheets.Count);
            cbIncreaseX.SelectedIndex = (int) book.Location.IncreaseX;
            cbIncreaseY.SelectedIndex = (int) book.Location.IncreaseY;
            nudGlobalXOffset.Value = book.Location.OffsetX;
            nudGlobalYOffset.Value = book.Location.OffsetY;
            rbDoorInSquare.Checked = book.QuickDoor == DoorType.FullSquare;
            rbDoorInWall.Checked = book.QuickDoor == DoorType.StraddleWall;
            tabControl1.SelectedIndex = 1;
            pbGridLines.BackColor = book.GridLines.Color;
            GridLineStyle = book.GridLines.Pattern;
            Global.SetNud(nudGridLineWidth, book.GridLines.Width);
            m_cpUnvisited = book.UnvisitedPattern;
            UpdateUnvisited();
            UpdateEnabledState();
            m_bUpdating = false;
            UpdateGridLineExample();
        }

        protected override bool OnCommonKeySelectAll()
        {
            Global.SelectAll(ActiveControl);
            return true;
        }

        public int DefaultZoom { get { return (int) nudDefaultZoom.Value; } set { Global.SetNud(nudDefaultZoom, value); } }

        private Rectangle RectangleFromNuds(NumericUpDown nudLeft, NumericUpDown nudTop, NumericUpDown nudRight, NumericUpDown nudBottom)
        {
            return new Rectangle((int)nudLeft.Value, (int)nudTop.Value, (int)(nudRight.Value - nudLeft.Value), (int)(nudBottom.Value - nudTop.Value));
        }

        public MapSection[] GetSections()
        {
            if (lvSections.Items.Count == 0)
                return null;
            MapSection[] sections = new MapSection[lvSections.Items.Count];
            for (int i = 0; i < lvSections.Items.Count; i++)
                sections[i] = lvSections.Items[i].Tag as MapSection;
            return sections;
        }

        public void SetSections(IEnumerable<MapSection> sections)
        {
            lvSections.BeginUpdate();
            lvSections.Items.Clear();
            if (sections != null)
            {
                foreach (MapSection section in sections)
                    AddSectionLVI(section);
            }
            lvSections.EndUpdate();
        }

        public void SetBookFromUI(MapBook book, int iSheet)
        {
            book.Title = tbBookTitle.Text;
            book.BookNote = tbBookNotes.Text;
            if (iSheet < book.Sheets.Count)
            {
                MapSheet sheet = book.Sheets[iSheet];
                sheet.Title = tbSheetTitle.Text;
                sheet.GameMapIndex = (int)nudGameMapIndex.Value;
                sheet.DefaultZoom = (int)nudDefaultZoom.Value;
                sheet.SheetNote = tbSheetNotes.Text;
                sheet.UnvisitedBitmapFile = tbCustomImage.Text;
                sheet.UseUnvisitedBitmap = cbUseCustomImage.Checked;
                sheet.UnvisitedCrop = RectangleFromNuds(nudCustomLeft, nudCustomTop, nudCustomRight, nudCustomBottom);
                sheet.UnvisitedGrid = RectangleFromNuds(nudGridLeft, nudGridTop, nudGridRight, nudGridBottom);
                sheet.UnvisitedGrid.Width++;
                sheet.UnvisitedGrid.Height++;
                sheet.Sections = GetSections();
            }
            book.Location.IncreaseX = (AxisIncreaseX) cbIncreaseX.SelectedIndex;
            book.Location.IncreaseY = (AxisIncreaseY) cbIncreaseY.SelectedIndex;
            book.Location.OffsetX = (int)nudGlobalXOffset.Value;
            book.Location.OffsetY = (int) nudGlobalYOffset.Value;
            book.QuickDoor = rbDoorInSquare.Checked ? DoorType.FullSquare : DoorType.StraddleWall;
            book.GridLines = new MapLineInfo(pbGridLines.BackColor, GridLineStyle, (int) nudGridLineWidth.Value);
            book.UnvisitedPattern = m_cpUnvisited;
        }

        private DashStyle GridLineStyle
        {
            get
            {
                switch (comboGridLineStyle.SelectedIndex)
                {
                    case 1: return DashStyle.Dot;
                    case 2: return DashStyle.Dash;
                    default: return DashStyle.Solid;
                }
            }

            set
            {
                switch (value)
                {
                    case DashStyle.Dot:
                        comboGridLineStyle.SelectedIndex = 1;
                        break;
                    case DashStyle.Dash:
                        comboGridLineStyle.SelectedIndex = 2;
                        break;
                    default:
                        comboGridLineStyle.SelectedIndex = 0;
                        break;
                }
            }
        }

        private void UpdateGridLineExample()
        {
            if (m_bUpdating)
                return;
            Bitmap bmp = new Bitmap(pbGridLineExample.Width, pbGridLineExample.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Properties.Settings.Default.DefaultGridBackground);
                Pen pen = new Pen(pbGridLines.BackColor, (int)nudGridLineWidth.Value);
                pen.DashStyle = GridLineStyle;
                for (int iRow = 0; iRow < bmp.Height; iRow += 16)
                    g.DrawLine(pen, 0, iRow, bmp.Width - 1, iRow);
                for (int iCol = 0; iCol < bmp.Width; iCol += 16)
                    g.DrawLine(pen, iCol, 0, iCol, bmp.Height - 1);
            }

            if (pbGridLineExample.Image != null)
                pbGridLineExample.Image.Dispose();

            pbGridLineExample.Image = bmp;
        }

        private void pbDefaultGridLines_Click(object sender, EventArgs e)
        {
            TitledColorDialog colors = new TitledColorDialog("Select grid line color");
            colors.Color = pbGridLines.BackColor;
            if (colors.ShowDialog() == DialogResult.OK)
            {
                pbGridLines.BackColor = colors.Color;
                UpdateGridLineExample();
            }
        }

        private void comboDefaultGridLineStyle_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateGridLineExample();
        }

        private void nudDefaultGridLineWidth_ValueChanged(object sender, EventArgs e)
        {
            UpdateGridLineExample();
        }

        private void llGridLineDefaults_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_bUpdating = true;
            MapLineInfo info = Global.DefaultGridLineInfo;
            pbGridLines.BackColor = info.Color;
            nudGridLineWidth.Value = info.Width;
            GridLineStyle = info.Pattern;
            m_bUpdating = false;
            UpdateGridLineExample();
        }

        private void pbUnvisited_Click(object sender, EventArgs e)
        {
            ColorPatternSelectForm form = new ColorPatternSelectForm("Select unvisited square style");
            form.DualColor = true;
            form.ColorPattern = m_cpUnvisited;
            if (form.ShowDialog() == DialogResult.OK)
            {
                m_cpUnvisited = form.ColorPattern;
                UpdateUnvisited();
            }
        }

        private void UpdateUnvisited()
        {
            if (pbUnvisited.Image != null)
                pbUnvisited.Image.Dispose();
            pbUnvisited.Image = Global.GetFillBitmap(m_cpUnvisited, pbUnvisited.Size);
        }

        private void llUnvisitedDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            m_cpUnvisited = MapBook.DefaultUnvisitedPattern;
            UpdateUnvisited();
        }

        private void cbUseCustomImage_CheckedChanged(object sender, EventArgs e)
        {
            UpdateEnabledState();
        }

        private void UpdateEnabledState()
        {
            bool bUse = cbUseCustomImage.Checked;
            tbCustomImage.ReadOnly = !bUse;
            nudGridBottom.Enabled = bUse;
            nudGridLeft.Enabled = bUse;
            nudGridRight.Enabled = bUse;
            nudGridTop.Enabled = bUse;
            nudCustomBottom.Enabled = bUse;
            nudCustomLeft.Enabled = bUse;
            nudCustomRight.Enabled = bUse;
            nudCustomTop.Enabled = bUse;
            llChoosePixels.Enabled = bUse;
            llGridFull.Enabled = bUse;
        }

        private void llGridFull_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SetFullCustomGrid();
        }

        private void SetFullCustomGrid()
        {
            Global.SetNud(nudGridTop, 0);
            Global.SetNud(nudGridLeft, 0);
            Global.SetNud(nudGridRight, m_book.Sheets[m_iSheet].GridWidth - 1);
            Global.SetNud(nudGridBottom, m_book.Sheets[m_iSheet].GridHeight - 1);
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            if (!cbUseCustomImage.Checked)
                cbUseCustomImage.Checked = true;
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select an image to use for hiding unvisited squares";
            ofd.FileName = String.Empty;
            ofd.Filter = "Common Image Types|*.jpg;*.png;*.bmp;*.gif|All Files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                LoadImage(ofd.FileName);
            }
        }

        private bool LoadImage(string strFile, bool bSetMax = true)
        {
            if (!File.Exists(strFile) && Directory.Exists(m_book.LastFile))
                strFile = Path.Combine(Path.GetDirectoryName(m_book.LastFile), strFile);

            if (!File.Exists(strFile))
            {
                MessageBox.Show(String.Format("The file \"{0}\" does not exist.", strFile), "File not found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            try
            {
                m_imgCustom = Image.FromFile(strFile);
                tbCustomImage.Text = strFile;
                if (bSetMax)
                {
                    Global.SetNud(nudCustomLeft, 0);
                    Global.SetNud(nudCustomTop, 0);
                    Global.SetNud(nudCustomRight, m_imgCustom.Width);
                    Global.SetNud(nudCustomBottom, m_imgCustom.Height);
                    nudCustomRight.Maximum = m_imgCustom.Width - 1;
                    nudCustomBottom.Maximum = m_imgCustom.Height - 1;
                }
                if (nudGridLeft.Value == nudGridRight.Value || nudGridTop.Value == nudGridBottom.Value)
                {
                    SetFullCustomGrid();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("The contents of the file \"{0}\" could not be read as an image.\r\n\r\nException: {1}", strFile, ex.Message),
                    "Unable to read image format", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void llChoosePixels_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_imgCustom == null)
            {
                if (!String.IsNullOrWhiteSpace(tbCustomImage.Text))
                {
                    if (!LoadImage(tbCustomImage.Text, false))
                        return;
                }
                else
                    return;
            }

            SelectImageRectangleForm form = new SelectImageRectangleForm();
            form.SetImage(m_imgCustom);
            form.SelectedRectangle = RectangleFromNuds(nudCustomLeft, nudCustomTop, nudCustomRight, nudCustomBottom);
            if (SelectionFormInfo != null)
            {
                form.StartPosition = FormStartPosition.Manual;
                form.WindowState = SelectionFormInfo.Maximized ? FormWindowState.Maximized : FormWindowState.Normal;
                form.FullSize = SelectionFormInfo.AutoShow;
                form.Location = SelectionFormInfo.NormalSize.Location;
                form.Width = SelectionFormInfo.NormalSize.Width;
                form.Height = SelectionFormInfo.NormalSize.Height;
            }
            if (form.ShowDialog() == DialogResult.OK)
            {
                Rectangle rc = form.SelectedRectangle;
                Global.SetNud(nudCustomLeft, rc.Left);
                Global.SetNud(nudCustomTop, rc.Top);
                Global.SetNud(nudCustomRight, rc.Width + rc.Left);
                Global.SetNud(nudCustomBottom, rc.Height + rc.Top);
            }
            SelectionFormInfo = new WindowInfo(form);
            SelectionFormInfo.AutoShow = form.FullSize;
        }

        private void tbCustomImage_MouseDown(object sender, MouseEventArgs e)
        {
            if (!cbUseCustomImage.Checked)
                cbUseCustomImage.Checked = true;
        }

        private void InfoForm_Load(object sender, EventArgs e)
        {
        }

        private void miCtxCustomCopy_Click(object sender, EventArgs e)
        {
            CustomImageCopy copy = new CustomImageCopy(tbCustomImage.Text,
                RectangleFromNuds(nudCustomLeft, nudCustomTop, nudCustomRight, nudCustomBottom),
                RectangleFromNuds(nudGridLeft, nudGridTop, nudGridRight, nudGridBottom));
            Clipboard.SetData(typeof(CustomImageCopy).FullName, copy);
        }

        private void miCtxCustomPaste_Click(object sender, EventArgs e)
        {
            IDataObject dataObj = Clipboard.GetDataObject();
            string format = typeof(CustomImageCopy).FullName;

            if (!dataObj.GetDataPresent(format))
                return;

            CustomImageCopy copy = dataObj.GetData(format) as CustomImageCopy;
            cbUseCustomImage.Checked = true;
            tbCustomImage.Text = copy.File;
            Global.SetNud(nudCustomBottom, copy.Crop.Bottom);
            Global.SetNud(nudCustomTop, copy.Crop.Top);
            Global.SetNud(nudCustomLeft, copy.Crop.Left);
            Global.SetNud(nudCustomRight, copy.Crop.Right);
            Global.SetNud(nudGridBottom, copy.Grid.Bottom);
            Global.SetNud(nudGridTop, copy.Grid.Top);
            Global.SetNud(nudGridLeft, copy.Grid.Left);
            Global.SetNud(nudGridRight, copy.Grid.Right);
        }

        private void cmCustomImage_Opening(object sender, CancelEventArgs e)
        {
            miCtxCustomCopy.Enabled = cbUseCustomImage.Checked;
            miCtxCustomPaste.Enabled = Clipboard.ContainsData(typeof(CustomImageCopy).FullName);
        }

        private void CustomImage_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                cmCustomImage.Show(Cursor.Position);
            }
        }

        private void llAddSection_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddSection();
        }

        private void AddSection()
        {
            EditMapSectionForm formEdit = new EditMapSectionForm(Rectangle.Empty, Point.Empty);
            if (formEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                MapSection section = formEdit.Section;
                AddSectionLVI(section);
            }
        }

        private void AddSectionLVI(MapSection section)
        {
            ListViewItem lvi = new ListViewItem(section.SourceString);
            lvi.SubItems.Add(section.TargetString);
            lvi.Tag = section;
            lvSections.Items.Add(lvi);
        }

        private void RemoveSelectedSections()
        {
            lvSections.BeginUpdate();
            foreach (ListViewItem lvi in lvSections.SelectedItems)
                lvi.Remove();
            lvSections.EndUpdate();
        }

        private void EditSelectedSection()
        {
            if (lvSections.SelectedItems.Count < 1)
                return;

            ListViewItem lvi = lvSections.SelectedItems[0];
            MapSection section = lvi.Tag as MapSection;
            if (section == null)
                return;

            EditMapSectionForm formEdit = new EditMapSectionForm(section);
            if (formEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                section = formEdit.Section;
                lvi.Text = section.SourceString;
                lvi.SubItems[1].Text = section.TargetString;
                lvi.Tag = section;
            }
        }

        private void llRemoveSection_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RemoveSelectedSections();
        }

        private void lvSections_SelectedIndexChanged(object sender, EventArgs e)
        {
            llRemoveSection.Enabled = lvSections.SelectedItems.Count > 0;
            llEditSection.Enabled = lvSections.SelectedItems.Count > 0;
        }

        private void llEditSection_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EditSelectedSection();
        }

        private void lvSections_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Delete:
                    RemoveSelectedSections();
                    break;
                case Keys.Insert:
                    AddSection();
                    break;
                case Keys.Enter:
                    EditSelectedSection();
                    break;
                default:
                    break;
            }
        }

        private void lvSections_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            EditSelectedSection();
        }
    }

    [Serializable]
    public class CustomImageCopy
    {
        public string File;
        public Rectangle Crop;
        public Rectangle Grid;

        public CustomImageCopy(string file, Rectangle crop, Rectangle grid)
        {
            File = file;
            Crop = crop;
            Grid = grid;
        }
    }
}
