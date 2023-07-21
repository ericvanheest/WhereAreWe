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
    public partial class Wiz123TrapsControl : TrapsControl
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

        public Wiz123TrapsControl()
        {
            InitializeComponent();
            Init();
        }

        public Wiz123TrapsControl(IMain main)
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

            TrapInfo trapInfo = Hacker.CreateTrapInfo(0);
            if (trapInfo == null)
                return;

            comboTrapType.Items.Clear();
            for (int i = 0; i < trapInfo.TrapCount; i++)
            {
                TrapInfo ti = Hacker.CreateTrapInfo(i);
                if (ti != null)
                    comboTrapType.Items.Add(new TrapTag(ti));
            }

            // Select the character with the highest "disarm trap" skill
            int iBestDisarm = 0;
            int iBestChar = -1;
            for(int i = 0; i < comboCharacter.Items.Count; i++)
            {
                WizCharacter wiz1Char = ((CharTag) comboCharacter.Items[i]).Character as WizCharacter;
                if (wiz1Char == null)
                    continue;
                // The disarm chance is also based on the current map, but that doesn't matter for this relative calculation
                int iDisarm = wiz1Char.Level + ((wiz1Char.Class == WizClass.Thief || wiz1Char.Class == WizClass.Ninja) ? 50 : 0);
                if (iDisarm > iBestDisarm)
                {
                    iBestChar = i;
                    iBestDisarm = iDisarm;
                }
            }

            if (iBestChar != -1)
                comboCharacter.SelectedIndex = iBestChar;

            comboTrapType.SelectedIndex = 0;

            if (results is Wiz123SearchResults)
            {
                // Select the first possible trap type
                int iReward = ((Wiz123SearchResults)results).RewardIndex;
                if (iReward >= 0 && iReward < Wiz1.Treasures.Count)
                {
                    WizTrapInfo.TrapFlags flags = Wiz1.Treasures[iReward].Trap.Traps;
                    foreach (TrapTag tag in comboTrapType.Items)
                    {
                        if (tag.Trap == (int) WizTrapInfo.WizTrap.Alarm && flags.HasFlag(WizTrapInfo.TrapFlags.Alarm))
                        {
                            comboTrapType.SelectedItem = tag;
                            break;
                        }
                        if (tag.Trap == (int)WizTrapInfo.WizTrap.CrossbowBolt && flags.HasFlag(WizTrapInfo.TrapFlags.MiscTrap))
                        {
                            comboTrapType.SelectedItem = tag;
                            break;
                        }
                        if (tag.Trap == (int)WizTrapInfo.WizTrap.GasBomb && flags.HasFlag(WizTrapInfo.TrapFlags.GasBomb))
                        {
                            comboTrapType.SelectedItem = tag;
                            break;
                        }
                        if (tag.Trap == (int)WizTrapInfo.WizTrap.MageBlaster && flags.HasFlag(WizTrapInfo.TrapFlags.MageBlaster))
                        {
                            comboTrapType.SelectedItem = tag;
                            break;
                        }
                        if (tag.Trap == (int)WizTrapInfo.WizTrap.PoisonNeedle && flags.HasFlag(WizTrapInfo.TrapFlags.PoisonNeedle))
                        {
                            comboTrapType.SelectedItem = tag;
                            break;
                        }
                        if (tag.Trap == (int)WizTrapInfo.WizTrap.PriestBlaster && flags.HasFlag(WizTrapInfo.TrapFlags.PriestBlaster))
                        {
                            comboTrapType.SelectedItem = tag;
                            break;
                        }
                        if (tag.Trap == (int)WizTrapInfo.WizTrap.Teleporter && flags.HasFlag(WizTrapInfo.TrapFlags.Teleporter))
                        {
                            comboTrapType.SelectedItem = tag;
                            break;
                        }
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
            if (Hacker == null || !Hacker.IsValid)
                return false;

            WizGameState state = Hacker.GetGameState() as WizGameState;
            int iChar = ((CharTag) comboCharacter.SelectedItem).Character.BasicAddress;

            Keys[] keys = null;
            switch (state.Main)
            {
                case MainState.TreasureCouldNotDisarm:
                    keys = new Keys[] { Keys.Space, Keys.D, Keys.D1 + iChar };
                    break;
                case MainState.Treasure:
                    keys = new Keys[] { Keys.D, Keys.D1 + iChar };
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
