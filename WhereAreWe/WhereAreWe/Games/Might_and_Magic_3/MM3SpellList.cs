using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public enum MM3InternalSpellIndex
    {
        First = 0,
        Light = 0,
        Awaken = 1,
        FirstAid = 2,
        FlyingFist = 3,
        DetectMagic = 4,
        ElementalArrow = 5,
        Revitalize = 6,
        CureWounds = 7,
        Sparks = 8,
        EnergyBlast = 9,
        Sleep = 10,
        Pain = 11,
        CreateRope = 12,
        ToxicCloud = 13,
        SuppressPoison = 14,
        ProtFromElements = 15,
        TurnUndead = 16,
        Jump = 17,
        AcidStream = 18,
        SuppressDisease = 19,
        Silence = 20,
        Blessed = 21,
        Levitate = 22,
        WizardEye = 23,
        IdentifyMonster = 24,
        HolyBonus = 25,
        PowerCure = 26,
        NaturesCure = 27,
        LightningBolt = 28,
        Immobilize = 29,
        Heroism = 30,
        WalkOnWater = 31,
        FrostBite = 32,
        LloydsBeacon = 33,
        PowerShield = 34,
        CurePoison = 35,
        Fireball = 36,
        DetectMonster = 37,
        AcidSpray = 38,
        ColdRay = 39,
        CureDisease = 40,
        NaturesGate = 41,
        TimeDistortion = 42,
        FeebleMind = 43,
        DeadlySwarm = 44,
        Teleport = 45,
        FingerOfDeath = 46,
        CureParalysis = 47,
        Paralyze = 48,
        DragonBreath = 49,
        SuperShelter = 50,
        FieryFlail = 51,
        CreateFood = 52,
        TownPortal = 53,
        StoneToFlesh = 54,
        RechargeItem = 55,
        FantasticFreeze = 56,
        Duplication = 57,
        Disintegrate = 58,
        RaiseDead = 59,
        HalfForMe = 60,
        Etherealize = 61,
        DancingSword = 62,
        PrismaticLight = 63,
        MoonRay = 64,
        MassDistortion = 65,
        EnchantItem = 66,
        Incinerate = 67,
        ElementalStorm = 68,
        HolyWord = 69,
        Resurrect = 70,
        MegaVolts = 71,
        Inferno = 72,
        SunRay = 73,
        Implosion = 74,
        StarBurst = 75,
        DivineIntervention = 76,
        Last = 76,
        None = 255,
    }

    public enum MM3SpellIndex
    {
        // Cleric Spells
        LightCleric,
        AwakenCleric,
        FirstAid,
        FlyingFist,
        Revitalize,
        CureWounds,
        Sparks,
        ProtectionFromElements,
        Pain,
        SuppressPoison,
        SuppressDisease,
        TurnUndead,
        Silence,
        Blessed,
        HolyBonus,
        PowerCure,
        Heroism,
        Immobilize,
        ColdRay,
        CurePoison,
        AcidSpray,
        CureDisease,
        CureParalysis,
        Paralyze,
        CreateFood,
        FieryFlail,
        TownPortal,
        StoneToFlesh,
        HalfForMe,
        RaiseDead,
        MoonRay,
        MassDistortion,
        HolyWord,
        Resurrection,
        SunRay,
        DivineIntervention,


        // Sorcerer (Arcane) Spells
        LightSorcerer,
        AwakenSorcerer,
        DetectMagic,
        ElementalArrow,
        EnergyBlast,
        Sleep,
        CreateRope,
        ToxicCloud,
        Jump,
        AcidStream,
        Levitate,
        WizardEye,
        IdentifyMonster,
        LightningBolt,
        LloydsBeacon,
        PowerShield,
        DetectMonster,
        Fireball,
        TimeDistortion,
        FeebleMind,
        Teleport,
        FingerOfDeath,
        SuperShelter,
        DragonBreath,
        RechargeItem,
        FantasticFreeze,
        Duplication,
        Disintegration,
        Etherealize,
        DancingSword,
        EnchantItem,
        Incinerate,
        MegaVolts,
        Inferno,
        Implosion,
        StarBurst,

        // Druid Spells
        LightDruid,
        AwakenDruid,
        FirstAidDruid,
        DetectMagicDruid,
        ElementalArrowDruid,
        RevitalizeDruid,
        CreateRopeDruid,
        SleepDruid,
        ProtectionFromElementsDruid,
        SuppressPoisonDruid,
        SuppressDiseaseDruid,
        IdentifyMonsterDruid,
        NaturesCure,
        ImmobilizeDruid,
        WalkOnWater,
        FrostBite,
        LightningBoltDruid,
        AcidSprayDruid,
        ColdRayDruid,
        NaturesGate,
        FireballDruid,
        DeadlySwarm,
        CureParalysisDruid,
        ParalyzeDruid,
        CreateFoodDruid,
        StoneToFleshDruid,
        RaiseDeadDruid,
        PrismaticLight,
        ElementalStorm,
        None = 255,
    }

    public class MM3SpellList
    {
        Dictionary<MM3SpellIndex, MM3Spell> m_spells;

        public Dictionary<MM3SpellIndex, MM3Spell> Spells
        {
            get { return m_spells; }
        }

        private static string[] m_spellNames = null;

        public static string[] GetSpellNames()
        {
            if (m_spellNames != null)
                return m_spellNames;

            MM3InternalSpellIndex[] spells = GetInternalIndexList();
            m_spellNames = new string[spells.Length];
            for (int i = 0; i < m_spellNames.Length; i++)
                m_spellNames[i] = GetSpellName(spells[i]);
            return m_spellNames;
        }

        private Spell[] m_spellList = null;

        public Spell[] GetSpells()
        {
            if (m_spellList != null)
                return m_spellList;

            MM3InternalSpellIndex[] spells = GetInternalIndexList();
            m_spellList = new Spell[spells.Length];
            for (int i = 0; i < m_spellList.Length; i++)
                m_spellList[i] = GetSpell(spells[i]);
            return m_spellList;
        }

        public static string GetSpellName(MM3SpellIndex index)
        {
            if (index == MM3SpellIndex.None)
                return "None";

            MM3Spell spell = MM3.Spells[index];

            return spell.Name;
        }

        private static MM3InternalSpellIndex[] m_internalIndexList = null;

        public static MM3InternalSpellIndex[] GetInternalIndexList()
        {
            if (m_internalIndexList != null)
                return m_internalIndexList;

            m_internalIndexList = new MM3InternalSpellIndex[MM3InternalSpellIndex.Last - MM3InternalSpellIndex.First + 1];
            for (MM3InternalSpellIndex si = MM3InternalSpellIndex.First; si <= MM3InternalSpellIndex.Last; si++)
                m_internalIndexList[si - MM3InternalSpellIndex.First] = si;
            return m_internalIndexList;
        }

        public static string GetSpellName(MM3InternalSpellIndex index)
        {
            switch (index)
            {
                case MM3InternalSpellIndex.Light: return "Light";
                case MM3InternalSpellIndex.Awaken: return "Awaken";
                case MM3InternalSpellIndex.FirstAid: return "First Aid";
                case MM3InternalSpellIndex.FlyingFist: return "Flying Fist";
                case MM3InternalSpellIndex.DetectMagic: return "Detect Magic";
                case MM3InternalSpellIndex.ElementalArrow: return "Elemental Arrow";
                case MM3InternalSpellIndex.Revitalize: return "Revitalize";
                case MM3InternalSpellIndex.CureWounds: return "Cure Wounds";
                case MM3InternalSpellIndex.Sparks: return "Sparks";
                case MM3InternalSpellIndex.EnergyBlast: return "Energy Blast";
                case MM3InternalSpellIndex.Sleep: return "Sleep";
                case MM3InternalSpellIndex.Pain: return "Pain";
                case MM3InternalSpellIndex.CreateRope: return "Create Rope";
                case MM3InternalSpellIndex.ToxicCloud: return "Toxic Cloud";
                case MM3InternalSpellIndex.SuppressPoison: return "Suppress Poison";
                case MM3InternalSpellIndex.ProtFromElements: return "Prot. from Elements";
                case MM3InternalSpellIndex.TurnUndead: return "Turn Undead";
                case MM3InternalSpellIndex.Jump: return "Jump";
                case MM3InternalSpellIndex.AcidStream: return "Acid Stream";
                case MM3InternalSpellIndex.SuppressDisease: return "Suppress Disease";
                case MM3InternalSpellIndex.Silence: return "Silence";
                case MM3InternalSpellIndex.Blessed: return "Blessed";
                case MM3InternalSpellIndex.Levitate: return "Levitate";
                case MM3InternalSpellIndex.WizardEye: return "Wizard Eye";
                case MM3InternalSpellIndex.IdentifyMonster: return "Identify Monster";
                case MM3InternalSpellIndex.HolyBonus: return "Holy Bonus";
                case MM3InternalSpellIndex.PowerCure: return "Power Cure";
                case MM3InternalSpellIndex.NaturesCure: return "Nature's Cure";
                case MM3InternalSpellIndex.LightningBolt: return "Lightning Bolt";
                case MM3InternalSpellIndex.Immobilize: return "Immobilize";
                case MM3InternalSpellIndex.Heroism: return "Heroism";
                case MM3InternalSpellIndex.WalkOnWater: return "Walk on Water";
                case MM3InternalSpellIndex.FrostBite: return "Frost Bite";
                case MM3InternalSpellIndex.LloydsBeacon: return "Lloyd's Beacon";
                case MM3InternalSpellIndex.PowerShield: return "Power Shield";
                case MM3InternalSpellIndex.CurePoison: return "Cure Poison";
                case MM3InternalSpellIndex.Fireball: return "Fireball";
                case MM3InternalSpellIndex.DetectMonster: return "Detect Monster";
                case MM3InternalSpellIndex.AcidSpray: return "Acid Spray";
                case MM3InternalSpellIndex.ColdRay: return "Cold Ray";
                case MM3InternalSpellIndex.CureDisease: return "Cure Disease";
                case MM3InternalSpellIndex.NaturesGate: return "Nature's Gate";
                case MM3InternalSpellIndex.TimeDistortion: return "Time Distortion";
                case MM3InternalSpellIndex.FeebleMind: return "Feeble Mind";
                case MM3InternalSpellIndex.DeadlySwarm: return "Deadly Swarm";
                case MM3InternalSpellIndex.Teleport: return "Teleport";
                case MM3InternalSpellIndex.FingerOfDeath: return "Finger of Death";
                case MM3InternalSpellIndex.CureParalysis: return "Cure Paralysis";
                case MM3InternalSpellIndex.Paralyze: return "Paralyze";
                case MM3InternalSpellIndex.DragonBreath: return "Dragon Breath";
                case MM3InternalSpellIndex.SuperShelter: return "Super Shelter";
                case MM3InternalSpellIndex.FieryFlail: return "Fiery Flail";
                case MM3InternalSpellIndex.CreateFood: return "Create Food";
                case MM3InternalSpellIndex.TownPortal: return "Town Portal";
                case MM3InternalSpellIndex.StoneToFlesh: return "Stone to Flesh";
                case MM3InternalSpellIndex.RechargeItem: return "Recharge Item";
                case MM3InternalSpellIndex.FantasticFreeze: return "Fantastic Freeze";
                case MM3InternalSpellIndex.Duplication: return "Duplication";
                case MM3InternalSpellIndex.Disintegrate: return "Disintegrate";
                case MM3InternalSpellIndex.RaiseDead: return "Raise Dead";
                case MM3InternalSpellIndex.HalfForMe: return "Half for Me";
                case MM3InternalSpellIndex.Etherealize: return "Etherealize";
                case MM3InternalSpellIndex.DancingSword: return "Dancing Sword";
                case MM3InternalSpellIndex.PrismaticLight: return "Prismatic Light";
                case MM3InternalSpellIndex.MoonRay: return "Moon Ray";
                case MM3InternalSpellIndex.MassDistortion: return "Mass Distortion";
                case MM3InternalSpellIndex.EnchantItem: return "Enchant Item";
                case MM3InternalSpellIndex.Incinerate: return "Incinerate";
                case MM3InternalSpellIndex.ElementalStorm: return "Elemental Storm";
                case MM3InternalSpellIndex.HolyWord: return "Holy Word";
                case MM3InternalSpellIndex.Resurrect: return "Resurrect";
                case MM3InternalSpellIndex.MegaVolts: return "Mega Volts";
                case MM3InternalSpellIndex.Inferno: return "Inferno";
                case MM3InternalSpellIndex.SunRay: return "Sun Ray";
                case MM3InternalSpellIndex.Implosion: return "Implosion";
                case MM3InternalSpellIndex.StarBurst: return "Star Burst";
                case MM3InternalSpellIndex.DivineIntervention: return "Divine Intervention";
                default: return "None Ready";
            }
        }

        public MM3Spell GetSpell(MM3InternalSpellIndex index)
        {
            switch (index)
            {
                case MM3InternalSpellIndex.Light: return Spells[MM3SpellIndex.LightCleric];
                case MM3InternalSpellIndex.Awaken: return Spells[MM3SpellIndex.AwakenCleric];
                case MM3InternalSpellIndex.FirstAid: return Spells[MM3SpellIndex.FirstAid];
                case MM3InternalSpellIndex.FlyingFist: return Spells[MM3SpellIndex.FlyingFist];
                case MM3InternalSpellIndex.Revitalize: return Spells[MM3SpellIndex.Revitalize];
                case MM3InternalSpellIndex.CureWounds: return Spells[MM3SpellIndex.CureWounds];
                case MM3InternalSpellIndex.Sparks: return Spells[MM3SpellIndex.Sparks];
                case MM3InternalSpellIndex.ProtFromElements: return Spells[MM3SpellIndex.ProtectionFromElements];
                case MM3InternalSpellIndex.Pain: return Spells[MM3SpellIndex.Pain];
                case MM3InternalSpellIndex.SuppressPoison: return Spells[MM3SpellIndex.SuppressPoison];
                case MM3InternalSpellIndex.SuppressDisease: return Spells[MM3SpellIndex.SuppressDisease];
                case MM3InternalSpellIndex.TurnUndead: return Spells[MM3SpellIndex.TurnUndead];
                case MM3InternalSpellIndex.Silence: return Spells[MM3SpellIndex.Silence];
                case MM3InternalSpellIndex.Blessed: return Spells[MM3SpellIndex.Blessed];
                case MM3InternalSpellIndex.HolyBonus: return Spells[MM3SpellIndex.HolyBonus];
                case MM3InternalSpellIndex.PowerCure: return Spells[MM3SpellIndex.PowerCure];
                case MM3InternalSpellIndex.Heroism: return Spells[MM3SpellIndex.Heroism];
                case MM3InternalSpellIndex.Immobilize: return Spells[MM3SpellIndex.Immobilize];
                case MM3InternalSpellIndex.ColdRay: return Spells[MM3SpellIndex.ColdRay];
                case MM3InternalSpellIndex.CurePoison: return Spells[MM3SpellIndex.CurePoison];
                case MM3InternalSpellIndex.AcidSpray: return Spells[MM3SpellIndex.AcidSpray];
                case MM3InternalSpellIndex.CureDisease: return Spells[MM3SpellIndex.CureDisease];
                case MM3InternalSpellIndex.CureParalysis: return Spells[MM3SpellIndex.CureParalysis];
                case MM3InternalSpellIndex.Paralyze: return Spells[MM3SpellIndex.Paralyze];
                case MM3InternalSpellIndex.CreateFood: return Spells[MM3SpellIndex.CreateFood];
                case MM3InternalSpellIndex.FieryFlail: return Spells[MM3SpellIndex.FieryFlail];
                case MM3InternalSpellIndex.TownPortal: return Spells[MM3SpellIndex.TownPortal];
                case MM3InternalSpellIndex.StoneToFlesh: return Spells[MM3SpellIndex.StoneToFlesh];
                case MM3InternalSpellIndex.HalfForMe: return Spells[MM3SpellIndex.HalfForMe];
                case MM3InternalSpellIndex.RaiseDead: return Spells[MM3SpellIndex.RaiseDead];
                case MM3InternalSpellIndex.MoonRay: return Spells[MM3SpellIndex.MoonRay];
                case MM3InternalSpellIndex.MassDistortion: return Spells[MM3SpellIndex.MassDistortion];
                case MM3InternalSpellIndex.HolyWord: return Spells[MM3SpellIndex.HolyWord];
                case MM3InternalSpellIndex.Resurrect: return Spells[MM3SpellIndex.Resurrection];
                case MM3InternalSpellIndex.SunRay: return Spells[MM3SpellIndex.SunRay];
                case MM3InternalSpellIndex.DivineIntervention: return Spells[MM3SpellIndex.DivineIntervention];
                case MM3InternalSpellIndex.DetectMagic: return Spells[MM3SpellIndex.DetectMagic];
                case MM3InternalSpellIndex.ElementalArrow: return Spells[MM3SpellIndex.ElementalArrow];
                case MM3InternalSpellIndex.EnergyBlast: return Spells[MM3SpellIndex.EnergyBlast];
                case MM3InternalSpellIndex.Sleep: return Spells[MM3SpellIndex.Sleep];
                case MM3InternalSpellIndex.CreateRope: return Spells[MM3SpellIndex.CreateRope];
                case MM3InternalSpellIndex.ToxicCloud: return Spells[MM3SpellIndex.ToxicCloud];
                case MM3InternalSpellIndex.Jump: return Spells[MM3SpellIndex.Jump];
                case MM3InternalSpellIndex.AcidStream: return Spells[MM3SpellIndex.AcidStream];
                case MM3InternalSpellIndex.Levitate: return Spells[MM3SpellIndex.Levitate];
                case MM3InternalSpellIndex.WizardEye: return Spells[MM3SpellIndex.WizardEye];
                case MM3InternalSpellIndex.IdentifyMonster: return Spells[MM3SpellIndex.IdentifyMonster];
                case MM3InternalSpellIndex.LightningBolt: return Spells[MM3SpellIndex.LightningBolt];
                case MM3InternalSpellIndex.LloydsBeacon: return Spells[MM3SpellIndex.LloydsBeacon];
                case MM3InternalSpellIndex.PowerShield: return Spells[MM3SpellIndex.PowerShield];
                case MM3InternalSpellIndex.DetectMonster: return Spells[MM3SpellIndex.DetectMonster];
                case MM3InternalSpellIndex.Fireball: return Spells[MM3SpellIndex.Fireball];
                case MM3InternalSpellIndex.TimeDistortion: return Spells[MM3SpellIndex.TimeDistortion];
                case MM3InternalSpellIndex.FeebleMind: return Spells[MM3SpellIndex.FeebleMind];
                case MM3InternalSpellIndex.Teleport: return Spells[MM3SpellIndex.Teleport];
                case MM3InternalSpellIndex.FingerOfDeath: return Spells[MM3SpellIndex.FingerOfDeath];
                case MM3InternalSpellIndex.SuperShelter: return Spells[MM3SpellIndex.SuperShelter];
                case MM3InternalSpellIndex.DragonBreath: return Spells[MM3SpellIndex.DragonBreath];
                case MM3InternalSpellIndex.RechargeItem: return Spells[MM3SpellIndex.RechargeItem];
                case MM3InternalSpellIndex.FantasticFreeze: return Spells[MM3SpellIndex.FantasticFreeze];
                case MM3InternalSpellIndex.Duplication: return Spells[MM3SpellIndex.Duplication];
                case MM3InternalSpellIndex.Disintegrate: return Spells[MM3SpellIndex.Disintegration];
                case MM3InternalSpellIndex.Etherealize: return Spells[MM3SpellIndex.Etherealize];
                case MM3InternalSpellIndex.DancingSword: return Spells[MM3SpellIndex.DancingSword];
                case MM3InternalSpellIndex.EnchantItem: return Spells[MM3SpellIndex.EnchantItem];
                case MM3InternalSpellIndex.Incinerate: return Spells[MM3SpellIndex.Incinerate];
                case MM3InternalSpellIndex.MegaVolts: return Spells[MM3SpellIndex.MegaVolts];
                case MM3InternalSpellIndex.Inferno: return Spells[MM3SpellIndex.Inferno];
                case MM3InternalSpellIndex.Implosion: return Spells[MM3SpellIndex.Implosion];
                case MM3InternalSpellIndex.StarBurst: return Spells[MM3SpellIndex.StarBurst];
                case MM3InternalSpellIndex.NaturesCure: return Spells[MM3SpellIndex.NaturesCure];
                case MM3InternalSpellIndex.WalkOnWater: return Spells[MM3SpellIndex.WalkOnWater];
                case MM3InternalSpellIndex.FrostBite: return Spells[MM3SpellIndex.FrostBite];
                case MM3InternalSpellIndex.NaturesGate: return Spells[MM3SpellIndex.NaturesGate];
                case MM3InternalSpellIndex.DeadlySwarm: return Spells[MM3SpellIndex.DeadlySwarm];
                case MM3InternalSpellIndex.PrismaticLight: return Spells[MM3SpellIndex.PrismaticLight];
                case MM3InternalSpellIndex.ElementalStorm: return Spells[MM3SpellIndex.ElementalStorm];
                default: return MM3Spell.MM3None;
            }
        }

        public static int GetSpellPurchasePrice(MM3InternalSpellIndex index, GenericClass mm3Class)
        {
            switch (mm3Class)
            {
                case GenericClass.Cleric:
                case GenericClass.Druid:
                case GenericClass.Sorcerer:
                    return GetSpellPurchasePrice(index);
                default:
                    return GetSpellPurchasePrice(index) * 2;
            }
        }

        public static int GetSpellPurchasePrice(MM3InternalSpellIndex index)
        {
            switch (index)
            {
                case MM3InternalSpellIndex.Light: return 100;
                case MM3InternalSpellIndex.Awaken: return 100;
                case MM3InternalSpellIndex.FirstAid: return 100;
                case MM3InternalSpellIndex.FlyingFist: return 200;
                case MM3InternalSpellIndex.Revitalize: return 200;
                case MM3InternalSpellIndex.CureWounds: return 300;
                case MM3InternalSpellIndex.Sparks: return 500;
                case MM3InternalSpellIndex.ProtFromElements: return 500;
                case MM3InternalSpellIndex.Pain: return 400;
                case MM3InternalSpellIndex.SuppressPoison: return 400;
                case MM3InternalSpellIndex.SuppressDisease: return 500;
                case MM3InternalSpellIndex.TurnUndead: return 500;
                case MM3InternalSpellIndex.Silence: return 600;
                case MM3InternalSpellIndex.Blessed: return 1000;
                case MM3InternalSpellIndex.HolyBonus: return 1000;
                case MM3InternalSpellIndex.PowerCure: return 1000;
                case MM3InternalSpellIndex.Heroism: return 1000;
                case MM3InternalSpellIndex.Immobilize: return 600;
                case MM3InternalSpellIndex.ColdRay: return 1000;
                case MM3InternalSpellIndex.CurePoison: return 800;
                case MM3InternalSpellIndex.AcidSpray: return 800;
                case MM3InternalSpellIndex.CureDisease: return 1000;
                case MM3InternalSpellIndex.CureParalysis: return 1200;
                case MM3InternalSpellIndex.Paralyze: return 1500;
                case MM3InternalSpellIndex.CreateFood: return 2000;
                case MM3InternalSpellIndex.FieryFlail: return 2500;
                case MM3InternalSpellIndex.TownPortal: return 3000;
                case MM3InternalSpellIndex.StoneToFlesh: return 3500;
                case MM3InternalSpellIndex.HalfForMe: return 4000;
                case MM3InternalSpellIndex.RaiseDead: return 5000;
                case MM3InternalSpellIndex.MoonRay: return 6000;
                case MM3InternalSpellIndex.MassDistortion: return 7500;
                case MM3InternalSpellIndex.HolyWord: return 10000;
                case MM3InternalSpellIndex.Resurrect: return 12500;
                case MM3InternalSpellIndex.SunRay: return 15000;
                case MM3InternalSpellIndex.DivineIntervention: return 20000;
                case MM3InternalSpellIndex.DetectMagic: return 100;
                case MM3InternalSpellIndex.ElementalArrow: return 200;
                case MM3InternalSpellIndex.EnergyBlast: return 500;
                case MM3InternalSpellIndex.CreateRope: return 300;
                case MM3InternalSpellIndex.Sleep: return 300;
                case MM3InternalSpellIndex.ToxicCloud: return 400;
                case MM3InternalSpellIndex.Jump: return 400;
                case MM3InternalSpellIndex.AcidStream: return 500;
                case MM3InternalSpellIndex.Levitate: return 500;
                case MM3InternalSpellIndex.WizardEye: return 500;
                case MM3InternalSpellIndex.IdentifyMonster: return 500;
                case MM3InternalSpellIndex.LightningBolt: return 1000;
                case MM3InternalSpellIndex.LloydsBeacon: return 600;
                case MM3InternalSpellIndex.PowerShield: return 1000;
                case MM3InternalSpellIndex.DetectMonster: return 600;
                case MM3InternalSpellIndex.Fireball: return 1000;
                case MM3InternalSpellIndex.TimeDistortion: return 800;
                case MM3InternalSpellIndex.FeebleMind: return 800;
                case MM3InternalSpellIndex.Teleport: return 1000;
                case MM3InternalSpellIndex.FingerOfDeath: return 1000;
                case MM3InternalSpellIndex.SuperShelter: return 1500;
                case MM3InternalSpellIndex.DragonBreath: return 1500;
                case MM3InternalSpellIndex.RechargeItem: return 1500;
                case MM3InternalSpellIndex.FantasticFreeze: return 1500;
                case MM3InternalSpellIndex.Duplication: return 2000;
                case MM3InternalSpellIndex.Disintegrate: return 2500;
                case MM3InternalSpellIndex.Etherealize: return 3000;
                case MM3InternalSpellIndex.DancingSword: return 1500;
                case MM3InternalSpellIndex.EnchantItem: return 3000;
                case MM3InternalSpellIndex.Incinerate: return 3500;
                case MM3InternalSpellIndex.MegaVolts: return 4000;
                case MM3InternalSpellIndex.Inferno: return 7500;
                case MM3InternalSpellIndex.Implosion: return 10000;
                case MM3InternalSpellIndex.StarBurst: return 20000;
                case MM3InternalSpellIndex.NaturesCure: return 600;
                case MM3InternalSpellIndex.WalkOnWater: return 700;
                case MM3InternalSpellIndex.FrostBite: return 700;
                case MM3InternalSpellIndex.NaturesGate: return 1000;
                case MM3InternalSpellIndex.DeadlySwarm: return 1200;
                case MM3InternalSpellIndex.PrismaticLight: return 6000;
                case MM3InternalSpellIndex.ElementalStorm: return 10000;
                default: return 0;
            }
        }

        public static bool IsDruidSpell(MM3InternalSpellIndex spell)
        {
            switch (spell)
            {
                case MM3InternalSpellIndex.Light:
                case MM3InternalSpellIndex.Awaken:
                case MM3InternalSpellIndex.FirstAid:
                case MM3InternalSpellIndex.DetectMagic:
                case MM3InternalSpellIndex.ElementalArrow:
                case MM3InternalSpellIndex.Revitalize:
                case MM3InternalSpellIndex.CreateRope:
                case MM3InternalSpellIndex.Sleep:
                case MM3InternalSpellIndex.SuppressPoison:
                case MM3InternalSpellIndex.ProtFromElements:
                case MM3InternalSpellIndex.SuppressDisease:
                case MM3InternalSpellIndex.IdentifyMonster:
                case MM3InternalSpellIndex.NaturesCure:
                case MM3InternalSpellIndex.Immobilize:
                case MM3InternalSpellIndex.WalkOnWater:
                case MM3InternalSpellIndex.FrostBite:
                case MM3InternalSpellIndex.LightningBolt:
                case MM3InternalSpellIndex.AcidSpray:
                case MM3InternalSpellIndex.ColdRay:
                case MM3InternalSpellIndex.NaturesGate:
                case MM3InternalSpellIndex.Fireball:
                case MM3InternalSpellIndex.DeadlySwarm:
                case MM3InternalSpellIndex.CureParalysis:
                case MM3InternalSpellIndex.Paralyze:
                case MM3InternalSpellIndex.CreateFood:
                case MM3InternalSpellIndex.StoneToFlesh:
                case MM3InternalSpellIndex.RaiseDead:
                case MM3InternalSpellIndex.PrismaticLight:
                case MM3InternalSpellIndex.ElementalStorm:
                    return true;
                default:
                    return false;
            }
        }

        public MM3SpellList()
        {
            m_spells = new Dictionary<MM3SpellIndex, MM3Spell>(96);
            // Cleric spells
            m_spells.Add(MM3SpellIndex.LightCleric, new MM3Spell(MM3SpellIndex.LightCleric, MM3InternalSpellIndex.Light, SpellType.Cleric, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.AwakenCleric, new MM3Spell(MM3SpellIndex.AwakenCleric, MM3InternalSpellIndex.Awaken, SpellType.Cleric, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.FirstAid, new MM3Spell(MM3SpellIndex.FirstAid, MM3InternalSpellIndex.FirstAid, SpellType.Cleric, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.FlyingFist, new MM3Spell(MM3SpellIndex.FlyingFist, MM3InternalSpellIndex.FlyingFist, SpellType.Cleric, 2, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM3SpellIndex.Revitalize, new MM3Spell(MM3SpellIndex.Revitalize, MM3InternalSpellIndex.Revitalize, SpellType.Cleric, 2, false, 0, 2, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.CureWounds, new MM3Spell(MM3SpellIndex.CureWounds, MM3InternalSpellIndex.CureWounds, SpellType.Cleric, 3, false, 1, 2, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.Sparks, new MM3Spell(MM3SpellIndex.Sparks, MM3InternalSpellIndex.Sparks, SpellType.Cleric, 1, true, 1, 2, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.ProtectionFromElements, new MM3Spell(MM3SpellIndex.ProtectionFromElements, MM3InternalSpellIndex.ProtFromElements, SpellType.Cleric, 1, true, 2, 3, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.Pain, new MM3Spell(MM3SpellIndex.Pain, MM3InternalSpellIndex.Pain, SpellType.Cleric, 4, false, 0, 3, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.SuppressPoison, new MM3Spell(MM3SpellIndex.SuppressPoison, MM3InternalSpellIndex.SuppressPoison, SpellType.Cleric, 4, false, 0, 3, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.SuppressDisease, new MM3Spell(MM3SpellIndex.SuppressDisease, MM3InternalSpellIndex.SuppressDisease, SpellType.Cleric, 5, false, 0, 4, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.TurnUndead, new MM3Spell(MM3SpellIndex.TurnUndead, MM3InternalSpellIndex.TurnUndead, SpellType.Cleric, 5, false, 2, 4, SpellWhen.CombatAnywhere, SpellTarget.UndeadMonsterGroup));
            m_spells.Add(MM3SpellIndex.Silence, new MM3Spell(MM3SpellIndex.Silence, MM3InternalSpellIndex.Silence, SpellType.Cleric, 6, false, 0, 5, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.Blessed, new MM3Spell(MM3SpellIndex.Blessed, MM3InternalSpellIndex.Blessed, SpellType.Cleric, 2, true, 0, 5, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.HolyBonus, new MM3Spell(MM3SpellIndex.HolyBonus, MM3InternalSpellIndex.HolyBonus, SpellType.Cleric, 2, true, 0, 6, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.PowerCure, new MM3Spell(MM3SpellIndex.PowerCure, MM3InternalSpellIndex.PowerCure, SpellType.Cleric, 2, true, 3, 6, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.Heroism, new MM3Spell(MM3SpellIndex.Heroism, MM3InternalSpellIndex.Heroism, SpellType.Cleric, 2, true, 3, 7, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.Immobilize, new MM3Spell(MM3SpellIndex.Immobilize, MM3InternalSpellIndex.Immobilize, SpellType.Cleric, 6, false, 3, 7, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.ColdRay, new MM3Spell(MM3SpellIndex.ColdRay, MM3InternalSpellIndex.ColdRay, SpellType.Cleric, 2, true, 4, 8, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters));
            m_spells.Add(MM3SpellIndex.CurePoison, new MM3Spell(MM3SpellIndex.CurePoison, MM3InternalSpellIndex.CurePoison, SpellType.Cleric, 8, false, 0, 8, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.AcidSpray, new MM3Spell(MM3SpellIndex.AcidSpray, MM3InternalSpellIndex.AcidSpray, SpellType.Cleric, 8, false, 0, 9, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters));
            m_spells.Add(MM3SpellIndex.CureDisease, new MM3Spell(MM3SpellIndex.CureDisease, MM3InternalSpellIndex.CureDisease, SpellType.Cleric, 10, false, 0, 9, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.CureParalysis, new MM3Spell(MM3SpellIndex.CureParalysis, MM3InternalSpellIndex.CureParalysis, SpellType.Cleric, 12, false, 0, 10, SpellWhen.CombatAnywhere, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.Paralyze, new MM3Spell(MM3SpellIndex.Paralyze, MM3InternalSpellIndex.Paralyze, SpellType.Cleric, 15, false, 4, 10, SpellWhen.AnywhereAnytime, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.CreateFood, new MM3Spell(MM3SpellIndex.CreateFood, MM3InternalSpellIndex.CreateFood, SpellType.Cleric, 20, false, 5, 11, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.FieryFlail, new MM3Spell(MM3SpellIndex.FieryFlail, MM3InternalSpellIndex.FieryFlail, SpellType.Cleric, 25, false, 5, 11, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM3SpellIndex.TownPortal, new MM3Spell(MM3SpellIndex.TownPortal, MM3InternalSpellIndex.TownPortal, SpellType.Cleric, 30, false, 5, 12, SpellWhen.NonCombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.StoneToFlesh, new MM3Spell(MM3SpellIndex.StoneToFlesh, MM3InternalSpellIndex.StoneToFlesh, SpellType.Cleric, 35, false, 5, 12, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.HalfForMe, new MM3Spell(MM3SpellIndex.HalfForMe, MM3InternalSpellIndex.HalfForMe, SpellType.Cleric, 40, false, 10, 13, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.RaiseDead, new MM3Spell(MM3SpellIndex.RaiseDead, MM3InternalSpellIndex.RaiseDead, SpellType.Cleric, 50, false, 10, 13, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.MoonRay, new MM3Spell(MM3SpellIndex.MoonRay, MM3InternalSpellIndex.MoonRay, SpellType.Cleric, 60, false, 10, 14, SpellWhen.AnywhereAnytime, SpellTarget.AllPlayersAndMonsters));
            m_spells.Add(MM3SpellIndex.MassDistortion, new MM3Spell(MM3SpellIndex.MassDistortion, MM3InternalSpellIndex.MassDistortion, SpellType.Cleric, 75, false, 10, 14, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.HolyWord, new MM3Spell(MM3SpellIndex.HolyWord, MM3InternalSpellIndex.HolyWord, SpellType.Cleric, 100, false, 20, 15, SpellWhen.CombatAnywhere, SpellTarget.AllUndeadMonsters));
            m_spells.Add(MM3SpellIndex.Resurrection, new MM3Spell(MM3SpellIndex.Resurrection, MM3InternalSpellIndex.Resurrect, SpellType.Cleric, 125, false, 20, 15, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.SunRay, new MM3Spell(MM3SpellIndex.SunRay, MM3InternalSpellIndex.SunRay, SpellType.Cleric, 150, false, 10, 16, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters));
            m_spells.Add(MM3SpellIndex.DivineIntervention, new MM3Spell(MM3SpellIndex.DivineIntervention, MM3InternalSpellIndex.DivineIntervention, SpellType.Cleric, 200, false, 20, 17, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            // Sorcerer (arcane) spells
            m_spells.Add(MM3SpellIndex.LightSorcerer, new MM3Spell(MM3SpellIndex.LightSorcerer, MM3InternalSpellIndex.Light, SpellType.Sorcerer, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.AwakenSorcerer, new MM3Spell(MM3SpellIndex.AwakenSorcerer, MM3InternalSpellIndex.Awaken, SpellType.Sorcerer, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.DetectMagic, new MM3Spell(MM3SpellIndex.DetectMagic, MM3InternalSpellIndex.DetectMagic, SpellType.Sorcerer, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Caster));
            m_spells.Add(MM3SpellIndex.ElementalArrow, new MM3Spell(MM3SpellIndex.ElementalArrow, MM3InternalSpellIndex.ElementalArrow, SpellType.Sorcerer, 2, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM3SpellIndex.EnergyBlast, new MM3Spell(MM3SpellIndex.EnergyBlast, MM3InternalSpellIndex.EnergyBlast, SpellType.Sorcerer, 1, true, 1, 2, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM3SpellIndex.Sleep, new MM3Spell(MM3SpellIndex.Sleep, MM3InternalSpellIndex.Sleep, SpellType.Sorcerer, 3, false, 1, 2, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.CreateRope, new MM3Spell(MM3SpellIndex.CreateRope, MM3InternalSpellIndex.CreateRope, SpellType.Sorcerer, 3, false, 0, 3, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.ToxicCloud, new MM3Spell(MM3SpellIndex.ToxicCloud, MM3InternalSpellIndex.ToxicCloud, SpellType.Sorcerer, 4, false, 1, 3, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.Jump, new MM3Spell(MM3SpellIndex.Jump, MM3InternalSpellIndex.Jump, SpellType.Sorcerer, 4, false, 0, 4, SpellWhen.NonCombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.AcidStream, new MM3Spell(MM3SpellIndex.AcidStream, MM3InternalSpellIndex.AcidStream, SpellType.Sorcerer, 5, false, 0, 4, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM3SpellIndex.Levitate, new MM3Spell(MM3SpellIndex.Levitate, MM3InternalSpellIndex.Levitate, SpellType.Sorcerer, 5, false, 0, 5, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.WizardEye, new MM3Spell(MM3SpellIndex.WizardEye, MM3InternalSpellIndex.WizardEye, SpellType.Sorcerer, 5, false, 2, 5, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.IdentifyMonster, new MM3Spell(MM3SpellIndex.IdentifyMonster, MM3InternalSpellIndex.IdentifyMonster, SpellType.Sorcerer, 5, false, 0, 6, SpellWhen.CombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.LightningBolt, new MM3Spell(MM3SpellIndex.LightningBolt, MM3InternalSpellIndex.LightningBolt, SpellType.Sorcerer, 2, true, 2, 6, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.LloydsBeacon, new MM3Spell(MM3SpellIndex.LloydsBeacon, MM3InternalSpellIndex.LloydsBeacon, SpellType.Sorcerer, 6, false, 2, 7, SpellWhen.NonCombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.PowerShield, new MM3Spell(MM3SpellIndex.PowerShield, MM3InternalSpellIndex.PowerShield, SpellType.Sorcerer, 2, true, 2, 7, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.DetectMonster, new MM3Spell(MM3SpellIndex.DetectMonster, MM3InternalSpellIndex.DetectMonster, SpellType.Sorcerer, 6, false, 0, 8, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.Fireball, new MM3Spell(MM3SpellIndex.Fireball, MM3InternalSpellIndex.Fireball, SpellType.Sorcerer, 2, true, 2, 8, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.TimeDistortion, new MM3Spell(MM3SpellIndex.TimeDistortion, MM3InternalSpellIndex.TimeDistortion, SpellType.Sorcerer, 8, false, 3, 9, SpellWhen.CombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.FeebleMind, new MM3Spell(MM3SpellIndex.FeebleMind, MM3InternalSpellIndex.FeebleMind, SpellType.Sorcerer, 8, false, 0, 9, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.Teleport, new MM3Spell(MM3SpellIndex.Teleport, MM3InternalSpellIndex.Teleport, SpellType.Sorcerer, 10, false, 0, 10, SpellWhen.NonCombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.FingerOfDeath, new MM3Spell(MM3SpellIndex.FingerOfDeath, MM3InternalSpellIndex.FingerOfDeath, SpellType.Sorcerer, 10, false, 4, 10, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.SuperShelter, new MM3Spell(MM3SpellIndex.SuperShelter, MM3InternalSpellIndex.SuperShelter, SpellType.Sorcerer, 15, false, 5, 11, SpellWhen.NonCombatLand, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.DragonBreath, new MM3Spell(MM3SpellIndex.DragonBreath, MM3InternalSpellIndex.DragonBreath, SpellType.Sorcerer, 3, true, 5, 11, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters));
            m_spells.Add(MM3SpellIndex.RechargeItem, new MM3Spell(MM3SpellIndex.RechargeItem, MM3InternalSpellIndex.RechargeItem, SpellType.Sorcerer, 15, false, 10, 12, SpellWhen.AnywhereAnytime, SpellTarget.Caster));
            m_spells.Add(MM3SpellIndex.FantasticFreeze, new MM3Spell(MM3SpellIndex.FantasticFreeze, MM3InternalSpellIndex.FantasticFreeze, SpellType.Sorcerer, 15, false, 5, 12, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.Duplication, new MM3Spell(MM3SpellIndex.Duplication, MM3InternalSpellIndex.Duplication, SpellType.Sorcerer, 20, false, 50, 13, SpellWhen.AnywhereAnytime, SpellTarget.Caster));
            m_spells.Add(MM3SpellIndex.Disintegration, new MM3Spell(MM3SpellIndex.Disintegration, MM3InternalSpellIndex.Disintegrate, SpellType.Sorcerer, 25, false, 8, 13, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.Etherealize, new MM3Spell(MM3SpellIndex.Etherealize, MM3InternalSpellIndex.Etherealize, SpellType.Sorcerer, 30, false, 8, 14, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.DancingSword, new MM3Spell(MM3SpellIndex.DancingSword, MM3InternalSpellIndex.DancingSword, SpellType.Sorcerer, 3, true, 10, 14, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.EnchantItem, new MM3Spell(MM3SpellIndex.EnchantItem, MM3InternalSpellIndex.EnchantItem, SpellType.Sorcerer, 30, false, 20, 15, SpellWhen.AnywhereAnytime, SpellTarget.Caster));
            m_spells.Add(MM3SpellIndex.Incinerate, new MM3Spell(MM3SpellIndex.Incinerate, MM3InternalSpellIndex.Incinerate, SpellType.Sorcerer, 35, false, 10, 15, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM3SpellIndex.MegaVolts, new MM3Spell(MM3SpellIndex.MegaVolts, MM3InternalSpellIndex.MegaVolts, SpellType.Sorcerer, 40, false, 10, 16, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.Inferno, new MM3Spell(MM3SpellIndex.Inferno, MM3InternalSpellIndex.Inferno, SpellType.Sorcerer, 75, false, 10, 16, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.Implosion, new MM3Spell(MM3SpellIndex.Implosion, MM3InternalSpellIndex.Implosion, SpellType.Sorcerer, 100, false, 20, 17, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM3SpellIndex.StarBurst, new MM3Spell(MM3SpellIndex.StarBurst, MM3InternalSpellIndex.StarBurst, SpellType.Sorcerer, 200, false, 20, 17, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters));
            // Druid spells
            m_spells.Add(MM3SpellIndex.LightDruid, new MM3Spell(MM3SpellIndex.LightDruid, MM3InternalSpellIndex.Light, SpellType.Druid, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.AwakenDruid, new MM3Spell(MM3SpellIndex.AwakenDruid, MM3InternalSpellIndex.Awaken, SpellType.Druid, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.FirstAidDruid, new MM3Spell(MM3SpellIndex.FirstAidDruid, MM3InternalSpellIndex.FirstAid, SpellType.Druid, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.DetectMagicDruid, new MM3Spell(MM3SpellIndex.DetectMagicDruid, MM3InternalSpellIndex.DetectMagic, SpellType.Druid, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Caster));
            m_spells.Add(MM3SpellIndex.ElementalArrowDruid, new MM3Spell(MM3SpellIndex.ElementalArrowDruid, MM3InternalSpellIndex.ElementalArrow, SpellType.Druid, 2, false, 0, 2, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM3SpellIndex.RevitalizeDruid, new MM3Spell(MM3SpellIndex.RevitalizeDruid, MM3InternalSpellIndex.Revitalize, SpellType.Druid, 2, false, 0, 2, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.CreateRopeDruid, new MM3Spell(MM3SpellIndex.CreateRopeDruid, MM3InternalSpellIndex.CreateRope, SpellType.Druid, 3, false, 0, 3, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.SleepDruid, new MM3Spell(MM3SpellIndex.SleepDruid, MM3InternalSpellIndex.Sleep, SpellType.Druid, 3, false, 1, 3, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.ProtectionFromElementsDruid, new MM3Spell(MM3SpellIndex.ProtectionFromElementsDruid, MM3InternalSpellIndex.ProtFromElements, SpellType.Druid, 1, true, 2, 4, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.SuppressPoisonDruid, new MM3Spell(MM3SpellIndex.SuppressPoisonDruid, MM3InternalSpellIndex.SuppressPoison, SpellType.Druid, 4, false, 0, 4, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.SuppressDiseaseDruid, new MM3Spell(MM3SpellIndex.SuppressDiseaseDruid, MM3InternalSpellIndex.SuppressDisease, SpellType.Druid, 5, false, 0, 5, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.IdentifyMonsterDruid, new MM3Spell(MM3SpellIndex.IdentifyMonsterDruid, MM3InternalSpellIndex.IdentifyMonster, SpellType.Druid, 5, false, 0, 5, SpellWhen.CombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.NaturesCure, new MM3Spell(MM3SpellIndex.NaturesCure, MM3InternalSpellIndex.NaturesCure, SpellType.Druid, 6, false, 0, 6, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.ImmobilizeDruid, new MM3Spell(MM3SpellIndex.ImmobilizeDruid, MM3InternalSpellIndex.Immobilize, SpellType.Druid, 6, false, 3, 6, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.WalkOnWater, new MM3Spell(MM3SpellIndex.WalkOnWater, MM3InternalSpellIndex.WalkOnWater, SpellType.Druid, 7, false, 0, 7, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.FrostBite, new MM3Spell(MM3SpellIndex.FrostBite, MM3InternalSpellIndex.FrostBite, SpellType.Druid, 7, false, 0, 7, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM3SpellIndex.LightningBoltDruid, new MM3Spell(MM3SpellIndex.LightningBoltDruid, MM3InternalSpellIndex.LightningBolt, SpellType.Druid, 2, true, 2, 8, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.AcidSprayDruid, new MM3Spell(MM3SpellIndex.AcidSprayDruid, MM3InternalSpellIndex.AcidSpray, SpellType.Druid, 8, false, 0, 8, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters));
            m_spells.Add(MM3SpellIndex.ColdRayDruid, new MM3Spell(MM3SpellIndex.ColdRayDruid, MM3InternalSpellIndex.ColdRay, SpellType.Druid, 2, true, 4, 9, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters));
            m_spells.Add(MM3SpellIndex.NaturesGate, new MM3Spell(MM3SpellIndex.NaturesGate, MM3InternalSpellIndex.NaturesGate, SpellType.Druid, 10, false, 0, 9, SpellWhen.NonCombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.FireballDruid, new MM3Spell(MM3SpellIndex.FireballDruid, MM3InternalSpellIndex.Fireball, SpellType.Druid, 2, true, 2, 10, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.DeadlySwarm, new MM3Spell(MM3SpellIndex.DeadlySwarm, MM3InternalSpellIndex.DeadlySwarm, SpellType.Druid, 12, false, 0, 10, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.CureParalysisDruid, new MM3Spell(MM3SpellIndex.CureParalysisDruid, MM3InternalSpellIndex.CureParalysis, SpellType.Druid, 12, false, 0, 11, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.ParalyzeDruid, new MM3Spell(MM3SpellIndex.ParalyzeDruid, MM3InternalSpellIndex.Paralyze, SpellType.Druid, 15, false, 4, 11, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM3SpellIndex.CreateFoodDruid, new MM3Spell(MM3SpellIndex.CreateFoodDruid, MM3InternalSpellIndex.CreateFood, SpellType.Druid, 20, false, 5, 12, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM3SpellIndex.StoneToFleshDruid, new MM3Spell(MM3SpellIndex.StoneToFleshDruid, MM3InternalSpellIndex.StoneToFlesh, SpellType.Druid, 35, false, 5, 12, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.RaiseDeadDruid, new MM3Spell(MM3SpellIndex.RaiseDeadDruid, MM3InternalSpellIndex.RaiseDead, SpellType.Druid, 50, false, 10, 13, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM3SpellIndex.PrismaticLight, new MM3Spell(MM3SpellIndex.PrismaticLight, MM3InternalSpellIndex.PrismaticLight, SpellType.Druid, 60, false, 10, 14, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters));
            m_spells.Add(MM3SpellIndex.ElementalStorm, new MM3Spell(MM3SpellIndex.ElementalStorm, MM3InternalSpellIndex.ElementalStorm, SpellType.Druid, 100, false, 10, 15, SpellWhen.CombatAnywhere, SpellTarget.AllMonsters));
        }

        public static string LongDescription(MM3InternalSpellIndex index)
        {
            switch (index)
            {
                case MM3InternalSpellIndex.Light: return "Provides a magical light for the darker areas.";
                case MM3InternalSpellIndex.Awaken: return "Pulls all sleeping party members from their slumber.";
                case MM3InternalSpellIndex.FirstAid: return "An adventurer's minor wounds of battle can quickly be healed with this anointing incantation.";
                case MM3InternalSpellIndex.FlyingFist: return "Summons an enchanted gauntlet to deliver a stinging punch to a single foe.";
                case MM3InternalSpellIndex.DetectMagic: return "If there be a magical item in your pack this will make it known, and reveal the number of its limited use.";
                case MM3InternalSpellIndex.ElementalArrow: return "Expels a single bolt of flame, electricity, acid, or ice upon your foe.";
                case MM3InternalSpellIndex.Revitalize: return "Restores a weakened adventurer to former strength.";
                case MM3InternalSpellIndex.CureWounds: return "Cures serious battle damage.  Restores the broken flesh of warriors.";
                case MM3InternalSpellIndex.Sparks: return "Generates a shower of sparks from your hand that cascades through a group of foes to deliver tiny electric shocks.";
                case MM3InternalSpellIndex.EnergyBlast: return "The greater your skills, the more devastating the blow of this pure energy blast.  Crisping death is delivered to its wretched recipient.";
                case MM3InternalSpellIndex.Sleep: return "Hypnotizes a group of foes into a sleep-like trance to halt their actions.";
                case MM3InternalSpellIndex.Pain: return "Overcomes a group of foes with writhing pain.  A little more than your average bee sting.";
                case MM3InternalSpellIndex.CreateRope: return "If you find yourself without a rope, fear not.  This enchantment will conjure up the very strand you lack.  Cast it over a pit to make it useful.";
                case MM3InternalSpellIndex.ToxicCloud: return "Exudes a noxious cloud around a group of monsters so foul they will choke away their very existence.";
                case MM3InternalSpellIndex.SuppressPoison: return "Without removing poison from an adventurer's system, this incantation will slow its degenerative effects.";
                case MM3InternalSpellIndex.ProtFromElements: return "Temporarily raises the resistance of your party to elemental damage.  The more disciplined the caster, the greater the resistance (This is cast at a power level equal to 5 plus twice the caster's level, with a maximum of 200.  This provides damage reduction against the specific elemental form; it does NOT provide resistance, which is separate, and applies before reduction.  If damage is reduced to zero, then additional status conditions from that attack do not apply.).";
                case MM3InternalSpellIndex.TurnUndead: return "Dissipates the magical energy that animates the dead, reducing to dust any undead armies before you.";
                case MM3InternalSpellIndex.Jump: return "The great hand of the genie Vaultus will sweep you up and over the next step directly ahead, provided there are no walls of matter or magic. (This spell is handy not only jumping over traps and holes in the ground, but also putting distance between you and a monster lacking a ranged attack.)";
                case MM3InternalSpellIndex.AcidStream: return "Sprays a stream of liquescent acid from your palm, robbing an unfortunate foe of much health.";
                case MM3InternalSpellIndex.SuppressDisease: return "The disease will remain and still requires treatment, but this spell will temporarily halt the spreading sickness.";
                case MM3InternalSpellIndex.Silence: return "Steals the tongues of a group of foes to prevent them from reciting spells, but be careful.  That which is lost can once again be found.";
                case MM3InternalSpellIndex.Blessed: return "For a time, this will clothe an adventurer in a magical armor as strong as the one who summons it.";
                case MM3InternalSpellIndex.Levitate: return "For much of the waking day your party will ride above the ground on the breath of the genie Floatious, protecting you from pits of peril. ";
                case MM3InternalSpellIndex.WizardEye: return "Allows your party to see an overhead view of the surroundings.";
                case MM3InternalSpellIndex.IdentifyMonster: return "Gives your party knowledge of the condition of any monsters directly in front of you.";
                case MM3InternalSpellIndex.HolyBonus: return "Brings forth favor from the graces of Bellum, lending extra strength to those who would strike their enemy.";
                case MM3InternalSpellIndex.PowerCure: return "A very powerful aid to the stricken adventurer.  More experienced healers can do a wounded warrior well with this spell.";
                case MM3InternalSpellIndex.NaturesCure: return "The healing powers of Gaiam are bestowed upon you to restore a great deal of fitness to whomsoever you choose.";
                case MM3InternalSpellIndex.LightningBolt: return "Summons a great bolt of lightning directed against a group of your foes.  Your strength is reflected in the crackling charge.";
                case MM3InternalSpellIndex.Immobilize: return "Freezes any vulnerable group of monsters in their tracks, preventing them from attacking with might or magic.";
                case MM3InternalSpellIndex.Heroism: return "An aura of greatness overcomes your character.  You will seem better able to perform within your chosen disciplines.";
                case MM3InternalSpellIndex.WalkOnWater: return "You will step over the waves like walking on a carpet of air.  Deep water will no longer be an obstacle.";
                case MM3InternalSpellIndex.FrostBite: return "Summons the freezing winds of the Frozen Isles to whip around a foe for no small amount of distress.";
                case MM3InternalSpellIndex.LloydsBeacon: return "This special spell can be very useful for returning from long distances.  Cast it once to lay the beacon and then again from anywhere to return.";
                case MM3InternalSpellIndex.PowerShield: return "This magical wall of force absorbs a great deal of damage before letting any pass through to your character. (The damage reduction from this spell applies to all sources of damage; it applies after resistance.  If damage is reduced to zero, then additional status conditions from that attack do not apply.)";
                case MM3InternalSpellIndex.CurePoison: return "Removes toxins from the affected adventurer's system.";
                case MM3InternalSpellIndex.Fireball: return "Heaves a flaming package of death to a group of those unfortunate enough to draw your fire.";
                case MM3InternalSpellIndex.DetectMonster: return "Here is a little notion of what's out there lurking around the corner and watching from behind.";
                case MM3InternalSpellIndex.AcidSpray: return "Showers all foes within sight in a spray of acid.";
                case MM3InternalSpellIndex.ColdRay: return "Unleashes a deluge of freezing mist that glaciates all before you.  More powerful conjurers will find this quite effective.";
                case MM3InternalSpellIndex.CureDisease: return "Removes diseases from an infected adventurer's body.";
                case MM3InternalSpellIndex.NaturesGate: return "Dependent on the day of the week, calls forth a portal to the Towns and Castles of Terra.  In order from Onesday to Tensday: Baywatch, Wildabar, Swamp Town, Blistering Heights, Whiteshield, Blood Reign, Dragontooth, Greywind, Blackwind, Fountain Head";
                case MM3InternalSpellIndex.TimeDistortion: return "Even in the most evil of places there are areas of haven.  Go there with this incantation.";
                case MM3InternalSpellIndex.FeebleMind: return "Turn the minds of those you fight against themselves, and they can no longer take action against you.";
                case MM3InternalSpellIndex.DeadlySwarm: return "Summons a swarm of poisonous, stinging insects that fly around a group of foes to cause a great deal of discomfort.";
                case MM3InternalSpellIndex.Teleport: return "Move like the wind with this spell and travel, light and invisible, nine steps where you like.";
                case MM3InternalSpellIndex.FingerOfDeath: return "Your hand becomes that of all Wizards of lore, resulting in power so great it can only mean suffering to the group receiving its attention.";
                case MM3InternalSpellIndex.CureParalysis: return "Restores the ability of movement to a party member rendered immobile.";
                case MM3InternalSpellIndex.Paralyze: return "Attempts to stiffen the bodies of a group of foes to prevent them from attacking or casting spells.";
                case MM3InternalSpellIndex.DragonBreath: return "Brings forth a torrent of flame, electricity, acid or ice that engulfs any unfortunate enough to be within your view.";
                case MM3InternalSpellIndex.SuperShelter: return "Creates an aura of well being and peace allowing your party to rest in an area otherwise precarious.";
                case MM3InternalSpellIndex.FieryFlail: return "Summons an enormous flail of fire to bring great peril to a single foe.";
                case MM3InternalSpellIndex.CreateFood: return "When the adventure wears longer than anticipated this life saving incantation will increase your party's food supply.";
                case MM3InternalSpellIndex.TownPortal: return "Opens a mysterious gateway to any town.  A great way to return from a tiring adventure.";
                case MM3InternalSpellIndex.StoneToFlesh: return "Restores the flesh of an adventurer to a less ingeneous state.";
                case MM3InternalSpellIndex.RechargeItem: return "Any item of limited use can be rejuvenated with this spell, but know the danger of destruction that faces the greedy. (This will never destroy an item, contrary to what the documentation or game may claim.  The maximum number of charges an item may hold is 63.  An item that can cast Recharge Item can be used to recharge itself.)";
                case MM3InternalSpellIndex.FantasticFreeze: return "Creates a freezing gale that inflicts much damage to those accustomed to warmer climates.";
                case MM3InternalSpellIndex.Duplication: return "Any item in your pack can be reproduced, but the risk of the greedy is more than the weak of heart can bear.";
                case MM3InternalSpellIndex.Disintegrate: return "Pieces of your enemy will dissolve into thin air.";
                case MM3InternalSpellIndex.RaiseDead: return "Restores life to the unlucky in battle.  But life is cheap neither to the one who regains it nor the one who restores. (This will permanently reduce the Endurance of the recipient by 1.  This may be circumvented by raising via either a temple or the Divine Intervention spell.)";
                case MM3InternalSpellIndex.HalfForMe: return "A great testimony to the resolve of the healer, this special spell will restore a member of your party back to health, at great cost.";
                case MM3InternalSpellIndex.Etherealize: return "Waver like a vision in the heat and pass through thin walls and locked doors.";
                case MM3InternalSpellIndex.DancingSword: return "Summons the magical blade of the ages to dance around a group of foes and harm them rather badly.";
                case MM3InternalSpellIndex.PrismaticLight: return "A powerful but erratic spell that has completely unpredictable effects and should thus be used with caution.";
                case MM3InternalSpellIndex.MoonRay: return "Bathes all combatants in a beneficent ray that drains the enemy and restores the adventurers.  A very powerful spell, indeed.";
                case MM3InternalSpellIndex.MassDistortion: return "Makes a group of foes so heavy their every movement inflicts damage upon themselves.";
                case MM3InternalSpellIndex.EnchantItem: return "Gives the weapon of your choice a little extra edge in battle, if you can cut through my meaning.";
                case MM3InternalSpellIndex.Incinerate: return "Reduces a foe to cindering ashes.  Few monsters can withstand the heat of this inferno.";
                case MM3InternalSpellIndex.ElementalStorm: return "Unleashes a torrent of elemental destruction inflicting massive damage to all enemy before you.";
                case MM3InternalSpellIndex.HolyWord: return "It is a word of such devastating power it destroys all undead that are unlucky enough to hear it's resounding cry.";
                case MM3InternalSpellIndex.Resurrect: return "So powerful is this incantation that it can bring the ashes of a fallen warrior back to their former glory.  Not, of course, without a price. (This will permanently reduce the Endurance of the recipient by 1.  This may be circumvented by resurrecting via a temple.)";
                case MM3InternalSpellIndex.MegaVolts: return "Chain lightning dances through a group of foes with force enough to bring even a giant among giants to his knees.";
                case MM3InternalSpellIndex.Inferno: return "Unleashes the very heat of the sun on a group of enemies resulting in great, fiery destruction to all.";
                case MM3InternalSpellIndex.SunRay: return "Heat above description emanates from your body, delivering great, fiery death to all foes before you.";
                case MM3InternalSpellIndex.Implosion: return "One of the most devastating spells that can be cast on a single creature, causing its recipient's body to collapse in on itself.";
                case MM3InternalSpellIndex.StarBurst: return "Pieces of an exploding star rain down before you in a shower of stellar destruction sure to cripple the most formidable of armies.";
                case MM3InternalSpellIndex.DivineIntervention: return "Calls upon the supernatural powers of Esoterica to completely restore your party to full health. (This will reduce or raise hit points to the normal maximum, and will remove all status conditions except ERADICATED, including those which you cannot otherwise heal yourself.)";
                default: return String.Format("Unknown Spell #{0}", (int)index);
            }
        }

        public static string ShortDescription(MM3InternalSpellIndex index)
        {
            switch (index)
            {
                case MM3InternalSpellIndex.Light: return "Provide light in dungeons and caves";
                case MM3InternalSpellIndex.Awaken: return "Remove SLEEP from all characters";
                case MM3InternalSpellIndex.FirstAid: return "+6 hit points to one character";
                case MM3InternalSpellIndex.FlyingFist: return "6 Physical damage to one monster";
                case MM3InternalSpellIndex.Revitalize: return "Remove WEAK from one character";
                case MM3InternalSpellIndex.CureWounds: return "+15 hit points to one character";
                case MM3InternalSpellIndex.Sparks: return "2*Level Electrical damage to one group";
                case MM3InternalSpellIndex.ProtFromElements: return "2*Level + 5 elemental resistance";
                case MM3InternalSpellIndex.Pain: return "8 Physical damage to one group";
                case MM3InternalSpellIndex.SuppressPoison: return "Reduce Poisoned to a minimum of 1";
                case MM3InternalSpellIndex.SuppressDisease: return "Reduce Diseased to a minimum of 1";
                case MM3InternalSpellIndex.TurnUndead: return "Attempt to destroy a group of undead";
                case MM3InternalSpellIndex.Silence: return "Block a group's spellcasting";
                case MM3InternalSpellIndex.Blessed: return "+Level to Armor Class on one character";
                case MM3InternalSpellIndex.HolyBonus: return "+Level to weapon damage on one character";
                case MM3InternalSpellIndex.PowerCure: return "+(2-12)*Level hit points to one character";
                case MM3InternalSpellIndex.Heroism: return "+Level to hit on one character";
                case MM3InternalSpellIndex.Immobilize: return "Prevent a group from attacking";
                case MM3InternalSpellIndex.ColdRay: return "(2-4)*Level Cold damage to all monsters";
                case MM3InternalSpellIndex.CurePoison: return "Remove POISONED from one character";
                case MM3InternalSpellIndex.AcidSpray: return "15 Poison damage to all monsters";
                case MM3InternalSpellIndex.CureDisease: return "Remove DISEASED from one character";
                case MM3InternalSpellIndex.CureParalysis: return "Remove PARALYZED from one character";
                case MM3InternalSpellIndex.Paralyze: return "Stronger version of Immobilize";
                case MM3InternalSpellIndex.CreateFood: return "Add 1 food for each character in the party";
                case MM3InternalSpellIndex.FieryFlail: return "100 Fire damage to one monster";
                case MM3InternalSpellIndex.TownPortal: return "Teleport to a town; noncombat only";
                case MM3InternalSpellIndex.StoneToFlesh: return "Remove STONE from one character";
                case MM3InternalSpellIndex.HalfForMe: return "One character heals; caster damaged for 1/2";
                case MM3InternalSpellIndex.RaiseDead: return "Remove DEAD from one character";
                case MM3InternalSpellIndex.MoonRay: return "30 Energy damage to all; heals party as well";
                case MM3InternalSpellIndex.MassDistortion: return "One group loses half its hit points";
                case MM3InternalSpellIndex.HolyWord: return "Destroy all monsters which are undead";
                case MM3InternalSpellIndex.Resurrect: return "Remove ERADICATED from one character; +5 Age";
                case MM3InternalSpellIndex.SunRay: return "200 Energy damage to all monsters";
                case MM3InternalSpellIndex.DivineIntervention: return "Heal party damage except ERADICATION; +5 Age";
                case MM3InternalSpellIndex.DetectMagic: return "Show charges remaining on items in backpack";
                case MM3InternalSpellIndex.ElementalArrow: return "8 Elemental damage to one monster";
                case MM3InternalSpellIndex.EnergyBlast: return "(2-6)*Level Energy damage to one monster";
                case MM3InternalSpellIndex.Sleep: return "Attempt to put a group to sleep";
                case MM3InternalSpellIndex.CreateRope: return "Allow descent into a pit without a rope";
                case MM3InternalSpellIndex.ToxicCloud: return "10 Poison damage to a group";
                case MM3InternalSpellIndex.Jump: return "Move the party 2 squares; noncombat only";
                case MM3InternalSpellIndex.AcidStream: return "25 Poison damage to one monster";
                case MM3InternalSpellIndex.Levitate: return "Keep the party safe from pits";
                case MM3InternalSpellIndex.WizardEye: return "Show an overhead view of the vicinity";
                case MM3InternalSpellIndex.IdentifyMonster: return "Show monster group hit points, other stats";
                case MM3InternalSpellIndex.LightningBolt: return "(4-6)*Level Electrical damage to a group";
                case MM3InternalSpellIndex.LloydsBeacon: return "Set and recall location; noncombat only";
                case MM3InternalSpellIndex.PowerShield: return "+Level damage reduction on one character";
                case MM3InternalSpellIndex.DetectMonster: return "Show number and location of nearby monsters";
                case MM3InternalSpellIndex.Fireball: return "(3-7)*Level Fire damage to a group";
                case MM3InternalSpellIndex.TimeDistortion: return "Cause the entire party to flee combat";
                case MM3InternalSpellIndex.FeebleMind: return "Block a group's spellcasting";
                case MM3InternalSpellIndex.Teleport: return "Teleport up to 9 squares; noncombat only";
                case MM3InternalSpellIndex.FingerOfDeath: return "Attempt to destroy a group";
                case MM3InternalSpellIndex.SuperShelter: return "Allow safe rest; noncombat and on land only";
                case MM3InternalSpellIndex.DragonBreath: return "5*Level Elemental damage to all monsters";
                case MM3InternalSpellIndex.RechargeItem: return "+1-6 charges to a charged item";
                case MM3InternalSpellIndex.FantasticFreeze: return "40 Cold damage to a group";
                case MM3InternalSpellIndex.Duplication: return "Duplicate an item of level 1-4";
                case MM3InternalSpellIndex.Disintegrate: return "Attempt to disintegrate a group";
                case MM3InternalSpellIndex.Etherealize: return "Move straight through doors, other barriers";
                case MM3InternalSpellIndex.DancingSword: return "(6-14)*Level Physical damage to a group";
                case MM3InternalSpellIndex.EnchantItem: return "Enchants an item which isn't already magical";
                case MM3InternalSpellIndex.Incinerate: return "250 Fire damage to one monster";
                case MM3InternalSpellIndex.MegaVolts: return "150 Electrical damage to a group";
                case MM3InternalSpellIndex.Inferno: return "250 Fire damage to a group";
                case MM3InternalSpellIndex.Implosion: return "1000 Energy damage to one monster";
                case MM3InternalSpellIndex.StarBurst: return "500 Physical damage to all monsters";
                case MM3InternalSpellIndex.NaturesCure: return "+25 hit points to one character";
                case MM3InternalSpellIndex.WalkOnWater: return "Allow the party to travel on deep water";
                case MM3InternalSpellIndex.FrostBite: return "35 Cold damage to one monster";
                case MM3InternalSpellIndex.NaturesGate: return "Portal dependent on day; noncombat only";
                case MM3InternalSpellIndex.DeadlySwarm: return "40 Physical damage to a group";
                case MM3InternalSpellIndex.PrismaticLight: return "Random effects on all monsters";
                case MM3InternalSpellIndex.ElementalStorm: return "150 Elemental damage to all monsters";
                default: return String.Format("Unknown Spell #{0}", (int)index);
            }
        }

        public static string WhereLearned(MM3InternalSpellIndex index)
        {
            switch (index)
            {
                case MM3InternalSpellIndex.Light: return "A-1, Ancient Temple of Moo (5,1)";
                case MM3InternalSpellIndex.Awaken: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.FirstAid: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.FlyingFist: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.Revitalize: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.CureWounds: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.Sparks: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.ProtFromElements: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.Pain: return "A-1, Ancient Temple of Moo (6,29)";
                case MM3InternalSpellIndex.SuppressPoison: return "A-1, Ancient Temple of Moo (2,16)";
                case MM3InternalSpellIndex.SuppressDisease: return "A-1, Ancient Temple of Moo (9,12)";
                case MM3InternalSpellIndex.TurnUndead: return "A-2, Baywatch Cavern (0,8) or A-1, Ancient Temple of Moo (26,1)";
                case MM3InternalSpellIndex.Silence: return "A-1, Ancient Temple of Moo (31,15)";
                case MM3InternalSpellIndex.Blessed: return "B-3, Cathedral of Carnage (25,8)";
                case MM3InternalSpellIndex.HolyBonus: return "E-2, Swamp Town (14,14)";
                case MM3InternalSpellIndex.PowerCure: return "A-2, Baywatch Cavern (0,10)";
                case MM3InternalSpellIndex.Heroism: return "E-2, Swamp Town (12,5)";
                case MM3InternalSpellIndex.Immobilize: return "B-3, Cathedral of Carnage (26,8)";
                case MM3InternalSpellIndex.ColdRay: return "B-3, Cathedral of Carnage (27,8)";
                case MM3InternalSpellIndex.CurePoison: return "B-2, Surface (11,7) or B-4, Wildabar (14,2) or B-4, Arachnoid Cavern (24,31)";
                case MM3InternalSpellIndex.AcidSpray: return "B-3, Cathedral of Carnage (28,8)";
                case MM3InternalSpellIndex.CureDisease: return "B-4, Wildabar (14,1)";
                case MM3InternalSpellIndex.CureParalysis: return "E-2, Swamp Town (9,2)";
                case MM3InternalSpellIndex.Paralyze: return "B-3, Cathedral of Carnage (30,3)";
                case MM3InternalSpellIndex.CreateFood: return "B2 (2,15) or E-2, Swamp Town Cavern (10,10)";
                case MM3InternalSpellIndex.FieryFlail: return "B-4, Arachnoid Cavern (5,15)";
                case MM3InternalSpellIndex.TownPortal: return "B-3, Cathedral of Carnage (30,4)";
                case MM3InternalSpellIndex.StoneToFlesh: return "B-3, Cathedral of Carnage (30,5)";
                case MM3InternalSpellIndex.HalfForMe: return "B-4, Arachnoid Cavern (15,26)";
                case MM3InternalSpellIndex.RaiseDead: return "D-3, Blistering Heights Cavern (11,12) or B-3, Cathedral of Carnage (30,6)";
                case MM3InternalSpellIndex.MoonRay: return "B-3, Cathedral of Carnage (4,11)";
                case MM3InternalSpellIndex.MassDistortion: return "B-3, Cathedral of Carnage (4,3)";
                case MM3InternalSpellIndex.HolyWord: return "F-3, Surface (6,15) or B-3, Cathedral of Carnage (18,21)";
                case MM3InternalSpellIndex.Resurrect: return "B-3, Cathedral of Carnage (18,20)";
                case MM3InternalSpellIndex.SunRay: return "F-2, Tomb of Terror (28,10)";
                case MM3InternalSpellIndex.DivineIntervention: return "F-2, Surface (3,2) or F-2, Tomb of Terror (22,10)";
                case MM3InternalSpellIndex.DetectMagic: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.ElementalArrow: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.EnergyBlast: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.Sleep: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.CreateRope: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.ToxicCloud: return "A-1, Fountainhead (10,3)";
                case MM3InternalSpellIndex.Jump: return "B-1, Cyclops Cavern (17,31)";
                case MM3InternalSpellIndex.AcidStream: return "A-2, Baywatch (14,1)";
                case MM3InternalSpellIndex.Levitate: return "A-2, Baywatch Cavern (15,7)";
                case MM3InternalSpellIndex.WizardEye: return "A-1, Surface (13,6)";
                case MM3InternalSpellIndex.IdentifyMonster: return "A-1, Surface (13,8)";
                case MM3InternalSpellIndex.LightningBolt: return "A-2, Baywatch Cavern (3,5) or B-1, Cyclops Cavern (7,16)";
                case MM3InternalSpellIndex.LloydsBeacon: return "A-2, Baywatch Cavern (15,5)";
                case MM3InternalSpellIndex.PowerShield: return "B-1, Cyclops Cavern (18,23)";
                case MM3InternalSpellIndex.DetectMonster: return "B-1, Cyclops Cavern (19,17)";
                case MM3InternalSpellIndex.Fireball: return "A-1, Ancient Temple of Moo (12,24) or B-1, Cyclops Cavern (18,19) or E-4, Magic Cavern (13,29)";
                case MM3InternalSpellIndex.TimeDistortion: return "A-3, Surface (8,14) or B-1, Cyclops Cavern (3,17) or E-4, Magic Cavern (26,23)";
                case MM3InternalSpellIndex.FeebleMind: return "E-4, Magic Cavern (16,16)";
                case MM3InternalSpellIndex.Teleport: return "E-2, Swamp Town (1,1)";
                case MM3InternalSpellIndex.FingerOfDeath: return "B-1, Cyclops Cavern (2,17)";
                case MM3InternalSpellIndex.SuperShelter: return "E-4, Magic Cavern (5,24)";
                case MM3InternalSpellIndex.DragonBreath: return "A-3, Surface (5,15) or E-2, Swamp Town Cavern (13,15) or E-4, Magic Cavern (30,22)";
                case MM3InternalSpellIndex.RechargeItem: return "B-4, Arachnoid Cavern (5,31)";
                case MM3InternalSpellIndex.FantasticFreeze: return "B-1, Cyclops Cavern (1,10)";
                case MM3InternalSpellIndex.Duplication: return "E-4, Magic Cavern (22,15)";
                case MM3InternalSpellIndex.Disintegrate: return "E-4, Magic Cavern (26,20)";
                case MM3InternalSpellIndex.Etherealize: return "B-3, Surface (6,6) or E-4, Magic Cavern (30,12)";
                case MM3InternalSpellIndex.DancingSword: return "E-4, Magic Cavern (29,2)";
                case MM3InternalSpellIndex.EnchantItem: return "B-3, Surface (8,2) or B-4, Arachnoid Cavern (18,20)";
                case MM3InternalSpellIndex.Incinerate: return "D-3, Blistering Heights Cavern (5,12)";
                case MM3InternalSpellIndex.MegaVolts: return "E-4, Magic Cavern (5,0)";
                case MM3InternalSpellIndex.Inferno: return "E-4, Magic Cavern (13,2)";
                case MM3InternalSpellIndex.Implosion: return "E-4, Magic Cavern (11,2)";
                case MM3InternalSpellIndex.StarBurst: return "E-4, Magic Cavern (2,24)";
                case MM3InternalSpellIndex.NaturesCure: return "B-4, Wildabar (1,14)";
                case MM3InternalSpellIndex.WalkOnWater: return "B-4, Wildabar (14,6)";
                case MM3InternalSpellIndex.FrostBite: return "B-4, Wildabar (1,14)";
                case MM3InternalSpellIndex.NaturesGate: return "B-4, Wildabar (14,10) or B-4, Arachnoid Cavern (23,15)";
                case MM3InternalSpellIndex.DeadlySwarm: return "B-4, Arachnoid Cavern (31,3)";
                case MM3InternalSpellIndex.PrismaticLight: return "D-3, Blistering Heights (14,14)";
                case MM3InternalSpellIndex.ElementalStorm: return "D-3, Blistering Heights (14,14)";
                default: return String.Format("Unknown Spell #{0}", (int)index);
            }
        }

    }

    public class MM3Spell : MM345Spell
    {
        public MM3SpellIndex Index
        {
            get { return (MM3SpellIndex)Index345; }
            set { Index345 = (int)value; }
        }

        public MM3InternalSpellIndex InternalIndex 
        {
            get { return (MM3InternalSpellIndex)InternalIndex345; }
            set { InternalIndex345 = (int)value; }
        }

        public override int BasicIndex { get { return (int)Index345; } }

        public MM3Spell(MM3SpellIndex index, MM3InternalSpellIndex iInternal, string name, SpellType type, int sp, bool perlevel,
            int gems, int level, SpellWhen when, SpellTarget target, string shortDesc, string desc, string location)
        {
            SetInfo((int) index, (int) iInternal, name, type, sp, perlevel, gems, level, when, target, shortDesc, desc, location);
        }

        public MM3Spell(MM3SpellIndex index, MM3InternalSpellIndex iInternal, SpellType type, int sp, bool perlevel,
            int gems, int level, SpellWhen when, SpellTarget target)
        {
            SetInfo((int)index, (int)iInternal, MM3SpellList.GetSpellName(iInternal), type, sp, perlevel, gems, level, when, target,
                MM3SpellList.ShortDescription(iInternal), MM3SpellList.LongDescription(iInternal), MM3SpellList.WhereLearned(iInternal));
        }

        public static MM3Spell MM3None { get { return new MM3Spell(MM3SpellIndex.None, MM3InternalSpellIndex.None, "None", SpellType.Unknown, 0, false, 0, 0, SpellWhen.None, SpellTarget.Unknown, "", "", ""); } }

        public override MMSpell None { get { return MM3None; } }

        public override Keys[] GetKeys(BaseCharacter character)
        {
            MM3Character mm3Char = character as MM3Character;
            if (mm3Char == null)
                return null;

            if (!mm3Char.Spells.IsKnown((int) Index, Type))
                return null;

            // The keys to send depends on which spells the character knows, because they are all shown in a list of 10 each
            // So send a PageDown for each 10 spells the character knows before the desired one
            // and then down arrows for the remainder
            MM3SpellIndex firstSpell = MM3SpellIndex.None;
            MM3SpellIndex lastSpell = MM3SpellIndex.None;
            SpellType spellType = SpellType.Cleric;
            switch (mm3Char.Class)
            {
                case MM345Class.Cleric:
                case MM345Class.Paladin:
                    firstSpell = MM3SpellIndex.LightCleric;
                    lastSpell = MM3SpellIndex.DivineIntervention;
                    spellType = SpellType.Cleric;
                    break;
                case MM345Class.Sorcerer:
                case MM345Class.Archer:
                    firstSpell = MM3SpellIndex.LightSorcerer;
                    lastSpell = MM3SpellIndex.StarBurst;
                    spellType = SpellType.Sorcerer;
                    break;
                case MM345Class.Druid:
                case MM345Class.Ranger:
                    firstSpell = MM3SpellIndex.LightDruid;
                    lastSpell = MM3SpellIndex.ElementalStorm;
                    spellType = SpellType.Druid;
                    break;
                default:
                    return null;    // Not a caster
            }
            MM3SpellIndex spell = firstSpell;

            int iDown = 0;
            while (spell != Index)
            {
                if (spell > lastSpell)
                    break;
                if (mm3Char.Spells.IsKnown((int)spell, spellType))
                    iDown++;
                spell++;
            }

            return KeysForKnownSpell(mm3Char.Spells.NumKnown, iDown);
        }
    }
}
