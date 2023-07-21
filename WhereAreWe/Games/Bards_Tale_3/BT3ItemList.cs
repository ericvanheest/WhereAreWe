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
    public enum BT3ItemFlags
    {
        None = 0x00,
        Equipped = 0x01,
        Unequippable = 0x02,
        FilledWater = 0x00,
        FilledSpirits = 0x04,
        FilledWaterOfLife = 0x08,
        FilledDragonBlood = 0x0C,
        FilledMoltenTar = 0x10,
        Filled = 0x1C,  // Filled with is a 3-bit value
        FailedIdentify = 0x40,
        Unidentified = 0x80,
    }

    public enum BT3ItemIndex
    {
        None = 0,
        Torch = 1,
        Lamp = 2,
        Broadsword = 3,
        ShortSword = 4,
        Dagger = 5,
        WarAxe = 6,
        Halbard = 7,
        LongBow = 8,
        Staff = 9,
        Buckler = 10,
        TowerShield = 11,
        LeatherArmor = 12,
        ChainMail = 13,
        ScaleArmor = 14,
        PlateArmor = 15,
        Robes = 16,
        Helm = 17,
        LeatherGloves = 18,
        Gauntlets = 19,
        Mandolin = 20,
        Spear = 21,
        Arrows = 22,
        MithrilSword = 23,
        MithrilShield = 24,
        MithrilChain = 25,
        MithrilScale = 26,
        GiantFgn = 27,
        MithrilBracers = 28,
        Bardsword = 29,
        FireHorn = 30,
        Litewand = 31,
        MithrilDagger = 32,
        MithrilHelm = 33,
        MithrilGloves = 34,
        MithrilAxe = 35,
        Shuriken = 36,
        MithrilPlate = 37,
        MoltenFgn = 38,
        SpellSpear = 39,
        ShieldRing = 40,
        FinsFlute = 41,
        KaelsAxe = 42,
        MithrilArrows = 43,
        Dayblade = 44,
        ShieldStaff = 45,
        ElfCloak = 46,
        Hawkblade = 47,
        AdamantSword = 48,
        AdamantShield = 49,
        AdamantHelm = 50,
        AdamantGloves = 51,
        Pureblade = 52,
        Boomerang = 53,
        AlisCarpet = 54,
        Luckshield = 55,
        DozerFgn = 56,
        AdamantChain = 57,
        DeathStars = 58,
        AdamantPlate = 59,
        AdamantBracers = 60,
        SlayerFgn = 61,
        PureShield = 62,
        MageStaff = 63,
        WarStaff = 64,
        ThiefDagger = 65,
        SoulMace = 66,
        Ogrewand = 67,
        Katosbracer = 68,
        Sorcerstaff = 69,
        GaltsFlute = 70,
        FrostHorn = 71,
        AgsArrows = 72,
        DiamondShield = 73,
        BardBow = 74,
        DiamondHelm = 75,
        ElfBoots = 76,
        VanquisherFgn = 77,
        Conjurstaff = 78,
        StaffOfLor = 79,
        FlameSword = 80,
        Powerstaff = 81,
        BreathRing = 82,
        Dragonshield = 83,
        DiamondPlate = 84,
        Wargloves = 85,
        Wizhelm = 86,
        Dragonwand = 87,
        Deathring = 88,
        CrystalSword = 89,
        Speedboots = 90,
        FlameHorn = 91,
        ZenArrows = 92,
        Deathdrum = 93,
        PipesOfPan = 94,
        PowerRing = 95,
        SongAxe = 96,
        TrickBrick = 97,
        DragonFgn = 98,
        MageFgn = 99,
        TrollRing = 100,
        AramsKnife = 101,
        AngrasEye = 102,
        HerbFgn = 103,
        MasterWand = 104,
        BrothersFgn = 105,
        Dynamite = 106,
        ThorsHammer = 107,
        Stoneblade = 108,
        HolyHandgrenade = 109,
        Masterkey = 110,
        NospinRing = 111,
        CrystalLens = 112,
        SmokeyLens = 113,
        BlackLens = 114,
        SphereOfLanatir = 115,
        WandOfPower = 116,
        Acorn = 117,
        Wineskin = 118,
        Nightspear = 119,
        TslothasHead = 120,
        TslothasHeart = 121,
        Arefolia = 122,
        ValariansBow = 123,
        ArrowsOfLife = 124,
        Canteen = 125,
        TitanPlate = 126,
        TitanShield = 127,
        TitanHelm = 128,
        FireSpear = 129,
        WillowFlute = 130,
        Firebrand = 131,
        HolySword = 132,
        WandOfFury = 133,
        Lightstar = 134,
        CrownOfTruth = 135,
        BeltOfAlliria = 136,
        CrystalKey = 137,
        TaoRing = 138,
        StealthArrows = 139,
        YellowStaff = 140,
        SteadyEye = 141,
        DivineHalbard = 142,
        Incense = 143,
        IChing = 144,
        WhiteRose = 145,
        BlueRose = 146,
        RedRose = 147,
        YellowRose = 148,
        RainbowRose = 149,
        MagicTriangle = 150,
        Item151 = 151,
        HammerOfWrath = 152,
        FerofistsHelm = 153,
        Item154 = 154,
        Item155 = 155,
        HelmOfJustice = 156,
        SceadusCloak = 157,
        Shadelance = 158,
        BlackArrows = 159,
        WerrasShield = 160,
        Strifespear = 161,
        Sheetmusic = 162,
        RightKey = 163,
        LeftKey = 164,
        Lever = 165,
        Nut = 166,
        Bolt = 167,
        Spanner = 168,
        ShadowLock = 169,
        ShadowDoor = 170,
        Misericorde = 171,
        HolyAvenger = 172,
        Shadowshiv = 173,
        KalisGarrote = 174,
        FlameKnife = 175,
        RedsStiletto = 176,
        Heartseeker = 177,
        Item178 = 178,
        Item179 = 179,
        Item180 = 180,
        DiamondScale = 181,
        HolyTNT = 182,
        EternalTorch = 183,
        OsconsStaff = 184,
        AngelsRing = 185,
        Deathhorn = 186,
        StaffOfMangar = 187,
        TeslaRing = 188,
        DiamondBracers = 189,
        DeathFgn = 190,
        ThunderSword = 191,
        PoisonDagger = 192,
        SparkBlade = 193,
        GalvanicOboe = 194,
        HarmonicGem = 195,
        TungstenShield = 196,
        TungstenPlate = 197,
        MinstrelsGlove = 198,
        HuntersCloak = 199,
        DeathHammer = 200,
        BloodMeshRobe = 201,
        SoothingBalm = 202,
        MagesCloak = 203,
        FamiliarFgn = 204,
        Hourglass = 205,
        ThievesHood = 206,
        SurehandAmulet = 207,
        ThievesDart = 208,
        ShrillFlute = 209,
        AngelsHarp = 210,
        TheBook = 211,
        TrothLance = 212,
        DiamondSuit = 213,
        DiamondFlail = 214,
        PurpleHeart = 215,
        TitanBracers = 216,
        EelskinTunic = 217,
        SorcerersHood = 218,
        DiamondStaff = 219,
        CrystalGem = 220,
        WandOfForce = 221,
        CliLyre = 222,
        YouthPotion = 223,
        Item224 = 224,
        Item225 = 225,
        Item226 = 226,
        Item227 = 227,
        Item228 = 228,
        Item229 = 229,
        Item230 = 230,
        Item231 = 231,
        Item232 = 232,
        Item233 = 233,
        Item234 = 234,
        Item235 = 235,
        Item236 = 236,
        Item237 = 237,
        Item238 = 238,
        Item239 = 239,
        MithrilSuit = 240,
        TitanSuit = 241,
        MagesGlove = 242,
        FlareCrystal = 243,
        HolyMissile = 244,
        GodsBlade = 245,
        HunterBlade = 246,
        StaffOfGods = 247,
        HornOfGods = 248,
        Last
    }

    public enum BT3ItemEffect
    {
        None = 0,
        ShortLight = 126,
        MediumLight = 127,
        Eat = 128,
        Drink = 129,
        Quest = 130,
        Throw = 131,
        Summon = 132,
        BreathDamage = 133,
        RestoreAllSP = 134,
        Last
    }

    public class BT3Item : BTItem
    {
        public int Charges;
        public BT3ItemEffect ItemEffect;
        public BT3SpellIndex Spell;
        public BT2EquipEffect EquipEffect;
        public BT3ItemFlags Contains;
        public bool FailedIdentify;

        public override int SpellIndex { get { return 0; } }
        public override int EffectIndex { get { return 0; } }
        public override int ChargesCurrent { get { return Charges; } set { Charges = value; } }

        public BT3Item(int index, byte[] bytes, int offset = 0)
        {
            SetBT3Bytes(index, bytes, offset);
        }

        public override GameNames Game { get { return GameNames.BardsTale3; } }
        public override int Index { get { return m_index; } set { m_index = value; } }
        public override BTItem CreateItem(int index, byte[] bytes, int offset = 0) { return new BT3Item(index, bytes, offset); }
        public BT3ItemIndex ItemIndex { get { return (BT3ItemIndex)m_index; } set { m_index = (int)value; } }
        public override bool ChargeBased { get { return (ChargesCurrent > 0 && ChargesCurrent < 255) || BTType == BTItemType.Container; } }
        public override int MaxCharges { get { return 254; } }   // 255 means "infinite" which isn't appropriate for, e.g. stacking
        public override string TrashIndex { get { return String.Format("{0:X2}{1:X1}", Index, (int) (Contains & BT3ItemFlags.Filled) >> 2); } }

        public override Item Clone()
        {
            return new BT3Item((int)ItemIndex, GetBytes());
        }

        public static BT3Item FromBT3InventoryBytes(byte[] bytes, int offset = 0)
        {
            int iItem = bytes[offset + 1];

            BT3Item item = Global.BT3.GetClonedItem(iItem) as BT3Item;
            if (item == null)
                return item;
            BT3ItemFlags flags = (BT3ItemFlags) bytes[offset];
            item.Identified = !flags.HasFlag(BT3ItemFlags.Unidentified);
            item.FailedIdentify = flags.HasFlag(BT3ItemFlags.FailedIdentify);
            item.WhereEquipped = EquipLocation.None;
            item.ChargesCurrent = bytes[offset + 2];
            if (flags.HasFlag(BT3ItemFlags.Equipped))
                item.WhereEquipped = item.CanEquipLocation;
            item.Contains = flags & BT3ItemFlags.Filled;
            return item;
        }

        public override void SetBytes(int index, byte[] bytes, int offset = 0) { SetBT3Bytes(index, bytes, offset); }

        public void SetBT3Bytes(int index, byte[] bytes, int offset = 0)
        {
            Contains = BT3ItemFlags.FilledWater;
            ItemIndex = (BT3ItemIndex)index;
            m_strName = GetName((BT3ItemIndex) index);
            BTType = (BTItemType)(bytes[1] & 0x0f);
            WeaponEffect = (BTAttackEffect)((bytes[1] & 0xf0) >> 4);
            int iFaceIndex = (bytes[4] & 0xf0) >> 5;
            if (!IsWeapon || BTType == BTItemType.Ranged || BTType == BTItemType.Quiver)
                Damage = DamageDice.Zero;
            else
                Damage = new DamageDice(1 << (iFaceIndex+1), (bytes[4] & 0x01f) + 1, (bytes[0] & 0xf0) >> 4);
            MissileDamage = GetMissileDamage(ItemIndex);
            Value = ((bytes[5] & 0xf8) >> 3) * (int)(Math.Pow(10, bytes[5] & 0x07));
            ItemEffect = GetItemEffect(bytes[3]);
            Spell = GetItemSpell(bytes[3]);
            Usable = GetUsableBy(bytes[2], Game);
            AC = (bytes[0] & 0x0f);
            Identified = true;
            FailedIdentify = false;
            if (bytes.Length > 6)
                Charges = bytes[6];
            else
                Charges = 255;
            if (bytes.Length > 7)
                EquipEffect = (BT2EquipEffect)bytes[7];
            else
                EquipEffect = BT2EquipEffect.None;
            if (bytes.Length > 8)
                Contains = (BT3ItemFlags)bytes[8];
            WhereEquipped = EquipLocation.None;
        }

        public override string DamageStringFull
        {
            get
            {
                if (BTType == BTItemType.Quiver)
                    return MissileDamage.StringWithAverage;
                return Damage.StringWithAverage;
            }
        }

        public static DamageDice GetMissileDamage(BT3ItemIndex item)
        {
            switch (item)
            {
                case BT3ItemIndex.WarAxe: return new DamageDice(8, 2, 6); // 20' 8-22 2d8+6
                case BT3ItemIndex.Spear: return new DamageDice(6, 3, 5); // 20' 8-23 3d6+5
                case BT3ItemIndex.MithrilAxe: return new DamageDice(10, 2, 7); // Axe 30' 9-26 2d10+7
                case BT3ItemIndex.Shuriken: return new DamageDice(4, 4, 0); // 30' 4-16 4d4
                case BT3ItemIndex.SpellSpear: return new DamageDice(11, 2, 8); // 40' 10-30 2d11+8
                case BT3ItemIndex.KaelsAxe: return new DamageDice(8, 11, 9); // 40' 38-82 11d8+9
                case BT3ItemIndex.Boomerang: return new DamageDice(8, 4, 12); // 40' 16-44 4d8+12
                case BT3ItemIndex.DeathStars: return new DamageDice(8, 10, 16); // 60' 38-82 10d8+16
                case BT3ItemIndex.SongAxe: return new DamageDice(8, 22, 64); // 80' 118-202 22d8+64
                case BT3ItemIndex.AramsKnife: return new DamageDice(8, 44, 128); // 90' 260-390 44d8+128
                case BT3ItemIndex.ThorsHammer: return new DamageDice(8, 11, 32); // 70' 54-106 11d8+32
                case BT3ItemIndex.Nightspear: return new DamageDice(8, 6, 32); // 70' 38-80 6d8+32
                case BT3ItemIndex.FireSpear: return new DamageDice(8, 16, 50); // 60' 93-155 16d8+50
                case BT3ItemIndex.Lightstar: return new DamageDice(4, 30, 50); // 60' 95-154 30d4+50
                case BT3ItemIndex.HolyMissile: return new DamageDice(16, 45, 256); // 80' 550-747 45d16+256
                case BT3ItemIndex.Arrows: return new DamageDice(4, 3, 0); // 30' 3-12 3d4
                case BT3ItemIndex.MithrilArrows: return new DamageDice(9, 3, 8); // 50' 11-34 3d9+8
                case BT3ItemIndex.AgsArrows: return new DamageDice(6, 10, 24); // 90' 39-80 10d6+24
                case BT3ItemIndex.ZenArrows: return new DamageDice(8, 13, 40); // 70' 72-125 13d8+40
                case BT3ItemIndex.ArrowsOfLife: return new DamageDice(16, 35, 200); // 50' 409-589 35d16+200
                case BT3ItemIndex.StealthArrows: return new DamageDice(16, 36, 200); // 60' 411-599 36d16+200
                case BT3ItemIndex.BlackArrows: return new DamageDice(16, 36, 200); // 90' 411-602 36d16+200
                default: return DamageDice.Zero;
            }
        }

        public override bool PassiveEffect
        {
            get
            {
                if (Spell != BT3SpellIndex.None)
                    return false;

                if (WeaponEffect != BTAttackEffect.None)
                    return true;

                switch (ItemEffect)
                {
                    case BT3ItemEffect.None:
                    case BT3ItemEffect.Throw:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override byte GetFlagByte()
        {
            BT3ItemFlags flags = Contains;
            if (!Identified)
                flags |= BT3ItemFlags.Unidentified;
            if (FailedIdentify)
                flags |= BT3ItemFlags.FailedIdentify;
            if (WhereEquipped != EquipLocation.None)
                flags |= BT3ItemFlags.Equipped;
            return (byte)flags;
        }

        public override byte[] Serialize() { return new byte[] { GetFlagByte(), (byte)Index, (byte)ChargesCurrent }; }

        public override byte[] GetBytes()
        {
            return new byte[]
            {
                (byte) (AC | (Damage.Bonus << 4)),
                (byte) ((int) BTType | ((int) WeaponEffect << 4)),
                GetUsableByte(Usable, Game),
                GetEffectByte(ItemEffect, Spell),
                (IsWeapon && Damage.Quantity > 0 && Damage.Faces > 1) ? 
                    (byte) ((Damage.Quantity - 1) | ((Global.NumRightZeros(Damage.Faces) - 1) << 5)) : (byte) 0,
                GetValueByte((int) Value),
                (byte) Charges,
                (byte) EquipEffect,
                (byte) (Contains & BT3ItemFlags.Filled)
            };
        }

        public override bool IsWeapon { get { return base.IsWeapon || BTType == BTItemType.Quiver; } }

        public static byte GetEffectByte(BT3ItemEffect effect, BT3SpellIndex spell)
        {
            if (effect >= BT3ItemEffect.ShortLight && effect < BT3ItemEffect.Last)
                return (byte)effect;

            if (spell > BT3SpellIndex.MageFlame && spell < BT3SpellIndex.SirRobinsTune)
                return (byte)(spell - 1);

            return 0;
        }

        public static BT3ItemEffect GetItemEffect(int index)
        {
            if (index >= (int) BT3ItemEffect.ShortLight && index < 255)
                return (BT3ItemEffect)index;

            return BT3ItemEffect.None;
        }

        public static BT3SpellIndex GetItemSpell(int index)
        {
            if (index > 0 && index < (int)BT3SpellIndex.Gotterdamurung)
                return (BT3SpellIndex)index + 1;

            return BT3SpellIndex.None;
        }

        public override bool CanEquip { get { return true; } }      // Can equip almost anything in BT3

        public override string GetName(int index) { return GetName((BT3ItemIndex) index); }

        public override bool Trashable
        {
            get
            {
                switch ((BT3ItemIndex) Index)
                {
                    case BT3ItemIndex.CrystalLens:
                    case BT3ItemIndex.SmokeyLens:
                    case BT3ItemIndex.BlackLens:
                    case BT3ItemIndex.SphereOfLanatir:
                    case BT3ItemIndex.WandOfPower:
                    case BT3ItemIndex.Acorn:
                    case BT3ItemIndex.Nightspear:
                    case BT3ItemIndex.TslothasHead:
                    case BT3ItemIndex.TslothasHeart:
                    case BT3ItemIndex.Arefolia:
                    case BT3ItemIndex.ValariansBow:
                    case BT3ItemIndex.ArrowsOfLife:
                    case BT3ItemIndex.CrownOfTruth:
                    case BT3ItemIndex.BeltOfAlliria:
                    case BT3ItemIndex.CrystalKey:
                    case BT3ItemIndex.WhiteRose:
                    case BT3ItemIndex.BlueRose:
                    case BT3ItemIndex.RedRose:
                    case BT3ItemIndex.YellowRose:
                    case BT3ItemIndex.RainbowRose:
                    case BT3ItemIndex.MagicTriangle:
                    case BT3ItemIndex.HammerOfWrath:
                    case BT3ItemIndex.FerofistsHelm:
                    case BT3ItemIndex.HelmOfJustice:
                    case BT3ItemIndex.SceadusCloak:
                    case BT3ItemIndex.WerrasShield:
                    case BT3ItemIndex.Strifespear:
                    case BT3ItemIndex.Sheetmusic:
                    case BT3ItemIndex.RightKey:
                    case BT3ItemIndex.LeftKey:
                    case BT3ItemIndex.ShadowLock:
                    case BT3ItemIndex.ShadowDoor:
                        return false;
                    default:
                        return true;
                }
            }
        }

        public static string GetName(BT3ItemIndex index)
        {
            switch (index)
            {
                case BT3ItemIndex.Torch: return "Torch";
                case BT3ItemIndex.Lamp: return "Lamp";
                case BT3ItemIndex.Broadsword: return "Broadsword";
                case BT3ItemIndex.ShortSword: return "Short Sword";
                case BT3ItemIndex.Dagger: return "Dagger";
                case BT3ItemIndex.WarAxe: return "War Axe";
                case BT3ItemIndex.Halbard: return "Halbard";
                case BT3ItemIndex.LongBow: return "Long Bow";
                case BT3ItemIndex.Staff: return "Staff";
                case BT3ItemIndex.Buckler: return "Buckler";
                case BT3ItemIndex.TowerShield: return "Tower Shield";
                case BT3ItemIndex.LeatherArmor: return "Leather Armor";
                case BT3ItemIndex.ChainMail: return "Chain Mail";
                case BT3ItemIndex.ScaleArmor: return "Scale Armor";
                case BT3ItemIndex.PlateArmor: return "Plate Armor";
                case BT3ItemIndex.Robes: return "Robes";
                case BT3ItemIndex.Helm: return "Helm";
                case BT3ItemIndex.LeatherGloves: return "Leather Gloves";
                case BT3ItemIndex.Gauntlets: return "Gauntlets";
                case BT3ItemIndex.Mandolin: return "Mandolin";
                case BT3ItemIndex.Spear: return "Spear";
                case BT3ItemIndex.Arrows: return "Arrows";
                case BT3ItemIndex.MithrilSword: return "Mithril Sword";
                case BT3ItemIndex.MithrilShield: return "Mithril Shield";
                case BT3ItemIndex.MithrilChain: return "Mithril Chain";
                case BT3ItemIndex.MithrilScale: return "Mithril Scale";
                case BT3ItemIndex.GiantFgn: return "Giant Figurine";
                case BT3ItemIndex.MithrilBracers: return "Mithril Bracers";
                case BT3ItemIndex.Bardsword: return "Bardsword";
                case BT3ItemIndex.FireHorn: return "Fire Horn";
                case BT3ItemIndex.Litewand: return "Litewand";
                case BT3ItemIndex.MithrilDagger: return "Mithril Dagger";
                case BT3ItemIndex.MithrilHelm: return "Mithril Helm";
                case BT3ItemIndex.MithrilGloves: return "Mithril Gloves";
                case BT3ItemIndex.MithrilAxe: return "Mithril Axe";
                case BT3ItemIndex.Shuriken: return "Shuriken";
                case BT3ItemIndex.MithrilPlate: return "Mithril Plate";
                case BT3ItemIndex.MoltenFgn: return "Molten Figurine";
                case BT3ItemIndex.SpellSpear: return "Spell Spear";
                case BT3ItemIndex.ShieldRing: return "Shield Ring";
                case BT3ItemIndex.FinsFlute: return "Fin's Flute";
                case BT3ItemIndex.KaelsAxe: return "Kael's Axe";
                case BT3ItemIndex.MithrilArrows: return "Mithril Arrows";
                case BT3ItemIndex.Dayblade: return "Dayblade";
                case BT3ItemIndex.ShieldStaff: return "Shield Staff";
                case BT3ItemIndex.ElfCloak: return "Elf Cloak";
                case BT3ItemIndex.Hawkblade: return "Hawkblade";
                case BT3ItemIndex.AdamantSword: return "Adamant Sword";
                case BT3ItemIndex.AdamantShield: return "Adamant Shield";
                case BT3ItemIndex.AdamantHelm: return "Adamant Helm";
                case BT3ItemIndex.AdamantGloves: return "Adamant Gloves";
                case BT3ItemIndex.Pureblade: return "Pureblade";
                case BT3ItemIndex.Boomerang: return "Boomerang";
                case BT3ItemIndex.AlisCarpet: return "Ali's Carpet";
                case BT3ItemIndex.Luckshield: return "Luckshield";
                case BT3ItemIndex.DozerFgn: return "Dozer Figurine";
                case BT3ItemIndex.AdamantChain: return "Adamant Chain";
                case BT3ItemIndex.DeathStars: return "Death Stars";
                case BT3ItemIndex.AdamantPlate: return "Adamant Plate";
                case BT3ItemIndex.AdamantBracers: return "Adamant Bracers";
                case BT3ItemIndex.SlayerFgn: return "Slayer Figurine";
                case BT3ItemIndex.PureShield: return "Pure Shield";
                case BT3ItemIndex.MageStaff: return "Mage Staff";
                case BT3ItemIndex.WarStaff: return "War Staff";
                case BT3ItemIndex.ThiefDagger: return "Thief Dagger";
                case BT3ItemIndex.SoulMace: return "Soul Mace";
                case BT3ItemIndex.Ogrewand: return "Ogrewand";
                case BT3ItemIndex.Katosbracer: return "Kato's bracer";
                case BT3ItemIndex.Sorcerstaff: return "Sorcerstaff";
                case BT3ItemIndex.GaltsFlute: return "Galt's Flute";
                case BT3ItemIndex.FrostHorn: return "Frost Horn";
                case BT3ItemIndex.AgsArrows: return "Ag's Arrows";
                case BT3ItemIndex.DiamondShield: return "Diamond Shield";
                case BT3ItemIndex.BardBow: return "Bard Bow";
                case BT3ItemIndex.DiamondHelm: return "Diamond Helm";
                case BT3ItemIndex.ElfBoots: return "Elf Boots";
                case BT3ItemIndex.VanquisherFgn: return "Vanquisher Figurine";
                case BT3ItemIndex.Conjurstaff: return "Conjurstaff";
                case BT3ItemIndex.StaffOfLor: return "Staff of Lor";
                case BT3ItemIndex.FlameSword: return "Flame Sword";
                case BT3ItemIndex.Powerstaff: return "Powerstaff";
                case BT3ItemIndex.BreathRing: return "Breath Ring";
                case BT3ItemIndex.Dragonshield: return "Dragonshield";
                case BT3ItemIndex.DiamondPlate: return "Diamond Plate";
                case BT3ItemIndex.Wargloves: return "Wargloves";
                case BT3ItemIndex.Wizhelm: return "Wizhelm";
                case BT3ItemIndex.Dragonwand: return "Dragonwand";
                case BT3ItemIndex.Deathring: return "Deathring";
                case BT3ItemIndex.CrystalSword: return "Crystal Sword";
                case BT3ItemIndex.Speedboots: return "Speedboots";
                case BT3ItemIndex.FlameHorn: return "Flame Horn";
                case BT3ItemIndex.ZenArrows: return "Zen Arrows";
                case BT3ItemIndex.Deathdrum: return "Deathdrum";
                case BT3ItemIndex.PipesOfPan: return "Pipes of Pan";
                case BT3ItemIndex.PowerRing: return "Power Ring";
                case BT3ItemIndex.SongAxe: return "Song Axe";
                case BT3ItemIndex.TrickBrick: return "Trick Brick";
                case BT3ItemIndex.DragonFgn: return "Dragon Figurine";
                case BT3ItemIndex.MageFgn: return "Mage Figurine";
                case BT3ItemIndex.TrollRing: return "Troll Ring";
                case BT3ItemIndex.AramsKnife: return "Aram's Knife";
                case BT3ItemIndex.AngrasEye: return "Angra's Eye";
                case BT3ItemIndex.HerbFgn: return "Herb Figurine";
                case BT3ItemIndex.MasterWand: return "Master Wand";
                case BT3ItemIndex.BrothersFgn: return "Brothers Figurine";
                case BT3ItemIndex.Dynamite: return "Dynamite";
                case BT3ItemIndex.ThorsHammer: return "Thor's Hammer";
                case BT3ItemIndex.Stoneblade: return "Stoneblade";
                case BT3ItemIndex.HolyHandgrenade: return "Holy Handgrenade";
                case BT3ItemIndex.Masterkey: return "Masterkey";
                case BT3ItemIndex.NospinRing: return "Nospin Ring";
                case BT3ItemIndex.CrystalLens: return "Crystal Lens";
                case BT3ItemIndex.SmokeyLens: return "Smokey Lens";
                case BT3ItemIndex.BlackLens: return "Black Lens";
                case BT3ItemIndex.SphereOfLanatir: return "Sphere of Lanatir";
                case BT3ItemIndex.WandOfPower: return "Wand of Power";
                case BT3ItemIndex.Acorn: return "Acorn";
                case BT3ItemIndex.Wineskin: return "Wineskin";
                case BT3ItemIndex.Nightspear: return "Nightspear";
                case BT3ItemIndex.TslothasHead: return "Tslotha's Head";
                case BT3ItemIndex.TslothasHeart: return "Tslotha's Heart";
                case BT3ItemIndex.Arefolia: return "Arefolia";
                case BT3ItemIndex.ValariansBow: return "Valarian's Bow";
                case BT3ItemIndex.ArrowsOfLife: return "Arrows of Life";
                case BT3ItemIndex.Canteen: return "Canteen";
                case BT3ItemIndex.TitanPlate: return "Titan Plate";
                case BT3ItemIndex.TitanShield: return "Titan Shield";
                case BT3ItemIndex.TitanHelm: return "Titan Helm";
                case BT3ItemIndex.FireSpear: return "Fire Spear";
                case BT3ItemIndex.WillowFlute: return "Willow Flute";
                case BT3ItemIndex.Firebrand: return "Firebrand";
                case BT3ItemIndex.HolySword: return "Holy Sword";
                case BT3ItemIndex.WandOfFury: return "Wand of Fury";
                case BT3ItemIndex.Lightstar: return "Lightstar";
                case BT3ItemIndex.CrownOfTruth: return "Crown of Truth";
                case BT3ItemIndex.BeltOfAlliria: return "Belt of Alliria";
                case BT3ItemIndex.CrystalKey: return "Crystal Key";
                case BT3ItemIndex.TaoRing: return "Tao Ring";
                case BT3ItemIndex.StealthArrows: return "Stealth Arrows";
                case BT3ItemIndex.YellowStaff: return "Yellow Staff";
                case BT3ItemIndex.SteadyEye: return "Steady Eye";
                case BT3ItemIndex.DivineHalbard: return "Divine Halbard";
                case BT3ItemIndex.Incense: return "Incense";
                case BT3ItemIndex.IChing: return "I-ching";
                case BT3ItemIndex.WhiteRose: return "White Rose";
                case BT3ItemIndex.BlueRose: return "Blue Rose";
                case BT3ItemIndex.RedRose: return "Red Rose";
                case BT3ItemIndex.YellowRose: return "Yellow Rose";
                case BT3ItemIndex.RainbowRose: return "Rainbow Rose";
                case BT3ItemIndex.MagicTriangle: return "Magic Triangle";
                case BT3ItemIndex.HammerOfWrath: return "Hammer of Wrath";
                case BT3ItemIndex.FerofistsHelm: return "Ferofist's Helm";
                case BT3ItemIndex.HelmOfJustice: return "Helm of Justice";
                case BT3ItemIndex.SceadusCloak: return "Sceadu's Cloak";
                case BT3ItemIndex.Shadelance: return "Shadelance";
                case BT3ItemIndex.BlackArrows: return "Black Arrows";
                case BT3ItemIndex.WerrasShield: return "Werra's Shield";
                case BT3ItemIndex.Strifespear: return "Strifespear";
                case BT3ItemIndex.Sheetmusic: return "Sheetmusic";
                case BT3ItemIndex.RightKey: return "Right Key";
                case BT3ItemIndex.LeftKey: return "Left Key";
                case BT3ItemIndex.Lever: return "Lever";
                case BT3ItemIndex.Nut: return "Nut";
                case BT3ItemIndex.Bolt: return "Bolt";
                case BT3ItemIndex.Spanner: return "Spanner";
                case BT3ItemIndex.ShadowLock: return "Shadow Lock";
                case BT3ItemIndex.ShadowDoor: return "Shadow Door";
                case BT3ItemIndex.Misericorde: return "Misericorde";
                case BT3ItemIndex.HolyAvenger: return "Holy Avenger";
                case BT3ItemIndex.Shadowshiv: return "Shadowshiv";
                case BT3ItemIndex.KalisGarrote: return "Kali's Garrote";
                case BT3ItemIndex.FlameKnife: return "Flame Knife";
                case BT3ItemIndex.RedsStiletto: return "Red's Stiletto";
                case BT3ItemIndex.Heartseeker: return "Heartseeker";
                case BT3ItemIndex.DiamondScale: return "Diamond Scale";
                case BT3ItemIndex.HolyTNT: return "Holy TNT";
                case BT3ItemIndex.EternalTorch: return "Eternal Torch";
                case BT3ItemIndex.OsconsStaff: return "Oscon's Staff";
                case BT3ItemIndex.AngelsRing: return "Angel's Ring";
                case BT3ItemIndex.Deathhorn: return "Deathhorn";
                case BT3ItemIndex.StaffOfMangar: return "Staff of Mangar";
                case BT3ItemIndex.TeslaRing: return "Tesla Ring";
                case BT3ItemIndex.DiamondBracers: return "Diamond Bracers";
                case BT3ItemIndex.DeathFgn: return "Death Figurine";
                case BT3ItemIndex.ThunderSword: return "Thunder Sword";
                case BT3ItemIndex.PoisonDagger: return "Poison Dagger";
                case BT3ItemIndex.SparkBlade: return "Spark Blade";
                case BT3ItemIndex.GalvanicOboe: return "Galvanic Oboe";
                case BT3ItemIndex.HarmonicGem: return "Harmonic Gem";
                case BT3ItemIndex.TungstenShield: return "Tungsten Shield";
                case BT3ItemIndex.TungstenPlate: return "Tungsten Plate";
                case BT3ItemIndex.MinstrelsGlove: return "Minstrels Glove";
                case BT3ItemIndex.HuntersCloak: return "Hunters Cloak";
                case BT3ItemIndex.DeathHammer: return "Death Hammer";
                case BT3ItemIndex.BloodMeshRobe: return "Blood Mesh Robe";
                case BT3ItemIndex.SoothingBalm: return "Soothing Balm";
                case BT3ItemIndex.MagesCloak: return "Mages Cloak";
                case BT3ItemIndex.FamiliarFgn: return "Familiar Figurine";
                case BT3ItemIndex.Hourglass: return "Hourglass";
                case BT3ItemIndex.ThievesHood: return "Thieves Hood";
                case BT3ItemIndex.SurehandAmulet: return "Surehand Amulet";
                case BT3ItemIndex.ThievesDart: return "Thieves Dart";
                case BT3ItemIndex.ShrillFlute: return "Shrill Flute";
                case BT3ItemIndex.AngelsHarp: return "Angel's Harp";
                case BT3ItemIndex.TheBook: return "The Book";
                case BT3ItemIndex.TrothLance: return "Troth Lance";
                case BT3ItemIndex.DiamondSuit: return "Diamond Suit";
                case BT3ItemIndex.DiamondFlail: return "Diamond Flail";
                case BT3ItemIndex.PurpleHeart: return "Purple Heart";
                case BT3ItemIndex.TitanBracers: return "Titan Bracers";
                case BT3ItemIndex.EelskinTunic: return "Eelskin Tunic";
                case BT3ItemIndex.SorcerersHood: return "Sorcerer's Hood";
                case BT3ItemIndex.DiamondStaff: return "Diamond Staff";
                case BT3ItemIndex.CrystalGem: return "Crystal Gem";
                case BT3ItemIndex.WandOfForce: return "Wand of Force";
                case BT3ItemIndex.CliLyre: return "Cli Lyre";
                case BT3ItemIndex.YouthPotion: return "Youth Potion";
                case BT3ItemIndex.MithrilSuit: return "Mithril Suit";
                case BT3ItemIndex.TitanSuit: return "Titan Suit";
                case BT3ItemIndex.MagesGlove: return "Mages Glove";
                case BT3ItemIndex.FlareCrystal: return "Flare Crystal";
                case BT3ItemIndex.HolyMissile: return "Holy Missile";
                case BT3ItemIndex.GodsBlade: return "Gods' Blade";
                case BT3ItemIndex.HunterBlade: return "Hunter Blade";
                case BT3ItemIndex.StaffOfGods: return "Staff of Gods";
                case BT3ItemIndex.HornOfGods: return "Horn of Gods";
                default: return String.Format("Unused Item #{0}", (int)index);
            }
        }

        public override string EquipEffects { get { return BT2Item.GetEquipEffect(EquipEffect); } }

        public override string UseEffectString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Spell != BT3SpellIndex.None)
                    sb.AppendFormat("Cast {0}; ", BT3.Spells[(int)Spell - 1].Name);
                if (ItemEffect != BT3ItemEffect.None)
                    sb.AppendFormat("{0}; ", BT3Item.GetEffectString(ItemEffect, (BT3ItemIndex) Index));
                if (WeaponEffect != BTAttackEffect.None)
                    sb.AppendFormat("{0}; ", GetWeaponEffectString(WeaponEffect));
                if (sb.Length > 0 && sb[sb.Length - 2] == ';')
                    sb.Remove(sb.Length - 2, 2);
                return sb.ToString();
            }
        }

        public static string GetEffectString(BT3ItemEffect index, BT3ItemIndex item = BT3ItemIndex.None)
        {
            DamageDice dd = GetMissileDamage(item);
            String strMissile = (dd.Quantity == 0 ? "" : String.Format(" {0}", dd.ToString()));
            switch (index)
            {
                case BT3ItemEffect.None: return String.Empty;
                case BT3ItemEffect.ShortLight: return "8-hour Light";
                case BT3ItemEffect.MediumLight: return "16-hour Light";
                case BT3ItemEffect.Throw:
                    {
                        switch (item)
                        {
                        case BT3ItemIndex.WarAxe: return "Throw 20'" + strMissile;
                        case BT3ItemIndex.Spear: return "Throw 20'" + strMissile;
                        case BT3ItemIndex.MithrilAxe: return "Throw 30'" + strMissile;
                        case BT3ItemIndex.Shuriken: return "Throw 30'" + strMissile;
                        case BT3ItemIndex.SpellSpear: return "Throw 40'" + strMissile;
                        case BT3ItemIndex.Boomerang: return "Throw 40'" + strMissile;
                        case BT3ItemIndex.DeathStars: return "Throw 60'" + strMissile;
                        case BT3ItemIndex.SongAxe: return "Throw 80'" + strMissile;
                        case BT3ItemIndex.AramsKnife: return "Throw 90'" + strMissile;
                        case BT3ItemIndex.ThorsHammer: return "Throw 70'" + strMissile;
                        case BT3ItemIndex.Nightspear: return "Throw 70'" + strMissile;
                        case BT3ItemIndex.FireSpear: return "Throw 60'" + strMissile;
                        case BT3ItemIndex.Lightstar: return "Throw 60'" + strMissile;
                        case BT3ItemIndex.HolyMissile: return "Throw 80'" + strMissile;
                        case BT3ItemIndex.KaelsAxe: return "Throw 40'" + strMissile;
                        case BT3ItemIndex.Arrows: return "Shoot 30'" + strMissile;
                        case BT3ItemIndex.MithrilArrows: return "Shoot 50'" + strMissile;
                        case BT3ItemIndex.AgsArrows: return "Shoot 90'" + strMissile;
                        case BT3ItemIndex.ZenArrows: return "Shoot 70'" + strMissile;
                        case BT3ItemIndex.ArrowsOfLife: return "Shoot 50'" + strMissile;
                        case BT3ItemIndex.StealthArrows: return "Shoot 60'" + strMissile;
                        case BT3ItemIndex.BlackArrows: return "Shoot 90'" + strMissile;
                        default: return "Shoot/Throw" + strMissile;
                        }
                    }
                case BT3ItemEffect.Quest: return "Quest-specific";
                case BT3ItemEffect.RestoreAllSP: return "Restore All SP";
                case BT3ItemEffect.Summon:
                    switch (item)
                    {
                        case BT3ItemIndex.DragonFgn: return "Summon Blast Dragon";
                        case BT3ItemIndex.MageFgn: return "Summon One-eyed Angra";
                        case BT3ItemIndex.SlayerFgn: return "Summon Slayer";
                        case BT3ItemIndex.DozerFgn: return "Summon Bulldozer";
                        case BT3ItemIndex.MoltenFgn: return "Summon Molten Man";
                        case BT3ItemIndex.DeathFgn: return "Summon Black Death";
                        case BT3ItemIndex.FamiliarFgn: return "Summon Familiar";
                        case BT3ItemIndex.VanquisherFgn: return "Summon Vanquisher";
                        default:  return "Summon Ally";
                    }
                case BT3ItemEffect.BreathDamage: return "Breath Damage";
                case BT3ItemEffect.Eat: return "Eat";
                case BT3ItemEffect.Drink: return "Drink";

                default:
                    return String.Format("Unknown({0})", (int)index);
            }
        }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sbFull = new StringBuilder(base.MultiLineDescription);
                if (BTType == BTItemType.Container && ChargesCurrent > 0)
                    sbFull.AppendFormat("Contains: {0}\r\n", ContentsString(Contains));
                if (ChargesCurrent < 255)
                    sbFull.AppendFormat("Charges: {0}\r\n", ChargesCurrent);
                return sbFull.ToString();
            }
        }

        public static string ContentsString(BT3ItemFlags flags)
        {
            if (flags.HasFlag(BT3ItemFlags.FilledDragonBlood))
                return "Dragon Blood";
            if (flags.HasFlag(BT3ItemFlags.FilledMoltenTar))
                return "Molten Tar";
            if (flags.HasFlag(BT3ItemFlags.FilledSpirits))
                return "Spirits";
            if (flags.HasFlag(BT3ItemFlags.FilledWaterOfLife))
                return "Water of Life";
            return "Water";
        }

        public override string DescriptionString
        {
            get
            {
                if (BTType == BTItemType.Container && ChargesCurrent > 0)
                    return String.Format("{0} ({1})", Name, ContentsString(Contains));
                return Name;
            }
        }

        public static void SetUsableBit(byte[] bytes, GenericClass gc, int iOffset = 0)
        {
            // Bard's Tale 3 items in the inventory have a bit that means "this character can't use this item"
            // which needs to be set explicitly when a new item is created or transferred between characters
            if (bytes == null || bytes.Length < iOffset + 2 || bytes[iOffset + 1] < 1 || bytes[iOffset + 1] >= BT3.Items.Count)
                return;
            if (BT3.Items[bytes[iOffset + 1]].IsUsable(gc))
                bytes[iOffset] &= (~ (int) BT3ItemFlags.Unequippable & 0xff);
            else
                bytes[iOffset] |= (byte) BT3ItemFlags.Unequippable;
        }
    }

    public class BT3ItemList : BT123ItemList
    {
        public override BTItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return new BT3Item(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.BT3_Item_List); }
        public override int BlockSize { get { return 256; } }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes mbItemAC = hacker.ReadOffset(BT3.Memory.ItemACBonus, BlockSize);
            MemoryBytes mbTypes = hacker.ReadOffset(BT3.Memory.ItemTypes, BlockSize);
            MemoryBytes mbUsableBy = hacker.ReadOffset(BT3.Memory.ItemUsableBy, BlockSize);
            MemoryBytes mbEffects = hacker.ReadOffset(BT3.Memory.ItemEffects, BlockSize);
            MemoryBytes mbDamage = hacker.ReadOffset(BT3.Memory.ItemDamage, BlockSize);
            MemoryBytes mbValues = new MemoryBytes(Global.NullBytes(BlockSize), 0);
            MemoryBytes mbCharges = hacker.ReadOffset(BT3.Memory.ItemCharges, BlockSize);
            MemoryBytes mbEquipEffects = hacker.ReadOffset(BT3.Memory.ItemEquipEffect, BlockSize);
            if (mbItemAC == null || mbTypes == null || mbUsableBy == null || mbEffects == null || mbDamage == null || mbValues == null || mbCharges == null || mbEquipEffects == null)
                return GetInternalBytes();
            return Global.Combine(mbItemAC.Bytes, mbTypes.Bytes, mbUsableBy.Bytes, mbEffects.Bytes, mbDamage.Bytes, mbValues.Bytes, mbCharges.Bytes, mbEquipEffects.Bytes);
        }

        public BT3ItemList()
        {
            InitBT3InternalList();
        }

        private void InitBT3InternalList()
        {
            Items = SetFromBytes(Global.DecompressBytes(Properties.Resources.BT3_Item_List));
        }

        public override bool InitInternalList()
        {
            Items = SetFromBytes(GetInternalBytes());
            return true;
        }
    }
}
