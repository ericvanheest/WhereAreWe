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
    public partial class EOB123GameInformationControl
        : GameInformationControl
    {
        private IMain m_main = null;
        private EOBGameInfo m_lastInfo = null;

        public EOB123GameInformationControl()
        {
            InitializeComponent();
        }

        public EOB123GameInformationControl(IMain main)
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
            if (!(info is EOBGameInfo))
                return;

            if (!bForce && m_lastInfo != null && Global.Compare(info.Bytes, m_lastInfo.Bytes))
                return;

            m_lastInfo = info as EOBGameInfo;

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

    public class EOB1GameInformationControl : EOB123GameInformationControl
    {
        public EOB1GameInformationControl(IMain main) { SetMain(main); }
        public override GameNames Game { get { return GameNames.EyeOfTheBeholder1; } }
    }

    public class EOB2GameInformationControl : EOB123GameInformationControl
    {
        public EOB2GameInformationControl(IMain main) { SetMain(main); }
        public override GameNames Game { get { return GameNames.EyeOfTheBeholder2; } }
    }

    public class EOB3GameInformationControl : EOB123GameInformationControl
    {
        public EOB3GameInformationControl(IMain main) { SetMain(main); }
        public override GameNames Game { get { return GameNames.EyeOfTheBeholder3; } }
    }
}
