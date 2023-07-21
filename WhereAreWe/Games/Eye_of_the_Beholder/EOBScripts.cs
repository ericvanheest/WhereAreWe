using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    [Flags]
    public enum EOBScriptClass
    {
        None =      0x00,
        Fighter =   0x01,
        Mage =      0x02,
        Cleric =    0x04,
        Thief =     0x08
    }

    [Flags]
    public enum EOBActivationReason
    {
        None =          0x00,
        PartyEnter =    0x01,
        PartyLeave =    0x02,
        ItemEnter =     0x04,
        ItemLeave =     0x08,
        MissileEnter =  0x10
    }

    public enum EOBScriptRace
    {
        Human = 0,
        Elf = 1,
        HalfElf = 2,
        Dwarf = 3,
        Gnome = 4,
        Halfling = 5
    }

    public interface IScriptAddressLocator
    {
        Point GetLocation(int iAddress);
    }

    public class EOBConditional
    {
        public string Description;
        public string Abbreviation;
        public string Prefix;

        public EOBConditional(string strDescription)
        {
            Description = strDescription;
            Prefix = "and";
        }

        public override string ToString() { return Description; }
    }

    public class EOBScriptMonster
    {
        public int Index;
        public int LevelType;
        public Point Location;
        public int Position;
        public int Facing;
        public int Type;
        public int Graphic;
        public int Animation;
        public int Pause;
        public int Weapon;
        public int Loot;

        public EOBScriptMonster(byte[] bytes, int offset)
        {
            if (offset + 14 <= bytes.Length)
            {
                LevelType = bytes[offset + 1];
                Location = EOBMemoryHacker.PointFromPackedFive(BitConverter.ToUInt16(bytes, offset + 2));
                Position = bytes[offset + 4];
                Facing = bytes[offset + 5];
                Type = bytes[offset + 6];
                Graphic = bytes[offset + 7];
                Animation = bytes[offset + 8];
                Pause = bytes[offset + 9];
                Weapon = BitConverter.ToInt16(bytes, offset + 10);
                Loot = BitConverter.ToInt16(bytes, offset + 12);
                Index = Type;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("#{0} at {1},{2}", Index, Location.X, Location.Y);
            if (Weapon != 0)
                sb.AppendFormat(", Weapon {0}", Weapon);
            if (Loot != 0)
                sb.AppendFormat(", Loot {0}", Loot);
            return sb.ToString();
        }
    }
}

