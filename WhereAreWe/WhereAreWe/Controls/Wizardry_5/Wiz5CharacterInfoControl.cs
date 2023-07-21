using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WhereAreWe
{
    public partial class Wiz5CharacterInfoControl : Wiz123CharacterInfoControl
    {
        public Wiz5CharacterInfoControl()
        {
            InitializeComponent();
        }

        public Wiz5CharacterInfoControl(IMain main) : base(main)
        {
            InitializeComponent();
            m_char = new Wiz5Character();

            FindEditableAttributes();
        }

        public override void UpdateUI()
        {
            base.UpdateUI();

            Wiz5Character wizChar = m_char as Wiz5Character;

            labelSwimming.Text = String.Format("{0}", wizChar.Swimming);
            labelDeaths.Text = String.Format("{0}", wizChar.RIP);
        }

        public override TriggerControl GetTriggerControl(TriggerEntity entity, string strVal)
        {
            switch (entity)
            {
                case TriggerEntity.Deaths: return new TriggerControl(labelDeaths);
                case TriggerEntity.Swimming: return new TriggerControl(labelSwimming);
                default: return base.GetTriggerControl(entity, strVal);
            }
        }

        protected override void Stat_MouseLeave(object sender, EventArgs e) { base.Stat_MouseLeave(sender, e); }

        private void labelSwimmingHeader_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Swimming, labelSwimmingHeader); }
        private void labelSwimming_MouseEnter(object sender, EventArgs e) { SetTip(ModAttr.Swimming, labelSwimming); }

        protected override CheatMenuFlags PrepareCheatMenu(Control label, CheatMenuFlags flags = CheatMenuFlags.None)
        {
            m_cheatType = AttributeType.Int16;

            if (label == labelSwimming)
            {
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Swim });
                return CheatMenuFlags.AllNonlevel;
            }
            else if (label == m_commonCtrls.labelPoison)
            {
                m_cheatOffsets = new CheatOffsets(new int[] { m_char.Offsets.Poison });
                return CheatMenuFlags.AllNonlevel;
            }

            return base.PrepareCheatMenu(label, flags);
        }
    }
}
