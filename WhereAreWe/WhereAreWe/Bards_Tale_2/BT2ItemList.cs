using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum BT2ItemIndex
    {
        None = 0,
        Torch,
        Lamp,
        Broadsword,
        ShortSword,
        Dagger,
        WarAxe,
        Halbard,
        LongBow,
        Staff,
        Buckler,
        TowerShield,
        LeatherArmor,
        ChainMail,
        ScaleArmor,
        PlateArmor,
        Robes,
        Helm,
        LeatherGloves,
        Gauntlets,
        Mandolin,
        Spear,
        Arrows,
        MithrilSword,
        MithrilShield,
        MithrilChain,
        MithrilScale,
        GiantFigurine,
        Bracers6,
        Bardsword,
        ColdHorn,
        LightWand,
        MithrilDagger,
        MithrilHelm,
        MithrilGloves,
        MithrilAxe,
        Shuriken,
        MithrilPlate,
        MoltenFigurine,
        SpellSpear,
        ShieldRing,
        FinsFlute,
        KaelsAxe,
        MithrilArrows,
        Dayblade,
        ShieldStaff,
        ElfCloak,
        Hawkblade,
        AdamantSword,
        AdamantShield,
        AdamantHelm,
        AdamantGloves,
        Pureblade,
        Boomerang,
        AlisCarpet,
        Luckshield,
        BulldozerFigurine,
        AdamantChain,
        DeathStars,
        AdamantPlate,
        Bracers4,
        SlayerFigurine,
        PureShield,
        MageStaff,
        WarStaff,
        ThiefDagger,
        SoulMace,
        OgreWand,
        KatosBracer,
        SorcererStaff,
        GaltsFlute,
        FrostHorn,
        AgsArrows,
        DiamondShield,
        BardBow,
        DiamondHelm,
        ElfBoots,
        VanFigurine,
        Conjurstaff,
        StaffOfLor,
        RingOfReturn,
        PowerStaff,
        BreathRing,
        DragonShield,
        DiamondPlate,
        Wargloves,
        WizHelm,
        DragonWand,
        Deathring,
        CrystalSword,
        SpeedBoots,
        FlameHorn,
        ZenArrows,
        DrumsOfDeath,
        PipesOfPan,
        RingOfPower,
        SongAxe,
        TrickBrick,
        DragonFigurine,
        MastermageFigurine,
        TrollRing,
        AramsKnife,
        AngrasEye,
        HerbFigurine,
        MasterWand,
        BrothersFigurine,
        Dynamite,
        ThorsHammer,
        Stoneblade,
        Grenade,
        MasterKey,
        NospenRing,
        TorchEx,
        SwordOfZar,
        Vial,
        ItemOfK,
        TheRing,
        TroyP,
        DaggerEx,
        WandSegment1,
        WandSegment2,
        WandSegment3,
        WandSegment4,
        WandSegment5,
        WandSegment6,
        WandSegment7,
        TheScepter,
        SpectreSnare,
        Last
    }

    public enum BT2EquipEffect
    {
        None = 0,
        HPRegen = 1,
        SPRegen = 2,
        NegateSpinner = 3,
        HalfSP = 4,
        UnlimitedSongs = 5,
        ImproveSavingThrows = 6,
        ImproveRun = 7,
        ImproveHide = 8,
        MonsterLoyalty = 9,
        ProtectBreath = 10,
        UseArrows = 13,
        Vial = 14,
        OpenGates = 15,
        UnlockOscons = 16,
        Shoot = 17,
        Segment1 = 0x20,
        Segment2 = 0x21,
        Segment3 = 0xA2,
        Segment4 = 0x23,
        Segment5 = 0x24,
        Segment6 = 0x25,
        Segment7 = 0x26,
        HighBit = 0x80,
        HPRegen2 = 0x81,
        Scepter = 0xA7
    }

    public enum BT2ItemEffect
    {
        None = 0,
        ShortLight = 96,
        MediumLight = 97,
        Arrow30 = 98,
        Spear = 99,
        Axe = 100,
        Throw30 = 101,
        Throw40 = 102,
        Arrow50 = 103,
        Throw40Unlimited = 104,
        Throw60 = 105,
        Arrow90 = 106,
        Throw70UnlimitedHPRegen = 107,
        Arrow70 = 108,
        Throw80Unlimited1 = 109,
        Throw80Unlimited2 = 110,
        Throw90Unlimited = 111,
        SummonGiant = 112,
        SummonMoltenMan = 113,
        SummonBulldozer = 114,
        SummonSlayer = 115,
        SummonVanquisher = 116,
        SummonBlastDragon = 117,
        SummonMasterMage = 118,
        SummonHerb = 119,
        Last
    }

    public class BT2Item : BTItem
    {
        public int Charges;
        public BT2ItemEffect ItemEffect;
        public BT2SpellIndex Spell;
        public override int SpellIndex { get { return (int)Spell; } }
        public override int EffectIndex { get { return (int)ItemEffect; } }
        public BT2EquipEffect EquipEffect;

        public BT2Item(int index, byte[] bytes, int offset = 0)
        {
            SetBT2Bytes(index, bytes, offset);
        }

        public override GameNames Game { get { return GameNames.BardsTale2; } }
        public override int Index { get { return m_index; } set { m_index = value; } }
        public override BTItem CreateItem(int index, byte[] bytes, int offset = 0) { return new BT2Item(index, bytes, offset); }
        public BT2ItemIndex ItemIndex { get { return (BT2ItemIndex)m_index; } set { m_index = (int)value; } }
        public override int ChargesCurrent { get { return Charges; } set { Charges = value; } }
        public override bool CanEquip { get { return true; } }

        public override Item Clone()
        {
            return new BT2Item((int)ItemIndex, GetBytes());
        }

        public override void SetBytes(int index, byte[] bytes, int offset = 0) { SetBT2Bytes(index, bytes, offset); }

        public void SetBT2Bytes(int index, byte[] bytes, int offset = 0)
        {
            ItemIndex = (BT2ItemIndex)index;
            m_strName = GetName((BT2ItemIndex)index);
            BTType = (BTItemType)(bytes[1] & 0x0f);
            WeaponEffect = (BTAttackEffect)((bytes[1] & 0xf0) >> 4);
            int iFaceIndex = (bytes[4] & 0xf0) >> 4;
            if (bytes[4] == 0 && !base.IsWeapon)
                Damage = DamageDice.Zero;
            else
                Damage = new DamageDice(iFaceIndex == 0 ? 4 : iFaceIndex * 4, (bytes[4] & 0x0f) + 1, (bytes[0] & 0xf0) >> 4);
            m_value = ((bytes[5] & 0xf8) >> 3) * (int)(Math.Pow(10, bytes[5] & 0x07));
            ItemEffect = GetItemEffect(bytes[3]);
            Spell = GetItemSpell(bytes[3]);
            Usable = GetUsableBy(bytes[2]);
            AC = (bytes[0] & 0x0f);
            Identified = true;
            if (bytes.Length > 6)
                Charges = bytes[6];
            else
                Charges = 255;
            if (bytes.Length > 7)
                EquipEffect = (BT2EquipEffect)bytes[7];
            else
                EquipEffect = BT2EquipEffect.None;
            WhereEquipped = EquipLocation.None;
        }

        public override byte[] GetBytes()
        {
            return new byte[]
            {
                (byte) (AC | (Damage.Bonus << 4)),
                (byte) ((int) BTType | ((int) WeaponEffect << 4)),
                GetUsableByte(Usable),
                GetEffectByte(ItemEffect, Spell),
                IsWeapon ? (byte) ((Damage.Quantity - 1) | ((Damage.Faces <= 4 ? 0 : Damage.Faces / 4) << 4)) : (byte) 0,
                GetValueByte((int) Value),
                (byte) Charges,
                (byte) EquipEffect
            };
        }

        public static byte GetEffectByte(BT2ItemEffect effect, BT2SpellIndex spell)
        {
            if (effect >= BT2ItemEffect.ShortLight && effect < BT2ItemEffect.Last)
                return (byte)effect;

            if (spell > BT2SpellIndex.MageFlame && spell < BT2SpellIndex.Last)
                return (byte)(spell - 1);

            return 0;
        }

        public static BT2ItemEffect GetItemEffect(int index)
        {
            if (index >= (int)BT2ItemEffect.ShortLight && index < (int)BT2ItemEffect.Last)
                return (BT2ItemEffect)index;

            return BT2ItemEffect.None;
        }

        public static BT2SpellIndex GetItemSpell(int index)
        {
            if (index > 0 && index < (int) BT2SpellIndex.MangarsMallet)
                return (BT2SpellIndex)index + 1;

            return BT2SpellIndex.None;
        }

        public override string GetName(int index) { return GetName((BT2ItemIndex) index); }

        public static string GetName(BT2ItemIndex index)
        {
            switch (index)
            {
                case BT2ItemIndex.None: return "Empty";
                case BT2ItemIndex.Torch: return "Torch";
                case BT2ItemIndex.Lamp: return "Lamp";
                case BT2ItemIndex.Broadsword: return "Broadsword";
                case BT2ItemIndex.ShortSword: return "Short Sword";
                case BT2ItemIndex.Dagger: return "Dagger";
                case BT2ItemIndex.WarAxe: return "War Axe";
                case BT2ItemIndex.Halbard: return "Halbard";
                case BT2ItemIndex.LongBow: return "Long Bow";
                case BT2ItemIndex.Staff: return "Staff";
                case BT2ItemIndex.Buckler: return "Buckler";
                case BT2ItemIndex.TowerShield: return "Tower Shield";
                case BT2ItemIndex.LeatherArmor: return "Leather Armor";
                case BT2ItemIndex.ChainMail: return "Chain Mail";
                case BT2ItemIndex.ScaleArmor: return "Scale Armor";
                case BT2ItemIndex.PlateArmor: return "Plate Armor";
                case BT2ItemIndex.Robes: return "Robes";
                case BT2ItemIndex.Helm: return "Helm";
                case BT2ItemIndex.LeatherGloves: return "Leather Gloves";
                case BT2ItemIndex.Gauntlets: return "Gauntlets";
                case BT2ItemIndex.Mandolin: return "Mandolin";
                case BT2ItemIndex.Spear: return "Spear";
                case BT2ItemIndex.Arrows: return "Arrows";
                case BT2ItemIndex.MithrilSword: return "Mithril Sword";
                case BT2ItemIndex.MithrilShield: return "Mithril Shield";
                case BT2ItemIndex.MithrilChain: return "Mithril Chain";
                case BT2ItemIndex.MithrilScale: return "Mithril Scale";
                case BT2ItemIndex.GiantFigurine: return "Giant Figurine";
                case BT2ItemIndex.Bracers6: return "Bracers AC6";
                case BT2ItemIndex.Bardsword: return "Bardsword";
                case BT2ItemIndex.ColdHorn: return "Cold Horn";
                case BT2ItemIndex.LightWand: return "Lightwand";
                case BT2ItemIndex.MithrilDagger: return "Mithril Dagger";
                case BT2ItemIndex.MithrilHelm: return "Mithril Helm";
                case BT2ItemIndex.MithrilGloves: return "Mithril Gloves";
                case BT2ItemIndex.MithrilAxe: return "Mithril Axe";
                case BT2ItemIndex.Shuriken: return "Shuriken";
                case BT2ItemIndex.MithrilPlate: return "Mithril Plate";
                case BT2ItemIndex.MoltenFigurine: return "Molten Figurine";
                case BT2ItemIndex.SpellSpear: return "Spell Spear";
                case BT2ItemIndex.ShieldRing: return "Shield Ring";
                case BT2ItemIndex.FinsFlute: return "Fin's Flute";
                case BT2ItemIndex.KaelsAxe: return "Kael's Axe";
                case BT2ItemIndex.MithrilArrows: return "Mithril Arrows";
                case BT2ItemIndex.Dayblade: return "Dayblade";
                case BT2ItemIndex.ShieldStaff: return "Shield Staff";
                case BT2ItemIndex.ElfCloak: return "Elf Cloak";
                case BT2ItemIndex.Hawkblade: return "Hawkblade";
                case BT2ItemIndex.AdamantSword: return "Adamant Sword";
                case BT2ItemIndex.AdamantShield: return "Adamant Shield";
                case BT2ItemIndex.AdamantHelm: return "Adamant Helm";
                case BT2ItemIndex.AdamantGloves: return "Adamant Gloves";
                case BT2ItemIndex.Pureblade: return "Pureblade";
                case BT2ItemIndex.Boomerang: return "Boomerang";
                case BT2ItemIndex.AlisCarpet: return "Ali's Carpet";
                case BT2ItemIndex.Luckshield: return "Luckshield";
                case BT2ItemIndex.BulldozerFigurine: return "Bulldozer Figurine";
                case BT2ItemIndex.AdamantChain: return "Adamant Chain";
                case BT2ItemIndex.DeathStars: return "Death Stars";
                case BT2ItemIndex.AdamantPlate: return "Adamant Plate";
                case BT2ItemIndex.Bracers4: return "Bracers AC4";
                case BT2ItemIndex.SlayerFigurine: return "Slayer Figurine";
                case BT2ItemIndex.PureShield: return "Pure Shield";
                case BT2ItemIndex.MageStaff: return "Mage Staff";
                case BT2ItemIndex.WarStaff: return "War Staff";
                case BT2ItemIndex.ThiefDagger: return "Thief Dagger";
                case BT2ItemIndex.SoulMace: return "Soul Mace";
                case BT2ItemIndex.OgreWand: return "Ogrewand";
                case BT2ItemIndex.KatosBracer: return "Kato's Bracer";
                case BT2ItemIndex.SorcererStaff: return "Sorcerstaff";
                case BT2ItemIndex.GaltsFlute: return "Galt's Flute";
                case BT2ItemIndex.FrostHorn: return "Frost Horn";
                case BT2ItemIndex.AgsArrows: return "Ag's Arrows";
                case BT2ItemIndex.DiamondShield: return "Diamond Shield";
                case BT2ItemIndex.BardBow: return "Bard Bow";
                case BT2ItemIndex.DiamondHelm: return "Diamond Helm";
                case BT2ItemIndex.ElfBoots: return "Elf Boots";
                case BT2ItemIndex.VanFigurine: return "Van Figurine";
                case BT2ItemIndex.Conjurstaff: return "Conjurstaff";
                case BT2ItemIndex.StaffOfLor: return "Staff of Lor";
                case BT2ItemIndex.RingOfReturn: return "Ring of Return";
                case BT2ItemIndex.PowerStaff: return "Powerstaff";
                case BT2ItemIndex.BreathRing: return "Breathring";
                case BT2ItemIndex.DragonShield: return "Dragonshield";
                case BT2ItemIndex.DiamondPlate: return "Diamond Plate";
                case BT2ItemIndex.Wargloves: return "Wargloves";
                case BT2ItemIndex.WizHelm: return "Wizhelm";
                case BT2ItemIndex.DragonWand: return "Dragonwand";
                case BT2ItemIndex.Deathring: return "Deathring";
                case BT2ItemIndex.CrystalSword: return "Crystal Sword";
                case BT2ItemIndex.SpeedBoots: return "Speedboots";
                case BT2ItemIndex.FlameHorn: return "Flame Horn";
                case BT2ItemIndex.ZenArrows: return "Zen Arrows";
                case BT2ItemIndex.DrumsOfDeath: return "Drums of Death";
                case BT2ItemIndex.PipesOfPan: return "Pipes of Pan";
                case BT2ItemIndex.RingOfPower: return "Ring of Power";
                case BT2ItemIndex.SongAxe: return "Song Axe";
                case BT2ItemIndex.TrickBrick: return "Trick Brick";
                case BT2ItemIndex.DragonFigurine: return "Dragon Figurine";
                case BT2ItemIndex.MastermageFigurine: return "Mastermage Figurine";
                case BT2ItemIndex.TrollRing: return "Troll Ring";
                case BT2ItemIndex.AramsKnife: return "Aram's Knife";
                case BT2ItemIndex.AngrasEye: return "Angra's Eye";
                case BT2ItemIndex.HerbFigurine: return "Herb Figurine";
                case BT2ItemIndex.MasterWand: return "Master Wand";
                case BT2ItemIndex.BrothersFigurine: return "Brothers Figurine";
                case BT2ItemIndex.Dynamite: return "Dynamite";
                case BT2ItemIndex.ThorsHammer: return "Thor's Hammer";
                case BT2ItemIndex.Stoneblade: return "Stoneblade";
                case BT2ItemIndex.Grenade: return "Grenade";
                case BT2ItemIndex.MasterKey: return "Master Key";
                case BT2ItemIndex.NospenRing: return "Nospen Ring";
                case BT2ItemIndex.TorchEx: return "Torch!";
                case BT2ItemIndex.SwordOfZar: return "Sword of Zar";
                case BT2ItemIndex.Vial: return "Vial";
                case BT2ItemIndex.ItemOfK: return "Item of K";
                case BT2ItemIndex.TheRing: return "The Ring";
                case BT2ItemIndex.TroyP: return "Troy P.";
                case BT2ItemIndex.DaggerEx: return "Dagger!";
                case BT2ItemIndex.WandSegment1: return "Scepter Segment 1";
                case BT2ItemIndex.WandSegment2: return "Scepter Segment 2";
                case BT2ItemIndex.WandSegment3: return "Scepter Segment 3";
                case BT2ItemIndex.WandSegment4: return "Scepter Segment 4";
                case BT2ItemIndex.WandSegment5: return "Scepter Segment 5";
                case BT2ItemIndex.WandSegment6: return "Scepter Segment 6";
                case BT2ItemIndex.WandSegment7: return "Scepter Segment 7";
                case BT2ItemIndex.TheScepter: return "The Scepter";
                case BT2ItemIndex.SpectreSnare: return "Spectre Snare";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public override bool PassiveEffect
        {
            get
            {
                if (Spell != BT2SpellIndex.None)
                    return false;

                if (WeaponEffect != BTAttackEffect.None)
                    return true;

                switch (ItemEffect)
                {
                    case BT2ItemEffect.None:
                    case BT2ItemEffect.Arrow30:
                    case BT2ItemEffect.Spear:
                    case BT2ItemEffect.Axe:
                    case BT2ItemEffect.Throw30:
                    case BT2ItemEffect.Throw40:
                    case BT2ItemEffect.Arrow50:
                    case BT2ItemEffect.Throw40Unlimited:
                    case BT2ItemEffect.Throw60:
                    case BT2ItemEffect.Arrow90:
                    case BT2ItemEffect.Throw70UnlimitedHPRegen:
                    case BT2ItemEffect.Arrow70:
                    case BT2ItemEffect.Throw80Unlimited1:
                    case BT2ItemEffect.Throw80Unlimited2:
                    case BT2ItemEffect.Throw90Unlimited:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override bool Trashable
        {
            get
            {
                switch ((BT2ItemIndex)Index)
                {
                    case BT2ItemIndex.MasterKey:
                    case BT2ItemIndex.TorchEx:
                    case BT2ItemIndex.Vial:
                    case BT2ItemIndex.ItemOfK:
                    case BT2ItemIndex.TheRing:
                    case BT2ItemIndex.TroyP:
                    case BT2ItemIndex.DaggerEx:
                    case BT2ItemIndex.WandSegment1:
                    case BT2ItemIndex.WandSegment2:
                    case BT2ItemIndex.WandSegment3:
                    case BT2ItemIndex.WandSegment4:
                    case BT2ItemIndex.WandSegment5:
                    case BT2ItemIndex.WandSegment6:
                    case BT2ItemIndex.WandSegment7:
                    case BT2ItemIndex.TheScepter:
                        return false;
                    default:
                        return true;
                }
            }
        }

        public static string GetEquipEffect(BT2EquipEffect effect)
        {
            switch (effect)
            {
                case BT2EquipEffect.None: return String.Empty;
                case BT2EquipEffect.HPRegen: return "HP Regen";
                case BT2EquipEffect.SPRegen: return "SP Regen";
                case BT2EquipEffect.NegateSpinner: return "Disable spinners";
                case BT2EquipEffect.HalfSP: return "50% SP use";
                case BT2EquipEffect.UnlimitedSongs: return "Unlimited songs";
                case BT2EquipEffect.ImproveSavingThrows: return "Improve saving throws";
                case BT2EquipEffect.ImproveRun: return "Improve run chance";
                case BT2EquipEffect.ImproveHide: return "Improve hide chance";
                case BT2EquipEffect.MonsterLoyalty: return "Ensure monster loyalty";
                case BT2EquipEffect.ProtectBreath: return "Protect from breath";
                case BT2EquipEffect.UseArrows: return "Allows arrows";
                case BT2EquipEffect.HPRegen2: return "HP Regen";
                case BT2EquipEffect.OpenGates: return "Unlocks gates";
                case BT2EquipEffect.UnlockOscons: return "Unlocks Oscon's";
                case BT2EquipEffect.Vial:
                case BT2EquipEffect.Shoot:
                case BT2EquipEffect.Segment1:
                case BT2EquipEffect.Segment2:
                case BT2EquipEffect.Segment3:
                case BT2EquipEffect.Segment4:
                case BT2EquipEffect.Segment5:
                case BT2EquipEffect.Segment6:
                case BT2EquipEffect.Segment7:
                case BT2EquipEffect.Scepter:
                    return "";  // This item's "effect" is for a quest and isn't worth showing on the inventory screen
                case BT2EquipEffect.HighBit: return ""; // Not sure what this is
                default: return String.Format("Unknown({0})", (int)effect);
            }
        }

        public override string EquipEffects { get { return GetEquipEffect(EquipEffect); } }

        public override string UseEffectString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Spell != BT2SpellIndex.None)
                    sb.AppendFormat("Cast {0}; ", BT2.Spells[(int)Spell - 1].Name);
                if (ItemEffect != BT2ItemEffect.None)
                    sb.AppendFormat("{0}; ", GetEffectString(ItemEffect));
                if (WeaponEffect != BTAttackEffect.None)
                    sb.AppendFormat("{0}; ", GetWeaponEffectString(WeaponEffect));
                if (sb.Length > 0 && sb[sb.Length - 2] == ';')
                    sb.Remove(sb.Length - 2, 2);
                return sb.ToString();
            }
        }

        public static string GetEffectString(BT2ItemEffect index)
        {
            switch (index)
            {
                case BT2ItemEffect.None: return String.Empty;
                case BT2ItemEffect.ShortLight: return "Short Light";
                case BT2ItemEffect.MediumLight: return "Medium Light";
                case BT2ItemEffect.Arrow30: return "Shoot 30'";
                case BT2ItemEffect.Spear: return "Throw 20'";
                case BT2ItemEffect.Axe: return "Throw 20'";
                case BT2ItemEffect.Throw30: return "Throw 30'";
                case BT2ItemEffect.Throw40: return "Throw 40'";
                case BT2ItemEffect.Arrow50: return "Shoot 50'";
                case BT2ItemEffect.Throw40Unlimited: return "Throw 40' unl.";
                case BT2ItemEffect.Throw60: return "Throw 60'";
                case BT2ItemEffect.Arrow90: return "Shoot 90'";
                case BT2ItemEffect.Throw70UnlimitedHPRegen: return "Throw 70' unl.";
                case BT2ItemEffect.Arrow70: return "Shoot 70'";
                case BT2ItemEffect.Throw80Unlimited1: return "Throw 80' unl.";
                case BT2ItemEffect.Throw80Unlimited2: return "Throw 80' unl.";
                case BT2ItemEffect.Throw90Unlimited: return "Throw 90' unl.";
                case BT2ItemEffect.SummonGiant: return "Summon Giant";
                case BT2ItemEffect.SummonMoltenMan: return "Summon Molten Man";
                case BT2ItemEffect.SummonBulldozer: return "Summon Bulldozer";
                case BT2ItemEffect.SummonSlayer: return "Summon Slayer";
                case BT2ItemEffect.SummonVanquisher: return "Summon Vanquisher";
                case BT2ItemEffect.SummonBlastDragon: return "Summon Blast Dragon";
                case BT2ItemEffect.SummonMasterMage: return "Summon Master Mage";
                case BT2ItemEffect.SummonHerb: return "Summon Herb";

                default:
                    return String.Format("Unknown({0})", (int)index);
            }
        }
    }

    public class BT2ItemList : BT123ItemList
    {
        public override BTItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return new BT2Item(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.BT2_Item_List); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes mbItemAC = hacker.ReadOffset(BT2.Memory.ItemACBonus, BlockSize);
            MemoryBytes mbTypes = hacker.ReadOffset(BT2.Memory.ItemTypes, BlockSize);
            MemoryBytes mbUsableBy = hacker.ReadOffset(BT2.Memory.ItemUsableBy, BlockSize);
            MemoryBytes mbEffects = hacker.ReadOffset(BT2.Memory.ItemEffects, BlockSize);
            MemoryBytes mbDamage = hacker.ReadOffset(BT2.Memory.ItemDamage, BlockSize);
            MemoryBytes mbValues = hacker.ReadOffset(BT2.Memory.ItemValues, BlockSize);
            MemoryBytes mbCharges = hacker.ReadOffset(BT2.Memory.ItemCharges, BlockSize);
            MemoryBytes mbEquipEffects = hacker.ReadOffset(BT2.Memory.ItemEquipEffect, BlockSize);
            if (mbItemAC == null || mbTypes == null || mbUsableBy == null || mbEffects == null || mbDamage == null || mbValues == null || mbCharges == null || mbEquipEffects == null)
                return GetInternalBytes();
            return Global.Combine(mbItemAC.Bytes, mbTypes.Bytes, mbUsableBy.Bytes, mbEffects.Bytes, mbDamage.Bytes, mbValues.Bytes, mbCharges.Bytes, mbEquipEffects.Bytes);
        }

        public BT2ItemList()
        {
            InitBT2InternalList();
        }

        private void InitBT2InternalList()
        {
            Items = SetFromBytes(Global.DecompressBytes(Properties.Resources.BT2_Item_List));
        }

        public override bool InitInternalList()
        {
            Items = SetFromBytes(GetInternalBytes());
            return true;
        }
    }

    public class BT2ShopInventory : ShopInventory
    {
        public static List<ShopItem> Items = null;

        public override IEnumerable<ShopItem> AllItems { get { return Items; } }

        public BT2ShopInventory(byte[] bytes, int offset = 0)
        {
            if (bytes == null)
                return;

            Items = new List<ShopItem>(bytes.Length);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] >= BT2.Items.Count)
                {
                    Items = null;
                    return;
                }

                Items.Add(new ShopItem(Global.BT2.GetClonedItem(bytes[i]), i, -1));
            }
        }
    }
}
