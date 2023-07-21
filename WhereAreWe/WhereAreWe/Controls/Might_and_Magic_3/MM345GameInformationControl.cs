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
    public partial class MM345GameInformationControl
        : GameInformationControl
    {
        private IMain m_main = null;
        private MM345GameInfo m_lastInfo = null;

        public MM345GameInformationControl()
        {
            InitializeComponent();
        }

        public MM345GameInformationControl(IMain main)
        {
            InitializeComponent();
            SetMain(main);
        }

        public override GameNames Game { get { return GameNames.MightAndMagic3; } }

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

        public void SetMain(IMain main)
        {
            m_main = main;
            glvParty.SetMain(main);
            glvMisc.SetMain(main);
            glvMap.SetMain(main);
        }

        public override void UpdateUI(GameInfo info, bool bForce)
        {
            if (!(info is MM345GameInfo))
                return;

            MM345GameInfo mm345Info = info as MM345GameInfo;

            if (!bForce && m_lastInfo != null && Global.Compare(mm345Info.Bytes, m_lastInfo.Bytes))
                return;

            if (m_lastInfo == null ||
                !Global.Compare(mm345Info.RawParty, m_lastInfo.RawParty) ||
                !Global.Compare(mm345Info.RawMap, m_lastInfo.RawMap))
            {
                glvParty.UpdateUI(mm345Info.GetPartyItems());
                glvMisc.UpdateUI(mm345Info.GetMiscItems());
                glvMap.UpdateUI(mm345Info.GetMapItems());
            }

            m_lastInfo = mm345Info;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F5:
                    if (glvMisc.Items.Count > 0)
                        glvMisc.RedrawItems(0, glvMisc.Items.Count - 1, false);
                    if (glvMap.Items.Count > 0)
                        glvMap.RedrawItems(0, glvMap.Items.Count - 1, false);
                    if (glvParty.Items.Count > 0)
                        glvParty.RedrawItems(0, glvParty.Items.Count - 1, false);
                    break;
                default:
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }
    }

    public class MM3GameInformationControl : MM345GameInformationControl
    {
        public override GameNames Game { get { return GameNames.MightAndMagic3; } }

        public MM3GameInformationControl(IMain main) : base(main) {}
    }

    public class MM45GameInformationControl : MM345GameInformationControl
    {
        public override GameNames Game { get { return GameNames.MightAndMagic45; } }

        public MM45GameInformationControl(IMain main) : base(main) {}
    }
}
