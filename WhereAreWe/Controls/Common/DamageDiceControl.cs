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
    public partial class DamageDiceControl : UserControl
    {
        public DamageDiceControl()
        {
            InitializeComponent();
        }

        public DamageDiceControl(DamageDice dd)
        {
            InitializeComponent();
            Dice = dd;
        }

        public DamageDiceControl(DamageDice dd, int maxQuantity, int maxFaces, int maxBonus)
        {
            InitializeComponent();
            SetDice(dd, maxQuantity, maxFaces, maxBonus);
        }

        public void SetDice(DamageDice dd, int maxQuantity, int maxFaces, int maxBonus)
        {
            FacesMax = maxFaces;
            QuantityMax = maxQuantity;
            BonusMax = maxBonus;
            Dice = dd;
        }

        private bool m_bUpdating = false;

        public int Faces
        {
            get => (int)nudFaces.Value;
            set => nudFaces.Value = value;
        }

        public int FacesMax
        {
            get => (int) nudFaces.Maximum;
            set => nudFaces.Maximum = value;
        }

        public int Quantity
        {
            get => (int)nudQuantity.Value;
            set => nudQuantity.Value = value;
        }

        public int QuantityMax
        {
            get => (int)nudQuantity.Maximum;
            set => nudQuantity.Maximum = value;
        }

        public int Bonus
        {
            get => (int)nudBonus.Value;
            set => nudBonus.Value = value;
        }

        public int BonusMax
        {
            get => (int)nudBonus.Maximum;
            set => nudBonus.Maximum = value;
        }

        public DamageDice Dice
        {
            get => new DamageDice(Faces, Quantity, Bonus);
            set
            {
                m_bUpdating = true;
                Faces = value.Faces;
                Quantity = value.Quantity;
                Bonus = value.Bonus;
                m_bUpdating = false;
            }
        }

        public event VoidHandler ValueChanged;

        private void uiItem_ValueChanged(object sender, EventArgs e)
        {
            if (m_bUpdating)
                return;
            ValueChanged?.Invoke(this, new EventArgs());
        }
    }
}
