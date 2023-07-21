using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WhereAreWe
{
    public enum UltimaItemIndex
    {
        Coffin = -3,  // Virtual item
        Chest = -2,  // Virtual item
        None = -1,
        RedGem = 0,
        GreenGem = 1,
        BlueGem = 2,
        WhiteGem = 3,
        Skin = 4,
        LeatherArmor = 5,
        ChainMail = 6,
        PlateMail = 7,
        VacuumSuit = 8,
        ReflectSuit = 9,
        Hands = 10,
        Dagger = 11,
        Mace = 12,
        Axe = 13,
        RopeSpikes = 14,
        Sword = 15,
        GreatSword = 16,
        BowArrows = 17,
        Amulet = 18,
        Wand = 19,
        Staff = 20,
        Triangle = 21,
        Pistol = 22,
        LightSword = 23,
        Phazor = 24,
        Blaster = 25,
        Prayer = 26,
        Open = 27,
        Unlock = 28,
        MagicMissile = 29,
        Steal = 30,
        LadderDown = 31,
        LadderUp = 32,
        Blink = 33,
        Create = 34,
        Destroy = 35,
        Kill = 36,
        Foot = 37,
        Horse = 38,
        Cart = 39,
        Raft = 40,
        Frigate = 41,
        Aircar = 42,
        Shuttle = 43,
        TimeMachine = 44,
        EnemyVessels = 45,
        EndVehicles = 46,
        Last
    }

    public class UltimaItem : Item
    {
        public bool Ready;  // equipped
        public UltimaItemIndex ItemIndex;
        public override int Index { get => (int)ItemIndex; set { } }
        public override GameNames Game => GameNames.Ultima1;
        public override string Name => GetName(ItemIndex);
        public override bool IsUsableByAny(object filter) => true;
        public override int Count => m_iCount;
        public override int EmptyIndex => -1;

        private int m_iCount;

        public UltimaItem(UltimaItemIndex index, int count, bool ready = false)
        {
            Ready = ready;
            m_iCount = count;
            ItemIndex = index;
        }

        public override bool IsEquipped => Ready;
        public override bool IsWeapon => Type == ItemType.Weapon;

        public override ItemType Type
        {
            get
            {
                switch (ItemIndex)
                {
                    case UltimaItemIndex.RedGem:
                    case UltimaItemIndex.GreenGem:
                    case UltimaItemIndex.BlueGem:
                    case UltimaItemIndex.WhiteGem:
                        return ItemType.Miscellaneous;

                    case UltimaItemIndex.LeatherArmor:
                    case UltimaItemIndex.ChainMail:
                    case UltimaItemIndex.PlateMail:
                    case UltimaItemIndex.VacuumSuit:
                    case UltimaItemIndex.ReflectSuit:
                        return ItemType.Armor;

                    case UltimaItemIndex.RopeSpikes:
                    case UltimaItemIndex.Amulet:
                    case UltimaItemIndex.Wand:
                    case UltimaItemIndex.Staff:
                    case UltimaItemIndex.Dagger:
                    case UltimaItemIndex.Mace:
                    case UltimaItemIndex.Axe:
                    case UltimaItemIndex.Sword:
                    case UltimaItemIndex.GreatSword:
                    case UltimaItemIndex.BowArrows:
                    case UltimaItemIndex.Triangle:
                    case UltimaItemIndex.Pistol:
                    case UltimaItemIndex.LightSword:
                    case UltimaItemIndex.Phazor:
                    case UltimaItemIndex.Blaster:
                        return ItemType.Weapon;

                    case UltimaItemIndex.Open:
                    case UltimaItemIndex.Unlock:
                    case UltimaItemIndex.MagicMissile:
                    case UltimaItemIndex.Steal:
                    case UltimaItemIndex.LadderDown:
                    case UltimaItemIndex.LadderUp:
                    case UltimaItemIndex.Blink:
                    case UltimaItemIndex.Create:
                    case UltimaItemIndex.Destroy:
                    case UltimaItemIndex.Kill:
                        return ItemType.Spell;

                    case UltimaItemIndex.Horse:
                    case UltimaItemIndex.Cart:
                    case UltimaItemIndex.Raft:
                    case UltimaItemIndex.Frigate:
                    case UltimaItemIndex.Aircar:
                    case UltimaItemIndex.Shuttle:
                    case UltimaItemIndex.TimeMachine:
                        return ItemType.Vehicle;

                    case UltimaItemIndex.Skin:
                    case UltimaItemIndex.Hands:
                    case UltimaItemIndex.Prayer:
                    case UltimaItemIndex.Foot:
                    case UltimaItemIndex.EnemyVessels:
                    case UltimaItemIndex.EndVehicles:
                    case UltimaItemIndex.Chest:
                    case UltimaItemIndex.Coffin:
                        return ItemType.PseudoItem;

                    default:
                        return ItemType.Miscellaneous;
                }
            }
            set { }
        }

        public override bool IsMelee => IsWeapon;

        public override bool RangedCapable
        {
            get
            {
                switch (ItemIndex)
                {
                    case UltimaItemIndex.BowArrows:
                    case UltimaItemIndex.Pistol:
                    case UltimaItemIndex.Phazor:
                    case UltimaItemIndex.Blaster:
                    case UltimaItemIndex.Aircar:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public override string EquipEffects
        {
            get
            {
                switch (ItemIndex)
                {
                    case UltimaItemIndex.VacuumSuit:
                    case UltimaItemIndex.ReflectSuit:
                        return "Allow Space Docking";
                    case UltimaItemIndex.Triangle:
                    case UltimaItemIndex.Staff:
                        return "+200% Spell Damage";
                    case UltimaItemIndex.Wand:
                        return "+100% Spell Damage";
                    case UltimaItemIndex.Amulet:
                        return "+50% Spell Damage";
                    case UltimaItemIndex.RopeSpikes:
                        return "Escape Pit Traps";
                    default:
                        return "";
                }
            }
        }

        public override BasicDamage BaseDamage
        {
            get
            {
                if (!IsWeapon)
                    return BasicDamage.Zero;
                return new BasicDamage(1, new DamageDice(8 * (int)(Index - UltimaItemIndex.Dagger + 1), 1, 1));
            }
        }

        public override string GetDamAC()
        {
            if (IsWeapon)
                return BaseDamage.ToString();
            return "";
        }

        public override ItemNounType ItemBasicType
        {
            get
            {
                switch (Type)
                {
                    case ItemType.Weapon: return ItemNounType.Weapon;
                    case ItemType.Armor: return ItemNounType.Armor;
                    default: return ItemNounType.Misc;
                }
            }
        }

        public static string GetName(UltimaItemIndex index)
        {
            switch (index)
            {
                case UltimaItemIndex.RedGem: return "Red Gem";
                case UltimaItemIndex.GreenGem: return "Green Gem";
                case UltimaItemIndex.BlueGem: return "Blue Gem";
                case UltimaItemIndex.WhiteGem: return "White Gem";
                case UltimaItemIndex.Skin: return "Skin";
                case UltimaItemIndex.LeatherArmor: return "Leather Armor";
                case UltimaItemIndex.ChainMail: return "Chain Mail";
                case UltimaItemIndex.PlateMail: return "Plate Mail";
                case UltimaItemIndex.VacuumSuit: return "Vacuum Suit";
                case UltimaItemIndex.ReflectSuit: return "Reflect Suit";
                case UltimaItemIndex.Hands: return "Hands";
                case UltimaItemIndex.Dagger: return "Dagger";
                case UltimaItemIndex.Mace: return "Mace";
                case UltimaItemIndex.Axe: return "Axe";
                case UltimaItemIndex.RopeSpikes: return "Rope & Spikes";
                case UltimaItemIndex.Sword: return "Sword";
                case UltimaItemIndex.GreatSword: return "Great Sword";
                case UltimaItemIndex.BowArrows: return "Bow & Arrows";
                case UltimaItemIndex.Amulet: return "Amulet";
                case UltimaItemIndex.Wand: return "Wand";
                case UltimaItemIndex.Staff: return "Staff";
                case UltimaItemIndex.Triangle: return "Triangle";
                case UltimaItemIndex.Pistol: return "Pistol";
                case UltimaItemIndex.LightSword: return "Light Sword";
                case UltimaItemIndex.Phazor: return "Phazor";
                case UltimaItemIndex.Blaster: return "Blaster";
                case UltimaItemIndex.Prayer: return "Prayer";
                case UltimaItemIndex.Open: return "Open";
                case UltimaItemIndex.Unlock: return "Unlock";
                case UltimaItemIndex.MagicMissile: return "Magic Missile";
                case UltimaItemIndex.Steal: return "Steal";
                case UltimaItemIndex.LadderDown: return "Ladder Down";
                case UltimaItemIndex.LadderUp: return "Ladder Up";
                case UltimaItemIndex.Blink: return "Blink";
                case UltimaItemIndex.Create: return "Create";
                case UltimaItemIndex.Destroy: return "Destroy";
                case UltimaItemIndex.Kill: return "Kill";
                case UltimaItemIndex.Foot: return "Foot";
                case UltimaItemIndex.Horse: return "Horse";
                case UltimaItemIndex.Cart: return "Cart";
                case UltimaItemIndex.Raft: return "Raft";
                case UltimaItemIndex.Frigate: return "Frigate";
                case UltimaItemIndex.Aircar: return "Aircar";
                case UltimaItemIndex.Shuttle: return "Shuttle";
                case UltimaItemIndex.EnemyVessels: return "Enemy Vessels";
                case UltimaItemIndex.TimeMachine: return "Time Machine";
                case UltimaItemIndex.Chest: return "Chest";
                case UltimaItemIndex.Coffin: return "Coffin";
                case UltimaItemIndex.EndVehicles: return "(Invalid Vehicle)";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public override string UsableString
        {
            get
            {
                switch (ItemIndex)
                {
                    case UltimaItemIndex.Blink:
                    case UltimaItemIndex.Create:
                    case UltimaItemIndex.Kill:
                        return "W";
                    default:
                        return "FCWT";
                }
            }
        }

        public override long Value
        {
            get
            {
                switch (ItemIndex)
                {
                    case UltimaItemIndex.LeatherArmor:
                    case UltimaItemIndex.ChainMail:
                    case UltimaItemIndex.PlateMail:
                    case UltimaItemIndex.VacuumSuit:
                    case UltimaItemIndex.ReflectSuit:
                        return 50 * (ItemIndex - UltimaItemIndex.Skin);

                    case UltimaItemIndex.Dagger:
                    case UltimaItemIndex.Mace:
                    case UltimaItemIndex.Axe:
                    case UltimaItemIndex.RopeSpikes:
                    case UltimaItemIndex.Sword:
                    case UltimaItemIndex.GreatSword:
                    case UltimaItemIndex.BowArrows:
                    case UltimaItemIndex.Amulet:
                    case UltimaItemIndex.Wand:
                    case UltimaItemIndex.Staff:
                    case UltimaItemIndex.Triangle:
                    case UltimaItemIndex.Pistol:
                    case UltimaItemIndex.LightSword:
                    case UltimaItemIndex.Phazor:
                    case UltimaItemIndex.Blaster:
                        return (ItemIndex - UltimaItemIndex.Hands) * (ItemIndex - UltimaItemIndex.Hands) + 4;

                    case UltimaItemIndex.Open:
                    case UltimaItemIndex.Unlock:
                    case UltimaItemIndex.MagicMissile:
                    case UltimaItemIndex.Steal:
                    case UltimaItemIndex.LadderDown:
                    case UltimaItemIndex.LadderUp:
                    case UltimaItemIndex.Blink:
                    case UltimaItemIndex.Create:
                    case UltimaItemIndex.Destroy:
                    case UltimaItemIndex.Kill:
                        return 6 * (ItemIndex - UltimaItemIndex.Prayer);

                    case UltimaItemIndex.Horse:
                    case UltimaItemIndex.Cart:
                    case UltimaItemIndex.Raft:
                    case UltimaItemIndex.Frigate:
                    case UltimaItemIndex.Aircar:
                    case UltimaItemIndex.Shuttle:
                    case UltimaItemIndex.TimeMachine:
                    case UltimaItemIndex.EnemyVessels:
                        return (ItemIndex - UltimaItemIndex.Foot) * (ItemIndex - UltimaItemIndex.Foot) * 40;

                    default: return 0;
                }
            }
            set { }
        }

        public override string ItemNoun => GetName(ItemIndex);

        public static List<Item> InventoryFromBytes(byte[] bytes, int offset = 0)
        {
            List<Item> items = new List<Item>();
            for (UltimaItemIndex idx = 0; idx < UltimaItemIndex.Last; idx++)
            {
                int iCount = BitConverter.ToInt16(bytes, offset + (2 * (int)idx));
                if (iCount > 0)
                    items.Add(new UltimaItem(idx, iCount));
            }

            return items;
        }

        public override string ToString()
        {
            return String.Format("#{0}[{1}]: {2}", Index, MemoryIndex, DescriptionString);
        }

        public override string TypeString => GetItemNoun(ItemBasicType, "Misc");

        public override byte[] Serialize() { return BitConverter.GetBytes((short)Index); }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sbFull = new StringBuilder();
                // Basic item info only
                sbFull.AppendFormat("Item Index: {0}\r\n", (int)ItemIndex);
                if (IsWeapon)
                    sbFull.AppendFormat("Damage: {0}\r\n", DamageStringFull);
                if (IsArmor)
                    sbFull.AppendFormat("Dodge: {0}\r\n", DodgeString);
                sbFull.AppendFormat("Name: {0}\r\n", GetName(ItemIndex));
                sbFull.AppendFormat("Count: {0}\r\n", Count);
                return sbFull.ToString();
            }
        }

        public override string ArmorClassString => DodgeString;

        public string DodgeString
        {
            get
            {
                if (!IsArmor)
                    return "";
                return String.Format("+{0:F2}%", (float) (Index - UltimaItemIndex.Skin) * 1600 / 508);
            }
        }

        public override string GetLongDescription(GenericAlignmentValue currentAlign, GenericClass currentClass, string strOverrideName)
        {
            if (Count == 1)
                return GetName(ItemIndex);
            return String.Format("{0} ({1})", GetName(ItemIndex), Count);
        }

        public override string DescriptionString { get { return Name; } }
    }

    public abstract class Ultima123ItemList : InternExternList
    {
        public List<UltimaItem> Items;
        public byte[] RawBytes;
        public List<string> ItemNames;

        public virtual UltimaItem GetItem(int index, int memory = -1)
        {
            UltimaItem item = null;
            if (index < 0 || index >= Items.Count)
                index = 0;

            item = Items[index].Clone() as UltimaItem;
            item.MemoryIndex = memory;
            return item;
        }

        public static List<UltimaItem> ItemList()
        {
            List<UltimaItem> items = new List<UltimaItem>((int)UltimaItemIndex.Last);
            for (UltimaItemIndex idx = 0; idx < UltimaItemIndex.TimeMachine; idx++)
            {
                UltimaItem item = new UltimaItem(idx, 1);
                if (item.Type != ItemType.PseudoItem)
                    items.Add(new UltimaItem(idx, 1));
            }
            return items;
        }

        public List<String> GetNamesFromBytes(byte[] bytes)
        {
            return Global.GetNullTerminatedStrings(bytes, 0, bytes.Length);
        }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            List<string> gemsStrings = Global.GetNullTerminatedStrings(hacker.ReadOffset(Ultima1.Memory.GemNames, 37), 0, 37);
            List<string> armorStrings = Global.GetNullTerminatedStrings(hacker.ReadOffset(Ultima1.Memory.ArmorNames, 66), 0, 66);
            List<string> weaponStrings = Global.GetNullTerminatedStrings(hacker.ReadOffset(Ultima1.Memory.WeaponNames, 128), 0, 128);
            List<string> spellStrings = Global.GetNullTerminatedStrings(hacker.ReadOffset(Ultima1.Memory.SpellNames, 87), 0, 87);
            List<string> vehicleStrings = Global.GetNullTerminatedStrings(hacker.ReadOffset(Ultima1.Memory.VehicleNames, 57), 0, 57);

            MemoryStream stream = new MemoryStream();
            foreach (List<string> list in new List<string>[] { gemsStrings, armorStrings, weaponStrings, spellStrings, vehicleStrings })
            {
                foreach (string str in list)
                {
                    stream.Write(Encoding.ASCII.GetBytes(str), 0, str.Length);
                    stream.WriteByte(0);
                }
            }
            return stream.ToArray();
        }

        public override bool InitExternalList(MemoryHacker hacker, bool bOverrideSanityCheck = false)
        {
            if (!hacker.IsValid)
                return false;

            byte[] bytes = GetExternalBytes(hacker);

            if (bytes == null)
                return false;

            ItemNames = Global.GetNullTerminatedStrings(bytes, 0, bytes.Length);
            Items = ItemList(); // Not actually using the external item names at the moment
            m_bInternal = false;
            return true;
        }
    }
}
