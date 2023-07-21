using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    [Flags]
    public enum MM2MonsterTouch
    {
        None = 0x00,
        StealSomeGold = 0x01,
        StealSomeGems = 0x02,
        Poison = 0x03,
        Disease = 0x04,
        Sleep = 0x05,
        Curse = 0x06,
        StealAllFood = 0x07,
        Paralyze = 0x08,
        Collapse = 0x09,
        Death = 0x0A,
        Stone = 0x0B,
        Eradication = 0x0C,
        StealItem = 0x0D,
        StealBackpack = 0x0E,
        StealSomeFood = 0x0F,
        StealAllGold = 0x11,
        StealAllGems = 0x12,
        StealGemsAndGold = 0x13,
        Aging1 = 0x14,
        Aging2 = 0x15,
        DrainStatistic1 = 0x16,
        DrainStatistic2 = 0x17,
        DrainStatistic3 = 0x18,
        DrainLevel1 = 0x19,
        DrainLevel2 = 0x1a,
        DrainExperience = 0x1B,
        ScrambleItems = 0x1C,
        DrainSpellPoints = 0x1D,
        Assassination = 0x1E,
        RandomEffect = 0x1F,
        AllTouches = 0x1F,
        RareEffect = 0x20,
        Missile = 0x40,
        Undead = 0x80
    }

    [Flags]
    public enum MM2MonsterPower
    {
        SprayPoison = 0x00,
        SprayAcid = 0x01,
        Curse = 0x02,
        BreatheFire = 0x03,
        BreatheLightning = 0x04,
        BreatheCold = 0x05,
        BreatheEnergy = 0x06,
        BreatheGas = 0x07,
        BreatheAcid = 0x08,
        Explode = 0x09,
        PetrificationGaze = 0x0A,
        DrainMagic = 0x0B,
        DrainSpellLevel = 0x0C,
        VaporizeValuables = 0x0D,
        JuggleParty = 0x0E,
        EnergyBlast = 0x0F,
        Sleep = 0x10,
        LightningBolts = 0x11,
        Fireball = 0x12,
        FingersOfDeath = 0x13,
        Disintegration = 0x14,
        SuperShock = 0x15,
        DancingSword = 0x16,
        Incinerate = 0x17,
        InvokePower = 0x18,
        Implosion = 0x19,
        Inferno = 0x1A,
        Pain = 0x1B,
        Silence = 0x1C,
        Frenzy = 0x1D,
        Paralyze = 0x1E,
        Swarm = 0x1F,
        AllPowers = 0x1F,
        PowerChance = 0xE0,
    }

    [Flags]
    public enum MM2BraveryFlags
    {
        NumInGroup = 0x1f,
        BraveryValue = 0x60,
        AddFriends = 0x80
    }

    [Flags]
    public enum MM2AttackByte
    {
        NumAttacks = 0x0f,
        Level = 0xf0,
    }

    [Flags]
    public enum MM2ACByte
    {
        ArmorClass = 0x3f,
        Advance = 0x40,
        Regenerate = 0x80
    }

    [Flags]
    public enum MM2DamageByte
    {
        DamageValue = 0x3f,
        FireResist = 0x40,
        ElecResist = 0x80
    }
    
    [Flags]
    public enum MM2SpeedByte
    {
        SpeedValue = 0x3f,
        ColdResist = 0x40,
        AcidResist = 0x80
    }

    [Flags]
    public enum MM2MagicResistByte
    {
        SleepResist = 0x01,
        ParalyzeResist = 0x02,
        WeaponResist = 0x04,
        MaleResist = 0x08,
        FemaleResist = 0x10,
        MagicResist = 0xE0
    }

    [Flags]
    public enum MM2TreasureByte
    {
        Item = 0x03,
        Gems = 0x04,
        Gold = 0x18,
        BribeFood = 0x20,
        BribeMoney = 0x40,
        BribeGems = 0x80,
    }

    public class MM2MonsterList
    {
        private bool m_bValid = false;
        private string m_strError = string.Empty;
        private List<MM2Monster> m_monsters = new List<MM2Monster>();

        public List<MM2Monster> Monsters
        {
            get { return m_monsters; }
        }

        public MM2MonsterList(string strFile)
        {
            m_bValid = false;
            BinaryReader reader = null;
            int iMonsterIndex = 0;

            try
            {
                reader = new BinaryReader(File.OpenRead(strFile));
                while (reader.BaseStream.Position <= reader.BaseStream.Length - 26)
                {
                    byte[] bytes = reader.ReadBytes(26);
                    MM2Monster monster = new MM2Monster(bytes, 0, iMonsterIndex, 0, (MM2MonsterCombatStatus)0, false);
                    m_monsters.Add(monster);
                    iMonsterIndex++;
                }
                m_bValid = true;
            }
            catch (Exception ex)
            {
                m_strError = ex.Message;
            }
        }

        public bool Valid
        {
            get { return m_bValid; }
        }

        public string LastError
        {
            get { return m_strError; }
        }

        public string Dump()
        {
            StringBuilder sb = new StringBuilder(MM2Monster.GetHeaderString());
            foreach (MM2Monster item in m_monsters)
            {
                sb.Append(item.GetRowString());
            }
            return sb.ToString();
        }

        public MM2Monster GetItem(byte index)
        {
            if (m_monsters.Count > index)
                return m_monsters[index].Clone() as MM2Monster;
            return new MM2Monster();
        }

        public MM2MonsterList()
        {
            // Load the standard MM2 monster list
            m_monsters = new List<MM2Monster>(256);
            m_monsters.Add(new MM2Monster(0, "Creepy Crawler", 5, 4, 20, 2, 6, 35, 0, 48, 0, 6, 3, 0, 0, 0, 0, 0, 0, 0, 0, 150, 0, 0, 0, 1, 1));
            m_monsters.Add(new MM2Monster(1, "Giant Beetle", 10, 7, 15, 1, 10, 0, 0, 58, 0, 6, 3, 0, 0, 0, 0, 0, 0, 0, 0, 200, 0, 0, 0, 2, 1));
            m_monsters.Add(new MM2Monster(2, "Sewer Rat", 8, 2, 12, 1, 12, 36, 0, 0, 0, 8, 0, 0, 0, 0, 0, 1, 1, 0, 0, 150, 0, 0, 0, 4, 1));
            m_monsters.Add(new MM2Monster(3, "Kobold", 8, 6, 15, 2, 6, 0, 0, 0, 0, 10, 0, 0, 0, 0, 1, 1, 0, 1, 0, 200, 1, 1, 0, 5, 1));
            m_monsters.Add(new MM2Monster(4, "Old Miser", 1, 4, 12, 1, 4, 0, 194, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 1, 0, 60, 0, 1, 1, 7, 1));
            m_monsters.Add(new MM2Monster(5, "Goblin", 6, 6, 15, 1, 12, 0, 0, 0, 0, 12, 1, 0, 0, 0, 0, 1, 0, 1, 0, 200, 1, 1, 0, 8, 1));
            m_monsters.Add(new MM2Monster(6, "Cripple", 1, 1, 5, 2, 4, 36, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 1, 1, 1, 60, 0, 0, 0, 12, 1));
            m_monsters.Add(new MM2Monster(7, "Hungry Plant", 10, 4, 12, 1, 15, 35, 0, 48, 1, 6, 3, 0, 0, 0, 0, 0, 1, 0, 0, 300, 0, 0, 0, 155, 1));
            m_monsters.Add(new MM2Monster(8, "Merchant", 6, 5, 13, 1, 8, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 1, 1, 1, 1, 120, 1, 1, 0, 34, 1));
            m_monsters.Add(new MM2Monster(9, "Mugger", 10, 6, 16, 2, 6, 1, 0, 0, 0, 8, 0, 0, 0, 0, 0, 1, 0, 1, 0, 300, 1, 1, 1, 39, 1));
            m_monsters.Add(new MM2Monster(10, "Skeleton", 6, 5, 14, 1, 8, 128, 0, 61, 0, 12, 3, 1, 0, 0, 1, 0, 0, 0, 0, 200, 0, 1, 0, 45, 1));
            m_monsters.Add(new MM2Monster(11, "Flesh Eater", 6, 4, 22, 2, 6, 164, 0, 32, 0, 8, 1, 1, 0, 0, 1, 0, 1, 0, 0, 200, 0, 0, 0, 53, 1));
            m_monsters.Add(new MM2Monster(12, "Poltergeist", 8, 6, 25, 3, 4, 128, 0, 59, 2, 10, 1, 1, 0, 0, 1, 0, 0, 0, 0, 250, 0, 0, 0, 51, 1));
            m_monsters.Add(new MM2Monster(13, "Fool", 6, 4, 16, 1, 6, 0, 144, 0, 0, 6, 1, 0, 0, 0, 0, 0, 1, 1, 1, 150, 1, 1, 0, 60, 1));
            m_monsters.Add(new MM2Monster(14, "Witch's Cat", 4, 3, 18, 1, 6, 0, 0, 0, 0, 6, 1, 0, 0, 0, 1, 0, 1, 0, 0, 150, 0, 0, 0, 19, 1));
            m_monsters.Add(new MM2Monster(15, "Mini Rex", 10, 6, 12, 1, 12, 0, 0, 16, 0, 4, 2, 0, 0, 0, 1, 0, 1, 0, 0, 250, 0, 0, 0, 26, 1));
            m_monsters.Add(new MM2Monster(16, "Greedy Snitch", 12, 4, 16, 1, 8, 15, 0, 0, 0, 8, 1, 0, 0, 0, 0, 0, 0, 1, 0, 150, 1, 1, 1, 8, 1));
            m_monsters.Add(new MM2Monster(17, "Orc", 20, 6, 16, 1, 15, 64, 0, 0, 0, 8, 1, 0, 0, 1, 0, 1, 1, 1, 1, 250, 1, 1, 0, 11, 1));
            m_monsters.Add(new MM2Monster(18, "Beggar", 10, 4, 10, 2, 6, 2, 0, 0, 0, 12, 0, 0, 0, 0, 0, 0, 1, 1, 1, 250, 0, 1, 0, 12, 1));
            m_monsters.Add(new MM2Monster(19, "Sludge Beast", 20, 4, 18, 2, 6, 35, 97, 56, 1, 6, 3, 0, 0, 0, 0, 0, 0, 0, 0, 400, 0, 0, 0, 142, 4));
            m_monsters.Add(new MM2Monster(20, "Blood Sucker", 1, 10, 22, 1, 4, 54, 0, 0, 0, 10, 0, 0, 0, 0, 0, 0, 1, 0, 0, 300, 0, 0, 0, 16, 1));
            m_monsters.Add(new MM2Monster(21, "Venomous Snake", 12, 3, 19, 1, 10, 35, 0, 0, 0, 4, 2, 0, 0, 0, 0, 0, 1, 0, 0, 300, 0, 0, 0, 20, 1));
            m_monsters.Add(new MM2Monster(22, "Screaming Pods", 15, 4, 12, 2, 8, 40, 0, 0, 0, 4, 3, 0, 0, 0, 0, 0, 1, 0, 0, 400, 0, 0, 0, 27, 1));
            m_monsters.Add(new MM2Monster(23, "Man-at-arms", 20, 9, 14, 1, 16, 64, 0, 48, 0, 5, 1, 0, 0, 1, 0, 0, 0, 1, 0, 400, 1, 1, 0, 29, 1));
            m_monsters.Add(new MM2Monster(24, "Conjurer", 12, 3, 16, 1, 5, 0, 207, 32, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 1, 250, 0, 1, 1, 36, 6));
            m_monsters.Add(new MM2Monster(25, "Neophyte Thief", 14, 6, 16, 1, 6, 1, 0, 0, 0, 6, 0, 0, 0, 0, 0, 0, 0, 1, 0, 250, 0, 1, 1, 38, 1));
            m_monsters.Add(new MM2Monster(26, "Zombie", 20, 7, 2, 2, 8, 164, 0, 48, 1, 8, 3, 1, 0, 0, 0, 0, 0, 0, 0, 400, 0, 0, 0, 47, 1));
            m_monsters.Add(new MM2Monster(27, "Brain Eater", 10, 5, 18, 1, 10, 165, 208, 48, 2, 4, 3, 1, 0, 0, 0, 0, 0, 0, 0, 300, 0, 0, 1, 54, 1));
            m_monsters.Add(new MM2Monster(28, "Inept Wizard", 2, 2, 10, 1, 6, 0, 81, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 1, 200, 0, 1, 0, 37, 4));
            m_monsters.Add(new MM2Monster(29, "Phantasm", 12, 7, 20, 1, 20, 128, 0, 48, 0, 6, 3, 1, 0, 0, 0, 0, 0, 0, 0, 400, 0, 0, 0, 10, 1));
            m_monsters.Add(new MM2Monster(30, "Sprite", 12, 8, 19, 1, 6, 0, 130, 48, 0, 6, 2, 0, 0, 0, 0, 0, 0, 0, 1, 300, 1, 0, 1, 57, 4));
            m_monsters.Add(new MM2Monster(31, "Thug Trainee", 18, 7, 16, 2, 9, 64, 0, 0, 0, 8, 2, 0, 0, 1, 1, 1, 0, 1, 1, 300, 1, 1, 1, 39, 1));
            m_monsters.Add(new MM2Monster(32, "Hypnobeetle", 20, 8, 21, 2, 10, 35, 112, 32, 0, 6, 1, 0, 0, 0, 0, 0, 1, 0, 0, 600, 0, 0, 0, 2, 2));
            m_monsters.Add(new MM2Monster(33, "Rabid Rodent", 20, 3, 16, 2, 10, 36, 0, 0, 0, 6, 1, 0, 0, 0, 0, 0, 1, 0, 0, 400, 0, 0, 0, 4, 1));
            m_monsters.Add(new MM2Monster(34, "Gnome Elder", 20, 4, 19, 1, 8, 0, 219, 32, 2, 3, 1, 0, 0, 0, 0, 0, 0, 0, 1, 700, 1, 2, 1, 7, 6));
            m_monsters.Add(new MM2Monster(35, "Winged Steed", 30, 6, 18, 3, 8, 0, 131, 33, 3, 8, 1, 0, 0, 0, 0, 0, 0, 1, 0, 800, 0, 1, 1, 21, 2));
            m_monsters.Add(new MM2Monster(36, "Giant Lizard", 40, 8, 18, 1, 25, 0, 0, 0, 0, 12, 2, 0, 0, 0, 0, 0, 1, 0, 0, 800, 0, 0, 0, 26, 1));
            m_monsters.Add(new MM2Monster(37, "Foot Soldier", 35, 10, 15, 2, 12, 0, 0, 32, 0, 10, 1, 0, 0, 0, 0, 0, 0, 1, 0, 500, 1, 1, 0, 29, 1));
            m_monsters.Add(new MM2Monster(38, "Ranger", 28, 7, 17, 4, 6, 64, 0, 32, 1, 8, 1, 0, 0, 1, 0, 0, 0, 1, 0, 600, 2, 1, 0, 32, 1));
            m_monsters.Add(new MM2Monster(39, "Soldier", 25, 8, 15, 2, 10, 64, 0, 48, 0, 12, 1, 0, 0, 1, 0, 0, 0, 1, 0, 400, 2, 1, 0, 33, 1));
            m_monsters.Add(new MM2Monster(40, "Friar", 20, 3, 13, 2, 8, 0, 143, 48, 0, 6, 1, 0, 0, 0, 0, 0, 1, 0, 0, 400, 0, 0, 0, 34, 8));
            m_monsters.Add(new MM2Monster(41, "Burglar", 22, 5, 22, 2, 7, 17, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 1, 1, 600, 1, 2, 1, 38, 1));
            m_monsters.Add(new MM2Monster(42, "Killer Cadaver", 30, 6, 13, 2, 6, 164, 73, 48, 0, 6, 3, 1, 0, 0, 1, 0, 0, 0, 0, 700, 0, 0, 1, 50, 1));
            m_monsters.Add(new MM2Monster(43, "Ghoul", 25, 7, 15, 2, 8, 168, 0, 48, 0, 12, 1, 1, 0, 0, 1, 0, 0, 0, 0, 700, 0, 0, 0, 181, 1));
            m_monsters.Add(new MM2Monster(44, "Juggler", 20, 4, 26, 3, 6, 64, 174, 0, 4, 6, 1, 0, 0, 1, 0, 0, 0, 1, 0, 1500, 1, 2, 1, 60, 2));
            m_monsters.Add(new MM2Monster(45, "Carnage Spirit", 25, 8, 17, 3, 8, 182, 0, 112, 1, 5, 3, 1, 0, 0, 1, 0, 0, 0, 0, 1000, 0, 0, 0, 52, 1));
            m_monsters.Add(new MM2Monster(46, "Kobold Captain", 28, 8, 19, 2, 10, 64, 0, 16, 0, 6, 3, 0, 0, 1, 1, 1, 0, 1, 0, 600, 1, 1, 0, 5, 1));
            m_monsters.Add(new MM2Monster(47, "Brainless One", 20, 6, 15, 2, 8, 0, 0, 48, 0, 8, 1, 0, 0, 0, 0, 0, 1, 0, 0, 500, 0, 0, 0, 48, 1));
            m_monsters.Add(new MM2Monster(48, "Arachnoid", 45, 8, 22, 2, 15, 35, 0, 56, 0, 6, 3, 0, 0, 0, 1, 0, 0, 0, 0, 700, 0, 0, 0, 1, 1));
            m_monsters.Add(new MM2Monster(49, "Insect Plague", 35, 5, 17, 16, 2, 0, 127, 48, 0, 4, 3, 0, 0, 0, 0, 0, 1, 0, 0, 700, 0, 0, 0, 3, 5));
            m_monsters.Add(new MM2Monster(50, "Crazed Dwarf", 45, 7, 16, 2, 20, 0, 125, 32, 0, 8, 2, 0, 0, 0, 1, 0, 0, 1, 0, 1200, 1, 1, 1, 6, 1));
            m_monsters.Add(new MM2Monster(51, "Hermit", 30, 9, 14, 1, 15, 45, 0, 48, 0, 3, 1, 0, 0, 0, 0, 0, 1, 1, 1, 500, 2, 2, 1, 7, 1));
            m_monsters.Add(new MM2Monster(52, "Mutant Swine", 50, 8, 18, 2, 15, 0, 0, 0, 0, 10, 1, 0, 0, 0, 1, 0, 1, 0, 0, 800, 0, 0, 0, 11, 1));
            m_monsters.Add(new MM2Monster(53, "Swamp Dog", 40, 7, 18, 1, 20, 36, 0, 48, 0, 10, 1, 0, 0, 0, 1, 0, 1, 0, 0, 600, 0, 0, 0, 18, 1));
            m_monsters.Add(new MM2Monster(54, "Deadly Rattler", 40, 5, 20, 1, 30, 35, 0, 0, 0, 4, 3, 0, 0, 0, 0, 0, 1, 0, 0, 600, 0, 0, 0, 20, 1));
            m_monsters.Add(new MM2Monster(55, "Woodsman", 50, 10, 18, 2, 20, 64, 0, 32, 2, 18, 2, 0, 0, 1, 0, 1, 1, 1, 1, 1000, 1, 1, 0, 32, 1));
            m_monsters.Add(new MM2Monster(56, "Ninja", 35, 15, 27, 3, 12, 126, 0, 32, 3, 4, 2, 0, 0, 1, 1, 0, 0, 0, 0, 1200, 2, 1, 1, 40, 1));
            m_monsters.Add(new MM2Monster(57, "Squire", 40, 10, 22, 2, 15, 0, 0, 32, 0, 4, 2, 0, 0, 0, 1, 0, 0, 0, 0, 900, 1, 1, 0, 44, 1));
            m_monsters.Add(new MM2Monster(58, "Dancing Bones", 35, 4, 18, 2, 10, 128, 0, 112, 1, 10, 3, 1, 0, 0, 0, 0, 0, 0, 0, 500, 0, 1, 0, 45, 1));
            m_monsters.Add(new MM2Monster(59, "Dancing Dead", 45, 6, 12, 1, 16, 164, 0, 48, 2, 6, 3, 1, 0, 0, 1, 0, 0, 0, 0, 700, 0, 0, 0, 177, 1));
            m_monsters.Add(new MM2Monster(60, "Cursed Corpse", 60, 8, 16, 2, 10, 166, 0, 176, 1, 8, 3, 1, 0, 0, 1, 0, 0, 0, 0, 1200, 0, 0, 0, 50, 1));
            m_monsters.Add(new MM2Monster(61, "Nasty Witch", 38, 7, 22, 2, 12, 0, 219, 48, 3, 2, 1, 0, 0, 0, 0, 0, 0, 1, 1, 800, 2, 1, 1, 58, 6));
            m_monsters.Add(new MM2Monster(62, "Super Sprite", 40, 10, 22, 2, 15, 38, 130, 48, 2, 6, 2, 0, 0, 0, 0, 1, 0, 1, 1, 1000, 1, 1, 1, 57, 6));
            m_monsters.Add(new MM2Monster(63, "Cat Corpse", 40, 10, 20, 2, 18, 128, 0, 48, 1, 6, 3, 1, 0, 0, 0, 0, 0, 0, 0, 600, 0, 0, 0, 19, 1));
            m_monsters.Add(new MM2Monster(64, "Giant Scorpion", 60, 11, 22, 3, 20, 35, 0, 32, 0, 10, 2, 0, 0, 0, 1, 0, 0, 0, 0, 1300, 0, 0, 0, 2, 1));
            m_monsters.Add(new MM2Monster(65, "Killer Bees", 40, 9, 20, 16, 4, 35, 127, 48, 0, 4, 3, 0, 0, 0, 1, 0, 0, 0, 0, 1300, 0, 0, 0, 3, 4));
            m_monsters.Add(new MM2Monster(66, "Minor Demon", 50, 13, 18, 2, 20, 0, 111, 49, 2, 8, 2, 0, 1, 0, 1, 1, 0, 0, 0, 1200, 1, 1, 1, 9, 6));
            m_monsters.Add(new MM2Monster(67, "Hunchback", 35, 3, 10, 2, 12, 0, 0, 128, 0, 18, 1, 0, 1, 0, 0, 0, 1, 1, 1, 900, 0, 1, 0, 12, 1));
            m_monsters.Add(new MM2Monster(68, "Giant Ogre", 70, 8, 17, 2, 50, 0, 0, 48, 1, 6, 1, 0, 0, 0, 1, 0, 0, 1, 0, 1500, 2, 2, 0, 15, 1));
            m_monsters.Add(new MM2Monster(69, "Werebat", 35, 13, 24, 3, 10, 36, 0, 48, 2, 15, 1, 0, 0, 0, 1, 0, 0, 0, 0, 800, 0, 0, 0, 144, 1));
            m_monsters.Add(new MM2Monster(70, "Wind Mare", 50, 15, 28, 3, 12, 5, 0, 40, 3, 4, 2, 0, 1, 0, 1, 0, 0, 0, 0, 1000, 0, 2, 1, 21, 1));
            m_monsters.Add(new MM2Monster(71, "Werebull", 62, 10, 18, 1, 50, 36, 0, 32, 2, 4, 2, 0, 0, 0, 1, 0, 0, 0, 1, 1100, 1, 1, 1, 150, 1));
            m_monsters.Add(new MM2Monster(72, "Snapping Spore", 40, 6, 16, 2, 25, 0, 73, 32, 0, 4, 3, 0, 0, 0, 1, 0, 1, 0, 0, 900, 0, 1, 0, 27, 1));
            m_monsters.Add(new MM2Monster(73, "Cavalier", 70, 17, 21, 3, 20, 64, 0, 48, 1, 3, 3, 0, 0, 1, 1, 0, 0, 1, 0, 1400, 2, 2, 0, 31, 1));
            m_monsters.Add(new MM2Monster(74, "Druid", 40, 9, 22, 2, 12, 0, 188, 48, 3, 4, 1, 0, 0, 0, 0, 0, 0, 0, 1, 800, 1, 1, 1, 36, 2));
            m_monsters.Add(new MM2Monster(75, "Crazed Native", 30, 8, 20, 4, 15, 0, 125, 0, 4, 19, 2, 0, 0, 0, 1, 0, 1, 0, 0, 1000, 0, 1, 0, 42, 1));
            m_monsters.Add(new MM2Monster(76, "Coffin Creep", 50, 6, 6, 2, 10, 128, 107, 63, 3, 10, 3, 1, 1, 0, 1, 0, 0, 0, 0, 1100, 1, 1, 1, 49, 2));
            m_monsters.Add(new MM2Monster(77, "Gargoyle", 50, 10, 20, 3, 13, 40, 0, 57, 2, 12, 2, 0, 1, 0, 1, 0, 0, 0, 0, 1200, 1, 1, 1, 53, 1));
            m_monsters.Add(new MM2Monster(78, "Vampiric Rat", 45, 9, 22, 2, 14, 0, 0, 0, 0, 10, 3, 0, 0, 0, 1, 1, 1, 0, 0, 1100, 0, 0, 0, 4, 1));
            m_monsters.Add(new MM2Monster(79, "Cursed Slayer", 50, 13, 24, 3, 18, 102, 0, 48, 2, 6, 3, 0, 0, 1, 1, 0, 0, 0, 0, 1200, 1, 1, 0, 22, 1));
            m_monsters.Add(new MM2Monster(80, "Viking", 80, 14, 20, 3, 30, 0, 0, 32, 0, 6, 2, 0, 0, 0, 1, 0, 0, 1, 0, 2100, 1, 1, 1, 6, 1));
            m_monsters.Add(new MM2Monster(81, "Gnome", 40, 10, 18, 2, 12, 1, 0, 48, 3, 18, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1300, 1, 2, 1, 7, 1));
            m_monsters.Add(new MM2Monster(82, "Minor Devil", 60, 16, 19, 2, 40, 0, 114, 57, 2, 8, 2, 0, 1, 0, 1, 0, 0, 0, 0, 2400, 1, 1, 1, 10, 2));
            m_monsters.Add(new MM2Monster(83, "Warrior Boar", 60, 11, 21, 2, 30, 0, 0, 0, 0, 6, 2, 0, 0, 0, 1, 0, 1, 0, 0, 1900, 0, 0, 0, 11, 1));
            m_monsters.Add(new MM2Monster(84, "Cockatrice", 50, 10, 18, 3, 20, 43, 0, 57, 0, 4, 2, 0, 1, 0, 1, 0, 0, 1, 1, 2200, 2, 1, 1, 17, 1));
            m_monsters.Add(new MM2Monster(85, "Killer Canine", 50, 13, 20, 2, 50, 0, 0, 32, 0, 10, 1, 0, 0, 0, 1, 0, 1, 0, 0, 2000, 0, 0, 0, 18, 1));
            m_monsters.Add(new MM2Monster(86, "Killer Cobra", 50, 10, 19, 1, 80, 35, 0, 0, 0, 3, 2, 0, 0, 0, 1, 0, 1, 0, 0, 1600, 0, 0, 0, 20, 1));
            m_monsters.Add(new MM2Monster(87, "Champion", 80, 20, 22, 3, 30, 64, 0, 45, 2, 4, 2, 0, 0, 1, 0, 0, 0, 1, 0, 2700, 2, 2, 0, 28, 1));
            m_monsters.Add(new MM2Monster(88, "Gate Keeper", 60, 15, 13, 1, 40, 0, 0, 63, 2, 4, 3, 0, 0, 0, 1, 0, 1, 1, 1, 2100, 1, 1, 1, 157, 1));
            m_monsters.Add(new MM2Monster(89, "Shaman", 45, 8, 17, 1, 12, 0, 158, 48, 3, 4, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1800, 1, 1, 1, 34, 4));
            m_monsters.Add(new MM2Monster(90, "Illusionist", 45, 11, 21, 1, 10, 2, 108, 47, 1, 8, 1, 0, 0, 0, 0, 0, 0, 0, 0, 2100, 2, 1, 1, 37, 12));
            m_monsters.Add(new MM2Monster(91, "Mounted Patrol", 70, 22, 19, 4, 25, 64, 0, 32, 0, 12, 2, 0, 0, 1, 1, 1, 0, 1, 0, 2800, 2, 2, 1, 44, 1));
            m_monsters.Add(new MM2Monster(92, "Night Stalker", 60, 14, 15, 2, 30, 182, 0, 48, 0, 6, 3, 1, 1, 0, 1, 0, 0, 0, 0, 1900, 0, 2, 1, 46, 1));
            m_monsters.Add(new MM2Monster(93, "Wraith", 50, 10, 23, 5, 18, 184, 0, 48, 2, 10, 3, 1, 1, 0, 1, 0, 0, 0, 0, 3000, 0, 0, 0, 51, 1));
            m_monsters.Add(new MM2Monster(94, "Mad Peasant", 60, 13, 19, 2, 30, 0, 93, 32, 0, 12, 2, 0, 0, 0, 0, 1, 1, 1, 0, 2500, 0, 1, 0, 12, 1));
            m_monsters.Add(new MM2Monster(95, "Canine Creep", 64, 15, 24, 3, 20, 0, 0, 48, 0, 6, 3, 0, 0, 0, 1, 1, 0, 1, 0, 2400, 1, 2, 0, 5, 1));
            m_monsters.Add(new MM2Monster(96, "Dinobug", 100, 10, 18, 1, 80, 0, 0, 32, 0, 2, 2, 0, 0, 0, 1, 0, 1, 0, 0, 2000, 0, 1, 0, 2, 1));
            m_monsters.Add(new MM2Monster(97, "Swarming Wasps", 50, 15, 21, 16, 5, 35, 127, 0, 0, 5, 3, 0, 0, 0, 1, 0, 0, 0, 0, 3000, 0, 0, 0, 3, 4));
            m_monsters.Add(new MM2Monster(98, "Leprechaun", 40, 28, 22, 1, 20, 17, 141, 48, 6, 2, 1, 0, 1, 0, 1, 0, 0, 1, 1, 4000, 2, 3, 1, 8, 2));
            m_monsters.Add(new MM2Monster(99, "Flaming Fear", 70, 18, 18, 2, 20, 0, 163, 55, 1, 8, 3, 0, 1, 0, 1, 0, 0, 0, 0, 3200, 1, 0, 1, 137, 6));
            m_monsters.Add(new MM2Monster(100, "Leper", 40, 5, 8, 1, 10, 36, 0, 0, 0, 26, 1, 0, 0, 0, 0, 1, 1, 1, 1, 1500, 0, 1, 0, 12, 1));
            m_monsters.Add(new MM2Monster(101, "Troll", 70, 13, 18, 4, 30, 0, 0, 48, 0, 10, 2, 0, 1, 0, 1, 0, 0, 1, 1, 4000, 2, 2, 1, 141, 1));
            m_monsters.Add(new MM2Monster(102, "Acidic Blob", 60, 15, 7, 2, 30, 0, 97, 47, 0, 6, 3, 0, 0, 0, 1, 0, 0, 0, 0, 2100, 0, 0, 1, 142, 5));
            m_monsters.Add(new MM2Monster(103, "Werewolf", 70, 17, 21, 3, 25, 36, 0, 32, 1, 4, 2, 0, 1, 0, 1, 0, 0, 0, 0, 3200, 1, 2, 0, 146, 1));
            m_monsters.Add(new MM2Monster(104, "Pyro Hydra", 80, 15, 20, 3, 50, 0, 131, 49, 1, 2, 2, 0, 0, 0, 1, 0, 0, 0, 0, 5000, 2, 3, 1, 25, 5));
            m_monsters.Add(new MM2Monster(105, "Castle Guard", 70, 17, 19, 2, 32, 64, 0, 0, 0, 15, 1, 0, 0, 1, 0, 1, 0, 1, 1, 2600, 1, 2, 0, 33, 1));
            m_monsters.Add(new MM2Monster(106, "Thief", 50, 16, 26, 3, 16, 114, 0, 32, 0, 6, 1, 0, 0, 1, 1, 1, 0, 1, 1, 2800, 2, 2, 1, 39, 1));
            m_monsters.Add(new MM2Monster(107, "Warrior Maiden", 50, 19, 21, 3, 20, 0, 0, 64, 4, 15, 1, 0, 1, 0, 1, 0, 1, 1, 1, 5000, 0, 1, 0, 42, 1));
            m_monsters.Add(new MM2Monster(108, "Swamp Thing", 70, 11, 17, 2, 40, 36, 96, 30, 0, 4, 3, 0, 1, 0, 1, 0, 1, 1, 1, 4000, 0, 2, 1, 178, 2));
            m_monsters.Add(new MM2Monster(109, "Iron Wizard", 80, 21, 20, 2, 30, 0, 114, 48, 0, 3, 3, 0, 0, 0, 1, 0, 0, 0, 0, 5000, 0, 0, 1, 59, 10));
            m_monsters.Add(new MM2Monster(110, "Mutant", 70, 16, 16, 2, 30, 0, 79, 55, 3, 8, 3, 0, 1, 0, 1, 0, 0, 1, 0, 3200, 2, 2, 0, 175, 4));
            m_monsters.Add(new MM2Monster(111, "Strangler", 80, 18, 25, 3, 25, 41, 126, 48, 0, 4, 2, 0, 0, 0, 1, 0, 0, 0, 0, 4000, 2, 2, 0, 48, 4));
            m_monsters.Add(new MM2Monster(112, "Dwarven Knight", 100, 23, 24, 4, 30, 64, 0, 57, 3, 4, 2, 0, 0, 1, 1, 0, 0, 0, 1, 5000, 2, 3, 1, 6, 1));
            m_monsters.Add(new MM2Monster(113, "Horned Fiend", 80, 18, 19, 3, 20, 55, 74, 57, 1, 8, 3, 0, 1, 0, 1, 0, 0, 0, 0, 6000, 0, 2, 1, 10, 2));
            m_monsters.Add(new MM2Monster(114, "Swamp Beast", 100, 11, 17, 4, 19, 0, 97, 50, 0, 4, 3, 0, 0, 0, 1, 0, 1, 1, 1, 5000, 0, 2, 1, 141, 3));
            m_monsters.Add(new MM2Monster(115, "Hill Giant", 120, 17, 21, 2, 70, 64, 0, 57, 0, 6, 2, 0, 0, 1, 1, 0, 0, 1, 0, 5000, 2, 2, 0, 15, 1));
            m_monsters.Add(new MM2Monster(116, "Wyvern", 100, 15, 21, 3, 40, 35, 0, 48, 0, 12, 2, 0, 0, 0, 1, 0, 0, 1, 0, 5000, 2, 3, 1, 23, 1));
            m_monsters.Add(new MM2Monster(117, "Earth Wyrm", 130, 19, 23, 3, 60, 0, 99, 41, 2, 4, 2, 0, 1, 0, 1, 0, 0, 0, 0, 7000, 2, 3, 1, 24, 5));
            m_monsters.Add(new MM2Monster(118, "White Knight", 100, 18, 27, 4, 32, 64, 0, 185, 5, 3, 2, 0, 0, 1, 1, 0, 0, 1, 0, 8000, 2, 2, 0, 31, 1));
            m_monsters.Add(new MM2Monster(119, "Necromancer", 60, 13, 31, 1, 15, 0, 179, 48, 3, 3, 1, 0, 0, 0, 0, 0, 0, 1, 1, 5000, 2, 2, 1, 36, 5));
            m_monsters.Add(new MM2Monster(120, "Mountain Man", 90, 11, 21, 3, 23, 64, 0, 40, 0, 18, 1, 0, 0, 1, 1, 0, 1, 1, 1, 4000, 1, 1, 0, 43, 1));
            m_monsters.Add(new MM2Monster(121, "Gravewalker", 70, 15, 23, 2, 20, 185, 0, 57, 2, 6, 3, 1, 1, 0, 1, 0, 0, 0, 0, 5000, 0, 0, 1, 50, 1));
            m_monsters.Add(new MM2Monster(122, "Phantom", 64, 19, 21, 2, 30, 189, 43, 63, 2, 4, 3, 1, 1, 0, 1, 0, 0, 0, 0, 6000, 0, 0, 1, 51, 9));
            m_monsters.Add(new MM2Monster(123, "Lost Soul", 80, 18, 25, 2, 25, 180, 172, 63, 2, 2, 3, 1, 1, 0, 1, 0, 0, 0, 0, 6000, 0, 1, 1, 52, 16));
            m_monsters.Add(new MM2Monster(124, "Slasher", 60, 11, 20, 4, 20, 158, 0, 48, 0, 8, 3, 1, 1, 0, 1, 0, 0, 0, 0, 6000, 1, 1, 1, 181, 1));
            m_monsters.Add(new MM2Monster(125, "Guardian", 150, 13, 15, 1, 50, 0, 39, 63, 4, 4, 3, 0, 1, 0, 1, 0, 0, 0, 0, 8000, 0, 0, 1, 54, 3));
            m_monsters.Add(new MM2Monster(126, "Seductress", 60, 9, 26, 1, 10, 40, 126, 112, 3, 2, 1, 0, 0, 0, 0, 0, 0, 1, 1, 8000, 2, 2, 1, 58, 10));
            m_monsters.Add(new MM2Monster(127, "Pixie", 90, 20, 24, 2, 30, 50, 66, 56, 3, 6, 2, 0, 1, 0, 0, 0, 0, 0, 1, 6000, 2, 1, 1, 57, 2));
            m_monsters.Add(new MM2Monster(128, "Lightning Bugs", 80, 19, 25, 10, 10, 0, 100, 60, 0, 4, 3, 0, 0, 0, 1, 0, 0, 0, 0, 9000, 0, 0, 0, 3, 5));
            m_monsters.Add(new MM2Monster(129, "Trickster", 90, 19, 21, 3, 20, 18, 0, 32, 0, 3, 1, 0, 0, 0, 1, 0, 0, 1, 1, 7000, 2, 2, 1, 8, 1));
            m_monsters.Add(new MM2Monster(130, "Griffin", 150, 20, 22, 5, 25, 43, 0, 57, 3, 4, 2, 0, 1, 0, 1, 0, 0, 1, 1, 13000, 2, 2, 1, 17, 1));
            m_monsters.Add(new MM2Monster(131, "Pegasus", 120, 26, 29, 3, 40, 0, 0, 63, 5, 8, 2, 0, 1, 0, 1, 0, 1, 1, 1, 11000, 2, 3, 1, 21, 1));
            m_monsters.Add(new MM2Monster(132, "Gorgon", 150, 14, 23, 3, 30, 0, 106, 41, 2, 10, 2, 0, 0, 0, 1, 0, 0, 1, 0, 13000, 2, 2, 0, 22, 6));
            m_monsters.Add(new MM2Monster(133, "Cloud Dragon", 160, 19, 26, 5, 30, 0, 133, 60, 2, 3, 2, 0, 1, 0, 1, 0, 0, 1, 1, 13000, 2, 3, 1, 23, 6));
            m_monsters.Add(new MM2Monster(134, "Troubadour", 120, 16, 18, 2, 30, 0, 0, 63, 0, 3, 2, 0, 0, 0, 1, 0, 0, 1, 1, 7000, 2, 2, 1, 28, 1));
            m_monsters.Add(new MM2Monster(135, "Paladin", 120, 24, 26, 5, 30, 0, 0, 61, 2, 4, 2, 0, 0, 0, 1, 0, 0, 1, 1, 11000, 2, 2, 1, 30, 1));
            m_monsters.Add(new MM2Monster(136, "Elf Warrior", 120, 22, 32, 4, 20, 64, 0, 32, 0, 12, 2, 0, 0, 1, 0, 0, 0, 1, 1, 6000, 2, 2, 0, 32, 1));
            m_monsters.Add(new MM2Monster(137, "Priest", 100, 20, 21, 2, 12, 0, 190, 32, 0, 4, 1, 0, 0, 0, 0, 0, 0, 1, 1, 6000, 2, 2, 1, 34, 8));
            m_monsters.Add(new MM2Monster(138, "Assassin", 100, 22, 28, 2, 80, 62, 0, 32, 0, 3, 2, 0, 0, 0, 1, 0, 0, 0, 0, 10000, 2, 2, 0, 40, 1));
            m_monsters.Add(new MM2Monster(139, "Amazon", 90, 12, 24, 2, 30, 64, 0, 0, 0, 10, 1, 0, 0, 1, 1, 0, 1, 1, 1, 5000, 0, 1, 0, 42, 1));
            m_monsters.Add(new MM2Monster(140, "Grim Reaper", 70, 16, 14, 2, 25, 170, 115, 61, 4, 4, 3, 1, 1, 0, 1, 0, 0, 0, 0, 10000, 2, 2, 1, 46, 16));
            m_monsters.Add(new MM2Monster(141, "Mummy", 150, 11, 10, 2, 50, 164, 0, 60, 3, 6, 3, 1, 1, 0, 1, 0, 0, 0, 0, 8000, 0, 0, 1, 49, 1));
            m_monsters.Add(new MM2Monster(142, "Bonehead", 90, 20, 16, 3, 30, 166, 73, 48, 3, 6, 3, 1, 1, 0, 1, 0, 0, 0, 0, 7000, 1, 2, 1, 173, 1));
            m_monsters.Add(new MM2Monster(143, "Melting Man", 130, 22, 20, 3, 30, 40, 0, 55, 0, 6, 2, 0, 0, 0, 1, 0, 0, 0, 0, 9000, 2, 2, 0, 47, 1));
            m_monsters.Add(new MM2Monster(144, "Demon Soldier", 200, 22, 19, 5, 50, 64, 0, 57, 2, 19, 2, 0, 1, 1, 1, 0, 0, 0, 0, 18000, 2, 3, 1, 5, 1));
            m_monsters.Add(new MM2Monster(145, "Fire Devil", 150, 22, 19, 3, 60, 64, 146, 57, 3, 6, 2, 0, 1, 1, 1, 0, 0, 0, 0, 18000, 1, 2, 1, 9, 8));
            m_monsters.Add(new MM2Monster(146, "Apparition", 100, 20, 21, 3, 30, 180, 0, 48, 5, 4, 3, 1, 1, 0, 1, 0, 0, 0, 0, 11000, 0, 2, 1, 10, 1));
            m_monsters.Add(new MM2Monster(147, "Vampire", 250, 24, 16, 3, 60, 184, 113, 63, 4, 6, 2, 1, 1, 0, 1, 0, 0, 0, 0, 25000, 2, 3, 1, 144, 10));
            m_monsters.Add(new MM2Monster(148, "Frost Dragon", 250, 22, 16, 5, 40, 0, 133, 58, 2, 3, 2, 0, 1, 0, 1, 0, 0, 1, 0, 22000, 3, 3, 1, 23, 8));
            m_monsters.Add(new MM2Monster(149, "Dinosaur", 250, 16, 21, 2, 100, 0, 0, 32, 0, 6, 1, 0, 0, 0, 1, 0, 0, 0, 0, 16000, 0, 2, 0, 26, 1));
            m_monsters.Add(new MM2Monster(150, "Avenger", 160, 23, 23, 4, 25, 64, 0, 32, 2, 2, 2, 0, 0, 1, 1, 0, 0, 0, 0, 15000, 1, 2, 0, 31, 1));
            m_monsters.Add(new MM2Monster(151, "Court Bowman", 150, 25, 23, 6, 40, 64, 0, 32, 0, 12, 2, 0, 0, 1, 0, 1, 0, 1, 1, 20000, 2, 2, 0, 33, 1));
            m_monsters.Add(new MM2Monster(152, "Holy Man", 100, 20, 22, 2, 15, 0, 120, 48, 2, 2, 1, 0, 0, 0, 0, 0, 0, 1, 1, 20000, 2, 2, 1, 35, 2));
            m_monsters.Add(new MM2Monster(153, "Court Mage", 100, 19, 40, 1, 20, 0, 213, 57, 2, 4, 2, 0, 0, 0, 0, 0, 0, 1, 1, 22000, 1, 2, 1, 37, 16));
            m_monsters.Add(new MM2Monster(154, "Warlock", 90, 20, 32, 2, 19, 0, 180, 53, 5, 6, 2, 0, 0, 0, 0, 0, 0, 1, 1, 21000, 1, 2, 1, 37, 12));
            m_monsters.Add(new MM2Monster(155, "Barbarian", 200, 16, 23, 5, 30, 64, 0, 41, 2, 19, 2, 0, 0, 1, 1, 0, 1, 1, 1, 16000, 1, 2, 0, 42, 1));
            m_monsters.Add(new MM2Monster(156, "Royal Horseman", 250, 32, 23, 6, 40, 0, 0, 48, 0, 6, 2, 0, 0, 0, 1, 0, 0, 0, 0, 24000, 2, 2, 1, 44, 1));
            m_monsters.Add(new MM2Monster(157, "Court Jester", 80, 17, 24, 3, 20, 124, 174, 48, 6, 2, 1, 0, 1, 1, 1, 1, 0, 0, 1, 25000, 3, 2, 1, 60, 5));
            m_monsters.Add(new MM2Monster(158, "Fire Faery", 230, 22, 28, 3, 40, 50, 131, 53, 3, 6, 2, 0, 1, 0, 0, 1, 0, 0, 1, 20000, 3, 2, 1, 57, 8));
            m_monsters.Add(new MM2Monster(159, "Thug Leader", 220, 22, 23, 4, 30, 105, 0, 16, 0, 8, 2, 0, 0, 1, 1, 1, 0, 1, 0, 20000, 2, 2, 0, 39, 1));
            m_monsters.Add(new MM2Monster(160, "Dino Spider", 250, 20, 20, 2, 100, 35, 96, 32, 0, 4, 3, 0, 0, 0, 1, 0, 0, 0, 0, 20000, 0, 2, 0, 1, 5));
            m_monsters.Add(new MM2Monster(161, "Plant Golem", 250, 30, 14, 2, 60, 0, 0, 48, 4, 4, 3, 0, 1, 0, 1, 0, 0, 0, 0, 30000, 1, 0, 1, 13, 1));
            m_monsters.Add(new MM2Monster(162, "Stone Golem", 250, 30, 18, 2, 70, 0, 0, 48, 5, 2, 3, 0, 1, 0, 1, 0, 0, 0, 0, 40000, 1, 0, 1, 15, 1));
            m_monsters.Add(new MM2Monster(163, "War Eagle", 300, 21, 28, 3, 70, 0, 0, 41, 0, 3, 2, 0, 1, 0, 1, 0, 0, 0, 0, 25000, 1, 2, 0, 17, 1));
            m_monsters.Add(new MM2Monster(164, "Guardian Hound", 200, 15, 32, 2, 80, 0, 104, 32, 2, 6, 3, 0, 1, 0, 1, 1, 0, 0, 0, 20000, 0, 0, 0, 18, 8));
            m_monsters.Add(new MM2Monster(165, "Minotaur", 150, 30, 40, 2, 80, 23, 114, 61, 3, 2, 2, 0, 1, 0, 1, 0, 0, 0, 0, 30000, 2, 3, 1, 150, 5));
            m_monsters.Add(new MM2Monster(166, "Fire Dragon", 300, 25, 40, 5, 50, 0, 163, 53, 3, 3, 2, 0, 1, 0, 1, 0, 0, 0, 0, 40000, 3, 3, 1, 24, 10));
            m_monsters.Add(new MM2Monster(167, "Shadow Rogue", 150, 23, 32, 6, 30, 113, 0, 57, 0, 4, 2, 0, 0, 1, 1, 0, 0, 1, 1, 25000, 2, 2, 1, 38, 1));
            m_monsters.Add(new MM2Monster(168, "Crusader", 200, 29, 40, 5, 40, 0, 0, 48, 4, 5, 2, 0, 1, 0, 1, 0, 0, 1, 1, 20000, 2, 2, 0, 30, 1));
            m_monsters.Add(new MM2Monster(169, "Chancellor", 90, 20, 28, 1, 20, 0, 181, 32, 5, 2, 1, 0, 0, 0, 0, 0, 1, 1, 1, 30000, 1, 2, 1, 35, 16));
            m_monsters.Add(new MM2Monster(170, "Ghost", 200, 17, 40, 2, 30, 181, 0, 59, 1, 4, 3, 1, 1, 0, 1, 0, 0, 0, 0, 40000, 0, 1, 1, 51, 1));
            m_monsters.Add(new MM2Monster(171, "Dead Head", 250, 15, 23, 2, 50, 27, 0, 48, 0, 4, 3, 0, 1, 0, 1, 0, 0, 0, 0, 25000, 1, 0, 1, 54, 1));
            m_monsters.Add(new MM2Monster(172, "Enchantress", 100, 13, 50, 1, 25, 29, 182, 57, 5, 2, 2, 0, 0, 0, 0, 0, 0, 1, 1, 25000, 2, 2, 1, 58, 10));
            m_monsters.Add(new MM2Monster(173, "Warbot", 300, 25, 50, 3, 60, 0, 212, 63, 3, 8, 3, 0, 0, 0, 1, 0, 0, 0, 0, 40000, 0, 0, 1, 59, 16));
            m_monsters.Add(new MM2Monster(174, "Stalker", 140, 24, 40, 3, 40, 0, 0, 48, 0, 8, 2, 0, 1, 0, 0, 1, 1, 0, 0, 30000, 0, 0, 0, 19, 1));
            m_monsters.Add(new MM2Monster(175, "Hatchet Man", 200, 25, 40, 5, 32, 94, 0, 48, 0, 4, 2, 0, 0, 1, 1, 0, 0, 0, 0, 23000, 2, 3, 0, 43, 1));
            m_monsters.Add(new MM2Monster(176, "Dwarven Elder", 300, 24, 32, 4, 80, 0, 0, 41, 3, 2, 2, 0, 1, 0, 1, 0, 0, 1, 1, 70000, 2, 3, 1, 6, 1));
            m_monsters.Add(new MM2Monster(177, "Ooze Warrior", 350, 22, 24, 3, 70, 56, 97, 47, 0, 6, 3, 0, 1, 0, 1, 0, 0, 0, 0, 60000, 0, 0, 0, 142, 10));
            m_monsters.Add(new MM2Monster(178, "Roc", 400, 21, 60, 3, 100, 0, 0, 48, 0, 2, 2, 0, 1, 0, 1, 0, 1, 1, 1, 70000, 2, 2, 0, 17, 1));
            m_monsters.Add(new MM2Monster(179, "Dagger Jaw", 300, 22, 40, 2, 150, 3, 0, 57, 0, 2, 3, 0, 1, 0, 1, 0, 0, 0, 0, 60000, 1, 2, 0, 20, 1));
            m_monsters.Add(new MM2Monster(180, "Armored Dragon", 400, 31, 40, 5, 80, 0, 199, 57, 4, 2, 2, 0, 1, 0, 1, 0, 0, 0, 1, 80000, 3, 3, 1, 23, 10));
            m_monsters.Add(new MM2Monster(181, "Tyrannosaurus", 500, 24, 24, 3, 90, 0, 0, 32, 0, 4, 3, 0, 0, 0, 1, 0, 0, 0, 0, 70000, 0, 2, 1, 26, 1));
            m_monsters.Add(new MM2Monster(182, "Valiant Knight", 300, 32, 60, 6, 50, 64, 0, 57, 4, 4, 3, 0, 1, 1, 1, 0, 0, 1, 1, 80000, 3, 2, 1, 28, 1));
            m_monsters.Add(new MM2Monster(183, "Endless Knight", 300, 50, 50, 8, 50, 0, 0, 41, 0, 10, 3, 0, 1, 0, 1, 0, 0, 0, 0, 80000, 3, 0, 1, 29, 1));
            m_monsters.Add(new MM2Monster(184, "Archer", 250, 31, 50, 6, 50, 64, 81, 48, 2, 6, 2, 0, 0, 1, 0, 0, 0, 1, 1, 80000, 2, 2, 0, 33, 6));
            m_monsters.Add(new MM2Monster(185, "Wizard", 150, 22, 60, 1, 25, 0, 215, 32, 4, 3, 2, 0, 0, 0, 0, 0, 0, 1, 1, 70000, 2, 2, 1, 37, 12));
            m_monsters.Add(new MM2Monster(186, "Crypt Fiend", 150, 32, 24, 3, 40, 150, 211, 61, 3, 12, 3, 1, 1, 0, 1, 0, 0, 0, 0, 80000, 2, 2, 1, 49, 16));
            m_monsters.Add(new MM2Monster(187, "Phase Spirit", 200, 60, 40, 4, 40, 157, 171, 63, 3, 18, 3, 1, 1, 0, 1, 0, 0, 0, 0, 60000, 1, 2, 1, 52, 3));
            m_monsters.Add(new MM2Monster(188, "Sorceress", 150, 18, 50, 1, 25, 0, 183, 57, 3, 3, 2, 0, 0, 0, 0, 0, 0, 1, 1, 70000, 2, 3, 1, 58, 16));
            m_monsters.Add(new MM2Monster(189, "Mystic Clown", 100, 16, 32, 3, 30, 63, 174, 57, 6, 4, 2, 0, 1, 0, 1, 1, 1, 1, 1, 120000, 3, 2, 1, 60, 8));
            m_monsters.Add(new MM2Monster(190, "Spido Bug", 300, 22, 40, 3, 70, 35, 0, 6, 0, 6, 3, 0, 0, 0, 0, 0, 0, 0, 0, 70000, 0, 0, 0, 129, 1));
            m_monsters.Add(new MM2Monster(191, "Living Dead", 180, 50, 32, 4, 70, 170, 147, 55, 4, 8, 3, 1, 1, 0, 1, 0, 0, 0, 0, 60000, 1, 1, 1, 175, 10));
            m_monsters.Add(new MM2Monster(192, "Devil's Mouse", 500, 31, 32, 3, 120, 41, 0, 57, 4, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 180000, 2, 2, 0, 132, 1));
            m_monsters.Add(new MM2Monster(193, "Fire Elemental", 250, 26, 30, 6, 50, 0, 99, 55, 4, 2, 3, 0, 1, 0, 1, 0, 0, 0, 0, 180000, 2, 2, 1, 9, 8));
            m_monsters.Add(new MM2Monster(194, "Air Elemental", 250, 26, 30, 6, 50, 0, 133, 56, 4, 2, 3, 0, 1, 0, 1, 0, 0, 0, 0, 180000, 2, 2, 1, 52, 6));
            m_monsters.Add(new MM2Monster(195, "Mist Rider", 350, 50, 70, 8, 30, 0, 101, 62, 3, 4, 2, 0, 1, 0, 1, 0, 0, 1, 1, 230000, 2, 2, 1, 21, 8));
            m_monsters.Add(new MM2Monster(196, "Magic Serpent", 800, 40, 70, 2, 120, 0, 167, 57, 4, 2, 3, 0, 1, 0, 1, 0, 0, 0, 0, 250000, 3, 3, 1, 24, 8));
            m_monsters.Add(new MM2Monster(197, "Cron Man Trap", 400, 21, 30, 4, 40, 0, 73, 112, 0, 4, 3, 0, 0, 0, 0, 0, 1, 0, 0, 230000, 0, 3, 0, 27, 1));
            m_monsters.Add(new MM2Monster(198, "Dark Knight", 700, 60, 80, 10, 40, 122, 0, 63, 3, 6, 3, 0, 1, 1, 1, 0, 0, 0, 0, 250000, 2, 2, 0, 156, 1));
            m_monsters.Add(new MM2Monster(199, "Sorcerer", 300, 24, 70, 2, 30, 0, 218, 57, 3, 4, 2, 0, 0, 0, 0, 0, 0, 1, 1, 150000, 2, 3, 1, 36, 12));
            m_monsters.Add(new MM2Monster(200, "Kensai", 500, 40, 60, 8, 32, 64, 0, 48, 0, 6, 3, 0, 0, 1, 1, 0, 1, 1, 1, 190000, 2, 2, 1, 40, 1));
            m_monsters.Add(new MM2Monster(201, "Jouster", 500, 50, 40, 4, 80, 0, 0, 48, 1, 2, 3, 0, 0, 0, 1, 0, 0, 1, 1, 210000, 3, 2, 1, 44, 1));
            m_monsters.Add(new MM2Monster(202, "Devil's Envoy", 500, 40, 32, 5, 50, 22, 114, 57, 4, 4, 3, 0, 1, 0, 1, 0, 0, 0, 0, 220000, 1, 2, 1, 174, 6));
            m_monsters.Add(new MM2Monster(203, "Ethereal Being", 250, 70, 100, 10, 30, 58, 0, 63, 1, 3, 3, 0, 1, 0, 1, 0, 0, 0, 0, 240000, 1, 2, 1, 52, 1));
            m_monsters.Add(new MM2Monster(204, "Death's Agent", 600, 40, 32, 9, 50, 20, 145, 63, 2, 4, 3, 0, 1, 0, 1, 0, 0, 0, 0, 230000, 2, 2, 1, 182, 16));
            m_monsters.Add(new MM2Monster(205, "Alien Probe", 500, 31, 40, 4, 50, 0, 215, 61, 4, 4, 3, 0, 0, 0, 1, 0, 0, 0, 0, 230000, 0, 0, 0, 59, 16));
            m_monsters.Add(new MM2Monster(206, "Element Hydra", 600, 40, 50, 8, 40, 0, 102, 63, 3, 4, 3, 0, 1, 0, 1, 0, 0, 0, 0, 230000, 2, 1, 1, 25, 6));
            m_monsters.Add(new MM2Monster(207, "Monster Masher", 500, 40, 50, 6, 60, 42, 116, 48, 0, 4, 3, 0, 1, 0, 1, 0, 0, 1, 1, 250000, 2, 2, 1, 48, 4));
            m_monsters.Add(new MM2Monster(208, "Devil King", 5000, 60, 110, 6, 250, 44, 57, 63, 7, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 30000000, 3, 3, 1, 138, 16));
            m_monsters.Add(new MM2Monster(209, "Titan", 2000, 40, 60, 4, 120, 41, 150, 57, 6, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 1500000, 3, 3, 1, 143, 16));
            m_monsters.Add(new MM2Monster(210, "Ancient Dragon", 5000, 50, 70, 8, 200, 0, 166, 57, 5, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 20000000, 3, 3, 1, 152, 16));
            m_monsters.Add(new MM2Monster(211, "Reptoid", 2500, 32, 40, 10, 50, 0, 0, 57, 1, 3, 3, 0, 1, 0, 1, 0, 1, 0, 0, 1000000, 1, 2, 0, 26, 1));
            m_monsters.Add(new MM2Monster(212, "Cuisinart", 1000, 60, 160, 16, 250, 0, 61, 48, 2, 4, 3, 0, 1, 0, 1, 0, 0, 1, 0, 20000000, 3, 3, 1, 156, 1));
            m_monsters.Add(new MM2Monster(213, "Holy Warrior", 1000, 80, 80, 12, 60, 0, 0, 63, 3, 2, 3, 0, 1, 0, 1, 0, 0, 1, 1, 3000000, 3, 3, 1, 30, 1));
            m_monsters.Add(new MM2Monster(214, "Elven Archer", 1000, 40, 90, 14, 40, 64, 0, 57, 3, 3, 3, 0, 0, 1, 1, 0, 0, 1, 1, 1500000, 3, 3, 1, 32, 1));
            m_monsters.Add(new MM2Monster(215, "High Priest", 1000, 32, 40, 4, 30, 0, 120, 57, 4, 2, 3, 0, 0, 0, 0, 0, 0, 1, 1, 3000000, 2, 3, 1, 35, 6));
            m_monsters.Add(new MM2Monster(216, "Master Robber", 1000, 40, 70, 8, 40, 83, 173, 48, 3, 4, 3, 0, 0, 1, 1, 0, 0, 1, 1, 2000000, 2, 3, 1, 38, 16));
            m_monsters.Add(new MM2Monster(217, "Master Ninja", 1000, 60, 110, 12, 40, 126, 0, 48, 4, 3, 3, 0, 0, 1, 1, 0, 0, 0, 0, 2000000, 2, 2, 1, 40, 1));
            m_monsters.Add(new MM2Monster(218, "Lich Lord", 2000, 40, 50, 4, 50, 172, 121, 61, 6, 1, 3, 1, 1, 0, 1, 0, 0, 0, 0, 6000000, 3, 3, 1, 174, 16));
            m_monsters.Add(new MM2Monster(219, "Time Lord", 3000, 110, 210, 12, 150, 31, 120, 63, 7, 2, 3, 0, 1, 0, 0, 0, 0, 0, 0, 15000000, 3, 3, 1, 180, 16));
            m_monsters.Add(new MM2Monster(220, "Orc God", 50000, 40, 70, 4, 200, 0, 0, 63, 0, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 15000000, 3, 3, 1, 139, 1));
            m_monsters.Add(new MM2Monster(221, "Death in a Box", 2000, 40, 60, 8, 100, 44, 180, 59, 3, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 9000000, 2, 3, 1, 59, 16));
            m_monsters.Add(new MM2Monster(222, "Mega Troll", 2500, 50, 60, 8, 170, 0, 0, 63, 2, 2, 3, 0, 1, 0, 1, 0, 0, 0, 0, 4000000, 3, 3, 0, 141, 1));
            m_monsters.Add(new MM2Monster(223, "Cat from Hell", 2000, 40, 70, 6, 100, 59, 120, 55, 5, 3, 3, 0, 1, 0, 1, 0, 1, 0, 0, 10000000, 2, 3, 1, 19, 7));
            m_monsters.Add(new MM2Monster(224, "Earth Elemental", 250, 26, 30, 6, 50, 0, 104, 54, 4, 2, 0, 0, 1, 0, 1, 0, 0, 0, 0, 180000, 2, 2, 1, 54, 8));
            m_monsters.Add(new MM2Monster(225, "Water Elemental", 250, 26, 30, 6, 50, 0, 101, 62, 4, 2, 3, 0, 1, 0, 1, 0, 0, 0, 0, 180000, 2, 2, 1, 14, 8));
            m_monsters.Add(new MM2Monster(226, "Gnasher", 25, 8, 22, 2, 10, 0, 0, 33, 0, 12, 2, 0, 0, 0, 0, 1, 1, 0, 0, 700, 0, 0, 0, 55, 1));
            m_monsters.Add(new MM2Monster(227, "Chomper", 50, 15, 26, 4, 8, 0, 93, 49, 0, 12, 3, 0, 0, 0, 1, 1, 1, 0, 0, 1200, 1, 0, 0, 55, 1));
            m_monsters.Add(new MM2Monster(228, "Mutant Fish", 6, 6, 20, 1, 12, 35, 0, 1, 0, 14, 2, 0, 0, 0, 0, 1, 1, 0, 0, 200, 0, 0, 0, 55, 1));
            m_monsters.Add(new MM2Monster(229, "Sea Monster", 70, 16, 18, 3, 30, 0, 74, 49, 0, 3, 2, 0, 1, 0, 0, 0, 1, 0, 0, 4000, 2, 1, 0, 183, 4));
            m_monsters.Add(new MM2Monster(230, "Aquasaurus", 160, 25, 20, 2, 70, 0, 0, 49, 0, 20, 2, 0, 0, 0, 1, 0, 0, 0, 0, 9000, 0, 2, 0, 55, 1));
            m_monsters.Add(new MM2Monster(231, "Cosmic Sludge", 130, 25, 20, 3, 30, 36, 0, 59, 0, 6, 3, 0, 0, 0, 1, 0, 0, 1, 0, 11000, 2, 2, 0, 14, 1));
            m_monsters.Add(new MM2Monster(232, "Sarakin", 250, 25, 40, 2, 40, 53, 74, 63, 3, 1, 3, 0, 1, 0, 0, 0, 0, 1, 1, 150000, 2, 3, 1, 51, 5));
            m_monsters.Add(new MM2Monster(233, "The Long One", 300, 30, 50, 3, 70, 119, 120, 191, 1, 1, 3, 0, 1, 1, 1, 0, 0, 0, 1, 400000, 3, 2, 1, 24, 4));
            m_monsters.Add(new MM2Monster(234, "Spaz Twit", 50, 200, 40, 4, 40, 64, 148, 63, 3, 1, 3, 0, 0, 1, 0, 0, 0, 1, 0, 120000, 2, 2, 1, 28, 6));
            m_monsters.Add(new MM2Monster(235, "The Snowbeast", 60, 16, 21, 4, 25, 0, 0, 56, 0, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0, 3000, 2, 2, 1, 181, 16));
            m_monsters.Add(new MM2Monster(236, "Bozorc the Orc", 200, 25, 28, 4, 40, 65, 0, 57, 0, 1, 2, 0, 0, 1, 1, 0, 0, 1, 1, 20000, 3, 3, 1, 11, 16));
            m_monsters.Add(new MM2Monster(237, "Brutal Bruno", 300, 30, 40, 6, 50, 64, 0, 48, 2, 1, 3, 0, 0, 1, 1, 0, 1, 1, 0, 60000, 2, 2, 0, 43, 16));
            m_monsters.Add(new MM2Monster(238, "Death Spider", 90, 19, 23, 4, 23, 35, 96, 60, 0, 1, 3, 0, 0, 0, 1, 0, 0, 0, 0, 10000, 2, 1, 0, 129, 6));
            m_monsters.Add(new MM2Monster(239, "Dread Knight", 300, 28, 50, 4, 70, 64, 0, 63, 0, 1, 3, 0, 1, 1, 1, 0, 0, 0, 0, 80000, 3, 2, 0, 44, 16));
            m_monsters.Add(new MM2Monster(240, "Baron Wilfrey", 300, 50, 50, 5, 60, 64, 0, 63, 0, 1, 3, 0, 0, 1, 0, 0, 0, 0, 0, 90000, 3, 2, 1, 31, 16));
            m_monsters.Add(new MM2Monster(241, "Mist Warrior", 350, 30, 50, 6, 60, 0, 150, 63, 3, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 160000, 2, 2, 1, 28, 16));
            m_monsters.Add(new MM2Monster(242, "Queen Beetle", 350, 50, 30, 4, 80, 8, 103, 55, 0, 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 230000, 3, 0, 1, 2, 9));
            m_monsters.Add(new MM2Monster(243, "Serpent King", 400, 60, 31, 2, 200, 3, 136, 55, 4, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 240000, 3, 2, 1, 152, 8));
            m_monsters.Add(new MM2Monster(244, "Dragon Lord", 340, 40, 25, 6, 50, 11, 132, 63, 4, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0, 260000, 3, 3, 1, 23, 8));
            m_monsters.Add(new MM2Monster(245, "Mandagual", 100, 20, 21, 4, 25, 67, 0, 63, 0, 1, 3, 0, 0, 1, 1, 0, 0, 0, 0, 10000, 2, 2, 1, 29, 1));
            m_monsters.Add(new MM2Monster(246, "Lucky Dog", 70, 20, 22, 2, 32, 63, 110, 63, 3, 7, 2, 0, 1, 0, 0, 1, 1, 0, 0, 5000, 2, 1, 1, 146, 8));
            m_monsters.Add(new MM2Monster(247, "The Horvath", 400, 50, 28, 4, 90, 43, 106, 15, 2, 1, 3, 0, 0, 0, 1, 0, 0, 1, 1, 230000, 3, 2, 1, 10, 6));
            m_monsters.Add(new MM2Monster(248, "Orb Guardian", 300, 32, 50, 6, 100, 61, 0, 63, 4, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 80000, 0, 0, 0, 15, 1));
            m_monsters.Add(new MM2Monster(249, "Dawn", 300, 25, 40, 4, 70, 126, 103, 15, 2, 1, 3, 0, 0, 1, 0, 0, 0, 0, 0, 200000, 3, 2, 1, 58, 5));
            m_monsters.Add(new MM2Monster(250, "Mega Dragon", 64000, 250, 250, 16, 250, 76, 134, 255, 7, 1, 3, 0, 1, 1, 1, 0, 0, 0, 0, 32000000, 3, 3, 1, 154, 16));
            m_monsters.Add(new MM2Monster(251, "Shalwend", 1000, 70, 60, 16, 80, 12, 152, 62, 5, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 5000000, 3, 3, 1, 52, 8));
            m_monsters.Add(new MM2Monster(252, "Pyrannaste", 1500, 60, 50, 16, 80, 12, 131, 55, 4, 1, 3, 0, 1, 0, 0, 0, 0, 0, 0, 6000000, 3, 3, 1, 9, 16));
            m_monsters.Add(new MM2Monster(253, "Acwalandar", 2000, 80, 70, 16, 100, 12, 133, 63, 6, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 8000000, 3, 3, 1, 142, 16));
            m_monsters.Add(new MM2Monster(254, "Gralkor", 1700, 70, 60, 10, 80, 44, 136, 53, 4, 1, 3, 0, 1, 0, 1, 0, 0, 0, 0, 7000000, 3, 3, 1, 54, 10));
            m_monsters.Add(new MM2Monster(255, "Sheltem", 500, 60, 40, 8, 60, 105, 0, 63, 1, 1, 3, 0, 1, 1, 1, 0, 0, 0, 0, 500000, 3, 3, 1, 10, 1));
            m_bValid = true;
        }
    }

    [Flags]
    public enum MM2MonsterCombatStatus
    {
        // Only the highest bit of status is displayed onscreen
        Good = 0x00,
        Hurt = 0x01,
        Silenced = 0x02,
        Weak = 0x04,
        Afraid = 0x08,
        Asleep = 0x10,
        Held = 0x20,
        Mindless = 0x40,
        Encased = 0x80
    }

    [Flags]
    public enum MM2Resistances
    {
        None = 0,
        Fire = 0x01,
        Cold = 0x02,
        Electricity = 0x04,
        Acid = 0x08,
        Sleep = 0x10,
        Paralyze = 0x20,
        MaleWeapons = 0x40,
        FemaleWeapons = 0x80,
        AllWeapons = 0x100
    }

    public class MM2Monster : MMMonster
    {
        public int Level;
        public MM2MonsterTouch Touch;
        public MM2MonsterPower SpecialPower;
        public bool SummonFriends;
        public MM2Resistances Resistances;
        public bool DropGems;
        public int DropItem;
        public int DropGold;
        public bool TouchRare;
        public bool BribeGold;
        public bool BribeGems;
        public bool BribeFood;
        public MM2MonsterCombatStatus CombatStatus;

        public override BasicConditionFlags Condition { get { return GetCondition(CombatStatus); } }
        public override double AverageHP { get { return HP; } }

        public static BasicConditionFlags GetCondition(MM2MonsterCombatStatus status)
        {
            BasicConditionFlags cond = BasicConditionFlags.Good;

            if (status.HasFlag(MM2MonsterCombatStatus.Afraid))
                cond |= BasicConditionFlags.Afraid;
            if (status.HasFlag(MM2MonsterCombatStatus.Asleep))
                cond |= BasicConditionFlags.Asleep;
            if (status.HasFlag(MM2MonsterCombatStatus.Encased))
                cond |= BasicConditionFlags.EncasedAir;
            if (status.HasFlag(MM2MonsterCombatStatus.Held))
                cond |= BasicConditionFlags.Held;
            if (status.HasFlag(MM2MonsterCombatStatus.Mindless))
                cond |= BasicConditionFlags.Mindless;
            if (status.HasFlag(MM2MonsterCombatStatus.Hurt))
                cond |= BasicConditionFlags.Hurt;
            if (status.HasFlag(MM2MonsterCombatStatus.Silenced))
                cond |= BasicConditionFlags.Silenced;
            if (status.HasFlag(MM2MonsterCombatStatus.Weak))
                cond |= BasicConditionFlags.Weak;

            return cond;
        }

        public static MM2MonsterCombatStatus GetCondition(BasicConditionFlags status)
        {
            MM2MonsterCombatStatus cond = MM2MonsterCombatStatus.Good;

            if (status.HasFlag(BasicConditionFlags.Afraid))
                cond |= MM2MonsterCombatStatus.Afraid;
            if (status.HasFlag(BasicConditionFlags.Asleep))
                cond |= MM2MonsterCombatStatus.Asleep;
            if (status.HasFlag(BasicConditionFlags.EncasedAir))
                cond |= MM2MonsterCombatStatus.Encased;
            if (status.HasFlag(BasicConditionFlags.Held))
                cond |= MM2MonsterCombatStatus.Held;
            if (status.HasFlag(BasicConditionFlags.Mindless))
                cond |= MM2MonsterCombatStatus.Mindless;
            if (status.HasFlag(BasicConditionFlags.Hurt))
                cond |= MM2MonsterCombatStatus.Hurt;
            if (status.HasFlag(BasicConditionFlags.Silenced))
                cond |= MM2MonsterCombatStatus.Silenced;
            if (status.HasFlag(BasicConditionFlags.Weak))
                cond |= MM2MonsterCombatStatus.Weak;

            return cond;
        }

        public override string HPString(bool bPreEncounter)
        {
            if (bPreEncounter)
                return HP.ToString();   // The pre-encounter monster HP array has stale data
            return CurrentHP.ToString();
        }

        public static string GetMonsterPowerString(MM2MonsterPower power, bool bIgnoreChance = false)
        {
            string strPower = "";

            switch (power & MM2MonsterPower.AllPowers)
            {
                case MM2MonsterPower.SprayPoison: strPower = "Spray Poison"; break;
                case MM2MonsterPower.SprayAcid: strPower = "Spray Acid"; break;
                case MM2MonsterPower.Curse: strPower = "Cast Curse"; break;
                case MM2MonsterPower.BreatheFire: strPower = "Breathe Fire"; break;
                case MM2MonsterPower.BreatheLightning: strPower = "Breathe Lightning"; break;
                case MM2MonsterPower.BreatheCold: strPower = "Breathe Cold"; break;
                case MM2MonsterPower.BreatheEnergy: strPower = "Breathe Energy"; break;
                case MM2MonsterPower.BreatheGas: strPower = "Breathe Gas"; break;
                case MM2MonsterPower.BreatheAcid: strPower = "Breathe Acid"; break;
                case MM2MonsterPower.Explode: strPower = "Explode"; break;
                case MM2MonsterPower.PetrificationGaze: strPower = "Petrification Gaze"; break;
                case MM2MonsterPower.DrainMagic: strPower = "Drain Magic"; break;
                case MM2MonsterPower.DrainSpellLevel: strPower = "Drain Spell Level"; break;
                case MM2MonsterPower.VaporizeValuables: strPower = "Vaporize Valuables"; break;
                case MM2MonsterPower.JuggleParty: strPower = "Juggle Party"; break;
                case MM2MonsterPower.EnergyBlast: strPower = "Energy Blast"; break;
                case MM2MonsterPower.Sleep: strPower = "Sleep"; break;
                case MM2MonsterPower.LightningBolts: strPower = "Cast Lightning Bolts"; break;
                case MM2MonsterPower.Fireball: strPower = "Cast Fireball"; break;
                case MM2MonsterPower.FingersOfDeath: strPower = "Cast Fingers of Death"; break;
                case MM2MonsterPower.Disintegration: strPower = "Cast Disintegration"; break;
                case MM2MonsterPower.SuperShock: strPower = "Cast Super Shock"; break;
                case MM2MonsterPower.DancingSword: strPower = "Cast Dancing Sword"; break;
                case MM2MonsterPower.Incinerate: strPower = "Cast Incinerate"; break;
                case MM2MonsterPower.InvokePower: strPower = "Invoke Power"; break;
                case MM2MonsterPower.Implosion: strPower = "Cast Implosion"; break;
                case MM2MonsterPower.Inferno: strPower = "Cast Inferno"; break;
                case MM2MonsterPower.Pain: strPower = "Cast Pain"; break;
                case MM2MonsterPower.Silence: strPower = "Cast Silence"; break;
                case MM2MonsterPower.Frenzy: strPower = "Frenzy"; break;
                case MM2MonsterPower.Paralyze: strPower = "Paralyze"; break;
                case MM2MonsterPower.Swarm: strPower = "Swarm"; break;
                default: return String.Format("Unknown 0x{0:X2}", (byte)power);
            }

            if (bIgnoreChance)
                return strPower;

            int chance = (int) (power & MM2MonsterPower.PowerChance);
            chance >>= 5;

            if (chance < 1)
                return "None";

            return String.Format("{0} ({1}%)", strPower, (int)Math.Round(chance / 7.0 * 100));
        }

        public static string GetMonsterTouchString(MM2MonsterTouch touch)
        {
            string strTouch = "";
            switch (touch & MM2MonsterTouch.AllTouches)
            {
                case MM2MonsterTouch.None: return "None";
                case MM2MonsterTouch.StealSomeGold: strTouch = "Steal Some Gold"; break;
                case MM2MonsterTouch.StealSomeGems: strTouch = "Steal Some Gems"; break;
                case MM2MonsterTouch.Poison: strTouch = "Poison"; break;
                case MM2MonsterTouch.Disease: strTouch = "Disease"; break;
                case MM2MonsterTouch.Sleep: strTouch = "Sleep"; break;
                case MM2MonsterTouch.Curse: strTouch = "Curse"; break;
                case MM2MonsterTouch.StealAllFood: strTouch = "Steal All Food"; break;
                case MM2MonsterTouch.Paralyze: strTouch = "Paralyze"; break;
                case MM2MonsterTouch.Collapse: strTouch = "Collapse"; break;
                case MM2MonsterTouch.Death: strTouch = "Death"; break;
                case MM2MonsterTouch.Stone: strTouch = "Stone"; break;
                case MM2MonsterTouch.Eradication: strTouch = "Eradication"; break;
                case MM2MonsterTouch.StealItem: strTouch = "Steal Item"; break;
                case MM2MonsterTouch.StealBackpack: strTouch = "StealBackpack"; break;
                case MM2MonsterTouch.StealSomeFood: strTouch = "Steal Some Food"; break;
                case MM2MonsterTouch.StealAllGold: strTouch = "Steal All Gold"; break;
                case MM2MonsterTouch.StealAllGems: strTouch = "Steal All Gems"; break;
                case MM2MonsterTouch.StealGemsAndGold: strTouch = "Steal Gems+Gold"; break;
                case MM2MonsterTouch.Aging1: strTouch = "Aging"; break;
                case MM2MonsterTouch.Aging2: strTouch = "Advanced Aging"; break;
                case MM2MonsterTouch.DrainStatistic1: strTouch = "Drain Stat 1"; break;
                case MM2MonsterTouch.DrainStatistic2: strTouch = "Drain Stat 2"; break;
                case MM2MonsterTouch.DrainStatistic3: strTouch = "Drain Stat 3"; break;
                case MM2MonsterTouch.DrainLevel1: strTouch = "Drain Level 1"; break;
                case MM2MonsterTouch.DrainLevel2: strTouch = "Drain Level 2"; break;
                case MM2MonsterTouch.DrainExperience: strTouch = "Drain Experience"; break;
                case MM2MonsterTouch.ScrambleItems: strTouch = "Scramble Items"; break;
                case MM2MonsterTouch.DrainSpellPoints: strTouch = "Drain Spell Points"; break;
                case MM2MonsterTouch.Assassination: strTouch = "Assassination"; break;
                case MM2MonsterTouch.RandomEffect: strTouch = "Random Effect"; break;
                default: return String.Format("Unknown 0x{0:X2}", (byte)touch);
            }

            if (touch.HasFlag(MM2MonsterTouch.RareEffect))
                strTouch += " (rare)";

            return strTouch;
        }

        public static string GetHeaderString()
        {
            //return "Idx Name             GroupSiz Friendly HitPoint ArmorCla Damage   NumAttac Speed    ExperienceGained  Treasure MagicRes Resistan OnTouch  SpecialP ChancePo Bravery  ImageIdx";

            return String.Format("{0,-3} {1,-14} {2,-5} {3,-3} {4,-3} {5,-2} {6,-3} {7,-25} {8,-28} {9,-8} {10,-2} {11,-1} {12,-5} {13,-3} {14,-5} {15,-3} {16,-2}",
                "Id",
                "Name",
                "HP",
                "AC",
                "Spd",
                "At",
                "Dmg",
                "Touch",
                "Special Power",
                "Resist",
                "Gr",
                "B",
                "URMAF",
                "Bri",
                "EXP",
                "IgG",
                "Lv"
                );
        }

        public string GetRowString()
        {
            //return "Idx Name             GroupSiz Friendly HitPoint ArmorCla Damage   NumAttac Speed    ExperienceGained  Treasure MagicRes Resistan OnTouch  SpecialP ChancePo Bravery  ImageIdx";

            return String.Format("{0,-3} {1,-14} {2,-5} {3,-3} {4,-3} {5,-2} {6,-3} {7,-25} {8,-28} {9,-8} {10,-2} {11,-1} {12,-5} {13,-3} {14,-5} {15,-3} {16,-2}",
                Index,
                Name,
                HP,
                AC,
                Speed,
                NumAttacks,
                Damage,
                GetMonsterTouchString(Touch),
                GetMonsterPowerString(SpecialPower),
                ResistancesStringAbbr,
                GroupSize,
                Bravery,
                URMAF,
                Bribery,
                Global.GetHRString(Experience),
                TreasureAbbr,
                Level
                );
        }

        public string GetInputString()
        {
            return String.Format("m_monsters.Add(new MM2Monster({0},\"{1}\",{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26}));",
                Index,
                Name,
                HP,
                AC,
                Speed,
                NumAttacks,
                Damage,
                (byte)Touch,
                (byte)SpecialPower,
                (byte)Resistances,
                MagicResist,
                GroupSize,
                Bravery,
                Undead ? 1 : 0,
                Regenerate ? 1 : 0,
                Missile ? 1 : 0,
                Advance ? 1 : 0,
                SummonFriends ? 1 : 0,
                BribeFood ? 1 : 0,
                BribeGold ? 1 : 0,
                BribeGems ? 1 : 0,
                Experience,
                DropItem,
                DropGold,
                DropGems ? 1 : 0,
                ImageIndex,
                Level
                );
        }

        public string GetLongDescription()
        {
            return String.Format("{0}, {1} HP, {2} Dam, {3} AC",
                Name,
                HP,
                Damage,
                AC);
        }

        public string URMAF
        {
            get
            {
                return String.Format("{0}{1}{2}{3}{4}",
                    Undead ? "U" : " ",
                    Regenerate ? "R" : " ",
                    Missile ? "M" : " ",
                    Advance ? "A" : " ",
                    SummonFriends ? "F" : " "
                    );
            }
        }

        public string TreasureAbbr
        {
            get
            {
                return String.Format("{0}{1}{2}", DropItem, DropGold, DropGems ? "G" : " ");
            }
        }

        public string Bribery
        {
            get
            {
                return String.Format("{0}{1}{2}",
                    BribeFood ? "F" : " ",
                    BribeGems ? "G" : " ",
                    BribeGold ? "g" : " "
                    );
            }
        }

        public string BriberyFull
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (BribeFood)
                    sb.Append("Food, ");
                if (BribeGold)
                    sb.Append("Gold, ");
                if (BribeGems)
                    sb.Append("Gems, ");
                if (Global.Trim(sb).Length == 0)
                    return "None";
                return sb.ToString();
            }
        }

        public string AttributesString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Undead)
                    sb.Append("Undead, ");
                if (Advance)
                    sb.Append("Regeneration, ");
                if (Regenerate)
                    sb.Append("Missile, ");
                if (Regenerate)
                    sb.Append("Advance, ");
                if (Regenerate)
                    sb.Append("Summon Friends, ");
                if (Global.Trim(sb).Length == 0)
                    sb.Append("None");
                return sb.ToString();
            }
        }

        public override string GetMultiLineDescription(bool bActive)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Name);
            sb.AppendFormat("Level: {0}\r\n", Level);
            if (bActive)
            {
                sb.AppendFormat("HP: {0}/{1}\r\n", CurrentHP, HP);
                sb.AppendFormat("Status: {0}\r\n", GetMonsterConditionAll(CombatStatus));
            }
            else
                sb.AppendFormat("MaxHP: {0}\r\n", HP);
            sb.AppendFormat("AC: {0}\r\n", AC);
            sb.AppendFormat("Magic Resistance: {0}%\r\n", (int)Math.Round(MagicResist / 8.0 * 100));
            sb.AppendFormat("Resists: {0}\r\n", ResistancesString);
            sb.AppendFormat("Max Number in Group: {0}\r\n", GroupSize);
            sb.AppendFormat("Damage: {0}\r\n", DamageString);
            sb.AppendFormat("Speed: {0}\r\n", Speed);
            sb.AppendFormat("Experience: {0}\r\n", Global.GetHRString(Experience));
            sb.AppendFormat("Bravery: {0}\r\n", Bravery * Level);
            sb.AppendFormat("Bribe With: {0}\r\n", BriberyFull);
            sb.AppendFormat("On Touch: {0}\r\n", GetMonsterTouchString(Touch));
            sb.AppendFormat("Special Power: {0}\r\n", GetMonsterPowerString(SpecialPower));
            sb.AppendFormat("Attributes: {0}\r\n", AttributesString);
            sb.AppendFormat("Treasure: {0}\r\n", TreasureString);
            return sb.ToString();
        }

        public static MM2Monster Create(int index, byte[] rawMonsterData)
        {
            if (index < 0 || index > MM2.Monsters.Count)
                return null;

            MM2Monster monster;
            if (rawMonsterData == null || rawMonsterData.Length < (index + 1) * 26)
                monster = MM2.Monsters[index].Clone() as MM2Monster;
            else
                monster = new MM2Monster(rawMonsterData, index * 26, index, MM2MonsterCombatStatus.Good, false);
            monster.CurrentHP = monster.HP;
            monster.CombatStatus = MM2MonsterCombatStatus.Good;

            return monster;
        }

        public static MM2Monster Create(int index, int currentHP, MM2MonsterCombatStatus status, byte[] rawMonsterData)
        {
            if (index < 0 || index > MM2.Monsters.Count)
                return null;

            MM2Monster monster;
            if (rawMonsterData == null || rawMonsterData.Length < (index + 1) * 26)
                monster = MM2.Monsters[index].Clone() as MM2Monster;
            else
                monster = new MM2Monster(rawMonsterData, index * 26, index, status, false);
            monster.CurrentHP = currentHP < 0 ? monster.HP : currentHP;
            monster.CombatStatus = status;

            return monster;
        }

        public MM2Monster(int index,string name, int hp, int ac, int speed, int numAttacks, int damage, byte touch, byte special, byte resistances, int magicResist, int groupSize,
            int bravery, int undead, int regen, int missile, int advance, int summonFriends, int bribeFood, int bribeGold, int bribeGems, long experience, int dropItem,
            int dropGold, int dropGems, int imageIndex, int level)
        {
            //return String.Format("m_monsters.Add(new MM2Monster({0},\"{1}\",{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21},{22},{23},{24},{25},{26}));",
            //    Index,
            //    ProperName,
            //    HP,
            //    AC,
            //    Speed,
            //    NumAttacks,
            //    Damage,
            //    (byte)Touch,
            //    (byte)SpecialPower,
            //    (byte)Resistances,
            //    MagicResist,
            //    GroupSize,
            //    Bravery,
            //    Undead ? 1 : 0,
            //    Regenerate ? 1 : 0,
            //    Missile ? 1 : 0,
            //    Advance ? 1 : 0,
            //    SummonFriends ? 1 : 0,
            //    BribeFood ? 1 : 0,
            //    BribeGold ? 1 : 0,
            //    BribeGems ? 1 : 0,
            //    Experience,
            //    DropItem,
            //    DropGold,
            //    DropGems ? 1 : 0,
            //    ImageIndex
            //    );

            Index = index;
            Name = name;
            GroupSize = groupSize;
            MagicResist = magicResist;
            HP = hp;
            AC = ac;
            Damage = damage;
            Touch = (MM2MonsterTouch)touch;
            NumAttacks = numAttacks;
            Speed = speed;
            Experience = experience;
            ImageIndex = imageIndex;
            SpecialPower = (MM2MonsterPower) special;
            Bravery = bravery;
            Undead = undead > 0;
            Regenerate = regen > 0;
            Missile = missile > 0;
            Advance = advance > 0;
            SummonFriends = summonFriends > 0;
            BribeFood = bribeFood > 0;
            BribeGold = bribeGold > 0;
            BribeGems = bribeGems > 0;
            DropItem = dropItem;
            DropGold = dropGold;
            DropGems = dropGems > 0;
            Resistances = (MM2Resistances) resistances;
            Level = level;
        }

        public override string DamageString
        {
            get
            {
                if (NumAttacks == 1)
                    return String.Format("1d{0}", GetDamageString(Damage));
                return String.Format("{0}x 1d{1}", NumAttacks, GetDamageString(Damage));
            }
        }

        public static string GetMonsterConditionSingle(MM2MonsterCombatStatus status)
        {
            // Assumes only one bit is set
            if (status.HasFlag(MM2MonsterCombatStatus.Afraid))
                return "Afraid";
            if (status.HasFlag(MM2MonsterCombatStatus.Asleep))
                return "Asleep";
            if (status.HasFlag(MM2MonsterCombatStatus.Encased))
                return "Encased";
            if (status.HasFlag(MM2MonsterCombatStatus.Held))
                return "Held";
            if (status.HasFlag(MM2MonsterCombatStatus.Mindless))
                return "Mindless";
            if (status.HasFlag(MM2MonsterCombatStatus.Weak))
                return "Weak";
            if (status.HasFlag(MM2MonsterCombatStatus.Silenced))
                return "Silenced";
            if (status.HasFlag(MM2MonsterCombatStatus.Hurt))
                return "Hurt";
            return "Good";
        }

        public static string GetMonsterConditionAll(MM2MonsterCombatStatus status)
        {
            StringBuilder sb = new StringBuilder();

            if (status.HasFlag(MM2MonsterCombatStatus.Afraid))
                sb.Append("Afraid, ");
            if (status.HasFlag(MM2MonsterCombatStatus.Asleep))
                sb.Append("Asleep, ");
            if (status.HasFlag(MM2MonsterCombatStatus.Encased))
                sb.Append("Encased, ");
            if (status.HasFlag(MM2MonsterCombatStatus.Held))
                sb.Append("Held, ");
            if (status.HasFlag(MM2MonsterCombatStatus.Mindless))
                sb.Append("Mindless, ");
            if (status.HasFlag(MM2MonsterCombatStatus.Hurt))
                sb.Append("Hurt, ");
            if (status.HasFlag(MM2MonsterCombatStatus.Silenced))
                sb.Append("Silenced, ");
            if (status.HasFlag(MM2MonsterCombatStatus.Weak))
                sb.Append("Weak, ");

            if (Global.Trim(sb).Length == 0)
                return "Good";
            return sb.ToString();
        }

        public static string GetMonsterResistanceSingle(MM2Resistances resist)
        {
            // Assumes only one bit is set
            if (resist.HasFlag(MM2Resistances.Fire))
                return "Fire";
            if (resist.HasFlag(MM2Resistances.Cold))
                return "Cold";
            if (resist.HasFlag(MM2Resistances.Acid))
                return "Acid";
            if (resist.HasFlag(MM2Resistances.AllWeapons))
                return "Weapons";
            if (resist.HasFlag(MM2Resistances.MaleWeapons))
                return "Male Weapons";
            if (resist.HasFlag(MM2Resistances.Paralyze))
                return "Paralyze";
            if (resist.HasFlag(MM2Resistances.Sleep))
                return "Sleep";
            if (resist.HasFlag(MM2Resistances.FemaleWeapons))
                return "Female Weapons";
            if (resist.HasFlag(MM2Resistances.Electricity))
                return "Electricity";
            return "Good";
        }

        public string ResistancesString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (MagicResist > 0)
                    sb.AppendFormat("Magic ({0}), ", MagicResist);
                if (Resistances.HasFlag(MM2Resistances.Acid))
                    sb.Append("Acid, ");
                if (Resistances.HasFlag(MM2Resistances.Cold))
                    sb.Append("Cold, ");
                if (Resistances.HasFlag(MM2Resistances.Fire))
                    sb.Append("Fire, ");
                if (Resistances.HasFlag(MM2Resistances.Electricity))
                    sb.Append("Electric, ");
                if (Resistances.HasFlag(MM2Resistances.Paralyze))
                    sb.Append("Paralyze, ");
                if (Resistances.HasFlag(MM2Resistances.Sleep))
                    sb.Append("Sleep, ");
                if (Resistances.HasFlag(MM2Resistances.AllWeapons))
                    sb.Append("All Weapons, ");
                else
                {
                    if (Resistances.HasFlag(MM2Resistances.FemaleWeapons))
                        sb.Append("Female Weapons, ");
                    if (Resistances.HasFlag(MM2Resistances.MaleWeapons))
                        sb.Append("Male Weapons, ");
                }
                if (Global.Trim(sb).Length == 0)
                    return "Nothing";

                return sb.ToString();
            }
        }

        public override GenericResistanceFlags GenericResistances { get { return GetResistances(Resistances); } }

        public static GenericResistanceFlags GetResistances(MM2Resistances resistances)
        {
            GenericResistanceFlags result = GenericResistanceFlags.None;

            if (resistances.HasFlag(MM2Resistances.Acid))
                result |= GenericResistanceFlags.Acid;
            if (resistances.HasFlag(MM2Resistances.Cold))
                result |= GenericResistanceFlags.Cold;
            if (resistances.HasFlag(MM2Resistances.FemaleWeapons))
                result |= GenericResistanceFlags.Female;
            if (resistances.HasFlag(MM2Resistances.Fire))
                result |= GenericResistanceFlags.Fire;
            if (resistances.HasFlag(MM2Resistances.Electricity))
                result |= GenericResistanceFlags.Electricity;
            if (resistances.HasFlag(MM2Resistances.MaleWeapons))
                result |= GenericResistanceFlags.Male;
            if (resistances.HasFlag(MM2Resistances.Paralyze))
                result |= GenericResistanceFlags.Paralyze;
            if (resistances.HasFlag(MM2Resistances.Sleep))
                result |= GenericResistanceFlags.Sleep;
            if (resistances.HasFlag(MM2Resistances.AllWeapons))
                result |= GenericResistanceFlags.Weapons;

            return result;
        }

        public static MM2Resistances GetResistances(GenericResistanceFlags flags)
        {
            MM2Resistances res = MM2Resistances.None;

            if (flags.HasFlag(GenericResistanceFlags.Acid))
                res |= (MM2Resistances.Acid);
            if (flags.HasFlag(GenericResistanceFlags.Cold))
                res |= MM2Resistances.Cold;
            if (flags.HasFlag(GenericResistanceFlags.Weapons))
                res |= MM2Resistances.AllWeapons;
            if (flags.HasFlag(GenericResistanceFlags.Electricity))
                res |= MM2Resistances.Electricity;
            if (flags.HasFlag(GenericResistanceFlags.Male))
                res |= MM2Resistances.MaleWeapons;
            if (flags.HasFlag(GenericResistanceFlags.Paralyze))
                res |= MM2Resistances.Paralyze;
            if (flags.HasFlag(GenericResistanceFlags.Sleep))
                res |= MM2Resistances.Sleep;
            if (flags.HasFlag(GenericResistanceFlags.Female))
                res |= MM2Resistances.FemaleWeapons;
            if (flags.HasFlag(GenericResistanceFlags.Fire))
                res |= MM2Resistances.Fire;

            return res;
        }

        public override string ResistancesStringShort
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("M{0} ", MagicResist);
                sb.Append(Resistances.HasFlag(MM2Resistances.Acid) ? "A" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.Cold) ? "C" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.Fire) ? "F" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.Electricity) ? "E" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.Paralyze) ? "P" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.Sleep) ? "S" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.AllWeapons) ? "W" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.FemaleWeapons) ? "G" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.MaleWeapons) ? "B" : " ");
                return Global.Trim(sb).ToString();
            }
        }

        public string ResistancesStringAbbr
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}", MagicResist);
                sb.Append(Resistances.HasFlag(MM2Resistances.Acid) ? "A" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.Cold) ? "C" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.Fire) ? "F" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.Electricity) ? "E" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.Paralyze) ? "P" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.Sleep) ? "S" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.AllWeapons) ? "W" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.FemaleWeapons) ? "G" : " ");
                sb.Append(Resistances.HasFlag(MM2Resistances.MaleWeapons) ? "B" : " ");
                return sb.ToString();
            }
        }

        public int PowerChance
        {
            get { return ((int) (SpecialPower & MM2MonsterPower.PowerChance)) >> 5; }
        }

        public override string AllPowersString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (PowerChance > 0)
                    sb.AppendFormat("{0}, ", GetMonsterPowerString(SpecialPower));
                if (Touch != MM2MonsterTouch.None)
                    sb.AppendFormat("Touch: {0}, ", GetMonsterTouchString(Touch));
                if (Undead)
                    sb.Append("Undead, ");
                if (Regenerate)
                    sb.Append("Regen, ");
                if (Missile)
                    sb.Append("Missile, ");
                if (Advance)
                    sb.Append("Advance, ");
                if (SummonFriends)
                    sb.Append("Summon Friends, ");
                return Global.Trim(sb).ToString();
            }
        }

        public string TreasureString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (DropItem > 0)
                    sb.AppendFormat("Level {0} Item, ", DropItem);
                switch (DropGold)
                {
                    case 1:
                        sb.AppendFormat("10-60 Gold, ", DropGold);
                        break;
                    case 2:
                        sb.AppendFormat("4k-8k Gold, ", DropGold);
                        break;
                    case 3:
                        sb.AppendFormat("30k-70k Gold, ", DropGold);
                        break;
                    default:
                        break;
                }
                if (DropGems)
                    sb.Append("Gems, ");
                return Global.Trim(sb).ToString();
            }
        }

        public override int TreasureStrength
        {
            get
            {
                return ((DropItem * Level) << 7) | ((DropGold * Level) << 1) | (DropGems ? 1 : 0);
            }
        }

        public override string TreasureStringShort
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (DropItem > 0)
                    sb.AppendFormat("I{0} ", DropItem);
                if (DropGold > 0)
                    sb.AppendFormat("G{0} ", DropGold);
                if (DropGems)
                    sb.Append("Gems");
                return sb.ToString();
            }
        }

        public static string GetDamageString(int damage)
        {
            return String.Format("{0}", damage);
        }

        public MM2Monster()
        {
            Name = "<none>";
            Index = -1;
        }

        public override Monster Clone()
        {
            return new MM2Monster(Index, Name, HP, AC, Speed, NumAttacks, Damage, (byte)Touch, (byte)SpecialPower, (byte)Resistances, MagicResist, GroupSize,
                Bravery, Undead ? 1 : 0, Regenerate ? 1 : 0, Missile ? 1 : 0, Advance ? 1 : 0, SummonFriends ? 1 : 0, BribeFood ? 1 : 0, BribeGold ? 1 : 0,
                BribeGems ? 1 : 0, Experience, DropItem, DropGold, DropGems ? 1 : 0, ImageIndex, Level);
        }

        private int HPFromByte(byte bHP)
        {
            int iBase = (bHP & 0x3f) + 1;
            switch ((bHP & 0xc0) >> 6)
            {
                case 0: return iBase;
                case 1: return 10 * iBase;
                case 2: return 100 * iBase;
                case 3: return 1000 * iBase;
                default: return 0;
            }
        }

        public static byte HPToByte(int iHP)
        {
            // Many-to-one

            if (iHP <= 64)
                return (byte)(iHP - 1);
            if (iHP <= 640)
                return (byte)((iHP / 10 - 1) | 0x40);
            if (iHP <= 6400)
                return (byte)((iHP / 100 - 1) | 0x80);
            if (iHP <= 64000)
                return (byte)((iHP / 1000 - 1) | 0xc0);
            return (byte)0xff;
        }

        private int SixBitValue(int iValue)
        {
            int iBase = (iValue & 0x3f) + 1;
            if (iValue < 32)
                return iBase;
            return 10 * (iBase - 32);
        }

        public static int ByteToSixBit(int iValue)
        {
            // Many-to-one
            if (iValue == 0)
                return 0;
            if (iValue <= 32)
                return (byte)(iValue-1);
            return (byte)((iValue / 10 - 1) | 0x20);
        }

        private int ExpFromByte(byte bExp)
        {
            int iBase = (bExp & 0x1f) + 1;
            switch ((bExp & 0xe0) >> 5)
            {
                case 0: return iBase;
                case 1: return 10 * iBase;
                case 2:
                case 3: return 100 * iBase;
                case 4: return 1000 * iBase;
                case 5: return 10000 * iBase;
                case 6: return 100000 * iBase;
                case 7: return 1000000 * iBase;
                default: return 0;
            }
        }

        public static byte ExpToByte(long iExp)
        {
            // Many-to-one
            if (iExp <= 32)
                return (byte)(iExp - 1);
            if (iExp <= 320)
                return (byte)((iExp / 10 - 1) | 0x20);
            if (iExp <= 3200)
                return (byte)((iExp / 100 - 1) | 0x60);
            if (iExp <= 32000)
                return (byte)((iExp / 1000 - 1) | 0x80);
            if (iExp <= 320000)
                return (byte)((iExp / 10000 - 1) | 0xA0);
            if (iExp <= 3200000)
                return (byte)((iExp / 100000 - 1) | 0xC0);
            if (iExp <= 32000000)
                return (byte)((iExp / 1000000 - 1) | 0xE0);
            return (byte)0xff;
        }

        private void SetFromBytes(byte[] bytes, int iOffset, int iMonsterIndex)
        {
            //0-13	Name (padded with spaces)
            //14	HP
            //15	XP
            //16	Bits 0-1: item, Bit 2: gems, Bits 3-4: gold, Bit 5: bribe food, bit 6: bribe money, bit 7: bribe gems
            //17	Bits 5-7: chance of special, Bits 0-4: Special Ability
            //18	Bit 7: Undead, Bit 6: Missile, Bits 0-4: Touch, Bit 5: rare touch
            //19	Bits 5-6: bravery, Bits 0-4: # in group-1, Bit 7: add friends
            //20	lower 4 bits: number of attacks-1, Upper 4 bits: Monster Level-1
            //21	icon
            //22	lower 6 bits: AC, bit 6: advance, bit 7: regenerate?
            //23	lower 6 bits: Damage, bit 6: Fire resist, bit 7: Electric resist
            //24	lower 6 bits: speed, Bit 6: Acid resist, Bit 7: Cold resist
            //25	Resistance:  upper 3 bits: magic (0-7), Bit 0: Sleep, Bit 1: Paralyze, Bit 2: Weapons, Bit 3: Male, Bit 4: Female

            //Unknown:  Upper 4 bits of 20, all of 21, Bits 2-4 of 25
            //Unknown:  Weapon Immunity (probably 2 bits), icon

            Index = iMonsterIndex;

            // The characters in the monster name have bit 7 set for some odd reason
            for (int i = 0; i < 14; i++)
                bytes[iOffset+i] &= 0x7f;

            Name = Encoding.ASCII.GetString(bytes, iOffset, 14).Trim();
            HP = HPFromByte(bytes[iOffset+14]);
            Experience = ExpFromByte(bytes[iOffset+15]);
            DropItem = bytes[iOffset+16] & (int) MM2TreasureByte.Item;
            DropGems = (bytes[iOffset+16] & (int) MM2TreasureByte.Gems) > 0;
            DropGold = (bytes[iOffset+16] & (int) MM2TreasureByte.Gold) >> 3;
            BribeFood = (bytes[iOffset+16] & (int)MM2TreasureByte.BribeFood) > 0;
            BribeGold = (bytes[iOffset+16] & (int)MM2TreasureByte.BribeMoney) > 0;
            BribeGems = (bytes[iOffset+16] & (int)MM2TreasureByte.BribeGems) > 0;
            SpecialPower = (MM2MonsterPower)bytes[iOffset+17];
            Touch = (MM2MonsterTouch)bytes[iOffset+18];
            Missile = Touch.HasFlag(MM2MonsterTouch.Missile);
            Undead = Touch.HasFlag(MM2MonsterTouch.Undead);
            SummonFriends = (bytes[iOffset+19] & (int) MM2BraveryFlags.AddFriends) > 0;
            Bravery = (bytes[iOffset+19] & (int)MM2BraveryFlags.BraveryValue) >> 5;
            GroupSize = (bytes[iOffset+19] & (int)MM2BraveryFlags.NumInGroup) + 1;
            NumAttacks = (bytes[iOffset+20] & (int) MM2AttackByte.NumAttacks) + 1;
            Level = ((bytes[iOffset + 20] & (int)MM2AttackByte.Level) >> 4) + 1;
            ImageIndex = bytes[iOffset + 21];
            Advance = (bytes[iOffset + 22] & (int)MM2ACByte.Advance) > 0;
            Regenerate = (bytes[iOffset + 22] & (int)MM2ACByte.Regenerate) > 0;
            AC = SixBitValue(bytes[iOffset+22] & (int)MM2ACByte.ArmorClass);
            Damage = SixBitValue(bytes[iOffset+23] & (int)MM2DamageByte.DamageValue);
            Speed = SixBitValue(bytes[iOffset+24] & (int)MM2SpeedByte.SpeedValue);
            Resistances = MM2Resistances.None;
            if (((MM2DamageByte)bytes[iOffset + 23]).HasFlag(MM2DamageByte.ElecResist))
                Resistances|=  MM2Resistances.Electricity;
            if (((MM2DamageByte)bytes[iOffset + 23]).HasFlag(MM2DamageByte.FireResist))
                Resistances|= MM2Resistances.Fire;
            if (((MM2SpeedByte)bytes[iOffset + 24]).HasFlag(MM2SpeedByte.AcidResist))
                Resistances|= MM2Resistances.Acid;
            if (((MM2SpeedByte)bytes[iOffset + 24]).HasFlag(MM2SpeedByte.ColdResist))
                Resistances|= MM2Resistances.Cold;
            MagicResist = (bytes[iOffset + 25] & (int)MM2MagicResistByte.MagicResist) >> 5;
            if (((MM2MagicResistByte)bytes[iOffset + 25]).HasFlag(MM2MagicResistByte.SleepResist))
                Resistances|= MM2Resistances.Sleep;
            if (((MM2MagicResistByte)bytes[iOffset + 25]).HasFlag(MM2MagicResistByte.ParalyzeResist))
                Resistances|= MM2Resistances.Paralyze;
            if (((MM2MagicResistByte)bytes[iOffset + 25]).HasFlag(MM2MagicResistByte.WeaponResist))
                Resistances|= MM2Resistances.AllWeapons;
            if (((MM2MagicResistByte)bytes[iOffset + 25]).HasFlag(MM2MagicResistByte.MaleResist))
                Resistances|= MM2Resistances.MaleWeapons;
            if (((MM2MagicResistByte)bytes[iOffset + 25]).HasFlag(MM2MagicResistByte.FemaleResist))
                Resistances|= MM2Resistances.FemaleWeapons;
        }



        public MM2Monster(byte[] bytes, int iOffset, int iMonsterIndex, byte currentHP, MM2MonsterCombatStatus status, bool moved)
        {
            SetFromBytes(bytes, iOffset, iMonsterIndex);
            CombatStatus = status;
            CurrentHP = currentHP;
            HasMoved = moved;
        }

        public MM2Monster(byte[] bytes, int iOffset, int iMonsterIndex, MM2MonsterCombatStatus status, bool moved)
        {
            SetFromBytes(bytes, iOffset, iMonsterIndex);
            CombatStatus = status;
            CurrentHP = HP;
            HasMoved = moved;
        }

        public override string ProperName
        {
            get
            {
                return Name.Replace("Elementl", "Elemental");
            }
        }
    }
}
