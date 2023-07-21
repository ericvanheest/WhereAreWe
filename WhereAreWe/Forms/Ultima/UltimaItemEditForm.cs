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
    public partial class UltimaItemEditForm : Form
    {
        public UltimaItemEditForm()
        {
            InitializeComponent();
        }

        private UltimaInventory m_inventory;

        public UltimaInventory Inventory
        {
            get
            {
                m_inventory = InventoryFromUI();
                return m_inventory;    
            }

            set
            {
                m_inventory = value;
                UpdateUI(m_inventory);
            }
        }

        private UltimaInventory InventoryFromUI()
        {
            Ultima1Inventory inv = new Ultima1Inventory();
            for (UltimaItemIndex index = UltimaItemIndex.RedGem; index < UltimaItemIndex.Last; index++)
            {
                NumericUpDown nud = NudForItem(index);
                if (nud != null && nud.Value != 0)
                    inv.Items.Add(new UltimaItem(index, (int) nud.Value, false));
            }
            if (comboReadyArmor.SelectedIndex > 0)
                inv.Items.Add((comboReadyArmor.SelectedItem as UltimaEditItem).GetItem());
            if (comboReadyWeapon.SelectedIndex > 0)
                inv.Items.Add((comboReadyWeapon.SelectedItem as UltimaEditItem).GetItem());
            if (comboReadySpell.SelectedIndex > 0)
                inv.Items.Add((comboReadySpell.SelectedItem as UltimaEditItem).GetItem());

            return inv;
        }

        private void UpdateUI(UltimaInventory inv)
        {
            SetComboItems();
            foreach (UltimaItem item in inv.Items)
            {
                if (item.Ready)
                {
                    if (item.IsArmor)
                        comboReadyArmor.SelectedIndex = (item.ItemIndex - UltimaItemIndex.Skin);
                    else if (item.IsWeapon)
                        comboReadyWeapon.SelectedIndex = (item.ItemIndex - UltimaItemIndex.Hands);
                    else if (item.Type == ItemType.Spell)
                        comboReadySpell.SelectedIndex = (item.ItemIndex - UltimaItemIndex.Prayer);
                }
                else
                {
                    NumericUpDown nud = NudForItem(item.ItemIndex);
                    if (nud != null)
                        Global.SetNud(nud, item.Count);
                }
            }
        }

        private NumericUpDown NudForItem(UltimaItemIndex item)
        {
            switch (item)
            {
                case UltimaItemIndex.RedGem: return nudRedGem;
                case UltimaItemIndex.GreenGem: return nudGreenGem;
                case UltimaItemIndex.BlueGem: return nudBlueGem;
                case UltimaItemIndex.WhiteGem: return nudWhiteGem;
                case UltimaItemIndex.LeatherArmor: return nudLeatherArmor;
                case UltimaItemIndex.ChainMail: return nudChainMail;
                case UltimaItemIndex.PlateMail: return nudPlateMail;
                case UltimaItemIndex.VacuumSuit: return nudVacuumSuit;
                case UltimaItemIndex.ReflectSuit: return nudReflectSuit;
                case UltimaItemIndex.Dagger: return nudDagger;
                case UltimaItemIndex.Mace: return nudMace;
                case UltimaItemIndex.Axe: return nudAxe;
                case UltimaItemIndex.RopeSpikes: return nudRopeAndSpikes;
                case UltimaItemIndex.Sword: return nudSword;
                case UltimaItemIndex.GreatSword: return nudGreatSword;
                case UltimaItemIndex.BowArrows: return nudBowAndArrows;
                case UltimaItemIndex.Amulet: return nudAmulet;
                case UltimaItemIndex.Wand: return nudWand;
                case UltimaItemIndex.Staff: return nudStaff;
                case UltimaItemIndex.Triangle: return nudTriangle;
                case UltimaItemIndex.Pistol: return nudPistol;
                case UltimaItemIndex.LightSword: return nudLightSword;
                case UltimaItemIndex.Phazor: return nudPhazor;
                case UltimaItemIndex.Blaster: return nudBlaster;
                case UltimaItemIndex.Open: return nudOpen;
                case UltimaItemIndex.Unlock: return nudUnlock;
                case UltimaItemIndex.MagicMissile: return nudMagicMissile;
                case UltimaItemIndex.Steal: return nudSteal;
                case UltimaItemIndex.LadderDown: return nudLadderDown;
                case UltimaItemIndex.LadderUp: return nudLadderUp;
                case UltimaItemIndex.Blink: return nudBlink;
                case UltimaItemIndex.Create: return nudCreate;
                case UltimaItemIndex.Destroy: return nudDestroy;
                case UltimaItemIndex.Kill: return nudKill;
                case UltimaItemIndex.Horse: return nudHorse;
                case UltimaItemIndex.Cart: return nudCart;
                case UltimaItemIndex.Raft: return nudRaft;
                case UltimaItemIndex.Frigate: return nudFrigate;
                case UltimaItemIndex.Aircar: return nudAircar;
                case UltimaItemIndex.Shuttle: return nudShuttle;
                case UltimaItemIndex.TimeMachine: return nudTimeMachine;
                case UltimaItemIndex.EnemyVessels: return nudEnemyVessels;
                default: return null;
            }
        }

        private void UltimaItemEditForm_Load(object sender, EventArgs e)
        {
            SetComboItems();
        }

        private void SetComboItems()
        {
            if (comboReadyWeapon.Items.Count == 0)
            {
                for (UltimaItemIndex index = UltimaItemIndex.Hands; index <= UltimaItemIndex.Blaster; index++)
                    comboReadyWeapon.Items.Add(new UltimaEditItem(index));
            }
            if (comboReadyArmor.Items.Count == 0)
            {
                for (UltimaItemIndex index = UltimaItemIndex.Skin; index <= UltimaItemIndex.ReflectSuit; index++)
                    comboReadyArmor.Items.Add(new UltimaEditItem(index));
            }
            if (comboReadySpell.Items.Count == 0)
            {
                for (UltimaItemIndex index = UltimaItemIndex.Prayer; index <= UltimaItemIndex.Kill; index++)
                    comboReadySpell.Items.Add(new UltimaEditItem(index));
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    public class UltimaEditItem
    {
        public UltimaItemIndex Index;
        public override string ToString() => UltimaItem.GetName(Index);
        public UltimaItem GetItem() => new UltimaItem(Index, 1, true);

        public UltimaEditItem(UltimaItemIndex index)
        {
            Index = index;
        }
    }
}
