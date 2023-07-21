using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class QuickRefForm : HackerBasedForm
    {
        private byte[] m_lastBytes;
        private GameTime m_lastTime;
        private int m_iLastSortColumn = -1;
        private int m_iMonitorTotal = 0;
        private bool m_bAscendingSort = true;
        private ColumnHeaderList m_colWidths;
        private List<int> m_lastMonitorHP = null;
        private List<StatAndModifier[]> m_lastMonitorStats = null;
        private bool m_bNeedNewColumnData = false;
        private int m_iAntiFluxCounterHP = 0;
        private int m_iAntiFluxCounterStats = 0;
        private Dictionary<int, int> m_dictHPDeltas = new Dictionary<int, int>();
        private Dictionary<int, int> m_dictStatUp = new Dictionary<int, int>();
        private Dictionary<int, int> m_dictStatDown = new Dictionary<int, int>();
        private Dictionary<int, int> m_dictStatEqual = new Dictionary<int, int>();
        private bool m_bSkipNextStats = false;
        private Font m_defaultFont = null;
        private Color m_defaultForeColor;
        private Color m_defaultBackColor;

        public QuickRefForm()
        {
            InitializeComponent();

            m_colWidths = Properties.Settings.Default.QuickRefColumns;
            if (m_colWidths == null)
            {
                m_colWidths = new ColumnHeaderList(lvChars, m_iLastSortColumn, m_bAscendingSort);
                Properties.Settings.Default.QuickRefColumns = m_colWidths;
            }
        }

        private string TotalAC(BaseCharacter mmChar)
        {
            return mmChar.GetACString(Hacker.GetBlessValue());
        }

        private void AddCharacter(BaseCharacter baseChar, int iIndex, bool bHideNormalAge = true)
        {
            ListViewItem lvi = new ListViewItem(String.Format("{0}", iIndex));
            lvi.SubItems.Add(baseChar.Name);
            lvi.SubItems.Add(BaseCharacter.ClassString(baseChar.BasicClass));
            lvi.SubItems.Add(String.Format("{0}", baseChar.BasicLevelString));
            lvi.SubItems.Add(String.Format("{0}", baseChar.QuickRefHitPoints.Current));
            lvi.SubItems.Add(String.Format("{0}", baseChar.QuickRefHitPoints.TemporaryMaximum));
            lvi.SubItems.Add(String.Format("{0}", baseChar.QuickRefSpellPoints == null ? "" : baseChar.QuickRefSpellPoints.Current));
            if (baseChar is MMBaseCharacter)
            {
                MMBaseCharacter mmChar = baseChar as MMBaseCharacter;
                lvi.SubItems.Add(String.Format("{0}", mmChar.QuickRefSpellPoints.Maximum));
                lvi.SubItems.Add(String.Format("{0}", TotalAC(mmChar)));
                lvi.SubItems.Add(mmChar.MeleeDamageString);
                lvi.SubItems.Add(mmChar.RangedDamageString);
                lvi.SubItems.Add(String.Format("{0}", mmChar.BasicMight.Temporary + mmChar.Modifiers.Might));
                lvi.SubItems.Add(String.Format("{0}", mmChar.BasicIntellect.Temporary + mmChar.Modifiers.Intellect));
                lvi.SubItems.Add(String.Format("{0}", mmChar.BasicPersonality.Temporary + mmChar.Modifiers.Personality));
                lvi.SubItems.Add(String.Format("{0}", mmChar.BasicEndurance.Temporary + mmChar.Modifiers.Endurance));
                lvi.SubItems.Add(String.Format("{0}", mmChar.BasicSpeed.Temporary + mmChar.Modifiers.Speed));
                lvi.SubItems.Add(String.Format("{0}", mmChar.BasicAccuracy.Temporary + mmChar.Modifiers.Accuracy));
                lvi.SubItems.Add(String.Format("{0}", mmChar.BasicLuck.Temporary + mmChar.Modifiers.Luck));
                string strAge = bHideNormalAge ? "" : baseChar.BasicAge.Years.ToString();
                lvi.SubItems.Add(strAge + (baseChar.BasicAge.Modifier == 0 ? "" : String.Format("{0}", Global.AddPlus(baseChar.BasicAge.Modifier))));
            }
            else if (baseChar is WizardryBaseCharacter)
            {
                WizardryBaseCharacter wizChar = baseChar as WizardryBaseCharacter;
                lvi.SubItems.Add(String.Empty); // SP Max
                lvi.SubItems.Add(String.Format("{0}", TotalAC(wizChar)));
                lvi.SubItems.Add(wizChar.MeleeDamageString);
                lvi.SubItems.Add(String.Empty); // Ranged damage
                lvi.SubItems.Add(String.Format("{0}", wizChar.BasicStrength.Temporary + wizChar.Modifiers.Strength));
                lvi.SubItems.Add(String.Format("{0}", wizChar.BasicIQ.Temporary + wizChar.Modifiers.IQ));
                lvi.SubItems.Add(String.Format("{0}", wizChar.BasicPiety.Temporary + wizChar.Modifiers.Piety));
                lvi.SubItems.Add(String.Format("{0}", wizChar.BasicVitality.Temporary + wizChar.Modifiers.Vitality));
                lvi.SubItems.Add(String.Format("{0}", wizChar.BasicAgility.Temporary + wizChar.Modifiers.Agility));
                lvi.SubItems.Add(String.Format("{0}", wizChar.BasicLuck.Temporary + wizChar.Modifiers.Luck));
                lvi.SubItems.Add(String.Empty); // Stat 7
                lvi.SubItems.Add(wizChar.BasicAge.Years.ToString());
            }
            else if (baseChar is BTBaseCharacter)
            {
                BTBaseCharacter btChar = baseChar as BTBaseCharacter;
                lvi.SubItems.Add(String.Format("{0}", btChar.QuickRefSpellPoints.Maximum));
                lvi.SubItems.Add(String.Format("{0}", TotalAC(btChar)));
                lvi.SubItems.Add(btChar.MeleeDamageString);
                lvi.SubItems.Add(String.Empty); // Ranged damage
                lvi.SubItems.Add(String.Format("{0}", btChar.BasicStrength.Temporary + btChar.Modifiers.Strength));
                lvi.SubItems.Add(String.Format("{0}", btChar.BasicIQ.Temporary + btChar.Modifiers.IQ));
                lvi.SubItems.Add(String.Format("{0}", btChar.BasicDexterity.Temporary + btChar.Modifiers.Dexterity));
                lvi.SubItems.Add(String.Format("{0}", btChar.BasicConstitution.Temporary + btChar.Modifiers.Constitution));
                lvi.SubItems.Add(String.Format("{0}", btChar.BasicLuck.Temporary + btChar.Modifiers.Luck));
                lvi.SubItems.Add(String.Empty); // Stat 6
                lvi.SubItems.Add(String.Empty); // Stat 7
                lvi.SubItems.Add(String.Empty); // Age
            }
            else if (baseChar is EOBBaseCharacter)
            {
                EOBBaseCharacter eobChar = baseChar as EOBBaseCharacter;
                lvi.SubItems.Add(String.Format("{0}", eobChar.QuickRefSpellPoints.ToString()));
                lvi.SubItems.Add(String.Format("{0}", TotalAC(eobChar)));
                lvi.SubItems.Add(eobChar.MeleeDamageString);
                lvi.SubItems.Add(String.Empty); // Ranged damage
                lvi.SubItems.Add(String.Format("{0}", eobChar.BasicStrength.Temporary + eobChar.Modifiers.Strength));
                lvi.SubItems.Add(String.Format("{0}", eobChar.BasicIntelligence.Temporary + eobChar.Modifiers.Intelligence));
                lvi.SubItems.Add(String.Format("{0}", eobChar.BasicWisdom.Temporary + eobChar.Modifiers.Wisdom));
                lvi.SubItems.Add(String.Format("{0}", eobChar.BasicDexterity.Temporary + eobChar.Modifiers.Dexterity));
                lvi.SubItems.Add(String.Format("{0}", eobChar.BasicConstitution.Temporary + eobChar.Modifiers.Constitution));
                lvi.SubItems.Add(String.Format("{0}", eobChar.BasicCharisma.Temporary + eobChar.Modifiers.Charisma));
                lvi.SubItems.Add(String.Empty); // Stat 7
                lvi.SubItems.Add(String.Empty); // Age
            }
            lvi.SubItems.Add(baseChar.BasicInventory.Items.Count.ToString());
            lvi.SubItems.Add(baseChar.QuickRefCondition);
            lvi.Tag = baseChar;
            lvChars.Items.Add(lvi);
        }

        private bool CompareLists(List<BaseCharacter> list, byte[] m_lastBytes)
        {
            if (list == null || m_lastBytes == null || list.Count == 0)
                return false;

            if (list.Count * list[0].CharacterSize != m_lastBytes.Length)
                return false;

            for (int i = 0; i < list.Count; i++)
            {
                if (!Global.CompareBytes(list[i].RawBytes, m_lastBytes, 0, i * list[i].CharacterSize, list[i].RawBytes.Length))
                    return false;
            }

            return true;
        }

        private byte[] ToBytes(List<BaseCharacter> chars)
        {
            byte[] bytes = new byte[chars.Sum(c => c.RawBytes.Length)];
            int iOffset = 0;
            foreach (BaseCharacter c in chars)
            {
                Buffer.BlockCopy(c.RawBytes, 0, bytes, iOffset, c.RawBytes.Length);
                iOffset += c.RawBytes.Length;
            }
            return bytes;
        }

        protected override bool OnCommonKeyRefresh()
        {
            m_lastBytes = null;

            return DumpMonitorStats();
        }

        private bool DumpMonitorStats()
        {
            if (Monitoring && Global.Debug)
            {
                List<BaseCharacter> chars = Hacker.GetCharacters();
                if (chars == null)
                    return false;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine();
                StatAndModifier[] stats = chars[0].PrimaryStats;
                sb.AppendFormat("{0} ({1}/{2}/{3}/{4}/{5}/{6})", chars[0].BasicInfoString,
                    stats[0].Temporary, stats[1].Temporary, stats[2].Temporary, stats[3].Temporary, stats[4].Temporary, stats[5].Temporary);
                sb.AppendLine();
                sb.Append("Hit Points:\r\n");
                int iMax = m_dictHPDeltas.Keys.Count > 0 ? m_dictHPDeltas.Keys.Max() : 0;
                for (int i = 0; i <= iMax; i++)
                {
                    if (m_dictHPDeltas.ContainsKey(i))
                        sb.AppendFormat("+{0}: {1}\r\n", i, m_dictHPDeltas[i]);
                }
                sb.AppendLine();
                //sb.Append("Stats:\r\n");
                int iStat = 0;
                int iTotalDown = 0;
                int iTotalUp = 0;
                int iTotalEqual = 0;
                int iGrandTotal = 0;
                foreach(string stat in new string[] { "Str", "IQ ", "Pie", "Vit", "Agi", "Lck" })
                {
                    int iDown = m_dictStatDown.ContainsKey(iStat) ? m_dictStatDown[iStat] : 0;
                    int iEqual = m_dictStatEqual.ContainsKey(iStat) ? m_dictStatEqual[iStat] : 0;
                    int iUp = m_dictStatUp.ContainsKey(iStat) ? m_dictStatUp[iStat] : 0;
                    int iTotal = iDown + iEqual + iUp;
                    if (iTotal == 0)
                        iTotal = 1;
                    iTotalDown += iDown;
                    iTotalUp += iUp;
                    iTotalEqual += iEqual;
                    //sb.AppendFormat("-{0}:{1,3} ({2,4:F1}%); ", stat, iDown, iDown * 100.0 / iTotal);
                    //sb.AppendFormat("={0}:{1,3} ({2,4:F1}%); ", stat, iEqual, iEqual * 100.0 / iTotal);
                    //sb.AppendFormat("+{0}:{1,3} ({2,4:F1}%)", stat, iUp, iUp * 100.0 / iTotal);
                    //sb.AppendLine();
                    iStat++;
                }
                iGrandTotal = iTotalDown + iTotalUp + iTotalEqual;
                if (iGrandTotal == 0)
                    iGrandTotal = 1;

                sb.AppendFormat("Age {9}: Up:{0} ({1:F2}%)  Down:{2} ({3:F2}%)  Equal:{4} ({5:F2}%)  Up-to-Down:{6}:{7} ({8:F2}%)",
                    iTotalUp, iTotalUp * 100.0 / iGrandTotal,
                    iTotalDown, iTotalDown * 100.0 / iGrandTotal,
                    iTotalEqual, iTotalEqual * 100.0 / iGrandTotal,
                    iTotalUp, iTotalDown,
                    iTotalUp * 100.0 / (iTotalUp + iTotalDown),
                    chars[0].BasicAge.Years);
                sb.AppendLine();

                Global.Log(sb.ToString());
            }
            return true;
        }

        public void ForceUpdate()
        {
            m_lastBytes = null;
        }

        private void UpdateUI(List<BaseCharacter> chars)
        {
            if (chars == null)
            {
                lvChars.Items.Clear();
                Global.UpdateText(this, "Character Quick Reference (no party detected)");
                return;
            }

            Global.UpdateText(this, "Character Quick Reference");

            GameTime time = m_main.Hacker.GetGameTime();
            if (chars.Count == lvChars.Items.Count && CompareLists(chars, m_lastBytes) && m_iAntiFluxCounterHP == 0 && m_iAntiFluxCounterStats == 0)
            {
                if (!time.Equals(m_lastTime))
                {
                    Global.SetDefaultStyle(lvChars, m_defaultFont, m_defaultForeColor, m_defaultBackColor);
                    CheckTriggers(chars, Properties.Settings.Default.Triggers);
                    m_lastTime = time;
                }
                return;
            }

            m_lastTime = time;

            if (Global.Debug)
                CheckMonitors(chars);

            m_lastBytes = ToBytes(chars);

            int iLastSelected = lvChars.SelectedIndices.Count > 0 ? lvChars.SelectedIndices[0] : -1;

            lvChars.BeginUpdate();
            lvChars.Items.Clear();

            int iIndex = 1;
            foreach (BaseCharacter mmChar in chars)
                AddCharacter(mmChar, iIndex++, !(mmChar is MM1Character));

            Global.RestoreColumnOrder(lvChars, m_colWidths, cmColumns);
            if (m_colWidths.SortColumn != -1)
            {
                m_iLastSortColumn = m_colWidths.SortColumn;
                m_bAscendingSort = m_colWidths.SortAscending;
                lvChars.ListViewItemSorter = new QuickRefItemComparer(lvChars, m_iLastSortColumn, m_bAscendingSort);
                lvChars.Sort();
            }

            Global.SizeHeadersAndContent(lvChars);

            if (chars.Count > 0 && chars[0] is WizardryBaseCharacter)
            {
                chStat1.Text = "Str";
                chStat2.Text = "IQ";
                chStat3.Text = "Pie";
                chStat4.Text = "Vit";
                chStat5.Text = "Agi";
                chStat6.Text = "Lck";
                chStat7.Width = 0;
                chRanged.Width = 0;
                chSPMax.Width = 0;
                miColumnsStat1.Text = "Strength";
                miColumnsStat2.Text = "I.Q.";
                miColumnsStat3.Text = "Piety";
                miColumnsStat4.Text = "Vitality";
                miColumnsStat5.Text = "Agility";
                miColumnsStat6.Text = "Luck";
                miColumnsAge.Text = "Age";
                miColumnsStat7.Visible = false;
                miColumnsRanged.Visible = false;
                miColumnsMaxSP.Visible = false;
            }
            else if (chars.Count > 0 && chars[0] is BTBaseCharacter)
            {
                chStat1.Text = "Str";
                chStat2.Text = "IQ";
                chStat3.Text = "Dex";
                chStat4.Text = "Con";
                chStat5.Text = "Lck";
                chStat6.Width = 0;
                chStat7.Width = 0;
                chRanged.Width = 0;
                chSPMax.Width = 0;
                chAge.Width = 0;
                miColumnsStat1.Text = "Strength";
                miColumnsStat2.Text = "I.Q.";
                miColumnsStat3.Text = "Dexterity";
                miColumnsStat4.Text = "Constitution";
                miColumnsStat5.Text = "Luck";
                miColumnsStat7.Visible = false;
                miColumnsRanged.Visible = false;
                miColumnsMaxSP.Visible = false;
            }
            else if (chars.Count > 0 && chars[0] is EOBBaseCharacter)
            {
                chStat1.Text = "Str";
                chStat2.Text = "Int";
                chStat3.Text = "Wis";
                chStat4.Text = "Dex";
                chStat5.Text = "Con";
                chStat6.Text = "Cha";
                chStat7.Width = 0;
                chRanged.Width = 0;
                chSPMax.Width = 0;
                chAge.Width = 0;
                miColumnsStat1.Text = "Strength";
                miColumnsStat2.Text = "Intelligence";
                miColumnsStat3.Text = "Wisdom";
                miColumnsStat4.Text = "Dexterity";
                miColumnsStat5.Text = "Constitution";
                miColumnsStat6.Text = "Charisma";
                miColumnsStat7.Visible = false;
                miColumnsRanged.Visible = false;
                miColumnsMaxSP.Visible = false;
            }

            if (lvChars.Items.Count > 0)
            {
                m_defaultFont = lvChars.Items[0].Font;
                m_defaultForeColor = lvChars.Items[0].ForeColor;
                m_defaultBackColor = lvChars.Items[0].BackColor;
            }

            CheckTriggers(chars, Properties.Settings.Default.Triggers);

            if (iLastSelected >= 0 && iLastSelected < lvChars.Items.Count)
                lvChars.Items[iLastSelected].Selected = true;

            lvChars.EndUpdate();
        }

        private void CheckTriggers(List<BaseCharacter> chars, TriggerList triggers)
        {
            if (triggers == null || triggers.Items == null || triggers.Items.Count < 1 || !triggers.Enabled)
                return;

            // The listview might not be in the same order the chars list is, so we need a map from one to the other
            Dictionary<int, int> dictChars = new Dictionary<int, int>(chars.Count);
            foreach (ListViewItem lvi in lvChars.Items)
            {
                int address = ((BaseCharacter)lvi.Tag).BasicAddress;
                if (!dictChars.ContainsKey(address))
                    dictChars.Add(((BaseCharacter)lvi.Tag).BasicAddress, lvi.Index);
            }

            foreach (CharacterTrigger trigger in triggers.Items)
                CheckTrigger(chars, trigger, dictChars);
        }

        private void CheckTrigger(List<BaseCharacter> chars, CharacterTrigger trigger, Dictionary<int, int> addresses)
        {
            if (!trigger.Enabled)
                return;

            trigger.Tested = false;
            for (int i = 0; i < chars.Count; i++)
            {
                if (!CharacterInfoControl.CheckTriggerWho(trigger, chars[i], i))
                    continue;

                TriggerTarget[] testedEntities = CharacterInfoControl.CheckTriggerConditions(m_main.Hacker, chars, chars[i], trigger);
                if (testedEntities == null || testedEntities.Length < 1)
                    continue;

                TriggerTarget[] targets = trigger.To == TriggerEntity.TestedItem ? testedEntities : new TriggerTarget[] { new TriggerTarget(trigger.To, trigger.ToValue, testedEntities) };

                foreach (TriggerTarget target in targets)
                {
                    if (!addresses.ContainsKey(i))
                        continue;
                    TriggerSubItem tsi = new TriggerSubItem(lvChars.Items[addresses[i]], GetColumn(target.Entity, target.Value));

                    if (tsi == null || tsi.SubItem == null)
                        continue;

                    switch (trigger.Do)
                    {
                        case TriggerDo.SetBoldFont:
                            tsi.SetBold();
                            break;
                        case TriggerDo.SetItalicFont:
                            tsi.SetItalic();
                            break;
                        case TriggerDo.SetColorTo:
                            tsi.SetColors(trigger.DoColorFore, trigger.DoColorBack);
                            break;
                        case TriggerDo.SetGradient:
                            Color colorBetweenFore = Global.GetGradientValue(trigger.DoColorFore, trigger.DoColorFore2, target.TestedValue, target.BetweenMin, target.BetweenMax);
                            Color colorBetweenBack = Global.GetGradientValue(trigger.DoColorBack, trigger.DoColorBack2, target.TestedValue, target.BetweenMin, target.BetweenMax);
                            tsi.SetColors(colorBetweenFore, colorBetweenBack);
                            break;
                    }
                }
            }
        }

        private int GetColumn(TriggerEntity entity, string strVal)
        {
            int iTest = 0;
            switch (entity)
            {
                case TriggerEntity.CharIndex: return 0;
                case TriggerEntity.Name: return 1;
                case TriggerEntity.Class: return 2;
                case TriggerEntity.CurrentLevel: return 3;
                case TriggerEntity.HitPoints: return 4;
                case TriggerEntity.MaxHitPoints: return 5;
                case TriggerEntity.SpellPoints: return 6;
                case TriggerEntity.MaxSpellPoints: return 7;
                case TriggerEntity.ArmorClass: return 8;
                case TriggerEntity.MeleeDamageAverage: return 9;
                case TriggerEntity.RangedDamageAverage: return 10;
                case TriggerEntity.StatIndex:
                    if (!Int32.TryParse(strVal, out iTest))
                        return -1;
                    return 11 + iTest;
                case TriggerEntity.CurrentAge: return 18;
                case TriggerEntity.BackpackItemCount: return 19;
                case TriggerEntity.Condition: return 20;
                default: return -1;
            }
        }

        private void CheckMonitors(List<BaseCharacter> chars)
        {
            bool bWizTest = false;
            bool bForceStats = false;

            if (miMonitorHP.Checked)
            {
                if (m_lastMonitorHP == null)
                    m_lastMonitorHP = new List<int>(chars.Count);

                m_iAntiFluxCounterHP++;

                for (int i = 0; i < chars.Count; i++)
                {
                    int iHP = chars[i].QuickRefHitPoints.Current;
                    if (m_lastMonitorHP.Count > i && m_lastMonitorHP[i] != iHP && m_iAntiFluxCounterHP > 2)
                    {
                        if (m_lastMonitorHP != null && m_lastMonitorHP.Count > i)
                        {
                            int iDiff = iHP - m_lastMonitorHP[i];
                            if (iDiff < 0 && bWizTest)
                                m_bSkipNextStats = true;
                            else
                            {
                                Global.Log("{0}: {1} ({2})", chars[i].Name, iHP, Global.AddPlus(iDiff));
                                if (!m_dictHPDeltas.ContainsKey(iDiff))
                                    m_dictHPDeltas.Add(iDiff, 0);
                                m_dictHPDeltas[iDiff]++;
                                bForceStats = true;
                            }
                        }
                        else
                            Global.Log("{0}: {1}", chars[i].Name, iHP);

                        m_iAntiFluxCounterHP = 0;
                    }

                    if (m_iAntiFluxCounterHP == 0 || m_lastMonitorHP.Count <= i)
                    {
                        if (m_lastMonitorHP.Count > i)
                            m_lastMonitorHP[i] = iHP;
                        else
                            m_lastMonitorHP.Add(iHP);
                    }
                }
            }
            if (miMonitorStats.Checked)
            {
                if (m_lastMonitorStats == null)
                    m_lastMonitorStats = new List<StatAndModifier[]>(chars.Count);

                m_iAntiFluxCounterStats++;

                for (int i = 0; i < chars.Count; i++)
                {
                    StatAndModifier[] stats = chars[i].PrimaryStats;
                    if (m_lastMonitorStats.Count > i && (bForceStats || !Global.Compare(m_lastMonitorStats[i], stats)) && m_iAntiFluxCounterStats > 2)
                    {
                        if (m_bSkipNextStats)
                            m_bSkipNextStats = false;
                        else
                        {
                            if (m_lastMonitorStats != null && m_lastMonitorStats.Count > i)
                            {
                                StatString(stats, m_lastMonitorStats[i]);
                                System.Diagnostics.Debugger.Log(0, "", String.Format("{0},", m_iMonitorTotal));
                                if (m_iMonitorTotal % 25 == 24)
                                    System.Diagnostics.Debugger.Log(0, "", "\r\n");
                                m_iMonitorTotal++;
                                //if (m_iMonitorTotal >= 100)
                                //{
                                //    System.Diagnostics.Debugger.Log(0, "", "\r\n");
                                //    DumpMonitorStats();
                                //    RestartTimer();
                                //}
                                //Global.Log("{0}: {1} ({2})", chars[i].Name, StatString(stats), StatString(stats, m_lastMonitorStats[i]));
                                if (bWizTest)
                                {
                                    Wiz123MemoryHacker wizHacker = Hacker as Wiz123MemoryHacker;
                                    wizHacker.WriteUInt16(Wiz1.Memory.PartyInfo + (chars[i].BasicAddress * WizCharacter.SizeInBytes) + Wiz123.Offsets.CurrentHP, 1);
                                    wizHacker.WriteUInt16(Wiz1.Memory.PartyInfo + (chars[i].BasicAddress * WizCharacter.SizeInBytes) + Wiz123.Offsets.Level, 1);
                                    wizHacker.WriteUInt16(Wiz1.Memory.PartyInfo + (chars[i].BasicAddress * WizCharacter.SizeInBytes) + Wiz123.Offsets.Condition, 0);
                                    wizHacker.WriteUInt16(Wiz1.Memory.PartyInfo + (chars[i].BasicAddress * WizCharacter.SizeInBytes) + Wiz123.Offsets.MaxHP, 1);
                                    PackedFiveBitValues pb5 = new PackedFiveBitValues(17, 17, 17, 18, 17, 17);
                                    wizHacker.WriteOffset(Wiz1.Memory.PartyInfo + (chars[i].BasicAddress * WizCharacter.SizeInBytes) + Wiz123.Offsets.Stats, pb5.Bytes);
                                }
                            }
                            else
                                Global.Log("{0}: {1}", chars[i].Name, StatString(stats));
                        }
                        m_iAntiFluxCounterStats = 0;
                    }

                    if (m_iAntiFluxCounterStats == 0 || m_lastMonitorStats.Count <= i)
                    {
                        if (m_lastMonitorStats.Count > i)
                            m_lastMonitorStats[i] = stats;
                        else
                            m_lastMonitorStats.Add(stats);
                    }
                }
            }
        }

        private string StatString(StatAndModifier[] stats)
        {
            if (stats == null || stats.Length < 1)
                return String.Empty;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < stats.Length; i++)
                sb.AppendFormat("{0,2}/", stats[i].Temporary);
            if (sb.Length > 0 && sb[sb.Length - 1] == '/')
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private string StatString(StatAndModifier[] stats1, StatAndModifier[] stats2)
        {
            if (stats1 == null || stats1.Length < 1 || stats2 == null || stats2.Length != stats1.Length)
                return String.Empty;

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < stats1.Length; i++)
            {
                int iDiff = stats1[i].Temporary - stats2[i].Temporary;
                sb.AppendFormat("{0}/", Global.AddPlus(iDiff));
                switch (Math.Sign(iDiff))
                {
                    case -1:
                        if (!m_dictStatDown.ContainsKey(i))
                            m_dictStatDown.Add(i, 0);
                        m_dictStatDown[i]++;
                        break;
                    case 0:
                        if (!m_dictStatEqual.ContainsKey(i))
                            m_dictStatEqual.Add(i, 0);
                        m_dictStatEqual[i]++;
                        break;
                    case 1:
                        if (!m_dictStatUp.ContainsKey(i))
                            m_dictStatUp.Add(i, 0);
                        m_dictStatUp[i]++;
                        break;
                }
            }
            if (sb.Length > 0 && sb[sb.Length - 1] == '/')
                sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lvChars_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == m_iLastSortColumn)
                m_bAscendingSort = !m_bAscendingSort;
            else
                m_bAscendingSort = true;

            m_iLastSortColumn = e.Column;

            m_colWidths.SortColumn = m_iLastSortColumn;
            m_colWidths.SortAscending = m_bAscendingSort;

            lvChars.ListViewItemSorter = new QuickRefItemComparer(lvChars, e.Column, m_bAscendingSort);
            lvChars.Sort();
        }

        protected override void OnMainSet()
        {
            Global.RestartTimer(timerUpdate);
        }

        private void QuickRefForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.QuickRefColumns = new ColumnHeaderList(lvChars, m_iLastSortColumn, m_bAscendingSort);
            timerUpdate.Stop();
        }

        public override void Destroy()
        {
            timerUpdate.Stop();
            base.Destroy();
        }

        private void timerUpdate_Tick(object sender, EventArgs e)
        {
            if (m_bNeedNewColumnData)
            {
                m_colWidths = new ColumnHeaderList(lvChars, m_iLastSortColumn, m_bAscendingSort);
                Properties.Settings.Default.QuickRefColumns = m_colWidths;
                m_bNeedNewColumnData = false;
            }

            if (m_main.PartyForm != null && !m_main.PartyForm.IsDisposed && m_main.PartyForm.Visible && !Monitoring)
                UpdateUI(m_main.PartyForm.GetCharacters());
            else if (Hacker != null)
                UpdateUI(Hacker.GetCharacters());
        }


        private void miColumnsIndex_Click(object sender, EventArgs e)
        {
            miColumnsIndex.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chNumber.Index);
        }

        private void miColumnsName_Click(object sender, EventArgs e)
        {
            miColumnsName.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chName.Index);
        }

        private void miColumnsClass_Click(object sender, EventArgs e)
        {
            miColumnsClass.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chClass.Index);
        }

        private void miColumnsLevel_Click(object sender, EventArgs e)
        {
            miColumnsLevel.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chLevel.Index);
        }

        private void miColumnsHP_Click(object sender, EventArgs e)
        {
            miColumnsHP.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chHP.Index);
        }

        private void miColumnsMaxHP_Click(object sender, EventArgs e)
        {
            miColumnsMaxHP.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chHPMax.Index);
        }

        private void miColumnsSP_Click(object sender, EventArgs e)
        {
            miColumnsSP.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chSP.Index);
        }

        private void miColumnsMaxSP_Click(object sender, EventArgs e)
        {
            miColumnsMaxSP.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chSPMax.Index);
        }

        private void miColumnsAC_Click(object sender, EventArgs e)
        {
            miColumnsAC.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chAC.Index);
        }

        private void miColumnsMelee_Click(object sender, EventArgs e)
        {
            miColumnsMelee.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chMelee.Index);
        }

        private void miColumnsRanged_Click(object sender, EventArgs e)
        {
            miColumnsRanged.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chRanged.Index);
        }

        private void miColumnsMight_Click(object sender, EventArgs e)
        {
            miColumnsStat1.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chStat1.Index);
        }

        private void miColumnsIntellect_Click(object sender, EventArgs e)
        {
            miColumnsStat2.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chStat2.Index);
        }

        private void miColumnsPersonality_Click(object sender, EventArgs e)
        {
            miColumnsStat3.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chStat3.Index);
        }

        private void miColumnsEndurance_Click(object sender, EventArgs e)
        {
            miColumnsStat4.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chStat4.Index);
        }

        private void miColumnsSpeed_Click(object sender, EventArgs e)
        {
            miColumnsStat5.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chStat5.Index);
        }

        private void miColumnsAccuracy_Click(object sender, EventArgs e)
        {
            miColumnsStat6.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chStat6.Index);
        }

        private void miColumnsLuck_Click(object sender, EventArgs e)
        {
            miColumnsStat7.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chStat7.Index);
        }

        private void miColumnsCondition_Click(object sender, EventArgs e)
        {
            miColumnsCondition.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chCondition.Index);
        }

        private void miColumnsAge_Click(object sender, EventArgs e)
        {
            miColumnsAge.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chAge.Index);
        }

        private void miColumnsReset_Click(object sender, EventArgs e)
        {
            Global.ShowAllColumns(lvChars, m_colWidths, cmColumns);
        }

        private void cmColumns_Opening(object sender, CancelEventArgs e)
        {
        }

        private void miMonitorHP_Click(object sender, EventArgs e)
        {
            miMonitorHP.Checked = !miMonitorHP.Checked;
            RestartTimer();
        }

        private bool Monitoring { get { return Global.Debug && (miMonitorHP.Checked || miMonitorStats.Checked); } }

        private void RestartTimer()
        {
            if (Monitoring)
            {
                m_dictHPDeltas = new Dictionary<int, int>();
                m_dictStatUp = new Dictionary<int, int>();
                m_dictStatDown = new Dictionary<int, int>();
                m_dictStatEqual = new Dictionary<int, int>();
                m_iMonitorTotal = 0;
                timerUpdate.Interval = 25;
            }
            else
            {
                timerUpdate.Interval = 250;
            }
            timerUpdate.Stop();
            timerUpdate.Start();
        }

        private void miCharCureAll_Click(object sender, EventArgs e)
        {
            if (lvChars.SelectedItems.Count < 1)
                return;
            BaseCharacter baseChar = (BaseCharacter)lvChars.SelectedItems[0].Tag;
            m_main.CureAll(baseChar.BasicAddress, false);
        }

        private void cmCharacter_Opening(object sender, CancelEventArgs e)
        {
            if (Global.HeaderRect(lvChars).Contains(lvChars.PointToClient(Cursor.Position)))
            {
                e.Cancel = true;
                cmColumns.Show(Cursor.Position);
                return;
            }

            cmCharacter.ShowCheckMargin = Global.Debug;
            miMonitorHP.Visible = Global.Debug;
            miMonitorStats.Visible = Global.Debug;
            miCharCureAll.Visible = lvChars.SelectedItems.Count > 0 &&
                ((BaseCharacter)lvChars.SelectedItems[0].Tag).IsHealer &&
                Properties.Settings.Default.EnableMemoryWrite;
        }

        private void lvChars_ColumnReordered(object sender, ColumnReorderedEventArgs e)
        {
            m_bNeedNewColumnData = true;
        }

        private void miMonitorStats_Click(object sender, EventArgs e)
        {
            miMonitorStats.Checked = !miMonitorStats.Checked;
            RestartTimer();
        }

        private void miColumnsItems_Click(object sender, EventArgs e)
        {
            miColumnsItems.Checked = Global.ToggleColumnVisible(lvChars, m_colWidths, chItems.Index);
        }
    }

    class QuickRefItemComparer : BasicListViewComparer
    {
        public QuickRefItemComparer(ListView lv, int column, bool bAscending) : base(lv, column, bAscending) { }

        public override int Compare(object x, object y)
        {
            if (x == null || y == null)
                return 0;

            BaseCharacter char1 = ((ListViewItem)x).Tag as BaseCharacter;
            BaseCharacter char2 = ((ListViewItem)y).Tag as BaseCharacter;

            switch (m_column)
            {
                case 1: return Order(String.Compare(char1.Name, char2.Name));
                case 2: return Order(String.Compare(BaseCharacter.ClassString(char1.BasicClass), BaseCharacter.ClassString(char2.BasicClass)));
                case 3: return Order(Math.Sign(char1.BasicLevel.Temporary - char2.BasicLevel.Temporary));
                case 4: return Order(Math.Sign(char1.QuickRefHitPoints.Current - char2.QuickRefHitPoints.Current));
                case 5: return Order(Math.Sign(char1.QuickRefHitPoints.TemporaryMaximum - char2.QuickRefHitPoints.TemporaryMaximum));
                case 6: return Order(Math.Sign(SpellPoints.CompareCurrent(char1.QuickRefSpellPoints, char2.QuickRefSpellPoints)));
                case 7: return Order(Math.Sign(SpellPoints.CompareMaximum(char1.QuickRefSpellPoints, char2.QuickRefSpellPoints)));
                case 8: return Order(Math.Sign(char1.BasicAC.Temporary - char2.BasicAC.Temporary));
                case 9: return Order(Math.Sign(char1.BasicMeleeDamage.Max - char2.BasicMeleeDamage.Max));
                case 10: return Order(Math.Sign(char1.BasicRangedDamage.Max - char2.BasicRangedDamage.Max));
                case 11: return Order(Math.Sign(char1.Stat(0).Temporary - char2.Stat(0).Temporary));
                case 12: return Order(Math.Sign(char1.Stat(1).Temporary - char2.Stat(1).Temporary));
                case 13: return Order(Math.Sign(char1.Stat(2).Temporary - char2.Stat(2).Temporary));
                case 14: return Order(Math.Sign(char1.Stat(3).Temporary - char2.Stat(3).Temporary));
                case 15: return Order(Math.Sign(char1.Stat(4).Temporary - char2.Stat(4).Temporary));
                case 16: return Order(Math.Sign(char1.Stat(5).Temporary - char2.Stat(5).Temporary));
                case 17: return Order(Math.Sign(char1.Stat(6).Temporary - char2.Stat(6).Temporary));
                case 18: return Order(Math.Sign(char1.BasicAge.Modifier - char2.BasicAge.Modifier));
                case 19: return Order(String.Compare(char1.QuickRefCondition, char2.QuickRefCondition));
                default: return base.Compare(x, y);
            }
        }
    }
}
