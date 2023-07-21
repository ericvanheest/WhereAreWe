using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    static class Tests
    {
        public static void TestWiz1Treasures()
        {
            StringBuilder sb = new StringBuilder();
            for(int index = 0; index < Wiz1.Treasures.Count; index++)
            {
                WizTreasure treasure = Wiz1.Treasures[index];
                sb.AppendFormat("#{0}, {1}\r\n", index, treasure.Chest ? "Chest" : "No Chest");
                sb.Append(treasure.MultilineDescription);
                sb.AppendLine();
            }
            File.WriteAllText(@"F:\Wiz1Treasures1.txt", sb.ToString());
            File.WriteAllText(@"F:\Wiz1Treasures2.txt", Wiz1.TreasureList.Value.GetFullDescriptions());
            Environment.Exit(0);
        }

        public static string TestImageCache()
        {
            StringBuilder sb = new StringBuilder();
            Global.BitmapCache.Clear();
            sb.AppendFormat("Clear:  {0}, {1}\r\n", Global.BitmapCache.FullSize, Global.BitmapCache.CropSize);
            for (int i = 1; i < 8; i++)
            {
                Global.BitmapCache.GetFullImage(String.Format(@"F:\\Test{0}.jpg", i));
                sb.AppendFormat("Add {0}:  {1}, {2}\r\n", i, Global.BitmapCache.FullSize, Global.BitmapCache.CropSize);
            }

            for (int i = 1; i < 8; i++)
            {
                Global.BitmapCache.GetFullImage(String.Format(@"F:\\Test{0}.jpg", i));
                sb.AppendFormat("Re-get {0}:  {1}, {2}\r\n", i, Global.BitmapCache.FullSize, Global.BitmapCache.CropSize);
            }

            string strResult = sb.ToString();
            return strResult;
        }

        public static string TestMapBytes()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("        public static uint[] StaticMaps = new uint[] {");
            sb.AppendLine("            0, 0, 0, 0,                          // Map 0: Invalid");

            for (MM4Map map = MM4Map.A1Surface; map < MM4Map.LastMain; map++)
            {
                uint[] offsets = MM4FileOffsets.MapOffsets(map);
                string strLine = String.Format("            {0}, {1}, {2}, {3},", 
                    offsets.Length > 0 ? String.Format("0x{0:X5}", offsets[0]) : "0",
                    offsets.Length > 1 ? String.Format("0x{0:X5}", offsets[1]) : "0",
                    offsets.Length > 2 ? String.Format("0x{0:X5}", offsets[2]) : "0",
                    offsets.Length > 3 ? String.Format("0x{0:X5}", offsets[3]) : "0");
                sb.AppendFormat("{0,-50}// Map {1}", strLine, MM45MemoryHacker.GetMapTitlePair((int)map, 0).ToString());
                sb.AppendLine();
            }

            sb.AppendLine("        };");
            sb.AppendLine();

            sb.AppendLine("        public static uint[] StaticMaps = new uint[] {");
            sb.AppendLine("            0, 0, 0, 0,                          // Map 0: Invalid");

            for (MM5Map map = MM5Map.A1Surface; map < MM5Map.LastMain; map++)
            {
                uint[] offsets = MM5FileOffsets.MapOffsets(map);
                string strLine = String.Format("            {0}, {1}, {2}, {3},",
                    offsets.Length > 0 ? String.Format("0x{0:X5}", offsets[0]) : "0",
                    offsets.Length > 1 ? String.Format("0x{0:X5}", offsets[1]) : "0",
                    offsets.Length > 2 ? String.Format("0x{0:X5}", offsets[2]) : "0",
                    offsets.Length > 3 ? String.Format("0x{0:X5}", offsets[3]) : "0");
                sb.AppendFormat("{0,-50}// Map {1}", strLine, MM45MemoryHacker.GetMapTitlePair((int)map, 1).ToString());
                sb.AppendLine();
            }

            sb.AppendLine("        };");
            sb.AppendLine();

            string strCode = sb.ToString();
            return strCode;
        }

        public static void TestMM45Monsters()
        {
            byte[] bytes1 = MM45.MM4Monsters[2].RawBytes;
            MM45Monster copy = MM45.MM4Monsters[2].Clone() as MM45Monster;
        }

        public static void TestShortcutKey(Keys keyLast, Keys[] keys)
        {
            Properties.Settings.Default.Shortcuts.InputOptionForKeys(keyLast, keys);
        }

        public static void TestMM45Guilds()
        {
            StringBuilder sb = new StringBuilder();
            MM45Character c = new MM45Character();
            c.Class = MM345Class.Cleric;
            foreach (MM45Guild guild in Enum.GetValues(typeof(MM45Guild)))
            {
                sb.AppendFormat("{0}\r\n", Enum.GetName(typeof(MM45Guild), guild));
                sb.AppendFormat("Cleric Spells: ", Enum.GetName(typeof(MM45Guild), guild));
                foreach (MM45InternalSpellIndex index in Enum.GetValues(typeof(MM45InternalSpellIndex)))
                {
                    if (c.CanCast(index))
                        if (MM45SpellList.GetSaleLocations(index).Contains(guild))
                            sb.AppendFormat("{0} ({1}), ", MM45SpellList.GetSpellName(index), MM45SpellList.GetSpellPurchasePrice(index));
                }
                sb.AppendLine();
                sb.AppendFormat("Arcane Spells: ", Enum.GetName(typeof(MM45Guild), guild));
                foreach (MM45InternalSpellIndex index in Enum.GetValues(typeof(MM45InternalSpellIndex)))
                {
                    if (!c.CanCast(index))
                        if (MM45SpellList.GetSaleLocations(index).Contains(guild))
                            sb.AppendFormat("{0} ({1}), ", MM45SpellList.GetSpellName(index), MM45SpellList.GetSpellPurchasePrice(index));
                }
                sb.AppendLine();
            }
        }

        public static void TestShops()
        {
            MM3MemoryHacker hacker = new MM3MemoryHacker();
            hacker.Init();

            MM3Shops shops = hacker.GetShopItems();

            if (shops == null)
                return;

            StringBuilder sb = new StringBuilder();
            int iShop = 1;
            sb.AppendFormat("Shops: {0}\n", shops.Inventories.Count);
            foreach (MM3ShopInventory inv in shops.Inventories)
            {
                sb.AppendFormat("Shop {0}\n", iShop++);
                sb.AppendFormat(" Weapons: {0}\n", inv.Weapons.Count);
                foreach (ShopItem item in inv.Weapons)
                    sb.AppendFormat("  {0} - {1} [{2}]\n", item.Item.DescriptionString, item.Item.GetLongDescription(), item.Offset - MM3Memory.ShopInventory);
                sb.AppendFormat(" Armor: {0}\n", inv.Armor.Count);
                foreach (ShopItem item in inv.Armor)
                    sb.AppendFormat("  {0} - {1}\n", item.Item.DescriptionString, item.Item.GetLongDescription());
                sb.AppendFormat(" Misc: {0}\n", inv.Misc.Count);
                foreach (ShopItem item in inv.Misc)
                    sb.AppendFormat("  {0} - {1}\n", item.Item.DescriptionString, item.Item.GetLongDescription());
            }
        }

        public static void TestStrings()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("public const int MonsterNames = {0};            // 90 bytes\n", 0x054F1109 - 0x054F0149);
            sb.AppendFormat("public const int MonsterPhysicalResist = {0};   // 90 bytes\n", 0x055559F0 - 0x054F0149);
            sb.AppendFormat("public const int MonsterEnergyResist = {0};     // 90 bytes\n", 0x05555A70 - 0x054F0149);
            sb.AppendFormat("public const int MonsterAcidResist = {0};       // 90 bytes\n", 0x05555AF0 - 0x054F0149);
            sb.AppendFormat("public const int MonsterColdResist = {0};       // 90 bytes\n", 0x05555B70 - 0x054F0149);
            sb.AppendFormat("public const int MonsterElecResist = {0};       // 90 bytes\n", 0x05555BF0 - 0x054F0149);
            sb.AppendFormat("public const int MonsterFireResist = {0};       // 90 bytes\n", 0x05555C70 - 0x054F0149);
            sb.AppendFormat("public const int MonsterMagicResist = {0};      // 90 words\n", 0x05555CF0 - 0x054F0149);
            sb.AppendFormat("public const int MonsterGems = {0};             // 90 dwords\n", 0x05555D70 - 0x054F0149);
            sb.AppendFormat("public const int MonsterGold = {0};             // 90 bytes\n", 0x05555E50 - 0x054F0149);
            sb.AppendFormat("public const int MonsterItems = {0};            // 90 bytes\n", 0x05555FE0 - 0x054F0149);
            sb.AppendFormat("public const int MonsterAccuracy = {0};         // 90 bytes\n", 0x05556060 - 0x054F0149);
            sb.AppendFormat("public const int MonsterRanged = {0};           // 90 bytes\n", 0x055560E0 - 0x054F0149);
            sb.AppendFormat("public const int MonsterSpecialPower = {0};     // 90 bytes\n", 0x05556160 - 0x054F0149);
            sb.AppendFormat("public const int MonsterTarget = {0};           // 90 bytes\n", 0x055561E0 - 0x054F0149);
            sb.AppendFormat("public const int MonsterDamageType = {0};       // 90 dwords\n", 0x05556260 - 0x054F0149);
            sb.AppendFormat("public const int MonsterExperience = {0};       // 90 bytes\n", 0x055562E0 - 0x054F0149);
            sb.AppendFormat("public const int MonsterNumAttacks = {0};       // 90 bytes\n", 0x05556470 - 0x054F0149);
            sb.AppendFormat("public const int MonsterDamage1 = {0};          // 90 bytes\n", 0x055564F0 - 0x054F0149);
            sb.AppendFormat("public const int MonsterDamage2 = {0};          // 90 bytes\n", 0x05556570 - 0x054F0149);
            sb.AppendFormat("public const int MonsterSpeed = {0};            // 90 bytes\n", 0x055565F0 - 0x054F0149);
            sb.AppendFormat("public const int MonsterAC = {0};               // 90 words\n", 0x05556670 - 0x054F0149);
            sb.AppendFormat("public const int MonsterHPMax = {0};            // 90 words\n", 0x055566F0 - 0x054F0149);
            sb.AppendFormat("public const int MonOffsetPhysicalResist = {0}; // 90 bytes\n", 0x055559F0 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetEnergyResist = {0};   // 90 bytes\n", 0x05555A70 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetAcidResist = {0};     // 90 bytes\n", 0x05555AF0 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetColdResist = {0};     // 90 bytes\n", 0x05555B70 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetElecResist = {0};     // 90 bytes\n", 0x05555BF0 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetFireResist = {0};     // 90 bytes\n", 0x05555C70 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetMagicResist = {0};    // 90 words\n", 0x05555CF0 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetGems = {0};           // 90 dwords\n", 0x05555D70 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetGold = {0};           // 90 bytes\n", 0x05555E50 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetItems = {0};          // 90 bytes\n", 0x05555FE0 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetAccuracy = {0};       // 90 bytes\n", 0x05556060 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetRanged = {0};         // 90 bytes\n", 0x055560E0 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetSpecialPower = {0};   // 90 bytes\n", 0x05556160 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetTarget = {0};         // 90 bytes\n", 0x055561E0 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetDamageType = {0};     // 90 dwords\n", 0x05556260 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetExperience = {0};     // 90 bytes\n", 0x055562E0 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetNumAttacks = {0};     // 90 bytes\n", 0x05556470 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetDamage1 = {0};        // 90 bytes\n", 0x055564F0 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetDamage2 = {0};        // 90 bytes\n", 0x05556570 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetSpeed = {0};          // 90 bytes\n", 0x055565F0 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetAC = {0};             // 90 words\n", 0x05556670 - 0x054F0149 - 415911);
            sb.AppendFormat("public const int MonOffsetHPMax = {0};          // 90 words\n", 0x055566F0 - 0x054F0149 - 415911);
            string s = sb.ToString();
        }

        public static void TestMemoryHacker()
        {
            MemoryHacker hacker = Games.CreateHacker(GameNames.MightAndMagic1);
            hacker.Init();
        }

        public static void TestItems()
        {
            MM2ItemList items = new MM2ItemList();
            StringBuilder sb = new StringBuilder();
            foreach (MM2Item item in items.Items)
                sb.AppendLine(item.Dump());
            File.WriteAllText(@"MM2Items-Test.txt", sb.ToString());
            Environment.Exit(0);
        }

        public static MMSpell FindSpell(SpellType type, int level, int number)
        {
            foreach(MMSpell spell in MM2.Spells)
            {
                if (spell.Type == type && spell.Level == level && spell.Number == number)
                    return spell;
            }
            return null;
        }

        public static void TestSpells()
        {
            string[] spells = { "SpellS13", "SpellS17", "SpellS21", "SpellS23", "SpellS26", "SpellS27", "SpellS31", "SpellS34", "SpellS36", "SpellS41", "SpellS42", "SpellS43", "SpellS51", "SpellS53", "SpellS61", "SpellS63", "SpellS65", "SpellS71", "SpellS72", "SpellS82", "SpellS83", "SpellS91", "SpellS92", "SpellS93", "SpellS94", "SpellC11", "SpellC12", "SpellC16", "SpellC22", "SpellC25", "SpellC27", "SpellC31", "SpellC35", "SpellC36", "SpellC42", "SpellC44", "SpellC46", "SpellC51", "SpellC53", "SpellC55", "SpellC61", "SpellC64", "SpellC65", "SpellC71", "SpellC72", "SpellC81", "SpellC82", "SpellC83", "SpellC92", "SpellC93", "SpellC94" };
            StringBuilder sb = new StringBuilder();
            foreach (string spell in spells)
            {
                SpellType type = spell.Substring(5, 1) == "S" ? SpellType.Sorcerer : SpellType.Cleric;
                int level = Int32.Parse(spell.Substring(6,1));
                int number = Int32.Parse(spell.Substring(7,1));
                MMSpell found = FindSpell(type, level, number);
                sb.AppendFormat(@"            if ({0}.ValidClass)
            {{
                quest = new MM2Quest(BasicQuestType.Side, ""Learn Spell {1}, {2}"", String.Empty, ""{1}"");
                quest.State = SpellS11;
                if (SpellS11.Accepted)
                    quest.Primary = new QuestLocation(""Purchase spell at "", MM2Map.{3});
                quests.Add(quest);
            }}

", spell, spell.Substring(5,2) + "-" + spell.Substring(7), found.Name, found.Learned);

            }
            File.WriteAllText(@"F:\MM2-SpellTemp.txt", sb.ToString());
            Environment.Exit(0);
        }

        public static void TestLevels()
        {
            long[] sorcReq = new long[] {
0, 2000, 4000, 8000, 16000, 32000, 64000, 128000, 256000, 512000, 704000, 896000, 1088000, 1472000, 1856000, 2624000, 3392000, 4160000, 4928000, 5696000, 7232000, 8768000, 10304000, 11840000, 13376000, 14912000, 16448000, 17984000, 19520000, 21056000, 24128000, 27200000, 30272000, 33344000, 36416000, 39488000, 42560000, 45632000, 48704000, 51776000, 54848000, 57920000, 60992000, 64064000, 67136000, 70208000, 73280000, 76352000, 79424000, 82496000, 84134400, 85772800, 87411200, 89049600, 90688000, 92326400, 93964800, 95603200, 97241600, 98880000, 100518400, 102156800, 103795200, 105433600, 107072000, 108710400, 110348800, 111987200, 113625600, 115264000, 116902400, 118540800, 120179200, 121817600, 123456000, 129600000, 135744000, 141888000, 148032000, 154176000, 160320000, 166464000, 172608000, 178752000, 184896000, 191040000, 197184000, 203328000, 209472000, 215616000, 221760000, 227904000, 234048000, 240192000, 246336000, 252480000, 258624000, 264768000, 270912000, 277056000, 283200000, 289344000, 295488000, 301632000, 307776000, 313920000, 320064000, 326208000, 332352000, 338496000, 344640000, 350784000, 356928000
            };

            long[] clericReq = new long[] {
0, 1500, 3000, 6000, 12000, 24000, 48000, 96000, 192000, 384000, 576000, 768000, 960000, 1344000, 1728000, 2496000, 3264000, 4032000, 4800000, 5568000, 7104000, 8640000, 10176000, 11712000, 13248000, 14784000, 16320000, 17856000, 19392000, 20928000, 24000000, 27072000, 30144000, 33216000, 36288000, 39360000, 42432000, 45504000, 48576000, 51648000, 54720000, 57792000, 60864000, 63936000, 67008000, 70080000, 73152000, 76224000, 79296000, 82368000, 84006400, 85644800, 87283200, 88921600, 90560000, 92198400, 93836800, 95475200, 97113600, 98752000, 100390400, 102028800, 103667200, 105305600, 106944000, 108582400, 110220800, 111859200, 113497600, 115136000, 116774400, 118412800, 120051200, 121689600, 123328000, 129472000, 135616000, 141760000, 147904000, 154048000, 160192000, 166336000, 172480000, 178624000, 184768000, 190912000, 197056000, 203200000, 209344000, 215488000, 221632000, 227776000, 233920000, 240064000, 246208000, 252352000, 258496000, 264640000, 270784000, 276928000, 283072000, 289216000, 295360000, 301504000, 307648000, 313792000, 319936000, 326080000, 332224000, 338368000, 344512000, 350656000, 356800000, 362944000, 369088000, 375232000, 381376000, 387520000, 393664000, 399808000, 405952000, 412096000, 418240000, 424384000, 430528000, 436672000, 442816000, 448960000, 455104000, 461248000, 467392000, 473536000, 479680000, 485824000, 491968000, 498112000, 504256000, 510400000, 516544000, 522688000, 528832000, 534976000, 541120000, 547264000, 553408000, 559552000, 565696000, 571840000
            };

            string strFile = "E:\\Temp\\levels.txt";
            File.Delete(strFile);
            //for (int i = 1; i < sorcReq.Length; i++)
            //    File.AppendAllText(strFile, String.Format("Sorc {0,-3}: {1} ({2})\r\n", i, sorcReq[i] - sorcReq[i - 1], sorcReq[i-1]));
            //for (int i = 1; i < clericReq.Length; i++)
            //    File.AppendAllText(strFile, String.Format("Cler {0,-3}: {1} ({2})\r\n", i, clericReq[i] - clericReq[i - 1], clericReq[i-1]));
            for (int i = 1; i < 256; i++)
                File.AppendAllText(strFile, String.Format("Cler {0,-3}: {1}\r\n", i, MM2Character.XPForLevel(MM2Class.Cleric, i)));
            for (int i = 1; i < 256; i++)
                File.AppendAllText(strFile, String.Format("Sorc {0,-3}: {1}\r\n", i, MM2Character.XPForLevel(MM2Class.Sorcerer, i)));
            
            Environment.Exit(0);
        }

        public static void TestMonsters()
        {
            StringBuilder sb = new StringBuilder();
            foreach (MM2Monster monster in MM2.Monsters)
            {
                sb.AppendFormat("{0}: {1}, Raw Exp: {2}\r\n", monster.Index, monster.ProperName, MM2Monster.ExpToByte(monster.Experience));
            }
            File.WriteAllText(@"F:\MM2-RawExp.txt", sb.ToString());
            Environment.Exit(0);
        }
    }
}
