using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum WizAlignment
    {
        None = 0,
        Any = 0,
        Good,
        Neutral,
        Evil,
        Last
    }

    public enum WizEquipLocation
    {
        Weapon = 0,
        Armor,
        Shield,
        Helmet,
        Gauntlets,
        Miscellaneous,
        Accessory
    }

    public enum WizItemEffect
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
        HolyHandGrenade = 31,
        HopalongCarrot = 32,
        WingedBoots = 33,
        Bloodstone = 34,
        LandersTurquoise = 35,
        AmberDragon = 36,
        DaggerOfSpeed = 37,
        RemoveTrebor = 38,
        RemoveCurses = 39,
        AromaticBall = 40,
        CrystalRose = 41,
        OxygenMask = 42,
        GoldenPyrite = 43
    }

    [Flags]
    public enum WizUsable
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

    public abstract class WizItem : Item
    {
        public static string GetEffectString(WizItemEffect index)
        {
            switch (index)
            {
                case WizItemEffect.Spell: return "Cast Spell";
                case WizItemEffect.Add50000Exp: return "+50000 Exp";
                case WizItemEffect.Add50000Gold: return "+50000 Gold";
                case WizItemEffect.AgeMinusOneYear: return "Age -1 Year";
                case WizItemEffect.AgePlusOneYear: return "Age +1 Year";
                case WizItemEffect.StrengthPlusOne: return "Strength +1";
                case WizItemEffect.IQPlusOne: return "I.Q. +1";
                case WizItemEffect.PietyPlusOne: return "Piety +1";
                case WizItemEffect.VitalityPlusOne: return "Vitality +1";
                case WizItemEffect.AgilityPlusOne: return "Agility +1";
                case WizItemEffect.LuckPlusOne: return "Luck +1";
                case WizItemEffect.StrengthMinusOne: return "Strength -1";
                case WizItemEffect.IQMinusOne: return "I.Q. -1";
                case WizItemEffect.PietyMinusOne: return "Piety -1";
                case WizItemEffect.VitalityMinusOne: return "Vitality -1";
                case WizItemEffect.AgilityMinusOne: return "Agility -1";
                case WizItemEffect.LuckMinusOne: return "Luck -1";
                case WizItemEffect.ChangeClassToNinja: return "Change class to Ninja";
                case WizItemEffect.ChangeClassToLord: return "Change class to Lord";
                case WizItemEffect.ChangeClassToSamurai: return "Change class to Samurai";
                case WizItemEffect.HealParty: return "Heal all HP";
                case WizItemEffect.HitPointsPlusOne: return "HP +1";
                case WizItemEffect.FullHealAndRestore: return "Heal all HP, restore condition";
                case WizItemEffect.BecomeLost: return "Set condition to Lost";
                case WizItemEffect.ChangeClassToSamuraiLordNinja: return "Change to Ninja/Lord/Samurai";
                case WizItemEffect.BecomeDead: return "Set condition to Dead";
                case WizItemEffect.SpellPointsAllNine1: return "Set all SP to 9";
                case WizItemEffect.SpellPointsAllNine2: return "Set all SP to 9";
                case WizItemEffect.CrystalOfGood: return "Invoke the Crystal of Good";
                case WizItemEffect.CrystalOfEvil: return "Invoke the Crystal of Evil";
                case WizItemEffect.CrystalOfEarithin: return "Invoke the Crystal of Earithin";
                case WizItemEffect.HolyHandGrenade: return "Destroy a wall";
                case WizItemEffect.HopalongCarrot: return "Hop over a wall";
                case WizItemEffect.WingedBoots: return "Avoid traps";
                case WizItemEffect.OxygenMask: return "Protect vs. Makanito and Lakanito";
                case WizItemEffect.Bloodstone: return "Glow Ethereally";
                case WizItemEffect.LandersTurquoise: return "Glow Ethereally";
                case WizItemEffect.AmberDragon: return "Glow Ethereally";
                case WizItemEffect.DaggerOfSpeed: return "No Effect";
                case WizItemEffect.RemoveTrebor: return "Dispell Trebor's Ghost";
                case WizItemEffect.RemoveCurses: return "Un-equip cursed items";
                case WizItemEffect.AromaticBall: return "Emit noxious fumes";
                case WizItemEffect.CrystalRose: return "No Effect";
                case WizItemEffect.GoldenPyrite: return "Glow Ethereally";

                default: 
                    int iVal = (int) index;
                    if (iVal >= 200 && iVal < 300)
                        return String.Format("+{0} To-Hit", iVal - 200);
                    if (iVal >= 300 && iVal < 400)
                        return String.Format("+{0} Spell Power", iVal - 300);
                    if (iVal >= 400 && iVal < 500)
                        return String.Format("+{0} Weapon Power", iVal - 400);
                    if (iVal >= 500 && iVal < 600)
                        return String.Format("+{0} To-Hit and Weapon Power", iVal - 500);
                    if (iVal >= 600 && iVal < 700)
                        return String.Format("+{0} Spell/Weapon Power", iVal - 600);
                    if (iVal >= 700 && iVal < 800)
                        return String.Format("+{0} To-Hit and Spell/Weapon Power", iVal - 700);
                    return String.Format("Unknown({0})", iVal);
            }
        }

        public static string GetAlignmentString(WizAlignment index)
        {
            switch (index)
            {
                case WizAlignment.None: return "None";
                case WizAlignment.Good: return "Good";
                case WizAlignment.Neutral: return "Neutral";
                case WizAlignment.Evil: return "Evil";
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
                    case WizEquipLocation.Accessory: return EquipLocation.Accessory;
                    case WizEquipLocation.Armor: return EquipLocation.Torso;
                    case WizEquipLocation.Gauntlets: return EquipLocation.Gauntlet;
                    case WizEquipLocation.Helmet: return EquipLocation.Head;
                    case WizEquipLocation.Miscellaneous: return EquipLocation.Accessory;
                    case WizEquipLocation.Shield: return EquipLocation.LeftHand;
                    case WizEquipLocation.Weapon: return EquipLocation.RightHand;
                    default: return EquipLocation.None;
                }
            }
        }

        public static string GetEquipLocationString(WizEquipLocation index)
        {
            switch (index)
            {
                case WizEquipLocation.Weapon: return "Weapon";
                case WizEquipLocation.Armor: return "Armor";
                case WizEquipLocation.Shield: return "Shield";
                case WizEquipLocation.Helmet: return "Helmet";
                case WizEquipLocation.Gauntlets: return "Gauntlets";
                case WizEquipLocation.Miscellaneous: return "Miscellaneous";
                case WizEquipLocation.Accessory: return "Accessory";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public override ItemType Type
        {
            get
            {
                switch (Location)
                {
                    case WizEquipLocation.Weapon: return ItemType.Weapon;
                    case WizEquipLocation.Armor:
                    case WizEquipLocation.Shield:
                    case WizEquipLocation.Helmet:
                    case WizEquipLocation.Gauntlets: return ItemType.Armor;
                    case WizEquipLocation.Miscellaneous: return ItemType.Miscellaneous;
                    case WizEquipLocation.Accessory: return ItemType.Accessory;
                    default: return ItemType.None;
                }
            }
            set {}
        }

        public override string TypeString { get { return GetEquipLocationString(Location); } }

        public static string GetSpellString(Wiz1234SpellIndex index)
        {
            switch (index)
            {
                case Wiz1234SpellIndex.None: return "None";
                case Wiz1234SpellIndex.Halito: return "Halito";
                case Wiz1234SpellIndex.Mogref: return "Mogref";
                case Wiz1234SpellIndex.Katino: return "Katino";
                case Wiz1234SpellIndex.Dumapic: return "Dumapic";
                case Wiz1234SpellIndex.Dilto: return "Dilto";
                case Wiz1234SpellIndex.Sopic: return "Sopic";
                case Wiz1234SpellIndex.Mahalito: return "Mahalito";
                case Wiz1234SpellIndex.Molito: return "Molito";
                case Wiz1234SpellIndex.Morlis: return "Morlis";
                case Wiz1234SpellIndex.Dalto: return "Dalto";
                case Wiz1234SpellIndex.Lahalito: return "Lahalito";
                case Wiz1234SpellIndex.Mamorlis: return "Mamorlis";
                case Wiz1234SpellIndex.Makanito: return "Makanito";
                case Wiz1234SpellIndex.Madalto: return "Madalto";
                case Wiz1234SpellIndex.Lakanito: return "Lakanito";
                case Wiz1234SpellIndex.Zilwan: return "Zilwan";
                case Wiz1234SpellIndex.Masopic: return "Masopic";
                case Wiz1234SpellIndex.Haman: return "Haman";
                case Wiz1234SpellIndex.Malor: return "Malor";
                case Wiz1234SpellIndex.Mahaman: return "Mahaman";
                case Wiz1234SpellIndex.Tiltowait: return "Tiltowait";
                case Wiz1234SpellIndex.Kalki: return "Kalki";
                case Wiz1234SpellIndex.Dios: return "Dios";
                case Wiz1234SpellIndex.Badios: return "Badios";
                case Wiz1234SpellIndex.Milwa: return "Milwa";
                case Wiz1234SpellIndex.Porfic: return "Porfic";
                case Wiz1234SpellIndex.Matu: return "Matu";
                case Wiz1234SpellIndex.Calfo: return "Calfo";
                case Wiz1234SpellIndex.Manifo: return "Manifo";
                case Wiz1234SpellIndex.Montino: return "Montino";
                case Wiz1234SpellIndex.Lomilwa: return "Lomilwa";
                case Wiz1234SpellIndex.Dialko: return "Dialko";
                case Wiz1234SpellIndex.Latumapic: return "Latumapic";
                case Wiz1234SpellIndex.Bamatu: return "Bamatu";
                case Wiz1234SpellIndex.Dial: return "Dial";
                case Wiz1234SpellIndex.Badial: return "Badial";
                case Wiz1234SpellIndex.Latumofis: return "Latumofis";
                case Wiz1234SpellIndex.Maporfic: return "Maporfic";
                case Wiz1234SpellIndex.Dialma: return "Dialma";
                case Wiz1234SpellIndex.Badialma: return "Badialma";
                case Wiz1234SpellIndex.Litokan: return "Litokan";
                case Wiz1234SpellIndex.Kandi: return "Kandi";
                case Wiz1234SpellIndex.Di: return "Di";
                case Wiz1234SpellIndex.Badi: return "Badi";
                case Wiz1234SpellIndex.Lorto: return "Lorto";
                case Wiz1234SpellIndex.Madi: return "Madi";
                case Wiz1234SpellIndex.Mabadi: return "Mabadi";
                case Wiz1234SpellIndex.Loktofeit: return "Loktofeit";
                case Wiz1234SpellIndex.Malikto: return "Malikto";
                case Wiz1234SpellIndex.Kadorto: return "Kadorto";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public static string GetSingleUsableString(WizUsable index)
        {
            switch (index)
            {
                case WizUsable.None: return "None";
                case WizUsable.Fighter: return "Fighter";
                case WizUsable.Mage: return "Mage";
                case WizUsable.Priest: return "Priest";
                case WizUsable.Thief: return "Thief";
                case WizUsable.Bishop: return "Bishop";
                case WizUsable.Samurai: return "Samurai";
                case WizUsable.Lord: return "Lord";
                case WizUsable.Ninja: return "Ninja";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public static string GetSingleTargetString(WizTarget index)
        {
            switch (index)
            {
                case WizTarget.None: return "None";
                case WizTarget.Fighter: return "Fighter";
                case WizTarget.Mage: return "Mage";
                case WizTarget.Priest: return "Priest";
                case WizTarget.Rogue: return "Rogue";
                case WizTarget.Midget: return "Midget";
                case WizTarget.Giant: return "Giant";
                case WizTarget.Mythical: return "Mythical";
                case WizTarget.Dragon: return "Dragon";
                case WizTarget.Beast: return "Beast";
                case WizTarget.Were: return "Were";
                case WizTarget.Undead: return "Undead";
                case WizTarget.Demon: return "Demon";
                case WizTarget.Insect: return "Insect";
                case WizTarget.Construct: return "Construct";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public static string GetSingleResistString(WizResist index)
        {
            if (index == WizResist.All)
                return "All";

            switch (index)
            {
                case WizResist.None: return "None";
                case WizResist.Physical: return "Physical";
                case WizResist.Fire: return "Fire";
                case WizResist.Cold: return "Cold";
                case WizResist.Poison: return "Poison";
                case WizResist.LevelDrain: return "LevelDrain";
                case WizResist.Stone: return "Stone";
                case WizResist.Magic: return "Magic";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public static string GetUsableFlagsString(WizUsable index, bool bAbbreviated = false)
        {
            if (index == WizUsable.None)
                return bAbbreviated ? "" : "None";
            if (index == WizUsable.All)
                return bAbbreviated ? "FMPTBSLN" : "All";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                WizUsable single = (WizUsable)(1 << i);
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

        public static string GetTargetString(WizTarget index, bool bAbbreviated = false)
        {
            if (index == WizTarget.Werdna)
                return bAbbreviated ? "All" : "All except Construct";

            if (index == WizTarget.All)
                return "All";

            if (index == WizTarget.None)
                return bAbbreviated ? "" : "None";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                WizTarget single = (WizTarget)(1 << i);
                if (index.HasFlag(single))
                {
                    sb.AppendFormat("{0},", bAbbreviated ? GetSingleTargetString(single).Substring(0, 2) : GetSingleTargetString(single));
                    if (!bAbbreviated)
                        sb.Append(" ");
                }
            }

            return Global.Trim(sb).ToString();
        }

        public static string GetResistString(WizResist index, bool bAbbreviated = false)
        {
            if (index == WizResist.All)
                return "All";

            if (index == WizResist.None)
                return bAbbreviated ? "" : "None";

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < 16; i++)
            {
                WizResist single = (WizResist)(1 << i);
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
        public WizAlignment WizAlign;
        public WizEquipLocation Location;
        public bool Identified;
        public bool Equipped;
        public bool InvCursed;
        public WizItemEffect Effect;
        public int BreakChance;
        public int BoltacNum;
        public int Spell;
        public WizUsable Usable;
        public int Regeneration;
        public WizTarget Protection;
        public WizResist Resistance;
        public int ToHitModifier;
        public int Swings;
        public int AC;
        public bool AutoKill;
        public WizTarget Purposed;
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
            Location = (WizEquipLocation)BitConverter.ToInt16(Bytes, Offsets.Location);
            WizAlign = (WizAlignment)BitConverter.ToInt16(Bytes, Offsets.Alignment);
            Cursed = BitConverter.ToInt16(Bytes, Offsets.Cursed) == -1;
            Effect = (WizItemEffect)BitConverter.ToInt16(Bytes, Offsets.Effect);
            m_breaks = BitConverter.ToInt16(Bytes, Offsets.BreaksInto);
            BreakChance = BitConverter.ToInt16(Bytes, Offsets.BreakChance);
            WizValue = new WizardryLong(Bytes, Offsets.Value).Number;
            BoltacNum = BitConverter.ToInt16(Bytes, Offsets.BoltacNum);
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
            AutoKill = BitConverter.ToInt16(Bytes, Offsets.AutoKill) == -1;
            Purposed = (WizTarget)BitConverter.ToInt16(Bytes, Offsets.Purposed);
        }

        public virtual byte[] GetBytes()
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
            Global.WriteInt16(ms, Math.Max(Damage.NumAttacks - 1, 0));
            Global.WriteInt16(ms, AutoKill ? -1 : 0);
            Global.WriteInt16(ms, (int) Purposed);
            return ms.ToArray();
        }

        public virtual int DiskIndex { get { return Index; } set { Index = value; } }
        public static string PositivePlus(int i) { return i > 0 ? String.Format("+{0}", i) : i.ToString(); }
        public override int ArmorClass { get { return AC; } }
        public override GenericAlignmentValue Alignment { get { return WizCharacter.GetGenericAlignment(WizAlign); } }
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
                if (Purposed == WizTarget.None)
                    return String.Empty;
                return GetTargetString(Purposed);
            }
        }

        public override string MaterialString   // overloaded to be "protects from"
        {
            get
            {
                if (Protection == WizTarget.None)
                    return String.Empty;
                return GetTargetString(Protection);
            }
        }

        public override string UseEffectString
        {
            get
            {
                if (Game == GameNames.Wizardry4)
                {
                    if (Index == (int)Wiz4ItemIndex.BlackBox)
                        return "Access contents";
                    if (Index == (int)Wiz4ItemIndex.GetOutOfJailFree)
                        return "Escape a cage";
                }
                if (Spell != (int) Wiz1234SpellIndex.None && (Effect == WizItemEffect.None || Effect == WizItemEffect.Spell))
                    return String.Format("Cast {0}{1}", GetSpellString((Wiz1234SpellIndex) Spell), BreakEffectString);
                if ((Effect != WizItemEffect.None && Effect != WizItemEffect.Spell) && Spell == (int) Wiz1234SpellIndex.None)
                    return String.Format("{0}{1}", GetEffectString(Effect), BreakEffectString);
                if ((Effect == WizItemEffect.None || Effect == WizItemEffect.Spell) && Spell == (int) Wiz1234SpellIndex.None)
                    return String.Empty;
                return String.Format("{0} (equip) or cast {1} (use){2}", GetEffectString(Effect), GetSpellString((Wiz1234SpellIndex) Spell), BreakEffectString);
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
                if (Resistance != WizResist.None)
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
                    sb.AppendFormat("{0} AC, ", Global.AddPlus(-ArmorClass));
                if (Regeneration != 0)
                    sb.AppendFormat("Regen {0}, ", Global.AddPlus(Regeneration));
                if (Resistance != WizResist.None)
                    sb.AppendFormat("Resist {0}, ", GetResistString(Resistance).Replace(", ","/"));
                if (Protection != WizTarget.None)
                    sb.AppendFormat("50% damage from {0}, ", GetTargetString(Protection).Replace(", ","/"));
                if (Purposed != WizTarget.None && IsWeapon)
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
                if (WizAlign != WizAlignment.None)
                    sb.AppendFormat("Alignment: {0}\r\n", GetAlignmentString(WizAlign));
                //sb.AppendFormat("Cursed: {0}\r\n", Cursed ? "Yes" : "No");
                string strUseEffect = UseEffectString;
                if (!String.IsNullOrWhiteSpace(strUseEffect))
                    sb.AppendFormat("Effect: {0}\r\n", strUseEffect);
                //sb.AppendFormat("Unknown1: {0}\r\n", Unknown1);
                if (Usable != WizUsable.None && Location != WizEquipLocation.Miscellaneous)
                    sb.AppendFormat("Usable by: {0}\r\n", GetUsableFlagsString(Usable));
                if (Regeneration != 0)
                    sb.AppendFormat("Regeneration: {0}\r\n", PositivePlus(Regeneration));
                if (Protection != WizTarget.None)
                    sb.AppendFormat("50% damage from: {0}\r\n", GetTargetString(Protection));
                if (Resistance != WizResist.None)
                    sb.AppendFormat("Resists: {0}\r\n", GetResistString(Resistance));
                if (ArmorClass != 0 && Location != WizEquipLocation.Miscellaneous)
                    sb.AppendFormat("Armor Class: {0}\r\n", PositivePlus(ArmorClass));
                if (ToHitModifier != 0)
                    sb.AppendFormat("To Hit: {0}\r\n", PositivePlus(ToHitModifier));
                if (IsWeapon)
                {
                    sb.AppendFormat("Damage: {0}{1}\r\n", Damage.StringWithAverage, AutoKill ? ", Enables critical hits" : "");
                    if (this is Wiz5Item)
                        sb.AppendFormat("Range: {0}\r\n", RangeString);
                }
                //else if (AutoKill)
                //    sb.AppendFormat("Gives possibility of critical attacks\r\n");
                if (Purposed != WizTarget.None && IsWeapon)
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

        public static WizItem FromInventoryBytes(GameNames game, byte[] bytes, int offset = 0)
        {
            if (game == GameNames.Wizardry5)
                return FromWiz5InventoryBytes(bytes, offset);

            WizItem item = Games.GetWizGlobals(game).GetClonedItem(BitConverter.ToInt16(bytes, offset + 6) % 1000);
            if (item != null)
            {
                item.Equipped = bytes[offset] != 0;
                item.InvCursed = bytes[offset + 2] != 0;
                item.Identified = bytes[offset + 4] != 0;
            }
            return item;
        }

        public static WizItem FromWiz5InventoryBytes(byte[] bytes, int offset = 0)
        {
            WizItem item = Games.GetWizGlobals(GameNames.Wizardry5).GetClonedItem(BitConverter.ToInt16(bytes, offset));
            if (item != null)
            {
                item.InvCursed = (bytes[offset + 2] & (int) Wiz5Item.IdentifyFlags.Cursed) > 0;
                item.Identified = (bytes[offset + 2] & (int)Wiz5Item.IdentifyFlags.Identified) > 0;
                item.Equipped = (bytes[offset + 2] & (int)Wiz5Item.IdentifyFlags.Equipped) > 0;
            }
            return item;
        }

        public override string DescriptionString { get { return Name; } }
        public override string Name { get { return GetName(m_index); } }

        public abstract WizItem CreateItem(int index, byte[] bytes, int offset = 0);

        public override Item Clone()
        {
            WizItem item = CreateItem(Index, GetBytes());
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
                case GenericClass.Fighter: return Usable.HasFlag(WizUsable.Fighter);
                case GenericClass.Mage: return Usable.HasFlag(WizUsable.Mage);
                case GenericClass.Priest: return Usable.HasFlag(WizUsable.Priest);
                case GenericClass.Thief: return Usable.HasFlag(WizUsable.Thief);
                case GenericClass.Bishop: return Usable.HasFlag(WizUsable.Bishop);
                case GenericClass.Samurai: return Usable.HasFlag(WizUsable.Samurai);
                case GenericClass.Lord: return Usable.HasFlag(WizUsable.Lord);
                case GenericClass.Ninja: return Usable.HasFlag(WizUsable.Ninja);
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

        public static WizItem CreateRandom(List<WizItem> list, ItemType type, BaseCharacter charUsable)
        {
            IntDeck deck = new IntDeck(1, list.Count);
            deck.Shuffle();
            foreach(int i in deck.Cards)
            {
                if (type != ItemType.None && type != list[i].ItemBaseType)
                    continue;

                if (charUsable == null)
                    return list[i].Clone() as WizItem;

                if (list[i].IsUsableByAny(charUsable))
                    return list[i].Clone() as WizItem;
            }
            return list[0].Clone() as WizItem;
        }

        public override CompareResult CompareTo(Item item)
        {
            if (!(item is WizItem))
                return CompareResult.Uncomparable;
            if (item == this)
                return CompareResult.Identical;
            if (Type != item.Type)
                return CompareResult.Uncomparable;

            WizItem wiz1Item = item as WizItem;

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
        public List<WizItem> Items;
        public bool Valid = false;
        public bool Internal = true;

        public virtual WizItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return null; }
        public virtual byte[] GetInternalBytes() { return null; }
        public virtual byte[] GetExternalBytes(MemoryHacker hacker) { return null; }
        public virtual bool Pad1024 { get { return true; } }
        public virtual int ItemLength { get { return 46; } }

        public virtual List<WizItem> SetFromBytes(byte[] bytes)
        {
            List<WizItem> items = new List<WizItem>(bytes.Length / ItemLength);

            try
            {
                // The wizardry 1-4 items are stored in 22-item sections, padded out to 1024 bytes each
                int iPos = 0;
                int iItemCount = 0;
                while (iPos <= bytes.Length - ItemLength)
                {
                    items.Add(CreateItem(iItemCount, bytes, iPos));
                    iPos += ItemLength;
                    iItemCount++;
                    if (Pad1024 && (iItemCount % 22 == 0))
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

        public virtual WizItem GetItem(int index, int memory = -1)
        {
            WizItem item = null;
            if (index < 0 || index >= Items.Count)
                index = 0;

            item = Items[index].Clone() as WizItem;
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
            if (Items == null || Items[0].Item.UsableString == "")
            {
                Items = new List<ShopItem>(25);
                foreach (Wiz1ItemIndex index in Boltac)
                    Items.Add(new ShopItem(Wiz1.Items[(int)index], -1, -1));
            }
        }
    }
}
