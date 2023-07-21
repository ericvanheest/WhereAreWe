using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class EOB3AttackControl : UserControl
    {
        public EOB3AttackControl()
        {
            InitializeComponent();
            if (comboAttack.Items.Count == 0)
            {
                comboAttack.Items.Add("Melee Attack");
                foreach (EOB1Spell spell in EOB1.Spells)
                    comboAttack.Items.Add(String.Format("{0}", spell.Name));
                comboAttack.SelectedIndex = 0;
            }

            InitFaces(comboFaces);
        }

        private byte[] m_bytesOriginal;
        public event VoidHandler ValueChanged;

        public static void InitFaces(ComboBox combo)
        {
            if (combo.Items.Count > 0)
                combo.Items.Clear();
            for (int i = 0; i < 8; i++)
                combo.Items.Add(String.Format("d{0}", FacesFromIndex(i)));
            combo.SelectedIndex = 0;
        }

        private void comboAttack_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboAttack.SelectedIndex == 0)
            {
                labelAttack.Text = "for";
                if (nudLevel.Value < 1)
                    nudLevel.Value = 1;
                nudLevel.Minimum = 1;
                if (nudLevel.Value > 31)
                    nudLevel.Value = 31;
                nudLevel.Maximum = 31;
                comboFaces.Visible = true;
            }
            else
            {
                labelAttack.Text = "at level";
                nudLevel.Minimum = 0;
                nudLevel.Maximum = 255;
                comboFaces.Visible = false;
            }
            ValueChanged?.Invoke(this, new EventArgs());
        }

        public static int IndexFromFaces(int iFaces)
        {
            for (int i = 0; i < 8; i++)
            {
                if (iFaces <= (2 << i))
                    return i;
            }
            return 0;
        }

        public static int FacesFromIndex(int iIndex) { return 2 << iIndex; }

        public byte DamageByte { get { return (byte)((comboFaces.SelectedIndex << 5) | (int) (nudLevel.Value - 1)); } }

        public EOB1MonsterAttack Attack
        {
            get
            {
                if (m_bytesOriginal == null)
                    return new EOB1MonsterAttack(new byte[] { 0, 0 });
                if (comboAttack.SelectedIndex == 0)
                    return new EOB1MonsterAttack(new byte[] { m_bytesOriginal[0] < 0x80 ? (byte) 0x80 : m_bytesOriginal[0], DamageByte });
                return new EOB1MonsterAttack(new byte[] { (byte)(comboAttack.SelectedIndex - 1), (byte)nudLevel.Value });
            }

            set
            {
                m_bytesOriginal = value.RawBytes;
                if (value.Melee)
                {
                    comboAttack.SelectedIndex = 0;
                    nudLevel.Value = Math.Min(31, value.Damage.Quantity);
                    comboFaces.SelectedIndex = IndexFromFaces(value.Damage.Faces);
                }
                else
                {
                    comboAttack.SelectedIndex = (int)value.SpellIndex;
                    nudLevel.Value = value.Level;
                }
            }
        }

        private void nudLevel_ValueChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, new EventArgs());
        }

        private void comboFaces_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
