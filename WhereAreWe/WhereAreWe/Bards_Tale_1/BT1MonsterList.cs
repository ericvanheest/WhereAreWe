using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum BT1MonsterIndex
    {
        None = 0,
        Kobold = 1,
        Hobbit,
        Gnome,
        Dwarf,
        Thief,
        Hobgoblin,
        L1Conjurer,
        L1Magician,
        Orc,
        Skeleton,
        Nomad,
        Spider,
        MadDog,
        Barbarian,
        Mercenary,
        Wolf,
        JadeMonk,
        HalfOrc,
        Swordsman,
        Zombie,
        L2Conjurer,
        L2Magician,
        L2Sorcerer,
        L2Wizard,
        Samurai,
        BlackWidow,
        Assassin,
        Werewolf,
        Ogre,
        Wight,
        Statue,
        Bladesman,
        GoblinLord,
        MasterThief,
        L3Conjurer,
        L3Magician,
        L3Sorcerer,
        L3Wizard,
        Ninja,
        Spinner,
        ScarletMonk,
        Doppleganger,
        StoneGiant,
        OgreMagician,
        Jackalwere,
        StoneElemental,
        BlueDragon,
        Seeker,
        DwarfKing,
        SamuraiLord,
        Ghoul,
        L4Conjurer,
        L4Magician,
        L4Sorcerer,
        L4Wizard,
        AzureMonk,
        Weretiger,
        Hydra,
        GreenDragon,
        Wraith,
        Lurker,
        FireGiant,
        CopperDragon,
        IvoryMonk,
        Shadow,
        Berserker,
        L5Conjurer,
        L5Magician,
        L5Sorcerer,
        L5Wizard,
        WhiteDragon,
        IceGiant,
        EyeSpy,
        OgreLord,
        BodySnatcher,
        Xorn,
        Phantom,
        LesserDemon,
        Fred,
        L6Conjurer,
        L6Magician,
        L6Sorcerer,
        L6Wizard,
        MasterNinja,
        WarGiant,
        WarriorElite,
        BoneCrusher,
        Ghost,
        GreyDragon,
        Basilisk,
        EvilEye,
        Mimic,
        Golem,
        Vampire,
        Demon,
        Bandersnatch,
        MazeDweller,
        Mongo,
        MangarGuard,
        Gimp,
        RedDragon,
        Titan,
        MasterConjurer,
        MasterMagician,
        MasterSorcerer,
        MindShadow,
        Spectre,
        CloudGiant,
        Beholder,
        VampireLord,
        GreaterDemon,
        MasterWizard,
        MadGod,
        MazeMaster,
        DeathDenizen,
        Jabberwock,
        BlackDragon,
        Mangar,
        CrystalGolem,
        SoulSucker,
        StormGiant,
        AncientEnemy,
        Balrog,
        Lich,
        Archmage,
        DemonLord,
        OldMan, 
        Last
    }

    public class BT1Monster : BTMonster
    {
        public override string GetName(int index) { return GetName((BT1MonsterIndex) index); }

        public override Monster Clone() 
        {
            return new BT1Monster(BTIndex, HPDice, AC, DamDice, (int) Experience, GroupSize, Special);
        }

        public override void SetBytes(int index, byte[] bytes, int offset = 0) { SetBT1Bytes(index, bytes, offset); }

        public void SetBT1Bytes(int index, byte[] bytes, int offset = 0)
        {
            MonsterIndex = (BT1MonsterIndex)index;
            Name = GetName(MonsterIndex);
            if (bytes == null || bytes.Length < offset + 4)
                return;

            Experience = ExpFromByte(bytes[offset + 3]);
            GroupSize = 1 << ((bytes[0] & 0x0f) + 1);
            if (GroupSize < 4)
                GroupSize = 1;
            if (GroupSize > 99)
                GroupSize = 99;
            AC = 10 - (bytes[1] & 0x1f);
            int iHP = 1 << (((bytes[0] & 0xf0) >> 4) + 1);
            if (iHP < 4)
                iHP = 0;
            HPDice = new DamageDice(iHP, 1, ((bytes[1] & 0xe0) >> 4) - 1);
            DamDice = new DamageDice(4, (bytes[2] & 0x1f) + 1, 0);
            Special = (BTMonsterSpecial)((bytes[2] & 0xe0) >> 5);
        }

        public static int ExpFromByte(byte b) { return b > 15 ? b : 256 * b; }

        public BT1MonsterIndex MonsterIndex { get { return (BT1MonsterIndex)Index; } set { Index = (int)value; } }

        public static string GetName(BT1MonsterIndex index)
        {
            switch (index)
            {
                case BT1MonsterIndex.None: return "<None>";
                case BT1MonsterIndex.Kobold: return "Kobold";
                case BT1MonsterIndex.Hobbit: return "Hobbit";
                case BT1MonsterIndex.Gnome: return "Gnome";
                case BT1MonsterIndex.Dwarf: return "Dwarf";
                case BT1MonsterIndex.Thief: return "Thief";
                case BT1MonsterIndex.Hobgoblin: return "Hobgoblin";
                case BT1MonsterIndex.L1Conjurer: return "Lv1 Conjurer";
                case BT1MonsterIndex.L1Magician: return "Lv1 Magician";
                case BT1MonsterIndex.Orc: return "Orc";
                case BT1MonsterIndex.Skeleton: return "Skeleton";
                case BT1MonsterIndex.Nomad: return "Nomad";
                case BT1MonsterIndex.Spider: return "Spider";
                case BT1MonsterIndex.MadDog: return "Mad Dog";
                case BT1MonsterIndex.Barbarian: return "Barbarian";
                case BT1MonsterIndex.Mercenary: return "Mercenary";
                case BT1MonsterIndex.Wolf: return "Wolf";
                case BT1MonsterIndex.JadeMonk: return "Jade Monk";
                case BT1MonsterIndex.HalfOrc: return "Half Orc";
                case BT1MonsterIndex.Swordsman: return "Swordsman";
                case BT1MonsterIndex.Zombie: return "Zombie";
                case BT1MonsterIndex.L2Conjurer: return "Lv2 Conjurer";
                case BT1MonsterIndex.L2Magician: return "Lv2 Magician";
                case BT1MonsterIndex.L2Sorcerer: return "Lv2 Sorcerer";
                case BT1MonsterIndex.L2Wizard: return "Lv2 Wizard";
                case BT1MonsterIndex.Samurai: return "Samurai";
                case BT1MonsterIndex.BlackWidow: return "Black Widow";
                case BT1MonsterIndex.Assassin: return "Assassin";
                case BT1MonsterIndex.Werewolf: return "Werewolf";
                case BT1MonsterIndex.Ogre: return "Ogre";
                case BT1MonsterIndex.Wight: return "Wight";
                case BT1MonsterIndex.Statue: return "Statue";
                case BT1MonsterIndex.Bladesman: return "Bladesman";
                case BT1MonsterIndex.GoblinLord: return "Goblin Lord";
                case BT1MonsterIndex.MasterThief: return "Master Thief";
                case BT1MonsterIndex.L3Conjurer: return "Lv3 Conjurer";
                case BT1MonsterIndex.L3Magician: return "Lv3 Magician";
                case BT1MonsterIndex.L3Sorcerer: return "Lv3 Sorcerer";
                case BT1MonsterIndex.L3Wizard: return "Lv3 Wizard";
                case BT1MonsterIndex.Ninja: return "Ninja";
                case BT1MonsterIndex.Spinner: return "Spinner";
                case BT1MonsterIndex.ScarletMonk: return "Scarlet Monk";
                case BT1MonsterIndex.Doppleganger: return "Doppleganger";
                case BT1MonsterIndex.StoneGiant: return "Stone Giant";
                case BT1MonsterIndex.OgreMagician: return "Ogre Magician";
                case BT1MonsterIndex.Jackalwere: return "Jackalwere";
                case BT1MonsterIndex.StoneElemental: return "Stone Elemental";
                case BT1MonsterIndex.BlueDragon: return "Blue Dragon";
                case BT1MonsterIndex.Seeker: return "Seeker";
                case BT1MonsterIndex.DwarfKing: return "Dwarf King";
                case BT1MonsterIndex.SamuraiLord: return "Samurai Lord";
                case BT1MonsterIndex.Ghoul: return "Ghoul";
                case BT1MonsterIndex.L4Conjurer: return "Lv4 Conjurer";
                case BT1MonsterIndex.L4Magician: return "Lv4 Magician";
                case BT1MonsterIndex.L4Sorcerer: return "Lv4 Sorcerer";
                case BT1MonsterIndex.L4Wizard: return "Lv4 Wizard";
                case BT1MonsterIndex.AzureMonk: return "Azure Monk";
                case BT1MonsterIndex.Weretiger: return "Weretiger";
                case BT1MonsterIndex.Hydra: return "Hydra";
                case BT1MonsterIndex.GreenDragon: return "Green Dragon";
                case BT1MonsterIndex.Wraith: return "Wraith";
                case BT1MonsterIndex.Lurker: return "Lurker";
                case BT1MonsterIndex.FireGiant: return "Fire Giant";
                case BT1MonsterIndex.CopperDragon: return "Copper Dragon";
                case BT1MonsterIndex.IvoryMonk: return "Ivory Monk";
                case BT1MonsterIndex.Shadow: return "Shadow";
                case BT1MonsterIndex.Berserker: return "Berserker";
                case BT1MonsterIndex.L5Conjurer: return "Lv5 Conjurer";
                case BT1MonsterIndex.L5Magician: return "Lv5 Magician";
                case BT1MonsterIndex.L5Sorcerer: return "Lv5 Sorcerer";
                case BT1MonsterIndex.L5Wizard: return "Lv5 Wizard";
                case BT1MonsterIndex.WhiteDragon: return "White Dragon";
                case BT1MonsterIndex.IceGiant: return "Ice Giant";
                case BT1MonsterIndex.EyeSpy: return "Eye Spy";
                case BT1MonsterIndex.OgreLord: return "Ogre Lord";
                case BT1MonsterIndex.BodySnatcher: return "Body Snatcher";
                case BT1MonsterIndex.Xorn: return "Xorn";
                case BT1MonsterIndex.Phantom: return "Phantom";
                case BT1MonsterIndex.LesserDemon: return "Lesser Demon";
                case BT1MonsterIndex.Fred: return "Fred";
                case BT1MonsterIndex.L6Conjurer: return "Lv6 Conjurer";
                case BT1MonsterIndex.L6Magician: return "Lv6 Magician";
                case BT1MonsterIndex.L6Sorcerer: return "Lv6 Sorcerer";
                case BT1MonsterIndex.L6Wizard: return "Lv6 Wizard";
                case BT1MonsterIndex.MasterNinja: return "Master Ninja";
                case BT1MonsterIndex.WarGiant: return "War Giant";
                case BT1MonsterIndex.WarriorElite: return "Warrior Elite";
                case BT1MonsterIndex.BoneCrusher: return "Bone Crusher";
                case BT1MonsterIndex.Ghost: return "Ghost";
                case BT1MonsterIndex.GreyDragon: return "Grey Dragon";
                case BT1MonsterIndex.Basilisk: return "Basilisk";
                case BT1MonsterIndex.EvilEye: return "Evil Eye";
                case BT1MonsterIndex.Mimic: return "Mimic";
                case BT1MonsterIndex.Golem: return "Golem";
                case BT1MonsterIndex.Vampire: return "Vampire";
                case BT1MonsterIndex.Demon: return "Demon";
                case BT1MonsterIndex.Bandersnatch: return "Bandersnatch";
                case BT1MonsterIndex.MazeDweller: return "Maze Dweller";
                case BT1MonsterIndex.Mongo: return "Mongo";
                case BT1MonsterIndex.MangarGuard: return "Mangar Guard";
                case BT1MonsterIndex.Gimp: return "Gimp";
                case BT1MonsterIndex.RedDragon: return "Red Dragon";
                case BT1MonsterIndex.Titan: return "Titan";
                case BT1MonsterIndex.MasterConjurer: return "Master Conjurer";
                case BT1MonsterIndex.MasterMagician: return "Master Magician";
                case BT1MonsterIndex.MasterSorcerer: return "Master Sorcerer";
                case BT1MonsterIndex.MindShadow: return "Mind Shadow";
                case BT1MonsterIndex.Spectre: return "Spectre";
                case BT1MonsterIndex.CloudGiant: return "Cloud Giant";
                case BT1MonsterIndex.Beholder: return "Beholder";
                case BT1MonsterIndex.VampireLord: return "Vampire Lord";
                case BT1MonsterIndex.GreaterDemon: return "Greater Demon";
                case BT1MonsterIndex.MasterWizard: return "Master Wizard";
                case BT1MonsterIndex.MadGod: return "Mad God";
                case BT1MonsterIndex.MazeMaster: return "Maze Master";
                case BT1MonsterIndex.DeathDenizen: return "Death Denizen";
                case BT1MonsterIndex.Jabberwock: return "Jabberwock";
                case BT1MonsterIndex.BlackDragon: return "Black Dragon";
                case BT1MonsterIndex.Mangar: return "Mangar";
                case BT1MonsterIndex.CrystalGolem: return "Crystal Golem";
                case BT1MonsterIndex.SoulSucker: return "Soul Sucker";
                case BT1MonsterIndex.StormGiant: return "Storm Giant";
                case BT1MonsterIndex.AncientEnemy: return "Ancient Enemy";
                case BT1MonsterIndex.Balrog: return "Balrog";
                case BT1MonsterIndex.Lich: return "Lich";
                case BT1MonsterIndex.Archmage: return "Archmage";
                case BT1MonsterIndex.DemonLord: return "Demon Lord";
                case BT1MonsterIndex.OldMan: return "Old Man";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public BT1MonsterIndex BTIndex { get { return (BT1MonsterIndex)Index; } set { Index = (int)value; } }

        public BT1Monster(int iIndex, byte[] bytes, int offset)
        {
            SetBT1Bytes(iIndex, bytes, offset);
        }

        public BT1Monster(BT1MonsterIndex index, DamageDice hp, int ac, DamageDice damage, int exp, int groupSize, BTMonsterSpecial special)
        {
            Name = GetName(index);
            MonsterIndex = index;
            HPDice = hp;
            DamDice = damage;
            AC = ac;
            Experience = exp;
            GroupSize = groupSize;
            Special = special;
        }
    }

    public class BT1MonsterList : BT123MonsterList
    {
        public override BTMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return new BT1Monster(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.BT1_Monster_List); }

        public override byte[] GetExternalBytes(MemoryHacker hacker)
        {
            if (hacker == null || !hacker.IsValid)
                return null;
            MemoryBytes mb = hacker.ReadOffset(BT1.Memory.MonsterGroup, 512);
            if (mb == null)
                return null;
            return mb.Bytes;
        }

        public override bool InitInternalList() { return InitBT1InternalList(); }

        public override List<BTMonster> SetFromBytes(byte[] bytes) { return SetFromBT1Bytes(bytes); }

        private List<BTMonster> SetFromBT1Bytes(byte[] bytes)
        {
            int iNumMonsters = bytes.Length / MonsterLength;
            List<BTMonster> monsters = new List<BTMonster>(iNumMonsters);
            monsters.Add(new BT1Monster(0, new byte[] { 0, 0, 0, 0 }, 0));

            try
            {
                for (int iIndex = 1; iIndex < iNumMonsters; iIndex++)
                {
                    byte[] bytesMonster = new byte[] { bytes[iIndex], bytes[iIndex + 0x80], bytes[iIndex + 0x100], bytes[iIndex + 0x180] };
                    BTMonster monster = new BT1Monster(iIndex, bytesMonster, 0);
                    monsters.Add(monster);
                }

                m_bValid = true;
            }
            catch (Exception)
            {
                m_bValid = false;
            }

            return monsters;
        }

        private bool InitBT1InternalList()
        {
            m_monsters = SetFromBT1Bytes(Global.DecompressBytes(Properties.Resources.BT1_Monster_List));
            return true;
        }

        public BT1MonsterList()
        {
            InitBT1InternalList();
        }
    }
}
