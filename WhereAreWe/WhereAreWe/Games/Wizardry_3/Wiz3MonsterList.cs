using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum Wiz3MonsterIndex
    {
        None = -1,
        MoatMonster = 0,
        HighCorsair,
        GarianRaider,
        GarianGuard,
        GarianCaptain,
        GarianPriest,
        GarianMage,
        Poltergeist,
        Duster,
        GiantSlug,
        Anaconda,
        CrawlingKelp,
        MainatArms,
        Friar,
        Witch,
        Looter,
        Ronin,
        Asher,
        WereRat,
        Mummy,
        Vulture,
        BengalTiger,
        Goblin,
        Hobgoblin,
        LionofHell,
        Harpy,
        Ninja,
        Leprechaun,
        Nocorn,
        Crusader,
        Pixie,
        Centaur,
        DwarfFighter,
        Acolyte,
        Necromancer,
        DoomBeetle,
        StranglerVine,
        Banshee,
        PlagueFrog,
        KomodoDragon,
        GiantLeech,
        TwoHeadedSnake,
        GoblinPrince,
        GoblinShaman,
        DarkRider,
        StoneFly,
        MasterNinja,
        CrusaderLord,
        Faerie,
        Seraph,
        TienLung,
        Roc,
        Berserker,
        GnomePriest,
        ElvenMage,
        Burglar,
        Samurai,
        Ghost,
        Damned,
        Doppelganger,
        Fiend,
        KodiakBear,
        Ghast,
        Firedrake,
        VenusManTrap,
        Mifune,
        Archdemon,
        Cyclops,
        OgreKing,
        FateSpinner,
        Xeno,
        Hydra,
        PriestofFung,
        SoulTrapper,
        Delf,
        DelfsMinion,
        Pole,
        Lkbreth,
        Last
    }

    public class Wiz3Monster : WizMonster
    {
        public override string GetName(int index) { return GetName((Wiz3MonsterIndex) index); }
        public override Monster Clone() { return new Wiz3Monster(Index, GetBytes(), 0); }

        public static string GetName(Wiz3MonsterIndex index)
        {
            switch (index)
            {
                case Wiz3MonsterIndex.MoatMonster: return "Moat Monster";
                case Wiz3MonsterIndex.HighCorsair: return "High Corsair";
                case Wiz3MonsterIndex.GarianRaider: return "Garian Raider";
                case Wiz3MonsterIndex.GarianGuard: return "Garian Guard";
                case Wiz3MonsterIndex.GarianCaptain: return "Garian Captain";
                case Wiz3MonsterIndex.GarianPriest: return "Garian Priest";
                case Wiz3MonsterIndex.GarianMage: return "Garian Mage";
                case Wiz3MonsterIndex.Poltergeist: return "Poltergeist";
                case Wiz3MonsterIndex.Duster: return "Duster";
                case Wiz3MonsterIndex.GiantSlug: return "Giant Slug";
                case Wiz3MonsterIndex.Anaconda: return "Anaconda";
                case Wiz3MonsterIndex.CrawlingKelp: return "Crawling Kelp";
                case Wiz3MonsterIndex.MainatArms: return "Main at Arms";
                case Wiz3MonsterIndex.Friar: return "Friar";
                case Wiz3MonsterIndex.Witch: return "Witch";
                case Wiz3MonsterIndex.Looter: return "Looter";
                case Wiz3MonsterIndex.Ronin: return "Ronin";
                case Wiz3MonsterIndex.Asher: return "Asher";
                case Wiz3MonsterIndex.WereRat: return "Were Rat";
                case Wiz3MonsterIndex.Mummy: return "Mummy";
                case Wiz3MonsterIndex.Vulture: return "Vulture";
                case Wiz3MonsterIndex.BengalTiger: return "Bengal Tiger";
                case Wiz3MonsterIndex.Goblin: return "Goblin";
                case Wiz3MonsterIndex.Hobgoblin: return "Hobgoblin";
                case Wiz3MonsterIndex.LionofHell: return "Lion of Hell";
                case Wiz3MonsterIndex.Harpy: return "Harpy";
                case Wiz3MonsterIndex.Ninja: return "Ninja";
                case Wiz3MonsterIndex.Leprechaun: return "Leprechaun";
                case Wiz3MonsterIndex.Nocorn: return "Nocorn";
                case Wiz3MonsterIndex.Crusader: return "Crusader";
                case Wiz3MonsterIndex.Pixie: return "Pixie";
                case Wiz3MonsterIndex.Centaur: return "Centaur";
                case Wiz3MonsterIndex.DwarfFighter: return "Dwarf Fighter";
                case Wiz3MonsterIndex.Acolyte: return "Acolyte";
                case Wiz3MonsterIndex.Necromancer: return "Necromancer";
                case Wiz3MonsterIndex.DoomBeetle: return "Doom Beetle";
                case Wiz3MonsterIndex.StranglerVine: return "Strangler Vine";
                case Wiz3MonsterIndex.Banshee: return "Banshee";
                case Wiz3MonsterIndex.PlagueFrog: return "Plague Frog";
                case Wiz3MonsterIndex.KomodoDragon: return "Komodo Dragon";
                case Wiz3MonsterIndex.GiantLeech: return "Giant Leech";
                case Wiz3MonsterIndex.TwoHeadedSnake: return "2-Headed Snake";
                case Wiz3MonsterIndex.GoblinPrince: return "Goblin Prince";
                case Wiz3MonsterIndex.GoblinShaman: return "Goblin Shaman";
                case Wiz3MonsterIndex.DarkRider: return "Dark Rider";
                case Wiz3MonsterIndex.StoneFly: return "Stone Fly";
                case Wiz3MonsterIndex.MasterNinja: return "Master Ninja";
                case Wiz3MonsterIndex.CrusaderLord: return "Crusader Lord";
                case Wiz3MonsterIndex.Faerie: return "Faerie";
                case Wiz3MonsterIndex.Seraph: return "Seraph";
                case Wiz3MonsterIndex.TienLung: return "T'ien Lung";
                case Wiz3MonsterIndex.Roc: return "Roc";
                case Wiz3MonsterIndex.Berserker: return "Berserker";
                case Wiz3MonsterIndex.GnomePriest: return "Gnome Priest";
                case Wiz3MonsterIndex.ElvenMage: return "Elven Mage";
                case Wiz3MonsterIndex.Burglar: return "Burglar";
                case Wiz3MonsterIndex.Samurai: return "Samurai";
                case Wiz3MonsterIndex.Ghost: return "Ghost";
                case Wiz3MonsterIndex.Damned: return "Damned";
                case Wiz3MonsterIndex.Doppelganger: return "Doppelganger";
                case Wiz3MonsterIndex.Fiend: return "Fiend";
                case Wiz3MonsterIndex.KodiakBear: return "Kodiak Bear";
                case Wiz3MonsterIndex.Ghast: return "Ghast";
                case Wiz3MonsterIndex.Firedrake: return "Firedrake";
                case Wiz3MonsterIndex.VenusManTrap: return "Venus Man-Trap";
                case Wiz3MonsterIndex.Mifune: return "Mifune";
                case Wiz3MonsterIndex.Archdemon: return "Archdemon";
                case Wiz3MonsterIndex.Cyclops: return "Cyclops";
                case Wiz3MonsterIndex.OgreKing: return "Ogre King";
                case Wiz3MonsterIndex.FateSpinner: return "Fate Spinner";
                case Wiz3MonsterIndex.Xeno: return "Xeno";
                case Wiz3MonsterIndex.Hydra: return "Hydra";
                case Wiz3MonsterIndex.PriestofFung: return "Priest of Fung";
                case Wiz3MonsterIndex.SoulTrapper: return "Soul Trapper";
                case Wiz3MonsterIndex.Delf: return "Delf";
                case Wiz3MonsterIndex.DelfsMinion: return "Delf's Minion";
                case Wiz3MonsterIndex.Pole: return "Po'le";
                case Wiz3MonsterIndex.Lkbreth: return "L'kbreth";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public Wiz3MonsterIndex WizIndex { get { return (Wiz3MonsterIndex)Index; } set { Index = (int)value; } }
        public Wiz3MonsterIndex GroupHelp { get { return (Wiz3MonsterIndex) Help; } set { Help = (int) value; } }
        public override int TreasureCount { get { return Wiz3.Treasures.Count; } }
        public override WizTreasure Treasure(int index) { return index < 0 || index >= Wiz3.Treasures.Count ? null : Wiz3.Treasures[index]; }

        public Wiz3Monster(int iIndex, byte[] bytes, int offset)
        {
            SetFromBytes(iIndex, GetName((Wiz3MonsterIndex)iIndex), bytes, offset);
        }
    }

    public class Wiz3MonsterList : Wiz123MonsterList
    {
        public override WizMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return new Wiz3Monster(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_3_Monster_List_mem); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes bytes = hacker.ReadOffset(Wiz3.Memory.MonsterListDisk, 7920);
            if (bytes == null)
                return null;
            return bytes.Bytes;
        }

        public Wiz3MonsterList()
        {
            InitInternalList();
        }
    }

    public class Wiz3EncounterInfo : WizEncounterInfo
    {
        public override WizMonster CreateMonster(int index, byte[] bytes, int offset) { return new Wiz3Monster(index, bytes, offset); }
        public override void CreateSearchResults(int iRewardIndex) { SearchResults = new Wiz3SearchResults(iRewardIndex); }

        public Wiz3EncounterInfo(WizGameState state, byte[] bytes, Point ptParty, int iRewardModifier, int offset = 0)
        {
            SetBytes(state, bytes, ptParty, iRewardModifier, offset);
        }
    }

    public class Wiz3EncounterData : WizEncounterData
    {
        public Wiz3EncounterData()
        {
            SetWizBytes(GameNames.Wizardry3, Global.DecompressBytes(Properties.Resources.Wizardry_3_Maps_mem));
        }

        public override bool IsMonsterEncounter(int iMapIndex, Point pt)
        {
            if (iMapIndex < 1 || iMapIndex >= Fights.Count)
                return false;
            return Fights[iMapIndex - 1][pt.X, pt.Y];
        }
    }

    public class Wiz3TreasureList : Wiz123TreasureList
    {
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_3_Treasure_List_mem); }
        public override string ItemName(int index) { return index < 0 || index >= Wiz3.Items.Count ? "Unknown" : Wiz3.Items[index].Name; }
        public override int TreasureCount { get { return Wiz3.Treasures.Count; } }
        public override WizTreasure Treasure(int index) { return index < 0 || index >= Wiz3.Treasures.Count ? null : Wiz3.Treasures[index]; }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes bytes = hacker.ReadOffset(Wiz3.Memory.TreasureList, 4096);
            if (bytes == null)
                return null;
            return bytes.Bytes;
        }

        public Wiz3TreasureList()
        {
            InitInternalList(GameNames.Wizardry3);
        }
    }
}
