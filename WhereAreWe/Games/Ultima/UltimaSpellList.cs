using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public class UltimaSpellList : SpellList
    {
        List<UltimaSpell> m_spells;

        public override Spell GetSpell(int index) { return index < 0 || index >= m_spells.Count ? null : m_spells[index]; }

        public List<UltimaSpell> Spells
        {
            get { return m_spells; }
        }

        public UltimaSpellList()
        {
            m_spells = new List<UltimaSpell>(49);
            m_spells.Add(UltimaSpell.None);
            m_spells.Add(new UltimaSpell(UltimaSpellIndex.Prayer, SpellType.Any, "Prayer", "Cast Random Spell", "The ability, when in dire straits, to call upon one's personal deity in hopes of finding a way out of a pressing dilemma. Should be used only when the spellcaster is in serious need of divine aid. (Casts spell regardless of class.  23% chance of casting \"Ladder Down\", 11% chance each of casting one of: Ladder Up, Blink, Create, Destroy, Unlock, Missile, Kill)"));
            m_spells.Add(new UltimaSpell(UltimaSpellIndex.Open, SpellType.Any, "Open", "Open Coffin", "This spell permits the opening of coffins at no risk to the spellcaster by magically disarming any traps."));
            m_spells.Add(new UltimaSpell(UltimaSpellIndex.Unlock, SpellType.Any, "Unlock", "Open Chest", "This spell permits the opening of chests at no risk to the spellcaster by magically disarming any traps."));
            m_spells.Add(new UltimaSpell(UltimaSpellIndex.MagicMissile, SpellType.Any, "Magic Missile", "3-INT damage", "The ability to strike a foe with a blast of magical force. The more skilled and well equipped the spellcaster, the greater the damage inflicted by the blast. (Deals damage up to the value of your Intelligence stat, minimum 3)"));
            m_spells.Add(new UltimaSpell(UltimaSpellIndex.Steal, SpellType.Any, "Steal", "Steal", "(This spell is present in the game code but cannot be obtained by ordinary means)"));
            m_spells.Add(new UltimaSpell(UltimaSpellIndex.LadderDown, SpellType.Any, "Ladder Down", "Create Ladder Down", "This enchantment creates a magical ladder which permits the spellcaster to descend to the next level of a dungeon."));
            m_spells.Add(new UltimaSpell(UltimaSpellIndex.LadderUp, SpellType.Any, "Ladder Up", "Create Ladder Up", "This enchantment creates a magical ladder which permits the spellcaster to ascend to the next level of a dungeon."));
            m_spells.Add(new UltimaSpell(UltimaSpellIndex.Blink, SpellType.Wizard, "Blink", "Teleport Randomly", "The ability to be physically transported a short distance while underground (Teleports to any open dungeon square between 2,2 and 9,9)."));
            m_spells.Add(new UltimaSpell(UltimaSpellIndex.Create, SpellType.Wizard, "Create", "Create Barrier", "The ability to create a wall of magical force directly in front of the spellcaster."));
            m_spells.Add(new UltimaSpell(UltimaSpellIndex.Destroy, SpellType.Wizard, "Destroy", "Remove Barrier", "The ability to remove a wall of magical force that blocks the spellcaster's path."));
            m_spells.Add(new UltimaSpell(UltimaSpellIndex.Kill, SpellType.Wizard, "Kill", "Destroy Monster", "An enchantment hurled at a foe in front of the spellcaster. If successful, this cantrip will destroy the opponent."));
        }
    }

    public enum UltimaSpellIndex
    {
        None = 0,
        Prayer,
        Open,
        Unlock,
        MagicMissile,
        Steal,
        LadderDown,
        LadderUp,
        Blink,
        Create,
        Destroy,
        Kill,
    }

    public class UltimaSpell : Spell
    {
        public UltimaSpellIndex Index;

        public UltimaItemIndex ItemIndex
        {
            get
            {
                switch (Index)
                {
                    case UltimaSpellIndex.Open: return UltimaItemIndex.Open;
                    case UltimaSpellIndex.Unlock: return UltimaItemIndex.Unlock;
                    case UltimaSpellIndex.MagicMissile: return UltimaItemIndex.MagicMissile;
                    case UltimaSpellIndex.Steal: return UltimaItemIndex.Steal;
                    case UltimaSpellIndex.LadderDown: return UltimaItemIndex.LadderDown;
                    case UltimaSpellIndex.LadderUp: return UltimaItemIndex.LadderUp;
                    case UltimaSpellIndex.Blink: return UltimaItemIndex.Blink;
                    case UltimaSpellIndex.Create: return UltimaItemIndex.Create;
                    case UltimaSpellIndex.Destroy: return UltimaItemIndex.Destroy;
                    case UltimaSpellIndex.Kill: return UltimaItemIndex.Kill;
                    default: return UltimaItemIndex.None;
                }
            }
        }

        public UltimaSpell(UltimaSpellIndex index, SpellType type, string name, string shortDesc, string desc)
        {
            Index = index;
            Name = name;
            Type = SpellType.Any;
            When = SpellWhen.AnywhereAnytime;
            ShortDescription = shortDesc;
            Description = desc;
            Learned = "[town name?]";
            if (type == SpellType.Wizard)
                Learned += " (Wizard Only)";
            Cost = new SpellCost(0);
        }

        public static UltimaSpell None => new UltimaSpell(UltimaSpellIndex.None, SpellType.Unknown, "None", "", "");
        public override Keys[] GetKeys(BaseCharacter character) => new Keys[0];
        public override string MultiLineDescription => base.MultiLineDescription + "\r\nLearned: " + Learned;
    }
}
