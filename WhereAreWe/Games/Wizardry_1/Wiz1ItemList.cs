using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum Wiz1ItemIndex
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
        AmuletOfWerdna,
        StatuetteOfBear,
        StatuetteOfFrog,
        KeyOfBronze,
        KeyOfSilver,
        KeyOfGold,
        BlueRibbon,
        Last
    }

    public class Wiz1Item : WizItem
    {
        public Wiz1Item(int index, byte[] bytes, int offset = 0)
        {
            SetBytes(index, bytes, offset);
        }

        public override string MaterialString   // overloaded to be "protects from"
        {
            get
            {
                if (ItemIndex == Wiz1ItemIndex.AmuletOfWerdna)
                    return "<All>";
                return base.MaterialString;
            }
        }

        public override string ResistString
        {
            get
            {
                if (ItemIndex == Wiz1ItemIndex.AmuletOfWerdna)
                    return "<All>";
                return base.ResistString;
            }
        }

        public override GameNames Game { get { return GameNames.Wizardry1; } }
        public override int Index { get { return m_index; } set { m_index = value; } }
        public override WizItem CreateItem(int index, byte[] bytes, int offset = 0) { return new Wiz1Item(index, bytes, offset); }

        public Wiz1ItemIndex ItemIndex { get { return (Wiz1ItemIndex)m_index; } set { m_index = (int)value; } }
        public Wiz1ItemIndex BreaksInto { get { return (Wiz1ItemIndex)m_breaks; } set { m_breaks = (int)value; } }

        public override bool Trashable
        {
            get
            {
                switch ((Wiz1ItemIndex)Index)
                {
                    case Wiz1ItemIndex.KeyOfBronze:
                    case Wiz1ItemIndex.KeyOfGold:
                    case Wiz1ItemIndex.KeyOfSilver:
                    case Wiz1ItemIndex.StatuetteOfBear:
                    case Wiz1ItemIndex.StatuetteOfFrog:
                    case Wiz1ItemIndex.AmuletOfWerdna:
                    case Wiz1ItemIndex.BlueRibbon:
                        return false;
                    default:
                        return true;
                }
            }
        }

        public override string ItemNoun
        {
            get
            {
                switch ((Wiz1ItemIndex)Index)
                {
                    case Wiz1ItemIndex.BrokenItem: return "Broken Item";
                    case Wiz1ItemIndex.AmuletOfWerdna:
                    case Wiz1ItemIndex.AmuletOfManifo:
                    case Wiz1ItemIndex.JeweledAmulet:
                    case Wiz1ItemIndex.AmuletOfMakanito: return "Amulet";
                    case Wiz1ItemIndex.BreastPlateMinus1:
                    case Wiz1ItemIndex.LeatherPlus2:
                    case Wiz1ItemIndex.ChainPlus2:
                    case Wiz1ItemIndex.LeatherArmor:
                    case Wiz1ItemIndex.ChainMail:
                    case Wiz1ItemIndex.BreastPlate:
                    case Wiz1ItemIndex.PlateMail:
                    case Wiz1ItemIndex.EvilPlatePlus3:
                    case Wiz1ItemIndex.ColdChainMail:
                    case Wiz1ItemIndex.NeutPMinusMailPlus2:
                    case Wiz1ItemIndex.ChainMinus1:
                    case Wiz1ItemIndex.LordsGarb:
                    case Wiz1ItemIndex.BreastPlatePlus3:
                    case Wiz1ItemIndex.PlateMailPlus2:
                    case Wiz1ItemIndex.LeatherMinus1:
                    case Wiz1ItemIndex.LeatherPlus1:
                    case Wiz1ItemIndex.BreastPlatePlus1:
                    case Wiz1ItemIndex.BreastPlatePlus2:
                    case Wiz1ItemIndex.PlateMailPlus1:
                    case Wiz1ItemIndex.ChainMailPlus1:
                    case Wiz1ItemIndex.BreastPlateMinus2:
                    case Wiz1ItemIndex.ChainMinus2:
                    case Wiz1ItemIndex.LeatherMinus2: return "Armor";
                    case Wiz1ItemIndex.EvilChainPlus2: return "Chain";
                    case Wiz1ItemIndex.Robes:
                    case Wiz1ItemIndex.RobeOfCurses: return "Clothing";
                    case Wiz1ItemIndex.DaggerPlus2:
                    case Wiz1ItemIndex.DaggerOfThieves:
                    case Wiz1ItemIndex.Dagger:
                    case Wiz1ItemIndex.DaggerOfSpeed: return "Dagger";
                    case Wiz1ItemIndex.GlovesOfSilver:
                    case Wiz1ItemIndex.GlovesOfCopper: return "Gloves";
                    case Wiz1ItemIndex.Helm:
                    case Wiz1ItemIndex.CursedHelmet:
                    case Wiz1ItemIndex.HelmOfMalor:
                    case Wiz1ItemIndex.HelmPlus1:
                    case Wiz1ItemIndex.HelmPlus2: return "Helm";
                    case Wiz1ItemIndex.KeyOfBronze:
                    case Wiz1ItemIndex.KeyOfSilver:
                    case Wiz1ItemIndex.KeyOfGold: return "Key";
                    case Wiz1ItemIndex.PotionOfLatumofis:
                    case Wiz1ItemIndex.PotionOfSopic:
                    case Wiz1ItemIndex.PotionOfDial:
                    case Wiz1ItemIndex.PotionOfDios: return "Potion";
                    case Wiz1ItemIndex.BlueRibbon: return "Ribbon";
                    case Wiz1ItemIndex.PriestsRing:
                    case Wiz1ItemIndex.RingOfPorfic:
                    case Wiz1ItemIndex.DeadlyRing:
                    case Wiz1ItemIndex.RingOfHealing: return "Ring";
                    case Wiz1ItemIndex.RodOfFlame: return "Rod";
                    case Wiz1ItemIndex.ScrollOfLomilwa:
                    case Wiz1ItemIndex.ScrollOfHalito:
                    case Wiz1ItemIndex.ScrollOfBadial:
                    case Wiz1ItemIndex.ScrollOfKantino:
                    case Wiz1ItemIndex.ScrollOfDilto:
                    case Wiz1ItemIndex.ScrollOfBadios1:
                    case Wiz1ItemIndex.ScrollOfBadios2: return "Scroll";
                    case Wiz1ItemIndex.ShieldMinus1:
                    case Wiz1ItemIndex.ShieldPlus3:
                    case Wiz1ItemIndex.EvilShieldPlus3:
                    case Wiz1ItemIndex.ShieldPlus1:
                    case Wiz1ItemIndex.ShieldMinus2:
                    case Wiz1ItemIndex.SmallShield:
                    case Wiz1ItemIndex.LargeShield:
                    case Wiz1ItemIndex.ShieldPlus2: return "Shield";
                    case Wiz1ItemIndex.MaceMinus1:
                    case Wiz1ItemIndex.StaffOfMontino:
                    case Wiz1ItemIndex.Staff:
                    case Wiz1ItemIndex.MacePlus2:
                    case Wiz1ItemIndex.MaceProPoison:
                    case Wiz1ItemIndex.MaceMinus2:
                    case Wiz1ItemIndex.AnointedFlail:
                    case Wiz1ItemIndex.StaffMinus2:
                    case Wiz1ItemIndex.AnointedMace:
                    case Wiz1ItemIndex.MacePlus1:
                    case Wiz1ItemIndex.StaffPlus2:
                    case Wiz1ItemIndex.StaffOfMogref: return "Staff";
                    case Wiz1ItemIndex.Shurikens: return "Stars";
                    case Wiz1ItemIndex.StatuetteOfFrog:
                    case Wiz1ItemIndex.StatuetteOfBear: return "Statue";
                    case Wiz1ItemIndex.EvilShortSwordPlus3:
                    case Wiz1ItemIndex.MurasamaBlade:
                    case Wiz1ItemIndex.ShortSwordPlus1:
                    case Wiz1ItemIndex.LongSwordMinus1:
                    case Wiz1ItemIndex.ShortSwordMinus1:
                    case Wiz1ItemIndex.DragonSlayer:
                    case Wiz1ItemIndex.ShortSwordMinus2:
                    case Wiz1ItemIndex.LongSwordPlus1:
                    case Wiz1ItemIndex.BladeCusinart:
                    case Wiz1ItemIndex.LongSwordPlus2:
                    case Wiz1ItemIndex.EvilSwordPlus3:
                    case Wiz1ItemIndex.ShortSwordPlus2:
                    case Wiz1ItemIndex.MageMasher:
                    case Wiz1ItemIndex.WereSlayer:
                    case Wiz1ItemIndex.ShortSword:
                    case Wiz1ItemIndex.LongSword: return "Sword";
                    default: return String.Format("Unknown Item");
                }
            }
        }

        public override string GetName(int index) { return GetName((Wiz1ItemIndex) index); }

        public static string GetName(Wiz1ItemIndex index)
        {
            switch (index)
            {
                case Wiz1ItemIndex.BrokenItem: return "Broken Item";
                case Wiz1ItemIndex.LongSword: return "Long Sword";
                case Wiz1ItemIndex.ShortSword: return "Short Sword";
                case Wiz1ItemIndex.AnointedMace: return "Anointed Mace";
                case Wiz1ItemIndex.AnointedFlail: return "Anointed Flail";
                case Wiz1ItemIndex.Staff: return "Staff";
                case Wiz1ItemIndex.Dagger: return "Dagger";
                case Wiz1ItemIndex.SmallShield: return "Small Shield";
                case Wiz1ItemIndex.LargeShield: return "Large Shield";
                case Wiz1ItemIndex.Robes: return "Robes";
                case Wiz1ItemIndex.LeatherArmor: return "Leather Armor";
                case Wiz1ItemIndex.ChainMail: return "Chain Mail";
                case Wiz1ItemIndex.BreastPlate: return "Breast Plate";
                case Wiz1ItemIndex.PlateMail: return "Plate Mail";
                case Wiz1ItemIndex.Helm: return "Helm";
                case Wiz1ItemIndex.PotionOfDios: return "Potion of Dios";
                case Wiz1ItemIndex.PotionOfLatumofis: return "Potion of Latumofis";
                case Wiz1ItemIndex.LongSwordPlus1: return "Long Sword +1";
                case Wiz1ItemIndex.ShortSwordPlus1: return "Short Sword +1";
                case Wiz1ItemIndex.MacePlus1: return "Mace +1";
                case Wiz1ItemIndex.StaffOfMogref: return "Staff of Mogref";
                case Wiz1ItemIndex.ScrollOfKantino: return "Scroll of Katino";
                case Wiz1ItemIndex.LeatherPlus1: return "Leather Armor +1";
                case Wiz1ItemIndex.ChainMailPlus1: return "Chain Mail +1";
                case Wiz1ItemIndex.PlateMailPlus1: return "Plate Mail +1";
                case Wiz1ItemIndex.ShieldPlus1: return "Shield +1";
                case Wiz1ItemIndex.BreastPlatePlus1: return "Breast Plate +1";
                case Wiz1ItemIndex.ScrollOfBadios1: return "Scroll of Badios";
                case Wiz1ItemIndex.ScrollOfHalito: return "Scroll of Halito";
                case Wiz1ItemIndex.LongSwordMinus1: return "Long Sword -1";
                case Wiz1ItemIndex.ShortSwordMinus1: return "Short Sword -1";
                case Wiz1ItemIndex.MaceMinus1: return "Mace -1";
                case Wiz1ItemIndex.StaffPlus2: return "Staff +2";
                case Wiz1ItemIndex.DragonSlayer: return "Slayer of Dragons";
                case Wiz1ItemIndex.HelmPlus1: return "Helm +1";
                case Wiz1ItemIndex.LeatherMinus1: return "Leather Armor -1";
                case Wiz1ItemIndex.ChainMinus1: return "Chain Mail -1";
                case Wiz1ItemIndex.BreastPlateMinus1: return "Breast Plate -1";
                case Wiz1ItemIndex.ShieldMinus1: return "Shield -1";
                case Wiz1ItemIndex.JeweledAmulet: return "Amulet of Jewels";
                case Wiz1ItemIndex.ScrollOfBadios2: return "Scroll of Badios";
                case Wiz1ItemIndex.PotionOfSopic: return "Potion of Sopic";
                case Wiz1ItemIndex.LongSwordPlus2: return "Long Sword +2";
                case Wiz1ItemIndex.ShortSwordPlus2: return "Short Sword +2";
                case Wiz1ItemIndex.MacePlus2: return "Mace +2";
                case Wiz1ItemIndex.ScrollOfLomilwa: return "Scroll of Lomilwa";
                case Wiz1ItemIndex.ScrollOfDilto: return "Scroll of Dilto";
                case Wiz1ItemIndex.GlovesOfCopper: return "Gloves of Copper";
                case Wiz1ItemIndex.LeatherPlus2: return "Leather Armor +2";
                case Wiz1ItemIndex.ChainPlus2: return "Chain Mail +2";
                case Wiz1ItemIndex.PlateMailPlus2: return "Plate Mail +2";
                case Wiz1ItemIndex.ShieldPlus2: return "Shield +2";
                case Wiz1ItemIndex.HelmPlus2: return "Helm +2";
                case Wiz1ItemIndex.PotionOfDial: return "Potion of Dial";
                case Wiz1ItemIndex.RingOfPorfic: return "Ring of Porfic";
                case Wiz1ItemIndex.WereSlayer: return "Were Slayer";
                case Wiz1ItemIndex.MageMasher: return "Mage Masher";
                case Wiz1ItemIndex.MaceProPoison: return "Mace of Poison";
                case Wiz1ItemIndex.StaffOfMontino: return "Staff of Montino";
                case Wiz1ItemIndex.BladeCusinart: return "Blade Cusinart'";
                case Wiz1ItemIndex.AmuletOfManifo: return "Amulet of Manifo";
                case Wiz1ItemIndex.RodOfFlame: return "Rod of Flame";
                case Wiz1ItemIndex.EvilChainPlus2: return "Evil Chain Mail +2";
                case Wiz1ItemIndex.NeutPMinusMailPlus2: return "Neutral Plate Mail +2";
                case Wiz1ItemIndex.EvilShieldPlus3: return "Evil Shield +3";
                case Wiz1ItemIndex.AmuletOfMakanito: return "Amulet of Makanito";
                case Wiz1ItemIndex.HelmOfMalor: return "Helm of Malor";
                case Wiz1ItemIndex.ScrollOfBadial: return "Scroll of Badial";
                case Wiz1ItemIndex.ShortSwordMinus2: return "Short Sword -2";
                case Wiz1ItemIndex.DaggerPlus2: return "Dagger +2";
                case Wiz1ItemIndex.MaceMinus2: return "Mace -2";
                case Wiz1ItemIndex.StaffMinus2: return "Staff -2";
                case Wiz1ItemIndex.DaggerOfSpeed: return "Dagger of Speed";
                case Wiz1ItemIndex.RobeOfCurses: return "Robe of Curses";
                case Wiz1ItemIndex.LeatherMinus2: return "Leather Armor -2";
                case Wiz1ItemIndex.ChainMinus2: return "Chain Mail -2";
                case Wiz1ItemIndex.BreastPlateMinus2: return "Breast Plate -2";
                case Wiz1ItemIndex.ShieldMinus2: return "Shield -2";
                case Wiz1ItemIndex.CursedHelmet: return "Helm of Curses";
                case Wiz1ItemIndex.BreastPlatePlus2: return "Breast Plate +2";
                case Wiz1ItemIndex.GlovesOfSilver: return "Gloves of Silver";
                case Wiz1ItemIndex.EvilSwordPlus3: return "Evil Sword +3";
                case Wiz1ItemIndex.EvilShortSwordPlus3: return "Evil Short Sword +3";
                case Wiz1ItemIndex.DaggerOfThieves: return "Dagger of Thieves";
                case Wiz1ItemIndex.BreastPlatePlus3: return "Breast Plate +3";
                case Wiz1ItemIndex.LordsGarb: return "Garb of Lords";
                case Wiz1ItemIndex.MurasamaBlade: return "Muramasa Blade!";
                case Wiz1ItemIndex.Shurikens: return "Shurikens";
                case Wiz1ItemIndex.ColdChainMail: return "Cold Chain Mail";
                case Wiz1ItemIndex.EvilPlatePlus3: return "Evil Plate +3";
                case Wiz1ItemIndex.ShieldPlus3: return "Shield +3";
                case Wiz1ItemIndex.RingOfHealing: return "Ring of Healing";
                case Wiz1ItemIndex.PriestsRing: return "Priest's Ring";
                case Wiz1ItemIndex.DeadlyRing: return "Ring of Death";
                case Wiz1ItemIndex.AmuletOfWerdna: return "Amulet of Werdna";
                case Wiz1ItemIndex.StatuetteOfBear: return "Statue of Bear";
                case Wiz1ItemIndex.StatuetteOfFrog: return "Statue of Frog";
                case Wiz1ItemIndex.KeyOfBronze: return "Key of Bronze";
                case Wiz1ItemIndex.KeyOfSilver: return "Key of Silver";
                case Wiz1ItemIndex.KeyOfGold: return "Key of Gold";
                case Wiz1ItemIndex.BlueRibbon: return "Blue Ribbon";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }
    }

    public class Wiz1ItemList : Wiz123ItemList
    {
        public override WizItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return new Wiz1Item(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_1_Item_List_mem); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes bytes = hacker.ReadOffset(Wiz1.Memory.ItemList, 4694);
            if (bytes == null)
                return null;
            return bytes.Bytes;
        }

        public Wiz1ItemList()
        {
            InitInternalList();
        }
    }

    public class Wiz1SearchResults : Wiz123SearchResults
    {
        public Wiz1SearchResults(int iRewardIndex)
        {
            RewardIndex = iRewardIndex;
        }

        public override List<WizTreasure> Treasures { get { return Wiz1.Treasures; } }
    }
}
