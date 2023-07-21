using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WhereAreWe
{
    public partial class UltimaMonsterEditForm : Form
    {
        public UltimaMonsterEditForm()
        {
            InitializeComponent();
        }

        private UltimaMonster m_monster;

        public UltimaMonster Monster
        {
            get
            {
                m_monster = MonsterFromUI();
                return m_monster;    
            }

            set
            {
                m_monster = value;
                UpdateUI(m_monster);
            }
        }

        private UltimaMonster MonsterFromUI()
        {
            int index = 0;
            UltimaMonsterItem mon = comboMonster.SelectedItem as UltimaMonsterItem;
            if (mon == null)
                index = m_monster == null ? 0 : m_monster.Index;
            else
                index = (int) mon.MonsterIndex;

            int indexEncounter = m_monster == null ? -1 : m_monster.EncounterIndex;

            UltimaMonster monster = new UltimaMonster((UltimaMonsterIndex) index);
            monster.HP = (int)nudHits.Value;
            monster.CurrentHP = (int)nudHits.Value;
            monster.Position = new Point((int)nudPositionX.Value, (int)nudPositionY.Value);
            monster.EncounterIndex = indexEncounter;
            UltimaTileItem tile = comboTravel.SelectedItem as UltimaTileItem;
            if (tile != null)
                monster.Travel = tile.OutdoorTile;
            return monster;
        }

        private void UpdateUI(UltimaMonster mon)
        {
            SetComboItems(mon.MapType, (UltimaMonsterIndex) mon.Index);
            Global.SetNud(nudHits, mon.CurrentHP);
            Global.SetNud(nudPositionX, mon.Position.X);
            Global.SetNud(nudPositionY, mon.Position.Y);
            if (mon.Travel <= UltimaOutdoorTile.AirCar)
                comboTravel.SelectedIndex = (int) mon.Travel + 1;
            comboTravel.Enabled = (mon.MapType == UltimaMapType.Overworld);
            labelIndex.Text = String.Format("{0}", mon.Index);
            labelEncounterIndex.Text = String.Format("{0}", mon.EncounterIndex);
            labelName.Text = mon.Name;
            labelDamage.Text = mon.DamageString;
            labelMapType.Text = UltimaMonster.MapTypeString(mon.MapType);
        }

        private void SetComboItems(UltimaMapType mapType, UltimaMonsterIndex index = UltimaMonsterIndex.None)
        {
            if (comboTravel.Items.Count == 0)
            {
                for (UltimaOutdoorTile tile = UltimaOutdoorTile.Border; tile <= UltimaOutdoorTile.AirCar; tile++)
                    comboTravel.Items.Add(new UltimaTileItem(tile));
            }
            if (index == UltimaMonsterIndex.None && comboMonster.Items.Count > 0)
                return;
            comboMonster.Items.Clear();
            {
                for (UltimaMonsterIndex mi = UltimaMonsterIndex.Ranger; mi <= UltimaMonsterIndex.Last; mi++)
                {
                    if (UltimaMonster.MonsterMapType(mi) == mapType)
                    {
                        comboMonster.Items.Add(new UltimaMonsterItem(mi));
                        if (mi == index)
                            comboMonster.SelectedIndex = comboMonster.Items.Count - 1;
                    }
                }
            }
        }

        private void UltimaMonsterEditForm_Load(object sender, EventArgs e)
        {
            SetComboItems(m_monster == null ? UltimaMapType.Dungeon : m_monster.MapType);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public class UltimaTileItem
    {
        public UltimaOutdoorTile OutdoorTile;
        public UltimaTileItem(UltimaOutdoorTile tile)
        {
            OutdoorTile = tile;
        }

        public override string ToString() => UltimaMaps.OutdoorTileName(OutdoorTile);
    }

    public class UltimaMonsterItem
    {
        public UltimaMonsterIndex MonsterIndex;
        public UltimaMonsterItem(UltimaMonsterIndex index)
        {
            MonsterIndex = index;
        }

        public override string ToString() => UltimaMonster.GetName(MonsterIndex);
    }
}
