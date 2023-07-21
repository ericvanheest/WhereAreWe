using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    [Flags]
    public enum MM1MonsterTouch
    {
        None = 0,
        TakesFood = 1,
        InflictsDisease1 = 2,   // 5% chance
        CausesParalysis1 = 3,
        InducesPoison1 = 4,     // 5% chance
        StealsSomeGems = 5,
        ReducesEndurance = 6,
        InducesSleep = 7,
        CausesParalysis2 = 8,
        InflictsDisease2 = 9,   // 20% chance
        StealsSomeGold = 10,
        StealsSomething = 11,
        InducesPoison2 = 12,    // 95% chance
        CausesBlindness = 13,
        DrainsLifeforce1 = 14,
        IsTurnedtoStone = 15,
        CausesRapidAging = 16,
        DrainsLifeforce2 = 17,
        IsKilled = 18,
        InducesUnconsciousness = 19,
        DrainsMight = 20,
        DrainsAbilities = 21,
        StealsBackpack = 22,
        StealsGoldandGems = 23,
        IsEradicated = 24,
        DrainsSpellPoints = 25,
        Something = 26,
        AllEffects = 0x1f,
        Disabled = 0x80
    }

    [Flags]
    public enum MM1MonsterCombatStatus
    {
        // Only the highest bit of status is displayed onscreen
        Good = 0x00,
        Afraid = 0x01,
        Blinded = 0x02,
        Silenced = 0x04,
        Mindless = 0x08,
        Asleep = 0x10,
        Held = 0x20,
        Webbed = 0x40,
        Paralyze = 0x80,
        Dead = 0xFF
    }

    [Flags]
    public enum MM1MonsterPower
    {
        None = 0,
        ACurse = 1,
        EnergyBlast = 2,
        Fire = 3,
        Blindness = 4,
        SpraysPoison = 5,
        SpraysAcid = 6,
        Sleep = 7,
        Paralyze = 8,
        Dispel = 9,
        LightningBolt = 10,
        StrangeGas = 11,
        Explode = 12,
        FireBall = 13,
        BreathesFire = 14,
        Gazes = 15,
        AcidArrow = 16,
        CallsupontheElements = 17,
        ColdBeam = 18,
        DancingSword = 19,
        MagicDrain = 20,
        FingerofDeath = 21,
        SunRay = 22,
        Disintegration = 23,
        CommandsEnergy = 24,
        DragonPoison = 25,
        DragonLightning = 26,
        DragonFrost = 27,
        DragonSpikes = 28,
        DragonAcid = 29,
        DragonFire = 30,
        DragonEnergy = 31,
        Swarm = 32,
        Missile = 0x80,
        AllPowers = 0x3f,
    }

    [Flags]
    public enum MM1BraveryFlags
    {
        ValueFlags = 0x3f,
        Regenerate = 0x40,
        Advance = 0x80
    }

    [Flags]
    public enum MM1TreasureFlags
    {
        None = 0x00,
        Gems = 0x01,
        Gold10 = 0x02,
        Gold100 = 0x04,
        Gold1000 = 0x06,
        ItemLevel1 = 0x08,
        ItemLevel2 = 0x10,
        ItemLevel3 = 0x20,
        ItemLevel4 = 0x40,
        ItemLevel5 = 0x80,
        AnyItem = 0xf8,
        AnyGold = 0x06
    }

    [Flags]
    public enum MM1Resistances
    {
        None = 0x00,
        Sleep = 0x01,
        Mental = 0x02,
        Paralyze = 0x04,
        Energy = 0x08,
        Cold = 0x10,
        Lightning = 0x20,
        Fire = 0x40,
        Acid = 0x60,           // Fire/Lightning explicitly grants acid resistance
        Weapons = 0x80,
        All = 0xFF
    }

    public class MM1MonsterList
    {
        private bool m_bValid = false;
        private string m_strError = string.Empty;
        private List<MM1Monster> m_monsters = new List<MM1Monster>();

        public List<MM1Monster> Monsters
        {
            get { return m_monsters; }
        }

        public MM1MonsterList(string strFile)
        {
            m_bValid = false;
            BinaryReader reader = null;
            int iIndex = 1;

            try
            {
                reader = new BinaryReader(File.OpenRead(strFile));
                while (reader.BaseStream.Position <= reader.BaseStream.Length - 32)
                {
                    byte[] bytes = reader.ReadBytes(32);
                    MM1Monster monster = new MM1Monster(bytes, 0, 0, (MM1MonsterCombatStatus)0, false);
                    monster.Index = iIndex;
                    m_monsters.Add(monster);
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
            StringBuilder sb = new StringBuilder();
            foreach (MM1Monster item in m_monsters)
            {
                sb.Append(item.Dump());
            }
            return sb.ToString();
        }

        public MM1Monster GetItem(byte index)
        {
            if (m_monsters.Count > index)
                return m_monsters[index].Clone() as MM1Monster;
            return new MM1Monster();
        }

        public MM1MonsterList()
        {
            // Load the standard MM1 monster list
            m_monsters = new List<MM1Monster>(195);
            m_monsters.Add(new MM1Monster(1, "Flesh Eater", 6, 0, 2, 2, 6, 0, 1, 7, 50, 4, 16, 2, 0, 0, 0, 0, 0, 0, 0, 0));
            m_monsters.Add(new MM1Monster(2, "Battle Rat", 10, 0, 1, 3, 3, 0, 1, 12, 50, 6, 0, 2, 0, 0, 0, 1, 0, 2, 0, 0));
            m_monsters.Add(new MM1Monster(3, "Slither Beast", 6, 0, 2, 4, 8, 0, 1, 10, 125, 35, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0));
            m_monsters.Add(new MM1Monster(4, "Gnome", 6, 20, 3, 5, 6, 0, 1, 12, 125, 8, 6, 0, 132, 0, 0, 50, 1, 2, 0, 0));
            m_monsters.Add(new MM1Monster(5, "Goblin", 12, 0, 1, 4, 6, 0, 1, 10, 50, 15, 4, 0, 131, 0, 0, 0, 0, 2, 0, 0));
            m_monsters.Add(new MM1Monster(6, "Gremlin", 6, 0, 1, 3, 3, 0, 2, 4, 125, 10, 0, 1, 0, 0, 0, 1, 4, 10, 0, 0));
            m_monsters.Add(new MM1Monster(7, "Guardsman", 6, 0, 0, 2, 6, 0, 1, 14, 75, 55, 22, 0, 134, 0, 0, 10, 0, 2, 1, 0));
            m_monsters.Add(new MM1Monster(8, "Kobold", 10, 0, 1, 4, 4, 0, 1, 6, 50, 5, 4, 0, 131, 0, 0, 1, 0, 2, 0, 0));
            m_monsters.Add(new MM1Monster(9, "Mutant Larva", 6, 0, 0, 0, 3, 0, 1, 2, 50, 20, 0, 2, 0, 0, 0, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(10, "Orc", 12, 0, 2, 4, 6, 0, 1, 11, 75, 14, 22, 0, 134, 0, 0, 5, 0, 10, 0, 0));
            m_monsters.Add(new MM1Monster(11, "Poltergeist", 6, 0, 1, 0, 2, 0, 2, 16, 150, 60, 32, 3, 0, 0, 1, 0, 7, 0, 1, 0));
            m_monsters.Add(new MM1Monster(12, "Rabid Jackal", 6, 0, 0, 3, 2, 0, 1, 15, 50, 29, 16, 2, 0, 0, 0, 0, 0, 0, 0, 0));
            m_monsters.Add(new MM1Monster(13, "Skeleton", 12, 0, 1, 3, 6, 0, 1, 9, 50, 63, 32, 0, 0, 0, 1, 0, 7, 2, 1, 0));
            m_monsters.Add(new MM1Monster(14, "Snake", 6, 0, 0, 2, 3, 0, 1, 17, 125, 35, 16, 4, 0, 0, 0, 0, 0, 0, 0, 0));
            m_monsters.Add(new MM1Monster(15, "Sprite", 8, 30, 0, 10, 2, 0, 1, 20, 250, 9, 8, 5, 1, 50, 0, 60, 1, 11, 0, 0));
            m_monsters.Add(new MM1Monster(16, "Vampire Bat", 8, 0, 0, 2, 3, 0, 1, 14, 125, 24, 16, 6, 0, 0, 0, 0, 0, 0, 0, 0));
            m_monsters.Add(new MM1Monster(17, "Minor Demon", 10, 20, 10, 5, 8, 0, 2, 16, 200, 11, 26, 7, 2, 25, 0, 0, 15, 5, 1, 1));
            m_monsters.Add(new MM1Monster(18, "Demon Dog", 10, 0, 8, 3, 10, 0, 2, 14, 100, 32, 16, 0, 0, 0, 0, 3, 0, 0, 0, 0));
            m_monsters.Add(new MM1Monster(19, "Dung Beetle", 6, 0, 8, 6, 8, 0, 1, 8, 100, 1, 16, 2, 0, 0, 0, 1, 1, 8, 0, 0));
            m_monsters.Add(new MM1Monster(20, "Fire Ant", 15, 0, 5, 5, 6, 0, 1, 7, 100, 0, 20, 0, 3, 10, 0, 0, 67, 26, 0, 0));
            m_monsters.Add(new MM1Monster(21, "Ghoul", 6, 0, 8, 4, 5, 0, 3, 13, 200, 58, 16, 8, 0, 0, 1, 0, 7, 5, 0, 0));
            m_monsters.Add(new MM1Monster(22, "Gnoll", 10, 0, 8, 5, 8, 0, 1, 10, 100, 15, 6, 0, 134, 0, 0, 0, 0, 10, 0, 0));
            m_monsters.Add(new MM1Monster(23, "Hag", 2, 0, 3, 1, 4, 0, 2, 8, 100, 12, 20, 10, 4, 20, 0, 0, 0, 1, 0, 0));
            m_monsters.Add(new MM1Monster(24, "Locust Plague", 4, 0, 8, 5, 1, 0, 10, 17, 200, 2, 47, 2, 32, 10, 0, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(25, "Orc Leader", 1, 0, 10, 5, 8, 0, 1, 14, 100, 14, 38, 0, 138, 0, 0, 0, 0, 28, 0, 0));
            m_monsters.Add(new MM1Monster(26, "Rabid Leper", 4, 0, 5, 0, 3, 0, 1, 11, 100, 65, 16, 9, 0, 0, 0, 0, 0, 0, 0, 0));
            m_monsters.Add(new MM1Monster(27, "Rotting Corpse", 6, 0, 8, 2, 4, 0, 2, 3, 100, 65, 16, 9, 0, 0, 1, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(28, "Savage Shrew", 4, 0, 4, 3, 5, 0, 3, 13, 150, 32, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0));
            m_monsters.Add(new MM1Monster(29, "Strangling Vine", 10, 0, 1, 3, 3, 0, 4, 6, 100, 47, 51, 6, 0, 0, 0, 0, 3, 40, 0, 0));
            m_monsters.Add(new MM1Monster(30, "Thief", 8, 0, 6, 3, 8, 0, 1, 16, 100, 49, 4, 11, 134, 0, 0, 0, 0, 20, 0, 0));
            m_monsters.Add(new MM1Monster(31, "Troglodyte", 6, 0, 8, 5, 4, 0, 3, 11, 100, 37, 22, 4, 5, 20, 0, 1, 3, 10, 0, 0));
            m_monsters.Add(new MM1Monster(32, "Zombie", 8, 0, 8, 2, 8, 0, 1, 2, 100, 7, 32, 2, 0, 0, 1, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(33, "Acidic Blob", 6, 0, 8, 1, 8, 0, 2, 8, 300, 20, 54, 12, 6, 20, 0, 0, 127, 0, 0, 1));
            m_monsters.Add(new MM1Monster(34, "Centaur", 12, 0, 20, 4, 4, 0, 4, 12, 200, 18, 20, 0, 7, 20, 0, 60, 3, 27, 0, 0));
            m_monsters.Add(new MM1Monster(35, "Cleric", 4, 0, 15, 5, 8, 0, 1, 12, 300, 53, 20, 0, 8, 50, 0, 10, 0, 11, 0, 0));
            m_monsters.Add(new MM1Monster(36, "Minor Devil", 10, 20, 15, 4, 4, 0, 2, 15, 250, 11, 42, 8, 2, 30, 0, 0, 15, 61, 1, 1));
            m_monsters.Add(new MM1Monster(37, "Fire Beetle", 5, 0, 16, 7, 15, 0, 1, 6, 200, 1, 22, 0, 3, 30, 0, 0, 65, 2, 0, 0));
            m_monsters.Add(new MM1Monster(38, "Gargoyle", 6, 20, 24, 5, 4, 0, 4, 12, 300, 11, 16, 8, 0, 0, 0, 0, 15, 3, 0, 0));
            m_monsters.Add(new MM1Monster(39, "Gargantuan Ant", 6, 0, 18, 8, 12, 0, 1, 9, 200, 0, 32, 0, 0, 0, 0, 0, 3, 24, 1, 0));
            m_monsters.Add(new MM1Monster(40, "Dinolizard", 6, 0, 16, 5, 10, 0, 1, 11, 200, 36, 16, 0, 0, 0, 0, 0, 3, 0, 0, 0));
            m_monsters.Add(new MM1Monster(41, "Giant Spider", 6, 0, 20, 5, 8, 0, 1, 18, 200, 0, 16, 12, 0, 0, 0, 1, 1, 1, 0, 0));
            m_monsters.Add(new MM1Monster(42, "Harpy", 6, 0, 15, 3, 5, 0, 3, 15, 200, 25, 20, 0, 8, 10, 0, 0, 3, 28, 0, 0));
            m_monsters.Add(new MM1Monster(43, "Hippogriff", 6, 0, 18, 5, 8, 0, 3, 14, 200, 39, 16, 10, 0, 0, 0, 5, 0, 12, 0, 0));
            m_monsters.Add(new MM1Monster(44, "Killer Bees", 4, 0, 10, 10, 2, 0, 10, 16, 300, 2, 42, 4, 32, 20, 0, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(45, "Pegasus", 6, 10, 25, 4, 8, 0, 3, 20, 250, 38, 4, 7, 9, 10, 0, 90, 15, 27, 0, 0));
            m_monsters.Add(new MM1Monster(46, "Shadow Beast", 6, 0, 20, 3, 5, 0, 1, 18, 200, 60, 32, 13, 0, 0, 0, 0, 15, 0, 0, 0));
            m_monsters.Add(new MM1Monster(47, "Wild Boar", 6, 0, 15, 3, 12, 0, 1, 14, 200, 32, 16, 0, 0, 0, 0, 5, 0, 0, 0, 0));
            m_monsters.Add(new MM1Monster(48, "Wolverine", 3, 0, 10, 5, 5, 0, 3, 12, 250, 29, 16, 0, 0, 0, 0, 0, 1, 10, 0, 0));
            m_monsters.Add(new MM1Monster(49, "Barbarian", 10, 20, 50, 8, 12, 0, 2, 15, 400, 23, 42, 0, 140, 0, 0, 5, 1, 12, 1, 0));
            m_monsters.Add(new MM1Monster(50, "Caryatid Guard", 6, 25, 20, 5, 10, 0, 1, 15, 400, 64, 54, 14, 9, 10, 0, 2, 255, 1, 0, 0));
            m_monsters.Add(new MM1Monster(51, "Cockatrice", 6, 0, 28, 4, 8, 0, 1, 8, 500, 28, 16, 15, 0, 0, 0, 3, 3, 0, 0, 0));
            m_monsters.Add(new MM1Monster(52, "Cyclops", 8, 0, 32, 6, 15, 0, 2, 10, 400, 22, 40, 0, 148, 0, 0, 1, 1, 28, 1, 0));
            m_monsters.Add(new MM1Monster(53, "Druid", 4, 0, 24, 4, 8, 0, 2, 14, 400, 53, 22, 10, 7, 50, 0, 2, 1, 3, 0, 0));
            m_monsters.Add(new MM1Monster(54, "Rhino Beetle", 4, 0, 25, 7, 20, 0, 1, 7, 500, 3, 35, 0, 6, 30, 0, 0, 3, 24, 0, 0));
            m_monsters.Add(new MM1Monster(55, "Giant Centipede", 4, 0, 15, 5, 4, 0, 8, 9, 500, 1, 32, 8, 0, 0, 0, 0, 3, 2, 0, 0));
            m_monsters.Add(new MM1Monster(56, "Magician", 3, 0, 25, 5, 8, 0, 1, 15, 600, 52, 24, 0, 10, 90, 0, 1, 1, 29, 0, 0));
            m_monsters.Add(new MM1Monster(57, "Militiaman", 6, 0, 30, 9, 10, 0, 2, 9, 300, 50, 22, 0, 138, 0, 0, 10, 1, 2, 1, 0));
            m_monsters.Add(new MM1Monster(58, "Orc Chieftain", 1, 0, 60, 10, 12, 0, 2, 15, 500, 14, 42, 8, 140, 0, 0, 0, 1, 28, 1, 0));
            m_monsters.Add(new MM1Monster(59, "Ogre", 6, 0, 45, 7, 10, 0, 2, 12, 350, 19, 42, 0, 140, 0, 0, 0, 1, 12, 1, 0));
            m_monsters.Add(new MM1Monster(60, "Satyr", 5, 0, 28, 5, 8, 0, 2, 10, 300, 18, 24, 7, 8, 40, 0, 50, 15, 26, 0, 0));
            m_monsters.Add(new MM1Monster(61, "Swarming Wasps", 4, 0, 20, 4, 2, 0, 10, 17, 400, 2, 47, 4, 32, 25, 0, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(62, "Unicorn", 4, 20, 35, 8, 10, 0, 3, 22, 500, 38, 5, 13, 9, 20, 0, 90, 127, 7, 0, 1));
            m_monsters.Add(new MM1Monster(63, "Werewolf", 4, 0, 30, 7, 8, 0, 2, 14, 400, 30, 16, 9, 0, 0, 0, 10, 7, 0, 1, 0));
            m_monsters.Add(new MM1Monster(64, "Wight", 6, 0, 20, 6, 10, 0, 1, 12, 500, 60, 32, 14, 0, 0, 1, 0, 7, 0, 1, 0));
            m_monsters.Add(new MM1Monster(65, "Barbarian Chief", 1, 0, 80, 10, 12, 0, 3, 18, 800, 23, 56, 0, 143, 0, 0, 0, 3, 28, 1, 0));
            m_monsters.Add(new MM1Monster(66, "Basilisk", 4, 0, 30, 5, 15, 0, 1, 14, 1000, 45, 35, 15, 11, 30, 0, 2, 3, 0, 1, 0));
            m_monsters.Add(new MM1Monster(67, "Celestial Stag", 2, 0, 50, 14, 10, 0, 3, 19, 700, 39, 32, 0, 0, 0, 0, 1, 3, 34, 0, 0));
            m_monsters.Add(new MM1Monster(68, "Dust Demon", 3, 0, 30, 9, 12, 0, 3, 15, 600, 34, 26, 7, 7, 20, 0, 0, 15, 1, 1, 0));
            m_monsters.Add(new MM1Monster(69, "Giant Scorpion", 4, 0, 30, 7, 8, 0, 3, 13, 600, 0, 32, 12, 0, 0, 0, 0, 3, 0, 0, 0));
            m_monsters.Add(new MM1Monster(70, "5 Headed Hydra", 1, 0, 80, 7, 8, 0, 5, 12, 1500, 44, 32, 0, 14, 40, 0, 0, 3, 60, 1, 0));
            m_monsters.Add(new MM1Monster(71, "Warrior Cat", 4, 0, 40, 6, 6, 0, 4, 17, 600, 33, 16, 0, 0, 0, 0, 5, 3, 0, 0, 0));
            m_monsters.Add(new MM1Monster(72, "Minotaur", 2, 20, 55, 7, 35, 0, 1, 15, 1500, 40, 48, 16, 0, 0, 0, 0, 143, 60, 1, 1));
            m_monsters.Add(new MM1Monster(73, "Ogre Chief", 1, 0, 64, 9, 15, 0, 2, 15, 750, 19, 32, 0, 148, 0, 0, 0, 131, 28, 1, 0));
            m_monsters.Add(new MM1Monster(74, "Panthro Mist", 6, 0, 40, 7, 8, 0, 4, 18, 600, 33, 16, 0, 0, 0, 0, 5, 3, 0, 0, 0));
            m_monsters.Add(new MM1Monster(75, "Phantom", 6, 0, 35, 7, 8, 0, 2, 10, 600, 63, 16, 17, 0, 0, 0, 0, 3, 0, 0, 0));
            m_monsters.Add(new MM1Monster(76, "Swordsman", 6, 0, 40, 6, 10, 0, 2, 18, 600, 55, 38, 0, 143, 0, 0, 5, 3, 20, 1, 0));
            m_monsters.Add(new MM1Monster(77, "Troll", 6, 0, 40, 6, 9, 0, 3, 12, 600, 17, 32, 0, 0, 0, 0, 0, 131, 12, 1, 1));
            m_monsters.Add(new MM1Monster(78, "Wood Golem", 3, 0, 30, 5, 15, 0, 2, 5, 800, 16, 48, 0, 0, 0, 0, 0, 191, 1, 1, 0));
            m_monsters.Add(new MM1Monster(79, "Wraith", 6, 0, 35, 6, 6, 0, 2, 9, 800, 60, 48, 14, 0, 0, 0, 0, 135, 0, 1, 0));
            m_monsters.Add(new MM1Monster(80, "Yeti", 6, 0, 50, 4, 10, 0, 2, 13, 600, 31, 24, 0, 148, 0, 0, 5, 3, 10, 0, 0));
            m_monsters.Add(new MM1Monster(81, "Deadly Spores", 15, 0, 10, 2, 1, 0, 1, 10, 600, 48, 54, 4, 12, 90, 0, 0, 3, 0, 0, 0));
            m_monsters.Add(new MM1Monster(82, "Enchantress", 2, 0, 30, 6, 6, 0, 2, 15, 1000, 54, 26, 8, 13, 80, 0, 1, 3, 37, 0, 0));
            m_monsters.Add(new MM1Monster(83, "Fire Lizard", 4, 0, 40, 7, 10, 0, 3, 12, 800, 36, 38, 0, 3, 20, 0, 1, 67, 0, 1, 0));
            m_monsters.Add(new MM1Monster(84, "Giant Sloth", 3, 0, 40, 5, 8, 0, 4, 14, 750, 31, 0, 0, 0, 0, 0, 70, 19, 8, 0, 0));
            m_monsters.Add(new MM1Monster(85, "Griffin", 8, 0, 45, 7, 8, 0, 3, 14, 700, 28, 16, 15, 0, 0, 0, 10, 3, 28, 0, 0));
            m_monsters.Add(new MM1Monster(86, "Pyro Hydra", 1, 0, 60, 7, 8, 0, 5, 12, 2000, 44, 36, 0, 14, 50, 0, 0, 195, 96, 1, 0));
            m_monsters.Add(new MM1Monster(87, "Man Eating Mare", 4, 0, 42, 6, 8, 0, 3, 14, 600, 39, 32, 0, 0, 0, 0, 0, 3, 52, 0, 0));
            m_monsters.Add(new MM1Monster(88, "Manticore", 4, 0, 40, 6, 6, 0, 4, 12, 600, 26, 42, 0, 3, 20, 0, 1, 3, 28, 0, 0));
            m_monsters.Add(new MM1Monster(89, "Medusa", 2, 0, 32, 5, 4, 0, 1, 9, 1000, 12, 36, 15, 15, 20, 0, 1, 135, 0, 0, 0));
            m_monsters.Add(new MM1Monster(90, "Rakshasha", 4, 50, 40, 14, 5, 0, 3, 14, 3000, 11, 31, 14, 13, 80, 0, 5, 255, 255, 0, 0));
            m_monsters.Add(new MM1Monster(91, "Stone Golem", 3, 20, 50, 7, 40, 0, 1, 6, 1500, 16, 48, 0, 0, 0, 0, 0, 255, 1, 0, 0));
            m_monsters.Add(new MM1Monster(92, "Cave Troll", 6, 0, 50, 7, 11, 0, 3, 10, 900, 17, 32, 0, 0, 0, 0, 1, 163, 28, 0, 1));
            m_monsters.Add(new MM1Monster(93, "Hill Troll", 5, 0, 50, 8, 12, 0, 3, 11, 1100, 17, 32, 0, 0, 0, 0, 2, 179, 10, 0, 1));
            m_monsters.Add(new MM1Monster(94, "Warlock", 4, 0, 40, 8, 8, 0, 1, 16, 800, 52, 26, 0, 16, 95, 0, 60, 3, 21, 0, 0));
            m_monsters.Add(new MM1Monster(95, "Werebear", 4, 0, 45, 8, 8, 0, 4, 14, 800, 31, 32, 9, 0, 0, 0, 10, 135, 0, 1, 0));
            m_monsters.Add(new MM1Monster(96, "Wicked Witch", 4, 0, 30, 4, 6, 0, 2, 14, 1000, 12, 26, 23, 10, 40, 0, 50, 3, 3, 0, 0));
            m_monsters.Add(new MM1Monster(97, "Assassin", 4, 0, 45, 6, 8, 0, 1, 19, 2000, 57, 38, 18, 153, 0, 0, 0, 3, 48, 1, 0));
            m_monsters.Add(new MM1Monster(98, "Banshee", 2, 0, 40, 10, 10, 0, 1, 12, 3000, 62, 52, 17, 8, 40, 1, 1, 135, 0, 0, 0));
            m_monsters.Add(new MM1Monster(99, "Cave Giant", 6, 0, 50, 10, 16, 0, 3, 12, 2000, 17, 42, 0, 158, 0, 0, 5, 3, 60, 0, 0));
            m_monsters.Add(new MM1Monster(100, "Chimera", 4, 0, 60, 8, 5, 0, 6, 14, 5000, 26, 35, 0, 14, 40, 0, 1, 127, 28, 0, 0));
            m_monsters.Add(new MM1Monster(101, "Air Elemental", 1, 20, 70, 7, 15, 0, 1, 20, 4000, 27, 52, 19, 17, 30, 0, 5, 175, 0, 1, 0));
            m_monsters.Add(new MM1Monster(102, "Executioner", 1, 0, 60, 8, 12, 0, 2, 14, 4000, 57, 32, 18, 0, 0, 0, 0, 3, 28, 1, 0));
            m_monsters.Add(new MM1Monster(103, "Gorgon", 4, 0, 55, 8, 12, 0, 1, 12, 6000, 45, 35, 15, 11, 40, 0, 5, 3, 0, 0, 0));
            m_monsters.Add(new MM1Monster(104, "Lesser Demon", 4, 20, 40, 10, 8, 0, 4, 16, 2500, 59, 52, 17, 9, 15, 0, 0, 255, 125, 1, 1));
            m_monsters.Add(new MM1Monster(105, "Lesser Devil", 4, 20, 40, 8, 6, 0, 5, 16, 2500, 59, 52, 17, 9, 25, 0, 0, 255, 125, 1, 1));
            m_monsters.Add(new MM1Monster(106, "Mummy", 6, 0, 50, 7, 20, 0, 2, 7, 2000, 61, 48, 9, 0, 0, 1, 0, 191, 96, 0, 0));
            m_monsters.Add(new MM1Monster(107, "Necromancer", 4, 40, 35, 7, 8, 0, 2, 17, 2000, 54, 42, 0, 18, 95, 0, 1, 3, 63, 0, 0));
            m_monsters.Add(new MM1Monster(108, "Specter", 6, 0, 47, 8, 12, 0, 1, 12, 3000, 58, 48, 17, 0, 0, 1, 0, 143, 0, 0, 0));
            m_monsters.Add(new MM1Monster(109, "White Wolf", 6, 0, 42, 10, 12, 0, 3, 14, 2500, 29, 50, 9, 27, 30, 0, 0, 19, 4, 0, 0));
            m_monsters.Add(new MM1Monster(110, "Wyvern", 6, 0, 45, 7, 18, 0, 2, 12, 2000, 46, 32, 21, 0, 0, 0, 5, 3, 38, 0, 0));
            m_monsters.Add(new MM1Monster(111, "Green Dragon", 2, 20, 55, 8, 8, 0, 3, 12, 6000, 43, 35, 12, 25, 50, 0, 0, 255, 63, 1, 0));
            m_monsters.Add(new MM1Monster(112, "Blue Dragon", 3, 20, 50, 8, 10, 0, 3, 13, 6000, 43, 35, 12, 26, 50, 0, 5, 255, 63, 1, 0));
            m_monsters.Add(new MM1Monster(113, "Evil Eye", 2, 20, 65, 10, 10, 0, 2, 10, 5000, 21, 38, 15, 21, 60, 0, 0, 255, 103, 0, 0));
            m_monsters.Add(new MM1Monster(114, "Earth Elemental", 1, 0, 80, 8, 20, 0, 1, 18, 4000, 27, 52, 19, 17, 35, 0, 5, 215, 0, 1, 0));
            m_monsters.Add(new MM1Monster(115, "Ghost", 3, 0, 60, 10, 10, 0, 1, 10, 4000, 62, 48, 16, 0, 0, 1, 0, 135, 0, 0, 0));
            m_monsters.Add(new MM1Monster(116, "Frost Giant", 6, 0, 70, 10, 24, 0, 1, 12, 5000, 23, 38, 0, 148, 0, 0, 0, 147, 62, 1, 0));
            m_monsters.Add(new MM1Monster(117, "Stone Giant", 6, 0, 80, 10, 10, 0, 4, 12, 4000, 19, 38, 0, 148, 0, 0, 2, 131, 62, 1, 0));
            m_monsters.Add(new MM1Monster(118, "8 Headed Hydra", 1, 0, 75, 10, 8, 0, 8, 13, 7500, 44, 34, 0, 14, 35, 0, 0, 131, 116, 1, 0));
            m_monsters.Add(new MM1Monster(119, "Lava Beast", 6, 0, 50, 5, 12, 0, 2, 9, 4000, 20, 35, 0, 14, 20, 0, 0, 231, 0, 0, 0));
            m_monsters.Add(new MM1Monster(120, "Mantis Warrior", 3, 0, 70, 8, 12, 0, 4, 16, 4000, 4, 32, 12, 0, 0, 0, 0, 131, 68, 1, 0));
            m_monsters.Add(new MM1Monster(121, "Naga", 3, 25, 65, 8, 8, 0, 1, 15, 4000, 35, 22, 8, 8, 40, 0, 0, 131, 60, 0, 0));
            m_monsters.Add(new MM1Monster(122, "Dinobeetle", 4, 0, 200, 10, 50, 0, 1, 8, 6000, 3, 32, 0, 0, 0, 0, 0, 131, 28, 1, 0));
            m_monsters.Add(new MM1Monster(123, "Sphinx", 2, 40, 60, 11, 10, 0, 3, 18, 5000, 34, 22, 7, 8, 40, 0, 50, 255, 63, 0, 0));
            m_monsters.Add(new MM1Monster(124, "Vampire", 4, 10, 55, 9, 12, 0, 2, 14, 5000, 59, 54, 17, 13, 50, 1, 0, 135, 61, 1, 1));
            m_monsters.Add(new MM1Monster(125, "Warrior", 4, 0, 75, 12, 12, 0, 2, 14, 4000, 50, 40, 0, 148, 0, 0, 1, 131, 38, 1, 0));
            m_monsters.Add(new MM1Monster(126, "Wizard", 2, 0, 60, 8, 6, 0, 2, 18, 7000, 52, 42, 0, 19, 95, 0, 1, 139, 39, 0, 0));
            m_monsters.Add(new MM1Monster(127, "White Dragon", 3, 30, 65, 8, 12, 0, 3, 15, 10000, 42, 35, 0, 27, 60, 0, 0, 255, 127, 1, 0));
            m_monsters.Add(new MM1Monster(128, "Gray Dragon", 3, 30, 70, 8, 15, 0, 3, 16, 10000, 42, 35, 0, 28, 60, 0, 2, 255, 127, 1, 0));
            m_monsters.Add(new MM1Monster(129, "Arch Druid", 1, 0, 65, 10, 8, 0, 2, 18, 15000, 53, 34, 23, 20, 90, 0, 5, 255, 29, 0, 0));
            m_monsters.Add(new MM1Monster(130, "Chaotic Knight", 2, 20, 90, 14, 15, 0, 3, 18, 7000, 61, 58, 20, 158, 0, 0, 0, 135, 134, 1, 1));
            m_monsters.Add(new MM1Monster(131, "Greater Demon", 2, 30, 60, 15, 8, 0, 7, 19, 10000, 13, 58, 21, 13, 25, 0, 0, 255, 127, 1, 1));
            m_monsters.Add(new MM1Monster(132, "Greater Devil", 2, 30, 65, 12, 15, 0, 4, 19, 10000, 13, 58, 21, 13, 35, 0, 0, 255, 127, 1, 1));
            m_monsters.Add(new MM1Monster(133, "Fire Elemental", 1, 0, 90, 9, 30, 0, 1, 20, 8000, 27, 52, 19, 17, 40, 0, 5, 199, 0, 1, 0));
            m_monsters.Add(new MM1Monster(134, "Guardian Spirit", 2, 0, 60, 8, 6, 0, 6, 16, 6000, 64, 48, 17, 0, 0, 1, 0, 135, 0, 0, 0));
            m_monsters.Add(new MM1Monster(135, "Storm Giant", 4, 0, 100, 9, 30, 0, 2, 14, 10000, 23, 38, 0, 19, 40, 0, 1, 167, 198, 0, 0));
            m_monsters.Add(new MM1Monster(136, "12 Headed Hydra", 1, 0, 70, 10, 10, 0, 12, 16, 12000, 44, 34, 0, 14, 25, 0, 0, 131, 127, 1, 0));
            m_monsters.Add(new MM1Monster(137, "Invisible Thing", 4, 0, 50, 14, 10, 0, 3, 25, 6000, 58, 32, 22, 0, 0, 0, 2, 255, 0, 0, 0));
            m_monsters.Add(new MM1Monster(138, "Mage", 2, 20, 60, 10, 6, 0, 3, 20, 8000, 52, 42, 0, 21, 95, 0, 1, 143, 39, 0, 0));
            m_monsters.Add(new MM1Monster(139, "Master Thief", 2, 0, 65, 12, 8, 0, 2, 20, 5000, 49, 42, 23, 148, 0, 0, 1, 131, 62, 0, 0));
            m_monsters.Add(new MM1Monster(140, "Steel Golem", 2, 30, 70, 15, 25, 0, 2, 10, 7500, 61, 48, 0, 0, 0, 0, 0, 255, 129, 1, 0));
            m_monsters.Add(new MM1Monster(141, "Werephase Mummy", 4, 50, 70, 20, 20, 0, 2, 35, 8000, 61, 48, 9, 0, 0, 0, 1, 255, 1, 1, 1));
            m_monsters.Add(new MM1Monster(142, "Black Dragon", 2, 30, 75, 12, 18, 0, 3, 16, 15000, 41, 35, 7, 29, 70, 0, 0, 255, 127, 1, 0));
            m_monsters.Add(new MM1Monster(143, "Red Dragon", 2, 30, 80, 12, 20, 0, 3, 15, 15000, 41, 35, 13, 30, 70, 0, 2, 255, 127, 1, 0));
            m_monsters.Add(new MM1Monster(144, "XX!XX!XX!XX!XX", 2, 50, 10, 20, 1, 0, 1, 18, 15000, 9, 0, 23, 0, 0, 0, 0, 255, 0, 0, 0));
            m_monsters.Add(new MM1Monster(145, "Black Knight", 1, 30, 150, 16, 50, 0, 3, 20, 10000, 56, 54, 14, 168, 0, 0, 0, 255, 134, 1, 1));
            m_monsters.Add(new MM1Monster(146, "Demon Lord", 1, 50, 150, 20, 50, 0, 2, 30, 60000, 13, 54, 24, 23, 20, 0, 0, 255, 255, 1, 1));
            m_monsters.Add(new MM1Monster(147, "Arch Devil", 1, 50, 200, 16, 100, 0, 1, 30, 60000, 13, 54, 24, 23, 30, 0, 0, 255, 255, 1, 1));
            m_monsters.Add(new MM1Monster(148, "Gold Dragon", 1, 40, 150, 10, 20, 0, 5, 16, 50000, 41, 52, 8, 31, 80, 0, 10, 255, 255, 1, 0));
            m_monsters.Add(new MM1Monster(149, "Silver Dragon", 2, 40, 90, 8, 16, 0, 4, 16, 20000, 41, 52, 8, 31, 80, 0, 0, 255, 255, 1, 0));
            m_monsters.Add(new MM1Monster(150, "Diamond Golem", 2, 40, 100, 15, 60, 0, 3, 12, 15000, 16, 48, 0, 0, 0, 0, 0, 255, 1, 0, 0));
            m_monsters.Add(new MM1Monster(151, "16 Headed Hydra", 1, 20, 100, 15, 12, 0, 16, 12, 20000, 44, 54, 0, 29, 25, 0, 0, 255, 255, 1, 1));
            m_monsters.Add(new MM1Monster(152, "High Cleric", 1, 20, 100, 14, 16, 0, 3, 18, 10000, 54, 36, 0, 22, 80, 0, 5, 131, 127, 0, 0));
            m_monsters.Add(new MM1Monster(153, "Kirin", 1, 30, 90, 15, 40, 0, 4, 22, 14000, 26, 42, 25, 9, 25, 0, 50, 255, 135, 0, 0));
            m_monsters.Add(new MM1Monster(154, "Lich", 1, 90, 80, 10, 10, 0, 2, 20, 20000, 65, 54, 17, 23, 90, 1, 0, 255, 135, 0, 0));
            m_monsters.Add(new MM1Monster(155, "Arch Mage", 1, 30, 70, 12, 8, 0, 2, 25, 25000, 52, 42, 0, 24, 95, 0, 0, 255, 63, 0, 0));
            m_monsters.Add(new MM1Monster(156, "Master Archer", 1, 20, 120, 10, 16, 0, 8, 18, 25000, 51, 47, 22, 250, 0, 0, 5, 255, 199, 0, 0));
            m_monsters.Add(new MM1Monster(157, "Phoenix", 1, 10, 150, 13, 8, 0, 3, 24, 15000, 27, 42, 16, 14, 60, 0, 50, 255, 129, 0, 0));
            m_monsters.Add(new MM1Monster(158, "Roc", 2, 0, 130, 10, 50, 0, 3, 14, 15000, 39, 32, 0, 0, 0, 0, 20, 255, 0, 0, 0));
            m_monsters.Add(new MM1Monster(159, "Sand Worm", 3, 0, 200, 7, 200, 0, 1, 8, 15000, 35, 48, 18, 0, 0, 0, 1, 143, 129, 1, 0));
            m_monsters.Add(new MM1Monster(160, "Titan", 1, 95, 180, 13, 60, 0, 2, 30, 20000, 36, 58, 0, 250, 0, 0, 50, 255, 255, 0, 1));
            m_monsters.Add(new MM1Monster(161, "Algae Beast", 15, 0, 1, 0, 6, 0, 1, 1, 50, 20, 16, 2, 0, 0, 0, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(162, "Water Rat", 15, 0, 3, 1, 6, 0, 1, 6, 100, 6, 16, 2, 0, 0, 0, 0, 6, 0, 0, 0));
            m_monsters.Add(new MM1Monster(163, "Lamprey", 10, 0, 10, 2, 8, 0, 1, 15, 200, 35, 32, 3, 0, 0, 0, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(164, "Giant Leech", 6, 0, 10, 2, 8, 0, 1, 3, 250, 75, 32, 4, 0, 0, 0, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(165, "Crocodile", 8, 0, 30, 5, 10, 0, 2, 12, 300, 75, 32, 0, 0, 0, 0, 0, 6, 0, 1, 0));
            m_monsters.Add(new MM1Monster(166, "Giant Crab", 6, 0, 25, 9, 10, 0, 2, 12, 300, 0, 32, 0, 0, 0, 0, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(167, "Barracuda", 15, 0, 20, 5, 20, 0, 1, 16, 350, 75, 48, 0, 0, 0, 0, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(168, "Giant Squid", 4, 0, 30, 5, 6, 0, 8, 14, 400, 75, 38, 8, 6, 20, 0, 0, 7, 0, 0, 0));
            m_monsters.Add(new MM1Monster(169, "Electric Eel", 10, 0, 30, 5, 8, 0, 1, 15, 500, 75, 47, 8, 10, 20, 0, 0, 47, 0, 0, 0));
            m_monsters.Add(new MM1Monster(170, "Sea Hag", 6, 20, 40, 8, 6, 0, 3, 12, 500, 12, 38, 12, 15, 30, 0, 0, 15, 0, 0, 1));
            m_monsters.Add(new MM1Monster(171, "Hippocampus", 8, 20, 50, 12, 10, 0, 4, 18, 600, 38, 32, 7, 0, 0, 0, 0, 15, 0, 1, 1));
            m_monsters.Add(new MM1Monster(172, "Shark", 15, 0, 40, 6, 30, 0, 2, 24, 700, 75, 48, 0, 0, 0, 0, 0, 15, 0, 1, 0));
            m_monsters.Add(new MM1Monster(173, "Siren", 8, 20, 30, 8, 8, 0, 2, 13, 700, 64, 38, 15, 7, 20, 0, 0, 255, 0, 0, 1));
            m_monsters.Add(new MM1Monster(174, "Water Elemental", 6, 20, 80, 12, 50, 0, 1, 30, 1200, 9, 55, 0, 17, 50, 0, 0, 255, 0, 1, 1));
            m_monsters.Add(new MM1Monster(175, "Sea Serpent", 3, 40, 100, 10, 100, 0, 1, 20, 3000, 42, 48, 18, 0, 0, 0, 32, 143, 0, 1, 1));
            m_monsters.Add(new MM1Monster(176, "Sea Dragon", 3, 50, 150, 15, 50, 0, 4, 32, 20000, 42, 51, 8, 31, 50, 0, 32, 255, 0, 1, 1));
            m_monsters.Add(new MM1Monster(177, "Scorpion", 1, 50, 210, 12, 60, 0, 2, 20, 12000, 1, 48, 12, 0, 0, 0, 0, 207, 0, 1, 1));
            m_monsters.Add(new MM1Monster(178, "Dark Rider", 1, 90, 210, 15, 50, 0, 4, 30, 12000, 56, 63, 17, 19, 20, 0, 0, 127, 0, 1, 1));
            m_monsters.Add(new MM1Monster(179, "Winged Beast", 1, 50, 210, 12, 120, 0, 1, 30, 12000, 46, 48, 8, 0, 0, 0, 0, 159, 0, 1, 1));
            m_monsters.Add(new MM1Monster(180, "Great Sea Beast", 1, 50, 210, 12, 100, 0, 1, 30, 12000, 75, 48, 19, 0, 0, 0, 0, 143, 0, 1, 1));
            m_monsters.Add(new MM1Monster(181, "Demon King", 1, 120, 240, 30, 50, 0, 5, 35, 50000, 73, 63, 24, 23, 30, 0, 0, 255, 255, 1, 1));
            m_monsters.Add(new MM1Monster(182, "Succubus Queen", 1, 100, 150, 20, 30, 0, 3, 20, 10000, 74, 63, 19, 21, 50, 0, 0, 239, 127, 1, 1));
            m_monsters.Add(new MM1Monster(183, "Tyrannosaurus", 3, 0, 240, 10, 200, 0, 1, 12, 5000, 45, 32, 19, 0, 0, 0, 0, 7, 6, 1, 0));
            m_monsters.Add(new MM1Monster(184, "Alien", 6, 40, 100, 15, 20, 0, 2, 15, 1000, 4, 35, 21, 24, 30, 0, 16, 7, 1, 0, 0));
            m_monsters.Add(new MM1Monster(185, "Natives", 15, 0, 40, 6, 10, 0, 2, 10, 200, 7, 35, 4, 32, 20, 0, 21, 3, 3, 1, 0));
            m_monsters.Add(new MM1Monster(186, "Volcano God", 1, 120, 220, 30, 40, 0, 6, 32, 60000, 20, 63, 24, 13, 50, 0, 0, 255, 255, 1, 1));
            m_monsters.Add(new MM1Monster(187, "Paul Pead", 1, 50, 100, 10, 30, 0, 1, 19, 2000, 50, 51, 14, 13, 40, 0, 0, 143, 61, 1, 1));
            m_monsters.Add(new MM1Monster(188, "Pirate", 15, 10, 40, 8, 20, 0, 1, 17, 500, 58, 48, 10, 0, 0, 0, 0, 135, 7, 1, 0));
            m_monsters.Add(new MM1Monster(189, "Pirate Captain", 1, 30, 80, 10, 20, 0, 3, 18, 1000, 58, 51, 11, 7, 20, 0, 0, 135, 15, 0, 0));
            m_monsters.Add(new MM1Monster(190, "Gray Minotaur", 1, 40, 150, 13, 30, 0, 4, 20, 15000, 40, 63, 7, 10, 30, 0, 0, 143, 63, 1, 1));
            m_monsters.Add(new MM1Monster(191, "Lord Archer", 1, 100, 32, 15, 80, 0, 3, 21, 20000, 51, 63, 6, 250, 0, 0, 0, 207, 63, 1, 1));
            m_monsters.Add(new MM1Monster(192, "Brontasaurus", 3, 0, 200, 8, 150, 0, 1, 6, 5000, 45, 32, 19, 0, 0, 0, 16, 7, 6, 1, 0));
            m_monsters.Add(new MM1Monster(193, "Stegosaurus", 3, 0, 200, 15, 200, 0, 1, 6, 5000, 45, 32, 19, 0, 0, 0, 5, 7, 6, 1, 0));
            m_monsters.Add(new MM1Monster(194, "Killer Cadaver", 3, 0, 50, 8, 12, 0, 3, 15, 1500, 65, 48, 9, 0, 0, 0, 1, 1, 255, 1, 1));
            m_monsters.Add(new MM1Monster(195, "Okrim", 2, 0, 80, 11, 6, 0, 4, 18, 20000, 52, 42, 0, 19, 95, 0, 1, 139, 39, 0, 0));
            m_bValid = true;
        }
    }

    public class MM1Monster : MMMonster
    {
        public MM1MonsterTouch Touch;
        public MM1MonsterPower SpecialPower;
        public byte PowerChance;
        public byte Friendliness;
        public MM1Resistances Resistances;
        public MM1TreasureFlags Treasure;
        public bool TouchDisabled;
        public MM1MonsterCombatStatus CombatStatus;

        public override double AverageHP { get { return HP + ((8 - 1) / 2.0); } }

        public override BasicConditionFlags Condition { get { return GetCondition(CombatStatus); } }

        public static BasicConditionFlags GetCondition(MM1MonsterCombatStatus status)
        {
            if (status == MM1MonsterCombatStatus.Dead)
                return BasicConditionFlags.Dead;

            BasicConditionFlags cond = BasicConditionFlags.Good;

            if (status.HasFlag(MM1MonsterCombatStatus.Afraid))
                cond |= BasicConditionFlags.Afraid;
            if (status.HasFlag(MM1MonsterCombatStatus.Asleep))
                cond |= BasicConditionFlags.Asleep;
            if (status.HasFlag(MM1MonsterCombatStatus.Blinded))
                cond |= BasicConditionFlags.Blinded;
            if (status.HasFlag(MM1MonsterCombatStatus.Held))
                cond |= BasicConditionFlags.Held;
            if (status.HasFlag(MM1MonsterCombatStatus.Mindless))
                cond |= BasicConditionFlags.Mindless;
            if (status.HasFlag(MM1MonsterCombatStatus.Paralyze))
                cond |= BasicConditionFlags.Paralyzed;
            if (status.HasFlag(MM1MonsterCombatStatus.Silenced))
                cond |= BasicConditionFlags.Silenced;
            if (status.HasFlag(MM1MonsterCombatStatus.Webbed))
                cond |= BasicConditionFlags.Webbed;

            return cond;
        }

        public static MM1MonsterCombatStatus GetCondition(BasicConditionFlags status)
        {
            if (status.HasFlag(BasicConditionFlags.Dead))
                return MM1MonsterCombatStatus.Dead;

            MM1MonsterCombatStatus cond = MM1MonsterCombatStatus.Good;

            if (status.HasFlag(BasicConditionFlags.Afraid))
                cond |= MM1MonsterCombatStatus.Afraid;
            if (status.HasFlag(BasicConditionFlags.Asleep))
                cond |= MM1MonsterCombatStatus.Asleep;
            if (status.HasFlag(BasicConditionFlags.Blinded))
                cond |= MM1MonsterCombatStatus.Blinded;
            if (status.HasFlag(BasicConditionFlags.Held))
                cond |= MM1MonsterCombatStatus.Held;
            if (status.HasFlag(BasicConditionFlags.Mindless))
                cond |= MM1MonsterCombatStatus.Mindless;
            if (status.HasFlag(BasicConditionFlags.Paralyzed))
                cond |= MM1MonsterCombatStatus.Paralyze;
            if (status.HasFlag(BasicConditionFlags.Silenced))
                cond |= MM1MonsterCombatStatus.Silenced;
            if (status.HasFlag(BasicConditionFlags.Webbed))
                cond |= MM1MonsterCombatStatus.Webbed;

            return cond;
        }

        public static string GetMonsterPowerString(MM1MonsterPower power, byte chance)
        {
            if (power.HasFlag(MM1MonsterPower.Missile))
                return String.Format("Missile 1d{0}", (byte)(power & MM1MonsterPower.AllPowers));

            string strPower = "";

            switch (power & MM1MonsterPower.AllPowers)
            {
                case MM1MonsterPower.None: return "None";
                case MM1MonsterPower.ACurse: strPower = "Curse"; break;
                case MM1MonsterPower.EnergyBlast: strPower = "Energy Blast"; break;
                case MM1MonsterPower.Fire: strPower = "Cast Fire"; break;
                case MM1MonsterPower.Blindness: strPower = "Blind"; break;
                case MM1MonsterPower.SpraysPoison: strPower = "Poison"; break;
                case MM1MonsterPower.SpraysAcid: strPower = "Acid"; break;
                case MM1MonsterPower.Sleep: strPower = "Sleep"; break;
                case MM1MonsterPower.Paralyze: strPower = "Paralyze"; break;
                case MM1MonsterPower.Dispel: strPower = "Dispel"; break;
                case MM1MonsterPower.LightningBolt: strPower = "Lightning"; break;
                case MM1MonsterPower.StrangeGas: strPower = "Stone"; break;
                case MM1MonsterPower.Explode: strPower = "Explode"; break;
                case MM1MonsterPower.FireBall: strPower = "Fireball"; break;
                case MM1MonsterPower.BreathesFire: strPower = "Breathe Fire"; break;
                case MM1MonsterPower.Gazes: strPower = "Gaze"; break;
                case MM1MonsterPower.AcidArrow: strPower = "Acid Arrow"; break;
                case MM1MonsterPower.CallsupontheElements: strPower = "Call Elements"; break;
                case MM1MonsterPower.ColdBeam: strPower = "Cold Beam"; break;
                case MM1MonsterPower.DancingSword: strPower = "Dancing Sword"; break;
                case MM1MonsterPower.MagicDrain: strPower = "Magic Drain"; break;
                case MM1MonsterPower.FingerofDeath: strPower = "Finger of Death"; break;
                case MM1MonsterPower.SunRay: strPower = "Sun Ray"; break;
                case MM1MonsterPower.Disintegration: strPower = "Disintegration"; break;
                case MM1MonsterPower.CommandsEnergy: strPower = "Command Energy"; break;
                case MM1MonsterPower.DragonPoison: strPower = "Dragon Poison"; break;
                case MM1MonsterPower.DragonLightning: strPower = "Dragon Lightning"; break;
                case MM1MonsterPower.DragonFrost: strPower = "Dragon Frost"; break;
                case MM1MonsterPower.DragonSpikes: strPower = "Dragon Spikes"; break;
                case MM1MonsterPower.DragonAcid: strPower = "Dragon Acid"; break;
                case MM1MonsterPower.DragonFire: strPower = "Dragon Fire"; break;
                case MM1MonsterPower.DragonEnergy: strPower = "Dragon Energy"; break;
                case MM1MonsterPower.Swarm: strPower = "Swarm"; break;
                default: return String.Format("Unknown 0x{0:X2}", (byte)power);
            }

            if (chance == 255)
                return strPower;

            return String.Format("{0} ({1}%)", strPower, chance);
        }

        public static string GetMonsterConditionSingle(MM1MonsterCombatStatus status)
        {
            // Assumes only one bit is set
            if (status == MM1MonsterCombatStatus.Dead)
                return "Dead";
            if (status.HasFlag(MM1MonsterCombatStatus.Afraid))
                return "Afraid";
            if (status.HasFlag(MM1MonsterCombatStatus.Asleep))
                return "Asleep";
            if (status.HasFlag(MM1MonsterCombatStatus.Blinded))
                return "Blinded";
            if (status.HasFlag(MM1MonsterCombatStatus.Held))
                return "Held";
            if (status.HasFlag(MM1MonsterCombatStatus.Mindless))
                return "Mindless";
            if (status.HasFlag(MM1MonsterCombatStatus.Paralyze))
                return "Paralyzed";
            if (status.HasFlag(MM1MonsterCombatStatus.Silenced))
                return "Silenced";
            if (status.HasFlag(MM1MonsterCombatStatus.Webbed))
                return "Webbed";
            return "Good";
        }

        public static string GetMonsterConditionAll(MM1MonsterCombatStatus status)
        {
            StringBuilder sb = new StringBuilder();

            if (status == MM1MonsterCombatStatus.Dead)
                return "Dead";

            if (status.HasFlag(MM1MonsterCombatStatus.Afraid))
                sb.Append("Afraid, ");
            if (status.HasFlag(MM1MonsterCombatStatus.Asleep))
                sb.Append("Asleep, ");
            if (status.HasFlag(MM1MonsterCombatStatus.Blinded))
                sb.Append("Blinded, ");
            if (status.HasFlag(MM1MonsterCombatStatus.Held))
                sb.Append("Held, ");
            if (status.HasFlag(MM1MonsterCombatStatus.Mindless))
                sb.Append("Mindless, ");
            if (status.HasFlag(MM1MonsterCombatStatus.Paralyze))
                sb.Append("Paralyzed, ");
            if (status.HasFlag(MM1MonsterCombatStatus.Silenced))
                sb.Append("Silenced, ");
            if (status.HasFlag(MM1MonsterCombatStatus.Webbed))
                sb.Append("Webbed, ");

            if (sb.Length < 2)
                return "Good";

            return Global.Trim(sb).ToString();
        }

        public static string GetMonsterResistanceSingle(MM1Resistances resist)
        {
            // Assumes only one bit is set
            if (resist.HasFlag(MM1Resistances.Fire))
                return "Fire";
            if (resist.HasFlag(MM1Resistances.Cold))
                return "Cold";
            if (resist.HasFlag(MM1Resistances.Energy))
                return "Energy";
            if (resist.HasFlag(MM1Resistances.Lightning))
                return "Lightning";
            if (resist.HasFlag(MM1Resistances.Mental))
                return "Mental";
            if (resist.HasFlag(MM1Resistances.Paralyze))
                return "Paralyze";
            if (resist.HasFlag(MM1Resistances.Sleep))
                return "Sleep";
            if (resist.HasFlag(MM1Resistances.Weapons))
                return "Weapons";
            return "Good";
        }

        public static string GetMonsterTouchString(MM1MonsterTouch touch, bool disabled, bool shortdisplay)
        {
            string strTouch = "";
            switch (touch & MM1MonsterTouch.AllEffects)
            {
                case MM1MonsterTouch.None: strTouch = "None"; break;
                case MM1MonsterTouch.TakesFood: strTouch =  "Steal Food"; break;
                case MM1MonsterTouch.InflictsDisease1: strTouch = "Disease (rare)"; break;
                case MM1MonsterTouch.CausesParalysis1: strTouch = "Paralyze (rare)"; break;
                case MM1MonsterTouch.InducesPoison1: strTouch = "Poison (rare)"; break;
                case MM1MonsterTouch.StealsSomeGems: strTouch =  "Steal Gems"; break;
                case MM1MonsterTouch.ReducesEndurance: strTouch =  "Reduce Endurance"; break;
                case MM1MonsterTouch.InducesSleep: strTouch =  "Sleep"; break;
                case MM1MonsterTouch.CausesParalysis2: strTouch =  "Paralyze"; break;
                case MM1MonsterTouch.InflictsDisease2: strTouch =  "Disease"; break;
                case MM1MonsterTouch.StealsSomeGold: strTouch =  "Steal Gold"; break;
                case MM1MonsterTouch.StealsSomething: strTouch =  "Steal Item"; break;
                case MM1MonsterTouch.InducesPoison2: strTouch =  "Poison"; break;
                case MM1MonsterTouch.CausesBlindness: strTouch =  "Blind"; break;
                case MM1MonsterTouch.DrainsLifeforce1: strTouch = "Drain Life (rare)"; break;
                case MM1MonsterTouch.IsTurnedtoStone: strTouch =  "Stone"; break;
                case MM1MonsterTouch.CausesRapidAging: strTouch =  "Rapid Aging"; break;
                case MM1MonsterTouch.DrainsLifeforce2: strTouch =  "Drain Life"; break;
                case MM1MonsterTouch.IsKilled: strTouch =  "Kill"; break;
                case MM1MonsterTouch.InducesUnconsciousness: strTouch =  "Knockout"; break;
                case MM1MonsterTouch.DrainsMight: strTouch =  "Drain Might"; break;
                case MM1MonsterTouch.DrainsAbilities: strTouch =  "Drain Stats"; break;
                case MM1MonsterTouch.StealsBackpack: strTouch =  "Steal Backpack"; break;
                case MM1MonsterTouch.StealsGoldandGems: strTouch =  "Steal Gold/Gems"; break;
                case MM1MonsterTouch.IsEradicated: strTouch =  "Eradicate"; break;
                case MM1MonsterTouch.DrainsSpellPoints: strTouch =  "Drain SP"; break;
                default: return String.Format("Unknown 0x{0:X2}", (byte)touch);
            }
            if (disabled && shortdisplay)
                strTouch = "None";
            else if (disabled)
            {
                if (strTouch.EndsWith(" (rare)"))
                    strTouch = strTouch.Substring(0, strTouch.Length - 7) + " (disabled)";
                else
                    strTouch += " (disabled)";
            }

            return strTouch;
        }

        public static string GetHeaderString()
        {
            //return "Idx Name             GroupSiz Friendly HitPoint ArmorCla Damage   NumAttac Speed    ExperienceGained  Treasure MagicRes Resistan OnTouch  SpecialP ChancePo Bravery  ImageIdx";

            return String.Format("{0,-3} {1,-16} {2,-2} {3,-3} {4,-7} {5,-2} {6,-9} {7,-2} {8,-2} {9,-5} {10,-2} {11,-3} {12,-17} {13,-22} {14}{18}{19} {15,-4} {16,-8} {17,-8}\r\n",
                "Id",
                "Name",
                "Gr",
                "MR",
                "HP",
                "AC",
                "Damage",
                "At",
                "Sp",
                "XP",
                "ic",
                "Brv",
                "Touch",
                "Special Power",
                "U",
                "Fr",
                "Treasure",
                "Resist",
                "A",
                "R"
                );
        }

        public string GetInputString()
        {
            return String.Format("m_monsters.Add(new MM1Monster({0},\"{1}\",{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21}));",
                Index,
                ProperName,
                GroupSize,
                MagicResist,
                HP,
                AC,
                Damage,
                ((byte)Touch > 127) ? 1 : 0,
                NumAttacks,
                Speed,
                Experience,
                ImageIndex,
                Bravery,
                (byte)Touch,
                (byte)SpecialPower,
                PowerChance,
                Undead ? 1 : 0,
                Friendliness,
                (byte)Resistances,
                (byte)Treasure,
                Advance ? 1 : 0,
                Regenerate ? 1 : 0
                );
        }

        public string GetLongDescription()
        {
            return String.Format("{0}, {1} HP, {2} Exp, {3} Dmg, {4} AC",
                Name,
                HP,
                Experience,
                Damage,
                AC);
        }
        
        public string AttributesString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Undead)
                    sb.Append("Undead, ");
                if (Advance)
                    sb.Append("Advance, ");
                if (Regenerate)
                    sb.Append("Regeneration, ");
                if (Global.Trim(sb).Length == 0)
                    sb.Append("None");
                return sb.ToString();
            }
        }

        public override string GetMultiLineDescription(bool bActive)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(ProperName);
            if (bActive)
            {
                sb.AppendFormat("HP: {0}/{1}\r\n", CurrentHP, HP);
                sb.AppendFormat("Status: {0}\r\n", GetMonsterConditionAll(CombatStatus));
            }
            else
                sb.AppendFormat("MaxHP: {0}\r\n", HP + 8);
            sb.AppendFormat("AC: {0}\r\n", AC);
            sb.AppendFormat("Magic Resistance: {0}\r\n", MagicResist);
            string strResist = ResistancesString;
            sb.AppendFormat("Resists: {0}\r\n", String.IsNullOrWhiteSpace(strResist) ? "None" : strResist);
            sb.AppendFormat("Damage: {0}\r\n", DamageString);
            sb.AppendFormat("Speed: {0}\r\n", Speed);
            sb.AppendFormat("Experience: {0}\r\n", Experience);
            sb.AppendFormat("Bravery: {0}\r\n", Bravery);
            sb.AppendFormat("On Touch: {0}\r\n", GetMonsterTouchString(Touch, TouchDisabled, false));
            sb.AppendFormat("Special Power: {0}\r\n", GetMonsterPowerString(SpecialPower, PowerChance));
            sb.AppendFormat("Attributes: {0}\r\n", AttributesString);
            string strTreasure = TreasureString;
            sb.AppendFormat("Treasure: {0}\r\n", String.IsNullOrWhiteSpace(strTreasure) ? "None" : strTreasure);
            return sb.ToString();
        }

        public string GetInsertString()
        {
            return String.Format("m_monsters.add({0},\"{1}\",{2},{3},{4},{5},{6},{7},{8},{9},{10},{11},{12},{13},{14},{15},{16},{17},{18},{19},{20},{21});",
                Index,
                Name,
                GroupSize,
                MagicResist,
                HP,
                AC,
                Damage,
                TouchDisabled ? 1 : 0,
                NumAttacks,
                Speed,
                Experience,
                ImageIndex,
                Bravery,
                (byte) Touch,
                (byte) SpecialPower,
                PowerChance,
                Undead ? 1 : 0,
                Friendliness,
                (byte) Treasure,
                (byte) Resistances,
                Advance ? 1 : 0,
                Regenerate ? 1 : 0
                );
        }

        public MM1Monster(int index,string name, int groupsize, int magicresist, int hp, int ac, int damage, int touchdisabled, int numattacks, int speed, long experience, int imageindex,
            int bravery, int touch, int specialpower, byte powerchance, int undead, byte friendliness, int resistances, int treasure, int advance, int regenerate)
        {
            Index = index;
            Name = name;
            GroupSize = groupsize;
            MagicResist = magicresist;
            HP = hp;
            AC = ac;
            Damage = damage;
            TouchDisabled = touchdisabled > 0;
            NumAttacks = numattacks;
            Speed = speed;
            Experience = experience;
            ImageIndex = imageindex;
            Touch = (MM1MonsterTouch) touch;
            SpecialPower = (MM1MonsterPower) specialpower;
            PowerChance = powerchance;
            Bravery = bravery;
            Undead = undead > 0;
            Friendliness = friendliness;
            Resistances = (MM1Resistances) resistances;
            Treasure = (MM1TreasureFlags) treasure;
            Advance = advance > 0;
            Regenerate = regenerate > 0;
        }

        public string GetRowString()
        {
            //StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("{0,-3} {1,-16} ", Index, Name);
            //foreach (byte b in unknown)
            //    sb.AppendFormat("{0} ", GetBits(b));
            //return sb.ToString();

            return String.Format("{0,-3} {1,-16} {2,-2} {3,-3} {4,-7} {5,-2} 1d{6,-7} {7,-2} {8,-2} {9,-5} {10,-2} {11,-3} {12,-17} {13,-22} {14}{18}{19} {15,-4} {16,-8} {17,-8}",
                Index,
                Name,
                GroupSize,
                MagicResist,
                String.Format("{0}+1d8", HP),
                AC,
                GetDamageString(Damage),
                NumAttacks,
                Speed,
                Experience,
                ImageIndex,
                Bravery,
                GetMonsterTouchString(Touch, TouchDisabled, true),
                GetMonsterPowerString(SpecialPower, PowerChance),
                Undead ? "U" : " ",
                Friendliness,
                Global.GetBits((byte)Treasure, "54321ggG", "        "),
                Global.GetBits((byte)Resistances, "WFLCEPMS", "        "),
                Advance ? "A" : " ",
                Regenerate ? "R" : " "
                );
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

        public string ResistancesString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (MagicResist > 0)
                    sb.AppendFormat("Magic ({0}), ", MagicResist);
                if (Resistances.HasFlag(MM1Resistances.Acid))
                    sb.Append("Acid, ");
                if (Resistances.HasFlag(MM1Resistances.Cold))
                    sb.Append("Cold, ");
                if (Resistances.HasFlag(MM1Resistances.Energy))
                    sb.Append("Energy, ");
                if (Resistances.HasFlag(MM1Resistances.Fire))
                    sb.Append("Fire, ");
                if (Resistances.HasFlag(MM1Resistances.Lightning))
                    sb.Append("Lightning, ");
                if (Resistances.HasFlag(MM1Resistances.Mental))
                    sb.Append("Mental, ");
                if (Resistances.HasFlag(MM1Resistances.Paralyze))
                    sb.Append("Hold, ");
                if (Resistances.HasFlag(MM1Resistances.Sleep))
                    sb.Append("Sleep, ");
                if (Resistances.HasFlag(MM1Resistances.Weapons))
                    sb.Append("Weapons, ");
                Global.Trim(sb);
                return sb.ToString();
            }
        }

        public override GenericResistanceFlags GenericResistances { get { return GetResistances(Resistances); } }

        public static GenericResistanceFlags GetResistances(MM1Resistances resistances)
        {
            GenericResistanceFlags result = GenericResistanceFlags.None;

            if (resistances.HasFlag(MM1Resistances.Acid))
                result |= GenericResistanceFlags.Acid;
            if (resistances.HasFlag(MM1Resistances.Cold))
                result |= GenericResistanceFlags.Cold;
            if (resistances.HasFlag(MM1Resistances.Energy))
                result |= GenericResistanceFlags.Energy;
            if (resistances.HasFlag(MM1Resistances.Fire))
                result |= GenericResistanceFlags.Fire;
            if (resistances.HasFlag(MM1Resistances.Lightning))
                result |= GenericResistanceFlags.Electricity;
            if (resistances.HasFlag(MM1Resistances.Mental))
                result |= GenericResistanceFlags.Mental;
            if (resistances.HasFlag(MM1Resistances.Paralyze))
                result |= GenericResistanceFlags.Paralyze;
            if (resistances.HasFlag(MM1Resistances.Sleep))
                result |= GenericResistanceFlags.Sleep;
            if (resistances.HasFlag(MM1Resistances.Weapons))
                result |= GenericResistanceFlags.Weapons;

            if (resistances.HasFlag(MM1Resistances.Fire) && resistances.HasFlag(MM1Resistances.Lightning))
                result |= GenericResistanceFlags.Acid;

            return result;
        }

        public static MM1Resistances GetResistances(GenericResistanceFlags flags)
        {
            MM1Resistances res = MM1Resistances.None;

            if (flags.HasFlag(GenericResistanceFlags.Acid))
                res |= (MM1Resistances.Fire | MM1Resistances.Lightning);
            if (flags.HasFlag(GenericResistanceFlags.Cold))
                res |= MM1Resistances.Cold;
            if (flags.HasFlag(GenericResistanceFlags.Energy))
                res |= MM1Resistances.Energy;
            if (flags.HasFlag(GenericResistanceFlags.Electricity))
                res |= MM1Resistances.Lightning;
            if (flags.HasFlag(GenericResistanceFlags.Mental))
                res |= MM1Resistances.Mental;
            if (flags.HasFlag(GenericResistanceFlags.Paralyze))
                res |= MM1Resistances.Paralyze;
            if (flags.HasFlag(GenericResistanceFlags.Sleep))
                res |= MM1Resistances.Sleep;
            if (flags.HasFlag(GenericResistanceFlags.Weapons))
                res |= MM1Resistances.Weapons;
            if (flags.HasFlag(GenericResistanceFlags.Fire))
                res |= MM1Resistances.Fire;

            return res;
        }

        public override string ResistancesStringShort
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (MagicResist > 0)
                    sb.AppendFormat("M{0}, ", MagicResist);
                if (Resistances.HasFlag(MM1Resistances.Acid))
                    sb.Append("A");
                if (Resistances.HasFlag(MM1Resistances.Cold))
                    sb.Append("C");
                if (Resistances.HasFlag(MM1Resistances.Energy))
                    sb.Append("E");
                if (Resistances.HasFlag(MM1Resistances.Fire))
                    sb.Append("F");
                if (Resistances.HasFlag(MM1Resistances.Lightning))
                    sb.Append("L");
                if (Resistances.HasFlag(MM1Resistances.Mental))
                    sb.Append("M");
                if (Resistances.HasFlag(MM1Resistances.Paralyze))
                    sb.Append("H");
                if (Resistances.HasFlag(MM1Resistances.Sleep))
                    sb.Append("S");
                if (Resistances.HasFlag(MM1Resistances.Weapons))
                    sb.Append("W");
                return Global.Trim(sb).ToString();
            }
        }

        public override string AllPowersString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (SpecialPower != MM1MonsterPower.None)
                    sb.AppendFormat("{0}, ", GetMonsterPowerString(SpecialPower, PowerChance));
                if (Touch != MM1MonsterTouch.None && !TouchDisabled)
                    sb.AppendFormat("Touch: {0}, ", GetMonsterTouchString(Touch, TouchDisabled, true));
                if (Undead)
                    sb.Append("Undead, ");
                if (Advance)
                    sb.Append("Advance, ");
                if (Regenerate)
                    sb.Append("Regen, ");
                return Global.Trim(sb).ToString();
            }
        }

        public string TreasureString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Treasure.HasFlag(MM1TreasureFlags.ItemLevel5))
                    sb.Append("Lv5 Item, ");
                if (Treasure.HasFlag(MM1TreasureFlags.ItemLevel4))
                    sb.Append("Lv4 Item, ");
                if (Treasure.HasFlag(MM1TreasureFlags.ItemLevel3))
                    sb.Append("Lv3 Item, ");
                if (Treasure.HasFlag(MM1TreasureFlags.ItemLevel2))
                    sb.Append("Lv2 Item, ");
                if (Treasure.HasFlag(MM1TreasureFlags.ItemLevel1))
                    sb.Append("Lv1 Item, ");
                if (Treasure.HasFlag(MM1TreasureFlags.Gems))
                    sb.Append("1-5 Gems, ");
                if (Treasure.HasFlag(MM1TreasureFlags.Gold10) && Treasure.HasFlag(MM1TreasureFlags.Gold100))
                    sb.Append("256-1024 Gold, ");
                else if (Treasure.HasFlag(MM1TreasureFlags.Gold10))
                    sb.Append("1-10 Gold, ");
                else if (Treasure.HasFlag(MM1TreasureFlags.Gold100))
                    sb.Append("1-100 Gold, ");
                return Global.Trim(sb).ToString();
            }
        }

        public override int TreasureStrength
        {
            get
            {
                return (int)Treasure;
            }
        }

        public override string TreasureStringShort
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                if (Treasure.HasFlag(MM1TreasureFlags.Gems))
                    sb.Append("G");
                if (Treasure.HasFlag(MM1TreasureFlags.Gold100) && Treasure.HasFlag(MM1TreasureFlags.Gold10))
                    sb.Append("ggg");
                else if (Treasure.HasFlag(MM1TreasureFlags.Gold10))
                    sb.Append("g");
                else if (Treasure.HasFlag(MM1TreasureFlags.Gold100))
                    sb.Append("gg");
                if (Treasure.HasFlag(MM1TreasureFlags.ItemLevel1))
                    sb.Append("1");
                if (Treasure.HasFlag(MM1TreasureFlags.ItemLevel2))
                    sb.Append("2");
                if (Treasure.HasFlag(MM1TreasureFlags.ItemLevel3))
                    sb.Append("3");
                if (Treasure.HasFlag(MM1TreasureFlags.ItemLevel4))
                    sb.Append("4");
                if (Treasure.HasFlag(MM1TreasureFlags.ItemLevel5))
                    sb.Append("5");
                return sb.ToString();
            }
        }

        public static string GetDamageString(int damage)
        {
            return String.Format("{0}", damage);
        }

        public MM1Monster()
        {
            Name = "<none>";
            Index = -1;
        }

        public override Monster Clone()
        {
            return new MM1Monster(Index, Name, GroupSize, MagicResist, HP, AC, Damage, TouchDisabled ? (byte) 1 : (byte) 0, NumAttacks, Speed, Experience, ImageIndex,
                Bravery, (byte) Touch, (byte) SpecialPower, PowerChance, Undead ? (byte) 1 : (byte) 0, Friendliness,
                (byte) Resistances, (byte) Treasure, Advance ? (byte) 1 : (byte) 0, Regenerate ? (byte) 1 : (byte) 0);
        }

        private void SetFromBytes(byte[] bytes, int iIndex)
        {
            Name = Encoding.ASCII.GetString(bytes, iIndex, 15).Trim();
            GroupSize = bytes[iIndex + 15];
            Friendliness = bytes[iIndex + 16];
            HP = bytes[iIndex + 17];        // Actual in-game monster will have HP + 1d8
            AC = bytes[iIndex + 18];
            Damage = bytes[iIndex + 19];
            NumAttacks = bytes[iIndex + 20];
            Speed = bytes[iIndex + 21];
            Experience = BitConverter.ToUInt16(bytes, iIndex + 22);
            Treasure = (MM1TreasureFlags)bytes[iIndex + 24];
            MagicResist = (byte)(bytes[iIndex + 25] & 0x7f);
            Undead = (bytes[iIndex + 25] & 0x80) > 0;
            Resistances = (MM1Resistances)bytes[iIndex + 26];
            Touch = (MM1MonsterTouch) (bytes[iIndex + 27] & (byte)MM1MonsterTouch.AllEffects);
            TouchDisabled = (bytes[iIndex + 27] & (byte)MM1MonsterTouch.Disabled) > 0;
            SpecialPower = (MM1MonsterPower)bytes[iIndex + 28];
            PowerChance = bytes[iIndex + 29];
            Advance = (bytes[iIndex + 30] & (byte)MM1BraveryFlags.Advance) > 0;
            Regenerate = (bytes[iIndex + 30] & (byte)MM1BraveryFlags.Regenerate) > 0;
            Bravery = (byte)(bytes[iIndex + 30] & (byte)MM1BraveryFlags.ValueFlags);
            ImageIndex = bytes[iIndex + 31];
        }

        public MM1Monster(byte[] bytes, int iIndex, byte currentHP, MM1MonsterCombatStatus status, bool moved)
        {
            SetFromBytes(bytes, iIndex);
            CombatStatus = status;
            CurrentHP = currentHP;
            HasMoved = moved;
        }

        public MM1Monster(byte[] bytes, int iIndex, MM1MonsterCombatStatus status, bool moved)
        {
            SetFromBytes(bytes, iIndex);
            CombatStatus = status;
            CurrentHP = HP;
            HasMoved = moved;
        }

        public override string HPString(bool bPreEncounter) { return bPreEncounter ? String.Format("{0}+1d8", HP) : CurrentHP.ToString(); }

        public override string ProperName
        {
            get
            {
                if (Name.StartsWith("XX"))
                    return Name;
                StringBuilder sb = new StringBuilder(Name);
                sb[0] = Char.ToUpper(sb[0]);
                for (int i = 1; i < sb.Length; i++)
                    if (Char.IsLetter(sb[i - 1]))
                        sb[i] = Char.ToLower(sb[i]);
                return sb.ToString();
            }
        }

        public string Dump()
        {
            return "";
        }
    }
}
