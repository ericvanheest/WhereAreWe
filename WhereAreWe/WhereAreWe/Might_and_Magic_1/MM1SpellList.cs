using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public enum MM1SpellIndex
    {
        AwakenCleric = 0,
        Bless = 1,
        Blind = 2,
        FirstAid = 3,
        LightCleric = 4,
        PowerCure = 5,
        ProtfromFear = 6,
        TurnUndead = 7,
        CureWounds = 8,
        Heroism = 9,
        Pain = 10,
        ProtectFromCold = 11,
        ProtectFromFire = 12,
        ProtectFromPoison = 13,
        Silence = 14,
        Suggestion = 15,
        CreateFood = 16,
        CureBlindness = 17,
        CureParalysis = 18,
        LastingLight = 19,
        ProduceFlame = 20,
        ProduceFrost = 21,
        RemoveQuest = 22,
        WalkonWater = 23,
        CureDisease = 24,
        NeutralizePoison = 25,
        ProtectfromAcid = 26,
        ProtectfromElec = 27,
        RestoreAlignment = 28,
        SummonLightning = 29,
        SuperHeroism = 30,
        Surface = 31,
        DeadlySwarm = 32,
        DispelMagicCleric = 33,
        Paralyze = 34,
        RemoveCondition = 35,
        RestoreEnergy = 36,
        MoonRay = 37,
        RaiseDead = 38,
        Rejuvenate = 39,
        StonetoFlesh = 40,
        TownPortal = 41,
        DivineIntervention = 42,
        HolyWord = 43,
        ProtfromElements = 44,
        Resurrection = 45,
        SunRay = 46,
        AwakenSorcerer = 47,
        DetectMagic = 48,
        EnergyBlast = 49,
        FlameArrow = 50,
        LeatherSkin = 51,
        LightSorcerer = 52,
        Location = 53,
        Sleep = 54,
        ElecricArrow = 55,
        Hypnotize = 56,
        IdentifyMonster = 57,
        Jump = 58,
        Levitate = 59,
        Power = 60,
        Quickness = 61,
        Scare = 62,
        FireBall = 63,
        Fly = 64,
        Invisibility = 65,
        LightningBolt = 66,
        MakeRoom = 67,
        Slow = 68,
        Weaken = 69,
        Web = 70,
        AcidArrow = 71,
        ColdBeam = 72,
        FeebleMind = 73,
        Freeze = 74,
        GuardDog = 75,
        PsychicProt = 76,
        Shield = 77,
        TimeDistortion = 78,
        AcidRain = 79,
        DispelMagicSorcerer = 80,
        FingerofDeath = 81,
        Shelter = 82,
        Teleport = 83,
        DancingSword = 84,
        Disintegration = 85,
        Etherealize = 86,
        ProtfromMagic = 87,
        RechargeItem = 88,
        AstralSpell = 89,
        Duplication = 90,
        MeteorShower = 91,
        PowerShield = 92,
        PrismaticLight = 93,
        None = 255,
    }

    public class MM1SpellList
    {
        List<MM1Spell> m_spells;

        public List<MM1Spell> Spells
        {
            get { return m_spells; }
        }

        public static string GetSpellNameForItem(int iIndex)
        {
            if (iIndex >= MM1.Spells.Count)
                return "Unknown";

            MM1Spell spell = MM1.Spells[iIndex];

            return String.Format("{0} ({1}{2}{3})",
                spell.Name,
                spell.Type == SpellType.Cleric ? "C" : "S",
                spell.Level,
                spell.Number
                );
        }

        public MM1SpellList()
        {
            m_spells = new List<MM1Spell>(94);
			m_spells.Add(new MM1Spell(MM1SpellIndex.AwakenCleric,"Awaken",SpellType.Cleric,1,1,1,false,0,SpellWhen.CombatAnywhere,SpellTarget.Party,"Remove asleep condition","Awakens all sleeping members of the party, instantaneously cancelling the sleep condition. May be critical if party is attacked during rest."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Bless,"Bless",SpellType.Cleric,1,2,1,false,0,SpellWhen.CombatAnywhere,SpellTarget.Party,"Increase accuracy","Increases the accuracy with which all characters fight, for the duration of combat."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Blind,"Blind",SpellType.Cleric,1,3,1,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"Reduce accuracy","Blinds the affected monster for the duration of combat. Forced to rely on other senses, the monster's chances of landing a blow are diminished."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.FirstAid,"First Aid",SpellType.Cleric,1,4,1,false,0,SpellWhen.AnywhereAnytime,SpellTarget.Character,"Heal 8 HP","Heals minor battle wounds, restoring 8 Hit Points to that character."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.LightCleric,"Light",SpellType.Cleric,1,5,1,false,0,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Create 1 light factor","Gives the party 1 light factor, which is sufficient to light up 1 dark area. Multiple light spells can be cast to accumulate multiple light factors."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.PowerCure,"Power Cure",SpellType.Cleric,1,6,1,true,1,SpellWhen.AnywhereAnytime,SpellTarget.Character,"Heal 1-10 HP/level","Restores character's health and 1-10 Hit Points/lv."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ProtfromFear,"Prot from Fear",SpellType.Cleric,1,7,1,false,0,SpellWhen.AnywhereAnytime,SpellTarget.Party,"Add 20% + 1%/level fear resistance","Increases all characters' resistance to fear and spells of intimidation. Amount of the increase depends on experience level of the caster. Spell lasts 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.TurnUndead,"Turn Undead",SpellType.Cleric,1,8,1,false,0,SpellWhen.CombatAnywhere,SpellTarget.AllUndeadMonsters,"Chance to destroy undead monsters","Destroys some or all undead monsters, depending on caster's experience level and monster's power level."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.CureWounds,"Cure Wounds",SpellType.Cleric,2,1,2,false,0,SpellWhen.AnywhereAnytime,SpellTarget.Character,"Heal 15 HP","Cures more serious wounds, restoring 15 Hit Points to the character."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Heroism,"Heroism",SpellType.Cleric,2,2,2,false,1,SpellWhen.CombatAnywhere,SpellTarget.CharacterSameAlign,"Add 6 HP and 2 levels","Bestows 6 additional Hit Points and temporarily elevates character 2 levels of experience."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Pain,"Pain",SpellType.Cleric,2,3,2,false,0,SpellWhen.CombatAnywhere,SpellTarget.NonUndeadMonster,"2-12 sleep/hold damage","Cripples monster with pain, inflicting 2-12 damage points, unless the monster is immune to pain."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ProtectFromCold,"Protect from Cold",SpellType.Cleric,2,4,2,false,0,SpellWhen.AnywhereAnytime,SpellTarget.Party,"Add 20% + 1%/level cold resistance","Increases all character's resistance to cold or freezing spells. Amount of the increase depends on the caster's experience level. Spell lasts 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ProtectFromFire,"Protect from Fire",SpellType.Cleric,2,5,2,false,0,SpellWhen.AnywhereAnytime,SpellTarget.Party,"Add 20% + 1%/level fire resistance","Increases all character's resistance to fire or heat spells. Amount of the increase depends on the caster's experience level. Spell lasts 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ProtectFromPoison,"Protect from Poison",SpellType.Cleric,2,6,2,false,0,SpellWhen.AnywhereAnytime,SpellTarget.Party,"Add 20% + 1%/level poison resistance","Increases all characters' resistance to poison and poisonous spells. Amount of the increase depends on the caster's experience level. Spell lasts 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Silence,"Silence",SpellType.Cleric,2,7,2,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"Prevent casting of spells","Prevents the monster from casting spells for the duration of combat."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Suggestion,"Suggestion",SpellType.Cleric,2,8,2,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"Cause monster to not attack","Coerces monster into refraining from attack, unless it is attacked. Lasts for the duration of combat"));
			m_spells.Add(new MM1Spell(MM1SpellIndex.CreateFood,"Create Food",SpellType.Cleric,3,1,3,false,1,SpellWhen.NonCombatAnywhere,SpellTarget.Caster,"Add 6 food","Adds 6 food units to caster's food supply. Caster may then distribute food among other party members, if he/she desires."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.CureBlindness,"Cure Blindness",SpellType.Cleric,3,2,3,false,0,SpellWhen.AnywhereAnytime,SpellTarget.Character,"Remove blind condition","Restores sight to that character, instantaneously removing the blinded condition."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.CureParalysis,"Cure Paralysis",SpellType.Cleric,3,3,3,false,0,SpellWhen.AnywhereAnytime,SpellTarget.Character,"Remove paralyzed condition","Restores movement to that character, instantaneously removing the paralyzed condition."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.LastingLight,"Lasting Light",SpellType.Cleric,3,4,3,false,0,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Create 20 light factors","Bestows 20 light factors on the party, for use in dispelling darkness."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ProduceFlame,"Produce Flame",SpellType.Cleric,3,5,3,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"3-18 fire damage","Attacks monster with a jet of flame that inflicts 3-18 damage points, providing monster is not immune to fire."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ProduceFrost,"Produce Frost",SpellType.Cleric,3,6,3,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"3-18 cold damage","Inflicts severe frostbite on monster, doing 3-18 points of damage, unless monster is immune to cold."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.RemoveQuest,"Remove Quest",SpellType.Cleric,3,7,3,false,0,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Remove current side quest","Releases party from its commitment to a quest (only affects those given by Lords of castles)."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.WalkonWater,"Walk on Water",SpellType.Cleric,3,8,3,false,1,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Allow passage through water","Creates a floating sand dune upon which the party may walk on. Lasts 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.CureDisease,"Cure Disease",SpellType.Cleric,4,1,4,false,0,SpellWhen.NonCombatAnywhere,SpellTarget.Character,"Remove diseased condition","Restores full health to sick character, instantaneously removing the diseased condition."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.NeutralizePoison,"Neutralize Poison",SpellType.Cleric,4,2,4,false,0,SpellWhen.NonCombatAnywhere,SpellTarget.Character,"Remove poisoned condition","Flushes poison out of character's system, instantaneously removing the poisoned condition."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ProtectfromAcid,"Protect from Acid",SpellType.Cleric,4,3,4,false,0,SpellWhen.AnywhereAnytime,SpellTarget.Party,"Add 20% + 1%/level acid resistance","Increases all characters' resistance to acid attacks. Amount of the increase depends on the caster's experience level. Spell lasts 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ProtectfromElec,"Protect from Elec",SpellType.Cleric,4,4,4,false,0,SpellWhen.AnywhereAnytime,SpellTarget.Party,"Add 20% + 1%/level electrical resistance","Increases all characters' resistance to electrical attacks. Amount of the increase depends on the caster's experience level. Spell lasts 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.RestoreAlignment,"Restore Alignment",SpellType.Cleric,4,5,4,false,2,SpellWhen.NonCombatAnywhere,SpellTarget.Character,"Restores original alignment","Restores a character's original alignment, after actions and responses have caused it to shift."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.SummonLightning,"Summon Lightning",SpellType.Cleric,4,6,4,false,0,SpellWhen.OutdoorCombat,SpellTarget.ThreeRangedMonsters,"4-32 electrical damage","Zaps monsters with lightning bolts, inflicting 4-32 damage points on each monster not immune to lightning."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.SuperHeroism,"Super Heroism",SpellType.Cleric,4,7,4,false,2,SpellWhen.CombatAnywhere,SpellTarget.Character,"Add 10 HP and 3 levels","Temporarily bestows 10 additional Hit Points and 3 additional experience levels on character. Lasts for the duration of combat."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Surface,"Surface",SpellType.Cleric,4,8,4,false,2,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Teleport to surface map","Instantly transports all party members from an underground location to ground surface."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.DeadlySwarm,"Deadly Swarm",SpellType.Cleric,5,1,5,false,0,SpellWhen.OutdoorCombat,SpellTarget.AllMonsters,"2-20 physical damage","Sends a swarm of killer insects against the monsters, inflicting 2-20 damage points against each monster."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.DispelMagicCleric,"Dispel Magic",SpellType.Cleric,5,2,5,false,0,SpellWhen.AnywhereAnytime,SpellTarget.AllPlayersAndMonsters,"Remove all protective and detrimental magic","Cancels all magic spells currently active both for characters and monsters."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Paralyze,"Paralyze",SpellType.Cleric,5,3,5,false,0,SpellWhen.CombatAnywhere,SpellTarget.AllMeleeMonsters,"Prevent action","Attempts to immobilize all monsters and prevent them from fighting. May be partially or completely ineffective on some or all monsters."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.RemoveCondition,"Remove Condition",SpellType.Cleric,5,4,5,false,3,SpellWhen.AnywhereAnytime,SpellTarget.Character,"Remove conditions except dead, stoned, eradicated","Releases character from all undesirable conditions except dead, stoned or eradicated."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.RestoreEnergy,"Restore Energy",SpellType.Cleric,5,5,5,false,3,SpellWhen.AnywhereAnytime,SpellTarget.Character,"Restore 1-5 levels","Replaces 1-5 experience levels that have been lost or drained from character, up to his/her former level."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.MoonRay,"Moon Ray",SpellType.Cleric,6,1,6,false,4,SpellWhen.OutdoorCombat,SpellTarget.AllPlayersAndMonsters,"Heal 3-30 HP and does 3-30 energy damage","Bathes all combatants in a beneficent ray that bestows 3-30 Hit Points on each character and removes 3-30 Hit Points from each monster."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.RaiseDead,"Raise Dead",SpellType.Cleric,6,2,6,false,4,SpellWhen.AnywhereAnytime,SpellTarget.Character,"Remove dead condition","Brings the character back to life, removing the dead condition. Spell carries a moderate chance of failure and a remote chance of eradicating the character."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Rejuvenate,"Rejuvenate",SpellType.Cleric,6,3,6,false,4,SpellWhen.NonCombatAnywhere,SpellTarget.Character,"Subtract 1-10 years from age","A fountain of youth that trims 1-10 years off a character's age, restoring his/her abilities to the younger level. Spell carries some risk of producing the opposite effect."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.StonetoFlesh,"Stone to Flesh",SpellType.Cleric,6,4,6,false,4,SpellWhen.AnywhereAnytime,SpellTarget.Character,"Remove stone condition","Re-animates a character who has been tumed to stone, removing the stoned condition."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.TownPortal,"Town Portal",SpellType.Cleric,6,5,6,false,4,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Teleport to town","Opens a temporary portal to any town and moves the party through the portal to that town."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.DivineIntervention,"Divine Intervention",SpellType.Cleric,7,1,7,false,10,SpellWhen.CombatAnywhere,SpellTarget.Party,"Restore HP and remove conditions except eradicated","Intercedes with supernatural forces to restore all characters' Hit Points and remove all undesirable conditions, except eradicated."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.HolyWord,"Holy Word",SpellType.Cleric,7,2,7,false,5,SpellWhen.CombatAnywhere,SpellTarget.AllUndeadMonsters,"Destroy all undead monsters","Utters a single word of devastating power, that totally destroys all undead monsters."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ProtfromElements,"Prot from Elements",SpellType.Cleric,7,3,7,false,5,SpellWhen.AnywhereAnytime,SpellTarget.Party,"Add 25% + 1%/level elemental resistance","Increases all characters' resistance to fear, cold, fire, poison, acid and electricity. Amount of the increase depends on the caster's experience level. Spell lasts 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Resurrection,"Resurrection",SpellType.Cleric,7,4,7,false,5,SpellWhen.NonCombatAnywhere,SpellTarget.Character,"Remove eradicated condition, +10 years, -1 END","Removes the eradicated condition from the character, adding 10 years to his/her age and subtracting 1 endurance point from his/her vital statistics. There is a chance that the spell will fail."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.SunRay,"Sun Ray",SpellType.Cleric,7,5,7,false,5,SpellWhen.OutdoorCombat,SpellTarget.Monster,"50-100 fire damage","Sears the monster with a focused ray of deadly light, inflicting 50-100 damage points."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.AwakenSorcerer,"Awaken",SpellType.Sorcerer,1,1,1,false,0,SpellWhen.CombatAnywhere,SpellTarget.Party,"Remove asleep condition","Awakens all sleeping members of the party, instantaneously cancelling the sleep condition."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.DetectMagic,"Detect Magic",SpellType.Sorcerer,1,2,1,false,0,SpellWhen.NonCombatAnywhere,SpellTarget.Caster,"Identify magic items and number of charges","Reveals any magical items in caster's back pack, and notes the number of magical charges remaining in any item which must be charged for use. Also detects any magic surrounding or inside containers."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.EnergyBlast,"Energy Blast",SpellType.Sorcerer,1,3,1,true,1,SpellWhen.CombatAnywhere,SpellTarget.Monster,"1-4 energy damage/level","Zaps the monster with a blast of pure energy, inflicting 1-4 damage points/lv."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.FlameArrow,"Flame Arrow",SpellType.Sorcerer,1,4,1,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"1-6 fire damage","Sends a burning shaft into the monster, inflicting 1-6 points of fire damage, unless monster is immune to fire."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.LeatherSkin,"Leather Skin",SpellType.Sorcerer,1,5,1,false,0,SpellWhen.AnywhereAnytime,SpellTarget.Party,"Resist damage","Toughens all characters' skin, so that attacks from monsters bounce off rather than hitting."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.LightSorcerer,"Light",SpellType.Sorcerer,1,6,1,false,0,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Create 1 light factor","Gives the party 1 light factor, sufficient to light a single darkened square. Multiple light spells can be cast, to accumulate light factors."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Location,"Location",SpellType.Sorcerer,1,7,1,false,0,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Reveal map sector, coordinates, and facing info","Gives precise information on party's location. This spell is the key to successful mapping."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Sleep,"Sleep",SpellType.Sorcerer,1,8,1,false,0,SpellWhen.CombatAnywhere,SpellTarget.FiveMonsters,"Prevent action until attacked","Casts monsters into a deep sleep, preventing them from attacking. Effective until monster is damaged."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ElecricArrow,"Electric Arrow",SpellType.Sorcerer,2,1,2,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"2-12 electrical damage","Electrocutes a monster, inflicting 2-12 damage points, unless monster is immune to electrical attack."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Hypnotize,"Hypnotize",SpellType.Sorcerer,2,2,2,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"Prevent action until attacked","Uses the power of suggestion to prevent a monster from attacking. Effective until monster is attacked."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.IdentifyMonster,"Identify Monster",SpellType.Sorcerer,2,3,2,false,1,SpellWhen.CombatAnywhere,SpellTarget.Monster,"Show speed, attacks, damage, HP, AC, etc.","Informs caster of the nature of any one monster during combat."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Jump,"Jump",SpellType.Sorcerer,2,4,2,false,0,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Teleport 2 squares forward","Gives all characters super strength, enabling them to jump 2 squares forward, providing there are no magical obstructions (force fields, etc.) in the way."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Levitate,"Levitate",SpellType.Sorcerer,2,5,2,false,0,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Prevent damage and encounters from pit traps","Raises all characters above ground level, protecting them from various dangers for 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Power,"Power",SpellType.Sorcerer,2,6,2,false,0,SpellWhen.CombatAnywhere,SpellTarget.Character,"Add 1-4 might","Boosts that character's Might by 1-4 points for the duration of combat."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Quickness,"Quickness",SpellType.Sorcerer,2,7,2,false,0,SpellWhen.CombatAnywhere,SpellTarget.Character,"Add 1-4 speed","Boosts character's Speed by 1-4 points for the duration of combat, moving him/her further forward in order of combat."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Scare,"Scare",SpellType.Sorcerer,2,8,2,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"Decreace chance to hit","Strikes fear into the monster's heart, decreasing its probability of hitting a character during combat."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.FireBall,"Fire Ball",SpellType.Sorcerer,3,1,1,true,1,SpellWhen.CombatAnywhere,SpellTarget.FiveRangedMonsters,"1-6 fire damage/level","Rolls a deadly ball of fire into the monsters' midst, inflicting 1-6 damage points per level of experience on each monster."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Fly,"Fly",SpellType.Sorcerer,3,2,3,false,0,SpellWhen.OutdoorNonCombat,SpellTarget.Party,"Teleport to surface map","Grants magical flight to all characters, allowing the party as a whole to move to any other outdoor area. The party will land in the safest square in that area."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Invisibility,"Invisibility",SpellType.Sorcerer,3,3,3,false,1,SpellWhen.CombatAnywhere,SpellTarget.Party,"Reduce chance of being hit","Turns all characters invisible, greatly decreasing the monsters' chances of hitting them."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.LightningBolt,"Lightning Bolt",SpellType.Sorcerer,3,4,1,true,1,SpellWhen.CombatAnywhere,SpellTarget.ThreeMonsters,"1-6 electrical damage/level","Blasts the monsters with a gigantic lightning bolt that inflicts 1-6 damage points per level of experience on each monster."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.MakeRoom,"Make Room",SpellType.Sorcerer,3,5,3,false,0,SpellWhen.CombatAnywhere,SpellTarget.Party,"Puts characters 1-5 in melee range","Expands the combat area, allowing the first 5 characters to engage in hand-to-hand combat."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Slow,"Slow",SpellType.Sorcerer,3,6,3,false,0,SpellWhen.CombatAnywhere,SpellTarget.AllMonsters,"Reduce speed by 50%","Places an invisible force field around all monsters' feet, slowing them down to 1/2 their original speed and putting them farther back in order of combat."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Weaken,"Weaken",SpellType.Sorcerer,3,7,3,false,1,SpellWhen.CombatAnywhere,SpellTarget.AllMonsters,"2 damage and -1 AC","Weakens all monsters, reducing each monster's Hit Points by 2 and Armor Class by 1."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Web,"Web",SpellType.Sorcerer,3,8,3,false,0,SpellWhen.CombatAnywhere,SpellTarget.FiveRangedMonsters,"Prevent action","Wraps 1-5 monsters in a supernatural web, preventing them from fighting for the duration of combat or until they escape."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.AcidArrow,"Acid Arrow",SpellType.Sorcerer,4,1,4,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"3-30 acid damage","Attacks with corrosive acid that inflicts 3-30 damage points, unless the monster is immune to acid."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ColdBeam,"Cold Beam",SpellType.Sorcerer,4,2,4,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"4-40 cold damage","Attacks with a beam of intense cold that penetrates to the monster's heart and inflicts 4-40 damage points, unless the monster is immune to cold."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.FeebleMind,"Feeble Mind",SpellType.Sorcerer,4,3,4,false,2,SpellWhen.CombatAnywhere,SpellTarget.Monster,"Prevent abilities","Erases the monsters brain, removing all its abilities for the duration of combat or until the monster overcomes the spell."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Freeze,"Freeze",SpellType.Sorcerer,4,4,4,false,0,SpellWhen.CombatAnywhere,SpellTarget.Monster,"Prevent action","Immobilizes the monster, preventing it from attacking for the duration of combat. Monster's chance of overcoming this spell is very small."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.GuardDog,"Guard Dog",SpellType.Sorcerer,4,5,4,false,0,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Prevent surprise attacks","Places a supernatural guard over party, preventing surprise attacks for 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.PsychicProt,"Psychic Prot",SpellType.Sorcerer,4,6,4,false,2,SpellWhen.AnywhereAnytime,SpellTarget.Party,"Prevent damage from psychic traps","Grants all characters immunity from mind influencing spells for 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Shield,"Shield",SpellType.Sorcerer,4,7,4,false,2,SpellWhen.CombatAnywhere,SpellTarget.Party,"Protect against missile weapons","Creates an invisible shield which surrounds the party and protects all characters from most missile weapons for the duration of combat."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.TimeDistortion,"Time Distortion",SpellType.Sorcerer,4,8,4,false,2,SpellWhen.CombatAnywhere,SpellTarget.Party,"Causes successful run from combat","Creates a warp in time that enables the party to retreat safely from most battles."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.AcidRain,"Acid Rain",SpellType.Sorcerer,5,1,5,false,0,SpellWhen.OutdoorCombat,SpellTarget.AllRangedMonsters,"5-50 acid damage","Unleashes a torrent of acid rain that inflicts 5-50 damage points on each monster, unless immune to acid."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.DispelMagicSorcerer,"Dispel Magic",SpellType.Sorcerer,5,2,5,false,0,SpellWhen.AnywhereAnytime,SpellTarget.AllPlayersAndMonsters,"Remove all protective and detrimental magic","Cancels all magic spells currently active, both for characters and monsters."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.FingerofDeath,"Finger of Death",SpellType.Sorcerer,5,3,5,false,3,SpellWhen.CombatAnywhere,SpellTarget.NonUndeadMonster,"Cause death","Channels the ancient power of all dead sorcerers through the caster, resulting in death to the monster at whom the caster points a finger."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Shelter,"Shelter",SpellType.Sorcerer,5,4,5,false,3,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Rest without encounter","Provides' 1 day's rest free of the danger of encounter."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Teleport,"Teleport",SpellType.Sorcerer,5,5,5,false,3,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Teleport 9 squares any direction","Instantly moves the party from its present position, up to 9 squares in any direction."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.DancingSword,"Dancing Sword",SpellType.Sorcerer,6,1,6,false,4,SpellWhen.CombatAnywhere,SpellTarget.AllMonsters,"1-30 magical damage","A magical sword that moves with lightning speed, inflicting 1-30 damage points on each monster.  The sword cannot be avoided, nor can the damage from it be minimized."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Disintegration,"Disintegration",SpellType.Sorcerer,6,2,6,false,4,SpellWhen.CombatAnywhere,SpellTarget.Monster,"Cause death","Reduces the monster to a pile of dust, utterly destroying it."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Etherealize,"Etherealize",SpellType.Sorcerer,6,3,6,false,4,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Teleport one square forward","Alters all characters' molecular structure long enough to allow them to move 1 square forward through any special barrier (force field, etc.)."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.ProtfromMagic,"Prot from Magic",SpellType.Sorcerer,6,4,6,false,4,SpellWhen.AnywhereAnytime,SpellTarget.Party,"Add 20% + 1%/level magic resistance","Increases all characters' resistance to magic. Amount of the increase depends on experience level of caster. Spell lasts 1 day."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.RechargeItem,"Recharge Item",SpellType.Sorcerer,6,5,6,false,4,SpellWhen.NonCombatAnywhere,SpellTarget.Caster,"Add 1-4 charges to an item","Restores 1-4 charges to any item in caster's back pack that still has 1 magical charge remaining.  Some risk that the spell will fail and destroy the item."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.AstralSpell,"Astral Spell",SpellType.Sorcerer,7,1,7,false,5,SpellWhen.NonCombatAnywhere,SpellTarget.Party,"Teleport to Astral Plane","Transports all characters' to the astral plane. This highly dangerous and unpredictable area is otherwise impossible to reach."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.Duplication,"Duplication",SpellType.Sorcerer,7,2,7,false,100,SpellWhen.NonCombatAnywhere,SpellTarget.Caster,"Create duplicate item","Allows the caster to exactly duplicate any 1 item in his/her back pack, provided that there is room in the caster's pack for the new item.  Small chance that the spell will fail and destroy the original item."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.MeteorShower,"Meteor Shower",SpellType.Sorcerer,7,3,7,false,5,SpellWhen.OutdoorCombat,SpellTarget.AllMonsters,"1-120 energy damage","Buries all monsters under a hail of meteors, inflicting 1-120 damage points on each monster."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.PowerShield,"Power Shield",SpellType.Sorcerer,7,4,7,false,5,SpellWhen.CombatAnywhere,SpellTarget.Party,"50% damage reduction","Reduces the damage inflicted on all characters by any attack, by 1/2.  Lasts for the duration of combat."));
			m_spells.Add(new MM1Spell(MM1SpellIndex.PrismaticLight,"Prismatic Light",SpellType.Sorcerer,7,5,7,false,5,SpellWhen.CombatAnywhere,SpellTarget.AllMonsters,"Random effect","A powerful, but erratic spell that has completely unpredictable effects on all monsters."));        }
    }

    public class MM1SpellCost : MMSpellCost
    {
        public MM1SpellCost(int sp, bool perlevel, int gems)
        {
            SpellPoints = sp;
            PerLevel = perlevel;
            Gems = gems;
        }

    }

    public class MM1Spell : MMSpell
    {
        public MM1SpellIndex Index;
        public override int BasicIndex { get { return (int)Index; } }

        public MM1Spell(MM1SpellIndex index, string name, SpellType type, int level, int number, int sp, bool perlevel, int gems, SpellWhen when, SpellTarget target, string shortDesc, string desc)
        {
            Index = index;
            Name = name;
            Type = type;
            Level = level;
            Number = number;
            Cost = new MM1SpellCost(sp, perlevel, gems);
            When = when;
            Target = target;
            ShortDescription = shortDesc;
            Description = desc;
            Learned = GetLocation(type, level);
        }

        public string GetLocation(SpellType type, int level)
        {
            if (type == SpellType.Cleric)
            {
                if (level < 5)
                    return String.Format("Cleric level {0}, Paladin level {1}", level * 2 - 1, level * 2 + 5);
                else
                    return String.Format("Cleric level {0}", level * 2 - 1);
            }
            else
            {
                if (level < 5)
                    return String.Format("Sorcerer level {0}, Archer level {1}", level * 2 - 1, level * 2 + 5);
                else
                    return String.Format("Sorcerer level {0}", level * 2 - 1);
            }
        }

        public override Keys[] GetKeys()
        {
            return new Keys[] { Keys.D0 + Level, Keys.D0 + Number, Keys.None, Keys.Enter };
        }

        public override MMSpell None
        {
            get { return new MM1Spell(MM1SpellIndex.None, "None", SpellType.Unknown, 0, 0, 0, false, 0, SpellWhen.None, SpellTarget.Unknown, "", ""); }
        }
    }
}
