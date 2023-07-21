using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    [Flags]
    public enum BTItemFlags
    {
        None =          0x00,
        Unidentified =  0x40,
        Equipped =      0x80
    }

    public enum BTAttackEffect
    {
        None = 0,
        Poison = 1,
        LevelDrain = 2,
        Insanity = 3,
        Old = 4,
        Possession = 5,
        Stone = 6,
        Critical = 7
    }

    public enum BTItemType
    {
        Misc = 0,
        Weapon = 1,
        Shield = 2,
        Armor = 3,
        Helmet = 4,
        Gloves = 5,
        Instrument = 6,
        Figurine = 7,
        Ring = 8,
        Wand = 9,
        Accessory = 10,
        Ranged = 11,
        Quiver = 12,
        Container = 13,
        Armor2 = 14,
    }

    [Flags]
    public enum BTUseFlags
    {
        None =        0x0000,
        Warrior =     0x0001,
        Paladin =     0x0002,
        Rogue =       0x0004,
        Bard =        0x0008,
        Hunter =      0x0010,
        Monk =        0x0020,
        Conjurer =    0x0040,
        Magician =    0x0080,
        Sorcerer =    0x0100,
        Wizard =      0x0200,
        Archmage =    0x0400,
        All =         0x07ff,

        CoMaSo = Conjurer | Magician | Sorcerer,
        ArWi = Archmage | Wizard,
    }

    public abstract class BTItem : Item
    {
        public byte[] Bytes;
        protected int m_index;
        public bool Identified;
        public BTItemType BTType;
        public BTAttackEffect WeaponEffect;
        public DamageDice Damage;
        public BTUseFlags Usable;
        public abstract int SpellIndex { get; }
        public abstract int EffectIndex { get; }
        public int AC;
        protected long m_value;

        public override bool IsIdentified { get { return Identified; } }
        public override long Value { get { return m_value; } set { m_value = value; } }
        public override BasicDamage BaseDamage { get { return new BasicDamage(1, Damage); } }
        public override int ArmorClass { get { return AC; } }

        public virtual void SetBytes(int index, byte[] bytes, int offset = 0)
        {
        }

        public override ItemNounType ItemBasicType
        {
            get
            {
                switch (BTType)
                {
                    case BTItemType.Misc: return ItemNounType.Misc;
                    case BTItemType.Weapon: return ItemNounType.Weapon;
                    case BTItemType.Shield: return ItemNounType.Shield;
                    case BTItemType.Armor: return ItemNounType.Armor;
                    case BTItemType.Helmet: return ItemNounType.Helmet;
                    case BTItemType.Gloves: return ItemNounType.Gloves;
                    case BTItemType.Instrument: return ItemNounType.Instrument;
                    case BTItemType.Figurine: return ItemNounType.Figurine;
                    case BTItemType.Ring: return ItemNounType.Ring;
                    case BTItemType.Wand: return ItemNounType.Wand;
                    case BTItemType.Accessory: return ItemNounType.Accessory;
                    case BTItemType.Ranged: return ItemNounType.Bow;
                    case BTItemType.Quiver: return ItemNounType.Quiver;
                    case BTItemType.Container: return ItemNounType.Container;
                    default: return ItemNounType.Misc;
                }
            }
        }

        public virtual byte[] GetBytes()
        {
            return Serialize();
        }

        public static byte GetValueByte(int val)
        {
            if (val < 32)
                return (byte)(val << 3);
            int iExp = 8;
            int iTest = 100000000;
            while (iTest > 1)
            {
                if (val < iTest)
                    iExp--;
                else
                    break;
                iTest /= 10;
            }
            return (byte)(iExp | ((val / iTest) << 3));
        }

        public static byte GetUsableByte(BTUseFlags btUse)
        {
            byte result = 0x00;
            if (btUse.HasFlag(BTUseFlags.Monk))
                result |= 0x01;
            if (btUse.HasFlag(BTUseFlags.Hunter))
                result |= 0x02;
            if (btUse.HasFlag(BTUseFlags.Paladin))
                result |= 0x04;
            if (btUse.HasFlag(BTUseFlags.Bard))
                result |= 0x08;
            if (btUse.HasFlag(BTUseFlags.Rogue))
                result |= 0x10;
            if (btUse.HasFlag(BTUseFlags.Wizard))
                result |= 0x20;
            if (btUse.HasFlag(BTUseFlags.CoMaSo))
                result |= 0x40;
            if (btUse.HasFlag(BTUseFlags.Warrior))
                result |= 0x80;
            return result;
        }

        public static BTUseFlags GetUsableBy(int btFlags)
        {
            BTUseFlags result = BTUseFlags.None;
            if ((btFlags & 0x01) != 0)
                result |= BTUseFlags.Monk;
            if ((btFlags & 0x02) != 0)
                result |= BTUseFlags.Hunter;
            if ((btFlags & 0x04) != 0)
                result |= BTUseFlags.Paladin;
            if ((btFlags & 0x08) != 0)
                result |= BTUseFlags.Bard;
            if ((btFlags & 0x10) != 0)
                result |= BTUseFlags.Rogue;
            if ((btFlags & 0x20) != 0)
                result |= BTUseFlags.ArWi;
            if ((btFlags & 0x40) != 0)
                result |= BTUseFlags.CoMaSo;
            if ((btFlags & 0x80) != 0)
                result |= BTUseFlags.Warrior;
            return result;
        }

        public static BTItemFlags ToBTItemFlags(BT3ItemFlags bt3Flags)
        {
            BTItemFlags flags = BTItemFlags.None;
            if (bt3Flags.HasFlag(BT3ItemFlags.Equipped))
                flags |= BTItemFlags.Equipped;
            if (bt3Flags.HasFlag(BT3ItemFlags.Unidentified))
                flags |= BTItemFlags.Unidentified;
            return flags;
        }

        public static BTItem FromInventoryBytes(GameNames game, byte[] bytes, int offset = 0)
        {
            if (game == GameNames.BardsTale3)
                return BT3Item.FromBT3InventoryBytes(bytes, offset);

            int iItem = bytes[offset];
            BTItem item = Games.GetBTGlobals(game).GetClonedItem(iItem);
            if (item == null)
                return item;
            BTItemFlags flags = (BTItemFlags) bytes[offset + 1];
            item.Identified = !flags.HasFlag(BTItemFlags.Unidentified);
            item.WhereEquipped = EquipLocation.None;
            if (game != GameNames.BardsTale1)
                item.ChargesCurrent = bytes[offset + 2];
            if (flags.HasFlag(BTItemFlags.Equipped))
                item.WhereEquipped = item.CanEquipLocation;
            return item;
        }

        public override CompareResult CompareTo(Item item)
        {
            if (!(item is BTItem))
                return CompareResult.Uncomparable;
            if (item == this)
                return CompareResult.Identical;
            if (Type != item.Type)
                return CompareResult.Uncomparable;

            BTItem btItem = item as BTItem;

            if (CanEquipLocation != btItem.CanEquipLocation)
                return CompareResult.Uncomparable;

            CompareResult result = CompareResult.Identical;

            if (btItem.SpellIndex != SpellIndex)
            {
                if (btItem.SpellIndex == 0)
                    result = CompareResult.Better;  // Any spell is better than no spell
                else if (SpellIndex == 0)
                    result = CompareResult.Worse;
                else
                    return CompareResult.Uncomparable;
            }
            if (btItem.EffectIndex != EffectIndex)
            {
                if (btItem.EffectIndex == 0)
                    result = CompareResult.Better;  // Any effect is better than no effect
                else if (EffectIndex == 0)
                    result = CompareResult.Worse;
                else
                    return CompareResult.Uncomparable;
            }
            if (btItem.WeaponEffect != WeaponEffect)
            {
                if (btItem.WeaponEffect == BTAttackEffect.None)
                    result = CompareResult.Better;
                else if (WeaponEffect == BTAttackEffect.None)
                    result = CompareResult.Worse;
                else
                    return CompareResult.Uncomparable;
            }
            if (!btItem.Identified || !Identified)
                return CompareResult.Uncomparable;

            switch (Type)
            {
                case ItemType.Weapon:
                case ItemType.OneHandMelee:
                case ItemType.TwoHandMelee:
                    return Item.Compare(result, CompareValues(Damage.Average, btItem.Damage.Average));
                default:
                    if (CanEquip && btItem.CanEquip)
                        return Item.Compare(result, CompareValues(ArmorClass, btItem.ArmorClass));
                    break;
            }
            return CompareResult.Uncomparable;
        }

        public override EquipLocation CanEquipLocation
        {
            get
            {
                EquipLocation loc = GetEquipLocation(ItemBasicType);
                if (loc != EquipLocation.None)
                    return loc;
                if (CanEquip)
                    return EquipLocation.Accessory;
                return EquipLocation.None;
            }
        }

        public abstract BTItem CreateItem(int index, byte[] bytes, int offset = 0);

        public override string ToString()
        {
            return String.Format("#{0}[{1}]: {2}", (int)Index, MemoryIndex, DescriptionString);
        }

        public static BTItem CreateRandom(List<BTItem> list, ItemType type, BaseCharacter charUsable)
        {
            IntDeck deck = new IntDeck(1, list.Count);
            deck.Shuffle();
            foreach(int i in deck.Cards)
            {
                if (type != ItemType.None && type != list[i].ItemBaseType)
                    continue;

                if (charUsable == null)
                    return list[i].Clone() as BTItem;

                if (list[i].IsUsableByAny(charUsable))
                    return list[i].Clone() as BTItem;
            }
            return list[0].Clone() as BTItem;
        }

        public virtual string GetName(int index) { return String.Format("Unknown({0})", index); }
        public override int ChargesCurrent { get { return -1; } set { } }
        public override string UsableString { get { return UsableByString(Usable, false); } }

        public static string UsableByString(BTUseFlags flags, bool bShowDots)
        {
            if (flags == BTUseFlags.All)
                return "<All>";
            string strDot = bShowDots ? "." : "";
            string strUsable = String.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}{9}",
                flags.HasFlag(BTUseFlags.Bard) ? "B" : strDot,
                flags.HasFlag(BTUseFlags.Conjurer) ? "C" : strDot,
                flags.HasFlag(BTUseFlags.Hunter) ? "H" : strDot,
                flags.HasFlag(BTUseFlags.Magician) ? "M" : strDot,
                flags.HasFlag(BTUseFlags.Monk) ? "K" : strDot,
                flags.HasFlag(BTUseFlags.Paladin) ? "P" : strDot,
                flags.HasFlag(BTUseFlags.Rogue) ? "R" : strDot,
                flags.HasFlag(BTUseFlags.Sorcerer) ? "S" : strDot,
                flags.HasFlag(BTUseFlags.Warrior) ? "W" : strDot,
                flags.HasFlag(BTUseFlags.Wizard) ? "Z" : strDot
                );
            return strUsable;
        }

        public override string TypeString { get { return GetItemNoun(ItemBasicType, "Misc"); } }

        public bool IsUsable(GenericClass gc)
        {
            switch (gc)
            {
                case GenericClass.Bard: return Usable.HasFlag(BTUseFlags.Bard);
                case GenericClass.Conjurer: return Usable.HasFlag(BTUseFlags.Conjurer);
                case GenericClass.Hunter: return Usable.HasFlag(BTUseFlags.Hunter);
                case GenericClass.Magician: return Usable.HasFlag(BTUseFlags.Magician);
                case GenericClass.Monk: return Usable.HasFlag(BTUseFlags.Monk);
                case GenericClass.Paladin: return Usable.HasFlag(BTUseFlags.Paladin);
                case GenericClass.Rogue: return Usable.HasFlag(BTUseFlags.Rogue);
                case GenericClass.Sorcerer: return Usable.HasFlag(BTUseFlags.Sorcerer);
                case GenericClass.Warrior: return Usable.HasFlag(BTUseFlags.Warrior);
                case GenericClass.Wizard: return Usable.HasFlag(BTUseFlags.Wizard);
                case GenericClass.Archmage: return Usable.HasFlag(BTUseFlags.Wizard);
                default: return false;
            }
        }

        public override bool IsUsableByAny(object filter)
        {
            if (filter is BaseCharacter)
                return IsUsable(((BaseCharacter)filter).BasicClass);
            if (!(filter is GenericClass))
                return true;
            return IsUsable((GenericClass)filter);
        }

        public override ItemType Type
        {
            get
            {
                switch (ItemBasicType)
                {
                    case ItemNounType.Bow:
                    case ItemNounType.Weapon: return ItemType.Weapon;
                    case ItemNounType.Shield:
                    case ItemNounType.Helm:
                    case ItemNounType.Helmet:
                    case ItemNounType.Gloves:
                    case ItemNounType.Armor: return ItemType.Armor;
                    case ItemNounType.Ring:
                    case ItemNounType.Wand:
                    case ItemNounType.Quiver:
                    case ItemNounType.Container:
                    case ItemNounType.Figurine: return ItemType.Accessory;
                    default: return ItemType.None;
                }
            }
            set { }
        }

        public virtual byte GetFlagByte()
        {
            BTItemFlags flags = BTItemFlags.None;
            if (!Identified)
                flags |= BTItemFlags.Unidentified;
            if (WhereEquipped != EquipLocation.None)
                flags |= BTItemFlags.Equipped;
            return (byte) flags;
        }

        public override byte[] Serialize() { return new byte[] { (byte) Index, GetFlagByte(), (byte) ChargesCurrent }; }

        public override string MultiLineDescription
        {
            get
            {
                ItemNounType itemType = ItemBasicType;
                string strItemType = GetItemNoun(itemType, "Miscellaneous");

                string strName = Name;

                StringBuilder sbUsableClass = new StringBuilder("Usable by class: ");
                if (Usable.HasFlag(BTUseFlags.All))
                    sbUsableClass.Append("ANY");
                else if (Usable == BTUseFlags.None)
                    sbUsableClass.Clear();
                else
                {
                    if (Usable.HasFlag(BTUseFlags.Bard))
                        sbUsableClass.Append("Bard, ");
                    if (Usable.HasFlag(BTUseFlags.Conjurer))
                        sbUsableClass.Append("Conjurer, ");
                    if (Usable.HasFlag(BTUseFlags.Hunter))
                        sbUsableClass.Append("Hunter, ");
                    if (Usable.HasFlag(BTUseFlags.Magician))
                        sbUsableClass.Append("Magician, ");
                    if (Usable.HasFlag(BTUseFlags.Monk))
                        sbUsableClass.Append("Monk, ");
                    if (Usable.HasFlag(BTUseFlags.Paladin))
                        sbUsableClass.Append("Paladin, ");
                    if (Usable.HasFlag(BTUseFlags.Rogue))
                        sbUsableClass.Append("Rogue, ");
                    if (Usable.HasFlag(BTUseFlags.Sorcerer))
                        sbUsableClass.Append("Sorcerer, ");
                    if (Usable.HasFlag(BTUseFlags.Warrior))
                        sbUsableClass.Append("Warrior, ");
                    if (Usable.HasFlag(BTUseFlags.Wizard))
                        sbUsableClass.Append("Wizard, ");
                    if (Usable.HasFlag(BTUseFlags.Archmage))
                        sbUsableClass.Append("Archmage, ");
                    Global.Trim(sbUsableClass);
                }

                string strDamageAC = "";
                StringBuilder sbEquip = new StringBuilder(EquipEffects + ", ");

                switch (itemType)
                {
                    case ItemNounType.Armor:
                    case ItemNounType.Shield:
                    case ItemNounType.Helmet:
                    case ItemNounType.Gloves:
                    case ItemNounType.Helm:
                        strDamageAC = String.Format("Armor Class: {0}", AC);
                        if (Damage.Max > 0)
                            sbEquip.AppendFormat("+{0} Damage, ", Damage.ToString());
                        break;
                    case ItemNounType.Weapon:
                        if (Damage.Max > 0)
                            strDamageAC = String.Format("Damage: {0}", Damage.ToString());
                        else
                            strDamageAC = "Damage: 0";
                        if (AC > 0)
                            sbEquip.AppendFormat("+{0} AC, ", AC);
                        break;
                    case ItemNounType.Accessory:
                        if (AC > 0)
                            strDamageAC = String.Format("Armor Class: {0}", AC);
                        break;
                    default:
                        break;
                }

                string strUse = UseEffectString;
                Global.Trim(sbEquip);

                StringBuilder sbFull = new StringBuilder();
                sbFull.AppendLine(strName);
                sbFull.AppendFormat("Type: {0}\r\n", strItemType);
                if (sbUsableClass.Length > 0)
                    sbFull.AppendFormat("{0}\r\n", sbUsableClass.ToString());
                if (!String.IsNullOrEmpty(strDamageAC))
                    sbFull.AppendLine(strDamageAC);
                if (!String.IsNullOrEmpty(strUse))
                    sbFull.AppendFormat("{0}{1}\r\n", PassiveEffect ? "" : "Use: ", strUse);
                if (sbEquip.Length > 0)
                    sbFull.AppendFormat("Equip: {0}\r\n", sbEquip.ToString());

                sbFull.AppendFormat("Value: {0} gold\r\n", Value);
                return sbFull.ToString();
            }
        }

        public override string GetLongDescription(GenericAlignmentValue currentAlign, GenericClass currentClass, string strOverrideName)
        {
            string strName = String.IsNullOrWhiteSpace(strOverrideName) ? (String.IsNullOrWhiteSpace(Name) ? "<no name>" : Name) : strOverrideName;

            string strUsable = "";
            if (currentClass != GenericClass.None && !IsUsable(currentClass))
                strUsable = String.Format(" (!{0})", MM1Character.ClassString(currentClass));
            //string strType = GetItemNoun(ItemBasicType, "Misc");

            string strDamage = String.Empty;
            if (Damage.Max > 0 && AC != 0)
                strDamage = String.Format("{0}, AC {1}", DamageStringFull, AC);
            else
                strDamage = Damage.Max > 0 ? DamageStringFull : AC != 0 ? String.Format("AC {0}", AC) : String.Empty;
            string strUse = UseEffectString;
            string strEquip = EquipEffects;

            return String.Format("{0}{1}, {2}{3}{4}{5} Gold",
                strName,
                strUsable,
                //String.IsNullOrEmpty(strType) ? "" : strType + " ",
                String.IsNullOrEmpty(strDamage) ? "" : strDamage + ", ",
                String.IsNullOrEmpty(strUse) ? "" : (PassiveEffect ? "" : "Use: ") + strUse + ", ",
                String.IsNullOrEmpty(strEquip) ? "" : "Equip: " + strEquip+ ", ",
                Value);
        }

        public override string DescriptionString { get { return Name; } }
        public override string DamageStringFull { get { return Damage.ToString(); } }


        public static string GetWeaponEffectString(BTAttackEffect index)
        {
            switch (index)
            {
                case BTAttackEffect.None: return String.Empty;
                case BTAttackEffect.Critical: return "Critical";
                case BTAttackEffect.LevelDrain: return "Level Drain";
                case BTAttackEffect.Old: return "Old";
                case BTAttackEffect.Poison: return "Poison";
                case BTAttackEffect.Possession: return "Possession";
                case BTAttackEffect.Insanity: return "Insane";
                case BTAttackEffect.Stone: return "Stone";

                default:
                    return String.Format("Unknown({0})", (int)index);
            }
        }

        public override string ItemNoun
        {
            get
            {
                switch (ItemBasicType)
                {
                    case ItemNounType.Armor: return "Armor";
                    case ItemNounType.Gloves: return "Gloves";
                    case ItemNounType.Helmet: return "Helmet";
                    case ItemNounType.Shield: return "Shield";
                    case ItemNounType.Weapon: return "Weapon";
                    case ItemNounType.Instrument: return "Instrument";
                    case ItemNounType.Ring: return "Ring";
                    case ItemNounType.Figurine: return "Figurine";
                    case ItemNounType.Wand: return "Wand";
                    case ItemNounType.Accessory: return "Accessory";
                    case ItemNounType.Container: return "Container";
                    case ItemNounType.Item: return "Item";
                    case ItemNounType.Bow: return "Container";
                    case ItemNounType.Quiver: return "Quiver";
                    default: return String.Format("Unknown Item");
                }
            }
        }

    }

    public abstract class BT123ItemList : InternExternList
    {
        public List<BTItem> Items;

        public virtual BTItem CreateItem(int iItemCount, byte[] bytes, int iPos) { return null; }
        public virtual int ItemLength { get { return 8; } }
        public virtual int BlockSize { get { return 128; } }

        public List<BTItem> SetFromBytes(byte[] bytes)
        {
            int iNumItems = bytes.Length / ItemLength;
            List<BTItem> items = new List<BTItem>(iNumItems);

            try
            {
                for(int iIndex = 0; iIndex < iNumItems; iIndex++)
                {
                    byte[] bytesItem = new byte[ItemLength];
                    for (int iBlock = 0; iBlock < ItemLength; iBlock++)
                        bytesItem[iBlock] = bytes[iIndex + (iBlock * BlockSize)];
                    BTItem item = CreateItem(iIndex, bytesItem, 0);
                    items.Add(item);
                }

                m_bValid = true;
            }
            catch (Exception)
            {
                m_bValid = false;
            }

            return items;
        }

        public virtual BTItem GetItem(int index, int memory = -1)
        {
            BTItem item = null;
            if (index < 0 || index >= Items.Count)
                index = 0;

            item = Items[index].Clone() as BTItem;
            item.MemoryIndex = memory;
            return item;
        }

        public override bool InitExternalList(MemoryHacker hacker, bool bOverrideSanityCheck = false)
        {
            if (!hacker.IsValid)
                return false;

            byte[] bytes = GetExternalBytes(hacker);
            if (bytes == null)
                return false;

            Items = SetFromBytes(bytes);
            m_bInternal = false;
            return true;
        }

        public override bool InitInternalList()
        {
            Items = SetFromBytes(GetInternalBytes());
            m_bInternal = true;
            return true;
        }
    }
}
