using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Media;

namespace SpaceInvader
{
    class Character
    {
        protected string design; //<^>
        protected int x, y; //Current position
        protected int lx, ly; //Last position

        public int life { get; protected set; }
        public bool active { get; set; }
        public List<Lasor> lasors { get; protected set; }

        protected bool canShoot;
        protected bool canMove;

        protected System.Timers.Timer shootClock;

        protected SoundPlayer sound_laser;
        protected SoundPlayer sound_wilhelm;

        public Character(string design, int x, int y)
        {
            this.design = design;
            this.x = x;
            this.y = y;

            lx = Console.WindowWidth - 1;
            ly = Console.WindowHeight - 1;

            active = true;
            life = 100;
            lasors = new List<Lasor>();

            canShoot = true;
            canMove = true;

            shootClock = new System.Timers.Timer(1200);
            shootClock.Elapsed += shootEvent;
            
            //Sound

            sound_laser = new SoundPlayer("../../pew.wav");
            sound_wilhelm = new SoundPlayer("../../cri_wilhelm.wav");
        }

        public void display()
        {
            if (lx < x)
                Utils.displayAt(lx, ly," ");
            if (lx > x)
                Utils.displayAt(lx + design.Length - 1, ly, " ");

            if (ly != y)
                for (int i = 0; i < design.Length; i++)
                    Utils.displayAt(lx + i, ly, " ");

            if (life > 50)
                Console.ForegroundColor = ConsoleColor.White;
            else if (life > 20)
                Console.ForegroundColor = ConsoleColor.Yellow;
            else
                Console.ForegroundColor = ConsoleColor.DarkRed;

            if(Utils.inBounds(x + design.Length - 1, y))
                Utils.displayAt(x, y, design);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void shootEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            shootClock.Stop();
            canShoot = true;
        }

        public void update()
        {
            if(life > 0)
                display();

            for (int i = 0; i < lasors.Count; i++)
            {
                lasors[i].update();
                if (lasors[i].isLeavingKerbin)
                    lasors.RemoveAt(i);
            }
             
        }

        public void takeDamage(int damage)
        {
            life -= damage;

            if (life <= 0)
                destroy();
        }

        public void destroy()
        {
            sound_wilhelm.Play();
            life = 0;
            for (int i = 0; i <= design.Length; i++)
                Utils.displayAt(x + i, y, " ");
        }

        public bool isTouching(int x, int y)
        {
            return x >= this.x && y == this.y
                && x < this.x + design.Length;
        }
    }
}
