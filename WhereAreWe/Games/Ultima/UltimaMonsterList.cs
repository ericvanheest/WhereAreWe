using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum UltimaOverworldMonsterIndex
    {
        None = 0,
        Grass = 1,
        Forest = 2,
        Mountain = 3,
        Castle4 = 4,
        Castle5 = 5,
        Sign = 6,
        City7 = 7,
        City8 = 8,
        Cave = 9,
        Player = 10,
        Horse = 11,
        Cart = 12,
        Raft = 13,
        Frigate = 14,
        Aircar = 16,
        Shuttle = 17,
        TimeMachine = 18,
        NessCreature = 19,
        GiantSquid = 21,
        DragonTurtle = 23,
        PirateShip = 25,
        Hood = 27,
        Bear = 29,
        HiddenArcher = 31,
        DarkKnight = 33,
        EvilTrent = 35,
        Thief = 37,
        Orc = 39,
        Knight = 41,
        Necromancer = 43,
        EvilRanger = 45,
        WanderingWarlock = 47,
    }

    public enum UltimaDungeonMonsterIndex
    {
        None = -1,
        Ranger = 0,
        Skeleton = 1,
        Thief = 2,
        GiantRat = 3,
        Bat = 4,
        Spider = 5,
        Viper = 6,
        Orc = 7,
        Cyclops = 8,
        GelatinousCube = 9,
        Ettin = 10,
        Mimic = 11,
        LizardMan = 12,
        Minatour = 13,
        CarrionCreeper = 14,
        Tangler = 15,
        Gremlin = 16,
        WanderingEyes = 17,
        Wraith = 18,
        Lich = 19,
        InvisibleSeeker = 20,
        MindWhipper = 21,
        Zorn = 22,
        Daemon = 23,
        Balron = 24,
    }

    public enum UltimaMonsterIndex
    {
        None = -1,
        Ranger = 0,
        Skeleton = 1,
        Thief = 2,
        GiantRat = 3,
        Bat = 4,
        Spider = 5,
        Viper = 6,
        Orc = 7,
        Cyclops = 8,
        GelatinousCube = 9,
        Ettin = 10,
        Mimic = 11,
        LizardMan = 12,
        Minatour = 13,
        CarrionCreeper = 14,
        Tangler = 15,
        Gremlin = 16,
        WanderingEyes = 17,
        Wraith = 18,
        Lich = 19,
        InvisibleSeeker = 20,
        MindWhipper = 21,
        Zorn = 22,
        Daemon = 23,
        Balron = 24,
        NessCreature = 25,
        GiantSquid = 26,
        DragonTurtle = 27,
        PirateShip = 28,
        Hood = 29,
        Bear = 30,
        HiddenArcher = 31,
        DarkKnight = 32,
        EvilTrent = 33,
        SurfaceThief = 34,
        SurfaceOrc = 35,
        Knight = 36,
        Necromancer = 37,
        EvilRanger = 38,
        WanderingWarlock = 39,
        King = 40,
        Princess = 41,
        Shopkeeper = 42,
        Jester = 43,
        Wench = 44,
        Guard = 45,
        Bard = 46,
        Last,
    }

    public enum UltimaMapType
    {
        Overworld = 0,
        Dungeon = 1,
        City = 2,
        Castle = 3,
    }

    public class UltimaMonster : Monster
    {
        public UltimaMapType MapType = UltimaMapType.Dungeon;
        public const int Size = 16;
        public UltimaOutdoorTile Travel = UltimaOutdoorTile.Border;
        public Point PreviousPosition;   // For editing
        public override int InternalIndex => Index;
        public int ExternalIndex => GetExternal((UltimaMonsterIndex)Index);
        public override string ExperienceString => "3-11";
        public override bool IsAlive => CurrentHP > 0 || (MapType == UltimaMapType.Dungeon && CurrentHP == 0);

        public override string TreasureStringShort
        {
            get
            {
                switch ((UltimaMonsterIndex)Index)
                {
                    case UltimaMonsterIndex.Jester: return "Key";
                    default:
                        if (Index <= (int) UltimaMonsterIndex.WanderingWarlock)
                            return String.Format("3-{0} Coin", MaxCoin((UltimaMonsterIndex)Index));
                        return "";
                }
            }
        }

        public int MaxCoin(UltimaMonsterIndex index)
        {
            switch ((UltimaMonsterIndex)Index)
            {
                case UltimaMonsterIndex.NessCreature: return 50;
                case UltimaMonsterIndex.GiantSquid: return 30;
                case UltimaMonsterIndex.DragonTurtle: return 26;
                case UltimaMonsterIndex.PirateShip: return 22;
                case UltimaMonsterIndex.Hood: return 18;
                case UltimaMonsterIndex.Bear: return 14;
                case UltimaMonsterIndex.HiddenArcher: return 26;
                case UltimaMonsterIndex.DarkKnight: return 34;
                case UltimaMonsterIndex.EvilTrent: return 42;
                case UltimaMonsterIndex.SurfaceThief: return 50;
                case UltimaMonsterIndex.SurfaceOrc: return 14;
                case UltimaMonsterIndex.Knight: return 18;
                case UltimaMonsterIndex.Necromancer: return 26;
                case UltimaMonsterIndex.EvilRanger: return 34;
                case UltimaMonsterIndex.WanderingWarlock: return 42;
                default:
                    if (Index < 25)
                        return Index + 13;
                    return 0;
            }
        }

        public override string HPString(bool bPreEncounter)
        {
            if (bPreEncounter)
            {
                switch ((UltimaMonsterIndex)Index)
                {
                    case UltimaMonsterIndex.NessCreature: return "41 +Level";
                    case UltimaMonsterIndex.GiantSquid: return "61 +Level";
                    case UltimaMonsterIndex.DragonTurtle: return "81 +Level";
                    case UltimaMonsterIndex.PirateShip: return "101 +Level";
                    case UltimaMonsterIndex.Hood: return "11 +Level";
                    case UltimaMonsterIndex.Bear: return "21 +Level";
                    case UltimaMonsterIndex.DarkKnight: return "61 +Level";
                    case UltimaMonsterIndex.EvilTrent: return "81 +Level";
                    case UltimaMonsterIndex.SurfaceThief: return "21 +Level";
                    case UltimaMonsterIndex.SurfaceOrc: return "41 +Level";
                    case UltimaMonsterIndex.HiddenArcher: return "51 +Level";
                    case UltimaMonsterIndex.Knight: return "61 +Level";
                    case UltimaMonsterIndex.Necromancer: return "81 +Level";
                    case UltimaMonsterIndex.EvilRanger: return "91 +Level";
                    case UltimaMonsterIndex.WanderingWarlock: return "101 +Level";
                    case UltimaMonsterIndex.Guard: return "500";
                    case UltimaMonsterIndex.King: return "2000";
                    default:
                        int iLevel = LevelFound;
                        if (iLevel == -1)
                            return "1";
                        int iMinHP = ((iLevel + 1) / 2) * 5 + 7;
                        return String.Format("{0}+(0-Level ²)", iMinHP, iMinHP + (iLevel * iLevel));
                }
            }
            return base.HPString(bPreEncounter);
        }

        public int LevelFound => MapType == UltimaMapType.Dungeon ? (Index / 5 + 1) * 2 : -1;

        public UltimaMonster(UltimaMapType mapType, int index)
        {
            PreviousPosition = Global.NullPoint;
            MapType = mapType;
            Index = GetInternal(mapType, index);
            Name = Index.ToString() +": " + GetName((UltimaMonsterIndex) Index);
            NumAttacks = 1;
        }

        public UltimaMonster(UltimaMonsterIndex index)
        {
            PreviousPosition = Global.NullPoint;
            MapType = MonsterMapType(index);
            Index = (int) index;
            Name = Index.ToString() + ": " + GetName(index);
            NumAttacks = 1;
        }

        public static string MapTypeString(UltimaMapType mapType)
        {
            switch (mapType)
            {
                case UltimaMapType.Overworld: return "Overworld";
                case UltimaMapType.Dungeon: return "Dungeon";
                case UltimaMapType.City: return "City";
                case UltimaMapType.Castle: return "Castle";
                default: return String.Format("Unknown({0})", (int) mapType);
            }
        }

        public static int GetExternal(UltimaMonsterIndex index)
        {
            switch (index)
            {
                case UltimaMonsterIndex.NessCreature: return (int)UltimaOverworldMonsterIndex.NessCreature;
                case UltimaMonsterIndex.GiantSquid: return (int)UltimaOverworldMonsterIndex.GiantSquid;
                case UltimaMonsterIndex.DragonTurtle: return (int)UltimaOverworldMonsterIndex.DragonTurtle;
                case UltimaMonsterIndex.PirateShip: return (int)UltimaOverworldMonsterIndex.PirateShip;
                case UltimaMonsterIndex.Hood: return (int)UltimaOverworldMonsterIndex.Hood;
                case UltimaMonsterIndex.Bear: return (int)UltimaOverworldMonsterIndex.Bear;
                case UltimaMonsterIndex.HiddenArcher: return (int)UltimaOverworldMonsterIndex.HiddenArcher;
                case UltimaMonsterIndex.DarkKnight: return (int)UltimaOverworldMonsterIndex.DarkKnight;
                case UltimaMonsterIndex.EvilTrent: return (int)UltimaOverworldMonsterIndex.EvilTrent;
                case UltimaMonsterIndex.SurfaceThief: return (int)UltimaOverworldMonsterIndex.Thief;
                case UltimaMonsterIndex.SurfaceOrc: return (int)UltimaOverworldMonsterIndex.Orc;
                case UltimaMonsterIndex.Knight: return (int)UltimaOverworldMonsterIndex.Knight;
                case UltimaMonsterIndex.Necromancer: return (int)UltimaOverworldMonsterIndex.Necromancer;
                case UltimaMonsterIndex.EvilRanger: return (int)UltimaOverworldMonsterIndex.EvilRanger;
                case UltimaMonsterIndex.WanderingWarlock: return (int)UltimaOverworldMonsterIndex.WanderingWarlock;
                case UltimaMonsterIndex.Guard: return 17;
                case UltimaMonsterIndex.Bard: return 19;
                case UltimaMonsterIndex.Jester: return 19;
                case UltimaMonsterIndex.King: return 20;
                case UltimaMonsterIndex.Shopkeeper: return 21;
                case UltimaMonsterIndex.Princess: return 22;
                case UltimaMonsterIndex.Wench: return 50;
                default: return (int) index;
            }
        }

        public static UltimaMapType MonsterMapType(UltimaMonsterIndex index)
        {
            if (index <= UltimaMonsterIndex.Balron)
                return UltimaMapType.Dungeon;
            if (index <= UltimaMonsterIndex.WanderingWarlock)
                return UltimaMapType.Overworld;
            switch (index)
            {
                case UltimaMonsterIndex.Bard:
                case UltimaMonsterIndex.Wench:
                case UltimaMonsterIndex.Shopkeeper:
                    return UltimaMapType.City;
                default:
                    return UltimaMapType.Castle;
            }
        }

        public static int GetInternal(UltimaMapType mapType, int iExternalIndex)
        {
            if (mapType == UltimaMapType.Dungeon)
                return iExternalIndex;
            if (mapType == UltimaMapType.Overworld)
            {
                switch ((UltimaOverworldMonsterIndex)iExternalIndex)
                {
                    case UltimaOverworldMonsterIndex.NessCreature: return (int)UltimaMonsterIndex.NessCreature;
                    case UltimaOverworldMonsterIndex.GiantSquid: return (int)UltimaMonsterIndex.GiantSquid;
                    case UltimaOverworldMonsterIndex.DragonTurtle: return (int)UltimaMonsterIndex.DragonTurtle;
                    case UltimaOverworldMonsterIndex.PirateShip: return (int)UltimaMonsterIndex.PirateShip;
                    case UltimaOverworldMonsterIndex.Hood: return (int)UltimaMonsterIndex.Hood;
                    case UltimaOverworldMonsterIndex.Bear: return (int)UltimaMonsterIndex.Bear;
                    case UltimaOverworldMonsterIndex.DarkKnight: return (int)UltimaMonsterIndex.DarkKnight;
                    case UltimaOverworldMonsterIndex.EvilTrent: return (int)UltimaMonsterIndex.EvilTrent;
                    case UltimaOverworldMonsterIndex.Thief: return (int)UltimaMonsterIndex.SurfaceThief;
                    case UltimaOverworldMonsterIndex.HiddenArcher: return (int)UltimaMonsterIndex.HiddenArcher;
                    case UltimaOverworldMonsterIndex.Orc: return (int)UltimaMonsterIndex.SurfaceOrc;
                    case UltimaOverworldMonsterIndex.Knight: return (int)UltimaMonsterIndex.Knight;
                    case UltimaOverworldMonsterIndex.Necromancer: return (int)UltimaMonsterIndex.Necromancer;
                    case UltimaOverworldMonsterIndex.EvilRanger: return (int)UltimaMonsterIndex.EvilRanger;
                    case UltimaOverworldMonsterIndex.WanderingWarlock: return (int)UltimaMonsterIndex.WanderingWarlock;
                    default: return -1;
                }
            }
            switch (iExternalIndex)
            {
                case 17: return (int)UltimaMonsterIndex.Guard;
                case 19: return mapType == UltimaMapType.City ? (int)UltimaMonsterIndex.Bard : (int) UltimaMonsterIndex.Jester;
                case 20: return (int)UltimaMonsterIndex.King;
                case 21: return (int)UltimaMonsterIndex.Shopkeeper;
                case 22: return (int)UltimaMonsterIndex.Princess;
                case 50: return (int)UltimaMonsterIndex.Wench;
                default:
                    return -1;
            }
        }

        public static string GetName(UltimaMonsterIndex index)
        {
            {
                switch (index)
                {
                    case UltimaMonsterIndex.Ranger: return "Ranger";
                    case UltimaMonsterIndex.Skeleton: return "Skeleton";
                    case UltimaMonsterIndex.Thief: return "Thief";
                    case UltimaMonsterIndex.GiantRat: return "Giant rat";
                    case UltimaMonsterIndex.Bat: return "Bat";
                    case UltimaMonsterIndex.Spider: return "Spider";
                    case UltimaMonsterIndex.Viper: return "Viper";
                    case UltimaMonsterIndex.Orc: return "Orc";
                    case UltimaMonsterIndex.Cyclops: return "Cyclops";
                    case UltimaMonsterIndex.GelatinousCube: return "Gelatinous cube";
                    case UltimaMonsterIndex.Ettin: return "Ettin";
                    case UltimaMonsterIndex.Mimic: return "Mimic";
                    case UltimaMonsterIndex.LizardMan: return "Lizard man";
                    case UltimaMonsterIndex.Minatour: return "Minatour";
                    case UltimaMonsterIndex.CarrionCreeper: return "Carrion creeper";
                    case UltimaMonsterIndex.Tangler: return "Tangler";
                    case UltimaMonsterIndex.Gremlin: return "Gremlin";
                    case UltimaMonsterIndex.WanderingEyes: return "Wandering eyes";
                    case UltimaMonsterIndex.Wraith: return "Wraith";
                    case UltimaMonsterIndex.Lich: return "Lich";
                    case UltimaMonsterIndex.InvisibleSeeker: return "Invisible seeker";
                    case UltimaMonsterIndex.MindWhipper: return "Mind whipper";
                    case UltimaMonsterIndex.Zorn: return "Zorn";
                    case UltimaMonsterIndex.Daemon: return "Daemon";
                    case UltimaMonsterIndex.Balron: return "Balron";
                    case UltimaMonsterIndex.NessCreature: return "Ness Creature";
                    case UltimaMonsterIndex.GiantSquid: return "Giant Squid";
                    case UltimaMonsterIndex.DragonTurtle: return "Dragon Turtle";
                    case UltimaMonsterIndex.PirateShip: return "Pirate Ship";
                    case UltimaMonsterIndex.Hood: return "Hood";
                    case UltimaMonsterIndex.Bear: return "Bear";
                    case UltimaMonsterIndex.DarkKnight: return "Dark Knight";
                    case UltimaMonsterIndex.EvilTrent: return "Evil Trent";
                    case UltimaMonsterIndex.SurfaceThief: return "Surface Thief";
                    case UltimaMonsterIndex.SurfaceOrc: return "Surface Orc";
                    case UltimaMonsterIndex.HiddenArcher: return "Hidden Archer";
                    case UltimaMonsterIndex.Knight: return "Knight";
                    case UltimaMonsterIndex.Necromancer: return "Necromancer";
                    case UltimaMonsterIndex.EvilRanger: return "Evil Ranger";
                    case UltimaMonsterIndex.WanderingWarlock: return "Wandering Warlock";
                    case UltimaMonsterIndex.King: return "King";
                    case UltimaMonsterIndex.Princess: return "Princess";
                    case UltimaMonsterIndex.Shopkeeper: return "Shopkeeper";
                    case UltimaMonsterIndex.Jester: return "Jester";
                    case UltimaMonsterIndex.Wench: return "Wench";
                    case UltimaMonsterIndex.Guard: return "Guard";
                    case UltimaMonsterIndex.Bard: return "Bard";
                    default: return String.Format("Unknown({0})", index);
                }
            }
        }

        public override string DamageString
        {
            get
            {
                switch ((UltimaMonsterIndex)Index)
                {
                    case UltimaMonsterIndex.Gremlin: return "½ Food";
                    case UltimaMonsterIndex.NessCreature: return "1d20+1";
                    case UltimaMonsterIndex.GiantSquid: return "1d10+1";
                    case UltimaMonsterIndex.DragonTurtle: return "1d8+1";
                    case UltimaMonsterIndex.PirateShip: return "1d6+1";
                    case UltimaMonsterIndex.Hood: return "1d4+1";
                    case UltimaMonsterIndex.Bear: return "1d2+1";
                    case UltimaMonsterIndex.HiddenArcher: return "1d8+1";
                    case UltimaMonsterIndex.DarkKnight: return "1d12+1";
                    case UltimaMonsterIndex.EvilTrent: return "1d16+1";
                    case UltimaMonsterIndex.SurfaceThief: return "1d20+1";
                    case UltimaMonsterIndex.SurfaceOrc: return "1d2+1";
                    case UltimaMonsterIndex.Knight: return "1d4+1";
                    case UltimaMonsterIndex.Necromancer: return "1d8+1";
                    case UltimaMonsterIndex.EvilRanger: return "1d12+1";
                    case UltimaMonsterIndex.WanderingWarlock: return "1d16+1";
                    default:
                        if (Index < 16)
                            return String.Format("{0}", (Index * Index + 1));
                        if (Index < 25)
                            return String.Format("{0}-254", Index + 2);
                        return "?";
                }
        }
        }

        public override string OneLineDescription
        {
            get
            {
                if (Travel == UltimaOutdoorTile.Border)
                    return String.Format("{0}, {1}", Name, CurrentHP);
                return String.Format("{0} ({1}), {2}", Name, UltimaMaps.OutdoorTileName(Travel), CurrentHP);
            }
        }

        public static byte[] Int16sToBytes(params int[] list)
        {
            byte[] bytes = new byte[list.Length * 2];
            for (int i = 0; i < list.Length; i++)
                Global.SetInt16(bytes, i * 2, list[i]);
            return bytes;
        }

        public virtual byte[] GetBytes()
        {
            switch (MapType)
            {
                case UltimaMapType.Dungeon:
                    return Int16sToBytes(0, ExternalIndex, 0, CurrentHP);
                case UltimaMapType.Overworld:
                    return Int16sToBytes(ExternalIndex, (int) Travel, Position.X, Position.Y, CurrentHP);
                case UltimaMapType.Castle:
                case UltimaMapType.City:
                    return Int16sToBytes(ExternalIndex, Position.X, Position.Y, CurrentHP);
                default:
                    return new byte[0];
            }
        }

        public virtual void SetBytes(byte[] bytes) { }

        public override string ProperName { get { return Name; } }
        public override string GetMultiLineDescription(bool bActive) { return MultiLineDescription; }

        public override string MultiLineDescription
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("#{0}, {1}\r\n", Index, Name);
                sb.AppendFormat("Damage: {0}\r\n", DamageString);
                if (MapType != UltimaMapType.Dungeon)
                    sb.AppendFormat("Travel: {0}\r\n", UltimaMaps.OutdoorTileName(Travel));
                sb.AppendFormat("HP: {0}\r\n", HPString(true));
                return sb.ToString();
            }
        }
    }

    public abstract class Ultima123MonsterList : InternExternList
    {
        protected List<Monster> m_monsters;
        public List<UltimaMonster> InternalMonsters;
        public bool UsingInternalList = false;
        public int MonsterLength = 4;

        public virtual UltimaMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return null; }

        public List<Monster> Monsters
        {
            get
            {
                if (m_monsters == null)
                    InitInternalList();
                return m_monsters;
            }
        }

        public bool Reinitialize(UltimaMemoryHacker hacker, bool bOverrideSanityCheck)
        {
            InitInternalList();
            return false;
        }

        public abstract List<UltimaMonster> SetFromBytes(byte[] bytes);

        public override bool InitInternalList()
        {
            m_monsters = new List<Monster>();
            for (UltimaMonsterIndex i = UltimaMonsterIndex.Ranger; i < UltimaMonsterIndex.Last; i++)
                m_monsters.Add(new UltimaMonster(i));
            UsingInternalList = true;
            return true;
        }
    }
}
