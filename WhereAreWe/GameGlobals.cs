using System;
using System.Collections.Generic;

namespace WhereAreWe
{
    public interface GameGlobals
    {
        string GetMonsterName(int index);
        Item GetItem(int index);
        void InitItemList(MemoryHacker hacker);
        void InitMonsterList(MemoryHacker hacker);
        void ResetItemList();
        void ResetMonsterList();
        List<Spell> GetAllSpells();
        BasicWall GetWall(int iMap, int iIndex);
    }

    public static class MM1
    {
        public static Lazy<MM1Locations> Spots1 = new Lazy<MM1Locations>();
        public static Lazy<MM1SpellList> SpellList = new Lazy<MM1SpellList>();
        public static Lazy<MM1EncounterData> EncounterData = new Lazy<MM1EncounterData>();
        public static MM1CharacterOffsets Offsets = new MM1CharacterOffsets();
        public static MM1RaceModifiers Modifiers = new MM1RaceModifiers();
        public static Lazy<MM1ItemList> ItemList = new Lazy<MM1ItemList>();
        public static Lazy<MM1MonsterList> MonsterList = new Lazy<MM1MonsterList>();
        public static List<MM1Item> Items { get { return ItemList.Value.Items; } }
        public static List<MM1Monster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static string MonsterName(int index) { return index >= 0 && index < Monsters.Count ? Monsters[index].ProperName : "<Unknown>"; }

        public class MM1RaceModifiers : RaceClassModifiers
        {
            public MM1RaceModifiers()
            {
                Human = new Modifiers();
                Human.Adjust(ModAttr.Fear, 70, "Race bonus (human)");
                Human.Adjust(ModAttr.Sleep, 25, "Race bonus (human)");
                Elf = new Modifiers();
                Elf.Adjust(ModAttr.Fear, 70, "Race bonus (elf)");
                Elf.Adjust(ModAttr.Intellect, 1, "Race bonus (elf)");
                Elf.Adjust(ModAttr.Accuracy, 1, "Race bonus (elf)");
                Elf.Adjust(ModAttr.Might, -1, "Race penalty (elf)");
                Elf.Adjust(ModAttr.Endurance, -1, "Race penalty (elf)");
                Dwarf = new Modifiers();
                Dwarf.Adjust(ModAttr.Poison, 25, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Endurance, 1, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Luck, 1, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Intellect, -1, "Race penalty (dwarf)");
                Dwarf.Adjust(ModAttr.Speed, -1, "Race penalty (dwarf)");
                Gnome = new Modifiers();
                Gnome.Adjust(ModAttr.Magic, 20, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Luck, 2, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Speed, -1, "Race penalty (gnome)");
                Gnome.Adjust(ModAttr.Accuracy, -1, "Race penalty (gnome)");
                Halforc = new Modifiers();
                Halforc.Adjust(ModAttr.Sleep, 50, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Might, 1, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Endurance, 1, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Intellect, -1, "Race penalty (half-orc)");
                Halforc.Adjust(ModAttr.Personality, -1, "Race penalty (half-orc)");
                Halforc.Adjust(ModAttr.Luck, -1, "Race penalty (half-orc)");
            }
        }

        public static List<MM1Spell> Spells { get { return SpellList.Value.Spells; } }
        public static MM1EncounterData Encounters { get { return EncounterData.Value; } }
        public static MM1Locations Spots { get { return Spots1.Value; } }
    }

    public class MM1Globals : GameGlobals
    {
        public Item GetItem(int index) { return index >= 0 && index < MM1.Items.Count ? MM1.Items[index] : null; }
        public void ResetItemList() { MM1.ItemList = new Lazy<MM1ItemList>(); }
        public void ResetMonsterList() { MM1.MonsterList = new Lazy<MM1MonsterList>(); }
        public string GetMonsterName(int index) { return MM1.MonsterName(index); }
        public void InitItemList(MemoryHacker hacker) { } // MM1 item list is internal
        public void InitMonsterList(MemoryHacker hacker) { } // MM1 monster list is internal
        public List<Spell> GetAllSpells() { return new List<Spell>(MM1.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }

    public static class MM2
    {
        public static Lazy<MM2Locations> Spots2 = new Lazy<MM2Locations>();
        public static Lazy<MM2ItemList> ItemList = new Lazy<MM2ItemList>();
        public static Lazy<MM2SpellList> SpellList = new Lazy<MM2SpellList>();
        public static Lazy<MM2MonsterList> MonsterList = new Lazy<MM2MonsterList>();
        public static Lazy<MM2EncounterData> EncounterData = new Lazy<MM2EncounterData>();
        public static MM2CharacterOffsets Offsets = new MM2CharacterOffsets();
        public static MM2RaceModifiers Modifiers = new MM2RaceModifiers();
        public static string MonsterName(int index) { return index >= 0 && index < Monsters.Count ? Monsters[index].ProperName : "<Unknown>"; }

        public class MM2RaceModifiers : RaceClassModifiers
        {
            public MM2RaceModifiers()
            {
                Human = new Modifiers();
                Human.Adjust(ModAttr.Fire, 5, "Race bonus (human)");
                Human.Adjust(ModAttr.Electricity, 5, "Race bonus (human)");
                Human.Adjust(ModAttr.Cold, 5, "Race bonus (human)");
                Human.Adjust(ModAttr.Sleep, 60, "Race bonus (human)");
                Human.Adjust(ModAttr.Poison, 60, "Race bonus (human)");
                Elf = new Modifiers();
                Elf.Adjust(ModAttr.Sleep, 30, "Race bonus (elf)");
                Elf.Adjust(ModAttr.Poison, 5, "Race bonus (elf)");
                Elf.Adjust(ModAttr.Intellect, 1, "Race bonus (elf)");
                Elf.Adjust(ModAttr.Accuracy, 1, "Race bonus (elf)");
                Elf.Adjust(ModAttr.Might, -1, "Race penalty (elf)");
                Elf.Adjust(ModAttr.Endurance, -1, "Race penalty (elf)");
                Dwarf = new Modifiers();
                Dwarf.Adjust(ModAttr.Fire, 10, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Electricity, 10, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Cold, 10, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Poison, 60, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Endurance, 1, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Luck, 1, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Intellect, -1, "Race penalty (dwarf)");
                Dwarf.Adjust(ModAttr.Speed, -1, "Race penalty (dwarf)");
                Gnome = new Modifiers();
                Gnome.Adjust(ModAttr.Magic, 35, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Luck, 2, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Speed, -1, "Race penalty (gnome)");
                Gnome.Adjust(ModAttr.Accuracy, -1, "Race penalty (gnome)");
                Halforc = new Modifiers();
                Halforc.Adjust(ModAttr.Fire, 5, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Electricity, 5, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Cold, 5, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Sleep, 30, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Poison, 30, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Might, 1, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Endurance, 1, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Intellect, -1, "Race penalty (half-orc)");
                Halforc.Adjust(ModAttr.Personality, -1, "Race penalty (half-orc)");
                Halforc.Adjust(ModAttr.Luck, -1, "Race penalty (half-orc)");
            }
        }

        public static MM2Locations Spots { get { return Spots2.Value; } }
        public static List<MM2Monster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static List<MM2Spell> Spells { get { return SpellList.Value.Spells; } }
        public static List<MM2Item> Items { get { return ItemList.Value.Items; } }
        public static MM2EncounterData Encounters { get { return EncounterData.Value; } }
    }

    public class MM2Globals : GameGlobals
    {
        public Item GetItem(int index) { return index >= 0 && index < MM2.Items.Count ? MM2.Items[index] : null; }
        public void ResetItemList() { MM2.ItemList = new Lazy<MM2ItemList>(); }
        public void ResetMonsterList() { MM2.MonsterList = new Lazy<MM2MonsterList>(); }
        public string GetMonsterName(int index) { return MM2.MonsterName(index); }
        public void InitItemList(MemoryHacker hacker) { } // MM2 item list is internal
        public void InitMonsterList(MemoryHacker hacker) { } // MM2 monster list is internal
        public List<Spell> GetAllSpells() { return new List<Spell>(MM2.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }

    public static class MM3
    {
        public static Lazy<MM3Locations> Spots3 = new Lazy<MM3Locations>();
        public static Lazy<MM3SpellList> SpellList = new Lazy<MM3SpellList>();
        public static Lazy<MM3MonsterList> MonsterList = new Lazy<MM3MonsterList>();
        public static MM3CharacterOffsets Offsets = new MM3CharacterOffsets();
        public static MM3RaceModifiers Modifiers = new MM3RaceModifiers();
        public static string MonsterName(int index) { return index >= 0 && index < Monsters.Count ? Monsters[index].ProperName : "<Unknown>"; }
        public class MM3RaceModifiers : RaceClassModifiers
        {
            public MM3RaceModifiers()
            {
                Human = new Modifiers();
                Human.Adjust(ModAttr.Fire, 7, "Race bonus (human)");
                Human.Adjust(ModAttr.Cold, 7, "Race bonus (human)");
                Human.Adjust(ModAttr.Electricity, 7, "Race bonus (human)");
                Human.Adjust(ModAttr.Poison, 7, "Race bonus (human)");
                Human.Adjust(ModAttr.Energy, 7, "Race bonus (human)");
                Human.Adjust(ModAttr.Magic, 7, "Race bonus (human)");
                Elf = new Modifiers();
                Elf.Adjust(ModAttr.Energy, 5, "Race bonus (elf)");
                Elf.Adjust(ModAttr.Magic, 5, "Race bonus (elf)");
                Dwarf = new Modifiers();
                Dwarf.Adjust(ModAttr.Fire, 5, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Cold, 5, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Electricity, 5, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Poison, 20, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Energy, 5, "Race bonus (dwarf)");
                Gnome = new Modifiers();
                Gnome.Adjust(ModAttr.Fire, 2, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Cold, 2, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Electricity, 2, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Poison, 2, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Energy, 2, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Magic, 20, "Race bonus (gnome)");
                Halforc = new Modifiers();
                Halforc.Adjust(ModAttr.Fire, 10, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Cold, 10, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Electricity, 10, "Race bonus (half-orc)");
            }
        }

        public static MM3Locations Spots { get { return Spots3.Value; } }
        public static List<MMMonster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static Dictionary<MM3SpellIndex, MM3Spell> Spells { get { return SpellList.Value.Spells; } }
    }

    public class MM3Globals : GameGlobals
    {
        // MM3 doesn't have an item list; all are built dynamically
        public Item GetItem(int index) { return null; }
        public void ResetItemList() { }
        public void ResetMonsterList() { MM3.MonsterList = new Lazy<MM3MonsterList>(); }
        public string GetMonsterName(int index) { return MM3.MonsterName(index); }
        public void InitItemList(MemoryHacker hacker) { } // MM3 item list is internal
        public void InitMonsterList(MemoryHacker hacker) { } // MM3 monster list is internal
        public List<Spell> GetAllSpells() { return new List<Spell>(MM3.SpellList.Value.GetSpells()); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }

    public static class MM45
    {
        public static MM4FileOffsets FileOffsetsMM4 = new MM4FileOffsets();
        public static MM5FileOffsets FileOffsetsMM5 = new MM5FileOffsets();
        public static Lazy<MM45Locations> Spots45 = new Lazy<MM45Locations>();
        public static Lazy<MM45SpellList> SpellList = new Lazy<MM45SpellList>();
        public static Lazy<MM4MonsterList> MM4MonsterList = new Lazy<MM4MonsterList>();
        public static Lazy<MM5MonsterList> MM5MonsterList = new Lazy<MM5MonsterList>();
        public static MM45CharacterOffsets Offsets = new MM45CharacterOffsets();
        public static MM45RaceModifiers Modifiers = new MM45RaceModifiers();
        public static string MonsterName(int index)
        {
            if (index < 0)
                return "<Unknown";
            if (index < MM4Monsters.Count)
                return MM4Monsters[index].ProperName;
            index -= MM4Monsters.Count;
            if (index < MM5Monsters.Count)
                return MM5Monsters[index].ProperName;
            return "<Unknown";
        }
        public class MM45RaceModifiers : RaceClassModifiers
        {
            public MM45RaceModifiers()
            {
                Human = new Modifiers();
                Human.Adjust(ModAttr.Fire, 7, "Race bonus (human)");
                Human.Adjust(ModAttr.Cold, 7, "Race bonus (human)");
                Human.Adjust(ModAttr.Electricity, 7, "Race bonus (human)");
                Human.Adjust(ModAttr.Poison, 7, "Race bonus (human)");
                Human.Adjust(ModAttr.Energy, 7, "Race bonus (human)");
                Human.Adjust(ModAttr.Magic, 7, "Race bonus (human)");
                Elf = new Modifiers();
                Elf.Adjust(ModAttr.Energy, 5, "Race bonus (elf)");
                Elf.Adjust(ModAttr.Magic, 5, "Race bonus (elf)");
                Gnome = new Modifiers();
                Gnome.Adjust(ModAttr.Fire, 5, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Cold, 5, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Electricity, 5, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Poison, 20, "Race bonus (gnome)");
                Gnome.Adjust(ModAttr.Energy, 5, "Race bonus (gnome)");
                Dwarf = new Modifiers();
                Dwarf.Adjust(ModAttr.Fire, 2, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Cold, 2, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Electricity, 2, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Poison, 2, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Energy, 2, "Race bonus (dwarf)");
                Dwarf.Adjust(ModAttr.Magic, 20, "Race bonus (dwarf)");
                Halforc = new Modifiers();
                Halforc.Adjust(ModAttr.Fire, 10, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Cold, 10, "Race bonus (half-orc)");
                Halforc.Adjust(ModAttr.Electricity, 10, "Race bonus (half-orc)");
            }
        }

        public static MM45Locations Spots { get { return Spots45.Value; } }
        public static List<MMMonster> MM4Monsters { get { return MM4MonsterList.Value.Monsters; } }
        public static List<MMMonster> MM5Monsters { get { return MM5MonsterList.Value.Monsters; } }
        public static Dictionary<MM45SpellIndex, MM45Spell> Spells { get { return SpellList.Value.Spells; } }
    }

    public class MM45Globals : GameGlobals
    {
        // MM4/5 don't have item lists; all are built dynamically
        public Item GetItem(int index) { return null; }
        public void ResetItemList() { }
        public void ResetMonsterList()
        {
            MM45.MM4MonsterList = new Lazy<MM4MonsterList>();
            MM45.MM5MonsterList = new Lazy<MM5MonsterList>();
        }
        public string GetMonsterName(int index) { return MM45.MonsterName(index); }
        public void InitItemList(MemoryHacker hacker) { } // MM45 item list is internal
        public void InitMonsterList(MemoryHacker hacker) { } // MM45 monster list is internal
        public List<Spell> GetAllSpells() { return new List<Spell>(MM45.SpellList.Value.GetSpells()); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }

    public static class Wiz123
    {
        public static Lazy<Wiz1234SpellList> SpellList = new Lazy<Wiz1234SpellList>();
        public static Wiz1234CharacterOffsets Offsets = new Wiz1234CharacterOffsets();
        public static Wiz123RaceModifiers Modifiers = new Wiz123RaceModifiers();
        public static List<Wiz1234Spell> Spells { get { return SpellList.Value.Spells; } }

        public class Wiz123RaceModifiers : RaceClassModifiers
        {
            public Wiz123RaceModifiers()
            {
                Human = new Modifiers();
                Human.Adjust(ModAttr.SaveVsPoison, -1, "Race bonus (human)");
                Elf = new Modifiers();
                Elf.Adjust(ModAttr.SaveVsWand, -2, "Race bonus (elf)");
                Gnome = new Modifiers();
                Gnome.Adjust(ModAttr.SaveVsPetrify, -2, "Race bonus (gnome)");
                Dwarf = new Modifiers();
                Dwarf.Adjust(ModAttr.SaveVsBreath, -4, "Race bonus (dwarf)");
                Hobbit = new Modifiers();
                Hobbit.Adjust(ModAttr.SaveVsSpell, -3, "Race bonus (hobbit)");

                Fighter = new Modifiers();
                Fighter.Adjust(ModAttr.SaveVsPoison, -3, "Class bonus (Fighter)");
                Mage = new Modifiers();
                Mage.Adjust(ModAttr.SaveVsSpell, -3, "Class bonus (Mage)");
                Priest = new Modifiers();
                Priest.Adjust(ModAttr.SaveVsPetrify, -3, "Class bonus (Priest)");
                Thief = new Modifiers();
                Thief.Adjust(ModAttr.SaveVsBreath, -3, "Class bonus (Thief)");
                Bishop = new Modifiers();
                Bishop.Adjust(ModAttr.SaveVsPetrify, -2, "Class bonus (Bishop)");
                Bishop.Adjust(ModAttr.SaveVsBreath, -2, "Class bonus (Bishop)");
                Bishop.Adjust(ModAttr.SaveVsSpell, -2, "Class bonus (Bishop)");
                Samurai = new Modifiers();
                Samurai.Adjust(ModAttr.SaveVsPoison, -2, "Class bonus (Samurai)");
                Samurai.Adjust(ModAttr.SaveVsSpell, -2, "Class bonus (Samurai)");
                Lord = new Modifiers();
                Lord.Adjust(ModAttr.SaveVsPoison, -2, "Class bonus (Lord)");
                Lord.Adjust(ModAttr.SaveVsPetrify, -2, "Class bonus (Lord)");
                Ninja = new Modifiers();
                Ninja.Adjust(ModAttr.SaveVsPoison, -3, "Class bonus (Ninja)");
                Ninja.Adjust(ModAttr.SaveVsPetrify, -2, "Class bonus (Ninja)");
                Ninja.Adjust(ModAttr.SaveVsWand, -4, "Class bonus (Ninja)");
                Ninja.Adjust(ModAttr.SaveVsBreath, -3, "Class bonus (Ninja)");
                Ninja.Adjust(ModAttr.SaveVsSpell, -2, "Class bonus (Ninja)");
            }
        }
    }

    public static class BT1
    {
        public static Lazy<BT1SpellList> SpellList = new Lazy<BT1SpellList>();
        public static Lazy<BT1Locations> SpotsBT1 = new Lazy<BT1Locations>();
        public static BT1CharacterOffsets Offsets = new BT1CharacterOffsets();
        public static List<BT1Spell> Spells { get { return SpellList.Value.Spells; } }
        public static Lazy<BT1Memory> MemoryData = new Lazy<BT1Memory>();
        public static BT1Item Item(BT1ItemIndex index) { return Global.BT1.GetDirectItem((int) index) as BT1Item; }
        public static Lazy<BT1ItemList> ItemList = new Lazy<BT1ItemList>();
        public static Lazy<BT1MonsterList> MonsterList = new Lazy<BT1MonsterList>();
        public static List<BTItem> Items { get { return ItemList.Value.Items; } }
        public static List<BTMonster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static string MonsterName(int index) { return index >= 0 && index < Monsters.Count ? Monsters[index].ProperName : "<Unknown>"; }
        public static string SpellName(int index) { return index >= 0 && index < BT1.Spells.Count ? BT1.Spells[index].Abbreviation : "<Unknown>"; }
        public static BT1Memory Memory { get { return MemoryData.Value; } }
        public static BT1Locations Spots { get { return SpotsBT1.Value; } }
    }

    public static class BT2
    {
        public static Lazy<BT2SpellList> SpellList = new Lazy<BT2SpellList>();
        public static Lazy<BT2Locations> SpotsBT2 = new Lazy<BT2Locations>();
        public static BT2CharacterOffsets Offsets = new BT2CharacterOffsets();
        public static List<BT2Spell> Spells { get { return SpellList.Value.Spells; } }
        public static Lazy<BT2Memory> MemoryData = new Lazy<BT2Memory>();
        public static BT2Item Item(BT2ItemIndex index) { return Global.BT2.GetDirectItem((int)index) as BT2Item; }
        public static BT2Memory Memory { get { return MemoryData.Value; } }
        public static BT2Locations Spots { get { return SpotsBT2.Value; } }
        public static Lazy<BT2ItemList> ItemList = new Lazy<BT2ItemList>();
        public static Lazy<BT2MonsterList> MonsterList = new Lazy<BT2MonsterList>();
        public static List<BTItem> Items { get { return ItemList.Value.Items; } }
        public static List<BTMonster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static string SpellName(int index) { return index >= 0 && index < BT2.Spells.Count ? BT2.Spells[index].Abbreviation : "<Unknown>"; }
    }

    public static class BT3
    {
        public static Lazy<BT3SpellList> SpellList = new Lazy<BT3SpellList>();
        public static Lazy<BT3Locations> SpotsBT3 = new Lazy<BT3Locations>();
        public static BT3CharacterOffsets Offsets = new BT3CharacterOffsets();
        public static List<BT3Spell> Spells { get { return SpellList.Value.Spells; } }
        public static Lazy<BT3Memory> MemoryData = new Lazy<BT3Memory>();
        public static BT3Item Item(BT3ItemIndex index) { return Global.BT3.GetDirectItem((int)index) as BT3Item; }
        public static string ItemName(BT3ItemIndex index) { BT3Item item = Item(index); return item == null ? "<invalid>" : item.Name; }
        public static BT3Memory Memory { get { return MemoryData.Value; } }
        public static BT3Locations Spots { get { return SpotsBT3.Value; } }
        public static Lazy<BT3ItemList> ItemList = new Lazy<BT3ItemList>();
        public static Lazy<BT3MonsterList> MonsterList = new Lazy<BT3MonsterList>();
        public static List<BTItem> Items { get { return ItemList.Value.Items; } }
        public static List<BTMonster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static string SpellName(int index) { return index >= 0 && index < BT3.Spells.Count ? BT3.Spells[index].Abbreviation : "<Unknown>"; }
    }

    public interface BTGameGlobals : GameGlobals
    {
        List<BTItem> GetItems();
        List<BTMonster> GetMonsters();
        BTItem GetClonedItem(int index);
        BTItem GetDirectItem(int index);
        BTMonster GetDirectMonster(int index);
        BTMonster GetClonedMonster(int index);
    }

    public class BT1Globals : BTGameGlobals, GameGlobals
    {
        public void ResetItemList() { BT1.ItemList = new Lazy<BT1ItemList>(); }
        public void ResetMonsterList() { BT1.MonsterList = new Lazy<BT1MonsterList>(); }
        public Item GetItem(int index) { return index >= 0 && index < BT1.Items.Count ? BT1.Items[index] : null; }
        public List<BTItem> GetItems() { return BT1.Items; }
        public List<BTMonster> GetMonsters() { return BT1.Monsters; }
        public BTItem GetClonedItem(int index) { return index >= 0 && index < BT1.Items.Count ? BT1.Items[index].Clone() as BTItem : null; }
        public BTItem GetDirectItem(int index) { return index >= 0 && index < BT1.Items.Count ? BT1.Items[index] : null; }
        public string GetMonsterName(int index) { return BT1.MonsterName(index); }
        public void InitItemList(MemoryHacker hacker)
        {
            if (BT1.ItemList.Value.Internal)
                BT1.ItemList.Value.InitExternalList(hacker);
        }
        public void InitMonsterList(MemoryHacker hacker)
        {
            if (BT1.MonsterList.Value.Internal)
                BT1.MonsterList.Value.InitExternalList(hacker);
        }
        public BTMonster GetDirectMonster(int index) { return index >= 0 && index < BT1.Monsters.Count ? BT1.Monsters[index] : null; }
        public BTMonster GetClonedMonster(int index) { return index >= 0 && index < BT1.Monsters.Count ? BT1.Monsters[index].Clone() as BTMonster : null; }
        public List<Spell> GetAllSpells() { return new List<Spell>(BT1.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }

    public class BT2Globals : BTGameGlobals, GameGlobals
    {
        public void ResetItemList() { BT2.ItemList = new Lazy<BT2ItemList>(); }
        public void ResetMonsterList() { BT2.MonsterList = new Lazy<BT2MonsterList>(); }
        public Item GetItem(int index) { return index >= 0 && index < BT2.Items.Count ? BT2.Items[index] : null; }
        public List<BTItem> GetItems() { return BT2.Items; }
        public List<BTMonster> GetMonsters() { return null; }
        public BTItem GetClonedItem(int index) { return index >= 0 && index < BT2.Items.Count ? BT2.Items[index].Clone() as BTItem : null; }
        public BTItem GetDirectItem(int index) { return index >= 0 && index < BT2.Items.Count ? BT2.Items[index] : null; }
        public void InitItemList(MemoryHacker hacker)
        {
            if (BT2.ItemList.Value.Internal)
                BT2.ItemList.Value.InitExternalList(hacker);
        }
        public string GetMonsterName(int index) { return "Unknown"; }
        public void InitMonsterList(MemoryHacker hacker) { }  // List is internal only
        public BTMonster GetDirectMonster(int index) { return null; }
        public BTMonster GetClonedMonster(int index) { return null; }
        public List<Spell> GetAllSpells() { return new List<Spell>(BT2.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }

    public class BT3Globals : BTGameGlobals, GameGlobals
    {
        public void ResetItemList() { BT3.ItemList = new Lazy<BT3ItemList>(); }
        public void ResetMonsterList() { BT3.MonsterList = new Lazy<BT3MonsterList>(); }
        public Item GetItem(int index) { return index >= 0 && index < BT3.Items.Count ? BT3.Items[index] : null; }
        public List<BTItem> GetItems() { return BT3.Items; }
        public List<BTMonster> GetMonsters() { return null; }
        public BTItem GetClonedItem(int index) { return index >= 0 && index < BT3.Items.Count ? BT3.Items[index].Clone() as BTItem : null; }
        public BTItem GetDirectItem(int index) { return index >= 0 && index < BT3.Items.Count ? BT3.Items[index] : null; }
        public string GetMonsterName(int index) { return "Unknown"; }
        public void InitItemList(MemoryHacker hacker)
        {
            if (BT3.ItemList.Value.Internal)
                BT3.ItemList.Value.InitExternalList(hacker);
        }
        public void InitMonsterList(MemoryHacker hacker) { }  // List is internal only
        public BTMonster GetDirectMonster(int index) { return null; }
        public BTMonster GetClonedMonster(int index) { return null; }
        public List<Spell> GetAllSpells() { return new List<Spell>(BT3.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }

    public static class Ultima
    {
        public static UltimaCharacterOffsets Offsets = new UltimaCharacterOffsets();
    }

    public static class Ultima1
    {
        public static Lazy<Ultima1Locations> SpotsUltima1 = new Lazy<Ultima1Locations>();
        public static Lazy<UltimaSpellList> SpellList = new Lazy<UltimaSpellList>();
        public static Ultima1CharacterOffsets Offsets = new Ultima1CharacterOffsets();
        public static Lazy<Ultima1Memory> MemoryData = new Lazy<Ultima1Memory>();
        public static List<UltimaSpell> Spells { get { return SpellList.Value.Spells; } }
        public static Ultima1Memory Memory => MemoryData.Value;
        public static Ultima1Locations Spots => SpotsUltima1.Value;
        public static Lazy<Ultima1MonsterList> MonsterList = new Lazy<Ultima1MonsterList>();
        public static List<Monster> Monsters => new Ultima1MonsterList().Monsters;
        public static Lazy<Ultima1ItemList> ItemList = new Lazy<Ultima1ItemList>();
        public static List<UltimaItem> Items { get { return ItemList.Value.Items; } }
    }

    public static class EOB
    {
        public static EOBCharacterOffsets Offsets = new EOBCharacterOffsets();
    }

    public static class EOB1
    {
        public static Lazy<EOB1Walls> WallsEOB1 = new Lazy<EOB1Walls>();
        public static Lazy<EOB1SpellList> SpellList = new Lazy<EOB1SpellList>();
        public static Lazy<EOB1Locations> SpotsEOB1 = new Lazy<EOB1Locations>();
        public static EOB1CharacterOffsets Offsets = new EOB1CharacterOffsets();
        public static List<EOB1Spell> Spells { get { return SpellList.Value.Spells; } }
        public static Lazy<EOB1Memory> MemoryData = new Lazy<EOB1Memory>();
        public static EOB1Item Item(EOBItemIndex index) { return Global.EOB1.GetDirectItem((int)index) as EOB1Item; }
        public static string ItemName(EOBItemIndex index) { EOB1Item item = Item(index); return item == null ? "<invalid>" : item.Name; }
        public static EOB1Memory Memory { get { return MemoryData.Value; } }
        public static EOB1Locations Spots { get { return SpotsEOB1.Value; } }
        public static Lazy<EOB1ItemList> ItemList = new Lazy<EOB1ItemList>();
        public static Lazy<EOB1MonsterList> MonsterList = new Lazy<EOB1MonsterList>();
        public static List<EOBItem> Items { get { return ItemList.Value.Items; } }
        public static List<EOBMonster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static string SpellName(int index) { return index >= 0 && index < EOB1.Spells.Count ? EOB1.Spells[index].Name: "<Unknown>"; }
        public static EOB1Monster CloneMonster(int index) { return index < 0 || index >= Monsters.Count ? null : Monsters[index].Clone() as EOB1Monster; }
    }

    public static class EOB2
    {
        public static Lazy<EOB2Walls> WallsEOB2 = new Lazy<EOB2Walls>();
        public static Lazy<EOB2SpellList> SpellList = new Lazy<EOB2SpellList>();
        public static Lazy<EOB2Locations> SpotsEOB2 = new Lazy<EOB2Locations>();
        public static EOB2CharacterOffsets Offsets = new EOB2CharacterOffsets();
        public static List<EOB2Spell> Spells { get { return SpellList.Value.Spells; } }
        public static Lazy<EOB2Memory> MemoryData = new Lazy<EOB2Memory>();
        public static EOB2Item Item(EOBItemIndex index) { return Global.EOB2.GetDirectItem((int)index) as EOB2Item; }
        public static string ItemName(EOBItemIndex index) { EOB2Item item = Item(index); return item == null ? "<invalid>" : item.Name; }
        public static EOB2Memory Memory { get { return MemoryData.Value; } }
        public static EOB2Locations Spots { get { return SpotsEOB2.Value; } }
        public static Lazy<EOB2ItemList> ItemList = new Lazy<EOB2ItemList>();
        public static Lazy<EOB2MonsterList> MonsterList = new Lazy<EOB2MonsterList>();
        public static List<EOBItem> Items { get { return ItemList.Value.Items; } }
        public static List<EOBMonster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static string SpellName(int index) { return index >= 0 && index < EOB2.Spells.Count ? EOB2.Spells[index].Name : "<Unknown>"; }
        public static EOB2Monster CloneMonster(int index) { return index < 0 || index >= Monsters.Count ? null : Monsters[index].Clone() as EOB2Monster; }
    }

    public interface UltimaGameGlobals : GameGlobals
    {
    }

    public class Ultima1Globals : UltimaGameGlobals, GameGlobals
    {
        public string GetMonsterName(int index) => "Unknown";
        public Item GetItem(int index) => new UltimaItem((UltimaItemIndex)index, 1);
        public void InitItemList(MemoryHacker hacker) { }
        public void InitMonsterList(MemoryHacker hacker) { }
        public void ResetItemList() { }
        public void ResetMonsterList() { }
        public List<Spell> GetAllSpells() { return new List<Spell>(); }
        public BasicWall GetWall(int iMap, int iIndex) => BasicWall.Solid;
    }

    public interface EOBGameGlobals : GameGlobals
    {
        List<EOBItem> GetItems();
        List<EOBMonster> GetMonsters();
        EOBItem GetClonedItem(int index);
        EOBItem GetDirectItem(int index);
        EOBMonster GetDirectMonster(int index);
        EOBMonster GetClonedMonster(int index);
    }

    public class EOB1Globals : EOBGameGlobals, GameGlobals
    {
        public void ResetItemList() { EOB1.ItemList = new Lazy<EOB1ItemList>(); }
        public void ResetMonsterList() { EOB1.MonsterList = new Lazy<EOB1MonsterList>(); }
        public Item GetItem(int index) { return index >= 0 && index < EOB1.Items.Count ? EOB1.Items[index] : null; }
        public List<EOBItem> GetItems() { return EOB1.Items; }
        public List<EOBMonster> GetMonsters() { return null; }
        public EOBItem GetClonedItem(int index) { return index >= 0 && index < EOB1.Items.Count ? EOB1.Items[index].Clone() as EOBItem : null; }
        public EOBItem GetDirectItem(int index) { return index >= 0 && index < EOB1.Items.Count ? EOB1.Items[index] : null; }
        public string GetMonsterName(int index) { return "Unknown"; }
        public void InitItemList(MemoryHacker hacker)
        {
            if (EOB1.ItemList.Value.Internal)
                EOB1.ItemList.Value.InitExternalList(hacker);
        }
        public void InitMonsterList(MemoryHacker hacker) { }  // List is internal only
        public EOBMonster GetDirectMonster(int index) { return null; }
        public EOBMonster GetClonedMonster(int index) { return null; }
        public List<Spell> GetAllSpells() { return new List<Spell>(EOB1.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return EOB1.WallsEOB1.Value.GetWall(iMap, iIndex); }
    }

    public class EOB2Globals : EOBGameGlobals, GameGlobals
    {
        public void ResetItemList() { EOB2.ItemList = new Lazy<EOB2ItemList>(); }
        public void ResetMonsterList() { EOB2.MonsterList = new Lazy<EOB2MonsterList>(); }
        public Item GetItem(int index) { return index >= 0 && index < EOB2.Items.Count ? EOB2.Items[index] : null; }
        public List<EOBItem> GetItems() { return EOB2.Items; }
        public List<EOBMonster> GetMonsters() { return null; }
        public EOBItem GetClonedItem(int index) { return index >= 0 && index < EOB2.Items.Count ? EOB2.Items[index].Clone() as EOBItem : null; }
        public EOBItem GetDirectItem(int index) { return index >= 0 && index < EOB2.Items.Count ? EOB2.Items[index] : null; }
        public string GetMonsterName(int index) { return "Unknown"; }
        public void InitItemList(MemoryHacker hacker)
        {
            if (EOB2.ItemList.Value.Internal)
                EOB2.ItemList.Value.InitExternalList(hacker);
        }
        public void InitMonsterList(MemoryHacker hacker) { }  // List is internal only
        public EOBMonster GetDirectMonster(int index) { return null; }
        public EOBMonster GetClonedMonster(int index) { return null; }
        public List<Spell> GetAllSpells() { return new List<Spell>(EOB2.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return EOB2.WallsEOB2.Value.GetWall(iMap, iIndex); }
    }

    public static class Wiz1
    {
        public static Lazy<Wiz1Locations> SpotsWiz1 = new Lazy<Wiz1Locations>();
        public static Lazy<Wiz1ItemList> ItemList = new Lazy<Wiz1ItemList>();
        public static Lazy<Wiz1MonsterList> MonsterList = new Lazy<Wiz1MonsterList>();
        public static Lazy<Wiz1TreasureList> TreasureList = new Lazy<Wiz1TreasureList>();
        public static Lazy<Wiz1EncounterData> EncounterData = new Lazy<Wiz1EncounterData>();
        public static Lazy<Wiz1Memory> MemoryData = new Lazy<Wiz1Memory>();
        public static List<WizMonster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static List<WizTreasure> Treasures { get { return TreasureList.Value.Treasures; } }
        public static List<WizItem> Items { get { return ItemList.Value.Items; } }
        public static Wiz1Locations Spots { get { return SpotsWiz1.Value; } }
        public static Wiz1EncounterData Encounters { get { return EncounterData.Value; } }
        public static Wiz1Memory Memory { get { return MemoryData.Value; } }
        public static string MonsterName(int index) { return index >= 0 && index < Monsters.Count ? Monsters[index].ProperName : "<Unknown>"; }
        public static string ItemName(int index) { return index >= 0 && index < Items.Count ? Items[index].Name : "<Unknown>"; }
    }

    public static class Wiz2
    {
        public static Lazy<Wiz2Locations> SpotsWiz2 = new Lazy<Wiz2Locations>();
        public static Lazy<Wiz2ItemList> ItemList = new Lazy<Wiz2ItemList>();
        public static Lazy<Wiz2MonsterList> MonsterList = new Lazy<Wiz2MonsterList>();
        public static Lazy<Wiz2TreasureList> TreasureList = new Lazy<Wiz2TreasureList>();
        public static Lazy<Wiz2EncounterData> EncounterData = new Lazy<Wiz2EncounterData>();
        public static Lazy<Wiz2Memory> MemoryData = new Lazy<Wiz2Memory>();
        public static List<WizMonster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static List<WizTreasure> Treasures { get { return TreasureList.Value.Treasures; } }
        public static List<WizItem> Items { get { return ItemList.Value.Items; } }
        public static Wiz2Locations Spots { get { return SpotsWiz2.Value; } }
        public static Wiz2EncounterData Encounters { get { return EncounterData.Value; } }
        public static Wiz2Memory Memory { get { return MemoryData.Value; } }
        public static string MonsterName(int index) { return index >= 0 && index < Monsters.Count ? Monsters[index].ProperName : "<Unknown>"; }
        public static string ItemName(int index) { return index >= 0 && index < Items.Count ? Items[index].Name : "<Unknown>"; }
    }

    public static class Wiz3
    {
        public static Lazy<Wiz3Locations> SpotsWiz3 = new Lazy<Wiz3Locations>();
        public static Lazy<Wiz3ItemList> ItemList = new Lazy<Wiz3ItemList>();
        public static Lazy<Wiz3MonsterList> MonsterList = new Lazy<Wiz3MonsterList>();
        public static Lazy<Wiz3TreasureList> TreasureList = new Lazy<Wiz3TreasureList>();
        public static Lazy<Wiz3EncounterData> EncounterData = new Lazy<Wiz3EncounterData>();
        public static Lazy<Wiz3Memory> MemoryData = new Lazy<Wiz3Memory>();
        public static List<WizMonster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static List<WizTreasure> Treasures { get { return TreasureList.Value.Treasures; } }
        public static List<WizItem> Items { get { return ItemList.Value.Items; } }
        public static Wiz3Locations Spots { get { return SpotsWiz3.Value; } }
        public static Wiz3EncounterData Encounters { get { return EncounterData.Value; } }
        public static Wiz3Memory Memory { get { return MemoryData.Value; } }
        public static string MonsterName(int index) { return index >= 0 && index < Monsters.Count ? Monsters[index].ProperName : "<Unknown>"; }
        public static Wiz3Item CloneItem(int index)
        {
            if (index >= 0 && index < Items.Count)
                return Items[index].Clone() as Wiz3Item;
            if (index >= 1000 && index < 1000 + Items.Count)
                return Items[index - 1000].Clone() as Wiz3Item;
            return null;
        }
        public static string ItemName(int index)
        {
            if (index >= 0 && index < Items.Count)
                return Items[index].Name;
            if (index >= 1000 && index < 1000 + Items.Count)
                return Items[index - 1000].Name;
            return "<Unknown>";
        }
    }

    public static class Wiz4
    {
        public static Lazy<Wiz4Locations> SpotsWiz4 = new Lazy<Wiz4Locations>();
        public static Lazy<Wiz4ItemList> ItemList = new Lazy<Wiz4ItemList>();
        public static Lazy<Wiz4MonsterList> MonsterList = new Lazy<Wiz4MonsterList>();
        public static Lazy<Wiz4EncounterData> EncounterData = new Lazy<Wiz4EncounterData>();
        public static Lazy<Wiz4Memory> MemoryData = new Lazy<Wiz4Memory>();
        public static List<WizMonster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static List<WizItem> Items { get { return ItemList.Value.Items; } }
        public static Wiz4Locations Spots { get { return SpotsWiz4.Value; } }
        public static Wiz4EncounterData Encounters { get { return EncounterData.Value; } }
        public static Wiz4Memory Memory { get { return MemoryData.Value; } }
        public static string MonsterName(int index) { return index >= 0 && index < Monsters.Count ? Monsters[index].ProperName : "<Unknown>"; }
        public static Wiz4Item CloneItem(int index) { return index >= 0 && index < Items.Count ? Items[index].Clone() as Wiz4Item : null; }
        public static string ItemName(int index) { return index >= 0 && index < Items.Count ? Items[index].Name : "<Unknown>"; }
    }

    public static class Wiz5
    {
        public static Lazy<Wiz5SpellList> SpellList = new Lazy<Wiz5SpellList>();
        public static Wiz5CharacterOffsets Offsets = new Wiz5CharacterOffsets();
        public static Wiz123RaceModifiers Modifiers = new Wiz123RaceModifiers();
        public static List<Wiz5Spell> Spells { get { return SpellList.Value.Spells; } }
        public class Wiz123RaceModifiers : RaceClassModifiers
        {
            public Wiz123RaceModifiers()
            {
                Human = new Modifiers();
                Human.Adjust(ModAttr.SaveVsPoison, -1, "Race bonus (human)");
                Elf = new Modifiers();
                Elf.Adjust(ModAttr.SaveVsWand, -2, "Race bonus (elf)");
                Gnome = new Modifiers();
                Gnome.Adjust(ModAttr.SaveVsPetrify, -2, "Race bonus (gnome)");
                Dwarf = new Modifiers();
                Dwarf.Adjust(ModAttr.SaveVsBreath, -4, "Race bonus (dwarf)");
                Hobbit = new Modifiers();
                Hobbit.Adjust(ModAttr.SaveVsSpell, -3, "Race bonus (hobbit)");

                Fighter = new Modifiers();
                Fighter.Adjust(ModAttr.SaveVsPoison, -3, "Class bonus (Fighter)");
                Mage = new Modifiers();
                Mage.Adjust(ModAttr.SaveVsSpell, -3, "Class bonus (Mage)");
                Priest = new Modifiers();
                Priest.Adjust(ModAttr.SaveVsPetrify, -3, "Class bonus (Priest)");
                Thief = new Modifiers();
                Thief.Adjust(ModAttr.SaveVsBreath, -3, "Class bonus (Thief)");
                Bishop = new Modifiers();
                Bishop.Adjust(ModAttr.SaveVsPetrify, -2, "Class bonus (Bishop)");
                Bishop.Adjust(ModAttr.SaveVsBreath, -2, "Class bonus (Bishop)");
                Bishop.Adjust(ModAttr.SaveVsSpell, -2, "Class bonus (Bishop)");
                Samurai = new Modifiers();
                Samurai.Adjust(ModAttr.SaveVsPoison, -2, "Class bonus (Samurai)");
                Samurai.Adjust(ModAttr.SaveVsSpell, -2, "Class bonus (Samurai)");
                Lord = new Modifiers();
                Lord.Adjust(ModAttr.SaveVsPoison, -2, "Class bonus (Lord)");
                Lord.Adjust(ModAttr.SaveVsPetrify, -2, "Class bonus (Lord)");
                Ninja = new Modifiers();
                Ninja.Adjust(ModAttr.SaveVsPoison, -3, "Class bonus (Ninja)");
                Ninja.Adjust(ModAttr.SaveVsPetrify, -2, "Class bonus (Ninja)");
                Ninja.Adjust(ModAttr.SaveVsWand, -4, "Class bonus (Ninja)");
                Ninja.Adjust(ModAttr.SaveVsBreath, -3, "Class bonus (Ninja)");
                Ninja.Adjust(ModAttr.SaveVsSpell, -2, "Class bonus (Ninja)");
            }
        }

        public static Lazy<Wiz5Locations> SpotsWiz5 = new Lazy<Wiz5Locations>();
        public static Lazy<Wiz5ItemList> ItemList = new Lazy<Wiz5ItemList>();
        public static Lazy<Wiz5MonsterList> MonsterList = new Lazy<Wiz5MonsterList>();
        public static Lazy<Wiz5TreasureList> TreasureList = new Lazy<Wiz5TreasureList>();
        public static Lazy<Wiz5EncounterData> EncounterData = new Lazy<Wiz5EncounterData>();
        public static Lazy<Wiz5Memory> MemoryData = new Lazy<Wiz5Memory>();
        public static List<WizMonster> Monsters { get { return MonsterList.Value.Monsters; } }
        public static List<WizTreasure> Treasures { get { return TreasureList.Value.Treasures; } }
        public static List<WizItem> Items { get { return ItemList.Value.Items; } }
        public static Wiz5Locations Spots { get { return SpotsWiz5.Value; } }
        public static Wiz5EncounterData Encounters { get { return EncounterData.Value; } }
        public static Wiz5Memory Memory { get { return MemoryData.Value; } }
        public static string MonsterName(int index) { return index >= 0 && index < Monsters.Count ? Monsters[index].ProperName : "<Unknown>"; }
        public static string ItemName(int index) { return index >= 0 && index < Items.Count ? Items[index].Name : "<Unknown>"; }
    }

    public interface WizGameGlobals : GameGlobals
    {
        List<WizMonster> GetMonsters();
        List<WizTreasure> GetTreasures();
        List<WizItem> GetItems();
        WizMemory GetMemory();
        WizEncounterData GetEncounters();
        string GetItemName(int index);
        WizItem GetClonedItem(int index);
        WizItem GetDirectItem(int index);
        WizMonster GetInternalMonster(int index);
        string GetTreasureDescriptions();
        string GetTreasureDescription(int index);
    }

    public class Wiz1Globals : WizGameGlobals
    {
        public Item GetItem(int index) { return index >= 0 && index < Wiz1.Items.Count ? Wiz1.Items[index] : null; }
        public void ResetItemList() { Wiz1.ItemList = new Lazy<Wiz1ItemList>(); }
        public void ResetMonsterList() { Wiz1.MonsterList = new Lazy<Wiz1MonsterList>(); }
        public List<WizMonster> GetMonsters() { return Wiz1.Monsters; }
        public List<WizTreasure> GetTreasures() { return Wiz1.Treasures; }
        public List<WizItem> GetItems() { return Wiz1.Items; }
        public WizMemory GetMemory() { return Wiz1.Memory; }
        public WizEncounterData GetEncounters() { return Wiz1.Encounters; }
        public string GetMonsterName(int index) { return Wiz1.MonsterName(index); }
        public string GetItemName(int index) { return Wiz1.ItemName(index); }
        public WizItem GetClonedItem(int index) { return index >= 0 && index < Wiz1.Items.Count ? Wiz1.Items[index].Clone() as WizItem : null; }
        public WizItem GetDirectItem(int index) { return index >= 0 && index < Wiz1.Items.Count ? Wiz1.Items[index] : null; }
        public WizMonster GetInternalMonster(int index) { return index >= 0 && index < Wiz1.MonsterList.Value.InternalMonsters.Count ? Wiz1.MonsterList.Value.InternalMonsters[index] : null; }
        public string GetTreasureDescriptions() { return Wiz1.TreasureList.Value.GetFullDescriptions(); }
        public string GetTreasureDescription(int index) { return Wiz1.TreasureList.Value.GetFullDescription(index); }
        public void InitItemList(MemoryHacker hacker)
        {
            if (Wiz1.ItemList.Value.Internal)
                Wiz1.ItemList.Value.InitExternalList(hacker);
        }
        public void InitMonsterList(MemoryHacker hacker) { }  // List is internal only
        public List<Spell> GetAllSpells() { return new List<Spell>(Wiz123.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }

    public class Wiz2Globals : WizGameGlobals
    {
        public Item GetItem(int index) { return index >= 0 && index < Wiz2.Items.Count ? Wiz2.Items[index] : null; }
        public void ResetItemList() { Wiz2.ItemList = new Lazy<Wiz2ItemList>(); }
        public void ResetMonsterList() { Wiz2.MonsterList = new Lazy<Wiz2MonsterList>(); }
        public List<WizMonster> GetMonsters() { return Wiz2.Monsters; }
        public List<WizTreasure> GetTreasures() { return Wiz2.Treasures; }
        public List<WizItem> GetItems() { return Wiz2.Items; }
        public WizMemory GetMemory() { return Wiz2.Memory; }
        public WizEncounterData GetEncounters() { return Wiz2.Encounters; }
        public string GetMonsterName(int index) { return Wiz2.MonsterName(index); }
        public string GetItemName(int index) { return Wiz2.ItemName(index); }
        public WizItem GetClonedItem(int index) { return index >= 0 && index < Wiz2.Items.Count ? Wiz2.Items[index].Clone() as WizItem : null; }
        public WizItem GetDirectItem(int index) { return index >= 0 && index < Wiz2.Items.Count ? Wiz2.Items[index] : null; }
        public WizMonster GetInternalMonster(int index) { return index >= 0 && index < Wiz2.MonsterList.Value.InternalMonsters.Count ? Wiz2.MonsterList.Value.InternalMonsters[index] : null; }
        public string GetTreasureDescriptions() { return Wiz2.TreasureList.Value.GetFullDescriptions(); }
        public string GetTreasureDescription(int index) { return Wiz2.TreasureList.Value.GetFullDescription(index); }
        public void InitItemList(MemoryHacker hacker)
        {
            if (Wiz2.ItemList.Value.Internal)
                Wiz2.ItemList.Value.InitExternalList(hacker);
        }
        public void InitMonsterList(MemoryHacker hacker) { }  // List is internal only
        public List<Spell> GetAllSpells() { return new List<Spell>(Wiz123.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }

    public class Wiz3Globals : WizGameGlobals
    {
        public Item GetItem(int index) { return index >= 0 && index < Wiz3.Items.Count ? Wiz3.Items[index] : null; }
        public void ResetItemList() { Wiz3.ItemList = new Lazy<Wiz3ItemList>(); }
        public void ResetMonsterList() { Wiz3.MonsterList = new Lazy<Wiz3MonsterList>(); }
        public List<WizMonster> GetMonsters() { return Wiz3.Monsters; }
        public List<WizTreasure> GetTreasures() { return Wiz3.Treasures; }
        public List<WizItem> GetItems() { return Wiz3.Items; }
        public WizMemory GetMemory() { return Wiz3.Memory; }
        public WizEncounterData GetEncounters() { return Wiz3.Encounters; }
        public string GetMonsterName(int index) { return Wiz3.MonsterName(index); }
        public string GetItemName(int index) { return Wiz3.ItemName(index); }
        public WizMonster GetInternalMonster(int index) { return index >= 0 && index < Wiz3.MonsterList.Value.InternalMonsters.Count ? Wiz3.MonsterList.Value.InternalMonsters[index] : null; }
        public WizItem GetClonedItem(int index) { return Wiz3.CloneItem(index); }
        public string GetTreasureDescriptions() { return Wiz3.TreasureList.Value.GetFullDescriptions(); }
        public string GetTreasureDescription(int index) { return Wiz3.TreasureList.Value.GetFullDescription(index); }
        public WizItem GetDirectItem(int index)
        {
            if (index >= 0 && index < Wiz3.Items.Count)
                return Wiz3.Items[index];
            if (index >= 1000 && index < 1000 + Wiz3.Items.Count)
                return Wiz3.Items[index - 1000];
            return null;
        }
        public void InitItemList(MemoryHacker hacker)
        {
            if (Wiz3.ItemList.Value.Internal)
                Wiz3.ItemList.Value.InitExternalList(hacker);
        }
        public void InitMonsterList(MemoryHacker hacker) { }  // List is internal only
        public List<Spell> GetAllSpells() { return new List<Spell>(Wiz123.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }

    public class Wiz4Globals : WizGameGlobals
    {
        public Item GetItem(int index) { return index >= 0 && index < Wiz4.Items.Count ? Wiz4.Items[index] : null; }
        public void ResetItemList() { Wiz4.ItemList = new Lazy<Wiz4ItemList>(); }
        public void ResetMonsterList() { Wiz4.MonsterList = new Lazy<Wiz4MonsterList>(); }
        public List<WizMonster> GetMonsters() { return Wiz4.Monsters; }
        public List<WizTreasure> GetTreasures() { return null; }
        public List<WizItem> GetItems() { return Wiz4.Items; }
        public WizMemory GetMemory() { return Wiz4.Memory; }
        public WizEncounterData GetEncounters() { return Wiz4.Encounters; }
        public string GetMonsterName(int index) { return Wiz4.MonsterName(index); }
        public string GetItemName(int index) { return Wiz4.ItemName(index); }
        public WizItem GetClonedItem(int index) { return Wiz4.CloneItem(index); }
        public WizItem GetDirectItem(int index) { return index >= 0 && index < Wiz4.Items.Count ? Wiz4.Items[index] : null; }
        public WizMonster GetInternalMonster(int index) { return index >= 0 && index < Wiz4.MonsterList.Value.InternalMonsters.Count ? Wiz4.MonsterList.Value.InternalMonsters[index] : null; }
        public string GetTreasureDescriptions() { return String.Empty; }
        public string GetTreasureDescription(int index) { return String.Empty; }
        public void InitItemList(MemoryHacker hacker)
        {
            if (Wiz4.ItemList.Value.Internal)
                Wiz4.ItemList.Value.InitExternalList(hacker);
        }
        public void InitMonsterList(MemoryHacker hacker) { }  // List is internal only
        public List<Spell> GetAllSpells() { return new List<Spell>(Wiz123.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }

    public class Wiz5Globals : WizGameGlobals
    {
        public Item GetItem(int index) { return index >= 0 && index < Wiz5.Items.Count ? Wiz5.Items[index] : null; }
        public void ResetItemList() { Wiz5.ItemList = new Lazy<Wiz5ItemList>(); }
        public void ResetMonsterList() { Wiz5.MonsterList = new Lazy<Wiz5MonsterList>(); }
        public List<WizMonster> GetMonsters() { return Wiz5.Monsters; }
        public List<WizTreasure> GetTreasures() { return Wiz5.Treasures; }
        public List<WizItem> GetItems() { return Wiz5.Items; }
        public WizMemory GetMemory() { return Wiz5.Memory; }
        public WizEncounterData GetEncounters() { return Wiz5.Encounters; }
        public string GetMonsterName(int index) { return Wiz5.MonsterName(index); }
        public string GetItemName(int index) { return Wiz5.ItemName(index); }
        public WizItem GetClonedItem(int index) { return index >= 0 && index < Wiz5.Items.Count ? Wiz5.Items[index].Clone() as WizItem : null; }
        public WizItem GetDirectItem(int index) { return index >= 0 && index < Wiz5.Items.Count ? Wiz5.Items[index] : null; }
        public WizMonster GetInternalMonster(int index) { return index >= 0 && index < Wiz5.MonsterList.Value.InternalMonsters.Count ? Wiz5.MonsterList.Value.InternalMonsters[index] : null; }
        public string GetTreasureDescriptions() { return Wiz5.TreasureList.Value.GetFullDescriptions(); }
        public string GetTreasureDescription(int index) { return Wiz5.TreasureList.Value.GetFullDescription(index); }
        public void InitItemList(MemoryHacker hacker)
        {
            if (Wiz5.ItemList.Value.Internal)
                Wiz5.ItemList.Value.InitExternalList(hacker);
        }
        public void InitMonsterList(MemoryHacker hacker) { }  // List is internal only
        public List<Spell> GetAllSpells() { return new List<Spell>(Wiz5.Spells); }
        public BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }
}
