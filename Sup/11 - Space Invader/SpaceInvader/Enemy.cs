using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    class Enemy : Character
    {
        protected System.Timers.Timer moveClock;
        protected Random rand;

        public Enemy(string design, int x, int y, Random rand)
            : base(design, x, y)
        {
            moveClock = new System.Timers.Timer(300);
            moveClock.Elapsed += moveEvent;
            this.rand = rand;
            sound_laser.Play();
        }

        public void update(Player player, List<Enemy> enemies)
        {
            base.update();

            //These two won't trigger at each loop, just a way to have
            //Action done in active certain order i didn't called them with 
            //the elapsed event
            if(canMove)
                move();
            if (canShoot && rand.Next(0,50) == 4)
                shoot();

            collideCheck(player);

            if (y >= Console.WindowHeight - 1)
            {
                player.takeDamage(9000); //Just to be sure...
            }
        }

        public void shoot()
        {
            canShoot = false;
            if(life > 0)
                lasors.Add(new Lasor(ConsoleColor.Red, x + design.Length / 2, y + 1, new Vector2D(0, 1), 100));
            shootClock.Start();
        }

        public void moveEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            moveClock.Stop();
            canMove = true;
        }

        public void move()
        {
            canMove = false;
            lx = x;
            ly = y;

            if (x > 0 && x < Console.BufferWidth - design.Length)
                x += (y % 2 == 0 ? 1 : -1);

            if (!(x > 0 && x < Console.BufferWidth - design.Length))
            {
                y += 3;
                x += (x == 0 ? 1 : -1);
            }
            moveClock.Start();
        }

        void collideCheck(Player player)
        {  
            for (int i = 0; i < lasors.Count; i++)
            {
                if (player.isTouching(lasors[i].x, lasors[i].y))
                {
                    player.takeDamage(20);
                    lasors[i].destroy();
                    lasors.RemoveAt(i);
                }

            }

            if (player.isTouching(x, y) || player.isTouching(x + design.Length, y))
            {
                player.takeDamage(90);
                takeDamage(200);
            }
        }
    }
}
