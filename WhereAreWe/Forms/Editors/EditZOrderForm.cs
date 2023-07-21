using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class EditZOrderForm : HackerBasedForm
    {
        private bool m_bAscendingSort = true;
        private int m_iLastSortColumn = -1;
        private WindowType[] m_finalOrder = null;

        public static WindowType[] DefaultOrder = new WindowType[] {
            WindowType.SpellReference,
            WindowType.QuickRef,
            WindowType.ShopInventory,
            WindowType.GameInfo,
            WindowType.Encounter,
            WindowType.Quests,
            WindowType.Party,
            WindowType.Main
            };

        public EditZOrderForm()
        {
            InitializeComponent();
        }

        public WindowType[] WindowOrder
        {
            get
            {
                if (m_finalOrder != null)
                    return m_finalOrder;
                WindowType[] types = new WindowType[lvZOrder.Items.Count];
                for (int i = 0; i < lvZOrder.Items.Count; i++)
                    types[i] = ((ZOrderInfo)lvZOrder.Items[i].Tag).Window;
                return types;
            }

            set
            {
                WindowType[] types = value;
                if (types.Length < 2)
                    types = DefaultOrder;
                lvZOrder.BeginUpdate();
                lvZOrder.Items.Clear();
                foreach (WindowType type in types)
                    lvZOrder.Items.Add(CreateLVI(type));
                lvZOrder.EndUpdate();
            }
        }

        public bool IgnoreList
        {
            get { return cbIgnoreZOrderList.Checked; }
            set { cbIgnoreZOrderList.Checked = value; }
        }

        private ListViewItem CreateLVI(WindowType type)
        {
            ZOrderInfo info = new ZOrderInfo(type);
            ListViewItem lvi = new ListViewItem(info.Name);
            lvi.Tag = info;
            return lvi;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            m_finalOrder = WindowOrder;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lvZOrder_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            lvZOrder.ListViewItemSorter = new ZOrderComparer(lvZOrder, e.Column, m_bAscendingSort);
            lvZOrder.Sort();
        }

        private void lvZOrder_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.A:
                    if (e.Control)
                    {
                        lvZOrder.BeginUpdate();
                        foreach (ListViewItem lvi in lvZOrder.Items)
                            lvi.Selected = true;
                        lvZOrder.EndUpdate();
                    }
                    break;
                default:
                    break;
            }
        }

        private void llResetToDefault_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MessageBox.Show("Reset the Z-Order list to its default?", "Reset to Default?", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                return;

            SetDefaultOrder();
        }

        private void SetDefaultOrder()
        {
            lvZOrder.ListViewItemSorter = null;
            WindowOrder = DefaultOrder;
        }

        private void cbIgnoreZOrderList_CheckedChanged(object sender, EventArgs e)
        {
            lvZOrder.Enabled = !cbIgnoreZOrderList.Checked;
        }
    }

    class ZOrderInfo
    {
        public WindowType Window;

        public ZOrderInfo(WindowType type)
        {
            Window = type;
        }

        public static string GetWindowName(WindowType type)
        {
            switch (type)
            {
                case WindowType.None: return "None";
                case WindowType.Main: return "Main (Map)";
                case WindowType.Party: return "Party Information";
                case WindowType.GameInfo: return "Game Information";
                case WindowType.Encounter: return "Encounter Information";
                case WindowType.CreationAssistant: return "Creation Assistant";
                case WindowType.TrainingAssistant: return "Training Assistant";
                case WindowType.About: return "About";
                case WindowType.Colors: return "Colors";
                case WindowType.DoNotShowAgain: return "Do Not Show Again";
                case WindowType.DropTrash: return "Drop Trash";
                case WindowType.ColorPicker: return "Color Picker";
                case WindowType.Icons: return "Icons";
                case WindowType.MapInfo: return "Map Info";
                case WindowType.Items: return "Complete Item List";
                case WindowType.MapSelection: return "Map Selection";
                case WindowType.SpellSelection: return "Spell Selection";
                case WindowType.Monsters: return "Complete Monster List";
                case WindowType.Options: return "Options";
                case WindowType.Quests: return "Quests";
                case WindowType.QuickRef: return "Character Quick Reference";
                case WindowType.Scripts: return "Scripts";
                case WindowType.Search: return "Search for Text";
                case WindowType.SelectGameFiles: return "Select Game Files";
                case WindowType.SheetExpand: return "Sheet Expand";
                case WindowType.ShopInventory: return "Shop Inventory";
                case WindowType.SpellReference: return "Spell Reference";
                case WindowType.StringsView: return "Strings View";
                case WindowType.Unicode: return "Unicode Character Selection";
                case WindowType.Wait: return "Please Wait";
                case WindowType.Wizard: return "Initial Wizard";
                case WindowType.AskValue: return "Ask for Value";
                case WindowType.AttributeEdit: return "Attribute Edit";
                case WindowType.BasicCharInfo: return "Character Info Edit";
                case WindowType.ColorPattern: return "Edit Color/Pattern";
                case WindowType.ConditionEdit: return "Edit Condition";
                case WindowType.EditBits: return "Edit Bits";
                case WindowType.EditBytes: return "Edit Bytes";
                case WindowType.EditBytesSmall: return "Edit Bytes (Small)";
                case WindowType.EditCartography: return "Edit Cartography";
                case WindowType.EditLabels: return "Edit Labels";
                case WindowType.EditRoster: return "Edit Roster";
                case WindowType.EraEdit: return "Edit Era";
                case WindowType.ForbiddenSpellsEdit: return "Forbidden Spells";
                case WindowType.GameShortcutsEditor: return "Game Shortcuts";
                case WindowType.InventoryManipulator: return "Bag of Holding";
                case WindowType.ItemEditor: return "Edit Item";
                case WindowType.LineStyle: return "Line Style";
                case WindowType.Effects: return "Effects";
                case WindowType.Skills: return "Skills";
                case WindowType.NoteTemplates: return "Note Templates";
                case WindowType.SelectImageRect: return "Select Image Rectangle";
                case WindowType.SheetOrganizer: return "Sheet Organizer";
                case WindowType.SheetPathEdit: return "Edit Sheet Path";
                case WindowType.SheetSelector: return "Sheet Selector";
                case WindowType.TimeEdit: return "Edit Game Time";
                case WindowType.MM2KnownSPells: return "Known Spells (MM2)";
                case WindowType.MM2MonsterEdit: return "Edit Monster (MM2)";
                case WindowType.MM2SecondarySkills: return "Secondary Skills (MM2)";
                case WindowType.MM345KnownSPells: return "Known Spells (MM3/4/5)";
                case WindowType.MM345MonsterEdit: return "Edit Monster (MM3/4/5)";
                case WindowType.MM3ConditionEdit: return "Edit Condition (MM3)";
                case WindowType.MM3ItemEdit: return "Edit Item (MM3)";
                case WindowType.MM45ItemEdit: return "Edit Item (MM4/5)";
                case WindowType.DebugConsole: return "Debug Console";
                case WindowType.EditZOrder: return "Edit Z-Order";
                default: return String.Format("<Unknown:{0}>", (int)type);
            }
        }

        public string Name { get { return GetWindowName(Window); } }
    }

    class ZOrderComparer : BasicListViewComparer
    {
        public ZOrderComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

        public override int Compare(object x, object y)
        {
            try
            {
                ZOrderInfo info1 = ((ListViewItem) x).Tag as ZOrderInfo;
                ZOrderInfo info2 = ((ListViewItem) y).Tag as ZOrderInfo;

                switch (m_column)
                {
                    case 0: return Order(String.Compare(info1.Name, info2.Name));
                    default: return base.Compare(x, y);
                }
            }
            catch(Exception)
            {
                return 0;
            }
        }
    }
}
