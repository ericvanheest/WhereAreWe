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
    public class Wiz5Memory : WizMemory
    {
        // Search for "WIZ5.DSK is missing"
        public override byte[] MainSearch { get { return new byte[] { 0x57, 0x49, 0x5A, 0x35, 0x2E, 0x44, 0x53, 0x4B, 0x20, 0x69, 0x73, 0x20, 0x6D, 0x69, 0x73, 0x73, 0x69, 0x6E, 0x67 }; } }

        public override int MainBlockSVN { get { return -7514; } }
        public override int MainBlockOldSVN { get { return -7146; } }
        public override int MainBlockNonSVN { get { return -7514; } }

        public override int PartyInfo { get { return 60930; } }
        public override int State1 { get { return 77458; } }
        public override int State2 { get { return 59014; } }

        public override int Facing { get { return 58904; } }         // Int16
        public override int LocationDown { get { return 58906; } }   // Int16
        public override int LocationNorth { get { return 58908; } }  // Int16
        public override int LocationEast { get { return 58910; } }   // Int16
        public override int NumChars { get { return 58896; } }       // Int16
        public override int InspectingChar { get { return 49310; } }

        public override int Map { get { return 45976; } }
        public override int EncounterInfo { get { return 52938; } }
        public override int CombatCharInfo { get { return 52938; } }
        public override int Text { get { return 31414; } }

        public override int CreateName { get { return 48016; } }
        public override int CreateBonus { get { return 47852; } }
        public override int CreateAttributes { get { return 48374; } }  // 6 Int16s
        public override int CreationSelectedStat { get { return 47908; } }
        public override int CreationSelectedRace { get { return 48048; } }
        public override int Light { get { return 58976; } }
        public override int ACBonus { get { return 58982; } }
        public override int CreateGold { get { return 48068; } }
        public override int TrainingChar { get { return 48280; } }
        public override int TrapType { get { return 44668; } }
        public override int TreasureList { get { return 17515; } }      // 54 bytes each, compressed
        public override int RewardIndex { get { return 44690; } }
        public override int EncounterRewardModifier { get { return 58966; } }
        public override int Identify { get { return 58974; } }  // Latumapic
        public override int CombatOptionActiveChar { get { return 43428; } }

        // Unfinished:
        public override int FightMap { get { return 46584; } }

        public override int AllMaps { get { return 204134; } }
        public override int MonsterListDisk { get { return 210278; } }
        public override int ItemList { get { return 180070; } }
        public override int State3 { get { return 38868; } }
        public override int State4 { get { return 38412; } }
        public override int State5 { get { return 64866; } }
        public override int TimeDelay { get { return 48672; } }
        public override int ShoppingChar { get { return 42200; } }
        public override int TrapType2 { get { return 43280; } }


        // Specific to Wiz5:
        public int InventoryDisplay { get { return 49350; } }
        public int EquipItemBytes { get { return 52198; } }
        public int MapBlockPosY { get { return 46488; } }  // 16 bytes
        public int MapBlockPosX { get { return 46504; } }  // 16 bytes
        public int MapDarkBits { get { return 59840; } }  // 128 Int16s (2048 bits, half of which are used)
        public int ActiveSquares { get { return 59584; } }  // 128 Int16s (2048 bits, half of which are used)
        public int Group0 { get { return 52938; } }
        public int Group1 { get { return 53248; } }
        public int Group2 { get { return 53558; } }
        public int Monster1_1 { get { return 53260; } }
        public int L1_15x23_Monster { get { return 47446; } }
        public int Monster1Bytes { get { return 53440; } }
        public int MapSpecials { get { return 46898; } }
        public int MapSpecialsY { get { return 46946; } }
        public int MapSpecialsX { get { return 46986; } }
        public int MapSpecialsSub8 { get { return 47026; } }
        public int MapAux0 { get { return 47074; } }
        public int MapAux1 { get { return 47202; } }
        public int MapAux2 { get { return 47362; } }
        public int MapAux3 { get { return 47458; } }
        public int BuyFromNPC { get { return 46244; } }
        public int BuyInventory { get { return 46348; } }
        public int BoltacCurrent { get { return 50198; } }
        public int ActivatedBits { get { return 60864; } }
        public int InaccessibleBits { get { return 60096; } }  // 128 Int16s
        public int SummonedMonsters { get { return 54488; } }
        public int Levitate { get { return 58972; } }
        public int MagicScreen { get { return 52946; } }
        public int InnNeedExp { get { return 47552; } }
        public int CurrentTreasure { get { return 44698; } }  // 114 bytes
        public int FizzleFieldStrength { get { return 52948; } }
        public int SummonedMonsterIndex { get { return 54494; } }
        
        public override MemoryGuess[] Guesses { get { return Global.MemoryGuesses.Guesses[GameNames.Wizardry4]; } }
    }

    public enum Wiz5Map
    {
        None = -1,
        First = 0,
        Castle = 0,
        MazeLevel1 = 1,
        MazeLevel2 = 2,
        MazeLevel3 = 3,
        MazeLevel4 = 4,
        MazeLevel5 = 5,
        MazeLevel6 = 6,
        MazeLevel7 = 7,
        MazeLevel8 = 8,
        Last,
        Unknown = -1
    }

    public class Wiz5SpellInfo : WizSpellInfo
    {
        public Wiz5Spell Spell;

        public Wiz5SpellInfo() : base() { Spell = null; }
    }

    public class Wiz5PartyInfo : WizPartyInfo
    {
        public Wiz5PartyInfo(byte[] bytes, byte numchars) : base(bytes, numchars) { }

        public override byte[] QuestBytes
        {
            get
            {
                if (Bytes == null)
                    return null;
                MemoryStream stream = new MemoryStream();
                for (int i = 0; i < NumChars; i++)
                {
                    stream.WriteByte(Bytes[i * CharacterSize + Wiz5.Offsets.Level]);
                    stream.WriteByte(Bytes[i * CharacterSize + Wiz5.Offsets.Alignment]);
                    stream.Write(Bytes, i * CharacterSize + Wiz5.Offsets.Awards, Wiz5.Offsets.AwardsLength);
                    stream.Write(Bytes, i * CharacterSize + Wiz5.Offsets.Inventory, Wiz5.Offsets.InventoryLength);
                }
                stream.WriteByte(ActingChar);
                return stream.ToArray();
            }
        }

        public override int CharacterSize { get { return Wiz5Character.SizeInBytes; } }
    }

    public class Wiz5GameInfo : Wiz1234GameInfo
    {
        public byte[][] ActivatedBits;
        public UInt16 Walls;
        public int Levitate;
        public int MagicScreen;
        public int FizzleField;
        public UInt16 State1;
        public UInt16 State2;
        public UInt16 SummonedMonster;

        public override GameNames Game { get { return GameNames.Wizardry5; } }

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
                foreach(byte[] bits in ActivatedBits)
                    Global.WriteBytes(ms, bits);
                ms.WriteByte((byte) SummonedMonster);
                return ms.ToArray();
            }
        }

        public override List<GameInfoItem> GetEffectItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();

            items.Add(new Wiz5GameInfoItem("Light", (Int16) Light, new OffsetList(Wiz5.Memory.Light)));
            items.Add(new Wiz5GameInfoItem("Levitate", (Int16)Levitate, new OffsetList(Wiz5.Memory.Levitate)));
            items.Add(new Wiz5GameInfoItem("Identify", (Int16)Identify, new OffsetList(Wiz5.Memory.Identify)));
            items.Add(new Wiz5GameInfoItem("AC Bonus", (Int16)ACBonus, new OffsetList(Wiz5.Memory.ACBonus)));

            if (InCombat)
            {
                items.Add(new Wiz5GameInfoItem("Magic Screen", (Int16)MagicScreen, new OffsetList(Wiz5.Memory.MagicScreen)));
                items.Add(new Wiz5GameInfoItem("Fizzle Field", (Int16)FizzleField, new OffsetList(Wiz5.Memory.FizzleFieldStrength)));
            }

            if (Global.Debug)
            {
                items.Add(new Wiz5GameInfoItem("State1", State1, new OffsetList(Wiz5.Memory.State1)));
                items.Add(new Wiz5GameInfoItem("State2", State2, new OffsetList(Wiz5.Memory.State2)));
            }

            return items;
        }

        public static BitDesc ActivatedBitsDesc(object val)
        {
            if (!(val is int))
                return BitDesc.Empty;

            int iBit = ((int)val / 8) * 8 + (7 - (((int)val) % 8));
            switch (iBit)
            {
                // Level 1
                case 0 + 0: return new BitDesc("", MapXY.Empty, "All unassigned scripts");
                case 0 + 1: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L1_8D85_Portal, "Use \"Bag of Tokens\"");
                case 0 + 2: return new BitDesc("Enter DBCA (stop motor)", Wiz5.Spots.L1_8785_Motor, "Enter the square");
                case 0 + 3: return new BitDesc("Enter the level", MapXY.Empty);
                case 0 + 4: return new BitDesc("Enter the level", MapXY.Empty);
                case 0 + 6: return new BitDesc("Enter the level", MapXY.Empty);
                case 0 + 7: return new BitDesc("Enter the level", MapXY.Empty);
                case 0 + 8: return new BitDesc("Enter the level", MapXY.Empty);
                case 0 + 9: return new BitDesc("Answer VAMPIRE riddle (statue moves away)", Wiz5.Spots.L1_959C_Vampire, "Enter the square");
                case 0 + 10: return new BitDesc("Enter the level", MapXY.Empty);
                case 0 + 11: return new BitDesc("Search (Receive \"Orb of Llylgamyn\")", Wiz5.Spots.L1_938A_Orb, "Enter the square");
                case 0 + 12: return new BitDesc("Read message", Wiz5.Spots.L1_8E98_Sign, "Enter the square");
                case 0 + 13: return new BitDesc("Encounter \"Golem\"", Wiz5.Spots.L1_9098_Golem, "Enter the square");
                case 0 + 14: return new BitDesc("Encounter \"G'bli Gedook\"", Wiz5.Spots.L1_8999_Gbli, "Enter the square");
                case 0 + 15: return new BitDesc("Enter the square (activate \"Ironose\")", Wiz5.Spots.L1_969C_Ironose);
                case 0 + 16: return new BitDesc("Use \"Brass Key\" (unlocks north door)", Wiz5.Spots.L1_8784_MaintDoor, "Enter the square");
                case 0 + 17: return new BitDesc("Use \"Silver Key\" (unlocks silver door)", Wiz5.Spots.L1_879C_Silver, "Enter the square");
                case 0 + 18: return new BitDesc("Encounter \"Zombie\" (receive \"Bag of Tokens\")", Wiz5.Spots.L1_8585_Zombie, "Enter the square");
                case 0 + 19: return new BitDesc("Inspect Hidden Items (receive \"Silver Key\")", Wiz5.Spots.L1_9C8F_SilverKey, "Enter the square");
                case 0 + 20: return new BitDesc("Answer VAMPIRE riddle (create east door)", Wiz5.Spots.L1_959C_Vampire, "Enter the square");
                case 0 + 24: return new BitDesc("Enter the level", MapXY.Empty);
                case 0 + 31: return new BitDesc("Enter the level", MapXY.Empty);
                case 0 + 34: return new BitDesc("Encounter \"LaLa Moo-Moo\"", Wiz5.Spots.L1_1818_Lala, "Enter the square");
                case 0 + 36: return new BitDesc("Read message", Wiz5.Spots.L1_947B_Sign, "Enter the square");
                case 0 + 38: return new BitDesc("Enter the square (unlocks the east door)", Wiz5.Spots.L1_8E8C_Unlock, "Enter the square");

                // Level 2
                case 64 + 1: return new BitDesc("Enter the square (activate \"Hurkle Beast\")", Wiz5.Spots.L2_8279_Portal, "Enter the square");
                case 64 + 2: return new BitDesc("Search (receive \"Bottle of Rum\")", Wiz5.Spots.L2_7E73_Rum, "Enter the square");
                case 64 + 3: return new BitDesc("Give \"Bottle of Rum\" or defeat \"Ruby Warlock\"", Wiz5.Spots.L2_7A84_Ruby, "Enter the square");
                case 64 + 4: return new BitDesc("Use \"Hacksaw\" (unlock door)", Wiz5.Spots.L2_8372_Chains, "Enter the square");
                case 64 + 5: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L2_8372_Chains);
                case 64 + 6: return new BitDesc("Read message", Wiz5.Spots.L2_8381_Sign, "Enter the square");
                case 64 + 7: return new BitDesc("Inspect Hidden Items", Wiz5.Spots.L2_8584_Hacksaw, "Enter the square");
                case 64 + 8: return new BitDesc("Enter the level", Wiz5.Spots.L2_8584_Hacksaw, "Enter the square");
                case 64 + 9: return new BitDesc("Encounter \"The Guardian\"", Wiz5.Spots.L2_8988_Guardian, "Enter the square");
                case 64 + 10: return new BitDesc("Use \"Potion of Spirit-Away\"", Wiz5.Spots.L2_8581_Scepter, "Enter the square");
                case 64 + 11: return new BitDesc("Enter the level", MapXY.Empty);
                case 64 + 12: return new BitDesc("Enter the level", MapXY.Empty);
                case 64 + 13: return new BitDesc("Enter the level", MapXY.Empty, "", Wiz5.Spots.L2_8981_SpiritAway, "Make a potion");
                case 64 + 14: return new BitDesc("Enter the level", MapXY.Empty, "", Wiz5.Spots.L2_8981_SpiritAway, "Fail to make a potion");
                case 64 + 15: return new BitDesc("Enter the level", MapXY.Empty, "", Wiz5.Spots.L2_8981_SpiritAway, "Pay the fee");
                case 64 + 27: return new BitDesc("Enter the square (activate Duck of Sparks)", Wiz5.Spots.L2_8A8B_Duck);
                case 64 + 30: return new BitDesc("Encounter anyone", Wiz5.Spots.L2_8E8A_Flagon, "Enter the square");
                case 64 + 31: return new BitDesc("Enter the level", MapXY.Empty);

                // Level 3
                case 128 + 0: return new BitDesc("", MapXY.Empty, "All unassigned scripts");
                case 128 + 1: return new BitDesc("Use \"Jeweled Scepter\"", Wiz5.Spots.L3_8D86_Hienmitey);
                case 128 + 2: return new BitDesc("", Wiz5.Spots.L3_8D86_Hienmitey, "Enter the square");
                case 128 + 3: return BitDesc.ClearOnly("Enter the level", "Use \"Jeweled Scepter\"", Wiz5.Spots.L3_8D86_Hienmitey);
                case 128 + 4: return new BitDesc("Encounter \"The Dejin Wind King\"", Wiz5.Spots.L3_8D89_Dejin, "Enter the square");
                case 128 + 5: return new BitDesc("Inspect Hidden Items", Wiz5.Spots.L3_878A_Timeless, "Enter the square");
                case 128 + 6: return BitDesc.ClearOnly("Enter the level", "Inspect Hidden Items", Wiz5.Spots.L3_878A_Timeless);
                case 128 + 7: return BitDesc.SetClear("Press C, D, E, G", Wiz5.Spots.L3_878A_Timeless, "Use \"Battery\"", Wiz5.Spots.L3_878A_Timeless);
                case 128 + 8: return BitDesc.SetClear("Press C, D, E, G", Wiz5.Spots.L3_878A_Timeless, "Press C, D, E, G", Wiz5.Spots.L3_878A_Timeless);
                case 128 + 9: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L3_878A_Timeless);
                case 128 + 10: return BitDesc.SetClear("Finish messages", Wiz5.Spots.L3_878A_Timeless, "Press C, D, E, G", Wiz5.Spots.L3_878A_Timeless);
                case 128 + 11: return new BitDesc("Use \"Blue Candle\"", Wiz5.Spots.L3_8D68_Candle, "Enter the square");
                case 128 + 12: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L3_8D68_Candle);
                case 128 + 13: return BitDesc.CheckOnly("Enter the square", Wiz5.Spots.L3_8D67_Sign);
                case 128 + 14: return BitDesc.CheckOnly("Enter the square", Wiz5.Spots.L3_8173_Sign);
                case 128 + 15: return BitDesc.CheckOnly("Enter the square", Wiz5.Spots.L3_9973_Sign);
                case 128 + 16: return BitDesc.CheckOnly("Enter the square", Wiz5.Spots.L3_8785_Sign);
                case 128 + 17: return BitDesc.CheckOnly("Enter the square", Wiz5.Spots.L3_9385_Sign);
                case 128 + 18: return BitDesc.CheckOnly("Enter the square", Wiz5.Spots.L3_8789_Sign);
                case 128 + 28: return BitDesc.SetClear("Enter the square", Wiz5.Spots.L3_8772_Stomper, "Leave the level");
                case 128 + 31: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", MapXY.Empty);

                // Level 4
                case 192 + 0: return new BitDesc("", MapXY.Empty, "All unassigned scripts");
                case 192 + 1: return new BitDesc("Search", Wiz5.Spots.L4_796C_Statues, "Enter the square");
                case 192 + 2: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_796C_Statues);
                case 192 + 3: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_796C_Statues, "Search");
                case 192 + 4: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_8A78_Pool, "Pay fee").AddSet("Finish messages", Wiz5.Spots.L4_8A78_Pool);
                case 192 + 5: return new BitDesc("Inspect Hidden Items", Wiz5.Spots.L4_8C7A_Battery, "Enter the square");
                case 192 + 6: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_8C7A_Battery, "Inspect Hidden Items").AddSet("Finish messages", Wiz5.Spots.L4_8C7A_Battery);
                case 192 + 7: return new BitDesc("Use \"Gold Key\"", Wiz5.Spots.L4_726C_Gold, "Enter the square");
                case 192 + 8: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_726C_Gold, "Use \"Gold Key\"").AddSet("Finish messages", Wiz5.Spots.L4_726C_Gold);
                case 192 + 9: return new BitDesc("Answer TIME riddle", Wiz5.Spots.L4_8069_Time, "Enter the square");
                case 192 + 10: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_8069_Time, "Answer TIME riddle").AddSet("Finish messages", Wiz5.Spots.L4_8069_Time);
                case 192 + 11: return new BitDesc("Unlock the door", Wiz5.Spots.L4_8569_Skeleton, "Enter the square");
                case 192 + 12: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_8569_Skeleton, "Unlock the door").AddSet("Finish messages", Wiz5.Spots.L4_8569_Skeleton);
                case 192 + 14: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_8369_Loon, "Use \"Pocketwatch\"");
                case 192 + 15: return new BitDesc("Use \"Petrified Demon\"", Wiz5.Spots.L4_8770_Demon, "Enter the square");
                case 192 + 16: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_8770_Demon, "Use \"Petrified Demon\"").AddSet("Finish messages", Wiz5.Spots.L4_8770_Demon);
                case 192 + 17: return new BitDesc("Search", Wiz5.Spots.L4_8C71_Jack, "Enter the square");
                case 192 + 18: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_8C71_Jack, "Search").AddSet("Finish messages", Wiz5.Spots.L4_8C71_Jack);
                case 192 + 19: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7267_Discs, "Remove the north wall");
                case 192 + 20: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7761_Discs, "Remove the west wall");
                case 192 + 21: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7761_Discs, "Remove the east wall");
                case 192 + 22: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7261_Discs, "Remove the south wall");
                case 192 + 23: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7261_Discs, "Remove the north wall");
                case 192 + 24: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7264_Discs, "Remove the north wall");
                case 192 + 25: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7264_Discs, "Remove the east wall");
                case 192 + 26: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7764_Discs, "Remove the north wall");
                case 192 + 27: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7764_Discs, "Remove the east wall");
                case 192 + 28: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7767_Discs, "Remove the north wall");
                case 192 + 29: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7767_Discs, "Remove the west wall");
                case 192 + 30: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7267_Discs, "Remove the west wall");
                case 192 + 31: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", MapXY.Empty);
                case 192 + 36: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7761_Discs, "Search", Wiz5.Spots.L4_7660_Access);
                case 192 + 37: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7261_Discs, "Search", Wiz5.Spots.L4_7862_Access);
                case 192 + 38: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7264_Discs, "Search", Wiz5.Spots.L4_705F_Access);
                case 192 + 39: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7764_Discs, "Search", Wiz5.Spots.L4_7065_Access);
                case 192 + 40: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7767_Discs, "Search", Wiz5.Spots.L4_7565_Access);
                case 192 + 41: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7267_Discs, "Search", Wiz5.Spots.L4_7968_Access);
                case 192 + 42: return new BitDesc("Enter the level", MapXY.Empty).AddClear("Search", Wiz5.Spots.L4_7163_Access);
                case 192 + 44: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_785F_Encounter, "Search").AddSet("Search", Wiz5.Spots.L4_785F_Encounter);
                case 192 + 45: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7465_Encounter);
                case 192 + 46: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L4_7568_Encounter);
                case 192 + 50: return new BitDesc("", Wiz5.Spots.L4_8B7F_Den, "Enter the square");

                // Level 5
                case 256 + 0: return new BitDesc("", MapXY.Empty, "All unassigned scripts");
                case 256 + 1: return new BitDesc("Give \"Tickets\"", Wiz5.Spots.L5_8880_BigMax, "Enter the square", Wiz5.Spots.L5_8880_BigMax, "Enter the square");
                case 256 + 2: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L5_8880_BigMax, "Finish messages");
                case 256 + 3: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L5_8080_Mystery).AddSet("Finish messages", Wiz5.Spots.L5_8080_Mystery);
                case 256 + 4: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L5_7B80_Teleport, "Search").AddClear(Wiz5.Spots.L5_7C83_Teleport).AddClear(Wiz5.Spots.L5_7D81_Spinner);
                case 256 + 5: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L5_7B80_Teleport);
                case 256 + 6: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L5_7983_Sign).AddSet("Finish messages", Wiz5.Spots.L5_7983_Sign);
                case 256 + 18: return new BitDesc("", Wiz5.Spots.L5_8675_Snatch, "Enter the square");
                case 256 + 28: return new BitDesc("Enter the square", Wiz5.Spots.L5_8382_Lord);
                case 256 + 29: return new BitDesc("Finish messages", Wiz5.Spots.L5_8D8F_Lady, "Enter the square");
                case 256 + 31: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", MapXY.Empty);
                case 256 + 41: return new BitDesc("", Wiz5.Spots.L5_7E6B_Locked, "Enter the square");
                case 256 + 42: return new BitDesc("", Wiz5.Spots.L5_6D82_Locked, "Enter the square");

                // Level 6
                case 320 + 0: return new BitDesc("", MapXY.Empty, "All unassigned scripts");
                case 320 + 1: return new BitDesc("Repair the machine", Wiz5.Spots.L6_886F_IceCapades, "Enter the square");
                case 320 + 2: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L6_886F_IceCapades, "Repair the machine");
                case 320 + 3: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L6_8470_Slide, "Activate the slide").AddSet("Enter the square", Wiz5.Spots.L6_8470_Slide);
                case 320 + 4: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L6_8668_Cavern, "Encounter \"The Robuna Ice King\"").AddSet("Finish messages", Wiz5.Spots.L6_8668_Cavern);
                case 320 + 5: return new BitDesc("", Wiz5.Spots.L6_8F7C_MightyYog, "Enter the square");
                case 320 + 6: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L6_8F7C_MightyYog).AddClear("Use \"Gold Medallion\"", Wiz5.Spots.L6_8F7C_MightyYog);
                case 320 + 7: return new BitDesc("Inspect Hidden Items", Wiz5.Spots.L6_817F_IceKey, "Enter the square");
                case 320 + 8: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L6_817F_IceKey, "Search").AddSet("Finish messages", Wiz5.Spots.L6_817F_IceKey);
                case 320 + 9: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L6_817F_IceKey, "Encounter \"Horbule\"").AddSet("Finish messages", Wiz5.Spots.L6_817F_IceKey);
                case 320 + 10: return new BitDesc("Enter the square", Wiz5.Spots.L6_797F_EvilEyes);
                case 320 + 11: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L6_876A_DemonOut, "Pay the fee").AddSet("Finish messages", Wiz5.Spots.L6_876A_DemonOut);
                case 320 + 12: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L6_876A_DemonOut, "Select \"Potion of Demon-Out\"");
                case 320 + 13: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L6_876A_DemonOut, "Select a potion").AddSet("Finish messages", Wiz5.Spots.L6_876A_DemonOut);
                case 320 + 30: return new BitDesc("Repair the machine", Wiz5.Spots.L6_886F_IceCapades, "Enter the square", Wiz5.Spots.L6_886F_IceCapades);
                case 320 + 31: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", MapXY.Empty);
                case 320 + 40: return new BitDesc("Hear the first vision", Wiz5.Spots.L6_797F_EvilEyes, "Pay the fee", Wiz5.Spots.L6_797F_EvilEyes);
                case 320 + 43: return new BitDesc("Hear the first vision", Wiz5.Spots.L6_797F_EvilEyes, "Pay the fee", Wiz5.Spots.L6_797F_EvilEyes);
                case 320 + 44: return new BitDesc("Hear the second vision", Wiz5.Spots.L6_797F_EvilEyes, "Pay the fee", Wiz5.Spots.L6_797F_EvilEyes);
                case 320 + 45: return new BitDesc("Hear the third vision", Wiz5.Spots.L6_797F_EvilEyes, "Pay the fee", Wiz5.Spots.L6_797F_EvilEyes);
                case 320 + 46: return new BitDesc("Hear the fourth vision", Wiz5.Spots.L6_797F_EvilEyes, "Pay the fee", Wiz5.Spots.L6_797F_EvilEyes);
                case 320 + 47: return new BitDesc("Hear the fifth vision", Wiz5.Spots.L6_797F_EvilEyes, "Pay the fee", Wiz5.Spots.L6_797F_EvilEyes);

                // Level 7
                case 384 + 0: return new BitDesc("", MapXY.Empty, "All unassigned scripts");
                case 384 + 1: return new BitDesc("Answer LIFE riddle", Wiz5.Spots.L7_757A_AirStaff, "Enter the square");
                case 384 + 2: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L7_757A_AirStaff).AddSet("Finish messages", Wiz5.Spots.L7_757A_AirStaff);
                case 384 + 3: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L7_757A_AirStaff);
                case 384 + 4: return new BitDesc("Encounter \"Kong\" and \"Fay\"", Wiz5.Spots.L7_7C74_EarthStaff, "Enter the square");
                case 384 + 5: return new BitDesc("Encounter \"The Kanzi Fire King\"", Wiz5.Spots.L7_8E84_Trap, "Enter the square");
                case 384 + 6: return new BitDesc("", Wiz5.Spots.L7_8E78_FireStaff, "Enter the square");
                case 384 + 16: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L7_8180_Red, "Use \"Orb of Llylgamyn\"");
                case 384 + 17: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L7_8080_Blue, "Use \"Orb of Llylgamyn\"");
                case 384 + 18: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L7_8181_Yellow, "Use \"Orb of Llylgamyn\"");
                case 384 + 19: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L7_8081_White, "Use \"Orb of Llylgamyn\"");
                case 384 + 20: return new BitDesc("Give \"Jack of Spades\"", Wiz5.Spots.L7_7B86_LordSpades, "Enter the square", Wiz5.Spots.L7_8180_Red);
                case 384 + 21: return new BitDesc("Give \"Queen of Hearts\"", Wiz5.Spots.L7_8686_LordHearts, "Enter the square", Wiz5.Spots.L7_8080_Blue);
                case 384 + 22: return new BitDesc("Give \"King of Diamonds\"", Wiz5.Spots.L7_7B7B_LordDiamonds, "Enter the square", Wiz5.Spots.L7_8181_Yellow);
                case 384 + 23: return new BitDesc("Give \"Ace of Clubs\"", Wiz5.Spots.L7_867B_LordClubs, "Enter the square", Wiz5.Spots.L7_8081_White);
                case 384 + 24: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L7_7B86_LordSpades, "Give \"Jack of Spades\"");
                case 384 + 25: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L7_8686_LordHearts, "Give \"Queen of Hearts\"");
                case 384 + 26: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L7_7B7B_LordDiamonds, "Give \"King of Diamonds\"");
                case 384 + 27: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L7_867B_LordClubs, "Give \"Ace of Clubs\"");
                case 384 + 31: return new BitDesc("Enter the level", MapXY.Empty);

                // Level 8
                case 448 + 0: return new BitDesc("", MapXY.Empty, "All unassigned scripts");
                case 448 + 1: return new BitDesc("Encounter clones", Wiz5.Spots.L8_7B81_Clones, "Enter the square");
                case 448 + 2: return new BitDesc("Use \"Staff of Earth\"", Wiz5.Spots.L8_7D81_Red, "Enter the square");
                case 448 + 3: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_7D81_Red, "Use \"Staff of Earth\"").AddSet(
                    "Finish messages", Wiz5.Spots.L8_7D81_Red).AddClear("Fail NATURE riddle", Wiz5.Spots.L8_7D81_Red);
                case 448 + 4: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_7D81_Red, "Light candles A, D, I").AddSet("Finish messages", Wiz5.Spots.L8_7D81_Red);
                case 448 + 5: return new BitDesc("Encounter clones", Wiz5.Spots.L8_817B_Clones, "Enter the square");
                case 448 + 6: return new BitDesc("Use \"Staff of Water\"", Wiz5.Spots.L8_817D_Blue, "Enter the square");
                case 448 + 7: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_817D_Blue, "Use \"Staff of Water\"").AddSet(
                    "Finish messages", Wiz5.Spots.L8_817D_Blue).AddClear("Fail GROWTH riddle", Wiz5.Spots.L8_817D_Blue);
                case 448 + 8: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_817D_Blue, "Light candles B, E, H").AddSet("Finish messages", Wiz5.Spots.L8_817D_Blue);
                case 448 + 9: return new BitDesc("Encounter clones", Wiz5.Spots.L8_8781_Clones, "Enter the square");
                case 448 + 10: return new BitDesc("Use \"Staff of Fire\"", Wiz5.Spots.L8_8581_Yellow, "Enter the square");
                case 448 + 11: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_8581_Yellow, "Use \"Staff of Fire\"").AddSet(
                    "Finish messages", Wiz5.Spots.L8_8581_Yellow).AddClear("Fail CHANGE riddle", Wiz5.Spots.L8_8581_Yellow);
                case 448 + 12: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_8581_Yellow, "Light candles C, F, G").AddSet("Finish messages", Wiz5.Spots.L8_8581_Yellow);
                case 448 + 13: return new BitDesc("Encounter clones", Wiz5.Spots.L8_8187_Clones, "Enter the square");
                case 448 + 14: return new BitDesc("Use \"Staff of Air\"", Wiz5.Spots.L8_8185_White, "Enter the square");
                case 448 + 15: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_8185_White, "Use \"Staff of Air\"").AddSet(
                    "Finish messages", Wiz5.Spots.L8_8185_White).AddClear("Fail MAN riddle", Wiz5.Spots.L8_8185_White);
                case 448 + 16: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_8185_White, "Light candles A through I").AddSet("Finish messages", Wiz5.Spots.L8_8185_White);
                case 448 + 17: return new BitDesc("Encounter \"The S*O*R*N\"", Wiz5.Spots.L8_8181_Sorn, "Enter the square");
                case 448 + 18: return new BitDesc("Defeat \"The S*O*R*N\"", Wiz5.Spots.L8_8181_Sorn, "Enter the square");
                case 448 + 20: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_7D81_Red, "Answer NATURE riddle");
                case 448 + 21: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_817D_Blue, "Answer GROWTH riddle");
                case 448 + 22: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_8581_Yellow, "Answer CHANGE riddle");
                case 448 + 23: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_8185_White, "Answer MAN riddle");
                case 448 + 24: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_8181_Sorn, "Enter after completing ritual");
                case 448 + 25: return new BitDesc("Enter after completing ritual", Wiz5.Spots.L8_8181_Sorn, "Enter the square");
                case 448 + 26: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_7D81_Red, "Answer NATURE riddle").AddSet("Finish messages", Wiz5.Spots.L8_7D81_Red);
                case 448 + 27: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_817D_Blue, "Answer GROWTH riddle").AddSet("Finish messages", Wiz5.Spots.L8_817D_Blue);
                case 448 + 28: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_8581_Yellow, "Answer CHANGE riddle").AddSet("Finish messages", Wiz5.Spots.L8_8581_Yellow);
                case 448 + 29: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", Wiz5.Spots.L8_8185_White, "Answer MAN riddle").AddSet("Finish messages", Wiz5.Spots.L8_8185_White);
                case 448 + 31: return new BitDesc("Enter the level", MapXY.Empty, "Enter the square", MapXY.Empty);
                case 448 + 39: return new BitDesc("", Wiz5.Spots.L8_A5DC_Sign, "Enter the square");
                case 448 + 50: return new BitDesc("Encounter \"Manfretti's Ghost\"", Wiz5.Spots.L8_D042_Manfretti, "Enter the square");

                default: return BitDesc.Empty;
            }
        }

        public static BitDesc L1BitsDesc(object val) { return val is int ? ActivatedBitsDesc((int)val + (64 * 0)) : BitDesc.Empty; }
        public static BitDesc L2BitsDesc(object val) { return val is int ? ActivatedBitsDesc((int)val + (64 * 1)) : BitDesc.Empty; }
        public static BitDesc L3BitsDesc(object val) { return val is int ? ActivatedBitsDesc((int)val + (64 * 2)) : BitDesc.Empty; }
        public static BitDesc L4BitsDesc(object val) { return val is int ? ActivatedBitsDesc((int)val + (64 * 3)) : BitDesc.Empty; }
        public static BitDesc L5BitsDesc(object val) { return val is int ? ActivatedBitsDesc((int)val + (64 * 4)) : BitDesc.Empty; }
        public static BitDesc L6BitsDesc(object val) { return val is int ? ActivatedBitsDesc((int)val + (64 * 5)) : BitDesc.Empty; }
        public static BitDesc L7BitsDesc(object val) { return val is int ? ActivatedBitsDesc((int)val + (64 * 6)) : BitDesc.Empty; }
        public static BitDesc L8BitsDesc(object val) { return val is int ? ActivatedBitsDesc((int)val + (64 * 7)) : BitDesc.Empty; }

        public static BitDescriptionDelegate[] BitDescriptions = new BitDescriptionDelegate[] { 
            L1BitsDesc, L2BitsDesc, L3BitsDesc, L4BitsDesc, L5BitsDesc, L6BitsDesc, L7BitsDesc, L8BitsDesc };

        public override List<GameInfoItem> GetMiscItems()
        {
            List<GameInfoItem> items = new List<GameInfoItem>();

            WizMapData map = new WizMapData(Game, Location.MapIndex, Bytes, 0, true);
            if (map == null)
                return items;

            for(int i = 0; i < ActivatedBits.Length; i++)
                items.Add(new Wiz5GameInfoItem(String.Format("Bits: Level {0}", i + 1), ActivatedBits[i], new OffsetList(Wiz5.Memory.ActivatedBits + (i * 8)), BitDescriptions[i]));
            items.Add(new Wiz5GameInfoItem("Square Walls", Walls, new OffsetList(Wiz5.Memory.ActivatedBits + 64)));

            if (InCombat)
            {
                items.Add(new Wiz5GameInfoItem("Trap Type", TrapType, new OffsetList(Wiz5.Memory.TrapType)));
            }

            return items;
        }
    }

    public class Wiz5GameInfoItem : WizGameInfoItem
    {
        public override MapTitleInfo GetMapTitlePair(int iMap) { return Wiz5MemoryHacker.GetMapTitlePair(iMap); }

        public Wiz5GameInfoItem(string desc, object val, OffsetList offsets, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, offsets, type, mask, style, fn)
        {
        }

        public Wiz5GameInfoItem(string desc, object val, int offset, DataType type = DataType.Auto, UInt32 mask = 0, ShowStyle style = ShowStyle.Editable, BitDescriptionDelegate fn = null)
            : base(desc, val, new OffsetList(offset), type, mask, style, fn)
        {
        }

        public Wiz5GameInfoItem(string desc, object val, OffsetList offsets, BitDescriptionDelegate fn)
            : base(desc, val, offsets, DataType.Bits, 0, ShowStyle.Editable, fn)
        {
        }
    }

    public class Wiz5TrainingInfo : TrainingInfo
    {
        public WizGameState State;
    }

    public class Wiz5MapData : WizardryMapData
    {
        public override int DefaultZoom { get { return 100; } }
        public byte[] DarkBytes;
        public Wiz5SpecialSquares Special;

        public Wiz5MapData(int iMapIndex, byte[] bytes, byte[] bytesDark)
        {
            Game = GameNames.Wizardry5;
            LocationOffset = Point.Empty;
            SetFromBytes(iMapIndex, bytes, bytesDark);
        }

        public class SectionOffsets
        {
            public Point[] Points;
            public bool[] Adjacent;
            public Rectangle Bounds;

            public SectionOffsets(int iMap, byte[] bytes, int offset = 0)
            {
                if (bytes == null || bytes.Length < 32 + offset)
                    return;

                Points = new Point[16];
                for (int i = 0; i < 16; i++)
                    Points[i] = new Point(bytes[offset + i + 16] - 129, bytes[offset + i] - 129);

                Adjacent = new bool[16];
                for (int i = 0; i < 16; i++)
                    Adjacent[i] = Points.Any(p => p != Points[i] && Math.Abs(p.X - Points[i].X) <= 8 && Math.Abs(p.Y - Points[i].Y) <= 8);

                int iMinX = 127;
                int iMaxX = -129;
                int iMinY = 127;
                int iMaxY = -129;

                for (int i = 0; i < 16; i++)
                {
                    if (!Adjacent[i])
                        continue;

                    if (Points[i].X < iMinX)
                        iMinX = Points[i].X;
                    if (Points[i].X > iMaxX)
                        iMaxX = Points[i].X;
                    if (Points[i].Y < iMinY)
                        iMinY = Points[i].Y;
                    if (Points[i].Y > iMaxY)
                        iMaxY = Points[i].Y;
                }

                iMaxX += 8;
                iMaxY += 8;

                Bounds = new Rectangle(iMinX, iMinY, iMaxX - iMinX, iMaxY - iMinY);
                if (iMap == 8)  // Goofy level needs special case
                    Bounds = new Rectangle(-12, -12, 43, 43);
                if (Bounds.Width < 32)
                    Bounds.Width = 32;
                if (Bounds.Height < 32)
                    Bounds.Height = 32;
            }

            public int GetNormalIndex(int iMemX, int iMemY)
            {
                int iOffset = iMemX / 8 + (iMemY / 8 * 4);
                if (iOffset < 0 || iOffset >= Points.Length)
                    return 0;
                return iOffset;
            }
        }

        public class Wiz5WallSet : WallSet
        {
            public SectionOffsets Sections = null;
            private bool[,] m_dark = null;
            private bool[,] m_inaccessible = null;

            public override WizWall GetWall(int x, int y)
            {
                if (x - Sections.Bounds.Left >= 0 && x - Sections.Bounds.Left < m_walls.GetLength(0) && 
                    y - Sections.Bounds.Top >= 0 && y - Sections.Bounds.Top < m_walls.GetLength(1))
                    return m_walls[x - Sections.Bounds.Left, y - Sections.Bounds.Top];
                return WizWall.OffMap;
            }

            public bool GetDark(int x, int y)
            {
                if (m_dark != null && x - Sections.Bounds.Left >= 0 && x - Sections.Bounds.Left < m_walls.GetLength(0) &&
                    y - Sections.Bounds.Top >= 0 && y - Sections.Bounds.Top < m_walls.GetLength(1))
                    return m_dark[x - Sections.Bounds.Left, y - Sections.Bounds.Top];
                return false;
            }

            public bool GetInaccessible(int x, int y)
            {
                if (m_inaccessible != null && x - Sections.Bounds.Left >= 0 && x - Sections.Bounds.Left < m_walls.GetLength(0) &&
                    y - Sections.Bounds.Top >= 0 && y - Sections.Bounds.Top < m_walls.GetLength(1))
                    return m_inaccessible[x - Sections.Bounds.Left, y - Sections.Bounds.Top];
                return false;
            }

            public Wiz5WallSet(int iMap, byte[] bytes)
            {
                // Create a set of walls that will be copies of whatever is on the other side
                byte[] byteSections = Global.Subset(bytes, 512, 32);
                Sections = new SectionOffsets(iMap, byteSections);
                m_walls = new WizWall[Sections.Bounds.Width, Sections.Bounds.Height];

                for (int i = 0; i < Sections.Bounds.Width; i++)
                    for (int j = 0; j < Sections.Bounds.Height; j++)
                        m_walls[i, j] = WizWall.Dependent;
            }

            public Wiz5WallSet(int iMap, byte[] bytes, byte[] bytesDark, int offset = 0, int bitOffset = 0)
            {
                byte[] byteSections = Global.Subset(bytes, 512, 32);
                //byteSections = new byte[] { 128, 128, 128, 128, 136, 136, 136, 136, 144, 144, 144, 144, 152, 152, 152, 152,
                //                            128, 136, 144, 152, 128, 136, 144, 152, 128, 136, 144, 152, 128, 136, 144, 152 };
                Sections = new SectionOffsets(iMap, byteSections);
                int width = Sections.Bounds.Width;
                int height = Sections.Bounds.Height;

                m_walls = new WizWall[width, height];
                if (bytesDark != null)
                {
                    m_dark = new bool[width, height];
                    m_inaccessible = new bool[width, height];
                }

                // For Wizardry 5 the default square is "wall" (for sparse sections that do not actually have bytes)
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        m_walls[i, j] = WizWall.SparseRock;
                        if (m_dark != null)
                            m_dark[i, j] = false;
                        if (m_inaccessible != null)
                            m_inaccessible[i, j] = false;
                    }
                }

                Point ptMapOffset = Point.Empty;
                int iDark = 0;
                for (int row = 0; row < 32; row += 8)
                {
                    for (int col = 0; col < 32; col++)
                    {
                        if (col % 8 == 0)
                            ptMapOffset = SectionOffset(iMap, col, row);

                        int iGameX = col - Sections.Bounds.X + ptMapOffset.X;
                        int iGameY = row - Sections.Bounds.Y + ptMapOffset.Y + bitOffset;

                        int iByte = offset + (row / 4 * 32) + (col * 2);
                        if (iByte < 0)
                        {
                            SetFromByte(0, iGameX, iGameY);
                            SetFromByte(0, iGameX, iGameY + 4);
                        }
                        else
                        {
                            SetFromByte(bytes[iByte], iGameX, iGameY);
                            SetFromByte(bytes[iByte + 1], iGameX, iGameY + 4);
                        }

                        if (m_dark != null)
                            Global.SetPackedBools(m_dark, bytesDark[iDark * 2], iGameX, iGameY, 8);
                        if (m_inaccessible != null)
                            Global.SetPackedBools(m_inaccessible, bytesDark[256 + (iDark * 2)], iGameX, iGameY, 8);
                        iDark++;
                    }
                }
            }

            public Point SectionOffset(int iMap, int iMemX, int iMemY)
            {
                // Returns the relative offset (in squares) to where this 8x8 section "should be"
                // (i.e. east of the previous one, or north 1, west 4 if past the 32x32 boundary)
                int iNormalIndex = Sections.GetNormalIndex(iMemX, iMemY);
                if (Sections.Points[iNormalIndex].X == iMemX && Sections.Points[iNormalIndex].Y == iMemY)
                    return Point.Empty; // No offset

                if (iMap == 8)
                {
                    // Level 8 has some widely dispersed small areas that need special treatment
                    if (iMemX == 8 && iMemY == 16)
                        return new Point(142 - 136, 117 - 144);
                    if (iMemX == 16 && iMemY == 16)
                        return new Point(146 - 144, 125 - 144);
                    if (iMemX == 24 && iMemY == 16)
                        return new Point(142 - 152, 137 - 144);
                    if (iMemX == 0 && iMemY == 24)
                        return new Point(150 - 128, 134 - 152);
                    if (iMemX == 8 && iMemY == 24)
                        return new Point(117 - 136, 145 - 152);
                    if (iMemX == 16 && iMemY == 24)
                        return new Point(125 - 144, 150 - 152);
                    if (iMemX == 24 && iMemY == 24)
                        return new Point(131 - 152, 142 - 152);
                }

                if (!Sections.Adjacent[iNormalIndex])
                {
                    // No offset, but this will need a custom map section
                    return new Point(-1, -1);
                }

                return new Point(Sections.Points[iNormalIndex].X - iMemX, Sections.Points[iNormalIndex].Y - iMemY);
            }
        }

        public void SetFromBytes(int index, byte[] bytes, byte[] bytesDark, int offset = 0, bool bSkipMainInfo = false, bool bSkipNonWallInfo = false)
        {
            if (bytes.Length <= 546)
                bSkipNonWallInfo = true;

            Index = index;
            if (!bSkipMainInfo)
            {
                Title = new MapTitleInfo(index, String.Format("Maze Level {0}", index));
                DarkBytes = bytesDark;
                Wiz5WallSet east = new Wiz5WallSet(index, bytes, bytesDark);
                East = east;
                North = new Wiz5WallSet(index, bytes, null, 32 * 32 / 4);

                West = new Wiz5WallSet(index, bytes);
                South = new Wiz5WallSet(index, bytes);
                // West = new Wiz5WallSet(index, bytes, null, -2);
                // South = new Wiz5WallSet(index, bytes, null, 32 * 32 / 4, 1);

                Special = bytes.Length > 546 ? new Wiz5SpecialSquares(bytes, index) : null;

                if (!bSkipNonWallInfo)
                {
                    Fights = new bool[32, 32];
                //    Fights = GetFights(bytes, offset + Offsets.Fights);
                //    FightBytes = new byte[80];
                //    Buffer.BlockCopy(bytes, offset + Offsets.Fights, FightBytes, 0, 80);
                //    Extras = GetExtras(bytes, offset + Offsets.Extras);
                //    Types = GetTypes(bytes, offset + Offsets.Types);
                //    Aux0 = GetAux(bytes, offset + Offsets.Aux0);
                //    Aux1 = GetAux(bytes, offset + Offsets.Aux1);
                //    Aux2 = GetAux(bytes, offset + Offsets.Aux2);
                }
                else
                    LiveOnly = true;

                List<MapSection> customSections = new List<MapSection>();
                if (index == 8)
                {
                    customSections.Add(new MapSection(new Rectangle(26, -11, 12, 16), new Point(east.Sections.Points[9].X, east.Sections.Points[9].Y)));
                    customSections.Add(new MapSection(new Rectangle(26, 6, 16, 11), new Point(east.Sections.Points[11].X, east.Sections.Points[11].Y - 3)));
                    customSections.Add(new MapSection(new Rectangle(1, 14, 22, 16), new Point(east.Sections.Points[13].X, east.Sections.Points[13].Y - 3)));
                }
                else
                {
                    for (int i = 0; i < east.Sections.Adjacent.Length; i++)
                    {
                        if (!east.Sections.Adjacent[i])
                            customSections.Add(new MapSection(new Rectangle((i % 4) * 8, i / 4 * 8 - 1, 8, 8), east.Sections.Points[i]));
                    }
                }
                if (customSections.Count > 0)
                    Sections = customSections.ToArray();

                Bounds = east.Sections.Bounds;
            }
            //Enemies = GetEnemies(bytes, offset + Offsets.Enemies1);
        }
    }

    public class Wiz5ActiveSquares : ActiveSquares
    {
        private Rectangle[] m_remap;
        private Wiz5SpecialSquares m_special;
        private byte[] m_bytesMap;
        private byte[] m_bitsActive;

        public byte[] GetMapBytes() { return m_bytesMap; }
        public byte[] GetActiveBits() { return m_bitsActive; }

        public bool BytesEqual(byte[] bytesMap, byte[] bytesActive, byte[] bitsActive)
        {
            return Global.Compare(RawBytes, bytesActive) && Global.Compare(bytesMap, m_bytesMap) && Global.Compare(bitsActive, m_bitsActive);
        }

        public override bool BytesEqual(ActiveSquares active)
        {
            if (active is Wiz5ActiveSquares)
                return BytesEqual(((Wiz5ActiveSquares)active).GetMapBytes(), RawBytes, ((Wiz5ActiveSquares)active).GetActiveBits());
            return base.BytesEqual(active);
        }

        public Wiz5ActiveSquares(MainForm main, int mapIndex, byte[] bytesMap, byte[] bytesActive, byte[] bitsActive)
        {
            Main = main;
            m_iMapIndex = mapIndex;
            RawBytes = bytesActive;
            m_bytesMap = bytesMap;
            m_bitsActive = bitsActive;
            m_remap = GetRemapPoints(bytesMap, Wiz5.Memory.MapBlockPosY - Wiz5.Memory.Map, 32);
            m_special = new Wiz5SpecialSquares(bytesMap, mapIndex);
            m_bInitialized = false;
        }

        public static Rectangle[] GetRemapPoints(byte[] bytes, int offset = 0, int count = -1)
        {
            if (count == -1)
                count = bytes.Length - offset;

            int iNum = count / 2;
            Rectangle[] remaps = new Rectangle[iNum];
            for (int i = 0; i < iNum; i++)
                remaps[i] = new Rectangle(bytes[offset + iNum + i] - 129, bytes[offset + i] - 129, 8, 8);

            return remaps;
        }

        public bool IsEventBitSet(int iMap, int iBit)
        {
            if (iMap < 1 || iMap > 8)
                return false;
            return Global.GetBit(m_bitsActive, ((iMap - 1) * 64) + iBit, true) == 1;
        }

        public override bool IsActive(int x, int y, bool bEncountersOnly)
        {
            if (m_remap == null)
                return false;

            for (int i = 0; i < m_remap.Length; i++)
            {
                if (m_remap[i].Contains(x, y))
                {
                    Point ptGame = new Point(x, y);
                    x -= m_remap[i].Left;
                    y -= m_remap[i].Top;
                    int offset = ((i / 4) * 64) + ((i % 4) * 16) + (x * 2);
                    if (offset < 0 || offset >= RawBytes.Length)
                        return false;
                    bool bActive = Global.IsBitSet(RawBytes[offset], y % 8);
                    if (!bActive)
                        return false;

                    if (!AnyEventsInSquare(ptGame))
                        return false;

                    if (bEncountersOnly)
                    {
                        if (m_special.Squares.ContainsKey(ptGame))
                            return m_special.Squares[ptGame].Any(s => s.Type == Wiz5SquareType.Encounter);
                        return false;
                    }
                    return true;
                }
            }
            return false;
        }

        private bool AnyEventsInSquare(Point ptGame)
        {
            // A square may be deactivated if all of the event bits for that square are set
            if (m_special.Squares.ContainsKey(ptGame))
                return (m_special.Squares[ptGame].Any(s => !IsEventBitSet(m_iMapIndex, s.Value0)));

            return false;
        }

        protected override void Initialize()
        {
            if (m_remap == null)
                return;

            m_activeSquares = new Dictionary<Point, ActiveSquareInfo>();

            if (RawBytes == null || RawBytes.Length < 256)
                return;

            for (int i = 0; i < 16; i++)
            {
                for (int y = 0; y < 8; y++)
                {
                    for (int x = 0; x < 8; x++)
                    {
                        Point pt = new Point(x + m_remap[i].Left, y + m_remap[i].Top);
                        if (m_activeSquares.ContainsKey(pt))
                        {
                            m_remap = null;
                            m_activeSquares = null;
                            return;    // Proper map data does not overlap points; probably in a transitional state
                        }
                        int offset = ((i / 4) * 64) + ((i % 4) * 16) + (x * 2);
                        if (offset >= 0 && offset < RawBytes.Length)
                        {
                            // If the bit in the raw bytes is not set, the square is not active.  If it is, there is more work to do
                            if (!Global.IsBitSet(RawBytes[offset], y))
                                m_activeSquares.Add(pt, new ActiveSquareInfo(pt, false));
                            else
                                m_activeSquares.Add(pt, new ActiveSquareInfo(pt, AnyEventsInSquare(pt)));
                        }
                    }
                }
            }

            m_bInitialized = true;
        }
    }

    public class Wiz5EncounterGroup : WizEncounterGroup
    {
        public new const int Size = 12 + (Wiz5EncounterRecord.Size * 9) + (Wiz5Monster.Size);

        public Wiz5EncounterGroup(byte[] bytes, int offset = 0)
        {
            if (bytes == null || bytes.Length < offset + 120)   // It's okay to be missing the monster bytes at the end
            {
                SetDefaults();
                return;
            }
            Identified = BitConverter.ToInt16(bytes, offset) != 0;
            NumAlive = BitConverter.ToInt16(bytes, offset + 2);
            NumEnemies = BitConverter.ToInt16(bytes, offset + 4);
            Index = BitConverter.ToInt16(bytes, offset + 6);
            MagicScreen = BitConverter.ToInt16(bytes, offset + 8);
            FizzleField = BitConverter.ToInt16(bytes, offset + 10);
            Records = new List<WizEncounterRecord>(9);
            for (int i = 0; i < 9; i++)
            {
                if (i < NumAlive)
                    Records.Add(new Wiz5EncounterRecord(bytes, offset + 12 + (20 * i)));
                else
                    Records.Add(Wiz5EncounterRecord.Default);
            }
            MonsterChanged = false;
        }
    }

    public class Wiz5EncounterRecord : WizEncounterRecord
    {
        public new const int Size = 20;

        public Wiz5EncounterRecord(byte[] bytes, int offset = 0)
        {
            if (bytes.Length - offset < 20)
                return;

            Victim = BitConverter.ToInt16(bytes, offset);
            SpellHash = BitConverter.ToInt16(bytes, offset + 2);
            Initiative = BitConverter.ToInt16(bytes, offset + 4);
            Unknown1 = BitConverter.ToInt16(bytes, offset + 6);
            CurrentHP = BitConverter.ToInt16(bytes, offset + 8);
            ArmorClass = -BitConverter.ToInt16(bytes, offset + 10);
            Unknown2 = BitConverter.ToInt16(bytes, offset + 12);
            Silenced = BitConverter.ToInt16(bytes, offset + 14);
            Level = BitConverter.ToInt16(bytes, offset + 16);
            Condition = (WizCondition)BitConverter.ToInt16(bytes, offset + 18);
        }

        public Wiz5EncounterRecord()
        {
            Victim = 0;
            SpellHash = -1;
            Initiative = 0;
            CurrentHP = 0;
            ArmorClass = 0;
            Silenced = 0;
            Condition = WizCondition.Good;
            Unknown1 = 0;
            Unknown2 = 0;
            Level = 0;
        }

        public new static WizEncounterRecord Default
        {
            get
            {
                Wiz5EncounterRecord record = new Wiz5EncounterRecord();
                record.ArmorClass = 0;
                record.Condition = WizCondition.Good;
                record.CurrentHP = 1;
                record.Initiative = 0;
                record.Silenced = 0;
                record.SpellHash = -1;
                record.Unknown1 = 1;
                record.Victim = 1;
                record.Level = 1;
                record.Unknown2 = 0;
                return record;
            }
        }
    }

    public class Wiz5LocationInt
    {
        public int Value;

        public Wiz5LocationInt(int val) { Value = val; }

        // --ll.llss.ssyy.yxxx
        public int Level { get { return (Value & 0x3c00) >> 10; } }
        public int Section { get { return (Value & 0x03c0) >> 6; } }
        public int Y { get { return (Value & 0x0038) >> 3; } }
        public int X { get { return Value & 0x0007; } }
    }

    public enum Wiz5SquareType
    {
        None = 0,
        SwitchMap = 1,
        Move = 2,
        Receive = 3,
        Encounter = 4,
        Unknown5 = 5,
        CheckItem = 6,
        AlterMap = 7,
        Message = 8,
        Trap = 9,
        Unknown10 = 10,
        Unknown11 = 11,
        Unknown12 = 12,
        Unknown13 = 13,
        Unknown14 = 14,
        BlueMessage = 15
    }

    public class Wiz5SpecialSquare
    {
        public int Section;
        public Wiz5SquareType Type;
        public Point Location;
        public int SubX;
        public int SubY;
        public int Value0;
        public int Value1;
        public int Value2;
        public int Value3;

        public Wiz5SpecialSquare(int section, int type, int subX, int subY, Point location, int val0, int val1, int val2, int val3)
        {
            Section = section;
            Type = (Wiz5SquareType)type;
            SubX = subX;
            SubY = subY;
            Location = location;
            Value0 = val0;
            Value1 = val1;
            Value2 = val2;
            Value3 = val3;
        }
    }

    public class Wiz5SpecialSquares
    {
        public Dictionary<Point, List<Wiz5SpecialSquare>> Squares;

        public Wiz5SpecialSquares(byte[] bytesMap, int iMapIndex)
        {
            Wiz5MapData.SectionOffsets sections = new Wiz5MapData.SectionOffsets(iMapIndex, bytesMap, Wiz5.Memory.MapBlockPosY - Wiz5.Memory.Map);

            PackedFourBitValues p4Spec = new PackedFourBitValues(bytesMap, Wiz5.Memory.MapSpecials - Wiz5.Memory.Map, 48);
            PackedThreeBitValues p3X = new PackedThreeBitValues(bytesMap, Wiz5.Memory.MapSpecialsX - Wiz5.Memory.Map, 40);
            PackedThreeBitValues p3Y = new PackedThreeBitValues(bytesMap, Wiz5.Memory.MapSpecialsY - Wiz5.Memory.Map, 40);
            PackedFourBitValues p4Sub = new PackedFourBitValues(bytesMap, Wiz5.Memory.MapSpecialsSub8 - Wiz5.Memory.Map, 48);
            PackedSixBitValues p6Bits = new PackedSixBitValues(bytesMap, Wiz5.Memory.MapAux0 - Wiz5.Memory.Map, 96);

            int[] aux0 = new int[96];
            int[] aux2 = new int[96];
            for (int i = 0; i < aux2.Length; i++)
            {
                aux0[i] = p6Bits.Values[i];
                aux2[i] = bytesMap[Wiz5.Memory.MapAux2 - Wiz5.Memory.Map + i];
            }

            int[] aux1 = new int[96];
            int[] aux3 = new int[96];
            for (int i = 0; i < aux3.Length; i++)
            {
                aux1[i] = BitConverter.ToUInt16(bytesMap, Wiz5.Memory.MapAux1 - Wiz5.Memory.Map + (i * 2));
                aux3[i] = BitConverter.ToUInt16(bytesMap, Wiz5.Memory.MapAux3 - Wiz5.Memory.Map + (i * 2));
            }

            Squares = new Dictionary<Point,List<Wiz5SpecialSquare>>();

            for (int i = 0; i < p4Spec.Values.Length; i++)
            {
                Point ptMap = sections.Points[p4Sub.Values[i]];
                Wiz5SpecialSquare square = new Wiz5SpecialSquare(p4Sub.Values[i], p4Spec.Values[i], p3X.Values[i], p3Y.Values[i], 
                    new Point(ptMap.X + p3X.Values[i], ptMap.Y + p3Y.Values[i]), aux0[i], aux1[i], aux2[i], aux3[i]);

                if (!Squares.ContainsKey(square.Location))
                    Squares.Add(square.Location, new List<Wiz5SpecialSquare>(1));
                Squares[square.Location].Add(square);
            }
        }
    }

    public class Wiz5CureAllInfo : CureAllInfo
    {
        public WizCondition[] Conditions;   // 6 bytes; one per character
        public WizCondition CasterCondition;
        public Int16[] HitPoints;
        public Int16[] HitPointsMax;
        public Wiz5KnownSpells CasterSpells;
        public Wiz5SpellPoints CasterSpellPoints;
        public bool InCastle;

        public override bool Valid { get { return Conditions != null && Conditions.Length > 0; } }
        public override bool IsHealer { get { return CasterSpells.KnowsAnyHealing; } }
        public override bool IsIncapacitated { get { return CasterCondition >= WizCondition.Asleep; } }
        public override bool MagicPermitted { get { return true; } }
        public override bool Combat { get { return !InCastle; } }
        public override string CombatString { get { return "the Maze"; } }

        public Wiz5CureAllInfo()
        {
        }
    }

    public class Wiz5MemoryHacker : Wiz123MemoryHacker
    {
        protected override WizMemory Memory { get { return Wiz5.Memory; } }
        public override List<WizItem> WizItems { get { return Wiz5.Items; } }
        public override GameInformationControl CreateGameInfoControl(IMain main) { return new Wiz5GameInformationControl(main); }
        protected override QuestInfo CreateQuestInfo() { return new Wiz5QuestInfo(); }
        public override bool InitExternalMonsterList() { return Wiz5.MonsterList.Value.InitExternalList(this); }
        protected override Wiz1234GameInfo CreateGameInfo() { return new Wiz5GameInfo(); }
        public override List<bool[,]> GetFights() { return Wiz5.Encounters.Fights; }
        public override string GetMapEnum(int index) { return String.Format("Wiz5Map.{0}", Enum.GetName(typeof(Wiz5Map), (Wiz5Map)(index))); }
        public override string PleaseFormPartyString { get { return "Please load or start a new game."; } }
        public override TrainingAssistantControl CreateTrainingAssistantControl(IMain main) { return new Wiz123TrainingAssistantControl(main); }
        public override string ShopWindowTitle { get { return "Items for Sale"; } }
        public override int CharacterSize { get { return Wiz5Character.SizeInBytes; } }
        public override BaseCharacter CreateCharFromBytes(byte[] bytes) { return Wiz5Character.Create(0, bytes, 0, null, null); }
        protected override int InventoryOffset { get { return Wiz5.Offsets.Inventory; } }
        protected override int InventoryLength { get { return Wiz5.Offsets.InventoryLength; } }
        protected override WizInventory CreateInventory(List<Item> items) { return new Wiz5Inventory(items); }
        protected override WizInventory CreateInventory(byte[] bytesChar) { return new Wiz5Inventory(Game, bytesChar, InventoryOffset); }
        public override byte[] GetInventoryCharBytes() { return Properties.Resources.Wizardry_5_Inventory_Char; }
        public override string RaiseStatChance { get { return "The chance to raise a sub-18 stat is 50%, minus 0.32% for every year of age"; } }
        public override RosterFile CreateRoster(string strFile, bool bSilent) { return Wiz5RosterFile.CreateWiz5(strFile, bSilent); }
        public override string DefaultRosterFileName { get { return "SAVE5.DSK"; } }

        public override string LowerStatChance
        {
            get
            {
                return "The chance to lower a sub-18 stat depends on your age and level:\r\n" +
                    "1-2\tPractically zero\r\n" + 
                    "3-9\t0.01% per year of age\r\n" +
                    "10+\t0.12% per year of age (maximum of 20% at 150 years)";
            }
        }

        public override string LowerMaxStatChance
        {
            get
            {
                return "The chance to lower an 18 stat depends on your age and level:\r\n" +
                    "1-2\tPractically zero\r\n" +
                    "3-9\t0.001% per year of age\r\n" +
                    "10+\t0.012% per year of age (maximum of 1.8% at 150 years)";
            }
        }

        private Wiz5ActiveSquares m_lastActiveSquares = null;

        public static MapTitleInfo GetMapTitlePair(int index)
        {
            switch ((Wiz5Map)index)
            {
                default: return new MapTitleInfo(index, String.Format("Maze Level {0}", index));
            }
        }

        public override MapTitleInfo GetMapTitle(int index) { return GetMapTitlePair(index); }

        protected override WizEncounterInfo CreateEncounterInfo(WizGameState state, byte[] bytesEncounter, Point ptPartyPosition, int iRewardModifier, int offset = 0)
        {
            if (state.InCombat)
                return new Wiz5EncounterInfo(state, bytesEncounter, ptPartyPosition, iRewardModifier, offset);
            return null;
        }

        public Wiz5MemoryHacker()
        {
            m_game = GameNames.Wizardry5;
        }

        protected override void OnReinitialized(EventArgs e)
        {
            Wiz5.MonsterList.Value.Reinitialize(this, false);
            Wiz5.ItemList.Value.InitExternalList(this);
            base.OnReinitialized(e);
        }

        protected override MainState GetMainState(int state1, int state2, int state3, int state4, int state5)
        {
            switch (state1)
            {
                case 0x8DCA: return MainState.EdgeOfTown;
                case 0x84D2: return MainState.Castle;
                case 0x83AA: return MainState.Tavern;
                case 0x85C6:
                case 0x85A0:
                case 0x85C8:
                case 0x85A4: return MainState.Inn;
                case 0x8B88: return MainState.Temple;
                case 0x8ABC:
                case 0x8D52: return MainState.Shop;
                case 0x85F8: return MainState.TavernRemoveChar;
                case 0x8594: return MainState.TavernAddChar;
                case 0x8738: return MainState.CampInspecting;
                //case 0x8738: return MainState.TrainingInspecting;
                //case 0x8738: return MainState.TavernInspect;
                case 0x8748: return MainState.TavernInspectRead;
                case 0x8920: return MainState.TavernInspectTrade;
                case 0x8D84: return MainState.Utilities;
                case 0x8ED6: return MainState.ChangeName;
                case 0x841A: return MainState.MoveInsertDisc;
                case 0x8242: return MainState.Training;
                case 0x83CC: return MainState.TrainingInspectSelectChar;
                case 0x82F8: return MainState.TrainingInspectCharSelected;
                //case 0x8748: return MainState.TrainingInspectRead;
                case 0x8424: return MainState.TrainingInspectChangeClass;
                case 0x83CA: return MainState.CreateSelectPassword;
                case 0x8220: return MainState.CreateSelectName;
                case 0x83DC: 
                    switch(state2)
                    {
                        case 0x329C: return MainState.CreateSelectRace;
                        case 0x301A: return MainState.CreateSelectAlignment;
                        default: return MainState.CreateSelectAlignment;
                    }
                case 0x841C: return MainState.CreateExchangeStat;

                case 0x84CE: return MainState.Roster;
                case 0x8880: return MainState.Camp;
                case 0x776A: return MainState.TreasureWhoWillOpen;
                case 0x7768: return MainState.TreasureWhoWillCalfo;
                case 0x7764: return MainState.TreasureWhoWillInspect;
                case 0x76F0: return MainState.TreasureCouldNotDisarm;
                case 0x76CC: return MainState.TreasureEnterTrapType;
                case 0x76C0: return MainState.TreasureWhoWillDisarm;
                case 0x8958: return MainState.UseSelectItem;
                case 0x8990: return MainState.DropSelectItem;
                case 0x88D8: return MainState.SelectSpell;
                case 0x9468: return MainState.CampReorder;
                case 0x4476:
                case 0x94C6: return MainState.CampEquip;
                case 0x153E:
                case 0x7C7E: return MainState.Adventuring;
                case 0x72CA: return MainState.CombatConfirmRound;
                    // These states happen frequently when loading a map, so don't just treat them as combat without a secondary check
                case 0x0092:
                case 0x00DA:
                case 0x00DD:
                case 0x00E3:
                case 0x010B:
                case 0x01F9:
                case 0x0361:
                case 0x3BE4:
                case 0x71CE:
                case 0x71D2:
                case 0x7278:
                case 0x72D0:
                    return MainState.Combat;
                case 0x00C9:
                case 0x7579: return MainState.CombatUseItem;
                case 0x010D:
                case 0x726C: return MainState.CombatSelectSpell;
                case 0xCAA6: return MainState.CombatSelectFightTarget;
                case 0x7168: return MainState.CombatOptions;
                case 0x83B2: return MainState.CombatFriendly;
                case 0x000A:
                    switch (state2)
                    {
                        case 0x2B34: return MainState.Unknown;
                        default: return MainState.Treasure;
                    }
                case 0x7778:
                case 0x7786:
                case 0x777E:
                case 0x752A: return MainState.Treasure; 
                case 0x00005438: return MainState.PreCombat;
                case 0x843a:
                case 0x39E6: return MainState.ReceiveExp;
                case 0x77C6: return MainState.ReceiveGold;
                case 0x87EC: return MainState.Opening;
                case 0x0000514C: return MainState.InsertDisk;
                case 0x7B5E: return MainState.Barter;
                case 0x7C1E:
                case 0x7C94: // Picking....
                case 0x7C3C: // Who will pick the lock?
                case 0x7C40: // Who will cast Desto?
                case 0x7C54: // Inspect secret doors
                case 0x7C7C: // Inspect hidden items
                case 0x866A: // Who will use an item?
                case 0x7CB0: // Inspect dead bodies
                case 0x7B00: // Pick locks
                case 0x7B16: // Press button?
                case 0x7C70: // Take stairs?
                case 0x7817: // Please wait...
                case 0x7BA0: // Who will wade?
                case 0x7BCE: // Press enter
                    return MainState.Question;

                // Any of this pile of states can happen while moving between screens or maps
                case 0x0000: case 0x0001: case 0x000B: case 0x0020: case 0x0028: case 0x0056: case 0x007B: case 0x007F: case 0x0093: case 0x009E:
                case 0x00BB: case 0x00C1: case 0x00C3: case 0x00C6: case 0x00D7: case 0x00EA: case 0x00EE: case 0x0112: case 0x0116: case 0x0124:
                case 0x0125: case 0x0133: case 0x013A: case 0x013E: case 0x0163: case 0x0165: case 0x0166: case 0x019C: case 0x01A6: case 0x021A:
                case 0x0221: case 0x0222: case 0x023B: case 0x0240: case 0x053E: case 0x0736: case 0x1918: case 0x1999: case 0x2A26: case 0x2B96:
                case 0x4406: case 0x4420: case 0x7046: case 0x7082: case 0x7202: case 0x7283: case 0x7813: case 0x9632: case 0xAF58: case 0xE98F:
                case 0xF000: case 0xFEAE: case 0x019F: case 0x7287: case 0x0111: case 0x0099: case 0x0009: case 0x007D: case 0x009B: case 0x0135:
                case 0x0139: case 0x0160: case 0x0198: case 0x0210: case 0x0217: case 0x023E: case 0x7002: case 0x4544: case 0x7246: case 0x7207:
                case 0xAFAE: case 0x7047: case 0x41E4: case 0x441C:

                    return MainState.Transitional;
                default:
                    return MainState.Unknown;
            }
        }

        public override Point GetPartyPosition()
        {
            if (!IsValid)
                return Global.NullPoint;

            MemoryBytes pos = ReadOffset(Memory.LocationNorth, 4);
            if (pos == null)
                return Global.NullPoint;
            return new Point(pos.Bytes[2] - 129, pos.Bytes[0] - 129);
        }

        public override bool SetLocation(Point ptLocation)
        {
            if (!IsValid)
                return false;

            byte[] bytes = new byte[4];
            Global.SetInt16(bytes, 0, ptLocation.Y + 129);
            Global.SetInt16(bytes, 2, ptLocation.X + 129);
            return WriteOffset(Memory.LocationNorth, bytes);
        }

        public override IEnumerable<Monster> GetMonsterList() { return Wiz5.Monsters; }

        public Wiz5SpecialSquares GetSpecialSquares()
        {
            if (!IsValid)
                return null;

            MemoryBytes mb = ReadOffset(Wiz5.Memory.Map, 2048);
            return new Wiz5SpecialSquares(mb.Bytes, GetCurrentMapIndex());
        }

        protected override List<Item> GetSuperItems(WizClass wizClass, WizAlignment alignment)
        {
            bool bEvil = alignment == WizAlignment.Evil;
            bool bGood = alignment == WizAlignment.Good;

            List<Item> items = new List<Item>(8);
            WizItem chain = Wiz5.Items[(int)Wiz5ItemIndex.ChainMailPlus2];
            WizItem gold = Wiz5.Items[(int)Wiz5ItemIndex.GoldPlatePlus5];
            WizItem shield = Wiz5.Items[(int)Wiz5ItemIndex.HeaterPlus2];
            WizItem leather = Wiz5.Items[(int)Wiz5ItemIndex.LeatherPlus2];
            WizItem gloves = Wiz5.Items[(int)Wiz5ItemIndex.GlovesOfMyrdall];
            WizItem leathergloves = Wiz5.Items[(int)Wiz5ItemIndex.LeatherGloves];
            WizItem cloak = Wiz5.Items[(int)Wiz5ItemIndex.CloakOfCapricorn];
            WizItem armet = Wiz5.Items[(int)Wiz5ItemIndex.JeweledArmet];
            WizItem bascinet = Wiz5.Items[(int)Wiz5ItemIndex.Bascinet];
            WizItem sallet = Wiz5.Items[(int)Wiz5ItemIndex.BrassSallet];
            WizItem katana = Wiz5.Items[(int)Wiz5ItemIndex.MuramasaKatana];
            WizItem bow = Wiz5.Items[(int)Wiz5ItemIndex.SylvanBow];
            WizItem rod = Wiz5.Items[(int)Wiz5ItemIndex.LightningRod];
            WizItem wonder = Wiz5.Items[(int)Wiz5ItemIndex.AnkhOfWonder];
            WizItem magic = Wiz5.Items[(int)Wiz5ItemIndex.ShieldProMagic];

            switch (wizClass)
            {
                case WizClass.Fighter:
                    items.Add(bow);
                    items.Add(gold);
                    items.Add(armet);
                    items.Add(gloves);
                    items.Add(shield);
                    break;
                case WizClass.Samurai:
                    items.Add(katana);
                    items.Add(gold);
                    items.Add(bascinet);
                    items.Add(gloves);
                    items.Add(magic);
                    break;
                case WizClass.Lord:
                    items.Add(Wiz5.Items[(int)Wiz5ItemIndex.Odinsword]);
                    items.Add(gold);
                    items.Add(armet);
                    items.Add(gloves);
                    items.Add(shield);
                    break;
                case WizClass.Mage:
                    items.Add(rod);
                    if (bGood)
                        items.Add(Wiz5.Items[(int)Wiz5ItemIndex.EmeraldRobes]);
                    else if (bEvil)
                        items.Add(Wiz5.Items[(int)Wiz5ItemIndex.ScarletRobes]);
                    else
                        items.Add(Wiz5.Items[(int)Wiz5ItemIndex.Robes]);
                    items.Add(Wiz5.Items[(int)Wiz5ItemIndex.WizardsCap]);
                    break;
                case WizClass.Priest:
                    items.Add(Wiz5.Items[(int)Wiz5ItemIndex.SacredBasher]);
                    items.Add(chain);
                    items.Add(sallet);
                    items.Add(leathergloves);
                    break;
                case WizClass.Thief:
                    items.Add(bow);
                    items.Add(leather);
                    items.Add(leathergloves);
                    items.Add(Wiz5.Items[(int)Wiz5ItemIndex.TargetPlus1]);
                    break;
                case WizClass.Bishop:
                    items.Add(rod);
                    items.Add(chain);
                    items.Add(sallet);
                    items.Add(leathergloves);
                    break;
                case WizClass.Ninja:
                    items.Add(katana);
                    items.Add(gold);
                    items.Add(bascinet);
                    items.Add(gloves);
                    items.Add(magic);
                    break;
                default:
                    break;
            }

            items.Add(Wiz5.Items[(int)Wiz5ItemIndex.CloakOfCapricorn]);

            return items;
        }

        public override bool CreateSuperCharacter(int iAddress)
        {
            if (!IsValid)
                return false;

            int offset = iAddress * Wiz5Character.SizeInBytes;
            CharacterOffsets offsets = Wiz5.Offsets;

            PartyInfo info = GetPartyInfo();
            if (offset + Wiz5Character.SizeInBytes > info.Bytes.Length + 1)
                return false;

            WizClass wizClass = (WizClass)info.Bytes[offset + offsets.Class];

            byte[] bytes = new PackedFiveBitValues(18, 18, 18, 18, 18, 18).Bytes;   // Stats
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Stats, bytes.Length);
            bytes = new PackedFiveBitValues(0, 0, 0, 0, 0).Bytes;   // Saving throws
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.SavingThrows, bytes.Length);
            info.Bytes[offset + offsets.Condition] = (byte)WizCondition.Good;
            Global.SetInt16(info.Bytes, offset + offsets.Age, 14 * 52);
            Global.SetInt16(info.Bytes, offset + offsets.Level, 99);
            Global.SetInt16(info.Bytes, offset + offsets.LevelMod, 99);
            Global.SetInt16(info.Bytes, offset + offsets.CurrentHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.MaxHP, 9999);
            Global.SetInt16(info.Bytes, offset + offsets.ArmorClass, -10);
            // Global.SetInt16(info.Bytes, offset + offsets.LastArmorClass, -10);
            bytes = WizardryLong.GetBytes(99999999999);
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Gold, bytes.Length);
            bytes = WizardryLong.GetBytes(new Wiz5Character().XPForLevel(wizClass, 99));
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Experience, bytes.Length);
            bytes = new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.Spells, bytes.Length);
            bytes = Global.NullBytes(16);
            for (int i = 0; i < 7; i++)
            {
                bytes[i] = 9;
                bytes[i + 8] = 9;
            }
            Buffer.BlockCopy(bytes, 0, info.Bytes, offset + offsets.CurrentSP, bytes.Length);

            WriteOffset(Memory.PartyInfo, info.Bytes);

            List<Item> items = GetSuperItems(wizClass, (WizAlignment)info.Bytes[offset + offsets.Alignment]);

            foreach (WizItem item in items)
                item.Identified = true;

            SetBackpack(iAddress, items, true);

            return true;
        }

        public override RosterFile CreateRoster(bool bSilent)
        {
            return Wiz5RosterFile.CreateWiz5(Global.CombineRoster(Game), bSilent);
        }

        public override List<MapTitleInfo> GetMapTitles()
        {
            List<MapTitleInfo> maps = new List<MapTitleInfo>(14);
            for (Wiz5Map map = Wiz5Map.First; map < Wiz5Map.Last; map++)
                maps.Add(GetMapTitlePair((int)map));
            return maps;
        }

        private Wiz5PartyInfo ReadWiz5PartyInfo()
        {
            byte numChars = (byte) GetNumChars();
            if (numChars > 6)
                numChars = 6;
            if (m_block == null)
                return null;
            if (numChars == 0)
                return new Wiz5PartyInfo(new byte[0], 0);

            MemoryBytes mbParty = ReadOffset(Wiz5.Memory.PartyInfo, numChars * Wiz5Character.SizeInBytes);
            if (mbParty == null)
                return null;

            Wiz5PartyInfo info = new Wiz5PartyInfo(mbParty, numChars);

            info.State = GetGameState() as WizGameState;
            info.ActingChar = (byte)GetInspectingChar(info.State.Main);
            info.ActingCombatChar = ReadByte(Wiz5.Memory.CombatOptionActiveChar);
            if (info.ActingChar >= numChars && info.ActingCombatChar < numChars)
                info.ActingChar = info.ActingCombatChar;
            info.ActingCaster = info.ActingChar;

            return info;
        }

        public override PartyInfo GetPartyInfo()
        {
            if (!IsValid)
                return null;

            return ReadWiz5PartyInfo();
        }

        public override GameInfo GetGameInfo()
        {
            if (!IsValid)
                return null;

            Wiz5GameInfo info = CreateGameInfo() as Wiz5GameInfo;
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

            info.Walls = ReadUInt16(Wiz5.Memory.ActivatedBits + 64);
            info.TrapType = ReadInt16(Wiz5.Memory.TrapType);
            info.Light = ReadInt16(Wiz5.Memory.Light);
            info.Levitate = ReadInt16(Wiz5.Memory.Levitate);
            info.Identify = ReadInt16(Wiz5.Memory.Identify);
            info.MagicScreen = ReadInt16(Wiz5.Memory.MagicScreen);
            info.FizzleField = ReadInt16(Wiz5.Memory.FizzleFieldStrength);
            info.ACBonus = ReadInt16(Wiz5.Memory.ACBonus);
            info.ActivatedBits = new byte[8][];
            info.State1 = ReadUInt16(Wiz5.Memory.State1);
            info.State2 = ReadUInt16(Wiz5.Memory.State2);
            info.SummonedMonster = ReadUInt16(Wiz5.Memory.SummonedMonsterIndex);
            for (int i = 0; i < info.ActivatedBits.Length; i++)
            {
                info.ActivatedBits[i] = ReadOffset(Wiz5.Memory.ActivatedBits + (i * 8), 8);
                stream.Write(info.ActivatedBits[i], 0, info.ActivatedBits[i].Length);
            }
            Global.WriteUInt16(stream, info.Walls);
            Global.WriteInt16(stream, info.TrapType);
            Global.WriteInt16(stream, info.Light);
            Global.WriteInt16(stream, info.Levitate);
            Global.WriteInt16(stream, info.Identify);
            Global.WriteInt16(stream, info.FizzleField);
            Global.WriteInt16(stream, info.ACBonus);
            Global.WriteInt16(stream, info.MagicScreen);

            // Don't write the state information; use manual refresh for this debug information

            info.Bytes = stream.ToArray();

            return info;
        }

        public override byte[] GetBackpackBytes(int iCharAddress)
        {
            MemoryBytes mb = ReadOffset(Wiz5.Memory.PartyInfo + (iCharAddress * Wiz5Character.SizeInBytes) + Wiz5.Offsets.Inventory, 1 + (4 * 8));
            if (mb == null)
                return null;
            return mb.Bytes;
        }

        public override bool SetBackpackBytes(int iCharAddress, byte[] bytes)
        {
            return WriteOffset(Wiz5.Memory.PartyInfo + (iCharAddress * Wiz5Character.SizeInBytes) + Wiz5.Offsets.Inventory, bytes);
        }

        protected override LocationInformation GetLocationForce()
        {
            LocationInformation info = base.GetLocationForce();
            return info;
        }

        public override bool SetEncounterInfo(EncounterInfo info)
        {
            if (!(info is Wiz5EncounterInfo) || !IsValid)
                return false;

            Wiz5EncounterInfo wi = info as Wiz5EncounterInfo;

            byte[] bytes = wi.GetBytes();
            if (bytes == null)
                return false;

            return WriteOffset(Memory.EncounterInfo, bytes);
        }

        public override List<BaseCharacter> GetCharacters()
        {
            Wiz5PartyInfo party = ReadWiz5PartyInfo();
            if (party == null)
                return null;

            Wiz5GameInfo gameInfo = GetGameInfo() as Wiz5GameInfo;
            Wiz5EncounterInfo encounterInfo = GetEncounterInfo() as Wiz5EncounterInfo;

            Wiz5Character[] chars = new Wiz5Character[party.NumChars];
            for (int i = 0; i < party.NumChars; i++)
                chars[i] = Wiz5Character.Create(i, party.Bytes, i * Wiz5Character.SizeInBytes, gameInfo, encounterInfo);

            return new List<BaseCharacter>(chars);
        }

        protected override WizQuestData CreateQuestData()
        {
            WizPartyInfo party = GetPartyInfo() as WizPartyInfo;
            if (party == null)
                return null;
            MemoryBytes bits = ReadOffset(Wiz5.Memory.ActivatedBits, 512);
            if (bits == null)
                return null;
            MemoryBytes aux3 = ReadOffset(Wiz5.Memory.MapAux3, 192);
            if (aux3 == null)
                return null;
            return new Wiz5QuestData(party, GetLocation(), GetGameState() as WizGameState, bits.Bytes, aux3.Bytes, ReadUInt16(Wiz5.Memory.SummonedMonsterIndex));
        }

        public override bool SetCharacterBytes(int iAddress, byte[] bytes)
        {
            return WriteOffset(Memory.PartyInfo + (iAddress * Wiz5Character.SizeInBytes), bytes, Math.Min(Wiz5Character.SizeInBytes, bytes.Length));
        }

        public override WizSpellInfo CreateSpellInfo() { return new Wiz5SpellInfo(); }

        public override MapData GetMapData(bool bIncludeStrings, int iMapIndex)
        {
            MemoryBytes bytes = ReadOffset(Memory.Map, 2048);
            if (bytes == null)
                return null;

            MemoryBytes bytesDark = ReadOffset(Wiz5.Memory.MapDarkBits, 512);
            if (bytesDark == null)
                return null;

            return new Wiz5MapData(GetCurrentMapIndex(), bytes.Bytes, bytesDark.Bytes);
        }

        private bool MapMismatch(int iMap, byte[] bytesMap)
        {
            // The map level changes a fair bit of time before the map bytes are actually ready.  This function
            // will return "true" if the section remap bytes are not the known correct values for the map.
            // (avoids issues where the active squares are shown for the previous map on top of the current map)

            if (iMap < 1 || iMap > 8)
                return true;   // Not even a valid map, let alone a non-mismatch

            return !Global.CompareBytes(Properties.Resources.Wizardry_5_Sector_Remap, bytesMap, (iMap - 1) * 32, Wiz5.Memory.MapBlockPosY - Wiz5.Memory.Map, 32);
        }

        public override ActiveSquares GetActiveSquares(MainForm form, bool bForce = false)
        {
            if (!IsValid)
                return null;

            switch (GetGameState().Main)
            {
                case MainState.LoadingMap:
                    return null;
                default:
                    break;
            }

            int iMap = GetCurrentMapIndex();

            MemoryBytes map = ReadOffset(Wiz5.Memory.Map, 2048);
            if (map == null)
                return null;

            if (MapMismatch(iMap, map.Bytes))
                return ActiveSquares.CreateAllInactive;

            MemoryBytes active = ReadOffset(Wiz5.Memory.ActiveSquares, 256);
            if (active == null)
                return null;

            MemoryBytes bits = ReadOffset(Wiz5.Memory.ActivatedBits, 64);
            if (bits == null)
                return null;

            if (!bForce && m_lastActiveSquares != null && m_lastActiveSquares.BytesEqual(map, active, bits))
                return m_lastActiveSquares;

            m_lastActiveSquares = new Wiz5ActiveSquares(form, iMap, map.Bytes, active.Bytes, bits.Bytes);

            return m_lastActiveSquares;
        }

        public override EncounterInfo GetEncounterInfo(bool bForceNew = false)
        {
            if (!IsValid)
                return null;

            WizGameState state = GetGameState() as WizGameState;

            switch (state.Main)
            {
                case MainState.Opening:
                case MainState.Opening2:
                case MainState.MainMenu:
                case MainState.SelectSave:
                    return null;
                default:
                    break;
            }

            if (!state.InCombat)
                return null;

            MemoryBytes mbEncounter = ReadOffset(Memory.EncounterInfo, Wiz5EncounterInfo.Size);
            if (mbEncounter == null)
                return null;

            byte[] bytesEncounter = mbEncounter.Bytes;
            byte[] bytesMod = ReadOffset(Memory.EncounterRewardModifier, 2).Bytes;
            WizPartyInfo party = GetPartyInfo() as WizPartyInfo;
            byte[] bytesReward = ReadOffset(Memory.RewardIndex, 1).Bytes;
            byte[] bytes = Global.Combine(bytesEncounter, party.Bytes, bytesMod, bytesReward);
            if (m_lastEncounterInfo != null && m_lastEncounterInfo.HasTreasure != (state.Main == MainState.Treasure))
                bForceNew = true;
            if (!bForceNew && m_lastEncounterInfo != null && Global.Compare(bytes, m_lastEncounterInfo.AllBytes))
                return m_lastEncounterInfo;

            m_lastEncounterInfo = CreateEncounterInfo(state, bytesEncounter, GetPartyPosition(), bytesMod[0]);
            m_lastEncounterInfo.Party = party;
            m_lastEncounterInfo.AllBytes = bytes;
            m_lastEncounterInfo.CreateSearchResults(bytesReward[0]);
            return m_lastEncounterInfo;
        }

        public override string GetDebugMemoryInfo()
        {
            // return GetDebugSpecialSquaresCode();
            return GetDebugSpecialSquaresInfo();
            // return GetDebugSpots();
        }

        private string GetDebugSpots()
        {
            Wiz5SpecialSquares special = GetSpecialSquares();
            StringBuilder sb = new StringBuilder();
            int iMap = GetCurrentMapIndex();
            string strLast = null;
            foreach (List<Wiz5SpecialSquare> list in special.Squares.Values)
            {
                foreach(Wiz5SpecialSquare square in list)
                {
                    string str = String.Format("        public MapXY L{0}_{1:X2}{2:X2} = new MapXY(Wiz5Map.MazeLevel{0}, {3}, {4});\r\n",
                        iMap, square.Location.X + 129, square.Location.Y + 129, square.Location.X, square.Location.Y);
                    if (str != strLast)
                    {
                        sb.Append(str);
                        strLast = str;
                    }
                }
            }
            return sb.ToString();
        }

        private string GetDebugSpecialSquaresCode()
        {
            StringBuilder sb = new StringBuilder();

            MemoryBytes mb = ReadOffset(Wiz5.Memory.Map, 2048);
            Wiz5SpecialSquares squares = new Wiz5SpecialSquares(mb.Bytes, GetCurrentMapIndex());
            int iMap = GetCurrentMapIndex();
            sb.AppendFormat("                case {0,3} +  0: return new BitDesc(\"\", MapXY.Empty, \"All unassigned scripts\");\r\n", (iMap - 1) * 64);

            Dictionary<int, Wiz5SpecialSquare> dict = new Dictionary<int, Wiz5SpecialSquare>();

            foreach (List<Wiz5SpecialSquare> list in squares.Squares.Values)
            {
                foreach (Wiz5SpecialSquare square in list)
                {
                    if (!dict.ContainsKey(square.Value0))
                        dict.Add(square.Value0, square);
                }
            }

            for(int iBit = 1; iBit < 64; iBit++)
            {
                if (!dict.ContainsKey(iBit))
                    continue;

                Wiz5SpecialSquare square = dict[iBit];

                sb.AppendFormat("                case {0,3} + {1,2}: return new BitDesc(\"\", Wiz5.Spots.L{2}_{3:X2}{4:X2}, \"Enter the square\");\r\n",
                    (iMap - 1) * 64, iBit, iMap, square.Location.X + 129, square.Location.Y + 129);
            }

            return sb.ToString();
        }

        private string GetDebugSpecialSquaresInfo()
        {
            StringBuilder sb = new StringBuilder();

            MemoryBytes mb = ReadOffset(Wiz5.Memory.Map, 2048);
            Wiz5SpecialSquares squares = new Wiz5SpecialSquares(mb.Bytes, GetCurrentMapIndex());
            int iMap = GetCurrentMapIndex();

            foreach(Point ptMap in squares.Squares.Keys)
            {
                foreach (Wiz5SpecialSquare square in squares.Squares[ptMap])
                {
                    string strAux = String.Format("{0},{1:X4},{2},{3:X4}", square.Value0, square.Value1, square.Value2, square.Value3);

                    sb.AppendFormat("{0,3} ", square.Value0);
                    sb.AppendFormat("Specials[{0},{1}] ({2},{3},{4}): {5} [{6}]",
                        ptMap.X, ptMap.Y, square.Section, square.SubX, square.SubY, (int) square.Type, strAux);
                    switch (square.Type)
                    {
                        case Wiz5SquareType.SwitchMap:
                            // These don't seem consistently correct
                            //switch (aux2[i])
                            //{
                            //    case 0:
                            //        sb.Append("  Chute to ");
                            //        break;
                            //    case 1:
                            //        sb.Append("  Stairs Up to ");
                            //        break;
                            //    case 2:
                            //        sb.Append("  Stairs Down to ");
                            //        break;
                            //    default:
                            //        sb.Append("  Teleport to ");
                            //        break;
                            //}
                            sb.Append("  Change map to ");
                            Wiz5LocationInt li = new Wiz5LocationInt(square.Value3);
                            sb.AppendFormat("level {0} section {1} ({2},{3})", li.Level, li.Section, li.X, li.Y);
                            break;
                        case Wiz5SquareType.Move:
                            string strSquares = Global.Plural(square.Value3 & 0xff, "square");
                            if ((square.Value3 & 0x0100) > 0)
                                sb.AppendFormat("  Move north {0}", strSquares);
                            if ((square.Value3 & 0x0200) > 0)
                                sb.AppendFormat("  Move east {0}", strSquares);
                            if ((square.Value3 & 0x0400) > 0)
                                sb.AppendFormat("  Move south {0}", strSquares);
                            if ((square.Value3 & 0x0800) > 0)
                                sb.AppendFormat("  Move west {0}", strSquares);
                            break;
                        case Wiz5SquareType.Receive:
                            if (square.Value2 != 0)
                                sb.AppendFormat("  Receive Item #{0} ({1})", square.Value2, Wiz5.ItemName(square.Value2));
                            else
                                sb.AppendFormat("  Receive Item");
                            break;
                        case Wiz5SquareType.Encounter:
                            if (square.Value2 != 0)
                                sb.AppendFormat("  Encounter with #{0} ({1})", square.Value2, Wiz5.MonsterName(square.Value2));
                            else
                                sb.AppendFormat("  Encounter");
                            break;
                        case Wiz5SquareType.CheckItem:
                            if (square.Value2 != 0)
                                sb.AppendFormat("  Check Item #{0} ({1})", square.Value2, Wiz5.ItemName(square.Value2));
                            else
                                sb.AppendFormat("  Check Item");
                            break;
                        case Wiz5SquareType.AlterMap:
                            sb.Append("  Map: ");
                            int[] iNESW = new int[4];
                            iNESW[0] = square.Value3 & 0x0003;
                            iNESW[1] = (square.Value3 & 0x000c) >> 2;
                            iNESW[2] = (square.Value3 & 0x0030) >> 4;
                            iNESW[3] = (square.Value3 & 0x00c0) >> 6;
                            for (int iDir = 0; iDir < 4; iDir++)
                            {
                                switch (iNESW[iDir])
                                {
                                    case 1:
                                        sb.AppendFormat("lock1 {0}, ", Global.NESW[iDir]);
                                        break;
                                    case 2:
                                        sb.AppendFormat("lock2 {0}, ", Global.NESW[iDir]);
                                        break;
                                    case 3:
                                        sb.AppendFormat("barrier {0}, ", Global.NESW[iDir]);
                                        break;
                                    default:
                                        break;
                                }
                            }
                            Global.Trim(sb);
                            break;
                        case Wiz5SquareType.Message:
                            sb.Append("  Message (no indicator)");
                            break;
                        case Wiz5SquareType.Trap:
                            sb.AppendFormat("  Pit ({0}d{1} damage)", (square.Value3 & 0x00f0) >> 4, square.Value3 & 0x000f);
                            break;
                        case Wiz5SquareType.Unknown14:
                            sb.AppendFormat("  Pool (depth {0})", square.Value2);
                            break;
                        case Wiz5SquareType.BlueMessage:
                            sb.Append("  Message (blue square)");
                            break;
                        default:
                            break;

                    }
                    sb.AppendLine();
                }
            }

            return sb.ToString();
        }

        public override Shops GetShopInfo()
        {
            Shops shops = new Shops();

            GameState state = GetGameState();
            shops.InShop = (state.Main == MainState.Barter || state.Main == MainState.Shop);

            if (!shops.InShop)
                return null;

            LocationInformation info = GetLocation();

            // "Shops" in Wizardry 5 are either Boltac's in the Castle or the wandering characters such as Ironose

            shops.Inventories = new List<ShopInventory>(1);
            MemoryBytes bytesShops = null;
            Wiz5ShopInventory inv = null;

            if (info.MapIndex == 0 || info.MapIndex == 255)
            {
                bytesShops = ReadOffset(Wiz5.Memory.BoltacCurrent - 2, 14);
                if (bytesShops == null)
                    return shops;

                Global.SetInt16(bytesShops, 0, 6);
                inv = new Wiz5ShopInventory(bytesShops, 0);
                inv.Town = "Boltac's";
            }
            else
            {
                bytesShops = ReadOffset(Wiz5.Memory.BuyFromNPC, 128);
                if (bytesShops == null)
                    return shops;
                inv = new Wiz5ShopInventory(bytesShops, 104);
                inv.Town = Global.PascalString(bytesShops, 0, 15);
            }

            if (inv.AllItems == null)
                return null;
            shops.RawBytes = bytesShops;
            shops.Inventories.Add(inv);

            return shops;
        }

        public override MapBytes GetCurrentMapBytes()
        {
            if (GetGameState().Main != MainState.Adventuring)
            {
                m_iCurrentMapAdventuringCount = 2;
                return null;    // The in-memory map only exists during the "Adventuring" state
            }

            if (m_iCurrentMapAdventuringCount > 0)
            {
                // Don't try to read the map the instant it becomes available in adventuring mode; it might be in a transition state
                m_iCurrentMapAdventuringCount--;
                return null;
            }

            MemoryBytes bytes = ReadOffset(Memory.Map, 546);
            if (bytes == null)
                return null;

            // Wizardry often has trash in the map data space during special events; this tries to avoid
            // returning the trash as valid data at least some of the time.
            if (m_bytesLastLiveMap != null && Global.NumDifferences(bytes.Bytes, m_bytesLastLiveMap, 64) > 32)
                return new MapBytes(m_bytesLastLiveMap, 32, 32);

            return new MapBytes(bytes, 32, 32);
        }

        public override MapData CreateLiveMapData(MapBytes mb)
        {
            if (mb == null || mb.Bytes == null)
                return null;
            Wiz5MapData data = new Wiz5MapData(0, mb.Bytes, null);
            data.LiveOnly = true;
            return data;
        }

        public override bool PartyInfoChanged(GameInfo info1, GameInfo info2)
        {
            Wiz5GameInfo w5Info1 = info1 as Wiz5GameInfo;
            Wiz5GameInfo w5Info2 = info2 as Wiz5GameInfo;

            if (w5Info1 == null || w5Info2 == null)
                return false;   // Not a valid comparison

            if (w5Info1.ACBonus != w5Info2.ACBonus)
                return true;

            return false;
        }

        public override bool SetTrainingInfo(TrainingInfo info)
        {
            if (!IsValid)
                return false;

            if (info is Wiz123TrainingInfo)
            {
                Wiz123TrainingInfo wiz1Info = info as Wiz123TrainingInfo;
                byte[] bytes = new byte[4];
                Buffer.BlockCopy(info.Party.Bytes, wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz5.Offsets.Stats, bytes, 0, 4);
                WriteOffset(Memory.PartyInfo + (wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz5.Offsets.Stats), bytes);

                UInt16 iCurrentHP = BitConverter.ToUInt16(info.Party.Bytes, wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz5.Offsets.CurrentHP);
                UInt16 iMaxHP = BitConverter.ToUInt16(info.Party.Bytes, wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz5.Offsets.MaxHP);

                WriteUInt16(Memory.PartyInfo + (wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz5.Offsets.CurrentHP), iCurrentHP);
                return WriteUInt16(Memory.PartyInfo + (wiz1Info.Party.ActingChar * wiz1Info.Party.CharacterSize + Wiz5.Offsets.MaxHP), iMaxHP);
            }

            return false;
        }

        public override bool SetMonster(Monster monster) { return false; } // Monsters are compressed and currently not editable

        public override CureAllInfo GetCureAllInfo(int iCasterIndex, int[] partyAddresses)
        {
            if (!IsValid)
                return null;

            if (iCasterIndex >= partyAddresses.Length)
                return null;

            Wiz5CureAllInfo info = new Wiz5CureAllInfo();
            WizPartyInfo party = GetPartyInfo() as Wiz5PartyInfo;

            info.Conditions = new WizCondition[party.NumChars];
            info.HitPoints = new Int16[party.NumChars];
            info.HitPointsMax = new Int16[party.NumChars];
            for (int i = 0; i < partyAddresses.Length; i++)
            {
                info.Conditions[i] = (WizCondition)party.Bytes[partyAddresses[i] * party.CharacterSize + Wiz5.Offsets.Condition];
                info.HitPoints[i] = BitConverter.ToInt16(party.Bytes, partyAddresses[i] * party.CharacterSize + Wiz5.Offsets.CurrentHP);
                info.HitPointsMax[i] = BitConverter.ToInt16(party.Bytes, partyAddresses[i] * party.CharacterSize + Wiz5.Offsets.MaxHP);
            }

            int iCasterAddress = partyAddresses[iCasterIndex];
            info.CasterCondition = info.Conditions[iCasterAddress];
            info.CasterSpellPoints = new Wiz5SpellPoints(party.Bytes, iCasterAddress * party.CharacterSize + Wiz5.Offsets.CurrentSP);
            info.CasterSpells = new Wiz5KnownSpells(party.Bytes, iCasterAddress * party.CharacterSize + Wiz5.Offsets.Spells);
            int map = GetCurrentMapIndex();
            info.InCastle = IsCastle(map);
            return info;
        }

        public override void SetCureAllInfo(CureAllInfo cureAll, int iCasterIndex, int[] partyAddresses)
        {
            if (!IsValid)
                return;

            if (iCasterIndex >= partyAddresses.Length)
                return;

            Wiz5CureAllInfo info = cureAll as Wiz5CureAllInfo;
            int iCasterAddress = partyAddresses[iCasterIndex];
            byte[] bytesSP = info.CasterSpellPoints.GetBytes();
            WriteOffset(Memory.PartyInfo + (iCasterAddress * Wiz5Character.SizeInBytes) + Wiz5.Offsets.CurrentSP, bytesSP);
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                WriteByte(Memory.PartyInfo + (partyAddresses[i] * Wiz5Character.SizeInBytes + Wiz5.Offsets.Condition), (byte)info.Conditions[i]);
                WriteInt16(Memory.PartyInfo + (partyAddresses[i] * Wiz5Character.SizeInBytes + Wiz5.Offsets.CurrentHP), info.HitPoints[i]);
            }
        }

        public override CureAllResult CureAll(CureAllInfo cureAllInfo)
        {
            bool bUnknownSpells = false;

            if (!(cureAllInfo is Wiz5CureAllInfo))
                return CureAllResult.Error;

            Wiz5CureAllInfo info = cureAllInfo as Wiz5CureAllInfo;

            // Okay, let's start curing!  Since Wizardry 5 cure-all requires being in the Castle,
            // we don't need to check spell points, just whether the spell is known (entering the Maze restores
            // all of your SP anyway, at least on the DOS version).
            for (int i = 0; i < info.Conditions.Length; i++)
            {
                if (info.Conditions[i] >= WizCondition.Dead)
                    continue;   // Don't deal with death and eradication; make the player do that manually

                if (info.Conditions[i] == WizCondition.Petrified)
                {
                    if (info.CasterSpells.IsKnown(Wiz5SpellIndex.Madi))
                        info.Conditions[i] = WizCondition.Good;
                    else
                        bUnknownSpells = true;
                }
                if (info.Conditions[i] == WizCondition.Paralyzed || info.Conditions[i] == WizCondition.Asleep)
                {
                    if (info.CasterSpells.IsKnown(Wiz5SpellIndex.Madi) || info.CasterSpells.IsKnown(Wiz5SpellIndex.Dialko))
                        info.Conditions[i] = WizCondition.Good;
                    else
                        bUnknownSpells = true;
                }
            }

            // Restore all HP if the caster knows any HP restoring spells at all
            if (Properties.Settings.Default.CureAllHPWithConditions)
            {
                for (int i = 0; i < info.HitPoints.Length; i++)
                {
                    if (info.HitPoints[i] < info.HitPointsMax[i])
                    {
                        if (info.CasterSpells.IsKnown(Wiz5SpellIndex.Dios) ||
                            info.CasterSpells.IsKnown(Wiz5SpellIndex.Dial) ||
                            info.CasterSpells.IsKnown(Wiz5SpellIndex.Dialma) ||
                            info.CasterSpells.IsKnown(Wiz5SpellIndex.Madi)
                            )
                        {
                            info.HitPoints[i] = info.HitPointsMax[i];
                        }
                        else
                            bUnknownSpells = true;
                    }
                }
            }

            if (bUnknownSpells)
                return CureAllResult.SpellNotKnown;
            return CureAllResult.Success;
        }

        public override TrapInfo CreateTrapInfo(int iTrap)
        {
            Wiz5TrapInfo.Wiz5Trap trap = (Wiz5TrapInfo.Wiz5Trap)iTrap;
            return new Wiz5TrapInfo(trap);
        }
    }
}
