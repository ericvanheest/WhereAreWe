using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public enum Wiz5SpellIndex
    {
        None = 0,
        First = 1,
        Katino = 1,     // Mage    (1) 
        Halito,         // Mage    (2) 
        Dumapic,        // Mage    (3) 
        Mogref,         // Mage    (4) 
        Ponti,          // Mage    (5) 
        Melito,         // Mage    (6) 
        Desto,          // Mage    (7) 
        Morlis,         // Mage    (8) 
        Bolatu,         // Mage    (9) 
        Calific,        // Mage    (10)
        Mahalito,       // Mage    (11)
        Cortu,          // Mage    (12)
        Kantios,        // Mage    (13)
        Tzalik,         // Mage    (14)
        Lahalito,       // Mage    (15)
        Litofeit,       // Mage    (16)
        Rokdo,          // Mage    (17)
        Socordi,        // Mage    (18)
        Madalto,        // Mage    (19)
        Palios,         // Mage    (20)
        Vaskyre,        // Mage    (21)
        Bacortu,        // Mage    (22)
        Mamogref,       // Mage    (23)
        Zilwan,         // Mage    (24)
        Lokara,         // Mage    (25)
        Ladalto,        // Mage    (26)
        Malor,          // Mage    (27)
        Mahaman,        // Mage    (28)
        Tiltowait,      // Mage    (29)
        Abriel,         // Mage    (30)
        Mawxiwtz,       // Mage    (31)
        Dios,           // Priest  (32)
        Badios,         // Priest  (33)
        Milwa,          // Priest  (34)
        Kalki,          // Priest  (35)
        Porfic,         // Priest  (36)
        Katu,           // Priest  (37)
        Calfo,          // Priest  (38)
        Montino,        // Priest  (39)
        Kandi,          // Priest  (40)
        Latumapic,      // Priest  (41)
        Dialko,         // Priest  (42)
        Bamatu,         // Priest  (43)
        Lomilwa,        // Priest  (44)
        Hakanido,       // Priest  (45)
        Dial,           // Priest  (46)
        Badial,         // Priest  (47)
        Latumofis,      // Priest  (48)
        Maporfic,       // Priest  (49)
        Bariko,         // Priest  (50)
        Dialma,         // Priest  (51)
        Badi,           // Priest  (52)
        Di,             // Priest  (53)
        Bamordi,        // Priest  (54)
        Mogato,         // Priest  (55)
        Loktofeit,      // Priest  (56)
        Madi,           // Priest  (57)
        Labadi,         // Priest  (58)
        Kakamen,        // Priest  (59)
        Mabariko,       // Priest  (60)
        Kadorto,        // Priest  (61)
        Ihalon,         // Priest  (62)
        Bakadi,         // Priest  (63)
        Last
    }

    public class Wiz5SpellList
    {
        List<Wiz5Spell> m_spells;

        public static Wiz5SpellIndex[] Mage =
        {
            Wiz5SpellIndex.Katino,
            Wiz5SpellIndex.Halito,
            Wiz5SpellIndex.Dumapic,
            Wiz5SpellIndex.Mogref,
            Wiz5SpellIndex.Ponti,
            Wiz5SpellIndex.Melito,
            Wiz5SpellIndex.Desto,
            Wiz5SpellIndex.Morlis,
            Wiz5SpellIndex.Bolatu,
            Wiz5SpellIndex.Calific,
            Wiz5SpellIndex.Mahalito,
            Wiz5SpellIndex.Cortu,
            Wiz5SpellIndex.Kantios,
            Wiz5SpellIndex.Tzalik,
            Wiz5SpellIndex.Lahalito,
            Wiz5SpellIndex.Litofeit,
            Wiz5SpellIndex.Rokdo,
            Wiz5SpellIndex.Socordi,
            Wiz5SpellIndex.Madalto,
            Wiz5SpellIndex.Palios,
            Wiz5SpellIndex.Vaskyre,
            Wiz5SpellIndex.Bacortu,
            Wiz5SpellIndex.Mamogref,
            Wiz5SpellIndex.Zilwan,
            Wiz5SpellIndex.Lokara,
            Wiz5SpellIndex.Ladalto,
            Wiz5SpellIndex.Malor,
            Wiz5SpellIndex.Mahaman,
            Wiz5SpellIndex.Tiltowait,
            Wiz5SpellIndex.Abriel,
            Wiz5SpellIndex.Mawxiwtz
        };

        public static Wiz5SpellIndex[] Priest =
        {
            Wiz5SpellIndex.Dios,
            Wiz5SpellIndex.Badios,
            Wiz5SpellIndex.Milwa,
            Wiz5SpellIndex.Kalki,
            Wiz5SpellIndex.Porfic,
            Wiz5SpellIndex.Katu,
            Wiz5SpellIndex.Calfo,
            Wiz5SpellIndex.Montino,
            Wiz5SpellIndex.Kandi,
            Wiz5SpellIndex.Latumapic,
            Wiz5SpellIndex.Dialko,
            Wiz5SpellIndex.Bamatu,
            Wiz5SpellIndex.Lomilwa,
            Wiz5SpellIndex.Hakanido,
            Wiz5SpellIndex.Dial,
            Wiz5SpellIndex.Badial,
            Wiz5SpellIndex.Latumofis,
            Wiz5SpellIndex.Maporfic,
            Wiz5SpellIndex.Bariko,
            Wiz5SpellIndex.Dialma,
            Wiz5SpellIndex.Badi,
            Wiz5SpellIndex.Di,
            Wiz5SpellIndex.Bamordi,
            Wiz5SpellIndex.Mogato,
            Wiz5SpellIndex.Loktofeit,
            Wiz5SpellIndex.Madi,
            Wiz5SpellIndex.Labadi,
            Wiz5SpellIndex.Kakamen,
            Wiz5SpellIndex.Mabariko,
            Wiz5SpellIndex.Kadorto,
            Wiz5SpellIndex.Ihalon,
            Wiz5SpellIndex.Bakadi
        };

        public List<Wiz5Spell> Spells
        {
            get { return m_spells; }
        }

        public Wiz5SpellList()
        {
            m_spells = new List<Wiz5Spell>(63);
            m_spells.Add(Wiz5Spell.None);
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Katino, "Katino", "Kati", "Bad Air", SpellType.Mage, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Cause \"Asleep\" condition (2x damage)", "Katino causes most of the monsters in a group to fall asleep.  Katino only affects normal animal or humanoid monsters, and the duration of its effect is inversely proportional to the power of the monster.  Sleeping monsters are easier to hit and successful attacks do double damage!"));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Halito, "Halito", "Hal", "Little Fire", SpellType.Mage, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster, "1d8 (1-8) fire damage", "Halito causes a flame ball about the size of a baseball to strike a monster, inflicting from 1 to 8 hit points of damage."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Dumapic, "Dumapic", "Du", "Clarity", SpellType.Mage, 1, SpellWhen.NonCombatCamp, SpellTarget.Party, "Shows current location", "Dumapic grants you insight into your party's position in the Maze:  the exact vertical displacement from the stairs leading to the Castle (for example,  North and East or West and South), and the direction you are currently facing."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Mogref, "Mogref", "Mogr", "Body Iron", SpellType.Mage, 1, SpellWhen.CombatAnywhere, SpellTarget.Caster, "Reduce AC by 2", "Mogref reduces the spell-caster's AC (Armor Class) by two points.  This protection lasts for the rest of the encounter."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Ponti, "Ponti", "Pon", "Speed", SpellType.Mage, 2, SpellWhen.CombatAnywhere, SpellTarget.Character, "Reduce AC by 1, Swings +1", "Ponti increases the speed of the party member so that he may strike more times per round of combat.  This indirectly increases the chances to hit a monster.  It also reduces the Armor Class (AC) of the recipient by one."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Melito, "Melito", "Me", "Little Sparks", SpellType.Mage, 2, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "1d8 (1-8) physical damage", "Melito sprays one monster group with sparks and does 1 to 8 hit points of damage on each affected monster."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Desto, "Desto", "De", "Unlock", SpellType.Mage, 2, SpellWhen.NonCombatCamp, SpellTarget.Caster, "Unlock a door", "Desto attempts to unlock a door as if the caster were a Thief of the same experience level.  This may be cast as often as necessary until either the door unlocks, or you run out of patience (or spells)."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Morlis, "Morlis", "Mor", "Fear", SpellType.Mage, 2, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Cause \"Afraid\" condition, AC +2", "Morlis makes Monsters to fear the party, causing them to flee and/or cower.  Afraid monsters may not be able to strike against the party, and sometimes they are not able to execute their desired action.  The monsters' AC is also raised."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Bolatu, "Bolatu", "Bo", "Heart of Stone", SpellType.Mage, 2, SpellWhen.CombatAnywhere, SpellTarget.Monster, "Cause \"Petrified\" condition", "Bolatu attempts to solidify one monster by turning it to stone."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Calific, "Calific", "Cali", "Reveal", SpellType.Mage, 3, SpellWhen.NonCombatCamp, SpellTarget.Caster, "Reveal secret door", "Calific will always reveal a secret door if one is present on the wall the party is facing."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Mahalito, "Mahalito", "Mahal", "Big Fire", SpellType.Mage, 3, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "4d6 (4-24) fire damage", "Mahalito causes a fiery explosion to erupt amid a monster group, doing 4 to 24 hit points of damage."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Cortu, "Cortu", "Co", "Magic Screen", SpellType.Mage, 3, SpellWhen.CombatAnywhere, SpellTarget.Party, "Add level*2 to Magic Screen (max 90)", "Cortu erects a magic screen relative to the level of the caster to prevent magic spells from affecting the party.  Each successive casting adds to the barrier.  It even helps protect against \"breathing\" monsters."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Kantios, "Kantios", "Kant", "Disruption", SpellType.Mage, 3, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Disrupt enemy actions", "Kantios attempts to disrupt one monster group.  The spell interferes with any action requiring some mental thought by the monsters (casting spells, breath, calling for help).  Any monster or person affected may not be able to execute some options otherwise normally available."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Tzalik, "Tzalik", "Tz", "The Fist of God", SpellType.Mage, 4, SpellWhen.CombatAnywhere, SpellTarget.Monster, "4d10+20 (24-60) physical damage", "Tzalik invokes a powerful heavenly force and does 24-58 hit points of damage on one monster."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Lahalito, "Lahalito", "Lah", "Torch", SpellType.Mage, 4, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "6d6 (6-36) fire damage", "Lahalito is an \"industrial-strength\" version of  Mahalito, and inflicts 6-36 hit points of damage."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Litofeit, "Litofeit", "Li", "Levitate", SpellType.Mage, 4, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Adds 1000 steps of levitation", "Litofeit levitates the party several inches above the ground, thus preventing them from doing stupid things like falling into pits or tripping over traps.  Since \"walking on air\" causes the party to move quietly, this spell greatly reduces the chance of the party being surprised."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Rokdo, "Rokdo", "R", "Stun", SpellType.Mage, 4, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Cause \"Petrified\" condition", "Rokdo attempts to stun one monster group.  It is like Katino except that it petrifies the monsters, making it much harder for them to recover."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Socordi, "Socordi", "S", "Conjuring", SpellType.Mage, 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Summon monster group", "Socordi conjures a group of monsters from one of the elemental planes to come and fight for the party."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Madalto, "Madalto", "Mada", "Frost King", SpellType.Mage, 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "8d8 (8-64) cold damage", "Madalto brings down a great blizzard on the monsters that causes 8-64 hit points of snowy, icy damage."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Palios, "Palios", "Pa", "Anti-Magic", SpellType.Mage, 5, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, "-66% enemy magic screen, dispel fizzle fields", "Palios greatly reduces magic screens erected by the monsters and dispels monster-caused fizzle fields around the party."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Vaskyre, "Vaskyre", "V", "Rainbow Rays", SpellType.Mage, 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Random effect", "The effects of Vaskyre's penetrating rays are random, but they are generally quite devastating."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Bacortu, "Bacortu", "Bac", "Fizzle Field", SpellType.Mage, 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Add character level to fizzle field strength", "Bacortu creates a fizzle field around one monster group.  Unlike the Priest Spell, Montino, the field around the monster cannot be resisted.  It can be a highly effective way of preventing monsters from burning the party with magic.  The strength of this spell is relative to the experience level of the caster."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Mamogref, "Mamogref", "Mam", "Wall of Force", SpellType.Mage, 6, SpellWhen.CombatAnywhere, SpellTarget.Character, "Create AC -10 wall", "Mamogref creates a virtually impregnable wall of force of  AC -10 around one party member."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Zilwan, "Zilwan", "Z", "Dispel", SpellType.Mage, 6, SpellWhen.CombatAnywhere, SpellTarget.Monster, "3d500 (500-1500) physical damage to undead", "Zilwan will dispel one monster of the \"Undead\" variety, causing 500 - 1500 points damage."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Lokara, "Lokara", "Loka", "Earth Feast", SpellType.Mage, 6, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, "Remove monsters from combat", "Lokara attempts to have the earth around the monsters open up and swallow them, but it does not affect some monster types."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Ladalto, "Ladalto", "Lad", "Ice Storm", SpellType.Mage, 6, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "8d8+26 (34-90) cold damage", "Ladalto is a super high-powered Madalto, and does 34-90 damage to one monster group."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Malor, "Malor", "Mal", "Teleport", SpellType.Mage, 7, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Move party to another location", "When cast in Combat, Malor randomly teleports the party to another location on the same level.  When cast in Camp, the caster can select the destination precisely.  Teleporting outside the Maze, or into an area of solid rock, will have catastrophic results."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Mahaman, "Mahaman", "Maham", "Beseech", SpellType.Mage, 7, SpellWhen.CombatAnywhere, SpellTarget.Variable, "Grant a wish", "A Call upon the Gods for favors.  This spell cannot be cast except by a Level 13 character or greater and the caster is drained 1 level of experience if successfully cast.  However, the wish granted is by the choice of the caster, and often the benefits far outweigh the price."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Tiltowait, "Tiltowait", "Ti", "Ka-Blam!", SpellType.Mage, 7, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, "10d15 (15-150) physical damage", "The effect of this spell is somewhat like the detonation of a small, tactical nuclear weapon, and causes from 10-150 hit points of damage to all the monsters opposing the party!"));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Abriel, "Abriel", "A", "Divine Magic", SpellType.Mage, 7, SpellWhen.CombatAnywhere, SpellTarget.Unknown, "Unknown effect", "No one we know has ever learned this spell, hence its exact effect is unknown.  Rumor has it that this is a spell often employed by the Gods when they want to battle other Gods that they despise."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Mawxiwtz, "Mawxiwtz", "Maw", "Madhouse", SpellType.Mage, 7, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, "Random effects", "Mawxiwtz is a super-charged Vaskyre, causing utter havoc and pandemonium in the monster ranks."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Dios, "Dios", "Dio", "Heal", SpellType.Priest, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Heal 1d8 (1-8) HP", "Dios restores from 1 to 8 lost hit points to a party member.  It will not bring the dead back to life."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Badios, "Badios", "Badio", "Harm", SpellType.Priest, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster, "1d8 (1-8) physical damage", "Badios inflicts from 1 to 8 hit points of damage upon a monster.  It is the inverse of Dios."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Milwa, "Milwa", "Mi", "Light", SpellType.Priest, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Adds 35-70 steps of light", "Milwa causes a softly glowing magical light to accompany the party, illuminating more of the Maze.  The light lasts only a short time."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Kalki, "Kalki", "Kal", "Blessings", SpellType.Priest, 1, SpellWhen.CombatAnywhere, SpellTarget.Party, "Reduce AC by 1", "Kalki reduces the AC (Armor Class) of all party members by one point, and thus makes them harder to hit."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Porfic, "Porfic", "Por", "Shield", SpellType.Priest, 1, SpellWhen.CombatAnywhere, SpellTarget.Caster, "Reduce AC by 4", "Porfic lowers the AC of the caster by 4 points.  The effect lasts for the rest of the combat."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Katu, "Katu", "Katu", "Charm", SpellType.Priest, 2, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Charm target", "When Katu is cast in combat, it attempts to charm the monsters, thus preventing them from attacking the party.  Any monster charmed will likewise be easier to hit.  When cast in non-combat situations (interactive encounters), the spell attempts to charm the Non-Player Character (NPC) so that it regards the party in a friendly manner."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Calfo, "Calfo", "Calf", "X-Ray", SpellType.Priest, 2, SpellWhen.NonCombatLooting, SpellTarget.Caster, "Reveal trap type", "Calfo permits the caster to determine the nature of a trap on a chest with excellent reliability."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Montino, "Montino", "Mon", "Still Air", SpellType.Priest, 2, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Cause \"Silenced\" condition", "Montino causes the air around a group of monsters to stop transmitting sound, thus preventing them from casting spells!"));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Kandi, "Kandi", "Kand", "Locate Dead Soul or Body", SpellType.Priest, 2, SpellWhen.NonCombatCamp, SpellTarget.Caster, "Reveal location of missing person", "Kandi gives direction of the person the party is attempting to locate and retrieve relative to the position of the caster."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Latumapic, "Latumapic", "Latuma", "Identify", SpellType.Priest, 3, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Reveal monster names", "Latumapic reveals the true names of all the monsters you meet.  The effects of this spell are long-lasting."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Dialko, "Dialko", "Dialk", "Softness", SpellType.Priest, 3, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Remove \"Asleep\" and \"Paralyzed\" conditions", "Dialko cures paralysis, and wakes up someone who is asleep."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Bamatu, "Bamatu", "Bama", "Prayer", SpellType.Priest, 3, SpellWhen.CombatAnywhere, SpellTarget.Party, "Reduce AC by 3", "Bamatu is a triple-strength Kalki spell.  It reduces the AC of each party member by three points for the duration of the combat."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Lomilwa, "Lomilwa", "Lom", "Sunbeam", SpellType.Priest, 3, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Gives 2500 steps of light", "Like Milwa, Lomilwa causes a softly glowing magical light to accompany the party, illuminating more of the Maze.  The effects of  Lomilwa, however, last much longer."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Hakanido, "Hakanido", "Hak", "Magic Drain", SpellType.Priest, 3, SpellWhen.CombatAnywhere, SpellTarget.Monster, "-1d3 enemy spell level", "Hakanido attempts to drain the monster of high level magic power, thus reducing the level of spells that it is able to cast."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Dial, "Dial", "Dial", "Cure", SpellType.Priest, 4, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Heal 2d8 (2-16) HP", "Dial is an improved Dios spell.  It restores 2-16 hit points to a party member."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Badial, "Badial", "Badia", "Wound", SpellType.Priest, 4, SpellWhen.CombatAnywhere, SpellTarget.Monster, "2d8 (2-16) physical damage", "Badial is the inverse of  Dial.  It inflicts 3-32 hit points of damage upon a monster."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Latumofis, "Latumofis", "Latumo", "Cleanse", SpellType.Priest, 4, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Remove \"Poisoned\" condition", "Latumofis removes the effects of poison."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Maporfic, "Maporfic", "Map", "Big Shield", SpellType.Priest, 4, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Reduce AC by 2", "Maporfic is an improved longer-lasting version of  Porfic.  This is the best, overall defensive spell."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Bariko, "Bariko", "Bar", "Razor Wind", SpellType.Priest, 4, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "3d5 (5-15) physical damage", "Bariko sends blades through a single monster group and causes 6-15 hit points of damage."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Dialma, "Dialma", "Dialm", "Big Cure", SpellType.Priest, 5, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Heal 3d8 (3-24) HP", "Dialma is an improved Dial spell.  It restores 3-24 hit points to a party member."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Badi, "Badi", "Badi", "Death", SpellType.Priest, 5, SpellWhen.CombatAnywhere, SpellTarget.Monster, "Kill a monster", "Badi attempts to give the target a heart-attack.  If successful (and the target must have a heart for this to be so) the monster dies!"));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Di, "Di", "Di", "Life", SpellType.Priest, 5, SpellWhen.NonCombatCamp, SpellTarget.Character, "Remove \"Dead\" condition, chance of -1 Vitality", "Di attempts to resurrect a dead character.  If successful, the restored character will have but one hit point.  If the spell fails, the dead character will dwindle to ashes! Unfortunately, Di cannot resurrect a character who is in such an ashen mess.  This spell is not as effective as the one cast by the Priests of the Temple of  Cant."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Bamordi, "Bamordi", "Bamo", "Summoning", SpellType.Priest, 5, SpellWhen.CombatAnywhere, SpellTarget.Party, "Summon monster group", "Bamordi attempts to summon one group of monsters from the elemental planes to fight for the party."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Mogato, "Mogato", "Moga", "Astral Gate", SpellType.Priest, 5, SpellWhen.CombatAnywhere, SpellTarget.Monster, "Banish a demonic monster", "Mogato attempts to banish one monster of the DEMON-type variety back into the planes from which it originated."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Loktofeit, "Loktofeit", "Lokt", "Recall", SpellType.Priest, 6, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Move party to castle, forget spell", "Loktofeit causes all party members to be teleported back to the Castle with all of their equipment and gold.  One side effect -- after it is cast, the caster forgets the spell and must relearn it.  There is a chance this spell will not work."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Madi, "Madi", "Madi", "Restore", SpellType.Priest, 6, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Heal all HP, remove minor conditions", "Madi totally restores the recipient to perfect health, so long as he or she is not dead or worse.  It is important to recognize that in the world of Wizardry, there are things that are worse than death."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Labadi, "Labadi", "Lab", "Life Steal", SpellType.Priest, 6, SpellWhen.CombatAnywhere, SpellTarget.Monster, "Reduce monster HP to 1d8 (1d8) and heal caster", "Labadi drains all but 1-8 hit points from one monster, and is able to channel that energy back into the caster, healing him or her for a substantial amount of the damage drained from the monster.  Monsters casting the spell are likewise healed."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Kakamen, "Kakamen", "Kak", "Fire Wind", SpellType.Priest, 6, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "7d7 (7-49) fire damage", "Kakamen does 18-38 damage on one monster group."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Mabariko, "Mabariko", "Mab", "Meteor Winds", SpellType.Priest, 7, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, "6d12 (12-72) physical damage", "Mabariko pelts all monsters with boulders doing 18-58 damage."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Kadorto, "Kadorto", "Kad", "Rebirth", SpellType.Priest, 7, SpellWhen.NonCombatCamp, SpellTarget.Character, "Remove \"Ashes\" condition, chance of -1 Vitality", "Kadorto restores the dead to life, even those reduced to ashes!  It also restores all of the recipient's hit points.  As with Di, there is a chance that Kadorto will fail.  If a character who is in ashes fails to be resurrected by Kadorto, he or she will be lost forever"));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Ihalon, "Ihalon", "I", "Blessed Favor", SpellType.Priest, 7, SpellWhen.NonCombatCamp, SpellTarget.Character, "Increase stat; reduce age; uncurse item", "Ihalon grants a special favor (permanently raising a primary statistic, reducing age by one year, or uncursing an item) to one party member.  The spell is forgotten after being cast, and must be relearned."));
            m_spells.Add(new Wiz5Spell(Wiz5SpellIndex.Bakadi, "Bakadi", "Bak", "Death Wind", SpellType.Priest, 7, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, "Slay monsters", "Bakadi attempts to slay outright all of the monsters in a group."));
        }

        public static Wiz5Spell[] HealingSpells = new Wiz5Spell[]
        {
            Wiz5.Spells[(int) Wiz5SpellIndex.Dios],
            Wiz5.Spells[(int) Wiz5SpellIndex.Dialko],
            Wiz5.Spells[(int) Wiz5SpellIndex.Dial],
            Wiz5.Spells[(int) Wiz5SpellIndex.Latumofis],
            Wiz5.Spells[(int) Wiz5SpellIndex.Dialma],
            Wiz5.Spells[(int) Wiz5SpellIndex.Di],
            Wiz5.Spells[(int) Wiz5SpellIndex.Madi],
            Wiz5.Spells[(int) Wiz5SpellIndex.Kadorto]
        };
    }

    public class Wiz5Spell : WizardrySpell
    {
        public Wiz5SpellIndex Index;
        public override int BasicIndex { get { return (int)Index; } }
        public override bool UsesLevelOnly { get { return true; } }

        public Wiz5Spell(Wiz5SpellIndex index, string name, string abbrev, string trans, SpellType type, int level, SpellWhen when, SpellTarget target, string shortDesc, string desc)
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
                case Wiz5SpellIndex.Halito:
                case Wiz5SpellIndex.Katino:
                    Learned = "Automatic for a level 1 Mage/Bishop during character creation, or a 3.33% chance to learn per point of I.Q. for a level 4 Samurai (or any character that already knows another level 1 Mage spell) during level-up.";
                    break;
                case Wiz5SpellIndex.Dios:
                case Wiz5SpellIndex.Badios:
                    Learned = "Automatic for a level 1 Priest during character creation, or a 3.33% chance to learn per point of Piety for a level 4 Bishop/Lord (or any character that already knows another level 1 Priest spell)  during level-up";
                    break;
                case Wiz5SpellIndex.Abriel:
                    Learned = "Can only be learned by returning the \"Heart of Abriel\" to the Castle (and thus finishing the game)";
                    break;
                case Wiz5SpellIndex.None:
                case Wiz5SpellIndex.Last:
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

        public static Wiz5Spell None
        {
            get { return new Wiz5Spell(Wiz5SpellIndex.None, "None", "", "None", SpellType.Unknown, 0, SpellWhen.None, SpellTarget.Unknown, "", ""); }
        }
    }
}
