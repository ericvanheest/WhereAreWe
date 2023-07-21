using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum Wiz123Alignment
    {
        None = 0,
        Any = 0,
        Good,
        Neutral,
        Evil,
        Last
    }

    public enum Wiz123EquipLocation
    {
        Weapon = 0,
        Armor,
        Shield,
        Helmet,
        Gauntlets,
        Miscellaneous,
        Accessory
    }

    public enum Wiz123ItemEffect
    {
        None = -1,
        Spell = 0,
        StrengthPlusOne = 1,
        IQPlusOne = 2,
        PietyPlusOne = 3,
        VitalityPlusOne = 4,
        AgilityPlusOne = 5,
        LuckPlusOne = 6,
        StrengthMinusOne = 7,
        IQMinusOne = 8,
        PietyMinusOne = 9,
        VitalityMinusOne = 10,
        AgilityMinusOne = 11,
        LuckMinusOne = 12,
        AgeMinusOneYear = 13,
        AgePlusOneYear = 14,
        ChangeClassToSamurai = 15,
        ChangeClassToLord = 16,
        ChangeClassToNinja = 17,
        Add50000Gold = 18,
        Add50000Exp = 19,
        BecomeLost = 20,
        FullHealAndRestore = 21,
        HitPointsPlusOne = 22,
        HealParty = 23,
        ChangeClassToSamuraiLordNinja = 24,
        BecomeDead = 25,
        SpellPointsAllNine1 = 26,
        SpellPointsAllNine2 = 27,
        CrystalOfGood = 28,
        CrystalOfEvil = 29,
        CrystalOfEarithin = 30,
    }

    [Flags]
    public enum Wiz123Usable
    {
        None = 0x00,
        Fighter = 0x01,
        Mage = 0x02,
        Priest = 0x04,
        Thief = 0x08,
        Bishop = 0x10,
        Samurai = 0x20,
        Lord = 0x40,
        Ninja = 0x80,
        All = 0xff
    }

    public abstract class Wiz123Item : Item
    {
        public static string GetEffectString(Wiz123ItemEffect index)
        {
            switch (index)
            {
                case Wiz123ItemEffect.Spell: return "Cast Spell";
                case Wiz123ItemEffect.Add50000Exp: return "+50000 Exp";
                case Wiz123ItemEffect.Add50000Gold: return "+50000 Gold";
                case Wiz123ItemEffect.AgeMinusOneYear: return "Age -1 Year";
                case Wiz123ItemEffect.AgePlusOneYear: return "Age +1 Year";
                case Wiz123ItemEffect.StrengthPlusOne: return "Strength +1";
                case Wiz123ItemEffect.IQPlusOne: return "I.Q. +1";
                case Wiz123ItemEffect.PietyPlusOne: return "Piety +1";
                case Wiz123ItemEffect.VitalityPlusOne: return "Vitality +1";
                case Wiz123ItemEffect.AgilityPlusOne: return "Agility +1";
                case Wiz123ItemEffect.LuckPlusOne: return "Luck +1";
                case Wiz123ItemEffect.StrengthMinusOne: return "Strength -1";
                case Wiz123ItemEffect.IQMinusOne: return "I.Q. -1";
                case Wiz123ItemEffect.PietyMinusOne: return "Piety -1";
                case Wiz123ItemEffect.VitalityMinusOne: return "Vitality -1";
                case Wiz123ItemEffect.AgilityMinusOne: return "Agility -1";
                case Wiz123ItemEffect.LuckMinusOne: return "Luck -1";
                case Wiz123ItemEffect.ChangeClassToNinja: return "Change class to Ninja";
                case Wiz123ItemEffect.ChangeClassToLord: return "Change class to Lord";
                case Wiz123ItemEffect.ChangeClassToSamurai: return "Change class to Samurai";
                case Wiz123ItemEffect.HealParty: return "Heal all HP";
                case Wiz123ItemEffect.HitPointsPlusOne: return "HP +1";
                case Wiz123ItemEffect.FullHealAndRestore: return "Heal all HP, restore condition";
                case Wiz123ItemEffect.BecomeLost: return "Set condition to Lost";
                case Wiz123ItemEffect.ChangeClassToSamuraiLordNinja: return "Change to Ninja/Lord/Samurai";
                case Wiz123ItemEffect.BecomeDead: return "Set condition to Dead";
                case Wiz123ItemEffect.SpellPointsAllNine1: return "Set all SP to 9";
                case Wiz123ItemEffect.SpellPointsAllNine2: return "Set all SP to 9";
                case Wiz123ItemEffect.CrystalOfGood: return "Invoke the Crystal of Good";
                case Wiz123ItemEffect.CrystalOfEvil: return "Invoke the Crystal of Evil";
                case Wiz123ItemEffect.CrystalOfEarithin: return "Invoke the Crystal of Earithin";

                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public static string GetAlignmentString(Wiz123Alignment index)
        {
            switch (index)
            {
                case Wiz123Alignment.None: return "None";
                case Wiz123Alignment.Good: return "Good";
                case Wiz123Alignment.Neutral: return "Neutral";
                case Wiz123Alignment.Evil: return "Evil";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public override string UsableByAlignment { get { return Alignment == GenericAlignmentValue.None ? String.Empty : GetAlignmentString(WizAlign); } }
        public override EquipLocation CanEquipLocation
        {
            get 
            {
                switch (Location)
                {
                    case Wiz123EquipLocation.Accessory: return EquipLocation.Accessory;
                    case Wiz123EquipLocation.Armor: return EquipLocation.Torso;
                    case Wiz123EquipLocation.Gauntlets: return EquipLocation.Gauntlet;
                    case Wiz123EquipLocation.Helmet: return EquipLocation.Head;
                    case Wiz123EquipLocation.Miscellaneous: return EquipLocation.Accessory;
                    case Wiz123EquipLocation.Shield: return EquipLocation.LeftHand;
                    case Wiz123EquipLocation.Weapon: return EquipLocation.RightHand;
                    default: return EquipLocation.None;
                }
            }
        }

        public static string GetEquipLocationString(Wiz123EquipLocation index)
        {
            switch (index)
            {
                case Wiz123EquipLocation.Weapon: return "Weapon";
                case Wiz123EquipLocation.Armor: return "Armor";
                case Wiz123EquipLocation.Shield: return "Shield";
                case Wiz123EquipLocation.Helmet: return "Helmet";
                case Wiz123EquipLocation.Gauntlets: return "Gauntlets";
                case Wiz123EquipLocation.Miscellaneous: return "Miscellaneous";
                case Wiz123EquipLocation.Accessory: return "Accessory";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public override ItemType Type
        {
            get
            {
                switch (Location)
                {
                    case Wiz123EquipLocation.Weapon: return ItemType.Weapon;
                    case Wiz123EquipLocation.Armor:
                    case Wiz123EquipLocation.Shield:
                    case Wiz123EquipLocation.Helmet:
                    case Wiz123EquipLocation.Gauntlets: return ItemType.Armor;
                    case Wiz123EquipLocation.Miscellaneous: return ItemType.Miscellaneous;
                    case Wiz123EquipLocation.Accessory: return ItemType.Accessory;
                    default: return ItemType.None;
                }
            }
            set {}
        }

        public override string TypeString { get { return GetEquipLocationString(Location); } }

        public static string GetSpellString(Wiz123SpellIndex index)
        {
            switch (index)
            {
                case Wiz123SpellIndex.None: return "None";
                case Wiz123SpellIndex.Halito: return "Halito";
                case Wiz123SpellIndex.Mogref: return "Mogref";
                case Wiz123SpellIndex.Katino: return "Katino";
                case Wiz123SpellIndex.Dumapic: return "Dumapic";
                case Wiz123SpellIndex.Dilto: return "Dilto";
                case Wiz123SpellIndex.Sopic: return "Sopic";
                case Wiz123SpellIndex.Mahalito: return "Mahalito";
                case Wiz123SpellIndex.Molito: return "Molito";
                case Wiz123SpellIndex.Morlis: return "Morlis";
                case Wiz123SpellIndex.Dalto: return "Dalto";
                case Wiz123SpellIndex.Lahalito: return "Lahalito";
                case Wiz123SpellIndex.Mamorlis: return "Mamorlis";
                case Wiz123SpellIndex.Makanito: return "Makanito";
                case Wiz123SpellIndex.Madalto: return "Madalto";
                case Wiz123SpellIndex.Lakanito: return "Lakanito";
                case Wiz123SpellIndex.Zilwan: return "Zilwan";
                case Wiz123SpellIndex.Masopic: return "Masopic";
                case Wiz123SpellIndex.Haman: return "Haman";
                case Wiz123SpellIndex.Malor: return "Malor";
                case Wiz123SpellIndex.Mahaman: return "Mahaman";
                case Wiz123SpellIndex.Tiltowait: return "Tiltowait";
                case Wiz123SpellIndex.Kalki: return "Kalki";
                case Wiz123SpellIndex.Dios: return "Dios";
                case Wiz123SpellIndex.Badios: return "Badios";
                case Wiz123SpellIndex.Milwa: return "Milwa";
                case Wiz123SpellIndex.Porfic: return "Porfic";
                case Wiz123SpellIndex.Matu: return "Matu";
                case Wiz123SpellIndex.Calfo: return "Calfo";
                case Wiz123SpellIndex.Manifo: return "Manifo";
                case Wiz123SpellIndex.Montino: return "Montino";
                case Wiz123SpellIndex.Lomilwa: return "Lomilwa";
                case Wiz123SpellIndex.Dialko: return "Dialko";
                case Wiz123SpellIndex.Latumapic: return "Latumapic";
                case Wiz123SpellIndex.Bamatu: return "Bamatu";
                case Wiz123SpellIndex.Dial: return "Dial";
                case Wiz123SpellIndex.Badial: return "Badial";
                case Wiz123SpellIndex.Latumofis: return "Latumofis";
                case Wiz123SpellIndex.Maporfic: return "Maporfic";
                case Wiz123SpellIndex.Dialma: return "Dialma";
                case Wiz123SpellIndex.Badialma: return "Badialma";
                case Wiz123SpellIndex.Litokan: return "Litokan";
                case Wiz123SpellIndex.Kandi: return "Kandi";
                case Wiz123SpellIndex.Di: return "Di";
                case Wiz123SpellIndex.Badi: return "Badi";
                case Wiz123SpellIndex.Lorto: return "Lorto";
                case Wiz123SpellIndex.Madi: return "Madi";
                case Wiz123SpellIndex.Mabadi: return "Mabadi";
                case Wiz123SpellIndex.Loktofeit: return "Loktofeit";
                case Wiz123SpellIndex.Malikto: return "Malikto";
                case Wiz123SpellIndex.Kadorto: return "Kadorto";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public static string GetSingleUsableString(Wiz123Usable index)
        {
            switch (index)
            {
                case Wiz123Usable.None: return "None";
                case Wiz123Usable.Fighter: return "Fighter";
                case Wiz123Usable.Mage: return "Mage";
                case Wiz123Usable.Priest: return "Priest";
                case Wiz123Usable.Thief: return "Thief";
                case Wiz123Usable.Bishop: return "Bishop";
                case Wiz123Usable.Samurai: return "Samurai";
                case Wiz123Usable.Lord: return "Lord";
                case Wiz123Usable.Ninja: return "Ninja";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public static string GetSingleTargetString(Wiz123Target index)
        {
            switch (index)
            {
                case Wiz123Target.None: return "None";
                case Wiz123Target.Fighter: return "Fighter";
                case Wiz123Target.Mage: return "Mage";
                case Wiz123Target.Priest: return "Priest";
                case Wiz123Target.Rogue: return "Rogue";
                case Wiz123Target.Midget: return "Midget";
                case Wiz123Target.Giant: return "Giant";
                case Wiz123Target.Mythical: return "Mythical";
                case Wiz123Target.Dragon: return "Dragon";
                case Wiz123Target.Beast: return "Beast";
                case Wiz123Target.Were: return "Were";
                case Wiz123Target.Undead: return "Undead";
                case Wiz123Target.Demon: return "Demon";
                case Wiz123Target.Insect: return "Insect";
                case Wiz123Target.Construct: return "Construct";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public static string GetSingleResistString(Wiz123Resist index)
        {
            if (index == Wiz123Resist.All)
                return "All";

            switch (index)
            {
                case Wiz123Resist.None: return "None";
                case Wiz123Resist.Physical: return "Physical";
                case Wiz123Resist.Fire: return "Fire";
                case Wiz123Resist.Cold: return "Cold";
                case Wiz123Resist.Poison: return "Poison";
                case Wiz123Resist.LevelDrain: return "LevelDrain";
                case Wiz123Resist.Stone: return "Stone";
                case Wiz123Resist.Magic: return "Magic";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public static string GetUsableFlagsString(Wiz123Usable index, bool bAbbreviated = false)
        {
            if (index == Wiz123Usable.None)
                return bAbbreviated ? "" : "None";
            if (index == Wiz123Usable.All)
                return bAbbreviated ? "FMPTBSLN" : "All";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                Wiz123Usable single = (Wiz123Usable)(1 << i);
                if (index.HasFlag(single))
                {
                    sb.Append(bAbbreviated ? GetSingleUsableString(single).Substring(0, 1) : GetSingleUsableString(single));
                    if (!bAbbreviated)
                        sb.Append(", ");
                }
            }
            return Global.Trim(sb).ToString();
        }

        public override string UsableString { get { return GetUsableFlagsString(Usable, true); } }

        public static string GetTargetString(Wiz123Target index, bool bAbbreviated = false)
        {
            if (index == Wiz123Target.Werdna)
                return bAbbreviated ? "All" : "All except Construct";

            if (index == Wiz123Target.All)
                return "All";

            if (index == Wiz123Target.None)
                return bAbbreviated ? "" : "None";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                Wiz123Target single = (Wiz123Target)(1 << i);
                if (index.HasFlag(single))
                {
                    sb.AppendFormat("{0},", bAbbreviated ? GetSingleTargetString(single).Substring(0, 2) : GetSingleTargetString(single));
                    if (!bAbbreviated)
                        sb.Append(" ");
                }
            }

            return Global.Trim(sb).ToString();
        }

        public static string GetResistString(Wiz123Resist index, bool bAbbreviated = false)
        {
            if (index == Wiz123Resist.All)
                return "All";

            if (index == Wiz123Resist.None)
                return bAbbreviated ? "" : "None";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                Wiz123Resist single = (Wiz123Resist)(1 << i);
                if (index.HasFlag(single))
                {
                    sb.AppendFormat("{0},", bAbbreviated ? GetSingleResistString(single).Substring(0, 2) : GetSingleResistString(single));
                    if (!bAbbreviated)
                        sb.Append(" ");
                }
            }
            return Global.Trim(sb).ToString();
        }

        public byte[] Bytes;

        protected int m_index;
        protected int m_breaks;
        public Wiz123Alignment WizAlign;
        public Wiz123EquipLocation Location;
        public bool Identified;
        public bool Equipped;
        public bool InvCursed;
        public Wiz123ItemEffect Effect;
        public int BreakChance;
        public int BoltacNum;
        public Wiz123SpellIndex Spell;
        public Wiz123Usable Usable;
        public int Regeneration;
        public Wiz123Target Protection;
        public Wiz123Resist Resistance;
        public int ToHitModifier;
        public int Swings;
        public int AC;
        public bool AutoKill;
        public Wiz123Target Purposed;
        public BasicDamage Damage;
        public long WizValue;

        public static class Offsets
        {
            public const int Location = 0;
            public const int Alignment = 2;
            public const int Cursed = 4;
            public const int Effect = 6;
            public const int BreaksInto = 8;
            public const int BreakChance = 10;
            public const int Value = 12;
            public const int BoltacNum = 18;
            public const int Spell = 20;
            public const int Usable = 22;
            public const int Regeneration = 24;
            public const int Protection = 26;
            public const int Resistance = 28;
            public const int ArmorClass = 30;
            public const int ToHitModifier = 32;
            public const int NumDice = 34;
            public const int NumFaces = 36;
            public const int DamageModifier = 38;
            public const int Swings = 40;
            public const int AutoKill = 42;
            public const int Purposed = 44;
        }

        public void SetBytes(int index, byte[] bytes, int offset = 0)
        {
            Bytes = null;
            Index = index;

            if (bytes.Length - offset < 46)
                return;
            Bytes = new byte[46];
            Buffer.BlockCopy(bytes, offset, Bytes, 0, 46);

            int dice;
            int faces;
            int bonus;
            int swings;

            Equipped = false;
            Identified = true;
            InvCursed = false;
            Location = (Wiz123EquipLocation)BitConverter.ToInt16(Bytes, Offsets.Location);
            WizAlign = (Wiz123Alignment)BitConverter.ToInt16(Bytes, Offsets.Alignment);
            Cursed = BitConverter.ToInt16(Bytes, Offsets.Cursed) == -1;
            Effect = (Wiz123ItemEffect)BitConverter.ToInt16(Bytes, Offsets.Effect);
            m_breaks = BitConverter.ToInt16(Bytes, Offsets.BreaksInto);
            BreakChance = BitConverter.ToInt16(Bytes, Offsets.BreakChance);
            WizValue = new WizardryLong(Bytes, Offsets.Value).Number;
            BoltacNum = BitConverter.ToInt16(Bytes, Offsets.BoltacNum);
            Spell = (Wiz123SpellIndex)BitConverter.ToInt16(Bytes, Offsets.Spell);
            Usable = (Wiz123Usable)BitConverter.ToInt16(Bytes, Offsets.Usable);
            Regeneration = BitConverter.ToInt16(Bytes, Offsets.Regeneration);
            Protection = (Wiz123Target)BitConverter.ToInt16(Bytes, Offsets.Protection);
            Resistance = (Wiz123Resist)BitConverter.ToInt16(Bytes, Offsets.Resistance);
            AC = BitConverter.ToInt16(Bytes, Offsets.ArmorClass);
            ToHitModifier = BitConverter.ToInt16(Bytes, Offsets.ToHitModifier);
            dice = BitConverter.ToInt16(Bytes, Offsets.NumDice);
            faces = BitConverter.ToInt16(Bytes, Offsets.NumFaces);
            bonus = BitConverter.ToInt16(Bytes, Offsets.DamageModifier);
            swings = BitConverter.ToInt16(Bytes, Offsets.Swings) + 1;
            Damage = new BasicDamage(swings, new DamageDice(faces, dice, bonus));
            AutoKill = BitConverter.ToInt16(Bytes, Offsets.AutoKill) == -1;
            Purposed = (Wiz123Target)BitConverter.ToInt16(Bytes, Offsets.Purposed);
        }

        public byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            Global.WriteInt16(ms, (int) Location);
            Global.WriteInt16(ms, (int)WizAlign);
            Global.WriteInt16(ms, Cursed ? -1 : 0);
            Global.WriteInt16(ms, (int)Effect);
            Global.WriteInt16(ms, m_breaks);
            Global.WriteInt16(ms, BreakChance);
            ms.Write(WizardryLong.GetBytes(Value), 0, 6);
            Global.WriteInt16(ms, BoltacNum);
            Global.WriteInt16(ms, (int) Spell);
            Global.WriteInt16(ms, (int) Usable);
            Global.WriteInt16(ms, Regeneration);
            Global.WriteInt16(ms, (int) Protection);
            Global.WriteInt16(ms, (int) Resistance);
            Global.WriteInt16(ms, AC);
            Global.WriteInt16(ms, ToHitModifier);
            Global.WriteInt16(ms, Damage.Quantity);
            Global.WriteInt16(ms, Damage.Faces);
            Global.WriteInt16(ms, Damage.Bonus);
            Global.WriteInt16(ms, Damage.NumAttacks);
            Global.WriteInt16(ms, AutoKill ? -1 : 0);
            Global.WriteInt16(ms, (int) Purposed);
            return ms.ToArray();
        }

        public virtual int DiskIndex { get { return Index; } set { Index = value; } }
        public static string PositivePlus(int i) { return i > 0 ? String.Format("+{0}", i) : i.ToString(); }
        public override int ArmorClass { get { return AC; } }
        public override GenericAlignmentValue Alignment { get { return Wiz123Character.GetGenericAlignment(WizAlign); } }
        public override bool Broken { get { return m_index == 0; } }
        public override BasicDamage BaseDamage { get { return Damage; } }
        public override int ToHitBonus { get { return ToHitModifier; } }
        public override int EquipBonusValue { get { return ToHitModifier; } }
        public override string ValueString { get { return Value.ToString(); } }
        public override long Value { get { return WizValue; } set { WizValue = value; } }
        public override int ChargesCurrent { get { return -1; } set { } }
        public override bool IsIdentified { get { return Identified; } }

        public override string ElementString   // overloaded to be "purposed toward"
        {
            get
            {
                if (Purposed == Wiz123Target.None)
                    return String.Empty;
                return GetTargetString(Purposed);
            }
        }

        public override string MaterialString   // overloaded to be "protects from"
        {
            get
            {
                if (Protection == Wiz123Target.None)
                    return String.Empty;
                return GetTargetString(Protection);
            }
        }

        public override string UseEffectString
        {
            get
            {
                if (Spell != Wiz123SpellIndex.None && (Effect == Wiz123ItemEffect.None || Effect == Wiz123ItemEffect.Spell))
                    return String.Format("Cast {0}{1}", GetSpellString(Spell), BreakEffectString);
                if ((Effect != Wiz123ItemEffect.None && Effect != Wiz123ItemEffect.Spell) && Spell == Wiz123SpellIndex.None)
                    return String.Format("{0}{1}", GetEffectString(Effect), BreakEffectString);
                if ((Effect == Wiz123ItemEffect.None || Effect == Wiz123ItemEffect.Spell) && Spell == Wiz123SpellIndex.None)
                    return String.Empty;
                return String.Format("{0} (equip) or cast {1} (use){2}", GetEffectString(Effect), GetSpellString(Spell), BreakEffectString);
            }
        }

        public override string AttributeString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Cursed)
                    sb.Append("Cursed, ");

                if (Regeneration != 0)
                    sb.AppendFormat("Regen {0}, ", Global.AddPlus(Regeneration));

                if (AutoKill)
                    sb.AppendFormat("Critical, ");

                return Global.Trim(sb).ToString();
            }
        }

        public virtual string GetName(int index) { return String.Format("Unknown({0})", index); }

        public string BreakEffectString
        {
            get
            {
                string strInto = (m_breaks == 0 ? "" : String.Format(" into {0}", GetName(m_breaks)));
                if (BreakChance >= 100)
                    return " and break" + strInto;
                if (BreakChance > 0)
                    return String.Format(", {0}% chance to break{1}", BreakChance, strInto);
                return "";
            }
        }

        public override string ResistString
        {
            get
            {
                if (Resistance != Wiz123Resist.None)
                    return GetResistString(Resistance);
                else
                    return String.Empty;
            }
        }

        public override string EquipEffects
        {
            get 
            {
                StringBuilder sb = new StringBuilder();
                if (ToHitBonus > 0)
                    sb.AppendFormat("{0} To-Hit, ", Global.AddPlus(ToHitBonus));
                if (IsWeapon && ArmorClass != 0)
                    sb.AppendFormat("{0} AC, ", Global.AddPlus(ArmorClass));
                if (Regeneration != 0)
                    sb.AppendFormat("Regen {0}, ", Global.AddPlus(Regeneration));
                if (Resistance != Wiz123Resist.None)
                    sb.AppendFormat("Resist {0}, ", GetResistString(Resistance).Replace(", ","/"));
                if (Protection != Wiz123Target.None)
                    sb.AppendFormat("50% damage from {0}, ", GetTargetString(Protection).Replace(", ","/"));
                if (Purposed != Wiz123Target.None && IsWeapon)
                    sb.AppendFormat("2x damage vs. {0}, ", GetTargetString(Purposed).Replace(", ","/"));
                if (AutoKill && IsWeapon)
                    sb.Append("Enables criticals, ");
                return Global.Trim(sb).ToString();
            }
        }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("#{0}: {1} ({2}){3}\r\n", (int)Index, Name, GetEquipLocationString(Location), Cursed ? ", CURSED" : "");
                if (WizAlign != Wiz123Alignment.None)
                    sb.AppendFormat("Alignment: {0}\r\n", GetAlignmentString(WizAlign));
                //sb.AppendFormat("Cursed: {0}\r\n", Cursed ? "Yes" : "No");
                string strUseEffect = UseEffectString;
                if (!String.IsNullOrWhiteSpace(strUseEffect))
                    sb.AppendFormat("Effect: {0}\r\n", strUseEffect);
                //sb.AppendFormat("Unknown1: {0}\r\n", Unknown1);
                if (Usable != Wiz123Usable.None && Location != Wiz123EquipLocation.Miscellaneous)
                    sb.AppendFormat("Usable by: {0}\r\n", GetUsableFlagsString(Usable));
                if (Regeneration != 0)
                    sb.AppendFormat("Regeneration: {0}\r\n", PositivePlus(Regeneration));
                if (Protection != Wiz123Target.None)
                    sb.AppendFormat("50% damage from: {0}\r\n", GetTargetString(Protection));
                if (Resistance != Wiz123Resist.None)
                    sb.AppendFormat("Resists: {0}\r\n", GetResistString(Resistance));
                if (ArmorClass != 0 && Location != Wiz123EquipLocation.Miscellaneous)
                    sb.AppendFormat("Armor Class: {0}\r\n", PositivePlus(ArmorClass));
                if (ToHitModifier != 0)
                    sb.AppendFormat("To Hit: {0}\r\n", PositivePlus(ToHitModifier));
                if (IsWeapon)
                    sb.AppendFormat("Damage: {0}{1}\r\n", Damage.ToString(), AutoKill ? ", Enables critical hits" : "");
                //else if (AutoKill)
                //    sb.AppendFormat("Gives possibility of critical attacks\r\n");
                if (Purposed != Wiz123Target.None && IsWeapon)
                    sb.AppendFormat("2x damage vs: {0}\r\n", GetTargetString(Purposed));
                if (Value > 0)
                    sb.AppendFormat("Value: {0} Gold\r\n", Value);
                return sb.ToString();
            }
        }

        public override byte[] Serialize()
        {
            byte[] bytes = new byte[8];
            Global.SetInt16(bytes, 0, IsEquipped ? 1 : 0);
            Global.SetInt16(bytes, 2, Cursed ? 1 : 0);
            Global.SetInt16(bytes, 4, Identified ? 1 : 0);
            Global.SetInt16(bytes, 6, DiskIndex);
            return bytes;
        }

        public static Wiz123Item FromInventoryBytes(GameNames game, byte[] bytes, int offset = 0)
        {
            Wiz123Item item = null;

            switch (game)
            {
                case GameNames.Wizardry1:
                    item = Wiz1.ItemList.Value.GetItem(BitConverter.ToInt16(bytes, offset + 6));
                    break;
                case GameNames.Wizardry2:
                    item = Wiz2.ItemList.Value.GetItem(BitConverter.ToInt16(bytes, offset + 6));
                    break;
                case GameNames.Wizardry3:
                    item = Wiz3.ItemList.Value.GetItem(BitConverter.ToInt16(bytes, offset + 6));
                    break;
            }
            if (item != null)
            {
                item.Equipped = bytes[offset] != 0;
                item.InvCursed = bytes[offset + 2] != 0;
                item.Identified = bytes[offset + 4] != 0;
            }
            return item;
        }

        public override string DescriptionString { get { return Name; } }
        public override string Name { get { return GetName(m_index); } }

        public abstract Wiz123Item CreateItem(int index, byte[] bytes, int offset = 0);

        public override Item Clone()
        {
            Wiz123Item item = CreateItem(Index, GetBytes());
            item.Equipped = false;
            item.InvCursed = InvCursed;
            item.Identified = Identified;
            return item;
        }

        public override bool IsEquipped { get { return Equipped; } }

        public bool IsUsableBy(GenericClass genClass)
        {
            switch(genClass)
            {
                case GenericClass.Fighter: return Usable.HasFlag(Wiz123Usable.Fighter);
                case GenericClass.Mage: return Usable.HasFlag(Wiz123Usable.Mage);
                case GenericClass.Priest: return Usable.HasFlag(Wiz123Usable.Priest);
                case GenericClass.Thief: return Usable.HasFlag(Wiz123Usable.Thief);
                case GenericClass.Bishop: return Usable.HasFlag(Wiz123Usable.Bishop);
                case GenericClass.Samurai: return Usable.HasFlag(Wiz123Usable.Samurai);
                case GenericClass.Lord: return Usable.HasFlag(Wiz123Usable.Lord);
                case GenericClass.Ninja: return Usable.HasFlag(Wiz123Usable.Ninja);
                default: return false;
            }
        }

        public bool IsUsableBy(GenericAlignmentValue align)
        {
            if (Alignment == GenericAlignmentValue.None)
                return true;
            return Alignment == align;
        }

        public override bool IsUsableByAny(object filter)
        {
            if (filter is BaseCharacter)
                return IsUsableBy(((BaseCharacter)filter).BasicClass) && IsUsableBy(((BaseCharacter)filter).BasicAlignment.Temporary);

            if (filter is GenericClass)
                return IsUsableBy((GenericClass)filter);

            if (filter is GenericAlignmentValue)
                return IsUsableBy((GenericAlignmentValue)filter);

            return false; 
        }

        public override bool NotUsable { get { return ItemBaseType == ItemType.Miscellaneous; } }
        
        public override string ToString()
        {
            return String.Format("#{0}[{1}]: {2}", (int)Index, MemoryIndex, DescriptionString);
        }

        public static Wiz123Item CreateRandom(List<Wiz123Item> list, ItemType type, BaseCharacter charUsable)
        {
            IntDeck deck = new IntDeck(1, list.Count);
            deck.Shuffle();
            foreach(int i in deck.Cards)
            {
                if (type != ItemType.None && type != list[i].ItemBaseType)
                    continue;

                if (charUsable == null)
                    return list[i].Clone() as Wiz123Item;

                if (list[i].IsUsableByAny(charUsable))
                    return list[i].Clone() as Wiz123Item;
            }
            return list[0].Clone() as Wiz123Item;
        }

        public override CompareResult CompareTo(Item item)
        {
            if (!(item is Wiz123Item))
                return CompareResult.Uncomparable;
            if (item == this)
                return CompareResult.Identical;
            if (Type != item.Type)
                return CompareResult.Uncomparable;

            Wiz123Item wiz1Item = item as Wiz123Item;

            if (wiz1Item.Cursed && !Cursed)
                return CompareResult.Uncomparable;  // Wizardry 1 has some decent cursed items; the player needs to decide
            else if (!wiz1Item.Cursed && Cursed)
                return CompareResult.Uncomparable;

            if (CanEquipLocation != wiz1Item.CanEquipLocation)
                return CompareResult.Uncomparable;

            switch (Type)
            {
                case ItemType.Armor:
                    return CompareValues(ArmorClass, item.ArmorClass);
                case ItemType.OneHandMelee:
                case ItemType.TwoHandMelee:
                case ItemType.Weapon:
                    // Some weapons have odd properties other than damage
                    CompareResult result = CompareValues(Damage.Average, wiz1Item.Damage.Average, ToHitBonus, wiz1Item.ToHitBonus);
                    if (item.ArmorClass != ArmorClass)
                    {
                        if (result == CompareResult.Identical)
                            return CompareValues(ArmorClass, item.ArmorClass);
                        return CompareResult.Uncomparable;
                    }
                    return result;
                default:
                    //if (CanEquip && wiz1Item.CanEquip)
                    //    return CompareValues();
                    break;
            }
            return CompareResult.Uncomparable;
        }

        public override string LargestBonusEffect
        {
            get
            {
                switch (ItemBaseType)
                {
                    case ItemType.Weapon: return "To-Hit";
                    case ItemType.Armor: return "AC";
                    default:
                        if (Regeneration > 0)
                            return "Regen";
                        return String.Empty;
                }
            }
        }

        public override int LargestBonus
        {
            get 
            {
                switch (ItemBaseType)
                {
                    case ItemType.Weapon: return ToHitBonus;
                    case ItemType.Armor: return ArmorClass;
                    default:
                        if (Regeneration > 0)
                            return Regeneration;
                        return 0;
                }
            }
        }
    }

    public abstract class Wiz123ItemList
    {
        public List<Wiz123Item> Items;
        public bool Valid = false;
        public bool Internal = true;

        public virtual Wiz123Item CreateItem(int iItemCount, byte[] bytes, int iPos) { return null; }
        public virtual byte[] GetInternalBytes() { return null; }
        public virtual byte[] GetExternalBytes(MemoryHacker hacker) { return null; }

        public List<Wiz123Item> SetFromBytes(byte[] bytes)
        {
            List<Wiz123Item> items = new List<Wiz123Item>(bytes.Length / 46);

            try
            {
                // The wizardry 1 items are stored in 22-item sections, padded out to 1024 bytes each
                int iPos = 0;
                int iItemCount = 0;
                while (iPos <= bytes.Length - 46)
                {
                    items.Add(CreateItem(iItemCount, bytes, iPos));
                    iPos += 46;
                    iItemCount++;
                    if (iItemCount % 22 == 0)
                        iPos += 12;
                }
                Valid = true;
            }
            catch (Exception)
            {
                Valid = false;
            }

            return items;
        }

        public bool InitExternalList(MemoryHacker hacker)
        {
            if (!hacker.IsValid)
                return false;

            byte[] bytes = GetExternalBytes(hacker);
            if (bytes == null)
                return false;

            Items = SetFromBytes(bytes);
            Internal = false;
            return true;
        }

        public bool InitInternalList()
        {
            Items = SetFromBytes(GetInternalBytes());
            Internal = true;
            return true;
        }

        public virtual Wiz123Item GetItem(int index, int memory = -1)
        {
            Wiz123Item item = null;
            if (index < 0 || index >= Items.Count)
                index = 0;

            item = Items[index].Clone() as Wiz123Item;
            item.MemoryIndex = memory;
            return item;
        }
    }

    public class Wiz1ShopInventory : ShopInventory
    {
        public static Wiz1ItemIndex[] Boltac = new Wiz1ItemIndex[] {
            Wiz1ItemIndex.LongSword, Wiz1ItemIndex.ShortSword, Wiz1ItemIndex.AnointedMace, Wiz1ItemIndex.AnointedFlail,
            Wiz1ItemIndex.Staff, Wiz1ItemIndex.Dagger, Wiz1ItemIndex.SmallShield, Wiz1ItemIndex.LargeShield,
            Wiz1ItemIndex.Robes, Wiz1ItemIndex.LeatherArmor, Wiz1ItemIndex.ChainMail, Wiz1ItemIndex.BreastPlate,
            Wiz1ItemIndex.PlateMail, Wiz1ItemIndex.Helm, Wiz1ItemIndex.PotionOfDios, Wiz1ItemIndex.PotionOfLatumofis,
            Wiz1ItemIndex.ScrollOfKantino, Wiz1ItemIndex.LeatherPlus1, Wiz1ItemIndex.ChainMailPlus1, Wiz1ItemIndex.ShieldPlus1,
            Wiz1ItemIndex.BreastPlatePlus1, Wiz1ItemIndex.ScrollOfBadios1, Wiz1ItemIndex.ScrollOfHalito, Wiz1ItemIndex.StaffPlus2,
            Wiz1ItemIndex.GlovesOfCopper };

        public static List<ShopItem> Items = null;

        public override IEnumerable<ShopItem> AllItems { get { return Items; } }

        public Wiz1ShopInventory()
        {
            if (Items == null)
            {
                Items = new List<ShopItem>(25);
                foreach (Wiz1ItemIndex index in Boltac)
                    Items.Add(new ShopItem(Wiz1.Items[(int)index], -1, -1));
            }
        }
    }
}
