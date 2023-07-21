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
    public partial class AskValueForm : Form
    {
        public AskValueForm()
        {
            InitializeComponent();
            Minimum = 0;
            Maximum = Int16.MaxValue;
            Value = 0;
            ValueText = "Value";
        }

        public AskValueForm(string strValue, int iValue, int iMin, int iMax)
        {
            InitializeComponent();
            Minimum = iMin;
            Maximum = iMax;
            Value = iValue;
            ValueText = strValue;
        }

        public string ValueText { get; set; }
        public int Value { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        private void AskValueForm_Load(object sender, EventArgs e)
        {
            labelValue.Text = ValueText;
            Global.SetNud(nudValue, Value);
            nudValue.Minimum = Minimum;
            nudValue.Maximum = Maximum;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Value = (int) nudValue.Value;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
