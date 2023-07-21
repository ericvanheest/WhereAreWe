using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    class DebugMonitor : IDisposable
    {
        private MainForm m_main;
        System.Windows.Forms.Timer m_timer;

        private byte[] m_lastBits;
        private string[] m_lastCharacters;
        private int m_invCurrent = 1;
        private Dictionary<int, Dictionary<int, List<LevelStat>>> m_stats = new Dictionary<int, Dictionary<int, List<LevelStat>>>();
        LevelStat m_lastLevelStat = null;
        StreamWriter m_writer = null;
        private int m_iMemoryIndex = 0;

        public DebugMonitor(MainForm main)
        {
            m_main = main;
            m_timer = new System.Windows.Forms.Timer();
            m_timer.Interval = 200;
            m_timer.Tick += m_timer_Tick;
            m_timer.Start();
        }

        public List<int> BitsAdded(byte[] bytesOriginal, byte[] bytesNew)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < bytesNew.Length * 8; i++)
            {
                if (i / 8 >= bytesOriginal.Length)
                    continue;
                if (Global.GetBit(bytesOriginal, i) == 0 && Global.GetBit(bytesNew, i) == 1)
                    list.Add(i);
            }

            return list;
        }

        public List<int> BitsRemoved(byte[] bytesOriginal, byte[] bytesNew)
        {
            List<int> list = new List<int>();

            for (int i = 0; i < bytesNew.Length * 8; i++)
            {
                if (i / 8 >= bytesOriginal.Length)
                    continue;
                if (Global.GetBit(bytesOriginal, i) == 1 && Global.GetBit(bytesNew, i) == 0)
                    list.Add(i);
            }

            return list;
        }

        public string BitsAddedString(byte[] bytesOriginal, byte[] bytesNew, int iMax = -1)
        {
            if (iMax == -1)
                iMax = bytesOriginal.Length * 8;

            StringBuilder sb = new StringBuilder();

            List<int> added = BitsAdded(bytesOriginal, bytesNew);

            foreach (int i in added)
            {
                if (i < iMax)
                    sb.AppendFormat("{0}, ", i);
            }

            return Global.Trim(sb).ToString();
        }

        public int ReverseBitIndex(int i)
        {
            return ((i / 8) * 8) + (7 - (i % 8));
        }

        public string BitsChangedString(byte[] bytesOriginal, byte[] bytesNew, int iMax = -1, int iMap = 0)
        {
            if (iMax == -1)
                iMax = bytesOriginal.Length * 8;

            StringBuilder sb = new StringBuilder();

            List<int> added = BitsAdded(bytesOriginal, bytesNew);
            List<int> removed = BitsRemoved(bytesOriginal, bytesNew);

            foreach (int i in added)
            {
                if (i < iMax)
                    sb.AppendFormat("+{0} ", ReverseBitIndex(iMap == 0 ? i : i % 64));
            }

            foreach (int i in removed)
            {
                if (i < iMax)
                    sb.AppendFormat("-{0} ", ReverseBitIndex(iMap == 0 ? i : i % 64));
            }

            return Global.Trim(sb).ToString();
        }

        public string BitsRemovedString(byte[] bytesOriginal, byte[] bytesNew, int iMax = -1)
        {
            if (iMax == -1)
                iMax = bytesOriginal.Length * 8;

            StringBuilder sb = new StringBuilder();

            List<int> removed = BitsRemoved(bytesOriginal, bytesNew);

            foreach (int i in removed)
            {
                if (i < iMax)
                    sb.AppendFormat("{0}, ", i);
            }

            return Global.Trim(sb).ToString();
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            if (!Global.Debug)
            {
                m_timer.Stop();
                return;
            }

            if (m_main == null || m_main.Hacker == null || !m_main.Hacker.Running)
                return;

            // CopyMemory();
        }

        private void CopyMemory()
        {
            if (WaitControl())
            {
                MemoryBytes mb = m_main.Hacker.ReadOffset(Wiz5.Memory.TreasureList + (m_iMemoryIndex * 54), 54);
                if (mb == null)
                    return;

                m_main.Hacker.WriteOffset(Wiz5.Memory.TreasureList + (15 * 54), mb.Bytes);

                Global.Log("Copied Treasure {0} => 15", m_iMemoryIndex);

                m_iMemoryIndex++;
            }

            if (WaitShift())
            {
                MemoryBytes mb = m_main.Hacker.ReadOffset(Wiz5.Memory.CurrentTreasure, 114);
                if (mb == null)
                    return;

                Global.Log("Treasure[{0}]: {1}", m_iMemoryIndex - 1, Global.ByteString(mb.Bytes));
            }
        }

        private void GatherLevelStats()
        {
            List<BaseCharacter> characters = m_main.Hacker.GetCharacters();
            if (characters == null || characters.Count < 1)
                return;

            Wiz5Character wiz5Char = characters[0] as Wiz5Character;

            if (m_lastLevelStat != null && wiz5Char.Level == m_lastLevelStat.LevelFrom)
                return;

            if (m_lastLevelStat == null)
            {
                m_lastLevelStat = new LevelStat(wiz5Char);
                return;
            }

            m_lastLevelStat.SetTo(wiz5Char);

            if (m_writer == null)
                m_writer = new StreamWriter("F:\\DumpStats.txt");

            m_writer.WriteLine(m_lastLevelStat.ToString());

            if (!m_stats.ContainsKey(m_lastLevelStat.LevelFrom))
                m_stats.Add(m_lastLevelStat.LevelFrom, new Dictionary<int, List<LevelStat>>(1));
            if (!m_stats[m_lastLevelStat.LevelFrom].ContainsKey(wiz5Char.BasicAge.Years))
                m_stats[m_lastLevelStat.LevelFrom].Add(wiz5Char.BasicAge.Years, new List<LevelStat>(1));
            m_stats[m_lastLevelStat.LevelFrom][m_lastLevelStat.Age].Add(m_lastLevelStat);

            m_lastLevelStat = new LevelStat(wiz5Char);

            DebugConsole.SetAllStats(m_main, 0, wiz5Char, 18);
            DebugConsole.SetFullHP(m_main, 0, wiz5Char, 1);
            DebugConsole.SetLevel(m_main, 0, wiz5Char, 20);

            //if (wiz5Char.Level > 20)
            //{
            //    DebugConsole.SetLevel(m_main, 0, wiz5Char, 1);

            //    StringBuilder sbAge = new StringBuilder();
            //    int iCountAge = 0;
            //    for (int i = 0; i < 21; i++)
            //    {
            //        if (m_stats.ContainsKey(i))
            //        {
            //            sbAge.AppendFormat("{0}:{1},", i, m_stats[i].Count);
            //            iCountAge++;
            //        }
            //    }
            //    Global.Trim(sbAge);

            //    Global.Log("Age: {0}, byLevel: {1}, byAge({2}): {3}", wiz5Char.BasicAge.Years, m_stats.Count, iCountAge, sbAge.ToString());

            //    if (wiz5Char.BasicAge.Years > 200)
            //    {
            //        DebugConsole.SetAge(m_main, 0, wiz5Char, 0);
            //        //DumpStats(m_stats);
            //    }
            //    else
            //        DebugConsole.SetAge(m_main, 0, wiz5Char, wiz5Char.BasicAge.Years + 1);
            //}
        }

        private void DumpStats(Dictionary<int, Dictionary<int, List<LevelStat>>> dict)
        {
            // Write all of the stats to a file
            StreamWriter writer = new StreamWriter("F:\\DumpStats.txt", true);
            foreach (Dictionary<int, List<LevelStat>> byLevel in dict.Values)
            {
                foreach (List<LevelStat> byAge in byLevel.Values)
                {
                    foreach (LevelStat stat in byAge)
                        writer.WriteLine(stat.ToString());
                }
            }
            writer.Close();
        }

        private long m_iLastExp = -1;

        private void CheckExp()
        {
            if (!WaitControl())
                return;

            MemoryBytes mb = m_main.Hacker.ReadOffset(Wiz5.Memory.InnNeedExp, 6);
            if (mb == null)
                return;

            long exp = new WizardryLong(mb.Bytes).Number;
            if (exp > 10000000)
                return;

            if (m_iLastExp != exp)
            {
                Global.Log("+{0}, ", exp);
                m_iLastExp = exp;
                DebugConsole.GiveExperience(m_main, 0, m_main.Hacker.GetCharacters()[0], exp);
            }
        }

        private void CheckBits()
        {
            MemoryBytes bits = m_main.Hacker.ReadOffset(Wiz5.Memory.ActivatedBits, 66);

            if (bits == null)
                return;

            if (m_lastBits != null && !Global.Compare(bits.Bytes, m_lastBits))
            {
                string strInfo = m_main.Hacker.GetMapStrings(true);

                int iMap = m_main.Hacker.GetCurrentMapIndex();
                LocationInformation location = m_main.Hacker.GetLocation();

                string strSpot = String.Format("Wiz5.Spots.L{0}_{1:X2}{2:X2}",
                    iMap, location.PrimaryCoordinates.X + 129, location.PrimaryCoordinates.Y + 129);

                // Single bits have changed; log it
                //string strSingleAdd = BitsAddedString(m_lastBits, bits, 512);
                //if (!String.IsNullOrWhiteSpace(strSingleAdd))
                //    Global.Log("+Bits: {0} {1}[{2}]", strSingleAdd, strSpot, strInfo);
                //string strSingleSub = BitsRemovedString(m_lastBits, bits, 512);
                //if (!String.IsNullOrWhiteSpace(strSingleSub))
                //    Global.Log("-Bits: {0} {1}[{2}]", strSingleSub, strSpot, strInfo);

                string strBoth = BitsChangedString(m_lastBits, bits, 512, iMap);
                if (!String.IsNullOrWhiteSpace(strBoth))
                    Global.Log("{0}: {1}", strSpot, strBoth);
            }

            m_lastBits = bits;
        }

        private bool WaitControl()
        {
            if (!NativeMethods.IsControlDown())
                return false;

            while (NativeMethods.IsControlDown())
                Thread.Sleep(50);

            return true;
        }

        private bool WaitShift()
        {
            if (!NativeMethods.IsShiftDown())
                return false;

            while (NativeMethods.IsShiftDown())
                Thread.Sleep(50);

            return true;
        }

        private int m_iNextMonsterBytesIndex = 0;

        private void DumpMonsterBytes()
        {
            if (WaitControl())
            {
                byte bMonster = m_main.Hacker.ReadByte(Wiz5.Memory.Group1 + 6);
                MemoryBytes mbMonster = m_main.Hacker.ReadOffset(Wiz5.Memory.Monster1Bytes, 118);

                Global.Log("{0:D3}: {1}", bMonster, Global.ByteString(mbMonster.Bytes));
            }

            if (WaitShift())
            {
                m_main.Hacker.WriteByte(Wiz5.Memory.L1_15x23_Monster, (byte)m_iNextMonsterBytesIndex);
                m_iNextMonsterBytesIndex++;
            }
        }

        private void CheckEncounters()
        {
            if (WaitControl())
            {
                MemoryBytes mb = m_main.Hacker.ReadOffset(Wiz5.Memory.Group1, 102);
                if (mb == null)
                    return;

                MemoryBytes mbMonster = m_main.Hacker.ReadOffset(Wiz5.Memory.Monster1_1, 100);

                ASCIIEncoding ascii = new ASCIIEncoding();
                Global.Log("{0:D3} ({1}): ", mb.Bytes[6], Wiz5.MonsterName(mb.Bytes[6]), Global.ByteString(mbMonster));
            }
        }

        private int[] checkItems = new int[] {
            0, 001, 002, 012, 027, 047, 051, 054, 071, 075, 076, 077, 078, 089, 090, 091, 092, 093, 094, 095, 096, 097, 098, 099, 100,
            101, 102, 103, 104, 105, 106, 107, 112, 114, 115, 116, 117, 118, 119, 121, 122, 124, 125, 126, 127, 128, 129, 130, 131, 136
        };

        private void InventoryEquipBytes()
        {
            if (WaitControl())
            {
                // Dump current bytes
                Wiz5PartyInfo party = m_main.Hacker.GetPartyInfo() as Wiz5PartyInfo;

                MemoryBytes mb = m_main.Hacker.ReadOffset(Wiz5.Memory.EquipItemBytes, 85);
                if (mb == null)
                    return;

                Global.Log("{0:D3}:{1}", party.Bytes[Wiz5.Offsets.Inventory + 2 + 4], Global.ByteString(mb.Bytes));

                Global.SetInt16(party.Bytes, Wiz5.Offsets.Inventory + 2 + 4, checkItems[m_invCurrent]);
                m_main.Hacker.SetCharacterBytes(0, party.Bytes);
                m_invCurrent += 1;
                if (m_invCurrent >= checkItems.Length)
                    m_invCurrent = 0;
            }
        }

        private void InventoryNamer()
        {
            if (NativeMethods.IsControlDown() && NativeMethods.IsShiftDown())
            {
                while (NativeMethods.IsControlDown() || NativeMethods.IsShiftDown())
                    Thread.Sleep(50);

                // Dump current items
                Wiz5PartyInfo party = m_main.Hacker.GetPartyInfo() as Wiz5PartyInfo;
                MemoryBytes mb = m_main.Hacker.ReadOffset(Wiz5.Memory.InventoryDisplay, 8 * 32);
                if (mb == null)
                    return;

                ASCIIEncoding ascii = new ASCIIEncoding();
                for (int i = 0; i < 8; i++)
                {
                    Global.Log("Item[{0}].Unidentified: {1}\r\nItem[{0}].Identified: {2}",
                        BitConverter.ToInt16(party.Bytes, Wiz5.Offsets.Inventory + 2 + (i * 4)),
                        ascii.GetString(mb.Bytes, i * 32 + 1, mb.Bytes[i * 32]),
                        ascii.GetString(mb.Bytes, i * 32 + 17, mb.Bytes[i * 32 + 16]));
                }

                Global.SetInt16(party.Bytes, Wiz5.Offsets.Inventory, 8);
                for (int i = 0; i < 8; i++)
                    Global.SetInt16(party.Bytes, Wiz5.Offsets.Inventory + 2 + (i * 4), m_invCurrent + i);
                m_main.Hacker.SetCharacterBytes(0, party.Bytes);

                m_invCurrent += 8;
            }
        }

        private void CheckDoGooders()
        {
            byte[] dogooders = m_main.Hacker.ReadOffset(Wiz4.Memory.DoGooders, 64);
            List<BaseCharacter> characters = m_main.Hacker.GetCharacters();

            if (dogooders == null)
                return;

            if (m_lastBits != null && !Global.Compare(dogooders, m_lastBits))
            {
                // Sinlges bits have changed; log it
                string strSingleAdd = BitsAddedString(m_lastBits, dogooders);
                if (!String.IsNullOrWhiteSpace(strSingleAdd))
                    Global.Log("+Bits: {0}", strSingleAdd);
                string strSingleSub = BitsRemovedString(m_lastBits, dogooders);
                if (!String.IsNullOrWhiteSpace(strSingleSub))
                    Global.Log("-Bits: {0}", strSingleSub);
            }

            m_lastBits = dogooders;

            if (characters != null)
            {
                string[] names = new string[characters.Count];

                for (int i = 0; i < characters.Count; i++)
                {
                    names[i] = characters[i].Name;
                }


                if (m_lastCharacters != null)
                {
                    string strParty = null;
                    if (characters.Count == 7)
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 1; i < 7; i++)
                            sb.AppendFormat("{0}", ((WizCharacter)characters[i]).Password);
                        strParty = sb.ToString();
                    }

                    bool bAnyNew = false;
                    foreach (string strTest in names)
                    {
                        if (m_lastCharacters.Contains(strTest))
                            continue;
                        if (!String.IsNullOrWhiteSpace(strTest) && strTest != "WERDNA")
                        {
                            bAnyNew = true;
                            Global.Log("+Char: {0}", strTest);
                        }
                    }
                    if (bAnyNew && !String.IsNullOrWhiteSpace(strParty))
                        Global.Log("+Herald: {0}", strParty);
                    foreach (string strTest in m_lastCharacters)
                    {
                        if (names.Contains(strTest))
                            continue;
                        if (!String.IsNullOrWhiteSpace(strTest) && strTest != "WERDNA")
                            Global.Log("-Char: {0}", strTest);
                    }
                }

                m_lastCharacters = names;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (m_timer != null)
                    m_timer.Dispose();
                if (m_writer != null)
                    m_writer.Dispose();
            }
        }
    }

    class LevelStat
    {
        public int LevelFrom;
        public int LevelTo;
        public int HPFrom;
        public int HPTo;
        public int[] StatsFrom;
        public int[] StatsTo;
        public GenericClass Class;
        public int Age;

        public LevelStat(BaseCharacter charFrom)
        {
            LevelFrom = charFrom.BasicLevel.Permanent;
            HPFrom = charFrom.QuickRefHitPoints.Maximum;
            Class = charFrom.BasicClass;
            Age = charFrom.BasicAge.Years;
            StatsFrom = new int[charFrom.PrimaryStats.Length];
            for (int i = 0; i < StatsFrom.Length; i++)
                StatsFrom[i] = charFrom.PrimaryStats[i].Permanent;
        }

        public void SetTo(BaseCharacter charTo)
        {
            LevelTo = charTo.BasicLevel.Permanent;
            HPTo = charTo.QuickRefHitPoints.Maximum;
            Class = charTo.BasicClass;
            Age = charTo.BasicAge.Years;
            StatsTo = new int[charTo.PrimaryStats.Length];
            for (int i = 0; i < StatsFrom.Length; i++)
                StatsTo[i] = charTo.PrimaryStats[i].Permanent;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("{0},{1},{2},{3},{4},{5},", BaseCharacter.ClassString(Class), Age, LevelFrom, LevelTo, HPFrom, HPTo);
            sb.AppendFormat("{0},{1},", Age, LevelTo);
            //for (int i = 0; i < StatsFrom.Length; i++)
            //    sb.AppendFormat("{0},{1},", StatsFrom == null ? 0: StatsFrom[i], StatsTo == null ? 0 : StatsTo[i]);
            //sb.AppendFormat("{0},{1},", StatsTo == null ? 0 : StatsTo.Count(s => s > 12), StatsTo == null ? 0 : StatsTo.Count(s => s < 12));
            sb.AppendFormat("{0},{1},", StatsTo == null ? 0 : StatsTo.Count(s => s > 18), StatsTo == null ? 0 : StatsTo.Count(s => s < 18));
            Global.Trim(sb);
            return sb.ToString();
        }
    }
}
