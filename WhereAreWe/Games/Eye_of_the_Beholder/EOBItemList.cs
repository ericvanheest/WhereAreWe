using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    [Flags]
    public enum EOBBasicType
    {
        None =     0x0000,
        Quiver =   0x0001,
        Armor =    0x0002,
        Bracers =  0x0004,
        Misc =     0x0008,
        Boots =    0x0010,
        Helmet =   0x0020,
        Necklace = 0x0040,
        FoodKey =  0x0080,
        Ring =     0x0100
    }

    public enum EOBItemGraphic
    {
        MouseCursor = 0,
        LongSword1 = 1,
        ShortSword = 2,
        Axe1 = 3,
        Mace = 4,
        Flail = 5,
        Spear = 6,
        Axe2 = 7,
        Staff1 = 8,
        Halberd1 = 9,
        Bow1 = 10,
        Bow2 = 11,
        Dagger1 = 12,
        Halberd2 = 13,
        Dart1 = 14,
        Dagger2 = 15,
        Arrow = 16,
        MultiJewelKey = 17,
        Sling = 18,
        Rock1 = 19,
        Helmet1 = 20,
        LeatherBoots = 21,
        Shield1 = 22,
        Shield2 = 23,
        RedJewelKey = 24,
        MetalBracers = 25,
        PlateMail = 26,
        GreenSymbol = 27,
        BandedArmor = 28,
        ChainMail = 29,
        ScaleMail = 30,
        LeatherArmor = 31,
        Robe = 32,
        Necklace1 = 33,
        Necklace2 = 34,
        Book = 35,
        YellowScroll = 36,
        Orb = 37,
        LargePackage = 38,
        SmallPackage = 39,
        RedPotion = 40,
        BluePotion = 41,
        GreenPotion = 42,
        SerratedSword = 43,
        Bones = 44,
        CurvedSword = 45,
        RedGem = 46,
        BlueGem = 47,
        GrayKey = 48,
        GoldKey = 49,
        GreenJewelKey = 50,
        YellowKey = 51,
        SkullKey = 52,
        RedCircleKey = 53,
        Wand = 54,
        YellowSymbol = 55,
        LockPicks = 56,
        RedJewelRing = 57,
        Egg = 58,
        StoneJewel = 59,
        StoneAmulet = 60,
        StoneSpike = 61,
        StoneDagger = 62,
        StoneOrb = 63,
        StoneRing = 64,
        StoneSymbol = 65,
        StoneNecklace = 66,
        LongSword2 = 67,
        RedScepter = 68,
        Rock2 = 69,
        Rock3 = 70,
        BlueBoots = 71,
        BlueBracers = 72,
        Shield3 = 73,
        Shield4 = 74,
        Helmet2 = 75,
        Helmet3 = 76,
        Dart2 = 77,
        Staff2 = 78,
        YellowPlateMail = 79,
        BlueJewelRing = 80,
        GreenJewelRing = 81,
        BlueHand = 82,
        RedHand = 83,
        FlameSword = 84,
        LeftHand = 85,
        RightHand = 86,
        BrownScroll = 87,
        OpenScroll = 88,
        Last
    }

    public enum EOBSecondaryType
    {
        Armor = 0x80,
        Weapon1 = 0x01,
        Weapon2 = 0x81,
        ThrownWeapon = 0x82,
        QuiverItem = 0x02,
        MissileWeapon = 0x03,
        NecklaceKenku = 0x04,
        LockPicks = 0x84,
        SpellBook = 0x05,
        HolySymbol = 0x06,
        Food = 0x07,
        Bones = 0x08,
        MageScroll = 0x09,
        ClericScroll = 0x0A,
        TextScroll = 0x0B,
        Stone = 0x0C,
        Key = 0x0D,
        Potion = 0x0E,
        Gem = 0x0F,
        Medallion = 0x15,
        Ring = 0x90,
        Wand = 0x92
    }

    public enum EOBItemIndex
    {
        Axe = 0,
        LongSword = 1,
        ShortSword = 2,
        OrbOfPower = 3,
        AdamantiteDart = 4,
        Dagger = 5,
        DwarvenHealingPotion = 6,
        Bow = 7,
        Unknown8 = 8,
        Spear = 9,
        Halberd = 10,
        MaceScepter = 11,
        Flail = 12,
        Staff = 13,
        Sling = 14,
        Dart = 15,
        Arrow = 16,
        Unknown17 = 17,
        Rock = 18,
        BandedArmor = 19,
        Chainmail = 20,
        Helmet = 21,
        LeatherArmor = 22,
        Unknown23 = 23,
        PlateMail = 24,
        ScaleMail = 25,
        Unknown26 = 26,
        Shield = 27,
        LockPicks = 28,
        SpellBook = 29,
        HolySymbol = 30,
        Rations = 31,
        Boots = 32,
        Bones = 33,
        MageScroll = 34,
        ClericScroll = 35,
        TextScroll = 36,
        Stone = 37,
        Key = 38,
        Potion = 39,
        Gem = 40,
        Robe = 41,
        RingProtection = 42,
        Bracers = 43,
        MedallionNecklace = 44,
        Unknown45 = 45,
        Medallion = 46,
        RingEffect = 47,    
        Wand = 48,
        KenkuEgg = 49,
        Unknown50 = 50,

        Last
    }

    public enum EOBEquipPosition
    {
        Invalid = -1,
        RightHand = 0,
        LeftHand = 1,
        Backpack1 = 2,
        Backpack2 = 3,
        Backpack3 = 4,
        Backpack4 = 5,
        Backpack5 = 6,
        Backpack6 = 7,
        Backpack7 = 8,
        Backpack8 = 9,
        Backpack9 = 10,
        Backpack10 = 11,
        Backpack11 = 12,
        Backpack12 = 13,
        Backpack13 = 14,
        Backpack14 = 15,
        Quiver = 16,
        Torso = 17,
        Forearm = 18,
        Head = 19,
        Neck = 20,
        Feet = 21,
        Belt1 = 22,
        Belt2 = 23,
        Belt3 = 24,
        Ring1 = 25,
        Ring2 = 26,

        Last
    }

    public enum EOBItemLocation
    {
        Any = -2,
        Invalid = -1,
        NorthWest = 0,
        NorthEast = 1,
        SouthWest = 2,
        SouthEast = 3,
        Unknown4 = 4,
        Unknown5 = 5,
        Unknown6 = 6,
        Unknown7 = 7,
        Alcove = 8,
    }

    public enum EOBPotionModifier
    {
        Invalid = -1,
        None = 0,
        GiantStrength = 1,
        Healing = 2,
        ExtraHealing = 3,
        Poison = 4,
        Vitality = 5,
        Speed = 6,
        Invisibility = 7,
        CurePoison = 8
    }

    public enum EOBRingModifier
    {
        Invalid = -1,
        Adornment = 0,
        Wizardry = 1,
        Sustenance = 2,
        FeatherFall = 3
    }

    public enum EOBWandModifier
    {
        Invalid = -1,
        None = 0,
        LightningBolt = 1,
        ConeOfCold = 2,
        Unknown3 = 3,
        Fireball = 4,
        Slivias = 5,
        MagicMissile = 6,
        MagicalVestment = 7,
        Unknown8 = 8,
        FlameBlade = 9
    }

    [Flags]
    public enum EOBItemFlags
    {
        None =          0x00,
        Charges =       0x1F,
        Nonremovable =  0x20,
        Identified =    0x40,
        Magical =       0x80
    }

    [Flags]
    public enum EOBUseFlags
    {
        None =     0x00,
        Fighter =  0x01,
        Mage =     0x02,
        Cleric =   0x04,
        Thief =    0x08,
        All =      0x0f
    }

    public enum EOBHanded
    {
        None = 0,
        OneHanded = 1,
        TwoHanded = 2
    }

    public abstract class EOBItem : Item
    {
        public byte Unknown01;
        public int Charges;
        public int Image;
        public int StringIndex;
        protected int m_index;
        public bool Identified;
        public bool Magical;
        public bool Nonremovable;
        public EOBBasicType FixedType1;
        public EOBBasicType FixedType2;
        public DamageDice Damage;
        public DamageDice DamageLarge;
        public DamageDice MissileDamage = DamageDice.Zero;
        public EOBUseFlags Usable;
        public EOBSecondaryType SecondaryType;
        public EOBHanded Handed;
        public abstract int SpellIndex { get; }
        public abstract int EffectIndex { get; }
        public int AC;
        public int ItemListIndex;
        public int MapIndex;
        public int NextItem;
        public int PrevItem;
        public int Modifier;
        public bool Available;
        public bool InQuiver;
        protected long m_value;
        public EOBItemLocation Floor;
        public EOBEquipPosition EOBInvLocation;
        public Point Location;
        public override int Index { get { return m_index; } set { m_index = value; } }
        public EOBItemIndex ItemIndex { get { return (EOBItemIndex)m_index; } set { m_index = (int)value; } }
        public override bool ChargeBased { get { return (ChargesCurrent > 0); } }
        public override int MaxCharges { get { return 31; } }
        public override int EmptyIndex { get { return -1; } }

        public override bool IsIdentified { get { return Identified; } }
        public override long Value { get { return m_value; } set { m_value = value; } }
        public override BasicDamage BaseDamage { get { return new BasicDamage(1, Damage.Plus(Modifier)); } }
        public override int ArmorClass { get { return AC; } }
        public override string VisibleDescription { get { return (Identified || !Properties.Settings.Default.HideUnidentifiedItems) ? DescriptionString : String.Format("Unidentified {0}", ItemNoun); } }

        public virtual void SetBytes(int index, byte[] bytes, int offset = 0)
        {
        }

        public static EOBItem CreateEmpty(GameNames game, EOBEquipPosition pos)
        {
            EOBItem item = new EOB1Item();
            item.EOBInvLocation = pos;
            item.Index = 0;
            item.ItemListIndex = -2;
            item.Name = "";
            item.Available = true;
            return item;
        }

        public static EOBItem CreateClone(GameNames game, EOBItem item, int iNewMasterIndex)
        {
            byte[] bytesClone = item.GetBytes();
            Global.SetBytes(bytesClone, 5, 8, 0);  // Zero out the location bytes for the clone
            EOBItem itemClone = new EOB1Item(bytesClone, 0, iNewMasterIndex);
            return itemClone;
        }

        public virtual string StringName => "Invalid";

        public override ItemType Type
        {
            get
            {
                switch (ItemIndex)
                {
                    case EOBItemIndex.Axe:
                    case EOBItemIndex.LongSword:
                    case EOBItemIndex.Bow:
                    case EOBItemIndex.Spear:
                    case EOBItemIndex.Halberd:
                    case EOBItemIndex.MaceScepter:
                    case EOBItemIndex.Flail:
                    case EOBItemIndex.Staff:
                    case EOBItemIndex.Sling:
                    case EOBItemIndex.AdamantiteDart:
                    case EOBItemIndex.Dart:
                    case EOBItemIndex.Arrow:
                    case EOBItemIndex.Rock:
                    case EOBItemIndex.Dagger:
                    case EOBItemIndex.ShortSword: return ItemType.Weapon;
                    case EOBItemIndex.BandedArmor:
                    case EOBItemIndex.LeatherArmor:
                    case EOBItemIndex.PlateMail:
                    case EOBItemIndex.ScaleMail:
                    case EOBItemIndex.Chainmail:
                    case EOBItemIndex.Robe:
                    case EOBItemIndex.Bracers:
                    case EOBItemIndex.Shield:
                    case EOBItemIndex.Boots:
                    case EOBItemIndex.Helmet: return ItemType.Armor;
                    case EOBItemIndex.MedallionNecklace:
                    case EOBItemIndex.Medallion:
                    case EOBItemIndex.RingProtection:
                    case EOBItemIndex.RingEffect: return ItemType.Accessory;
                    default: return ItemType.Miscellaneous;
                }
            }
            set { }
        }

        public static string GraphicString(EOBItemGraphic graphic)
        {
            switch (graphic)
            {
                case EOBItemGraphic.MouseCursor: return "Mouse Cursor";
                case EOBItemGraphic.LongSword1: return "Long Sword 1";
                case EOBItemGraphic.ShortSword: return "Short Sword";
                case EOBItemGraphic.Axe1: return "Axe 1";
                case EOBItemGraphic.Mace: return "Mace";
                case EOBItemGraphic.Flail: return "Flail";
                case EOBItemGraphic.Spear: return "Spear";
                case EOBItemGraphic.Axe2: return "Axe 2";
                case EOBItemGraphic.Staff1: return "Staff 1";
                case EOBItemGraphic.Halberd1: return "Halberd 1";
                case EOBItemGraphic.Bow1: return "Bow 1";
                case EOBItemGraphic.Bow2: return "Bow 2";
                case EOBItemGraphic.Dagger1: return "Dagger 1";
                case EOBItemGraphic.Halberd2: return "Halberd 2";
                case EOBItemGraphic.Dart1: return "Dart 1";
                case EOBItemGraphic.Dagger2: return "Dagger 2";
                case EOBItemGraphic.Arrow: return "Arrow";
                case EOBItemGraphic.MultiJewelKey: return "Multi Jewel Key";
                case EOBItemGraphic.Sling: return "Sling";
                case EOBItemGraphic.Rock1: return "Rock 1";
                case EOBItemGraphic.Helmet1: return "Helmet 1";
                case EOBItemGraphic.LeatherBoots: return "Leather Boots";
                case EOBItemGraphic.Shield1: return "Shield 1";
                case EOBItemGraphic.Shield2: return "Shield 2";
                case EOBItemGraphic.RedJewelKey: return "Red Jewel Key";
                case EOBItemGraphic.MetalBracers: return "Metal Bracers";
                case EOBItemGraphic.PlateMail: return "Plate Mail";
                case EOBItemGraphic.GreenSymbol: return "Green Symbol";
                case EOBItemGraphic.BandedArmor: return "Banded Armor";
                case EOBItemGraphic.ChainMail: return "Chain Mail";
                case EOBItemGraphic.ScaleMail: return "Scale Mail";
                case EOBItemGraphic.LeatherArmor: return "Leather Armor";
                case EOBItemGraphic.Robe: return "Robe";
                case EOBItemGraphic.Necklace1: return "Necklace 1";
                case EOBItemGraphic.Necklace2: return "Necklace 2";
                case EOBItemGraphic.Book: return "Book";
                case EOBItemGraphic.YellowScroll: return "Yellow Scroll";
                case EOBItemGraphic.Orb: return "Orb";
                case EOBItemGraphic.LargePackage: return "Large Package";
                case EOBItemGraphic.SmallPackage: return "Small Package";
                case EOBItemGraphic.RedPotion: return "Red Potion";
                case EOBItemGraphic.BluePotion: return "Blue Potion";
                case EOBItemGraphic.GreenPotion: return "Green Potion";
                case EOBItemGraphic.SerratedSword: return "Serrated Sword";
                case EOBItemGraphic.Bones: return "Bones";
                case EOBItemGraphic.CurvedSword: return "Curved Sword";
                case EOBItemGraphic.RedGem: return "Red Gem";
                case EOBItemGraphic.BlueGem: return "Blue Gem";
                case EOBItemGraphic.GrayKey: return "Gray Key";
                case EOBItemGraphic.GoldKey: return "Gold Key";
                case EOBItemGraphic.GreenJewelKey: return "Green Jewel Key";
                case EOBItemGraphic.YellowKey: return "Yellow Key";
                case EOBItemGraphic.SkullKey: return "Skull Key";
                case EOBItemGraphic.RedCircleKey: return "Red Circle Key";
                case EOBItemGraphic.Wand: return "Wand";
                case EOBItemGraphic.YellowSymbol: return "Yellow Symbol";
                case EOBItemGraphic.LockPicks: return "Lock Picks";
                case EOBItemGraphic.RedJewelRing: return "Red Jewel Ring";
                case EOBItemGraphic.Egg: return "Egg";
                case EOBItemGraphic.StoneJewel: return "Stone Jewel";
                case EOBItemGraphic.StoneAmulet: return "Stone Amulet";
                case EOBItemGraphic.StoneSpike: return "Stone Spike";
                case EOBItemGraphic.StoneDagger: return "Stone Dagger";
                case EOBItemGraphic.StoneOrb: return "Stone Orb";
                case EOBItemGraphic.StoneRing: return "Stone Ring";
                case EOBItemGraphic.StoneSymbol: return "Stone Symbol";
                case EOBItemGraphic.StoneNecklace: return "Stone Necklace";
                case EOBItemGraphic.LongSword2: return "Long Sword 2";
                case EOBItemGraphic.RedScepter: return "Red Scepter";
                case EOBItemGraphic.Rock2: return "Rock 2";
                case EOBItemGraphic.Rock3: return "Rock 3";
                case EOBItemGraphic.BlueBoots: return "Blue Boots";
                case EOBItemGraphic.BlueBracers: return "Blue Bracers";
                case EOBItemGraphic.Shield3: return "Shield 3";
                case EOBItemGraphic.Shield4: return "Shield 4";
                case EOBItemGraphic.Helmet2: return "Helmet 2";
                case EOBItemGraphic.Helmet3: return "Helmet 3";
                case EOBItemGraphic.Dart2: return "Dart 2";
                case EOBItemGraphic.Staff2: return "Staff 2";
                case EOBItemGraphic.YellowPlateMail: return "Yellow Plate Mail";
                case EOBItemGraphic.BlueJewelRing: return "Blue Jewel Ring";
                case EOBItemGraphic.GreenJewelRing: return "Green Jewel Ring";
                case EOBItemGraphic.BlueHand: return "Blue Hand";
                case EOBItemGraphic.RedHand: return "Red Hand";
                case EOBItemGraphic.FlameSword: return "Flame Sword";
                case EOBItemGraphic.LeftHand: return "Left Hand";
                case EOBItemGraphic.RightHand: return "Right Hand";
                case EOBItemGraphic.BrownScroll: return "Brown Scroll";
                case EOBItemGraphic.OpenScroll: return "Open Scroll";
                default: return String.Format("Unknown({0})", (int)graphic);
            }
        }

        public override ItemNounType ItemBasicType
        {
            get
            {
                switch (ItemIndex)
                {

                    case EOBItemIndex.Axe: return ItemNounType.Axe;
                    case EOBItemIndex.LongSword:
                    case EOBItemIndex.ShortSword: return ItemNounType.Sword;
                    case EOBItemIndex.Dagger: return ItemNounType.Dagger;
                    case EOBItemIndex.DwarvenHealingPotion: return ItemNounType.Potion;
                    case EOBItemIndex.Bow: return ItemNounType.Bow;
                    case EOBItemIndex.Spear: return ItemNounType.Spear;
                    case EOBItemIndex.Halberd: return ItemNounType.Halberd;
                    case EOBItemIndex.MaceScepter: return ItemNounType.Mace;
                    case EOBItemIndex.Flail: return ItemNounType.Flail;
                    case EOBItemIndex.Staff: return ItemNounType.Staff;
                    case EOBItemIndex.Sling: return ItemNounType.Sling;
                    case EOBItemIndex.AdamantiteDart:
                    case EOBItemIndex.Dart: return ItemNounType.Dart;
                    case EOBItemIndex.Arrow: return ItemNounType.Arrow;
                    case EOBItemIndex.Rock: return ItemNounType.Rock;
                    case EOBItemIndex.BandedArmor: return ItemNounType.BandedArmor;
                    case EOBItemIndex.Chainmail: return ItemNounType.ChainMail;
                    case EOBItemIndex.Helmet: return ItemNounType.Helmet;
                    case EOBItemIndex.LeatherArmor: return ItemNounType.LeatherArmor;
                    case EOBItemIndex.PlateMail: return ItemNounType.PlateMail;
                    case EOBItemIndex.ScaleMail: return ItemNounType.ScaleMail;
                    case EOBItemIndex.Shield: return ItemNounType.Shield;
                    case EOBItemIndex.LockPicks: return ItemNounType.Picks;
                    case EOBItemIndex.SpellBook: return ItemNounType.Book;
                    case EOBItemIndex.HolySymbol: return ItemNounType.Symbol;
                    case EOBItemIndex.Rations: return ItemNounType.Food;
                    case EOBItemIndex.Boots: return ItemNounType.Boots;
                    case EOBItemIndex.Bones: return ItemNounType.Bones;
                    case EOBItemIndex.MageScroll:
                    case EOBItemIndex.ClericScroll:
                    case EOBItemIndex.TextScroll: return ItemNounType.Scroll;
                    case EOBItemIndex.Key: return ItemNounType.Key;
                    case EOBItemIndex.Potion: return ItemNounType.Potion;
                    case EOBItemIndex.Gem: return ItemNounType.Gem;
                    case EOBItemIndex.Robe: return ItemNounType.Robe;
                    case EOBItemIndex.Bracers: return ItemNounType.Bracers;
                    case EOBItemIndex.MedallionNecklace:
                    case EOBItemIndex.Medallion: return ItemNounType.Medallion;
                    case EOBItemIndex.RingProtection:
                    case EOBItemIndex.RingEffect: return ItemNounType.Ring;
                    case EOBItemIndex.Wand: return ItemNounType.Wand;

                    default: return ItemNounType.Misc;
                }
            }
        }

        public static string GetName(EOBItemIndex index)
        {
            switch (index)
            {
                case EOBItemIndex.Axe: return "Axe";
                case EOBItemIndex.LongSword: return "Long Sword";
                case EOBItemIndex.ShortSword: return "Short Sword";
                case EOBItemIndex.OrbOfPower: return "Orb of Power";
                case EOBItemIndex.AdamantiteDart: return "Adamantite Dart";
                case EOBItemIndex.Dagger: return "Dagger";
                case EOBItemIndex.DwarvenHealingPotion: return "Dwarven Healing Potion";
                case EOBItemIndex.Bow: return "Bow";
                case EOBItemIndex.Spear: return "Spear";
                case EOBItemIndex.Halberd: return "Halberd";
                case EOBItemIndex.MaceScepter: return "Mace";
                case EOBItemIndex.Flail: return "Flail";
                case EOBItemIndex.Staff: return "Staff";
                case EOBItemIndex.Sling: return "Sling";
                case EOBItemIndex.Dart: return "Dart";
                case EOBItemIndex.Arrow: return "Arrow";
                case EOBItemIndex.Rock: return "Rock";
                case EOBItemIndex.BandedArmor: return "Banded Armor";
                case EOBItemIndex.Chainmail: return "Chainmail";
                case EOBItemIndex.Helmet: return "Helmet";
                case EOBItemIndex.LeatherArmor: return "Leather Armor";
                case EOBItemIndex.PlateMail: return "Plate Mail";
                case EOBItemIndex.ScaleMail: return "Scale Mail";
                case EOBItemIndex.Shield: return "Shield";
                case EOBItemIndex.LockPicks: return "Lock Picks";
                case EOBItemIndex.SpellBook: return "Spell Book";
                case EOBItemIndex.HolySymbol: return "Holy Symbol";
                case EOBItemIndex.Rations: return "Rations";
                case EOBItemIndex.Boots: return "Boots";
                case EOBItemIndex.Bones: return "Bones";
                case EOBItemIndex.MageScroll: return "Mage Scroll";
                case EOBItemIndex.ClericScroll: return "Cleric Scroll";
                case EOBItemIndex.TextScroll: return "Scroll";
                case EOBItemIndex.Stone: return "Stone Item";
                case EOBItemIndex.Key: return "Key";
                case EOBItemIndex.Potion: return "Potion";
                case EOBItemIndex.Gem: return "Gem";
                case EOBItemIndex.Robe: return "Robe";
                case EOBItemIndex.Bracers: return "Bracers";
                case EOBItemIndex.MedallionNecklace: return "Necklace";
                case EOBItemIndex.Medallion: return "Medallion";
                case EOBItemIndex.RingProtection: return "Ring of Protection";  // of Protection (modifier is AC bonus)
                case EOBItemIndex.RingEffect: return "Ring";  // of Effect (modifier is misc. effect)
                case EOBItemIndex.Wand: return "Wand";
                case EOBItemIndex.KenkuEgg: return "Kenku Egg";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public override bool AlwaysIdentified
        {
            get
            {
                switch (ItemIndex)
                {
                    case EOBItemIndex.Potion:
                    case EOBItemIndex.Rations:
                    case EOBItemIndex.Key:
                    case EOBItemIndex.Bones:
                    case EOBItemIndex.Gem:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override string ItemNoun { get { return StringName; } }    // Used for unidentified items; in EOB an "unidentified" item is only missing the "cursed" prefix and/or the modifier suffix

        public static string LocationString(EOBItemLocation floor, Point pt, int iMap, bool bShort = false)
        {
            if (iMap == 0)
                return bShort ? "" : "None";
            if (iMap == 255)
                return "Quiver";
            string strFloor;
            if (bShort)
            {
                strFloor = floor == EOBItemLocation.Alcove ? "alcove" : "floor";
                return String.Format("Map {0}, {1},{2}, {3}", iMap, pt.X, pt.Y, strFloor);
            }
            strFloor = floor == EOBItemLocation.Alcove ? "In an alcove" : "On the floor";
            return String.Format("{0} at {1},{2} on level {3}", strFloor, pt.X, pt.Y, iMap);
        }

        public static string BonesCharacter(int iMod)
        {
            switch (iMod)
            {
                case 1: return "Anya";
                case 2: return "Beohram";
                case 3: return "Kirath";
                case 4: return "Ileria";
                case 5: return "Tyrra";
                case 6: return "Tod Uphill";
                case 7: return "Taghor";
                default: return String.Format("Unknown({0})", iMod);
            }
        }

        public static string ModifierString(EOBItemIndex index, int iModifier)
        {
            switch (index)
            {
                case EOBItemIndex.ClericScroll: return EOB1.SpellName(iModifier + (int)EOBSpellIndex.Bless - 1);
                case EOBItemIndex.MageScroll: return String.Format(EOB1.SpellName(iModifier));
                case EOBItemIndex.DwarvenHealingPotion:
                case EOBItemIndex.Potion: return PotionModifier((EOBPotionModifier)iModifier);
                case EOBItemIndex.Wand: return WandModifier((EOBWandModifier)iModifier);
                case EOBItemIndex.Gem:
                case EOBItemIndex.Stone:
                case EOBItemIndex.KenkuEgg:
                case EOBItemIndex.Key: return String.Format("#{0}", iModifier);
                case EOBItemIndex.Bones: return BonesCharacter(iModifier);
                case EOBItemIndex.Rations:
                case EOBItemIndex.TextScroll: return iModifier.ToString();
                case EOBItemIndex.RingEffect:
                    switch ((EOBRingModifier)iModifier)
                    {
                        case EOBRingModifier.Adornment: return "of Adornment";
                        case EOBRingModifier.FeatherFall: return "of Feather Fall";
                        case EOBRingModifier.Sustenance: return "of Sustenance";
                        case EOBRingModifier.Wizardry: return "of Wizardry";
                        default: return String.Format("Unknown({0})", iModifier);
                    }
                default:
                    if (iModifier == 0)
                        return "";
                    return Global.AddPlus((sbyte)iModifier);
            }
        }

        public static string PotionModifier(EOBPotionModifier mod)
        {
            switch (mod)
            {
                case EOBPotionModifier.GiantStrength: return "Giant Strength";
                case EOBPotionModifier.Healing: return "Healing";
                case EOBPotionModifier.ExtraHealing: return "Extra Healing";
                case EOBPotionModifier.Poison: return "Poison";
                case EOBPotionModifier.Vitality: return "Vitality";
                case EOBPotionModifier.Speed: return "Speed";
                case EOBPotionModifier.Invisibility: return "Invisibility";
                case EOBPotionModifier.CurePoison: return "Cure Poison";
                default: return String.Format("Unknown({0})", (int)mod);
            }
        }

        public static string WandModifier(EOBWandModifier mod)
        {
            switch (mod)
            {
                case EOBWandModifier.None: return "Nothing";
                case EOBWandModifier.LightningBolt: return "Lightning Bolt";
                case EOBWandModifier.ConeOfCold: return "Frost";
                case EOBWandModifier.Fireball: return "Fireball";
                case EOBWandModifier.Slivias: return "Slivias";
                case EOBWandModifier.MagicMissile: return "Magic Missile";
                case EOBWandModifier.MagicalVestment: return "Magical Vestment";
                case EOBWandModifier.FlameBlade: return "Flame Blade";
                default: return String.Format("Unknown({0})", (int)mod);
            }
        }

        public byte[] GetItemListBytes()
        {
            ushort location = EOBMemoryHacker.PackedFiveFromPoint(Location);
            if (Available)
                location = 0xFFFF;
            else if (InQuiver)
                location = 0xFFFE;

            return new byte[]
            {
                (byte) StringIndex,
                Unknown01,
                GetFlagByte(),
                (byte) Image,
                (byte) ItemIndex,
                (byte) Floor,
                (byte) (location & 0x00FF),
                (byte) ((location & 0xFF00) >> 8),
                (byte) (NextItem & 0x00FF),
                (byte) ((NextItem & 0xFF00) >> 8),
                (byte) (PrevItem & 0x00FF),
                (byte) ((PrevItem & 0xFF00) >> 8),
                (byte) MapIndex,
                (byte) Modifier
            };
        }

        public virtual byte[] GetBytes()
        {
            return Serialize();
        }

        public static byte GetValueByte(int val)
        {
            if (val < 32)
                return (byte)(val << 3);
            int iExp = 8;
            int iTest = 100000000;
            while (iTest > 1)
            {
                if (val < iTest)
                    iExp--;
                else
                    break;
                iTest /= 10;
            }
            return (byte)(iExp | ((val / iTest) << 3));
        }

        public static byte GetUsableByte(EOBUseFlags eobUse, GameNames game)
        {
            return (byte)eobUse;
        }

        public static EOBUseFlags GetUsableBy(int eobFlags, GameNames game)
        {
            return (EOBUseFlags)eobFlags;
        }

        public static EOBItem FromItemListBytes(GameNames game, byte[] bytes, int offset = 0)
        {
            switch (game)
            {
                case GameNames.EyeOfTheBeholder1: return new EOB1Item(bytes, offset);
                case GameNames.EyeOfTheBeholder2: return new EOB2Item(bytes, offset);
                default: return null;
            }
        }

        public static EquipLocation EOBEquipLocation(EOBEquipPosition pos)
        {
            switch (pos)
            {
                case EOBEquipPosition.RightHand: return EquipLocation.RightHand;
                case EOBEquipPosition.LeftHand: return EquipLocation.LeftHand;
                case EOBEquipPosition.Quiver: return EquipLocation.Ranged;
                case EOBEquipPosition.Torso: return EquipLocation.Torso;
                case EOBEquipPosition.Forearm: return EquipLocation.Gauntlet;
                case EOBEquipPosition.Head: return EquipLocation.Head;
                case EOBEquipPosition.Neck: return EquipLocation.Neck;
                case EOBEquipPosition.Feet: return EquipLocation.Feet;
                case EOBEquipPosition.Belt1:
                case EOBEquipPosition.Belt2:
                case EOBEquipPosition.Belt3: return EquipLocation.Belt;
                case EOBEquipPosition.Ring1:
                case EOBEquipPosition.Ring2: return EquipLocation.Finger;
                default: return EquipLocation.None;
            }
        }

        public static string EOBInvPositionString(EOBEquipPosition equip)
        {
            switch (equip)
            {
                case EOBEquipPosition.LeftHand: return "left hand";
                case EOBEquipPosition.RightHand: return "right hand";
                case EOBEquipPosition.Torso: return "torso";
                case EOBEquipPosition.Head: return "head";
                case EOBEquipPosition.Forearm: return "forearm";
                case EOBEquipPosition.Ring1: return "ring 1";
                case EOBEquipPosition.Ring2: return "ring 2";
                case EOBEquipPosition.Feet: return "feet";
                case EOBEquipPosition.Neck: return "neck";
                case EOBEquipPosition.Belt1: return "belt 1";
                case EOBEquipPosition.Belt2: return "belt 2";
                case EOBEquipPosition.Belt3: return "belt 3";
                case EOBEquipPosition.Quiver: return "quiver";
                default: return "backpack";
            }
        }

        public static EOBItem FromInventoryBytes(GameNames game, byte[] bytes, byte[] bytesItemTable, int iPosition, int offset = 0)
        {
            if (bytesItemTable == null)
                return null;
            int iIndex = BitConverter.ToInt16(bytes, offset + (iPosition * 2));
            if (iIndex < 0 || iIndex >= (bytesItemTable.Length / 14))
                return null;

            EOBItem item = FromItemListBytes(game, bytesItemTable, 14 * iIndex);
            if (item == null)
                return item;

            item.MemoryIndex = iPosition;
            item.EOBInvLocation = (EOBEquipPosition)iPosition;
            item.WhereEquipped = EOBEquipLocation(item.EOBInvLocation);

            return item;
        }

        public override CompareResult CompareTo(Item item)
        {
            if (!(item is EOBItem))
                return CompareResult.Uncomparable;
            if (item == this)
                return CompareResult.Identical;
            if (Type != item.Type)
                return CompareResult.Uncomparable;

            EOBItem btItem = item as EOBItem;

            if (CanEquipLocation != btItem.CanEquipLocation)
                return CompareResult.Uncomparable;

            CompareResult result = CompareResult.Identical;

            if (btItem.SpellIndex != SpellIndex)
            {
                if (btItem.SpellIndex == 0)
                    result = CompareResult.Better;  // Any spell is better than no spell
                else if (SpellIndex == 0)
                    result = CompareResult.Worse;
                else
                    return CompareResult.Uncomparable;
            }
            if (btItem.EffectIndex != EffectIndex)
            {
                if (btItem.EffectIndex == 0)
                    result = CompareResult.Better;  // Any effect is better than no effect
                else if (EffectIndex == 0)
                    result = CompareResult.Worse;
                else
                    return CompareResult.Uncomparable;
            }
            if (!btItem.Identified || !Identified)
                return CompareResult.Uncomparable;

            switch (Type)
            {
                case ItemType.Weapon:
                case ItemType.OneHandMelee:
                case ItemType.TwoHandMelee:
                    return Item.Compare(result, CompareValues(Damage.Average, btItem.Damage.Average));
                default:
                    if (CanEquip && btItem.CanEquip)
                        return Item.Compare(result, CompareValues(ArmorClass, btItem.ArmorClass));
                    break;
            }
            return CompareResult.Uncomparable;
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

        public abstract EOBItem CreateItem(byte[] bytes, int offset = 0);

        public override string ToString()
        {
            return String.Format("#{0}[{1}]: {2}", (int)Index, MemoryIndex, DescriptionString);
        }

        public static EOBItem CreateBasic(GameNames game, byte[] bytes, int iIndex, int offset = 0)
        {
            switch (game)
            {
                case GameNames.EyeOfTheBeholder1:
                    return EOB1Item.FromBasicBytes(bytes, iIndex, offset);
                case GameNames.EyeOfTheBeholder2:
                    return EOB2Item.FromBasicBytes(bytes, iIndex, offset);
                default:
                    return null;
            }
        }

        public virtual byte[] GetBasicBytes() { return null; }

        public static EOBItem CreateRandom(List<EOBItem> list, ItemType type, BaseCharacter charUsable)
        {
            IntDeck deck = new IntDeck(1, list.Count);
            deck.Shuffle();
            foreach (int i in deck.Cards)
            {
                if (type != ItemType.None && type != list[i].ItemBaseType)
                    continue;

                if (charUsable == null)
                    return list[i].Clone() as EOBItem;

                if (list[i].IsUsableByAny(charUsable))
                    return list[i].Clone() as EOBItem;
            }
            return list[0].Clone() as EOBItem;
        }

        public override int ChargesCurrent { get { return -1; } set { } }
        public override string UsableString { get { return UsableByString(Usable, false); } }

        public static string UsableByString(EOBUseFlags flags, bool bShowDots)
        {
            if (flags == EOBUseFlags.All)
                return "<All>";
            string strDot = bShowDots ? "." : "";
            string strUsable = String.Format("{0}{1}{2}{3}",
                flags.HasFlag(EOBUseFlags.Fighter) ? "F" : strDot,
                flags.HasFlag(EOBUseFlags.Mage) ? "M" : strDot,
                flags.HasFlag(EOBUseFlags.Cleric) ? "C" : strDot,
                flags.HasFlag(EOBUseFlags.Thief) ? "T" : strDot
                );
            return strUsable;
        }

        public override string TypeString { get { return GetItemNoun(ItemBasicType, "Misc"); } }

        public bool IsUsable(GenericClass gc)
        {
            // An item in EOB is usable if any of the classes of a multi-class can use it
            return (Usable.HasFlag(EOBUseFlags.Fighter) && EOBCharacter.IsWarrior(gc)) ||
                   (Usable.HasFlag(EOBUseFlags.Cleric) && EOBCharacter.IsCleric(gc)) ||
                   (Usable.HasFlag(EOBUseFlags.Thief) && EOBCharacter.IsThief(gc)) ||
                   (Usable.HasFlag(EOBUseFlags.Mage) && EOBCharacter.IsMage(gc));
        }

        public override bool IsUsableByAny(object filter)
        {
            if (filter is BaseCharacter)
                return IsUsable(((BaseCharacter)filter).BasicClass);
            if (!(filter is GenericClass))
                return true;
            return IsUsable((GenericClass)filter);
        }

        public virtual byte GetFlagByte()
        {
            EOBItemFlags flags = EOBItemFlags.None;
            if (Identified)
                flags |= EOBItemFlags.Identified;
            if (Magical)
                flags |= EOBItemFlags.Magical;
            if (Nonremovable)
                flags |= EOBItemFlags.Nonremovable;
            return (byte)((int)flags | ChargesCurrent);
        }

        public override byte[] Serialize() { return BitConverter.GetBytes((short)ItemListIndex); }

        private bool UnknownModifier(EOBItemIndex eobType)
        {
            switch (eobType)
            {
                // Item types for which the modifier is a known entity
                case EOBItemIndex.ClericScroll:
                case EOBItemIndex.MageScroll:
                case EOBItemIndex.Potion:
                case EOBItemIndex.DwarvenHealingPotion:
                case EOBItemIndex.Wand:
                case EOBItemIndex.Key:
                case EOBItemIndex.Rations:
                case EOBItemIndex.Bones:
                case EOBItemIndex.TextScroll:
                case EOBItemIndex.Gem:
                    return false;
                // Other items should show the modifier directly, as the purpose is unknown
                default:
                    return true;
            }
        }

        public string DebugDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Index == 0)
                    sb.AppendFormat("{0}: EMPTY", ItemListIndex, Name);
                else
                {
                    sb.AppendFormat("{0}: {1}", ItemListIndex, Name);
                    sb.AppendFormat(" ({0})", GetName(ItemIndex));
                    if (ChargesCurrent > 0)
                        sb.AppendFormat(", {0} charges", ChargesCurrent);
                    if (UnknownModifier(ItemIndex) && Modifier > 0)
                        sb.AppendFormat(", #{0}", Modifier);
                    if (MapIndex != 0)
                    {
                        sb.AppendFormat(" [Map {0} {1},{2} @{3}]", MapIndex, Location.X, Location.Y, (int)Floor);
                    }
                }
                return sb.ToString();
            }
        }

        public string LootName
        {
            get
            {
                // The string used for items on the ground, in the map notes
                switch (ItemIndex)
                {
                    case EOBItemIndex.Rations: return String.Format("Rations ({0} food)", Modifier);
                    default: return Name;
                }
            }
        }

        public static string UsableClasses(EOBUseFlags usable)
        {
            StringBuilder sb = new StringBuilder();
            if (usable.HasFlag(EOBUseFlags.All))
                return "All";
            if (usable == EOBUseFlags.None)
                return "None";
            if (usable.HasFlag(EOBUseFlags.Fighter))
                sb.Append("Fighter, ");
            if (usable.HasFlag(EOBUseFlags.Cleric))
                sb.Append("Cleric, ");
            if (usable.HasFlag(EOBUseFlags.Mage))
                sb.Append("Mage, ");
            if (usable.HasFlag(EOBUseFlags.Thief))
                sb.Append("Thief, ");
            return Global.Trim(sb).ToString();
        }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sbFull = new StringBuilder();
                if (ItemListIndex < 1)
                {
                    // Basic item info only
                    sbFull.AppendFormat("Item Index: {0}\r\n", (int)ItemIndex);
                    sbFull.AppendFormat("Name: {0}\r\n", GetName(ItemIndex));
                }
                else
                {
                    sbFull.AppendFormat("List Index: {0}\r\n", ItemListIndex);
                    sbFull.AppendFormat("Name: {0}\r\n", Name);
                    sbFull.AppendFormat("Magical: {0}\r\n", Magical ? "Yes" : "No");
                    sbFull.AppendFormat("Identified: {0}\r\n", Identified ? "Yes" : "No");
                    string strUse = UseEffectString;
                    if (!String.IsNullOrEmpty(strUse))
                        sbFull.AppendFormat("Use: {0}\r\n", strUse);
                    if (ChargesCurrent > 0)
                        sbFull.AppendFormat("Charges: {0}\r\n", ChargesCurrent);
                    sbFull.AppendFormat("Type: {0}\r\n", GetName(ItemIndex));
                    if (UnknownModifier(ItemIndex) && Modifier != 0 && !IsArmor)
                        sbFull.AppendFormat("Modifier: {0}\r\n", Modifier);
                    if (MapIndex != 0)
                        sbFull.AppendFormat("Location: {0}\r\n", LocationString(Floor, Location, MapIndex));
                    else if (WhereEquipped != EquipLocation.None)
                        sbFull.AppendFormat("Location: {0}\r\n", Global.Title(GetEquipLocationString(WhereEquipped)));
                    if (PrevItem > 0 && PrevItem != ItemListIndex)
                        sbFull.AppendFormat("Previous Item: {0}\r\n", PrevItem);
                    if (NextItem > 0 && NextItem != ItemListIndex)
                        sbFull.AppendFormat("Next Item: {0}\r\n", NextItem);
                }
                sbFull.AppendFormat("Usable By: {0}\r\n", UsableClasses(Usable));
                if (Damage.Quantity != 0)
                    sbFull.AppendFormat("Damage{0}: {1}\r\n", DamageLarge.Quantity == 0 ? "" : " vs. small", Damage.Plus(Modifier).StringWithAverage);
                if (DamageLarge.Quantity != 0)
                    sbFull.AppendFormat("Damage vs. large: {0}\r\n", DamageLarge.Plus(Modifier).StringWithAverage);
                if (AC != 0 || ((IsArmor || IsAccessory) && Modifier != 0))
                {
                    if (Modifier == 0)
                        sbFull.AppendFormat("Armor Class: {0}\r\n", AC);
                    else
                        sbFull.AppendFormat("Armor Class: {0} ({1} base, {2} modifer)\r\n", AC - Modifier, AC, Global.AddPlus(-Modifier));
                }
                return sbFull.ToString();
            }
        }

        public override int ArmorClassFull { get { return ModAffectsAC ? AC - Modifier : 0; } }

        public bool ModAffectsAC
        {
            get
            {
                // Some item's modifiers affect AC even if the base armor is zero
                switch (ItemIndex)
                {
                    case EOBItemIndex.Boots:
                    case EOBItemIndex.Robe:
                    case EOBItemIndex.RingProtection:
                    case EOBItemIndex.Bracers:
                    case EOBItemIndex.Medallion:
                    case EOBItemIndex.MedallionNecklace:
                        return true;
                    default:
                        return IsArmor;
                }
            }
        }

        public override string EquipEffects
        {
            get
            {
                switch (ItemIndex)
                {
                    case EOBItemIndex.RingEffect:
                        switch ((EOBRingModifier)Modifier)
                        {
                            case EOBRingModifier.FeatherFall: return "no fall damage";
                            case EOBRingModifier.Wizardry: return "+2 L4, +1 L5 mage spells";
                            default: return "";
                        }
                    default: return "";
                }
            }
        }

        public override string UseEffectString
        {
            get
            {
                if (Modifier == 0)
                    return "";
                string strModifier = ModifierString(ItemIndex, Modifier);
                switch (ItemIndex)
                {
                    case EOBItemIndex.ClericScroll:
                    case EOBItemIndex.MageScroll: return String.Format("Learn {0}", strModifier);
                    case EOBItemIndex.Potion:
                        switch ((EOBPotionModifier)Modifier)
                        {
                            case EOBPotionModifier.CurePoison: return "Remove poison";
                            case EOBPotionModifier.ExtraHealing: return "Heal 6d4 HP";
                            case EOBPotionModifier.GiantStrength: return "Strength 22";
                            case EOBPotionModifier.Healing: return "Heal 2d4+2 HP";
                            case EOBPotionModifier.Invisibility: return "Cast Invisibility";
                            case EOBPotionModifier.Poison: return "Feel ill";
                            case EOBPotionModifier.Speed: return "Feel agile";
                            case EOBPotionModifier.Vitality: return "Food 100";
                            default: return String.Format("Unknown({0})", Modifier);
                        }
                    case EOBItemIndex.DwarvenHealingPotion: return "Cure poison";
                    case EOBItemIndex.Wand:
                        switch ((EOBWandModifier)Modifier)
                        {
                            case EOBWandModifier.Slivias: return "";
                            case EOBWandModifier.None: return "";
                            default: return String.Format("Cast {0}", strModifier);
                        }
                    case EOBItemIndex.Key:
                    case EOBItemIndex.Gem: return "";
                    case EOBItemIndex.KenkuEgg: return "+10 Food";
                    case EOBItemIndex.Rations: return String.Format("+{0} Food", strModifier);
                    case EOBItemIndex.TextScroll: return String.Format("Text #{0})", strModifier);
                    default: return "";
                }
            }
        }

        public override string GetLongDescription(GenericAlignmentValue currentAlign, GenericClass currentClass, string strOverrideName)
        {
            string strName = String.IsNullOrWhiteSpace(strOverrideName) ? (String.IsNullOrWhiteSpace(Name) ? "<no name>" : Name) : strOverrideName;

            string strUsable = "";
            if (currentClass != GenericClass.None && !IsUsable(currentClass))
                strUsable = String.Format(" (!{0})", BaseCharacter.ClassString(currentClass));

            string strDamage = String.Empty;
            if (Damage.Max > 0 && AC != 0)
                strDamage = String.Format("{0}, AC {1}", DamageStringFull, ArmorClassFull);
            else
                strDamage = Damage.Max > 0 ? DamageStringFull : AC != 0 ? ArmorClassFull.ToString() : String.Empty;
            string strUse = UseEffectString;
            string strEquip = EquipEffects;

            StringBuilder sb = new StringBuilder();
            sb.Append(Name);
            sb.Append(strUsable);
            if (!String.IsNullOrEmpty(strDamage))
                sb.AppendFormat(", {0}", strDamage);
            if (!String.IsNullOrEmpty(strUse))
                sb.AppendFormat(", {0}{1}", PassiveEffect ? "" : "Use: ", strUse);
            if (!String.IsNullOrEmpty(strEquip))
                sb.AppendFormat(", {0}", strEquip);
            return sb.ToString();
        }

        public override string DescriptionString { get { return Name; } }

        public override string DamageStringFull
        {
            get
            {
                if (DamageLarge.Equals(Damage))
                    return Damage.Plus(Modifier).ToString();
                return String.Format("{0} (large: {1})", Damage.Plus(Modifier).ToString(), DamageLarge.Plus(Modifier).ToString());
            }
        }

        public static List<EOBItem> ItemList(GameNames game, byte[] bytesItemList, int iHead)
        {
            List<EOBItem> list = new List<EOBItem>();
            EOBItem item = EOBItem.FromItemListBytes(game, bytesItemList, iHead * 14);
            while (item != null)
            {
                list.Add(item);
                if (item.NextItem == 0)
                    break;
                item = EOBItem.FromItemListBytes(game, bytesItemList, item.NextItem * 14);
                if (item == null)
                    break;
                if (list.Any(i => i.ItemListIndex == item.ItemListIndex))
                    break;  // end of item loop
            }
            return list;
        }

        public static string ItemListString(GameNames game, byte[] bytesItemList, int iHead)
        {
            List<EOBItem> list = ItemList(game, bytesItemList, iHead);
            StringBuilder sb = new StringBuilder();
            foreach (EOBItem item in list)
                sb.AppendFormat("{0}, ", item.LootName);
            return Global.Trim(sb).ToString();
        }
    }

    public abstract class EOB123ItemList : InternExternList
    {
        public List<EOBItem> Items;
        public byte[] RawBytes;

        public virtual EOBItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return null; }
        public virtual int ItemLength { get { return 16; } }

        public List<EOBItem> SetFromBytes(GameNames game, byte[] bytes)
        {
            RawBytes = bytes;
            int iNumItems = bytes.Length / ItemLength;
            List<EOBItem> items = new List<EOBItem>(iNumItems);
            for (int i = 0; i < iNumItems; i++)
            {
                switch (game)
                {
                    case GameNames.EyeOfTheBeholder1:
                        items.Add(EOB1Item.FromBasicBytes(bytes, i, i * ItemLength));
                        break;
                    case GameNames.EyeOfTheBeholder2:
                        items.Add(EOB2Item.FromBasicBytes(bytes, i, i * ItemLength));
                        break;
                    default:
                        break;
                }
            }
            return items;
        }

        public virtual EOBItem GetItem(int index, int memory = -1)
        {
            EOBItem item = null;
            if (index < 0 || index >= Items.Count)
                index = 0;

            item = Items[index].Clone() as EOBItem;
            item.MemoryIndex = memory;
            return item;
        }

        public override bool InitExternalList(MemoryHacker hacker, bool bOverrideSanityCheck = false)
        {
            if (!hacker.IsValid)
                return false;

            byte[] bytes = GetExternalBytes(hacker);
            if (!bOverrideSanityCheck)
            {
                if (!Global.CompareBytes(bytes, GetInternalBytes(), 0, 0, 16))
                    return false;
            }

            if (bytes == null)
                return false;

            Items = SetFromBytes(hacker.Game, bytes);
            m_bInternal = false;
            return true;
        }
    }
}
