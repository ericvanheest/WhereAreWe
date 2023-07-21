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
    public enum WallSolidity
    {
        NonePass     = 0x00,
        LightPass    = 0x01,
        ObjectPass   = 0x02,
        MonsterPass  = 0x04,
        PlayerPass   = 0x08,
        Destructible = 0x10,

        AllPass          = 0x0F,
        FalseWall        = ObjectPass | MonsterPass | PlayerPass,
        MonsterBarrier   = LightPass | ObjectPass | PlayerPass
    }

    [Flags]
    public enum DoorStatus
    {
        None        = 0x00,
        Closed      = 0x01,
        PartlyOpen  = 0x02,
        Open        = 0x04,
        Grate       = 0x08,
        Transparent = 0x10,
        Locked      = 0x20,
        Forceable   = 0x40,

        Portcullis  = Grate | Transparent,
    }

    [Flags]
    public enum LevelChange
    {
        None        = 0x00,
        Up          = 0x01,
        Down        = 0x02,
        Gateway     = 0x04,
        Teleport    = 0x08,
        HoleUp      = 0x10,
        HoleDown    = 0x20
    }

    [Flags]
    public enum WallSpecials
    {
        None            = 0x000000,
        FloorPlate      = 0x000001,
        IndicatorLeft   = 0x000002,
        IndicatorRight  = 0x000004,
        IndicatorUp     = 0x000008,
        IndicatorDown   = 0x000010,
        KeyHole         = 0x000020,
        SwitchUp        = 0x000040,
        SwitchDown      = 0x000080,
        Alcove          = 0x000100,
        Writing         = 0x000200,
        Rune            = 0x000400,
        Button          = 0x000800,
        ButtonPressed   = 0x001000,
        Decoration      = 0x002000,
        Socket          = 0x004000,
        SocketFilled    = 0x008000,
        Eyes            = 0x010000
    }

    public class BasicWall
    {
        public WallSolidity Solidity;
        public DirectionFlags Facing;
        public Point Location;
        public int Map;
        public int Index;
        public string Description;
        public DoorStatus DoorType;
        public WallSpecials Specials;
        public LevelChange Transport;

        public static BasicWall Solid = new BasicWall("Solid");
        public static BasicWall Open = new BasicWall("Open", true, true);
        public static BasicWall Barrier = new BasicWall("Barrier", true, false);
        public static BasicWall FalseWall = new BasicWall("False Wall", false, true);

        private void Set(string strDesc, bool bLight, bool bObject, bool bMonster, bool bParty, int iMap, Point ptLocation, DirectionFlags dir, DoorStatus door, bool bButton, LevelChange change)
        {
            Description = strDesc;
            Solidity = WallSolidity.NonePass;
            if (bLight)
                Solidity |= WallSolidity.LightPass;
            if (bObject)
                Solidity |= WallSolidity.ObjectPass;
            if (bMonster)
                Solidity |= WallSolidity.MonsterPass;
            if (bParty)
                Solidity |= WallSolidity.PlayerPass;
            Map = iMap;
            Location = ptLocation;
            Facing = dir;
            DoorType = door;
            Transport = change;
        }

        public BasicWall Clone(string strDesc)
        {
            BasicWall w = new BasicWall(strDesc);
            w.Solidity = Solidity;
            w.Facing = Facing;
            w.Location = Location;
            w.Map = Map;
            w.DoorType = DoorType;
            w.Specials = Specials;
            w.Transport = Transport;
            w.Index = Index;
            return w;
        }

        public BasicWall(string strDesc, bool bLight, bool bObject, bool bMonster, bool bParty)
        {
            Set(strDesc, bLight, bObject, bMonster, bParty, -1, Global.NullPoint, DirectionFlags.None, DoorStatus.None, false, LevelChange.None);
        }

        public BasicWall(string strDesc) : this(strDesc, false, false) { }
        public BasicWall(string strDesc, bool bSee, bool bMove) : this(strDesc, bSee, bMove, bMove, bMove) { }
        public BasicWall(string strDesc, DoorStatus door, bool bSee, bool bMove) : this(strDesc, bSee, bMove) { DoorType = door; }
        public BasicWall(string strDesc, WallSpecials specials) : this(strDesc, false, false) { Specials = specials; }

        public BasicWall NewButton(string strDesc, bool bButton = true) => Clone(strDesc).AddButton(bButton);
        public BasicWall NewDoor(string strDesc, DoorStatus status) => Clone(strDesc).AddDoor(status);

        public BasicWall AddButton(bool bButton = true, bool bPressed = false)
        {
            if (bButton)
                Specials |= WallSpecials.Button;
            else
                Specials &= (~(WallSpecials.Button | WallSpecials.ButtonPressed));
            return this;
        }

        public BasicWall AddDoor(DoorStatus status)
        {
            DoorType |= status;
            return this;
        }

        public virtual bool IsOpen => Solidity == WallSolidity.AllPass;
        public virtual bool IsBarrier => Solidity == WallSolidity.LightPass;        // Only light passes: invisible barrier
        public virtual bool IsSolid => Solidity == WallSolidity.NonePass;           // Nothing can pass through: ordinary wall
        public virtual bool IsFalseWall => Solidity == WallSolidity.FalseWall;      // Only light doesn't pass: false wall
        public virtual bool IsMissileWall => Solidity == WallSolidity.ObjectPass;   // Only objects pass: missile wall
        public virtual bool IsDoor => DoorType != DoorStatus.None;
        public virtual bool IsButtonDoor => IsDoor && HasButton;
        public virtual bool HasButton => Specials.HasFlag(WallSpecials.Button | WallSpecials.ButtonPressed);
        public virtual bool IsForceable => DoorType.HasFlag(DoorStatus.Forceable);
        public virtual bool IsFloorHole => Transport.HasFlag(LevelChange.HoleDown);
    }

    public class Walls
    {
        public virtual BasicWall GetWall(int iMap, int iIndex) { return BasicWall.Open; }
    }
}
