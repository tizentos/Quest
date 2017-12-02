using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quest
{
    public enum Direction
    {
        Up,
        Down,
        Right,
        Left
    }
    public partial class Form1 : Form
    {
        private Game game;
        private Random random = new Random();
        public Form1()
        {
            InitializeComponent();
            
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            game = new Game(new Rectangle(78, 57, 420, 155));
            game.NewLevel(random);
            UpdateCharacters();
            InventoryArrowPix.Visible = false;
            InventoryBluePix.Visible = false;
            InventoryMacePix.Visible = false;
            InventoryRedPix.Visible = false;
            InventorySwordPix.Visible = false;
        }

        private void InventorySwordPix_Click(object sender, EventArgs e)
        {
            if (game.CheckPlayerInventory("Sword"))
            {
                game.Equip("Sword");
                InventorySwordPix.BorderStyle = BorderStyle.FixedSingle;
                InventoryArrowPix.BorderStyle = BorderStyle.None;
                InventoryBluePix.BorderStyle = BorderStyle.None;
                InventoryMacePix.BorderStyle = BorderStyle.None;
                InventoryRedPix.BorderStyle = BorderStyle.None;
            }
            UpdateCharacters();
        }

        private void InventoryBluePix_Click(object sender, EventArgs e)
        {
            if (game.CheckPlayerInventory("Blue Potion"))
            {
                game.Equip("Blue Potion");
                game.Equip("Red Potion");
                buttonAttDown.Visible = false;
                buttonAttLeft.Visible = false;
                buttonAttRight.Visible = false;
                buttonAttUp.Text = "Drink";
                InventoryBluePix.BorderStyle = BorderStyle.FixedSingle;
                InventoryArrowPix.BorderStyle = BorderStyle.None;
                InventorySwordPix.BorderStyle = BorderStyle.None;
                InventoryMacePix.BorderStyle = BorderStyle.None;
                InventoryRedPix.BorderStyle = BorderStyle.None;
            }
            UpdateCharacters();
        }

        private void InventoryRedPix_Click(object sender, EventArgs e)
        {
            if (game.CheckPlayerInventory("Red Potion"))
            {
                InventorySwordPix.BorderStyle = BorderStyle.FixedSingle;
                game.Equip("Red Potion");
                buttonAttDown.Visible = false;
                buttonAttLeft.Visible = false;
                buttonAttRight.Visible = false;
                buttonAttUp.Text = "Drink";
            }
            UpdateCharacters();
        }

        private void InventoryMacePix_Click(object sender, EventArgs e)
        {
            if (game.CheckPlayerInventory("Mace"))
            {
                InventoryMacePix.BorderStyle = BorderStyle.FixedSingle;
                InventoryArrowPix.BorderStyle = BorderStyle.None;
                InventoryBluePix.BorderStyle = BorderStyle.None;
                InventorySwordPix.BorderStyle = BorderStyle.None;
                InventoryRedPix.BorderStyle = BorderStyle.None;
                game.Equip("Mace");
            }
            UpdateCharacters();
        }

        private void InventoryArrowPix_Click(object sender, EventArgs e)
        {
            if (game.CheckPlayerInventory("Bow"))
            {
                InventoryArrowPix.BorderStyle = BorderStyle.FixedSingle;
                InventorySwordPix.BorderStyle = BorderStyle.None;
                InventoryBluePix.BorderStyle = BorderStyle.None;
                InventoryMacePix.BorderStyle = BorderStyle.None;
                InventoryRedPix.BorderStyle = BorderStyle.None;
                game.Equip("Bow");
            }
            UpdateCharacters();
        }

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Up, random);
            UpdateCharacters();
           foreach (Enemy enemy in game.Enemies)
            {
                 enemy.Move(random);
                UpdateCharacters();
            }
            UpdateCharacters();
        }

        private void buttonMoveLeft_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Left, random);
            UpdateCharacters();
            foreach (Enemy enemy in game.Enemies)
            {
                enemy.Move(random);
                UpdateCharacters();
            }
            UpdateCharacters();
        }

        private void buttonMoveRight_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Right, random);
            UpdateCharacters();
            foreach (Enemy enemy in game.Enemies)
            {
                enemy.Move(random);
                UpdateCharacters();
            }
            UpdateCharacters();
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            game.Move(Direction.Down, random);
            UpdateCharacters();
            foreach (Enemy enemy in game.Enemies)
            {
                enemy.Move(random);
                UpdateCharacters();
            }
            UpdateCharacters();
        }

        private void buttonAttUp_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Up, random);
            UpdateCharacters();
            foreach (Enemy enemy in game.Enemies)
            {
                enemy.Move(random);
                UpdateCharacters();
            }
            UpdateCharacters();
        }

        private void buttonAttLeft_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Left, random);
            UpdateCharacters();
            foreach (Enemy enemy in game.Enemies)
            {
                enemy.Move(random);
                UpdateCharacters();
            }
            UpdateCharacters();
        }

        private void buttonAttRight_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Right, random);
            UpdateCharacters();
            foreach (Enemy enemy in game.Enemies)
            {
                enemy.Move(random);
                UpdateCharacters();
            }
            UpdateCharacters();
        }

        private void buttonAttDown_Click(object sender, EventArgs e)
        {
            game.Attack(Direction.Down, random);
            UpdateCharacters();
            foreach (Enemy enemy in game.Enemies)
            {
                enemy.Move(random);
                UpdateCharacters();
            }
            UpdateCharacters();
        }
        public void UpdateCharacters()
        {
            Player.Location = game.PlayerLocation;
            playerHitPoint.Text = game.PlayerHitPoints.ToString();
            foreach (string weapon in game.PlayerWeapons)
                switch (weapon)
                {
                    case "Sword":
                        InventorySwordPix.Visible = true;
                        break;
                    case "Bow":
                        InventoryArrowPix.Visible = true;
                        break;
                    case "Mace":
                        InventoryMacePix.Visible = true;
                        break;
                    case "Blue Potion":
                        InventoryBluePix.Visible = true;
                        break;
                    case "Red Potion":
                        InventoryRedPix.Visible = true;
                        break;
                    default:
                        break;
                }
            bool showBat = false;
            bool showGhost = false;
            bool showGhoul = false;
            int enemiesShown = 0;
            foreach (Enemy enemy in game.Enemies)
            {
                if (enemy is Bat)
                {
                    bat.Location = enemy.Location;
                    BatHitPoint.Text = enemy.HitPoints.ToString();
                    if (enemy.HitPoints > 0)
                    {
                        showBat = true;
                        enemiesShown++;
                    }
                }
                if (enemy is Ghost)
                {
                    ghost.Location = enemy.Location;
                    GhostHitPoint.Text = enemy.HitPoints.ToString();
                    if (enemy.HitPoints > 0)
                    {
                        showGhost = true;
                        enemiesShown++;
                    }
                }
                if (enemy is Ghoul)
                {
                    ghoul.Location = enemy.Location;
                    GhoulHitPoint.Text = enemy.HitPoints.ToString();
                    if (enemy.HitPoints > 0)
                    {
                        showGhoul = true;
                        enemiesShown++;
                    }
                }
                if (showBat == false)
                {
                    bat.Visible = false;
                    BatHitPoint.Text = "0";
                }
                if (showGhost == false)
                {
                    ghost.Visible = false;
                    GhostHitPoint.Text = "0";
                }
                if (showGhoul == false)
                {
                    ghoul.Visible = false;
                    GhoulHitPoint.Text = "0";
                }
            }
            bat.Visible = showBat;
            ghost.Visible = showGhost;
            ghoul.Visible = showGhoul;

            Sword.Visible = false;
            Arrow.Visible = false;
            RedPotion.Visible = false;
            BluePotion.Visible = false;
            Mace.Visible = false;
            Control weaponControl = null;
            switch (game.WeaponInRoom.Name)
            {
                case "Sword":
                    weaponControl = Sword; break;
                case "Bow":
                    weaponControl = Arrow; break;
                case "Mace":
                    weaponControl = Mace; break;
                case "Red Potion":
                    weaponControl = RedPotion;
                    break;
                case "Blue Potion":
                    weaponControl = BluePotion; break;
                default:
                    break;
            }
            weaponControl.Visible = true;
          /*  foreach (string weapon in game.PlayerWeapons)
            {
                if (game.CheckPlayerInventory(weapon) && (weapon == "Sword"))
                    Sword.Visible = true;
                if (game.CheckPlayerInventory(weapon) && (weapon == "Bow"))
                    Arrow.Visible = true;
                if (game.CheckPlayerInventory(weapon) && (weapon == "Mace"))
                    Mace.Visible = true;
                if (game.CheckPlayerInventory(weapon) && (weapon == "Blue Potion"))
                    BluePotion.Visible = true;
                if (game.CheckPlayerInventory(weapon) && (weapon == "Red Potion"))
                    RedPotion.Visible = true;
            } */
            weaponControl.Location = game.WeaponInRoom.Location;
            if (game.WeaponInRoom.PickedUp)
                weaponControl.Visible = false;
            else
                weaponControl.Visible = true;
            if (game.PlayerHitPoints<=0)
            {
                MessageBox.Show("You Died");
                Application.Exit();
            }
            if (enemiesShown < 1)
            {
                MessageBox.Show("You have defeated all enemies");
                game.NewLevel(random);
                UpdateCharacters();
            }
            Control inventoryControl = new Control();
            buttonAttDown.Visible = true;
            buttonAttLeft.Visible = true;
            buttonAttRight.Visible = true;
            buttonAttUp.Text = "Up";
            Level.Text = game.Level.ToString();
        }
        
    }
}
