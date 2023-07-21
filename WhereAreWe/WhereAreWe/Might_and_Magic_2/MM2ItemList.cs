using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum MM2ItemIndex
    {
        Blank = 0,
        SmallClub = 1,
        SmallKnife = 2,
        LargeClub = 3,
        Dagger = 4,
        LargeKnife = 5,
        HandAxe = 6,
        Cudgel = 7,
        SpikedClub = 8,
        BullWhip = 9,
        LongDagger = 10,
        Maul = 11,
        ShortSword = 12,
        Nunchakas = 13,
        Mace = 14,
        Spear = 15,
        Cutlass = 16,
        Flail = 17,
        Sabre = 18,
        LongSword = 19,
        Wakizashi = 20,
        Scimitar = 21,
        BattleAxe = 22,
        BroadSword = 23,
        Katana = 24,
        SlumberClub = 25,
        PowerClub = 26,
        LuckyKnife = 27,
        LooterKnife = 28,
        PowerCudgel = 29,
        EnergyWhip = 30,
        SonicWhip = 31,
        MightyWhip = 32,
        ScorchMaul = 33,
        MaulerMace = 34,
        ExactoSpear = 35,
        FierySpear = 36,
        FastCutlass = 37,
        QuickFlail = 38,
        ShockFlail = 39,
        SharpSabre = 40,
        EgoScimitar = 41,
        TrueAxe = 42,
        BlazingAxe = 43,
        ElectricAxe = 44,
        RapidKatana = 45,
        AccurateSword = 46,
        ChanceSword = 47,
        SpeedySword = 48,
        FlashSword = 49,
        FlamingSword = 50,
        ElectricSword = 51,
        AcidicSword = 52,
        ColdBlade = 53,
        SageDagger = 54,
        HolyCudgel = 55,
        DivineMace = 56,
        IceScimitar = 57,
        GrandAxe = 58,
        SwiftAxe = 59,
        DynoKatana = 60,
        ForceSword = 61,
        MagicSword = 62,
        ThunderSword = 63,
        EnergyBlade = 64,
        PhotonBlade = 65,
        Staff = 66,
        Sickle = 67,
        Scythe = 68,
        Glaive = 69,
        WarHammer = 70,
        Trident = 71,
        Pike = 72,
        Naginata = 73,
        Bardiche = 74,
        GreatHammer = 75,
        Halberd = 76,
        GreatAxe = 77,
        Flamberge = 78,
        WindStaff = 79,
        TriSickle = 80,
        IceSickle = 81,
        FireGlaive = 82,
        HarshHammer = 83,
        StoneHammer = 84,
        GeniusStaff = 85,
        WizardStaff = 86,
        SoulScythe = 87,
        DarkTrident = 88,
        TitansPike = 89,
        MoonHalberd = 90,
        SunNaginata = 91,
        Blowpipe = 92,
        Sling = 93,
        ShortBow = 94,
        Crossbow = 95,
        LongBow = 96,
        GreatBow = 97,
        ShamanPipe = 98,
        CinderPipe = 99,
        QuietSling = 100,
        PiratesXBow = 101,
        BurningXBow = 102,
        FireballBow = 103,
        VoltageBow = 104,
        GiantSling = 105,
        EnergySling = 106,
        DeathBow = 107,
        StarBow = 108,
        MeteorBow = 109,
        AncientBow = 110,
        GreenKey = 111,
        YellowKey = 112,
        RedKey = 113,
        BlackKey = 114,
        SmallShield = 115,
        LargeShield = 116,
        GreatShield = 117,
        FireShield = 118,
        ElectricShield = 119,
        AcidShield = 120,
        ColdShield = 121,
        SilverShield = 122,
        BronzeShield = 123,
        IronShield = 124,
        MagicShield = 125,
        GoldShield = 126,
        PaddedArmor = 127,
        LeatherSuit = 128,
        ScaleArmor = 129,
        RingMail = 130,
        ChainMail = 131,
        SplintMail = 132,
        PlateMail = 133,
        PlateArmor = 134,
        IronScaleMail = 135,
        BronzeScaleMail = 136,
        SilverScaleMail = 137,
        IronRingMail = 138,
        BronzeRingMail = 139,
        SilverRingMail = 140,
        IronChainMail = 141,
        BronzeChainMail = 142,
        SilverChainMail = 143,
        IronSplintMail = 144,
        BronzeSplintMail = 145,
        SilverSplintMail = 146,
        IronPlateMail = 147,
        BronzePlateMail = 148,
        SilverPlateMail = 149,
        GoldScaleMail = 150,
        GoldRingMail = 151,
        GoldChainMail = 152,
        GoldSplintMail = 153,
        GoldPlateMail = 154,
        Helm = 155,
        IronHelm = 156,
        BronzeHelm = 157,
        SilverHelm = 158,
        GoldHelm = 159,
        MagicHerbs = 160,
        Torch = 161,
        Lantern = 162,
        ThiefsPick = 163,
        RopeandHooks = 164,
        WakeupHorn = 165,
        Compass = 166,
        Sextant = 167,
        ForcePotion = 168,
        SkillPotion = 169,
        MaxHPPotion = 170,
        HolyCharm = 171,
        HerbalPatch = 172,
        HeroMedal = 173,
        SilentHorn = 174,
        MagicMeal = 175,
        AntidoteAle = 176,
        SuperFlare = 177,
        DovesBlood = 178,
        RayGun = 179,
        MagicCharm = 180,
        WitchBroom = 181,
        Invisocloak = 182,
        StormWand = 183,
        LavaGrenade = 184,
        Hourglass = 185,
        InstantKeep = 186,
        TeleportOrb = 187,
        SkeletonKey = 188,
        DefenseRing = 189,
        MightGauntlet = 190,
        AccuracyGauntlet = 191,
        StealthCape = 192,
        Admit8Pass = 193,
        SpeedBoots = 194,
        CureallWand = 195,
        MoonRock = 196,
        RubyAnkh = 197,
        Disruptor = 198,
        LichHand = 199,
        Phaser = 200,
        FreezeWand = 201,
        Energizer = 202,
        MagicMirror = 203,
        ElvenCloak = 204,
        ElvenBoots = 205,
        SageRobe = 206,
        EnchantedId = 207,
        GreenTicket = 208,
        YellowTicket = 209,
        RedTicket = 210,
        BlackTicket = 211,
        FeFarthing = 212,
        CastleKey = 213,
        MarksKeys = 214,
        DogWhistle = 215,
        WebCaster = 216,
        MonsterTome = 217,
        CupieDoll = 218,
        WaterTalon = 219,
        AirTalon = 220,
        FireTalon = 221,
        EarthTalon = 222,
        ElementOrb = 223,
        GoldGoblet = 224,
        Loincloth = 225,
        ValorSword = 226,
        HonorSword = 227,
        NobleSword = 228,
        CoraksSoul = 229,
        EmeraldRing = 230,
        WaterDisc = 231,
        AirDisc = 232,
        FireDisc = 233,
        EarthDisc = 234,
        SapphirePin = 235,
        AmethystBox = 236,
        CoralBroach = 237,
        LapisScarab = 238,
        AmberSkull = 239,
        QuartzSkull = 240,
        AgateGrail = 241,
        OpalPendant = 242,
        CrystalVial = 243,
        RubyAmulet = 244,
        IvoryCameo = 245,
        RubyTiara = 246,
        OnyxEffigy = 247,
        PearlChoker = 248,
        TopazShard = 249,
        SunCrown = 250,
        J26Fluxer = 251,
        M27Radicon = 252,
        A1Todilor = 253,
        N19Capitor = 254,
        UselessItem = 255
    }

    public class MM2ItemList
    {
        private bool m_bValid = false;
        private string m_strError = string.Empty;
        private List<MM2Item> m_items = new List<MM2Item>();

        public List<MM2Item> Items
        {
            get { return m_items; }
        }

        public MM2ItemList(string strFile)
        {
            m_bValid = false;
            BinaryReader reader = null;
            int iIndex = 0;

            try
            {
                reader = new BinaryReader(File.OpenRead(strFile));
                while (reader.BaseStream.Position <= reader.BaseStream.Length - 20)
                {
                    byte[] bytes = reader.ReadBytes(20);
                    m_items.Add(MM2Item.Create(bytes, iIndex++));
                }
                m_bValid = true;
            }
            catch (Exception ex)
            {
                m_strError = ex.Message;
            }
        }

        public bool Valid
        {
            get { return m_bValid; }
        }

        public string LastError
        {
            get { return m_strError; }
        }

        public string Dump()
        {
            StringBuilder sb = new StringBuilder();
            foreach (MM2Item item in m_items)
            {
                sb.Append(item.Dump());
            }
            return sb.ToString();
        }

        public MM2Item GetItem(byte index, int memory = -1)
        {
            MM2Item item = null;
            if (m_items.Count > index)
                item = m_items[index].Clone() as MM2Item;
            else
                item = MM2Item.Create(0, "<empty>", 255, 0, 0, 0, 0);
            item.MemoryIndex = memory;
            return item;
        }

        public MM2ItemList()
        {
            // Load the standard MM2 item list
            m_items = new List<MM2Item>(256);
            m_items.Add(MM2Item.Create(MM2ItemIndex.Blank, "BLANK", 255, 240, 0, 0, 0));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SmallClub, "Small Club", 255, 0, 0, 2, 1));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SmallKnife, "Small Knife", 239, 0, 0, 3, 5));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LargeClub, "Large Club", 255, 0, 0, 4, 4));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Dagger, "Dagger", 239, 0, 0, 4, 8));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LargeKnife, "Large Knife", 239, 0, 0, 5, 10));
            m_items.Add(MM2Item.Create(MM2ItemIndex.HandAxe, "Hand Axe", 231, 0, 0, 5, 10));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Cudgel, "Cudgel", 245, 0, 0, 5, 15));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SpikedClub, "Spiked Club", 239, 0, 0, 6, 15));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BullWhip, "Bull Whip", 159, 0, 0, 6, 25));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LongDagger, "Long Dagger", 239, 0, 0, 6, 20));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Maul, "Maul", 245, 0, 0, 6, 30));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ShortSword, "Short Sword", 230, 0, 0, 6, 15));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Nunchakas, "Nunchakas", 130, 0, 0, 6, 30));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Mace, "Mace", 245, 0, 0, 7, 50));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Spear, "Spear", 231, 0, 0, 7, 15));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Cutlass, "Cutlass", 228, 0, 0, 7, 40));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Flail, "Flail", 244, 0, 0, 8, 100));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Sabre, "Sabre", 228, 0, 0, 8, 60));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LongSword, "Long Sword", 228, 0, 0, 8, 50));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Wakizashi, "Wakizashi", 130, 0, 0, 8, 60));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Scimitar, "Scimitar", 228, 0, 0, 9, 80));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BattleAxe, "Battle Axe", 229, 0, 0, 10, 60));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BroadSword, "Broad Sword", 228, 0, 0, 10, 100));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Katana, "Katana", 130, 0, 0, 10, 150));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SlumberClub, "Slumber Club", 255, 191, 135, 4, 100));
            m_items.Add(MM2Item.Create(MM2ItemIndex.PowerClub, "Power Club", 255, 3, 0, 6, 200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LuckyKnife, "Lucky Knife", 239, 90, 0, 5, 250));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LooterKnife, "Looter Knife", 239, 239, 0, 6, 400));
            m_items.Add(MM2Item.Create(MM2ItemIndex.PowerCudgel, "Power Cudgel", 245, 3, 0, 5, 300));
            m_items.Add(MM2Item.Create(MM2ItemIndex.EnergyWhip, "Energy Whip", 159, 175, 131, 6, 500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SonicWhip, "Sonic Whip", 159, 207, 187, 6, 500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MightyWhip, "Mighty Whip", 159, 3, 0, 6, 400));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ScorchMaul, "Scorch Maul", 245, 127, 0, 6, 400));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MaulerMace, "Mauler Mace", 245, 6, 0, 7, 600));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ExactoSpear, "Exacto Spear", 231, 70, 0, 7, 800));
            m_items.Add(MM2Item.Create(MM2ItemIndex.FierySpear, "Fiery Spear", 231, 127, 151, 7, 1200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.FastCutlass, "Fast Cutlass", 228, 52, 0, 7, 1000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.QuickFlail, "Quick Flail", 244, 53, 0, 8, 1200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ShockFlail, "Shock Flail", 244, 143, 137, 8, 1200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SharpSabre, "Sharp Sabre", 228, 69, 0, 8, 1500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.EgoScimitar, "Ego Scimitar", 228, 44, 0, 9, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.TrueAxe, "True Axe", 229, 69, 0, 10, 1800));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BlazingAxe, "Blazing Axe", 229, 127, 0, 10, 1500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ElectricAxe, "Electric Axe", 229, 143, 146, 10, 2500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.RapidKatana, "Rapid Katana", 130, 54, 0, 10, 3000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.AccurateSword, "Accurate Sword", 228, 74, 0, 10, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ChanceSword, "Chance Sword", 228, 95, 0, 10, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SpeedySword, "Speedy Sword", 228, 58, 0, 10, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.FlashSword, "Flash Sword", 228, 175, 146, 10, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.FlamingSword, "Flaming Sword", 228, 127, 151, 10, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ElectricSword, "Electric Sword", 228, 143, 164, 10, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.AcidicSword, "Acidic Sword", 228, 223, 143, 10, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ColdBlade, "Cold Blade", 228, 159, 149, 10, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SageDagger, "Sage Dagger", 40, 31, 95, 8, 20000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.HolyCudgel, "Holy Cudgel", 80, 47, 222, 10, 20000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.DivineMace, "Divine Mace", 245, 250, 221, 14, 30000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.IceScimitar, "Ice Scimitar", 228, 159, 162, 18, 20000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GrandAxe, "Grand Axe", 229, 15, 31, 20, 20000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SwiftAxe, "Swift Axe", 229, 63, 47, 20, 20000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.DynoKatana, "Dyno Katana", 130, 143, 95, 20, 20000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ForceSword, "Force Sword", 228, 15, 31, 20, 30000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MagicSword, "Magic Sword", 228, 111, 95, 20, 30000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ThunderSword, "Thunder Sword", 228, 15, 146, 20, 30000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.EnergyBlade, "Energy Blade", 228, 175, 160, 20, 30000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.PhotonBlade, "Photon Blade", 128, 15, 173, 25, 50000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Staff, "Staff", 251, 0, 0, 8, 40));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Sickle, "Sickle", 225, 0, 0, 8, 30));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Scythe, "Scythe", 225, 0, 0, 9, 50));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Glaive, "Glaive", 225, 0, 0, 10, 80));
            m_items.Add(MM2Item.Create(MM2ItemIndex.WarHammer, "War Hammer", 241, 0, 0, 10, 120));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Trident, "Trident", 225, 0, 0, 11, 100));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Pike, "Pike", 225, 0, 0, 12, 150));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Naginata, "Naginata", 130, 0, 0, 12, 300));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Bardiche, "Bardiche", 225, 0, 0, 13, 200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GreatHammer, "Great Hammer", 241, 0, 0, 14, 300));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Halberd, "Halberd", 225, 0, 0, 14, 250));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GreatAxe, "Great Axe", 225, 0, 0, 15, 300));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Flamberge, "Flamberge", 224, 0, 0, 16, 400));
            m_items.Add(MM2Item.Create(MM2ItemIndex.WindStaff, "Wind Staff", 251, 53, 203, 8, 1500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.TriSickle, "Tri-Sickle", 225, 0, 0, 24, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.IceSickle, "Ice Sickle", 225, 159, 149, 16, 3000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.FireGlaive, "Fire Glaive", 225, 127, 151, 10, 3000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.HarshHammer, "Harsh Hammer", 241, 3, 0, 15, 1500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.StoneHammer, "Stone Hammer", 241, 111, 0, 18, 3000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GeniusStaff, "Genius Staff", 250, 26, 95, 16, 30000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.WizardStaff, "Wizard Staff", 8, 31, 168, 16, 30000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SoulScythe, "Soul Scythe", 225, 111, 156, 18, 40000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.DarkTrident, "Dark Trident", 225, 245, 0, 30, 50000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.TitansPike, "Titan's Pike", 225, 15, 31, 40, 50000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MoonHalberd, "Moon Halberd", 225, 95, 215, 30, 50000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SunNaginata, "Sun Naginata", 130, 250, 95, 25, 40000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Blowpipe, "Blowpipe", 239, 0, 0, 4, 10));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Sling, "Sling", 231, 0, 0, 5, 15));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ShortBow, "Short Bow", 226, 0, 0, 6, 25));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Crossbow, "Crossbow", 230, 0, 0, 8, 50));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LongBow, "Long Bow", 226, 0, 0, 10, 100));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GreatBow, "Great Bow", 224, 0, 0, 12, 200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ShamanPipe, "Shaman Pipe", 239, 106, 97, 4, 1500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.CinderPipe, "Cinder Pipe", 239, 122, 151, 4, 2500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.QuietSling, "Quiet Sling", 231, 191, 189, 5, 1500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.PiratesXBow, "Pirates X-Bow", 230, 234, 63, 8, 3000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BurningXBow, "Burning X-Bow", 230, 122, 195, 8, 2500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.FireballBow, "Fireball Bow", 226, 127, 151, 10, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.VoltageBow, "Voltage Bow", 226, 138, 146, 10, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GiantSling, "Giant Sling", 231, 207, 31, 15, 20000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.EnergySling, "Energy Sling", 231, 175, 131, 10, 15000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.DeathBow, "Death Bow", 224, 95, 95, 24, 40000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.StarBow, "Star Bow", 224, 175, 175, 24, 34464));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MeteorBow, "Meteor Bow", 224, 245, 171, 24, 34464));
            m_items.Add(MM2Item.Create(MM2ItemIndex.AncientBow, "Ancient Bow", 224, 79, 63, 35, 3392));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GreenKey, "Green Key", 255, 240, 0, 0, 100));
            m_items.Add(MM2Item.Create(MM2ItemIndex.YellowKey, "Yellow Key", 255, 240, 0, 0, 200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.RedKey, "Red Key", 255, 240, 0, 0, 500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BlackKey, "Black Key", 255, 240, 0, 0, 1000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SmallShield, "Small Shield", 213, 0, 0, 1, 15));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LargeShield, "Large Shield", 213, 0, 0, 2, 60));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GreatShield, "Great Shield", 213, 0, 0, 3, 150));
            m_items.Add(MM2Item.Create(MM2ItemIndex.FireShield, "Fire Shield", 213, 127, 0, 3, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ElectricShield, "Electric Shield", 213, 143, 0, 3, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.AcidShield, "Acid Shield", 213, 223, 0, 3, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ColdShield, "Cold Shield", 213, 159, 0, 3, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SilverShield, "Silver Shield", 213, 175, 0, 3, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BronzeShield, "Bronze Shield", 213, 207, 0, 3, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.IronShield, "Iron Shield", 213, 191, 0, 3, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MagicShield, "Magic Shield", 213, 111, 0, 5, 5000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GoldShield, "Gold Shield", 213, 95, 0, 7, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.PaddedArmor, "Padded Armor", 255, 0, 0, 2, 20));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LeatherSuit, "Leather Suit", 247, 0, 0, 3, 40));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ScaleArmor, "Scale Armor", 247, 0, 0, 4, 100));
            m_items.Add(MM2Item.Create(MM2ItemIndex.RingMail, "Ring Mail", 246, 0, 0, 5, 200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ChainMail, "Chain Mail", 244, 0, 0, 6, 400));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SplintMail, "Splint Mail", 208, 0, 0, 7, 600));
            m_items.Add(MM2Item.Create(MM2ItemIndex.PlateMail, "Plate Mail", 192, 0, 0, 8, 1000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.PlateArmor, "Plate Armor", 192, 0, 0, 10, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.IronScaleMail, "Iron Scale Mail", 247, 191, 0, 4, 3000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BronzeScaleMail, "Bronze Scale Mail", 247, 207, 0, 4, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SilverScaleMail, "Silver Scale Mail", 247, 175, 0, 4, 5000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.IronRingMail, "Iron Ring Mail", 246, 191, 0, 5, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BronzeRingMail, "Bronze Ring Mail", 246, 207, 0, 5, 5000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SilverRingMail, "Silver Ring Mail", 246, 175, 0, 5, 6000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.IronChainMail, "Iron Chain Mail", 244, 191, 0, 6, 6000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BronzeChainMail, "Bronze Chain Mail", 244, 207, 0, 6, 7000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SilverChainMail, "Silver Chain Mail", 244, 175, 0, 6, 8000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.IronSplintMail, "Iron Splint Mail", 208, 191, 0, 7, 8000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BronzeSplintMail, "Bronze Splint Mail", 208, 207, 0, 7, 9000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SilverSplintMail, "Silver Splint Mail", 208, 175, 0, 7, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.IronPlateMail, "Iron Plate Mail", 192, 191, 0, 8, 12000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BronzePlateMail, "Bronze Plate Mail", 192, 207, 0, 8, 13000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SilverPlateMail, "Silver Plate Mail", 192, 175, 0, 8, 14000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GoldScaleMail, "Gold Scale Mail", 247, 95, 90, 6, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GoldRingMail, "Gold Ring Mail", 246, 95, 90, 7, 20000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GoldChainMail, "Gold Chain Mail", 244, 95, 90, 8, 40000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GoldSplintMail, "Gold Splint Mail", 208, 95, 92, 9, 60000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GoldPlateMail, "Gold Plate Mail", 192, 95, 95, 12, 3392));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Helm, "Helm", 209, 0, 0, 2, 30));
            m_items.Add(MM2Item.Create(MM2ItemIndex.IronHelm, "Iron Helm", 209, 191, 0, 2, 1000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BronzeHelm, "Bronze Helm", 209, 207, 0, 2, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SilverHelm, "Silver Helm", 209, 175, 0, 3, 5000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GoldHelm, "Gold Helm", 209, 95, 85, 4, 20000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MagicHerbs, "Magic Herbs", 255, 240, 180, 0, 50));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Torch, "Torch", 255, 240, 133, 0, 1));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Lantern, "Lantern", 255, 240, 133, 0, 20));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ThiefsPick, "Thief's Pick", 6, 239, 0, 0, 200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.RopeandHooks, "Rope and Hooks", 255, 240, 139, 0, 10));
            m_items.Add(MM2Item.Create(MM2ItemIndex.WakeupHorn, "Wakeup Horn", 255, 240, 129, 0, 50));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Compass, "Compass", 255, 240, 134, 0, 200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Sextant, "Sextant", 255, 240, 134, 0, 500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ForcePotion, "Force Potion", 255, 240, 26, 0, 100));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SkillPotion, "Skill Potion", 255, 240, 85, 0, 500));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MaxHPPotion, "MaxHP Potion", 255, 240, 2, 0, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.HolyCharm, "Holy Charm", 255, 240, 183, 0, 200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.HerbalPatch, "Herbal Patch", 255, 240, 184, 0, 400));
            m_items.Add(MM2Item.Create(MM2ItemIndex.HeroMedal, "Hero Medal", 255, 36, 185, 0, 800));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SilentHorn, "Silent Horn", 255, 202, 189, 0, 800));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MagicMeal, "Magic Meal", 255, 240, 192, 0, 1000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.AntidoteAle, "Antidote Ale", 255, 240, 193, 0, 1000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SuperFlare, "Super Flare", 255, 240, 195, 0, 1000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.DovesBlood, "Dove's Blood", 255, 240, 199, 0, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.RayGun, "Ray Gun", 255, 69, 131, 0, 400));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MagicCharm, "Magic Charm", 255, 106, 142, 0, 800));
            m_items.Add(MM2Item.Create(MM2ItemIndex.WitchBroom, "Witch Broom", 255, 240, 144, 0, 1000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Invisocloak, "Invisocloak", 255, 246, 145, 0, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.StormWand, "Storm Wand", 255, 138, 146, 0, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LavaGrenade, "Lava Grenade", 255, 240, 151, 0, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Hourglass, "Hourglass", 255, 240, 154, 0, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.InstantKeep, "Instant Keep", 255, 240, 158, 0, 5000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.TeleportOrb, "Teleport Orb", 255, 240, 159, 0, 5000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SkeletonKey, "Skeleton Key", 6, 234, 0, 0, 800));
            m_items.Add(MM2Item.Create(MM2ItemIndex.DefenseRing, "Defense Ring", 255, 242, 153, 0, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MightGauntlet, "Might Gauntlet", 245, 6, 26, 0, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.AccuracyGauntlet, "Accuracy Gauntlet", 247, 70, 58, 0, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.StealthCape, "Stealth Cape", 6, 234, 47, 0, 4000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Admit8Pass, "Admit 8 Pass", 255, 240, 0, 0, 200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SpeedBoots, "Speed Boots", 255, 63, 205, 0, 15000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.CureallWand, "Cureall Wand", 255, 207, 207, 0, 15000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MoonRock, "Moon Rock", 255, 240, 215, 0, 12000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.RubyAnkh, "Ruby Ankh", 255, 90, 216, 0, 30000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Disruptor, "Disruptor", 255, 175, 155, 0, 20000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LichHand, "Lich Hand", 140, 240, 156, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Phaser, "Phaser", 255, 69, 160, 0, 20000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.FreezeWand, "Freeze Wand", 255, 159, 162, 0, 25000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Energizer, "Energizer", 255, 240, 163, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MagicMirror, "Magic Mirror", 255, 240, 166, 0, 30000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ElvenCloak, "Elven Cloak", 36, 245, 145, 0, 15000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ElvenBoots, "Elven Boots", 36, 53, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SageRobe, "Sage Robe", 8, 22, 90, 0, 25000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.EnchantedId, "Enchanted Id", 255, 47, 95, 0, 25000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GreenTicket, "Green Ticket", 255, 240, 0, 0, 10));
            m_items.Add(MM2Item.Create(MM2ItemIndex.YellowTicket, "Yellow Ticket", 255, 240, 0, 0, 50));
            m_items.Add(MM2Item.Create(MM2ItemIndex.RedTicket, "Red Ticket", 255, 240, 0, 0, 250));
            m_items.Add(MM2Item.Create(MM2ItemIndex.BlackTicket, "Black Ticket", 255, 240, 0, 0, 1000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.FeFarthing, "Fe Farthing", 255, 240, 0, 0, 10));
            m_items.Add(MM2Item.Create(MM2ItemIndex.CastleKey, "Castle Key", 6, 229, 0, 0, 200));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MarksKeys, "Mark's Keys", 255, 240, 0, 0, 1));
            m_items.Add(MM2Item.Create(MM2ItemIndex.DogWhistle, "Dog Whistle", 255, 81, 152, 0, 50));
            m_items.Add(MM2Item.Create(MM2ItemIndex.WebCaster, "Web Caster", 255, 0, 147, 0, 100));
            m_items.Add(MM2Item.Create(MM2ItemIndex.MonsterTome, "Monster Tome", 255, 240, 138, 0, 2000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.CupieDoll, "Cupie Doll", 255, 240, 0, 0, 1));
            m_items.Add(MM2Item.Create(MM2ItemIndex.WaterTalon, "Water Talon", 255, 240, 211, 0, 50000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.AirTalon, "Air Talon", 255, 240, 203, 0, 50000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.FireTalon, "Fire Talon", 255, 240, 217, 0, 50000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.EarthTalon, "Earth Talon", 255, 240, 213, 0, 50000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ElementOrb, "Element Orb", 255, 240, 175, 0, 34464));
            m_items.Add(MM2Item.Create(MM2ItemIndex.GoldGoblet, "Gold Goblet", 255, 240, 0, 0, 250));
            m_items.Add(MM2Item.Create(MM2ItemIndex.Loincloth, "+7 Loincloth", 255, 42, 0, 0, 5000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.ValorSword, "Valor Sword", 255, 240, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.HonorSword, "Honor Sword", 255, 240, 0, 0, 5000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.NobleSword, "Noble Sword", 255, 240, 0, 0, 5000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.CoraksSoul, "Corak's Soul", 255, 240, 0, 0, 1));
            m_items.Add(MM2Item.Create(MM2ItemIndex.EmeraldRing, "Emerald Ring", 255, 255, 0, 0, 1000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.WaterDisc, "Water Disc", 255, 240, 212, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.AirDisc, "Air Disc", 255, 240, 198, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.FireDisc, "Fire Disc", 255, 240, 218, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.EarthDisc, "Earth Disc", 255, 240, 208, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SapphirePin, "Sapphire Pin", 4, 95, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.AmethystBox, "Amethyst Box", 4, 95, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.CoralBroach, "Coral Broach", 1, 15, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.LapisScarab, "Lapis Scarab", 1, 15, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.AmberSkull, "Amber Skull", 8, 31, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.QuartzSkull, "Quartz Skull", 8, 31, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.AgateGrail, "Agate Grail", 64, 47, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.OpalPendant, "Opal Pendant", 64, 15, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.CrystalVial, "Crystal Vial", 2, 63, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.RubyAmulet, "Ruby Amulet", 2, 95, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.IvoryCameo, "Ivory Cameo", 128, 15, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.RubyTiara, "Ruby Tiara", 128, 79, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.OnyxEffigy, "Onyx Effigy", 16, 47, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.PearlChoker, "Pearl Choker", 16, 47, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.TopazShard, "Topaz Shard", 32, 79, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.SunCrown, "Sun Crown", 32, 31, 0, 0, 10000));
            m_items.Add(MM2Item.Create(MM2ItemIndex.J26Fluxer, "J-26 Fluxer", 255, 240, 0, 0, 1));
            m_items.Add(MM2Item.Create(MM2ItemIndex.M27Radicon, "M-27 Radicon", 255, 240, 0, 0, 1));
            m_items.Add(MM2Item.Create(MM2ItemIndex.A1Todilor, "A-1 Todilor", 255, 240, 0, 0, 1));
            m_items.Add(MM2Item.Create(MM2ItemIndex.N19Capitor, "N-19 Capitor", 255, 240, 0, 0, 1));
            m_items.Add(MM2Item.Create(MM2ItemIndex.UselessItem, "Useless Item", 255, 240, 0, 0, 1));
            m_bValid = true;
        }
    }

    [Flags]
    public enum MM2UsableByFlags
    {
        None = 0x00,
        Barbarian = 0x01,
        Ninja = 0x02,
        Robber = 0x04,
        Sorcerer = 0x08,
        Cleric = 0x10,
        Archer = 0x20,
        Paladin = 0x40,
        Knight = 0x80,
        AnyClass = 0xff
    }

    [Flags]
    public enum MM2BonusFlags
    {
        GoodOnly = 0x80,
        EvilOnly = 0x40,
        NeutralOnly = 0xC0,
        Anyone = 0x00,
        AlignmentFlags = 0xc0,
        PlusFlags = 0x3f,
        Cursed = 0xff,
    }

    public enum MM2ItemType
    {
        None,
        OneHandedMelee,
        TwoHandedMelee,
        Missile,
        Armor,
        Shield,
        Helmet,
        Key,
        Miscellaneous
    }

    public class MM2Item : MMItem
    {
        public override GameNames Game { get { return GameNames.MightAndMagic2; } }
        public MM2UsableByFlags UsableBy;
        public MM2BonusFlags BonusCurrent;
        public byte Equip;
        public byte Use;
        public byte DamageByte;
        public int BaseValue;
        private int m_base;

        public override int Index
        {
            get { return m_base; }
            set { m_base = value; }
        }

        public static MM2Item FromBagBytes(byte[] bytes)
        {
            MM2Item item = MM2.Items[bytes[0]].Clone() as MM2Item;
            item.m_iChargesCurrent = bytes[1];
            item.BonusCurrent = (MM2BonusFlags)bytes[2];
            return item;
        }

        public override int GetHashCode()
        {
            return Index | (m_iChargesCurrent << 8) | (Bonus << 16);
        }

        public override string UsableString
        {
            get
            {
                return UsableByString(UsableBy, false);
            }
        }

        public override string TypeString
        {
            get
            {
                switch (GetItemType(Index))
                {
                    case MM2ItemType.Armor: return "Armor";
                    case MM2ItemType.OneHandedMelee: return "1H Weapon";
                    case MM2ItemType.TwoHandedMelee: return "2H Weapon";
                    case MM2ItemType.Missile: return "Missile";
                    case MM2ItemType.Shield: return "Shield";
                    case MM2ItemType.Helmet: return "Helmet";
                    case MM2ItemType.Key: return "Key";
                    default: return "Misc";
                }
            }
        }

        public override string DescriptionString { get { return BonusValue > 0 ? String.Format("{0} {1}", Name, BonusString) : Name; } }

        public override long Value
        {
            get
            {
                if (BonusValue == 0)
                    return BaseValue;
                return (BonusValue - 1) * 1000 + (BaseValue * 2);
            }
            set
            {
                BaseValue = (int) value;
            }
        }

        public string BonusString
        {
            get
            {
                if (BonusValue == 0)
                    return "";
                return String.Format("+{0}", BonusValue);
            }
        }

        public int BonusValue { get { return (int)(BonusCurrent & MM2BonusFlags.PlusFlags); } }

        public override byte[] Serialize()
        {
            return new byte[] { (byte) Index, m_iChargesCurrent, (byte) BonusCurrent };
        }

        public override string UsableByAlignment
        {
            get
            {
                MM2BonusFlags align = BonusCurrent & MM2BonusFlags.AlignmentFlags;

                switch(align)
                {
                    case MM2BonusFlags.Anyone:  return "Any";
                    case MM2BonusFlags.EvilOnly: return "Evil";
                    case MM2BonusFlags.GoodOnly: return "Good";
                    case MM2BonusFlags.NeutralOnly: return "Neutral";
                    default: return "Unknown";
                }
            }
        }

        public override int ArmorClass { get { return ItemBaseType == ItemType.Armor ? DamageByte : 0; } }

        public override string MultiLineDescription
        {
            get
            {
                MM2ItemType type = GetItemType(Index);
                string strType = GetItemTypeString(type, true);

                string strName = Name;
                if (BonusCurrent == MM2BonusFlags.Cursed)
                    strName += " (CURSED)";
                else
                    strName += " " + BonusString;

                StringBuilder sbUsableClass = new StringBuilder("Usable by class: ");
                if (UsableBy.HasFlag(MM2UsableByFlags.AnyClass))
                    sbUsableClass.Append("ANY");
                else
                {
                    if (UsableBy.HasFlag(MM2UsableByFlags.Knight))
                        sbUsableClass.Append("Knight, ");
                    if (UsableBy.HasFlag(MM2UsableByFlags.Paladin))
                        sbUsableClass.Append("Paladin, ");
                    if (UsableBy.HasFlag(MM2UsableByFlags.Archer))
                        sbUsableClass.Append("Archer, ");
                    if (UsableBy.HasFlag(MM2UsableByFlags.Cleric))
                        sbUsableClass.Append("Cleric, ");
                    if (UsableBy.HasFlag(MM2UsableByFlags.Sorcerer))
                        sbUsableClass.Append("Sorcerer, ");
                    if (UsableBy.HasFlag(MM2UsableByFlags.Robber))
                        sbUsableClass.Append("Robber, ");
                    if (UsableBy.HasFlag(MM2UsableByFlags.Ninja))
                        sbUsableClass.Append("Ninja, ");
                    if (UsableBy.HasFlag(MM2UsableByFlags.Barbarian))
                        sbUsableClass.Append("Barbarian, ");
                    if (Global.Trim(sbUsableClass).Length == 0)
                        sbUsableClass.Append("NONE");
                }

                string sUsableAlign = "Usable by alignment: " + UsableByAlignment;

                string strDamageAC = "";
                switch (type)
                {
                    case MM2ItemType.Armor:
                    case MM2ItemType.Shield:
                    case MM2ItemType.Helmet:
                        strDamageAC = String.Format("Armor Class: {0}", DamageByte + BonusValue);
                        break;
                    case MM2ItemType.Missile:
                    case MM2ItemType.OneHandedMelee:
                    case MM2ItemType.TwoHandedMelee:
                        if (DamageByte > 0)
                            strDamageAC = String.Format("Damage: 1d{0}{1}", DamageByte, BonusString);
                        else
                            strDamageAC = "Damage: 0";
                        break;
                    default:
                        break;
                }

                string strEquip = EquipStringFull;
                string strUse = UseStringFull;

                StringBuilder sbFull = new StringBuilder();
                sbFull.AppendLine(strName);
                sbFull.AppendFormat("Type: {0}\r\n", strType);
                if (Equip != 0xF0)
                    sbFull.AppendFormat("{0}\r\n{1}\r\n", sbUsableClass.ToString(), sUsableAlign.ToString());
                if (!String.IsNullOrEmpty(strDamageAC))
                    sbFull.AppendLine(strDamageAC);
                if (Equip != 0xF0)
                    sbFull.AppendFormat("Equip: {0}\r\n", strEquip);
                sbFull.AppendFormat("Use: {0}\r\n", strUse);
                sbFull.AppendFormat("Value: {0} gold (base: {1})\r\n", Value, BaseValue);
                sbFull.AppendFormat("Duplicatable: {0}\r\n", Duplicatable ? "No" : "Yes");

                return sbFull.ToString();
            }
        }

        public override bool Duplicatable { get { return Index < 208; } }

        public static string GetItemTypeString(MM2ItemType type, bool bFullText)
        {
            switch (type)
            {
                case MM2ItemType.Armor: return "Armor";
                case MM2ItemType.OneHandedMelee: return bFullText ? "One-Handed Weapon" : "1H";
                case MM2ItemType.TwoHandedMelee: return bFullText ? "Two-Handed Weapon" : "2H";
                case MM2ItemType.Missile: return bFullText ? "Missile Weapon" : "Missile";
                case MM2ItemType.Shield: return "Shield";
                case MM2ItemType.Helmet: return "Helmet";
                case MM2ItemType.Key: return "Key";
                default: return bFullText ? "Miscellaneous Item" : "";
            }
        }

        public override EquipLocation CanEquipLocation
        {
            get
            {
                EquipLocation loc = GetEquipLocation(ItemBasicType);
                if (loc != EquipLocation.None)
                    return loc;
                if (CanEquip)
                    return EquipLocation.Accessory;
                return EquipLocation.None;
            }
        }

        public override string ItemNoun { get { return MMItem.GetItemNoun(ItemBasicType, Name); } }

        public override ItemNounType ItemBasicType
        {
            get
            {
                switch ((MM2ItemIndex)Index)
                {
                    case MM2ItemIndex.SmallClub:
                    case MM2ItemIndex.LargeClub:
                    case MM2ItemIndex.SpikedClub:
                    case MM2ItemIndex.SlumberClub:
                    case MM2ItemIndex.PowerClub: return ItemNounType.Club;
                    case MM2ItemIndex.SmallKnife:
                    case MM2ItemIndex.LargeKnife:
                    case MM2ItemIndex.LuckyKnife:
                    case MM2ItemIndex.LooterKnife: return ItemNounType.Knife;
                    case MM2ItemIndex.Dagger:
                    case MM2ItemIndex.LongDagger:
                    case MM2ItemIndex.SageDagger: return ItemNounType.Dagger;
                    case MM2ItemIndex.HandAxe:
                    case MM2ItemIndex.BattleAxe:
                    case MM2ItemIndex.TrueAxe:
                    case MM2ItemIndex.BlazingAxe:
                    case MM2ItemIndex.ElectricAxe:
                    case MM2ItemIndex.GrandAxe:
                    case MM2ItemIndex.SwiftAxe: return ItemNounType.Axe;
                    case MM2ItemIndex.GreatAxe: return ItemNounType.GreatAxe;
                    case MM2ItemIndex.Cudgel:
                    case MM2ItemIndex.PowerCudgel:
                    case MM2ItemIndex.HolyCudgel: return ItemNounType.Cudgel;
                    case MM2ItemIndex.BullWhip:
                    case MM2ItemIndex.EnergyWhip:
                    case MM2ItemIndex.SonicWhip:
                    case MM2ItemIndex.MightyWhip: return ItemNounType.Whip;
                    case MM2ItemIndex.Maul:
                    case MM2ItemIndex.ScorchMaul: return ItemNounType.Maul;
                    case MM2ItemIndex.Mace:
                    case MM2ItemIndex.DivineMace:
                    case MM2ItemIndex.MaulerMace: return ItemNounType.Mace;
                    case MM2ItemIndex.ShortSword:
                    case MM2ItemIndex.LongSword:
                    case MM2ItemIndex.BroadSword:
                    case MM2ItemIndex.AccurateSword:
                    case MM2ItemIndex.ChanceSword:
                    case MM2ItemIndex.SpeedySword:
                    case MM2ItemIndex.FlashSword:
                    case MM2ItemIndex.FlamingSword:
                    case MM2ItemIndex.ElectricSword:
                    case MM2ItemIndex.AcidicSword:
                    case MM2ItemIndex.ForceSword:
                    case MM2ItemIndex.MagicSword:
                    case MM2ItemIndex.ThunderSword: return ItemNounType.Sword;
                    case MM2ItemIndex.Nunchakas: return ItemNounType.Nunchakas;
                    case MM2ItemIndex.Spear:
                    case MM2ItemIndex.ExactoSpear:
                    case MM2ItemIndex.FierySpear: return ItemNounType.Spear;
                    case MM2ItemIndex.Cutlass:
                    case MM2ItemIndex.FastCutlass: return ItemNounType.Cutlass;
                    case MM2ItemIndex.Flail:
                    case MM2ItemIndex.QuickFlail:
                    case MM2ItemIndex.ShockFlail: return ItemNounType.Flail;
                    case MM2ItemIndex.Sabre:
                    case MM2ItemIndex.SharpSabre: return ItemNounType.Sabre;
                    case MM2ItemIndex.Wakizashi: return ItemNounType.Wakizashi;
                    case MM2ItemIndex.Scimitar:
                    case MM2ItemIndex.EgoScimitar:
                    case MM2ItemIndex.IceScimitar: return ItemNounType.Scimitar;
                    case MM2ItemIndex.Katana:
                    case MM2ItemIndex.RapidKatana:
                    case MM2ItemIndex.DynoKatana: return ItemNounType.Katana;
                    case MM2ItemIndex.ColdBlade:
                    case MM2ItemIndex.EnergyBlade:
                    case MM2ItemIndex.PhotonBlade: return ItemNounType.Blade;
                    case MM2ItemIndex.Staff:
                    case MM2ItemIndex.WindStaff:
                    case MM2ItemIndex.GeniusStaff:
                    case MM2ItemIndex.WizardStaff: return ItemNounType.Staff;
                    case MM2ItemIndex.Sickle:
                    case MM2ItemIndex.TriSickle:
                    case MM2ItemIndex.IceSickle: return ItemNounType.Sickle;
                    case MM2ItemIndex.Scythe:
                    case MM2ItemIndex.SoulScythe: return ItemNounType.Scythe;
                    case MM2ItemIndex.Glaive:
                    case MM2ItemIndex.FireGlaive: return ItemNounType.Glaive;
                    case MM2ItemIndex.WarHammer:
                    case MM2ItemIndex.GreatHammer:
                    case MM2ItemIndex.HarshHammer:
                    case MM2ItemIndex.StoneHammer: return ItemNounType.Hammer;
                    case MM2ItemIndex.Trident:
                    case MM2ItemIndex.DarkTrident: return ItemNounType.Trident;
                    case MM2ItemIndex.Pike:
                    case MM2ItemIndex.TitansPike: return ItemNounType.Pike;
                    case MM2ItemIndex.Naginata:
                    case MM2ItemIndex.SunNaginata: return ItemNounType.Naginata;
                    case MM2ItemIndex.Bardiche: return ItemNounType.Bardiche;
                    case MM2ItemIndex.Halberd:
                    case MM2ItemIndex.MoonHalberd: return ItemNounType.Halberd;
                    case MM2ItemIndex.Flamberge: return ItemNounType.Flamberge;
                    case MM2ItemIndex.Blowpipe:
                    case MM2ItemIndex.ShamanPipe:
                    case MM2ItemIndex.CinderPipe: return ItemNounType.Pipe;
                    case MM2ItemIndex.Sling:
                    case MM2ItemIndex.QuietSling:
                    case MM2ItemIndex.GiantSling:
                    case MM2ItemIndex.EnergySling: return ItemNounType.Sling;
                    case MM2ItemIndex.ShortBow:
                    case MM2ItemIndex.Crossbow:
                    case MM2ItemIndex.LongBow:
                    case MM2ItemIndex.GreatBow:
                    case MM2ItemIndex.PiratesXBow:
                    case MM2ItemIndex.BurningXBow:
                    case MM2ItemIndex.FireballBow:
                    case MM2ItemIndex.VoltageBow:
                    case MM2ItemIndex.DeathBow:
                    case MM2ItemIndex.StarBow:
                    case MM2ItemIndex.MeteorBow:
                    case MM2ItemIndex.AncientBow: return ItemNounType.Bow;
                    case MM2ItemIndex.SmallShield:
                    case MM2ItemIndex.LargeShield:
                    case MM2ItemIndex.GreatShield:
                    case MM2ItemIndex.FireShield:
                    case MM2ItemIndex.ElectricShield:
                    case MM2ItemIndex.AcidShield:
                    case MM2ItemIndex.ColdShield:
                    case MM2ItemIndex.SilverShield:
                    case MM2ItemIndex.BronzeShield:
                    case MM2ItemIndex.IronShield:
                    case MM2ItemIndex.MagicShield:
                    case MM2ItemIndex.GoldShield: return ItemNounType.Shield;
                    case MM2ItemIndex.Helm:
                    case MM2ItemIndex.IronHelm:
                    case MM2ItemIndex.BronzeHelm:
                    case MM2ItemIndex.SilverHelm:
                    case MM2ItemIndex.GoldHelm: return ItemNounType.Helm;
                    case MM2ItemIndex.PlateMail:
                    case MM2ItemIndex.IronPlateMail:
                    case MM2ItemIndex.BronzePlateMail:
                    case MM2ItemIndex.SilverPlateMail:
                    case MM2ItemIndex.GoldPlateMail: return ItemNounType.PlateMail;
                    case MM2ItemIndex.SplintMail:
                    case MM2ItemIndex.IronSplintMail:
                    case MM2ItemIndex.BronzeSplintMail:
                    case MM2ItemIndex.SilverSplintMail:
                    case MM2ItemIndex.GoldSplintMail: return ItemNounType.SplintMail;
                    case MM2ItemIndex.ChainMail:
                    case MM2ItemIndex.IronChainMail:
                    case MM2ItemIndex.BronzeChainMail:
                    case MM2ItemIndex.SilverChainMail:
                    case MM2ItemIndex.GoldChainMail: return ItemNounType.ChainMail;
                    case MM2ItemIndex.ScaleArmor: return ItemNounType.ScaleArmor;
                    case MM2ItemIndex.IronScaleMail:
                    case MM2ItemIndex.BronzeScaleMail:
                    case MM2ItemIndex.SilverScaleMail:
                    case MM2ItemIndex.GoldScaleMail: return ItemNounType.ScaleMail;
                    case MM2ItemIndex.RingMail:
                    case MM2ItemIndex.IronRingMail:
                    case MM2ItemIndex.BronzeRingMail:
                    case MM2ItemIndex.SilverRingMail:
                    case MM2ItemIndex.GoldRingMail: return ItemNounType.RingMail;
                    case MM2ItemIndex.PaddedArmor: return ItemNounType.PaddedArmor;
                    case MM2ItemIndex.LeatherSuit: return ItemNounType.LeatherSuit;
                    case MM2ItemIndex.PlateArmor: return ItemNounType.PlateArmor;
                    default: return ItemNounType.None;
                }
            }
        }

        public override string GetLongDescription(GenericAlignmentValue currentAlign, GenericClass currentClass, string strOverrideName)
        {
            MM2ItemType type = GetItemType(Index);
            string strType = GetItemTypeString(type, false);

            string strName = String.IsNullOrWhiteSpace(strOverrideName) ? ItemNoun : strOverrideName;

            if (BonusCurrent == MM2BonusFlags.Cursed)
                strName += " (CURSED)";

            string strUsable = "";
            if (type != MM2ItemType.None)
            {
                if (currentClass != GenericClass.None && !IsUsable(currentClass))
                    strUsable = String.Format(" (!{0})", MM2Character.ClassString(currentClass));
                if (currentClass != GenericClass.None && !IsUsable(currentAlign))
                    strUsable = String.Format(" (!{0})", MM2Character.AlignmentString(currentAlign));
            }

            string strBonus = BonusString;
            string strDamage = DamageString;
            string strEquip = EquipString;
            string strUse = UseString;

            return String.Format("{0}{1}{2}{3}, {4}{5}{6}{7} Gold",
                strName,
                String.IsNullOrEmpty(strBonus) ? "" : " " + strBonus,
                strUsable,
                String.IsNullOrEmpty(strType) ? "" : " (" + strType + ")",
                String.IsNullOrEmpty(strDamage) ? "" : strDamage + ", ",
                String.IsNullOrEmpty(strEquip) ? "" : strEquip + ", ",
                String.IsNullOrEmpty(strUse) ? "" : "Use: " + strUse + ", ",
                Value);
        }

        public string UseString
        {
            get
            {
                if (Use == 0x00)
                    return "";
                if ((Use & 0x80) > 0)
                {
                    int iIndex = (Use & 0x7f);
                    return String.Format("{0} [{1}]", MM2SpellList.GetSpellNameForItem(iIndex), m_iChargesCurrent);
                }
                int iUseAttr = (Use & 0x70) >> 4;
                int iBonus = (Use & 0xf) + BonusValue;
                if (iUseAttr == 0)
                    iBonus *= 256;  // MaxHP affects the upper byte  
                return String.Format("{0}+{1} [{2}]", UseBonusStringAbbreviated(iUseAttr), iBonus, m_iChargesCurrent);
            }
        }

        public string UseStringFull
        {
            get
            {
                if (Use == 0x00)
                    return "No special power";
                if ((Use & 0x80) > 0)
                {
                    int iIndex = (Use & 0x7f);
                    return String.Format("Casts {0} ({1})", MM2SpellList.GetSpellNameForItem(iIndex), Global.Plural(m_iChargesCurrent, "charge"));
                }
                int iUseAttr = (Use & 0x70) >> 4;
                int iBonus = (Use & 0xf) + BonusValue;
                if (iUseAttr == 0)
                    iBonus *= 256;  // MaxHP affects the upper byte  
                return String.Format("Gives +{0} to {1} ({2})", iBonus, UseBonusStringAbbreviated(iUseAttr), Global.Plural(m_iChargesCurrent, "charge"));
            }
        }

        public string EquipString
        {
            get
            {
                if ((Equip & 0x0f) == 0)
                    return "";
                return String.Format("{0}+{1}", EquipBonusStringAbbreviated((Equip & 0xf0) >> 4), (Equip & 0x0f) + BonusValue);
            }
        }

        public override string EquipEffects { get { return EquipString; } }

        public string EquipStringFull
        {
            get
            {
                if (Equip == 0xF0)
                    return "Cannot be equipped";
                if ((Equip & 0x0f) == 0)
                    return "No special bonus";
                return String.Format("Gives +{0} to {1}", (Equip & 0x0f) + BonusValue, EquipBonusString((Equip & 0xf0) >> 4));
            }
        }

        public override CompareResult CompareTo(Item item)
        {
            if (!(item is MM2Item))
                return CompareResult.Uncomparable;
            if (item == this)
                return CompareResult.Identical;
            if (Type != item.Type)
                return CompareResult.Uncomparable;

            MM2Item mm2Item = item as MM2Item;

            if (mm2Item.Cursed && !Cursed)
                return CompareResult.Better;     // Cursed is always worse than non-cursed
            else if (!mm2Item.Cursed && Cursed)
                return CompareResult.Worse;    // ... and vice-versa

            if (Equip != mm2Item.Equip && (Equip & 0x0f) != 0 && (mm2Item.Equip & 0x0f) != 0)
                return CompareResult.Uncomparable;
            if (CanEquipLocation != mm2Item.CanEquipLocation)
                return CompareResult.Uncomparable;

            switch (GetItemType(Index))
            {
                case MM2ItemType.Helmet:
                case MM2ItemType.Armor:
                case MM2ItemType.Shield:
                    return CompareValues(EquipBonusValue, mm2Item.EquipBonusValue, ArmorClass, mm2Item.ArmorClass);
                case MM2ItemType.Missile:
                case MM2ItemType.OneHandedMelee:
                case MM2ItemType.TwoHandedMelee:
                    return CompareValues((DamageByte + 1) / 2.0 + BonusValue, (mm2Item.DamageByte + 1) / 2.0 + mm2Item.BonusValue, EquipBonusValue, mm2Item.EquipBonusValue);
                default:
                    if (CanEquip && mm2Item.CanEquip)
                        return CompareValues(EquipBonusValue, mm2Item.EquipBonusValue, ArmorClass, mm2Item.ArmorClass, BonusValue, mm2Item.BonusValue);
                    break;
            }
            return CompareResult.Uncomparable;
        }

        public string DamageString
        {
            get
            {
                MM2ItemType type = GetItemType(Index);
                switch (type)
                {
                    case MM2ItemType.Armor:
                    case MM2ItemType.Shield:
                    case MM2ItemType.Helmet:
                        return String.Format("AC {0}", DamageByte + BonusValue);
                    case MM2ItemType.Missile:
                    case MM2ItemType.OneHandedMelee:
                    case MM2ItemType.TwoHandedMelee:
                        if (DamageByte > 0)
                            return String.Format("1d{0}{1}", DamageByte, BonusString);
                        return "0";
                    default:
                        return "";
                }
            }
        }

        public override bool IsWeapon
        {
            get
            {
                MM2ItemType type = GetItemType(Index);
                switch (type)
                {
                    case MM2ItemType.Missile:
                    case MM2ItemType.OneHandedMelee:
                    case MM2ItemType.TwoHandedMelee:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override BasicDamage BaseDamage
        {
            get
            {
                if (!IsWeapon)
                    return BasicDamage.Zero;
                return new BasicDamage(1, new DamageDice(DamageByte, 1, BonusValue));
            }
        }

        public bool IsUsable(GenericClass testClass)
        {
            switch (testClass)
            {
                case GenericClass.Archer: return UsableBy.HasFlag(MM2UsableByFlags.Archer);
                case GenericClass.Cleric: return UsableBy.HasFlag(MM2UsableByFlags.Cleric);
                case GenericClass.Knight: return UsableBy.HasFlag(MM2UsableByFlags.Knight);
                case GenericClass.Paladin: return UsableBy.HasFlag(MM2UsableByFlags.Paladin);
                case GenericClass.Robber: return UsableBy.HasFlag(MM2UsableByFlags.Robber);
                case GenericClass.Sorcerer: return UsableBy.HasFlag(MM2UsableByFlags.Sorcerer);
                case GenericClass.Ninja: return UsableBy.HasFlag(MM2UsableByFlags.Ninja);
                case GenericClass.Barbarian: return UsableBy.HasFlag(MM2UsableByFlags.Barbarian);
                default: return false;
            }
        }

        public override GenericAlignmentValue Alignment
        {
            get
            {
                switch (BonusCurrent & MM2BonusFlags.AlignmentFlags)
                {
                    case MM2BonusFlags.GoodOnly: return GenericAlignmentValue.Good;
                    case MM2BonusFlags.EvilOnly: return GenericAlignmentValue.Evil;
                    case MM2BonusFlags.NeutralOnly: return GenericAlignmentValue.Neutral;
                    default: return GenericAlignmentValue.None;
                }
            }
        }

        public bool IsUsable(GenericAlignmentValue testAlign)
        {
            MM2BonusFlags restrict = BonusCurrent & MM2BonusFlags.AlignmentFlags;
            switch (restrict)
            {
                case MM2BonusFlags.GoodOnly: return (testAlign == GenericAlignmentValue.Good);
                case MM2BonusFlags.EvilOnly: return (testAlign == GenericAlignmentValue.Evil);
                case MM2BonusFlags.NeutralOnly: return (testAlign == GenericAlignmentValue.Neutral);
                default: return true;
            }
        }

        public override bool IsUsableByAny(object filter)
        {
            if (filter is GenericAlignmentValue)
                return IsUsable((GenericAlignmentValue)filter);
            else if (filter is GenericClass)
                return IsUsable((GenericClass)filter);

            return false;
        }

        public override Item Clone()
        {
            return MM2Item.Create((MM2ItemIndex) Index, Name, (byte)UsableBy, Equip, Use, DamageByte, (ushort) Value, BonusCurrent, m_iChargesCurrent);
        }

        private MM2Item()
        {
        }

        public static MM2Item Create(byte[] bytes, int iIndex)
        {
            MM2Item item = new MM2Item();
            item.SetFromBytes(bytes, iIndex);
            return item;
        }

        public void SetFromBytes(byte[] bytes, int iIndex)
        {
            // Name 0-11
            // Terminator 12  Always zero in ITEMS.DAT
            // UsableBy 13    Actually "not usable by" whichever bits are set
            // EquipBonus 14  low 4 bits = value, high 4 bits = offset into character record + 16
            // UseBonus 15    10xxxxxx = sorcerer spell, 11xxxxxx = cleric spell, 0xxxyyyy = raise xxx by yyyy
            // Damage 16      Damage for weapons, AC for armor
            // Unknown 17     Plus value?  Always zero in ITEMS.DAT
            // Value 18-19    Gold value

            Index = iIndex;
            Name = Encoding.ASCII.GetString(bytes, 0, 12).Trim();

            UsableBy = (MM2UsableByFlags)(~bytes[13]);
            Equip = bytes[14];
            Use = bytes[15];
            DamageByte = bytes[16];
            Value = (ushort)(bytes[19] << 8 | bytes[18]);
            m_iChargesCurrent = 0;
            BonusCurrent = 0;
        }

        public static MM2Item Create(MM2ItemIndex index, string name, byte usable, byte equip, byte use, byte damage, ushort value, MM2BonusFlags bonus = MM2BonusFlags.Anyone, byte charges = 0)
        {
            MM2Item item = new MM2Item();
            item.SetFromValues(index, name, usable, equip, use, damage, value, bonus, charges);
            return item;
        }

        public void SetFromValues(MM2ItemIndex index, string name, byte usable, byte equip, byte use, byte damage, ushort value, MM2BonusFlags bonus = MM2BonusFlags.Anyone, byte charges = 0)
        {
            Index = (int) index;
            Name = name;
            UsableBy = (MM2UsableByFlags)usable;
            Equip = equip;
            Use = use;
            Value = value;
            DamageByte = damage;
            m_iChargesCurrent = charges;
            BonusCurrent = bonus;
        }

        public static MM2ItemType GetItemType(int index)
        {
            if (index >= 1 && index <= 65)
                return MM2ItemType.OneHandedMelee;
            if (index >= 66 && index <= 91)
                return MM2ItemType.TwoHandedMelee;
            if (index >= 92 && index <= 110)
                return MM2ItemType.Missile;
            if (index >= 111 && index <= 114)
                return MM2ItemType.Key;
            if (index >= 115 && index <= 126)
                return MM2ItemType.Shield;
            if (index >= 127 && index <= 154)
                return MM2ItemType.Armor;
            if (index >= 155 && index <= 159)
                return MM2ItemType.Helmet;
            if (index >= 160 && index <= 255)
                return MM2ItemType.Miscellaneous;
            return MM2ItemType.None;
        }

        public override bool Trashable
        {
            get
            {
                switch ((MM2ItemIndex)Index)
                {
                    case MM2ItemIndex.Admit8Pass:
                    case MM2ItemIndex.Phaser:
                    case MM2ItemIndex.EnchantedId:
                    case MM2ItemIndex.GreenTicket:
                    case MM2ItemIndex.YellowTicket:
                    case MM2ItemIndex.RedTicket:
                    case MM2ItemIndex.BlackTicket:
                    case MM2ItemIndex.FeFarthing:
                    case MM2ItemIndex.CastleKey:
                    case MM2ItemIndex.MarksKeys:
                    case MM2ItemIndex.CupieDoll:
                    case MM2ItemIndex.WaterTalon:
                    case MM2ItemIndex.AirTalon:
                    case MM2ItemIndex.FireTalon:
                    case MM2ItemIndex.EarthTalon:
                    case MM2ItemIndex.ElementOrb:
                    case MM2ItemIndex.GoldGoblet:
                    case MM2ItemIndex.Loincloth:
                    case MM2ItemIndex.ValorSword:
                    case MM2ItemIndex.HonorSword:
                    case MM2ItemIndex.NobleSword:
                    case MM2ItemIndex.CoraksSoul:
                    case MM2ItemIndex.EmeraldRing:
                    case MM2ItemIndex.WaterDisc:
                    case MM2ItemIndex.AirDisc:
                    case MM2ItemIndex.FireDisc:
                    case MM2ItemIndex.EarthDisc:
                    case MM2ItemIndex.J26Fluxer:
                    case MM2ItemIndex.M27Radicon:
                    case MM2ItemIndex.A1Todilor:
                    case MM2ItemIndex.N19Capitor: 
                        return false;
                    default:
                        return true;
                }
            }
        }

        public override ItemType Type
        {
            get
            {
                switch (GetItemType(Index))
                {
                    case MM2ItemType.Armor:
                    case MM2ItemType.Helmet:
                    case MM2ItemType.Shield:
                        return ItemType.Armor;
                    case MM2ItemType.Missile: return ItemType.Missile;
                    case MM2ItemType.OneHandedMelee: return ItemType.OneHandMelee;
                    case MM2ItemType.TwoHandedMelee: return ItemType.TwoHandMelee;
                    case MM2ItemType.Miscellaneous: return Equip == 0xf0 ? ItemType.Miscellaneous : ItemType.Accessory;
                    default: return ItemType.None;
                }
            }
            set { }
        }

        public string Dump()
        {
            return String.Format("{0:X2} {1,-12}  {2}  {3,-9}  {4,-3}  {5,-6} {6,-28}  {7,-3}  {8,-5}  {9,-3}",
                //            return String.Format("m_items.Add({0},\"{1}\", {2},{3},{4},{5},{6},{7},{8},{9},{10});\r\n",
                Index,
                Name,
                UsableByString(UsableBy, true),
                EquipAttributeStringAbbreviated(Equip),
                Equip & 0x0f,
                UseAttributeStringAbbreviated(Use),
                ((Use & 0x80) > 0) ? MM2SpellList.GetSpellNameForItem(Use & 0x7f) : (Use & 0x0f).ToString(),
                DamageByte,
                Value,
                m_iChargesCurrent
                );
        }

        public string InsertString()
        {
            return String.Format("new MM2Item({0},\"{1}\",{2},{3},{4},{5},{6})",
                Index,
                Name.Trim(),
                (byte) UsableBy,
                Equip,
                Use,
                DamageByte,
                Value
                );
        }

        public static string UsableByString(MM2UsableByFlags flags, bool bShowDots)
        {
            string strDot = bShowDots ? "." : "";
            string strUsable = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                flags.HasFlag(MM2UsableByFlags.Knight) ? "K" : strDot,
                flags.HasFlag(MM2UsableByFlags.Paladin) ? "P" : strDot,
                flags.HasFlag(MM2UsableByFlags.Archer) ? "A" : strDot,
                flags.HasFlag(MM2UsableByFlags.Cleric) ? "C" : strDot,
                flags.HasFlag(MM2UsableByFlags.Sorcerer) ? "S" : strDot,
                flags.HasFlag(MM2UsableByFlags.Robber) ? "R" : strDot,
                flags.HasFlag(MM2UsableByFlags.Barbarian) ? "B" : strDot,
                flags.HasFlag(MM2UsableByFlags.Ninja) ? "N" : strDot
                );
            return strUsable;
        }

        public static string EquipAttributeString(int attr)
        {
            if (attr == 0xf0)
                return "No Equip";
            if (attr == 0)
                return "None";
            return EquipBonusString((attr & 0xf0) >> 4);
        }

        public static string EquipAttributeStringAbbreviated(int attr)
        {
            if (attr == 0xf0)
                return "No Equip";
            if (attr == 0)
                return "None";
            return EquipBonusStringAbbreviated((attr & 0xf0) >> 4);
        }

        public static string UseAttributeString(byte attr)
        {
            if (attr == 0)
                return "None";
            if ((attr & 0x80) > 0)
                return "Spell";
            return UseBonusString((attr & 0x70) >> 4);
        }

        public static string UseAttributeStringAbbreviated(byte attr)
        {
            if (attr == 0)
                return "None";
            if ((attr & 0x80) > 0)
                return "Spell";
            return UseBonusStringAbbreviated((attr & 0x70) >> 4);
        }

        public static GenericAttribute GetEquipAttribute(int offset)
        {
            switch (offset)
            {
                case 0: return GenericAttribute.Might;
                case 1: return GenericAttribute.Intellect;
                case 2: return GenericAttribute.Personality;
                case 3: return GenericAttribute.Speed;
                case 4: return GenericAttribute.Accuracy;
                case 5: return GenericAttribute.Luck;
                case 6: return GenericAttribute.MagicRes;
                case 7: return GenericAttribute.FireRes;
                case 8: return GenericAttribute.ElecRes;
                case 9: return GenericAttribute.ColdRes;
                case 10: return GenericAttribute.EnergyRes;
                case 11: return GenericAttribute.SleepRes;
                case 12: return GenericAttribute.PoisonRes;
                case 13: return GenericAttribute.AcidRes;
                case 14: return GenericAttribute.Thievery;
                case 15: return GenericAttribute.AC;
                default: return GenericAttribute.None;
            }
        }

        public static string EquipBonusString(int offset)
        {
            switch (offset)
            {
                case 0: return "Might";
                case 1: return "Intellect";
                case 2: return "Personality";
                case 3: return "Speed";
                case 4: return "Accuracy";
                case 5: return "Luck";
                case 6: return "Magic Resistance";
                case 7: return "Fire Resistance";
                case 8: return "Electricity Resistance";
                case 9: return "Cold Resistance";
                case 10: return "Energy Resistance";
                case 11: return "Sleep Resistance";
                case 12: return "Poison Resistance";
                case 13: return "Acid Resistance";
                case 14: return "Thievery";
                case 15: return "Armor Class";
                default: return String.Format("(invalid:{0:X2})", offset);
            }
        }

        public static string EquipBonusStringAbbreviated(int offset)
        {
            switch (offset)
            {
                case 0: return "Mgt";
                case 1: return "Int";
                case 2: return "Per";
                case 3: return "Spd";
                case 4: return "Acy";
                case 5: return "Lck";
                case 6: return "MagRes";
                case 7: return "FireRes";
                case 8: return "ElecRes";
                case 9: return "ColdRes";
                case 10: return "EnergyRes";
                case 11: return "SleepRes";
                case 12: return "PoisonRes";
                case 13: return "AcidRes";
                case 14: return "Thievery";
                case 15: return "AC";
                default: return String.Format("({0:X2})", offset);
            }
        }

        public static string UseBonusString(int offset)
        {
            switch (offset)
            {
                case 0: return "MaxHP";
                case 1: return "Might";
                case 2: return "Speed";
                case 3: return "Accuracy";
                case 4: return "Unknown";
                case 5: return "Level";
                case 6: return "Spell Level";
                default: return String.Format("(invalid:{0:X2})", offset);
            }
        }

        public static string UseBonusStringAbbreviated(int offset)
        {
            switch (offset)
            {
                case 0: return "MaxHP";
                case 1: return "Mgt";
                case 2: return "Spd";
                case 3: return "Acy";
                case 4: return "Unk";
                case 5: return "Lev";
                case 6: return "S.Lev";
                default: return String.Format("({0:X2})", offset);
            }
        }

        public static string SpellString(ushort spell)
        {
            return String.Format("{0:X4}", spell);
        }

        public override string AttributeString 
        { 
            get
            {
                if (Equip == 0xf0 || Equip == 0)
                    return String.Empty;
                return EquipBonusString(Equip >> 4).Replace("Resistance", "Res");
            }
        }

        public override int EquipBonusValue
        { 
            get
            {
                if (Equip == 0xf0 || Equip == 0)
                    return 0;
                return (Equip & 0xf) + BonusValue;
            }
        }

        public override string UseEffectString
        {
            get
            {
                if (Use == 0x00)
                    return String.Empty;
                if ((Use & 0x80) > 0)
                    return MM2SpellList.GetSpellNameForItem(Use & 0x7f);
                int iUseAttr = (Use & 0x70) >> 4;
                int iBonus = (Use & 0xf) + BonusValue;
                if (iUseAttr == 0)
                    iBonus *= 256;  // MaxHP affects the upper byte  
                return String.Format("{0} {1}", UseBonusString(iUseAttr), Global.AddPlus(iBonus));
            }
        }

        public override string LargestBonusEffect { get { return AttributeString; } }
        public override int LargestBonus { get { return EquipBonusValue; } }
    }

    public class MM2ShopInventory : ShopInventory
    {
        public enum ItemCategory { Current, Weapons, Armor, Misc, Special };

        public List<ShopItem> Items;

        public override IEnumerable<ShopItem> AllItems { get { return Items; } }

        public List<ShopItem> GetItems(byte[] bytes, int iStart, int iLength, ItemCategory category, int iCurrentDay)
        {
            List<ShopItem> items = new List<ShopItem>(iLength);
            for (int i = iStart; i < iStart + iLength; i++)
            {
                if (i >= bytes.Length)
                    break;

                MM2Item item = MM2.Items[bytes[i]].Clone() as MM2Item;

                switch (category)
                {
                    case ItemCategory.Misc:
                        item.m_iChargesCurrent = bytes[i + 30];
                        break;
                    case ItemCategory.Special:
                        if (iCurrentDay % 30 == 29)
                            item.BonusCurrent = (MM2BonusFlags)bytes[210 + (iCurrentDay / 30)];
                        else
                            item.BonusCurrent = (MM2BonusFlags)bytes[216 + (iCurrentDay % 30)];
                        break;
                    default:
                        item.BonusCurrent = (MM2BonusFlags) bytes[i + 30];
                        break;
                }
                items.Add(new ShopItem(item, MM2Memory.ShopItems + i, (int)category));
            }

            return items;
        }

        public List<ShopItem> GetItemSet(byte[] bytes, int iStart, int iCurrentDay)
        {
            List<ShopItem> items = new List<ShopItem>(24);
            items.AddRange(GetItems(bytes, iStart, 6, ItemCategory.Weapons, iCurrentDay));
            items.AddRange(GetItems(bytes, 60 + iStart, 6, ItemCategory.Armor, iCurrentDay));
            items.AddRange(GetItems(bytes, 120 + iStart, 6, ItemCategory.Misc, iCurrentDay));
            items.AddRange(GetItems(bytes, 180 + iStart, 6, ItemCategory.Special, iCurrentDay));
            return items;
        }

        public MM2ShopInventory(byte[] bytes, MM2Map map, int iCurrentDay)
        {
            Items = new List<ShopItem>(24);
            switch (map)
            {
                case MM2Map.C2Middlegate:
                    Items.AddRange(GetItemSet(bytes, 0, iCurrentDay));
                    break;
                case MM2Map.A4Atlantium:
                    Items.AddRange(GetItemSet(bytes, 6, iCurrentDay));
                    break;
                case MM2Map.A1Tundara:
                    Items.AddRange(GetItemSet(bytes, 12, iCurrentDay));
                    break;
                case MM2Map.E1Vulcania:
                    Items.AddRange(GetItemSet(bytes, 18, iCurrentDay));
                    break;
                case MM2Map.E4Sandsobar:
                    Items.AddRange(GetItemSet(bytes, 24, iCurrentDay));
                    break;
                default:
                    break;
            }
        }
    }
}
