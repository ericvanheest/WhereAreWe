using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum Wiz2ItemIndex
    {
        Unknown = -1,
        BrokenItem = 0,
        LongSword,
        ShortSword,
        AnointedMace,
        AnointedFlail,
        Staff,
        Dagger,
        SmallShield,
        LargeShield,
        Robes,
        LeatherArmor,
        ChainMail,
        BreastPlate,
        PlateMail,
        Helm,
        PotionOfDios,
        PotionOfLatumofis,
        LongSwordPlus1,
        ShortSwordPlus1,
        MacePlus1,
        StaffOfMogref,
        ScrollOfKantino,
        LeatherPlus1,
        ChainMailPlus1,
        PlateMailPlus1,
        ShieldPlus1,
        BreastPlatePlus1,
        ScrollOfBadios1,
        ScrollOfHalito,
        LongSwordMinus1,
        ShortSwordMinus1,
        MaceMinus1,
        StaffPlus2,
        DragonSlayer,
        HelmPlus1,
        LeatherMinus1,
        ChainMinus1,
        BreastPlateMinus1,
        ShieldMinus1,
        JeweledAmulet,
        ScrollOfBadios2,
        PotionOfSopic,
        LongSwordPlus2,
        ShortSwordPlus2,
        MacePlus2,
        ScrollOfLomilwa,
        ScrollOfDilto,
        GlovesOfCopper,
        LeatherPlus2,
        ChainPlus2,
        PlateMailPlus2,
        ShieldPlus2,
        HelmPlus2,
        PotionOfDial,
        RingOfPorfic,
        WereSlayer,
        MageMasher,
        MaceProPoison,
        StaffOfMontino,
        BladeCusinart,
        AmuletOfManifo,
        RodOfFlame,
        EvilChainPlus2,
        NeutPMinusMailPlus2,
        EvilShieldPlus3,
        AmuletOfMakanito,
        HelmOfMalor,
        ScrollOfBadial,
        ShortSwordMinus2,
        DaggerPlus2,
        MaceMinus2,
        StaffMinus2,
        DaggerOfSpeed,
        RobeOfCurses,
        LeatherMinus2,
        ChainMinus2,
        BreastPlateMinus2,
        ShieldMinus2,
        CursedHelmet,
        BreastPlatePlus2,
        GlovesOfSilver,
        EvilSwordPlus3,
        EvilShortSwordPlus3,
        DaggerOfThieves,
        BreastPlatePlus3,
        LordsGarb,
        MurasamaBlade,
        Shurikens,
        ColdChainMail,
        EvilPlatePlus3,
        ShieldPlus3,
        RingOfHealing,
        PriestsRing,
        DeadlyRing,
        RodOfRaising,
        AmuletOfCover,
        RobePlus3,
        WinterMittens,
        MagicCharms,
        StaffOfLight,
        LongSwordPlus5,
        SwordOfSwings,
        PriestPuncher,
        PriestsMace,
        ShortSwordOfSwings,
        RingOfFire,
        CursedPlatePlus1,
        PlateMailPlus5,
        StaffOfCuring,
        RingOfLife,
        MetamorphRing,
        GraniteStone,
        DreamersStone,
        DamienStone,
        WandOfMages,
        CoinOfPower,
        StoneOfYouth,
        MindStone,
        StoneOfPiety,
        BlarneyStone,
        AmuletOfSkill,
        AmuletOfSkill2,
        WandOfMages2,
        CoinOfPower2,
        StaffOfGnilda,
        Hrathnir,
        KodsHelm,
        KodsShield,
        KodsGauntlets,
        KodsArmor, 
        Last
    }

    public class Wiz2Item : WizItem
    {
        public Wiz2Item(int index, byte[] bytes, int offset = 0)
        {
            SetBytes(index, bytes, offset);
        }

        public override GameNames Game { get { return GameNames.Wizardry2; } }
        public override int Index { get { return m_index; } set { m_index = value; } }
        public override WizItem CreateItem(int index, byte[] bytes, int offset = 0) { return new Wiz2Item(index, bytes, offset); }

        public Wiz2ItemIndex ItemIndex { get { return (Wiz2ItemIndex)m_index; } set { m_index = (int)value; } }
        public Wiz2ItemIndex BreaksInto { get { return (Wiz2ItemIndex)m_breaks; } set { m_breaks = (int)value; } }

        public override bool Trashable
        {
            get
            {
                switch (ItemIndex)
                {
                    case Wiz2ItemIndex.StaffOfGnilda:
                    case Wiz2ItemIndex.Hrathnir:
                    case Wiz2ItemIndex.KodsHelm:
                    case Wiz2ItemIndex.KodsShield:
                    case Wiz2ItemIndex.KodsGauntlets:
                    case Wiz2ItemIndex.KodsArmor:
                        return false;
                    default: return true;
                }
            }
        }

        public override string ItemNoun
        {
            get
            {
                switch ((Wiz2ItemIndex)Index)
                {
                    case Wiz2ItemIndex.BrokenItem: return "Broken Item";
                    case Wiz2ItemIndex.AmuletOfManifo:
                    case Wiz2ItemIndex.JeweledAmulet:
                    case Wiz2ItemIndex.AmuletOfCover:
                    case Wiz2ItemIndex.AmuletOfSkill:
                    case Wiz2ItemIndex.AmuletOfSkill2:
                    case Wiz2ItemIndex.AmuletOfMakanito: return "Amulet";
                    case Wiz2ItemIndex.BreastPlateMinus1:
                    case Wiz2ItemIndex.LeatherPlus2:
                    case Wiz2ItemIndex.ChainPlus2:
                    case Wiz2ItemIndex.LeatherArmor:
                    case Wiz2ItemIndex.ChainMail:
                    case Wiz2ItemIndex.BreastPlate:
                    case Wiz2ItemIndex.PlateMail:
                    case Wiz2ItemIndex.EvilPlatePlus3:
                    case Wiz2ItemIndex.ColdChainMail:
                    case Wiz2ItemIndex.NeutPMinusMailPlus2:
                    case Wiz2ItemIndex.ChainMinus1:
                    case Wiz2ItemIndex.LordsGarb:
                    case Wiz2ItemIndex.BreastPlatePlus3:
                    case Wiz2ItemIndex.PlateMailPlus2:
                    case Wiz2ItemIndex.LeatherMinus1:
                    case Wiz2ItemIndex.LeatherPlus1:
                    case Wiz2ItemIndex.BreastPlatePlus1:
                    case Wiz2ItemIndex.BreastPlatePlus2:
                    case Wiz2ItemIndex.PlateMailPlus1:
                    case Wiz2ItemIndex.ChainMailPlus1:
                    case Wiz2ItemIndex.BreastPlateMinus2:
                    case Wiz2ItemIndex.ChainMinus2:
                    case Wiz2ItemIndex.CursedPlatePlus1:
                    case Wiz2ItemIndex.PlateMailPlus5:
                    case Wiz2ItemIndex.KodsArmor:
                    case Wiz2ItemIndex.LeatherMinus2: return "Armor";
                    case Wiz2ItemIndex.EvilChainPlus2: return "Chain";
                    case Wiz2ItemIndex.Robes:
                    case Wiz2ItemIndex.RobePlus3:
                    case Wiz2ItemIndex.RobeOfCurses: return "Clothing";
                    case Wiz2ItemIndex.DaggerPlus2:
                    case Wiz2ItemIndex.DaggerOfThieves:
                    case Wiz2ItemIndex.Dagger:
                    case Wiz2ItemIndex.DaggerOfSpeed: return "Dagger";
                    case Wiz2ItemIndex.GlovesOfSilver:
                    case Wiz2ItemIndex.GlovesOfCopper: return "Gloves";
                    case Wiz2ItemIndex.Helm:
                    case Wiz2ItemIndex.CursedHelmet:
                    case Wiz2ItemIndex.HelmOfMalor:
                    case Wiz2ItemIndex.HelmPlus1:
                    case Wiz2ItemIndex.KodsHelm:
                    case Wiz2ItemIndex.HelmPlus2: return "Helm";
                    case Wiz2ItemIndex.PotionOfLatumofis:
                    case Wiz2ItemIndex.PotionOfSopic:
                    case Wiz2ItemIndex.PotionOfDial:
                    case Wiz2ItemIndex.PotionOfDios: return "Potion";
                    case Wiz2ItemIndex.PriestsRing:
                    case Wiz2ItemIndex.RingOfPorfic:
                    case Wiz2ItemIndex.DeadlyRing:
                    case Wiz2ItemIndex.RingOfFire:
                    case Wiz2ItemIndex.RingOfLife:
                    case Wiz2ItemIndex.MetamorphRing:
                    case Wiz2ItemIndex.RingOfHealing: return "Ring";
                    case Wiz2ItemIndex.RodOfFlame: return "Rod";
                    case Wiz2ItemIndex.ScrollOfLomilwa:
                    case Wiz2ItemIndex.ScrollOfHalito:
                    case Wiz2ItemIndex.ScrollOfBadial:
                    case Wiz2ItemIndex.ScrollOfKantino:
                    case Wiz2ItemIndex.ScrollOfDilto:
                    case Wiz2ItemIndex.ScrollOfBadios1:
                    case Wiz2ItemIndex.ScrollOfBadios2: return "Scroll";
                    case Wiz2ItemIndex.ShieldMinus1:
                    case Wiz2ItemIndex.ShieldPlus3:
                    case Wiz2ItemIndex.EvilShieldPlus3:
                    case Wiz2ItemIndex.ShieldPlus1:
                    case Wiz2ItemIndex.ShieldMinus2:
                    case Wiz2ItemIndex.SmallShield:
                    case Wiz2ItemIndex.LargeShield:
                    case Wiz2ItemIndex.KodsShield:
                    case Wiz2ItemIndex.ShieldPlus2: return "Shield";
                    case Wiz2ItemIndex.MaceMinus1:
                    case Wiz2ItemIndex.StaffOfMontino:
                    case Wiz2ItemIndex.Staff:
                    case Wiz2ItemIndex.MacePlus2:
                    case Wiz2ItemIndex.MaceProPoison:
                    case Wiz2ItemIndex.MaceMinus2:
                    case Wiz2ItemIndex.AnointedFlail:
                    case Wiz2ItemIndex.StaffMinus2:
                    case Wiz2ItemIndex.AnointedMace:
                    case Wiz2ItemIndex.MacePlus1:
                    case Wiz2ItemIndex.StaffPlus2:
                    case Wiz2ItemIndex.StaffOfLight:
                    case Wiz2ItemIndex.StaffOfCuring:
                    case Wiz2ItemIndex.RodOfRaising:
                    case Wiz2ItemIndex.PriestsMace:
                    case Wiz2ItemIndex.WandOfMages:
                    case Wiz2ItemIndex.WandOfMages2:
                    case Wiz2ItemIndex.StaffOfMogref: return "Staff";
                    case Wiz2ItemIndex.Shurikens: return "Stars";
                    case Wiz2ItemIndex.EvilShortSwordPlus3:
                    case Wiz2ItemIndex.MurasamaBlade:
                    case Wiz2ItemIndex.ShortSwordPlus1:
                    case Wiz2ItemIndex.LongSwordMinus1:
                    case Wiz2ItemIndex.ShortSwordMinus1:
                    case Wiz2ItemIndex.DragonSlayer:
                    case Wiz2ItemIndex.ShortSwordMinus2:
                    case Wiz2ItemIndex.LongSwordPlus1:
                    case Wiz2ItemIndex.BladeCusinart:
                    case Wiz2ItemIndex.LongSwordPlus2:
                    case Wiz2ItemIndex.EvilSwordPlus3:
                    case Wiz2ItemIndex.ShortSwordPlus2:
                    case Wiz2ItemIndex.MageMasher:
                    case Wiz2ItemIndex.WereSlayer:
                    case Wiz2ItemIndex.ShortSword:
                    case Wiz2ItemIndex.LongSword:
                    case Wiz2ItemIndex.LongSwordPlus5:
                    case Wiz2ItemIndex.SwordOfSwings:
                    case Wiz2ItemIndex.PriestPuncher:
                    case Wiz2ItemIndex.ShortSwordOfSwings:
                    case Wiz2ItemIndex.Hrathnir: return "Sword";
                    case Wiz2ItemIndex.WinterMittens:
                    case Wiz2ItemIndex.KodsGauntlets: return "Gauntlets";
                    case Wiz2ItemIndex.MagicCharms: return "Necklace";
                    case Wiz2ItemIndex.GraniteStone:
                    case Wiz2ItemIndex.DreamersStone:
                    case Wiz2ItemIndex.DamienStone:
                    case Wiz2ItemIndex.StoneOfYouth:
                    case Wiz2ItemIndex.MindStone:
                    case Wiz2ItemIndex.StoneOfPiety:
                    case Wiz2ItemIndex.BlarneyStone: return "Stone";
                    case Wiz2ItemIndex.CoinOfPower2:
                    case Wiz2ItemIndex.CoinOfPower: return "Coin";
                    case Wiz2ItemIndex.StaffOfGnilda: return "Staff of Gnilda";
                    default: return String.Format("Unknown Item");
                }
            }
        }

        public override string GetName(int index) { return GetName((Wiz2ItemIndex) index); }

        public static string GetName(Wiz2ItemIndex index)
        {
            switch (index)
            {
                case Wiz2ItemIndex.BrokenItem: return "Broken Item";
                case Wiz2ItemIndex.LongSword: return "Long Sword";
                case Wiz2ItemIndex.ShortSword: return "Short Sword";
                case Wiz2ItemIndex.AnointedMace: return "Anointed Mace";
                case Wiz2ItemIndex.AnointedFlail: return "Anointed Flail";
                case Wiz2ItemIndex.Staff: return "Staff";
                case Wiz2ItemIndex.Dagger: return "Dagger";
                case Wiz2ItemIndex.SmallShield: return "Small Shield";
                case Wiz2ItemIndex.LargeShield: return "Large Shield";
                case Wiz2ItemIndex.Robes: return "Robes";
                case Wiz2ItemIndex.LeatherArmor: return "Leather Armor";
                case Wiz2ItemIndex.ChainMail: return "Chain Mail";
                case Wiz2ItemIndex.BreastPlate: return "Breast Plate";
                case Wiz2ItemIndex.PlateMail: return "Plate Mail";
                case Wiz2ItemIndex.Helm: return "Helm";
                case Wiz2ItemIndex.PotionOfDios: return "Potion of Dios";
                case Wiz2ItemIndex.PotionOfLatumofis: return "Potion of Latumofis";
                case Wiz2ItemIndex.LongSwordPlus1: return "Long Sword +1";
                case Wiz2ItemIndex.ShortSwordPlus1: return "Short Sword +1";
                case Wiz2ItemIndex.MacePlus1: return "Mace +1";
                case Wiz2ItemIndex.StaffOfMogref: return "Staff of Mogref";
                case Wiz2ItemIndex.ScrollOfKantino: return "Scroll of Katino";
                case Wiz2ItemIndex.LeatherPlus1: return "Leather Armor +1";
                case Wiz2ItemIndex.ChainMailPlus1: return "Chain Mail +1";
                case Wiz2ItemIndex.PlateMailPlus1: return "Plate Mail +1";
                case Wiz2ItemIndex.ShieldPlus1: return "Shield +1";
                case Wiz2ItemIndex.BreastPlatePlus1: return "Breast Plate +1";
                case Wiz2ItemIndex.ScrollOfBadios1: return "Scroll of Badios";
                case Wiz2ItemIndex.ScrollOfHalito: return "Scroll of Halito";
                case Wiz2ItemIndex.LongSwordMinus1: return "Long Sword -1";
                case Wiz2ItemIndex.ShortSwordMinus1: return "Short Sword -1";
                case Wiz2ItemIndex.MaceMinus1: return "Mace -1";
                case Wiz2ItemIndex.StaffPlus2: return "Staff +2";
                case Wiz2ItemIndex.DragonSlayer: return "Slayer of Dragons";
                case Wiz2ItemIndex.HelmPlus1: return "Helm +1";
                case Wiz2ItemIndex.LeatherMinus1: return "Leather Armor -1";
                case Wiz2ItemIndex.ChainMinus1: return "Chain Mail -1";
                case Wiz2ItemIndex.BreastPlateMinus1: return "Breast Plate -1";
                case Wiz2ItemIndex.ShieldMinus1: return "Shield -1";
                case Wiz2ItemIndex.JeweledAmulet: return "Amulet of Jewels";
                case Wiz2ItemIndex.ScrollOfBadios2: return "Scroll of Badios";
                case Wiz2ItemIndex.PotionOfSopic: return "Potion of Sopic";
                case Wiz2ItemIndex.LongSwordPlus2: return "Long Sword +2";
                case Wiz2ItemIndex.ShortSwordPlus2: return "Short Sword +2";
                case Wiz2ItemIndex.MacePlus2: return "Mace +2";
                case Wiz2ItemIndex.ScrollOfLomilwa: return "Scroll of Lomilwa";
                case Wiz2ItemIndex.ScrollOfDilto: return "Scroll of Dilto";
                case Wiz2ItemIndex.GlovesOfCopper: return "Gloves of Copper";
                case Wiz2ItemIndex.LeatherPlus2: return "Leather Armor +2";
                case Wiz2ItemIndex.ChainPlus2: return "Chain Mail +2";
                case Wiz2ItemIndex.PlateMailPlus2: return "Plate Mail +2";
                case Wiz2ItemIndex.ShieldPlus2: return "Shield +2";
                case Wiz2ItemIndex.HelmPlus2: return "Helm +2";
                case Wiz2ItemIndex.PotionOfDial: return "Potion of Dial";
                case Wiz2ItemIndex.RingOfPorfic: return "Ring of Porfic";
                case Wiz2ItemIndex.WereSlayer: return "Were Slayer";
                case Wiz2ItemIndex.MageMasher: return "Mage Masher";
                case Wiz2ItemIndex.MaceProPoison: return "Mace of Poison";
                case Wiz2ItemIndex.StaffOfMontino: return "Staff of Montino";
                case Wiz2ItemIndex.BladeCusinart: return "Blade Cusinart'";
                case Wiz2ItemIndex.AmuletOfManifo: return "Amulet of Manifo";
                case Wiz2ItemIndex.RodOfFlame: return "Rod of Flame";
                case Wiz2ItemIndex.EvilChainPlus2: return "Evil Chain Mail +2";
                case Wiz2ItemIndex.NeutPMinusMailPlus2: return "Neutral Plate Mail +2";
                case Wiz2ItemIndex.EvilShieldPlus3: return "Evil Shield +3";
                case Wiz2ItemIndex.AmuletOfMakanito: return "Amulet of Makanito";
                case Wiz2ItemIndex.HelmOfMalor: return "Helm of Malor";
                case Wiz2ItemIndex.ScrollOfBadial: return "Scroll of Badial";
                case Wiz2ItemIndex.ShortSwordMinus2: return "Short Sword -2";
                case Wiz2ItemIndex.DaggerPlus2: return "Dagger +2";
                case Wiz2ItemIndex.MaceMinus2: return "Mace -2";
                case Wiz2ItemIndex.StaffMinus2: return "Staff -2";
                case Wiz2ItemIndex.DaggerOfSpeed: return "Dagger of Speed";
                case Wiz2ItemIndex.RobeOfCurses: return "Robe of Curses";
                case Wiz2ItemIndex.LeatherMinus2: return "Leather Armor -2";
                case Wiz2ItemIndex.ChainMinus2: return "Chain Mail -2";
                case Wiz2ItemIndex.BreastPlateMinus2: return "Breast Plate -2";
                case Wiz2ItemIndex.ShieldMinus2: return "Shield -2";
                case Wiz2ItemIndex.CursedHelmet: return "Helm of Curses";
                case Wiz2ItemIndex.BreastPlatePlus2: return "Breast Plate +2";
                case Wiz2ItemIndex.GlovesOfSilver: return "Gloves of Silver";
                case Wiz2ItemIndex.EvilSwordPlus3: return "Evil Sword +3";
                case Wiz2ItemIndex.EvilShortSwordPlus3: return "Evil Short Sword +3";
                case Wiz2ItemIndex.DaggerOfThieves: return "Dagger of Thieves";
                case Wiz2ItemIndex.BreastPlatePlus3: return "Breast Plate +3";
                case Wiz2ItemIndex.LordsGarb: return "Garb of Lords";
                case Wiz2ItemIndex.MurasamaBlade: return "Muramasa Blade!";
                case Wiz2ItemIndex.Shurikens: return "Shurikens";
                case Wiz2ItemIndex.ColdChainMail: return "Cold Chain Mail";
                case Wiz2ItemIndex.EvilPlatePlus3: return "Evil Plate +3";
                case Wiz2ItemIndex.ShieldPlus3: return "Shield +3";
                case Wiz2ItemIndex.RingOfHealing: return "Ring of Healing";
                case Wiz2ItemIndex.PriestsRing: return "Priest's Ring";
                case Wiz2ItemIndex.DeadlyRing: return "Ring of Death";
                case Wiz2ItemIndex.RodOfRaising: return "Rod of Raising";
                case Wiz2ItemIndex.AmuletOfCover: return "Amulet of Cover";
                case Wiz2ItemIndex.RobePlus3: return "Robe +3";
                case Wiz2ItemIndex.WinterMittens: return "Winter Mittens";
                case Wiz2ItemIndex.MagicCharms: return "Magic Charms";
                case Wiz2ItemIndex.StaffOfLight: return "Staff of Light";
                case Wiz2ItemIndex.LongSwordPlus5: return "Long Sword +5";
                case Wiz2ItemIndex.SwordOfSwings: return "Sword of Swings";
                case Wiz2ItemIndex.PriestPuncher: return "Priest Puncher";
                case Wiz2ItemIndex.PriestsMace: return "Priest's Mace";
                case Wiz2ItemIndex.ShortSwordOfSwings: return "Short Sword of Swings";
                case Wiz2ItemIndex.RingOfFire: return "Ring of Fire";
                case Wiz2ItemIndex.CursedPlatePlus1: return "Cursed Plate +1";
                case Wiz2ItemIndex.PlateMailPlus5: return "Plate +5";
                case Wiz2ItemIndex.StaffOfCuring: return "Staff of Curing";
                case Wiz2ItemIndex.RingOfLife: return "Ring of Life";
                case Wiz2ItemIndex.MetamorphRing: return "Metamorph Ring";
                case Wiz2ItemIndex.GraniteStone: return "Granite Stone";
                case Wiz2ItemIndex.DreamersStone: return "Dreamer's Stone";
                case Wiz2ItemIndex.DamienStone: return "Damien Stone";
                case Wiz2ItemIndex.WandOfMages: return "Wand of Mages A";
                case Wiz2ItemIndex.CoinOfPower: return "Coin of Power A";
                case Wiz2ItemIndex.StoneOfYouth: return "Stone of Youth";
                case Wiz2ItemIndex.MindStone: return "Mind Stone";
                case Wiz2ItemIndex.StoneOfPiety: return "Stone of Piety";
                case Wiz2ItemIndex.BlarneyStone: return "Blarney Stone";
                case Wiz2ItemIndex.AmuletOfSkill: return "Amulet of Skill A";
                case Wiz2ItemIndex.AmuletOfSkill2: return "Amulet of Skill B";
                case Wiz2ItemIndex.WandOfMages2: return "Wand of Mages B";
                case Wiz2ItemIndex.CoinOfPower2: return "Coin of Power B";
                case Wiz2ItemIndex.StaffOfGnilda: return "Staff of Gnilda";
                case Wiz2ItemIndex.Hrathnir: return "Hrathnir";
                case Wiz2ItemIndex.KodsHelm: return "Kod's Helm";
                case Wiz2ItemIndex.KodsShield: return "Kod's Shield";
                case Wiz2ItemIndex.KodsGauntlets: return "Kod's Gauntlets";
                case Wiz2ItemIndex.KodsArmor: return "Kod's Armor";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }
    }

    public class Wiz2ItemList : Wiz123ItemList
    {
        public override WizItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return new Wiz2Item(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_2_Item_List_mem); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes bytes = hacker.ReadOffset(Wiz2.Memory.ItemList, 6052);
            if (bytes == null)
                return null;
            return bytes.Bytes;
        }

        public Wiz2ItemList()
        {
            InitInternalList();
        }
    }

    public class Wiz2SearchResults : Wiz123SearchResults
    {
        public Wiz2SearchResults(int iRewardIndex)
        {
            RewardIndex = iRewardIndex;
        }

        public override List<WizTreasure> Treasures { get { return Wiz2.Treasures; } }
    }
}
