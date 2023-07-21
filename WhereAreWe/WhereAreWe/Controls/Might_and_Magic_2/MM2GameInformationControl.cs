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
    public partial class MM2GameInformationControl
        : GameInformationControl
    {
        private IMain m_main = null;
        private MM2GameInfo m_lastInfo = null;

        public MM2GameInformationControl()
        {
            InitializeComponent();
        }

        public MM2GameInformationControl(IMain main)
        {
            InitializeComponent();
            SetMain(main);
        }

        public override GameNames Game { get { return GameNames.MightAndMagic2; } }

        public void SetMain(IMain main)
        {
            m_main = main;
            glvAffect.SetMain(main);
            glvMisc.SetMain(main);
            glvMap.SetMain(main);
        }

        public override void UpdateUI(GameInfo info, bool bForce)
        {
            if (!(info is MM2GameInfo))
                return;

            if (!bForce && m_lastInfo != null && Global.Compare(info.Bytes, m_lastInfo.Bytes))
                return;

            m_lastInfo = info as MM2GameInfo;

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
