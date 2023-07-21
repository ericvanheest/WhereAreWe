using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WhereAreWe
{
    public enum WizRace
    {
        None = 0,
        Human = 1,
        Elf = 2,
        Dwarf = 3,
        Gnome = 4,
        Hobbit = 5,
        Last
    }

    public enum WizClass
    {
        None = -1,
        Fighter = 0,
        Mage = 1,
        Priest = 2,
        Thief = 3,
        Bishop = 4,
        Samurai = 5,
        Lord = 6,
        Ninja = 7,
        Last
    }

    public enum WizCondition
    {
        Good = 0,
        Afraid = 1,
        Asleep = 2,
        Paralyzed = 3,
        Petrified = 4,
        Dead = 5,
        Ashes = 6,
        Lost = 7
    }

    public class WizInventory : Inventory
    {
        protected List<Item> m_items;

        public override List<Item> Items { get { return m_items; } set { m_items = value; } }

        public override int NumBackpackItems { get { return Items.Count(i => !i.IsEquipped); } }

        public WizInventory(GameNames game, byte[] bytes, int offset = 0)
        {
            // A Wizardry 1 inventory is an Int16 followed by up to eight 8-byte items
            int iNumItems = BitConverter.ToInt16(bytes, offset);

            m_items = new List<Item>(iNumItems);
            for (int i = 0; i < iNumItems; i++)
            {
                if (i * 8 + 2 > bytes.Length - 8 - offset)
                    break;  // Not enough bytes for the item count

                WizItem item = WizItem.FromInventoryBytes(game, bytes, offset + 2 + (i * 8));
                if (item != null)
                {
                    item.MemoryIndex = i;
                    item.DisplayIndex = String.Format("{0}", i + 1);
                    m_items.Add(item);
                }
            }
        }

        public WizInventory(List<Item> items)
        {
            m_items = items;
        }

        public WizInventory()
        {
            m_items = new List<Item>();
        }

        public bool HasItem(GameNames game, int index, bool bEquippedOnly = false)
        {
            Item itemFound = bEquippedOnly ? Items.FirstOrDefault(i => i.Index == index && i.IsEquipped) : Items.FirstOrDefault(i => i.Index == index);
            switch (game)
            {
                case GameNames.Wizardry1: return itemFound is Wiz1Item;
                case GameNames.Wizardry2: return itemFound is Wiz2Item;
                case GameNames.Wizardry3: return itemFound is Wiz3Item;
                case GameNames.Wizardry4: return itemFound is Wiz4Item;
                case GameNames.Wizardry5: return itemFound is Wiz5Item;
                default: return false;
            }
        }

        public bool HasItem(Wiz1ItemIndex itemWanted) { return HasItem(GameNames.Wizardry1, (int)itemWanted); }
        public bool HasItem(Wiz2ItemIndex itemWanted) { return HasItem(GameNames.Wizardry2, (int)itemWanted); }
        public bool HasItem(Wiz3ItemIndex itemWanted) { return HasItem(GameNames.Wizardry3, (int)itemWanted); }
        public bool HasItem(Wiz4ItemIndex itemWanted) { return HasItem(GameNames.Wizardry4, (int)itemWanted); }
        public bool HasItem(Wiz5ItemIndex itemWanted) { return HasItem(GameNames.Wizardry5, (int)itemWanted); }

        public virtual byte[] GetBytes()
        {
            byte[] bytes = new byte[66];

            for (int i = 0; i < 66; i++)
                bytes[i] = 0;

            Global.SetInt16(bytes, 0, m_items.Count);

            int iIndexItem = 0;
            foreach (WizItem item in m_items)
            {
                bytes[2 + (iIndexItem * 8)] = (byte)(item.IsEquipped ? 1 : 0);
                bytes[2 + (iIndexItem * 8) + 2] = (byte)(item.InvCursed ? 1 : 0);
                bytes[2 + (iIndexItem * 8) + 4] = (byte)(item.Identified ? 1 : 0);
                byte[] bytesInt = BitConverter.GetBytes((Int16)item.DiskIndex);
                Buffer.BlockCopy(bytesInt, 0, bytes, 2 + (iIndexItem * 8) + 6, bytesInt.Length);
                iIndexItem++;
            }

            return bytes;
        }

        public override BasicDamage MeleeWeaponDamage
        {
            get 
            {
                foreach (WizItem item in SelectEquippedItems)
                {
                    if (item.IsWeapon)
                        return item.Damage;
                }
                return new BasicDamage(1, DamageDice.Zero);
            }
        }
    }

    public class Wiz1234CharacterOffsets : CharacterOffsets
    {
        public override int NameLength { get { return 0; } }
        public override int Name { get { return 1; } }
        public override int PasswordLength { get { return 16; } }
        public override int Password { get { return 17; } }
        public override int Out { get { return 32; } }
        public override int Race { get { return 34; } }
        public override int Class { get { return 36; } }
        public override int Age { get { return 38; } }
        public override int Condition { get { return 40; } }
        public override int Alignment { get { return 42; } }
        public override int ConditionLength { get { return 2; } }
        public override int Stats { get { return 44; } }
        public override int SavingThrows { get { return 48; } }
        public override int Gold { get { return 52; } }
        public override int GoldLength { get { return 6; } }
        public override int Inventory { get { return 58; } }
        public override int InventoryLength { get { return 66; } }
        public override int Experience { get { return 124; } }
        public override int ExperienceLength { get { return 6; } }
        public override int LevelMod { get { return 130; } }
        public override int Level { get { return 132; } }
        public override int CurrentHP { get { return 134; } }
        public override int MaxHP { get { return 136; } }
        public override int CurrentSP { get { return 146; } }
        public override int LastArmorClass { get { return 174; } }
        public override int ArmorClass { get { return 176; } }
        public override int Regeneration { get { return 178; } }
        public override int Critical { get { return 180; } }
        public override int Swings { get { return 182; } }
        public override int MeleeDamage { get { return 184; } }
        public override int WeaponEffects { get { return 190; } }
        public override int LocationX { get { return 200; } }
        public override int LocationY { get { return 202; } }
        public override int LocationZ { get { return 204; } }
        public override int Awards { get { return 206; } }
        public override int AwardsLength { get { return 2; } }
        public override int Spells { get { return 138; } }
        public override int SpellsLength { get { return 8; } }
    }

    public class WizCharacter : WizardryBaseCharacter
    {
        public string CharName;
        public string Password;
        public int Silenced;
        public bool Out;
        public WizAlignment Alignment;
        public WizRace Race;
        public WizClass Class;
        public int Strength;
        public int IQ;
        public int Piety;
        public int Vitality;
        public int Agility;
        public int Luck;
        public int Level;
        public int LevelMod;
        public int Age;
        public long Experience;
        public WizSpellPoints SpellPoints;
        public MMHitPoints HitPoints;
        public long Gold;
        public int ArmorClass;
        public int ACBonus;
        public int LastArmorClass;
        public WizCondition Condition;
        public WizInventory Inventory;
        public WizKnownSpells SpellBook;
        public int Critical;
        public BasicDamage MeleeDamage;
        public byte[] WeaponEffects;  // 10 bytes
        public bool[,] ProtectionAgainst;
        public bool[,] Resistances;
        public bool[] Purposed;
        public int LocationX; // Also used for poison counters
        public int LocationY;
        public int LocationZ;
        public int Honors;
        public int RIP;
        public long Marks;

        protected GameNames m_game = GameNames.Wizardry1;

        public int Address = -1;
        public const int SizeInBytes = 208;

        public override int BasicSP { get { return SpellPoints.Mage.Sum() + SpellPoints.Priest.Sum(); } }
        public override int BasicMaxSP { get { return BasicSP; } }
        public override int BasicHP { get { return HitPoints.Current; } }
        public override int BasicMaxHP { get { return HitPoints.TemporaryMaximum; } }
        public override long BasicMoney { get { return Gold; } }
        public override int BasicDeaths { get { return RIP; } }
        public override int BasicDisarm { get { return Disarm; } }
        public override int BasicRegen { get { return Regeneration; } }
        public override int BasicPoison { get { return PoisonCounter; } }
        public override int BasicIDTrap { get { return IDTraps; } }
        public override int BasicIDItem { get { return IDItem; } }
        public override int BasicDispel { get { return Dispel; } }
        public override int BasicCritical { get { return CriticalChance; } }

        public WizCharacter()
        {
            Address = -1;
        }

        public int Dispel
        {
            get
            {
                switch (Class)
                {
                    case WizClass.Priest: return Math.Min(999, Level * 5 + 50);
                    case WizClass.Bishop: return Level < 4 ? 0 : Math.Min(999, Level * 5 + 30);
                    case WizClass.Lord: return Level < 9 ? 0 : Math.Min(999, Level * 5 + 10);
                    default: return 0;
                }
            }
        }

        public int ThiefMod { get { return Class == WizClass.Thief ? 6 : Class == WizClass.Ninja ? 4 : 1; } }
        public int IDTraps { get { return Math.Min(95, ThiefMod * Agility); } }
        public int IDItem { get { return Class != WizClass.Bishop ? 0 : Math.Min(100, 3 * Level + 43); } }
        public int CriticalChance { get { return Critical == 0 ? 0 : Math.Min(50, 2 * Level); } }

        public int Disarm
        {
            get
            {
                int iMod = Class == WizClass.Thief ? 6 : Class == WizClass.Ninja ? 4 : 1;
                return Math.Min(100, ((iMod == 1 ? 0 : 50) + Level) * 100 / 70);
            }
        }

        public override GameNames Game { get { return m_game; } }
        public virtual string PoisonString { get { return LocationX == 0 ? "None" : string.Format("{0}", LocationX); } }

        public string GetExtraConditionDesc()
        {
            StringBuilder sb = new StringBuilder();
            if (BackpackFull)
                sb.AppendFormat("Backpack Full: Monsters may not drop items\r\n");
            if (PoisonCounter != 0)
                sb.AppendFormat("Poisoned: 25% chance per step of losing 10% HP\r\n");
            if (Silenced != 0)
                sb.AppendFormat("Silenced: Character cannot cast spells for {0}\r\n", Global.Plural(Silenced, "round"));
            return sb.ToString().Trim();
        }

        public string GetExtraConditions()
        {
            StringBuilder sb = new StringBuilder();
            if (BackpackFull)
                sb.AppendFormat("Backpack Full, ");
            if (PoisonCounter != 0)
                sb.AppendFormat("Poisoned, ");
            if (Silenced > 0 && Silenced < 100)
                sb.AppendFormat("Silenced ({0}), ", Silenced);
            return Global.Trim(sb).ToString();
        }

        public override CharacterOffsets Offsets { get { return Wiz123.Offsets; } }
        public override int BasicAddress { get { return Address; } }

        public override int CharacterSize { get { return SizeInBytes; } }
        public override Inventory BasicInventory { get { return Inventory as Inventory; } }

        public static WizCharacter Create(GameNames game, int iCharIndex, byte[] bytes, int iIndex, WizEncounterInfo encounterInfo, bool bFromRosterFile = false)
        {

            WizCharacter wizChar = (game == GameNames.Wizardry5 ? new Wiz5Character() : new WizCharacter());
            wizChar.SetFromBytes(game, iCharIndex, bytes, iIndex, encounterInfo, bFromRosterFile);
            return wizChar;
        }

        public void SetFromBytes(GameNames game, int iCharIndex, byte[] bytes, int iIndex, WizEncounterInfo encounterInfo, bool bFromRosterFile = false)
        {
            m_game = game;
            Address = -1;
            if (bytes == null || bytes.Length < iIndex + SizeInBytes - 1)
                return;
            SetCharFromStream(iCharIndex, new MemoryStream(bytes, iIndex, bytes.Length - iIndex), null, encounterInfo, bFromRosterFile);
        }

        public override void Serialize(Stream stream)
        {
            byte[] bytes = new byte[CharacterSize];

            bytes[Offsets.NameLength] = (byte)CharName.Length;
            for (int i = Offsets.Name; i <= 15; i++)
                bytes[i] = 0x00;
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(CharName), 0, bytes, Offsets.Name, CharName.Length);
            bytes[Offsets.PasswordLength] = (byte)Password.Length;
            for (int i = Offsets.Password; i <= 15; i++)
                bytes[i] = 0x00;
            Buffer.BlockCopy(Encoding.ASCII.GetBytes(Password), 0, bytes, Offsets.Password, CharName.Length);
            Global.SetInt16(bytes, Offsets.Alignment, (int)Alignment);
            Global.SetInt16(bytes, Offsets.Race, (int)Race);
            Global.SetInt16(bytes, Offsets.Class, (int)Class);
            Global.SetInt16(bytes, Offsets.Age, Age);
            Global.SetInt16(bytes, Offsets.LevelMod, LevelMod);
            Global.SetInt16(bytes, Offsets.Level, Level);
            Global.SetInt16(bytes, Offsets.CurrentHP, HitPoints.Current);
            Global.SetInt16(bytes, Offsets.MaxHP, HitPoints.Maximum);
            Global.SetInt16(bytes, Offsets.LastArmorClass, LastArmorClass);
            Global.SetInt16(bytes, Offsets.ArmorClass, ArmorClass);

            Global.SetInt16(bytes, Offsets.Regeneration, Regeneration);
            Global.SetInt16(bytes, Offsets.Critical, Critical);
            Global.SetInt16(bytes, Offsets.Swings, MeleeDamage.NumAttacks);
            Global.SetInt16(bytes, Offsets.MeleeDamage, MeleeDamage.Quantity);
            Global.SetInt16(bytes, Offsets.MeleeDamage + 2, MeleeDamage.Faces);
            Global.SetInt16(bytes, Offsets.MeleeDamage + 4, MeleeDamage.Bonus);
            Global.SetInt16(bytes, Offsets.LocationX, LocationX);
            Global.SetInt16(bytes, Offsets.LocationY, LocationY);
            Global.SetInt16(bytes, Offsets.LocationZ, LocationZ);
            Global.SetInt16(bytes, Offsets.Awards, Honors);

            Global.SetInt16(bytes, Offsets.Out, Out ? 1 : 0);
            Global.SetInt16(bytes, Offsets.Condition, (int)Condition);
            PackedFiveBitValues bytesP5 = new PackedFiveBitValues(Strength, IQ, Piety, Vitality, Agility, Luck);
            byte[] bytesTemp = bytesP5.Bytes;
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.Stats, bytesTemp.Length);

            bytesP5 = new PackedFiveBitValues(SaveVsPoison, SaveVsPetrify, SaveVsWand, SaveVsBreath, SaveVsSpell);
            bytesTemp = bytesP5.Bytes;
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.SavingThrows, bytesTemp.Length);

            bytesTemp = WizardryLong.GetBytes(Gold);
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.Gold, bytesTemp.Length);
            bytesTemp = WizardryLong.GetBytes(Experience);
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.Experience, bytesTemp.Length);
            bytesTemp = Inventory.GetBytes();
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.Inventory, bytesTemp.Length);
            bytesTemp = SpellBook.GetBytes();
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.Spells, bytesTemp.Length);
            bytesTemp = SpellPoints.GetBytes();
            Buffer.BlockCopy(bytesTemp, 0, bytes, Offsets.CurrentSP, bytesTemp.Length);

            Buffer.BlockCopy(WeaponEffects, 0, bytes, Offsets.WeaponEffects, WeaponEffects.Length);

            stream.Write(bytes, 0, CharacterSize);
        }

        public override void SetCharFromStream(int iCharIndex, Stream stream, GameInfo info, EncounterInfo encounterInfo = null, bool bFromRosterFile = false)
        {
            if (stream.Length < CharacterSize)
                return;

            if (info != null)
                m_game = info.Game;

            RawBytes = new byte[CharacterSize];
            stream.Read(RawBytes, 0, CharacterSize);

            WizEncounterInfo wizEncounters = encounterInfo as WizEncounterInfo;
            WizEncounterRecord encRecord = null;

            int iCombatIndex = (Game == GameNames.Wizardry4 ? iCharIndex - 1 : iCharIndex);

            if (wizEncounters != null && wizEncounters.Characters != null && wizEncounters.Characters.Records.Count > iCombatIndex && iCombatIndex >= 0)
                encRecord = wizEncounters.Characters.Records[iCombatIndex];

            if (encRecord == null || !wizEncounters.InCombat)
                Silenced = 0;
            else
                Silenced = wizEncounters.Characters.Records[iCombatIndex].Silenced;

            CharName = Global.PascalString(RawBytes, Offsets.NameLength, 15);
            Password = Global.PascalString(RawBytes, Offsets.PasswordLength, 15);
            Alignment = (WizAlignment)BitConverter.ToInt16(RawBytes, Offsets.Alignment);
            Race = (WizRace)BitConverter.ToInt16(RawBytes, Offsets.Race);
            Class = (WizClass)BitConverter.ToInt16(RawBytes, Offsets.Class);
            Condition = (WizCondition)BitConverter.ToInt16(RawBytes, Offsets.Condition);
            Age = BitConverter.ToInt16(RawBytes, Offsets.Age);
            LevelMod = BitConverter.ToInt16(RawBytes, Offsets.LevelMod);
            Level = BitConverter.ToInt16(RawBytes, Offsets.Level);
            HitPoints = new MMHitPoints(BitConverter.ToInt16(RawBytes, Offsets.CurrentHP), BitConverter.ToInt16(RawBytes, Offsets.MaxHP));
            LastArmorClass = BitConverter.ToInt16(RawBytes, Offsets.LastArmorClass);
            ArmorClass = BitConverter.ToInt16(RawBytes, Offsets.ArmorClass);
            ACBonus = info is Wiz1234GameInfo ? ((Wiz1234GameInfo)info).ACBonus : 0;
            if (encRecord != null)
                ACBonus -= encRecord.ArmorClass;
            Out = BitConverter.ToInt16(RawBytes, Offsets.Out) == 1;
            PackedFiveBitValues stats = new PackedFiveBitValues(RawBytes, Offsets.Stats);
            Strength = stats.Values[0];
            IQ = stats.Values[1];
            Piety = stats.Values[2];
            Vitality = stats.Values[3];
            Agility = stats.Values[4];
            Luck = stats.Values[5];
            PackedFiveBitValues saving = new PackedFiveBitValues(RawBytes, Offsets.SavingThrows);
            SaveVsPoison = saving.Values[0];
            SaveVsPetrify = saving.Values[1];
            SaveVsWand = saving.Values[2];
            SaveVsBreath = saving.Values[3];
            SaveVsSpell = saving.Values[4];

            int swings, quantity, faces, bonus;

            Gold = new WizardryLong(RawBytes, Offsets.Gold).Number;
            Experience = new WizardryLong(RawBytes, Offsets.Experience).Number;
            if (Game == GameNames.Wizardry5)
            {
                Inventory = new Wiz5Inventory(Game, RawBytes, Offsets.Inventory);
                SpellBook = new Wiz5KnownSpells(RawBytes, Offsets.Spells);
                SpellPoints = new Wiz5SpellPoints(RawBytes, Offsets.CurrentSP);
                Regeneration = InternalModifiers.Regen;
                Critical = (Class == WizClass.Ninja ? Math.Min(50, Level * 2) : InternalModifiers.Critical);
                MeleeDamage = Inventory.MeleeWeaponDamage;
            }
            else
            {
                Inventory = new WizInventory(Game, RawBytes, Offsets.Inventory);
                SpellBook = new Wiz123KnownSpells(RawBytes, Offsets.Spells);
                SpellPoints = new WizSpellPoints(RawBytes, Offsets.CurrentSP);
                Regeneration = BitConverter.ToInt16(RawBytes, Offsets.Regeneration);
                Critical = BitConverter.ToInt16(RawBytes, Offsets.Critical);
                swings = BitConverter.ToInt16(RawBytes, Offsets.Swings);
                quantity = BitConverter.ToInt16(RawBytes, Offsets.MeleeDamage);
                faces = BitConverter.ToInt16(RawBytes, Offsets.MeleeDamage + 2);
                bonus = BitConverter.ToInt16(RawBytes, Offsets.MeleeDamage + 4);
                MeleeDamage = new BasicDamage(swings, new DamageDice(faces, quantity, bonus));
            }

            LocationX = BitConverter.ToInt16(RawBytes, Offsets.LocationX);
            LocationY = BitConverter.ToInt16(RawBytes, Offsets.LocationY);
            LocationZ = BitConverter.ToInt16(RawBytes, Offsets.LocationZ);

            if (this is Wiz5Character)
                PoisonCounter = BitConverter.ToInt16(RawBytes, Offsets.Poison);
            else
                PoisonCounter = (info != null && info.Location.MapIndex == 0) ? 0 : LocationX;

            Honors = BitConverter.ToInt16(RawBytes, Offsets.Awards);

            WeaponEffects = new byte[10];
            Buffer.BlockCopy(RawBytes, Offsets.WeaponEffects, WeaponEffects, 0, WeaponEffects.Length);
            GetWeaponEffects(WeaponEffects, 0, out ProtectionAgainst, out Resistances, out Purposed);

            if (Offsets.Marks != -1)
                Marks = new WizardryLong(RawBytes, Offsets.Marks).Number;
            if (Offsets.RIP != -1)
                RIP = BitConverter.ToInt16(RawBytes, Offsets.RIP);
            if (Offsets.Swim != -1)
                Swimming = BitConverter.ToInt16(RawBytes, Offsets.Swim);
        }

        public void GetWeaponEffects(byte[] bytes, int offset, out bool[,] ProtectionAgainst, out bool[,] Resistances, out bool[] Purposed)
        {
            ProtectionAgainst = new bool[2, 14];    // 4 bytes
            Resistances = new bool[2, 7];   // 4 bytes
            Purposed = new bool[14];    // 2 bytes

            for (int i = 0; i < 14; i++)
            {
                Purposed[i] = false;
                ProtectionAgainst[0, i] = false;
                ProtectionAgainst[1, i] = false;
            }
            for (int i = 0; i < 7; i++)
            {
                Resistances[0, i] = false;
                Resistances[1, i] = false;
            }

            Global.SetPackedBools(ProtectionAgainst, bytes[offset], 0, 0, 8);
            Global.SetPackedBools(ProtectionAgainst, bytes[offset + 1], 0, 8, 6);
            Global.SetPackedBools(ProtectionAgainst, bytes[offset + 2], 1, 0, 8);
            Global.SetPackedBools(ProtectionAgainst, bytes[offset + 3], 1, 8, 6);
            Global.SetPackedBools(Resistances, bytes[offset + 4], 0, 0, 7);
            Global.SetPackedBools(Resistances, bytes[offset + 6], 1, 0, 7);
            Global.SetPackedBools(Purposed, bytes[offset + 8], 0, 8);
            Global.SetPackedBools(Purposed, bytes[offset + 9], 8, 6);
        }

        public override int MaxBackpackSize { get { return 8 - Inventory.SelectEquippedItems.Count; } }

        public override Modifiers InternalModifiers
        {
            get
            {
                if (BasicRace == GenericRace.None)
                    return null;

                Modifiers mod = Wiz123.Modifiers.For(BasicRace).Clone();
                mod.Adjust(Wiz123.Modifiers.For(BasicClass).Clone());
                
                foreach (ModAttr attr in new ModAttr[] { ModAttr.SaveVsPoison, ModAttr.SaveVsPetrify, ModAttr.SaveVsWand, ModAttr.SaveVsBreath, ModAttr.SaveVsSpell })
                {
                    mod.Adjust(attr, GetStatModifier(BasicLuck.Temporary, PrimaryStat.Luck).Value, "Luck modifier");
                    mod.Adjust(attr, -Level / 5, "Level modifier");
                }

                mod.Adjust(ModAttr.SaveVsSleep, -Level * 4, "Level modifier");
                mod.Adjust(ModAttr.SaveVsParalyze, -10, "Basic chance to avoid paralysis");
                mod.Adjust(ModAttr.SaveVsParalyze, -Level * 2, "Level modifier");

                List<Item> equipped = Inventory.SelectEquippedItems;
                if (equipped.Count == 0)
                {
                    if (Class == WizClass.Ninja)
                        mod.Adjust(ModAttr.ArmorClass, -(Level / 3 + 2), "Unarmed Ninja bonus");
                }
                else
                {
                    foreach (WizItem item in Inventory.SelectEquippedItems)
                    {
                        if (item.CanEquip && item.AC > 0)
                            mod.Adjust(ModAttr.ArmorClass, -item.AC, item.DescriptionString);
                        if (item.CanEquip && item.Regeneration != 0)
                            mod.Adjust(ModAttr.Regen, item.Regeneration, item.DescriptionString);
                        if (item.CanEquip && item.AutoKill)
                            mod.Adjust(ModAttr.Critical, 1, item.DescriptionString);
                    }
                }
                foreach (WizItem item in Inventory.SelectUnequippedItems)
                {
                    if (item.Regeneration != 0 && Game != GameNames.Wizardry5)
                        mod.Adjust(ModAttr.Regen, item.Regeneration, String.Format("{0} (in backpack)", item.DescriptionString));
                }

                if (mod.ReasonArray[(int)ModAttr.Regen].Count > 1)
                    mod.ReasonArray[(int)ModAttr.Regen][mod.ReasonArray[(int)ModAttr.Regen].Count - 1] += "\r\n(Only the largest single regeneration bonus is actually applied)";
                return mod;
            }
        }

        public override StatAndModifier BasicStrength { get { return new StatAndModifier(Strength, 0); } }
        public override StatAndModifier BasicIQ { get { return new StatAndModifier(IQ, 0); } }
        public override StatAndModifier BasicPiety { get { return new StatAndModifier(Piety, 0); } }
        public override StatAndModifier BasicVitality { get { return new StatAndModifier(Vitality, 0); } }
        public override StatAndModifier BasicAgility { get { return new StatAndModifier(Agility, 0); } }
        public override StatAndModifier BasicLuck { get { return new StatAndModifier(Luck, 0); } }

        public override string Name { get { return CharName; } }
        public override SpellPoints QuickRefSpellPoints { get { return SpellPoints; } }
        public override List<Item> BackpackItems { get { return Inventory.SelectUnequippedItems; } }

        public override string GetACFormula(int iBless = 0)
        {
            if (ACBonus < 1)
                return String.Empty;
            return String.Format("-{0}\tSpell bonus", ACBonus);
        }

        public override bool KnowsSpell(Spell spell)
        {
            if (SpellBook == null)
                return false;
            return SpellBook.IsKnown(spell.BasicIndex, spell.Type);
        }

        public bool IsMage { get { return Class == WizClass.Mage || Class == WizClass.Bishop || Class == WizClass.Samurai; } }
        public bool IsPriest { get { return Class == WizClass.Priest || Class == WizClass.Bishop || Class == WizClass.Lord; } }

        public int MaxMageSpell(int iLevel)
        {
            switch (Class)
            {
                case WizClass.Mage: return Math.Min(7, (iLevel + 1) / 2);
                case WizClass.Bishop: return Math.Min(7, (iLevel + 3) / 4);
                case WizClass.Samurai: return Math.Min(7, (iLevel - 1) / 3);
                default: return 0;
            }
        }

        public int MaxPriestSpell(int iLevel)
        {
            switch (Class)
            {
                case WizClass.Priest: return Math.Min(7, (iLevel + 1) / 2);
                case WizClass.Bishop: return Math.Min(7, iLevel / 4);
                case WizClass.Lord: return Math.Min(7, (iLevel - 1) / 3);
                default: return 0;
            }
        }

        public override BasicDamage BasicMeleeDamage { get { return new BasicDamage(MeleeDamage.NumAttacks, MeleeDamage.Dice); } }

        public override string MeleeDamageString
        {
            get
            {
                StringBuilder sb = new StringBuilder(BasicMeleeDamage.ToString());
                bool bAnyPurposed = false;
                for (WizMonsterFamily family = WizMonsterFamily.First; family < WizMonsterFamily.Last; family++)
                {
                    if (Purposed[(int)family])
                    {
                        if (!bAnyPurposed)
                            sb.Append(", 2x ");
                        bAnyPurposed = true;
                        sb.AppendFormat("{0}/", WizMonster.GetFamilyString(family));
                    }
                }
                if (sb.Length > 0 && sb[sb.Length - 1] == '/')
                    sb.Remove(sb.Length - 1, 1);
                return sb.ToString();
            }
        }

        public override string CombatInfo
        {
            get
            {
                return String.Format("{0}{1} {2}", Condition == WizCondition.Good ? "" : "*", CharName, HitPoints.ToString());
            }
        }

        public override long NeedsXP
        {
            get
            {
                return XPForNextLevel - Experience;
            }
        }

        public override long XPForNextLevel { get { return XPForLevel(Class, Level + 1); } }

        public override long BasicExperience { get { return Experience; } }

        public override long XPForLevel(GenericClass gClass, int iLevel)
        {
            return XPForLevel(ClassForGeneric(gClass), iLevel);
        }

        public virtual long[] LevelArray(WizClass wiz1Class)
        {
            switch (wiz1Class)
            {
                case WizClass.Fighter: return new long[] { 0, 0, 1000, 1724, 2972, 5124, 8834, 15231, 26260, 45275, 78060, 134586, 232044, 400075, 289709 };
                case WizClass.Mage: return new long[] { 0, 0, 1100, 1896, 3268, 5634, 9713, 16746, 28872, 49779, 85825, 147974, 255127, 439874, 318529 };
                case WizClass.Priest: return new long[] { 0, 0, 1050, 1810, 3120, 5379, 9274, 15989, 27567, 47529, 81946, 141286, 243596, 419993, 304132 };
                case WizClass.Thief: return new long[] { 0, 0, 900, 1551, 2674, 4610, 7948, 13703, 23625, 40732, 70187, 121081, 208750, 359931, 260639 };
                case WizClass.Bishop: return new long[] { 0, 0, 1200, 2105, 3692, 6477, 11363, 19935, 34973, 61136, 107642, 188845, 331370, 581240, 438479 };
                case WizClass.Samurai: return new long[] { 0, 0, 1250, 2192, 3845, 6745, 11833, 20759, 36419, 63892, 112091, 196650, 345000, 605263, 456601 };
                case WizClass.Lord: return new long[] { 0, 0, 1300, 2280, 4000, 7017, 12310, 21596, 37887, 66468, 116610, 204578, 358908, 629663, 475008 };
                case WizClass.Ninja: return new long[] { 0, 0, 1450, 2543, 4461, 7826, 13729, 24085, 42254, 74129, 130050, 228157, 400275, 702236, 529756 };
                default: return new long[] { 0, 0, 1000, 1724, 2972, 5124, 8834, 15231, 26260, 45275, 78060, 134586, 232044, 400075, 289709 };
            }
        }

        public virtual long XPForLevel(WizClass wiz1Class, int iLevel)
        {
            long[] levels = LevelArray(wiz1Class);

            if (iLevel < 2)
                return 0;
            if (iLevel < 14)
                return levels[iLevel];
            else
                return levels[13] + (levels[14] * (iLevel - 13));
        }

        public static string AlignmentString(WizAlignment align) { return WizItem.GetAlignmentString(align); }

        public static string RaceString(WizRace race)
        {
            switch (race)
            {
                case WizRace.None: return "None";
                case WizRace.Dwarf: return "Dwarf";
                case WizRace.Elf: return "Elf";
                case WizRace.Gnome: return "Gnome";
                case WizRace.Hobbit: return "Hobbit";
                case WizRace.Human: return "Human";
                default: return String.Format("Unknown({0})", (int)race);
            }
        }
        public static string ClassString(WizClass classenum)
        {
            switch (classenum)
            {
                case WizClass.Fighter: return "Fighter";
                case WizClass.Mage: return "Mage";
                case WizClass.Priest: return "Priest";
                case WizClass.Thief: return "Thief";
                case WizClass.Bishop: return "Bishop";
                case WizClass.Samurai: return "Samurai";
                case WizClass.Lord: return "Lord";
                case WizClass.Ninja: return "Ninja";
                default: return String.Format("Unknown({0})", (int)classenum);
            }
        }

        public static string ConditionString(WizCondition cond) { return ConditionString(cond, String.Empty, true); }

        public static string ConditionString(WizCondition cond, string strExtra, bool bIncludeGood)
        {
            if (cond == WizCondition.Good)
            {
                if (!String.IsNullOrWhiteSpace(strExtra))
                    return strExtra;
                return bIncludeGood ? "Good" : "";
            }

            if (!String.IsNullOrWhiteSpace(strExtra))
                strExtra = ", " + strExtra;

            switch (cond)
            {
                case WizCondition.Good: return "Good" + strExtra;
                case WizCondition.Afraid: return "Afraid" + strExtra;
                case WizCondition.Asleep: return "Asleep" + strExtra;
                case WizCondition.Paralyzed: return "Paralyzed" + strExtra;
                case WizCondition.Petrified: return "Petrified" + strExtra;
                case WizCondition.Dead: return "Dead" + strExtra;
                case WizCondition.Ashes: return "Ashes" + strExtra;
                case WizCondition.Lost: return "Lost" + strExtra;
                default: return String.Format("Unknown({0})" + strExtra, (int)cond);
            }
        }

        public static StatModifier GetStatModifier(int value, PrimaryStat stat)
        {
            switch (stat)
            {
                case PrimaryStat.Strength:
                    return StatModifier.FromTable(value, stat, 4, -3, 5, -2, 6, -1, 16, 0, 17, 1, 18, 2, 3);
                case PrimaryStat.IQ:
                case PrimaryStat.Piety:
                    return StatModifier.FromTable(value, stat, 1, 0, 2, 3, 3, 7, 4, 10, 5, 13, 6, 17, 7, 20, 8, 23, 9, 27,
                        10, 30, 11, 33, 12, 37, 13, 40, 14, 43, 15, 47, 16, 50, 17, 53, 18, 57, 60);
                case PrimaryStat.Agility:
                    return StatModifier.FromTable(value, stat, 4, 3, 6, 2, 8, 1, 15, 0, 16, -1, 17, -2, 18, -3, -4);
                case PrimaryStat.Vitality:
                    return StatModifier.FromTable(value, stat, 4, -2, 6, -1, 16, 0, 17, 1, 18, 2, 3);
                case PrimaryStat.Luck:
                    return StatModifier.FromTable(value, stat, 6, 0, 12, -1, 18, -2, -3);
                default:
                    return StatModifier.Zero;
            }
        }

        public int NumAttacks
        {
            get
            {
                int iNumAttacks = 1;
                switch (Class)
                {
                    case WizClass.Fighter:
                    case WizClass.Priest:
                    case WizClass.Samurai:
                    case WizClass.Lord:
                        iNumAttacks = Level / 5 + 1;
                        break;
                    case WizClass.Ninja:
                        iNumAttacks = Level / 5 + 2;
                        break;
                }
                return iNumAttacks;
            }
        }

        public static string ConditionDescription(WizCondition cond, WizCharacter wizChar = null)
        {
            string strExtra = wizChar == null ? String.Empty : wizChar.GetExtraConditionDesc();
            if (cond == WizCondition.Good)
            {
                if (String.IsNullOrWhiteSpace(strExtra))
                    return "Good: Character is healthy";
                return strExtra;
            }

            strExtra = "\r\n" + strExtra;
            // Wizardry 1 conditions are not flags; you may only have one at a time (except Poison, which is stored elsewhere)
            switch (cond)
            {
                case WizCondition.Good:
                case WizCondition.Afraid:
                    if (wizChar == null)
                        return "Afraid: Cannot identify items" + strExtra;
                    return String.Format("Afraid: Cannot identify items, recovery chance {0}%/round", Math.Min(50, 5 * wizChar.Level)) + strExtra;
                case WizCondition.Asleep:
                    if (wizChar == null)
                        return "Asleep: Cannot perform actions until attacked";
                    return String.Format("Asleep: Cannot perform actions until attacked, recovery chance {0}%/round", Math.Min(50, 10 * wizChar.Level)) + strExtra;
                case WizCondition.Dead: return "Dead: Cannot perform actions and gains no XP" + strExtra;
                case WizCondition.Lost: return "Lost: Character cannot be recovered without cheating" + strExtra;
                case WizCondition.Paralyzed: return "Paralyzed: Cannot perform actions" + strExtra;
                case WizCondition.Petrified: return "Stone: Cannot perform actions and gains no XP" + strExtra;
                case WizCondition.Ashes: return "Ashes: Cannot perform actions and gains no XP" + strExtra;
                default: return String.Format("Unknown Condition({0})" + strExtra, (int)cond);
            }
        }

        public override string AttributesString
        {
            get
            {
                return String.Format("Str:{0}, IQ:{1}, Pie:{2}, Vit:{3}, Agi:{4}, Lck:{5}",
                    Strength.ToString(),
                    IQ.ToString(),
                    Piety.ToString(),
                    Vitality.ToString(),
                    Agility.ToString(),
                    Luck.ToString());
            }
        }

        public override string ExperienceString
        {
            get
            {
                if (Level >= MaxLevel)
                    return String.Format("{0} (Max Level)", Experience);
                return String.Format("{0}{1}", Experience, ReadyToTrain ? " (Train!)" : ("/" + XPForNextLevel.ToString()));
            }
        }

        public override int TrainableLevel
        {
            get
            {
                int iLevel = Level;
                while (XPForLevel(Class, iLevel + 1) <= Experience && iLevel < 255)
                    iLevel++;
                return iLevel;
            }
        }

        public override bool ReadyToTrain
        {
            get
            {
                return NeedsXP < 1;
            }
        }

        public static int MaxHPPerLevel(WizClass wiz1Class)
        {
            switch (wiz1Class)
            {
                case WizClass.Fighter: return 10;
                case WizClass.Mage: return 4;
                case WizClass.Priest: return 8;
                case WizClass.Thief: return 6;
                case WizClass.Bishop: return 6;
                case WizClass.Samurai: return 8;
                case WizClass.Lord: return 10;
                case WizClass.Ninja: return 6;
                default:
                    return 0;
            }
        }

        public string HPLevelString
        {
            get
            {
                int iBase = MaxHPPerLevel(Class);
                int iBonus = WizCharacter.GetStatModifier(Vitality, PrimaryStat.Vitality).Value;

                return String.Format("{0} - {1}", iBonus + 1, iBonus + iBase);
            }
        }

        public override string EquippedString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (WizItem item in Inventory.SelectEquippedItems)
                    sb.AppendFormat("{0}, ", (item.IsIdentified || !Properties.Settings.Default.HideUnidentifiedItems) ? item.Name : String.Format("Unidentified {0}", item.ItemNoun));
                Global.Trim(sb);
                if (sb.Length == 0)
                    return "(nothing)";
                return sb.ToString();
            }
        }

        public override string BackpackString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                foreach (WizItem item in Inventory.SelectUnequippedItems)
                    sb.AppendFormat("{0}, ", (item.IsIdentified || !Properties.Settings.Default.HideUnidentifiedItems) ? item.Name : String.Format("Unidentified {0}", item.ItemNoun));
                Global.Trim(sb);
                if (sb.Length == 0)
                    return "(empty)";
                return sb.ToString();
            }
        }

        public static string AgeString(int age)
        {
            // Age is in weeks
            return String.Format("{0}, {1}", Global.Plural(age / 52, "year"), Global.Plural(age % 52, "week"));
        }

        public override string BasicInfoString
        {
            get
            {
                if (String.IsNullOrWhiteSpace(Name))
                    return "<Invalid Character Record>";
                return String.Format("Level {0} {1} {2} {3}, {4} old",
                 Level.ToString(),
                 WizCharacter.AlignmentString(Alignment),
                 WizCharacter.RaceString(Race),
                 WizCharacter.ClassString(Class),
                 WizCharacter.AgeString(Age));
            }
        }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(CharName);
                sb.AppendLine(BasicInfoString);
                sb.AppendLine(AttributesString);
                sb.AppendFormat("Experience: {0}\r\n", ExperienceString);
                sb.AppendFormat("Condition: {0}\r\n", WizCharacter.ConditionString(Condition, GetExtraConditions(), true));
                sb.AppendFormat("HP: {0}\r\n", HitPoints.ToString());
                sb.AppendFormat("SP: {0}\r\n", SpellPoints.ToString());
                sb.AppendFormat("AC: {0}\r\n", ArmorClass);
                sb.AppendFormat("Melee: {0}\r\n", MeleeDamageString);
                sb.AppendFormat("Equipped: {0}\r\n", EquippedString);
                sb.AppendFormat("Backpack: {0}\r\n", BackpackString);
                sb.AppendFormat("Gold: {0}\r\n", Gold);
                return sb.ToString();
            }
        }

        public override StatAndModifier BasicLevel { get { return new StatAndModifier(Level, LevelMod - Level); } }
        public override StatAndModifier BasicAC { get { return new StatAndModifier(LastArmorClass, ArmorClass - LastArmorClass); } }

        public override GenericClass BasicClass
        {
            get
            {
                switch (Class)
                {
                    case WizClass.Bishop: return GenericClass.Bishop;
                    case WizClass.Fighter: return GenericClass.Fighter;
                    case WizClass.Lord: return GenericClass.Lord;
                    case WizClass.Mage: return GenericClass.Mage;
                    case WizClass.Ninja: return GenericClass.Ninja;
                    case WizClass.Priest: return GenericClass.Priest;
                    case WizClass.Samurai: return GenericClass.Samurai;
                    case WizClass.Thief: return GenericClass.Thief;
                    default: return GenericClass.None;
                }
            }
        }

        public override GenericRace BasicRace
        {
            get
            {
                switch (Race)
                {
                    case WizRace.Human: return GenericRace.Human;
                    case WizRace.Elf: return GenericRace.Elf;
                    case WizRace.Gnome: return GenericRace.Gnome;
                    case WizRace.Dwarf: return GenericRace.Dwarf;
                    case WizRace.Hobbit: return GenericRace.Hobbit;
                    default: return GenericRace.None;
                }
            }
        }

        public override GenericAge BasicAge { get { return new GenericAge(Age / 52, (Age % 52) * 7); } }

        public override GenericAlignment BasicAlignment
        {
            get
            {
                return new GenericAlignment(BasicAlignmentValue(true), BasicAlignmentValue(true));
            }
        }

        public static GenericAlignmentValue GetGenericAlignment(WizAlignment alignment)
        {
            switch (alignment)
            {
                case WizAlignment.Good: return GenericAlignmentValue.Good;
                case WizAlignment.Neutral: return GenericAlignmentValue.Neutral;
                case WizAlignment.Evil: return GenericAlignmentValue.Evil;
                default: return GenericAlignmentValue.None;
            }
        }

        public GenericAlignmentValue BasicAlignmentValue(bool bTemporary)
        {
            return GetGenericAlignment(Alignment);
        }

        public override long QuickRefExperience { get { return Experience; } }
        public override MMHitPoints QuickRefHitPoints { get { return HitPoints; } }
        public override string QuickRefCondition { get { return WizCharacter.ConditionString(Condition, GetExtraConditions(), false); } }
        public override bool IsHealer { get { return true; } }   // Any Wizard character may know spells from a prior class

        public override BasicConditionFlags BasicCondition { get { return GetBasicCondition(Condition); } }

        public static BasicConditionFlags GetBasicCondition(WizCondition wiz1Condition)
        {
            BasicConditionFlags cond = BasicConditionFlags.Good;

            if (wiz1Condition.HasFlag(WizCondition.Lost))
                cond |= BasicConditionFlags.Lost;
            if (wiz1Condition.HasFlag(WizCondition.Ashes))
                cond |= BasicConditionFlags.Eradicated;
            if (wiz1Condition.HasFlag(WizCondition.Dead))
                cond |= BasicConditionFlags.Dead;
            if (wiz1Condition.HasFlag(WizCondition.Petrified))
                cond |= BasicConditionFlags.Stone;
            if (wiz1Condition.HasFlag(WizCondition.Paralyzed))
                cond |= BasicConditionFlags.Paralyzed;
            if (wiz1Condition.HasFlag(WizCondition.Asleep))
                cond |= BasicConditionFlags.Asleep;
            if (wiz1Condition.HasFlag(WizCondition.Afraid))
                cond |= BasicConditionFlags.Afraid;

            return cond;
        }

        public override byte ConditionValue(BasicConditionFlags condition)
        {
            if (condition.HasFlag(BasicConditionFlags.Lost))
                return (byte)WizCondition.Lost;
            if (condition.HasFlag(BasicConditionFlags.Eradicated))
                return (byte)WizCondition.Ashes;
            if (condition.HasFlag(BasicConditionFlags.Dead))
                return (byte)WizCondition.Dead;
            if (condition.HasFlag(BasicConditionFlags.Stone))
                return (byte)WizCondition.Petrified;
            if (condition.HasFlag(BasicConditionFlags.Paralyzed))
                return (byte)WizCondition.Paralyzed;
            if (condition.HasFlag(BasicConditionFlags.Asleep))
                return (byte)WizCondition.Asleep;
            if (condition.HasFlag(BasicConditionFlags.Afraid))
                return (byte)WizCondition.Afraid;

            return (byte)WizCondition.Good;
        }

        public override byte AlignmentValue(GenericAlignmentValue align)
        {
            switch (align)
            {
                case GenericAlignmentValue.Evil: return (byte)WizAlignment.Evil;
                case GenericAlignmentValue.Neutral: return (byte)WizAlignment.Neutral;
                case GenericAlignmentValue.Good: return (byte)WizAlignment.Good;
                default: return (byte)WizAlignment.None;
            }
        }

        public override byte RaceValue(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Human: return (byte)WizRace.Human;
                case GenericRace.Elf: return (byte)WizRace.Elf;
                case GenericRace.Dwarf: return (byte)WizRace.Dwarf;
                case GenericRace.Gnome: return (byte)WizRace.Gnome;
                case GenericRace.Hobbit: return (byte)WizRace.Hobbit;
                default: return (byte)WizRace.None;
            }
        }

        public static WizClass ClassForGeneric(GenericClass gClass)
        {
            switch (gClass)
            {
                case GenericClass.Fighter: return WizClass.Fighter;
                case GenericClass.Mage: return WizClass.Mage;
                case GenericClass.Priest: return WizClass.Priest;
                case GenericClass.Thief: return WizClass.Thief;
                case GenericClass.Bishop: return WizClass.Bishop;
                case GenericClass.Samurai: return WizClass.Samurai;
                case GenericClass.Lord: return WizClass.Lord;
                case GenericClass.Ninja: return WizClass.Ninja;
                default: return WizClass.None;
            }
        }

        public override byte ClassValue(GenericClass classVal)
        {
            return (byte)ClassForGeneric(classVal);
        }

        public override Item GetItem(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < 8)
                return null;

            return WizItem.FromInventoryBytes(Game, bytes, offset);
        }

        public override int FirstEmptyBackpackIndex
        {
            get
            {
                // Wizardry items are always stored in order
                if (Inventory == null)
                    return -1;
                if (Inventory.Items.Count > 7)
                    return -1;
                return Inventory.Items.Count;
            }
        }

        public override bool BackpackFull
        {
            get
            {
                return (FirstEmptyBackpackIndex == -1);
            }
        }

        public override int MaxLevel { get { return Int16.MaxValue; } }

        public override string GetCriticalFormula()
        {
            if (Critical != 0)
                return "Chance to cause a critical hit: Level * 2 (max 50%)\r\n(Monsters have approximately a (3*HitDice + 27)% chance to evade)";
            return "Non-ninjas must equip a critical-enabled weapon in order to make critical hits";
        }
    }

    public class CheatTag
    {
        public long Maximum;
        public long Minimum;

        public byte ByteMax { get { return (byte)Maximum; } }
        public Int16 Int16Max { get { return (Int16)Maximum; } }
        public UInt16 UInt16Max { get { return (UInt16)Maximum; } }
        public Int32 Int32Max { get { return (Int32)Maximum; } }
        public UInt32 UInt32Max { get { return (UInt32)Maximum; } }

        public byte ByteMin { get { return (byte)Minimum; } }
        public Int16 Int16Min { get { return (Int16)Minimum; } }
        public UInt16 UInt16Min { get { return (UInt16)Minimum; } }
        public Int32 Int32Min { get { return (Int32)Minimum; } }
        public UInt32 UInt32Min { get { return (UInt32)Minimum; } }

        public CheatTag(long max = 0, long min = 0)
        {
            Maximum = max;
            Minimum = min;
        }
    }

    public class WizardryCheatTag : CheatTag
    {
        public PackedStat Stat;

        public bool IsWizLong { get { return Stat == PackedStat.Gold || Stat == PackedStat.Experience; } }
        public bool IsFiveBitStat
        {
            get
            {
                switch (Stat)
                {
                    case PackedStat.Strength:
                    case PackedStat.IQ:
                    case PackedStat.Piety:
                    case PackedStat.Vitality:
                    case PackedStat.Agility:
                    case PackedStat.Luck:
                    case PackedStat.SaveVsPoison:
                    case PackedStat.SaveVsPetrify:
                    case PackedStat.SaveVsWand:
                    case PackedStat.SaveVsBreath:
                    case PackedStat.SaveVsSpell:
                        return true;
                    default:
                        return false;
                }
            }
        }

        public int StatOffset
        {
            get
            {
                switch (Stat)
                {
                    case PackedStat.Strength:
                    case PackedStat.IQ:
                    case PackedStat.Piety:
                    case PackedStat.Vitality:
                    case PackedStat.Agility:
                    case PackedStat.Luck:
                        return Stat - PackedStat.Strength;
                    case PackedStat.SaveVsPoison:
                    case PackedStat.SaveVsPetrify:
                    case PackedStat.SaveVsWand:
                    case PackedStat.SaveVsBreath:
                    case PackedStat.SaveVsSpell:
                        return Stat - PackedStat.SaveVsPoison;
                    default:
                        return 0;
                }
            }
        }

        public WizardryCheatTag(PackedStat stat, long max = 0, long min = 0)
        {
            Stat = stat;
            Maximum = max;
            Minimum = min;
        }
    }
}