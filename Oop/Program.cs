using System;

namespace Oop
{
    class Program
    {
        static void Main(string[] args)
        {
            NPC npc = new NPC();
            npc.MoveLeft();
            Renderer renderer = new Renderer(npc);

            renderer.Draw();
        }
    }

    class Renderer
    {
        private NPC npc;

        public Renderer(NPC pC)
        {
            if (pC==null)
            {
                throw new ArgumentNullException("Npc is null");
            }
            npc = pC;
        }
        public void Draw()
        {
            npc.GetX();
        }
    }

    class NPC
    {
        private int x, y;

        public int X { 
            get
            {
                return x;
            } 
            private set 
            { 
                x= value;
            }
        }
        public NPC(int x = 0, int y = 0)
        {
            this.x = x;
            this.y = y;
        }
       

        public void MoveRight()
        {
            X++;
        }

        public void MoveLeft()
        {
            X--;
            if (X<0)
            {
                X = 0;
            }
        }
    }
}
