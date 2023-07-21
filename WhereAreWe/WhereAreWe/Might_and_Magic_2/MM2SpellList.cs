using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public enum MM2SpellIndex
    {
        AwakenSorcerer = 0, 
        DetectMagic = 1, 
        EnergyBlast = 2, 
        FlameArrow = 3, 
        LightSorcerer = 4, 
        Location = 5, 
        Sleep = 6, 
        EagleEye = 7, 
        ElectricArrow = 8, 
        IdentifyMonster = 9, 
        Jump = 10,
        Levitate = 11,
        LloydsBeacon = 12,
        ProtectionfromMagic = 13,
        AcidStream = 14,
        Fly = 15,
        Invisibility = 16,
        LightningBolt = 17,
        Web = 18,
        WizardEye = 19,
        ColdBeam = 20,
        FeebleMind = 21,
        FireBall = 22,
        GuardDog = 23,
        Shield = 24,
        TimeDistortion = 25,
        Disrupt = 26,
        FingersofDeath = 27,
        SandStorm = 28,
        Shelter = 29,
        Teleport = 30,
        Disintegration = 31,
        Entrapment = 32,
        FantasticFreeze = 33,
        RechargeItem = 34,
        SuperShock = 35,
        DancingSword = 36,
        Duplication = 37,
        Etherealize = 38,
        PrismaticLight = 39,
        Incinerate = 40,
        MegaVolts = 41,
        MeteorShower = 42,
        PowerShield = 43,
        Implosion = 44,
        Inferno = 45,
        StarBurst = 46,
        EnchantItem = 47,
        Apparition = 48,
        AwakenCleric = 49,
        Bless = 50,
        FirstAid = 51,
        LightCleric = 52,
        PowerCure = 53,
        TurnUndead = 54,
        CureWounds = 55,
        Heroism = 56,
        NaturesGate = 57,
        Pain = 58,
        ProtectionFromElements = 59,
        Silence = 60,
        Weaken = 61,
        ColdRay = 62,
        CreateFood = 63,
        CurePoison = 64,
        Immobilize = 65,
        LastingLight = 66,
        WalkonWater = 67,
        AcidSpray = 68,
        AirTransmutation = 69,
        CureDisease = 70,
        RestoreAlignment = 71,
        Surface = 72,
        HolyBonus = 73,
        AirEncasement = 74,
        DeadlySwarm = 75,
        Frenzy = 76,
        Paralyze = 77,
        RemoveCondition = 78,
        EarthTransmutatuion = 79,
        Rejuvenate = 80,
        StonetoFlesh = 81,
        WaterEncasement = 82,
        WaterTransmutation = 83,
        EarthEncasement = 84,
        FieryFlail = 85,
        MoonRay = 86,
        RaiseDead = 87,
        FireEncasement = 88,
        FireTransmutation = 89,
        MassDistortion = 90,
        TownPortal = 91,
        DivineIntervention = 92,
        HolyWord = 93,
        Resurrection = 94,
        UncurseItem = 95,
        None = 255,
    }

    public class MM2SpellList
    {
        List<MM2Spell> m_spells;

        public List<MM2Spell> Spells
        {
            get { return m_spells; }
        }

        public static string GetSpellNameForItem(int iIndex)
        {
            if (iIndex == 0)
                return "None";

            iIndex--;   // Items index spell at 1; we index them at 0

            if (iIndex >= MM2.Spells.Count)
                return "Unknown";

            MM2Spell spell = MM2.Spells[iIndex];

            return String.Format("{0} ({1}{2}{3})",
                spell.Name,
                spell.Type == SpellType.Cleric ? "C" : "S",
                spell.Level,
                spell.Number
                );
        }

        public MM2SpellList()
        {
            m_spells = new List<MM2Spell>(96);
            m_spells.Add(new MM2Spell(MM2SpellIndex.AwakenSorcerer, "Awaken", SpellType.Sorcerer, 1, 1, 1, false, false, false, 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Remove asleep condition", "Awakens all sleeping members of the party, instantaneously cancelling the sleep condition. May be critical if party is attacked during rest.", "C-2, Middlegate (7,14)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.DetectMagic, "Detect Magic", SpellType.Sorcerer, 1, 2, 1, false, false, false, 0, SpellWhen.NonCombatAnywhere, SpellTarget.Caster, "Identify magic items and number of charges", "Reveals any magical items in caster's backpack, and notes the number of magical charges remaining in any item which must be charged for use. Also detects any magic surrounding or inside a chest.", "Sorcerer L1, Archer L7"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.EnergyBlast, "Energy Blast", SpellType.Sorcerer, 1, 3, 1, true, false, false, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster, "1-6 energy damage/level ", "Zaps the monster with a blast of pure energy, inflicting 1-6 damage points per experience level of caster.", "C-2, Middlegate (7,14)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.FlameArrow, "Flame Arrow", SpellType.Sorcerer, 1, 4, 1, false, false, false, 0, SpellWhen.CombatAnywhere, SpellTarget.Monster, "2-8 fire damage", "Sends a burning shaft into the monster, inflicting 2-8 points of fire damage, unless monster is immune to fire.", "Sorcerer L1, Archer L7"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.LightSorcerer, "Light", SpellType.Sorcerer, 1, 5, 1, false, false, false, 0, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Create 1 light factor", "Gives the party 1 light factor, sufficient to light a single darkened square. Multiple light spells can be cast, to accumulate light factors.", "Sorcerer L1, Archer L7"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Location, "Location", SpellType.Sorcerer, 1, 6, 1, false, false, false, 0, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Reveal map sector, coordinates, and facing info", "Gives precise information on party's location. Shows a map of the current 16xl6 area that the party has mapped and shows your present location on that map. May be critical when party is lost or magically transported. In general, this spell is the key to successful mapping.", "Sorcerer L1, Archer L7"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Sleep, "Sleep", SpellType.Sorcerer, 1, 7, 1, false, false, false, 0, SpellWhen.CombatAnywhere, SpellTarget.FourMonstersPlusOnePerLevel, "Prevent action until attacked", "Sends monsters into a deep sleep, preventing them from attacking. Effective until monster is damaged or overcomes the spell.", "C-2, Middlegate (7,14)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.EagleEye, "Eagle Eye", SpellType.Sorcerer, 2, 1, 2, true, false, false, 0, SpellWhen.OutdoorNonCombat, SpellTarget.FiveStepsPerLevel, "Show minimap for outdoor areas", "An eagle eye view of the outdoor terrain appears on the screen, providing a 5x5 overhead view of the area and your party's location.", "C-2, Middlegate (10,2)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.ElectricArrow, "Electric Arrow", SpellType.Sorcerer, 2, 2, 2, false, false, false, 0, SpellWhen.CombatAnywhere, SpellTarget.Monster, "4-16 electrical damage", "Electrocutes a monster, inflicting 4-16 damage points, unless monster is immune to electrical attack.", "Sorcerer L3, Archer L9"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.IdentifyMonster, "Identify Monster", SpellType.Sorcerer, 2, 3, 2, false, false, false, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster, "Show speed, attacks, damage, HP, AC, etc.", "Informs caster of the current condition of any one monster during combat.", "C-2, Middlegate (7,14)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Jump, "Jump", SpellType.Sorcerer, 2, 4, 2, false, false, false, 0, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Teleport 2 squares forward if no obstacles", "Moves the party 2 squares forward, providing there are no magical obstructions (force fields, etc.) in the way.", "Sorcerer L3, Archer L9"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Levitate, "Levitate", SpellType.Sorcerer, 2, 5, 2, false, false, false, 0, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Prevent damage and encounters from pit traps", "Raises all characters above ground level, protecting them from various dangers for 1 day.", "Sorcerer L1, Archer L7"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.LloydsBeacon, "Lloyd's Beacon", SpellType.Sorcerer, 2, 6, 2, false, false, false, 1, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Set/Return to beacon", "Leaves a beacon at your current location so that you may instantaneously return to that location the next time you cast this spell.", "C-2, Corak's Cave (7,11)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.ProtectionfromMagic, "Protection from Magic", SpellType.Sorcerer, 2, 7, 1, true, false, false, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Add 10% + 1%/level magic resistance", "Increases all characters' resistance to magic. Amount of the increase depends on experience level of caster. Spell lasts 1 day.", "E-4, Sandsobar (6,7)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.AcidStream, "Acid Stream", SpellType.Sorcerer, 3, 1, 1, true, false, false, 2, SpellWhen.CombatAnywhere, SpellTarget.Monster, "2-8 acid damage/level", "Sprays a burning stream of acid inflicting 2-8 points of damage per level of caster, unless immune to acid.", "E-4, Sandsobar (6,7)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Fly, "Fly", SpellType.Sorcerer, 3, 2, 3, false, false, false, 0, SpellWhen.OutdoorNonCombat, SpellTarget.Party, "Teleport to surface map", "Grants magical flight to all characters, allowing the party as a whole to move to any other outdoor area. The party will land in the safest square in that area.", "Sorcerer L5, Archer L11"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Invisibility, "Invisibility", SpellType.Sorcerer, 3, 3, 3, false, false, false, 0, SpellWhen.CombatAnywhere, SpellTarget.Party, "Reduce chance of being hit", "Drops a cloak of invisibility over all characters, greatly decreasing the monsters' chances of hitting them.", "Sorcerer L5, Archer L11"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.LightningBolt, "Lightning Bolt", SpellType.Sorcerer, 3, 4, 1, true, false, false, 2, SpellWhen.CombatAnywhere, SpellTarget.FourMonsters, "1-6 electrical damage/level", "Blasts the monsters with a gigantic lightning bolt that inflicts 1-6 damage points per level of caster.", "E-4, Sandsobar (6,7)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Web, "Web", SpellType.Sorcerer, 3, 5, 3, false, false, false, 2, SpellWhen.CombatAnywhere, SpellTarget.FourRangedMonstersPlusOnePerLevel, "Prevent action", "Wraps monsters in a supernatural web, preventing them from fighting for the duration of combat or until they escape.", "Sorcerer L5, Archer L11"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.WizardEye, "Wizard Eye", SpellType.Sorcerer, 3, 6, 3, true, false, false, 2, SpellWhen.IndoorNonCombat, SpellTarget.FiveStepsPerLevel, "Show minimap for indoor areas", "Uses the magical eye of a powerful wizard to show a 5x5 overhead view of your party's location in any indoor maze.", "E-4, Sandsobar (7,4)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.ColdBeam, "Cold Beam", SpellType.Sorcerer, 4, 1, 1, true, false, false, 3, SpellWhen.CombatAnywhere, SpellTarget.Monster, "6 cold damage/level", "Attacks with a beam of intense cold that penetrates to the monster's heart and inflicts 6 damage point per level of caster, unless the monster is immune to cold.", "E-4, Sandsobar (6,7)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.FeebleMind, "Feeble Mind", SpellType.Sorcerer, 4, 2, 4, false, false, false, 3, SpellWhen.CombatAnywhere, SpellTarget.FiveMonsters, "Prevent abilities", "Erases the monsters brain, removing all its abilities for the duration of combat or until the monster overcomes the spell.", "A-1, Tundara (14,13)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.FireBall, "Fire Ball", SpellType.Sorcerer, 4, 3, 1, true, false, false, 3, SpellWhen.CombatAnywhere, SpellTarget.SixRangedMonsters, "1-6 fire damage/level", "Rolls a deadly ball of fire into the monsters' midst, inflicting 1-6 damage points per level of caster.", "A-1, Tundara (14,13)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.GuardDog, "Guard Dog", SpellType.Sorcerer, 4, 4, 4, false, false, false, 0, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Prevent surprise attacks", "Places a supernatural guard over party, preventing surprise attacks for 1 day.", "Sorcerer L7, Archer L13"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Shield, "Shield", SpellType.Sorcerer, 4, 5, 4, false, false, false, 0, SpellWhen.CombatAnywhere, SpellTarget.Party, "Protect against missile weapons", "Creates an invisible shield which surrounds the party and protects all characters from most missile weapons for the duration of combat.", "Sorcerer L7, Archer L13"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.TimeDistortion, "Time Distortion", SpellType.Sorcerer, 4, 6, 4, false, false, false, 3, SpellWhen.CombatAnywhere, SpellTarget.Party, "Causes successful run from combat", "Creates a warp in time that enables the party to retreat safely from most battles.", "Sorcerer L7, Archer L13"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Disrupt, "Disrupt", SpellType.Sorcerer, 5, 1, 5, false, false, false, 5, SpellWhen.CombatAnywhere, SpellTarget.OneRangedMonster, "100 physical damage", "Creates a powerful energy field that disrupts the molecular bonds of the target, inflicting 100 points of damage.", "A-1, Tundara (14,13)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.FingersofDeath, "Fingers of Death", SpellType.Sorcerer, 5, 2, 5, false, false, false, 5, SpellWhen.CombatAnywhere, SpellTarget.ThreeNonUndeadMonsters, "Cause death", "Channels the ancient power of all dead sorcerers through the caster, resulting in death to the monsters at whom the caster points a finger.", "C-1, Surface (1,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.SandStorm, "Sand Storm", SpellType.Sorcerer, 5, 3, 2, true, false, false, 5, SpellWhen.OutdoorCombat, SpellTarget.TenMonsters, "1-8 physical damage/level", "Calls upon the forces of nature to create a violent sand storm inflicting 1-8 points of damage per level of caster.", "A-1, Tundara (14,13)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Shelter, "Shelter", SpellType.Sorcerer, 5, 4, 5, false, false, false, 0, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Rest without encounter", "Provides 1 day's rest free of the danger of encounter.", "Sorcerer L9, Archer L15"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Teleport, "Teleport", SpellType.Sorcerer, 5, 5, 5, false, false, false, 0, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Teleport 9 squares any direction", "Instantly moves the party from its present position, up to 9 squares in any direction.", "Sorcerer L9, Archer L15"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Disintegration, "Disintegration", SpellType.Sorcerer, 6, 1, 6, false, false, false, 6, SpellWhen.CombatAnywhere, SpellTarget.ThreeMonsters, "Destroys monsters", "Inflicts 50 damage points while disintegrating parts or all of the target.", "E-1, Vulcania (11,5)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Entrapment, "Entrapment", SpellType.Sorcerer, 6, 2, 6, false, false, false, 6, SpellWhen.CombatAnywhere, SpellTarget.AllPlayersAndMonsters, "Prevent fleeing from combat", "Surrounds the battle with a magical energy field preventing all from escaping.", "Sorcerer L11, Archer L17"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.FantasticFreeze, "Fantastic Freeze", SpellType.Sorcerer, 6, 3, 2, true, false, false, 6, SpellWhen.CombatAnywhere, SpellTarget.ThreeRangedMonsters, "10 cold damage /level", "Shoots a fantastic beam of cold at 3 monsters, crystalizing them and inflicting 10 damage points per level of caster.", "E-1, Vulcania (11,5)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.RechargeItem, "Recharge Item", SpellType.Sorcerer, 6, 4, 6, false, false, false, 6, SpellWhen.NonCombatAnywhere, SpellTarget.Caster, "Add 1-6 charges to an item", "Restores 1-6 charges to any item in caster's backpack that still has 1 magical charge remaining. Some risk that the spell will fail and destroy the item.", "Sorcerer L11, Archer L17"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.SuperShock, "Super Shock", SpellType.Sorcerer, 6, 5, 2, true, false, false, 6, SpellWhen.CombatAnywhere, SpellTarget.Monster, "20 electrical damage/level", "Shoots an intense beam of electricity, shocking a monster with 20 damage points per level of caster.", "E-1, Vulcania (11,5)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.DancingSword, "Dancing Sword", SpellType.Sorcerer, 7, 1, 3, true, false, false, 7, SpellWhen.CombatAnywhere, SpellTarget.TenMonsters, "1-12 physical damage/level", "A magical sword that moves with lightning speed and inflicts 1-12 damage points per level of caster.", "A-2, Surface (15,11)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Duplication, "Duplication", SpellType.Sorcerer, 7, 2, 7, false, false, false, 100, SpellWhen.NonCombatAnywhere, SpellTarget.Caster, "Create duplicate item", "Allows the caster to exactly duplicate any 1 item in his/her backpack, provided that there is room in the caster's pack for the new item. Small chance that the spell will fail and destroy the original item.", "E-1, Vulcania (11,5)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Etherealize, "Etherealize", SpellType.Sorcerer, 7, 3, 7, false, false, false, 7, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Teleport one square forward", "Alters all characters' molecular struture long enough to allow them to move 1 square forward through any barrier (force field, wall, mountain, etc.).", "Sorcerer L13, Archer L19"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.PrismaticLight, "Prismatic Light", SpellType.Sorcerer, 7, 4, 7, false, false, false, 7, SpellWhen.CombatAnywhere, SpellTarget.TenMonsters, "Random effect", "A powerful, but erratic spell that has completely unpredictable effects.", "Sorcerer L13, Archer L19"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Incinerate, "Incinerate", SpellType.Sorcerer, 8, 1, 3, true, false, false, 8, SpellWhen.CombatAnywhere, SpellTarget.Monster, "20-40 fire damage/level", "Engulfs a monster with the heat of a thousand fires doing 20-40 damage points per level of caster.", "Sorcerer L15"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.MegaVolts, "Mega Volts", SpellType.Sorcerer, 8, 2, 3, true, false, false, 8, SpellWhen.CombatAnywhere, SpellTarget.TenMonsters, "4-16 electrical damage/level", "Creates a chain of electricity connecting all opponents with the deadly voltage doing 4-16 damage points per level of caster.", "A-4, Atlantium (0,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.MeteorShower, "Meteor Shower", SpellType.Sorcerer, 8, 3, 8, false, true, false, 8, SpellWhen.OutdoorCombat, SpellTarget.AllMonsters, "5-30 physical damage", "Buries all monsters under a hail of meteors, inflicting 5-30 damage points on each monster.", "A-4, Atlantium (0,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.PowerShield, "Power Shield", SpellType.Sorcerer, 8, 4, 8, false, false, false, 8, SpellWhen.CombatAnywhere, SpellTarget.Party, "50% damage reduction", "Reduces the damage inflicted on all characters by any attack, by 1/ Lasts for the duration of combat.", "Sorcerer L15"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Implosion, "Implosion", SpellType.Sorcerer, 9, 1, 10, false, false, false, 10, SpellWhen.CombatAnywhere, SpellTarget.Monster, "1000 physical damage", "Creates a hole in space, at the center of the target creature, sucking it into nothingness.", "A-4, Atlantium (0,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Inferno, "Inferno", SpellType.Sorcerer, 9, 2, 3, true, false, false, 10, SpellWhen.CombatAnywhere, SpellTarget.TenMonsters, "1-20 fire damage/level", "Unleashes the heat of the sun on all monsters shown, doing 1-20 points damage per level of caster.", "A-4, Atlantium (0,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.StarBurst, "Star Burst", SpellType.Sorcerer, 9, 3, 10, false, true, false, 20, SpellWhen.OutdoorCombat, SpellTarget.AllMonsters, "20-200 physical damage", "Showers all monsters with pieces of an exploding star, doing 20-200 points of damage.", "D-1, Surface (5,6)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.EnchantItem, "Enchant Item", SpellType.Sorcerer, 9, 4, 0, false, false, true, 50, SpellWhen.NonCombatAnywhere, SpellTarget.Caster, "Add 1 to bonus of an item", "Attempts to raise the magic ability of an item by increasing it's '+'.", "E-1, Gemmaker's Cave (3,3)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Apparition, "Apparition", SpellType.Cleric, 1, 1, 1, false, false, false, 0, SpellWhen.CombatAnywhere, SpellTarget.TenMonsters, "Decreace chance to hit", "Creates a frightening apparition in the monsters memory causing them to be afraid, reducing their chance to hit.", "C-2, Middlegate (7,7)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.AwakenCleric, "Awaken", SpellType.Cleric, 1, 2, 1, false, false, false, 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Remove asleep condition", "Awakens all sleeping members of the party, instantaneously cancelling the sleep condition. May be critical if party is attacked during rest.", "C-2, Middlegate (7,7)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Bless, "Bless", SpellType.Cleric, 1, 3, 1, false, false, false, 0, SpellWhen.CombatAnywhere, SpellTarget.Party, "Increase accuracy", "Increases the accuracy with which all characters fight, for the duration of combat.", "Cleric L1, Paladin L7"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.FirstAid, "First Aid", SpellType.Cleric, 1, 4, 1, false, false, false, 0, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Heal 8 HP", "Heals minor battle wounds, restoring 8 Hit Points to that character.", "Cleric L1, Paladin L7"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.LightCleric, "Light", SpellType.Cleric, 1, 5, 1, false, false, false, 0, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Create 1 light factor", "Gives the party 1 light factor, which is sufficient to light up 1 dark area. Multiple light spells can be cast to accumulate multiple light factors.", "Cleric L1, Paladin L7"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.PowerCure, "Power Cure", SpellType.Cleric, 1, 6, 1, true, false, false, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Heal 1-10 HP/level", "Restores character's health and 1-10 Hit Points per experience level of caster.", "C-2, Middlegate (7,7)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.TurnUndead, "Turn Undead", SpellType.Cleric, 1, 7, 1, false, false, false, 0, SpellWhen.CombatAnywhere, SpellTarget.AllUndeadMonsters, "Chance to destroy undead monsters", "Destroys some or all undead monsters, depending on caster's experience level and monster's power level.", "Cleric L1, Paladin L7"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.CureWounds, "Cure Wounds", SpellType.Cleric, 2, 1, 2, false, false, false, 0, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Heal 15 HP", "Cures more serious wounds, restoring 15 Hit Points to the character.", "Cleric L3, Paladin L9"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Heroism, "Heroism", SpellType.Cleric, 2, 2, 2, false, false, false, 1, SpellWhen.CombatAnywhere, SpellTarget.Character, "Add 6 levels", "Temporarily elevates a character 6 levels of experience. Spell lasts for the duration of combat.", "E-4, Sandsobar (6,11)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.NaturesGate, "Nature's Gate", SpellType.Cleric, 2, 3, 2, false, false, false, 0, SpellWhen.OutdoorNonCombat, SpellTarget.Party, "Teleport to another time and place", "Using the forces of nature, opens a portal between two locations in the land of Cron. These locations vary with time (days/years).", "C-3, Surface (1,9)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Pain, "Pain", SpellType.Cleric, 2, 4, 2, false, false, false, 0, SpellWhen.CombatAnywhere, SpellTarget.NonUndeadMonster, "2-16 physical damage", "Cripples monster with pain, inflicting 2-16 damage points, unless the monster is immune to pain.", "Cleric L3, Paladin L9"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.ProtectionFromElements, "Protection From Elements", SpellType.Cleric, 2, 5, 2, false, false, false, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party, "Add 20% + 1%/level elemental resistance", "Increases all character's resistance to fear, cold, fire, poison, acid and electricity. Amount of the increase depends on the caster's experience level. Spell lasts 1 day.", "E-4, Sandsobar (6,11)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Silence, "Silence", SpellType.Cleric, 2, 6, 2, false, false, false, 0, SpellWhen.CombatAnywhere, SpellTarget.FourMonstersPlusOnePerLevel, "Prevent casting of spells", "Prevents the monsters from casting spells for the duration of combat, or until they overcome the spell.", "Cleric L3, Paladin L9"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Weaken, "Weaken", SpellType.Cleric, 2, 7, 2, false, false, false, 1, SpellWhen.CombatAnywhere, SpellTarget.TenMonsters, "-50% physical damage", "Weakens all monsters affected, reducing their physical damage by half until the spell is overcome.", "E-4, Sandsobar (6,11)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.ColdRay, "Cold Ray", SpellType.Cleric, 3, 1, 3, false, false, false, 2, SpellWhen.CombatAnywhere, SpellTarget.FiveRangedMonsters, "25 cold damage", "Attacks with a ray of intensive cold that penetrates to the monsters heart and inflicts 25 points of damage to each monster affected.", "A-1, Tundara (11,13)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.CreateFood, "Create Food", SpellType.Cleric, 3, 2, 3, false, false, false, 2, SpellWhen.NonCombatAnywhere, SpellTarget.Caster, "Add 8 food", "Adds 8 food units to caster's food supply. Caster may then distribute food among other party members, if he/she desires.", "Cleric L5, Paladin L11"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.CurePoison, "Cure Poison", SpellType.Cleric, 3, 3, 3, false, false, false, 0, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Remove poisoned condition", "Flushes poison out of a character's system, instantaneously removing the poisoned condition.", "Cleric L5, Paladin L11"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Immobilize, "Immobilize", SpellType.Cleric, 3, 4, 3, false, false, false, 0, SpellWhen.CombatAnywhere, SpellTarget.FiveMonsters, "Prevent action", "Immobilizes any monster affected.", "Cleric L5, Paladin L11"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.LastingLight, "Lasting Light", SpellType.Cleric, 3, 5, 3, false, false, false, 0, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Create 20 light factors", "Bestows 20 light factors on the party, for use in dispelling darkness.", "A-1, Tundara (11,13)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.WalkonWater, "Walk on Water", SpellType.Cleric, 3, 6, 3, false, false, false, 2, SpellWhen.OutdoorNonCombat, SpellTarget.Party, "Allow passage through water", "Creates a floating sand dune upon which the party may walk on. Lasts 1 day.", "C-2, Surface (11,1)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.AcidSpray, "Acid Spray", SpellType.Cleric, 4, 1, 4, false, false, false, 3, SpellWhen.CombatAnywhere, SpellTarget.ThreeRangedMonsters, "6-60 acid damage", "Sprays a corrosive stream of acid inflicting 6-60 points of damage, unless immune to acid.", "Cleric L7, Paladin L13"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.AirTransmutation, "Air Transmutation", SpellType.Cleric, 4, 2, 4, false, false, false, 3, SpellWhen.OutdoorNonCombat, SpellTarget.Party, "Allow access to air elemental plane", "Transforms the party into air, allowing the exploration of the elemental plane of air.", "A-1, Surface (8,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.CureDisease, "Cure Disease", SpellType.Cleric, 4, 3, 4, false, false, false, 0, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Remove diseased condition", "Restores full health to sick character, instantaneously removing the diseased condition.", "Cleric L7, Paladin L13"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.RestoreAlignment, "Restore Alignment", SpellType.Cleric, 4, 4, 4, false, false, false, 3, SpellWhen.NonCombatAnywhere, SpellTarget.Character, "Restores original alignment", "Restores a character's original alignment, after actions and responses have caused it to shift.", "A-1, Tundara (11,13)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Surface, "Surface", SpellType.Cleric, 4, 5, 4, false, false, false, 0, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Teleport to current surface map", "Instantly transports all party members from an underground location to grounds surface.", "Cleric L7, Paladin L13"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.HolyBonus, "Holy Bonus", SpellType.Cleric, 4, 6, 4, false, false, false, 3, SpellWhen.CombatAnywhere, SpellTarget.Party, "+1 damage per 2 levels", "The generous forces of the cleric's deity increase the damage done by party members by 1 point per 2 levels of the caster.", "E-1, Vulcania (11,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.AirEncasement, "Air Encasement", SpellType.Cleric, 5, 1, 5, false, false, false, 5, SpellWhen.CombatAnywhere, SpellTarget.Monster, "10 sleep damage/round and prevent action", "Encases the target in a field of air, inflicting 10 points of damage per combat round and separating it from the battle until the spell is overcome or the monster is attacked.", "A-1, Surface (1,14)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.DeadlySwarm, "Deadly Swarm", SpellType.Cleric, 5, 2, 5, false, false, false, 5, SpellWhen.CombatAnywhere, SpellTarget.TenMonsters, "4-40 physical damage", "Sends a swarm of killer insects against the monsters, inflicting 4-40 damage points against each monster.", "Cleric L9, Paladin L15"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Frenzy, "Frenzy", SpellType.Cleric, 5, 3, 5, false, false, false, 5, SpellWhen.CombatAnywhere, SpellTarget.CharacterOnce, "Attacks all monsters, -1 end, unconscious", "Sends one party member into a frenzy, allowing him/her to attack all the monsters on the screen. Drained from the experience, the character loses 1 point of endurance and is then rendered unconscious.", "B-4, Surface (8,1)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Paralyze, "Paralyze", SpellType.Cleric, 5, 4, 5, false, false, false, 5, SpellWhen.CombatAnywhere, SpellTarget.TenMonsters, "Prevent action", "Attempts to immobilize all monsters and prevent them from fighting. May be partially or completely effective on some or all monsters.", "Cleric L9, Paladin L15"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.RemoveCondition, "Remove Condition", SpellType.Cleric, 5, 5, 5, false, false, false, 5, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Remove conditions except dead/stoned/erad ", "Releases character from all undesirable conditions except dead, stoned or eradicated.", "E-1, Vulcania (11,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.EarthTransmutatuion, "Earth Transmutatuion", SpellType.Cleric, 6, 1, 6, false, false, false, 6, SpellWhen.OutdoorNonCombat, SpellTarget.Party, "Allow access to earth elemental plane", "Transforms the party into earth, allowing the exploration of the elemental plane of earth.", "E-4, Surface (8,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Rejuvenate, "Rejuvenate", SpellType.Cleric, 6, 2, 6, false, false, false, 6, SpellWhen.NonCombatAnywhere, SpellTarget.Character, "Subtract 1-10 years from age", "A fountain of youth that trims 1-10 years off a character's age, restoring his/her abilities to the younger level. Spell carries some risk of producing the opposite effect.", "Cleric L11, Paladin L17"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.StonetoFlesh, "Stone to Flesh", SpellType.Cleric, 6, 3, 6, false, false, false, 6, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Remove stone condition", "Re-animates a character who has been turned to stone, removing the stoned condition.", "Cleric L11, Paladin L17"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.WaterEncasement, "Water Encasement", SpellType.Cleric, 6, 4, 6, false, false, false, 6, SpellWhen.CombatAnywhere, SpellTarget.Monster, "20 acid damage/round and prevent action", "Encases the target in a field of water, inflicting 20 points of damage per combat round and separating it from the battle until the spell is overcome or the monster is attacked.", "A-4, Surface (1,1)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.WaterTransmutation, "Water Transmutation", SpellType.Cleric, 6, 5, 6, false, false, false, 6, SpellWhen.OutdoorNonCombat, SpellTarget.Party, "Allow access to water elemental plane", "Transforms the party into water, allowing the exploration of the elemental plane of water.", "A-4, Surface (8,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.EarthEncasement, "Earth Encasement", SpellType.Cleric, 7, 1, 7, false, false, false, 7, SpellWhen.CombatAnywhere, SpellTarget.Monster, "40 cold damage/round and prevent action", "Encases the target in a field of earth, inflicting 40 points of damage per combat round and separating it from the battle until the spell is overcome or the monster is attacked.", "E-4, Surface (14,1)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.FieryFlail, "Fiery Flail", SpellType.Cleric, 7, 2, 7, false, false, false, 7, SpellWhen.CombatAnywhere, SpellTarget.Monster, "100-400 fire damage", "Creates a huge flail of fire, striking a single opponent, inflicting 100-400 points of damage.", "E-1, Vulcania (11,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.MoonRay, "Moon Ray", SpellType.Cleric, 7, 3, 7, false, false, false, 7, SpellWhen.OutdoorCombat, SpellTarget.TenMonsters, "Heal 10-100 HP, 10-100 physical damage", "Bathes all combatants in a beneficent ray that bestows 10-100 Hit Points on each character and removes 10-100 Hit Points from each monster.", "Cleric L13, Paladin L19"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.RaiseDead, "Raise Dead", SpellType.Cleric, 7, 4, 7, false, false, false, 7, SpellWhen.AnywhereAnytime, SpellTarget.Character, "Remove dead condition, +1 age to both", "Brings the character back to life, removing the dead condition. Spell carries a moderate chance of failure and a remote chance of eradicating the character. (Note: Spell-caster and recipient age by 1 year.)", "Cleric L11, Paladin L17"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.FireEncasement, "Fire Encasement", SpellType.Cleric, 8, 1, 8, false, false, false, 8, SpellWhen.CombatAnywhere, SpellTarget.Monster, "80 fire damage/round and prevent action", "Encases the target in a field of fire, inflicting 80 points of damage per combat round and separating it from the battle until the spell is overcome or the monster is attacked.", "E-1, Surface (14,14)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.FireTransmutation, "Fire Transmutation", SpellType.Cleric, 8, 2, 8, false, false, false, 8, SpellWhen.OutdoorNonCombat, SpellTarget.Party, "Allow access to fire elemental plane", "Transforms the party into fire, allowing the exploration of the elemental plane of fire.", "E-1, Surface (8,8)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.MassDistortion, "Mass Distortion", SpellType.Cleric, 8, 3, 8, false, false, false, 8, SpellWhen.CombatAnywhere, SpellTarget.TwoMonsters, "50% damage", "Increases the weight of monsters causing them to fall and subsequently lose half their hit points.", "A-4, Atlantium (4,7)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.TownPortal, "Town Portal", SpellType.Cleric, 8, 4, 8, false, false, false, 8, SpellWhen.NonCombatAnywhere, SpellTarget.Party, "Teleport to town", "Opens a temporary portal to any town and moves the party through the portal to that town.", "Cleric L13"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.DivineIntervention, "Divine Intervention", SpellType.Cleric, 9, 1, 10, false, false, false, 20, SpellWhen.CombatAnywhere, SpellTarget.Party, "Cure/Heal all except erad, +5 caster age", "Intercedes with supernatural forces to restore all characters' Hit Points and remove all undesirable conditions, except eradicated. (Note: Spell-caster ages 5 years every time this spell is cast.)", "C-3, Druid's Cave (15,14)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.HolyWord, "Holy Word", SpellType.Cleric, 9, 2, 10, false, false, false, 10, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, "Destroy all undead monsters", "Utters a single word of devastating power, that destroys all undead monsters. (Note: Ages caster 1 year.) [does not actually appear to age the caster in the PC version]", "C-1, Surface (5,5)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.Resurrection, "Resurrection", SpellType.Cleric, 9, 3, 10, false, false, false, 10, SpellWhen.NonCombatAnywhere, SpellTarget.Character, "Remove erad, +5 age, -1 end, +1 caster age", "Removes the eradicated condition from the character, adding 5 years to his/her age and subtracting 1 endurance point from his/her vital statistics. There is a chance that the spell will fail. (Note: Ages caster 1 year.)", "A-4, Atlantium (4,7)"));
            m_spells.Add(new MM2Spell(MM2SpellIndex.UncurseItem, "Uncurse Item", SpellType.Cleric, 9, 4, 10, false, false, false, 50, SpellWhen.NonCombatAnywhere, SpellTarget.Caster, "Remove curse from item in backpack", "Attempts to remove the curse from an item in casters backpack.", "A-4, Atlantium (4,7)"));
        }
    }

    public class MM2SpellCost : MMSpellCost
    {
        public bool PlusOnePerMonster;
        public bool PlusFiftyPerBonus;

        public MM2SpellCost(int sp, bool perlevel, bool permonster, bool perbonus, int gems)
        {
            SpellPoints = sp;
            PerLevel = perlevel;
            Gems = gems;
            PlusFiftyPerBonus = perbonus;
            PlusOnePerMonster = permonster;
        }

        public override string ShortString
        {
            get
            {
                if (PlusFiftyPerBonus)
                    return String.Format("50 SP per plus of the item + {0} Gems", Gems);

                return String.Format("{0} SP{1}{2}{3}",
                    SpellPoints,
                    PerLevel ? " per level of the caster" : "",
                    PlusOnePerMonster ? " + 1 per monster" : "",
                    Gems > 0 ? String.Format(" + {0} Gem{1}", Gems, Gems == 1 ? "" : "s") : "");
            }
        }

        public override string ToString()
        {
            if (PlusFiftyPerBonus)
                return String.Format("50/plus+{0}G", Gems);

            return String.Format("{0}{1}{2}{3}",
                SpellPoints,
                PerLevel ? "/lv" : "",
                PlusOnePerMonster ? "+1/M" : "",
                Gems > 0 ? String.Format("+{0}G", Gems) : "");
        }
    }

    public class MM2Spell : MMSpell
    {
        public MM2SpellIndex Index;
        public override int BasicIndex { get { return (int)Index; } }

        public MM2Spell(MM2SpellIndex index, string name, SpellType type, int level, int number, int sp, bool perlevel,
            bool permonster, bool perbonus, int gems, SpellWhen when, SpellTarget target, string shortDesc, string desc, string location)
        {
            Index = index;
            Name = name;
            Type = type;
            Level = level;
            Number = number;
            Cost = new MM2SpellCost(sp, perlevel, permonster, perbonus, gems);
            When = when;
            Target = target;
            ShortDescription = shortDesc;
            Description = desc;
            Learned = location;
        }

        public override Keys[] GetKeys()
        {
            return new Keys[] { Keys.D0 + Level, Keys.D0 + Number, Keys.None, Keys.Enter };
        }

        public override MMSpell None
        {
            get { return new MM2Spell(MM2SpellIndex.None, "None", SpellType.Unknown, 0, 0, 0, false, false, false, 0, SpellWhen.None, SpellTarget.Unknown, "", "", ""); }
        }
    }
}
