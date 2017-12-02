using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Quest
{
    abstract class Weapon:Mover
    {
        protected Game game;
        private bool pickedUp;
        public bool PickedUp { get { return pickedUp; } }
        private Point location;
        public Point Location { get { return location; } }

        public Weapon(Game game, Point location) :base(game,location)
        {
            this.game = game;
            this.location = location;
            pickedUp = false;
        }
        public void PickUpWeapon()
        {
            pickedUp = true;
        }
        public abstract string Name { get; }
        public abstract void Attack(Direction direction, Random random);
        public bool NearBy(Point locationToCheck,Point playerLocation,int radius)
        {
            if (Math.Abs(playerLocation.X - locationToCheck.X) < radius &&
               Math.Abs(playerLocation.Y - locationToCheck.Y) < radius)
            {
                return true;

            }
            else
                return false;
        }
        protected bool DamageEnemy(Direction direction, int radius, int damage, Random random)
        {
            Point target = game.PlayerLocation;
            for (int distance = 0; distance < radius; distance++)
            {
                foreach (Enemy enemy in game.Enemies)
                {
                    if (NearBy(enemy.Location, target, radius))
                    {
                        enemy.Hit(damage, random);
                        return true;
                    }
                }
                target = Move(direction,target,game.Boundaries);
            }
            return false;
        }
        public Point Move(Direction direction,Point target, Rectangle boundaries)
        {

            Point newLocation = target;
            switch (direction)
            {
                case Direction.Up:
                    if (newLocation.Y - MoveInterval >= boundaries.Top)
                        newLocation.Y -= MoveInterval;
                    break;
                case Direction.Down:
                    if (newLocation.Y + MoveInterval <= boundaries.Bottom)
                        newLocation.Y += MoveInterval;
                    break;
                case Direction.Right:
                    if (newLocation.X - MoveInterval >= boundaries.Right)
                        newLocation.X += MoveInterval;
                    break;
                case Direction.Left:
                    if (newLocation.X - MoveInterval >= boundaries.Left)
                        newLocation.X -= MoveInterval;
                    break;
                default:
                    break;
            }
            return newLocation;
        }
    }
    class Sword : Weapon
    {
        public Sword(Game game, Point location)
            : base(game, location)
        { }
        public override string Name
        {
            get { return "Sword"; }
        }
        public override void Attack(Direction direction, Random random)
        {
            Direction next_attack = direction;
            switch (direction)
            {
                case Direction.Up:
                    DamageEnemy(direction, 10, 3, random);
                    if (!DamageEnemy(direction, 10, 3, random))
                    {
                        next_attack = (Direction)2;
                        if (!DamageEnemy(next_attack, 10, 3, random))
                            next_attack = (Direction)3;
                        DamageEnemy(next_attack, 10, 3, random);
                    }
                    break;
                case Direction.Down:
                    DamageEnemy(direction, 10, 3, random);
                    if (!DamageEnemy(direction, 10, 3, random))
                    {
                        next_attack = (Direction)3;
                        if (!DamageEnemy(next_attack, 10, 3, random))
                            next_attack = (Direction)2;
                        DamageEnemy(next_attack, 10, 3, random);
                    }
                    break;
                case Direction.Right:
                    DamageEnemy(direction, 10, 3, random);
                    if (!DamageEnemy(direction, 10, 3, random))
                    {
                        next_attack = (Direction)1;
                        if (!DamageEnemy(next_attack, 10, 3, random))
                            next_attack = (Direction)0;
                        DamageEnemy(next_attack, 10, 3, random);
                    }
                    break;
                case Direction.Left:
                    DamageEnemy(direction, 10, 3, random);
                    if (!DamageEnemy(direction, 10, 3, random))
                    {
                        next_attack = (Direction)0;
                        if (!DamageEnemy(next_attack, 10, 3, random))
                            next_attack = (Direction)1;
                        DamageEnemy(next_attack, 10, 3, random);
                    }
                    break;
                default:
                    break;
            }
        }
    }
    class Bow : Weapon
    {
        public Bow(Game game, Point location)
            : base(game, location)
        { }
        public override string Name
        {
            get { return "Bow"; }
        }
        public override void Attack(Direction direction, Random random)
        {
            Direction next_attack = direction;
            switch (direction)
            {
                case Direction.Up:
                    DamageEnemy(direction, 30, 1, random);
                    break;
                case Direction.Down:
                    DamageEnemy(direction, 30, 3, random);
                    break;
                case Direction.Right:
                    DamageEnemy(direction, 30, 3, random);
                    break;
                case Direction.Left:
                    DamageEnemy(direction, 30, 3, random);
                    break;
                default:
                    break;
            }
        }
    }
    class Mace : Weapon
    {
        public Mace(Game game, Point location)
            : base(game, location)
        { }
        public override string Name
        {
            get { return "Mace"; }
        }
        public override void Attack(Direction direction, Random random)
        {
            Direction next_attack = direction;
            bool damage=DamageEnemy(direction,20,6,random);
            int i=0;
            while(!damage)
            {
                DamageEnemy((Direction)i,20,6,random);
                damage=DamageEnemy((Direction)i,20,6,random);
                i++;
                if (i>=4)
                    break;
            }
        }
    }
    class BluePotion : Weapon, IPotion
    {
        override public string Name { get { return "Blue Potion"; } }
        public bool Used { get; set; }
        public BluePotion(Game game, Point location) : base(game, location) 
        {   }
        public override void Attack(Direction direction, Random random)
        {
            if (!Used)
                game.IncreasePlayerHealth(5, random);
            Used = true;
        }
    }
    class RedPotion : Weapon, IPotion
    {
        override public string Name { get { return "Red Potion"; } }
        public bool Used { get; set; }
        public RedPotion(Game game, Point location)
            : base(game, location)
        { }
        public override void Attack(Direction direction, Random random)
        {
            if (!Used)
                game.IncreasePlayerHealth(10, random);
            Used = true;
        }
    }
}
