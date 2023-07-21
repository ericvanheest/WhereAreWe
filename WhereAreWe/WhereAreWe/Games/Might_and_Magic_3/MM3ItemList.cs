using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum MM3ItemIndex
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
        PaddedArmor = 34,
        LeatherArmor = 35,
        ScaleArmor = 36,
        RingMail = 37,
        ChainMail = 38,
        SplintMail = 39,
        PlateMail = 40,
        PlateArmor = 41,
        Shield = 42,
        Helm = 43,
        Crown = 44,
        Tiara = 45,
        Gauntlets = 46,
        Ring = 47,
        Boots = 48,
        Cloak = 49,
        Robes = 50,
        Cape = 51,
        Belt = 52,
        Broach = 53,
        Medal = 54,
        Charm = 55,
        Cameo = 56,
        Scarab = 57,
        Pendant = 58,
        Necklace = 59,
        Amulet = 60,
        Rod = 61,
        Jewel = 62,
        Gem = 63,
        Box = 64,
        Orb = 65,
        Horn = 66,
        Coin = 67,
        Wand = 68,
        Whistle = 69,
        Potion = 70,
        Scroll = 71,
        Torch = 72,
        RopeAndHooks = 73,
        UselessItem = 74,
        AncientJewelry = 75,
        GreenEyeballKey = 76,
        RedWarriorsKey = 77,
        SacredSilverSkull = 78,
        AncientArtifactofGood = 79,
        AncientArtifactofNeutrality = 80,
        AncientArtifactofEvil = 81,
        Jewelry = 82,
        PreciousPearlofYouthandBeauty = 83,
        BlackTerrorKey = 84,
        KingsUltimatePowerOrb = 85,
        AncientFizbinofMisfortune = 86,
        GoldMasterKey = 87,
        QuatlooCoin = 88,
        HologramSequencingCard001 = 89,
        YellowFortressKey = 90,
        BlueUnholyKey = 91,
        HologramSequencingCard002 = 92,
        HologramSequencingCard003 = 93,
        HologramSequencingCard004 = 94,
        HologramSequencingCard005 = 95,
        HologramSequencingCard006 = 96,
        ZItem23 = 97,
        BluePriorityPassCard = 98,
        InterspacialTransportBox = 99,
        MightPotion = 100,
        GoldenPyramidKeyCard = 101,
        AlacornofIcarus = 102,
        SeaShellofSerenity = 103,
        Invalid = 104,
        Last = 104
    }

    public enum MM3ItemElementalIndex
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
        Power = 30,
        Thermal = 31,
        Radiating = 32,
        Kinetic = 33,
        Mystic = 34,
        Magical = 35,
        Ectoplasmic = 36,
        Invalid = 37
    }

    public enum MM3ItemMaterialIndex
    {
        None = 0,
        Wooden = 1,
        Leather = 2,
        Brass = 3,
        Bronze = 4,
        Iron = 5,
        Silver = 6,
        Steel = 7,
        Gold = 8,
        Platinum = 9,
        Glass = 10,
        Coral = 11,
        Crystal = 12,
        Lapis = 13,
        Pearl = 14,
        Amber = 15,
        Ebony = 16,
        Quartz = 17,
        Ruby = 18,
        Emerald = 19,
        Sapphire = 20,
        Diamond = 21,
        Obsidian = 22,
        Invalid = 23
    }

    public enum MM3ItemAttributeIndex
    {
        None = 0,
        Might = 1,
        Strength = 2,
        Warrior = 3,
        Ogre = 4,
        Giant = 5,
        Thunder = 6,
        Force = 7,
        Power = 8,
        Dragon = 9,
        Photon = 10,
        Clever = 11,
        Mind = 12,
        Sage = 13,
        Thought = 14,
        Knowledge = 15,
        Intellect = 16,
        Wisdom = 17,
        Genius = 18,
        Buddy = 19,
        Friendship = 20,
        Charm = 21,
        Personality = 22,
        Charisma = 23,
        Leadership = 24,
        Ego = 25,
        Holy = 26,
        Quick = 27,
        Swift = 28,
        Fast = 29,
        Rapid = 30,
        Speed = 31,
        Wind = 32,
        Accelerator = 33,
        Velocity = 34,
        Sharp = 35,
        Accurate = 36,
        Marksman = 37,
        Precision = 38,
        True = 39,
        Exacto = 40,
        Clover = 41,
        Chance = 42,
        Winners = 43,
        Lucky = 44,
        Gamblers = 45,
        Leprechauns = 46,
        Vigor = 47,
        Health = 48,
        Life = 49,
        Troll = 50,
        Vampiric = 51,
        Spell = 52,
        Castors = 53,
        Witch = 54,
        Mage = 55,
        Archmage = 56,
        Arcane = 57,
        Protection = 58,
        Armored = 59,
        Defender = 60,
        Stealth = 61,
        Divine = 62,
        Mugger = 63,
        Burgler = 64,
        Looter = 65,
        Brigand = 66,
        Filch = 67,
        Thief = 68,
        Rogue = 69,
        Plunder = 70,
        Criminal = 71,
        Pirate = 72,
        Invalid
    }

    public enum MM3ItemPropertyIndex
    {
        None = 0,
        Light = 1,
        Awakening = 2,
        MagicDetection = 3,
        Arrows = 4,
        Aid = 5,
        Fists = 6,
        EnergyBlasts = 7,
        Sleeping = 8,
        Revitalization = 9,
        Curing = 10,
        Sparking = 11,
        Ropes = 12,
        ToxicClouds = 13,
        Elements = 14,
        Pain = 15,
        Jumping = 16,
        AcidStreams = 17,
        UndeadTurning = 18,
        Levitation = 19,
        WizardEyes = 20,
        Silence = 21,
        Blessing = 22,
        Identification = 23,
        Lightning = 24,
        HolyBonuses = 25,
        PowerCuring = 26,
        Nature = 27,
        Beacons = 28,
        Shielding = 29,
        Heroism = 30,
        Immobilization = 31,
        WaterWalking = 32,
        FrostBiting = 33,
        MonsterFinding = 34,
        Fireballs = 35,
        ColdRays = 36,
        Antidotes = 37,
        AcidSpraying = 38,
        Distortion = 39,
        FeebleMinding = 40,
        Vaccination = 41,
        Gating = 42,
        Teleportation = 43,
        Death = 44,
        FreeMovement = 45,
        Paralyzing = 46,
        DeadlySwarms = 47,
        Sanctuaries = 48,
        DragonBreath = 49,
        Feasting = 50,
        FieryFlails = 51,
        Recharging = 52,
        Freezing = 53,
        Portals = 54,
        StoneToFlesh = 55,
        Duplication = 56,
        Disintegration = 57,
        HalfForMe = 58,
        RaisingTheDead = 59,
        Etherealization = 60,
        DancingSwords = 61,
        MoonRays = 62,
        MassDistortion = 63,
        PrismaticLight = 64,
        Enchantment = 65,
        Incinerating = 66,
        HolyWords = 67,
        Resurrection = 68,
        Storms = 69,
        Megavoltage = 70,
        Infernos = 71,
        SunRays = 72,
        Implosions = 73,
        StarBursts = 74,
        TheGods = 75,
        Invalid = 76
    }

    public class MM3ItemList
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

        public MM3ItemList()
        {
            m_bValid = true;
        }
    }

    [Flags]
    public enum MM3ChargesFlags
    {
        ChargesMask = 0x3f,
        Cursed = 0x40,
        Broken = 0x80
    }

    public class MM3Item : MMItem
    {
        public override GameNames Game { get { return GameNames.MightAndMagic3; } }
        public MM3ItemIndex m_base;
        public MM3ItemElementalIndex Element;
        public MM3ItemMaterialIndex Material;
        public MM3ItemAttributeIndex Attribute;
        public MM3ItemPropertyIndex Property;

        public override int Index
        {
            get { return (int) m_base; }
            set { m_base = (MM3ItemIndex) value; }
        }

        public override string TrashIndex
        {
            get
            {
                long id = ((long) Index << 25) | ((long) Element << 19) | ((long) Material << 14) | ((long) Attribute << 7) | (long) Property;
                return String.Format("{0:X8}", (uint)id);
            }
        }

        public override int GetHashCode()
        {
            return (int) m_base | (ChargesByte << 8) | ((int) WhereEquipped << 16) | (int) Element | ((int) Material << 8) | ((int) Attribute << 16) | ((int) Property << 24);
        }

        public MM3ItemIndex Base
        {
            get { return m_base; }
            set
            {
                m_base = value;
                Index = (int)m_base;
            }
        }

        private MM3Item()
        {
        }

        public static MM3Item Create()
        {
            MM3Item item = new MM3Item();
            item.SetDefaults();
            return item;
        }

        public void SetDefaults()
        {
            Base = MM3ItemIndex.None;
            Element = MM3ItemElementalIndex.None;
            Material = MM3ItemMaterialIndex.None;
            Attribute = MM3ItemAttributeIndex.None;
            Property = MM3ItemPropertyIndex.None;
            ChargesByte = 0;
            WhereEquipped = EquipLocation.None;
        }

        public static MM3Item Create(byte equipped, byte charges, byte element, byte material, byte attribute, byte item, byte property)
        {
            MM3Item mm3Item = new MM3Item();
            mm3Item.SetFromBytes(equipped, charges, element, material, attribute, item, property);
            return mm3Item;
        }

        public void SetFromBytes(byte equipped, byte charges, byte element, byte material, byte attribute, byte item, byte property)
        {
            Base = (MM3ItemIndex)item;
            Index = item;
            Element = (MM3ItemElementalIndex)element;
            Material = (MM3ItemMaterialIndex)material;
            Attribute = (MM3ItemAttributeIndex)attribute;
            Property = (MM3ItemPropertyIndex)property;
            ChargesByte = charges;
            WhereEquipped = (EquipLocation)equipped;
        }

        public override ItemType Type
        {
            get
            {
                // Catch some that aren't sequential first
                switch (Base)
                {
                    case MM3ItemIndex.Helm:
                    case MM3ItemIndex.Gauntlets:
                    case MM3ItemIndex.Boots:
                    case MM3ItemIndex.Robes:
                    case MM3ItemIndex.Cape:
                    case MM3ItemIndex.Cloak:
                        return ItemType.Armor;
                    default:
                        break;
                }

                if (Base == MM3ItemIndex.None)
                    return ItemType.None;
                if (Base < MM3ItemIndex.Bardiche)
                    return ItemType.OneHandMelee;
                if (Base < MM3ItemIndex.ShortBow)
                    return ItemType.TwoHandMelee;
                if (Base < MM3ItemIndex.PaddedArmor)
                    return ItemType.Missile;
                if (Base < MM3ItemIndex.Shield)
                    return ItemType.Armor;
                if (Base == MM3ItemIndex.Shield)
                    return ItemType.Armor;
                if (Base < MM3ItemIndex.Rod)
                    return ItemType.Accessory;
                if (Base < MM3ItemIndex.UselessItem)
                    return ItemType.Miscellaneous;
                if (Base < MM3ItemIndex.Invalid)
                    return ItemType.Quest;
                return ItemType.None;
            }
        }

        public override bool Trashable
        {
            get
            {
                switch (Base)
                {
                    case MM3ItemIndex.GreenEyeballKey:
                    case MM3ItemIndex.RedWarriorsKey:
                    case MM3ItemIndex.SacredSilverSkull:
                    case MM3ItemIndex.AncientArtifactofGood:
                    case MM3ItemIndex.AncientArtifactofNeutrality:
                    case MM3ItemIndex.AncientArtifactofEvil:
                    case MM3ItemIndex.PreciousPearlofYouthandBeauty:
                    case MM3ItemIndex.BlackTerrorKey:
                    case MM3ItemIndex.KingsUltimatePowerOrb:
                    case MM3ItemIndex.AncientFizbinofMisfortune:
                    case MM3ItemIndex.GoldMasterKey:
                    case MM3ItemIndex.QuatlooCoin:
                    case MM3ItemIndex.HologramSequencingCard001:
                    case MM3ItemIndex.YellowFortressKey:
                    case MM3ItemIndex.BlueUnholyKey:
                    case MM3ItemIndex.HologramSequencingCard002:
                    case MM3ItemIndex.HologramSequencingCard003:
                    case MM3ItemIndex.HologramSequencingCard004:
                    case MM3ItemIndex.HologramSequencingCard005:
                    case MM3ItemIndex.HologramSequencingCard006:
                    case MM3ItemIndex.ZItem23:
                    case MM3ItemIndex.BluePriorityPassCard:
                    case MM3ItemIndex.InterspacialTransportBox:
                    case MM3ItemIndex.MightPotion:
                    case MM3ItemIndex.GoldenPyramidKeyCard:
                    case MM3ItemIndex.AlacornofIcarus:
                    case MM3ItemIndex.SeaShellofSerenity:
                        return false;
                    default:
                        return true;
                }
            }
        }

        public byte ChargesByte
        {
            get
            {
                return (byte) ((m_iChargesCurrent & (byte) MM3ChargesFlags.ChargesMask) | (Cursed ? (byte)MM3ChargesFlags.Cursed : 0) | (Broken ? (byte)MM3ChargesFlags.Broken : 0));
            }
            set
            {
                m_iChargesCurrent = (byte)(value & (byte)MM3ChargesFlags.ChargesMask);
                Cursed = ((MM3ChargesFlags)value).HasFlag(MM3ChargesFlags.Cursed);
                Broken = ((MM3ChargesFlags)value).HasFlag(MM3ChargesFlags.Broken);
            }
        }

        public override bool NotUsable
        {
            get
            {
                return GetUsableBy((MM3ItemIndex) Index) == MM345UsableFlags.None;
            }
        }

        public static MM3Item CreateRandom(ItemType type, BaseCharacter charUsable, bool bSingleModifier)
        {
            MM3Item item = new MM3Item();
            item.Randomize(type, charUsable, bSingleModifier);
            return item;
        }

        public override int Bonus
        {
            get
            {
                switch (Type)
                {
                    case ItemType.Weapon:
                    case ItemType.OneHandMelee:
                    case ItemType.TwoHandMelee:
                    case ItemType.Missile:
                        return ElementalEffect(Element).DamageValue + AttributeEffect(Attribute).Modifier;
                    case ItemType.Armor:
                    case ItemType.Accessory:
                        return ElementalEffect(Element).ResistValue + AttributeEffect(Attribute).Modifier;
                }
                return 0;
            }
        }

        public void Randomize(ItemType type = ItemType.None, BaseCharacter charUsable = null, bool bSingleModifier = false)
        {
            // Randomize everything except whether the item is equipped
            IntDeck deck = new IntDeck(1, (int)MM3ItemIndex.Invalid - 1);
            deck.Shuffle();

            foreach(int iIndex in deck.Cards)
            {
                Base = (MM3ItemIndex) iIndex;
                if (MatchTypeAndChar(type, charUsable))
                    break;
            }

            if (bSingleModifier)
            {
                switch (Global.Rand.Next(4))
                {
                    case 0:
                        Element = (MM3ItemElementalIndex)Global.Rand.Next((int)MM3ItemElementalIndex.Invalid);
                        break;
                    case 1:
                        Material = (MM3ItemMaterialIndex)Global.Rand.Next((int)MM3ItemMaterialIndex.Invalid);
                        break;
                    case 2:
                        Attribute = (MM3ItemAttributeIndex)Global.Rand.Next((int)MM3ItemAttributeIndex.Invalid);
                        break;
                    case 3:
                        Property = (MM3ItemPropertyIndex)Global.Rand.Next((int)MM3ItemPropertyIndex.Invalid);
                        break;
                }
            }
            else
            {
                Element = (MM3ItemElementalIndex)Global.Rand.Next((int)MM3ItemElementalIndex.Invalid);
                Material = (MM3ItemMaterialIndex)Global.Rand.Next((int)MM3ItemMaterialIndex.Invalid);
                Attribute = (MM3ItemAttributeIndex)Global.Rand.Next((int)MM3ItemAttributeIndex.Invalid);
                Property = (MM3ItemPropertyIndex)Global.Rand.Next((int)MM3ItemPropertyIndex.Invalid);
            }
            m_iChargesCurrent = (byte)Global.Rand.Next(64);
            if (charUsable == null)
            {
                Cursed = Global.Rand.Next(6) == 1;
                Broken = Global.Rand.Next(6) == 0;
            }
        }

        public override byte[] Serialize()
        {
            return new byte[] { (byte)WhereEquipped, ChargesByte, (byte)Element, (byte)Material, (byte)Attribute, (byte)Base, (byte)Property };
        }

        public static MM3Item FromBagBytes(byte[] bytes)
        {
            return MM3Item.Create(bytes[0], bytes[1], bytes[2], bytes[3], bytes[4], bytes[5], bytes[6]);
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
                    case ItemType.Quest: return "Quest";
                    default: return String.Empty;
                }
            }
        }

        public override string DescriptionString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                string strPrefix1 = GetItemElementalString(Element);
                string strPrefix2 = GetItemMaterialString(Material);
                string strPrefix3 = GetItemAttributeString(Attribute);
                string strSuffix = GetItemPropertyString(Property);
                string strName = GetItemName(Base);
                if (!String.IsNullOrWhiteSpace(strPrefix1))
                    sb.AppendFormat("{0} ", strPrefix1);
                if (!String.IsNullOrWhiteSpace(strPrefix2))
                    sb.AppendFormat("{0} ", strPrefix2);
                if (!String.IsNullOrWhiteSpace(strPrefix3))
                    sb.AppendFormat("{0} ", strPrefix3);
                sb.Append(strName);
                if (!String.IsNullOrWhiteSpace(strSuffix))
                    sb.AppendFormat(" of {0}", strSuffix);
                if (sb.Length > 0)
                    sb[0] = Char.ToUpper(sb[0]);
                return sb.ToString();
            }
        }

        public static int BaseItemValue(MM3ItemIndex index)
        {
            switch (index)
            {
                case MM3ItemIndex.BroadSword: return 100;
                case MM3ItemIndex.Club: return 1;
                case MM3ItemIndex.Cudgel: return 15;
                case MM3ItemIndex.Cutlass: return 40;
                case MM3ItemIndex.Dagger: return 8;
                case MM3ItemIndex.Flail: return 100;
                case MM3ItemIndex.HandAxe: return 10;
                case MM3ItemIndex.Katana: return 150;
                case MM3ItemIndex.LongSword: return 50;
                case MM3ItemIndex.Mace: return 50;
                case MM3ItemIndex.Maul: return 30;
                case MM3ItemIndex.Nunchakas: return 30;
                case MM3ItemIndex.Sabre: return 60;
                case MM3ItemIndex.Scimitar: return 80;
                case MM3ItemIndex.ShortSword: return 15;
                case MM3ItemIndex.Spear: return 15;
                case MM3ItemIndex.Wakazashi: return 60;
                case MM3ItemIndex.Bardiche: return 200;
                case MM3ItemIndex.BattleAxe: return 100;
                case MM3ItemIndex.Flamberge: return 400;
                case MM3ItemIndex.Glaive: return 80;
                case MM3ItemIndex.GrandAxe: return 200;
                case MM3ItemIndex.GreatAxe: return 300;
                case MM3ItemIndex.Halberd: return 250;
                case MM3ItemIndex.Hammer: return 120;
                case MM3ItemIndex.Naginata: return 300;
                case MM3ItemIndex.Pike: return 180;
                case MM3ItemIndex.Staff: return 40;
                case MM3ItemIndex.Trident: return 100;
                case MM3ItemIndex.Crossbow: return 50;
                case MM3ItemIndex.LongBow: return 100;
                case MM3ItemIndex.ShortBow: return 25;
                case MM3ItemIndex.Sling: return 15;
                case MM3ItemIndex.PaddedArmor: return 20;
                case MM3ItemIndex.LeatherArmor: return 40;
                case MM3ItemIndex.ScaleArmor: return 100;
                case MM3ItemIndex.RingMail: return 200;
                case MM3ItemIndex.ChainMail: return 400;
                case MM3ItemIndex.SplintMail: return 600;
                case MM3ItemIndex.PlateMail: return 1000;
                case MM3ItemIndex.PlateArmor: return 2000;
                case MM3ItemIndex.Box: return 10;
                case MM3ItemIndex.Coin: return 10;
                case MM3ItemIndex.Gem: return 500;
                case MM3ItemIndex.Horn: return 20;
                case MM3ItemIndex.Jewel: return 1000;
                case MM3ItemIndex.Orb: return 100;
                case MM3ItemIndex.Rod: return 50;
                case MM3ItemIndex.Shield: return 100;
                case MM3ItemIndex.Wand: return 50;
                case MM3ItemIndex.Whistle: return 10;
                case MM3ItemIndex.Crown: return 1000;
                case MM3ItemIndex.Helm: return 60;
                case MM3ItemIndex.Tiara: return 200;
                case MM3ItemIndex.Cape: return 200;
                case MM3ItemIndex.Cloak: return 250;
                case MM3ItemIndex.Robes: return 150;
                case MM3ItemIndex.Belt: return 100;
                case MM3ItemIndex.Boots: return 40;
                case MM3ItemIndex.Gauntlets: return 100;
                case MM3ItemIndex.Amulet: return 2000;
                case MM3ItemIndex.Necklace: return 1000;
                case MM3ItemIndex.Pendant: return 500;
                case MM3ItemIndex.Broach: return 250;
                case MM3ItemIndex.Cameo: return 300;
                case MM3ItemIndex.Charm: return 50;
                case MM3ItemIndex.Medal: return 100;
                case MM3ItemIndex.Scarab: return 200;
                case MM3ItemIndex.Ring: return 100;
                case MM3ItemIndex.Jewelry: return 1000;
                case MM3ItemIndex.AncientJewelry: return 2000;
                default: return 0;
            }
        }

        public static decimal MaterialValueMultiplier(MM3ItemMaterialIndex material)
        {
            switch (material)
            {
                case MM3ItemMaterialIndex.Wooden: return 0.10M;
                case MM3ItemMaterialIndex.Leather: return 0.25M;
                case MM3ItemMaterialIndex.Brass: return 0.50M;
                case MM3ItemMaterialIndex.Bronze: return 0.75M;
                case MM3ItemMaterialIndex.Iron: return 2;
                case MM3ItemMaterialIndex.Silver: return 5;
                case MM3ItemMaterialIndex.Steel: return 10;
                case MM3ItemMaterialIndex.Gold: return 20;
                case MM3ItemMaterialIndex.Platinum: return 50;
                case MM3ItemMaterialIndex.Glass: return 2;
                case MM3ItemMaterialIndex.Coral: return 3;
                case MM3ItemMaterialIndex.Crystal: return 5;
                case MM3ItemMaterialIndex.Lapis: return 10;
                case MM3ItemMaterialIndex.Pearl: return 20;
                case MM3ItemMaterialIndex.Amber: return 30;
                case MM3ItemMaterialIndex.Ebony: return 40;
                case MM3ItemMaterialIndex.Quartz: return 50;
                case MM3ItemMaterialIndex.Ruby: return 60;
                case MM3ItemMaterialIndex.Emerald: return 70;
                case MM3ItemMaterialIndex.Sapphire: return 80;
                case MM3ItemMaterialIndex.Diamond: return 90;
                case MM3ItemMaterialIndex.Obsidian: return 100;
                default: return 1;
            }
        }

        public static int ElementalValueModifier(MM3ItemElementalIndex element)
        {
            switch (element)
            {
                case MM3ItemElementalIndex.Burning: return 200;
                case MM3ItemElementalIndex.Fiery: return 300;
                case MM3ItemElementalIndex.Pyric: return 400;
                case MM3ItemElementalIndex.Fuming: return 500;
                case MM3ItemElementalIndex.Flaming: return 1000;
                case MM3ItemElementalIndex.Seething: return 1500;
                case MM3ItemElementalIndex.Blazing: return 2000;
                case MM3ItemElementalIndex.Scorching: return 3000;
                case MM3ItemElementalIndex.Icy: return 200;
                case MM3ItemElementalIndex.Frost: return 400;
                case MM3ItemElementalIndex.Freezing: return 500;
                case MM3ItemElementalIndex.Cold: return 1000;
                case MM3ItemElementalIndex.Cryo: return 2000;
                case MM3ItemElementalIndex.Flickering: return 200;
                case MM3ItemElementalIndex.Sparking: return 300;
                case MM3ItemElementalIndex.Static: return 400;
                case MM3ItemElementalIndex.Flashing: return 500;
                case MM3ItemElementalIndex.Shocking: return 1000;
                case MM3ItemElementalIndex.Electric: return 1500;
                case MM3ItemElementalIndex.Dyna: return 2000;
                case MM3ItemElementalIndex.Acidic: return 200;
                case MM3ItemElementalIndex.Venemous: return 400;
                case MM3ItemElementalIndex.Poisonous: return 800;
                case MM3ItemElementalIndex.Toxic: return 1600;
                case MM3ItemElementalIndex.Noxious: return 3200;
                case MM3ItemElementalIndex.Glowing: return 200;
                case MM3ItemElementalIndex.Incandescent: return 300;
                case MM3ItemElementalIndex.Dense: return 400;
                case MM3ItemElementalIndex.Sonic: return 500;
                case MM3ItemElementalIndex.Power: return 1000;
                case MM3ItemElementalIndex.Thermal: return 1500;
                case MM3ItemElementalIndex.Radiating: return 2000;
                case MM3ItemElementalIndex.Kinetic: return 3000;
                case MM3ItemElementalIndex.Mystic: return 500;
                case MM3ItemElementalIndex.Magical: return 1000;
                case MM3ItemElementalIndex.Ectoplasmic: return 2500;
                default: return 0;
            }
        }

        public static int AttributeValueModifier(MM3ItemAttributeIndex attribute)
        {
            switch (attribute)
            {
                case MM3ItemAttributeIndex.Might: return 200;
                case MM3ItemAttributeIndex.Strength: return 300;
                case MM3ItemAttributeIndex.Warrior: return 500;
                case MM3ItemAttributeIndex.Ogre: return 800;
                case MM3ItemAttributeIndex.Giant: return 1200;
                case MM3ItemAttributeIndex.Thunder: return 1700;
                case MM3ItemAttributeIndex.Force: return 2300;
                case MM3ItemAttributeIndex.Power: return 3000;
                case MM3ItemAttributeIndex.Dragon: return 3800;
                case MM3ItemAttributeIndex.Photon: return 4700;
                case MM3ItemAttributeIndex.Clever: return 200;
                case MM3ItemAttributeIndex.Mind: return 300;
                case MM3ItemAttributeIndex.Sage: return 500;
                case MM3ItemAttributeIndex.Thought: return 800;
                case MM3ItemAttributeIndex.Knowledge: return 1200;
                case MM3ItemAttributeIndex.Intellect: return 1700;
                case MM3ItemAttributeIndex.Wisdom: return 2300;
                case MM3ItemAttributeIndex.Genius: return 3000;
                case MM3ItemAttributeIndex.Buddy: return 200;
                case MM3ItemAttributeIndex.Friendship: return 300;
                case MM3ItemAttributeIndex.Charm: return 500;
                case MM3ItemAttributeIndex.Personality: return 800;
                case MM3ItemAttributeIndex.Charisma: return 1200;
                case MM3ItemAttributeIndex.Leadership: return 1700;
                case MM3ItemAttributeIndex.Ego: return 2300;
                case MM3ItemAttributeIndex.Holy: return 3000;
                case MM3ItemAttributeIndex.Quick: return 200;
                case MM3ItemAttributeIndex.Swift: return 300;
                case MM3ItemAttributeIndex.Fast: return 500;
                case MM3ItemAttributeIndex.Rapid: return 800;
                case MM3ItemAttributeIndex.Speed: return 1200;
                case MM3ItemAttributeIndex.Wind: return 1700;
                case MM3ItemAttributeIndex.Accelerator: return 2300;
                case MM3ItemAttributeIndex.Velocity: return 3000;
                case MM3ItemAttributeIndex.Sharp: return 300;
                case MM3ItemAttributeIndex.Accurate: return 500;
                case MM3ItemAttributeIndex.Marksman: return 1000;
                case MM3ItemAttributeIndex.Precision: return 1500;
                case MM3ItemAttributeIndex.True: return 2000;
                case MM3ItemAttributeIndex.Exacto: return 3000;
                case MM3ItemAttributeIndex.Clover: return 500;
                case MM3ItemAttributeIndex.Chance: return 1000;
                case MM3ItemAttributeIndex.Winners: return 1500;
                case MM3ItemAttributeIndex.Lucky: return 2000;
                case MM3ItemAttributeIndex.Gamblers: return 2500;
                case MM3ItemAttributeIndex.Leprechauns: return 3000;
                case MM3ItemAttributeIndex.Vigor: return 400;
                case MM3ItemAttributeIndex.Health: return 600;
                case MM3ItemAttributeIndex.Life: return 1000;
                case MM3ItemAttributeIndex.Troll: return 2000;
                case MM3ItemAttributeIndex.Vampiric: return 5000;
                case MM3ItemAttributeIndex.Spell: return 400;
                case MM3ItemAttributeIndex.Castors: return 800;
                case MM3ItemAttributeIndex.Witch: return 1200;
                case MM3ItemAttributeIndex.Mage: return 1600;
                case MM3ItemAttributeIndex.Archmage: return 2000;
                case MM3ItemAttributeIndex.Arcane: return 2500;
                case MM3ItemAttributeIndex.Protection: return 200;
                case MM3ItemAttributeIndex.Armored: return 400;
                case MM3ItemAttributeIndex.Defender: return 600;
                case MM3ItemAttributeIndex.Stealth: return 1000;
                case MM3ItemAttributeIndex.Divine: return 1600;
                case MM3ItemAttributeIndex.Mugger: return 400;
                case MM3ItemAttributeIndex.Burgler: return 600;
                case MM3ItemAttributeIndex.Looter: return 800;
                case MM3ItemAttributeIndex.Brigand: return 1000;
                case MM3ItemAttributeIndex.Filch: return 1200;
                case MM3ItemAttributeIndex.Thief: return 1400;
                case MM3ItemAttributeIndex.Rogue: return 1600;
                case MM3ItemAttributeIndex.Plunder: return 1800;
                case MM3ItemAttributeIndex.Criminal: return 2000;
                case MM3ItemAttributeIndex.Pirate: return 2500;
                default: return 0;
            }
        }

        public static int PropertyValueModifier(MM3ItemPropertyIndex property)
        {
            switch (property)
            {
                case MM3ItemPropertyIndex.Light:
                case MM3ItemPropertyIndex.Awakening:
                case MM3ItemPropertyIndex.MagicDetection:
                case MM3ItemPropertyIndex.Arrows:
                case MM3ItemPropertyIndex.Aid:
                case MM3ItemPropertyIndex.Fists:
                case MM3ItemPropertyIndex.EnergyBlasts:
                case MM3ItemPropertyIndex.Sleeping:
                case MM3ItemPropertyIndex.Revitalization:
                case MM3ItemPropertyIndex.Curing:
                case MM3ItemPropertyIndex.Sparking:
                case MM3ItemPropertyIndex.Ropes:
                case MM3ItemPropertyIndex.ToxicClouds:
                case MM3ItemPropertyIndex.Elements:
                case MM3ItemPropertyIndex.Pain:
                case MM3ItemPropertyIndex.Jumping:
                    return 100;
                case MM3ItemPropertyIndex.AcidStreams:
                case MM3ItemPropertyIndex.UndeadTurning:
                case MM3ItemPropertyIndex.Levitation:
                case MM3ItemPropertyIndex.WizardEyes:
                case MM3ItemPropertyIndex.Silence:
                case MM3ItemPropertyIndex.Blessing:
                case MM3ItemPropertyIndex.Identification:
                case MM3ItemPropertyIndex.Lightning:
                case MM3ItemPropertyIndex.HolyBonuses:
                case MM3ItemPropertyIndex.PowerCuring:
                case MM3ItemPropertyIndex.Nature:
                case MM3ItemPropertyIndex.Beacons:
                case MM3ItemPropertyIndex.Shielding:
                case MM3ItemPropertyIndex.Heroism:
                    return 200;
                case MM3ItemPropertyIndex.Immobilization:
                case MM3ItemPropertyIndex.WaterWalking:
                case MM3ItemPropertyIndex.FrostBiting:
                case MM3ItemPropertyIndex.MonsterFinding:
                case MM3ItemPropertyIndex.Fireballs:
                case MM3ItemPropertyIndex.ColdRays:
                case MM3ItemPropertyIndex.Antidotes:
                case MM3ItemPropertyIndex.AcidSpraying:
                case MM3ItemPropertyIndex.Distortion:
                case MM3ItemPropertyIndex.FeebleMinding:
                    return 300;
                case MM3ItemPropertyIndex.Vaccination:
                case MM3ItemPropertyIndex.Gating:
                case MM3ItemPropertyIndex.Teleportation:
                case MM3ItemPropertyIndex.Death:
                case MM3ItemPropertyIndex.FreeMovement:
                case MM3ItemPropertyIndex.Paralyzing:
                case MM3ItemPropertyIndex.DeadlySwarms:
                case MM3ItemPropertyIndex.Sanctuaries:
                case MM3ItemPropertyIndex.DragonBreath:
                case MM3ItemPropertyIndex.Feasting:
                    return 400;
                case MM3ItemPropertyIndex.FieryFlails:
                case MM3ItemPropertyIndex.Recharging:
                case MM3ItemPropertyIndex.Freezing:
                case MM3ItemPropertyIndex.Portals:
                case MM3ItemPropertyIndex.StoneToFlesh:
                case MM3ItemPropertyIndex.Duplication:
                case MM3ItemPropertyIndex.Disintegration:
                case MM3ItemPropertyIndex.HalfForMe:
                case MM3ItemPropertyIndex.RaisingTheDead:
                case MM3ItemPropertyIndex.Etherealization:
                    return 500;
                case MM3ItemPropertyIndex.DancingSwords:
                case MM3ItemPropertyIndex.MoonRays:
                case MM3ItemPropertyIndex.MassDistortion:
                case MM3ItemPropertyIndex.PrismaticLight:
                case MM3ItemPropertyIndex.Enchantment:
                case MM3ItemPropertyIndex.Incinerating:
                case MM3ItemPropertyIndex.HolyWords:
                case MM3ItemPropertyIndex.Resurrection:
                case MM3ItemPropertyIndex.Storms:
                case MM3ItemPropertyIndex.Megavoltage:
                case MM3ItemPropertyIndex.Infernos:
                case MM3ItemPropertyIndex.SunRays:
                case MM3ItemPropertyIndex.Implosions:
                case MM3ItemPropertyIndex.StarBursts:
                case MM3ItemPropertyIndex.TheGods:
                    return 600;
                default:
                    return 0;
            }
        }

        public static Ranges LevelRange(MM3ItemPropertyIndex material)
        {
            return new Ranges(PropertyValueModifier(material) / 100);
        }

        public static Ranges LevelRange(MM3ItemMaterialIndex material)
        {
            switch (material)
            {
                case MM3ItemMaterialIndex.Wooden: return new Ranges(1, 2, 3);
                case MM3ItemMaterialIndex.Leather: return new Ranges(1, 2, 3, 4);
                case MM3ItemMaterialIndex.Brass: return new Ranges(1, 2, 3, 4);
                case MM3ItemMaterialIndex.Bronze: return new Ranges(1, 2, 3, 4, 5);
                case MM3ItemMaterialIndex.Iron: return new Ranges(2, 3, 4, 5);
                case MM3ItemMaterialIndex.Silver: return new Ranges(2, 3, 4);
                case MM3ItemMaterialIndex.Steel: return new Ranges(2, 3, 4, 5);
                case MM3ItemMaterialIndex.Gold: return new Ranges(3, 4, 5);
                case MM3ItemMaterialIndex.Platinum: return new Ranges(4, 5);
                case MM3ItemMaterialIndex.Glass: return new Ranges(1, 2, 3);
                case MM3ItemMaterialIndex.Coral: return new Ranges(1, 2, 3, 4, 5);
                case MM3ItemMaterialIndex.Crystal: return new Ranges(1, 2, 3, 4);
                case MM3ItemMaterialIndex.Lapis: return new Ranges(1, 2, 3, 4);
                case MM3ItemMaterialIndex.Pearl: return new Ranges(2, 3);
                case MM3ItemMaterialIndex.Amber: return new Ranges(2, 3, 4);
                case MM3ItemMaterialIndex.Ebony: return new Ranges(3, 4);
                case MM3ItemMaterialIndex.Quartz: return new Ranges(4);
                case MM3ItemMaterialIndex.Ruby: return new Ranges(4, 5);
                case MM3ItemMaterialIndex.Emerald: return new Ranges(4, 5);
                case MM3ItemMaterialIndex.Sapphire: return new Ranges(5);
                case MM3ItemMaterialIndex.Diamond: return new Ranges(5);
                case MM3ItemMaterialIndex.Obsidian: return new Ranges(5, 6);
                default: return Ranges.Empty;
            }
        }

        public static Ranges LevelRange(MM3ItemAttributeIndex attrib)
        {
            switch (attrib)
            {
                case MM3ItemAttributeIndex.Might: return new Ranges(1);
                case MM3ItemAttributeIndex.Strength: return new Ranges(1,2,3,4);
                case MM3ItemAttributeIndex.Warrior: return new Ranges(1,2,3);
                case MM3ItemAttributeIndex.Ogre: return new Ranges(1,2,3,4);
                case MM3ItemAttributeIndex.Giant: return new Ranges(2,3,4,5);
                case MM3ItemAttributeIndex.Thunder: return new Ranges(3,4,5);
                case MM3ItemAttributeIndex.Force: return new Ranges(4,5);
                case MM3ItemAttributeIndex.Power: return new Ranges(5);
                case MM3ItemAttributeIndex.Dragon: return new Ranges(5);
                case MM3ItemAttributeIndex.Photon: return new Ranges(5,6);
                case MM3ItemAttributeIndex.Clever: return new Ranges(1);
                case MM3ItemAttributeIndex.Mind: return new Ranges(1,2,3,4);
                case MM3ItemAttributeIndex.Sage: return new Ranges(1,2,3);
                case MM3ItemAttributeIndex.Thought: return new Ranges(2,3);
                case MM3ItemAttributeIndex.Knowledge: return new Ranges(2,3);
                case MM3ItemAttributeIndex.Intellect: return new Ranges(3,4);
                case MM3ItemAttributeIndex.Wisdom: return new Ranges(4,5);
                case MM3ItemAttributeIndex.Genius: return new Ranges(5,6);
                case MM3ItemAttributeIndex.Buddy: return new Ranges(1);
                case MM3ItemAttributeIndex.Friendship: return new Ranges(2,3,4);
                case MM3ItemAttributeIndex.Charm: return new Ranges(2,3,4,5);
                case MM3ItemAttributeIndex.Personality: return new Ranges(2,3);
                case MM3ItemAttributeIndex.Charisma: return new Ranges(2,3,4);
                case MM3ItemAttributeIndex.Leadership: return new Ranges(3,4);
                case MM3ItemAttributeIndex.Ego: return new Ranges(4,5);
                case MM3ItemAttributeIndex.Holy: return new Ranges(5,6);
                case MM3ItemAttributeIndex.Quick: return new Ranges(1);
                case MM3ItemAttributeIndex.Swift: return new Ranges(1,2,3,4,5);
                case MM3ItemAttributeIndex.Fast: return new Ranges(1,2,3);
                case MM3ItemAttributeIndex.Rapid: return new Ranges(4,5);
                case MM3ItemAttributeIndex.Speed: return new Ranges(2,3,4,5);
                case MM3ItemAttributeIndex.Wind: return new Ranges(3,4,5);
                case MM3ItemAttributeIndex.Accelerator: return new Ranges(4,5);
                case MM3ItemAttributeIndex.Velocity: return new Ranges(6);
                case MM3ItemAttributeIndex.Sharp: return new Ranges(1,2);
                case MM3ItemAttributeIndex.Accurate: return new Ranges(1,2,3);
                case MM3ItemAttributeIndex.Marksman: return new Ranges(2,3,4,5);
                case MM3ItemAttributeIndex.Precision: return new Ranges(3,4,5);
                case MM3ItemAttributeIndex.True: return new Ranges(4,5);
                case MM3ItemAttributeIndex.Exacto: return new Ranges(5,6);
                case MM3ItemAttributeIndex.Clover: return new Ranges(1,2,3,4);
                case MM3ItemAttributeIndex.Chance: return new Ranges(1,2,3,4,5);
                case MM3ItemAttributeIndex.Winners: return new Ranges(2,3,4,5);
                case MM3ItemAttributeIndex.Lucky: return new Ranges(3,4,5);
                case MM3ItemAttributeIndex.Gamblers: return new Ranges(4,5);
                case MM3ItemAttributeIndex.Leprechauns: return new Ranges(5,6);
                case MM3ItemAttributeIndex.Vigor: return new Ranges(2,3,4,5);
                case MM3ItemAttributeIndex.Health: return new Ranges(1);
                case MM3ItemAttributeIndex.Life: return new Ranges(2,3,4);
                case MM3ItemAttributeIndex.Troll: return new Ranges(3,4);
                case MM3ItemAttributeIndex.Vampiric: return new Ranges(6);
                case MM3ItemAttributeIndex.Spell: return new Ranges(1,2);
                case MM3ItemAttributeIndex.Castors: return new Ranges(1,2,3,4);
                case MM3ItemAttributeIndex.Witch: return new Ranges(2,3);
                case MM3ItemAttributeIndex.Mage: return new Ranges(4);
                case MM3ItemAttributeIndex.Archmage: return new Ranges(5);
                case MM3ItemAttributeIndex.Arcane: return new Ranges(6);
                case MM3ItemAttributeIndex.Protection: return new Ranges(1,2);
                case MM3ItemAttributeIndex.Armored: return new Ranges(1,2,3);
                case MM3ItemAttributeIndex.Defender: return new Ranges(2,3,4);
                case MM3ItemAttributeIndex.Stealth: return new Ranges(3,4);
                case MM3ItemAttributeIndex.Divine: return new Ranges(5,6);
                case MM3ItemAttributeIndex.Mugger: return new Ranges(1,2);
                case MM3ItemAttributeIndex.Burgler: return new Ranges(1,2);
                case MM3ItemAttributeIndex.Looter: return new Ranges(2);
                case MM3ItemAttributeIndex.Brigand: return new Ranges(2);
                case MM3ItemAttributeIndex.Filch: return new Ranges(3,4);
                case MM3ItemAttributeIndex.Thief: return new Ranges(3,4);
                case MM3ItemAttributeIndex.Rogue: return new Ranges(4,5);
                case MM3ItemAttributeIndex.Plunder: return new Ranges(5);
                case MM3ItemAttributeIndex.Criminal: return new Ranges(5);
                case MM3ItemAttributeIndex.Pirate: return new Ranges(6);
                default: return Ranges.Empty;
            }
        }

        public static Ranges LevelRange(MM3ItemElementalIndex element)
        {
            switch (element)
            {
                case MM3ItemElementalIndex.Burning: return new Ranges(1, 4, 5);
                case MM3ItemElementalIndex.Fiery: return new Ranges(1, 2);
                case MM3ItemElementalIndex.Pyric: return new Ranges(1, 2, 3, 4);
                case MM3ItemElementalIndex.Fuming: return new Ranges(2, 3, 4);
                case MM3ItemElementalIndex.Flaming: return new Ranges(2, 3, 4, 5);
                case MM3ItemElementalIndex.Seething: return new Ranges(4, 5);
                case MM3ItemElementalIndex.Blazing: return new Ranges(4, 5);
                case MM3ItemElementalIndex.Scorching: return new Ranges(5);
                case MM3ItemElementalIndex.Flickering: return new Ranges(1, 2, 3);
                case MM3ItemElementalIndex.Sparking: return new Ranges(1, 2, 3);
                case MM3ItemElementalIndex.Static: return new Ranges(1, 2, 3, 4);
                case MM3ItemElementalIndex.Flashing: return new Ranges(2, 3, 4, 5);
                case MM3ItemElementalIndex.Shocking: return new Ranges(2, 3, 4, 5);
                case MM3ItemElementalIndex.Electric: return new Ranges(3, 4, 5);
                case MM3ItemElementalIndex.Dyna: return new Ranges(4, 5, 6);
                case MM3ItemElementalIndex.Icy: return new Ranges(1, 2, 5);
                case MM3ItemElementalIndex.Frost: return new Ranges(1, 3);
                case MM3ItemElementalIndex.Freezing: return new Ranges(3, 4, 5);
                case MM3ItemElementalIndex.Cold: return new Ranges(3, 4, 5);
                case MM3ItemElementalIndex.Cryo: return new Ranges(4, 5, 6);
                case MM3ItemElementalIndex.Acidic: return new Ranges(1, 2, 3, 5);
                case MM3ItemElementalIndex.Venemous: return new Ranges(1, 2, 3, 5);
                case MM3ItemElementalIndex.Poisonous: return new Ranges(2, 3, 4);
                case MM3ItemElementalIndex.Toxic: return new Ranges(4, 5);
                case MM3ItemElementalIndex.Noxious: return new Ranges(5, 6);
                case MM3ItemElementalIndex.Glowing: return new Ranges(1, 3);
                case MM3ItemElementalIndex.Incandescent: return new Ranges(1, 4);
                case MM3ItemElementalIndex.Dense: return new Ranges(1, 2, 3, 4);
                case MM3ItemElementalIndex.Sonic: return new Ranges(2, 3, 4, 5);
                case MM3ItemElementalIndex.Power: return new Ranges(2, 3, 4, 5);
                case MM3ItemElementalIndex.Thermal: return new Ranges(3, 4, 5);
                case MM3ItemElementalIndex.Radiating: return new Ranges(4);
                case MM3ItemElementalIndex.Kinetic: return new Ranges(6);
                case MM3ItemElementalIndex.Mystic: return new Ranges(1, 2, 3, 4);
                case MM3ItemElementalIndex.Magical: return new Ranges(3, 4, 5);
                case MM3ItemElementalIndex.Ectoplasmic: return new Ranges(5, 6);
                default: return Ranges.Empty;
            }
        }

        public override long Value
        {
            get
            {
                int iBaseValue = BaseItemValue(Base);
                if (iBaseValue == 0)
                    return 0;
                return (long) ((decimal) (iBaseValue * MaterialValueMultiplier(Material)) + 
                                  ElementalValueModifier(Element) +
                                  AttributeValueModifier(Attribute) +
                                  PropertyValueModifier(Property));
            }
        }

        public override string UsableByAlignment
        {
            get
            {
                return String.Empty;
            }
        }

        public override bool Duplicatable
        {
            get
            {
                switch (Base)
                {
                    case MM3ItemIndex.RopeAndHooks:
                    case MM3ItemIndex.UselessItem:
                    case MM3ItemIndex.AncientJewelry:
                    case MM3ItemIndex.GreenEyeballKey:
                    case MM3ItemIndex.RedWarriorsKey:
                    case MM3ItemIndex.SacredSilverSkull:
                    case MM3ItemIndex.AncientArtifactofGood:
                    case MM3ItemIndex.AncientArtifactofNeutrality:
                    case MM3ItemIndex.AncientArtifactofEvil:
                    case MM3ItemIndex.Jewelry:
                    case MM3ItemIndex.PreciousPearlofYouthandBeauty:
                    case MM3ItemIndex.BlackTerrorKey:
                    case MM3ItemIndex.KingsUltimatePowerOrb:
                    case MM3ItemIndex.AncientFizbinofMisfortune:
                    case MM3ItemIndex.GoldMasterKey:
                    case MM3ItemIndex.QuatlooCoin:
                    case MM3ItemIndex.HologramSequencingCard001:
                    case MM3ItemIndex.YellowFortressKey:
                    case MM3ItemIndex.BlueUnholyKey:
                    case MM3ItemIndex.HologramSequencingCard002:
                    case MM3ItemIndex.HologramSequencingCard003:
                    case MM3ItemIndex.HologramSequencingCard004:
                    case MM3ItemIndex.HologramSequencingCard005:
                    case MM3ItemIndex.HologramSequencingCard006:
                    case MM3ItemIndex.ZItem23:
                    case MM3ItemIndex.BluePriorityPassCard:
                    case MM3ItemIndex.InterspacialTransportBox:
                    case MM3ItemIndex.MightPotion:
                    case MM3ItemIndex.GoldenPyramidKeyCard:
                    case MM3ItemIndex.AlacornofIcarus:
                    case MM3ItemIndex.SeaShellofSerenity:
                        return false;
                    default:
                        break;
                }

                switch (Material)
                {
                    case MM3ItemMaterialIndex.Ruby:
                    case MM3ItemMaterialIndex.Emerald:
                    case MM3ItemMaterialIndex.Sapphire:
                    case MM3ItemMaterialIndex.Diamond:
                    case MM3ItemMaterialIndex.Obsidian:
                        return false;
                    default:
                        break;
                }

                switch (Element)
                {
                    case MM3ItemElementalIndex.Blazing:
                    case MM3ItemElementalIndex.Scorching:
                    case MM3ItemElementalIndex.Electric:
                    case MM3ItemElementalIndex.Dyna:
                    case MM3ItemElementalIndex.Cold:
                    case MM3ItemElementalIndex.Cryo:
                    case MM3ItemElementalIndex.Toxic:
                    case MM3ItemElementalIndex.Noxious:
                    case MM3ItemElementalIndex.Radiating:
                    case MM3ItemElementalIndex.Kinetic:
                    case MM3ItemElementalIndex.Ectoplasmic:
                        return false;
                    default:
                        break;
                }

                switch (Attribute)
                {
                    case MM3ItemAttributeIndex.Photon:
                    case MM3ItemAttributeIndex.Genius:
                    case MM3ItemAttributeIndex.Holy:
                    case MM3ItemAttributeIndex.Velocity:
                    case MM3ItemAttributeIndex.True:
                    case MM3ItemAttributeIndex.Exacto:
                    case MM3ItemAttributeIndex.Gamblers:
                    case MM3ItemAttributeIndex.Leprechauns:
                    case MM3ItemAttributeIndex.Vampiric:
                    case MM3ItemAttributeIndex.Arcane:
                    case MM3ItemAttributeIndex.Stealth:
                    case MM3ItemAttributeIndex.Divine:
                    case MM3ItemAttributeIndex.Pirate:
                        return false;
                    default:
                        break;
                }

                switch (Property)
                {
                    case MM3ItemPropertyIndex.Portals:
                    case MM3ItemPropertyIndex.StoneToFlesh:
                    case MM3ItemPropertyIndex.HalfForMe:
                    case MM3ItemPropertyIndex.RaisingTheDead:
                    case MM3ItemPropertyIndex.Recharging:
                    case MM3ItemPropertyIndex.Freezing:
                    case MM3ItemPropertyIndex.Duplication:
                    case MM3ItemPropertyIndex.Disintegration:
                    case MM3ItemPropertyIndex.Etherealization:
                    case MM3ItemPropertyIndex.MoonRays:
                    case MM3ItemPropertyIndex.DancingSwords:
                    case MM3ItemPropertyIndex.Enchantment:
                    case MM3ItemPropertyIndex.Incinerating:
                    case MM3ItemPropertyIndex.Megavoltage:
                    case MM3ItemPropertyIndex.Infernos:
                    case MM3ItemPropertyIndex.Implosions:
                    case MM3ItemPropertyIndex.StarBursts:
                    case MM3ItemPropertyIndex.PrismaticLight:
                    case MM3ItemPropertyIndex.Storms:
                        return false;
                    default:
                        break;
                }

                return true;
            }
        }

        public override bool IsWeapon
        {
            get
            {
                switch (Type)
                {
                    case ItemType.Missile:
                    case ItemType.OneHandMelee:
                    case ItemType.TwoHandMelee:
                        return true;
                    default:
                        return false;
                }
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
                if (Element != MM3ItemElementalIndex.None)
                    sb.AppendFormat("Element: {0} ({1})\r\n", Global.Title(GetItemElementalString(Element)), ElementalEffect(Element).ToString(IsWeapon));
                if (Material != MM3ItemMaterialIndex.None)
                {
                    string strEffect = MaterialEffect(Material).ToString(Type);
                    if (String.IsNullOrWhiteSpace(strEffect))
                        sb.AppendFormat("Material: {0} (no effect for {1} items)\r\n", Global.Title(GetItemMaterialString(Material)), BaseTypeString);
                    else
                        sb.AppendFormat("Material: {0} ({1})\r\n", Global.Title(GetItemMaterialString(Material)), strEffect);
                }
                if (Attribute != MM3ItemAttributeIndex.None)
                    sb.AppendFormat("Attribute: {0} ({1})\r\n",  Global.Title(GetItemAttributeString(Attribute)), AttributeEffect(Attribute));
                if (Property != MM3ItemPropertyIndex.None)
                    sb.AppendFormat("Property: {0} ({1})\r\n",
                        Global.Title(GetItemPropertyString(Property)),
                        Global.Title(MM3SpellList.GetSpellName(MM3Item.ItemPropertyEffect(Property))));

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

                string strDam = DamageOrAC(Base, true);
                if (!String.IsNullOrWhiteSpace(strDam))
                    sb.AppendLine(strDam);

                sb.AppendFormat("Value: {0} Gold\r\n", Value);
                sb.AppendFormat("Duplicatable: {0}\r\n", Duplicatable ? "Yes" : "No");
                return sb.ToString();
            }
        }

        public override int ArmorClass { get { return ItemBaseType == ItemType.Armor ? GetArmorClass(Base) : 0; } }

        public static string DamageOrAC(MM3ItemIndex index, bool bFull)
        {
            DamageDice damage = GetItemDamage(index);
            int iAC = GetArmorClass(index);

            if (damage.Quantity != 0)
                return bFull ? String.Format("Damage: {0}", damage.StringWithAverage) : damage.ToString();
            if (iAC != 0)
                return String.Format("{0}{1}", bFull ? "Armor Class: " : "AC", iAC);
            return String.Empty;
        }

        public override BasicDamage BaseDamage
        {
            get
            {
                if (!IsWeapon)
                    return BasicDamage.Zero;
                return new BasicDamage(1, GetItemDamage(Base));
            }
        }

        public override string DebugString
        {
            get
            {
                return String.Format("Element:{0:D3}, Attribute:{1:D3}, Material:{2:D3}, Suffix:{3:D3}", (int)Element, (int)Attribute, (int)Material, (int)Property);
            }
        }

        public override string EquipEffects
        {
            get
            {
                ElementDamageResistance elementalEffect = ElementalEffect(Element);
                AttributeModifier attributeEffect = AttributeEffect(Attribute);
                DamageDice damage = GetItemDamage(Base);

                string strEquipElement = "";
                if (elementalEffect.ResistValue > 0 && damage.Quantity == 0)
                    strEquipElement = String.Format("{0}Res{1}", Global.SingleResistance(elementalEffect.ResistElement), Global.AddPlus(elementalEffect.ResistValue));
                string strEquipAttribute = "";
                if (attributeEffect.Modifier > 0)
                    strEquipAttribute = String.Format("{0}{1}", Global.GenericAttributeString(attributeEffect.Attribute), Global.AddPlus(attributeEffect.Modifier));

                if (String.IsNullOrWhiteSpace(strEquipAttribute))
                    return strEquipElement;
                if (String.IsNullOrWhiteSpace(strEquipElement))
                    return strEquipAttribute;
                return String.Format("{0}, {1}", strEquipElement, strEquipAttribute);
            }
        }

        public override string DamageStringFull
        {
            get
            {
                ElementDamageResistance elementalEffect = ElementalEffect(Element);
                HitDamageAC materialEffect = MaterialEffect(Material);
                DamageDice damage = GetItemDamage(Base);

                if (damage.Quantity < 1)
                    return String.Empty;

                string strBonus = "";
                if (elementalEffect.DamageValue > 0 && damage.Quantity > 0)
                    strBonus = String.Format("+{0} {1}", elementalEffect.DamageValue, Global.SingleResistance(elementalEffect.DamageElement));

                damage.Bonus += materialEffect.Damage;
                string strDamage = damage.ToString();

                if (String.IsNullOrWhiteSpace(strDamage))
                    return String.Empty;

                if (String.IsNullOrWhiteSpace(strBonus))
                    return String.Format("{0}{1}", strDamage, materialEffect.AddHitString);

                return String.Format("{0} {1}{2}", strDamage, strBonus, materialEffect.AddHitString);
            }
        }

        public override int ArmorClassFull
        {
            get
            {
                if (!IsArmor && !IsAccessory)
                    return 0;
                return ArmorClass + MaterialEffect(Material).AC;
            }
        }

        public override int ToHitBonus { get { return IsWeapon ? MaterialEffect(Material).Hit : 0; } }

        public override string GetLongDescription(GenericAlignmentValue currentAlign, GenericClass currentClass, string strOverrideName)
        {
            HitDamageAC materialEffect = MaterialEffect(Material);
            DamageDice damage = GetItemDamage(Base);
            string strBrokenCursed = "";
            if (Broken)
                strBrokenCursed = "BROKEN ";
            if (Cursed)
                strBrokenCursed = strBrokenCursed + "CURSED ";
            string strUse = "";
            if (Property != MM3ItemPropertyIndex.None)
                strUse = String.Format("{0} [{1}]", MM3SpellList.GetSpellName(MM3Item.ItemPropertyEffect((MM3ItemPropertyIndex)Property)), m_iChargesCurrent);
            string strItem = Global.Title(GetItemName(Base));
            string strUsable = "";
            if (!IsUsable(currentClass) && currentClass != GenericClass.None)
                strUsable = String.Format(" (!{0})", MM3Character.ClassString(currentClass));

            string strType = ItemTypeAbbr;
            int iAC = GetArmorClass(Base) + materialEffect.AC;
            string strDamage = damage.Quantity > 0 ? DamageStringFull : (iAC != 0 ? String.Format("AC {0}", iAC) : "");

            string strEquipEffect = EquipEffects;

            return String.Format("{0}{1}{2}{3}, {4}{5}{6}{7} Gold",
                strBrokenCursed,
                String.IsNullOrWhiteSpace(strOverrideName) ? strItem : strOverrideName,
                strUsable,
                Global.SpaceParen(strType),
                Global.AddCommaSpace(strDamage),
                Global.AddCommaSpace(strEquipEffect),
                Global.AddCommaSpace(strUse),
                Value);
        }

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
            return MM3Item.Create((byte)EquipLocation.None, ChargesByte, (byte)Element, (byte)Material, (byte)Attribute, (byte)Index, (byte)Property);
        }

        public override EquipLocation CanEquipLocation
        {
            get
            {
                switch (Type)
                {
                    case ItemType.OneHandMelee: return EquipLocation.RightHand;
                    case ItemType.TwoHandMelee: return EquipLocation.BothHands;
                    case ItemType.Missile: return EquipLocation.Ranged;
                    default: break;
                }
                switch ((MM3ItemIndex)Index)
                {
                    case MM3ItemIndex.PaddedArmor:
                    case MM3ItemIndex.LeatherArmor:
                    case MM3ItemIndex.ScaleArmor:
                    case MM3ItemIndex.RingMail:
                    case MM3ItemIndex.ChainMail:
                    case MM3ItemIndex.SplintMail:
                    case MM3ItemIndex.PlateMail:
                    case MM3ItemIndex.PlateArmor: return EquipLocation.Torso;
                    case MM3ItemIndex.Rod:
                    case MM3ItemIndex.Jewel:
                    case MM3ItemIndex.Gem:
                    case MM3ItemIndex.Box:
                    case MM3ItemIndex.Orb:
                    case MM3ItemIndex.Horn:
                    case MM3ItemIndex.Coin:
                    case MM3ItemIndex.Wand:
                    case MM3ItemIndex.Whistle:
                    case MM3ItemIndex.Shield: return EquipLocation.LeftHand;
                    case MM3ItemIndex.Helm:
                    case MM3ItemIndex.Crown:
                    case MM3ItemIndex.Tiara: return EquipLocation.Head;
                    case MM3ItemIndex.Gauntlets: return EquipLocation.Gauntlet;
                    case MM3ItemIndex.Ring: return EquipLocation.Finger;
                    case MM3ItemIndex.Boots: return EquipLocation.Feet;
                    case MM3ItemIndex.Cape:
                    case MM3ItemIndex.Robes:
                    case MM3ItemIndex.Cloak: return EquipLocation.Cloak;
                    case MM3ItemIndex.Belt: return EquipLocation.Belt;
                    case MM3ItemIndex.Medal:
                    case MM3ItemIndex.Charm:
                    case MM3ItemIndex.Cameo:
                    case MM3ItemIndex.Scarab:
                    case MM3ItemIndex.Broach: return EquipLocation.Medallion;
                    case MM3ItemIndex.Pendant:
                    case MM3ItemIndex.Amulet:
                    case MM3ItemIndex.Necklace: return EquipLocation.Neck;
                    default: return EquipLocation.None;
                }
            }
        }

        public override CompareResult CompareTo(Item item)
        {
            if (!(item is MM3Item))
                return CompareResult.Uncomparable;
            if (item == this)
                return CompareResult.Identical;
            if (Type != item.Type)
                return CompareResult.Uncomparable;

            MM3Item mm3Item = item as MM3Item;

            if (mm3Item.Cursed && !Cursed)
                return CompareResult.Better;     // Cursed is always worse than non-cursed
            else if (!mm3Item.Cursed && Cursed)
                return CompareResult.Worse;    // ... and vice-versa

            if (CanEquipLocation != mm3Item.CanEquipLocation)
                return CompareResult.Uncomparable;

            if (!CanEquip)
                return CompareResult.Uncomparable;

            ElementDamageResistance elemental1 = ElementalEffect(Element);
            HitDamageAC hdac1 = MaterialEffect(Material);
            AttributeModifier modifier1 = AttributeEffect(Attribute);
            MM3SpellIndex spell1 = ItemPropertyEffect(Property);

            ElementDamageResistance elemental2 = ElementalEffect(mm3Item.Element);
            HitDamageAC hdac2 = MaterialEffect(mm3Item.Material);
            AttributeModifier modifier2 = AttributeEffect(mm3Item.Attribute);
            MM3SpellIndex spell2 = ItemPropertyEffect(mm3Item.Property);

            if (elemental1.DamageElement != elemental2.DamageElement && !(elemental1.DamageElement == GenericResistanceFlags.None || elemental2.DamageElement == GenericResistanceFlags.None))
                return CompareResult.Uncomparable;
            if (modifier1.Attribute != modifier2.Attribute && !(modifier1.Attribute == GenericAttribute.None || modifier2.Attribute == GenericAttribute.None))
                return CompareResult.Uncomparable;
            if (spell1 != spell2 && !(spell1 == MM3SpellIndex.None || spell2 == MM3SpellIndex.None))
                return CompareResult.Uncomparable;

            CompareResult compElement = CompareValues(elemental1.DamageValue, elemental2.DamageValue);
            CompareResult compAttribute = CompareValues(modifier1.Modifier, modifier2.Modifier);
            CompareResult compHDAC = IsWeapon ? CompareValues(hdac1.Damage, hdac2.Damage) : CompareValues(hdac1.AC, hdac2.AC);

            CompareResult compAll = Compare(compElement, compAttribute, compHDAC);
            if (compAll == CompareResult.Uncomparable)
                return CompareResult.Uncomparable;

            if (IsWeapon)
                return Compare(compAll, CompareValues(BaseDamage.Average, mm3Item.BaseDamage.Average));
            if (IsArmor)
                return Compare(compAll, CompareValues(ArmorClassFull, mm3Item.ArmorClassFull));
            if (IsAccessory)
                return compAll;
            return CompareResult.Uncomparable;
        }

        public static DamageDice GetItemDamage(MM3ItemIndex index)
        {
            switch (index)
            {
                case MM3ItemIndex.None: return DamageDice.Zero;
                case MM3ItemIndex.LongSword: return new DamageDice(3, 3, 0);
                case MM3ItemIndex.ShortSword: return new DamageDice(3, 2, 0);
                case MM3ItemIndex.BroadSword: return new DamageDice(4, 3, 0);
                case MM3ItemIndex.Scimitar: return new DamageDice(5, 2, 0);
                case MM3ItemIndex.Cutlass: return new DamageDice(4, 2, 0);
                case MM3ItemIndex.Sabre: return new DamageDice(2, 4, 0);
                case MM3ItemIndex.Club: return new DamageDice(3, 1, 0);
                case MM3ItemIndex.HandAxe: return new DamageDice(3, 2, 0);
                case MM3ItemIndex.Katana: return new DamageDice(3, 4, 0);
                case MM3ItemIndex.Nunchakas: return new DamageDice(3, 2, 0);
                case MM3ItemIndex.Wakazashi: return new DamageDice(3, 3, 0);
                case MM3ItemIndex.Dagger: return new DamageDice(2, 2, 0);
                case MM3ItemIndex.Mace: return new DamageDice(4, 2, 0);
                case MM3ItemIndex.Flail: return new DamageDice(10, 1, 0);
                case MM3ItemIndex.Cudgel: return new DamageDice(6, 1, 0);
                case MM3ItemIndex.Maul: return new DamageDice(8, 1, 0);
                case MM3ItemIndex.Spear: return new DamageDice(9, 1, 0);
                case MM3ItemIndex.Bardiche: return new DamageDice(4, 4, 0);
                case MM3ItemIndex.Glaive: return new DamageDice(3, 4, 0);
                case MM3ItemIndex.Halberd: return new DamageDice(6, 3, 0);
                case MM3ItemIndex.Pike: return new DamageDice(8, 2, 0);
                case MM3ItemIndex.Flamberge: return new DamageDice(5, 4, 0);
                case MM3ItemIndex.Trident: return new DamageDice(6, 2, 0);
                case MM3ItemIndex.Staff: return new DamageDice(4, 2, 0);
                case MM3ItemIndex.Hammer: return new DamageDice(5, 2, 0);
                case MM3ItemIndex.Naginata: return new DamageDice(3, 5, 0);
                case MM3ItemIndex.BattleAxe: return new DamageDice(5, 3, 0);
                case MM3ItemIndex.GrandAxe: return new DamageDice(6, 3, 0);
                case MM3ItemIndex.GreatAxe: return new DamageDice(7, 3, 0);
                case MM3ItemIndex.ShortBow: return new DamageDice(2, 3, 0);
                case MM3ItemIndex.LongBow: return new DamageDice(2, 5, 0);
                case MM3ItemIndex.Crossbow: return new DamageDice(2, 4, 0);
                case MM3ItemIndex.Sling: return new DamageDice(2, 2, 0);
                default: return DamageDice.Zero;
            }
        }

        public static int GetArmorClass(MM3ItemIndex index)
        {
            switch (index)
            {
                case MM3ItemIndex.PaddedArmor: return 2;
                case MM3ItemIndex.LeatherArmor: return 3;
                case MM3ItemIndex.ScaleArmor: return 4;
                case MM3ItemIndex.RingMail: return 5;
                case MM3ItemIndex.ChainMail: return 6;
                case MM3ItemIndex.SplintMail: return 7;
                case MM3ItemIndex.PlateMail: return 8;
                case MM3ItemIndex.PlateArmor: return 10;
                case MM3ItemIndex.Shield: return 4;
                case MM3ItemIndex.Helm: return 2;
                case MM3ItemIndex.Gauntlets: return 1;
                case MM3ItemIndex.Boots: return 1;
                case MM3ItemIndex.Cloak: return 1;
                case MM3ItemIndex.Robes: return 1;
                case MM3ItemIndex.Cape: return 1;
                default: return 0;
            }
        }

        public static HitDamageAC MaterialEffect(MM3ItemMaterialIndex material)
        {
            switch(material)
            {
                case MM3ItemMaterialIndex.Wooden: return new HitDamageAC(-3,-3,-3);
                case MM3ItemMaterialIndex.Leather: return new HitDamageAC(-4,-6,0); 
                case MM3ItemMaterialIndex.Brass: return new HitDamageAC(3,-4,-2); 
                case MM3ItemMaterialIndex.Bronze: return new HitDamageAC(2,-2,-1); 
                case MM3ItemMaterialIndex.Iron: return new HitDamageAC(1,2,1);   
                case MM3ItemMaterialIndex.Silver: return new HitDamageAC(2,4,2);   
                case MM3ItemMaterialIndex.Steel: return new HitDamageAC(3,6,4);   
                case MM3ItemMaterialIndex.Gold: return new HitDamageAC(4,8,6);   
                case MM3ItemMaterialIndex.Platinum: return new HitDamageAC(6,10,8);  
                case MM3ItemMaterialIndex.Glass: return new HitDamageAC(0,0,0);   
                case MM3ItemMaterialIndex.Coral: return new HitDamageAC(1,1,1);   
                case MM3ItemMaterialIndex.Crystal: return new HitDamageAC(1,1,1);   
                case MM3ItemMaterialIndex.Lapis: return new HitDamageAC(2,2,2);   
                case MM3ItemMaterialIndex.Pearl: return new HitDamageAC(2,2,2);   
                case MM3ItemMaterialIndex.Amber: return new HitDamageAC(3,3,3);   
                case MM3ItemMaterialIndex.Ebony: return new HitDamageAC(4,4,4);   
                case MM3ItemMaterialIndex.Quartz: return new HitDamageAC(5,5,5);   
                case MM3ItemMaterialIndex.Ruby: return new HitDamageAC(6,12,10); 
                case MM3ItemMaterialIndex.Emerald: return new HitDamageAC(7,15,12); 
                case MM3ItemMaterialIndex.Sapphire: return new HitDamageAC(8,20,14); 
                case MM3ItemMaterialIndex.Diamond: return new HitDamageAC(9,30,16); 
                case MM3ItemMaterialIndex.Obsidian: return new HitDamageAC(10,50,20);
                default: return new HitDamageAC(0,0,0);
            }
        }

        public static ElementDamageResistance ElementalEffect(MM3ItemElementalIndex element)
        {
            switch (element)
            {
                case MM3ItemElementalIndex.Burning: return new ElementDamageResistance(GenericResistanceFlags.Fire, 2, 5);
                case MM3ItemElementalIndex.Fiery: return new ElementDamageResistance(GenericResistanceFlags.Fire, 3, 7);
                case MM3ItemElementalIndex.Pyric: return new ElementDamageResistance(GenericResistanceFlags.Fire, 4, 9);
                case MM3ItemElementalIndex.Fuming: return new ElementDamageResistance(GenericResistanceFlags.Fire, 5, 12);
                case MM3ItemElementalIndex.Flaming: return new ElementDamageResistance(GenericResistanceFlags.Fire, 10, 15);
                case MM3ItemElementalIndex.Seething: return new ElementDamageResistance(GenericResistanceFlags.Fire, 15, 20);
                case MM3ItemElementalIndex.Blazing: return new ElementDamageResistance(GenericResistanceFlags.Fire, 20, 25);
                case MM3ItemElementalIndex.Scorching: return new ElementDamageResistance(GenericResistanceFlags.Fire, 30, 30);
                case MM3ItemElementalIndex.Flickering: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 2, 5);
                case MM3ItemElementalIndex.Sparking: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 3, 7);
                case MM3ItemElementalIndex.Static: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 4, 9);
                case MM3ItemElementalIndex.Flashing: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 5, 12);
                case MM3ItemElementalIndex.Shocking: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 10, 15);
                case MM3ItemElementalIndex.Electric: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 15, 20);
                case MM3ItemElementalIndex.Dyna: return new ElementDamageResistance(GenericResistanceFlags.Electricity, 20, 25);
                case MM3ItemElementalIndex.Icy: return new ElementDamageResistance(GenericResistanceFlags.Cold, 2, 5);
                case MM3ItemElementalIndex.Frost: return new ElementDamageResistance(GenericResistanceFlags.Cold, 4, 10);
                case MM3ItemElementalIndex.Freezing: return new ElementDamageResistance(GenericResistanceFlags.Cold, 5, 15);
                case MM3ItemElementalIndex.Cold: return new ElementDamageResistance(GenericResistanceFlags.Cold, 10, 20);
                case MM3ItemElementalIndex.Cryo: return new ElementDamageResistance(GenericResistanceFlags.Cold, 20, 25);
                case MM3ItemElementalIndex.Acidic: return new ElementDamageResistance(GenericResistanceFlags.Poison, 2, 10);
                case MM3ItemElementalIndex.Venemous: return new ElementDamageResistance(GenericResistanceFlags.Poison, 4, 15);
                case MM3ItemElementalIndex.Poisonous: return new ElementDamageResistance(GenericResistanceFlags.Poison, 8, 20);
                case MM3ItemElementalIndex.Toxic: return new ElementDamageResistance(GenericResistanceFlags.Poison, 16, 25);
                case MM3ItemElementalIndex.Noxious: return new ElementDamageResistance(GenericResistanceFlags.Poison, 32, 40);
                case MM3ItemElementalIndex.Glowing: return new ElementDamageResistance(GenericResistanceFlags.Energy, 2, 5);
                case MM3ItemElementalIndex.Incandescent: return new ElementDamageResistance(GenericResistanceFlags.Energy, 3, 7);
                case MM3ItemElementalIndex.Dense: return new ElementDamageResistance(GenericResistanceFlags.Energy, 4, 9);
                case MM3ItemElementalIndex.Sonic: return new ElementDamageResistance(GenericResistanceFlags.Energy, 5, 11);
                case MM3ItemElementalIndex.Power: return new ElementDamageResistance(GenericResistanceFlags.Energy, 10, 13);
                case MM3ItemElementalIndex.Thermal: return new ElementDamageResistance(GenericResistanceFlags.Energy, 15, 15);
                case MM3ItemElementalIndex.Radiating: return new ElementDamageResistance(GenericResistanceFlags.Energy, 20, 20);
                case MM3ItemElementalIndex.Kinetic: return new ElementDamageResistance(GenericResistanceFlags.Energy, 30, 25);
                case MM3ItemElementalIndex.Mystic: return new ElementDamageResistance(GenericResistanceFlags.Magic, 5, 5);
                case MM3ItemElementalIndex.Magical: return new ElementDamageResistance(GenericResistanceFlags.Magic, 10, 10);
                case MM3ItemElementalIndex.Ectoplasmic: return new ElementDamageResistance(GenericResistanceFlags.Magic, 25, 20);
                default: return new ElementDamageResistance(GenericResistanceFlags.None, 0, 0);
            }
        }

        public static AttributeModifier AttributeEffect(MM3ItemAttributeIndex attribute)
        {
            switch (attribute)
            {
                case MM3ItemAttributeIndex.Might: return new AttributeModifier(GenericAttribute.Might, 2);
                case MM3ItemAttributeIndex.Strength: return new AttributeModifier(GenericAttribute.Might, 3);
                case MM3ItemAttributeIndex.Warrior: return new AttributeModifier(GenericAttribute.Might, 5);
                case MM3ItemAttributeIndex.Ogre: return new AttributeModifier(GenericAttribute.Might, 8);
                case MM3ItemAttributeIndex.Giant: return new AttributeModifier(GenericAttribute.Might, 12);
                case MM3ItemAttributeIndex.Thunder: return new AttributeModifier(GenericAttribute.Might, 17);
                case MM3ItemAttributeIndex.Force: return new AttributeModifier(GenericAttribute.Might, 23);
                case MM3ItemAttributeIndex.Power: return new AttributeModifier(GenericAttribute.Might, 30);
                case MM3ItemAttributeIndex.Dragon: return new AttributeModifier(GenericAttribute.Might, 38);
                case MM3ItemAttributeIndex.Photon: return new AttributeModifier(GenericAttribute.Might, 47);
                case MM3ItemAttributeIndex.Clever: return new AttributeModifier(GenericAttribute.Intellect, 2);
                case MM3ItemAttributeIndex.Mind: return new AttributeModifier(GenericAttribute.Intellect, 3);
                case MM3ItemAttributeIndex.Sage: return new AttributeModifier(GenericAttribute.Intellect, 5);
                case MM3ItemAttributeIndex.Thought: return new AttributeModifier(GenericAttribute.Intellect, 8);
                case MM3ItemAttributeIndex.Knowledge: return new AttributeModifier(GenericAttribute.Intellect, 12);
                case MM3ItemAttributeIndex.Intellect: return new AttributeModifier(GenericAttribute.Intellect, 17);
                case MM3ItemAttributeIndex.Wisdom: return new AttributeModifier(GenericAttribute.Intellect, 23);
                case MM3ItemAttributeIndex.Genius: return new AttributeModifier(GenericAttribute.Intellect, 30);
                case MM3ItemAttributeIndex.Buddy: return new AttributeModifier(GenericAttribute.Personality, 2);
                case MM3ItemAttributeIndex.Friendship: return new AttributeModifier(GenericAttribute.Personality, 3);
                case MM3ItemAttributeIndex.Charm: return new AttributeModifier(GenericAttribute.Personality, 5);
                case MM3ItemAttributeIndex.Personality: return new AttributeModifier(GenericAttribute.Personality, 8);
                case MM3ItemAttributeIndex.Charisma: return new AttributeModifier(GenericAttribute.Personality, 12);
                case MM3ItemAttributeIndex.Leadership: return new AttributeModifier(GenericAttribute.Personality, 17);
                case MM3ItemAttributeIndex.Ego: return new AttributeModifier(GenericAttribute.Personality, 23);
                case MM3ItemAttributeIndex.Holy: return new AttributeModifier(GenericAttribute.Personality, 30);
                case MM3ItemAttributeIndex.Quick: return new AttributeModifier(GenericAttribute.Speed, 2);
                case MM3ItemAttributeIndex.Swift: return new AttributeModifier(GenericAttribute.Speed, 3);
                case MM3ItemAttributeIndex.Fast: return new AttributeModifier(GenericAttribute.Speed, 5);
                case MM3ItemAttributeIndex.Rapid: return new AttributeModifier(GenericAttribute.Speed, 8);
                case MM3ItemAttributeIndex.Speed: return new AttributeModifier(GenericAttribute.Speed, 12);
                case MM3ItemAttributeIndex.Wind: return new AttributeModifier(GenericAttribute.Speed, 17);
                case MM3ItemAttributeIndex.Accelerator: return new AttributeModifier(GenericAttribute.Speed, 23);
                case MM3ItemAttributeIndex.Velocity: return new AttributeModifier(GenericAttribute.Speed, 30);
                case MM3ItemAttributeIndex.Sharp: return new AttributeModifier(GenericAttribute.Accuracy, 3);
                case MM3ItemAttributeIndex.Accurate: return new AttributeModifier(GenericAttribute.Accuracy, 5);
                case MM3ItemAttributeIndex.Marksman: return new AttributeModifier(GenericAttribute.Accuracy, 10);
                case MM3ItemAttributeIndex.Precision: return new AttributeModifier(GenericAttribute.Accuracy, 15);
                case MM3ItemAttributeIndex.True: return new AttributeModifier(GenericAttribute.Accuracy, 20);
                case MM3ItemAttributeIndex.Exacto: return new AttributeModifier(GenericAttribute.Accuracy, 30);
                case MM3ItemAttributeIndex.Clover: return new AttributeModifier(GenericAttribute.Luck, 5);
                case MM3ItemAttributeIndex.Chance: return new AttributeModifier(GenericAttribute.Luck, 10);
                case MM3ItemAttributeIndex.Winners: return new AttributeModifier(GenericAttribute.Luck, 15);
                case MM3ItemAttributeIndex.Lucky: return new AttributeModifier(GenericAttribute.Luck, 20);
                case MM3ItemAttributeIndex.Gamblers: return new AttributeModifier(GenericAttribute.Luck, 25);
                case MM3ItemAttributeIndex.Leprechauns: return new AttributeModifier(GenericAttribute.Luck, 30);
                case MM3ItemAttributeIndex.Vigor: return new AttributeModifier(GenericAttribute.HP, 4);
                case MM3ItemAttributeIndex.Health: return new AttributeModifier(GenericAttribute.HP, 6);
                case MM3ItemAttributeIndex.Life: return new AttributeModifier(GenericAttribute.HP, 10);
                case MM3ItemAttributeIndex.Troll: return new AttributeModifier(GenericAttribute.HP, 20);
                case MM3ItemAttributeIndex.Vampiric: return new AttributeModifier(GenericAttribute.HP, 50);
                case MM3ItemAttributeIndex.Spell: return new AttributeModifier(GenericAttribute.SP, 4);
                case MM3ItemAttributeIndex.Castors: return new AttributeModifier(GenericAttribute.SP, 8);
                case MM3ItemAttributeIndex.Witch: return new AttributeModifier(GenericAttribute.SP, 12);
                case MM3ItemAttributeIndex.Mage: return new AttributeModifier(GenericAttribute.SP, 16);
                case MM3ItemAttributeIndex.Archmage: return new AttributeModifier(GenericAttribute.SP, 20);
                case MM3ItemAttributeIndex.Arcane: return new AttributeModifier(GenericAttribute.SP, 25);
                case MM3ItemAttributeIndex.Protection: return new AttributeModifier(GenericAttribute.AC, 2);
                case MM3ItemAttributeIndex.Armored: return new AttributeModifier(GenericAttribute.AC, 4);
                case MM3ItemAttributeIndex.Defender: return new AttributeModifier(GenericAttribute.AC, 6);
                case MM3ItemAttributeIndex.Stealth: return new AttributeModifier(GenericAttribute.AC, 10);
                case MM3ItemAttributeIndex.Divine: return new AttributeModifier(GenericAttribute.AC, 16);
                case MM3ItemAttributeIndex.Mugger: return new AttributeModifier(GenericAttribute.Thievery, 4);
                case MM3ItemAttributeIndex.Burgler: return new AttributeModifier(GenericAttribute.Thievery, 6);
                case MM3ItemAttributeIndex.Looter: return new AttributeModifier(GenericAttribute.Thievery, 8);
                case MM3ItemAttributeIndex.Brigand: return new AttributeModifier(GenericAttribute.Thievery, 10);
                case MM3ItemAttributeIndex.Filch: return new AttributeModifier(GenericAttribute.Thievery, 12);
                case MM3ItemAttributeIndex.Thief: return new AttributeModifier(GenericAttribute.Thievery, 14);
                case MM3ItemAttributeIndex.Rogue: return new AttributeModifier(GenericAttribute.Thievery, 16);
                case MM3ItemAttributeIndex.Plunder: return new AttributeModifier(GenericAttribute.Thievery, 18);
                case MM3ItemAttributeIndex.Criminal: return new AttributeModifier(GenericAttribute.Thievery, 20);
                case MM3ItemAttributeIndex.Pirate: return new AttributeModifier(GenericAttribute.Thievery, 25);
                default: return new AttributeModifier(GenericAttribute.None, 0);
            }
        }

        public static MM3SpellIndex ItemPropertyEffect(MM3ItemPropertyIndex property)
        {
            switch (property)
            {
                case MM3ItemPropertyIndex.Light: return MM3SpellIndex.LightCleric;
                case MM3ItemPropertyIndex.Awakening: return MM3SpellIndex.AwakenCleric;
                case MM3ItemPropertyIndex.MagicDetection: return MM3SpellIndex.DetectMagic;
                case MM3ItemPropertyIndex.Arrows: return MM3SpellIndex.ElementalArrow;
                case MM3ItemPropertyIndex.Aid: return MM3SpellIndex.FirstAid;
                case MM3ItemPropertyIndex.Fists: return MM3SpellIndex.FlyingFist;
                case MM3ItemPropertyIndex.EnergyBlasts: return MM3SpellIndex.EnergyBlast;
                case MM3ItemPropertyIndex.Sleeping: return MM3SpellIndex.Sleep;
                case MM3ItemPropertyIndex.Revitalization: return MM3SpellIndex.Revitalize;
                case MM3ItemPropertyIndex.Curing: return MM3SpellIndex.CureWounds;
                case MM3ItemPropertyIndex.Sparking: return MM3SpellIndex.Sparks;
                case MM3ItemPropertyIndex.Ropes: return MM3SpellIndex.CreateRope;
                case MM3ItemPropertyIndex.ToxicClouds: return MM3SpellIndex.ToxicCloud;
                case MM3ItemPropertyIndex.Elements: return MM3SpellIndex.ProtectionFromElements;
                case MM3ItemPropertyIndex.Pain: return MM3SpellIndex.Pain;
                case MM3ItemPropertyIndex.Jumping: return MM3SpellIndex.Jump;
                case MM3ItemPropertyIndex.AcidStreams: return MM3SpellIndex.AcidStream;
                case MM3ItemPropertyIndex.UndeadTurning: return MM3SpellIndex.TurnUndead;
                case MM3ItemPropertyIndex.Levitation: return MM3SpellIndex.Levitate;
                case MM3ItemPropertyIndex.WizardEyes: return MM3SpellIndex.WizardEye;
                case MM3ItemPropertyIndex.Silence: return MM3SpellIndex.Silence;
                case MM3ItemPropertyIndex.Blessing: return MM3SpellIndex.Blessed;
                case MM3ItemPropertyIndex.Identification: return MM3SpellIndex.IdentifyMonster;
                case MM3ItemPropertyIndex.Lightning: return MM3SpellIndex.LightningBolt;
                case MM3ItemPropertyIndex.HolyBonuses: return MM3SpellIndex.HolyBonus;
                case MM3ItemPropertyIndex.PowerCuring: return MM3SpellIndex.PowerCure;
                case MM3ItemPropertyIndex.Nature: return MM3SpellIndex.NaturesCure;
                case MM3ItemPropertyIndex.Beacons: return MM3SpellIndex.LloydsBeacon;
                case MM3ItemPropertyIndex.Shielding: return MM3SpellIndex.PowerShield;
                case MM3ItemPropertyIndex.Heroism: return MM3SpellIndex.Heroism;
                case MM3ItemPropertyIndex.Immobilization: return MM3SpellIndex.Immobilize;
                case MM3ItemPropertyIndex.WaterWalking: return MM3SpellIndex.WalkOnWater;
                case MM3ItemPropertyIndex.FrostBiting: return MM3SpellIndex.FrostBite;
                case MM3ItemPropertyIndex.MonsterFinding: return MM3SpellIndex.DetectMonster;
                case MM3ItemPropertyIndex.Fireballs: return MM3SpellIndex.Fireball;
                case MM3ItemPropertyIndex.ColdRays: return MM3SpellIndex.ColdRay;
                case MM3ItemPropertyIndex.Antidotes: return MM3SpellIndex.CurePoison;
                case MM3ItemPropertyIndex.AcidSpraying: return MM3SpellIndex.AcidSpray;
                case MM3ItemPropertyIndex.Distortion: return MM3SpellIndex.TimeDistortion;
                case MM3ItemPropertyIndex.FeebleMinding: return MM3SpellIndex.FeebleMind;
                case MM3ItemPropertyIndex.Vaccination: return MM3SpellIndex.CureDisease;
                case MM3ItemPropertyIndex.Gating: return MM3SpellIndex.NaturesGate;
                case MM3ItemPropertyIndex.Teleportation: return MM3SpellIndex.Teleport;
                case MM3ItemPropertyIndex.Death: return MM3SpellIndex.FingerOfDeath;
                case MM3ItemPropertyIndex.FreeMovement: return MM3SpellIndex.CureParalysis;
                case MM3ItemPropertyIndex.Paralyzing: return MM3SpellIndex.Paralyze;
                case MM3ItemPropertyIndex.DeadlySwarms: return MM3SpellIndex.DeadlySwarm;
                case MM3ItemPropertyIndex.Sanctuaries: return MM3SpellIndex.SuperShelter;
                case MM3ItemPropertyIndex.DragonBreath: return MM3SpellIndex.DragonBreath;
                case MM3ItemPropertyIndex.Feasting: return MM3SpellIndex.CreateFood;
                case MM3ItemPropertyIndex.FieryFlails: return MM3SpellIndex.FieryFlail;
                case MM3ItemPropertyIndex.Recharging: return MM3SpellIndex.RechargeItem;
                case MM3ItemPropertyIndex.Freezing: return MM3SpellIndex.FantasticFreeze;
                case MM3ItemPropertyIndex.Portals: return MM3SpellIndex.TownPortal;
                case MM3ItemPropertyIndex.StoneToFlesh: return MM3SpellIndex.StoneToFlesh;
                case MM3ItemPropertyIndex.Duplication: return MM3SpellIndex.Duplication;
                case MM3ItemPropertyIndex.Disintegration: return MM3SpellIndex.Disintegration;
                case MM3ItemPropertyIndex.HalfForMe: return MM3SpellIndex.HalfForMe;
                case MM3ItemPropertyIndex.RaisingTheDead: return MM3SpellIndex.RaiseDead;
                case MM3ItemPropertyIndex.Etherealization: return MM3SpellIndex.Etherealize;
                case MM3ItemPropertyIndex.DancingSwords: return MM3SpellIndex.DancingSword;
                case MM3ItemPropertyIndex.MoonRays: return MM3SpellIndex.MoonRay;
                case MM3ItemPropertyIndex.MassDistortion: return MM3SpellIndex.MassDistortion;
                case MM3ItemPropertyIndex.PrismaticLight: return MM3SpellIndex.PrismaticLight;
                case MM3ItemPropertyIndex.Enchantment: return MM3SpellIndex.EnchantItem;
                case MM3ItemPropertyIndex.Incinerating: return MM3SpellIndex.Incinerate;
                case MM3ItemPropertyIndex.HolyWords: return MM3SpellIndex.HolyWord;
                case MM3ItemPropertyIndex.Resurrection: return MM3SpellIndex.Resurrection;
                case MM3ItemPropertyIndex.Storms: return MM3SpellIndex.ElementalStorm;
                case MM3ItemPropertyIndex.Megavoltage: return MM3SpellIndex.MegaVolts;
                case MM3ItemPropertyIndex.Infernos: return MM3SpellIndex.Inferno;
                case MM3ItemPropertyIndex.SunRays: return MM3SpellIndex.SunRay;
                case MM3ItemPropertyIndex.Implosions: return MM3SpellIndex.Implosion;
                case MM3ItemPropertyIndex.StarBursts: return MM3SpellIndex.StarBurst;
                case MM3ItemPropertyIndex.TheGods: return MM3SpellIndex.DivineIntervention;
                default: return MM3SpellIndex.None;
            }
        }

        public static MM345UsableFlags GetUsableBy(MM3ItemIndex index)
        {
            switch (index)
            {
                case MM3ItemIndex.LongSword: return MM345UsableFlags.KPATR;
                case MM3ItemIndex.ShortSword: return MM345UsableFlags.KPATR;
                case MM3ItemIndex.BroadSword: return MM345UsableFlags.KPATR;
                case MM3ItemIndex.Scimitar: return MM345UsableFlags.KPATR;
                case MM3ItemIndex.Cutlass: return MM345UsableFlags.KPATR;
                case MM3ItemIndex.Sabre: return MM345UsableFlags.KPATR;
                case MM3ItemIndex.Club: return MM345UsableFlags.AnyClass;
                case MM3ItemIndex.HandAxe: return MM345UsableFlags.KPATNBDR;
                case MM3ItemIndex.Katana: return MM345UsableFlags.KPN;
                case MM3ItemIndex.Nunchakas: return MM345UsableFlags.KPN;
                case MM3ItemIndex.Wakazashi: return MM345UsableFlags.KPN;
                case MM3ItemIndex.Dagger: return MM345UsableFlags.KPASTNBDR;
                case MM3ItemIndex.Mace: return MM345UsableFlags.KPACTNBDR;
                case MM3ItemIndex.Flail: return MM345UsableFlags.KPACTNBDR;
                case MM3ItemIndex.Cudgel: return MM345UsableFlags.KPACTNBDR;
                case MM3ItemIndex.Maul: return MM345UsableFlags.KPACTNBDR;
                case MM3ItemIndex.Spear: return MM345UsableFlags.KPATNBDR;
                case MM3ItemIndex.Bardiche: return MM345UsableFlags.KPATNR;
                case MM3ItemIndex.Glaive: return MM345UsableFlags.KPATNBR;
                case MM3ItemIndex.Halberd: return MM345UsableFlags.KPATNBR;
                case MM3ItemIndex.Pike: return MM345UsableFlags.KPATNBR;
                case MM3ItemIndex.Flamberge: return MM345UsableFlags.KPAR;
                case MM3ItemIndex.Trident: return MM345UsableFlags.KPATNBR;
                case MM3ItemIndex.Staff: return MM345UsableFlags.AnyClass;
                case MM3ItemIndex.Hammer: return MM345UsableFlags.KPACTNBDR;
                case MM3ItemIndex.Naginata: return MM345UsableFlags.KPN;
                case MM3ItemIndex.BattleAxe: return MM345UsableFlags.KPATBR;
                case MM3ItemIndex.GrandAxe: return MM345UsableFlags.KPATBR;
                case MM3ItemIndex.GreatAxe: return MM345UsableFlags.KPATBR;
                case MM3ItemIndex.ShortBow: return MM345UsableFlags.KPATNBR;
                case MM3ItemIndex.LongBow: return MM345UsableFlags.KPATNBR;
                case MM3ItemIndex.Crossbow: return MM345UsableFlags.KPATNBR;
                case MM3ItemIndex.Sling: return MM345UsableFlags.KPATNBR;
                case MM3ItemIndex.PaddedArmor: return MM345UsableFlags.AnyClass;
                case MM3ItemIndex.LeatherArmor: return MM345UsableFlags.KPACTNBDR;
                case MM3ItemIndex.ScaleArmor: return MM345UsableFlags.KPACTNBR;
                case MM3ItemIndex.RingMail: return MM345UsableFlags.KPACTNR;
                case MM3ItemIndex.ChainMail: return MM345UsableFlags.KPACTR;
                case MM3ItemIndex.SplintMail: return MM345UsableFlags.KPCR;
                case MM3ItemIndex.PlateMail: return MM345UsableFlags.KP;
                case MM3ItemIndex.PlateArmor: return MM345UsableFlags.KP;
                case MM3ItemIndex.Shield: return MM345UsableFlags.KPCTBR;
                case MM3ItemIndex.Helm:
                case MM3ItemIndex.Crown:
                case MM3ItemIndex.Tiara:
                case MM3ItemIndex.Gauntlets:
                case MM3ItemIndex.Ring:
                case MM3ItemIndex.Boots:
                case MM3ItemIndex.Cloak:
                case MM3ItemIndex.Robes:
                case MM3ItemIndex.Cape:
                case MM3ItemIndex.Belt:
                case MM3ItemIndex.Broach:
                case MM3ItemIndex.Medal:
                case MM3ItemIndex.Charm:
                case MM3ItemIndex.Cameo:
                case MM3ItemIndex.Scarab:
                case MM3ItemIndex.Pendant:
                case MM3ItemIndex.Necklace:
                case MM3ItemIndex.Amulet:
                case MM3ItemIndex.Rod:
                case MM3ItemIndex.Jewel:
                case MM3ItemIndex.Gem:
                case MM3ItemIndex.Box:
                case MM3ItemIndex.Orb:
                case MM3ItemIndex.Horn:
                case MM3ItemIndex.Coin:
                case MM3ItemIndex.Wand:
                case MM3ItemIndex.Whistle:
                case MM3ItemIndex.Potion:
                case MM3ItemIndex.Scroll: return MM345UsableFlags.AnyClass;
                case MM3ItemIndex.AncientJewelry: return MM345UsableFlags.KPACTNR;
                default: return MM345UsableFlags.None;
            }
        }

        public override string Name { get { return Global.Title(GetItemName((MM3ItemIndex)Index)); } }

        public static string GetItemName(MM3ItemIndex index)
        {
            switch (index)
            {
                case MM3ItemIndex.None: return "";
                case MM3ItemIndex.LongSword: return "long sword";
                case MM3ItemIndex.ShortSword: return "short sword";
                case MM3ItemIndex.BroadSword: return "broad sword";
                case MM3ItemIndex.Scimitar: return "scimitar";
                case MM3ItemIndex.Cutlass: return "cutlass";
                case MM3ItemIndex.Sabre: return "sabre";
                case MM3ItemIndex.Club: return "club";
                case MM3ItemIndex.HandAxe: return "hand axe";
                case MM3ItemIndex.Katana: return "katana";
                case MM3ItemIndex.Nunchakas: return "nunchakas";
                case MM3ItemIndex.Wakazashi: return "wakazashi";
                case MM3ItemIndex.Dagger: return "dagger";
                case MM3ItemIndex.Mace: return "mace";
                case MM3ItemIndex.Flail: return "flail";
                case MM3ItemIndex.Cudgel: return "cudgel";
                case MM3ItemIndex.Maul: return "maul";
                case MM3ItemIndex.Spear: return "spear";
                case MM3ItemIndex.Bardiche: return "bardiche";
                case MM3ItemIndex.Glaive: return "glaive";
                case MM3ItemIndex.Halberd: return "halberd";
                case MM3ItemIndex.Pike: return "pike";
                case MM3ItemIndex.Flamberge: return "flamberge";
                case MM3ItemIndex.Trident: return "trident";
                case MM3ItemIndex.Staff: return "staff";
                case MM3ItemIndex.Hammer: return "hammer";
                case MM3ItemIndex.Naginata: return "naginata";
                case MM3ItemIndex.BattleAxe: return "battle axe";
                case MM3ItemIndex.GrandAxe: return "grand axe";
                case MM3ItemIndex.GreatAxe: return "great axe";
                case MM3ItemIndex.ShortBow: return "short bow";
                case MM3ItemIndex.LongBow: return "long bow";
                case MM3ItemIndex.Crossbow: return "crossbow";
                case MM3ItemIndex.Sling: return "sling";
                case MM3ItemIndex.PaddedArmor: return "padded armor";
                case MM3ItemIndex.LeatherArmor: return "leather armor";
                case MM3ItemIndex.ScaleArmor: return "scale armor";
                case MM3ItemIndex.RingMail: return "ring mail";
                case MM3ItemIndex.ChainMail: return "chain mail";
                case MM3ItemIndex.SplintMail: return "splint mail";
                case MM3ItemIndex.PlateMail: return "plate mail";
                case MM3ItemIndex.PlateArmor: return "plate armor";
                case MM3ItemIndex.Shield: return "shield";
                case MM3ItemIndex.Helm: return "helm";
                case MM3ItemIndex.Crown: return "crown";
                case MM3ItemIndex.Tiara: return "tiara";
                case MM3ItemIndex.Gauntlets: return "gauntlets";
                case MM3ItemIndex.Ring: return "ring";
                case MM3ItemIndex.Boots: return "boots";
                case MM3ItemIndex.Cloak: return "cloak";
                case MM3ItemIndex.Robes: return "robes";
                case MM3ItemIndex.Cape: return "cape";
                case MM3ItemIndex.Belt: return "belt";
                case MM3ItemIndex.Broach: return "broach";
                case MM3ItemIndex.Medal: return "medal";
                case MM3ItemIndex.Charm: return "charm";
                case MM3ItemIndex.Cameo: return "cameo";
                case MM3ItemIndex.Scarab: return "scarab";
                case MM3ItemIndex.Pendant: return "pendant";
                case MM3ItemIndex.Necklace: return "necklace";
                case MM3ItemIndex.Amulet: return "amulet";
                case MM3ItemIndex.Rod: return "rod";
                case MM3ItemIndex.Jewel: return "jewel";
                case MM3ItemIndex.Gem: return "gem";
                case MM3ItemIndex.Box: return "box";
                case MM3ItemIndex.Orb: return "orb";
                case MM3ItemIndex.Horn: return "horn";
                case MM3ItemIndex.Coin: return "coin";
                case MM3ItemIndex.Wand: return "wand";
                case MM3ItemIndex.Whistle: return "whistle";
                case MM3ItemIndex.Potion: return "potion";
                case MM3ItemIndex.Scroll: return "scroll";
                case MM3ItemIndex.Torch: return "Torch";
                case MM3ItemIndex.RopeAndHooks: return "Rope and Hooks";
                case MM3ItemIndex.UselessItem: return "Useless Item";
                case MM3ItemIndex.AncientJewelry: return "Ancient Jewelry";
                case MM3ItemIndex.GreenEyeballKey: return "Green Eyeball Key";
                case MM3ItemIndex.RedWarriorsKey: return "Red Warriors Key";
                case MM3ItemIndex.SacredSilverSkull: return "Sacred Silver Skull";
                case MM3ItemIndex.AncientArtifactofGood: return "Ancient Artifact of Good";
                case MM3ItemIndex.AncientArtifactofNeutrality: return "Ancient Artifact of Neutrality";
                case MM3ItemIndex.AncientArtifactofEvil: return "Ancient Artifact of Evil";
                case MM3ItemIndex.Jewelry: return "Jewelry";
                case MM3ItemIndex.PreciousPearlofYouthandBeauty: return "Precious Pearl of Youth and Beauty";
                case MM3ItemIndex.BlackTerrorKey: return "Black Terror Key";
                case MM3ItemIndex.KingsUltimatePowerOrb: return "King's Ultimate Power Orb";
                case MM3ItemIndex.AncientFizbinofMisfortune: return "Ancient Fizbin of Misfortune";
                case MM3ItemIndex.GoldMasterKey: return "Gold Master Key";
                case MM3ItemIndex.QuatlooCoin: return "Quatloo Coin";
                case MM3ItemIndex.HologramSequencingCard001: return "Hologram Sequencing Card 001";
                case MM3ItemIndex.YellowFortressKey: return "Yellow Fortress Key";
                case MM3ItemIndex.BlueUnholyKey: return "Blue Unholy Key";
                case MM3ItemIndex.HologramSequencingCard002: return "Hologram Sequencing Card 002";
                case MM3ItemIndex.HologramSequencingCard003: return "Hologram Sequencing Card 003";
                case MM3ItemIndex.HologramSequencingCard004: return "Hologram Sequencing Card 004";
                case MM3ItemIndex.HologramSequencingCard005: return "Hologram Sequencing Card 005";
                case MM3ItemIndex.HologramSequencingCard006: return "Hologram Sequencing Card 006";
                case MM3ItemIndex.ZItem23: return "Z Item 23";
                case MM3ItemIndex.BluePriorityPassCard: return "Blue Priority Pass Card";
                case MM3ItemIndex.InterspacialTransportBox: return "Interspacial Transport Box";
                case MM3ItemIndex.MightPotion: return "Might Potion (+5)";
                case MM3ItemIndex.GoldenPyramidKeyCard: return "Golden Pyramid Key Card";
                case MM3ItemIndex.AlacornofIcarus: return "Alacorn of Icarus";
                case MM3ItemIndex.SeaShellofSerenity: return "Sea Shell of Serenity";
                default: return "<unknown>";
            }
        }

        public static string GetItemElementalString(MM3ItemElementalIndex index)
        {
            switch (index)
            {
                case MM3ItemElementalIndex.None: return "";
                case MM3ItemElementalIndex.Burning: return "burning";
                case MM3ItemElementalIndex.Fiery: return "fiery";
                case MM3ItemElementalIndex.Pyric: return "pyric";
                case MM3ItemElementalIndex.Fuming: return "fuming";
                case MM3ItemElementalIndex.Flaming: return "flaming";
                case MM3ItemElementalIndex.Seething: return "seething";
                case MM3ItemElementalIndex.Blazing: return "blazing";
                case MM3ItemElementalIndex.Scorching: return "scorching";
                case MM3ItemElementalIndex.Flickering: return "flickering";
                case MM3ItemElementalIndex.Sparking: return "sparking";
                case MM3ItemElementalIndex.Static: return "static";
                case MM3ItemElementalIndex.Flashing: return "flashing";
                case MM3ItemElementalIndex.Shocking: return "shocking";
                case MM3ItemElementalIndex.Electric: return "electric";
                case MM3ItemElementalIndex.Dyna: return "dyna";
                case MM3ItemElementalIndex.Icy: return "icy";
                case MM3ItemElementalIndex.Frost: return "frost";
                case MM3ItemElementalIndex.Freezing: return "freezing";
                case MM3ItemElementalIndex.Cold: return "cold";
                case MM3ItemElementalIndex.Cryo: return "cryo";
                case MM3ItemElementalIndex.Acidic: return "acidic";
                case MM3ItemElementalIndex.Venemous: return "venemous";
                case MM3ItemElementalIndex.Poisonous: return "poisonous";
                case MM3ItemElementalIndex.Toxic: return "toxic";
                case MM3ItemElementalIndex.Noxious: return "noxious";
                case MM3ItemElementalIndex.Glowing: return "glowing";
                case MM3ItemElementalIndex.Incandescent: return "incandescent";
                case MM3ItemElementalIndex.Dense: return "dense";
                case MM3ItemElementalIndex.Sonic: return "sonic";
                case MM3ItemElementalIndex.Power: return "power";
                case MM3ItemElementalIndex.Thermal: return "thermal";
                case MM3ItemElementalIndex.Radiating: return "radiating";
                case MM3ItemElementalIndex.Kinetic: return "kinetic";
                case MM3ItemElementalIndex.Mystic: return "mystic";
                case MM3ItemElementalIndex.Magical: return "magical";
                case MM3ItemElementalIndex.Ectoplasmic: return "ectoplasmic";
                default: return "<unknown>";
            }
        }

        public static string GetItemMaterialString(MM3ItemMaterialIndex index)
        {
            switch (index)
            {
                case MM3ItemMaterialIndex.None: return "";
                case MM3ItemMaterialIndex.Wooden: return "wooden";
                case MM3ItemMaterialIndex.Leather: return "leather";
                case MM3ItemMaterialIndex.Brass: return "brass";
                case MM3ItemMaterialIndex.Bronze: return "bronze";
                case MM3ItemMaterialIndex.Iron: return "iron";
                case MM3ItemMaterialIndex.Silver: return "silver";
                case MM3ItemMaterialIndex.Steel: return "steel";
                case MM3ItemMaterialIndex.Gold: return "gold";
                case MM3ItemMaterialIndex.Platinum: return "platinum";
                case MM3ItemMaterialIndex.Glass: return "glass";
                case MM3ItemMaterialIndex.Coral: return "coral";
                case MM3ItemMaterialIndex.Crystal: return "crystal";
                case MM3ItemMaterialIndex.Lapis: return "lapis";
                case MM3ItemMaterialIndex.Pearl: return "pearl";
                case MM3ItemMaterialIndex.Amber: return "amber";
                case MM3ItemMaterialIndex.Ebony: return "ebony";
                case MM3ItemMaterialIndex.Quartz: return "quartz";
                case MM3ItemMaterialIndex.Ruby: return "ruby";
                case MM3ItemMaterialIndex.Emerald: return "emerald";
                case MM3ItemMaterialIndex.Sapphire: return "sapphire";
                case MM3ItemMaterialIndex.Diamond: return "diamond";
                case MM3ItemMaterialIndex.Obsidian: return "obsidian";
                default: return "<unknown>";
            }
        }

        public static string GetItemAttributeString(MM3ItemAttributeIndex index)
        {
            switch (index)
            {
                case MM3ItemAttributeIndex.None: return "";
                case MM3ItemAttributeIndex.Might: return "might";
                case MM3ItemAttributeIndex.Strength: return "strength";
                case MM3ItemAttributeIndex.Warrior: return "warrior";
                case MM3ItemAttributeIndex.Ogre: return "ogre";
                case MM3ItemAttributeIndex.Giant: return "giant";
                case MM3ItemAttributeIndex.Thunder: return "thunder";
                case MM3ItemAttributeIndex.Force: return "force";
                case MM3ItemAttributeIndex.Power: return "power";
                case MM3ItemAttributeIndex.Dragon: return "dragon";
                case MM3ItemAttributeIndex.Photon: return "photon";
                case MM3ItemAttributeIndex.Clever: return "clever";
                case MM3ItemAttributeIndex.Mind: return "mind";
                case MM3ItemAttributeIndex.Sage: return "sage";
                case MM3ItemAttributeIndex.Thought: return "thought";
                case MM3ItemAttributeIndex.Knowledge: return "knowledge";
                case MM3ItemAttributeIndex.Intellect: return "intellect";
                case MM3ItemAttributeIndex.Wisdom: return "wisdom";
                case MM3ItemAttributeIndex.Genius: return "genius";
                case MM3ItemAttributeIndex.Buddy: return "buddy";
                case MM3ItemAttributeIndex.Friendship: return "friendship";
                case MM3ItemAttributeIndex.Charm: return "charm";
                case MM3ItemAttributeIndex.Personality: return "personality";
                case MM3ItemAttributeIndex.Charisma: return "charisma";
                case MM3ItemAttributeIndex.Leadership: return "leadership";
                case MM3ItemAttributeIndex.Ego: return "ego";
                case MM3ItemAttributeIndex.Holy: return "holy";
                case MM3ItemAttributeIndex.Quick: return "quick";
                case MM3ItemAttributeIndex.Swift: return "swift";
                case MM3ItemAttributeIndex.Fast: return "fast";
                case MM3ItemAttributeIndex.Rapid: return "rapid";
                case MM3ItemAttributeIndex.Speed: return "speed";
                case MM3ItemAttributeIndex.Wind: return "wind";
                case MM3ItemAttributeIndex.Accelerator: return "accelerator";
                case MM3ItemAttributeIndex.Velocity: return "velocity";
                case MM3ItemAttributeIndex.Sharp: return "sharp";
                case MM3ItemAttributeIndex.Accurate: return "accurate";
                case MM3ItemAttributeIndex.Marksman: return "marksman";
                case MM3ItemAttributeIndex.Precision: return "precision";
                case MM3ItemAttributeIndex.True: return "true";
                case MM3ItemAttributeIndex.Exacto: return "exacto";
                case MM3ItemAttributeIndex.Clover: return "clover";
                case MM3ItemAttributeIndex.Chance: return "chance";
                case MM3ItemAttributeIndex.Winners: return "winners";
                case MM3ItemAttributeIndex.Lucky: return "lucky";
                case MM3ItemAttributeIndex.Gamblers: return "gamblers";
                case MM3ItemAttributeIndex.Leprechauns: return "leprechauns";
                case MM3ItemAttributeIndex.Vigor: return "vigor";
                case MM3ItemAttributeIndex.Health: return "health";
                case MM3ItemAttributeIndex.Life: return "life";
                case MM3ItemAttributeIndex.Troll: return "troll";
                case MM3ItemAttributeIndex.Vampiric: return "vampiric";
                case MM3ItemAttributeIndex.Spell: return "spell";
                case MM3ItemAttributeIndex.Castors: return "castors";
                case MM3ItemAttributeIndex.Witch: return "witch";
                case MM3ItemAttributeIndex.Mage: return "mage";
                case MM3ItemAttributeIndex.Archmage: return "archmage";
                case MM3ItemAttributeIndex.Arcane: return "arcane";
                case MM3ItemAttributeIndex.Protection: return "protection";
                case MM3ItemAttributeIndex.Armored: return "armored";
                case MM3ItemAttributeIndex.Defender: return "defender";
                case MM3ItemAttributeIndex.Stealth: return "stealth";
                case MM3ItemAttributeIndex.Divine: return "divine";
                case MM3ItemAttributeIndex.Mugger: return "mugger";
                case MM3ItemAttributeIndex.Burgler: return "burgler";
                case MM3ItemAttributeIndex.Looter: return "looter";
                case MM3ItemAttributeIndex.Brigand: return "brigand";
                case MM3ItemAttributeIndex.Filch: return "filch";
                case MM3ItemAttributeIndex.Thief: return "thief";
                case MM3ItemAttributeIndex.Rogue: return "rogue";
                case MM3ItemAttributeIndex.Plunder: return "plunder";
                case MM3ItemAttributeIndex.Criminal: return "criminal";
                case MM3ItemAttributeIndex.Pirate: return "pirate";
                default: return "<unknown>";
            }
        }

        public static string GetItemPropertyString(MM3ItemPropertyIndex index)
        {
            switch (index)
            {
                case MM3ItemPropertyIndex.None: return String.Empty;
                case MM3ItemPropertyIndex.Light: return "light";
                case MM3ItemPropertyIndex.Awakening: return "awakening";
                case MM3ItemPropertyIndex.MagicDetection: return "magic detection";
                case MM3ItemPropertyIndex.Arrows: return "arrows";
                case MM3ItemPropertyIndex.Aid: return "aid";
                case MM3ItemPropertyIndex.Fists: return "fists";
                case MM3ItemPropertyIndex.EnergyBlasts: return "energy blasts";
                case MM3ItemPropertyIndex.Sleeping: return "sleeping";
                case MM3ItemPropertyIndex.Revitalization: return "revitalization";
                case MM3ItemPropertyIndex.Curing: return "curing";
                case MM3ItemPropertyIndex.Sparking: return "sparking";
                case MM3ItemPropertyIndex.Ropes: return "ropes";
                case MM3ItemPropertyIndex.ToxicClouds: return "toxic clouds";
                case MM3ItemPropertyIndex.Elements: return "elements";
                case MM3ItemPropertyIndex.Pain: return "pain";
                case MM3ItemPropertyIndex.Jumping: return "jumping";
                case MM3ItemPropertyIndex.AcidStreams: return "acid streams";
                case MM3ItemPropertyIndex.UndeadTurning: return "undead turning";
                case MM3ItemPropertyIndex.Levitation: return "levitation";
                case MM3ItemPropertyIndex.WizardEyes: return "wizard eyes";
                case MM3ItemPropertyIndex.Silence: return "silence";
                case MM3ItemPropertyIndex.Blessing: return "blessing";
                case MM3ItemPropertyIndex.Identification: return "identification";
                case MM3ItemPropertyIndex.Lightning: return "lightning";
                case MM3ItemPropertyIndex.HolyBonuses: return "holy bonuses";
                case MM3ItemPropertyIndex.PowerCuring: return "power curing";
                case MM3ItemPropertyIndex.Nature: return "nature";
                case MM3ItemPropertyIndex.Beacons: return "beacons";
                case MM3ItemPropertyIndex.Shielding: return "shielding";
                case MM3ItemPropertyIndex.Heroism: return "heroism";
                case MM3ItemPropertyIndex.Immobilization: return "immobilization";
                case MM3ItemPropertyIndex.WaterWalking: return "water walking";
                case MM3ItemPropertyIndex.FrostBiting: return "frost biting";
                case MM3ItemPropertyIndex.MonsterFinding: return "monster finding";
                case MM3ItemPropertyIndex.Fireballs: return "fireballs";
                case MM3ItemPropertyIndex.ColdRays: return "cold rays";
                case MM3ItemPropertyIndex.Antidotes: return "antidotes";
                case MM3ItemPropertyIndex.AcidSpraying: return "acid spraying";
                case MM3ItemPropertyIndex.Distortion: return "distortion";
                case MM3ItemPropertyIndex.FeebleMinding: return "feeble minding";
                case MM3ItemPropertyIndex.Vaccination: return "vaccination";
                case MM3ItemPropertyIndex.Gating: return "gating";
                case MM3ItemPropertyIndex.Teleportation: return "teleportation";
                case MM3ItemPropertyIndex.Death: return "death";
                case MM3ItemPropertyIndex.FreeMovement: return "free movement";
                case MM3ItemPropertyIndex.Paralyzing: return "paralyzing";
                case MM3ItemPropertyIndex.DeadlySwarms: return "deadly swarms";
                case MM3ItemPropertyIndex.Sanctuaries: return "sanctuaries";
                case MM3ItemPropertyIndex.DragonBreath: return "dragon breath";
                case MM3ItemPropertyIndex.Feasting: return "feasting";
                case MM3ItemPropertyIndex.FieryFlails: return "fiery flails";
                case MM3ItemPropertyIndex.Recharging: return "recharging";
                case MM3ItemPropertyIndex.Freezing: return "freezing";
                case MM3ItemPropertyIndex.Portals: return "portals";
                case MM3ItemPropertyIndex.StoneToFlesh: return "stone to flesh";
                case MM3ItemPropertyIndex.Duplication: return "duplication";
                case MM3ItemPropertyIndex.Disintegration: return "disintegration";
                case MM3ItemPropertyIndex.HalfForMe: return "half for me";
                case MM3ItemPropertyIndex.RaisingTheDead: return "raising the dead";
                case MM3ItemPropertyIndex.Etherealization: return "etherealization";
                case MM3ItemPropertyIndex.DancingSwords: return "dancing swords";
                case MM3ItemPropertyIndex.MoonRays: return "moon rays";
                case MM3ItemPropertyIndex.MassDistortion: return "mass distortion";
                case MM3ItemPropertyIndex.PrismaticLight: return "prismatic light";
                case MM3ItemPropertyIndex.Enchantment: return "enchantment";
                case MM3ItemPropertyIndex.Incinerating: return "incinerating";
                case MM3ItemPropertyIndex.HolyWords: return "holy words";
                case MM3ItemPropertyIndex.Resurrection: return "resurrection";
                case MM3ItemPropertyIndex.Storms: return "storms";
                case MM3ItemPropertyIndex.Megavoltage: return "megavoltage";
                case MM3ItemPropertyIndex.Infernos: return "infernos";
                case MM3ItemPropertyIndex.SunRays: return "sun rays";
                case MM3ItemPropertyIndex.Implosions: return "implosions";
                case MM3ItemPropertyIndex.StarBursts: return "star bursts";
                case MM3ItemPropertyIndex.TheGods: return "the GODS!";
                default: return "<unknown>";
            }
        }

        public override string AttributeString
        {
            get
            {
                if (Attribute == MM3ItemAttributeIndex.None)
                    return String.Empty;
                return Global.GenericAttributeString(AttributeEffect(Attribute).Attribute);
            }
        }

        public override int EquipBonusValue
        {
            get
            {
                if (Attribute == MM3ItemAttributeIndex.None)
                    return 0;
                return AttributeEffect(Attribute).Modifier;
            }
        }

        public override string UseEffectString
        {
            get
            {
                if (Property == MM3ItemPropertyIndex.None)
                    return String.Empty;

                return Global.Title(MM3SpellList.GetSpellName(MM3Item.ItemPropertyEffect(Property)));
            }
        }

        public override string MaterialString { get { return Global.Title(GetItemMaterialString(Material)); } }

        public static string GetElementString(MM3ItemElementalIndex index)
        {
            switch (index)
            {
                case MM3ItemElementalIndex.Burning:
                case MM3ItemElementalIndex.Fiery:
                case MM3ItemElementalIndex.Pyric:
                case MM3ItemElementalIndex.Fuming:
                case MM3ItemElementalIndex.Flaming:
                case MM3ItemElementalIndex.Seething:
                case MM3ItemElementalIndex.Blazing:
                case MM3ItemElementalIndex.Scorching: return "Fire";
                case MM3ItemElementalIndex.Flickering:
                case MM3ItemElementalIndex.Sparking:
                case MM3ItemElementalIndex.Static:
                case MM3ItemElementalIndex.Flashing:
                case MM3ItemElementalIndex.Shocking:
                case MM3ItemElementalIndex.Electric:
                case MM3ItemElementalIndex.Dyna: return "Electric";
                case MM3ItemElementalIndex.Icy:
                case MM3ItemElementalIndex.Frost:
                case MM3ItemElementalIndex.Freezing:
                case MM3ItemElementalIndex.Cold:
                case MM3ItemElementalIndex.Cryo: return "Cold";
                case MM3ItemElementalIndex.Acidic:
                case MM3ItemElementalIndex.Venemous:
                case MM3ItemElementalIndex.Poisonous:
                case MM3ItemElementalIndex.Toxic:
                case MM3ItemElementalIndex.Noxious: return "Poison";
                case MM3ItemElementalIndex.Glowing:
                case MM3ItemElementalIndex.Incandescent:
                case MM3ItemElementalIndex.Dense:
                case MM3ItemElementalIndex.Sonic:
                case MM3ItemElementalIndex.Power:
                case MM3ItemElementalIndex.Thermal:
                case MM3ItemElementalIndex.Radiating:
                case MM3ItemElementalIndex.Kinetic: return "Energy";
                case MM3ItemElementalIndex.Mystic:
                case MM3ItemElementalIndex.Magical:
                case MM3ItemElementalIndex.Ectoplasmic: return "Magic";
                default: return String.Empty;
            }
        }

        public override string ElementString { get { return MM3Item.GetElementString(Element); } }
        public override string PropertyString { get { return MM3Item.GetItemPropertyString(Property); } }

        private void GetLargestBonusInfo(out string effect, out int value)
        {
            effect = String.Empty;
            value = 0;

            int iElemental = 0;
            int iAttribute = 0;
            int iMaterial = 0;

            switch (Type)
            {
                case ItemType.Weapon:
                case ItemType.OneHandMelee:
                case ItemType.TwoHandMelee:
                case ItemType.Missile:
                    iElemental = ElementalEffect(Element).DamageValue;
                    iAttribute = AttributeEffect(Attribute).Modifier;
                    iMaterial = MaterialEffect(Material).Damage;
                    break;
                case ItemType.Armor:
                case ItemType.Accessory:
                    iElemental = ElementalEffect(Element).ResistValue;
                    iAttribute = AttributeEffect(Attribute).Modifier;
                    iMaterial = MaterialEffect(Material).AC;
                    break;
                default:
                    return;
            }

            if (iElemental > iAttribute && iElemental > iMaterial)
            {
                value = iElemental;
                effect = IsWeapon ? Global.SingleResistance(ElementalEffect(Element).DamageElement) : 
                    Global.SingleResistance(ElementalEffect(Element).ResistElement) + " Res";
            } else if (iAttribute > iMaterial)
            {
                value = iAttribute;
                effect = Global.GenericAttributeString(AttributeEffect(Attribute).Attribute);
            }
            else
            {
                value = iMaterial;
                effect = IsWeapon ? "Damage" : "Armor Class";
            }
        }

        public override string LargestBonusEffect
        {
            get
            {
                string strEffect = String.Empty;
                int iValue = 0;
                GetLargestBonusInfo(out strEffect, out iValue);
                return strEffect;
            }
        }

        public override int LargestBonus
        {
            get
            {
                string strEffect = String.Empty;
                int iValue = 0;
                GetLargestBonusInfo(out strEffect, out iValue);
                return iValue;
            }
        }
    }

    public class MM3Shops : Shops
    {
        public MM3ShopInventory FountainHead { get { return Inventories[0] as MM3ShopInventory; } }
        public MM3ShopInventory BayWatch { get { return Inventories[1] as MM3ShopInventory; } }
        public MM3ShopInventory Wildabar { get { return Inventories[2] as MM3ShopInventory; } }
        public MM3ShopInventory SwampTown { get { return Inventories[3] as MM3ShopInventory; } }
        public MM3ShopInventory BlisteringHeights { get { return Inventories[4] as MM3ShopInventory; } }

        public static string[] TownNames = new string[] { "Fountain Head", "Bay Watch", "Wildabar", "Swamp Town", "Blistering Heights" };

        public MM3Shops(long offset, byte[] bytes, long offsetCurrent, byte[] bytesCurrent)
        {
            RawBytes = bytes;
            // The order of the bytes is
            // Town 1 Charges (weapon, armor, misc)
            // ...
            // Town 5 Charges (weapon, armor, misc)
            // Town 1 Elements (weapon, armor, misc)
            // ...
            // Town 5 Elements (weapon, armor, misc)
            // [Materials]
            // [Attributes]
            // [Bases]
            // [Properties]

            Inventories = new List<ShopInventory>(5);
            for(int i = 0; i < 5; i++)
                Inventories.Add(new MM3ShopInventory(offset + 27*i, 135, 
                Global.Subset(bytes, 135*0 + i*27, 27),
                Global.Subset(bytes, 135*1 + i*27, 27),
                Global.Subset(bytes, 135*2 + i*27, 27),
                Global.Subset(bytes, 135*3 + i*27, 27),
                Global.Subset(bytes, 135*4 + i*27, 27),
                Global.Subset(bytes, 135*5 + i*27, 27),
                TownNames[i]
                ));

            CurrentDisplay = new List<ShopItem>(9);
            if (bytesCurrent != null)
            {
                MemoryStream ms = new MemoryStream(bytes);
                ms.Write(bytesCurrent, 0, bytesCurrent.Length);
                RawBytes = ms.ToArray();

                for (int i = 0; i < 18; i++)
                {
                    if (bytesCurrent[19*4+i] != 0)
                        CurrentDisplay.Add(new ShopItem(MM3Item.Create(0, 
                            bytesCurrent[19*0 + i], 
                            bytesCurrent[19*1 + i],
                            bytesCurrent[19*2 + i],
                            bytesCurrent[19*3 + i],
                            bytesCurrent[19*4 + i],
                            bytesCurrent[19*5 + i]
                            ), offsetCurrent + i, 19));
                }
            }
        }
    }

    public class MM3ShopInventory : ShopInventory
    {
        public List<ShopItem> Weapons;
        public List<ShopItem> Armor;
        public List<ShopItem> Misc;

        public override IEnumerable<ShopItem> AllItems
        {
            get { return Weapons.Concat(Armor).Concat(Misc); }
        }

        public MM3ShopInventory(long offset, int multiplier, byte[] charges, byte[] elements, byte[] materials, byte[] attributes, byte[] bases, byte[] properties, string strTown)
        {
            Town = strTown;
            Weapons = new List<ShopItem>(9);
            Armor = new List<ShopItem>(9);
            Misc = new List<ShopItem>(9);
            for(int i = 0; i < 9; i++)
            {
                if (bases[i] != 0)
                    Weapons.Add(new ShopItem(
                        MM3Item.Create(0, charges[i], elements[i], materials[i], attributes[i], bases[i], properties[i]),
                        offset + i, multiplier));
                if (bases[i+9] != 0)
                    Armor.Add(new ShopItem(
                        MM3Item.Create(0, charges[i + 9], elements[i + 9], materials[i + 9], attributes[i + 9], bases[i + 9], properties[i + 9]),
                        offset + i + 9, multiplier));
                if (bases[i + 18] != 0)
                    Misc.Add(new ShopItem(
                        MM3Item.Create(0, charges[i + 18], elements[i + 18], materials[i + 18], attributes[i + 18], bases[i + 18], properties[i + 18]),
                        offset + i + 18, multiplier));
            }
        }
    }
}
