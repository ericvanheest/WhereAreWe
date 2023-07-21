using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum MM1ItemIndex
    {
        Empty = 0,
        Club = 1,
        Dagger = 2,
        HandAxe = 3,
        Spear = 4,
        ShortSword = 5,
        Mace = 6,
        Flail = 7,
        Scimitar = 8,
        BroadSword = 9,
        BattleAxe = 10,
        LongSword = 11,
        ClubPlus1 = 12,
        ClubPlus2 = 13,
        DaggerPlus1 = 14,
        HandAxePlus1 = 15,
        SpearPlus1 = 16,
        ShortSwordPlus1 = 17,
        MacePlus1 = 18,
        FlailPlus1 = 19,
        ScimitarPlus1 = 20,
        BroadSwordPlus1 = 21,
        BattleAxePlus1 = 22,
        LongSwordPlus1 = 23,
        FlamingClub = 24,
        ClubOfNoise = 25,
        DaggerPlus2 = 26,
        HandAxePlus2 = 27,
        SpearPlus2 = 28,
        ShortSwordPlus2 = 29,
        MacePlus2 = 30,
        FlailPlus2 = 31,
        ScimitarPlus2 = 32,
        BroadSwordPlus2 = 33,
        BattleAxePlus2 = 34,
        LongSwordPlus2 = 35,
        RoyalDagger = 36,
        DaggerOfMind = 37,
        DiamondDagger = 38,
        ElectricSpear = 39,
        HolyMace = 40,
        UnHolyMace = 41,
        DarkFlail = 42,
        FlailOfFear = 43,
        LuckyScimitar = 44,
        MaceOfUndead = 45,
        ColdAxe = 46,
        ElectricSword = 47,
        FlamingSword = 48,
        SwordOfMight = 49,
        SwordOfSpeed = 50,
        SharpSword = 51,
        AccurateSword = 52,
        SwordOfMagic = 53,
        ImmortalSword = 54,
        AxeProtector = 55,
        AxeDestroyer = 56,
        XXXXsSword = 57,
        AdamantineAxe = 58,
        UltimateSword = 59,
        ElementSword = 60,
        Sling = 61,
        Crossbow = 62,
        ShortBow = 63,
        LongBow = 64,
        GreatBow = 65,
        SlingPlus1 = 66,
        CrossbowPlus1 = 67,
        ShortBowPlus1 = 68,
        LongBowPlus1 = 69,
        GreatBowPlus1 = 70,
        MagicSling = 71,
        CrossbowPlus2 = 72,
        ShortBowPlus2 = 73,
        LongBowPlus2 = 74,
        GreatBowPlus2 = 75,
        CrossbowLuck = 76,
        CrossbowSpeed = 77,
        LightningBow = 78,
        FlamingBow = 79,
        GiantsBow = 80,
        TheMagicBow = 81,
        BowOfPower = 82,
        RobbersXBow = 83,
        ArchersBow = 84,
        ObsidianBow = 85,
        Staff = 86,
        Glaive = 87,
        Bardiche = 88,
        Halberd = 89,
        GreatHammer = 90,
        GreatAxe = 91,
        Flamberge = 92,
        StaffPlus1 = 93,
        GlaivePlus1 = 94,
        BardichePlus1 = 95,
        HalberdPlus1 = 96,
        GreatHammerPlus1 = 97,
        GreatAxePlus1 = 98,
        FlambergePlus1 = 99,
        StaffPlus2 = 100,
        GlaivePlus2 = 101,
        BardichePlus2 = 102,
        HalberdPlus2 = 103,
        GreatHammerPlus2 = 104,
        GreatAxePlus2 = 105,
        FlambergePlus2 = 106,
        StaffOfLight = 107,
        ColdGlaive = 108,
        CuringStaff = 109,
        MinotaursAxe = 110,
        ThunderHammer = 111,
        GreatAxePlus3 = 112,
        FlambergePlus3 = 113,
        SorcererStaff = 114,
        StaffOfMagic = 115,
        DemonsGlaive = 116,
        DevilsGlaive = 117,
        TheFlamberge = 118,
        HolyFlamberge = 119,
        EvilFlamberge = 120,
        PaddedArmor = 121,
        LeatherArmor = 122,
        ScaleArmor = 123,
        RingMail = 124,
        ChainMail = 125,
        SplintMail = 126,
        PlateMail = 127,
        PaddedArmorPlus1 = 128,
        LeatherArmorPlus1 = 129,
        ScaleArmorPlus1 = 130,
        RingMailPlus1 = 131,
        ChainMailPlus1 = 132,
        SplintMailPlus1 = 133,
        PlateMailPlus1 = 134,
        LeatherArmorPlus2 = 135,
        ScaleArmorPlus2 = 136,
        RingMailPlus2 = 137,
        ChainMailPlus2 = 138,
        SplintMailPlus2 = 139,
        PlateMailPlus2 = 140,
        BracersAC4 = 141,
        RingMailPlus3 = 142,
        ChainMailPlus3 = 143,
        SplintMailPlus3 = 144,
        PlateMailPlus3 = 145,
        BracersAC6 = 146,
        ChainMailPlus3Cursed = 147,
        BracersAC8Cursed = 148,
        BlueRingMail = 149,
        RedChainMail = 150,
        XXXXsPlate = 151,
        HolyPlate = 152,
        UnHolyPlate = 153,
        UltimatePlate = 154,
        BracersAC8 = 155,
        SmallShield = 156,
        LargeShield = 157,
        SilverShield = 158,
        SmallShieldPlus1 = 159,
        LargeShieldPlus1 = 160,
        LargeShieldPlus1Cursed = 161,
        SmallShieldPlus2 = 162,
        LargeShieldPlus2 = 163,
        LargeShieldPlus2Cursed = 164,
        FireShield = 165,
        ColdShield = 166,
        ElectricShield = 167,
        AcidShield = 168,
        MagicShield = 169,
        DragonShield = 170,
        RopeAndHooks = 171,
        Torch = 172,
        Lantern = 173,
        TenFootPole = 174,
        Garlic = 175,
        Wolfsbane = 176,
        Belladonna = 177,
        MagicHerbs = 178,
        DriedBeef = 179,
        RobbersTools = 180,
        BagOfSilver = 181,
        AmberGem = 182,
        SmellingSalt = 183,
        BagOfSand = 184,
        MightPotion = 185,
        SpeedPotion = 186,
        Sundial = 187,
        CuringPotion = 188,
        MagicPotion = 189,
        DefenseRing = 190,
        BagOfGarbage = 191,
        ScrollOfFire = 192,
        FlyingCarpet = 193,
        JadeAmulet = 194,
        AntidoteBrew = 195,
        SkillPotion = 196,
        BootsOfSpeed = 197,
        LuckyCharm = 198,
        WandOfFire = 199,
        UndeadAmulet = 200,
        SilentChime = 201,
        BeltOfPower = 202,
        ModelBoat = 203,
        DefenseCloak = 204,
        KnowledgeBook = 205,
        RubyIdol = 206,
        SorcererRobe = 207,
        PowerGauntlet = 208,
        ClericsBeads = 209,
        HornOfDeath = 210,
        PotionOfLife = 211,
        ShinyPendant = 212,
        LightningWand = 213,
        PrecisionRing = 214,
        ReturnScroll = 215,
        TeleportHelm = 216,
        YouthPotion = 217,
        BellsOfTime = 218,
        MagicOil = 219,
        MagicVest = 220,
        DestroyerWand = 221,
        ElementScarab = 222,
        SunScroll = 223,
        StarRuby = 224,
        StarSapphire = 225,
        WealthChest = 226,
        GemSack = 227,
        DiamondCollar = 228,
        FireOpal = 229,
        Unobtainium = 230,
        VellumScroll = 231,
        RubyWhistle = 232,
        KingsPass = 233,
        MerchantsPass = 234,
        CrystalKey = 235,
        CoralKey = 236,
        BronzeKey = 237,
        SilverKey = 238,
        GoldKey = 239,
        DiamondKey = 240,
        CactusNectar = 241,
        MapOfDesert = 242,
        LaserBlaster = 243,
        DragonsTooth = 244,
        WyvernEye = 245,
        MedusaHead = 246,
        RingOfOkrim = 247,
        BlackQueenIdol = 248,
        WhiteQueenIdol = 249,
        PiratesMapA = 250,
        PiratesMapB = 251,
        Thundranium = 252,
        KeyCard = 253,
        EyeOfGoros = 254,
        UselessItem = 255
    }

    public class MM1ItemList
    {
        private bool m_bValid = false;
        private string m_strError = string.Empty;
        private List<MM1Item> m_items = new List<MM1Item>();

        public List<MM1Item> Items
        {
            get { return m_items; }
        }

        public MM1ItemList(string strFile)
        {
            m_bValid = false;
            BinaryReader reader = null;
            int iIndex = 1;

            try
            {
                reader = new BinaryReader(File.OpenRead(strFile));
                while (reader.BaseStream.Position <= reader.BaseStream.Length - 24)
                {
                    byte[] bytes = reader.ReadBytes(24);
                    m_items.Add(MM1Item.Create(bytes, iIndex++));
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
            foreach (MM1Item item in m_items)
            {
                sb.Append(item.Dump());
            }
            return sb.ToString();
        }

        public MM1Item GetItem(byte index, int memory = -1)
        {
            MM1Item item = null;
            if (m_items.Count > index)
                item = m_items[index].Clone() as MM1Item;
            else
                item = MM1Item.Create(0, "<empty>", 255, 0, 0, 0, 0, 0, 0, 0, 0);
            item.MemoryIndex = memory;
            return item;
        }

        public MM1ItemList()
        {
            // Load the standard MM1 item list
            m_items = new List<MM1Item>(256);
            m_items.Add(MM1Item.Create(MM1ItemIndex.Empty, "<empty>", 255, 0, 0, 0, 0, 0, 0, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Club, "Club", 255, 0, 0, 0, 0, 0, 1, 3, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Dagger, "Dagger", 251, 0, 0, 0, 0, 0, 5, 4, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.HandAxe, "Hand Axe", 249, 0, 0, 0, 0, 0, 10, 5, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Spear, "Spear", 248, 0, 0, 0, 0, 0, 15, 6, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ShortSword, "Short Sword", 249, 0, 0, 0, 0, 0, 20, 6, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Mace, "Mace", 253, 0, 0, 0, 0, 0, 40, 6, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Flail, "Flail", 253, 0, 0, 0, 0, 0, 40, 7, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Scimitar, "Scimitar", 249, 0, 0, 0, 0, 0, 40, 7, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BroadSword, "Broad Sword", 249, 0, 0, 0, 0, 0, 50, 7, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BattleAxe, "Battle Axe", 249, 0, 0, 0, 0, 0, 60, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LongSword, "Long Sword", 249, 0, 0, 0, 0, 0, 60, 8, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ClubPlus1, "Club +1", 255, 0, 0, 0, 0, 0, 30, 3, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ClubPlus2, "Club +2", 255, 0, 0, 0, 0, 0, 100, 3, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DaggerPlus1, "Dagger +1", 251, 0, 0, 0, 0, 0, 50, 4, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.HandAxePlus1, "Hand Axe +1", 121, 33, 1, 0, 0, 0, 75, 5, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SpearPlus1, "Spear +1", 184, 33, 1, 0, 0, 0, 100, 6, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ShortSwordPlus1, "Short Sword +1", 249, 0, 0, 0, 0, 0, 100, 6, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MacePlus1, "Mace +1", 253, 0, 0, 0, 0, 0, 125, 6, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FlailPlus1, "Flail +1", 253, 0, 0, 0, 0, 0, 200, 7, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ScimitarPlus1, "Scimitar +1", 185, 33, 2, 0, 0, 0, 250, 7, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BroadSwordPlus1, "Broad Sword +1", 121, 33, 2, 0, 0, 0, 300, 7, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BattleAxePlus1, "Battle Axe +1", 249, 0, 0, 0, 0, 0, 300, 8, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LongSwordPlus1, "Long Sword +1", 249, 0, 0, 0, 0, 0, 300, 8, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FlamingClub, "Flaming Club", 255, 90, 20, 255, 50, 30, 500, 3, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ClubOfNoise, "Club of Noise", 255, 255, 0, 0, 0, 0, 100, 3, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DaggerPlus2, "Dagger +2", 251, 0, 0, 255, 52, 25, 200, 4, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.HandAxePlus2, "Hand Axe +2", 185, 33, 2, 0, 0, 0, 225, 5, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SpearPlus2, "Spear +2", 120, 33, 2, 0, 0, 0, 250, 6, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ShortSwordPlus2, "Short Sword +2", 249, 0, 0, 255, 48, 15, 300, 6, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MacePlus2, "Mace +2", 253, 25, 1, 255, 7, 10, 325, 6, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FlailPlus2, "Flail +2", 253, 25, 1, 255, 3, 15, 350, 7, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ScimitarPlus2, "Scimitar +2", 121, 23, 1, 0, 0, 0, 400, 7, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BroadSwordPlus2, "Broad Sword +2", 185, 23, 1, 0, 0, 0, 400, 7, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BattleAxePlus2, "Battle Axe +2", 249, 90, 20, 24, 2, 10, 500, 8, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LongSwordPlus2, "Long Sword +2", 249, 96, 20, 24, 2, 10, 550, 8, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RoyalDagger, "Royal Dagger", 59, 0, 0, 0, 0, 0, 2500, 4, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DaggerOfMind, "Dagger of Mind", 194, 21, 3, 255, 77, 20, 750, 4, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DiamondDagger, "Diamond Dagger", 194, 23, 4, 0, 0, 0, 800, 10, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ElectricSpear, "Electric Spear", 248, 94, 40, 255, 55, 16, 1200, 6, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.HolyMace, "Holy Mace", 132, 25, 3, 255, 38, 5, 2000, 6, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.UnHolyMace, "Un-Holy Mace", 68, 25, 3, 255, 37, 5, 2000, 6, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DarkFlail, "Dark Flail", 124, 255, 0, 255, 33, 10, 600, 3, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FlailOfFear, "Flail of Fear", 196, 98, 40, 255, 62, 8, 1600, 7, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LuckyScimitar, "Lucky Scimitar", 249, 33, 5, 0, 0, 0, 2200, 7, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MaceOfUndead, "Mace of Undead", 188, 255, 0, 37, 10, 5, 500, 6, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ColdAxe, "Cold Axe", 240, 92, 40, 255, 72, 10, 2500, 8, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ElectricSword, "Electric Sword", 248, 94, 40, 255, 66, 10, 2200, 8, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FlamingSword, "Flaming Sword", 248, 90, 50, 255, 63, 10, 2200, 8, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SwordOfMight, "Sword of Might", 224, 23, 6, 24, 5, 30, 8000, 8, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SwordOfSpeed, "Sword of Speed", 248, 29, 6, 30, 5, 20, 7000, 8, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SharpSword, "Sharp Sword", 112, 88, 21, 255, 81, 5, 6500, 10, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.AccurateSword, "Accurate Sword", 184, 31, 6, 32, 5, 10, 6500, 8, 6));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SwordOfMagic, "Sword of Magic", 249, 88, 30, 255, 87, 15, 10000, 8, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ImmortalSword, "Immortal Sword", 185, 33, 5, 255, 39, 25, 7000, 8, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.AxeProtector, "Axe Protector", 248, 88, 25, 255, 92, 15, 8000, 8, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.AxeDestroyer, "Axe Destroyer", 112, 23, 4, 255, 85, 6, 8000, 8, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.XXXXsSword, "X!XX!X's Sword", 57, 33, 15, 34, 5, 10, 6000, 8, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.AdamantineAxe, "Adamantine Axe", 248, 33, 8, 255, 36, 5, 12000, 8, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.UltimateSword, "Ultimate Sword", 249, 23, 10, 30, 5, 20, 15000, 20, 6));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ElementSword, "Element Sword", 249, 88, 25, 255, 44, 10, 12000, 8, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Sling, "Sling", 249, 0, 0, 0, 0, 0, 10, 4, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Crossbow, "Crossbow", 249, 0, 0, 0, 0, 0, 50, 6, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ShortBow, "Short Bow", 248, 0, 0, 0, 0, 0, 75, 8, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LongBow, "Long Bow", 248, 0, 0, 0, 0, 0, 100, 10, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GreatBow, "Great Bow", 248, 0, 0, 0, 0, 0, 250, 12, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SlingPlus1, "Sling +1", 249, 0, 0, 0, 0, 0, 50, 4, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.CrossbowPlus1, "Crossbow +1", 249, 0, 0, 0, 0, 0, 250, 6, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ShortBowPlus1, "Short Bow +1", 248, 0, 0, 0, 0, 0, 375, 8, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LongBowPlus1, "Long Bow +1", 248, 0, 0, 0, 0, 0, 500, 10, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GreatBowPlus1, "Great Bow +1", 248, 0, 0, 0, 0, 0, 1250, 12, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MagicSling, "Magic Sling", 249, 88, 10, 89, 20, 10, 800, 4, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.CrossbowPlus2, "Crossbow +2", 201, 31, 2, 0, 0, 0, 1000, 6, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ShortBowPlus2, "Short Bow +2", 120, 102, 10, 0, 0, 0, 1000, 8, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LongBowPlus2, "Long Bow +2", 184, 102, 10, 0, 0, 0, 1200, 10, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GreatBowPlus2, "Great Bow +2", 248, 98, 30, 0, 0, 0, 2000, 12, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.CrossbowLuck, "Crossbow Luck", 201, 33, 3, 255, 1, 20, 2000, 6, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.CrossbowSpeed, "Crossbow Speed", 249, 29, 4, 255, 2, 10, 2000, 6, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LightningBow, "Lightning Bow", 184, 94, 20, 255, 63, 10, 3000, 10, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FlamingBow, "Flaming Bow", 120, 90, 20, 255, 66, 10, 3000, 10, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GiantsBow, "Giant's Bow", 248, 0, 0, 0, 0, 0, 2000, 20, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.TheMagicBow, "The Magic Bow", 184, 88, 20, 255, 83, 5, 6000, 16, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BowOfPower, "Bow of Power", 120, 98, 40, 36, 4, 15, 6000, 16, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RobbersXBow, "Robber's X-Bow", 193, 29, 4, 255, 61, 10, 8000, 10, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ArchersBow, "Archer's Bow", 200, 31, 5, 255, 85, 10, 12000, 20, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ObsidianBow, "Obsidian Bow", 255, 255, 0, 255, 80, 3, 2000, 3, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Staff, "Staff", 254, 0, 0, 0, 0, 0, 30, 8, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Glaive, "Glaive", 248, 0, 0, 0, 0, 0, 80, 10, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Bardiche, "Bardiche", 248, 0, 0, 0, 0, 0, 80, 10, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Halberd, "Halberd", 248, 0, 0, 0, 0, 0, 100, 12, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GreatHammer, "Great Hammer", 252, 0, 0, 0, 0, 0, 150, 12, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GreatAxe, "Great Axe", 248, 0, 0, 0, 0, 0, 150, 12, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Flamberge, "Flamberge", 248, 0, 0, 0, 0, 0, 250, 14, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.StaffPlus1, "Staff +1", 254, 21, 1, 0, 0, 0, 200, 8, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GlaivePlus1, "Glaive +1", 120, 29, 1, 0, 0, 0, 350, 10, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BardichePlus1, "Bardiche +1", 184, 29, 1, 0, 0, 0, 350, 10, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.HalberdPlus1, "Halberd +1", 248, 0, 0, 0, 0, 0, 500, 12, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GreatHammerPlus1, "Great Hammer+1", 252, 25, 1, 0, 0, 0, 550, 12, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GreatAxePlus1, "Great Axe +1", 248, 0, 0, 0, 0, 0, 500, 12, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FlambergePlus1, "Flamberge +1", 248, 0, 0, 0, 0, 0, 600, 14, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.StaffPlus2, "Staff +2", 254, 33, 2, 255, 54, 10, 600, 8, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GlaivePlus2, "Glaive +2", 120, 29, 2, 0, 0, 0, 900, 10, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BardichePlus2, "Bardiche +2", 184, 29, 2, 0, 0, 0, 900, 10, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.HalberdPlus2, "Halberd +2", 248, 29, 3, 255, 3, 20, 1200, 12, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GreatHammerPlus2, "Great Hammer+2", 252, 25, 2, 255, 1, 20, 1200, 12, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GreatAxePlus2, "Great Axe +2", 248, 23, 2, 24, 2, 10, 1200, 12, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FlambergePlus2, "Flamberge +2", 248, 23, 2, 24, 2, 10, 2000, 14, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.StaffOfLight, "Staff of Light", 254, 102, 40, 255, 19, 20, 1500, 8, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ColdGlaive, "Cold Glaive", 120, 92, 40, 255, 21, 20, 2500, 10, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.CuringStaff, "Curing Staff", 134, 100, 30, 255, 5, 12, 2500, 8, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MinotaursAxe, "Minotaur's Axe", 248, 255, 0, 0, 0, 0, 2000, 3, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ThunderHammer, "Thunder Hammer", 196, 94, 40, 255, 29, 15, 3500, 12, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GreatAxePlus3, "Great Axe +3", 248, 23, 4, 30, 3, 10, 3500, 12, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FlambergePlus3, "Flamberge +3", 248, 23, 4, 30, 3, 10, 5000, 14, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SorcererStaff, "Sorcerer Staff", 194, 21, 4, 255, 91, 10, 8000, 8, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.StaffOfMagic, "Staff of Magic", 254, 88, 25, 255, 87, 10, 5000, 8, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DemonsGlaive, "Demon's Glaive", 40, 96, 50, 255, 71, 40, 10000, 10, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DevilsGlaive, "Devil's Glaive", 40, 92, 50, 255, 72, 40, 10000, 10, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.TheFlamberge, "The Flamberge", 248, 23, 10, 255, 73, 10, 15000, 30, 6));
            m_items.Add(MM1Item.Create(MM1ItemIndex.HolyFlamberge, "Holy Flamberge", 144, 88, 50, 255, 43, 15, 20000, 20, 6));
            m_items.Add(MM1Item.Create(MM1ItemIndex.EvilFlamberge, "Evil Flamberge", 80, 88, 50, 255, 46, 15, 20000, 20, 6));
            m_items.Add(MM1Item.Create(MM1ItemIndex.PaddedArmor, "Padded Armor", 255, 0, 0, 0, 0, 0, 10, 0, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LeatherArmor, "Leather Armor", 253, 0, 0, 0, 0, 0, 20, 0, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ScaleArmor, "Scale Armor", 253, 0, 0, 0, 0, 0, 50, 0, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RingMail, "Ring Mail", 253, 0, 0, 0, 0, 0, 100, 0, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ChainMail, "Chain Mail", 252, 0, 0, 0, 0, 0, 200, 0, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SplintMail, "Splint Mail", 240, 0, 0, 0, 0, 0, 400, 0, 6));
            m_items.Add(MM1Item.Create(MM1ItemIndex.PlateMail, "Plate Mail", 240, 0, 0, 0, 0, 0, 1000, 0, 7));
            m_items.Add(MM1Item.Create(MM1ItemIndex.PaddedArmorPlus1, "Padded Armor +1", 255, 0, 0, 0, 0, 0, 25, 0, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LeatherArmorPlus1, "Leather Armor +1", 253, 0, 0, 0, 0, 0, 60, 0, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ScaleArmorPlus1, "Scale Armor +1", 253, 0, 0, 0, 0, 0, 120, 0, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RingMailPlus1, "Ring Mail +1", 253, 90, 5, 0, 0, 0, 250, 0, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ChainMailPlus1, "Chain Mail +1", 252, 90, 5, 0, 0, 0, 500, 0, 6));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SplintMailPlus1, "Splint Mail +1", 240, 90, 10, 0, 0, 0, 1000, 0, 7));
            m_items.Add(MM1Item.Create(MM1ItemIndex.PlateMailPlus1, "Plate Mail +1", 240, 90, 10, 0, 0, 0, 2500, 0, 8));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LeatherArmorPlus2, "Leather Armor +2", 253, 94, 10, 0, 0, 0, 150, 0, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ScaleArmorPlus2, "Scale Armor +2", 253, 92, 10, 0, 0, 0, 300, 0, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RingMailPlus2, "Ring Mail +2", 253, 90, 15, 0, 0, 0, 750, 0, 6));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ChainMailPlus2, "Chain Mail +2", 252, 90, 15, 0, 0, 0, 1500, 0, 7));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SplintMailPlus2, "Splint Mail +2", 240, 90, 20, 0, 0, 0, 2500, 0, 8));
            m_items.Add(MM1Item.Create(MM1ItemIndex.PlateMailPlus2, "Plate Mail +2", 240, 90, 20, 0, 0, 0, 7500, 0, 9));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BracersAC4, "Bracers AC 4", 203, 0, 0, 0, 0, 0, 1000, 0, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RingMailPlus3, "Ring Mail +3", 253, 29, 2, 0, 0, 0, 2000, 0, 7));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ChainMailPlus3, "Chain Mail +3", 252, 33, 4, 0, 0, 0, 4500, 0, 8));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SplintMailPlus3, "Splint Mail +3", 240, 23, 2, 0, 0, 0, 7500, 0, 9));
            m_items.Add(MM1Item.Create(MM1ItemIndex.PlateMailPlus3, "Plate Mail +3", 240, 90, 50, 0, 0, 0, 15000, 0, 10));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BracersAC6, "Bracers AC 6", 203, 98, 20, 255, 77, 20, 2500, 0, 6));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ChainMailPlus3Cursed, "Chain Mail +3", 252, 255, 0, 0, 0, 0, 4500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BracersAC8Cursed, "Bracers AC 8", 255, 255, 0, 0, 0, 0, 7500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BlueRingMail, "Blue Ring Mail", 253, 94, 60, 255, 66, 30, 10000, 0, 9));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RedChainMail, "Red Chain Mail", 252, 90, 60, 255, 63, 30, 15000, 0, 10));
            m_items.Add(MM1Item.Create(MM1ItemIndex.XXXXsPlate, "X!XX!X's Plate", 48, 33, 10, 34, 5, 10, 18000, 0, 11));
            m_items.Add(MM1Item.Create(MM1ItemIndex.HolyPlate, "Holy Plate", 144, 88, 40, 99, 50, 30, 25000, 0, 12));
            m_items.Add(MM1Item.Create(MM1ItemIndex.UnHolyPlate, "Un-Holy Plate", 80, 88, 40, 99, 50, 30, 25000, 0, 12));
            m_items.Add(MM1Item.Create(MM1ItemIndex.UltimatePlate, "Ultimate Plate", 234, 88, 40, 255, 49, 30, 30000, 0, 13));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BracersAC8, "Bracers AC 8", 203, 98, 60, 255, 77, 40, 7500, 0, 8));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SmallShield, "Small Shield", 245, 0, 0, 0, 0, 0, 10, 0, 1));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LargeShield, "Large Shield", 245, 0, 0, 0, 0, 0, 50, 0, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SilverShield, "Silver Shield", 245, 102, 20, 0, 0, 0, 100, 0, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SmallShieldPlus1, "Small Shield +1", 245, 0, 0, 0, 0, 0, 100, 0, 2));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LargeShieldPlus1, "Large Shield +1", 245, 0, 0, 0, 0, 0, 200, 0, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LargeShieldPlus1Cursed, "Large Shield +1", 245, 255, 0, 0, 0, 0, 200, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SmallShieldPlus2, "Small Shield +2", 245, 0, 0, 0, 0, 0, 400, 0, 3));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LargeShieldPlus2, "Large Shield +2", 245, 0, 0, 0, 0, 0, 800, 0, 4));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LargeShieldPlus2Cursed, "Large Shield +2", 245, 255, 0, 0, 0, 0, 800, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FireShield, "Fire Shield", 245, 90, 20, 0, 0, 0, 2500, 0, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ColdShield, "Cold Shield", 245, 92, 20, 0, 0, 0, 2500, 0, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ElectricShield, "Electric Shield", 245, 94, 20, 0, 0, 0, 2500, 0, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.AcidShield, "Acid Shield", 245, 96, 20, 0, 0, 0, 2500, 0, 5));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MagicShield, "Magic Shield", 245, 88, 20, 255, 77, 20, 5000, 0, 6));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DragonShield, "Dragon Shield", 245, 88, 10, 255, 92, 20, 8000, 0, 7));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RopeAndHooks, "Rope & Hooks", 255, 1, 0, 255, 58, 30, 10, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Torch, "Torch", 255, 1, 0, 255, 4, 1, 2, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Lantern, "Lantern", 255, 1, 0, 255, 4, 10, 20, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.TenFootPole, "10 Foot Pole", 255, 1, 0, 0, 0, 0, 10, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Garlic, "Garlic", 255, 1, 0, 0, 0, 0, 5, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Wolfsbane, "Wolfsbane", 255, 1, 0, 0, 0, 0, 10, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Belladonna, "Belladonna", 255, 1, 0, 0, 0, 0, 25, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MagicHerbs, "Magic Herbs", 255, 1, 0, 255, 3, 3, 50, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DriedBeef, "Dried Beef", 255, 1, 0, 62, 6, 3, 40, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RobbersTools, "Robber's Tools", 193, 108, 20, 0, 0, 0, 150, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BagOfSilver, "Bag of Silver", 255, 1, 0, 0, 0, 0, 300, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.AmberGem, "Amber Gem", 255, 1, 0, 0, 0, 0, 500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SmellingSalt, "Smelling Salt", 255, 1, 0, 255, 0, 3, 50, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BagOfSand, "Bag of Sand", 255, 1, 0, 255, 54, 5, 100, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MightPotion, "Might Potion", 255, 1, 0, 24, 5, 3, 200, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SpeedPotion, "Speed Potion", 255, 1, 0, 30, 5, 3, 200, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Sundial, "Sundial", 255, 1, 0, 255, 53, 50, 500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.CuringPotion, "Curing Potion", 255, 1, 0, 255, 8, 4, 350, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MagicPotion, "Magic Potion", 255, 1, 0, 43, 10, 2, 500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DefenseRing, "Defense Ring", 255, 60, 1, 255, 57, 30, 500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BagOfGarbage, "Bag of Garbage", 255, 255, 0, 0, 0, 0, 100, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ScrollOfFire, "Scroll of Fire", 255, 1, 0, 255, 63, 1, 300, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FlyingCarpet, "Flying Carpet", 194, 60, 2, 255, 64, 10, 500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.JadeAmulet, "Jade Amulet", 0, 19, 5, 0, 0, 0, 600, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.AntidoteBrew, "Antidote Brew", 255, 1, 0, 255, 25, 2, 500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SkillPotion, "Skill Potion", 255, 1, 0, 36, 5, 5, 600, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BootsOfSpeed, "Boots of Speed", 255, 29, 5, 30, 10, 10, 800, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LuckyCharm, "Lucky Charm", 255, 33, 5, 34, 10, 20, 800, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.WandOfFire, "Wand of Fire", 202, 90, 15, 255, 63, 10, 1000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.UndeadAmulet, "Undead Amulet", 255, 98, 50, 255, 7, 20, 800, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SilentChime, "Silent Chime", 255, 1, 0, 255, 14, 20, 400, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BeltOfPower, "Belt of Power", 241, 23, 5, 0, 0, 0, 600, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ModelBoat, "Model Boat", 255, 1, 0, 255, 23, 15, 400, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DefenseCloak, "Defense Cloak", 255, 60, 2, 0, 0, 0, 700, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.KnowledgeBook, "Knowledge Book", 222, 21, 2, 48, 1, 4, 1000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RubyIdol, "Ruby Idol", 255, 1, 0, 0, 0, 0, 3000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SorcererRobe, "Sorcerer Robe", 194, 21, 5, 255, 65, 20, 2500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.PowerGauntlet, "Power Gauntlet", 253, 23, 5, 0, 0, 0, 3000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ClericsBeads, "Cleric's Beads", 196, 25, 5, 255, 8, 50, 3000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.HornOfDeath, "Horn of Death", 255, 1, 0, 255, 81, 10, 2500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.PotionOfLife, "Potion of Life", 255, 1, 0, 255, 38, 2, 1500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ShinyPendant, "Shiny Pendant", 255, 102, 30, 255, 56, 10, 2000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LightningWand, "Lightning Wand", 205, 94, 20, 255, 66, 10, 1500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.PrecisionRing, "Precision Ring", 255, 31, 5, 0, 0, 0, 3000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ReturnScroll, "Return Scroll", 255, 1, 0, 255, 41, 1, 2000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.TeleportHelm, "Teleport Helm", 255, 88, 10, 255, 83, 20, 5000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.YouthPotion, "Youth Potion", 255, 1, 0, 255, 39, 2, 4000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BellsOfTime, "Bells of Time", 255, 1, 0, 37, 10, 50, 1000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MagicOil, "Magic Oil", 255, 1, 0, 255, 88, 1, 3000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MagicVest, "Magic Vest", 255, 88, 20, 255, 78, 10, 6000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DestroyerWand, "Destroyer Wand", 202, 88, 10, 255, 85, 10, 7000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.ElementScarab, "Element Scarab", 255, 25, 5, 255, 44, 20, 6000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SunScroll, "Sun Scroll", 255, 1, 0, 255, 46, 1, 3000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.StarRuby, "Star Ruby", 255, 33, 10, 255, 49, 30, 6000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.StarSapphire, "Star Sapphire", 255, 88, 30, 255, 87, 10, 6000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.WealthChest, "Wealth Chest", 255, 1, 0, 58, 20, 5, 6000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GemSack, "Gem Sack", 255, 1, 0, 49, 10, 10, 10000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DiamondCollar, "Diamond Collar", 255, 37, 80, 255, 93, 10, 10000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.FireOpal, "Fire Opal", 255, 37, 80, 255, 91, 10, 10000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Unobtainium, "Unobtainium", 0, 16, 5, 0, 0, 0, 50000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.VellumScroll, "Vellum Scroll", 255, 1, 0, 0, 0, 0, 10, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RubyWhistle, "Ruby Whistle", 255, 33, 2, 255, 0, 200, 500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.KingsPass, "Kings Pass", 255, 1, 0, 0, 0, 0, 0, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MerchantsPass, "Merchants Pass", 255, 1, 0, 0, 0, 0, 0, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.CrystalKey, "Crystal Key", 255, 1, 0, 255, 93, 10, 1000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.CoralKey, "Coral Key", 255, 1, 0, 255, 23, 10, 300, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BronzeKey, "Bronze Key", 255, 1, 0, 255, 48, 20, 500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.SilverKey, "Silver Key", 255, 1, 0, 255, 51, 30, 600, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.GoldKey, "Gold Key", 255, 1, 0, 255, 65, 15, 800, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DiamondKey, "Diamond Key", 255, 1, 0, 255, 83, 20, 2000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.CactusNectar, "Cactus Nectar", 255, 1, 0, 255, 16, 10, 400, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MapOfDesert, "Map of Desert", 255, 1, 0, 255, 53, 20, 400, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.LaserBlaster, "Laser Blaster", 255, 31, 5, 255, 85, 10, 2000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.DragonsTooth, "Dragons Tooth", 255, 1, 0, 255, 39, 10, 1500, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.WyvernEye, "Wyvern Eye", 255, 1, 0, 255, 62, 20, 1000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.MedusaHead, "Medusa Head", 255, 255, 0, 0, 0, 0, 0, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.RingOfOkrim, "Ring of Okrim", 255, 33, 10, 255, 78, 20, 3000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.BlackQueenIdol, "Black Queen Idol", 255, 1, 0, 0, 0, 0, 0, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.WhiteQueenIdol, "White Queen Idol", 255, 1, 0, 0, 0, 0, 0, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.PiratesMapA, "Pirates Map A", 255, 1, 0, 0, 0, 0, 1000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.PiratesMapB, "Pirates Map B", 255, 1, 0, 0, 0, 0, 2000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.Thundranium, "Thundranium", 255, 1, 0, 24, 15, 250, 10000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.KeyCard, "Key Card", 255, 1, 0, 0, 0, 0, 0, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.EyeOfGoros, "Eye of Goros", 255, 1, 0, 255, 89, 20, 10000, 0, 0));
            m_items.Add(MM1Item.Create(MM1ItemIndex.UselessItem, "(Useless Item)", 255, 1, 0, 0, 0, 0, 0, 0, 0));
            m_bValid = true;
        }
    }

    [Flags]
    public enum MM1UsableByFlags
    {
        None = 0x00,
        Robber = 0x01,
        Sorcerer = 0x02,
        Cleric = 0x04,
        Archer = 0x08,
        Paladin = 0x10,
        Knight = 0x20,
        Evil = 0x40,
        Good = 0x80,
        AnyClass = Robber | Sorcerer | Cleric | Archer | Paladin | Knight,
        AnyAlignment = Evil | Good,
        Neutral = 0x00,
        Anyone = 0xff
    }

    public enum MM1ItemType
    {
        None,
        OneHandedMelee,
        TwoHandedMelee,
        Missile,
        Armor,
        Shield,
        Miscellaneous
    }

    public class MM1Item : MMItem
    {
        public override GameNames Game { get { return GameNames.MightAndMagic1; } }
        public MM1UsableByFlags UsableBy;
        public byte EquipAttribute;             // Offset into the character record that the bonus increases
        public byte EquipAttributeBonus;
        public byte UseAttribute;
        public byte UseAttributeBonus;
        public byte Charges;
        public byte DamageByte;
        public byte Extra;
        private ushort m_iValue;
        private int m_base;

        public override int Index
        {
            get { return m_base; }
            set { m_base = value; }
        }

        public static MM1Item FromBagBytes(byte[] bytes)
        {
            MM1Item item = MM1.Items[bytes[0]].Clone() as MM1Item;
            item.m_iChargesCurrent = bytes[1];
            // No use for bytes[2] for MM1
            return item;
        }

        public override int GetHashCode()
        {
            return Index | (Charges << 8);
        }

        public override string DescriptionString { get { return Name; } }
        public override long Value { get { return m_iValue; } set { m_iValue = (ushort) value; } }

        public override string TypeString
        {
            get
            {
                switch (GetItemType(Index))
                {
                    case MM1ItemType.Armor: return "Armor";
                    case MM1ItemType.OneHandedMelee: return "1H Weapon";
                    case MM1ItemType.TwoHandedMelee: return "2H Weapon";
                    case MM1ItemType.Missile: return "Missile";
                    case MM1ItemType.Shield: return "Shield";
                    case MM1ItemType.Miscellaneous: return EquipAttribute < 2 ? "Misc" : "Accessory";
                    default: return string.Empty;
                }
            }
        }

        public override bool Trashable
        {
            get
            {
                switch ((MM1ItemIndex)Index)
                {
                    case MM1ItemIndex.Garlic:
                    case MM1ItemIndex.Wolfsbane:
                    case MM1ItemIndex.Belladonna:
                    case MM1ItemIndex.VellumScroll:
                    case MM1ItemIndex.RubyWhistle:
                    case MM1ItemIndex.KingsPass:
                    case MM1ItemIndex.MerchantsPass:
                    case MM1ItemIndex.CrystalKey:
                    case MM1ItemIndex.CoralKey:
                    case MM1ItemIndex.BronzeKey:
                    case MM1ItemIndex.SilverKey:
                    case MM1ItemIndex.GoldKey:
                    case MM1ItemIndex.DiamondKey:
                    case MM1ItemIndex.CactusNectar:
                    case MM1ItemIndex.MapOfDesert:
                    case MM1ItemIndex.DragonsTooth:
                    case MM1ItemIndex.WyvernEye:
                    case MM1ItemIndex.MedusaHead:
                    case MM1ItemIndex.RingOfOkrim:
                    case MM1ItemIndex.BlackQueenIdol:
                    case MM1ItemIndex.WhiteQueenIdol:
                    case MM1ItemIndex.PiratesMapA:
                    case MM1ItemIndex.PiratesMapB:
                    case MM1ItemIndex.KeyCard:
                    case MM1ItemIndex.EyeOfGoros: 
                        return false;
                    default:
                        return true;
                }
            }
        }

        public override string MultiLineDescription
        {
            get
            {
                MM1ItemType type = GetItemType(Index);
                string strType = GetItemTypeString(type, true);

                StringBuilder sbUsableClass = new StringBuilder("Usable by class: ");
                if (UsableBy.HasFlag(MM1UsableByFlags.AnyClass))
                    sbUsableClass.Append("ANY");
                else
                {
                    if (UsableBy.HasFlag(MM1UsableByFlags.Knight))
                        sbUsableClass.Append("Knight, ");
                    if (UsableBy.HasFlag(MM1UsableByFlags.Paladin))
                        sbUsableClass.Append("Paladin, ");
                    if (UsableBy.HasFlag(MM1UsableByFlags.Archer))
                        sbUsableClass.Append("Archer, ");
                    if (UsableBy.HasFlag(MM1UsableByFlags.Cleric))
                        sbUsableClass.Append("Cleric, ");
                    if (UsableBy.HasFlag(MM1UsableByFlags.Sorcerer))
                        sbUsableClass.Append("Sorcerer, ");
                    if (UsableBy.HasFlag(MM1UsableByFlags.Robber))
                        sbUsableClass.Append("Robber, ");
                    if (Global.Trim(sbUsableClass).Length == 0)
                        sbUsableClass.Append("NONE");
                }

                StringBuilder sbUsableAlign = new StringBuilder("Usable by alignment: ");
                if (UsableBy.HasFlag(MM1UsableByFlags.AnyAlignment))
                    sbUsableAlign.Append("ANY");
                else
                {
                    if (UsableBy.HasFlag(MM1UsableByFlags.Good))
                        sbUsableAlign.Append("Good, ");
                    if (UsableBy.HasFlag(MM1UsableByFlags.Evil))
                        sbUsableAlign.Append("Evil, ");
                    if (!UsableBy.HasFlag(MM1UsableByFlags.Good) && !UsableBy.HasFlag(MM1UsableByFlags.Evil))
                        sbUsableAlign.Append("Neutral, ");
                }
                Global.Trim(sbUsableAlign);

                string strDamageAC = "";
                switch (type)
                {
                    case MM1ItemType.Armor:
                    case MM1ItemType.Shield:
                        strDamageAC = String.Format("Armor Class: {0}", Extra);
                        break;
                    case MM1ItemType.Missile:
                    case MM1ItemType.OneHandedMelee:
                    case MM1ItemType.TwoHandedMelee:
                        if (Extra > 0)
                            strDamageAC = String.Format("Damage: 1d{0}+{1}", DamageByte, Extra);
                        else
                            strDamageAC = String.Format("Damage: 1d{0}", DamageByte);
                        break;
                    default:
                        break;
                }

                string strEquip = EquipStringFull;
                string strUse = UseStringFull;

                StringBuilder sbFull = new StringBuilder();
                sbFull.AppendLine(Name);
                sbFull.AppendFormat("Type: {0}\r\n", strType);
                if (EquipAttribute != 1)
                    sbFull.AppendFormat("{0}\r\n{1}\r\n", sbUsableClass.ToString(), sbUsableAlign.ToString());
                if (!String.IsNullOrEmpty(strDamageAC))
                    sbFull.AppendLine(strDamageAC);
                if (EquipAttribute > 1)
                    sbFull.AppendFormat("Equip: {0}\r\n", strEquip);
                sbFull.AppendFormat("Use: {0}\r\n", strUse);
                sbFull.AppendFormat("Value: {0} gold\r\n", Value);
                sbFull.AppendFormat("Duplicatable: {0}\r\n", Duplicatable ? "Yes" : "No");

                return sbFull.ToString();
            }
        }

        public override bool Duplicatable { get { return Index < 230; } }

        public override ItemType Type
        {
            get
            {
                switch (GetItemType(Index))
                {
                    case MM1ItemType.Armor:
                    case MM1ItemType.Shield:
                        return ItemType.Armor;
                    case MM1ItemType.Missile: return ItemType.Missile;
                    case MM1ItemType.OneHandedMelee: return ItemType.OneHandMelee;
                    case MM1ItemType.TwoHandedMelee: return ItemType.TwoHandMelee;
                    case MM1ItemType.Miscellaneous: return EquipAttribute < 2 ? ItemType.Miscellaneous : ItemType.Accessory;
                    default: return ItemType.None;
                }
            }
            set {} 
        }

        public override bool IsWeapon
        {
            get 
            {
                switch (GetItemType(Index))
                {
                    case MM1ItemType.Missile:
                    case MM1ItemType.OneHandedMelee:
                    case MM1ItemType.TwoHandedMelee:
                        return true;
                    default:
                        return false;
                }
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

        public override CompareResult CompareTo(Item item)
        {
            if (!(item is MM1Item))
                return CompareResult.Uncomparable;
            if (item == this)
                return CompareResult.Identical;
            if (Type != item.Type)
                return CompareResult.Uncomparable;

            MM1Item mm1Item = item as MM1Item;

            if (mm1Item.Cursed && !Cursed)
                return CompareResult.Better;     // Cursed is always worse than non-cursed
            else if (!mm1Item.Cursed && Cursed)
                return CompareResult.Worse;    // ... and vice-versa

            if (EquipAttribute != mm1Item.EquipAttribute && EquipAttribute != 0 && mm1Item.EquipAttribute != 0)
                return CompareResult.Uncomparable;
            if (CanEquipLocation != mm1Item.CanEquipLocation)
                return CompareResult.Uncomparable;

            switch (GetItemType(Index))
            {
                case MM1ItemType.Armor:
                case MM1ItemType.Shield:
                    return CompareValues(EquipBonusValue, mm1Item.EquipBonusValue, ArmorClass, mm1Item.ArmorClass);
                case MM1ItemType.Missile:
                case MM1ItemType.OneHandedMelee:
                case MM1ItemType.TwoHandedMelee:
                    return CompareValues((DamageByte + 1) / 2.0 + Extra, (mm1Item.DamageByte + 1) / 2.0 + mm1Item.Extra, EquipBonusValue, mm1Item.EquipBonusValue);
                default:
                    if (CanEquip && mm1Item.CanEquip)
                        return CompareValues(EquipBonusValue, mm1Item.EquipBonusValue, ArmorClass, mm1Item.ArmorClass);
                    break;
            }
            return CompareResult.Uncomparable;
        }

        public override ItemNounType ItemBasicType
        {
            get
            {
                switch ((MM1ItemIndex)Index)
                {
                    case MM1ItemIndex.Club:
                    case MM1ItemIndex.ClubPlus1:
                    case MM1ItemIndex.ClubPlus2:
                    case MM1ItemIndex.FlamingClub:
                    case MM1ItemIndex.ClubOfNoise: return ItemNounType.Club;
                    case MM1ItemIndex.Dagger:
                    case MM1ItemIndex.DaggerPlus1:
                    case MM1ItemIndex.DaggerPlus2:
                    case MM1ItemIndex.RoyalDagger:
                    case MM1ItemIndex.DaggerOfMind:
                    case MM1ItemIndex.DiamondDagger: return ItemNounType.Dagger;
                    case MM1ItemIndex.HandAxe:
                    case MM1ItemIndex.BattleAxe:
                    case MM1ItemIndex.HandAxePlus1:
                    case MM1ItemIndex.BattleAxePlus1:
                    case MM1ItemIndex.HandAxePlus2:
                    case MM1ItemIndex.BattleAxePlus2:
                    case MM1ItemIndex.ColdAxe:
                    case MM1ItemIndex.AxeProtector:
                    case MM1ItemIndex.AdamantineAxe:
                    case MM1ItemIndex.GreatAxe:
                    case MM1ItemIndex.GreatAxePlus1:
                    case MM1ItemIndex.GreatAxePlus2:
                    case MM1ItemIndex.MinotaursAxe:
                    case MM1ItemIndex.GreatAxePlus3:
                    case MM1ItemIndex.AxeDestroyer: return ItemNounType.Axe;
                    case MM1ItemIndex.Spear:
                    case MM1ItemIndex.SpearPlus1:
                    case MM1ItemIndex.SpearPlus2:
                    case MM1ItemIndex.ElectricSpear: return ItemNounType.Spear;
                    case MM1ItemIndex.ShortSword:
                    case MM1ItemIndex.BroadSword:
                    case MM1ItemIndex.LongSword:
                    case MM1ItemIndex.ShortSwordPlus1:
                    case MM1ItemIndex.BroadSwordPlus1:
                    case MM1ItemIndex.LongSwordPlus1:
                    case MM1ItemIndex.ShortSwordPlus2:
                    case MM1ItemIndex.BroadSwordPlus2:
                    case MM1ItemIndex.LongSwordPlus2:
                    case MM1ItemIndex.ElectricSword:
                    case MM1ItemIndex.FlamingSword:
                    case MM1ItemIndex.SwordOfMight:
                    case MM1ItemIndex.SwordOfSpeed:
                    case MM1ItemIndex.SharpSword:
                    case MM1ItemIndex.AccurateSword:
                    case MM1ItemIndex.SwordOfMagic:
                    case MM1ItemIndex.ImmortalSword:
                    case MM1ItemIndex.XXXXsSword:
                    case MM1ItemIndex.UltimateSword:
                    case MM1ItemIndex.ElementSword: return ItemNounType.Sword;
                    case MM1ItemIndex.Mace:
                    case MM1ItemIndex.MacePlus1:
                    case MM1ItemIndex.MacePlus2:
                    case MM1ItemIndex.HolyMace:
                    case MM1ItemIndex.UnHolyMace:
                    case MM1ItemIndex.MaceOfUndead: return ItemNounType.Mace;
                    case MM1ItemIndex.Flail:
                    case MM1ItemIndex.FlailPlus1:
                    case MM1ItemIndex.FlailPlus2:
                    case MM1ItemIndex.DarkFlail: return ItemNounType.Flail;
                    case MM1ItemIndex.FlailOfFear:
                    case MM1ItemIndex.Scimitar:
                    case MM1ItemIndex.ScimitarPlus1:
                    case MM1ItemIndex.ScimitarPlus2:
                    case MM1ItemIndex.LuckyScimitar: return ItemNounType.Scimitar;
                    case MM1ItemIndex.Sling:
                    case MM1ItemIndex.SlingPlus1:
                    case MM1ItemIndex.MagicSling: return ItemNounType.Sling;
                    case MM1ItemIndex.Crossbow:
                    case MM1ItemIndex.CrossbowPlus1:
                    case MM1ItemIndex.CrossbowPlus2:
                    case MM1ItemIndex.CrossbowLuck:
                    case MM1ItemIndex.CrossbowSpeed: return ItemNounType.Crossbow;
                    case MM1ItemIndex.ShortBow:
                    case MM1ItemIndex.ShortBowPlus1:
                    case MM1ItemIndex.ShortBowPlus2:
                    case MM1ItemIndex.LongBow:
                    case MM1ItemIndex.LongBowPlus1:
                    case MM1ItemIndex.GreatBow:
                    case MM1ItemIndex.GreatBowPlus1:
                    case MM1ItemIndex.GreatBowPlus2:
                    case MM1ItemIndex.LongBowPlus2:
                    case MM1ItemIndex.LightningBow:
                    case MM1ItemIndex.FlamingBow:
                    case MM1ItemIndex.GiantsBow:
                    case MM1ItemIndex.TheMagicBow:
                    case MM1ItemIndex.BowOfPower:
                    case MM1ItemIndex.RobbersXBow:
                    case MM1ItemIndex.ArchersBow:
                    case MM1ItemIndex.ObsidianBow: return ItemNounType.Bow;
                    case MM1ItemIndex.Staff:
                    case MM1ItemIndex.StaffPlus1:
                    case MM1ItemIndex.StaffPlus2:
                    case MM1ItemIndex.StaffOfLight:
                    case MM1ItemIndex.CuringStaff:
                    case MM1ItemIndex.SorcererStaff:
                    case MM1ItemIndex.StaffOfMagic: return ItemNounType.Staff;
                    case MM1ItemIndex.Glaive:
                    case MM1ItemIndex.GlaivePlus1:
                    case MM1ItemIndex.GlaivePlus2:
                    case MM1ItemIndex.ColdGlaive:
                    case MM1ItemIndex.DemonsGlaive:
                    case MM1ItemIndex.DevilsGlaive: return ItemNounType.Glaive;
                    case MM1ItemIndex.Bardiche:
                    case MM1ItemIndex.BardichePlus1:
                    case MM1ItemIndex.BardichePlus2: return ItemNounType.Bardiche;
                    case MM1ItemIndex.Halberd:
                    case MM1ItemIndex.HalberdPlus1:
                    case MM1ItemIndex.HalberdPlus2: return ItemNounType.Halberd;
                    case MM1ItemIndex.GreatHammer:
                    case MM1ItemIndex.GreatHammerPlus1:
                    case MM1ItemIndex.GreatHammerPlus2:
                    case MM1ItemIndex.ThunderHammer: return ItemNounType.Hammer;
                    case MM1ItemIndex.Flamberge:
                    case MM1ItemIndex.FlambergePlus1:
                    case MM1ItemIndex.FlambergePlus2:
                    case MM1ItemIndex.FlambergePlus3:
                    case MM1ItemIndex.TheFlamberge:
                    case MM1ItemIndex.HolyFlamberge:
                    case MM1ItemIndex.EvilFlamberge: return ItemNounType.Flamberge;
                    case MM1ItemIndex.PaddedArmor:
                    case MM1ItemIndex.PaddedArmorPlus1: return ItemNounType.PaddedArmor;
                    case MM1ItemIndex.LeatherArmor:
                    case MM1ItemIndex.LeatherArmorPlus1:
                    case MM1ItemIndex.LeatherArmorPlus2: return ItemNounType.LeatherArmor;
                    case MM1ItemIndex.ScaleArmor:
                    case MM1ItemIndex.ScaleArmorPlus1:
                    case MM1ItemIndex.ScaleArmorPlus2: return ItemNounType.ScaleArmor;
                    case MM1ItemIndex.RingMail:
                    case MM1ItemIndex.RingMailPlus1:
                    case MM1ItemIndex.RingMailPlus2:
                    case MM1ItemIndex.RingMailPlus3:
                    case MM1ItemIndex.BlueRingMail: return ItemNounType.RingMail;
                    case MM1ItemIndex.ChainMail:
                    case MM1ItemIndex.ChainMailPlus1:
                    case MM1ItemIndex.ChainMailPlus2:
                    case MM1ItemIndex.ChainMailPlus3:
                    case MM1ItemIndex.ChainMailPlus3Cursed:
                    case MM1ItemIndex.RedChainMail: return ItemNounType.ChainMail;
                    case MM1ItemIndex.SplintMail:
                    case MM1ItemIndex.SplintMailPlus1:
                    case MM1ItemIndex.SplintMailPlus2:
                    case MM1ItemIndex.SplintMailPlus3: return ItemNounType.SplintMail;
                    case MM1ItemIndex.PlateMail:
                    case MM1ItemIndex.PlateMailPlus1:
                    case MM1ItemIndex.PlateMailPlus2:
                    case MM1ItemIndex.PlateMailPlus3:
                    case MM1ItemIndex.XXXXsPlate:
                    case MM1ItemIndex.HolyPlate:
                    case MM1ItemIndex.UnHolyPlate:
                    case MM1ItemIndex.UltimatePlate: return ItemNounType.PlateMail;
                    case MM1ItemIndex.BracersAC4:
                    case MM1ItemIndex.BracersAC6:
                    case MM1ItemIndex.BracersAC8:
                    case MM1ItemIndex.BracersAC8Cursed: return ItemNounType.Bracers;
                    case MM1ItemIndex.SmallShield:
                    case MM1ItemIndex.LargeShield:
                    case MM1ItemIndex.SilverShield:
                    case MM1ItemIndex.SmallShieldPlus1:
                    case MM1ItemIndex.LargeShieldPlus1:
                    case MM1ItemIndex.LargeShieldPlus1Cursed:
                    case MM1ItemIndex.SmallShieldPlus2:
                    case MM1ItemIndex.LargeShieldPlus2:
                    case MM1ItemIndex.LargeShieldPlus2Cursed:
                    case MM1ItemIndex.FireShield:
                    case MM1ItemIndex.ColdShield:
                    case MM1ItemIndex.ElectricShield:
                    case MM1ItemIndex.AcidShield:
                    case MM1ItemIndex.MagicShield:
                    case MM1ItemIndex.DragonShield: return ItemNounType.Shield;
                    default: return ItemNounType.None;
                }
            }
        }

        public static string GetItemTypeString(MM1ItemType type, bool bFullText)
        {
            switch (type)
            {
                case MM1ItemType.Armor: return "Armor";
                case MM1ItemType.OneHandedMelee: return bFullText ? "One-Handed Weapon" : "1H";
                case MM1ItemType.TwoHandedMelee: return bFullText ? "Two-Handed Weapon" : "2H";
                case MM1ItemType.Missile: return bFullText ? "Missile Weapon" : "Missile";
                case MM1ItemType.Shield: return "Shield";
                default: return bFullText ? "Miscellaneous Item" : "";
            }
        }

        public override string GetLongDescription(GenericAlignmentValue currentAlign, GenericClass currentClass, string strOverrideName)
        {
            MM1ItemType type = GetItemType(Index);
            string strType = GetItemTypeString(type, false);

            string strName = String.IsNullOrWhiteSpace(strOverrideName) ? ItemNoun : strOverrideName;

            string strUsable = "";
            if (type != MM1ItemType.None)
            {
                if (currentClass != GenericClass.None && !IsUsable(currentClass))
                    strUsable = String.Format(" (!{0})", MM1Character.ClassString(currentClass));
                if (currentAlign != GenericAlignmentValue.None && !IsUsable(currentAlign))
                    strUsable = String.Format(" (!{0})", MM1Character.AlignmentString(currentAlign));
            }

            string strDamage = DamageString;
            string strEquip = EquipString;
            string strUse = UseString;

            return String.Format("{0}{1}, {2}{3}{4}{5}{6} Gold",
                strName,
                strUsable,
                String.IsNullOrEmpty(strType) ? "" : strType + " ",
                String.IsNullOrEmpty(strDamage) ? "" : strDamage + ", ",
                String.IsNullOrEmpty(strEquip) ? "" : strEquip + ", ",
                String.IsNullOrEmpty(strUse) ? "" : "Use: " + strUse + ", ",
                Value);
        }

        public string UseString
        {
            get
            {
                if (UseAttribute == 0x00)
                    return "";
                if (UseAttribute == 0xFF)
                    return String.Format("{0} [{1}]", MM1SpellList.GetSpellNameForItem(UseAttributeBonus), m_iChargesCurrent);
                return String.Format("{0}+{1} [{2}]", OffsetStringAbbreviated(UseAttribute), UseAttributeBonus, m_iChargesCurrent);
            }
        }

        public string UseStringFull
        {
            get
            {
                if (UseAttribute == 0x00)
                    return "No special power";
                if (UseAttribute == 0xFF)
                    return String.Format("Casts {0} ({1})", MM1SpellList.GetSpellNameForItem(UseAttributeBonus), Global.Plural(m_iChargesCurrent, "charge"));
                return String.Format("Gives +{0} to {1} ({2})", UseAttributeBonus, OffsetString(UseAttribute), Global.Plural(m_iChargesCurrent, "charge"));
            }
        }

        public string EquipString
        {
            get
            {
                if (EquipAttribute < 2 || EquipAttribute == 0xff)
                    return "";
                return String.Format("{0}+{1}", OffsetStringAbbreviated(EquipAttribute), EquipAttributeBonus);
            }
        }

        public override string EquipEffects { get { return EquipString; } }

        public string EquipStringFull
        {
            get
            {
                if (EquipAttribute == 1)
                    return "Cannot be equipped";
                if (EquipAttribute == 0)
                    return "No special bonus";
                if (EquipAttribute == 0xff)
                    return "Cursed (cannot unequip)";
                return String.Format("Gives +{0} to {1}", EquipAttributeBonus, OffsetString(EquipAttribute));
            }
        }

        public string DamageString
        {
            get
            {
                MM1ItemType type = GetItemType(Index);
                switch (type)
                {
                    case MM1ItemType.Armor:
                    case MM1ItemType.Shield:
                        return String.Format("AC {0}", Extra);
                    case MM1ItemType.Missile:
                    case MM1ItemType.OneHandedMelee:
                    case MM1ItemType.TwoHandedMelee:
                        if (Extra > 0)
                            return String.Format("1d{0}+{1}", DamageByte, Extra);
                        return String.Format("1d{0}", DamageByte);
                    default:
                        return "";
                }
            }
        }

        public bool IsUsable(GenericClass testClass)
        {
            switch (testClass)
            {
                case GenericClass.Archer: return UsableBy.HasFlag(MM1UsableByFlags.Archer);
                case GenericClass.Cleric: return UsableBy.HasFlag(MM1UsableByFlags.Cleric);
                case GenericClass.Knight: return UsableBy.HasFlag(MM1UsableByFlags.Knight);
                case GenericClass.Paladin: return UsableBy.HasFlag(MM1UsableByFlags.Paladin);
                case GenericClass.Robber: return UsableBy.HasFlag(MM1UsableByFlags.Robber);
                case GenericClass.Sorcerer: return UsableBy.HasFlag(MM1UsableByFlags.Sorcerer);
                default: return false;
            }
        }

        public override GenericAlignmentValue Alignment
        {
            get
            {
                switch(UsableBy & MM1UsableByFlags.AnyAlignment)
                {
                    case MM1UsableByFlags.Evil: return GenericAlignmentValue.Evil;
                    case MM1UsableByFlags.Good: return GenericAlignmentValue.Good;
                    case MM1UsableByFlags.Neutral: return GenericAlignmentValue.Neutral;
                    default: return GenericAlignmentValue.None;
                }
            }
        }

        public bool IsUsable(GenericAlignmentValue testAlign)
        {
            switch (testAlign)
            {
                case GenericAlignmentValue.Evil: return UsableBy.HasFlag(MM1UsableByFlags.Evil);
                case GenericAlignmentValue.Neutral: return UsableBy.HasFlag(MM1UsableByFlags.Evil) == UsableBy.HasFlag(MM1UsableByFlags.Good);
                case GenericAlignmentValue.Good: return UsableBy.HasFlag(MM1UsableByFlags.Good);
                default: return false;
            }
        }

        public override bool IsUsableByAny(object filter)
        {
            if (filter is GenericAlignmentValue)
                return IsUsable((GenericAlignmentValue) filter);
            else if (filter is GenericClass)
                return IsUsable((GenericClass) filter);

            return false;
        }

        public override Item Clone()
        {
            MM1Item item = MM1Item.Create((MM1ItemIndex) Index, Name, (byte)UsableBy, EquipAttribute, EquipAttributeBonus, UseAttribute, UseAttributeBonus, Charges, (ushort) Value, DamageByte, Extra);
            item.m_iChargesCurrent = m_iChargesCurrent;
            return item;
        }

        public override byte[] Serialize()
        {
            return new byte[] { (byte) Index, m_iChargesCurrent };
        }

        private MM1Item()
        {
        }

        public static MM1Item Create(byte[] bytes, int iIndex)
        {
            MM1Item item = new MM1Item();
            item.SetFromBytes(bytes, iIndex);
            return item;
        }

        public void SetFromBytes(byte[] bytes, int iIndex)
        {
            Index = iIndex;
            Name = Encoding.ASCII.GetString(bytes, 0, 14).Trim();
            UsableBy = (MM1UsableByFlags)(~bytes[14]);
            //Attribute = (ItemAttribute)bytes[15];
            EquipAttribute = bytes[15];
            EquipAttributeBonus = bytes[16];
            UseAttribute = bytes[17];
            UseAttributeBonus = bytes[18];
            Charges = bytes[19];
            Value = (ushort)(bytes[20] << 8 | bytes[21]);
            DamageByte = bytes[22];
            Extra = bytes[23];
            m_iChargesCurrent = Charges;
            Cursed = EquipAttribute == 255;
        }

        public static MM1Item Create(MM1ItemIndex index, string name, byte usable, byte equip, byte equipBonus, byte useAttr, byte useBonus, byte charges, ushort value, byte damage, byte extra)
        {
            MM1Item item = new MM1Item();
            item.SetFromValues(index, name, usable, equip, equipBonus, useAttr, useBonus, charges, value, damage, extra);
            return item;
        }

        public void SetFromValues(MM1ItemIndex index, string name, byte usable, byte equip, byte equipBonus, byte useAttr, byte useBonus, byte charges, ushort value, byte damage, byte extra)
        {
            Index = (int) index;
            Name = name;
            UsableBy = (MM1UsableByFlags)usable;
            EquipAttribute = equip;
            EquipAttributeBonus = equipBonus;
            UseAttribute = useAttr;
            UseAttributeBonus = useBonus;
            Charges = charges;
            Value = value;
            DamageByte = damage;
            Extra = extra;
            m_iChargesCurrent = Charges;
            Cursed = EquipAttribute == 255;
        }

        public static MM1ItemType GetItemType(int index)
        {
            if (index >= 1 && index <= 60)
                return MM1ItemType.OneHandedMelee;
            if (index >= 61 && index <= 85)
                return MM1ItemType.Missile;
            if (index >= 86 && index <= 120)
                return MM1ItemType.TwoHandedMelee;
            if (index >= 121 && index <= 155)
                return MM1ItemType.Armor;
            if (index >= 156 && index <= 170)
                return MM1ItemType.Shield;
            if (index >= 171 && index <= 255)
                return MM1ItemType.Miscellaneous;
            return MM1ItemType.None;
        }

        public string Dump()
        {
            return String.Format("{0:X2} {1,-14}  {2}  {3,-14}  {4,-3}  {5,-14} {6,-24}  {7,-3}  {8,-5}  {9,-3}  {10,-3}\r\n",
                //            return String.Format("m_items.Add({0},\"{1}\", {2},{3},{4},{5},{6},{7},{8},{9},{10});\r\n",
                Index,
                Name,
                UsableByString(UsableBy),
                //(byte) UsableBy,
                EquipAttributeString(EquipAttribute),
                //EquipAttribute,
                EquipAttributeBonus,
                UseAttributeString(UseAttribute),
                //UseAttribute,
                UseAttribute == 0xFF ? MM1SpellList.GetSpellNameForItem(UseAttributeBonus) : UseAttributeBonus.ToString(),
                //UseAttributeBonus,
                Charges,
                Value,
                DamageByte,
                Extra
                );
        }

        public static string UsableByString(MM1UsableByFlags flags)
        {
            return String.Format("{0}{1}{2}{3}{4}{5}{6}{7}",
                flags.HasFlag(MM1UsableByFlags.Robber) ? "R" : ".",
                flags.HasFlag(MM1UsableByFlags.Sorcerer) ? "S" : ".",
                flags.HasFlag(MM1UsableByFlags.Cleric) ? "C" : ".",
                flags.HasFlag(MM1UsableByFlags.Archer) ? "A" : ".",
                flags.HasFlag(MM1UsableByFlags.Paladin) ? "P" : ".",
                flags.HasFlag(MM1UsableByFlags.Knight) ? "K" : ".",
                flags.HasFlag(MM1UsableByFlags.Evil) ? "E" : ".",
                flags.HasFlag(MM1UsableByFlags.Good) ? "G" : "."
                );
        }

        public static string EquipAttributeString(byte attr)
        {
            switch (attr)
            {
                case 0x00: return "None";
                case 0x01: return "No Equip";
                default: return OffsetString(attr);
                case 0xFF: return "Cursed";
            }
        }

        public static string UseAttributeString(byte attr)
        {
            switch (attr)
            {
                case 0x00: return "None";
                default: return OffsetString(attr);
                case 0xFF: return "Spell";
            }
        }

        public static GenericAttribute GetEquipAttribute(byte offset)
        {
            switch (offset)
            {
                case 0x10: return GenericAttribute.Sex;
                case 0x13: return GenericAttribute.Race;
                case 0x15:
                case 0x16: return GenericAttribute.Intellect;
                case 0x17:
                case 0x18: return GenericAttribute.Might;
                case 0x19:
                case 0x1a: return GenericAttribute.Personality;
                case 0x1B:
                case 0x1C: return GenericAttribute.Endurance;
                case 0x1D:
                case 0x1E: return GenericAttribute.Speed;
                case 0x1F:
                case 0x20: return GenericAttribute.Accuracy;
                case 0x21:
                case 0x22: return GenericAttribute.Luck;
                case 0x23:
                case 0x24: return GenericAttribute.Level;
                case 0x25: return GenericAttribute.Age;
                case 0x2B: return GenericAttribute.SP;
                case 0x30: return GenericAttribute.SpellLevel;
                case 0x31: return GenericAttribute.Gems;
                case 0x3A: return GenericAttribute.Gold;
                case 0x3C: return GenericAttribute.AC;
                case 0x3E: return GenericAttribute.Food;
                case 0x58:
                case 0x59: return GenericAttribute.MagicRes;
                case 0x5A:
                case 0x5B: return GenericAttribute.FireRes;
                case 0x5C:
                case 0x5D: return GenericAttribute.ColdRes;
                case 0x5E:
                case 0x5F: return GenericAttribute.ElecRes;
                case 0x60:
                case 0x61: return GenericAttribute.AcidRes;
                case 0x62:
                case 0x63: return GenericAttribute.FearRes;
                case 0x64:
                case 0x65: return GenericAttribute.PoisonRes;
                case 0x66:
                case 0x67: return GenericAttribute.SleepRes;
                case 0x6C: return GenericAttribute.Thievery;
                case 0xFF: return GenericAttribute.Cursed;
                default: return GenericAttribute.None;
            }
        }

        public static string OffsetString(byte offset)
        {
            switch (offset)
            {
                case 0x10: return "Sex";
                case 0x13: return "Race";
                case 0x15: return "Intellect";
                case 0x16: return "Intellect (temporary)";
                case 0x17: return "Might";
                case 0x18: return "Might (temporary)";
                case 0x19: return "Personality";
                case 0x1a: return "Personality (temporary)";
                case 0x1B: return "Endurance";
                case 0x1C: return "Endurance (temporary)";
                case 0x1D: return "Speed";
                case 0x1E: return "Speed (temporary)";
                case 0x1F: return "Accuracy";
                case 0x20: return "Accuracy (temporary)";
                case 0x21: return "Luck";
                case 0x22: return "Luck (temporary)";
                case 0x23: return "Level";
                case 0x24: return "Level (temporary)";
                case 0x25: return "Age";
                case 0x2B: return "Current Spell Points";
                case 0x30: return "Spell Level (temporary)";
                case 0x31: return "Gems";
                case 0x3A: return "Gold (second byte)";
                case 0x3C: return "AC";
                case 0x3E: return "Food";
                case 0x58: return "Magic Resistance";
                case 0x59: return "Magic Resistance (temporary)";
                case 0x5A: return "Fire Resistance";
                case 0x5B: return "Fire Resistance (temporary)";
                case 0x5C: return "Cold Resistance";
                case 0x5D: return "Cold Resistance (temporary)";
                case 0x5E: return "Elec Resistance";
                case 0x5F: return "Elec Resistance (temporary)";
                case 0x60: return "Acid Resistance";
                case 0x61: return "Acid Resistance (temporary)";
                case 0x62: return "Fear Resistance";
                case 0x63: return "Fear Resistance (temporary)";
                case 0x64: return "Poison Resistance";
                case 0x65: return "Poison Resistance (temporary)";
                case 0x66: return "Sleep Resistance";
                case 0x67: return "Sleep Resistance (temporary)";
                case 0x6C: return "Thievery";
                case 0xFF: return "Cursed";
                default: return String.Format("(unknown:{0:X2})", offset);
            }
        }

        public static string OffsetStringHeader(byte offset)
        {
            switch (offset)
            {
                case 0x10: return "Sex";
                case 0x13: return "Race";
                case 0x15:
                case 0x16: return "Intellect";
                case 0x17:
                case 0x18: return "Might";
                case 0x19:
                case 0x1a: return "Personality";
                case 0x1B:
                case 0x1C: return "Endurance";
                case 0x1D:
                case 0x1E: return "Speed";
                case 0x1F:
                case 0x20: return "Accuracy";
                case 0x21:
                case 0x22: return "Luck";
                case 0x23:
                case 0x24: return "Level";
                case 0x25: return "Age";
                case 0x2B: return "Spell Pts";
                case 0x30: return "Spell Lvl";
                case 0x31: return "Gems";
                case 0x3A: return "GoldByte2";
                case 0x3C: return "Armor Class";
                case 0x3E: return "Food";
                case 0x58:
                case 0x59: return "Magic Res";
                case 0x5A:
                case 0x5B: return "Fire Res";
                case 0x5C:
                case 0x5D: return "Cold Res";
                case 0x5E:
                case 0x5F: return "Elec Res";
                case 0x60:
                case 0x61: return "Acid Res";
                case 0x62:
                case 0x63: return "Fear Res";
                case 0x64:
                case 0x65: return "Poison Res";
                case 0x66:
                case 0x67: return "Sleep Res";
                case 0x6C: return "Thievery";
                case 0xFF: return "Cursed";
                default: return String.Format("(unknown:{0:X2})", offset);
            }
        }

        public static string OffsetStringAbbreviated(byte offset)
        {
            switch (offset)
            {
                case 0x10: return "Sex";
                case 0x13: return "Race";
                case 0x15:
                case 0x16: return "Int";
                case 0x17:
                case 0x18: return "Might";
                case 0x19:
                case 0x1a: return "Pers";
                case 0x1B:
                case 0x1C: return "End";
                case 0x1D:
                case 0x1E: return "Speed";
                case 0x1F:
                case 0x20: return "Accu";
                case 0x21:
                case 0x22: return "Luck";
                case 0x23:
                case 0x24: return "Level";
                case 0x25: return "Age";
                case 0x2B: return "SP";
                case 0x30: return "SpellLev";
                case 0x31: return "Gems";
                case 0x3A: return "Gold (2b)";
                case 0x3C: return "AC";
                case 0x3E: return "Food";
                case 0x58:
                case 0x59: return "MagicRes";
                case 0x5A:
                case 0x5B: return "FireRes";
                case 0x5C:
                case 0x5D: return "ColdRes";
                case 0x5E:
                case 0x5F: return "ElecRes";
                case 0x60:
                case 0x61: return "AcidRes";
                case 0x62:
                case 0x63: return "FearRes";
                case 0x64:
                case 0x65: return "PoisonRes";
                case 0x66:
                case 0x67: return "SleepRes";
                case 0x6C: return "Thievery";
                case 0xFF: return "Cursed";
                default: return String.Format("(unk:{0:X2})", offset);
            }
        }

        public static string SpellString(ushort spell) { return String.Format("{0:X4}", spell); }
        public override string UsableString { get { return UsableByString(UsableBy, false); } }

        public static string UsableByString(MM1UsableByFlags flags, bool bShowDots)
        {
            string strDot = bShowDots ? "." : "";
            string strUsable = String.Format("{0}{1}{2}{3}{4}{5}",
                flags.HasFlag(MM1UsableByFlags.Knight) ? "K" : strDot,
                flags.HasFlag(MM1UsableByFlags.Paladin) ? "P" : strDot,
                flags.HasFlag(MM1UsableByFlags.Archer) ? "A" : strDot,
                flags.HasFlag(MM1UsableByFlags.Cleric) ? "C" : strDot,
                flags.HasFlag(MM1UsableByFlags.Sorcerer) ? "S" : strDot,
                flags.HasFlag(MM1UsableByFlags.Robber) ? "R" : strDot
                );
            return strUsable;
        }

        public override string UsableByAlignment
        {
            get
            {
                MM1UsableByFlags align = UsableBy & MM1UsableByFlags.AnyAlignment;
                switch (align )
                {
                    case MM1UsableByFlags.None:
                        if (UsableBy == MM1UsableByFlags.None)
                            return String.Empty;
                        return "Neutral";
                    case MM1UsableByFlags.Evil: return "Evil";
                    case MM1UsableByFlags.Good: return "Good";
                    default: return "Any";
                }
            }
        }

        public override int ArmorClass { get { return ItemBaseType == ItemType.Armor ? Extra : 0; } }
        public override string AttributeString { get { return EquipAttribute < 2 ? String.Empty : OffsetStringHeader(EquipAttribute); } }
        public override int EquipBonusValue { get { return EquipAttribute < 2 ? 0 : EquipAttributeBonus; } }

        public override BasicDamage BaseDamage
        {
            get
            {
                if (!IsWeapon)
                    return BasicDamage.Zero;
                return new BasicDamage(1, new DamageDice(DamageByte, 1, Extra));
            }
        }

        public override string UseEffectString
        { 
            get
            {
                if (UseAttribute == 0)
                    return String.Empty;
                if (UseAttribute == 0xFF)
                    return MM1SpellList.GetSpellNameForItem(UseAttributeBonus);
                return String.Format("{0} {1}", OffsetStringHeader(UseAttribute), Global.AddPlus(UseAttributeBonus));
            }
        }

        public override string LargestBonusEffect { get { return AttributeString; } }
        public override int LargestBonus { get { return EquipBonusValue; } }
    }

    public class MM1ShopInventory : ShopInventory
    {
        public List<ShopItem> Items;

        public override IEnumerable<ShopItem> AllItems { get { return Items; } }

        public List<ShopItem> GetItems(byte[] bytes, int iStart, int iLength)
        {
            List<ShopItem> items = new List<ShopItem>(iLength);
            for (int i = iStart; i < iStart + iLength; i++)
            {
                if (i >= bytes.Length)
                    break;

                items.Add(new ShopItem(MM1.Items[bytes[i]].Clone(), MM1Memory.ShopItems + i, 1));
            }

            return items;
        }

        public List<ShopItem> GetItemSet(byte[] bytes, int iStart)
        {
            List<ShopItem> items = new List<ShopItem>(18);
            items.AddRange(GetItems(bytes, iStart, 6));
            items.AddRange(GetItems(bytes, 30 + iStart, 6));
            items.AddRange(GetItems(bytes, 60 + iStart, 6));
            return items;
        }

        public MM1ShopInventory(byte[] bytes, MM1Map map)
        {
            Items = new List<ShopItem>(18);
            switch (map)
            {
                case MM1Map.C2Sorpigal:
                    Items.AddRange(GetItemSet(bytes, 0));
                    break;
                case MM1Map.B3Portsmith:
                    Items.AddRange(GetItemSet(bytes, 6));
                    break;
                case MM1Map.D4Algary:
                    Items.AddRange(GetItemSet(bytes, 12));
                    break;
                case MM1Map.E1Dusk:
                    Items.AddRange(GetItemSet(bytes, 18));
                    break;
                case MM1Map.B1Erliquin:
                    Items.AddRange(GetItemSet(bytes, 24));
                    break;
                default:
                    break;
            }
        }
    }
}
