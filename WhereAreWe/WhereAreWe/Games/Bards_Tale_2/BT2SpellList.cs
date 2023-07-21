using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public enum BT2SpellIndex
    {
        None = 0,
        MageFlame,                      // Conjurer L1 (MAFL)    # 1 
        ArcFire,                        // Conjurer L1 (ARFI)    # 2 
        TrapZap,                        // Conjurer L1 (TRZP)    # 3 
        Freeze,                         // Conjurer L2 (FRFO)    # 4 
        KielsMagicCompass,              // Conjurer L2 (MACO)    # 5 
        WordOfHealing,                  // Conjurer L2 (WOHL)    # 6 
        LesserRevelation,               // Conjurer L3 (LERE)    # 7 
        Levitation,                     // Conjurer L3 (LEVI)    # 8 
        Warstrike,                      // Conjurer L3 (WAST)    # 9 
        EliksInstantWolf,               // Conjurer L4 (INWO)    # 10
        FleshRestore,                   // Conjurer L4 (FLRE)    # 11
        GreaterRevelation,              // Conjurer L5 (GRRE)    # 12
        Shocksphere,                    // Conjurer L5 (SHSP)    # 13
        EliksInstantOgre,               // Conjurer L6 (INOG)    # 14
        MajorLevitation,                // Conjurer L6 (MALE)    # 15
        FleshAnew,                      // Conjurer L7 (FLAN)    # 16
        ApportArcane,                   // Conjurer L7 (APAR)    # 17
        FarFoe,                         // Conjurer L7 (FAFO)    # 18
        EliksInstantSlayer,             // Conjurer L7 (INSL)    # 19

        VorpalPlating,                  // Magician L1 (VOPL)    # 20
        QuickFix,                       // Magician L1 (QUFI)    # 21
        ScrySite,                       // Magician L1 (SCSI)    # 22
        HolyWater,                      // Magician L2 (HOWA)    # 23
        MageGauntlets,                  // Magician L2 (MAGA)    # 24
        AreaEnchant,                    // Magician L2 (AREN)    # 25
        YbarrasMysticShield,            // Magician L3 (MYSH)    # 26
        OsconsOgrestrength,             // Magician L3 (OGST)    # 27
        Starflare,                      // Magician L3 (STFL)    # 28
        SpectreTouch,                   // Magician L4 (SPTO)    # 29
        DragonBreath,                   // Magician L4 (DRBR)    # 30
        Antimagic,                      // Magician L5 (ANMA)    # 31
        StoneTouch,                     // Magician L5 (STTO)    # 32
        PhaseDoor,                      // Magician L6 (PHDO)    # 33
        YbarrasMysticalCoatOfArmor,     // Magician L6 (YMCA)    # 34
        Restoration,                    // Magician L7 (REST)    # 35
        Deathstrike,                    // Magician L7 (DEST)    # 36
        WizardWall,                     // Magician L7 (WZWA)    # 37
        SafetySpell,                    // Magician L7 (SASP)    # 38

        MangarsMindJab,                 // Sorcerer L1 (MIJA)    # 39
        PhaseBlur,                      // Sorcerer L1 (PHBL)    # 40
        LocateTraps,                    // Sorcerer L1 (LOTR)    # 41
        Disbelieve,                     // Sorcerer L2 (DISB)    # 42
        WindWarrior,                    // Sorcerer L2 (WIWA)    # 43
        WordOfFear,                     // Sorcerer L2 (FEAR)    # 44
        WindOgre,                       // Sorcerer L3 (WIOG)    # 45
        KylearansInvisibilitySpell,     // Sorcerer L3 (INVI)    # 46
        SecondSight,                    // Sorcerer L3 (SESI)    # 47
        CatEyes,                        // Sorcerer L4 (CAEY)    # 48
        WindDragon,                     // Sorcerer L4 (WIDR)    # 49
        DisruptIllusion,                // Sorcerer L5 (DIIL)    # 50
        MangarsMindBlade,               // Sorcerer L5 (MIBL)    # 51
        WindGiant,                      // Sorcerer L6 (WIGI)    # 52
        SorcererSight,                  // Sorcerer L6 (SOSI)    # 53
        WindMage,                       // Sorcerer L7 (WIMA)    # 54
        WindHero,                       // Sorcerer L7 (WIHE)    # 55
        MageMaelstrom,                  // Sorcerer L7 (MAGM)    # 56
        Dreamspell,                     // Sorcerer L7 (ZZGO)    # 57

        SummonElemental,                // Wizard L1 (SUEL)      # 58
        FanskarsForceFocus,             // Wizard L1 (FOFO)      # 59
        Gate,                           // Wizard L2 (GATE)      # 60
        DemonBane,                      // Wizard L2 (DEBA)      # 61
        FlameColumn,                    // Wizard L3 (FLCO)      # 62
        Dispossess,                     // Wizard L3 (DISP)      # 63
        PrimeSummoning,                 // Wizard L4 (PRSU)      # 64
        AnimateDead,                    // Wizard L4 (ANDE)      # 65
        BaylorsSpellBind,               // Wizard L5 (SPBI)      # 66
        StoralsSoulWhip,                // Wizard L5 (SOWH)      # 67
        GreaterSummoning,               // Wizard L6 (GRSU)      # 68
        BeyondDeath,                    // Wizard L6 (BEDE)      # 69
        WacumsWizardWar,                // Wizard L7 (WIZW)      # 70
        SummonHerb,                     // Wizard L7 (HERB)      # 71

        OsconsHaltfoe,                  // Archmage L1 (HAFO)    # 72
        MeleeMen,                       // Archmage L1 (MEME)    # 73
        Batchspell,                     // Archmage L2 (BASP)    # 74
        Camaraderie,                    // Archmage L3 (CAMR)    # 75
        FanskarsNightLance,             // Archmage L4 (NILA)    # 76
        HealAll,                        // Archmage L5 (HEAL)    # 77
        TheBrothersKringle,             // Archmage L6 (BRKR)    # 78
        MangarsMallet,                  // Archmage L7 (MAMA)    # 79

        ArchersTune,                    // Bard                  # 80
        Spellsong,                      // Bard                  # 81
        SanctuaryScore,                 // Bard                  # 82
        MeleeMarch,                     // Bard                  # 83
        ZanduvarCarack,                 // Bard                  # 84
        RhymeOfDuotime,                 // Bard                  # 85
        WatchwoodMelody,                // Bard                  # 86

        Last
    }

    public class BT2SpellList : SpellList
    {
        List<BT2Spell> m_spells;

        public override Spell GetSpell(int index) { return index < 0 || index >= m_spells.Count ? null : m_spells[index]; }

        public static BT2SpellIndex[] Conjurer =
        {
            BT2SpellIndex.MageFlame,                      // Conjurer L1 (MAFL)   # 1 
            BT2SpellIndex.ArcFire,                        // Conjurer L1 (ARFI)   # 2 
            BT2SpellIndex.TrapZap,                        // Conjurer L1 (TRZP)   # 3 
            BT2SpellIndex.Freeze,                         // Conjurer L2 (FRFO)   # 4 
            BT2SpellIndex.KielsMagicCompass,              // Conjurer L2 (MACO)   # 5 
            BT2SpellIndex.WordOfHealing,                  // Conjurer L2 (WOHL)   # 6 
            BT2SpellIndex.LesserRevelation,               // Conjurer L3 (LERE)   # 7 
            BT2SpellIndex.Levitation,                     // Conjurer L3 (LEVI)   # 8 
            BT2SpellIndex.Warstrike,                      // Conjurer L3 (WAST)   # 9 
            BT2SpellIndex.EliksInstantWolf,               // Conjurer L4 (INWO)   # 10
            BT2SpellIndex.FleshRestore,                   // Conjurer L4 (FLRE)   # 11
            BT2SpellIndex.GreaterRevelation,              // Conjurer L5 (GRRE)   # 12
            BT2SpellIndex.Shocksphere,                    // Conjurer L5 (SHSP)   # 13
            BT2SpellIndex.EliksInstantOgre,               // Conjurer L6 (INOG)   # 14
            BT2SpellIndex.MajorLevitation,                // Conjurer L6 (MALE)   # 15
            BT2SpellIndex.FleshAnew,                      // Conjurer L7 (FLAN)   # 16
            BT2SpellIndex.ApportArcane,                   // Conjurer L7 (APAR)   # 17
            BT2SpellIndex.FarFoe,                         // Conjurer L7 (FAFO)   # 18
            BT2SpellIndex.EliksInstantSlayer,             // Conjurer L7 (INSL)   # 19
        };

        public static BT2SpellIndex[] Magician =
        {
            BT2SpellIndex.VorpalPlating,                  // Magician L1 (VOPL)   # 20
            BT2SpellIndex.QuickFix,                       // Magician L1 (QUFI)   # 21
            BT2SpellIndex.ScrySite,                       // Magician L1 (SCSI)   # 22
            BT2SpellIndex.HolyWater,                      // Magician L2 (HOWA)   # 23
            BT2SpellIndex.MageGauntlets,                  // Magician L2 (MAGA)   # 24
            BT2SpellIndex.AreaEnchant,                    // Magician L2 (AREN)   # 25
            BT2SpellIndex.YbarrasMysticShield,            // Magician L3 (MYSH)   # 26
            BT2SpellIndex.OsconsOgrestrength,             // Magician L3 (OGST)   # 27
            BT2SpellIndex.Starflare,                      // Magician L3 (STFL)   # 28
            BT2SpellIndex.SpectreTouch,                   // Magician L4 (SPTO)   # 29
            BT2SpellIndex.DragonBreath,                   // Magician L4 (DRBR)   # 30
            BT2SpellIndex.Antimagic,                      // Magician L5 (ANMA)   # 31
            BT2SpellIndex.StoneTouch,                     // Magician L5 (STTO)   # 32
            BT2SpellIndex.PhaseDoor,                      // Magician L6 (PHDO)   # 33
            BT2SpellIndex.YbarrasMysticalCoatOfArmor,     // Magician L6 (YMCA)   # 34
            BT2SpellIndex.Restoration,                    // Magician L7 (REST)   # 35
            BT2SpellIndex.Deathstrike,                    // Magician L7 (DEST)   # 36
            BT2SpellIndex.WizardWall,                     // Magician L7 (WZWA)   # 37
            BT2SpellIndex.SafetySpell,                    // Magician L7 (SASP)   # 38
        };

        public static BT2SpellIndex[] Sorcerer =
        {
            BT2SpellIndex.MangarsMindJab,                 // Sorcerer L1 (MIJA)    # 39
            BT2SpellIndex.PhaseBlur,                      // Sorcerer L1 (PHBL)    # 40
            BT2SpellIndex.LocateTraps,                    // Sorcerer L1 (LOTR)    # 41
            BT2SpellIndex.Disbelieve,                     // Sorcerer L2 (DISB)    # 42
            BT2SpellIndex.WindWarrior,                    // Sorcerer L2 (WIWA)    # 43
            BT2SpellIndex.WordOfFear,                     // Sorcerer L2 (FEAR)    # 44
            BT2SpellIndex.WindOgre,                       // Sorcerer L3 (WIOG)    # 45
            BT2SpellIndex.KylearansInvisibilitySpell,     // Sorcerer L3 (INVI)    # 46
            BT2SpellIndex.SecondSight,                    // Sorcerer L3 (SESI)    # 47
            BT2SpellIndex.CatEyes,                        // Sorcerer L4 (CAEY)    # 48
            BT2SpellIndex.WindDragon,                     // Sorcerer L4 (WIDR)    # 49
            BT2SpellIndex.DisruptIllusion,                // Sorcerer L5 (DIIL)    # 50
            BT2SpellIndex.MangarsMindBlade,               // Sorcerer L5 (MIBL)    # 51
            BT2SpellIndex.WindGiant,                      // Sorcerer L6 (WIGI)    # 52
            BT2SpellIndex.SorcererSight,                  // Sorcerer L6 (SOSI)    # 53
            BT2SpellIndex.WindMage,                       // Sorcerer L7 (WIMA)    # 54
            BT2SpellIndex.WindHero,                       // Sorcerer L7 (WIHE)    # 55
            BT2SpellIndex.MageMaelstrom,                  // Sorcerer L7 (MAGM)    # 56
            BT2SpellIndex.Dreamspell,                     // Sorcerer L7 (ZZGO)    # 57

        };

        public static BT2SpellIndex[] Wizard =
        {
            BT2SpellIndex.SummonElemental,                // Wizard L1 (SUEL)      # 58
            BT2SpellIndex.FanskarsForceFocus,             // Wizard L1 (FOFO)      # 59
            BT2SpellIndex.Gate,                           // Wizard L2 (GATE)      # 60
            BT2SpellIndex.DemonBane,                      // Wizard L2 (DEBA)      # 61
            BT2SpellIndex.FlameColumn,                    // Wizard L3 (FLCO)      # 62
            BT2SpellIndex.Dispossess,                     // Wizard L3 (DISP)      # 63
            BT2SpellIndex.PrimeSummoning,                 // Wizard L4 (PRSU)      # 64
            BT2SpellIndex.AnimateDead,                    // Wizard L4 (ANDE)      # 65
            BT2SpellIndex.BaylorsSpellBind,               // Wizard L5 (SPBI)      # 66
            BT2SpellIndex.StoralsSoulWhip,                // Wizard L5 (SOWH)      # 67
            BT2SpellIndex.GreaterSummoning,               // Wizard L6 (GRSU)      # 68
            BT2SpellIndex.BeyondDeath,                    // Wizard L6 (BEDE)      # 69
            BT2SpellIndex.WacumsWizardWar,                // Wizard L7 (WIZW)      # 70
            BT2SpellIndex.SummonHerb,                     // Wizard L7 (HERB)      # 71
        };

        public static BT2SpellIndex[] Archmage =
        {
            BT2SpellIndex.OsconsHaltfoe,                  // Archmage L1 (HAFO)    # 72
            BT2SpellIndex.MeleeMen,                       // Archmage L1 (MEME)    # 73
            BT2SpellIndex.Batchspell,                     // Archmage L2 (BASP)    # 74
            BT2SpellIndex.Camaraderie,                    // Archmage L3 (CAMR)    # 75
            BT2SpellIndex.FanskarsNightLance,             // Archmage L4 (NILA)    # 76
            BT2SpellIndex.HealAll,                        // Archmage L5 (HEAL)    # 77
            BT2SpellIndex.TheBrothersKringle,             // Archmage L6 (BRKR)    # 78
            BT2SpellIndex.MangarsMallet,                  // Archmage L7 (MAMA)    # 79
        };

        public static BT2SpellIndex[] Bard =
        {
            BT2SpellIndex.ArchersTune,                    // Bard                  # 80
            BT2SpellIndex.Spellsong,                      // Bard                  # 81
            BT2SpellIndex.SanctuaryScore,                 // Bard                  # 82
            BT2SpellIndex.MeleeMarch,                     // Bard                  # 83
            BT2SpellIndex.ZanduvarCarack,                 // Bard                  # 84
            BT2SpellIndex.RhymeOfDuotime,                 // Bard                  # 85
            BT2SpellIndex.WatchwoodMelody,                // Bard                  # 86
        };

        public List<BT2Spell> Spells
        {
            get { return m_spells; }
        }

        public static BT2Spell[] HealingSpells = new BT2Spell[]
        {
            BT2.Spells[(int) BT2SpellIndex.WordOfHealing],
            BT2.Spells[(int) BT2SpellIndex.FleshRestore],
            BT2.Spells[(int) BT2SpellIndex.FleshAnew],
            BT2.Spells[(int) BT2SpellIndex.Restoration],
            BT2.Spells[(int) BT2SpellIndex.Dispossess],
            BT2.Spells[(int) BT2SpellIndex.BeyondDeath],
            BT2.Spells[(int) BT2SpellIndex.HealAll],
        };

        public BT2SpellList()
        {
            m_spells = new List<BT2Spell>(88);
            m_spells.Add(new BT2Spell(BT2SpellIndex.MageFlame, SpellType.Conjurer, 1, "MAFL", "Mage Flame", 2, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Medium, "Light (3 squares)", "A small self-propelled \"torch\" appears and floats above the spellcaster as he travels (10-14 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.ArcFire, SpellType.Conjurer, 1, "ARFI", "Arc Fire", 3, SpellWhen.CombatAnywhere, SpellTarget.Monster10Feet, SpellDuration.Instant, "1d4 damage/level", "A fan of blue flame jets from the spellcaster's fingers, inflicting 1 to 4 hits of damage, which are multiplied by the spellcaster's level, on the selected opponent."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.TrapZap, SpellType.Conjurer, 1, "TRZP", "Trap Zap", 2, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Instant, "Disarm traps", "Disarms any trap within 30 feet (3 squares), in the direction the spellcaster is facing. TRZP also works on chests, but still costs the same amount of spell points."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Freeze, SpellType.Conjurer, 2, "FRFO", "Freeze", 3, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Combat, "+1 AC", "Foes binds your enemies in magical force, slowing them down and making them easier to hit."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.KielsMagicCompass, SpellType.Conjurer, 2, "MACO", "Kiel's Magic Compass", 3, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Medium, "Show direction", "A compass of shimmering magelight appears above the party and shows the direction they face (10-14 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.WordOfHealing, SpellType.Conjurer, 2, "WOHL", "Word Of Healing", 4, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "+4d4 HP", "Lets the spellcaster heal a party member who suffers from 4 to 16 points of damage by uttering a single word."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.LesserRevelation, SpellType.Conjurer, 3, "LERE", "Lesser Revelation", 5, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Long, "Light (4 squares); Reveal secrets", "An extended MAGE FLAME spell that also reveals secret doors (15-19 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Levitation, SpellType.Conjurer, 3, "LEVI", "Levitation", 4, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Float over traps and up portals", "Partially nullifies gravity causing the party to float over traps, or up or down through portals (5-9 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Warstrike, SpellType.Conjurer, 3, "WAST", "Warstrike", 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup20Feet, SpellDuration.Instant, "4d5 damage", "An energy strewn shot from the spellcaster's finger that sizzles a group of Foes for 5 to 20 hits of damage."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.EliksInstantWolf, SpellType.Conjurer, 4, "INWO", "Elik's Instant Wolf", 6, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Wolf", "Summons a giant, extremely fierce wolf to join your party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.FleshRestore, SpellType.Conjurer, 4, "FLRE", "Flesh Restore", 6, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "+10d4 HP; Cure poison/nuts", "A powerful healing spell that restores 10 to 40 hit points to a party member, including those stricken with insanity or poisoning."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.GreaterRevelation, SpellType.Conjurer, 5, "GRRE", "Greater Revelation", 7, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Long, "Light (5 squares); Reveal secrets", "Operates like LESSER REVELATION, but illuminates a wider area for a longer period of time (20-24 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Shocksphere, SpellType.Conjurer, 5, "SHSP", "Shock-sphere", 7, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup30Feet, SpellDuration.Instant, "10d4 damage", "Creates a large globe of intense electrical energy that envelops a group of enemies and inflicts 10 to 40 hits of damage."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.EliksInstantOgre, SpellType.Conjurer, 6, "INOG", "Elik's Instant Ogre", 9, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Ogre", "Materializes the biggest, meanest ogre you've ever met to ally with your party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.MajorLevitation, SpellType.Conjurer, 6, "MALE", "Major Levitation", 8, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "Float over traps and up portals", "Operates like LEVI from level 3, but it lasts until dispelled (i.e., until the spell is terminated by some event such as activating an anti-magic square)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.FleshAnew, SpellType.Conjurer, 7, "FLAN", "Flesh Anew", 12, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "+10d4 HP; Cure poison/nuts", "Operates like FLRE, but affects every member of the party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.ApportArcane, SpellType.Conjurer, 7, "APAR", "Apport Arcane", 15, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Change location", "Teleports the party within a dungeon to any location that's not protected by a teleportation shield. Also teleports the party between cities that are in the range of +1 to 6. Your party always arrives in the city's Adventurers' Guild."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.FarFoe, SpellType.Conjurer, 7, "FAFO", "Far Foe", 18, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "Move group 40' away", "Moves a group of Foes 40 feet further away from your party, up to a maximum distance of 90 feet."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.EliksInstantSlayer, SpellType.Conjurer, 7, "INSL", "Elik's Instant Slayer", 12, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Slayer", "Materializes a slayer that joins your party. What's a slayer? The name speaks for itself..."));

            m_spells.Add(new BT2Spell(BT2SpellIndex.VorpalPlating, SpellType.Magician, 1, "VOPL", "Vorpal Plating", 3, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Combat, "+2d4 damage", "Causes the weapon (or hands) of a party member to emit a magical field that inflicts 2 to 8 points of additional damage."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.QuickFix, SpellType.Magician, 1, "QUFI", "Quick Fix", 3, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "+8 HP", "Regenerates a character for precisely 8 hit points up to the character's maximum hit point level."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.ScrySite, SpellType.Magician, 1, "SCSI", "Scry Site", 2, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Show location", "Causes a dungeon or wilderness pathway to reveal the party's location."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.HolyWater, SpellType.Magician, 2, "HOWA", "Holy Water", 4, SpellWhen.CombatAnywhere, SpellTarget.Monster10Feet, SpellDuration.Instant, "6d4 damage to evil/supernatural", "Holy water sprays from the spellcaster's fingers, inflicting 6 to 24 points of damage on any foe of evil or supernatural origin."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.MageGauntlets, SpellType.Magician, 2, "MAGA", "Mage Gauntlets", 5, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Combat, "+4d4 damage", "Makes the hands (or weapon) of a party member more deadly by adding 4 to 16 points of damage to every wound it inflicts on a foe."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.AreaEnchant, SpellType.Magician, 2, "AREN", "Area Enchant", 5, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Short, "Locate stairways", "Causes the dungeon walls within 30 feet (3 squares) of a stairway to call out if the party is headed toward the stairs (5-9 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.YbarrasMysticShield, SpellType.Magician, 3, "MYSH", "Ybarra's Mystic Shield", 6, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Medium, "-2 AC", "Causes the air in front of the party to form an invisible shield that's as hard as metal and precedes the party as they move (10-14 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.OsconsOgrestrength, SpellType.Magician, 3, "OGST", "Oscon's Ogrestrength", 6, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Combat, "+7 To-Hit/Damage", "Endows a specific party member with the strength of Elik's ogre for the duration of the battle."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Starflare, SpellType.Magician, 3, "STFL", "Starflare", 6, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup40FeetDiminish, SpellDuration.Instant, "10d4 damage", "Ignites the air around your enemies, scorching them for 10 to 40 damage points."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.SpectreTouch, SpellType.Magician, 4, "SPTO", "Spectre Touch", 8, SpellWhen.CombatAnywhere, SpellTarget.Monster70Feet, SpellDuration.Instant, "15d4 damage", "Drains a single enemy of 15 to 60 hit points; like a touch from death itself."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.DragonBreath, SpellType.Magician, 4, "DRBR", "Dragon Breath", 7, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup30Feet, SpellDuration.Instant, "11d4 damage", "Lets the spellcaster breathe fire at a group of monsters, inflicting 11 to 44 points of damage on each monster."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Antimagic, SpellType.Magician, 5, "ANMA", "Anti-magic", 8, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Combat, "+2 Magic Resist", "Causes the ground to absorb a portion of the spells cast at the party by monsters. Often allows the party to escape unharmed. This spell also aids in disbelieving illusions and shielding against magical fire such as Dragon Breath."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.StoneTouch, SpellType.Magician, 5, "STTO", "Stone Touch", 8, SpellWhen.CombatAnywhere, SpellTarget.Monster10Feet, SpellDuration.Instant, "Cause \"Stone\" condition", "Usually turns an enemy to stone (except those already made of stone), instantly killing the enemy."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.PhaseDoor, SpellType.Magician, 6, "PHDO", "Phase Door", 9, SpellWhen.AnywhereAnytime, SpellTarget.OneWall, SpellDuration.OneMove, "Allow passage through wall", "Turns almost any wall to air for exactly one move."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.YbarrasMysticalCoatOfArmor, SpellType.Magician, 6, "YMCA", "Ybarra's Mystical Coat Of Armor", 10, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "-2 AC", "Operates like YBARRA'S MYSTIC SHIELD, but lasts indefinitely."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Restoration, SpellType.Magician, 7, "REST", "Restoration", 12, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Heal all HP; Cure poison/nuts", "Regenerates the body of every party member to perfect condition; it even cures insanity or poisoning."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Deathstrike, SpellType.Magician, 7, "DEST", "Deathstrike", 14, SpellWhen.CombatAnywhere, SpellTarget.Monster10Feet, SpellDuration.Instant, "Kill enemy", "Very likely to instantly kill one selected enemy."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.WizardWall, SpellType.Magician, 7, "WZWA", "Wizard Wall", 11, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Wall", "Creates a wall of force that travels with the party and absorbs many of the enemy's attacks."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.SafetySpell, SpellType.Magician, 7, "SASP", "Safety Spell", 30, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Teleport to Tangramayne", "Teleports your entire party to the Adventurers' Guild in Tangramayne, minus all gold. Use this spell only in dire emergencies because it is not 100% reliable."));

            m_spells.Add(new BT2Spell(BT2SpellIndex.MangarsMindJab, SpellType.Sorcerer, 1, "MIJA", "Mangar's Mind Jab", 3, SpellWhen.CombatAnywhere, SpellTarget.Monster40FeetDiminish, SpellDuration.Instant, "2d4 damage/level", "Casts a concentrated blast of energy at one opponent, inflicting 2 to 8 points of damage for each experience level of the spellcaster."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.PhaseBlur, SpellType.Sorcerer, 1, "PHBL", "Phase Blur", 2, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Combat, "-1 AC", "Causes the entire party to waver and blur in the sight of the enemy, rendering your party difficult to strike."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.LocateTraps, SpellType.Sorcerer, 1, "LOTR", "Locate Traps", 2, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Short, "Detect traps", "Heightens the spellcaster's awareness in order to detect traps within 30' along the direction the spellcaster is facing (5-9 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Disbelieve, SpellType.Sorcerer, 2, "DISB", "Disbelieve", 4, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Dispel illusions", "Reveals the true nature of any attacking illusion, causing it to vanish."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.WindWarrior, SpellType.Sorcerer, 2, "WIWA", "Wind Warrior", 5, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Mercenary Illusion", "Creates the illusion of a battle-ready ninja among the ranks of your party. The illusionary ninja will fight until defeated or disbelieved."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.WordOfFear, SpellType.Sorcerer, 2, "FEAR", "Word Of Fear", 4, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Combat, "-1 To-Hit/Damage", "An incantation that causes a group of enemies to quake in fear, thus reducing their ability to attack and inflict damage."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.WindOgre, SpellType.Sorcerer, 3, "WIOG", "Wind Ogre", 6, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Ogre Illusion", "Similar to ELIK'S OGRE, but the WIOG is an illusion."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.KylearansInvisibilitySpell, SpellType.Sorcerer, 3, "INVI", "Kylearan's Invisibility Spell", 6, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "-4 AC", "An invocation that renders the entire party nearly invisible to the enemy."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.SecondSight, SpellType.Sorcerer, 3, "SESI", "Second Sight", 6, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Medium, "Detect special areas", "Heightens the awareness of the spellcaster in order to detect all manner of traps and tricks that lie directly ahead (15-19 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.CatEyes, SpellType.Sorcerer, 4, "CAEY", "Cat Eyes", 7, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "Light (5 squares)", "Endows the entire party with perfect night vision for an indefinite period of time."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.WindDragon, SpellType.Sorcerer, 4, "WIDR", "Wind Dragon", 12, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Red Dragon Illusion", "Creates an illusionary red dragon to join the ranks of your party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.DisruptIllusion, SpellType.Sorcerer, 5, "DIIL", "Disrupt Illusion", 8, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.Combat, "Dispel illusions and dopplegangers", "Destroys any illusions among the ranks of the enemy and prevents new illusions from appearing. This spell also exposes any Dopplegangers within the party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.MangarsMindBlade, SpellType.Sorcerer, 5, "MIBL", "Mangar's Mind Blade", 10, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters30FeetDiminish, SpellDuration.Instant, "25d4 damage", "Strikes every opposing group within range with an explosion of energy capable of inflicting 25 to 100 points of damage."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.WindGiant, SpellType.Sorcerer, 6, "WIGI", "Wind Giant", 13, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Storm Giant Illusion", "Creates an illusionary storm giant that joins and fights for your party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.SorcererSight, SpellType.Sorcerer, 6, "SOSI", "Sorcerer Sight", 11, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Indefinite, "Detect special areas", "Operates like the SECOND SIGHT spell, but lasts indefinitely."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.WindMage, SpellType.Sorcerer, 7, "WIMA", "Wind Mage", 14, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Archmage Illusion", "Creates an illusionary Archmage to join your party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.WindHero, SpellType.Sorcerer, 7, "WIHE", "Wind Hero", 16, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Hero Illusion", "Creates an illusionary hero to join your party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.MageMaelstrom, SpellType.Sorcerer, 7, "MAGM", "Mage Maelstrom", 40, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "60d4 damage/stone/kill", "Assaults a group of spellcasters and may do one of the following: inflict 60 to 240 points of damage, turn them to stone, or kill them outright. However, because the maelstrom is illusionary in nature, a disbelieving monster can totally disarm it."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Dreamspell, SpellType.Sorcerer, 7, "ZZGO", "Dreamspell", 100, SpellWhen.AnywhereAnytime, SpellTarget.Unknown, SpellDuration.Instant, "Teleport/Various", "Known only as \"The Dreamspell,\" it is the subject of myth and speculation and no one knows this spell's code. Legend has it that this is a spell of such magnitude that it can actually rip the fabric of reality in half."));

            m_spells.Add(new BT2Spell(BT2SpellIndex.SummonElemental, SpellType.Wizard, 1, "SUEL", "Summon Elemental", 10, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Fire Elemental", "Creates a fire-being from the raw elements of the universe to join and fight for your party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.FanskarsForceFocus, SpellType.Wizard, 1, "FOFO", "Fanskar's Force Focus", 11, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup10Feet, SpellDuration.Instant, "24d4 damage", "Lands a cone of gravitational energy on a group of your Foes, inflicting 25 to 100 points of damage."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Gate, SpellType.Wizard, 2, "GATE", "Gate", 12, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Wraith", "Bids a shadowy wraith to unwillingly join your party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.DemonBane, SpellType.Wizard, 2, "DEBA", "Demon Bane", 11, SpellWhen.CombatAnywhere, SpellTarget.Monster30Feet, SpellDuration.Instant, "100d4 damage to evil/supernatural", "Inflicts 100 to 400 points of damage on a single creature of evil or supernatural origin."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.FlameColumn, SpellType.Wizard, 3, "FLCO", "Flame Column", 14, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup30Feet, SpellDuration.Instant, "22d4 damage", "Creates a cyclone of flame that lashes out and delivers 22 to 88 points of damage to a group of your Foes."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Dispossess, SpellType.Wizard, 3, "DISP", "Dispossess", 12, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "Cure \"Possessed\" condition", "Returns a possessed party member to the normal state of consciousness."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.PrimeSummoning, SpellType.Wizard, 4, "PRSU", "Prime Summoning", 15, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon undead creature", "Forces a powerful undead creature to join and fight for your party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.AnimateDead, SpellType.Wizard, 4, "ANDE", "Animate Dead", 14, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Combat, "Allow dead character to fight", "Reanimates a dead character with living strength so he or she attacks enemies as if truly alive -- combat only spell."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.BaylorsSpellBind, SpellType.Wizard, 5, "SPBI", "Baylor's Spell Bind", 16, SpellWhen.AnywhereAnytime, SpellTarget.Monster, SpellDuration.Instant, "Causes \"Possessed\" condition", "If successful, this spell possesses the mind of an enemy and forces him to join and fight for your party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.StoralsSoulWhip, SpellType.Wizard, 5, "SOWH", "Storal's Soul Whip", 13, SpellWhen.CombatAnywhere, SpellTarget.Monster70Feet, SpellDuration.Instant, "50d4 damage", "Whips out a tendril of psionic (mind) power to strike a selected foe, inflicting 50 to 200 damage points."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.GreaterSummoning, SpellType.Wizard, 6, "GRSU", "Greater Summoning", 22, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon elemental creature", "Operates like PRIME SUMMONING but causes a powerful elemental creature to appear and fight for the party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.BeyondDeath, SpellType.Wizard, 6, "BEDE", "Beyond Death", 18, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "Cure \"Dead\" condition", "Restores life and one hit point to a deceased character."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.WacumsWizardWar, SpellType.Wizard, 7, "WIZW", "Wacum's Wizard War", 16, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup50Feet, SpellDuration.Instant, "50d4 damage", "Creates a pyrotechnical storm over a group of monsters, inflicting 50 to 200 damage points."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.SummonHerb, SpellType.Wizard, 7, "HERB", "Summon Herb", 25, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Herb", "Summons Herb to join your party. Herb is really busy, but he'll hang out with your party for a while if you need him."));

            m_spells.Add(new BT2Spell(BT2SpellIndex.OsconsHaltfoe, SpellType.Archmage, 1, "HAFO", "Oscon's Haltfoe", 15, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.OneRound, "Prevent action", "If successful, this spell causes every attacking group to do nothing during the next round."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.MeleeMen, SpellType.Archmage, 1, "MEME", "Melee Men", 20, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "Pull into melee range", "Pulls an attacking group into melee range (10') regardless of how far they were when they began attacking."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Batchspell, SpellType.Archmage, 2, "BASP", "Batchspell", 28, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "GRRE/YMCA/SOSI/MALE/MACO", "Performs the following multiple spells: GREATER REVELATION, YBARRA'S MYSTICAL COAT OF ARMOR, SORCERER SIGHT, MAJOR LEVITATION, and KIEL'S MAGIC COMPASS."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Camaraderie, SpellType.Archmage, 3, "CAMR", "Camaraderie", 26, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Calm hostile monsters", "Has a 50% chance of calming any or all monsters in your party that have turned hostile."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.FanskarsNightLance, SpellType.Archmage, 4, "NILA", "Fanskar's Night Lance", 30, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup60Feet, SpellDuration.Instant, "100d4 damage", "Launches a chilling missile against a group of Foes, inflicting 100 to 400 damage points."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.HealAll, SpellType.Archmage, 5, "HEAL", "Heal All", 50, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Resurrect and heal all", "A Beyond Death spell that resurrects every dead party member and heals all wounds, paralysis, and insanity."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.TheBrothersKringle, SpellType.Archmage, 6, "BRKR", "The Brothers Kringle", 60, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Kringle Bro", "The brothers are always ready to help friends in trouble. Enough brothers appear to fill the empty slots in your party."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.MangarsMallet, SpellType.Archmage, 7, "MAMA", "Mangar's Mallet", 80, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.Instant, "200d4 damage", "Inflicts 200 to 800 bone-crushing damage points against every monster group you face."));

            m_spells.Add(new BT2Spell(BT2SpellIndex.ArchersTune, SpellType.Bard, 1, "", "The Archer's Tune", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "2x party missile, half foe missile", "Doubles the party's missile damage, and cuts the missile damage inflicted by a foe in half.  Missile weapons are those weapons that are thrown or shot such as arrows, spears, and axes (3 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.Spellsong, SpellType.Bard, 1, "", "Spellsong", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Assists with saving rolls", "+2 Bonus to saving roll.  This means the party is less likely to be damaged by magic and traps (3 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.SanctuaryScore, SpellType.Bard, 1, "", "Sanctuary Score", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "-2 AC", "Lowers the Armor Class for all party members by 2 (3 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.MeleeMarch, SpellType.Bard, 1, "", "The Melee March", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "+3 HP, bonus damage", "Increases the party's hit points for extra protection and also increase the damage points inflicted on enemies (3 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.ZanduvarCarack, SpellType.Bard, 1, "", "Zanduvar Carack", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Trap protection, +3 HP per round", "Protection from traps when played under normal conditions, but heals during combat (3 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.RhymeOfDuotime, SpellType.Bard, 1, "", "Rhyme of Duotime", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "2x SP regen, +1 attack", "Regenerates spell points at twice the normal speed when played under normal conditions, and provides extra attacks during combat (3 hours)."));
            m_spells.Add(new BT2Spell(BT2SpellIndex.WatchwoodMelody, SpellType.Bard, 1, "", "The Watchwood Melody", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Light (3 squares)", "Creates light. May work even in anti-magic zones (3 hours)."));
        }
    }

    public class BT2Spell : BTSpell
    {
        public BT2SpellIndex Index;
        public override int BasicIndex { get { return (int)Index; } }
        public override bool UsesLevelOnly { get { return true; } }

        public BT2Spell(BT2SpellIndex index, SpellType type, int level, string abbrev, string name, int sp, SpellWhen when, SpellTarget target, SpellDuration duration, string shortDesc, string desc)
        {
            Index = index;
            Name = name;
            Abbreviation = abbrev;
            Type = type;
            Level = level;
            When = when;
            Target = target;
            ShortDescription = shortDesc;
            Description = desc;
            Duration = duration;
            Cost = new SpellCost(sp);
        }

        public static BT2Spell None
        {
            get { return new BT2Spell(BT2SpellIndex.None, SpellType.Unknown, 0, "", "None", 0, SpellWhen.AnywhereAnytime, SpellTarget.Unknown, SpellDuration.Instant, "", ""); }
        }

        public override Keys[] GetKeys(BaseCharacter character = null)
        {
            if (Type == SpellType.Bard)
                return new Keys[] { Keys.D1 + (Index - BT2SpellIndex.ArchersTune) };
            Keys[] keys = new Keys[Abbreviation.Length + 1];
            for (int i = 0; i < Abbreviation.Length; i++)
                keys[i] = NativeMethods.KeyForChar(Char.ToLower(Abbreviation[i]));
            keys[Abbreviation.Length] = Keys.Enter;
            return keys;
        }
    }
}
