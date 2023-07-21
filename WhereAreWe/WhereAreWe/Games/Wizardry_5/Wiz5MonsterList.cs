using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    [Flags]
    public enum Wiz5MonsterProperty
    {
        None =      0x0000,
        Sleep =     0x0001,     // bit 0
        Stone =     0x0002,     // bit 1
        Poison =    0x0004,     // bit 2
        Paralyze =  0x0008,     // bit 3
        Autokill =  0x0010,     // bit 4
        Prop0020 =  0x0020,     // bit 5
        Prop0040 =  0x0040,     // bit 6
        Prop0080 =  0x0080,     // bit 7
        RunAway =   0x0100,     // bit 8
    }


    public enum Wiz5MonsterIndex
    {
        None = -1,
        First = 0,
        GreenSlime = 0,
        LadyStinger,
        BlackFly,
        LeechLizard,
        Netherman,
        Bandit,
        Troll,
        Magician,
        Acolyte,
        UndeadWarrior,
        IcePhantom,
        FangedToad,
        Stilette,
        BlackBat,
        Raven,
        Level4Fighter,
        BonBon,
        Conjurer,
        Berserker,
        Gwylion,
        Scarecrow,
        HellHound,
        Gypsy,
        Jackalwere,
        PinkMushroom,
        DemonImp,
        Golem,
        MustardSlime,
        Scorpion,
        Wereboar,
        Tiger,
        GiantSpider,
        Magsman,
        Amazon,
        Samurai,
        Shugenja,
        TogaLlama,
        RottedVapor,
        Minotaur,
        KingCobra,
        Wyvern,
        DemonDog,
        Ghoul,
        Ghast,
        NightLocust,
        Gorilla,
        Gargoyle,
        Basilisk,
        BlackBlade,
        Level8Thief,
        QuiSangMonk,
        Warlock,
        Druid,
        HawdyBawder,
        Firedrake,
        Assassin,
        Medusa,
        SmokeDemon,
        ArmorEater,
        Shiegetzu,
        Kalkydri,
        Wight,
        MasterThief,
        RoyalGuard,
        Hatamoto,
        Quack,
        JokerofDeath,
        RoyalLord,
        RoyalLady,
        GreenDragon,
        Werewolf,
        Vampire,
        Frankenstein,
        Quasimodo,
        BlackKnight,
        Nightmare,
        TheBeauty,
        TheBeast,
        Yeti,
        Barbarian,
        FrostGiant,
        SnowCat,
        Yomama,
        Ancient,
        Troglodyte,
        Bloodweir,
        Freezie,
        WhiteMushroom,
        BlankStare,
        SwampThing,
        MightyOak,
        Wraith,
        Lich,
        Efreeti,
        WillOWisp,
        FireElemental,
        WaterElemental,
        AirElemental,
        EarthElemental,
        RopavDica,
        HighNinja,
        SilentKnight,
        DarkWizard,
        CardinalFang,
        Psionic,
        Succubus,
        ManfrettisGhost,
        UnholyTerror,
        Houdini,
        IndigoMushroom,
        Gorgon,
        PitFiend,
        Incubus,
        Cacodaemon,
        Spectre,
        Dragonaire,
        Skeleton,
        MurphysGhost,
        Mane,
        Halycon,
        GreaterDevil,
        Bleeb,
        Golem2,
        AirElemental2,
        Djinni,
        GreaterDemon,
        TheGatekeeper,
        TheSORN,
        Clone,
        DarkLord,
        NetherDemon,
        Archdevil,
        Zombie,
        Werebat,
        TheHurkleBeast,
        TheGuardian,
        Spelunker,
        TheDejinWindKing,
        Makara,
        SeaCobra,
        GoldStatue,
        Nessie,
        LochBaby,
        TheCopperDemon,
        TheSlyNymph,
        LadyNeptune,
        Triton,
        Horbule,
        TheRobunaIceKing,
        Kong,
        Fay,
        TheZanaFireQueen,
        TheKanziFireKing,
        Dragonfinn,
        Phoenix,
        TheLaughingKettle,
        TheLordOfClubs,
        TheLordOfDiamonds,
        TheLordOfHearts,
        TheLordOfSpades,
        TheSnatch,
        TheMadStomper,
        EvilEyes,
        TheMightyYog,
        BigMax,
        TheLoon,
        LordHienmitey,
        TheDuckOfSparks,
        TheRubyWarlock,
        Ironose,
        GbliGedook,
        LalaMooMoo,
        HighSamurai,
        Assassin2,
        EnchantedBard,
        Last
    }

    public class Wiz5Monster : WizMonster
    {
        public new const int Size = 118;

        public override string GetName(int index) { return GetName((Wiz5MonsterIndex)index); }
        public override Monster Clone() { return new Wiz5Monster(Index, GetBytes(), 0); }

        public static string GetName(Wiz5MonsterIndex index)
        {
            switch (index)
            {
                case Wiz5MonsterIndex.GreenSlime: return "Green Slime";
                case Wiz5MonsterIndex.LadyStinger: return "Lady Stinger";
                case Wiz5MonsterIndex.BlackFly: return "BlackFly";
                case Wiz5MonsterIndex.LeechLizard: return "Leech Lizard";
                case Wiz5MonsterIndex.Netherman: return "Netherman";
                case Wiz5MonsterIndex.Bandit: return "Bandit";
                case Wiz5MonsterIndex.Troll: return "Troll";
                case Wiz5MonsterIndex.Magician: return "Magician";
                case Wiz5MonsterIndex.Acolyte: return "Acolyte";
                case Wiz5MonsterIndex.UndeadWarrior: return "Undead Warrior";
                case Wiz5MonsterIndex.IcePhantom: return "Ice Phantom";
                case Wiz5MonsterIndex.FangedToad: return "Fanged Toad";
                case Wiz5MonsterIndex.Stilette: return "Stilette";
                case Wiz5MonsterIndex.BlackBat: return "Black Bat";
                case Wiz5MonsterIndex.Raven: return "Raven";
                case Wiz5MonsterIndex.Level4Fighter: return "Level 4 Fighter";
                case Wiz5MonsterIndex.BonBon: return "Bon Bon";
                case Wiz5MonsterIndex.Conjurer: return "Conjurer";
                case Wiz5MonsterIndex.Berserker: return "Berserker";
                case Wiz5MonsterIndex.Gwylion: return "Gwylion";
                case Wiz5MonsterIndex.Scarecrow: return "Scarecrow";
                case Wiz5MonsterIndex.HellHound: return "Hell Hound";
                case Wiz5MonsterIndex.Gypsy: return "Gypsy";
                case Wiz5MonsterIndex.Jackalwere: return "Jackalwere";
                case Wiz5MonsterIndex.PinkMushroom: return "Pink Mushroom";
                case Wiz5MonsterIndex.DemonImp: return "Demon Imp";
                case Wiz5MonsterIndex.Golem: return "Golem A";
                case Wiz5MonsterIndex.MustardSlime: return "Mustard Slime";
                case Wiz5MonsterIndex.Scorpion: return "Scorpion";
                case Wiz5MonsterIndex.Wereboar: return "Wereboar";
                case Wiz5MonsterIndex.Tiger: return "Tiger";
                case Wiz5MonsterIndex.GiantSpider: return "Giant Spider";
                case Wiz5MonsterIndex.Magsman: return "Magsman";
                case Wiz5MonsterIndex.Amazon: return "Amazon";
                case Wiz5MonsterIndex.Samurai: return "Samurai";
                case Wiz5MonsterIndex.Shugenja: return "Shugenja";
                case Wiz5MonsterIndex.TogaLlama: return "Toga Llama";
                case Wiz5MonsterIndex.RottedVapor: return "Rotted Vapor";
                case Wiz5MonsterIndex.Minotaur: return "Minotaur";
                case Wiz5MonsterIndex.KingCobra: return "King Cobra";
                case Wiz5MonsterIndex.Wyvern: return "Wyvern";
                case Wiz5MonsterIndex.DemonDog: return "Demon Dog";
                case Wiz5MonsterIndex.Ghoul: return "Ghoul";
                case Wiz5MonsterIndex.Ghast: return "Ghast";
                case Wiz5MonsterIndex.NightLocust: return "Night Locust";
                case Wiz5MonsterIndex.Gorilla: return "Gorilla";
                case Wiz5MonsterIndex.Gargoyle: return "Gargoyle";
                case Wiz5MonsterIndex.Basilisk: return "Basilisk";
                case Wiz5MonsterIndex.BlackBlade: return "BlackBlade";
                case Wiz5MonsterIndex.Level8Thief: return "Level 8 Thief";
                case Wiz5MonsterIndex.QuiSangMonk: return "Qui-Sang Monk";
                case Wiz5MonsterIndex.Warlock: return "Warlock";
                case Wiz5MonsterIndex.Druid: return "Druid";
                case Wiz5MonsterIndex.HawdyBawder: return "Hawdy Bawder";
                case Wiz5MonsterIndex.Firedrake: return "Firedrake";
                case Wiz5MonsterIndex.Assassin: return "Assassin A";
                case Wiz5MonsterIndex.Medusa: return "Medusa";
                case Wiz5MonsterIndex.SmokeDemon: return "Smoke Demon";
                case Wiz5MonsterIndex.ArmorEater: return "Armor Eater";
                case Wiz5MonsterIndex.Shiegetzu: return "Shiegetzu";
                case Wiz5MonsterIndex.Kalkydri: return "Kalkydri";
                case Wiz5MonsterIndex.Wight: return "Wight";
                case Wiz5MonsterIndex.MasterThief: return "Master Thief";
                case Wiz5MonsterIndex.RoyalGuard: return "Royal Guard";
                case Wiz5MonsterIndex.Hatamoto: return "Hatamoto";
                case Wiz5MonsterIndex.Quack: return "Quack";
                case Wiz5MonsterIndex.JokerofDeath: return "Joker of Death";
                case Wiz5MonsterIndex.RoyalLord: return "Royal Lord";
                case Wiz5MonsterIndex.RoyalLady: return "Royal Lady";
                case Wiz5MonsterIndex.GreenDragon: return "Green Dragon";
                case Wiz5MonsterIndex.Werewolf: return "Werewolf";
                case Wiz5MonsterIndex.Vampire: return "Vampire";
                case Wiz5MonsterIndex.Frankenstein: return "Frankenstein";
                case Wiz5MonsterIndex.Quasimodo: return "Quasimodo";
                case Wiz5MonsterIndex.BlackKnight: return "Black Knight";
                case Wiz5MonsterIndex.Nightmare: return "Nightmare";
                case Wiz5MonsterIndex.TheBeauty: return "The Beauty";
                case Wiz5MonsterIndex.TheBeast: return "The Beast";
                case Wiz5MonsterIndex.Yeti: return "Yeti";
                case Wiz5MonsterIndex.Barbarian: return "Barbarian";
                case Wiz5MonsterIndex.FrostGiant: return "Frost Giant";
                case Wiz5MonsterIndex.SnowCat: return "Snow Cat";
                case Wiz5MonsterIndex.Yomama: return "Yomama";
                case Wiz5MonsterIndex.Ancient: return "Ancient";
                case Wiz5MonsterIndex.Troglodyte: return "Troglodyte";
                case Wiz5MonsterIndex.Bloodweir: return "Bloodweir";
                case Wiz5MonsterIndex.Freezie: return "Freezie";
                case Wiz5MonsterIndex.WhiteMushroom: return "White Mushroom";
                case Wiz5MonsterIndex.BlankStare: return "Blank Stare";
                case Wiz5MonsterIndex.SwampThing: return "Swamp Thing";
                case Wiz5MonsterIndex.MightyOak: return "Mighty Oak";
                case Wiz5MonsterIndex.Wraith: return "Wraith";
                case Wiz5MonsterIndex.Lich: return "Lich";
                case Wiz5MonsterIndex.Efreeti: return "Efreeti";
                case Wiz5MonsterIndex.WillOWisp: return "Will O' Wisp";
                case Wiz5MonsterIndex.FireElemental: return "Fire Elemental";
                case Wiz5MonsterIndex.WaterElemental: return "Water Elemental";
                case Wiz5MonsterIndex.AirElemental: return "Air Elemental A";
                case Wiz5MonsterIndex.EarthElemental: return "Earth Elemental";
                case Wiz5MonsterIndex.RopavDica: return "Ropav Dica";
                case Wiz5MonsterIndex.HighNinja: return "High Ninja";
                case Wiz5MonsterIndex.SilentKnight: return "Silent Knight";
                case Wiz5MonsterIndex.DarkWizard: return "Dark Wizard";
                case Wiz5MonsterIndex.CardinalFang: return "Cardinal Fang";
                case Wiz5MonsterIndex.Psionic: return "Psionic";
                case Wiz5MonsterIndex.Succubus: return "Succubus";
                case Wiz5MonsterIndex.ManfrettisGhost: return "Manfretti's Ghost";
                case Wiz5MonsterIndex.UnholyTerror: return "Unholy Terror";
                case Wiz5MonsterIndex.Houdini: return "Houdini";
                case Wiz5MonsterIndex.IndigoMushroom: return "Indigo Mushroom";
                case Wiz5MonsterIndex.Gorgon: return "Gorgon";
                case Wiz5MonsterIndex.PitFiend: return "Pit Fiend";
                case Wiz5MonsterIndex.Incubus: return "Incubus";
                case Wiz5MonsterIndex.Cacodaemon: return "Cacodaemon";
                case Wiz5MonsterIndex.Spectre: return "Spectre";
                case Wiz5MonsterIndex.Dragonaire: return "Dragonaire";
                case Wiz5MonsterIndex.Skeleton: return "Skeleton";
                case Wiz5MonsterIndex.MurphysGhost: return "Murphy's Ghost";
                case Wiz5MonsterIndex.Mane: return "Mane";
                case Wiz5MonsterIndex.Halycon: return "Halycon";
                case Wiz5MonsterIndex.GreaterDevil: return "Greater Devil";
                case Wiz5MonsterIndex.Bleeb: return "Bleeb";
                case Wiz5MonsterIndex.Golem2: return "Golem B";
                case Wiz5MonsterIndex.AirElemental2: return "Air Elemental B";
                case Wiz5MonsterIndex.Djinni: return "Djinni";
                case Wiz5MonsterIndex.GreaterDemon: return "Greater Demon";
                case Wiz5MonsterIndex.TheGatekeeper: return "The Gatekeeper";
                case Wiz5MonsterIndex.TheSORN: return "The S*O*R*N";
                case Wiz5MonsterIndex.Clone: return "Clone";
                case Wiz5MonsterIndex.DarkLord: return "Dark Lord";
                case Wiz5MonsterIndex.NetherDemon: return "Nether Demon";
                case Wiz5MonsterIndex.Archdevil: return "Archdevil";
                case Wiz5MonsterIndex.Zombie: return "Zombie";
                case Wiz5MonsterIndex.Werebat: return "Werebat";
                case Wiz5MonsterIndex.TheHurkleBeast: return "The Hurkle Beast";
                case Wiz5MonsterIndex.TheGuardian: return "The Guardian";
                case Wiz5MonsterIndex.Spelunker: return "Spelunker";
                case Wiz5MonsterIndex.TheDejinWindKing: return "The Dejin Wind King";
                case Wiz5MonsterIndex.Makara: return "Makara";
                case Wiz5MonsterIndex.SeaCobra: return "Sea Cobra";
                case Wiz5MonsterIndex.GoldStatue: return "Gold Statue";
                case Wiz5MonsterIndex.Nessie: return "Nessie";
                case Wiz5MonsterIndex.LochBaby: return "Loch Baby";
                case Wiz5MonsterIndex.TheCopperDemon: return "The Copper Demon";
                case Wiz5MonsterIndex.TheSlyNymph: return "The Sly Nymph";
                case Wiz5MonsterIndex.LadyNeptune: return "Lady Neptune";
                case Wiz5MonsterIndex.Triton: return "Triton";
                case Wiz5MonsterIndex.Horbule: return "Horbule";
                case Wiz5MonsterIndex.TheRobunaIceKing: return "The Robuna Ice King";
                case Wiz5MonsterIndex.Kong: return "Kong";
                case Wiz5MonsterIndex.Fay: return "Fay";
                case Wiz5MonsterIndex.TheZanaFireQueen: return "The Zana Fire Queen";
                case Wiz5MonsterIndex.TheKanziFireKing: return "The Kanzi Fire King";
                case Wiz5MonsterIndex.Dragonfinn: return "Dragonfinn";
                case Wiz5MonsterIndex.Phoenix: return "Phoenix";
                case Wiz5MonsterIndex.TheLaughingKettle: return "The Laughing Kettle";
                case Wiz5MonsterIndex.TheLordOfClubs: return "The Lord of Clubs";
                case Wiz5MonsterIndex.TheLordOfDiamonds: return "The Lord of Diamonds";
                case Wiz5MonsterIndex.TheLordOfHearts: return "The Lord of Hearts";
                case Wiz5MonsterIndex.TheLordOfSpades: return "The Lord of Spades";
                case Wiz5MonsterIndex.TheSnatch: return "The Snatch";
                case Wiz5MonsterIndex.TheMadStomper: return "The Mad Stomper";
                case Wiz5MonsterIndex.EvilEyes: return "Evil Eyes";
                case Wiz5MonsterIndex.TheMightyYog: return "The Mighty Yog";
                case Wiz5MonsterIndex.BigMax: return "Big Max";
                case Wiz5MonsterIndex.TheLoon: return "The Loon";
                case Wiz5MonsterIndex.LordHienmitey: return "Lord Hienmitey";
                case Wiz5MonsterIndex.TheDuckOfSparks: return "The Duck of Sparks";
                case Wiz5MonsterIndex.TheRubyWarlock: return "The Ruby Warlock";
                case Wiz5MonsterIndex.Ironose: return "Ironose";
                case Wiz5MonsterIndex.GbliGedook: return "G'bli Gedook";
                case Wiz5MonsterIndex.LalaMooMoo: return "Lala Moo-Moo";
                case Wiz5MonsterIndex.HighSamurai: return "High Samurai";
                case Wiz5MonsterIndex.Assassin2: return "Assassin B";
                case Wiz5MonsterIndex.EnchantedBard: return "Enchanted Bard";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public Wiz5MonsterIndex WizIndex { get { return (Wiz5MonsterIndex)Index; } set { Index = (int)value; } }
        public Wiz5MonsterIndex GroupHelp { get { return (Wiz5MonsterIndex)Help; } set { Help = (int)value; } }
        public override int TreasureCount { get { return Wiz5.Treasures.Count; } }
        public override WizTreasure Treasure(int index) { return index < 0 || index >= Wiz5.Treasures.Count ? null : Wiz5.Treasures[index]; }

        public static class OffsetsWiz5
        {
            public static int Image = 0;
            public static int NumAppearing = 2;
            public static int HitPoints = 8;
            public static int Family = 14;
            public static int ArmorClass = 16;
            public static int NumAttacks = 18;
            public static int Attack1 = 20;
            public static int Attack2 = 26;
            public static int Attack3 = 32;
            public static int Attack4 = 38;
            public static int Attack5 = 44;
            public static int Attack6 = 50;
            public static int Attack7 = 56;
            public static int Attack8 = 62;
            public static int Attack9 = 68;
            public static int Experience = 74;
            public static int Drain = 80;
            public static int DrainStat = 82;
            public static int Regeneration = 84;
            public static int Reward1 = 86;
            public static int Reward2 = 88;
            public static int GroupHelp = 90;
            public static int GroupHelpChance = 96;
            public static int Help2 = 92;
            public static int Help3 = 94;
            public static int MagicResist = 102;
            public static int MagicChance = 108;
            public static int MageSpellLevel = 98;
            public static int ClericSpellLevel = 99;
            public static int BreathWeapon = 100;
            public static int Resistances = 104;
            public static int Special = 106;
            public static int BreathChance = 110;
            public static int StealChance = 112;
            public static int NPC1 = 114;
            public static int NPC2 = 116;
        }

        public void SetFromBytesWiz5(int iIndex, string strName, byte[] bytes, int offset)
        {
            if (bytes == null || bytes.Length - Size < offset)
                return;
            Index = iIndex;
            Silenced = 0;
            ACModifier = 0;
            Image = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.Image);
            NumAppearing = DiceFromOffset(bytes, offset + OffsetsWiz5.NumAppearing);
            HitPoints = DiceFromOffset(bytes, offset + OffsetsWiz5.HitPoints);
            Family = (WizMonsterFamily)BitConverter.ToInt16(bytes, offset + OffsetsWiz5.Family);
            AC = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.ArmorClass);
            NumAttacks = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.NumAttacks);
            Attacks = new List<DamageDice>();
            for (int i = OffsetsWiz5.Attack1; i <= OffsetsWiz5.Attack9; i += 6)
            {
                DamageDice dice = DiceFromOffset(bytes, offset + i);
                if (dice.Quantity > 0)
                    Attacks.Add(dice);
            }
            Drain = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.Drain);
            Regeneration = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.Regeneration);
            Help = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.GroupHelp);
            GroupHelpChance = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.GroupHelpChance);
            MageSpellLevel = bytes[offset + OffsetsWiz5.MageSpellLevel];
            PriestSpellLevel = bytes[offset + OffsetsWiz5.ClericSpellLevel];
            BreathWeapon = (WizBreath)BitConverter.ToInt16(bytes, offset + OffsetsWiz5.BreathWeapon);
            MagicResist = bytes[offset + OffsetsWiz5.MagicResist];
            MagicChance = bytes[offset + OffsetsWiz5.MagicChance];
            Resistances = (WizResist)BitConverter.ToInt16(bytes, offset + OffsetsWiz5.Resistances);
            Wiz5MonsterProperty prop = (Wiz5MonsterProperty)BitConverter.ToInt16(bytes, offset + OffsetsWiz5.Special);
            Properties = ToWizProp(prop);
            Reward1 = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.Reward1);
            Reward2 = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.Reward2);
            int iDrainStat = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.DrainStat);
            if (iDrainStat > 0)
                DrainStat = GenericAttribute.Strength + (iDrainStat - 1);
            else
                DrainStat = GenericAttribute.None;

            NPC1 = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.NPC1);
            NPC2 = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.NPC2);
            BreathChance = bytes[offset + OffsetsWiz5.BreathChance];
            StealChance = bytes[offset + OffsetsWiz5.StealChance];
            Help2 = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.Help2);
            Help3 = BitConverter.ToInt16(bytes, offset + OffsetsWiz5.Help3);

            int[] unknownBytes = new int[] { 103, 109, 111 };
            Unknowns = new int[unknownBytes.Length];
            for (int i = 0; i < unknownBytes.Length; i++)
                Unknowns[i] = bytes[offset + unknownBytes[i]];

            RewardModifier = 0;
            Name = strName;
            Experience = new WizardryLong(bytes, offset + OffsetsWiz5.Experience).Number;
        }

        public static WizMonsterProperty ToWizProp(Wiz5MonsterProperty w5Prop)
        {
            WizMonsterProperty prop = WizMonsterProperty.None;
            if (w5Prop.HasFlag(Wiz5MonsterProperty.Autokill))
                prop |= WizMonsterProperty.Autokill;
            if (w5Prop.HasFlag(Wiz5MonsterProperty.Paralyze))
                prop |= WizMonsterProperty.Paralyze;
            if (w5Prop.HasFlag(Wiz5MonsterProperty.Poison))
                prop |= WizMonsterProperty.Poison;
            if (w5Prop.HasFlag(Wiz5MonsterProperty.RunAway))
                prop |= WizMonsterProperty.RunAway;
            if (w5Prop.HasFlag(Wiz5MonsterProperty.Sleep))
                prop |= WizMonsterProperty.Sleep;
            if (w5Prop.HasFlag(Wiz5MonsterProperty.Stone))
                prop |= WizMonsterProperty.Stone;
            return prop;
        }

        public static Wiz5MonsterProperty ToWiz5Prop(WizMonsterProperty wProp)
        {
            Wiz5MonsterProperty prop = Wiz5MonsterProperty.None;
            if (wProp.HasFlag(WizMonsterProperty.Autokill))
                prop |= Wiz5MonsterProperty.Autokill;
            if (wProp.HasFlag(WizMonsterProperty.Paralyze))
                prop |= Wiz5MonsterProperty.Paralyze;
            if (wProp.HasFlag(WizMonsterProperty.Poison))
                prop |= Wiz5MonsterProperty.Poison;
            if (wProp.HasFlag(WizMonsterProperty.RunAway))
                prop |= Wiz5MonsterProperty.RunAway;
            if (wProp.HasFlag(WizMonsterProperty.Sleep))
                prop |= Wiz5MonsterProperty.Sleep;
            if (wProp.HasFlag(WizMonsterProperty.Stone))
                prop |= Wiz5MonsterProperty.Stone;
            return prop;
        }

        public override byte[] GetBytes()
        {
            if (NumAppearing == null)
                return null;
            MemoryStream ms = new MemoryStream();
            Global.WriteInt16(ms, Image);
            WriteDice(ms, NumAppearing);
            WriteDice(ms, HitPoints);
            Global.WriteInt16(ms, (int)Family);
            Global.WriteInt16(ms, AC);
            Global.WriteInt16(ms, NumAttacks);
            for (int i = 0; i < 9; i++)
            {
                if (Attacks.Count > i)
                    WriteDice(ms, Attacks[i]);
                else
                    WriteDice(ms, DamageDice.Zero);
            }
            byte[] bytesExp = WizardryLong.GetBytes(Experience);
            ms.Write(bytesExp, 0, bytesExp.Length);
            Global.WriteInt16(ms, Drain);
            Global.WriteInt16(ms, DrainStat == GenericAttribute.None ? 0 : (int) (DrainStat - GenericAttribute.Strength + 1));
            Global.WriteInt16(ms, Regeneration);
            Global.WriteInt16(ms, Reward1);
            Global.WriteInt16(ms, Reward2);
            Global.WriteInt16(ms, Help);
            Global.WriteInt16(ms, Help2);
            Global.WriteInt16(ms, Help3);
            Global.WriteInt16(ms, GroupHelpChance);
            ms.WriteByte((byte)MageSpellLevel);
            ms.WriteByte((byte)PriestSpellLevel);
            Global.WriteInt16(ms, (int)BreathWeapon);
            ms.WriteByte((byte)MagicResist);
            ms.WriteByte((byte)Unknowns[0]);
            Global.WriteInt16(ms, (int)Resistances);
            Global.WriteInt16(ms, (int)ToWiz5Prop(Properties));
            ms.WriteByte((byte)MagicChance);
            ms.WriteByte((byte)Unknowns[1]);
            ms.WriteByte((byte)BreathChance);
            ms.WriteByte((byte)Unknowns[2]);
            Global.WriteInt16(ms, StealChance);
            Global.WriteInt16(ms, NPC1);
            Global.WriteInt16(ms, NPC2);
            return ms.ToArray();
        }

        public Wiz5Monster(int iIndex, byte[] bytes, int offset)
        {
            SetFromBytesWiz5(iIndex, GetName((Wiz5MonsterIndex)iIndex), bytes, offset);
        }
    }

    public class Wiz5MonsterList : Wiz123MonsterList
    {
        public override WizMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return new Wiz5Monster(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_5_Monster_List_mem); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            return GetInternalBytes();
        }

        public override List<WizMonster> SetFromBytes(byte[] bytes)
        {
            List<WizMonster> monsters = new List<WizMonster>(bytes.Length / Wiz5Monster.Size);

            try
            {
                // The wizardry 5 monsters are stored in 118-byte segments
                int iPos = 0;
                int iItemCount = 0;
                while (iPos <= bytes.Length - Wiz5Monster.Size)
                {
                    monsters.Add(CreateMonster(iItemCount, bytes, iPos));
                    iPos += Wiz5Monster.Size;
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

        public Wiz5MonsterList()
        {
            InitInternalList();
        }
    }

    public class Wiz5EncounterInfo : WizEncounterInfo
    {
        public override WizMonster CreateMonster(int index, byte[] bytes, int offset) { return new Wiz5Monster(index, bytes, offset); }
        public override void CreateSearchResults(int iRewardIndex) { SearchResults = new Wiz5SearchResults(iRewardIndex); }
        public override int GetGroupSize() { return Wiz5EncounterGroup.Size; }
        public override int GetRecordSize() { return Wiz5EncounterRecord.Size; }
        public override WizEncounterGroup CreateEncounterGroup(byte[] bytes, int offset = 0) { return new Wiz5EncounterGroup(bytes, offset); }
        public override int GetMonsterOffset() { return 20 * 9 + 12; }
        public new const int Size = 6 * (WizEncounterGroup.Size + Wiz5Monster.Size);

        public Wiz5EncounterInfo(WizGameState state, byte[] bytes, Point ptParty, int iRewardModifier, int offset = 0)
        {
            SetBytes(state, bytes, ptParty, iRewardModifier, offset);
        }

        public override byte[] GetCharBytes()
        {
            MemoryStream ms = new MemoryStream();
            WriteGroup(ms, Characters as Wiz5EncounterGroup);
            return ms.ToArray();
        }

        public override WizEncounterRecord CreateEncounterRecord(byte[] bytes, int offset = 0)
        {
            return new Wiz5EncounterRecord(bytes, offset);
        }

        public override MeleeType GetMeleeType(int iChar, out string strWeapon)
        {
            // Melee range in Wizardry 5 is not as straightfoward as in other games.
            // It depends on the character row (1-3 or 4-6) and the weapon range (close, short, medium, long)

            strWeapon = "Fists";

            if (Party == null)
                return MeleeType.None;

            Wiz5Inventory inv = new Wiz5Inventory(GameNames.Wizardry5, Party.Bytes, iChar * Wiz5Character.SizeInBytes + Wiz5.Offsets.Inventory);
            if (inv == null)
                return MeleeType.None;

            bool bFrontRow = (iChar < 3);

            Item weapon = inv.Items.FirstOrDefault<Item>(i => i is Wiz5Item && ((Wiz5Item)i).Location == WizEquipLocation.Weapon);

            int iRange = (weapon == null ? -1 : ((Wiz5Item)weapon).Range);
            strWeapon = (weapon == null ? "Fists" : weapon.Name);
            string strRange = (weapon == null ? "close" : ((Wiz5Item)weapon).RangeString.ToLower());

            if (bFrontRow)
            {
                switch (iRange)
                {
                    case -1: return MeleeType.FrontRowClose;
                    case 0: return MeleeType.FrontRowShort;
                    case 1: return MeleeType.FrontRowMedium;
                    case 2: return MeleeType.FrontRowLong;
                }
            }
            else
            {
                switch (iRange)
                {
                    case -1: return MeleeType.BackRowClose;
                    case 0: return MeleeType.BackRowShort;
                    case 1: return MeleeType.BackRowMedium;
                    case 2: return MeleeType.BackRowLong;
                }
            }

            return MeleeType.None;
        }

        private void WriteRecord(Stream stream, Wiz5EncounterRecord record)
        {
            Global.WriteInt16(stream, record.Victim);
            Global.WriteInt16(stream, record.SpellHash);
            Global.WriteInt16(stream, record.Initiative);
            Global.WriteInt16(stream, record.Unknown1);
            Global.WriteInt16(stream, record.CurrentHP);
            Global.WriteInt16(stream, -record.ArmorClass);
            Global.WriteInt16(stream, record.Unknown2);
            Global.WriteInt16(stream, record.Silenced);
            Global.WriteInt16(stream, record.Level);
            Global.WriteInt16(stream, (int)record.Condition);
        }

        private void WriteGroup(Stream stream, Wiz5EncounterGroup group)
        {
            Global.WriteInt16(stream, group.Identified ? 1 : 0);
            Global.WriteInt16(stream, group.NumAlive);
            Global.WriteInt16(stream, group.NumEnemies);
            Global.WriteInt16(stream, (int)group.Index);
            Global.WriteInt16(stream, group.MagicScreen);
            Global.WriteInt16(stream, group.FizzleField);

            for (int i = 0; i < 9; i++)
            {
                if (i >= group.Records.Count)
                    Global.WriteBytes(stream, Global.NullBytes(Wiz5EncounterRecord.Size));
                else
                    WriteRecord(stream, group.Records[i] as Wiz5EncounterRecord);
            }

            byte[] bytesMonster = null;

            if (group.Monster == null)
                bytesMonster = Global.NullBytes(Wiz5Monster.Size);
            else
                bytesMonster = group.Monster.GetBytes();

            if (bytesMonster == null)
                bytesMonster = Global.NullBytes(Wiz5Monster.Size);
            stream.Write(bytesMonster, 0, bytesMonster.Length);
        }

        public override byte[] GetBytes()
        {
            MemoryStream ms = new MemoryStream();
            foreach (Wiz5EncounterGroup group in Groups)
                WriteGroup(ms, group);
            return ms.ToArray();
        }

        public override TurnOrderCalculator GetTurnOrder(CharCombatLabel[] labelChars, GameInfo gameInfo)
        {
            // Wizardry determines initiative randomly after commands are committed,
            // so we can't reliably determine who will be in which order beforehand
            TurnOrderCalculator toc = new TurnOrderCalculator(0, 0);
            int iNameMax = 0;

            Wiz5Character[] characters = new Wiz5Character[Party.Bytes.Length / Party.CharacterSize];
            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
            {
                string strWeapon = String.Empty;
                MeleeType melee = GetMeleeType(iIndex, out strWeapon);
                labelChars[iIndex].ToolTip = GetMeleeTip(melee, strWeapon);

                bool bCanAttackAny = (melee != MeleeType.None && melee != MeleeType.BackRowClose);

                characters[iIndex] = Wiz5Character.Create(0, Party.Bytes, iIndex * Wiz5Character.SizeInBytes, gameInfo as Wiz5GameInfo, this);
                labelChars[iIndex].Melee = bCanAttackAny;
                labelChars[iIndex].Condition = characters[iIndex].BasicCondition;
                labelChars[iIndex].CharName = String.Format("{0})  {1}", iIndex + 1, characters[iIndex].CharName);
                labelChars[iIndex].HP = characters[iIndex].HitPoints.Current.ToString();
                labelChars[iIndex].SP = characters[iIndex].SpellPoints.Current.ToString();

                iNameMax = Math.Max(iNameMax, labelChars[iIndex].NameLength);
            }
            for (byte iIndex = Party.NumChars; iIndex < 8; iIndex++)
                labelChars[iIndex].Clear();

            for (byte iIndex = 0; iIndex < Party.NumChars; iIndex++)
                labelChars[iIndex].SetHPOffset(iNameMax + 2);

            return toc;
        }

        public override string ExtraText
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Characters.MagicScreen > 0 || Characters.FizzleField > 0)
                {
                    sb.Append("Party: ");
                    if (Characters.MagicScreen > 0)
                        sb.AppendFormat("Magic Screen {0}, ", Characters.MagicScreen);
                    if (Characters.FizzleField > 0)
                        sb.AppendFormat("Fizzle Field {0}, ", Characters.FizzleField);
                    Global.Trim(sb);
                }
                for (int i = 0; i < Groups.Count; i++)
                {
                    if (Groups[i].Monster == null)
                        continue;

                    if (Groups[i].MagicScreen > 0 || Groups[i].FizzleField > 0)
                    {
                        if (sb.Length > 1)
                            sb.AppendLine();
                        sb.AppendFormat("Group {0} ({1}): ", i + 1, Groups[i].Monster.ProperName);
                        if (Groups[i].MagicScreen > 0)
                            sb.AppendFormat("Magic Screen {0}, ", Groups[i].MagicScreen);
                        if (Groups[i].FizzleField > 0)
                            sb.AppendFormat("Fizzle Field {0}, ", Groups[i].FizzleField);
                    }
                    Global.Trim(sb);
                }
                return Global.Trim(sb).ToString();
            }
        }
    }

    public class Wiz5EncounterData : WizEncounterData
    {
        public Wiz5EncounterData()
        {
            SetWizBytes(GameNames.Wizardry5, null); // Global.DecompressBytes(Properties.Resources.Wizardry_3_Maps_mem));
        }

        public override bool IsMonsterEncounter(int iMapIndex, Point pt)
        {
            if (iMapIndex < 1 || iMapIndex >= Fights.Count)
                return false;
            return Fights[iMapIndex - 1][pt.X, pt.Y];
        }
    }

    public class Wiz5TreasureList : Wiz123TreasureList
    {
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.Wizardry_5_Treasure_List_mem); }
        public override string ItemName(int index) { return index < 0 || index >= Wiz5.Items.Count ? "Unknown" : Wiz5.Items[index].Name; }
        public override int TreasureCount { get { return Wiz5.Treasures.Count; } }
        public override WizTreasure Treasure(int index) { return index < 0 || index >= Wiz5.Treasures.Count ? null : Wiz5.Treasures[index]; }
        public override int TreasureSize { get { return Wiz5Treasure.Size; } }
        public override int Padding { get { return 0; } }
        public override WizTreasure CreateTreasure(GameNames game, byte[] bytes, int offset) { return new Wiz5Treasure(bytes, offset); }

        public override byte[] GetExternalBytes(MemoryHacker hacker) { return GetInternalBytes(); }

        public Wiz5TreasureList()
        {
            InitInternalList(GameNames.Wizardry5);
        }
    }

    public class Wiz5TrapInfo : WizTrapInfo
    {
        public override int TrapCount { get { return (int)Wiz5Trap.Last; } }
        public override string GetTrapName(int trap) { return GetWiz5TrapName((Wiz5Trap)trap); }

        public enum Wiz5Trap
        {
            TraplessChest = 0,
            JaxSling = 1,
            VaporCoil = 2,
            RainbowRay = 3,
            DaemonEye = 4,
            Sirens = 5,
            Psionics = 6,
            Magnetics = 7,
            LapisSpine = 8,
            Teleporter = 9,
            AstralCube = 10,
            PowderKeg = 11,
            FigbyFist = 12,
            DragonsAsp = 13,
            ElectricBolt = 14,
            MagicDrain = 15,
            Last
        }

        [Flags]
        public enum Wiz5TrapFlags
        {
            None = 0x0000,
            TraplessChest = 0x0001,
            JaxSling = 0x0002,
            VaporCoil = 0x0004,
            RainbowRay = 0x0008,
            DaemonEye = 0x0010,
            Sirens = 0x0020,
            Psionics = 0x0040,
            Magnetics = 0x0080,
            LapisSpine = 0x0100,
            Teleporter = 0x0200,
            AstralCube = 0x0400,
            PowderKeg = 0x0800,
            FigbyFist = 0x1000,
            DragonsAsp = 0x2000,
            ElectricBolt = 0x4000,
            MagicDrain = 0x8000,
            AnyTrap = 0xFFFE,
            Anything = 0xFFFF
        }

        public static Wiz5TrapFlags FlagsFromWiz5Trap(Wiz5Trap trap)
        {
            switch (trap)
            {
                case Wiz5Trap.TraplessChest: return Wiz5TrapFlags.TraplessChest;
                case Wiz5Trap.JaxSling: return Wiz5TrapFlags.JaxSling;
                case Wiz5Trap.VaporCoil: return Wiz5TrapFlags.VaporCoil;
                case Wiz5Trap.RainbowRay: return Wiz5TrapFlags.RainbowRay;
                case Wiz5Trap.DaemonEye: return Wiz5TrapFlags.DaemonEye;
                case Wiz5Trap.Sirens: return Wiz5TrapFlags.Sirens;
                case Wiz5Trap.Psionics: return Wiz5TrapFlags.Psionics;
                case Wiz5Trap.Magnetics: return Wiz5TrapFlags.Magnetics;
                case Wiz5Trap.LapisSpine: return Wiz5TrapFlags.LapisSpine;
                case Wiz5Trap.Teleporter: return Wiz5TrapFlags.Teleporter;
                case Wiz5Trap.AstralCube: return Wiz5TrapFlags.AstralCube;
                case Wiz5Trap.PowderKeg: return Wiz5TrapFlags.PowderKeg;
                case Wiz5Trap.FigbyFist: return Wiz5TrapFlags.FigbyFist;
                case Wiz5Trap.DragonsAsp: return Wiz5TrapFlags.DragonsAsp;
                case Wiz5Trap.ElectricBolt: return Wiz5TrapFlags.ElectricBolt;
                case Wiz5Trap.MagicDrain: return Wiz5TrapFlags.MagicDrain;
                default: return Wiz5TrapFlags.None;
            }
        }

        public override string TrapsString
        {
            get
            {
                if (Wiz5Traps == Wiz5TrapFlags.AnyTrap || Wiz5Traps == Wiz5TrapFlags.Anything)
                    return "Any";
                if (Wiz5Traps == Wiz5TrapFlags.None)
                    return "None";
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 16; i++)
                {
                    if (Wiz5Traps.HasFlag((Wiz5TrapFlags)(1 << i)))
                        sb.AppendFormat("{0}, ", GetWiz5TrapName((Wiz5Trap)i));
                }
                return Global.Trim(sb).ToString();
            }
        }

        public static string GetWiz5TrapName(Wiz5Trap trap)
        {
            switch (trap)
            {
                case Wiz5Trap.TraplessChest: return "Trapless Chest";
                case Wiz5Trap.JaxSling: return "Jax Sling";
                case Wiz5Trap.VaporCoil: return "Vapor Coil";
                case Wiz5Trap.RainbowRay: return "Rainbow Ray";
                case Wiz5Trap.DaemonEye: return "Daemon Eye";
                case Wiz5Trap.Sirens: return "Sirens";
                case Wiz5Trap.Psionics: return "Psionics";
                case Wiz5Trap.Magnetics: return "Magnetics";
                case Wiz5Trap.LapisSpine: return "Lapis Spine";
                case Wiz5Trap.Teleporter: return "Teleporter";
                case Wiz5Trap.AstralCube: return "Astral Cube";
                case Wiz5Trap.PowderKeg: return "Powder Keg";
                case Wiz5Trap.FigbyFist: return "Figby Fist";
                case Wiz5Trap.DragonsAsp: return "Dragon's Asp";
                case Wiz5Trap.ElectricBolt: return "Electric Bolt";
                case Wiz5Trap.MagicDrain: return "Magic Drain";
                default: return "Unknown";
            }
        }

        public Wiz5TrapFlags Wiz5Traps { get { return (Wiz5TrapFlags)m_iTrapsFlag; } set { m_iTrapsFlag = (int)value; } }

        public Wiz5TrapInfo(int iTrapsFlag) : base(iTrapsFlag)
        {
        }

        public Wiz5TrapInfo(Wiz5Trap trap) : base(0)
        {
            Trap = (int)trap;
            m_iTrapsFlag = (int)FlagsFromWiz5Trap(trap);
        }
    }

    public class Wiz5Treasure : WizTreasure
    {
        public new const int Size = 114;

        public Wiz5Treasure(byte[] bytes, int offset = 0)
        {
            Game = GameNames.Wizardry5;
            Bytes = Global.Subset(bytes, offset, Size);
            Chest = BitConverter.ToInt16(bytes, offset) != 0;
            Trap = new Wiz5TrapInfo(BitConverter.ToUInt16(bytes, offset + 2));
            NumRewards = BitConverter.ToInt16(bytes, offset + 4);
            Rewards = new List<Reward>(6);
            for (int i = 0; i < 6; i++)
                Rewards.Add(new Reward(bytes, offset + 6 + (i * Reward.Size)));
        }

        public override int LevelForItemMFactor(int index)
        {
            int iLevel = index / 10 - 3;
            if (iLevel < 1)
                iLevel = 1;
            return iLevel;
        }

        public override int LevelForItem(int index)
        {
            //4-23  Basic Items
            //24-43 +1-ish items
            //44-66 +2-ish items
            //67-77 +3-ish items
            //78-90 Mage stuff
            //91-100 Scrolls and Potions
            //101+ Quest items
            if (index >= 91 && index <= 100)
                return 2;    // Scrolls and Potions

            if (index < 24)
                return 1;    // Basic equipment
            if (index < 44)
                return 3;    // Items of +1 or equivalent
            if (index < 67)
                return 4;    // Items of +2 or equivalent
            if (index < 78)
                return 5;    // Items of +3 or equivalent
            if (index < 91)
                return 6;    // Mage-type things (rings, ankhs, staffs)
            return 7;   // Quest items
        }
    }
}
