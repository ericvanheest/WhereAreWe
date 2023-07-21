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
    public partial class ItemSelectionForm : Form
    {
        public ItemSelectionForm()
        {
            InitializeComponent();
        }

        public List<SelectionItem> Items { get; set; }

        public SelectionItem SelectedItem { get; set; }
        public String[] ColumnText { get; set; }

        private void ItemSelectionForm_Load(object sender, EventArgs e)
        {
            InitItemList();
        }

        private void InitItemList()
        {
            lvItems.BeginUpdate();
            lvItems.Clear();
            foreach (SelectionItem item in Items)
            {
                ListViewItem lvi = new ListViewItem(item.ItemText);
                if (lvItems.Columns.Count < 1 && ColumnText != null && ColumnText.Length > 0)
                    lvItems.Columns.Add(ColumnText[0]);
                for (int i = 0; i < item.Extra.Length; i++)
                {
                    if (lvItems.Columns.Count - 1 <= i && ColumnText != null && ColumnText.Length - 1 > i)
                        lvItems.Columns.Add(ColumnText[i + 1]);
                    lvi.SubItems.Add(item.Extra[i]);
                }
                lvi.Tag = item;
                lvItems.Items.Add(lvi);
            }
            Global.SizeHeadersAndContent(lvItems);
            lvItems.EndUpdate();
        }

        private void ItemSelectionForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.Escape:
                    DialogResult = DialogResult.Cancel;
                    Close();
                    break;
                case Keys.Enter:
                    UseSelectedItem();
                    break;
                default:
                    break;
            }
        }

        private void lvItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            UseSelectedItem();
        }

        private void UseSelectedItem()
        {
            if (lvItems.SelectedItems.Count < 1)
                SelectedItem = null;
            else
                SelectedItem = lvItems.SelectedItems[0].Tag as SelectionItem;
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public class SelectionItem
    {
        public object Item { get; set; }
        public string Text { get; set; }
        public string[] Extra { get; set; }
        public object Tag { get; set; }

        public SelectionItem(object o, params string[] extra)
        {
            Item = o;
            Extra = extra;
        }

        public string ItemText
        {
            get
            {
                if (String.IsNullOrEmpty(Text))
                    return Item.ToString();
                return Text;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(Item.ToString());
            foreach (string str in Extra)
                sb.AppendFormat("\t{0}", str);
            return sb.ToString();
        }
    }
}
