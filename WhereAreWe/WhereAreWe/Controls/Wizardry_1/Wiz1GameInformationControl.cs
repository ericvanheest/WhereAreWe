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
    public partial class Wiz1GameInformationControl
        : GameInformationControl
    {
        private IMain m_main = null;
        private Wiz1GameInfo m_lastInfo = null;

        public Wiz1GameInformationControl()
        {
            InitializeComponent();
        }

        public Wiz1GameInformationControl(IMain main)
        {
            InitializeComponent();
            SetMain(main);
        }

        public void SetMain(IMain main)
        {
            m_main = main;
            glvAffect.SetMain(main);
            glvMap.SetMain(main);
            glvMisc.SetMain(main);
        }

        public override GameNames Game { get { return GameNames.Wizardry1; } }

        public override void UpdateUI(GameInfo info, bool bForce)
        {
            if (!(info is Wiz1GameInfo))
                return;

            if (!bForce && m_lastInfo != null && Global.CompareBytes(info.Bytes, m_lastInfo.Bytes))
                return;

            m_lastInfo = info as Wiz1GameInfo;

            glvAffect.UpdateUI(m_lastInfo.GetEffectItems());
            glvMisc.UpdateUI(m_lastInfo.GetMiscItems());
            glvMap.UpdateUI(m_lastInfo.GetMapItems());
        }

        public override int[] Splitters
        {
            get
            {
                return new int[] { splitContainer1.SplitterDistance, splitContainer2.SplitterDistance };
            }
            set
            {
                if (value != null && value.Length > 1)
                {
                    Global.SetSplitterDistance(splitContainer1, value[0]);
                    Global.SetSplitterDistance(splitContainer2, value[1]);
                }
            }
        }

    }
}
