using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum MM45WeaponIndex
    {
        None = 0,
        First = 1,
        LongSword = 1,
        ShortSword = 2,
        BroadSword = 3,
        Scimitar = 4,
        Cutlass = 5,
        Sabre = 6,
        Club = 7,
        HandAxe = 8,
        Katana = 9,
        Nunchakas = 10,
        Wakazashi = 11,
        Dagger = 12,
        Mace = 13,
        Flail = 14,
        Cudgel = 15,
        Maul = 16,
        Spear = 17,
        Bardiche = 18,
        Glaive = 19,
        Halberd = 20,
        Pike = 21,
        Flamberge = 22,
        Trident = 23,
        Staff = 24,
        Hammer = 25,
        Naginata = 26,
        BattleAxe = 27,
        GrandAxe = 28,
        GreatAxe = 29,
        ShortBow = 30,
        LongBow = 31,
        Crossbow = 32,
        Sling = 33,
        XeenSlayerSword = 34, 
        Invalid = 35,
        Last = 35,
    }

    public enum MM45ArmorIndex
    {
        None = 0,
        First = 1,
        Robes = 1,
        ScaleArmor = 2,
        RingMail = 3,
        ChainMail = 4,
        SplintMail = 5,
        PlateMail = 6,
        PlateArmor = 7,
        Shield = 8,
        Helm = 9,
        Boots = 10,
        Cloak = 11,
        Cape = 12,
        Gauntlets = 13, 
        Invalid = 14,
        Last = 14
    }

    public enum MM45AccessoryIndex
    {
        None = 0,
        First = 1,
        Ring = 1,
        Belt = 2,
        Broach = 3,
        Medal = 4,
        Charm = 5,
        Cameo = 6,
        Scarab = 7,
        Pendant = 8,
        Necklace = 9,
        Amulet = 10, 
        Invalid = 11,
        Last = 11
    }

    public enum MM45MiscItemIndex
    {
        None = 0,
        First = 1,
        Rod = 1,
        Jewel = 2,
        Gem = 3,
        Box = 4,
        Orb = 5,
        Horn = 6,
        Coin = 7,
        Wand = 8,
        Whistle = 9,
        Potion = 10,
        Scroll = 11,
        Bogus = 12, 
        Invalid = 13,
        Last = 13
    }

    public enum MM45QuestItemIndex
    {
        None = 0,
        DeedToNewCastle = 1,
        CrystalKeyToWitchTower = 2,
        SkeletonKeyToDarzogsTower = 3,
        EnchantedKeyToTowerOfHighMagic = 4,
        JeweledAmuletOfTheNorthernSphinx = 5,
        StoneOfAThousandTerrors = 6,
        GolemStoneOfAdmittance = 7,
        YakStoneOfOpening = 8,
        XeensScepterOfTemporalDistortion = 9,
        AlacornOfFalista = 10,
        ElixirOfRestoration = 11,
        WandOfFaeryMagic = 12,
        PrincessRoxannesTiara = 13,
        HolyBookOfElvenkind = 14,
        ScarabOfImaging = 15,
        CrystalsOfPiezoelectricity = 16,
        ScrollOfInsight = 17,
        PhirnaRoot = 18,
        OrothinsBoneWhistle = 19,
        BaroksMagicPendant = 20,
        LigonosMissingSkull = 21,
        LastFlowerOfSummer = 22,
        LastRaindropOfSpring = 23,
        LastSnowflakeOfWinter = 24,
        LastLeafOfAutumn = 25,
        EverHotLavaRock = 26,
        KingsMegaCredit = 27,
        ExcavationPermit = 28,
        CupieDoll = 29,
        MightDoll = 30,
        SpeedDoll = 31,
        EnduranceDoll = 32,
        AccuracyDoll = 33,
        LuckDoll = 34,
        Widget = 35,
        PassToCastleview = 36,
        PassToSandcaster = 37,
        PassToLakeside = 38,
        PassToNecropolis = 39,
        PassToOlympus = 40,
        KeyToGreatWesternTower = 41,
        KeyToGreatSouthernTower = 42,
        KeyToGreatEasternTower = 43,
         KeyToGreatNorthernTower = 44,
        KeyToEllingersTower = 45,
        KeyToDragonTower = 46,
        KeyToDarkstoneTower = 47,
        KeyToTempleOfBark = 48,
        KeyToDungeonOfLostSouls = 49,
        KeyToAncientPyramid = 50,
        KeyToDungeonOfDeath = 51,
        AmuletOfTheSouthernSphinx = 52,
        DragonPharoahsOrb = 53,
        CubeOfPower = 54,
        ChimeOfOpening = 55,
        GoldIDCard = 56,
        SilverIDCard = 57,
        VultureRepellant = 58,
        Bridle = 59,
        EnchantedBridle = 60,
        TreasureMap = 61,
        NotUseD = 62,
        FakeMap = 63,
        OnyxNecklace = 64,
        DragonEgg = 65,
        Tribble = 66,
        GoldenPegasusStatuette = 67,
        GoldenDragonStatuette = 68,
        GoldenGriffinStatuette = 69,
        ChaliceOfProtection = 70,
        JewelOfAges = 71,
        SongbirdOfSerenity = 72,
        SandrosHeart = 73,
        EctorsRing = 74,
        VesparsEmeraldHandle = 75,
        QueenKalindrasCrown = 76,
        CalebsMagnifyingGlass = 77,
        SoulBox = 78,
        SoulBoxWithCorakInside = 79,
        RubyRock = 80,
        EmeraldRock = 81,
        SapphireRock = 82,
        DiamondRock = 83,
        MongaMelon = 84,
        EnergyDisk = 85,
        Invalid = 86
    }

    public enum MM45WeaponSuffixIndex
    {
        None = 0,
        DragonSlayer = 1,
        UndeadEater = 2,
        GolemSmasher = 3,
        BugZapper = 4,
        MonsterMasher = 5,
        BeastBopper = 6, 
        Invalid = 7
    }

    public enum MM45ItemSuffixIndex
    {
        None = 0,
        Light = 1,
        Awakening = 2,
        MagicArrows = 3,
        FirstAid = 4,
        Fists = 5,
        EnergyBlasts = 6,
        Sleeping = 7,
        Revitalization = 8,
        Curing = 9,
        Sparking = 10,
        Shrapmetal = 11,
        InsectRepellent = 12,
        ToxicClouds = 13,
        ElementalProtection = 14,
        Pain = 15,
        Jumping = 16,
        BeastControl = 17,
        Clairvoyance = 18,
        UndeadTurning = 19,
        Levitation = 20,
        WizardEyes = 21,
        Blessing = 22,
        MonsterIdentification = 23,
        Lightning = 24,
        HolyBonuses = 25,
        PowerCuring = 26,
        NaturesCures = 27,
        Beacons = 28,
        Shielding = 29,
        Heroism = 30,
        Hypnotism = 31,
        WaterWalking = 32,
        FrostBiting = 33,
        MonsterFinding = 34,
        Fireballs = 35,
        ColdRays = 36,
        Antidotes = 37,
        AcidSpraying = 38,
        TimeDistortion = 39,
        DragonSleep = 40,
        Vaccination = 41,
        Teleportation = 42,
        Death = 43,
        FreeMovement = 44,
        GolemStopping = 45,
        PoisonVolleys = 46,
        DeadlySwarms = 47,
        Shelter = 48,
        DailyProtection = 49,
        DailySorcerery = 50,
        Feasting = 51,
        FieryFlails = 52,
        Recharging = 53,
        Freezing = 54,
        TownPortals = 55,
        StoneToFlesh = 56,
        RaisingTheDead = 57,
        Etherealization = 58,
        DancingSwords = 59,
        MoonRays = 60,
        MassDistortion = 61,
        PrismaticLight = 62,
        EnchantItem = 63,
        Incinerating = 64,
        HolyWords = 65,
        Resurrection = 66,
        Storms = 67,
        Megavoltage = 68,
        Infernos = 69,
        SunRays = 70,
        Implosions = 71,
        StarBursts = 72,
        TheGODS = 73,
        Invalid = 74
    }

    public enum MM45ItemPrefixIndex
    {
        None = 0,
        Burning = 1,
        Fiery = 2,
        Pyric = 3,
        Fuming = 4,
        Flaming = 5,
        Seething = 6,
        Blazing = 7,
        Scorching = 8,
        Flickering = 9,
        Sparking = 10,
        Static = 11,
        Flashing = 12,
        Shocking = 13,
        Electric = 14,
        Dyna = 15,
        Icy = 16,
        Frost = 17,
        Freezing = 18,
        Cold = 19,
        Cryo = 20,
        Acidic = 21,
        Venemous = 22,
        Poisonous = 23,
        Toxic = 24,
        Noxious = 25,
        Glowing = 26,
        Incandescent = 27,
        Dense = 28,
        Sonic = 29,
        PowerEnergy = 30,
        Thermal = 31,
        Radiating = 32,
        Kinetic = 33,
        Mystic = 34,
        Magical = 35,
        Ectoplasmic = 36,
        Wooden = 37,
        Leather = 38,
        Brass = 39,
        Bronze = 40,
        Iron = 41,
        Silver = 42,
        Steel = 43,
        Gold = 44,
        Platinum = 45,
        Glass = 46,
        Coral = 47,
        Crystal = 48,
        Lapis = 49,
        Pearl = 50,
        Amber = 51,
        Ebony = 52,
        Quartz = 53,
        Ruby = 54,
        Emerald = 55,
        Sapphire = 56,
        Diamond = 57,
        Obsidian = 58,
        Might = 59,
        Strength = 60,
        Warrior = 61,
        Ogre = 62,
        Giant = 63,
        Thunder = 64,
        Force = 65,
        PowerMight = 66,
        Dragon = 67,
        Photon = 68,
        Clever = 69,
        Mind = 70,
        Sage = 71,
        Thought = 72,
        Knowledge = 73,
        Intellect = 74,
        Wisdom = 75,
        Genius = 76,
        Buddy = 77,
        Friendship = 78,
        Charm = 79,
        Personality = 80,
        Charisma = 81,
        Leadership = 82,
        Ego = 83,
        Holy = 84,
        Quick = 85,
        Swift = 86,
        Fast = 87,
        Rapid = 88,
        Speed = 89,
        Wind = 90,
        Accelerator = 91,
        Velocity = 92,
        Sharp = 93,
        Accurate = 94,
        Marksman = 95,
        Precision = 96,
        True = 97,
        Exacto = 98,
        Clover = 99,
        Chance = 100,
        Winners = 101,
        Lucky = 102,
        Gamblers = 103,
        Leprechauns = 104,
        Vigor = 105,
        Health = 106,
        Life = 107,
        Troll = 108,
        Vampiric = 109,
        Spell = 110,
        Castors = 111,
        Witch = 112,
        Mage = 113,
        Archmage = 114,
        Arcane = 115,
        Protection = 116,
        Armored = 117,
        Defender = 118,
        Stealth = 119,
        Divine = 120,
        Mugger = 121,
        Burgler = 122,
        Looter = 123,
        Brigand = 124,
        Filch = 125,
        Thief = 126,
        Rogue = 127,
        Plunder = 128,
        Criminal = 129,
        Pirate = 130,
        Invalid = 131
    }

    public class MM45ItemList
    {
        private bool m_bValid = false;
        private string m_strError = string.Empty;

        public bool Valid
        {
            get { return m_bValid; }
        }

        public string LastError
        {
            get { return m_strError; }
        }

        public MM45ItemList()
        {
            m_bValid = true;
        }
    }

    [Flags]
    public enum MM45ChargesFlags
    {
        ChargesMask = 0x3f,
        SuffixMask = 0x3f,
        Cursed = 0x40,
        Broken = 0x80
    }

    public class MM45ItemBase
    {
        public ItemType Type;
        public int Index;

        public MM45ItemBase(ItemType type, int index)
        {
            Type = type;
            Index = index;
        }

        public MM45WeaponIndex Weapon
        {
            get
            {
                if (Type == ItemType.Weapon)
                    return (MM45WeaponIndex)Index;
                return MM45WeaponIndex.None;
            }

            set
            {
                Type = ItemType.Weapon;
                Index = (int)value;
            }
        }

        public MM45ArmorIndex Armor
        {
            get
            {
                if (Type == ItemType.Armor)
                    return (MM45ArmorIndex)Index;
                return MM45ArmorIndex.None;
            }

            set
            {
                Type = ItemType.Armor;
                Index = (int)value;
            }
        }
        public MM45AccessoryIndex Accessory
        {
            get
            {
                if (Type == ItemType.Accessory)
                    return (MM45AccessoryIndex)Index;
                return MM45AccessoryIndex.None;
            }

            set
            {
                Type = ItemType.Accessory;
                Index = (int)value;
            }
        }

        public MM45MiscItemIndex Miscellaneous
        {
            get
            {
                if (Type == ItemType.Miscellaneous)
                    return (MM45MiscItemIndex)Index;
                return MM45MiscItemIndex.None;
            }

            set
            {
                Type = ItemType.Miscellaneous;
                Index = (int)value;
            }
        }

        public static string TypeString(ItemType type)
        {
            switch(type)
            {
                case ItemType.Accessory: return "Accessory";
                case ItemType.Armor: return "Armor";
                case ItemType.Miscellaneous: return "Miscellaneous";
                case ItemType.Weapon: return "Weapon";
                default: return "Unknown";
            }
        }
    }

    public class MM45Item : MMItem
    {
        public override GameNames Game { get { return GameNames.MightAndMagic45; } }
        private byte[] RawBytes;
        public MM45ItemBase Base;

        public override int Index
        {
            get { return Base.Index; }
            set { Base.Index = value; }
        }

        public override int GetHashCode()
        {
            return Base.Index | (int)WhereEquipped | (ChargesByte << 8) | ((int)Prefix << 16) | (Suffix << 24);
        }

        public MM45WeaponSuffixIndex WeaponSuffix;
        public MM45ItemSuffixIndex ItemSuffix;
        public MM45ItemPrefixIndex Prefix;

        public bool EqualsBase(MM45ItemBase item)
        {
            return (Base.Type == item.Type && Base.Index == item.Index);
        }

        public override string Name { get { return Global.Title(GetItemName(Base)); } }

        public int Suffix
        {
            get
            {
                if (IsWeapon)
                    return (int)WeaponSuffix;
                return (int)ItemSuffix;
            }

            set
            {
                if (IsWeapon)
                    WeaponSuffix = (MM45WeaponSuffixIndex) value;
                ItemSuffix = (MM45ItemSuffixIndex)value;
            }
        }

        private MM45Item()
        {
        }

        public static MM45Item Create()
        {
            MM45Item item = new MM45Item();
            item.SetDefaults();
            return item;
        }

        private void SetDefaults()
        {
            RawBytes = null;
            Base = new MM45ItemBase(ItemType.None, 0);
            ItemSuffix = MM45ItemSuffixIndex.None;
            Prefix = MM45ItemPrefixIndex.None;
            ChargesByte = 0;
            WhereEquipped = EquipLocation.None;
            WeaponSuffix = MM45WeaponSuffixIndex.None;
        }

        public static MM45Item Create(byte[] bytes, ItemType itemType, int offset)
        {
            MM45Item item = new MM45Item();
            item.SetFromBytes(bytes, itemType, offset);
            return item;
        }

        public static MM45Item Create(ItemType type, int index)
        {
            MM45Item item = new MM45Item();
            item.SetDefaults();
            item.Base = new MM45ItemBase(type, index);
            return item;
        }

        public static MM45Item FromScriptBytes(byte[] bytes)
        {
            if (bytes.Length < 4)
                return null;

            byte b = bytes[0];
            MM45ItemBase mmBase;
            if (b > 81)
                mmBase = new MM45ItemBase(ItemType.Quest, b - 81);
            else if (b > 60)
                mmBase = new MM45ItemBase(ItemType.Miscellaneous, b - 60);
            else if (b > 49)
                mmBase = new MM45ItemBase(ItemType.Accessory, b - 49);
            else if (b > 35)
                mmBase = new MM45ItemBase(ItemType.Armor, b - 35);
            else
                mmBase = new MM45ItemBase(ItemType.Weapon, b);

            MM45Item item = new MM45Item();
            item.Base = mmBase;
            byte b64 = (byte)(bytes[2] & 0x3f);
            switch (mmBase.Type)
            {
                case ItemType.Miscellaneous:
                    item.ItemSuffix = (MM45ItemSuffixIndex)bytes[1];
                    item.ChargesByte = bytes[3];
                    break;
                case ItemType.Weapon:
                    item.Prefix = (MM45ItemPrefixIndex)bytes[1];
                    item.WeaponSuffix = (MM45WeaponSuffixIndex)b64;
                    break;
                default:
                    item.Prefix = (MM45ItemPrefixIndex)bytes[1];
                    item.ItemSuffix = (MM45ItemSuffixIndex)bytes[2];
                    break;
            }

            return item;
        }

        public void SetFromBytes(byte[] bytes, ItemType itemType, int offset)
        {
            SetDefaults();

            RawBytes = bytes;

            byte b64 = (byte) (bytes[offset + 2] & 0x3f);

            switch(itemType)
            {
                case ItemType.Miscellaneous:
                    Base = new MM45ItemBase(itemType, bytes[offset]);
                    ItemSuffix = (MM45ItemSuffixIndex) bytes[offset+1];
                    ChargesByte = bytes[offset + 2];
                    break;
                case ItemType.Weapon:
                    Base = new MM45ItemBase(itemType, bytes[offset + 1]);
                    Prefix = (MM45ItemPrefixIndex)bytes[offset];
                    WeaponSuffix = (MM45WeaponSuffixIndex)b64;
                    WhereEquipped = (EquipLocation)bytes[offset + 3];
                    break;
                default:
                    Base = new MM45ItemBase(itemType, bytes[offset + 1]);
                    Prefix = (MM45ItemPrefixIndex) bytes[offset];
                    ItemSuffix = (MM45ItemSuffixIndex)b64;
                    WhereEquipped = (EquipLocation)bytes[offset+3];
                    break;
            }

            Cursed = ((bytes[offset + 2] & 0x40) > 0);
            Broken = ((bytes[offset + 2] & 0x80) > 0);

            MemoryIndex = -1;
        }

        public override BasicDamage BaseDamage
        {
            get
            {
                if (!IsWeapon)
                    return BasicDamage.Zero;
                return new BasicDamage(1, GetItemDamage(this));
            }
        }

        public override bool Trashable
        {
            get
            {
                if (IsWeapon && Base.Weapon == MM45WeaponIndex.XeenSlayerSword)
                    return false;
                return true;
            }
        }

        public override int Bonus
        {
            get
            {
                switch(Base.Type)
                {
                    case ItemType.Weapon:
                        if (IsElemental(Prefix))
                            return ElementalEffect(Prefix).DamageValue;
                        if (IsAttribute(Prefix))
                            return AttributeEffect(Prefix).Modifier;
                        break;
                    case ItemType.Armor:
                    case ItemType.Accessory:
                        if (IsElemental(Prefix))
                            return ElementalEffect(Prefix).ResistValue;
                        if (IsAttribute(Prefix))
                            return AttributeEffect(Prefix).Modifier;
                        break;
                }
                return 0;
            }
        }

        public override ItemType ItemBaseType { get { return Base.Type; } set { } }

        public override ItemType Type
        {
            get
            {
                switch(Base.Type)
                {
                    case ItemType.Weapon:
                        switch (Base.Weapon)
                        {
                            case MM45WeaponIndex.None:
                            case MM45WeaponIndex.Invalid:
                                 return ItemType.None;
                            case MM45WeaponIndex.Spear:
                            case MM45WeaponIndex.Bardiche:
                            case MM45WeaponIndex.Glaive:
                            case MM45WeaponIndex.Halberd:
                            case MM45WeaponIndex.Pike:
                            case MM45WeaponIndex.Flamberge:
                            case MM45WeaponIndex.Trident:
                            case MM45WeaponIndex.Staff:
                            case MM45WeaponIndex.Hammer:
                            case MM45WeaponIndex.Naginata:
                            case MM45WeaponIndex.BattleAxe:
                            case MM45WeaponIndex.GrandAxe:
                            case MM45WeaponIndex.GreatAxe:
                                 return ItemType.TwoHandMelee;
                            case MM45WeaponIndex.ShortBow:
                            case MM45WeaponIndex.LongBow:
                            case MM45WeaponIndex.Crossbow:
                            case MM45WeaponIndex.Sling:
                                 return ItemType.Missile;
                            default:
                                 return ItemType.OneHandMelee;
                        }
                    default:
                        return Base.Type;
                }
            }

            set { Base.Type = value; }
        }

        public byte ChargesByte
        {
            get
            {
                return (byte) ((m_iChargesCurrent & (byte) MM45ChargesFlags.ChargesMask) | (Cursed ? (byte)MM45ChargesFlags.Cursed : 0) | (Broken ? (byte)MM45ChargesFlags.Broken : 0));
            }
            set
            {
                m_iChargesCurrent = (byte)(value & (byte)MM45ChargesFlags.ChargesMask);
                Cursed = ((MM45ChargesFlags)value).HasFlag(MM45ChargesFlags.Cursed);
                Broken = ((MM45ChargesFlags)value).HasFlag(MM45ChargesFlags.Broken);
            }
        }

        public byte SuffixByte
        {
            get
            {
                return (byte)((Suffix & (byte)MM45ChargesFlags.SuffixMask) | (Cursed ? (byte)MM45ChargesFlags.Cursed : 0) | (Broken ? (byte)MM45ChargesFlags.Broken : 0));
            }
            set
            {
                Suffix = (byte)(value & (byte)MM45ChargesFlags.SuffixMask);
                Cursed = ((MM45ChargesFlags)value).HasFlag(MM45ChargesFlags.Cursed);
                Broken = ((MM45ChargesFlags)value).HasFlag(MM45ChargesFlags.Broken);
            }
        }

        public override bool NotUsable
        {
            get
            {
                return GetUsableBy(Base) == MM345UsableFlags.None;
            }
        }

        public static MM45Item CreateRandom(ItemType type, BaseCharacter baseChar = null)
        {
            MM45Item item = MM45Item.Create(Global.NullBytes(4), type, 0);
            item.Randomize(baseChar);
            return item;
        }

        public void Randomize(BaseCharacter baseChar = null)
        {
            // Generally in MM4 the item type can't be randomized, so randomize based on the type
            IntDeck deck = null;
            byte[] bytes = new byte[4];
            bytes[3] = (byte)WhereEquipped; // don't change this

            switch (Base.Type)
            {
                case ItemType.Weapon:
                    deck = new IntDeck(1, (int)MM45WeaponIndex.Invalid - 1);
                    break;
                case ItemType.Armor:
                    deck = new IntDeck(1, (int)MM45ArmorIndex.Invalid - 1);
                    break;
                case ItemType.Accessory:
                    deck = new IntDeck(1, (int)MM45AccessoryIndex.Invalid - 1);
                    break;
                case ItemType.Miscellaneous:
                    deck = new IntDeck(1, (int)MM45MiscItemIndex.Invalid - 1);
                    break;
                default:
                    return;
            }

            deck.Shuffle();

            foreach (int iIndex in deck.Cards)
            {
                switch (Base.Type)
                {
                    case ItemType.Weapon:
                        deck = new IntDeck(1, (int)MM45WeaponIndex.Invalid - 1);
                        deck.Shuffle();
                        bytes[0] = (byte)Global.Rand.Next(0, (int)MM45ItemPrefixIndex.Invalid);
                        bytes[1] = (byte)Global.Rand.Next(1, (int)MM45WeaponIndex.Invalid);
                        bytes[2] = (byte)(Global.Rand.Next(6) > 0 ? 0 : Global.Rand.Next(1, (int)MM45WeaponSuffixIndex.Invalid));
                        break;
                    case ItemType.Armor:
                        bytes[0] = (byte)Global.Rand.Next(0, (int)MM45ItemPrefixIndex.Invalid);
                        bytes[1] = (byte)Global.Rand.Next(1, (int)MM45ArmorIndex.Invalid);
                        bytes[2] = 0;   // Armor has no suffix
                        break;
                    case ItemType.Accessory:
                        bytes[0] = (byte)Global.Rand.Next(0, (int)MM45ItemPrefixIndex.Invalid);
                        bytes[1] = (byte)Global.Rand.Next(1, (int)MM45AccessoryIndex.Invalid);
                        bytes[2] = 0;   // Accessories have no suffixes
                        break;
                    case ItemType.Miscellaneous:
                        bytes[0] = (byte)Global.Rand.Next(1, (int)MM45MiscItemIndex.Bogus);
                        bytes[1] = (byte)Global.Rand.Next(0, (int)MM45ItemSuffixIndex.Invalid);
                        bytes[2] = (byte)Global.Rand.Next(0, 64);     // Charges
                        break;
                }
                if (baseChar == null && Global.Rand.Next(8) == 0)
                    bytes[2] |= (byte)(Global.Rand.Next(4) << 6);
                SetFromBytes(bytes, Base.Type, 0);
                if (MatchTypeAndChar(Base.Type, baseChar))
                    break;
            }
        }

        public override byte[] Serialize()
        {
            return GetBytes();
        }

        public static MM45Item FromBagBytes(byte[] bytes)
        {
            return MM45Item.FromBytes(bytes);
        }

        public override string UsableString
        {
            get
            {
                return UsableByString(GetUsableBy(Base), false);
            }
        }

        public static string UsableByString(MM345UsableFlags flags, bool bShowDots)
        {
            if (flags.HasFlag(MM345UsableFlags.AnyClass))
                return "<all>";

            string strDot = bShowDots ? "." : "";
            string strUsable = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                flags.HasFlag(MM345UsableFlags.Knight) ? "K" : strDot,
                flags.HasFlag(MM345UsableFlags.Paladin) ? "P" : strDot,
                flags.HasFlag(MM345UsableFlags.Archer) ? "A" : strDot,
                flags.HasFlag(MM345UsableFlags.Cleric) ? "C" : strDot,
                flags.HasFlag(MM345UsableFlags.Sorcerer) ? "S" : strDot,
                flags.HasFlag(MM345UsableFlags.Robber) ? "T" : strDot,
                flags.HasFlag(MM345UsableFlags.Ninja) ? "N" : strDot,
                flags.HasFlag(MM345UsableFlags.Barbarian) ? "B" : strDot,
                flags.HasFlag(MM345UsableFlags.Druid) ? "D" : strDot,
                flags.HasFlag(MM345UsableFlags.Ranger) ? "R" : strDot
                );
            return strUsable;
        }

        public override string TypeString
        {
            get
            {
                switch (Type)
                {
                    case ItemType.Armor: return "Armor";
                    case ItemType.Miscellaneous: return "Misc";
                    case ItemType.Missile: return "Missile";
                    case ItemType.OneHandMelee: return "1H Weapon";
                    case ItemType.TwoHandMelee: return "2H Weapon";
                    case ItemType.Accessory: return "Accessory";
                    case ItemType.Weapon: return "Weapon";
                    default: return String.Empty;
                }
            }
        }

        public override string BaseTypeString
        {
            get
            {
                switch (Type)
                {
                    case ItemType.Armor: return "Armor";
                    case ItemType.Miscellaneous: return "Misc";
                    case ItemType.Missile:
                    case ItemType.OneHandMelee:
                    case ItemType.TwoHandMelee:
                    case ItemType.Weapon: return "Weapon";
                    case ItemType.Accessory: return "Accessory";
                    default: return String.Empty;
                }
            }
        }

        public override string ScriptString
        {
            get
            {
                StringBuilder sb = new StringBuilder(DescriptionString);
                if (m_iChargesCurrent > 0 && Base.Type == ItemType.Miscellaneous)
                    sb.AppendFormat(", {0}", Global.Plural(m_iChargesCurrent, "charge"));
                return sb.ToString();
            }
        }

        public override string DescriptionString
        {
            get
            {
                if (Base.Type == ItemType.Quest)
                    return GetItemName((MM45QuestItemIndex) Base.Index);
                StringBuilder sb = new StringBuilder();
                string strPrefix = GetItemPrefix(Base.Type, Prefix).ToLower();
                string strBase = GetItemName(Base);
                if (Base.Type != ItemType.Weapon || Base.Index != (int)MM45WeaponIndex.XeenSlayerSword)
                    strBase = strBase.ToLower();
                string strSuffix = GetItemSuffix(Base.Type, Suffix);
                if (Base.Type != ItemType.Weapon && (Base.Type != ItemType.Miscellaneous || Suffix != (int) MM45ItemSuffixIndex.TheGODS))
                    strSuffix = strSuffix.ToLower();

                switch (Base.Type)
                {
                    case ItemType.Weapon:
                    case ItemType.Armor:
                    case ItemType.Accessory:
                        if (!String.IsNullOrWhiteSpace(strPrefix))
                            sb.AppendFormat("{0} ", strPrefix);
                        sb.Append(strBase);
                        if (!String.IsNullOrWhiteSpace(strSuffix))
                            sb.AppendFormat(" {0}", strSuffix);
                        break;
                    case ItemType.Miscellaneous:
                        sb.Append(strBase);
                        if (!String.IsNullOrWhiteSpace(strSuffix))
                            sb.AppendFormat(" of {0}", strSuffix);
                        break;
                    default:
                        sb.AppendFormat("<unknown item type:{0} index:{1}>", (int) Base.Type, Base.Index);
                        break;
                }

                if (sb.Length > 0)
                    sb[0] = Char.ToUpper(sb[0]);
                return sb.ToString();
            }
        }

        public static int BaseItemValue(ItemType type, int index)
        {
            switch (type)
            {
                case ItemType.Weapon:
                    switch ((MM45WeaponIndex)index)
                    {
                        case MM45WeaponIndex.LongSword: return 50;
                        case MM45WeaponIndex.ShortSword: return 15;
                        case MM45WeaponIndex.BroadSword: return 100;
                        case MM45WeaponIndex.Scimitar: return 80;
                        case MM45WeaponIndex.Cutlass: return 40;
                        case MM45WeaponIndex.Sabre: return 60;
                        case MM45WeaponIndex.Club: return 1;
                        case MM45WeaponIndex.HandAxe: return 10;
                        case MM45WeaponIndex.Katana: return 150;
                        case MM45WeaponIndex.Nunchakas: return 30;
                        case MM45WeaponIndex.Wakazashi: return 60;
                        case MM45WeaponIndex.Dagger: return 8;
                        case MM45WeaponIndex.Mace: return 50;
                        case MM45WeaponIndex.Flail: return 100;
                        case MM45WeaponIndex.Cudgel: return 15;
                        case MM45WeaponIndex.Maul: return 30;
                        case MM45WeaponIndex.Spear: return 15;
                        case MM45WeaponIndex.Bardiche: return 200;
                        case MM45WeaponIndex.Glaive: return 80;
                        case MM45WeaponIndex.Halberd: return 250;
                        case MM45WeaponIndex.Pike: return 150;
                        case MM45WeaponIndex.Flamberge: return 400;
                        case MM45WeaponIndex.Trident: return 100;
                        case MM45WeaponIndex.Staff: return 40;
                        case MM45WeaponIndex.Hammer: return 120;
                        case MM45WeaponIndex.Naginata: return 300;
                        case MM45WeaponIndex.BattleAxe: return 100;
                        case MM45WeaponIndex.GrandAxe: return 200;
                        case MM45WeaponIndex.GreatAxe: return 300;
                        case MM45WeaponIndex.ShortBow: return 25;
                        case MM45WeaponIndex.LongBow: return 100;
                        case MM45WeaponIndex.Crossbow: return 50;
                        case MM45WeaponIndex.Sling: return 15;
                        case MM45WeaponIndex.XeenSlayerSword: return 1;
                        default: return 0;
                    }
                case ItemType.Armor:
                    switch ((MM45ArmorIndex)index)
                    {
                        case MM45ArmorIndex.Robes: return 20;
                        case MM45ArmorIndex.ScaleArmor: return 100;
                        case MM45ArmorIndex.RingMail: return 200;
                        case MM45ArmorIndex.ChainMail: return 400;
                        case MM45ArmorIndex.SplintMail: return 600;
                        case MM45ArmorIndex.PlateMail: return 1000;
                        case MM45ArmorIndex.PlateArmor: return 2000;
                        case MM45ArmorIndex.Shield: return 100;
                        case MM45ArmorIndex.Helm: return 60;
                        case MM45ArmorIndex.Boots: return 40;
                        case MM45ArmorIndex.Cloak: return 250;
                        case MM45ArmorIndex.Cape: return 200;
                        case MM45ArmorIndex.Gauntlets: return 100;
                        default: return 0;
                    }
                case ItemType.Accessory:
                    switch ((MM45AccessoryIndex)index)
                    {
                        case MM45AccessoryIndex.Ring: return 100;
                        case MM45AccessoryIndex.Belt: return 100;
                        case MM45AccessoryIndex.Broach: return 250;
                        case MM45AccessoryIndex.Medal: return 100;
                        case MM45AccessoryIndex.Charm: return 50;
                        case MM45AccessoryIndex.Cameo: return 300;
                        case MM45AccessoryIndex.Scarab: return 200;
                        case MM45AccessoryIndex.Pendant: return 500;
                        case MM45AccessoryIndex.Necklace: return 1000;
                        case MM45AccessoryIndex.Amulet: return 2000;
                        default: return 0;
                    }
                case ItemType.Miscellaneous:
                    switch ((MM45MiscItemIndex)index)
                    {
                        case MM45MiscItemIndex.Rod: return 50;
                        case MM45MiscItemIndex.Jewel: return 1000;
                        case MM45MiscItemIndex.Gem: return 500;
                        case MM45MiscItemIndex.Box: return 10;
                        case MM45MiscItemIndex.Orb: return 100;
                        case MM45MiscItemIndex.Horn: return 20;
                        case MM45MiscItemIndex.Coin: return 10;
                        case MM45MiscItemIndex.Wand: return 50;
                        case MM45MiscItemIndex.Whistle: return 10;
                        case MM45MiscItemIndex.Potion: return 10;
                        case MM45MiscItemIndex.Scroll: return 100;
                        case MM45MiscItemIndex.Bogus: return 0;
                        default: return 0;
                    }
                default: return 0;
            }
        }

        public static int SuffixValueModifier(MM45ItemSuffixIndex suffix)
        {
            switch (suffix)
            {
                case MM45ItemSuffixIndex.Light:
                case MM45ItemSuffixIndex.Awakening:
                case MM45ItemSuffixIndex.MagicArrows:
                case MM45ItemSuffixIndex.FirstAid:
                case MM45ItemSuffixIndex.Fists:
                case MM45ItemSuffixIndex.EnergyBlasts:
                case MM45ItemSuffixIndex.Sleeping:
                case MM45ItemSuffixIndex.Revitalization:
                case MM45ItemSuffixIndex.Curing:
                case MM45ItemSuffixIndex.Sparking:
                case MM45ItemSuffixIndex.Shrapmetal:
                case MM45ItemSuffixIndex.InsectRepellent:
                case MM45ItemSuffixIndex.ToxicClouds:
                case MM45ItemSuffixIndex.ElementalProtection:
                case MM45ItemSuffixIndex.Pain:
                    return 100;
                case MM45ItemSuffixIndex.Jumping:
                case MM45ItemSuffixIndex.BeastControl:
                case MM45ItemSuffixIndex.Clairvoyance:
                case MM45ItemSuffixIndex.UndeadTurning:
                case MM45ItemSuffixIndex.Levitation:
                case MM45ItemSuffixIndex.WizardEyes:
                case MM45ItemSuffixIndex.Blessing:
                case MM45ItemSuffixIndex.MonsterIdentification:
                case MM45ItemSuffixIndex.Lightning:
                case MM45ItemSuffixIndex.HolyBonuses:
                case MM45ItemSuffixIndex.PowerCuring:
                case MM45ItemSuffixIndex.NaturesCures:
                case MM45ItemSuffixIndex.Beacons:
                case MM45ItemSuffixIndex.Shielding:
                case MM45ItemSuffixIndex.Heroism:
                    return 200;
                case MM45ItemSuffixIndex.Hypnotism:
                case MM45ItemSuffixIndex.WaterWalking:
                case MM45ItemSuffixIndex.FrostBiting:
                case MM45ItemSuffixIndex.MonsterFinding:
                case MM45ItemSuffixIndex.Fireballs:
                case MM45ItemSuffixIndex.ColdRays:
                case MM45ItemSuffixIndex.Antidotes:
                case MM45ItemSuffixIndex.AcidSpraying:
                case MM45ItemSuffixIndex.TimeDistortion:
                    return 300;
                case MM45ItemSuffixIndex.DragonSleep:
                case MM45ItemSuffixIndex.Vaccination:
                case MM45ItemSuffixIndex.Teleportation:
                case MM45ItemSuffixIndex.Death:
                case MM45ItemSuffixIndex.FreeMovement:
                case MM45ItemSuffixIndex.GolemStopping:
                case MM45ItemSuffixIndex.PoisonVolleys:
                case MM45ItemSuffixIndex.DeadlySwarms:
                case MM45ItemSuffixIndex.Shelter:
                case MM45ItemSuffixIndex.DailyProtection:
                case MM45ItemSuffixIndex.DailySorcerery:
                    return 400;
                case MM45ItemSuffixIndex.Feasting:
                case MM45ItemSuffixIndex.FieryFlails:
                case MM45ItemSuffixIndex.Recharging:
                case MM45ItemSuffixIndex.Freezing:
                case MM45ItemSuffixIndex.TownPortals:
                case MM45ItemSuffixIndex.StoneToFlesh:
                case MM45ItemSuffixIndex.RaisingTheDead:
                case MM45ItemSuffixIndex.Etherealization:
                case MM45ItemSuffixIndex.DancingSwords:
                case MM45ItemSuffixIndex.MoonRays:
                    return 500;
                case MM45ItemSuffixIndex.MassDistortion:
                case MM45ItemSuffixIndex.PrismaticLight:
                case MM45ItemSuffixIndex.EnchantItem:
                case MM45ItemSuffixIndex.Incinerating:
                case MM45ItemSuffixIndex.HolyWords:
                case MM45ItemSuffixIndex.Resurrection:
                case MM45ItemSuffixIndex.Storms:
                case MM45ItemSuffixIndex.Megavoltage:
                case MM45ItemSuffixIndex.Infernos:
                case MM45ItemSuffixIndex.SunRays:
                case MM45ItemSuffixIndex.Implosions:
                case MM45ItemSuffixIndex.StarBursts:
                case MM45ItemSuffixIndex.TheGODS:
                    return 600;
                default:
                    return 0;
            }
        }

        public override long Value
        {
            get 
            {
                decimal val = BaseItemValue(Base.Type, Base.Index);
                val *= ValueMultiplier(Prefix);
                val += ValueModifier(Prefix);
                if (Base.Type == ItemType.Miscellaneous)
                    val += SuffixValueModifier(ItemSuffix);
                return (long)val;
            }
        }

        public override string UsableByAlignment { get { return String.Empty; } }

        public static decimal ValueMultiplier(MM45ItemPrefixIndex prefix)
        {
            switch (prefix)
            {
                case MM45ItemPrefixIndex.Wooden: return 0.10M;
                case MM45ItemPrefixIndex.Leather: return 0.25M;
                case MM45ItemPrefixIndex.Brass: return 0.50M;
                case MM45ItemPrefixIndex.Bronze: return 0.75M;
                case MM45ItemPrefixIndex.Iron: return 2;
                case MM45ItemPrefixIndex.Glass: return 2;
                case MM45ItemPrefixIndex.Coral: return 3;
                case MM45ItemPrefixIndex.Silver: return 5;
                case MM45ItemPrefixIndex.Crystal: return 5;
                case MM45ItemPrefixIndex.Steel: return 10;
                case MM45ItemPrefixIndex.Lapis: return 10;
                case MM45ItemPrefixIndex.Pearl: return 20;
                case MM45ItemPrefixIndex.Gold: return 20;
                case MM45ItemPrefixIndex.Amber: return 30;
                case MM45ItemPrefixIndex.Ebony: return 40;
                case MM45ItemPrefixIndex.Platinum: return 50;
                case MM45ItemPrefixIndex.Quartz: return 50;
                case MM45ItemPrefixIndex.Ruby: return 60;
                case MM45ItemPrefixIndex.Emerald: return 70;
                case MM45ItemPrefixIndex.Sapphire: return 80;
                case MM45ItemPrefixIndex.Diamond: return 90;
                case MM45ItemPrefixIndex.Obsidian: return 100;
                default: return 1;
            }
        }

        public static bool IsMaterial(MM45ItemPrefixIndex prefix)
        {
            return (prefix >= MM45ItemPrefixIndex.Wooden && prefix <= MM45ItemPrefixIndex.Obsidian);
        }

        public static bool IsElemental(MM45ItemPrefixIndex prefix)
        {
            return (prefix >= MM45ItemPrefixIndex.Burning && prefix <= MM45ItemPrefixIndex.Ectoplasmic);
        }

        public static bool IsAttribute(MM45ItemPrefixIndex prefix)
        {
            return (prefix >= MM45ItemPrefixIndex.Might && prefix <= MM45ItemPrefixIndex.Pirate);
        }

        public static string ItemPrefixLevelRange(MM45ItemPrefixIndex prefix)
        {
            switch (prefix)
            {
                case MM45ItemPrefixIndex.Leather:
                case MM45ItemPrefixIndex.Flickering:
                case MM45ItemPrefixIndex.Glass:
                case MM45ItemPrefixIndex.Burning:
                case MM45ItemPrefixIndex.Glowing:
                case MM45ItemPrefixIndex.Wooden: return "2";
                case MM45ItemPrefixIndex.Quick:
                case MM45ItemPrefixIndex.Brass:
                case MM45ItemPrefixIndex.Friendship:
                case MM45ItemPrefixIndex.Buddy:
                case MM45ItemPrefixIndex.Coral:
                case MM45ItemPrefixIndex.Crystal:
                case MM45ItemPrefixIndex.Mind:
                case MM45ItemPrefixIndex.Sharp:
                case MM45ItemPrefixIndex.Fiery:
                case MM45ItemPrefixIndex.Burgler:
                case MM45ItemPrefixIndex.Mugger:
                case MM45ItemPrefixIndex.Clever:
                case MM45ItemPrefixIndex.Clover:
                case MM45ItemPrefixIndex.Sparking:
                case MM45ItemPrefixIndex.Chance:
                case MM45ItemPrefixIndex.Protection:
                case MM45ItemPrefixIndex.Strength:
                case MM45ItemPrefixIndex.Icy:
                case MM45ItemPrefixIndex.Might:
                case MM45ItemPrefixIndex.Acidic:
                case MM45ItemPrefixIndex.Vigor:
                case MM45ItemPrefixIndex.Spell:
                case MM45ItemPrefixIndex.Incandescent:
                case MM45ItemPrefixIndex.Swift: return "2-3";
                case MM45ItemPrefixIndex.Venemous:
                case MM45ItemPrefixIndex.Health:
                case MM45ItemPrefixIndex.Castors:
                case MM45ItemPrefixIndex.Dense:
                case MM45ItemPrefixIndex.Mystic:
                case MM45ItemPrefixIndex.Frost:
                case MM45ItemPrefixIndex.Static:
                case MM45ItemPrefixIndex.Armored:
                case MM45ItemPrefixIndex.Charm:
                case MM45ItemPrefixIndex.Warrior:
                case MM45ItemPrefixIndex.Fast:
                case MM45ItemPrefixIndex.Bronze:
                case MM45ItemPrefixIndex.Pyric:
                case MM45ItemPrefixIndex.Accurate:
                case MM45ItemPrefixIndex.Sage:
                case MM45ItemPrefixIndex.Lapis: return "2-4";
                case MM45ItemPrefixIndex.Ogre: return "2-5";
                case MM45ItemPrefixIndex.Looter:
                case MM45ItemPrefixIndex.Brigand:
                case MM45ItemPrefixIndex.Fuming:
                case MM45ItemPrefixIndex.Pearl:
                case MM45ItemPrefixIndex.Winners: return "3-4";
                case MM45ItemPrefixIndex.Rapid:
                case MM45ItemPrefixIndex.Shocking:
                case MM45ItemPrefixIndex.Freezing:
                case MM45ItemPrefixIndex.Iron:
                case MM45ItemPrefixIndex.Amber:
                case MM45ItemPrefixIndex.Defender:
                case MM45ItemPrefixIndex.Poisonous:
                case MM45ItemPrefixIndex.Thought:
                case MM45ItemPrefixIndex.Life:
                case MM45ItemPrefixIndex.Silver:
                case MM45ItemPrefixIndex.Marksman:
                case MM45ItemPrefixIndex.Steel:
                case MM45ItemPrefixIndex.Sonic:
                case MM45ItemPrefixIndex.Witch:
                case MM45ItemPrefixIndex.Flashing:
                case MM45ItemPrefixIndex.Giant: return "3-5";
                case MM45ItemPrefixIndex.Knowledge:
                case MM45ItemPrefixIndex.Flaming:
                case MM45ItemPrefixIndex.Speed:
                case MM45ItemPrefixIndex.Charisma:
                case MM45ItemPrefixIndex.PowerEnergy: return "3-6";
                case MM45ItemPrefixIndex.Magical:
                case MM45ItemPrefixIndex.Personality:
                case MM45ItemPrefixIndex.Filch:
                case MM45ItemPrefixIndex.Ebony:
                case MM45ItemPrefixIndex.Thief:
                case MM45ItemPrefixIndex.Lucky: return "4-5";
                case MM45ItemPrefixIndex.Gold:
                case MM45ItemPrefixIndex.Stealth:
                case MM45ItemPrefixIndex.Electric:
                case MM45ItemPrefixIndex.Troll:
                case MM45ItemPrefixIndex.Seething:
                case MM45ItemPrefixIndex.Toxic:
                case MM45ItemPrefixIndex.Precision:
                case MM45ItemPrefixIndex.Thermal:
                case MM45ItemPrefixIndex.Intellect:
                case MM45ItemPrefixIndex.Thunder:
                case MM45ItemPrefixIndex.Mage:
                case MM45ItemPrefixIndex.Cold:
                case MM45ItemPrefixIndex.Wind:
                case MM45ItemPrefixIndex.Leadership: return "4-6";
                case MM45ItemPrefixIndex.Quartz: return "5";
                case MM45ItemPrefixIndex.Archmage:
                case MM45ItemPrefixIndex.PowerMight:
                case MM45ItemPrefixIndex.Accelerator:
                case MM45ItemPrefixIndex.Rogue:
                case MM45ItemPrefixIndex.Gamblers:
                case MM45ItemPrefixIndex.Ruby:
                case MM45ItemPrefixIndex.Plunder:
                case MM45ItemPrefixIndex.Force:
                case MM45ItemPrefixIndex.Emerald:
                case MM45ItemPrefixIndex.Blazing:
                case MM45ItemPrefixIndex.Platinum:
                case MM45ItemPrefixIndex.True:
                case MM45ItemPrefixIndex.Wisdom:
                case MM45ItemPrefixIndex.Ego:
                case MM45ItemPrefixIndex.Radiating: return "5-6";
                case MM45ItemPrefixIndex.Dyna:
                case MM45ItemPrefixIndex.Cryo: return "5-7";
                case MM45ItemPrefixIndex.Criminal:
                case MM45ItemPrefixIndex.Diamond:
                case MM45ItemPrefixIndex.Sapphire:
                case MM45ItemPrefixIndex.Dragon: return "6";
                case MM45ItemPrefixIndex.Scorching:
                case MM45ItemPrefixIndex.Arcane:
                case MM45ItemPrefixIndex.Genius:
                case MM45ItemPrefixIndex.Pirate:
                case MM45ItemPrefixIndex.Obsidian:
                case MM45ItemPrefixIndex.Divine:
                case MM45ItemPrefixIndex.Vampiric:
                case MM45ItemPrefixIndex.Noxious:
                case MM45ItemPrefixIndex.Leprechauns:
                case MM45ItemPrefixIndex.Kinetic:
                case MM45ItemPrefixIndex.Ectoplasmic:
                case MM45ItemPrefixIndex.Exacto:
                case MM45ItemPrefixIndex.Velocity:
                case MM45ItemPrefixIndex.Photon:
                case MM45ItemPrefixIndex.Holy: return "6-7";
                default: return "?";
            }
        }

        public static string ItemPrefixEffect(ItemType type, MM45ItemPrefixIndex prefix, bool bIncludeItemLevel = true)
        {
            StringBuilder sb = new StringBuilder();
            if (IsMaterial(prefix))
            {
                switch (type)
                {
                    case ItemType.Weapon:
                    case ItemType.Armor:
                        sb.AppendFormat("{0}", PrefixHDAC(prefix).ToString(type));
                        break;
                    default:
                        break;
                }
                if (sb.Length > 0)
                    sb.Append(", ");
                sb.AppendFormat("Value x{0}", MM45Item.ValueMultiplier(prefix));
            }
            else if (IsElemental(prefix))
                sb.AppendFormat("{0}, Value +{1}", ElementalEffect(prefix).ToString(type == ItemType.Weapon), ValueModifier(prefix));
            else if (IsAttribute(prefix))
                sb.AppendFormat("{0}, Value +{1}", AttributeEffect(prefix).ToString(), ValueModifier(prefix));

            if (bIncludeItemLevel)
            {
                if (sb.Length > 0)
                    sb.Append(", Level ");
                sb.Append(ItemPrefixLevelRange(prefix));
            }

            return sb.ToString();
        }

        public static int ValueModifier(MM45ItemPrefixIndex prefix)
        {
            switch (prefix)
            {
                case MM45ItemPrefixIndex.Burning: return 200;
                case MM45ItemPrefixIndex.Fiery: return 300;
                case MM45ItemPrefixIndex.Pyric: return 400;
                case MM45ItemPrefixIndex.Fuming: return 500;
                case MM45ItemPrefixIndex.Flaming: return 1000;
                case MM45ItemPrefixIndex.Seething: return 1500;
                case MM45ItemPrefixIndex.Blazing: return 2000;
                case MM45ItemPrefixIndex.Scorching: return 3000;
                case MM45ItemPrefixIndex.Icy: return 200;
                case MM45ItemPrefixIndex.Frost: return 400;
                case MM45ItemPrefixIndex.Freezing: return 500;
                case MM45ItemPrefixIndex.Cold: return 1000;
                case MM45ItemPrefixIndex.Cryo: return 2000;
                case MM45ItemPrefixIndex.Flickering: return 200;
                case MM45ItemPrefixIndex.Sparking: return 300;
                case MM45ItemPrefixIndex.Static: return 400;
                case MM45ItemPrefixIndex.Flashing: return 500;
                case MM45ItemPrefixIndex.Shocking: return 1000;
                case MM45ItemPrefixIndex.Electric: return 1500;
                case MM45ItemPrefixIndex.Dyna: return 2000;
                case MM45ItemPrefixIndex.Acidic: return 200;
                case MM45ItemPrefixIndex.Venemous: return 400;
                case MM45ItemPrefixIndex.Poisonous: return 800;
                case MM45ItemPrefixIndex.Toxic: return 1600;
                case MM45ItemPrefixIndex.Noxious: return 3200;
                case MM45ItemPrefixIndex.Glowing: return 200;
                case MM45ItemPrefixIndex.Incandescent: return 300;
                case MM45ItemPrefixIndex.Dense: return 400;
                case MM45ItemPrefixIndex.Sonic: return 500;
                case MM45ItemPrefixIndex.PowerEnergy: return 1000;
                case MM45ItemPrefixIndex.Thermal: return 1500;
                case MM45ItemPrefixIndex.Radiating: return 2000;
                case MM45ItemPrefixIndex.Kinetic: return 3000;
                case MM45ItemPrefixIndex.Mystic: return 500;
                case MM45ItemPrefixIndex.Magical: return 1000;
                case MM45ItemPrefixIndex.Ectoplasmic: return 2500;
                case MM45ItemPrefixIndex.Might: return 200;
                case MM45ItemPrefixIndex.Strength: return 300;
                case MM45ItemPrefixIndex.Warrior: return 500;
                case MM45ItemPrefixIndex.Ogre: return 800;
                case MM45ItemPrefixIndex.Giant: return 1200;
                case MM45ItemPrefixIndex.Thunder: return 1700;
                case MM45ItemPrefixIndex.Force: return 2300;
                case MM45ItemPrefixIndex.PowerMight: return 3000;
                case MM45ItemPrefixIndex.Dragon: return 3800;
                case MM45ItemPrefixIndex.Photon: return 4700;
                case MM45ItemPrefixIndex.Clever: return 200;
                case MM45ItemPrefixIndex.Mind: return 300;
                case MM45ItemPrefixIndex.Sage: return 500;
                case MM45ItemPrefixIndex.Thought: return 800;
                case MM45ItemPrefixIndex.Knowledge: return 1200;
                case MM45ItemPrefixIndex.Intellect: return 1700;
                case MM45ItemPrefixIndex.Wisdom: return 2300;
                case MM45ItemPrefixIndex.Genius: return 3000;
                case MM45ItemPrefixIndex.Buddy: return 200;
                case MM45ItemPrefixIndex.Friendship: return 300;
                case MM45ItemPrefixIndex.Charm: return 500;
                case MM45ItemPrefixIndex.Personality: return 800;
                case MM45ItemPrefixIndex.Charisma: return 1200;
                case MM45ItemPrefixIndex.Leadership: return 1700;
                case MM45ItemPrefixIndex.Ego: return 2300;
                case MM45ItemPrefixIndex.Holy: return 3000;
                case MM45ItemPrefixIndex.Quick: return 200;
                case MM45ItemPrefixIndex.Swift: return 300;
                case MM45ItemPrefixIndex.Fast: return 500;
                case MM45ItemPrefixIndex.Rapid: return 800;
                case MM45ItemPrefixIndex.Speed: return 1200;
                case MM45ItemPrefixIndex.Wind: return 1700;
                case MM45ItemPrefixIndex.Accelerator: return 2300;
                case MM45ItemPrefixIndex.Velocity: return 3000;
                case MM45ItemPrefixIndex.Sharp: return 300;
                case MM45ItemPrefixIndex.Accurate: return 500;
                case MM45ItemPrefixIndex.Marksman: return 1000;
                case MM45ItemPrefixIndex.Precision: return 1500;
                case MM45ItemPrefixIndex.True: return 2000;
                case MM45ItemPrefixIndex.Exacto: return 3000;
                case MM45ItemPrefixIndex.Clover: return 500;
                case MM45ItemPrefixIndex.Chance: return 1000;
                case MM45ItemPrefixIndex.Winners: return 1500;
                case MM45ItemPrefixIndex.Lucky: return 2000;
                case MM45ItemPrefixIndex.Gamblers: return 2500;
                case MM45ItemPrefixIndex.Leprechauns: return 3000;
                case MM45ItemPrefixIndex.Vigor: return 400;
                case MM45ItemPrefixIndex.Health: return 600;
                case MM45ItemPrefixIndex.Life: return 1000;
                case MM45ItemPrefixIndex.Troll: return 2000;
                case MM45ItemPrefixIndex.Vampiric: return 5000;
                case MM45ItemPrefixIndex.Spell: return 400;
                case MM45ItemPrefixIndex.Castors: return 800;
                case MM45ItemPrefixIndex.Witch: return 1200;
                case MM45ItemPrefixIndex.Mage: return 1600;
                case MM45ItemPrefixIndex.Archmage: return 2000;
                case MM45ItemPrefixIndex.Arcane: return 2500;
                case MM45ItemPrefixIndex.Protection: return 200;
                case MM45ItemPrefixIndex.Armored: return 400;
                case MM45ItemPrefixIndex.Defender: return 600;
                case MM45ItemPrefixIndex.Stealth: return 1000;
                case MM45ItemPrefixIndex.Divine: return 1600;
                case MM45ItemPrefixIndex.Mugger: return 400;
                case MM45ItemPrefixIndex.Burgler: return 600;
                case MM45ItemPrefixIndex.Looter: return 800;
                case MM45ItemPrefixIndex.Brigand: return 1000;
                case MM45ItemPrefixIndex.Filch: return 1200;
                case MM45ItemPrefixIndex.Thief: return 1400;
                case MM45ItemPrefixIndex.Rogue: return 1600;
                case MM45ItemPrefixIndex.Plunder: return 1800;
                case MM45ItemPrefixIndex.Criminal: return 2000;
                case MM45ItemPrefixIndex.Pirate: return 2500;
                default: return 0;
            }
        }

        public static HitDamageAC PrefixHDAC(MM45ItemPrefixIndex prefix)
        {
            switch (prefix)
            {
                case MM45ItemPrefixIndex.Wooden: return new HitDamageAC(-3, -3, -3);
                case MM45ItemPrefixIndex.Leather: return new HitDamageAC(-4, -6, 0);
                case MM45ItemPrefixIndex.Brass: return new HitDamageAC(3, -4, -2);
                case MM45ItemPrefixIndex.Bronze: return new HitDamageAC(2, -2, -1);
                case MM45ItemPrefixIndex.Iron: return new HitDamageAC(1, 2, 1);
                case MM45ItemPrefixIndex.Silver: return new HitDamageAC(2, 4, 2);
                case MM45ItemPrefixIndex.Steel: return new HitDamageAC(3, 6, 4);
                case MM45ItemPrefixIndex.Gold: return new HitDamageAC(4, 8, 6);
                case MM45ItemPrefixIndex.Platinum: return new HitDamageAC(6, 10, 8);
                case MM45ItemPrefixIndex.Glass: return new HitDamageAC(0, 0, 0);
                case MM45ItemPrefixIndex.Coral: return new HitDamageAC(1, 1, 1);
                case MM45ItemPrefixIndex.Crystal: return new HitDamageAC(1, 1, 1);
                case MM45ItemPrefixIndex.Lapis: return new HitDamageAC(2, 2, 2);
                case MM45ItemPrefixIndex.Pearl: return new HitDamageAC(2, 2, 2);
                case MM45ItemPrefixIndex.Amber: return new HitDamageAC(3, 3, 3);
                case MM45ItemPrefixIndex.Ebony: return new HitDamageAC(4, 4, 4);
                case MM45ItemPrefixIndex.Quartz: return new HitDamageAC(5, 5, 5);
                case MM45ItemPrefixIndex.Ruby: return new HitDamageAC(6, 12, 10);
                case MM45ItemPrefixIndex.Emerald: return new HitDamageAC(7, 15, 12);
                case MM45ItemPrefixIndex.Sapphire: return new HitDamageAC(8, 20, 14);
                case MM45ItemPrefixIndex.Diamond: return new HitDamageAC(9, 30, 16);
                case MM45ItemPrefixIndex.Obsidian: return new HitDamageAC(10, 50, 20);
                default: return new HitDamageAC(0, 0, 0);
            }
        }

        public static ElementDamageResistance ElementalEffect(MM45ItemPrefixIndex element)
        {
            switch (element)
            {
                case MM45ItemPrefixIndex.Burning: return new ElementDamageResistance(GenericResistanceFlags.Fire, 2, 5);
                case MM45ItemPrefixIndex.Fiery: return new ElementDamageResistance(GenericResistanceFlags.Fire, 3, 7);
                case MM45ItemPrefixIndex.Pyric: return new ElementDamageResistance(GenericResistanceFlags.Fire, 4, 9);
                case MM45ItemPrefixIndex.Fuming: return new ElementDamageResistance(GenericResistanceFlags.Fire, 5, 12);
                case MM45ItemPrefixIndex.Flaming: return new ElementDamageResistance(GenericResistanceFlags.Fire, 10, 15);
                case MM45ItemPrefixIndex.Seething: return new ElementDamageResistance(GenericResistanceFlags.Fire, 15, 20);
                case MM45ItemPrefixIndex.Blazing: return new ElementDamageResistance(GenericResistanceFlags.Fire, 20, 25);
                case MM45ItemPrefixIndex.Scorching: return new ElementDamageResistance(GenericResistanceFlags.Fire, 30, 30);
                case MM45ItemPrefixIndex.Flickering: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 2, 5);
                case MM45ItemPrefixIndex.Sparking: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 3, 7);
                case MM45ItemPrefixIndex.Static: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 4, 9);
                case MM45ItemPrefixIndex.Flashing: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 5, 12);
                case MM45ItemPrefixIndex.Shocking: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 10, 15);
                case MM45ItemPrefixIndex.Electric: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 15, 20);
                case MM45ItemPrefixIndex.Dyna: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 20, 25);
                case MM45ItemPrefixIndex.Icy: return new ElementDamageResistance(GenericResistanceFlags.Cold, 2, 5);
                case MM45ItemPrefixIndex.Frost: return new ElementDamageResistance(GenericResistanceFlags.Cold, 4, 10);
                case MM45ItemPrefixIndex.Freezing: return new ElementDamageResistance(GenericResistanceFlags.Cold, 5, 15);
                case MM45ItemPrefixIndex.Cold: return new ElementDamageResistance(GenericResistanceFlags.Cold, 10, 20);
                case MM45ItemPrefixIndex.Cryo: return new ElementDamageResistance(GenericResistanceFlags.Cold, 20, 25);
                case MM45ItemPrefixIndex.Acidic: return new ElementDamageResistance(GenericResistanceFlags.Poison, 2, 10);
                case MM45ItemPrefixIndex.Venemous: return new ElementDamageResistance(GenericResistanceFlags.Poison, 4, 15);
                case MM45ItemPrefixIndex.Poisonous: return new ElementDamageResistance(GenericResistanceFlags.Poison, 8, 20);
                case MM45ItemPrefixIndex.Toxic: return new ElementDamageResistance(GenericResistanceFlags.Poison, 16, 25);
                case MM45ItemPrefixIndex.Noxious: return new ElementDamageResistance(GenericResistanceFlags.Poison, 32, 40);
                case MM45ItemPrefixIndex.Glowing: return new ElementDamageResistance(GenericResistanceFlags.Energy, 2, 5);
                case MM45ItemPrefixIndex.Incandescent: return new ElementDamageResistance(GenericResistanceFlags.Energy, 3, 7);
                case MM45ItemPrefixIndex.Dense: return new ElementDamageResistance(GenericResistanceFlags.Energy, 4, 9);
                case MM45ItemPrefixIndex.Sonic: return new ElementDamageResistance(GenericResistanceFlags.Energy, 5, 11);
                case MM45ItemPrefixIndex.PowerEnergy: return new ElementDamageResistance(GenericResistanceFlags.Energy, 10, 13);
                case MM45ItemPrefixIndex.Thermal: return new ElementDamageResistance(GenericResistanceFlags.Energy, 15, 15);
                case MM45ItemPrefixIndex.Radiating: return new ElementDamageResistance(GenericResistanceFlags.Energy, 20, 20);
                case MM45ItemPrefixIndex.Kinetic: return new ElementDamageResistance(GenericResistanceFlags.Energy, 30, 25);
                case MM45ItemPrefixIndex.Mystic: return new ElementDamageResistance(GenericResistanceFlags.Magic, 5, 5);
                case MM45ItemPrefixIndex.Magical: return new ElementDamageResistance(GenericResistanceFlags.Magic, 10, 10);
                case MM45ItemPrefixIndex.Ectoplasmic: return new ElementDamageResistance(GenericResistanceFlags.Magic, 25, 20);
                default: return new ElementDamageResistance(GenericResistanceFlags.None, 0, 0);
            }
        }

        public static AttributeModifier AttributeEffect(MM45ItemPrefixIndex attribute)
        {
            switch (attribute)
            {
                case MM45ItemPrefixIndex.Might: return new AttributeModifier(GenericAttribute.Might, 2);
                case MM45ItemPrefixIndex.Strength: return new AttributeModifier(GenericAttribute.Might, 3);
                case MM45ItemPrefixIndex.Warrior: return new AttributeModifier(GenericAttribute.Might, 5);
                case MM45ItemPrefixIndex.Ogre: return new AttributeModifier(GenericAttribute.Might, 8);
                case MM45ItemPrefixIndex.Giant: return new AttributeModifier(GenericAttribute.Might, 12);
                case MM45ItemPrefixIndex.Thunder: return new AttributeModifier(GenericAttribute.Might, 17);
                case MM45ItemPrefixIndex.Force: return new AttributeModifier(GenericAttribute.Might, 23);
                case MM45ItemPrefixIndex.PowerMight: return new AttributeModifier(GenericAttribute.Might, 30);
                case MM45ItemPrefixIndex.Dragon: return new AttributeModifier(GenericAttribute.Might, 38);
                case MM45ItemPrefixIndex.Photon: return new AttributeModifier(GenericAttribute.Might, 47);
                case MM45ItemPrefixIndex.Clever: return new AttributeModifier(GenericAttribute.Intellect, 2);
                case MM45ItemPrefixIndex.Mind: return new AttributeModifier(GenericAttribute.Intellect, 3);
                case MM45ItemPrefixIndex.Sage: return new AttributeModifier(GenericAttribute.Intellect, 5);
                case MM45ItemPrefixIndex.Thought: return new AttributeModifier(GenericAttribute.Intellect, 8);
                case MM45ItemPrefixIndex.Knowledge: return new AttributeModifier(GenericAttribute.Intellect, 12);
                case MM45ItemPrefixIndex.Intellect: return new AttributeModifier(GenericAttribute.Intellect, 17);
                case MM45ItemPrefixIndex.Wisdom: return new AttributeModifier(GenericAttribute.Intellect, 23);
                case MM45ItemPrefixIndex.Genius: return new AttributeModifier(GenericAttribute.Intellect, 30);
                case MM45ItemPrefixIndex.Buddy: return new AttributeModifier(GenericAttribute.Personality, 2);
                case MM45ItemPrefixIndex.Friendship: return new AttributeModifier(GenericAttribute.Personality, 3);
                case MM45ItemPrefixIndex.Charm: return new AttributeModifier(GenericAttribute.Personality, 5);
                case MM45ItemPrefixIndex.Personality: return new AttributeModifier(GenericAttribute.Personality, 8);
                case MM45ItemPrefixIndex.Charisma: return new AttributeModifier(GenericAttribute.Personality, 12);
                case MM45ItemPrefixIndex.Leadership: return new AttributeModifier(GenericAttribute.Personality, 17);
                case MM45ItemPrefixIndex.Ego: return new AttributeModifier(GenericAttribute.Personality, 23);
                case MM45ItemPrefixIndex.Holy: return new AttributeModifier(GenericAttribute.Personality, 30);
                case MM45ItemPrefixIndex.Quick: return new AttributeModifier(GenericAttribute.Speed, 2);
                case MM45ItemPrefixIndex.Swift: return new AttributeModifier(GenericAttribute.Speed, 3);
                case MM45ItemPrefixIndex.Fast: return new AttributeModifier(GenericAttribute.Speed, 5);
                case MM45ItemPrefixIndex.Rapid: return new AttributeModifier(GenericAttribute.Speed, 8);
                case MM45ItemPrefixIndex.Speed: return new AttributeModifier(GenericAttribute.Speed, 12);
                case MM45ItemPrefixIndex.Wind: return new AttributeModifier(GenericAttribute.Speed, 17);
                case MM45ItemPrefixIndex.Accelerator: return new AttributeModifier(GenericAttribute.Speed, 23);
                case MM45ItemPrefixIndex.Velocity: return new AttributeModifier(GenericAttribute.Speed, 30);
                case MM45ItemPrefixIndex.Sharp: return new AttributeModifier(GenericAttribute.Accuracy, 3);
                case MM45ItemPrefixIndex.Accurate: return new AttributeModifier(GenericAttribute.Accuracy, 5);
                case MM45ItemPrefixIndex.Marksman: return new AttributeModifier(GenericAttribute.Accuracy, 10);
                case MM45ItemPrefixIndex.Precision: return new AttributeModifier(GenericAttribute.Accuracy, 15);
                case MM45ItemPrefixIndex.True: return new AttributeModifier(GenericAttribute.Accuracy, 20);
                case MM45ItemPrefixIndex.Exacto: return new AttributeModifier(GenericAttribute.Accuracy, 30);
                case MM45ItemPrefixIndex.Clover: return new AttributeModifier(GenericAttribute.Luck, 5);
                case MM45ItemPrefixIndex.Chance: return new AttributeModifier(GenericAttribute.Luck, 10);
                case MM45ItemPrefixIndex.Winners: return new AttributeModifier(GenericAttribute.Luck, 15);
                case MM45ItemPrefixIndex.Lucky: return new AttributeModifier(GenericAttribute.Luck, 20);
                case MM45ItemPrefixIndex.Gamblers: return new AttributeModifier(GenericAttribute.Luck, 25);
                case MM45ItemPrefixIndex.Leprechauns: return new AttributeModifier(GenericAttribute.Luck, 30);
                case MM45ItemPrefixIndex.Vigor: return new AttributeModifier(GenericAttribute.HP, 4);
                case MM45ItemPrefixIndex.Health: return new AttributeModifier(GenericAttribute.HP, 6);
                case MM45ItemPrefixIndex.Life: return new AttributeModifier(GenericAttribute.HP, 10);
                case MM45ItemPrefixIndex.Troll: return new AttributeModifier(GenericAttribute.HP, 20);
                case MM45ItemPrefixIndex.Vampiric: return new AttributeModifier(GenericAttribute.HP, 50);
                case MM45ItemPrefixIndex.Spell: return new AttributeModifier(GenericAttribute.SP, 4);
                case MM45ItemPrefixIndex.Castors: return new AttributeModifier(GenericAttribute.SP, 8);
                case MM45ItemPrefixIndex.Witch: return new AttributeModifier(GenericAttribute.SP, 12);
                case MM45ItemPrefixIndex.Mage: return new AttributeModifier(GenericAttribute.SP, 16);
                case MM45ItemPrefixIndex.Archmage: return new AttributeModifier(GenericAttribute.SP, 20);
                case MM45ItemPrefixIndex.Arcane: return new AttributeModifier(GenericAttribute.SP, 25);
                case MM45ItemPrefixIndex.Protection: return new AttributeModifier(GenericAttribute.AC, 2);
                case MM45ItemPrefixIndex.Armored: return new AttributeModifier(GenericAttribute.AC, 4);
                case MM45ItemPrefixIndex.Defender: return new AttributeModifier(GenericAttribute.AC, 6);
                case MM45ItemPrefixIndex.Stealth: return new AttributeModifier(GenericAttribute.AC, 10);
                case MM45ItemPrefixIndex.Divine: return new AttributeModifier(GenericAttribute.AC, 16);
                case MM45ItemPrefixIndex.Mugger: return new AttributeModifier(GenericAttribute.Thievery, 4);
                case MM45ItemPrefixIndex.Burgler: return new AttributeModifier(GenericAttribute.Thievery, 6);
                case MM45ItemPrefixIndex.Looter: return new AttributeModifier(GenericAttribute.Thievery, 8);
                case MM45ItemPrefixIndex.Brigand: return new AttributeModifier(GenericAttribute.Thievery, 10);
                case MM45ItemPrefixIndex.Filch: return new AttributeModifier(GenericAttribute.Thievery, 12);
                case MM45ItemPrefixIndex.Thief: return new AttributeModifier(GenericAttribute.Thievery, 14);
                case MM45ItemPrefixIndex.Rogue: return new AttributeModifier(GenericAttribute.Thievery, 16);
                case MM45ItemPrefixIndex.Plunder: return new AttributeModifier(GenericAttribute.Thievery, 18);
                case MM45ItemPrefixIndex.Criminal: return new AttributeModifier(GenericAttribute.Thievery, 20);
                case MM45ItemPrefixIndex.Pirate: return new AttributeModifier(GenericAttribute.Thievery, 25);
                default: return new AttributeModifier(GenericAttribute.None, 0);
            }
        }


        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Item: {0}{1}{2}\r\n", Global.Title(GetItemName(Base)), Broken ? " (broken)" : "", Cursed ? " (cursed)" : "");
                if (m_iChargesCurrent > 0)
                    sb.AppendFormat("Charges: {0}\r\n", m_iChargesCurrent);
                sb.AppendFormat("Description: {0}\r\n", DescriptionString);
                if (IsElemental(Prefix))
                    sb.AppendFormat("Prefix: {0} ({1})\r\n", Global.Title(GetItemPrefix(Base.Type, Prefix)), ElementalEffect(Prefix).ToString(IsWeapon));
                else if (IsMaterial(Prefix) && Base.Type != ItemType.Accessory)
                    sb.AppendFormat("Prefix: {0} ({1})\r\n", Global.Title(GetItemPrefix(Base.Type, Prefix)), PrefixHDAC(Prefix).ToString(Base.Type));
                else if (IsAttribute(Prefix))
                    sb.AppendFormat("Prefix: {0} ({1})\r\n", Global.Title(GetItemPrefix(Base.Type, Prefix)), AttributeEffect(Prefix).ToString());
                if (Base.Type == ItemType.Miscellaneous && ItemSuffix != MM45ItemSuffixIndex.None)
                    sb.AppendFormat("Suffix: {0} ({1})\r\n", GetItemSuffix(Base.Type, Suffix), MM45SpellList.GetSpellName(ItemSuffixEffect(Base.Type, Suffix)));

                if (m_iChargesCurrent > 0 || Base.Type == ItemType.Miscellaneous)
                    sb.AppendFormat("Charges: {0}\r\n", m_iChargesCurrent);

                StringBuilder sbUsableClass = new StringBuilder("Usable by class: ");
                MM345UsableFlags usable = GetUsableBy(Base);
                if (usable.HasFlag(MM345UsableFlags.AnyClass))
                    sbUsableClass.Append("ANY");
                else
                {
                    if (usable.HasFlag(MM345UsableFlags.Knight))
                        sbUsableClass.Append("Knight, ");
                    if (usable.HasFlag(MM345UsableFlags.Paladin))
                        sbUsableClass.Append("Paladin, ");
                    if (usable.HasFlag(MM345UsableFlags.Archer))
                        sbUsableClass.Append("Archer, ");
                    if (usable.HasFlag(MM345UsableFlags.Cleric))
                        sbUsableClass.Append("Cleric, ");
                    if (usable.HasFlag(MM345UsableFlags.Sorcerer))
                        sbUsableClass.Append("Sorcerer, ");
                    if (usable.HasFlag(MM345UsableFlags.Robber))
                        sbUsableClass.Append("Robber, ");
                    if (usable.HasFlag(MM345UsableFlags.Ninja))
                        sbUsableClass.Append("Ninja, ");
                    if (usable.HasFlag(MM345UsableFlags.Barbarian))
                        sbUsableClass.Append("Barbarian, ");
                    if (usable.HasFlag(MM345UsableFlags.Druid))
                        sbUsableClass.Append("Druid, ");
                    if (usable.HasFlag(MM345UsableFlags.Ranger))
                        sbUsableClass.Append("Ranger, ");
                    if (Global.Trim(sbUsableClass).Length == 0)
                        sbUsableClass.Append("NONE");
                }
                sb.AppendLine(sbUsableClass.ToString());

                string strDam = DamageOrAC(true);
                if (!String.IsNullOrWhiteSpace(strDam))
                    sb.AppendLine(strDam);

                sb.AppendFormat("Value: {0} Gold\r\n", Value);
                return sb.ToString();
            }
        }

        public string DamageOrAC(bool bFull)
        {
            DamageDice damage = GetItemDamage(this);
            int iAC = GetArmorClass(this);

            switch (Base.Type)
            {
                case ItemType.Weapon:
                    return String.Format("{0}{1}", bFull ? "Damage: " : "", damage.ToString());
                case ItemType.Armor:
                    return String.Format("{0}{1}", bFull ? "Armor Class: " : "AC", iAC);
                default:
                    return String.Empty;
            }
        }

        public static string DamageOrAC(MM45ItemBase item, bool bFull)
        {
            DamageDice damage = GetItemDamage(item);
            int iAC = GetArmorClass(item);

            if (damage.Quantity != 0)
                return String.Format("{0}{1}", bFull ? "Damage: " : "", damage.ToString());
            if (iAC != 0)
                return String.Format("{0}{1}", bFull ? "Armor Class: " : "AC", iAC);
            return String.Empty;
        }

        public override string DamageStringFull
        {
            get
            {
                DamageDice damage = GetItemDamage(Base);
                if (damage.Quantity < 1)
                    return String.Empty;

                HitDamageAC materialEffect = PrefixHDAC(Prefix);

                string strBonus = "";
                if (IsElemental(Prefix))
                {
                    ElementDamageResistance effect = ElementalEffect(Prefix);
                    if (IsWeapon)
                        strBonus = String.Format("+{0} {1}", effect.DamageValue, Global.SingleResistance(effect.DamageElement));
                }
                else if (IsMaterial(Prefix) && IsWeapon)
                    damage.Bonus += materialEffect.Damage;

                string strDamage = String.Format("{0}{1}", damage.ToString(), materialEffect.AddHitString);

                if (String.IsNullOrWhiteSpace(strDamage))
                    return String.Empty;

                if (String.IsNullOrWhiteSpace(strBonus))
                    return strDamage;

                return String.Format("{0} {1}", strDamage, strBonus);
            }
        }

        public override int ArmorClassFull { get { return GetArmorClass(this); } }

        public override int ToHitBonus { get { return IsWeapon ? PrefixHDAC(Prefix).Hit : 0; } }

        public override string EquipEffects
        {
            get
            {
                if (IsAttribute(Prefix))
                {
                    AttributeModifier attributeEffect = AttributeEffect(Prefix);
                    if (!IsArmor || attributeEffect.Attribute != GenericAttribute.AC) // Don't display things like "AC 1, AC+4"
                        return String.Format("{0}{1}", Global.GenericAttributeString(attributeEffect.Attribute), Global.AddPlus(attributeEffect.Modifier));
                }
                else if (IsElemental(Prefix) && !IsWeapon)
                {
                    ElementDamageResistance effect = ElementalEffect(Prefix);
                    return String.Format("{0}Res +{1}", Global.SingleResistance(effect.ResistElement), effect.ResistValue);
                }
                return String.Empty;
            }
        }

        public override string GetLongDescription(GenericAlignmentValue currentAlign, GenericClass currentClass, string strOverrideName)
        {
            DamageDice damage = GetItemDamage(Base);
            string strBrokenCursed = "";
            if (Broken)
                strBrokenCursed = "BROKEN ";
            if (Cursed)
                strBrokenCursed = strBrokenCursed + "CURSED ";
            string strUse = "";
            if (ItemSuffix != MM45ItemSuffixIndex.None)
                strUse = String.Format("{0} [{1}]", MM45SpellList.GetSpellName(MM45Item.ItemSuffixEffect(Base.Type, Suffix)), m_iChargesCurrent);
            string strItem = Global.Title(GetItemName(Base));
            string strUsable = "";
            if (!IsUsable(currentClass) && currentClass != GenericClass.None)
                strUsable = String.Format(" (!{0})", MM45Character.ClassString(currentClass));

            string strWeaponSuffix = "";
            if (WeaponSuffix != MM45WeaponSuffixIndex.None)
                strWeaponSuffix = WeaponSuffixEffect(WeaponSuffix, true);

            string strEquipAttribute = EquipEffects;

            string strType = ItemTypeAbbr;

            int iAC = ArmorClassFull;

            string strDamage = damage.Quantity > 0 ? DamageStringFull : iAC != 0 ? String.Format("AC {0}", iAC) : String.Empty;

            return String.Format("{0}{1}{2}{3}, {4}{5}{6}{7}{8} Gold",
                strBrokenCursed,
                String.IsNullOrWhiteSpace(strOverrideName) ? strItem : strOverrideName,
                strUsable,
                Global.SpaceParen(strType),
                Global.AddCommaSpace(strDamage),
                Global.AddCommaSpace(strWeaponSuffix),
                Global.AddCommaSpace(strEquipAttribute),
                String.IsNullOrEmpty(strUse) ? "" : "Use: " + strUse + ", ",
                Value);
        }

        public override int ArmorClass { get { return ArmorClassFull; } }

        public bool IsUsable(GenericClass testClass)
        {
            MM345UsableFlags usable = GetUsableBy(Base);
            switch (testClass)
            {
                case GenericClass.Archer: return usable.HasFlag(MM345UsableFlags.Archer);
                case GenericClass.Cleric: return usable.HasFlag(MM345UsableFlags.Cleric);
                case GenericClass.Knight: return usable.HasFlag(MM345UsableFlags.Knight);
                case GenericClass.Paladin: return usable.HasFlag(MM345UsableFlags.Paladin);
                case GenericClass.Robber: return usable.HasFlag(MM345UsableFlags.Robber);
                case GenericClass.Sorcerer: return usable.HasFlag(MM345UsableFlags.Sorcerer);
                case GenericClass.Ninja: return usable.HasFlag(MM345UsableFlags.Ninja);
                case GenericClass.Barbarian: return usable.HasFlag(MM345UsableFlags.Barbarian);
                case GenericClass.Druid: return usable.HasFlag(MM345UsableFlags.Druid);
                case GenericClass.Ranger: return usable.HasFlag(MM345UsableFlags.Ranger);
                default: return false;
            }
        }

        public bool IsUsable(GenericAlignmentValue testAlign)
        {
            return true;
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
            MM45Item item = MM45Item.FromBytes(GetBytes());
            item.WhereEquipped = EquipLocation.None;
            item.MemoryIndex = -1;
            return item;
        }

        public byte[] GetBytes()
        {
            switch (Type)
            {
                case ItemType.Miscellaneous:
                    return new byte[] { (byte)Base.Type, (byte)Base.Index, (byte)ItemSuffix, (byte)ChargesByte, (byte)WhereEquipped };
                default:
                    return new byte[] { (byte)Base.Type, (byte)Prefix, (byte)Base.Index, (byte)SuffixByte, (byte) WhereEquipped };
            }
        }

        public byte[] GetMemoryBytes()
        {
            return GetBytes().Skip(1).ToArray();
        }

        public static MM45Item FromBytes(byte[] bytes, int offset = 0)
        {
            if (bytes == null || bytes.Length - offset < 5)
                return null;
            return MM45Item.Create(bytes, (ItemType)bytes[offset], offset + 1);
        }

        public override EquipLocation CanEquipLocation
        {
            get
            {
                if (IsWeapon)
                {
                    switch (Type)
                    {
                        case ItemType.OneHandMelee: return EquipLocation.RightHand;
                        case ItemType.TwoHandMelee: return EquipLocation.BothHands;
                        case ItemType.Missile: return EquipLocation.Ranged;
                        default: return EquipLocation.None;
                    }
                }
                if (IsArmor)
                {
                    switch ((MM45ArmorIndex)Index)
                    {
                        case MM45ArmorIndex.Shield: return EquipLocation.LeftHand;
                        case MM45ArmorIndex.Helm: return EquipLocation.Head;
                        case MM45ArmorIndex.Boots: return EquipLocation.Feet;
                        case MM45ArmorIndex.Cloak:
                        case MM45ArmorIndex.Cape: return EquipLocation.Cloak;
                        case MM45ArmorIndex.Gauntlets: return EquipLocation.Gauntlet;
                        default: return EquipLocation.Torso;
                    }
                }
                if (IsAccessory)
                {
                    switch ((MM45AccessoryIndex)Index)
                    {
                        case MM45AccessoryIndex.Ring: return EquipLocation.Finger;
                        case MM45AccessoryIndex.Belt: return EquipLocation.Belt;
                        case MM45AccessoryIndex.Broach:
                        case MM45AccessoryIndex.Medal:
                        case MM45AccessoryIndex.Charm:
                        case MM45AccessoryIndex.Cameo:
                        case MM45AccessoryIndex.Scarab: return EquipLocation.Medallion;
                        case MM45AccessoryIndex.Pendant:
                        case MM45AccessoryIndex.Necklace:
                        case MM45AccessoryIndex.Amulet: return EquipLocation.Neck;
                        default: return EquipLocation.None;
                    }
                }
                return EquipLocation.None;
            }
        }

        public override CompareResult CompareTo(Item item)
        {
            if (!(item is MM45Item))
                return CompareResult.Uncomparable;
            if (item == this)
                return CompareResult.Identical;
            if (Type != item.Type)
                return CompareResult.Uncomparable;

            MM45Item mm45Item = item as MM45Item;

            if (mm45Item.Cursed && !Cursed)
                return CompareResult.Better;     // Cursed is always worse than non-cursed
            else if (!mm45Item.Cursed && Cursed)
                return CompareResult.Worse;    // ... and vice-versa

            if (CanEquipLocation != mm45Item.CanEquipLocation)
                return CompareResult.Uncomparable;

            if (!CanEquip)
                return CompareResult.Uncomparable;

            ElementDamageResistance elemental1 = IsElemental(Prefix) ? ElementalEffect(Prefix) : ElementDamageResistance.Empty;
            HitDamageAC hdac1 = IsMaterial(Prefix) ? PrefixHDAC(Prefix) : HitDamageAC.Empty;
            AttributeModifier modifier1 = IsAttribute(Prefix) ? AttributeEffect(Prefix) : AttributeModifier.Empty;
            MM45SpellIndex spell1 = ItemSuffixEffect(ItemBaseType, Suffix);

            ElementDamageResistance elemental2 = IsElemental(mm45Item.Prefix) ? ElementalEffect(mm45Item.Prefix) : ElementDamageResistance.Empty;
            HitDamageAC hdac2 = IsMaterial(mm45Item.Prefix) ? PrefixHDAC(mm45Item.Prefix) : HitDamageAC.Empty;
            AttributeModifier modifier2 = IsAttribute(mm45Item.Prefix) ? AttributeEffect(mm45Item.Prefix) : AttributeModifier.Empty;
            MM45SpellIndex spell2 = ItemSuffixEffect(ItemBaseType, mm45Item.Suffix);

            if (elemental1.DamageElement != elemental2.DamageElement && !(elemental1.DamageElement == GenericResistanceFlags.None || elemental2.DamageElement == GenericResistanceFlags.None))
                return CompareResult.Uncomparable;
            if (modifier1.Attribute != modifier2.Attribute && !(modifier1.Attribute == GenericAttribute.None || modifier2.Attribute == GenericAttribute.None))
                return CompareResult.Uncomparable;
            if (spell1 != spell2 && !(spell1 == MM45SpellIndex.None || spell2 == MM45SpellIndex.None))
                return CompareResult.Uncomparable;

            CompareResult compElement = CompareValues(elemental1.DamageValue, elemental2.DamageValue);
            CompareResult compAttribute = CompareValues(modifier1.Modifier, modifier2.Modifier);
            CompareResult compHDAC = IsWeapon ? CompareValues(hdac1.Damage, hdac2.Damage) : CompareValues(ArmorClassFull, mm45Item.ArmorClassFull);

            CompareResult compAll = Compare(compElement, compAttribute, compHDAC);
            if (compAll == CompareResult.Uncomparable)
                return CompareResult.Uncomparable;

            if (IsWeapon)
                return Compare(compAll, CompareValues(BaseDamage.Average, mm45Item.BaseDamage.Average));
            if (IsArmor)
                return Compare(compAll, CompareValues(ArmorClassFull, mm45Item.ArmorClassFull));
            if (IsAccessory)
                return compAll;
            return CompareResult.Uncomparable;
        }

        public static DamageDice GetItemDamage(MM45Item item)
        {
            switch (item.Base.Type)
            {
                case ItemType.Weapon:
                    DamageDice dd = GetItemDamage(item.Base);
                    HitDamageAC hdac = PrefixHDAC(item.Prefix);
                    ElementDamageResistance elemental = ElementalEffect(item.Prefix);
                    dd.Bonus = hdac.Damage + elemental.DamageValue;
                    return dd;
                default:
                    return DamageDice.Zero;
            }
        }

        public static DamageDice GetItemDamage(MM45ItemBase item)
        {
            if (item.Type != ItemType.Weapon)
                return DamageDice.Zero;

            switch (item.Weapon)
            {
                case MM45WeaponIndex.LongSword: return new DamageDice(3, 3, 0);
                case MM45WeaponIndex.ShortSword: return new DamageDice(3, 2, 0);
                case MM45WeaponIndex.BroadSword: return new DamageDice(4, 3, 0);
                case MM45WeaponIndex.Scimitar: return new DamageDice(5, 2, 0);
                case MM45WeaponIndex.Cutlass: return new DamageDice(2, 4, 0);
                case MM45WeaponIndex.Sabre: return new DamageDice(2, 4, 0);
                case MM45WeaponIndex.Club: return new DamageDice(3, 1, 0);
                case MM45WeaponIndex.HandAxe: return new DamageDice(3, 2, 0);
                case MM45WeaponIndex.Katana: return new DamageDice(3, 4, 0);
                case MM45WeaponIndex.Nunchakas: return new DamageDice(3, 2, 0);
                case MM45WeaponIndex.Wakazashi: return new DamageDice(3, 3, 0);
                case MM45WeaponIndex.Dagger: return new DamageDice(2, 2, 0);
                case MM45WeaponIndex.Mace: return new DamageDice(2, 4, 0);
                case MM45WeaponIndex.Flail: return new DamageDice(10, 1, 0);
                case MM45WeaponIndex.Cudgel: return new DamageDice(6, 1, 0);
                case MM45WeaponIndex.Maul: return new DamageDice(8, 1, 0);
                case MM45WeaponIndex.Spear: return new DamageDice(9, 1, 0);
                case MM45WeaponIndex.Bardiche: return new DamageDice(4, 4, 0);
                case MM45WeaponIndex.Glaive: return new DamageDice(3, 4, 0);
                case MM45WeaponIndex.Halberd: return new DamageDice(6, 3, 0);
                case MM45WeaponIndex.Pike: return new DamageDice(8, 2, 0);
                case MM45WeaponIndex.Flamberge: return new DamageDice(5, 4, 0);
                case MM45WeaponIndex.Trident: return new DamageDice(6, 2, 0);
                case MM45WeaponIndex.Staff: return new DamageDice(4, 2, 0);
                case MM45WeaponIndex.Hammer: return new DamageDice(5, 2, 0);
                case MM45WeaponIndex.Naginata: return new DamageDice(3, 5, 0);
                case MM45WeaponIndex.BattleAxe: return new DamageDice(5, 3, 0);
                case MM45WeaponIndex.GrandAxe: return new DamageDice(6, 3, 0);
                case MM45WeaponIndex.GreatAxe: return new DamageDice(7, 3, 0);
                case MM45WeaponIndex.ShortBow: return new DamageDice(2, 3, 0);
                case MM45WeaponIndex.LongBow: return new DamageDice(2, 5, 0);
                case MM45WeaponIndex.Crossbow: return new DamageDice(2, 4, 0);
                case MM45WeaponIndex.Sling: return new DamageDice(2, 2, 0);
                case MM45WeaponIndex.XeenSlayerSword: return new DamageDice(4, 6, 0);
                default: return DamageDice.Zero;
            }
        }

        public static int GetArmorClass(MM45ItemBase item)
        {
            if (item.Type != ItemType.Armor)
                return 0;

            switch (item.Armor)
            {
                case MM45ArmorIndex.Robes: return 2;
                case MM45ArmorIndex.ScaleArmor: return 4;
                case MM45ArmorIndex.RingMail: return 5;
                case MM45ArmorIndex.ChainMail: return 6;
                case MM45ArmorIndex.SplintMail: return 7;
                case MM45ArmorIndex.PlateMail: return 8;
                case MM45ArmorIndex.PlateArmor: return 10;
                case MM45ArmorIndex.Shield: return 4;
                case MM45ArmorIndex.Helm: return 2;
                case MM45ArmorIndex.Boots: return 1;
                case MM45ArmorIndex.Cloak: return 1;
                case MM45ArmorIndex.Cape: return 1;
                case MM45ArmorIndex.Gauntlets: return 1;
                default: return 0;
            }
        }

        public static int GetArmorClass(MM45Item item)
        {
            int iBaseAC = GetArmorClass(item.Base);

            switch (item.Base.Type)
            {
                case ItemType.Armor:
                case ItemType.Accessory:
                    if (IsAttribute(item.Prefix))
                    {
                        AttributeModifier mod = AttributeEffect(item.Prefix);
                        if (mod.Attribute == GenericAttribute.AC)
                            return iBaseAC + mod.Modifier;
                    }
                    if (item.Base.Type == ItemType.Accessory)
                        return iBaseAC;
                    return iBaseAC + MM45Item.PrefixHDAC(item.Prefix).AC;
                default:
                    return iBaseAC;
            }
        }

        public static MM345UsableFlags GetUsableBy(MM45ItemBase item)
        {
            switch (item.Type)
            {
                case ItemType.Weapon:
                    switch (item.Weapon)
                    {
                        case MM45WeaponIndex.LongSword: return MM345UsableFlags.KPATR;
                        case MM45WeaponIndex.ShortSword: return MM345UsableFlags.KPATR;
                        case MM45WeaponIndex.BroadSword: return MM345UsableFlags.KPATR;
                        case MM45WeaponIndex.Scimitar: return MM345UsableFlags.KPATR;
                        case MM45WeaponIndex.Cutlass: return MM345UsableFlags.KPATR;
                        case MM45WeaponIndex.Sabre: return MM345UsableFlags.KPATR;
                        case MM45WeaponIndex.Club: return MM345UsableFlags.AnyClass;
                        case MM45WeaponIndex.HandAxe: return MM345UsableFlags.KPATNBDR;
                        case MM45WeaponIndex.Katana: return MM345UsableFlags.KPN;
                        case MM45WeaponIndex.Nunchakas: return MM345UsableFlags.KPN;
                        case MM45WeaponIndex.Wakazashi: return MM345UsableFlags.KPN;
                        case MM45WeaponIndex.Dagger: return MM345UsableFlags.KPASTNBDR;
                        case MM45WeaponIndex.Mace: return MM345UsableFlags.KPACTNBDR;
                        case MM45WeaponIndex.Flail: return MM345UsableFlags.KPACTNBDR;
                        case MM45WeaponIndex.Cudgel: return MM345UsableFlags.KPACTNBDR;
                        case MM45WeaponIndex.Maul: return MM345UsableFlags.KPACTNBDR;
                        case MM45WeaponIndex.Spear: return MM345UsableFlags.KPATNBDR;
                        case MM45WeaponIndex.Bardiche: return MM345UsableFlags.KPATNR;
                        case MM45WeaponIndex.Glaive: return MM345UsableFlags.KPATNBR;
                        case MM45WeaponIndex.Halberd: return MM345UsableFlags.KPATNBR;
                        case MM45WeaponIndex.Pike: return MM345UsableFlags.KPATNBR;
                        case MM45WeaponIndex.Flamberge: return MM345UsableFlags.KPAR;
                        case MM45WeaponIndex.Trident: return MM345UsableFlags.KPATNBR;
                        case MM45WeaponIndex.Staff: return MM345UsableFlags.AnyClass;
                        case MM45WeaponIndex.Hammer: return MM345UsableFlags.KPACTNBDR;
                        case MM45WeaponIndex.Naginata: return MM345UsableFlags.KPN;
                        case MM45WeaponIndex.BattleAxe: return MM345UsableFlags.KPATBR;
                        case MM45WeaponIndex.GrandAxe: return MM345UsableFlags.KPATBR;
                        case MM45WeaponIndex.GreatAxe: return MM345UsableFlags.KPATBR;
                        case MM45WeaponIndex.ShortBow: return MM345UsableFlags.KPATNBR;
                        case MM45WeaponIndex.LongBow: return MM345UsableFlags.KPATNBR;
                        case MM45WeaponIndex.Crossbow: return MM345UsableFlags.KPATNBR;
                        case MM45WeaponIndex.Sling: return MM345UsableFlags.KPATNBR;
                        default: return MM345UsableFlags.AnyClass;
                    }
                case ItemType.Armor:
                    switch(item.Armor)
                    {
                        case MM45ArmorIndex.ScaleArmor: return MM345UsableFlags.KPACTNBR;
                        case MM45ArmorIndex.RingMail: return MM345UsableFlags.KPACTNR;
                        case MM45ArmorIndex.ChainMail: return MM345UsableFlags.KPACTR;
                        case MM45ArmorIndex.SplintMail: return MM345UsableFlags.KPCR;
                        case MM45ArmorIndex.PlateMail: return MM345UsableFlags.KP;
                        case MM45ArmorIndex.PlateArmor: return MM345UsableFlags.KP;
                        case MM45ArmorIndex.Shield: return MM345UsableFlags.KPCTBR;
                        default: return MM345UsableFlags.AnyClass;
                    }
                default: return MM345UsableFlags.AnyClass;
            }
        }

        public static string GetItemPrefix(ItemType type, MM45ItemPrefixIndex index)
        {
            switch (type)
            {
                case ItemType.Weapon:
                case ItemType.Armor:
                case ItemType.Accessory:
                case ItemType.Miscellaneous:
                    switch (index)
                    {
                        case MM45ItemPrefixIndex.Burning: return "Burning";
                        case MM45ItemPrefixIndex.Fiery: return "Fiery";
                        case MM45ItemPrefixIndex.Pyric: return "Pyric";
                        case MM45ItemPrefixIndex.Fuming: return "Fuming";
                        case MM45ItemPrefixIndex.Flaming: return "Flaming";
                        case MM45ItemPrefixIndex.Seething: return "Seething";
                        case MM45ItemPrefixIndex.Blazing: return "Blazing";
                        case MM45ItemPrefixIndex.Scorching: return "Scorching";
                        case MM45ItemPrefixIndex.Flickering: return "Flickering";
                        case MM45ItemPrefixIndex.Sparking: return "Sparking";
                        case MM45ItemPrefixIndex.Static: return "Static";
                        case MM45ItemPrefixIndex.Flashing: return "Flashing";
                        case MM45ItemPrefixIndex.Shocking: return "Shocking";
                        case MM45ItemPrefixIndex.Electric: return "Electric";
                        case MM45ItemPrefixIndex.Dyna: return "Dyna";
                        case MM45ItemPrefixIndex.Icy: return "Icy";
                        case MM45ItemPrefixIndex.Frost: return "Frost";
                        case MM45ItemPrefixIndex.Freezing: return "Freezing";
                        case MM45ItemPrefixIndex.Cold: return "Cold";
                        case MM45ItemPrefixIndex.Cryo: return "Cryo";
                        case MM45ItemPrefixIndex.Acidic: return "Acidic";
                        case MM45ItemPrefixIndex.Venemous: return "Venemous";
                        case MM45ItemPrefixIndex.Poisonous: return "Poisonous";
                        case MM45ItemPrefixIndex.Toxic: return "Toxic";
                        case MM45ItemPrefixIndex.Noxious: return "Noxious";
                        case MM45ItemPrefixIndex.Glowing: return "Glowing";
                        case MM45ItemPrefixIndex.Incandescent: return "Incandescent";
                        case MM45ItemPrefixIndex.Dense: return "Dense";
                        case MM45ItemPrefixIndex.Sonic: return "Sonic";
                        case MM45ItemPrefixIndex.PowerEnergy: return "Power";
                        case MM45ItemPrefixIndex.Thermal: return "Thermal";
                        case MM45ItemPrefixIndex.Radiating: return "Radiating";
                        case MM45ItemPrefixIndex.Kinetic: return "Kinetic";
                        case MM45ItemPrefixIndex.Mystic: return "Mystic";
                        case MM45ItemPrefixIndex.Magical: return "Magical";
                        case MM45ItemPrefixIndex.Ectoplasmic: return "Ectoplasmic";
                        case MM45ItemPrefixIndex.Wooden: return "Wooden";
                        case MM45ItemPrefixIndex.Leather: return "Leather";
                        case MM45ItemPrefixIndex.Brass: return "Brass";
                        case MM45ItemPrefixIndex.Bronze: return "Bronze";
                        case MM45ItemPrefixIndex.Iron: return "Iron";
                        case MM45ItemPrefixIndex.Silver: return "Silver";
                        case MM45ItemPrefixIndex.Steel: return "Steel";
                        case MM45ItemPrefixIndex.Gold: return "Gold";
                        case MM45ItemPrefixIndex.Platinum: return "Platinum";
                        case MM45ItemPrefixIndex.Glass: return "Glass";
                        case MM45ItemPrefixIndex.Coral: return "Coral";
                        case MM45ItemPrefixIndex.Crystal: return "Crystal";
                        case MM45ItemPrefixIndex.Lapis: return "Lapis";
                        case MM45ItemPrefixIndex.Pearl: return "Pearl";
                        case MM45ItemPrefixIndex.Amber: return "Amber";
                        case MM45ItemPrefixIndex.Ebony: return "Ebony";
                        case MM45ItemPrefixIndex.Quartz: return "Quartz";
                        case MM45ItemPrefixIndex.Ruby: return "Ruby";
                        case MM45ItemPrefixIndex.Emerald: return "Emerald";
                        case MM45ItemPrefixIndex.Sapphire: return "Sapphire";
                        case MM45ItemPrefixIndex.Diamond: return "Diamond";
                        case MM45ItemPrefixIndex.Obsidian: return "Obsidian";
                        case MM45ItemPrefixIndex.Might: return "Might";
                        case MM45ItemPrefixIndex.Strength: return "Strength";
                        case MM45ItemPrefixIndex.Warrior: return "Warrior";
                        case MM45ItemPrefixIndex.Ogre: return "Ogre";
                        case MM45ItemPrefixIndex.Giant: return "Giant";
                        case MM45ItemPrefixIndex.Thunder: return "Thunder";
                        case MM45ItemPrefixIndex.Force: return "Force";
                        case MM45ItemPrefixIndex.PowerMight: return "Power";
                        case MM45ItemPrefixIndex.Dragon: return "Dragon";
                        case MM45ItemPrefixIndex.Photon: return "Photon";
                        case MM45ItemPrefixIndex.Clever: return "Clever";
                        case MM45ItemPrefixIndex.Mind: return "Mind";
                        case MM45ItemPrefixIndex.Sage: return "Sage";
                        case MM45ItemPrefixIndex.Thought: return "Thought";
                        case MM45ItemPrefixIndex.Knowledge: return "Knowledge";
                        case MM45ItemPrefixIndex.Intellect: return "Intellect";
                        case MM45ItemPrefixIndex.Wisdom: return "Wisdom";
                        case MM45ItemPrefixIndex.Genius: return "Genius";
                        case MM45ItemPrefixIndex.Buddy: return "Buddy";
                        case MM45ItemPrefixIndex.Friendship: return "Friendship";
                        case MM45ItemPrefixIndex.Charm: return "Charm";
                        case MM45ItemPrefixIndex.Personality: return "Personality";
                        case MM45ItemPrefixIndex.Charisma: return "Charisma";
                        case MM45ItemPrefixIndex.Leadership: return "Leadership";
                        case MM45ItemPrefixIndex.Ego: return "Ego";
                        case MM45ItemPrefixIndex.Holy: return "Holy";
                        case MM45ItemPrefixIndex.Quick: return "Quick";
                        case MM45ItemPrefixIndex.Swift: return "Swift";
                        case MM45ItemPrefixIndex.Fast: return "Fast";
                        case MM45ItemPrefixIndex.Rapid: return "Rapid";
                        case MM45ItemPrefixIndex.Speed: return "Speed";
                        case MM45ItemPrefixIndex.Wind: return "Wind";
                        case MM45ItemPrefixIndex.Accelerator: return "Accelerator";
                        case MM45ItemPrefixIndex.Velocity: return "Velocity";
                        case MM45ItemPrefixIndex.Sharp: return "Sharp";
                        case MM45ItemPrefixIndex.Accurate: return "Accurate";
                        case MM45ItemPrefixIndex.Marksman: return "Marksman";
                        case MM45ItemPrefixIndex.Precision: return "Precision";
                        case MM45ItemPrefixIndex.True: return "True";
                        case MM45ItemPrefixIndex.Exacto: return "Exacto";
                        case MM45ItemPrefixIndex.Clover: return "Clover";
                        case MM45ItemPrefixIndex.Chance: return "Chance";
                        case MM45ItemPrefixIndex.Winners: return "Winners";
                        case MM45ItemPrefixIndex.Lucky: return "Lucky";
                        case MM45ItemPrefixIndex.Gamblers: return "Gamblers";
                        case MM45ItemPrefixIndex.Leprechauns: return "Leprechauns";
                        case MM45ItemPrefixIndex.Vigor: return "Vigor";
                        case MM45ItemPrefixIndex.Health: return "Health";
                        case MM45ItemPrefixIndex.Life: return "Life";
                        case MM45ItemPrefixIndex.Troll: return "Troll";
                        case MM45ItemPrefixIndex.Vampiric: return "Vampiric";
                        case MM45ItemPrefixIndex.Spell: return "Spell";
                        case MM45ItemPrefixIndex.Castors: return "Castors";
                        case MM45ItemPrefixIndex.Witch: return "Witch";
                        case MM45ItemPrefixIndex.Mage: return "Mage";
                        case MM45ItemPrefixIndex.Archmage: return "Archmage";
                        case MM45ItemPrefixIndex.Arcane: return "Arcane";
                        case MM45ItemPrefixIndex.Protection: return "Protection";
                        case MM45ItemPrefixIndex.Armored: return "Armored";
                        case MM45ItemPrefixIndex.Defender: return "Defender";
                        case MM45ItemPrefixIndex.Stealth: return "Stealth";
                        case MM45ItemPrefixIndex.Divine: return "Divine";
                        case MM45ItemPrefixIndex.Mugger: return "Mugger";
                        case MM45ItemPrefixIndex.Burgler: return "Burgler";
                        case MM45ItemPrefixIndex.Looter: return "Looter";
                        case MM45ItemPrefixIndex.Brigand: return "Brigand";
                        case MM45ItemPrefixIndex.Filch: return "Filch";
                        case MM45ItemPrefixIndex.Thief: return "Thief";
                        case MM45ItemPrefixIndex.Rogue: return "Rogue";
                        case MM45ItemPrefixIndex.Plunder: return "Plunder";
                        case MM45ItemPrefixIndex.Criminal: return "Criminal";
                        case MM45ItemPrefixIndex.Pirate: return "Pirate";
                        case MM45ItemPrefixIndex.None: return String.Empty;
                        default: return "<unknown prefix>";
                    }
                default: return "<unknown>";
            }
        }

        public static string WeaponSuffixString(MM45WeaponSuffixIndex effect)
        {
            switch (effect)
            {
                case MM45WeaponSuffixIndex.DragonSlayer: return "Dragon Slayer";
                case MM45WeaponSuffixIndex.UndeadEater: return "Undead Eater";
                case MM45WeaponSuffixIndex.GolemSmasher: return "Golem Smasher";
                case MM45WeaponSuffixIndex.BugZapper: return "Bug Zapper";
                case MM45WeaponSuffixIndex.MonsterMasher: return "Monster Masher";
                case MM45WeaponSuffixIndex.BeastBopper: return "Beast Bopper";
                case MM45WeaponSuffixIndex.None: return String.Empty;
                default: return "Unknown Weapon Suffix";
            }
        }

        public static string WeaponSuffixEffect(MM45WeaponSuffixIndex effect, bool bAbbrev = false)
        {
            string strFormat = bAbbrev ? "x3 {0}" : "x3 vs. {0}";

            switch (effect)
            {
                case MM45WeaponSuffixIndex.DragonSlayer: return String.Format(strFormat, "Dragons");
                case MM45WeaponSuffixIndex.UndeadEater: return String.Format(strFormat, "Undead");
                case MM45WeaponSuffixIndex.GolemSmasher: return String.Format(strFormat, "Golems");
                case MM45WeaponSuffixIndex.BugZapper: return String.Format(strFormat, "Insects");
                case MM45WeaponSuffixIndex.MonsterMasher: return String.Format(strFormat, "Unique");
                case MM45WeaponSuffixIndex.BeastBopper: return String.Format(strFormat, "Animals");
                case MM45WeaponSuffixIndex.None: return String.Empty;
                default: return bAbbrev ? "?x3" : "Unknown Weapon Suffix";
            }
        }

        public static string GetItemSuffix(ItemType type, int index)
        {
            switch (type)
            {
                case ItemType.Weapon:
                    return WeaponSuffixString((MM45WeaponSuffixIndex)index);
                case ItemType.Armor:
                case ItemType.Accessory:
                    return String.Empty;
                case ItemType.Miscellaneous:
                    switch ((MM45ItemSuffixIndex)index)
                    {
                        case MM45ItemSuffixIndex.Light: return "Light";
                        case MM45ItemSuffixIndex.Awakening: return "Awakening";
                        case MM45ItemSuffixIndex.MagicArrows: return "Magic Arrows";
                        case MM45ItemSuffixIndex.FirstAid: return "First Aid";
                        case MM45ItemSuffixIndex.Fists: return "Fists";
                        case MM45ItemSuffixIndex.EnergyBlasts: return "Energy Blasts";
                        case MM45ItemSuffixIndex.Sleeping: return "Sleeping";
                        case MM45ItemSuffixIndex.Revitalization: return "Revitalization";
                        case MM45ItemSuffixIndex.Curing: return "Curing";
                        case MM45ItemSuffixIndex.Sparking: return "Sparking";
                        case MM45ItemSuffixIndex.Shrapmetal: return "Shrapmetal";
                        case MM45ItemSuffixIndex.InsectRepellent: return "Insect Repellent";
                        case MM45ItemSuffixIndex.ToxicClouds: return "Toxic Clouds";
                        case MM45ItemSuffixIndex.ElementalProtection: return "Elemental Protection";
                        case MM45ItemSuffixIndex.Pain: return "Pain";
                        case MM45ItemSuffixIndex.Jumping: return "Jumping";
                        case MM45ItemSuffixIndex.BeastControl: return "Beast Control";
                        case MM45ItemSuffixIndex.Clairvoyance: return "Clairvoyance";
                        case MM45ItemSuffixIndex.UndeadTurning: return "Undead Turning";
                        case MM45ItemSuffixIndex.Levitation: return "Levitation";
                        case MM45ItemSuffixIndex.WizardEyes: return "Wizard Eyes";
                        case MM45ItemSuffixIndex.Blessing: return "Blessing";
                        case MM45ItemSuffixIndex.MonsterIdentification: return "Monster Identification";
                        case MM45ItemSuffixIndex.Lightning: return "Lightning";
                        case MM45ItemSuffixIndex.HolyBonuses: return "Holy Bonuses";
                        case MM45ItemSuffixIndex.PowerCuring: return "Power Curing";
                        case MM45ItemSuffixIndex.NaturesCures: return "Nature's Cures";
                        case MM45ItemSuffixIndex.Beacons: return "Beacons";
                        case MM45ItemSuffixIndex.Shielding: return "Shielding";
                        case MM45ItemSuffixIndex.Heroism: return "Heroism";
                        case MM45ItemSuffixIndex.Hypnotism: return "Hypnotism";
                        case MM45ItemSuffixIndex.WaterWalking: return "Water Walking";
                        case MM45ItemSuffixIndex.FrostBiting: return "Frost Biting";
                        case MM45ItemSuffixIndex.MonsterFinding: return "Monster Finding";
                        case MM45ItemSuffixIndex.Fireballs: return "Fireballs";
                        case MM45ItemSuffixIndex.ColdRays: return "Cold Rays";
                        case MM45ItemSuffixIndex.Antidotes: return "Antidotes";
                        case MM45ItemSuffixIndex.AcidSpraying: return "Acid Spraying";
                        case MM45ItemSuffixIndex.TimeDistortion: return "Time Distortion";
                        case MM45ItemSuffixIndex.DragonSleep: return "Dragon Sleep";
                        case MM45ItemSuffixIndex.Vaccination: return "Vaccination";
                        case MM45ItemSuffixIndex.Teleportation: return "Teleportation";
                        case MM45ItemSuffixIndex.Death: return "Death";
                        case MM45ItemSuffixIndex.FreeMovement: return "Free Movement";
                        case MM45ItemSuffixIndex.GolemStopping: return "Golem Stopping";
                        case MM45ItemSuffixIndex.PoisonVolleys: return "Poison Volleys";
                        case MM45ItemSuffixIndex.DeadlySwarms: return "Deadly Swarms";
                        case MM45ItemSuffixIndex.Shelter: return "Shelter";
                        case MM45ItemSuffixIndex.DailyProtection: return "Daily Protection";
                        case MM45ItemSuffixIndex.DailySorcerery: return "Daily Sorcerery";
                        case MM45ItemSuffixIndex.Feasting: return "Feasting";
                        case MM45ItemSuffixIndex.FieryFlails: return "Fiery Flails";
                        case MM45ItemSuffixIndex.Recharging: return "Recharging";
                        case MM45ItemSuffixIndex.Freezing: return "Freezing";
                        case MM45ItemSuffixIndex.TownPortals: return "Town Portals";
                        case MM45ItemSuffixIndex.StoneToFlesh: return "Stone to Flesh";
                        case MM45ItemSuffixIndex.RaisingTheDead: return "Raising the Dead";
                        case MM45ItemSuffixIndex.Etherealization: return "Etherealization";
                        case MM45ItemSuffixIndex.DancingSwords: return "Dancing Swords";
                        case MM45ItemSuffixIndex.MoonRays: return "Moon Rays";
                        case MM45ItemSuffixIndex.MassDistortion: return "Mass Distortion";
                        case MM45ItemSuffixIndex.PrismaticLight: return "Prismatic Light";
                        case MM45ItemSuffixIndex.EnchantItem: return "Enchant Item";
                        case MM45ItemSuffixIndex.Incinerating: return "Incinerating";
                        case MM45ItemSuffixIndex.HolyWords: return "Holy Words";
                        case MM45ItemSuffixIndex.Resurrection: return "Resurrection";
                        case MM45ItemSuffixIndex.Storms: return "Storms";
                        case MM45ItemSuffixIndex.Megavoltage: return "Megavoltage";
                        case MM45ItemSuffixIndex.Infernos: return "Infernos";
                        case MM45ItemSuffixIndex.SunRays: return "Sun Rays";
                        case MM45ItemSuffixIndex.Implosions: return "Implosions";
                        case MM45ItemSuffixIndex.StarBursts: return "Star Bursts";
                        case MM45ItemSuffixIndex.TheGODS: return "the GODS!";
                        case MM45ItemSuffixIndex.None: return String.Empty;
                        default: return "<unknown>";
                    }
                default: return "<unknown>";
            }
        }

        public static string GetItemName(MM45QuestItemIndex item)
        {
            switch (item)
            {
                case MM45QuestItemIndex.DeedToNewCastle: return "Deed to New Castle";
                case MM45QuestItemIndex.CrystalKeyToWitchTower: return "Crystal Key to Witch Tower";
                case MM45QuestItemIndex.SkeletonKeyToDarzogsTower: return "Skeleton Key to Darzog's Tower";
                case MM45QuestItemIndex.EnchantedKeyToTowerOfHighMagic: return "Enchanted Key to Tower of High Magic";
                case MM45QuestItemIndex.JeweledAmuletOfTheNorthernSphinx: return "Jeweled Amulet of the Northern Sphinx";
                case MM45QuestItemIndex.StoneOfAThousandTerrors: return "Stone of a Thousand Terrors";
                case MM45QuestItemIndex.GolemStoneOfAdmittance: return "Golem Stone of Admittance";
                case MM45QuestItemIndex.YakStoneOfOpening: return "Yak Stone of Opening";
                case MM45QuestItemIndex.XeensScepterOfTemporalDistortion: return "Xeen's Scepter of Temporal Distortion";
                case MM45QuestItemIndex.AlacornOfFalista: return "Alacorn of Falista";
                case MM45QuestItemIndex.ElixirOfRestoration: return "Elixir of Restoration";
                case MM45QuestItemIndex.WandOfFaeryMagic: return "Wand of Faery Magic";
                case MM45QuestItemIndex.PrincessRoxannesTiara: return "Princess Roxanne's Tiara";
                case MM45QuestItemIndex.HolyBookOfElvenkind: return "Holy Book of Elvenkind";
                case MM45QuestItemIndex.ScarabOfImaging: return "Scarab of Imaging";
                case MM45QuestItemIndex.CrystalsOfPiezoelectricity: return "Crystals of Piezoelectricity";
                case MM45QuestItemIndex.ScrollOfInsight: return "Scroll of Insight";
                case MM45QuestItemIndex.PhirnaRoot: return "Phirna Root";
                case MM45QuestItemIndex.OrothinsBoneWhistle: return "Orothin's Bone Whistle";
                case MM45QuestItemIndex.BaroksMagicPendant: return "Barok's Magic Pendant";
                case MM45QuestItemIndex.LigonosMissingSkull: return "Ligono's Missing Skull";
                case MM45QuestItemIndex.LastFlowerOfSummer: return "Last Flower of Summer";
                case MM45QuestItemIndex.LastRaindropOfSpring: return "Last Raindrop of Spring";
                case MM45QuestItemIndex.LastSnowflakeOfWinter: return "Last Snowflake of Winter";
                case MM45QuestItemIndex.LastLeafOfAutumn: return "Last Leaf of Autumn";
                case MM45QuestItemIndex.EverHotLavaRock: return "Ever Hot Lava Rock";
                case MM45QuestItemIndex.KingsMegaCredit: return "King's Mega Credit";
                case MM45QuestItemIndex.ExcavationPermit: return "Excavation Permit";
                case MM45QuestItemIndex.CupieDoll: return "Cupie Doll";
                case MM45QuestItemIndex.MightDoll: return "Might Doll";
                case MM45QuestItemIndex.SpeedDoll: return "Speed Doll";
                case MM45QuestItemIndex.EnduranceDoll: return "Endurance Doll";
                case MM45QuestItemIndex.AccuracyDoll: return "Accuracy Doll";
                case MM45QuestItemIndex.LuckDoll: return "Luck Doll";
                case MM45QuestItemIndex.Widget: return "Widget";
                case MM45QuestItemIndex.PassToCastleview: return "Pass to Castleview";
                case MM45QuestItemIndex.PassToSandcaster: return "Pass to Sandcaster";
                case MM45QuestItemIndex.PassToLakeside: return "Pass to Lakeside";
                case MM45QuestItemIndex.PassToNecropolis: return "Pass to Necropolis";
                case MM45QuestItemIndex.PassToOlympus: return "Pass to Olympus";
                case MM45QuestItemIndex.KeyToGreatWesternTower: return "Key to Great Western Tower";
                case MM45QuestItemIndex.KeyToGreatSouthernTower: return "Key to Great Southern Tower";
                case MM45QuestItemIndex.KeyToGreatEasternTower: return "Key to Great Eastern Tower";
                case MM45QuestItemIndex.KeyToGreatNorthernTower: return "Key to Great Northern Tower";
                case MM45QuestItemIndex.KeyToEllingersTower: return "Key to Ellinger's Tower";
                case MM45QuestItemIndex.KeyToDragonTower: return "Key to Dragon Tower";
                case MM45QuestItemIndex.KeyToDarkstoneTower: return "Key to Darkstone Tower";
                case MM45QuestItemIndex.KeyToTempleOfBark: return "Key to Temple of Bark";
                case MM45QuestItemIndex.KeyToDungeonOfLostSouls: return "Key to Dungeon of Lost Souls";
                case MM45QuestItemIndex.KeyToAncientPyramid: return "Key to Ancient Pyramid";
                case MM45QuestItemIndex.KeyToDungeonOfDeath: return "Key to Dungeon of Death";
                case MM45QuestItemIndex.AmuletOfTheSouthernSphinx: return "Amulet of the Southern Sphinx";
                case MM45QuestItemIndex.DragonPharoahsOrb: return "Dragon Pharoah's Orb";
                case MM45QuestItemIndex.CubeOfPower: return "Cube of Power";
                case MM45QuestItemIndex.ChimeOfOpening: return "Chime of Opening";
                case MM45QuestItemIndex.GoldIDCard: return "Gold ID Card";
                case MM45QuestItemIndex.SilverIDCard: return "Silver ID Card";
                case MM45QuestItemIndex.VultureRepellant: return "Vulture Repellant";
                case MM45QuestItemIndex.Bridle: return "Bridle";
                case MM45QuestItemIndex.EnchantedBridle: return "Enchanted Bridle";
                case MM45QuestItemIndex.TreasureMap: return "Treasure Map (Goto E1 x1, y11)";
                case MM45QuestItemIndex.NotUseD: return "NOTUSED";
                case MM45QuestItemIndex.FakeMap: return "Fake Map";
                case MM45QuestItemIndex.OnyxNecklace: return "Onyx Necklace";
                case MM45QuestItemIndex.DragonEgg: return "Dragon Egg";
                case MM45QuestItemIndex.Tribble: return "Tribble";
                case MM45QuestItemIndex.GoldenPegasusStatuette: return "Golden Pegasus Statuette";
                case MM45QuestItemIndex.GoldenDragonStatuette: return "Golden Dragon Statuette";
                case MM45QuestItemIndex.GoldenGriffinStatuette: return "Golden Griffin Statuette";
                case MM45QuestItemIndex.ChaliceOfProtection: return "Chalice of Protection";
                case MM45QuestItemIndex.JewelOfAges: return "Jewel of Ages";
                case MM45QuestItemIndex.SongbirdOfSerenity: return "Songbird of Serenity";
                case MM45QuestItemIndex.SandrosHeart: return "Sandro's Heart";
                case MM45QuestItemIndex.EctorsRing: return "Ector's Ring";
                case MM45QuestItemIndex.VesparsEmeraldHandle: return "Vespar's Emerald Handle";
                case MM45QuestItemIndex.QueenKalindrasCrown: return "Queen Kalindra's Crown";
                case MM45QuestItemIndex.CalebsMagnifyingGlass: return "Caleb's Magnifying Glass";
                case MM45QuestItemIndex.SoulBox: return "Soul Box";
                case MM45QuestItemIndex.SoulBoxWithCorakInside: return "Soul Box with Corak inside";
                case MM45QuestItemIndex.RubyRock: return "Ruby Rock";
                case MM45QuestItemIndex.EmeraldRock: return "Emerald Rock";
                case MM45QuestItemIndex.SapphireRock: return "Sapphire Rock";
                case MM45QuestItemIndex.DiamondRock: return "Diamond Rock";
                case MM45QuestItemIndex.MongaMelon: return "Monga Melon";
                case MM45QuestItemIndex.EnergyDisk: return "Energy Disk";
                default: return String.Format("<Quest Item {0}>", (int) item);
            }                
        }

        public static string GetItemName(MM45ItemBase item)
        {
            switch (item.Type)
            {
                case ItemType.Weapon:
                    switch (item.Weapon)
                    {
                        case MM45WeaponIndex.LongSword: return "Long Sword";
                        case MM45WeaponIndex.ShortSword: return "Short Sword";
                        case MM45WeaponIndex.BroadSword: return "Broad Sword";
                        case MM45WeaponIndex.Scimitar: return "Scimitar";
                        case MM45WeaponIndex.Cutlass: return "Cutlass";
                        case MM45WeaponIndex.Sabre: return "Sabre";
                        case MM45WeaponIndex.Club: return "Club";
                        case MM45WeaponIndex.HandAxe: return "Hand Axe";
                        case MM45WeaponIndex.Katana: return "Katana";
                        case MM45WeaponIndex.Nunchakas: return "Nunchakas";
                        case MM45WeaponIndex.Wakazashi: return "Wakazashi";
                        case MM45WeaponIndex.Dagger: return "Dagger";
                        case MM45WeaponIndex.Mace: return "Mace";
                        case MM45WeaponIndex.Flail: return "Flail";
                        case MM45WeaponIndex.Cudgel: return "Cudgel";
                        case MM45WeaponIndex.Maul: return "Maul";
                        case MM45WeaponIndex.Spear: return "Spear";
                        case MM45WeaponIndex.Bardiche: return "Bardiche";
                        case MM45WeaponIndex.Glaive: return "Glaive";
                        case MM45WeaponIndex.Halberd: return "Halberd";
                        case MM45WeaponIndex.Pike: return "Pike";
                        case MM45WeaponIndex.Flamberge: return "Flamberge";
                        case MM45WeaponIndex.Trident: return "Trident";
                        case MM45WeaponIndex.Staff: return "Staff";
                        case MM45WeaponIndex.Hammer: return "Hammer";
                        case MM45WeaponIndex.Naginata: return "Naginata";
                        case MM45WeaponIndex.BattleAxe: return "Battle Axe";
                        case MM45WeaponIndex.GrandAxe: return "Grand Axe";
                        case MM45WeaponIndex.GreatAxe: return "Great Axe";
                        case MM45WeaponIndex.ShortBow: return "Short Bow";
                        case MM45WeaponIndex.LongBow: return "Long Bow";
                        case MM45WeaponIndex.Crossbow: return "Crossbow";
                        case MM45WeaponIndex.Sling: return "Sling";
                        case MM45WeaponIndex.XeenSlayerSword: return "Xeen Slayer Sword";
                        default: return "<unknown weapon>";
                    }
                case ItemType.Armor:
                    switch (item.Armor)
                    {
                        case MM45ArmorIndex.Robes: return "Robes";
                        case MM45ArmorIndex.ScaleArmor: return "Scale Armor";
                        case MM45ArmorIndex.RingMail: return "Ring Mail";
                        case MM45ArmorIndex.ChainMail: return "Chain Mail";
                        case MM45ArmorIndex.SplintMail: return "Splint Mail";
                        case MM45ArmorIndex.PlateMail: return "Plate Mail";
                        case MM45ArmorIndex.PlateArmor: return "Plate Armor";
                        case MM45ArmorIndex.Shield: return "Shield";
                        case MM45ArmorIndex.Helm: return "Helm";
                        case MM45ArmorIndex.Boots: return "Boots";
                        case MM45ArmorIndex.Cloak: return "Cloak";
                        case MM45ArmorIndex.Cape: return "Cape";
                        case MM45ArmorIndex.Gauntlets: return "Gauntlets";
                        default: return "<unknown armor>";
                    }
                case ItemType.Accessory:
                    switch (item.Accessory)
                    {
                        case MM45AccessoryIndex.Ring: return "Ring";
                        case MM45AccessoryIndex.Belt: return "Belt";
                        case MM45AccessoryIndex.Broach: return "Broach";
                        case MM45AccessoryIndex.Medal: return "Medal";
                        case MM45AccessoryIndex.Charm: return "Charm";
                        case MM45AccessoryIndex.Cameo: return "Cameo";
                        case MM45AccessoryIndex.Scarab: return "Scarab";
                        case MM45AccessoryIndex.Pendant: return "Pendant";
                        case MM45AccessoryIndex.Necklace: return "Necklace";
                        case MM45AccessoryIndex.Amulet: return "Amulet";
                        default: return "<unknown accessory>";
                    }
                case ItemType.Miscellaneous:
                    switch (item.Miscellaneous)
                    {
                        case MM45MiscItemIndex.Rod: return "Rod";
                        case MM45MiscItemIndex.Jewel: return "Jewel";
                        case MM45MiscItemIndex.Gem: return "Gem";
                        case MM45MiscItemIndex.Box: return "Box";
                        case MM45MiscItemIndex.Orb: return "Orb";
                        case MM45MiscItemIndex.Horn: return "Horn";
                        case MM45MiscItemIndex.Coin: return "Coin";
                        case MM45MiscItemIndex.Wand: return "Wand";
                        case MM45MiscItemIndex.Whistle: return "Whistle";
                        case MM45MiscItemIndex.Potion: return "Potion";
                        case MM45MiscItemIndex.Scroll: return "Scroll";
                        case MM45MiscItemIndex.Bogus: return "Bogus";
                        default: return "<unknown misc>";
                    }
                default: return "<unknown>";
            }
        }

        public override string LargestBonusEffect
        {
            get
            {
                if (!IsMiscellaneous)
                {
                    if (IsElemental(Prefix))
                    {
                        if (IsWeapon)
                            return Global.SingleResistance(ElementalEffect(Prefix).DamageElement);
                        return String.Format("{0} Res", Global.SingleResistance(ElementalEffect(Prefix).DamageElement));
                    }
                    if (IsAttribute(Prefix))
                        return AttributeString;
                    if (IsMaterial(Prefix))
                    {
                        HitDamageAC hdac = PrefixHDAC(Prefix);
                        if (IsWeapon)
                            return "Damage";
                        else
                            return "Armor Class";
                    }
                }
                return String.Empty;
            }
        }

        public override int LargestBonus
        {
            get
            {
                if (!IsMiscellaneous)
                {
                    if (IsElemental(Prefix))
                    {
                        if (IsWeapon)
                            return ElementalEffect(Prefix).DamageValue;
                        return ElementalEffect(Prefix).ResistValue;
                    }
                    if (IsAttribute(Prefix))
                        return AttributeEffect(Prefix).Modifier;
                    if (IsMaterial(Prefix))
                    {
                        HitDamageAC hdac = PrefixHDAC(Prefix);
                        if (IsWeapon)
                            return hdac.Damage;
                        return hdac.AC;
                    }
                }
                return 0;
            }
        }

        public static string MiscItemSuffixEffect(MM45ItemSuffixIndex suffix, bool bIncludeLevel = true)
        {
            MM45SpellIndex spell = ItemSuffixEffect(ItemType.Miscellaneous, (int)suffix);
            if (spell == MM45SpellIndex.None)
                return String.Empty;
            int value = SuffixValueModifier(suffix);
            string strEffect = String.Format("{0}Value +{1}", Global.AddCommaSpace(MM45SpellList.GetSpellName(spell)), value);
            if (bIncludeLevel)
                return String.Format("{0}, Level {1}", strEffect, value / 100);
            return strEffect;
        }

        public static MM45SpellIndex ItemSuffixEffect(ItemType type, int suffix)
        {
            switch (type)
            {
                case ItemType.Miscellaneous:
                    switch ((MM45ItemSuffixIndex)suffix)
                    {
                        case MM45ItemSuffixIndex.Light: return MM45SpellIndex.Light;
                        case MM45ItemSuffixIndex.Awakening: return MM45SpellIndex.Awaken;
                        case MM45ItemSuffixIndex.MagicArrows: return MM45SpellIndex.MagicArrow;
                        case MM45ItemSuffixIndex.FirstAid: return MM45SpellIndex.FirstAid;
                        case MM45ItemSuffixIndex.Fists: return MM45SpellIndex.FlyingFist;
                        case MM45ItemSuffixIndex.EnergyBlasts: return MM45SpellIndex.EnergyBlast;
                        case MM45ItemSuffixIndex.Sleeping: return MM45SpellIndex.Sleep;
                        case MM45ItemSuffixIndex.Revitalization: return MM45SpellIndex.Revitalize;
                        case MM45ItemSuffixIndex.Curing: return MM45SpellIndex.CureWounds;
                        case MM45ItemSuffixIndex.Sparking: return MM45SpellIndex.Sparks;
                        case MM45ItemSuffixIndex.Shrapmetal: return MM45SpellIndex.Shrapmetal;
                        case MM45ItemSuffixIndex.InsectRepellent: return MM45SpellIndex.InsectSpray;
                        case MM45ItemSuffixIndex.ToxicClouds: return MM45SpellIndex.ToxicCloud;
                        case MM45ItemSuffixIndex.ElementalProtection: return MM45SpellIndex.ProtFromElements;
                        case MM45ItemSuffixIndex.Pain: return MM45SpellIndex.Pain;
                        case MM45ItemSuffixIndex.Jumping: return MM45SpellIndex.Jump;
                        case MM45ItemSuffixIndex.BeastControl: return MM45SpellIndex.BeastMaster;
                        case MM45ItemSuffixIndex.Clairvoyance: return MM45SpellIndex.Clairvoyance;
                        case MM45ItemSuffixIndex.AcidSpraying: return MM45SpellIndex.AcidSpray;
                        case MM45ItemSuffixIndex.UndeadTurning: return MM45SpellIndex.TurnUndead;
                        case MM45ItemSuffixIndex.Levitation: return MM45SpellIndex.Levitate;
                        case MM45ItemSuffixIndex.WizardEyes: return MM45SpellIndex.WizardEye;
                        case MM45ItemSuffixIndex.Blessing: return MM45SpellIndex.Bless;
                        case MM45ItemSuffixIndex.MonsterIdentification: return MM45SpellIndex.IdentifyMonster;
                        case MM45ItemSuffixIndex.Lightning: return MM45SpellIndex.LightningBolt;
                        case MM45ItemSuffixIndex.HolyBonuses: return MM45SpellIndex.HolyBonus;
                        case MM45ItemSuffixIndex.PowerCuring: return MM45SpellIndex.PowerCure;
                        case MM45ItemSuffixIndex.NaturesCures: return MM45SpellIndex.NaturesCure;
                        case MM45ItemSuffixIndex.Beacons: return MM45SpellIndex.LloydsBeacon;
                        case MM45ItemSuffixIndex.Shielding: return MM45SpellIndex.PowerShield;
                        case MM45ItemSuffixIndex.Heroism: return MM45SpellIndex.Heroism;
                        case MM45ItemSuffixIndex.Hypnotism: return MM45SpellIndex.Hypnotize;
                        case MM45ItemSuffixIndex.WaterWalking: return MM45SpellIndex.WalkOnWater;
                        case MM45ItemSuffixIndex.FrostBiting: return MM45SpellIndex.FrostBite;
                        case MM45ItemSuffixIndex.MonsterFinding: return MM45SpellIndex.DetectMonster;
                        case MM45ItemSuffixIndex.Fireballs: return MM45SpellIndex.Fireball;
                        case MM45ItemSuffixIndex.ColdRays: return MM45SpellIndex.ColdRay;
                        case MM45ItemSuffixIndex.Antidotes: return MM45SpellIndex.CurePoison;
                        case MM45ItemSuffixIndex.TimeDistortion: return MM45SpellIndex.TimeDistortion;
                        case MM45ItemSuffixIndex.Vaccination: return MM45SpellIndex.CureDisease;
                        case MM45ItemSuffixIndex.Teleportation: return MM45SpellIndex.Teleport;
                        case MM45ItemSuffixIndex.Death: return MM45SpellIndex.FingerOfDeath;
                        case MM45ItemSuffixIndex.FreeMovement: return MM45SpellIndex.CureParalysis;
                        case MM45ItemSuffixIndex.GolemStopping: return MM45SpellIndex.GolemStopper;
                        case MM45ItemSuffixIndex.PoisonVolleys: return MM45SpellIndex.PoisonVolley;
                        case MM45ItemSuffixIndex.DeadlySwarms: return MM45SpellIndex.DeadlySwarm;
                        case MM45ItemSuffixIndex.Shelter: return MM45SpellIndex.SuperShelter;
                        case MM45ItemSuffixIndex.DailyProtection: return MM45SpellIndex.DayOfProtection;
                        case MM45ItemSuffixIndex.DailySorcerery: return MM45SpellIndex.DayOfSorcery;
                        case MM45ItemSuffixIndex.DragonSleep: return MM45SpellIndex.DragonSleep;
                        case MM45ItemSuffixIndex.Feasting: return MM45SpellIndex.CreateFood;
                        case MM45ItemSuffixIndex.FieryFlails: return MM45SpellIndex.FieryFlail;
                        case MM45ItemSuffixIndex.Recharging: return MM45SpellIndex.RechargeItem;
                        case MM45ItemSuffixIndex.Freezing: return MM45SpellIndex.FantasticFreeze;
                        case MM45ItemSuffixIndex.TownPortals: return MM45SpellIndex.TownPortal;
                        case MM45ItemSuffixIndex.StoneToFlesh: return MM45SpellIndex.StoneToFlesh;
                        case MM45ItemSuffixIndex.RaisingTheDead: return MM45SpellIndex.RaiseDead;
                        case MM45ItemSuffixIndex.Etherealization: return MM45SpellIndex.Etherealize;
                        case MM45ItemSuffixIndex.DancingSwords: return MM45SpellIndex.DancingSword;
                        case MM45ItemSuffixIndex.MoonRays: return MM45SpellIndex.MoonRay;
                        case MM45ItemSuffixIndex.MassDistortion: return MM45SpellIndex.MassDistortion;
                        case MM45ItemSuffixIndex.PrismaticLight: return MM45SpellIndex.PrismaticLight;
                        case MM45ItemSuffixIndex.EnchantItem: return MM45SpellIndex.EnchantItem;
                        case MM45ItemSuffixIndex.Incinerating: return MM45SpellIndex.Incinerate;
                        case MM45ItemSuffixIndex.HolyWords: return MM45SpellIndex.HolyWord;
                        case MM45ItemSuffixIndex.Resurrection: return MM45SpellIndex.Resurrect;
                        case MM45ItemSuffixIndex.Storms: return MM45SpellIndex.ElementalStorm;
                        case MM45ItemSuffixIndex.Megavoltage: return MM45SpellIndex.MegaVolts;
                        case MM45ItemSuffixIndex.Infernos: return MM45SpellIndex.Inferno;
                        case MM45ItemSuffixIndex.SunRays: return MM45SpellIndex.SunRay;
                        case MM45ItemSuffixIndex.Implosions: return MM45SpellIndex.Implosion;
                        case MM45ItemSuffixIndex.StarBursts: return MM45SpellIndex.StarBurst;
                        case MM45ItemSuffixIndex.TheGODS: return MM45SpellIndex.DivineIntervention;
                        default: return MM45SpellIndex.None;
                    }
                default:
                    return MM45SpellIndex.None;
            }
        }

        public override string AttributeString
        {
            get
            {
                if (!IsAttribute(Prefix))
                    return String.Empty;
                return Global.GenericAttributeString(AttributeEffect(Prefix).Attribute);
            }
        }

        public override int EquipBonusValue
        {
            get
            {
                if (!IsAttribute(Prefix))
                    return 0;
                return AttributeEffect(Prefix).Modifier;
            }
        }

        public override string UseEffectString
        {
            get
            {
                if (Base.Type != ItemType.Miscellaneous || ItemSuffix == MM45ItemSuffixIndex.None)
                    return String.Empty;

                return MM45SpellList.GetSpellName(ItemSuffixEffect(Base.Type, Suffix));
            }
        }

        public override string MaterialString
        { 
            get
            {
                if (!IsMaterial(Prefix))
                    return String.Empty;
                return GetItemPrefix(Type, Prefix);
            }
        }

        public override string ElementString
        {
            get
            {
                if (!IsElemental(Prefix))
                    return String.Empty;
                return Global.SingleResistance(ElementalEffect(Prefix).ResistElement);
            }
        }

        public override string PropertyString { get { return MM45Item.GetItemSuffix(ItemBaseType, Suffix); } }
    }

    public class MM45Shops : Shops
    {
        public byte[] CloudBytes;
        public byte[] DarkBytes;
        public byte[] CurrentBytes;

        public bool CompareBytes(byte[] cloud, byte[] dark, byte[] current)
        {
            return Global.Compare(cloud, CloudBytes) &&
                   Global.Compare(dark, DarkBytes) &&
                   Global.Compare(current, CurrentBytes);
        }

        public MM45Shops(long offsetCloud, byte[] bytesCloud, long offsetDark, byte[] bytesDark, long offsetCurrent, byte[] bytesCurrent)
        {
            CloudBytes = bytesCloud;
            DarkBytes = bytesDark;
            CurrentBytes = bytesCurrent;

            RawBytes = Global.Combine(bytesCloud, bytesDark, bytesCurrent);

            // The order of the bytes is
            // Weapons[1-9] (4 bytes each)
            // Armor[1-9] (4 bytes each)
            // Accessories[1-9] (4 bytes each)
            // Misc[1-9] (4 bytes each)

            Inventories = new List<ShopInventory>(8);
            Inventories.Add(new MM45ShopInventory(offsetCloud, 0, bytesCloud, "Vertigo"));
            Inventories.Add(new MM45ShopInventory(offsetCloud, 4, bytesCloud, "Rivercity"));
            Inventories.Add(new MM45ShopInventory(offsetCloud, 8, bytesCloud, "Newcastle"));
            Inventories.Add(new MM45ShopInventory(offsetCloud, 12, bytesCloud, "Shangri-La"));
            Inventories.Add(new MM45ShopInventory(offsetDark, 0, bytesDark, "Castleview"));
            Inventories.Add(new MM45ShopInventory(offsetDark, 4, bytesDark, "Sandcaster"));
            Inventories.Add(new MM45ShopInventory(offsetDark, 8, bytesDark, "Olympus"));
            Inventories.Add(new MM45ShopInventory(offsetDark, 12, bytesDark, "Kalindra"));

            CurrentDisplay = new List<ShopItem>(36);
            for (int i = (36 * 0); i < (36 * 1); i += 4)
                if (bytesCurrent[i + 1] != 0)
                    CurrentDisplay.Add(new ShopItem(MM45Item.Create(bytesCurrent, ItemType.Weapon, i), offsetCurrent + i, 1));
            for (int i = (36 * 1); i < (36 * 2); i += 4)
                if (bytesCurrent[i + 1] != 0)
                    CurrentDisplay.Add(new ShopItem(MM45Item.Create(bytesCurrent, ItemType.Armor, i), offsetCurrent + i, 1));
            for (int i = (36 * 2); i < (36 * 3); i += 4)
                if (bytesCurrent[i + 1] != 0)
                    CurrentDisplay.Add(new ShopItem(MM45Item.Create(bytesCurrent, ItemType.Accessory, i), offsetCurrent + i, 1));
            for (int i = (36 * 3); i < (36 * 4); i += 4)
                if (bytesCurrent[i] != 0)
                    CurrentDisplay.Add(new ShopItem(MM45Item.Create(bytesCurrent, ItemType.Miscellaneous, i), offsetCurrent + i, 1));
        }
    }

    public class MM45ShopInventory : ShopInventory
    {
        public List<ShopItem> Weapons;
        public List<ShopItem> Armor;
        public List<ShopItem> Accessories;
        public List<ShopItem> Misc;

        public override IEnumerable<ShopItem> AllItems
        {
            get { return Weapons.Concat(Armor).Concat(Accessories).Concat(Misc); }
        }

        public MM45ShopInventory(long memoryOffset, int byteOffset, byte[] bytes, string town)
        {
            memoryOffset += byteOffset;
            Town = town;
            Weapons = new List<ShopItem>(9);
            Armor = new List<ShopItem>(9);
            Accessories = new List<ShopItem>(9);
            Misc = new List<ShopItem>(9);
            for (int i = byteOffset; i < 144; i += 16)
            {
                if (bytes[i + (144 * 0) + 1] != 0)
                    Weapons.Add(new ShopItem(MM45Item.Create(bytes, ItemType.Weapon, (144 * 0) + i), memoryOffset + (144 * 0) + i, 1));
                if (bytes[i + (144 * 1) + 1] != 0)
                    Armor.Add(new ShopItem(MM45Item.Create(bytes, ItemType.Armor, (144 * 1) + i), memoryOffset + (144 * 1)  + i, 1));
                if (bytes[i + (144 * 2) + 1] != 0)
                    Accessories.Add(new ShopItem(MM45Item.Create(bytes, ItemType.Accessory, (144 * 2) + i), memoryOffset + (144 * 2) + i, 1));
                if (bytes[i + (144 * 3)] != 0)
                    Misc.Add(new ShopItem(MM45Item.Create(bytes, ItemType.Miscellaneous, (144 * 3) + i), memoryOffset + (144 * 3) + i, 1));
            }
        }
    }
}
