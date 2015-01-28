using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    static class Spawner
    {
        public static List<Enemy> getInitialSpawn(string design_e, int difficulty)
        {
            Random rand = new Random();

            int split = rand.Next(1, 4);
            int row = difficulty % 8;
            int line = difficulty + 3 % (Console.BufferWidth / (design_e.Length + split * 2));

            List<Enemy> enemies = new List<Enemy>();

            for (; row > 0; row--)
            {
                for (int i = line; i > 0; i--)
                {
                    enemies.Add(new Enemy(design_e, 2 * split * i + i * design_e.Length + split, 3 * row, rand));
                }
            }

            return enemies;
        }
    }
}
