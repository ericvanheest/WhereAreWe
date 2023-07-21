using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum Wiz5ItemIndex
    {
        Unknown = -1,
        BrokenItem = 0,
        Torch,             // 1  
        Lantern,           // 2  
        RubberDuck,        // 3  
        Dagger,            // 4  
        Staff,             // 5  
        ShortSword,        // 6  
        LongSword,         // 7  
        Mace,              // 8  
        BattleAxe,         // 9  
        Pike,              // 10 
        WarHammer,         // 11 
        HolyBasher,        // 12 
        LongBow,           // 13 
        ThievesBow,        // 14 
        Robes,             // 15 
        LeatherArmor,      // 16 
        ChainMail,         // 17 
        ScaleMail,         // 18 
        PlateMail,         // 19 
        TargetShield,      // 20 
        HeaterShield,      // 21 
        LeatherSallet,     // 22 
        LeatherGloves,     // 23 
        ShortSwordPlus1,   // 24 
        LongSwordPlus1,    // 25 
        Blackblade,        // 26 
        Katana,            // 27 
        BattleAxePlus1,    // 28 
        Morningstar,       // 29 
        RunedFlail,        // 30 
        Halberd,           // 31 
        LightCrossbow,     // 32 
        LeatherPlus1,      // 33 
        ChainMailPlus1,    // 34 
        ScaleMailPlus1,    // 35 
        PlateMailPlus1,    // 36 
        SilverMail,        // 37 
        TargetPlus1,       // 38 
        HeaterPlus1,       // 39 
        CrestedShield,     // 40 
        BrassSallet,       // 41 
        IronGloves,        // 42 
        Bracers,           // 43 
        LongSwordPlus2,    // 44 
        Robinsword,        // 45 
        SwordOfFire,       // 46 
        MasterKatana,      // 47 
        Soulstealer,       // 48 
        BattleAxePlus2,    // 49 
        AxeOfDeath,        // 50 
        SacredBasher,      // 51 
        FaustHalberd,      // 52 
        SilverHammer,      // 53 
        MagesYewBow,       // 54 
        HeavyCrossbow,     // 55 
        LeatherPlus2,      // 56 
        ChainMailPlus2,    // 57 
        ScaleMailPlus2,    // 58 
        PlateMailPlus2,    // 59 
        ScarletRobes,      // 60 
        EmeraldRobes,      // 61 
        HeaterPlus2,       // 62 
        Bascinet,          // 63 
        ConeOfFire,        // 64 
        SilverGloves,      // 65 
        BracersPlus1,      // 66 
        LongSwordPlus3,    // 67 
        PlateMailPlus3,    // 68 
        ShieldProMagic,    // 69 
        JeweledArmet,      // 70 
        WizardsCap,        // 71 
        GlovesOfMyrdall,   // 72 
        CloakOfCapricorn,  // 73 
        SylvanBow,         // 74 
        MuramasaKatana,    // 75 
        Odinsword,         // 76 
        GoldPlatePlus5,    // 77 
        RingOfFrozz,       // 78 
        RingOfSkulls,      // 79 
        RingOfMadi,        // 80 
        RingOfJade,        // 81 
        RingOfSolitude,    // 82 
        AnkhOfWonder,      // 83 
        AnkhOfPower,       // 84 
        AnkhOfLife,        // 85 
        AnkhOfIntellect,   // 86 
        AnkhOfSanctity,    // 87 
        AnkhOfYouth,       // 88 
        StaffOfSummoning,  // 89 
        StaffOfDeath,      // 90 
        ScrollOfKatino,    // 91 
        ScrollOfStoning,   // 92 
        ScrollOfFire,      // 93 
        ScrollOfConjuring, // 94 
        PotionOfDios,      // 95 
        PotionOfCharming,  // 96 
        PotionOfLatumofis, // 97 
        PotionOfDialko,    // 98 
        PotionOfWounding,  // 99 
        PotionOfMadi,      // 100
        KingOfDiamonds,    // 101
        QueenOfHearts,     // 102
        JackOfSpades,      // 103
        AceOfClubs,        // 104
        MunkeWand,         // 105
        LightningRod,      // 106
        LarkInACage,       // 107
        StaffOfWater,      // 108
        StaffOfFire,       // 109
        StaffOfAir,        // 110
        StaffOfEarth,      // 111
        PotionOfDemonOut,  // 112
        GoldMedallion,     // 113
        IceKey,            // 114
        TicketStubs,       // 115
        Tickets,           // 116
        SkeletonKey,       // 117
        Pocketwatch,       // 118
        Battery,           // 119
        PetrifiedDemon,    // 120
        GoldKey,           // 121
        BlueCandle,        // 122
        JeweledScepter,    // 123
        PotionOfSpiritAway,// 124
        Hacksaw,           // 125
        BottleOfRum,       // 126
        SilverKey,         // 127
        BagOfTokens,       // 128
        BrassKey,          // 129
        OrbOfLlylgamyn,    // 130
        HeartOfAbriel,     // 131
        HolyTalisman,      // 132
        AmuletOfRainbows,  // 133
        AmuletOfScreens,   // 134
        AmuletOfFlames,    // 135
        Last
    }

    public enum Wiz5ItemEffect
    {
        None = -1,
        Spell = 0,
        StrengthPlus1 = 1,
        IntellectPlus1 = 2,
        PietyPlus1 = 3,
        VitalityPlus1 = 4,
        AgilityPlus1 = 5,
        LuckPlus1 = 6,
        StrengthMinus1 = 7,
        IntellectMinus1 = 8,
        PietyMinus1 = 9,
        VitalityMinus1 = 10,
        AgilityMinus1 = 11,
        LuckMinus1 = 12,
        AgeMinus1 = 13,
        AgePlus1 = 14,
        RestoreHP = 15,
        RestoreAllCharactersHP = 16,
        Plus1d4MaxHP = 17,
        AgeMinus1_Minus1d4MaxHP = 18,
        MaximumSP = 19,
        DrainSpells = 20,
        BecomeDead = 24,
        BecomeParalyzed = 25,
        BecomePetrified = 26,
        VitalityMinus1_AgePlus1 = 27,
        VitalityPlus1_Minus1d4MaxHP = 28,
        BecomeAshes = 29,
        Heal1 = 30,
        VitalityMinus1_Plus1Minus4MaxHP = 31,
        AgilityPlus1_StrengthPlus1_LuckPlus1 = 32,
        Heal2 = 33,
        StrengthMinus1_AgilityPlus1 = 34,
        StrengthPlus1_LuckMinus1 = 35,
        PietyMinus1_AgeMinus1 = 36,
    }

    public class Wiz5Item : WizItem
    {
        public long Value2;
        private int m_iRange;
        public int Unknown01;
        public bool Special;
        public Wiz5ItemEffect Wiz5Effect;

        [Flags]
        private enum CurseFlags
        {
            Cursed =    0x01,
            Critical =  0x02,
            Special =   0x04
        }

        [Flags]
        public enum IdentifyFlags
        {
            Equipped =   0x01,
            Identified = 0x02,
            Cursed =     0x04
        }

        new public static class Offsets
        {
            public const int Location = 0;
            public const int Alignment = 2;
            public const int Cursed = 4;
            public const int Effect = 6;
            public const int BreaksInto = 10;
            public const int BreakChance = 12;
            public const int Value1 = 14;
            public const int Value2 = 20;
            public const int Range = 26;
            public const int Spell = 28;
            public const int Usable = 30;
            public const int Unknown01 = 32;
            public const int Regeneration = 34;
            public const int Protection = 36;
            public const int Resistance = 38;
            public const int ArmorClass = 40;
            public const int ToHitModifier = 42;
            public const int NumDice = 44;
            public const int NumFaces = 46;
            public const int DamageModifier = 48;
            public const int Swings = 50;
            public const int Purposed = 52;
        }

        public static string GetSpellString(Wiz5SpellIndex index)
        {
            switch (index)
            {
                case Wiz5SpellIndex.None: return "None";
                case Wiz5SpellIndex.Halito: return "Halito";
                case Wiz5SpellIndex.Mogref: return "Mogref";
                case Wiz5SpellIndex.Katino: return "Katino";
                case Wiz5SpellIndex.Dumapic: return "Dumapic";
                case Wiz5SpellIndex.Ponti: return "Ponti";
                case Wiz5SpellIndex.Melito: return "Melito";
                case Wiz5SpellIndex.Desto: return "Desto";
                case Wiz5SpellIndex.Morlis: return "Morlis";
                case Wiz5SpellIndex.Bolatu: return "Bolatu";
                case Wiz5SpellIndex.Calific: return "Calific";
                case Wiz5SpellIndex.Mahalito: return "Mahalito";
                case Wiz5SpellIndex.Cortu: return "Cortu";
                case Wiz5SpellIndex.Kantios: return "Kantios";
                case Wiz5SpellIndex.Tzalik: return "Tzalik";
                case Wiz5SpellIndex.Lahalito: return "Lahalito";
                case Wiz5SpellIndex.Litofeit: return "Litofeit";
                case Wiz5SpellIndex.Rokdo: return "Rokdo";
                case Wiz5SpellIndex.Socordi: return "Socordi";
                case Wiz5SpellIndex.Madalto: return "Madalto";
                case Wiz5SpellIndex.Palios: return "Palios";
                case Wiz5SpellIndex.Vaskyre: return "Vaskyre";
                case Wiz5SpellIndex.Bacortu: return "Bacortu";
                case Wiz5SpellIndex.Mamogref: return "Mamogref";
                case Wiz5SpellIndex.Zilwan: return "Zilwan";
                case Wiz5SpellIndex.Lokara: return "Lokara";
                case Wiz5SpellIndex.Ladalto: return "Ladalto";
                case Wiz5SpellIndex.Malor: return "Malor";
                case Wiz5SpellIndex.Mahaman: return "Mahaman";
                case Wiz5SpellIndex.Tiltowait: return "Tiltowait";
                case Wiz5SpellIndex.Mawxiwtz: return "Mawxiwtz";
                case Wiz5SpellIndex.Abriel: return "Abriel";
                case Wiz5SpellIndex.Dios: return "Dios";
                case Wiz5SpellIndex.Badios: return "Badios";
                case Wiz5SpellIndex.Milwa: return "Milwa";
                case Wiz5SpellIndex.Kalki: return "Kalki";
                case Wiz5SpellIndex.Porfic: return "Porfic";
                case Wiz5SpellIndex.Katu: return "Katu";
                case Wiz5SpellIndex.Calfo: return "Calfo";
                case Wiz5SpellIndex.Montino: return "Montino";
                case Wiz5SpellIndex.Kandi: return "Kandi";
                case Wiz5SpellIndex.Latumapic: return "Latumapic";
                case Wiz5SpellIndex.Dialko: return "Dialko";
                case Wiz5SpellIndex.Bamatu: return "Bamatu";
                case Wiz5SpellIndex.Lomilwa: return "Lomilwa";
                case Wiz5SpellIndex.Hakanido: return "Hakanido";
                case Wiz5SpellIndex.Dial: return "Dial";
                case Wiz5SpellIndex.Badial: return "Badial";
                case Wiz5SpellIndex.Latumofis: return "Latumofis";
                case Wiz5SpellIndex.Maporfic: return "Maporfic";
                case Wiz5SpellIndex.Bariko: return "Bariko";
                case Wiz5SpellIndex.Dialma: return "Dialma";
                case Wiz5SpellIndex.Di: return "Di";
                case Wiz5SpellIndex.Bamordi: return "Bamordi";
                case Wiz5SpellIndex.Mogato: return "Mogato";
                case Wiz5SpellIndex.Badi: return "Badi";
                case Wiz5SpellIndex.Loktofeit: return "Loktofeit";
                case Wiz5SpellIndex.Madi: return "Madi";
                case Wiz5SpellIndex.Labadi: return "Labadi";
                case Wiz5SpellIndex.Kakamen: return "Kakamen";
                case Wiz5SpellIndex.Mabariko: return "Mabariko";
                case Wiz5SpellIndex.Ihalon: return "Ihalon";
                case Wiz5SpellIndex.Bakadi: return "Bakadi";
                case Wiz5SpellIndex.Kadorto: return "Kadorto";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public void SetWiz5Bytes(int index, byte[] bytes, int offset = 0)
        {
            Bytes = null;
            m_index = index;

            if (bytes.Length - offset < 54)
                return;
            Bytes = new byte[54];
            Buffer.BlockCopy(bytes, offset, Bytes, 0, 54);

            int dice;
            int faces;
            int bonus;
            int swings;

            Equipped = false;
            Identified = true;
            InvCursed = false;
            Location = (WizEquipLocation)BitConverter.ToInt16(Bytes, Offsets.Location);
            WizAlign = (WizAlignment)BitConverter.ToInt16(Bytes, Offsets.Alignment);
            CurseFlags flags = (CurseFlags)BitConverter.ToInt16(Bytes, Offsets.Cursed);
            Cursed = flags.HasFlag(CurseFlags.Cursed);
            AutoKill = flags.HasFlag(CurseFlags.Critical);
            Special = flags.HasFlag(CurseFlags.Special);
            Wiz5Effect = (Wiz5ItemEffect)BitConverter.ToInt16(Bytes, Offsets.Effect);
            m_breaks = BitConverter.ToInt16(Bytes, Offsets.BreaksInto);
            BreakChance = BitConverter.ToInt16(Bytes, Offsets.BreakChance);
            WizValue = new WizardryLong(Bytes, Offsets.Value1).Number;
            Value2 = new WizardryLong(Bytes, Offsets.Value2).Number;
            Spell = BitConverter.ToInt16(Bytes, Offsets.Spell);
            Usable = (WizUsable)BitConverter.ToInt16(Bytes, Offsets.Usable);
            Regeneration = BitConverter.ToInt16(Bytes, Offsets.Regeneration);
            Protection = (WizTarget)BitConverter.ToInt16(Bytes, Offsets.Protection);
            Resistance = (WizResist)BitConverter.ToInt16(Bytes, Offsets.Resistance);
            AC = BitConverter.ToInt16(Bytes, Offsets.ArmorClass);
            ToHitModifier = BitConverter.ToInt16(Bytes, Offsets.ToHitModifier);
            dice = BitConverter.ToInt16(Bytes, Offsets.NumDice);
            faces = BitConverter.ToInt16(Bytes, Offsets.NumFaces);
            bonus = BitConverter.ToInt16(Bytes, Offsets.DamageModifier);
            swings = BitConverter.ToInt16(Bytes, Offsets.Swings) + 1;
            Damage = new BasicDamage(swings, new DamageDice(faces, dice, bonus));
            Purposed = (WizTarget)BitConverter.ToInt16(Bytes, Offsets.Purposed);
            m_iRange = BitConverter.ToInt16(Bytes, Offsets.Range);
            Unknown01 = BitConverter.ToInt16(Bytes, Offsets.Unknown01);

            switch ((Wiz5ItemIndex)index)
            {
                case Wiz5ItemIndex.ShortSword:
                    Usable = WizUsable.All & ~(WizUsable.Mage | WizUsable.Priest | WizUsable.Bishop);
                    break;
                default:
                    break;
            }
        }

        public Int16 CurseValue
        {
            get
            {
                Int16 curseFlags = 0;
                if (Cursed)
                    curseFlags |= (int)CurseFlags.Cursed;
                if (AutoKill)
                    curseFlags |= (int)CurseFlags.Critical;
                if (Special)
                    curseFlags |= (int)CurseFlags.Special;
                return curseFlags;
            }
        }

        public Int16 IdentifyFlagValue
        {
            get
            {
                Int16 IDFlags = 0;
                if (IsIdentified)
                    IDFlags |= (int)IdentifyFlags.Identified;
                if (IsEquipped)
                    IDFlags |= (int)IdentifyFlags.Equipped;
                if (InvCursed)
                    IDFlags |= (int)IdentifyFlags.Cursed;
                return IDFlags;
            }
        }

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            Global.WriteInt16(ms, (int)Location);
            Global.WriteInt16(ms, (int)WizAlign);
            Global.WriteInt16(ms, CurseValue);
            Global.WriteInt16(ms, (int)Wiz5Effect);
            Global.WriteInt16(ms, (int)Effect);
            Global.WriteInt16(ms, m_breaks);
            Global.WriteInt16(ms, BreakChance);
            ms.Write(WizardryLong.GetBytes(Value), 0, 6);
            ms.Write(WizardryLong.GetBytes(Value2), 0, 6);
            Global.WriteInt16(ms, Range);
            Global.WriteInt16(ms, (int)Spell);
            Global.WriteInt16(ms, (int)Usable);
            Global.WriteInt16(ms, Unknown01);
            Global.WriteInt16(ms, Regeneration);
            Global.WriteInt16(ms, (int)Protection);
            Global.WriteInt16(ms, (int)Resistance);
            Global.WriteInt16(ms, AC);
            Global.WriteInt16(ms, ToHitModifier);
            Global.WriteInt16(ms, Damage.Quantity);
            Global.WriteInt16(ms, Damage.Faces);
            Global.WriteInt16(ms, Damage.Bonus);
            Global.WriteInt16(ms, Math.Max(Damage.NumAttacks - 1, 0));
            Global.WriteInt16(ms, (int)Purposed);
            if (ms.Length != 54)
                throw new Exception(String.Format("Wrong Wiz5 Item Length ({0})", ms.Length));
            return ms.ToArray();
        }

        public override byte[] Serialize()
        {
            byte[] bytes = new byte[4];
            Global.SetInt16(bytes, 0, DiskIndex);
            Global.SetInt16(bytes, 2, IdentifyFlagValue);
            return bytes;
        }

        public Wiz5Item(int index, byte[] bytes, int offset = 0)
        {
            SetWiz5Bytes(index, bytes, offset);
        }

        public override long Value { get { return WizValue > 0 ? WizValue : Value2; } set { base.Value = value; } }
        public override GameNames Game { get { return GameNames.Wizardry5; } }
        public override WizItem CreateItem(int index, byte[] bytes, int offset = 0) { return new Wiz5Item(index, bytes, offset); }
        public override int Index { get { return m_index; } set { m_index = value; } }
        public override int Range { get { return m_iRange; } set { } }

        public override string RangeString
        { 
            get 
            {
                if (!IsWeapon)
                    return String.Empty;
                switch (m_iRange)
                {
                    case -1: return "Close";
                    case 1: return "Medium";
                    case 2: return "Long";
                    default: return "Short";
                }
            }
        }

        public Wiz5ItemIndex BreaksInto { get { return (Wiz5ItemIndex)m_breaks; } set { m_breaks = (int)value; } }

        public override bool Trashable
        {
            get
            {
                switch ((Wiz5ItemIndex)m_index)
                {
                    case Wiz5ItemIndex.BlueCandle:
                    case Wiz5ItemIndex.BagOfTokens:
                    case Wiz5ItemIndex.Battery:
                    case Wiz5ItemIndex.BrassKey:
                    case Wiz5ItemIndex.BottleOfRum:
                    case Wiz5ItemIndex.GoldKey:
                    case Wiz5ItemIndex.Hacksaw:
                    case Wiz5ItemIndex.IceKey:
                    case Wiz5ItemIndex.LarkInACage:
                    case Wiz5ItemIndex.OrbOfLlylgamyn:
                    case Wiz5ItemIndex.Pocketwatch:
                    case Wiz5ItemIndex.SilverKey:
                    case Wiz5ItemIndex.SkeletonKey:
                    case Wiz5ItemIndex.Tickets:
                    case Wiz5ItemIndex.TicketStubs:
                    case Wiz5ItemIndex.JackOfSpades:
                    case Wiz5ItemIndex.QueenOfHearts:
                    case Wiz5ItemIndex.KingOfDiamonds:
                    case Wiz5ItemIndex.AceOfClubs:
                    case Wiz5ItemIndex.HeartOfAbriel:
                        return true;
                    default:
                        return WizValue != 0;
                }
            }
        }

        public override string GetName(int index) { return GetName((Wiz5ItemIndex) index); }

        public override string ItemNoun
        {
            get
            {
                switch ((Wiz5ItemIndex)Index)
                {
                    case Wiz5ItemIndex.BrokenItem: return "Broken Item";
                    case Wiz5ItemIndex.AmuletOfFlames:
                    case Wiz5ItemIndex.AmuletOfScreens:
                    case Wiz5ItemIndex.AmuletOfRainbows: return "Amulet";
                    case Wiz5ItemIndex.AnkhOfPower:
                    case Wiz5ItemIndex.AnkhOfYouth:
                    case Wiz5ItemIndex.AnkhOfSanctity:
                    case Wiz5ItemIndex.AnkhOfIntellect:
                    case Wiz5ItemIndex.AnkhOfLife:
                    case Wiz5ItemIndex.AnkhOfWonder: return "Ankh";
                    case Wiz5ItemIndex.ScaleMail:
                    case Wiz5ItemIndex.LeatherPlus2:
                    case Wiz5ItemIndex.GoldPlatePlus5:
                    case Wiz5ItemIndex.ScaleMailPlus1:
                    case Wiz5ItemIndex.PlateMailPlus1:
                    case Wiz5ItemIndex.SilverMail:
                    case Wiz5ItemIndex.PlateMailPlus3:
                    case Wiz5ItemIndex.ChainMailPlus2:
                    case Wiz5ItemIndex.ScaleMailPlus2:
                    case Wiz5ItemIndex.ChainMailPlus1:
                    case Wiz5ItemIndex.PlateMail:
                    case Wiz5ItemIndex.PlateMailPlus2:
                    case Wiz5ItemIndex.LeatherPlus1:
                    case Wiz5ItemIndex.LeatherArmor:
                    case Wiz5ItemIndex.ChainMail: return "Armor";
                    case Wiz5ItemIndex.BattleAxePlus2:
                    case Wiz5ItemIndex.BattleAxe:
                    case Wiz5ItemIndex.AxeOfDeath:
                    case Wiz5ItemIndex.BattleAxePlus1: return "Axe";
                    case Wiz5ItemIndex.LarkInACage: return "Bird in a Cage";
                    case Wiz5ItemIndex.Battery: return "Black Cube";
                    case Wiz5ItemIndex.LightningRod: return "Black Rod";
                    case Wiz5ItemIndex.BottleOfRum: return "Bottle";
                    case Wiz5ItemIndex.MagesYewBow:
                    case Wiz5ItemIndex.LongBow:
                    case Wiz5ItemIndex.SylvanBow:
                    case Wiz5ItemIndex.ThievesBow: return "Bow";
                    case Wiz5ItemIndex.WizardsCap:
                    case Wiz5ItemIndex.ConeOfFire: return "Cap";
                    case Wiz5ItemIndex.QueenOfHearts:
                    case Wiz5ItemIndex.AceOfClubs:
                    case Wiz5ItemIndex.JackOfSpades:
                    case Wiz5ItemIndex.KingOfDiamonds: return "Card";
                    case Wiz5ItemIndex.CloakOfCapricorn: return "Cloak";
                    case Wiz5ItemIndex.Dagger: return "Dagger";
                    case Wiz5ItemIndex.BracersPlus1:
                    case Wiz5ItemIndex.Bracers: return "Equipment";
                    case Wiz5ItemIndex.HolyTalisman:
                    case Wiz5ItemIndex.PetrifiedDemon: return "Figurine";
                    case Wiz5ItemIndex.Morningstar:
                    case Wiz5ItemIndex.RunedFlail: return "Flail";
                    case Wiz5ItemIndex.GlovesOfMyrdall:
                    case Wiz5ItemIndex.LeatherGloves:
                    case Wiz5ItemIndex.IronGloves:
                    case Wiz5ItemIndex.SilverGloves: return "Gloves";
                    case Wiz5ItemIndex.WarHammer:
                    case Wiz5ItemIndex.SilverHammer: return "Hammer";
                    case Wiz5ItemIndex.HeartOfAbriel: return "Heart";
                    case Wiz5ItemIndex.LeatherSallet:
                    case Wiz5ItemIndex.BrassSallet:
                    case Wiz5ItemIndex.Bascinet:
                    case Wiz5ItemIndex.JeweledArmet: return "Helm";
                    case Wiz5ItemIndex.GoldKey:
                    case Wiz5ItemIndex.BrassKey:
                    case Wiz5ItemIndex.SkeletonKey:
                    case Wiz5ItemIndex.IceKey:
                    case Wiz5ItemIndex.SilverKey: return "Key";
                    case Wiz5ItemIndex.Lantern: return "Lantern";
                    case Wiz5ItemIndex.Pocketwatch:
                    case Wiz5ItemIndex.GoldMedallion: return "Locket";
                    case Wiz5ItemIndex.Mace: return "Mace";
                    case Wiz5ItemIndex.OrbOfLlylgamyn: return "Orb";
                    case Wiz5ItemIndex.JeweledScepter: return "Ornate Rod";
                    case Wiz5ItemIndex.Pike:
                    case Wiz5ItemIndex.SacredBasher:
                    case Wiz5ItemIndex.Halberd:
                    case Wiz5ItemIndex.HolyBasher:
                    case Wiz5ItemIndex.FaustHalberd: return "Pole Arm";
                    case Wiz5ItemIndex.PotionOfMadi:
                    case Wiz5ItemIndex.PotionOfWounding:
                    case Wiz5ItemIndex.PotionOfDialko:
                    case Wiz5ItemIndex.PotionOfLatumofis:
                    case Wiz5ItemIndex.PotionOfSpiritAway:
                    case Wiz5ItemIndex.PotionOfDios:
                    case Wiz5ItemIndex.PotionOfCharming:
                    case Wiz5ItemIndex.PotionOfDemonOut: return "Potion";
                    case Wiz5ItemIndex.BagOfTokens: return "Purse";
                    case Wiz5ItemIndex.RingOfJade: return "Ring";
                    case Wiz5ItemIndex.RingOfSkulls:
                    case Wiz5ItemIndex.RingOfMadi:
                    case Wiz5ItemIndex.RingOfSolitude:
                    case Wiz5ItemIndex.RingOfFrozz: return "Ring";
                    case Wiz5ItemIndex.ScarletRobes:
                    case Wiz5ItemIndex.EmeraldRobes:
                    case Wiz5ItemIndex.Robes: return "Robes";
                    case Wiz5ItemIndex.ScrollOfKatino:
                    case Wiz5ItemIndex.ScrollOfConjuring:
                    case Wiz5ItemIndex.ScrollOfFire:
                    case Wiz5ItemIndex.ScrollOfStoning: return "Scroll";
                    case Wiz5ItemIndex.HeaterPlus2:
                    case Wiz5ItemIndex.CrestedShield:
                    case Wiz5ItemIndex.TargetShield:
                    case Wiz5ItemIndex.HeaterShield:
                    case Wiz5ItemIndex.HeaterPlus1:
                    case Wiz5ItemIndex.TargetPlus1:
                    case Wiz5ItemIndex.ShieldProMagic: return "Shield";
                    case Wiz5ItemIndex.StaffOfDeath:
                    case Wiz5ItemIndex.StaffOfEarth:
                    case Wiz5ItemIndex.StaffOfAir:
                    case Wiz5ItemIndex.StaffOfFire:
                    case Wiz5ItemIndex.StaffOfWater:
                    case Wiz5ItemIndex.StaffOfSummoning:
                    case Wiz5ItemIndex.Staff: return "Staff";
                    case Wiz5ItemIndex.RubberDuck:
                    case Wiz5ItemIndex.Hacksaw: return "Strange Item";
                    case Wiz5ItemIndex.Soulstealer:
                    case Wiz5ItemIndex.MasterKatana:
                    case Wiz5ItemIndex.LongSwordPlus3:
                    case Wiz5ItemIndex.MuramasaKatana:
                    case Wiz5ItemIndex.LongSwordPlus1:
                    case Wiz5ItemIndex.Blackblade:
                    case Wiz5ItemIndex.ShortSword:
                    case Wiz5ItemIndex.ShortSwordPlus1:
                    case Wiz5ItemIndex.Katana:
                    case Wiz5ItemIndex.SwordOfFire:
                    case Wiz5ItemIndex.Robinsword:
                    case Wiz5ItemIndex.LongSwordPlus2:
                    case Wiz5ItemIndex.LongSword:
                    case Wiz5ItemIndex.Odinsword: return "Sword";
                    case Wiz5ItemIndex.BlueCandle: return "Tallow Stick";
                    case Wiz5ItemIndex.TicketStubs: return "Ticket Stubs";
                    case Wiz5ItemIndex.Tickets: return "Tickets";
                    case Wiz5ItemIndex.Torch: return "Torch";
                    case Wiz5ItemIndex.MunkeWand: return "Wand";
                    case Wiz5ItemIndex.LightCrossbow:
                    case Wiz5ItemIndex.HeavyCrossbow: return "Weapon";
                    default: return String.Format("Unknown Item");
                }
            }
        }

        public static string GetName(Wiz5ItemIndex index)
        {
            switch (index)
            {
                case Wiz5ItemIndex.BrokenItem: return "Broken Item";
                case Wiz5ItemIndex.Torch: return "Torch";
                case Wiz5ItemIndex.Lantern: return "Lantern";
                case Wiz5ItemIndex.RubberDuck: return "Rubber Duck";
                case Wiz5ItemIndex.Dagger: return "Dagger";
                case Wiz5ItemIndex.Staff: return "Staff";
                case Wiz5ItemIndex.ShortSword: return "Short Sword";
                case Wiz5ItemIndex.LongSword: return "Long Sword";
                case Wiz5ItemIndex.Mace: return "Mace";
                case Wiz5ItemIndex.BattleAxe: return "Battle Axe";
                case Wiz5ItemIndex.Pike: return "Pike";
                case Wiz5ItemIndex.WarHammer: return "War Hammer";
                case Wiz5ItemIndex.HolyBasher: return "Holy Basher";
                case Wiz5ItemIndex.LongBow: return "Long Bow";
                case Wiz5ItemIndex.ThievesBow: return "Thieves Bow";
                case Wiz5ItemIndex.Robes: return "Robes";
                case Wiz5ItemIndex.LeatherArmor: return "Leather Armor";
                case Wiz5ItemIndex.ChainMail: return "Chain Mail";
                case Wiz5ItemIndex.ScaleMail: return "Scale Mail";
                case Wiz5ItemIndex.PlateMail: return "Plate Mail";
                case Wiz5ItemIndex.TargetShield: return "Target Shield";
                case Wiz5ItemIndex.HeaterShield: return "Heater Shield";
                case Wiz5ItemIndex.LeatherSallet: return "Leather Sallet";
                case Wiz5ItemIndex.LeatherGloves: return "Leather Gloves";
                case Wiz5ItemIndex.ShortSwordPlus1: return "Short Sword +1";
                case Wiz5ItemIndex.LongSwordPlus1: return "Long Sword +1";
                case Wiz5ItemIndex.Blackblade: return "Blackblade";
                case Wiz5ItemIndex.Katana: return "Katana";
                case Wiz5ItemIndex.BattleAxePlus1: return "Battle Axe +1";
                case Wiz5ItemIndex.Morningstar: return "Morningstar";
                case Wiz5ItemIndex.RunedFlail: return "Runed Flail";
                case Wiz5ItemIndex.Halberd: return "Halberd";
                case Wiz5ItemIndex.LightCrossbow: return "Light Crossbow";
                case Wiz5ItemIndex.LeatherPlus1: return "Leather Armor +1";
                case Wiz5ItemIndex.ChainMailPlus1: return "Chain Mail +1";
                case Wiz5ItemIndex.ScaleMailPlus1: return "Scale Mail +1";
                case Wiz5ItemIndex.PlateMailPlus1: return "Plate Mail +1";
                case Wiz5ItemIndex.SilverMail: return "Silver Mail";
                case Wiz5ItemIndex.TargetPlus1: return "Target Shield +1";
                case Wiz5ItemIndex.HeaterPlus1: return "Heater Shield +1";
                case Wiz5ItemIndex.CrestedShield: return "Crested Shield";
                case Wiz5ItemIndex.BrassSallet: return "Brass Sallet";
                case Wiz5ItemIndex.IronGloves: return "Iron Gloves";
                case Wiz5ItemIndex.Bracers: return "Bracers";
                case Wiz5ItemIndex.LongSwordPlus2: return "Long Sword +2";
                case Wiz5ItemIndex.Robinsword: return "Robinsword";
                case Wiz5ItemIndex.SwordOfFire: return "Sword of Fire";
                case Wiz5ItemIndex.MasterKatana: return "Master Katana";
                case Wiz5ItemIndex.Soulstealer: return "Soulstealer";
                case Wiz5ItemIndex.BattleAxePlus2: return "Battle Axe +2";
                case Wiz5ItemIndex.AxeOfDeath: return "Axe of Death";
                case Wiz5ItemIndex.SacredBasher: return "Sacred Basher";
                case Wiz5ItemIndex.FaustHalberd: return "Faust Halberd";
                case Wiz5ItemIndex.SilverHammer: return "Silver Hammer";
                case Wiz5ItemIndex.MagesYewBow: return "Mage's Yew Bow";
                case Wiz5ItemIndex.HeavyCrossbow: return "Heavy Crossbow";
                case Wiz5ItemIndex.LeatherPlus2: return "Leather Armor +2";
                case Wiz5ItemIndex.ChainMailPlus2: return "Chain Mail +2";
                case Wiz5ItemIndex.ScaleMailPlus2: return "Scale Mail +2";
                case Wiz5ItemIndex.PlateMailPlus2: return "Plate Mail +2";
                case Wiz5ItemIndex.ScarletRobes: return "Scarlet Robes";
                case Wiz5ItemIndex.EmeraldRobes: return "Emerald Robes";
                case Wiz5ItemIndex.HeaterPlus2: return "Heater Shield +2";
                case Wiz5ItemIndex.Bascinet: return "Bascinet";
                case Wiz5ItemIndex.ConeOfFire: return "Cone of Fire";
                case Wiz5ItemIndex.SilverGloves: return "Silver Gloves";
                case Wiz5ItemIndex.BracersPlus1: return "Bracers +1";
                case Wiz5ItemIndex.LongSwordPlus3: return "Long Sword +3";
                case Wiz5ItemIndex.PlateMailPlus3: return "Plate Mail +3";
                case Wiz5ItemIndex.ShieldProMagic: return "Shield Pro Magic";
                case Wiz5ItemIndex.JeweledArmet: return "Jeweled Armet";
                case Wiz5ItemIndex.WizardsCap: return "Wizard's Cap";
                case Wiz5ItemIndex.GlovesOfMyrdall: return "Gloves of Myrdall";
                case Wiz5ItemIndex.CloakOfCapricorn: return "Cloak of Capricorn";
                case Wiz5ItemIndex.SylvanBow: return "Sylvan Bow";
                case Wiz5ItemIndex.MuramasaKatana: return "Muramasa Katana";
                case Wiz5ItemIndex.Odinsword: return "Odinsword";
                case Wiz5ItemIndex.GoldPlatePlus5: return "Gold Plate Mail +5";
                case Wiz5ItemIndex.RingOfFrozz: return "Ring of Frozz";
                case Wiz5ItemIndex.RingOfSkulls: return "Ring of Skulls";
                case Wiz5ItemIndex.RingOfMadi: return "Ring of Madi";
                case Wiz5ItemIndex.RingOfJade: return "Ring of Jade";
                case Wiz5ItemIndex.RingOfSolitude: return "Ring of Solitude";
                case Wiz5ItemIndex.AnkhOfWonder: return "Ankh of Wonder";
                case Wiz5ItemIndex.AnkhOfPower: return "Ankh of Power";
                case Wiz5ItemIndex.AnkhOfLife: return "Ankh of Life";
                case Wiz5ItemIndex.AnkhOfIntellect: return "Ankh of Intellect";
                case Wiz5ItemIndex.AnkhOfSanctity: return "Ankh of Sanctity";
                case Wiz5ItemIndex.AnkhOfYouth: return "Ankh of Youth";
                case Wiz5ItemIndex.StaffOfSummoning: return "Staff of Summoning";
                case Wiz5ItemIndex.StaffOfDeath: return "Staff of Death";
                case Wiz5ItemIndex.ScrollOfKatino: return "Scroll of Katino";
                case Wiz5ItemIndex.ScrollOfStoning: return "Scroll of Stoning";
                case Wiz5ItemIndex.ScrollOfFire: return "Scroll of Fire";
                case Wiz5ItemIndex.ScrollOfConjuring: return "Scroll of Conjuring";
                case Wiz5ItemIndex.PotionOfDios: return "Potion of Dios";
                case Wiz5ItemIndex.PotionOfCharming: return "Potion of Charming";
                case Wiz5ItemIndex.PotionOfLatumofis: return "Potion of Latumofis";
                case Wiz5ItemIndex.PotionOfDialko: return "Potion of Dialko";
                case Wiz5ItemIndex.PotionOfWounding: return "Potion of Wounding";
                case Wiz5ItemIndex.PotionOfMadi: return "Potion of Madi";
                case Wiz5ItemIndex.KingOfDiamonds: return "King of Diamonds";
                case Wiz5ItemIndex.QueenOfHearts: return "Queen of Hearts";
                case Wiz5ItemIndex.JackOfSpades: return "Jack of Spades";
                case Wiz5ItemIndex.AceOfClubs: return "Ace of Clubs";
                case Wiz5ItemIndex.MunkeWand: return "Munke Wand";
                case Wiz5ItemIndex.LightningRod: return "Lightning Rod";
                case Wiz5ItemIndex.LarkInACage: return "Lark in a Cage";
                case Wiz5ItemIndex.StaffOfWater: return "Staff of Water";
                case Wiz5ItemIndex.StaffOfFire: return "Staff of Fire";
                case Wiz5ItemIndex.StaffOfAir: return "Staff of Air";
                case Wiz5ItemIndex.StaffOfEarth: return "Staff of Earth";
                case Wiz5ItemIndex.PotionOfDemonOut: return "Potion of Demon-Out";
                case Wiz5ItemIndex.GoldMedallion: return "Gold Medallion";
                case Wiz5ItemIndex.IceKey: return "Ice Key";
                case Wiz5ItemIndex.TicketStubs: return "Ticket Stubs";
                case Wiz5ItemIndex.Tickets: return "Tickets";
                case Wiz5ItemIndex.SkeletonKey: return "Skeleton Key";
                case Wiz5ItemIndex.Pocketwatch: return "Pocketwatch";
                case Wiz5ItemIndex.Battery: return "Battery";
                case Wiz5ItemIndex.PetrifiedDemon: return "Petrified Demon";
                case Wiz5ItemIndex.GoldKey: return "Gold Key";
                case Wiz5ItemIndex.BlueCandle: return "Blue Candle";
                case Wiz5ItemIndex.JeweledScepter: return "Jeweled Scepter";
                case Wiz5ItemIndex.PotionOfSpiritAway: return "Potion of Spirit-Away";
                case Wiz5ItemIndex.Hacksaw: return "Hacksaw";
                case Wiz5ItemIndex.BottleOfRum: return "Bottle of Rum";
                case Wiz5ItemIndex.SilverKey: return "Silver Key";
                case Wiz5ItemIndex.BagOfTokens: return "Bag of Tokens";
                case Wiz5ItemIndex.BrassKey: return "Brass Key";
                case Wiz5ItemIndex.OrbOfLlylgamyn: return "Orb of Llylgamyn";
                case Wiz5ItemIndex.HeartOfAbriel: return "Heart of Abriel";
                case Wiz5ItemIndex.HolyTalisman: return "Holy Talisman";
                case Wiz5ItemIndex.AmuletOfRainbows: return "Amulet of Rainbows";
                case Wiz5ItemIndex.AmuletOfScreens: return "Amulet of Screens";
                case Wiz5ItemIndex.AmuletOfFlames: return "Amulet of Flames";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public static string GetEffectString(Wiz5ItemEffect index)
        {
            switch (index)
            {
                case Wiz5ItemEffect.Spell: return "Cast Spell";
                case Wiz5ItemEffect.StrengthPlus1: return "Strength +1";
                case Wiz5ItemEffect.IntellectPlus1: return "Intellect +1";
                case Wiz5ItemEffect.PietyPlus1: return "Piety +1";
                case Wiz5ItemEffect.VitalityPlus1: return "Vitality +1";
                case Wiz5ItemEffect.AgilityPlus1: return "Agility +1";
                case Wiz5ItemEffect.LuckPlus1: return "Luck +1";
                case Wiz5ItemEffect.StrengthMinus1: return "Strength -1";
                case Wiz5ItemEffect.IntellectMinus1: return "Intellect -1";
                case Wiz5ItemEffect.PietyMinus1: return "Piety -1";
                case Wiz5ItemEffect.VitalityMinus1: return "Vitality -1";
                case Wiz5ItemEffect.AgilityMinus1: return "Agility -1";
                case Wiz5ItemEffect.LuckMinus1: return "Luck -1";
                case Wiz5ItemEffect.AgeMinus1: return "Age -1";
                case Wiz5ItemEffect.AgePlus1: return "Age +1";
                case Wiz5ItemEffect.RestoreHP: return "Restore HP";
                case Wiz5ItemEffect.RestoreAllCharactersHP: return "Restore Party HP";
                case Wiz5ItemEffect.Plus1d4MaxHP: return "MaxHP +1d4";
                case Wiz5ItemEffect.AgeMinus1_Minus1d4MaxHP: return "Age -1, MaxHP -1d4";
                case Wiz5ItemEffect.MaximumSP: return "Maximum SP";
                case Wiz5ItemEffect.DrainSpells: return "Drain Spells";
                case Wiz5ItemEffect.BecomeDead: return "Become Dead";
                case Wiz5ItemEffect.BecomeParalyzed: return "Become Paralyzed";
                case Wiz5ItemEffect.BecomePetrified: return "Become Petrified";
                case Wiz5ItemEffect.VitalityMinus1_AgePlus1: return "Vitality -1, Age +1";
                case Wiz5ItemEffect.VitalityPlus1_Minus1d4MaxHP: return "Vitality +1, MaxHP -1d4";
                case Wiz5ItemEffect.BecomeAshes: return "Become Ashes";
                case Wiz5ItemEffect.Heal1: return "Heal";
                case Wiz5ItemEffect.VitalityMinus1_Plus1Minus4MaxHP: return "Vitality -1, MaxHP +1d4";
                case Wiz5ItemEffect.AgilityPlus1_StrengthPlus1_LuckPlus1: return "Agility/Strength/Luck +1";
                case Wiz5ItemEffect.Heal2: return "Restore HP";
                case Wiz5ItemEffect.StrengthMinus1_AgilityPlus1: return "Strength -1, Agility +1";
                case Wiz5ItemEffect.StrengthPlus1_LuckMinus1: return "Strength +1, Luck -1";
                case Wiz5ItemEffect.PietyMinus1_AgeMinus1: return "Piety -1, Age -1";

                default:
                    return String.Format("Unknown({0})", (int)index);
            }
        }

        public override string UseEffectString
        {
            get
            {
                if (Spell != (int)Wiz5SpellIndex.None && (Wiz5Effect == Wiz5ItemEffect.None || Wiz5Effect == Wiz5ItemEffect.Spell))
                    return String.Format("Cast {0}{1}", GetSpellString((Wiz5SpellIndex) Spell), BreakEffectString);
                if ((Wiz5Effect != Wiz5ItemEffect.None && Wiz5Effect != Wiz5ItemEffect.Spell) && Spell == (int)Wiz5SpellIndex.None)
                    return String.Format("{0}{1}", GetEffectString(Wiz5Effect), BreakEffectString);
                if ((Wiz5Effect == Wiz5ItemEffect.None || Wiz5Effect == Wiz5ItemEffect.Spell) && Spell == (int)Wiz5SpellIndex.None)
                    return String.Empty;
                switch (Wiz5Effect)
                {
                    case Wiz5ItemEffect.RestoreAllCharactersHP:
                    case Wiz5ItemEffect.Heal1:
                    case Wiz5ItemEffect.AgeMinus1_Minus1d4MaxHP:
                        return String.Format("Cast {0}{1}", GetSpellString((Wiz5SpellIndex)Spell), BreakEffectString);
                    default:
                        return String.Format("Unknown({0},{1},{2})", (int)Wiz5Effect, GetSpellString((Wiz5SpellIndex)Spell), BreakEffectString);
                }
            }
        }

    }

    public class Wiz5ItemList : Wiz123ItemList
    {
        public override bool Pad1024 { get { return false; } }
        public override WizItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return new Wiz5Item(iItemCount, bytes, iPos); }
        public override int ItemLength { get { return 54; } }

        public override byte[] GetInternalBytes()
        {
            byte[] bytes = Global.DecompressBytes(Properties.Resources.Wizardry_5_Item_List_mem);

            return bytes;
        }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            // The Wizardry 5 items are compressed somewhere; use the internal list instead
            return GetInternalBytes();
        }

        public Wiz5ItemList()
        {
            InitInternalList();
        }

        public override WizItem GetItem(int index, int memory = -1)
        {
            WizItem item = null;
            if (index < (int) Wiz5ItemIndex.BrokenItem || index >= (int) Wiz5ItemIndex.Last)
                index = (int)Wiz5ItemIndex.BrokenItem;

            item = Items[index - (int) Wiz5ItemIndex.BrokenItem].Clone() as Wiz5Item;
            item.MemoryIndex = memory;

            return item;
        }
    }

    public class Wiz5SearchResults : Wiz123SearchResults
    {
        public Wiz5SearchResults(int iRewardIndex)
        {
            RewardIndex = iRewardIndex;
        }

        public override List<WizTreasure> Treasures { get { return Wiz5.Treasures; } }

        public override int CompareTo(SearchResults results)
        {
            if (!(results is Wiz5SearchResults))
                return 1;

            return base.CompareTo(results);
        }
    }

    public class Wiz5ShopInventory : ShopInventory
    {
        public static List<ShopItem> Items = null;

        public override IEnumerable<ShopItem> AllItems { get { return Items; } }

        public Wiz5ShopInventory(byte[] bytes, int offset = 0)
        {
            if (bytes == null || bytes.Length < offset + 2)
                return;

            int iCount = BitConverter.ToInt16(bytes, offset);
            if (iCount > 9)
                return;

            Items = new List<ShopItem>(iCount);
            for (int i = 0; i < iCount; i++)
            {
                int iItem = BitConverter.ToInt16(bytes, offset + 2 + (2 * i));
                if (iItem < 0 || iItem >= Wiz5.Items.Count)
                {
                    Items = null;
                    return;
                }

                Items.Add(new ShopItem(Wiz5.Items[iItem].Clone(), -1, -1));
            }
        }
    }
}
