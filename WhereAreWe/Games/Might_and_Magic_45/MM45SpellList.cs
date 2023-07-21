using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public enum MM45Guild
    {
        None,
        Vertigo,
        Nightshadow,
        Rivercity,
        Asp,
        Winterkill,
        ShangriLa,
        Castleview,
        Sandcaster,
        Lakeside,
        Necropolis,
        Olympus
    }

    public enum MM45InternalSpellIndex
    {
        None = -1,
        First = 0,
        AcidSpray = 0,
        Awaken,
        BeastMaster,
        Bless,
        Clairvoyance,
        ColdRay,
        CreateFood,
        CureDisease,
        CureParalysis,
        CurePoison,
        CureWounds,
        DancingSword,
        DayOfProtection,
        DayOfSorcery,
        DeadlySwarm,
        DetectMonster,
        DivineIntervention,
        DragonSleep,
        ElementalStorm,
        EnchantItem,
        EnergyBlast,
        Etherealize,
        FantasticFreeze,
        FieryFlail,
        FingerOfDeath,
        FireBall,
        FirstAid,
        FlyingFist,
        FrostBite,
        GolemStopper,
        Heroism,
        HolyBonus,
        HolyWord,
        Hypnotize,
        IdentifyMonster,
        Implosion,
        Incinerate,
        Inferno,
        InsectSpray,
        ItemToGold,
        Jump,
        Levitate,
        Light,
        LightningBolt,
        LloydsBeacon,
        MagicArrow,
        MassDistortion,
        MegaVolts,
        MoonRay,
        NaturesCure,
        Pain,
        PoisonVolley,
        PowerCure,
        PowerShield,
        PrismaticLight,
        ProtFromElements,
        RaiseDead,
        RechargeItem,
        Resurrect,
        Revitalize,
        Shrapmetal,
        Sleep,
        Sparks,
        StarBurst,
        StoneToFlesh,
        SunRay,
        SuperShelter,
        SuppressDisease,
        SuppressPoison,
        Teleport,
        TimeDistortion,
        TownPortal,
        ToxicCloud,
        TurnUndead,
        WalkOnWater,
        WizardEye,
        Last = WizardEye + 1,
    }

    public enum MM45SpellIndex
    {
        // Cleric Spells
        AcidSpray,
        FirstCleric = AcidSpray,
        Awaken,
        BeastMaster,
        Bless,
        ColdRay,
        CreateFood,
        CureDisease,
        CureParalysis,
        CurePoison,
        CureWounds,
        DayOfProtection,
        DeadlySwarm,
        DivineIntervention,
        FieryFlail,
        FirstAid,
        FlyingFist,
        FrostBite,
        Heroism,
        HolyBonus,
        HolyWord,
        Hypnotize,
        Light,
        MassDistortion,
        MoonRay,
        NaturesCure,
        Pain,
        PowerCure,
        ProtFromElements,
        RaiseDead,
        Resurrect,
        Revitalize,
        Sparks,
        StoneToFlesh,
        SunRay,
        SuppressDisease,
        SuppressPoison,
        TownPortal,
        TurnUndead,
        WalkOnWater,
        LastCleric = WalkOnWater,

        // Arcane Spells
        AwakenArcane,
        FirstArcane = AwakenArcane,
        Clairvoyance,
        DancingSword,
        DayOfSorcery,
        DetectMonster,
        DragonSleep,
        ElementalStorm,
        EnchantItem,
        EnergyBlast,
        Etherealize,
        FantasticFreeze,
        FingerOfDeath,
        Fireball,
        GolemStopper,
        IdentifyMonster,
        Implosion,
        Incinerate,
        Inferno,
        InsectSpray,
        ItemToGold,
        Jump,
        Levitate,
        LightArcane,
        LightningBolt,
        LloydsBeacon,
        MagicArrow,
        MegaVolts,
        PoisonVolley,
        PowerShield,
        PrismaticLight,
        RechargeItem,
        Shrapmetal,
        Sleep,
        StarBurst,
        SuperShelter,
        Teleport,
        TimeDistortion,
        ToxicCloud,
        WizardEye,
        LastArcane = WizardEye,

        // Druid Spells
        AcidSprayDruid,
        FirstDruid = AcidSprayDruid,
        AwakenDruid,
        BeastMasterDruid,
        BlessDruid,
        ClairvoyanceDruid,
        ColdRayDruid,
        CureDiseaseDruid,
        CurePoisonDruid,
        CureWoundsDruid,
        EnergyBlastDruid,
        FireballDruid,
        FirstAidDruid,
        FlyingFistDruid,
        FrostBiteDruid,
        HeroismDruid,
        HolyBonusDruid,
        IdentifyMonsterDruid,
        InsectSprayDruid,
        JumpDruid,
        LevitateDruid,
        LightDruid,
        LightningBoltDruid,
        LloydsBeaconDruid,
        MagicArrowDruid,
        NaturesCureDruid,
        PainDruid,
        PowerCureDruid,
        PowerShieldDruid,
        ProtFromElementsDruid,
        RevitalizeDruid,
        ShrapmetalDruid,
        SleepDruid,
        SparksDruid,
        SuppressDiseaseDruid,
        SuppressPoisonDruid,
        ToxicCloudDruid,
        TurnUndeadDruid,
        WalkOnWaterDruid,
        WizardEyeDruid,
        LastDruid = WizardEyeDruid,
        Last = LastDruid,
        None = 255,
    }

    public class MM45SpellList
    {
        Dictionary<MM45SpellIndex, MM45Spell> m_spells;

        public Dictionary<MM45SpellIndex, MM45Spell> Spells
        {
            get { return m_spells; }
        }

        public static string GetSpellName(byte b, SpellType type)
        {
            switch (type)
            {
                case SpellType.Cleric: return GetSpellName((MM45SpellIndex) (MM45SpellIndex.FirstCleric + b));
                case SpellType.Sorcerer: return GetSpellName((MM45SpellIndex)(MM45SpellIndex.FirstArcane + b));
                case SpellType.Druid: return GetSpellName((MM45SpellIndex)(MM45SpellIndex.FirstDruid + b));
                default: return "None";
            }
        }

        private static string[] m_spellNames = null;

        public static string[] GetSpellNames()
        {
            if (m_spellNames != null)
                return m_spellNames;

            MM45InternalSpellIndex[] spells = GetInternalIndexList();
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

            MM45InternalSpellIndex[] spells = GetInternalIndexList();
            m_spellList = new Spell[spells.Length];
            for (int i = 0; i < m_spellList.Length; i++)
                m_spellList[i] = GetSpell(spells[i]);
            return m_spellList;
        }

        public static string GetSpellName(MM45SpellIndex index)
        {
            if (index == MM45SpellIndex.None || index > MM45SpellIndex.Last)
                return "None";

            MM45Spell spell = MM45.Spells[index];

            return spell.Name;
        }

        private static MM45InternalSpellIndex[] m_internalIndexList = null;

        public static MM45InternalSpellIndex[] GetInternalIndexList()
        {
            if (m_internalIndexList != null)
                return m_internalIndexList;

            m_internalIndexList = new MM45InternalSpellIndex[MM45InternalSpellIndex.Last - MM45InternalSpellIndex.First + 1];
            for (MM45InternalSpellIndex si = MM45InternalSpellIndex.First; si <= MM45InternalSpellIndex.Last; si++)
                m_internalIndexList[si - MM45InternalSpellIndex.First] = si;
            return m_internalIndexList;
        }

        public static string GetSpellName(MM45InternalSpellIndex index)
        {
            switch (index)
            {
                case MM45InternalSpellIndex.None: return "None";
                case MM45InternalSpellIndex.Light: return "Light";
                case MM45InternalSpellIndex.Awaken: return "Awaken";
                case MM45InternalSpellIndex.MagicArrow: return "Magic Arrow";
                case MM45InternalSpellIndex.FirstAid: return "First Aid";
                case MM45InternalSpellIndex.FlyingFist: return "Flying Fist";
                case MM45InternalSpellIndex.EnergyBlast: return "Energy Blast";
                case MM45InternalSpellIndex.Sleep: return "Sleep";
                case MM45InternalSpellIndex.Revitalize: return "Revitalize";
                case MM45InternalSpellIndex.CureWounds: return "Cure Wounds";
                case MM45InternalSpellIndex.Sparks: return "Sparks";
                case MM45InternalSpellIndex.Shrapmetal: return "Shrapmetal";
                case MM45InternalSpellIndex.InsectSpray: return "Insect Spray";
                case MM45InternalSpellIndex.ToxicCloud: return "Toxic Cloud";
                case MM45InternalSpellIndex.ProtFromElements: return "Prot from Elements";
                case MM45InternalSpellIndex.Pain: return "Pain";
                case MM45InternalSpellIndex.Jump: return "Jump";
                case MM45InternalSpellIndex.BeastMaster: return "Beast Master";
                case MM45InternalSpellIndex.Clairvoyance: return "Clairvoyance";
                case MM45InternalSpellIndex.TurnUndead: return "Turn Undead";
                case MM45InternalSpellIndex.Levitate: return "Levitate";
                case MM45InternalSpellIndex.WizardEye: return "Wizard Eye";
                case MM45InternalSpellIndex.Bless: return "Bless";
                case MM45InternalSpellIndex.IdentifyMonster: return "Identify Monster";
                case MM45InternalSpellIndex.LightningBolt: return "Lightning";
                case MM45InternalSpellIndex.HolyBonus: return "Holy Bonus";
                case MM45InternalSpellIndex.PowerCure: return "Power Cure";
                case MM45InternalSpellIndex.NaturesCure: return "Nature's Cure";
                case MM45InternalSpellIndex.LloydsBeacon: return "Lloyd's Beacon";
                case MM45InternalSpellIndex.PowerShield: return "Power Shield";
                case MM45InternalSpellIndex.Heroism: return "Heroism";
                case MM45InternalSpellIndex.Hypnotize: return "Hypnotize";
                case MM45InternalSpellIndex.WalkOnWater: return "Walk On Water";
                case MM45InternalSpellIndex.FrostBite: return "Frost Bite";
                case MM45InternalSpellIndex.DetectMonster: return "Detect Monster";
                case MM45InternalSpellIndex.FireBall: return "Fireball";
                case MM45InternalSpellIndex.ColdRay: return "Cold Ray";
                case MM45InternalSpellIndex.CurePoison: return "Cure Poison";
                case MM45InternalSpellIndex.AcidSpray: return "Acid Spray";
                case MM45InternalSpellIndex.TimeDistortion: return "Time Distortion";
                case MM45InternalSpellIndex.DragonSleep: return "Dragon Sleep";
                case MM45InternalSpellIndex.CureDisease: return "Cure Disease";
                case MM45InternalSpellIndex.Teleport: return "Teleport";
                case MM45InternalSpellIndex.FingerOfDeath: return "Finger Of Death";
                case MM45InternalSpellIndex.CureParalysis: return "Cure Paralysis";
                case MM45InternalSpellIndex.GolemStopper: return "Golem Stopper";
                case MM45InternalSpellIndex.PoisonVolley: return "Poison Volley";
                case MM45InternalSpellIndex.DeadlySwarm: return "Deadly Swarm";
                case MM45InternalSpellIndex.SuperShelter: return "Super Shelter";
                case MM45InternalSpellIndex.DayOfProtection: return "Day of Protection";
                case MM45InternalSpellIndex.DayOfSorcery: return "Day of Sorcery";
                case MM45InternalSpellIndex.CreateFood: return "Create Food";
                case MM45InternalSpellIndex.FieryFlail: return "Fiery Flail";
                case MM45InternalSpellIndex.RechargeItem: return "Recharge Item";
                case MM45InternalSpellIndex.FantasticFreeze: return "Fantastic Freeze";
                case MM45InternalSpellIndex.TownPortal: return "Town Portal";
                case MM45InternalSpellIndex.StoneToFlesh: return "Stone to Flesh";
                case MM45InternalSpellIndex.RaiseDead: return "Raise Dead";
                case MM45InternalSpellIndex.Etherealize: return "Etherealize";
                case MM45InternalSpellIndex.DancingSword: return "Dancing Sword";
                case MM45InternalSpellIndex.MoonRay: return "Moon Ray";
                case MM45InternalSpellIndex.MassDistortion: return "Mass Distortion";
                case MM45InternalSpellIndex.PrismaticLight: return "Prismatic Light";
                case MM45InternalSpellIndex.EnchantItem: return "Enchant Item";
                case MM45InternalSpellIndex.Incinerate: return "Incinerate";
                case MM45InternalSpellIndex.HolyWord: return "Holy Word";
                case MM45InternalSpellIndex.Resurrect: return "Resurrect";
                case MM45InternalSpellIndex.ElementalStorm: return "Elemental Storm";
                case MM45InternalSpellIndex.MegaVolts: return "Mega Volts";
                case MM45InternalSpellIndex.Inferno: return "Inferno";
                case MM45InternalSpellIndex.SunRay: return "Sun Ray";
                case MM45InternalSpellIndex.Implosion: return "Implosion";
                case MM45InternalSpellIndex.StarBurst: return "Star Burst";
                case MM45InternalSpellIndex.DivineIntervention: return "Divine Intervention";
                case MM45InternalSpellIndex.SuppressDisease: return "Suppress Disease";
                case MM45InternalSpellIndex.SuppressPoison: return "Suppress Poison";
                case MM45InternalSpellIndex.ItemToGold: return "Item to Gold";
                default: return "Unknown";
            }
        }

        public MM45Spell GetSpell(MM45InternalSpellIndex index)
        {
            switch (index)
            {
                case MM45InternalSpellIndex.AcidSpray: return Spells[MM45SpellIndex.AcidSpray];
                case MM45InternalSpellIndex.Awaken: return Spells[MM45SpellIndex.Awaken];
                case MM45InternalSpellIndex.BeastMaster: return Spells[MM45SpellIndex.BeastMaster];
                case MM45InternalSpellIndex.Bless: return Spells[MM45SpellIndex.Bless];
                case MM45InternalSpellIndex.Clairvoyance: return Spells[MM45SpellIndex.Clairvoyance];
                case MM45InternalSpellIndex.ColdRay: return Spells[MM45SpellIndex.ColdRay];
                case MM45InternalSpellIndex.CreateFood: return Spells[MM45SpellIndex.CreateFood];
                case MM45InternalSpellIndex.CureDisease: return Spells[MM45SpellIndex.CureDisease];
                case MM45InternalSpellIndex.CureParalysis: return Spells[MM45SpellIndex.CureParalysis];
                case MM45InternalSpellIndex.CurePoison: return Spells[MM45SpellIndex.CurePoison];
                case MM45InternalSpellIndex.CureWounds: return Spells[MM45SpellIndex.CureWounds];
                case MM45InternalSpellIndex.DancingSword: return Spells[MM45SpellIndex.DancingSword];
                case MM45InternalSpellIndex.DayOfProtection: return Spells[MM45SpellIndex.DayOfProtection];
                case MM45InternalSpellIndex.DayOfSorcery: return Spells[MM45SpellIndex.DayOfSorcery];
                case MM45InternalSpellIndex.DeadlySwarm: return Spells[MM45SpellIndex.DeadlySwarm];
                case MM45InternalSpellIndex.DetectMonster: return Spells[MM45SpellIndex.DetectMonster];
                case MM45InternalSpellIndex.DivineIntervention: return Spells[MM45SpellIndex.DivineIntervention];
                case MM45InternalSpellIndex.DragonSleep: return Spells[MM45SpellIndex.DragonSleep];
                case MM45InternalSpellIndex.ElementalStorm: return Spells[MM45SpellIndex.ElementalStorm];
                case MM45InternalSpellIndex.EnchantItem: return Spells[MM45SpellIndex.EnchantItem];
                case MM45InternalSpellIndex.EnergyBlast: return Spells[MM45SpellIndex.EnergyBlast];
                case MM45InternalSpellIndex.Etherealize: return Spells[MM45SpellIndex.Etherealize];
                case MM45InternalSpellIndex.FantasticFreeze: return Spells[MM45SpellIndex.FantasticFreeze];
                case MM45InternalSpellIndex.FieryFlail: return Spells[MM45SpellIndex.FieryFlail];
                case MM45InternalSpellIndex.FingerOfDeath: return Spells[MM45SpellIndex.FingerOfDeath];
                case MM45InternalSpellIndex.FireBall: return Spells[MM45SpellIndex.Fireball];
                case MM45InternalSpellIndex.FirstAid: return Spells[MM45SpellIndex.FirstAid];
                case MM45InternalSpellIndex.FlyingFist: return Spells[MM45SpellIndex.FlyingFist];
                case MM45InternalSpellIndex.FrostBite: return Spells[MM45SpellIndex.FrostBite];
                case MM45InternalSpellIndex.GolemStopper: return Spells[MM45SpellIndex.GolemStopper];
                case MM45InternalSpellIndex.Heroism: return Spells[MM45SpellIndex.Heroism];
                case MM45InternalSpellIndex.HolyBonus: return Spells[MM45SpellIndex.HolyBonus];
                case MM45InternalSpellIndex.HolyWord: return Spells[MM45SpellIndex.HolyWord];
                case MM45InternalSpellIndex.Hypnotize: return Spells[MM45SpellIndex.Hypnotize];
                case MM45InternalSpellIndex.IdentifyMonster: return Spells[MM45SpellIndex.IdentifyMonster];
                case MM45InternalSpellIndex.Implosion: return Spells[MM45SpellIndex.Implosion];
                case MM45InternalSpellIndex.Incinerate: return Spells[MM45SpellIndex.Incinerate];
                case MM45InternalSpellIndex.Inferno: return Spells[MM45SpellIndex.Inferno];
                case MM45InternalSpellIndex.InsectSpray: return Spells[MM45SpellIndex.InsectSpray];
                case MM45InternalSpellIndex.ItemToGold: return Spells[MM45SpellIndex.ItemToGold];
                case MM45InternalSpellIndex.Jump: return Spells[MM45SpellIndex.Jump];
                case MM45InternalSpellIndex.Levitate: return Spells[MM45SpellIndex.Levitate];
                case MM45InternalSpellIndex.Light: return Spells[MM45SpellIndex.Light];
                case MM45InternalSpellIndex.LightningBolt: return Spells[MM45SpellIndex.LightningBolt];
                case MM45InternalSpellIndex.LloydsBeacon: return Spells[MM45SpellIndex.LloydsBeacon];
                case MM45InternalSpellIndex.MagicArrow: return Spells[MM45SpellIndex.MagicArrow];
                case MM45InternalSpellIndex.MassDistortion: return Spells[MM45SpellIndex.MassDistortion];
                case MM45InternalSpellIndex.MegaVolts: return Spells[MM45SpellIndex.MegaVolts];
                case MM45InternalSpellIndex.MoonRay: return Spells[MM45SpellIndex.MoonRay];
                case MM45InternalSpellIndex.NaturesCure: return Spells[MM45SpellIndex.NaturesCure];
                case MM45InternalSpellIndex.Pain: return Spells[MM45SpellIndex.Pain];
                case MM45InternalSpellIndex.PoisonVolley: return Spells[MM45SpellIndex.PoisonVolley];
                case MM45InternalSpellIndex.PowerCure: return Spells[MM45SpellIndex.PowerCure];
                case MM45InternalSpellIndex.PowerShield: return Spells[MM45SpellIndex.PowerShield];
                case MM45InternalSpellIndex.PrismaticLight: return Spells[MM45SpellIndex.PrismaticLight];
                case MM45InternalSpellIndex.ProtFromElements: return Spells[MM45SpellIndex.ProtFromElements];
                case MM45InternalSpellIndex.RaiseDead: return Spells[MM45SpellIndex.RaiseDead];
                case MM45InternalSpellIndex.RechargeItem: return Spells[MM45SpellIndex.RechargeItem];
                case MM45InternalSpellIndex.Resurrect: return Spells[MM45SpellIndex.Resurrect];
                case MM45InternalSpellIndex.Revitalize: return Spells[MM45SpellIndex.Revitalize];
                case MM45InternalSpellIndex.Shrapmetal: return Spells[MM45SpellIndex.Shrapmetal];
                case MM45InternalSpellIndex.Sleep: return Spells[MM45SpellIndex.Sleep];
                case MM45InternalSpellIndex.Sparks: return Spells[MM45SpellIndex.Sparks];
                case MM45InternalSpellIndex.StarBurst: return Spells[MM45SpellIndex.StarBurst];
                case MM45InternalSpellIndex.StoneToFlesh: return Spells[MM45SpellIndex.StoneToFlesh];
                case MM45InternalSpellIndex.SunRay: return Spells[MM45SpellIndex.SunRay];
                case MM45InternalSpellIndex.SuperShelter: return Spells[MM45SpellIndex.SuperShelter];
                case MM45InternalSpellIndex.SuppressDisease: return Spells[MM45SpellIndex.SuppressDisease];
                case MM45InternalSpellIndex.SuppressPoison: return Spells[MM45SpellIndex.SuppressPoison];
                case MM45InternalSpellIndex.Teleport: return Spells[MM45SpellIndex.Teleport];
                case MM45InternalSpellIndex.TimeDistortion: return Spells[MM45SpellIndex.TimeDistortion];
                case MM45InternalSpellIndex.TownPortal: return Spells[MM45SpellIndex.TownPortal];
                case MM45InternalSpellIndex.ToxicCloud: return Spells[MM45SpellIndex.ToxicCloud];
                case MM45InternalSpellIndex.TurnUndead: return Spells[MM45SpellIndex.TurnUndead];
                case MM45InternalSpellIndex.WalkOnWater: return Spells[MM45SpellIndex.WalkOnWater];
                case MM45InternalSpellIndex.WizardEye: return Spells[MM45SpellIndex.WizardEye];
                default: return MM45Spell.MM45None;
            }
        }

        public static int GetSpellPurchasePrice(MM45InternalSpellIndex index, GenericClass mmClass)
        {
            switch (mmClass)
            {
                case GenericClass.Cleric:
                case GenericClass.Druid:
                case GenericClass.Sorcerer:
                    return GetSpellPurchasePrice(index);
                default:
                    return GetSpellPurchasePrice(index) * 2;
            }
        }

        public static int GetSpellPurchasePrice(MM45InternalSpellIndex index)
        {
            switch (index)
            {
                case MM45InternalSpellIndex.AcidSpray: return 800;
                case MM45InternalSpellIndex.Awaken: return 100;
                case MM45InternalSpellIndex.BeastMaster: return 500;
                case MM45InternalSpellIndex.Bless: return 1000;
                case MM45InternalSpellIndex.Clairvoyance: return 500;
                case MM45InternalSpellIndex.ColdRay: return 1000;
                case MM45InternalSpellIndex.CreateFood: return 2000;
                case MM45InternalSpellIndex.CureDisease: return 1000;
                case MM45InternalSpellIndex.CureParalysis: return 1200;
                case MM45InternalSpellIndex.CurePoison: return 800;
                case MM45InternalSpellIndex.CureWounds: return 300;
                case MM45InternalSpellIndex.DancingSword: return 1500;
                case MM45InternalSpellIndex.DayOfProtection: return 7500;
                case MM45InternalSpellIndex.DayOfSorcery: return 4000;
                case MM45InternalSpellIndex.DeadlySwarm: return 1200;
                case MM45InternalSpellIndex.DetectMonster: return 600;
                case MM45InternalSpellIndex.DivineIntervention: return 20000;
                case MM45InternalSpellIndex.DragonSleep: return 1000;
                case MM45InternalSpellIndex.ElementalStorm: return 10000;
                case MM45InternalSpellIndex.EnchantItem: return 3000;
                case MM45InternalSpellIndex.EnergyBlast: return 500;
                case MM45InternalSpellIndex.Etherealize: return 3000;
                case MM45InternalSpellIndex.FantasticFreeze: return 1500;
                case MM45InternalSpellIndex.FieryFlail: return 2500;
                case MM45InternalSpellIndex.FingerOfDeath: return 1000;
                case MM45InternalSpellIndex.FireBall: return 1000;
                case MM45InternalSpellIndex.FirstAid: return 100;
                case MM45InternalSpellIndex.FlyingFist: return 200;
                case MM45InternalSpellIndex.FrostBite: return 700;
                case MM45InternalSpellIndex.GolemStopper: return 2000;
                case MM45InternalSpellIndex.Heroism: return 1000;
                case MM45InternalSpellIndex.HolyBonus: return 1000;
                case MM45InternalSpellIndex.HolyWord: return 10000;
                case MM45InternalSpellIndex.Hypnotize: return 1500;
                case MM45InternalSpellIndex.IdentifyMonster: return 500;
                case MM45InternalSpellIndex.Implosion: return 10000;
                case MM45InternalSpellIndex.Incinerate: return 3500;
                case MM45InternalSpellIndex.Inferno: return 7500;
                case MM45InternalSpellIndex.InsectSpray: return 500;
                case MM45InternalSpellIndex.ItemToGold: return 2000;
                case MM45InternalSpellIndex.Jump: return 400;
                case MM45InternalSpellIndex.Levitate: return 500;
                case MM45InternalSpellIndex.Light: return 100;
                case MM45InternalSpellIndex.LightningBolt: return 1000;
                case MM45InternalSpellIndex.LloydsBeacon: return 600;
                case MM45InternalSpellIndex.MagicArrow: return 200;
                case MM45InternalSpellIndex.MassDistortion: return 7500;
                case MM45InternalSpellIndex.MegaVolts: return 4000;
                case MM45InternalSpellIndex.MoonRay: return 6000;
                case MM45InternalSpellIndex.NaturesCure: return 600;
                case MM45InternalSpellIndex.Pain: return 400;
                case MM45InternalSpellIndex.PoisonVolley: return 2500;
                case MM45InternalSpellIndex.PowerCure: return 1000;
                case MM45InternalSpellIndex.PowerShield: return 1000;
                case MM45InternalSpellIndex.PrismaticLight: return 6000;
                case MM45InternalSpellIndex.ProtFromElements: return 500;
                case MM45InternalSpellIndex.RaiseDead: return 5000;
                case MM45InternalSpellIndex.RechargeItem: return 1500;
                case MM45InternalSpellIndex.Resurrect: return 12500;
                case MM45InternalSpellIndex.Revitalize: return 200;
                case MM45InternalSpellIndex.Shrapmetal: return 500;
                case MM45InternalSpellIndex.Sleep: return 300;
                case MM45InternalSpellIndex.Sparks: return 500;
                case MM45InternalSpellIndex.StarBurst: return 20000;
                case MM45InternalSpellIndex.StoneToFlesh: return 3500;
                case MM45InternalSpellIndex.SunRay: return 15000;
                case MM45InternalSpellIndex.SuperShelter: return 1500;
                case MM45InternalSpellIndex.SuppressDisease: return 500;
                case MM45InternalSpellIndex.SuppressPoison: return 400;
                case MM45InternalSpellIndex.Teleport: return 1000;
                case MM45InternalSpellIndex.TimeDistortion: return 800;
                case MM45InternalSpellIndex.TownPortal: return 3000;
                case MM45InternalSpellIndex.ToxicCloud: return 400;
                case MM45InternalSpellIndex.TurnUndead: return 500;
                case MM45InternalSpellIndex.WalkOnWater: return 700;
                case MM45InternalSpellIndex.WizardEye: return 500;
                default: return 0;
            }
        }

        public static MM45Guild[] GetSaleLocations(MM45SpellIndex spell)
        {
            return GetSaleLocations(MM45.Spells[spell].InternalIndex);
        }

        public static MM45Guild[] GetSaleLocations(MM45InternalSpellIndex spell)
        {
            const MM45Guild olympus = MM45Guild.Olympus;
            const MM45Guild shangrila = MM45Guild.ShangriLa;
            const MM45Guild vertigo = MM45Guild.Vertigo;
            const MM45Guild castleview = MM45Guild.Castleview;
            const MM45Guild sandcaster = MM45Guild.Sandcaster;
            const MM45Guild nightshadow = MM45Guild.Nightshadow;
            const MM45Guild asp = MM45Guild.Asp;
            const MM45Guild winterkill = MM45Guild.Winterkill;
            const MM45Guild lakeside = MM45Guild.Lakeside;
            const MM45Guild necropolis = MM45Guild.Necropolis;
            const MM45Guild rivercity = MM45Guild.Rivercity;

            switch (spell)
            {
                case MM45InternalSpellIndex.AcidSpray: return new MM45Guild[] { sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.Awaken: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.BeastMaster: return new MM45Guild[] { castleview, shangrila, olympus };
                case MM45InternalSpellIndex.Bless: return new MM45Guild[] { castleview, nightshadow, shangrila, olympus };
                case MM45InternalSpellIndex.Clairvoyance: return new MM45Guild[] { castleview, nightshadow, rivercity, shangrila, olympus };
                case MM45InternalSpellIndex.ColdRay: return new MM45Guild[] { castleview, nightshadow, shangrila, olympus };
                case MM45InternalSpellIndex.CreateFood: return new MM45Guild[] { asp, lakeside, sandcaster, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.CureDisease: return new MM45Guild[] { asp, sandcaster, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.CureParalysis: return new MM45Guild[] { rivercity, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.CurePoison: return new MM45Guild[] { asp, rivercity, sandcaster, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.CureWounds: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.DancingSword: return new MM45Guild[] { asp, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.DayOfProtection: return new MM45Guild[] { asp, lakeside, necropolis, rivercity, sandcaster, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.DayOfSorcery: return new MM45Guild[] { asp, lakeside, necropolis, rivercity, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.DeadlySwarm: return new MM45Guild[] { nightshadow, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.DetectMonster: return new MM45Guild[] { nightshadow, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.DivineIntervention: return new MM45Guild[] { necropolis, shangrila, olympus };
                case MM45InternalSpellIndex.DragonSleep: return new MM45Guild[] { asp, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.ElementalStorm: return new MM45Guild[] { lakeside, necropolis, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.EnchantItem: return new MM45Guild[] { lakeside, necropolis, shangrila, olympus };
                case MM45InternalSpellIndex.EnergyBlast: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.Etherealize: return new MM45Guild[] { asp, lakeside, necropolis, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.FantasticFreeze: return new MM45Guild[] { asp, rivercity, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.FieryFlail: return new MM45Guild[] { lakeside, rivercity, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.FingerOfDeath: return new MM45Guild[] { asp, rivercity, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.FireBall: return new MM45Guild[] { castleview, nightshadow, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.FirstAid: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.FlyingFist: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.FrostBite: return new MM45Guild[] { castleview, rivercity, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.GolemStopper: return new MM45Guild[] { asp, lakeside, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.Heroism: return new MM45Guild[] { castleview, nightshadow, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.HolyBonus: return new MM45Guild[] { castleview, nightshadow, shangrila, olympus };
                case MM45InternalSpellIndex.HolyWord: return new MM45Guild[] { lakeside, necropolis, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.Hypnotize: return new MM45Guild[] { sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.IdentifyMonster: return new MM45Guild[] { castleview, nightshadow, rivercity, shangrila, olympus };
                case MM45InternalSpellIndex.Implosion: return new MM45Guild[] { necropolis, shangrila, olympus };
                case MM45InternalSpellIndex.Incinerate: return new MM45Guild[] { asp, lakeside, necropolis, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.Inferno: return new MM45Guild[] { lakeside, necropolis, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.InsectSpray: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.ItemToGold: return new MM45Guild[] { lakeside, shangrila, olympus };
                case MM45InternalSpellIndex.Jump: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.Levitate: return new MM45Guild[] { castleview, nightshadow, rivercity, shangrila, olympus };
                case MM45InternalSpellIndex.Light: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.LightningBolt: return new MM45Guild[] { castleview, shangrila, olympus };
                case MM45InternalSpellIndex.LloydsBeacon: return new MM45Guild[] { castleview, rivercity, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.MagicArrow: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.MassDistortion: return new MM45Guild[] { lakeside, necropolis, sandcaster, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.MegaVolts: return new MM45Guild[] { lakeside, necropolis, shangrila, olympus };
                case MM45InternalSpellIndex.MoonRay: return new MM45Guild[] { lakeside, necropolis, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.NaturesCure: return new MM45Guild[] { castleview, nightshadow, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.Pain: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.PoisonVolley: return new MM45Guild[] { lakeside, nightshadow, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.PowerCure: return new MM45Guild[] { castleview, rivercity, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.PowerShield: return new MM45Guild[] { castleview, nightshadow, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.PrismaticLight: return new MM45Guild[] { lakeside, necropolis, shangrila, olympus };
                case MM45InternalSpellIndex.ProtFromElements: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.RaiseDead: return new MM45Guild[] { asp, lakeside, necropolis, sandcaster, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.RechargeItem: return new MM45Guild[] { sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.Resurrect: return new MM45Guild[] { asp, lakeside, necropolis, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.Revitalize: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.Shrapmetal: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.Sleep: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.Sparks: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.StarBurst: return new MM45Guild[] { necropolis, shangrila, olympus };
                case MM45InternalSpellIndex.StoneToFlesh: return new MM45Guild[] { asp, lakeside, necropolis, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.SunRay: return new MM45Guild[] { necropolis, shangrila, olympus };
                case MM45InternalSpellIndex.SuperShelter: return new MM45Guild[] { sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.SuppressDisease: return new MM45Guild[] { castleview, nightshadow, shangrila, olympus };
                case MM45InternalSpellIndex.SuppressPoison: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.Teleport: return new MM45Guild[] { sandcaster, winterkill, shangrila, olympus };
                case MM45InternalSpellIndex.TimeDistortion: return new MM45Guild[] { rivercity, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.TownPortal: return new MM45Guild[] { asp, lakeside, necropolis, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.ToxicCloud: return new MM45Guild[] { castleview, vertigo, shangrila, olympus };
                case MM45InternalSpellIndex.TurnUndead: return new MM45Guild[] { castleview, nightshadow, rivercity, shangrila, olympus };
                case MM45InternalSpellIndex.WalkOnWater: return new MM45Guild[] { rivercity, sandcaster, shangrila, olympus };
                case MM45InternalSpellIndex.WizardEye: return new MM45Guild[] { castleview, nightshadow, vertigo, shangrila, olympus };
                default: return new MM45Guild[] { };
            }
        }

        public MM45SpellList()
        {
            m_spells = new Dictionary<MM45SpellIndex,MM45Spell>(117);

            // Cleric spells

            m_spells.Add(MM45SpellIndex.AcidSpray, new MM45Spell(MM45SpellIndex.AcidSpray, MM45InternalSpellIndex.AcidSpray, SpellType.Cleric, 8, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.AllVisibleMonsters));
            m_spells.Add(MM45SpellIndex.Awaken, new MM45Spell(MM45SpellIndex.Awaken, MM45InternalSpellIndex.Awaken, SpellType.Cleric, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.BeastMaster, new MM45Spell(MM45SpellIndex.BeastMaster, MM45InternalSpellIndex.BeastMaster, SpellType.Cleric, 5, false, 2, 1, SpellWhen.CombatAnywhere, SpellTarget.AnimalGroup));
            m_spells.Add(MM45SpellIndex.Bless, new MM45Spell(MM45SpellIndex.Bless, MM45InternalSpellIndex.Bless, SpellType.Cleric, 2, true, 1, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.ColdRay, new MM45Spell(MM45SpellIndex.ColdRay, MM45InternalSpellIndex.ColdRay, SpellType.Cleric, 2, true, 4, 1, SpellWhen.CombatAnywhere, SpellTarget.AllVisibleMonsters));
            m_spells.Add(MM45SpellIndex.CreateFood, new MM45Spell(MM45SpellIndex.CreateFood, MM45InternalSpellIndex.CreateFood, SpellType.Cleric, 20, false, 5, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.CureDisease, new MM45Spell(MM45SpellIndex.CureDisease, MM45InternalSpellIndex.CureDisease, SpellType.Cleric, 10, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.CureParalysis, new MM45Spell(MM45SpellIndex.CureParalysis, MM45InternalSpellIndex.CureParalysis, SpellType.Cleric, 12, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.CurePoison, new MM45Spell(MM45SpellIndex.CurePoison, MM45InternalSpellIndex.CurePoison, SpellType.Cleric, 8, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.CureWounds, new MM45Spell(MM45SpellIndex.CureWounds, MM45InternalSpellIndex.CureWounds, SpellType.Cleric, 3, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.DayOfProtection, new MM45Spell(MM45SpellIndex.DayOfProtection, MM45InternalSpellIndex.DayOfProtection, SpellType.Cleric, 75, false, 10, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.DeadlySwarm, new MM45Spell(MM45SpellIndex.DeadlySwarm, MM45InternalSpellIndex.DeadlySwarm, SpellType.Cleric, 12, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.DivineIntervention, new MM45Spell(MM45SpellIndex.DivineIntervention, MM45InternalSpellIndex.DivineIntervention, SpellType.Cleric, 200, false, 20, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.FieryFlail, new MM45Spell(MM45SpellIndex.FieryFlail, MM45InternalSpellIndex.FieryFlail, SpellType.Cleric, 25, false, 5, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM45SpellIndex.FirstAid, new MM45Spell(MM45SpellIndex.FirstAid, MM45InternalSpellIndex.FirstAid, SpellType.Cleric, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.FlyingFist, new MM45Spell(MM45SpellIndex.FlyingFist, MM45InternalSpellIndex.FlyingFist, SpellType.Cleric, 2, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM45SpellIndex.FrostBite, new MM45Spell(MM45SpellIndex.FrostBite, MM45InternalSpellIndex.FrostBite, SpellType.Cleric, 7, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM45SpellIndex.Heroism, new MM45Spell(MM45SpellIndex.Heroism, MM45InternalSpellIndex.Heroism, SpellType.Cleric, 2, true, 3, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.HolyBonus, new MM45Spell(MM45SpellIndex.HolyBonus, MM45InternalSpellIndex.HolyBonus, SpellType.Cleric, 2, true, 1, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.HolyWord, new MM45Spell(MM45SpellIndex.HolyWord, MM45InternalSpellIndex.HolyWord, SpellType.Cleric, 100, false, 20, 1, SpellWhen.CombatAnywhere, SpellTarget.UndeadMonsterGroup));
            m_spells.Add(MM45SpellIndex.Hypnotize, new MM45Spell(MM45SpellIndex.Hypnotize, MM45InternalSpellIndex.Hypnotize, SpellType.Cleric, 15, false, 4, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.Light, new MM45Spell(MM45SpellIndex.Light, MM45InternalSpellIndex.Light, SpellType.Cleric, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.MassDistortion, new MM45Spell(MM45SpellIndex.MassDistortion, MM45InternalSpellIndex.MassDistortion, SpellType.Cleric, 75, false, 10, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.MoonRay, new MM45Spell(MM45SpellIndex.MoonRay, MM45InternalSpellIndex.MoonRay, SpellType.Cleric, 60, false, 10, 1, SpellWhen.AnywhereAnytime, SpellTarget.PartyAndVisibleMonsters));
            m_spells.Add(MM45SpellIndex.NaturesCure, new MM45Spell(MM45SpellIndex.NaturesCure, MM45InternalSpellIndex.NaturesCure, SpellType.Cleric, 6, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.Pain, new MM45Spell(MM45SpellIndex.Pain, MM45InternalSpellIndex.Pain, SpellType.Cleric, 4, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.PowerCure, new MM45Spell(MM45SpellIndex.PowerCure, MM45InternalSpellIndex.PowerCure, SpellType.Cleric, 2, true, 3, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.ProtFromElements, new MM45Spell(MM45SpellIndex.ProtFromElements, MM45InternalSpellIndex.ProtFromElements, SpellType.Cleric, 1, true, 1, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.RaiseDead, new MM45Spell(MM45SpellIndex.RaiseDead, MM45InternalSpellIndex.RaiseDead, SpellType.Cleric, 50, false, 10, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.Resurrect, new MM45Spell(MM45SpellIndex.Resurrect, MM45InternalSpellIndex.Resurrect, SpellType.Cleric, 125, false, 20, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.Revitalize, new MM45Spell(MM45SpellIndex.Revitalize, MM45InternalSpellIndex.Revitalize, SpellType.Cleric, 2, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.Sparks, new MM45Spell(MM45SpellIndex.Sparks, MM45InternalSpellIndex.Sparks, SpellType.Cleric, 1, true, 1, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.StoneToFlesh, new MM45Spell(MM45SpellIndex.StoneToFlesh, MM45InternalSpellIndex.StoneToFlesh, SpellType.Cleric, 35, false, 5, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.SunRay, new MM45Spell(MM45SpellIndex.SunRay, MM45InternalSpellIndex.SunRay, SpellType.Cleric, 150, false, 20, 1, SpellWhen.CombatAnywhere, SpellTarget.AllVisibleMonsters));
            m_spells.Add(MM45SpellIndex.SuppressDisease, new MM45Spell(MM45SpellIndex.SuppressDisease, MM45InternalSpellIndex.SuppressDisease, SpellType.Cleric, 5, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.SuppressPoison, new MM45Spell(MM45SpellIndex.SuppressPoison, MM45InternalSpellIndex.SuppressPoison, SpellType.Cleric, 4, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.TownPortal, new MM45Spell(MM45SpellIndex.TownPortal, MM45InternalSpellIndex.TownPortal, SpellType.Cleric, 30, false, 5, 1, SpellWhen.NonCombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.TurnUndead, new MM45Spell(MM45SpellIndex.TurnUndead, MM45InternalSpellIndex.TurnUndead, SpellType.Cleric, 5, false, 2, 1, SpellWhen.CombatAnywhere, SpellTarget.UndeadMonsterGroup));
            m_spells.Add(MM45SpellIndex.WalkOnWater, new MM45Spell(MM45SpellIndex.WalkOnWater, MM45InternalSpellIndex.WalkOnWater, SpellType.Cleric, 7, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));

            // Arcane Spells

            m_spells.Add(MM45SpellIndex.AwakenArcane, new MM45Spell(MM45SpellIndex.AwakenArcane, MM45InternalSpellIndex.Awaken, SpellType.Sorcerer, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.Clairvoyance, new MM45Spell(MM45SpellIndex.Clairvoyance, MM45InternalSpellIndex.Clairvoyance, SpellType.Sorcerer, 5, false, 2, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.DancingSword, new MM45Spell(MM45SpellIndex.DancingSword, MM45InternalSpellIndex.DancingSword, SpellType.Sorcerer, 3, true, 10, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.DayOfSorcery, new MM45Spell(MM45SpellIndex.DayOfSorcery, MM45InternalSpellIndex.DayOfSorcery, SpellType.Sorcerer, 40, false, 10, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.DetectMonster, new MM45Spell(MM45SpellIndex.DetectMonster, MM45InternalSpellIndex.DetectMonster, SpellType.Sorcerer, 6, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.DragonSleep, new MM45Spell(MM45SpellIndex.DragonSleep, MM45InternalSpellIndex.DragonSleep, SpellType.Sorcerer, 10, false, 4, 1, SpellWhen.CombatAnywhere, SpellTarget.Dragon));
            m_spells.Add(MM45SpellIndex.ElementalStorm, new MM45Spell(MM45SpellIndex.ElementalStorm, MM45InternalSpellIndex.ElementalStorm, SpellType.Sorcerer, 100, false, 10, 1, SpellWhen.CombatAnywhere, SpellTarget.AllVisibleMonsters));
            m_spells.Add(MM45SpellIndex.EnchantItem, new MM45Spell(MM45SpellIndex.EnchantItem, MM45InternalSpellIndex.EnchantItem, SpellType.Sorcerer, 30, false, 20, 1, SpellWhen.AnywhereAnytime, SpellTarget.Item));
            m_spells.Add(MM45SpellIndex.EnergyBlast, new MM45Spell(MM45SpellIndex.EnergyBlast, MM45InternalSpellIndex.EnergyBlast, SpellType.Sorcerer, 1, true, 1, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM45SpellIndex.Etherealize, new MM45Spell(MM45SpellIndex.Etherealize, MM45InternalSpellIndex.Etherealize, SpellType.Sorcerer, 30, false, 10, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.FantasticFreeze, new MM45Spell(MM45SpellIndex.FantasticFreeze, MM45InternalSpellIndex.FantasticFreeze, SpellType.Sorcerer, 15, false, 5, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.FingerOfDeath, new MM45Spell(MM45SpellIndex.FingerOfDeath, MM45InternalSpellIndex.FingerOfDeath, SpellType.Sorcerer, 10, false, 4, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.Fireball, new MM45Spell(MM45SpellIndex.Fireball, MM45InternalSpellIndex.FireBall, SpellType.Sorcerer, 2, true, 2, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.GolemStopper, new MM45Spell(MM45SpellIndex.GolemStopper, MM45InternalSpellIndex.GolemStopper, SpellType.Sorcerer, 20, false, 10, 1, SpellWhen.CombatAnywhere, SpellTarget.Golem));
            m_spells.Add(MM45SpellIndex.IdentifyMonster, new MM45Spell(MM45SpellIndex.IdentifyMonster, MM45InternalSpellIndex.IdentifyMonster, SpellType.Sorcerer, 5, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.Implosion, new MM45Spell(MM45SpellIndex.Implosion, MM45InternalSpellIndex.Implosion, SpellType.Sorcerer, 100, false, 20, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM45SpellIndex.Incinerate, new MM45Spell(MM45SpellIndex.Incinerate, MM45InternalSpellIndex.Incinerate, SpellType.Sorcerer, 35, false, 10, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM45SpellIndex.Inferno, new MM45Spell(MM45SpellIndex.Inferno, MM45InternalSpellIndex.Inferno, SpellType.Sorcerer, 75, false, 10, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.InsectSpray, new MM45Spell(MM45SpellIndex.InsectSpray, MM45InternalSpellIndex.InsectSpray, SpellType.Sorcerer, 5, false, 1, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.ItemToGold, new MM45Spell(MM45SpellIndex.ItemToGold, MM45InternalSpellIndex.ItemToGold, SpellType.Sorcerer, 20, false, 10, 1, SpellWhen.AnywhereAnytime, SpellTarget.Item));
            m_spells.Add(MM45SpellIndex.Jump, new MM45Spell(MM45SpellIndex.Jump, MM45InternalSpellIndex.Jump, SpellType.Sorcerer, 4, false, 0, 1, SpellWhen.NonCombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.Levitate, new MM45Spell(MM45SpellIndex.Levitate, MM45InternalSpellIndex.Levitate, SpellType.Sorcerer, 5, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.LightArcane, new MM45Spell(MM45SpellIndex.LightArcane, MM45InternalSpellIndex.Light, SpellType.Sorcerer, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.LightningBolt, new MM45Spell(MM45SpellIndex.LightningBolt, MM45InternalSpellIndex.LightningBolt, SpellType.Sorcerer, 2, true, 2, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.LloydsBeacon, new MM45Spell(MM45SpellIndex.LloydsBeacon, MM45InternalSpellIndex.LloydsBeacon, SpellType.Sorcerer, 6, false, 2, 1, SpellWhen.NonCombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.MagicArrow, new MM45Spell(MM45SpellIndex.MagicArrow, MM45InternalSpellIndex.MagicArrow, SpellType.Sorcerer, 2, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM45SpellIndex.MegaVolts, new MM45Spell(MM45SpellIndex.MegaVolts, MM45InternalSpellIndex.MegaVolts, SpellType.Sorcerer, 40, false, 10, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.PoisonVolley, new MM45Spell(MM45SpellIndex.PoisonVolley, MM45InternalSpellIndex.PoisonVolley, SpellType.Sorcerer, 25, false, 10, 1, SpellWhen.CombatAnywhere, SpellTarget.AllVisibleMonsters));
            m_spells.Add(MM45SpellIndex.PowerShield, new MM45Spell(MM45SpellIndex.PowerShield, MM45InternalSpellIndex.PowerShield, SpellType.Sorcerer, 2, true, 2, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.PrismaticLight, new MM45Spell(MM45SpellIndex.PrismaticLight, MM45InternalSpellIndex.PrismaticLight, SpellType.Sorcerer, 60, false, 10, 1, SpellWhen.CombatAnywhere, SpellTarget.AllVisibleMonsters));
            m_spells.Add(MM45SpellIndex.RechargeItem, new MM45Spell(MM45SpellIndex.RechargeItem, MM45InternalSpellIndex.RechargeItem, SpellType.Sorcerer, 15, false, 10, 1, SpellWhen.AnywhereAnytime, SpellTarget.Item));
            m_spells.Add(MM45SpellIndex.Shrapmetal, new MM45Spell(MM45SpellIndex.Shrapmetal, MM45InternalSpellIndex.Shrapmetal, SpellType.Sorcerer, 1, true, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.Sleep, new MM45Spell(MM45SpellIndex.Sleep, MM45InternalSpellIndex.Sleep, SpellType.Sorcerer, 3, false, 1, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.StarBurst, new MM45Spell(MM45SpellIndex.StarBurst, MM45InternalSpellIndex.StarBurst, SpellType.Sorcerer, 200, false, 20, 1, SpellWhen.CombatAnywhere, SpellTarget.AllVisibleMonsters));
            m_spells.Add(MM45SpellIndex.SuperShelter, new MM45Spell(MM45SpellIndex.SuperShelter, MM45InternalSpellIndex.SuperShelter, SpellType.Sorcerer, 15, false, 5, 1, SpellWhen.NonCombatLand, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.Teleport, new MM45Spell(MM45SpellIndex.Teleport, MM45InternalSpellIndex.Teleport, SpellType.Sorcerer, 10, false, 0, 1, SpellWhen.NonCombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.TimeDistortion, new MM45Spell(MM45SpellIndex.TimeDistortion, MM45InternalSpellIndex.TimeDistortion, SpellType.Sorcerer, 8, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.ToxicCloud, new MM45Spell(MM45SpellIndex.ToxicCloud, MM45InternalSpellIndex.ToxicCloud, SpellType.Sorcerer, 4, false, 1, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.WizardEye, new MM45Spell(MM45SpellIndex.WizardEye, MM45InternalSpellIndex.WizardEye, SpellType.Sorcerer, 5, false, 2, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));

            // Druid Spells

            m_spells.Add(MM45SpellIndex.AcidSprayDruid, new MM45Spell(MM45SpellIndex.AcidSprayDruid, MM45InternalSpellIndex.AcidSpray, SpellType.Druid, 8, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.AllVisibleMonsters));
            m_spells.Add(MM45SpellIndex.AwakenDruid, new MM45Spell(MM45SpellIndex.AwakenDruid, MM45InternalSpellIndex.Awaken, SpellType.Druid, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.BeastMasterDruid, new MM45Spell(MM45SpellIndex.BeastMasterDruid, MM45InternalSpellIndex.BeastMaster, SpellType.Druid, 5, false, 2, 1, SpellWhen.CombatAnywhere, SpellTarget.AnimalGroup));
            m_spells.Add(MM45SpellIndex.BlessDruid, new MM45Spell(MM45SpellIndex.BlessDruid, MM45InternalSpellIndex.Bless, SpellType.Druid, 2, true, 1, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.ClairvoyanceDruid, new MM45Spell(MM45SpellIndex.ClairvoyanceDruid, MM45InternalSpellIndex.Clairvoyance, SpellType.Druid, 5, false, 2, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.ColdRayDruid, new MM45Spell(MM45SpellIndex.ColdRayDruid, MM45InternalSpellIndex.ColdRay, SpellType.Druid, 2, true, 4, 1, SpellWhen.CombatAnywhere, SpellTarget.AllVisibleMonsters));
            m_spells.Add(MM45SpellIndex.CureDiseaseDruid, new MM45Spell(MM45SpellIndex.CureDiseaseDruid, MM45InternalSpellIndex.CureDisease, SpellType.Druid, 10, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.CurePoisonDruid, new MM45Spell(MM45SpellIndex.CurePoisonDruid, MM45InternalSpellIndex.CurePoison, SpellType.Druid, 8, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.CureWoundsDruid, new MM45Spell(MM45SpellIndex.CureWoundsDruid, MM45InternalSpellIndex.CureWounds, SpellType.Druid, 3, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.EnergyBlastDruid, new MM45Spell(MM45SpellIndex.EnergyBlastDruid, MM45InternalSpellIndex.EnergyBlast, SpellType.Druid, 1, true, 1, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM45SpellIndex.FireballDruid, new MM45Spell(MM45SpellIndex.FireballDruid, MM45InternalSpellIndex.FireBall, SpellType.Druid, 2, true, 2, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.FirstAidDruid, new MM45Spell(MM45SpellIndex.FirstAidDruid, MM45InternalSpellIndex.FirstAid, SpellType.Druid, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.FlyingFistDruid, new MM45Spell(MM45SpellIndex.FlyingFistDruid, MM45InternalSpellIndex.FlyingFist, SpellType.Druid, 2, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM45SpellIndex.FrostBiteDruid, new MM45Spell(MM45SpellIndex.FrostBiteDruid, MM45InternalSpellIndex.FrostBite, SpellType.Druid, 7, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM45SpellIndex.HeroismDruid, new MM45Spell(MM45SpellIndex.HeroismDruid, MM45InternalSpellIndex.Heroism, SpellType.Druid, 2, true, 3, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.HolyBonusDruid, new MM45Spell(MM45SpellIndex.HolyBonusDruid, MM45InternalSpellIndex.HolyBonus, SpellType.Druid, 2, true, 1, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.IdentifyMonsterDruid, new MM45Spell(MM45SpellIndex.IdentifyMonsterDruid, MM45InternalSpellIndex.IdentifyMonster, SpellType.Druid, 5, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.InsectSprayDruid, new MM45Spell(MM45SpellIndex.InsectSprayDruid, MM45InternalSpellIndex.InsectSpray, SpellType.Druid, 5, false, 1, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.JumpDruid, new MM45Spell(MM45SpellIndex.JumpDruid, MM45InternalSpellIndex.Jump, SpellType.Druid, 4, false, 0, 1, SpellWhen.NonCombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.LevitateDruid, new MM45Spell(MM45SpellIndex.LevitateDruid, MM45InternalSpellIndex.Levitate, SpellType.Druid, 5, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.LightDruid, new MM45Spell(MM45SpellIndex.LightDruid, MM45InternalSpellIndex.Light, SpellType.Druid, 1, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.LightningBoltDruid, new MM45Spell(MM45SpellIndex.LightningBoltDruid, MM45InternalSpellIndex.LightningBolt, SpellType.Druid, 2, true, 2, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.LloydsBeaconDruid, new MM45Spell(MM45SpellIndex.LloydsBeaconDruid, MM45InternalSpellIndex.LloydsBeacon, SpellType.Druid, 6, false, 2, 1, SpellWhen.NonCombatAnywhere, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.MagicArrowDruid, new MM45Spell(MM45SpellIndex.MagicArrowDruid, MM45InternalSpellIndex.MagicArrow, SpellType.Druid, 2, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.Monster));
            m_spells.Add(MM45SpellIndex.NaturesCureDruid, new MM45Spell(MM45SpellIndex.NaturesCureDruid, MM45InternalSpellIndex.NaturesCure, SpellType.Druid, 6, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.PainDruid, new MM45Spell(MM45SpellIndex.PainDruid, MM45InternalSpellIndex.Pain, SpellType.Druid, 4, false, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.PowerCureDruid, new MM45Spell(MM45SpellIndex.PowerCureDruid, MM45InternalSpellIndex.PowerCure, SpellType.Druid, 2, true, 3, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.PowerShieldDruid, new MM45Spell(MM45SpellIndex.PowerShieldDruid, MM45InternalSpellIndex.PowerShield, SpellType.Druid, 2, true, 2, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.ProtFromElementsDruid, new MM45Spell(MM45SpellIndex.ProtFromElementsDruid, MM45InternalSpellIndex.ProtFromElements, SpellType.Druid, 1, true, 1, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.RevitalizeDruid, new MM45Spell(MM45SpellIndex.RevitalizeDruid, MM45InternalSpellIndex.Revitalize, SpellType.Druid, 2, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.ShrapmetalDruid, new MM45Spell(MM45SpellIndex.ShrapmetalDruid, MM45InternalSpellIndex.Shrapmetal, SpellType.Druid, 1, true, 0, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.SleepDruid, new MM45Spell(MM45SpellIndex.SleepDruid, MM45InternalSpellIndex.Sleep, SpellType.Druid, 3, false, 1, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.SparksDruid, new MM45Spell(MM45SpellIndex.SparksDruid, MM45InternalSpellIndex.Sparks, SpellType.Druid, 1, true, 1, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.SuppressDiseaseDruid, new MM45Spell(MM45SpellIndex.SuppressDiseaseDruid, MM45InternalSpellIndex.SuppressDisease, SpellType.Druid, 5, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.SuppressPoisonDruid, new MM45Spell(MM45SpellIndex.SuppressPoisonDruid, MM45InternalSpellIndex.SuppressPoison, SpellType.Druid, 4, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Character));
            m_spells.Add(MM45SpellIndex.ToxicCloudDruid, new MM45Spell(MM45SpellIndex.ToxicCloudDruid, MM45InternalSpellIndex.ToxicCloud, SpellType.Druid, 4, false, 1, 1, SpellWhen.CombatAnywhere, SpellTarget.MonsterGroup));
            m_spells.Add(MM45SpellIndex.TurnUndeadDruid, new MM45Spell(MM45SpellIndex.TurnUndeadDruid, MM45InternalSpellIndex.TurnUndead, SpellType.Druid, 5, false, 2, 1, SpellWhen.CombatAnywhere, SpellTarget.UndeadMonsterGroup));
            m_spells.Add(MM45SpellIndex.WalkOnWaterDruid, new MM45Spell(MM45SpellIndex.WalkOnWaterDruid, MM45InternalSpellIndex.WalkOnWater, SpellType.Druid, 7, false, 0, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
            m_spells.Add(MM45SpellIndex.WizardEyeDruid, new MM45Spell(MM45SpellIndex.WizardEyeDruid, MM45InternalSpellIndex.WizardEye, SpellType.Druid, 5, false, 2, 1, SpellWhen.AnywhereAnytime, SpellTarget.Party));
        }

        public static string LongDescription(MM45InternalSpellIndex index)
        {
            switch (index)
            {
                case MM45InternalSpellIndex.AcidSpray: return "Caster sprays a fine acid mist on all the monsters in front of him, inflicting 15 points of Poison damage on each monster.";
                case MM45InternalSpellIndex.Awaken: return "Pulls all sleeping party members from their slumber, cancelling the SLEEP condition.";
                case MM45InternalSpellIndex.BeastMaster: return "Hypnotizes a group of monsters into stillness until they overcome the spell.";
                case MM45InternalSpellIndex.Bless: return "Improves the armor class of a character by 1 per level of the caster.";
                case MM45InternalSpellIndex.Clairvoyance: return "Cases the two gargoyle heads on the screen to animate and give advice for certain yes/no decisions, usually chests.";
                case MM45InternalSpellIndex.ColdRay: return "A cone of absolute zero springs from the caster's hand momentarily, inflicting 2-4 points of Cold damage per level of the caster on all visible monsters.";
                case MM45InternalSpellIndex.CreateFood: return "Creates one unit of food for each living party member.";
                case MM45InternalSpellIndex.CureDisease: return "Removes the DISEASED condition from a character.";
                case MM45InternalSpellIndex.CureParalysis: return "Removes the PARALYZED condition from a character.";
                case MM45InternalSpellIndex.CurePoison: return "Removes the POISONED condition from a character.";
                case MM45InternalSpellIndex.CureWounds: return "Magically cures one character of 15 points of damage.";
                case MM45InternalSpellIndex.DancingSword: return "The Dancing Sword spell creates hundreds of razor sharp blades that strip the flesh from your foes. Dancing Sword inflicts 6 to 14 points of Physical damage per level of the caster.";
                case MM45InternalSpellIndex.DayOfProtection: return "Simultaneously casts Light, Protection from all elements, Heroism, Holy Bonus, and Bless for the bargain basement price of 75 spell points.";
                case MM45InternalSpellIndex.DayOfSorcery: return "This spell is a super saver plan that simultaneously casts Light, Levitate, Wizard Eye, Clairvoyance, and Power Shield on all party members.";
                case MM45InternalSpellIndex.DeadlySwarm: return "Covers a group of monsters with biting, stinging, burrowing insects, inflicting 25 points of Physical damage to each monster.";
                case MM45InternalSpellIndex.DetectMonster: return "Shows the location of all the monsters near the party.";
                case MM45InternalSpellIndex.DivineIntervention: return "Heals the entire party of all damage short of ERADICATION.";
                case MM45InternalSpellIndex.DragonSleep: return "Puts a dragon to sleep, much the way the Sleep spell puts a human to sleep.";
                case MM45InternalSpellIndex.ElementalStorm: return "Pounds all the monsters in front of the party with a storm of magical energy, inflicting 150 points of a random damage type to each monster.";
                case MM45InternalSpellIndex.EnchantItem: return "Bestows magical power to an item that has none. The more powerful the spell caster, the better the chance for a powerful item.";
                case MM45InternalSpellIndex.EnergyBlast: return "A bolt of pure energy is fired from the caster's clenched fist, inflicting 2 to 6 points of Energy damage per level of the caster.";
                case MM45InternalSpellIndex.Etherealize: return "Moves the party one square forward, regardless of barriers. This spell may succeed in areas where the Teleport spell fails.";
                case MM45InternalSpellIndex.FantasticFreeze: return "Reduces the temperature of the air around a group of monsters to absolute zero for a moment, inflicting 40 points of Cold damage on each monster.";
                case MM45InternalSpellIndex.FieryFlail: return "Caster fires a jet of flame at one monster, inflicting 100 points of Fire damage.";
                case MM45InternalSpellIndex.FingerOfDeath: return "Bloodlessly slays the opponents the caster points to.";
                case MM45InternalSpellIndex.FireBall: return "Sets off a fiery explosion within a group of monsters, inflicting 3 to 7 points of Fire damage per level of the caster.";
                case MM45InternalSpellIndex.FirstAid: return "Magically cures one character of 6 points of damage.";
                case MM45InternalSpellIndex.FlyingFist: return "Deals a light blow to a monster, inflicting 6 points of Physical damage.";
                case MM45InternalSpellIndex.FrostBite: return "Draws the body heat out of a monster, inflicting 35 points of Cold damage.";
                case MM45InternalSpellIndex.GolemStopper: return "Golem Stopper deprives a golem of the magic that animates it, inflicting 100 points of damage.";
                case MM45InternalSpellIndex.Heroism: return "Increases the temporary level of a character by 1 per level of the caster.";
                case MM45InternalSpellIndex.HolyBonus: return "Increases the damage inflicted by a character when fighting by 1 point per level of the caster.";
                case MM45InternalSpellIndex.HolyWord: return "Completely removes the animating magic of the Undead, returning them to the dust from whence they came.";
                case MM45InternalSpellIndex.Hypnotize: return "Like Beast Master, this spell hypnotizes a group of monsters into stillness until they overcome the spell, except that it works on monsters other than animals.";
                case MM45InternalSpellIndex.IdentifyMonster: return "Reveals the condition of the monsters the party is fighting.";
                case MM45InternalSpellIndex.Implosion: return "Implosion concentrates local gravity inside the targeted monster, annihilating all but the most powerful opponents. Implosion inflicts 1000 points of Energy damage.";
                case MM45InternalSpellIndex.Incinerate: return "Shoots a stream of fire at one monster, inflicting 250 points of Fire damage.";
                case MM45InternalSpellIndex.Inferno: return "Engulfs one group of monsters in magical fire, inflicting 250 points of Fire damage on one group.";
                case MM45InternalSpellIndex.InsectSpray: return "Coats a group of monsters with a poison specially designed to kill insects.";
                case MM45InternalSpellIndex.ItemToGold: return "Converts an item into an amount of gold pieces equal to the value of the item (merchant skill not included).";
                case MM45InternalSpellIndex.Jump: return "Puts enough strength into the legs of the party to jump over one square, provided there are no walls of matter or magic. This spell cannot be used in combat.";
                case MM45InternalSpellIndex.Levitate: return "Imparts weightlessness to the party members, preventing them from falling into pit traps, quagmires, holes in clouds, etc.";
                case MM45InternalSpellIndex.Light: return "Fills a dungeon with a steady, soft light until the party rests.";
                case MM45InternalSpellIndex.LightningBolt: return "Lightning flashes from the caster's hand, electrocuting monsters for 4 to 6 points of damage per level of the caster.";
                case MM45InternalSpellIndex.LloydsBeacon: return "This spell allows you to magically return to a place you have already been. Cast this spell once to set the beacon, and again when you wish to return. Each party member may have their own beacon.";
                case MM45InternalSpellIndex.MagicArrow: return "Fires a magical bolt at one opponent, inflicting 8 points of Magical damage.";
                case MM45InternalSpellIndex.MassDistortion: return "Increases the weight of your opponents, effectively removing half their hit points.";
                case MM45InternalSpellIndex.MegaVolts: return "Mega Volts is an improved version of Lightning Bolt, inflicting 150 points of Electrical damage on a group of monsters.";
                case MM45InternalSpellIndex.MoonRay: return "Inflicts 30 points of Energy damage to each monster in sight and cures each party member of 30 points of damage.";
                case MM45InternalSpellIndex.NaturesCure: return "Heals a character of 25 points of damage.";
                case MM45InternalSpellIndex.Pain: return "Stimulates the pain centers of your opponent's brains, inflicting 8 points of Physical damage.";
                case MM45InternalSpellIndex.PoisonVolley: return "Fires 6 poison arrows into each square in front of the party. The arrows do 10 points of Poison damage each.";
                case MM45InternalSpellIndex.PowerCure: return "Heals a character of 2-12 points of damage per level of the caster.";
                case MM45InternalSpellIndex.PowerShield: return "Reduces the damage inflicted on a party member by a number equal to the level of the caster.";
                case MM45InternalSpellIndex.PrismaticLight: return "Mysterious Light springs from the caster's palm, inflicting 80 points of a random damage type depending on which ray hits a monster. The damage type is unpredictable.";
                case MM45InternalSpellIndex.ProtFromElements: return "Reduces the damage the party receives from the elements.  The caster can choose which element this applies to when the spell is cast.";
                case MM45InternalSpellIndex.RaiseDead: return "Removes the DEAD condition from a character.";
                case MM45InternalSpellIndex.RechargeItem: return "Restores 1 to 6 charges to an item that has at least one charge remaining. There is a slight risk the spell will destroy the item.";
                case MM45InternalSpellIndex.Resurrect: return "Removes the ERADICATED condition from a character.";
                case MM45InternalSpellIndex.Revitalize: return "Removes the WEAK condition from a character.";
                case MM45InternalSpellIndex.Shrapmetal: return "Sprays a group of monsters with sharp metal fragments, inflicting 2 points of Physical damage per level of the caster.";
                case MM45InternalSpellIndex.Sleep: return "Puts a group of monsters to sleep until they overcome the spell or are damaged.";
                case MM45InternalSpellIndex.Sparks: return "Envelopes the monsters in an electrically charged gas cloud, inflicting 2 points of Electrical damage per level of the caster.";
                case MM45InternalSpellIndex.StarBurst: return "Includes all monsters in front of the party in a massive explosion, inflicting 500 points of Physical damage on each monster.";
                case MM45InternalSpellIndex.StoneToFlesh: return "Removes the STONED condition from a character.";
                case MM45InternalSpellIndex.SunRay: return "Shines the intensified light of the sun into all monsters in front of the caster, inflicting 200 points of Energy damage on each monster.";
                case MM45InternalSpellIndex.SuperShelter: return "Hides the party from the monsters in unsafe places, permitting them to rest without incident.";
                case MM45InternalSpellIndex.SuppressDisease: return "Slows the effect of disease on a character, but does not remove the DISEASED condition.";
                case MM45InternalSpellIndex.SuppressPoison: return "Slows the effect of poison on a character, but does not remove the POISONED condition.";
                case MM45InternalSpellIndex.Teleport: return "Sends the party up to 9 squares in the direction the party is facing, regardless of obstacles.";
                case MM45InternalSpellIndex.TimeDistortion: return "Warps time, giving the party just enough time to run away from a combat.";
                case MM45InternalSpellIndex.TownPortal: return "Teleports the party to the town of your choice.";
                case MM45InternalSpellIndex.ToxicCloud: return "Surrounds a group of monsters with noxious gasses, inflicting 10 points of Poison damage.";
                case MM45InternalSpellIndex.TurnUndead: return "Weakens the evil magic that animates the Undead, inflicting 25 points of damage.";
                case MM45InternalSpellIndex.WalkOnWater: return "Allows the party to walk over both shallow and deep water.";
                case MM45InternalSpellIndex.WizardEye: return "Wizard Eye gives the party a bird's eye view of their surroundings. The view will appear in the upper right corner of the games screen.";
                default: return String.Format("Unknown Spell #{0}", (int)index);
            }
        }

        public static string ShortDescription(MM45InternalSpellIndex index)
        {
            switch (index)
            {
                case MM45InternalSpellIndex.AcidSpray: return "15 Poison damage to all monsters";
                case MM45InternalSpellIndex.Awaken: return "Remove SLEEP from all characters";
                case MM45InternalSpellIndex.BeastMaster: return "Attempt to paralyze a group of animals";
                case MM45InternalSpellIndex.Bless: return "+Level to Armor Class for party";
                case MM45InternalSpellIndex.Clairvoyance: return "Gives advice on noncombat decisions";
                case MM45InternalSpellIndex.ColdRay: return "(2-4)*Level Cold damage to all monsters";
                case MM45InternalSpellIndex.CreateFood: return "Create enough food for party if you lack it";
                case MM45InternalSpellIndex.CureDisease: return "Remove DISEASED from one character";
                case MM45InternalSpellIndex.CureParalysis: return "Remove PARALYZED from one character";
                case MM45InternalSpellIndex.CurePoison: return "Remove POISONED from one character";
                case MM45InternalSpellIndex.CureWounds: return "+15 hit points to one character";
                case MM45InternalSpellIndex.DancingSword: return "(6-14)*Level Physical damage to a group";
                case MM45InternalSpellIndex.DayOfProtection: return "Bless+Heroism+Holy Bonus+Prot. from Elements";
                case MM45InternalSpellIndex.DayOfSorcery: return "Clairvoy.+Levitate+Power Shield+Wizard Eye";
                case MM45InternalSpellIndex.DeadlySwarm: return "40 Physical damage to one group";
                case MM45InternalSpellIndex.DetectMonster: return "Show number and location of nearby monsters";
                case MM45InternalSpellIndex.DivineIntervention: return "Heal party damage except ERADICATION; +5 Age";
                case MM45InternalSpellIndex.DragonSleep: return "Attempt to paralyze one dragon";
                case MM45InternalSpellIndex.ElementalStorm: return "150 damage of random type to all monsters";
                case MM45InternalSpellIndex.EnchantItem: return "Enchants an item which isn't already magical";
                case MM45InternalSpellIndex.EnergyBlast: return "(2-6)*Level Energy damage to one monster";
                case MM45InternalSpellIndex.Etherealize: return "Move straight through doors, other barriers";
                case MM45InternalSpellIndex.FantasticFreeze: return "40 Cold damage to a group";
                case MM45InternalSpellIndex.FieryFlail: return "100 Fire damage to one monster";
                case MM45InternalSpellIndex.FingerOfDeath: return "Attempt to destroy a group";
                case MM45InternalSpellIndex.FireBall: return "(3-7)*Level Fire damage to a group";
                case MM45InternalSpellIndex.FirstAid: return "+6 hit points to one character";
                case MM45InternalSpellIndex.FlyingFist: return "6 Physical damage to one monster";
                case MM45InternalSpellIndex.FrostBite: return "35 Cold damage to one monster";
                case MM45InternalSpellIndex.GolemStopper: return "100 damage to one golem";
                case MM45InternalSpellIndex.Heroism: return "+Level to hit for party";
                case MM45InternalSpellIndex.HolyBonus: return "+Level to Physical weapon damage for party";
                case MM45InternalSpellIndex.HolyWord: return "Destroy one group of undead";
                case MM45InternalSpellIndex.Hypnotize: return "Attempt to paralyze a group";
                case MM45InternalSpellIndex.IdentifyMonster: return "Show monster group hit points, other stats";
                case MM45InternalSpellIndex.Implosion: return "1000 Energy damage to one monster";
                case MM45InternalSpellIndex.Incinerate: return "250 Fire damage to one monster";
                case MM45InternalSpellIndex.Inferno: return "250 Fire damage to a group";
                case MM45InternalSpellIndex.InsectSpray: return "25 damage to group of insects";
                case MM45InternalSpellIndex.ItemToGold: return "Converts item into half gold value";
                case MM45InternalSpellIndex.Jump: return "Move the party 2 squares; noncombat only";
                case MM45InternalSpellIndex.Levitate: return "Party floats over pits and clouds";
                case MM45InternalSpellIndex.Light: return "Provide light in dungeons and caves";
                case MM45InternalSpellIndex.LightningBolt: return "(4-6)*Level Electrical damage to a group";
                case MM45InternalSpellIndex.LloydsBeacon: return "Set and recall location; noncombat only";
                case MM45InternalSpellIndex.MagicArrow: return "8 Magic damage to one monster";
                case MM45InternalSpellIndex.MassDistortion: return "One group loses half its hit points";
                case MM45InternalSpellIndex.MegaVolts: return "150 Electrical damage to a group";
                case MM45InternalSpellIndex.MoonRay: return "30 Energy damage to all; heals party as well";
                case MM45InternalSpellIndex.NaturesCure: return "+25 hit points to one character";
                case MM45InternalSpellIndex.Pain: return "8 Physical damage to one group";
                case MM45InternalSpellIndex.PoisonVolley: return "Fires arrows that do 10 Poison damage each";
                case MM45InternalSpellIndex.PowerCure: return "+(2-12)*Level hit points to one character";
                case MM45InternalSpellIndex.PowerShield: return "+Level damage reduction for party";
                case MM45InternalSpellIndex.PrismaticLight: return "80 damage of random type to all monsters";
                case MM45InternalSpellIndex.ProtFromElements: return "+2*Level+5 Elem. damage reduction for party";
                case MM45InternalSpellIndex.RaiseDead: return "Remove DEAD from one character";
                case MM45InternalSpellIndex.RechargeItem: return "+1-6 charges to a charged item";
                case MM45InternalSpellIndex.Resurrect: return "Remove ERADICATED from one character; +5 Age";
                case MM45InternalSpellIndex.Revitalize: return "Remove WEAK from one character";
                case MM45InternalSpellIndex.Shrapmetal: return "2*Level Physical damage to a group";
                case MM45InternalSpellIndex.Sleep: return "Attempt to put a group to sleep";
                case MM45InternalSpellIndex.Sparks: return "2*Level Electrical damage to one group";
                case MM45InternalSpellIndex.StarBurst: return "500 Physical damage to all monsters";
                case MM45InternalSpellIndex.StoneToFlesh: return "Remove STONE from one character";
                case MM45InternalSpellIndex.SunRay: return "200 Energy damage to all monsters";
                case MM45InternalSpellIndex.SuperShelter: return "Allow safe rest; noncombat and on land only";
                case MM45InternalSpellIndex.SuppressDisease: return "Reduce DISEASED on one character to 1";
                case MM45InternalSpellIndex.SuppressPoison: return "Reduce POISONED on one character to 1";
                case MM45InternalSpellIndex.Teleport: return "Teleport up to 9 squares; noncombat only";
                case MM45InternalSpellIndex.TimeDistortion: return "Cause the entire party to flee combat";
                case MM45InternalSpellIndex.TownPortal: return "Teleport to a town; noncombat only";
                case MM45InternalSpellIndex.ToxicCloud: return "10 Poison damage to a group";
                case MM45InternalSpellIndex.TurnUndead: return "25 damage to a group of undead";
                case MM45InternalSpellIndex.WalkOnWater: return "Allow the party to travel on deep water";
                case MM45InternalSpellIndex.WizardEye: return "Show an overhead view of the vicinity";
                default: return String.Format("Unknown Spell #{0}", (int)index);
            }
        }

        public static string WhereLearnedNonGuild(MM45InternalSpellIndex index)
        {
            switch (index)
            {
                case MM45InternalSpellIndex.AcidSpray: return "Ancient Temple of Yak (4,5) [book teaches spell for free]";
                case MM45InternalSpellIndex.BeastMaster: return "Witch Tower Level 3 (4,6) [6 gems]";
                case MM45InternalSpellIndex.Clairvoyance: return "Witch Tower Level 3 (5,5) [11 gems]";
                case MM45InternalSpellIndex.CureDisease: return "F3 (12,2) [taught by the statue for free after you finish the quest of Orothin at F3 (9,6)]";
                case MM45InternalSpellIndex.CurePoison: return "F3 (12,8) [taught by the statue for free after you finish the quest of Orothin at F3 (9,6)]";
                case MM45InternalSpellIndex.DivineIntervention: return "Northern Sphinx Head (2,8) [book teaches spell for free]";
                case MM45InternalSpellIndex.EnchantItem: return "Rivercity (25,20) [taught by Barok for free once after returning Barok's Magic Pendant; afterwards for 1000 gold]";
                case MM45InternalSpellIndex.Hypnotize: return "Ancient Temple of Yak (30,3) [book teaches spell for free]";
                case MM45InternalSpellIndex.ItemToGold: return "Northern Sphinx Dungeon (14,1) [book teaches spell for free]";
                case MM45InternalSpellIndex.Light: return "Ancient Temple of Yak (6,11) [book teaches spell for free]";
                case MM45InternalSpellIndex.LightningBolt: return "Witch Tower Level 3 (10,6) [11 gems]";
                case MM45InternalSpellIndex.MegaVolts: return "C2 (8,11) [taught by Falagar for free once after returning Crystals of Piezoelectricity; afterwards for 2000 gold]";
                case MM45InternalSpellIndex.MoonRay: return "C2 (10,6) [taught by Carlawna for free once after returning Scarab of Imaging; afterwards for 2000 gold]";
                case MM45InternalSpellIndex.Pain: return "Witch Tower Level 3 (4,10) [6 gems]";
                case MM45InternalSpellIndex.PrismaticLight: return "Tower of High Magic Level 4 (6,6) [book teaches spell for free]";
                case MM45InternalSpellIndex.RechargeItem: return "D3 (12,8) [taught by Ligono for free after returning Ligono's Missing Skull; one time only]";
                case MM45InternalSpellIndex.Sleep: return "Witch Tower Level 3 (5,11) [6 gems]";
                case MM45InternalSpellIndex.Sparks: return "Ancient Temple of Yak (0,5) [book teaches spell for free]";
                case MM45InternalSpellIndex.StarBurst: return "Northern Sphinx Head (12,8) [book teaches spell for free]";
                case MM45InternalSpellIndex.SuperShelter: return "B3 (6,3) [taught by Thickbark for free after destroying Troll lair; one time only]";
                case MM45InternalSpellIndex.Teleport: return "Northern Sphinx Dungeon (0,15), book teaches spell for free";
                case MM45InternalSpellIndex.ToxicCloud: return "Witch Tower Level 3 (9,5), [11 gems]";
                default: return String.Empty;
            }
        }

        public static string WhereLearned(MM45InternalSpellIndex index)
        {
            string strNonGuild = WhereLearnedNonGuild(index);
            StringBuilder sb = new StringBuilder();
            foreach (MM45Guild guild in GetSaleLocations(index))
            {
                MapXY map = MM45QuestInfo.GuildLocation(guild);
                sb.AppendFormat("{0} ({1},{2}), ", MM45QuestInfo.GuildMap(guild), map.X, map.Y);
            }
            Global.Trim(sb);

            if (String.IsNullOrWhiteSpace(strNonGuild))
                return sb.ToString();

            return strNonGuild + ", " + sb.ToString();
        }
    }

    public class MM45Spell : MM345Spell
    {
        public MM45SpellIndex Index
        {
            get { return (MM45SpellIndex)Index345; }
            set { Index345 = (int)value; }
        }

        public MM45InternalSpellIndex InternalIndex
        {
            get { return (MM45InternalSpellIndex)InternalIndex345; }
            set { InternalIndex345 = (int)value; }
        }

        public override int BasicIndex { get { return (int)InternalIndex345; } }

        public MM45Spell(MM45SpellIndex index, MM45InternalSpellIndex iInternal, string name, SpellType type, int sp, bool perlevel,
            int gems, int level, SpellWhen when, SpellTarget target, string shortDesc, string desc, string location)
        {
            SetInfo((int) index, (int) iInternal, name, type, sp, perlevel, gems, level, when, target, shortDesc, desc, location);
        }

        public MM45Spell(MM45SpellIndex index, MM45InternalSpellIndex iInternal, SpellType type, int sp, bool perlevel,
            int gems, int level, SpellWhen when, SpellTarget target)
        {
            SetInfo((int)index, (int)iInternal, MM45SpellList.GetSpellName(iInternal), type, sp, perlevel, gems, level, when, target,
                MM45SpellList.ShortDescription(iInternal), MM45SpellList.LongDescription(iInternal), MM45SpellList.WhereLearned(iInternal));
        }

        public static MM45Spell MM45None { get { return new MM45Spell(MM45SpellIndex.None, MM45InternalSpellIndex.None, "None", SpellType.Unknown, 0, false, 0, 0, SpellWhen.None, SpellTarget.Unknown, "", "", ""); } }

        public override MMSpell None { get { return MM45None; } }
        public override bool UsesLevelOnly { get { return true; } }

        public override Keys[] GetKeys(BaseCharacter character)
        {
            MM45Character mm4Char = character as MM45Character;
            if (mm4Char == null)
                return null;

            if (!mm4Char.Spells.IsKnown((int)Index, Type))
                return null;

            // The keys to send depends on which spells the character knows, because they are all shown in a list of 10 each
            // So send a PageDown for each 10 spells the character knows before the desired one
            // and then down arrows for the remainder
            MM45SpellIndex firstSpell = MM45SpellIndex.None;
            MM45SpellIndex lastSpell = MM45SpellIndex.None;
            SpellType spellType = SpellType.Cleric;
            switch (mm4Char.Class)
            {
                case MM345Class.Cleric:
                case MM345Class.Paladin:
                    firstSpell = MM45SpellIndex.FirstCleric;
                    lastSpell = MM45SpellIndex.LastCleric;
                    spellType = SpellType.Cleric;
                    break;
                case MM345Class.Sorcerer:
                case MM345Class.Archer:
                    firstSpell = MM45SpellIndex.FirstArcane;
                    lastSpell = MM45SpellIndex.LastArcane;
                    spellType = SpellType.Sorcerer;
                    break;
                case MM345Class.Druid:
                case MM345Class.Ranger:
                    firstSpell = MM45SpellIndex.FirstDruid;
                    lastSpell = MM45SpellIndex.LastDruid;
                    spellType = SpellType.Druid;
                    break;
                default:
                    return null;    // Not a caster
            }
            MM45SpellIndex spell = firstSpell;

            int iDown = 0;
            while (spell != Index)
            {
                if (spell > lastSpell)
                    break;
                if (mm4Char.Spells.IsKnown((int)spell, spellType))
                    iDown++;
                spell++;
            }

            return KeysForKnownSpell(mm4Char.Spells.NumKnown, iDown);
        }
    }
}
