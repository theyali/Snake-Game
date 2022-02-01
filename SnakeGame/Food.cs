using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGame
{
    public class Food 
    {
        private static readonly Random Random = new Random();
        private Snake currentSnake;
        private readonly ConsoleColor FoodColor = ConsoleColor.Green;
        private Map map;
        private Pixel foodPixel;

        public Pixel FoodPixel { get { return foodPixel; } }


        public Food(Snake currentSnake, Map currentMap)
        {
            this.currentSnake=currentSnake;
            map=currentMap;
        }

        public Pixel GenerateFood()
        {
            do
            {
                foodPixel = new Pixel(Random.Next(1, map.MapWidth - 2), Random.Next(1, map.MapHeight - 2), FoodColor);
            } while (currentSnake.Head.X==foodPixel.X && currentSnake.Head.Y == foodPixel.Y
                    || currentSnake.Body.Any(b=>b.X==foodPixel.X || b.Y==foodPixel.Y));

            return foodPixel;
        }

        public void Draw()
        {
            foodPixel.Draw();
        }

        public void Clear()
        {
           foodPixel.Clear();
        }

    }
}
