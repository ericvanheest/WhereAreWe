using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace WhereAreWe
{
    public class Wiz4Memory : WizMemory
    {
        // Search for "Wiz4.DSK is missing"
        public override byte[] MainSearch { get { return new byte[] { 0x57, 0x49, 0x5A, 0x34, 0x2E, 0x44, 0x53, 0x4B, 0x20, 0x69, 0x73, 0x20, 0x6D, 0x69, 0x73, 0x73, 0x69, 0x6E, 0x67 }; } }

        public override int MainBlockSVN { get { return -7514; } }
        public override int MainBlockOldSVN { get { return -7146; } }
        public override int MainBlockNonSVN { get { return -7514; } }
        public override int Facing { get { return 63396; } }         // Int16
        public override int LocationDown { get { return 63398; } }   // Int16
        public override int LocationNorth { get { return 63400; } }  // Int16
        public override int LocationEast { get { return 63402; } }   // Int16
        public override int PartyInfo { get { return 64168; } }
        public override int Map { get { return 51086; } }
        public override int Light { get { return 63456; } }
        public override int TimeDelay { get { return 63446; } }
        public override int State1 { get { return 19482; } }
        public override int State2 { get { return 81410; } }
        public override int NumChars { get { return 63394; } }       // Int16
        public override int CombatCharInfo { get { return 61214; } }
        public override int EncounterInfo { get { return 61442; } }
        public override int FightMap { get { return 64088; } }
        public override int ACBonus { get { return 63458; } }
        public override int Text { get { return 39230; } }

        // Incomplete:

        public override int AllMaps { get { return 204134; } }
        public override int MonsterListDisk { get { return 210278; } }
        public override int ItemList { get { return 180070; } }
        public override int CombatOptionActiveChar { get { return 39000; } }

        // Specific to Wiz4:

        public int EncounterTreasureList { get { return 53320; } }
        public int WerdnaCharRecord { get { return 65416; } }
        public int Group1Count { get { return 63512; } }
        public int Group2Count { get { return 63514; } }
        public int Group3Count { get { return 63516; } }
        public int Group1Index { get { return 63518; } }
        public int Group2Index { get { return 63520; } }
        public int Group3Index { get { return 63522; } }
        public int TreborNorth { get { return 67496; } }
        public int TreborEast { get { return 67498; } }
        public int BlackBox { get { return 67420; } } // 19 UInt16s
        public int SummonNumCreatures { get { return 51878; } }
        public int SummonMonsterBits { get { return 51892; } } // 15 bytes of bitflags
        public int OxygenMask { get { return 67474; } }
        public int Bloodstone { get { return 67460; } }
        public int BloodstoneLocation { get { return 67410; } }
        public int Turquoise { get { return 67462; } }
        public int TurquoiseLocation { get { return 67412; } }
        public int AmberDragon { get { return 67464; } }
        public int AmberDragonLocation { get { return 67414; } }
        public int CrystalRose { get { return 67490; } }
        public int ToHitBonus { get { return 63526; } }
        public int SpellPowerBonus { get { return 63524; } }
        public int WeaponPowerBonus { get { return 67494; } }
        public int MronNorth { get { return 51986; } }
        public int MronEast { get { return 51988; } }
        public int MronHintIndex { get { return 67488; } }
        public int DoGooders { get { return 67728; } }     // 64 bytes
        public int UsedHHG { get { return 67504; } }
        public int UsedBoots { get { return 67458; } }
        public int HellItems { get { return 52022; } }
        public int PinPulled { get { return 67468; } }  // 0 = not pulled, 65535 = pulled
        public int CurseLifted { get { return 67466; } }  // 1 = lifted, 0 = not
        public int FountainFixed { get { return 67484; } }  // 1/0

        public override MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.Wizardry4]; } }
    }

    public enum Wiz4Map
    {
        None = -1,
        Unknown0 = 0,
        CastleFirstFloor = 1,
        L1CosmicCube = 2,
        L2CosmicCube = 3,
        L3CosmicCube = 4,
        L4MazeOfWandering = 5,
        L5LandOfTheCreaturesOfLightAndDarkness = 6,
        L6RealmOfTheWhirlingDervish = 7,
        L7TempleOfTheDreampainter = 8,
        L8LandOf1000Cuts = 9,
        L9TheCatacombs = 10,
        L10PyramidOfEntrapment = 11,
        L11Grandmaster = 12,
        CastleSecondFloor = 13,
        CastleThirdFloor = 14,
        Last = 15,
        AltCastle = 255,

        Unknown = -1
    }

    public class Wiz4GameInfo : Wiz1234GameInfo
    {
        public Point Trebor;
        public Point Mron;
        public int MronHint;
        public bool OxygenMask;
        public bool Bloodstone;
        public int BloodstoneLoc;
        public bool Turquoise;
        public int TurquoiseLoc;
        public bool AmberDragon;
        public int AmberDragonLoc;
        public bool CrystalRose;
        public int ToHitBonus;
        public int SpellPowerBonus;
        public int WeaponPowerBonus;
        public byte[] DoGooders;
        public bool UsedHHG;
        public bool UsedBoots;
        public bool CurseLifted;
        public bool FountainFixed;
        public UInt16 PinPulled;
        public int HellItems;

        public override GameNames Game { get { return GameNames.Wizardry4; } }

        public override List<GameInfoItem> GetMapItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();

            return items;
        }

        public override byte[] QuestBytes
        {
            get
            {
                MemoryStream ms = new MemoryStream();
                Global.WriteInt16(ms, Light);
                Global.WriteInt16(ms, MronHint);
                Global.WriteBool(ms, Bloodstone);
                Global.WriteInt16(ms, BloodstoneLoc);
                Global.WriteBool(ms, Turquoise);
                Global.WriteInt16(ms, TurquoiseLoc);
                Global.WriteBool(ms, AmberDragon);
                Global.WriteInt16(ms, AmberDragonLoc);
                Global.WriteInt16(ms, HellItems);
                Global.WriteUInt16(ms, PinPulled);
                Global.WriteBool(ms, CrystalRose);
                Global.WriteBool(ms, UsedHHG);
                Global.WriteBool(ms, UsedBoots);
                Global.WriteBool(ms, CurseLifted);
                Global.WriteBool(ms, FountainFixed);
                Global.WriteBytes(ms, DoGooders);
                return ms.ToArray();
            }
        }

        public override List<GameInfoItem> GetEffectItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();
            items.Add(new Wiz4GameInfoItem("Light", (Int16)Light, Wiz4.Memory.Light));
            //items.Add(new Wiz4GameInfoItem("Identify", (Int16)Identify, Wiz4.Memory.Identify));
            items.Add(new Wiz4GameInfoItem("AC Bonus", (Int16)ACBonus, Wiz4.Memory.ACBonus));
            items.Add(new Wiz4GameInfoItem("To-Hit Bonus", (Int16)ToHitBonus, Wiz4.Memory.ToHitBonus));
            items.Add(new Wiz4GameInfoItem("Spell Bonus", (Int16)SpellPowerBonus, Wiz4.Memory.SpellPowerBonus));
            items.Add(new Wiz4GameInfoItem("Weapon Bonus", (Int16)WeaponPowerBonus, Wiz4.Memory.WeaponPowerBonus));
            items.Add(new Wiz4GameInfoItem("Invoked Oxygen Mask", OxygenMask, Wiz4.Memory.OxygenMask));
            items.Add(new Wiz4GameInfoItem("Invoked Winged Boots", UsedBoots, Wiz4.Memory.UsedBoots));
            items.Add(new Wiz4GameInfoItem("Invoked Bloodstone", Bloodstone, Wiz4.Memory.Bloodstone));
            items.Add(new Wiz4GameInfoItem("Invoked Turquoise", Turquoise, Wiz4.Memory.Turquoise));
            items.Add(new Wiz4GameInfoItem("Invoked Amber Dr.", AmberDragon, Wiz4.Memory.AmberDragon));
            items.Add(new Wiz4GameInfoItem("Invoked Cr. Rose", CrystalRose, Wiz4.Memory.CrystalRose));
            items.Add(new Wiz4GameInfoItem("Trebor Uncursed", CurseLifted, Wiz4.Memory.CurseLifted));
            items.Add(new Wiz4GameInfoItem("Fountain Fixed", FountainFixed, Wiz4.Memory.FountainFixed));
            if (Global.Debug)
            {
                items.Add(new Wiz4GameInfoItem("Bloodstone Loc", (Int16)BloodstoneLoc, Wiz4.Memory.BloodstoneLocation));
                items.Add(new Wiz4GameInfoItem("Turquoise Loc", (Int16)TurquoiseLoc, Wiz4.Memory.TurquoiseLocation));
                items.Add(new Wiz4GameInfoItem("AmberDragon Loc", (Int16)AmberDragonLoc, Wiz4.Memory.AmberDragonLocation));
            }
            return items;
        }

        public override List<GameInfoItem> GetMiscItems()
        {
            int mapOffset = Wiz4.Memory.Map;
            List<GameInfoItem> items = new List<GameInfoItem>();

            WizMapData map = new WizMapData(Game, Location.MapIndex, Bytes, 0, true);
            if (map == null)
                return items;

            items.Add(new Wiz4GameInfoItem("Map Index", (Int16) Location.MapIndex, -1));
            items.Add(new Wiz4GameInfoItem("Trebor X", (Int16) Trebor.X, Wiz4.Memory.TreborEast));
            items.Add(new Wiz4GameInfoItem("Trebor Y", (Int16) Trebor.Y, Wiz4.Memory.TreborNorth));
            items.Add(new Wiz4GameInfoItem("Mron X", (Int16) Mron.X, Wiz4.Memory.MronEast));
            items.Add(new Wiz4GameInfoItem("Mron Y", (Int16) Mron.Y, Wiz4.Memory.MronNorth));
            items.Add(new Wiz4GameInfoItem("Mron Hint", (Int16)MronHint, Wiz4.Memory.MronHintIndex));
            items.Add(new Wiz4GameInfoItem("Bits: Do-Gooders", DoGooders, new OffsetList(Wiz4.Memory.DoGooders), Wiz4GameInfo.DoGooderDesc));
            items.Add(new Wiz4GameInfoItem("Delay", (Int16)TimeDelay, Wiz4.Memory.TimeDelay));
            if (Global.Debug)
            {
                items.Add(new Wiz4GameInfoItem("Used Grenade", UsedHHG, Wiz4.Memory.UsedHHG));
                items.Add(new Wiz4GameInfoItem("Hell Items", (Int16)HellItems, Wiz4.Memory.HellItems));
                items.Add(new Wiz4GameInfoItem("Pin Pulled", (UInt16)PinPulled, Wiz4.Memory.PinPulled));
            }
            return items;
        }

        public static BitDesc DoGooderDesc(object val)
        {
            int bit = (int)val;
            if (bit < 512)
            {
                string strChar = DoGooder(bit);
                if (!String.IsNullOrWhiteSpace(strChar))
                {
                    BitDesc desc = new BitDesc(String.Format("Defeat \"{0}\"", strChar));
                    desc.Cleared.Add(new BitDesc.WWW(BitDesc.WWW.Action.Clear, String.Format("Leave level {0}", DoGooderLevel(bit)), MapXY.Empty));
                    return desc;
                }
            }
            return BitDesc.Empty;
        }

        public static string DoGooder(int bit)
        {
            switch (bit)
            {
                case 0: return "Thorin's Tramplers: Stomp 'Em Boys!";
                case 1: return "Sorriman's Sorcerers: Bubble, Bubble, Toil and Cuddle!!!";
                case 2: return "Arcturus's Avengers: Murderer, You Shall Pay!";
                case 3: return "Abduul's Artful Dodgers: Die Infidels!";
                case 4: return "Greyhawk's Ghostbusters: Bell Werdna!";
                case 5: return "Talon's Tigers: R O A R ! ! !";
                case 6: return "Jiri's Jaguars: Hack, Slash, Fun!";
                case 7: return "Rendor's Roughnecks: Charge!!!";
                case 8: return "Blackthorn's Blackguards: Take No Prisoners!";
                case 9: return "The Company: Mordor or Bust!";
                case 10: return "Khan's Kosmic Killers: C'mon, Do You Want to Live Forever?";
                case 11: return "Dorion's Greys: Well, Curse Your Soul!";
                case 12: return "Horin's Holy Rollers: for God and St. Trebor!";
                case 13: return "Raiden's Raiders: Spread Out Men!";
                case 14: return "Gomez's Gorillas: Ugga Bugga! Ugga Bugga!";
                case 15: return "Myriad's Marauders: You Are Burgerbits, Fellow!";
                case 16: return "GROWLER";
                case 17: return "BONB";
                case 18: return "LYANNA";
                case 19: return "FEARLESS FRED";
                case 20: return "Applet's Angels: Ring the Bell, Read the Book, Light the Candle!";
                case 21: return "Loktar's Lucky Laddies: Erin Go Braugh!";
                case 22: return "Elindull's Evil Elites: Chuckle! Chuckle!";
                case 23: return "Joachim's Jihad: for the Love of Allah!";
                case 24: return "WERTY";
                case 25: return "NIGHT-WALKER";
                case 26: return "ANIMOTION";
                case 27: return "RICATAMAK";
                case 28: return "MANDORALLEN";
                case 29: return "BLUE SONJA";
                case 30: return "IRONBAR";
                case 31: return "BELGARION";
                case 32: return "DARKFORCE";
                case 33: return "CAPSIN";
                case 34: return "RUMPEL";
                case 35: return "BALORSK";
                case 36: return "GALVIN";
                case 37: return "PIG-I-IGGY";
                case 38: return "CAULDRON BORN";
                case 39: return "MIGHTY MOH";
                case 40: return "ICE FIGHTER";
                case 41: return "JETSTREAM";
                case 42: return "LUKE APPLE";
                case 43: return "TYRON";
                case 44: return "RANDALF";
                case 45: return "FULL-STRIKE";
                case 46: return "FALSTAFF";
                case 47: return "THONOLAN";
                case 48: return "BOREHIMHERE";
                case 49: return "TRADER";
                case 50: return "BONES";
                case 51: return "OZZY";
                case 52: return "KYOKO";
                case 53: return "RASTLIN";
                case 54: return "PERSIA";
                case 55: return "NAKON";
                case 56: return "MERLIN";
                case 57: return "DREADNOK";
                case 58: return "DRIP";
                case 59: return "LYNSING";
                case 60: return "BRIGHTBLADE";
                case 61: return "ALANNON";
                case 62: return "LEPER";

                case 64: return "FANK";
                case 65: return "DAIBALO";
                case 66: return "SPELL-WEAVER";
                case 67: return "BUGNEWS";
                case 68: return "THALESSA";
                case 69: return "TARS TARKAS";
                case 70: return "KAZRAK";
                case 71: return "BANANARAMA";
                case 72: return "GYTR";
                case 73: return "LORD GWYDION";
                case 74: return "ASPERGIL";
                case 75: return "ZACHERI";
                case 76: return "GAELEN";
                case 77: return "OGER";
                case 78: return "MEMOLE";
                case 79: return "XENIC";
                case 80: return "FLINT";
                case 81: return "TOEN";
                case 82: return "CUTTER";
                case 83: return "LINGIN LORD";
                case 84: return "BONIS";
                case 85: return "TAZ";
                case 86: return "LAENGER";
                case 87: return "EXODOR";
                case 88: return "WEBBIRAN";
                case 89: return "VEE DUB";
                case 90: return "KILLER";
                case 91: return "ILURE";
                case 92: return "BANKIS";
                case 93: return "SAKURA";
                case 94: return "AC/DC";
                case 95: return "ELECTRO";
                case 96: return "ASCII";
                case 97: return "CADIDELHOP";
                case 98: return "WINDER";
                case 99: return "ARIAL";
                case 100: return "WARTY";
                case 101: return "TELE-VIPERS";
                case 102: return "VOLTAR";
                case 103: return "LANCE";
                case 104: return "BOZ";
                case 105: return "THARAGORN";
                case 106: return "CHIQUITA";
                case 107: return "ELRIK";
                case 108: return "ZYXXUS";
                case 109: return "WACKER";
                case 110: return "RAVEN";
                case 111: return "FEARLESS FARLEY";
                case 112: return "ARMANDO";
                case 113: return "GOR-Y";
                case 114: return "CHICO";
                case 115: return "BLACKSTONE";
                case 116: return "XAVIER";
                case 117: return "PEDRO";
                case 118: return "LITTLE CONAN";
                case 119: return "TILTOWAIT";
                case 120: return "SALEG";
                case 121: return "INTERFACE";
                case 122: return "LOGIN";
                case 123: return "STILGAR";
                case 124: return "SULTAN";
                case 125: return "ZANDOR";
                case 126: return "NAYLON";
                case 127: return "MARLIN";
                case 128: return "AIRIAN";
                case 129: return "DAJA";
                case 130: return "AURELIA";
                case 131: return "MAGE MARIAN";
                case 132: return "STURM";
                case 133: return "SAMSON";
                case 134: return "QUILEN";
                case 135: return "FINGERS";
                case 136: return "BRUD";
                case 137: return "DARWIN";
                case 138: return "TORAN";
                case 139: return "QUIL";
                case 140: return "CHRYSEIS";
                case 141: return "TELIMA";
                case 142: return "MOLYX";
                case 143: return "WINCEN";
                case 144: return "URBO";
                case 145: return "DEMONSLAYER";
                case 146: return "STALKER";
                case 147: return "SHANDRA";
                case 148: return "TRUENO";
                case 149: return "YURON";
                case 150: return "CELICA";
                case 151: return "STARLETO";
                case 152: return "FIRESLINGER";
                case 153: return "DR X";
                case 154: return "DOUBLE-STRIKE";
                case 155: return "TORANAGA";
                case 156: return "NARKNEEIA";
                case 157: return "DEADLY-HAND";
                case 158: return "SWIFT-ONE";
                case 159: return "KOTRAN";
                case 160: return "ZMAN";
                case 161: return "LOCO";
                case 162: return "FRODOUGH";
                case 163: return "SLY";
                case 164: return "BILBOUS BAGGINS";
                case 165: return "SINBAR";
                case 166: return "QUIMBY";
                case 167: return "PEPE LA PHEW";
                case 168: return "DRY GIMILI";
                case 169: return "ASGARD";
                case 170: return "EOWYN";
                case 171: return "SWORDWALKER";
                case 172: return "URAK";
                case 173: return "HUNTER";
                case 174: return "STERLING";
                case 175: return "CHAUNCY";
                case 176: return "YETY";
                case 177: return "MUTHA";
                case 178: return "DARTY";
                case 179: return "TEUT WEIDEMANN";
                case 180: return "URU";
                case 181: return "CROW";
                case 182: return "DESERT HAWK";
                case 183: return "MANTA";
                case 184: return "MOWRAN";
                case 185: return "BLACKTOOTH";
                case 186: return "STORMER";
                case 187: return "NIGHTHAWK";
                case 188: return "ELITE";
                case 189: return "RANDAL";
                case 190: return "ORGO";
                case 191: return "JAXOM";
                case 192: return "ILLUVATAR";
                case 193: return "OTHUS";
                case 194: return "CONAN COFFEE";
                case 195: return "MAD DOG";
                case 196: return "THE BROS.KOHLEN";
                case 197: return "BYTOR";
                case 198: return "LANCELOT";
                case 199: return "TOSHWA";
                case 200: return "RAIST";
                case 201: return "LORD DONSDAY";
                case 202: return "SLIK";
                case 203: return "YURI";
                case 204: return "COLON";
                case 205: return "STOMACH";
                case 206: return "GORAUKAR";
                case 207: return "PHILTHY";
                case 208: return "GULTHALION";
                case 209: return "FERMAN";
                case 210: return "SPELLS=WISHES";
                case 211: return "LA SPELLS";
                case 212: return "SHADOW";
                case 213: return "SCIOS";
                case 214: return "ELIZAR";
                case 215: return "CLALIS";
                case 216: return "MAGISTER";
                case 217: return "SHADES";
                case 218: return "DALLBEN";
                case 219: return "ELDOSABERRY";
                case 220: return "STRONG MAN";
                case 221: return "BOMART";
                case 222: return "SWORDS MAN";
                case 223: return "ARMANIR";
                case 224: return "EXCALIBUR";
                case 225: return "ALPINE";
                case 226: return "TREVOR LOCKLEER";
                case 227: return "MASTER CASTER";
                case 228: return "SPELL BOUND";
                case 229: return "SUNBURST";
                case 230: return "FIRESTORM";
                case 231: return "BINK";
                case 232: return "BLACK LANTERN";
                case 233: return "MJOLNIR";
                case 234: return "MURATOMO";
                case 235: return "ANDOMUS";
                case 236: return "ANEAS";
                case 237: return "FACE OF DEATH";
                case 238: return "TITAN";
                case 239: return "MORGIN";

                case 296: return "SQUANTHEAD";
                case 297: return "BLOODMETAL";
                case 298: return "GUILDENSTERN";
                case 299: return "KANE";

                case 304: return "MEPHISTO";
                case 305: return "GRAMORE";
                case 306: return "GRAVE EATER";
                case 307: return "DEAD EYE";
                case 308: return "CROM";
                case 309: return "SHADOWSTALKER";
                case 310: return "BRUTUS";
                case 311: return "YOREL";
                case 312: return "ZIRKONIAN";
                case 313: return "HAWK MAN";
                case 314: return "JOMER";
                case 315: return "VENASUS";
                case 316: return "ASPLUND";
                case 317: return "BIMBO";
                case 318: return "BLACK KNIGHT";
                case 319: return "TRATH";
                case 320: return "STRIDENT";
                case 321: return "GROTE";
                case 322: return "SPROCKET";
                case 323: return "DARJON'";
                case 324: return "THOR II";
                case 325: return "THUNDERWALKER";
                case 326: return "QUWERT";
                case 327: return "ZAPP";
                case 328: return "VENIM";
                case 329: return "LOKI";
                case 330: return "OMER";
                case 331: return "KHANICAL";
                case 332: return "BROADSWORD";
                case 333: return "CRYSANIA";
                case 334: return "FORTEK";
                case 335: return "NUBS";
                case 336: return "BLADE";
                case 337: return "KEFER";
                case 338: return "RAH";
                case 339: return "AUGGIE";
                case 340: return "SUBATI";
                case 341: return "DALAMAR";
                case 342: return "DIGGERO";
                case 343: return "ODIN";

                case 346: return "KAZY DAIN";
                case 347: return "NERMAL";
                case 348: return "LESSA";
                case 349: return "RAISTLIN";
                case 350: return "ARES";
                case 351: return "ISIS";

                default: return String.Empty;
            }
        }

        public static int[] DoGoodersOnLevel(int level)
        {
            // Some characters are on multiple levels (particularly levels 1-3)
            switch (level)
            {
                case 1: return new int[] { 20, 21, 339, 350, 223, 316, 317, 318, 336, 297, 332, 310, 308, 333, 341, 323, 307, 342, 334, 206, 305, 306,
                    321, 298, 208, 313, 351, 314, 299, 346, 337, 331, 348, 329, 304, 347, 335, 343, 330, 207, 326, 338, 349, 309, 322, 296, 320, 340,
                    324, 325, 319, 315, 328, 311, 327, 312 };
                case 2: return new int[] { 22, 23, 225, 235, 236, 223, 231, 232, 221, 215, 204, 218, 178, 219, 214, 224, 237, 209, 230, 206, 208, 195, 216, 227, 233, 239, 234, 211, 
                    201, 207, 200, 213, 217, 212, 202, 228, 210, 205, 220, 229, 222, 179, 196, 238, 226, 203 };
                case 3: return new int[] { 8, 9, 169, 164, 185, 197, 175, 194, 181, 178, 182, 168, 188, 170, 162, 173, 192, 191, 198, 161, 195, 183, 184, 177, 187, 190,
                    193, 167, 166, 189, 165, 163, 174, 186, 171, 179, 196, 199, 172, 180, 176, 160 };
                case 4: return new int[] { 10, 11, 136, 150, 137, 157, 145, 154, 153, 152, 159, 156, 139, 147, 146, 151, 158, 138, 155, 148, 144, 149 };
                case 5: return new int[] { 12, 13, 128, 130, 140, 129, 135, 121, 122, 131, 127, 142, 126, 134, 120, 133, 123, 132, 124, 141, 143, 125 };
                case 6: return new int[] { 14, 15, 99, 112, 96, 115, 104, 97, 114, 106, 107, 111, 113, 118, 117, 110, 105, 119, 109, 98, 116, 108 };
                case 7: return new int[] { 0, 1, 94, 92, 84, 82, 95, 87, 80, 91, 90, 86, 103, 83, 93, 85, 101, 81, 89, 102, 100 };
                case 8: return new int[] { 2, 3, 74, 71, 67, 65, 58, 57, 64, 76, 72, 70, 73, 59, 78, 56, 77, 66, 69, 68, 88, 79, 75 };
                case 9: return new int[] { 4, 5, 61, 50, 48, 60, 46, 45, 40, 41, 52, 62, 42, 55, 51, 54, 44, 53, 47, 49, 43 };
                case 10: return new int[] { 6, 7, 26, 35, 31, 29, 17, 33, 38, 32, 19, 36, 16, 30, 18, 28, 39, 25, 37, 27, 34, 24 };
                default: return new int[0];
            }
        }

        public static int DoGooderLevel(int bit)
        {
            switch (bit)
            {
                    // Parties
                case 0: return 7;   // Thorin's Tramplers: Stomp 'Em Boys!"
                case 1: return 7;   // Sorriman's Sorcerers: Bubble, Bubble, Toil and Cuddle!!!"
                case 2: return 8;   // Arcturus's Avengers: Murderer, You Shall Pay!"
                case 3: return 8;   // Abduul's Artful Dodgers: Die Infidels!"
                case 4: return 9;   // Greyhawk's Ghostbusters: Bell Werdna!"
                case 5: return 9;   // Talon's Tigers: R O A R ! ! !"
                case 6: return 10;  // Jiri's Jaguars: Hack, Slash, Fun!"
                case 7: return 10;  // Rendor's Roughnecks: Charge!!!"
                case 8: return 3;   // Blackthorn's Blackguards: Take No Prisoners!"
                case 9: return 3;   // The Company: Mordor or Bust!"
                case 10: return 4;  // Khan's Kosmic Killers: C'mon, Do You Want to Live Forever?"
                case 11: return 4;  // Dorion's Greys: Well, Curse Your Soul!"
                case 12: return 5;  // Horin's Holy Rollers: for God and St. Trebor!"
                case 13: return 5;  // Raiden's Raiders: Spread Out Men!"
                case 14: return 6;  // Gomez's Gorillas: Ugga Bugga! Ugga Bugga!"
                case 15: return 6;  // Myriad's Marauders: You Are Burgerbits, Fellow!"
                case 20: return 1;  // Applet's Angels: Ring the Bell, Read the Book, Light the Candle!"
                case 21: return 1;  // Loktar's Lucky Laddies: Erin Go Braugh!"
                case 22: return 2;  // Elindull's Evil Elites: Chuckle! Chuckle!"
                case 23: return 2;  // Joachim's Jihad: for the Love of Allah!"

                    // Individuals
                case 339:    // AUGGIE
                case 350:    // ARES
                case 316:    // ASPLUND
                case 317:    // BIMBO
                case 318:    // BLACK KNIGHT
                case 336:    // BLADE
                case 297:    // BLOODMETAL
                case 332:    // BROADSWORD
                case 310:    // BRUTUS
                case 308:    // CROM
                case 333:    // CRYSANIA
                case 341:    // DALAMAR
                case 323:    // DARJON'
                case 307:    // DEAD EYE
                case 342:    // DIGGERO
                case 334:    // FORTEK
                case 206:    // GORAUKAR
                case 305:    // GRAMORE
                case 306:    // GRAVE EATER
                case 321:    // GROTE
                case 298:    // GUILDENSTERN
                case 208:    // GULTHALION
                case 313:    // HAWK MAN
                case 351:    // ISIS
                case 314:    // JOMER
                case 299:    // KANE
                case 346:    // KAZY DAIN
                case 337:    // KEFER
                case 331:    // KHANICAL
                case 348:    // LESSA
                case 329:    // LOKI
                case 304:    // MEPHISTO
                case 347:    // NERMAL
                case 335:    // NUBS
                case 343:    // ODIN
                case 330:    // OMER
                case 207:    // PHILTHY
                case 326:    // QUWERT
                case 338:    // RAH
                case 349:    // RAISTLIN
                case 309:    // SHADOWSTALKER
                case 322:    // SPROCKET
                case 296:    // SQUANTHEAD
                case 320:    // STRIDENT
                case 340:    // SUBATI
                case 324:    // THOR II
                case 325:    // THUNDERWALKER
                case 319:    // TRATH
                case 315:    // VENASUS
                case 328:    // VENIM
                case 311:    // YOREL
                case 327:    // ZAPP
                case 312: return 01;   // ZIRKONIAN
                case 225:    // ALPINE
                case 235:    // ANDOMUS
                case 236:    // ANEAS
                case 223:    // ARMANIR
                case 231:    // BINK
                case 232:    // BLACK LANTERN
                case 221:    // BOMART
                case 215:    // CLALIS
                case 204:    // COLON
                case 218:    // DALLBEN
                case 178:    // DARTY
                case 219:    // ELDOSABERRY
                case 214:    // ELIZAR
                case 224:    // EXCALIBUR
                case 237:    // FACE OF DEATH
                case 209:    // FERMAN
                case 230:    // FIRESTORM
                case 195:    // MAD DOG
                case 216:    // MAGISTER
                case 227:    // MASTER CASTER
                case 233:    // MJOLNIR
                case 239:    // MORGIN
                case 234:    // MURATOMO
                case 211:    // LA SPELLS
                case 201:    // LORD DONSDAY
                case 200:    // RAIST
                case 213:    // SCIOS
                case 217:    // SHADES
                case 212:    // SHADOW
                case 202:    // SLIK
                case 228:    // SPELL BOUND
                case 210:    // SPELLS=WISHES
                case 205:    // STOMACH
                case 220:    // STRONG MAN
                case 229:    // SUNBURST
                case 222:    // SWORDS MAN
                case 179:    // TEUT WEIDEMANN
                case 196:    // THE BROS.KOHLEN
                case 238:    // TITAN
                case 226:    // TREVOR LOCKLEER
                case 203: return 02;   // YURI
                case 169:    // ASGARD
                case 164:    // BILBOUS BAGGINS
                case 185:    // BLACKTOOTH
                case 197:    // BYTOR
                case 175:    // CHAUNCY
                case 194:    // CONAN COFFEE
                case 181:    // CROW
                case 182:    // DESERT HAWK
                case 168:    // DRY GIMILI
                case 188:    // ELITE
                case 170:    // EOWYN
                case 162:    // FRODOUGH
                case 173:    // HUNTER
                case 192:    // ILLUVATAR
                case 191:    // JAXOM
                case 198:    // LANCELOT
                case 161:    // LOCO
                case 183:    // MANTA
                case 184:    // MOWRAN
                case 177:    // MUTHA
                case 187:    // NIGHTHAWK
                case 190:    // ORGO
                case 193:    // OTHUS
                case 167:    // PEPE LA PHEW
                case 166:    // QUIMBY
                case 189:    // RANDAL
                case 165:    // SINBAR
                case 163:    // SLY
                case 174:    // STERLING
                case 186:    // STORMER
                case 171:    // SWORDWALKER
                case 199:    // TOSHWA
                case 172:    // URAK
                case 180:    // URU
                case 176:    // YETY
                case 160: return 03;   // ZMAN
                case 136:    // BRUD
                case 150:    // CELICA
                case 137:    // DARWIN
                case 157:    // DEADLY-HAND
                case 145:    // DEMONSLAYER
                case 154:    // DOUBLE-STRIKE
                case 153:    // DR X
                case 152:    // FIRESLINGER
                case 159:    // KOTRAN
                case 156:    // NARKNEEIA
                case 139:    // QUIL
                case 147:    // SHANDRA
                case 146:    // STALKER
                case 151:    // STARLETO
                case 158:    // SWIFT-ONE
                case 138:    // TORAN
                case 155:    // TORANAGA
                case 148:    // TRUENO
                case 144:    // URBO
                case 149: return 04;   // YURON
                case 128:    // AIRIAN
                case 130:    // AURELIA
                case 140:    // CHRYSEIS
                case 129:    // DAJA
                case 135:    // FINGERS
                case 121:    // INTERFACE
                case 122:    // LOGIN
                case 131:    // MAGE MARIAN
                case 127:    // MARLIN
                case 142:    // MOLYX
                case 126:    // NAYLON
                case 134:    // QUILEN
                case 120:    // SALEG
                case 133:    // SAMSON
                case 123:    // STILGAR
                case 132:    // STURM
                case 124:    // SULTAN
                case 141:    // TELIMA
                case 143:    // WINCEN
                case 125: return 05;   // ZANDOR
                case 099:    // ARIAL
                case 112:    // ARMANDO
                case 096:    // ASCII
                case 115:    // BLACKSTONE
                case 104:    // BOZ
                case 097:    // CADIDELHOP
                case 114:    // CHICO
                case 106:    // CHIQUITA
                case 107:    // ELRIK
                case 111:    // FEARLESS FARLEY
                case 113:    // GOR-Y
                case 118:    // LITTLE CONAN
                case 117:    // PEDRO
                case 110:    // RAVEN
                case 105:    // THARAGORN
                case 119:    // TILTOWAIT
                case 109:    // WACKER
                case 098:    // WINDER
                case 116:    // XAVIER
                case 108: return 06;   // ZYXXUS
                case 094:    // AC/DC
                case 092:    // BANKIS
                case 084:    // BONIS
                case 082:    // CUTTER
                case 095:    // ELECTRO
                case 087:    // EXODOR
                case 080:    // FLINT
                case 091:    // ILURE
                case 090:    // KILLER
                case 086:    // LAENGER
                case 103:    // LANCE
                case 083:    // LINGIN LORD
                case 093:    // SAKURA
                case 085:    // TAZ
                case 101:    // TELE-VIPERS
                case 081:    // TOEN
                case 089:    // VEE DUB
                case 102:    // VOLTAR
                case 100: return 07;   // WARTY
                case 074:    // ASPERGIL
                case 071:    // BANANARAMA
                case 067:    // BUGNEWS
                case 065:    // DAIBALO
                case 058:    // DRIP
                case 057:    // DREADNOK
                case 064:    // FANK
                case 076:    // GAELEN
                case 072:    // GYTR
                case 070:    // KAZRAK
                case 073:    // LORD GWYDION
                case 059:    // LYNSING
                case 078:    // MEMOLE
                case 056:    // MERLIN
                case 077:    // OGER
                case 066:    // SPELL-WEAVER
                case 069:    // TARS TARKAS
                case 068:    // THALESSA
                case 088:    // WEBBIRAN
                case 079:    // XENIC
                case 075: return 08;   // ZACHERI
                case 061:    // ALANNON
                case 050:    // BONES
                case 048:    // BOREHIMHERE
                case 060:    // BRIGHTBLADE
                case 046:    // FALSTAFF
                case 045:    // FULL-STRIKE
                case 040:    // ICE FIGHTER
                case 041:    // JETSTREAM
                case 052:    // KYOKO
                case 062:    // LEPER
                case 042:    // LUKE APPLE
                case 055:    // NAKON
                case 051:    // OZZY
                case 054:    // PERSIA
                case 044:    // RANDALF
                case 053:    // RASTLIN
                case 047:    // THONOLAN
                case 049:    // TRADER
                case 043: return 09;   // TYRON
                case 026:    // ANIMOTION
                case 035:    // BALORSK
                case 031:    // BELGARION
                case 029:    // BLUE SONJA
                case 017:    // BONB
                case 033:    // CAPSIN
                case 038:    // CAULDRON BORN
                case 032:    // DARKFORCE
                case 019:    // FEARLESS FRED
                case 036:    // GALVIN
                case 016:    // GROWLER
                case 030:    // IRONBAR
                case 018:    // LYANNA
                case 028:    // MANDORALLEN
                case 039:    // MIGHTY MOH
                case 025:    // NIGHT-WALKER
                case 037:    // PIG-I-IGGY
                case 027:    // RICATAMAK
                case 034:    // RUMPEL
                case 024: return 10;   // WERTY
                default: return -1;
            }
        }
    }

    public class Wiz4GameInfoItem : WizGameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return Wiz4MemoryHacker.GetMapTitlePair(iMap); }

        public Wiz4GameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offsets, type, mask, style, fn)
        {
        }

        public Wiz4GameInfoItem(string desc, object val, int offset, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, new OffsetList(offset), type, mask, style, fn)
        {
        }

        public Wiz4GameInfoItem(string desc, object val, OffsetList offsets, BitDescriptionDelegate fn)
            : base(desc, val, offsets, DataType.Bits, 0, ShowStyle.Editable, fn)
        {
        }
    }

    public class Wiz4ActiveSquares : Wiz123ActiveSquares
    {
        public Wiz4ActiveSquares(MainForm main, int mapIndex, byte[] bytesFightMap) : base(main, mapIndex, bytesFightMap) { }

        public override bool IsActive(int x, int y, bool bEncountersOnly)
        {
            switch ((Wiz4Map)m_iMapIndex)
            {
                case Wiz4Map.L11Grandmaster:
                case Wiz4Map.CastleFirstFloor:
                case Wiz4Map.CastleSecondFloor:
                case Wiz4Map.CastleThirdFloor:
                    return false; // No encounters here
                default: return base.IsActive(x, y, bEncountersOnly);
            }
        }
    }

    public class FixedMonster : Monster
    {
        private string m_strOneLine;
        private string m_strMultiLine;

        public FixedMonster(string strName, string strOneLine, string strMultiLine)
        {
            Name = strName;
            m_strOneLine = strOneLine;
            m_strMultiLine = strMultiLine;
        }

        public override Monster Clone() { return new FixedMonster(Name, m_strOneLine, m_strMultiLine); }
        public override string MultiLineDescription { get { return m_strMultiLine; } }
        public override string OneLineDescription { get { return m_strOneLine; } }
    }

    public class Wiz4TrainingInfo : TrainingInfo
    {
        public WizGameState State;
        public int NumSummoning = 0;
        public byte[] MonsterFlags;

        public byte[] RawBytes { get { return Global.Combine(new byte[] { (byte)NumSummoning, (byte)(InTraining ? 1 : 0) }, MonsterFlags); } }

        public override bool InTraining
        {
            get
            {
                switch (State.Main)
                {
                    case MainState.PentagramSelect:
                    case MainState.PentagramText:
                        return Wiz4.Spots.IsPentagram(State.Location.MapIndex, State.Location.PrimaryCoordinates);
                    default:
                        return false;
                }
            }
        }

        public Wiz4MonsterIndex[] GetSelectedMonsters()
        {
            if (!InTraining || NumSummoning > 3 || NumSummoning < 0)
                return new Wiz4MonsterIndex[0];

            Wiz4MonsterIndex[] monsters = new Wiz4MonsterIndex[NumSummoning];
            List<int> flags = Global.GetBitsSet(MonsterFlags, true);
            for (int i = 0; i < NumSummoning; i++)
                monsters[i] = (Wiz4MonsterIndex) (flags.Count > i ? flags[i] : -1);

            return monsters;
        }
    }

    public class Wiz4EncounterGroup : WizEncounterGroup
    {
        public new const int Size = 8 + (WizEncounterRecord.Size * 9) + (Wiz4Monster.SizeWiz4);

        public Wiz4EncounterGroup(byte[] bytes, int offset = 0) : base(bytes, offset) { }
    }

    public class Wiz4MemoryHacker : Wiz123MemoryHacker
    {
        protected override WizMemory Memory { get { return Wiz4.Memory; } }
        public override List<WizItem> WizItems { get { return Wiz4.Items; } }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new Wiz4GameInformationControl(main); }
        protected override QuestInfo CreateQuestInfo() { return new Wiz4QuestInfo(); }
        public override bool InitExternalMonsterList() { return Wiz4.MonsterList.Value.InitExternalList(this); }
        protected override Wiz1234GameInfo CreateGameInfo() { return new Wiz4GameInfo(); }
        public override List<bool[,]> GetFights() { return Wiz4.Encounters.Fights; }
        public override string GetMapEnum(int index) { return String.Format("Wiz4Map.{0}", Enum.GetName(typeof(Wiz4Map), (Wiz4Map)(index))); }
        public override string PleaseFormPartyString { get { return "Please load or start a new game."; } }
        public override TrainingAssistantControl CreateTrainingAssistantControl(IMain main) { return new Wiz4TrainingAssistantControl(main); }

        public static MapTitleInfo GetMapTitlePair(int index)
        {
            switch ((Wiz4Map)index)
            {
                case Wiz4Map.CastleFirstFloor: return new MapTitleInfo(index, "The Castle, First Floor");
                case Wiz4Map.L1CosmicCube: return new MapTitleInfo(index, "Level 1: The Cosmic Cube");
                case Wiz4Map.L2CosmicCube: return new MapTitleInfo(index, "Level 2: The Cosmic Cube");
                case Wiz4Map.L3CosmicCube: return new MapTitleInfo(index, "Level 3: The Cosmic Cube");
                case Wiz4Map.L4MazeOfWandering: return new MapTitleInfo(index, "Level 4: The Maze of Wandering");
                case Wiz4Map.L5LandOfTheCreaturesOfLightAndDarkness: return new MapTitleInfo(index, "Level 5: Land of the Creatures of Light and Darkness");
                case Wiz4Map.L6RealmOfTheWhirlingDervish: return new MapTitleInfo(index, "Level 6: Realm of the Whirling Dervish");
                case Wiz4Map.L7TempleOfTheDreampainter: return new MapTitleInfo(index, "Level 7: Temple of the Dreampainter");
                case Wiz4Map.L8LandOf1000Cuts: return new MapTitleInfo(index, "Level 8: Land of a Thousand Cuts");
                case Wiz4Map.L9TheCatacombs: return new MapTitleInfo(index, "Level 9: The Catacombs");
                case Wiz4Map.L10PyramidOfEntrapment: return new MapTitleInfo(index, "Level 10: Pyramid of Entrapment");
                case Wiz4Map.L11Grandmaster: return new MapTitleInfo(index, "Level 11: Grandmaster");
                case Wiz4Map.CastleSecondFloor: return new MapTitleInfo(index, "The Castle, Second Floor");
                case Wiz4Map.CastleThirdFloor: return new MapTitleInfo(index, "The Castle, Third Floor");
                default: return new MapTitleInfo(index, String.Format("Maze Level {0}", index));
            }
        }

        public override MapTitleInfo GetMapTitle(int index) { return GetMapTitlePair(index); }

        protected override WizEncounterInfo CreateEncounterInfo(WizGameState state, byte[] bytesEncounter, Point ptPartyPosition, int iRewardModifier, int offset = 0)
        {
            if (state.InCombat)
                return new Wiz4EncounterInfo(state, bytesEncounter, ptPartyPosition, iRewardModifier, offset);
            MemoryBytes mb = ReadOffset(Wiz4.Memory.Group1Count, 12);
            if (mb == null)
                return null;
            return new Wiz4EncounterInfo(state, mb.Bytes, ptPartyPosition);
        }

        public Wiz4MemoryHacker()
        {
            m_game = GameNames.Wizardry4;
        }

        protected override void OnReinitialized(EventArgs e)
        {
            Wiz4.MonsterList.Value.Reinitialize(this, false);
            if (Wiz4.MonsterList.Value.UsingInternalList)
                NeedsReinitialize = true;
            Wiz4.ItemList.Value.InitExternalList(this);
            base.OnReinitialized(e);
        }

        protected override MainState GetMainState(int state1, int state2, int state3, int state4, int state5)
        {
            switch (state1)
            {
                case 0x550e: return MainState.PentagramText;
                case 0x5750: return MainState.PentagramSelect;
                case 0x51EA: return MainState.MoveSelectChars;
                case 0x57b2:
                case 0x5858: return MainState.Camp;
                case 0x5924: return MainState.CampInspecting;
                case 0x5C34: return MainState.CampInspectingRead;
                case 0x5A28: return MainState.CampEquip;
                case 0x5394:
                case 0x5172: return MainState.MainMenu;
                case 0x519E: return MainState.SelectSave;
                case 0x5400:
                case 0x53BE: return MainState.Adventuring;
                case 0x1652: return MainState.Opening;
                case 0x513A: return MainState.Opening2;
                case 0x57E8: return MainState.Treasure;
                case 0x53C0: return MainState.Question;
                case 0x5850:
                    switch (state2)
                    {
                        case 0x8962: return MainState.UseSelectItem;
                        case 0x8990: return MainState.DropSelectItem;
                        case 0x892E: return MainState.SelectSpell;
                        default: return MainState.CampInspectingCastDropUse;
                    }

                case 0x53E4: return MainState.PreCombat;
                case 0x5576:
                    switch (state2)
                    {
                        case 0x8CE4: return MainState.CombatConfirmRound;
                        case 0x8B82: return MainState.CombatOptions;
                        case 0x8C64: return MainState.CombatSelectFightTarget;
                        case 0x8C98: return MainState.CombatSelectSpell;
                        default: return MainState.Combat;
                    }
                case 0x55d4:
                case 0x53de:
                case 0x553c:
                case 0x555a:
                case 0x531a:
                case 0x55f2: return MainState.EndGame;
                case 0x535c: return MainState.LoadingMap;
                case 0x541c:
                case 0x5374:
                case 0x54f0: return MainState.Transitional;
                default: return MainState.Unknown;
            }
        }

        public override IEnumerable<Monster> GetMonsterList() { return Wiz4.Monsters; }

        protected override List<Item> GetSuperItems(WizClass wizClass, WizAlignment alignment)
        {
            List<Item> items = new List<Item>();

            items.Add(Wiz4.Items[(int)Wiz4ItemIndex.DragonsClaw]);
            items.Add(Wiz4.Items[(int)Wiz4ItemIndex.RodOfFlame]);
            items.Add(Wiz4.Items[(int)Wiz4ItemIndex.GoodHopeCape]);
            items.Add(Wiz4.Items[(int)Wiz4ItemIndex.WizardSkullcap]);
            items.Add(Wiz4.Items[(int)Wiz4ItemIndex.Robes]);
            return items;
        }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            // Wizardry 4 only has one character
            byte[] bytesChar = ReadOffset(Wiz4.Memory.WerdnaCharRecord, WizCharacter.SizeInBytes).Bytes;

            byte[] bytes = new PackedFiveBitValues(18, 18, 18, 18, 18, 18).Bytes;   // Stats
            Buffer.BlockCopy(bytes, 0, bytesChar, Wiz123.Offsets.Stats, bytes.Length);
            bytes = new PackedFiveBitValues(0, 0, 0, 0, 0).Bytes;   // Saving throws
            Buffer.BlockCopy(bytes, 0, bytesChar, Wiz123.Offsets.SavingThrows, bytes.Length);
            bytesChar[Wiz123.Offsets.Condition] = (byte)WizCondition.Good;
            Global.SetInt16(bytesChar, Wiz123.Offsets.Age, 14 * 52);
            Global.SetInt16(bytesChar, Wiz123.Offsets.Level, 99);
            Global.SetInt16(bytesChar, Wiz123.Offsets.LevelMod, 99);
            Global.SetInt16(bytesChar, Wiz123.Offsets.CurrentHP, 9999);
            Global.SetInt16(bytesChar, Wiz123.Offsets.MaxHP, 9999);
            Global.SetInt16(bytesChar, Wiz123.Offsets.ArmorClass, -10);
            Global.SetInt16(bytesChar, Wiz123.Offsets.LastArmorClass, -10);
            bytes = WizardryLong.GetBytes(99999999999);
            Buffer.BlockCopy(bytes, 0, bytesChar, Wiz123.Offsets.Gold, bytes.Length);
            bytes = new byte[] { 0xFE, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x07, 0x00 };
            Buffer.BlockCopy(bytes, 0, bytesChar, Wiz123.Offsets.Spells, bytes.Length);
            bytes = new byte[28];
            for (int i = 0; i < 28; i += 2)
            {
                bytes[i] = 9;
                bytes[i + 1] = 0;
            }
            Buffer.BlockCopy(bytes, 0, bytesChar, Wiz123.Offsets.CurrentSP, bytes.Length);

            WriteOffset(Wiz4.Memory.WerdnaCharRecord, bytesChar);

            List<Item> items = GetSuperItems(WizClass.Mage, WizAlignment.Evil);

            foreach (WizItem item in items)
                item.Identified = true;

            SetBackpack(iAddress, items, true);

            return true;
        }

        public override RosterFile CreateRoster(bool bSilent)
        {
            return Wiz4RosterFile.CreateWiz4(Global.CombineRoster(Game), bSilent);
        }

        public override QuestData GetQuestData()
        {
            return null;
        }

        public override List<MapTitleInfo> GetMapTitles()
        {
            List<MapTitleInfo> maps = new List<MapTitleInfo>(14);
            for (Wiz4Map map = Wiz4Map.CastleFirstFloor; map < Wiz4Map.Last; map++)
                maps.Add(GetMapTitlePair((int)map));
            return maps;
        }

        private WizPartyInfo ReadWiz4PartyInfo()
        {
            WizPartyInfo info = null;
            MemoryBytes bytesWerdna = ReadOffset(Wiz4.Memory.WerdnaCharRecord, WizCharacter.SizeInBytes);
            MemoryBytes bytesBox = ReadOffset(Wiz4.Memory.BlackBox, 38);

            if (bytesWerdna == null || bytesBox == null)
                return null;

            WizGameState state = GetGameState() as WizGameState;
            if (!state.InCombat)
            {
                info = new WizPartyInfo(Global.Combine(bytesWerdna, bytesBox.Bytes), 1);
                info.BlackBox = GetBlackBox(bytesBox.Bytes);
                return info;
            }

            byte numChars = (byte) GetNumChars();
            if (numChars > 6)
                numChars = 6;
            if (m_block == null)
                return null;
            if (numChars == 0)
            {
                info = new WizPartyInfo(Global.Combine(bytesWerdna, bytesBox.Bytes), 1);
                info.BlackBox = GetBlackBox(bytesBox.Bytes);
                return info;
            }

            MemoryBytes bytes = ReadOffset(Memory.PartyInfo, WizCharacter.SizeInBytes * numChars);
            info = new WizPartyInfo(Global.Combine(bytesWerdna, bytes, bytesBox.Bytes), (byte)(numChars + 1));

            info.State = GetGameState() as WizGameState;
            info.ActingChar = 0;
            info.ActingCaster = 0;
            info.BlackBox = GetBlackBox(bytesBox.Bytes);

            return info;
        }

        public override PartyInfo GetPartyInfo()
        {
            if (!IsValid)
                return null;

            return ReadWiz4PartyInfo();
        }

        public override GameInfo GetGameInfo()
        {
            if (!IsValid)
                return null;

            Wiz4GameInfo info = CreateGameInfo() as Wiz4GameInfo;
            WizGameState state = GetGameState() as WizGameState;

            switch (state.Main)
            {
                case MainState.Opening:
                    return null;
                default:
                    break;
            }

            info.Location = GetLocation();

            MemoryStream stream = new MemoryStream();

            info.InCombat = state.InCombat;

            info.Location = GetLocation();
            if (info.Location == null)
                return null;
            byte[] locationBytes = info.Location.GetBytes();
            stream.Write(locationBytes, 0, locationBytes.Length);
            stream.WriteByte((byte)(info.InCombat ? 1 : 0));

            info.ACBonus = ReadInt16(Memory.ACBonus);
            info.Light = ReadInt16(Memory.Light);
            info.TimeDelay = ReadInt16(Memory.TimeDelay);
            info.Trebor = new Point(ReadInt16(Wiz4.Memory.TreborEast), ReadInt16(Wiz4.Memory.TreborNorth));
            info.Mron = new Point(ReadInt16(Wiz4.Memory.MronEast), ReadInt16(Wiz4.Memory.MronNorth));
            info.MronHint = ReadInt16(Wiz4.Memory.MronHintIndex);
            info.AmberDragon = ReadInt16(Wiz4.Memory.AmberDragon) != 0;
            info.AmberDragonLoc = ReadInt16(Wiz4.Memory.AmberDragonLocation);
            info.Bloodstone = ReadInt16(Wiz4.Memory.Bloodstone) != 0;
            info.BloodstoneLoc = ReadInt16(Wiz4.Memory.BloodstoneLocation);
            info.Turquoise = ReadInt16(Wiz4.Memory.Turquoise) != 0;
            info.TurquoiseLoc = ReadInt16(Wiz4.Memory.TurquoiseLocation);
            info.CrystalRose = ReadInt16(Wiz4.Memory.CrystalRose) != 0;
            info.OxygenMask = ReadInt16(Wiz4.Memory.OxygenMask) != 0;
            info.SpellPowerBonus = ReadInt16(Wiz4.Memory.SpellPowerBonus);
            info.WeaponPowerBonus = ReadInt16(Wiz4.Memory.WeaponPowerBonus);
            info.ToHitBonus = ReadInt16(Wiz4.Memory.ToHitBonus);
            info.DoGooders = ReadOffset(Wiz4.Memory.DoGooders, 64);
            info.UsedHHG = ReadInt16(Wiz4.Memory.UsedHHG) != 0;
            info.UsedBoots = ReadInt16(Wiz4.Memory.UsedBoots) != 0;
            info.CurseLifted = ReadInt16(Wiz4.Memory.CurseLifted) != 0;
            info.FountainFixed = ReadInt16(Wiz4.Memory.FountainFixed) != 0;
            info.HellItems = (info.Location.IsAt(Wiz4.Spots.GatesOfHell) ? ReadInt16(Wiz4.Memory.HellItems) : 0);
            info.PinPulled = ReadUInt16(Wiz4.Memory.PinPulled);
            Global.WriteInt16(stream, info.ACBonus);
            Global.WriteInt16(stream, info.Light);
            Global.WriteInt16(stream, info.TimeDelay);
            Global.WriteInt16(stream, info.Trebor.X);
            Global.WriteInt16(stream, info.Trebor.Y);
            Global.WriteInt16(stream, info.Mron.X);
            Global.WriteInt16(stream, info.Mron.Y);
            Global.WriteInt16(stream, info.MronHint);
            Global.WriteBool(stream, info.AmberDragon);
            Global.WriteInt16(stream, info.AmberDragonLoc);
            Global.WriteBool(stream, info.Bloodstone);
            Global.WriteInt16(stream, info.BloodstoneLoc);
            Global.WriteBool(stream, info.Turquoise);
            Global.WriteInt16(stream, info.TurquoiseLoc);
            Global.WriteBool(stream, info.CrystalRose);
            Global.WriteBool(stream, info.OxygenMask);
            Global.WriteInt16(stream, info.SpellPowerBonus);
            Global.WriteInt16(stream, info.WeaponPowerBonus);
            Global.WriteInt16(stream, info.ToHitBonus);
            Global.WriteInt16(stream, info.HellItems);
            Global.WriteUInt16(stream, info.PinPulled);
            stream.Write(info.DoGooders, 0, info.DoGooders.Length);
            Global.WriteBool(stream, info.UsedHHG);
            Global.WriteBool(stream, info.UsedBoots);
            Global.WriteBool(stream, info.CurseLifted);
            Global.WriteBool(stream, info.FountainFixed);
            info.Bytes = stream.ToArray();

            return info;
        }

        public override byte[] GetBackpackBytes(int iCharAddress)
        {
            if (iCharAddress == 0)
            {
                // Include the Black Box items for the Werdna character only
                MemoryStream ms = new MemoryStream(Wiz123.Offsets.InventoryLength + 38);
                MemoryBytes mbMain = ReadOffset(Wiz4.Memory.WerdnaCharRecord + Wiz123.Offsets.Inventory, Wiz123.Offsets.InventoryLength);
                List<Item> box = GetBlackBox();
                ms.Write(mbMain.Bytes, 0, mbMain.Length);
                foreach (Item item in box)
                {
                    byte[] bytesItem = new byte[] { 0, 0, 0, 0, 1, 0, (byte)item.Index, 0 };
                    ms.Write(bytesItem, 0, bytesItem.Length);
                }
                return ms.ToArray();
            }

            return ReadOffset(Memory.PartyInfo + ((iCharAddress - 1) * WizCharacter.SizeInBytes) + Wiz123.Offsets.Inventory, Wiz123.Offsets.InventoryLength).Bytes;
        }

        public override bool SetBackpackBytes(int iCharAddress, byte[] bytes)
        {
            if (iCharAddress == 0)
            {
                // Do not include any items that are in the Black Box
                WriteOffset(Wiz4.Memory.WerdnaCharRecord + Wiz123.Offsets.Inventory, bytes, Wiz123.Offsets.InventoryLength);
            }

            if (bytes.Length > Wiz123.Offsets.InventoryLength)
                return false;

            return WriteOffset(Memory.PartyInfo + ((iCharAddress - 1) * WizCharacter.SizeInBytes) + Wiz123.Offsets.Inventory, bytes);
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            if (iAddress == 0)
                return WriteOffset(Wiz4.Memory.WerdnaCharRecord, bytes);

            return WriteOffset(Memory.PartyInfo + ((iAddress - 1) * WizCharacter.SizeInBytes), bytes, Math.Min(WizCharacter.SizeInBytes, bytes.Length));
        }

        protected override LocationInformation GetLocationForce()
        {
            LocationInformation info = base.GetLocationForce();
            info.NumChars++;    // Werdna is always available
            return info;
        }

        public override bool SetEncounterInfo(EncounterInfo info)
        {
            if (!(info is Wiz4EncounterInfo))
                return base.SetEncounterInfo(info);

            if (!IsValid)
                return false;

            Wiz4EncounterInfo wi = info as Wiz4EncounterInfo;

            if (wi.IsFightingDoGooders)
            {
                WriteOffset(Memory.CombatCharInfo, wi.GetCharBytes());
                return WriteOffset(Memory.EncounterInfo, wi.GetBytes());
            }

            // Only set the number and type of monsters in the groups
            for(int i = 0; i < wi.Groups.Count; i++)
            {
                WriteInt16(Wiz4.Memory.Group1Count + (i * 2), (Int16)wi.Groups[i].NumAlive);
                WriteInt16(Wiz4.Memory.Group1Index + (i * 2), (Int16)wi.Groups[i].Index);
            }
            for (int i = wi.Groups.Count; i < 3; i++)
            {
                WriteInt16(Wiz4.Memory.Group1Count + (i * 2), 0);
                WriteInt16(Wiz4.Memory.Group1Index + (i * 2), 0);
            }

            return true;
        }

        protected override int GetInspectingChar(MainState state = MainState.Camp) { return 0; }
        protected override int GetActingCombatChar() { return 0; }

        public override string ReplaceNoteStrings(string str)
        {
            if (!IsValid)
                return str;

            StringBuilder sbResult = new StringBuilder(str);

            if (str.Contains("EncounterMonsters"))
            {
                PartyInfo info = ReadWiz4PartyInfo();
                StringBuilder sb = new StringBuilder();
                if (str.Contains("$uniqueEncounterMonsters") || str.Contains("$allEncounterMonsters"))
                {
                    Dictionary<string, MonsterCount> dict = new Dictionary<string, MonsterCount>();
                    for (int i = 0; i < info.NumChars; i++)
                    {
                        string strName = Global.PascalString(info.Bytes, i * WizCharacter.SizeInBytes + Wiz123.Offsets.NameLength, 15);
                        if (strName != "WERDNA")
                        {
                            if (!dict.ContainsKey(strName))
                                dict.Add(strName, new MonsterCount(strName, 1));
                            else
                                dict[strName].Count += 1;
                        }
                    }
                    sbResult.Replace("$allEncounterMonsters", MonsterCount.MonsterList(dict));
                    sbResult.Replace("$uniqueEncounterMonsters", MonsterCount.MonsterListUnique(dict));
                }
            }
            return sbResult.ToString();
        }

        public List<Item> GetBlackBox()
        {
            if (!IsValid)
                return new List<Item>();

            return GetBlackBox(ReadOffset(Wiz4.Memory.BlackBox, 38).Bytes);
        }

        public List<Item> GetBlackBox(byte[] bytes)
        {
            List<Item> list = new List<Item>();

            for (int i = 0; i < 19; i++)
            {
                int index = BitConverter.ToInt16(bytes, i * 2);
                if (index > 0 && index < Wiz4.Items.Count)
                {
                    Wiz4Item item = Wiz4.CloneItem(index);
                    item.MemoryIndex = i + 8;
                    item.DisplayIndex = String.Format("{0}", (char) ('A' + i)); 
                    list.Add(item);
                }
            }

            return list;
        }

        public bool SetBlackBox(int iPosition, int iItem)
        {
            if (!IsValid)
                return false;

            return WriteInt16(Wiz4.Memory.BlackBox + (iPosition * 2), (short)iItem);
        }

        public SetBackpackResult SetBlackBox(List<Item> list)
        {
            if (!IsValid || list == null)
                return SetBackpackResult.InvalidHacker;

            byte[] bytesBox = Global.NullBytes(38);
            bool bFit = true;
            bool bInvalidItems = false;
            for(int i = 0; i < list.Count; i++)
            {
                if (i > 18)
                {
                    bFit = false;
                    break;
                }

                if (list[i] is Wiz4Item)
                    Global.SetInt16(bytesBox, i * 2, list[i].Index);
                else
                    bInvalidItems = true;
            }

            WriteOffset(Wiz4.Memory.BlackBox, bytesBox);

            if (bInvalidItems)
                return SetBackpackResult.InvalidItems;
            if (!bFit)
                return SetBackpackResult.InsufficientSpace;
            return SetBackpackResult.Success;
        }

        public override List<BaseCharacter> GetCharacters()
        {
            WizEncounterInfo encounterInfo = null;
            List<BaseCharacter> list = null;
            if (GetGameState().InCombat)
            {
                encounterInfo = GetEncounterInfo() as WizEncounterInfo;

                list = base.GetCharacters(encounterInfo);
                if (list == null)
                    list = new List<BaseCharacter>();
                else
                    return list;
            }

            MemoryBytes mbWerdna = ReadOffset(Wiz4.Memory.WerdnaCharRecord, Wiz4Character.SizeInBytes);
            if (mbWerdna == null)
                return list;

            Wiz4Character werdna = Wiz4Character.Create(0, mbWerdna.Bytes, 0, GetGameInfo() as Wiz4GameInfo, encounterInfo, GetBlackBox());
            werdna.Address = 0;
            if (list == null)
                list = new List<BaseCharacter>();

            foreach (WizCharacter wizChar in list)
                wizChar.Address++;
            list.Insert(0, werdna);

            return list;
        }

        public override ActiveSquares GetActiveSquares(MainForm form, bool bForce = false)
        {
            if (!IsValid)
                return null;

            MemoryBytes fights = ReadOffset(Memory.FightMap, 80);
            if (fights == null)
                return null;

            return new Wiz4ActiveSquares(form, GetCurrentMapIndex(), fights);
        }

        public override MonsterLocations GetMonsterLocations(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            MonsterLocations monsters = new MonsterLocations();
            FixedMonster trebor = new FixedMonster("Ghost of Trebor", "Ghost of Trebor: Invincible, avoid at all costs!",
                "The Ghost of Trebor is invincible and can kill you with a single touch.\r\nAvoid at all costs!");
            trebor.EncounterIndex = -1;
            MemoryBytes bytesTrebor = ReadOffset(Wiz4.Memory.TreborNorth, 4);
            if (bytesTrebor == null)
                return null;
            trebor.Position = new Point(BitConverter.ToInt16(bytesTrebor.Bytes, 2), BitConverter.ToInt16(bytesTrebor.Bytes, 0));

            int iHint = ReadInt16(Wiz4.Memory.MronHintIndex);
            if (iHint > 42)
                iHint = 42;
            FixedMonster mron = new FixedMonster("Mron", String.Format("Mron the Wandering Oracle: Sells hints ({0} remaining) for 2500 Gold", 42 - iHint),
                "Mron, the Wandering Oracle, will sell obscure gameplay hints to Werdna for 2500 Gold");
            mron.EncounterIndex = -2;
            mron.NPC = true;
            MemoryBytes bytesMron = ReadOffset(Wiz4.Memory.MronNorth, 4);
            mron.Position = new Point(BitConverter.ToInt16(bytesMron.Bytes, 2), BitConverter.ToInt16(bytesMron.Bytes, 0));

            monsters.RawBytes = Global.Combine(bytesTrebor, bytesMron);
            monsters.AddMonster(trebor);
            monsters.AddMonster(mron);
            monsters.AlwaysShow = true;
            return monsters;
        }

        public override TrainingInfo GetTrainingInfo()
        {
            if (!IsValid)
                return null;

            Wiz4TrainingInfo info = new Wiz4TrainingInfo();
            info.State = GetGameState() as WizGameState;
            if (info.State == null)
                return null;

            info.MapIndex = info.State.Location.MapIndex;
            MemoryBytes mbMonsters = ReadOffset(Wiz4.Memory.SummonMonsterBits, 15);
            info.MonsterFlags = mbMonsters.Bytes;
            info.NumSummoning = ReadInt16(Wiz4.Memory.SummonNumCreatures);
            return info;
        }

        protected override WizQuestData CreateQuestData()
        {
            WizPartyInfo party = GetPartyInfo() as WizPartyInfo;
            if (party == null)
                return null;
            return new Wiz4QuestData(party, GetLocation(), ReadOffset(Memory.FightMap, 80), GetGameState() as WizGameState, GetGameInfo() as Wiz4GameInfo,
                ReadOffset(Wiz4.Memory.Group1Count, 12), GetBlackBox());
        }

        public override bool SetQuestBits(int iAddress, QuestBits bits, bool bSet)
        {
            if (bits == null || bits.Bits == null || bits.Bits.Length < 1)
                return false;

            if (!(bits.Bits[0] is Wiz4QuestBits))
                return false;

            switch ((Wiz4QuestBits)bits.Bits[0])
            {
                case Wiz4QuestBits.Temple:
                    if (bSet)
                    {
                        WriteUInt16(Wiz4.Memory.Bloodstone, 1);
                        WriteUInt16(Wiz4.Memory.Turquoise, 1);
                        WriteUInt16(Wiz4.Memory.AmberDragon, 1);
                        WriteUInt16(Wiz4.Memory.BloodstoneLocation, 2);
                        WriteUInt16(Wiz4.Memory.TurquoiseLocation, 2);
                        WriteUInt16(Wiz4.Memory.AmberDragonLocation, 2);
                    }
                    else
                    {
                        WriteUInt16(Wiz4.Memory.Bloodstone, 0);
                        WriteUInt16(Wiz4.Memory.Turquoise, 0);
                        WriteUInt16(Wiz4.Memory.AmberDragon, 0);
                        WriteUInt16(Wiz4.Memory.BloodstoneLocation, 0);
                        WriteUInt16(Wiz4.Memory.TurquoiseLocation, 0);
                        WriteUInt16(Wiz4.Memory.AmberDragonLocation, 0);
                    }
                    return true;
                case Wiz4QuestBits.Trebor:
                    WriteUInt16(Wiz4.Memory.CurseLifted, (UInt16) (bSet ? 1 : 0));
                    return true;
                default:
                    return false;
            }
        }

        public override bool AutoCombat()
        {
            WizGameState state = GetGameState() as WizGameState;

            switch (state.Main)
            {
                case MainState.CombatSelectFightTarget:
                    SendKeysToDOSBox(new Keys[] { Keys.D1 }, true);
                    return true;
                case MainState.CombatConfirmRound:
                    SendKeysToDOSBox(new Keys[] { Keys.Enter }, true);
                    return true;
                case MainState.CombatOptions:
                    WizEncounterInfo info = GetEncounterInfo() as WizEncounterInfo;
                    if (info == null || info.Party.NumChars < 3)
                        SendKeysToDOSBox(new Keys[] { Keys.Enter }, true);
                    else
                        SendKeysToDOSBox(new Keys[] { Keys.Enter, Keys.None, Keys.D1 }, true);
                    return true;
                default:
                    if (state.InCombat)
                    {
                        SendKeysToDOSBox(new Keys[] { Keys.LMenu }, true);  // Speeds up combat
                        return true;
                    }
                    return false;
            }
        }

        public override bool SkipIntroductions(int iTimeout = 5000)
        {
            DateTime dtStart = DateTime.Now;
            while ((DateTime.Now - dtStart).TotalMilliseconds < iTimeout)
            {
                WizGameState state = GetGameState() as WizGameState;
                if (state != null)
                {
                    switch (state.Main)
                    {
                        case MainState.Opening:
                            SendKeysToDOSBox(new Keys[] { Keys.S }, true);  // Start the game
                            Thread.Sleep(200);
                            break;
                        case MainState.MainMenu:
                            SendKeysToDOSBox(new Keys[] { Keys.Enter }, true);  // Skip this screen
                            Thread.Sleep(200);
                            break;
                        case MainState.SelectSave:
                            SendKeysToDOSBox(new Keys[] { Keys.D1 }, true);  // Select savegame #1
                            Thread.Sleep(200);
                            break;
                        case MainState.Opening2:
                            SendKeysToDOSBox(new Keys[] { Keys.LControlKey }, true);  // add characters to the party
                            Thread.Sleep(200);
                            break;
                        case MainState.Adventuring:  // Done!
                            return true;
                        default:
                            break;
                    }
                }
                Thread.Sleep(10);
            }
            return false;
        }

        public override bool SetMonster(Monster monster) { return false; } // Monsters are compressed and currently not editable
    }
}
