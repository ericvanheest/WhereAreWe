using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class MMItemLabel : UserControl
    {
        private Item m_item = null;
        private BaseCharacter m_character = null;
        private ToolTip m_tipItem = null;

        public MMItemLabel()
        {
            InitializeComponent();
        }

        private void UpdateUI()
        {
            labelItem.AutoSize = true;
            if (m_item == null || m_item.Index == 0)
            {
                ClearLabel();
                return;
            }
            else if (m_character != null && m_item != null)
                labelItem.Text = m_item.GetLongDescription(m_character.BasicAlignment.Temporary, m_character.BasicClass, String.Empty);
            else if (m_item != null)
                labelItem.Text = m_item.DescriptionString;
            else
            {
                ClearLabel();
                return;
            }

            m_tipItem = new ToolTip();
            m_tipItem.SetToolTip(labelItem, m_item.MultiLineDescription);
            m_tipItem.ShowAlways = true;
            m_tipItem.AutoPopDelay = 32000;
        }

        public void AddMouseUpEvent(MouseEventHandler handler)
        {
            labelItem.MouseUp += handler;
        }

        private void ClearLabel()
        {
            labelItem.Text = "";
            labelItem.AutoSize = false;
            labelItem.Width = 100;
        }

        public Item Item
        {
            set
            {
                m_item = value;
                UpdateUI();
            }

            get
            {
                return m_item;
            }
        }

        public BaseCharacter Character
        {
            set
            {
                m_character = value;
                UpdateUI();
            }
            get { return m_character; }
        }

        public void SetItem(Item item, BaseCharacter character)
        {
            m_item = item;
            m_character = character;
            if (m_tipItem != null)
                m_tipItem.RemoveAll();
            m_tipItem = null;
            UpdateUI();
        }

        public void Clear()
        {
            labelItem.Text = "";
            m_item = null;
            m_character = null;
            if (m_tipItem != null)
                m_tipItem.RemoveAll();
            m_tipItem = null;
            UpdateUI();
        }
    }
}
