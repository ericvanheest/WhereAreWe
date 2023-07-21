using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public enum BT2MonsterIndex
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

    public class BT2Monster : BTMonster
    {
        public byte[] Attacks;
        public int DistanceLow;
        public int InitialDistance;
        public int TouchHigh;

        public override string GetName(int index) { return GetName((BT2MonsterIndex) index); }

        public override Monster Clone() 
        {
            return new BT2Monster(BTIndex, Name, HPDice, AC, DamDice, (int) Experience, GroupSize, Special);
        }

        public override void SetBytes(int index, byte[] bytes, int offset = 0) { SetBT2Bytes(index, bytes, offset); }

        public void SetBT2Bytes(int index, byte[] bytes, int offset = 0)
        {
            if (bytes == null || bytes.Length < offset + 32)
                return;

            MonsterName name = BT2MemoryHacker.ExtractMonsterNames(bytes, offset);
            NumAttacks = bytes[offset + 23] + 1;
            DamDice = new DamageDice(4, 1 + bytes[offset + 29], 0);

            MonsterIndex = (BT2MonsterIndex)index;
            Name = name.Singular;

            HPDice = new DamageDice(BitConverter.ToUInt16(bytes, offset + 16), 1, BitConverter.ToUInt16(bytes, offset + 18));
            Speed = bytes[offset + 21];
            Attacks = Global.Subset(bytes, offset + 24, 4);
            InitialDistance = (bytes[offset + 31] >> 4);
            Distance = InitialDistance;
            GroupSize = 1 + (bytes[offset + 20] * 2);
            AC = 10 - bytes[offset + 22];
            Special = (BTMonsterSpecial) (bytes[offset + 28] & 0x0f);
            DistanceLow = bytes[offset + 31] & 0x0f;
            TouchHigh = bytes[offset + 28] & 0xf0;
            Experience = NumAttacks * DamDice.Quantity * 128 + 1;
            ImageIndex = bytes[offset + 30];
        }

        public override byte[] GetBytes()
        {
            byte[] bytes = Global.NullBytes(16);
            Global.SetUInt16(bytes, 0, HPDice.Faces);
            Global.SetUInt16(bytes, 2, HPDice.Bonus);
            bytes[4] = (byte)((GroupSize - 1) / 2);
            bytes[5] = (byte)Speed;
            bytes[6] = (byte)(10 - AC);
            bytes[7] = (byte) (NumAttacks - 1);
            Buffer.BlockCopy(Attacks, 0, bytes, 8, Attacks.Length);
            bytes[13] = (byte)(DamDice.Quantity);
            bytes[12] = (byte)((int) Special | TouchHigh);
            bytes[13] = (byte) (DamDice.Quantity - 1);
            bytes[14] = (byte)ImageIndex;
            bytes[15] = (byte)(((InitialDistance) << 4) | DistanceLow);
            return bytes;
        }

        public override string GetAttackString(int attack, bool bAbbrev) { return BT2MonsterEditForm.AttackString(attack, bAbbrev); }

        public static int ExpFromByte(byte b) { return b > 15 ? b : 256 * b; }

        public BT2MonsterIndex MonsterIndex { get { return (BT2MonsterIndex)Index; } set { Index = (int)value; } }

        public static string GetName(BT2MonsterIndex index)
        {
            switch (index)
            {
                case BT2MonsterIndex.None: return "<None>";
                case BT2MonsterIndex.Kobold: return "Kobold";
                case BT2MonsterIndex.Hobbit: return "Hobbit";
                case BT2MonsterIndex.Gnome: return "Gnome";
                case BT2MonsterIndex.Dwarf: return "Dwarf";
                case BT2MonsterIndex.Thief: return "Thief";
                case BT2MonsterIndex.Hobgoblin: return "Hobgoblin";
                case BT2MonsterIndex.L1Conjurer: return "Lv1 Conjurer";
                case BT2MonsterIndex.L1Magician: return "Lv1 Magician";
                case BT2MonsterIndex.Orc: return "Orc";
                case BT2MonsterIndex.Skeleton: return "Skeleton";
                case BT2MonsterIndex.Nomad: return "Nomad";
                case BT2MonsterIndex.Spider: return "Spider";
                case BT2MonsterIndex.MadDog: return "Mad Dog";
                case BT2MonsterIndex.Barbarian: return "Barbarian";
                case BT2MonsterIndex.Mercenary: return "Mercenary";
                case BT2MonsterIndex.Wolf: return "Wolf";
                case BT2MonsterIndex.JadeMonk: return "Jade Monk";
                case BT2MonsterIndex.HalfOrc: return "Half Orc";
                case BT2MonsterIndex.Swordsman: return "Swordsman";
                case BT2MonsterIndex.Zombie: return "Zombie";
                case BT2MonsterIndex.L2Conjurer: return "Lv2 Conjurer";
                case BT2MonsterIndex.L2Magician: return "Lv2 Magician";
                case BT2MonsterIndex.L2Sorcerer: return "Lv2 Sorcerer";
                case BT2MonsterIndex.L2Wizard: return "Lv2 Wizard";
                case BT2MonsterIndex.Samurai: return "Samurai";
                case BT2MonsterIndex.BlackWidow: return "Black Widow";
                case BT2MonsterIndex.Assassin: return "Assassin";
                case BT2MonsterIndex.Werewolf: return "Werewolf";
                case BT2MonsterIndex.Ogre: return "Ogre";
                case BT2MonsterIndex.Wight: return "Wight";
                case BT2MonsterIndex.Statue: return "Statue";
                case BT2MonsterIndex.Bladesman: return "Bladesman";
                case BT2MonsterIndex.GoblinLord: return "Goblin Lord";
                case BT2MonsterIndex.MasterThief: return "Master Thief";
                case BT2MonsterIndex.L3Conjurer: return "Lv3 Conjurer";
                case BT2MonsterIndex.L3Magician: return "Lv3 Magician";
                case BT2MonsterIndex.L3Sorcerer: return "Lv3 Sorcerer";
                case BT2MonsterIndex.L3Wizard: return "Lv3 Wizard";
                case BT2MonsterIndex.Ninja: return "Ninja";
                case BT2MonsterIndex.Spinner: return "Spinner";
                case BT2MonsterIndex.ScarletMonk: return "Scarlet Monk";
                case BT2MonsterIndex.Doppleganger: return "Doppleganger";
                case BT2MonsterIndex.StoneGiant: return "Stone Giant";
                case BT2MonsterIndex.OgreMagician: return "Ogre Magician";
                case BT2MonsterIndex.Jackalwere: return "Jackalwere";
                case BT2MonsterIndex.StoneElemental: return "Stone Elemental";
                case BT2MonsterIndex.BlueDragon: return "Blue Dragon";
                case BT2MonsterIndex.Seeker: return "Seeker";
                case BT2MonsterIndex.DwarfKing: return "Dwarf King";
                case BT2MonsterIndex.SamuraiLord: return "Samurai Lord";
                case BT2MonsterIndex.Ghoul: return "Ghoul";
                case BT2MonsterIndex.L4Conjurer: return "Lv4 Conjurer";
                case BT2MonsterIndex.L4Magician: return "Lv4 Magician";
                case BT2MonsterIndex.L4Sorcerer: return "Lv4 Sorcerer";
                case BT2MonsterIndex.L4Wizard: return "Lv4 Wizard";
                case BT2MonsterIndex.AzureMonk: return "Azure Monk";
                case BT2MonsterIndex.Weretiger: return "Weretiger";
                case BT2MonsterIndex.Hydra: return "Hydra";
                case BT2MonsterIndex.GreenDragon: return "Green Dragon";
                case BT2MonsterIndex.Wraith: return "Wraith";
                case BT2MonsterIndex.Lurker: return "Lurker";
                case BT2MonsterIndex.FireGiant: return "Fire Giant";
                case BT2MonsterIndex.CopperDragon: return "Copper Dragon";
                case BT2MonsterIndex.IvoryMonk: return "Ivory Monk";
                case BT2MonsterIndex.Shadow: return "Shadow";
                case BT2MonsterIndex.Berserker: return "Berserker";
                case BT2MonsterIndex.L5Conjurer: return "Lv5 Conjurer";
                case BT2MonsterIndex.L5Magician: return "Lv5 Magician";
                case BT2MonsterIndex.L5Sorcerer: return "Lv5 Sorcerer";
                case BT2MonsterIndex.L5Wizard: return "Lv5 Wizard";
                case BT2MonsterIndex.WhiteDragon: return "White Dragon";
                case BT2MonsterIndex.IceGiant: return "Ice Giant";
                case BT2MonsterIndex.EyeSpy: return "Eye Spy";
                case BT2MonsterIndex.OgreLord: return "Ogre Lord";
                case BT2MonsterIndex.BodySnatcher: return "Body Snatcher";
                case BT2MonsterIndex.Xorn: return "Xorn";
                case BT2MonsterIndex.Phantom: return "Phantom";
                case BT2MonsterIndex.LesserDemon: return "Lesser Demon";
                case BT2MonsterIndex.Fred: return "Fred";
                case BT2MonsterIndex.L6Conjurer: return "Lv6 Conjurer";
                case BT2MonsterIndex.L6Magician: return "Lv6 Magician";
                case BT2MonsterIndex.L6Sorcerer: return "Lv6 Sorcerer";
                case BT2MonsterIndex.L6Wizard: return "Lv6 Wizard";
                case BT2MonsterIndex.MasterNinja: return "Master Ninja";
                case BT2MonsterIndex.WarGiant: return "War Giant";
                case BT2MonsterIndex.WarriorElite: return "Warrior Elite";
                case BT2MonsterIndex.BoneCrusher: return "Bone Crusher";
                case BT2MonsterIndex.Ghost: return "Ghost";
                case BT2MonsterIndex.GreyDragon: return "Grey Dragon";
                case BT2MonsterIndex.Basilisk: return "Basilisk";
                case BT2MonsterIndex.EvilEye: return "Evil Eye";
                case BT2MonsterIndex.Mimic: return "Mimic";
                case BT2MonsterIndex.Golem: return "Golem";
                case BT2MonsterIndex.Vampire: return "Vampire";
                case BT2MonsterIndex.Demon: return "Demon";
                case BT2MonsterIndex.Bandersnatch: return "Bandersnatch";
                case BT2MonsterIndex.MazeDweller: return "Maze Dweller";
                case BT2MonsterIndex.Mongo: return "Mongo";
                case BT2MonsterIndex.MangarGuard: return "Mangar Guard";
                case BT2MonsterIndex.Gimp: return "Gimp";
                case BT2MonsterIndex.RedDragon: return "Red Dragon";
                case BT2MonsterIndex.Titan: return "Titan";
                case BT2MonsterIndex.MasterConjurer: return "Master Conjurer";
                case BT2MonsterIndex.MasterMagician: return "Master Magician";
                case BT2MonsterIndex.MasterSorcerer: return "Master Sorcerer";
                case BT2MonsterIndex.MindShadow: return "Mind Shadow";
                case BT2MonsterIndex.Spectre: return "Spectre";
                case BT2MonsterIndex.CloudGiant: return "Cloud Giant";
                case BT2MonsterIndex.Beholder: return "Beholder";
                case BT2MonsterIndex.VampireLord: return "Vampire Lord";
                case BT2MonsterIndex.GreaterDemon: return "Greater Demon";
                case BT2MonsterIndex.MasterWizard: return "Master Wizard";
                case BT2MonsterIndex.MadGod: return "Mad God";
                case BT2MonsterIndex.MazeMaster: return "Maze Master";
                case BT2MonsterIndex.DeathDenizen: return "Death Denizen";
                case BT2MonsterIndex.Jabberwock: return "Jabberwock";
                case BT2MonsterIndex.BlackDragon: return "Black Dragon";
                case BT2MonsterIndex.Mangar: return "Mangar";
                case BT2MonsterIndex.CrystalGolem: return "Crystal Golem";
                case BT2MonsterIndex.SoulSucker: return "Soul Sucker";
                case BT2MonsterIndex.StormGiant: return "Storm Giant";
                case BT2MonsterIndex.AncientEnemy: return "Ancient Enemy";
                case BT2MonsterIndex.Balrog: return "Balrog";
                case BT2MonsterIndex.Lich: return "Lich";
                case BT2MonsterIndex.Archmage: return "Archmage";
                case BT2MonsterIndex.DemonLord: return "Demon Lord";
                case BT2MonsterIndex.OldMan: return "Old Man";
                default: return String.Format("Unknown({0})", (int)index);
            }
        }

        public BT2MonsterIndex BTIndex { get { return (BT2MonsterIndex)Index; } set { Index = (int)value; } }

        public BT2Monster(int iIndex, byte[] bytes, int offset)
        {
            SetBT2Bytes(iIndex, bytes, offset);
        }

        public BT2Monster(BT2MonsterIndex index, string strName, DamageDice hp, int ac, DamageDice damage, int exp, int groupSize, BTMonsterSpecial special)
        {
            Name = strName;
            MonsterIndex = index;
            HPDice = hp;
            DamDice = damage;
            AC = ac;
            Experience = exp;
            GroupSize = groupSize;
            Special = special;
        }
    }

    public class BT2MonsterList : BT123MonsterList
    {
        public override BTMonster CreateMonster(int iItemCount, byte[] bytes, int iPos) { return new BT2Monster(iItemCount, bytes, iPos); }
        public override byte[] GetInternalBytes() { return Global.DecompressBytes(Properties.Resources.BT2_Monster_List); }

        // The external bytes for BT2 are not stored in memory directly, so always use the internal bytes
        public override byte[] GetExternalBytes(MemoryHacker hacker) { return GetInternalBytes(); }
        public override bool InitInternalList()
        {
            m_monsters = SetFromBytes(GetInternalBytes());
            return true;
        }

        public BT2MonsterList()
        {
            MonsterLength = 32;
            InitBT2InternalList();
        }

        private bool InitBT2InternalList()
        {
            m_monsters = SetFromBT2Bytes(Global.DecompressBytes(Properties.Resources.BT2_Monster_List));
            return true;
        }

        public override List<BTMonster> SetFromBytes(byte[] bytes) { return SetFromBT2Bytes(bytes); }

        private List<BTMonster> SetFromBT2Bytes(byte[] bytes)
        {
            List<BTMonster> monsters = new List<BTMonster>();

            try
            {
                int iIndex = 0;
                int iCount = 0;
                while (iIndex < bytes.Length - MonsterLength)
                {
                    int iSize = BitConverter.ToInt16(bytes, iIndex);
                    int iMap = BitConverter.ToInt16(bytes, iIndex + 2);
                    string strAbbrev = Global.GetNullTerminatedString(bytes, iIndex + 4, 12);
                    string strMap = "Unknown";
                    switch (iMap)
                    {
                        case 0x0100:
                            strMap = "Town";
                            break;
                        case 0x0005:
                            strMap = "Summon";
                            break;
                        default:
                            strMap = BT2MemoryHacker.GetMapTitlePair(iMap).Title.Replace(", Level ", " ");
                            break;
                    }

                    for (int i = iIndex + 16; i < iIndex + iSize; i += MonsterLength)
                    {
                        BTMonster monster = new BT2Monster(iCount, bytes, i);
                        monster.Name = String.Format("{0}: {1}", strMap, monster.Name);
                        monsters.Add(monster);
                        iCount++;
                    }
                    iIndex += iSize;
                }

                m_bValid = true;
            }
            catch (Exception)
            {
                m_bValid = false;
            }

            return monsters;
        }
    }
}
