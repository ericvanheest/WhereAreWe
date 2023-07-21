using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public class EOB1SpellList : SpellList
    {
        List<EOB1Spell> m_spells;

        public override Spell GetSpell(int index) { return index < 0 || index >= m_spells.Count ? null : m_spells[index]; }

        public static EOBSpellIndex[] Mage = new EOBSpellIndex[]
        {
            EOBSpellIndex.Armor,
            EOBSpellIndex.BurningHands,
            EOBSpellIndex.DetectMagicMage,
            EOBSpellIndex.MagicMissile,
            EOBSpellIndex.ReadMagic,
            EOBSpellIndex.Shield,
            EOBSpellIndex.ShockingGrasp,
            EOBSpellIndex.Invisibility,
            EOBSpellIndex.Knock,
            EOBSpellIndex.MelfsAcidArrow,
            EOBSpellIndex.StinkingCloud,
            EOBSpellIndex.DispelMagicMage,
            EOBSpellIndex.Fireball,
            EOBSpellIndex.FlameArrow,
            EOBSpellIndex.Haste,
            EOBSpellIndex.HoldPersonMage,
            EOBSpellIndex.Invisibility10,
            EOBSpellIndex.LightningBolt,
            EOBSpellIndex.VampiricTouch,
            EOBSpellIndex.Fear,
            EOBSpellIndex.IceStorm,
            EOBSpellIndex.Stoneskin,
            EOBSpellIndex.Cloudkill,
            EOBSpellIndex.ConeOfCold,
            EOBSpellIndex.HoldMonster
        };

        public static EOBSpellIndex[] Cleric = new EOBSpellIndex[]
        {
            EOBSpellIndex.Bless,
            EOBSpellIndex.CureLightWounds,
            EOBSpellIndex.CauseLightWounds,
            EOBSpellIndex.DetectMagicCleric,
            EOBSpellIndex.ProtectionFromEvil,
            EOBSpellIndex.Aid,
            EOBSpellIndex.FlameBlade,
            EOBSpellIndex.HoldPersonCleric,
            EOBSpellIndex.SlowPoison,
            EOBSpellIndex.CreateFood,
            EOBSpellIndex.DispelMagicCleric,
            EOBSpellIndex.MagicalVestment,
            EOBSpellIndex.Prayer,
            EOBSpellIndex.RemoveParalysis,
            EOBSpellIndex.CureSeriousWounds,
            EOBSpellIndex.CauseSeriousWounds,
            EOBSpellIndex.NeutralizePoison,
            EOBSpellIndex.ProtectionFromEvil10,
            EOBSpellIndex.ProtectionFromLightning,
            EOBSpellIndex.CureCriticalWounds,
            EOBSpellIndex.CauseCriticalWounds,
            EOBSpellIndex.FlameStrike,
            EOBSpellIndex.RaiseDead,
            EOBSpellIndex.LayOnHands
        };

        public List<EOB1Spell> Spells
        {
            get { return m_spells; }
        }

        public EOB1SpellList()
        {
            m_spells = new List<EOB1Spell>(49);
            m_spells.Add(EOB1Spell.None);
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Armor, SpellType.Mage, 1, SpellRange.Character, "Armor", SpellTarget.Character, SpellDuration.Special, "Protection AC6", "With this spell the mage can surround a character with a magical field that protects as chain mail (AC 6). The spell has no effect on characters who already have AC 6 or greater and it does not have a cumulative effect with the Shield spell.  The spell lasts until the character suffers 8 points +1 per level of the caster of damage or a Dispel Magic is cast."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.BurningHands, SpellType.Mage, 1, SpellRange.Close, "Burning Hands", SpellTarget.Monster, SpellDuration.Instant, "1d3 +2/level damage", "When a mage casts this spell, a jet of searing flame shoots from his fingertips.  The damage inflicted by the flame increases as the mage increases in level and gains power.  The spell does one to three points of damage plus two points per level of the caster.  For example, a 10th level mage would do 21-23 points of damage."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.DetectMagicMage, SpellType.Mage, 1, SpellRange.Character, "Detect Magic", SpellTarget.Inventory, SpellDuration.Short, "Highlight magical items", "This spell allows a mage to determine if any of the items carried by members of the party are magically enchanted.  All magic items in the party are indicated for a short period of time."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.MagicMissile, SpellType.Mage, 1, SpellRange.Far, "Magic Missile", SpellTarget.Monster, SpellDuration.Instant, "1d4+1 damage per 2 levels", "The mage creates a bolt of magic force that unerringly strikes one target.  If there are two monsters, the missile automatically hits the one on the same side as the caster.  Magic Missiles do greater damage as a mage increases in level.  Initially, Magic Missile does two to five points of damage, and for every two extra levels the spell does two to five more points.  So a first or second-level mage does two to five points of damage, but a third or fourth-level mage does four to ten, and so on."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.ReadMagic, SpellType.Mage, 1, SpellRange.Character, "Read Magic", SpellTarget.Character, SpellDuration.Instant, "Not implemented", "This spell is not implemented (but has an index in the spell table)"));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Shield, SpellType.Mage, 1, SpellRange.Character, "Shield", SpellTarget.Caster, SpellDuration.Medium, "AC2 vs. hurled, AC3 vs propelled", "This spell produces an invisible barrier in front of the mage that totally blocks Magic Missile attacks.  It also offers AC 2 against hurled weapons (darts, spears) and AC 3 against propelled missiles (arrows, sling-stones).  The spell does not have a cumulative effect with the Armor spell.  The spell duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.ShockingGrasp, SpellType.Mage, 1, SpellRange.Close, "Shocking Grasp", SpellTarget.Monster, SpellDuration.Special, "1d8 +1/level damage", "This spell magically charges the casters hand with a powerful electrical field.  The field remains in place until the spell dissipates naturally or the character touches an adjacent monster.  When the spell is cast a hand picture appears in the casters primary hand - Use this as you would any other weapon.  The spell does one to eight points of damage plus one point per level of the caster.  For example, a 10th-level mage does 11-18 points of damage.  The amount of time it takes the spell to dissipate ranges from medium to long with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Invisibility, SpellType.Mage, 2, SpellRange.Character, "Invisibility", SpellTarget.Monster, SpellDuration.Special, "Hide character until attack", "This spell causes the target to vanish from sight.  The invisible character remains unseen until he attacks a monster or is hit.  Certain powerful monsters can sense invisible characters, or even see them outright."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Knock, SpellType.Mage, 2, SpellRange.Character, "Knock", SpellTarget.Character, SpellDuration.Instant, "Not implemented", "This spell is not implemented (but has an index in the spell table)"));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.MelfsAcidArrow, SpellType.Mage, 2, SpellRange.Far, "Melf's Acid Arrow", SpellTarget.Monster, SpellDuration.Special, "2d4 damage per 3 levels", "This spell creates a magical arrow that launches itself at a target as though it were fired by a fighter of the same level as the mage (including the dexterity missile modifier).  The arrow is not affected by distance.  The arrow does two to eight points of damage per attack.  For every three levels the mage has earned, the arrow gains an additional attack.  For example, at third to fifth-level the arrow attacks twice, and at sixth to eighth-level the arrow attacks three times."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.StinkingCloud, SpellType.Mage, 2, SpellRange.Medium, "Stinking Cloud", SpellTarget.OneSquare, SpellDuration.Medium, "Incapacitate creatures", "This spell creates a billowing mass of noxious vapor.  Any creature or character entering the cloud has a chance of becoming incapacitated by nausea.  The spell duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.DispelMagicMage, SpellType.Mage, 3, SpellRange.Far, "Dispel Magic", SpellTarget.Party, SpellDuration.Instant, "Remove magical effects", "This spell negates the effects of any spell affecting the party.  Dispel does not counter Cure spells, but it will dispel Hold Person, Cloudkill and similar spells."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Fireball, SpellType.Mage, 3, SpellRange.Far, "Fireball", SpellTarget.OneSquare, SpellDuration.Instant, "1d6 damage per level", "A fireball is an explosive blast of flame that damages everything in the target square.  The explosion does one to six points of damage for every level of the caster to a maximum of 10th-level.  For example, a 10th-level mage does 10-60 points of damage."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.FlameArrow, SpellType.Mage, 3, SpellRange.Far, "Flame Arrow", SpellTarget.Monster, SpellDuration.Special, "3-30 damage per 10 levels", "The caster of this spell can fire a flaming energy 'arrow' that does 3 to 30 hit points of damage (plus the dexterity missile modifier).  When the mage reaches 10th-level the amount of damage is doubled to 6 to 60 points."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Haste, SpellType.Mage, 3, SpellRange.Character, "Haste", SpellTarget.OneMonsterPerLevel, SpellDuration.Medium, "Double move and fight speed", "This spell causes all targets to move and fight at double their normal rate.  The spell's duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.HoldPersonMage, SpellType.Mage, 3, SpellRange.Far, "Hold Person", SpellTarget.FourMonsters, SpellDuration.Medium, "Paralyze humanoid", "This spell can affect humans, demi-humans, or humanoid creatures.  Creatures that are affected become rigid and unable to move or speak.  Spell duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Invisibility10, SpellType.Mage, 3, SpellRange.Character, "Invisibility 10' Radius", SpellTarget.Party, SpellDuration.Special, "Hide party until attack", "This spell is similar to the second-level Invisibility spell, except that the entire party is affected.  If an individual character is hit while under the spell's effect, that character becomes visible.  If any character in the party attacks, the spell is broken for all."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.LightningBolt, SpellType.Mage, 3, SpellRange.Far, "Lightning Bolt", SpellTarget.TwoSquares, SpellDuration.Instant, "1d6 damage per level", "This spell allows the mage to cast a powerful bolt of electrical energy.  The spell flies to its first target and then continues into the next square.  The bolt does one to six points of damage for every level of the caster to a maximum of 10th-level.  For example, a 10th-level mage does 10-60 points of damage."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.VampiricTouch, SpellType.Mage, 3, SpellRange.Close, "Vampiric Touch", SpellTarget.Caster, SpellDuration.OneRound, "Drain 1d6 HP per 2 levels", "When the caster touches an opponent with a successful attack, The spell does 1-6 points of damage for every two levels of the mage.  For example, a 10th-level mage would do 5-30 points of damage.  These points in turn are transferred temporarily to the mage, so any damage he takes is subtracted from these points first.  When the spell is cast a hand picture appears in the caster's primary hand - Use this as you would any other weapon.  This spell does not affect undead monsters such as skeletons."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Fear, SpellType.Mage, 4, SpellRange.Character, "Fear", SpellTarget.TwoSquares, SpellDuration.Medium, "Cause creatures to flee", "When this spell is cast the mage projects an invisible cone of terror.  Any creature affected by the spell will turn tail and run from the party.  The amount of time the affected creatures remain terrified is based on the level of the casting mage.  The spell's duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.IceStorm, SpellType.Mage, 4, SpellRange.Far, "Ice Storm", SpellTarget.ThreeSquaresCross, SpellDuration.Instant, "3d10 damage", "The magically created storm this spell produces is a pounding torrent of huge hailstones.  The spell pummels the targets with 3-30 points of damage.  The range of this spell is based on the casters level."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Stoneskin, SpellType.Mage, 4, SpellRange.Character, "Stoneskin", SpellTarget.Character, SpellDuration.Special, "Negate 1d4 +1/level attacks", "This spell grants the recipient virtual immunity to any attack by cut, blow, projectile or the like.  Stoneskin protects the user from almost any non-magical attack.  The spell lasts for one to four attacks plus one for every two levels of the caster.  For example, a ninth-level mage casting Stoneskin would protect against five to eight attacks."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Cloudkill, SpellType.Mage, 5, SpellRange.Close, "Cloudkill", SpellTarget.OneSquare, SpellDuration.Medium, "Kill creatures", "This spell creates a billowing cloud of ghastly yellowish-green vapor that instantly kills lesser monsters such as giant leeches, while creatures such as hell hounds have a chance to avoid death.  The spell's duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.ConeOfCold, SpellType.Mage, 5, SpellRange.Character, "Cone of Cold", SpellTarget.ThreeSquares, SpellDuration.Instant, "1d4+1 damage per level", "This spell causes the mage to project a chilling cone of sub-zero cold.  The numbing cone causes two to five points of damage per level of the caster.  For example, a 10th-level mage would do 20-50 points of damage."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.HoldMonster, SpellType.Mage, 5, SpellRange.Far, "Hold Monster", SpellTarget.OneSquare, SpellDuration.Medium, "Paralyze living creature", "This spell is similar to the Hold Person spell except that it affects a wider range of creatures.  The spell does not affect undead creatures.  The spell's duration increases with the the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Bless, SpellType.Cleric, 1, SpellRange.Character, "Bless", SpellTarget.Party, SpellDuration.Medium, "+? Attack bonus", "Upon uttering this spell the morale of the entire party is raised.  All characters gain a bonus to their attacks.  Bless spells are not cumulative.  Bless can be cast by paladins."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.CureLightWounds, SpellType.Cleric, 1, SpellRange.Character, "Cure Light Wounds", SpellTarget.Character, SpellDuration.Permanent, "Heal 1d8 HP", "By casting this spell on a wounded character the cleric can heal one to eight hit points of damage.  Cure Light Wounds can be cast by paladins."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.CauseLightWounds, SpellType.Cleric, 1, SpellRange.Close, "Cause Light Wounds", SpellTarget.Monster, SpellDuration.Permanent, "1d8 damage", "By casting this spell the cleric can cause one to eight hit points of damage."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.DetectMagicCleric, SpellType.Cleric, 1, SpellRange.Character, "Detect Magic", SpellTarget.Inventory, SpellDuration.Instant, "Highlight magical items", "This spell allows the caster to determine if any of the items carried by members of the party are magically enchanted.  All magic items in the party are indicated for a short period of time.  Detect Magic can be cast by paladins."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.ProtectionFromEvil, SpellType.Cleric, 1, SpellRange.Character, "Protection from Evil", SpellTarget.Character, SpellDuration.Medium, "Prevent evil-aligned attacks", "This spell envelops the recipient in a magical shell.  The shell inhibits the attacks of any evil-aligned creatures.  The spell's duration increases with the level of the caster.  Can be cast by paladins."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Aid, SpellType.Cleric, 2, SpellRange.Character, "Aid", SpellTarget.Character, SpellDuration.Medium, "+? Attack bonus, +1d8 Max HP", "This spell acts like a Bless spell plus it confers one to eight extra hit points to the recipient.  The temporary hit points are subtracted before the character's own if he is injured in combat.  The spell's duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.FlameBlade, SpellType.Cleric, 2, SpellRange.Close, "Flame Blade", SpellTarget.Monster, SpellDuration.Medium, "Create 1d4+6 sword", "This spell causes a blazing scimitar-like flame to leap from the caster's hand.  The blade attacks like a normal sword and normally does 7-10 points of damage.  When the spell is cast a burning sword picture appears in the caster's primary hand - Use this as you would any other weapon.  The spell does slightly less damage against targets protected from fire.  Spell duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.HoldPersonCleric, SpellType.Cleric, 2, SpellRange.Far, "Hold Person", SpellTarget.OneSquare, SpellDuration.Medium, "Paralyze humanoid", "This spell can affect human, demi-human, or humanoid creatures.  Creatures that are affected become rigid and unable to move or speak.  Spell duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.SlowPoison, SpellType.Cleric, 2, SpellRange.Character, "Slow Poison", SpellTarget.Character, SpellDuration.Long, "Delay poison effects", "This spell slows the effects of any type of poison for a limited amount of time.  When the spell dissipates the victims suffer the poison's full effect unless a Neutralize Poison spell can be cast.  The spell's duration increases with the level of the caster.  Can be cast by paladins."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.CreateFood, SpellType.Cleric, 3, SpellRange.Character, "Create Food & Water", SpellTarget.Special, SpellDuration.Permanent, "Set food to maximum", "This spell allows the cleric to conjure nourishment for the entire party.  When characters' food bars are blank, and they do not eat, they suffer 1 hit point of damage every 24 hours and starving mages and clerics are unable to regain spells."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.DispelMagicCleric, SpellType.Cleric, 3, SpellRange.Far, "Dispel Magic", SpellTarget.Party, SpellDuration.Instant, "Remove magical effects", "This spell negates the effects of any spell affecting the party.  Dispel Magic does not counter Cure spells, but it will dispel Hold Person, Cloudkill, Bless, and similar spells."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.MagicalVestment, SpellType.Cleric, 3, SpellRange.Character, "Magical Vestment", SpellTarget.Caster, SpellDuration.Medium, "Protection AC5 +1 per 3 levels above 5", "This spell enchants the cleric's own robes, providing protection at least equivalent to chain mail (AC 5).  The vestment gains a +1 enchantment for every three levels the cleric earns above fifth-level.  For example, a 10th-level cleric would have AC 3 protection.  This spell is not cumulative with itself or any other spells or armor.  The spell's duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.Prayer, SpellType.Cleric, 3, SpellRange.Character, "Prayer", SpellTarget.Party, SpellDuration.Medium, "+? Attack bonus, -? Enemy attack", "This spell is a more powerful version of the first-level Bless.  This spell increases the party's combat ability and decreases the enemy's.  The spell has no cumulative effect.  The spell's duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.RemoveParalysis, SpellType.Cleric, 3, SpellRange.Character, "Remove Paralysis", SpellTarget.Party, SpellDuration.Permanent, "Remove paralyzed condition", "This spell negates the effects of any type of paralyzation or related magic.  The spell counters Hold or Slow spells."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.CureSeriousWounds, SpellType.Cleric, 4, SpellRange.Character, "Cure Serious Wounds", SpellTarget.Character, SpellDuration.Permanent, "Heal 2d8+1 HP", "This spell is identical to the first-level Cure Light Wounds, except that it heals 3-17 hit points of damage."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.CauseSeriousWounds, SpellType.Cleric, 4, SpellRange.Close, "Cause Serious Wounds", SpellTarget.Monster, SpellDuration.Permanent, "2d8+1 damage", "This spell is identical to the first-level Cause Light Wounds, except that it inflicts 3-17 hit points of damage."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.NeutralizePoison, SpellType.Cleric, 4, SpellRange.Character, "Neutralize Poison", SpellTarget.Character, SpellDuration.Permanent, "Remove poison effects", "This spell detoxifies any sort of poison or venom and counters the effects in any character.  The spell cannot return to life characters who have died from poison."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.ProtectionFromEvil10, SpellType.Cleric, 4, SpellRange.Character, "Protection from Evil 10' Radius", SpellTarget.Party, SpellDuration.Long, "Prevent evil - aligned attacks", "This spell is identical to the first - level spell except that it affects the entire party.The spell's duration increases with the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.ProtectionFromLightning, SpellType.Cleric, 4, SpellRange.Character, "Protection from Lightning", SpellTarget.Character, SpellDuration.Special, "10 pts/level or 50% electrical prot.", "This spell grants protection from any type of electrical attack.  If the spell recipient is the caster, the cleric receives complete protection against attacks until the spell dissipates or it has absorbed 10 points times the cleric's level of electrical damage.  If the recipient is a character other than the caster, the spell confers bonuses against electrical attacks, and reduces damage by 50%.  The spell duration ranges from medium to long with the the level of the caster."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.CureCriticalWounds, SpellType.Cleric, 5, SpellRange.Character, "Cure Critical Wounds", SpellTarget.Character, SpellDuration.Permanent, "3d8+3 damage", "This spell is identical to the first-level Cure Light Wounds, except that it heals 6-27 hit points of damage."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.CauseCriticalWounds, SpellType.Cleric, 5, SpellRange.Character, "Cause Critical Wounds", SpellTarget.Monster, SpellDuration.Permanent, "Heal 3d8+3 HP", "This spell is identical to The first-level Cause Light Wounds, except that it inflicts 6-27 hit points of damage."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.FlameStrike, SpellType.Cleric, 5, SpellRange.Far, "Flame Strike", SpellTarget.OneSquare, SpellDuration.Instant, "6d8 damage", "By means of this spell, the cleric calls down from the sky a column of flame.  Creatures fully affected by the spell suffer 6-48 points of damage."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.RaiseDead, SpellType.Cleric, 5, SpellRange.Character, "Raise Dead", SpellTarget.Character, SpellDuration.Permanent, "Remove dead condition", "This spell allows the cleric to attempt to restore life to any non-elven character.  Chances for success are based on the deceased character's constitution and chance.  Whenever a character is raised his constitution is permanently reduced by one point."));
            m_spells.Add(new EOB1Spell(EOBSpellIndex.LayOnHands, SpellType.Cleric, 5, SpellRange.Character, "Lay On Hands", SpellTarget.Character, SpellDuration.Permanent, "Heal 2 HP/level", "Restores 2 HP per level of advancement."));
        }
    }

    public class EOB1Spell : EOBSpell
    {
        public EOBSpellIndex Index;
        public override int BasicIndex { get { return (int)Index; } }
        public override bool UsesLevelOnly { get { return true; } }

        public EOB1Spell(EOBSpellIndex index, SpellType type, int level, SpellRange range, string name, SpellTarget target, SpellDuration duration, string shortDesc, string desc)
        {
            Index = index;
            Name = name;
            Type = type;
            Level = level;
            When = SpellWhen.AnywhereAnytime;
            Target = target;
            Range = range;
            ShortDescription = shortDesc;
            Description = desc;
            Duration = duration;
            Learned = String.Format("Available to {0} class at level {1}", MMSpell.TypeString(Type), Level * 2 - 1);
            Cost = new SpellCost(0);
        }

        public static EOB1Spell None
        {
            get { return new EOB1Spell(EOBSpellIndex.None, SpellType.Unknown, 0, SpellRange.Character, "None", SpellTarget.Unknown, SpellDuration.Instant, "", ""); }
        }

        public override Keys[] GetKeys(BaseCharacter character)
        {
            return new Keys[0];
        }

        public override string MultiLineDescription { get { return base.MultiLineDescription + "\r\nLearned: " + Learned; } }
    }
}
