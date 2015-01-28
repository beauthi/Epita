using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    class Player : Character
    {
        public Player(string design, int x, int y)
            : base(design, x, y)
        {
            shootClock.Interval = 350;
        }

        public void update(List<Enemy> enemies)
        {
            base.update();
            collideCheck(enemies);
        }

        public void shoot()
        {
            if (canShoot)
            {
                sound_laser.Play();
                canShoot = false;
                if (life > 0)
                    lasors.Add(new Lasor(ConsoleColor.Green, x + design.Length / 2, y - 1, new Vector2D(0, -1), 100));
                shootClock.Start();
            }
        }

        public void move(ConsoleKey key)
        {
            ly = y;
            lx = x;
                
            if (key == ConsoleKey.LeftArrow)
                x -= (x > 1 ? 1 : 0);
            if (key == ConsoleKey.RightArrow)
                x += (x + design.Length < Console.BufferWidth - 1 ? 1 : 0);
        }

        void collideCheck(List<Enemy> enemies)
        {

            for (int i = 0; i < lasors.Count; i++)
            {
                for (int j = 0; j < enemies.Count; j++)
                {
                    if (enemies[j].life > 0)
                    {
                        Enemy e = enemies[j];
                        if (enemies[j].isTouching(lasors[i].x, lasors[i].y))
                        if (e.isTouching(lasors[i].x, lasors[i].y))
                        {
                            enemies[j].takeDamage(50);
                            lasors[i].destroy();
                            lasors.RemoveAt(i);
                            j = enemies.Count;
                            i--;
                        }
                    }
                }

            }

        }
    }
}
