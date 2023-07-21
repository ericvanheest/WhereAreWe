using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Timers;
using System.Windows.Forms;

namespace WhereAreWe
{
    public abstract class Item
    {
        public enum ItemNounType
        {
            None,
            Axe,
            Bardiche,
            Blade,
            Bow,
            Bracers,
            ChainMail,
            Club,
            Crossbow,
            Cudgel,
            Cutlass,
            Dagger,
            Flail,
            Flamberge,
            Glaive,
            GreatAxe,
            Halberd,
            Hammer,
            Helm,
            Katana,
            Knife,
            LeatherArmor,
            LeatherSuit,
            Mace,
            Maul,
            Naginata,
            Nunchakas,
            PaddedArmor,
            Pike,
            Pipe,
            PlateArmor,
            PlateMail,
            RingMail,
            Sabre,
            ScaleArmor,
            ScaleMail,
            Scimitar,
            Scythe,
            Shield,
            Sickle,
            Sling,
            Spear,
            SplintMail,
            Staff,
            Sword,
            Trident,
            Wakizashi,
            Whip,
            Armor,
            Gloves,
            Helmet,
            Weapon,
            Figurine,
            Instrument,
            Ring,
            Wand,
            Accessory,
            Container,
            Item,
            Quiver,
            Misc
        }

        public enum CompareResult
        {
            Uncomparable,
            Worse,
            Identical,
            Better
        }

        protected string m_strName;
        private bool m_bCursed = false;
        public int MemoryIndex;
        public string DisplayIndex;

        public abstract int Index { get; set; }

        public abstract GameNames Game { get; }
        public virtual string Name { get { return m_strName; } set { m_strName = value; } }
        public bool Cursed { get { return m_bCursed; } set { m_bCursed = value; } }
        public EquipLocation WhereEquipped;
        public virtual GenericAlignmentValue Alignment { get { return GenericAlignmentValue.None; } }
        public virtual string DebugString { get { return String.Format("{0} ({1})", Index, Name); } }

        public virtual string GetLongDescription() { return GetLongDescription(GenericAlignmentValue.None, GenericClass.None, Name); }

        public virtual string GetLongDescription(GenericAlignmentValue align, GenericClass mmClass, string strOverrideName)
        {
            return DescriptionString;
        }

        public virtual bool CanEquip { get { return IsWeapon || IsArmor || IsAccessory; } }
        public virtual long Value { get { return 0; } set { } }
        public virtual int Range { get { return 0; } set { } }
        public virtual string DescriptionString { get { return "(no description)"; } }
        public virtual string FullDescriptionString { get { return GetLongDescription(GenericAlignmentValue.None, GenericClass.None, DescriptionString); } }
        public virtual string MultiLineDescription { get { return DescriptionString; } }
        public virtual byte[] Serialize() { return new byte[] { 0 }; }
        public virtual string UsableString { get { return "<all>"; } }
        public virtual string TypeString { get { return "item"; } }
        public virtual string BaseTypeString { get { return TypeString; } }
        public virtual string UsableByAlignment { get { return "Any"; } }
        public virtual string LargestBonusEffect { get { return String.Empty; } }
        public virtual int LargestBonus { get { return 0; } }
        public virtual bool IsUsableByAny(object filter) { return false; }
        public virtual bool NotUsable { get { return false; } }
        public virtual Item Clone() { return null; }
        public virtual ItemType Type { get { return ItemType.None; } set { } }
        public virtual string DamageStringFull { get { return BaseDamage.ToString(); } }
        public virtual int ArmorClassFull { get { return ArmorClass; } }
        public virtual EquipLocation CanEquipLocation { get { return EquipLocation.None; } }
        public virtual CompareResult CompareTo(Item item) { return CompareResult.Uncomparable; }
        public virtual bool IsBetterThan(Item item) { return CompareTo(item) == CompareResult.Better; }
        public virtual bool IsWorseThan(Item item) { return CompareTo(item) == CompareResult.Worse; }
        public virtual string AttributeString { get { return String.Empty; } }
        public virtual string MaterialString { get { return String.Empty; } }
        public virtual string ElementString { get { return String.Empty; } }
        public virtual string PropertyString { get { return String.Empty; } }
        public virtual string ResistString { get { return String.Empty; } }
        public virtual bool Broken { get { return false; } set { } }
        public virtual bool Duplicatable { get { return false; } }
        public virtual int ChargesCurrent { get { return 0; } set { } }
        public virtual bool ChargeBased { get { return ChargesCurrent > 0; } }
        public virtual int MaxCharges { get { return 255; } }
        public virtual string TrashIndex { get { return String.Format("{0:X2}", Index); } }
        public override int GetHashCode()
        {
            byte[] bytes = Serialize();
            return (int) Global.ConvertBytesToLong(bytes);
        }

        public static EquipLocation GetEquipLocation(ItemNounType type)
        {
            switch (type)
            {
                case ItemNounType.Weapon:
                case ItemNounType.Club:
                case ItemNounType.Dagger:
                case ItemNounType.Axe:
                case ItemNounType.Spear:
                case ItemNounType.Sword:
                case ItemNounType.Mace:
                case ItemNounType.Flail:
                case ItemNounType.Blade:
                case ItemNounType.Cudgel:
                case ItemNounType.Cutlass:
                case ItemNounType.Katana:
                case ItemNounType.Nunchakas:
                case ItemNounType.Knife:
                case ItemNounType.Maul:
                case ItemNounType.Sabre:
                case ItemNounType.Wakizashi:
                case ItemNounType.Whip:
                case ItemNounType.Scimitar: return EquipLocation.RightHand;
                case ItemNounType.Sling:
                case ItemNounType.Crossbow:
                case ItemNounType.Pipe:
                case ItemNounType.Bow: return EquipLocation.Ranged;
                case ItemNounType.Staff:
                case ItemNounType.Glaive:
                case ItemNounType.Bardiche:
                case ItemNounType.Halberd:
                case ItemNounType.Hammer:
                case ItemNounType.GreatAxe:
                case ItemNounType.Naginata:
                case ItemNounType.Pike:
                case ItemNounType.Scythe:
                case ItemNounType.Sickle:
                case ItemNounType.Trident:
                case ItemNounType.Flamberge: return EquipLocation.BothHands;
                case ItemNounType.PaddedArmor:
                case ItemNounType.LeatherArmor:
                case ItemNounType.ScaleArmor:
                case ItemNounType.RingMail:
                case ItemNounType.ChainMail:
                case ItemNounType.SplintMail:
                case ItemNounType.PlateMail:
                case ItemNounType.LeatherSuit:
                case ItemNounType.PlateArmor:
                case ItemNounType.ScaleMail:
                case ItemNounType.Armor:
                case ItemNounType.Bracers: return EquipLocation.Torso;
                case ItemNounType.Shield: return EquipLocation.LeftHand;
                case ItemNounType.Helmet:
                case ItemNounType.Helm: return EquipLocation.Head;
                case ItemNounType.Ring: return EquipLocation.Finger;
                case ItemNounType.Gloves: return EquipLocation.Gauntlet;
                case ItemNounType.Accessory: return EquipLocation.Accessory;
                case ItemNounType.Instrument: return EquipLocation.Accessory;

                default: return EquipLocation.None;
            }
        }

        protected static char[] EndChars = new char[] { ',', ' ' };

        protected void ReplaceOrRemove(StringBuilder sb, string strSearch, string strReplace)
        {
            ReplaceOrRemove(sb, strSearch, !String.IsNullOrWhiteSpace(strReplace), strReplace);
        }

        public bool MatchTypeAndChar(ItemType type, BaseCharacter baseChar)
        {
            return (type == ItemType.None || ItemBaseType == type) &&
                   (baseChar == null || (IsUsableByAny(baseChar.BasicClass) && IsUsableByAny(baseChar.BasicAlignment.Temporary) && CanEquip));
        }

        protected void ReplaceOrRemove(StringBuilder sb, string strSearch, bool bReplace, string strReplace)
        {
            // Replaces strSearch with strReplace if bReplace is set, otherwise remove braces or brackets
            // surrounding strSearch as well as the string itself.

            if (bReplace)
            {
                sb.Replace(strSearch, strReplace);
                return;
            }

            string str = sb.ToString();
            int iReplaceLen = strSearch.Length;
            int iFind = str.IndexOf(strSearch);
            if (iFind > 0)
            {
                switch (str[iFind - 1])
                {
                    case '<':
                    case '(':
                    case '[':
                    case '{':
                        if (str[iFind + iReplaceLen] == Global.MatchingBrace(str[iFind - 1]))
                        {
                            iFind--;
                            iReplaceLen += 2;
                        }
                        break;
                }
                sb.Remove(iFind, iReplaceLen);
            }
            else if (iFind == 0)
                sb.Remove(0, iReplaceLen);
        }

        public virtual string ItemTypeAbbr
        {
            get
            {
                switch (Type)
                {
                    case ItemType.OneHandMelee: return "1H";
                    case ItemType.TwoHandMelee: return "2H";
                    default: return "";
                }
            }
        }

        public virtual string ItemNoun { get { return Name; } }

        public static string GetItemNoun(ItemNounType type, string strDefault)
        {
            switch (type)
            {
                case ItemNounType.Axe: return "Axe";
                case ItemNounType.Bardiche: return "Bardiche";
                case ItemNounType.Blade: return "Blade";
                case ItemNounType.Bow: return "Bow";
                case ItemNounType.Bracers: return "Bracers";
                case ItemNounType.ChainMail: return "ChainMail";
                case ItemNounType.Club: return "Club";
                case ItemNounType.Crossbow: return "Crossbow";
                case ItemNounType.Cudgel: return "Cudgel";
                case ItemNounType.Cutlass: return "Cutlass";
                case ItemNounType.Dagger: return "Dagger";
                case ItemNounType.Flail: return "Flail";
                case ItemNounType.Flamberge: return "Flamberge";
                case ItemNounType.Glaive: return "Glaive";
                case ItemNounType.Halberd: return "Halberd";
                case ItemNounType.Hammer: return "Hammer";
                case ItemNounType.Helm: return "Helm";
                case ItemNounType.Katana: return "Katana";
                case ItemNounType.Knife: return "Knife";
                case ItemNounType.LeatherArmor: return "Leather Armor";
                case ItemNounType.LeatherSuit: return "Leather Suit";
                case ItemNounType.Mace: return "Mace";
                case ItemNounType.Maul: return "Maul";
                case ItemNounType.Naginata: return "Naginata";
                case ItemNounType.Nunchakas: return "Nunchakas";
                case ItemNounType.PaddedArmor: return "Padded Armor";
                case ItemNounType.Pike: return "Pike";
                case ItemNounType.Pipe: return "Pipe";
                case ItemNounType.PlateArmor: return "Plate Armor";
                case ItemNounType.PlateMail: return "Plate Mail";
                case ItemNounType.RingMail: return "Ring Mail";
                case ItemNounType.Sabre: return "Sabre";
                case ItemNounType.ScaleArmor: return "Scale Armor";
                case ItemNounType.ScaleMail: return "Scale Mail";
                case ItemNounType.Scimitar: return "Scimitar";
                case ItemNounType.Scythe: return "Scythe";
                case ItemNounType.Shield: return "Shield";
                case ItemNounType.Sickle: return "Sickle";
                case ItemNounType.Sling: return "Sling";
                case ItemNounType.Spear: return "Spear";
                case ItemNounType.SplintMail: return "Splint Mail";
                case ItemNounType.Staff: return "Staff";
                case ItemNounType.Sword: return "Sword";
                case ItemNounType.Trident: return "Trident";
                case ItemNounType.Wakizashi: return "Wakizashi";
                case ItemNounType.Whip: return "Whip";
                case ItemNounType.Armor: return "Armor";
                case ItemNounType.Gloves: return "Gloves";
                case ItemNounType.Helmet: return "Helmet";
                case ItemNounType.Weapon: return "Weapon";
                case ItemNounType.Figurine: return "Figurine";
                case ItemNounType.Instrument: return "Instrument";
                case ItemNounType.Ring: return "Ring";
                case ItemNounType.Wand: return "Wand";
                case ItemNounType.Misc: return "Misc";
                case ItemNounType.Container: return "Container";
                case ItemNounType.Quiver: return "Quiver";
                default: return strDefault;
            }
        }

        public virtual string ItemTypeFull
        {
            get
            {
                switch (Type)
                {
                    case ItemType.Armor: return "Armor";
                    case ItemType.OneHandMelee: return "One-Handed Weapon";
                    case ItemType.TwoHandMelee: return "Two-Handed Weapon";
                    case ItemType.Missile: return "Missile Weapon";
                    case ItemType.Accessory: return "Accessory";
                    case ItemType.Miscellaneous: return "Miscellaneous";
                    case ItemType.Quest: return "Quest";
                    default: return "";
                }
            }
        }

        public virtual bool IsIdentified { get { return true; } }
        public virtual string EquipEffects { get { return String.Empty; } }
        public virtual bool RevealUnidentified { get { return false; } }

        protected static Regex m_reRedundantCommas = new Regex(" *,[ ,]*");
        protected static Regex m_reRedundantSpaces = new Regex("  +");

        public virtual string GetDamAC()
        {
            if (BaseDamage.Quantity > 0 && ArmorClassFull != 0)
                return String.Format("{0}, AC {1}", DamageStringFull, ArmorClassFull);
            return BaseDamage.Quantity > 0 ? DamageStringFull : (ArmorClassFull != 0 ? String.Format("AC {0}", ArmorClassFull) : "");
        }

        public virtual string FormatDescription(string strFormat, GenericAlignmentValue currentAlign = GenericAlignmentValue.None, GenericClass currentClass = GenericClass.None)
        {
            if (!IsIdentified && Properties.Settings.Default.HideUnidentifiedItems && !RevealUnidentified)
                return String.Format("(unidentified {0})", ItemNoun);

            if (String.IsNullOrWhiteSpace(strFormat))
                return DescriptionString;

            if (!strFormat.Contains('$'))
                return strFormat;

            StringBuilder sb = new StringBuilder(strFormat);
            ReplaceOrRemove(sb, "$[LongDescription]", GetLongDescription(currentAlign, currentClass, DescriptionString));

            ReplaceOrRemove(sb, "$[Cursed]", Cursed, "CURSED");
            ReplaceOrRemove(sb, "$[Broken]", Broken, "BROKEN");
            sb.Replace("$[Name]", DescriptionString);

            string strUsable = String.Empty;
            if (!IsUsableByAny(currentClass) && currentClass != GenericClass.None)
                strUsable = String.Format("!{0}", BaseCharacter.ClassString(currentClass));
            else if (!IsUsableByAny(currentAlign) && currentAlign != GenericAlignmentValue.None)
                strUsable = String.Format("!{0}", BaseCharacter.AlignmentString(currentAlign));

            string strUse = UseEffectString;
            bool bPassive = PassiveEffect;
            string strToHit = ToHitBonus == 0 ? String.Empty : Global.AddPlus(ToHitBonus);

            ReplaceOrRemove(sb, "$[Why]", CanEquip && !String.IsNullOrWhiteSpace(strUsable), strUsable);
            ReplaceOrRemove(sb, "$[Type]", ItemTypeAbbr);
            ReplaceOrRemove(sb, "$[TypeLong]", ItemTypeFull);
            ReplaceOrRemove(sb, "$[DamAC]", GetDamAC());
            ReplaceOrRemove(sb, "$[Equip]", EquipEffects);
            ReplaceOrRemove(sb, "$[Use]", !String.IsNullOrWhiteSpace(strUse), String.Format("{0}{1}", bPassive ? "" : "Use: ", strUse));
            ReplaceOrRemove(sb, "$[Charges]", ChargesCurrent != -1 && ShowCharges, ChargesCurrent.ToString());
            ReplaceOrRemove(sb, "$[Value]", Value > 0, String.Format("{0} Gold", ValueString));
            ReplaceOrRemove(sb, "$[Align]", BaseCharacter.AlignmentString(Alignment));
            ReplaceOrRemove(sb, "$[AlignShort]", BaseCharacter.AlignmentString(Alignment).Substring(0, 1));
            ReplaceOrRemove(sb, "$[UsableBy]", UsableString);
            ReplaceOrRemove(sb, "$[Usable]", IsUsableByAny(currentClass) && IsUsableByAny(currentAlign) ? "Yes" : "No");
            ReplaceOrRemove(sb, "$[BasicName]", Name);
            ReplaceOrRemove(sb, "$[Element]", ElementString);
            ReplaceOrRemove(sb, "$[Material]", MaterialString);
            ReplaceOrRemove(sb, "$[Attribute]", AttributeString);
            ReplaceOrRemove(sb, "$[Property]", PropertyString);
            ReplaceOrRemove(sb, "$[Duplicatable]", Duplicatable ? "Yes" : "No");
            ReplaceOrRemove(sb, "$[Damage]", BaseDamage.Quantity > 0, DamageStringFull);
            ReplaceOrRemove(sb, "$[AvgDamage]", BaseDamage.Quantity > 0, String.Format("{0:F1}", BaseDamage.Average));
            ReplaceOrRemove(sb, "$[AC]", ArmorClassFull != 0, ArmorClassFull.ToString());
            ReplaceOrRemove(sb, "$[Index]", Index.ToString());
            ReplaceOrRemove(sb, "$[ToHit]", !String.IsNullOrWhiteSpace(strToHit), String.Format("ToHit: {0}", strToHit));

            while (sb.Length > 0 && (sb[sb.Length - 1] == ' ' || sb[sb.Length - 1] == ','))
                sb.Remove(sb.Length - 1, 1);

            return m_reRedundantCommas.Replace(m_reRedundantSpaces.Replace(sb.ToString(), " "), ", ").Trim();
        }

        public bool ShowCharges
        {
            get
            {
                if ((Game == GameNames.BardsTale3 || Game == GameNames.BardsTale2) && ChargesCurrent == 255)
                    return false;

                return ChargeBased;
            }
        }

        public virtual ItemType ItemBaseType
        {
            get
            {
                switch (Type)
                {
                    case ItemType.Weapon:
                    case ItemType.OneHandMelee:
                    case ItemType.TwoHandMelee:
                    case ItemType.Missile:
                        return ItemType.Weapon;
                    case ItemType.Armor:
                        return ItemType.Armor;
                    case ItemType.Accessory:
                        return ItemType.Accessory;
                    case ItemType.Miscellaneous:
                        return ItemType.Miscellaneous;
                    default: return ItemType.Miscellaneous;
                }
            }

            set { }
        }

        public virtual bool IsEquipped { get { return WhereEquipped != EquipLocation.None; } }
        public virtual bool IsWeapon { get { return ItemBaseType == ItemType.Weapon; } }
        public virtual bool IsArmor { get { return ItemBaseType == ItemType.Armor; } }
        public virtual bool IsAccessory { get { return ItemBaseType == ItemType.Accessory; } }
        public virtual bool IsMiscellaneous { get { return ItemBaseType == ItemType.Miscellaneous; } }
        public virtual int Bonus { get { return 0; } }
        public virtual bool Trashable { get { return true; } }
        public virtual int ArmorClass { get { return 0; } }
        public virtual BasicDamage BaseDamage { get { return BasicDamage.Zero; } }
        public virtual int EquipBonusValue { get { return 0; } }
        public virtual int ToHitBonus { get { return 0; } }
        public virtual string UseEffectString { get { return String.Empty; } }
        public virtual bool PassiveEffect { get { return false; } }
        public virtual string ValueString { get { return Value == 0 ? String.Empty : Value.ToString(); } }
        public virtual string RangeString { get { return String.Empty; } }

        public virtual bool Matches(ItemType type) { return ItemBaseType == type || type == ItemType.Any; }

        public static CompareResult CompareValues(params int[] values)
        {
            if ((values.Length & 1) == 1)
                return CompareResult.Uncomparable;  // number of parameters must be even

            bool bAll = true;
            for (int i = 0; i < values.Length; i += 2)
            {
                if (values[i] != values[i + 1])
                {
                    bAll = false;
                    break;
                }
            }
            if (bAll)
                return CompareResult.Identical;

            bAll = true;
            for (int i = 0; i < values.Length; i += 2)
            {
                if (values[i] < values[i + 1])
                {
                    bAll = false;
                    break;
                }
            }
            if (bAll)
                return CompareResult.Better;

            bAll = true;
            for (int i = 0; i < values.Length; i += 2)
            {
                if (values[i] > values[i + 1])
                {
                    bAll = false;
                    break;
                }
            }
            if (bAll)
                return CompareResult.Worse;

            return CompareResult.Uncomparable;
        }

        public static CompareResult Compare(params CompareResult[] results)
        {
            CompareResult resFirstDefinite = CompareResult.Identical;
            foreach (CompareResult res in results)
            {
                if (res == CompareResult.Uncomparable)
                    return CompareResult.Uncomparable;
                if (res == CompareResult.Identical)
                    continue;
                if (resFirstDefinite == CompareResult.Identical)
                {
                    resFirstDefinite = res;
                    continue;
                }
                if (resFirstDefinite != res)
                    return CompareResult.Uncomparable;
            }
            return resFirstDefinite;
        }

        public static CompareResult CompareValues(double d1, double d2, params int[] values)
        {
            CompareResult compare = CompareValues(values);
            if (d1 == d2 && compare == CompareResult.Identical)
                return CompareResult.Identical;
            if (d1 >= d2 && (compare == CompareResult.Better || compare == CompareResult.Identical))
                return CompareResult.Better;
            if (d1 <= d2 && (compare == CompareResult.Worse || compare == CompareResult.Identical))
                return CompareResult.Worse;
            return CompareResult.Uncomparable;
        }

        public static string GetEquipLocationString(EquipLocation equip)
        {
            switch (equip)
            {
                case EquipLocation.LeftHand: return "left hand";
                case EquipLocation.RightHand: return "right hand";
                case EquipLocation.Torso: return "torso";
                case EquipLocation.Ranged: return "ranged";
                case EquipLocation.Head: return "head";
                case EquipLocation.Gauntlet: return "gauntlet";
                case EquipLocation.Medallion: return "medallion";
                case EquipLocation.Finger: return "finger";
                case EquipLocation.Feet: return "feet";
                case EquipLocation.Cloak: return "cloak";
                case EquipLocation.Neck: return "neck";
                case EquipLocation.Belt: return "belt";
                case EquipLocation.BothHands: return "both hands";
                case EquipLocation.Accessory: return "accessory";
                default: return "";
            }
        }

        public override string ToString()
        {
            string strDesc = DescriptionString;
            if (String.IsNullOrWhiteSpace(strDesc))
                return base.ToString();
            return strDesc;
        }

        public virtual ItemNounType ItemBasicType { get { return ItemNounType.None; } }
    }

    public enum EquipLocation
    {
        None = 0,
        LeftHand = 1,
        RightHand = 2,
        Torso = 3,
        Ranged = 4,
        Head = 5,
        Gauntlet = 6,
        Medallion = 7,
        Finger = 8,
        Feet = 9,
        Cloak = 10,
        Neck = 11,
        Belt = 12,
        BothHands = 13,
        Invalid = 14,
        Slot1 = 81,
        Slot2 = Slot1 + 1,
        Slot3 = Slot1 + 2,
        Slot4 = Slot1 + 3,
        Slot5 = Slot1 + 4,
        Slot6 = Slot1 + 5,
        Accessory = 128
    }

    public enum ConditionIndex
    {
        Cursed,
        HeartBroken,
        Weak,
        Poisoned,
        Diseased,
        Insane,
        InLove,
        Drunk,
        Asleep,
        Depressed,
        Confused,
        Paralyzed,
        Unconscious,
        Dead,
        Stone,
        Eradicated,
        Mobile   // This is a meta-condition for scripts that seems to mean "not asleep/paralyzed/unconscious/dead/stone/eradicated"
    }

    public abstract class CharacterOffsets
    {
        public virtual int Name { get { return -1; } }
        public virtual int NameTerminator { get { return -1; } }
        public virtual int Sex { get { return -1; } }
        public virtual int Race { get { return -1; } }
        public virtual int Alignment { get { return -1; } }
        public virtual int AlignmentMod { get { return Alignment < 0 ? -1 : Alignment + 1; } }
        public virtual int Class { get { return -1; } }
        public virtual int Might { get { return -1; } }
        public virtual int Intellect { get { return -1; } }
        public virtual int Personality { get { return -1; } }
        public virtual int Endurance { get { return -1; } }
        public virtual int Speed { get { return -1; } }
        public virtual int Accuracy { get { return -1; } }
        public virtual int Luck { get { return -1; } }
        public virtual int MightMod { get { return Might < 0 ? -1 : Might + 1; } }
        public virtual int IntellectMod { get { return Intellect < 0 ? -1 : Intellect + 1; } }
        public virtual int PersonalityMod { get { return Personality < 0 ? -1 : Personality + 1; } }
        public virtual int EnduranceMod { get { return Endurance < 0 ? -1 : Endurance + 1; } }
        public virtual int SpeedMod { get { return Speed < 0 ? -1 : Speed + 1; } }
        public virtual int AccuracyMod { get { return Accuracy < 0 ? -1 : Accuracy + 1; } }
        public virtual int LuckMod { get { return Luck < 0 ? -1 : Luck + 1; } }
        public virtual int LastArmorClass { get { return -1; } }
        public virtual int ArmorClass { get { return -1; } }
        public virtual int ArmorClassMod { get { return -1; } }
        public virtual int Level { get { return -1; } }
        public virtual int LevelMod { get { return Level < 0 ? -1 : Level + 1; } }
        public virtual int LevelLength { get { return 2; } }
        public virtual int BirthDay { get { return -1; } }
        public virtual int Age { get { return -1; } }
        public virtual int AgeDays { get { return -1; } }
        public virtual int AgeModifier { get { return -1; } }
        public virtual int Skills { get { return -1; } }
        public virtual int SkillsLength { get { return -1; } }
        public virtual int Awards { get { return -1; } }
        public virtual int AwardsLength { get { return -1; } }
        public virtual int SpellLevel { get { return -1; } }
        public virtual int SpellLevelLength { get { return -1; } }
        public virtual int SpellLevelMod { get { return SpellLevel < 0 ? -1 : SpellLevel+1; } }
        public virtual int Spells { get { return -1; } }
        public virtual int SpellsLength { get { return -1; } }
        public virtual int Beacon { get { return -1; } }
        public virtual int SpellCaster { get { return -1; } }
        public virtual int ReadySpell { get { return -1; } }
        public virtual int Inventory { get { return -1; } }
        public virtual int InventoryLength { get { return -1; } }
        public virtual int FireResist { get { return -1; } }
        public virtual int ColdResist { get { return -1; } }
        public virtual int EnergyResist { get { return -1; } }
        public virtual int PoisonResist { get { return -1; } }
        public virtual int AcidResist { get { return -1; } }
        public virtual int SleepResist { get { return -1; } }
        public virtual int FearResist { get { return -1; } }
        public virtual int ElecResist { get { return -1; } }
        public virtual int MagicResist { get { return -1; } }
        public virtual int FireResistMod { get { return FireResist < 0 ? -1 : FireResist + 1; } }
        public virtual int ColdResistMod { get { return ColdResist < 0 ? -1 : ColdResist + 1; } }
        public virtual int ElecResistMod { get { return ElecResist < 0 ? -1 : ElecResist + 1; } }
        public virtual int PoisonResistMod { get { return PoisonResist < 0 ? -1 : PoisonResist + 1; } }
        public virtual int EnergyResistMod { get { return EnergyResist < 0 ? -1 : EnergyResist + 1; } }
        public virtual int MagicResistMod { get { return MagicResist < 0 ? -1 : MagicResist + 1; } }
        public virtual int AcidResistMod { get { return AcidResist < 0 ? -1 : AcidResist + 1; } }
        public virtual int SleepResistMod { get { return SleepResist < 0 ? -1 : SleepResist + 1; } }
        public virtual int FearResistMod { get { return FearResist < 0 ? -1 : FearResist + 1; } }
        public virtual int Condition { get { return -1; } }
        public virtual int ConditionLength { get { return -1; } }
        public virtual int Town { get { return -1; } }
        public virtual int HPSPLength { get { return 2; } }
        public virtual int CurrentHP { get { return -1; } }
        public virtual int MaxSP { get { return -1; } }
        public virtual int CurrentSP { get { return -1; } }
        public virtual int MaxHP { get { return -1; } }
        public virtual int MaxHPMod { get { return -1; } }
        public virtual int BirthYear { get { return -1; } }
        public virtual int Experience { get { return -1; } }
        public virtual int ExperienceLength { get { return 4; } }
        public virtual int Gems { get { return -1; } }
        public virtual int Food { get { return -1; } }
        public virtual int Gold { get { return -1; } }
        public virtual int GoldLength { get { return 4; } }
        public virtual int Blessed { get { return -1; } }
        public virtual int HolyBonus { get { return -1; } }
        public virtual int PowerShield { get { return -1; } }
        public virtual int Heroism { get { return -1; } }
        public virtual int RosterPos { get { return -1; } }
        public virtual int Donations { get { return -1; } }
        public virtual int InventoryBases { get { return -1; } }
        public virtual int Protection { get { return -1; } }
        public virtual int BackpackBases { get { return -1; } }
        public virtual int BackpackCharges { get { return -1; } }
        public virtual int BackpackBonus { get { return -1; } }
        public virtual int EquippedBases { get { return -1; } }
        public virtual int EquippedCharges { get { return -1; } }
        public virtual int EquippedBonus { get { return -1; } }
        public virtual int InvEquipLoc { get { return -1; } }
        public virtual int InvCharges { get { return -1; } }
        public virtual int InvElements { get { return -1; } }
        public virtual int InvMaterials { get { return -1; } }
        public virtual int InvAttributes { get { return -1; } }
        public virtual int InvBases { get { return -1; } }
        public virtual int InvProperties { get { return -1; } }
        public virtual int MeleeDamage { get { return -1; } }
        public virtual int RangedDamage { get { return -1; } }
        public virtual int Thievery { get { return Skills; } }
        public virtual int InvWeapons { get { return -1; } }
        public virtual int InvArmor { get { return -1; } }
        public virtual int InvAccessories { get { return -1; } }
        public virtual int InvMisc { get { return -1; } }
        public virtual int BeaconSide { get { return -1; } }
        public virtual int NameLength { get { return -1; } }
        public virtual int PasswordLength { get { return -1; } }
        public virtual int Password { get { return -1; } }
        public virtual int Stats { get { return -1; } }
        public virtual int Out { get { return -1; } }
        public virtual int SavingThrows { get { return -1; } }
        public virtual int LocationX { get { return -1; } }
        public virtual int LocationY { get { return -1; } }
        public virtual int LocationZ { get { return -1; } }
        public virtual int Regeneration { get { return -1; } }
        public virtual int Critical { get { return -1; } }
        public virtual int Swings { get { return -1; } }
        public virtual int WeaponEffects { get { return -1; } }
        public virtual int Poison { get { return -1; } }
        public virtual int Marks { get { return -1; } }
        public virtual int RIP { get { return -1; } }
        public virtual int Swim { get { return -1; } }
        public virtual int BardSongs { get { return -1; } }
        public virtual int BattlesWon { get { return -1; } }
        public virtual int HideChance { get { return -1; } }
        public virtual int Identify { get { return -1; } }
    }

    public abstract class Inventory
    {
        public virtual List<Item> Items { get; set; }
        public virtual bool BrokenEquipped { get { return false; } }
        public virtual bool CursedEquipped { get { return false; } }
        public virtual Modifiers GetModifiers() { return new Modifiers(); }
        public virtual BasicDamage MeleeWeaponDamage { get { return BasicDamage.Zero; } }
        public virtual string MeleeWeaponName { get { return String.Empty; } }
        public virtual BasicDamage RangedWeaponDamage { get { return BasicDamage.Zero; } }
        public virtual string RangedWeaponName { get { return String.Empty; } }
        public virtual int NumBackpackItems { get { return Items.Count(n => !n.IsEquipped); } }

        public virtual List<Item> SelectUnequippedItems
        {
            get
            {
                List<Item> list = new List<Item>();
                foreach (Item item in Items.Where(i => !i.IsEquipped))
                    list.Add(item);
                return list;
            }
        }

        public virtual Item GetItem(int index)
        {
            foreach (Item item in Items)
                if (item.Index == index)
                    return item;
            return null;
        }

        public virtual List<Item> SelectEquippedItems
        {
            get
            {
                List<Item> list = new List<Item>();
                foreach (Item item in Items.Where(i => i.IsEquipped))
                    list.Add(item);
                return list;
            }
        }
    }

    public class Resistances
    {
        public int Physical;
        public int Fire;
        public int Cold;
        public int Electricity;
        public int Poison;
        public int Energy;
        public int Magic;

        public Resistances(int physical, int fire, int cold, int electricity, int poison, int energy, int magic)
        {
            Physical = physical;
            Fire = fire;
            Cold = cold;
            Electricity = electricity;
            Poison = poison;
            Energy = energy;
            Magic = magic;
        }
    }

    public enum DamageType
    {
        First = 0,
        Physical = 0,
        Magic = 1,
        Fire = 2,
        Electricity = 3,
        Cold = 4,
        Poison = 5,
        Energy = 6,
        Last,
    }

    public abstract class Monster
    {
        public byte[] RawBytes;
        public int Index;
        public int AC;
        public long Experience;
        public string Name;
        public MonsterName FullName;
        public int EncounterIndex;
        public bool Melee;
        public Point Position;
        public bool Killed;
        public bool Active;
        public int GroupSize;
        public int MagicResist;
        public int HP;
        public int Damage;
        public int DamageMin;
        public int NumAttacks;
        public int Speed;
        public int ImageIndex;
        public int Bravery;
        public bool Undead;
        public bool Missile;
        public bool Advance;
        public bool Regenerate;
        public bool HasMoved;
        public int CurrentHP;
        public int Accuracy;
        public int Gold;
        public int Gems;
        public int Items;
        public bool Flying;
        public bool NPC = false;
        public int MonsterGroup;
        public int MonsterSubGroup;
        public int RewardModifier;
        public int Distance;
        public bool Summoned = false;

        public virtual string IndexString { get { return InternalIndex.ToString(); } }
        public virtual string ProperName { get { return Name; } }
        public virtual string DamageString { get { return "N/A"; } }
        public virtual string SpeedString { get { return Speed.ToString(); } }
        public virtual string ResistancesStringShort { get { return "N/A"; } }
        public virtual string AllPowersString { get { return "N/A"; } }
        public virtual string TreasureStringShort { get { return "N/A"; } }
        public virtual string TargetString { get { return "N/A"; } }
        public virtual string OneLineDescription { get { return String.Empty; } }
        public virtual string MultiLineDescription { get { return GetMultiLineDescription(true); } }
        public virtual string GetMultiLineDescription(bool bActive) { return "N/A"; }
        public virtual int TreasureStrength { get { return 0; } }
        public virtual GenericResistanceFlags GenericResistances { get { return GenericResistanceFlags.None; } }
        public virtual BasicConditionFlags Condition { get { return BasicConditionFlags.Good; } }
        public virtual Monster Clone() { return null; }
        public virtual double AverageDamage { get { return 0; } }
        public virtual double AverageHP { get { return 0; } }
        public virtual int AverageResistance { get { return 0; } }
        public virtual bool IsAlive { get { return true; } }
        public virtual int InternalIndex { get { return Index; } }

        public virtual string StatusSuffix
        {
            get
            {
                // Show the "worst" condition
                // In MM2 the order shown on the screen is 
                // Encased, Mindless, Held, Asleep, Afraid, Weak, Silenced, Hurt
                if (Condition.HasFlag(BasicConditionFlags.Eradicated))
                    return "eradicated";
                if (Condition.HasFlag(BasicConditionFlags.Dead))
                    return "dead";
                if (Condition.HasFlag(BasicConditionFlags.Stone))
                    return "stone";
                if (Condition.HasFlag(BasicConditionFlags.EncasedAir) ||
                    Condition.HasFlag(BasicConditionFlags.EncasedFire) ||
                    Condition.HasFlag(BasicConditionFlags.EncasedWater) ||
                    Condition.HasFlag(BasicConditionFlags.EncasedEarth))
                    return "encased";
                if (Condition.HasFlag(BasicConditionFlags.Mindless))
                    return "mindless";
                if (Condition.HasFlag(BasicConditionFlags.Held))
                    return "held";
                if (Condition.HasFlag(BasicConditionFlags.Paralyzed))
                    return "paralyzed";
                if (Condition.HasFlag(BasicConditionFlags.Hypnotized))
                    return "hypnotized";
                if (Condition.HasFlag(BasicConditionFlags.Asleep))
                    return "asleep";
                if (Condition.HasFlag(BasicConditionFlags.Afraid))
                    return "afraid";
                if (Condition.HasFlag(BasicConditionFlags.Weak))
                    return "weak";
                if (Condition.HasFlag(BasicConditionFlags.Silenced))
                    return "silenced";
                if (Condition.HasFlag(BasicConditionFlags.Hurt))
                    return "hurt";
                return "";
            }
        }

        public virtual string HPString(bool bPreEncounter) { return bPreEncounter ? HP.ToString() : CurrentHP.ToString(); }

        public static string GetDamageTypeString(DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Cold: return "Cold";
                case DamageType.Electricity: return "Electricity";
                case DamageType.Energy: return "Energy";
                case DamageType.Fire: return "Fire";
                case DamageType.Magic: return "Magic";
                case DamageType.Physical: return "Physical";
                case DamageType.Poison: return "Poison";
                default: return "None";
            }
        }

        public static string GetDamageTypeStringShort(DamageType damageType)
        {
            switch (damageType)
            {
                case DamageType.Cold: return "Co";
                case DamageType.Electricity: return "El";
                case DamageType.Energy: return "En";
                case DamageType.Fire: return "Fi";
                case DamageType.Magic: return "Ma";
                case DamageType.Physical: return "Ph";
                case DamageType.Poison: return "Po";
                default: return "";
            }
        }

        public static string GetResistancesStringShort(Resistances res)
        {
            if (res == null)
                return "Unknown";

            return String.Format("{0}/{1}/{2}/{3}/{4}/{5}/{6}",
                res.Physical,
                res.Fire,
                res.Cold,
                res.Electricity,
                res.Poison,
                res.Energy,
                res.Magic
                );
        }

        public static int GetAverageResistance(Resistances res)
        {
            return (res.Physical +
                    res.Fire +
                    res.Cold +
                    res.Electricity +
                    res.Poison +
                    res.Energy +
                    res.Magic) / 7;
        }
    }

    public abstract class GameMultiEnum
    {
        public GameNames Game;
        public int Index;

        public void SetIndex(object o, GameNames game)
        {
            Index = (int)o;
            Game = game;
        }
    }

    public class InventoryItemCounts
    {
        public ItemType ItemType;
        public int Weapons;
        public int Armor;
        public int Accessories;
        public int Miscellaneous;

        public InventoryItemCounts(int weapon, int armor, int accessory, int misc)
        {
            Weapons = weapon;
            Armor = armor;
            Accessories = accessory;
            Miscellaneous = misc;
            ItemType = ItemType.None;
        }

        public InventoryItemCounts() { SetDefaults(); }
        public void SetDefaults() { Clear(); }

        public void Clear()
        {
            Weapons = 0;
            Armor = 0;
            Accessories = 0;
            Miscellaneous = 0;
            ItemType = ItemType.None;
        }

        public InventoryItemCounts(ListView.ListViewItemCollection lvic)
        {
            foreach (ListViewItem lvi in lvic)
                Add(lvi.Tag as MMItem);
        }

        public InventoryItemCounts(List<Item> items)
        {
            Clear();

            if (items == null)
                return;

            foreach (Item item in items)
            {
                if (item.IsWeapon)
                    Weapons++;
                else if (item.IsArmor)
                    Armor++;
                else if (item.IsAccessory)
                    Accessories++;
                else if (item.IsMiscellaneous)
                    Miscellaneous++;
            }
        }

        public void Adjust(Item item, int count)
        {
            if (item == null)
                return;
            if (item.IsAccessory)
                Accessories+= count;
            else if (item.IsMiscellaneous)
                Miscellaneous+= count;
            else if (item.IsArmor)
                Armor+= count;
            else if (item.IsWeapon)
                Weapons+= count;
        }

        public int CountType(ItemType type)
        {
            switch (type)
            {
                case ItemType.Weapon: return Weapons;
                case ItemType.Armor: return Armor;
                case ItemType.Accessory: return Accessories;
                case ItemType.Miscellaneous: return Miscellaneous;
                default: return 0;
            }
        }

        public void Add(Item item) { Adjust(item, 1); }
        public void Remove(Item item) { Adjust(item, -1); }
        public int Total { get { return Weapons + Armor + Accessories + Miscellaneous; } }
        public bool AnyOver(int i) { return (Weapons > i || Armor > i || Accessories > i || Miscellaneous > i); }

        public string GetHeaderString(string strList, int iNumElsewhere = 0, string strElsewhere = "")
        {
            StringBuilder sb = new StringBuilder();
            if (iNumElsewhere > 0)
                sb.AppendFormat("{0} ({1} + {2} {3}): ", strList, Total - iNumElsewhere, iNumElsewhere, strElsewhere);
            else
                sb.AppendFormat("{0} ({1}): ", strList, Total);
            if (Weapons > 0)
                sb.AppendFormat("{0}, ", Global.Plural(Weapons, "weapon"));
            if (Armor > 0)
                sb.AppendFormat("{0} armor, ", Armor);
            if (Accessories > 0)
                sb.AppendFormat("{0}, ", Global.Plural(Accessories, "accessory", "accessories"));
            if (Miscellaneous > 0)
                sb.AppendFormat("{0} misc, ", Miscellaneous);

            return Global.Trim(sb).ToString();
        }

        public string GetShortHeaderString()
        {
            StringBuilder sb = new StringBuilder();
            if (Weapons > 0)
                sb.AppendFormat("{0} weap, ", Weapons);
            if (Armor > 0)
                sb.AppendFormat("{0} armor, ", Armor);
            if (Accessories > 0)
                sb.AppendFormat("{0} acc, ", Accessories);
            if (Miscellaneous > 0)
                sb.AppendFormat("{0} misc, ", Miscellaneous);

            return Global.Trim(sb).ToString();
        }
    }

    public enum SetBackpackResult
    {
        InvalidHacker,
        NotImplemented,
        InvalidPosition,
        InvalidFile,
        InsufficientSpace,
        LoadCharFailure,
        InvalidItems,
        Success
    }

    public enum Subscreen
    {
        Unknown,
        Character,
        Weapons,
        Armor,
        Accessories,
        Miscellaneous,
        QuestItems,
        Quests,
        AutoNotes,
        Inventory1,
        Inventory2,
        InventoryMain
    }

    public abstract class InternExternList
    {
        protected bool m_bValid = false;
        protected bool m_bInternal = true;

        public virtual bool Valid { get { return m_bValid; } }
        public virtual bool Internal { get { return m_bInternal; } }

        public virtual byte[] GetInternalBytes() { return null; }
        public virtual byte[] GetExternalBytes(MemoryHacker hacker) { return null; }

        public abstract bool InitExternalList(MemoryHacker hacker, bool bOverrideSanityCheck = false);
        public abstract bool InitInternalList();
    }

    public abstract class ScriptLine
    {
        public int Number;
        public Point Location;
        public DirectionFlags Facing;
        public MemoryBytes Bytes;
        public int Address;

        public virtual List<ScriptSummary> Summary(ScriptInfo info, bool bForNote) { return new List<ScriptSummary>(); }
        public virtual bool IsTeleportCommand { get { return false; } }
        public virtual bool IsDisplayCommand { get { return false; } }
        public virtual bool IsReturnCommand { get { return false; } }
        public virtual bool IsSubscriptReturnCommand { get { return IsReturnCommand; } }
        public virtual bool IsSubscriptCommand { get { return false; } }
        public virtual Point TeleportLocation { get { return Global.NullPoint; } }
        public virtual int TeleportMapIndex { get { return -1; } }
        public virtual string Description(ScriptInfo info, string strLinePrefix) { return "Not Implemented"; }
        public virtual int[] GotoLines { get { return new int[0]; } }
        public virtual Point InsertScriptLocation { get { return Global.NullPoint; } }
        public virtual int InsertScriptLine { get { return 0; } }
        public virtual string GetTabSeparatedString(ScriptInfo info, string strLinePrefix) { return "Not Implemented"; }
        public virtual MemoryBytes HeaderBytes { get { return null; } }
        public virtual MemoryBytes CommandBytes { get { return Bytes; } }
        public virtual bool IsEncounterCommand { get { return false; } }
    }

    public abstract class ScriptString
    {
        public byte[] RawBytes;
        public string Basic;
        public int AbbreviationLength = 20;

        public virtual bool Valid
        {
            get
            {
                return (RawBytes != null);
            }
        }

        public override string ToString()
        {
            return Basic;
        }

        public string Abbreviated
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Basic))
                    return String.Empty;

                if (Basic.Length < AbbreviationLength)
                    return Basic;

                int i = AbbreviationLength + 2;
                while (i > Basic.Length - 1 || !Char.IsWhiteSpace(Basic[i]))
                {
                    i--;
                    if (i < AbbreviationLength - 5) // last word too long
                        return Basic.Substring(0, AbbreviationLength - 2) + "...";
                }
                return Basic.Substring(0, i) + "...";
            }
        }

        public virtual void SetFromBytes(byte[] bytes, ref int iNext)
        {
            Init();
        }

        protected void Init()
        {
            RawBytes = null;
            Basic = String.Empty;
        }
    }

    public abstract class ScriptInfo
    {
        public GameScripts Scripts;
        public MapTitleInfo Map;
        public List<ScriptString> Strings;
    }

    public abstract class GameScript
    {
        public int Index;
        public Point Location;
        public DirectionFlags Facing;
        public MemoryBytes Bytes;
        public bool AutoExecute;

        public virtual NoteInfo Summary(ScriptInfo info, bool bSkipSubscripts, bool bForNote = false)
        {
            return new NoteInfo("Unknown", "?", Color.Black);
        }

        public virtual NoteInfo GetNoteText(ScriptInfo info, bool bSkipSubscripts)
        {
            return Summary(info, bSkipSubscripts, true);
        }

        public virtual MapNote GetNote(ScriptInfo info, bool bSkipSubscripts)
        {
            NoteInfo noteInfo = GetNoteText(info, bSkipSubscripts);
            return new MapNote(noteInfo.Text, noteInfo.Color, noteInfo.Symbol);
        }

        public virtual List<ScriptLine> Lines { get { return null; } }
        public virtual bool DoesNothing { get { return false; } }
        public virtual void Summary(int iDepth, List<ScriptSummary> listSummary, ScriptInfo info, int iStartLine, bool bSkipSubscripts, bool bForNote) { }
        public virtual bool HasHeaderBytes { get { return false; } }
        public virtual int NumBytes { get { return Bytes == null ? 0 : Bytes.Length; } }
    }

    public abstract class GameScripts
    {
        public Dictionary<Point, List<GameScript>> Scripts;

        public GameScripts()
        {
            Scripts = new Dictionary<Point, List<GameScript>>();
        }

        public virtual bool HasHeaderBytes { get { return false; } }
        public virtual bool IsMainList { get { return true; } }

        public virtual NoteInfo GetNoteInfo(ScriptInfo info, Point pt)
        {
            if (!Scripts.ContainsKey(pt))
                return NoteInfo.Empty;

            List<GameScript> scripts = Scripts[pt];
            if (scripts.Count > 0)
                return scripts[0].GetNoteText(info, false);

            return NoteInfo.Empty;
        }

        public virtual MapNote GetNote(ScriptInfo info, Point pt)
        {
            NoteInfo noteInfo = GetNoteInfo(info, pt);

            return new MapNote(noteInfo.Text, noteInfo.Color, noteInfo.Symbol);
        }
    }

    public abstract class MapsBase
    {
        protected MapSheet m_sheet;

        protected int GridWidth { get { return m_sheet.GridWidth; } }
        protected int GridHeight { get { return m_sheet.GridHeight; } }
        protected MapSquare[,] Grid { get { return m_sheet.Grid; } }
        protected MapLineInfo GridLines { get { return m_sheet.GridLines; } }
    }
}
