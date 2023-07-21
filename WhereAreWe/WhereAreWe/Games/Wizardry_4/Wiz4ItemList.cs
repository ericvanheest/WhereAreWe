using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum Wiz4ItemIndex
    {
        Unknown = -1,
        BrokenItem = 0,
        Bloodstone,
        LandersTurq,
        AmberDragon,
        HHGOfAuntyOck,
        WingedBoots,
        DreampainterKa,
        EastWindSword,
        WestWindSword,
        DragonsClaw,
        HopalongCarrot,
        CleansingOil,
        WitchingRod,
        AromaticBall,
        VoidTransducer,
        KrisOfTruth,
        InnKey,
        CrystalRose,
        DabOfPuce,
        Pennonceaux,
        MaintenanceCap,
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
        PotionOfPorfic,
        LongSwordPlus1,
        ShortSwordPlus1,
        MacePlus1,
        StaffOfMogref,
        ScrollOfKatino,
        LeatherArmorPlus1,
        ChainMailPlus1,
        PlateMailPlus1,
        ShieldPlus1,
        StKasFoot,
        ScrollOfBadios,
        ScollOfHalito,
        StaffPlus2,
        DragonSlayer,
        HelmPlus1,
        JeweledAmulet,
        ScrollOfBadialA,
        PotionOfSopic,
        LongSwordPlus2,
        GoodHopeCape,
        MagiciansHat,
        NovicesCap,
        ScrollOfDilto,
        CopperGloves,
        InitiateTurban,
        WizardSkullcap,
        PlateMailPlus2,
        ShieldPlus2,
        Mordorcharge,
        PotionOfDial,
        RingOfPorfic,
        WereSlayer,
        MageMasher,
        MaceOfCuring,
        StaffOfMontino,
        BladeCusinart,
        AmuletOfBadialma,
        RodOfFlame,
        CapeOfHideA,
        CapeOfJackal,
        CapeOfHideB,
        AmuletOfMakanito,
        DiademOfMalor,
        ScrollOfBadialB,
        DaggerPlus2,
        DaggerOfSpeed,
        LichsRobes,
        SkullsCap,
        PotionOfMasopic,
        SilverGloves,
        GetOutOfJailFree,
        GoldenPyrite,
        OxygenMask,
        ChroniclesOfHawkwind,
        LordsGarb,
        MuramasaBlade,
        Shuriken,
        ChainProIce,
        Invalid95,
        Invalid96,
        RingOfHealing,
        RingOfDispelling,
        RingOfDeath,
        AdeptBaldness,
        ArabicDiary,
        DemonicChimes,
        BlackCandle,
        BlackBox,
        StTreborRump,
        BishsTongue,
        StRimboDigit,
        ArrowOfTruth,
        OrbOfDreams,
        RallyingHorn,
        SignetRing,
        MythrilGlove,
        HolyLimpWrist,
        TwilightCloak,
        ShadowCloak,
        ConeOfSilence,
        DarknessCloak,
        NightCloak,
        EntropyCloak,
        Last
    }

    public class Wiz4Item : WizItem
    {
        public Wiz4Item(int index, byte[] bytes, int offset = 0)
        {
            SetBytes(index, bytes, offset);
        }

        public override GameNames Game { get { return GameNames.Wizardry4; } }
        public override WizItem CreateItem(int index, byte[] bytes, int offset = 0) { return new Wiz4Item(index, bytes, offset); }
        public override int Index { get { return m_index; } set { m_index = value; } }
        public override bool RevealUnidentified { get { return true; } }

        public Wiz4ItemIndex BreaksInto { get { return (Wiz4ItemIndex)m_breaks; } set { m_breaks = (int)value; } }

        public override bool Trashable { get { return WizValue != 0; } }

        public override string GetName(int index) { return GetName((Wiz4ItemIndex) index); }

        public override string ItemNoun
        {
            get
            {
                switch ((Wiz4ItemIndex)Index)
                {
                    case Wiz4ItemIndex.BrokenItem: return "Broken Item";
                    case Wiz4ItemIndex.CapeOfJackal: return "**USE ME** Cape";
                    case Wiz4ItemIndex.WizardSkullcap: return "3-Sided Cloth";
                    case Wiz4ItemIndex.GetOutOfJailFree: return "A Yellow Card";
                    case Wiz4ItemIndex.AmuletOfMakanito:
                    case Wiz4ItemIndex.AmuletOfBadialma:
                    case Wiz4ItemIndex.JeweledAmulet: return "Amulet";
                    case Wiz4ItemIndex.BreastPlate:
                    case Wiz4ItemIndex.ChainMail:
                    case Wiz4ItemIndex.LordsGarb:
                    case Wiz4ItemIndex.LeatherArmorPlus1:
                    case Wiz4ItemIndex.ChainMailPlus1:
                    case Wiz4ItemIndex.PlateMailPlus1:
                    case Wiz4ItemIndex.LeatherArmor:
                    case Wiz4ItemIndex.ChainProIce:
                    case Wiz4ItemIndex.PlateMailPlus2:
                    case Wiz4ItemIndex.PlateMail: return "Armor";
                    case Wiz4ItemIndex.NovicesCap: return "Beanie";
                    case Wiz4ItemIndex.WestWindSword: return "Blue Sword";
                    case Wiz4ItemIndex.OxygenMask: return "Breath of Life";
                    case Wiz4ItemIndex.CapeOfHideA:
                    case Wiz4ItemIndex.GoodHopeCape:
                    case Wiz4ItemIndex.CapeOfHideB: return "Cape";
                    case Wiz4ItemIndex.Mordorcharge: return "Charge Card";
                    case Wiz4ItemIndex.KrisOfTruth: return "Clear Light";
                    case Wiz4ItemIndex.Robes: return "Clothing";
                    case Wiz4ItemIndex.InitiateTurban: return "Conical Hat";
                    case Wiz4ItemIndex.DaggerOfSpeed:
                    case Wiz4ItemIndex.DaggerPlus2:
                    case Wiz4ItemIndex.Dagger: return "Dagger";
                    case Wiz4ItemIndex.DabOfPuce: return "Dark Glob";
                    case Wiz4ItemIndex.DiademOfMalor: return "Diadem";
                    case Wiz4ItemIndex.DreampainterKa: return "Feather";
                    case Wiz4ItemIndex.WitchingRod: return "Forked Stick";
                    case Wiz4ItemIndex.MagiciansHat: return "Furred Cone";
                    case Wiz4ItemIndex.SilverGloves: return "Gauntlets";
                    case Wiz4ItemIndex.CrystalRose: return "Glass Sculpture";
                    case Wiz4ItemIndex.CopperGloves: return "Gloves";
                    case Wiz4ItemIndex.DragonsClaw: return "Golden Sword";
                    case Wiz4ItemIndex.EastWindSword: return "Green Sword";
                    case Wiz4ItemIndex.MaintenanceCap: return "Hat with Visor";
                    case Wiz4ItemIndex.HelmPlus1:
                    case Wiz4ItemIndex.Helm: return "Helm";
                    case Wiz4ItemIndex.ChroniclesOfHawkwind: return "Iron Bound Book";
                    case Wiz4ItemIndex.HHGOfAuntyOck: return "Jeweled Fruit";
                    case Wiz4ItemIndex.InnKey: return "Key on Chain";
                    case Wiz4ItemIndex.MacePlus1:
                    case Wiz4ItemIndex.MaceOfCuring:
                    case Wiz4ItemIndex.ShortSword: return "Knobbed Stick";
                    case Wiz4ItemIndex.WingedBoots: return "Molting Leather";
                    case Wiz4ItemIndex.VoidTransducer: return "Nyin";
                    case Wiz4ItemIndex.CleansingOil: return "Oil of Ole'";
                    case Wiz4ItemIndex.HopalongCarrot: return "Orange Rod";
                    case Wiz4ItemIndex.PotionOfDial:
                    case Wiz4ItemIndex.PotionOfMasopic:
                    case Wiz4ItemIndex.PotionOfDios:
                    case Wiz4ItemIndex.PotionOfPorfic:
                    case Wiz4ItemIndex.PotionOfSopic: return "Potion";
                    case Wiz4ItemIndex.RingOfDeath:
                    case Wiz4ItemIndex.RingOfHealing:
                    case Wiz4ItemIndex.RingOfDispelling:
                    case Wiz4ItemIndex.RingOfPorfic: return "Ring";
                    case Wiz4ItemIndex.LichsRobes: return "Robe";
                    case Wiz4ItemIndex.RodOfFlame: return "Rod";
                    case Wiz4ItemIndex.ScrollOfBadialA:
                    case Wiz4ItemIndex.ScrollOfDilto:
                    case Wiz4ItemIndex.ScrollOfKatino:
                    case Wiz4ItemIndex.ScrollOfBadialB:
                    case Wiz4ItemIndex.ScrollOfBadios:
                    case Wiz4ItemIndex.ScollOfHalito: return "Scroll";
                    case Wiz4ItemIndex.SmallShield:
                    case Wiz4ItemIndex.LargeShield:
                    case Wiz4ItemIndex.ShieldPlus1:
                    case Wiz4ItemIndex.ShieldPlus2: return "Shield";
                    case Wiz4ItemIndex.Pennonceaux: return "Silk Cloth";
                    case Wiz4ItemIndex.StaffOfMontino:
                    case Wiz4ItemIndex.StaffOfMogref: return "Staff";
                    case Wiz4ItemIndex.AnointedMace: return "Stick with Chain";
                    case Wiz4ItemIndex.Staff:
                    case Wiz4ItemIndex.StaffPlus2: return "Stick";
                    case Wiz4ItemIndex.AmberDragon:
                    case Wiz4ItemIndex.LandersTurq:
                    case Wiz4ItemIndex.Bloodstone:
                    case Wiz4ItemIndex.GoldenPyrite:
                    case Wiz4ItemIndex.AnointedFlail: return "Stone";
                    case Wiz4ItemIndex.ShortSwordPlus1:
                    case Wiz4ItemIndex.WereSlayer:
                    case Wiz4ItemIndex.MageMasher:
                    case Wiz4ItemIndex.DragonSlayer:
                    case Wiz4ItemIndex.BladeCusinart:
                    case Wiz4ItemIndex.LongSwordPlus1:
                    case Wiz4ItemIndex.LongSwordPlus2:
                    case Wiz4ItemIndex.LongSword: return "Sword";
                    case Wiz4ItemIndex.MuramasaBlade:
                    case Wiz4ItemIndex.Shuriken: return "Weapon";
                    case Wiz4ItemIndex.SkullsCap: return "White Cap";
                    case Wiz4ItemIndex.AromaticBall: return "White Sphere";
                    case Wiz4ItemIndex.BlackCandle: return "Charred Tallow";
                    case Wiz4ItemIndex.DarknessCloak:
                    case Wiz4ItemIndex.NightCloak:
                    case Wiz4ItemIndex.EntropyCloak:
                    case Wiz4ItemIndex.ShadowCloak:
                    case Wiz4ItemIndex.TwilightCloak: return "Cloak";
                    case Wiz4ItemIndex.ConeOfSilence: return "Dunce Cap";
                    case Wiz4ItemIndex.OrbOfDreams: return "Gold Ball";
                    case Wiz4ItemIndex.ArrowOfTruth: return "Gwilym's Arrow";
                    case Wiz4ItemIndex.AdeptBaldness: return "Hair Remover";
                    case Wiz4ItemIndex.StTreborRump:
                    case Wiz4ItemIndex.StRimboDigit:
                    case Wiz4ItemIndex.BishsTongue:
                    case Wiz4ItemIndex.StKasFoot:
                    case Wiz4ItemIndex.HolyLimpWrist: return "Holy Reliquary";
                    case Wiz4ItemIndex.RallyingHorn: return "Horn";
                    case Wiz4ItemIndex.MythrilGlove: return "Shiny Gauntlet";
                    case Wiz4ItemIndex.ArabicDiary: return "Tale of Madness";
                    case Wiz4ItemIndex.SignetRing: return "Wax Seal";
                    case Wiz4ItemIndex.BlackBox: return "Weighty Cube";
                    case Wiz4ItemIndex.DemonicChimes: return "Wired Bones";
                    default: return String.Format("Unknown Item");
                }
            }
        }

        public static string GetName(Wiz4ItemIndex index)
        {
            switch (index)
            {
                case Wiz4ItemIndex.BrokenItem: return "Broken Item";
                case Wiz4ItemIndex.Bloodstone: return "Bloodstone";
                case Wiz4ItemIndex.LandersTurq: return "Lander's Turquoise";
                case Wiz4ItemIndex.AmberDragon: return "Amber Dragon";
                case Wiz4ItemIndex.HHGOfAuntyOck: return "Holy Hand Grenade of Aunty Ock";
                case Wiz4ItemIndex.WingedBoots: return "Winged Boots";
                case Wiz4ItemIndex.DreampainterKa: return "Dreampainter Ka";
                case Wiz4ItemIndex.EastWindSword: return "East Wind Sword";
                case Wiz4ItemIndex.WestWindSword: return "West Wind Sword";
                case Wiz4ItemIndex.DragonsClaw: return "Dragon's Claw";
                case Wiz4ItemIndex.HopalongCarrot: return "Hopalong Carrot";
                case Wiz4ItemIndex.CleansingOil: return "Cleansing Oil";
                case Wiz4ItemIndex.WitchingRod: return "Witching Rod";
                case Wiz4ItemIndex.AromaticBall: return "Aromatic Ball";
                case Wiz4ItemIndex.VoidTransducer: return "Void Transducer";
                case Wiz4ItemIndex.KrisOfTruth: return "Kris of Truth";
                case Wiz4ItemIndex.InnKey: return "Inn Key";
                case Wiz4ItemIndex.CrystalRose: return "Crystal Rose";
                case Wiz4ItemIndex.DabOfPuce: return "Dab of Puce";
                case Wiz4ItemIndex.Pennonceaux: return "Pennonceaux";
                case Wiz4ItemIndex.MaintenanceCap: return "Maintenance Cap";
                case Wiz4ItemIndex.LongSword: return "Long Sword";
                case Wiz4ItemIndex.ShortSword: return "Short Sword";
                case Wiz4ItemIndex.AnointedMace: return "Anointed Mace";
                case Wiz4ItemIndex.AnointedFlail: return "Anointed Flail";
                case Wiz4ItemIndex.Staff: return "Staff";
                case Wiz4ItemIndex.Dagger: return "Dagger";
                case Wiz4ItemIndex.SmallShield: return "Small Shield";
                case Wiz4ItemIndex.LargeShield: return "Large Shield";
                case Wiz4ItemIndex.Robes: return "Robes";
                case Wiz4ItemIndex.LeatherArmor: return "Leather Armor";
                case Wiz4ItemIndex.ChainMail: return "Chain Mail";
                case Wiz4ItemIndex.BreastPlate: return "Breast Plate";
                case Wiz4ItemIndex.PlateMail: return "Plate Mail";
                case Wiz4ItemIndex.Helm: return "Helm";
                case Wiz4ItemIndex.PotionOfDios: return "Potion of Dios";
                case Wiz4ItemIndex.PotionOfPorfic: return "Potion of Porfic";
                case Wiz4ItemIndex.LongSwordPlus1: return "Long Sword +1";
                case Wiz4ItemIndex.ShortSwordPlus1: return "Short Sword +1";
                case Wiz4ItemIndex.MacePlus1: return "Mace +1";
                case Wiz4ItemIndex.StaffOfMogref: return "Staff of Mogref";
                case Wiz4ItemIndex.ScrollOfKatino: return "Scroll of Katino";
                case Wiz4ItemIndex.LeatherArmorPlus1: return "Leather Armor +1";
                case Wiz4ItemIndex.ChainMailPlus1: return "Chain Mail +1";
                case Wiz4ItemIndex.PlateMailPlus1: return "Plate Mail +1";
                case Wiz4ItemIndex.ShieldPlus1: return "Shield +1";
                case Wiz4ItemIndex.StKasFoot: return "St. Ka's Foot";
                case Wiz4ItemIndex.ScrollOfBadios: return "Scroll of Badios";
                case Wiz4ItemIndex.ScollOfHalito: return "Scoll of Halito";
                case Wiz4ItemIndex.StaffPlus2: return "Staff +2";
                case Wiz4ItemIndex.DragonSlayer: return "Dragon Slayer";
                case Wiz4ItemIndex.HelmPlus1: return "Helm +1";
                case Wiz4ItemIndex.JeweledAmulet: return "Jeweled Amulet";
                case Wiz4ItemIndex.ScrollOfBadialA: return "Scroll of Badial A";
                case Wiz4ItemIndex.PotionOfSopic: return "Potion of Sopic";
                case Wiz4ItemIndex.LongSwordPlus2: return "Long Sword +2";
                case Wiz4ItemIndex.GoodHopeCape: return "Good Hope Cape";
                case Wiz4ItemIndex.MagiciansHat: return "Magician's Hat";
                case Wiz4ItemIndex.NovicesCap: return "Novice's Cap";
                case Wiz4ItemIndex.ScrollOfDilto: return "Scroll of Dilto";
                case Wiz4ItemIndex.CopperGloves: return "Copper Gloves";
                case Wiz4ItemIndex.InitiateTurban: return "Initiate Turban";
                case Wiz4ItemIndex.WizardSkullcap: return "Wizard Skullcap";
                case Wiz4ItemIndex.PlateMailPlus2: return "Plate Mail +2";
                case Wiz4ItemIndex.ShieldPlus2: return "Shield +2";
                case Wiz4ItemIndex.Mordorcharge: return "Mordorcharge";
                case Wiz4ItemIndex.PotionOfDial: return "Potion of Dial";
                case Wiz4ItemIndex.RingOfPorfic: return "Ring of Porfic";
                case Wiz4ItemIndex.WereSlayer: return "Were Slayer";
                case Wiz4ItemIndex.MageMasher: return "Mage Masher";
                case Wiz4ItemIndex.MaceOfCuring: return "Mace of Curing";
                case Wiz4ItemIndex.StaffOfMontino: return "Staff of Montino";
                case Wiz4ItemIndex.BladeCusinart: return "Blade Cusinart'";
                case Wiz4ItemIndex.AmuletOfBadialma: return "Amulet of Badialma";
                case Wiz4ItemIndex.RodOfFlame: return "Rod of Flame";
                case Wiz4ItemIndex.CapeOfHideA: return "Cape of Hide A";
                case Wiz4ItemIndex.CapeOfJackal: return "Cape of Jackal";
                case Wiz4ItemIndex.CapeOfHideB: return "Cape of Hide B";
                case Wiz4ItemIndex.AmuletOfMakanito: return "Amulet of Makanito";
                case Wiz4ItemIndex.DiademOfMalor: return "Diadem of Malor";
                case Wiz4ItemIndex.ScrollOfBadialB: return "Scroll of Badial B";
                case Wiz4ItemIndex.DaggerPlus2: return "Dagger +2";
                case Wiz4ItemIndex.DaggerOfSpeed: return "Dagger of Speed";
                case Wiz4ItemIndex.LichsRobes: return "Lich's Robes";
                case Wiz4ItemIndex.SkullsCap: return "Skull's Cap";
                case Wiz4ItemIndex.PotionOfMasopic: return "Potion of Masopic";
                case Wiz4ItemIndex.SilverGloves: return "Silver Gloves";
                case Wiz4ItemIndex.GetOutOfJailFree: return "Get Out of Jail Free";
                case Wiz4ItemIndex.GoldenPyrite: return "Golden Pyrite";
                case Wiz4ItemIndex.OxygenMask: return "Oxygen Mask";
                case Wiz4ItemIndex.ChroniclesOfHawkwind: return "Chronicles of Hawkwind";
                case Wiz4ItemIndex.LordsGarb: return "Lord's Garb";
                case Wiz4ItemIndex.MuramasaBlade: return "Muramasa Blade";
                case Wiz4ItemIndex.Shuriken: return "Shuriken";
                case Wiz4ItemIndex.ChainProIce: return "Chain Pro Ice";
                case Wiz4ItemIndex.RingOfHealing: return "Ring of Healing";
                case Wiz4ItemIndex.RingOfDispelling: return "Ring of Dispelling";
                case Wiz4ItemIndex.RingOfDeath: return "Ring of Death";
                case Wiz4ItemIndex.AdeptBaldness: return "Adept Baldness";
                case Wiz4ItemIndex.ArabicDiary: return "Arabic Diary";
                case Wiz4ItemIndex.DemonicChimes: return "Demonic Chimes";
                case Wiz4ItemIndex.BlackCandle: return "Black Candle";
                case Wiz4ItemIndex.BlackBox: return "Black Box";
                case Wiz4ItemIndex.StTreborRump: return "St. Trebor Rump";
                case Wiz4ItemIndex.BishsTongue: return "Bish's Tongue";
                case Wiz4ItemIndex.StRimboDigit: return "St. Rimbo Digit";
                case Wiz4ItemIndex.ArrowOfTruth: return "Arrow of Truth";
                case Wiz4ItemIndex.OrbOfDreams: return "Orb of Dreams";
                case Wiz4ItemIndex.RallyingHorn: return "Rallying Horn";
                case Wiz4ItemIndex.SignetRing: return "Signet Ring";
                case Wiz4ItemIndex.MythrilGlove: return "Mythril Glove";
                case Wiz4ItemIndex.HolyLimpWrist: return "Holy Limp Wrist";
                case Wiz4ItemIndex.TwilightCloak: return "Twilight Cloak";
                case Wiz4ItemIndex.ShadowCloak: return "Shadow Cloak";
                case Wiz4ItemIndex.ConeOfSilence: return "Cone of Silence";
                case Wiz4ItemIndex.DarknessCloak: return "Darkness Cloak";
                case Wiz4ItemIndex.NightCloak: return "Night Cloak";
                case Wiz4ItemIndex.EntropyCloak: return "Entropy Cloak";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }
    }

    public class Wiz4ItemList : Wiz123ItemList
    {
        public override bool Pad1024 { get { return false; } }
        public override WizItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return new Wiz4Item(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes()
        {
            byte[] bytes = Global.DecompressBytes(Properties.Resources.Wizardry_4_Item_List_mem);

            // Some items seem to have different in-game behavior than their bytes would normally indicate
            int iBaldAC = (int)Wiz4ItemIndex.AdeptBaldness * 46 + WizItem.Offsets.ArmorClass;
            if (bytes.Length > iBaldAC)
                bytes[iBaldAC] = 8;

            return bytes;
        }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            // The Wizardry 4 items are compressed somewhere; use the internal list instead
            return GetInternalBytes();
        }

        public Wiz4ItemList()
        {
            InitInternalList();
        }

        public override WizItem GetItem(int index, int memory = -1)
        {
            WizItem item = null;
            if (index < (int) Wiz4ItemIndex.BrokenItem || index >= (int) Wiz4ItemIndex.Last)
                index = (int)Wiz4ItemIndex.BrokenItem;

            item = Items[index - (int) Wiz4ItemIndex.BrokenItem].Clone() as Wiz4Item;
            item.MemoryIndex = memory;
            return item;
        }
    }

    public class Wiz4SearchResults : SearchResults
    {
        public List<int> Indices;

        public Wiz4SearchResults(byte[] bytes)
        {
            if (bytes == null || bytes.Length < 16)
                return;

            Items = new List<Item>();
            Indices = new List<int>();
            for (int i = 0; i < 8; i++)
            {
                int iIndex = BitConverter.ToInt16(bytes, i * 2);
                if (iIndex >= 0 && iIndex < Wiz4.Items.Count)
                {
                    Items.Add(Wiz4.CloneItem(iIndex));
                    Indices.Add(i+1);
                }
            }
        }

        public override string HeaderString { get { return "Loot!"; } }
        public override string ContainerString { get { return "Pile of bodies"; } }

        public override string ContentsString
        {
            get
            {
                if (IsEmpty)
                    return String.Empty;
                StringBuilder sb = new StringBuilder();
                int iCount = 0;
                foreach (Wiz4Item item in Items)
                    sb.AppendFormat("{0}. {1}\r\n", Indices[iCount++], item.FormatDescription(Properties.Settings.Default.ItemFormat));
                return sb.ToString();
            }
        }

        public override int CompareTo(SearchResults results)
        {
            if (!(results is Wiz4SearchResults))
                return 1;

            return base.CompareTo(results);
        }
    }
}
