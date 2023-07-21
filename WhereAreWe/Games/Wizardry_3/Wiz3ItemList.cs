using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum Wiz3ItemIndex
    {
        Unknown = -1,
        BrokenItem = 1000,
        OrbOfEarithin,
        NeutralCrystal,
        CrystalOfEvil,
        CrystalOfGood,
        ShipInBottle,
        StaffOfEarth,
        AmuletOfAir,
        HolyWater,
        RodOfFire,
        GoldMedallion,
        OrbOfMhuuzfes,
        ButterflyKnifeA,
        ShortSword,
        Broadsword,
        Mace,
        Staff,
        HandAxe,
        BattleAxe,
        Dagger,
        Flail,
        RoundShield,
        HeaterShield,
        MagesRobes,
        Cuirass,
        Hauberk,
        Breastplate,
        PlateArmor,
        Sallet,
        PotionOfDios,
        PotionOfLatumofis,
        ShortSwordPlus1,
        BroadswordPlus1,
        MacePlus1,
        BattleAxePlus1,
        Nunchaka,
        DaggerPlus1,
        ScrollOfKatino,
        CuirassPlus1,
        HauberkPlus1,
        BreastplatePlus1,
        PlateArmorPlus1,
        HeaterPlus1,
        Bascinet,
        GlovesOfIron,
        ScrollOfBadios,
        ScrollOfHalito,
        ShortSwordMinus1,
        BroadswordMinus1,
        MaceMinus1,
        DaggerMinus1,
        BattleAxeMinus1,
        MargauxsFlail,
        BagOfGems,
        WizardsStaff,
        Flametongue,
        RoundShieldMinus1,
        CuirassMinus1,
        HauberkMinus1,
        BreastplateMinus1,
        PlateArmorMinus1,
        SalletMinus1,
        PotionOfSopic,
        GoldRing,
        SalamanderRing,
        SerpentsTooth,
        ShortSwordPlus2,
        BroadswordPlus2,
        BattleAxePlus2,
        IvoryBlade,
        EbonyBlade,
        AmberBlade,
        MacePlus2,
        GlovesOfMithril,
        AmuletOfDialko,
        CuirassPlus2,
        HeaterPlus2,
        DisplacerRobes,
        HauberkPlus2,
        BreastplatePlus2,
        PlateArmorPlus2,
        Armet,
        WarganRobes,
        GiantsClub,
        BladeCuisinart,
        ShepherdCrook,
        UnholyAxe,
        RodOfDeath,
        GemOfExorcism,
        BagOfEmeralds,
        BagOfGarnets,
        BluePearl,
        RubySlippers,
        NecrologyRod,
        BookOfLife,
        BookOfDeath,
        DragonsTooth,
        TrollkinRing,
        RabbitsFoot,
        ThiefsPick,
        BookOfDemons,
        ButterflyKnifeB,
        GoldTiara,
        MantisGloves,
        Last
    }

    public class Wiz3Item : WizItem
    {
        protected int m_diskIndex;

        public Wiz3Item(int index, byte[] bytes, int offset = 0)
        {
            SetBytes(index, bytes, offset);
        }

        public override GameNames Game { get { return GameNames.Wizardry3; } }
        public override int DiskIndex { get { return m_diskIndex; } set { m_diskIndex = value; } }
        public override int Index { get { return m_index % 1000; } set { m_index = value % 1000; m_diskIndex = value % 1000 + 1000; } }
        public override WizItem CreateItem(int index, byte[] bytes, int offset = 0) { return new Wiz3Item(index, bytes, offset); }

        public Wiz3ItemIndex ItemIndex { get { return (Wiz3ItemIndex)DiskIndex; } set { Index = (int)value % 1000; } }
        public Wiz3ItemIndex BreaksInto { get { return (Wiz3ItemIndex)m_breaks; } set { m_breaks = (int)value; } }

        public override bool Trashable { get { return WizValue != 0; } }

        public override string ItemNoun
        {
            get
            {
                switch ((Wiz3ItemIndex)DiskIndex)
                {
                    case Wiz3ItemIndex.BrokenItem: return "Broken Item";
                    case Wiz3ItemIndex.AmuletOfAir: return "Amulet";
                    case Wiz3ItemIndex.HauberkPlus2:
                    case Wiz3ItemIndex.BreastplatePlus2:
                    case Wiz3ItemIndex.PlateArmorPlus2:
                    case Wiz3ItemIndex.PlateArmorMinus1:
                    case Wiz3ItemIndex.CuirassPlus2:
                    case Wiz3ItemIndex.BreastplateMinus1:
                    case Wiz3ItemIndex.Cuirass:
                    case Wiz3ItemIndex.Hauberk:
                    case Wiz3ItemIndex.HauberkMinus1:
                    case Wiz3ItemIndex.CuirassMinus1:
                    case Wiz3ItemIndex.Breastplate:
                    case Wiz3ItemIndex.PlateArmor:
                    case Wiz3ItemIndex.CuirassPlus1:
                    case Wiz3ItemIndex.BreastplatePlus1:
                    case Wiz3ItemIndex.HauberkPlus1:
                    case Wiz3ItemIndex.PlateArmorPlus1: return "Armor";
                    case Wiz3ItemIndex.BagOfGarnets:
                    case Wiz3ItemIndex.BagOfEmeralds:
                    case Wiz3ItemIndex.BagOfGems: return "Bag";
                    case Wiz3ItemIndex.BookOfLife:
                    case Wiz3ItemIndex.BookOfDeath:
                    case Wiz3ItemIndex.BookOfDemons: return "Book";
                    case Wiz3ItemIndex.ShipInBottle: return "Bottle";
                    case Wiz3ItemIndex.WarganRobes:
                    case Wiz3ItemIndex.DisplacerRobes:
                    case Wiz3ItemIndex.MagesRobes: return "Clothing";
                    case Wiz3ItemIndex.CrystalOfEvil: return "Crystal of Evil";
                    case Wiz3ItemIndex.CrystalOfGood: return "Crystal of Good";
                    case Wiz3ItemIndex.OrbOfEarithin: return "Crystal Sphere";
                    case Wiz3ItemIndex.GlovesOfMithril: return "Gauntlets";
                    case Wiz3ItemIndex.MantisGloves:
                    case Wiz3ItemIndex.GlovesOfIron:
                    case Wiz3ItemIndex.SalletMinus1:
                    case Wiz3ItemIndex.Sallet:
                    case Wiz3ItemIndex.Armet:
                    case Wiz3ItemIndex.Bascinet: return "Helm";
                    case Wiz3ItemIndex.GoldTiara:
                    case Wiz3ItemIndex.BluePearl:
                    case Wiz3ItemIndex.GemOfExorcism:
                    case Wiz3ItemIndex.AmuletOfDialko: return "Jewelry";
                    case Wiz3ItemIndex.GoldMedallion: return "Medallion";
                    case Wiz3ItemIndex.NeutralCrystal: return "Neutral Crystal";
                    case Wiz3ItemIndex.HolyWater:
                    case Wiz3ItemIndex.PotionOfLatumofis:
                    case Wiz3ItemIndex.PotionOfSopic:
                    case Wiz3ItemIndex.ScrollOfHalito:
                    case Wiz3ItemIndex.PotionOfDios: return "Potion";
                    case Wiz3ItemIndex.GoldRing:
                    case Wiz3ItemIndex.TrollkinRing:
                    case Wiz3ItemIndex.SalamanderRing: return "Ring";
                    case Wiz3ItemIndex.NecrologyRod:
                    case Wiz3ItemIndex.RodOfFire:
                    case Wiz3ItemIndex.RodOfDeath: return "Rod";
                    case Wiz3ItemIndex.ScrollOfKatino:
                    case Wiz3ItemIndex.ScrollOfBadios: return "Scroll";
                    case Wiz3ItemIndex.RoundShield:
                    case Wiz3ItemIndex.HeaterPlus1:
                    case Wiz3ItemIndex.HeaterPlus2:
                    case Wiz3ItemIndex.RoundShieldMinus1:
                    case Wiz3ItemIndex.HeaterShield: return "Shield";
                    case Wiz3ItemIndex.OrbOfMhuuzfes: return "Sphere";
                    case Wiz3ItemIndex.WizardsStaff:
                    case Wiz3ItemIndex.Mace:
                    case Wiz3ItemIndex.MaceMinus1:
                    case Wiz3ItemIndex.Staff:
                    case Wiz3ItemIndex.Flail:
                    case Wiz3ItemIndex.StaffOfEarth:
                    case Wiz3ItemIndex.MacePlus2:
                    case Wiz3ItemIndex.GiantsClub:
                    case Wiz3ItemIndex.MacePlus1:
                    case Wiz3ItemIndex.ShepherdCrook: return "Staff";
                    case Wiz3ItemIndex.DragonsTooth:
                    case Wiz3ItemIndex.ThiefsPick:
                    case Wiz3ItemIndex.SerpentsTooth:
                    case Wiz3ItemIndex.RabbitsFoot:
                    case Wiz3ItemIndex.RubySlippers: return "Strange Item";
                    case Wiz3ItemIndex.EbonyBlade:
                    case Wiz3ItemIndex.BladeCuisinart:
                    case Wiz3ItemIndex.ShortSwordPlus1:
                    case Wiz3ItemIndex.Flametongue:
                    case Wiz3ItemIndex.BattleAxePlus1:
                    case Wiz3ItemIndex.Nunchaka:
                    case Wiz3ItemIndex.DaggerPlus1:
                    case Wiz3ItemIndex.IvoryBlade:
                    case Wiz3ItemIndex.Dagger:
                    case Wiz3ItemIndex.BattleAxe:
                    case Wiz3ItemIndex.HandAxe:
                    case Wiz3ItemIndex.BattleAxePlus2:
                    case Wiz3ItemIndex.UnholyAxe:
                    case Wiz3ItemIndex.Broadsword:
                    case Wiz3ItemIndex.ShortSword:
                    case Wiz3ItemIndex.ButterflyKnifeA:
                    case Wiz3ItemIndex.BroadswordPlus2:
                    case Wiz3ItemIndex.ShortSwordPlus2:
                    case Wiz3ItemIndex.ShortSwordMinus1:
                    case Wiz3ItemIndex.BroadswordMinus1:
                    case Wiz3ItemIndex.DaggerMinus1:
                    case Wiz3ItemIndex.BattleAxeMinus1:
                    case Wiz3ItemIndex.ButterflyKnifeB:
                    case Wiz3ItemIndex.AmberBlade:
                    case Wiz3ItemIndex.MargauxsFlail:
                    case Wiz3ItemIndex.BroadswordPlus1: return "Weapon";
                    default: return String.Format("Unknown Item");
                }
            }
        }

        public override string GetName(int index) { return GetName((Wiz3ItemIndex) (index % 1000 + 1000)); }

        public static string GetName(Wiz3ItemIndex index)
        {
            switch (index)
            {
                case Wiz3ItemIndex.BrokenItem: return "Broken Item";
                case Wiz3ItemIndex.OrbOfEarithin: return "Orb of Earithin";
                case Wiz3ItemIndex.NeutralCrystal: return "Neutral Crystal";
                case Wiz3ItemIndex.CrystalOfEvil: return "Crystal of Evil";
                case Wiz3ItemIndex.CrystalOfGood: return "Crystal of Good";
                case Wiz3ItemIndex.ShipInBottle: return "Ship in Bottle";
                case Wiz3ItemIndex.StaffOfEarth: return "Staff of Earth";
                case Wiz3ItemIndex.AmuletOfAir: return "Amulet of Air";
                case Wiz3ItemIndex.HolyWater: return "Holy Water";
                case Wiz3ItemIndex.RodOfFire: return "Rod of Fire";
                case Wiz3ItemIndex.GoldMedallion: return "Gold Medallion";
                case Wiz3ItemIndex.OrbOfMhuuzfes: return "Orb of Mhuuzfes";
                case Wiz3ItemIndex.ButterflyKnifeA: return "Butterfly Knife A";
                case Wiz3ItemIndex.ShortSword: return "Short Sword";
                case Wiz3ItemIndex.Broadsword: return "Broadsword";
                case Wiz3ItemIndex.Mace: return "Mace";
                case Wiz3ItemIndex.Staff: return "Staff";
                case Wiz3ItemIndex.HandAxe: return "Hand Axe";
                case Wiz3ItemIndex.BattleAxe: return "Battle Axe";
                case Wiz3ItemIndex.Dagger: return "Dagger";
                case Wiz3ItemIndex.Flail: return "Flail";
                case Wiz3ItemIndex.RoundShield: return "Round Shield";
                case Wiz3ItemIndex.HeaterShield: return "Heater Shield";
                case Wiz3ItemIndex.MagesRobes: return "Mage's Robes";
                case Wiz3ItemIndex.Cuirass: return "Cuirass";
                case Wiz3ItemIndex.Hauberk: return "Hauberk";
                case Wiz3ItemIndex.Breastplate: return "Breastplate";
                case Wiz3ItemIndex.PlateArmor: return "Plate Armor";
                case Wiz3ItemIndex.Sallet: return "Sallet";
                case Wiz3ItemIndex.PotionOfDios: return "Potion of Dios";
                case Wiz3ItemIndex.PotionOfLatumofis: return "Potion of Latumofis";
                case Wiz3ItemIndex.ShortSwordPlus1: return "Short Sword +1";
                case Wiz3ItemIndex.BroadswordPlus1: return "Broadsword +1";
                case Wiz3ItemIndex.MacePlus1: return "Mace +1";
                case Wiz3ItemIndex.BattleAxePlus1: return "Battle Axe +1";
                case Wiz3ItemIndex.Nunchaka: return "Nunchaka";
                case Wiz3ItemIndex.DaggerPlus1: return "Dagger +1";
                case Wiz3ItemIndex.ScrollOfKatino: return "Scroll of Katino";
                case Wiz3ItemIndex.CuirassPlus1: return "Cuirass +1";
                case Wiz3ItemIndex.HauberkPlus1: return "Hauberk +1";
                case Wiz3ItemIndex.BreastplatePlus1: return "Breastplate +1";
                case Wiz3ItemIndex.PlateArmorPlus1: return "Plate Armor +1";
                case Wiz3ItemIndex.HeaterPlus1: return "Heater +1";
                case Wiz3ItemIndex.Bascinet: return "Bascinet";
                case Wiz3ItemIndex.GlovesOfIron: return "Gloves of Iron";
                case Wiz3ItemIndex.ScrollOfBadios: return "Scroll of Badios";
                case Wiz3ItemIndex.ScrollOfHalito: return "Scroll of Halito";
                case Wiz3ItemIndex.ShortSwordMinus1: return "Short Sword -1";
                case Wiz3ItemIndex.BroadswordMinus1: return "Broadsword -1";
                case Wiz3ItemIndex.MaceMinus1: return "Mace -1";
                case Wiz3ItemIndex.DaggerMinus1: return "Dagger -1";
                case Wiz3ItemIndex.BattleAxeMinus1: return "Battle Axe -1";
                case Wiz3ItemIndex.MargauxsFlail: return "Margaux's Flail";
                case Wiz3ItemIndex.BagOfGems: return "Bag of Gems";
                case Wiz3ItemIndex.WizardsStaff: return "Wizard's Staff";
                case Wiz3ItemIndex.Flametongue: return "Flametongue";
                case Wiz3ItemIndex.RoundShieldMinus1: return "Round Shield -1";
                case Wiz3ItemIndex.CuirassMinus1: return "Cuirass -1";
                case Wiz3ItemIndex.HauberkMinus1: return "Hauberk -1";
                case Wiz3ItemIndex.BreastplateMinus1: return "Breastplate -1";
                case Wiz3ItemIndex.PlateArmorMinus1: return "Plate Armor -1";
                case Wiz3ItemIndex.SalletMinus1: return "Sallet -1";
                case Wiz3ItemIndex.PotionOfSopic: return "Potion of Sopic";
                case Wiz3ItemIndex.GoldRing: return "Gold Ring";
                case Wiz3ItemIndex.SalamanderRing: return "Salamander Ring";
                case Wiz3ItemIndex.SerpentsTooth: return "Serpent's Tooth";
                case Wiz3ItemIndex.ShortSwordPlus2: return "Short Sword +2";
                case Wiz3ItemIndex.BroadswordPlus2: return "Broadsword +2";
                case Wiz3ItemIndex.BattleAxePlus2: return "Battle Axe +2";
                case Wiz3ItemIndex.IvoryBlade: return "Ivory Blade (G)";
                case Wiz3ItemIndex.EbonyBlade: return "Ebony Blade (E)";
                case Wiz3ItemIndex.AmberBlade: return "Amber Blade (N)";
                case Wiz3ItemIndex.MacePlus2: return "Mace +2";
                case Wiz3ItemIndex.GlovesOfMithril: return "Gloves of Mithril";
                case Wiz3ItemIndex.AmuletOfDialko: return "Amulet of Dialko";
                case Wiz3ItemIndex.CuirassPlus2: return "Cuirass +2";
                case Wiz3ItemIndex.HeaterPlus2: return "Heater +2";
                case Wiz3ItemIndex.DisplacerRobes: return "Displacer Robes";
                case Wiz3ItemIndex.HauberkPlus2: return "Hauberk +2";
                case Wiz3ItemIndex.BreastplatePlus2: return "Breastplate +2";
                case Wiz3ItemIndex.PlateArmorPlus2: return "Plate Armor +2";
                case Wiz3ItemIndex.Armet: return "Armet";
                case Wiz3ItemIndex.WarganRobes: return "Wargan Robes";
                case Wiz3ItemIndex.GiantsClub: return "Giant's Club";
                case Wiz3ItemIndex.BladeCuisinart: return "Blade Cuisinart'";
                case Wiz3ItemIndex.ShepherdCrook: return "Shepherd Crook";
                case Wiz3ItemIndex.UnholyAxe: return "Unholy Axe";
                case Wiz3ItemIndex.RodOfDeath: return "Rod of Death";
                case Wiz3ItemIndex.GemOfExorcism: return "Gem of Exorcism";
                case Wiz3ItemIndex.BagOfEmeralds: return "Bag of Emeralds";
                case Wiz3ItemIndex.BagOfGarnets: return "Bag of Garnets";
                case Wiz3ItemIndex.BluePearl: return "Blue Pearl";
                case Wiz3ItemIndex.RubySlippers: return "Ruby Slippers";
                case Wiz3ItemIndex.NecrologyRod: return "Necrology Rod";
                case Wiz3ItemIndex.BookOfLife: return "Book of Life";
                case Wiz3ItemIndex.BookOfDeath: return "Book of Death";
                case Wiz3ItemIndex.DragonsTooth: return "Dragon's Tooth";
                case Wiz3ItemIndex.TrollkinRing: return "Trollkin Ring";
                case Wiz3ItemIndex.RabbitsFoot: return "Rabbit's Foot";
                case Wiz3ItemIndex.ThiefsPick: return "Thief's Pick";
                case Wiz3ItemIndex.BookOfDemons: return "Book of Demons";
                case Wiz3ItemIndex.ButterflyKnifeB: return "Butterfly Knife B";
                case Wiz3ItemIndex.GoldTiara: return "Gold Tiara";
                case Wiz3ItemIndex.MantisGloves: return "Mantis Gloves";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }
    }

    public class Wiz3ItemList : Wiz123ItemList
    {
        public override WizItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return new Wiz3Item(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_2_Item_List_mem); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes bytes = hacker.ReadOffset(Wiz3.Memory.ItemList, 4832);
            if (bytes == null)
                return null;
            return bytes.Bytes;
        }

        public Wiz3ItemList()
        {
            InitInternalList();
        }

        public override WizItem GetItem(int index, int memory = -1)
        {
            index = index % 1000 + 1000;
            WizItem item = null;
            if (index < (int) Wiz3ItemIndex.BrokenItem || index >= (int) Wiz3ItemIndex.Last)
                index = (int)Wiz3ItemIndex.BrokenItem;

            item = Items[index - (int) Wiz3ItemIndex.BrokenItem].Clone() as Wiz3Item;
            item.MemoryIndex = memory;
            return item;
        }
    }

    public class Wiz3SearchResults : Wiz123SearchResults
    {
        public Wiz3SearchResults(int iRewardIndex)
        {
            RewardIndex = iRewardIndex;
        }

        public override List<WizTreasure> Treasures { get { return Wiz3.Treasures; } }
    }
}
