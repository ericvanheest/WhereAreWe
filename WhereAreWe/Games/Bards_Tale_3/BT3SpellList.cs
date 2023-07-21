using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public enum BT3SpellIndex
    {
        None = 0,
        MageFlame,                      // Conjurer L1 (MAFL)        # 1 
        ArcFire,                        // Conjurer L1 (ARFI)        # 2 
        TrapZap,                        // Conjurer L1 (TRZP)        # 3 
        Freeze,                         // Conjurer L2 (FRFO)        # 4 
        KielsMagicCompass,              // Conjurer L2 (MACO)        # 5 
        WordOfHealing,                  // Conjurer L2 (WOHL)        # 6 
        LesserRevelation,               // Conjurer L3 (LERE)        # 7 
        Levitation,                     // Conjurer L3 (LEVI)        # 8 
        Warstrike,                      // Conjurer L3 (WAST)        # 9 
        EliksInstantWolf,               // Conjurer L4 (INWO)        # 10
        FleshRestore,                   // Conjurer L4 (FLRE)        # 11
        GreaterRevelation,              // Conjurer L5 (GRRE)        # 12
        Shocksphere,                    // Conjurer L5 (SHSP)        # 13
        FleshAnew,                      // Conjurer L6 (FLAN)        # 14
        MajorLevitation,                // Conjurer L6 (MALE)        # 15
        Regeneration,                   // Conjurer L7 (REGN)        # 16
        ApportArcane,                   // Conjurer L7 (APAR)        # 17
        FarFoe,                         // Conjurer L7 (FAFO)        # 18
        EliksInstantSlayer,             // Conjurer L7 (INSL)        # 19
                                                                    
        VorpalPlating,                  // Magician L1 (VOPL)        # 20
        QuickFix,                       // Magician L1 (QUFI)        # 21
        ScrySite,                       // Magician L1 (SCSI)        # 22
        HolyWater,                      // Magician L2 (HOWA)        # 23
        MageGauntlets,                  // Magician L2 (MAGA)        # 24
        AreaEnchant,                    // Magician L2 (AREN)        # 25
        YbarrasMysticShield,            // Magician L3 (MYSH)        # 26
        OsconsOgrestrength,             // Magician L3 (OGST)        # 27
        Starflare,                      // Magician L3 (STFL)        # 28
        SpectreTouch,                   // Magician L4 (SPTO)        # 29
        DragonBreath,                   // Magician L4 (DRBR)        # 30
        Antimagic,                      // Magician L5 (ANMA)        # 31
        GiantStrength,                  // Magician L5 (GIST)        # 32
        PhaseDoor,                      // Magician L6 (PHDO)        # 33
        YbarrasMysticalCoatOfArmor,     // Magician L6 (YMCA)        # 34
        Restoration,                    // Magician L7 (REST)        # 35
        Deathstrike,                    // Magician L7 (DEST)        # 36
        IceStorm,                       // Magician L7 (ICES)        # 37
        StoneToFlesh,                   // Magician L7 (STON)        # 38
                                                                    
        MangarsMindJab,                 // Sorcerer L1 (MIJA)        # 39
        PhaseBlur,                      // Sorcerer L1 (PHBL)        # 40
        LocateTraps,                    // Sorcerer L1 (LOTR)        # 41
        Disbelieve,                     // Sorcerer L2 (DISB)        # 42
        WindWarrior,                    // Sorcerer L2 (WIWA)        # 43
        WordOfFear,                     // Sorcerer L2 (FEAR)        # 44
        WindOgre,                       // Sorcerer L3 (WIOG)        # 45
        KylearansInvisibilitySpell,     // Sorcerer L3 (INVI)        # 46
        SecondSight,                    // Sorcerer L3 (SESI)        # 47
        CatEyes,                        // Sorcerer L4 (CAEY)        # 48
        WindDragon,                     // Sorcerer L4 (WIDR)        # 49
        DisruptIllusion,                // Sorcerer L5 (DIIL)        # 50
        MangarsMindBlade,               // Sorcerer L5 (MIBL)        # 51
        WindGiant,                      // Sorcerer L6 (WIGI)        # 52
        SorcererSight,                  // Sorcerer L6 (SOSI)        # 53
        Rimefang,                       // Sorcerer L7 (RIME)        # 54
        WindHero,                       // Sorcerer L7 (WIHE)        # 55
        MageMaelstrom,                  // Sorcerer L7 (MAGM)        # 56
        Preclusion,                     // Sorcerer L7 (PREC)        # 57
                                                                    
        SummonElemental,                // Wizard L1 (SUEL)          # 58
        FanskarsForceFocus,             // Wizard L1 (FOFO)          # 59
        PrimeSummoning,                 // Wizard L2 (PRSU)          # 60
        DemonBane,                      // Wizard L2 (DEBA)          # 61
        FlameColumn,                    // Wizard L3 (FLCO)          # 62
        Dispossess,                     // Wizard L3 (DISP)          # 63
        SummonHerb,                     // Wizard L4 (HERB)          # 64
        AnimateDead,                    // Wizard L4 (ANDE)          # 65
        BaylorsSpellBind,               // Wizard L5 (SPBI)          # 66
        StoralsSoulWhip,                // Wizard L5 (SOWH)          # 67
        GreaterSummoning,               // Wizard L6 (GRSU)          # 68
        BeyondDeath,                    // Wizard L6 (BEDE)          # 69
        WacumsWizardWar,                // Wizard L7 (WIZW)          # 70
        DemonStrike,                    // Wizard L7 (DMST)          # 71

        OsconsHaltfoe,                  // Archmage L1 (HAFO)        # 72
        MeleeMen,                       // Archmage L1 (MEME)        # 73
        Batchspell,                     // Archmage L2 (BASP)        # 74
        Camaraderie,                    // Archmage L3 (CAMR)        # 75
        FanskarsNightLance,             // Archmage L4 (NILA)        # 76
        HealAll,                        // Archmage L5 (HEAL)        # 77
        TheBrothersKringle,             // Archmage L6 (BRKR)        # 78
        MangarsMallet,                  // Archmage L7 (MAMA)        # 79

        Vitality,                       // Chronomancer L1 (Vitl)    # 80 
        ToArboria,                      // Chronomancer L1 (Arbo)    # 81 
        FromArboria,                    // Chronomancer L1 (Enik)    # 82 
        Witherfist,                     // Chronomancer L2 (Wifi)    # 83 
        FrostForce,                     // Chronomancer L2 (Cold)    # 84 
        ToGelidia,                      // Chronomancer L2 (Geli)    # 85 
        FromGelidia,                    // Chronomancer L2 (Ecul)    # 86 
        GodFire,                        // Chronomancer L3 (Gofi)    # 87 
        StunForce,                      // Chronomancer L3 (Stun)    # 88 
        ToLucencia,                     // Chronomancer L3 (Luce)    # 89 
        FromLucencia,                   // Chronomancer L3 (Ileg)    # 90 
        LuckChant,                      // Chronomancer L4 (Luck)    # 91 
        FarDeath,                       // Chronomancer L4 (Fade)    # 92 
        ToKinestia,                     // Chronomancer L4 (Kine)    # 93 
        FromKinestia,                   // Chronomancer L4 (Obra)    # 94 
        Identify,                       // Chronomancer L5 (What)    # 95 
        Youth,                          // Chronomancer L5 (Olay)    # 96 
        ToTenabrosia,                   // Chronomancer L5 (Oluk)    # 97 
        FromTenabrosia,                 // Chronomancer L5 (Ecea)    # 98 
        GraveRobber,                    // Chronomancer L6 (Grro)    # 99 
        ForceOfTarjan,                  // Chronomancer L6 (Fota)    # 100
        ToTarmitia,                     // Chronomancer L6 (Aece)    # 101
        FromTarmitia,                   // Chronomancer L6 (Kulo)    # 102
        ShadowShield,                   // Chronomancer L7 (Shsh)    # 103
        FatalFist,                      // Chronomancer L7 (Fafi)    # 104
        ToMalefia,                      // Chronomancer L7 (Evil)    # 105
        FromMalefia,                    // Chronomancer L7 (Live)    # 106

        EarthDagger,                    // Geomancer L1 (Eada)       # 107
        EarthSong,                      // Geomancer L1 (Easo)       # 108
        EarthWard,                      // Geomancer L1 (Eawa)       # 109
        Trebuchet,                      // Geomancer L2 (Treb)       # 110
        EarthElemental,                 // Geomancer L2 (Eael)       # 111
        WallWarp,                       // Geomancer L2 (Wawa)       # 112
        Petrify,                        // Geomancer L3 (Rock)       # 113
        RoscoesAlert,                   // Geomancer L3 (Roal)       # 114
        SuccorSong,                     // Geomancer L4 (Suso)       # 115
        Sandstorm,                      // Geomancer L4 (Sast)       # 116
        Sanctuary,                      // Geomancer L5 (Sant)       # 117
        GlacierStrike,                  // Geomancer L5 (Glst)       # 118
        Pathfinder,                     // Geomancer L6 (Path)       # 119
        MagmaBlast,                     // Geomancer L6 (Maba)       # 120
        JoltBolt,                       // Geomancer L7 (Jobo)       # 121
        EarthMaw,                       // Geomancer L7 (Eama)       # 122

        GillesGills,                    // Miscellaneous L1 (Gill)   # 123
        DivineIntervention,             // Miscellaneous L1 (Diva)   # 124
        Gotterdamurung,                 // Miscellaneous L1 (Nuke)   # 125

        SirRobinsTune,                  // Bard                      # 126
        SafetySong,                     // Bard                      # 127
        SanctuaryScore,                 // Bard                      # 128
        BringaroundBallad,              // Bard                      # 129
        RhymeOfDuotime,                 // Bard                      # 130
        WatchwoodMelody,                // Bard                      # 131
        KielsOverture,                  // Bard                      # 132
        MinstrelShield,                 // Bard                      # 133

        Last
    }

    public class BT3SpellList : SpellList
    {
        List<BT3Spell> m_spells;

        public override Spell GetSpell(int index) { return index < 0 || index >= m_spells.Count ? null : m_spells[index]; }

        public static BT3SpellIndex[] Conjurer =
        {
            BT3SpellIndex.MageFlame,                      // Conjurer L1 (MAFL)   # 1 
            BT3SpellIndex.ArcFire,                        // Conjurer L1 (ARFI)   # 2 
            BT3SpellIndex.TrapZap,                        // Conjurer L1 (TRZP)   # 3 
            BT3SpellIndex.Freeze,                         // Conjurer L2 (FRFO)   # 4 
            BT3SpellIndex.KielsMagicCompass,              // Conjurer L2 (MACO)   # 5 
            BT3SpellIndex.WordOfHealing,                  // Conjurer L2 (WOHL)   # 6 
            BT3SpellIndex.LesserRevelation,               // Conjurer L3 (LERE)   # 7 
            BT3SpellIndex.Levitation,                     // Conjurer L3 (LEVI)   # 8 
            BT3SpellIndex.Warstrike,                      // Conjurer L3 (WAST)   # 9 
            BT3SpellIndex.EliksInstantWolf,               // Conjurer L4 (INWO)   # 10
            BT3SpellIndex.FleshRestore,                   // Conjurer L4 (FLRE)   # 11
            BT3SpellIndex.GreaterRevelation,              // Conjurer L5 (GRRE)   # 12
            BT3SpellIndex.Shocksphere,                    // Conjurer L5 (SHSP)   # 13
            BT3SpellIndex.FleshAnew,                      // Conjurer L6 (FLAN)   # 14
            BT3SpellIndex.MajorLevitation,                // Conjurer L6 (MALE)   # 15
            BT3SpellIndex.Regeneration,                   // Conjurer L7 (REGN)   # 16
            BT3SpellIndex.ApportArcane,                   // Conjurer L7 (APAR)   # 17
            BT3SpellIndex.FarFoe,                         // Conjurer L7 (FAFO)   # 18
            BT3SpellIndex.EliksInstantSlayer              // Conjurer L7 (INSL)   # 19
        };

        public static BT3SpellIndex[] Magician =
        {
            BT3SpellIndex.VorpalPlating,                  // Magician L1 (VOPL)   # 20
            BT3SpellIndex.QuickFix,                       // Magician L1 (QUFI)   # 21
            BT3SpellIndex.ScrySite,                       // Magician L1 (SCSI)   # 22
            BT3SpellIndex.HolyWater,                      // Magician L2 (HOWA)   # 23
            BT3SpellIndex.MageGauntlets,                  // Magician L2 (MAGA)   # 24
            BT3SpellIndex.AreaEnchant,                    // Magician L2 (AREN)   # 25
            BT3SpellIndex.YbarrasMysticShield,            // Magician L3 (MYSH)   # 26
            BT3SpellIndex.OsconsOgrestrength,             // Magician L3 (OGST)   # 27
            BT3SpellIndex.Starflare,                      // Magician L3 (STFL)   # 28
            BT3SpellIndex.SpectreTouch,                   // Magician L4 (SPTO)   # 29
            BT3SpellIndex.DragonBreath,                   // Magician L4 (DRBR)   # 30
            BT3SpellIndex.Antimagic,                      // Magician L5 (ANMA)   # 31
            BT3SpellIndex.GiantStrength,                  // Magician L5 (GIST)   # 32
            BT3SpellIndex.PhaseDoor,                      // Magician L6 (PHDO)   # 33
            BT3SpellIndex.YbarrasMysticalCoatOfArmor,     // Magician L6 (YMCA)   # 34
            BT3SpellIndex.Restoration,                    // Magician L7 (REST)   # 35
            BT3SpellIndex.Deathstrike,                    // Magician L7 (DEST)   # 36
            BT3SpellIndex.IceStorm,                       // Magician L7 (ICES)   # 37
            BT3SpellIndex.StoneToFlesh,                   // Magician L7 (STON)   # 38
        };

        public static BT3SpellIndex[] Sorcerer =
        {
            BT3SpellIndex.MangarsMindJab,                 // Sorcerer L1 (MIJA)    # 39
            BT3SpellIndex.PhaseBlur,                      // Sorcerer L1 (PHBL)    # 40
            BT3SpellIndex.LocateTraps,                    // Sorcerer L1 (LOTR)    # 41
            BT3SpellIndex.Disbelieve,                     // Sorcerer L2 (DISB)    # 42
            BT3SpellIndex.WindWarrior,                    // Sorcerer L2 (WIWA)    # 43
            BT3SpellIndex.WordOfFear,                     // Sorcerer L2 (FEAR)    # 44
            BT3SpellIndex.WindOgre,                       // Sorcerer L3 (WIOG)    # 45
            BT3SpellIndex.KylearansInvisibilitySpell,     // Sorcerer L3 (INVI)    # 46
            BT3SpellIndex.SecondSight,                    // Sorcerer L3 (SESI)    # 47
            BT3SpellIndex.CatEyes,                        // Sorcerer L4 (CAEY)    # 48
            BT3SpellIndex.WindDragon,                     // Sorcerer L4 (WIDR)    # 49
            BT3SpellIndex.DisruptIllusion,                // Sorcerer L5 (DIIL)    # 50
            BT3SpellIndex.MangarsMindBlade,               // Sorcerer L5 (MIBL)    # 51
            BT3SpellIndex.WindGiant,                      // Sorcerer L6 (WIGI)    # 52
            BT3SpellIndex.SorcererSight,                  // Sorcerer L6 (SOSI)    # 53
            BT3SpellIndex.Rimefang,                       // Sorcerer L7 (RIME)    # 54
            BT3SpellIndex.WindHero,                       // Sorcerer L7 (WIHE)    # 55
            BT3SpellIndex.MageMaelstrom,                  // Sorcerer L7 (MAGM)    # 56
            BT3SpellIndex.Preclusion                      // Sorcerer L7 (PREC)    # 57
        };

        public static BT3SpellIndex[] Wizard =
        {
            BT3SpellIndex.SummonElemental,                // Wizard L1 (SUEL)      # 58
            BT3SpellIndex.FanskarsForceFocus,             // Wizard L1 (FOFO)      # 59
            BT3SpellIndex.PrimeSummoning,                 // Wizard L2 (PRSU)      # 60
            BT3SpellIndex.DemonBane,                      // Wizard L2 (DEBA)      # 61
            BT3SpellIndex.FlameColumn,                    // Wizard L3 (FLCO)      # 62
            BT3SpellIndex.Dispossess,                     // Wizard L3 (DISP)      # 63
            BT3SpellIndex.SummonHerb,                     // Wizard L4 (HERB)      # 64
            BT3SpellIndex.AnimateDead,                    // Wizard L4 (ANDE)      # 65
            BT3SpellIndex.BaylorsSpellBind,               // Wizard L5 (SPBI)      # 66
            BT3SpellIndex.StoralsSoulWhip,                // Wizard L5 (SOWH)      # 67
            BT3SpellIndex.GreaterSummoning,               // Wizard L6 (GRSU)      # 68
            BT3SpellIndex.BeyondDeath,                    // Wizard L6 (BEDE)      # 69
            BT3SpellIndex.WacumsWizardWar,                // Wizard L7 (WIZW)      # 70
            BT3SpellIndex.DemonStrike                     // Wizard L7 (DMST)      # 71
        };

        public static BT3SpellIndex[] Archmage =
        {
            BT3SpellIndex.OsconsHaltfoe,                  // Archmage L1 (HAFO)    # 72
            BT3SpellIndex.MeleeMen,                       // Archmage L1 (MEME)    # 73
            BT3SpellIndex.Batchspell,                     // Archmage L2 (BASP)    # 74
            BT3SpellIndex.Camaraderie,                    // Archmage L3 (CAMR)    # 75
            BT3SpellIndex.FanskarsNightLance,             // Archmage L4 (NILA)    # 76
            BT3SpellIndex.HealAll,                        // Archmage L5 (HEAL)    # 77
            BT3SpellIndex.TheBrothersKringle,             // Archmage L6 (BRKR)    # 78
            BT3SpellIndex.MangarsMallet                   // Archmage L7 (MAMA)    # 79
        };

        public static BT3SpellIndex[] Chronomancer =
        {
            BT3SpellIndex.Vitality,                       // Chronomancer L1 (Vitl)    # 80 
            BT3SpellIndex.ToArboria,                      // Chronomancer L1 (Arbo)    # 81 
            BT3SpellIndex.FromArboria,                    // Chronomancer L1 (Enik)    # 82 
            BT3SpellIndex.Witherfist,                     // Chronomancer L2 (Wifi)    # 83 
            BT3SpellIndex.FrostForce,                     // Chronomancer L2 (Cold)    # 84 
            BT3SpellIndex.ToGelidia,                      // Chronomancer L2 (Geli)    # 85 
            BT3SpellIndex.FromGelidia,                    // Chronomancer L2 (Ecul)    # 86 
            BT3SpellIndex.GodFire,                        // Chronomancer L3 (Gofi)    # 87 
            BT3SpellIndex.StunForce,                      // Chronomancer L3 (Stun)    # 88 
            BT3SpellIndex.ToLucencia,                     // Chronomancer L3 (Luce)    # 89 
            BT3SpellIndex.FromLucencia,                   // Chronomancer L3 (Ileg)    # 90 
            BT3SpellIndex.LuckChant,                      // Chronomancer L4 (Luck)    # 91 
            BT3SpellIndex.FarDeath,                       // Chronomancer L4 (Fade)    # 92 
            BT3SpellIndex.ToKinestia,                     // Chronomancer L4 (Kine)    # 93 
            BT3SpellIndex.FromKinestia,                   // Chronomancer L4 (Obra)    # 94 
            BT3SpellIndex.Identify,                       // Chronomancer L5 (What)    # 95 
            BT3SpellIndex.Youth,                          // Chronomancer L5 (Olay)    # 96 
            BT3SpellIndex.ToTenabrosia,                   // Chronomancer L5 (Oluk)    # 97 
            BT3SpellIndex.FromTenabrosia,                 // Chronomancer L5 (Ecea)    # 98 
            BT3SpellIndex.GraveRobber,                    // Chronomancer L6 (Grro)    # 99 
            BT3SpellIndex.ForceOfTarjan,                  // Chronomancer L6 (Fota)    # 100
            BT3SpellIndex.ToTarmitia,                     // Chronomancer L6 (Aece)    # 101
            BT3SpellIndex.FromTarmitia,                   // Chronomancer L6 (Kulo)    # 102
            BT3SpellIndex.ShadowShield,                   // Chronomancer L7 (Shsh)    # 103
            BT3SpellIndex.FatalFist,                      // Chronomancer L7 (Fafi)    # 104
            BT3SpellIndex.ToMalefia,                      // Chronomancer L7 (Evil)    # 105
            BT3SpellIndex.FromMalefia                     // Chronomancer L7 (Live)    # 106
        };

        public static BT3SpellIndex[] Geomancer =
        {
            BT3SpellIndex.EarthDagger,                    // Geomancer L1 (Eada)       # 107
            BT3SpellIndex.EarthSong,                      // Geomancer L1 (Easo)       # 108
            BT3SpellIndex.EarthWard,                      // Geomancer L1 (Eawa)       # 109
            BT3SpellIndex.Trebuchet,                      // Geomancer L2 (Treb)       # 110
            BT3SpellIndex.EarthElemental,                 // Geomancer L2 (Eael)       # 111
            BT3SpellIndex.WallWarp,                       // Geomancer L2 (Wawa)       # 112
            BT3SpellIndex.Petrify,                        // Geomancer L3 (Rock)       # 113
            BT3SpellIndex.RoscoesAlert,                   // Geomancer L3 (Roal)       # 114
            BT3SpellIndex.SuccorSong,                     // Geomancer L4 (Suso)       # 115
            BT3SpellIndex.Sandstorm,                      // Geomancer L4 (Sast)       # 116
            BT3SpellIndex.Sanctuary,                      // Geomancer L5 (Sant)       # 117
            BT3SpellIndex.GlacierStrike,                  // Geomancer L5 (Glst)       # 118
            BT3SpellIndex.Pathfinder,                     // Geomancer L6 (Path)       # 119
            BT3SpellIndex.MagmaBlast,                     // Geomancer L6 (Maba)       # 120
            BT3SpellIndex.JoltBolt,                       // Geomancer L7 (Jobo)       # 121
            BT3SpellIndex.EarthMaw                        // Geomancer L7 (Eama)       # 122
        };

        public static BT3SpellIndex[] Miscellaneous =
        {
            BT3SpellIndex.GillesGills,                    // Miscellaneous L1 (Gill)   # 123
            BT3SpellIndex.DivineIntervention,             // Miscellaneous L1 (Diva)   # 124
            BT3SpellIndex.Gotterdamurung                  // Miscellaneous L1 (Nuke)   # 125
        };

        public static BT3SpellIndex[] Bard =
        {
            BT3SpellIndex.SirRobinsTune,                  // Bard                  # 126
            BT3SpellIndex.SafetySong,                     // Bard                  # 127
            BT3SpellIndex.SanctuaryScore,                 // Bard                  # 128
            BT3SpellIndex.BringaroundBallad,              // Bard                  # 129
            BT3SpellIndex.RhymeOfDuotime,                 // Bard                  # 130
            BT3SpellIndex.WatchwoodMelody,                // Bard                  # 131
            BT3SpellIndex.KielsOverture,                  // Bard                  # 132
            BT3SpellIndex.MinstrelShield                  // Bard                  # 133
        };

        public List<BT3Spell> Spells
        {
            get { return m_spells; }
        }

        public static BT3Spell[] HealingSpells = new BT3Spell[]
        {
            BT3.Spells[(int) BT3SpellIndex.WordOfHealing],
            BT3.Spells[(int) BT3SpellIndex.FleshRestore],
            BT3.Spells[(int) BT3SpellIndex.FleshAnew],
            BT3.Spells[(int) BT3SpellIndex.Restoration],
            BT3.Spells[(int) BT3SpellIndex.Dispossess],
            BT3.Spells[(int) BT3SpellIndex.BeyondDeath],
            BT3.Spells[(int) BT3SpellIndex.HealAll],
        };

        public BT3SpellList()
        {
            m_spells = new List<BT3Spell>(88);
            m_spells.Add(new BT3Spell(BT3SpellIndex.MageFlame, SpellType.Conjurer, 1, "MAFL", "Mage Flame", 2, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Medium, "Light (3 squares)", "A small self-propelled \"torch\" appears and floats above the spellcaster as he travels (8 hours)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ArcFire, SpellType.Conjurer, 1, "ARFI", "Arc Fire", 3, SpellWhen.CombatAnywhere, SpellTarget.Monster10Feet, SpellDuration.Instant, "1d4 damage/level", "A fan of blue flame jets from the spellcaster's fingers, inflicting 1 to 4 hits of damage, which are multiplied by the spellcaster's level, on the selected opponent."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.TrapZap, SpellType.Conjurer, 1, "TRZP", "Trap Zap", 2, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Instant, "Disarm traps", "Disarms any trap within 30 feet (3 squares), in the direction the spellcaster is facing. TRZP also works on chests, but still costs the same amount of spell points."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Freeze, SpellType.Conjurer, 2, "FRFO", "Freeze", 3, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Combat, "+1 AC", "Foes binds your enemies in magical force, slowing them down and making them easier to hit."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.KielsMagicCompass, SpellType.Conjurer, 2, "MACO", "Kiel's Magic Compass", 3, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Medium, "Show direction", "A compass of shimmering magelight appears above the party and shows the direction they face (64 hours)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.WordOfHealing, SpellType.Conjurer, 2, "WOHL", "Word Of Healing", 4, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "+4d4 HP", "Lets the spellcaster heal a party member who suffers from 4 to 16 points of damage by uttering a single word."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.LesserRevelation, SpellType.Conjurer, 3, "LERE", "Lesser Revelation", 5, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Long, "Light (4 squares); Reveal secrets", "An extended MAGE FLAME spell that also reveals secret doors (24 hours)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Levitation, SpellType.Conjurer, 3, "LEVI", "Levitation", 4, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Float over traps and up portals", "Partially nullifies gravity causing the party to float over traps, or up or down through portals (8 hours)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Warstrike, SpellType.Conjurer, 3, "WAST", "Warstrike", 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup20Feet, SpellDuration.Instant, "4d5 damage", "An energy strewn shot from the spellcaster's finger that sizzles a group of Foes for 5 to 20 hits of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.EliksInstantWolf, SpellType.Conjurer, 4, "INWO", "Elik's Instant Wolf", 6, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Wolf", "Summons a giant, extremely fierce wolf to join your party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FleshRestore, SpellType.Conjurer, 4, "FLRE", "Flesh Restore", 6, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "+10d4 HP; Cure poison/nuts", "A powerful healing spell that restores 10 to 40 hit points to a party member, including those stricken with insanity or poisoning."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.GreaterRevelation, SpellType.Conjurer, 5, "GRRE", "Greater Revelation", 7, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Long, "Light (5 squares); Reveal secrets", "Operates like LESSER REVELATION, but illuminates a wider area for a longer period of time (32 hours)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Shocksphere, SpellType.Conjurer, 5, "SHSP", "Shock-sphere", 7, SpellWhen.AnywhereAnytime, SpellTarget.MonsterGroup30Feet, SpellDuration.Instant, "10d4 damage", "Creates a large globe of intense electrical energy that envelops a group of enemies and inflicts 10 to 40 hits of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FleshAnew, SpellType.Conjurer, 6, "FLAN", "Flesh Anew", 9, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "+10d4 HP; Cure poison/nuts", "Operates like FLRE, but affects every member of the party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.MajorLevitation, SpellType.Conjurer, 6, "MALE", "Major Levitation", 8, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "Float over traps and up portals", "Operates like LEVI from level 3, but it lasts until dispelled (i.e., until the spell is terminated by some event such as activating an anti-magic square)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Regeneration, SpellType.Conjurer, 7, "REGN", "Regeneration", 12, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "Restore HP", "A health spell that revives all the hit points for one lucky member of the party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ApportArcane, SpellType.Conjurer, 7, "APAR", "Apport Arcane", 15, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Change location", "Teleports the party within a dungeon to any location that's not protected by a teleportation shield. Also teleports the party between cities that are in the range of +1 to 6. Your party always arrives in the city's Adventurers' Guild."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FarFoe, SpellType.Conjurer, 7, "FAFO", "Far Foe", 18, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "Move group 40' away", "Moves a group of Foes 40 feet further away from your party, up to a maximum distance of 90 feet."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.EliksInstantSlayer, SpellType.Conjurer, 7, "INSL", "Elik's Instant Slayer", 12, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Slayer", "Materializes a slayer that joins your party. What's a slayer? The name speaks for itself..."));

            m_spells.Add(new BT3Spell(BT3SpellIndex.VorpalPlating, SpellType.Magician, 1, "VOPL", "Vorpal Plating", 3, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Combat, "+2d4 damage", "Causes the weapon (or hands) of a party member to emit a magical field that inflicts 2 to 8 points of additional damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.QuickFix, SpellType.Magician, 1, "QUFI", "Quick Fix", 3, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "+8 HP", "Regenerates a character for precisely 8 hit points up to the character's maximum hit point level."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ScrySite, SpellType.Magician, 1, "SCSI", "Scry Site", 2, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Show location", "Causes a dungeon or wilderness pathway to reveal the party's location."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.HolyWater, SpellType.Magician, 2, "HOWA", "Holy Water", 4, SpellWhen.CombatAnywhere, SpellTarget.Monster10Feet, SpellDuration.Instant, "6d4 damage to evil/supernatural", "Holy water sprays from the spellcaster's fingers, inflicting 6 to 24 points of damage on any foe of evil or supernatural origin."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.MageGauntlets, SpellType.Magician, 2, "MAGA", "Mage Gauntlets", 5, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Combat, "+4d4 damage", "Makes the hands (or weapon) of a party member more deadly by adding 4 to 16 points of damage to every wound it inflicts on a foe."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.AreaEnchant, SpellType.Magician, 2, "AREN", "Area Enchant", 5, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Short, "Locate stairways", "Causes the dungeon walls within 30 feet (3 squares) of a stairway to call out if the party is headed toward the stairs (8 hours)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.YbarrasMysticShield, SpellType.Magician, 3, "MYSH", "Ybarra's Mystic Shield", 6, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Medium, "-2 AC", "Causes the air in front of the party to form an invisible shield that's as hard as metal and precedes the party as they move (8 hours)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.OsconsOgrestrength, SpellType.Magician, 3, "OGST", "Oscon's Ogrestrength", 6, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Combat, "+7 To-Hit/Damage", "Endows a specific party member with the strength of Elik's ogre for the duration of the battle."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Starflare, SpellType.Magician, 3, "STFL", "Starflare", 6, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup40FeetDiminish, SpellDuration.Instant, "10d4 damage", "Ignites the air around your enemies, scorching them for 10 to 40 damage points."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.SpectreTouch, SpellType.Magician, 4, "SPTO", "Spectre Touch", 8, SpellWhen.CombatAnywhere, SpellTarget.Monster70Feet, SpellDuration.Instant, "15d4 damage", "Drains a single enemy of 15 to 60 hit points; like a touch from death itself."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.DragonBreath, SpellType.Magician, 4, "DRBR", "Dragon Breath", 7, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup30Feet, SpellDuration.Instant, "11d4 damage", "Lets the spellcaster breathe fire at a group of monsters, inflicting 11 to 44 points of damage on each monster."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Antimagic, SpellType.Magician, 5, "ANMA", "Anti-magic", 8, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Combat, "+2 Magic Resist", "Causes the ground to absorb a portion of the spells cast at the party by monsters. Often allows the party to escape unharmed. This spell also aids in disbelieving illusions and shielding against magical fire such as Dragon Breath."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.GiantStrength, SpellType.Magician, 5, "GIST", "Giant Strength", 10, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Instant, "+10 To-Hit/Damage", "Instills tremendous power in your party, increasing their strike ability by 10."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.PhaseDoor, SpellType.Magician, 6, "PHDO", "Phase Door", 10, SpellWhen.AnywhereAnytime, SpellTarget.OneWall, SpellDuration.OneMove, "Allow passage through wall", "Turns almost any wall to air for exactly one move."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.YbarrasMysticalCoatOfArmor, SpellType.Magician, 6, "YMCA", "Ybarra's Mystical Coat Of Armor", 10, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "-2 AC", "Operates like YBARRA'S MYSTIC SHIELD, but lasts indefinitely."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Restoration, SpellType.Magician, 7, "REST", "Restoration", 25, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Heal all HP; Cure poison/nuts", "Regenerates the body of every party member to perfect condition; it even cures insanity or poisoning."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Deathstrike, SpellType.Magician, 7, "DEST", "Deathstrike", 16, SpellWhen.CombatAnywhere, SpellTarget.Monster10Feet, SpellDuration.Instant, "Kill enemy", "Very likely to instantly kill one selected enemy."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.IceStorm, SpellType.Magician, 7, "ICES", "Ice Storm", 11, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup50Feet, SpellDuration.Instant, "20d4 damage", "Pummels a group of monsters with chunks of ice, causing 20 to 80 points of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.StoneToFlesh, SpellType.Magician, 7, "STON", "Stone to Flesh", 20, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "Cure stone", "Takes a character who has been turned to stone and restores him to his natural flesh state."));

            m_spells.Add(new BT3Spell(BT3SpellIndex.MangarsMindJab, SpellType.Sorcerer, 1, "MIJA", "Mangar's Mind Jab", 3, SpellWhen.CombatAnywhere, SpellTarget.Monster40FeetDiminish, SpellDuration.Instant, "2d4 damage/level", "Casts a concentrated blast of energy at one opponent, inflicting 2 to 8 points of damage for each experience level of the spellcaster."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.PhaseBlur, SpellType.Sorcerer, 1, "PHBL", "Phase Blur", 2, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Combat, "-1 AC", "Causes the entire party to waver and blur in the sight of the enemy, rendering your party difficult to strike."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.LocateTraps, SpellType.Sorcerer, 1, "LOTR", "Locate Traps", 2, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Short, "Detect traps", "Heightens the spellcaster's awareness in order to detect traps within 30' along the direction the spellcaster is facing (5-9 hours)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Disbelieve, SpellType.Sorcerer, 2, "DISB", "Disbelieve", 4, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Dispel illusions", "Reveals the true nature of any attacking illusion, causing it to vanish."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.WindWarrior, SpellType.Sorcerer, 2, "WIWA", "Wind Warrior", 5, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Mercenary Illusion", "Creates the illusion of a battle-ready ninja among the ranks of your party. The illusionary ninja will fight until defeated or disbelieved."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.WordOfFear, SpellType.Sorcerer, 2, "FEAR", "Word Of Fear", 4, SpellWhen.AnywhereAnytime, SpellTarget.MonsterGroup, SpellDuration.Combat, "-1 To-Hit/Damage", "An incantation that causes a group of enemies to quake in fear, thus reducing their ability to attack and inflict damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.WindOgre, SpellType.Sorcerer, 3, "WIOG", "Wind Ogre", 6, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Ogre Illusion", "Similar to ELIK'S OGRE, but the WIOG is an illusion."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.KylearansInvisibilitySpell, SpellType.Sorcerer, 3, "INVI", "Kylearan's Invisibility Spell", 6, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "-4 AC", "An invocation that renders the entire party nearly invisible to the enemy."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.SecondSight, SpellType.Sorcerer, 3, "SESI", "Second Sight", 6, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Medium, "Detect special areas", "Heightens the awareness of the spellcaster in order to detect all manner of traps and tricks that lie directly ahead (12 hours)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.CatEyes, SpellType.Sorcerer, 4, "CAEY", "Cat Eyes", 7, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "Light (5 squares)", "Endows the entire party with perfect night vision for an indefinite period of time."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.WindDragon, SpellType.Sorcerer, 4, "WIDR", "Wind Dragon", 12, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Red Dragon Illusion", "Creates an illusionary red dragon to join the ranks of your party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.DisruptIllusion, SpellType.Sorcerer, 5, "DIIL", "Disrupt Illusion", 8, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.Combat, "Dispel illusions and dopplegangers", "Destroys any illusions among the ranks of the enemy and prevents new illusions from appearing. This spell also exposes any Dopplegangers within the party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.MangarsMindBlade, SpellType.Sorcerer, 5, "MIBL", "Mangar's Mind Blade", 10, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters30FeetDiminish, SpellDuration.Instant, "25d4 damage", "Strikes every opposing group within range with an explosion of energy capable of inflicting 25 to 100 points of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.WindGiant, SpellType.Sorcerer, 6, "WIGI", "Wind Giant", 11, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Storm Giant Illusion", "Creates an illusionary storm giant that joins and fights for your party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.SorcererSight, SpellType.Sorcerer, 6, "SOSI", "Sorcerer Sight", 11, SpellWhen.AnywhereAnytime, SpellTarget.ThirtyFeet, SpellDuration.Indefinite, "Detect special areas", "Operates like the SECOND SIGHT spell, but lasts indefinitely."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Rimefang, SpellType.Sorcerer, 7, "RIME", "Rimefang", 20, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters40Feet, SpellDuration.Instant, "Summon Archmage Illusion", "Creates an illusionary Archmage to join your party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.WindHero, SpellType.Sorcerer, 7, "WIHE", "Wind Hero", 16, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Hero Illusion", "Creates an illusionary hero to join your party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.MageMaelstrom, SpellType.Sorcerer, 7, "MAGM", "Mage Maelstrom", 40, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "60d4 damage/stone/kill", "Assaults a group of spellcasters and may do one of the following: inflict 60 to 240 points of damage, turn them to stone, or kill them outright. However, because the maelstrom is illusionary in nature, a disbelieving monster can totally disarm it."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Preclusion, SpellType.Sorcerer, 7, "PREC", "Preclusion", 50, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.Instant, "Prevent Summoning", "Keeps the enemy from being able to summon any creatures."));

            m_spells.Add(new BT3Spell(BT3SpellIndex.SummonElemental, SpellType.Wizard, 1, "SUEL", "Summon Elemental", 10, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Fire Elemental", "Creates a fire-being from the raw elements of the universe to join and fight for your party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FanskarsForceFocus, SpellType.Wizard, 1, "FOFO", "Fanskar's Force Focus", 11, SpellWhen.AnywhereAnytime, SpellTarget.MonsterGroup10Feet, SpellDuration.Instant, "24d4 damage", "Lands a cone of gravitational energy on a group of your Foes, inflicting 25 to 100 points of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.PrimeSummoning, SpellType.Wizard, 2, "PRSU", "Prime Summoning", 14, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon undead creature", "Coerces a powerful undead creature to unwillingly join your party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.DemonBane, SpellType.Wizard, 2, "DEBA", "Demon Bane", 11, SpellWhen.CombatAnywhere, SpellTarget.Monster30Feet, SpellDuration.Instant, "100d4 damage to evil/supernatural", "Inflicts 100 to 400 points of damage on a single creature of evil or supernatural origin."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FlameColumn, SpellType.Wizard, 3, "FLCO", "Flame Column", 14, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup30Feet, SpellDuration.Instant, "22d4 damage", "Creates a cyclone of flame that lashes out and delivers 22 to 88 points of damage to a group of your Foes."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Dispossess, SpellType.Wizard, 3, "DISP", "Dispossess", 12, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "Cure \"Possessed\" condition", "Returns a possessed party member to the normal state of consciousness."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.SummonHerb, SpellType.Wizard, 4, "HERB", "Summon Herb", 13, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Herb", "Summons Herb to join your party. Herb is really busy, but he'll hang out with your party for a while if you need him."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.AnimateDead, SpellType.Wizard, 4, "ANDE", "Animate Dead", 14, SpellWhen.CombatAnywhere, SpellTarget.Character, SpellDuration.Combat, "Allow dead character to fight", "Reanimates a dead character with living strength so he or she attacks enemies as if truly alive -- combat only spell."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.BaylorsSpellBind, SpellType.Wizard, 5, "SPBI", "Baylor's Spell Bind", 16, SpellWhen.AnywhereAnytime, SpellTarget.Monster, SpellDuration.Instant, "Causes \"Possessed\" condition", "If successful, this spell possesses the mind of an enemy and forces him to join and fight for your party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.StoralsSoulWhip, SpellType.Wizard, 5, "SOWH", "Storal's Soul Whip", 13, SpellWhen.CombatAnywhere, SpellTarget.Monster70Feet, SpellDuration.Instant, "50d4 damage", "Whips out a tendril of psionic (mind) power to strike a selected foe, inflicting 50 to 200 damage points."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.GreaterSummoning, SpellType.Wizard, 6, "GRSU", "Greater Summoning", 22, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon elemental creature", "Operates like PRIME SUMMONING but causes a powerful elemental creature to appear and fight for the party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.BeyondDeath, SpellType.Wizard, 6, "BEDE", "Beyond Death", 18, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "Cure \"Dead\" condition", "Restores life and one hit point to a deceased character."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.WacumsWizardWar, SpellType.Wizard, 7, "WIZW", "Wacum's Wizard War", 16, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup50Feet, SpellDuration.Instant, "50d4 damage", "Creates a pyrotechnical storm over a group of monsters, inflicting 50 to 200 damage points."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.DemonStrike, SpellType.Wizard, 7, "DMST", "Demon Strike", 25, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup50Feet, SpellDuration.Instant, "200d2 damage", "Unleashes the terrorizing power of demons into the enemy ranks, causing 200 to 400 points of damage."));

            m_spells.Add(new BT3Spell(BT3SpellIndex.OsconsHaltfoe, SpellType.Archmage, 1, "HAFO", "Oscon's Haltfoe", 15, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.OneRound, "Prevent action", "If successful, this spell causes every attacking group to do nothing during the next round."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.MeleeMen, SpellType.Archmage, 1, "MEME", "Melee Men", 20, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "Pull into melee range", "Pulls an attacking group into melee range (10') regardless of how far they were when they began attacking."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Batchspell, SpellType.Archmage, 2, "BASP", "Batchspell", 28, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "GRRE/YMCA/SOSI/MALE/MACO", "Performs the following multiple spells: GREATER REVELATION, YBARRA'S MYSTICAL COAT OF ARMOR, SORCERER SIGHT, MAJOR LEVITATION, and KIEL'S MAGIC COMPASS."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Camaraderie, SpellType.Archmage, 3, "CAMR", "Camaraderie", 26, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Calm hostile monsters", "Has a 50% chance of calming any or all monsters in your party that have turned hostile."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FanskarsNightLance, SpellType.Archmage, 4, "NILA", "Fanskar's Night Lance", 30, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup60Feet, SpellDuration.Instant, "100d4 damage", "Launches a chilling missile against a group of Foes, inflicting 100 to 400 damage points."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.HealAll, SpellType.Archmage, 5, "HEAL", "Heal All", 50, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Resurrect and heal all", "A Beyond Death spell that resurrects every dead party member and heals all wounds, paralysis, and insanity."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.TheBrothersKringle, SpellType.Archmage, 6, "BRKR", "The Brothers Kringle", 60, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Kringle Bro", "The brothers are always ready to help friends in trouble. Enough brothers appear to fill the empty slots in your party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.MangarsMallet, SpellType.Archmage, 7, "MAMA", "Mangar's Mallet", 80, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.Instant, "200d4 damage", "Inflicts 200 to 800 bone-crushing damage points against every monster group you face."));

            m_spells.Add(new BT3Spell(BT3SpellIndex.Vitality, SpellType.Chronomancer, 1, "VITL", "Vitality", 12, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "+2d4 HP/level", "Invigorates a character by healing 4 to 8 hit points times the spellcaster's level."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ToArboria, SpellType.Chronomancer, 1, "ARBO", "To Arboria", 10, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport to Arboria", "Teleports the party to Arboria if they are in the Skara Brae Wilderness at (6,14)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FromArboria, SpellType.Chronomancer, 1, "ENIK", "From Arboria", 10, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport from Arboria", "Teleports the party from Arboria back to Skara Brae."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Witherfist, SpellType.Chronomancer, 2, "WIFI", "Witherfist", 20, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup20Feet, SpellDuration.Instant, "300d2 damage", "Crushes a group of enemies under a huge fist of power for 300 to 600 points of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FrostForce, SpellType.Chronomancer, 2, "COLD", "Frost Force", 20, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup80Feet, SpellDuration.Instant, "50d8 damage", "Blasts the enemy with a deadly frost for 50 to 400 points of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ToGelidia, SpellType.Chronomancer, 2, "GELI", "To Geledia", 15, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport to Geledia", "Teleports the party to Geledia if they are in the Skara Brae Wilderness at (0,0)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FromGelidia, SpellType.Chronomancer, 2, "ECUL", "From Geledia", 15, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport from Geledia", "Teleports the party from Geledia back to Skara Brae."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.GodFire, SpellType.Chronomancer, 3, "GOFI", "God Fire", 25, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup80Feet, SpellDuration.Instant, "60d4 damage", "A holy spell where blazing red fires are sent from the angry gods to roast the enemy for 60 to 240 damage points."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.StunForce, SpellType.Chronomancer, 3, "STUN", "Stun Force", 30, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.Instant, "50d4 damage", "An electric spell that gives the enemy a high-voltage zap for 50 to 200 damage points."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ToLucencia, SpellType.Chronomancer, 3, "LUCE", "To Lucencia", 20, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport to Lucencia", "Teleports the party to Lucencia if they are in the Skara Brae Wilderness at (17,2)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FromLucencia, SpellType.Chronomancer, 3, "ILEG", "From Lucencia", 20, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport from Lucencia", "Teleports the party from Lucencia back to Skara Brae."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.LuckChant, SpellType.Chronomancer, 4, "LUCK", "Luck Chant", 45, SpellWhen.CombatAnywhere, SpellTarget.Party, SpellDuration.Combat, "+8 Magic Resist", "Increases your chances of hitting or defending by eight points."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FarDeath, SpellType.Chronomancer, 4, "FADE", "Far Death", 50, SpellWhen.CombatAnywhere, SpellTarget.Monster30Feet, SpellDuration.Instant, "Kill monster", "A long-range spell that drops a distant foe dead in its tracks."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ToKinestia, SpellType.Chronomancer, 4, "KINE", "To Kinestia", 25, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport to Kinestia", "Teleports the party to Kinestia if they are in the Skara Brae Wilderness at (2,16)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FromKinestia, SpellType.Chronomancer, 4, "OBRA", "From Kinestia", 25, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport from Kinestia", "Teleports the party from Kinestia back to Skara Brae."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Identify, SpellType.Chronomancer, 5, "WHAT", "Identify", 60, SpellWhen.AnywhereAnytime, SpellTarget.Item, SpellDuration.Instant, "Identify item", "Cast this spell on something to find out just what the heck it is."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Youth, SpellType.Chronomancer, 5, "OLAY", "Youth", 60, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "Cure old", "Coats a character with a light, fragrant lotion to cure oldness."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ToTenabrosia, SpellType.Chronomancer, 5, "OLUK", "To Tenabrosia", 30, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport to Tenabrosia", "Teleports the party to Tenabrosia if they are in the Skara Brae Wilderness at (19,10)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FromTenabrosia, SpellType.Chronomancer, 5, "ECEA", "From Tenabrosia", 30, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport from Tenabrosia", "Teleports the party from Tenabrosia back to Skara Brae."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.GraveRobber, SpellType.Chronomancer, 6, "GRRO", "Grave Robber", 65, SpellWhen.AnywhereAnytime, SpellTarget.Character, SpellDuration.Instant, "Raise dead with full HP", "Casts Beyond Death and Regeneration for a life-giving combination of spells."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ForceOfTarjan, SpellType.Chronomancer, 6, "FOTA", "Force of Tarjan", 70, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "Witherfist/Sandstorm", "Casts Witherfist and Sandstorm for a double offensive punch."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ToTarmitia, SpellType.Chronomancer, 6, "AECE", "To Tarmitia", 35, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport to Tarmitia", "Teleports the party to Tarmitia if they are in the Skara Brae Wilderness at (10,9)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FromTarmitia, SpellType.Chronomancer, 6, "KULO", "From Tarmitia", 35, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport from Tarmitia", "Teleports the party from Tarmitia back to Skara Brae."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ShadowShield, SpellType.Chronomancer, 7, "SHSH", "Shadow Shield", 60, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Indefinite, "-4 AC", "Casts a gray shadow around the party, and lowers their armor class by 4"));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FatalFist, SpellType.Chronomancer, 7, "FAFI", "Fatal Fist", 100, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.Instant, "400d4 damage", "Crushes the enemy under an unearthly gravitational force for 400 to 1500 points of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.ToMalefia, SpellType.Chronomancer, 7, "EVIL", "To Malefia", 50, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport to Malefia", "Teleports the party to Malefia if they are in the Skara Brae Wilderness at (18,18)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.FromMalefia, SpellType.Chronomancer, 7, "LIVE", "From Malefia", 50, SpellWhen.NonCombatAnywhere, SpellTarget.Party, SpellDuration.Instant, "Teleport from Malefia", "Teleports the party from Malefia back to Skara Brae."));

            m_spells.Add(new BT3Spell(BT3SpellIndex.EarthDagger, SpellType.Geomancer, 1, "EADA", "Earth Dagger", 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup40Feet, SpellDuration.Instant, "200d4 damage", "Cuts down the enemy with holy daggers for 200 to 800 points of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.EarthSong, SpellType.Geomancer, 1, "EASO", "Earth Song", 5, SpellWhen.AnywhereAnytime, SpellTarget.Level, SpellDuration.Instant, "Show traps", "Reveals all booby-trapped areas that can injure the party."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.EarthWard, SpellType.Geomancer, 1, "EAWA", "Earth Ward", 8, SpellWhen.AnywhereAnytime, SpellTarget.Level, SpellDuration.Instant, "Disarm traps", "Casts the Trap Zap spell on the entire level."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Trebuchet, SpellType.Geomancer, 2, "TREB", "Trebuchet", 10, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.Instant, "150d4 damage", "Fries all foes with wickedly hot flames for 150 to 600 points."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.EarthElemental, SpellType.Geomancer, 2, "EAEL", "Earth Elemental", 15, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Instant, "Summon Earth Elemental", "Summons an Earth Elemental, which is a creature created from the raw elements of the earth."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.WallWarp, SpellType.Geomancer, 2, "WAWA", "Wall Warp", 15, SpellWhen.AnywhereAnytime, SpellTarget.OneWall, SpellDuration.Indefinite, "Remove wall", "Works like Phase Door until the party leaves."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Petrify, SpellType.Geomancer, 3, "ROCK", "Petrify", 18, SpellWhen.CombatAnywhere, SpellTarget.Monster60Feet, SpellDuration.Instant, "Stone monster", "Turns an enemy up to 60 feet away into the hardest stone."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.RoscoesAlert, SpellType.Geomancer, 3, "ROAL", "Roscoe's Alert", 20, SpellWhen.AnywhereAnytime, SpellTarget.Level, SpellDuration.Instant, "Show anti-magic squares", "Reveals to the party where the anti-magic areas are."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.SuccorSong, SpellType.Geomancer, 4, "SUSO", "Succor Song", 20, SpellWhen.AnywhereAnytime, SpellTarget.Level, SpellDuration.Instant, "Show HP regen squares", "Shows all heal-party squares, so your party can put an end to their weakness and pain."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Sandstorm, SpellType.Geomancer, 4, "SAST", "Sandstorm", 25, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup, SpellDuration.Instant, "Push monsters 60'", "With a violent swirl of sand, all foes are whipped back 60 feet."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Sanctuary, SpellType.Geomancer, 5, "SANT", "Sanctuary", 30, SpellWhen.AnywhereAnytime, SpellTarget.Level, SpellDuration.Instant, "Show SP regen squares", "Shows all mage regeneration squares, so your spellcasters can be refreshed."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.GlacierStrike, SpellType.Geomancer, 5, "GLST", "Glacier Strike", 40, SpellWhen.CombatAnywhere, SpellTarget.Monster90Feet, SpellDuration.Instant, "400d4 damage", "Impales the enemy with an icy stalagmite, causing 400 to 1600 points of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Pathfinder, SpellType.Geomancer, 6, "PATH", "Pathfinder", 40, SpellWhen.AnywhereAnytime, SpellTarget.Level, SpellDuration.Instant, "Show map", "An instant map, this shows the entire maze that the party's in."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.MagmaBlast, SpellType.Geomancer, 6, "MABA", "Magma Blast", 50, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup50Feet, SpellDuration.Instant, "300d4 damage", "Burns a group of foes with a blast of hot, fiery magma for 300 to 1200 points of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.JoltBolt, SpellType.Geomancer, 7, "JOBO", "Jolt Bolt", 60, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.Instant, "400d4 damage", "Wrenches the earth below the enemy, smashing them to the ground and gives them a jolting electrical shock to cause 400 to 1600 points of damage."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.EarthMaw, SpellType.Geomancer, 7, "EAMA", "Earth Maw", 80, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup50Feet, SpellDuration.Instant, "Kill monsters", "Commands the ground beneath the enemy's feet to open wide and drop the foes in, so they're never seen again."));

            m_spells.Add(new BT3Spell(BT3SpellIndex.GillesGills, SpellType.Miscellaneous, 1, "GILL", "Gilles Gills", 10, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Medium, "+100 underwater steps", "This survival spell lets your party breathe under water.  It is cumulative in effect; casting it more than once will extend the amount of time you can spend underwater."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.DivineIntervention, SpellType.Miscellaneous, 1, "DIVA", "Divine Intervention", 250, SpellWhen.AnywhereAnytime, SpellTarget.AllPlayersAndMonsters, SpellDuration.Instant, "Heal all/MAMA/+20 AC", "This powerful spell earns its name by doing the following:  1) Turns illusionary characters into real characters; 2) Cures characters of all illnesses but age; and 3) Restores all hit points to the party.  If you're in combat, it also does the following:  1) Lowers your armor class, saving throw, to hit, and damage by 20 points; 2) Increases your attack by eight points; and 3) Casts Mangar's Mallet."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.Gotterdamurung, SpellType.Miscellaneous, 1, "NUKE", "Gotterdamurung", 150, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters, SpellDuration.Instant, "2000+280d4 damage", "The finest in offensive obliteration, this spell annihilates the opponent for 2000 damage points (approximately 2000+280d4)."));

            m_spells.Add(new BT3Spell(BT3SpellIndex.SirRobinsTune, SpellType.Bard, 1, "", "Sir Robin's Tune", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Allow running, prevent summoning", "This lets you run away from attackers as long as the combat has not yet begun.  During combat, this keeps the monsters from calling for additional help (90 min)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.SafetySong, SpellType.Bard, 1, "", "Safety Song", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Prevent random encounters", "Sets up an an anti-monster aura, so foes won't randomly attack you (90 min)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.SanctuaryScore, SpellType.Bard, 1, "", "Sanctuary Score", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "-1 AC", "Lowers the party's armor class level up to a maximum of 15 points. (90 min)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.BringaroundBallad, SpellType.Bard, 1, "", "Bringaround Ballad", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "+1 HP/15min, +1 HP/round", "In non-combat situations, this rejuvenates the Bard's hit points.  During combat, this song will affect everyone in your party, including the Bard. (90 min)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.RhymeOfDuotime, SpellType.Bard, 1, "", "Rhyme of Duotime", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "2x SP regen, +1 attack", "In non-combat situations, this regenerates the mage's spell points.  During combat, it gives the party an extra attack. (90 min)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.WatchwoodMelody, SpellType.Bard, 1, "", "The Watchwood Melody", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Light (3 squares)", "This creates light so you can find your way around.  May even work in anti - magic zones. (90 min)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.KielsOverture, SpellType.Bard, 1, "", "Kiel's Overture", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "Compass, Trebuchet", "In non-combat situations, this calls up a compass so you can get your bearings.  During combat, this casts the monster-frying Trebuchet spell for one round. (90 min)."));
            m_spells.Add(new BT3Spell(BT3SpellIndex.MinstrelShield, SpellType.Bard, 1, "", "Minstrel Shield", 0, SpellWhen.AnywhereAnytime, SpellTarget.Party, SpellDuration.Short, "-? AC, half damage", "In non-combat situations, this lowers your armor class.  During combat, it also partially shields your party so they only take half damage from monster attacks. (90 min) (This song does not appear to actually do anything in the PC version of Bard's Tale 3)."));
        }
    }

    public class BT3Spell : BTSpell
    {
        public BT3SpellIndex Index;
        public override int BasicIndex { get { return (int)Index; } }
        public override bool UsesLevelOnly { get { return true; } }

        private string WhereLearned(BT3SpellIndex index)
        {
            switch (Index)
            {
                case BT3SpellIndex.SirRobinsTune:
                case BT3SpellIndex.SafetySong:
                case BT3SpellIndex.SanctuaryScore:
                case BT3SpellIndex.BringaroundBallad:
                case BT3SpellIndex.RhymeOfDuotime:
                case BT3SpellIndex.WatchwoodMelody: return "Available to Bard class at level 1";
                case BT3SpellIndex.MinstrelShield: return "From bard in Black Scar (6,7) for 30000 Gold";
                case BT3SpellIndex.Gotterdamurung: return "From the Wizard Guild in Black Scar (9,7) for 50000 Gold";
                case BT3SpellIndex.DivineIntervention: return "From the Wizard Guild in Celaria Bree (5,4) for 50000 Gold";
                case BT3SpellIndex.GillesGills: return "From fisherman in Arboria (1,11) for 500 Gold or from the Wizard Guild in Ciera Brannia (6,5) for 10000 Gold";
                case BT3SpellIndex.KielsOverture: return "From bard in Celaria Bree (9,13) for 60000 Gold";
                default: return "";
            }
        }

        public BT3Spell(BT3SpellIndex index, SpellType type, int level, string abbrev, string name, int sp, SpellWhen when, SpellTarget target, SpellDuration duration, string shortDesc, string desc)
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
            if (Type != SpellType.Miscellaneous && Type != SpellType.Bard)
                Learned = String.Format("Available to {0} class at level {1}", MMSpell.TypeString(Type), Level * 2 - 1);
            else
                Learned = WhereLearned(Index);
            Cost = new SpellCost(sp);
        }

        public static BT3Spell None
        {
            get { return new BT3Spell(BT3SpellIndex.None, SpellType.Unknown, 0, "", "None", 0, SpellWhen.AnywhereAnytime, SpellTarget.Unknown, SpellDuration.Instant, "", ""); }
        }

        public override Keys[] GetKeys(BaseCharacter character)
        {
            if (Type == SpellType.Bard)
                return new Keys[] { Keys.D1 + (Index - BT3SpellIndex.SirRobinsTune) };
            BT3Character bt3Char = character as BT3Character;
            if (bt3Char == null)
                return null;

            if (!bt3Char.Spells.IsKnown((int) Index, bt3Char.BasicClass))
                return null;

            // The keys to send depends on which spells the character knows, because they are all shown in a list of 9 each
            // Need to essentially hit the down arrow once for each spell the character knows before the desired one
            // but replace every nine arrows with a PageDown for efficiency
            BT3SpellIndex spell = BT3SpellIndex.MageFlame;
            int iDown = 0;
            while (spell != Index)
            {
                if (spell >= BT3SpellIndex.Gotterdamurung)
                    break;
                if (bt3Char.Spells.IsKnown((int)spell, bt3Char.BasicClass))
                    iDown++;
                spell++;
            }

            int iPageDown = iDown / 9;
            iDown = iDown % 9;

            List<Keys> keys = new List<Keys>(iPageDown + iDown + 1);
            keys.Add(Keys.C);       // if we're in combat, press C to cast the spell (and doesn't really hurt anything outside combat)
            keys.Add(Keys.Home);    // in case something else is already selected
            for (int i = 0; i < iPageDown; i++)
                keys.Add(Keys.PageDown);
            for (int i = 0; i < iDown; i++)
                keys.Add(Keys.Down);
            keys.Add(Keys.Enter);
            return keys.ToArray();
        }

        public override string MultiLineDescription { get { return base.MultiLineDescription + "\r\nLearned: " + Learned; } }
    }
}
