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
    public abstract class WizMemory
    {
        public abstract byte[] MainSearch { get; }
        public abstract MemoryGuess[] Guesses { get; }

        public abstract int MainBlockSVN { get; }
        public abstract int MainBlockOldSVN { get; }
        public virtual int MainBlockNonSVN { get { return MainBlockSVN; } }

        public abstract int Facing { get; }
        public abstract int LocationDown { get; }
        public abstract int LocationNorth { get; }
        public abstract int LocationEast { get; }
        public abstract int NumChars { get; }
        public abstract int Map { get; }
        public abstract int ItemList { get; }
        public abstract int PartyInfo { get; }
        public abstract int EncounterInfo { get; }
        public abstract int AllMaps { get; }

        public virtual int MonsterListDisk { get { return -1; } }
        public virtual int State1 { get { return -1; } }
        public virtual int State2 { get { return -1; } }
        public virtual int State3 { get { return -1; } }
        public virtual int State4 { get { return -1; } }
        public virtual int State5 { get { return State4; } }
        public virtual int InspectingChar { get { return -1; } }
        public virtual int InspectingChar2 { get { return -1; } }
        public virtual int CombatOptionActiveChar { get { return -1; } }
        public virtual int TrainingChar { get { return -1; } }
        public virtual int ShoppingChar { get { return -1; } }
        public virtual int TreasureList { get { return -1; } }
        public virtual int FightMap { get { return -1; } }
        public virtual int EncounterRewardModifier { get { return -1; } }
        public virtual int CreateName { get { return -1; } }
        public virtual int CreateBonus { get { return -1; } }
        public virtual int CreateAttributes { get { return -1; } }
        public virtual int CreationSelectedStat { get { return -1; } }
        public virtual int CreationSelectedRace { get { return -1; } }
        public virtual int CreateGold { get { return -1; } }
        public virtual int TimeDelay { get { return -1; } }
        public virtual int Light { get { return -1; } }
        public virtual int ACBonus { get { return -1; } }
        public virtual int Identify { get { return -1; } }
        public virtual int TrapType { get { return -1; } }
        public virtual int TrapType2 { get { return -1; } }
        public virtual int RewardIndex { get { return -1; } }
        public virtual int CombatCharInfo { get { return -1; } }
        public virtual int Text { get { return -1; } }
    }

    public abstract class WizardryMapData : MapData
    {
        public GameNames Game;

        public class Offsets
        {
            public const int West = 0;
            public const int South = 120;
            public const int East = 240;
            public const int North = 360;
            public const int Fights = 480;
            public const int Extras = 560;
            public const int Types = 760;
            public const int Aux0 = 768;
            public const int Aux1 = 800;
            public const int Aux2 = 832;
            public const int Enemies1 = 864;
            public const int Enemies2 = 874;
            public const int Enemies3 = 884;
        }

        public override int DefaultZoom { get { return 150; } }

        public override void CopyMetadataFrom(MapData dataCopy)
        {
            base.CopyMetadataFrom(dataCopy);
            if (dataCopy is WizardryMapData)
                Game = ((WizardryMapData) dataCopy).Game;
        }

        public class WallSet
        {
            protected WizWall[,] m_walls;

            public virtual WizWall GetWall(int x, int y) { return m_walls[x, y]; }

            public WallSet()
            {
            }

            public WallSet(byte[] bytes, int offset = 0)
            {
                m_walls = new WizWall[20,20];

                for(int col = 0; col < 20; col++)
                {
                    for(int row = 0; row < 20; row += 4)
                        SetFromByte(bytes[offset + (col * 6) + (row/4)], col, row);
                }
            }

            protected void SetFromByte(byte b, int col, int row)
            {
                int iHeight = m_walls.GetLength(1);

                if (col < 0 || col >= m_walls.GetLength(0))
                    return;

                if (row >= 0 && row < iHeight)
                    m_walls[col, row] = (WizWall) (b & 0x03);
                if (row >= -1 && row < iHeight - 1)
                    m_walls[col, row + 1] = (WizWall)((b & 0x0c) >> 2);
                if (row >= -2 && row < iHeight - 2)
                    m_walls[col, row + 2] = (WizWall)((b & 0x30) >> 4);
                if (row >= -3 && row < iHeight - 3)
                    m_walls[col, row + 3] = (WizWall)((b & 0xc0) >> 6);
            }
        }

        public class EnemyCalc
        {
            public const int Size = 10;

            public int MinEnemy;
            public int Multiplier;
            public int MaxEnemy;
            public int Range;
            public int WorseChance;

            public EnemyCalc(byte[] bytes, int offset = 0)
            {
                MinEnemy = BitConverter.ToInt16(bytes, offset);
                Multiplier = BitConverter.ToInt16(bytes, offset + 2);
                MaxEnemy = BitConverter.ToInt16(bytes, offset + 4);
                Range = BitConverter.ToInt16(bytes, offset + 6);
                WorseChance = BitConverter.ToInt16(bytes, offset + 8);
            }
        }

        public WallSet West;
        public WallSet South;
        public WallSet East;
        public WallSet North;

        public bool[,] Fights;
        public int[,] Extras;
        public WizSquare[] Types;
        public int[] Aux0;
        public int[] Aux1;
        public int[] Aux2;
        public EnemyCalc[] Enemies;

        public byte[] FightBytes;

        public static bool Fight(byte[] fights, MapXY xy)
        {
            return Global.GetBit(fights, xy.X * 32 + xy.Y, true) != 0;
        }

        public static bool[,] GetFights(byte[] bytes, int offset = 0)
        {
            bool[,] fights = new bool[20, 20];
            for (int col = 0; col < 20; col++)
            {
                for (int i = 0; i < 8; i++)
                {
                    fights[col, i] = ((bytes[offset + (4 * col)] >> i) & 1) != 0;
                    fights[col, i + 8] = ((bytes[offset + (4 * col) + 1] >> i) & 1) != 0;
                    if (i < 4)
                        fights[col, i + 16] = ((bytes[offset + (4 * col) + 2] >> i) & 1) != 0;
                }
            }
            return fights;
        }

        public int[,] GetExtras(byte[] bytes, int offset = 0)
        {
            int[,] extras = new int[20, 20];
            for (int col = 0; col < 20; col++)
            {
                for (int row = 0; row < 20; row += 2)
                {
                    extras[col, row] = bytes[offset + (10 * col) + (row / 2)] & 0xf;
                    extras[col, row + 1] = bytes[offset + (10 * col) + (row / 2)] >> 4;
                }
            }
            return extras;
        }

        public WizSquare[] GetTypes(byte[] bytes, int offset = 0)
        {
            WizSquare[] types = new WizSquare[16];
            for (int i = 0; i < 8; i++)
            {
                types[i * 2] = (WizSquare)(bytes[offset + i] & 0x0f);
                types[i * 2 + 1] = (WizSquare)(bytes[offset + i] >> 4);
            }
            return types;
        }

        public int GetExtra(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height || Extras == null)
                return 0;
            return Extras[x, y];
        }

        public int[] GetAux(byte[] bytes, int offset = 0)
        {
            int[] aux = new int[16];
            for (int i = 0; i < 32; i += 2)
                aux[i/2] = BitConverter.ToInt16(bytes, offset + i);
            return aux;
        }

        public EnemyCalc[] GetEnemies(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < EnemyCalc.Size * 3)
                return null;
            EnemyCalc[] enemies = new EnemyCalc[3];
            for(int i = 0; i < 3; i++)
                enemies[i] = new EnemyCalc(bytes, offset + i * EnemyCalc.Size);
            return enemies;
        }

        public WizWall GetWall(Direction dir, int x, int y)
        {
            if (x < Bounds.Left || x >= Bounds.Right || y < Bounds.Top || y >= Bounds.Bottom)
                return WizWall.OffMap;
            switch (dir)
            {
                case Direction.Up: return North.GetWall(x, y);
                case Direction.Left: return West.GetWall(x, y);
                case Direction.Down: return South.GetWall(x, y);
                case Direction.Right: return East.GetWall(x, y);
                default: return WizWall.SolidWall;
            }
        }

        public WizWall GetWest(int x, int y) { return GetWall(Direction.Left, x, y); }
        public WizWall GetSouth(int x, int y) { return GetWall(Direction.Down, x, y); }
        public WizWall GetNorth(int x, int y) { return GetWall(Direction.Up, x, y); }
        public WizWall GetEast(int x, int y) { return GetWall(Direction.Right, x, y); }

        public void SetFromBytes(GameNames game, int index, byte[] bytes, int offset = 0, bool bSkipMainInfo = false, bool bSkipNonWallInfo = false)
        {
            Game = game;
            Bounds = new Rectangle(0, 0, 20, 20);
            Index = index;
            if (!bSkipMainInfo)
            {
                if (game == GameNames.Wizardry4)
                    Title = Wiz4MemoryHacker.GetMapTitlePair(index);
                else
                    Title = new MapTitleInfo(index, String.Format("{0} Level {1}", game == GameNames.Wizardry3 ? "Tower" : "Maze", index));
                West = new WallSet(bytes, offset + Offsets.West);
                South = new WallSet(bytes, offset + Offsets.South);
                East = new WallSet(bytes, offset + Offsets.East);
                North = new WallSet(bytes, offset + Offsets.North);
                if (!bSkipNonWallInfo)
                {
                    Fights = GetFights(bytes, offset + Offsets.Fights);
                    FightBytes = new byte[80];
                    Buffer.BlockCopy(bytes, offset + Offsets.Fights, FightBytes, 0, 80);
                    Extras = GetExtras(bytes, offset + Offsets.Extras);
                    Types = GetTypes(bytes, offset + Offsets.Types);
                    Aux0 = GetAux(bytes, offset + Offsets.Aux0);
                    Aux1 = GetAux(bytes, offset + Offsets.Aux1);
                    Aux2 = GetAux(bytes, offset + Offsets.Aux2);
                }
                else
                    LiveOnly = true;
            }
            Enemies = GetEnemies(bytes, offset + Offsets.Enemies1);
        }
    }

    public class WizardrySpell : Spell
    {
        public string Translation;

        public override string ExtendedName
        { 
            get
            {
                return String.Format("{0} ({1})", Name, Translation);
            }
        }

        public override string MultiLineDescription
        {
            get
            {
                return String.Format("Name: {0}\r\nType: {1}\r\nLevel: {2}\r\nWhen: {3}\r\nTarget: {4}\r\nLearned: {5}\r\nShort: {6}\r\n{7}",
                    ExtendedName, TypeString(Type), Level, WhenString, TargetStringFull, Learned, ShortDescription, Description);
            }
        }
    }

    public class WizardryMemoryHacker : MemoryHacker
    {
        public override PrimaryStat[] StatOrder { get { return StatOrderSIPVAL; } }
        public override string SpellType1 { get { return "Priest"; } }
        public override string SpellType2 { get { return "Mage"; } }
        public override string SpellType3 { get { return "Mage"; } }
        public override bool SpellsUseLevelOnly { get { return true; } }
    }

    public abstract class WizardryBaseCharacter : BaseCharacter
    {
        public int SaveVsPoison;
        public int SaveVsPetrify;
        public int SaveVsWand;
        public int SaveVsBreath;
        public int SaveVsSpell;
        public int PoisonCounter;
        public int Regeneration;
        public int Swimming;

        public virtual StatAndModifier BasicStrength { get { return null; } }
        public virtual StatAndModifier BasicIQ { get { return null; } }
        public virtual StatAndModifier BasicPiety { get { return null; } }
        public virtual StatAndModifier BasicVitality { get { return null; } }
        public virtual StatAndModifier BasicAgility { get { return null; } }
        public virtual StatAndModifier BasicLuck { get { return null; } }
        public override int BasicSwimming { get { return Swimming; } }

        public override StatAndModifier Stat(string str)
        {
            if (str == null || str.Length < 1)
                return StatAndModifier.Zero;
            switch (Char.ToLower(str[0]))
            {
                case 's': return BasicStrength;
                case 'i': return BasicIQ;
                case 'p': return BasicPiety;
                case 'v': return BasicVitality;
                case 'a': return BasicAgility;
                case 'l': return BasicLuck;
                default: return StatAndModifier.Zero;
            }
        }

        public override StatAndModifier[] PrimaryStats
        {
            get { return new StatAndModifier[] { BasicStrength, BasicIQ, BasicPiety, BasicVitality, BasicAgility, BasicLuck }; }
        }

        public override bool HasSpellLevel { get { return false; } }
        public override bool PartyGems { get { return true; } }
        public override OneByteStat QuickRefSpellLevel { get { return new OneByteStat(7, 7); } }

        public override string AttributeTip(ModAttr attrib, MemoryHacker hacker)
        {
            string strBase = base.AttributeTip(attrib, hacker);
            if (!String.IsNullOrWhiteSpace(strBase))
                return strBase;

            StringBuilder sb;

            switch (attrib)
            {
                case ModAttr.Strength: return GetModifier(BasicStrength.Temporary, PrimaryStat.Strength).TipString("{0} Strength: {1} to-hit and to melee combat damage ({2})");
                case ModAttr.IQ: return GetModifier(BasicIQ.Temporary, PrimaryStat.IQ).TipString("{0} I.Q.: {1}% chance to learn Mage spells ({2})", false);
                case ModAttr.Piety: return GetModifier(BasicPiety.Temporary, PrimaryStat.Piety).TipString("{0} Piety: {1}% chance to learn Priest spells ({2})", false);
                case ModAttr.Vitality: return GetModifier(BasicVitality.Temporary, PrimaryStat.Vitality).TipString("{0} Vitality: {1} HP per level ({2})");
                case ModAttr.Agility: return GetModifier(BasicAgility.Temporary, PrimaryStat.Agility).TipString("{0} Agility: {1} to initiative rolls ({2})");
                case ModAttr.Luck: return GetModifier(BasicLuck.Temporary, PrimaryStat.Luck).TipString("{0} Luck: {1} to saving throws ({2})");
                case ModAttr.SaveVsPoison: return String.Format("Save vs. Poison: {0}", SaveVsPoison);
                case ModAttr.SaveVsPetrify: return String.Format("Save vs. Petrify: {0}", SaveVsPetrify);
                case ModAttr.SaveVsWand: return String.Format("Save vs. Wand: {0}", SaveVsWand);
                case ModAttr.SaveVsBreath: return String.Format("Save vs. Breath: {0}", SaveVsBreath);
                case ModAttr.SaveVsSpell: return String.Format("Save vs. Spell: {0}", SaveVsSpell);
                case ModAttr.SaveVsSleep: return String.Format("Save vs. Sleep (except traps): {0}", Math.Max(1, 20 - (4 * BasicLevel.Temporary)));
                case ModAttr.SaveVsParalyze: return String.Format("Save vs. Paralyze (except traps): {0}", Math.Max(1, 10 - (2 * BasicLevel.Temporary)));
                case ModAttr.PoisonCount: return String.Format("{0} Poison: {1}", PoisonCounter, 
                    PoisonCounter == 0 ? "Character is not poisoned" : "25% chance per step to take 10% damage");
                case ModAttr.Regen: return String.Format("{0} Regen: {1}", Regeneration,
                    Regeneration == 0 ? "Character does not restore HP" : 
                    String.Format("25% chance per step to {0} {1} HP", Regeneration < 0 ? "lose" : "restore", Math.Abs(Regeneration)));
                case ModAttr.IdentifyTrap:
                    int iMod = BasicClass == GenericClass.Thief ? 6 : BasicClass == GenericClass.Ninja ? 4 : 1;
                    int iIdentify = Math.Min(95, iMod * BasicAgility.Temporary);
                    return String.Format("{0}% chance to correctly identify traps{1}\r\n+{2}\tAgility bonus{3}", iIdentify, iIdentify >= 95 ? " (Max)" : "", BasicAgility.Temporary,
                        iMod > 1 ? String.Format("\r\nx{0}\tClass bonus ({1})", iMod, ClassString(BasicClass)) : "");
                case ModAttr.IdentifyItem:
                    if (BasicClass != GenericClass.Bishop)
                        return "Only a Bishop can identify items";
                    sb = new StringBuilder();
                    sb.AppendFormat("{0}% chance to identify items\r\n+43%\tBase chance\r\n+{1}%\tLevel bonus\r\n", Math.Min(100, 3 * BasicLevel.Temporary + 43), 3 * BasicLevel.Temporary);
                    sb.AppendFormat("Formula: 3*CharLevel + 43\r\n");
                    sb.AppendFormat("If an identify attempt fails, there is a (65 + 3*CharLevel)% chance to avoid equipping a cursed item");
                    return sb.ToString();
                case ModAttr.Dispel:
                    if ((BasicClass != GenericClass.Bishop && BasicClass != GenericClass.Lord && BasicClass != GenericClass.Priest) ||
                        (BasicClass == GenericClass.Bishop && BasicLevel.Temporary < 4) ||
                        (BasicClass == GenericClass.Lord && BasicLevel.Temporary < 9))
                        return "Only a Priest, level 4+ Bishop or level 9+ Lord can dispel undead enemies";
                    int iDispelPenalty = (BasicClass == GenericClass.Bishop ? 20 : BasicClass == GenericClass.Lord ? 40 : 0);
                    int iLevelBonus = 5 * BasicLevel.Temporary;
                    sb = new StringBuilder();
                    sb.AppendFormat("{0}% chance to dispel undead monsters (-10% for each hit die of the monster)\r\n+50%\tBase chance\r\n+{1}%\tLevel bonus\r\n", Math.Min(999, 50 + iLevelBonus - iDispelPenalty), iLevelBonus);
                    if (BasicClass == GenericClass.Bishop)
                        sb.Append("-20%\tClass penalty (Bishop)\r\n");
                    else if (BasicClass == GenericClass.Lord)
                        sb.Append("-40%\tClass penalty (Lord)\r\n");
                    return sb.ToString();
                case ModAttr.Thievery:
                    int iModDisarm = BasicClass == GenericClass.Thief || BasicClass == GenericClass.Ninja ? 50 : 0;
                    int iMap = (hacker == null ? 0 : hacker.GetCurrentMapIndex());
                    if (iMap < 1 || iMap > 10)
                        iMap = 0;
                    int iDisarm = Math.Min(100, (iModDisarm + BasicLevel.Temporary - iMap) * 100 / 70);
                    sb = new StringBuilder();
                    sb.AppendFormat("{0}% chance to disarm traps\r\n", iDisarm);
                    sb.AppendFormat("+{0}%\tLevel bonus\r\n", BasicLevel.Temporary * 10 / 7);
                    sb.AppendFormat("-{0}%\tMaze level\r\n", iMap * 10 / 7);
                    if (iModDisarm > 0)
                        sb.AppendFormat("+{0}%\tClass bonus ({1})\r\n", iModDisarm * 10 / 7, BaseCharacter.ClassString(BasicClass));
                    sb.AppendFormat("Formula: (CharLevel - MazeLevel, plus 50 if Thief/Ninja) / 70\r\n");
                    sb.AppendFormat("If a disarm attempt fails, the chance to avoid setting off the trap is (Agility/20)");
                    return sb.ToString();
                case ModAttr.Swimming: return String.Format("{0} Swimming ({1}.{2}): Allows character to survive diving to level {1}",
                    Swimming, (Swimming + 18) / 10, (Swimming + 8) % 10);
                default: return String.Empty;
            }
        }
    }
}
