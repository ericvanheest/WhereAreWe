using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum BT1ItemIndex
    {
        None = 0,
        Torch = 1,
        Lamp = 2,
        Broadsword = 3,
        ShortSword = 4,
        Dagger = 5,
        WarAxe = 6,
        Halbard = 7,
        Mace = 8,
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
        Harp = 21,
        Flute = 22,
        MithrilSword = 23,
        MithrilShield = 24,
        MithrilChain = 25,
        MithrilScale = 26,
        SamuraiFigurine = 27,
        Bracers6 = 28,
        BardSword = 29,
        FireHorn = 30,
        LightWand = 31,
        MithrilDagger = 32,
        MithrilHelm = 33,
        MithrilGloves = 34,
        MithrilAxe = 35,
        MithrilMace = 36,
        MithrilPlate = 37,
        OgreFigurine = 38,
        LaksLyre = 39,
        ShieldRing = 40,
        DorkRing = 41,
        FinsFlute = 42,
        KaelsAxe = 43,
        BloodAxe = 44,
        Dayblade = 45,
        ShieldStaff = 46,
        ElfCloak = 47,
        Hawkblade = 48,
        AdamantSword = 49,
        AdamantShield = 50,
        AdamantDagger = 51,
        AdamantHelm = 52,
        AdamantGloves = 53,
        AdamantMace = 54,
        Broom = 55,
        Pureblade = 56,
        Exorwand = 57,
        AlisCarpet = 58,
        MagicMouth = 59,
        LuckShield = 60,
        GiantFigurine = 61,
        AdamantChain = 62,
        AdamantScale = 63,
        AdamantPlate = 64,
        Bracers4 = 65,
        ArcShield = 66,
        PureShield = 67,
        MageStaff = 68,
        WarStaff = 69,
        ThiefDagger = 70,
        SoulMace = 71,
        WitherStaff = 72,
        SorcerersStaff = 73,
        SwordofPak = 74,
        HealHarp = 75,
        GaltsFlute = 76,
        FrostHorn = 77,
        DiamondSword = 78,
        DiamondShield = 79,
        DiamondDagger = 80,
        DiamondHelm = 81,
        GolemFigurine = 82,
        TitanFigurine = 83,
        ConjurersStaff = 84,
        ArcsHammer = 85,
        StaffofLor = 86,
        PowerStaff = 87,
        Mournblade = 88,
        DragonShield = 89,
        DiamondPlate = 90,
        Wargloves = 91,
        LoreHelm = 92,
        DragonWand = 93,
        KielsCompass = 94,
        SpeedBoots = 95,
        FlameHorn = 96,
        TruthDrum = 97,
        SpiritDrum = 98,
        PipesofPan = 99,
        RingofPower = 100,
        DeathRing = 101,
        YbarraShield = 102,
        SpectreMace = 103,
        DagStone = 104,
        ArcsEye = 105,
        OgreWand = 106,
        SpiritHelm = 107,
        DragonFigurine = 108,
        MageFigurine = 109,
        TrollRing = 110,
        TrollStaff = 111,
        OnyxKey = 112,
        CrystalSword = 113,
        Stoneblade = 114,
        TravelHelm = 115,
        DeathDagger = 116,
        MongoFigurine = 117,
        LichFigurine = 118,
        Eye = 119,
        MasterKey = 120,
        WizWand = 121,
        SilverSquare = 122,
        SilverCircle = 123,
        SilverTriangle = 124,
        ThorFigurine = 125,
        OldManFigurine = 126,
        SpectreSnare = 127,
        Last
    }

    public enum BT1ItemEffect
    {
        None = 0,
        UnlimitedBardSongs,
        RegenMP,
        RegenHP,
        IncreaseHide,
        HalfSpellCost,
        HideAlwaysSucceeds,
        FreezeGroup,
        ShortLight,
        MediumLight,
        Samurai,
        GroupDamage33to46,
        HideSucceeds100,
        WarGiant,
        GroupDamage52to67,
        Golem,
        Titan,
        Insanity,
        GroupDamage42to43,
        HelpFleeing,
        GroupDamage86to101,
        Dragon,
        MasterWizard,
        AccessMangarsTower,
        Mongo,
        Lich,
        ActivateTarjanStatue,
        OpenAllGates,
        Thor,
        OldMan,
        DestroyCrystalGolem,
        Ogre,
    }

    public class BT1Item : BTItem
    {
        public BT1ItemEffect ItemEffect;
        public BT1SpellIndex Spell;
        public override int SpellIndex { get { return (int)Spell; } }
        public override int EffectIndex { get { return (int)ItemEffect; } }

        public BT1Item(int index, byte[] bytes, int offset = 0)
        {
            SetBT1Bytes(index, bytes, offset);
        }

        public BT1Item(BT1ItemIndex index, DamageDice damage, long value, BT1ItemEffect effect, BT1SpellIndex spell, int ac, BTUseFlags usable)
        {
            m_strName = GetName((BT1ItemIndex)index);
            ItemIndex = index;
            Damage = damage;
            m_value = value;
            ItemEffect = effect;
            Usable = usable;
            Spell = spell;
            AC = ac;
            Identified = true;
            WhereEquipped = EquipLocation.None;
        }

        public override GameNames Game { get { return GameNames.BardsTale1; } }
        public override int Index { get { return m_index; } set { m_index = value; } }
        public override BTItem CreateItem(int index, byte[] bytes, int offset = 0) { return new BT1Item(index, bytes, offset); }
        public BT1ItemIndex ItemIndex { get { return (BT1ItemIndex)m_index; } set { m_index = (int)value; } }

        public override void SetBytes(int index, byte[] bytes, int offset = 0) { SetBT1Bytes(index, bytes, offset); }

        private void SetBT1Bytes(int index, byte[] bytes, int offset = 0)
        {
            ItemIndex = (BT1ItemIndex) index;
            m_strName = GetName((BT1ItemIndex)index);
            BTType = (BTItemType) (bytes[1] & 0x0f);
            WeaponEffect = (BTAttackEffect) ((bytes[1] & 0xf0) >> 4);
            int iFaceIndex = (bytes[4] & 0xf0) >> 4;
            if (bytes[4] == 0 && BTType != BTItemType.Weapon)
                Damage = DamageDice.Zero;
            else
                Damage = new DamageDice(iFaceIndex == 0 ? 4 : iFaceIndex * 4, (bytes[4] & 0x0f) + 1, (bytes[0] & 0xf0) >> 4);
            m_value = ((bytes[5] & 0xf8) >> 3) * (int) (Math.Pow(10, bytes[5] & 0x07));
            ItemEffect = GetItemEffect(bytes[3]);
            Spell = GetItemSpell(bytes[3]);
            Usable = GetUsableBy(bytes[2], Game);
            AC = (bytes[0] & 0x0f);
            Identified = true;
            WhereEquipped = EquipLocation.None;
        }

        public override byte[] GetBytes()
        {
            return new byte[]
            {
                (byte) (AC | (Damage.Bonus << 4)),
                (byte) ((int) BTType | ((int) WeaponEffect << 4)),
                GetUsableByte(Usable, Game),
                GetEffectByte(ItemEffect, Spell),
                IsWeapon ? (byte) ((Damage.Quantity - 1) | ((Damage.Faces <= 4 ? 0 : Damage.Faces / 4) << 4)) : (byte) 0,
                GetValueByte((int) Value)
            };
        }

        public static byte GetEffectByte(BT1ItemEffect effect, BT1SpellIndex spell)
        {
            switch(effect)
            {
                case BT1ItemEffect.RegenHP: return 0x01;
                case BT1ItemEffect.RegenMP: return 0x02;
                case BT1ItemEffect.HalfSpellCost: return 0x04;
                case BT1ItemEffect.UnlimitedBardSongs: return 0x05;
                case BT1ItemEffect.HideAlwaysSucceeds: return 0x06;
                case BT1ItemEffect.HelpFleeing: return 0x07;
                case BT1ItemEffect.IncreaseHide: return 0x08;
                case BT1ItemEffect.DestroyCrystalGolem: return 0x09;
                case BT1ItemEffect.ActivateTarjanStatue: return 0x0A;
                case BT1ItemEffect.AccessMangarsTower: return 0x0E;
                case BT1ItemEffect.OpenAllGates: return 0x0F;
                case BT1ItemEffect.GroupDamage33to46: return 0x2B;
                case BT1ItemEffect.GroupDamage42to43: return 0x2C;
                case BT1ItemEffect.GroupDamage52to67: return 0x2D;
                case BT1ItemEffect.GroupDamage86to101: return 0x2F;
                case BT1ItemEffect.Thor: return 0x39;
                case BT1ItemEffect.ShortLight: return 0xB0;
                case BT1ItemEffect.MediumLight: return 0xB1;
                case BT1ItemEffect.Dragon: return 0xB2;
                case BT1ItemEffect.WarGiant: return 0xB3;
                case BT1ItemEffect.Ogre: return 0xB4;
                case BT1ItemEffect.Mongo: return 0xB5;
                case BT1ItemEffect.OldMan: return 0xB7;
                case BT1ItemEffect.MasterWizard: return 0xBA;
                case BT1ItemEffect.Lich: return 0xBB;
                case BT1ItemEffect.Samurai: return 0xBC;
                case BT1ItemEffect.Titan: return 0xBD;
                case BT1ItemEffect.Golem: return 0xBE;
                default: break;
            }

            switch(spell)
            {
                case BT1SpellIndex.Warstrike: return 0x12;
                case BT1SpellIndex.SpectreTouch: return 0x19;
                case BT1SpellIndex.AkersAnimatedSword: return 0x1A;
                case BT1SpellIndex.StoneTouch: return 0x1B;
                case BT1SpellIndex.BaylorsSpellBind: return 0x28;
                case BT1SpellIndex.MageFlame: return 0x90;
                case BT1SpellIndex.WordOfHealing: return 0x91;
                case BT1SpellIndex.EliksInstantWolf: return 0x93;
                case BT1SpellIndex.GreaterRevelation: return 0x94;
                case BT1SpellIndex.EliksInstantOgre: return 0x95;
                case BT1SpellIndex.MajorLevitation: return 0x96;
                case BT1SpellIndex.FleshAnew: return 0x97;
                case BT1SpellIndex.ApportArcane: return 0x98;
                case BT1SpellIndex.Deathstrike: return 0x9C;
                case BT1SpellIndex.HypnoticImage: return 0x9D;
                case BT1SpellIndex.WordOfFear: return 0x9E;
                case BT1SpellIndex.Curse: return 0x9F;
                case BT1SpellIndex.KylearansInvisibilitySpell: return 0xA0;
                case BT1SpellIndex.MindWarp: return 0xA2;
                case BT1SpellIndex.WindGiant: return 0xA3;
                case BT1SpellIndex.Dispossess: return 0xA4;
                case BT1SpellIndex.LesserSummoning: return 0xA5;
                case BT1SpellIndex.PrimeSummoning: return 0xA6;
                case BT1SpellIndex.AnimateDead: return 0xA7;
                default: return 0x00;
            }
        }

        public static BT1ItemEffect GetItemEffect(int index)
        {
            switch (index)
            {
                case 0x00: return BT1ItemEffect.None;
                case 0x01: return BT1ItemEffect.RegenHP;
                case 0x02: return BT1ItemEffect.RegenMP;
                case 0x04: return BT1ItemEffect.HalfSpellCost;
                case 0x05: return BT1ItemEffect.UnlimitedBardSongs;
                case 0x06: return BT1ItemEffect.HideAlwaysSucceeds;
                case 0x07: return BT1ItemEffect.HelpFleeing;
                case 0x08: return BT1ItemEffect.IncreaseHide;
                case 0x09: return BT1ItemEffect.DestroyCrystalGolem;
                case 0x0A: return BT1ItemEffect.ActivateTarjanStatue;
                case 0x0E: return BT1ItemEffect.AccessMangarsTower;
                case 0x0F: return BT1ItemEffect.OpenAllGates;
                case 0x2B: return BT1ItemEffect.GroupDamage33to46;
                case 0x2C: return BT1ItemEffect.GroupDamage42to43;
                case 0x2D: return BT1ItemEffect.GroupDamage52to67;
                case 0x2F: return BT1ItemEffect.GroupDamage86to101;
                case 0x39: return BT1ItemEffect.Thor;
                case 0xB0: return BT1ItemEffect.ShortLight;
                case 0xB1: return BT1ItemEffect.MediumLight;
                case 0xB2: return BT1ItemEffect.Dragon;
                case 0xB3: return BT1ItemEffect.WarGiant;
                case 0xB4: return BT1ItemEffect.Ogre;
                case 0xB5: return BT1ItemEffect.Mongo;
                case 0xB7: return BT1ItemEffect.OldMan;
                case 0xBA: return BT1ItemEffect.MasterWizard;
                case 0xBB: return BT1ItemEffect.Lich;
                case 0xBC: return BT1ItemEffect.Samurai;
                case 0xBD: return BT1ItemEffect.Titan;
                case 0xBE: return BT1ItemEffect.Golem;
                default: return BT1ItemEffect.None;
            }
        }

        public static BT1SpellIndex GetItemSpell(int index)
        {
            switch (index)
            {
                case 0x12: return BT1SpellIndex.Warstrike;
                case 0x19: return BT1SpellIndex.SpectreTouch;
                case 0x1A: return BT1SpellIndex.AkersAnimatedSword;
                case 0x1B: return BT1SpellIndex.StoneTouch;
                case 0x28: return BT1SpellIndex.BaylorsSpellBind;
                case 0x90: return BT1SpellIndex.MageFlame;
                case 0x91: return BT1SpellIndex.WordOfHealing;
                case 0x93: return BT1SpellIndex.EliksInstantWolf;
                case 0x94: return BT1SpellIndex.GreaterRevelation;
                case 0x95: return BT1SpellIndex.EliksInstantOgre;
                case 0x96: return BT1SpellIndex.MajorLevitation;
                case 0x97: return BT1SpellIndex.FleshAnew;
                case 0x98: return BT1SpellIndex.ApportArcane;
                case 0x9C: return BT1SpellIndex.Deathstrike;
                case 0x9D: return BT1SpellIndex.HypnoticImage;
                case 0x9E: return BT1SpellIndex.WordOfFear;
                case 0x9F: return BT1SpellIndex.Curse;
                case 0xA0: return BT1SpellIndex.KylearansInvisibilitySpell;
                case 0xA2: return BT1SpellIndex.MindWarp;
                case 0xA3: return BT1SpellIndex.WindGiant;
                case 0xA4: return BT1SpellIndex.Dispossess;
                case 0xA5: return BT1SpellIndex.LesserSummoning;
                case 0xA6: return BT1SpellIndex.PrimeSummoning;
                case 0xA7: return BT1SpellIndex.AnimateDead;
                default: return BT1SpellIndex.None;
            }
        }

        public static string GetName(BT1ItemIndex index)
        {
            switch (index)
            {
                case BT1ItemIndex.None: return "Empty";
                case BT1ItemIndex.Torch: return "Torch";
                case BT1ItemIndex.Lamp: return "Lamp";
                case BT1ItemIndex.Broadsword: return "Broadsword";
                case BT1ItemIndex.ShortSword: return "Short Sword";
                case BT1ItemIndex.Dagger: return "Dagger";
                case BT1ItemIndex.WarAxe: return "War Axe";
                case BT1ItemIndex.Halbard: return "Halbard";
                case BT1ItemIndex.Mace: return "Mace";
                case BT1ItemIndex.Staff: return "Staff";
                case BT1ItemIndex.Buckler: return "Buckler";
                case BT1ItemIndex.TowerShield: return "Tower Shield";
                case BT1ItemIndex.LeatherArmor: return "Leather Armor";
                case BT1ItemIndex.ChainMail: return "Chain Mail";
                case BT1ItemIndex.ScaleArmor: return "Scale Armor";
                case BT1ItemIndex.PlateArmor: return "Plate Armor";
                case BT1ItemIndex.Robes: return "Robes";
                case BT1ItemIndex.Helm: return "Helm";
                case BT1ItemIndex.LeatherGloves: return "Leather Gloves";
                case BT1ItemIndex.Gauntlets: return "Gauntlets";
                case BT1ItemIndex.Mandolin: return "Mandolin";
                case BT1ItemIndex.Harp: return "Harp";
                case BT1ItemIndex.Flute: return "Flute";
                case BT1ItemIndex.MithrilSword: return "Mithril Sword";
                case BT1ItemIndex.MithrilShield: return "Mithril Shield";
                case BT1ItemIndex.MithrilChain: return "Mithril Chain";
                case BT1ItemIndex.MithrilScale: return "Mithril Scale";
                case BT1ItemIndex.SamuraiFigurine: return "Samurai Figurine";
                case BT1ItemIndex.Bracers6: return "Bracers 6";
                case BT1ItemIndex.BardSword: return "Bard Sword";
                case BT1ItemIndex.FireHorn: return "Fire Horn";
                case BT1ItemIndex.LightWand: return "Light Wand";
                case BT1ItemIndex.MithrilDagger: return "Mithril Dagger";
                case BT1ItemIndex.MithrilHelm: return "Mithril Helm";
                case BT1ItemIndex.MithrilGloves: return "Mithril Gloves";
                case BT1ItemIndex.MithrilAxe: return "Mithril Axe";
                case BT1ItemIndex.MithrilMace: return "Mithril Mace";
                case BT1ItemIndex.MithrilPlate: return "Mithril Plate";
                case BT1ItemIndex.OgreFigurine: return "Ogre Figurine";
                case BT1ItemIndex.LaksLyre: return "Lak's Lyre";
                case BT1ItemIndex.ShieldRing: return "Shield Ring";
                case BT1ItemIndex.DorkRing: return "Dork Ring";
                case BT1ItemIndex.FinsFlute: return "Fin's Flute";
                case BT1ItemIndex.KaelsAxe: return "Kael's Axe";
                case BT1ItemIndex.BloodAxe: return "Blood Axe";
                case BT1ItemIndex.Dayblade: return "Dayblade";
                case BT1ItemIndex.ShieldStaff: return "Shield Staff";
                case BT1ItemIndex.ElfCloak: return "Elf Cloak";
                case BT1ItemIndex.Hawkblade: return "Hawkblade";
                case BT1ItemIndex.AdamantSword: return "Adamant Sword";
                case BT1ItemIndex.AdamantShield: return "Adamant Shield";
                case BT1ItemIndex.AdamantDagger: return "Adamant Dagger";
                case BT1ItemIndex.AdamantHelm: return "Adamant Helm";
                case BT1ItemIndex.AdamantGloves: return "Adamant Gloves";
                case BT1ItemIndex.AdamantMace: return "Adamant Mace";
                case BT1ItemIndex.Broom: return "Broom";
                case BT1ItemIndex.Pureblade: return "Pureblade";
                case BT1ItemIndex.Exorwand: return "Exorwand";
                case BT1ItemIndex.AlisCarpet: return "Ali's Carpet";
                case BT1ItemIndex.MagicMouth: return "Magic Mouth";
                case BT1ItemIndex.LuckShield: return "Luck Shield";
                case BT1ItemIndex.GiantFigurine: return "Giant Figurine";
                case BT1ItemIndex.AdamantChain: return "Adamant Chain";
                case BT1ItemIndex.AdamantScale: return "Adamant Scale";
                case BT1ItemIndex.AdamantPlate: return "Adamant Plate";
                case BT1ItemIndex.Bracers4: return "Bracers 4";
                case BT1ItemIndex.ArcShield: return "Arc Shield";
                case BT1ItemIndex.PureShield: return "Pure Shield";
                case BT1ItemIndex.MageStaff: return "Mage Staff";
                case BT1ItemIndex.WarStaff: return "War Staff";
                case BT1ItemIndex.ThiefDagger: return "Thief Dagger";
                case BT1ItemIndex.SoulMace: return "Soul Mace";
                case BT1ItemIndex.WitherStaff: return "Wither Staff";
                case BT1ItemIndex.SorcerersStaff: return "Sorcerer's Staff";
                case BT1ItemIndex.SwordofPak: return "Sword of Pak";
                case BT1ItemIndex.HealHarp: return "Heal Harp";
                case BT1ItemIndex.GaltsFlute: return "Galt's Flute";
                case BT1ItemIndex.FrostHorn: return "Frost Horn";
                case BT1ItemIndex.DiamondSword: return "Diamond Sword";
                case BT1ItemIndex.DiamondShield: return "Diamond Shield";
                case BT1ItemIndex.DiamondDagger: return "Diamond Dagger";
                case BT1ItemIndex.DiamondHelm: return "Diamond Helm";
                case BT1ItemIndex.GolemFigurine: return "Golem Figurine";
                case BT1ItemIndex.TitanFigurine: return "Titan Figurine";
                case BT1ItemIndex.ConjurersStaff: return "Conjurer's Staff";
                case BT1ItemIndex.ArcsHammer: return "Arc's Hammer";
                case BT1ItemIndex.StaffofLor: return "Staff of Lor";
                case BT1ItemIndex.PowerStaff: return "Power Staff";
                case BT1ItemIndex.Mournblade: return "Mournblade";
                case BT1ItemIndex.DragonShield: return "Dragon Shield";
                case BT1ItemIndex.DiamondPlate: return "Diamond Plate";
                case BT1ItemIndex.Wargloves: return "Wargloves";
                case BT1ItemIndex.LoreHelm: return "Lore Helm";
                case BT1ItemIndex.DragonWand: return "Dragon Wand";
                case BT1ItemIndex.KielsCompass: return "Kiels Compass";
                case BT1ItemIndex.SpeedBoots: return "Speed Boots";
                case BT1ItemIndex.FlameHorn: return "Flame Horn";
                case BT1ItemIndex.TruthDrum: return "Truth Drum";
                case BT1ItemIndex.SpiritDrum: return "Spirit Drum";
                case BT1ItemIndex.PipesofPan: return "Pipes of Pan";
                case BT1ItemIndex.RingofPower: return "Ring of Power";
                case BT1ItemIndex.DeathRing: return "Death Ring";
                case BT1ItemIndex.YbarraShield: return "Ybarra Shield";
                case BT1ItemIndex.SpectreMace: return "Spectre Mace";
                case BT1ItemIndex.DagStone: return "Dag Stone";
                case BT1ItemIndex.ArcsEye: return "Arc's Eye";
                case BT1ItemIndex.OgreWand: return "Ogre Wand";
                case BT1ItemIndex.SpiritHelm: return "Spirit Helm";
                case BT1ItemIndex.DragonFigurine: return "Dragon Figurine";
                case BT1ItemIndex.MageFigurine: return "Mage Figurine";
                case BT1ItemIndex.TrollRing: return "Troll Ring";
                case BT1ItemIndex.TrollStaff: return "Troll Staff";
                case BT1ItemIndex.OnyxKey: return "Onyx Key";
                case BT1ItemIndex.CrystalSword: return "Crystal Sword";
                case BT1ItemIndex.Stoneblade: return "Stoneblade";
                case BT1ItemIndex.TravelHelm: return "Travel Helm";
                case BT1ItemIndex.DeathDagger: return "Death Dagger";
                case BT1ItemIndex.MongoFigurine: return "Mongo Figurine";
                case BT1ItemIndex.LichFigurine: return "Lich Figurine";
                case BT1ItemIndex.Eye: return "Eye";
                case BT1ItemIndex.MasterKey: return "Master Key";
                case BT1ItemIndex.WizWand: return "WizWand";
                case BT1ItemIndex.SilverSquare: return "Silver Square";
                case BT1ItemIndex.SilverCircle: return "Silver Circle";
                case BT1ItemIndex.SilverTriangle: return "Silver Triangle";
                case BT1ItemIndex.ThorFigurine: return "Thor Figurine";
                case BT1ItemIndex.OldManFigurine: return "Old Man Figurine";
                case BT1ItemIndex.SpectreSnare: return "Spectre Snare"; 
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public override ItemType Type
        {
            get
            {
                switch (ItemBasicType)
                {
                    case ItemNounType.Weapon:
                        return ItemType.Weapon;
                    case ItemNounType.Armor:
                    case ItemNounType.Helmet:
                    case ItemNounType.Shield:
                    case ItemNounType.Gloves:
                        return ItemType.Armor;
                    default:
                        return ItemType.Accessory;
                }
            }
            set { }
        }

        public override bool Trashable
        {
            get
            {
                switch ((BT1ItemIndex)Index)
                {
                    case BT1ItemIndex.OnyxKey:
                    case BT1ItemIndex.Eye:
                    case BT1ItemIndex.MasterKey:
                    case BT1ItemIndex.SilverSquare:
                    case BT1ItemIndex.SilverCircle:
                    case BT1ItemIndex.SilverTriangle:
                        return false;
                    default:
                        return true;
                }
            }
        }

        public override Item Clone()
        {
            return new BT1Item((int) ItemIndex, GetBytes());
        }

        public override bool PassiveEffect
        {
            get
            {
                if (Spell != BT1SpellIndex.None)
                    return false;

                if (WeaponEffect != BTAttackEffect.None)
                    return true;

                switch (ItemEffect)
                {
                    case BT1ItemEffect.None:
                    case BT1ItemEffect.UnlimitedBardSongs:
                    case BT1ItemEffect.RegenMP:
                    case BT1ItemEffect.RegenHP:
                    case BT1ItemEffect.IncreaseHide:
                    case BT1ItemEffect.HalfSpellCost:
                    case BT1ItemEffect.HideAlwaysSucceeds:
                    case BT1ItemEffect.HideSucceeds100:
                    case BT1ItemEffect.Insanity:
                    case BT1ItemEffect.HelpFleeing:
                    case BT1ItemEffect.AccessMangarsTower:
                    case BT1ItemEffect.ActivateTarjanStatue:
                    case BT1ItemEffect.OpenAllGates:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override string UseEffectString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Spell != BT1SpellIndex.None)
                    sb.AppendFormat("Cast {0}; ", BT1.Spells[(int)Spell - 1].Name);
                if (ItemEffect != BT1ItemEffect.None)
                    sb.AppendFormat("{0}; ", GetEffectString(ItemEffect));
                if (WeaponEffect != BTAttackEffect.None)
                    sb.AppendFormat("{0}; ", GetWeaponEffectString(WeaponEffect));
                if (sb.Length > 0 && sb[sb.Length - 2] == ';')
                    sb.Remove(sb.Length - 2, 2);
                return sb.ToString();
            }
        }

        public static string GetEffectString(BT1ItemEffect index)
        {
            switch (index)
            {
                case BT1ItemEffect.None: return String.Empty;
                case BT1ItemEffect.UnlimitedBardSongs: return "Unlimited Songs";
                case BT1ItemEffect.RegenMP: return "MP Regen +1";
                case BT1ItemEffect.RegenHP: return "HP Regen +1";
                case BT1ItemEffect.IncreaseHide: return "Increase Hide Chance";
                case BT1ItemEffect.HalfSpellCost: return "Half Spell Cost";
                case BT1ItemEffect.HideAlwaysSucceeds: return "Hide Always Succeeds";
                case BT1ItemEffect.FreezeGroup: return "Freeze Group";
                case BT1ItemEffect.ShortLight: return "Light";
                case BT1ItemEffect.MediumLight: return "Light";
                case BT1ItemEffect.Samurai: return "Summon Samurai";
                case BT1ItemEffect.GroupDamage33to46: return "Group Damage 33-46";
                case BT1ItemEffect.HideSucceeds100: return "Hide Succeeds 100%";
                case BT1ItemEffect.WarGiant: return "Summon War Giant";
                case BT1ItemEffect.Ogre: return "Summon Ogre";
                case BT1ItemEffect.Golem: return "Summon Golem";
                case BT1ItemEffect.Titan: return "Summon Titan";
                case BT1ItemEffect.Insanity: return "Insanity";
                case BT1ItemEffect.GroupDamage52to67: return "Group Damage 52-67";
                case BT1ItemEffect.GroupDamage42to43: return "Group Damage 42-43";
                case BT1ItemEffect.HelpFleeing: return "Helps Fleeing";
                case BT1ItemEffect.GroupDamage86to101: return "Group Damage 86-101";
                case BT1ItemEffect.Dragon: return "Summon Dragon";
                case BT1ItemEffect.MasterWizard: return "Summon Master Wizard";
                case BT1ItemEffect.AccessMangarsTower: return "Access Mangar's Tower";
                case BT1ItemEffect.Mongo: return "Summon Mongo";
                case BT1ItemEffect.Lich: return "Summon Lich";
                case BT1ItemEffect.ActivateTarjanStatue: return "Activate Tarjan Statue";
                case BT1ItemEffect.OpenAllGates: return "Open All Gates";
                case BT1ItemEffect.Thor: return "Summon Thor";
                case BT1ItemEffect.OldMan: return "Summon Old Man";
                case BT1ItemEffect.DestroyCrystalGolem: return "Destroy Crystal Golem";

                default:
                    return String.Format("Unknown({0})", (int)index);
            }
        }
    }

    public class BT1ItemList : BT123ItemList
    {
        public override BTItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return new BT1Item(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.BT1_Item_List); }
        public override int ItemLength { get { return 6; } }

        public override byte[] GetExternalBytes(MemoryHacker hacker) 
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes mbItems = hacker.ReadOffset(BT1.Memory.ItemACBonus, 512);
            MemoryBytes mbDamage = hacker.ReadOffset(BT1.Memory.ItemDamage, 128);
            MemoryBytes mbValues = hacker.ReadOffset(BT1.Memory.ItemValues, 128);
            if (mbItems == null || mbDamage == null || mbValues == null)
                return null;
            return Global.Combine(mbItems.Bytes, mbDamage.Bytes, mbValues.Bytes);
        }

        public BT1ItemList()
        {
            InitBT1InternalList();
        }

        private void InitBT1InternalList()
        {
            Items = SetFromBytes(Global.DecompressBytes(Properties.Resources.BT1_Item_List));
        }

        public override bool InitInternalList() 
        {
            Items = SetFromBytes(GetInternalBytes());
            return true;
        }
    }

    public class BT1ShopInventory : ShopInventory
    {
        public static List<ShopItem> Items = null;

        public override IEnumerable<ShopItem> AllItems { get { return Items; } }

        public BT1ShopInventory(byte[] bytes, int offset = 0)
        {
            if (bytes == null)
                return;

            Items = new List<ShopItem>(bytes.Length);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0)
                    return;
                if (bytes[i] >= BT1.Items.Count)
                {
                    Items = null;
                    return;
                }

                Items.Add(new ShopItem(Global.BT1.GetClonedItem(bytes[i]), i, -1));
            }
        }
    }
}
