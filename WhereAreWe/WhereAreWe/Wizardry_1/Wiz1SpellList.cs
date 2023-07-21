using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public enum Wiz1SpellIndex
    {
        None = 0,
        Halito = 1,   // Mage
        Mogref,       // Mage
        Katino,       // Mage
        Dumapic,      // Mage
        Dilto,        // Mage
        Sopic,        // Mage
        Mahalito,     // Mage
        Molito,       // Mage
        Morlis,       // Mage
        Dalto,        // Mage
        Lahalito,     // Mage
        Mamorlis,     // Mage
        Makanito,     // Mage
        Madalto,      // Mage
        Lakanito,     // Mage
        Zilwan,       // Mage
        Masopic,      // Mage
        Haman,        // Mage
        Malor,        // Mage
        Mahaman,      // Mage
        Tiltowait,    // Mage
        Kalki,        // Priest
        Dios,         // Priest
        Badios,       // Priest
        Milwa,        // Priest
        Porfic,       // Priest
        Matu,         // Priest
        Calfo,        // Priest
        Manifo,       // Priest
        Montino,      // Priest
        Lomilwa,      // Priest
        Dialko,       // Priest
        Latumapic,    // Priest
        Bamatu,       // Priest
        Dial,         // Priest
        Badial,       // Priest
        Latumofis,    // Priest
        Maporfic,     // Priest
        Dialma,       // Priest
        Badialma,     // Priest
        Litokan,      // Priest
        Kandi,        // Priest
        Di,           // Priest
        Badi,         // Priest
        Lorto,        // Priest
        Madi,         // Priest
        Mabadi,       // Priest
        Loktofeit,    // Priest
        Malikto,      // Priest
        Kadorto,      // Priest
        Last
    }

    public class Wiz1SpellList
    {
        List<Wiz1Spell> m_spells;

        public static Wiz1SpellIndex[] Mage =
        {
	        Wiz1SpellIndex.Halito,
	        Wiz1SpellIndex.Mogref,
	        Wiz1SpellIndex.Katino,
	        Wiz1SpellIndex.Dumapic,
	        Wiz1SpellIndex.Dilto,
	        Wiz1SpellIndex.Sopic,
	        Wiz1SpellIndex.Mahalito,
	        Wiz1SpellIndex.Molito,
	        Wiz1SpellIndex.Morlis,
	        Wiz1SpellIndex.Dalto,
	        Wiz1SpellIndex.Lahalito,
	        Wiz1SpellIndex.Mamorlis,
	        Wiz1SpellIndex.Makanito,
	        Wiz1SpellIndex.Madalto,
	        Wiz1SpellIndex.Lakanito,
	        Wiz1SpellIndex.Zilwan,
	        Wiz1SpellIndex.Masopic,
	        Wiz1SpellIndex.Haman,
	        Wiz1SpellIndex.Malor,
	        Wiz1SpellIndex.Mahaman,
	        Wiz1SpellIndex.Tiltowait
        };

        public static Wiz1SpellIndex[] Priest =
        {
	        Wiz1SpellIndex.Kalki,
	        Wiz1SpellIndex.Dios,
	        Wiz1SpellIndex.Badios,
	        Wiz1SpellIndex.Milwa,
	        Wiz1SpellIndex.Porfic,
	        Wiz1SpellIndex.Matu,
	        Wiz1SpellIndex.Calfo,
	        Wiz1SpellIndex.Manifo,
	        Wiz1SpellIndex.Montino,
	        Wiz1SpellIndex.Lomilwa,
	        Wiz1SpellIndex.Dialko,
	        Wiz1SpellIndex.Latumapic,
	        Wiz1SpellIndex.Bamatu,
	        Wiz1SpellIndex.Dial,
	        Wiz1SpellIndex.Badial,
	        Wiz1SpellIndex.Latumofis,
	        Wiz1SpellIndex.Maporfic,
	        Wiz1SpellIndex.Dialma,
	        Wiz1SpellIndex.Badialma,
	        Wiz1SpellIndex.Litokan,
	        Wiz1SpellIndex.Kandi,
	        Wiz1SpellIndex.Di,
	        Wiz1SpellIndex.Badi,
	        Wiz1SpellIndex.Lorto,
	        Wiz1SpellIndex.Madi,
	        Wiz1SpellIndex.Mabadi,
	        Wiz1SpellIndex.Loktofeit,
	        Wiz1SpellIndex.Malikto,
	        Wiz1SpellIndex.Kadorto
        };

        public List<Wiz1Spell> Spells
        {
            get { return m_spells; }
        }

        public Wiz1SpellList()
        {
            m_spells = new List<Wiz1Spell>(50);
            m_spells.Add(Wiz1Spell.None);
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Halito, "Halito", "Hal", "Little Fire", SpellType.Mage, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster, "1-8 fire damage", "Causes a flame ball about the size of a baseball to strike a monster, inflicting from one to eight hit points of damage."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Mogref, "Mogref", "Mog", "Body Iron", SpellType.Mage, 1, SpellWhen.CombatAnywhere, SpellTarget.Caster, "Reduce AC by 2", "Reduces the spell-caster's AC (Armor Class) by two points. This protection lasts for the rest of the encounter."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Katino, "Katino", "Kat", "Bad Air", SpellType.Mage, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Prevent action and reduce defense", "Causes most of the monsters in a group to fall asleep. It only affects normal animal or humanoid monsters, and the duraton of it's effect is inversely proportional to the power of the monster. Sleeping monsters are easier to hit and successful attacks do double damage!"));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Dumapic, "Dumapic", "Du", "Clarity", SpellType.Mage, 1, SpellWhen.NonCombatCamp, SpellTarget.Party, "Shows current location", "Grants you insight into your party's position in the Maze: the exact displacement from the stairs leading to the Castle (vertically, North and East), and the direction you are currently facing."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Dilto, "Dilto", "Dil", "Darkness", SpellType.Mage, 2, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Reduce defensive ability", "Causes one group of monsters to be enveloped in darkness, which reduces their ability to defend themselves."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Sopic, "Sopic", "S", "Glass", SpellType.Mage, 2, SpellWhen.CombatAnywhere, SpellTarget.Caster, "Reduce AC by 4", "Causes the spell-caster to become transparent. This makes him harder to see; thus the caster's AC is effectively reduced by four points during the rest of the encounter."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Mahalito, "Mahalito", "Mahal", "Big Fire", SpellType.Mage, 3, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "4-24 fire damage", "Causes a fiery explosion to erupt amid a monster group, doing four to twenty-four hit points of damage."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Molito, "Molito", "Mol", "Sparks", SpellType.Mage, 3, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "3-18 physical damage", "Causes sparks to fly about and cause three to eighteen points of damage to about half the monsters in a group. While inferior in many respects to Mahalito, it affects some monsters that are impervious to fire-based spells, and the monters that are struck by the spell are less likely to be able to minimize it's effects."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Morlis, "Morlis", "Mor", "Fear", SpellType.Mage, 4, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Reduce attack effectiveness", "Causes one group of monsters to fear the party, thus reducing the effectiveness of their attacks. The effects are comparable to a double-strength Dilto spell."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Dalto, "Dalto", "Da", "Blizzard", SpellType.Mage, 4, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "6-36 cold damage", "A frigid version of Mahalito, inflicting six to thirty-six points of damage."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Lahalito, "Lahalito", "Lah", "Torch", SpellType.Mage, 4, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "6-36 fire damage", "An \"industrial-strength\" version of Mahalito, inflicting six to thirty-six points of damage."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Mamorlis, "Mamorlis", "Mam", "Terror", SpellType.Mage, 5, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, "Reduce attack effectiveness", "An improved version of Morlis that makes all of the monsters in an encounter fear the party, thus reducing the effectiveness of their attacks."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Makanito, "Makanito", "Mak", "Deadly Air", SpellType.Mage, 5, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, "Kill <40 HP air-breathing monsters", "Asphyxiates most air-breathing monsters with less than forty hit-points.  This is an all or nothing spell; if it does not kill a monster, that monster is undamaged by the spell."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Madalto, "Madalto", "Mada", "Frost King", SpellType.Mage, 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "8-64 cold damage", "A super-cooled Dalto that causes eight to sixty-four points of icy damage."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Lakanito, "Lakanito", "Lak", "Vacuum", SpellType.Mage, 6, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Kill air-breathing monsters", "Kills all monsters in a group if they breath air."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Zilwan, "Zilwan", "Z", "Dispell", SpellType.Mage, 6, SpellWhen.CombatAnywhere, SpellTarget.Monster, "Remove an undead monster from combat", "Dispell one monster of \"Undead\" variety."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Masopic, "Masopic", "Mas", "Crystal", SpellType.Mage, 6, SpellWhen.CombatAnywhere, SpellTarget.Party, "Reduce AC by 4", "Duplicates the \"transparency\" effects of Sopic, but affects the entire party."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Haman, "Haman", "Ham", "Beg", SpellType.Mage, 6, SpellWhen.CombatAnywhere, SpellTarget.Variable, "Cure party or reduce magic resistance", "Allows the caster to beg the Gods for aid. Only thirteenth-level or higher characters may cast it, and doing so costs them a level of experience! If the Gods decide to answer your plea, you will be given a choice of possible boons."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Malor, "Malor", "Malo", "Teleport", SpellType.Mage, 7, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Move party to another location", "When cast in combat, randomly teleports the party to another location on the same level. When cast in camp, the caster can select his destination precisely.  Teleporting outside the Maze, or into an area of solid rock, will have catastrophic results."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Mahaman, "Mahaman", "Maham", "Beseech", SpellType.Mage, 7, SpellWhen.CombatAnywhere, SpellTarget.Variable, "Cure party, silence or kill monsters", "A more powerful version of Haman has the same costs and conditions of casting, but the boons that the Gods grant are more valuable."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Tiltowait, "Tiltowait", "T", "Ka-Blam!", SpellType.Mage, 7, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, "10-100 physical damage", "The effect of this spell is somewhat like the detonation of a small tactical nuclear weapon, and causes from ten to a hundred hit points of damage to all the monsters opposing the party!"));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Kalki, "Kalki", "Kal", "Blessings", SpellType.Priest, 1, SpellWhen.CombatAnywhere, SpellTarget.Party, "Reduce AC by 1", "Reduces the AC (Armor Class) of all party members by one point, and thus makes them harder to hit."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Dios, "Dios", "Dio", "Heal", SpellType.Priest, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Heal 1-8 HP", "Restores from one to eight lost hit points to a party member. It will not bring the dead back to life."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Badios, "Badios", "Badio", "Harm", SpellType.Priest, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster, "1-8 physical damage", "Inflicts from one to eight hit points of damage upon a monster. It is the inverse of Dios."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Milwa, "Milwa", "Mi", "Light", SpellType.Priest, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Increase visibility and reveal secret doors", "Causes a softly glowing magical light to accompany the party, illuminating more of the Maze and revealing all secret doors. The light lasts only a short time."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Porfic, "Porfic", "P", "Shield", SpellType.Priest, 1, SpellWhen.CombatAnywhere, SpellTarget.Caster, "Reduce AC by 4", "Lowers the AC of the caster by 4 points. The effects last for the rest of the combat."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Matu, "Matu", "Mat", "Zeal", SpellType.Priest, 2, SpellWhen.CombatAnywhere, SpellTarget.Party, "Reduce AC by 2", "Reduces the AC (Armor Class) of all party members by two points, and thus is a double strength Kalki."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Calfo, "Calfo", "C", "X-Ray", SpellType.Priest, 2, SpellWhen.NonCombatLooting, SpellTarget.Caster, "95% chance to reveal correct trap type", "Permits the caster to determine the nature of a trap on a chest with excellent reliability."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Manifo, "Manifo", "Man", "Statue", SpellType.Priest, 2, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Prevent action and reduce defense", "Causes some of the monsters in a group to become still as statues for one or more melee rounds. The practical effects are similar to Katino; the monsters cannot attack, and physical attacks upon them are easier and do double damage."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Montino, "Montino", "Mon", "Still Air", SpellType.Priest, 2, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Prevent spellcasting", "Causes the air around a group of monsters to stop transmitting sound, thus preventing them from casting spells!"));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Lomilwa, "Lomilwa", "Lom", "Sunbeam", SpellType.Priest, 3, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Increase visibility and reveal secret doors", "Causes a softly glowing magical light to accompany the party, illuminating more of the Maze and revealing all secret doors. The effects last for the duration of the expedition."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Dialko, "Dialko", "Dialk", "Softness", SpellType.Priest, 3, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Remove \"sleep\" and \"paralyzed\" conditions", "Cures paralysis, and frees those under the spell of Katino or Manifo."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Latumapic, "Latumapic", "Latuma", "Identify", SpellType.Priest, 3, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Reveal monster names", "Reveals the true names of all the monsters you meet. The effects last for the rest of the expedition."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Bamatu, "Bamatu", "Bam", "Prayer", SpellType.Priest, 3, SpellWhen.CombatAnywhere, SpellTarget.Party, "Reduce AC by 4", "A double-strength Matu spell that reduces the AC of each party member by four points for the duration of the combat."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Dial, "Dial", "Dial", "Cure", SpellType.Priest, 4, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Heal 2-16 HP", "An improved Dios spell that restores two to sixteen hit points to a party member."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Badial, "Badial", "Badial", "Wound", SpellType.Priest, 4, SpellWhen.CombatAnywhere, SpellTarget.Monster, "2-16 physical damage", "The inverse of Dial, inflicting two to sixteen hit points of damage upon a monster."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Latumofis, "Latumofis", "Latumo", "Cleanse", SpellType.Priest, 4, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Remove \"poisoned\" condition", "Removes the effects of poison."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Maporfic, "Maporfic", "Map", "Big Shield", SpellType.Priest, 4, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Reduce AC by 2", "An improved version of Porfic that lasts for the duration of the expedition. This is the best overall defensive spell."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Dialma, "Dialma", "Dialm", "Big Cure", SpellType.Priest, 5, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Heal 3-24 HP", "An improved Dial spell that restores three to twenty-four hit points to a party member."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Badialma, "Badialma", "Badialm", "Big Wound", SpellType.Priest, 5, SpellWhen.CombatAnywhere, SpellTarget.Monster, "3-24 physical damage", "An improved Badial spell that inflicts three to twenty-four hit points of damage upon a monster."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Litokan, "Litokan", "Li", "Flames", SpellType.Priest, 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "3-24 fire damage", "Causes a pillar of flame to strike a group of monsters, doing three to twenty-four points of damage to each."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Kandi, "Kandi", "Kan", "Location", SpellType.Priest, 5, SpellWhen.NonCombatCamp, SpellTarget.Caster, "Reveal location of \"out\" character", "Allows the caster to locate the approximate position in the Maze of another character."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Di, "Di", "Di", "Life", SpellType.Priest, 5, SpellWhen.NonCombatCamp, SpellTarget.Character, "Remove \"dead\" condition", "Attempts to resurrect a dead character. There is a chance that the spell will fail. If successful, the restored character will have but one hit point.  It cannot resurrect a character who is in ashes, and if it fails will turn a dead character into ashes. This spell is not as effective as the one cast by the priests of the Temple of Cant."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Badi, "Badi", "Badi", "Death", SpellType.Priest, 5, SpellWhen.CombatAnywhere, SpellTarget.Monster, "Kill a monster with a heart", "Attempts to give the target a heart-attack. If successful (and the target must have a heart for this to be so!) the monster dies."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Lorto, "Lorto", "Lor", "Blades", SpellType.Priest, 6, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "6-36 physical damage", "Causes sharp blades to slice through a group, causing six to thirty-six points of damage to each monster in that group."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Madi, "Madi", "Madi", "Restore", SpellType.Priest, 6, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Heal all HP and remove minor conditions", "Totally restores the recipient to perfect health, so long as he or she is not dead or worse. It is important to recognize that in the world of Wizardry, there are things that are worse than death."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Mabadi, "Mabadi", "Mab", "Maiming", SpellType.Priest, 6, SpellWhen.CombatAnywhere, SpellTarget.Monster, "Reduce monster HP to critical level", "Strips the target monster of all but a few of its hit points"));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Loktofeit, "Loktofeit", "Lok", "Recall", SpellType.Priest, 6, SpellWhen.CombatAnywhere, SpellTarget.Party, "Move party to castle without equipment", "Causes all party members to be teleported back to the Castle, minus all their equipment and most of their gold. There is a very good chance this spell will fizzle."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Malikto, "Malikto", "Mali", "Wrath", SpellType.Priest, 7, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, "12-72 fire damage", "Causes fiery monsters to descend upon all the monsters, inflicting from twelve to seventy-two points of damage upon each."));
            m_spells.Add(new Wiz1Spell(Wiz1SpellIndex.Kadorto, "Kadorto", "Kad", "Rebirth", SpellType.Priest, 7, SpellWhen.NonCombatCamp, SpellTarget.Character, "Remove \"ashes\" condition", "Restores the dead to life, even those reduced to ashes. It also restores all of the recipient's hit points. As with Di, there is a chance that it will fail. If a character who is in ashes fails to be resurrected, he or she will be lost forever."));
        }

    }

    public class Wiz1Spell : WizardrySpell
    {
        public Wiz1SpellIndex Index;
        public override int BasicIndex { get { return (int)Index; } }
        public override bool UsesLevelOnly { get { return true; } }

        public Wiz1Spell(Wiz1SpellIndex index, string name, string abbrev, string trans, SpellType type, int level, SpellWhen when, SpellTarget target, string shortDesc, string desc)
        {
            Index = index;
            Name = name;
            Abbreviation = abbrev;
            Translation = trans;
            Type = type;
            Level = level;
            When = when;
            Target = target;
            ShortDescription = shortDesc;
            Description = desc;
            Cost = new WizardrySpellCost(level);
            int iMageReq = level * 2 - 1;
            int iPriestReq = level * 2 - 1;
            int iSamuraiReq = level * 3 + 1;
            int iLordReq = level * 3 + 1;
            int iBishopMageReq = level * 4 - 3;
            int iBishopPriestReq = level * 4;

            switch (index)
            {
                case Wiz1SpellIndex.Halito:
                case Wiz1SpellIndex.Katino:
                    Learned = "Automatic for a level 1 Mage/Bishop during character creation, or a 3.33% chance to learn per point of I.Q. for a level 4 Samurai (or any character that already knows another level 1 Mage spell) during level-up.";
                    break;
                case Wiz1SpellIndex.Dios:
                case Wiz1SpellIndex.Badios:
                    Learned = "Automatic for a level 1 Priest during character creation, or a 3.33% chance to learn per point of Piety for a level 4 Bishop/Lord (or any character that already knows another level 1 Priest spell)  during level-up";
                    break;
                case Wiz1SpellIndex.None:
                case Wiz1SpellIndex.Last:
                    Learned = "N/A";
                    break;
                default:
                    if (Type == SpellType.Mage)
                    {
                        Learned = String.Format("3.33% chance to learn per point of I.Q. during level-up for a level {0} Mage, level {1} Bishop, level {2} Samurai, or any character that already knows another level {3} Mage spell.",
                            iMageReq, iBishopMageReq, iSamuraiReq, level);
                    }
                    else
                    {
                        Learned = String.Format("3.33% chance to learn per point of Piety during level-up for a level {0} Priest, level {1} Bishop, level {2} Lord, or any character that already knows another level {3} Priest spell.",
                            iPriestReq, iBishopPriestReq, iLordReq, level);
                    }
                    break;
            }
        }

        public override Keys[] GetKeys()
        {
            Keys[] keys = new Keys[Name.Length + 1];
            for (int i = 0; i < Name.Length; i++)
                keys[i] = Keys.A + (Char.ToUpper(Name[i]) - 'A');
            keys[Name.Length] = Keys.Enter;
            return keys;
        }

        public static Wiz1Spell None
        {
            get { return new Wiz1Spell(Wiz1SpellIndex.None, "None", "", "None", SpellType.Unknown, 0, SpellWhen.None, SpellTarget.Unknown, "", ""); }
        }
    }
}
