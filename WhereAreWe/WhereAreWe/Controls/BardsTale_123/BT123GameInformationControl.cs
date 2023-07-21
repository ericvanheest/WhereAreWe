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
    public partial class BT123GameInformationControl
        : GameInformationControl
    {
        private IMain m_main = null;
        private BTGameInfo m_lastInfo = null;

        public BT123GameInformationControl()
        {
            InitializeComponent();
        }

        public BT123GameInformationControl(IMain main)
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

        public override void UpdateUI(GameInfo info, bool bForce)
        {
            if (!(info is BTGameInfo))
                return;

            if (!bForce && m_lastInfo != null && Global.Compare(info.Bytes, m_lastInfo.Bytes))
                return;

            m_lastInfo = info as BTGameInfo;

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

    public class BT1GameInformationControl : BT123GameInformationControl
    {
        public BT1GameInformationControl(IMain main) { SetMain(main); }
        public override GameNames Game { get { return GameNames.BardsTale1; } }
    }

    public class BT2GameInformationControl : BT123GameInformationControl
    {
        public BT2GameInformationControl(IMain main) { SetMain(main); }
        public override GameNames Game { get { return GameNames.BardsTale2; } }
    }

    public class BT3GameInformationControl : BT123GameInformationControl
    {
        public BT3GameInformationControl(IMain main) { SetMain(main); }
        public override GameNames Game { get { return GameNames.BardsTale3; } }
    }
}
