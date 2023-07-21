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
    public partial class Wiz123GameInformationControl
        : GameInformationControl
    {
        private IMain m_main = null;
        private Wiz1234GameInfo m_lastInfo = null;

        public Wiz123GameInformationControl()
        {
            InitializeComponent();
        }

        public Wiz123GameInformationControl(IMain main)
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
            if (!(info is Wiz1234GameInfo))
                return;

            if (!bForce && m_lastInfo != null && Global.Compare(info.Bytes, m_lastInfo.Bytes))
                return;

            m_lastInfo = info as Wiz1234GameInfo;

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

    public class Wiz1GameInformationControl : Wiz123GameInformationControl
    {
        public Wiz1GameInformationControl(IMain main) { SetMain(main); }
        public override GameNames Game { get { return GameNames.Wizardry1; } }
    }

    public class Wiz2GameInformationControl : Wiz123GameInformationControl
    {
        public Wiz2GameInformationControl(IMain main) { SetMain(main); }
        public override GameNames Game { get { return GameNames.Wizardry2; } }
    }

    public class Wiz3GameInformationControl : Wiz123GameInformationControl
    {
        public Wiz3GameInformationControl(IMain main) { SetMain(main); }
        public override GameNames Game { get { return GameNames.Wizardry3; } }
    }

    public class Wiz4GameInformationControl : Wiz123GameInformationControl
    {
        public Wiz4GameInformationControl(IMain main) { SetMain(main); }
        public override GameNames Game { get { return GameNames.Wizardry4; } }
    }

    public class Wiz5GameInformationControl : Wiz123GameInformationControl
    {
        public Wiz5GameInformationControl(IMain main) { SetMain(main); }
        public override GameNames Game { get { return GameNames.Wizardry5; } }
    }
}
