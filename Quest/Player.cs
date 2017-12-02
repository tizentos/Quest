using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Quest
{
    class Player:Mover
    {
        private Weapon equippedWeapon;
        private int hitPoints;
        public int HitPoints{get{return hitPoints;}}
        private List<Weapon> inventory = new List<Weapon>();
        public List<string> Weapons
        {
            get
            {
                List<string> names = new List<string>();
                foreach (Weapon weapon in inventory)
                    names.Add(weapon.Name);
                return names;
            }
        }
        public Player(Game game, Point location)
            : base(game, location)
        {
            hitPoints = 10;
        }
        public void Hit(int maxDamage, Random random)
        {
            hitPoints -= random.Next(1, maxDamage);
        }
        public void increaseHealth(int health, Random random)
        {
            hitPoints += random.Next(1, health);
        }
        public void Equip(string weaponName)
        {
            foreach (Weapon weapon in inventory)
                if (weapon.Name == weaponName)
                {
                    equippedWeapon = weapon;
                }
        }
        public void Move(Direction direction)
        {
            base.location = Move(direction, game.Boundaries);
            //the code i added
            if (!game.WeaponInRoom.PickedUp)
                //if its nearby a weapon ,it picks it and equip it immediately if its only one in the inventory
                if (game.WeaponInRoom.NearBy(game.WeaponInRoom.Location, 10))
                {
                    game.WeaponInRoom.PickUpWeapon();  //player picks up weapon
                    this.inventory.Add(game.WeaponInRoom); //adds to inventory
                    if (inventory.IndexOf(game.WeaponInRoom)==0)  //subject to edit
                        this.Equip(game.WeaponInRoom.Name);
                }                
        }
        public void Attack(Direction direction, Random random)
        {
            equippedWeapon.Attack(direction, random);
        }
    }
}
