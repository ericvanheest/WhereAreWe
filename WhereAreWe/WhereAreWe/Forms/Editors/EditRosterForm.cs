using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class EditRosterForm : CommonKeyForm
    {
        private int m_iLastSortColumn = -1;
        private bool m_bAscendingSort = true;
        private RosterFile m_roster = null;
        public bool RosterValid = false;
        public GameNames RosterGame = GameNames.None;

        private bool m_bDirty = false;

        public EditRosterForm()
        {
            InitializeComponent();

            NativeMethods.SetTooltipDelay(lvCharacters, 32000);

            CommonKeySelectAll += EditRosterForm_CommonKeySelectAll;
        }

        void EditRosterForm_CommonKeySelectAll(object sender, EventArgs e)
        {
            if (lvCharacters.Focused)
                Global.SelectAll(lvCharacters);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool SaveCurrentFile()
        {
            if (m_roster is MM3RosterFile || m_roster is MM4RosterFile || m_roster is MM5RosterFile)
            {
                if (MessageBox.Show("WARNING:  In Might and Magic 3-5, the portraits used for the characters are linked to the position of the character in the roster.\r\n\r\n" +
                    "If you save this file, your characters' portraits will change and it may be difficult to restore them.  Are you sure you want to reorganize the portraits in this way?",
                    "WARNING!  Portraits Will Change",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return false;
            }

            if (m_roster.Save(lvCharacters))
            {
                m_bDirty = false;
                btnSave.Enabled = false;
                LoadRosterFile(tbFilename.Text, false);
                return true;
            }

            return false;
        }

        public string RosterFilePath
        {
            get { return tbFilename.Text; }
            set { tbFilename.Text = value; }
        }

        private void lvCharacters_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvCharacters.ListViewItemSorter = new RosterItemComparer(e.Column, m_bAscendingSort);
            lvCharacters.Sort();
            SetDirty();
        }

        private void SetDirty(bool bDirty = true)
        {
            m_bDirty = bDirty;
            btnSave.Enabled = bDirty;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveCurrentFile();
        }

        private void LoadRosterFile(string strFileName, bool bSilent)
        {
            m_roster = RosterFile.Create(strFileName, bSilent);
            if (m_roster == null)
                return;

            if (!m_roster.Valid)
                return;

            RosterValid = true;
            RosterGame = m_roster.Game;

            if (m_roster is WizRosterFile)
            {
                chTown.Text = "AC";
                chStats.Text = "S/I/P/V/A/L";
                chAge.Text = "Age";
            }
            else if (m_roster is BT3RosterFile)
            {
                chTown.Text = "AC";
                chStats.Text = "S/I/D/C/L";
                chAge.Text = "Spells";
            }
            else if (m_roster is BTRosterFile)
            {
                chTown.Text = "AC";
                chStats.Text = "S/I/D/C/L";
                chAge.Text = "Spells";
            }
            else
            {
                chTown.Text = "Town";
                chStats.Text = "I/M/P/E/S/A/L";
                if (m_roster is MM1RosterFile || m_roster is MM2RosterFile)
                    lvCharacters.Columns[8].Text = "Age";
                else
                    lvCharacters.Columns[8].Text = "Born";
            }

            lvCharacters.BeginUpdate();
            lvCharacters.Items.Clear();
            lvCharacters.ListViewItemSorter = null;
            foreach (CharAndBytes cab in m_roster.Chars)
            {
                if (cab.Char == null)
                    continue;

                if (cab.Char.Name == null)
                    continue;

                if (!cab.Display)
                    continue;

                ListViewItem lvi = new ListViewItem(cab.Char.Name);
                if (cab.Char is WizCharacter)
                {
                    lvi.SubItems.Add(cab.PositionText);
                    lvi.SubItems.Add(cab.Char.BasicAC.Temporary.ToString());
                    lvi.SubItems.Add(cab.Char.BasicLevel.Permanent.ToString());
                    lvi.SubItems.Add(BaseCharacter.ClassString(cab.Char.BasicClass));
                    lvi.SubItems.Add(GetStats(cab.Char));
                    lvi.SubItems.Add(cab.Char.QuickRefHitPoints.Maximum.ToString());
                    lvi.SubItems.Add(cab.Char.QuickRefSpellPoints.Current);
                    lvi.SubItems.Add(cab.Char.BasicAge.Years.ToString());
                }
                else if (cab.Char is BTCharacter)
                {
                    lvi.SubItems.Add(cab.Position.ToString());
                    lvi.SubItems.Add(cab.Char.BasicAC.Temporary.ToString());
                    lvi.SubItems.Add(cab.Char.BasicLevel.Permanent.ToString());
                    lvi.SubItems.Add(BaseCharacter.ClassString(cab.Char.BasicClass));
                    lvi.SubItems.Add(GetStats(cab.Char));
                    lvi.SubItems.Add(cab.Char.QuickRefHitPoints.Maximum.ToString());
                    lvi.SubItems.Add(cab.Char.QuickRefSpellPoints.Current);
                    if (cab.Char is BT3Character)
                        lvi.SubItems.Add(((BT3Character)cab.Char).Spells.NumKnown.ToString());
                    else
                        lvi.SubItems.Add(((BTCharacter)cab.Char).SpellLevel.ToString());
                }
                else
                {
                    lvi.SubItems.Add(cab.PositionText);
                    lvi.SubItems.Add(cab.TownString(cab.Town));
                    lvi.SubItems.Add(cab.Char.BasicLevel.Permanent.ToString());
                    lvi.SubItems.Add(BaseCharacter.ClassString(cab.Char.BasicClass));
                    lvi.SubItems.Add(GetStats(cab.Char));
                    lvi.SubItems.Add(cab.Char.QuickRefHitPoints.Maximum.ToString());
                    lvi.SubItems.Add(cab.Char.QuickRefSpellPoints.Maximum.ToString());
                    lvi.SubItems.Add(cab.Char.BasicAge.Years.ToString());
                }
                lvi.Tag = cab;
                string strDescription = cab.Char.MultiLineDescription;
                if (strDescription.Length > 750)
                    strDescription = strDescription.Substring(0, 750) + "...";
                lvi.ToolTipText = strDescription;
                lvCharacters.Items.Add(lvi);
            }

            Global.SizeHeadersAndContent(lvCharacters);

            lvCharacters.EndUpdate();

            btnResetHirelings.Visible = (m_roster is MM2RosterFile);

            SetDirty(false);
        }

        private string GetStats(BaseCharacter character)
        {
            StringBuilder sb = new StringBuilder();
            foreach(StatAndModifier stat in character.PrimaryStats)
                sb.AppendFormat("{0}/", stat.Permanent);
            if (sb.Length > 0)
                sb.Remove(sb.Length-1,1);
            return sb.ToString();
        }

        private void EditRosterForm_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(RosterFilePath))
                tbFilename.Text = Global.CombineRoster(GameNames.MightAndMagic1);
            LoadRosterFile(tbFilename.Text, true);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            ofdRoster.FilterIndex = Games.GetFilterIndex(Properties.Settings.Default.Game);

            if (!String.IsNullOrWhiteSpace(tbFilename.Text))
            {
                try
                {
                    ofdRoster.InitialDirectory = Path.GetDirectoryName(tbFilename.Text);
                    ofdRoster.FileName = tbFilename.Text;
                }
                catch (Exception)
                {
                }
            }

            if (ofdRoster.ShowDialog() == DialogResult.OK)
            {
                LoadRosterFile(ofdRoster.FileName, false);
                if (m_roster != null && m_roster.Valid)
                    tbFilename.Text = ofdRoster.FileName;
                else
                {
                    MessageBox.Show(String.Format("The file \"{0}\" could not be recognized as a roster file.", ofdRoster.FileName),
                        "Invalid file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    tbFilename.Text = String.Empty;
                }
            }
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadRosterFile(tbFilename.Text, false);
        }

        private void EditRosterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_bDirty)
                return;

            switch (MessageBox.Show("Do you want to save changes to " + Path.GetFileName(tbFilename.Text) + "?", "Exit Roster Editor", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
            {
                case DialogResult.Yes:
                    if (!SaveCurrentFile())
                        e.Cancel = true;
                    break;
                case DialogResult.No:
                    break;
                case DialogResult.Cancel:
                    e.Cancel = true;
                    break;
                default:
                    break;
            }
        }

        private void lvCharacters_ItemsRearranged(object sender, EventArgs e)
        {
            SetDirty();
        }

        private void btnResetHirelings_Click(object sender, EventArgs e)
        {
            if (!(m_roster is MM2RosterFile))
                return;

            if (MessageBox.Show("This will remove all hirelings from the towns, reset their stats, and place them back into the game where they may be found again.\r\n\r\n" +
                "Are you sure you wish to do this?", "Reset All Hirelings", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
                return;

            m_roster.ResetHirelings();
            SetDirty();
        }
    }

    class RosterItemComparer : IComparer
    {
        private int m_column;
        private bool m_bAscending;

        public RosterItemComparer()
        {
            m_column = 0;
            m_bAscending = true;
        }
        public RosterItemComparer(int column, bool bAscending)
        {
            m_column = column;
            m_bAscending = bAscending;
        }
        public int Compare(object x, object y)
        {
            int returnVal = -1;
            CharAndBytes cab1 = ((ListViewItem)x).Tag as CharAndBytes;
            CharAndBytes cab2 = ((ListViewItem)y).Tag as CharAndBytes;

            switch (m_column)
            {
                case 0:
                    returnVal = String.Compare(cab1.Char.Name, cab2.Char.Name);
                    break;
                case 1:
                    returnVal = Math.Sign(cab1.Position - cab2.Position);
                    break;
                case 2:
                    returnVal = String.Compare(((ListViewItem)x).SubItems[m_column].Text, ((ListViewItem)y).SubItems[m_column].Text);
                    break;
                case 3:
                    returnVal = Math.Sign(cab1.Char.BasicLevel.Permanent - cab2.Char.BasicLevel.Permanent);
                    break;
                case 4:
                    returnVal = String.Compare(((ListViewItem)x).SubItems[m_column].Text, ((ListViewItem)y).SubItems[m_column].Text);
                    break;
                case 5:
                    returnVal = String.Compare(((ListViewItem)x).SubItems[m_column].Text, ((ListViewItem)y).SubItems[m_column].Text);
                    break;
                case 6:
                    returnVal = Math.Sign(cab1.Char.QuickRefHitPoints.Maximum - cab2.Char.QuickRefHitPoints.Maximum);
                    break;
                case 7:
                    if (cab1.Char.QuickRefSpellPoints is MMSpellPoints)
                        returnVal = Math.Sign(((MMSpellPoints)cab1.Char.QuickRefSpellPoints).MaximumSP - ((MMSpellPoints)cab2.Char.QuickRefSpellPoints).MaximumSP);
                    else
                        returnVal = String.Compare(cab1.Char.QuickRefSpellPoints.Current, cab2.Char.QuickRefSpellPoints.Current);
                    break;
                case 8:
                    if (cab1.Char is BTCharacter)
                        returnVal = String.Compare(((ListViewItem)x).SubItems[m_column].Text, ((ListViewItem)y).SubItems[m_column].Text);
                    else
                        returnVal = Math.Sign(cab1.Char.BasicAge.Years - cab2.Char.BasicAge.Years);
                    break;
            }

            return m_bAscending ? returnVal : -returnVal;
        }
    }

    public class CharAndBytes
    {
        public BaseCharacter Char;
        public int Town;
        public byte[] Bytes;
        public int Position;

        public CharAndBytes()
        {
        }

        public CharAndBytes(byte[] bytes)
        {
            Bytes = bytes;
        }

        public virtual string TownString(int i) { return "Unknown"; }
        public virtual string PositionText { get { return String.Format("{0}", (char) ('A' + Position)); } }
        public virtual bool Display { get { return true; } }
        public virtual bool IsInventoryChar { get { return Char.Name.ToLower() == "inventory"; } }

        public override string ToString() { return String.Format("Length {0}, {1}", Bytes == null ? 0 : Bytes.Length, Char == null ? "<null>" : Char.Name); }
    }

    public class MM1CharAndBytes : CharAndBytes
    {
        public MM1CharAndBytes(byte[] bytes, int iIndex, byte town, bool bRoster)
        {
            if (bytes[iIndex] == 0 || town < 1 || town > 5)
                Char = null;
            else
                Char = MM1Character.Create(bytes, iIndex, bRoster);
            Bytes = new byte[bRoster ? MM1Character.SizeInBytes-1 : MM1Character.SizeInBytes];
            Buffer.BlockCopy(bytes, iIndex, Bytes, 0, bRoster ? MM1Character.SizeInBytes - 1 : MM1Character.SizeInBytes);
            Town = town;
            Position = bytes[iIndex + 126];
        }

        public override string TownString(int i)
        {
            switch (i)
            {
                case 0: return "None";
                case 1: return "Sorpigal";
                case 2: return "Portsmith";
                case 3: return "Algary";
                case 4: return "Dusk";
                case 5: return "Erliquin";
                default: return "Unknown";
            }
        }
    }

    public class MM2CharAndBytes : CharAndBytes
    {
        public MM2CharAndBytes(byte[] bytes, int iIndex, int pos)
        {
            Char = MM2Character.Create(bytes, iIndex);
            Bytes = new byte[MM2Character.SizeInBytes];
            Buffer.BlockCopy(bytes, iIndex, Bytes, 0, MM2Character.SizeInBytes);
            Town = ((MM2Character)Char).Town;
            Position = pos;
        }

        public override string TownString(int i)
        {
            switch (i)
            {
                case 0: return "None";
                case 1: return "Middlegate";
                case 2: return "Atlantium";
                case 3: return "Tundara";
                case 4: return "Vulcania";
                case 5: return "Sandsobar";
                default: return "Unknown";
            }
        }
    }

    public class MM3CharAndBytes : CharAndBytes
    {
        public MM3CharAndBytes(byte[] bytes, int iIndex, int pos)
        {
            Char = MM3Character.Create(bytes, iIndex, null);
            Bytes = new byte[MM3Character.SizeInBytes];
            Buffer.BlockCopy(bytes, iIndex, Bytes, 0, MM3Character.SizeInBytes);
            Town = ((MM3Character)Char).Town;
            Position = pos;
        }

        public override string TownString(int i)
        {
            switch (i)
            {
                case 0: return "None";
                case 1: return "Fountain Head";
                case 2: return "Bay Watch";
                case 3: return "Wildabar";
                case 4: return "Swamp Town";
                case 5: return "Blistering Heights";
                default: return "Unknown";
            }
        }

        public override string PositionText { get { return String.Format("{0}", Position+1); } }

        public override bool Display { get { return (Town >= 1 && Town <= 5); } }
    }

    public class MM45CharAndBytes : CharAndBytes
    {
        public MM45CharAndBytes(byte[] bytes, int iIndex, int pos)
        {
            Char = MM45Character.Create(bytes, iIndex, null);
            Bytes = new byte[MM45Character.SizeInBytes];
            Buffer.BlockCopy(bytes, iIndex, Bytes, 0, MM45Character.SizeInBytes);
            Town = ((MM45Character)Char).Town + (((MM45Character)Char).SaveSide == 1 ? 256 : 0);
            Position = pos;
        }

        public override string TownString(int i)
        {
            return MM45MemoryHacker.GetMapTitlePair(i % 256, i / 256).Title;
        }

        public override string PositionText { get { return String.Format("{0}", Position + 1); } }

        public override bool Display
        { 
            get 
            {
                return (Town >= (int) MM4Map.A1Surface && Town < (int) MM4Map.LastMain) ||
                    (Town >= (int)MM5Map.A1Surface + 256 && Town < (int) MM5Map.LastMain + 256);
            }
        }
    }

    public class Wiz1234CharAndBytes : CharAndBytes
    {
        public Wiz1234CharAndBytes(byte[] bytes, int offset, int pos, GameNames game)
        {
            Char = WizCharacter.Create(game, 0, bytes, offset, null, true);
            Bytes = new byte[WizCharacter.SizeInBytes];
            Buffer.BlockCopy(bytes, offset, Bytes, 0, Bytes.Length);
            Position = pos;
        }

        public override bool Display
        {
            get
            {
                return !String.IsNullOrWhiteSpace(Char.Name) && ((WizCharacter) Char).Condition != WizCondition.Lost;
            }
        }
    }

    public class Wiz5CharAndBytes : CharAndBytes
    {
        public Wiz5CharAndBytes(byte[] bytes, int offset, int pos)
        {
            Char = Wiz5Character.Create(0, bytes, offset, null, null);
            Bytes = new byte[Wiz5Character.SizeInBytes];
            Buffer.BlockCopy(bytes, offset, Bytes, 0, Bytes.Length);
            Position = pos;
        }

        public override bool Display
        {
            get
            {
                return !String.IsNullOrWhiteSpace(Char.Name) && ((Wiz5Character)Char).Condition != WizCondition.Lost;
            }
        }
    }

    public class BT1CharAndBytes : CharAndBytes
    {
        public BT1CharAndBytes(byte[] bytes, int iIndex)
        {
            Char = BTCharacter.Create(GameNames.BardsTale1, iIndex, bytes, 0);
            Bytes = bytes;
            Position = iIndex;
        }
    }

    public class BT2CharAndBytes : CharAndBytes
    {
        public BT2CharAndBytes(byte[] bytes, int iIndex)
        {
            Char = BTCharacter.Create(GameNames.BardsTale2, iIndex, bytes, 0);
            Bytes = bytes;
            Position = iIndex;
        }
    }

    public class BT3CharAndBytes : CharAndBytes
    {
        public BT3CharAndBytes(byte[] bytes, int iIndex)
        {
            Char = BTCharacter.Create(GameNames.BardsTale3, iIndex, bytes, iIndex);
            Bytes = Global.Subset(bytes, iIndex, BT3Character.SizeInBytes);
            Position = iIndex / BT3Character.SizeInBytes;
        }

        public BT3CharAndBytes(byte[] bytes)
        {
            Char = BTCharacter.Create(GameNames.BardsTale3, 0, bytes, 0);
            Bytes = Global.Subset(bytes, 0, BT3Character.SizeInBytes);
            Position = 0;
        }
    }

    public class RosterFile
    {
        public List<CharAndBytes> Chars;
        public string FileName;
        protected byte[] m_bytesRoster;
        public bool Valid;
        public GameNames Game;

        protected virtual byte[] ReadAllBytes(string strFileName) { return File.ReadAllBytes(strFileName); }

        public RosterFile()
        {
            Game = GameNames.None;
        }

        public void InitRosterFile(string strFileName, bool bSilent)
        {
            m_bytesRoster = null;
            Valid = false;

            try
            {
                FileInfo info = new FileInfo(strFileName);
                if (info.Exists && !IsAcceptableSize(info.Length))
                {
                    if (!bSilent)
                    {
                        MessageBox.Show(String.Format("Not a valid roster file: {0} (expected {1} bytes, found {2})", strFileName, ExpectedSize, info.Length),
                            "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (!info.Exists)
                {
                    if (!bSilent)
                    {
                        MessageBox.Show(String.Format("File does not exist: {0}", strFileName),
                            "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    m_bytesRoster = ReadAllBytes(strFileName);
            }
            catch (Exception ex)
            {
                if (!bSilent)
                {
                    MessageBox.Show("Could not load file: " + strFileName + "\r\n\r\nException: " + ex.Message,
                        "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (m_bytesRoster == null || !IsAcceptableSize(m_bytesRoster.Length))
                m_bytesRoster = null;
            else
                Valid = true;
        }

        public static RosterFile Create(string strFile, bool bSilent)
        {
            FileInfo info = new FileInfo(strFile);
            if (!info.Exists)
                return null;

            if (MM1RosterFile.AcceptableSize(info.Length))
                return new MM1RosterFile(strFile, bSilent);
            else if (MM2RosterFile.AcceptableSize(info.Length))
                return new MM2RosterFile(strFile, bSilent);
            else if (MM3RosterFile.AcceptableSize(info.Length))
                return new MM3RosterFile(strFile, bSilent);
            else if (MM4RosterFile.AcceptableSize(info.Length))
                return new MM4RosterFile(strFile, bSilent);
            else if (MM5RosterFile.AcceptableSize(info.Length))
                return new MM5RosterFile(strFile, bSilent);
            else if (WizRosterFile.AcceptableSize(info.Length))
            {
                if (strFile.ToLower().EndsWith("save5.dsk"))
                    return Wiz5RosterFile.CreateWiz5(strFile, bSilent);
                if (strFile.ToLower().EndsWith("save4.dsk"))
                    return Wiz4RosterFile.CreateWiz4(strFile, bSilent);
                if (strFile.ToLower().EndsWith("save3.dsk"))
                    return Wiz3RosterFile.CreateWiz3(strFile, bSilent);
                if (strFile.ToLower().EndsWith("save2.dsk"))
                    return Wiz2RosterFile.CreateWiz2(strFile, bSilent);
                return Wiz1RosterFile.CreateWiz1(strFile, bSilent);
            }
            else if (BT1RosterFile.AcceptableSize(info.Length))
                return new BT1RosterFile(strFile, bSilent);
            else if (BT2RosterFile.AcceptableSize(info.Length))
                return new BT2RosterFile(strFile, bSilent);
            else if (BT3RosterFile.AcceptableFile(strFile))
                return new BT3RosterFile(strFile, bSilent);

            return null;
        }

        public virtual int ExpectedSize { get { return -1; } }
        public virtual bool Save(ListView lvCharacters) { return false;  }
        public virtual bool ResetHirelings() { return false; }
        public virtual int SaveCharBytes(int iChar) { return -1; }
        public virtual int SaveCharBytes(int iChar, int town, byte[] bytes) { return -1; }
        public virtual byte[] LoadCharBytes(int iChar) { return new byte[0]; }
        public virtual bool IsAcceptableSize(long iSize) { return iSize == ExpectedSize; }
    }

    public class MM1RosterFile : RosterFile
    {
        public static int GetExpectedSize() { return 2304; }
        public static bool AcceptableSize(long iSize) { return iSize == GetExpectedSize(); }
        public override int ExpectedSize { get { return GetExpectedSize(); } }

        public MM1RosterFile(string strFileName, bool bSilent)
        {
            Game = GameNames.MightAndMagic1;
            InitRosterFile(strFileName, bSilent);
            if (m_bytesRoster == null)
                return;

            byte[] bytesTowns = new byte[18];
            Buffer.BlockCopy(m_bytesRoster, 0x8ee, bytesTowns, 0, 18);

            Chars = new List<CharAndBytes>(18);
            for (int i = 0; i < 18; i++)
            {
                Chars.Add(new MM1CharAndBytes(m_bytesRoster, i * 127, bytesTowns[i], true));
            }

            FileName = strFileName;
        }

        public override byte[] LoadCharBytes(int iChar)
        {
            if (iChar > 17)
                return null;

            byte[] bytes = new byte[127];
            try
            {
                FileStream fs = File.Open(FileName, FileMode.Open);
                fs.Seek(iChar * 127, SeekOrigin.Begin);
                fs.Read(bytes, 0, 127);
                fs.Seek(0x8ee + iChar, SeekOrigin.Begin);
                byte town = (byte) fs.ReadByte();
                fs.Close();
                return bytes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not read from file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public override int SaveCharBytes(int iChar)
        {
            return SaveCharBytes(iChar, Chars[iChar].Town, Chars[iChar].Bytes);
        }

        public override int SaveCharBytes(int iChar, int town, byte[] bytes)
        {
            if (iChar > 17)
                return -1;

            try
            {
                if (bytes.Length > 126)
                    bytes[126] = (byte) iChar;

                FileStream fs = File.Open(FileName, FileMode.Open);
                fs.Seek(iChar * 127, SeekOrigin.Begin);
                fs.Write(bytes, 0, Math.Min(127, bytes.Length));
                if (town < 6)
                {
                    fs.Seek(0x8ee + iChar, SeekOrigin.Begin);
                    fs.WriteByte((byte) town);
                }
                fs.Close();
                return iChar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return -1;
        }

        public override bool Save(ListView lvCharacters)
        {
            byte iPosition = 0;
            MemoryStream stream = new MemoryStream(127 * 18 + 18);
            byte[] zeros = Global.NullBytes(127);

            byte[] towns = new byte[18];
            for (int i = 0; i < 18; i++)
                zeros[i] = 0;

            List<int> invChars = new List<int>();

            foreach (ListViewItem lvi in lvCharacters.Items)
            {
                CharAndBytes cab = (CharAndBytes)lvi.Tag;
                if (cab.IsInventoryChar)
                {
                    invChars.Add(lvi.Index);
                    continue;       // Leave these at the end
                }

                cab.Position = iPosition;
                cab.Bytes[126] = iPosition;
                stream.Write(cab.Bytes, 0, 127);
                towns[iPosition] = (byte) cab.Town;
                iPosition++;
            }
            while (iPosition < (18 - invChars.Count))
            {
                stream.Write(zeros, 0, 126);
                stream.WriteByte((byte)iPosition);
                iPosition++;
            }
            foreach (int iInvIndex in invChars)
            {
                CharAndBytes cab = (CharAndBytes)lvCharacters.Items[iInvIndex].Tag;
                cab.Position = iPosition;
                cab.Bytes[126] = iPosition;
                stream.Write(cab.Bytes, 0, 127);
                towns[iPosition] = (byte) cab.Town;
                iPosition++;
            }

            stream.Write(towns.ToArray(), 0, 18);

            try
            {
                File.WriteAllBytes(FileName, stream.ToArray());
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }

    public class MM2RosterFile : RosterFile
    {
        public static int GetExpectedSize() { return 8293; }
        public static bool AcceptableSize(long iSize) { return iSize == GetExpectedSize() || iSize == 8292; }
        public override int ExpectedSize { get { return GetExpectedSize(); } }
        public override bool IsAcceptableSize(long iSize) { return AcceptableSize(iSize); }

        public List<CharAndBytes> Hirelings;
        public const int iCurrentPartyBytes = 6280;

        public MM2RosterFile(string strFileName, bool bSilent)
        {
            Game = GameNames.MightAndMagic2;
            InitRosterFile(strFileName, bSilent);
            if (m_bytesRoster == null)
                return;

            Chars = new List<CharAndBytes>(24);
            Hirelings = new List<CharAndBytes>(24);
            for (int i = 0; i < 48; i++)
            {
                if (m_bytesRoster[i * MM2Character.SizeInBytes] == 0)
                    continue;
                if (m_bytesRoster[i * MM2Character.SizeInBytes + 11] < 1 || m_bytesRoster[i * MM2Character.SizeInBytes + 11] > 5)
                    continue;
                if (i < 24)
                    Chars.Add(new MM2CharAndBytes(m_bytesRoster, i * MM2Character.SizeInBytes, i));
                else
                    Hirelings.Add(new MM2CharAndBytes(m_bytesRoster, i * MM2Character.SizeInBytes, i));
            }
            FileName = strFileName;
        }

        public override bool ResetHirelings()
        {
            byte[] resetHirelings = Properties.Resources.MM2_Hirelings;
            Hirelings = new List<CharAndBytes>(24);
            for (int i = 0; i < 24; i++)
            {
                Hirelings.Add(new MM2CharAndBytes(resetHirelings, i * MM2Character.SizeInBytes, i + 24));
                m_bytesRoster[0x202e + i] = 0;
            }
            return true;
        }

        public override bool Save(ListView lvCharacters)
        {
            byte[] bytesDeleted = Global.NullBytes(MM2Character.SizeInBytes);

            // Only save the first 24 characters; the other bytes are for hirelings, 
            // cartography data and whatnot

            if (m_bytesRoster == null)
                return false;

            // If we rearrange the characters in the roster, we have to fix the current party bytes
            byte[] bytesCurrentParty = new byte[16];
            Buffer.BlockCopy(m_bytesRoster, iCurrentPartyBytes, bytesCurrentParty, 0, bytesCurrentParty.Length);

            MemoryStream stream = new MemoryStream(lvCharacters.Items.Count * MM2Character.SizeInBytes);
            Dictionary<int, int> dictPositionMap = new Dictionary<int, int>(24);

            List<int> invChars = new List<int>();

            int iCount = 0;
            foreach (ListViewItem lvi in lvCharacters.Items)
            {
                CharAndBytes cab = (CharAndBytes)lvi.Tag;
                if (cab.IsInventoryChar)
                {
                    invChars.Add(lvi.Index);
                    continue;       // Leave these at the end
                }

                stream.Write(cab.Bytes, 0, MM2Character.SizeInBytes);
                dictPositionMap.Add(cab.Position, iCount);
                iCount++;
            }

            while (iCount < (24 - invChars.Count))
            {
                // Erase any other characters in the roster
                stream.Write(bytesDeleted, 0, bytesDeleted.Length);
                iCount++;
            }

            foreach (int iInvIndex in invChars)
            {
                CharAndBytes cab = (CharAndBytes)lvCharacters.Items[iInvIndex].Tag;
                stream.Write(cab.Bytes, 0, MM2Character.SizeInBytes);
                dictPositionMap.Add(cab.Position, iCount);
                iCount++;
            }

            for (int i = 0; i < bytesCurrentParty.Length; i += 2)
            {
                if (dictPositionMap.ContainsKey(bytesCurrentParty[i]))
                    bytesCurrentParty[i] = (byte)dictPositionMap[bytesCurrentParty[i]];
            }

            Buffer.BlockCopy(stream.ToArray(), 0, m_bytesRoster, 0, (int) stream.Length);

            // Also save the hirelings, since they can be reset
            stream = new MemoryStream(Hirelings.Count * MM2Character.SizeInBytes);
            foreach (CharAndBytes cab in Hirelings)
            {
                stream.Write(cab.Bytes, 0, MM2Character.SizeInBytes);
            }

            Buffer.BlockCopy(stream.ToArray(), 0, m_bytesRoster, 24 * MM2Character.SizeInBytes, (int)stream.Length);
            Buffer.BlockCopy(bytesCurrentParty, 0, m_bytesRoster, iCurrentPartyBytes, bytesCurrentParty.Length);

            try
            {
                File.WriteAllBytes(FileName, m_bytesRoster);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }

    public class MM3RosterFile : RosterFile
    {
        public const int ExpectedLength = 207551;
        public static long[] AcceptableLengths = new long[] { ExpectedLength };
        public static int GetExpectedSize() { return ExpectedLength; }
        public override int ExpectedSize { get { return GetExpectedSize(); } }
        public static bool AcceptableSize(long iSize) { return iSize == GetExpectedSize(); }
        public List<CharAndBytes> Hirelings;
        public const int iHeaderBytes = 1953;
        public const int iCurrentPartyBytes = 11045;

        public MM3RosterFile(string strFileName, bool bSilent)
        {
            Game = GameNames.MightAndMagic3;
            InitRosterFile(strFileName, bSilent);
            if (m_bytesRoster == null)
                return;

            Chars = new List<CharAndBytes>(20);
            Hirelings = new List<CharAndBytes>(10);
            for (int i = 0; i < 30; i++)
            {
                if (i < 20)
                    Chars.Add(new MM3CharAndBytes(m_bytesRoster, iHeaderBytes + i * MM3Character.SizeInBytes, i));
                else
                    Hirelings.Add(new MM3CharAndBytes(m_bytesRoster, iHeaderBytes + i * MM3Character.SizeInBytes, i));
            }
            FileName = strFileName;
        }

        protected override byte[] ReadAllBytes(string strFileName)
        {
            FileStream fs = File.Open(strFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, (int) fs.Length);
            fs.Close();
            return bytes;
        }

        public override bool ResetHirelings()
        {
            return true;
        }

        public override int SaveCharBytes(int iChar)
        {
            return SaveCharBytes(iChar, Chars[iChar].Town, Chars[iChar].Bytes);
        }

        public override int SaveCharBytes(int iChar, int town, byte[] bytes)
        {
            if (iChar > 19)
                return -1;

            try
            {
                FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Seek(iHeaderBytes + iChar * MM3Character.SizeInBytes, SeekOrigin.Begin);
                fs.Write(bytes, 0, Math.Min(MM3Character.SizeInBytes, bytes.Length));
                fs.Close();
                return iChar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return -1;
        }

        public override bool Save(ListView lvCharacters)
        {
            // Only save the first 20 characters; the other bytes are for hirelings, 
            // cartography data and whatnot

            if (m_bytesRoster == null)
                return false;

            byte[] bytesDeleted = Global.NullBytes(MM3Character.SizeInBytes);

            // If we rearrange the characters in the roster, we have to fix the current party bytes
            byte[] bytesCurrentParty = new byte[8];
            Buffer.BlockCopy(m_bytesRoster, iCurrentPartyBytes, bytesCurrentParty, 0, bytesCurrentParty.Length);

            MemoryStream stream = new MemoryStream(iHeaderBytes + 20 * MM3Character.SizeInBytes);

            Dictionary<int, int> dictPositionMap = new Dictionary<int, int>(20);

            int iCount = 0;
            foreach (ListViewItem lvi in lvCharacters.Items)
            {
                CharAndBytes cab = (CharAndBytes)lvi.Tag;
                stream.Write(cab.Bytes, 0, MM3Character.SizeInBytes);

                dictPositionMap.Add(cab.Position, iCount);
                iCount++;
            }

            for (int i = 0; i < bytesCurrentParty.Length; i++)
            {
                if (dictPositionMap.ContainsKey(bytesCurrentParty[i]))
                    bytesCurrentParty[i] = (byte)dictPositionMap[bytesCurrentParty[i]];
            }

            while (iCount < 20)
            {
                // Erase any other characters in the roster
                stream.Write(bytesDeleted, 0, bytesDeleted.Length);
                iCount++;
            }

            Buffer.BlockCopy(stream.ToArray(), 0, m_bytesRoster, iHeaderBytes + 0, (int)stream.Length);
            Buffer.BlockCopy(bytesCurrentParty, 0, m_bytesRoster, iCurrentPartyBytes, bytesCurrentParty.Length);

            // Also save the hirelings, since they can be reset
            stream = new MemoryStream(Hirelings.Count * MM3Character.SizeInBytes);
            foreach (CharAndBytes cab in Hirelings)
            {
                stream.Write(cab.Bytes, 0, MM3Character.SizeInBytes);
            }

            Buffer.BlockCopy(stream.ToArray(), 0, m_bytesRoster, iHeaderBytes + 20 * MM3Character.SizeInBytes, (int)stream.Length);

            try
            {
                File.WriteAllBytes(FileName, m_bytesRoster);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public override byte[] LoadCharBytes(int iChar)
        {
            if (iChar > 19)
                return null;

            byte[] bytes = new byte[MM3Character.SizeInBytes];
            try
            {
                FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Seek(iHeaderBytes + iChar * MM3Character.SizeInBytes, SeekOrigin.Begin);
                fs.Read(bytes, 0, MM3Character.SizeInBytes);
                fs.Close();
                return bytes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not read from file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }

    public class MM4RosterFile : RosterFile
    {
        public const int ExpectedLength = 283796;
        public static long[] AcceptableLengths = new long[] { ExpectedLength, 286045 };
        public static int GetExpectedSize() { return ExpectedLength; }
        public override int ExpectedSize { get { return GetExpectedSize(); } }
        public static bool AcceptableSize(long iSize) { return AcceptableLengths.Contains(iSize); }
        public List<CharAndBytes> Hirelings;
        public const int iHeaderBytes = MM4FileOffsets.Characters;
        public const int iCurrentPartyBytes = MM4FileOffsets.CurrentParty;

        public MM4RosterFile(string strFileName, bool bSilent)
        {
            Game = GameNames.MightAndMagic4;
            InitRosterFile(strFileName, bSilent);
            if (m_bytesRoster == null)
                return;

            Chars = new List<CharAndBytes>(20);
            Hirelings = new List<CharAndBytes>(10);
            for (int i = 0; i < 30; i++)
            {
                if (i < 20)
                    Chars.Add(new MM45CharAndBytes(m_bytesRoster, iHeaderBytes + i * MM45Character.SizeInBytes, i));
                else
                    Hirelings.Add(new MM45CharAndBytes(m_bytesRoster, iHeaderBytes + i * MM45Character.SizeInBytes, i));
            }
            FileName = strFileName;
        }

        public override bool IsAcceptableSize(long iSize) { return AcceptableSize(iSize); }

        protected override byte[] ReadAllBytes(string strFileName)
        {
            FileStream fs = File.Open(strFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);
            fs.Close();
            return bytes;
        }

        public override bool ResetHirelings()
        {
            return true;
        }

        public override int SaveCharBytes(int iChar)
        {
            return SaveCharBytes(iChar, Chars[iChar].Town, Chars[iChar].Bytes);
        }

        public override int SaveCharBytes(int iChar, int town, byte[] bytes)
        {
            if (iChar > 19)
                return -1;

            try
            {
                FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Seek(iHeaderBytes + iChar * MM45Character.SizeInBytes, SeekOrigin.Begin);
                fs.Write(bytes, 0, Math.Min(MM45Character.SizeInBytes, bytes.Length));
                fs.Close();
                return iChar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return -1;
        }

        public override bool Save(ListView lvCharacters)
        {
            // Only save the first 20 characters; the other bytes are for hirelings, 
            // cartography data and whatnot

            if (m_bytesRoster == null)
                return false;

            byte[] bytesDeleted = Global.NullBytes(MM45Character.SizeInBytes);

            // If we rearrange the characters in the roster, we have to fix the current party bytes
            byte[] bytesCurrentParty = new byte[8];
            Buffer.BlockCopy(m_bytesRoster, iCurrentPartyBytes, bytesCurrentParty, 0, bytesCurrentParty.Length);

            MemoryStream stream = new MemoryStream(iHeaderBytes + 20 * MM45Character.SizeInBytes);

            Dictionary<int, int> dictPositionMap = new Dictionary<int, int>(20);

            int iCount = 0;
            foreach (ListViewItem lvi in lvCharacters.Items)
            {
                CharAndBytes cab = (CharAndBytes)lvi.Tag;
                stream.Write(cab.Bytes, 0, MM45Character.SizeInBytes);

                dictPositionMap.Add(cab.Position, iCount);
                iCount++;
            }

            for (int i = 0; i < bytesCurrentParty.Length; i++)
            {
                if (dictPositionMap.ContainsKey(bytesCurrentParty[i]))
                    bytesCurrentParty[i] = (byte)dictPositionMap[bytesCurrentParty[i]];
            }

            while (iCount < 20)
            {
                // Erase any other characters in the roster
                stream.Write(bytesDeleted, 0, bytesDeleted.Length);
                iCount++;
            }

            Buffer.BlockCopy(stream.ToArray(), 0, m_bytesRoster, iHeaderBytes + 0, (int)stream.Length);
            Buffer.BlockCopy(bytesCurrentParty, 0, m_bytesRoster, iCurrentPartyBytes, bytesCurrentParty.Length);

            // Also save the hirelings, since they can be reset
            stream = new MemoryStream(Hirelings.Count * MM45Character.SizeInBytes);
            foreach (CharAndBytes cab in Hirelings)
            {
                stream.Write(cab.Bytes, 0, MM45Character.SizeInBytes);
            }

            Buffer.BlockCopy(stream.ToArray(), 0, m_bytesRoster, iHeaderBytes + 20 * MM45Character.SizeInBytes, (int)stream.Length);

            try
            {
                File.WriteAllBytes(FileName, m_bytesRoster);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public override byte[] LoadCharBytes(int iChar)
        {
            if (iChar > 19)
                return null;

            byte[] bytes = new byte[MM45Character.SizeInBytes];
            try
            {
                FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Seek(iHeaderBytes + iChar * MM45Character.SizeInBytes, SeekOrigin.Begin);
                fs.Read(bytes, 0, MM45Character.SizeInBytes);
                fs.Close();
                return bytes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not read from file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }

    public class MM5RosterFile : RosterFile
    {
        public const int ExpectedLength = 381671;
        public static long[] AcceptableLengths = new long[] { ExpectedLength, 386264 };
        public static int GetExpectedSize() { return ExpectedLength; }
        public override int ExpectedSize { get { return GetExpectedSize(); } }
        public static bool AcceptableSize(long iSize) { return AcceptableLengths.Contains(iSize); }
        public List<CharAndBytes> Hirelings;
        public const int iHeaderBytes = MM5FileOffsets.Characters;
        public const int iCurrentPartyBytes = MM5FileOffsets.CurrentParty;

        public MM5RosterFile(string strFileName, bool bSilent)
        {
            Game = GameNames.MightAndMagic5;
            InitRosterFile(strFileName, bSilent);
            if (m_bytesRoster == null)
                return;

            Chars = new List<CharAndBytes>(20);
            Hirelings = new List<CharAndBytes>(10);
            for (int i = 0; i < 30; i++)
            {
                if (i < 20)
                    Chars.Add(new MM45CharAndBytes(m_bytesRoster, iHeaderBytes + i * MM45Character.SizeInBytes, i));
                else
                    Hirelings.Add(new MM45CharAndBytes(m_bytesRoster, iHeaderBytes + i * MM45Character.SizeInBytes, i));
            }
            FileName = strFileName;
        }

        public override bool IsAcceptableSize(long iSize) { return AcceptableSize(iSize); }

        protected override byte[] ReadAllBytes(string strFileName)
        {
            FileStream fs = File.Open(strFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);
            fs.Close();
            return bytes;
        }

        public override bool ResetHirelings()
        {
            return true;
        }

        public override int SaveCharBytes(int iChar)
        {
            return SaveCharBytes(iChar, Chars[iChar].Town, Chars[iChar].Bytes);
        }

        public override int SaveCharBytes(int iChar, int town, byte[] bytes)
        {
            if (iChar > 19)
                return -1;

            try
            {
                FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Seek(iHeaderBytes + iChar * MM45Character.SizeInBytes, SeekOrigin.Begin);
                fs.Write(bytes, 0, Math.Min(MM45Character.SizeInBytes, bytes.Length));
                fs.Close();
                return iChar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return -1;
        }

        public override bool Save(ListView lvCharacters)
        {
            // Only save the first 20 characters; the other bytes are for hirelings, 
            // cartography data and whatnot

            if (m_bytesRoster == null)
                return false;

            byte[] bytesDeleted = Global.NullBytes(MM45Character.SizeInBytes);

            // If we rearrange the characters in the roster, we have to fix the current party bytes
            byte[] bytesCurrentParty = new byte[8];
            Buffer.BlockCopy(m_bytesRoster, iCurrentPartyBytes, bytesCurrentParty, 0, bytesCurrentParty.Length);

            MemoryStream stream = new MemoryStream(iHeaderBytes + 20 * MM45Character.SizeInBytes);

            Dictionary<int, int> dictPositionMap = new Dictionary<int, int>(20);

            int iCount = 0;
            foreach (ListViewItem lvi in lvCharacters.Items)
            {
                CharAndBytes cab = (CharAndBytes)lvi.Tag;
                stream.Write(cab.Bytes, 0, MM45Character.SizeInBytes);

                dictPositionMap.Add(cab.Position, iCount);
                iCount++;
            }

            for (int i = 0; i < bytesCurrentParty.Length; i++)
            {
                if (dictPositionMap.ContainsKey(bytesCurrentParty[i]))
                    bytesCurrentParty[i] = (byte)dictPositionMap[bytesCurrentParty[i]];
            }

            while (iCount < 20)
            {
                // Erase any other characters in the roster
                stream.Write(bytesDeleted, 0, bytesDeleted.Length);
                iCount++;
            }

            Buffer.BlockCopy(stream.ToArray(), 0, m_bytesRoster, iHeaderBytes + 0, (int)stream.Length);
            Buffer.BlockCopy(bytesCurrentParty, 0, m_bytesRoster, iCurrentPartyBytes, bytesCurrentParty.Length);

            // Also save the hirelings, since they can be reset
            stream = new MemoryStream(Hirelings.Count * MM45Character.SizeInBytes);
            foreach (CharAndBytes cab in Hirelings)
            {
                stream.Write(cab.Bytes, 0, MM45Character.SizeInBytes);
            }

            Buffer.BlockCopy(stream.ToArray(), 0, m_bytesRoster, iHeaderBytes + 20 * MM45Character.SizeInBytes, (int)stream.Length);

            try
            {
                File.WriteAllBytes(FileName, m_bytesRoster);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public override byte[] LoadCharBytes(int iChar)
        {
            if (iChar > 19)
                return null;

            byte[] bytes = new byte[MM45Character.SizeInBytes];
            try
            {
                FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Seek(iHeaderBytes + iChar * MM45Character.SizeInBytes, SeekOrigin.Begin);
                fs.Read(bytes, 0, MM45Character.SizeInBytes);
                fs.Close();
                return bytes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not read from file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }
    }

    public class Wiz1RosterFile : WizRosterFile
    {
        public Wiz1RosterFile()
        {
            Game = GameNames.Wizardry1;
        }

        public static WizRosterFile CreateWiz1(string strFileName, bool bSilent)
        {
            Wiz1RosterFile roster = new Wiz1RosterFile();
            roster.SetFromFile(strFileName, bSilent);
            return roster;
        }
    }

    public class Wiz2RosterFile : WizRosterFile
    {
        public Wiz2RosterFile()
        {
            Game = GameNames.Wizardry2;
        }

        public static WizRosterFile CreateWiz2(string strFileName, bool bSilent)
        {
            Wiz2RosterFile roster = new Wiz2RosterFile();
            roster.SetFromFile(strFileName, bSilent);
            return roster;
        }

        public override int OffsetForChar(int iChar)
        {
            return 0x1D200 + ((iChar / 4) * 1024) + ((iChar % 4) * WizCharacter.SizeInBytes);
        }
    }

    public class Wiz3RosterFile : WizRosterFile
    {
        public Wiz3RosterFile()
        {
            Game = GameNames.Wizardry3;
        }

        public static WizRosterFile CreateWiz3(string strFileName, bool bSilent)
        {
            Wiz3RosterFile roster = new Wiz3RosterFile();
            roster.SetFromFile(strFileName, bSilent);
            return roster;
        }

        public override int OffsetForChar(int iChar)
        {
            return 0x1DE00 + ((iChar / 4) * 1024) + ((iChar % 4) * WizCharacter.SizeInBytes);
        }
    }

    public class Wiz4RosterFile : WizRosterFile
    {
        public Wiz4RosterFile()
        {
            Game = GameNames.Wizardry4;
        }

        public static WizRosterFile CreateWiz4(string strFileName, bool bSilent)
        {
            Wiz4RosterFile roster = new Wiz4RosterFile();
            roster.SetFromFile(strFileName, bSilent);
            return roster;
        }

        public override int OffsetForChar(int iChar)
        {
            return 0x4AC00 + (iChar * 1024) + WizCharacter.SizeInBytes;
        }
    }

    public class Wiz5RosterFile : WizRosterFile
    {
        public Wiz5RosterFile()
        {
            Game = GameNames.Wizardry5;
        }

        public static WizRosterFile CreateWiz5(string strFileName, bool bSilent)
        {
            Wiz5RosterFile roster = new Wiz5RosterFile();
            roster.SetFromFile(strFileName, bSilent);
            return roster;
        }

        public override void SetFromFile(string strFileName, bool bSilent)
        {
            InitRosterFile(strFileName, bSilent);
            if (m_bytesRoster == null)
                return;

            Chars = new List<CharAndBytes>(20);
            for (int i = 0; i < 20; i++)
                Chars.Add(new Wiz5CharAndBytes(m_bytesRoster, OffsetForChar(i), i));

            FileName = strFileName;
        }

        public override int ConditionOffset { get { return Wiz5.Offsets.Condition; } }

        public override int OffsetForChar(int iChar)
        {
            return 0x4C00 + (iChar * Wiz5Character.SizeInBytes);
        }
    }

    public class WizRosterFile : RosterFile
    {
        public static int GetExpectedSize() { return 655360; }
        
        public static bool AcceptableSize(long iSize) { return iSize == GetExpectedSize() || iSize == 327680; }
        public override int ExpectedSize { get { return GetExpectedSize(); } }

        public virtual int OffsetForChar(int iChar)
        {
            return 0x1D800 + ((iChar / 4) * 1024) + ((iChar % 4) * WizCharacter.SizeInBytes);
        }

        public virtual void SetFromFile(string strFileName, bool bSilent)
        {
            InitRosterFile(strFileName, bSilent);
            if (m_bytesRoster == null)
                return;

            Chars = new List<CharAndBytes>(20);
            for (int i = 0; i < 20; i++)
                Chars.Add(new Wiz1234CharAndBytes(m_bytesRoster, OffsetForChar(i), i, Game));

            if (Chars.Count > 0)
                Game = Chars[0].Char.Game;
            FileName = strFileName;
        }

        protected override byte[] ReadAllBytes(string strFileName)
        {
            FileStream fs = File.Open(strFileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            byte[] bytes = new byte[fs.Length];
            fs.Read(bytes, 0, (int)fs.Length);
            fs.Close();
            return bytes;
        }

        public override byte[] LoadCharBytes(int iChar)
        {
            if (iChar < 0 || iChar > 19 || FileName == null || !File.Exists(FileName))
                return null;

            byte[] bytes = new byte[WizCharacter.SizeInBytes];
            try
            {
                FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Seek(OffsetForChar(iChar), SeekOrigin.Begin);
                fs.Read(bytes, 0, bytes.Length);
                fs.Close();
                return bytes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not read from file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public override int SaveCharBytes(int iChar)
        {
            return SaveCharBytes(iChar, 0, Chars[iChar].Bytes);
        }

        public virtual int ConditionOffset { get { return Wiz123.Offsets.Condition; } }

        public bool SetGoodCondition(int iChar)
        {
            if (iChar < 0 || iChar > 19)
                return false;

            try
            {
                FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Seek(OffsetForChar(iChar) + ConditionOffset, SeekOrigin.Begin);
                fs.WriteByte((byte) WizCondition.Good);
                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not write to file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        public override int SaveCharBytes(int iChar, int iTown, byte[] bytes)
        {
            if (iChar < 0 || iChar > 19)
                return -1;

            try
            {
                FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Seek(OffsetForChar(iChar), SeekOrigin.Begin);
                fs.Write(bytes, 0, bytes.Length);
                fs.Close();
                return iChar;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return -1;
        }

        public override bool Save(ListView lvCharacters)
        {
            byte iPosition = 0;
            byte[] zeros = Global.NullBytes(WizCharacter.SizeInBytes);

            List<int> invChars = new List<int>();

            try
            {
                FileStream fs = File.Open(FileName, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

                foreach (ListViewItem lvi in lvCharacters.Items)
                {
                    CharAndBytes cab = (CharAndBytes)lvi.Tag;
                    if (cab.IsInventoryChar)
                    {
                        invChars.Add(lvi.Index);
                        continue;       // Leave these at the end
                    }

                    cab.Position = iPosition;
                    fs.Seek(OffsetForChar(iPosition), SeekOrigin.Begin);
                    fs.Write(cab.Bytes, 0, cab.Bytes.Length);
                    iPosition++;
                }
                while (iPosition < (20 - invChars.Count))
                {
                    fs.Seek(OffsetForChar(iPosition), SeekOrigin.Begin);
                    fs.Write(zeros, 0, zeros.Length);
                    iPosition++;
                }
                foreach (int iInvIndex in invChars)
                {
                    CharAndBytes cab = (CharAndBytes)lvCharacters.Items[iInvIndex].Tag;
                    cab.Position = iPosition;
                    fs.Seek(OffsetForChar(iPosition), SeekOrigin.Begin);
                    fs.Write(cab.Bytes, 0, cab.Bytes.Length);
                    iPosition++;
                }

                fs.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }
    }

    public class BTRosterFile : RosterFile
    {
        public Dictionary<int, string> FileNames;
        public virtual string CharFileWildcard { get { return "*.TPW"; } }
        protected virtual CharAndBytes CreateChar(byte[] bytes, int iIndex) { return new BT1CharAndBytes(bytes, iIndex); }

        public void InitFromExe(string strFileName, bool bSilent)
        {
            Valid = false;
            if (!File.Exists(strFileName))
                return;

            // Bard's Tale rosters aren't a single file; they are multiple files stored in the same directory
            // as the bard.exe file.
            string[] files = Global.GetFilesNumeric(Path.GetDirectoryName(strFileName), CharFileWildcard);
            if (files == null)
                return;

            FileNames = new Dictionary<int, string>(files.Length);
            foreach (string file in files)
            {
                string strNum = Path.GetFileNameWithoutExtension(file);
                int iNum = 0;
                if (!Int32.TryParse(strNum, out iNum))
                    continue;   // Character files must begin with a number (e.g. "12.TPW")
                FileNames.Add(iNum, file);
            }

            Chars = new List<CharAndBytes>(files.Length);
            foreach(int i in FileNames.Keys)
                Chars.Add(CreateChar(File.ReadAllBytes(FileNames[i]), i));

            FileName = strFileName;
            Valid = true;
        }

        public void DeleteInventoryChars()
        {
            ASCIIEncoding ascii = new ASCIIEncoding();
            for (int i = 1; i < 99; i++)
            {
                string strFile = CharacterFilePath(i);
                if (!File.Exists(strFile))
                    continue;

                byte[] bytes = File.ReadAllBytes(strFile);
                if (ascii.GetString(bytes, 0, 15) != "INVENTORY\0\0\0\0\0\0")
                    continue;

                File.Delete(strFile);
            }
        }

        protected virtual string CharacterFileName(int iIndex) { return String.Format("{0}.TPW", iIndex); }
        private string CharacterFilePath(int iIndex) { return String.Format("{0}\\{1}", Path.GetDirectoryName(FileName), CharacterFileName(iIndex)); }

        private int TPWIndexForChar(int iCharIndex)
        {
            int iFileIndex = 0;
            int iResult = -1;
            int iLastEmptyIndex = -1;
            for (int iFile = 1; iFile < 100; iFile++)
            {
                if (File.Exists(CharacterFilePath(iFile)))
                {
                    if (iFileIndex == iCharIndex)
                        iResult = iFile;
                    iFileIndex++;
                }
                else
                    iLastEmptyIndex = iFile;
            }
            return iResult == -1 ? iLastEmptyIndex : iResult;
        }

        private int CharIndexForTPW(int iTPW)
        {
            string strTPW = CharacterFileName(iTPW);
            string[] files = Global.GetFilesNumeric(Path.GetDirectoryName(FileName), CharFileWildcard);
            for (int i = 0; i < files.Length; i++)
            {
                if (String.Compare(Path.GetFileName(files[i]), strTPW, StringComparison.InvariantCultureIgnoreCase) == 0)
                    return i;
            }
            return -1;
        }

        private string TPWFileForChar(int iCharIndex) { return CharacterFilePath(TPWIndexForChar(iCharIndex)); }

        public override byte[] LoadCharBytes(int iChar)
        {
            string strFile = TPWFileForChar(iChar);
            if (strFile == null || !File.Exists(strFile))
                return null;
            try
            {
                return File.ReadAllBytes(strFile);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not read from file: " + strFile + "\r\n\r\nException: " + ex.Message,
                    "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return null;
        }

        public override int SaveCharBytes(int iChar)
        {
            return SaveCharBytes(iChar, 0, Chars[iChar].Bytes);
        }

        public override int SaveCharBytes(int iChar, int town, byte[] bytes)
        {
            if (iChar == -1)
                return -1;

            int iTPWIndex = TPWIndexForChar(iChar);
            string strFile = CharacterFilePath(iTPWIndex);
            try
            {
                File.WriteAllBytes(strFile, bytes);
                return CharIndexForTPW(iTPWIndex);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + strFile + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return -1;
        }

        public override bool Save(ListView lvCharacters)
        {
            string strPath = Path.GetDirectoryName(FileName);
            string strTempPath = Path.Combine(strPath, "_roster");

            if (!Directory.Exists(strTempPath))
                Directory.CreateDirectory(strTempPath);

            string strFileCurrent = String.Empty;
            try
            {
                int iIndex = 1;
                foreach (ListViewItem lvi in lvCharacters.Items)
                {
                    CharAndBytes cab = (CharAndBytes)lvi.Tag;
                    cab.Position = iIndex;
                    strFileCurrent = CharacterFileName(iIndex);
                    File.WriteAllBytes(Path.Combine(strTempPath, strFileCurrent), cab.Bytes);
                    iIndex++;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error write in file: " + strFileCurrent + "\r\nChanges to the roster have been aborted.\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            foreach (string strOldFile in Directory.GetFiles(strPath, CharFileWildcard))
            {
                try
                {
                    File.Delete(strOldFile);
                }
                catch (Exception)
                {
                    // Still need to move the new files even if deleting a file causes an error
                }
            }

            foreach (string strNewFile in Directory.GetFiles(strTempPath, CharFileWildcard))
                File.Move(strNewFile, Path.Combine(strPath, Path.GetFileName(strNewFile)));

            Directory.Delete(strTempPath);

            return true;
        }
    }

    public class BT1RosterFile : BTRosterFile
    {
        public static int GetExpectedSize() { return 114528; }
        public static bool AcceptableSize(long iSize) { return iSize == GetExpectedSize(); }
        public override int ExpectedSize { get { return GetExpectedSize(); } }

        public BT1RosterFile(string strFileName, bool bSilent)
        {
            Game = GameNames.BardsTale1;
            InitFromExe(strFileName, bSilent);
        }
    }

    public class BT2RosterFile : BTRosterFile
    {
        public static int GetExpectedSize() { return 147485; }
        public static bool AcceptableSize(long iSize) { return iSize == GetExpectedSize(); }
        public override int ExpectedSize { get { return GetExpectedSize(); } }
        public override string CharFileWildcard { get { return "*.TW"; } }
        protected override string CharacterFileName(int iIndex) { return String.Format("{0}.TW", iIndex); }
        protected override CharAndBytes CreateChar(byte[] bytes, int iIndex) { return new BT2CharAndBytes(bytes, iIndex); }

        public BT2RosterFile(string strFileName, bool bSilent)
        {
            Game = GameNames.BardsTale2;
            InitFromExe(strFileName, bSilent);
        }
    }

    public class BT3RosterFile : RosterFile // BT3 uses a roster file, unlike BT1/2
    {
        public static bool AcceptableFile(string strName) { return String.Compare(Path.GetFileName(strName), "THIEVES.INF", true) == 0; }
        public override bool IsAcceptableSize(long iSize) { return iSize > 0; }
        public override int ExpectedSize { get { return -1; } }

        public BT3RosterFile(string strFileName, bool bSilent)
        {
            Game = GameNames.BardsTale3;
            LoadRosterFromFile(strFileName, bSilent);
        }

        private bool LoadRosterFromFile(string strFileName, bool bSilent)
        {
            Valid = false;
            try
            {
                m_bytesRoster = File.ReadAllBytes(strFileName);
            }
            catch (Exception ex)
            {
                if (!bSilent)
                    MessageBox.Show(String.Format("Could not read from roster file \"{0}\": {1}", strFileName, ex.Message),
                        "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return Valid;
            }
            FileName = strFileName;
            int iIndex = 0;
            Chars = new List<CharAndBytes>(m_bytesRoster.Length / BT3Character.SizeInBytes);
            while (iIndex <= m_bytesRoster.Length - BT3Character.SizeInBytes)
            {
                try
                {
                    if (m_bytesRoster[iIndex] == 0)
                        break;  // There may be more character data after this point but not as far as the game is concerned

                    Chars.Add(new BT3CharAndBytes(m_bytesRoster, iIndex));
                }
                catch (Exception ex)
                {
                    Global.LogError(String.Format("Could not load BT3 character from position {0} in file {1}: {2}", iIndex, strFileName, ex.Message));
                }
                iIndex += BT3Character.SizeInBytes;
            }
            Valid = true;
            return Valid;
        }

        public void DeleteInventoryChars()
        {
            if (Chars == null)
                return;

            int i = Chars.Count - 1;
            while (i > 0)
            {
                if (Chars[i].Char.Name.StartsWith("INVENTORY"))
                {
                    Chars.RemoveAt(i);
                }

                i--;
            }
        }

        public void CreateInventoryChars(int iNumChars)
        {
            if (Chars == null)
                Chars = new List<CharAndBytes>(iNumChars);

            while (iNumChars-- > 0)
            {
                BT3CharAndBytes cab = new BT3CharAndBytes(Properties.Resources.BT3InventoryChar);
                cab.Position = Chars.Count;
                Chars.Add(cab);
            }
        }

        public override bool Save(ListView lvCharacters)
        {
            using (MemoryStream ms = new MemoryStream(lvCharacters.Items.Count * BT3Character.SizeInBytes))
            {
                foreach (ListViewItem lvi in lvCharacters.Items)
                {
                    BT3CharAndBytes cab = lvi.Tag as BT3CharAndBytes;
                    if (cab == null)
                        continue;

                    ms.Write(cab.Bytes, 0, cab.Bytes.Length);
                }
                using (FileStream fsOut = new FileStream(FileName, FileMode.Create))
                {
                    ms.WriteTo(fsOut);
                }
            }

            return true;
        }

        public override int SaveCharBytes(int iChar) { return SaveRoster() ? 0 : -1; }

        public override int SaveCharBytes(int iChar, int town, byte[] bytes)
        {
            if (iChar < 0 || iChar >= Chars.Count)
                return -1;

            Chars[iChar] = new BT3CharAndBytes(bytes);
            Chars[iChar].Position = iChar;
            return SaveRoster() ? 0 : -1;
        }

        public bool SaveRoster()
        {
            try
            {
                using (MemoryStream ms = new MemoryStream(Chars.Count * BT3Character.SizeInBytes))
                {
                    foreach (BT3CharAndBytes cab in Chars)
                    {
                        if (cab == null)
                            continue;

                        ms.Write(cab.Bytes, 0, cab.Bytes.Length);
                    }
                    using (FileStream fsOut = new FileStream(FileName, FileMode.Create))
                    {
                        ms.WriteTo(fsOut);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not save file: " + FileName + "\r\n\r\nException: " + ex.Message,
                    "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        public override byte[] LoadCharBytes(int iChar)
        {
            if (!Valid)
                LoadRosterFromFile(FileName, true);

            if (!Valid || iChar < 0 || iChar >= Chars.Count)
                return null;

            return Chars[iChar].Bytes;
        }
    }
}
