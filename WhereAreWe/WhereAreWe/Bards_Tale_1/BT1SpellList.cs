using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public enum BT1SpellIndex
    {
        None = 0,
        MageFlame,                  // Conjurer L1 (MAFL)     # 1 
        ArcFire,                    // Conjurer L1 (ARFI)     # 2 
        SorcererShield,             // Conjurer L1 (SOSH)     # 3 
        TrapZap,                    // Conjurer L1 (TRZP)     # 4 
        FreezeFoes,                 // Conjurer L2 (FRFO)     # 5 
        KielsMagicCompass,          // Conjurer L2 (MACO)     # 6 
        Battleskill,                // Conjurer L2 (BASK)     # 7 
        WordOfHealing,              // Conjurer L2 (WOHL)     # 8 
        ArcynesMagestar,            // Conjurer L3 (MAST)     # 9 
        LesserRevelation,           // Conjurer L3 (LERE)     # 10
        Levitation,                 // Conjurer L3 (LEVI)     # 11
        Warstrike,                  // Conjurer L3 (WAST)     # 12
        EliksInstantWolf,           // Conjurer L4 (INWO)     # 13
        FleshRestore,               // Conjurer L4 (FLRE)     # 14
        PoisonStrike,               // Conjurer L4 (POST)     # 15
        GreaterRevelation,          // Conjurer L5 (GRRE)     # 16
        WrathOfValhalla,            // Conjurer L5 (WROV)     # 17
        ShockSphere,                // Conjurer L5 (SHSP)     # 18
        EliksInstantOgre,           // Conjurer L6 (INOG)     # 19
        MajorLevitation,            // Conjurer L6 (MALE)     # 20
        FleshAnew,                  // Conjurer L7 (FLAN)     # 21
        ApportArcane,               // Conjurer L7 (APAR)     # 22

        MangarsMindJab,             // Sorcerer L1 (MIJA)     # 23
        PhaseBlur,                  // Sorcerer L1 (PHBL)     # 24
        LocateTraps,                // Sorcerer L1 (LOTR)     # 25
        HypnoticImage,              // Sorcerer L1 (HYIM)     # 26
        Disbelief,                  // Sorcerer L2 (DISB)     # 27
        TargetDummy,                // Sorcerer L2 (TADU)     # 28
        MangarsMindFist,            // Sorcerer L2 (MIFI)     # 29
        WordOfFear,                 // Sorcerer L2 (FEAR)     # 30
        WindWolf,                   // Sorcerer L3 (WIWO)     # 31
        KylearansVanishingSpell,    // Sorcerer L3 (VANI)     # 32
        SecondSight,                // Sorcerer L3 (SESI)     # 33
        Curse,                      // Sorcerer L3 (CURS)     # 34
        CatEyes,                    // Sorcerer L4 (CAEY)     # 35
        WindWarrior,                // Sorcerer L4 (WIWA)     # 36
        KylearansInvisibilitySpell, // Sorcerer L4 (INVI)     # 37
        WindOgre,                   // Sorcerer L5 (WIOG)     # 38
        DisruptIllusion,            // Sorcerer L5 (DIIL)     # 39
        MangarsMindBlade,           // Sorcerer L5 (MIBL)     # 40
        WindDragon,                 // Sorcerer L6 (WIDR)     # 41
        MindWarp,                   // Sorcerer L6 (MIWP)     # 42
        WindGiant,                  // Sorcerer L7 (WIGI)     # 43
        SorcererSight,              // Sorcerer L7 (SOSI)     # 44

        VorpalPlating,              // Magician L1 (VOPL)     # 45
        AirArmor,                   // Magician L1 (AIAR)     # 46
        SabharsSteelightSpell,      // Magician L1 (STLI)     # 47
        ScrySite,                   // Magician L1 (SCSI)     # 48
        HolyWater,                  // Magician L2 (HOWA)     # 49
        WitherStrike,               // Magician L2 (WIST)     # 50
        MageGauntlets,              // Magician L2 (MAGA)     # 51
        AreaEnchant,                // Magician L2 (AREN)     # 52
        YbarrasMysticShield,        // Magician L3 (MYSH)     # 53
        OsconsOgrestrength,         // Magician L3 (OGST)     # 54
        MithrilMight,               // Magician L3 (MIMI)     # 55
        Starflare,                  // Magician L3 (STFL)     # 56
        SpectreTouch,               // Magician L4 (SPTO)     # 57
        DragonBreath,               // Magician L4 (DRBR)     # 58
        SabharsStonelightSpell,     // Magician L4 (STSI)     # 59
        AntiMagic,                  // Magician L5 (ANMA)     # 60
        AkersAnimatedSword,         // Magician L5 (ANSW)     # 61
        StoneTouch,                 // Magician L5 (STTO)     # 62
        PhaseDoor,                  // Magician L6 (PHDO)     # 63
        YbarrasMysticalCoatOfArmor, // Magician L6 (YMCA)     # 64
        Restoration,                // Magician L7 (REST)     # 65
        Deathstrike,                // Magician L7 (DEST)     # 66

        SummonDead,                 // Wizard L1 (SUDE)       # 67
        RepelDead,                  // Wizard L1 (REDE)       # 68
        LesserSummoning,            // Wizard L2 (LESU)       # 69
        DemonBane,                  // Wizard L2 (DEBA)       # 70
        SummonPhantom,              // Wizard L3 (SUPH)       # 71
        Dispossess,                 // Wizard L3 (DISP)       # 72
        PrimeSummoning,             // Wizard L4 (PRSU)       # 73
        AnimateDead,                // Wizard L4 (ANDE)       # 74
        BaylorsSpellBind,           // Wizard L5 (SPBI)       # 75
        DemonStrike,                // Wizard L5 (DMST)       # 76
        SpellSpirit,                // Wizard L6 (SPSP)       # 77
        BeyondDeath,                // Wizard L6 (BEDE)       # 78
        GreaterSummoning,           // Wizard L7 (GRSU)       # 79

        FalkentynesFury,            // Bard
        TheSeekersBallad,           // Bard
        WaylandsWatch,              // Bard
        BadhrKilnfest,              // Bard
        TheTravellersTune,          // Bard
        Lucklaran,                  // Bard

        Last
    }

    public class BT1SpellList : SpellList
    {
        List<BT1Spell> m_spells;

        public override Spell GetSpell(int index) { return index < 0 || index >= m_spells.Count ? null : m_spells[index]; }

        public static BT1SpellIndex[] Conjurer =
        {
            BT1SpellIndex.MageFlame,                  // Conjurer L1 (MAFL)
            BT1SpellIndex.ArcFire,                    // Conjurer L1 (ARFI)
            BT1SpellIndex.SorcererShield,             // Conjurer L1 (SOSH)
            BT1SpellIndex.TrapZap,                    // Conjurer L1 (TRZP)
            BT1SpellIndex.FreezeFoes,                 // Conjurer L2 (FRFO)
            BT1SpellIndex.KielsMagicCompass,          // Conjurer L2 (MACO)
            BT1SpellIndex.Battleskill,                // Conjurer L2 (BASK)
            BT1SpellIndex.WordOfHealing,              // Conjurer L2 (WOHL)
            BT1SpellIndex.ArcynesMagestar,            // Conjurer L3 (MAST)
            BT1SpellIndex.LesserRevelation,           // Conjurer L3 (LERE)
            BT1SpellIndex.Levitation,                 // Conjurer L3 (LEVI)
            BT1SpellIndex.Warstrike,                  // Conjurer L3 (WAST)
            BT1SpellIndex.EliksInstantWolf,           // Conjurer L4 (INWO)
            BT1SpellIndex.FleshRestore,               // Conjurer L4 (FLRE)
            BT1SpellIndex.PoisonStrike,               // Conjurer L4 (POST)
            BT1SpellIndex.GreaterRevelation,          // Conjurer L5 (GRRE)
            BT1SpellIndex.WrathOfValhalla,            // Conjurer L5 (WROV)
            BT1SpellIndex.ShockSphere,                // Conjurer L5 (SHSP)
            BT1SpellIndex.EliksInstantOgre,           // Conjurer L6 (INOG)
            BT1SpellIndex.MajorLevitation,            // Conjurer L6 (MALE)
            BT1SpellIndex.FleshAnew,                  // Conjurer L7 (FLAN)
            BT1SpellIndex.ApportArcane                // Conjurer L7 (APAR)
        };

        public static BT1SpellIndex[] Magician =
        {
            BT1SpellIndex.VorpalPlating,              // Magician L1 (VOPL)
            BT1SpellIndex.AirArmor,                   // Magician L1 (AIAR)
            BT1SpellIndex.SabharsSteelightSpell,      // Magician L1 (STLI)
            BT1SpellIndex.ScrySite,                   // Magician L1 (SCSI)
            BT1SpellIndex.HolyWater,                  // Magician L2 (HOWA)
            BT1SpellIndex.WitherStrike,               // Magician L2 (WIST)
            BT1SpellIndex.MageGauntlets,              // Magician L2 (MAGA)
            BT1SpellIndex.AreaEnchant,                // Magician L2 (AREN)
            BT1SpellIndex.YbarrasMysticShield,        // Magician L3 (MYSH)
            BT1SpellIndex.OsconsOgrestrength,         // Magician L3 (OGST)
            BT1SpellIndex.MithrilMight,               // Magician L3 (MIMI)
            BT1SpellIndex.Starflare,                  // Magician L3 (STFL)
            BT1SpellIndex.SpectreTouch,               // Magician L4 (SPTO)
            BT1SpellIndex.DragonBreath,               // Magician L4 (DRBR)
            BT1SpellIndex.SabharsStonelightSpell,     // Magician L4 (STSI)
            BT1SpellIndex.AntiMagic,                  // Magician L5 (ANMA)
            BT1SpellIndex.AkersAnimatedSword,         // Magician L5 (ANSW)
            BT1SpellIndex.StoneTouch,                 // Magician L5 (STTO)
            BT1SpellIndex.PhaseDoor,                  // Magician L6 (PHDO)
            BT1SpellIndex.YbarrasMysticalCoatOfArmor, // Magician L6 (YMCA)
            BT1SpellIndex.Restoration,                // Magician L7 (REST)
            BT1SpellIndex.Deathstrike                 // Magician L7 (DEST)
        };

        public static BT1SpellIndex[] Sorcerer =
        {
            BT1SpellIndex.MangarsMindJab,             // Sorcerer L1 (MIJA)
            BT1SpellIndex.PhaseBlur,                  // Sorcerer L1 (PHBL)
            BT1SpellIndex.LocateTraps,                // Sorcerer L1 (LOTR)
            BT1SpellIndex.HypnoticImage,              // Sorcerer L1 (HYIM)
            BT1SpellIndex.Disbelief,                  // Sorcerer L2 (DISB)
            BT1SpellIndex.TargetDummy,                // Sorcerer L2 (TADU)
            BT1SpellIndex.MangarsMindFist,            // Sorcerer L2 (MIFI)
            BT1SpellIndex.WordOfFear,                 // Sorcerer L2 (FEAR)
            BT1SpellIndex.WindWolf,                   // Sorcerer L3 (WIWO)
            BT1SpellIndex.KylearansVanishingSpell,    // Sorcerer L3 (VANI)
            BT1SpellIndex.SecondSight,                // Sorcerer L3 (SESI)
            BT1SpellIndex.Curse,                      // Sorcerer L3 (CURS)
            BT1SpellIndex.CatEyes,                    // Sorcerer L4 (CAEY)
            BT1SpellIndex.WindWarrior,                // Sorcerer L4 (WIWA)
            BT1SpellIndex.KylearansInvisibilitySpell, // Sorcerer L4 (INVI)
            BT1SpellIndex.WindOgre,                   // Sorcerer L5 (WIOG)
            BT1SpellIndex.DisruptIllusion,            // Sorcerer L5 (DIIL)
            BT1SpellIndex.MangarsMindBlade,           // Sorcerer L5 (MIBL)
            BT1SpellIndex.WindDragon,                 // Sorcerer L6 (WIDR)
            BT1SpellIndex.MindWarp,                   // Sorcerer L6 (MIWP)
            BT1SpellIndex.WindGiant,                  // Sorcerer L7 (WIGI)
            BT1SpellIndex.SorcererSight               // Sorcerer L7 (SOSI)

        };

        public static BT1SpellIndex[] Wizard =
        {
            BT1SpellIndex.SummonDead,                 // Wizard L1 (SUDE)
            BT1SpellIndex.RepelDead,                  // Wizard L1 (REDE)
            BT1SpellIndex.LesserSummoning,            // Wizard L2 (LESU)
            BT1SpellIndex.DemonBane,                  // Wizard L2 (DEBA)
            BT1SpellIndex.SummonPhantom,              // Wizard L3 (SUPH)
            BT1SpellIndex.Dispossess,                 // Wizard L3 (DISP)
            BT1SpellIndex.PrimeSummoning,             // Wizard L4 (PRSU)
            BT1SpellIndex.AnimateDead,                // Wizard L4 (ANDE)
            BT1SpellIndex.BaylorsSpellBind,           // Wizard L5 (SPBI)
            BT1SpellIndex.DemonStrike,                // Wizard L5 (DMST)
            BT1SpellIndex.SpellSpirit,                // Wizard L6 (SPSP)
            BT1SpellIndex.BeyondDeath,                // Wizard L6 (BEDE)
            BT1SpellIndex.GreaterSummoning            // Wizard L7 (GRSU)
        };

        public static BT1SpellIndex[] Bard =
        {
            BT1SpellIndex.FalkentynesFury,            // Bard
            BT1SpellIndex.TheSeekersBallad,           // Bard
            BT1SpellIndex.WaylandsWatch,              // Bard
            BT1SpellIndex.BadhrKilnfest,              // Bard
            BT1SpellIndex.TheTravellersTune,          // Bard
            BT1SpellIndex.Lucklaran                   // Bard
        };

        public List<BT1Spell> Spells
        {
            get { return m_spells; }
        }

        public static BT1Spell[] HealingSpells = new BT1Spell[]
        {
            BT1.Spells[(int) BT1SpellIndex.WordOfHealing],
            BT1.Spells[(int) BT1SpellIndex.FleshRestore],
            BT1.Spells[(int) BT1SpellIndex.FleshAnew],
            BT1.Spells[(int) BT1SpellIndex.Restoration],
            BT1.Spells[(int) BT1SpellIndex.Dispossess],
            BT1.Spells[(int) BT1SpellIndex.BeyondDeath],
        };

        public BT1SpellList()
        {
            m_spells = new List<BT1Spell>(85);
            m_spells.Add(new BT1Spell(BT1SpellIndex.MageFlame, SpellType.Conjurer, 1, "MAFL", "Mage Flame", 2, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Medium, "Light (3 squares)", "A small, mobile \"torch\" will appear, and float above the spell caster as he travels (lasts 10-14 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.ArcFire, SpellType.Conjurer, 1, "ARFI", "Arc Fire", 3, SpellWhen.CombatAnywhere, SpellTarget.Monster, SpellDuration.Instant, "1d4 damage", "A fan of blue flames will shoot from the spell caster's fingers, doing 1-4 hits of damage per level to a selected opponent."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.SorcererShield, SpellType.Conjurer, 1, "SOSH", "Sorcerer Shield", 3, SpellWhen.CombatAnywhere, SpellTarget.Caster, SpellDuration.Combat, "-1 AC", "The mage is protected by an invisible \"shield\" of magic, that turns aside many blows that would otherwise hit him."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.TrapZap, SpellType.Conjurer, 1, "TRZP", "Trap Zap", 2, SpellWhen.NonCombatAnywhere, SpellTarget.ThirtyFeet, SpellDuration.Instant, "Disarm traps", "This spell will disarm any trap within thirty feet, in the direction the party is facing. It will also disarm traps on chests."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.FreezeFoes, SpellType.Conjurer, 2, "FRFO", "Freeze Foes", 3, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Combat, "+1 AC", "This spell binds your enemies with magical force, slowing their movements and making them easier to hit."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.KielsMagicCompass, SpellType.Conjurer, 2, "MACO", "Kiel's Magic Compass", 3, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Medium, "Show direction", "A compass of shimmering magelight appears above the party, telling the direction they face (lasts 10-14 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.Battleskill, SpellType.Conjurer, 2, "BASK", "Battleskill", 4, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Combat, "+4 To-Hit/Damage", "This spell increases one of your party member's skill with weapons, increasing the accuracy and ferocity of his attacks."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.WordOfHealing, SpellType.Conjurer, 2, "WOHL", "Word of Healing", 4, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "+2d4 HP", "With the utterance of a single word the spell caster can cure a party member of minor wounds, healing 2-8 points of damage."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.ArcynesMagestar, SpellType.Conjurer, 3, "MAST", "Arcyne's Magestar", 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "Lose combat turn", "A bright flare will ignite in front of a group of your enemies, temporarily blinding them and causing them to miss the next combat round."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.LesserRevelation, SpellType.Conjurer, 3, "LERE", "Lesser Revelation", 5, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Long, "Light (4 squares); Reveal secrets", "This is an extended \"Mage Flame\" spell which also reveals secret doors (lasts 15-19 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.Levitation, SpellType.Conjurer, 3, "LEVI", "Levitation", 4, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Float over traps and up portals", "Partially negates the effect of gravity on the party, causing them to float over traps or up through portals (lasts 5-8 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.Warstrike, SpellType.Conjurer, 3, "WAST", "Warstrike", 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "4d4 damage", "Causes a spray of energy to spring from the caster's extended finger, sizzling a group of opponents for 4-16 hits damage."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.EliksInstantWolf, SpellType.Conjurer, 4, "INWO", "Elik's Instant Wolf", 6, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Wolf", "With this spell the caster can make a real wolf appear and join the party, fighting in its defense."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.FleshRestore, SpellType.Conjurer, 4, "FLRE", "Flesh Restore", 6, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "+6d4 HP; Cure poison, nuts", "This powerful healing spell will restore 6-24 hit points to a party member and cure poisoning and insanity."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.PoisonStrike, SpellType.Conjurer, 4, "POST", "Poison Strike", 6, SpellWhen.CombatAnywhere, SpellTarget.Monster, SpellDuration.Instant, "Causes \"Poison\" condition", "This spell hurls porcupine-sharp needles from the mage's finger into a selected monster, poisoning it."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.GreaterRevelation, SpellType.Conjurer, 5, "GRRE", "Greater Revelation", 7, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Long, "Light (5 squares); Reveal secrets", "This spell functions like \"Lesser Revelation\" spell, only it illuminates a wider area (lasts 15-19 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.WrathOfValhalla, SpellType.Conjurer, 5, "WROV", "Wrath of Valhalla", 7, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Combat, "+10 To-Hit/Damage", "Makes a member of your party fight with the strength and accuracy of ancient Norse heroes for the entire combat."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.ShockSphere, SpellType.Conjurer, 5, "SHSP", "Shock Sphere", 7, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "8d4 damage", "A large globe of intense electrical energy envelops a group of enemies, doing 8-32 hits of damage."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.EliksInstantOgre, SpellType.Conjurer, 6, "INOG", "Elik's Instant Ogre", 9, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Ogre", "This incantation will cause a real ogre to appear and join the party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.MajorLevitation, SpellType.Conjurer, 6, "MALE", "Major Levitation", 8, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "Float over traps and up portals", "This will make the party levitate as does the level 3 spell, but its effects will last until dispelled."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.FleshAnew, SpellType.Conjurer, 7, "FLAN", "Flesh Anew", 12, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "+6d4 HP; Cure poison, nuts", "This spell behaves like the \"Flesh Restore\" spell, except that it will affect every member of the party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.ApportArcane, SpellType.Conjurer, 7, "APAR", "Apport Arcane", 15, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Change location", "Allows the party to teleport anywhere within a dungeon, except for places protected by teleportation shields."));

            m_spells.Add(new BT1Spell(BT1SpellIndex.MangarsMindJab, SpellType.Sorcerer, 1, "MIJA", "Mangar's Mind Jab", 3, SpellWhen.CombatAnywhere, SpellTarget.Monster, SpellDuration.Instant, "2d4 damage", "The mage casts a concentrated blast of psychic energy at one opponent doing 2-8 hits of damage for each experience level of the mage."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.PhaseBlur, SpellType.Sorcerer, 1, "PHBL", "Phase Blur", 2, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Combat, "-1 AC", "The entire party will seem to waver and blur in the sight of the monsters, making the party very difficult to strike."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.LocateTraps, SpellType.Sorcerer, 1, "LOTR", "Locate Traps", 2, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Short, "Detect traps", "In a state of magically-heightened awareness, the spell caster will be able to sense a trap within thirty feet, if he faces it (lasts 5-8 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.HypnoticImage, SpellType.Sorcerer, 1, "HYIM", "Hypnotic Image", 3, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "Prevent attacks", "If successfully cast, this spell will make a group of your enemies miss the following attack round."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.Disbelief, SpellType.Sorcerer, 2, "DISB", "Disbelief", 4, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Dispel illusions", "This spell will reveal the true of any illusion attacking the party, causing it to instantly vanish."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.TargetDummy, SpellType.Sorcerer, 2, "TADU", "Target-Dummy", 4, SpellWhen.CombatAnywhere, SpellTarget.Special, SpellDuration.Combat, "Summon Dummy Illusion", "A magical illusion appears in the party's special slot. Unable to attack, it will serve to draw enemy attacks to himself."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.MangarsMindFist, SpellType.Sorcerer, 2, "MIFI", "Mangar's Mind Fist", 4, SpellWhen.CombatAnywhere, SpellTarget.Monster, SpellDuration.Instant, "3d4 damage", "A higher power \"Mind Jab,\" does 3-12 hits of damage to one foe, times the experience level of the mage."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.WordOfFear, SpellType.Sorcerer, 2, "FEAR", "Word of Fear", 4, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Combat, "-1 To-Hit/Damage", "This incantation will make a group of your enemies shake in fear, reducing their ability to attack and do damage."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.WindWolf, SpellType.Sorcerer, 3, "WIWO", "Wind Wolf", 5, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Wolf Illusion", "This spell creates an illusionary wolf to join the party. This and other illusions are only effective as long as an enemy \"believes\" them. Depending on power and location, the monster may see through the illusion, and cause it to vanish."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.KylearansVanishingSpell, SpellType.Sorcerer, 3, "VANI", "Kylearan's Vanishing Spell", 6, SpellWhen.CombatAnywhere, SpellTarget.Caster, SpellDuration.Combat, "-5 AC", "The mage casting this spell will turn nearly invisible in the eyes of his enemies, who will have great difficulty in striking him."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.SecondSight, SpellType.Sorcerer, 3, "SESI", "Second Sight", 6, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Medium, "Detect special areas", "The mage will experience heightened awareness and be able to sense stairways, special encounters, spell negation zones, and other unusual occurrences (lasts 10-14 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.Curse, SpellType.Sorcerer, 3, "CURS", "Curse", 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Combat, "-3 To-Hit/Damage", "Causes a group of your enemies to fear you greatly, lessening their morale and their ability to hit and damage you."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.CatEyes, SpellType.Sorcerer, 4, "CAEY", "Cat Eyes", 7, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "Light (5 squares)", "The member's of the mage's party will all receive perfect night-vision, which will last indefinitely."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.WindWarrior, SpellType.Sorcerer, 4, "WIWA", "Wind Warrior", 6, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Mercenary Illusion", "This spell will create the illusion of a battle-ready warrior that joins your party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.KylearansInvisibilitySpell, SpellType.Sorcerer, 4, "INVI", "Kylearan's Invisibility Spell", 7, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Combat, "-4 AC", "This invocation will perform a Vanishing Spell on the entire party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.WindOgre, SpellType.Sorcerer, 5, "WIOG", "Wind Ogre", 7, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Ogre Illusion", "This spell will create the illusion of an ogre, which will accompany and fight with your party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.DisruptIllusion, SpellType.Sorcerer, 5, "DIIL", "Disrupt Illusion", 8, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Combat, "Dispel illusions and dopplegangers", "This spell will destroy any illusion fighting the party, and any new illusions created later in the combat. It will also point out any dopplegangers in the party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.MangarsMindBlade, SpellType.Sorcerer, 5, "MIBL", "Mangar's Mind Blade", 8, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.Instant, "10d4 damage", "A sharp explosion of psychic energy that inflicts 10-40 hits to each and every enemy you face."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.WindDragon, SpellType.Sorcerer, 6, "WIDR", "Wind Dragon", 10, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Red Dragon Illusion", "This incantation will create an illusionary red dragon to fight with your party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.MindWarp, SpellType.Sorcerer, 6, "MIWP", "Mind Warp", 9, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "Causes \"Nuts\" condition", "This spell will make a member of your party go totally insane. Useful for possessions."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.WindGiant, SpellType.Sorcerer, 7, "WIGI", "Wind Giant", 12, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Storm Giant Illusion", "This spell will create an illusionary storm giant, to join with, and fight for, your party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.SorcererSight, SpellType.Sorcerer, 7, "SOSI", "Sorcerer Sight", 11, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Indefinite, "Detect special areas", "This spell functions the same as the \"second sight\", but it will last indefinitely."));

            m_spells.Add(new BT1Spell(BT1SpellIndex.VorpalPlating, SpellType.Magician, 1, "VOPL", "Vorpal Plating", 3, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Combat, "+2d4 damage", "This spell causes the weapon (or hands) of a party member to be covered with a magical field, which causes him to do an additional 2-8 points of damage."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.AirArmor, SpellType.Magician, 1, "AIAR", "Air Armor", 3, SpellWhen.CombatAnywhere, SpellTarget.Caster, SpellDuration.Combat, "-1 AC", "This spell will make the air around the spell caster to bind itself into a weightless suit of \"armor.\""));
            m_spells.Add(new BT1Spell(BT1SpellIndex.SabharsSteelightSpell, SpellType.Magician, 1, "STLI", "Sabhar's Steelight Spell", 2, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Light (3 squares)", "Causes all metal near the party to glow with magical light, illuminating the surrounding area (lasts 5-8 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.ScrySite, SpellType.Magician, 1, "SCSI", "Scry Site", 2, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Show location", "The walls themselves will speak, under direction of this spell, revealing to the spell caster his location in the labyrinth."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.HolyWater, SpellType.Magician, 2, "HOWA", "Holy Water", 4, SpellWhen.CombatAnywhere, SpellTarget.Monster, SpellDuration.Instant, "6d4 damage to undead", "A spray of water will emanate from the mage's fingers, doing 6-24 points of damage to any undead foe (e.g. skeleton, zombie, vampire)"));
            m_spells.Add(new BT1Spell(BT1SpellIndex.WitherStrike, SpellType.Magician, 2, "WIST", "Wither Strike", 5, SpellWhen.CombatAnywhere, SpellTarget.Monster, SpellDuration.Instant, "Causes \"Old\" condition", "Any foe at whom this spell is cast is likely to be turned old, thus reducing his ability to attack and defend in combat."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.MageGauntlets, SpellType.Magician, 2, "MAGA", "Mage Gauntlets", 5, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Combat, "+4d4 damage", "Makes a party member's hands (or weapons) more deadly, adding 4-16 points of damage to every wound he inflicts."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.AreaEnchant, SpellType.Magician, 2, "AREN", "Area Enchant", 5, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Short, "Locate stairways", "This spell will cause the dungeon walls within thirty feet of a stairway to call out, if the party is traveling toward it (lasts 5-8 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.YbarrasMysticShield, SpellType.Magician, 3, "MYSH", "Ybarra's Mystic Shield", 6, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Medium, "-2 AC", "The air in front of the party will bind itself into metallic hardness and will accompany the party when it moves, as a sort of invisible \"shield\" (lasts 10-14 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.OsconsOgrestrength, SpellType.Magician, 3, "OGST", "Oscon's Ogrestrength", 6, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Combat, "+7 To-Hit/Damage", "Allows a member of your party to damage monsters as if he were as incredibly strong as an ogre."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.MithrilMight, SpellType.Magician, 3, "MIMI", "Mithril Might", 7, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Combat, "-3 AC", "Increases the armor protection of each party member by enhancing their armor's natural strength by magic."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.Starflare, SpellType.Magician, 3, "STFL", "Starflare", 6, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "6d4 damage", "The air surrounding a group of your enemies will instantly ignite, causing them to be burnt for 6 to 24 damage points."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.SpectreTouch, SpellType.Magician, 4, "SPTO", "Spectre Touch", 8, SpellWhen.CombatAnywhere, SpellTarget.Monster, SpellDuration.Instant, "12d4 damage", "This spell will drain a single enemy of 12 to 48 points of damage, as if touched by a spectre."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.DragonBreath, SpellType.Magician, 4, "DRBR", "Dragon Breath", 7, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "8d4 damage", "Allows the mage to breathe fire at a group of foes, doing 8 to 32 points of damage to each."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.SabharsStonelightSpell, SpellType.Magician, 4, "STSI", "Sabhar's Stonelight Spell", 7, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Medium, "Light (5 squares); Reveal secrets", "Makes all stone and earth within range of the party glow with magical light, revealing even secret doors (lasts 10-14 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.AntiMagic, SpellType.Magician, 5, "ANMA", "Anti-Magic", 8, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Combat, "+2 Magic Resist", "Causes the ground to absorb a portion of the magical energies cast at the party, frequently allowing the members to escape all damage. Also aids in disbelieving illusions and in turning back magical fire, like a dragon's breath."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.AkersAnimatedSword, SpellType.Magician, 5, "ANSW", "Aker's Animated Sword", 8, SpellWhen.CombatAnywhere, SpellTarget.Special, SpellDuration.Combat, "Summon Joe the Sword", "A magical sword will appear and fight like a summoned monster in defense of the party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.StoneTouch, SpellType.Magician, 5, "STTO", "Stone Touch", 8, SpellWhen.CombatAnywhere, SpellTarget.Monster, SpellDuration.Instant, "Cause \"Stone\" condition", "This spell will often turn an enemy to stone, or a stone monster from living stone to dead stone. But it doesn't always work."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.PhaseDoor, SpellType.Magician, 6, "PHDO", "Phase Door", 9, SpellWhen.NonCombatAnywhere, SpellTarget.OneWall, SpellDuration.OneMove, "Allow passage through wall", "This incantation will alter the structure of almost any wall directly in front of the party, turning it to air for exactly 1 move."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.YbarrasMysticalCoatOfArmor, SpellType.Magician, 6, "YMCA", "Ybarra's Mystical Coat of Armor", 10, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "-2 AC", "Causes an effect like \"Air Armor\" to cover every member of the party, lasting indefinitely."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.Restoration, SpellType.Magician, 7, "REST", "Restoration", 12, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Heal all HP; Cure poison, nuts", "Makes all wounds disappear as your entire party is reforged into unflawed bodies. Also cures poisoning and insanity."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.Deathstrike, SpellType.Magician, 7, "DEST", "Deathstrike", 14, SpellWhen.CombatAnywhere, SpellTarget.Monster, SpellDuration.Instant, "Kill enemy", "This incantation is very likely to kill one selected enemy, big or small."));

            m_spells.Add(new BT1Spell(BT1SpellIndex.SummonDead, SpellType.Wizard, 1, "SUDE", "Summon Dead", 6, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Zombie or Skeleton", "This will gate into our universe a zombie or skeleton to fight for the party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.RepelDead, SpellType.Wizard, 1, "REDE", "Repel Dead", 4, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "16d5 damage to undead", "This spell will do 16 to 80 points of damage to a group of undead creatures."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.LesserSummoning, SpellType.Wizard, 2, "LESU", "Lesser Summoning", 8, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Lesser Demon", "This spell will gate into our universe a lower power elemental or demon, who will (under protest) join the party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.DemonBane, SpellType.Wizard, 2, "DEBA", "Demon Bane", 8, SpellWhen.CombatAnywhere, SpellTarget.Monster, SpellDuration.Instant, "32d4 damage to demon", "This spell will do 32 to 128 points of damage to a single demon. The power to summon is the power to destroy."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.SummonPhantom, SpellType.Wizard, 3, "SUPH", "Summon Phantom", 10, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Ghoul or Wraith", "This spell will bring a medium level undead creature into the party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.Dispossess, SpellType.Wizard, 3, "DISP", "Dispossess", 10, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "Cure \"Possessed\" condition", "This spell will make any possessed party member to his normal state."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.PrimeSummoning, SpellType.Wizard, 4, "PRSU", "Prime Summoning", 12, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Demon", "This spell gates in a medium level elemental or demon, to fight with the party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.AnimateDead, SpellType.Wizard, 4, "ANDE", "Animate Dead", 11, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "Allow dead character to fight", "Gives a dead character undead strength, making him attack your enemies as though he were truly alive."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.BaylorsSpellBind, SpellType.Wizard, 5, "SPBI", "Baylor's Spell Bind", 14, SpellWhen.CombatAnywhere, SpellTarget.Monster, SpellDuration.Instant, "Causes \"possessed\" condition", "This spell if successful possesses the mind of any enemy, forcing him to join your party and fight in its defense."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.DemonStrike, SpellType.Wizard, 5, "DMST", "Demon Strike", 14, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "32d4 damage to demons", "This spell works like Demon Bane, but it will affect an entire group of demons."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.SpellSpirit, SpellType.Wizard, 6, "SPSP", "Spell Spirit", 15, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Lich or Spectre", "This spell will gate in a higher-level undead creature to fight for the party."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.BeyondDeath, SpellType.Wizard, 6, "BEDE", "Beyond Death", 18, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "Cure \"Dead\" condition", "This spell will restore life and one hit point to a character."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.GreaterSummoning, SpellType.Wizard, 7, "GRSU", "Greater Summoning", 22, SpellWhen.AnywhereAnytime, SpellTarget.Special, SpellDuration.Instant, "Summon Demon Lord or Greater Demon", "This spell will gate a greater demon into our universe and bind him to the party."));

            m_spells.Add(new BT1Spell(BT1SpellIndex.FalkentynesFury, SpellType.Bard, 1, "", "Falkentyne's Fury", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Increase melee damage", "This tune increases the damage your party will do in combat, by driving them into a berserker rage (lasts 3 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.TheSeekersBallad, SpellType.Bard, 1, "", "The Seeker's Ballad", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Light (3 squares); Increase to-hit", "This song will produce light when exploring, and during combat it will increase the party's chance of hitting a foe with a weapon (lasts 3 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.WaylandsWatch, SpellType.Bard, 1, "", "Wayland's Watch", 0, SpellWhen.AnywhereAnytime, SpellTarget.AllMonsters, SpellDuration.Short, "Reduce damage", "This song will soothe your savage foes, making them do less damage in combat (lasts 3 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.BadhrKilnfest, SpellType.Bard, 1, "", "Badh'r Kilnfest", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Restore HP", "This is an ancient Elven melody, which will heal the Bard's wounds during traveling (1 HP every 15 minutes), and heal the party's wounds during combat (1 HP every round) (lasts 3 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.TheTravellersTune, SpellType.Bard, 1, "", "The Traveller's Tune", 0, SpellWhen.AnywhereAnytime, SpellTarget.AllMonsters, SpellDuration.Short, "Reduce to-hit", "This melody makes the members of your party more dexterous and agile, and thus more difficult to hit (lasts 3 hours)."));
            m_spells.Add(new BT1Spell(BT1SpellIndex.Lucklaran, SpellType.Bard, 1, "", "Lucklaran", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Increase spell defense", "This song sets up a partial \"anti-magic\" field, which gives party members some increased protection against spell casting (lasts 3 hours)."));
        }
    }

    public class BT1Spell : BTSpell
    {
        public BT1SpellIndex Index;
        public override int BasicIndex { get { return (int)Index; } }
        public override bool UsesLevelOnly { get { return true; } }

        public BT1Spell(BT1SpellIndex index, SpellType type, int level, string abbrev, string name, int sp, SpellWhen when, SpellTarget target, SpellDuration duration, string shortDesc, string desc)
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

        public static BT1Spell None
        {
            get { return new BT1Spell(BT1SpellIndex.None, SpellType.Unknown, 0, "", "None", 0, SpellWhen.AnywhereAnytime, SpellTarget.Unknown, SpellDuration.Instant, "", ""); }
        }

        public override Keys[] GetKeys()
        {
            if (Type == SpellType.Bard)
                return new Keys[] { Keys.D1 + (Index - BT1SpellIndex.FalkentynesFury) };
            Keys[] keys = new Keys[Abbreviation.Length + 1];
            for (int i = 0; i < Abbreviation.Length; i++)
                keys[i] = NativeMethods.KeyForChar(Char.ToLower(Abbreviation[i]));
            keys[Abbreviation.Length] = Keys.Enter;
            return keys;
        }
    }
}
