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
    public partial class NotificationForm : Form
    {
        private const double StartingOpacity = 1.8;
        private double m_opacity = StartingOpacity;

        protected override bool ShowWithoutActivation { get { return true; } }

        private const int WS_EX_TOPMOST = 0x00000008;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= WS_EX_TOPMOST;
                return createParams;
            }
        }

        public NotificationForm()
        {
            InitializeComponent();
        }

        public NotificationForm(string strNote)
        {
            InitializeComponent();
            labelText.Text = strNote;
        }

        public void Restart(string strNote = null)
        {
            timerOpacity.Stop();
            m_opacity = StartingOpacity;
            if (strNote != null)
                labelText.Text = strNote;
            timerOpacity.Start();
        }

        private void timerOpacity_Tick(object sender, EventArgs e)
        {
            m_opacity -= StartingOpacity / (Properties.Settings.Default.NotificationDelay / (double) timerOpacity.Interval);
            if (m_opacity <= 0.2)
                Close();

            Opacity = Math.Min(m_opacity, 1.0);
        }

        private void NotificationForm_Load(object sender, EventArgs e)
        {
            Restart();
        }
    }
}
