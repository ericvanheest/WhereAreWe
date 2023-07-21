using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace WhereAreWe
{
    public partial class EOB123TrapsControl : TrapsControl
    {
        public class CharTag
        {
            public BaseCharacter Character;
            public override string ToString() { return Character == null ? "<invalid>" : Character.Name; }

            public CharTag(BaseCharacter bc)
            {
                Character = bc;
            }
        }

        List<BaseCharacter> m_chars = null;

        public EOB123TrapsControl()
        {
            InitializeComponent();
            Init();
        }

        public EOB123TrapsControl(IMain main)
        {
            InitializeComponent();
            SetMain(main);
            Init();
        }

        private void Init()
        {
        }

        public override void OnLoad()
        {
            base.OnLoad();
        }

        protected override void OnMainSet()
        {
        }

        public override void SetSearchResults(SearchResults results)
        {
            if (Hacker == null || !Hacker.IsValid || results == null)
                return;

            m_chars = Hacker.GetCharacters();
            if (m_chars == null)
                return;

            comboCharacter.Items.Clear();
            foreach(BaseCharacter bc in m_chars)
                comboCharacter.Items.Add(new CharTag(bc));

            comboTrapType.Items.Clear();

            TrapInfo treasure = Hacker.CreateTrapInfo(0);

            for(int trap = 0; trap < treasure.TrapCount; trap++)
                comboTrapType.Items.Add(new TrapTag(Hacker.CreateTrapInfo(trap)));

            // Select the character with the highest "disarm trap" skill
            int iBestDisarm = 0;
            int iBestChar = -1;
            for(int i = 0; i < comboCharacter.Items.Count; i++)
            {
                EOB1Character eobChar = ((CharTag) comboCharacter.Items[i]).Character as EOB1Character;
                if (eobChar == null)
                    continue;
                int iDisarm = EOBCharacter.IsThief(eobChar.Class) ? eobChar.Disarm : eobChar.Level;
                if (iDisarm > iBestDisarm)
                {
                    iBestChar = i;
                    iBestDisarm = iDisarm;
                }
            }

            if (iBestChar != -1)
                comboCharacter.SelectedIndex = iBestChar;

            comboTrapType.SelectedIndex = 0;

            if (results is EOBSearchResults)
            {
                // Select the first possible trap type
                int trap = ((EOBSearchResults)results).Trap;
                foreach (TrapTag tag in comboTrapType.Items)
                {
                    if (tag.Trap == trap)
                    {
                        comboTrapType.SelectedItem = tag;
                        break;
                    }
                }
            }
        }

        private void btnDisarm_Click(object sender, EventArgs e)
        {
            DisarmTrap();
        }

        public override bool DisarmTrap()
        {
            if (Hacker == null || !Hacker.IsValid || comboCharacter.SelectedItem == null)
                return false;

            EOBGameState state = Hacker.GetGameState() as EOBGameState;
            int iChar = ((CharTag) comboCharacter.SelectedItem).Character.BasicAddress;
            iChar = state.MarchingChar(Hacker, iChar);

            Keys[] keys = null;
            switch (state.Main)
            {
                case MainState.TreasureCouldNotDisarm:
                    keys = new Keys[] { Keys.Space, Keys.D, Keys.D1 + iChar };
                    break;
                case MainState.Treasure:
                    keys = new Keys[] { Keys.Escape, Keys.Escape, Keys.D, Keys.D1 + iChar };
                    break;
                case MainState.TreasureEnterTrapType:
                    // Might not be the right character, so back out of this first
                    keys = new Keys[] { Keys.Enter, Keys.D, Keys.D1 + iChar };
                    break;
                case MainState.TreasureWhoWillDisarm:
                    keys = new Keys[] { Keys.D0 + iChar };
                    break;
                case MainState.TreasureWhoWillOpen:
                case MainState.TreasureWhoWillInspect:
                case MainState.TreasureWhoWillCalfo:
                    keys = new Keys[] { Keys.Enter, Keys.D, Keys.D1 + iChar };
                    break;
                default:
                    // Can't disarm from an unknown state
                    return false;
            }

            if (keys != null)
                Hacker.SendKeysToDOSBox(keys, true);

            TrapTag tag = comboTrapType.SelectedItem as TrapTag;
            Hacker.SendStringToDOSBox(tag.ToString().ToLower());
            Hacker.SendKeysToDOSBox(new Keys[] { Keys.Enter }, true);

            return true;
        }
    }

}
