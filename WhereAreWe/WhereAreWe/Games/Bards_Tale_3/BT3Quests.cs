using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    public static class BT3Bits
    {
        public enum Scripts
        {
            PlantAcorn_VT30102 = 00,
            WaterAcorn_VT30102 = 01,
            GetNightspear_VT40304 = 02,
            DefeatTslotha_FP20211 = 04,
            GetTslothasHead_FP20211 = 05,
            GetTslothasHeart_FP20211 = 06,
            Grove_CB0709 = 07,
            GiveTslothasHead_CB0811 = 07,
            TalkKing_CB0811 = 20,
            LEVIWhiteTowerDoor_IK11109 = 08,
            ANMAWhiteTowerDoor_IK11109 = 09,
            PHDOWhiteTowerDoor_IK11109 = 10,
            INWOGreyTowerDoor_IK10009 = 11,
            WIHEGreyTowerDoor_IK10009 = 12,
            FOFOGreyTowerDoor_IK10009 = 13,
            INVIGreyTowerDoor_IK10009 = 14,
            MAFLBlackTowerDoor_IK11100 = 15,
            SHSPBlackTowerDoor_IK11100 = 16,
            FEARBlackTowerDoor_IK11100 = 17,
            SUELBlackTowerDoor_IK11100 = 18,
            SPBIBlackTowerDoor_IK11100 = 19,
            GetBowChamber_SG0900 = 21,
            GetArrowsChamber_SG0900 = 22,
            UseHeartValarian_SG0403 = 23,
            UseWaterValarian_SG0403 = 24,
            SacredFlameOpensDoor_SG0403 = 25,
            TalkHawkslayer_A1011 = 26,
            DefeatDragon_M20500 = 27,
            GetCrystalKey_M20500 = 28,
            FLANCyanis_CT30005 = 29,
            FLANCyanis_CT30302 = 29,
            DefeatCyanis_CT30005 = 30,
            DefeatCyanis_CT30302 = 30,
            GetTriangle_CT30302 = 31,
            UseDragonBlood_L0608 = 32,
            GiveWhiteRose_AT20500 = 33,
            GiveWhiteRose_AT20400 = 33,
            GiveBlueRose_AT20005 = 34,
            GiveBlueRose_AT20004 = 34,
            GiveRedRose_AT20508 = 35,
            GiveRedRose_AT20608 = 35,
            GiveYellowRose_AT21100 = 36,
            GiveYellowRose_AT21000 = 36,
            GiveRainbowRose_AT21005 = 37,
            GiveRainbowRose_AT21004 = 37,
            GetCrown_AT20905 = 38,
            GetBelt_AT20905 = 39,
            UseTriangle_AT10615 = 40,
            GetStrifespear_M11002 = 41,
            Hawkslayer_A1011 = 42,
            TalkHawkslayer_F0302 = 43,
            GetHawkslayer_F0302 = 44,
            TalkFerofist_PQ0215 = 45,
            GetLeftKey_PQ0811 = 46,
            GetRightKey_B1102 = 47,
            DefeatWhiteWizard_WT40400 = 48,
            GetCrystalLens_WT40400 = 49,
            DefeatGreyWizard_GT40203 = 50,
            GetSmokeyLens_GT40203 = 51,
            DefeatBlackWizard_BT40304 = 52,
            GetBlackLens_BT40304 = 53,
            GetShadowLock_SC0307 = 54,
            GetShadowDoor_DC0505 = 55,
            UseShadowDoor_N0505 = 56,
            UseShadowLock_N0505 = 57,
            UseRightKey_W0202 = 58,
            UseLeftKey_W0202 = 59,
            GetWand_ID20400 = 60,
            GetSphere_ID20400 = 61,
            GiveShieldToElder_SB1514 = 62,
            TalkElder_SB1514 = 63,
            DefeatUrmech_S0607 = 64,
            SpareUrmech_S0607 = 65,
            SpareUrmech_S0602 = 65,
            GetHelm_S0703 = 66,
            GetHammer_S0703 = 67,
            KillWerra_T0000 = 68,
            KillShield_T0001 = 68,
            GetShield_T0001 = 69,
            ElderDies_SB1514 = 70,
            UseHelm_M32102 = 72,
            DoorHelm_M31107 = 72,
            DoorSphere_M31107 = 73,
            DoorBelt_M31107 = 74,
            DoorCloak_M31107 = 75,
            DoorBow_M31107 = 76,
            DoorShield_M31107 = 77,
            UseSphere_M20912 = 73,
            UseBelt_M10316 = 74,
            UseCloak_M31520 = 75,
            UseBow_M21816 = 76,
            UseShield_M31205 = 77,
            DefeatTarjan_T0302 = 78,
            DefeatLorini_T0302 = 79,
            UseCrystalLens_IK10509 = 80,
            UseSmokeyLens_IK10509 = 81,
            UseBlackLens_IK10509 = 82,
            DefeatSceadu_SD20513 = 83,
            GetCloak_SD20513 = 84,
            GetJustice_SD20513 = 85,
            None = -1,
        }

        public static bool IsSet(byte[] bytes, Scripts bit) { return bit == Scripts.None ? false : (Global.GetBit(bytes, (int)bit) == 1); }

        public static BitDesc ScriptsDescription(object val) { return Description((Scripts)val); }
        public static BitDesc AwardsDescription(object val) { return Description((BT3AwardIndex)val); }

        private const string enter = "Enter the location";

        public static BitDesc Description(BT3AwardIndex ai)
        {
            string strWhen = BT3Character.AwardString(ai);
            switch (ai)
            {
                case BT3AwardIndex.BeginValariansBow:
                case BT3AwardIndex.FinishValariansBow:
                case BT3AwardIndex.BeginLanatirSphere:
                case BT3AwardIndex.FinishLanatirSphere:
                case BT3AwardIndex.BeginBeltOfAlliria:
                case BT3AwardIndex.FinishBeltOfAlliria:
                case BT3AwardIndex.BeginFerofistsHelm:
                case BT3AwardIndex.FinishFerofistsHelm:
                case BT3AwardIndex.BeginSceadusCloak:
                case BT3AwardIndex.FinishSceadusCloak:
                case BT3AwardIndex.BeginWerrasShield:
                case BT3AwardIndex.FinishWerrasShield:
                case BT3AwardIndex.TalkElder1: return new BitDesc(strWhen, BT3.Spots.SB_ReviewBoard, "Talk to the Elder");
                case BT3AwardIndex.BeginDefeatTarjan:
                case BT3AwardIndex.FinishDefeatTarjan: return new BitDesc(strWhen, BT3.Spots.SB_ReviewBoard);
                case BT3AwardIndex.DefeatBrilhasti: return new BitDesc(strWhen, BT3.Spots.U4_Brilhasti, "Talk to the Elder", BT3.Spots.SB_ReviewBoard);
            }
            if (!String.IsNullOrWhiteSpace(strWhen))
                return new BitDesc(strWhen);
            return BitDesc.Empty;
        }

        public static BitDesc Description(Scripts bit)
        {
            switch (bit)
            {
                case Scripts.WaterAcorn_VT30102: return new BitDesc("Use container with \"Water of Life\"", BT3.Spots.VT3_0102_Acorn, "Enter the square");
                case Scripts.PlantAcorn_VT30102: return new BitDesc("Use \"Acorn\"", BT3.Spots.VT3_0102_Acorn, "Enter the square");
                case Scripts.GetNightspear_VT40304: return new BitDesc("Obtain \"Nightspear\"", BT3.Spots.VT4_0304_Nightspear, "Enter the square");
                case Scripts.DefeatTslotha_FP20211: return new BitDesc("Defeat \"Tslotha Garnath\"", BT3.Spots.FP2_0211_Tslotha, "Enter the square");
                case Scripts.GetTslothasHead_FP20211: return new BitDesc("Obtain \"Tslotha's Head\"", BT3.Spots.FP2_0211_Tslotha, "Examine Tslotha Garnath");
                case Scripts.GetTslothasHeart_FP20211: return new BitDesc("Obtain \"Tslotha's Heart\"", BT3.Spots.FP2_0211_Tslotha, "Examine Tslotha Garnath");
                case Scripts.Grove_CB0709: return new BitDesc("Give \"Tslotha's Head\" to King", BT3.Spots.CB_0811_King, "Enter the square").AddChecked("Enter the square", BT3.Spots.CB_0709_King);
                case Scripts.TalkKing_CB0811: return new BitDesc("Meet the King", BT3.Spots.CB_0709_King, "Enter the square");
                case Scripts.LEVIWhiteTowerDoor_IK11109: return new BitDesc("Cast \"Levitation\"", BT3.Spots.IK1_1109_WhiteTowerDoor, "Enter the square");
                case Scripts.ANMAWhiteTowerDoor_IK11109: return new BitDesc("Cast \"Anti-Magic\"", BT3.Spots.IK1_1109_WhiteTowerDoor, "Enter the square");
                case Scripts.PHDOWhiteTowerDoor_IK11109: return new BitDesc("Cast \"Phase Door\"", BT3.Spots.IK1_1109_WhiteTowerDoor, "Enter the square");
                case Scripts.INWOGreyTowerDoor_IK10009: return new BitDesc("Cast \"Elik's Instant Wolf\"", BT3.Spots.IK1_0009_GreyTowerDoor, "Enter the square");
                case Scripts.WIHEGreyTowerDoor_IK10009: return new BitDesc("Cast \"Wind Hero\"", BT3.Spots.IK1_0009_GreyTowerDoor, "Enter the square");
                case Scripts.FOFOGreyTowerDoor_IK10009: return new BitDesc("Cast \"Fanskar's Force Focus\"", BT3.Spots.IK1_0009_GreyTowerDoor, "Enter the square");
                case Scripts.INVIGreyTowerDoor_IK10009: return new BitDesc("Cast \"Kylearan's Invisibility Spell\"", BT3.Spots.IK1_0009_GreyTowerDoor, "Enter the square");
                case Scripts.MAFLBlackTowerDoor_IK11100: return new BitDesc("Cast MAFL/LERE/GRRE", BT3.Spots.IK1_1100_BlackTowerDoor, "Enter the square");
                case Scripts.SHSPBlackTowerDoor_IK11100: return new BitDesc("Cast \"Shock-Sphere\"", BT3.Spots.IK1_1100_BlackTowerDoor, "Enter the square");
                case Scripts.FEARBlackTowerDoor_IK11100: return new BitDesc("Cast \"Word of Fear\"", BT3.Spots.IK1_1100_BlackTowerDoor, "Enter the square");
                case Scripts.SUELBlackTowerDoor_IK11100: return new BitDesc("Cast \"Summon Elemental\"", BT3.Spots.IK1_1100_BlackTowerDoor, "Enter the square");
                case Scripts.SPBIBlackTowerDoor_IK11100: return new BitDesc("Cast \"Baylor's Spell Bind\"", BT3.Spots.IK1_1100_BlackTowerDoor, "Enter the square");
                case Scripts.GetBowChamber_SG0900: return new BitDesc("Obtain \"Valarian's Bow\"", BT3.Spots.SG_0900_Chamber, "Enter the square");
                case Scripts.GetArrowsChamber_SG0900: return new BitDesc("Obtain \"Arrows of Life\"", BT3.Spots.SG_0900_Chamber, "Enter the square");
                case Scripts.UseHeartValarian_SG0403: return new BitDesc("Use \"Tslotha's Heart\"", BT3.Spots.SG_0403_Valarian, "Enter the square");
                case Scripts.UseWaterValarian_SG0403: return new BitDesc("Use container with \"Water of Life\"", BT3.Spots.SG_0403_Valarian, "Enter the square");
                case Scripts.SacredFlameOpensDoor_SG0403: return new BitDesc("Sacred flame opens Valarian's tomb", BT3.Spots.SG_0403_Valarian);
                case Scripts.TalkHawkslayer_A1011: return new BitDesc("Talk to Hawkslayer", BT3.Spots.A_1011_Hawkslayer, "Enter the square");
                case Scripts.DefeatDragon_M20500: return new BitDesc("Defeat \"Rainbow Dragon\"", BT3.Spots.M2_0500_DragonBlood, "Enter the square");
                case Scripts.GetCrystalKey_M20500: return new BitDesc("Obtain \"Crystal Key\"", BT3.Spots.M2_0500_DragonBlood, "Enter the square");
                case Scripts.FLANCyanis_CT30302: return new BitDesc("Cast FLAN/REST/HEAL", BT3.Spots.CT3_0302_Cyanis, "Enter the square").AddChecked("Enter the square", BT3.Spots.CT3_0005_Figures);
                case Scripts.DefeatCyanis_CT30302: return new BitDesc("Defeat \"Cyanis\"", BT3.Spots.CT3_0302_Cyanis, "Enter the square").AddChecked("Enter the square", BT3.Spots.CT3_0005_Figures);
                case Scripts.GetTriangle_CT30302: return new BitDesc("Obtain \"Magic Triangle\"", BT3.Spots.CT3_0302_Cyanis, "Enter the square");
                case Scripts.UseDragonBlood_L0608: return new BitDesc("Use container with \"Dragon Blood\"", BT3.Spots.L_0608_RainbowRose, "Enter the square").AddClear("Pick \"Rainbow Rose\"", BT3.Spots.L_0608_RainbowRose);
                case Scripts.GiveWhiteRose_AT20500: return new BitDesc("Give \"White Rose\" to Shade", BT3.Spots.AT2_0400_WhiteRose, "Enter the square").AddChecked("Enter the square", BT3.Spots.AT2_0500_Teleport);
                case Scripts.GiveBlueRose_AT20004: return new BitDesc("Give \"Blue Rose\" to Shade", BT3.Spots.AT2_0005_BlueRose, "Enter the square", BT3.Spots.AT2_0004_Teleport);
                case Scripts.GiveRedRose_AT20508: return new BitDesc("Give \"Red Rose\" to Shade", BT3.Spots.AT2_0608_RedRose, "Enter the Square",  BT3.Spots.AT2_0508_Teleport);
                case Scripts.GiveYellowRose_AT21100: return new BitDesc("Give \"Yellow Rose\" to Shade", BT3.Spots.AT2_1000_YellowRose, "Enter the square").AddChecked("Enter the square", BT3.Spots.AT2_1100_Teleport);
                case Scripts.GiveRainbowRose_AT21005: return new BitDesc("Give \"Rainbow Rose\" to Shade", BT3.Spots.AT2_1004_RainbowRose, "Enter the square").AddChecked("Enter the square", BT3.Spots.AT2_1005_Teleport);
                case Scripts.GetCrown_AT20905: return new BitDesc("Obtain \"Crown of Truth\"", BT3.Spots.AT2_0905_Alliria, "Enter the square");
                case Scripts.GetBelt_AT20905: return new BitDesc("Obtain \"Belt of Alliria\"", BT3.Spots.AT2_0905_Alliria, "Enter the square");
                case Scripts.UseTriangle_AT10615: return new BitDesc("Use \"Magic Triangle\"", BT3.Spots.AT1_0615_Crystal, "Enter the square");
                case Scripts.GetStrifespear_M11002: return new BitDesc("Obtain \"Strifespear\"", BT3.Spots.M1_1002_Strifespear, "Enter the square");
                case Scripts.Hawkslayer_A1011: return new BitDesc("Recruit \"Hawkslayer\"", BT3.Spots.A_1011_Hawkslayer, "Enter the square");
                case Scripts.TalkHawkslayer_F0302: return new BitDesc("Talk to Hawkslayer", BT3.Spots.F_0302_Hawkslayer, "Enter the square");
                case Scripts.GetHawkslayer_F0302: return new BitDesc("Recruit \"Hawkslayer\"", BT3.Spots.F_0302_Hawkslayer, "Enter the square");
                case Scripts.TalkFerofist_PQ0215: return new BitDesc("Talk to dwarf", BT3.Spots.PQ_0215_Ferofist, "Enter the square");
                case Scripts.GetLeftKey_PQ0811: return new BitDesc("Obtain \"Left Key\"", BT3.Spots.PQ_0811_LeftKey, "Enter the square");
                case Scripts.GetRightKey_B1102: return new BitDesc("Obtain \"Right Key\"", BT3.Spots.B_1102_RightKey, "Enter the square");
                case Scripts.DefeatWhiteWizard_WT40400: return new BitDesc("Defeat \"White Wizard\"", BT3.Spots.WT4_0400_CrystalLens, "Enter the square");
                case Scripts.GetCrystalLens_WT40400: return new BitDesc("Obtain \"Crystal Lens\"", BT3.Spots.WT4_0400_CrystalLens, "Enter the square");
                case Scripts.DefeatGreyWizard_GT40203: return new BitDesc("Defeat \"Grey Wizard\"", BT3.Spots.GT4_0203_SmokeyLens, "Enter the square");
                case Scripts.GetSmokeyLens_GT40203: return new BitDesc("Obtain \"Smokey Lens\"", BT3.Spots.GT4_0203_SmokeyLens, "Enter the square");
                case Scripts.DefeatBlackWizard_BT40304: return new BitDesc("Defeat \"Black Wizard\"", BT3.Spots.BT4_0304_BlackLens, "Enter the square");
                case Scripts.GetBlackLens_BT40304: return new BitDesc("Obtain \"Black Lens\"", BT3.Spots.BT4_0304_BlackLens, "Enter the square");
                case Scripts.GetShadowLock_SC0307: return new BitDesc("Obtain \"Shadow Lock\"", BT3.Spots.SC_0307_ShadowLock, "Enter the square");
                case Scripts.GetShadowDoor_DC0505: return new BitDesc("Obtain \"Shadow Door\"", BT3.Spots.DC_0505_ShadowDoor, "Enter the square");
                case Scripts.UseShadowDoor_N0505: return new BitDesc("Use \"Shadow Door\"", BT3.Spots.N_0505_MagicDoor, "Enter the square");
                case Scripts.UseShadowLock_N0505: return new BitDesc("Use \"Shadow Lock\"", BT3.Spots.N_0505_MagicDoor, "Enter the square");
                case Scripts.UseRightKey_W0202: return new BitDesc("Use \"Right Key\"", BT3.Spots.W_0202_Portal, "Enter the square").AddClear("Turn \"Right Key\" other than 18 times", BT3.Spots.W_0202_Portal);
                case Scripts.UseLeftKey_W0202: return new BitDesc("Use \"Left Key\"", BT3.Spots.W_0202_Portal, "Enter the square").AddClear("Turn \"Left Key\" other than 15 times", BT3.Spots.W_0202_Portal);
                case Scripts.GetWand_ID20400: return new BitDesc("Obtain \"Wand of Power\"", BT3.Spots.ID2_0400_Lanatir, "Enter the square");
                case Scripts.GetSphere_ID20400: return new BitDesc("Obtain \"Sphere of Lanatir\"", BT3.Spots.ID2_0400_Lanatir, "Enter the square");
                case Scripts.GiveShieldToElder_SB1514: return new BitDesc("Give \"Werra's Shield\" to the guild elder", BT3.Spots.SB_ReviewBoard);
                case Scripts.TalkElder_SB1514: return new BitDesc("Talk to the guild elder", BT3.Spots.SB_ReviewBoard);
                case Scripts.DefeatUrmech_S0607: return new BitDesc("Defeat \"Urmech\"", BT3.Spots.S_0607_Urmech, "Enter the square");
                case Scripts.SpareUrmech_S0607: return new BitDesc("Spare Urmech ", BT3.Spots.S_0607_Urmech, "Enter the square").AddChecked("Enter the square", BT3.Spots.S_0602_Geomancer);
                case Scripts.GetHelm_S0703: return new BitDesc("Obtain \"Ferofist's Helm\"", BT3.Spots.S_0703_Chest, "Enter the square");
                case Scripts.GetHammer_S0703: return new BitDesc("Obtain \"Hammer of Wrath\"", BT3.Spots.S_0703_Chest, "Enter the square");
                case Scripts.KillWerra_T0000: return new BitDesc("Black Slayers kill Werra", BT3.Spots.T_0000_Werra, "Enter the square").AddSet("Black Slayers kill Werra", BT3.Spots.T_0001_Shield).AddChecked("Enter the square", BT3.Spots.T_0001_Shield);
                case Scripts.GetShield_T0001: return new BitDesc("Obtain \"Werra's Shield\"", BT3.Spots.T_0001_Shield, "Enter the square");
                case Scripts.ElderDies_SB1514: return new BitDesc("Guild elder dies", BT3.Spots.SB_ReviewBoard);
                case Scripts.UseHelm_M32102: return new BitDesc("Use \"Ferofist's Helm\"", BT3.Spots.M3_2102_Helm, "Enter the square").AddChecked("Enter the square", BT3.Spots.M3_1107_Door);
                case Scripts.UseSphere_M20912: return new BitDesc("Use \"Sphere of Lanatir\"", BT3.Spots.M2_0912_Sphere, "Enter the square").AddChecked("Enter the square", BT3.Spots.M3_1107_Door);
                case Scripts.UseBelt_M10316: return new BitDesc("Use \"Belt of Alliria\"", BT3.Spots.M1_0316_Belt, "Enter the square").AddChecked("Enter the square", BT3.Spots.M3_1107_Door);
                case Scripts.UseCloak_M31520: return new BitDesc("Use \"Sceadu's Cloak\"", BT3.Spots.M3_1520_Cloak, "Enter the square").AddChecked("Enter the square", BT3.Spots.M3_1107_Door);
                case Scripts.UseBow_M21816: return new BitDesc("Use \"Valarian's Bow\"", BT3.Spots.M2_1816_Bow, "Enter the square").AddChecked("Enter the square", BT3.Spots.M3_1107_Door);
                case Scripts.UseShield_M31205: return new BitDesc("Use \"Werra's Shield\"", BT3.Spots.M3_1205_Shield, "Enter the square").AddChecked("Enter the square", BT3.Spots.M3_1107_Door);
                case Scripts.DefeatLorini_T0302: return new BitDesc("Defeat \"Lorini\"", BT3.Spots.T_0302_Tarjan);
                case Scripts.DefeatTarjan_T0302: return new BitDesc("Defeat \"Tarjan\"", BT3.Spots.T_0302_Tarjan);
                case Scripts.UseCrystalLens_IK10509: return new BitDesc("Use \"Crystal Lens\"", BT3.Spots.IK1_0509_IceDungeonEnt, "Enter the square");
                case Scripts.UseSmokeyLens_IK10509: return new BitDesc("Use \"Smokey Lens\"", BT3.Spots.IK1_0509_IceDungeonEnt, "Enter the square");
                case Scripts.UseBlackLens_IK10509: return new BitDesc("Use \"Black Lens\"", BT3.Spots.IK1_0509_IceDungeonEnt, "Enter the square");
                case Scripts.DefeatSceadu_SD20513: return new BitDesc("Defeat \"Sceadu\"", BT3.Spots.SD2_0513_Sceadu, "Enter the square");
                case Scripts.GetCloak_SD20513: return new BitDesc("Obtain \"Sceadu's Cloak\"", BT3.Spots.SD2_0513_Sceadu, "Enter the square");
                case Scripts.GetJustice_SD20513: return new BitDesc("Obtain \"Helm of Justice\"", BT3.Spots.SD2_0513_Sceadu, "Enter the square");
                default: return BitDesc.Empty;
            }
        }
    }

    public class BT3QuestData : BTQuestData
    {
        public BT3Effects Effects;
        public byte[] QuestBits;

        public BT3QuestData(BTPartyInfo party, LocationInformation location, BTGameState state, byte[] questBits, BT3MapData mapData, BT3Effects effects)
            : base(party, location, state, null, null)
        {
            Map = mapData;
            Effects = effects;
            QuestBits = questBits;
        }

        public bool IsActive(Point pt)
        {
            return false;
        }

        private BTWall Wall(BTMapSquareInfo info, Direction dir)
        {
            if (info == null)
                return BTWall.Open;
            switch (dir)
            {
                case Direction.Left: return info.West;
                case Direction.Right: return info.East;
                case Direction.Down: return info.South;
                case Direction.Up: return info.North;
                default: return BTWall.Open;
            }
        }

        public bool HasDoor(MapXY spot, Direction dir) { return HasDoor(spot.X, spot.Y, dir); }

        public bool HasDoor(int x, int y, Direction dir)
        {
            switch (Wall(BTMapSquareInfo.CreateBT3(Map as BT3MapData, Map.Height - y - 1, x), dir))
            {
                case BTWall.Door:
                case BTWall.BlueDoor:
                case BTWall.NoPhaseBlueDoor:
                case BTWall.NoPhaseDoor:
                    return true;
                default:
                    return false;
            }
        }

        public bool HasFlag(Point pt, BT3MapSpecials flag)
        {
            return BTMapSquareInfo.GetBT3Specials(Map as BT3MapData, Map.Height - pt.Y - 1, pt.X).HasFlag(flag);
        }

        public override void AddBytes(Stream stream)
        {
            base.AddBytes(stream);
            stream.WriteByte(Party.NumChars);
            Global.WriteBytes(stream, ((BT3MapData) Map).Squares);
            Global.WriteBytes(stream, Effects.Bytes);
            Global.WriteBytes(stream, QuestBits);
            for (int i = 0; i < 7; i++)
            {
                if (Party.Bytes != null && Party.CharacterSize > 0 && Party.Bytes.Length >= Party.CharacterSize * (i+1))
                {
                    stream.WriteByte(Party.Bytes[i * Party.CharacterSize + BT3.Offsets.Class]);
                    stream.WriteByte(Party.Bytes[i * Party.CharacterSize + BT3.Offsets.Condition]);
                    Global.WriteBytes(stream, Party.Bytes, i * Party.CharacterSize + BT3.Offsets.Inventory, BT3.Offsets.InventoryLength);
                    Global.WriteBytes(stream, Party.Bytes, i * Party.CharacterSize + BT3.Offsets.Name, BT3.Offsets.NameLength);
                }
            }
        }
    }

    public class BT3Quest : BasicQuest
    {
        public BT3Quest()
        {
        }
    }

    public class BT3QuestInfo : QuestInfo
    {
        public QuestStatus Brilhasti = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat Brilhasti ap Tarj");
        public QuestStatus Bow = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve Valarian's Bow");
        public QuestStatus Sphere = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve the Sphere of Lanatir");
        public QuestStatus Belt = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve the Belt of Alliria");
        public QuestStatus Helm = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve Ferofist's Helm");
        public QuestStatus Cloak = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve Sceadu's Cloak");
        public QuestStatus Shield = new QuestStatus(QuestStatus.Basic.NotStarted, "Retrieve Werra's Shield");
        public QuestStatus Tarjan = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat Tarjan");
        public QuestStatus Spells = new QuestStatus(QuestStatus.Basic.NotStarted, "Learn various spells");
        public QuestStatus Monsters = new QuestStatus(QuestStatus.Basic.NotStarted, "Defeat various specific monsters");
        public int FlameCounter = -1;

        public override QuestStatus[] GetAllQuests() { return new QuestStatus[] { Brilhasti, Bow, Sphere, Belt, Helm, Cloak, Shield, Tarjan, Spells, Monsters }; }

        protected override BasicQuest GetQuest(QuestStatus status, BasicQuestType type, object bits, string name, string giver, string reward, params QuestLocation[] locations)
        {
            return GetQuest<BT3Quest>(status, type, bits, name, giver, reward, null, locations);
        }

        public override bool QuestsEqual(QuestInfo info)
        {
            if (!base.QuestsEqual(info))
                return false;
            return (FlameCounter == ((BT3QuestInfo) info).FlameCounter);
        }

        public BasicQuest AddSideQuest(QuestStatus status, List<BT3Quest> quests, string strReward = "", string strPath = "")
        {
            BT3Quest quest = GetQuest(status, BasicQuestType.Side, null, null, strReward) as BT3Quest;
            if (!String.IsNullOrWhiteSpace(strPath))
                quest.Path = strPath;
            quests.Add(quest);
            return quest;
        }

        public BasicQuest AddMainQuest(QuestStatus status, List<BT3Quest> quests, string strReward = "")
        {
            return AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
        }

        public BasicQuest AddMainQuest(int iSortOrder, QuestStatus status, List<BT3Quest> quests, string strReward = "")
        {
            BasicQuest quest = AddQuest(BasicQuestType.Primary, status, quests, null, String.Empty, strReward, String.Empty);
            quest.SortOrder = iSortOrder;
            return quest;
        }

        public BasicQuest AddQuest(BasicQuestType type, QuestStatus status, List<BT3Quest> quests, object bits, string strGiver, string strReward, string strPath)
        {
            BT3Quest quest = null;
            if (status.Main != QuestStatus.Basic.InvalidClass)
            {
                quest = GetQuest(status, type, bits, strGiver, strReward) as BT3Quest;
                quest.Path = strPath;
                quests.Add(quest);
            }
            return quest;
        }

        public override BasicQuest[] GetQuests()
        {
            List<BT3Quest> quests = new List<BT3Quest>();
            int iMainOrder = 0;

            Brilhasti.AddLocations(new QuestLocation("Go to the Skara Brae Ruins", BT3.Spots.W_SkaraBrae),
                new QuestLocation("Talk to the Guild Elder", BT3.Spots.SB_ReviewBoard),
                new QuestLocation("Tell the priest \"Tarjan\"", BT3.Spots.SB_Temple),
                new QuestLocation("Descend to the Tunnels", BT3.Spots.C_ToTunnels),
                new QuestLocation("Learn the password \"Chaos\"", BT3.Spots.T_LearnChaos),
                new QuestLocation("Tell the priest \"Chaos\"", BT3.Spots.SB_Temple),
                new QuestLocation("Answer the riddle (\"Blue\")", BT3.Spots.U1_RiddleBlue),
                new QuestLocation("Descend to Unterbrae 2", BT3.Spots.U1_ToUnterbrae2),
                new QuestLocation("Answer the riddle (\"Shadow\")", BT3.Spots.U2_RiddleShadow),
                new QuestLocation("Descend to Unterbrae 3", BT3.Spots.U2_ToUnterbrae3),
                new QuestLocation("Answer the riddle (\"Sword\")", BT3.Spots.U3_RiddleSword),
                new QuestLocation("Descend to Unterbrae 4", BT3.Spots.U3_ToUnterbrae4),
                new QuestLocation("Defeat Brilhasti ap Tarj", BT3.Spots.U4_Brilhasti));
            Brilhasti.Postrequisites.Add(new QuestLocation("Return to the Guild Elder", BT3.Spots.SB_ReviewBoard));
            AddMainQuest(iMainOrder++, Brilhasti, quests, "Exp to level 35");

            string strFlame = FlameCounter == -1 ? "" : String.Format("({0}) ", Global.Plural(FlameCounter, "turn"));

            Bow.AddLocations(new QuestLocation("Train a Chronomancer", BT3.Spots.SB_ReviewBoard),
                new QuestLocation("Cast \"To Arboria\" (ARBO)", BT3.Spots.W_ToArboria),
                new QuestLocation("Recruit Hawkslayer into the party", BT3.Spots.A_1011_Hawkslayer),
                new QuestLocation("Obtain \"Arefolia\"", BT3.Spots.A_Arefolia),
                new QuestLocation("Learn the spell \"Gilles Gills\" (GILL)", BT3.Spots.A_Fisherman),
                new QuestLocation("Obtain \"Acorn\"", BT3.Spots.A_Acorn),
                new QuestLocation("Go to Ciera Brannia", BT3.Spots.A_ToCieraBrannia),
                new QuestLocation("Talk to King", BT3.Spots.CB_KingsCourt),
                new QuestLocation("Go to the Crystal Palace", BT3.Spots.A_ToCrystalPalace),
                new QuestLocation("Obtain a container", BT3.Spots.W_Tavern),
                new QuestLocation("Obtain \"Water of Life\"", BT3.Spots.CP_WaterOfLife),
                new QuestLocation("Go to Valarian's Tower", BT3.Spots.A_ToValariansTower),
                new QuestLocation("Ascend to level 2", BT3.Spots.VT1_ToValariansTower2),
                new QuestLocation("Ascend to level 3", BT3.Spots.VT2_ToValariansTower3),
                new QuestLocation("Use \"Acorn\"", BT3.Spots.VT3_0102_Acorn),
                new QuestLocation("Use \"Water of Life\"", BT3.Spots.VT3_0102_Acorn),
                new QuestLocation("Ascend to level 4", BT3.Spots.VT3_ToValariansTower4),
                new QuestLocation("Obtain \"Nightspear\"", BT3.Spots.VT4_Nightspear),
                new QuestLocation("Give \"Nightspear\" to a party member", BT3.Spots.VT4_Nightspear),
                new QuestLocation("Go to the Festering Pit", BT3.Spots.A_ToFesteringPit1),
                new QuestLocation("Descend to level 2", BT3.Spots.FP1_ToFesteringPit2A),
                new QuestLocation("Defeat Tslotha Garnath", BT3.Spots.FP2_TslothaGarnath),
                new QuestLocation("Obtain \"Tslotha's Head\"", BT3.Spots.FP2_TslothaGarnath),
                new QuestLocation("Give \"Tslotha's Head\" to a party member", BT3.Spots.FP2_TslothaGarnath),
                new QuestLocation("Obtain \"Tslotha's Heart\"", BT3.Spots.FP2_TslothaGarnath),
                new QuestLocation("Give \"Tslotha's Heart\" to a party member", BT3.Spots.FP2_TslothaGarnath),
                new QuestLocation("Give \"Tslotha's Head\" to King", BT3.Spots.CB_KingsCourt),
                new QuestLocation("Go to the Sacred Grove", BT3.Spots.CB_SacredGrove),
                new QuestLocation("Use \"Tslotha's Heart\"", BT3.Spots.SG_Valarian),
                new QuestLocation("Use \"Water of Life\"", BT3.Spots.SG_Valarian),
                new QuestLocation("Wait " + strFlame + "for the eternal flame to open the door", BT3.Spots.SG_ValarianOuterChamber),
                new QuestLocation("Obtain \"Valarian's Bow\"", BT3.Spots.SG_ValariansBow),
                new QuestLocation("Obtain \"Arrows of Life\"", BT3.Spots.SG_ValariansBow));
            Bow.Postrequisites.Add(new QuestLocation("Return to the Guild Elder", BT3.Spots.SB_ReviewBoard));
            AddMainQuest(iMainOrder++, Bow, quests, "600000 Exp");

            Sphere.AddLocations(new QuestLocation("Learn spells \"To Gelidia\" and \"From Gelidia\"", BT3.Spots.SB_ReviewBoard),
                new QuestLocation("Cast \"To Gelidia\" (GELI)", BT3.Spots.W_ToGelidia),
                new QuestLocation("Learn about Alendar", BT3.Spots.G_Outpost),
                new QuestLocation("Go to the Ice Keep", BT3.Spots.G_ToIceKeep1),
                new QuestLocation("Cast \"Levitation\" (LEVI)", BT3.Spots.IK1_1109_WhiteTowerDoor),
                new QuestLocation("Cast \"Anti-Magic\" (ANMA)", BT3.Spots.IK1_1109_WhiteTowerDoor),
                new QuestLocation("Cast \"Phase Door\" (PHDO)", BT3.Spots.IK1_1109_WhiteTowerDoor),
                new QuestLocation("Enter the White Tower", BT3.Spots.IK1_1109_WhiteTowerDoor),
                new QuestLocation("Ascend to level 2", BT3.Spots.WT1_ToWhiteTower2),
                new QuestLocation("Ascend to level 3", BT3.Spots.WT2_UpDown),
                new QuestLocation("Ascend to level 4", BT3.Spots.WT3_UpDown),
                new QuestLocation("Defeat the White Wizards", BT3.Spots.WT4_CrystalLens),
                new QuestLocation("Obtain \"Crystal Lens\"", BT3.Spots.WT4_CrystalLens),
                new QuestLocation("Cast \"Mage Flame\" (MAFL)", BT3.Spots.IK1_1100_BlackTowerDoor),
                new QuestLocation("Cast \"Shock Sphere\" (SHSP)", BT3.Spots.IK1_1100_BlackTowerDoor),
                new QuestLocation("Cast \"Word of Fear\" (FEAR)", BT3.Spots.IK1_1100_BlackTowerDoor),
                new QuestLocation("Cast \"Summon Elemental\" (SUEL)", BT3.Spots.IK1_1100_BlackTowerDoor),
                new QuestLocation("Cast \"Baylor's Spell Bind\" (SPBI)", BT3.Spots.IK1_1100_BlackTowerDoor),
                new QuestLocation("Enter the Black Tower", BT3.Spots.IK1_1100_BlackTowerDoor),
                new QuestLocation("Ascend to level 2", BT3.Spots.BT1_ToBlackTower2),
                new QuestLocation("Ascend to level 3", BT3.Spots.BT2_ToBlackTower3),
                new QuestLocation("Ascend to level 4", BT3.Spots.BT3_ToBlackTower4),
                new QuestLocation("Defeat the Black Wizards", BT3.Spots.BT4_BlackLens),
                new QuestLocation("Obtain \"Black Lens\"", BT3.Spots.BT4_BlackLens),
                new QuestLocation("Cast \"Elik's Instant Wolf\" (INWO)", BT3.Spots.IK1_0009_GreyTowerDoor),
                new QuestLocation("Cast \"Wind Hero\" (WIHE)", BT3.Spots.IK1_0009_GreyTowerDoor),
                new QuestLocation("Cast \"Fanskar's Force Focus\" (FOFO)", BT3.Spots.IK1_0009_GreyTowerDoor),
                new QuestLocation("Cast \"Kylearan's Invisibility Spell\" (INVI)", BT3.Spots.IK1_0009_GreyTowerDoor),
                new QuestLocation("Enter the Grey Tower", BT3.Spots.IK1_0009_GreyTowerDoor),
                new QuestLocation("Ascend to level 2", BT3.Spots.GT1_ToGreyTower2),
                new QuestLocation("Ascend to level 3", BT3.Spots.GT2_ToGreyTower3),
                new QuestLocation("Ascend to level 4", BT3.Spots.GT3_ToGreyTower4),
                new QuestLocation("Defeat the Grey Wizards", BT3.Spots.GT4_SmokeyLens),
                new QuestLocation("Obtain \"Smokey Lens\"", BT3.Spots.GT4_SmokeyLens),
                new QuestLocation("Place \"Crystal Lens\" in the crystal circle", BT3.Spots.IK1_0509_IceDungeonEnt),
                new QuestLocation("Place \"Black Lens\" in the black circle", BT3.Spots.IK1_0509_IceDungeonEnt),
                new QuestLocation("Place \"Smokey Lens\" in the smokey circle", BT3.Spots.IK1_0509_IceDungeonEnt),
                new QuestLocation("Enter the Ice Dungeon", BT3.Spots.IK1_0509_IceDungeonEnt),
                new QuestLocation("Descend to level 2", BT3.Spots.ID1_ToIceDungeon2),
                new QuestLocation("Answer the riddle (\"CALA\" or \"UTOR\")", BT3.Spots.ID2_RiddleCala),
                new QuestLocation("Obtain \"Wand of Power\"", BT3.Spots.ID2_SphereOfLanatir),
                new QuestLocation("Obtain \"Sphere of Lanatir\"", BT3.Spots.ID2_SphereOfLanatir));
            Sphere.Postrequisites.Add(new QuestLocation("Return to the Guild Elder", BT3.Spots.SB_ReviewBoard));
            AddMainQuest(iMainOrder++, Sphere, quests, "600000 Exp");

            Belt.AddLocations(new QuestLocation("Learn spells \"To Lucencia\" and \"From Lucencia\"", BT3.Spots.SB_ReviewBoard),
                new QuestLocation("Cast \"To Lucencia\" (LUCE)", BT3.Spots.W_ToLucencia),
                new QuestLocation("Climb the mountain", BT3.Spots.L_ToMountain1),
                new QuestLocation("Ascend to level 2", BT3.Spots.M1_ToMountain2),
                new QuestLocation("Defeat the Rainbow Dragon", BT3.Spots.M2_0500_DragonBlood),
                new QuestLocation("Obtain \"Crystal Key\"", BT3.Spots.M2_CrystalKey),
                new QuestLocation("Obtain a container", BT3.Spots.W_Tavern),
                new QuestLocation("Obtain \"Dragon Blood\"", BT3.Spots.M2_0500_DragonBlood),
                new QuestLocation("Enter Cyanis' Tower", BT3.Spots.L_CyanisTower1),
                new QuestLocation("Use \"Crystal Key\"", BT3.Spots.CT1_CrystalLock),
                new QuestLocation("Ascend to level 2", BT3.Spots.CT1_ToCyanisTower2),
                new QuestLocation("Ascend to level 3", BT3.Spots.CT2_ToCyanisTower3),
                new QuestLocation("Cast \"Flesh Anew\" (FLAN)", BT3.Spots.CT3_Cyanis),
                new QuestLocation("Obtain \"Magic Triangle\"", BT3.Spots.CT3_Cyanis),
                new QuestLocation("Obtain \"White Rose\"", BT3.Spots.L_WhiteRose),
                new QuestLocation("Obtain \"Blue Rose\"", BT3.Spots.L_BlueRose),
                new QuestLocation("Obtain \"Red Rose\"", BT3.Spots.L_RedRose),
                new QuestLocation("Obtain \"Yellow Rose\"", BT3.Spots.L_YellowRose),
                new QuestLocation("Use \"Dragon Blood\"", BT3.Spots.L_RainbowRose),
                new QuestLocation("Obtain \"Rainbow Rose\"", BT3.Spots.L_RainbowRose),
                new QuestLocation("Enter Alliria's Tomb", BT3.Spots.L_ToAlliriasTomb1),
                new QuestLocation("Use \"Magic Triangle\"", BT3.Spots.AT1_BlackCrystal),
                new QuestLocation("Ascend to level 2", BT3.Spots.AT1_ToAlliriasTomb2),
                new QuestLocation("Use \"White Rose\"", BT3.Spots.AT2_0400_WhiteRose),
                new QuestLocation("Use \"Blue Rose\"", BT3.Spots.AT2_0005_BlueRose),
                new QuestLocation("Use \"Red Rose\"", BT3.Spots.AT2_0608_RedRose),
                new QuestLocation("Use \"Yellow Rose\"", BT3.Spots.AT2_1000_YellowRose),
                new QuestLocation("Use \"Rainbow Rose\"", BT3.Spots.AT2_1004_RainbowRose),
                new QuestLocation("Obtain \"Crown of Truth\"", BT3.Spots.AT2_BeltOfAlliria),
                new QuestLocation("Obtain \"Belt of Alliria\"", BT3.Spots.AT2_BeltOfAlliria));
            Belt.Postrequisites.Add(new QuestLocation("Return to the Guild Elder", BT3.Spots.SB_ReviewBoard));
            AddMainQuest(iMainOrder++, Belt, quests, "600000 Exp");

            Helm.AddLocations(new QuestLocation("Learn spells \"To Kinestia\" and \"From Kinestia\"", BT3.Spots.SB_ReviewBoard),
                new QuestLocation("Cast \"To Kinestia\" (KINE)", BT3.Spots.F_ToKinestia),
                new QuestLocation("Answer the riddle (\"Iceberg\")", BT3.Spots.F_Hawkslayer),
                new QuestLocation("Recruit Hawkslayer", BT3.Spots.F_Hawkslayer),
                new QuestLocation("Enter the Barracks", BT3.Spots.F_ToBarracks),
                new QuestLocation("Obtain \"Right Key\"", BT3.Spots.B_RightKey),
                new QuestLocation("Enter the Private Quarters", BT3.Spots.F_ToPrivateQuarter1),
                new QuestLocation("Obtain \"Left Key\"", BT3.Spots.PQ_LeftKey),
                new QuestLocation("Talk to Ferofist", BT3.Spots.PQ_Ferofist),
                new QuestLocation("Enter the Workshop", BT3.Spots.F_ToWorkshop),
                new QuestLocation("Use \"Right Key\" and turn 18 times", BT3.Spots.W_ToUrmechsParadise),
                new QuestLocation("Use \"Left Key\" and turn 15 times", BT3.Spots.W_ToUrmechsParadise),
                new QuestLocation("Enter Urmech's Paradise", BT3.Spots.W_ToUrmechsParadise),
                new QuestLocation("Enter the Viscous Plane", BT3.Spots.UP_ToViscousPlane),
                new QuestLocation("Enter the Sanctum", BT3.Spots.VP_ToSanctum),
                new QuestLocation("Answer \"Yes\" to Urmech's question", BT3.Spots.S_Urmech),
                new QuestLocation("Obtain \"Hammer of Wrath\"", BT3.Spots.S_FerofistsHelm),
                new QuestLocation("Obtain \"Ferofist's Helm\"", BT3.Spots.S_FerofistsHelm));
            Helm.Postrequisites.Add(new QuestLocation("Return to the Guild Elder", BT3.Spots.SB_ReviewBoard));
            AddMainQuest(iMainOrder++, Helm, quests, "600000 Exp");

            Cloak.AddLocations(new QuestLocation("Learn spells \"To Tenabrosia\" and \"From Tenabrosia\"", BT3.Spots.SB_ReviewBoard),
                new QuestLocation("Cast \"To Tenabrosia\" (OLUK)", BT3.Spots.N_ToTenabrosia),
                new QuestLocation("Obtain a container", BT3.Spots.W_Tavern),
                new QuestLocation("Enter the Tar Quarry", BT3.Spots.N_ToTarQuarry),
                new QuestLocation("Obtain \"Molten Tar\"", BT3.Spots.TQ_MoltenTar),
                new QuestLocation("Enter the Dark Copse", BT3.Spots.N_ToDarkCopse),
                new QuestLocation("Use \"Molten Tar\" to remove trees", BT3.Spots.DC_ShadowDoor),
                new QuestLocation("Obtain \"Shadow Door\"", BT3.Spots.DC_ShadowDoor),
                new QuestLocation("Enter Shadow Canyon", BT3.Spots.N_ToShadowCanyon),
                new QuestLocation("Cast \"Phase Door\" or \"Wall Warp\"", BT3.Spots.SC_WeakWall1),
                new QuestLocation("Obtain \"Shadow Lock\"", BT3.Spots.SC_ShadowLock),
                new QuestLocation("Use \"Shadow Lock\"", BT3.Spots.N_ToSceadusDemesne),
                new QuestLocation("Use \"Shadow Door\"", BT3.Spots.N_ToSceadusDemesne),
                new QuestLocation("Enter Sceadu's Demesne", BT3.Spots.N_ToSceadusDemesne),
                new QuestLocation("Descend to level 2", BT3.Spots.SD1_ToSceadusDemesne2),
                new QuestLocation("Cast \"Phase Door\" or \"Wall Warp\"", BT3.Spots.SD2_PhasableDoor),
                new QuestLocation("Defeat Sceadu", BT3.Spots.SD2_Sceadu),
                new QuestLocation("Obtain \"Helm of Justice\"", BT3.Spots.M3_1520_Cloak),
                new QuestLocation("Obtain \"Sceadu's Cloak\"", BT3.Spots.M3_1520_Cloak));
            Cloak.Postrequisites.Add(new QuestLocation("Return to the Guild Elder", BT3.Spots.SB_ReviewBoard));
            AddMainQuest(iMainOrder++, Cloak, quests, "600000 Exp");

            Shield.AddLocations(new QuestLocation("Learn spells \"To Tarmitia\" and \"From Tarmitia\"", BT3.Spots.SB_ReviewBoard),
                new QuestLocation("Cast \"To Tarmitia\" (AECE)", BT3.Spots.W_ToTarmitia),
                new QuestLocation("Learn the name \"Ares\"", BT3.Spots.B_LearnAres),
                new QuestLocation("Go to Rome", BT3.Spots.B_ToRome),
                new QuestLocation("Go to Troy", BT3.Spots.R_ToTroy),
                new QuestLocation("Answer the riddle (\"ARES\")", BT3.Spots.T_ToNottinghamRiddle),
                new QuestLocation("Learn the name \"Yen-Lo-Wang\"", BT3.Spots.N_LearnYenLoWang),
                new QuestLocation("Go to Wasteland", BT3.Spots.N_ToWasteland),
                new QuestLocation("Go to K'un Wang", BT3.Spots.W_ToKunWang),
                new QuestLocation("Answer the riddle (\"YEN-LO-WANG\")", BT3.Spots.KW_ToWastelandRiddle),
                new QuestLocation("Learn the name \"Mars\"", BT3.Spots.W_LearnMars),
                new QuestLocation("Go to Berlin", BT3.Spots.W_ToBerlin),
                new QuestLocation("Go to Rome", BT3.Spots.B_ToRome),
                new QuestLocation("Answer the riddle (\"MARS\")", BT3.Spots.R_ToKunWangRiddle),
                new QuestLocation("Learn the name \"Susa-No-O\"", BT3.Spots.KW_LearnSusaNoO),
                new QuestLocation("Go to Stalingrad", BT3.Spots.KW_ToStalingrad),
                new QuestLocation("Go to Hiroshima", BT3.Spots.S_ToHiroshima),
                new QuestLocation("Answer the riddle (\"SUSA-NO-O\")", BT3.Spots.H_ToTroyRiddle),
                new QuestLocation("Learn the name \"Svarazic\"", BT3.Spots.T_LearnSvarazic),
                new QuestLocation("Go to K'un Wang", BT3.Spots.T_ToKunWang),
                new QuestLocation("Go to Stalingrad", BT3.Spots.KW_ToStalingrad),
                new QuestLocation("Answer the riddle (\"SVARAZIC\")", BT3.Spots.S_ToRomeRiddle),
                new QuestLocation("Learn the name \"St. George\"", BT3.Spots.R_LearnStGeorge),
                new QuestLocation("Go to Berlin", BT3.Spots.R_ToBerlin),
                new QuestLocation("Go to Nottingham", BT3.Spots.B_ToNottingham),
                new QuestLocation("Answer the riddle (\"ST. GEORGE\")", BT3.Spots.N_ToStalingradRiddle),
                new QuestLocation("Learn the name \"Sdiabm\"", BT3.Spots.S_LearnSdiabm),
                new QuestLocation("Go to Nottingham", BT3.Spots.S_ToNottingham),
                new QuestLocation("Go to Wasteland", BT3.Spots.N_ToWasteland),
                new QuestLocation("Answer the riddle (\"SDIABM\")", BT3.Spots.W_ToHiroshimaRiddle),
                new QuestLocation("Learn the name \"Tyr\"", BT3.Spots.H_LearnTyr),
                new QuestLocation("Go to Rome", BT3.Spots.H_ToRome),
                new QuestLocation("Go to Berlin", BT3.Spots.R_ToBerlin),
                new QuestLocation("Answer the riddle (\"TYR\" and \"WERRA\")", BT3.Spots.B_ToTarmitiaRiddle),
                new QuestLocation("Defeat Werra", BT3.Spots.TA_Werra1),
                new QuestLocation("Defeat Black Slayers", BT3.Spots.TA_Werra1),
                new QuestLocation("Obtain \"Werra's Shield\"", BT3.Spots.T_0001_Shield));
            Shield.Postrequisites.Add(new QuestLocation("Return to the Guild Elder", BT3.Spots.SB_ReviewBoard));
            AddMainQuest(iMainOrder++, Shield, quests);

            Tarjan.AddLocations(new QuestLocation("Learn spells \"To Malefia\" and \"From Malefia\"", BT3.Spots.SB_ReviewBoard),
                new QuestLocation("Cast \"To Malefia\" (EVIL)", BT3.Spots.W_ToMalefia),
                new QuestLocation("Obtain \"Strifespear\"", BT3.Spots.M1_1002_Strifespear),
                new QuestLocation("Go to the rooms near Valarian's statue", BT3.Spots.M2_1816_Bow),
                new QuestLocation("Use \"Valarian's Bow\" (any except Con/Mag/Sor/Wiz)", BT3.Spots.M2_1816_Bow),
                new QuestLocation("Go to the rooms near Lanatir's statue", BT3.Spots.M2_0912_Sphere),
                new QuestLocation("Use \"Sphere of Lanatir\" (must be a spellcaster)", BT3.Spots.M2_0912_Sphere),
                new QuestLocation("Go to the rooms near Alliria's statue", BT3.Spots.M1_0316_Belt),
                new QuestLocation("Use \"Belt of Alliria\" (any class)", BT3.Spots.M1_0316_Belt),
                new QuestLocation("Go to the rooms near Ferofist's statue", BT3.Spots.M3_Ferofist),
                new QuestLocation("Use \"Ferofist's Helm\" (War/Pal/Hun/Arc/Geo/Chr)", BT3.Spots.M3_Ferofist),
                new QuestLocation("Go to the rooms near Sceadu's statue", BT3.Spots.M3_1520_Cloak),
                new QuestLocation("Use \"Sceadu's Cloak\" (Rogue only)", BT3.Spots.M3_1520_Cloak),
                new QuestLocation("Go to the rooms near Werra's status", BT3.Spots.M3_1205_Shield),
                new QuestLocation("Use \"Werra's Shield\" (War/Pal/Hun/Bar/Arc/Geo/Chr)", BT3.Spots.M3_1205_Shield),
                new QuestLocation("Defeat the High Priestess", BT3.Spots.M3_HighPriestess),
                new QuestLocation("Defeat Redbeard", BT3.Spots.M3_Redbeard),
                new QuestLocation("Cast \"Phase Door\" or \"Wall Warp\"", BT3.Spots.T_NearTarjan),
                new QuestLocation("Defeat Lorini", BT3.Spots.T_Tarjan));
            Tarjan.Postrequisites.Add(new QuestLocation("Defeat Tarjan", BT3.Spots.T_Tarjan));
            AddMainQuest(iMainOrder++, Tarjan, quests);

            Spells.AddLocations(new QuestLocation("Learn \"Gilles Gills\" (500 Gold)", BT3.Spots.A_Fisherman),
                new QuestLocation("Learn \"Divine Intervention\" (50000 Gold)", BT3.Spots.CB_WizardGuild),
                new QuestLocation("Learn \"Gotterdamurung\" (50000 Gold)", BT3.Spots.BS_WizardGuild),
                new QuestLocation("Learn \"Kiel's Overture\" (60000 Gold)", BT3.Spots.CEB_BardsHall),
                new QuestLocation("Learn \"Minstrel Shield\" (30000 Gold)", BT3.Spots.BS_BardHall),
                new QuestLocation("Become a Geomancer", BT3.Spots.S_Geomancer));
            AddSideQuest(Spells, quests);

            quests.Sort(CompareBT3Quests);
            return quests.ToArray();
        }

        public int CompareBT3Quests(BT3Quest quest1, BT3Quest quest2)
        {
            return Global.CompareQuests(quest1, quest2);
        }

        private bool IsMap(BT3Map map, params BT3Map[] test) { return test.Any(m => m.Equals(map)); }
        private bool IsSpot(MapXY current, params MapXY[] test) { return test.Any(m => m.Equals(current)); }

        private int WhichMap(BT3Map map, params BT3Map[] maps)
        {
            for (int i = 0; i < maps.Length; i++)
            {
                if (maps[i] == map)
                    return i;
            }
            return -1;
        }

        private bool ScriptByte(BT3MapData data, int iScriptIndex, byte bCompare)
        {
            if (data == null || data.Scripts == null || data.Scripts.Length <= iScriptIndex - data.ScriptsOffset || iScriptIndex < data.ScriptsOffset)
                return false;
            return data.Scripts[iScriptIndex - data.ScriptsOffset] == bCompare;
        }

        public override void SetQuests(QuestData data, int iOverrideCharAddress)
        {
            BT3QuestData btData = data as BT3QuestData;
            if (btData == null)
                return;

            BTPartyInfo party = data.Party as BTPartyInfo;
            LocationInformation location = data.Location;
            BTGameState state = btData.State as BTGameState;

            if (iOverrideCharAddress == -1)
                iOverrideCharAddress = party.ActingChar;

            BT3Character bt3Char = BTCharacter.Create(state.Game, 0, party.Bytes, iOverrideCharAddress * party.CharacterSize) as BT3Character;

            if (!(data is BT3QuestData))
                return;

            BT3QuestData questData = data as BT3QuestData;
            BT3MapData bt3MapData = questData.Map as BT3MapData;

            QuestTotals totals = new QuestTotals(0, 0);

            CharName = bt3Char.Name;
            CharAddress = iOverrideCharAddress;

            PartyLocation = btData.Location.PrimaryCoordinates;
            MapXY spot = new MapXY(GameNames.BardsTale3, btData.Location.MapIndex, PartyLocation.X, PartyLocation.Y);
            BT3Map map = (BT3Map)spot.Map;
            bool bContainer = party.CurrentPartyHasItem(BTItemType.Container);
            int iSkaraMap = WhichMap(map, BT3Map.SkaraBraeRuins, BT3Map.Catacombs1, BT3Map.Catacombs2, BT3Map.Unterbrae1, BT3Map.Unterbrae2, BT3Map.Unterbrae3, BT3Map.Unterbrae4);
            bool bBrilhasti = bt3Char.HasAward(BT3AwardIndex.DefeatBrilhasti) || bt3Char.HasAward(BT3AwardIndex.BeginValariansBow);
            bool bArboria = IsMap(map, BT3Map.Arboria, BT3Map.CieraBrannia, BT3Map.CelariaBree, BT3Map.FesteringPit1, BT3Map.FesteringPit2, BT3Map.CrystalPalace,
                BT3Map.ValariansTower1, BT3Map.ValariansTower2, BT3Map.ValariansTower3, BT3Map.ValariansTower4, BT3Map.SacredGrove);
            bool bGelidia = IsMap(map, BT3Map.Gelidia, BT3Map.WhiteTower1, BT3Map.WhiteTower2, BT3Map.WhiteTower3, BT3Map.WhiteTower4,
                BT3Map.GreyTower1, BT3Map.GreyTower2, BT3Map.GreyTower3, BT3Map.GreyTower4,
                BT3Map.BlackTower1, BT3Map.BlackTower2, BT3Map.BlackTower3, BT3Map.BlackTower4,
                BT3Map.IceDungeon1, BT3Map.IceDungeon2, BT3Map.IceKeep1, BT3Map.IceKeep2);
            bool bLucencia = IsMap(map, BT3Map.Lucencia, BT3Map.Mountain1, BT3Map.Mountain2,
                BT3Map.CyanisTower1, BT3Map.CyanisTower2, BT3Map.CyanisTower3, BT3Map.AlliriasTomb1, BT3Map.AlliriasTomb2);
            bool bTarmitia = IsMap(map, BT3Map.Wasteland, BT3Map.Tarmitia, BT3Map.Berlin, BT3Map.Stalingrad, BT3Map.Hiroshima, BT3Map.Troy, BT3Map.Rome, BT3Map.Nottingham, BT3Map.KunWang);
            bool bMalefia = IsMap(map, BT3Map.Malefia1, BT3Map.Malefia2, BT3Map.Malefia3, BT3Map.Tarjan);
            bool bKinestia = IsMap(map, BT3Map.Barracks, BT3Map.Ferofists, BT3Map.PrivateQuarter, BT3Map.Workshop, BT3Map.UrmechsParadise, BT3Map.ViscousPlane, BT3Map.Sanctum);
            bool bSkaraBrae = IsMap(map, BT3Map.SkaraBraeRuins, BT3Map.Catacombs1, BT3Map.Catacombs2, BT3Map.Unterbrae1, BT3Map.Unterbrae2, BT3Map.Unterbrae3, BT3Map.Unterbrae4);
            bool bTenabrosia = IsMap(map, BT3Map.Nowhere, BT3Map.DarkCopse, BT3Map.BlackScar, BT3Map.TarQuarry, BT3Map.ShadowCanyon, BT3Map.SceadusDemesne1, BT3Map.SceadusDemesne2);

            Brilhasti.AddObj(
                bBrilhasti || iSkaraMap >= 0,
                bBrilhasti || bt3Char.HasAward(BT3AwardIndex.TalkElder1),
                bBrilhasti || iSkaraMap > 0,
                bBrilhasti || iSkaraMap > 1,
                false,
                bBrilhasti || iSkaraMap > 2,
                bBrilhasti || iSkaraMap > 3 || IsSpot(spot, BT3.Spots.U1_ToUnterbrae2),
                bBrilhasti || iSkaraMap > 3,
                bBrilhasti || iSkaraMap > 4 || (iSkaraMap == 4 && questData.HasDoor(BT3.Spots.U2_RiddleShadow, Direction.Left)),
                bBrilhasti || iSkaraMap > 4,
                bBrilhasti || iSkaraMap > 5 || (iSkaraMap == 5 && questData.HasDoor(BT3.Spots.U3_RiddleSword, Direction.Left)),
                bBrilhasti || iSkaraMap > 5,
                bBrilhasti);
            Brilhasti.AddPost(bt3Char.HasAward(BT3AwardIndex.BeginValariansBow));
            Brilhasti.Obj[4] = QuestStatus.Single.ManualNotCompleted;
            AddQuest(totals, Brilhasti);

            bool bTalkedKing = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.TalkKing_CB0811);
            bool bWaterOfLife = party.CurrentPartyHasItem(BTItemType.Container, BT3ItemFlags.FilledWaterOfLife);
            int iValariansTower = WhichMap(map, BT3Map.ValariansTower1, BT3Map.ValariansTower2, BT3Map.ValariansTower3, BT3Map.ValariansTower4);
            bool bNightspear = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetNightspear_VT40304);
            bool bHasNightspear = party.CurrentPartyHasItem(BT3ItemIndex.Nightspear);
            bool bHead = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetTslothasHead_FP20211);
            bool bHasHead = party.CurrentPartyHasItem(BT3ItemIndex.TslothasHead);
            bool bHeart = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetTslothasHeart_FP20211);
            bool bHasHeart = party.CurrentPartyHasItem(BT3ItemIndex.TslothasHeart);
            bool bTslotha = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.DefeatTslotha_FP20211);
            bool bGaveHead = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GiveTslothasHead_CB0811);
            bool bBow = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetBowChamber_SG0900);
            bool bWateredAcorn = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.WaterAcorn_VT30102);

            Bow.AddObj(
                party.CurrentPartyHasClass(GenericClass.Chronomancer),
                bBow || bArboria,
                bBow || party.CurrentPartyHasCharacter("Hawkslayer", GenericClass.Monster),
                bBow || bWaterOfLife || party.CurrentPartyHasItem(BT3ItemIndex.Arefolia),
                bBow || party.CurrentPartyKnowsSpell(BT3SpellIndex.GillesGills),
                bBow || bTslotha || party.CurrentPartyHasItem(BT3ItemIndex.Acorn),
                bTalkedKing || map == BT3Map.CieraBrannia,
                bTalkedKing,
                bBow || bWaterOfLife || bWateredAcorn || map == BT3Map.CrystalPalace,
                bBow || bWaterOfLife || bWateredAcorn || bContainer,
                bBow || bWaterOfLife || bWateredAcorn,
                bGaveHead || bTslotha || bNightspear || iValariansTower >= 0,
                bGaveHead || bTslotha || bNightspear || iValariansTower >= 1,
                bGaveHead || bTslotha || bNightspear || iValariansTower >= 2,
                bGaveHead || bTslotha || bNightspear || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.PlantAcorn_VT30102),
                bGaveHead || bTslotha || bNightspear || bWateredAcorn,
                bGaveHead || bTslotha || bNightspear || iValariansTower >= 3,
                bGaveHead || bTslotha || bNightspear,
                bGaveHead || bTslotha || bHasNightspear,
                bGaveHead || bTslotha || map == BT3Map.FesteringPit1 || map == BT3Map.FesteringPit2,
                bGaveHead || bTslotha || map == BT3Map.FesteringPit2,
                bGaveHead || bTslotha,
                bGaveHead || bHead,
                bGaveHead || bHasHead,
                bGaveHead || bHeart,
                bGaveHead || bHasHeart,
                bGaveHead,
                bBow || map == BT3Map.SacredGrove,
                bBow || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseHeartValarian_SG0403),
                bBow || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseWaterValarian_SG0403),
                bBow || map == BT3Map.SacredGrove && BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.SacredFlameOpensDoor_SG0403),
                bBow,
                BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetArrowsChamber_SG0900));
            Bow.AddPost(bt3Char.HasAward(BT3AwardIndex.FinishValariansBow));

            FlameCounter = map == BT3Map.SacredGrove ? questData.Effects.Counter(0) : -1;

            int iWhiteTower = WhichMap(map, BT3Map.WhiteTower1, BT3Map.WhiteTower2, BT3Map.WhiteTower3, BT3Map.WhiteTower4);
            int iGreyTower = WhichMap(map, BT3Map.GreyTower1, BT3Map.GreyTower2, BT3Map.GreyTower3, BT3Map.GreyTower4);
            int iBlackTower = WhichMap(map, BT3Map.BlackTower1, BT3Map.BlackTower2, BT3Map.BlackTower3, BT3Map.BlackTower4);
            bool bKeep = bGelidia && map != BT3Map.Gelidia;
            bool bSphere = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetSphere_ID20400);
            bool bCrystalLens = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetCrystalLens_WT40400);
            bool bSmokeyLens = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetSmokeyLens_GT40203);
            bool bBlackLens = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetBlackLens_BT40304);

            Sphere.AddObj(
                bSphere || party.CurrentPartyKnowsSpell(BT3SpellIndex.ToGelidia),
                bSphere || bGelidia,
                false,
                bSphere || bKeep,
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.LEVIWhiteTowerDoor_IK11109),
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.ANMAWhiteTowerDoor_IK11109),
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.PHDOWhiteTowerDoor_IK11109),
                bSphere || bCrystalLens || iWhiteTower >= 0,
                bSphere || bCrystalLens || iWhiteTower >= 1,
                bSphere || bCrystalLens || iWhiteTower >= 2,
                bSphere || bCrystalLens || iWhiteTower >= 3,
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.DefeatWhiteWizard_WT40400),
                bSphere || bCrystalLens,
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.MAFLBlackTowerDoor_IK11100),
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.SHSPBlackTowerDoor_IK11100),
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.FEARBlackTowerDoor_IK11100),
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.SUELBlackTowerDoor_IK11100),
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.SPBIBlackTowerDoor_IK11100),
                bSphere || bBlackLens || iBlackTower >= 0,
                bSphere || bBlackLens || iBlackTower >= 1,
                bSphere || bBlackLens || iBlackTower >= 2,
                bSphere || bBlackLens || iBlackTower >= 3,
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.DefeatBlackWizard_BT40304),
                bSphere || bBlackLens,
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.INWOGreyTowerDoor_IK10009),
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.WIHEGreyTowerDoor_IK10009),
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.FOFOGreyTowerDoor_IK10009),
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.INVIGreyTowerDoor_IK10009),
                bSphere || bSmokeyLens || iGreyTower >= 0,
                bSphere || bSmokeyLens || iGreyTower >= 1,
                bSphere || bSmokeyLens || iGreyTower >= 2,
                bSphere || bSmokeyLens || iGreyTower >= 3,
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.DefeatGreyWizard_GT40203),
                bSphere || bSmokeyLens,
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseCrystalLens_IK10509),
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseBlackLens_IK10509),
                bSphere || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseSmokeyLens_IK10509),
                bSphere || map == BT3Map.IceDungeon1 || map == BT3Map.IceDungeon2,
                bSphere || map == BT3Map.IceDungeon2,
                bSphere || map == BT3Map.IceDungeon2 && btData.HasDoor(BT3.Spots.ID2_LanatirDoor, Direction.Down),
                BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetWand_ID20400),
                bSphere);
            Sphere.AddPost(bt3Char.HasAward(BT3AwardIndex.FinishLanatirSphere));
            Sphere.Obj[2] = QuestStatus.Single.ManualNotCompleted;

            int iCyanisTower = WhichMap(map, BT3Map.CyanisTower1, BT3Map.CyanisTower2, BT3Map.CyanisTower3);
            int iMountain = WhichMap(map, BT3Map.Mountain1, BT3Map.Mountain2);
            bool bBelt = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetBelt_AT20905);
            bool bCrystalKey = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetCrystalKey_M20500);
            bool bTriangle = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetTriangle_CT30302);
            bool bWhiteRose = party.CurrentPartyHasItem(BT3ItemIndex.WhiteRose);
            bool bYellowRose = party.CurrentPartyHasItem(BT3ItemIndex.YellowRose);
            bool bBlueRose = party.CurrentPartyHasItem(BT3ItemIndex.BlueRose);
            bool bRedRose = party.CurrentPartyHasItem(BT3ItemIndex.RedRose);
            bool bRainbowRose = party.CurrentPartyHasItem(BT3ItemIndex.RainbowRose);
            bool bGiveWhiteRose = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GiveWhiteRose_AT20400);
            bool bGiveYellowRose = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GiveYellowRose_AT21000);
            bool bGiveBlueRose = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GiveBlueRose_AT20005);
            bool bGiveRedRose = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GiveRedRose_AT20508);
            bool bGiveRainbowRose = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GiveRainbowRose_AT21004);
            bool bBlood = party.CurrentPartyHasItem(BTItemType.Container, BT3ItemFlags.FilledDragonBlood);

            Belt.AddObj(
                bBelt || party.CurrentPartyKnowsSpell(BT3SpellIndex.ToLucencia),
                bBelt || bLucencia,
                bBelt || bCrystalKey || iMountain >= 0,
                bBelt || bCrystalKey || iMountain >= 1,
                bBelt || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.DefeatDragon_M20500),
                bBelt || bCrystalKey,
                bBelt || bContainer || bBlood || bRainbowRose || bGiveRainbowRose,
                bBelt || bBlood || bRainbowRose || bGiveRainbowRose,
                bBelt || bTriangle || iCyanisTower >= 0,
                bBelt || bTriangle || iCyanisTower >= 1 || (iCyanisTower == 0 && questData.HasDoor(BT3.Spots.CT1_CrystalLock, Direction.Up)),
                bBelt || bTriangle || iCyanisTower >= 1,
                bBelt || bTriangle || iCyanisTower >= 2,
                bBelt || bTriangle || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.FLANCyanis_CT30302) ||
                    BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.DefeatCyanis_CT30302),
                bTriangle,
                bBelt || bWhiteRose || bGiveWhiteRose,
                bBelt || bBlueRose || bGiveBlueRose,
                bBelt || bRedRose || bGiveRedRose,
                bBelt || bYellowRose || bGiveYellowRose,
                bBelt || bRainbowRose || bGiveRainbowRose || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseDragonBlood_L0608),
                bBelt || bRainbowRose || bGiveRainbowRose,
                bBelt || map == BT3Map.AlliriasTomb1 || map == BT3Map.AlliriasTomb2,
                bBelt || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseTriangle_AT10615),
                bBelt || map == BT3Map.AlliriasTomb2,
                bBelt || bGiveWhiteRose,
                bBelt || bGiveBlueRose,
                bBelt || bGiveRedRose,
                bBelt || bGiveYellowRose,
                bBelt || bGiveRainbowRose,
                bBelt || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetCrown_AT20905),
                bBelt);
            Belt.AddPost(bt3Char.HasAward(BT3AwardIndex.FinishBeltOfAlliria));

            bool bHelm = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetHelm_S0703);
            bool bRightKey = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetRightKey_B1102);
            bool bLeftKey = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetLeftKey_PQ0811);
            bool bUsedLeftKey = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseLeftKey_W0202);
            bool bUsedRightKey = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseRightKey_W0202);
            bool bDefeatUrmech = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.DefeatUrmech_S0607);

            Helm.AddObj(
                bHelm || party.CurrentPartyKnowsSpell(BT3SpellIndex.ToKinestia),
                bHelm || bKinestia,
                bHelm || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.TalkHawkslayer_F0302),
                bHelm || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetHawkslayer_F0302),
                bHelm || map == BT3Map.Barracks || bRightKey,
                bHelm || bRightKey,
                bHelm || map == BT3Map.PrivateQuarter || bLeftKey,
                bHelm || bLeftKey,
                bHelm || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.TalkFerofist_PQ0215),
                bHelm || map == BT3Map.Workshop || map == BT3Map.UrmechsParadise || map == BT3Map.ViscousPlane || map == BT3Map.Sanctum,
                bHelm || bUsedRightKey,
                bHelm || bUsedLeftKey,
                bHelm || map == BT3Map.UrmechsParadise || map == BT3Map.ViscousPlane || map == BT3Map.Sanctum,
                bHelm || map == BT3Map.ViscousPlane || map == BT3Map.Sanctum,
                bHelm || map == BT3Map.Sanctum,
                bHelm || bDefeatUrmech || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.SpareUrmech_S0602),
                BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetHammer_S0703),
                bHelm);
            Helm.AddPost(bt3Char.HasAward(BT3AwardIndex.FinishFerofistsHelm));

            bool bCloak = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetCloak_SD20513);
            bool bShadowDoor = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetShadowDoor_DC0505);
            bool bShadowLock = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetShadowLock_SC0307);
            bool bUsedDoor = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseShadowDoor_N0505);
            bool bUsedLock = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseShadowLock_N0505);
            bool bDefeatSceadu = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.DefeatSceadu_SD20513);
            bool bTar = party.CurrentPartyHasItem(BTItemType.Container, BT3ItemFlags.FilledMoltenTar);

            Cloak.AddObj(
                bCloak || party.CurrentPartyKnowsSpell(BT3SpellIndex.ToTenabrosia),
                bCloak || bTenabrosia,
                bCloak || bContainer || bShadowDoor,
                bCloak || bTar || bShadowDoor || map == BT3Map.TarQuarry,
                bCloak || bTar || bShadowDoor,
                bCloak || bShadowDoor || map == BT3Map.DarkCopse,
                bCloak || bShadowDoor,
                bCloak || bShadowDoor,
                bCloak || bShadowLock || map == BT3Map.ShadowCanyon,
                bCloak || bShadowLock || (map == BT3Map.ShadowCanyon && btData.HasFlag(PartyLocation, BT3MapSpecials.NoTeleport)),
                bCloak || bShadowLock,
                bCloak || bUsedLock,
                bCloak || bUsedDoor,
                bCloak || map == BT3Map.SceadusDemesne1 || map == BT3Map.SceadusDemesne2,
                bCloak || map == BT3Map.SceadusDemesne2,
                bCloak || Global.PointInRects(PartyLocation, 0, 11, 15, 4),
                bCloak || bDefeatSceadu,
                BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetJustice_SD20513),
                bCloak);
            Cloak.AddPost(bt3Char.HasAward(BT3AwardIndex.FinishSceadusCloak));

            bool bShield = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetShield_T0001);
            bool bDefeatWerra = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.KillWerra_T0000);

            bool bTarmitiaProper = (bShield || map == BT3Map.Tarmitia);
            Shield.AddObj(
                bTarmitiaProper || party.CurrentPartyKnowsSpell(BT3SpellIndex.ToTarmitia),
                bTarmitiaProper || bTarmitia,
                bTarmitiaProper, bTarmitiaProper, bTarmitiaProper, bTarmitiaProper,
                bTarmitiaProper, bTarmitiaProper, bTarmitiaProper, bTarmitiaProper,
                bTarmitiaProper, bTarmitiaProper, bTarmitiaProper, bTarmitiaProper,
                bTarmitiaProper, bTarmitiaProper, bTarmitiaProper, bTarmitiaProper,
                bTarmitiaProper, bTarmitiaProper, bTarmitiaProper, bTarmitiaProper,
                bTarmitiaProper, bTarmitiaProper, bTarmitiaProper, bTarmitiaProper,
                bTarmitiaProper, bTarmitiaProper, bTarmitiaProper, bTarmitiaProper,
                bTarmitiaProper, bTarmitiaProper, bTarmitiaProper, bTarmitiaProper,
                bShield || bDefeatWerra,
                bShield || bDefeatWerra,
                bShield);
            Shield.AddPost(bt3Char.HasAward(BT3AwardIndex.FinishWerrasShield));
            if (!bTarmitiaProper)
            {
                for (int i = 2; i < 33; i++)
                    Shield.Obj[i] = QuestStatus.Single.ManualNotCompleted;
            }

            Point pt = PartyLocation;
            bool bTarjan = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.DefeatTarjan_T0302);
            bool bValarianStatue = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseBow_M21816);
            bool bLanatirStatue = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseSphere_M20912);
            bool bAlliriaStatue = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseBelt_M10316);
            bool bFerofistStatue = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseHelm_M32102);
            bool bSceaduStatue = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseCloak_M31520);
            bool bWerraStatue = BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.UseShield_M31205);
            bool bValarianArea = map == BT3Map.Malefia2 && Global.PointInRects(pt, 18,13,4,4, 21,12,1,1);
            bool bLanatirArea = map == BT3Map.Malefia2 && Global.PointInRects(pt, 8,12,3,3);
            bool bAlliriaArea = map == BT3Map.Malefia1 && ((pt.X <= 9 && pt.Y >= (pt.X + 12)) || (pt.X <= 7 && pt.Y >= 18 - pt.X));
            bool bFerofistArea = map == BT3Map.Malefia3 && Global.PointInRects(pt, 17,2,5,5, 15,2,2,2);
            bool bSceaduArea = map == BT3Map.Malefia3 && Global.PointInRects(pt, 11,0,4,4, 15,0,7,2, 15,16,7,6, 18,12,4,4);
            bool bWerraArea = map == BT3Map.Malefia3 && Global.PointInRects(pt, 11,4,6,4) && !Global.PointInRects(pt, 15,7,2,1);
            bool bM3Central = map == BT3Map.Malefia3 && Global.PointInRects(pt, 7,8,8,7);

            Tarjan.AddObj(
                bTarjan || party.CurrentPartyKnowsSpell(BT3SpellIndex.ToMalefia),
                bTarjan || map == BT3Map.Malefia1 || map == BT3Map.Malefia2 || map == BT3Map.Malefia3 || map == BT3Map.Tarjan,
                bTarjan || BT3Bits.IsSet(questData.QuestBits, BT3Bits.Scripts.GetStrifespear_M11002),
                bTarjan || bValarianStatue || bValarianArea,
                bTarjan || bValarianStatue,
                bTarjan || bLanatirStatue || bLanatirArea,
                bTarjan || bLanatirStatue,
                bTarjan || bAlliriaStatue || bAlliriaArea,
                bTarjan || bAlliriaStatue,
                bTarjan || bFerofistStatue || bFerofistArea,
                bTarjan || bFerofistStatue,
                bTarjan || bSceaduStatue || bSceaduArea,
                bTarjan || bSceaduStatue,
                bTarjan || bWerraStatue || bWerraArea,
                bTarjan || bWerraStatue,
                bTarjan || bM3Central || map == BT3Map.Tarjan,
                bTarjan || map == BT3Map.Tarjan,
                bTarjan || IsSpot(spot, BT3.Spots.T_Tarjan),
                bTarjan);
            Tarjan.AddPost(bt3Char.HasAward(BT3AwardIndex.FinishDefeatTarjan));

            Spells.Main = QuestStatus.Basic.Accepted;
            Spells.AddObj(
                party.CharacterKnowsSpell(CharAddress, BT3SpellIndex.GillesGills),
                party.CharacterKnowsSpell(CharAddress, BT3SpellIndex.DivineIntervention),
                party.CharacterKnowsSpell(CharAddress, BT3SpellIndex.Gotterdamurung),
                party.CharacterKnowsSpell(CharAddress, BT3SpellIndex.KielsOverture),
                party.CharacterKnowsSpell(CharAddress, BT3SpellIndex.MinstrelShield),
                party.CharacterIsClass(CharAddress, GenericClass.Geomancer));
            for (int i = 0; i < 3; i++)
            {
                if (!party.CharacterIsCaster(CharAddress))
                    Spells.Obj[i] = QuestStatus.Single.Invalid(String.Format("A {0} may not learn this spell", BaseCharacter.ClassString(bt3Char.BasicClass)));
            }
            for (int i = 3; i < 5; i++)
            {
                if (!party.CharacterIsClass(CharAddress, GenericClass.Bard))
                    Spells.Obj[i] = QuestStatus.Single.Invalid(String.Format("Only a Bard may learn songs"));
            }
            if (party.CharacterIsCaster(CharAddress) && !party.CharacterIsClass(CharAddress, GenericClass.Geomancer))
                Spells.Obj[5] = QuestStatus.Single.Invalid(String.Format("A {0} may not become a Geomancer", BaseCharacter.ClassString(bt3Char.BasicClass)));
            else if (bDefeatUrmech)
                Spells.Obj[5] = QuestStatus.Single.Unachievable("The party has murdered Urmech and cannot use the Geomancer machinery");

            TotalQuests = totals.All;
            CompletedQuests = totals.Completed;
        }

        private bool IsActive(BT3QuestData data, BT3Map map, MapXY spot) { return (int)map == spot.Map && data.IsActive(spot.Location); }
        private bool IsInactive(BT3QuestData data, BT3Map map, MapXY spot) { return (int)map == spot.Map && !data.IsActive(spot.Location); }
    }
}
