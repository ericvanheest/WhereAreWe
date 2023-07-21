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
    public abstract class UltimaMemory
    {
        public abstract byte[] MainSearch { get; }
        public abstract MemoryGuess[] Guesses { get; }

        public abstract int MainBlockSVN { get; }
        public abstract int MainBlockOldSVN { get; }
        public virtual int MainBlockNonSVN { get { return MainBlockSVN; } }

        public abstract int MapOverworld { get; }
        public abstract int Map { get; }
        public abstract int DungeonLevel { get; }
        public abstract int LocationXY { get; }
        public abstract int LocationXYOverworld { get; }
        public abstract int Facing { get; }
        public abstract int PartyInfo { get; }
        public abstract int MainMapIndex { get; }
        public abstract int MonstersOverworld { get; }
        public abstract int TownNPCs { get; }

        public abstract int GemNames { get; }
        public abstract int WeaponNames { get; }
        public abstract int ArmorNames { get; }
        public abstract int SpellNames { get; }
        public abstract int VehicleNames { get; }

        public abstract int CityMap1 { get; }
        public abstract int CastleMap1 { get; }
        public abstract int MonsterCount { get; }
    }

    public class UltimaEncounterInfo : EncounterInfo
    {
        protected Dictionary<int, Monster> m_monsters;
        public override bool MonstersOnMap => true;

        public UltimaEncounterInfo()
            : base()
        {
            m_monsters = null;
        }

        public override bool HasTreasure => false;
        public override Dictionary<int, Monster> Monsters => m_monsters;
        public override bool InCombat => true;

        public void SetMonsters(Dictionary<int, Monster> monsters)
        {
            m_monsters = monsters;
            NumTotalMonsters = monsters.Count;
        }

        public override int NumLivingMonsters => m_monsters.Count;
    }

    public abstract class UltimaBaseCharacter : BaseCharacter
    {
        public virtual StatAndModifier BasicStrength => null;
        public virtual StatAndModifier BasicAgility => null;
        public virtual StatAndModifier BasicStamina => null;
        public virtual StatAndModifier BasicCharisma => null;
        public virtual StatAndModifier BasicWisdom => null;
        public virtual StatAndModifier BasicIntelligence => null;

        public override StatAndModifier[] PrimaryStats
        {
            get { return new StatAndModifier[] { BasicStrength, BasicAgility, BasicStamina, BasicCharisma, BasicWisdom, BasicIntelligence }; }
        }

        public override StatAndModifier Stat(string str)
        {
            if (str == null || str.Length < 1)
                return StatAndModifier.Zero;
            switch (Char.ToLower(str[0]))
            {
                case 's': return BasicStrength;
                case 'a': return BasicAgility;
                case 't': return BasicStamina;
                case 'c': return BasicCharisma;
                case 'w': return BasicWisdom;
                case 'i': return BasicIntelligence;
                default: return StatAndModifier.Zero;
            }
        }

        public override string AttributeTip(ModAttr attrib, MemoryHacker hacker)
        {
            string strBase = base.AttributeTip(attrib, hacker);
            if (!String.IsNullOrWhiteSpace(strBase))
                return strBase;

            switch (attrib)
            {
                default: return String.Empty;
            }
        }

        public static GenericClass[] UltimaClasses
        {
            get
            {
                return new GenericClass[] {
                    GenericClass.Fighter,
                    GenericClass.Cleric,
                    GenericClass.Wizard,
                    GenericClass.Thief
                };
            }
        }
    }
}
