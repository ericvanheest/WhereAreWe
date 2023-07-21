using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public class EOB1WallObjects
    {
        public BasicWall DoorClosed = new BasicWall("Door Closed", DoorStatus.Closed, true, true);   // Can't really see/move through a closed door, but it should look that way on a map
        public BasicWall Door1 = new BasicWall("Door 25%", DoorStatus.PartlyOpen, true, true);
        public BasicWall Door2 = new BasicWall("Door 50%", DoorStatus.PartlyOpen, true, true);
        public BasicWall Door3 = new BasicWall("Door 75%", DoorStatus.PartlyOpen, true, true);
        public BasicWall DoorOpen = new BasicWall("Door Open", DoorStatus.Open, true, true);

        public BasicWall ButtonDoorClosed, ButtonDoor1, ButtonDoor2, ButtonDoor3, ButtonDoorOpen;
        public BasicWall PortcullisClosed, Portcullis1, Portcullis2, Portcullis3, PortcullisOpen;
        public BasicWall ButtonPortcullisClosed, ButtonPortcullis1, ButtonPortcullis2, ButtonPortcullis3, ButtonPortcullisOpen;
        public BasicWall LadderUp, LadderDown, StairsUp, StairsDown, HoleUp, HoleDown;
        public BasicWall MonsterBarrier, TeleportField;
        public BasicWall PressurePlate, Alcove, Gargoyle, Rune, Writing, RatsArrow, DartHoles, FireballLauncher, Spider, Star, CrackedBrick, Manacles, Spikes;
        public BasicWall ForceableDoor, ForceablePortcullis, DoorFrameSide;
        public BasicWall Button, ButtonPressed, LargeButton, LargeButtonPressed, SmallButton, SmallButtonPressed, SpiderButton, SpiderButtonPressed;
        public BasicWall TinyButton, TinyButtonPressed, HiddenButton, HiddenButtonPressed, RedButton, RedButtonPressed, BlueButton, BlueButtonPressed;
        public BasicWall LeverUp, LeverDown, RopeUp, RopeDown, ChainUp, ChainDown, GargoyleLeverUp, GargoyleLeverDown;
        public BasicWall DaggerSocket, GemSocket, GemSocketFilled, EyesEmpty, EyesSingleGem, EyesTwoGems, EyesTwoGemsColored;
        public BasicWall KeyholeDecorative, KeyholeGargoyle, KeyholeMoss, KeyholeRectangle, KeyholeRound, KeyholeSpider;
        public BasicWall SewerPipe, SewerPipeEyes, SewerGrate, SewerGrateEyes;
        public BasicWall FalseWallRune, FalseWallSpider, CobwebHackable, CobwebHacked, CobwebWall, CobwebHackableWall;
        public BasicWall GatewayItemNone, GatewayItem1, GatewayItem2, GatewayItem3, GatewayItem4, GatewayItem5, GatewayItem6, GatewayItem7, GatewayItem8;
        public BasicWall MissileWall, MissileWriting, TorchUp, TorchSideways;

        public EOB1WallObjects()
        {
            PortcullisClosed = DoorClosed.NewDoor("Portcullis Closed", DoorStatus.Portcullis);
            Portcullis1 = Door1.NewDoor("Portcullis 25%", DoorStatus.Portcullis);
            Portcullis2 = Door2.NewDoor("Portcullis 50%", DoorStatus.Portcullis);
            Portcullis3 = Door3.NewDoor("Portcullis 75%", DoorStatus.Portcullis);
            PortcullisOpen = DoorOpen.NewDoor("Portcullis Open%", DoorStatus.Portcullis);
            ButtonDoorClosed = DoorClosed.NewButton("Button " + DoorClosed.Description);
            ButtonDoor1 = Door1.NewButton("Button " + Door1.Description);
            ButtonDoor2 = Door2.NewButton("Button " + Door2.Description);
            ButtonDoor3 = Door3.NewButton("Button " + Door3.Description);
            ButtonDoorOpen = DoorOpen.NewButton("Button " + DoorOpen.Description);
            ButtonPortcullisClosed = DoorClosed.NewDoor("Button " + PortcullisClosed.Description, DoorStatus.Portcullis);
            ButtonPortcullis1 = Door1.NewDoor("Button " + Portcullis1.Description, DoorStatus.Portcullis);
            ButtonPortcullis2 = Door2.NewDoor("Button " + Portcullis2.Description, DoorStatus.Portcullis);
            ButtonPortcullis3 = Door3.NewDoor("Button " + Portcullis3.Description, DoorStatus.Portcullis);
            ButtonPortcullisOpen = DoorOpen.NewDoor("Button " + PortcullisOpen.Description, DoorStatus.Portcullis);
            LadderUp = new BasicWall("Ladder Up", true, true);
            LadderDown = LadderUp.Clone("Ladder Down");
            LadderUp.Transport = LevelChange.Up;
            LadderDown.Transport = LevelChange.Down;
            StairsUp = LadderUp.Clone("Stairs Up");
            StairsDown = LadderDown.Clone("Stairs Down");
            MonsterBarrier = new BasicWall("Monster Barrier", true, true);
            MonsterBarrier.Solidity = WallSolidity.MonsterBarrier;
            HoleUp = new BasicWall("Ceiling Hole", true, true);
            HoleUp.Transport = LevelChange.HoleUp;
            HoleDown = new BasicWall("Floor Hole", true, true);
            HoleDown.Transport = LevelChange.HoleDown;
            PressurePlate = new BasicWall("Pressure Plate", true, true);
            PressurePlate.Specials = WallSpecials.FloorPlate;
            ForceableDoor = DoorClosed.NewDoor("Forceable Door", DoorStatus.Forceable);
            ForceablePortcullis = ForceableDoor.NewDoor("Forceable Portcullis", DoorStatus.Portcullis);
            Alcove = new BasicWall("Alcove", WallSpecials.Alcove);
            Button = new BasicWall("Button", WallSpecials.Button);
            ButtonPressed = new BasicWall("Button Pressed", WallSpecials.ButtonPressed);
            LargeButton = Button.Clone("Large Button");
            LargeButtonPressed = ButtonPressed.Clone("Large Button Pressed");
            SmallButton = Button.Clone("Small Button");
            SmallButtonPressed = ButtonPressed.Clone("Small Button Pressed");
            TinyButton = Button.Clone("Tiny Button");
            TinyButtonPressed = ButtonPressed.Clone("Tiny Button Pressed");
            HiddenButton = Button.Clone("Hidden Button");
            HiddenButtonPressed = ButtonPressed.Clone("Hidden Button Pressed");
            RedButton = Button.Clone("Red Button");
            RedButtonPressed = ButtonPressed.Clone("Red Button Pressed");
            BlueButton = Button.Clone("Blue Button");
            BlueButtonPressed = ButtonPressed.Clone("Blue Button Pressed");
            SpiderButton = Button.Clone("Spider Button");
            SpiderButtonPressed = ButtonPressed.Clone("Spider Button Pressed");
            Gargoyle = new BasicWall("Gargoyle", WallSpecials.Decoration);
            LeverUp = new BasicWall("Lever Up", WallSpecials.SwitchUp);
            LeverDown = new BasicWall("Lever Down", WallSpecials.SwitchDown);
            GargoyleLeverUp = new BasicWall("Gargoyle Lever Up", WallSpecials.SwitchUp);
            GargoyleLeverDown = new BasicWall("Gargoyle Lever Down", WallSpecials.SwitchDown);
            RopeUp = new BasicWall("Rope Up", WallSpecials.SwitchUp);
            RopeDown = new BasicWall("Rope Down", WallSpecials.SwitchDown);
            ChainUp = new BasicWall("Chain Up", WallSpecials.SwitchUp);
            ChainDown = new BasicWall("Chain Down", WallSpecials.SwitchDown);
            TorchUp = new BasicWall("Torch Up", WallSpecials.SwitchUp);
            TorchSideways = new BasicWall("Torch Turned", WallSpecials.SwitchDown);
            Rune = new BasicWall("Rune");
            Writing = new BasicWall("Writing");
            DaggerSocket = new BasicWall("Dagger Socket", WallSpecials.Socket);
            GemSocket = new BasicWall("Gem Socket", WallSpecials.Socket);
            GemSocketFilled = new BasicWall("Gem Socket Filled", WallSpecials.Socket);
            EyesEmpty = new BasicWall("Eyes Socket", WallSpecials.Socket);
            EyesSingleGem = new BasicWall("Eyes Socket One Gem", WallSpecials.Socket);
            EyesTwoGems = new BasicWall("Eyes Socket Two Gems", WallSpecials.SocketFilled);
            EyesTwoGemsColored = new BasicWall("Eyes Socket Two Purple", WallSpecials.SocketFilled);
            TeleportField = new BasicWall("Teleport Field");
            TeleportField.Transport = LevelChange.Teleport;
            TeleportField.Solidity = WallSolidity.AllPass;
            KeyholeDecorative = new BasicWall("Decorative Keyhole");
            KeyholeGargoyle = new BasicWall("Gargoyle Keyhole");
            KeyholeMoss = new BasicWall("Moss Keyhole");
            KeyholeRectangle = new BasicWall("Rectangle Keyhole");
            KeyholeRound = new BasicWall("Round Keyhole");
            KeyholeSpider = new BasicWall("Spider Keyhole");
            RatsArrow = new BasicWall("R.A.T.S. Arrow", WallSpecials.IndicatorRight);
            DoorFrameSide = new BasicWall("Doorframe Side");
            SewerPipe = new BasicWall("Sewer Pipe");
            SewerPipeEyes = new BasicWall("Sewer Pipe Eyes", WallSpecials.Eyes);
            SewerGrate = new BasicWall("Sewer Grate");
            SewerGrateEyes = new BasicWall("Sewer Grate Eyes", WallSpecials.Eyes);
            GatewayItemNone = new BasicWall("Gateway No Item");
            GatewayItemNone.Transport = LevelChange.Gateway;
            GatewayItem1 = GatewayItemNone.Clone("Gateway Item #1");
            GatewayItem2 = GatewayItemNone.Clone("Gateway Item #2");
            GatewayItem3 = GatewayItemNone.Clone("Gateway Item #3");
            GatewayItem4 = GatewayItemNone.Clone("Gateway Item #4");
            GatewayItem5 = GatewayItemNone.Clone("Gateway Item #5");
            GatewayItem6 = GatewayItemNone.Clone("Gateway Item #6");
            GatewayItem7 = GatewayItemNone.Clone("Gateway Item #7");
            GatewayItem8 = GatewayItemNone.Clone("Gateway Item #8");
            CobwebHackableWall = new BasicWall("Cobweb Brick Hackable", false, false);
            CobwebHackable = new BasicWall("Hackable Cobweb", true, false);
            CobwebHackable.Solidity |= WallSolidity.Destructible;
            CobwebHacked = CobwebHackable.Clone("Hacked Cobweb");
            CobwebHacked.Solidity = WallSolidity.AllPass;
            CobwebWall = new BasicWall("Cobweb Brick", WallSpecials.Decoration);
            Star = new BasicWall("Star", WallSpecials.Decoration);
            CrackedBrick = new BasicWall("Cracked Brick", WallSpecials.Decoration);
            Manacles = new BasicWall("Manacles", WallSpecials.Decoration);
            DartHoles = new BasicWall("Dart Holes", WallSpecials.Decoration);
            Spikes = new BasicWall("Bloody Spikes", true, true);
            FireballLauncher = new BasicWall("Fireball Launcher", WallSpecials.Decoration);
            Spider = new BasicWall("Spider", WallSpecials.Decoration);
            MissileWriting = Writing.Clone("Missile Writing");
            MissileWriting.Solidity = WallSolidity.ObjectPass;
            MissileWall = MissileWriting.Clone("Missile Wall");
            FalseWallRune = new BasicWall("False Rune Wall", false, true);
            FalseWallSpider = new BasicWall("False Spider Wall", false, true);
        }
    }

    public class EOB1Walls : EOBWalls
    {
        private EOB1WallObjects Walls = new EOB1WallObjects();

        private BasicWall Basic(int iMap, int iIndex)
        {
            switch (iIndex)
            {
                case -1: return BasicWall.Solid;
                case 0: return BasicWall.Open;
                case 1:
                case 2: return BasicWall.Solid;
                case 3: return Walls.ButtonDoorClosed;
                case 4: return Walls.ButtonDoor1;
                case 5: return Walls.ButtonDoor2;
                case 6: return Walls.ButtonDoor3;
                case 7: return Walls.ButtonDoorOpen;
                case 8: return Walls.DoorClosed;
                case 9: return Walls.Door1;
                case 10: return Walls.Door2;
                case 11: return Walls.Door3;
                case 12: return Walls.DoorOpen;
                case 13: return Walls.ButtonPortcullisClosed;
                case 14: return Walls.ButtonPortcullis1;
                case 15: return Walls.ButtonPortcullis2;
                case 16: return Walls.ButtonPortcullis3;
                case 17: return Walls.ButtonPortcullisOpen;
                case 18: return Walls.PortcullisClosed;
                case 19: return Walls.Portcullis1;
                case 20: return Walls.Portcullis2;
                case 21: return Walls.Portcullis3;
                case 22: return Walls.PortcullisOpen;
                case 23: return iMap < 4 ? Walls.LadderUp : Walls.StairsUp;
                case 24: return iMap < 4 ? Walls.LadderDown : Walls.StairsDown;
                case 25: return Walls.MonsterBarrier;
                case 26: return Walls.HoleUp;
                case 27: return Walls.HoleDown;
                case 28: return Walls.PressurePlate;
                case 29: return Walls.Alcove;
                case 30: return Walls.ForceableDoor;
                case 31: return Walls.ForceablePortcullis;
                case 32:
                case 33:
                case 34:
                case 35:
                case 36:
                case 37:
                case 38: return BasicWall.Barrier;   // 32-38 are typically specific to the map
                case 39: return Walls.LargeButton;
                case 40: return Walls.LargeButtonPressed;
                case 41: return BasicWall.Barrier;
                case 42: return Walls.LadderDown;
                case 43: return Walls.DaggerSocket;
                case 44: return BasicWall.Solid;
                case 45: return BasicWall.Barrier;
                case 46: return BasicWall.Barrier;
                case 47: return Walls.LargeButtonPressed;
                case 48: return BasicWall.Barrier;
                case 49: return BasicWall.Barrier;
                case 50: return Walls.SmallButton;
                case 51: return Walls.Writing;
                case 52: return Walls.TeleportField;
                case 53: return Walls.KeyholeRectangle;
                case 54: return Walls.KeyholeRound;
                case 55: return Walls.LeverUp;
                case 56: return Walls.LeverDown;
                case 57: return Walls.RatsArrow;
                case 58: return Walls.DoorFrameSide;
                case 59: return BasicWall.Barrier;
                case 60: return Walls.Button;
                case 61: return Walls.ButtonPressed;
                case 62: return Walls.SewerGrate;
                case 63: return Walls.SewerPipe;
                case 64: return BasicWall.FalseWall;
                case 65: return Walls.Rune;
                case 66: return Walls.DaggerSocket;
                case 67: return Walls.FalseWallRune;
                case 68: return Walls.GatewayItemNone;
                case 69: return BasicWall.Barrier;
                case 70: return BasicWall.Barrier;
                case 71: return BasicWall.Open;
                default: return BasicWall.Open;
            }
        }

        private BasicWall GargoyleBasic(int iMap, int iIndex)
        {
            switch (iIndex)
            {
                case 32: return Walls.GargoyleLeverUp;
                case 33: return Walls.GargoyleLeverDown;
                case 34: return Walls.Gargoyle;
                case 35: return Walls.CobwebWall;
                case 36: return Walls.ChainUp;
                case 37: return Walls.ChainDown;
                case 38: return Walls.DartHoles;
                case 39: return Walls.HiddenButton;
                case 40: return Walls.CobwebHackable;
                case 41: return Walls.CobwebHacked;
                case 42: return Walls.HoleUp;
                case 43: return Walls.GatewayItem2;
                case 44: return Walls.GargoyleLeverDown;
                case 45: return Walls.GatewayItem7;
                case 46: return Walls.GatewayItem5;
                case 47: return BasicWall.Solid;
                case 48: return Walls.GargoyleLeverDown;
                case 53: return Walls.KeyholeGargoyle;
                case 57:
                case 58:
                case 59:
                case 61: return Walls.CobwebWall;
                case 62: return Walls.LeverUp;
                case 63: return Walls.CobwebWall;
                case 65: return Walls.KeyholeGargoyle;
                case 66: return Walls.HiddenButton;
                case 67: return BasicWall.FalseWall;
                case 68: return Walls.GatewayItemNone;
                default: return Basic(iMap, iIndex);
            }
        }

        private BasicWall SpiderBasic(int iMap, int iIndex)
        {
            switch (iIndex)
            {
                case 32: return BasicWall.Solid;
                case 33: return BasicWall.Solid;
                case 34: return Walls.Spider;
                case 35: return Walls.Spider;
                case 36: return BasicWall.Solid;
                case 37: return BasicWall.Solid;
                case 38: return Walls.Spider;
                case 39: return Walls.HiddenButton;
                case 40: return Walls.GatewayItem1;
                case 41: return Walls.GatewayItem2;
                case 42: return Walls.Rune;
                case 43: return Walls.GatewayItem4;
                case 44: return Walls.GatewayItem5;
                case 45: return Walls.GatewayItem6;
                case 46: return Walls.GatewayItem8;
                case 47: return BasicWall.Solid;
                case 48: return Walls.FireballLauncher;
                case 49: return Walls.Spider;
                case 50: return BasicWall.Solid;
                case 53: return Walls.KeyholeSpider;
                case 54: return Walls.KeyholeSpider;
                case 57: return Walls.Spider;
                case 59: return Walls.SpiderButtonPressed;
                case 60: return Walls.SpiderButton;
                case 61: return Walls.SpiderButtonPressed;
                case 62: return BasicWall.Solid;
                case 63: return Walls.Spider;
                case 65: return Walls.Rune;
                case 66: return Walls.Spider;
                case 68: return Walls.GatewayItem5;
                default: return Basic(iMap, iIndex);
            }
        }

        private BasicWall LowerLevelBasic(int iMap, int iIndex)
        {
            switch (iIndex)
            {
                case 30: return BasicWall.Solid;
                case 31: return Walls.HiddenButton;
                case 32: return Walls.HiddenButtonPressed;
                case 33: return Walls.TinyButton;
                case 34: return Walls.TinyButtonPressed;
                case 35: return BasicWall.Barrier;
                case 36: return Walls.GatewayItem1;
                case 37: return Walls.GatewayItem3;
                case 38: return Walls.GatewayItem6;
                case 39: return Walls.GatewayItem7;
                case 40: return Walls.FireballLauncher;
                case 41: return Walls.Writing;
                case 42: return Walls.KeyholeMoss;
                case 43: return Walls.LeverUp;
                case 44: return Walls.LeverDown;
                case 45: return Walls.Button;
                case 46: return Walls.ButtonPressed;
                case 47: return BasicWall.Open;
                case 48: return Walls.Star;
                case 49: return Walls.CrackedBrick;
                case 50: return Walls.Manacles;
                case 51: return BasicWall.Solid;
                case 53: return BasicWall.Barrier;
                case 54: return BasicWall.Barrier;
                case 55: return Walls.Star;
                case 56: return BasicWall.Solid;
                case 57: return BasicWall.Barrier;
                case 60:
                case 61:
                case 62: return BasicWall.Solid;
                case 63: return Walls.HiddenButtonPressed;
                case 64: return BasicWall.Barrier;
                case 65: return BasicWall.Solid;
                case 66:
                case 67:
                case 68: return BasicWall.Barrier;
                default: return Basic(iMap, iIndex);
            }
        }

        public override BasicWall GetWall(int iMap, int iIndex)
        {
            BasicWall w = GetWallNoIndex(iMap, iIndex);
            w.Index = iIndex;
            return w;
        }

        public BasicWall GetWallNoIndex(int iMap, int iIndex)
        {
            switch (iMap)
            {
                case 1:
                case 2:
                    return Basic(iMap, iIndex);
                case 3:
                    switch (iIndex)
                    {
                        case 32: return Walls.EyesSingleGem;
                        case 33: return Walls.EyesTwoGems;
                        case 34: return Walls.EyesTwoGemsColored;
                        case 35: return Walls.GemSocket;
                        case 36: return Walls.GemSocketFilled;
                        case 41: return Walls.KeyholeRectangle;
                        case 42: return Walls.LadderUp;
                        case 43: return Walls.EyesTwoGemsColored;
                        case 44: return Walls.RedButton;
                        case 45: return Walls.BlueButton;
                        case 46: return Walls.BlueButtonPressed;
                        case 47: return Walls.KeyholeRectangle;
                        case 48: return Walls.RedButton;
                        case 49: return Walls.RedButtonPressed;
                        case 54: return Walls.EyesTwoGems;
                        case 62:
                        case 63: return BasicWall.Solid;
                        case 65: return Walls.LargeButtonPressed;
                        case 66: return Walls.EyesTwoGemsColored;
                        case 67: return BasicWall.FalseWall;
                        case 68: return Walls.GatewayItemNone;
                        default: return Basic(iMap, iIndex);
                    }
                case 4: return GargoyleBasic(iMap, iIndex);
                case 5:
                    switch (iIndex)
                    {
                        case 35: return Walls.GargoyleLeverUp;
                        case 36: return Walls.HiddenButton;
                        case 37: return Walls.HoleDown;
                        case 40: return Walls.HoleDown;
                        case 41: return BasicWall.Open;
                        case 42: return Walls.Rune;
                        case 44: return Walls.Rune;
                        case 48: return Walls.Rune;
                        case 57: return BasicWall.Solid;
                        case 59: return Walls.Alcove;
                        case 61: return Walls.Alcove;
                        case 62: return Walls.HoleUp;
                        case 63: return Walls.GargoyleLeverDown;
                        case 65: return Walls.Rune;
                        case 66: return BasicWall.FalseWall;
                        case 68: return Walls.GatewayItem2;
                        default: return GargoyleBasic(iMap, iIndex);
                    }
                case 6:
                    switch (iIndex)
                    {
                        case 35: return Walls.Gargoyle;
                        case 36: return Walls.CobwebHackableWall;
                        case 37: return Walls.CobwebWall;
                        case 42: return Walls.Rune;
                        case 44: return BasicWall.Solid;
                        case 48: return BasicWall.Solid;
                        case 59: return BasicWall.Barrier;
                        case 62: return Walls.HiddenButton;
                        case 63: return Walls.Gargoyle;
                        case 65: return Walls.Rune;
                        case 66: return BasicWall.FalseWall;
                        case 68: return Walls.GatewayItem5;
                        default: return GargoyleBasic(iMap, iIndex);
                    }
                case 7: return SpiderBasic(iMap, iIndex);
                case 8:
                    switch (iIndex)
                    {
                        case 32: return BasicWall.Barrier;
                        case 35: return BasicWall.Barrier;
                        case 36: return Walls.GemSocket;
                        case 37: return Walls.GemSocketFilled;
                        case 57: return Walls.FalseWallSpider;
                        case 59: return BasicWall.Barrier;
                        default: return SpiderBasic(iMap, iIndex);
                    }
                case 9:
                    switch (iIndex)
                    {
                        case 32: return BasicWall.Barrier;
                        case 33: return Walls.MissileWriting;
                        case 35: return BasicWall.Barrier;
                        case 36: return Walls.GemSocket;
                        case 37: return Walls.GemSocketFilled;
                        case 47: return Walls.MissileWall;
                        case 59: return BasicWall.Barrier;
                        default: return SpiderBasic(iMap, iIndex);
                    }
                case 10: return LowerLevelBasic(iMap, iIndex);
                case 11:
                    switch (iIndex)
                    {
                        case 50: return Walls.TinyButton;
                        case 56: return Walls.CrackedBrick;
                        default: return LowerLevelBasic(iMap, iIndex);
                    }
                case 12:
                    switch (iIndex)
                    {
                        case 30: return Walls.FireballLauncher;
                        case 31: return BasicWall.Solid;
                        case 32: return Walls.RopeUp;
                        case 33: return Walls.TinyButton;
                        case 34: return Walls.Button;
                        case 35: return Walls.RopeUp;
                        case 36: return Walls.RopeDown;
                        case 37: return Walls.GatewayItem3;
                        case 38: return Walls.TorchUp;
                        case 39: return Walls.Spikes;
                        case 40: return Walls.TorchSideways;
                        case 41: return Walls.Writing;
                        case 42: return Walls.KeyholeDecorative;
                        case 43: return Walls.TorchUp;
                        case 44: return Walls.TorchSideways;
                        case 45: return Walls.Button;
                        case 46: return Walls.ButtonPressed;
                        case 47:
                        case 48:
                        case 49: return BasicWall.Solid;
                        case 50: return Walls.Button;
                        case 51: return BasicWall.Solid;
                        case 53:
                        case 54: return BasicWall.Barrier;
                        case 55: return Walls.TinyButton;
                        case 56: return BasicWall.Solid;
                        case 57: return BasicWall.Barrier;
                        case 60:
                        case 61: return BasicWall.Solid;
                        case 62: return Walls.KeyholeDecorative;
                        case 63: return Walls.RopeDown;
                        case 64: return BasicWall.Barrier;
                        case 65: return Walls.ButtonPressed;
                        case 66:
                        case 67:
                        case 68: return BasicWall.Barrier;
                        default: return Basic(iMap, iIndex);
                    }

                default: return Basic(iMap, iIndex);
            }
        }
    }
}
