using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Quest
{
    abstract class Enemy:Mover
    {
        private const int NearPlayerDistance = 25;
        private int hitPoints;
        public int HitPoints { get { return hitPoints; } }
        public bool Dead
        {
            get
            {
                if (hitPoints <= 0) return true;
                else return false;
            }
        }
        public Enemy(Game game, Point location, int hitPoints)
            : base(game, location)
        {
            this.hitPoints = hitPoints;
        }
        public abstract void Move(Random random);
        public void Hit(int maxDamage, Random random)
        {
            hitPoints -= random.Next(1, maxDamage);
        }
        protected bool NearPlayer()
        {
            return (NearBy(game.PlayerLocation, NearPlayerDistance));
        }
        protected Direction FindPlayerDirection(Point playerLocation)
        {
            Direction directionToMove;
            if (playerLocation.X > location.X + 10)
                directionToMove = Direction.Right;
            else if (playerLocation.X < location.X)
                directionToMove = Direction.Left;
            else if (playerLocation.Y < location.Y - 10)
                directionToMove = Direction.Up;
            else
                directionToMove = Direction.Down;
            return directionToMove;
        }
    }
    class Bat : Enemy
    {
        public Bat(Game game, Point location)
            : base(game, location, 6)
        { }
        public override void Move(Random random)
        {
            Random decideMoveTowardsPlayer=new Random();
            random = new Random();
            Direction move_one = FindPlayerDirection(game.PlayerLocation);
            Direction move_two=(Direction)random.Next(4);
            if (decideMoveTowardsPlayer.Next(2) == 0)
                this.location= base.Move(move_one, game.Boundaries);
            else
                this.location=base.Move(move_two, game.Boundaries);
            if (NearPlayer())
                game.HitPlayer(2, random);
        }
    }
    class Ghost : Enemy
    {
        public Ghost(Game game, Point location)
            : base(game, location, 8)
        { }
        public override void Move(Random random)
        {
            Random decideMove = new Random();
            random = new Random();
            Direction move_one = FindPlayerDirection(game.PlayerLocation);
            Direction move_two = (Direction)random.Next(4);
            if ((decideMove.Next(3) == 0) && (decideMove.Next(3) == 2))
                return;
            else
                if (decideMove.Next(2) == 0)
                    this.location=base.Move(move_one, game.Boundaries);
                else
                    this.location=base.Move(move_two, game.Boundaries);
            if (NearPlayer())
                game.HitPlayer(3, random);
        }
    }
    class Ghoul : Enemy
    {
        public Ghoul(Game game, Point location)
            : base(game, location, 10)
        { }
        public override void Move(Random random)
        {
            Random decideMove = new Random();
            random = new Random();
            Direction move_one = FindPlayerDirection(game.PlayerLocation);
            if ((decideMove.Next(3) == 0) && (decideMove.Next(3) == 2))
                this.location=base.Move(move_one, game.Boundaries);
            else
                return;
            if (NearPlayer())
                game.HitPlayer(4, random);
        }
    }
}
