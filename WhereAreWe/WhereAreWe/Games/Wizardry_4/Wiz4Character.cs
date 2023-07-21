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
    public class Wiz4Character : WizCharacter
    {
        public Wiz4Character()
        {
            Address = -1;
        }

        public override GameNames Game { get { return GameNames.Wizardry4; } }

        public override bool BackpackFull
        {
            get
            {
                if (Address > 0)
                    return base.BackpackFull;

                // Werdna's inventory is mixed in with the Black Box
                if (Inventory.Items.Count < 8)
                    return false;

                // If the backpack is full, the eighth item will have a MemoryIndex of 7; otherwise
                // the eighth item will have one of the Black Box memory indices (i.e. 8 or higher)
                return Inventory.Items[7].MemoryIndex < 8;
            }
        }

        public override int FirstEmptyBackpackIndex
        {
            get
            {
                if (Address > 0 || Inventory == null)
                    return base.FirstEmptyBackpackIndex;

                int iIndex = 0;
                while (iIndex < Inventory.Items.Count && Inventory.Items[iIndex].MemoryIndex < 8)
                    iIndex++;

                return iIndex < 8 ? iIndex : -1;
            }
        }

        public override string ExperienceString { get { return Experience.ToString(); } }

        public static Wiz4Character Create(int iCharIndex, byte[] bytes, int iIndex, Wiz4GameInfo gameInfo, WizEncounterInfo encounterInfo, List<Item> blackBox)
        {
            Wiz4Character wizChar = new Wiz4Character();
            wizChar.SetFromBytes(iCharIndex, bytes, iIndex, gameInfo, encounterInfo, blackBox);
            return wizChar;
        }

        public void SetFromBytes(int iCharIndex, byte[] bytes, int iIndex, Wiz4GameInfo gameInfo, WizEncounterInfo encounterInfo, List<Item> blackBox)
        {
            m_game = GameNames.Wizardry4;
            Address = -1;
            if (bytes == null || bytes.Length < iIndex + SizeInBytes - 1)
                return;
            SetCharFromStream(iCharIndex, new MemoryStream(bytes, iIndex, bytes.Length - iIndex), gameInfo, encounterInfo, blackBox);
        }

        public void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info, EncounterInfo encounterInfo = null, List<Item> blackBox = null)
        {
            if (stream.Length < CharacterSize)
                return;

            base.SetCharFromStream(iCharIndex, stream, info, encounterInfo, false);

            if (iCharIndex == 0) // For Werdna character only
            {
                if (Inventory.Items.Any(i => i.Index == (int) Wiz4ItemIndex.BlackBox))
                    Inventory.Items.AddRange(blackBox);
            }
        }
    }
}