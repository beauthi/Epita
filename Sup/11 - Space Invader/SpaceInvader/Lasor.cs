using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceInvader
{
    class Lasor
    {
        protected ConsoleColor color; //<^>
        protected Vector2D direction;

        public int x { get; protected set; }
        public int y { get; protected set; }

        protected int lx, ly; //Last position
        protected bool canMove;

        public bool isLeavingKerbin { get; protected set; }

        protected System.Timers.Timer moveClock;

        public Lasor(ConsoleColor color , int x, int y, Vector2D direction, int iterDelay)
        {
            this.color = color;
            this.x = x;
            this.y = y;
            this.direction = direction;
            canMove = false;
            isLeavingKerbin = false;

            lx = Console.WindowWidth - 1;
            ly = Console.WindowHeight - 1;

            moveClock = new System.Timers.Timer(iterDelay);
            moveClock.Elapsed += moveEvent;
            moveClock.Start();
        }

        public void moveEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            canMove = true;
        }

        public void display()
        {
            if (lx != x || ly != y)
                Utils.displayAt(lx, ly, " ");

            Console.ForegroundColor = color;
            Utils.displayAt(x, y, "|");
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void update()
        {
            if(canMove)
                move();
            display();

            isLeavingKerbin = !Utils.inBounds(x, y);

            if(isLeavingKerbin)
                destroy();
        }

        public void move()
        {
            lx = x;
            ly = y;

            x += direction.x;
            y += direction.y;
            canMove = false;
        }

        public void destroy()
        {
            Utils.displayAt(x, y, " ");
        }
    }

    class Vector2D
    {
        public int x { get; set; }
        public int y { get; set; }

        public Vector2D(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}
