using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhereAreWe
{
    // This is a place to collect the functions that need to be different on a per-game basis, so that the rest of the code can
    // be more game-agnostic.

    public enum GameNames
    {
        None,
        MightAndMagic1,
        MightAndMagic2,
        MightAndMagic3,
        MightAndMagic4,
        MightAndMagic45,
        MightAndMagic5,
        MightAndMagic6,
        MightAndMagic7,
        MightAndMagic8,
        MightAndMagic9,
        MightAndMagic10,
        Wizardry1,
        Wizardry2,
        Wizardry3,
        Wizardry4,
        Wizardry5,
        Wizardry6,
        Wizardry7,
        Wizardry8,
        BardsTale1,
        BardsTale2,
        BardsTale3,
        DungeonMaster1,
        DungeonMaster2,
        DungeonMaster3,
        Ultima1,
        Ultima2,
        Ultima3,
        DOSBox,
        Last
    }

    public static class Games
    {
        public static bool CorrectRoster(GameNames gameRoster, GameNames gameCurrent)
        {
            if (gameRoster == gameCurrent)
                return true;

            if (gameCurrent != GameNames.MightAndMagic45)
                return false;

            return gameRoster == GameNames.MightAndMagic4 || gameRoster == GameNames.MightAndMagic5;
        }

        public static string GameEnumString(GameNames game)
        {
            switch (game)
            {
                case GameNames.None: return "None";
                case GameNames.MightAndMagic1: return "MightAndMagic1";
                case GameNames.MightAndMagic2: return "MightAndMagic2";
                case GameNames.MightAndMagic3: return "MightAndMagic3";
                case GameNames.MightAndMagic4: return "MightAndMagic4";
                case GameNames.MightAndMagic45: return "MightAndMagic45";
                case GameNames.MightAndMagic5: return "MightAndMagic5";
                case GameNames.MightAndMagic6: return "MightAndMagic6";
                case GameNames.MightAndMagic7: return "MightAndMagic7";
                case GameNames.MightAndMagic8: return "MightAndMagic8";
                case GameNames.MightAndMagic9: return "MightAndMagic9";
                case GameNames.MightAndMagic10: return "MightAndMagic10";
                case GameNames.Wizardry1: return "Wizardry1";
                case GameNames.Wizardry2: return "Wizardry2";
                case GameNames.Wizardry3: return "Wizardry3";
                case GameNames.Wizardry4: return "Wizardry4";
                case GameNames.Wizardry5: return "Wizardry5";
                case GameNames.Wizardry6: return "Wizardry6";
                case GameNames.Wizardry7: return "Wizardry7";
                case GameNames.Wizardry8: return "Wizardry8";
                case GameNames.BardsTale1: return "BardsTale1";
                case GameNames.BardsTale2: return "BardsTale2";
                case GameNames.BardsTale3: return "BardsTale3";
                case GameNames.DungeonMaster1: return "DungeonMaster1";
                case GameNames.DungeonMaster2: return "DungeonMaster2";
                case GameNames.DungeonMaster3: return "DungeonMaster3";
                case GameNames.Ultima1: return "Ultima1";
                case GameNames.Ultima2: return "Ultima2";
                case GameNames.Ultima3: return "Ultima3";
                case GameNames.DOSBox: return "DOSBox";
                default: return String.Format("{0}", game);
            }
        }

        public static GameNames GameEnum(string str)
        {
            switch (str)
            {
                case "MightAndMagic1": return GameNames.MightAndMagic1;
                case "MightAndMagic2": return GameNames.MightAndMagic2;
                case "MightAndMagic3": return GameNames.MightAndMagic3;
                case "MightAndMagic4": return GameNames.MightAndMagic4;
                case "MightAndMagic5": return GameNames.MightAndMagic5;
                case "MightAndMagic45": return GameNames.MightAndMagic45;
                case "MightAndMagic6": return GameNames.MightAndMagic6;
                case "MightAndMagic7": return GameNames.MightAndMagic7;
                case "MightAndMagic8": return GameNames.MightAndMagic8;
                case "MightAndMagic9": return GameNames.MightAndMagic9;
                case "MightAndMagic10": return GameNames.MightAndMagic10;
                case "Wizardry1": return GameNames.Wizardry1;
                case "Wizardry2": return GameNames.Wizardry2;
                case "Wizardry3": return GameNames.Wizardry3;
                case "Wizardry4": return GameNames.Wizardry4;
                case "Wizardry5": return GameNames.Wizardry5;
                case "Wizardry6": return GameNames.Wizardry6;
                case "Wizardry7": return GameNames.Wizardry7;
                case "Wizardry8": return GameNames.Wizardry8;
                case "BardsTale1": return GameNames.BardsTale1;
                case "BardsTale2": return GameNames.BardsTale2;
                case "BardsTale3": return GameNames.BardsTale3;
                case "DungeonMaster1": return GameNames.DungeonMaster1;
                case "DungeonMaster2": return GameNames.DungeonMaster2;
                case "DungeonMaster3": return GameNames.DungeonMaster3;
                case "Ultima1": return GameNames.Ultima1;
                case "Ultima2": return GameNames.Ultima2;
                case "Ultima3": return GameNames.Ultima3;
                case "DOSBox": return GameNames.DOSBox;
                default: return GameNames.None;
            }
        }

        public static GameNames GameEnumFromShort(string str)
        {
            switch(str.ToLower())
            {
                case "mm1": return GameNames.MightAndMagic1;
                case "mm2": return GameNames.MightAndMagic2;
                case "mm3": return GameNames.MightAndMagic3;
                case "mm4": return GameNames.MightAndMagic4;
                case "mm5": return GameNames.MightAndMagic5;
                case "mm45": return GameNames.MightAndMagic45;
                case "mm6": return GameNames.MightAndMagic6;
                case "mm7": return GameNames.MightAndMagic7;
                case "mm8": return GameNames.MightAndMagic8;
                case "mm9": return GameNames.MightAndMagic9;
                case "mm10": return GameNames.MightAndMagic10;
                case "wiz1": return GameNames.Wizardry1;
                case "wiz2": return GameNames.Wizardry2;
                case "wiz3": return GameNames.Wizardry3;
                case "wiz4": return GameNames.Wizardry4;
                case "wiz5": return GameNames.Wizardry5;
                case "wiz6": return GameNames.Wizardry6;
                case "wiz7": return GameNames.Wizardry7;
                case "wiz8": return GameNames.Wizardry8;
                case "bt1": return GameNames.BardsTale1;
                case "bt2": return GameNames.BardsTale2;
                case "bt3": return GameNames.BardsTale3;
                case "dm1": return GameNames.DungeonMaster1;
                case "dm2": return GameNames.DungeonMaster2;
                case "dm3": return GameNames.DungeonMaster3;
                case "ult1": return GameNames.Ultima1;
                case "ult2": return GameNames.Ultima2;
                case "ult3": return GameNames.Ultima3;
                case "dos": return GameNames.DOSBox;
                default: return GameNames.None;
            }
        }

        public static MemoryHacker CreateHacker(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return new MM1MemoryHacker();
                case GameNames.MightAndMagic2: return new MM2MemoryHacker();
                case GameNames.MightAndMagic3: return new MM3MemoryHacker();
                case GameNames.MightAndMagic45: return new MM45MemoryHacker();
                case GameNames.Wizardry1: return new Wiz1MemoryHacker();
                case GameNames.Wizardry2: return new Wiz2MemoryHacker();
                case GameNames.Wizardry3: return new Wiz3MemoryHacker();
                case GameNames.Wizardry4: return new Wiz4MemoryHacker();
                case GameNames.Wizardry5: return new Wiz5MemoryHacker();
                case GameNames.BardsTale1: return new BT1MemoryHacker();
                case GameNames.BardsTale2: return new BT2MemoryHacker();
                case GameNames.BardsTale3: return new BT3MemoryHacker();
                default: return null;
            }
        }

        public static byte[] InternalMapBytes(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return Global.DecompressBytes(Properties.Resources.Might_and_Magic_1_waw);
                case GameNames.MightAndMagic2: return Global.DecompressBytes(Properties.Resources.Might_and_Magic_2_waw);
                case GameNames.MightAndMagic3: return Global.DecompressBytes(Properties.Resources.Might_and_Magic_3_waw);
                case GameNames.MightAndMagic45: return Global.DecompressBytes(Properties.Resources.Might_and_Magic_45_waw);
                case GameNames.Wizardry1: return Global.DecompressBytes(Properties.Resources.Wizardry_1_waw);
                case GameNames.Wizardry2: return Global.DecompressBytes(Properties.Resources.Wizardry_2_waw);
                case GameNames.Wizardry3: return Global.DecompressBytes(Properties.Resources.Wizardry_3_waw);
                case GameNames.Wizardry4: return Global.DecompressBytes(Properties.Resources.Wizardry_4_waw);
                case GameNames.Wizardry5: return Global.DecompressBytes(Properties.Resources.Wizardry_5_waw);
                case GameNames.BardsTale1: return Global.DecompressBytes(Properties.Resources.Bards_Tale_1_waw);
                case GameNames.BardsTale2: return Global.DecompressBytes(Properties.Resources.Bards_Tale_2_waw);
                case GameNames.BardsTale3: return Global.DecompressBytes(Properties.Resources.Bards_Tale_3_waw);
                default: return new byte[0];
            }
        }

        public static Race[] Races(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1:
                case GameNames.MightAndMagic2:
                case GameNames.MightAndMagic45:
                    return new Race[] { new Race(GenericRace.None), new Race(GenericRace.Human), new Race(GenericRace.Elf), new Race(GenericRace.Dwarf),
                        new Race(GenericRace.Gnome), new Race(GenericRace.HalfOrc) };
                case GameNames.MightAndMagic3:
                    return new Race[] { new Race(GenericRace.None), new Race(GenericRace.Human), new Race(GenericRace.Elf), new Race(GenericRace.Gnome),
                        new Race(GenericRace.Dwarf), new Race(GenericRace.HalfOrc) };
                case GameNames.Wizardry1: 
                case GameNames.Wizardry2: 
                case GameNames.Wizardry3: 
                case GameNames.Wizardry4: 
                case GameNames.Wizardry5:
                    return new Race[] { new Race(GenericRace.None), new Race(GenericRace.Human), new Race(GenericRace.Elf), new Race(GenericRace.Dwarf),
                        new Race(GenericRace.Gnome), new Race(GenericRace.Hobbit) };
                case GameNames.BardsTale1:
                case GameNames.BardsTale2:
                case GameNames.BardsTale3:
                    return new Race[] { new Race(GenericRace.None), new Race(GenericRace.Human), new Race(GenericRace.Elf), new Race(GenericRace.Dwarf),
                        new Race(GenericRace.Hobbit), new Race(GenericRace.HalfElf), new Race(GenericRace.HalfOrc), new Race(GenericRace.Gnome) };
                default: return new Race[0];
            }
        }

        public static string GetInternalMap(string strInternalFile)
        {
            UTF8Encoding utf8 = new UTF8Encoding();
            switch (strInternalFile)
            {
                case "Might_and_Magic_1": return utf8.GetString(InternalMapBytes(GameNames.MightAndMagic1));
                case "Might_and_Magic_2": return utf8.GetString(InternalMapBytes(GameNames.MightAndMagic2));
                case "Might_and_Magic_3": return utf8.GetString(InternalMapBytes(GameNames.MightAndMagic3));
                case "Might_and_Magic_45": return utf8.GetString(InternalMapBytes(GameNames.MightAndMagic45));
                case "Wizardry_1": return utf8.GetString(InternalMapBytes(GameNames.Wizardry1));
                case "Wizardry_2": return utf8.GetString(InternalMapBytes(GameNames.Wizardry2));
                case "Wizardry_3": return utf8.GetString(InternalMapBytes(GameNames.Wizardry3));
                case "Wizardry_4": return utf8.GetString(InternalMapBytes(GameNames.Wizardry4));
                case "Wizardry_5": return utf8.GetString(InternalMapBytes(GameNames.Wizardry5));
                case "Bards_Tale_1": return utf8.GetString(InternalMapBytes(GameNames.BardsTale1));
                case "Bards_Tale_2": return utf8.GetString(InternalMapBytes(GameNames.BardsTale2));
                case "Bards_Tale_3": return utf8.GetString(InternalMapBytes(GameNames.BardsTale3));
                default: throw new Exception("Could not locate internal resource \"" + strInternalFile + "\"");
            }
        }

        public static string DefaultTitle(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return "    MM$|    MM ";
                case GameNames.MightAndMagic2: return "   MM2$|   MM2 ";
                case GameNames.MightAndMagic3: return "   MM3$|   MM3 ";
                case GameNames.MightAndMagic45: return "  XEEN$|  XEEN ";
                case GameNames.Wizardry1: return "     WIZ1$|     WIZ1 ";
                case GameNames.Wizardry2: return "     WIZ2$|     WIZ2 ";
                case GameNames.Wizardry3: return "     WIZ3$|     WIZ3 ";
                case GameNames.Wizardry4: return "     WIZ4$|     WIZ4 ";
                case GameNames.Wizardry5: return "     WIZ5$|     WIZ5 ";
                case GameNames.BardsTale1: return "     BARD$|     BARD ";
                case GameNames.BardsTale2: return "       DK$|       DK ";
                case GameNames.BardsTale3: return "    THIEF$|    THIEF ";
                case GameNames.DOSBox: return "^DOSBox [SE0]";
                default: return "";
            }
        }

        public static string CurrentTitle(GameNames game)
        {
            return Properties.Settings.Default.GameTitles.Get(game, DefaultTitle(game));
        }

        public static bool IsWizardry(GameNames game) { return WizardryGames.Contains(game); }

        public static bool IsBardsTale(GameNames game) { return BTGames.Contains(game); }

        public static GameNames[] ImplementedGames
        {
            get
            {
                return new GameNames[]
                {
                    GameNames.BardsTale1,
                    GameNames.BardsTale2,
                    GameNames.BardsTale3,
                    GameNames.MightAndMagic1,
                    GameNames.MightAndMagic2,
                    GameNames.MightAndMagic3,
                    GameNames.MightAndMagic45,
                    GameNames.Wizardry1,
                    GameNames.Wizardry2,
                    GameNames.Wizardry3,
                    GameNames.Wizardry4,
                    GameNames.Wizardry5,
                    GameNames.DOSBox,
                };
            }
        }

        public static bool IsImplemented(GameNames game) { return ImplementedGames.Contains(game); }

        public static IEnumerable<Monster> GetMonsters(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return MM1.Monsters;
                case GameNames.MightAndMagic2: return MM2.Monsters;
                case GameNames.MightAndMagic3: return MM3.Monsters;
                case GameNames.MightAndMagic45: return MM45.MM4Monsters.Concat(MM45.MM5Monsters);
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4:
                case GameNames.Wizardry5: return Games.GetWizGlobals(game).GetMonsters();
                default: return null;
            }
        }

        public static string GetMapTitle(GameNames game, int index) { return GetMapTitleFunction(game)(index).Title; }

        public static string GetItemName(GameNames game, int index)
        {
            string strUnknown = String.Format("Unknown({0})", index);
            if (index < 0)
                return strUnknown;
            switch (game)
            {
                case GameNames.MightAndMagic1: return index < MM1.Items.Count ? MM1.Items[index].Name : strUnknown;
                case GameNames.MightAndMagic2: return index < MM2.Items.Count ? MM2.Items[index].Name : strUnknown;
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4:
                case GameNames.Wizardry5:
                    List<WizItem> items = Games.GetWizGlobals(game).GetItems();
                    if (index < items.Count)
                        return items[index].Name;
                    return strUnknown;
                case GameNames.BardsTale1:
                case GameNames.BardsTale2:
                case GameNames.BardsTale3:
                    List<BTItem> btItems = Games.GetBTGlobals(game).GetItems();
                    if (index < btItems.Count)
                        return btItems[index].Name;
                    return strUnknown;
                default: return strUnknown;
            }
        }

        public static string GetMonsterName(GameNames game, int index)
        {
            string strUnknown = String.Format("Unknown({0})", index);
            if (index < 0)
                return strUnknown;
            switch (game)
            {
                case GameNames.MightAndMagic1: return index < MM1.Monsters.Count ? MM1.Monsters[index].ProperName : strUnknown;
                case GameNames.MightAndMagic2: return index < MM2.Monsters.Count ? MM1.Monsters[index].ProperName : strUnknown;
                case GameNames.MightAndMagic3: return index < MM3.Monsters.Count ? MM1.Monsters[index].ProperName : strUnknown;
                case GameNames.MightAndMagic45:
                    if (index < MM45.MM4Monsters.Count)
                        return MM45.MM4Monsters[index].ProperName;
                    if (index < MM45.MM4Monsters.Count + MM45.MM5Monsters.Count)
                        return MM45.MM5Monsters[index - MM45.MM4Monsters.Count].ProperName;
                    return strUnknown;
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4:
                case GameNames.Wizardry5:
                    List<WizMonster> monsters = Games.GetWizGlobals(game).GetMonsters();
                    if (index < monsters.Count)
                        return monsters[index].Name;
                    return strUnknown;

                case GameNames.BardsTale1:
                case GameNames.BardsTale2:
                case GameNames.BardsTale3:
                    List<BTMonster> btMonsters = Games.GetBTGlobals(game).GetMonsters();
                    if (index < btMonsters.Count)
                        return btMonsters[index].Name;
                    return strUnknown;

                default: return "Unknown Map";
            }
        }

        public static WizGameGlobals GetWizGlobals(GameNames game)
        {
            switch (game)
            {
                case GameNames.Wizardry1: return Global.Wiz1 as WizGameGlobals;
                case GameNames.Wizardry2: return Global.Wiz2 as WizGameGlobals;
                case GameNames.Wizardry3: return Global.Wiz3 as WizGameGlobals;
                case GameNames.Wizardry4: return Global.Wiz4 as WizGameGlobals;
                case GameNames.Wizardry5: return Global.Wiz5 as WizGameGlobals;
                default: return null;
            }
        }

        public static BTGameGlobals GetBTGlobals(GameNames game)
        {
            switch (game)
            {
                case GameNames.BardsTale1: return Global.BT1 as BTGameGlobals;
                case GameNames.BardsTale2: return Global.BT2 as BTGameGlobals;
                case GameNames.BardsTale3: return Global.BT3 as BTGameGlobals;
                default: return null;
            }
        }

        public static GameGlobals GetGlobals(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return Global.MM1 as GameGlobals;
                case GameNames.MightAndMagic2: return Global.MM2 as GameGlobals;
                case GameNames.MightAndMagic3: return Global.MM3 as GameGlobals;
                case GameNames.MightAndMagic45: return Global.MM45 as GameGlobals;
                case GameNames.Wizardry1: return Global.Wiz1 as GameGlobals;
                case GameNames.Wizardry2: return Global.Wiz2 as GameGlobals;
                case GameNames.Wizardry3: return Global.Wiz3 as GameGlobals;
                case GameNames.Wizardry4: return Global.Wiz4 as GameGlobals;
                case GameNames.Wizardry5: return Global.Wiz5 as GameGlobals;
                case GameNames.BardsTale1: return Global.BT1 as GameGlobals;
                case GameNames.BardsTale2: return Global.BT2 as GameGlobals;
                case GameNames.BardsTale3: return Global.BT3 as GameGlobals;
                default: return null;
            }
        }

        public static Spell GetSpell(GameNames game, int index)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return MM1.Spells[index];
                case GameNames.MightAndMagic2: return MM2.Spells[index];
                case GameNames.MightAndMagic3: return MM3.Spells[(MM3SpellIndex)index];
                case GameNames.MightAndMagic45: return MM45.Spells[(MM45SpellIndex)index];
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4: return Wiz123.Spells[index];
                case GameNames.Wizardry5: return Wiz5.Spells[index];
                case GameNames.BardsTale1: return BT1.Spells[index];
                case GameNames.BardsTale2: return BT2.Spells[index];
                case GameNames.BardsTale3: return BT3.Spells[index];
                default: return null;
            }
        }

        public static int GetSpellCount(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return MM1.Spells.Count;
                case GameNames.MightAndMagic2: return MM2.Spells.Count;
                case GameNames.MightAndMagic3: return MM3.Spells.Count;
                case GameNames.MightAndMagic45: return MM45.Spells.Count;
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4: return Wiz123.Spells.Count;
                case GameNames.Wizardry5: return Wiz5.Spells.Count;
                case GameNames.BardsTale1: return BT1.Spells.Count;
                case GameNames.BardsTale2: return BT2.Spells.Count;
                case GameNames.BardsTale3: return BT3.Spells.Count;
                default: return 0;
            }
        }

        public static string GetRegistry(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return "GOGMM1";
                case GameNames.MightAndMagic2: return "GOGMM2";
                case GameNames.MightAndMagic3: return "GOGMM3";
                case GameNames.MightAndMagic45: return "GOGMM45";
                    // The Wizardry and Bard's Tale games don't have a proper installer, so there are no registry keys
                default: return String.Empty;
            }
        }

        public static string GetDefaultLink(GameNames game)
        {
            switch(game)
            {
                case GameNames.MightAndMagic1: return "Launch Might and Magic 1 - Book I.lnk";
                case GameNames.MightAndMagic2: return "Launch Might and Magic 2 - Gates to Another World.lnk";
                case GameNames.MightAndMagic3: return "Launch Might and Magic 3 - Isles of Terra.lnk";
                case GameNames.MightAndMagic45: return "Launch Might and Magic 4-5 - World of Xeen.lnk";
                case GameNames.Wizardry1: return "Wizardry 1.lnk";
                case GameNames.Wizardry2: return "Wizardry 2.lnk";
                case GameNames.Wizardry3: return "Wizardry 3.lnk";
                case GameNames.Wizardry4: return "Wizardry 4.lnk";
                case GameNames.Wizardry5: return "Wizardry 5.lnk";
                case GameNames.BardsTale1: return "Bard's Tale 1.lnk";
                case GameNames.BardsTale2: return "Bard's Tale 2.lnk";
                case GameNames.BardsTale3: return "Bard's Tale 3.lnk";
                default: return String.Empty;
            }
        }

        public static BasicInfoStyle GetInfoType(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1:
                case GameNames.MightAndMagic2: return BasicInfoStyle.PermanentWithTemp;
                case GameNames.MightAndMagic3: return BasicInfoStyle.PermanentWithMod;
                case GameNames.MightAndMagic45: return BasicInfoStyle.PermanentWithModNoAlign;
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4:
                case GameNames.Wizardry5: return BasicInfoStyle.AgeInWeeks;
                case GameNames.BardsTale1:
                case GameNames.BardsTale2: return BasicInfoStyle.NoAgeOrAlign;
                case GameNames.BardsTale3: return BasicInfoStyle.SexNoAgeAlign;
                default: return BasicInfoStyle.Unknown;
            }
        }

        public static int MaxNameLength(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic2:
                case GameNames.MightAndMagic3:
                case GameNames.MightAndMagic45: return 10;
                default: return 15;
            }
        }

        public static byte[] GetNameBytes(GameNames game, string name)
        {
            byte[] bytes;
            byte[] bytesEnc;
            ASCIIEncoding enc = new ASCIIEncoding();
            switch (game)
            {
                case GameNames.MightAndMagic1:
                case GameNames.BardsTale1:
                case GameNames.BardsTale2:
                case GameNames.BardsTale3:
                    bytes = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    bytesEnc = enc.GetBytes(name);
                    Buffer.BlockCopy(bytesEnc, 0, bytes, 0, Math.Min(bytesEnc.Length, 15));
                    return bytes;
                case GameNames.MightAndMagic2:
                    return enc.GetBytes(String.Format("{0,-10}", name));
                case GameNames.MightAndMagic3:
                case GameNames.MightAndMagic45:
                    bytes = new byte[11] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    bytesEnc = enc.GetBytes(name);
                    Buffer.BlockCopy(bytesEnc, 0, bytes, 0, Math.Min(bytesEnc.Length, 10));
                    return bytes;
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4:
                case GameNames.Wizardry5:
                    bytes = new byte[16] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    bytesEnc = enc.GetBytes(name);
                    Buffer.BlockCopy(bytesEnc, 0, bytes, 1, Math.Min(bytesEnc.Length, 15));
                    bytes[0] = (byte)name.Length;
                    return bytes;
                default:
                    return new byte[0];
            }
        }

        public static int GetFilterIndex(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1:
                case GameNames.MightAndMagic2:
                case GameNames.MightAndMagic3:
                case GameNames.MightAndMagic45: return (game - GameNames.MightAndMagic1) + 1;
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4:
                case GameNames.Wizardry5: return (game - GameNames.Wizardry1) + 6;
                case GameNames.BardsTale1:
                case GameNames.BardsTale2:
                case GameNames.BardsTale3: return (game - GameNames.BardsTale1) + 11;
                default: return 1;
            }
        }

        public static GenericClass[] Classes(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return new GenericClass[] { GenericClass.Knight, GenericClass.Paladin, GenericClass.Archer,
                    GenericClass.Cleric, GenericClass.Sorcerer, GenericClass.Robber };
                case GameNames.MightAndMagic2: return new GenericClass[] { GenericClass.Knight, GenericClass.Paladin, GenericClass.Archer,
                    GenericClass.Cleric, GenericClass.Sorcerer, GenericClass.Robber, GenericClass.Ninja, GenericClass.Barbarian };
                case GameNames.MightAndMagic3:
                case GameNames.MightAndMagic45: return new GenericClass[] { GenericClass.Knight, GenericClass.Paladin, GenericClass.Archer,
                    GenericClass.Cleric, GenericClass.Sorcerer, GenericClass.Robber, GenericClass.Ninja, GenericClass.Barbarian, GenericClass.Druid, GenericClass.Ranger };
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4:
                case GameNames.Wizardry5: return new GenericClass[] { GenericClass.Fighter, GenericClass.Mage, GenericClass.Priest, GenericClass.Thief,
                    GenericClass.Bishop, GenericClass.Samurai, GenericClass.Lord, GenericClass.Ninja };
                case GameNames.BardsTale1:
                    return new GenericClass[] { GenericClass.Warrior, GenericClass.Paladin, GenericClass.Rogue, GenericClass.Bard,
                    GenericClass.Hunter, GenericClass.Monk, GenericClass.Conjurer, GenericClass.Magician, GenericClass.Sorcerer, GenericClass.Wizard };
                case GameNames.BardsTale2:
                    return new GenericClass[] { GenericClass.Warrior, GenericClass.Paladin, GenericClass.Rogue, GenericClass.Bard,
                    GenericClass.Hunter, GenericClass.Monk, GenericClass.Conjurer, GenericClass.Magician, GenericClass.Sorcerer,
                    GenericClass.Wizard, GenericClass.Archmage, GenericClass.Monster };
                case GameNames.BardsTale3:
                    return new GenericClass[] { GenericClass.Warrior, GenericClass.Paladin, GenericClass.Rogue, GenericClass.Bard,
                    GenericClass.Hunter, GenericClass.Monk, GenericClass.Conjurer, GenericClass.Magician, GenericClass.Sorcerer,
                    GenericClass.Wizard, GenericClass.Archmage, GenericClass.Chronomancer, GenericClass.Geomancer, GenericClass.Monster };
                default: return new GenericClass[0];
            }
        }

        public static GenericAlignmentValue[] Alignments(GameNames game)
        {
            switch (game)
            {
                case GameNames.BardsTale1:
                case GameNames.BardsTale2:
                case GameNames.BardsTale3: return new GenericAlignmentValue[0];
                default: return new GenericAlignmentValue[] {GenericAlignmentValue.Good, GenericAlignmentValue.Neutral, GenericAlignmentValue.Evil};
            }
        }

        public static int ClassValue(GameNames game, GenericClass gc)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return (int)MM1Character.ClassForGeneric(gc);
                case GameNames.MightAndMagic2: return (int)MM2Character.ClassForGeneric(gc);
                case GameNames.MightAndMagic3:
                case GameNames.MightAndMagic45: return (int)MM345BaseCharacter.ClassForGeneric(gc);
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4:
                case GameNames.Wizardry5: return (int)WizCharacter.ClassForGeneric(gc);
                case GameNames.BardsTale1:
                case GameNames.BardsTale2: return (int)BTCharacter.ClassForGeneric(gc);
                case GameNames.BardsTale3: return (int)BT3Character.BT3ClassForGeneric(gc);
                default: return 0;
            }
        }

        public static bool HasClass(GameNames game, GenericClass gc) { return Classes(game).Contains(gc); }

        public static SpellHotkeyList GetSpellHotkeys(GameNames game)
        {
            SpellHotkeyCollection shkc = Properties.Settings.Default.SpellHotkeys;
            if (shkc == null || !shkc.Hotkeys.ContainsKey(game))
                return null;
            return shkc.Hotkeys[game];
        }

        public static Dictionary<GameNames, Dictionary<int, Spell>> m_spells = new Dictionary<GameNames, Dictionary<int, Spell>>();

        private static void InitSpells(GameNames game)
        {
            if (m_spells.ContainsKey(game))
                return;

            m_spells.Add(game, new Dictionary<int, Spell>());
            Dictionary<int, Spell> dict = m_spells[game];
            List<Spell> spells = GetGlobals(game).GetAllSpells();
            foreach (Spell spell in spells)
                dict.Add(spell.BasicIndex, spell);
        }

        public static Spell GetSpellByBasicIndex(GameNames game, int index)
        {
            InitSpells(game);
            if (m_spells == null || !m_spells.ContainsKey(game) || !m_spells[game].ContainsKey(index))
                return null;
            return m_spells[game][index];
        }

        public static string GetSpellName(GameNames game, int iSpell, int iOriginalSpell = -1)
        {
            Spell spell = GetSpellByBasicIndex(game, iSpell);
            if (spell == null)
            {
                if (iOriginalSpell < -1)
                    return String.Format("Favorite #{0}", iOriginalSpell + 1000);
                return "Unknown";
            }
            return spell.Name.ToLower();
        }

        public static string RosterFile(GameNames game)
        {
            string strFile = Properties.Settings.Default.RosterFiles.Get(game, String.Empty);
            if (game == GameNames.MightAndMagic45 && String.IsNullOrWhiteSpace(strFile))
                return Properties.Settings.Default.RosterFiles.Get(GameNames.MightAndMagic5, String.Empty);
            return strFile;
        }

        public static string RosterPath(GameNames game)
        {
            string strPath = Properties.Settings.Default.RosterPaths.Get(game, String.Empty);
            if (game == GameNames.MightAndMagic45 && String.IsNullOrWhiteSpace(strPath))
                return Properties.Settings.Default.RosterPaths.Get(GameNames.MightAndMagic5, String.Empty);
            return strPath;
        }

        public static string Name(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return "Might and Magic 1:  The Secret of the Inner Sanctum";
                case GameNames.MightAndMagic2: return "Might and Magic 2:  Gates to Another World";
                case GameNames.MightAndMagic3: return "Might and Magic 3:  Isles of Terra";
                case GameNames.MightAndMagic45: return "Might and Magic 4-5:  World of Xeen";
                case GameNames.MightAndMagic6: return "Might and Magic 6: The Mandate of Heaven";
                case GameNames.MightAndMagic7: return "Might and Magic 7: For Blood and Honor";
                case GameNames.MightAndMagic8: return "Might and Magic 8: Day of the Destroyer";
                case GameNames.MightAndMagic9: return "Might and Magic 9: Writ of Fate";
                case GameNames.MightAndMagic10: return "Might and Magic 10: Legacy";
                case GameNames.Wizardry1: return "Wizardry 1: Proving Grounds of the Mad Overlord";
                case GameNames.Wizardry2: return "Wizardry 2: The Knight of Diamonds";
                case GameNames.Wizardry3: return "Wizardry 3: Legacy of Llylgamyn";
                case GameNames.Wizardry4: return "Wizardry 4: The Return of Werdna";
                case GameNames.Wizardry5: return "Wizardry 5: Heart of the Maelstrom";
                case GameNames.Wizardry6: return "Wizardry 6: Bane of the Cosmic Forge";
                case GameNames.Wizardry7: return "Wizardry 7: Crusaders of the Dark Savant";
                case GameNames.Wizardry8: return "Wizardry 8";
                case GameNames.BardsTale1: return "Bards Tale 1: Tales of the Unknown, Volume I";
                case GameNames.BardsTale2: return "Bards Tale 2: The Destiny Knight";
                case GameNames.BardsTale3: return "Bards Tale 3: Thief of Fate";
                case GameNames.DungeonMaster1: return "Dungeon Master";
                case GameNames.DungeonMaster2: return "Dungeon Master: Chaos Strikes Back";
                case GameNames.DungeonMaster3: return "Dungeon Master 2: The Legend of Skullkeep";
                case GameNames.Ultima1: return "Ultima 1: The First Age of Darkness";
                case GameNames.Ultima2: return "Ultima 2: The Revenge of the Enchantress";
                case GameNames.Ultima3: return "Ultima 3: Exodus";
                case GameNames.DOSBox: return "DOSBox (window matcher)";
                default: return "None";
            }
        }

        public static string ShortName(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return "Might and Magic 1";
                case GameNames.MightAndMagic2: return "Might and Magic 2";
                case GameNames.MightAndMagic3: return "Might and Magic 3";
                case GameNames.MightAndMagic45: return "Might and Magic 4-5";
                case GameNames.MightAndMagic6: return "Might and Magic 6";
                case GameNames.MightAndMagic7: return "Might and Magic 7";
                case GameNames.MightAndMagic8: return "Might and Magic 8";
                case GameNames.MightAndMagic9: return "Might and Magic 9";
                case GameNames.MightAndMagic10: return "Might and Magic 10";
                case GameNames.Wizardry1: return "Wizardry 1";
                case GameNames.Wizardry2: return "Wizardry 2";
                case GameNames.Wizardry3: return "Wizardry 3";
                case GameNames.Wizardry4: return "Wizardry 4";
                case GameNames.Wizardry5: return "Wizardry 5";
                case GameNames.Wizardry6: return "Wizardry 6";
                case GameNames.Wizardry7: return "Wizardry 7";
                case GameNames.Wizardry8: return "Wizardry 8";
                case GameNames.BardsTale1: return "Bards Tale 1";
                case GameNames.BardsTale2: return "Bards Tale 2";
                case GameNames.BardsTale3: return "Bards Tale 3";
                case GameNames.DungeonMaster1: return "Dungeon Master";
                case GameNames.DungeonMaster2: return "Dungeon Master CSB";
                case GameNames.DungeonMaster3: return "Dungeon Master 2";
                case GameNames.Ultima1: return "Ultima 1";
                case GameNames.Ultima2: return "Ultima 2";
                case GameNames.Ultima3: return "Ultima 3";
                case GameNames.DOSBox: return "DOSBox";
                default: return "";
            }
        }

        public static string MapForGame(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return ":Might_and_Magic_1";
                case GameNames.MightAndMagic2: return ":Might_and_Magic_2";
                case GameNames.MightAndMagic3: return ":Might_and_Magic_3";
                case GameNames.MightAndMagic45: return ":Might_and_Magic_45";
                case GameNames.MightAndMagic6: return ":Might_and_Magic_6";
                case GameNames.MightAndMagic7: return ":Might_and_Magic_7";
                case GameNames.MightAndMagic8: return ":Might_and_Magic_8";
                case GameNames.MightAndMagic9: return ":Might_and_Magic_9";
                case GameNames.MightAndMagic10: return ":Might_and_Magic_10";
                case GameNames.Wizardry1: return ":Wizardry_1";
                case GameNames.Wizardry2: return ":Wizardry_2";
                case GameNames.Wizardry3: return ":Wizardry_3";
                case GameNames.Wizardry4: return ":Wizardry_4";
                case GameNames.Wizardry5: return ":Wizardry_5";
                case GameNames.Wizardry6: return ":Wizardry_6";
                case GameNames.Wizardry7: return ":Wizardry_7";
                case GameNames.Wizardry8: return ":Wizardry_8";
                case GameNames.BardsTale1: return ":Bards_Tale_1";
                case GameNames.BardsTale2: return ":Bards_Tale_2";
                case GameNames.BardsTale3: return ":Bards_Tale_3";
                case GameNames.DungeonMaster1: return ":Dungeon_Master_1";
                case GameNames.DungeonMaster2: return ":Dungeon_Master_2";
                case GameNames.DungeonMaster3: return ":Dungeon_Master_3";
                case GameNames.Ultima1: return ":Ultima_1";
                case GameNames.Ultima2: return ":Ultima_2";
                case GameNames.Ultima3: return ":Ultima_3";
                default: return "";
            }
        }

        public delegate MapTitleInfo MapTitlePairFunction(int map);

        public static MapTitlePairFunction GetMapTitleFunction(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return MM1MemoryHacker.GetMapTitlePair;
                case GameNames.MightAndMagic2: return MM2MemoryHacker.GetMapTitlePair;
                case GameNames.MightAndMagic3: return MM3MemoryHacker.GetMapTitlePair;
                case GameNames.MightAndMagic45: return MM45MemoryHacker.GetMapTitlePair;
                case GameNames.Wizardry1: return Wiz1MemoryHacker.GetMapTitlePair;
                case GameNames.Wizardry2: return Wiz2MemoryHacker.GetMapTitlePair;
                case GameNames.Wizardry3: return Wiz3MemoryHacker.GetMapTitlePair;
                case GameNames.Wizardry4: return Wiz4MemoryHacker.GetMapTitlePair;
                case GameNames.Wizardry5: return Wiz5MemoryHacker.GetMapTitlePair;
                case GameNames.BardsTale1: return BT1MemoryHacker.GetMapTitlePair;
                case GameNames.BardsTale2: return BT2MemoryHacker.GetMapTitlePair;
                case GameNames.BardsTale3: return BT3MemoryHacker.GetMapTitlePair;
                default: return MemoryHacker.DefaultMapTitlePairFunction;
            }
        }

        public static void SetRosterFile(GameNames game, string strFile)
        {
            Properties.Settings.Default.RosterFiles.Set(game, strFile);
        }

        public static void SetRosterPath(GameNames game, string strDir)
        {
            Properties.Settings.Default.RosterPaths.Set(game, strDir);
        }

        public static void SetRosterFileAndPath(GameNames game, string strFullPath)
        {
            SetRosterPath(game, Path.GetDirectoryName(strFullPath));
            SetRosterFile(game, Path.GetFileName(strFullPath));
            if (game == GameNames.MightAndMagic5)
                SetRosterFileAndPath(GameNames.MightAndMagic45, strFullPath);
        }

        public static GameNames[] WizardryGames 
        { 
            get
            {
                return new GameNames[] { GameNames.Wizardry1, GameNames.Wizardry2, GameNames.Wizardry3, GameNames.Wizardry4, GameNames.Wizardry5 };
            }
        }

        public static GameNames[] BTGames { get { return new GameNames[] { GameNames.BardsTale1, GameNames.BardsTale2, GameNames.BardsTale3 }; } }

        public static GameNames WhichGame(object o)
        {
            if (o is BasicQuestType)
            {
                if (o is MM1Quest) return GameNames.MightAndMagic1;
                if (o is MM2Quest) return GameNames.MightAndMagic2;
                if (o is MM3Quest) return GameNames.MightAndMagic3;
                if (o is MM45Quest) return GameNames.MightAndMagic45;
                if (o is Wiz1Quest) return GameNames.Wizardry1;
                if (o is Wiz2Quest) return GameNames.Wizardry2;
                if (o is Wiz3Quest) return GameNames.Wizardry3;
                if (o is Wiz4Quest) return GameNames.Wizardry4;
                if (o is Wiz5Quest) return GameNames.Wizardry5;
                if (o is BT1Quest) return GameNames.BardsTale1;
                if (o is BT2Quest) return GameNames.BardsTale2;
                if (o is BT3Quest) return GameNames.BardsTale3;
            }
            else if (o is Item)
            {
                if (o is MM1Item) return GameNames.MightAndMagic1;
                if (o is MM2Item) return GameNames.MightAndMagic2;
                if (o is MM3Item) return GameNames.MightAndMagic3;
                if (o is MM45Item) return GameNames.MightAndMagic45;
                if (o is Wiz1Item) return GameNames.Wizardry1;
                if (o is Wiz2Item) return GameNames.Wizardry2;
                if (o is Wiz3Item) return GameNames.Wizardry3;
                if (o is Wiz4Item) return GameNames.Wizardry4;
                if (o is Wiz5Item) return GameNames.Wizardry5;
                if (o is BT1Item) return GameNames.BardsTale1;
                if (o is BT2Item) return GameNames.BardsTale2;
                if (o is BT3Item) return GameNames.BardsTale3;
            }
            else if (o is PartyInfo)
            {
                if (o is MM1PartyInfo) return GameNames.MightAndMagic1;
            }
            else if (o is MM1Map) return GameNames.MightAndMagic1;
            else if (o is MM2Map) return GameNames.MightAndMagic2;
            else if (o is MM3Map) return GameNames.MightAndMagic3;
            else if (o is MM4Map || o is MM5Map) return GameNames.MightAndMagic45;
            else if (o is Wiz1Map) return GameNames.Wizardry1;
            else if (o is Wiz2Map) return GameNames.Wizardry2;
            else if (o is Wiz3Map) return GameNames.Wizardry3;
            else if (o is Wiz4Map) return GameNames.Wizardry4;
            else if (o is Wiz5Map) return GameNames.Wizardry5;
            else if (o is BT1Map) return GameNames.BardsTale1;
            else if (o is BT2Map) return GameNames.BardsTale2;
            else if (o is BT3Map) return GameNames.BardsTale3;
            return GameNames.None;
        }

        public static string[] SpellList(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic3: return MM3SpellList.GetSpellNames();
                case GameNames.MightAndMagic45: return MM45SpellList.GetSpellNames();
                default: return new string[0];
            }
        }

        public static bool IsSightBlocking(GameNames game, MapSheet sheet, MapLineInfo line, List<MapIcon> icons = null, Direction dir = Direction.None)
        {
            if (line == null || sheet == null)
                return true;
            // All lines are assumed to be sight-blocking with a very few exceptions:
            // - Barriers (solid red lines in underground maps in MM1/2 and Wizardry 1-5)
            // - Water (solid or dotted blue lines in MM2-5; the water in MM1 blocks vision)
            if (line.Color.ToArgb() == Color.Red.ToArgb())
            {
                if (IsWizardry(game))
                    return false;   // No overworld in Wizardry; must be a barrier
                if (IsBardsTale(game))
                    return false;   // Towns/wilderness has no red lines; must be a barrier
                switch (game)
                {
                    case GameNames.MightAndMagic1: return MM1MemoryHacker.IsSurfaceMap(sheet.GameMapIndex);
                    case GameNames.MightAndMagic2: return MM2MemoryHacker.IsSurfaceMap(sheet.GameMapIndex);
                    default: return true;    // Mountain -> blocks vision
                }
            }
            else if (line.Color.ToArgb() == Color.Blue.ToArgb())
            {
                switch (game)
                {
                    case GameNames.MightAndMagic2:
                    case GameNames.MightAndMagic3:
                    case GameNames.MightAndMagic4:
                    case GameNames.MightAndMagic5:
                    case GameNames.MightAndMagic45:
                        return false;   // water can be seen past in these games
                    default:
                        return true;
                }
            }
            else if (icons != null)
            {
                switch (game)
                {
                    case GameNames.MightAndMagic3:
                    case GameNames.MightAndMagic4:
                    case GameNames.MightAndMagic45:
                    case GameNames.MightAndMagic5:
                        if (icons.Any(i => i.Name == IconName.GrateHalf && i.Orientation == dir))
                            return false;   // Can see through portcullis-style doors in this game
                        break;
                    default:
                        break;
                }
            }
            return true;    // Any other arbitrary line -> assume sight-blocking
        }

        public static int CharacterSize(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return MM1Character.SizeInBytes;
                case GameNames.MightAndMagic2: return MM2Character.SizeInBytes;
                case GameNames.MightAndMagic3: return MM3Character.SizeInBytes;
                case GameNames.MightAndMagic45: return MM45Character.SizeInBytes;
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4: return WizCharacter.SizeInBytes;
                case GameNames.Wizardry5: return Wiz5Character.SizeInBytes;
                case GameNames.BardsTale1: return BT1Character.SizeInBytes;
                case GameNames.BardsTale2: return BT2Character.SizeInBytes;
                case GameNames.BardsTale3: return BT3Character.SizeInBytes;
                default: return 0;
            }
        }

        public static CharacterOffsets GetCharacterOffsets(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return MM1.Offsets;
                case GameNames.MightAndMagic2: return MM2.Offsets;
                case GameNames.MightAndMagic3: return MM3.Offsets;
                case GameNames.MightAndMagic45: return MM45.Offsets;
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3: return Wiz123.Offsets;
                case GameNames.Wizardry4: return null;
                case GameNames.Wizardry5: return Wiz5.Offsets;
                case GameNames.BardsTale1: return BT1.Offsets;
                case GameNames.BardsTale2: return BT2.Offsets;
                case GameNames.BardsTale3: return BT3.Offsets;
                default: return null;
            }
        }

        public static int CharacterMemorySize(GameNames game)
        {
            switch (game)
            {
                case GameNames.BardsTale1: return BT1Character.SizeInMemory;
                case GameNames.BardsTale2: return BT2Character.SizeInMemory;
                case GameNames.BardsTale3: return BT3Character.SizeInMemory;
                default: return CharacterSize(game);
            }
        }

        public static CharacterOffsets CharacterOffsets(GameNames game)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return MM1.Offsets;
                case GameNames.MightAndMagic2: return MM2.Offsets;
                case GameNames.MightAndMagic3: return MM3.Offsets;
                case GameNames.MightAndMagic45: return MM45.Offsets;
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4: return Wiz123.Offsets;
                case GameNames.Wizardry5: return Wiz5.Offsets;
                case GameNames.BardsTale1: return BT1.Offsets;
                case GameNames.BardsTale2: return BT2.Offsets;
                case GameNames.BardsTale3: return BT3.Offsets;
                default: return null;
            }
        }

        public static CharacterInfoControl CreateCharacterInfoControl(GameNames game, IMain main)
        {
            switch (game)
            {
                case GameNames.MightAndMagic1: return new MM1CharacterInfoControl(main);
                case GameNames.MightAndMagic2: return new MM2CharacterInfoControl(main);
                case GameNames.MightAndMagic3: return new MM3CharacterInfoControl(main);
                case GameNames.MightAndMagic45: return new MM45CharacterInfoControl(main);
                case GameNames.Wizardry1:
                case GameNames.Wizardry2:
                case GameNames.Wizardry3:
                case GameNames.Wizardry4: return new Wiz123CharacterInfoControl(main);
                case GameNames.Wizardry5: return new Wiz5CharacterInfoControl(main);
                case GameNames.BardsTale1:
                case GameNames.BardsTale2:
                case GameNames.BardsTale3: return new BT123CharacterInfoControl(main);
                default: return null;
            }
        }

        public static Dictionary<int, Spell> GetSpellList(GameNames game)
        {
            InitSpells(game);
            return m_spells[game];
        }
    }

    public class SpellSelectItem
    {
        public MMSpell Spell;
        public int Index;
        public GameNames Game;

        public SpellSelectItem(MM3InternalSpellIndex index)
        {
            Game = GameNames.MightAndMagic3;
            Spell = MM3.SpellList.Value.GetSpell(index);
            Index = (int)index;
        }

        public SpellSelectItem(MM45SpellIndex index)
        {
            Game = GameNames.MightAndMagic45;
            Spell = MM45.SpellList.Value.Spells[index];
            Index = (int)index;
        }

        public override string ToString()
        {
            if (Spell == null)
                return "";
            return Spell.Name;
        }
    }

    public class Race
    {
        public GenericRace Generic;
        public override string ToString()
        {
            return RaceString(Generic);
        }

        public static string RaceString(GenericRace race)
        {
            switch (race)
            {
                case GenericRace.Dwarf: return "Dwarf";
                case GenericRace.Elf: return "Elf";
                case GenericRace.Gnome: return "Gnome";
                case GenericRace.HalfOrc: return "Half-Orc";
                case GenericRace.Human: return "Human";
                case GenericRace.Hobbit: return "Hobbit";
                case GenericRace.HalfElf: return "Half-Elf";
                case GenericRace.None: return "None";
                default: return "Unknown";
            }
        }

        public Race(GenericRace race)
        {
            Generic = race;
        }
    }

    public class StatsPerLevel
    {
        public int HPMin;
        public int HPMax;
        public int SPMin;
        public int SPMax;

        public StatsPerLevel(int iHPMin, int iHPMax, int iSPMin, int iSPMax)
        {
            HPMin = iHPMin;
            HPMax = iHPMax;
            SPMin = iSPMin;
            SPMax = iSPMax;
        }
    }
}
