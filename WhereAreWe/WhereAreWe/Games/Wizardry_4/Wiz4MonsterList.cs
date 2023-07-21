using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum Wiz4MonsterIndex
    {
        None = -1,
        Dink = 0,
        Fuzzball,
        CreepingCoin,
        BubblySlime,
        Orc,
        Level1Mage,
        GasCloud,
        Skeleton,
        GarianRaider,
        Level1Priest,
        Zombie,
        Kobold,
        CreepingCrud,
        CrawlingKelp,
        Mummy,
        Witch,
        Poltergeist,
        NoSeeUm,
        Rogues,
        Asher,
        Anaconda,
        Duster,
        HugeSpider,
        Level3Priest,
        RottingCorpse,
        DragonFly,
        Spirit,
        Harpy,
        Bugbear,
        WereRat,
        Ronin,
        GazeHound,
        Banshee,
        Shade,
        Level5Priest,
        Looter,
        BlinkDog,
        Bushwacker,
        MoatMonster,
        StranglerVine,
        GiantToad,
        VorpalBunny,
        GiantSlug,
        GoblinShaman,
        Goblin,
        Cockatrice,
        Ogre,
        Priestess,
        Level3Samurai,
        GraveMist,
        HighCorsair,
        MinorDaimyo,
        Lifestealer,
        Nightstalker,
        Wight,
        MasterNinja,
        Bishop,
        Werewolf,
        Hobgoblin,
        Centaur,
        Gargoyle,
        Ghast,
        KomodoDragon,
        Hellhound,
        PriestOfFung,
        MastersDragons,
        Seraph,
        Weretiger,
        BoringBeetle,
        DisplacerBeast,
        CorrosiveSlime,
        GasDragon,
        Scryll,
        Carrier,
        Myrmidon,
        Gorgon,
        Level6Ninja,
        DarkRider,
        Doppelganger,
        GiantMantis,
        EvilEye,
        GoblinPrince,
        MastersWWind,
        Wyvern,
        BrassDragon,
        Fiend,
        WillOWisp,
        Berserker,
        Chimera,
        Xeno,
        Bleeb,
        Roc,
        MajorDaimyo,
        Troll,
        ChampSamurai,
        Vampire,
        MurphysGhost,
        Manticore,
        Lich,
        FrostGiant,
        FireGiant,
        Hatamoto,
        MastersSummer,
        Hydra,
        Succubus,
        Firedrake,
        DragonZombie,
        Cyclops,
        GreaterDemon,
        PoisonGiant,
        GoldDragon,
        Maelific,
        VampireLord,
        HighMaster,
        Lycurgus,
        BlackDragon,
        FoamingMold,
        IronGolem,
        Fleck,
        DemonLord,
        Last
    }

    public class Wiz4Monster : WizMonster
    {
        public const int SizeWiz4 = 76;

        public override string GetName(int index) { return GetName((Wiz4MonsterIndex) index); }
        public override Monster Clone() { return new Wiz4Monster(Index, GetBytes(), 0); }

        public static string GetName(Wiz4MonsterIndex index)
        {
            switch (index)
            {
                case Wiz4MonsterIndex.Dink: return "Dink";
                case Wiz4MonsterIndex.Fuzzball: return "Fuzzball";
                case Wiz4MonsterIndex.CreepingCoin: return "Creeping Coin";
                case Wiz4MonsterIndex.BubblySlime: return "Bubbly Slime";
                case Wiz4MonsterIndex.Orc: return "Orc";
                case Wiz4MonsterIndex.Level1Mage: return "Level 1 Mage";
                case Wiz4MonsterIndex.GasCloud: return "Gas Cloud";
                case Wiz4MonsterIndex.Skeleton: return "Skeleton";
                case Wiz4MonsterIndex.GarianRaider: return "Garian Raider";
                case Wiz4MonsterIndex.Level1Priest: return "Level 1 Priest";
                case Wiz4MonsterIndex.Zombie: return "Zombie";
                case Wiz4MonsterIndex.Kobold: return "Kobold";
                case Wiz4MonsterIndex.CreepingCrud: return "Creeping Crud";
                case Wiz4MonsterIndex.CrawlingKelp: return "Crawling Kelp";
                case Wiz4MonsterIndex.Mummy: return "Mummy";
                case Wiz4MonsterIndex.Witch: return "Witch";
                case Wiz4MonsterIndex.Poltergeist: return "Poltergeist";
                case Wiz4MonsterIndex.NoSeeUm: return "No-See-Um";
                case Wiz4MonsterIndex.Rogues: return "Rogues";
                case Wiz4MonsterIndex.Asher: return "Asher";
                case Wiz4MonsterIndex.Anaconda: return "Anaconda";
                case Wiz4MonsterIndex.Duster: return "Duster";
                case Wiz4MonsterIndex.HugeSpider: return "Huge Spider";
                case Wiz4MonsterIndex.Level3Priest: return "Level 3 Priest";
                case Wiz4MonsterIndex.RottingCorpse: return "Rotting Corpse";
                case Wiz4MonsterIndex.DragonFly: return "Dragon Fly";
                case Wiz4MonsterIndex.Spirit: return "Spirit";
                case Wiz4MonsterIndex.Harpy: return "Harpy";
                case Wiz4MonsterIndex.Bugbear: return "Bugbear";
                case Wiz4MonsterIndex.WereRat: return "WereRat";
                case Wiz4MonsterIndex.Ronin: return "Ronin";
                case Wiz4MonsterIndex.GazeHound: return "Gaze Hound";
                case Wiz4MonsterIndex.Banshee: return "Banshee";
                case Wiz4MonsterIndex.Shade: return "Shade";
                case Wiz4MonsterIndex.Level5Priest: return "Level 5 Priest";
                case Wiz4MonsterIndex.Looter: return "Looter";
                case Wiz4MonsterIndex.BlinkDog: return "Blink Dog";
                case Wiz4MonsterIndex.Bushwacker: return "Bushwacker";
                case Wiz4MonsterIndex.MoatMonster: return "Moat Monster";
                case Wiz4MonsterIndex.StranglerVine: return "Strangler Vine";
                case Wiz4MonsterIndex.GiantToad: return "Giant Toad";
                case Wiz4MonsterIndex.VorpalBunny: return "Vorpal Bunny";
                case Wiz4MonsterIndex.GiantSlug: return "Giant Slug";
                case Wiz4MonsterIndex.GoblinShaman: return "Goblin Shaman";
                case Wiz4MonsterIndex.Goblin: return "Goblin";
                case Wiz4MonsterIndex.Cockatrice: return "Cockatrice";
                case Wiz4MonsterIndex.Ogre: return "Ogre";
                case Wiz4MonsterIndex.Priestess: return "Priestess";
                case Wiz4MonsterIndex.Level3Samurai: return "Level 3 Samurai";
                case Wiz4MonsterIndex.GraveMist: return "Grave Mist";
                case Wiz4MonsterIndex.HighCorsair: return "High Corsair";
                case Wiz4MonsterIndex.MinorDaimyo: return "Minor Daimyo";
                case Wiz4MonsterIndex.Lifestealer: return "Lifestealer";
                case Wiz4MonsterIndex.Nightstalker: return "Nightstalker";
                case Wiz4MonsterIndex.Wight: return "Wight";
                case Wiz4MonsterIndex.MasterNinja: return "Master Ninja";
                case Wiz4MonsterIndex.Bishop: return "Bishop";
                case Wiz4MonsterIndex.Werewolf: return "Werewolf";
                case Wiz4MonsterIndex.Hobgoblin: return "Hobgoblin";
                case Wiz4MonsterIndex.Centaur: return "Centaur";
                case Wiz4MonsterIndex.Gargoyle: return "Gargoyle";
                case Wiz4MonsterIndex.Ghast: return "Ghast";
                case Wiz4MonsterIndex.KomodoDragon: return "Komodo Dragon";
                case Wiz4MonsterIndex.Hellhound: return "Hellhound";
                case Wiz4MonsterIndex.PriestOfFung: return "Priest of Fung";
                case Wiz4MonsterIndex.MastersDragons: return "Masters of Dragons";
                case Wiz4MonsterIndex.Seraph: return "Seraph";
                case Wiz4MonsterIndex.Weretiger: return "Weretiger";
                case Wiz4MonsterIndex.BoringBeetle: return "Boring Beetle";
                case Wiz4MonsterIndex.DisplacerBeast: return "Displacer Beast";
                case Wiz4MonsterIndex.CorrosiveSlime: return "Corrosive Slime";
                case Wiz4MonsterIndex.GasDragon: return "Gas Dragon";
                case Wiz4MonsterIndex.Scryll: return "Scryll";
                case Wiz4MonsterIndex.Carrier: return "Carrier";
                case Wiz4MonsterIndex.Myrmidon: return "Myrmidon";
                case Wiz4MonsterIndex.Gorgon: return "Gorgon";
                case Wiz4MonsterIndex.Level6Ninja: return "Level 6 Ninja";
                case Wiz4MonsterIndex.DarkRider: return "Dark Rider";
                case Wiz4MonsterIndex.Doppelganger: return "Doppelganger";
                case Wiz4MonsterIndex.GiantMantis: return "Giant Mantis";
                case Wiz4MonsterIndex.EvilEye: return "Evil Eye";
                case Wiz4MonsterIndex.GoblinPrince: return "Goblin Prince";
                case Wiz4MonsterIndex.MastersWWind: return "Masters of West Wind";
                case Wiz4MonsterIndex.Wyvern: return "Wyvern";
                case Wiz4MonsterIndex.BrassDragon: return "Brass Dragon";
                case Wiz4MonsterIndex.Fiend: return "Fiend";
                case Wiz4MonsterIndex.WillOWisp: return "Will O' Wisp";
                case Wiz4MonsterIndex.Berserker: return "Berserker";
                case Wiz4MonsterIndex.Chimera: return "Chimera";
                case Wiz4MonsterIndex.Xeno: return "Xeno";
                case Wiz4MonsterIndex.Bleeb: return "Bleeb";
                case Wiz4MonsterIndex.Roc: return "Roc";
                case Wiz4MonsterIndex.MajorDaimyo: return "Major Daimyo";
                case Wiz4MonsterIndex.Troll: return "Troll";
                case Wiz4MonsterIndex.ChampSamurai: return "Champ Samurai";
                case Wiz4MonsterIndex.Vampire: return "Vampire";
                case Wiz4MonsterIndex.MurphysGhost: return "Murphy's Ghost";
                case Wiz4MonsterIndex.Manticore: return "Manticore";
                case Wiz4MonsterIndex.Lich: return "Lich";
                case Wiz4MonsterIndex.FrostGiant: return "Frost Giant";
                case Wiz4MonsterIndex.FireGiant: return "Fire Giant";
                case Wiz4MonsterIndex.Hatamoto: return "Hatamoto";
                case Wiz4MonsterIndex.MastersSummer: return "Masters of Summer";
                case Wiz4MonsterIndex.Hydra: return "Hydra";
                case Wiz4MonsterIndex.Succubus: return "Succubus";
                case Wiz4MonsterIndex.Firedrake: return "Firedrake";
                case Wiz4MonsterIndex.DragonZombie: return "Dragon Zombie";
                case Wiz4MonsterIndex.Cyclops: return "Cyclops";
                case Wiz4MonsterIndex.GreaterDemon: return "Greater Demon";
                case Wiz4MonsterIndex.PoisonGiant: return "Poison Giant";
                case Wiz4MonsterIndex.GoldDragon: return "Gold Dragon";
                case Wiz4MonsterIndex.Maelific: return "Maelific";
                case Wiz4MonsterIndex.VampireLord: return "Vampire Lord";
                case Wiz4MonsterIndex.HighMaster: return "High Master";
                case Wiz4MonsterIndex.Lycurgus: return "Lycurgus";
                case Wiz4MonsterIndex.BlackDragon: return "Black Dragon";
                case Wiz4MonsterIndex.FoamingMold: return "Foaming Mold";
                case Wiz4MonsterIndex.IronGolem: return "Iron Golem";
                case Wiz4MonsterIndex.Fleck: return "Fleck";
                case Wiz4MonsterIndex.DemonLord: return "Demon Lord";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public Wiz4MonsterIndex WizIndex { get { return (Wiz4MonsterIndex)Index; } set { Index = (int)value; } }
        public Wiz4MonsterIndex GroupHelp { get { return (Wiz4MonsterIndex) Help; } set { Help = (int) value; } }

        public static class OffsetsWiz4
        {
            public static int NumAppearing = 0;
            public static int HitPoints = 6;
            public static int Family = 12;
            public static int ArmorClass = 14;
            public static int NumAttacks = 16;
            public static int Attack1 = 18;
            public static int Attack2 = 24;
            public static int Attack3 = 30;
            public static int Attack4 = 36;
            public static int Attack5 = 42;
            public static int Attack6 = 48;
            public static int Attack7 = 54;
            public static int Drain = 60;
            public static int Regeneration = 60;
            public static int GroupHelp = 60;
            public static int GroupHelpChance = 62;
            public static int MageSpellLevel = 64;
            public static int ClericSpellLevel = 66;
            public static int BreathWeapon = 68;
            public static int MagicResist = 70;
            public static int Resistances = 72;
            public static int Special = 74;
        }

        public void SetFromBytesWiz4(int iIndex, string strName, byte[] bytes, int offset)
        {
            Index = iIndex;
            Silenced = 0;
            ACModifier = 0;
            NumAppearing = DiceFromOffset(bytes, offset + OffsetsWiz4.NumAppearing);
            if (NumAppearing.Bonus < 0)
                NumAppearing.Bonus = 0;     // Sometimes this is -1, but Wizardry 4 seems to ignore that (e.g 1d8-1 still has a max of 8)
            HitPoints = DiceFromOffset(bytes, offset + OffsetsWiz4.HitPoints);
            Family = (WizMonsterFamily)BitConverter.ToInt16(bytes, offset + OffsetsWiz4.Family);
            AC = BitConverter.ToInt16(bytes, offset + OffsetsWiz4.ArmorClass);
            NumAttacks = BitConverter.ToInt16(bytes, offset + OffsetsWiz4.NumAttacks);
            Attacks = new List<DamageDice>();
            for (int i = OffsetsWiz4.Attack1; i <= OffsetsWiz4.Attack7; i += 6)
            {
                DamageDice dice = DiceFromOffset(bytes, offset + i);
                if (dice.Quantity > 0)
                    Attacks.Add(dice);
            }
            Drain = BitConverter.ToInt16(bytes, offset + OffsetsWiz4.Drain);
            Regeneration = BitConverter.ToInt16(bytes, offset + OffsetsWiz4.Regeneration);
            Help = BitConverter.ToInt16(bytes, offset + OffsetsWiz4.GroupHelp);
            GroupHelpChance = BitConverter.ToInt16(bytes, offset + OffsetsWiz4.GroupHelpChance);
            MageSpellLevel = BitConverter.ToInt16(bytes, offset + OffsetsWiz4.MageSpellLevel);
            PriestSpellLevel = BitConverter.ToInt16(bytes, offset + OffsetsWiz4.ClericSpellLevel);
            BreathWeapon = (WizBreath)BitConverter.ToInt16(bytes, offset + OffsetsWiz4.BreathWeapon);
            MagicResist = BitConverter.ToInt16(bytes, offset + OffsetsWiz4.MagicResist);
            Resistances = (WizResist)BitConverter.ToInt16(bytes, offset + OffsetsWiz4.Resistances);
            Properties = (WizMonsterProperty)BitConverter.ToInt16(bytes, offset + OffsetsWiz4.Special);
            Reward1 = -1;
            Reward2 = -1;
            RewardModifier = -1;
            Name = strName;
            Experience = 0;
        }

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            //Global.WriteInt16(ms, Image);
            WriteDice(ms, NumAppearing);
            WriteDice(ms, HitPoints);
            Global.WriteInt16(ms, (int)Family);
            Global.WriteInt16(ms, AC);
            Global.WriteInt16(ms, NumAttacks);
            for (int i = 0; i < 7; i++)
            {
                if (Attacks.Count > i)
                    WriteDice(ms, Attacks[i]);
                else
                    WriteDice(ms, DamageDice.Zero);
            }
            //byte[] bytesExp = WizardryLong.GetBytes(Experience);
            //ms.Write(bytesExp, 0, bytesExp.Length);
            //Global.WriteInt16(ms, Drain);
            //Global.WriteInt16(ms, Regeneration);
            Global.WriteInt16(ms, Help);
            Global.WriteInt16(ms, GroupHelpChance);
            Global.WriteInt16(ms, MageSpellLevel);
            Global.WriteInt16(ms, PriestSpellLevel);
            //Global.WriteInt16(ms, Unique);
            Global.WriteInt16(ms, (int)BreathWeapon);
            Global.WriteInt16(ms, MagicResist);
            Global.WriteInt16(ms, (int)Resistances);
            Global.WriteInt16(ms, (int)Properties);
            return ms.ToArray();
        }

        public Wiz4Monster(int iIndex, byte[] bytes, int offset)
        {
            SetFromBytesWiz4(iIndex, GetName((Wiz4MonsterIndex)iIndex), bytes, offset);
        }
    }

    public class Wiz4MonsterList : Wiz123MonsterList
    {
        public override WizMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return new Wiz4Monster(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_4_Monster_List_mem); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            // The Wizardry 4 monsters are compressed somewhere; use the internal list instead
            return GetInternalBytes();
        }

        public Wiz4MonsterList()
        {
            InitInternalList();
        }

        public override List<WizMonster> SetFromBytes(byte[] bytes)
        {
            List<WizMonster> monsters = new List<WizMonster>(bytes.Length / Wiz4Monster.SizeWiz4);

            try
            {
                // The wizardry 4 monsters are stored in 76-byte segments
                int iPos = 0;
                int iItemCount = 0;
                while (iPos <= bytes.Length - Wiz4Monster.SizeWiz4)
                {
                    monsters.Add(CreateMonster(iItemCount, bytes, iPos));
                    iPos += Wiz4Monster.SizeWiz4;
                    iItemCount++;
                }
                Valid = true;
            }
            catch (Exception)
            {
                Valid = false;
            }

            return monsters;
        }
    }

    public class Wiz4EncounterInfo : WizEncounterInfo
    {
        public bool IsFightingDoGooders;    // Wizardry 4 is always "in combat" but there is not always a party of Do-Gooders to fight

        public override WizMonster CreateMonster(int index, byte[] bytes, int offset) { return new Wiz4Monster(index, bytes, offset); }
        public override int GetGroupSize() { return Wiz4EncounterGroup.Size; }
        public override WizEncounterGroup CreateEncounterGroup(byte[] bytes, int offset = 0) { return new Wiz4EncounterGroup(bytes, offset); }
        public override void CreateSearchResults(int iRewardIndex) { SearchResults = null; }

        public void CreateSearchResults(byte[] items)
        {
            SearchResults = new Wiz4SearchResults(items);
            if (!SearchResults.IsEmpty)
                NumTotalMonsters = 0;   // Don't show the monster list during looting
        }

        public Wiz4EncounterInfo(WizGameState state, byte[] bytes, Point ptParty, int iRewardModifier, int offset = 0)
        {
            IsFightingDoGooders = true;
            SetBytes(state, bytes, ptParty, iRewardModifier, offset);
        }

        public Wiz4EncounterInfo(WizGameState state, byte[] bytesNonCombat, Point ptParty)
        {
            IsFightingDoGooders = false;
            SetBytes(state, bytesNonCombat, ptParty);
        }

        public void SetBytes(WizGameState state, byte[] bytesNonCombat, Point ptParty)
        {
            if (bytesNonCombat == null || bytesNonCombat.Length < 12)
                return;

            int[] Counts = new int[3];
            int[] Indices = new int[3];
            int iCount = 0;
            Groups = new List<WizEncounterGroup>(3);
            m_monsters = new Dictionary<int, Monster>();

            for(int i = 0; i < 3; i++)
            {
                Counts[i] = BitConverter.ToInt16(bytesNonCombat, i * 2);
                Indices[i] = BitConverter.ToInt16(bytesNonCombat, i * 2 + 6);

                if (Counts[i] < 0 || Counts[i] > 9 || Indices[i] < 0 || Indices[i] > Wiz4.Monsters.Count)
                {
                    Groups.Add(new WizEncounterGroup());
                    continue;
                }

                Groups.Add(new WizEncounterGroup());
                Groups[i].Monster = Wiz4.Monsters[Indices[i]].Clone() as WizMonster;
                Groups[i].NumAlive = Counts[i];
                Groups[i].NumEnemies = Counts[i];
                Groups[i].Identified = true;
                Groups[i].Index = Indices[i];
                Groups[i].Records = new List<WizEncounterRecord>(9);
                for (int j = 0; j < 9; j++)
                {
                    Groups[i].Records.Add(new WizEncounterRecord());
                    if (j < Groups[i].NumAlive)
                    {
                        Groups[i].Records[j].CurrentHP = Math.Max(1, (int)Wiz4.Monsters[Groups[i].Index].HitPoints.Average);
                        Monster monster = Groups[i].GetMonster(j);
                        if (monster != null)
                        {
                            monster.Position = ptParty;
                            monster.MonsterGroup = i;
                            monster.MonsterSubGroup = j;
                            monster.EncounterIndex = iCount;
                            monster.RewardModifier = 0;
                            monster.Killed = (monster.Index < 0 || monster.Index >= Wiz4.Monsters.Count);
                            Groups[i].Monster = monster as WizMonster;
                            m_monsters.Add(iCount, monster);
                            iCount++;
                        }

                        iCount++;
                    }
                }
            }

            PreEncounter = true;
            PartyLocation = ptParty;
            NumTotalMonsters = iCount;
        }
    }

    public class Wiz4EncounterData : WizEncounterData
    {
        public Wiz4EncounterData()
        {
            SetWizBytes(GameNames.Wizardry4, Global.DecompressBytes(Properties.Resources.Wizardry_4_Maps_mem));
        }

        public override bool IsMonsterEncounter(int iMapIndex, Point pt)
        {
            if (iMapIndex < 1 || iMapIndex >= Fights.Count)
                return false;
            return Fights[iMapIndex - 1][pt.X, pt.Y];
        }
    }
}
