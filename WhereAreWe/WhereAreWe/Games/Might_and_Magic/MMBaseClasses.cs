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
    public class ShopItem
    {
        public Item Item;
        public long Offset;
        public int Multiplier;

        public ShopItem(Item item, long offset, int multiplier)
        {
            Item = item;
            Offset = offset;
            Multiplier = multiplier;
        }
    }

    public class ShopInventory
    {
        public string Town;
        public virtual IEnumerable<ShopItem> AllItems { get { return null; } }
    }

    public class Shops
    {
        public byte[] RawBytes;
        public List<ShopInventory> Inventories;
        public List<ShopItem> CurrentDisplay;
        public bool InShop = false;
        public Subscreen Screen;
    }

    public class MMInternalSpellIndex
    {
        private int m_index;
        private SpellType m_type;
        private GameNames m_game;

        private MMInternalSpellIndex(int i)
        {
            m_index = i;
            m_game = GameNames.None;
        }

        public MMInternalSpellIndex(MM3InternalSpellIndex index)
        {
            Set(index);
        }

        public MMInternalSpellIndex(byte index, SpellType type)
        {
            Set(index, type);
        }

        public void Set(MM3InternalSpellIndex index)
        {
            m_game = GameNames.MightAndMagic3;
            m_index = (int)index;
        }

        public void Set(byte index, SpellType type)
        {
            if (index > MM45Character.MaxReadySpell)
                index = MM45Character.MaxReadySpell;

            switch (type)
            {
                case SpellType.Cleric:
                    m_index = index + (int)MM45SpellIndex.FirstCleric;
                    break;
                case SpellType.Sorcerer:
                    m_index = index + (int)MM45SpellIndex.FirstArcane;
                    break;
                case SpellType.Druid:
                    m_index = index + (int)MM45SpellIndex.FirstDruid;
                    break;
                default:
                    m_index = index;
                    break;
            }

            m_game = GameNames.MightAndMagic45;
            m_type = type;
        }

        public void Set(SpellSelectItem ssi)
        {
            m_game = ssi.Game;
            m_index = ssi.Index;
            m_type = ssi.Spell.Type;
        }

        public int RawIndex { get { return m_index; } }

        public int CorrectedIndex
        {
            get
            {
                switch (m_game)
                {
                    case GameNames.MightAndMagic45:
                        switch (m_type)
                        {
                            case SpellType.Cleric: return m_index - (int)MM45SpellIndex.FirstCleric;
                            case SpellType.Sorcerer: return m_index - (int)MM45SpellIndex.FirstArcane;
                            case SpellType.Druid: return m_index - (int)MM45SpellIndex.FirstDruid;
                            default: return m_index;
                        }
                    default:
                        return m_index;
                }
            }
        }

        public static MMInternalSpellIndex None { get { return new MMInternalSpellIndex(0); } }

        public static implicit operator MM3InternalSpellIndex(MMInternalSpellIndex index)
        {
            return (MM3InternalSpellIndex)index;
        }
    }

    public enum MMSecondarySkillIndex
    {
        Thievery = 0,
        ArmsMaster,
        Astrologer,
        BodyBuilder,
        Cartographer,
        Crusader,
        DirectionSense,
        Linguist,
        Merchant,
        Mountaineer,
        Navigator,
        PathFinder,
        PrayerMaster,
        Prestidigitator,
        Swimmer,
        Tracker,
        SpotSecretDoors,
        DangerSense,
        Last
    }

    public class MMSecondarySkills
    {
        public byte Thievery;
        public byte ArmsMaster;
        public byte Astrologer;
        public byte BodyBuilder;
        public byte Cartographer;
        public byte Crusader;
        public byte DirectionSense;
        public byte Linguist;
        public byte Merchant;
        public byte Mountaineer;
        public byte Navigator;
        public byte PathFinder;
        public byte PrayerMaster;
        public byte Prestidigitator;
        public byte Swimmer;
        public byte Tracker;
        public byte SpotSecretDoors;
        public byte DangerSense;

        public MMSecondarySkills(byte[] bytes, int index = 0)
        {
            SetFromBytes(bytes, index);
        }

        public MMSecondarySkills()
        {
            SetAll(0);
        }

        public MMSecondarySkills(byte b)
        {
            SetAll(b);
        }

        public void SetAll(byte b)
        {
            Thievery = b;
            ArmsMaster = b;
            Astrologer = b;
            BodyBuilder = b;
            Cartographer = b;
            Crusader = b;
            DirectionSense = b;
            Linguist = b;
            Merchant = b;
            Mountaineer = b;
            Navigator = b;
            PathFinder = b;
            PrayerMaster = b;
            Prestidigitator = b;
            Swimmer = b;
            Tracker = b;
            SpotSecretDoors = b;
            DangerSense = b;
        }

        protected void SetFromBytes(byte[] bytes, int index)
        {
            if (bytes.Length < 18)
                return;

            Thievery = bytes[index + 0];
            ArmsMaster = bytes[index + 1];
            Astrologer = bytes[index + 2];
            BodyBuilder = bytes[index + 3];
            Cartographer = bytes[index + 4];
            Crusader = bytes[index + 5];
            DirectionSense = bytes[index + 6];
            Linguist = bytes[index + 7];
            Merchant = bytes[index + 8];
            Mountaineer = bytes[index + 9];
            Navigator = bytes[index + 10];
            PathFinder = bytes[index + 11];
            PrayerMaster = bytes[index + 12];
            Prestidigitator = bytes[index + 13];
            Swimmer = bytes[index + 14];
            Tracker = bytes[index + 15];
            SpotSecretDoors = bytes[index + 16];
            DangerSense = bytes[index + 17];
        }

        protected void SetFromSkills(MM3QuestStates.Skills skills)
        {
            Thievery = (byte)(skills.HasFlag(MM3QuestStates.Skills.Thievery) ? 1 : 0);
            ArmsMaster = (byte)(skills.HasFlag(MM3QuestStates.Skills.ArmsMaster) ? 1 : 0);
            Astrologer = (byte)(skills.HasFlag(MM3QuestStates.Skills.Astrologer) ? 1 : 0);
            BodyBuilder = (byte)(skills.HasFlag(MM3QuestStates.Skills.BodyBuilder) ? 1 : 0);
            Cartographer = (byte)(skills.HasFlag(MM3QuestStates.Skills.Cartographer) ? 1 : 0);
            Crusader = (byte)(skills.HasFlag(MM3QuestStates.Skills.Crusader) ? 1 : 0);
            DirectionSense = (byte)(skills.HasFlag(MM3QuestStates.Skills.DirectionSense) ? 1 : 0);
            Linguist = (byte)(skills.HasFlag(MM3QuestStates.Skills.Linguist) ? 1 : 0);
            Merchant = (byte)(skills.HasFlag(MM3QuestStates.Skills.Merchant) ? 1 : 0);
            Mountaineer = (byte)(skills.HasFlag(MM3QuestStates.Skills.Mountaineer) ? 1 : 0);
            Navigator = (byte)(skills.HasFlag(MM3QuestStates.Skills.Navigator) ? 1 : 0);
            PathFinder = (byte)(skills.HasFlag(MM3QuestStates.Skills.PathFinder) ? 1 : 0);
            PrayerMaster = (byte)(skills.HasFlag(MM3QuestStates.Skills.PrayerMaster) ? 1 : 0);
            Prestidigitator = (byte)(skills.HasFlag(MM3QuestStates.Skills.Prestidigitator) ? 1 : 0);
            Swimmer = (byte)(skills.HasFlag(MM3QuestStates.Skills.Swimmer) ? 1 : 0);
            Tracker = (byte)(skills.HasFlag(MM3QuestStates.Skills.Tracker) ? 1 : 0);
            SpotSecretDoors = (byte)(skills.HasFlag(MM3QuestStates.Skills.SpotSecretDoors) ? 1 : 0);
            DangerSense = (byte)(skills.HasFlag(MM3QuestStates.Skills.DangerSense) ? 1 : 0);
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[18];
            bytes[0] = Thievery;
            bytes[1] = ArmsMaster;
            bytes[2] = Astrologer;
            bytes[3] = BodyBuilder;
            bytes[4] = Cartographer;
            bytes[5] = Crusader;
            bytes[6] = DirectionSense;
            bytes[7] = Linguist;
            bytes[8] = Merchant;
            bytes[9] = Mountaineer;
            bytes[10] = Navigator;
            bytes[11] = PathFinder;
            bytes[12] = PrayerMaster;
            bytes[13] = Prestidigitator;
            bytes[14] = Swimmer;
            bytes[15] = Tracker;
            bytes[16] = SpotSecretDoors;
            bytes[17] = DangerSense;
            return bytes;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Thievery > 0)
                sb.AppendFormat("Thievery, ");
            if (ArmsMaster > 0)
                sb.AppendFormat("Arms Master, ");
            if (Astrologer > 0)
                sb.AppendFormat("Astrologer, ");
            if (BodyBuilder > 0)
                sb.AppendFormat("Body Builder, ");
            if (Cartographer > 0)
                sb.AppendFormat("Cartographer, ");
            if (Crusader > 0)
                sb.AppendFormat("Crusader, ");
            if (DirectionSense > 0)
                sb.AppendFormat("Direction Sense, ");
            if (Linguist > 0)
                sb.AppendFormat("Linguist, ");
            if (Merchant > 0)
                sb.AppendFormat("Merchant, ");
            if (Mountaineer > 0)
                sb.AppendFormat("Mountaineer, ");
            if (Navigator > 0)
                sb.AppendFormat("Navigator, ");
            if (PathFinder > 0)
                sb.AppendFormat("Path Finder, ");
            if (PrayerMaster > 0)
                sb.AppendFormat("Prayer Master, ");
            if (Prestidigitator > 0)
                sb.AppendFormat("Prestidigitator, ");
            if (Swimmer > 0)
                sb.AppendFormat("Swimmer, ");
            if (Tracker > 0)
                sb.AppendFormat("Tracker, ");
            if (SpotSecretDoors > 0)
                sb.AppendFormat("Spot Secret Doors, ");
            if (DangerSense > 0)
                sb.AppendFormat("Danger Sense, ");
            if (Global.Trim(sb).Length == 0)
                return "None";
            return sb.ToString();
        }

        public string AbbreviatedString()
        {
            StringBuilder sb = new StringBuilder();
            if (Thievery > 0)
                sb.AppendFormat("Th,");
            if (ArmsMaster > 0)
                sb.AppendFormat("Ar,");
            if (Astrologer > 0)
                sb.AppendFormat("As,");
            if (BodyBuilder > 0)
                sb.AppendFormat("Bo,");
            if (Cartographer > 0)
                sb.AppendFormat("Ca,");
            if (Crusader > 0)
                sb.AppendFormat("Cr,");
            if (DirectionSense > 0)
                sb.AppendFormat("Di,");
            if (Linguist > 0)
                sb.AppendFormat("Li,");
            if (Merchant > 0)
                sb.AppendFormat("Me,");
            if (Mountaineer > 0)
                sb.AppendFormat("Mo,");
            if (Navigator > 0)
                sb.AppendFormat("Na,");
            if (PathFinder > 0)
                sb.AppendFormat("Pa,");
            if (PrayerMaster > 0)
                sb.AppendFormat("Pra,");
            if (Prestidigitator > 0)
                sb.AppendFormat("Pre,");
            if (Swimmer > 0)
                sb.AppendFormat("Sw,");
            if (Tracker > 0)
                sb.AppendFormat("Tr,");
            if (SpotSecretDoors > 0)
                sb.AppendFormat("Sp,");
            if (DangerSense > 0)
                sb.AppendFormat("Se,");
            if (Global.Trim(sb).Length == 0)
                return "None";

            string strOut = sb.ToString();
            return String.Format("{0}: {1}", strOut.Count(c => c == ',') + 1, strOut);
        }

        public string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Thievery > 0)
                    sb.AppendFormat("Thievery: {0}\r\n", Description(MMSecondarySkillIndex.Thievery));
                if (ArmsMaster > 0)
                    sb.AppendFormat("Arms Master: {0}\r\n", Description(MMSecondarySkillIndex.ArmsMaster));
                if (Astrologer > 0)
                    sb.AppendFormat("Astrologer: {0}\r\n", Description(MMSecondarySkillIndex.Astrologer));
                if (BodyBuilder > 0)
                    sb.AppendFormat("BodyBuilder: {0}\r\n", Description(MMSecondarySkillIndex.BodyBuilder));
                if (Cartographer > 0)
                    sb.AppendFormat("Cartographer: {0}\r\n", Description(MMSecondarySkillIndex.Cartographer));
                if (Crusader > 0)
                    sb.AppendFormat("Crusader: {0}\r\n", Description(MMSecondarySkillIndex.Crusader));
                if (DirectionSense > 0)
                    sb.AppendFormat("Direction Sense: {0}\r\n", Description(MMSecondarySkillIndex.DirectionSense));
                if (Linguist > 0)
                    sb.AppendFormat("Linguist: {0}\r\n", Description(MMSecondarySkillIndex.Linguist));
                if (Merchant > 0)
                    sb.AppendFormat("Merchant: {0}\r\n", Description(MMSecondarySkillIndex.Merchant));
                if (Mountaineer > 0)
                    sb.AppendFormat("Mountaineer: {0}\r\n", Description(MMSecondarySkillIndex.Mountaineer));
                if (Navigator > 0)
                    sb.AppendFormat("Navigator: {0}\r\n", Description(MMSecondarySkillIndex.Navigator));
                if (PathFinder > 0)
                    sb.AppendFormat("PathFinder: {0}\r\n", Description(MMSecondarySkillIndex.PathFinder));
                if (PrayerMaster > 0)
                    sb.AppendFormat("Prayer Master: {0}\r\n", Description(MMSecondarySkillIndex.PrayerMaster));
                if (Prestidigitator > 0)
                    sb.AppendFormat("Prestidigitator: {0}\r\n", Description(MMSecondarySkillIndex.Prestidigitator));
                if (Swimmer > 0)
                    sb.AppendFormat("Swimmer: {0}\r\n", Description(MMSecondarySkillIndex.Swimmer));
                if (Tracker > 0)
                    sb.AppendFormat("Tracker: {0}\r\n", Description(MMSecondarySkillIndex.Tracker));
                if (SpotSecretDoors > 0)
                    sb.AppendFormat("Spot Secret Doors: {0}\r\n", Description(MMSecondarySkillIndex.SpotSecretDoors));
                if (DangerSense > 0)
                    sb.AppendFormat("Danger Sense: {0}\r\n", Description(MMSecondarySkillIndex.DangerSense));
                return sb.ToString();
            }
        }

        public string NumericString()
        {
            StringBuilder sb = new StringBuilder();
            if (Thievery > 0)
                sb.AppendFormat("Thievery: {0}, ", Thievery);
            if (ArmsMaster > 0)
                sb.AppendFormat("Arms Master: {0}, ", ArmsMaster);
            if (Astrologer > 0)
                sb.AppendFormat("Astrologer: {0}, ", Astrologer);
            if (BodyBuilder > 0)
                sb.AppendFormat("Body Builder: {0}, ", BodyBuilder);
            if (Cartographer > 0)
                sb.AppendFormat("Cartographer: {0}, ", Cartographer);
            if (Crusader > 0)
                sb.AppendFormat("Crusader: {0}, ", Crusader);
            if (DirectionSense > 0)
                sb.AppendFormat("Direction Sense: {0}, ", DirectionSense);
            if (Linguist > 0)
                sb.AppendFormat("Linguist: {0}, ", Linguist);
            if (Merchant > 0)
                sb.AppendFormat("Merchant: {0}, ", Merchant);
            if (Mountaineer > 0)
                sb.AppendFormat("Mountaineer: {0}, ", Mountaineer);
            if (Navigator > 0)
                sb.AppendFormat("Navigator: {0}, ", Navigator);
            if (PathFinder > 0)
                sb.AppendFormat("Path Finder: {0}, ", PathFinder);
            if (PrayerMaster > 0)
                sb.AppendFormat("Prayer Master: {0}, ", PrayerMaster);
            if (Prestidigitator > 0)
                sb.AppendFormat("Prestidigitator: {0}, ", Prestidigitator);
            if (Swimmer > 0)
                sb.AppendFormat("Swimmer: {0}, ", Swimmer);
            if (Tracker > 0)
                sb.AppendFormat("Tracker: {0}, ", Tracker);
            if (SpotSecretDoors > 0)
                sb.AppendFormat("Spot Secret Doors: {0}, ", SpotSecretDoors);
            if (DangerSense > 0)
                sb.AppendFormat("Danger Sense: {0}, ", DangerSense);
            return Global.Trim(sb).ToString();
        }

        public static string Name(MMSecondarySkillIndex skill)
        {
            switch (skill)
            {
                case MMSecondarySkillIndex.Thievery: return "Thievery";
                case MMSecondarySkillIndex.ArmsMaster: return "Arms Master";
                case MMSecondarySkillIndex.Astrologer: return "Astrologer";
                case MMSecondarySkillIndex.BodyBuilder: return "Body Builder";
                case MMSecondarySkillIndex.Cartographer: return "Cartographer";
                case MMSecondarySkillIndex.Crusader: return "Crusader";
                case MMSecondarySkillIndex.DirectionSense: return "Direction Sense";
                case MMSecondarySkillIndex.Linguist: return "Linguist";
                case MMSecondarySkillIndex.Merchant: return "Merchant";
                case MMSecondarySkillIndex.Mountaineer: return "Mountaineer";
                case MMSecondarySkillIndex.Navigator: return "Navigator";
                case MMSecondarySkillIndex.PathFinder: return "Path Finder";
                case MMSecondarySkillIndex.PrayerMaster: return "Prayer Master";
                case MMSecondarySkillIndex.Prestidigitator: return "Prestidigitator";
                case MMSecondarySkillIndex.Swimmer: return "Swimmer";
                case MMSecondarySkillIndex.Tracker: return "Tracker";
                case MMSecondarySkillIndex.SpotSecretDoors: return "Spot Secret Doors";
                case MMSecondarySkillIndex.DangerSense: return "Danger Sense";
                default: return "None";
            }
        }

        public static string Description(MMSecondarySkillIndex skill)
        {
            switch (skill)
            {
                case MMSecondarySkillIndex.ArmsMaster: return "+1 to hit";
                case MMSecondarySkillIndex.Astrologer: return "Extra spell points per level for Druids (2) or Rangers (1)";
                case MMSecondarySkillIndex.BodyBuilder: return "+1 hit point per level";
                case MMSecondarySkillIndex.Cartographer: return "Enable auto-mapping feature";
                case MMSecondarySkillIndex.Crusader: return "Allow entry to castles if all characters have this skill";
                case MMSecondarySkillIndex.DangerSense: return "Know if there are foes nearby";
                case MMSecondarySkillIndex.DirectionSense: return "Enable compass feature";
                case MMSecondarySkillIndex.Linguist: return "Read foreign languages";
                case MMSecondarySkillIndex.Merchant: return "Receive full value when selling items";
                case MMSecondarySkillIndex.Mountaineer: return "Pass through mountains if two characters have this skill";
                case MMSecondarySkillIndex.Navigator: return "Prevent the party from becoming lost";
                case MMSecondarySkillIndex.PathFinder: return "Pass through dense forest if two characters have this skill";
                case MMSecondarySkillIndex.PrayerMaster: return "Extra spell points per level for Clerics (2) or Paladins (1)";
                case MMSecondarySkillIndex.Prestidigitator: return "Extra spell points per level for Sorcerers (2) or Archers (1)";
                case MMSecondarySkillIndex.SpotSecretDoors: return "Allows detection of walls that can be bashed down";
                case MMSecondarySkillIndex.Swimmer: return "Allow movement in shallow water if all characters have this skill";
                case MMSecondarySkillIndex.Thievery: return "Allows picking locks on chests and doors";
                case MMSecondarySkillIndex.Tracker: return "Does nothing";
                default: return "Unknown";
            }
        }

        public virtual string WhereLearned(MMSecondarySkillIndex skill) { return "Unknown"; }
    }

    public enum ItemType
    {
        None,
        Weapon,
        OneHandMelee,
        TwoHandMelee,
        Missile,
        Armor,
        Accessory,
        Miscellaneous,
        Quest,
        FirstScreen,
        SecondScreen,
        Any
    }

    public abstract class MMItem : Item
    {
        private bool m_bBroken = false;

        public byte m_iChargesCurrent;
        public override int ChargesCurrent { get { return m_iChargesCurrent; } set { m_iChargesCurrent = (byte) value; } }
        public override bool Broken { get { return m_bBroken; } set { m_bBroken = value; } }

        public virtual string ScriptString { get { return DescriptionString; } }
        public override bool Duplicatable { get { return true; } }

        public override bool Matches(ItemType type)
        {
            if (ItemBaseType == type)
                return true;
            if (type == ItemType.FirstScreen)
                return (MemoryIndex < 9);
            if (type == ItemType.SecondScreen)
                return (MemoryIndex > 8);
            return false;
        }
    }

    public abstract class MMMetaItem : MMItem
    {
        public override string UsableString { get { return String.Empty; } }
        public override string UsableByAlignment { get { return String.Empty; } }
    }

    public class MM3ElementalItem : MMMetaItem
    {
        public override GameNames Game { get { return GameNames.MightAndMagic3; } }
        public MM3ItemElementalIndex InternalIndex;
        public MM3ElementalItem(MM3ItemElementalIndex index) { InternalIndex = index; }
        public override int Index { get { return (int)InternalIndex; } set { InternalIndex = (MM3ItemElementalIndex)value; } }
        public override string Name { get { return MM3Item.GetItemElementalString(InternalIndex); } }
        public override string TypeString { get { return "Element"; } }
        public override string ElementString { get { return MM3Item.GetElementString(InternalIndex); } }
        public override string AttributeString { get { return String.Format("{0} Res", ElementString.Replace("Electric","Elec.")); } }
        public override int EquipBonusValue { get { return MM3Item.ElementalEffect(InternalIndex).ResistValue; } }
        public override BasicDamage BaseDamage { get { return new BasicDamage(1, new DamageDice(1, 0, MM3Item.ElementalEffect(InternalIndex).DamageValue)); } }
        public override string ValueString { get { return Global.AddPlus(MM3Item.ElementalValueModifier(InternalIndex), false); } }
        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(String.Format("Elemental prefix: {0}\r\n", Global.Title(Name)));
                sb.AppendFormat(String.Format("{0} resistance: {1}\r\n", Global.Title(ElementString), Global.AddPlus(EquipBonusValue)));
                sb.AppendFormat(String.Format("{0} damage: {1}\r\n", Global.Title(ElementString), BaseDamage.ToString()));
                sb.AppendFormat(String.Format("Value: {0}\r\n", ValueString));
                return sb.ToString();
            }
        }
    }

    public class MM3AttributeItem : MMMetaItem
    {
        public override GameNames Game { get { return GameNames.MightAndMagic3; } }
        public MM3ItemAttributeIndex InternalIndex;
        public MM3AttributeItem(MM3ItemAttributeIndex index) { InternalIndex = index; }
        public override int Index { get { return (int)InternalIndex; } set { InternalIndex = (MM3ItemAttributeIndex)value; } }
        public override string Name { get { return MM3Item.GetItemAttributeString(InternalIndex); } }
        public override string TypeString { get { return "Attribute"; } }
        public override string AttributeString { get { return Global.GenericAttributeString(MM3Item.AttributeEffect(InternalIndex).Attribute); } }
        public override int EquipBonusValue { get { return MM3Item.AttributeEffect(InternalIndex).Modifier; } }
        public override string ValueString { get { return Global.AddPlus(MM3Item.AttributeValueModifier(InternalIndex), false); } }
        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(String.Format("Attribute prefix: {0}\r\n", Global.Title(Name)));
                sb.AppendFormat(String.Format("{0} bonus: {1}\r\n", Global.Title(AttributeString), Global.AddPlus(EquipBonusValue)));
                sb.AppendFormat(String.Format("Value: {0}\r\n", ValueString));
                return sb.ToString();
            }
        }
    }

    public class MM3MaterialItem : MMMetaItem
    {
        public override GameNames Game { get { return GameNames.MightAndMagic3; } }
        public MM3ItemMaterialIndex InternalIndex;
        public MM3MaterialItem(MM3ItemMaterialIndex index) { InternalIndex = index; }
        public override int Index { get { return (int)InternalIndex; } set { InternalIndex = (MM3ItemMaterialIndex)value; } }
        public override string Name { get { return MM3Item.GetItemMaterialString(InternalIndex); } }
        public override string TypeString { get { return "Material"; } }
        public override int ArmorClass { get { return MM3Item.MaterialEffect(InternalIndex).AC; } }
        public override BasicDamage BaseDamage { get { return new BasicDamage(1, new DamageDice(1, 0, MM3Item.MaterialEffect(InternalIndex).Damage)) ; } }
        public override string AttributeString { get { return "To-Hit"; } }
        public override int EquipBonusValue { get { return MM3Item.MaterialEffect(InternalIndex).Hit; } }
        public override string ValueString { get { return String.Format("x{0}", MM3Item.MaterialValueMultiplier(InternalIndex)); } }
        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(String.Format("Material prefix: {0}\r\n", Global.Title(Name)));
                sb.AppendFormat(String.Format("To-Hit: {0}\r\n", Global.AddPlus(EquipBonusValue)));
                sb.AppendFormat(String.Format("Damage: {0}\r\n", Global.AddPlus(BaseDamage.Bonus)));
                sb.AppendFormat(String.Format("Armor Class: {0}\r\n", Global.AddPlus(ArmorClass)));
                sb.AppendFormat(String.Format("Value multiplier: {0}\r\n", ValueString));
                return sb.ToString();
            }
        }
    }

    public class MM3PropertyItem : MMMetaItem
    {
        public override GameNames Game { get { return GameNames.MightAndMagic3; } }
        public MM3ItemPropertyIndex InternalIndex;
        public MM3PropertyItem(MM3ItemPropertyIndex index) { InternalIndex = index; }
        public override int Index { get { return (int)InternalIndex; } set { InternalIndex = (MM3ItemPropertyIndex)value; } }
        public override string Name { get { return String.Format("of {0}", MM3Item.GetItemPropertyString(InternalIndex)); } }
        public override string TypeString { get { return "Property"; } }
        public override string UseEffectString { get { return Global.Title(MM3SpellList.GetSpellName(MM3Item.ItemPropertyEffect(InternalIndex))); } }
        public override string ValueString { get { return Global.AddPlus(MM3Item.PropertyValueModifier(InternalIndex), false); } }
        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(String.Format("Property suffix: {0}\r\n", Name));
                sb.AppendFormat(String.Format("Casts spell: {0}\r\n", UseEffectString));
                sb.AppendFormat(String.Format("Value: {0}\r\n", ValueString));
                return sb.ToString();
            }
        }
    }

    public class MM45PrefixItem : MMMetaItem
    {
        public override GameNames Game { get { return GameNames.MightAndMagic45; } }
        public MM45ItemPrefixIndex InternalIndex;
        public MM45PrefixItem(MM45ItemPrefixIndex index) { InternalIndex = index; }
        public override int Index { get { return (int)InternalIndex; } set { InternalIndex = (MM45ItemPrefixIndex)value; } }
        public override string Name { get { return MM45Item.GetItemPrefix(ItemType.Miscellaneous, InternalIndex); } }
        public override string TypeString { get { return "Prefix"; } }

        public override string ElementString
        {
            get
            {
                if (!MM45Item.IsElemental(InternalIndex))
                    return String.Empty;
                return Global.SingleResistance(MM45Item.ElementalEffect(InternalIndex).DamageElement);
            }
        }

        public override int ArmorClass
        {
            get
            {
                if (MM45Item.IsMaterial(InternalIndex))
                    return MM45Item.PrefixHDAC(InternalIndex).AC;
                return 0;
            }
        }

        public override BasicDamage BaseDamage
        {
            get
            {
                if (MM45Item.IsElemental(InternalIndex))
                    return new BasicDamage(1, new DamageDice(1, 0, MM45Item.ElementalEffect(InternalIndex).DamageValue));
                if (MM45Item.IsMaterial(InternalIndex))
                    return new BasicDamage(1, new DamageDice(1, 0, MM45Item.PrefixHDAC(InternalIndex).Damage));
                return BasicDamage.Zero;
            }
        }

        public override string AttributeString
        {
            get
            {
                if (MM45Item.IsElemental(InternalIndex))
                    return String.Format("{0} Res", ElementString.Replace("Electric", "Elec."));
                if (MM45Item.IsMaterial(InternalIndex))
                    return "To-Hit";
                if (MM45Item.IsAttribute(InternalIndex))
                    return Global.GenericAttributeString(MM45Item.AttributeEffect(InternalIndex).Attribute);
                return String.Empty;
            }
        }

        public override int EquipBonusValue 
        { 
            get 
            {
                if (MM45Item.IsElemental(InternalIndex))
                    return MM45Item.ElementalEffect(InternalIndex).ResistValue;
                if (MM45Item.IsMaterial(InternalIndex))
                    return MM45Item.PrefixHDAC(InternalIndex).Hit;
                if (MM45Item.IsAttribute(InternalIndex))
                    return MM45Item.AttributeEffect(InternalIndex).Modifier;
                return 0;
            }
        }

        public override string ValueString
        { 
            get 
            {
                decimal multiplier = MM45Item.ValueMultiplier(InternalIndex);
                if (multiplier != 1)
                    return String.Format("x{0}", multiplier);
                return Global.AddPlus(MM45Item.ValueModifier(InternalIndex), false);
            }
        }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(String.Format("Prefix: {0}\r\n", Global.Title(Name)));
                if (MM45Item.IsElemental(InternalIndex))
                {
                    sb.AppendFormat(String.Format("{0} resistance: {1}\r\n", Global.Title(ElementString), Global.AddPlus(EquipBonusValue)));
                    sb.AppendFormat(String.Format("{0} damage: {1}\r\n", Global.Title(ElementString), BaseDamage.ToString()));
                }
                if (MM45Item.IsMaterial(InternalIndex))
                {
                    sb.AppendFormat(String.Format("To-Hit: {0}\r\n", Global.AddPlus(EquipBonusValue)));
                    sb.AppendFormat(String.Format("Damage: {0}\r\n", Global.AddPlus(BaseDamage.Bonus)));
                    sb.AppendFormat(String.Format("Armor Class: {0}\r\n", Global.AddPlus(ArmorClass)));
                }
                if (MM45Item.IsAttribute(InternalIndex))
                {
                    sb.AppendFormat(String.Format("{0} bonus: {1}\r\n", Global.Title(AttributeString), Global.AddPlus(EquipBonusValue)));
                }
                sb.AppendFormat(String.Format("Value: {0}\r\n", ValueString));
                return sb.ToString();
            }
        }
    }

    public class MM45SuffixItem : MMMetaItem
    {
        public override GameNames Game { get { return GameNames.MightAndMagic45; } }
        public MM45ItemSuffixIndex InternalIndex;
        public MM45SuffixItem(MM45ItemSuffixIndex index) { InternalIndex = index; }
        public override int Index { get { return (int)InternalIndex; } set { InternalIndex = (MM45ItemSuffixIndex)value; } }
        public override string Name { get { return String.Format("of {0}", MM45Item.GetItemSuffix(ItemType.Miscellaneous, (int)Index)); } }
        public override string TypeString { get { return "Suffix"; } }
        public override string UseEffectString { get { return Global.Title(MM45SpellList.GetSpellName(MM45Item.ItemSuffixEffect(ItemType.Miscellaneous, Index))); } }
        public override string ValueString { get { return Global.AddPlus(MM45Item.SuffixValueModifier(InternalIndex), false); } }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(String.Format("Suffix: {0}\r\n", Name));
                sb.AppendFormat(String.Format("Casts spell: {0}\r\n", UseEffectString));
                sb.AppendFormat(String.Format("Value: {0}\r\n", ValueString));
                return sb.ToString();
            }
        }
    }

    public class MM45WeaponSuffixItem : MMMetaItem
    {
        public override GameNames Game { get { return GameNames.MightAndMagic45; } }
        public MM45WeaponSuffixIndex InternalIndex;
        public MM45WeaponSuffixItem(MM45WeaponSuffixIndex index) { InternalIndex = index; }
        public override int Index { get { return (int)InternalIndex; } set { InternalIndex = (MM45WeaponSuffixIndex)value; } }
        public override string Name { get { return MM45Item.WeaponSuffixString(InternalIndex); } }
        public override string TypeString { get { return "Weap Sfx"; } }
        public override string UseEffectString { get { return MM45Item.WeaponSuffixEffect(InternalIndex); } }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(String.Format("Weapon Suffix: {0}\r\n", Name));
                sb.AppendFormat(String.Format("Effect: {0}\r\n", UseEffectString));
                return sb.ToString();
            }
        }
    }

    public class MMCondition
    {
        public byte Cursed;
        public byte HeartBroken;
        public byte Weak;
        public byte Poisoned;
        public byte Diseased;
        public byte Insane;
        public byte InLove;
        public byte Drunk;
        public byte Asleep;
        public byte Depressed;
        public byte Confused;
        public byte Paralyzed;
        public byte Unconscious;
        public byte Dead;
        public byte Stone;
        public byte Eradicated;

        public static string ConditionString(ConditionIndex index)
        {
            switch (index)
            {
                case ConditionIndex.Cursed: return "Cursed";
                case ConditionIndex.HeartBroken: return "Heart Broken";
                case ConditionIndex.Weak: return "Weak";
                case ConditionIndex.Poisoned: return "Poisoned";
                case ConditionIndex.Diseased: return "Diseased";
                case ConditionIndex.Insane: return "Insane";
                case ConditionIndex.InLove: return "In Love";
                case ConditionIndex.Drunk: return "Drunk";
                case ConditionIndex.Asleep: return "Asleep";
                case ConditionIndex.Depressed: return "Depressed";
                case ConditionIndex.Confused: return "Confused";
                case ConditionIndex.Paralyzed: return "Paralyzed";
                case ConditionIndex.Unconscious: return "Unconscious";
                case ConditionIndex.Dead: return "Dead";
                case ConditionIndex.Stone: return "Stone";
                case ConditionIndex.Eradicated: return "Eradicated";
                case ConditionIndex.Mobile: return "Mobile";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public bool Good
        {
            get
            {
                return (Cursed == 0 && HeartBroken == 0 && Weak == 0 && Poisoned == 0 && Diseased == 0 && Insane == 0 &&
                    InLove == 0 && Drunk == 0 && Asleep == 0 && Depressed == 0 && Confused == 0 &&
                    Paralyzed == 0 && Unconscious == 0 && Dead == 0 && Stone == 0 && Eradicated == 0);
            }
            set
            {
                if (value)
                    Clear();
            }
        }

        public void Clear()
        {
            Cursed = 0;
            HeartBroken = 0;
            Weak = 0;
            Poisoned = 0;
            Diseased = 0;
            Insane = 0;
            InLove = 0;
            Drunk = 0;
            Asleep = 0;
            Depressed = 0;
            Confused = 0;
            Paralyzed = 0;
            Unconscious = 0;
            Dead = 0;
            Stone = 0;
            Eradicated = 0;
        }

        public bool UnableToCast
        {
            get
            {
                return (Asleep > 0 || Depressed > 0 || Confused > 0 || Paralyzed > 0 || Unconscious > 0 || Dead > 0 || Stone > 0 || Eradicated > 0);
            }
        }

        public MMCondition(byte[] bytes, int index = 0)
        {
            SetFromBytes(bytes, index);
        }

        public MMCondition()
        {
            SetZero();
        }

        public void SetZero()
        {
            Cursed = 0;
            HeartBroken = 0;
            Weak = 0;
            Poisoned = 0;
            Diseased = 0;
            Insane = 0;
            InLove = 0;
            Drunk = 0;
            Asleep = 0;
            Depressed = 0;
            Confused = 0;
            Paralyzed = 0;
            Unconscious = 0;
            Dead = 0;
            Stone = 0;
            Eradicated = 0;
        }

        public void SetFromBytes(byte[] bytes, int index)
        {
            if (bytes.Length < 16)
                return;

            Cursed = bytes[index + 0];
            HeartBroken = bytes[index + 1];
            Weak = bytes[index + 2];
            Poisoned = bytes[index + 3];
            Diseased = bytes[index + 4];
            Insane = bytes[index + 5];
            InLove = bytes[index + 6];
            Drunk = bytes[index + 7];
            Asleep = bytes[index + 8];
            Depressed = bytes[index + 9];
            Confused = bytes[index + 10];
            Paralyzed = bytes[index + 11];
            Unconscious = bytes[index + 12];
            Dead = bytes[index + 13];
            Stone = bytes[index + 14];
            Eradicated = bytes[index + 15];
        }

        public byte[] GetBytes()
        {
            byte[] bytes = new byte[16];
            bytes[0] = Cursed;
            bytes[1] = HeartBroken;
            bytes[2] = Weak;
            bytes[3] = Poisoned;
            bytes[4] = Diseased;
            bytes[5] = Insane;
            bytes[6] = InLove;
            bytes[7] = Drunk;
            bytes[8] = Asleep;
            bytes[9] = Depressed;
            bytes[10] = Confused;
            bytes[11] = Paralyzed;
            bytes[12] = Unconscious;
            bytes[13] = Dead;
            bytes[14] = Stone;
            bytes[15] = Eradicated;
            return bytes;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Cursed > 0)
                sb.AppendFormat("Cursed: {0}, ", Cursed);
            if (HeartBroken > 0)
                sb.AppendFormat("Heart Broken: {0}, ", HeartBroken);
            if (Weak > 0)
                sb.AppendFormat("Weak: {0}, ", Weak);
            if (Poisoned > 0)
                sb.AppendFormat("Poisoned: {0}, ", Poisoned);
            if (Diseased > 0)
                sb.AppendFormat("Diseased: {0}, ", Diseased);
            if (Insane > 0)
                sb.AppendFormat("Insane: {0}, ", Insane);
            if (InLove > 0)
                sb.AppendFormat("InLove: {0}, ", InLove);
            if (Drunk > 0)
                sb.AppendFormat("Drunk: {0}, ", Drunk);
            if (Asleep > 0)
                sb.AppendFormat("Asleep: {0}, ", Asleep);
            if (Depressed > 0)
                sb.AppendFormat("Depressed: {0}, ", Depressed);
            if (Confused > 0)
                sb.AppendFormat("Confused: {0}, ", Confused);
            if (Paralyzed > 0)
                sb.AppendFormat("Paralyzed: {0}, ", Paralyzed);
            if (Unconscious > 0)
                sb.AppendFormat("Unconscious: {0}, ", Unconscious);
            if (Dead > 0)
                sb.AppendFormat("Dead: {0}, ", Dead);
            if (Stone > 0)
                sb.AppendFormat("Stone: {0}, ", Stone);
            if (Eradicated > 0)
                sb.AppendFormat("Eradicated: {0}, ", Eradicated);
            return Global.Trim(sb).ToString();
        }

        public string Description
        {
            get
            {
                if (Good)
                    return "Good: Character is healthy\n";

                StringBuilder sb = new StringBuilder();
                if (Cursed > 0)
                    sb.AppendFormat("Cursed: Luck -{0}\r\n", Cursed);
                if (HeartBroken > 0)
                    sb.AppendFormat("Heart Broken: All stats -{0}\r\n", HeartBroken);
                if (Weak > 0)
                    sb.AppendFormat("Weak: All stats -{0}\r\n", Weak);
                if (Poisoned > 0)
                    sb.AppendFormat("Poisoned: Might/Speed/Accuracy -{0}\r\n", Poisoned);
                if (Diseased > 0)
                    sb.AppendFormat("Diseased: Intellect/Personality/Endurance -{0}\r\n", Diseased);
                if (Insane > 0)
                    sb.AppendFormat("Insane: Mgt/Spd +{0}, Int/Per/Acy -{0}\r\n", Insane);
                if (InLove > 0)
                    sb.AppendFormat("In Love: All stats +{0}\r\n", InLove);
                if (Drunk > 0)
                    sb.AppendFormat("Drunk: Per/Lck +{0}, Mgt/Int/End/Spd/Acy -{0}\r\n", Drunk);
                if (Asleep > 0)
                    sb.AppendLine("Asleep: Cannot perform actions until attacked");
                if (Depressed > 0)
                    sb.AppendLine("Depressed: Cannot perform actions");
                if (Confused > 0)
                    sb.AppendLine("Confused: Performs random actions");
                if (Paralyzed > 0)
                    sb.AppendLine("Paralyzed: Cannot perform actions");
                if (Unconscious > 0)
                    sb.AppendLine("Unconscious: Cannot perform actions and dies if attacked");
                if (Dead > 0)
                    sb.AppendLine("Dead: Cannot perform actions and gains no XP");
                if (Stone > 0)
                    sb.AppendLine("Stone: Cannot perform actions and gains no XP");
                if (Eradicated > 0)
                    sb.AppendLine("Eradicated: Cannot perform actions and gains no XP");

                return sb.ToString();
            }
        }

        public Modifiers GetModifiers()
        {
            Modifiers mod = new Modifiers();

            mod.Adjust(ModAttr.Luck, -Cursed, String.Format("Character is Cursed ({0})", Cursed));

            mod.Adjust(ModAttr.Intellect, -Diseased, String.Format("Character is Diseased ({0})", Diseased));
            mod.Adjust(ModAttr.Personality, -Diseased, String.Format("Character is Diseased ({0})", Diseased));
            mod.Adjust(ModAttr.Endurance, -Diseased, String.Format("Character is Diseased ({0})", Diseased));

            mod.Adjust(ModAttr.Personality, Drunk, String.Format("Character is Drunk ({0})", Drunk));
            mod.Adjust(ModAttr.Luck, Drunk, String.Format("Character is Drunk ({0})", Drunk));

            mod.Adjust(ModAttr.Might, -Drunk, String.Format("Character is Drunk ({0})", Drunk));
            mod.Adjust(ModAttr.Endurance, -Drunk, String.Format("Character is Drunk ({0})", Drunk));
            mod.Adjust(ModAttr.Intellect, -Drunk, String.Format("Character is Drunk ({0})", Drunk));
            mod.Adjust(ModAttr.Speed, -Drunk, String.Format("Character is Drunk ({0})", Drunk));
            mod.Adjust(ModAttr.Accuracy, -Drunk, String.Format("Character is Drunk ({0})", Drunk));

            mod.Adjust(ModAttr.Might, -HeartBroken, String.Format("Character is HeartBroken ({0})", HeartBroken));
            mod.Adjust(ModAttr.Intellect, -HeartBroken, String.Format("Character is HeartBroken ({0})", HeartBroken));
            mod.Adjust(ModAttr.Personality, -HeartBroken, String.Format("Character is HeartBroken ({0})", HeartBroken));
            mod.Adjust(ModAttr.Endurance, -HeartBroken, String.Format("Character is HeartBroken ({0})", HeartBroken));
            mod.Adjust(ModAttr.Speed, -HeartBroken, String.Format("Character is HeartBroken ({0})", HeartBroken));
            mod.Adjust(ModAttr.Accuracy, -HeartBroken, String.Format("Character is HeartBroken ({0})", HeartBroken));
            mod.Adjust(ModAttr.Luck, -HeartBroken, String.Format("Character is HeartBroken ({0})", HeartBroken));

            mod.Adjust(ModAttr.Might, -Weak, String.Format("Character is Weak ({0})", Weak));
            mod.Adjust(ModAttr.Intellect, -Weak, String.Format("Character is Weak ({0})", Weak));
            mod.Adjust(ModAttr.Personality, -Weak, String.Format("Character is Weak ({0})", Weak));
            mod.Adjust(ModAttr.Endurance, -Weak, String.Format("Character is Weak ({0})", Weak));
            mod.Adjust(ModAttr.Speed, -Weak, String.Format("Character is Weak ({0})", Weak));
            mod.Adjust(ModAttr.Accuracy, -Weak, String.Format("Character is Weak ({0})", Weak));
            mod.Adjust(ModAttr.Luck, -Weak, String.Format("Character is Weak ({0})", Weak));

            mod.Adjust(ModAttr.Might, InLove, String.Format("Character is In Love ({0})", InLove));
            mod.Adjust(ModAttr.Intellect, InLove, String.Format("Character is In Love ({0})", InLove));
            mod.Adjust(ModAttr.Personality, InLove, String.Format("Character is In Love ({0})", InLove));
            mod.Adjust(ModAttr.Endurance, InLove, String.Format("Character is In Love ({0})", InLove));
            mod.Adjust(ModAttr.Speed, InLove, String.Format("Character is In Love ({0})", InLove));
            mod.Adjust(ModAttr.Accuracy, InLove, String.Format("Character is In Love ({0})", InLove));
            mod.Adjust(ModAttr.Luck, InLove, String.Format("Character is In Love ({0})", InLove));

            mod.Adjust(ModAttr.Might, Insane, String.Format("Character is Insane ({0})", Insane));
            mod.Adjust(ModAttr.Speed, Insane, String.Format("Character is Insane ({0})", Insane));
            mod.Adjust(ModAttr.Intellect, -Insane, String.Format("Character is Insane ({0})", Insane));
            mod.Adjust(ModAttr.Personality, -Insane, String.Format("Character is Insane ({0})", Insane));
            mod.Adjust(ModAttr.Accuracy, -Insane, String.Format("Character is Insane ({0})", Insane));

            mod.Adjust(ModAttr.Might, -Poisoned, String.Format("Character is Poisoned ({0})", Poisoned));
            mod.Adjust(ModAttr.Speed, -Poisoned, String.Format("Character is Poisoned ({0})", Poisoned));
            mod.Adjust(ModAttr.Accuracy, -Poisoned, String.Format("Character is Poisoned ({0})", Poisoned));

            return mod;
        }
    }

    public class MMMonster : Monster
    {
        public override string ProperName { get { return Name; } }
        public override string OneLineDescription { get { return Name; } }
        public override double AverageDamage { get { return Damage * NumAttacks; } }

        public override string HPString(bool bPreEncounter)
        {
            return CurrentHP.ToString() + (bPreEncounter ? "+1d8" : "");
        }

        public static int GetTreasureStrength(int items, int gems, int gold)
        {
            // arbitrary
            return (items * 1000) + (gems * 1000) + gold;
        }

        public static string GetTreasureStringShort(int items, int gems, int gold)
        {
            StringBuilder sb = new StringBuilder();
            if (items > 0)
                sb.AppendFormat("I{0}, ", items);
            if (gems > 0)
                sb.AppendFormat("{0}G, ", gems);
            if (gold > 0)
                sb.AppendFormat("{0}g, ", gold);
            return Global.Trim(sb).ToString();
        }
    }

    public abstract class MM345MapData : MMMapData
    {
        public Dictionary<Point, List<MM345VisibleObject>> VisibleObjects;
        public Dictionary<Point, List<MM345SpecialSquare>> SpecialSquares;

        public override void CopyMetadataFrom(MapData dataCopy)
        {
 	        base.CopyMetadataFrom(dataCopy);

            if (dataCopy is MM345MapData)
            {
                VisibleObjects = ((MM345MapData)dataCopy).VisibleObjects;
                SpecialSquares = ((MM345MapData)dataCopy).SpecialSquares;
                LiveOnly = ((MM345MapData)dataCopy).LiveOnly;
            }
        }
    }

    public abstract class MM345MapCartography : MapCartography
    {
        public override bool IsVisited(int x, int y)
        {
            if (Bytes.Length < 33)
                return (Global.GetBit(Bytes, y * 16 + x) > 0);

            int iQuad = 0;
            if (y > 15)
            {
                iQuad = 2;
                y -= 16;
            }
            if (x > 15)
            {
                iQuad++;
                x -= 16;
            }
            return (Global.GetBit(Bytes, iQuad * 256 + y * 16 + x) > 0);
        }
    }

    public class MM345SpecialSquare
    {
        // N,S,E,W,All
        public UInt16 X;
        public UInt16 Y;
        public UInt16 Dir;  // 0:North 1:South 2:East 3:West 4:All
        public UInt16 SubIndex;
        public UInt16 Offset;
        public byte[] Action;
        public bool AutoExecute;

        public Point Location { get { return new Point(X, Y); } }
        public virtual DirectionFlags Facing { get { return MM345Script.FacingFromInt(Dir); } }

        public void SetData(byte[] bytes, int iOffset, byte[] bytesActions)
        {
            if (bytes.Length - iOffset < 10)
                return;

            X = bytes[iOffset];
            Y = bytes[2 + iOffset];
            //X = BitConverter.ToUInt16(bytes, 0 + iOffset);
            //Y = BitConverter.ToUInt16(bytes, 2 + iOffset);
            Dir = BitConverter.ToUInt16(bytes, 4 + iOffset);
            SubIndex = BitConverter.ToUInt16(bytes, 6 + iOffset);
            Offset = BitConverter.ToUInt16(bytes, 8 + iOffset);
            AutoExecute = false;    // Set via map, not script bytes

            if (bytesActions != null && bytesActions.Length > Offset)
            {
                byte bLen = bytesActions[Offset];
                if (bytesActions.Length > Offset + bLen)
                {
                    Action = new byte[bLen];
                    Buffer.BlockCopy(bytesActions, Offset+1, Action, 0, bLen);
                }
            }
        }
    }

    public class MM345VisibleObject
    {
        public virtual Point Location { get { return Global.NullPoint; } }
        public virtual int Image { get { return -1; } }
        public virtual UInt32 Unknown { get { return 0; } }
    }

    public class MM345EncounterInfo : EncounterInfo
    {
        protected Dictionary<int, Monster> m_monsters;
        public byte[] CharsMovedThisRound;
        public byte[] MonstersMovedThisRound;
        public MMActiveEffects ActiveEffects;
        public int MaxEncounterIndex = 0;
        public override bool MonstersOnMap { get { return true; } }

        public MM345EncounterInfo()
            : base()
        {
            m_monsters = null;
        }

        public override bool HasTreasure { get { return false; } }
        public override Dictionary<int, Monster> Monsters { get { return m_monsters; } }
        public override bool InCombat { get { return true; } }

        public void SetMonsters(Dictionary<int, Monster> monsters)
        {
            m_monsters = monsters;
            NumTotalMonsters = monsters.Count;
        }

        public override int NumLivingMonsters
        {
            get
            {
                if (Monsters == null || Monsters.Count == 0)
                    return 0;

                return Monsters.Count<KeyValuePair<int, Monster>>(n => n.Value.IsAlive);
            }
        }

        public override TurnOrderCalculator GetTurnOrder(CharCombatLabel[] labelChars, GameInfo gameInfo)
        {
            if (Party == null)
                return null;

            TurnOrderCalculator toc = new TurnOrderCalculator(0, 0);
            MM345BaseCharacter[] characters = new MM345BaseCharacter[Party.NumChars];

            int iNameMax = 0;

            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
            {
                if (Party is MM45PartyInfo)
                    characters[iIndex] = MM45Character.Create(Party.Bytes, Party.Addresses[iIndex] * Party.CharacterSize, gameInfo);
                else
                    characters[iIndex] = MM3Character.Create(Party.Bytes, Party.Addresses[iIndex] * Party.CharacterSize, gameInfo);

                labelChars[iIndex].Melee = false;   // All characters are in melee if any are; don't put pointless indicators everywhere
                labelChars[iIndex].Condition = characters[iIndex].BasicCondition;
                labelChars[iIndex].CharName = String.Format("{0})  {1}", iIndex + 1, characters[iIndex].Name);
                labelChars[iIndex].HP = characters[iIndex].QuickRefHitPoints.Current.ToString();
                labelChars[iIndex].SP = characters[iIndex].QuickRefSpellPoints.Current.ToString();

                if (CharsMovedThisRound[iIndex] == 0 || PreEncounter)
                    toc.AddPlayerCharacter(characters[iIndex].Name, characters[iIndex].QuickRefSpeed.Temporary, iIndex);

                iNameMax = Math.Max(iNameMax, labelChars[iIndex].NameLength);
            }

            for (byte iIndex = Party.NumChars; iIndex < 8; iIndex++)
                labelChars[iIndex].Clear();

            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
                labelChars[iIndex].SetHPOffset(iNameMax + 2);

            // Only include melee monsters in the turn order
            int iMeleeMonster = 0;
            int iMonster = 0;
            foreach (MMMonster monster in Monsters.Values)
            {
                iMonster++;

                if (!monster.Melee)
                    continue;

                if (iMeleeMonster >= MonstersMovedThisRound.Length)
                    break;

                // TODO: Figure out which monster is which
                if (MonstersMovedThisRound[iMeleeMonster] == 0 || PreEncounter)
                    toc.AddMonster(monster.ProperName, monster.Speed, iMonster + 6);

                iMeleeMonster++;
            }

            return toc;
        }

        public string GetExtraText(Dictionary<MMEffects, MMEffectTag> effects)
        {
            if (effects.Count == 0)
                return "Active Effects: None";

            StringBuilder sb = new StringBuilder("Active Effects: ");
            foreach (MMEffectTag effect in effects.Values)
            {
                if (effect.Enabled)
                    sb.AppendFormat("{0}, ", effect.EffectText);
            }

            if (sb.Length < 17)
                return "Active Effects: None";

            return Global.Trim(sb).ToString();
        }
    }

    public class MM345Spell : MMSpell
    {
        public int Index345;
        public int InternalIndex345;

        public void SetInfo(int index, int internalIndex, string name, SpellType type, int sp, bool perlevel,
            int gems, int level, SpellWhen when, SpellTarget target, string shortDesc, string desc, string location)
        {
            Index345 = index;
            InternalIndex345 = internalIndex;
            Name = name;
            Type = type;
            Level = level;
            Number = 0;
            Cost = new MMSpellCost(sp, perlevel, gems);
            When = when;
            Target = target;
            ShortDescription = shortDesc;
            Description = desc;
            Learned = location;
        }

        public override bool UsesLevelOnly { get { return true; } }

        public Keys[] KeysForKnownSpell(int iTotalKnown, int iSelected)
        {
            int iPageDown = iSelected / 10;
            iSelected = iSelected % 10;
            if (iPageDown > 0 && iPageDown >= iTotalKnown / 10)
                iSelected += (10 - (iTotalKnown % 10));

            List<Keys> keys = new List<Keys>(iPageDown + 4);
            keys.Add(Keys.N);       // New spell (i.e. not the quickspell)
            // Page-up a few times in case something else is already selected; doesn't do anything if the list is already at the top
            keys.Add(Keys.PageUp);
            keys.Add(Keys.PageUp);
            keys.Add(Keys.PageUp);
            for (int i = 0; i < iPageDown; i++)
                keys.Add(Keys.PageDown);
            if (iSelected == 9)
                keys.Add(Keys.D0);  // Tenth spell in the list is "0"
            else
                keys.Add(Keys.D0 + iSelected + 1);  // Otherwise "1" through "9"
            keys.Add(Keys.Enter);   // Select spell
            keys.Add(Keys.C);       // Cast spell
            return keys.ToArray();
        }
    }

    public class MM345Monster : MMMonster
    {
        public int DamageNumDice;
        public int DamageDieMax;
        public DamageType DamageType;
        public Resistances Resistances;
        public MM345MonsterCondition MM345Condition;
        public override BasicConditionFlags Condition { get { return GetCondition(MM345Condition); } }

        public override bool IsAlive { get { return Position.X < 32 && Position.Y < 32; } }
        public virtual byte[] GetBytes() { return null; }
        public override double AverageHP { get { return HP; } }

        public static BasicConditionFlags GetCondition(MM345MonsterCondition cond)
        {
            // Might and Magic 3/4/5 monsters can only have one condition at a time
            switch (cond)
            {
                case MM345MonsterCondition.Asleep: return BasicConditionFlags.Asleep;
                case MM345MonsterCondition.Immobilized: return BasicConditionFlags.Paralyzed;
                case MM345MonsterCondition.Hypnotized: return BasicConditionFlags.Hypnotized;
                case MM345MonsterCondition.Silenced: return BasicConditionFlags.Silenced;
                case MM345MonsterCondition.FeebleMind: return BasicConditionFlags.Mindless;
                default: return BasicConditionFlags.Good;
            }
        }

        public static string ConditionString(MM345MonsterCondition condition)
        {
            switch (condition)
            {
                case MM345MonsterCondition.Asleep: return "Asleep";
                case MM345MonsterCondition.Immobilized: return "Immobilized";
                case MM345MonsterCondition.Silenced: return "Silenced";
                case MM345MonsterCondition.Good: return "Good";
                case MM345MonsterCondition.Hypnotized: return "Hypnotized";
                case MM345MonsterCondition.FeebleMind: return "Mindless";
                default: return String.Format("Unknown ({0})", (int)condition);
            }
        }
    }

    public class MM345String : MMString
    {
        public override void SetFromBytes(byte[] bytes, ref int iNext)
        {
            if (bytes == null)
                return;

            // A string header is either 02,xx,xx,xx or 03,xx,xx,xx,xx,xx
            // The xx values have something to do with font and alignment
            StringBuilder sb = new StringBuilder();

            int iStart = iNext;

            if (iStart >= bytes.Length)
                return;

            while (iNext < bytes.Length - 1 && bytes[iNext] != 0)
            {
                switch (bytes[iNext])
                {
                    case 0x01:
                        iNext += 1;
                        break;
                    case 0x02:
                        iNext += 1;
                        break;
                    case 0x03:
                        for (int i = 0; i < 2; i++)
                            if (iNext >= bytes.Length || bytes[++iNext] == 0)
                                break;
                        break;
                    case 0x0c:
                        if (bytes[iNext + 1] == 0x64)
                        {
                            iNext += 2;
                            break;
                        }
                        for (int i = 0; i < 3; i++)
                            if (iNext >= bytes.Length || bytes[++iNext] == 0)
                                break;
                        break;
                    case 0x0b:
                        for (int i = 0; i < 4; i++)
                            if (iNext >= bytes.Length || bytes[++iNext] == 0)
                                break;
                        break;
                    case 0x0a:
                        iNext++;
                        sb.Append(" ");
                        break;
                    case 0x09:
                        for (int i = 0; i < 4; i++)
                            if (iNext >= bytes.Length || bytes[++iNext] == 0)
                                break;
                        break;
                    default:
                        sb.Append((char)bytes[iNext]);
                        iNext++;
                        break;
                }
            }

            iNext++;
            RawBytes = new byte[iNext - iStart];
            Buffer.BlockCopy(bytes, iStart, RawBytes, 0, iNext - iStart);
            Basic = sb.ToString();
        }
    }

    public abstract class MM345GameInfo : GameInfo
    {
        public MMActiveEffects ActiveEffects;
        public byte[] RawMap;
        public byte[] RawParty;

        public MM345GameInfo()
        {
        }

        public static string DayString(int iDay)
        {
            switch (iDay % 10)
            {
                case 0: return "Tensday";
                case 1: return "Onesday";
                case 2: return "Twosday";
                case 3: return "Threesday";
                case 4: return "Foursday";
                case 5: return "Fivesday";
                case 6: return "Sixsday";
                case 7: return "Sevensday";
                case 8: return "Eightsday";
                case 9: return "Ninesday";
                default: return "UnknownDay";
            }
        }

        public virtual string GameTime(bool bFull) { return "N/A"; }

        public static string GetGameTimeString(int iYear, int iDay, int iMinutes, bool bFull)
        {
            int iHours = iMinutes / 60;
            iMinutes = iMinutes % 60;

            bool bPM = false;
            if (iHours >= 12)
            {
                iHours -= 12;
                bPM = true;
            }

            if (iHours == 0)
                iHours = 12;   // For display; 0:00 -> 12:00

            if (bFull)
                return String.Format("{0}:{1:D2} {2}, {3}, day {4} of year {5}", iHours, iMinutes, bPM ? "PM" : "AM", DayString(iDay), iDay, iYear);
            else
                return String.Format("{0}:{1:D2} {2}", iHours, iMinutes, bPM ? "PM" : "AM");
        }
    }

    public class MM345MapAttributes : MapAttributes
    {
        List<MM3InternalSpellIndex> Spells;
        public int LockStrength;
        public bool SavePermitted;

        public MM345MapAttributes(byte[] bytes)
        {
            Bytes = bytes;
            Spells = new List<MM3InternalSpellIndex>(7);
            if (bytes[13] == 0)
                Spells.Add(MM3InternalSpellIndex.Teleport);
            if (bytes[14] == 0)
                Spells.Add(MM3InternalSpellIndex.LloydsBeacon);
            if (bytes[15] == 0)
                Spells.Add(MM3InternalSpellIndex.TimeDistortion);
            if (bytes[16] == 0)
                Spells.Add(MM3InternalSpellIndex.SuperShelter);
            if (bytes[17] == 0)
                Spells.Add(MM3InternalSpellIndex.TownPortal);
            if (bytes[18] == 0)
                Spells.Add(MM3InternalSpellIndex.NaturesGate);
            if (bytes[19] == 0)
                Spells.Add(MM3InternalSpellIndex.Etherealize);

            Flags = MapAttributeFlags.None;
            if (!Spells.Contains(MM3InternalSpellIndex.Teleport))
                Flags |= MapAttributeFlags.AllowTeleport;
            if (!Spells.Contains(MM3InternalSpellIndex.LloydsBeacon))
                Flags |= MapAttributeFlags.AllowLloydsBeacon;
            if (!Spells.Contains(MM3InternalSpellIndex.TimeDistortion))
                Flags |= MapAttributeFlags.AllowTimeDistortion;
            if (!Spells.Contains(MM3InternalSpellIndex.SuperShelter))
                Flags |= MapAttributeFlags.AllowSuperShelter;
            if (!Spells.Contains(MM3InternalSpellIndex.TownPortal))
                Flags |= MapAttributeFlags.AllowTownPortal;
            if (!Spells.Contains(MM3InternalSpellIndex.NaturesGate))
                Flags |= MapAttributeFlags.AllowNaturesGate;
            if (!Spells.Contains(MM3InternalSpellIndex.Etherealize))
                Flags |= MapAttributeFlags.AllowEtherealize;

            if (bytes[6] == 0)
                Flags |= MapAttributeFlags.Darkness;

            LockStrength = bytes[10];
            SavePermitted = (bytes[5] != 0);
        }

        public void SetFlags(MapAttributeFlags flags)
        {
            Flags = flags;

            Spells = new List<MM3InternalSpellIndex>(7);
            if (!flags.HasFlag(MapAttributeFlags.AllowTeleport))
                Spells.Add(MM3InternalSpellIndex.Teleport);
            if (!flags.HasFlag(MapAttributeFlags.AllowLloydsBeacon))
                Spells.Add(MM3InternalSpellIndex.LloydsBeacon);
            if (!flags.HasFlag(MapAttributeFlags.AllowTimeDistortion))
                Spells.Add(MM3InternalSpellIndex.TimeDistortion);
            if (!flags.HasFlag(MapAttributeFlags.AllowSuperShelter))
                Spells.Add(MM3InternalSpellIndex.SuperShelter);
            if (!flags.HasFlag(MapAttributeFlags.AllowTownPortal))
                Spells.Add(MM3InternalSpellIndex.TownPortal);
            if (!flags.HasFlag(MapAttributeFlags.AllowNaturesGate))
                Spells.Add(MM3InternalSpellIndex.NaturesGate);
            if (!flags.HasFlag(MapAttributeFlags.AllowEtherealize))
                Spells.Add(MM3InternalSpellIndex.Etherealize);
        }

        public byte[] GetBytes()
        {
            Bytes[5] = (byte)(SavePermitted ? 1 : 0);
            Bytes[6] = (byte)(Flags.HasFlag(MapAttributeFlags.Darkness) ? 0 : 1);
            Bytes[10] = (byte)LockStrength;

            Buffer.BlockCopy(new byte[] {
                (byte) (Spells.Contains(MM3InternalSpellIndex.Teleport) ? 0 : 1),
                (byte) (Spells.Contains(MM3InternalSpellIndex.LloydsBeacon) ? 0 : 1),
                (byte) (Spells.Contains(MM3InternalSpellIndex.TimeDistortion) ? 0 : 1),
                (byte) (Spells.Contains(MM3InternalSpellIndex.SuperShelter) ? 0 : 1),
                (byte) (Spells.Contains(MM3InternalSpellIndex.TownPortal) ? 0 : 1),
                (byte) (Spells.Contains(MM3InternalSpellIndex.NaturesGate) ? 0 : 1),
                (byte) (Spells.Contains(MM3InternalSpellIndex.Etherealize) ? 0 : 1)
                }, 0, Bytes, 13, 7);

            return Bytes;
        }

        public string ForbiddenSpellsString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (MM3InternalSpellIndex index in Spells)
                sb.AppendFormat("{0}, ", MM3SpellList.GetSpellName(index));
            if (Global.Trim(sb).Length == 0)
                sb.Append("None");
            return sb.ToString();
        }
    }

    public abstract class MM345MemoryHacker : MMMemoryHacker
    {
        public virtual bool SetPartyGold(UInt32 gold) { return false; }
        public virtual bool SetPartyFood(UInt32 food) { return false; }
        public virtual bool SetPartyGems(UInt32 gems) { return false; }
        public virtual bool SetBankGold(UInt32 gold) { return false; }
        public virtual bool SetBankGems(UInt32 gems) { return false; }
        public virtual bool SetLockStrength(byte strength) { return false; }
        public virtual bool SetYear(UInt16 year) { return false; }
        public virtual bool SetDay(Byte day) { return false; }
        public virtual bool SetTime(UInt16 minutes) { return false; }
        public virtual bool SetSavePermitted(bool bPermitted) { return false; }
        public virtual bool SetTime(UInt16 year, byte day, UInt16 minutes) { return false; }
        public virtual MemoryBytes GetPartyStaticBits() { return null; }
        public virtual bool SetPartyStaticBits(byte[] bytes) { return false; }

        public override bool HasRoamingMonsters { get { return true; } }
        public override bool NeedsMovementDelay { get { return true; } }
        public override bool QuestsNeedFiles { get { return true; } }
        public override bool UsesTrainingAssistant { get { return false; } }

        public override bool SetReadySpell(SpellHotkey hk)
        {
            if (!Properties.Settings.Default.EnableMemoryWrite)
                return false;

            switch (hk.Character)
            {
                case SpellHotkey.HKCharacter.None:
                    break;
                case SpellHotkey.HKCharacter.Character1:
                    return SetReadySpell(0, hk.SpellIndex);
                case SpellHotkey.HKCharacter.Character2:
                    return SetReadySpell(1, hk.SpellIndex);
                case SpellHotkey.HKCharacter.Character3:
                    return SetReadySpell(2, hk.SpellIndex);
                case SpellHotkey.HKCharacter.Character4:
                    return SetReadySpell(3, hk.SpellIndex);
                case SpellHotkey.HKCharacter.Character5:
                    return SetReadySpell(4, hk.SpellIndex);
                case SpellHotkey.HKCharacter.Character6:
                    return SetReadySpell(5, hk.SpellIndex);
                case SpellHotkey.HKCharacter.Character7:
                    return SetReadySpell(6, hk.SpellIndex);
                case SpellHotkey.HKCharacter.Character8:
                    return SetReadySpell(7, hk.SpellIndex);
                case SpellHotkey.HKCharacter.AllArcane:
                    return SetReadySpells(hk.SpellIndex, SpellType.Sorcerer);
                case SpellHotkey.HKCharacter.AllCleric:
                    return SetReadySpells(hk.SpellIndex, SpellType.Cleric);
                case SpellHotkey.HKCharacter.AllDruid:
                    return SetReadySpells(hk.SpellIndex, SpellType.Druid);
                case SpellHotkey.HKCharacter.AllCharacters:
                    return SetReadySpells(hk.SpellIndex, SpellType.Unknown);
            }
            return false;
        }

        public override int[] StatMinimums(GenericClass charClass)
        {
            switch (charClass)
            {
                case GenericClass.Knight: return new int[]    { 0,  15, 0,  0,  0,  0,  0 };
                case GenericClass.Paladin: return new int[]   { 13, 13, 13, 0,  0,  0,  0 };
                case GenericClass.Archer: return new int[]    { 13, 0,  0,  0,  0,  12, 0 };
                case GenericClass.Sorcerer: return new int[]  { 13, 0,  0,  0,  0,  0,  0 };
                case GenericClass.Cleric: return new int[]    { 0,  0,  13, 0,  0,  0,  0 };
                case GenericClass.Robber: return new int[]    { 0,  0,  0,  0,  0,  0,  13 };
                case GenericClass.Ninja: return new int[]     { 0,  0,  0,  0,  13, 13, 0 };
                case GenericClass.Barbarian: return new int[] { 0,  0,  0,  15, 0,  0,  0 };
                case GenericClass.Druid: return new int[]     { 15, 0,  15, 0,  0,  0,  0 };
                case GenericClass.Ranger: return new int[]    { 12, 0,  12, 12, 12, 0,  0 };
                default: return null;
            }
        }

        public override int[] CreationStatWeights { get { return new int[22] { 0, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 3, 6, 10, 16, 22, 30 }; } }
        public override int DieMax { get { return 7; } }

        public override StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            return MM345BaseCharacter.GetStatModifier(value, stat);
        }
    }

    public class MM345CharCreationInfo : CharCreationInfo
    {
        public bool SwapMightIntellect = false;

        public override bool ValidValues
        {
            get
            {
                foreach (byte b in AttributesModified)
                    if (b < 3 || b > 21)
                        return false;

                return true;
            }
        }
    }

    public class MMScriptInfo : ScriptInfo
    {
        public List<int> Monsters;
        public Dictionary<Point, Flip> WorldChanges;
        public int MonsterListIndex;

        public enum Flip
        {
            None,
            ToCloudside,
            ToDarkside,
        }

        public MMScriptInfo(GameScripts scripts, List<ScriptString> strings, List<int> monsters, MapTitleInfo map, int monsterList = 0)
        {
            Scripts = scripts;
            Strings = strings;
            Monsters = monsters;
            Map = map;
            WorldChanges = new Dictionary<Point,Flip>();
            MonsterListIndex = monsterList;
        }

        public int QuestBitOffset { get { return Map.Map > 255 ? 30 : 0; } }
    }

    public abstract class MMBackpackBytes
    {
        public abstract bool Add(Item item);

        public virtual bool Add(List<Item> items)
        {
            bool bAllSucceeded = true;
            foreach (Item item in items)
            {
                if (!Add(item))
                    bAllSucceeded = false;
            }
            return bAllSucceeded;
        }

        public virtual byte[] GetBytes() { return null; }
    }

    public class MMMemoryHacker : MemoryHacker
    {
        public override ScriptInfo GetScriptInfo()
        {
            return new MMScriptInfo(
                GetScripts(),
                GetScriptStrings(),
                GetMonsterIndices(),
                GetMapTitle(GetCurrentMapIndex()),
                GetMonsterListIndex());
        }

        public override ScriptInfo GetScriptInfo(MemoryBytes scriptBytes)
        {
            return new MMScriptInfo(
                GetScripts(scriptBytes),
                GetScriptStrings(scriptBytes),
                GetMonsterIndices(),
                GetMapTitle(GetCurrentMapIndex()),
                GetMonsterListIndex());
        }

    }
}
