using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace SpaceInvader
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Title = "SpaceIndvader";
            Console.BufferHeight = Console.WindowHeight;

            
            string design_p = "";
            string design_e = "";
            int difficulty = get_param(ref design_p, ref design_e);

            //string design_p = "/^\\";
            //string design_e = "<-->";
            //int difficulty = 2;

            Console.Clear();
            bool play = true;

            Player player = new Player(design_p, 20,20);
            List<Enemy> enemies = Spawner.getInitialSpawn(design_e, difficulty);

            while (play && player.life > 0 && enemies.Count > 0)
            {
                Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 2);
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo k =  Console.ReadKey();
                    if (k.Key == ConsoleKey.Spacebar)
                        player.shoot();
                    else if (k.Key == ConsoleKey.Escape)
                        play = false;
                    else
                        player.move(k.Key);
                }

                player.update(enemies);
                for(int i = 0 ; i < enemies.Count ; i++)
                {
                    enemies[i].update(player, enemies);
                    if(enemies[i].lasors.Count == 0 && enemies[i].life <= 0)
                        enemies.RemoveAt(i);
                }

            }

            if (player.life == 0)
            {
                Console.SetCursorPosition(5, 6);
                Console.WriteLine("You just lost at something...");
            }

            if (enemies.Count == 0)
            {
                Console.SetCursorPosition(5, 6);
                Console.WriteLine("HA HA ! Well play !");
            }

            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.WriteLine("Press 'ANY' key to continue");
            Console.ReadLine();

        }

        static int get_param(ref string player_design, ref string enemy_design)
        {
            string[] instructions = new string[3] 
                { "Please, enter enemy design :",
                  "Please, enter your ship design :",
                  "Difficulty ? (int from 1 to 5)"};

            int difficulty = -1;

            for (int i = 0; i < 3; i++)
            {
                Console.WriteLine(instructions[i]);
                string in_ = Console.ReadLine();

                try
                {
                    if (in_.Replace(" ", "") == "")
                        throw new InputException("Nothing to read...");
                    if (i == 2 && !int.TryParse(in_, out difficulty))
                        throw new InputException("Unable to parse your difficulty level");
                    if (i == 2 && difficulty <= 0 && difficulty > 6)
                        throw new InputException("Difficulty must be over 0 and below 6 (over would be nonsense) !");

                    if (i == 0)
                        enemy_design = in_;
                    if (i == 1)
                        player_design = in_;
                }
                catch (InputException e)
                {
                    Console.WriteLine(e.Message);
                    i--;
                }
            }

            return difficulty;
        }
    }

    class InputException : Exception
    {
        public InputException() : base() {}
        public InputException(string msg) : base(msg) {}
    }

    static class Utils
    {
        public static bool inBounds(int x, int y)
        {
            return (x < Console.BufferWidth && x > 0
                   && y > 0 && y < Console.WindowHeight - 1);
        }

        //check for out of bounds inside
        public static void displayAt(int x, int y, string s)
        {
            if (inBounds(x, y))
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
        }
    }
}
