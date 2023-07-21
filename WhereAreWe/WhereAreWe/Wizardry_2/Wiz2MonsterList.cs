using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum Wiz2MonsterIndex
    {
        None = -1,
        Dink = 0,
        MajorDaimyo,
        Level7Priest,
        ChampSamurai,
        Level7Mage,
        Level6Thief,
        Werelion,
        OgreLord,
        KoboldKing,
        Webspinner,
        FuzzballA,
        WereAmoeba,
        NoSeeUm,
        Carrier,
        RhinoBeetle,
        Nightstalker,
        Hippopotamus,
        Level8Priest,
        Level10Fighter,
        Level8Mage,
        Level7Thief,
        Level8Ninja,
        EarthGiant,
        LesserDemon,
        Hellhound,
        FuzzballB,
        Doomtoad,
        RabidRat,
        WerePanther,
        BladeBear,
        OrcLord,
        Gorgon,
        Level11Bishop,
        Level12Fighter,
        Level10Mage,
        Thief,
        MasterNinja,
        MurphysGhost,
        WillOWisp,
        Bleeb,
        FuzzballC,
        Scryll,
        HellMaster,
        GiantWasp,
        GiantViper,
        AirGiant,
        FireDragon,
        HighPriest,
        HighWizard,
        MasterThief,
        Hatamoto,
        Vampire,
        GreaterDemon,
        GiantZombie,
        DragonZombie,
        FuzzballD,
        SmogBeast,
        GiantHornet,
        GiantCrab,
        Blob,
        ArchDemon,
        HighMaster,
        Flack,
        ArchMage,
        Maelific,
        VampireLord,
        Sidelle,
        GiantBat,
        WereBat,
        VampireBat,
        Constrictor,
        CorrosiveSlime,
        FoamingMold,
        MagicSword,
        MagicHelm,
        MagicShield,
        MagicGauntlet,
        MagicArmor,
        Last
    }

    public class Wiz2Monster : WizMonster
    {
        public override string GetName(int index) { return GetName((Wiz2MonsterIndex) index); }
        public override Monster Clone() { return new Wiz2Monster(Index, GetBytes(), 0); }

        public static string GetName(Wiz2MonsterIndex index)
        {
            switch (index)
            {
                case Wiz2MonsterIndex.Dink: return "Dink";
                case Wiz2MonsterIndex.MajorDaimyo: return "Major Daimyo";
                case Wiz2MonsterIndex.Level7Priest: return "Level 7 Priest";
                case Wiz2MonsterIndex.ChampSamurai: return "Champ Samurai";
                case Wiz2MonsterIndex.Level7Mage: return "Level 7 Mage";
                case Wiz2MonsterIndex.Level6Thief: return "Level 6 Thief";
                case Wiz2MonsterIndex.Werelion: return "Werelion";
                case Wiz2MonsterIndex.OgreLord: return "Ogre Lord";
                case Wiz2MonsterIndex.KoboldKing: return "Kobold King";
                case Wiz2MonsterIndex.Webspinner: return "Webspinner";
                case Wiz2MonsterIndex.FuzzballA: return "Fuzzball A";
                case Wiz2MonsterIndex.WereAmoeba: return "Were Amoeba";
                case Wiz2MonsterIndex.NoSeeUm: return "No-See-Um";
                case Wiz2MonsterIndex.Carrier: return "Carrier";
                case Wiz2MonsterIndex.RhinoBeetle: return "Rhino Beetle";
                case Wiz2MonsterIndex.Nightstalker: return "Nightstalker";
                case Wiz2MonsterIndex.Hippopotamus: return "Hippopotamus";
                case Wiz2MonsterIndex.Level8Priest: return "Level 8 Priest";
                case Wiz2MonsterIndex.Level10Fighter: return "Level 10 Fighter";
                case Wiz2MonsterIndex.Level8Mage: return "Level 8 Mage";
                case Wiz2MonsterIndex.Level7Thief: return "Level 7 Thief";
                case Wiz2MonsterIndex.Level8Ninja: return "Level 8 Ninja";
                case Wiz2MonsterIndex.EarthGiant: return "Earth Giant";
                case Wiz2MonsterIndex.LesserDemon: return "Lesser Demon";
                case Wiz2MonsterIndex.Hellhound: return "Hellhound";
                case Wiz2MonsterIndex.FuzzballB: return "Fuzzball B";
                case Wiz2MonsterIndex.Doomtoad: return "Doomtoad";
                case Wiz2MonsterIndex.RabidRat: return "Rabid Rat";
                case Wiz2MonsterIndex.WerePanther: return "Were Panther";
                case Wiz2MonsterIndex.BladeBear: return "Blade Bear";
                case Wiz2MonsterIndex.OrcLord: return "Orc Lord";
                case Wiz2MonsterIndex.Gorgon: return "Gorgon";
                case Wiz2MonsterIndex.Level11Bishop: return "Level 11 Bishop";
                case Wiz2MonsterIndex.Level12Fighter: return "Level 12 Fighter";
                case Wiz2MonsterIndex.Level10Mage: return "Level 10 Mage";
                case Wiz2MonsterIndex.Thief: return "Thief";
                case Wiz2MonsterIndex.MasterNinja: return "Master Ninja";
                case Wiz2MonsterIndex.MurphysGhost: return "Murphy's Ghost";
                case Wiz2MonsterIndex.WillOWisp: return "Will O' Wisp";
                case Wiz2MonsterIndex.Bleeb: return "Bleeb";
                case Wiz2MonsterIndex.FuzzballC: return "Fuzzball C";
                case Wiz2MonsterIndex.Scryll: return "Scryll";
                case Wiz2MonsterIndex.HellMaster: return "Hell Master";
                case Wiz2MonsterIndex.GiantWasp: return "Giant Wasp";
                case Wiz2MonsterIndex.GiantViper: return "Giant Viper";
                case Wiz2MonsterIndex.AirGiant: return "Air Giant";
                case Wiz2MonsterIndex.FireDragon: return "Fire Dragon";
                case Wiz2MonsterIndex.HighPriest: return "High Priest";
                case Wiz2MonsterIndex.HighWizard: return "High Wizard";
                case Wiz2MonsterIndex.MasterThief: return "Master Thief";
                case Wiz2MonsterIndex.Hatamoto: return "Hatamoto";
                case Wiz2MonsterIndex.Vampire: return "Vampire";
                case Wiz2MonsterIndex.GreaterDemon: return "Greater Demon";
                case Wiz2MonsterIndex.GiantZombie: return "Giant Zombie";
                case Wiz2MonsterIndex.DragonZombie: return "Dragon Zombie";
                case Wiz2MonsterIndex.FuzzballD: return "Fuzzball D";
                case Wiz2MonsterIndex.SmogBeast: return "Smog Beast";
                case Wiz2MonsterIndex.GiantHornet: return "Giant Hornet";
                case Wiz2MonsterIndex.GiantCrab: return "Giant Crab";
                case Wiz2MonsterIndex.Blob: return "Blob";
                case Wiz2MonsterIndex.ArchDemon: return "Arch Demon";
                case Wiz2MonsterIndex.HighMaster: return "High Master";
                case Wiz2MonsterIndex.Flack: return "Flack";
                case Wiz2MonsterIndex.ArchMage: return "Arch Mage";
                case Wiz2MonsterIndex.Maelific: return "Maelific";
                case Wiz2MonsterIndex.VampireLord: return "Vampire Lord";
                case Wiz2MonsterIndex.Sidelle: return "Sidelle";
                case Wiz2MonsterIndex.GiantBat: return "Giant Bat";
                case Wiz2MonsterIndex.WereBat: return "Were Bat";
                case Wiz2MonsterIndex.VampireBat: return "Vampire Bat";
                case Wiz2MonsterIndex.Constrictor: return "Constrictor";
                case Wiz2MonsterIndex.CorrosiveSlime: return "Corrosive Slime";
                case Wiz2MonsterIndex.FoamingMold: return "Foaming Mold";
                case Wiz2MonsterIndex.MagicSword: return "Magic Sword";
                case Wiz2MonsterIndex.MagicHelm: return "Magic Helm";
                case Wiz2MonsterIndex.MagicShield: return "Magic Shield";
                case Wiz2MonsterIndex.MagicGauntlet: return "Magic Gauntlet";
                case Wiz2MonsterIndex.MagicArmor: return "Magic Armor";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public Wiz2MonsterIndex WizIndex { get { return (Wiz2MonsterIndex)Index; } set { Index = (int)value; } }
        public Wiz2MonsterIndex GroupHelp { get { return (Wiz2MonsterIndex) Help; } set { Help = (int) value; } }
        public override int TreasureCount { get { return Wiz2.Treasures.Count; } }
        public override WizTreasure Treasure(int index) { return index < 0 || index >= Wiz2.Treasures.Count ? null : Wiz2.Treasures[index]; }

        public Wiz2Monster(int iIndex, byte[] bytes, int offset)
        {
            SetFromBytes(iIndex, GetName((Wiz2MonsterIndex)iIndex), bytes, offset);
        }
    }

    public class Wiz2MonsterList : Wiz123MonsterList
    {
        public override WizMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return new Wiz2Monster(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_2_Monster_List_mem); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes bytes = hacker.ReadOffset(Wiz2.Memory.MonsterListDisk, 7920);
            if (bytes == null)
                return null;
            return bytes.Bytes;
        }

        public Wiz2MonsterList()
        {
            InitInternalList();
        }
    }

    public class Wiz2EncounterInfo : WizEncounterInfo
    {
        public override WizMonster CreateMonster(int index, byte[] bytes, int offset) { return new Wiz2Monster(index, bytes, offset); }
        public override void CreateSearchResults(int iRewardIndex) { SearchResults = new Wiz2SearchResults(iRewardIndex); }

        public Wiz2EncounterInfo(WizGameState state, byte[] bytes, Point ptParty, int iRewardModifier, int offset = 0)
        {
            SetBytes(state, bytes, ptParty, iRewardModifier, offset);
        }
    }

    public class Wiz2EncounterData : WizEncounterData
    {
        public Wiz2EncounterData()
        {
            SetWizBytes(GameNames.Wizardry2, Global.DecompressBytes(Properties.Resources.Wizardry_2_Maps_mem));
        }

        public override bool IsMonsterEncounter(int iMapIndex, Point pt)
        {
            if (iMapIndex < 1 || iMapIndex >= Fights.Count)
                return false;
            return Fights[iMapIndex - 1][pt.X, pt.Y];
        }
    }

    public class Wiz2TreasureList : Wiz123TreasureList
    {
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_2_Treasure_List_mem); }
        public override string ItemName(int index) { return index < 0 || index >= Wiz2.Items.Count ? "Unknown" : Wiz2.Items[index].Name; }
        public override int TreasureCount { get { return Wiz2.Treasures.Count; } }
        public override WizTreasure Treasure(int index) { return index < 0 || index >= Wiz2.Treasures.Count ? null : Wiz2.Treasures[index]; }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes bytes = hacker.ReadOffset(Wiz2.Memory.TreasureList, 4096);
            if (bytes == null)
                return null;
            return bytes.Bytes;
        }

        public Wiz2TreasureList()
        {
            InitInternalList(GameNames.Wizardry2);
        }
    }
}
