using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum Wiz1MonsterIndex
    {
        None = -1,
        BubblySlime = 0,
        Orc,
        Kobold,
        UndeadKobold,
        Rogue,
        Bushwacker,
        Highwayman,
        Zombie,
        CreepingCrud,
        GasCloud,
        Level1Mage,
        Level1Priest,
        CreepingCoin,
        Level1Ninja,
        VorpalBunny,
        Capybara,
        GiantToad,
        Coyote,
        Level3Priest,
        Level3Samurai,
        Level3Ninja,
        WereBear,
        DragonFly,
        RottingCorpse,
        Ogre,
        HugeSpider1,
        WereRat,
        BoringBeetle,
        GasDragon,
        Priestess,
        Swordsman,
        HugeSpider2,
        AttackDog,
        Gargoyle,
        GraveMist,
        DragonPuppy,
        WereWolf,
        Shade,
        Bishop,
        MinorDaimyo,
        Level5Mage,
        Level4Thief,
        KillerWolf,
        Spirit,
        GiantSpider,
        WereTiger,
        Medusalizard,
        Level5Priest,
        Level6Ninja,
        Level7Mage1,
        MasterThief1,
        MajorDaimyo,
        HighPriest1,
        ChampSamurai,
        ArchMage1,
        MasterThief2,
        GazeHound,
        OgreLord,
        Troll,
        Lifestealer,
        Nightstalker,
        Wyvern,
        Level8Priest,
        Level10Fighter,
        Level7Mage2,
        Level7Thief,
        Level8Ninja,
        EarthGiant,
        LesserDemon,
        Chimera,
        FireGiant,
        Gorgon,
        Level8Bishop,
        Level8Fighter,
        Level10Mage,
        Thief,
        MasterNinja,
        MurphysGhost,
        WillOWisp,
        Bleeb,
        FrostGiant,
        FireDragon,
        HighPriest2,
        HighWizard,
        MasterThief3,
        Hatamoto,
        Vampire,
        GreaterDemon,
        PoisonGiant,
        DragonZombie,
        RaverLord,
        HighMaster,
        Flack,
        ArchMage2,
        Maelific,
        VampireLord,
        WERDNA,
        HighNinja,
        HighPriest3,
        Level7Mage3,
        Level7Fighter,
        Last
    }

    public class Wiz1Monster : WizMonster
    {
        public override string GetName(int index) { return GetName((Wiz1MonsterIndex) index); }
        public override Monster Clone() { return new Wiz1Monster(Index, GetBytes(), 0); }

        public static string GetName(Wiz1MonsterIndex index)
        {
            switch (index)
            {
                case Wiz1MonsterIndex.BubblySlime: return "Bubbly Slime";
                case Wiz1MonsterIndex.Orc: return "Orc";
                case Wiz1MonsterIndex.Kobold: return "Kobold";
                case Wiz1MonsterIndex.UndeadKobold: return "Undead Kobold";
                case Wiz1MonsterIndex.Rogue: return "Rogue";
                case Wiz1MonsterIndex.Bushwacker: return "Bushwacker";
                case Wiz1MonsterIndex.Highwayman: return "Highwayman";
                case Wiz1MonsterIndex.Zombie: return "Zombie";
                case Wiz1MonsterIndex.CreepingCrud: return "Creeping Crud";
                case Wiz1MonsterIndex.GasCloud: return "Gas Cloud";
                case Wiz1MonsterIndex.Level1Mage: return "Level 1 Mage";
                case Wiz1MonsterIndex.Level1Priest: return "Level 1 Priest";
                case Wiz1MonsterIndex.CreepingCoin: return "Creeping Coin?";
                case Wiz1MonsterIndex.Level1Ninja: return "Level 1 Ninja";
                case Wiz1MonsterIndex.VorpalBunny: return "Vorpal Bunny";
                case Wiz1MonsterIndex.Capybara: return "Capybara";
                case Wiz1MonsterIndex.GiantToad: return "Giant Toad";
                case Wiz1MonsterIndex.Coyote: return "Coyote";
                case Wiz1MonsterIndex.Level3Priest: return "Priest";
                case Wiz1MonsterIndex.Level3Samurai: return "Level 3 Samurai";
                case Wiz1MonsterIndex.Level3Ninja: return "Level 3 Ninja";
                case Wiz1MonsterIndex.WereBear: return "Were Bear";
                case Wiz1MonsterIndex.DragonFly: return "Dragon Fly";
                case Wiz1MonsterIndex.RottingCorpse: return "Rotting Corpse";
                case Wiz1MonsterIndex.Ogre: return "Ogre";
                case Wiz1MonsterIndex.HugeSpider1: return "Huge Spider A";
                case Wiz1MonsterIndex.WereRat: return "Were Rat";
                case Wiz1MonsterIndex.BoringBeetle: return "Boring Beetle";
                case Wiz1MonsterIndex.GasDragon: return "Gas Dragon";
                case Wiz1MonsterIndex.Priestess: return "Priestess";
                case Wiz1MonsterIndex.Swordsman: return "Swordsman";
                case Wiz1MonsterIndex.HugeSpider2: return "Huge Spider B";
                case Wiz1MonsterIndex.AttackDog: return "Attack Dog";
                case Wiz1MonsterIndex.Gargoyle: return "Gargoyle";
                case Wiz1MonsterIndex.GraveMist: return "Grave Mist";
                case Wiz1MonsterIndex.DragonPuppy: return "Dragon Puppy";
                case Wiz1MonsterIndex.WereWolf: return "Were Wolf";
                case Wiz1MonsterIndex.Shade: return "Shade";
                case Wiz1MonsterIndex.Bishop: return "Bishop";
                case Wiz1MonsterIndex.MinorDaimyo: return "Minor Daimyo";
                case Wiz1MonsterIndex.Level5Mage: return "Level 5 Mage";
                case Wiz1MonsterIndex.Level4Thief: return "Level 4 Thief";
                case Wiz1MonsterIndex.KillerWolf: return "Killer Wolf";
                case Wiz1MonsterIndex.Spirit: return "Spirit";
                case Wiz1MonsterIndex.GiantSpider: return "Giant Spider";
                case Wiz1MonsterIndex.WereTiger: return "Were Tiger";
                case Wiz1MonsterIndex.Medusalizard: return "Medusalizard";
                case Wiz1MonsterIndex.Level5Priest: return "Level 5 Priest";
                case Wiz1MonsterIndex.Level6Ninja: return "Level 6 Ninja";
                case Wiz1MonsterIndex.Level7Mage1: return "Level 7 Mage A";
                case Wiz1MonsterIndex.MasterThief1: return "Master Thief A";
                case Wiz1MonsterIndex.MajorDaimyo: return "Major Daimyo";
                case Wiz1MonsterIndex.HighPriest1: return "High Priest A";
                case Wiz1MonsterIndex.ChampSamurai: return "Champ Samurai";
                case Wiz1MonsterIndex.ArchMage1: return "Arch Mage A";
                case Wiz1MonsterIndex.MasterThief2: return "Master Thief B";
                case Wiz1MonsterIndex.GazeHound: return "Gaze Hound";
                case Wiz1MonsterIndex.OgreLord: return "Ogre Lord";
                case Wiz1MonsterIndex.Troll: return "Troll";
                case Wiz1MonsterIndex.Lifestealer: return "Lifestealer";
                case Wiz1MonsterIndex.Nightstalker: return "Nightstalker";
                case Wiz1MonsterIndex.Wyvern: return "Wyvern";
                case Wiz1MonsterIndex.Level8Priest: return "Level 8 Priest";
                case Wiz1MonsterIndex.Level10Fighter: return "Level 10 Fighter";
                case Wiz1MonsterIndex.Level7Mage2: return "Level 7 Mage B";
                case Wiz1MonsterIndex.Level7Thief: return "Level 7 Thief";
                case Wiz1MonsterIndex.Level8Ninja: return "Level 8 Ninja";
                case Wiz1MonsterIndex.EarthGiant: return "Earth Giant";
                case Wiz1MonsterIndex.LesserDemon: return "Lesser Demon";
                case Wiz1MonsterIndex.Chimera: return "Chimera";
                case Wiz1MonsterIndex.FireGiant: return "Fire Giant";
                case Wiz1MonsterIndex.Gorgon: return "Gorgon";
                case Wiz1MonsterIndex.Level8Bishop: return "Level 8 Bishop";
                case Wiz1MonsterIndex.Level8Fighter: return "Level 8 Fighter";
                case Wiz1MonsterIndex.Level10Mage: return "Level 10 Mage";
                case Wiz1MonsterIndex.Thief: return "Thief";
                case Wiz1MonsterIndex.MasterNinja: return "Master Ninja";
                case Wiz1MonsterIndex.MurphysGhost: return "Murphy's Ghost";
                case Wiz1MonsterIndex.WillOWisp: return "Will O' Wisp";
                case Wiz1MonsterIndex.Bleeb: return "Bleeb";
                case Wiz1MonsterIndex.FrostGiant: return "Frost Giant";
                case Wiz1MonsterIndex.FireDragon: return "Fire Dragon";
                case Wiz1MonsterIndex.HighPriest2: return "High Priest B";
                case Wiz1MonsterIndex.HighWizard: return "High Wizard";
                case Wiz1MonsterIndex.MasterThief3: return "Master Thief C";
                case Wiz1MonsterIndex.Hatamoto: return "Hatamoto";
                case Wiz1MonsterIndex.Vampire: return "Vampire";
                case Wiz1MonsterIndex.GreaterDemon: return "Greater Demon";
                case Wiz1MonsterIndex.PoisonGiant: return "Poison Giant";
                case Wiz1MonsterIndex.DragonZombie: return "Dragon Zombie";
                case Wiz1MonsterIndex.RaverLord: return "Raver Lord";
                case Wiz1MonsterIndex.HighMaster: return "The High Master";
                case Wiz1MonsterIndex.Flack: return "Flack";
                case Wiz1MonsterIndex.ArchMage2: return "Arch Mage B";
                case Wiz1MonsterIndex.Maelific: return "Maelific";
                case Wiz1MonsterIndex.VampireLord: return "Vampire Lord";
                case Wiz1MonsterIndex.WERDNA: return "WERDNA";
                case Wiz1MonsterIndex.HighNinja: return "High Ninja";
                case Wiz1MonsterIndex.HighPriest3: return "High Priest C";
                case Wiz1MonsterIndex.Level7Mage3: return "Level 7 Mage C";
                case Wiz1MonsterIndex.Level7Fighter: return "Level 7 Fighter";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public Wiz1MonsterIndex WizIndex { get { return (Wiz1MonsterIndex)Index; } set { Index = (int)value; } }
        public Wiz1MonsterIndex GroupHelp { get { return (Wiz1MonsterIndex) Help; } set { Help = (int) value; } }
        public override int TreasureCount { get { return Wiz1.Treasures.Count; } }
        public override WizTreasure Treasure(int index) { return index < 0 || index >= Wiz1.Treasures.Count ? null : Wiz1.Treasures[index]; }

        public Wiz1Monster(int iIndex, byte[] bytes, int offset)
        {
            SetFromBytes(iIndex, GetName((Wiz1MonsterIndex)iIndex), bytes, offset);
        }
    }

    public class Wiz1MonsterList : Wiz123MonsterList
    {
        public override WizMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return new Wiz1Monster(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_1_Monster_List_mem); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes bytes = hacker.ReadOffset(Wiz1.Memory.MonsterListDisk, 10335);
            if (bytes == null)
                return null;
            return bytes.Bytes;
        }

        public Wiz1MonsterList()
        {
            InitInternalList();
        }
    }

    public class Wiz1EncounterInfo : WizEncounterInfo
    {
        public override WizMonster CreateMonster(int index, byte[] bytes, int offset) { return new Wiz1Monster(index, bytes, offset); }
        public override void CreateSearchResults(int iRewardIndex) { SearchResults = new Wiz1SearchResults(iRewardIndex); }

        public Wiz1EncounterInfo(WizGameState state, byte[] bytes,  Point ptParty, int iRewardModifier, int offset = 0)
        {
            SetBytes(state, bytes, ptParty, iRewardModifier, offset);
        }
    }

    public class WizEncounterData : EncounterData
    {
        public List<bool[,]> Fights;

        protected void SetWizBytes(GameNames game, byte[] bytes)
        {
            Fights = new List<bool[,]>(bytes.Length / 1024);
            for (int i = 0; i < bytes.Length / 1024; i++)
            {
                WizMapData map = new WizMapData(game, i + 1, bytes, i * 1024);
                if (i == 6)
                    map.Fights[2, 9] = true;    // Forced encounter with Fire Dragons
                Fights.Add(map.Fights);
            }
        }
    }

    public class Wiz1EncounterData : WizEncounterData
    {
        public Wiz1EncounterData()
        {
            SetWizBytes(GameNames.Wizardry1, Global.DecompressBytes(Properties.Resources.Wizardry_1_Maps_mem));
        }

        public override bool IsMonsterEncounter(int iMapIndex, Point pt)
        {
            if (iMapIndex < 1 || iMapIndex >= Fights.Count)
                return false;
            return Fights[iMapIndex - 1][pt.X, pt.Y];
        }
    }

    public class Wiz1TreasureList : Wiz123TreasureList
    {
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_1_Treasure_List_mem); }
        public override string ItemName(int index) { return index < 0 || index >= Wiz1.Items.Count ? "Unknown" : Wiz1.Items[index].Name; }
        public override int TreasureCount { get { return Wiz1.Treasures.Count; } }
        public override WizTreasure Treasure(int index) { return index < 0 || index >= Wiz1.Treasures.Count ? null : Wiz1.Treasures[index]; }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes bytes = hacker.ReadOffset(Wiz1.Memory.TreasureList, 4096);
            if (bytes == null)
                return null;
            return bytes.Bytes;
        }

        public Wiz1TreasureList()
        {
            InitInternalList(GameNames.Wizardry1);
        }
    }
}
