using System;

namespace SnakeGame
{
    public class Map
    {
        private const int mapWidth = 30;
        private const int mapHeight = 20;
        private const ConsoleColor borderColor = ConsoleColor.Gray;


        public int MapWidth { get { return mapWidth; } }
        public int MapHeight { get { return mapHeight; } }
        public ConsoleColor BorderColor { get { return borderColor; } }


        
    }
}
